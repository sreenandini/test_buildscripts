using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Messaging;
using BMC.CoreLib.Concurrent;

namespace BMC.CoreLib.MSMQ
{
    internal delegate bool ProcessMessageInternalHandler(string queuePath, object message);

    internal class MessageQueueProcessorInternal
        : MessageQueueProcessor
    {
        internal MessageQueueProcessorInternal(IExecutorService executorService, string servicePath,
            bool isTransactional, IMessageFormatter formatter, int queueTimeout, 
            IDictionary<string, IMessageFormatter> messageFormatters)
            : base(executorService, servicePath, isTransactional, formatter, queueTimeout, messageFormatters) { }

        public override void ListenAsync()
        {
            _uniqueKey = "MsmqMessageQueueProcessor_" + this.ServicePath;
            this.StartAsync(_uniqueKey);
        }

        internal event ProcessMessageInternalHandler ProcessMessageInternal = null;

        public override bool ProcessMessage(object message)
        {
            if (this.ProcessMessageInternal != null)
            {
                return this.ProcessMessageInternal(this.ServicePath, message);
            }
            return true;
        }
    }
}
