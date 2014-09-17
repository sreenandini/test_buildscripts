using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using BMC.CoreLib.Diagnostics;
using BMC.CoreLib.Concurrent;
using BMC.Common;
namespace BMC.CoreLib.Win32
{
    public partial class AsyncConsoleDialogForm : Form, IAsyncProgress2
    {
        private const int CP_NOCLOSE_BUTTON = 0x200;

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

        private delegate void EnableButtonHandler();
        private EnableButtonHandler _enableButton = null;

        private IAsyncProgressTimer _elapsedTimer = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="AxAsyncProgress"/> class.
        /// </summary>
        public AsyncConsoleDialogForm(string formCaption, IExecutorService executorService,
            int minimum, int maximum,
            AsyncWaitCallback callback,
            AsyncWaitCallback finishedCallback,
            AsyncWaitCallback abortCallback)
        {
            InitializeComponent();
            SetTagProperty();
            this.ResolveResources();
            _elapsedTimer = AsyncProgressTimerFactory.Create();
            _executorService = executorService;
            _actualCallback = new WaitCallback(this.ExecAsync);
            _updateStatus = new UpdateAsyncStatusHandler(this.UpdateStatus);
            _updateStatusProgress = new UpdateAsyncStatusProgressHandler(this.UpdateStatusProgress);
            _enableButton = new EnableButtonHandler(this.EnableOKButton);
            this.InitializeProgress(minimum, maximum);

            btnCancel.Visible = (_executorService != null);
            if (_executorService != null)
            {
                //tblContainer.RowStyles[2].Height += tblContainer.RowStyles[3].Height;
                tblContainer.RowStyles[3].Height = 0;
            }
            else
            {
                IExecutorService2 executor2 = _executorService as IExecutorService2;
                if (executor2 != null)
                {
                    executor2.Reset();
                }
            }

            this.Text = formCaption;
            this.Initialize(callback, finishedCallback, abortCallback, this);
        }

        /// <summary>
        /// Assigns the Resource Key names to the controls--Created by kishore sivagnanam
        /// </summary>
        public void SetTagProperty()
        {
            this.btnCancel.Tag = "Key_CancelCaption";
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
            Form ownerForm)
        {
            _elapsedTimer.StopAndStart();
            _callback = callback;
            _finishedCallback = finishedCallback;
            _abortCallback = abortCallback;
            this.OwnerForm = ownerForm;
            //Log.WriteToExternalLog += new WriteToExternalLogHandler(Log_WriteToExternalLog);
            //Log.LogFormatMessage += new LogFormatMessageHandler(Log_LogFormatMessage);
        }

        void Log_LogFormatMessage(LogEntryType logType, string dateTime, string moduleName, string procedureName, string message)
        {
            this.UpdateStatus(message);
        }

        /// <summary>
        /// Log_s the write to external log.
        /// </summary>
        /// <param name="formattedMessage">The formatted message.</param>
        /// <param name="type">The type.</param>
        /// <param name="extra">The extra.</param>
        void Log_WriteToExternalLog(string formattedMessage, LogEntryType type, object extra)
        {

        }

        /// <summary>
        /// Enables the OK button.
        /// </summary>
        private void EnableOKButton()
        {
            if (!this.IsDisposed)
            {
                btnCancel.Enabled = true;
            }
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
                _workerThread.Abort();
                _workerThread = null;
            }
        }

        /// <summary>
        /// Execs the async.
        /// </summary>
        /// <param name="state">The state.</param>
        private void ExecAsync(object state)
        {
            ModuleProc PROC = new ModuleProc("AsyncConsoleDialogForm", "ExecAsync");
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
                                this.Close();
                            }
                        }
                        else
                        {
                            this.EnableOKButton();
                        }
                    }), null);
                }
                catch { }
                finally
                {
                    this.DialogResult = result;
                }
            }
        }

        /// <summary>
        /// Gets or sets the owner form.
        /// </summary>
        /// <value>The owner form.</value>
        [Browsable(false)]
        public Form OwnerForm { get; set; }

        #region IAsyncProgress Members

        /// <summary>
        /// Gets the executor service.
        /// </summary>
        /// <value>The executor service.</value>
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
                    this.SetText(statusText);
                    Application.DoEvents();
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
                Form ownerForm = this;
                if (ownerForm.IsDisposed || ownerForm.Disposing) return;
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
                this.CancelClick(btnCancel, EventArgs.Empty);
            }
        }
        #endregion

        /// <summary>
        /// Handles the FormClosing event of the AsyncConsoleDialogForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.FormClosingEventArgs"/> instance containing the event data.</param>
        private void AsyncConsoleDialogForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Log.WriteToExternalLog -= Log_WriteToExternalLog;
            Log.LogFormatMessage += Log_LogFormatMessage;
        }

        /// <summary>
        /// Handles the Load event of the AsyncConsoleDialogForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void AsyncConsoleDialogForm_Load(object sender, EventArgs e)
        {
            this.StartAsync();
        }

        /// <summary>
        /// Handles the Click event of the btnOK control.
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
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        #region IAsyncProgress2 Members

        public void InitializeProgress(int minimum, int maximum)
        {
            try
            {
                _minimum = minimum;
                _maximum = maximum;
                _canUpdateProgress = (minimum > 0 && maximum >= 1);
                this.CrossThreadInvoke(new Action(() =>
                {
                    if (_canUpdateProgress)
                    {
                        pbarStatus.Minimum = minimum;
                        pbarStatus.Maximum = maximum;
                    }
                    pbarStatus.Style = (_canUpdateProgress ? ProgressBarStyle.Blocks : ProgressBarStyle.Marquee);
                }));
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
                    if (value >= _minimum && value < _maximum)
                    {
                        pbarStatus.Value = value;
                    }
                    if (statusText.IsEmpty()) statusText = AsyncProgressMessages.GetMessage(value);
                    this.SetText(statusText + _elapsedTimer.Elapsed);
                    Application.DoEvents();
                }
            }
            catch { }
        }

        private void SetText(string statusText)
        {
            try
            {
                if (statusText.IsEmpty()) return;
                int length = txtMessage.Text.Length + statusText.Length;

                if (length > txtMessage.MaxLength)
                {
                    txtMessage.Clear();
                }

                ConsoleColor consoleColor = (this.ConsoleColor != null ? this.ConsoleColor.Value : Console.ForegroundColor);
                txtMessage.SelectionColor = ConsoleColorMapping.Get(consoleColor);
                txtMessage.SelectionStart = txtMessage.Text.Length;
                txtMessage.SelectionLength = statusText.Length;
                txtMessage.AppendText(statusText);
            }
            catch { }
        }

        /// <summary>
        /// Gets or sets the color of the console.
        /// </summary>
        /// <value>The color of the console.</value>
        public ConsoleColor? ConsoleColor { get; set; }

        #endregion

        private void AsyncConsoleDialogForm_FormClosing_1(object sender, FormClosingEventArgs e)
        {
            //if (e.CloseReason == CloseReason.UserClosing)
            //{
            //    e.Cancel = true;
            //}
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= CP_NOCLOSE_BUTTON;
                return cp;
            }
        }
    }
}
