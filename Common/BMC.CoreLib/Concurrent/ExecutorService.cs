using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using BMC.CoreLib.Diagnostics;
using BMC.CoreLib;

namespace BMC.CoreLib.Concurrent
{
    /// <summary>
    /// Shutdown Called Event Handler
    /// </summary>
    public delegate void ShutdownCalledEventHandler(object sender, ShutdownCalledEventArgs e);

    /// <summary>
    /// Shutdown Called Event Args
    /// </summary>
    public class ShutdownCalledEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ShutdownCalledEventArgs"/> class.
        /// </summary>
        /// <param name="noOfTimesCalled">The no of times called.</param>
        public ShutdownCalledEventArgs(int noOfTimesCalled)
        {
            this.NoOfTimesCalled = noOfTimesCalled;
        }

        /// <summary>
        /// Gets or sets the no of times called.
        /// </summary>
        /// <value>The no of times called.</value>
        public int NoOfTimesCalled { get; private set; }
    }

    /// <summary>
    /// Executor Service Interface
    /// </summary>
    public interface IExecutorService : IDisposable
    {
        /// <summary>
        /// Adds the executor.
        /// </summary>
        /// <param name="executor">The executor.</param>
        void AddExecutor(IExecutor executor);

        /// <summary>
        /// Removes the executor.
        /// </summary>
        /// <param name="executor">The executor.</param>
        void RemoveExecutor(IExecutor executor);

        /// <summary>
        /// Shutdowns this instance.
        /// </summary>
        void Shutdown();

        /// <summary>
        /// Waits for shutdown.
        /// </summary>
        /// <returns>True if shutdown was given.</returns>
        bool WaitForShutdown();

        /// <summary>
        /// Waits for shutdown.
        /// </summary>
        /// <param name="milliseconds">The milliseconds.</param>
        /// <returns>True if shutdown was given.</returns>
        bool WaitForShutdown(int milliseconds);

        /// <summary>
        /// Waits for shutdown.
        /// </summary>
        /// <param name="waitHandle">The waithandle.</param>
        /// <returns>True if shutdown was given.</returns>
        bool WaitForShutdown(WaitHandle waitHandle);

        /// <summary>
        /// Gets a value indicating whether this instance is shutdown.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is shutdown; otherwise, <c>false</c>.
        /// </value>
        bool IsShutdown { get; }

        /// <summary>
        /// Awaits the termination.
        /// </summary>
        /// <param name="timeToWait">The time to wait.</param>
        void AwaitTermination(TimeSpan timeToWait);

        /// <summary>
        /// Waits for all the threads to finish.
        /// </summary>
        /// <param name="timeToWait">The time to wait.</param>
        /// <returns>True if the signal was given; otherwise false.</returns>
        bool WaitAll(TimeSpan timeToWait);

        /// <summary>
        /// Waits any.
        /// </summary>
        /// <param name="waitHandle">The wait handle.</param>
        /// <param name="milliseconds">The milliseconds.</param>
        /// <returns>Wait handle index.</returns>
        int WaitAny(WaitHandle waitHandle, int milliseconds);
    }

    /// <summary>
    /// IExecutorService2
    /// </summary>
    public interface IExecutorService2 : IExecutorService
    {
        /// <summary>
        /// Sets this instance.
        /// </summary>
        void Set();

        /// <summary>
        /// Resets this instance.
        /// </summary>
        void Reset();

        /// <summary>
        /// Adds the wait handle.
        /// </summary>
        /// <param name="executorService">The executor service.</param>
        void AddWaitHandle(IExecutorService executorService);

        /// <summary>
        /// Adds the wait handle.
        /// </summary>
        /// <param name="waitHandle">The wait handle.</param>
        void AddWaitHandle(WaitHandle waitHandle);

        /// <summary>
        /// Waits any.
        /// </summary>
        /// <param name="milliseconds">The milliseconds.</param>
        /// <returns>Wait handle index.</returns>
        int WaitAny(int milliseconds);

        /// <summary>
        /// Gets the execute wait handle.
        /// </summary>
        /// <value>
        /// The execute wait handle.
        /// </value>
        WaitHandle ExecWaitHandle { get; }

        /// <summary>
        /// Gets the wait token source.
        /// </summary>
        /// <value>
        /// The wait token source.
        /// </value>
        CancellationTokenSource WaitTokenSource { get; }

        /// <summary>
        /// Creates the wait handles.
        /// </summary>
        /// <param name="waitHandle">The wait handle.</param>
        /// <returns>Array of wait handles.</returns>
        WaitHandle[] CreateWaitHandles(WaitHandle waitHandle);
    }

    /// Executor Service Factory Class
    /// </summary>
    public static class ExecutorServiceFactory
    {
        /// <summary>
        /// Creates the executor service.
        /// </summary>
        /// <returns>Executor service instance</returns>
        public static IExecutorService CreateExecutorService()
        {
            return new ExecutorService();
        }
    }

    /// <summary>
    /// Executor Service Class
    /// </summary>
    internal class ExecutorService : DisposableObjectLogger, IExecutorService2
    {
        private const int TIMEOUT_IN_MILLISECONDS = 10;
        private const int MAX_WAITHANDLES = 64;
        private int _noOfTimesCalled = 0;

        private ManualResetEvent _mreShutdown = null;
        private object _lockExecutors = new object();
        private IDictionary<string, IExecutor> _executors = null;
        private bool _isShutdown = false;

        private WaitHandle[] _waitHandles = new WaitHandle[1];

        private Lazy<CancellationTokenSource> _waitTokenSource = null;

        /// <summary>
        /// Initializes the <see cref="ExecutorService"/> class.
        /// </summary>
        internal ExecutorService()
        {
            _mreShutdown = new ManualResetEvent(false);
            _waitHandles[0] = _mreShutdown;
            _waitTokenSource = new Lazy<CancellationTokenSource>(() => { return new CancellationTokenSource(); }, true);
#if !SILVERLIGHT
            _executors = new SortedDictionary<string, IExecutor>(StringComparer.InvariantCultureIgnoreCase);
#else
            _executors = new Dictionary<string, IExecutor>(StringComparer.InvariantCultureIgnoreCase);
#endif
        }

        #region IExecutorService Members

        /// <summary>
        /// Adds the executor.
        /// </summary>
        /// <param name="executor">The executor.</param>
        public void AddExecutor(IExecutor executor)
        {
            if (executor == null) return;
            lock (_lockExecutors)
            {
                try
                {
                    if (!_executors.ContainsKey(executor.UniqueKey))
                    {
                        _executors.Add(executor.UniqueKey, executor);
                    }
                    else
                    {
                        _executors[executor.UniqueKey] = executor;
                    }
                }
                catch { }
            }
        }

        /// <summary>
        /// Removes the executor.
        /// </summary>
        /// <param name="executor">The executor.</param>
        public void RemoveExecutor(IExecutor executor)
        {
            if (executor == null) return;
            lock (_lockExecutors)
            {
                try
                {
                    if (_executors.ContainsKey(executor.UniqueKey))
                    {
                        _executors.Remove(executor.UniqueKey);
                    }
                }
                catch { }
            }
        }

        /// <summary>
        /// Shutdowns this instance.
        /// </summary>
        public void Shutdown()
        {
            Interlocked.Increment(ref _noOfTimesCalled);
            _mreShutdown.Set();
            _isShutdown = true;
            if (ShutdownCalled != null)
            {
                ShutdownCalled(null, new ShutdownCalledEventArgs(_noOfTimesCalled));
            }
        }

        /// <summary>
        /// Occurs when [shutdown called].
        /// </summary>
        public event ShutdownCalledEventHandler ShutdownCalled;

        /// <summary>
        /// Waits for shutdown.
        /// </summary>
        /// <returns>True if shutdown was given.</returns>
        public bool WaitForShutdown()
        {
            return this.WaitForShutdown(TIMEOUT_IN_MILLISECONDS);
        }

        /// <summary>
        /// Waits for shutdown.
        /// </summary>
        /// <param name="milliseconds">The milliseconds.</param>
        /// <returns>True if shutdown was given.</returns>
        public bool WaitForShutdown(int milliseconds)
        {
            ModuleProc PROC = new ModuleProc("ExecutorService", "WaitForShutdown");

            try
            {
                return _mreShutdown.WaitOne(milliseconds);
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
                return true;
            }
        }

        /// <summary>
        /// Waits for shutdown.
        /// </summary>
        /// <param name="waitHandle">The waithandle.</param>
        /// <returns>True if shutdown was given.</returns>
        public bool WaitForShutdown(WaitHandle waitHandle)
        {
            ModuleProc PROC = new ModuleProc("ExecutorService", "WaitForShutdown");

            try
            {
                return (WaitHandle.WaitAny(new WaitHandle[] { _mreShutdown, waitHandle }) == 0);
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
                return _isShutdown;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is shutdown.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is shutdown; otherwise, <c>false</c>.
        /// </value>
        public bool IsShutdown
        {
            get
            {
                return _isShutdown;
            }
        }

        /// <summary>
        /// Awaits the termination.
        /// </summary>
        /// <param name="timeToWait">The time to wait.</param>
        public void AwaitTermination(TimeSpan timeToWait)
        {
            this.Shutdown();
            this.WaitAll(timeToWait);
        }

        /// <summary>
        /// Waits for all the threads to finish.
        /// </summary>
        /// <param name="timeToWait">The time to wait.</param>
        /// <returns>
        /// True if the signal was given; otherwise false.
        /// </returns>
        public bool WaitAll(TimeSpan timeToWait)
        {
            ModuleProc PROC = new ModuleProc("ExecutorService", "IsShutdown");
            bool result = false;

            try
            {
                if (_executors.Count > 0)
                {
                    EventWaitHandle[] waitHandles = (from e in _executors.Values
                                                     select e.WaitHandle).ToArray();
                    if (waitHandles != null)
                    {
                        // more than MAX_WAITHANDLES will not be supported
                        if (waitHandles.Length < (MAX_WAITHANDLES + 1))
                        {
                            result = this.WaitAll(timeToWait, waitHandles, true);
                        }
                        else
                        {
                            int total = waitHandles.Length;
                            int iteration = (int)Math.Ceiling((double)total / (double)MAX_WAITHANDLES);
                            result = true;

                            for (int i = 0; i < iteration; i++)
                            {
                                int start = (i * MAX_WAITHANDLES);
                                int end = Math.Min(total, (start + MAX_WAITHANDLES));

                                EventWaitHandle[] waitHandlesDest = new EventWaitHandle[end - start];
                                Array.Copy(waitHandles, start, waitHandlesDest, 0, waitHandlesDest.Length);
                                result &= this.WaitAll(timeToWait, waitHandlesDest, (i == (iteration - 1)));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return result;
        }

        /// <summary>
        /// Waits for all the threads to finish.
        /// </summary>
        /// <param name="timeToWait">The time to wait.</param>
        /// <returns>
        /// True if the signal was given; otherwise false.
        /// </returns>
        private bool WaitAll(TimeSpan timeToWait, EventWaitHandle[] waitHandles, bool exitContext)
        {
            ModuleProc PROC = new ModuleProc("ExecutorService", "IsShutdown");
            bool result = false;

            try
            {
                if (timeToWait == TimeSpan.Zero)
                {
#if !SILVERLIGHT
                    result = EventWaitHandle.WaitAll(waitHandles, -1, exitContext);
#else
                    result = EventWaitHandle.WaitAll(waitHandles, -1);
#endif
                }
                else
                {
#if !SILVERLIGHT
                    result = EventWaitHandle.WaitAll(waitHandles, timeToWait, exitContext);
#else
                    result = EventWaitHandle.WaitAll(waitHandles, timeToWait);
#endif
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return result;
        }

        /// <summary>
        /// Waits any.
        /// </summary>
        /// <param name="waitHandle">The wait handle.</param>
        /// <param name="milliseconds">The milliseconds.</param>
        /// <returns>Wait handle index.</returns>
        public int WaitAny(WaitHandle waitHandle, int milliseconds)
        {
            return WaitHandle.WaitAny(new WaitHandle[] { _mreShutdown, waitHandle }, milliseconds);
        }

        /// <summary>
        /// Gets the execute wait handle.
        /// </summary>
        /// <value>
        /// The execute wait handle.
        /// </value>
        public WaitHandle ExecWaitHandle { get { return _mreShutdown; } }

        /// <summary>
        /// Gets the wait token source.
        /// </summary>
        /// <value>
        /// The wait token source.
        /// </value>
        /// <exception cref="System.NotImplementedException"></exception>
        public CancellationTokenSource WaitTokenSource
        {
            get { return _waitTokenSource.Value; }
        }

        /// <summary>
        /// Creates the wait handles.
        /// </summary>
        /// <param name="waitHandle">The wait handle.</param>
        /// <returns>
        /// Array of wait handles.
        /// </returns>
        public WaitHandle[] CreateWaitHandles(WaitHandle waitHandle)
        {
            if (waitHandle == null) return new WaitHandle[] { _mreShutdown };
            else return new WaitHandle[] { _mreShutdown, waitHandle };
        }

        #endregion

        #region New Members

        /// <summary>
        /// Sets this instance.
        /// </summary>
        public void Set()
        {
            _mreShutdown.Set();
            _isShutdown = true;
        }

        /// <summary>
        /// Resets this instance.
        /// </summary>
        public void Reset()
        {
            _mreShutdown.Reset();
            _isShutdown = false;
        }

        /// <summary>
        /// Adds the wait handle.
        /// </summary>
        /// <param name="executorService">The executor service.</param>
        public void AddWaitHandle(IExecutorService executorService)
        {
            if (executorService is ExecutorService)
            {
                this.AddWaitHandle(((ExecutorService)executorService)._mreShutdown);
            }
        }

        /// <summary>
        /// Adds the wait handle.
        /// </summary>
        /// <param name="waitHandle">The wait handle.</param>
        public void AddWaitHandle(WaitHandle waitHandle)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "AddWaitHandle");

            try
            {
                int size = _waitHandles.Length;
                Array.Resize<WaitHandle>(ref _waitHandles, size + 1);
                _waitHandles[size] = waitHandle;
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        /// <summary>
        /// Waits any.
        /// </summary>
        /// <param name="milliseconds">The milliseconds.</param>
        /// <returns>Wait handle index.</returns>
        public int WaitAny(int milliseconds)
        {
            return WaitHandle.WaitAny(_waitHandles, milliseconds);
        }

        #endregion
    }
}
