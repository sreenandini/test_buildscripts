using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Messaging;

namespace BMC.CoreLib.MSMQ
{
    public class QueueMessageInfo<T> : DisposableObject
    {
        public QueueMessageInfo(T[] )
        {
        }

        public IMessageFormatter DefaultFormatter { get; set;}
}
