using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.CoreLib.Concurrent;
using BMC.CoreLib;
using BMC.CoreLib.Win32;
using BMC.CoreLib.Services;
using BMC.CoreLib.Diagnostics;
using BMC.Common;
namespace BMC.ExOneService.Hosting
{
    public partial class ServiceMainForm : Form
    {
        private class LogItem
        {
            public string Message { get; set; }
            public BMC.CoreLib.Diagnostics.LogEntryType LogType { get; set; }

        }
        private IServiceHost _serviceHost = null;
        private delegate void SetStatusHandler(string message, LogEntryType logType);
        private SetStatusHandler _setStatus = null;
        private IProducerConsumerQueue<LogItem> _queue = null;

        private Font _boldFont = null;
        private Font _normalFont = null;

        public ServiceMainForm(IExecutorService executorService, IServiceHost serviceHost)
        {
            InitializeComponent();
            SetTagProperty();
            this.ResolveResources();

            _normalFont = this.Font;
            _boldFont = new Font(this.Font, FontStyle.Bold);

            _queue = ProducerConsumerQueueFactory.Create<LogItem>(executorService, -1);
            _queue.Dequeue += OnQueue_Dequeue;

            Log.GlobalWriteToExternalLog += new WriteToExternalLogHandler(Log_WriteToExternalLog);
            _setStatus = new SetStatusHandler(this.SetStatus);

            _serviceHost = serviceHost;
        }

        /// <summary>
        /// Assigns the Resource Key names to the controls--Created by kishore sivagnanam
        /// </summary>
        public void SetTagProperty()
        {
            this.Tag = "Key_MainForm";
        }

        void OnQueue_Dequeue(ServiceMainForm.LogItem item)
        {
            this.CrossThreadInvoke(_setStatus, item.Message, item.LogType);
        }

        void Log_WriteToExternalLog(string formattedMessage, BMC.CoreLib.Diagnostics.LogEntryType type, object extra)
        {
            _queue.Enqueue(new LogItem()
            {
                Message = formattedMessage,
                LogType = type,
            });
        }

        private void SetStatus(string message, LogEntryType logType)
        {
            string textDate = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss.fff");
            if (dgvLog.Rows.Count > 500) dgvLog.Rows.Clear();

            int rowIndex = dgvLog.Rows.Add();
            DataGridViewRow row = dgvLog.Rows[rowIndex];
            row.Cells[0].Value = rowIndex + 1;
            row.Cells[1].Value = textDate;
            row.Cells[2].Value = message;
            dgvLog.FirstDisplayedScrollingRowIndex = rowIndex;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            _serviceHost.Start();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _serviceHost.Stop();
        }
    }
}
