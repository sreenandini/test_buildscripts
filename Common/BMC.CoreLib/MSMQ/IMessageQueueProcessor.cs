using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Messaging;
using BMC.CoreLib.Concurrent;

namespace BMC.CoreLib.MSMQ
{
    public interface IMessageQueueProcessor
        : IProcessMessage, IExecutor
    {
        string ServicePath { get; }

        bool IsTransactional { get; }

        IMessageFormatter MessageFormatter { get; }

        void ListenAsync();

        bool PostMessage(object message);

        bool PostMessage(object message, string label);

        object GetMessage();

        bool ProcessMessage(object message);
    }
}
