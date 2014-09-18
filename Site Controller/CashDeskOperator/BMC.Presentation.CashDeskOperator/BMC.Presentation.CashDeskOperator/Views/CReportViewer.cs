namespace BMC.Presentation.POS.Views
{
    #region Namespaces

    using System;
    using System.Data;
    using System.Windows.Forms;
    using BMC.Presentation.POS.Reports;
    using BMC.Common.ExceptionManagement;
    using BMC.Common.LogManagement;
    using BMC.CashDeskOperator;
    using BMC.Common.Utilities;
    using Audit.BusinessClasses;
    using AuditTransport=Audit.Transport;
    using BMC.CashDeskOperator.BusinessObjects;
    using CrystalDecisions.Shared;
    using System.Data.Common;
    using System.Collections.Generic;
    using BMC.Transport;
    using CrystalDecisions.CrystalReports.Engine;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Threading;
    using System.Globalization;
    using System.Text;
    using System.IO;

    #endregion Namespaces

    #region Class CReportViewer

    public partial class CReportViewer : Form, IDisposable
    {
        #region Private Variables

        private DataSet dsReportsData = null;

        private System.Windows.Forms.PrintDialog ReportsPrintDialog;
        private System.Drawing.Printing.PrintDocument ReportsPrintDocument;
        private string strReportName = string.Empty;
        private string _ExchangeConnectionString = string.Empty;
        private string _TicketingConnectionString = string.Empty;
        #endregion Private Variables

        #region Default Constructor

        public CReportViewer()
        {
            LogManager.WriteLog("Inside Default Constructor CReportViewer", LogManager.enumLogLevel.Info);

            InitializeComponent();
            crystalReportViewer.ShowRefreshButton = false;
            crystalReportViewer.DisplayGroupTree = false;
            crystalReportViewer.ShowGroupTreeButton = false;
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(ExtensionMethods.CurrentCurrenyCulture);
        }
        public CReportViewer(string ExchangeConnectionString, string TicketingConnectionString)
            : this()
        {
            _ExchangeConnectionString = ExchangeConnectionString;
            _TicketingConnectionString = TicketingConnectionString;
        }
        #endregion

        #region Methods

        private void GetVersion_SiteName(out string sVersion, out string sSiteName)
        {
            sVersion = string.Empty;
            sSiteName = string.Empty;

            try
            {
                LogManager.WriteLog("Inside GetVersion_SiteName method", LogManager.enumLogLevel.Info);
                IReports objReports = String.IsNullOrEmpty(_ExchangeConnectionString) ? ReportsBusinessObject.CreateInstance() : ReportsBusinessObject.CreateInstance(_ExchangeConnectionString, _TicketingConnectionString) ;
                objReports.GetVersion_SiteName(out  sVersion, out  sSiteName);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void GetVersion_SiteName(out string sVersion, out string sSiteName,out string SiteCode)
        {
            sVersion = string.Empty;
            sSiteName = string.Empty;
            SiteCode = string.Empty;

            try
            {
                LogManager.WriteLog("Inside GetVersion_SiteName method", LogManager.enumLogLevel.Info);
                IReports objReports = String.IsNullOrEmpty(_ExchangeConnectionString) ? ReportsBusinessObject.CreateInstance() : ReportsBusinessObject.CreateInstance(_ExchangeConnectionString, _TicketingConnectionString);
                objReports.GetVersion_SiteName(out  sVersion, out  sSiteName, out SiteCode);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }


        private void GetVersion_SiteName(out string sVersion, out string sSiteName, string ExchangeConn, string TicketingConn)
        {
            sVersion = string.Empty;
            sSiteName = string.Empty;
            try
            {
                LogManager.WriteLog("Inside GetVersion_SiteName method", LogManager.enumLogLevel.Info);
                IReports objReports = ReportsBusinessObject.CreateInstance(ExchangeConn, TicketingConn);
                objReports.GetVersion_SiteName(out  sVersion, out  sSiteName);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        public void ShowVoucherCouponLiabilityReport(string reportName, DataSet dsReportsDataSet,DateTime issueDate,string sDeviceType,string sVoucherStatus)
        {
            try
            {
                string sVersion, sSiteName;

                var vVoucherCouponLiabilityReport = new VoucherCouponLiabilityReport();
                
                vVoucherCouponLiabilityReport.SetDataSource(dsReportsDataSet);
                vVoucherCouponLiabilityReport.SetParameterValue("issueDate", issueDate);
                vVoucherCouponLiabilityReport.SetParameterValue("DeviceType", sDeviceType);
                vVoucherCouponLiabilityReport.SetParameterValue("VoucherStatus", sVoucherStatus);

                GetVersion_SiteName(out  sVersion, out  sSiteName);

                vVoucherCouponLiabilityReport.SetParameterValue("siteName", sSiteName);
                vVoucherCouponLiabilityReport.SetParameterValue("siteCode", Settings.SiteCode);
                vVoucherCouponLiabilityReport.SetParameterValue("BMCVersion", sVersion);
                vVoucherCouponLiabilityReport.SetParameterValue("CurrencyCulture", ExtensionMethods.CurrentCurrenyCulture);
                vVoucherCouponLiabilityReport.SetParameterValue("CurrencySymbol", ExtensionMethods.CurrentCurrenyCulture.GetCurrencySymbol());
                vVoucherCouponLiabilityReport.SetParameterValue("DateCulture", ExtensionMethods.CurrentDateCulture);
                crystalReportViewer.ReportSource = vVoucherCouponLiabilityReport;
                AuditReports();

                strReportName = "Voucher Coupon Liability Report";
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }


        public void ShowRedeemedTicketByDevice(string reportName, DataSet dsReportsDataSet, DateTime fromdate,DateTime toDate, string DeviceType, string siteCode)
        {
            try
            {
                string sVersion, sSiteName;

                var vRedeemedTicketByDeviceReport = new RedeemedTicketByDeviceReport();

                vRedeemedTicketByDeviceReport.SetDataSource(dsReportsDataSet);
                vRedeemedTicketByDeviceReport.SetParameterValue("fromdate", fromdate);
                vRedeemedTicketByDeviceReport.SetParameterValue("toDate", toDate);
                vRedeemedTicketByDeviceReport.SetParameterValue("DeviceType", DeviceType);

                GetVersion_SiteName(out  sVersion, out  sSiteName);
                vRedeemedTicketByDeviceReport.SetParameterValue("siteName", sSiteName);
                vRedeemedTicketByDeviceReport.SetParameterValue("BMCVersion", sVersion);
                vRedeemedTicketByDeviceReport.SetParameterValue("CurrencyCulture", ExtensionMethods.CurrentCurrenyCulture);
                vRedeemedTicketByDeviceReport.SetParameterValue("CurrencySymbol", ExtensionMethods.CurrentCurrenyCulture.GetCurrencySymbol());
                vRedeemedTicketByDeviceReport.SetParameterValue("DateCulture", ExtensionMethods.CurrentDateCulture);
                vRedeemedTicketByDeviceReport.SetParameterValue("SiteCode", siteCode);
                
                crystalReportViewer.ReportSource = vRedeemedTicketByDeviceReport;
                AuditReports();

                strReportName = "Redeemed Ticket By Device Report";
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }


        public void ShowExpiredVoucherCoupon(string reportName, DataSet dsReportsDataSet, DateTime fromdate, DateTime toDate, string DeviceType)
        {
            try
            {
                string sVersion, sSiteName;

                var vExpiredVoucherCoupon = new ExpiredVoucherCouponReport();

                vExpiredVoucherCoupon.SetDataSource(dsReportsDataSet);
                vExpiredVoucherCoupon.SetParameterValue("fromdate", fromdate);
                vExpiredVoucherCoupon.SetParameterValue("toDate", toDate);
                vExpiredVoucherCoupon.SetParameterValue("DeviceType", DeviceType);

                GetVersion_SiteName(out  sVersion, out  sSiteName);
                vExpiredVoucherCoupon.SetParameterValue("siteName", sSiteName);
                vExpiredVoucherCoupon.SetParameterValue("BMCVersion", sVersion);
                vExpiredVoucherCoupon.SetParameterValue("CurrencyCulture", ExtensionMethods.CurrentCurrenyCulture);
                vExpiredVoucherCoupon.SetParameterValue("CurrencySymbol", ExtensionMethods.CurrentCurrenyCulture.GetCurrencySymbol());
                vExpiredVoucherCoupon.SetParameterValue("DateCulture", ExtensionMethods.CurrentDateCulture);

                crystalReportViewer.ReportSource = vExpiredVoucherCoupon;
                AuditReports();

                strReportName = "Expired Voucher Coupon Report";

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        public void ShowExceptionVoucherDetail(string reportName, DataSet dsReportsDataSet, DateTime fromdate, DateTime toDate, bool? IsDeclaredOnly, Int32 BatchNumber,string SiteCode)
        {
            try
            {
                string sVersion, sSiteName;

                var vExceptionVoucherDetail = new ExceptionVoucherDetails();

                vExceptionVoucherDetail.SetDataSource(dsReportsDataSet);
                vExceptionVoucherDetail.SetParameterValue("fromdate", fromdate);
                vExceptionVoucherDetail.SetParameterValue("toDate", toDate);

                GetVersion_SiteName(out  sVersion, out  sSiteName);
                vExceptionVoucherDetail.SetParameterValue("siteName", sSiteName);
                vExceptionVoucherDetail.SetParameterValue("BMCVersion", sVersion);
                vExceptionVoucherDetail.SetParameterValue("CurrencyCulture", ExtensionMethods.CurrentCurrenyCulture);
                vExceptionVoucherDetail.SetParameterValue("CurrencySymbol", ExtensionMethods.CurrentCurrenyCulture.GetCurrencySymbol());
                vExceptionVoucherDetail.SetParameterValue("DateCulture", ExtensionMethods.CurrentDateCulture);
                vExceptionVoucherDetail.SetParameterValue("IsDeclaredOnly", IsDeclaredOnly.Value ? "Yes" : "No");
                vExceptionVoucherDetail.SetParameterValue("Batch", BatchNumber == 0 ? "All" : Convert.ToString(BatchNumber));
                vExceptionVoucherDetail.SetParameterValue("SiteCode", SiteCode);

                crystalReportViewer.ReportSource = vExceptionVoucherDetail;
                AuditReports();

                strReportName = "Exception Voucher Detail Report";

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        public void ShowExpenseDetailsReport(DataSet dsReportsDataSet, DateTime reportDate, string reportPeriod, bool IsGamingDayBasedReport,string SiteCode)
        {
            string sVersion = string.Empty;
            string sSiteName = string.Empty;
            
            try
            {
                LogManager.WriteLog("Inside ShowExpenseDetailsReport method", LogManager.enumLogLevel.Info);

                ExpenseDetailsReport expenseDetailsReport = new ExpenseDetailsReport();
            
                GetVersion_SiteName(out  sVersion, out  sSiteName);

                LogManager.WriteLog("Setting Report Datasource...", LogManager.enumLogLevel.Info);

                expenseDetailsReport.SetDataSource(dsReportsDataSet);

                LogManager.WriteLog("Report DataSource set successfully", LogManager.enumLogLevel.Info);

                LogManager.WriteLog("Setting Report Parameters...", LogManager.enumLogLevel.Info);

                expenseDetailsReport.SetParameterValue("Site", sSiteName);
                expenseDetailsReport.SetParameterValue("Version", sVersion);
                expenseDetailsReport.SetParameterValue("ReportDate", reportDate);
                expenseDetailsReport.SetParameterValue("ReportPeriod", reportPeriod);
                expenseDetailsReport.SetParameterValue("IsGamingDayBasedReport", IsGamingDayBasedReport);
                expenseDetailsReport.SetParameterValue("CurrencyCulture", ExtensionMethods.CurrentCurrenyCulture);
                expenseDetailsReport.SetParameterValue("DateCulture", ExtensionMethods.CurrentDateCulture);
                expenseDetailsReport.SetParameterValue("CurrencySymbol", ExtensionMethods.CurrentSiteCulture.GetCurrencySymbol());
                expenseDetailsReport.SetParameterValue("CurrencyCulture", ExtensionMethods.CurrentCurrenyCulture, "ExpenseDetailsSummaryReport.rpt");
                expenseDetailsReport.SetParameterValue("CurrencySymbol", ExtensionMethods.CurrentSiteCulture.GetCurrencySymbol(), "ExpenseDetailsSummaryReport.rpt");
                expenseDetailsReport.SetParameterValue("SiteCode", SiteCode);
                LogManager.WriteLog("Report Parameters set successfully", LogManager.enumLogLevel.Info);

                LogManager.WriteLog("Setting Report Source...", LogManager.enumLogLevel.Info);

                crystalReportViewer.ReportSource = expenseDetailsReport;

                LogManager.WriteLog("Report Source set successfully", LogManager.enumLogLevel.Info);

                AuditReports();

                strReportName = "Expense Details Report";

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ShowStackerLevelDetailReport(DataSet dsStackerDetails, int StackerLevel,string siteCode)
        {
            string sVersion = string.Empty;
            string sSiteName = string.Empty;

            try
            {
                LogManager.WriteLog("Inside ShowStackerLevelDetailReport method", LogManager.enumLogLevel.Info);                

                StackerLevelDetailsReport stackerDetailReport = new StackerLevelDetailsReport();

                GetVersion_SiteName(out  sVersion, out  sSiteName);

                LogManager.WriteLog("Setting Report Datasource...", LogManager.enumLogLevel.Info);

                stackerDetailReport.SetDataSource(dsStackerDetails);

                stackerDetailReport.SetParameterValue("SiteName", sSiteName);
                stackerDetailReport.SetParameterValue("Version", sVersion);
                stackerDetailReport.SetParameterValue("@StackerLevel", StackerLevel);
                stackerDetailReport.SetParameterValue("CurrencyCulture", ExtensionMethods.CurrentCurrenyCulture);
                stackerDetailReport.SetParameterValue("DateCulture", ExtensionMethods.CurrentDateCulture);
                stackerDetailReport.SetParameterValue("CurrencySymbol", ExtensionMethods.CurrentSiteCulture.GetCurrencySymbol());
                stackerDetailReport.SetParameterValue("SiteCode", siteCode);
                //stackerDetailReport.SetParameterValue("CurrencyCulture", ExtensionMethods.CurrentCurrenyCulture, "StackerLevelDetailsReport.rpt");
                //stackerDetailReport.SetParameterValue("CurrencySymbol", ExtensionMethods.CurrentSiteCulture.GetCurrencySymbol(), "StackerLevelDetailsReport.rpt");

                CrystalDecisions.CrystalReports.Engine.Database crDatabase;
                CrystalDecisions.CrystalReports.Engine.Tables crTables;

                TableLogOnInfo tableLogonInfo = new TableLogOnInfo();
                ConnectionInfo connectionInfo = new ConnectionInfo();
                DbConnectionDetails.DatabaseName = "EXCHANGE";

                connectionInfo.ServerName = DbConnectionDetails.ServerName;
                connectionInfo.UserID = DbConnectionDetails.UserName;
                connectionInfo.Password = DbConnectionDetails.Password;
                connectionInfo.DatabaseName = DbConnectionDetails.DatabaseName;

                crDatabase = stackerDetailReport.Database;
                crTables = crDatabase.Tables;

                foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
                {
                    tableLogonInfo = crTable.LogOnInfo;
                    tableLogonInfo.ConnectionInfo = connectionInfo;
                    crTable.ApplyLogOnInfo(tableLogonInfo);
                }
                
                crystalReportViewer.ReportSource = stackerDetailReport;

                LogManager.WriteLog("Report Source set successfully", LogManager.enumLogLevel.Info);

                AuditReports();

                strReportName = "Stacker Level Details Report";
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public void ShowJackpotSlipSummaryReport(DataSet dsReportsDataSet, DateTime reportStartDateTime, 
            DateTime reportEndDateTime,bool? ShowHandpay,bool? ShowJackpot)
        {
            string sVersion = string.Empty;
            string sSiteName = string.Empty;
            
            try
            {
                LogManager.WriteLog("Inside ShowJackpotSlipSummaryReport method", LogManager.enumLogLevel.Info);

                JackpotSlipSummaryReport jackpotSlipSummaryReport = new JackpotSlipSummaryReport();

                GetVersion_SiteName(out  sVersion, out  sSiteName);

                LogManager.WriteLog("Setting Report Datasource...", LogManager.enumLogLevel.Info);

                jackpotSlipSummaryReport.SetDataSource(dsReportsDataSet);

                LogManager.WriteLog("Report DataSource set successfully", LogManager.enumLogLevel.Info);

                LogManager.WriteLog("Setting Report Parameters...", LogManager.enumLogLevel.Info);

                jackpotSlipSummaryReport.SetParameterValue("SiteCode", Settings.SiteCode);
                jackpotSlipSummaryReport.SetParameterValue("Site", sSiteName);
                jackpotSlipSummaryReport.SetParameterValue("Version", sVersion);
                jackpotSlipSummaryReport.SetParameterValue("ReportStartDate", reportStartDateTime);
                jackpotSlipSummaryReport.SetParameterValue("ReportEndDate", reportEndDateTime);
                jackpotSlipSummaryReport.SetParameterValue("ShowHandpay", ShowHandpay);
                jackpotSlipSummaryReport.SetParameterValue("ShowJackpot", ShowJackpot);
                jackpotSlipSummaryReport.SetParameterValue("CurrencySymbol", ExtensionMethods.CurrentSiteCulture.GetCurrencySymbol());
                jackpotSlipSummaryReport.SetParameterValue("CurrencyCulture", ExtensionMethods.CurrentCurrenyCulture);
                jackpotSlipSummaryReport.SetParameterValue("DateCulture", ExtensionMethods.CurrentDateCulture);
                
                LogManager.WriteLog("Report Parameters set successfully", LogManager.enumLogLevel.Info);

                LogManager.WriteLog("Setting Report Source...", LogManager.enumLogLevel.Info);

                crystalReportViewer.ReportSource = jackpotSlipSummaryReport;

                LogManager.WriteLog("Report Source set successfully", LogManager.enumLogLevel.Info);

                AuditReports();

                strReportName = "Jackpot SlipSummary Report";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ShowAuditTrailReport(string reportName, DataSet dsReportsDataSet, DateTime fromdate, DateTime toDate, string sModuleName)
        {
            try
            {
                string sVersion, sSiteName;

                var vAuditTrailReport = new AuditTrailReport();

                vAuditTrailReport.SetDataSource(dsReportsDataSet);
                vAuditTrailReport.SetParameterValue("fromdate", fromdate);
                vAuditTrailReport.SetParameterValue("toDate", toDate);
                vAuditTrailReport.SetParameterValue("ModuleName", sModuleName);

                GetVersion_SiteName(out  sVersion, out  sSiteName);
                vAuditTrailReport.SetParameterValue("siteName", sSiteName);
                vAuditTrailReport.SetParameterValue("BMCVersion", sVersion);
                vAuditTrailReport.SetParameterValue("DateCulture", ExtensionMethods.CurrentDateCulture);

                crystalReportViewer.ReportSource = vAuditTrailReport;

                AuditReports();

                strReportName = "Audit Trail Report";
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        public void ShowVoucherListingReport(string reportName, DataSet dsReportsDataSet, DateTime fromdate, DateTime toDate, string sStatus,string sSlot)
        {
            try
            {
                string sVersion, sSiteName;

                var vVoucherListingReport = new VoucherListingReport();

                vVoucherListingReport.SetDataSource(dsReportsDataSet);
                vVoucherListingReport.SetParameterValue("fromDate", fromdate);
                vVoucherListingReport.SetParameterValue("toDate", toDate);
                vVoucherListingReport.SetParameterValue("Status", sStatus);
                vVoucherListingReport.SetParameterValue("Slot", sSlot);

                GetVersion_SiteName(out  sVersion, out  sSiteName);
                vVoucherListingReport.SetParameterValue("siteName", sSiteName);
                vVoucherListingReport.SetParameterValue("BMCVersion", sVersion);
                vVoucherListingReport.SetParameterValue("CurrencyCulture", ExtensionMethods.CurrentCurrenyCulture);
                vVoucherListingReport.SetParameterValue("CurrencySymbol", ExtensionMethods.CurrentCurrenyCulture.GetCurrencySymbol());
                vVoucherListingReport.SetParameterValue("DateCulture", ExtensionMethods.CurrentDateCulture);
                vVoucherListingReport.SetParameterValue("siteCode", Settings.SiteCode);

                crystalReportViewer.ReportSource = vVoucherListingReport;

                AuditReports();

                strReportName = "Voucher Listing Report";
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        //public void ShowSiteIntegorration(string ReportName, DataSet dsReportDataSet)
        //{
        //    try
        //    {

        //        string sVersion, sSiteName;
        //        SiteIntegorrationReport objSiteIntegorrationReport = new SiteIntegorrationReport();
        //        objSiteIntegorrationReport.SetDataSource(dsReportDataSet);
        //        GetVersion_SiteName(out  sVersion, out  sSiteName);
        //        objSiteIntegorrationReport.SetParameterValue("siteName", sSiteName);
        //        objSiteIntegorrationReport.SetParameterValue("BMCVersion", sVersion);
        //        objSiteIntegorrationReport.SetParameterValue("CurrencyCulture", ExtensionMethods.CurrentCurrenyCulture);
        //        objSiteIntegorrationReport.SetParameterValue("CurrencySymbol", ExtensionMethods.CurrentCurrenyCulture.GetCurrencySymbol());
        //        //objSiteIntegorrationReport.SetParameterValue("DateCulture", ExtensionMethods.CurrentDateCulture);
        //        crystalReportViewer.ReportSource = objSiteIntegorrationReport;
        //        // AuditReports();
        //        strReportName = "SiteIntegorrationReport";
        //    }
        //    catch (Exception ex)
        //    {
        //        ExceptionManager.Publish(ex);
        //    }
        //}
        


        public void ShowMeterListReport(DataSet dsReportsDataSet, string assetNo)
        {
            string sVersion = string.Empty;
            string sSiteName = string.Empty;

            try
            {
                LogManager.WriteLog("Inside ShowMeterListReport method", LogManager.enumLogLevel.Info);

                MeterListReport meterListReport = new MeterListReport();

                GetVersion_SiteName(out  sVersion, out  sSiteName);

                LogManager.WriteLog("Setting Report Datasource...", LogManager.enumLogLevel.Info);

                meterListReport.SetDataSource(dsReportsDataSet);

                LogManager.WriteLog("Report DataSource set successfully", LogManager.enumLogLevel.Info);

                LogManager.WriteLog("Setting Report Parameters...", LogManager.enumLogLevel.Info);

                meterListReport.SetParameterValue("Site", sSiteName);
                meterListReport.SetParameterValue("Version", sVersion);
                meterListReport.SetParameterValue("AssetNo", assetNo);
                meterListReport.SetParameterValue("DateCulture", ExtensionMethods.CurrentDateCulture);
                meterListReport.SetParameterValue("CurrencyCulture", ExtensionMethods.CurrentCurrenyCulture);

                LogManager.WriteLog("Report Parameters set successfully", LogManager.enumLogLevel.Info);

                LogManager.WriteLog("Setting Report Source...", LogManager.enumLogLevel.Info);

                crystalReportViewer.ReportSource = meterListReport;

                LogManager.WriteLog("Report Source set successfully", LogManager.enumLogLevel.Info);
                AuditReports();

                strReportName = "Meter List Report";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ShowCashierTransactionsHistoryReport(DataSet dsReportsDataSet, DateTime StartDate, DateTime EndDate, int userNo, string UserName, string sRouteName, int Route_No, string xml)
        {
            string sVersion = string.Empty;
            string sSiteName = string.Empty;

            try
            {
                LogManager.WriteLog("Inside ShowCashierTransactionsReport method", LogManager.enumLogLevel.Info);

                GetVersion_SiteName(out  sVersion, out  sSiteName);
                CashierTransactionReport cashierTransactionReport = new CashierTransactionReport();


                CrystalDecisions.CrystalReports.Engine.Database crDatabase;
                CrystalDecisions.CrystalReports.Engine.Tables crTables;
                TableLogOnInfo tableLogonInfo;

                LogManager.WriteLog("Setting Report Datasource...", LogManager.enumLogLevel.Info);

                cashierTransactionReport.SetDataSource(dsReportsDataSet);

                LogManager.WriteLog("Report DataSource set successfully", LogManager.enumLogLevel.Info);

                LogManager.WriteLog("Setting Report Parameters...", LogManager.enumLogLevel.Info);
                cashierTransactionReport.SetParameterValue("SiteCode", Settings.SiteCode);
                cashierTransactionReport.SetParameterValue("@startdate", StartDate);
                cashierTransactionReport.SetParameterValue("@enddate", EndDate);
                cashierTransactionReport.SetParameterValue("@User", userNo);
                cashierTransactionReport.SetParameterValue("@Route_No", Route_No);
                //cashierTransactionReport.SetParameterValue("DateCulture", ExtensionMethods.CurrentDateCulture);
                cashierTransactionReport.SetParameterValue("Version", sVersion);
                cashierTransactionReport.SetParameterValue("SiteName", sSiteName);
                cashierTransactionReport.SetParameterValue("UserName", UserName);
                cashierTransactionReport.SetParameterValue("RouteName", sRouteName);
                //cashierTransactionReport.SetParameterValue("CurrencyCulture", ExtensionMethods.CurrentCurrenyCulture);
                cashierTransactionReport.SetParameterValue("CurrencySymbol", ExtensionMethods.CurrentCurrenyCulture.GetCurrencySymbol());
                cashierTransactionReport.SetParameterValue("@xml", xml);
                  


                IReports objReports = ReportsBusinessObject.CreateInstance();

                List<ServerDetails> DbConnectionInfo = objReports.GetDataBaseConnectionString();

                ConnectionInfo myConnectionInfo = new ConnectionInfo();


                myConnectionInfo.ServerName = DbConnectionInfo[0].ServerName;  // read this information from config file.
                myConnectionInfo.DatabaseName = "Ticketing"; // read this information from config file.
                myConnectionInfo.UserID = DbConnectionInfo[0].Username;  // read this information from config file.
                myConnectionInfo.Password = DbConnectionInfo[0].Password;  // read this information from config file.


                crDatabase = cashierTransactionReport.Database;
                crTables = crDatabase.Tables;

                foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
                {
                    tableLogonInfo = crTable.LogOnInfo;
                    tableLogonInfo.ConnectionInfo = myConnectionInfo;
                    crTable.ApplyLogOnInfo(tableLogonInfo);
                }


                LogManager.WriteLog("Report Parameters set successfully", LogManager.enumLogLevel.Info);

                LogManager.WriteLog("Setting Report Source...", LogManager.enumLogLevel.Info);

                crystalReportViewer.ReportSource = cashierTransactionReport;

                LogManager.WriteLog("Report Source set successfully", LogManager.enumLogLevel.Info);

                AuditReports();

                strReportName = "Cashier Transaction History Summary";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ShowCashDeskReconcilationReport(DataSet dsReportsDataSet, DateTime StartDate, DateTime EndDate)
        {
            string sVersion = string.Empty;
            string sSiteName = string.Empty;

            try
            {
                LogManager.WriteLog("Inside ShowCashDeskReconcilationReport method", LogManager.enumLogLevel.Info);

                GetVersion_SiteName(out  sVersion, out  sSiteName);
                CashDeskReconicilationReport cashdeskListReport = new CashDeskReconicilationReport();

                LogManager.WriteLog("Setting Report Datasource...", LogManager.enumLogLevel.Info);

                cashdeskListReport.SetDataSource(dsReportsDataSet);

                LogManager.WriteLog("Report DataSource set successfully", LogManager.enumLogLevel.Info);

                LogManager.WriteLog("Setting Report Parameters...", LogManager.enumLogLevel.Info);

                cashdeskListReport.SetParameterValue("@startDate", StartDate);
                cashdeskListReport.SetParameterValue("@endDate", EndDate);
                cashdeskListReport.SetParameterValue("DateCulture", ExtensionMethods.CurrentDateCulture);
                cashdeskListReport.SetParameterValue("Version", sVersion);
                cashdeskListReport.SetParameterValue("CurrencyCulture", ExtensionMethods.CurrentCurrenyCulture);
                cashdeskListReport.SetParameterValue("CurrencySymbol", ExtensionMethods.CurrentCurrenyCulture.GetCurrencySymbol());

                LogManager.WriteLog("Report Parameters set successfully", LogManager.enumLogLevel.Info);

                LogManager.WriteLog("Setting Report Source...", LogManager.enumLogLevel.Info);

                crystalReportViewer.ReportSource = cashdeskListReport;

                LogManager.WriteLog("Report Source set successfully", LogManager.enumLogLevel.Info);

                AuditReports();

                strReportName = "CashDesk Reconcilation Report";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public void ShowCashDeskMovementReport(DataSet dsReportsDataSet, DateTime StartDate, DateTime EndDate)
        {
            string sVersion = string.Empty;
            string sSiteName = string.Empty;

            try
            {
                LogManager.WriteLog("Inside ShowCashDeskMovementReport method", LogManager.enumLogLevel.Info);

                GetVersion_SiteName(out  sVersion, out  sSiteName);
                CashDeskMovement cashdeskMovementReport = new CashDeskMovement();

                LogManager.WriteLog("Setting Report Datasource...", LogManager.enumLogLevel.Info);

                cashdeskMovementReport.SetDataSource(dsReportsDataSet);

                LogManager.WriteLog("Report DataSource set successfully", LogManager.enumLogLevel.Info);

                LogManager.WriteLog("Setting Report Parameters...", LogManager.enumLogLevel.Info);

                cashdeskMovementReport.SetParameterValue("@StartDate", StartDate);
                cashdeskMovementReport.SetParameterValue("@EndDate", EndDate);
                cashdeskMovementReport.SetParameterValue("@Currency", ExtensionMethods.CurrentSiteCulture.GetCurrencySymbol());
                cashdeskMovementReport.SetParameterValue("DateCulture", ExtensionMethods.CurrentDateCulture);
                cashdeskMovementReport.SetParameterValue("Version", sVersion);
                cashdeskMovementReport.SetParameterValue("SiteName", sSiteName);
                cashdeskMovementReport.SetParameterValue("CurrencyCulture", ExtensionMethods.CurrentCurrenyCulture);
                cashdeskMovementReport.SetParameterValue("CurrencySymbol", ExtensionMethods.CurrentCurrenyCulture.GetCurrencySymbol());
                LogManager.WriteLog("Report Parameters set successfully", LogManager.enumLogLevel.Info);


                LogManager.WriteLog("Setting Report Source...", LogManager.enumLogLevel.Info);

                crystalReportViewer.ReportSource = cashdeskMovementReport;

                LogManager.WriteLog("Report Source set successfully", LogManager.enumLogLevel.Info);
                AuditReports();

                strReportName = "CashDesk Movement Report";

              
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ShowTicketAnomaliesReport(DateTime StartDate, DateTime EndDate)
        {
            string sVersion = string.Empty;
            string sSiteName = string.Empty;

            try
            {
                LogManager.WriteLog("Inside ShowTicketAnomaliesReport method", LogManager.enumLogLevel.Info);
                GetVersion_SiteName(out  sVersion, out  sSiteName);

                CrystalDecisions.CrystalReports.Engine.Database crDatabase;
                CrystalDecisions.CrystalReports.Engine.Tables crTables;

                TableLogOnInfo tableLogonInfo = new TableLogOnInfo();
                TicketAnomaliesReport TicketAnomaliesReport = new TicketAnomaliesReport();
                LogManager.WriteLog("Setting Report Datasource...", LogManager.enumLogLevel.Info);
                LogManager.WriteLog("Report DataSource set successfully", LogManager.enumLogLevel.Info);
                LogManager.WriteLog("Setting Report Parameters...", LogManager.enumLogLevel.Info);

                TicketAnomaliesReport.SetParameterValue("@StartDate", StartDate);
                TicketAnomaliesReport.SetParameterValue("@EndDate", EndDate);
                TicketAnomaliesReport.SetParameterValue("@Version", sVersion);
                TicketAnomaliesReport.SetParameterValue("@SiteCode", Settings.SiteCode);
                TicketAnomaliesReport.SetParameterValue("@SiteName", Settings.SiteName);
                TicketAnomaliesReport.SetParameterValue("@CurrencySymbol", ExtensionMethods.CurrentSiteCulture.GetCurrencySymbol());
                TicketAnomaliesReport.SetParameterValue("DateCulture", ExtensionMethods.CurrentDateCulture);

               IReports objReports = String.IsNullOrEmpty(_ExchangeConnectionString) ? ReportsBusinessObject.CreateInstance() : ReportsBusinessObject.CreateInstance(_ExchangeConnectionString, _TicketingConnectionString);
               List<ServerDetails> DbConnectionInfo = String.IsNullOrEmpty(_TicketingConnectionString) ? objReports.GetDataBaseConnectionString() : objReports.GetDataBaseConnectionString(_TicketingConnectionString);
                ConnectionInfo myConnectionInfo = new ConnectionInfo();

                myConnectionInfo.ServerName = DbConnectionInfo[0].ServerName;  // read this information from config file.
                myConnectionInfo.DatabaseName = "Ticketing"; // read this information from config file.
                myConnectionInfo.UserID = DbConnectionInfo[0].Username;  // read this information from config file.
                myConnectionInfo.Password = DbConnectionInfo[0].Password;  // read this information from config file.


                crDatabase = TicketAnomaliesReport.Database;
                crTables = crDatabase.Tables;

                foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
                {
                    tableLogonInfo = crTable.LogOnInfo;
                    tableLogonInfo.ConnectionInfo = myConnectionInfo;
                    crTable.ApplyLogOnInfo(tableLogonInfo);
                }


                LogManager.WriteLog("Report Parameters set successfully", LogManager.enumLogLevel.Info);


                LogManager.WriteLog("Setting Report Source...", LogManager.enumLogLevel.Info);

                crystalReportViewer.ReportSource = TicketAnomaliesReport;


                LogManager.WriteLog("Report Source set successfully", LogManager.enumLogLevel.Info);

               // AuditReports();

                strReportName = "Ticket Anomalies Report";

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                
            }
        }
        public void ShowCashDeskMovementUSReport(DataSet dsReportsDataSet, DateTime StartDate, DateTime EndDate)
        {
            string sVersion = string.Empty;
            string sSiteName = string.Empty;

            try
            {
                LogManager.WriteLog("Inside ShowCashDeskMovementUSReport method", LogManager.enumLogLevel.Info);
                GetVersion_SiteName(out  sVersion, out  sSiteName);

                CashDeskMovementUSReport cashdeskMovementUSReport = new CashDeskMovementUSReport();

                LogManager.WriteLog("Setting Report Datasource...", LogManager.enumLogLevel.Info);

                cashdeskMovementUSReport.SetDataSource(dsReportsDataSet);

                LogManager.WriteLog("Report DataSource set successfully", LogManager.enumLogLevel.Info);

                LogManager.WriteLog("Setting Report Parameters...", LogManager.enumLogLevel.Info);

                cashdeskMovementUSReport.SetParameterValue("@StartDate", StartDate);
                cashdeskMovementUSReport.SetParameterValue("@EndDate", EndDate);
                cashdeskMovementUSReport.SetParameterValue("DateCulture", ExtensionMethods.CurrentDateCulture);
                cashdeskMovementUSReport.SetParameterValue("Version", sVersion);
                cashdeskMovementUSReport.SetParameterValue("SiteName", sSiteName);
                cashdeskMovementUSReport.SetParameterValue("Region", Settings.Region);
                cashdeskMovementUSReport.SetParameterValue("CurrencyCulture", ExtensionMethods.CurrentCurrenyCulture);

                LogManager.WriteLog("Report Parameters set successfully", LogManager.enumLogLevel.Info);

                LogManager.WriteLog("Setting Report Source...", LogManager.enumLogLevel.Info);

                crystalReportViewer.ReportSource = cashdeskMovementUSReport;

                LogManager.WriteLog("Report Source set successfully", LogManager.enumLogLevel.Info);

                AuditReports();

                strReportName = "Cash Desk Movement Report";
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

       


        public void ShowSystemBalancingReport(DataSet dsReportsDataSet, DateTime StartDate, DateTime EndDate)
        {
            string sVersion = string.Empty;
            string sSiteName = string.Empty;

            try
            {
                LogManager.WriteLog("Inside ShowSystemBalancingReport method", LogManager.enumLogLevel.Info);

                GetVersion_SiteName(out  sVersion, out  sSiteName);
                SystemBalancing SystemBalancingReport = new SystemBalancing();

                LogManager.WriteLog("Setting Report Datasource...", LogManager.enumLogLevel.Info);

                SystemBalancingReport.SetDataSource(dsReportsDataSet);

                LogManager.WriteLog("Report DataSource set successfully", LogManager.enumLogLevel.Info);

                LogManager.WriteLog("Setting Report Parameters...", LogManager.enumLogLevel.Info);

                SystemBalancingReport.SetParameterValue("@StartDate", StartDate);
                SystemBalancingReport.SetParameterValue("@EndDate", EndDate);
                SystemBalancingReport.SetParameterValue("DateCulture", ExtensionMethods.CurrentDateCulture);
                SystemBalancingReport.SetParameterValue("Version", sVersion);
                SystemBalancingReport.SetParameterValue("CurrencyCulture", ExtensionMethods.CurrentCurrenyCulture);
                SystemBalancingReport.SetParameterValue("CurrencySymbol", ExtensionMethods.CurrentCurrenyCulture.GetCurrencySymbol());

                LogManager.WriteLog("Report Parameters set successfully", LogManager.enumLogLevel.Info);


                LogManager.WriteLog("Setting Report Source...", LogManager.enumLogLevel.Info);

                crystalReportViewer.ReportSource = SystemBalancingReport;

                LogManager.WriteLog("Report Source set successfully", LogManager.enumLogLevel.Info);

                AuditReports();

                strReportName = "System Balancing Report";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void PrintMeterLifeReport(DataTable meterData)
        {
            string strConnect = string.Empty;

            try
            {

             //   GetVersion_SiteName(out  sVersion, out  sSiteName);

                LogManager.WriteLog("Inside PrintMeterLifeReport method", LogManager.enumLogLevel.Info);                

                MeterLifeReport meterLifeReport = new MeterLifeReport();

                meterLifeReport.SetDataSource(meterData);

                meterLifeReport.SetParameterValue("DateCulture", ExtensionMethods.CurrentDateCulture);

                crystalReportViewer.ReportSource = meterLifeReport;

                LogManager.WriteLog("Report Source set successfully.", LogManager.enumLogLevel.Info);
                
                LogManager.WriteLog("Printing Report...", LogManager.enumLogLevel.Info);

                crystalReportViewer.PrintReport();

                LogManager.WriteLog("Report printed successfully ...", LogManager.enumLogLevel.Info);

                AuditReports();

                strReportName = "Meter Life Report";

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void PrintCurrentDayMeterReport(DataTable meterData)
        {
            string strConnect = string.Empty;
            string sVersion = string.Empty;
            string sSiteName = string.Empty;
            try
            {
                LogManager.WriteLog("Inside PrintCurrentDayMeterReport method", LogManager.enumLogLevel.Info);

                CurrentDayMetersReport currentMetersReport = new CurrentDayMetersReport();

                currentMetersReport.SetDataSource(meterData);

                GetVersion_SiteName(out  sVersion, out  sSiteName);

                currentMetersReport.SetParameterValue("SiteCode", Settings.SiteCode);
                currentMetersReport.SetParameterValue("Site", sSiteName);
                currentMetersReport.SetParameterValue("Version", sVersion);
                currentMetersReport.SetParameterValue("DateCulture", ExtensionMethods.CurrentDateCulture);

                crystalReportViewer.ReportSource = currentMetersReport;

                LogManager.WriteLog("Report Source set successfully.", LogManager.enumLogLevel.Info);

                LogManager.WriteLog("Printing Report...", LogManager.enumLogLevel.Info);

                crystalReportViewer.PrintReport();

                LogManager.WriteLog("Report printed successfully ...", LogManager.enumLogLevel.Info);

                AuditReports();

                strReportName = "Current Day Meter Report";

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion       
    
        #region IDisposable Members

        void IDisposable.Dispose()
        {
            LogManager.WriteLog("Inside Dispose Destructor", LogManager.enumLogLevel.Info);

            if (dsReportsData != null)
            {
                dsReportsData.Dispose();
                dsReportsData = null;
            }
        }

        #endregion

        internal void ShowAFTAuditTrailReport(string p, DataSet dtDataset, DateTime dtFromDate, DateTime dtToDate)
        {
            try
            {
                string sVersion, sSiteName, sitecode;

                var vAFTAuditTrailReport = new AFTAuditTrailReport();

                vAFTAuditTrailReport.SetDataSource(dtDataset);
                vAFTAuditTrailReport.SetParameterValue("fromdate", dtFromDate);
                vAFTAuditTrailReport.SetParameterValue("toDate", dtToDate);

                GetVersion_SiteName(out  sVersion, out  sSiteName, out sitecode);
                vAFTAuditTrailReport.SetParameterValue("siteName", sSiteName);
                vAFTAuditTrailReport.SetParameterValue("BMCVersion", sVersion);
                vAFTAuditTrailReport.SetParameterValue("sitecode", sitecode);

                vAFTAuditTrailReport.SetParameterValue("CurrencyCulture", ExtensionMethods.CurrentCurrenyCulture);
                vAFTAuditTrailReport.SetParameterValue("CurrencySymbol", ExtensionMethods.CurrentCurrenyCulture.GetCurrencySymbol());
                vAFTAuditTrailReport.SetParameterValue("DateCulture", ExtensionMethods.CurrentDateCulture);

                crystalReportViewer.ReportSource = vAFTAuditTrailReport;

                AuditReports();

                strReportName = "AFT Audit Trail Report";
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        internal void ShowReportsPrintTab(DataSet dtPrint, string sReportName, string sSiteCode)
        {
            try
            {
                this.ReportsPrintDialog = new System.Windows.Forms.PrintDialog();
                this.ReportsPrintDocument = new System.Drawing.Printing.PrintDocument();
                string sVersion, sSiteName;

                var vReportsTabPrint = new ReportsTabPrint();
                vReportsTabPrint.SetDataSource(dtPrint);

                GetVersion_SiteName(out  sVersion, out  sSiteName);              
                vReportsTabPrint.SetParameterValue("ReportName", sReportName);
                vReportsTabPrint.SetParameterValue("SiteCode", sSiteCode);
                vReportsTabPrint.SetParameterValue("CurrencyCulture", ExtensionMethods.CurrentCurrenyCulture);
                vReportsTabPrint.SetParameterValue("DateCulture", ExtensionMethods.CurrentDateCulture);
                vReportsTabPrint.SetParameterValue("SiteName", sSiteName);
                vReportsTabPrint.SetParameterValue("Version", sVersion);

                this.ReportsPrintDialog.Document = this.ReportsPrintDocument;
                DialogResult dr = this.ReportsPrintDialog.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    int nCopy = this.ReportsPrintDocument.PrinterSettings.Copies;
                    int sPage = this.ReportsPrintDocument.PrinterSettings.FromPage;
                    int ePage = this.ReportsPrintDocument.PrinterSettings.ToPage;
                    string PrinterName = this.ReportsPrintDocument.PrinterSettings.PrinterName;
               
                    vReportsTabPrint.PrintOptions.PrinterName = PrinterName;
                    vReportsTabPrint.PrintToPrinter(nCopy, false, sPage, ePage);                        
                  
                }

                AuditReports();
               

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        internal void ShowReportsAnalysisDetails(DataTable dtAnalysisDetails, string sReportName, bool isPrint, Decimal TotalAvgBet, string sAnalysisDetails)
        {

            string sVersion, sSiteName;

            try
            {
                this.ReportsPrintDialog = new System.Windows.Forms.PrintDialog();
                this.ReportsPrintDocument = new System.Drawing.Printing.PrintDocument();

                var vReportsAnalysisDetails = new AnalysisDetailsReport();
                vReportsAnalysisDetails.SetDataSource(dtAnalysisDetails);
                vReportsAnalysisDetails.SetParameterValue("@ReportName", sReportName);

                GetVersion_SiteName(out  sVersion, out  sSiteName);

                vReportsAnalysisDetails.SetParameterValue("siteName", sSiteName);
                vReportsAnalysisDetails.SetParameterValue("BMCVersion", sVersion);
                vReportsAnalysisDetails.SetParameterValue("TotalAvgBet", TotalAvgBet);
                vReportsAnalysisDetails.SetParameterValue("CurrencyCulture", ExtensionMethods.CurrentCurrenyCulture);
                vReportsAnalysisDetails.SetParameterValue("CurrencySymbol", ExtensionMethods.CurrentCurrenyCulture.GetCurrencySymbol());
                vReportsAnalysisDetails.SetParameterValue("SiteCode", Settings.SiteCode);
                if (isPrint)
                {
                    this.ReportsPrintDialog.Document = this.ReportsPrintDocument;
                    DialogResult dr = this.ReportsPrintDialog.ShowDialog();

                    if (dr == DialogResult.OK)
                    {
                        int nCopy = this.ReportsPrintDocument.PrinterSettings.Copies;
                        int sPage = this.ReportsPrintDocument.PrinterSettings.FromPage;
                        int ePage = this.ReportsPrintDocument.PrinterSettings.ToPage;
                        string PrinterName = this.ReportsPrintDocument.PrinterSettings.PrinterName;

                        vReportsAnalysisDetails.PrintOptions.PrinterName = PrinterName;
                        vReportsAnalysisDetails.PrintToPrinter(nCopy, false, sPage, ePage);
                    }
                }
                else
                {
                    ExportReports(vReportsAnalysisDetails, sAnalysisDetails);
                }

                AuditReports();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void AuditReports()
        {
            foreach (Control ctrl in crystalReportViewer.Controls)
            {
                if (ctrl.GetType() == typeof(System.Windows.Forms.ToolStrip))
                {
                    ((ToolStrip)(ctrl)).Items[1].Click += new EventHandler(CReportViewer_Click);
                }
            }
        }

      

        private  void CReportViewer_Click(object sender, EventArgs e)
        {

            AuditViewerBusiness.InsertAuditData(new AuditTransport.Audit_History
            {

                AuditModuleName = AuditTransport.ModuleName.PrintReports,
                Audit_Screen_Name = "Reports",
                Audit_Desc = strReportName + " is printed.",
                AuditOperationType = AuditTransport.OperationType.ADD,
                Audit_Field = "Print Date",
                Audit_New_Vl = DateTime.Now.Date.ToString("d")
            });
        }

        public void showDetailedReport(DataSet _DtDetails, DateTime StartDate, DateTime EndDate,string sFooter,bool TicketShow)
        {
            try
            {
                CDMDetailedView vDetailedViewReport = new CDMDetailedView();
                
                //
                vDetailedViewReport.SetDataSource(_DtDetails);
                vDetailedViewReport.SetParameterValue("fromDate", StartDate);
                vDetailedViewReport.SetParameterValue("toDate", EndDate);
                vDetailedViewReport.SetParameterValue("FooterText", sFooter);

                string sVersion, sSiteName;
                GetVersion_SiteName(out  sVersion, out  sSiteName);
                vDetailedViewReport.SetParameterValue("SiteCode", Settings.SiteCode);
                vDetailedViewReport.SetParameterValue("siteName", sSiteName);
                vDetailedViewReport.SetParameterValue("BMCVersion", sVersion);
                vDetailedViewReport.SetParameterValue("CurrencyCulture", ExtensionMethods.CurrentCurrenyCulture);
                vDetailedViewReport.SetParameterValue("CurrencySymbol", ExtensionMethods.CurrentCurrenyCulture.GetCurrencySymbol());
                vDetailedViewReport.SetParameterValue("DateCulture", ExtensionMethods.CurrentDateCulture);
                vDetailedViewReport.SetParameterValue("TicketShow", TicketShow);

                crystalReportViewer.ReportSource = vDetailedViewReport;
                //vDetailedViewReport.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.HTML40, @"D:\Tryy.html");
                //vDetailedViewReport.PrintToPrinter(1, false, 0, 0);
                //MessageBox.Show("Sent to Printer Successfully.");
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        public void ShowLiquidationReportForRead(int? iBatchId, int? iReadId)
        {

            try
            {
                CrystalDecisions.CrystalReports.Engine.Database crDatabase;
                CrystalDecisions.CrystalReports.Engine.Tables crTables;

                TableLogOnInfo tableLogonInfo = new TableLogOnInfo();

                var rptLiquidation = new LiquidationSummary_PS();
                string sVersion, sSiteName;
                GetVersion_SiteName(out  sVersion, out  sSiteName);

                rptLiquidation.SetParameterValue("@CurrencySymbol", ExtensionMethods.CurrentCurrenyCulture.GetCurrencySymbol());
                rptLiquidation.SetParameterValue("@BatchId", iBatchId);
                rptLiquidation.SetParameterValue("@ReadId", iReadId);
                rptLiquidation.SetParameterValue("BMCVersion", sVersion);
                rptLiquidation.SetParameterValue("@BatchId", iBatchId, "Liquidation_Summary_PS_Sub.rpt");
                rptLiquidation.SetParameterValue("@ReadId", iReadId, "Liquidation_Summary_PS_Sub.rpt");
                rptLiquidation.SetParameterValue("DateCulture", ExtensionMethods.CurrentDateCulture);
                rptLiquidation.SetParameterValue("@CurrencySymbol", ExtensionMethods.CurrentCurrenyCulture.GetCurrencySymbol(), "Liquidation_Summary_PS_Sub.rpt");
                rptLiquidation.SetParameterValue("CurrencyCulture", ExtensionMethods.CurrentCurrenyCulture);

                IReports objReports = String.IsNullOrEmpty(_ExchangeConnectionString) ? ReportsBusinessObject.CreateInstance() : ReportsBusinessObject.CreateInstance(_ExchangeConnectionString, _TicketingConnectionString);


                List<ServerDetails> DbConnectionInfo = String.IsNullOrEmpty(_ExchangeConnectionString) ? objReports.GetDataBaseConnectionString() : objReports.GetDataBaseConnectionString(_ExchangeConnectionString);

                ConnectionInfo myConnectionInfo = new ConnectionInfo();


                myConnectionInfo.ServerName = DbConnectionInfo[0].ServerName;  // read this information from config file.
                myConnectionInfo.DatabaseName = DbConnectionInfo[0].DataBase; // read this information from config file.
                myConnectionInfo.UserID = DbConnectionInfo[0].Username;  // read this information from config file.
                myConnectionInfo.Password = DbConnectionInfo[0].Password;  // read this information from config file.


                crDatabase = rptLiquidation.Database;
                crTables = crDatabase.Tables;

                foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
                {
                    tableLogonInfo = crTable.LogOnInfo;
                    tableLogonInfo.ConnectionInfo = myConnectionInfo;
                    crTable.ApplyLogOnInfo(tableLogonInfo);
                }

                crystalReportViewer.ReportSource = rptLiquidation;
                AuditReports();
                strReportName = "Report Liquidation Summary";

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        internal void PrintLiquidationReportForProfitShare()
        {
            try
            {
                CrystalDecisions.CrystalReports.Engine.Database crDatabase;
                CrystalDecisions.CrystalReports.Engine.Tables crTables;

                TableLogOnInfo tableLogonInfo = new TableLogOnInfo();
                ConnectionInfo connectionInfo = new ConnectionInfo();
                DbConnectionDetails.DatabaseName = "Exchange";

                connectionInfo.ServerName = DbConnectionDetails.ServerName;
                connectionInfo.UserID = DbConnectionDetails.UserName;
                connectionInfo.Password = DbConnectionDetails.Password;
                connectionInfo.DatabaseName = DbConnectionDetails.DatabaseName;
                var rLiquidationReportForProfitShare = new LiquidationSummary_PS();

                crDatabase = rLiquidationReportForProfitShare.Database;
                crTables = crDatabase.Tables;

                foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
                {
                    tableLogonInfo = crTable.LogOnInfo;
                    tableLogonInfo.ConnectionInfo = connectionInfo;
                    crTable.ApplyLogOnInfo(tableLogonInfo);
                }


                crystalReportViewer.ReportSource = rLiquidationReportForProfitShare;
                crystalReportViewer.Show();
                crystalReportViewer.PrintReport();
                AuditReports();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        public void ShowLiquidationReport(DataSet dsReportsDataSet, DataSet dsSummaryReport, int BatchNo)
        {
            string sVersion = string.Empty;
            string sSiteName = string.Empty;

            try
            {
                LogManager.WriteLog("Inside ShowLiquidationReport method", LogManager.enumLogLevel.Info);

                GetVersion_SiteName(out  sVersion, out  sSiteName);

                CrystalDecisions.CrystalReports.Engine.Database crDatabase;
                CrystalDecisions.CrystalReports.Engine.Tables crTables;

                TableLogOnInfo tableLogonInfo = new TableLogOnInfo();

                Liquidation LiquidationReport = new Liquidation();
                BMC.Presentation.POS.Reports.LiquidationSummary LiquidationSummaryReport = new BMC.Presentation.POS.Reports.LiquidationSummary();

                LogManager.WriteLog("Setting Report Datasource...", LogManager.enumLogLevel.Info);

                LiquidationReport.SetDataSource(dsReportsDataSet);
                LiquidationSummaryReport.SetDataSource(dsSummaryReport);

                LogManager.WriteLog("Report DataSource set successfully", LogManager.enumLogLevel.Info);

                LogManager.WriteLog("Setting Report Parameters...", LogManager.enumLogLevel.Info);


                LiquidationSummaryReport.SetParameterValue("@Batch_No", BatchNo);
                LiquidationSummaryReport.SetParameterValue("Currency", ExtensionMethods.CurrentSiteCulture.GetCurrencySymbol());
                LiquidationReport.SetParameterValue("@Batch_No", BatchNo);
                LiquidationReport.SetParameterValue("Currency", ExtensionMethods.CurrentSiteCulture.GetCurrencySymbol());
                IReports objReports = String.IsNullOrEmpty(_ExchangeConnectionString) ? ReportsBusinessObject.CreateInstance() : ReportsBusinessObject.CreateInstance(_ExchangeConnectionString, _TicketingConnectionString);

                
                List<ServerDetails> DbConnectionInfo = String.IsNullOrEmpty(_ExchangeConnectionString) ? objReports.GetDataBaseConnectionString() : objReports.GetDataBaseConnectionString(_ExchangeConnectionString);

                ConnectionInfo myConnectionInfo = new ConnectionInfo();


                myConnectionInfo.ServerName = DbConnectionInfo[0].ServerName;  // read this information from config file.
                myConnectionInfo.DatabaseName = DbConnectionInfo[0].DataBase ; // read this information from config file.
                myConnectionInfo.UserID = DbConnectionInfo[0].Username;  // read this information from config file.
                myConnectionInfo.Password = DbConnectionInfo[0].Password;  // read this information from config file.

              
                crDatabase = LiquidationReport.Database;
                crTables = crDatabase.Tables;

                foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
                {
                    tableLogonInfo = crTable.LogOnInfo;
                    tableLogonInfo.ConnectionInfo = myConnectionInfo;
                    crTable.ApplyLogOnInfo(tableLogonInfo);
                }

                crDatabase = LiquidationSummaryReport.Database;
                crTables = crDatabase.Tables;

                foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
                {
                    tableLogonInfo = crTable.LogOnInfo;
                    tableLogonInfo.ConnectionInfo = myConnectionInfo;
                    crTable.ApplyLogOnInfo(tableLogonInfo);
                }

                ReportDocument doc = LiquidationReport.OpenSubreport("LiquidationSummary.rpt");
                doc.SetDataSource(dsSummaryReport);



                LogManager.WriteLog("Report Parameters set successfully", LogManager.enumLogLevel.Info);


                LogManager.WriteLog("Setting Report Source...", LogManager.enumLogLevel.Info);

                
                crystalReportViewer.ReportSource = LiquidationReport;
                

                LogManager.WriteLog("Report Source set successfully", LogManager.enumLogLevel.Info);

                AuditReports();

                strReportName = "System Balancing Report";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //
        public void ShowNonSGVILiquidationReport(DataSet dsReportsDataSet, DataSet dsSummaryReport, int BatchNo)
        {
            string sVersion = string.Empty;
            string sSiteName = string.Empty;

            try
            {
                LogManager.WriteLog("Inside ShowLiquidationReport method", LogManager.enumLogLevel.Info);

                GetVersion_SiteName(out  sVersion, out  sSiteName);

                CrystalDecisions.CrystalReports.Engine.Database crDatabase;
                CrystalDecisions.CrystalReports.Engine.Tables crTables;

                TableLogOnInfo tableLogonInfo = new TableLogOnInfo();

                NonSGVILiquidation LiquidationReport = new NonSGVILiquidation();
                BMC.Presentation.POS.Reports.NonSGVILiquidationSummary LiquidationSummaryReport = new BMC.Presentation.POS.Reports.NonSGVILiquidationSummary();

                LogManager.WriteLog("Setting Report Datasource...", LogManager.enumLogLevel.Info);

                LiquidationReport.SetDataSource(dsReportsDataSet);
                LiquidationSummaryReport.SetDataSource(dsSummaryReport);

                LogManager.WriteLog("Report DataSource set successfully", LogManager.enumLogLevel.Info);

                LogManager.WriteLog("Setting Report Parameters...", LogManager.enumLogLevel.Info);

                LiquidationReport.SetParameterValue("@Batch_No", BatchNo);
                LiquidationSummaryReport.SetParameterValue("@Batch_No", BatchNo);


                IReports objReports = String.IsNullOrEmpty(_ExchangeConnectionString) ? ReportsBusinessObject.CreateInstance() : ReportsBusinessObject.CreateInstance(_ExchangeConnectionString, _TicketingConnectionString);

                List<ServerDetails> DbConnectionInfo = String.IsNullOrEmpty(_ExchangeConnectionString) ? objReports.GetDataBaseConnectionString() : objReports.GetDataBaseConnectionString(_ExchangeConnectionString);

                ConnectionInfo myConnectionInfo = new ConnectionInfo();


                myConnectionInfo.ServerName = DbConnectionInfo[0].ServerName;  // read this information from config file.
                myConnectionInfo.DatabaseName = DbConnectionInfo[0].DataBase; // read this information from config file.
                myConnectionInfo.UserID = DbConnectionInfo[0].Username;  // read this information from config file.
                myConnectionInfo.Password = DbConnectionInfo[0].Password;  // read this information from config file.


                crDatabase = LiquidationReport.Database;
                crTables = crDatabase.Tables;

                foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
                {
                    tableLogonInfo = crTable.LogOnInfo;
                    tableLogonInfo.ConnectionInfo = myConnectionInfo;
                    crTable.ApplyLogOnInfo(tableLogonInfo);
                }

                crDatabase = LiquidationSummaryReport.Database;
                crTables = crDatabase.Tables;

                foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
                {
                    tableLogonInfo = crTable.LogOnInfo;
                    tableLogonInfo.ConnectionInfo = myConnectionInfo;
                    crTable.ApplyLogOnInfo(tableLogonInfo);
                }

                ReportDocument doc = LiquidationReport.OpenSubreport("NonSGVILiquidationSummary.rpt");
                doc.SetDataSource(dsSummaryReport);

                LogManager.WriteLog("Report Parameters set successfully", LogManager.enumLogLevel.Info);


                LogManager.WriteLog("Setting Report Source...", LogManager.enumLogLevel.Info);


                crystalReportViewer.ReportSource = LiquidationReport;


                LogManager.WriteLog("Report Source set successfully", LogManager.enumLogLevel.Info);

                AuditReports();

                strReportName = "System Balancing Report";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //
        public void ShowExceptionSummaryReport( int BatchNo)
        {
            string sVersion = string.Empty;
            string sSiteName = string.Empty;

            try
            {
                LogManager.WriteLog("Inside ShowExceptionSummaryReport method", LogManager.enumLogLevel.Info);

                GetVersion_SiteName(out  sVersion, out  sSiteName);

                CrystalDecisions.CrystalReports.Engine.Database crDatabase;
                CrystalDecisions.CrystalReports.Engine.Tables crTables;

                TableLogOnInfo tableLogonInfo = new TableLogOnInfo();

                ExceptionSummary ExceptionSummaryReport = new ExceptionSummary();

                LogManager.WriteLog("Setting Report Datasource...", LogManager.enumLogLevel.Info);

                LogManager.WriteLog("Report DataSource set successfully", LogManager.enumLogLevel.Info);

                LogManager.WriteLog("Setting Report Parameters...", LogManager.enumLogLevel.Info);
                ExceptionSummaryReport.SetParameterValue("CurrencySymbol", ExtensionMethods.CurrentSiteCulture.GetCurrencySymbol());
                ExceptionSummaryReport.SetParameterValue("BatchNumber", BatchNo);
                ExceptionSummaryReport.SetParameterValue("@Batch_No", BatchNo);
                ExceptionSummaryReport.SetParameterValue("Version", sVersion);

                IReports objReports = ReportsBusinessObject.CreateInstance();

                List<ServerDetails> DbConnectionInfo = objReports.GetDataBaseConnectionString();

                ConnectionInfo myConnectionInfo = new ConnectionInfo();


                myConnectionInfo.ServerName = DbConnectionInfo[0].ServerName;  // read this information from config file.
                myConnectionInfo.DatabaseName = DbConnectionInfo[0].DataBase; // read this information from config file.
                myConnectionInfo.UserID = DbConnectionInfo[0].Username;  // read this information from config file.
                myConnectionInfo.Password = DbConnectionInfo[0].Password;  // read this information from config file.


                crDatabase = ExceptionSummaryReport.Database;
                crTables = crDatabase.Tables;

                foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
                {
                    tableLogonInfo = crTable.LogOnInfo;
                    tableLogonInfo.ConnectionInfo = myConnectionInfo;
                    crTable.ApplyLogOnInfo(tableLogonInfo);
                }


                LogManager.WriteLog("Report Parameters set successfully", LogManager.enumLogLevel.Info);


                LogManager.WriteLog("Setting Report Source...", LogManager.enumLogLevel.Info);

                crystalReportViewer.ReportSource = ExceptionSummaryReport;


                LogManager.WriteLog("Report Source set successfully", LogManager.enumLogLevel.Info);

                AuditReports();

                strReportName = "Exception Summary Report";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ShowExceptionSummaryReport(int BatchNo,string ExchangeConn,string TicketingConn)
        {
            string sVersion = string.Empty;
            string sSiteName = string.Empty;

            try
            {
                LogManager.WriteLog("Inside ShowExceptionSummaryReport method when Common CDO Enlable ", LogManager.enumLogLevel.Info);

                GetVersion_SiteName(out  sVersion, out  sSiteName);

                CrystalDecisions.CrystalReports.Engine.Database crDatabase;
                CrystalDecisions.CrystalReports.Engine.Tables crTables;

                TableLogOnInfo tableLogonInfo = new TableLogOnInfo();

                ExceptionSummary ExceptionSummaryReport = new ExceptionSummary();

                LogManager.WriteLog("Setting Report Datasource...", LogManager.enumLogLevel.Info);

                LogManager.WriteLog("Report DataSource set successfully", LogManager.enumLogLevel.Info);

                LogManager.WriteLog("Setting Report Parameters...", LogManager.enumLogLevel.Info);

                try
                {
                    ExceptionSummaryReport.SetParameterValue("CurrencySymbol", ExtensionMethods.CurrentSiteCulture.GetCurrencySymbol());
                }
                catch 
                {
                    ExceptionSummaryReport.SetParameterValue("CurrencySymbol", "£");
                    LogManager.WriteLog("Exception occurred at CurrencySymbol Set in ExceptionSummary...", LogManager.enumLogLevel.Info);
                }
                ExceptionSummaryReport.SetParameterValue("BatchNumber", BatchNo);
                ExceptionSummaryReport.SetParameterValue("@Batch_No", BatchNo);
                ExceptionSummaryReport.SetParameterValue("Version", sVersion);

                IReports objReports = ReportsBusinessObject.CreateInstance(ExchangeConn, TicketingConn);

                List<ServerDetails> DbConnectionInfo = objReports.GetDataBaseConnectionString( ExchangeConn);

                ConnectionInfo myConnectionInfo = new ConnectionInfo();


                myConnectionInfo.ServerName = DbConnectionInfo[0].ServerName;  // read this information from config file.
                myConnectionInfo.DatabaseName = DbConnectionInfo[0].DataBase; // read this information from config file.
                myConnectionInfo.UserID = DbConnectionInfo[0].Username;  // read this information from config file.
                myConnectionInfo.Password = DbConnectionInfo[0].Password;  // read this information from config file.


                crDatabase = ExceptionSummaryReport.Database;
                crTables = crDatabase.Tables;

                foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
                {
                    tableLogonInfo = crTable.LogOnInfo;
                    tableLogonInfo.ConnectionInfo = myConnectionInfo;
                    crTable.ApplyLogOnInfo(tableLogonInfo);
                }


                LogManager.WriteLog("Report Parameters set successfully", LogManager.enumLogLevel.Info);


                LogManager.WriteLog("Setting Report Source...", LogManager.enumLogLevel.Info);

                crystalReportViewer.ReportSource = ExceptionSummaryReport;


                LogManager.WriteLog("Report Source set successfully", LogManager.enumLogLevel.Info);

                AuditReports();

                strReportName = "Exception Summary Report";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ShowBatchWinLossReport(int BatchNo,int WeekNo)
        {
            string sVersion = string.Empty;
            string sSiteName = string.Empty;

            try
            {
                LogManager.WriteLog("Inside ShowBatchWinLossReport method", LogManager.enumLogLevel.Info);

                GetVersion_SiteName(out  sVersion, out  sSiteName);

                CrystalDecisions.CrystalReports.Engine.Database crDatabase;
                CrystalDecisions.CrystalReports.Engine.Tables crTables;

                TableLogOnInfo tableLogonInfo = new TableLogOnInfo();
                batch_winloss_report BatchWinLossReport = new batch_winloss_report();
                LogManager.WriteLog("Setting Report Datasource...", LogManager.enumLogLevel.Info);
                LogManager.WriteLog("Report DataSource set successfully", LogManager.enumLogLevel.Info);
                LogManager.WriteLog("Setting Report Parameters...", LogManager.enumLogLevel.Info);

                //BatchWinLossReport.SetParameterValue("BatchNo", BatchNo);
                BatchWinLossReport.SetParameterValue("@BatchNo", BatchNo);
                BatchWinLossReport.SetParameterValue("@WeekCollection", WeekNo > 0 ? true : false);
                BatchWinLossReport.SetParameterValue("CurrencySymbol", ExtensionMethods.CurrentSiteCulture.GetCurrencySymbol());
                BatchWinLossReport.SetParameterValue("CurrencyCulture", ExtensionMethods.CurrentCurrenyCulture);
                BatchWinLossReport.SetParameterValue("DateCulture", ExtensionMethods.CurrentDateCulture);
                BatchWinLossReport.SetParameterValue("Version", sVersion);
                //BatchWinLossReport.SetParameterValue("CopyRight", Settings.CopyRightInfo);
                BatchWinLossReport.SetParameterValue("Region", Settings.Region);

                IReports objReports = String.IsNullOrEmpty(_ExchangeConnectionString) ? ReportsBusinessObject.CreateInstance() : ReportsBusinessObject.CreateInstance(_ExchangeConnectionString, _TicketingConnectionString);
            
                List<ServerDetails> DbConnectionInfo = String.IsNullOrEmpty(_ExchangeConnectionString) ? objReports.GetDataBaseConnectionString() : objReports.GetDataBaseConnectionString( BMC.Business.CashDeskOperator.CommonUtilities.SiteConnectionString( _ExchangeConnectionString));

                ConnectionInfo myConnectionInfo = new ConnectionInfo();


                myConnectionInfo.ServerName = DbConnectionInfo[0].ServerName;  // read this information from config file.
                myConnectionInfo.DatabaseName = DbConnectionInfo[0].DataBase; // read this information from config file.
                myConnectionInfo.UserID = DbConnectionInfo[0].Username;  // read this information from config file.
                myConnectionInfo.Password = DbConnectionInfo[0].Password;  // read this information from config file.


                crDatabase = BatchWinLossReport.Database;
                crTables = crDatabase.Tables;

                foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
                {
                    tableLogonInfo = crTable.LogOnInfo;
                    tableLogonInfo.ConnectionInfo = myConnectionInfo;
                    crTable.ApplyLogOnInfo(tableLogonInfo);
            }


                LogManager.WriteLog("Report Parameters set successfully", LogManager.enumLogLevel.Info);


                LogManager.WriteLog("Setting Report Source...", LogManager.enumLogLevel.Info);

                crystalReportViewer.ReportSource = BatchWinLossReport;


                LogManager.WriteLog("Report Source set successfully", LogManager.enumLogLevel.Info);

                AuditReports();

                strReportName = "batch winloss report";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ShowWeeklyWinLossReport(int WeekID)
        {
            string sVersion = string.Empty;
            string sSiteName = string.Empty;

            try
            {
                LogManager.WriteLog("Inside ShowWeeklyExceptionSummary method", LogManager.enumLogLevel.Info);

                GetVersion_SiteName(out  sVersion, out  sSiteName);

                CrystalDecisions.CrystalReports.Engine.Database crDatabase;
                CrystalDecisions.CrystalReports.Engine.Tables crTables;

                TableLogOnInfo tableLogonInfo = new TableLogOnInfo();

                weekly_winloss_report WeeklyWinLossReport = new weekly_winloss_report();

                LogManager.WriteLog("Setting Report Datasource...", LogManager.enumLogLevel.Info);

                LogManager.WriteLog("Report DataSource set successfully", LogManager.enumLogLevel.Info);

                LogManager.WriteLog("Setting Report Parameters...", LogManager.enumLogLevel.Info);

                WeeklyWinLossReport.SetParameterValue("WeekNo", WeekID);
                WeeklyWinLossReport.SetParameterValue("@WeekID", WeekID);
                WeeklyWinLossReport.SetParameterValue("@Currency", ExtensionMethods.CurrentSiteCulture.GetCurrencySymbol());
                WeeklyWinLossReport.SetParameterValue("Version", sVersion);
                WeeklyWinLossReport.SetParameterValue("Client", Settings.Client);

                IReports objReports = ReportsBusinessObject.CreateInstance();

                List<ServerDetails> DbConnectionInfo = objReports.GetDataBaseConnectionString();

                ConnectionInfo myConnectionInfo = new ConnectionInfo();


                myConnectionInfo.ServerName = DbConnectionInfo[0].ServerName;  // read this information from config file.
                myConnectionInfo.DatabaseName = DbConnectionInfo[0].DataBase; // read this information from config file.
                myConnectionInfo.UserID = DbConnectionInfo[0].Username;  // read this information from config file.
                myConnectionInfo.Password = DbConnectionInfo[0].Password;  // read this information from config file.


                crDatabase = WeeklyWinLossReport.Database;
                crTables = crDatabase.Tables;

                foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
                {
                    tableLogonInfo = crTable.LogOnInfo;
                    tableLogonInfo.ConnectionInfo = myConnectionInfo;
                    crTable.ApplyLogOnInfo(tableLogonInfo);
                }


                LogManager.WriteLog("Report Parameters set successfully", LogManager.enumLogLevel.Info);


                LogManager.WriteLog("Setting Report Source...", LogManager.enumLogLevel.Info);

                crystalReportViewer.ReportSource = WeeklyWinLossReport;


                LogManager.WriteLog("Report Source set successfully", LogManager.enumLogLevel.Info);

                AuditReports();

                strReportName = "Weekly WinLoss Report";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public void ShowWeeklyExceptionSummary(int WeekID)
        {
            string sVersion = string.Empty;
            string sSiteName = string.Empty;

            try
            {
                LogManager.WriteLog("Inside ShowWeeklyExceptionSummary method", LogManager.enumLogLevel.Info);

                GetVersion_SiteName(out  sVersion, out  sSiteName);

                CrystalDecisions.CrystalReports.Engine.Database crDatabase;
                CrystalDecisions.CrystalReports.Engine.Tables crTables;

                TableLogOnInfo tableLogonInfo = new TableLogOnInfo();

                weekly_exception_summary_report WeeklyExceptionSummaryReport = new weekly_exception_summary_report();

                LogManager.WriteLog("Setting Report Datasource...", LogManager.enumLogLevel.Info);

                LogManager.WriteLog("Report DataSource set successfully", LogManager.enumLogLevel.Info);

                LogManager.WriteLog("Setting Report Parameters...", LogManager.enumLogLevel.Info);

                WeeklyExceptionSummaryReport.SetParameterValue("WeekNo", WeekID);
                WeeklyExceptionSummaryReport.SetParameterValue("@WeekID", WeekID);
                WeeklyExceptionSummaryReport.SetParameterValue("@Currency", ExtensionMethods.CurrentSiteCulture.GetCurrencySymbol());
                WeeklyExceptionSummaryReport.SetParameterValue("Version", sVersion);


                IReports objReports = ReportsBusinessObject.CreateInstance();

                List<ServerDetails> DbConnectionInfo = objReports.GetDataBaseConnectionString();

                ConnectionInfo myConnectionInfo = new ConnectionInfo();


                myConnectionInfo.ServerName = DbConnectionInfo[0].ServerName;  // read this information from config file.
                myConnectionInfo.DatabaseName = DbConnectionInfo[0].DataBase; // read this information from config file.
                myConnectionInfo.UserID = DbConnectionInfo[0].Username;  // read this information from config file.
                myConnectionInfo.Password = DbConnectionInfo[0].Password;  // read this information from config file.


                crDatabase = WeeklyExceptionSummaryReport.Database;
                crTables = crDatabase.Tables;

                foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
                {
                    tableLogonInfo = crTable.LogOnInfo;
                    tableLogonInfo.ConnectionInfo = myConnectionInfo;
                    crTable.ApplyLogOnInfo(tableLogonInfo);
                }


                LogManager.WriteLog("Report Parameters set successfully", LogManager.enumLogLevel.Info);


                LogManager.WriteLog("Setting Report Source...", LogManager.enumLogLevel.Info);

                crystalReportViewer.ReportSource = WeeklyExceptionSummaryReport;


                LogManager.WriteLog("Report Source set successfully", LogManager.enumLogLevel.Info);

                AuditReports();

                strReportName = "Weekly Exception Summary Report";
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        
        public void ShowPartCollection(int NoofRecords)
        {
            string sVersion = string.Empty;
            string sSiteName = string.Empty;

            try
            {
                LogManager.WriteLog("Inside ShowPartCollection method", LogManager.enumLogLevel.Info);

                GetVersion_SiteName(out  sVersion, out  sSiteName);

                CrystalDecisions.CrystalReports.Engine.Database crDatabase;
                CrystalDecisions.CrystalReports.Engine.Tables crTables;

                TableLogOnInfo tableLogonInfo = new TableLogOnInfo();

                PartCollectionReport Partcol = new PartCollectionReport();
                Partcol.SetParameterValue("@NumberofRecords", NoofRecords);                
                Partcol.SetParameterValue("SiteName", sSiteName);
                Partcol.SetParameterValue("Version", sVersion);
                Partcol.SetParameterValue("Region", Settings.Region);
                Partcol.SetParameterValue("CurrencySymbol", ExtensionMethods.CurrentSiteCulture.GetCurrencySymbol());


                IReports objReports = ReportsBusinessObject.CreateInstance();

                List<ServerDetails> DbConnectionInfo = objReports.GetDataBaseConnectionString();

                ConnectionInfo myConnectionInfo = new ConnectionInfo();

                myConnectionInfo.ServerName = DbConnectionInfo[0].ServerName;  // read this information from config file.
                myConnectionInfo.DatabaseName = DbConnectionInfo[0].DataBase; // read this information from config file.
                myConnectionInfo.UserID = DbConnectionInfo[0].Username;  // read this information from config file.
                myConnectionInfo.Password = DbConnectionInfo[0].Password;  // read this information from config file.

                crDatabase = Partcol.Database;
                crTables = crDatabase.Tables;

                foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
                {
                    tableLogonInfo = crTable.LogOnInfo;
                    tableLogonInfo.ConnectionInfo = myConnectionInfo;
                    crTable.ApplyLogOnInfo(tableLogonInfo);
                }
                crystalReportViewer.ReportSource = Partcol;

                AuditReports();

                strReportName = "Part Collection Report";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public void ShowPartCollection(int NoofRecords,string ExchangeConn, String TicketingConn)
        {
            string sVersion = string.Empty;
            string sSiteName = string.Empty;

            try
            {
                LogManager.WriteLog("Inside ShowPartCollection method", LogManager.enumLogLevel.Info);

                GetVersion_SiteName(out  sVersion, out  sSiteName);

                CrystalDecisions.CrystalReports.Engine.Database crDatabase;
                CrystalDecisions.CrystalReports.Engine.Tables crTables;

                TableLogOnInfo tableLogonInfo = new TableLogOnInfo();

                PartCollectionReport Partcol = new PartCollectionReport();
                Partcol.SetParameterValue("@NumberofRecords", NoofRecords);
                Partcol.SetParameterValue("SiteName", sSiteName);
                Partcol.SetParameterValue("Version", sVersion);
                Partcol.SetParameterValue("Region", Settings.Region);
                Partcol.SetParameterValue("CurrencySymbol", ExtensionMethods.CurrentSiteCulture.GetCurrencySymbol());


                IReports objReports = ReportsBusinessObject.CreateInstance(ExchangeConn, TicketingConn);

                List<ServerDetails> DbConnectionInfo = objReports.GetDataBaseConnectionString(ExchangeConn);

                ConnectionInfo myConnectionInfo = new ConnectionInfo();

                myConnectionInfo.ServerName = DbConnectionInfo[0].ServerName;  // read this information from config file.
                myConnectionInfo.DatabaseName = DbConnectionInfo[0].DataBase; // read this information from config file.
                myConnectionInfo.UserID = DbConnectionInfo[0].Username;  // read this information from config file.
                myConnectionInfo.Password = DbConnectionInfo[0].Password;  // read this information from config file.

                crDatabase = Partcol.Database;
                crTables = crDatabase.Tables;

                foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
                {
                    tableLogonInfo = crTable.LogOnInfo;
                    tableLogonInfo.ConnectionInfo = myConnectionInfo;
                    crTable.ApplyLogOnInfo(tableLogonInfo);
                }
                crystalReportViewer.ReportSource = Partcol;

                AuditReports();

                strReportName = "Part Collection Report";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

		public void ShowOutstandingVaultReport(bool ShowCassettes)
        {
            string sVersion = string.Empty;
            string sSiteName = string.Empty;

            try
            {
                LogManager.WriteLog("Inside Show Undeclared Vault Drop Report method", LogManager.enumLogLevel.Info);

                GetVersion_SiteName(out  sVersion, out  sSiteName);

                CrystalDecisions.CrystalReports.Engine.Database crDatabase;
                CrystalDecisions.CrystalReports.Engine.Tables crTables;

                TableLogOnInfo tableLogonInfo = new TableLogOnInfo();

                UndeclaredVaultDrops UndecVaultDrp = new UndeclaredVaultDrops();

                UndecVaultDrp.SetParameterValue("@GetCassette", ShowCassettes);
                UndecVaultDrp.SetParameterValue("SiteName", sSiteName);
                UndecVaultDrp.SetParameterValue("Version", sVersion);

                UndecVaultDrp.SetParameterValue("CurrencyCulture", ExtensionMethods.CurrentCurrenyCulture);
                UndecVaultDrp.SetParameterValue("CurrencySymbol", ExtensionMethods.CurrentCurrenyCulture.GetCurrencySymbol());
                UndecVaultDrp.SetParameterValue("DateCulture", ExtensionMethods.CurrentDateCulture);
                UndecVaultDrp.SetParameterValue("SiteCode", Settings.SiteCode);
                LogManager.WriteLog("Report Parameters set successfully", LogManager.enumLogLevel.Info);

                IReports objReports = ReportsBusinessObject.CreateInstance();

                List<ServerDetails> DbConnectionInfo = objReports.GetDataBaseConnectionString();

                ConnectionInfo myConnectionInfo = new ConnectionInfo();


                myConnectionInfo.ServerName = DbConnectionInfo[0].ServerName;  // read this information from config file.
                myConnectionInfo.DatabaseName = DbConnectionInfo[0].DataBase; // read this information from config file.
                myConnectionInfo.UserID = DbConnectionInfo[0].Username;  // read this information from config file.
                myConnectionInfo.Password = DbConnectionInfo[0].Password;  // read this information from config file.


                crDatabase = UndecVaultDrp.Database;
                crTables = crDatabase.Tables;

                foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
                {
                    tableLogonInfo = crTable.LogOnInfo;
                    tableLogonInfo.ConnectionInfo = myConnectionInfo;
                    crTable.ApplyLogOnInfo(tableLogonInfo);
                }            


                LogManager.WriteLog("Setting Report Source...", LogManager.enumLogLevel.Info);

                crystalReportViewer.ReportSource = UndecVaultDrp;


                LogManager.WriteLog("Report Source set successfully", LogManager.enumLogLevel.Info);

                AuditReports();

                strReportName = "Undeclared Vault Drop Report";

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        public void ShowVaultEventDetailReport(int Vault_Id, string Type, int No_Of_Records, string searchkey, DateTime StartDate, DateTime EndDate)
        {
            string sVersion = string.Empty;
            string sSiteName = string.Empty;

            try
            {
                LogManager.WriteLog("Inside Show Vault Event Details Report method", LogManager.enumLogLevel.Info);

                GetVersion_SiteName(out  sVersion, out  sSiteName);

                CrystalDecisions.CrystalReports.Engine.Database crDatabase;
                CrystalDecisions.CrystalReports.Engine.Tables crTables;

                TableLogOnInfo tableLogonInfo = new TableLogOnInfo();

                CashDispenserTransactionDetails VaultEventDet = new CashDispenserTransactionDetails();
                VaultEventDet.SetParameterValue("SiteCode", Settings.SiteCode);
                VaultEventDet.SetParameterValue("@EventType", Type);
                VaultEventDet.SetParameterValue("@No_Of_Records", No_Of_Records);                
                VaultEventDet.SetParameterValue("@SearchKey", searchkey);
                VaultEventDet.SetParameterValue("@StartDate", StartDate);
                VaultEventDet.SetParameterValue("@EndDate", EndDate);
                VaultEventDet.SetParameterValue("SiteName", sSiteName);
                VaultEventDet.SetParameterValue("Version", sVersion);

                VaultEventDet.SetParameterValue("CurrencyCulture", ExtensionMethods.CurrentCurrenyCulture);
                VaultEventDet.SetParameterValue("CurrencySymbol", ExtensionMethods.CurrentCurrenyCulture.GetCurrencySymbol());
                VaultEventDet.SetParameterValue("DateCulture", ExtensionMethods.CurrentDateCulture);

                LogManager.WriteLog("Report Parameters set successfully", LogManager.enumLogLevel.Info);

                IReports objReports = ReportsBusinessObject.CreateInstance();

                List<ServerDetails> DbConnectionInfo = objReports.GetDataBaseConnectionString();

                ConnectionInfo myConnectionInfo = new ConnectionInfo();


                myConnectionInfo.ServerName = DbConnectionInfo[0].ServerName;  // read this information from config file.
                myConnectionInfo.DatabaseName = DbConnectionInfo[0].DataBase; // read this information from config file.
                myConnectionInfo.UserID = DbConnectionInfo[0].Username;  // read this information from config file.
                myConnectionInfo.Password = DbConnectionInfo[0].Password;  // read this information from config file.


                crDatabase = VaultEventDet.Database;
                crTables = crDatabase.Tables;

                foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
                {
                    tableLogonInfo = crTable.LogOnInfo;
                    tableLogonInfo.ConnectionInfo = myConnectionInfo;
                    crTable.ApplyLogOnInfo(tableLogonInfo);
                }


                LogManager.WriteLog("Setting Report Source...", LogManager.enumLogLevel.Info);

                crystalReportViewer.ReportSource = VaultEventDet;


                LogManager.WriteLog("Report Source set successfully", LogManager.enumLogLevel.Info);

                AuditReports();

                strReportName = "Vault Event Details Report";

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

        }
        public void ShowVaultCurrentBalanceSlip(int Vault_Id, string VaultName, string Serial_No, string Manufacturer_Name, string Type_Prefix, string LoginUser,
                                                                  DateTime CreatedDate, bool IsWebServiceEnabled, decimal FillAmount, decimal TotalAmountOnFill, decimal CurrentBalance)
        {
            string sVersion = string.Empty;
            string sSiteName = string.Empty;

            try
            {
                LogManager.WriteLog("Inside Show Vault Current Balance Slip Report method", LogManager.enumLogLevel.Info);

                GetVersion_SiteName(out  sVersion, out  sSiteName);

                CrystalDecisions.CrystalReports.Engine.Database crDatabase;
                CrystalDecisions.CrystalReports.Engine.Tables crTables;

                TableLogOnInfo tableLogonInfo = new TableLogOnInfo();
                VaultCurrentBalanceSlip VaultBalance = new VaultCurrentBalanceSlip();
                VaultBalance.SetParameterValue("@Vault_id", Vault_Id);
                VaultBalance.SetParameterValue("VaultName", VaultName);
                VaultBalance.SetParameterValue("SiteName", sSiteName);
                VaultBalance.SetParameterValue("SerialNo", Serial_No);
                VaultBalance.SetParameterValue("Type_Prefix", Type_Prefix);
                VaultBalance.SetParameterValue("Manufacturer_Name", Manufacturer_Name);
                VaultBalance.SetParameterValue("LoginUser", LoginUser);
                VaultBalance.SetParameterValue("CreatedDate", CreatedDate);
                VaultBalance.SetParameterValue("IsWebServiceEnabled", IsWebServiceEnabled);
                VaultBalance.SetParameterValue("FillAmount", FillAmount);
                VaultBalance.SetParameterValue("TotalAmountOnFill", TotalAmountOnFill);
                VaultBalance.SetParameterValue("CurrentBalance", CurrentBalance);
                VaultBalance.SetParameterValue("Version", sVersion);
                VaultBalance.SetParameterValue("CurrencyCulture", ExtensionMethods.CurrentCurrenyCulture);
                VaultBalance.SetParameterValue("CurrencySymbol", ExtensionMethods.CurrentCurrenyCulture.GetCurrencySymbol());
                VaultBalance.SetParameterValue("DateCulture", ExtensionMethods.CurrentDateCulture);
                VaultBalance.SetParameterValue("SiteCode", Settings.SiteCode);

                LogManager.WriteLog("Report Parameters set successfully", LogManager.enumLogLevel.Info);

                IReports objReports = ReportsBusinessObject.CreateInstance();

                List<ServerDetails> DbConnectionInfo = objReports.GetDataBaseConnectionString();

                ConnectionInfo myConnectionInfo = new ConnectionInfo();


                myConnectionInfo.ServerName = DbConnectionInfo[0].ServerName;  // read this information from config file.
                myConnectionInfo.DatabaseName = DbConnectionInfo[0].DataBase; // read this information from config file.
                myConnectionInfo.UserID = DbConnectionInfo[0].Username;  // read this information from config file.
                myConnectionInfo.Password = DbConnectionInfo[0].Password;  // read this information from config file.


                crDatabase = VaultBalance.Database;
                crTables = crDatabase.Tables;

                foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
                {
                    tableLogonInfo = crTable.LogOnInfo;
                    tableLogonInfo.ConnectionInfo = myConnectionInfo;
                    crTable.ApplyLogOnInfo(tableLogonInfo);
                }


                LogManager.WriteLog("Setting Report Source...", LogManager.enumLogLevel.Info);

                crystalReportViewer.ReportSource = VaultBalance;


                LogManager.WriteLog("Report Source set successfully", LogManager.enumLogLevel.Info);

                AuditReports();

                strReportName = "Vault Current Balance Slip Report";

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ShowVaultCurrentTransactionSlip(int Vault_Id, string VaultName, string Serial_No, string Manufacturer_Name, string Type_Prefix, string LoginUser,
                                                          DateTime CreatedDate, bool IsWebServiceEnabled, decimal FillAmount, decimal TotalAmountOnFill, decimal CurrentBalance, string TransType)
        {
            string sVersion = string.Empty;
            string sSiteName = string.Empty;

            if (TransType.ToUpper() == "FILL")
            {
                TransType = "Fill";
            }
            else if (TransType.ToUpper() == "STANDARD FILL")
            {
                TransType = "Standard Fill";
            }
            else if (TransType.ToUpper() == "BLEED")
            {
                TransType = "Bleed";
            }
            else if (TransType.ToUpper() == "POSITIVE ADJUSTMENT")
            {
                TransType = "Positive Adjustment";
            }
            else if (TransType.ToUpper() == "NEGATIVE ADJUSTMENT")
            {
                TransType = "Negative Adjustment";
            }

            try
            {
                LogManager.WriteLog("Inside Show Vault Current Transaction Slip Report method", LogManager.enumLogLevel.Info);

                GetVersion_SiteName(out  sVersion, out  sSiteName);

                CrystalDecisions.CrystalReports.Engine.Database crDatabase;
                CrystalDecisions.CrystalReports.Engine.Tables crTables;

                TableLogOnInfo tableLogonInfo = new TableLogOnInfo();
                VaultTransactionSlip VaultBalance = new VaultTransactionSlip();

                VaultBalance.SetParameterValue("@Vault_id", Vault_Id);
                VaultBalance.SetParameterValue("VaultName", VaultName);
                VaultBalance.SetParameterValue("SiteName", sSiteName);
                VaultBalance.SetParameterValue("SiteCode", Settings.SiteCode);
                VaultBalance.SetParameterValue("SerialNo", Serial_No);
                VaultBalance.SetParameterValue("Type_Prefix", Type_Prefix);
                VaultBalance.SetParameterValue("Manufacturer_Name", Manufacturer_Name);
                VaultBalance.SetParameterValue("LoginUser", LoginUser);
                VaultBalance.SetParameterValue("CreatedDate", CreatedDate);
                VaultBalance.SetParameterValue("IsWebServiceEnabled", IsWebServiceEnabled);
                VaultBalance.SetParameterValue("FillAmount", FillAmount);
                VaultBalance.SetParameterValue("TotalAmountOnFill", TotalAmountOnFill);
                VaultBalance.SetParameterValue("CurrentBalance", CurrentBalance);
                VaultBalance.SetParameterValue("Version", sVersion);
                VaultBalance.SetParameterValue("CurrencyCulture", ExtensionMethods.CurrentCurrenyCulture);
                VaultBalance.SetParameterValue("CurrencySymbol", ExtensionMethods.CurrentCurrenyCulture.GetCurrencySymbol());
                VaultBalance.SetParameterValue("DateCulture", ExtensionMethods.CurrentDateCulture);
                VaultBalance.SetParameterValue("TransactionType", TransType);


                LogManager.WriteLog("Report Parameters set successfully", LogManager.enumLogLevel.Info);

                IReports objReports = ReportsBusinessObject.CreateInstance();

                List<ServerDetails> DbConnectionInfo = objReports.GetDataBaseConnectionString();

                ConnectionInfo myConnectionInfo = new ConnectionInfo();


                myConnectionInfo.ServerName = DbConnectionInfo[0].ServerName;  // read this information from config file.
                myConnectionInfo.DatabaseName = DbConnectionInfo[0].DataBase; // read this information from config file.
                myConnectionInfo.UserID = DbConnectionInfo[0].Username;  // read this information from config file.
                myConnectionInfo.Password = DbConnectionInfo[0].Password;  // read this information from config file.


                crDatabase = VaultBalance.Database;
                crTables = crDatabase.Tables;

                foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
                {
                    tableLogonInfo = crTable.LogOnInfo;
                    tableLogonInfo.ConnectionInfo = myConnectionInfo;
                    crTable.ApplyLogOnInfo(tableLogonInfo);
                }


                LogManager.WriteLog("Setting Report Source...", LogManager.enumLogLevel.Info);

                crystalReportViewer.ReportSource = VaultBalance;


                LogManager.WriteLog("Report Source set successfully", LogManager.enumLogLevel.Info);

                AuditReports();

                strReportName = "Vault Transaction Slip Report";

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ShowVaultCurrentDropSlip(int Vault_Id, string VaultName, string Serial_No, string Manufacturer_Name, string Type_Prefix, string LoginUser,
                                                        DateTime CreatedDate, bool IsWebServiceEnabled, decimal FillAmount, decimal TotalAmountOnFill,
                                                        decimal CurrentBalance, bool _isfinaldrop)
        {
            string sVersion = string.Empty;
            string sSiteName = string.Empty;

            try
            {
                LogManager.WriteLog("Inside Show Vault Current Drop Slip method", LogManager.enumLogLevel.Info);

                GetVersion_SiteName(out  sVersion, out  sSiteName);

                CrystalDecisions.CrystalReports.Engine.Database crDatabase;
                CrystalDecisions.CrystalReports.Engine.Tables crTables;

                TableLogOnInfo tableLogonInfo = new TableLogOnInfo();
                VaultDropTransactionSlip VaultBalance = new VaultDropTransactionSlip();

                string DropType = string.Empty;

                if (_isfinaldrop)
                    DropType = "Final Drop";
                else
                    DropType = "Standard Drop";

                VaultBalance.SetParameterValue("@Vault_id", Vault_Id);
                VaultBalance.SetParameterValue("VaultName", VaultName);
                VaultBalance.SetParameterValue("SiteName", sSiteName);
                VaultBalance.SetParameterValue("SerialNo", Serial_No);
                VaultBalance.SetParameterValue("Type_Prefix", Type_Prefix);
                VaultBalance.SetParameterValue("Manufacturer_Name", Manufacturer_Name);
                VaultBalance.SetParameterValue("LoginUser", LoginUser);
                VaultBalance.SetParameterValue("CreatedDate", CreatedDate);
                VaultBalance.SetParameterValue("IsWebServiceEnabled", IsWebServiceEnabled);
                VaultBalance.SetParameterValue("FillAmount", FillAmount);
                VaultBalance.SetParameterValue("TotalAmountOnFill", TotalAmountOnFill);
                VaultBalance.SetParameterValue("CurrentBalance", CurrentBalance);
                VaultBalance.SetParameterValue("Version", sVersion);
                VaultBalance.SetParameterValue("CurrencyCulture", ExtensionMethods.CurrentCurrenyCulture);
                VaultBalance.SetParameterValue("CurrencySymbol", ExtensionMethods.CurrentCurrenyCulture.GetCurrencySymbol());
                VaultBalance.SetParameterValue("DateCulture", ExtensionMethods.CurrentDateCulture);
                VaultBalance.SetParameterValue("DropType", DropType);
                VaultBalance.SetParameterValue("SiteCode", Settings.SiteCode);

                LogManager.WriteLog("Report Parameters set successfully", LogManager.enumLogLevel.Info);

                IReports objReports = ReportsBusinessObject.CreateInstance();

                List<ServerDetails> DbConnectionInfo = objReports.GetDataBaseConnectionString();

                ConnectionInfo myConnectionInfo = new ConnectionInfo();

                myConnectionInfo.ServerName = DbConnectionInfo[0].ServerName;  // read this information from config file.
                myConnectionInfo.DatabaseName = DbConnectionInfo[0].DataBase; // read this information from config file.
                myConnectionInfo.UserID = DbConnectionInfo[0].Username;  // read this information from config file.
                myConnectionInfo.Password = DbConnectionInfo[0].Password;  // read this information from config file.

                crDatabase = VaultBalance.Database;
                crTables = crDatabase.Tables;

                foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
                {
                    tableLogonInfo = crTable.LogOnInfo;
                    tableLogonInfo.ConnectionInfo = myConnectionInfo;
                    crTable.ApplyLogOnInfo(tableLogonInfo);
                }

                LogManager.WriteLog("Setting Report Source...", LogManager.enumLogLevel.Info);
                crystalReportViewer.ReportSource = VaultBalance;
                LogManager.WriteLog("Report Source set successfully", LogManager.enumLogLevel.Info);
                AuditReports();
                strReportName = "Vault Drop Transaction Slip Report";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }       
        public void ShowMachineDropReport(int BatchNo, int WeekNo)
        {
            string sVersion = string.Empty;
            string sSiteName = string.Empty;
            try
            {
                LogManager.WriteLog("Inside ShowMachineDropReport method", LogManager.enumLogLevel.Info);

                GetVersion_SiteName(out  sVersion, out  sSiteName);

                CrystalDecisions.CrystalReports.Engine.Database crDatabase;
                CrystalDecisions.CrystalReports.Engine.Tables crTables;

                TableLogOnInfo tableLogonInfo = new TableLogOnInfo();
                BatchWinLoss MachineDropReport = new BatchWinLoss();
                LogManager.WriteLog("Setting Report Datasource...", LogManager.enumLogLevel.Info);
                LogManager.WriteLog("Report DataSource set successfully", LogManager.enumLogLevel.Info);
                LogManager.WriteLog("Setting Report Parameters...", LogManager.enumLogLevel.Info);

                //MachineDropReport.SetParameterValue("BatchNo", BatchNo);
                MachineDropReport.SetParameterValue("@BatchNo", BatchNo);
                MachineDropReport.SetParameterValue("@WeekCollection", WeekNo > 0 ? true : false);
                MachineDropReport.SetParameterValue("CurrencySymbol", ExtensionMethods.CurrentSiteCulture.GetCurrencySymbol());
                MachineDropReport.SetParameterValue("CopyRight", Settings.CopyRightInfo);
                MachineDropReport.SetParameterValue("Region", Settings.Region);

                IReports objReports = String.IsNullOrEmpty(_ExchangeConnectionString) ? ReportsBusinessObject.CreateInstance() : ReportsBusinessObject.CreateInstance(_ExchangeConnectionString, _TicketingConnectionString);

                List<ServerDetails> DbConnectionInfo = String.IsNullOrEmpty(_ExchangeConnectionString) ? objReports.GetDataBaseConnectionString() : objReports.GetDataBaseConnectionString(BMC.Business.CashDeskOperator.CommonUtilities.SiteConnectionString(_ExchangeConnectionString));

                ConnectionInfo myConnectionInfo = new ConnectionInfo();


                myConnectionInfo.ServerName = DbConnectionInfo[0].ServerName;  // read this information from config file.
                myConnectionInfo.DatabaseName = DbConnectionInfo[0].DataBase; // read this information from config file.
                myConnectionInfo.UserID = DbConnectionInfo[0].Username;  // read this information from config file.
                myConnectionInfo.Password = DbConnectionInfo[0].Password;  // read this information from config file.


                crDatabase = MachineDropReport.Database;
                crTables = crDatabase.Tables;

                foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
                {
                    tableLogonInfo = crTable.LogOnInfo;
                    tableLogonInfo.ConnectionInfo = myConnectionInfo;
                    crTable.ApplyLogOnInfo(tableLogonInfo);
                }


                LogManager.WriteLog("Report Parameters set successfully", LogManager.enumLogLevel.Info);


                LogManager.WriteLog("Setting Report Source...", LogManager.enumLogLevel.Info);

                crystalReportViewer.ReportSource = MachineDropReport;


                LogManager.WriteLog("Report Source set successfully", LogManager.enumLogLevel.Info);

                AuditReports();

                strReportName = "Machine Drop Report";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
      
        private void ExportReports(ReportClass vReportCls,string dtAnalysis)
        {           
            System.Windows.Forms.SaveFileDialog ReportExportDialog;
            string FileName;
            int filterIndex;

            try
            {
                ReportExportDialog = new SaveFileDialog();

                ReportExportDialog.Filter = "PDF Document *.pdf|*.pdf|Excel File *.xls|*.xls|CSV Document *.csv|*.csv";
                ReportExportDialog.Title = "Export Report";
                ReportExportDialog.SupportMultiDottedExtensions = true;
                ReportExportDialog.DefaultExt = ".pdf";
                DialogResult DiaResult = ReportExportDialog.ShowDialog();
                filterIndex = ReportExportDialog.FilterIndex;

                if (DiaResult == DialogResult.OK)
                {
                    FileName = ReportExportDialog.FileName;
                    if (filterIndex!= 3)
                    {
                        switch (filterIndex)
                        {
                            case 1:
                                if (!FileName.ToUpper().EndsWith(".PDF"))
                                    FileName += ".pdf";
                                vReportCls.ExportToDisk(ExportFormatType.PortableDocFormat, FileName);
                                break;
                            case 2:
                                if (!FileName.ToUpper().EndsWith(".XLS"))
                                    FileName += ".xls";
                                vReportCls.ExportToDisk(ExportFormatType.Excel, FileName);
                                break;
                            default:
                                if (!FileName.ToUpper().EndsWith(".PDF"))
                                    FileName += ".pdf";
                                vReportCls.ExportToDisk(ExportFormatType.PortableDocFormat, FileName);
                                break;
                        }
                    }
                    else
                        ExportDatatabletoCSV(dtAnalysis,FileName);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);  
            }
        }



        internal void ShowDeclarationReport(DataSet DSDeclaration, string userName,int BatchNo, int nMachineCount, string sDropType, 
            string ExchangeConnectionString, string TicketingConnectionString)
        {
            try
            {
                string sVersion, sSiteName;

                var rDSDeclaration = new DeclarationPrint();

                rDSDeclaration.SetDataSource(DSDeclaration);
                rDSDeclaration.SetParameterValue("UserName", userName);
                rDSDeclaration.SetParameterValue("DropType", sDropType);
                if (ExchangeConnectionString.Trim() == "")
                    GetVersion_SiteName(out  sVersion, out  sSiteName);
                else
                    GetVersion_SiteName(out  sVersion, out  sSiteName, ExchangeConnectionString, TicketingConnectionString);
                rDSDeclaration.SetParameterValue("SiteCode", Settings.SiteCode);
                rDSDeclaration.SetParameterValue("siteName", sSiteName);
                rDSDeclaration.SetParameterValue("Version", sVersion);
                rDSDeclaration.SetParameterValue("CurrencyCulture", ExtensionMethods.CurrentCurrenyCulture);
                rDSDeclaration.SetParameterValue("CurrencySymbol", ExtensionMethods.CurrentCurrenyCulture.GetCurrencySymbol());
                rDSDeclaration.SetParameterValue("DateCulture", ExtensionMethods.CurrentDateCulture);
                rDSDeclaration.SetParameterValue("BatchNo", BatchNo);
                rDSDeclaration.SetParameterValue("MachineCount", nMachineCount);

                crystalReportViewer.ReportSource = rDSDeclaration;

                AuditReports();

                strReportName = "Declaration Report";
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }


        internal void DropSummaryReport(DataSet DSDeclaration, string userName, int BatchNo, int nMachineCount, string sDropType)
        {
            try
            {
                string sVersion, sSiteName;

                var rDSDeclaration = new DropSummaryReport();

                rDSDeclaration.SetDataSource(DSDeclaration);
                rDSDeclaration.SetParameterValue("UserName", userName);
                rDSDeclaration.SetParameterValue("DropType", sDropType);
                GetVersion_SiteName(out  sVersion, out  sSiteName);
                rDSDeclaration.SetParameterValue("SiteCode", Settings.SiteCode);
                rDSDeclaration.SetParameterValue("siteName", sSiteName);
                rDSDeclaration.SetParameterValue("Version", sVersion);
                rDSDeclaration.SetParameterValue("CurrencyCulture", ExtensionMethods.CurrentCurrenyCulture);
                rDSDeclaration.SetParameterValue("CurrencySymbol", ExtensionMethods.CurrentCurrenyCulture.GetCurrencySymbol());
                rDSDeclaration.SetParameterValue("DateCulture", ExtensionMethods.CurrentDateCulture);
                rDSDeclaration.SetParameterValue("BatchNo", BatchNo);
                rDSDeclaration.SetParameterValue("MachineCount", nMachineCount);

                crystalReportViewer.ReportSource = rDSDeclaration;

                AuditReports();

                strReportName = "DropSummary Report";
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        
        internal void PrintCashDispenserReceipt(string sWorkstation, string primaryFieldName, string primaryFieldvalue, decimal dAmount)
        {
            try
            {
                string sVersion, sSiteName;

                var rCashDispenserReceipt = new CashDispenserReceipt();

                //rCashDispenserReceipt.SetDataSource(DSDeclaration);
                rCashDispenserReceipt.SetParameterValue("UserName", Security.SecurityHelper.CurrentUser.UserName);
                rCashDispenserReceipt.SetParameterValue("Workstation", sWorkstation);
                GetVersion_SiteName(out  sVersion, out  sSiteName);
                rCashDispenserReceipt.SetParameterValue("siteName", sSiteName);
                rCashDispenserReceipt.SetParameterValue("BMCVersion", sVersion);
                rCashDispenserReceipt.SetParameterValue("CurrencyCulture", ExtensionMethods.CurrentCurrenyCulture);
                rCashDispenserReceipt.SetParameterValue("CurrencySymbol", ExtensionMethods.CurrentCurrenyCulture.GetCurrencySymbol());
                rCashDispenserReceipt.SetParameterValue("DateCulture", ExtensionMethods.CurrentDateCulture);
                rCashDispenserReceipt.SetParameterValue("PrimaryFieldName", primaryFieldName);
                rCashDispenserReceipt.SetParameterValue("VoucherNumber", primaryFieldvalue);
                rCashDispenserReceipt.SetParameterValue("Voucher Value", dAmount);

                crystalReportViewer.ReportSource = rCashDispenserReceipt;
                crystalReportViewer.PrintReport();

                AuditReports();

                strReportName = "Cash Dispenser Report";
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        internal void ShowCrossPropertyTicketAnalysisReport(string p, DataSet dtDataset, DateTime StartDate, DateTime EndDate)
        {

            try
            {
                CrystalDecisions.CrystalReports.Engine.Database crDatabase;
                CrystalDecisions.CrystalReports.Engine.Tables crTables;

                TableLogOnInfo tableLogonInfo = new TableLogOnInfo();
                ConnectionInfo connectionInfo = new ConnectionInfo();
                DbConnectionDetails.DatabaseName = "TICKETING";

                connectionInfo.ServerName = DbConnectionDetails.ServerName;
                connectionInfo.UserID = DbConnectionDetails.UserName;
                connectionInfo.Password = DbConnectionDetails.Password;
                connectionInfo.DatabaseName = DbConnectionDetails.DatabaseName;
                
                string sVersion, sSiteName;

                var vCrossPropertyTicketAnalysis = new CrossPropertyTicketAnalysis();

                vCrossPropertyTicketAnalysis.SetDataSource(dtDataset);
                vCrossPropertyTicketAnalysis.SetParameterValue("SiteCode", Settings.SiteCode);
                vCrossPropertyTicketAnalysis.SetParameterValue("fromDate", StartDate);
                vCrossPropertyTicketAnalysis.SetParameterValue("toDate", EndDate);
                vCrossPropertyTicketAnalysis.SetParameterValue("@STARTDATE", StartDate);
                vCrossPropertyTicketAnalysis.SetParameterValue("@ENDDATE", EndDate);

                GetVersion_SiteName(out  sVersion, out  sSiteName);
                vCrossPropertyTicketAnalysis.SetParameterValue("siteName", sSiteName);
                vCrossPropertyTicketAnalysis.SetParameterValue("BMCVersion", sVersion);
                vCrossPropertyTicketAnalysis.SetParameterValue("CurrencyCulture", ExtensionMethods.CurrentCurrenyCulture);
                vCrossPropertyTicketAnalysis.SetParameterValue("CurrencySymbol", ExtensionMethods.CurrentCurrenyCulture.GetCurrencySymbol());
                vCrossPropertyTicketAnalysis.SetParameterValue("DateCulture", ExtensionMethods.CurrentDateCulture);


                crDatabase = vCrossPropertyTicketAnalysis.Database;
                crTables = crDatabase.Tables;

                foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
                {
                    tableLogonInfo = crTable.LogOnInfo;
                    tableLogonInfo.ConnectionInfo = connectionInfo;
                    crTable.ApplyLogOnInfo(tableLogonInfo);
                }

                crystalReportViewer.ReportSource = vCrossPropertyTicketAnalysis;

                AuditReports();

                strReportName = "Voucher Listing Report";
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        internal void ShowCrossPropertyLiabilityTransferDetailsReport(string p, DataSet dtDataset, DateTime StartDate, DateTime EndDate)
        {
            try
            {

                CrystalDecisions.CrystalReports.Engine.Database crDatabase;
                CrystalDecisions.CrystalReports.Engine.Tables crTables;

                TableLogOnInfo tableLogonInfo = new TableLogOnInfo();
                ConnectionInfo connectionInfo = new ConnectionInfo();
                DbConnectionDetails.DatabaseName = "TICKETING";

                connectionInfo.ServerName = DbConnectionDetails.ServerName;
                connectionInfo.UserID = DbConnectionDetails.UserName;
                connectionInfo.Password = DbConnectionDetails.Password;
                connectionInfo.DatabaseName = DbConnectionDetails.DatabaseName;
                string sVersion, sSiteName;

                var vCrossPropertyLiabilityTransferDetailsReport = new CrossPropertyLiabilityTransferDetailsReport();

                vCrossPropertyLiabilityTransferDetailsReport.SetDataSource(dtDataset);

                vCrossPropertyLiabilityTransferDetailsReport.SetParameterValue("@STARTDATE", StartDate);
                vCrossPropertyLiabilityTransferDetailsReport.SetParameterValue("@ENDDATE", EndDate);

                GetVersion_SiteName(out  sVersion, out  sSiteName);
                vCrossPropertyLiabilityTransferDetailsReport.SetParameterValue("SiteCode", Settings.SiteCode);
                vCrossPropertyLiabilityTransferDetailsReport.SetParameterValue("siteName", sSiteName);
                vCrossPropertyLiabilityTransferDetailsReport.SetParameterValue("BMCVersion", sVersion);
                vCrossPropertyLiabilityTransferDetailsReport.SetParameterValue("CurrencyCulture", ExtensionMethods.CurrentCurrenyCulture);
                vCrossPropertyLiabilityTransferDetailsReport.SetParameterValue("CurrencySymbol", ExtensionMethods.CurrentCurrenyCulture.GetCurrencySymbol());
                vCrossPropertyLiabilityTransferDetailsReport.SetParameterValue("DateCulture", ExtensionMethods.CurrentDateCulture);


                crDatabase = vCrossPropertyLiabilityTransferDetailsReport.Database;
                crTables = crDatabase.Tables;

                foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
                {
                    tableLogonInfo = crTable.LogOnInfo;
                    tableLogonInfo.ConnectionInfo = connectionInfo;
                    crTable.ApplyLogOnInfo(tableLogonInfo);
                }


                crystalReportViewer.ReportSource = vCrossPropertyLiabilityTransferDetailsReport;

                AuditReports();

                strReportName = "Cross Property Liability Transfer Details Report";
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        internal void ShowCrossPropertyLiabilityTransferSummaryReport(string p, DataSet dtDataset, DateTime StartDate, DateTime EndDate)
        {
            try
            {

                CrystalDecisions.CrystalReports.Engine.Database crDatabase;
                CrystalDecisions.CrystalReports.Engine.Tables crTables;

                TableLogOnInfo tableLogonInfo = new TableLogOnInfo();
                ConnectionInfo connectionInfo = new ConnectionInfo();
                DbConnectionDetails.DatabaseName = "TICKETING";

                connectionInfo.ServerName = DbConnectionDetails.ServerName;
                connectionInfo.UserID = DbConnectionDetails.UserName;
                connectionInfo.Password = DbConnectionDetails.Password;
                connectionInfo.DatabaseName = DbConnectionDetails.DatabaseName;
                string sVersion, sSiteName;

                var vCrossPropertyLiabilityTransferSummaryReport = new CrossPropertyLiabilityTransferSummaryReport();

                vCrossPropertyLiabilityTransferSummaryReport.SetDataSource(dtDataset);

                vCrossPropertyLiabilityTransferSummaryReport.SetParameterValue("@STARTDATE", StartDate);
                vCrossPropertyLiabilityTransferSummaryReport.SetParameterValue("@ENDDATE", EndDate);

                GetVersion_SiteName(out  sVersion, out  sSiteName);
                vCrossPropertyLiabilityTransferSummaryReport.SetParameterValue("SiteCode", Settings.SiteCode);
                vCrossPropertyLiabilityTransferSummaryReport.SetParameterValue("siteName", sSiteName);
                vCrossPropertyLiabilityTransferSummaryReport.SetParameterValue("BMCVersion", sVersion);
                vCrossPropertyLiabilityTransferSummaryReport.SetParameterValue("CurrencyCulture", ExtensionMethods.CurrentCurrenyCulture);
                vCrossPropertyLiabilityTransferSummaryReport.SetParameterValue("CurrencySymbol", ExtensionMethods.CurrentCurrenyCulture.GetCurrencySymbol());
                vCrossPropertyLiabilityTransferSummaryReport.SetParameterValue("DateCulture", ExtensionMethods.CurrentDateCulture);


                crDatabase = vCrossPropertyLiabilityTransferSummaryReport.Database;
                crTables = crDatabase.Tables;

                foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
                {
                    tableLogonInfo = crTable.LogOnInfo;
                    tableLogonInfo.ConnectionInfo = connectionInfo;
                    crTable.ApplyLogOnInfo(tableLogonInfo);
                }


                crystalReportViewer.ReportSource = vCrossPropertyLiabilityTransferSummaryReport;

                AuditReports();

                strReportName = "Cross Property Liability Transfer Summary Report";
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        internal void ShowVaultFillHistory(DataTable dtHistory, int DeviceId, int Records)
        {
            try
            {
                string sVersion, sSiteName;

                FillVaultHistory oFillHistory = new FillVaultHistory();

                oFillHistory.SetDataSource(dtHistory);

                GetVersion_SiteName(out  sVersion, out  sSiteName);

                oFillHistory.SetParameterValue("@Device_ID", DeviceId);
                oFillHistory.SetParameterValue("@No_Of_Records", Records);               
                oFillHistory.SetParameterValue("siteName", sSiteName);
                oFillHistory.SetParameterValue("BMCVersion", sVersion);
                oFillHistory.SetParameterValue("RequestedRecords", Records);
                oFillHistory.SetParameterValue("CurrencyCulture", ExtensionMethods.CurrentCurrenyCulture);
                oFillHistory.SetParameterValue("CurrencySymbol", ExtensionMethods.CurrentCurrenyCulture.GetCurrencySymbol());
                oFillHistory.SetParameterValue("DateCulture", ExtensionMethods.CurrentDateCulture);                

                crystalReportViewer.ReportSource = oFillHistory;

                AuditReports();

                strReportName = "Fill Vault History";
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }    
		
		internal void PrintVoidVoucherReceipt(string sWorkstation, string sVoucherNumber, decimal dAmount, int iTransactionNo,string sCode,int iSequenceNo)
        {
            try
            {
                string sVersion, sSiteName;
                string sCurrency = string.Empty;

                if (ExtensionMethods.CurrentCurrenyCulture.GetCurrencySymbol() == "£")
                {
                    sCurrency = "GBP";                    
                }
                else if (ExtensionMethods.CurrentCurrenyCulture.GetCurrencySymbol() == "€")
                {
                    sCurrency = "EUR";
                }
                else
                {
                    sCurrency = "$";
                }

                var rVoidVoucherReceipt = new VoidVoucherReceipt();
                rVoidVoucherReceipt.SetParameterValue("UserName", Security.SecurityHelper.CurrentUser.First_Name + "," + Security.SecurityHelper.CurrentUser.Last_Name);
                rVoidVoucherReceipt.SetParameterValue("Workstation", sWorkstation);
                GetVersion_SiteName(out  sVersion, out  sSiteName);
                rVoidVoucherReceipt.SetParameterValue("siteName", sSiteName);
                rVoidVoucherReceipt.SetParameterValue("BMCVersion", sVersion);
                rVoidVoucherReceipt.SetParameterValue("CurrencyCulture", ExtensionMethods.CurrentCurrenyCulture);
                rVoidVoucherReceipt.SetParameterValue("CurrencySymbol", sCurrency);
                rVoidVoucherReceipt.SetParameterValue("DateCulture", ExtensionMethods.CurrentDateCulture);
                rVoidVoucherReceipt.SetParameterValue("VoucherNumber", sVoucherNumber);
                rVoidVoucherReceipt.SetParameterValue("Voucher Value", dAmount);
                rVoidVoucherReceipt.SetParameterValue("TransactionNo", iTransactionNo);
                rVoidVoucherReceipt.SetParameterValue("sCode", sCode);
                rVoidVoucherReceipt.SetParameterValue("SequenceNumber", iSequenceNo);
                rVoidVoucherReceipt.SetParameterValue("SHOW_NAME_IN_RECEPIT_SIGNATURE", Settings.SHOW_NAME_IN_RECEPIT_SIGNATURE);
                rVoidVoucherReceipt.SetParameterValue("HeadCashierSig", Convert.ToDouble(Settings.HeadCashierSig) / 100);
                rVoidVoucherReceipt.SetParameterValue("ManagerSig",Convert.ToDouble(Settings.ManagerSig) / 100);


                crystalReportViewer.ReportSource = rVoidVoucherReceipt;
                crystalReportViewer.PrintReport();

                AuditReports();

                strReportName = "Void Voucher Report";
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        internal void ShowSpotCheckReport(string sBarpositionName, string sZoneName, string sGameTitle, string dPop, DateTime dtDropDate, decimal dNetWinLoss, decimal dHandle, decimal dPercentagePayout, decimal dDrop, decimal dHandpay,DateTime dtLastDropDate, string siteCode)
        {
            try
            {
                string sVersion, sSiteName;
                var rptSpotCheck = new SpotCheckReport();

                rptSpotCheck.SetParameterValue("UserName", Security.SecurityHelper.CurrentUser.UserName);
                GetVersion_SiteName(out  sVersion, out  sSiteName);
                rptSpotCheck.SetParameterValue("siteName", sSiteName);
                rptSpotCheck.SetParameterValue("BMCVersion", sVersion);
                rptSpotCheck.SetParameterValue("CurrencyCulture", ExtensionMethods.CurrentCurrenyCulture);
                rptSpotCheck.SetParameterValue("CurrencySymbol", ExtensionMethods.CurrentCurrenyCulture.GetCurrencySymbol());
                rptSpotCheck.SetParameterValue("DateCulture", ExtensionMethods.CurrentDateCulture);
                rptSpotCheck.SetParameterValue("SiteCode", siteCode);

                rptSpotCheck.SetParameterValue("@CurrentDateTime", DateTime.Now);
                rptSpotCheck.SetParameterValue("@Position", sBarpositionName);
                rptSpotCheck.SetParameterValue("@ZoneName", sZoneName);
                rptSpotCheck.SetParameterValue("@GameTitle", sGameTitle);
                rptSpotCheck.SetParameterValue("@Denom", dPop);

                rptSpotCheck.SetParameterValue("@LastMeterUpdate", dtDropDate);
                rptSpotCheck.SetParameterValue("@NetWinLoss", dNetWinLoss);
                rptSpotCheck.SetParameterValue("@Handle", dHandle);
                rptSpotCheck.SetParameterValue("@PercentagePayout", dPercentagePayout.ToString());
                rptSpotCheck.SetParameterValue("@Drop", dDrop);
                rptSpotCheck.SetParameterValue("@Handpay", dHandpay);
                rptSpotCheck.SetParameterValue("@DropDate", dtLastDropDate);

                crystalReportViewer.ReportSource = rptSpotCheck;
                crystalReportViewer.PrintReport();

                AuditReports();

                strReportName = "Spot Check Report";
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void CReportViewer_Load(object sender, EventArgs e)
        {
            
        }
        
        internal void PrintPromotionalHistoryReport(DataSet dataSet)
        {
            try
            {
                string sVersion, sSiteName;
                var rPromotionalHistoryReport = new PromotionalHistoryReport();

                rPromotionalHistoryReport.SetDataSource(dataSet.Tables[0]);
                GetVersion_SiteName(out  sVersion, out  sSiteName);

                rPromotionalHistoryReport.SetParameterValue("SiteName", sSiteName);
                rPromotionalHistoryReport.SetParameterValue("SiteCode", Settings.SiteCode);
                rPromotionalHistoryReport.SetParameterValue("BMC Version", sVersion);
                rPromotionalHistoryReport.SetParameterValue("CurrencyCulture", ExtensionMethods.CurrentCurrenyCulture);
                rPromotionalHistoryReport.SetParameterValue("CurrencySymbol", ExtensionMethods.CurrentCurrenyCulture.GetCurrencySymbol());
                rPromotionalHistoryReport.SetParameterValue("DateCulture", ExtensionMethods.CurrentDateCulture);

                crystalReportViewer.ReportSource = rPromotionalHistoryReport;
                crystalReportViewer.Show();
                //crystalReportViewer.PrintReport();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        internal void PrintTISDetailsReport(DataSet dataSet, DateTime startDate, DateTime endDate, int noOfRecords)
        {
            try
            {
                string sVersion, sSiteName;
                var rTISVoucherReport = new TISVoucherDetailsReport();

                rTISVoucherReport.SetDataSource(dataSet.Tables[0]);
                GetVersion_SiteName(out  sVersion, out  sSiteName);

                rTISVoucherReport.SetParameterValue("SiteName", sSiteName);
                rTISVoucherReport.SetParameterValue("SiteCode", Settings.SiteCode);
                rTISVoucherReport.SetParameterValue("ReportStartDate", startDate);
                rTISVoucherReport.SetParameterValue("ReportEndDate", endDate);
                rTISVoucherReport.SetParameterValue("@StartDate", startDate);
                rTISVoucherReport.SetParameterValue("@EndDate", endDate);
                rTISVoucherReport.SetParameterValue("@NoOfRecords", noOfRecords);
                rTISVoucherReport.SetParameterValue("BMC Version", sVersion);
                rTISVoucherReport.SetParameterValue("CurrencyCulture", ExtensionMethods.CurrentCurrenyCulture);
                rTISVoucherReport.SetParameterValue("CurrencySymbol", ExtensionMethods.CurrentCurrenyCulture.GetCurrencySymbol());
                rTISVoucherReport.SetParameterValue("DateCulture", ExtensionMethods.CurrentDateCulture);

                crystalReportViewer.ReportSource = rTISVoucherReport;
                crystalReportViewer.Show();
                //crystalReportViewer.PrintReport();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        internal void ShowAccountingWinLossReport(string p, DataSet dtDataset, int ZoneNo, int MachineCategoryNo, DateTime StartDate, DateTime EndDate, string ZoneName, string CategoryName, bool IncludeNonCashable)
        {

            try
            {
                CrystalDecisions.CrystalReports.Engine.Database crDatabase;
                CrystalDecisions.CrystalReports.Engine.Tables crTables;

                TableLogOnInfo tableLogonInfo = new TableLogOnInfo();
                ConnectionInfo connectionInfo = new ConnectionInfo();
                DbConnectionDetails.DatabaseName = "EXCHANGE";

                connectionInfo.ServerName = DbConnectionDetails.ServerName;
                connectionInfo.UserID = DbConnectionDetails.UserName;
                connectionInfo.Password = DbConnectionDetails.Password;
                connectionInfo.DatabaseName = DbConnectionDetails.DatabaseName;

                string sVersion, sSiteName;

                var vAccountingWinLossReport = new AccountingWinLossReport();

                vAccountingWinLossReport.SetDataSource(dtDataset);
                vAccountingWinLossReport.SetParameterValue("@Zone", ZoneNo);
                vAccountingWinLossReport.SetParameterValue("@Category", MachineCategoryNo);
                vAccountingWinLossReport.SetParameterValue("@StartDate", StartDate);
                vAccountingWinLossReport.SetParameterValue("@EndDate", EndDate);
                vAccountingWinLossReport.SetParameterValue("@IncludeNonCashable", IncludeNonCashable);

                GetVersion_SiteName(out  sVersion, out  sSiteName);
                vAccountingWinLossReport.SetParameterValue("SiteName", sSiteName);
                vAccountingWinLossReport.SetParameterValue("ZoneName", ZoneName);
                vAccountingWinLossReport.SetParameterValue("CategoryName", CategoryName);
                vAccountingWinLossReport.SetParameterValue("Version", sVersion);
                vAccountingWinLossReport.SetParameterValue("Region", Settings.Region);
                vAccountingWinLossReport.SetParameterValue("CurrencyCulture", ExtensionMethods.CurrentCurrenyCulture);
                vAccountingWinLossReport.SetParameterValue("CurrencySymbol", ExtensionMethods.CurrentCurrenyCulture.GetCurrencySymbol());
                vAccountingWinLossReport.SetParameterValue("DateCulture", ExtensionMethods.CurrentDateCulture);
                vAccountingWinLossReport.SetParameterValue("SiteCode", Settings.SiteCode);


                crDatabase = vAccountingWinLossReport.Database;
                crTables = crDatabase.Tables;

                foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
                {
                    tableLogonInfo = crTable.LogOnInfo;
                    tableLogonInfo.ConnectionInfo = connectionInfo;
                    crTable.ApplyLogOnInfo(tableLogonInfo);
                }

                crystalReportViewer.ReportSource = vAccountingWinLossReport;

                AuditReports();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        internal void ExportDatatabletoCSV(string dtExport,string FileName)
        {
            try
            {
                if (FileName == string.Empty)
                {
                    SaveFileDialog ReportExportDialog = new SaveFileDialog();
                    ReportExportDialog.Filter = "CSV Document *.csv|*.csv|Excel File *.xls|*.xls";
                    ReportExportDialog.Title = "Export Report";
                    ReportExportDialog.SupportMultiDottedExtensions = true;
                    ReportExportDialog.DefaultExt = ".pdf";
                    DialogResult DiaResult = ReportExportDialog.ShowDialog();
                    if (DiaResult == DialogResult.OK) FileName = ReportExportDialog.FileName; else return;                
                }
                
                File.WriteAllText(FileName, dtExport,Encoding.Default);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog(ex.Message, LogManager.enumLogLevel.Error);
            }

        }

        public void ShowExceptionSummaryReportUK(int BatchNo)
        {
            string sVersion = string.Empty;
            string sSiteName = string.Empty;
            try
            {
                LogManager.WriteLog("Inside ShowExceptionSummaryReport method", LogManager.enumLogLevel.Info);
                GetVersion_SiteName(out  sVersion, out  sSiteName);

                CrystalDecisions.CrystalReports.Engine.Database crDatabase;
                CrystalDecisions.CrystalReports.Engine.Tables crTables;

                TableLogOnInfo tableLogonInfo = new TableLogOnInfo();

                ExceptionSummaryUK ExceptionSummaryReport = new ExceptionSummaryUK();

                LogManager.WriteLog("Setting Report Datasource...", LogManager.enumLogLevel.Info);

                LogManager.WriteLog("Report DataSource set successfully", LogManager.enumLogLevel.Info);

                LogManager.WriteLog("Setting Report Parameters...", LogManager.enumLogLevel.Info);
                ExceptionSummaryReport.SetParameterValue("CurrencySymbol", ExtensionMethods.CurrentSiteCulture.GetCurrencySymbol());
                ExceptionSummaryReport.SetParameterValue("BatchNumber", BatchNo);
                ExceptionSummaryReport.SetParameterValue("@Batch_No", BatchNo);
                ExceptionSummaryReport.SetParameterValue("Version", sVersion);

                IReports objReports = ReportsBusinessObject.CreateInstance();

                List<ServerDetails> DbConnectionInfo = objReports.GetDataBaseConnectionString();

                ConnectionInfo myConnectionInfo = new ConnectionInfo();


                myConnectionInfo.ServerName = DbConnectionInfo[0].ServerName;  // read this information from config file.
                myConnectionInfo.DatabaseName = DbConnectionInfo[0].DataBase; // read this information from config file.
                myConnectionInfo.UserID = DbConnectionInfo[0].Username;  // read this information from config file.
                myConnectionInfo.Password = DbConnectionInfo[0].Password;  // read this information from config file.


                crDatabase = ExceptionSummaryReport.Database;
                crTables = crDatabase.Tables;

                foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
                {
                    tableLogonInfo = crTable.LogOnInfo;
                    tableLogonInfo.ConnectionInfo = myConnectionInfo;
                    crTable.ApplyLogOnInfo(tableLogonInfo);
                }


                LogManager.WriteLog("Report Parameters set successfully", LogManager.enumLogLevel.Info);


                LogManager.WriteLog("Setting Report Source...", LogManager.enumLogLevel.Info);

                crystalReportViewer.ReportSource = ExceptionSummaryReport;


                LogManager.WriteLog("Report Source set successfully", LogManager.enumLogLevel.Info);

                AuditReports();

                strReportName = "Exception Summary Report";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ShowExceptionSummaryReportUK(int BatchNo, string ExchangeConn, string TicketingConn)
        {
            string sVersion = string.Empty;
            string sSiteName = string.Empty;

            try
            {
                LogManager.WriteLog("Inside ShowExceptionSummaryReport method when Common CDO Enlable ", LogManager.enumLogLevel.Info);

                GetVersion_SiteName(out  sVersion, out  sSiteName);

                CrystalDecisions.CrystalReports.Engine.Database crDatabase;
                CrystalDecisions.CrystalReports.Engine.Tables crTables;

                TableLogOnInfo tableLogonInfo = new TableLogOnInfo();

                ExceptionSummaryUK ExceptionSummaryReport = new ExceptionSummaryUK();

                LogManager.WriteLog("Setting Report Datasource...", LogManager.enumLogLevel.Info);

                LogManager.WriteLog("Report DataSource set successfully", LogManager.enumLogLevel.Info);

                LogManager.WriteLog("Setting Report Parameters...", LogManager.enumLogLevel.Info);

                try
                {
                    ExceptionSummaryReport.SetParameterValue("CurrencySymbol", ExtensionMethods.CurrentSiteCulture.GetCurrencySymbol());
                }
                catch
                {
                    ExceptionSummaryReport.SetParameterValue("CurrencySymbol", "£");
                    LogManager.WriteLog("Exception occurred at CurrencySymbol Set in ExceptionSummary...", LogManager.enumLogLevel.Info);
                }
                ExceptionSummaryReport.SetParameterValue("BatchNumber", BatchNo);
                ExceptionSummaryReport.SetParameterValue("@Batch_No", BatchNo);
                ExceptionSummaryReport.SetParameterValue("Version", sVersion);

                IReports objReports = ReportsBusinessObject.CreateInstance(ExchangeConn, TicketingConn);

                List<ServerDetails> DbConnectionInfo = objReports.GetDataBaseConnectionString(ExchangeConn);

                ConnectionInfo myConnectionInfo = new ConnectionInfo();


                myConnectionInfo.ServerName = DbConnectionInfo[0].ServerName;  // read this information from config file.
                myConnectionInfo.DatabaseName = DbConnectionInfo[0].DataBase; // read this information from config file.
                myConnectionInfo.UserID = DbConnectionInfo[0].Username;  // read this information from config file.
                myConnectionInfo.Password = DbConnectionInfo[0].Password;  // read this information from config file.


                crDatabase = ExceptionSummaryReport.Database;
                crTables = crDatabase.Tables;

                foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
                {
                    tableLogonInfo = crTable.LogOnInfo;
                    tableLogonInfo.ConnectionInfo = myConnectionInfo;
                    crTable.ApplyLogOnInfo(tableLogonInfo);
                }


                LogManager.WriteLog("Report Parameters set successfully", LogManager.enumLogLevel.Info);


                LogManager.WriteLog("Setting Report Source...", LogManager.enumLogLevel.Info);

                crystalReportViewer.ReportSource = ExceptionSummaryReport;


                LogManager.WriteLog("Report Source set successfully", LogManager.enumLogLevel.Info);

                AuditReports();

                strReportName = "Exception Summary Report";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ShowVarianceReport(int BatchNo)
        {
            string sVersion = string.Empty;
            string sSiteName = string.Empty;

            try
            {
                LogManager.WriteLog("Inside ShowExceptionSummaryReport method", LogManager.enumLogLevel.Info);

                GetVersion_SiteName(out  sVersion, out  sSiteName);

                CrystalDecisions.CrystalReports.Engine.Database crDatabase;
                CrystalDecisions.CrystalReports.Engine.Tables crTables;

                TableLogOnInfo tableLogonInfo = new TableLogOnInfo();

                VarianceReport varianceReport = new VarianceReport();

                LogManager.WriteLog("Setting Report Datasource...", LogManager.enumLogLevel.Info);

                LogManager.WriteLog("Report DataSource set successfully", LogManager.enumLogLevel.Info);

                LogManager.WriteLog("Setting Report Parameters...", LogManager.enumLogLevel.Info);
                varianceReport.SetParameterValue("CurrencySymbol", ExtensionMethods.CurrentSiteCulture.GetCurrencySymbol());
                varianceReport.SetParameterValue("BatchNumber", BatchNo);
                varianceReport.SetParameterValue("@Batch_No", BatchNo);
                varianceReport.SetParameterValue("Version", sVersion);

                IReports objReports = ReportsBusinessObject.CreateInstance();

                List<ServerDetails> DbConnectionInfo = objReports.GetDataBaseConnectionString();

                ConnectionInfo myConnectionInfo = new ConnectionInfo();


                myConnectionInfo.ServerName = DbConnectionInfo[0].ServerName;  // read this information from config file.
                myConnectionInfo.DatabaseName = DbConnectionInfo[0].DataBase; // read this information from config file.
                myConnectionInfo.UserID = DbConnectionInfo[0].Username;  // read this information from config file.
                myConnectionInfo.Password = DbConnectionInfo[0].Password;  // read this information from config file.


                crDatabase = varianceReport.Database;
                crTables = crDatabase.Tables;

                foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
                {
                    tableLogonInfo = crTable.LogOnInfo;
                    tableLogonInfo.ConnectionInfo = myConnectionInfo;
                    crTable.ApplyLogOnInfo(tableLogonInfo);
                }


                LogManager.WriteLog("Report Parameters set successfully", LogManager.enumLogLevel.Info);


                LogManager.WriteLog("Setting Report Source...", LogManager.enumLogLevel.Info);

                crystalReportViewer.ReportSource = varianceReport;


                LogManager.WriteLog("Report Source set successfully", LogManager.enumLogLevel.Info);

                AuditReports();

                strReportName = "Exception Summary Report";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ShowVarianceReport(int BatchNo, string ExchangeConn, string TicketingConn)
        {
            string sVersion = string.Empty;
            string sSiteName = string.Empty;

            try
            {
                LogManager.WriteLog("Inside ShowVarianceReport method when Common CDO Enlable ", LogManager.enumLogLevel.Info);

                GetVersion_SiteName(out  sVersion, out  sSiteName);

                CrystalDecisions.CrystalReports.Engine.Database crDatabase;
                CrystalDecisions.CrystalReports.Engine.Tables crTables;

                TableLogOnInfo tableLogonInfo = new TableLogOnInfo();

                VarianceReport varianceReport = new VarianceReport();

                LogManager.WriteLog("Setting Report Datasource...", LogManager.enumLogLevel.Info);

                LogManager.WriteLog("Report DataSource set successfully", LogManager.enumLogLevel.Info);

                LogManager.WriteLog("Setting Report Parameters...", LogManager.enumLogLevel.Info);

                try
                {
                    varianceReport.SetParameterValue("CurrencySymbol", ExtensionMethods.CurrentSiteCulture.GetCurrencySymbol());
                }
                catch
                {
                    varianceReport.SetParameterValue("CurrencySymbol", "£");
                    LogManager.WriteLog("Exception occurred at CurrencySymbol Set in ExceptionSummary...", LogManager.enumLogLevel.Info);
                }
                varianceReport.SetParameterValue("BatchNumber", BatchNo);
                varianceReport.SetParameterValue("@Batch_No", BatchNo);
                varianceReport.SetParameterValue("Version", sVersion);

                IReports objReports = ReportsBusinessObject.CreateInstance(ExchangeConn, TicketingConn);

                List<ServerDetails> DbConnectionInfo = objReports.GetDataBaseConnectionString(ExchangeConn);

                ConnectionInfo myConnectionInfo = new ConnectionInfo();


                myConnectionInfo.ServerName = DbConnectionInfo[0].ServerName;  // read this information from config file.
                myConnectionInfo.DatabaseName = DbConnectionInfo[0].DataBase; // read this information from config file.
                myConnectionInfo.UserID = DbConnectionInfo[0].Username;  // read this information from config file.
                myConnectionInfo.Password = DbConnectionInfo[0].Password;  // read this information from config file.


                crDatabase = varianceReport.Database;
                crTables = crDatabase.Tables;

                foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
                {
                    tableLogonInfo = crTable.LogOnInfo;
                    tableLogonInfo.ConnectionInfo = myConnectionInfo;
                    crTable.ApplyLogOnInfo(tableLogonInfo);
                }


                LogManager.WriteLog("Report Parameters set successfully", LogManager.enumLogLevel.Info);


                LogManager.WriteLog("Setting Report Source...", LogManager.enumLogLevel.Info);

                crystalReportViewer.ReportSource = varianceReport;


                LogManager.WriteLog("Report Source set successfully", LogManager.enumLogLevel.Info);

                AuditReports();

                strReportName = "Variance Report";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ShowBatchWinLossReportForUK(int BatchNo, int WeekNo)
        {
            string sVersion = string.Empty;
            string sSiteName = string.Empty;

            try
            {
                LogManager.WriteLog("Inside ShowBatchWinLossReport method", LogManager.enumLogLevel.Info);

                GetVersion_SiteName(out  sVersion, out  sSiteName);

                CrystalDecisions.CrystalReports.Engine.Database crDatabase;
                CrystalDecisions.CrystalReports.Engine.Tables crTables;

                TableLogOnInfo tableLogonInfo = new TableLogOnInfo();
                batchwinlossreport_UK BatchWinLossReport = new batchwinlossreport_UK();
                LogManager.WriteLog("Setting Report Datasource...", LogManager.enumLogLevel.Info);
                LogManager.WriteLog("Report DataSource set successfully", LogManager.enumLogLevel.Info);
                LogManager.WriteLog("Setting Report Parameters...", LogManager.enumLogLevel.Info);

                //BatchWinLossReport.SetParameterValue("BatchNo", BatchNo);
                BatchWinLossReport.SetParameterValue("@BatchNo", BatchNo);
                BatchWinLossReport.SetParameterValue("@WeekCollection", WeekNo > 0 ? true : false);
                BatchWinLossReport.SetParameterValue("CurrencySymbol", ExtensionMethods.CurrentSiteCulture.GetCurrencySymbol());
                BatchWinLossReport.SetParameterValue("CurrencyCulture", ExtensionMethods.CurrentCurrenyCulture);
                BatchWinLossReport.SetParameterValue("Version", sVersion);
                //BatchWinLossReport.SetParameterValue("CopyRight", Settings.CopyRightInfo);
                BatchWinLossReport.SetParameterValue("Region", Settings.Region);

                IReports objReports = String.IsNullOrEmpty(_ExchangeConnectionString) ? ReportsBusinessObject.CreateInstance() : ReportsBusinessObject.CreateInstance(_ExchangeConnectionString, _TicketingConnectionString);

                List<ServerDetails> DbConnectionInfo = String.IsNullOrEmpty(_ExchangeConnectionString) ? objReports.GetDataBaseConnectionString() : objReports.GetDataBaseConnectionString(BMC.Business.CashDeskOperator.CommonUtilities.SiteConnectionString(_ExchangeConnectionString));

                ConnectionInfo myConnectionInfo = new ConnectionInfo();


                myConnectionInfo.ServerName = DbConnectionInfo[0].ServerName;  // read this information from config file.
                myConnectionInfo.DatabaseName = DbConnectionInfo[0].DataBase; // read this information from config file.
                myConnectionInfo.UserID = DbConnectionInfo[0].Username;  // read this information from config file.
                myConnectionInfo.Password = DbConnectionInfo[0].Password;  // read this information from config file.


                crDatabase = BatchWinLossReport.Database;
                crTables = crDatabase.Tables;

                foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
                {
                    tableLogonInfo = crTable.LogOnInfo;
                    tableLogonInfo.ConnectionInfo = myConnectionInfo;
                    crTable.ApplyLogOnInfo(tableLogonInfo);
                }


                LogManager.WriteLog("Report Parameters set successfully", LogManager.enumLogLevel.Info);


                LogManager.WriteLog("Setting Report Source...", LogManager.enumLogLevel.Info);

                crystalReportViewer.ReportSource = BatchWinLossReport;


                LogManager.WriteLog("Report Source set successfully", LogManager.enumLogLevel.Info);

                AuditReports();

                strReportName = "batch winloss report";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ShowCollectionReport(int BatchNo, int WeekNo)
        {
            string sVersion = string.Empty;
            string sSiteName = string.Empty;

            try
            {
                LogManager.WriteLog("Inside ShowBatchWinLossReport method", LogManager.enumLogLevel.Info);

                GetVersion_SiteName(out  sVersion, out  sSiteName);

                CrystalDecisions.CrystalReports.Engine.Database crDatabase;
                CrystalDecisions.CrystalReports.Engine.Tables crTables;

                TableLogOnInfo tableLogonInfo = new TableLogOnInfo();
                CollectionReport oCollectionReport = new CollectionReport();
                LogManager.WriteLog("Setting Report Datasource...", LogManager.enumLogLevel.Info);
                LogManager.WriteLog("Report DataSource set successfully", LogManager.enumLogLevel.Info);
                LogManager.WriteLog("Setting Report Parameters...", LogManager.enumLogLevel.Info);

                oCollectionReport.SetParameterValue("@BatchNo", BatchNo);
                oCollectionReport.SetParameterValue("@WeekCollection", WeekNo > 0 ? true : false);
                oCollectionReport.SetParameterValue("CurrencySymbol", ExtensionMethods.CurrentSiteCulture.GetCurrencySymbol());
                oCollectionReport.SetParameterValue("CurrencyCulture", ExtensionMethods.CurrentCurrenyCulture);
                oCollectionReport.SetParameterValue("Version", sVersion);
                oCollectionReport.SetParameterValue("Region", Settings.Region);

                IReports objReports = String.IsNullOrEmpty(_ExchangeConnectionString) ? ReportsBusinessObject.CreateInstance() : ReportsBusinessObject.CreateInstance(_ExchangeConnectionString, _TicketingConnectionString);

                List<ServerDetails> DbConnectionInfo = String.IsNullOrEmpty(_ExchangeConnectionString) ? objReports.GetDataBaseConnectionString() : objReports.GetDataBaseConnectionString(BMC.Business.CashDeskOperator.CommonUtilities.SiteConnectionString(_ExchangeConnectionString));

                ConnectionInfo myConnectionInfo = new ConnectionInfo();


                myConnectionInfo.ServerName = DbConnectionInfo[0].ServerName;  // read this information from config file.
                myConnectionInfo.DatabaseName = DbConnectionInfo[0].DataBase; // read this information from config file.
                myConnectionInfo.UserID = DbConnectionInfo[0].Username;  // read this information from config file.
                myConnectionInfo.Password = DbConnectionInfo[0].Password;  // read this information from config file.


                crDatabase = oCollectionReport.Database;
                crTables = crDatabase.Tables;

                foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
                {
                    tableLogonInfo = crTable.LogOnInfo;
                    tableLogonInfo.ConnectionInfo = myConnectionInfo;
                    crTable.ApplyLogOnInfo(tableLogonInfo);
                }


                LogManager.WriteLog("Report Parameters set successfully", LogManager.enumLogLevel.Info);


                LogManager.WriteLog("Setting Report Source...", LogManager.enumLogLevel.Info);

                crystalReportViewer.ReportSource = oCollectionReport;


                LogManager.WriteLog("Report Source set successfully", LogManager.enumLogLevel.Info);

                AuditReports();

                strReportName = "batch winloss report";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
    }

    #endregion Class CReportViewer

    public static class DbConnectionDetails
    {
        static string strConnect = string.Empty;
        static string serverName = string.Empty;
        static string userName = string.Empty;
        static string password = string.Empty;
        static string databaseName = string.Empty;


        public static string ConnectionProperties
        {
            get
            {
                try
                {

                    if (databaseName.ToUpper() == "EXCHANGE")
                        strConnect = oCommonUtilities.CreateInstance().GetConnectionString();
                    else if (databaseName.ToUpper() == "TICKETING")
                        strConnect = oCommonUtilities.CreateInstance().GetTicketConnectionString();
                    if (strConnect != string.Empty)
                    {
                        try
                        {
                            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(strConnect);
                            ServerName = builder.DataSource;
                            UserName = builder.UserID;
                            Password = builder.Password;

                            // The following code was commented because if the connection string is configured in jumbled manner
                            //List<string> databaseInfo = strConnect.Split(';').ToList();

                            //ServerName = databaseInfo[0].Split('=')[1].ToString();
                            //UserName = databaseInfo[2].Split('=')[1].ToString();
                            //Password = databaseInfo[3].Split('=')[1].ToString();
                            ////DatabaseName = databaseInfo[1].Split('=')[1].ToString();

                            BMC.Common.LogManagement.LogManager.WriteLog("Database details retrieved successfully",
                                BMC.Common.LogManagement.LogManager.enumLogLevel.Info);
                        }
                        catch (Exception ex)
                        {
                            BMC.Common.LogManagement.LogManager.WriteLog("Error in retrieving the database details",
                                BMC.Common.LogManagement.LogManager.enumLogLevel.Error);
                            BMC.Common.ExceptionManagement.ExceptionManager.Publish(ex);
                        }
                    }
                    else
                    {
                        BMC.Common.LogManagement.LogManager.WriteLog("Error in retrieving the database details",
                            BMC.Common.LogManagement.LogManager.enumLogLevel.Error);
                    }
                }

                catch (SqlException ex)
                { BMC.Common.ExceptionManagement.ExceptionManager.Publish(ex); }

                catch (Exception ex)
                { BMC.Common.ExceptionManagement.ExceptionManager.Publish(ex); }
                return strConnect;

            }
        }

        public static string ServerName
        {
            get { return DbConnectionDetails.serverName; }
            set { DbConnectionDetails.serverName = value; }
        }

        public static string UserName
        {
            get { return DbConnectionDetails.userName; }
            set { DbConnectionDetails.userName = value; }
        }

        public static string Password
        {
            get { return DbConnectionDetails.password; }
            set { DbConnectionDetails.password = value; }
        }

        public static string DatabaseName
        {
            get { return DbConnectionDetails.databaseName; }
            set { DbConnectionDetails.databaseName = value;
            string empty = ConnectionProperties;
            }
        }
    }
}
