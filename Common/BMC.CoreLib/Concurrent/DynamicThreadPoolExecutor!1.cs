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
    internal class DynamicThreadPoolExecutor<T>
        : ThreadPoolExecutorBase<T>
        where T : IExecutorKey
    {
        internal DynamicThreadPoolExecutor(IExecutorService executorService, int queueCapacity)
            : this(executorService, queueCapacity, false, false) { }

        internal DynamicThreadPoolExecutor(IExecutorService executorService, int queueCapacity, bool kernelModeQueue, bool flushItemsBeforeClose)
            : base(executorService, queueCapacity, kernelModeQueue, flushItemsBeforeClose)
        {
            this.Initialize();
        }

        private void Initialize()
        {
            ModuleProc PROC = new ModuleProc(DYN_MODULE_NAME, "Initialize");

            try
            {
                _uniqueKey = "QueueThreadsThreadPoolExecutor_" + Guid.NewGuid().ToString();
                Log.InfoV(PROC, "{0} : Queue Capacity : {1:D}", _uniqueKey, this.QueueCapacity);

                this.RegisterForShutdown();
                this.ExecutorService.AddExecutor(this);                

                _activeWorkersCount = new SortedDictionary<string, ThreadHashValue>(StringComparer.InvariantCultureIgnoreCase);
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        private ThreadExecutor<T> AddThreadExecutor(string itemKey)
        {
            ModuleProc PROC = new ModuleProc(DYN_MODULE_NAME, "Initialize");
            ThreadExecutor<T> executor = null;

            try
            {
                IThreadSafeQueue<ThreadExecutorItem<T>> threadQueue = null;

                if (_kernelModeQueue)
                    threadQueue = new BlockingBoundQueueKernel<ThreadExecutorItem<T>>(this.ThreadExecutorService, this.QueueCapacity);
                else
                    threadQueue = new BlockingBoundQueueUser<ThreadExecutorItem<T>>(this.ThreadExecutorService, this.QueueCapacity);

                executor = new ThreadExecutor<T>(this.ThreadExecutorService, threadQueue, false, itemKey);
                executor.ProcessItem += new ExecutorProcessItemHandler<T>(OnExecutor_ProcessItem);
                executor.ProcessItemCompleted += new ExecutorProcessItemCompletedHandler<T>(OnExecutor_ProcessItemCompleted);
                _activeWorkersCount.Add(itemKey, new ThreadHashValue()
                {
                    Executor = executor,
                    ItemCount = 0
                });
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return executor;
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
                        this.ActiveWorkerThreadDecrement(item.UniqueKey, item.UniqueKey);
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
                            this.ActiveWorkerThreadIncrement(item.UniqueKey, item.UniqueKey);
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

        #region IExecutor<T> Members

        protected virtual bool IsQueueFull(ThreadExecutor<T> executor, T item)
        {
            return (_activeWorkersCount[item.UniqueKey].ItemCount == this.QueueCapacity);
        }

        public override void QueueWorkerItem(T item)
        {
            ModuleProc PROC = new ModuleProc(DYN_MODULE_NAME, "QueueWorkerItem");
            ThreadExecutor<T> executor = null;
            if (this.ExecutorService.IsShutdown) return;

            lock (_lockList)
            {
                try
                {
                    if (!item.Equals(default(T)))
                    {
                        // find the executor
                        string itemKey = item.UniqueKey;
                        if (_activeWorkersCount.ContainsKey(itemKey))
                        {
                            executor = _activeWorkersCount[itemKey].Executor;
                        }
                        else
                        {
                            executor = this.AddThreadExecutor(itemKey);
                            Log.Info(PROC, string.Format("Found Thread : {0} for item key : {1}", executor.UniqueKey, itemKey));                            
                        }
                    }

                    if (executor != null)
                    {
                        // if full, wait until some items consumed
                        while (this.IsQueueFull(executor, item))
                        {
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
