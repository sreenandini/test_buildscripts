using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using BMC.CoreLib;
using BMC.CoreLib.Diagnostics;

namespace BMC.CoreLib.Concurrent
{
    /// <summary>
    /// Executor Service Base Interface
    /// </summary>
    public interface IExecutorServiceBase
    {
        /// <summary>
        /// Gets the executor service.
        /// </summary>
        /// <value>The executor service.</value>
        IExecutorService ExecutorService { get; }

        /// <summary>
        /// Gets a value indicating whether this instance is shutdown.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is shutdown; otherwise, <c>false</c>.
        /// </value>
        bool IsShutdown { get; }

        /// <summary>
        /// Waits for shutdown.
        /// </summary>
        /// <param name="milliseconds">The milliseconds.</param>
        /// <returns>True if shutdown was given.</returns>
        bool WaitForShutdown(int milliseconds);
    }

    /// <summary>
    /// Executor Service Base
    /// </summary>
    public abstract class ExecutorServiceBase
        : DisposableObject, IExecutorServiceBase
    {
        private IExecutorService _executorService = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExecutorServiceBase"/> class.
        /// </summary>
        /// <param name="executorService">The executor service.</param>
        protected ExecutorServiceBase(IExecutorService executorService)
        {
            this.ExecutorService = executorService;
        }

        protected void RegisterForShutdown()
        {
            if (this.ExecutorService is ExecutorService)
            {
                ExecutorService executorService2 = ((ExecutorService)this.ExecutorService);
                executorService2.ShutdownCalled -= ExecutorService_ShutdownCalled;
                executorService2.ShutdownCalled += new ShutdownCalledEventHandler(ExecutorService_ShutdownCalled);
            }
        }

        /// <summary>
        /// Handles the ShutdownCalled event of the ExecutorService control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="BMC.CoreLib.Concurrent.ShutdownCalledEventArgs"/> instance containing the event data.</param>
        void ExecutorService_ShutdownCalled(object sender, ShutdownCalledEventArgs e)
        {
            this.OnShutDownCalled();
        }

        /// <summary>
        /// Called when [shut down called].
        /// </summary>
        protected virtual void OnShutDownCalled() { }

        /// <summary>
        /// Gets or sets the executor service.
        /// </summary>
        /// <value>The executor service.</value>
        public IExecutorService ExecutorService
        {
            get
            {
                return _executorService;
            }
            private set
            {
                _executorService = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is shutdown.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is shutdown; otherwise, <c>false</c>.
        /// </value>
        public bool IsShutdown
        {
            get
            {
                return this.ExecutorService.IsShutdown;
            }
        }

        /// <summary>
        /// Waits for shutdown.
        /// </summary>
        /// <param name="milliseconds">The milliseconds.</param>
        /// <returns>True if shutdown was given.</returns>
        public bool WaitForShutdown(int milliseconds)
        {
            return this.ExecutorService.WaitForShutdown(milliseconds);
        }

        protected override void DisposeManaged()
        {
            base.DisposeManaged();
        }
    }

    /// <summary>
    /// Executor Thread Pool Manager
    /// </summary>
    /// <typeparam name="T">Type of the executor item.</typeparam>
    public class ExecutorThreadPoolManager<T> : ExecutorServiceBase, IExecutor<T>
        where T : IExecutorKey
    {
        private IThreadPoolExecutor<T> _executor = null;
        private ExecutorProcessItemHandler<T> _handler = null;

        public ExecutorThreadPoolManager(IExecutorService executorService, int threads, ExecutorProcessItemHandler<T> handler)
            : this(executorService, ThreadPoolType.NonBlockQueue, threads, -1, handler) { }

        public ExecutorThreadPoolManager(IExecutorService executorService, ThreadPoolType poolType, int threads,
            int capacity, ExecutorProcessItemHandler<T> handler)
            : base(executorService)
        {
            _handler = handler;
            _executor = ThreadPoolExecutorFactory.CreateThreadPool<T>(executorService, poolType, threads, -1);
            _executor.ProcessItem += OnExecutor_ProcessItem;
        }

        void OnExecutor_ProcessItem(T item)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "OnExecutor_ProcessItem");

            try
            {
                Log.Info(PROC, "Data Received (on " + Thread.CurrentThread.ManagedThreadId.ToString() + ") : " + item.ToString());
                _handler(item);
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        protected override void DisposeManaged()
        {
            base.DisposeManaged();
            _executor.ProcessItem -= _handler;
        }

        public void QueueWorkerItem(T item)
        {
            _executor.QueueWorkerItem(item);
        }

        public System.Threading.EventWaitHandle WaitHandle
        {
            get { return null; }
        }

        public string UniqueKey
        {
            get { return string.Empty; }
        }
    }
}
