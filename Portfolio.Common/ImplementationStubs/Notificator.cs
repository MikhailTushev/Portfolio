using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Portfolio.Common.ImplementationStubs
{
    public class Notificator : INotificator
    {
        private readonly ILogger<Notificator> _logger;

        public Notificator(ILogger<Notificator> logger)
        {
            _logger = logger;
        }

        public Task SendCreateMaskAsync(long userId) => CreateAndPushNotificationAsync("Маска создана", userId);

        private async Task CreateAndPushNotificationAsync(string text, long userId)
        {
            // try
            // {
            //     _provider.PushMessage(new QueueMessageDefault() {Uid = Guid.NewGuid(), Body = JsonSerializer.Serialize(notice)});
            // }
            // catch (Exception ex)
            // {
            //     _logger.LogError(
            //         ex,
            //         "An error occurred while creating notifications for document with id {documentId} and contacts id {contactId}",
            //         documentId,
            //         string.Join(',', contactId));
            // }
        }
    }
}