using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.Common.LogManagement;
using BMC.Common.ExceptionManagement;
using BMC.Common.Utilities;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using BMC.EnterpriseBusiness.Business.CashierTransations;
using BMC.EnterpriseClient.Reports;
using BMC.EnterpriseDataAccess.CashierTransations;
using BMC.CoreLib.Win32;
using BMC.Common;

namespace BMC.EnterpriseClient.Views
{
    public partial class ReportViewer : Form
    {
        string sVersion = string.Empty;
        private string ServerName = string.Empty;
        private string UserName = string.Empty;
        private string Password = string.Empty;
        private string DatabaseName = string.Empty;
        BMCCashierTreasuryTransactions oBusiness = null;
        public ReportViewer()
        {
            InitializeComponent();
            oBusiness = new BMCCashierTreasuryTransactions();
            sVersion = oBusiness.GetBMCVersion();
        }

       //Get Currency Symbol based on Setting from Database
        private string GetReportsCurrency()
        {
            CashDeskManagerDataAccess dataaccess = new CashDeskManagerDataAccess();
            string strRegion = "";

            try
            {
                LogManager.WriteLog("Inside GetReportsCurrency()Method to get the Report Language", LogManager.enumLogLevel.Info);

                strRegion = dataaccess.GetSettingFromDB("BMC_Reports_Language");
                return new System.Globalization.RegionInfo(strRegion).CurrencySymbol;
                
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);                
                return strRegion = ExtensionMethods.CurrentSiteCulture.GetCurrencySymbol();
            }
        }

        //internal void showDetailedReport(DataSet dtDataset, DateTime FromDate, DateTime ToDate,string SiteName, string sFooterText)
        //{
        //    try
        //    {
        //        CDMDetailedView vDetailedViewReport = new CDMDetailedView();
        //        //
        //        vDetailedViewReport.SetDataSource(dtDataset);
        //        vDetailedViewReport.SetParameterValue("fromDate", FromDate);
        //        vDetailedViewReport.SetParameterValue("toDate", ToDate);
        //        vDetailedViewReport.SetParameterValue("FooterText", sFooterText);

        //        vDetailedViewReport.SetParameterValue("siteName", SiteName);
        //        vDetailedViewReport.SetParameterValue("BMCVersion", sVersion);
        //        vDetailedViewReport.SetParameterValue("CurrencyCulture", ExtensionMethods.CurrentCurrenyCulture);
        //        vDetailedViewReport.SetParameterValue("CurrencySymbol", ExtensionMethods.CurrentCurrenyCulture.GetCurrencySymbol());
        //        vDetailedViewReport.SetParameterValue("DateCulture", ExtensionMethods.CurrentDateCulture);
        //        crystalReportViewer1.ReportSource = vDetailedViewReport;
        //    }
        //    catch (Exception ex)
        //    {
        //        ExceptionManager.Publish(ex);
        //    }
        //}

        //internal void ShowCashDeskReconcilationReport(DataSet dsReportsDataSet, DateTime StartDate, DateTime EndDate, string SiteName, int RouteNo, int UserNo)
        //{




        //    //string sVersion = string.Empty;
        //    string sSiteName = string.Empty;

