using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using BMC.CoreLib.Diagnostics;
using BMC.CoreLib;
using System.Messaging;
using System.Diagnostics;
using BMC.CoreLib.Concurrent;

namespace BMC.CoreLib.MSMQ
{
    public delegate bool ProcessQueueMessageHandler(QueueMessageWrapper message);

    public class MessageQueueInfo : DisposableObject
    {
        public MessageQueueInfo(string servicePath, bool isTransactional, IMessageFormatter formatter, int queueTimeout)
            : this(servicePath, isTransactional, formatter, queueTimeout, null) { }

        public MessageQueueInfo(string servicePath, bool isTransactional,
            IMessageFormatter formatter, int queueTimeout,
            KeyValuePair<string, IMessageFormatter>[] messageFormatters)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, ".ctor");

            try
            {
                this.ServicePath = servicePath;
                this.IsTransactional = isTransactional;
                this.Formatter = formatter;
                this.QueueTimeout = queueTimeout;
                if (messageFormatters != null)
                {
                    this.MessageFormatters = new SortedDictionary<string, IMessageFormatter>(StringComparer.InvariantCultureIgnoreCase);
                    foreach (KeyValuePair<string, IMessageFormatter> messageFormatter in messageFormatters)
                    {
                        this.MessageFormatters.Add(messageFormatter);
                    }
                }

                Log.InfoV(PROC, "Service Path : {0}, Is Transactional : {1}", servicePath, isTransactional);
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        public string ServicePath { get; private set; }
        public bool IsTransactional { get; private set; }
        public IMessageFormatter Formatter { get; private set; }
        public IDictionary<string, IMessageFormatter> MessageFormatters { get; private set; }
        public int QueueTimeout { get; private set; }
    }

    public class MessageQueueWorkerInfo : DisposableObject
    {
        public MessageQueueWorkerInfo(int messageThreshold)
            : this(messageThreshold, messageThreshold, false, null) { }

        public MessageQueueWorkerInfo(int messageThreshold, int queueThreshold, bool flushItemsBeforeClose, MessageQueueInfo[] queueInfos)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, ".ctor");

            try
            {
                this.MessageThreshold = messageThreshold;
                this.QueueThreshold = queueThreshold;
                this.FlushItemsBeforeClose = flushItemsBeforeClose;
                if (queueInfos == null)
                {
                    this.QueueInfos = new List<MessageQueueInfo>();
                }
                else
                {
                    this.QueueInfos = new List<MessageQueueInfo>(queueInfos);
                }

                Log.InfoV(PROC, "Message Threshold : {0:D}", messageThreshold);
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        public int MessageThreshold { get; private set; }
        public int QueueThreshold { get; private set; }
        public bool FlushItemsBeforeClose { get; private set; }
        public IList<MessageQueueInfo> QueueInfos { get; private set; }

        public bool TrackItems { get; set; }
        public MessageQueueWorkerInfo DependencyWorker { get; set; }
        public bool MQUseWorkerThread { get; set; }
        public bool MQCheckMessageResult { get; set; }

        internal string WorkerKey { get; set; }
    }

    public class MessageQueueManager : ExecutorBase
    {
        private const string KEY_PREFIX = "MessageQueueManager_";
        private ThreadPoolType _poolType = ThreadPoolType.FreeThreads;

        private IDictionary<string, MessageQueueWorker> _queueWorkers = null;

        public MessageQueueManager(IExecutorService executorService, ThreadPoolType poolType,
            MessageQueueWorkerInfo[] workerInfos,
            KeyValuePair<string, CreateQueueMessageWrapperHandler>[] createMessageWrappers)
            : base(executorService)
        {
            Debug.Assert(workerInfos != null);
            _poolType = poolType;
            this.Initialize(workerInfos, createMessageWrappers);
        }

        private void Initialize(MessageQueueWorkerInfo[] workerInfos,
            KeyValuePair<string, CreateQueueMessageWrapperHandler>[] createMessageWrappers)
        {
            ModuleProc PROC = new ModuleProc(DYN_MODULE_NAME, "Initialize");

            try
            {
                _uniqueKey = KEY_PREFIX + Guid.NewGuid().ToString();
                this.ExecutorService.AddExecutor(this);
                this.RegisterForShutdown();

                _queueWorkers = new SortedDictionary<string, MessageQueueWorker>(StringComparer.InvariantCultureIgnoreCase);
                foreach (MessageQueueWorkerInfo workerInfo in workerInfos)
                {
                    MessageQueueWorker worker = new MessageQueueWorker(this, this.ExecutorService, _poolType, workerInfo, createMessageWrappers);
                    string key = worker.UniqueKey;
                    _queueWorkers.Add(key, worker);

                    if (workerInfo.DependencyWorker != null)
                    {
                        string workerKey = workerInfo.DependencyWorker.WorkerKey;
                        if (_queueWorkers.ContainsKey(workerKey))
                        {
                            worker.DependencyWorker = _queueWorkers[workerKey];
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        public void Start()
        {
            foreach (MessageQueueWorker queueWorker in _queueWorkers.Values)
            {
                try
                {
                    queueWorker.Start();
                }
                catch { }
            }
        }

        public event ProcessQueueMessageHandler ProcessMessage = null;

        internal bool OnProcessMessage(QueueMessageWrapper message)
        {
            ModuleProc PROC = new ModuleProc(DYN_MODULE_NAME, "OnProcessMessage");

            try
            {
                if (this.ProcessMessage != null)
                {
                    return this.ProcessMessage(message);
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
            return true;
        }

        protected override void OnShutDownCalled()
        {
            base.OnShutDownCalled();
            this.Shutdown();
        }
    }
}
