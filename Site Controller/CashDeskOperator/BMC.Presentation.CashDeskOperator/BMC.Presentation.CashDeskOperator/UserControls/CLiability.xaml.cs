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
using System.ComponentModel;
using System.Windows.Threading;
using System.Threading;
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
    /// Interaction logic for CLiability.xaml
    /// </summary>
    public partial class CLiability : IDisposable
    {
        #region "Declarations"
        ICashDeskManager objCashDeskManager = null;
        string RouteNumber = string.Empty;
        DateTime StartDate = DateTime.Now;
        DateTime EndDate = DateTime.Now;
        string CurrencySymbol = string.Empty;
        BackgroundWorker _worker = null;
        int UserNo = 0;
        #endregion

        public CLiability()
        {
            InitializeComponent();
        }

        public CLiability(string RouteNumber, DateTime  StartDate, DateTime EndDate,int UserNo)
        {
            InitializeComponent();

            _worker = new BackgroundWorker();
            _worker.WorkerReportsProgress = true;
            _worker.WorkerSupportsCancellation = true;

            this.RouteNumber = RouteNumber;
            this.StartDate = StartDate;
            this.EndDate = EndDate;
            this.UserNo = UserNo;
            // For resizing columns automatically.

            CurrencySymbol = CurrencySymbol.GetCurrencySymbol();
            GridView gv = lvLiability.View as GridView;
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


        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                btnExport.IsEnabled = false;
                SaveFileDialog fileDialog = null;
                string filepath = string.Empty;
                if (lvLiability != null)
                {
                    fileDialog = new SaveFileDialog();
                    fileDialog.Filter = "Excel Files (*.xls)|*.xls|All Files (*.*) |*.*";
                    fileDialog.ShowDialog();
                    filepath = fileDialog.FileName;
                    if (filepath != "")
                    {

                        bool bResult = objCashDeskManager.ExportToExcel(lvLiability, filepath);
                        if (bResult)
                        {
                            MessageBox.ShowBox("MessageID134", BMC_Icon.Information);
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

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

            if (!_worker.IsBusy)
            {
                loadWorker();
            }
        }


        private List<TicketExceptions> LoadLiabilities()
        {
            objCashDeskManager = CashDeskManagerBusinessObject.CreateInstance();
            System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
             {
                 lvLiability.Items.Clear();
             });
            List<string> lstPositionstoDisplay = objCashDeskManager.FillListOfFilteredPositions(RouteNumber);


            TicketsClaimed oTicketsClaimed = new TicketsClaimed();
            oTicketsClaimed.TicketsClaimedFrom = StartDate;
            oTicketsClaimed.TicketsClaimedTo = EndDate;

            Tickets oTickets = new Tickets();
            oTickets.EndDate = EndDate;
            oTickets.StartDate = StartDate;
            oTickets.IsLiability = true;
            oTickets.BarCode = "%";
            oTickets.Type = "C";
            oTickets.UserNo = UserNo;

            System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
            {
                prgLiability.Value += 50;
            });
            List<TicketExceptions> lstTitoTicketsClaimed = objCashDeskManager.TitoTicketsClaimedLiability(oTickets, lstPositionstoDisplay);
            if (lstTitoTicketsClaimed == null)
            {
                lstTitoTicketsClaimed = new List<TicketExceptions>();
            }

            oTickets.Type = "P";

            System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
            {
                prgLiability.Value += 50;
            });
            List<TicketExceptions> lstTitoTicketsPrinted = objCashDeskManager.TitoTicketsPrintedLiability(oTickets, lstPositionstoDisplay);
            //lvLiability.ItemsSource = lstTitoTicketsPrinted;

            if (lstTitoTicketsPrinted != null)
            {
                foreach (TicketExceptions item in lstTitoTicketsPrinted)
                {
                    if (item.VoucherStatus.ToUpper().Trim() != "PP")
                    {
                        lstTitoTicketsClaimed.Add(item);
                    }
                }
            }
            System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
            {
                prgLiability.Value += 50;
            });
            List<TicketExceptions> lstTicketsClaimed = objCashDeskManager.TicketsClaimed(oTicketsClaimed, lstPositionstoDisplay);
            if (lstTicketsClaimed != null)
            {
                foreach (TicketExceptions item in lstTicketsClaimed)
                {
                    if (item.VoucherStatus.ToUpper().Trim() != "PP")
                    {
                        lstTitoTicketsClaimed.Add(item);
                    }
                }
                //lvLiability.ItemsSource = lstTicketsClaimed;
            }
            System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
            {
                prgLiability.Value += 50;
            });
            List<TicketExceptions> lstTicketsPrinted = objCashDeskManager.TicketsPrinted(oTicketsClaimed, lstPositionstoDisplay);
            if (lstTicketsPrinted != null)
            {
                foreach (TicketExceptions item in lstTicketsPrinted)
                {
                    lstTitoTicketsClaimed.Add(item);
                }
                // lvLiability.ItemsSource = lstTicketsPrinted;
            }


            decimal ExceptionTotal = 0;
            TicketExceptions Total = new TicketExceptions();
            Total.PrintDate = "Total";
            foreach (TicketExceptions exep in lstTitoTicketsClaimed)
            {
                if (exep.VoucherStatus.ToUpper().Trim() != "PP")
                {
                    ExceptionTotal += (decimal)exep.Value;
                }
            }
            Total.Value = Convert.ToDouble(ExceptionTotal);
            Total.Amount = CurrencySymbol + "" + Convert.ToDecimal(ExceptionTotal).GetUniversalCurrencyFormat();
             //Total.Amount = ExceptionTotal.ToString();
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
                try
                {
                    System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
                    {
                        lvLiability.ItemsSource = (List<TicketExceptions>)args.Result;
                        prgLiability.Visibility = Visibility.Hidden;
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
                prgLiability.Value = i;
            });
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            objCashDeskManager.CloseExcel();
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
                        ((BMC.Presentation.CashDeskManager.UserControls.CLiability)(this)).Loaded -= (this.UserControl_Loaded);
                        this.btnExit.Click -= (this.btnExit_Click);
                        this.btnExport.Click -= (this.btnExport_Click);
                    },
                    (c) =>
                    {
                    });
                    LogManager.WriteLog("|=> CLiability objects are released successfully.", LogManager.enumLogLevel.Info);
                }
                disposed = true;
            }
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="CLiability"/> is reclaimed by garbage collection.
        /// </summary>
        ~CLiability()
        {
            Dispose(false);
        }

        #endregion

    }
}