        //    try
        //    {
        //        LogManager.WriteLog("Inside ShowCashDeskReconcilationReport method", LogManager.enumLogLevel.Info);
        //        CashDeskReconicilationReport cashdeskListReport = new CashDeskReconicilationReport();
        //        cashdeskListReport.SetDataSource(dsReportsDataSet);
        //        LogManager.WriteLog("Report DataSource set successfully", LogManager.enumLogLevel.Info);
        //        LogManager.WriteLog("Setting Report Parameters...", LogManager.enumLogLevel.Info);
        //        cashdeskListReport.SetParameterValue("@startDate", StartDate);
        //        cashdeskListReport.SetParameterValue("@endDate", EndDate);
        //        cashdeskListReport.SetParameterValue("RouteNo",RouteNo);
        //        cashdeskListReport.SetParameterValue("UserNo", UserNo);
        //        cashdeskListReport.SetParameterValue("CurrencySymbol", ExtensionMethods.CurrentSiteCulture.GetCurrencySymbol());
        //        cashdeskListReport.SetParameterValue("SiteName", SiteName);
        //        cashdeskListReport.SetParameterValue("BMCVersion", sVersion);
        //        LogManager.WriteLog("Report Parameters set successfully", LogManager.enumLogLevel.Info);
        //        LogManager.WriteLog("Setting Report Source...", LogManager.enumLogLevel.Info);
        //        crystalReportViewer1.ReportSource = cashdeskListReport;
        //        LogManager.WriteLog("Report Source set successfully", LogManager.enumLogLevel.Info);
        //    }
        //    catch (Exception ex)
        //    {
        //        ExceptionManager.Publish(ex);
        //    }
        //}

        //internal void ShowSystemBalancingReport(DataSet SystemBalancingDetails, DateTime StartDate, DateTime EndDate, string SiteName, int RouteNo, int UserNo)
        //{
        //    try
        //    {
        //        LogManager.WriteLog("Inside ShowSystemBalancingReport method", LogManager.enumLogLevel.Info);
        //        SystemBalancing SystemBalancingReport = new SystemBalancing();
        //        LogManager.WriteLog("Setting Report Datasource...", LogManager.enumLogLevel.Info);
        //        SystemBalancingReport.SetDataSource(SystemBalancingDetails);
        //        LogManager.WriteLog("Report DataSource set successfully", LogManager.enumLogLevel.Info);
        //        LogManager.WriteLog("Setting Report Parameters...", LogManager.enumLogLevel.Info);
        //        SystemBalancingReport.SetParameterValue("@StartDate", StartDate);
        //        SystemBalancingReport.SetParameterValue("@EndDate", EndDate);
        //        //SystemBalancingReport.SetParameterValue("SiteName", SiteName);
        //        SystemBalancingReport.SetParameterValue("@RouteNo", RouteNo);
        //        SystemBalancingReport.SetParameterValue("@UserNo", UserNo);
        //        LogManager.WriteLog("Report Parameters set successfully", LogManager.enumLogLevel.Info);
        //        LogManager.WriteLog("Setting Report Source...", LogManager.enumLogLevel.Info);
        //        crystalReportViewer1.ReportSource = SystemBalancingReport;
        //        LogManager.WriteLog("Report Source set successfully", LogManager.enumLogLevel.Info);
        //    }
        //    catch (Exception ex)
        //    {
        //        ExceptionManager.Publish(ex);
        //    }
        //}

