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
using BMC.CashDeskOperator.BusinessObjects;
using BMC.CashDeskOperator;
using CMN = BMC.Common.Utilities;
using BMC.Presentation.POS.Views;
using BMC.Common;
using System.Data;
using BMC.Business.CashDeskOperator;
using BMC.Common.ExceptionManagement;
using BMC.Common.LogManagement;
using BMC.Presentation.POS.Helper_classes;
using System.Data.Linq;
using System.ComponentModel;
//using System.Windows.Forms;
using System.IO;

namespace BMC.Presentation.CashDeskManager.UserControls
{
    /// <summary>
    /// Interaction logic for CashDeskManagerAllDetails.xaml
    /// </summary>
    public partial class CashDeskManagerAllDetails : IDisposable//: UserControl
    {
        #region "Declarations"
        ICashDeskManager objCashDeskManager = null;
        //string TREASURY_REFILL = "Refill";
        //string TREASURY_REFUND = "Refund";
        //string TREASURY_PRIZE_CARD_REFILL = "Prize Card Refill";
        //string TREASURY_HANDPAY_JACKPOT = "Attendantpay Jackpot";
        //string TREASURY_HANDPAY_CREDIT = "Attendantpay Credit";
        //string TREASURY_SHORTPAY = "Shortpay";
        //string TREASURY_CASH_DESK_FLOAT = "Cash Desk Float";
        //string TREASURY_FLOAT = "Float";
        //string TREASURY_PROGRESSIVE = "Prog";
        //string TREASURY_JACKPOT = "Attendantpay Jackpot";
        //string MYFORMAT = "#,##0.00";

        string RouteNumber = string.Empty;
        DateTime StartDate = DateTime.Now;
        DateTime EndDate = DateTime.Now;
        bool isChkHandpays = false;
        bool isChkRefills = false;
        bool ischkRefunds = false;
        bool ischkCashDeskFloat = false;
        bool ischkJackpot = false;
        bool ischkProgressive = false;
        bool ischkShortpays = false;
        bool ischkVoidVoucher = false;
        DataSet dtTickets = new DataSet();


        string sFooterText = string.Empty;
        List<rsp_CDM_GetCashierTransactionsDetails_New> lstCashierTransactionsDetails = null;

        // For Details Print
        bool? chkCashDeskTicketIn, chkCashDeskTicketOut, chkHandpays, chkShortPays, chkVoidVouchers, chkjackpot, chkProghandpays, chkVoidTransactions,
            chkTicketIn, chkTicketOut, chkNCTicketIn, chkNCTicketOut, chkActive, chkVoidCancel, chkExpired, chkException,
                                  chkLiability, chkPromo;

        int user = 0;
        int iRoute_No = 0;

        #endregion
        public CashDeskManagerAllDetails()
        {
            InitializeComponent();
            objCashDeskManager = CashDeskManagerBusinessObject.CreateInstance();
        }

