using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.CoreLib;
using BMC.CoreLib.Diagnostics;
using System.Threading;
using System.Diagnostics;
using BMC.CoreLib.Collections;

namespace BMC.CoreLib.Concurrent
{
    internal abstract class QueueThreadPoolExecutor<T>
        : ThreadPoolExecutorBase<T>
        where T : IExecutorKey
    {
        private IList<ThreadExecutor<T>> _workerThreads = null;
        private IDictionary<int, int> _workerThreadItems = null;
        private Random _rnd = null;

        internal QueueThreadPoolExecutor(IExecutorService executorService, int capacity, int queueCapacity)
            : this(executorService, capacity, queueCapacity, false, false) { }

        internal QueueThreadPoolExecutor(IExecutorService executorService, int capacity, int queueCapacity,
            bool kernelModeQueue, bool flushItemsBeforeClose)
            : base(executorService, queueCapacity, kernelModeQueue, flushItemsBeforeClose)
        {
            Debug.Assert(capacity > 0);
            this.Capacity = capacity;
            this.Initialize(false);
        }

        public int Capacity { get; set; }

        private void Initialize(bool checkItemCount)
        {
            ModuleProc PROC = new ModuleProc(DYN_MODULE_NAME, "Initialize");

            try
            {
                _uniqueKey = "QueueThreadsThreadPoolExecutor_" + Guid.NewGuid().ToString();
                Log.InfoV(PROC, "{0} : Capacity : {1:D}, Queue Capacity : {2:D}", _uniqueKey, this.Capacity, this.QueueCapacity);

                this.RegisterForShutdown();
                this.ExecutorService.AddExecutor(this);

                _workerThreads = new List<ThreadExecutor<T>>();
                _activeWorkersCount = new SortedDictionary<string, ThreadHashValue>(StringComparer.InvariantCultureIgnoreCase);
                _workerThreadItems = new SortedDictionary<int, int>();
                _rnd = new Random(0);

                for (int i = 0; i < this.Capacity; i++)
                {
                    IThreadSafeQueue<ThreadExecutorItem<T>> threadQueue = null;

                    if (_kernelModeQueue)
                        threadQueue = new BlockingBoundQueueKernel<ThreadExecutorItem<T>>(this.ThreadExecutorService, this.QueueCapacity);
                    else
                        threadQueue = new BlockingBoundQueueUser<ThreadExecutorItem<T>>(this.ThreadExecutorService, this.QueueCapacity);

                    ThreadExecutor<T> executor = new ThreadExecutor<T>(this.ThreadExecutorService, threadQueue, checkItemCount, string.Empty);
                    executor.ProcessItem += new ExecutorProcessItemHandler<T>(OnExecutor_ProcessItem);
                    executor.ProcessItemCompleted += new ExecutorProcessItemCompletedHandler<T>(OnExecutor_ProcessItemCompleted);
                    executor.ContainerIndex = i;
                    _workerThreads.Add(executor);
                    _activeWorkersCount.Add(executor.UniqueKey, new ThreadHashValue()
                    {
                        Executor = executor,
                        ItemCount = 0
                    });
                    if (!_workerThreadItems.ContainsKey(i))
                    {
                        _workerThreadItems.Add(i, 0);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        void OnExecutor_ProcessItem(T item)
        {
            this.OnProcessItem(item);
        }

        void OnExecutor_ProcessItemCompleted(ThreadExecutor<T> source, ThreadExecutorItem<T> item)
        {
            ModuleProc PROC = new ModuleProc(DYN_MODULE_NAME, "OnExecutor_ProcessItemCompleted");

            try
            {
                lock (_lockList)
                {
                    try
                    {
                        Interlocked.Decrement(ref _activeWorkers);
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

        private void DoWorkItem(ThreadExecutor<T> executor, T item)
        {
            ModuleProc PROC = new ModuleProc(DYN_MODULE_NAME, "DoWorkItem");

            try
            {
                if (!item.Equals(default(T)))
                {
                    try
                    {
                        if (executor != null)
                        {
                            ThreadExecutorItem<T> executorItem = new ThreadExecutorItem<T>(item);
                            executor.QueueWorkerItem(executorItem);
                            Interlocked.Increment(ref _activeWorkers);
                            this.ActiveWorkerThreadIncrement(executor.UniqueKey, item.UniqueKey);
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
        }

        private ThreadExecutor<T> GetFreeThreadExecutor(T item)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "GetFreeThreadExecutor");
            ThreadExecutor<T> executor = null;

            try
            {
                // executor key free thread approach
                if (item is IExecutorKeyFreeThread)
                {
                    // find the thread with minimum number of items
                    IGrouping<int, KeyValuePair<int, int>> groupedItem = (from i in _workerThreadItems
                                                                          group i by i.Value).OrderBy(i => i.Key).FirstOrDefault();
                    int index = 0;
                    if (groupedItem != null)
                    {
                        index = groupedItem.FirstOrDefault().Key;
                    }

                    if (index >= 0 && index < _workerThreads.Count)
                    {
                        executor = _workerThreads[index];
                        Log.Info(PROC, "Thread " + executor.ContainerIndex.ToString() + " was taken by IExecutorKeyFreeThread approach.");
                        if (_workerThreadItems.ContainsKey(index))
                        {
                            _workerThreadItems[index] = _workerThreadItems[index] + 1;
                        }
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
                        Log.Info(PROC, "Thread " + executor.ContainerIndex.ToString() + " was taken by ExecutorKeyTheread approach.");
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
            finally
            {
                // no more free threads
                if (executor == null)
                {
                    executor = _workerThreads[_rnd.Next(0, _workerThreads.Count - 1)];
                    Log.Info(PROC, "Thread " + executor.ContainerIndex.ToString() + " was taken as random.");
                }
            }

            return executor;
        }

        #region IExecutor<T> Members

        protected abstract bool IsQueueFull(ThreadExecutor<T> executor, T item);

        public override void QueueWorkerItem(T item)
        {
            ModuleProc PROC = new ModuleProc(DYN_MODULE_NAME, "QueueWorkerItem");
            ThreadExecutor<T> executor = null;
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
                            Log.Info(PROC, string.Format("Found Thread Index : {0:D} for item key : {1}", executor.ContainerIndex, itemKey));
                            _itemWorkerThreads.Add(itemKey, executor);
                        }
                    }

                    if (executor != null)
                    {
                        // if full, wait until some items consumed
                        while (this.IsQueueFull(executor, item))
                        {
                            Log.Description(PROC, "Queue is full. Thread pool is blocked for : " + itemKey);
                            if (this.ExecutorService.IsShutdown) break;
                            _fullWaiters++;

                            try
                            {
                                lock (_lockFullEvent)
                                {
                                    Monitor.Exit(_lockList);
                                    Log.Debug(PROC, "Locked : " + _uniqueKey);
                                    Monitor.Wait(_lockFullEvent);
                                    Log.Debug(PROC, "Unlocked : " + _uniqueKey);
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
                    Log.Exception(PROC, ex);
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

        #endregion
    }
}
