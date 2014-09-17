using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.CoreLib.Concurrent;
using System.Threading;
using BMC.CoreLib.Diagnostics;
using BMC.Common;
namespace BMC.CoreLib.Win32
{
    public partial class AxAsyncProgress2 : UserControl, IAsyncProgress2
    {
        private AsyncWaitCallback _callback = null;
        private AsyncWaitCallback _finishedCallback = null;
        private AsyncWaitCallback _abortCallback = null;

        private IExecutorService _executorService = null;
        private WaitCallback _actualCallback = null;
        private UpdateAsyncStatusHandler _updateStatus = null;
        private UpdateAsyncStatusProgressHandler _updateStatusProgress = null;
        private Thread _workerThread = null;
        private int _minimum = -1;
        private int _maximum = -1;
        private bool _canUpdateProgress = true;

        private IAsyncProgressTimer _elapsedTimer = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="AxAsyncProgress"/> class.
        /// </summary>
        public AxAsyncProgress2()
        {
            _elapsedTimer = AsyncProgressTimerFactory.Create();
            _actualCallback = new WaitCallback(this.ExecAsync);
            _updateStatus = new UpdateAsyncStatusHandler(this.UpdateStatus);
            _updateStatusProgress = new UpdateAsyncStatusProgressHandler(this.UpdateStatusProgress);
            InitializeComponent();
            SetTagProperty();
            this.ResolveResources();
        }

        /// <summary>
        /// Assigns the Resource Key names to the controls--Created by kishore sivagnanam
        /// </summary>
        public void SetTagProperty()
        {
            this.btnCancel.Tag = "Key_CancelCaption";
            this.lblStatus.Tag = "Key_LoadingDot";

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
            int minimum, int maximum,
            AsyncWaitCallback callback,
            AsyncWaitCallback finishedCallback,
            AsyncWaitCallback abortCallback,
            Form ownerForm,
            bool closeOnComplete)
        {
            pbarStatus.Style = ProgressBarStyle.Marquee;
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
            this.UpdateStatusProgress(0, AsyncProgressMessages.GetMessage(-1));
            //Log.LogFormatMessage += new LogFormatMessageHandler(Log_LogFormatMessage);
            this.CloseOnComplete = closeOnComplete;
            this.InitializeProgress(minimum, maximum);
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
                }
                _workerThread = null;
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
            finally
            {
                Log.LogFormatMessage -= (Log_LogFormatMessage);
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
                    _elapsedTimer.Stop();
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
                    this.CrossThreadInvoke(new Action(() =>
                    {
                        this.Visible = false;
                    }), null);
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

        public void InitializeProgress(int minimum, int maximum)
        {
            try
            {
                _minimum = minimum;
                _maximum = maximum;
                _canUpdateProgress = (minimum > 0 && maximum >= 1);
                Action ack = new Action(() =>
                {
                    if (_canUpdateProgress)
                    {
                        pbarStatus.Minimum = minimum;
                        pbarStatus.Maximum = maximum;
                    }
                    pbarStatus.Style = (_canUpdateProgress ? ProgressBarStyle.Blocks : ProgressBarStyle.Marquee);
                });

                if (this.OwnerForm.InvokeRequired)
                {
                    this.CrossThreadInvoke(ack);
                }
                else
                {
                    ack();
                }
            }
            catch { }
        }

        public void UpdateStatusObject(object status)
        {

        }

        /// <summary>
        /// Updates the status progress.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="statusText">The status text.</param>
        public void UpdateStatusProgress(int value, string statusText)
        {
            try
            {
                if (this.OwnerForm.InvokeRequired)
                {
                    this.OwnerForm.Invoke(_updateStatusProgress, value, statusText);
                }
                else
                {
                    if ((value >= _minimum && value <= _maximum) &&
                       (value >= pbarStatus.Minimum && value <= pbarStatus.Maximum))
                    {
                        pbarStatus.Value = value;
                    }
                    if (statusText.IsEmpty()) statusText = AsyncProgressMessages.GetMessage(value);
                    lblStatus.Text = statusText + _elapsedTimer.Elapsed;
                    //Application.DoEvents();
                }
            }
            catch { }
        }

        /// <summary>
        /// Gets or sets the color of the console.
        /// </summary>
        /// <value>The color of the console.</value>
        public ConsoleColor? ConsoleColor { get; set; }

        #endregion
    }
}
