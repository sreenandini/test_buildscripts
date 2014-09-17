using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using BMC.CoreLib.Collections;
using BMC.CoreLib.Diagnostics;

namespace BMC.CoreLib.Concurrent
{
    public interface IProducerConsumerQueue<T> : IDisposable
    {
        void Enqueue(T item);

        event ProducerConsumerDequeueHandler<T> Dequeue;
    }

    public delegate void ProducerConsumerDequeueHandler<T>(T item);

    public static class ProducerConsumerQueueFactory
    {
        public static IProducerConsumerQueue<T> Create<T>(IExecutorService executorService, int capacity)
        {
            return Create<T>(executorService, capacity, 100, false);
        }

        public static IProducerConsumerQueue<T> Create<T>(IExecutorService executorService, int capacity, int queueTimeout, bool isLogging)
        {
            return new ProducerConsumerQueue<T>(executorService, capacity, queueTimeout, isLogging);
        }
    }

    internal class ProducerConsumerQueue<T> 
        : ExecutorServiceBase, IProducerConsumerQueue<T>
    {
        private IThreadSafeQueue<T> _queue = null;
        private Thread _dequeueThread = null;
        private int _queueTimeout = 100;

        internal ProducerConsumerQueue(IExecutorService executorService, int capacity)
            : this(executorService, capacity, 100, false) { }


        internal ProducerConsumerQueue(IExecutorService executorService, int capacity, int queueTimeout, bool isLogging)
            : base(executorService)
        {
            _queueTimeout = queueTimeout;
            _queue = new BlockingBoundQueueUser<T>(executorService, capacity, queueTimeout, isLogging);
            _dequeueThread = Extensions.CreateThreadAndStart(new ThreadStart(this.OnDequeue), "ProducerConsumerQueue_OnDequeue_");
        }

        public void Enqueue(T item)
        {
            _queue.Enqueue(item);
        }

        public event ProducerConsumerDequeueHandler<T> Dequeue;

        public void OnDequeue()
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "OnDequeue");

            try
            {
                IExecutorService exec = this.ExecutorService;
                while (!exec.WaitForShutdown(_queueTimeout))
                {
                    try
                    {
                        T item = _queue.Dequeue();
                        if (exec.IsShutdown) break;

                        if (this.Dequeue != null)
                        {
                            this.Dequeue(item);
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
    }
}
