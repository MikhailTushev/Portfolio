using System.Threading.Tasks;
using Portfolio.Common;

namespace Portfolio.Domain.Events
{
    public delegate Task QueueMessageHandler(IQueueMessage message);

    public interface IQueueProvider
    {
        void PushMessage(IQueueMessage message);
    }
}