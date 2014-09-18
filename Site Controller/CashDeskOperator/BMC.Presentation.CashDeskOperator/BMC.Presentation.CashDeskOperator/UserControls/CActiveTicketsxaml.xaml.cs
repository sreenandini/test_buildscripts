using System;
using System.Collections.Generic;
using System.Windows;
using BMC.Transport;
using System.Threading;
using System.ComponentModel;
using System.Windows.Threading;
using BMC.CashDeskOperator.BusinessObjects;
using BMC.CashDeskOperator;
using System.Windows.Forms;
using System.Windows.Controls;
using BMC.Common.Utilities;
using BMC.Common.ExceptionManagement;
using BMC.Presentation.POS.Helper_classes;
using BMC.Common.LogManagement;

namespace BMC.Presentation.CashDeskManager.UserControls
{
    /// <summary>
    /// Interaction logic for CActiveTicketsxaml.xaml
    /// </summary>
    public partial class CActiveTicketsxaml : IDisposable
    {
        #region "Declarations"
        ICashDeskManager objCashDeskManager = null;
        string RouteNumber = string.Empty;
        DateTime StartDate = DateTime.Now;
        DateTime EndDate = DateTime.Now;
        BackgroundWorker _worker = null;
        string CurrencySymbol = string.Empty;
        int UserNo = 0;

        #endregion

        public CActiveTicketsxaml()
        {
            InitializeComponent();
        }


        public CActiveTicketsxaml(string RouteNumber, DateTime StartDate, DateTime EndDate,int UserNo)
        {
            InitializeComponent();
            _worker = new BackgroundWorker();
            _worker.WorkerReportsProgress = true;
            _worker.WorkerSupportsCancellation = true;



            CurrencySymbol=  CurrencySymbol.GetCurrencySymbol();
            this.RouteNumber = RouteNumber;
            this.StartDate = StartDate;
            this.EndDate = EndDate;
            this.UserNo = UserNo;

            // For resizing columns automatically.
            GridView gv = lvActiveTickets.View as GridView;
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
                if (lvActiveTickets != null)
                {
                    fileDialog = new SaveFileDialog();
                    fileDialog.Filter = "Excel Files (*.xls)|*.xls|All Files (*.*) |*.*";
                    fileDialog.ShowDialog();
                    filepath = fileDialog.FileName;
                    if (filepath != "")
                    {

                        bool bResult = objCashDeskManager.ExportToExcel(lvActiveTickets, filepath);
                        if (bResult)
                        {
                            MessageBox.ShowBox("MessageID129", BMC_Icon.Information);
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

        private List<TicketExceptions> LoadActiveTickets()
        {
            objCashDeskManager = CashDeskManagerBusinessObject.CreateInstance();

            System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
               {
                   lvActiveTickets.Items.Clear();
               });

            List<string> lstPositionstoDisplay = objCashDeskManager.FillListOfFilteredPositions(RouteNumber);

            TicketsClaimed oTicketsClaimed = new TicketsClaimed();
            oTicketsClaimed.TicketsClaimedFrom = StartDate;
            oTicketsClaimed.TicketsClaimedTo = EndDate;

            Tickets oTickets = new Tickets();
            oTickets.EndDate = EndDate;
            oTickets.StartDate = StartDate;
            oTickets.IsLiability = false;
            oTickets.BarCode = "%";
            oTickets.Type = "U";
            oTickets.UserNo = UserNo;

            System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
            {
                prgActiveTickets.Value += 50;
            });
            List<TicketExceptions> lstTitoTicketsUnclaimed = objCashDeskManager.TitoTicketsUnclaimed(oTickets, lstPositionstoDisplay);
            if (lstTitoTicketsUnclaimed == null)
            {
                lstTitoTicketsUnclaimed = new List<TicketExceptions>();
            }

            System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
            {
                prgActiveTickets.Value += 50;
            });
            List<TicketExceptions> lstTicketsUnClaimed = objCashDeskManager.TicketsUnclaimed(oTicketsClaimed, lstPositionstoDisplay);

            if (lstTicketsUnClaimed != null)
            {
                foreach (TicketExceptions item in lstTicketsUnClaimed)
                {
                    lstTitoTicketsUnclaimed.Add(item);
                }
            }
            decimal ExceptionTotal = 0;
            TicketExceptions Total = new TicketExceptions();
            Total.PrintDate = string.Empty;
            Total.PrintDate = "Total";
            Total.PayDate = string.Empty;
            foreach (TicketExceptions exep in lstTitoTicketsUnclaimed)
            {
                ExceptionTotal += (decimal)exep.Value;
            }
            Total.Value = (double)Decimal.Round(ExceptionTotal, 2);
            Total.Amount = CurrencySymbol + "" + Convert.ToDecimal(ExceptionTotal).GetUniversalCurrencyFormat();
            lstTitoTicketsUnclaimed.Insert(0, Total);
            System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
            {
                prgActiveTickets.Value += 50;
            });

            return lstTitoTicketsUnclaimed;
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
         {
             try
             {
                 btnPrint.IsEnabled = false;
                 if (lvActiveTickets.Items.Count > 1)
                 {
                     //objCashDeskManager.PrintFunction(lvActiveTickets, StartDate, EndDate);
                     objCashDeskManager.PrintFunction(lvActiveTickets, StartDate, EndDate, true, true, true, true, true, false, true, true, false, "ACTIVE");
                 }
             }
             finally
             {
                 btnPrint.IsEnabled = true;
             }
        }

        private void loadWorker()
        {
            try
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

                    args.Result = LoadActiveTickets();
                    System.Threading.Thread.Sleep(5);
                };

                _worker.ProgressChanged += new ProgressChangedEventHandler(_worker_ProgressChanged);

                _worker.RunWorkerCompleted += (s, args) =>
                {
                    try
                    {
                        System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
                        {
                            lvActiveTickets.ItemsSource = (List<TicketExceptions>)args.Result;
                            ((DataTemplate)FindResource("PrintDate")).RegisterName("PrintDate", (List<TicketExceptions>)args.Result);
                            prgActiveTickets.Visibility = Visibility.Hidden;
                        });
                    }
                    catch (Exception ex)
                    {
                        ExceptionManager.Publish(ex);
                    }
                };

                _worker.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        void _worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int i = e.ProgressPercentage;
            System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
            {
                prgActiveTickets.Value = i;
            });
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
                        ((BMC.Presentation.CashDeskManager.UserControls.CActiveTicketsxaml)(this)).Loaded -= (this.UserControl_Loaded);
                        this.btnExit.Click -= (this.btnExit_Click);
                        this.btnPrint.Click -= (this.btnPrint_Click);
                        this.btnExport.Click -= (this.btnExport_Click);
                    },
                    (c) =>
                    {
                    });
                    LogManager.WriteLog("|=> CActiveTicketsxaml objects are released successfully.", LogManager.enumLogLevel.Info);
                }
                disposed = true;
            }
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="classname"/> is reclaimed by garbage collection.
        /// </summary>
        ~CActiveTicketsxaml()
        {
            Dispose(false);
        }

        #endregion
    }
}
