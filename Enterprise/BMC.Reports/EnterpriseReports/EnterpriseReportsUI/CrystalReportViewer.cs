using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.EnterpriseReportsUI.Reports;
using BMC.Common.Utilities;
using BMC.Common.ExceptionManagement;
using BMC.Security;
using BMC.Common.ConfigurationManagement;
using BMC.EnterpriseReportsBusiness;
using BMC.Common.LogManagement;
using BMC.EnterpriseReportsDataAccess;
using CrystalDecisions.Shared;
using BMC.EnterpriseReportsTransport;
//using CrystalDecisions.CrystalReports.Engine;
using System.Threading;
using System.Globalization;
using BMC.Common;
namespace BMC.EnterpriseReportsUI
{
    public partial class CrystalReportViewer : Form
    {
        string sVersion = string.Empty;
        string sCurrentCurrenyCulture = string.Empty;
        string sCurrencySymbol = string.Empty;
        ReportsBusiness oReportsBusiness = null;
      
        public CrystalReportViewer(string sReportName)
        {
            InitializeComponent();
            setTagProperty();
            oReportsBusiness = new ReportsBusiness();
            sVersion = oReportsBusiness.GetBMCVersion();
            sCurrentCurrenyCulture = oReportsBusiness.GetCurrentCurrenyCulture();
            sCurrencySymbol = CurrencySymbolofCulture(sCurrentCurrenyCulture);            
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(sCurrentCurrenyCulture); 

            crystalReportViewer1.ShowRefreshButton = false;
            crystalReportViewer1.ShowGroupTreeButton = false;
            crystalReportViewer1.DisplayGroupTree = false;
            this.Text = sReportName;
        }

        private void setTagProperty()
        {
            this.Tag = "Key_BMCReports";
        }

        public string CurrencySymbolofCulture(string CurrencyCulture)
        {
            return new RegionInfo(sCurrentCurrenyCulture).CurrencySymbol;
        }

