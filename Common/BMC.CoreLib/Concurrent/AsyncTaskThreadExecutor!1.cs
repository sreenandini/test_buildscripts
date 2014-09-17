using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BMC.CoreLib.Collections;

namespace BMC.CoreLib.Concurrent
{
    internal delegate void ExecutorProcessItemCompletedHandler2<T>(AsyncTaskThreadExecutor<T> source, ThreadExecutorItem<T> item)
        where T : IExecutorKey;

    internal class AsyncTaskThreadExecutor<T>
       : ThreadExecutorBase<T>,
        IComparable<AsyncTaskThreadExecutor<T>>
       where T : IExecutorKey
    {
        private const string KEY_PREFIX = "AsyncExec_";
        private IExecutorService2 _executorService2 = null;
        private string _threadName = string.Empty;

        private IThreadSafeQueue<byte> _signalQueue = null;
        private IDictionary<string, ConcurrentQueue<ThreadExecutorItem<T>>> _dataQueues = null;
        private int _dataFound = 0;
        private int _dataProcessed = 0;

        internal AsyncTaskThreadExecutor(IExecutorService executorService, int queueCapacity)
            : this(executorService, queueCapacity, false, string.Empty) { }

        internal AsyncTaskThreadExecutor(IExecutorService executorService, int queueCapacity, bool checkItemCount, string threadSuffix)
            : base(executorService)
        {
            _executorService2 = executorService as IExecutorService2;
            _checkItemCount = checkItemCount;
            this.QueueCapacity = queueCapacity;
            this.Initialize(threadSuffix);
        }

        private void Initialize(string threadSuffix)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "Initialize"))
            {
                string suffix = (threadSuffix.IsEmpty() ? "" : (threadSuffix + "_"));

                try
                {
                    _signalQueue = new BlockingBoundQueueUser<byte>(this.ExecutorService, -1);
                    _dataQueues = new StringConcurrentDictionary<ConcurrentQueue<ThreadExecutorItem<T>>>();

                    _uniqueKey = KEY_PREFIX + Guid.NewGuid().ToString();                    
                    this.ExecutorService.AddExecutor(this);

                    if (!Extensions.UseTaskInsteadOfThread)
                        _thWorker = Extensions.CreateThreadAndStart(this.DoWork, KEY_PREFIX + suffix);
                    else
                        _tskWorker = Extensions.CreateLongRunningTask(this.DoWork);
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }
            }
        }

        private void DoWork()
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "DoWork"))
            {
                try
                {
                    _workerThreadId = Thread.CurrentThread.ManagedThreadId;
                    _threadName = Thread.CurrentThread.Name;
                    if (_threadName.IsEmpty()) _threadName = "Thread_" + Thread.CurrentThread.ManagedThreadId.ToString();
                    method.InfoV("( {0}::DoWork ) {1} started on the thread : {2:D}", _threadName, _uniqueKey, _workerThreadId);

                    while (!_executorService2.WaitForShutdown())
                    {
                        T item = default(T);

                        try
                        {
                            byte signalItem = _signalQueue.Dequeue();
                            do
                            {
                                _dataFound = 0;
                                _dataProcessed = 0;
                                if (_executorService2.IsShutdown)
                                {
                                    method.InfoV("( {0}::DoWork ) Shutdown called. Thread : {1:D} is exiting now.", _threadName, _workerThreadId);
                                    break;
                                }

                                int queuesCount = _dataQueues.Count;
                                Parallel.ForEach<KeyValuePair<string, ConcurrentQueue<ThreadExecutorItem<T>>>>(_dataQueues,
                                    (KeyValuePair<string, ConcurrentQueue<ThreadExecutorItem<T>>> p, ParallelLoopState ps) =>
                                    {
                                        int taskId = Task.CurrentId.SafeValue();
                                        int queueCount = 0;
                                        if (_executorService2.WaitTokenSource.IsCancellationRequested) ps.Stop();
                                        ConcurrentQueue<ThreadExecutorItem<T>> queue = p.Value;
                                        queueCount = queue.Count;

                                        // data found to process
                                        if (queueCount > 0)
                                        {
                                            try
                                            {
                                                ThreadExecutorItem<T> executorItem = null;
                                                if (queue.TryDequeue(out executorItem))
                                                {
                                                    _dataFound++;
                                                    _dataProcessed++;
                                                    method.DebugV("( {0}::DoWork_PLStart::{1} ) Item Count : {2:D}", _threadName, p.Key, queueCount);

                                                    if (_executorService2.IsShutdown)
                                                    {
                                                        method.InfoV("( {0}::DoWork ) Shutdown called. Thread : {1:D} is exiting now.", _threadName, _workerThreadId);
                                                        ps.Stop();
                                                    }
                                                    if (executorItem == null)
                                                    {
                                                        method.InfoV("( {0}::DoWork ) Shutdown called. Invalid executor item received.", _threadName);
                                                        return;
                                                    }

                                                    // actual item
                                                    method.DebugV("( {0}::DoWork ) Thread : {1:D}, Queue Size : {2:D}", _threadName, _workerThreadId, queue.Count);
                                                    item = executorItem.Item;

                                                    // still have item
                                                    if (_executorService2.IsShutdown)
                                                    {
                                                        method.InfoV("( {0}::DoWork ) Shutdown called. Thread : {1:D} is exiting now.", _threadName, _workerThreadId);
                                                        ps.Stop();
                                                    }

                                                    // actual processing and processing completion
                                                    if (!item.Equals(default(T)))
                                                    {
                                                        this.OnProcessItem(item);
                                                        this.OnProcessItemCompleted(executorItem);
                                                    }
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                method.Exception(ex);
                                            }
                                            finally
                                            {
                                                method.DebugV("( {0}::DoWork_PLEnd::{1} ) Item Count : {2:D}", _threadName, p.Key, queueCount);
                                            }
                                        }
                                        else
                                        {
                                            method.DebugV("( {0}::DoWork_PLStartEnd::{1} ) Item Count : {3:D}", _threadName, p.Key, queueCount);
                                        }
                                    });

                                if (_dataProcessed > 0 &&
                                    ((_itemCount - _dataProcessed) >= 0))
                                {
                                    int newCount = (_itemCount - _dataProcessed);
                                    Interlocked.Exchange(ref _itemCount, newCount);
                                    method.DebugV("( {0}::DoWork_ItemComplete ) Queue Count : {1:D}, Item Count : {2:D}", _threadName, queuesCount, _itemCount);
                                }
                                if (_executorService2.WaitForShutdown()) break;
                            } while (_dataFound > 0);
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
                finally
                {
                    method.InfoV("( {0}::DoWork ) Shutdown called. Thread : {1:D} has finished its work.", _threadName, _workerThreadId);
                    this.Shutdown();
                }
            }
        }

        public event ExecutorProcessItemCompletedHandler2<T> ProcessItemCompleted = null;

        private void OnProcessItemCompleted(ThreadExecutorItem<T> item)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "OnProcessItem"))
            {
                try
                {
                    if (this.ProcessItemCompleted != null)
                    {
                        this.ProcessItemCompleted(this, item);
                    }
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }
            }
        }

        #region IExecutor<ThereadExecutorItem<T>> Members

        public override void QueueWorkerItem(ThreadExecutorItem<T> item)
        {
            if (this.ExecutorService.IsShutdown) return;

            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "QueueWorkerItem"))
            {
                try
                {
                    if (item != null)
                    {
                        string uniqueKey = item.UniqueKey;
                        ConcurrentQueue<ThreadExecutorItem<T>> dataQueue = null;

                        if (!_dataQueues.ContainsKey(uniqueKey))
                        {
                            _dataQueues.Add(uniqueKey, (dataQueue = new ConcurrentQueue<ThreadExecutorItem<T>>()));
                        }
                        else
                        {
                            dataQueue = _dataQueues[uniqueKey];
                        }

                        dataQueue.Enqueue(item);
                        Interlocked.Increment(ref _itemCount);

                        method.DebugV("( {0}::QueueWorkerItem ) Queue Count : {1:D}, Item Count : {2:D}", _threadName, _dataQueues.Count, _itemCount);
                        _signalQueue.Enqueue(0);
                    }
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }
            }
        }

        #endregion

        #region IComparable<ThreadExecutor<T>> Members

        public int CompareTo(AsyncTaskThreadExecutor<T> other)
        {
            if (other == null) return 1;
            return (string.Compare(_uniqueKey, other._uniqueKey, true));
        }

        #endregion
    }
}