        public CashDeskManagerAllDetails(ISingleResult<rsp_CDM_GetCashierTransactionsDetails_New> lstTickets, DateTime startDate, DateTime endDate, string FooterText,
            bool? chkCashDeskTicketIn, bool? chkCashDeskTicketOut, bool? chkHandpays, bool? chkShortPays, bool? chkVoidVouchers, bool? chkjackpot, bool? chkProghandpays, bool? chkVoidTransactions,
            bool? chkTicketIn, bool? chkTicketOut, bool? chkActive, bool? chkVoidCancel, bool? chkExpired, bool? chkException,
            bool? chkLiability, bool? chkPromo, bool? chkNCTicketIn, bool? chkNCTicketOut, int userNo, int RouteNo)
        {
            try
            {

                InitializeComponent();


                lstCashierTransactionsDetails = lstTickets.ToList();
                this.DataContext = lstCashierTransactionsDetails;
                this.StartDate = startDate;
                this.EndDate = endDate;
                this.sFooterText = FooterText;

                this.chkCashDeskTicketIn = chkCashDeskTicketIn;
                this.chkCashDeskTicketOut = chkCashDeskTicketOut;
                this.chkHandpays = chkHandpays;
                this.chkShortPays = chkShortPays;
                this.chkVoidVouchers = chkVoidVouchers;
                this.chkjackpot = chkjackpot;
                this.chkProghandpays = chkProghandpays;
                this.chkVoidTransactions = chkVoidTransactions;
                this.chkTicketIn = chkTicketIn;
                this.chkTicketOut = chkTicketOut;
                this.chkActive = chkActive;
                this.chkVoidCancel = chkVoidCancel;
                this.chkExpired = chkExpired;
                this.chkException = chkException;
                this.chkLiability = chkLiability;
                this.chkPromo = chkPromo;
                this.chkNCTicketIn = chkNCTicketIn;
                this.chkNCTicketOut = chkNCTicketOut;
                this.user = userNo;
                this.iRoute_No = RouteNo;

                objCashDeskManager = CashDeskManagerBusinessObject.CreateInstance();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        public CashDeskManagerAllDetails(List<rsp_CDM_GetCashierTransactionsDetails_New> lstTickets, DateTime startDate, DateTime endDate, string FooterText, int userNo, int RouteNo)
        {
            try
            {

                InitializeComponent();

                lstCashierTransactionsDetails = lstTickets.ToList();
                dtTickets = lstTickets.ToDataSet<rsp_CDM_GetCashierTransactionsDetails_New>("DetailsView");

                if (!Security.SecurityHelper.HasAccess("BMC.Presentation.CashDeskManager.UserControls.CashDeskManagerAllDetails.lvViewAll.TicketValue"))
                {

                    lstTickets.ForEach((x) => { if (x.Ticket.Trim().Length > 8) x.Ticket = x.Ticket.Trim().Remove(x.Ticket.Trim().Length - 4, 4) + "****"; });

                }

                this.DataContext = lstTickets;
                this.StartDate = startDate;
                this.EndDate = endDate;
                this.sFooterText = FooterText;

                this.user = userNo;
                this.iRoute_No = RouteNo;

                objCashDeskManager = CashDeskManagerBusinessObject.CreateInstance();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        public CashDeskManagerAllDetails(string RouteNumber, DateTime StartDate, DateTime EndDate,
             bool isChkRefills,
            bool ischkRefunds,
            bool ischkCashDeskFloat,
            bool ischkJackpot,
            bool ischkProgressive,
            bool ischkShortpays,
            bool ischkVoidVoucher)
        {
            InitializeComponent();


            this.RouteNumber = RouteNumber;
            this.StartDate = StartDate;
            this.EndDate = EndDate;
            this.isChkRefills = isChkRefills;
            this.ischkRefunds = ischkRefunds;
            this.ischkCashDeskFloat = ischkCashDeskFloat;
            this.ischkJackpot = ischkJackpot;
            this.ischkProgressive = ischkProgressive;
            this.ischkShortpays = ischkShortpays;
            this.ischkVoidVoucher = ischkVoidVoucher;

        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }


        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {


        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bool TicketShow = true;
                if (!Security.SecurityHelper.HasAccess("BMC.Presentation.CashDeskManager.UserControls.CashDeskManagerAllDetails.lvViewAll.TicketValue"))
                {
                    TicketShow = false;
                }
                btnPrint.IsEnabled = false;
                //DataSet _DtDetails = BMC.Common.Utilities.CollectionExtensions.ToDataSet<CashierTransactionsDetails>(lstCashierTransactionsDetails, "DetailsView");

                BMC.Business.CashDeskOperator.Reports objReports = new BMC.Business.CashDeskOperator.Reports();

                //DataSet _DtDetails = objReports.GetCashierTransactionsDetails(true, true, true, true,
                //                                                              true, true, true, true,
                //                                                              true, true,
                //                                                              true, true, true, true,
                //                                                              true, true,
                //                                                              true, true,
                //                                                              StartDate, EndDate, user, iRoute_No);

                if (dtTickets.Tables[0].Rows.Count == 0)
                {
                    MessageBox.ShowBox("MessageID47");
                    return;
                }

                using (CReportViewer objReportViewer = new CReportViewer())
                {
                    objReportViewer.showDetailedReport(dtTickets, StartDate, EndDate, sFooterText, TicketShow);
                    objReportViewer.SetOwner(this);
                    objReportViewer.ShowDialog();
                }
            }
            finally
            {
                btnPrint.IsEnabled = true;
            }


        }

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                btnExport.IsEnabled = false;
                if (lvViewAll != null && lvViewAll.Items.Count > 0)
                { 
                    ExportDatatabletoCSV();                  
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

        internal void ExportDatatabletoCSV()
        {
            try
            {

                string dtExport = "";
                System.Windows.Forms.SaveFileDialog ReportExportDialog = new System.Windows.Forms.SaveFileDialog();
                ReportExportDialog.Filter = "CSV Document *.csv|*.csv|Excel File *.xls|*.xls";
                ReportExportDialog.Title = "Export Report";
                ReportExportDialog.SupportMultiDottedExtensions = true;
                ReportExportDialog.DefaultExt = ".pdf";
                System.Windows.Forms.DialogResult DiaResult = ReportExportDialog.ShowDialog();
                lvViewAll.SelectAllCells();
                lvViewAll.ClipboardCopyMode = (Microsoft.Windows.Controls.DataGridClipboardCopyMode)DataGridClipboardCopyMode.IncludeHeader;
                ApplicationCommands.Copy.Execute(null, lvViewAll);
                if (ReportExportDialog.FilterIndex == 2)//for excel
                {
                    dtExport = (string)Clipboard.GetData(DataFormats.Text);
                    dtExport = dtExport.Replace(',', ' ');
                }
                else
                {
                    dtExport = (string)Clipboard.GetData(DataFormats.CommaSeparatedValue);
                }
                lvViewAll.UnselectAllCells();
                Clipboard.Clear();
              
                string FileName = "";
                if (DiaResult == System.Windows.Forms.DialogResult.OK)
                    FileName = ReportExportDialog.FileName;
                else return;
                File.WriteAllText(FileName, dtExport, Encoding.Default);

            }
            catch (Exception ex)
            {
                LogManager.WriteLog(ex.Message, LogManager.enumLogLevel.Error);
                MessageBox.ShowBox("MessageID56");
            }

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
                        ((BMC.Presentation.CashDeskManager.UserControls.CashDeskManagerAllDetails)(this)).Loaded -= (this.UserControl_Loaded);
                        this.btnExit.Click -= (this.btnExit_Click);
                        this.btnPrint.Click -= (this.btnPrint_Click);
                        this.btnExport.Click -= (this.btnExport_Click);
                    },
                    (c) =>
                    {
                    });
                    this.WriteLog("CashDeskManagerAllDetails objects are released successfully.");

                }
                disposed = true;
            }
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="CashDeskManagerAllDetails"/> is reclaimed by garbage collection.
        /// </summary>
        ~CashDeskManagerAllDetails()
        {
            Dispose(false);
        }

        #endregion
    }
}
