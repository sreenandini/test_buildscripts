using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.CoreLib.Concurrent
{
    public enum ThreadPoolType
    {
        FreeThreads = 0,
        BlockQueue = 1,
        NonBlockQueue = 2,
        BlockDynamic = 3,
        NonBlockDynamic = 4,
        AsyncTaskQueue = 99,
    }

    public class ThreadPoolExecutorArg : DisposableObject
    {
        public IExecutorService ExecutorService { get; set; }
        public ThreadPoolType PoolType { get; set; }
        public int ThreadCount { get; set; }
        public int ThreadQueueCount { get; set; }
        public bool KernelModeQueue { get; set; }
        public bool FlushItemsBeforeClose { get; set; }
        public string ThreadSuffix { get; set; }
    }

    public static class ThreadPoolExecutorFactory
    {
        public static IThreadPoolExecutor<T> CreateThreadPool<T>(IExecutorService executorService, ThreadPoolType poolType, int threadCount)
            where T : IExecutorKey
        {
            return CreateThreadPool<T>(executorService, poolType, threadCount, threadCount, false, false);
        }

        public static IThreadPoolExecutor<T> CreateThreadPool<T>(IExecutorService executorService, ThreadPoolType poolType, int threadCount, int threadQueueCount)
            where T : IExecutorKey
        {
            return CreateThreadPool<T>(executorService, poolType, threadCount, threadQueueCount, false, false);
        }

        public static IThreadPoolExecutor<T> CreateThreadPool<T>(IExecutorService executorService, ThreadPoolType poolType, int threadCount,
            int threadQueueCount, bool kernelModeQueue, bool flushItemsBeforeClose)
            where T : IExecutorKey
        {
            return CreateThreadPool<T>(new ThreadPoolExecutorArg()
            {
                ExecutorService = executorService,
                PoolType = poolType,
                ThreadCount = threadCount,
                ThreadQueueCount = threadQueueCount,
                KernelModeQueue = kernelModeQueue,
                FlushItemsBeforeClose = flushItemsBeforeClose,
            });
        }

        public static IThreadPoolExecutor<T> CreateThreadPool<T>(ThreadPoolExecutorArg arg)
            where T : IExecutorKey
        {
            if (arg.ThreadCount <= 0) arg.ThreadCount = 1;

            switch (arg.PoolType)
            {
                case ThreadPoolType.BlockQueue:
                    return new BlockQueueThreadPoolExecutor<T>(arg.ExecutorService, arg.ThreadCount, 1, arg.KernelModeQueue, arg.FlushItemsBeforeClose);

                case ThreadPoolType.NonBlockQueue:
                    {
                        if (arg.ThreadQueueCount <= 0) arg.ThreadQueueCount = -1;
                        return new NonBlockQueueThreadPoolExecutor<T>(arg.ExecutorService, arg.ThreadCount, arg.ThreadQueueCount, arg.KernelModeQueue, arg.FlushItemsBeforeClose);
                    }

                case ThreadPoolType.BlockDynamic:
                    return new DynamicThreadPoolExecutor<T>(arg.ExecutorService, 1, arg.KernelModeQueue, arg.FlushItemsBeforeClose);

                case ThreadPoolType.NonBlockDynamic:
                    {
                        if (arg.ThreadQueueCount <= 0) arg.ThreadQueueCount = -1;
                        return new DynamicThreadPoolExecutor<T>(arg.ExecutorService, arg.ThreadQueueCount, arg.KernelModeQueue, arg.FlushItemsBeforeClose);
                    }

                case ThreadPoolType.AsyncTaskQueue:
                    {
                        if (arg.ThreadQueueCount <= 0) arg.ThreadQueueCount = -1;
                        return new AsyncTaskThreadPoolExecutor<T>(arg.ExecutorService, arg.ThreadCount, arg.ThreadQueueCount, 
                            arg.KernelModeQueue, arg.FlushItemsBeforeClose, arg.ThreadSuffix);
                    }

                default:
                    return new FreeThreadPoolExecutor<T>(arg.ExecutorService, arg.ThreadCount, arg.KernelModeQueue);
            }
        }
    }
}
