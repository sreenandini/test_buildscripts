using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.CoreLib;
using BMC.CoreLib.Diagnostics;
using System.Threading;
using BMC.CoreLib.Concurrent;

namespace BMC.CoreLib.Collections
{
    public class BlockingBoundQueueKernel<T>
        : ExecutorServiceBase, IThreadSafeQueue<T>
    {
        private Semaphore _semProducer = null;
        private Semaphore _semConsumer = null;
        private Mutex _mtxLock = null;
        private LinkedList<T> _queue = null;

        public BlockingBoundQueueKernel(IExecutorService executorService, int capacity)
            : this(executorService, capacity, -1) { }

        public BlockingBoundQueueKernel(IExecutorService executorService, int capacity, int queueTimeout)
            : base(executorService)
        {
            _semProducer = new Semaphore(capacity, capacity);
            _semConsumer = new Semaphore(0, capacity);
            _mtxLock = new Mutex();
            _queue = new LinkedList<T>();
            this.QueueTimeout = (queueTimeout < 0 ? -1 : queueTimeout);
        }

        public int QueueTimeout { get; set; }

        #region IQueueHandler<T> Members

        public int Capacity { get; private set; }

        public bool CanDequeue()
        {
            return false;
        }

        public bool Enqueue(T item)
        {
            return this.Enqueue(item, QueueItemPriority.Normal);
        }

        public bool Enqueue(T item, QueueItemPriority priority)
        {
            ModuleProc PROC = new ModuleProc(DYN_MODULE_NAME, "Enqueue");
            bool result = default(bool);

            // will block when the queue is full
            if (!_semProducer.WaitOne(this.QueueTimeout))
                return false;

            try
            {
                _mtxLock.WaitOne();

                // high priority items should be come first
                if (priority == QueueItemPriority.High)
                    _queue.AddFirst(item);
                else
                    _queue.AddLast(item);
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
            finally
            {
                _mtxLock.ReleaseMutex();
                _semConsumer.Release(); // signal the consumer to pick the data from queue
            }

            return result;
        }

        public T Dequeue()
        {
            ModuleProc PROC = new ModuleProc(DYN_MODULE_NAME, "Enqueue");
            T result = default(T);

            // some thing should be there in the queue
            if (!_semConsumer.WaitOne(this.QueueTimeout))
                return result;

            try
            {
                _mtxLock.WaitOne();

                // always remove the first node
                if (_queue.First != null)
                {
                    result = _queue.First.Value;
                    _queue.RemoveFirst();
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
            finally
            {
                _mtxLock.ReleaseMutex();
                _semProducer.Release(); // signal the producer to add items to queue
            }

            return result;
        }

        public bool HasItems
        {
            get
            {
                try
                {
                    _mtxLock.WaitOne();
                    return (_queue.Count > 0);
                }
                finally
                {
                    _mtxLock.ReleaseMutex();
                }
            }
        }

        public int QueueCount
        {
            get
            {
                try
                {
                    _mtxLock.WaitOne();
                    return _queue.Count;
                }
                finally
                {
                    _mtxLock.ReleaseMutex();
                }
            }
        }

        #endregion
    }
}