        public void ShowVoucherCouponLiabilityReport(string reportName, DataSet dsReportsDataSet, DateTime issueDate, string sDeviceType, string sVoucherStatus, string SiteName, int Zone, string Company, string SubCompany, string ZoneName, bool GroupByZone,string sSiteIdList)
        {
            try
            {

                var vVoucherCouponLiabilityReport = new VoucherCouponLiabilityReport();
                dsReportsDataSet.Tables[0].TableName = "VoucherCouponLiability";

                vVoucherCouponLiabilityReport.SetDataSource(dsReportsDataSet);
                vVoucherCouponLiabilityReport.SetParameterValue("issueDate", issueDate);
                vVoucherCouponLiabilityReport.SetParameterValue("DeviceType", sDeviceType);
                vVoucherCouponLiabilityReport.SetParameterValue("VoucherStatus", sVoucherStatus);

                //GetVersion_SiteName(out  sVersion, out  sSiteName);
                vVoucherCouponLiabilityReport.SetParameterValue("Zone", Zone);
                vVoucherCouponLiabilityReport.SetParameterValue("siteName", SiteName);
                vVoucherCouponLiabilityReport.SetParameterValue("BMCVersion", sVersion);
                vVoucherCouponLiabilityReport.SetParameterValue("CurrencyCulture", sCurrentCurrenyCulture);
                vVoucherCouponLiabilityReport.SetParameterValue("CurrencySymbol", sCurrencySymbol);
                vVoucherCouponLiabilityReport.SetParameterValue("DateCulture", sCurrentCurrenyCulture);
                vVoucherCouponLiabilityReport.SetParameterValue("CompanyName", Company);
                vVoucherCouponLiabilityReport.SetParameterValue("SubCompanyName", SubCompany);
                vVoucherCouponLiabilityReport.SetParameterValue("ZoneName", ZoneName);
                vVoucherCouponLiabilityReport.SetParameterValue("@GROUPBYZONE", GroupByZone);
                vVoucherCouponLiabilityReport.SetParameterValue("@SiteIDList", sSiteIdList);
                crystalReportViewer1.ReportSource = vVoucherCouponLiabilityReport;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }


        public void ShowRedeemedVoucherByDeviceReport(string reportName, DataSet dsReportsDataSet, DateTime startDate, DateTime endDate, string sDeviceType, string SiteName, string Zone, string CompanyName, string SubCompanyName, bool GroupByZone, string sSiteIDList)
        {
            try
            {
                var vRedeemedVoucherByDeviceReport = new RedeemedTicketByDeviceReport();
                dsReportsDataSet.Tables[0].TableName = "RedeemedTicketByDevice";

                vRedeemedVoucherByDeviceReport.SetDataSource(dsReportsDataSet);
                vRedeemedVoucherByDeviceReport.SetParameterValue("fromDate", startDate);
                vRedeemedVoucherByDeviceReport.SetParameterValue("toDate", endDate);
                vRedeemedVoucherByDeviceReport.SetParameterValue("DeviceType", sDeviceType);
                vRedeemedVoucherByDeviceReport.SetParameterValue("Zone", Zone);



                //GetVersion_SiteName(out  sVersion, out  sSiteName);

                vRedeemedVoucherByDeviceReport.SetParameterValue("siteName", SiteName);
                vRedeemedVoucherByDeviceReport.SetParameterValue("BMCVersion", sVersion);
                vRedeemedVoucherByDeviceReport.SetParameterValue("CurrencyCulture", sCurrentCurrenyCulture);
                vRedeemedVoucherByDeviceReport.SetParameterValue("CurrencySymbol", sCurrencySymbol);
                vRedeemedVoucherByDeviceReport.SetParameterValue("DateCulture", sCurrentCurrenyCulture);
                vRedeemedVoucherByDeviceReport.SetParameterValue("CompanyName", CompanyName);
                vRedeemedVoucherByDeviceReport.SetParameterValue("SubCompanyName", SubCompanyName);
                vRedeemedVoucherByDeviceReport.SetParameterValue("@GROUPBYZONE", GroupByZone);
                vRedeemedVoucherByDeviceReport.SetParameterValue("@SiteIDList", sSiteIDList);
                crystalReportViewer1.ReportSource = vRedeemedVoucherByDeviceReport;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        public void ShowExpiredVoucherCoupon(string reportName, DataSet dsReportsDataSet, DateTime fromdate, DateTime toDate, string DeviceType, String SiteName, string CompanyName, string SubCompanyName, string sSiteIDList)
        {
            try
            {
                var vExpiredVoucherCoupon = new ExpiredVoucherCouponReport();
                dsReportsDataSet.Tables[0].TableName = "ExpiredVoucherCoupon";

                vExpiredVoucherCoupon.SetDataSource(dsReportsDataSet);
                vExpiredVoucherCoupon.SetParameterValue("fromdate", fromdate);
                vExpiredVoucherCoupon.SetParameterValue("toDate", toDate);
                vExpiredVoucherCoupon.SetParameterValue("DeviceType", DeviceType);

                //GetVersion_SiteName(out  sVersion, out  sSiteName);
                vExpiredVoucherCoupon.SetParameterValue("siteName", SiteName);
                vExpiredVoucherCoupon.SetParameterValue("BMCVersion", sVersion);
                vExpiredVoucherCoupon.SetParameterValue("CurrencyCulture", sCurrentCurrenyCulture);
                vExpiredVoucherCoupon.SetParameterValue("CurrencySymbol", sCurrencySymbol);
                vExpiredVoucherCoupon.SetParameterValue("DateCulture", sCurrentCurrenyCulture);
                vExpiredVoucherCoupon.SetParameterValue("CompanyName", CompanyName);
                vExpiredVoucherCoupon.SetParameterValue("SubCompanyName", SubCompanyName);
                vExpiredVoucherCoupon.SetParameterValue("@SiteIDList", sSiteIDList);
                crystalReportViewer1.ReportSource = vExpiredVoucherCoupon;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        public void ShowLiabilityTransferSummary(String reportName, DataSet dsReportsDataSet, DateTime fromdate, DateTime toDate, int CompanyName, int SubCompanyName, String strCompanyName, String strSubCompanyName, int SiteID, String SiteName, int ZoneID,string sSiteIDList)
        {
            try
            {
                CrystalDecisions.CrystalReports.Engine.Database crDatabase;
                CrystalDecisions.CrystalReports.Engine.Tables crTables;

                TableLogOnInfo tableLogonInfo = new TableLogOnInfo();
                ConnectionInfo connectionInfo = new ConnectionInfo();

                connectionInfo.ServerName = DbConnection.ServerName;
                connectionInfo.UserID = DbConnection.UserName;
                connectionInfo.Password = DbConnection.Password;
                connectionInfo.DatabaseName = DbConnection.DatabaseName;

                var vLiabilityTransferSummary = new CrossPropertyLiabilityTransferSummaryReport();
                dsReportsDataSet.Tables[0].TableName = "LiabilityTransferSummary";

                vLiabilityTransferSummary.SetDataSource(dsReportsDataSet);
                vLiabilityTransferSummary.SetParameterValue("@StartDate", fromdate);
                vLiabilityTransferSummary.SetParameterValue("@EndDate", toDate);
                vLiabilityTransferSummary.SetParameterValue("@Company", CompanyName);
                vLiabilityTransferSummary.SetParameterValue("@SubCompany", SubCompanyName);
                vLiabilityTransferSummary.SetParameterValue("@CompanyName", strCompanyName);
                vLiabilityTransferSummary.SetParameterValue("@SubCompanyName", strSubCompanyName);
                vLiabilityTransferSummary.SetParameterValue("@SITE", SiteID);
                vLiabilityTransferSummary.SetParameterValue("siteName", SiteName);
                //vLiabilityTransferSummary.SetParameterValue("@ZONE", ZoneID);
                vLiabilityTransferSummary.SetParameterValue("BMCVersion", sVersion);
                vLiabilityTransferSummary.SetParameterValue("CurrencyCulture", sCurrentCurrenyCulture);
                vLiabilityTransferSummary.SetParameterValue("CurrencySymbol", sCurrencySymbol);
                vLiabilityTransferSummary.SetParameterValue("DateCulture", sCurrentCurrenyCulture);
                vLiabilityTransferSummary.SetParameterValue("@SiteIDList", sSiteIDList);
                crDatabase = vLiabilityTransferSummary.Database;
                crTables = crDatabase.Tables;

                foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
                {
                    tableLogonInfo = crTable.LogOnInfo;
                    tableLogonInfo.ConnectionInfo = connectionInfo;
                    crTable.ApplyLogOnInfo(tableLogonInfo);
                }

                crystalReportViewer1.ReportSource = vLiabilityTransferSummary;
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        public void ShowVoucherListingReport(string reportName, DataSet dsReportsDataSet, DateTime fromdate, DateTime toDate, string sStatus, string sSlot, string SiteName, string zone, string Company, string SubCompany, bool GroupByZone, string sSiteIDList)
        {
            try
            {

                CrystalDecisions.CrystalReports.Engine.Database crDatabase;
                CrystalDecisions.CrystalReports.Engine.Tables crTables;

                TableLogOnInfo tableLogonInfo = new TableLogOnInfo();
                ConnectionInfo connectionInfo = new ConnectionInfo();

                connectionInfo.ServerName = DbConnection.ServerName;
                connectionInfo.UserID = DbConnection.UserName;
                connectionInfo.Password = DbConnection.Password;
                connectionInfo.DatabaseName = DbConnection.DatabaseName;

                var vVoucherListingReport = new VoucherListingReport();
                dsReportsDataSet.Tables[0].TableName = "VoucherListingReport";
                vVoucherListingReport.SetDataSource(dsReportsDataSet);
                vVoucherListingReport.SetParameterValue("fromDate", fromdate);
                vVoucherListingReport.SetParameterValue("toDate", toDate);
                vVoucherListingReport.SetParameterValue("Status", sStatus);
                vVoucherListingReport.SetParameterValue("Slot", sSlot);
                vVoucherListingReport.SetParameterValue("Zone", zone);

                vVoucherListingReport.SetParameterValue("siteName", SiteName);
                vVoucherListingReport.SetParameterValue("BMCVersion", sVersion);
                vVoucherListingReport.SetParameterValue("CurrencyCulture", sCurrentCurrenyCulture);
                vVoucherListingReport.SetParameterValue("CurrencySymbol", sCurrencySymbol);
                vVoucherListingReport.SetParameterValue("DateCulture", sCurrentCurrenyCulture);
                vVoucherListingReport.SetParameterValue("CompanyName", Company);
                vVoucherListingReport.SetParameterValue("SubCompanyName", SubCompany);
                vVoucherListingReport.SetParameterValue("@GROUPBYZONE", GroupByZone);
                vVoucherListingReport.SetParameterValue("@SiteIDList", sSiteIDList);
                crystalReportViewer1.ReportSource = vVoucherListingReport;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        public void ShowPromotionalVoucherListingReport(string reportName, DataSet dsReportsDataSet, DateTime fromdate, DateTime toDate, string sStatus, string sSlot, string SiteName, string Company,
            string SubCompany, int nCompany, int nSubCompany, int nSite, DateTime StartDate, DateTime EndDate, String VoucherStatus, string sSiteIDList)
        {
            try
            {

                CrystalDecisions.CrystalReports.Engine.Database crDatabase;
                CrystalDecisions.CrystalReports.Engine.Tables crTables;

                TableLogOnInfo tableLogonInfo = new TableLogOnInfo();
                ConnectionInfo connectionInfo = new ConnectionInfo();

                connectionInfo.ServerName = DbConnection.ServerName;
                connectionInfo.UserID = DbConnection.UserName;
                connectionInfo.Password = DbConnection.Password;
                connectionInfo.DatabaseName = DbConnection.DatabaseName;

                var vPromoVoucherListingReport = new PromotionalVoucherListingReport();
                dsReportsDataSet.Tables[0].TableName = "PromotionalVoucherListingReport";
                vPromoVoucherListingReport.SetDataSource(dsReportsDataSet);
                vPromoVoucherListingReport.SetParameterValue("fromDate", fromdate);
                vPromoVoucherListingReport.SetParameterValue("toDate", toDate);
                vPromoVoucherListingReport.SetParameterValue("Status", sStatus);
               
              //  vPromoVoucherListingReport.SetParameterValue("Zone", zone);

                vPromoVoucherListingReport.SetParameterValue("siteName", SiteName);
                vPromoVoucherListingReport.SetParameterValue("BMCVersion", sVersion);
                vPromoVoucherListingReport.SetParameterValue("CurrencyCulture", sCurrentCurrenyCulture);
                vPromoVoucherListingReport.SetParameterValue("CurrencySymbol", sCurrencySymbol);
                vPromoVoucherListingReport.SetParameterValue("DateCulture", sCurrentCurrenyCulture);
                vPromoVoucherListingReport.SetParameterValue("CompanyName", Company);
                vPromoVoucherListingReport.SetParameterValue("SubCompanyName", SubCompany);
                vPromoVoucherListingReport.SetParameterValue("@Company", nCompany);
                vPromoVoucherListingReport.SetParameterValue("@SUBCOMPANY", nSubCompany);
                vPromoVoucherListingReport.SetParameterValue("@SITE", nSite);
//                vPromoVoucherListingReport.SetParameterValue("@Zone", nZone);
                vPromoVoucherListingReport.SetParameterValue("@StartDate", StartDate);
                vPromoVoucherListingReport.SetParameterValue("@EndDate", EndDate);
                vPromoVoucherListingReport.SetParameterValue("@VoucherStatus", VoucherStatus);
                vPromoVoucherListingReport.SetParameterValue("@SiteIDList", sSiteIDList);
                crDatabase = vPromoVoucherListingReport.Database;
                crTables = crDatabase.Tables;

                foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
                {
                    tableLogonInfo = crTable.LogOnInfo;
                    tableLogonInfo.ConnectionInfo = connectionInfo;
                    crTable.ApplyLogOnInfo(tableLogonInfo);
                }

                crystalReportViewer1.ReportSource = vPromoVoucherListingReport;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        

        public void ShowJackpotSlipSummaryReport(DataSet dsReportsDataSet, DateTime startDate, DateTime EndDate, int site, string SiteName,
            string company, string subCompany, bool ShowHandpay, bool ShowJackpot, string SiteIDList)
        {
            try
            {
                JackpotSlipSummaryReport jackpotSlipSummaryReport = new JackpotSlipSummaryReport();
                dsReportsDataSet.Tables[0].TableName = "JackpotSlipSummaryDetails";

                jackpotSlipSummaryReport.SetDataSource(dsReportsDataSet);

                jackpotSlipSummaryReport.SetParameterValue("Site", site);
                jackpotSlipSummaryReport.SetParameterValue("SiteName", SiteName);
                jackpotSlipSummaryReport.SetParameterValue("Version", sVersion);
                jackpotSlipSummaryReport.SetParameterValue("ReportStartDate", startDate);
                jackpotSlipSummaryReport.SetParameterValue("ReportEndDate", EndDate);
                jackpotSlipSummaryReport.SetParameterValue("ShowHandpay", ShowHandpay);
                jackpotSlipSummaryReport.SetParameterValue("ShowJackpot", ShowJackpot);
                jackpotSlipSummaryReport.SetParameterValue("CurrencySymbol", sCurrencySymbol);
                jackpotSlipSummaryReport.SetParameterValue("CurrencyCulture", sCurrentCurrenyCulture);
                jackpotSlipSummaryReport.SetParameterValue("DateCulture", sCurrentCurrenyCulture);
                jackpotSlipSummaryReport.SetParameterValue("Company", company);
                jackpotSlipSummaryReport.SetParameterValue("SubCompany", subCompany);
                jackpotSlipSummaryReport.SetParameterValue("@SiteIDList", SiteIDList);

                crystalReportViewer1.ReportSource = jackpotSlipSummaryReport;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ShowExpenseDetailsReport(DataSet dsReportsDataSet, DateTime reportDate, string reportPeriod, string company, string subCompany, int Site, string siteName, string SiteIDList, bool IsGamingDayBasedReport)
        {

            try
            {
                ExpenseDetailsReport expenseDetailsReport = new ExpenseDetailsReport();
                dsReportsDataSet.Tables[0].TableName = "ExpenseDetails";
                //dsReportsDataSet.Tables[1].TableName = "SummarizedExpenseDetails";
                //dsReportsDataSet.Tables[2].TableName = "SummExpenseDetails";

                expenseDetailsReport.SetDataSource(dsReportsDataSet);

                expenseDetailsReport.SetParameterValue("Company", company);
                expenseDetailsReport.SetParameterValue("SubCompany", subCompany);
                expenseDetailsReport.SetParameterValue("Site", Site);
                expenseDetailsReport.SetParameterValue("SiteName", siteName);
                expenseDetailsReport.SetParameterValue("Version", sVersion);
                expenseDetailsReport.SetParameterValue("ReportDate", reportDate);
                expenseDetailsReport.SetParameterValue("ReportPeriod", reportPeriod);
                expenseDetailsReport.SetParameterValue("CurrencyCulture", sCurrentCurrenyCulture);
                expenseDetailsReport.SetParameterValue("DateCulture", sCurrentCurrenyCulture);
                expenseDetailsReport.SetParameterValue("CurrencySymbol", sCurrencySymbol);
                expenseDetailsReport.SetParameterValue("@SiteIDList", SiteIDList);
                expenseDetailsReport.SetParameterValue("IsGamingDayBasedReport", IsGamingDayBasedReport);
                //expenseDetailsReport.SetParameterValue("CurrencyCulture", sCurrentCurrenyCulture, "ExpenseDetailsSummaryReport.rpt");
                //expenseDetailsReport.SetParameterValue("CurrencySymbol", sCurrencySymbol, "ExpenseDetailsSummaryReport.rpt");

                crystalReportViewer1.ReportSource = expenseDetailsReport;


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ShowCoinInByPayTableReport(DataSet dsReportsDataSet, DateTime startDate, DateTime endDate, string company, string subCompany, int companyID, int subCompanyID, int siteID, string siteName, string groupByOption,string SiteIDList)
        {
            try
            {
                CrystalDecisions.CrystalReports.Engine.Database crDatabase;
                CrystalDecisions.CrystalReports.Engine.Tables crTables;

                TableLogOnInfo tableLogonInfo = new TableLogOnInfo();
                ConnectionInfo connectionInfo = new ConnectionInfo();

                connectionInfo.ServerName = DbConnection.ServerName;
                connectionInfo.UserID = DbConnection.UserName;
                connectionInfo.Password = DbConnection.Password;
                connectionInfo.DatabaseName = DbConnection.DatabaseName;

                CoinInByPayTableReport coinInByPayTableReport = new CoinInByPayTableReport();

                coinInByPayTableReport.SetParameterValue("@Company", companyID);
                coinInByPayTableReport.SetParameterValue("CompanyName", company);
                coinInByPayTableReport.SetParameterValue("@SubCompany", subCompanyID);
                coinInByPayTableReport.SetParameterValue("SubCompanyName", subCompany);
                coinInByPayTableReport.SetParameterValue("@Site", siteID);
                coinInByPayTableReport.SetParameterValue("SiteName", siteName);
                coinInByPayTableReport.SetParameterValue("@StartDate", startDate);
                coinInByPayTableReport.SetParameterValue("@EndDate", endDate);
                coinInByPayTableReport.SetParameterValue("Version", sVersion);
                coinInByPayTableReport.SetParameterValue("CurrencySymbol", sCurrencySymbol);
                coinInByPayTableReport.SetParameterValue("CurrencyCulture", sCurrentCurrenyCulture);
                coinInByPayTableReport.SetParameterValue("DateCulture", sCurrentCurrenyCulture);
                coinInByPayTableReport.SetParameterValue("@GroupBy", groupByOption);
                coinInByPayTableReport.SetParameterValue("GroupBy2", groupByOption == "Asset" ? "Denom" : "Asset");
                coinInByPayTableReport.SetParameterValue("@SiteIDList", SiteIDList);

                crDatabase = coinInByPayTableReport.Database;
                crTables = crDatabase.Tables;

                foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
                {
                    tableLogonInfo = crTable.LogOnInfo;
                    tableLogonInfo.ConnectionInfo = connectionInfo;
                    crTable.ApplyLogOnInfo(tableLogonInfo);
                }

                crystalReportViewer1.ReportSource = coinInByPayTableReport;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ShowMultiGameSlotDetailReport(DataSet dsReportsDataSet, DateTime startDate, DateTime endDate, int companyID, int subCompanyID, string company, string subCompany, int siteID, string siteName, string groupByOption, string SiteIDList)
        {
            try
            {
                CrystalDecisions.CrystalReports.Engine.Database crDatabase;
                CrystalDecisions.CrystalReports.Engine.Tables crTables;

                TableLogOnInfo tableLogonInfo = new TableLogOnInfo();
                ConnectionInfo connectionInfo = new ConnectionInfo();

                connectionInfo.ServerName = DbConnection.ServerName;
                connectionInfo.UserID = DbConnection.UserName;
                connectionInfo.Password = DbConnection.Password;
                connectionInfo.DatabaseName = DbConnection.DatabaseName;

                MultiGameSlotDetailReport multiGameSlotDetailReport = new MultiGameSlotDetailReport();

                multiGameSlotDetailReport.SetParameterValue("@Company", companyID);
                multiGameSlotDetailReport.SetParameterValue("CompanyName", company);
                multiGameSlotDetailReport.SetParameterValue("@SubCompany", subCompanyID);
                multiGameSlotDetailReport.SetParameterValue("SubCompanyName", subCompany);
                multiGameSlotDetailReport.SetParameterValue("@Site", siteID);
                multiGameSlotDetailReport.SetParameterValue("SiteName", siteName);
                multiGameSlotDetailReport.SetParameterValue("@StartDate", startDate);
                multiGameSlotDetailReport.SetParameterValue("@EndDate", endDate);
                multiGameSlotDetailReport.SetParameterValue("Version", sVersion);
                multiGameSlotDetailReport.SetParameterValue("CurrencySymbol", sCurrencySymbol);
                multiGameSlotDetailReport.SetParameterValue("CurrencyCulture", sCurrentCurrenyCulture);
                multiGameSlotDetailReport.SetParameterValue("DateCulture", sCurrentCurrenyCulture);
                multiGameSlotDetailReport.SetParameterValue("@GroupBy", groupByOption);
                multiGameSlotDetailReport.SetParameterValue("GroupBy2", groupByOption == "Game" ? "Asset" : "Game");
                multiGameSlotDetailReport.SetParameterValue("@SiteIDList", SiteIDList);
                crDatabase = multiGameSlotDetailReport.Database;
                crTables = crDatabase.Tables;

                foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
                {
                    tableLogonInfo = crTable.LogOnInfo;
                    tableLogonInfo.ConnectionInfo = connectionInfo;
                    crTable.ApplyLogOnInfo(tableLogonInfo);
                }

                crystalReportViewer1.ReportSource = multiGameSlotDetailReport;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ShowMultiDenomSlotDetailReport(DataSet dsReportsDataSet, DateTime startDate, DateTime endDate, int companyID, int subCompanyID, string company, string subCompany, int siteID, string siteName, string groupByOption, string SiteIDList)
        {
            try
            {
                CrystalDecisions.CrystalReports.Engine.Database crDatabase;
                CrystalDecisions.CrystalReports.Engine.Tables crTables;

                TableLogOnInfo tableLogonInfo = new TableLogOnInfo();
                ConnectionInfo connectionInfo = new ConnectionInfo();

                connectionInfo.ServerName = DbConnection.ServerName;
                connectionInfo.UserID = DbConnection.UserName;
                connectionInfo.Password = DbConnection.Password;
                connectionInfo.DatabaseName = DbConnection.DatabaseName;

                MultiDenomSlotDetailReport multiDenomSlotDetailReport = new MultiDenomSlotDetailReport();

                multiDenomSlotDetailReport.SetParameterValue("@Company", companyID);
                multiDenomSlotDetailReport.SetParameterValue("CompanyName", company);
                multiDenomSlotDetailReport.SetParameterValue("@SubCompany", subCompanyID);
                multiDenomSlotDetailReport.SetParameterValue("SubCompanyName", subCompany);
                multiDenomSlotDetailReport.SetParameterValue("@Site", siteID);
                multiDenomSlotDetailReport.SetParameterValue("SiteName", siteName);
                multiDenomSlotDetailReport.SetParameterValue("@StartDate", startDate);
                multiDenomSlotDetailReport.SetParameterValue("@EndDate", endDate);
                multiDenomSlotDetailReport.SetParameterValue("Version", sVersion);
                multiDenomSlotDetailReport.SetParameterValue("CurrencySymbol", sCurrencySymbol);
                multiDenomSlotDetailReport.SetParameterValue("CurrencyCulture", sCurrentCurrenyCulture);
                multiDenomSlotDetailReport.SetParameterValue("DateCulture", sCurrentCurrenyCulture);
                multiDenomSlotDetailReport.SetParameterValue("@GroupBy", groupByOption);
                multiDenomSlotDetailReport.SetParameterValue("GroupBy2", groupByOption == "Asset" ? "Denom" : "Asset");
                multiDenomSlotDetailReport.SetParameterValue("@SiteIDList", SiteIDList);
                crDatabase = multiDenomSlotDetailReport.Database;
                crTables = crDatabase.Tables;

                foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
                {
                    tableLogonInfo = crTable.LogOnInfo;
                    tableLogonInfo.ConnectionInfo = connectionInfo;
                    crTable.ApplyLogOnInfo(tableLogonInfo);
                }

                crystalReportViewer1.ReportSource = multiDenomSlotDetailReport;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public void ShowMultiGameMultiDenomReport(DataSet dsReportsDataSet, DateTime fromdate, DateTime toDate, string SiteName, string sGroupBY)
        //{

        //    try
        //    {

        //        var vMultiGameMultiDenomReport = new MultiGameMultiDenomReport();
        //        dsReportsDataSet.Tables[0].TableName = "MultiGameMultiDenom";

        //        vMultiGameMultiDenomReport.SetDataSource(dsReportsDataSet);
        //        vMultiGameMultiDenomReport.SetParameterValue("fromdate", fromdate);
        //        vMultiGameMultiDenomReport.SetParameterValue("toDate", toDate);

        //        switch (sGroupBY)
        //        {
        //            case "GAME":
        //                vMultiGameMultiDenomReport.DataDefinition.FormulaFields["GroupName"].Text = "{MultiGameMultiDenom.AliasGameName}";
        //                vMultiGameMultiDenomReport.SetParameterValue("GroupHeader", "Game: ");
        //                vMultiGameMultiDenomReport.SetParameterValue("CurrencySymbol", "");//sCurrencySymbol);
        //                break;
        //            case "DENOM":
        //                vMultiGameMultiDenomReport.DataDefinition.FormulaFields["GroupName"].Text = "{MultiGameMultiDenom.MGMD_Denom_Value}";
        //                vMultiGameMultiDenomReport.SetParameterValue("GroupHeader", "DENOM: ");
        //                vMultiGameMultiDenomReport.SetParameterValue("CurrencySymbol", "$");//sCurrencySymbol);
        //                break;
        //            case "PAYTABLE":
        //                vMultiGameMultiDenomReport.DataDefinition.FormulaFields["GroupName"].Text = "{MultiGameMultiDenom.PaytableDescription}";
        //                vMultiGameMultiDenomReport.SetParameterValue("GroupHeader", "PAYTABLE: ");
        //                vMultiGameMultiDenomReport.SetParameterValue("CurrencySymbol", "");//sCurrencySymbol);
        //                break;
        //        }


        //        vMultiGameMultiDenomReport.SetParameterValue("siteName", SiteName);
        //        vMultiGameMultiDenomReport.SetParameterValue("BMCVersion", sVersion);
        //        vMultiGameMultiDenomReport.SetParameterValue("CurrencyCulture", "en-US");// sCurrentCurrenyCulture);                
        //        vMultiGameMultiDenomReport.SetParameterValue("DateCulture", "en-US");//sCurrentCurrenyCulture);

        //        crystalReportViewer1.ReportSource = vMultiGameMultiDenomReport;

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public void ShowMGMDDenomPerformanceReport(DataSet dsReportsDataSet, DateTime startDate, DateTime endDate,
            int company, int subCompany, int siteID, string siteName, string CompanyName, string SubCompanyName, string SiteIDList)
        {
            try
            {
                CrystalDecisions.CrystalReports.Engine.Database crDatabase;
                CrystalDecisions.CrystalReports.Engine.Tables crTables;

                TableLogOnInfo tableLogonInfo = new TableLogOnInfo();
                ConnectionInfo connectionInfo = new ConnectionInfo();

                connectionInfo.ServerName = DbConnection.ServerName;
                connectionInfo.UserID = DbConnection.UserName;
                connectionInfo.Password = DbConnection.Password;
                connectionInfo.DatabaseName = DbConnection.DatabaseName;

                MGMDDenomPerformance denomPerformanceReport = new MGMDDenomPerformance();

                denomPerformanceReport.SetParameterValue("@Company", company);
                denomPerformanceReport.SetParameterValue("@SubCompany", subCompany);
                denomPerformanceReport.SetParameterValue("@Site", siteID);
                denomPerformanceReport.SetParameterValue("@StartDate", startDate);
                denomPerformanceReport.SetParameterValue("@EndDate", endDate);
                denomPerformanceReport.SetParameterValue("Version", sVersion);
                denomPerformanceReport.SetParameterValue("CurrencySymbol", sCurrencySymbol);
                denomPerformanceReport.SetParameterValue("CurrencyCulture", sCurrentCurrenyCulture);
                denomPerformanceReport.SetParameterValue("DateCulture", sCurrentCurrenyCulture);
                denomPerformanceReport.SetParameterValue("SiteName", siteName);
                denomPerformanceReport.SetParameterValue("@CompanyName", CompanyName);
                denomPerformanceReport.SetParameterValue("@SubCompanyName", SubCompanyName);
                denomPerformanceReport.SetParameterValue("@SiteIDList", SiteIDList);


                crDatabase = denomPerformanceReport.Database;
                crTables = crDatabase.Tables;

                foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
                {
                    tableLogonInfo = crTable.LogOnInfo;
                    tableLogonInfo.ConnectionInfo = connectionInfo;
                    crTable.ApplyLogOnInfo(tableLogonInfo);
                }

                crystalReportViewer1.ReportSource = denomPerformanceReport;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("ShowMGMDGamePerformanceReport", LogManager.enumLogLevel.Info);
                ExceptionManager.Publish(ex);
            }
        }

        public void ShowAuditTrailReport(string reportName, DataSet dsReportsDataSet, DateTime fromdate, DateTime toDate,
            string sModuleID, int Rows, bool bLocal, string sSiteName)
        {
            try
            {

                var vAuditTrailReport = new AuditTrailReport();

                vAuditTrailReport.SetDataSource(dsReportsDataSet);
                vAuditTrailReport.SetParameterValue("fromdate", fromdate);
                vAuditTrailReport.SetParameterValue("toDate", toDate);
                vAuditTrailReport.SetParameterValue("ModuleID", sModuleID.ToString());

                // GetVersion_SiteName(out  sVersion, out  sSiteName);
                vAuditTrailReport.SetParameterValue("Rows", Rows);
                vAuditTrailReport.SetParameterValue("Local", bLocal);
                vAuditTrailReport.SetParameterValue("Sites", sSiteName);//sCurrentCurrenyCulture);
                vAuditTrailReport.SetParameterValue("DateCulture", "en-US");//sCurrentCurrenyCulture);

                crystalReportViewer1.ReportSource = vAuditTrailReport;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        public void ShowMGMDGamePerformanceReport(DataSet dsReportsDataSet, DateTime startDate, DateTime endDate, int company,
            int subCompany, int siteID, string siteName, int Zone, string CompanyName, string SubCompanyName, string ZoneName, string SiteIDList)
        {

            try
            {
                CrystalDecisions.CrystalReports.Engine.Database crDatabase;
                CrystalDecisions.CrystalReports.Engine.Tables crTables;

                TableLogOnInfo tableLogonInfo = new TableLogOnInfo();
                ConnectionInfo connectionInfo = new ConnectionInfo();

                connectionInfo.ServerName = DbConnection.ServerName;
                connectionInfo.UserID = DbConnection.UserName;
                connectionInfo.Password = DbConnection.Password;
                connectionInfo.DatabaseName = DbConnection.DatabaseName;

                GamePerformance gamePerformanceReport = new GamePerformance();

                gamePerformanceReport.SetParameterValue("@Company", company);
                gamePerformanceReport.SetParameterValue("@SubCompany", subCompany);
                gamePerformanceReport.SetParameterValue("@Site", siteID);
                gamePerformanceReport.SetParameterValue("@Zone", Zone);
                gamePerformanceReport.SetParameterValue("@StartDate", startDate);
                gamePerformanceReport.SetParameterValue("@EndDate", endDate);
                gamePerformanceReport.SetParameterValue("Version", sVersion);
                gamePerformanceReport.SetParameterValue("CurrencySymbol", sCurrencySymbol);
                gamePerformanceReport.SetParameterValue("CurrencyCulture", sCurrentCurrenyCulture);
                gamePerformanceReport.SetParameterValue("DateCulture", sCurrentCurrenyCulture);

                gamePerformanceReport.SetParameterValue("SiteName", siteName);
                gamePerformanceReport.SetParameterValue("@CompanyName", CompanyName);
                gamePerformanceReport.SetParameterValue("@SubCompanyName", SubCompanyName);
                gamePerformanceReport.SetParameterValue("ZoneName", ZoneName);
                gamePerformanceReport.SetParameterValue("@SiteIDList", SiteIDList);

                crDatabase = gamePerformanceReport.Database;
                crTables = crDatabase.Tables;

                foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
                {
                    tableLogonInfo = crTable.LogOnInfo;
                    tableLogonInfo.ConnectionInfo = connectionInfo;
                    crTable.ApplyLogOnInfo(tableLogonInfo);
                }

                crystalReportViewer1.ReportSource = gamePerformanceReport;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("ShowMGMDGamePerformanceReport", LogManager.enumLogLevel.Info);
                ExceptionManager.Publish(ex);
            }
        }

        public void ShowMGMDGameReport(DataSet dsReportsDataSet, DateTime startDate, DateTime endDate, int company,
         int subCompany, int siteID, string siteName, int Zone, string CompanyName, string SubCompanyName, string ZoneName, string SiteIDList)
        {
            try
            {
                CrystalDecisions.CrystalReports.Engine.Database crDatabase;
                CrystalDecisions.CrystalReports.Engine.Tables crTables;

                TableLogOnInfo tableLogonInfo = new TableLogOnInfo();
                ConnectionInfo connectionInfo = new ConnectionInfo();

                connectionInfo.ServerName = DbConnection.ServerName;
                connectionInfo.UserID = DbConnection.UserName;
                connectionInfo.Password = DbConnection.Password;
                connectionInfo.DatabaseName = DbConnection.DatabaseName;

                MGMD_GameReport gameReport = new MGMD_GameReport();

                gameReport.SetParameterValue("@Company", company);
                gameReport.SetParameterValue("@SubCompany", subCompany);
                gameReport.SetParameterValue("@Site", siteID);
                gameReport.SetParameterValue("@Zone", Zone);
                gameReport.SetParameterValue("@StartDate", startDate);
                gameReport.SetParameterValue("@EndDate", endDate);
                gameReport.SetParameterValue("Version", sVersion);
                gameReport.SetParameterValue("CurrencySymbol", sCurrencySymbol);
                gameReport.SetParameterValue("CurrencyCulture", sCurrentCurrenyCulture);
                gameReport.SetParameterValue("DateCulture", sCurrentCurrenyCulture);
                gameReport.SetParameterValue("SiteName", siteName);
                gameReport.SetParameterValue("@CompanyName", CompanyName);
                gameReport.SetParameterValue("@SubCompanyName", SubCompanyName);
                gameReport.SetParameterValue("ZoneName", ZoneName);
                gameReport.SetParameterValue("@SiteIDList", SiteIDList);

                crDatabase = gameReport.Database;
                crTables = crDatabase.Tables;

                foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
                {
                    tableLogonInfo = crTable.LogOnInfo;
                    tableLogonInfo.ConnectionInfo = connectionInfo;
                    crTable.ApplyLogOnInfo(tableLogonInfo);
                }

                crystalReportViewer1.ReportSource = gameReport;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("ShowMGMDGamePerformanceReport", LogManager.enumLogLevel.Info);
                ExceptionManager.Publish(ex);
            }
        }
        //
        public void ShowStandardMeterComparisonReport(DataSet dsReportsDataSet, DateTime startDate, DateTime endDate, int company,
            int subCompany, int siteID, string SiteName, int zone, string RegionName, string AreaName, string DistrictName, string CompanyName, string SubCompanyName, bool GroupByZone, string SiteIDList,string ZoneName)
        {
            try
            {
                CrystalDecisions.CrystalReports.Engine.Database crDatabase;
                CrystalDecisions.CrystalReports.Engine.Tables crTables;

                TableLogOnInfo tableLogonInfo = new TableLogOnInfo();
                ConnectionInfo connectionInfo = new ConnectionInfo();

                connectionInfo.ServerName = DbConnection.ServerName;
                connectionInfo.UserID = DbConnection.UserName;
                connectionInfo.Password = DbConnection.Password;
                connectionInfo.DatabaseName = DbConnection.DatabaseName;

                MeterComparisonReport MeterComparisonReport = new MeterComparisonReport();

                MeterComparisonReport.SetParameterValue("@company", company);
                MeterComparisonReport.SetParameterValue("@subcompany", subCompany);
                MeterComparisonReport.SetParameterValue("@site", siteID);
                MeterComparisonReport.SetParameterValue("SiteName", SiteName);
                MeterComparisonReport.SetParameterValue("@zone", zone);
                MeterComparisonReport.SetParameterValue("@startdate", startDate.Date);
                MeterComparisonReport.SetParameterValue("@enddate", endDate.Date);
                MeterComparisonReport.SetParameterValue("CurrencySymbol", sCurrencySymbol);
                MeterComparisonReport.SetParameterValue("CurrencyCulture", sCurrentCurrenyCulture);
                MeterComparisonReport.SetParameterValue("@Region", RegionName);
                MeterComparisonReport.SetParameterValue("@Area", AreaName);
                MeterComparisonReport.SetParameterValue("@District", DistrictName);
                MeterComparisonReport.SetParameterValue("@CompanyName", CompanyName);
                MeterComparisonReport.SetParameterValue("@SubCompanyName", SubCompanyName);
                MeterComparisonReport.SetParameterValue("Version", sVersion);
                MeterComparisonReport.SetParameterValue("@GROUPBYZONE", GroupByZone);
                MeterComparisonReport.SetParameterValue("@SiteIDList", SiteIDList);
                MeterComparisonReport.SetParameterValue("@ZoneName", ZoneName);
                crDatabase = MeterComparisonReport.Database;
                crTables = crDatabase.Tables;

                foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
                {
                    tableLogonInfo = crTable.LogOnInfo;
                    tableLogonInfo.ConnectionInfo = connectionInfo;
                    crTable.ApplyLogOnInfo(tableLogonInfo);
                }

                crystalReportViewer1.ReportSource = MeterComparisonReport;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("ShowMGMDGamePerformanceReport", LogManager.enumLogLevel.Info);
                ExceptionManager.Publish(ex);
            }
        }


        public void ShowSlotMachinePerformanceReport(DataSet dsReportsDataSet, DateTime gamingDate, int company, int subCompany, int site, string SiteName, int zone, bool includeNonCashable, string Company, string SubCompany, string SiteIDList)
        {
            try
            {
                CrystalDecisions.CrystalReports.Engine.Database crDatabase;
                CrystalDecisions.CrystalReports.Engine.Tables crTables;

                TableLogOnInfo tableLogonInfo = new TableLogOnInfo();
                ConnectionInfo connectionInfo = new ConnectionInfo();

                connectionInfo.ServerName = DbConnection.ServerName;
                connectionInfo.UserID = DbConnection.UserName;
                connectionInfo.Password = DbConnection.Password;
                connectionInfo.DatabaseName = DbConnection.DatabaseName;

                SlotMachinePerformanceReport slotMachinePerformanceReport = new SlotMachinePerformanceReport();

                slotMachinePerformanceReport.SetParameterValue("@company", company);
                slotMachinePerformanceReport.SetParameterValue("@subcompany", subCompany);
                slotMachinePerformanceReport.SetParameterValue("@site", site);
                slotMachinePerformanceReport.SetParameterValue("SiteName", SiteName);
                //slotMachinePerformanceReport.SetParameterValue("@zone", zone);
                slotMachinePerformanceReport.SetParameterValue("@gamingdate", gamingDate);
                slotMachinePerformanceReport.SetParameterValue("@IncludeNonCashable", includeNonCashable);
				slotMachinePerformanceReport.SetParameterValue("CurrencySymbol", sCurrencySymbol);
                slotMachinePerformanceReport.SetParameterValue("DateCulture", sCurrentCurrenyCulture);
                slotMachinePerformanceReport.SetParameterValue("Version", sVersion);
                slotMachinePerformanceReport.SetParameterValue("CurrencyCulture", sCurrentCurrenyCulture);
                slotMachinePerformanceReport.SetParameterValue("Company", Company);
                slotMachinePerformanceReport.SetParameterValue("SubCompany", SubCompany);
                slotMachinePerformanceReport.SetParameterValue("@SiteIDList", SiteIDList);
                crDatabase = slotMachinePerformanceReport.Database;
                crTables = crDatabase.Tables;

                foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
                {
                    tableLogonInfo = crTable.LogOnInfo;
                    tableLogonInfo.ConnectionInfo = connectionInfo;
                    crTable.ApplyLogOnInfo(tableLogonInfo);
                }

                crystalReportViewer1.ReportSource = slotMachinePerformanceReport;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //
        public void ShowSlotCountComparisonDetails(DataSet dsReportsDataSet, DateTime startDate, DateTime endDate, int company,
            int subCompany, int siteID, string SiteName, int zone, string Company, string SubCompany, string Zone, bool GroupByzone, string SiteIDList)
        {
            try
            {
                CrystalDecisions.CrystalReports.Engine.Database crDatabase;
                CrystalDecisions.CrystalReports.Engine.Tables crTables;

                TableLogOnInfo tableLogonInfo = new TableLogOnInfo();
                ConnectionInfo connectionInfo = new ConnectionInfo();

                connectionInfo.ServerName = DbConnection.ServerName;
                connectionInfo.UserID = DbConnection.UserName;
                connectionInfo.Password = DbConnection.Password;
                connectionInfo.DatabaseName = DbConnection.DatabaseName;

                SoftCountComparisonReport SoftCountComparisonReport = new SoftCountComparisonReport();

                SoftCountComparisonReport.SetParameterValue("@company", company);
                SoftCountComparisonReport.SetParameterValue("@subcompany", subCompany);
                SoftCountComparisonReport.SetParameterValue("@site", siteID);
                SoftCountComparisonReport.SetParameterValue("SiteName", SiteName);
                SoftCountComparisonReport.SetParameterValue("@zone", zone);
                SoftCountComparisonReport.SetParameterValue("@startdate", startDate.Date);
                SoftCountComparisonReport.SetParameterValue("@enddate", endDate.Date);
                SoftCountComparisonReport.SetParameterValue("CurrencySymbol", sCurrencySymbol);
                SoftCountComparisonReport.SetParameterValue("RegionCulture", sCurrentCurrenyCulture);
                SoftCountComparisonReport.SetParameterValue("CurrencyCulture", sCurrentCurrenyCulture);
                SoftCountComparisonReport.SetParameterValue("Company", Company);
                SoftCountComparisonReport.SetParameterValue("SubCompany", SubCompany);
                SoftCountComparisonReport.SetParameterValue("Zone", Zone);
                SoftCountComparisonReport.SetParameterValue("Version", sVersion);
                SoftCountComparisonReport.SetParameterValue("@GROUPBYZONE", GroupByzone);
                SoftCountComparisonReport.SetParameterValue("@SiteIDList", SiteIDList);
                crDatabase = SoftCountComparisonReport.Database;
                crTables = crDatabase.Tables;

                foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
                {
                    tableLogonInfo = crTable.LogOnInfo;
                    tableLogonInfo.ConnectionInfo = connectionInfo;
                    crTable.ApplyLogOnInfo(tableLogonInfo);
                }

                crystalReportViewer1.ReportSource = SoftCountComparisonReport;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("ShowSlotCountComparisonDetails", LogManager.enumLogLevel.Info);
                ExceptionManager.Publish(ex);
            }
        }

        internal void ShowDailyElectronicFundRevenueReport(int companyID, string company, int subCompanyID,
                 string subCompany, int siteID, string siteName, int zone, string dtGamingDate, string period,string Zone,string SiteIDList)
        {
            try
            {
                CrystalDecisions.CrystalReports.Engine.Database crDatabase;
                CrystalDecisions.CrystalReports.Engine.Tables crTables;

                TableLogOnInfo tableLogonInfo = new TableLogOnInfo();
                ConnectionInfo connectionInfo = new ConnectionInfo();

                connectionInfo.ServerName = DbConnection.ServerName;
                connectionInfo.UserID = DbConnection.UserName;
                connectionInfo.Password = DbConnection.Password;
                connectionInfo.DatabaseName = DbConnection.DatabaseName;

                DailyElectronicFundRevenue objDailyElectronicFundRevenue = new DailyElectronicFundRevenue();

                //SP parameters
                objDailyElectronicFundRevenue.SetParameterValue("@Company", companyID);
                objDailyElectronicFundRevenue.SetParameterValue("@SubCompany", subCompanyID);
                objDailyElectronicFundRevenue.SetParameterValue("@Site", siteID);
                objDailyElectronicFundRevenue.SetParameterValue("@zone", zone);
                objDailyElectronicFundRevenue.SetParameterValue("@gamingdate", dtGamingDate);
                objDailyElectronicFundRevenue.SetParameterValue("@Period", period);
                objDailyElectronicFundRevenue.SetParameterValue("Version", sVersion);
                objDailyElectronicFundRevenue.SetParameterValue("Zone", Zone);
                //parameters
                objDailyElectronicFundRevenue.SetParameterValue("CompanyName", company);
                objDailyElectronicFundRevenue.SetParameterValue("SubCompanyName", subCompany);
                objDailyElectronicFundRevenue.SetParameterValue("SiteName", siteName);
                objDailyElectronicFundRevenue.SetParameterValue("Version", sVersion);
                objDailyElectronicFundRevenue.SetParameterValue("CurrencySymbol", sCurrencySymbol);
                objDailyElectronicFundRevenue.SetParameterValue("CurrencyCulture", sCurrentCurrenyCulture);
                objDailyElectronicFundRevenue.SetParameterValue("DateCulture", sCurrentCurrenyCulture);
                objDailyElectronicFundRevenue.SetParameterValue("@SiteIDList", SiteIDList);
                crDatabase = objDailyElectronicFundRevenue.Database;
                crTables = crDatabase.Tables;

                foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
                {
                    tableLogonInfo = crTable.LogOnInfo;
                    tableLogonInfo.ConnectionInfo = connectionInfo;
                    crTable.ApplyLogOnInfo(tableLogonInfo);
                }

                crystalReportViewer1.ReportSource = objDailyElectronicFundRevenue;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Added by A.Vinod Kumar on 16/12/2010
        public void ShowElecTransferVsRevenueComparisonReport(DataSet dsReportsDataSet, DateTime gamingDate, int company, int subCompany, int site, string SiteName, string zone, string CompanyName, string SubCompany, string Zone, int ZoneId, bool GroupByZone, string SiteIDList)
        {
            try
            {
                CrystalDecisions.CrystalReports.Engine.Database crDatabase;
                CrystalDecisions.CrystalReports.Engine.Tables crTables;

                TableLogOnInfo tableLogonInfo = new TableLogOnInfo();
                ConnectionInfo connectionInfo = new ConnectionInfo();

                connectionInfo.ServerName = DbConnection.ServerName;
                connectionInfo.UserID = DbConnection.UserName;
                connectionInfo.Password = DbConnection.Password;
                connectionInfo.DatabaseName = DbConnection.DatabaseName;

                ElecTransferVsRevenueComparisonReport elecTransferVsRevenueComparison = new ElecTransferVsRevenueComparisonReport();

                elecTransferVsRevenueComparison.SetParameterValue("@company", company);                
                elecTransferVsRevenueComparison.SetParameterValue("@subcompany", subCompany);                
                elecTransferVsRevenueComparison.SetParameterValue("@site", site);
                elecTransferVsRevenueComparison.SetParameterValue("SiteName", SiteName);
                //if (zone == "--ALL--")
                //    elecTransferVsRevenueComparison.SetParameterValue("@zone", String.Empty);
                //else
                //    elecTransferVsRevenueComparison.SetParameterValue("@zone", zone);
                
                elecTransferVsRevenueComparison.SetParameterValue("@gamingdate", gamingDate);

                elecTransferVsRevenueComparison.SetParameterValue("CurrencySymbol", sCurrencySymbol);
                elecTransferVsRevenueComparison.SetParameterValue("CurrencyCulture", sCurrentCurrenyCulture);
                elecTransferVsRevenueComparison.SetParameterValue("BMCVersion", sVersion);
                elecTransferVsRevenueComparison.SetParameterValue("CompanyName", CompanyName);
                elecTransferVsRevenueComparison.SetParameterValue("SubCompanyName", SubCompany);
                elecTransferVsRevenueComparison.SetParameterValue("ZoneName", Zone);
                elecTransferVsRevenueComparison.SetParameterValue("@zone", ZoneId);
                elecTransferVsRevenueComparison.SetParameterValue("@GROUPBYZONE", GroupByZone);
                elecTransferVsRevenueComparison.SetParameterValue("@SiteIDList", SiteIDList);
                crDatabase = elecTransferVsRevenueComparison.Database;
                crTables = crDatabase.Tables;

                foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
                {
                    tableLogonInfo = crTable.LogOnInfo;
                    tableLogonInfo.ConnectionInfo = connectionInfo;
                    crTable.ApplyLogOnInfo(tableLogonInfo);
                }

                crystalReportViewer1.ReportSource = elecTransferVsRevenueComparison;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal void ShowEFTQuestionableTransactions(int nCompany, string sCompany, int nSubCompany, string sSubCompany, int nSite, string sSite, string sStartDate, string sEndDate,string SiteIDList)
        {
            try
            {
                CrystalDecisions.CrystalReports.Engine.Database crDatabase;
                CrystalDecisions.CrystalReports.Engine.Tables crTables;

                TableLogOnInfo tableLogonInfo = new TableLogOnInfo();
                ConnectionInfo connectionInfo = new ConnectionInfo();

                connectionInfo.ServerName = DbConnection.ServerName;
                connectionInfo.UserID = DbConnection.UserName;
                connectionInfo.Password = DbConnection.Password;
                connectionInfo.DatabaseName = DbConnection.DatabaseName;

                EFTQuestionableTransactionsReport objEFTQuestionableTransactions = new EFTQuestionableTransactionsReport();

                //SP parameters
                objEFTQuestionableTransactions.SetParameterValue("@Company", nCompany);
                objEFTQuestionableTransactions.SetParameterValue("@SubCompany", nSubCompany);
                objEFTQuestionableTransactions.SetParameterValue("@Site", nSite);
                objEFTQuestionableTransactions.SetParameterValue("@startdate", sStartDate);
                objEFTQuestionableTransactions.SetParameterValue("@enddate", sEndDate);

                //parameters
                objEFTQuestionableTransactions.SetParameterValue("CompanyName", sCompany);
                objEFTQuestionableTransactions.SetParameterValue("SubCompanyName", sSubCompany);
                objEFTQuestionableTransactions.SetParameterValue("SiteName", sSite);
                objEFTQuestionableTransactions.SetParameterValue("Version", sVersion);
                objEFTQuestionableTransactions.SetParameterValue("CurrencySymbol", sCurrencySymbol);
                objEFTQuestionableTransactions.SetParameterValue("CurrencyCulture", sCurrentCurrenyCulture);
                objEFTQuestionableTransactions.SetParameterValue("DateCulture", sCurrentCurrenyCulture);
                objEFTQuestionableTransactions.SetParameterValue("@SiteIDList", SiteIDList);
                crDatabase = objEFTQuestionableTransactions.Database;
                crTables = crDatabase.Tables;

                foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
                {
                    tableLogonInfo = crTable.LogOnInfo;
                    tableLogonInfo.ConnectionInfo = connectionInfo;
                    crTable.ApplyLogOnInfo(tableLogonInfo);
                }

                crystalReportViewer1.ReportSource = objEFTQuestionableTransactions;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal void ShowEFTSlotActivity(int nCompany, string sCompany, int nSubCompany, string sSubCompany, int nSite, string sSite, string sStartDate, string sEndDate, string SiteIDList)
        {
            try
            {
                CrystalDecisions.CrystalReports.Engine.Database crDatabase;
                CrystalDecisions.CrystalReports.Engine.Tables crTables;

                TableLogOnInfo tableLogonInfo = new TableLogOnInfo();
                ConnectionInfo connectionInfo = new ConnectionInfo();

                connectionInfo.ServerName = DbConnection.ServerName;
                connectionInfo.UserID = DbConnection.UserName;
                connectionInfo.Password = DbConnection.Password;
                connectionInfo.DatabaseName = DbConnection.DatabaseName;

                EFTSlotActivityReport objEFTSlotActivityReport = new EFTSlotActivityReport();

                //SP parameters
                objEFTSlotActivityReport.SetParameterValue("@Company", nCompany);
                objEFTSlotActivityReport.SetParameterValue("@SubCompany", nSubCompany);
                objEFTSlotActivityReport.SetParameterValue("@Site", nSite);
                objEFTSlotActivityReport.SetParameterValue("@startdate", sStartDate);
                objEFTSlotActivityReport.SetParameterValue("@enddate", sEndDate);

                //parameters
                objEFTSlotActivityReport.SetParameterValue("CompanyName", sCompany);
                objEFTSlotActivityReport.SetParameterValue("SubCompanyName", sSubCompany);
                objEFTSlotActivityReport.SetParameterValue("SiteName", sSite);
                objEFTSlotActivityReport.SetParameterValue("Version", sVersion);
                objEFTSlotActivityReport.SetParameterValue("CurrencySymbol", sCurrencySymbol);
                objEFTSlotActivityReport.SetParameterValue("CurrencyCulture", sCurrentCurrenyCulture);
                objEFTSlotActivityReport.SetParameterValue("DateCulture", sCurrentCurrenyCulture);
                objEFTSlotActivityReport.SetParameterValue("@SiteIDList", SiteIDList);

                crDatabase = objEFTSlotActivityReport.Database;
                crTables = crDatabase.Tables;

                foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
                {
                    tableLogonInfo = crTable.LogOnInfo;
                    tableLogonInfo.ConnectionInfo = connectionInfo;
                    crTable.ApplyLogOnInfo(tableLogonInfo);
                }

                crystalReportViewer1.ReportSource = objEFTSlotActivityReport;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal void ShowEFTSlotActivityCumulative(int nCompany, string sCompany, int nSubCompany, string sSubCompany, int nSite, string sSite, string sStartDate, string sEndDate, string SiteIDList)
        {
            try
            {
                CrystalDecisions.CrystalReports.Engine.Database crDatabase;
                CrystalDecisions.CrystalReports.Engine.Tables crTables;

                TableLogOnInfo tableLogonInfo = new TableLogOnInfo();
                ConnectionInfo connectionInfo = new ConnectionInfo();

                connectionInfo.ServerName = DbConnection.ServerName;
                connectionInfo.UserID = DbConnection.UserName;
                connectionInfo.Password = DbConnection.Password;
                connectionInfo.DatabaseName = DbConnection.DatabaseName;

                EFTSlotActivityCumulativeReport objEFTSlotActivityCumulativeReport = new EFTSlotActivityCumulativeReport();

                //SP parameters
                objEFTSlotActivityCumulativeReport.SetParameterValue("@Company", nCompany);
                objEFTSlotActivityCumulativeReport.SetParameterValue("@SubCompany", nSubCompany);
                objEFTSlotActivityCumulativeReport.SetParameterValue("@Site", nSite);
                objEFTSlotActivityCumulativeReport.SetParameterValue("@startdate", sStartDate);
                objEFTSlotActivityCumulativeReport.SetParameterValue("@enddate", sEndDate);

                //parameters
                objEFTSlotActivityCumulativeReport.SetParameterValue("CompanyName", sCompany);
                objEFTSlotActivityCumulativeReport.SetParameterValue("SubCompanyName", sSubCompany);
                objEFTSlotActivityCumulativeReport.SetParameterValue("SiteName", sSite);
                objEFTSlotActivityCumulativeReport.SetParameterValue("Version", sVersion);
                objEFTSlotActivityCumulativeReport.SetParameterValue("CurrencySymbol", sCurrencySymbol);
                objEFTSlotActivityCumulativeReport.SetParameterValue("CurrencyCulture", sCurrentCurrenyCulture);
                objEFTSlotActivityCumulativeReport.SetParameterValue("DateCulture", sCurrentCurrenyCulture);
                objEFTSlotActivityCumulativeReport.SetParameterValue("@SiteIDList", SiteIDList);

                crDatabase = objEFTSlotActivityCumulativeReport.Database;
                crTables = crDatabase.Tables;

                foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
                {
                    tableLogonInfo = crTable.LogOnInfo;
                    tableLogonInfo.ConnectionInfo = connectionInfo;
                    crTable.ApplyLogOnInfo(tableLogonInfo);
                }

                crystalReportViewer1.ReportSource = objEFTSlotActivityCumulativeReport;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal void ShowInstallationWinLoss(int nCompany, string sCompany, int nSubCompany, int region,
            int area, int district, string sSubCompany, int nSite, int category,
            string sSite, string sStartDate, string sEndDate, string RegionName, string AreaName, string DistrictName, string Category, string SiteIDList)
        {
            try
            {
                CrystalDecisions.CrystalReports.Engine.Database crDatabase;
                CrystalDecisions.CrystalReports.Engine.Tables crTables;

                TableLogOnInfo tableLogonInfo = new TableLogOnInfo();
                ConnectionInfo connectionInfo = new ConnectionInfo();

                connectionInfo.ServerName = DbConnection.ServerName;
                connectionInfo.UserID = DbConnection.UserName;
                connectionInfo.Password = DbConnection.Password;
                connectionInfo.DatabaseName = DbConnection.DatabaseName;

                InstallationWinLossReport objInstallationWinLossReport = new InstallationWinLossReport();

                //SP parameters
                objInstallationWinLossReport.SetParameterValue("@Company", nCompany);
                objInstallationWinLossReport.SetParameterValue("CompanyName", sCompany);
                objInstallationWinLossReport.SetParameterValue("@SubCompany", nSubCompany);
                objInstallationWinLossReport.SetParameterValue("SubCompany", sSubCompany);
                objInstallationWinLossReport.SetParameterValue("@region", region);
                objInstallationWinLossReport.SetParameterValue("@area", area);
                objInstallationWinLossReport.SetParameterValue("@district", district);

                objInstallationWinLossReport.SetParameterValue("@Site", nSite);
                objInstallationWinLossReport.SetParameterValue("@category", category);
                objInstallationWinLossReport.SetParameterValue("@startdate", sStartDate);
                objInstallationWinLossReport.SetParameterValue("@enddate", sEndDate);
                objInstallationWinLossReport.SetParameterValue("SiteName", sSite);
                objInstallationWinLossReport.SetParameterValue("CurrencySymbol", sCurrencySymbol);
                objInstallationWinLossReport.SetParameterValue("CurrencyCulture", sCurrentCurrenyCulture);
                objInstallationWinLossReport.SetParameterValue("DateCulture", sCurrentCurrenyCulture);

                objInstallationWinLossReport.SetParameterValue("RegionName", RegionName);
                objInstallationWinLossReport.SetParameterValue("AreaName", AreaName);
                objInstallationWinLossReport.SetParameterValue("DistrictName", DistrictName);
                objInstallationWinLossReport.SetParameterValue("Category", Category);
                objInstallationWinLossReport.SetParameterValue("BMCVersion", sVersion);
                objInstallationWinLossReport.SetParameterValue("@SiteIDList", SiteIDList);

                //parameters

                //objInstallationWinLossReport.SetParameterValue("Version", sVersion);
                //objInstallationWinLossReport.SetParameterValue("CurrencySymbol", sCurrencySymbol);
                //objInstallationWinLossReport.SetParameterValue("CurrencyCulture", sCurrentCurrenyCulture);
                //objInstallationWinLossReport.SetParameterValue("DateCulture", sCurrentCurrenyCulture);

                crDatabase = objInstallationWinLossReport.Database;
                crTables = crDatabase.Tables;

                foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
                {
                    tableLogonInfo = crTable.LogOnInfo;
                    tableLogonInfo.ConnectionInfo = connectionInfo;
                    crTable.ApplyLogOnInfo(tableLogonInfo);
                }

                crystalReportViewer1.ReportSource = objInstallationWinLossReport;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal void ShowDailyAccoutingPerInstallation(int nCompany, string sCompany, int nSubCompany, int region,
          int area, int district, string sSubCompany, int nSite, int category,
          string sSite, string sStartDate, string sEndDate,string RegionName,string AreaName,string District,string SiteIDList)
        {
            try
            {
                CrystalDecisions.CrystalReports.Engine.Database crDatabase;
                CrystalDecisions.CrystalReports.Engine.Tables crTables;

                TableLogOnInfo tableLogonInfo = new TableLogOnInfo();
                ConnectionInfo connectionInfo = new ConnectionInfo();

                connectionInfo.ServerName = DbConnection.ServerName;
                connectionInfo.UserID = DbConnection.UserName;
                connectionInfo.Password = DbConnection.Password;
                connectionInfo.DatabaseName = DbConnection.DatabaseName;

                DailyAccountingPerInstallation objDailyAccountingPerInstallationReport = new DailyAccountingPerInstallation();

                //SP parameters

                objDailyAccountingPerInstallationReport.SetParameterValue("@SubCompany", nSubCompany);
                objDailyAccountingPerInstallationReport.SetParameterValue("@region", region);
                objDailyAccountingPerInstallationReport.SetParameterValue("@area", area);
                objDailyAccountingPerInstallationReport.SetParameterValue("@district", district);
                objDailyAccountingPerInstallationReport.SetParameterValue("Company", sCompany);
                objDailyAccountingPerInstallationReport.SetParameterValue("SubCompany", sSubCompany);
                objDailyAccountingPerInstallationReport.SetParameterValue("AreaName", AreaName);
                objDailyAccountingPerInstallationReport.SetParameterValue("RegionName", RegionName);
                objDailyAccountingPerInstallationReport.SetParameterValue("@Site", nSite);

                objDailyAccountingPerInstallationReport.SetParameterValue("@startdate", sStartDate);
                objDailyAccountingPerInstallationReport.SetParameterValue("@enddate", sEndDate);
                objDailyAccountingPerInstallationReport.SetParameterValue("SiteName", sSite);
                objDailyAccountingPerInstallationReport.SetParameterValue("District", District);
                objDailyAccountingPerInstallationReport.SetParameterValue("Version", sVersion);


                //parameters

                objDailyAccountingPerInstallationReport.SetParameterValue("CurrencySymbol", sCurrencySymbol);
                objDailyAccountingPerInstallationReport.SetParameterValue("CurrencyCulture", sCurrentCurrenyCulture);
                objDailyAccountingPerInstallationReport.SetParameterValue("DateCulture", sCurrentCurrenyCulture);
                objDailyAccountingPerInstallationReport.SetParameterValue("@company", nCompany);
                objDailyAccountingPerInstallationReport.SetParameterValue("@SiteIDList", SiteIDList);

                crDatabase = objDailyAccountingPerInstallationReport.Database;
                crTables = crDatabase.Tables;

                foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
                {
                    tableLogonInfo = crTable.LogOnInfo;
                    tableLogonInfo.ConnectionInfo = connectionInfo;
                    crTable.ApplyLogOnInfo(tableLogonInfo);
                }

                crystalReportViewer1.ReportSource = objDailyAccountingPerInstallationReport;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        internal void ShowAssetDetailsReport(int Company, string CompanyName, int MachineStatusFlag)
        {
            try
            {
                CrystalDecisions.CrystalReports.Engine.Database crDatabase;
                CrystalDecisions.CrystalReports.Engine.Tables crTables;

                TableLogOnInfo tableLogonInfo = new TableLogOnInfo();
                ConnectionInfo connectionInfo = new ConnectionInfo();

                connectionInfo.ServerName = DbConnection.ServerName;
                connectionInfo.UserID = DbConnection.UserName;
                connectionInfo.Password = DbConnection.Password;
                connectionInfo.DatabaseName = DbConnection.DatabaseName;

                AssetDetailsReport objAssetDetails = new AssetDetailsReport();

                objAssetDetails.SetParameterValue("@Company", Company);
                objAssetDetails.SetParameterValue("@CompanyName", CompanyName);
                objAssetDetails.SetParameterValue("@StockStatus", MachineStatusFlag);               
                objAssetDetails.SetParameterValue("Version", sVersion); 
                crDatabase = objAssetDetails.Database;
                crTables = crDatabase.Tables;

                foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
                {
                    tableLogonInfo = crTable.LogOnInfo;
                    tableLogonInfo.ConnectionInfo = connectionInfo;
                    crTable.ApplyLogOnInfo(tableLogonInfo);
                }

                crystalReportViewer1.ReportSource = objAssetDetails;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        internal void ShowMGMDByGamingDeviceCabinetReport(int companyID, string company, int subCompanyID,
               string subCompany, int siteID, string siteName, int ZoneID, string zone, string dtGamingDate, string period, bool GroupByZone)
        {
            try
            {
                CrystalDecisions.CrystalReports.Engine.Database crDatabase;
                CrystalDecisions.CrystalReports.Engine.Tables crTables;

                TableLogOnInfo tableLogonInfo = new TableLogOnInfo();
                ConnectionInfo connectionInfo = new ConnectionInfo();

                connectionInfo.ServerName = DbConnection.ServerName;
                connectionInfo.UserID = DbConnection.UserName;
                connectionInfo.Password = DbConnection.Password;
                connectionInfo.DatabaseName = DbConnection.DatabaseName;

                MGMDByGamingDeviceCabinetReport oMGMDByGamingDeviceCabinetReport = new MGMDByGamingDeviceCabinetReport();

                //SP parameters
                oMGMDByGamingDeviceCabinetReport.SetParameterValue("@Company", companyID);
                oMGMDByGamingDeviceCabinetReport.SetParameterValue("@SubCompany", subCompanyID);
                oMGMDByGamingDeviceCabinetReport.SetParameterValue("@Site", siteID);
                oMGMDByGamingDeviceCabinetReport.SetParameterValue("@zone", ZoneID);
                oMGMDByGamingDeviceCabinetReport.SetParameterValue("@zone_Name", zone);
                oMGMDByGamingDeviceCabinetReport.SetParameterValue("@gamingdate", dtGamingDate);
                oMGMDByGamingDeviceCabinetReport.SetParameterValue("@Period", period);
                //parameters
                oMGMDByGamingDeviceCabinetReport.SetParameterValue("CompanyName", company);
                oMGMDByGamingDeviceCabinetReport.SetParameterValue("SubCompanyName", subCompany);
                oMGMDByGamingDeviceCabinetReport.SetParameterValue("SiteName", siteName);
                oMGMDByGamingDeviceCabinetReport.SetParameterValue("Version", sVersion);
                oMGMDByGamingDeviceCabinetReport.SetParameterValue("CurrencySymbol", sCurrencySymbol);
                oMGMDByGamingDeviceCabinetReport.SetParameterValue("CurrencyCulture", sCurrentCurrenyCulture);
                oMGMDByGamingDeviceCabinetReport.SetParameterValue("DateCulture", sCurrentCurrenyCulture);
                oMGMDByGamingDeviceCabinetReport.SetParameterValue("@GROUPBYZONE", GroupByZone);
                

                crDatabase = oMGMDByGamingDeviceCabinetReport.Database;
                crTables = crDatabase.Tables;

                foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
                {
                    tableLogonInfo = crTable.LogOnInfo;
                    tableLogonInfo.ConnectionInfo = connectionInfo;
                    crTable.ApplyLogOnInfo(tableLogonInfo);
                }

                crystalReportViewer1.ReportSource = oMGMDByGamingDeviceCabinetReport;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        internal void showMGMDSummaryAnalysis(int nCompany, int nSubCompany, int nSite, int nZone,
            string dtGamingDate, string sSite, string Period, string sVersion, string Company, string SubCompany, string Zone, bool GroupByZone, string SiteIDList)
        {
            try
            {
                CrystalDecisions.CrystalReports.Engine.Database crDatabase;
                CrystalDecisions.CrystalReports.Engine.Tables crTables;

                TableLogOnInfo tableLogonInfo = new TableLogOnInfo();
                ConnectionInfo connectionInfo = new ConnectionInfo();

                connectionInfo.ServerName = DbConnection.ServerName;
                connectionInfo.UserID = DbConnection.UserName;
                connectionInfo.Password = DbConnection.Password;
                connectionInfo.DatabaseName = DbConnection.DatabaseName;

                MGMDSummaryAnalysis objMGMDSummaryAnalysis = new MGMDSummaryAnalysis();

                //SP parameters

                objMGMDSummaryAnalysis.SetParameterValue("@SubCompany", nSubCompany);
                objMGMDSummaryAnalysis.SetParameterValue("@Site", nSite);
                objMGMDSummaryAnalysis.SetParameterValue("@Zone", nZone);
                objMGMDSummaryAnalysis.SetParameterValue("@gamingdate", dtGamingDate);
                objMGMDSummaryAnalysis.SetParameterValue("@Period", Period);
                objMGMDSummaryAnalysis.SetParameterValue("SiteName", sSite);

                //parameters

                objMGMDSummaryAnalysis.SetParameterValue("CurrencySymbol", sCurrencySymbol);
                objMGMDSummaryAnalysis.SetParameterValue("CurrencyCulture", sCurrentCurrenyCulture);
                objMGMDSummaryAnalysis.SetParameterValue("DateCulture", sCurrentCurrenyCulture);
                objMGMDSummaryAnalysis.SetParameterValue("@company", nCompany);
                objMGMDSummaryAnalysis.SetParameterValue("Version", sVersion);
                objMGMDSummaryAnalysis.SetParameterValue("CompanyName", Company);
                objMGMDSummaryAnalysis.SetParameterValue("SubCompanyName", SubCompany);
                objMGMDSummaryAnalysis.SetParameterValue("ZoneName", Zone);
                objMGMDSummaryAnalysis.SetParameterValue("@GROUPBYZONE", GroupByZone);
                objMGMDSummaryAnalysis.SetParameterValue("@SiteIDList", SiteIDList);
                

                crDatabase = objMGMDSummaryAnalysis.Database;
                crTables = crDatabase.Tables;

                foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
                {
                    tableLogonInfo = crTable.LogOnInfo;
                    tableLogonInfo.ConnectionInfo = connectionInfo;
                    crTable.ApplyLogOnInfo(tableLogonInfo);
                }

                crystalReportViewer1.ReportSource = objMGMDSummaryAnalysis;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        internal void ShowManufacturerPerformanceReport(int companyID, string company, int subCompanyID,
               string subCompany, int siteID, string siteName, string dtGamingDate, string period,string SiteIDList)
        {
            try
            {
                CrystalDecisions.CrystalReports.Engine.Database crDatabase;
                CrystalDecisions.CrystalReports.Engine.Tables crTables;

                TableLogOnInfo tableLogonInfo = new TableLogOnInfo();
                ConnectionInfo connectionInfo = new ConnectionInfo();

                connectionInfo.ServerName = DbConnection.ServerName;
                connectionInfo.UserID = DbConnection.UserName;
                connectionInfo.Password = DbConnection.Password;
                connectionInfo.DatabaseName = DbConnection.DatabaseName;

                ManufacturerPerformanceReport oManufacturerPerformanceReport = new ManufacturerPerformanceReport();

                //SP parameters
                oManufacturerPerformanceReport.SetParameterValue("@Company", companyID);
                oManufacturerPerformanceReport.SetParameterValue("@SubCompany", subCompanyID);
                oManufacturerPerformanceReport.SetParameterValue("@Site", siteID);
                oManufacturerPerformanceReport.SetParameterValue("@gamingdate", dtGamingDate);
                oManufacturerPerformanceReport.SetParameterValue("@Period", period);
                //parameters
                oManufacturerPerformanceReport.SetParameterValue("CompanyName", company);
                oManufacturerPerformanceReport.SetParameterValue("SubCompanyName", subCompany);
                oManufacturerPerformanceReport.SetParameterValue("SiteName", siteName);
                oManufacturerPerformanceReport.SetParameterValue("Version", sVersion);
                oManufacturerPerformanceReport.SetParameterValue("CurrencySymbol", sCurrencySymbol);
                oManufacturerPerformanceReport.SetParameterValue("CurrencyCulture", sCurrentCurrenyCulture);
                oManufacturerPerformanceReport.SetParameterValue("DateCulture", sCurrentCurrenyCulture);
                oManufacturerPerformanceReport.SetParameterValue("@SiteIDList", SiteIDList);

                crDatabase = oManufacturerPerformanceReport.Database;
                crTables = crDatabase.Tables;

                foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
                {
                    tableLogonInfo = crTable.LogOnInfo;
                    tableLogonInfo.ConnectionInfo = connectionInfo;
                    crTable.ApplyLogOnInfo(tableLogonInfo);
                }

                crystalReportViewer1.ReportSource = oManufacturerPerformanceReport;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        internal void ShowLiabilityTransferDetails(string ReportName, DataSet dtSet, DateTime fromdate, DateTime toDate, int company, int subcompany, string CompanyName, string SubCompanyName, int SiteID, string SiteName, int ZoneID,string sSiteIDList)
        {
            try
            {
                CrystalDecisions.CrystalReports.Engine.Database crDatabase;
                CrystalDecisions.CrystalReports.Engine.Tables crTables;

                TableLogOnInfo tableLogonInfo = new TableLogOnInfo();
                ConnectionInfo connectionInfo = new ConnectionInfo();

                connectionInfo.ServerName = DbConnection.ServerName;
                connectionInfo.UserID = DbConnection.UserName;
                connectionInfo.Password = DbConnection.Password;
                connectionInfo.DatabaseName = DbConnection.DatabaseName;

                var vLiabilityTransferDetails = new CrossPropertyLiabilityTransferDetailsReport();
                dtSet.Tables[0].TableName = "LiabilityTransferDetails";

                vLiabilityTransferDetails.SetDataSource(dtSet);
                vLiabilityTransferDetails.SetParameterValue("@STARTDATE", fromdate);
                vLiabilityTransferDetails.SetParameterValue("@ENDDATE", toDate);
                vLiabilityTransferDetails.SetParameterValue("@Company", company);
                vLiabilityTransferDetails.SetParameterValue("@SubCompany", subcompany);
                vLiabilityTransferDetails.SetParameterValue("@CompanyName", CompanyName);
                vLiabilityTransferDetails.SetParameterValue("@SubCompanyName", SubCompanyName);
                vLiabilityTransferDetails.SetParameterValue("@SITEID", SiteID);
                vLiabilityTransferDetails.SetParameterValue("siteName", SiteName);
                //vLiabilityTransferDetails.SetParameterValue("@ZONE", ZoneID);
                vLiabilityTransferDetails.SetParameterValue("BMCVersion", sVersion);
                vLiabilityTransferDetails.SetParameterValue("CurrencyCulture", sCurrentCurrenyCulture);
                vLiabilityTransferDetails.SetParameterValue("CurrencySymbol", sCurrencySymbol);
                vLiabilityTransferDetails.SetParameterValue("DateCulture", sCurrentCurrenyCulture);
                vLiabilityTransferDetails.SetParameterValue("@SiteIDList", sSiteIDList);

                crDatabase = vLiabilityTransferDetails.Database;
                crTables = crDatabase.Tables;

                foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
                {
                    tableLogonInfo = crTable.LogOnInfo;
                    tableLogonInfo.ConnectionInfo = connectionInfo;
                    crTable.ApplyLogOnInfo(tableLogonInfo);
                }



                crystalReportViewer1.ReportSource = vLiabilityTransferDetails;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        internal void ShowCrossPropertyTicketAnalysisReport(string ReportName, DataSet dtSet, DateTime fromdate, DateTime toDate, int company, int subcompany, string CompanyName, string SubCompanyName, int SiteID, string SiteName, int ZoneID,string sSiteIDList)
        {
            try
            {
                CrystalDecisions.CrystalReports.Engine.Database crDatabase;
                CrystalDecisions.CrystalReports.Engine.Tables crTables;

                TableLogOnInfo tableLogonInfo = new TableLogOnInfo();
                ConnectionInfo connectionInfo = new ConnectionInfo();

                connectionInfo.ServerName = DbConnection.ServerName;
                connectionInfo.UserID = DbConnection.UserName;
                connectionInfo.Password = DbConnection.Password;
                connectionInfo.DatabaseName = DbConnection.DatabaseName;

                var vCrossPropertyTicketAnalysis = new CrossPropertyTicketAnalysis();
                dtSet.Tables[0].TableName = "CrossPropertyTicketAnalysis";

                vCrossPropertyTicketAnalysis.SetDataSource(dtSet);
                vCrossPropertyTicketAnalysis.SetParameterValue("@STARTDATE", fromdate);
                vCrossPropertyTicketAnalysis.SetParameterValue("@ENDDATE", toDate);
                vCrossPropertyTicketAnalysis.SetParameterValue("fromDate", fromdate);
                vCrossPropertyTicketAnalysis.SetParameterValue("toDate", toDate);
                vCrossPropertyTicketAnalysis.SetParameterValue("@Company", company);
                vCrossPropertyTicketAnalysis.SetParameterValue("@SubCompany", subcompany);
                vCrossPropertyTicketAnalysis.SetParameterValue("@CompanyName", CompanyName);
                vCrossPropertyTicketAnalysis.SetParameterValue("@SubCompanyName", SubCompanyName);
                vCrossPropertyTicketAnalysis.SetParameterValue("@SITE", SiteID);
                vCrossPropertyTicketAnalysis.SetParameterValue("siteName", SiteName);
                //vCrossPropertyTicketAnalysis.SetParameterValue("@ZONE", ZoneID);
                vCrossPropertyTicketAnalysis.SetParameterValue("BMCVersion", sVersion);
                vCrossPropertyTicketAnalysis.SetParameterValue("CurrencyCulture", sCurrentCurrenyCulture);
                vCrossPropertyTicketAnalysis.SetParameterValue("CurrencySymbol", sCurrencySymbol);
                vCrossPropertyTicketAnalysis.SetParameterValue("DateCulture", sCurrentCurrenyCulture);
                vCrossPropertyTicketAnalysis.SetParameterValue("@SiteIDList", sSiteIDList);

                crDatabase = vCrossPropertyTicketAnalysis.Database;
                crTables = crDatabase.Tables;

                foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
                {
                    tableLogonInfo = crTable.LogOnInfo;
                    tableLogonInfo.ConnectionInfo = connectionInfo;
                    crTable.ApplyLogOnInfo(tableLogonInfo);
                }



                crystalReportViewer1.ReportSource = vCrossPropertyTicketAnalysis;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        public void ShowWinComparisonReport(String reportName, DataSet dsReportsDataSet, int Company, string StrCompany, int SubCompany, string StrSubCompany, int Zone, string ZoneName, int Site, string StrSite, DateTime GamingDate, bool IncludeNonCashable, bool UsePhysicalWin, string Slot, string Period, string SiteIDList)
        {
            try
            {
                CrystalDecisions.CrystalReports.Engine.Database crDatabase;
                CrystalDecisions.CrystalReports.Engine.Tables crTables;

                TableLogOnInfo tableLogonInfo = new TableLogOnInfo();
                ConnectionInfo connectionInfo = new ConnectionInfo();

                connectionInfo.ServerName = DbConnection.ServerName;
                connectionInfo.UserID = DbConnection.UserName;
                connectionInfo.Password = DbConnection.Password;
                connectionInfo.DatabaseName = DbConnection.DatabaseName;

                var vWinComparisonReport = new WinComparisonReport();
                dsReportsDataSet.Tables[0].TableName = "WinComparisonReport";

                vWinComparisonReport.SetDataSource(dsReportsDataSet);
                vWinComparisonReport.SetParameterValue("@Company", Company);
                vWinComparisonReport.SetParameterValue("@SubCompany", SubCompany);
                vWinComparisonReport.SetParameterValue("CompanyName", StrCompany);
                vWinComparisonReport.SetParameterValue("SubCompanyName", StrSubCompany);
                vWinComparisonReport.SetParameterValue("@Zone", Zone);
                vWinComparisonReport.SetParameterValue("ZoneName", ZoneName);
                vWinComparisonReport.SetParameterValue("@Site", Site);
                vWinComparisonReport.SetParameterValue("SiteName", StrSite);
                vWinComparisonReport.SetParameterValue("@GamingDate", GamingDate);
                vWinComparisonReport.SetParameterValue("@Slot", Slot);
                vWinComparisonReport.SetParameterValue("@IncludeNonCashable", IncludeNonCashable);
                vWinComparisonReport.SetParameterValue("@UsePhysicalWin", UsePhysicalWin);
                vWinComparisonReport.SetParameterValue("@Period", Period);
                vWinComparisonReport.SetParameterValue("BMCVersion", sVersion);
                vWinComparisonReport.SetParameterValue("CurrencyCulture", sCurrentCurrenyCulture);
                vWinComparisonReport.SetParameterValue("CurrencySymbol", sCurrencySymbol);
                vWinComparisonReport.SetParameterValue("DateCulture", sCurrentCurrenyCulture);
                vWinComparisonReport.SetParameterValue("@SiteIDList", SiteIDList);
                crDatabase = vWinComparisonReport.Database;
                crTables = crDatabase.Tables;

                foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
                {
                    tableLogonInfo = crTable.LogOnInfo;
                    tableLogonInfo.ConnectionInfo = connectionInfo;
                    crTable.ApplyLogOnInfo(tableLogonInfo);
                }
                crystalReportViewer1.ReportSource = vWinComparisonReport;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

      

        //SP8 Base Ends

        public void ShowStackerDetailsReport(String reportName, 
                                                DataSet dsReportsDataSet, 
                                                int Company, 
                                                string StrCompany, 
                                                int SubCompany, 
                                                string StrSubCompany,
                                                int Area, 
                                                int Region,
                                                int Site,
                                                string SiteName,
                                                int District,
                                                int StackerLevel, string strDistrict, string strArea, string strRegionName, string SiteIDList)
        {
            try
            {
                CrystalDecisions.CrystalReports.Engine.Database crDatabase;
                CrystalDecisions.CrystalReports.Engine.Tables crTables;

                TableLogOnInfo tableLogonInfo = new TableLogOnInfo();
                ConnectionInfo connectionInfo = new ConnectionInfo();

                connectionInfo.ServerName = DbConnection.ServerName;
                connectionInfo.UserID = DbConnection.UserName;
                connectionInfo.Password = DbConnection.Password;
                connectionInfo.DatabaseName = DbConnection.DatabaseName;

                var vStackerDetailsReport = new StackerLevelDetailsReport();
                dsReportsDataSet.Tables[0].TableName = "StackerDetailsReport";

                vStackerDetailsReport.SetDataSource(dsReportsDataSet);
                vStackerDetailsReport.SetParameterValue("@Company", Company);
                vStackerDetailsReport.SetParameterValue("@SubCompany", SubCompany);
                vStackerDetailsReport.SetParameterValue("@CompanyName", StrCompany);
                vStackerDetailsReport.SetParameterValue("@SubCompanyName", StrSubCompany);
                vStackerDetailsReport.SetParameterValue("@Site", Site);
                vStackerDetailsReport.SetParameterValue("SiteName", SiteName);
                vStackerDetailsReport.SetParameterValue("@Region", Region);
                vStackerDetailsReport.SetParameterValue("@District", District);
                vStackerDetailsReport.SetParameterValue("@Area", Area);
                vStackerDetailsReport.SetParameterValue("@StackerLevel", StackerLevel);
                vStackerDetailsReport.SetParameterValue("DistrictName", strDistrict);
                vStackerDetailsReport.SetParameterValue("AreaName", strArea);
                vStackerDetailsReport.SetParameterValue("@RegionName", strRegionName);
                vStackerDetailsReport.SetParameterValue("Version", sVersion);
                vStackerDetailsReport.SetParameterValue("CurrencyCulture", sCurrentCurrenyCulture);
                vStackerDetailsReport.SetParameterValue("CurrencySymbol", sCurrencySymbol);
                vStackerDetailsReport.SetParameterValue("DateCulture", sCurrentCurrenyCulture);
                vStackerDetailsReport.SetParameterValue("@SiteIDList", SiteIDList);
                crDatabase = vStackerDetailsReport.Database;
                crTables = crDatabase.Tables;

                foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
                {
                    tableLogonInfo = crTable.LogOnInfo;
                    tableLogonInfo.ConnectionInfo = connectionInfo;
                    crTable.ApplyLogOnInfo(tableLogonInfo);
                }
                crystalReportViewer1.ReportSource = vStackerDetailsReport;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }


        public void ShowDropScheduleReport(String reportName, 
                                                DataSet dsReportsDataSet, 
                                                int Company, 
                                                string StrCompany, 
                                                int SubCompany, 
                                                string StrSubCompany,
                                                int Area, 
                                                string AreaName,
                                                int Region,
                                                string RegionName,
                                                int Site,
                                                string SiteName,
                                                int District,
                                                string DistrictName,
                                                int StackerLevel,
                                                string SiteIDList)
        {
            try
            {
                CrystalDecisions.CrystalReports.Engine.Database crDatabase;
                CrystalDecisions.CrystalReports.Engine.Tables crTables;

                TableLogOnInfo tableLogonInfo = new TableLogOnInfo();
                ConnectionInfo connectionInfo = new ConnectionInfo();

                connectionInfo.ServerName = DbConnection.ServerName;
                connectionInfo.UserID = DbConnection.UserName;
                connectionInfo.Password = DbConnection.Password;
                connectionInfo.DatabaseName = DbConnection.DatabaseName;

                var vDropScheduleReport = new DropSchedule();
                dsReportsDataSet.Tables[0].TableName = "DropSchedule";

                vDropScheduleReport.SetDataSource(dsReportsDataSet);
                vDropScheduleReport.SetParameterValue("@Company", Company);
                vDropScheduleReport.SetParameterValue("@SubCompany", SubCompany);
                vDropScheduleReport.SetParameterValue("@CompanyName", StrCompany);
                vDropScheduleReport.SetParameterValue("@SubCompanyName", StrSubCompany);
                vDropScheduleReport.SetParameterValue("@Site", Site);
                vDropScheduleReport.SetParameterValue("SiteName", SiteName);
                vDropScheduleReport.SetParameterValue("@Region", Region);
                vDropScheduleReport.SetParameterValue("@District", District);
                vDropScheduleReport.SetParameterValue("@Area", Area);
                vDropScheduleReport.SetParameterValue("@RegionName", RegionName);
                vDropScheduleReport.SetParameterValue("@DistrictName", DistrictName);
                vDropScheduleReport.SetParameterValue("@AreaName", AreaName);
                vDropScheduleReport.SetParameterValue("@StackerLevel", StackerLevel);
                vDropScheduleReport.SetParameterValue("Version", sVersion);
                vDropScheduleReport.SetParameterValue("Currency Culture", sCurrentCurrenyCulture);
                vDropScheduleReport.SetParameterValue("Currency Symbol", sCurrencySymbol);
                vDropScheduleReport.SetParameterValue("Date Culture", sCurrentCurrenyCulture);
                vDropScheduleReport.SetParameterValue("@SiteIDList", SiteIDList);
                crDatabase = vDropScheduleReport.Database;
                crTables = crDatabase.Tables;

                foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
                {
                    tableLogonInfo = crTable.LogOnInfo;
                    tableLogonInfo.ConnectionInfo = connectionInfo;
                    crTable.ApplyLogOnInfo(tableLogonInfo);
                }
                crystalReportViewer1.ReportSource = vDropScheduleReport;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        public void ShowEmployeeCardSessionsReport(string ReportName, DataSet dsReportsDataSet, int Company,
                                                string StrCompany,
                                                int SubCompany,
                                                string StrSubCompany,
                                                int Region,
                                                string RegionName,
                                                int Area,
                                                string AreaName,
                                                int District,
                                                string DistrictName,
                                                int Site,
                                                string SiteName,
                                                int EmpID,
                                                string EmpCardID,
                                                DateTime StartDate,
                                                DateTime EndDate,
                                                string SiteIDList)
        {
            try
            {
                CrystalDecisions.CrystalReports.Engine.Database crDatabase;
                CrystalDecisions.CrystalReports.Engine.Tables crTables;

                TableLogOnInfo tableLogonInfo = new TableLogOnInfo();
                ConnectionInfo connectionInfo = new ConnectionInfo();

                connectionInfo.ServerName = DbConnection.ServerName;
                connectionInfo.UserID = DbConnection.UserName;
                connectionInfo.Password = DbConnection.Password;
                connectionInfo.DatabaseName = DbConnection.DatabaseName;
                var vEmployeeCardSessionsReport = new EmployeeCardSessionsReport();
                dsReportsDataSet.Tables[0].TableName = "EmployeeCardSessionsReport";

                vEmployeeCardSessionsReport.SetDataSource(dsReportsDataSet);
                vEmployeeCardSessionsReport.SetParameterValue("@Company", Company);
                vEmployeeCardSessionsReport.SetParameterValue("@SubCompany", SubCompany);
                vEmployeeCardSessionsReport.SetParameterValue("@CompanyName", StrCompany);
                vEmployeeCardSessionsReport.SetParameterValue("@SubCompanyName", StrSubCompany);                
                vEmployeeCardSessionsReport.SetParameterValue("@Region", Region);
                vEmployeeCardSessionsReport.SetParameterValue("@RegionName", RegionName);
                vEmployeeCardSessionsReport.SetParameterValue("@Area", Area);                
                vEmployeeCardSessionsReport.SetParameterValue("@AreaName", AreaName);   
                vEmployeeCardSessionsReport.SetParameterValue("@District", District);
                vEmployeeCardSessionsReport.SetParameterValue("@DistrictName", DistrictName);                
                vEmployeeCardSessionsReport.SetParameterValue("@Site", Site);
                vEmployeeCardSessionsReport.SetParameterValue("SiteName", SiteName);
                vEmployeeCardSessionsReport.SetParameterValue("BMCVersion", sVersion);
                vEmployeeCardSessionsReport.SetParameterValue("@UserID", EmpID);
                vEmployeeCardSessionsReport.SetParameterValue("@EmpCardID", EmpCardID);
                vEmployeeCardSessionsReport.SetParameterValue("@Startdate", StartDate);
                vEmployeeCardSessionsReport.SetParameterValue("@Enddate", EndDate);
                vEmployeeCardSessionsReport.SetParameterValue("CurrencyCulture", sCurrentCurrenyCulture);
                vEmployeeCardSessionsReport.SetParameterValue("@SiteIDList", SiteIDList);
               // vEmployeeCardSessionsReport.SetParameterValue("Currency Symbol", sCurrencySymbol);
                //vEmployeeCardSessionsReport.SetParameterValue("Date Culture", sCurrentCurrenyCulture);

                crDatabase = vEmployeeCardSessionsReport.Database;
                crTables = crDatabase.Tables;

                foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
                {
                    tableLogonInfo = crTable.LogOnInfo;
                    tableLogonInfo.ConnectionInfo = connectionInfo;
                    crTable.ApplyLogOnInfo(tableLogonInfo);
    }
                crystalReportViewer1.ReportSource = vEmployeeCardSessionsReport;
}
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        public void ShowEmployeeListReport(string ReportName, DataSet dsReportsDataSet, int CardNumber, string EmpName, string CardStatus, string CardType,string sCompany,string strCardNumber)
        {
            try
            {
                CrystalDecisions.CrystalReports.Engine.Database crDatabase;
                CrystalDecisions.CrystalReports.Engine.Tables crTables;

                TableLogOnInfo tableLogonInfo = new TableLogOnInfo();
                ConnectionInfo connectionInfo = new ConnectionInfo();

                connectionInfo.ServerName = DbConnection.ServerName;
                connectionInfo.UserID = DbConnection.UserName;
                connectionInfo.Password = DbConnection.Password;
                connectionInfo.DatabaseName = DbConnection.DatabaseName;
                var vEmployeeCardListReport = new EmployeeCardListReport();
                dsReportsDataSet.Tables[0].TableName = "EmployeeCardListReport";

                vEmployeeCardListReport.SetDataSource(dsReportsDataSet);
                vEmployeeCardListReport.SetParameterValue("BMCVersion", sVersion);
                vEmployeeCardListReport.SetParameterValue("@CardNumber", CardNumber);
                vEmployeeCardListReport.SetParameterValue("@EmployeeName", EmpName);
                vEmployeeCardListReport.SetParameterValue("@CardStatus", CardStatus);
                vEmployeeCardListReport.SetParameterValue("@CardType", CardType);
                vEmployeeCardListReport.SetParameterValue("CompanyName", sCompany);
                vEmployeeCardListReport.SetParameterValue("strCardNumber", strCardNumber);

                crDatabase = vEmployeeCardListReport.Database;
                crTables = crDatabase.Tables;

                foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
                {
                    tableLogonInfo = crTable.LogOnInfo;
                    tableLogonInfo.ConnectionInfo = connectionInfo;
                    crTable.ApplyLogOnInfo(tableLogonInfo);
                }
                crystalReportViewer1.ReportSource = vEmployeeCardListReport;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }                                    
                                                
        public void ShowLicenseHistory(string reportName, DataSet dsReportsDataSet, int CompanyId, int SubCompanyId, int Area, int District, int SiteId, int Region, DateTime StartDate, DateTime EndDate, string SiteName, string RegionName, string strDistrict, string strArea, string strCompanyName, string strSubCompanyName,string sRuleName, string SiteIDList)
        {
            try
            {
                CrystalDecisions.CrystalReports.Engine.Database crDatabase;
                CrystalDecisions.CrystalReports.Engine.Tables crTables;
                TableLogOnInfo tableLogonInfo = new TableLogOnInfo();
                ConnectionInfo connectionInfo = new ConnectionInfo();

                connectionInfo.ServerName = DbConnection.ServerName;
                connectionInfo.UserID = DbConnection.UserName;
                connectionInfo.Password = DbConnection.Password;
                connectionInfo.DatabaseName = DbConnection.DatabaseName;


                var ShowlicenseHistoryReport = new SiteLicenseHistory();
                dsReportsDataSet.Tables[0].TableName = "SiteLicenseHistory";
                ShowlicenseHistoryReport.SetDataSource(dsReportsDataSet);

                ShowlicenseHistoryReport.SetParameterValue("@Company", CompanyId);
                ShowlicenseHistoryReport.SetParameterValue("@SubCompany", SubCompanyId);
                ShowlicenseHistoryReport.SetParameterValue("@Area", Area);
                ShowlicenseHistoryReport.SetParameterValue("@District", District);
                ShowlicenseHistoryReport.SetParameterValue("@Site", SiteId);
                ShowlicenseHistoryReport.SetParameterValue("@region", Region);
                ShowlicenseHistoryReport.SetParameterValue("DateCulture", sCurrentCurrenyCulture);
                ShowlicenseHistoryReport.SetParameterValue("BMC Version", sVersion);
                ShowlicenseHistoryReport.SetParameterValue("@startdate", StartDate);
                ShowlicenseHistoryReport.SetParameterValue("@enddate", EndDate);
                ShowlicenseHistoryReport.SetParameterValue("@SiteIDList", SiteIDList);
                ShowlicenseHistoryReport.SetParameterValue("SiteName",SiteName );
                ShowlicenseHistoryReport.SetParameterValue("District", strDistrict);
                ShowlicenseHistoryReport.SetParameterValue("Area", strArea);
                ShowlicenseHistoryReport.SetParameterValue("SubCompanyName", strSubCompanyName);
                ShowlicenseHistoryReport.SetParameterValue("CompanyName", strCompanyName);
                ShowlicenseHistoryReport.SetParameterValue("RegionName", RegionName);
                ShowlicenseHistoryReport.SetParameterValue("@RuleName", sRuleName);

                crDatabase = ShowlicenseHistoryReport.Database;
                crTables = crDatabase.Tables;

                foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
                {
                    tableLogonInfo = crTable.LogOnInfo;
                    tableLogonInfo.ConnectionInfo = connectionInfo;
                    crTable.ApplyLogOnInfo(tableLogonInfo); 
                 }
                
                crystalReportViewer1.ReportSource = ShowlicenseHistoryReport;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);

            }
        }

        public void ShowSlotEnrollment(string reportName, DataSet dsReportsDataSet, int CompanyId, int SubCompanyId, int Area, int District, int SiteId, int Region, DateTime StartDate, DateTime EndDate, string SiteName, string strDistrict, string strArea, string strSubCompanyName, string strCompanyName, string RegionName, string SiteIDList)
        {
            try
            {
                CrystalDecisions.CrystalReports.Engine.Database crDatabase;
                CrystalDecisions.CrystalReports.Engine.Tables crTables;

                TableLogOnInfo tableLogonInfo = new TableLogOnInfo();
                ConnectionInfo connectionInfo = new ConnectionInfo();

                connectionInfo.ServerName = DbConnection.ServerName;
                connectionInfo.UserID = DbConnection.UserName;
                connectionInfo.Password = DbConnection.Password;
                connectionInfo.DatabaseName = DbConnection.DatabaseName;

                var ShowSlotEnrollmentReport = new SlotEnrollment();
                dsReportsDataSet.Tables[0].TableName = "ShowSlotEnrollment";
                ShowSlotEnrollmentReport.SetDataSource(dsReportsDataSet);

                ShowSlotEnrollmentReport.SetParameterValue("@company", CompanyId);
                ShowSlotEnrollmentReport.SetParameterValue("@subcompany", SubCompanyId);
                ShowSlotEnrollmentReport.SetParameterValue("@area", Area);
                ShowSlotEnrollmentReport.SetParameterValue("@district", District);
                ShowSlotEnrollmentReport.SetParameterValue("@site", SiteId);
                ShowSlotEnrollmentReport.SetParameterValue("@region", Region);
                ShowSlotEnrollmentReport.SetParameterValue("DateCulture", sCurrentCurrenyCulture);
                ShowSlotEnrollmentReport.SetParameterValue("BMC Version", sVersion);
                ShowSlotEnrollmentReport.SetParameterValue("@startdate", StartDate);
                ShowSlotEnrollmentReport.SetParameterValue("@enddate", EndDate);
                ShowSlotEnrollmentReport.SetParameterValue("SiteName", SiteName);
                ShowSlotEnrollmentReport.SetParameterValue("District", strDistrict);
                ShowSlotEnrollmentReport.SetParameterValue("Area", strArea);
                ShowSlotEnrollmentReport.SetParameterValue("SubCompanyName", strSubCompanyName);
                ShowSlotEnrollmentReport.SetParameterValue("CompanyName", strCompanyName);
                ShowSlotEnrollmentReport.SetParameterValue("RegionName", RegionName);
                ShowSlotEnrollmentReport.SetParameterValue("@SiteIDList", SiteIDList);
                crDatabase = ShowSlotEnrollmentReport.Database;
                crTables = crDatabase.Tables;

                foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
                {
                    tableLogonInfo = crTable.LogOnInfo;
                    tableLogonInfo.ConnectionInfo = connectionInfo;
                    crTable.ApplyLogOnInfo(tableLogonInfo);
                }

                crystalReportViewer1.ReportSource = ShowSlotEnrollmentReport;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);

            }

        }

        public void ShowLicenseExpiry(string reportName, DataSet dsReportsDataSet, int CompanyId, int SubCompanyId, int Area, int District, int SiteId, int Region, DateTime StartDate, DateTime EndDate, string SiteName, string strDistrict, string strArea, string strSubCompanyName, string strCompanyName, string RegionName, string SiteIDList)
        {
            try
            {
                CrystalDecisions.CrystalReports.Engine.Database crDatabase;
                CrystalDecisions.CrystalReports.Engine.Tables crTables;

                TableLogOnInfo tableLogonInfo = new TableLogOnInfo();
                ConnectionInfo connectionInfo = new ConnectionInfo();

                connectionInfo.ServerName = DbConnection.ServerName;
                connectionInfo.UserID = DbConnection.UserName;
                connectionInfo.Password = DbConnection.Password;
                connectionInfo.DatabaseName = DbConnection.DatabaseName;

                var ShowLicenseExpiryReport = new ReportLicenseExpiry();
                dsReportsDataSet.Tables[0].TableName = "ShowLicenseExpiry";
                ShowLicenseExpiryReport.SetDataSource(dsReportsDataSet);

                ShowLicenseExpiryReport.SetParameterValue("@Company", CompanyId);
                ShowLicenseExpiryReport.SetParameterValue("@SubCompany", SubCompanyId);
                ShowLicenseExpiryReport.SetParameterValue("@Area", Area);
                ShowLicenseExpiryReport.SetParameterValue("@District", District);
                ShowLicenseExpiryReport.SetParameterValue("@Site", SiteId);
                ShowLicenseExpiryReport.SetParameterValue("@region", Region);
                ShowLicenseExpiryReport.SetParameterValue("DateCulture", sCurrentCurrenyCulture);
                ShowLicenseExpiryReport.SetParameterValue("BMC Version", sVersion);
                ShowLicenseExpiryReport.SetParameterValue("@startdate", StartDate);
                ShowLicenseExpiryReport.SetParameterValue("@enddate", EndDate);
                ShowLicenseExpiryReport.SetParameterValue("@SiteIDList", SiteIDList);
                ShowLicenseExpiryReport.SetParameterValue("SiteName", SiteName);
                ShowLicenseExpiryReport.SetParameterValue("DistrictName", strDistrict);
                ShowLicenseExpiryReport.SetParameterValue("AreaName", strArea);
                ShowLicenseExpiryReport.SetParameterValue("SubCompanyName", strSubCompanyName);
                ShowLicenseExpiryReport.SetParameterValue("CompanyName", strCompanyName);
                ShowLicenseExpiryReport.SetParameterValue("RegionName", RegionName); 


                crDatabase = ShowLicenseExpiryReport.Database;
                crTables = crDatabase.Tables;

                foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
                {
                    tableLogonInfo = crTable.LogOnInfo;
                    tableLogonInfo.ConnectionInfo = connectionInfo;
                    crTable.ApplyLogOnInfo(tableLogonInfo);
                }

                crystalReportViewer1.ReportSource = ShowLicenseExpiryReport;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);

            }

        }
        public void ShowLiquidationExpenseReport(string ReportName, DataSet dsReportsDataSet, int Company,
                                                string StrCompany,
                                                int SubCompany,
                                                string StrSubCompany,
                                                int Area,
                                                string AreaName,
                                                int District,
                                                string DistrictName,
                                                int Site,
                                                string SiteName,
                                                DateTime StartDate,
                                                DateTime EndDate ,
                                                string RegionName, int RegionId, string SiteIDList)
        {
            
            try
            {
                CrystalDecisions.CrystalReports.Engine.Database crDatabase;
                CrystalDecisions.CrystalReports.Engine.Tables crTables;

                TableLogOnInfo tableLogonInfo = new TableLogOnInfo();
                ConnectionInfo connectionInfo = new ConnectionInfo();

                connectionInfo.ServerName = DbConnection.ServerName;
                connectionInfo.UserID = DbConnection.UserName;
                connectionInfo.Password = DbConnection.Password;
                connectionInfo.DatabaseName = DbConnection.DatabaseName;

                var ShowLiquidationExpenseReport = new LiquidationExpenseReport();
                dsReportsDataSet.Tables[0].TableName = "ShowLiquidationExpenseReport";
                ShowLiquidationExpenseReport.SetDataSource(dsReportsDataSet);
                ShowLiquidationExpenseReport.SetParameterValue("Version", sVersion);
                ShowLiquidationExpenseReport.SetParameterValue("@company", Company);
                ShowLiquidationExpenseReport.SetParameterValue("@subcompany", SubCompany);
                ShowLiquidationExpenseReport.SetParameterValue("@Region", RegionId);
                ShowLiquidationExpenseReport.SetParameterValue("@area", Area);
                ShowLiquidationExpenseReport.SetParameterValue("@district", District);
                ShowLiquidationExpenseReport.SetParameterValue("@site", Site);
                ShowLiquidationExpenseReport.SetParameterValue("@CompanyName", StrCompany);
                ShowLiquidationExpenseReport.SetParameterValue("@SubCompanyName", StrSubCompany);
                ShowLiquidationExpenseReport.SetParameterValue("@SiteName", SiteName);
                ShowLiquidationExpenseReport.SetParameterValue("@CurrencySymbol", sCurrencySymbol);                 
                ShowLiquidationExpenseReport.SetParameterValue("@DistrictName", DistrictName);
                ShowLiquidationExpenseReport.SetParameterValue("@AreaName", AreaName);               
                ShowLiquidationExpenseReport.SetParameterValue("@Startdate", StartDate);
                ShowLiquidationExpenseReport.SetParameterValue("@Enddate", EndDate);
                ShowLiquidationExpenseReport.SetParameterValue("CurrencyCulture", sCurrentCurrenyCulture);
                ShowLiquidationExpenseReport.SetParameterValue("RegionName", RegionName);
                ShowLiquidationExpenseReport.SetParameterValue("@SiteIDList", SiteIDList);

                crDatabase = ShowLiquidationExpenseReport.Database;
                crTables = crDatabase.Tables;

                foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
                {
                    tableLogonInfo = crTable.LogOnInfo;
                    tableLogonInfo.ConnectionInfo = connectionInfo;
                    crTable.ApplyLogOnInfo(tableLogonInfo);
                }

                crystalReportViewer1.ReportSource = ShowLiquidationExpenseReport;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        public void ShowPeriodEndLiquidationRevenueReport(string ReportName, DataSet dsReportsDataSet, int CompanyId,
                                            string StrCompany,
                                            int SubCompanyId,
                                            string StrSubCompany,
                                            int Area,
                                            string strArea,
                                            int District,
                                            string strDistrict,
                                            int SiteId,
                                            string SiteName,
                                            DateTime StartDate,
                                            DateTime EndDate,
                                            string RegionName, int RegionId, string SiteIDList)
        {
            try
            {
                CrystalDecisions.CrystalReports.Engine.Database crDatabase;
                CrystalDecisions.CrystalReports.Engine.Tables crTables;

                TableLogOnInfo tableLogonInfo = new TableLogOnInfo();
                ConnectionInfo connectionInfo = new ConnectionInfo();

                connectionInfo.ServerName = DbConnection.ServerName;
                connectionInfo.UserID = DbConnection.UserName;
                connectionInfo.Password = DbConnection.Password;
                connectionInfo.DatabaseName = DbConnection.DatabaseName;

                var ShowPeriodEndLiquidationRevenueReport = new PeriodEndLiquidationRevenueReport();
                dsReportsDataSet.Tables[0].TableName = "ShowPeriodEndLiquidationRevenueReport";
                ShowPeriodEndLiquidationRevenueReport.SetDataSource(dsReportsDataSet);
                ShowPeriodEndLiquidationRevenueReport.SetParameterValue("Version", sVersion);
                ShowPeriodEndLiquidationRevenueReport.SetParameterValue("@company", CompanyId);
                ShowPeriodEndLiquidationRevenueReport.SetParameterValue("@Subcompany", SubCompanyId);
                ShowPeriodEndLiquidationRevenueReport.SetParameterValue("@Region", RegionId);
                ShowPeriodEndLiquidationRevenueReport.SetParameterValue("@Area", Area);
                ShowPeriodEndLiquidationRevenueReport.SetParameterValue("@District", District);
                ShowPeriodEndLiquidationRevenueReport.SetParameterValue("@Startdate", StartDate);
                ShowPeriodEndLiquidationRevenueReport.SetParameterValue("@Enddate", EndDate);
                ShowPeriodEndLiquidationRevenueReport.SetParameterValue("@Site", SiteId);
                ShowPeriodEndLiquidationRevenueReport.SetParameterValue("@CompanyName", StrCompany);
                ShowPeriodEndLiquidationRevenueReport.SetParameterValue("@SubCompanyName", StrSubCompany);
                ShowPeriodEndLiquidationRevenueReport.SetParameterValue("@DistrictName", strDistrict);
                ShowPeriodEndLiquidationRevenueReport.SetParameterValue("@AreaName", strArea);
                ShowPeriodEndLiquidationRevenueReport.SetParameterValue("@SiteName", SiteName);       
                         
                ShowPeriodEndLiquidationRevenueReport.SetParameterValue("@CurrencySymbol", sCurrencySymbol);
                ShowPeriodEndLiquidationRevenueReport.SetParameterValue("CurrencyCulture", sCurrentCurrenyCulture);
                ShowPeriodEndLiquidationRevenueReport.SetParameterValue("RegionName", RegionName);
                ShowPeriodEndLiquidationRevenueReport.SetParameterValue("@SiteIDList", SiteIDList);
                crDatabase = ShowPeriodEndLiquidationRevenueReport.Database;
                crTables = crDatabase.Tables;

                foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
                {
                    tableLogonInfo = crTable.LogOnInfo;
                    tableLogonInfo.ConnectionInfo = connectionInfo;
                    crTable.ApplyLogOnInfo(tableLogonInfo);
                }

                crystalReportViewer1.ReportSource = ShowPeriodEndLiquidationRevenueReport;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
		
        //internal void ShowDeclarationBatchVouchersDetailsReport(string ReportName, DataSet dtSet, int Company, int SubCompany, int Site, string Slot, DateTime startDate, DateTime endDate, string strCompany, string strSubCompany, string strSiteName)
        //{
        //    try
        //    {
        //        CrystalDecisions.CrystalReports.Engine.Database crDatabase;
        //        CrystalDecisions.CrystalReports.Engine.Tables crTables;

        //        TableLogOnInfo tableLogonInfo = new TableLogOnInfo();
        //        ConnectionInfo connectionInfo = new ConnectionInfo();

        //        connectionInfo.ServerName = DbConnection.ServerName;
        //        connectionInfo.UserID = DbConnection.UserName;
        //        connectionInfo.Password = DbConnection.Password;
        //        connectionInfo.DatabaseName = DbConnection.DatabaseName;

        //        var vDeclaredVouchersReport = new DeclaredVouchersReport();
        //        dtSet.Tables[0].TableName = "DeclarationBatchVouchersDetailsReport";

        //        vDeclaredVouchersReport.SetDataSource(dtSet);
        //        vDeclaredVouchersReport.SetParameterValue("@Company", Company);
        //        vDeclaredVouchersReport.SetParameterValue("@SubCompany", SubCompany);
        //        vDeclaredVouchersReport.SetParameterValue("@Site", Site);
        //        vDeclaredVouchersReport.SetParameterValue("@Slot", Slot);
        //        vDeclaredVouchersReport.SetParameterValue("@startdate", startDate);
        //        vDeclaredVouchersReport.SetParameterValue("@enddate", endDate);
        //        vDeclaredVouchersReport.SetParameterValue("BMCVersion", sVersion);
        //        vDeclaredVouchersReport.SetParameterValue("CurrencyCulture", sCurrentCurrenyCulture);
        //        vDeclaredVouchersReport.SetParameterValue("CurrencySymbol", sCurrencySymbol);
        //        vDeclaredVouchersReport.SetParameterValue("CompanyName", strCompany);
        //        vDeclaredVouchersReport.SetParameterValue("SubCompany", strSubCompany);
        //        vDeclaredVouchersReport.SetParameterValue("SiteName", strSiteName);

        //        crDatabase = vDeclaredVouchersReport.Database;
        //        crTables = crDatabase.Tables;

        //        foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
        //        {
        //            tableLogonInfo = crTable.LogOnInfo;
        //            tableLogonInfo.ConnectionInfo = connectionInfo;
        //            crTable.ApplyLogOnInfo(tableLogonInfo);
        //        }
        //        crystalReportViewer1.ReportSource = vDeclaredVouchersReport;
        //    }
        //    catch (Exception ex)
        //    {
        //        ExceptionManager.Publish(ex);
        //    }
        //}
		 internal void ShowTotalFundsInSummary(int companyID, string company, int subCompanyID,
             string subCompany, int siteID, string siteName, string SiteIDList)
        {
            try
            {
                CrystalDecisions.CrystalReports.Engine.Database crDatabase;
                CrystalDecisions.CrystalReports.Engine.Tables crTables;

                TableLogOnInfo tableLogonInfo = new TableLogOnInfo();
                ConnectionInfo connectionInfo = new ConnectionInfo();

                connectionInfo.ServerName = DbConnection.ServerName;
                connectionInfo.UserID = DbConnection.UserName;
                connectionInfo.Password = DbConnection.Password;
                connectionInfo.DatabaseName = DbConnection.DatabaseName;

                TotalFundsInSummary oTotalFundsIn = new TotalFundsInSummary();

                //SP parameters
                oTotalFundsIn.SetParameterValue("@Company", companyID);
                oTotalFundsIn.SetParameterValue("@SubCompany", subCompanyID);
                oTotalFundsIn.SetParameterValue("@Site", siteID);
                oTotalFundsIn.SetParameterValue("@SiteIDList", SiteIDList);
             
                //parameters
              
                oTotalFundsIn.SetParameterValue("SiteName", siteName);
                oTotalFundsIn.SetParameterValue("Version", sVersion);
                oTotalFundsIn.SetParameterValue("CurrencySymbol", sCurrencySymbol);
                oTotalFundsIn.SetParameterValue("CurrencyCulture", sCurrentCurrenyCulture);
                oTotalFundsIn.SetParameterValue("DateCulture", sCurrentCurrenyCulture);
                oTotalFundsIn.SetParameterValue("CompanyName", company);
                oTotalFundsIn.SetParameterValue("SubCompanyName", subCompany);


                crDatabase = oTotalFundsIn.Database;
                crTables = crDatabase.Tables;

                foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
                {
                    tableLogonInfo = crTable.LogOnInfo;
                    tableLogonInfo.ConnectionInfo = connectionInfo;
                    crTable.ApplyLogOnInfo(tableLogonInfo);
                }

                crystalReportViewer1.ReportSource = oTotalFundsIn;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        internal void ShowTotalFundsInDetails(int companyID, string company, int subCompanyID,
         string subCompany, int siteID, string siteName, int ZoneID, string ZoneName, string SiteIDList)
        {
            try
            {
                CrystalDecisions.CrystalReports.Engine.Database crDatabase;
                CrystalDecisions.CrystalReports.Engine.Tables crTables;

                TableLogOnInfo tableLogonInfo = new TableLogOnInfo();
                ConnectionInfo connectionInfo = new ConnectionInfo();

                connectionInfo.ServerName = DbConnection.ServerName;
                connectionInfo.UserID = DbConnection.UserName;
                connectionInfo.Password = DbConnection.Password;
                connectionInfo.DatabaseName = DbConnection.DatabaseName;

                TotalFundsInDetailsReport oTotalFundsInDetails = new TotalFundsInDetailsReport();

                //SP parameters
                oTotalFundsInDetails.SetParameterValue("@Company", companyID);
                oTotalFundsInDetails.SetParameterValue("@SubCompany", subCompanyID);
                oTotalFundsInDetails.SetParameterValue("@Site", siteID);
                oTotalFundsInDetails.SetParameterValue("@SiteIDList", SiteIDList);
                //parameters

                oTotalFundsInDetails.SetParameterValue("SiteName", siteName);
                oTotalFundsInDetails.SetParameterValue("Version", sVersion);
                oTotalFundsInDetails.SetParameterValue("CurrencySymbol", sCurrencySymbol);
                oTotalFundsInDetails.SetParameterValue("CurrencyCulture", sCurrentCurrenyCulture);
                oTotalFundsInDetails.SetParameterValue("DateCulture", sCurrentCurrenyCulture);
                oTotalFundsInDetails.SetParameterValue("@Zone", ZoneID);
                oTotalFundsInDetails.SetParameterValue("IsZoneRequired", false);
                oTotalFundsInDetails.SetParameterValue("ZoneName", ZoneName);
                oTotalFundsInDetails.SetParameterValue("CompanyName", company);
                oTotalFundsInDetails.SetParameterValue("SubCompanyName", subCompany);


                crDatabase = oTotalFundsInDetails.Database;
                crTables = crDatabase.Tables;

                foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
                {
                    tableLogonInfo = crTable.LogOnInfo;
                    tableLogonInfo.ConnectionInfo = connectionInfo;
                    crTable.ApplyLogOnInfo(tableLogonInfo);
                }

                crystalReportViewer1.ReportSource = oTotalFundsInDetails;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        public void ShowVaultBalanceReport(DataSet dsReportsDataSet, DateTime startDate, DateTime endDate, int company,
           int subCompany, int siteID, string siteName, int Zone, string CompanyName, string SubCompanyName, string SiteIDList)
        {

            try
            {
                CrystalDecisions.CrystalReports.Engine.Database crDatabase;
                CrystalDecisions.CrystalReports.Engine.Tables crTables;

                TableLogOnInfo tableLogonInfo = new TableLogOnInfo();
                ConnectionInfo connectionInfo = new ConnectionInfo();

                connectionInfo.ServerName = DbConnection.ServerName;
                connectionInfo.UserID = DbConnection.UserName;
                connectionInfo.Password = DbConnection.Password;
                connectionInfo.DatabaseName = DbConnection.DatabaseName;

                TotalVaultBalanceReport VaultBal = new TotalVaultBalanceReport();

                VaultBal.SetParameterValue("@Company", company);
                VaultBal.SetParameterValue("@SubCompany", subCompany);
                VaultBal.SetParameterValue("@Site", siteID);
                VaultBal.SetParameterValue("Version", sVersion);
                VaultBal.SetParameterValue("CurrencySymbol", sCurrencySymbol);
                //VaultBal.SetParameterValue("CurrencyCulture", sCurrentCurrenyCulture);
                //VaultBal.SetParameterValue("DateCulture", sCurrentCurrenyCulture);

                VaultBal.SetParameterValue("SiteName", siteName);
                VaultBal.SetParameterValue("CompanyName", CompanyName);
                VaultBal.SetParameterValue("SubCompanyName", SubCompanyName);
                VaultBal.SetParameterValue("@SiteIDList", SiteIDList);

                crDatabase = VaultBal.Database;
                crTables = crDatabase.Tables;

                foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
                {
                    tableLogonInfo = crTable.LogOnInfo;
                    tableLogonInfo.ConnectionInfo = connectionInfo;
                    crTable.ApplyLogOnInfo(tableLogonInfo);
                }

                crystalReportViewer1.ReportSource = VaultBal;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("ShowVaultBalanceReport", LogManager.enumLogLevel.Info);
                ExceptionManager.Publish(ex);
            }
        }

        public void ShowCappedGameSummaryReport(int companyID, string company, int subCompanyID,
             string subCompany, int siteID, string siteName, int iOrderBy, string OrderBy, DateTime StartDate, DateTime EndDate, string SiteIDList)
        {
            sVersion = oReportsBusiness.GetBMCVersion();
            try
            {
                CrystalDecisions.CrystalReports.Engine.Database crDatabase;
                CrystalDecisions.CrystalReports.Engine.Tables crTables;

                TableLogOnInfo tableLogonInfo = new TableLogOnInfo();
                ConnectionInfo connectionInfo = new ConnectionInfo();

                connectionInfo.ServerName = DbConnection.ServerName;
                connectionInfo.UserID = DbConnection.UserName;
                connectionInfo.Password = DbConnection.Password;
                connectionInfo.DatabaseName = DbConnection.DatabaseName;

                CappedGameSummaryReport oCappedGame = new CappedGameSummaryReport();

                oCappedGame.SetParameterValue("@Company", companyID);
                oCappedGame.SetParameterValue("@SubCompany", subCompanyID);
                oCappedGame.SetParameterValue("@Site", siteID);
                oCappedGame.SetParameterValue("Version", sVersion);
                oCappedGame.SetParameterValue("SiteName", siteName);
                oCappedGame.SetParameterValue("OrderByName", OrderBy);
                oCappedGame.SetParameterValue("@OrderBy", iOrderBy);
                oCappedGame.SetParameterValue("CompanyName", company);
                oCappedGame.SetParameterValue("SubCompanyName", subCompany);
                oCappedGame.SetParameterValue("@StartDate", StartDate);
                oCappedGame.SetParameterValue("@EndDate", EndDate);
                oCappedGame.SetParameterValue("@SiteIDList", SiteIDList);
                crDatabase = oCappedGame.Database;
                crTables = crDatabase.Tables;

                foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
                {
                    tableLogonInfo = crTable.LogOnInfo;
                    tableLogonInfo.ConnectionInfo = connectionInfo;
                    crTable.ApplyLogOnInfo(tableLogonInfo);
                }

                crystalReportViewer1.ReportSource = oCappedGame;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("GameCappingSummary Report", LogManager.enumLogLevel.Info);
                ExceptionManager.Publish(ex);
            }
        }
        public void ShowCappedGameListReport(int companyID, string company, int subCompanyID,
            string subCompany, int siteID, string siteName, int iOrderBy, string OrderBy, DateTime StartDate, DateTime EndDate, string SiteIDList)
        {
            sVersion = oReportsBusiness.GetBMCVersion();
            try
            {
                CrystalDecisions.CrystalReports.Engine.Database crDatabase;
                CrystalDecisions.CrystalReports.Engine.Tables crTables;

                TableLogOnInfo tableLogonInfo = new TableLogOnInfo();
                ConnectionInfo connectionInfo = new ConnectionInfo();

                connectionInfo.ServerName = DbConnection.ServerName;
                connectionInfo.UserID = DbConnection.UserName;
                connectionInfo.Password = DbConnection.Password;
                connectionInfo.DatabaseName = DbConnection.DatabaseName;

                CappedGameListReport oCappedGame = new CappedGameListReport();

                oCappedGame.SetParameterValue("@Company", companyID);
                oCappedGame.SetParameterValue("@SubCompany", subCompanyID);
                oCappedGame.SetParameterValue("@Site", siteID);
                oCappedGame.SetParameterValue("Version", sVersion);
                oCappedGame.SetParameterValue("@SiteName", siteName);
                oCappedGame.SetParameterValue("OrderByName", OrderBy);
                oCappedGame.SetParameterValue("@OrderBy", iOrderBy);
                oCappedGame.SetParameterValue("@CompanyName", company);
                oCappedGame.SetParameterValue("@SubCompanyName", subCompany);
                oCappedGame.SetParameterValue("@StartDate", StartDate);
                oCappedGame.SetParameterValue("@EndDate", EndDate);
                oCappedGame.SetParameterValue("@SiteIDList", SiteIDList);
                crDatabase = oCappedGame.Database;
                crTables = crDatabase.Tables;

                foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
                {
                    tableLogonInfo = crTable.LogOnInfo;
                    tableLogonInfo.ConnectionInfo = connectionInfo;
                    crTable.ApplyLogOnInfo(tableLogonInfo);
                }

                crystalReportViewer1.ReportSource = oCappedGame;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("GameCappingListReport", LogManager.enumLogLevel.Info);
                ExceptionManager.Publish(ex);
            }
        }

         public void ShowPromoSummaryReport(DataSet dtSet, int Company,string strCompany,int SubCompany,string strSubCompany,int Region,string RegionName, int Area,string strArea,int District,string strDistrict,int Site,string strSiteName,string StartDate,string EndDate,string sSiteIDList)
        {

            try
            {
                CrystalDecisions.CrystalReports.Engine.Database crDatabase;
                CrystalDecisions.CrystalReports.Engine.Tables crTables;

                TableLogOnInfo tableLogonInfo = new TableLogOnInfo();
                ConnectionInfo connectionInfo = new ConnectionInfo();

                connectionInfo.ServerName = DbConnection.ServerName;
                connectionInfo.UserID = DbConnection.UserName;
                connectionInfo.Password = DbConnection.Password;
                connectionInfo.DatabaseName = DbConnection.DatabaseName;

                PromotionalHistoryReport PromoReport = new PromotionalHistoryReport();
                PromoReport.SetParameterValue("@COMPANY", Company);
                PromoReport.SetParameterValue("@CompanyName", strCompany);
                PromoReport.SetParameterValue("@SUBCOMPANY", SubCompany);
                PromoReport.SetParameterValue("@SubCompanyName", strSubCompany);
                PromoReport.SetParameterValue("@SITE", Site);
                PromoReport.SetParameterValue("@SiteNameVal", strSiteName);
                PromoReport.SetParameterValue("@REGION", Region);
                PromoReport.SetParameterValue("@RegionName", RegionName);
                PromoReport.SetParameterValue("@AREA", Area);
                PromoReport.SetParameterValue("@AreaName", strArea);
                PromoReport.SetParameterValue("@DISTRICT", District);
                PromoReport.SetParameterValue("@DistrictName", strDistrict);
                PromoReport.SetParameterValue("Version", sVersion);
                PromoReport.SetParameterValue("CurrencySymbol", sCurrencySymbol);
                PromoReport.SetParameterValue("@STARTDATE", StartDate);
                PromoReport.SetParameterValue("@ENDDATE", EndDate);
                PromoReport.SetParameterValue("DateCulture", sCurrentCurrenyCulture);
                PromoReport.SetParameterValue("@SiteIDList", sSiteIDList);
               

                crDatabase = PromoReport.Database;
                crTables = crDatabase.Tables;

                foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
                {
                    tableLogonInfo = crTable.LogOnInfo;
                    tableLogonInfo.ConnectionInfo = connectionInfo;
                    crTable.ApplyLogOnInfo(tableLogonInfo);
                }

                crystalReportViewer1.ReportSource = PromoReport;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("ShowPromoSummaryReport", LogManager.enumLogLevel.Info);
                ExceptionManager.Publish(ex);
            }
        }
        //(Ep4 Changes)
         public void ShowCashDispenserDropReport(string ReportName, DataSet dsReportsDataSet, int Company, string StrCompany, int SubCompany, string StrSubCompany, int Site, string SiteName, string Status, bool IncludeZero, int UserID, string StartDate, string EndDate, string SiteIDList)
         {
             try
             {
                 CrystalDecisions.CrystalReports.Engine.Database crDatabase;
                 CrystalDecisions.CrystalReports.Engine.Tables crTables;

                 TableLogOnInfo tableLogonInfo = new TableLogOnInfo();
                 ConnectionInfo connectionInfo = new ConnectionInfo();

                 connectionInfo.ServerName = DbConnection.ServerName;
                 connectionInfo.UserID = DbConnection.UserName;
                 connectionInfo.Password = DbConnection.Password;
                 connectionInfo.DatabaseName = DbConnection.DatabaseName;

                 var CashDispDropReport = new CashDispenserDropReport();
                 dsReportsDataSet.Tables[0].TableName = "CashDispenserDropReport";

                 CashDispDropReport.SetDataSource(dsReportsDataSet);
                 CashDispDropReport.SetParameterValue("@Company", Company);
                 CashDispDropReport.SetParameterValue("@SubCompany", SubCompany);
                 CashDispDropReport.SetParameterValue("CompanyName", StrCompany);
                 CashDispDropReport.SetParameterValue("SubCompanyName", StrSubCompany);
                 CashDispDropReport.SetParameterValue("@Site", Site);
                 CashDispDropReport.SetParameterValue("@VaultStatus", Status);
                 CashDispDropReport.SetParameterValue("@UserID", UserID);
                 CashDispDropReport.SetParameterValue("@Startdate", StartDate);
                 CashDispDropReport.SetParameterValue("@Enddate", EndDate);
                 CashDispDropReport.SetParameterValue("@IncludeZero", IncludeZero);
                 CashDispDropReport.SetParameterValue("@SiteIDList", SiteIDList);
                 CashDispDropReport.SetParameterValue("SiteName", SiteName);
                 CashDispDropReport.SetParameterValue("Version", sVersion);
                 CashDispDropReport.SetParameterValue("CurrencyCulture", sCurrentCurrenyCulture);
                 CashDispDropReport.SetParameterValue("CurrencySymbol", sCurrencySymbol);
                 CashDispDropReport.SetParameterValue("DateCulture", sCurrentCurrenyCulture);

                 crDatabase = CashDispDropReport.Database;
                 crTables = crDatabase.Tables;

                 foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
                 {
                     tableLogonInfo = crTable.LogOnInfo;
                     tableLogonInfo.ConnectionInfo = connectionInfo;
                     crTable.ApplyLogOnInfo(tableLogonInfo);
                 }
                 crystalReportViewer1.ReportSource = CashDispDropReport;

             }
             catch (Exception ex)
             {
                 ExceptionManager.Publish(ex);
             }

         }

         public void ShowCashDispenserVarianceReport(string ReportName, DataSet dsReportsDataSet, int Company, string StrCompany, int SubCompany, string StrSubCompany, int Site, string SiteName, string Status, bool IncludeZero, string StartDate, string EndDate, int UserID, string SiteIDList)
         {
             try
             {
                 CrystalDecisions.CrystalReports.Engine.Database crDatabase;
                 CrystalDecisions.CrystalReports.Engine.Tables crTables;

                 TableLogOnInfo tableLogonInfo = new TableLogOnInfo();
                 ConnectionInfo connectionInfo = new ConnectionInfo();

                 connectionInfo.ServerName = DbConnection.ServerName;
                 connectionInfo.UserID = DbConnection.UserName;
                 connectionInfo.Password = DbConnection.Password;
                 connectionInfo.DatabaseName = DbConnection.DatabaseName;

                 var CashDispVarianceReport = new CashDispenserVarianceReport();
                 dsReportsDataSet.Tables[0].TableName = "CashDispenserVarianceReport";

                 CashDispVarianceReport.SetDataSource(dsReportsDataSet);
                 CashDispVarianceReport.SetParameterValue("@Company", Company);
                 CashDispVarianceReport.SetParameterValue("@SubCompany", SubCompany);
                 CashDispVarianceReport.SetParameterValue("CompanyName", StrCompany);
                 CashDispVarianceReport.SetParameterValue("SubCompanyName", StrSubCompany);
                 CashDispVarianceReport.SetParameterValue("@Site", Site);
                 CashDispVarianceReport.SetParameterValue("@VaultStatus", Status);
                 CashDispVarianceReport.SetParameterValue("@Startdate", StartDate);
                 CashDispVarianceReport.SetParameterValue("@Enddate", EndDate);
                 CashDispVarianceReport.SetParameterValue("@IncludeZero", IncludeZero);
                 CashDispVarianceReport.SetParameterValue("SiteName", SiteName);
                 CashDispVarianceReport.SetParameterValue("Version", sVersion);
                 CashDispVarianceReport.SetParameterValue("@UserID", UserID);
                 CashDispVarianceReport.SetParameterValue("@SiteIDList", SiteIDList);
                 CashDispVarianceReport.SetParameterValue("CurrencyCulture", sCurrentCurrenyCulture);
                 CashDispVarianceReport.SetParameterValue("CurrencySymbol", sCurrencySymbol);
                 CashDispVarianceReport.SetParameterValue("DateCulture", sCurrentCurrenyCulture);

                 crDatabase = CashDispVarianceReport.Database;
                 crTables = crDatabase.Tables;

                 foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
                 {
                     tableLogonInfo = crTable.LogOnInfo;
                     tableLogonInfo.ConnectionInfo = connectionInfo;
                     crTable.ApplyLogOnInfo(tableLogonInfo);
                 }
                 crystalReportViewer1.ReportSource = CashDispVarianceReport;
             }
             catch (Exception ex)
             {
                 ExceptionManager.Publish(ex);
             }
         }

         public void ShowCashDispTransactionDetails(string ReportName, DataSet dsReportsDataSet, int Company, string StrCompany, int SubCompany, string StrSubCompany, int Site, string SiteName, string TransType, string StartDate, string EndDate, int UserID, string SiteIDList)
         {
             try
             {
                 CrystalDecisions.CrystalReports.Engine.Database crDatabase;
                 CrystalDecisions.CrystalReports.Engine.Tables crTables;

                 TableLogOnInfo tableLogonInfo = new TableLogOnInfo();
                 ConnectionInfo connectionInfo = new ConnectionInfo();

                 connectionInfo.ServerName = DbConnection.ServerName;
                 connectionInfo.UserID = DbConnection.UserName;
                 connectionInfo.Password = DbConnection.Password;
                 connectionInfo.DatabaseName = DbConnection.DatabaseName;

                 var CashDispTransDetails = new CashDispenserTransactionDetails();
                 dsReportsDataSet.Tables[0].TableName = "CashDispenserTransactionDetails";

                 CashDispTransDetails.SetDataSource(dsReportsDataSet);
                 CashDispTransDetails.SetParameterValue("@Company", Company);
                 CashDispTransDetails.SetParameterValue("@SubCompany", SubCompany);
                 CashDispTransDetails.SetParameterValue("CompanyName", StrCompany);
                 CashDispTransDetails.SetParameterValue("SubCompanyName", StrSubCompany);
                 CashDispTransDetails.SetParameterValue("@Site", Site);
                 CashDispTransDetails.SetParameterValue("@TransactionType", TransType);
                 CashDispTransDetails.SetParameterValue("@Startdate", StartDate);
                 CashDispTransDetails.SetParameterValue("@Enddate", EndDate);
                 CashDispTransDetails.SetParameterValue("SiteName", SiteName);
                 CashDispTransDetails.SetParameterValue("Version", sVersion);
                 CashDispTransDetails.SetParameterValue("@UserID", UserID);
                 CashDispTransDetails.SetParameterValue("@SiteIDList", SiteIDList);
                 CashDispTransDetails.SetParameterValue("CurrencyCulture", sCurrentCurrenyCulture);
                 CashDispTransDetails.SetParameterValue("CurrencySymbol", sCurrencySymbol);
                 CashDispTransDetails.SetParameterValue("DateCulture", sCurrentCurrenyCulture);

                 crDatabase = CashDispTransDetails.Database;
                 crTables = crDatabase.Tables;

                 foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
                 {
                     tableLogonInfo = crTable.LogOnInfo;
                     tableLogonInfo.ConnectionInfo = connectionInfo;
                     crTable.ApplyLogOnInfo(tableLogonInfo);
                 }
                 crystalReportViewer1.ReportSource = CashDispTransDetails;
             }
             catch (Exception ex)
             {
                 ExceptionManager.Publish(ex);
             }
         }

         public void ShowCashDispLevelDetails(string ReportName, DataSet dsReportsDataSet, int Company, string StrCompany, int SubCompany,
             string StrSubCompany, int Site, string SiteName, int Region, string strRegion, int Area, string StrArea, int District,
             string StrDistrict, int InventoryLevel, string InventoryType, int UserID, string SiteIDList)
         {
             try
             {
                 CrystalDecisions.CrystalReports.Engine.Database crDatabase;
                 CrystalDecisions.CrystalReports.Engine.Tables crTables;

                 TableLogOnInfo tableLogonInfo = new TableLogOnInfo();
                 ConnectionInfo connectionInfo = new ConnectionInfo();

                 connectionInfo.ServerName = DbConnection.ServerName;
                 connectionInfo.UserID = DbConnection.UserName;
                 connectionInfo.Password = DbConnection.Password;
                 connectionInfo.DatabaseName = DbConnection.DatabaseName;

                 var CashDispLevelDet = new CashDispenserLevelDetails();
                 dsReportsDataSet.Tables[0].TableName = "CashDispenserLevelDetails";

                 CashDispLevelDet.SetDataSource(dsReportsDataSet);
                 CashDispLevelDet.SetParameterValue("@Company", Company);
                 CashDispLevelDet.SetParameterValue("@SubCompany", SubCompany);
                 CashDispLevelDet.SetParameterValue("@Region", Region);
                 CashDispLevelDet.SetParameterValue("@Area", Area);
                 CashDispLevelDet.SetParameterValue("@District", District);
                 CashDispLevelDet.SetParameterValue("@Site", Site);
                 CashDispLevelDet.SetParameterValue("@InventoryLevel", InventoryLevel);
                 CashDispLevelDet.SetParameterValue("@InventoryType", InventoryType);
                 CashDispLevelDet.SetParameterValue("@UserID", UserID);
                 CashDispLevelDet.SetParameterValue("@SiteIDList", SiteIDList);
                 CashDispLevelDet.SetParameterValue("SiteName", SiteName);
                 CashDispLevelDet.SetParameterValue("CompanyName", StrCompany);
                 CashDispLevelDet.SetParameterValue("SubCompanyName", StrSubCompany);
                 CashDispLevelDet.SetParameterValue("CurrencyCulture", sCurrentCurrenyCulture);
                 CashDispLevelDet.SetParameterValue("CurrencySymbol", sCurrencySymbol);
                 CashDispLevelDet.SetParameterValue("DateCulture", sCurrentCurrenyCulture);
                 CashDispLevelDet.SetParameterValue("Version", sVersion);
                 CashDispLevelDet.SetParameterValue("RegionName", strRegion);
                 CashDispLevelDet.SetParameterValue("AreaName", StrArea);
                 CashDispLevelDet.SetParameterValue("DistrictName", StrDistrict);

                 crDatabase = CashDispLevelDet.Database;
                 crTables = crDatabase.Tables;

                 foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
                 {
                     tableLogonInfo = crTable.LogOnInfo;
                     tableLogonInfo.ConnectionInfo = connectionInfo;
                     crTable.ApplyLogOnInfo(tableLogonInfo);
                 }
                 crystalReportViewer1.ReportSource = CashDispLevelDet;
             }
             catch (Exception ex)
             {
                 ExceptionManager.Publish(ex);
             }
         }
        
         public void ShowCashDispCassetteInventoryStatus(string ReportName, DataSet dsReportsDataSet, int Company, string StrCompany, int SubCompany,
             string StrSubCompany, int Site, string SiteName, int UserID, string SiteIDList)
         {
             try
             {
                 CrystalDecisions.CrystalReports.Engine.Database crDatabase;
                 CrystalDecisions.CrystalReports.Engine.Tables crTables;

                 TableLogOnInfo tableLogonInfo = new TableLogOnInfo();
                 ConnectionInfo connectionInfo = new ConnectionInfo();

                 connectionInfo.ServerName = DbConnection.ServerName;
                 connectionInfo.UserID = DbConnection.UserName;
                 connectionInfo.Password = DbConnection.Password;
                 connectionInfo.DatabaseName = DbConnection.DatabaseName;

                 var CashDispCassetteStatus = new CashDispenserCassettesInventoryDetail();
                 dsReportsDataSet.Tables[0].TableName = "CashDispenserCassetteStatus";

                 CashDispCassetteStatus.SetDataSource(dsReportsDataSet);
                 CashDispCassetteStatus.SetParameterValue("@Company", Company);
                 CashDispCassetteStatus.SetParameterValue("@SubCompany", SubCompany);
                 CashDispCassetteStatus.SetParameterValue("@Site", Site);                 
                 CashDispCassetteStatus.SetParameterValue("@UserID", UserID);
                 CashDispCassetteStatus.SetParameterValue("@SiteIDList", SiteIDList);
                 CashDispCassetteStatus.SetParameterValue("CompanyName", StrCompany);
                 CashDispCassetteStatus.SetParameterValue("SubCompanyName", StrSubCompany);
                 CashDispCassetteStatus.SetParameterValue("SiteName", SiteName);
                 CashDispCassetteStatus.SetParameterValue("CurrencyCulture", sCurrentCurrenyCulture);
                 CashDispCassetteStatus.SetParameterValue("CurrencySymbol", sCurrencySymbol);
                 CashDispCassetteStatus.SetParameterValue("DateCulture", sCurrentCurrenyCulture);
                 CashDispCassetteStatus.SetParameterValue("Version", sVersion);

                 crDatabase = CashDispCassetteStatus.Database;
                 crTables = crDatabase.Tables;

                 foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
                 {
                     tableLogonInfo = crTable.LogOnInfo;
                     tableLogonInfo.ConnectionInfo = connectionInfo;
                     crTable.ApplyLogOnInfo(tableLogonInfo);
                 }
                 crystalReportViewer1.ReportSource = CashDispCassetteStatus;
             }
             catch (Exception ex)
             {
                 ExceptionManager.Publish(ex);
             }
         }

         public void ShowVaultConfigurationReport(string ReportName, DataSet dsReportsDataSet, int Company, string StrCompany, int SubCompany, string StrSubCompany, int Site, string SiteName, string Status, int UserID)
         {

             try
             {
                 CrystalDecisions.CrystalReports.Engine.Database crDatabase;
                 CrystalDecisions.CrystalReports.Engine.Tables crTables;

                 TableLogOnInfo tableLogonInfo = new TableLogOnInfo();
                 ConnectionInfo connectionInfo = new ConnectionInfo();

                 connectionInfo.ServerName = DbConnection.ServerName;
                 connectionInfo.UserID = DbConnection.UserName;
                 connectionInfo.Password = DbConnection.Password;
                 connectionInfo.DatabaseName = DbConnection.DatabaseName;


                 CashDispenserConfigurationDetailsReport v_config = new CashDispenserConfigurationDetailsReport();

                 if(Status != "Active" && Status != "Assigned To Site")
                 {
                     SiteName = "All Sites";
                     StrCompany = "--All--";
                     StrSubCompany = "--All--";
                 }

                 v_config.SetParameterValue("@Company", Company);
                 v_config.SetParameterValue("@SubCompany", SubCompany);
                 v_config.SetParameterValue("@Site", Site);
                 v_config.SetParameterValue("Version", sVersion);
                 v_config.SetParameterValue("CurrencySymbol", sCurrencySymbol);
                 v_config.SetParameterValue("CurrencyCulture", sCurrentCurrenyCulture);
                 v_config.SetParameterValue("DateCulture", sCurrentCurrenyCulture);

                 v_config.SetParameterValue("SiteName", SiteName);
                 v_config.SetParameterValue("CompanyName", StrCompany);
                 v_config.SetParameterValue("SubCompanyName", StrSubCompany);
                 v_config.SetParameterValue("@userID", UserID);
                 v_config.SetParameterValue("@VaultStatus", Status);


                 crDatabase = v_config.Database;
                 crTables = crDatabase.Tables;

                 foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
                 {
                     tableLogonInfo = crTable.LogOnInfo;
                     tableLogonInfo.ConnectionInfo = connectionInfo;
                     crTable.ApplyLogOnInfo(tableLogonInfo);
                 }

                 crystalReportViewer1.ReportSource = v_config;
             }
             catch (Exception ex)
             {
                 LogManager.WriteLog("ShowVaultConfigurationReport", LogManager.enumLogLevel.Info);
                 ExceptionManager.Publish(ex);
             }
         }   
		// Ep4 Changes Ends 
        public void ShowCashDispenserAccountingReport(string ReportName, DataSet dsReportset, int Company, string strCompany, int SubCompany, string strSubcompany,
            int site,string siteName,string StartDate, string EndDate, int userId, string SiteIDList)

        {
            try
            {
                CrystalDecisions.CrystalReports.Engine.Database crDatabase;
                CrystalDecisions.CrystalReports.Engine.Tables crTables;

                TableLogOnInfo tableLogonInfo = new TableLogOnInfo();
                ConnectionInfo connectionInfo = new ConnectionInfo();

                connectionInfo.ServerName = DbConnection.ServerName;
                connectionInfo.UserID = DbConnection.UserName;
                connectionInfo.Password = DbConnection.Password;
                connectionInfo.DatabaseName = DbConnection.DatabaseName;

                CashDispenserAccountingReport_ _CashDispenserAccounting = new CashDispenserAccountingReport_();

                _CashDispenserAccounting.SetParameterValue("@Company", Company);
                _CashDispenserAccounting.SetParameterValue("@SubCompany", SubCompany);
                _CashDispenserAccounting.SetParameterValue("@Site", site);
                _CashDispenserAccounting.SetParameterValue("@Startdate", StartDate);
                _CashDispenserAccounting.SetParameterValue("@Enddate", EndDate);
                _CashDispenserAccounting.SetParameterValue("Version", sVersion);
                _CashDispenserAccounting.SetParameterValue("CurrencySymbol", sCurrencySymbol);
                _CashDispenserAccounting.SetParameterValue("CurrencyCulture", sCurrentCurrenyCulture);
                _CashDispenserAccounting.SetParameterValue("DateCulture", sCurrentCurrenyCulture);
                _CashDispenserAccounting.SetParameterValue("SiteName", siteName);
                _CashDispenserAccounting.SetParameterValue("CompanyName", strCompany);
                _CashDispenserAccounting.SetParameterValue("SubCompanyName", strSubcompany);
                _CashDispenserAccounting.SetParameterValue("@userID", userId);
                _CashDispenserAccounting.SetParameterValue("@SiteIDList", SiteIDList);

                crDatabase = _CashDispenserAccounting.Database;
                crTables = crDatabase.Tables;

                foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
                {
                    tableLogonInfo = crTable.LogOnInfo;
                    tableLogonInfo.ConnectionInfo = connectionInfo;
                    crTable.ApplyLogOnInfo(tableLogonInfo);
                }

                crystalReportViewer1.ReportSource = _CashDispenserAccounting;

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("ShowCashDispenserAccountingReport", LogManager.enumLogLevel.Info);
                ExceptionManager.Publish(ex);
            
            }
        
        }
		public void ShowCashDispenserCassettAccountingDetail(string ReportName, DataSet dsReportsDataSet, int Company, string StrCompany, int SubCompany, string StrSubCompany, int Site, string SiteName, string StartDate, string EndDate, int UserID, string SiteIDList)
         {
             try
             {
                 CrystalDecisions.CrystalReports.Engine.Database crDatabase;
                 CrystalDecisions.CrystalReports.Engine.Tables crTables;

                 TableLogOnInfo tableLogonInfo = new TableLogOnInfo();
                 ConnectionInfo connectionInfo = new ConnectionInfo();

                 connectionInfo.ServerName = DbConnection.ServerName;
                 connectionInfo.UserID = DbConnection.UserName;
                 connectionInfo.Password = DbConnection.Password;
                 connectionInfo.DatabaseName = DbConnection.DatabaseName;

                 CashDispenserCassetteAccounting vc_Acc = new CashDispenserCassetteAccounting();
                 dsReportsDataSet.Tables[0].TableName = "CashDispenserCassetteAcc";

                 vc_Acc.SetDataSource(dsReportsDataSet);
                 vc_Acc.SetParameterValue("@Company", Company);
                 vc_Acc.SetParameterValue("@SubCompany", SubCompany);                 
                 vc_Acc.SetParameterValue("@Site", Site);                 
                 vc_Acc.SetParameterValue("@UserID", UserID);
                 vc_Acc.SetParameterValue("@SiteIDList", SiteIDList);
                 vc_Acc.SetParameterValue("SiteName", SiteName);
                 vc_Acc.SetParameterValue("CompanyName", StrCompany);
                 vc_Acc.SetParameterValue("SubCompanyName", StrSubCompany);
                 vc_Acc.SetParameterValue("@Startdate", StartDate);
                 vc_Acc.SetParameterValue("@Enddate", EndDate);
                 vc_Acc.SetParameterValue("CurrencyCulture", sCurrentCurrenyCulture);
                 vc_Acc.SetParameterValue("CurrencySymbol", sCurrencySymbol);
                 vc_Acc.SetParameterValue("DateCulture", sCurrentCurrenyCulture);
                 vc_Acc.SetParameterValue("Version", sVersion);

                 crDatabase = vc_Acc.Database;
                 crTables = crDatabase.Tables;

                 foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
                 {
                     tableLogonInfo = crTable.LogOnInfo;
                     tableLogonInfo.ConnectionInfo = connectionInfo;
                     crTable.ApplyLogOnInfo(tableLogonInfo);
                 }

                 crystalReportViewer1.ReportSource = vc_Acc;
             }
             catch (Exception ex)
             {
                 LogManager.WriteLog("ShowCashDispCassetteAccountingReport", LogManager.enumLogLevel.Info);
                 ExceptionManager.Publish(ex);
             }

         }

        private void CrystalReportViewer_Load(object sender, EventArgs e)
        {
            this.ResolveResources();
        }        
    }

}
