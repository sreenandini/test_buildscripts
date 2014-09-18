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
    /// Interaction logic for CPromoCashableDetails.xaml
    /// </summary>
    public partial class CPromoCashableDetails : UserControl
    {
        #region "Declarations"
        TreasuryTransactions busTreasury;
        string RouteNumber = string.Empty;
        string StartDate = string.Empty;
        string EndDate = string.Empty;
        string StartTime = string.Empty;
        string EndTime = string.Empty;
        BackgroundWorker _worker = null;

        #endregion
        public CPromoCashableDetails()
        {
            InitializeComponent();
        }

        public CPromoCashableDetails(string RouteNumber, string StartDate, string EndDate, string StartTime, string EndTime)
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

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            if (lvPromoCashable.Items.Count > 0)
            {
                MessageBox.showBox("Printing Started");
                Thread.Sleep(5000);
                MessageBox.showBox("Printing Failed");
            }
        }

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            bool bResult = Business.CashDeskManager.Common.ExportToExcel(lvPromoCashable, "C:\\promo.xls");
            if (bResult)
            {
                MessageBox.showBox("Promo Tickets Exported Successfully", BMC_Icon.Information);
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

            if (!_worker.IsBusy)
            {
                loadWorker();
            }
        }

        private List<TicketExceptions> LoadPromoTickets()
        {
            busTreasury = new TreasuryTransactions();
            System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
               {
                   lvPromoCashable.Items.Clear();
               });

            List<string> lstPositionstoDisplay = busTreasury.FillListOfFilteredPositions(RouteNumber);

            TicketsClaimed oTicketsClaimed = new TicketsClaimed();
            oTicketsClaimed.TicketsClaimedFrom = StartDate + " " + StartTime;
            oTicketsClaimed.TicketsClaimedTo = EndDate + " " + EndTime;
            System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
            {
                prgPromo.Value += 50;
            });


            List<TicketExceptions> lstPromoTickets = busTreasury.GetPromoCashableTickets(oTicketsClaimed, lstPositionstoDisplay);

            decimal ExceptionTotal = 0;
            TicketExceptions Total = new TicketExceptions();
            Total.PrintDate = "Total";
            foreach (TicketExceptions exep in lstPromoTickets)
            {
                ExceptionTotal += (decimal)exep.Value;
            }
            Total.Value = (double)Decimal.Round(ExceptionTotal, 2);
            lstPromoTickets.Insert(0, Total);
            System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
            {
                prgPromo.Value += 50;
            });

            return lstPromoTickets;
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

                args.Result = LoadPromoTickets();
                System.Threading.Thread.Sleep(5);
            };

            _worker.ProgressChanged += new ProgressChangedEventHandler(_worker_ProgressChanged);

            _worker.RunWorkerCompleted += (s, args) =>
            {
                System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
                {
                    lvPromoCashable.ItemsSource = (List<TicketExceptions>)args.Result;
                });
            };

            _worker.RunWorkerAsync();
        }

        void _worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int i = e.ProgressPercentage;
            System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
            {
                prgPromo.Value = i;
            });
        }
    }
}
