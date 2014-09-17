using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using BMC.CoreLib.Diagnostics;

namespace BMC.CoreLib.Concurrent
{
    internal class AsyncTaskThreadPoolExecutor<T>
        : AsyncTaskThreadPoolExecutorBase<T>
        where T : IExecutorKey
    {
        internal AsyncTaskThreadPoolExecutor(IExecutorService executorService, int capacity, int queueCapacity)
            : this(executorService, capacity, queueCapacity, false, false, string.Empty) { }

        internal AsyncTaskThreadPoolExecutor(IExecutorService executorService, int capacity, int queueCapacity,
            bool kernelModeQueue, bool flushItemsBeforeClose, string threadSuffix)
            : base(executorService, queueCapacity, kernelModeQueue, flushItemsBeforeClose)
        {
            _kernelModeQueue = kernelModeQueue;
            Debug.Assert(capacity > 0);
            this.Capacity = capacity;
            this.QueueCapacity = queueCapacity;
            this.Initialize(false, threadSuffix);
        }

        private void Initialize(bool checkItemCount, string threadSuffix)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "Initialize"))
            {
                try
                {
                    _uniqueKey = "AsyncTaskThreadPoolExecutor_" + Guid.NewGuid().ToString();
                    method.InfoV("{0} : Capacity : {1:D}, Queue Capacity : {2:D}", _uniqueKey, this.Capacity, this.QueueCapacity);

                    this.RegisterForShutdown();
                    this.ExecutorService.AddExecutor(this);

                    for (int i = 0; i < this.Capacity; i++)
                    {
                        AsyncTaskThreadExecutor<T> executor = new AsyncTaskThreadExecutor<T>(this.ThreadExecutorService, this.QueueCapacity, checkItemCount, threadSuffix);
                        executor.ProcessItem += new ExecutorProcessItemHandler<T>(OnExecutor_ProcessItem);
                        executor.ProcessItemCompleted += new ExecutorProcessItemCompletedHandler2<T>(OnExecutor_ProcessItemCompleted);
                        executor.ContainerIndex = i;

                        _workerThreads.Add(executor);
                        _activeWorkersCount.Add(executor.UniqueKey, new ThreadHashValue()
                        {
                            Executor = executor,
                            ItemCount = 0
                        });
                    }
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }
            }
        }

        protected override void ActiveWorkerThreadIncrement(string uniqueKey, string itemKey) { }

        protected override void ActiveWorkerThreadDecrement(string uniqueKey, string itemKey) { }

        protected override int GetActiveItems()
        {
            return (from a in _workerThreads
                    where a.ItemCount > 0
                    select a).Count();
        }

        #region IExecutor<T> Members

        protected override bool IsQueueFull(AsyncTaskThreadExecutor<T> executor, T item)
        {
            return (executor.ItemCount == executor.QueueCapacity);
        }

        #endregion        
    }
}
