using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using BMC.CoreLib.Diagnostics;
using BMC.CoreLib.Concurrent;

namespace BMC.CoreLib.Win32
{
    /// <summary>
    /// Asynchronous User Control
    /// </summary>
    public partial class AxAsyncProgress : UserControlUI, IAsyncProgress2
    {
        private AsyncWaitCallback _callback = null;
        private AsyncWaitCallback _finishedCallback = null;
        private AsyncWaitCallback _abortCallback = null;

        private IExecutorService _executorService = null;
        private WaitCallback _actualCallback = null;
        private UpdateAsyncStatusHandler _updateStatus = null;
        private Thread _workerThread = null;

        private IAsyncProgressTimer _elapsedTimer = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="AxAsyncProgress"/> class.
        /// </summary>
        public AxAsyncProgress()
        {
            _elapsedTimer = AsyncProgressTimerFactory.Create();
            _actualCallback = new WaitCallback(this.ExecAsync);
            _updateStatus = new UpdateAsyncStatusHandler(this.UpdateStatus);
            InitializeComponent();
        }

        /// <summary>
        /// Initializes the specified callback.
        /// </summary>
        /// <param name="callback">The callback.</param>
        /// <param name="finishedCallback">The finished callback.</param>
        /// <param name="abortCallback">The abort callback.</param>
        /// <param name="ownerForm">The owner form.</param>
        /// <param name="parentForm">The parent form.</param>
        public void Initialize(IExecutorService executorService,
            AsyncWaitCallback callback,
            AsyncWaitCallback finishedCallback,
            AsyncWaitCallback abortCallback,
            Form ownerForm,
            bool closeOnComplete)
        {
            _elapsedTimer.StopAndStart();
            _executorService = executorService;
            _callback = callback;
            _finishedCallback = finishedCallback;
            _abortCallback = abortCallback;

            this.IsCancellable = (_executorService != null);
            if (_executorService != null)
            {
                IExecutorService2 executor2 = _executorService as IExecutorService2;
                if (executor2 != null)
                {
                    executor2.Reset();
                }
            }

            this.OwnerForm = ownerForm;
            //Log.LogFormatMessage += new LogFormatMessageHandler(Log_LogFormatMessage);
            this.CloseOnComplete = closeOnComplete;
            progbarValue.Start();
        }

        void Log_LogFormatMessage(LogEntryType logType, string dateTime, string moduleName, string procedureName, string message)
        {
            this.UpdateStatus(message);
        }

