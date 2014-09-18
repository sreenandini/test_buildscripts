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
using System.ComponentModel;
using System.Windows.Threading;
using System.Threading;

namespace BMC.Presentation.CashDeskManager.UserControls
{
    /// <summary>
    /// Interaction logic for CLiability.xaml
    /// </summary>
    public partial class CLiability : UserControl
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

        public CLiability()
        {
            InitializeComponent();
        }

        public CLiability(string RouteNumber, string StartDate, string EndDate, string StartTime, string EndTime)
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
            bool bResult = Business.CashDeskManager.Common.ExportToExcel
                (lvLiability, "C:\\Liability.xls");
            if (bResult)
            {
                MessageBox.showBox("Liability Details Exported Successfully", BMC_Icon.Information);
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

            if (!_worker.IsBusy)
            {
                loadWorker();
            }
        }


        private List<TicketExceptions> LoadLiabilities()
        {
            busTreasury = new TreasuryTransactions();
            System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
             {
                 lvLiability.Items.Clear();
             });
            List<string> lstPositionstoDisplay = busTreasury.FillListOfFilteredPositions(RouteNumber);


            TicketsClaimed oTicketsClaimed = new TicketsClaimed();
            oTicketsClaimed.TicketsClaimedFrom = StartDate + " " + StartTime;
            oTicketsClaimed.TicketsClaimedTo = EndDate + " " + EndTime;

            Tickets oTickets = new Tickets();
            oTickets.EndDate = EndDate + " " + EndTime;
            oTickets.StartDate = StartDate + " " + StartTime;
            oTickets.IsLiability = true;
            oTickets.BarCode = "%";
            oTickets.Type = "C";

            System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
            {
                prgLiability.Value += 50;
            });
            List<TicketExceptions> lstTitoTicketsClaimed = busTreasury.TitoTicketsClaimedLiability(oTickets, lstPositionstoDisplay);
            if (lstTitoTicketsClaimed == null)
            {
                lstTitoTicketsClaimed = new List<TicketExceptions>();
            }

            oTickets.Type = "P";

            System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
            {
                prgLiability.Value += 50;
            });
            List<TicketExceptions> lstTitoTicketsPrinted = busTreasury.TitoTicketsPrintedLiability(oTickets, lstPositionstoDisplay);
            //lvLiability.ItemsSource = lstTitoTicketsPrinted;

            if (lstTitoTicketsPrinted != null)
            {
                foreach (TicketExceptions item in lstTitoTicketsPrinted)
                {
                    lstTitoTicketsClaimed.Add(item);
                }
            }
            System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
            {
                prgLiability.Value += 50;
            });
            List<TicketExceptions> lstTicketsClaimed = busTreasury.TicketsClaimed(oTicketsClaimed, lstPositionstoDisplay);
            if (lstTicketsClaimed != null)
            {
                foreach (TicketExceptions item in lstTicketsClaimed)
                {
                    lstTitoTicketsClaimed.Add(item);
                }
                //lvLiability.ItemsSource = lstTicketsClaimed;
            }
            System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
            {
                prgLiability.Value += 50;
            });
            List<TicketExceptions> lstTicketsPrinted = busTreasury.TicketsPrinted(oTicketsClaimed, lstPositionstoDisplay);
            if (lstTicketsPrinted != null)
            {
                foreach (TicketExceptions item in lstTicketsPrinted)
                {
                    lstTitoTicketsClaimed.Add(item);
                }
                // lvLiability.ItemsSource = lstTicketsPrinted;
            }


            float ExceptionTotal = 0F;
            TicketExceptions Total = new TicketExceptions();
            Total.PrintDate = "Total";
            foreach (TicketExceptions exep in lstTitoTicketsClaimed)
            {
                ExceptionTotal += (float)exep.Value;
            }
            Total.Value = ExceptionTotal;
            Total.Amount = ExceptionTotal.ToString();
            lstTitoTicketsClaimed.Insert(0, Total);
            System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
            {
                prgLiability.Value += 50;
            });

            return lstTitoTicketsClaimed;
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

                args.Result = LoadLiabilities();
                System.Threading.Thread.Sleep(5);
            };

            _worker.ProgressChanged += new ProgressChangedEventHandler(_worker_ProgressChanged);

            _worker.RunWorkerCompleted += (s, args) =>
            {
                System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
                {
                    lvLiability.ItemsSource = (List<TicketExceptions>)args.Result;
                });
            };

            _worker.RunWorkerAsync();
        }

        void _worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int i = e.ProgressPercentage;
            System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
            {
                prgLiability.Value = i;
            });
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            Business.CashDeskManager.Common.CloseExcel();
        }


    }
}
