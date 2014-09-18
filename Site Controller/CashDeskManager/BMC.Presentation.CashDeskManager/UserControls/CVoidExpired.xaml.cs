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
using System.Threading;
using System.ComponentModel;
using System.Windows.Threading;

namespace BMC.Presentation.CashDeskManager.UserControls
{
    /// <summary>
    /// Interaction logic for CVoidExpired.xaml
    /// </summary>
    public partial class CVoidExpired : UserControl
    {
        #region "Declarations"
        TreasuryTransactions busTreasury;
        string RouteNumber = string.Empty;
        string StartDate = string.Empty;
        string EndDate = string.Empty;
        string StartTime = string.Empty;
        string EndTime = string.Empty;
        BackgroundWorker _worker = null;
        bool bViewLoaded = false;
        #endregion
        public CVoidExpired()
        {
            InitializeComponent();
        }

        public CVoidExpired(string RouteNumber, string StartDate, string EndDate, string StartTime, string EndTime)
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


        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

            if (!_worker.IsBusy)
            {
                loadWorker();
            }
        }

        private List<TicketExceptions> LoadVoidExpiredTickets()
        {
            busTreasury = new TreasuryTransactions();
            System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
               {
                   lvVoidExpired.Items.Clear();
               });

            List<string> lstPositionstoDisplay = busTreasury.FillListOfFilteredPositions(RouteNumber);

            Tickets oTickets = new Tickets();
            oTickets.EndDate = EndDate + " " + EndTime;
            oTickets.StartDate = StartDate + " " + StartTime;
            oTickets.IsLiability = false;
            oTickets.BarCode = "%";
            oTickets.Type = "D";
            System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
            {
                prgVoidExpiredState.Value += 75;
            });

            List<TicketExceptions> lstVoidExpired = busTreasury.GetTicket_VoidnExpired(oTickets, lstPositionstoDisplay);


            decimal ExceptionTotal = 0;
            TicketExceptions Total = new TicketExceptions();
            Total.PrintDate = "Total";

            if (lstVoidExpired != null)
            {
                foreach (TicketExceptions exep in lstVoidExpired)
                {
                    ExceptionTotal += (decimal)exep.Value;
                }

                Total.Value = (double)Decimal.Round(ExceptionTotal, 2);
                lstVoidExpired.Insert(0, Total);
                System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
                {
                    prgVoidExpiredState.Value += 50;
                });
            }

            return lstVoidExpired;
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            if (lvVoidExpired.Items.Count > 0)
            {
                MessageBox.showBox("Printing Started");
                Thread.Sleep(5000);
                MessageBox.showBox("Printing Failed");
            }
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

                args.Result = LoadVoidExpiredTickets();
                System.Threading.Thread.Sleep(5);
            };

            _worker.ProgressChanged += new ProgressChangedEventHandler(_worker_ProgressChanged);

            _worker.RunWorkerCompleted += (s, args) =>
            {
                System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
                {
                    lvVoidExpired.ItemsSource = (List<TicketExceptions>)args.Result;
                });
            };

            _worker.RunWorkerAsync();
        }

        void _worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int i = e.ProgressPercentage;
            System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
            {
                prgVoidExpiredState.Value = i;
            });
        }

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Dispatcher.BeginInvoke((ThreadStart)delegate
           { lvVoidExpired.Items.Refresh(); }, DispatcherPriority.DataBind, null
           );
            bool bResult = Business.CashDeskManager.Common.ExportToExcel(lvVoidExpired, "C:\\VoidExpired.xls");
            if (bResult)
            {
                MessageBox.showBox("VoidExpired  Exported Successfully", BMC_Icon.Information);
            }
            // Business.CashDeskManager.Common.getContainers(lvVoidExpired);
        }

        private void lvVoidExpired_LayoutUpdated(object sender, EventArgs e)
        {
            Decorator border = VisualTreeHelper.GetChild(lvVoidExpired, 0) as Decorator;//Border is the first child of a Listview

            ScrollViewer scroll = border.Child as ScrollViewer;
            double height = scroll.ScrollableHeight;
        }
    }
}
