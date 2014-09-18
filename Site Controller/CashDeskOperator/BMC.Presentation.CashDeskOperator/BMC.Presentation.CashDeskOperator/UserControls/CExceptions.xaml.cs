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
using Microsoft.Office.Interop.Excel;
using System.Reflection;
using BMC.Common.ExceptionManagement;
using System.ComponentModel;
using System.Windows.Threading;
using System.Threading;
using BMC.CashDeskOperator.BusinessObjects;
using BMC.CashDeskOperator;
using System.Windows.Forms;
using BMC.Common.Utilities;
using BMC.Presentation.POS.Helper_classes;
using BMC.Common.LogManagement;

namespace BMC.Presentation.CashDeskManager.UserControls 
{
    /// <summary>
    /// Interaction logic for CExceptions.xaml
    /// </summary>
    public partial class CExceptions : IDisposable
    {
        #region "Declarations"
        ICashDeskManager objCashDeskManager = null;
        string RouteNumber = string.Empty;
        DateTime dtFrom = DateTime.Now;
        DateTime dtTo = DateTime.Now;
        BackgroundWorker _worker = null;
        Grid pnlContent = null;
        string CurrencySymbol = string.Empty;
        int UserNo = 0;
          #endregion
        public CExceptions()
        {
            InitializeComponent();
        }

        public CExceptions(string RouteNumber, DateTime From, DateTime To,int UserNo)
        {
            InitializeComponent();
            objCashDeskManager = CashDeskManagerBusinessObject.CreateInstance();
            _worker = new BackgroundWorker();

            CurrencySymbol = CurrencySymbol.GetCurrencySymbol();
            _worker.WorkerReportsProgress = true;
            _worker.WorkerSupportsCancellation = true;
            this.RouteNumber = RouteNumber;
            this.dtFrom = From;
            this.dtTo = To;
            this.UserNo = UserNo;

            // For resizing columns automatically.
            GridView gv = lvExceptions.View as GridView;
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
        public CExceptions(string RouteNumber, DateTime From, DateTime To, Grid pnlContent)
        {
            InitializeComponent();
            objCashDeskManager = CashDeskManagerBusinessObject.CreateInstance();
            _worker = new BackgroundWorker();
            _worker.WorkerReportsProgress = true;
            _worker.WorkerSupportsCancellation = true;
            this.RouteNumber = RouteNumber;
            this.dtFrom = From;
            this.dtTo = To;
            this.pnlContent = pnlContent;

            // For resizing columns automatically.
            GridView gv = lvExceptions.View as GridView;
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
            SaveFileDialog fileDialog = null;
            string filepath = string.Empty;            
            if (lvExceptions != null)
            {
                fileDialog = new SaveFileDialog();
                fileDialog.Filter = "Excel Files (*.xls)|*.xls|All Files (*.*) |*.*";
                fileDialog.ShowDialog();
                filepath = fileDialog.FileName;
                if (filepath != "")
                {

                    bool bResult = objCashDeskManager.ExportToExcel(lvExceptions, filepath);
                    if (bResult)
                    {
                        MessageBox.ShowBox("MessageID131", BMC_Icon.Information);
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

            decimal ExceptionTotal = 0;

           
            List<string> lstPositionstoDisplay = objCashDeskManager.FillListOfFilteredPositions(RouteNumber);
            //FillListOfFilteredPositions
            Tickets oTickets = new Tickets();
            oTickets.EndDate = dtTo;
            oTickets.StartDate = dtFrom;
            oTickets.IsLiability = false;
            oTickets.BarCode = "%";
            oTickets.Type = "E";
            oTickets.UserNo = UserNo;
            
            System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
                     {
                         prgExceptions.Value += 50;
                     });
            List<TicketExceptions> lstExceptions = objCashDeskManager.TITOTicketInExceptions(oTickets, lstPositionstoDisplay);
            if (lstExceptions == null)
            {
                lstExceptions = new List<TicketExceptions>();
                
            }
            System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
            {
                prgExceptions.Value += 50;
            });
            List<TicketExceptions> lstExceptionsOut = objCashDeskManager.TitoTicketOutExceptions(oTickets, lstPositionstoDisplay);



            if (lstExceptionsOut != null)
            {
                foreach (TicketExceptions exep in lstExceptionsOut)
                { 
                        lstExceptions.Add(exep);
                   
                }
            }

            //CreateTotals cExceptionsTotal
            foreach (TicketExceptions item in lstExceptions)
            {
                ExceptionTotal += (decimal) item.currValue;
            }

            TicketExceptions Total = new TicketExceptions();
            Total.Type = "Total";
            Total.Amount = CurrencySymbol + "" + Convert.ToDecimal(ExceptionTotal).GetUniversalCurrencyFormat();
            lstExceptions.Insert(0, Total);

          

            System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
            {
                prgExceptions.Value += 50;
            });
            return lstExceptions;
        }

        private void btnActivate_Click(object sender, RoutedEventArgs e)
        {
            ClearTicketStatus();
        }


        private void ClearTicketStatus()
        {
            string sTicket = string.Empty;
            string sDeviceID = string.Empty;

            TicketExceptions item = (TicketExceptions)lvExceptions.SelectedItem;
            sTicket = item.Ticket;
            sDeviceID = item.DeviceID;

            DialogResult dr = MessageBox.ShowBox("MessageID132",
                BMC_Icon.Information, BMC_Button.YesNo, sTicket);

            if (dr.ToString() == "Yes")
            {
                Dictionary<string, bool> dResult = objCashDeskManager.ActivateSDGTicket(sTicket, sDeviceID, false);

                foreach (KeyValuePair<string, bool> KeyValue in dResult)
                {
                    if ((bool)KeyValue.Value)
                    {
                       List<TicketExceptions> result = LoadExceptions();
                       lvExceptions.ItemsSource = result;
                    }
                    else
                    {
                        MessageBox.ShowBox(KeyValue.Key, BMC_Icon.Information, BMC_Button.OK);
                    }
                }
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

                args.Result = LoadExceptions();
                System.Threading.Thread.Sleep(5);
            };

            _worker.ProgressChanged += new ProgressChangedEventHandler(_worker_ProgressChanged);

            _worker.RunWorkerCompleted += (s, args) =>
            {
                try
                {
                    System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
                    {
                        lvExceptions.ItemsSource = (List<TicketExceptions>)args.Result;
                        prgExceptions.Visibility = Visibility.Hidden;
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
                  prgExceptions.Value = i;
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
                        ((BMC.Presentation.CashDeskManager.UserControls.CExceptions)(this)).Loaded -= (this.UserControl_Loaded);
                        this.btnExit.Click -= (this.btnExit_Click);
                        this.btnExport.Click -= (this.btnExport_Click);
                        this.btnActivate.Click -= (this.btnActivate_Click);
                        this.lvExceptions.SelectionChanged -= (this.lvExceptions_SelectionChanged);
                    },
                    (c) =>
                    {
                    });
                    LogManager.WriteLog("|=> CExceptions objects are released successfully.", LogManager.enumLogLevel.Info);
                }
                disposed = true;
            }
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="CExceptions"/> is reclaimed by garbage collection.
        /// </summary>
        ~CExceptions()
        {
            Dispose(false);
        }

        #endregion
    }
}