        //internal void ShowCashDeskMovementReport(DataSet dsReportsDataSet, DateTime StartDate, DateTime EndDate,string Site,int siteID,int RouteNo, int UserNo)
        //{
        //    try
        //    {
        //        LogManager.WriteLog("Inside ShowCashDeskMovementReport method", LogManager.enumLogLevel.Info);
        //        CashDeskMovement cashdeskMovementReport = new CashDeskMovement();
        //        LogManager.WriteLog("Setting Report Datasource...", LogManager.enumLogLevel.Info);
        //        cashdeskMovementReport.SetDataSource(dsReportsDataSet);
        //        LogManager.WriteLog("Report DataSource set successfully", LogManager.enumLogLevel.Info);
        //        LogManager.WriteLog("Setting Report Parameters...", LogManager.enumLogLevel.Info);
        //        Database crDatabase = null;
        //        Tables crTables = null;
        //        TableLogOnInfo tableLogonInfo = new TableLogOnInfo();
        //        ConnectionInfo connectionInfo = new ConnectionInfo();
        //        GetConnectionDetails();
        //        connectionInfo.ServerName = ServerName;
        //        connectionInfo.UserID = UserName;
        //        connectionInfo.Password = Password;
        //        connectionInfo.DatabaseName = DatabaseName;
        //        cashdeskMovementReport.SetParameterValue("@StartDate", StartDate);
        //        cashdeskMovementReport.SetParameterValue("@EndDate", EndDate);
        //        cashdeskMovementReport.SetParameterValue("@Site", siteID);
        //        cashdeskMovementReport.SetParameterValue("@Region", ExtensionMethods.CurrentSiteCulture);
        //        cashdeskMovementReport.SetParameterValue("@RouteNo", RouteNo);
        //        cashdeskMovementReport.SetParameterValue("@UserNo", UserNo);
        //        cashdeskMovementReport.SetParameterValue("@CurrencySymbol", GetReportsCurrency());
        //        crDatabase = cashdeskMovementReport.Database;
        //        crTables = crDatabase.Tables;
        //        foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
        //        {
        //            tableLogonInfo = crTable.LogOnInfo;
        //            tableLogonInfo.ConnectionInfo = connectionInfo;
        //            crTable.ApplyLogOnInfo(tableLogonInfo);
        //        }
        //        LogManager.WriteLog("Report Parameters set successfully", LogManager.enumLogLevel.Info);
        //        LogManager.WriteLog("Setting Report Source...", LogManager.enumLogLevel.Info);
        //        crystalReportViewer1.ReportSource = cashdeskMovementReport;
        //        LogManager.WriteLog("Report Source set successfully", LogManager.enumLogLevel.Info);
        //    }
        //    catch (Exception ex)
        //    {
        //        ExceptionManager.Publish(ex);
        //    }
        //}


