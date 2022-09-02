using System.Threading.Tasks;

namespace Portfolio.Common
{
    public delegate Task QueueMessageHandler(IQueueMessage message);

    public interface IQueueProvider
    {
        void PushMessage(IQueueMessage message);
    }
}