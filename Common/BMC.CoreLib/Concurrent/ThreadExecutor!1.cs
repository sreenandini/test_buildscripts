using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BMC.CoreLib.Diagnostics;
using System.Threading;
using BMC.CoreLib;
using BMC.CoreLib.Collections;

namespace BMC.CoreLib.Concurrent
{
    public delegate void ExecutorProcessItemHandler<T>(T item)
        where T : IExecutorKey;

    internal delegate void ExecutorProcessItemCompletedHandler<T>(ThreadExecutor<T> source, ThreadExecutorItem<T> item)
        where T : IExecutorKey;

    internal class ThreadExecutorItem<T>
        : DisposableObject, IExecutorKey
        where T : IExecutorKey
    {
        internal ThreadExecutorItem(T item) { this.Item = item; }

        public T Item { get; private set; }

        public EventWaitHandle WaitHandle { get; set; }

        #region IExecutorKey Members

        public string UniqueKey
        {
            get { return this.Item.UniqueKey; }
        }

        #endregion
    }

    internal abstract class ThreadExecutorBase<T>
        : ExecutorBase<ThreadExecutorItem<T>>
        where T : IExecutorKey
    {
        protected IDictionary<string, int> _itemCounts = null;
        protected int _workerThreadId = -1;
        protected int _itemCount = 0;
        protected bool _checkItemCount = false;
        protected Thread _thWorker = null;
        protected object _itemCountLock = new object();
        protected Task _tskWorker = null;

        internal ThreadExecutorBase(IExecutorService executorService)
            : base(executorService)
        {
            _itemCounts = new SortedDictionary<string, int>(StringComparer.InvariantCultureIgnoreCase);
        }

        public int WorkerThreadId
        {
            get { return _workerThreadId; }
        }

        public int QueueCapacity { get; set; }

        public int ContainerIndex { get; set; }

        public int ItemCount
        {
            get { return _itemCount; }
        }

        internal virtual int this[string key]
        {
            get
            {
                ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "getItem");
                int result = default(int);

                try
                {
                    if (_itemCounts.ContainsKey(key))
                    {
                        result = _itemCounts[key];
                    }
                }
                catch (Exception ex)
                {
                    Log.Exception(PROC, ex);
                }

                return result;
            }
            set
            {
                ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "setItem");

                try
                {
                    if (!_itemCounts.ContainsKey(key))
                    {
                        _itemCounts.Add(key, 0);
                    }

                    _itemCounts[key] += value;
                }
                catch (Exception ex)
                {
                    Log.Exception(PROC, ex);
                }
            }
        }

        public event ExecutorProcessItemHandler<T> ProcessItem = null;

        protected virtual void OnProcessItem(T item)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "OnProcessItem"))
            {
                try
                {
                    if (this.ProcessItem != null)
                    {
                        this.ProcessItem(item);
                    }
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }
            }
        }

        public bool IsQueueFull
        {
            get
            {
                return (_itemCount == this.QueueCapacity);
            }
        }

        public bool HasItems
        {
            get
            {
                return (_itemCount > 0);
            }
        }

        public override string ToString()
        {
            return _uniqueKey;
        }
    }
    internal class ThreadExecutor<T>
        : ExecutorBase<ThreadExecutorItem<T>>,
        IComparable<ThreadExecutor<T>>
        where T : IExecutorKey
    {
        private const string KEY_PREFIX = "ThreadExecutor_";

        private InitializeStatus _doWorkInitialized = InitializeStatus.Uninitialized;
        private object _lockObject = new object();

        protected IThreadSafeQueue<ThreadExecutorItem<T>> _queue = null;
        private IDictionary<string, int> _itemCounts = null;
        private Thread _thWorker = null;

        private int _itemCount = 0;
        private bool _checkItemCount = false;
        private EventWaitHandle _mreQueueSignal = null;
        private int _workerThreadId = -1;

        internal ThreadExecutor(IExecutorService executorService, IThreadSafeQueue<ThreadExecutorItem<T>> queue)
            : this(executorService, queue, false, string.Empty) { }

        internal ThreadExecutor(IExecutorService executorService, IThreadSafeQueue<ThreadExecutorItem<T>> queue, bool checkItemCount, string threadSuffix)
            : base(executorService)
        {
            _queue = queue;
            _checkItemCount = checkItemCount;
            this.QueueCapacity = queue.Capacity;
            this.Initialize(threadSuffix);
        }

        public int QueueCapacity { get; set; }

        public int WorkerThreadId
        {
            get { return _workerThreadId; }
        }

        private void Initialize(string threadSuffix)
        {
            ModuleProc PROC = new ModuleProc(DYN_MODULE_NAME, "Initialize");
            string suffix = (threadSuffix.IsEmpty() ? "" : (threadSuffix + "_"));

            try
            {
                _uniqueKey = KEY_PREFIX + Guid.NewGuid().ToString();
                this.ExecutorService.AddExecutor(this);
                this.ContainerIndex = -1;
                _mreQueueSignal = new ManualResetEvent(false);
                _itemCounts = new SortedDictionary<string, int>(StringComparer.InvariantCultureIgnoreCase);

                Extensions.InitializeThreadFunc(ref _thWorker, ref _doWorkInitialized, _lockObject,
                    new System.Threading.ThreadStart(this.DoWork), KEY_PREFIX + suffix);
                Log.InfoV(PROC, "ThreadExecutor : {0} started the thread : {1:D}", _uniqueKey, _thWorker.ManagedThreadId);
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        internal int ContainerIndex { get; set; }

        internal EventWaitHandle QueueSignal
        {
            get { return _mreQueueSignal; }
        }

        private void DoWork()
        {
            ModuleProc PROC = new ModuleProc(DYN_MODULE_NAME, "DoWork");
            _doWorkInitialized = InitializeStatus.Completed;
            int threadId = Thread.CurrentThread.ManagedThreadId;
            _workerThreadId = threadId;

            try
            {
                while (!this.ExecutorService.WaitForShutdown())
                {
                    T item = default(T);

                    try
                    {
                        ThreadExecutorItem<T> executorItem = _queue.Dequeue();
                        if (this.ExecutorService.IsShutdown)
                        {
                            Log.Info(PROC, string.Format("Shutdown called. Thread : {0:D} is exiting now.", threadId));
                            break;
                        }
                        if (executorItem == null)
                        {
                            Log.Info(PROC, "Invalid executor item received.");
                            continue;
                        }

                        // actual item
                        Log.DescriptionV(PROC, "Thread : {0:D}, Queue Size : {1:D}", threadId, _queue.QueueCount);
                        item = executorItem.Item;

                        // if any preceeding item there, then wait for it to finish
                        if (executorItem.WaitHandle != null)
                        {
                            // Waiting for previous item to finish
                            WaitHandle wh = executorItem.WaitHandle;
                            Log.Info(PROC, "Waiting for previous item to finish for : " + item.UniqueKey);

                            // if shutdown called?
                            if (this.ExecutorService.WaitForShutdown(wh))
                            {
                                Log.Info(PROC, string.Format("Shutdown called. Thread : {0:D} is exiting now.", threadId));
                                break;
                            }

                            // get the signal from the previous item
                            Log.Info(PROC, "Signal got from previous item for : " + item.UniqueKey);
                        }

                        // still have item
                        if (this.ExecutorService.IsShutdown)
                        {
                            Log.Info(PROC, string.Format("Shutdown called. Thread : {0:D} is exiting now.", threadId));
                            break;
                        }

                        // actual processing and processing completion
                        if (!item.Equals(default(T)))
                        {
                            this.OnProcessItem(item);
                            this.OnProcessItemCompleted(executorItem);
                            if (_checkItemCount && (_itemCount > 0))
                            {
                                Interlocked.Decrement(ref _itemCount);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Log.Exception(PROC, ex);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
            finally
            {
                Log.Info(PROC, string.Format("Shutdown called. Thread : {0:D} has finished his work.", Thread.CurrentThread.ManagedThreadId));
                this.Shutdown();
            }
        }

        public event ExecutorProcessItemHandler<T> ProcessItem = null;

        private void OnProcessItem(T item)
        {
            ModuleProc PROC = new ModuleProc(DYN_MODULE_NAME, "OnProcessItem");

            try
            {
                if (this.ProcessItem != null)
                {
                    this.ProcessItem(item);
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        public event ExecutorProcessItemCompletedHandler<T> ProcessItemCompleted = null;

        private void OnProcessItemCompleted(ThreadExecutorItem<T> item)
        {
            ModuleProc PROC = new ModuleProc(DYN_MODULE_NAME, "OnProcessItemCompleted");

            try
            {
                if (this.ProcessItemCompleted != null)
                {
                    this.ProcessItemCompleted(this, item);
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        public bool IsQueueFull
        {
            get
            {
                return (_itemCount == this.QueueCapacity);
            }
        }

        public bool HasItems
        {
            get
            {
                return (_itemCount > 0);
            }
        }

        internal int this[string key]
        {
            get
            {
                ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "getItem");
                int result = default(int);

                try
                {
                    if (_itemCounts.ContainsKey(key))
                    {
                        result = _itemCounts[key];
                    }
                }
                catch (Exception ex)
                {
                    Log.Exception(PROC, ex);
                }

                return result;
            }
            set
            {
                ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "setItem");

                try
                {
                    if (!_itemCounts.ContainsKey(key))
                    {
                        _itemCounts.Add(key, 0);
                    }

                    _itemCounts[key] += value;
                }
                catch (Exception ex)
                {
                    Log.Exception(PROC, ex);
                }
            }
        }

        public override string ToString()
        {
            return _uniqueKey;
        }

        #region IExecutor<ThereadExecutorItem<T>> Members

        public override void QueueWorkerItem(ThreadExecutorItem<T> item)
        {
            if (this.ExecutorService.IsShutdown) return;

            if (item != null)
            {
                _queue.Enqueue(item);
                if (_checkItemCount)
                {
                    Interlocked.Increment(ref _itemCount);
                }
            }
        }

        #endregion

        #region IComparable<ThreadExecutor<T>> Members

        public int CompareTo(ThreadExecutor<T> other)
        {
            if (other == null) return 1;
            return (string.Compare(_uniqueKey, other._uniqueKey, true));
        }

        #endregion
    }
}
