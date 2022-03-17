using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Portfolio.Common;
using Portfolio.Common.Exceptions;
using Portfolio.Domain.Dto;

namespace Portfolio.Web.Hubs
{
    public class Notificator : INotificator
    {
        private readonly ConcurrentDictionary<long, List<string>> _users;
        private readonly ILogger<Notificator> _logger;
        private readonly IServiceProvider _provider;

        public Notificator(
            ILogger<Notificator> logger,
            IQueueWatcher<IQueueMessage> watcher,
            IServiceProvider provider)
        {
            _logger = logger;
            _provider = provider;

            watcher.Subscribe += SendNotificationAsync;
            watcher.Error += LoggingError;

            _users = new ConcurrentDictionary<long, List<string>>();
        }

        public Task AddUserAsync(long userId, string connectionId)
        {
            if (string.IsNullOrWhiteSpace(connectionId))
            {
                _logger.LogError("ConnectionId is empty for {userId}.", userId);
                return Task.CompletedTask;
            }

            _users.AddOrUpdate(
                userId,
                _ => new List<string>()
                    {connectionId,},
                (_, oldValue) =>
                {
                    oldValue.Add(connectionId);
                    return oldValue;
                });

            return Task.CompletedTask;
        }

        public Task RemoveUserAsync(string connectionId)
        {
            if (string.IsNullOrWhiteSpace(connectionId))
            {
                return Task.CompletedTask;
            }

            var matches = _users.Where(x => x.Value.Any(y => y == connectionId)).Select(x => x.Key);

            if (!matches.Any())
            {
                _logger.LogError("ConnectionId {connectionId} is not exist.", connectionId);
                return Task.CompletedTask;
            }

            var key = matches.SingleOrDefault();
            if (!_users.TryGetValue(key, out var listConnection))
            {
                return Task.CompletedTask;
            }

            var newConnections = listConnection.Where(x => x != connectionId).ToList();

            if (newConnections.Count < 2)
            {
                _users.TryRemove(key, out _);
            }
            else
            {
                _users.TryUpdate(key, newConnections, listConnection);
            }
            
            return Task.CompletedTask;
        }

        public async Task SendNotificationAsync(IQueueMessage message)
        {
            var notice = JsonSerializer.Deserialize<UserDto>(message.Body);
            using (var scope = _provider.CreateScope())
            {
                var hubContext = scope.ServiceProvider.GetService<IHubContext<NotifyHub>>();

                if (notice is not null && _users.TryGetValue(notice.Id, out var listConnections))
                {
                    await hubContext.Clients.Clients(listConnections).SendAsync("SendNotificationAsync", message.Body);
                }
            }
        }

        public void LoggingError(object? sender, ExtThreadExceptionEventArgs args) => 
            _logger.LogError(
            args.Exception,
            "Some error is happened on notification flow: {message} with object: {object}",
            args.Message,
            JsonSerializer.Serialize(sender));
    }
}