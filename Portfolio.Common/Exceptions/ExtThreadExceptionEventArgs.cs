using System;
using System.Threading;

namespace Portfolio.Common.Exceptions
{
    public class ExtThreadExceptionEventArgs : ThreadExceptionEventArgs
    {
        public ExtThreadExceptionEventArgs(IQueueMessage message, Exception exp)
            : base(exp)
        {
            this.Message = message;
        }

        public IQueueMessage Message { get; private set; }
    }
}