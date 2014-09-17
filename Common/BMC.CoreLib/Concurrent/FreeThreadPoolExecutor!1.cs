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
    internal class FreeThreadPoolExecutor<T>
        : ExecutorBase<T>, IThreadPoolExecutor<T>
        where T : IExecutorKey
    {
        private object _lockList = new object();
        private LinkedList<ThreadExecutor<T>> _workersFree = null;
        private IDictionary<string, ThreadExecutor<T>> _workersRunning = null;
        private IDictionary<string, List<ThreadExecutorItem<T>>> _workerSequences = null;

        private object _lockFullEvent = new object();
        private int _fullWaiters = 0;

        internal FreeThreadPoolExecutor(IExecutorService executorService, int capacity)
            : this(executorService, capacity, false) { }

        internal FreeThreadPoolExecutor(IExecutorService executorService, int capacity, bool kernelModeQueue)
            : base(executorService)
        {
            Debug.Assert(capacity > 0);
            this.Capacity = capacity;
            this.Initialize(kernelModeQueue);
        }

        public int Capacity { get; set; }

        private void Initialize(bool kernelModeQueue)
        {
            ModuleProc PROC = new ModuleProc(DYN_MODULE_NAME, "Initialize");

            try
            {
                _uniqueKey = "FreeThreadPoolExecutor_" + Guid.NewGuid().ToString();
                this.RegisterForShutdown();
                this.ExecutorService.AddExecutor(this);
                Log.InfoV(PROC, "{0} : Capacity : {1:D}", _uniqueKey, this.Capacity);

                _workersFree = new LinkedList<ThreadExecutor<T>>();
                _workersRunning = new SortedDictionary<string, ThreadExecutor<T>>(StringComparer.InvariantCultureIgnoreCase);
                _workerSequences = new SortedDictionary<string, List<ThreadExecutorItem<T>>>(StringComparer.InvariantCultureIgnoreCase);

                for (int i = 0; i < this.Capacity; i++)
                {
                    IThreadSafeQueue<ThreadExecutorItem<T>> threadQueue = null;

                    if (kernelModeQueue)
                        threadQueue = new BlockingBoundQueueKernel<ThreadExecutorItem<T>>(this.ExecutorService, 1);
                    else
                        threadQueue = new BlockingBoundQueueUser<ThreadExecutorItem<T>>(this.ExecutorService, 1);

                    ThreadExecutor<T> executor = new ThreadExecutor<T>(this.ExecutorService, threadQueue);
                    executor.ProcessItem += new ExecutorProcessItemHandler<T>(OnExecutor_ProcessItem);
                    executor.ProcessItemCompleted += new ExecutorProcessItemCompletedHandler<T>(OnExecutor_ProcessItemCompleted);
                    _workersFree.AddLast(new LinkedListNode<ThreadExecutor<T>>(executor));
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
                        string executorKey = source.UniqueKey;
                        string itemKey = item.UniqueKey;

                        if (_workersRunning.ContainsKey(executorKey))
                        {
                            ThreadExecutor<T> executor = _workersRunning[executorKey];
                            if (source == executor)
                            {
                                _workersRunning.Remove(executorKey);
                                _workersFree.AddLast(executor);

                                if (_workerSequences.ContainsKey(itemKey))
                                {
                                    List<ThreadExecutorItem<T>> workerSequence = _workerSequences[itemKey];
                                    if (workerSequence.Count > 0)
                                    {
                                        ThreadExecutorItem<T> sequenceItem = workerSequence[0];
                                        EventWaitHandle wh = sequenceItem.WaitHandle;

                                        if (wh != null)
                                        {
                                            //wh.Close();
                                            //wh = null;
                                            wh.Reset();
                                        }
                                        workerSequence.RemoveAt(0);

                                        if (workerSequence.Count > 0)
                                        {
                                            wh = workerSequence[0].WaitHandle;
                                            if (wh != null)
                                            {
                                                wh.Set();
                                            }
                                        }
                                    }
                                }
                            }
                        }
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

        private void DoWorkItem(T item)
        {
            ModuleProc PROC = new ModuleProc(DYN_MODULE_NAME, "DoWorkItem");

            try
            {
                if (!item.Equals(default(T)))
                {
                    try
                    {
                        if (_workersFree.First != null)
                        {
                            ThreadExecutor<T> executor = _workersFree.First.Value;
                            string executorKey = executor.UniqueKey;
                            string itemKey = item.UniqueKey;
                            List<ThreadExecutorItem<T>> workerSequence = null;
                            _workersFree.RemoveFirst();

                            if (!_workersRunning.ContainsKey(executorKey))
                            {
                                _workersRunning.Add(executorKey, executor);
                            }
                            if (!_workerSequences.ContainsKey(itemKey))
                            {
                                _workerSequences.Add(itemKey, new List<ThreadExecutorItem<T>>());
                            }

                            workerSequence = _workerSequences[itemKey];
                            EventWaitHandle wh = null;
                            if (workerSequence.Count > 0)
                            {
                                //wh = new ManualResetEvent(false);
                                wh = executor.QueueSignal;
                                wh.Reset();
                            }

                            ThreadExecutorItem<T> executorItem = new ThreadExecutorItem<T>(item);
                            executorItem.WaitHandle = wh;
                            workerSequence.Add(executorItem);
                            executor.QueueWorkerItem(executorItem);
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

        public bool TrackItems { get; set; }

        #region IExecutor<T> Members

        public override void QueueWorkerItem(T item)
        {
            ModuleProc PROC = new ModuleProc(DYN_MODULE_NAME, "QueueWorkerItem");
            if (this.ExecutorService.IsShutdown) return;

            lock (_lockList)
            {
                try
                {
                    // if full, wait until some items consumed
                    while (_workersRunning.Count == this.Capacity)
                    {
                        Log.Description(PROC, "Queue is full. Thread pool is blocked : " + _uniqueKey);
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
                catch (Exception ex)
                {
                    Log.Exception(PROC, ex);
                }
                finally
                {
                    if (!this.ExecutorService.IsShutdown)
                    {
                        this.DoWorkItem(item);
                    }
                }
            }
        }

        #endregion

        protected override void OnShutDownCalled()
        {
            ModuleProc PROC = new ModuleProc(DYN_MODULE_NAME, "OnShutDownCalled");

            base.OnShutDownCalled();
            Log.Info(PROC, "Shutdown called. ThreadPoolExecutor has finished his work.");

            // wake the producers who are waiting to produce
            if (_fullWaiters > 0)
            {
                lock (_lockFullEvent)
                {
                    Monitor.Pulse(_lockFullEvent);
                }
            }
            this.Shutdown();
        }
    }
}
