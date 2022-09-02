using System;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Portfolio.Domain.Dto;

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

        public Task SendCreateMaskAsync(long userId) => CreateAndPushNotificationAsync("Mask created", userId);

        private Task CreateAndPushNotificationAsync(string text, long userId)
        {
            try
            {
                var b = JsonSerializer.Serialize(new NotifyDto { UserId = userId, Text = text });
                _provider.PushMessage(new QueueMessageDefault() {Body = b});
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