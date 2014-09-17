using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BMC.CoreLib.Win32;
using BMC.CoreLib.Concurrent;
using System.Threading;
using BMC.CoreLib.Diagnostics;
using System.ComponentModel;
using System.Windows.Forms;

namespace BMC.CoreLib.WPF
{
    /// <summary>
    /// Interaction logic for AxWpfAsyncProgress.xaml
    /// </summary>
    public partial class AxWpfAsyncProgress : System.Windows.Controls.UserControl, IAsyncProgress2
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

        private delegate void EnableButtonHandler();
        private EnableButtonHandler _enableButton = null;

        private ColumnDefinition[] _buttonLengths = new ColumnDefinition[] 
        {
            new ColumnDefinition() { Width = new GridLength(120, GridUnitType.Pixel) },
            new ColumnDefinition() { Width = new GridLength(0, GridUnitType.Pixel) },
        };

        /// <summary>
        /// Initializes a new instance of the <see cref="AxAsyncProgress"/> class.
        /// </summary>
        public AxWpfAsyncProgress()
        {
            _actualCallback = new WaitCallback(this.ExecAsync);
            _updateStatus = new UpdateAsyncStatusHandler(this.UpdateStatus);
            _updateStatusProgress = new UpdateAsyncStatusProgressHandler(this.UpdateStatusProgress);
            _enableButton = new EnableButtonHandler(this.EnableOKButton);
            InitializeComponent();
        }

        /// <summary>
        /// Initializes the user control.
        /// </summary>
        /// <param name="callback">The callback.</param>
        /// <param name="finishedCallback">The finished callback.</param>
        /// <param name="abortCallback">The abort callback.</param>
        /// <param name="ownerWindow">The owner form.</param>
        /// <param name="parentForm">The parent form.</param>
        public void Initialize(string formCaption, IExecutorService executorService,
            int minimum, int maximum)
        {
            _executorService = executorService;
            this.InitializeProgress(minimum, maximum);
            lblStatus.Text = "Loading...";

            this.IsCancellable = (_executorService != null);
            if (_executorService != null)
            {
                IExecutorService2 executor2 = _executorService as IExecutorService2;
                if (executor2 != null)
                {
                    executor2.Reset();
                }
            }
        }

