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
    public class BlockingBoundQueueUser<T>
        : ExecutorServiceBase, IThreadSafeQueue<T>
    {
        private LinkedList<T> _queue = null;

        private object _lockFullEvent = new object();
        private object _lockEmptyEvent = new object();
        private int _fullWaiters = 0;
        private int _emptyWaiters = 0;
        private int _count = 0;
        private bool _isLogging = false;

        public BlockingBoundQueueUser(IExecutorService executorService, int capacity)
            : this(executorService, capacity, -1) { }

        public BlockingBoundQueueUser(IExecutorService executorService, int capacity, int queueTimeout)
            : this(executorService, capacity, queueTimeout, false) { }

        public BlockingBoundQueueUser(IExecutorService executorService, int capacity, bool isLogging)
            : this(executorService, capacity, -1, isLogging) { }

        public BlockingBoundQueueUser(IExecutorService executorService, int capacity, int queueTimeout, bool isLogging)
            : base(executorService)
        {
            this.Capacity = capacity;
            _queue = new LinkedList<T>();
            _isLogging = isLogging;
            this.QueueTimeout = (queueTimeout < 0 ? -1 : queueTimeout);
            this.Initialize();
        }

        private void Initialize()
        {
            ModuleProc PROC = new ModuleProc(DYN_MODULE_NAME, "Initialize");

            try
            {
                this.RegisterForShutdown();
            }
            catch (Exception ex)
            {
                this.LogException(PROC, ex);
            }
        }

        private void LogException(ModuleProc PROC, Exception ex)
        {
            if (!_isLogging) Log.Exception(PROC, ex);
            else EventLogExceptionAdapter.WriteException(ex);
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

            lock (_queue)
            {
                try
                {
                    // if full, wait until some items consumed
                    while (_queue.Count == this.Capacity)
                    {
                        if (this.ExecutorService.IsShutdown) break;
                        _fullWaiters++;

                        try
                        {
                            lock (_lockFullEvent)
                            {
                                Monitor.Exit(_queue);
                                Log.Debug(PROC, "Locked : BlockingBoundQueueUser.Enqueue");
                                Monitor.Wait(_lockFullEvent);
                                Log.Debug(PROC, "Unlocked : BlockingBoundQueueUser.Enqueue");
                                Monitor.Enter(_queue);
                            }
                        }
                        finally
                        {
                            _fullWaiters--;
                        }
                    }
                }
                catch (Exception ex)
                {
                    this.LogException(PROC, ex);
                }
                finally
                {
                    if (!this.ExecutorService.IsShutdown)
                    {
                        if (priority == QueueItemPriority.High)
                        {
                            _queue.AddFirst(item);
                        }
                        else
                        {
                            _queue.AddLast(item);
                        }

                        result = true;
                        _count = _queue.Count;
                    }
                }
            }

            // wake the waiting consumers
            if (!this.ExecutorService.IsShutdown)
            {
                if (_emptyWaiters > 0)
                {
                    lock (_lockEmptyEvent)
                    {
                        Monitor.Pulse(_lockEmptyEvent);
                    }
                }
            }

            return result;
        }

        public T Dequeue()
        {
            ModuleProc PROC = new ModuleProc(DYN_MODULE_NAME, "Dequeue");
            T result = default(T);

            lock (_queue)
            {
                try
                {
                    // if empty, wait for something to add
                    while (_queue.Count == 0)
                    {
                        if (this.ExecutorService.IsShutdown) break;
                        _emptyWaiters++;

                        try
                        {
                            lock (_lockEmptyEvent)
                            {
                                Monitor.Exit(_queue);
                                Log.Debug(PROC, "Locked : BlockingBoundQueueUser.Dequeue");
                                Monitor.Wait(_lockEmptyEvent);
                                Log.Debug(PROC, "Unlocked : BlockingBoundQueueUser.Dequeue");
                                Monitor.Enter(_queue);
                            }
                        }
                        finally
                        {
                            _emptyWaiters--;
                        }
                    }
                }
                catch (Exception ex)
                {
                    this.LogException(PROC, ex);
                }
                finally
                {
                    if (!this.ExecutorService.IsShutdown)
                    {
                        // always remove from first
                        if (_queue.First != null)
                        {
                            result = _queue.First.Value;
                            _queue.RemoveFirst();
                        }
                    }
                    _count = _queue.Count;
                }
            }

            // wake the producers who are waiting to produce
            if (!this.ExecutorService.IsShutdown)
            {
                if (_fullWaiters > 0)
                {
                    lock (_lockFullEvent)
                    {
                        Monitor.Pulse(_lockFullEvent);
                    }
                }
            }

            return result;
        }

        public bool HasItems
        {
            get
            {
                return (_count > 0);
            }
        }

        public int QueueCount
        {
            get
            {
                return _count;
            }
        }

        #endregion

        protected override void OnShutDownCalled()
        {
            base.OnShutDownCalled();

            // wake the waiting consumers
            if (_emptyWaiters > 0)
            {
                lock (_lockEmptyEvent)
                {
                    Monitor.Pulse(_lockEmptyEvent);
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
    }
}
