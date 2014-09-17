using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using BMC.CoreLib.Diagnostics;
using BMC.CoreLib.Concurrent;

namespace BMC.CoreLib.Win32
{
    /// <summary>
    /// Thread Invoker
    /// </summary>
    public class ThreadInvoker : DisposableObject, IAsyncProgress2
    {
        private AsyncWaitCallback _callback = null;
        private AsyncWaitCallback _finishedCallback = null;
        private AsyncWaitCallback _abortCallback = null;

        private WaitCallback _actualCallback = null;
        private UpdateAsyncStatusObjectHandler _updateStatus = null;
        private UpdateAsyncStatusObjectHandler _updateStatusUser = null;
        private Thread _workerThread = null;
        //private bool _isListening = true;

        /// <summary>
        /// Initializes a new instance of the <see cref="ThreadInvoker"/> class.
        /// </summary>
        /// <param name="ownerForm">The owner form.</param>
        public ThreadInvoker(Form ownerForm, IExecutorService executorService, int sleepInterval)
        {
            this.OwnerForm = ownerForm;
            this.ExecutorService = executorService;
            this.SleepInterval = sleepInterval;
            _actualCallback = new WaitCallback(this.ExecAsync);
            _updateStatus = new UpdateAsyncStatusObjectHandler(this.UpdateStatusObject);
        }

        /// <summary>
        /// Initializes the specified callback.
        /// </summary>
        /// <param name="callback">The callback.</param>
        /// <param name="finishedCallback">The finished callback.</param>
        /// <param name="abortCallback">The abort callback.</param>
        /// <param name="ownerForm">The owner form.</param>
        /// <param name="parentForm">The parent form.</param>
        public void Initialize(AsyncWaitCallback callback,
            AsyncWaitCallback finishedCallback,
            AsyncWaitCallback abortCallback,
            UpdateAsyncStatusObjectHandler updateStatus)
        {
            _callback = callback;
            _finishedCallback = finishedCallback;
            _abortCallback = abortCallback;
            _updateStatusUser = updateStatus;
        }

        /// <summary>
        /// Starts the async.
        /// </summary>
        public void StartAsync()
        {
            if (_workerThread == null)
            {
                _workerThread = Extensions.CreateThread(new ParameterizedThreadStart(this.ExecAsync));
                _workerThread.Start(this);
            }
        }

        /// <summary>
        /// Stops the async.
        /// </summary>
        public void StopAsync()
        {
            if (_workerThread != null)
            {
                if (!this.ExecutorService.IsShutdown)
                {
                    this.ExecutorService.Shutdown();
                }
                Thread.Sleep(2000);
                if (_workerThread.ThreadState == ThreadState.Running)
                {
                    _workerThread.Abort();
                    _workerThread = null;
                }
            }
        }

        /// <summary>
        /// Execs the async.
        /// </summary>
        /// <param name="state">The state.</param>
        private void ExecAsync(object state)
        {
            ModuleProc PROC = new ModuleProc("ThreadInvoker", "ExecAsync");
            DialogResult result = DialogResult.None;

            try
            {
                while (!this.ExecutorService.WaitForShutdown(this.SleepInterval))
                {
                    if (_callback != null)
                        _callback(this);
                }
                result = DialogResult.OK;
            }
            catch (ThreadAbortException ex)
            {
                result = DialogResult.Abort;
                Log.Exception(PROC, ex);
                if (_abortCallback != null)
                    _abortCallback(this);
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
            finally
            {
                if (result != DialogResult.Abort)
                {
                    if (_finishedCallback != null)
                        _finishedCallback(this);
                }
            }
        }

        /// <summary>
        /// Gets or sets the owner form.
        /// </summary>
        /// <value>The owner form.</value>
        public Form OwnerForm { get; private set; }

        /// <summary>
        /// Gets or sets the sleep interval.
        /// </summary>
        /// <value>The sleep interval.</value>
        public int SleepInterval { get; private set; }

        #region IAsyncProgress Members

        /// <summary>
        /// Gets the executor service.
        /// </summary>
        /// <value>The executor service.</value>
        public IExecutorService ExecutorService { get; private set; }

        /// <summary>
        /// Updates the status.
        /// </summary>
        /// <param name="statusText">The status text.</param>
        public void UpdateStatus(string statusText){}

        /// <summary>
        /// Updates the status.
        /// </summary>
        /// <param name="status"></param>
        public void UpdateStatusObject(object status)
        {
            if (this.OwnerForm != null)
            {
                if (this.OwnerForm.InvokeRequired)
                {
                    this.OwnerForm.Invoke(_updateStatus, status);
                }
                else
                {
                    if (_updateStatusUser != null)
                        _updateStatusUser(status);
                }
            }
        }

        /// <summary>
        /// Crosses the thread invoke.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <param name="args">The args.</param>
        public void CrossThreadInvoke(Delegate method, params object[] args)
        {
            if (this.OwnerForm != null)
            {
                this.OwnerForm.CrossThreadInvoke(method, args);
            }
        }

        /// <summary>
        /// Crosses the thread invoke.
        /// </summary>
        /// <param name="action">The action.</param>
        public void CrossThreadInvoke(Action action)
        {
            if (this.OwnerForm != null)
            {
                this.OwnerForm.CrossThreadInvoke((Delegate)action);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [close on complete].
        /// </summary>
        /// <value><c>true</c> if [close on complete]; otherwise, <c>false</c>.</value>
        public bool CloseOnComplete { get; set; }

        public event EventHandler CancelClick = null;

        private void OnCancelClick()
        {
            if (this.CancelClick != null)
            {
                this.CancelClick(null, EventArgs.Empty);
            }
        }

        #endregion

        #region IAsyncProgress2 Members

        public void InitializeProgress(int minimum, int maximum)
        {
            throw new NotImplementedException();
        }

        public void UpdateStatusProgress(int value, string statusText)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets or sets the color of the console.
        /// </summary>
        /// <value>The color of the console.</value>
        public ConsoleColor? ConsoleColor { get; set; }

        #endregion
    }
}
