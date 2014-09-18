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
using BMC.Business.CashDeskManager;
using BMC.Transport;
using System.Timers;
using System.Threading;
using System.Windows.Threading;
using System.ComponentModel;
using System.Diagnostics;

namespace BMC.Presentation.CashDeskManager.UserControls
{
    /// <summary>
    /// Interaction logic for CDebug.xaml
    /// </summary>
    public partial class CDebug : UserControl
    {
        #region "Declarations"
        TreasuryTransactions busTreasury;
        string RouteNumber = string.Empty;
        string StartDate = string.Empty;
        string EndDate = string.Empty;
        string StartTime = string.Empty;
        string EndTime= string.Empty;
        private DispatcherTimer timer;
        private BackgroundWorker _worker;


        #endregion
        public CDebug()
        {
            InitializeComponent();
        }

        public CDebug(string RouteNumber,string StartDate,string EndDate,string StartTime,string EndTime)
        {
            InitializeComponent();
            _worker = new BackgroundWorker();
            _worker.WorkerReportsProgress = true;
            _worker.WorkerSupportsCancellation = true;
            this.RouteNumber = RouteNumber;
            this.StartDate = StartDate;
            this.EndDate = EndDate;
            this.StartTime = StartTime;
            this.EndTime = EndTime;

        }

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            bool bResult = Business.CashDeskManager.Common.ExportToExcel(lvVoucherStatus, "C:\\VoucherStatus.xls");
            if (bResult)
            {
                MessageBox.showBox("Exported Successfully", BMC_Icon.Information);
            }
        }

        private  List<TicketExceptions> LoadTicketsAll()
        {
            busTreasury = new TreasuryTransactions();

            List<string> lstPositionstoDisplay = busTreasury.FillListOfFilteredPositions(RouteNumber);


            Tickets oTickets = new Tickets();
            oTickets.EndDate = EndDate + " " + EndTime;
            oTickets.StartDate = StartDate + " " + StartTime;
            oTickets.IsLiability = false;
            oTickets.BarCode = "%";
            oTickets.Type = "A";
            System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
             {
                 prgDebugState.Value += 50;
             });
            List<TicketExceptions> lstExceptions = busTreasury.TitoTicketsAll(oTickets, lstPositionstoDisplay);

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
                    Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
                    {
                        lvVoucherStatus.ItemsSource = null;
                        lvVoucherStatus.ItemsSource = (List<TicketExceptions>)args.Result;
                       
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

    }
}
