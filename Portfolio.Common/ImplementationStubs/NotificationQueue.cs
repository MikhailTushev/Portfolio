using System;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using Portfolio.Common.Exceptions;
using Portfolio.Domain.Events;

namespace Portfolio.Common.ImplementationStubs
{
    public class NotificationQueue : IQueueProvider, IQueueWatcher<IQueueMessage>
    {
        private readonly ActionBlock<IQueueMessage> _pipe;

        public NotificationQueue()
        {
            _pipe = new ActionBlock<IQueueMessage>(OnSubscribe);
        }

        public event QueueMessageHandler Subscribe;

        public event EventHandler<ExtThreadExceptionEventArgs> Error;

        protected async Task OnSubscribe(IQueueMessage message)
        {
            try
            {
                await Subscribe?.Invoke(message);
            }
            catch (Exception e)
            {
                Error?.Invoke(this, new ExtThreadExceptionEventArgs(message, e));
            }
        }

        public void Dispose()
        {
            // Method intentionally left empty.
        }

        public void PushMessage(IQueueMessage message)
        {
            _pipe.Post(message);
        }
    }
}