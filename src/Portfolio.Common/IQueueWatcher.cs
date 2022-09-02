using System;
using Portfolio.Common.Exceptions;

namespace Portfolio.Common
{
    public interface IQueueWatcher<TQueueMessage>
    {
        event QueueMessageHandler Subscribe;

        event EventHandler<ExtThreadExceptionEventArgs> Error;
    }
}