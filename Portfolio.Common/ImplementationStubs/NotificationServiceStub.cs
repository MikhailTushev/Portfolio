using System;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Portfolio.Domain.Dto;
using Portfolio.Domain.Events;

namespace Portfolio.Common.ImplementationStubs
{
    public class NotificationServiceStub : INotificationService
    {
        private readonly ILogger<NotificationServiceStub> _logger;
        private readonly IQueueProvider _provider;

        public NotificationServiceStub(ILogger<NotificationServiceStub> logger, IQueueProvider provider)
        {
            _logger = logger;
            _provider = provider;
        }

        public Task SendCreateMaskAsync(long userId) => CreateAndPushNotificationAsync("Маска создана", userId);

        private Task CreateAndPushNotificationAsync(string text, long userId)
        {
            try
            {
                _provider.PushMessage(new QueueMessageDefault() {Body = JsonSerializer.Serialize(new NotifyDto {UserId = userId, Text = text})});
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "An error occurred while creating notifications for user with id {userId}",
                    userId);
            }

            return Task.CompletedTask;
        }
    }
}