        private void GetConnectionDetails()
        {
            try
            {
                string strConnect = DatabaseHelper.GetConnectionString();
                if (strConnect != string.Empty)
                {
                    try
                    {
                        List<string> databaseInfo = strConnect.Split(';').ToList();

                        ServerName = databaseInfo[0].Split('=')[1].ToString();
                        UserName = databaseInfo[2].Split('=')[1].ToString();
                        Password = databaseInfo[3].Split('=')[1].ToString();
                        DatabaseName = databaseInfo[1].Split('=')[1].ToString();

                        BMC.Common.LogManagement.LogManager.WriteLog("Database details retrieved successfully",
                            BMC.Common.LogManagement.LogManager.enumLogLevel.Info);
                    }
                    catch (Exception ex)
                    {
                        LogManager.WriteLog("Error in retrieving the database details", LogManager.enumLogLevel.Error);
                        ExceptionManager.Publish(ex);
                    }
                }
                else
                {
                    LogManager.WriteLog("Error in retrieving the database details", LogManager.enumLogLevel.Error);
                }
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        //internal void ShowCashDeskMovementUSReport(DataSet dsReportsDataSet, DateTime StartDate, DateTime EndDate, string Site, int SiteID, string Region, int RouteNo, int UserNo)
        //{
        //    try
        //    {
        //        Database crDatabase = null;
        //        Tables crTables = null;
        //        TableLogOnInfo tableLogonInfo = new TableLogOnInfo();
        //        ConnectionInfo connectionInfo = new ConnectionInfo();

        //        GetConnectionDetails();
        //        connectionInfo.ServerName = ServerName;
        //        connectionInfo.UserID = UserName;
        //        connectionInfo.Password = Password;
        //        connectionInfo.DatabaseName = DatabaseName;
        //        LogManager.WriteLog("Inside ShowCashDeskMovementUSReport method", LogManager.enumLogLevel.Info);
        //        CashDeskMovementUSReport cashdeskMovementUSReport = new CashDeskMovementUSReport();
        //        LogManager.WriteLog("Setting Report Datasource...", LogManager.enumLogLevel.Info);
        //        cashdeskMovementUSReport.SetDataSource(dsReportsDataSet);
        //        LogManager.WriteLog("Report DataSource set successfully", LogManager.enumLogLevel.Info);
        //        LogManager.WriteLog("Setting Report Parameters...", LogManager.enumLogLevel.Info);
        //        cashdeskMovementUSReport.SetParameterValue("@StartDate", StartDate);
        //        cashdeskMovementUSReport.SetParameterValue("@EndDate", EndDate);
        //        cashdeskMovementUSReport.SetParameterValue("Version", sVersion);
        //        cashdeskMovementUSReport.SetParameterValue("siteName", Site);
        //        cashdeskMovementUSReport.SetParameterValue("Region", ExtensionMethods.CurrentSiteCulture);
        //        cashdeskMovementUSReport.SetParameterValue("@CurrencySymbol", GetReportsCurrency());

        //        cashdeskMovementUSReport.SetParameterValue("@Site", SiteID); 
        //        cashdeskMovementUSReport.SetParameterValue("@RegionName", Region);
        //        cashdeskMovementUSReport.SetParameterValue("@RouteNo", RouteNo);
        //        cashdeskMovementUSReport.SetParameterValue("@UserNo", UserNo);
        //        crDatabase = cashdeskMovementUSReport.Database;
        //        crTables = crDatabase.Tables;
        //        foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
        //        {
        //            tableLogonInfo = crTable.LogOnInfo;
        //            tableLogonInfo.ConnectionInfo = connectionInfo;
        //            crTable.ApplyLogOnInfo(tableLogonInfo);
        //        }
        //        LogManager.WriteLog("Report Parameters set successfully", LogManager.enumLogLevel.Info);
        //        LogManager.WriteLog("Setting Report Source...", LogManager.enumLogLevel.Info);
        //        crystalReportViewer1.ReportSource = cashdeskMovementUSReport;
        //        LogManager.WriteLog("Report Source set successfully", LogManager.enumLogLevel.Info);
        //    }
        //    catch (Exception ex)
        //    {
        //        ExceptionManager.Publish(ex);
        //    }
        //}

        internal void showActiveReport(DataSet _DtDetails, DateTime _FromDate, DateTime _ToDate, string _SiteName,BMC.EnterpriseDataAccess.CashierTransations.ReportOptions eReport,string sPath)
        {
            try
            {
                ActiveReport vActiveReport = new ActiveReport();
                vActiveReport.SetDataSource(_DtDetails);
                vActiveReport.SetParameterValue("fromDate", _FromDate);
                vActiveReport.SetParameterValue("toDate", _ToDate);
                vActiveReport.SetParameterValue("siteName", _SiteName);
                vActiveReport.SetParameterValue("BMCVersion", sVersion);
                vActiveReport.SetParameterValue("CurrencyCulture", ExtensionMethods.CurrentCurrenyCulture);
                vActiveReport.SetParameterValue("CurrencySymbol", ExtensionMethods.CurrentCurrenyCulture.GetCurrencySymbol());
                vActiveReport.SetParameterValue("DateCulture", ExtensionMethods.CurrentDateCulture);
                crystalReportViewer1.ReportSource = vActiveReport;
                if (eReport == BMC.EnterpriseDataAccess.CashierTransations.ReportOptions.Export)
                {
                    vActiveReport.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.ExcelRecord, sPath);
                   // MessageBox.Show("File Saved Successfully.", "Bally MultiConnect - Enterprise", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.ShowInfoMessageBox(this.GetResourceTextByKey(1,"MSG_FILE_SAVE_SUCCESS"));
                }
                else if (eReport == BMC.EnterpriseDataAccess.CashierTransations.ReportOptions.Print)
                {
                    vActiveReport.PrintToPrinter(1, false, 0, 0);
                    //MessageBox.Show("Sent to Printer Successfully.", "Bally MultiConnect - Enterprise", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_SENT_TO_PRINTER"));
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }



        //internal void showVOIDReport(string _CODE, DataSet _DtDetails, DateTime _FromDate, DateTime _ToDate, string _SiteName, BMC.EnterpriseDataAccess.CashierTransations.ReportOptions eReport, string sPath)
        //{

        //    try
        //    {
        //        VoidCancelReport vVoidCancelReport = new VoidCancelReport();
        //        //
        //        vVoidCancelReport.SetDataSource(_DtDetails);
        //        vVoidCancelReport.SetParameterValue("fromDate", _FromDate);
        //        vVoidCancelReport.SetParameterValue("toDate", _ToDate);
        //        vVoidCancelReport.SetParameterValue("ReportName", _CODE);

        //        vVoidCancelReport.SetParameterValue("siteName", _SiteName);
        //        vVoidCancelReport.SetParameterValue("BMCVersion", sVersion);
        //        vVoidCancelReport.SetParameterValue("CurrencyCulture", ExtensionMethods.CurrentCurrenyCulture);
        //        vVoidCancelReport.SetParameterValue("CurrencySymbol", ExtensionMethods.CurrentCurrenyCulture.GetCurrencySymbol());
        //        vVoidCancelReport.SetParameterValue("DateCulture", ExtensionMethods.CurrentDateCulture);
        //        crystalReportViewer1.ReportSource = vVoidCancelReport;

        //        if (eReport == BMC.EnterpriseDataAccess.CashierTransations.ReportOptions.Export)
        //        {
        //            vVoidCancelReport.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.ExcelRecord, sPath);
        //            //MessageBox.Show("File Saved Successfully.", "Bally MultiConnect - Enterprise", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_FILE_SAVE_SUCCESS"));
        //        }
        //        else if (eReport == BMC.EnterpriseDataAccess.CashierTransations.ReportOptions.Print)
        //        {
        //            vVoidCancelReport.PrintToPrinter(1, false, 0, 0);
        //            //MessageBox.Show("Sent to Printer Successfully.", "Bally MultiConnect - Enterprise", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_SENT_TO_PRINTER"));
        //        }
                
        //    }
        //    catch (Exception ex)
        //    {
        //        ExceptionManager.Publish(ex);
        //    }
        //}

        //internal void showPROMOReport(DataSet _DtDetails, DateTime _FromDate, DateTime _ToDate, string _SiteName, BMC.EnterpriseDataAccess.CashierTransations.ReportOptions eReport, string sPath)
        //{
        //    try
        //    {
        //        PromoReport vPromoReport = new PromoReport();
        //        //
        //        vPromoReport.SetDataSource(_DtDetails);
        //        vPromoReport.SetParameterValue("fromDate", _FromDate);
        //        vPromoReport.SetParameterValue("toDate", _ToDate);
                

        //        vPromoReport.SetParameterValue("siteName", _SiteName);
        //        vPromoReport.SetParameterValue("BMCVersion", sVersion);
        //        vPromoReport.SetParameterValue("CurrencyCulture", ExtensionMethods.CurrentCurrenyCulture);
        //        vPromoReport.SetParameterValue("CurrencySymbol", ExtensionMethods.CurrentCurrenyCulture.GetCurrencySymbol());
        //        vPromoReport.SetParameterValue("DateCulture", ExtensionMethods.CurrentDateCulture);
        //        crystalReportViewer1.ReportSource = vPromoReport;

        //        if (eReport == BMC.EnterpriseDataAccess.CashierTransations.ReportOptions.Export)
        //        {
        //            vPromoReport.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.ExcelRecord, sPath);
        //            //MessageBox.Show("File Saved Successfully.", "Bally MultiConnect - Enterprise", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_FILE_SAVE_SUCCESS"));
        //        }
        //        else if (eReport == BMC.EnterpriseDataAccess.CashierTransations.ReportOptions.Print)
        //        {
        //            vPromoReport.PrintToPrinter(1, false, 0, 0);
        //           // MessageBox.Show("Sent to Printer Successfully.", "Bally MultiConnect - Enterprise", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_SENT_TO_PRINTER"));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ExceptionManager.Publish(ex);
        //    }
        //}

        //internal void showEXCEPReport(DataSet _DtDetails, DateTime _FromDate, DateTime _ToDate, string _SiteName, BMC.EnterpriseDataAccess.CashierTransations.ReportOptions eReport, string sPath)
        //{
        //    try
        //    {
        //        ExceptionReport vExceptionReport = new ExceptionReport();
        //        //
        //        vExceptionReport.SetDataSource(_DtDetails);
        //        vExceptionReport.SetParameterValue("fromDate", _FromDate);
        //        vExceptionReport.SetParameterValue("toDate", _ToDate);

        //        vExceptionReport.SetParameterValue("siteName", _SiteName);
        //        vExceptionReport.SetParameterValue("BMCVersion", sVersion);
        //        vExceptionReport.SetParameterValue("CurrencyCulture", ExtensionMethods.CurrentCurrenyCulture);
        //        vExceptionReport.SetParameterValue("CurrencySymbol", ExtensionMethods.CurrentCurrenyCulture.GetCurrencySymbol());
        //        vExceptionReport.SetParameterValue("DateCulture", ExtensionMethods.CurrentDateCulture);
        //        crystalReportViewer1.ReportSource = vExceptionReport;

        //        if (eReport == BMC.EnterpriseDataAccess.CashierTransations.ReportOptions.Export)
        //        {
        //            vExceptionReport.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.ExcelRecord, sPath);
        //            //MessageBox.Show("File Saved Successfully.", "Bally MultiConnect - Enterprise", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_FILE_SAVE_SUCCESS"));
        //        }
        //        else if (eReport == BMC.EnterpriseDataAccess.CashierTransations.ReportOptions.Print)
        //        {
        //            vExceptionReport.PrintToPrinter(1, false, 0, 0);
        //            //MessageBox.Show("Sent to Printer Successfully.", "Bally MultiConnect - Enterprise", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_SENT_TO_PRINTER"));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ExceptionManager.Publish(ex);
        //    }
        //}

        //internal void showLIABILITYReport(DataSet _DtDetails, DateTime _FromDate, DateTime _ToDate, string _SiteName, BMC.EnterpriseDataAccess.CashierTransations.ReportOptions eReport, string sPath)
        //{
        //    try
        //    {
        //        LiabilityReport vLiabilityReport = new LiabilityReport();
        //        //
        //        vLiabilityReport.SetDataSource(_DtDetails);
        //        vLiabilityReport.SetParameterValue("fromDate", _FromDate);
        //        vLiabilityReport.SetParameterValue("toDate", _ToDate);

        //        vLiabilityReport.SetParameterValue("siteName", _SiteName);
        //        vLiabilityReport.SetParameterValue("BMCVersion", sVersion);
        //        vLiabilityReport.SetParameterValue("CurrencyCulture", ExtensionMethods.CurrentCurrenyCulture);
        //        vLiabilityReport.SetParameterValue("CurrencySymbol", ExtensionMethods.CurrentCurrenyCulture.GetCurrencySymbol());
        //        vLiabilityReport.SetParameterValue("DateCulture", ExtensionMethods.CurrentDateCulture);
        //        crystalReportViewer1.ReportSource = vLiabilityReport;

        //        if (eReport == BMC.EnterpriseDataAccess.CashierTransations.ReportOptions.Export)
        //        {
        //            vLiabilityReport.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.ExcelRecord, sPath);
        //            //MessageBox.Show("File Saved Successfully.", "Bally MultiConnect - Enterprise", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_FILE_SAVE_SUCCESS"));
        //        }
        //        else if (eReport == BMC.EnterpriseDataAccess.CashierTransations.ReportOptions.Print)
        //        {
        //            vLiabilityReport.PrintToPrinter(1, false, 0, 0);
        //            //MessageBox.Show("Sent to Printer Successfully.", "Bally MultiConnect - Enterprise", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_SENT_TO_PRINTER"));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ExceptionManager.Publish(ex);
        //    }
        //}

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // ReportViewer
            // 
            this.ClientSize = new System.Drawing.Size(643, 273);
            this.Name = "ReportViewer";
            this.ResumeLayout(false);

        }
    }
}
