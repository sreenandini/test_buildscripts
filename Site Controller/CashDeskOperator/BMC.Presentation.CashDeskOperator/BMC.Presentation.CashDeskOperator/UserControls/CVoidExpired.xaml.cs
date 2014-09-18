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
using System.Threading;
using System.ComponentModel;
using System.Windows.Threading;
using BMC.CashDeskOperator.BusinessObjects;
using BMC.CashDeskOperator;
using System.Windows.Forms;
using BMC.Common.Utilities;
using BMC.Common.ExceptionManagement;
using BMC.Presentation.POS.Helper_classes;
using BMC.Common.LogManagement;

namespace BMC.Presentation.CashDeskManager.UserControls
{
    /// <summary>
    /// Interaction logic for CVoidExpired.xaml
    /// </summary>
    public partial class CVoidExpired : IDisposable
    {
        #region "Declarations"
        ICashDeskManager objCashDeskManager = null;
        string RouteNumber = string.Empty;
        DateTime StartDate = DateTime.Now;
        DateTime EndDate = DateTime.Now;
        BackgroundWorker _worker = null;
        bool bViewLoaded = false;
        string CurrencySymbol = string.Empty;
        int UserNo = 0;

        #endregion
        public CVoidExpired()
        {
            InitializeComponent();
        }

        public CVoidExpired(string RouteNumber, DateTime StartDate, DateTime EndDate,int UserNo)
        {
            InitializeComponent();
            _worker = new BackgroundWorker();
            _worker.WorkerReportsProgress = true;
            _worker.WorkerSupportsCancellation = true;

            CurrencySymbol = CurrencySymbol.GetCurrencySymbol();
            this.RouteNumber = RouteNumber;
            this.StartDate = StartDate;
            this.EndDate = EndDate;
            this.UserNo = UserNo;
            // For resizing columns automatically.
            GridView gv = lvVoidExpired.View as GridView;
            if (gv != null)
            {
                foreach (GridViewColumn gvc in gv.Columns)
                {
                    gvc.Width = gvc.ActualWidth;
                    if (gvc.Width == 0.0 || gvc.Width == 0)
                        gvc.Width = 0;
                    else
                        gvc.Width = Double.NaN;
                }
            }        
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
            objCashDeskManager = CashDeskManagerBusinessObject.CreateInstance();

            System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
               {
                   lvVoidExpired.Items.Clear();
               });

            List<string> lstPositionstoDisplay = objCashDeskManager.FillListOfFilteredPositions(RouteNumber);

            Tickets oTickets = new Tickets();
            oTickets.EndDate = EndDate;
            oTickets.StartDate = StartDate;
            oTickets.IsLiability = false;
            oTickets.BarCode = "%";
            oTickets.Type = "D";
            oTickets.UserNo = UserNo;

            System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
            {
                prgVoidExpiredState.Value += 75;
            });

            List<TicketExceptions> lstVoidExpired = objCashDeskManager.GetTicket_VoidnExpired(oTickets, lstPositionstoDisplay);


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
                Total.Amount = CurrencySymbol + "" + Convert.ToDecimal(ExceptionTotal).GetUniversalCurrencyFormat();
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
            try
            {
                btnPrint.IsEnabled = false;
                if (lvVoidExpired.Items.Count > 0)
                {
                    objCashDeskManager.PrintFunction(lvVoidExpired, StartDate, EndDate, true, true, true, true, true, false, true, true, true, "CVOIDEXPIRE");
                }
            }
            finally
            {
                btnPrint.IsEnabled = true;
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
                try
                {
                    System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
                    {
                        lvVoidExpired.ItemsSource = (List<TicketExceptions>)args.Result;
                        prgVoidExpiredState.Visibility = Visibility.Hidden;
                    });
                }
                catch (Exception ex)
                {
                    ExceptionManager.Publish(ex);
                }
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
            try
            {
                btnExport.IsEnabled = false;
                SaveFileDialog fileDialog = null;
                string filepath = string.Empty;
                if (lvVoidExpired != null)
                {
                    fileDialog = new SaveFileDialog();
                    fileDialog.Filter = "Excel Files (*.xls)|*.xls|All Files (*.*) |*.*";
                    fileDialog.ShowDialog();
                    filepath = fileDialog.FileName;
                    if (filepath != "")
                    {

                        bool bResult = objCashDeskManager.ExportToExcel(lvVoidExpired, filepath);
                        if (bResult)
                        {
                            MessageBox.ShowBox("MessageID138", BMC_Icon.Information);
                        }
                        else
                        {
                            MessageBox.ShowBox("MessageID1291");
                        }
                    }
                }
                else
                {
                    MessageBox.ShowBox("MessageID56");
                }
            }
            finally
            {
                btnExport.IsEnabled = true;
            }
        }

        private void lvVoidExpired_LayoutUpdated(object sender, EventArgs e)
        {
          //  Decorator border = VisualTreeHelper.GetChild(lvVoidExpired, 0) as Decorator;//Border is the first child of a Listview

         //   ScrollViewer scroll = border.Child as ScrollViewer;
          //  double height = scroll.ScrollableHeight;
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
                        ((BMC.Presentation.CashDeskManager.UserControls.CVoidExpired)(this)).Loaded -= (this.UserControl_Loaded);
                        this.btnExit.Click -= (this.btnExit_Click);
                        this.lvVoidExpired.LayoutUpdated -= (this.lvVoidExpired_LayoutUpdated);
                        this.btnPrint.Click -= (this.btnPrint_Click);
                        this.btnExport.Click -= (this.btnExport_Click);
                    },
                    (c) =>
                    {
                    });
                    LogManager.WriteLog("|=> CVoidExpired objects are released successfully.", LogManager.enumLogLevel.Info);
                }
                disposed = true;
            }
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="CVoidExpired"/> is reclaimed by garbage collection.
        /// </summary>
        ~CVoidExpired()
        {
            Dispose(false);
        }

        #endregion
    }
}
