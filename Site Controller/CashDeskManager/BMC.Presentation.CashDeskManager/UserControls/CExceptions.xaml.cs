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
using Microsoft.Office.Interop.Excel;
using System.Reflection;
using BMC.Common.ExceptionManagement;
using System.ComponentModel;
using System.Windows.Threading;
using System.Threading;

namespace BMC.Presentation.CashDeskManager.UserControls
{
    /// <summary>
    /// Interaction logic for CExceptions.xaml
    /// </summary>
    public partial class CExceptions : UserControl
    {
        #region "Declarations"
        TreasuryTransactions busTreasury;
        string RouteNumber = string.Empty;
        string dtFrom = string.Empty;
        string dtTo = string.Empty;
        string TimeFrom = string.Empty;
        string TimeTo = string.Empty;
        BackgroundWorker _worker = null;
          #endregion
        public CExceptions()
        {
            InitializeComponent();
        }

        public CExceptions(string RouteNumber, string From, string To, string TimeFrom, string TimeTo)
        {
            InitializeComponent();
            _worker = new BackgroundWorker();
            _worker.WorkerReportsProgress = true;
            _worker.WorkerSupportsCancellation = true;
            this.RouteNumber = RouteNumber;
            this.dtFrom = From;
            this.dtTo = To;
            this.TimeFrom = TimeFrom;
            this.TimeTo = TimeTo;
        }

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            bool bResult = Business.CashDeskManager.Common.ExportToExcel(lvExceptions, "C:\\Temp.xls");
            if (bResult)
            {
                MessageBox.showBox("Exception Details Exported Successfully", BMC_Icon.Information);
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (!_worker.IsBusy)
            {
                loadWorker();
            }
        }


        private void lvExceptions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvExceptions.Items.Count > 0)
            {
                TicketExceptions item = (TicketExceptions)lvExceptions.SelectedItem;
                if (item.Type == "IN")
                {
                    btnActivate.Visibility = Visibility.Visible;
                }
                else
                {
                    btnActivate.Visibility = Visibility.Hidden;
                }
            }

        }


        private List<TicketExceptions> LoadExceptions()
        {

            busTreasury = new TreasuryTransactions();
            float ExceptionTotal = 0F;
            System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
              {
                  lvExceptions.Items.Clear();
              });
            List<string> lstPositionstoDisplay = busTreasury.FillListOfFilteredPositions(RouteNumber);
            //FillListOfFilteredPositions
            Tickets oTickets = new Tickets();
            oTickets.EndDate = dtTo + " " + TimeTo;
            oTickets.StartDate = dtFrom + " " + TimeFrom;
            oTickets.IsLiability = false;
            oTickets.BarCode = "%";
            oTickets.Type = "E";
            System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
                     {
                         prgExceptions.Value += 50;
                     });
            List<TicketExceptions> lstExceptions = busTreasury.TITOTicketInExceptions(oTickets, lstPositionstoDisplay);
            System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
            {
                prgExceptions.Value += 50;
            });
            List<TicketExceptions> lstExceptionsOut = busTreasury.TitoTicketOutExceptions(oTickets, lstPositionstoDisplay);

            if (lstExceptionsOut.Count > 0)
            {
                if (lstExceptionsOut[0].bExceptionRecordFound)
                {
                    TicketExceptions Total = new TicketExceptions();
                    Total.Type = "Total";
                    foreach (TicketExceptions exep in lstExceptionsOut)
                    {
                        ExceptionTotal += exep.cExceptionsTotal;
                    }
                    Total.Value = Convert.ToDouble(ExceptionTotal.ToString("###0.00"));
                    lstExceptionsOut.Insert(0, Total);

                    //CreateTotals cExceptionsTotal
                }
            }
            System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
            {
                prgExceptions.Value += 50;
            });
            return lstExceptionsOut;
        }

        private void btnActivate_Click(object sender, RoutedEventArgs e)
        {

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

                args.Result = LoadExceptions();
                System.Threading.Thread.Sleep(5);
            };

            _worker.ProgressChanged += new ProgressChangedEventHandler(_worker_ProgressChanged);

            _worker.RunWorkerCompleted += (s, args) =>
            {
                System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
                {
                    lvExceptions.ItemsSource = (List<TicketExceptions>)args.Result;
                });
            };

            _worker.RunWorkerAsync();
        }

        void _worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int i = e.ProgressPercentage;
            System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
              {
                  prgExceptions.Value = i;
              });
        }
    }
}
