using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.CoreLib.Concurrent
{
    internal class BlockQueueThreadPoolExecutor<T>
        : QueueThreadPoolExecutor<T>
        where T : IExecutorKey
    {
        internal BlockQueueThreadPoolExecutor(IExecutorService executorService, int capacity, int queueCapacity, 
            bool kernelModeQueue, bool flushItemsBeforeClose)
            : base(executorService, capacity, queueCapacity, kernelModeQueue, flushItemsBeforeClose) { }

        protected override bool IsQueueFull(ThreadExecutor<T> executor, T item)
        {
            if (_activeWorkers == this.Capacity) return true;
            return (_activeWorkersCount[executor.UniqueKey].ItemCount == this.QueueCapacity);
        }
    }

    internal class NonBlockQueueThreadPoolExecutor<T>
        : QueueThreadPoolExecutor<T>
        where T : IExecutorKey
    {
        internal NonBlockQueueThreadPoolExecutor(IExecutorService executorService, int capacity, int queueCapacity, 
            bool kernelModeQueue, bool flushItemsBeforeClose)
            : base(executorService, capacity, queueCapacity, kernelModeQueue, flushItemsBeforeClose) { }

        protected override bool IsQueueFull(ThreadExecutor<T> executor, T item)
        {
            //return (_activeWorkers == this.Capacity);
            return (_activeWorkersCount[executor.UniqueKey].ItemCount == this.QueueCapacity);
        }
    }
}
