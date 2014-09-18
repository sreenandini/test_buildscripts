/* ================================================================================= 
 * Purpose		:	Thread Dispatcher
 * File Name	:   ThreadDispatcher.cs
 * Author		:	A.Vinod Kumar
 * Created  	:	21/10/2011
 * ================================================================================= 
 * Revision History :
 * ================================================================================= 
 * 21/10/2011		A.Vinod Kumar    Initial Version
 * ================================================================================= 
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using BMC.Common.ExceptionManagement;
using BMC.Common.LogManagement;
using BMC.Common.ConfigurationManagement;
using System.Diagnostics;

namespace BMC.Business.CashDeskOperator
{
    /// <summary>
    /// Thread Data
    /// </summary>
    public interface IThreadData
    {
        /// <summary>
        /// Gets the unique key.
        /// </summary>
        /// <value>The unique key.</value>
        string UniqueKey { get; }
    }

    /// <summary>
    /// Thread Data (Abstract)
    /// </summary>
    public abstract class ThreadData : IThreadData
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ThreadData"/> class.
        /// </summary>
        protected ThreadData() { }

        #region IThreadData Members

        /// <summary>
        /// Gets the unique key.
        /// </summary>
        /// <value>The unique key.</value>
        public abstract string UniqueKey { get; }

        /// <summary>
        /// Gets or sets the name of the dispatcher thread.
        /// </summary>
        /// <value>The name of the dispatcher thread.</value>
        internal string DispatcherThreadName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance can process.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance can process; otherwise, <c>false</c>.
        /// </value>
        public bool CanProcess { get; internal set; }

        /// <summary>
        /// Checks the and write log.
        /// </summary>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="message">The message.</param>
        /// <returns>True if succeeded; otherwise false.</returns>
        public bool CheckAndWriteLog(string methodName, string message)
        {
            bool result = this.CanProcess;

            try
            {
                if (result)
                {
                    LogManager.WriteLog("|::> [" + this.DispatcherThreadName + "] (" + methodName + ") " + message, LogManager.enumLogLevel.Info);
                }
                else
                {
                    LogManager.WriteLog("|::> [" + this.DispatcherThreadName + "] (" + methodName + ") Threads was instructed to close.", LogManager.enumLogLevel.Info);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return result;
        }

        #endregion
    }

    /// <summary>
    /// Process Thread Data Handler
    /// </summary>
    public delegate void ProcessThreadDataHandler<T>(T threadData)
        where T : ThreadData;

    /// <summary>
    /// Thread Dispatcher
    /// </summary>
    public class ThreadDispatcher<T>
        : ObjectStateObserver, IDisposable
        where T : ThreadData
    {
        #region Thread Container
        /// <summary>
        /// Thread Container
        /// </summary>
        private class ThreadContainer
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="ThreadDispatcher&lt;T&gt;.ThreadData"/> class.
            /// </summary>
            public ThreadContainer()
            {
#if THREAD_NO_QUEUE
                this.ActualDatas = new List<T>();
                this.BufferDatas = new List<T>();
#else
                this.ActualDatas = new Queue<T>();
                this.BufferDatas = new Queue<T>();
#endif
            }

            /// <summary>
            /// Buffer Lock
            /// </summary>
            public object BufferLock = new object();

            /// <summary>
            /// Gets or sets the worker thread.
            /// </summary>
            /// <value>The worker thread.</value>
            public Thread WorkerThread { get; set; }

            /// <summary>
            /// Gets the thread id.
            /// </summary>
            /// <value>The thread id.</value>
            public int ThreadId
            {
                get
                {
                    return WorkerThread.ManagedThreadId;
                }
            }

            /// <summary>
            /// Gets the name of the thread.
            /// </summary>
            /// <value>The name of the thread.</value>
            public string ThreadName
            {
                get
                {
                    return WorkerThread.Name;
                }
            }

            public int ContainerIndex { get; set; }

            public bool HaveItems
            {
                get
                {
                    return (this.ActualDatas.Count > 0 || this.BufferDatas.Count > 0);
                }
            }

            /// <summary>
            /// Gets or sets the wait handle.
            /// </summary>
            /// <value>The wait handle.</value>
            public EventWaitHandle WaitHandle { get; set; }

#if THREAD_NO_QUEUE
            /// <summary>
            /// Gets or sets the actual user datas.
            /// </summary>
            /// <value>The actual user datas.</value>
            public List<T> ActualDatas { get; private set; }

            /// <summary>
            /// Gets or sets the buffer user datas.
            /// </summary>
            /// <value>The buffer user datas.</value>
            public List<T> BufferDatas { get; private set; }
#else
            /// <summary>
            /// Gets or sets the actual user datas.
            /// </summary>
            /// <value>The actual user datas.</value>
            public Queue<T> ActualDatas { get; private set; }

            /// <summary>
            /// Gets or sets the buffer user datas.
            /// </summary>
            /// <value>The buffer user datas.</value>
            public Queue<T> BufferDatas { get; private set; }
#endif
        }
        #endregion

        #region Variables
        private int _intialized = 0;
        private int _killProcessInitiated = 0;
        private object _lockObject = new object();
        private object _lockEvents = new object();
        private int _threadCount = 0;
        private bool _isListening = false;
        private IDictionary<int, ThreadContainer> _threadContainers = null;
        private IDictionary<string, int> _threadIndexes = null;
        private Random _rnd = null;
        private ManualResetEvent _evtInitialize = null;
        private WaitHandle[] _closeHandles = null;
        private bool _enableDetailedLogs = true;
        private Thread _thMonitorProcess = null;
        private string _helperName = string.Empty;

        // to find the used/free threads
        private object _lockUsage = new object();
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="ThreadDispatcher"/> class.
        /// </summary>
        /// <param name="threadCount">The thread count.</param>
        public ThreadDispatcher(int threadCount, string helperName)
        {
            ObjectStateNotifier.AddObserver(this);
            _threadCount = (threadCount < 0 ? 1 : threadCount);
            _threadContainers = new SortedDictionary<int, ThreadContainer>();
            _threadIndexes = new SortedDictionary<string, int>(StringComparer.InvariantCultureIgnoreCase);
            _rnd = new Random(_threadCount - 1);
            _evtInitialize = new ManualResetEvent(false);
            _closeHandles = new WaitHandle[0];
            _helperName = helperName;
            this.InitSettings();
            LogManager.WriteLog("|=> ThreadDispatcher created.", LogManager.enumLogLevel.Info);
        }
        #endregion

        #region Events
        /// <summary>
        /// Occurs when [process thread data].
        /// </summary>
        private event ProcessThreadDataHandler<T> ProcessThreadData = null;

        /// <summary>
        /// Called when [process thread data].
        /// </summary>
        /// <param name="threadData">The thread data.</param>
        private void OnProcessThreadData(T threadData)
        {
            if (this.ProcessThreadData != null)
            {
                if (!_isListening)
                {
                    LogManager.WriteLog("|=> OnProcessThreadData() : " + "Thread was instructed to close.",
                        LogManager.enumLogLevel.Info);
                }
                else
                {
                    threadData.CanProcess = _isListening;
                    if (this.ProcessThreadData != null)
                    {
                        this.ProcessThreadData(threadData);
                    }
                }
            }
        }

        /// <summary>
        /// Adds the process thread data.
        /// </summary>
        /// <param name="handler">The handler.</param>
        public void AddProcessThreadData(ProcessThreadDataHandler<T> handler)
        {
            this.ModifyProcessThreadData(handler, false);
        }

        /// <summary>
        /// Removes the process thread data.
        /// </summary>
        /// <param name="handler">The handler.</param>
        public void RemoveProcessThreadData(ProcessThreadDataHandler<T> handler)
        {
            this.ModifyProcessThreadData(handler, true);
        }

        /// <summary>
        /// Modifies the process thread data.
        /// </summary>
        /// <param name="handler">The handler.</param>
        /// <param name="remove">if set to <c>true</c> [remove].</param>
        private void ModifyProcessThreadData(ProcessThreadDataHandler<T> handler, bool remove)
        {
            try
            {
                lock (_lockEvents)
                {
                    if (!remove)
                    {
                        this.ProcessThreadData += handler;
                    }
                    else
                    {
                        this.ProcessThreadData -= handler;
                    }
                }
                LogManager.WriteLog(string.Format("|::> ProcessThreadData event was successfully {0}.", (remove ? "removed" : "added")),
                    LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether this instance is listening.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is listening; otherwise, <c>false</c>.
        /// </value>
        internal bool IsListening
        {
            get { return _isListening; }
            set { _isListening = value; }
        }
        #endregion

        #region Methods

        /// <summary>
        /// Notifies the state.
        /// </summary>
        /// <param name="state">The state.</param>
        public override void NotifyState(ObjectState state)
        {
            base.NotifyState(state);
            this.IsListening = (state == ObjectState.Activated);
        }

        /// <summary>
        /// Inits the settings.
        /// </summary>
        private void InitSettings()
        {
            try
            {
                string disableDetailedLogs = ConfigManager.Read("DisableDetailedLogs");
                _enableDetailedLogs = (string.Compare(disableDetailedLogs, "false", true) == 0);
            }
            catch (Exception)
            {
                _enableDetailedLogs = false;
            }
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public void Initialize()
        {
            try
            {
                if (_intialized == 0)
                {
                    lock (_lockObject)
                    {
                        if (_intialized == 0)
                        {
                            _intialized = 1;
                            _isListening = true;
                            Thread th = new Thread(new ThreadStart(this.InitializeThreads));
                            th.Start();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// Initializes the threads.
        /// </summary>
        private void InitializeThreads()
        {
            LogManager.WriteLog("|=> Inside InitializeThreads().", LogManager.enumLogLevel.Info);

            try
            {
                for (int i = 0; i < _threadCount; i++)
                {
                    Thread th = new Thread(new ParameterizedThreadStart(this.DoWorker));
                    th.IsBackground = true;
                    th.Name = "ThreadDispatcher_" + _helperName + "_" + th.ManagedThreadId.ToString();

                    ThreadContainer container = new ThreadContainer();
                    container.WorkerThread = th;
                    container.WaitHandle = new ManualResetEvent(false);
                    container.ContainerIndex = i;

                    int index = _closeHandles.Length;
                    Array.Resize<WaitHandle>(ref _closeHandles, index + 1);
                    _closeHandles[index] = container.WaitHandle;

                    _threadContainers.Add(i, container);
                    th.Start(container);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            finally
            {
                _intialized = 2;
                _evtInitialize.Set();
                LogManager.WriteLog("|=> All the dispatcher threads are initialized.", LogManager.enumLogLevel.Info);
            }
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public void MonitorAndKillProcess(int secondsToWait)
        {
            try
            {
                secondsToWait = (secondsToWait <= 0 ? 120 : secondsToWait); // default 2 minutes
                if (_killProcessInitiated == 0)
                {
                    lock (_lockObject)
                    {
                        if (_killProcessInitiated == 0)
                        {
                            _killProcessInitiated = 1;
                            LogManager.WriteLog("|=> MonitorAndKillProcess() : Monitor Thread initiated.", LogManager.enumLogLevel.Info);
                            _thMonitorProcess = new Thread(new ParameterizedThreadStart(this.MonitorAndKillProcessInternal));
                            _thMonitorProcess.Name = "TH_MonitorProcess";
                            _thMonitorProcess.Start(secondsToWait);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// Monitors the and kill process internal.
        /// </summary>
        /// <param name="state">The state.</param>
        private void MonitorAndKillProcessInternal(object state)
        {
            try
            {
                Thread.Sleep(((int)state) * 1000);
                try
                {
                    LogManager.WriteLog("|=> MonitorAndKillProcessInternal() : Monitor Thread activated. Killing current process.", LogManager.enumLogLevel.Info);
                    Thread.Sleep(3000);
                    Process.GetCurrentProcess().Kill();
                }
                catch { }
            }
            catch (ThreadAbortException)
            {
                LogManager.WriteLog(string.Format("|=> MonitorAndKillProcessInternal() : Monitor Thread ({0:D}) aborted.", Thread.CurrentThread.ManagedThreadId), LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// Aborts the process monitor thread.
        /// </summary>
        private void AbortProcessMonitorThread()
        {
            try
            {
                if (_thMonitorProcess != null)
                {
                    if (_thMonitorProcess.IsAlive)
                    {
                        _thMonitorProcess.Abort();
                    }
                }
            }
            catch (ThreadAbortException)
            {
                LogManager.WriteLog("|=> AbortProcessMonitorThread() : Monitor Thread aborted.", LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// Waits all.
        /// </summary>
        public void WaitAll()
        {
            this.WaitAll(false, null);
        }

        /// <summary>
        /// Waits all.
        /// </summary>
        /// <param name="killProcessOnFailure">if set to <c>true</c> [kill process on failure].</param>
        public void WaitAll(bool killProcessOnFailure, Action afterWait)
        {
            try
            {
                this.IsListening = false;
                if (_closeHandles.Length > 0)
                {
                    if (!EventWaitHandle.WaitAll(_closeHandles, new TimeSpan(0, 5, 0))) // 5 minutes grace time
                    {
                        LogManager.WriteLog("|=> Unable to cleanup the wait handles.", LogManager.enumLogLevel.Info);
                        if (killProcessOnFailure)
                        {
                            LogManager.WriteLog("|=> Cleanup handles was failed. Killing the current process.", LogManager.enumLogLevel.Info);
                            Thread.Sleep(5000);
                            this.AbortProcessMonitorThread();

                            if (afterWait != null) afterWait();
                            try
                            {
                                Process.GetCurrentProcess().Kill();
                            }
                            catch { }
                        }
                    }
                    else
                    {
                        LogManager.WriteLog("|=> All the wait handles are released.", LogManager.enumLogLevel.Info);
                        this.AbortProcessMonitorThread();
                        if (afterWait != null) afterWait();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// Adds the thread data.
        /// </summary>
        /// <param name="threadData">The thread data.</param>
        /// <returns></returns>
        public bool AddThreadData(T threadData)
        {
            if (_intialized != 2)
            {
                _evtInitialize.WaitOne();
            }
            string PROC = "|=> AddThreadData() : ";
            bool result = false;

            try
            {
                if (!_isListening)
                {
                    LogManager.WriteLog(PROC + "Threads was instructed to close. Unable to add  : " +
                        threadData.UniqueKey, LogManager.enumLogLevel.Info);
                    return false;
                }

                // thread index
                int index = 0;
                if (_threadIndexes.ContainsKey(threadData.UniqueKey))
                {
                    index = _threadIndexes[threadData.UniqueKey];
                    if (_enableDetailedLogs)
                    {
                        LogManager.WriteLog(PROC + string.Format("Thread Unique Key [{0}] was found in [{1:D}].",
                                threadData.UniqueKey, index), LogManager.enumLogLevel.Info);
                    }
                }
                else
                {
                    // take any free thread or a randon thread
                    index = GetFreeThreadIndex();

                    // not already exist?
                    if (!_threadIndexes.ContainsKey(threadData.UniqueKey))
                    {
                        _threadIndexes.Add(threadData.UniqueKey, index);
                    }
                }

                // add to thread collection
                if (_threadContainers.ContainsKey(index))
                {
                    ThreadContainer container = _threadContainers[index];
                    lock (container.BufferLock)
                    {
                        threadData.DispatcherThreadName = container.ThreadName;
#if THREAD_NO_QUEUE
                        container.BufferDatas.Add(threadData);
#else
                        container.BufferDatas.Enqueue(threadData);
#endif
                        if (_enableDetailedLogs)
                        {
                            LogManager.WriteLog(PROC + string.Format("[{0}] added to [{1}] container.",
                                threadData.UniqueKey, container.ThreadId), LogManager.enumLogLevel.Info);
                        }
                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            return result;
        }

        private int GetFreeThreadIndex()
        {
            string PROC = "|=> GetFreeThreadIndex() : ";
            int index = -1;

            try
            {
                lock (_lockUsage)
                {
                    ThreadContainer freeContainer = (from c in _threadContainers.Values
                                                     where c.HaveItems == false
                                                     select c).FirstOrDefault();
                    if (freeContainer != null)
                    {
                        index = freeContainer.ContainerIndex;
                        LogManager.WriteLog(PROC + "Thread " + index.ToString() + " was taken from the free queue.", LogManager.enumLogLevel.Info);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(PROC, ex);
            }
            finally
            {
                // no more free threads
                if (index == -1)
                {
                    index = _rnd.Next(0, _threadCount - 1);
                    LogManager.WriteLog(PROC + "Thread " + index.ToString() + " was taken as random.", LogManager.enumLogLevel.Info);
                }
            }

            return index;
        }

        /// <summary>
        /// Does the worker.
        /// </summary>
        private void DoWorker(object state)
        {
            string PROC = "|=> DoWorker() : ";
            LogManager.WriteLog(PROC + "Thread : " + Thread.CurrentThread.ManagedThreadId.ToString(), LogManager.enumLogLevel.Info);
            ThreadContainer container = state as ThreadContainer;
            string threadMsg = PROC + string.Format("[{0}] : ", container.WorkerThread.Name);
#if THREAD_NO_QUEUE
            List<T> actualDatas = container.ActualDatas;
            List<T> bufferDatas = container.BufferDatas;
#else
            Queue<T> actualDatas = container.ActualDatas;
            Queue<T> bufferDatas = container.BufferDatas;
#endif

            try
            {
                int interval = 100;

                while (_isListening)
                {
                    try
                    {
                        // something to do
                        if (actualDatas.Count > 0)
                        {
                            if (_enableDetailedLogs)
                            {
                                LogManager.WriteLog(threadMsg + string.Format("[{0:D}] actual Items found.", actualDatas.Count), LogManager.enumLogLevel.Info);
                            }
                            while (actualDatas.Count > 0)
                            {
                                if (!_isListening)
                                {
                                    LogManager.WriteLog(threadMsg + "Thread was instructed to close.",
                                        LogManager.enumLogLevel.Info);
                                    return;
                                }
#if THREAD_NO_QUEUE
                                T threadData = actualDatas[0] as T;
#else
                                T threadData = actualDatas.Dequeue();
#endif

                                LogManager.WriteLog(threadMsg + "Processing Item : " + threadData.UniqueKey, LogManager.enumLogLevel.Info);
                                try
                                {
                                    this.OnProcessThreadData(threadData);
                                }
                                catch (Exception ex)
                                {
                                    ExceptionManager.Publish(ex);
                                }

#if THREAD_NO_QUEUE
                                actualDatas.RemoveAt(0);
#endif
                            }
                        }

                        // if the user added items to buffer datas in the meantime.
                        if (bufferDatas.Count > 0)
                        {
                            if (_enableDetailedLogs)
                            {
                                LogManager.WriteLog(threadMsg + string.Format("[{0:D}] Buffer Items found.", bufferDatas.Count.ToString()), LogManager.enumLogLevel.Info);
                            }

                            while (bufferDatas.Count > 0)
                            {
                                if (!_isListening)
                                {
                                    LogManager.WriteLog(threadMsg + "Thread was instructed to close.",
                                        LogManager.enumLogLevel.Info);
                                    return;
                                }
                                lock (container.BufferLock)
                                {
#if THREAD_NO_QUEUE
                                    T threadData = bufferDatas[0];
                                    actualDatas.Add(threadData);
                                    bufferDatas.RemoveAt(0);
#else
                                    T threadData = bufferDatas.Dequeue();
                                    if (this.ProcessThreadData != null)
                                    {
                                        actualDatas.Enqueue(threadData);
                                    }
#endif
                                }
                            }
                            if (_enableDetailedLogs)
                            {
                                LogManager.WriteLog(threadMsg + string.Format("[{0:D}] items moved from buffer to actual.", actualDatas.Count), LogManager.enumLogLevel.Info);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        ExceptionManager.Publish(ex);
                    }
                    finally
                    {
                        Thread.Sleep(interval);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            finally
            {
                LogManager.WriteLog(threadMsg + "Finished Successfully.", LogManager.enumLogLevel.Info);
                container.WaitHandle.Set();
            }
        }
        #endregion

        #region IDisposable Members

        private bool disposed = false;

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        private void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    ObjectStateNotifier.RemoveObserver(this);
                    _threadIndexes.Clear();
                    _threadContainers.Clear();
                    LogManager.WriteLog("|=> ThreadDispatcher disposed.", LogManager.enumLogLevel.Info);
                }
                disposed = true;
            }
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="ThreadDispatcher"/> is reclaimed by garbage collection.
        /// </summary>
        ~ThreadDispatcher()
        {
            Dispose(false);
        }

        #endregion
    }
}
