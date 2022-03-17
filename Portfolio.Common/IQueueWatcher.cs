using System;
using Portfolio.Common.Exceptions;
using Portfolio.Domain.Events;

namespace Portfolio.Common
{
    public interface IQueueWatcher<TQueueMessage>
    {
        event QueueMessageHandler Subscribe;

        event EventHandler<ExtThreadExceptionEventArgs> Error;
    }
}