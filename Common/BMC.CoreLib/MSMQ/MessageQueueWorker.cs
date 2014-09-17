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
using BMC.CoreLib.Collections;

namespace BMC.CoreLib.MSMQ
{
    public abstract class QueueMessageWrapper : DisposableObject, IExecutorKey
    {
        private object _message = null;

        protected QueueMessageWrapper(object message)
        {
            _message = message;
        }

        public object Message
        {
            get { return _message; }
        }

        public bool DisposeMessage { get; set; }

        internal bool MessageResult { get; set; }

        protected override void DisposeManaged()
        {
            base.DisposeManaged();
            if (this.Message is IDisposable)
            {
                ((IDisposable)this.Message).Dispose();
            }
        }

        #region IExecutorKey Members

        public abstract string UniqueKey { get; }

        #endregion
    }

    public class QueueMessageWrapper<T> : QueueMessageWrapper, IExecutorKeyThread
        where T : IExecutorKey
    {
        private T _actualMessage = default(T);

        public QueueMessageWrapper(T message)
            : base(message)
        {
            _actualMessage = message;
        }

        public T ActualMessage
        {
            get { return _actualMessage; }
        }

        public override string UniqueKey
        {
            get { return _actualMessage.UniqueKey; }
        }

        #region IExecutorKeyThread Members

        public int GetThreadIndex(int capacity)
        {
            if (_actualMessage != null &&
                _actualMessage is IExecutorKeyThread)
            {
                return ((IExecutorKeyThread)_actualMessage).GetThreadIndex(capacity);
            }
            return -1;
        }

        #endregion
    }

    public delegate QueueMessageWrapper CreateQueueMessageWrapperHandler(object message);

    internal class MessageQueueWorker : ExecutorBase<QueueMessageWrapper>
    {
        private class WorkerInfo : DisposableObject
        {
            public IMessageQueueProcessor Processor { get; set; }
            public IThreadSafeQueue<QueueMessageWrapper> Queue { get; set; }
            public MessageQueueInfo QueueInfo { get; set; }
        }

        private const string KEY_PREFIX = "MessageQueueWorker_";

        private InitializeStatus _listenMonitorQueues = InitializeStatus.Uninitialized;
        private object _lockObject = new object();

        private MessageQueueManager _parent = null;
        private IDictionary<string, WorkerInfo> _workerInfos = null;
        private IList<IThreadSafeQueue<QueueMessageWrapper>> _queues = null;
        private IDictionary<string, CreateQueueMessageWrapperHandler> _createMessageWrappers = null;
        private IThreadPoolExecutor<QueueMessageWrapper> _poolExecutor = null;
        private ThreadPoolType _poolType = ThreadPoolType.FreeThreads;

        private IMessageQueueProcessor _defaultQueueProcessor = null;
        private MessageQueueWorker _dependencyWorker = null;
        private List<MessageQueueWorker> _dependencyWorkers = null;
        private object _dependencyWorkerLock = new object();
        private bool _hasDependency = false;
        private bool _sharedQueues = false;

        internal MessageQueueWorker(MessageQueueManager parent, IExecutorService executorService,
            ThreadPoolType poolType, MessageQueueWorkerInfo workerInfo,
            KeyValuePair<string, CreateQueueMessageWrapperHandler>[] createMessageWrappers)
            : base(executorService)
        {
            Debug.Assert(workerInfo.QueueInfos.Count != 0);
            _parent = parent;
            _poolType = poolType;
            this.MessageThreshold = workerInfo.MessageThreshold;
            this.QueueThreshold = workerInfo.QueueThreshold;
            this.MQUseWorkerThread = workerInfo.MQUseWorkerThread;
            this.MQCheckMessageResult = workerInfo.MQCheckMessageResult;
            this.Initialize(workerInfo, createMessageWrappers);
        }

        public int MessageThreshold { get; private set; }

        public int QueueThreshold { get; set; }

        public bool MQUseWorkerThread { get; set; }

        public bool MQUseExecutor { get; set; }

        public bool MQCheckMessageResult { get; set; }

        internal MessageQueueWorker DependencyWorker
        {
            get { return _dependencyWorker; }
            set
            {
                _dependencyWorker = value;
                value.AddToDependencyWorker(this);
            }
        }