        /// <summary>
        /// Initializes the specified callback.
        /// </summary>
        /// <param name="callback">The callback.</param>
        /// <param name="finishedCallback">The finished callback.</param>
        /// <param name="abortCallback">The abort callback.</param>
        /// <param name="ownerWindow">The owner form.</param>
        /// <param name="parentForm">The parent form.</param>
        public void Initialize(string formCaption, IExecutorService executorService,
            int minimum, int maximum,
            AsyncWaitCallback callback,
            AsyncWaitCallback finishedCallback,
            AsyncWaitCallback abortCallback,
            Window ownerWindow,
            bool closeOnComplete)
        {
            this.Initialize(formCaption, executorService, minimum, maximum);
            _callback = callback;
            _finishedCallback = finishedCallback;
            _abortCallback = abortCallback;

            this.OwnerWindow = ownerWindow;
            //Log.LogFormatMessage += new LogFormatMessageHandler(Log_LogFormatMessage);
            this.CloseOnComplete = closeOnComplete;
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
                btnCancel.IsEnabled = true;
            }
        }

        public bool IsDisposed
        {
            get { return this.OwnerWindow.Dispatcher.HasShutdownStarted; }
        }

        /// <summary>
        /// Starts the async.
        /// </summary>
        public void StartAsync()
        {
            if (_workerThread == null)
            {
                _workerThread = Extensions.CreateThreadAndStart(new ParameterizedThreadStart(this.ExecAsync), this, "AxWpfAsyncProgress_");
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

        public void Close()
        {
            this.OwnerWindow.Close();
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
                this.DialogResult = result;
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
            }
        }

        /// <summary>
        /// Gets or sets the owner form.
        /// </summary>
        /// <value>The owner form.</value>
        [Browsable(false)]
        public Window OwnerWindow { get; set; }

        [Browsable(false)]
        public DialogResult DialogResult { get; set; }

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
                //return (btnCancel.Visibility == Visibility.Visible);
                return btnCancel.IsEnabled;
            }
            set
            {
                //btnCancel.Visibility = value ? Visibility.Visible : Visibility.Collapsed;
                btnCancel.IsEnabled = value;
                //LayoutRoot.ColumnDefinitions.RemoveAt(1);
                //LayoutRoot.ColumnDefinitions.Add((value ? _buttonLengths[0] : _buttonLengths[1]));
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
            ApplicationManager.SyncSend((o) =>
            {
                ((AxWpfAsyncProgress)o).lblStatus.Text = statusText;
            }, this);
        }

        /// <summary>
        /// Crosses the thread invoke.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <param name="args">The args.</param>
        public void CrossThreadInvoke(Delegate method, params object[] args)
        {
            //Form ownerWindow = this;
            //if (ownerWindow.IsDisposed || ownerWindow.Disposing) return;
            //ownerWindow.CrossThreadInvoke(method, args);
            ApplicationManager.SyncSend((o) =>
            {
                method.DynamicInvoke(args);
            }, this);
        }

        /// <summary>
        /// Crosses the thread invoke.
        /// </summary>
        /// <param name="action">The action.</param>
        public void CrossThreadInvoke(Action action)
        {
            //this.CrossThreadInvoke((Delegate)action, null);
            ApplicationManager.SyncSend((o) =>
            {
                action();
            }, this);
        }

        /// <summary>
        /// Gets or sets a value indicating whether [is standalone].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [is standalone]; otherwise, <c>false</c>.
        /// </value>
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public bool IsStandalone { get; set; }

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
        /// Handles the FormClosing event of the AsyncDialogForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.FormClosingEventArgs"/> instance containing the event data.</param>
        private void AsyncDialogForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Log.WriteToExternalLog -= Log_WriteToExternalLog;
            Log.LogFormatMessage += Log_LogFormatMessage;
        }

        /// <summary>
        /// Handles the Load event of the AsyncDialogForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void AsyncDialogForm_Load(object sender, EventArgs e)
        {
            this.StartAsync();
        }

        /// <summary>
        /// Handles the Click event of the btnOK control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            ModuleProc PROC = new ModuleProc("", "btnCancel_Click");

            try
            {
                if (this.ExecutorService != null)
                {
                    this.ExecutorService.Shutdown();
                }
                this.OnCancelClick();

                if (!this.IsStandalone)
                {
                    if (this.ExecutorService == null &&
                        !this.CloseOnComplete)
                    {
                        this.DialogResult = DialogResult.Cancel;
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        #region IAsyncProgress2 Members

        public void InitializeProgress(int minimum, int maximum)
        {
            _minimum = minimum;
            _maximum = maximum;
            _canUpdateProgress = (minimum > 0 && maximum >= 1);
            this.CrossThreadInvoke(new Action(() =>
            {
                if (_canUpdateProgress)
                {
                    prgStatus.Minimum = minimum;
                    prgStatus.Maximum = maximum;
                }
                prgStatus.IsIndeterminate = (!_canUpdateProgress);
            }));
        }

        public void UpdateStatusObject(object status)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Updates the status progress.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="statusText">The status text.</param>
        public void UpdateStatusProgress(int value, string statusText)
        {
            ApplicationManager.SyncSend((o) =>
            {
                AxWpfAsyncProgress pThis = (AxWpfAsyncProgress)o;
                if (value >= _minimum && value < _maximum)
                {
                    pThis.prgStatus.Value = value;
                }
                pThis.lblStatus.Text = statusText;
            }, this);
        }

        /// <summary>
        /// Gets or sets the color of the console.
        /// </summary>
        /// <value>The color of the console.</value>
        public ConsoleColor? ConsoleColor { get; set; }

        #endregion
    }
}
