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
using BMC.Transport;
using System.Timers;
using System.Threading;
using System.Windows.Threading;
using System.ComponentModel;
using System.Diagnostics;
using BMC.CashDeskOperator.BusinessObjects;
using BMC.CashDeskOperator;
using System.Windows.Forms;
using BMC.Presentation.POS.Helper_classes;
using BMC.Common.LogManagement;

namespace BMC.Presentation.CashDeskManager.UserControls
{
    /// <summary>
    /// Interaction logic for CDebug.xaml
    /// </summary>
    public partial class CDebug : System.Windows.Controls.UserControl, IDisposable
    {
        #region "Declarations"
        ICashDeskManager objCashDeskManager = null;
        string RouteNumber = string.Empty;
        DateTime StartDate = DateTime.Now;
        DateTime EndDate = DateTime.Now;
        private DispatcherTimer timer;
        private BackgroundWorker _worker;
        int UserNo = 0;


        #endregion
        public CDebug()
        {
            InitializeComponent();
        }

        public CDebug(string RouteNumber,DateTime StartDate,DateTime EndDate,int UserNo)
        {
            InitializeComponent();
            _worker = new BackgroundWorker();
            _worker.WorkerReportsProgress = true;
            _worker.WorkerSupportsCancellation = true;
            this.RouteNumber = RouteNumber;
            this.StartDate = StartDate;
            this.EndDate = EndDate;
            this.UserNo = UserNo;
        }

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
             SaveFileDialog fileDialog = new SaveFileDialog();

             fileDialog.Filter = "Excel Files (*.xls)|*.xls|All Files (*.*) |*.*";
            fileDialog.ShowDialog();
            string filepath = fileDialog.FileName;
            if (filepath != "")
            {
                bool bResult = objCashDeskManager.ExportToExcel(lvVoucherStatus, filepath);
                if (bResult)
                {
                    MessageBox.ShowBox("MessageID130", BMC_Icon.Information);
                }
            }
        }

        private  List<TicketExceptions> LoadTicketsAll()
        {
            objCashDeskManager = CashDeskManagerBusinessObject.CreateInstance();

            List<string> lstPositionstoDisplay = objCashDeskManager.FillListOfFilteredPositions(RouteNumber);


            Tickets oTickets = new Tickets();
            oTickets.EndDate = EndDate;
            oTickets.StartDate = StartDate;
            oTickets.IsLiability = false;
            oTickets.BarCode = "%";
            oTickets.Type = "A";
            oTickets.UserNo = UserNo;
            System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
             {
                 prgDebugState.Value += 50;
             });
            List<TicketExceptions> lstExceptions = objCashDeskManager.TitoTicketsAll(oTickets, lstPositionstoDisplay);

            System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
            {
                prgDebugState.Value += 150;
            });
            return lstExceptions;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (!_worker.IsBusy)
            {
                loadWorker();
            }
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMinutes(2);
            timer.Tick += timer1_Tick;
            timer.Start();
        }

        
        private void loadWorker()
        {
            int count = 1;

            _worker.DoWork += (s, args) =>
                {
                    BackgroundWorker worker = s as BackgroundWorker;
                    worker.ReportProgress(count * 10);

                    if (worker.CancellationPending)
                    {
                        args.Cancel = true;
                        return;
                    }

                    args.Result = LoadTicketsAll();
                    System.Threading.Thread.Sleep(5);
                };

            _worker.ProgressChanged += new ProgressChangedEventHandler(_worker_ProgressChanged);

            _worker.RunWorkerCompleted += (s, args) =>
                {
                    System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
                    {
                        lvVoucherStatus.ItemsSource = null;
                        lvVoucherStatus.ItemsSource = (List<TicketExceptions>)args.Result;
                        prgDebugState.Visibility = Visibility.Hidden;
                       
                     });
                };

            _worker.RunWorkerAsync();
        }

        void _worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int i = e.ProgressPercentage;
            System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
            {
                prgDebugState.Value = i;
            });
        }
       
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            this.timer.Stop();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
           loadWorker();
        }

        #region IDisposable Members

        /// <summary>
        /// Variable used to identity whether this object is already disposed or not.
        /// </summary>
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
                    this.CleanupWPFObjectTopControls((i) =>
                    {
                        // events
                        ((BMC.Presentation.CashDeskManager.UserControls.CDebug)(this)).Loaded -= (this.UserControl_Loaded);
                        this.btnExit.Click -= (this.btnExit_Click);
                        this.btnClose.Click -= (this.btnClose_Click);
                    },
                    (c) =>
                    {
                    });
                    LogManager.WriteLog("|=> CDebug objects are released successfully.", LogManager.enumLogLevel.Info);
                }
                disposed = true;
            }
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="CDebug"/> is reclaimed by garbage collection.
        /// </summary>
        ~CDebug()
        {
            Dispose(false);
        }

        #endregion
    }
}