        private void AddToDependencyWorker(MessageQueueWorker thisWorker)
        {
            if (_dependencyWorkers == null)
            {
                lock (_dependencyWorkerLock)
                {
                    if (_dependencyWorkers == null)
                    {
                        _dependencyWorkers = new List<MessageQueueWorker>();
                    }
                }
            }
            _hasDependency |= true;
            _dependencyWorkers.Add(thisWorker);
        }

        private void Initialize(MessageQueueWorkerInfo workerInfo,
            KeyValuePair<string, CreateQueueMessageWrapperHandler>[] createMessageWrappers)
        {
            ModuleProc PROC = new ModuleProc(DYN_MODULE_NAME, "Initialize");

            try
            {
                _uniqueKey = KEY_PREFIX + Guid.NewGuid().ToString();
                this.ExecutorService.AddExecutor(this);

                _workerInfos = new SortedDictionary<string, WorkerInfo>(StringComparer.InvariantCultureIgnoreCase);
                _createMessageWrappers = new SortedDictionary<string, CreateQueueMessageWrapperHandler>(StringComparer.InvariantCultureIgnoreCase);
                _queues = new List<IThreadSafeQueue<QueueMessageWrapper>>();

                if (createMessageWrappers != null)
                {
                    foreach (KeyValuePair<string, CreateQueueMessageWrapperHandler> createWrapper in createMessageWrappers)
                    {
                        if (!_createMessageWrappers.ContainsKey(createWrapper.Key))
                        {
                            _createMessageWrappers.Add(createWrapper);
                            Log.InfoV(PROC, "Message Wrapper Handler for : {0}", createWrapper.Key);
                        }
                    }
                }

                if (workerInfo.QueueInfos != null)
                {
                    foreach (MessageQueueInfo queueInfo in workerInfo.QueueInfos)
                    {
                        if (!_workerInfos.ContainsKey(queueInfo.ServicePath))
                        {
                            WorkerInfo wi = new WorkerInfo()
                            {
                                QueueInfo = queueInfo,
                                Queue = new BlockingBoundQueueUser<QueueMessageWrapper>(this.ExecutorService, 1)
                            };

                            try
                            {
                                MessageQueueProcessorInternal processor = new MessageQueueProcessorInternal(this.ExecutorService, queueInfo.ServicePath,
                                        queueInfo.IsTransactional, queueInfo.Formatter, queueInfo.QueueTimeout, queueInfo.MessageFormatters);
                                processor.ProcessMessageInternal += new ProcessMessageInternalHandler(OnMessageProcessor_ProcessMessage);
                                wi.Processor = processor;
                                _defaultQueueProcessor = processor;
                            }
                            catch (Exception ex)
                            {
                                Log.Exception(PROC, ex);
                            }
                            _workerInfos.Add(queueInfo.ServicePath, wi);
                            _queues.Add(wi.Queue);
                        }
                    }
                }

                // executors
                _sharedQueues = (_queues.Count > 1);
                this.MQUseExecutor = (_sharedQueues || (this.MessageThreshold > 1));
                if (this.MQUseExecutor)
                {
                    _poolExecutor = ThreadPoolExecutorFactory.CreateThreadPool<QueueMessageWrapper>(this.ExecutorService, _poolType,
                        this.MessageThreshold, this.QueueThreshold, false, workerInfo.FlushItemsBeforeClose);
                    _poolExecutor.TrackItems = workerInfo.TrackItems;
                    _poolExecutor.ProcessItem += new ExecutorProcessItemHandler<QueueMessageWrapper>(OnThreadPoolExecutor_ProcessItem);
                }
                workerInfo.WorkerKey = _uniqueKey;
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
            finally
            {
                if (!this.MQUseWorkerThread)
                {
                    this.RegisterForShutdown();
                }
            }
        }

        public void Start()
        {
            Thread th1 = null;
            Extensions.InitializeThreadFunc(ref th1, ref _listenMonitorQueues, _lockObject,
                    new System.Threading.ThreadStart(this.ListenMonitorQueues), KEY_PREFIX);
        }

        private void ListenMonitorQueues()
        {
            ModuleProc PROC = new ModuleProc(DYN_MODULE_NAME, "ListenMonitorQueues");
            bool canShutdown = true;

            // Start the queues
            try
            {
                foreach (WorkerInfo workerInfo in _workerInfos.Values)
                {
                    workerInfo.Processor.ListenAsync();
                    this.ExecutorService.WaitForShutdown();
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            // listen for queues
            try
            {
                int length = _queues.Count;

                // more queues found
                if (length > 1)
                {
                    while (!this.ExecutorService.WaitForShutdown())
                    {
                        for (int i = 0; i < length; i++)
                        {
                            IThreadSafeQueue<QueueMessageWrapper> queue = _queues[i];
                            bool hasDataInPriorityQueue = false;

                            while (queue.QueueCount > 0)
                            {
                                for (int j = 0; j < i; j++)
                                {
                                    // if any items available in the the previous queue
                                    if (_queues[j].QueueCount > 0)
                                    {
                                        hasDataInPriorityQueue = true;
                                        break;
                                    }
                                    if (this.ExecutorService.WaitForShutdown()) break;
                                }

                                if (hasDataInPriorityQueue ||
                                    this.ExecutorService.IsShutdown) break;

                                try
                                {
                                    _poolExecutor.QueueWorkerItem(queue.Dequeue());
                                }
                                catch (Exception ex)
                                {
                                    Log.Exception(PROC, ex);
                                }

                                if (hasDataInPriorityQueue ||
                                    this.ExecutorService.WaitForShutdown()) break;
                            }

                            if (hasDataInPriorityQueue ||
                                    this.ExecutorService.WaitForShutdown()) break;
                        }
                    }
                }
                else // single queue is enough
                {
                    if (!this.MQUseWorkerThread)
                    {
                        canShutdown = false;
                    }
                    else
                    {
                        IThreadSafeQueue<QueueMessageWrapper> queue = _queues[0];
                        while (!this.ExecutorService.WaitForShutdown())
                        {
                            try
                            {
                                _poolExecutor.QueueWorkerItem(queue.Dequeue());
                            }
                            catch (Exception ex)
                            {
                                Log.Exception(PROC, ex);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
            finally
            {
                if (canShutdown)
                {
                    this.Shutdown();
                }
            }
        }

        private bool OnMessageProcessor_ProcessMessage(string queuePath, object message)
        {
            ModuleProc PROC = new ModuleProc(DYN_MODULE_NAME, "OnMessageProcessor_ProcessMessage");

            string typeName = message.GetType().FullName;
            if (_createMessageWrappers.ContainsKey(typeName))
            {
                QueueMessageWrapper wrapper = _createMessageWrappers[typeName](message);
                if (_sharedQueues)
                {
                    _workerInfos[queuePath].Queue.Enqueue(wrapper);
                }
                else
                {
                    if (!this.MQUseWorkerThread)
                    {
                        if (this.MQUseExecutor)
                        {
                            _poolExecutor.QueueWorkerItem(wrapper);
                        }
                        else
                        {
                            this.OnThreadPoolExecutor_ProcessItem(wrapper);
                            if (this.MQCheckMessageResult)
                            {
                                return wrapper.MessageResult;
                            }
                        }
                    }
                    else
                    {
                        _workerInfos[queuePath].Queue.Enqueue(wrapper);
                    }
                }
                return true;
            }
            else
            {
                Log.InfoV(PROC, "Unable to find the object wrapper for the message type : ({0}).", typeName);
                return false;
            }
        }

        void OnThreadPoolExecutor_ProcessItem(QueueMessageWrapper item)
        {
            // don't process the message if the worker has dependency
            if (_hasDependency)
            {
                IExecutorKeyThread executorKey = item as IExecutorKeyThread;
                if (executorKey != null)
                {
                    int index = executorKey.GetThreadIndex(_dependencyWorkers.Count);
                    if (index >= 0 && index < _dependencyWorkers.Count)
                    {
                        // just post the message to corresponding message queue is enough
                        item.MessageResult = _dependencyWorkers[index]._defaultQueueProcessor.PostMessage(item.Message);
                    }
                }
                else
                {
                    item.MessageResult = _parent.OnProcessMessage(item);
                }
            }
            else
            {
                item.MessageResult = _parent.OnProcessMessage(item);
            }

            // dispose the message if no longer needed.
            if (item.DisposeMessage)
            {
                item.Dispose();
            }
        }

        public override void QueueWorkerItem(QueueMessageWrapper item)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return this.UniqueKey;
        }

        protected override void OnShutDownCalled()
        {
            base.OnShutDownCalled();
            if (!this.MQUseWorkerThread)
            {
                this.Shutdown();
            }
        }
    }
}