        /// <summary>
        /// Starts the async.
        /// </summary>
        public void StartAsync()
        {
            ModuleProc PROC = new ModuleProc("", "StopAsync");

            try
            {
                this.StopAsync();

                progbarValue.Start();
                if (_workerThread == null)
                {
                    _workerThread = Extensions.CreateThread(new ParameterizedThreadStart(this.ExecAsync));
                    _workerThread.Start(this);
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        /// <summary>
        /// Stops the async.
        /// </summary>
        public void StopAsync()
        {
            ModuleProc PROC = new ModuleProc("", "StopAsync");

            try
            {
                if (_workerThread != null &&
                    _workerThread.IsAlive)
                {
                    _workerThread.Abort();
                    _workerThread = null;
                    progbarValue.Stop();
                }
                _workerThread = null;
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        /// <summary>
        /// Execs the async.
        /// </summary>
        /// <param name="state">The state.</param>
        private void ExecAsync(object state)
        {
            ModuleProc PROC = new ModuleProc("AsyncDialogForm", "ExecAsync");
            DialogResult result = DialogResult.None;

            try
            {
                if (_callback != null)
                    _callback(this);

                if (this.ExecutorService != null)
                {
                    result = this.ExecutorService.IsShutdown ? DialogResult.Cancel : DialogResult.OK;
                }
                else
                {
                    result = DialogResult.OK;
                }
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
                result = DialogResult.Ignore;
            }
            finally
            {
                try
                {
                    if (result != DialogResult.Abort)
                    {
                        if (_finishedCallback != null)
                            _finishedCallback(this);
                    }
                    this.CrossThreadInvoke(new Action(() =>
                    {
                        if (this.CloseOnComplete)
                        {
                            if (!this.IsDisposed)
                            {
                                this.OwnerForm.Close();
                            }
                        }
                    }), null);
                }
                catch { }
                finally
                {
                    this.OwnerForm.DialogResult = result;
                }
            }
        }

        /// <summary>
        /// Gets or sets the owner form.
        /// </summary>
        /// <value>The owner form.</value>
        [Browsable(false)]
        public Form OwnerForm { get; private set; }

        /// <summary>
        /// Gets or sets the dialog owner.
        /// </summary>
        /// <value>The dialog owner.</value>
        [Browsable(false)]
        public Form DialogOwner { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is cancellable.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is cancellable; otherwise, <c>false</c>.
        /// </value>
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Category("Behavior")]
        public bool IsCancellable
        {
            get
            {
                return btnCancel.Visible;
            }
            set
            {
                btnCancel.Visible = value;
                tblItems.ColumnStyles[1].Width = (value ? 120 : 0);
            }
        }

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Category("Behavior")]
        public bool IsStatusUpdatable
        {
            get
            {
                return tblItems.Visible;
            }
            set
            {
                tblItems.Visible = value;
            }
        }

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Category("Behavior")]
        public bool IsActive
        {
            get
            {
                return progbarValue.IsActive;
            }
            set
            {
                if (value) progbarValue.Start();
                progbarValue.Stop();
            }
        }

        /// <summary>
        /// Handles the Click event of the btnCancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (this.ExecutorService != null)
            {
                this.ExecutorService.Shutdown();
            }
            this.OnCancelClick();
            if (this.ExecutorService == null &&
                !this.CloseOnComplete)
            {
                this.OwnerForm.DialogResult = DialogResult.Cancel;
                this.OwnerForm.Close();
            }
        }

        public bool CloseOnComplete { get; set; }

        public event EventHandler CancelClick = null;

        private void OnCancelClick()
        {
            if (this.CancelClick != null)
            {
                this.CancelClick(btnCancel, EventArgs.Empty);
            }
        }

        #region IAsyncProgress Members

        public IExecutorService ExecutorService { get { return _executorService; } }

        /// <summary>
        /// Updates the status.
        /// </summary>
        /// <param name="statusText">The status text.</param>
        public void UpdateStatus(string statusText)
        {
            try
            {
                if (this.OwnerForm.InvokeRequired)
                {
                    this.OwnerForm.Invoke(_updateStatus, statusText);
                }
                else
                {
                    lblStatus.Text = statusText;
                    //Application.DoEvents();
                }
            }
            catch { }
        }

        /// <summary>
        /// Crosses the thread invoke.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <param name="args">The args.</param>
        public void CrossThreadInvoke(Delegate method, params object[] args)
        {
            try
            {
                Form ownerForm = this.DialogOwner != null ? this.DialogOwner : this.OwnerForm;
                ownerForm.CrossThreadInvoke(method, args);
            }
            catch { }
        }

        /// <summary>
        /// Crosses the thread invoke.
        /// </summary>
        /// <param name="action">The action.</param>
        public void CrossThreadInvoke(Action action)
        {
            this.CrossThreadInvoke((Delegate)action, null);
        }

        #endregion

        #region IAsyncProgress2 Members

        public void InitializeProgress(int minimum, int maximum) { }

        public void UpdateStatusObject(object status)
        {
            this.UpdateStatus(status.ToString());
        }

        public void UpdateStatusProgress(int value, string statusText)
        {
            this.UpdateStatus(statusText + _elapsedTimer.Elapsed);
        }

        public ConsoleColor? ConsoleColor { get; set; }

        #endregion
    }
}
