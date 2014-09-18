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
using System.Drawing.Printing;
using System.Threading;
using System.ComponentModel;
using System.Windows.Threading;

namespace BMC.Presentation.CashDeskManager.UserControls
{
    /// <summary>
    /// Interaction logic for CVoidAutoCancelledTickets.xaml
    /// </summary>
    public partial class CVoidAutoCancelledTickets : UserControl
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

        public CVoidAutoCancelledTickets()
        {
            InitializeComponent();
        }

        public CVoidAutoCancelledTickets(string RouteNumber, string StartDate, string EndDate, string StartTime, string EndTime)
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
            bool bResult = Business.CashDeskManager.Common.ExportToExcel(lvVoidCancelled, "C:\\VoidCancelled.xls");
            if (bResult)
            {
                MessageBox.showBox("Void or Cancelled Tickets Exported Successfully", BMC_Icon.Information);
            }

        }

        void printDoc_PrintPage(object sender, PrintPageEventArgs ev)
        {
            List<TicketExceptions> items = lvVoidCancelled.Items.Cast<TicketExceptions>().ToList<TicketExceptions>();
            PrintUtility util = new PrintUtility(items);
            util.GetCommonPrintValues(ev);
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            if (lvVoidCancelled.Items.Count > 0)
            {
                //MessageBox.showBox("Printing Started");
                //Thread.Sleep(5000);
                //MessageBox.showBox("Printing Failed");
                PrintDocument printDoc = new PrintDocument();
                printDoc.PrintPage += new PrintPageEventHandler(printDoc_PrintPage);
                printDoc.Print();
            }
            

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

            if (!_worker.IsBusy)
            {
                loadWorker();
            }
        }

        private List<TicketExceptions> LoadVoidCancelledTickets()
        {
            busTreasury = new TreasuryTransactions();
            System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
               {
                   lvVoidCancelled.Items.Clear();
               });
            List<string> lstPositionstoDisplay = busTreasury.FillListOfFilteredPositions(RouteNumber);

            Tickets oTickets = new Tickets();
            oTickets.EndDate = EndDate + " " + EndTime;
            oTickets.StartDate = StartDate + " " + StartTime;
            oTickets.IsLiability = false;
            oTickets.BarCode = "%";
            oTickets.Type = "B";

            System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
            {
                prgVoidcancelled.Value += 50;
            });

            List<TicketExceptions> lstVoidCancelled = busTreasury.GetTicket_VoidnExpired(oTickets, lstPositionstoDisplay);

            if (lstVoidCancelled != null)
            {
                decimal ExceptionTotal = 0;
                TicketExceptions Total = new TicketExceptions();
                Total.PrintDate = "Total";
                foreach (TicketExceptions exep in lstVoidCancelled)
                {
                    ExceptionTotal += (decimal)exep.Value;
                }
                Total.Value = (double)Decimal.Round(ExceptionTotal, 2);
                lstVoidCancelled.Insert(0, Total);

                System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
                {
                    prgVoidcancelled.Value += 50;
                });
            }

            return lstVoidCancelled;
            
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
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

                args.Result = LoadVoidCancelledTickets();
                System.Threading.Thread.Sleep(5);
            };

            _worker.ProgressChanged += new ProgressChangedEventHandler(_worker_ProgressChanged);

            _worker.RunWorkerCompleted += (s, args) =>
            {
                System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
                {
                    lvVoidCancelled.ItemsSource = (List<TicketExceptions>)args.Result;
                });
            };

            _worker.RunWorkerAsync();
        }

        void _worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int i = e.ProgressPercentage;
            System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
            {
                prgVoidcancelled.Value = i;
            });
        }

       
    }

}
