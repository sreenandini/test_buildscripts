using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.CoreLib.Diagnostics;
using System.Diagnostics;
using BMC.CoreLib.Collections;
using System.Threading;

namespace BMC.CoreLib.Concurrent
{
    internal abstract class AsyncTaskThreadPoolExecutorBase<T>
       : ThreadPoolExecutorBase<T, AsyncTaskThreadExecutor<T>>
       where T : IExecutorKey
    {
        internal AsyncTaskThreadPoolExecutorBase(IExecutorService executorService, int queueCapacity, bool kernelModeQueue, bool flushItemsBeforeClose)
            : base(executorService, queueCapacity, kernelModeQueue, flushItemsBeforeClose) { }
    }

	internal abstract class ThreadPoolExecutorBase<T, E>
       : ExecutorBase<T>, IThreadPoolExecutor<T>
        where T : IExecutorKey
        where E : ThreadExecutorBase<T>
    {
        protected class ThreadHashValue : DisposableObject
        {
            public E Executor { get; set; }
            public int ItemCount { get; set; }

            public override string ToString()
            {
                return "Count : " + ItemCount.ToString();
            }
        }

        protected object _lockList = new object();
        protected IDictionary<string, ThreadHashValue> _activeWorkersCount = null;
        protected IDictionary<string, E> _itemWorkerThreads = null;
        protected IList<E> _workerThreads = null;

        protected object _lockFullEvent = new object();
        protected int _fullWaiters = 0;
        protected int _activeWorkers = 0;
        protected bool _kernelModeQueue = false;
        protected Random _rnd = null;

        internal ThreadPoolExecutorBase(IExecutorService executorService, int queueCapacity, bool kernelModeQueue, bool flushItemsBeforeClose)
            : base(executorService)
        {
            _kernelModeQueue = kernelModeQueue;
            this.QueueCapacity = queueCapacity;
            this.FlushItemsBeforeClose = flushItemsBeforeClose;
            this.ThreadExecutorService = ExecutorServiceFactory.CreateExecutorService();

            _activeWorkersCount = new StringConcurrentDictionary<ThreadHashValue>();
            _itemWorkerThreads = new StringConcurrentDictionary<E>();
            _workerThreads = new List<E>();
            _rnd = new Random(0);
        }

        public int Capacity { get; set; }

        public int QueueCapacity { get; set; }

        public bool TrackItems { get; set; }

        public bool FlushItemsBeforeClose { get; protected set; }

        protected IExecutorService ThreadExecutorService { get; set; }

        protected virtual void ActiveWorkerThreadIncrement(string uniqueKey, string itemKey)
        {
            _activeWorkersCount[uniqueKey].ItemCount += 1;
            if (this.TrackItems)
            {
                if (_itemWorkerThreads.ContainsKey(itemKey))
                {
                    _itemWorkerThreads[itemKey][itemKey] = 1;
                }
            }
        }

        protected virtual void ActiveWorkerThreadDecrement(string uniqueKey, string itemKey)
        {
            _activeWorkersCount[uniqueKey].ItemCount -= 1;
            if (this.TrackItems)
            {
                if (_itemWorkerThreads.ContainsKey(itemKey))
                {
                    _itemWorkerThreads[itemKey][itemKey] = -1;
                }
            }
        }

        protected abstract bool IsQueueFull(E executor, T item);

        public override void QueueWorkerItem(T item)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "QueueWorkerItem"))
            {
                E executor = null;
                if (this.ExecutorService.IsShutdown) return;
                string itemKey = string.Empty;

                lock (_lockList)
                {
                    try
                    {
                        if (!item.Equals(default(T)))
                        {
                            // find the executor
                            itemKey = item.UniqueKey;
                            if (_itemWorkerThreads.ContainsKey(itemKey))
                            {
                                executor = _itemWorkerThreads[itemKey];
                            }
                            else
                            {
                                executor = this.GetFreeThreadExecutor(item);
                                method.InfoV("Found Thread Index : {0:D} for item key : {1}", executor.ContainerIndex, itemKey);
                                _itemWorkerThreads.Add(itemKey, executor);
                            }
                        }

                        if (executor != null)
                        {
                            // if full, wait until some items consumed
                            while (this.IsQueueFull(executor, item))
                            {
                                Log.Description(method.PROC, "Queue is full. Thread pool is blocked for : " + itemKey);
                                if (this.ExecutorService.IsShutdown) break;
                                _fullWaiters++;

                                try
                                {
                                    lock (_lockFullEvent)
                                    {
                                        Monitor.Exit(_lockList);
                                        Log.Debug(method.PROC, "Locked : " + _uniqueKey);
                                        Monitor.Wait(_lockFullEvent);
                                        Log.Debug(method.PROC, "Unlocked : " + _uniqueKey);
                                        Monitor.Enter(_lockList);
                                    }
                                }
                                finally
                                {
                                    _fullWaiters--;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        method.Exception(ex);
                    }
                    finally
                    {
                        if (!this.ExecutorService.IsShutdown)
                        {
                            this.DoWorkItem(executor, item);
                        }
                    }
                }
            }
        }

        protected virtual void DoWorkItem(E executor, T item)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "DoWorkItem"))
            {
                try
                {
                    if (!item.Equals(default(T)) &&
                        executor != null)
                    {
                        try
                        {
                            Interlocked.Increment(ref _activeWorkers);
                            this.ActiveWorkerThreadIncrement(executor.UniqueKey, item.UniqueKey);
                            ThreadExecutorItem<T> executorItem = new ThreadExecutorItem<T>(item);
                            executor.QueueWorkerItem(executorItem);
                        }
                        catch (Exception ex)
                        {
                            method.Exception(ex);
                        }
                    }
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }
            }
        }

        public event ExecutorProcessItemHandler<T> ProcessItem = null;

        protected void OnProcessItem(T item)
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

        protected virtual void OnExecutor_ProcessItem(T item)
        {
            this.OnProcessItem(item);
        }

        protected virtual void OnExecutor_ProcessItemCompleted(E source, ThreadExecutorItem<T> item)
        {
            ModuleProc PROC = new ModuleProc(DYN_MODULE_NAME, "OnExecutor_ProcessItemCompleted");

            try
            {
                lock (_lockList)
                {
                    try
                    {
                        if (_activeWorkers > 0) Interlocked.Decrement(ref _activeWorkers);
                        this.ActiveWorkerThreadDecrement(source.UniqueKey, item.UniqueKey);
                    }
                    catch (Exception ex)
                    {
                        Log.Exception(PROC, ex);
                    }
                }

                // wake the producers who are waiting to produce
                if (_fullWaiters > 0)
                {
                    lock (_lockFullEvent)
                    {
                        Monitor.Pulse(_lockFullEvent);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        protected virtual E GetKeyFreeThreadExecutor()
        {
            return (from i in _workerThreads
                    orderby i.ItemCount
                    select i).FirstOrDefault();
        }

        protected virtual E GetFreeThreadExecutor(T item)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "GetFreeThreadExecutor"))
            {
                E executor = default(E);

                try
                {
                    // executor key free thread approach
                    if (item is IExecutorKeyFreeThread)
                    {
                        // find the thread with minimum number of items
                        executor = this.GetKeyFreeThreadExecutor();
                        if (executor != null)
                        {
                            method.Info("Thread " + executor.ContainerIndex.ToString() + " was taken by IExecutorKeyFreeThread approach.");
                        }
                    }

                    // executor key thread approach
                    if (executor == null &&
                        item is IExecutorKeyThread)
                    {
                        int index = ((IExecutorKeyThread)item).GetThreadIndex(this.Capacity);
                        if (index >= 0 && index < _workerThreads.Count)
                        {
                            executor = _workerThreads[index];
                            method.Info("Thread " + executor.ContainerIndex.ToString() + " was taken by ExecutorKeyTheread approach.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }
                finally
                {
                    // no more free threads
                    if (executor == null)
                    {
                        executor = _workerThreads[_rnd.Next(0, _workerThreads.Count - 1)];
                        method.Info("Thread " + executor.ContainerIndex.ToString() + " was taken as random.");
                    }
                }

                return executor;
            }
        }

        protected override void OnShutDownCalled()
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "OnShutDownCalled"))
            {
                try
                {
                    base.OnShutDownCalled();

                    //Debugger.Break();
                    if (this.TrackItems) { this.LogTrackItems(":BEFORE:"); }
                    if (this.FlushItemsBeforeClose)
                    {
                        // wait untill all the items finished
                        this.WaitForItemsToFlush();
                    }
                    if (this.TrackItems) { this.LogTrackItems(":AFTER:"); }
                    this.ThreadExecutorService.AwaitTermination(TimeSpan.Zero);

                    method.InfoV("Shutdown called. {0} has finished his work.", _uniqueKey);
                    this.Shutdown();
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }
            }
        }

        protected virtual void LogTrackItems(string prefix)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "LogTrackItems"))
            {
                try
                {
                    foreach (KeyValuePair<string, E> pair in _itemWorkerThreads)
                    {
                        string key = pair.Key;
                        E value = pair.Value;
                        method.InfoV("%%% ({3} => ) Thread : {0:D}, Item Key : {1}, Item Count : {2:D}", value.WorkerThreadId, key,
                            value[key], prefix);
                    }
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }
            }
        }

        protected virtual int GetActiveItems()
        {
            return (from a in _activeWorkersCount.Values
                    where a.ItemCount > 0
                    select a).Count();
        }

        protected virtual void WaitForItemsToFlush()
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "WaitForItemsToFlush"))
            {
                try
                {
                    int iteration = 0;
                    while (!this.ThreadExecutorService.WaitForShutdown(100))
                    {
                        int count = this.GetActiveItems();
                        if (count > 0)
                        {
                            if (iteration > 50)
                            {
                                method.InfoV("$$$ ({0:D}) thread executors have items to finish. So waiting for them to finish.", count);
                                iteration = 0;
                            }
                            else
                            {
                                iteration++;
                            }
                        }
                        else
                        {
                            method.Info("$$$ All the thread executors have finished their work. So can safely shutdown them now.");
                            break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }
            }
        }
    }
	
    internal abstract class ThreadPoolExecutorBase<T>
       : ExecutorBase<T>, IThreadPoolExecutor<T>
       where T : IExecutorKey
    {
        protected class ThreadHashValue : DisposableObject
        {
            public ThreadExecutor<T> Executor { get; set; }
            public int ItemCount { get; set; }

            public override string ToString()
            {
                return "Count : " + ItemCount.ToString();
            }
        }

        protected object _lockList = new object();
        protected IDictionary<string, ThreadHashValue> _activeWorkersCount = null;
        protected IDictionary<string, ThreadExecutor<T>> _itemWorkerThreads = null;

        protected object _lockFullEvent = new object();
        protected int _fullWaiters = 0;
        protected int _activeWorkers = 0;
        protected bool _kernelModeQueue = false;

        internal ThreadPoolExecutorBase(IExecutorService executorService, int queueCapacity, bool kernelModeQueue, bool flushItemsBeforeClose)
            : base(executorService)
        {
            _kernelModeQueue = kernelModeQueue;
            this.QueueCapacity = queueCapacity;
            this.FlushItemsBeforeClose = flushItemsBeforeClose;
            this.ThreadExecutorService = ExecutorServiceFactory.CreateExecutorService();
            _itemWorkerThreads = new SortedDictionary<string, ThreadExecutor<T>>(StringComparer.InvariantCultureIgnoreCase);
        }

        public int QueueCapacity { get; set; }

        public bool TrackItems { get; set; }

        public bool FlushItemsBeforeClose { get; private set; }

        protected IExecutorService ThreadExecutorService { get; set; }

        protected virtual void ActiveWorkerThreadIncrement(string uniqueKey, string itemKey)
        {
            _activeWorkersCount[uniqueKey].ItemCount += 1;
            if (this.TrackItems)
            {
                if (_itemWorkerThreads.ContainsKey(itemKey))
                {
                    _itemWorkerThreads[itemKey][itemKey] = 1;
                }
            }
        }

        protected virtual void ActiveWorkerThreadDecrement(string uniqueKey, string itemKey)
        {
            _activeWorkersCount[uniqueKey].ItemCount -= 1;
            if (this.TrackItems)
            {
                if (_itemWorkerThreads.ContainsKey(itemKey))
                {
                    _itemWorkerThreads[itemKey][itemKey] = -1;
                }
            }
        }

        public event ExecutorProcessItemHandler<T> ProcessItem = null;

        protected void OnProcessItem(T item)
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

        protected override void OnShutDownCalled()
        {
            ModuleProc PROC = new ModuleProc(DYN_MODULE_NAME, "OnShutDownCalled");
            base.OnShutDownCalled();

            //Debugger.Break();
            if (this.TrackItems) { this.LogTrackItems(":BEFORE:"); }
            if (this.FlushItemsBeforeClose)
            {
                // wait untill all the items finished
                this.WaitForItemsToFlush();
            }
            if (this.TrackItems) { this.LogTrackItems(":AFTER:"); }
            this.ThreadExecutorService.AwaitTermination(TimeSpan.Zero);

            Log.Info(PROC, "Shutdown called. QueableThreadsThreadPoolExecutor has finished his work.");
            this.Shutdown();
        }

        protected virtual void LogTrackItems(string prefix)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "LogTrackItems");

            try
            {
                foreach (KeyValuePair<string, ThreadExecutor<T>> pair in _itemWorkerThreads)
                {
                    string key = pair.Key;
                    ThreadExecutor<T> value = pair.Value;
                    Log.InfoV(PROC, "%%% ({3} => ) Thread : {0:D}, Item Key : {1}, Item Count : {2:D}", value.WorkerThreadId, key,
                        value[key], prefix);
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        protected virtual void WaitForItemsToFlush()
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "WaitForItemsToFlush");

            try
            {
                int iteration = 0;
                while (!this.ThreadExecutorService.WaitForShutdown(100))
                {
                    int count = (from a in _activeWorkersCount.Values
                                 where a.ItemCount > 0
                                 select a).Count();
                    if (count > 0)
                    {
                        if (iteration > 50)
                        {
                            Log.InfoV(PROC, "$$$ ({0:D}) thread executors have items to finish. So waiting for them to finish.", count);
                            iteration = 0;
                        }
                        else
                        {
                            iteration++;
                        }
                    }
                    else
                    {
                        Log.Info(PROC, "$$$ All the thread executors have finished their work. So can safely shutdown them now.");
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }
    }
}
