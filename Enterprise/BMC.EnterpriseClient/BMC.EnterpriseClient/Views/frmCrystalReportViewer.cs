using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using BMC.Common.Utilities;
using BMC.Common.ExceptionManagement;
using BMC.Common.ConfigurationManagement;
using BMC.Common.LogManagement;
using BMC.EnterpriseBusiness.Business;
using System.Globalization;
using BMC.Common;
using BMC.ReportViewer;
using BMC.Reports;
using BMC.EnterpriseBusiness.Entities;



namespace BMC.EnterpriseClient.Views
{
    public partial class frmCrystalReportViewer : Form
    {
        private string CurrentCurrencySymbol = ExtensionMethods.CurrentSiteCulture.GetCurrencySymbol();
        private string ServerName = string.Empty;
        private string UserName = string.Empty;
        private string Password = string.Empty;
        private string DatabaseName = string.Empty;
        string sVersion = string.Empty;
        string CurrencySymbol = string.Empty;
        AssetReportBiz oAsset = new AssetReportBiz();

        public frmCrystalReportViewer()
        {
            InitializeComponent();
            SetTagProperty();
        }

        public frmCrystalReportViewer(List<string> lstParams)
        {
            InitializeComponent();
            InvokeMethods(lstParams);
            SetTagProperty();
        }

        public void SetTagProperty()
        {
            this.Tag = "Key_BMCReports";
            this.ResolveResources();
        }

        private void InvokeMethods(List<string> lstParams)
        {
            switch (lstParams[0])
            {
                case "LiquidationSummaryReport":
                    ShowLiquidationSummaryReport(Convert.ToInt32(lstParams[1]), null);
                    break;

            }
        }

        public void ShowLiquidationSummaryReport(int? iBatchId, int? iReadId)
        {
            /*sVersion = oAsset.GetBMCVersion();
            try
            {
                Database crDatabase = null;
                Tables crTables = null;

                TableLogOnInfo tableLogonInfo = new TableLogOnInfo();
                ConnectionInfo connectionInfo = new ConnectionInfo();

                GetConnectionDetails();
                connectionInfo.ServerName = ServerName;
                connectionInfo.UserID = UserName;
                connectionInfo.Password = Password;
                connectionInfo.DatabaseName = DatabaseName;

                CurrencySymbol = GetCurrencySymbol();
                var rptLiquidation = new LiquidationSummary_PS();
                rptLiquidation.SetParameterValue("@BatchId", iBatchId);
                rptLiquidation.SetParameterValue("@ReadId", iReadId);
                rptLiquidation.SetParameterValue("@CurrencySymbol", CurrencySymbol);
                rptLiquidation.SetParameterValue("@BatchId", iBatchId, "Liquidation_Summary_PS_Sub.rpt");
                rptLiquidation.SetParameterValue("@ReadId", iReadId, "Liquidation_Summary_PS_Sub.rpt");
                rptLiquidation.SetParameterValue("@CurrencySymbol", ExtensionMethods.CurrentCurrenyCulture.GetCurrencySymbol(), "Liquidation_Summary_PS_Sub.rpt");
                rptLiquidation.SetParameterValue("DateCulture", Common.Utilities.ExtensionMethods.CurrentDateCulture);
                rptLiquidation.SetParameterValue("BMCVersion", sVersion);


                crDatabase = rptLiquidation.Database;
                crTables = crDatabase.Tables;

                foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
                {
                    tableLogonInfo = crTable.LogOnInfo;
                    tableLogonInfo.ConnectionInfo = connectionInfo;
                    crTable.ApplyLogOnInfo(tableLogonInfo);
                }

                this.objCrystalReportViewer.ReportSource = rptLiquidation;
                this.objCrystalReportViewer.Show();
                
                
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            */

            try
            {
                clsSPParams spParams = new clsSPParams();
                spParams.BatchId = iBatchId;
                spParams.ReadId = iReadId;
                BMC.ReportViewer.RDLReportViewer.Instance.LoadReport("rsp_Report_GetLiquidationDetail", "Liquidation", "ENT_LiquidationSummary_PS", spParams, false);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

        }

        public void ShowBatchDropExceptionReportUK(int iBatchId, int iSiteID)
        {
            sVersion = oAsset.GetBMCVersion();
            try
            {
                //    Database crDatabase = null;
                //    Tables crTables = null;

                //    TableLogOnInfo tableLogonInfo = new TableLogOnInfo();
                //    ConnectionInfo connectionInfo = new ConnectionInfo();

                //    GetConnectionDetails();
                //    connectionInfo.ServerName = ServerName;
                //    connectionInfo.UserID = UserName;
                //    connectionInfo.Password = Password;
                //    connectionInfo.DatabaseName = DatabaseName;

                //    CurrencySymbol = GetCurrencySymbol();
                //    var rptBatchDropException = new BatchDropExceptionReportUK();
                //    rptBatchDropException.SetParameterValue("@BatchID", iBatchId);
                //    rptBatchDropException.SetParameterValue("@SiteID", iSiteID);
                //    rptBatchDropException.SetParameterValue("BMCVersion", sVersion);
                //    rptBatchDropException.SetParameterValue("CurrencySymbol", CurrencySymbol);
                //    rptBatchDropException.SetParameterValue("CurrentCulture", Common.Utilities.ExtensionMethods.CurrentSiteCulture);
                //    rptBatchDropException.SetParameterValue("DateCulture", Common.Utilities.ExtensionMethods.CurrentDateCulture);
                //    crDatabase = rptBatchDropException.Database;
                //    crTables = crDatabase.Tables;

                //    foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
                //    {
                //        tableLogonInfo = crTable.LogOnInfo;
                //        tableLogonInfo.ConnectionInfo = connectionInfo;
                //        crTable.ApplyLogOnInfo(tableLogonInfo);
                //    }

                //    this.objCrystalReportViewer.ReportSource = rptBatchDropException;
                //    this.objCrystalReportViewer.Show();

                // TODO - Do Report in rdl

            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        public void ShowBatchDropExceptionReport(int iBatchId, int iSiteID,string SiteName)
        {
            /*sVersion = oAsset.GetBMCVersion();
            try
            {
                Database crDatabase = null;
                Tables crTables = null;

                TableLogOnInfo tableLogonInfo = new TableLogOnInfo();
                ConnectionInfo connectionInfo = new ConnectionInfo();

                GetConnectionDetails();
                connectionInfo.ServerName = ServerName;
                connectionInfo.UserID = UserName;
                connectionInfo.Password = Password;
                connectionInfo.DatabaseName = DatabaseName;

                CurrencySymbol = GetCurrencySymbol();
                var rptBatchDropException = new BatchDropExceptionReport();
                rptBatchDropException.SetParameterValue("@BatchID", iBatchId);
                rptBatchDropException.SetParameterValue("@SiteID", iSiteID);
                rptBatchDropException.SetParameterValue("BMCVersion", sVersion);
                rptBatchDropException.SetParameterValue("CurrencySymbol", CurrencySymbol);
                rptBatchDropException.SetParameterValue("CurrentCulture", Common.Utilities.ExtensionMethods.CurrentSiteCulture);
                rptBatchDropException.SetParameterValue("DateCulture", Common.Utilities.ExtensionMethods.CurrentDateCulture);
                crDatabase = rptBatchDropException.Database;
                crTables = crDatabase.Tables;

                foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
                {
                    tableLogonInfo = crTable.LogOnInfo;
                    tableLogonInfo.ConnectionInfo = connectionInfo;
                    crTable.ApplyLogOnInfo(tableLogonInfo);
                }

                this.objCrystalReportViewer.ReportSource = rptBatchDropException;
                this.objCrystalReportViewer.Show();

            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            */

            try
            {
                clsSPParams spParams = new clsSPParams();
                spParams.BatchId = iBatchId;
                spParams.SiteID = iSiteID;
                spParams.SiteName = SiteName;
                BMC.ReportViewer.RDLReportViewer.Instance.LoadReport("rsp_REPORT_BatchDropException", "Batch Drop Exception Report", "ENT_BatchDropExceptionReport", spParams, false);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

        }
        public void ShowWeekDropExceptionReport(int SiteId, int WeekId,string SiteName,string WeekNo,string Week)
        {
            /*sVersion = oAsset.GetBMCVersion();
            try
            {
                Database crDatabase = null;
                Tables crTables = null;

                TableLogOnInfo tableLogonInfo = new TableLogOnInfo();
                ConnectionInfo connectionInfo = new ConnectionInfo();

                GetConnectionDetails();
                connectionInfo.ServerName = ServerName;
                connectionInfo.UserID = UserName;
                connectionInfo.Password = Password;
                connectionInfo.DatabaseName = DatabaseName;

                CurrencySymbol = GetCurrencySymbol();
                var rptVSWeeklyDropExceptionReport = new VSWeeklyDropExceptionReport();
                rptVSWeeklyDropExceptionReport.SetParameterValue("@siteId", SiteId);
                rptVSWeeklyDropExceptionReport.SetParameterValue("@WeekId", WeekId);
                rptVSWeeklyDropExceptionReport.SetParameterValue("BMCVersion", sVersion);
                rptVSWeeklyDropExceptionReport.SetParameterValue("CurrencySymbol", CurrencySymbol);
                rptVSWeeklyDropExceptionReport.SetParameterValue("CurrentCulture", Common.Utilities.ExtensionMethods.CurrentSiteCulture);
                rptVSWeeklyDropExceptionReport.SetParameterValue("DateCulture", Common.Utilities.ExtensionMethods.CurrentDateCulture);
                crDatabase = rptVSWeeklyDropExceptionReport.Database;
                crTables = crDatabase.Tables;

                foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
                {
                    tableLogonInfo = crTable.LogOnInfo;
                    tableLogonInfo.ConnectionInfo = connectionInfo;
                    crTable.ApplyLogOnInfo(tableLogonInfo);
                }

                this.objCrystalReportViewer.ReportSource = rptVSWeeklyDropExceptionReport;
                this.objCrystalReportViewer.Show();

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            */

            try
            {
                clsSPParams spParams = new clsSPParams();
                spParams.WeekId = WeekId;
                spParams.SiteID = SiteId;
                spParams.SiteName = SiteName;
                spParams.WeekNo = WeekNo;
                spParams.Week = Week;
                BMC.ReportViewer.RDLReportViewer.Instance.LoadReport("rsp_Report_WeeklyDropException", "Weekly Drop Exception Report", "ENT_VSWeeklyDropExceptionReport", spParams, false);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        public void ShowBatchWinLossReport(int iBatchId, int iSiteID,string SiteName, bool isWeekID)
        {
            /*sVersion = oAsset.GetBMCVersion();
            try
            {
                Database crDatabase = null;
                Tables crTables = null;

                TableLogOnInfo tableLogonInfo = new TableLogOnInfo();
                ConnectionInfo connectionInfo = new ConnectionInfo();

                GetConnectionDetails();
                connectionInfo.ServerName = ServerName;
                connectionInfo.UserID = UserName;
                connectionInfo.Password = Password;
                connectionInfo.DatabaseName = DatabaseName;

                CurrencySymbol = GetCurrencySymbol();
                var rptBatchWinLoss = new BatchWinLossReport();
                rptBatchWinLoss.SetParameterValue("@BatchNo", iBatchId);
                rptBatchWinLoss.SetParameterValue("@SiteID", iSiteID);
                rptBatchWinLoss.SetParameterValue("@IsWeek", isWeekID);
                rptBatchWinLoss.SetParameterValue("BMCVersion", sVersion);
                rptBatchWinLoss.SetParameterValue("CurrencySymbol", CurrencySymbol);
                rptBatchWinLoss.SetParameterValue("CurrentCulture", Common.Utilities.ExtensionMethods.CurrentSiteCulture);
                rptBatchWinLoss.SetParameterValue("DateCulture", Common.Utilities.ExtensionMethods.CurrentDateCulture);


                crDatabase = rptBatchWinLoss.Database;
                crTables = crDatabase.Tables;

                foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
                {
                    tableLogonInfo = crTable.LogOnInfo;
                    tableLogonInfo.ConnectionInfo = connectionInfo;
                    crTable.ApplyLogOnInfo(tableLogonInfo);
                }

                this.objCrystalReportViewer.ReportSource = rptBatchWinLoss;
                this.objCrystalReportViewer.Show();

            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            */

            try
            {
                clsSPParams spParams = new clsSPParams();
                spParams.BatchNo = iBatchId;
                spParams.IsWeek = isWeekID;
                spParams.SiteID = iSiteID;
                spParams.SiteName = SiteName;
                spParams.CurrencyCulture = Common.Utilities.ExtensionMethods.CurrentSiteCulture;
                BMC.ReportViewer.RDLReportViewer.Instance.LoadReport("rsp_Report_BatchWinLoss", "Batch Win Loss Report", "ENT_BatchWinLossReport", spParams, false);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        public void ShowBatchWinLossReportForUK(int iBatchId, int iSiteID, bool isWeekID)
        {
            sVersion = oAsset.GetBMCVersion();
            try
            {
                //Database crDatabase = null;
                //Tables crTables = null;

                //TableLogOnInfo tableLogonInfo = new TableLogOnInfo();
                //ConnectionInfo connectionInfo = new ConnectionInfo();

                //GetConnectionDetails();
                //connectionInfo.ServerName = ServerName;
                //connectionInfo.UserID = UserName;
                //connectionInfo.Password = Password;
                //connectionInfo.DatabaseName = DatabaseName;

                //CurrencySymbol = GetCurrencySymbol();
                //var rptBatchWinLoss = new BatchWinLossReportUK();
                //rptBatchWinLoss.SetParameterValue("@BatchNo", iBatchId);
                //rptBatchWinLoss.SetParameterValue("@SiteID", iSiteID);
                //rptBatchWinLoss.SetParameterValue("@IsWeek", isWeekID);
                //rptBatchWinLoss.SetParameterValue("BMCVersion", sVersion);
                //rptBatchWinLoss.SetParameterValue("CurrencySymbol", CurrencySymbol);
                //rptBatchWinLoss.SetParameterValue("CurrentCulture", Common.Utilities.ExtensionMethods.CurrentSiteCulture);
                //rptBatchWinLoss.SetParameterValue("DateCulture", Common.Utilities.ExtensionMethods.CurrentDateCulture);


                //crDatabase = rptBatchWinLoss.Database;
                //crTables = crDatabase.Tables;

                //foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
                //{
                //    tableLogonInfo = crTable.LogOnInfo;
                //    tableLogonInfo.ConnectionInfo = connectionInfo;
                //    crTable.ApplyLogOnInfo(tableLogonInfo);
                //}

                //this.objCrystalReportViewer.ReportSource = rptBatchWinLoss;
                //this.objCrystalReportViewer.Show();

                // TODO - Do Report in rdl
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        public void ShowCollectionReport(int iBatchId, int iSiteID, bool isWeekID)
        {
            sVersion = oAsset.GetBMCVersion();
            try
            {
                //Database crDatabase = null;
                //Tables crTables = null;

                //TableLogOnInfo tableLogonInfo = new TableLogOnInfo();
                //ConnectionInfo connectionInfo = new ConnectionInfo();

                //GetConnectionDetails();
                //connectionInfo.ServerName = ServerName;
                //connectionInfo.UserID = UserName;
                //connectionInfo.Password = Password;
                //connectionInfo.DatabaseName = DatabaseName;

                //CurrencySymbol = GetCurrencySymbol();
                //var rptBatchWinLoss = new CollectionReport();
                //rptBatchWinLoss.SetParameterValue("@BatchNo", iBatchId);
                //rptBatchWinLoss.SetParameterValue("@SiteID", iSiteID);
                //rptBatchWinLoss.SetParameterValue("@IsWeek", isWeekID);
                //rptBatchWinLoss.SetParameterValue("BMCVersion", sVersion);
                //rptBatchWinLoss.SetParameterValue("CurrencySymbol", CurrencySymbol);
                //rptBatchWinLoss.SetParameterValue("CurrentCulture", Common.Utilities.ExtensionMethods.CurrentSiteCulture);
                //rptBatchWinLoss.SetParameterValue("DateCulture", Common.Utilities.ExtensionMethods.CurrentDateCulture);


                //crDatabase = rptBatchWinLoss.Database;
                //crTables = crDatabase.Tables;

                //foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
                //{
                //    tableLogonInfo = crTable.LogOnInfo;
                //    tableLogonInfo.ConnectionInfo = connectionInfo;
                //    crTable.ApplyLogOnInfo(tableLogonInfo);
                //}

                //this.objCrystalReportViewer.ReportSource = rptBatchWinLoss;
                //this.objCrystalReportViewer.Show();

                // TODO in Report in rdl
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }


        //private string GetCurrencySymbol()
        //{
        //    string sCurrentCulture = AdminBusiness.GetSetting("BMC_Reports_Language", "en-US");
        //    return new RegionInfo(sCurrentCulture).CurrencySymbol;
        //}

        /// <summary>
        /// Load the asset detailed history report in viewer.
        /// </summary>
        /// <param name="BarPosition"></param>
        /// <param name="IsDetailed"></param>
        /// <param name="sVersion"></param>
        public void ShowAssetDetailedHistoryReport(int BarPosition, bool IsDetailed, string sVersion, int iSiteID, String SiteName)
        {
            /*try
            {
                Database crDatabase = null;
                Tables crTables = null;

                TableLogOnInfo tableLogonInfo = new TableLogOnInfo();
                ConnectionInfo connectionInfo = new ConnectionInfo();

                //Set the DB Credentials for report.
                GetConnectionDetails();
                connectionInfo.ServerName = ServerName;
                connectionInfo.UserID = UserName;
                connectionInfo.Password = Password;
                connectionInfo.DatabaseName = DatabaseName;
                CurrencySymbol = GetCurrencySymbol();
                //Set parameter values 
                AssetDetailedReport rptAssetDetailed = new AssetDetailedReport();
                rptAssetDetailed.SetParameterValue("@Bar_Position_ID", BarPosition);
                rptAssetDetailed.SetParameterValue("@IsDetailed", IsDetailed);
                rptAssetDetailed.SetParameterValue("BMCVersion", sVersion);
                rptAssetDetailed.SetParameterValue("Version", sVersion);
                rptAssetDetailed.SetParameterValue("CurrencySymbol", CurrencySymbol);
                rptAssetDetailed.SetParameterValue("CurrentCulture", Common.Utilities.ExtensionMethods.CurrentSiteCulture);
                rptAssetDetailed.SetParameterValue("DateCulture", Common.Utilities.ExtensionMethods.CurrentDateCulture);
                rptAssetDetailed.SetParameterValue("@SiteID", iSiteID);
                rptAssetDetailed.SetParameterValue("@SiteName", SiteName);
                crDatabase = rptAssetDetailed.Database;
                crTables = crDatabase.Tables;

                //set the login information for each secion in the report.
                foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
                {
                    tableLogonInfo = crTable.LogOnInfo;
                    tableLogonInfo.ConnectionInfo = connectionInfo;
                    crTable.ApplyLogOnInfo(tableLogonInfo);
                }

                //Assign the report source
                this.objCrystalReportViewer.ReportSource = rptAssetDetailed;
                this.objCrystalReportViewer.Show();
            }

            catch (Exception ex)
            {
                LogManager.WriteLog("Asset Detailed History report could not be loaded....", LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
            }
            */

            try
            {
                clsSPParams spParams = new clsSPParams();
                spParams.BarPositionId = BarPosition;
                spParams.IsDetailed = IsDetailed;
                spParams.SiteID = iSiteID;
                spParams.SiteName = SiteName;
                BMC.ReportViewer.RDLReportViewer.Instance.LoadReport("rsp_GetDetailedHistory", "Asset Detailed Report", "ENT_AssetDetailedReport", spParams, false);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

        }

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
        /// <summary>
        /// load the asset report in viewer
        /// </summary>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="SiteID"></param>
        /// <param name="CategoryID"></param>
        /// <param name="sVersion"></param>
        internal void ShowAssetReport(string StartDate, string EndDate, int SiteID, int CategoryID, String Category, string sVersion, string sSiteName)
        {
            /*try
            {
                Database crDatabase = null;
                Tables crTables = null;

                TableLogOnInfo tableLogonInfo = new TableLogOnInfo();
                ConnectionInfo connectionInfo = new ConnectionInfo();
                CurrencySymbol = GetCurrencySymbol();
                //Set the DB Credentials for report.
                GetConnectionDetails();
                connectionInfo.ServerName = ServerName;
                connectionInfo.UserID = UserName;
                connectionInfo.Password = Password;
                connectionInfo.DatabaseName = DatabaseName;

                if (CategoryID == -1)
                {
                    CategoryID = 0;
                }
                //Set parameter values 
                AssetReport rptAsset = new AssetReport();
                rptAsset.SetParameterValue("@StartDate", StartDate);
                rptAsset.SetParameterValue("@EndDate", EndDate);
                rptAsset.SetParameterValue("@SiteID", SiteID);
                rptAsset.SetParameterValue("@Category", CategoryID);
                rptAsset.SetParameterValue("@CategoryName", Category);
                rptAsset.SetParameterValue("BMCVersion", sVersion);
                rptAsset.SetParameterValue("Version", sVersion);
                rptAsset.SetParameterValue("CurrencySymbol", CurrencySymbol);
                rptAsset.SetParameterValue("CurrentCulture", Common.Utilities.ExtensionMethods.CurrentSiteCulture);
                rptAsset.SetParameterValue("DateCulture", Common.Utilities.ExtensionMethods.CurrentDateCulture);
                rptAsset.SetParameterValue("SiteName", sSiteName);
                //rptAsset.SetParameterValue("CategoryName", CategoryName);

                crDatabase = rptAsset.Database;
                crTables = crDatabase.Tables;

                //set the login information for each secion in the report.
                foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
                {
                    tableLogonInfo = crTable.LogOnInfo;
                    tableLogonInfo.ConnectionInfo = connectionInfo;
                    crTable.ApplyLogOnInfo(tableLogonInfo);
                }

                //Assign the report source
                this.objCrystalReportViewer.ReportSource = rptAsset;
                this.objCrystalReportViewer.Show();
            }

            catch (Exception ex)
            {
                LogManager.WriteLog("Asset Report could not be loaded....", LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
            }
            */

            try
            {
                clsSPParams spParams = new clsSPParams();

                if (CategoryID == -1)
                {
                    CategoryID = 0;
                }

                spParams.StartDate  = StartDate;
                spParams.EndDate = EndDate;
                spParams.SiteID = SiteID;
                spParams.Category = CategoryID;
                spParams.CategoryName = Category;
                spParams.SiteName = sSiteName;
                BMC.ReportViewer.RDLReportViewer.Instance.LoadReport("rsp_AssetReport", "Asset Report", "ENT_AssetReport", spParams, false);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        public void ShowVSAssetGameReport(int BarPosition_ID, DateTime startDate, DateTime endDate, string sBarPostion, string strCompany, string strSubCompany, string strSiteName, string sVersion, int Site_ID)
        {
            /*try
            {

                Database crDatabase = null;
                Tables crTables = null;

                TableLogOnInfo tableLogonInfo = new TableLogOnInfo();
                ConnectionInfo connectionInfo = new ConnectionInfo();

                GetConnectionDetails();
                connectionInfo.ServerName = ServerName;
                connectionInfo.UserID = UserName;
                connectionInfo.Password = Password;
                connectionInfo.DatabaseName = DatabaseName;


                var rptVSAssetGame = new VSAssetGameReport();
                rptVSAssetGame.SetParameterValue("@BarPosition_ID", BarPosition_ID);
                rptVSAssetGame.SetParameterValue("@startdate", startDate);
                rptVSAssetGame.SetParameterValue("@enddate", endDate);
                rptVSAssetGame.SetParameterValue("BMCVersion", sVersion);
                rptVSAssetGame.SetParameterValue("BarPosition", sBarPostion);
                rptVSAssetGame.SetParameterValue("CompanyName", strCompany);
                rptVSAssetGame.SetParameterValue("SubCompany", strSubCompany);
                rptVSAssetGame.SetParameterValue("SiteName", strSiteName);
                rptVSAssetGame.SetParameterValue("@Site_ID", Site_ID);

                crDatabase = rptVSAssetGame.Database;
                crTables = crDatabase.Tables;

                foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
                {
                    tableLogonInfo = crTable.LogOnInfo;
                    tableLogonInfo.ConnectionInfo = connectionInfo;
                    crTable.ApplyLogOnInfo(tableLogonInfo);
                }

                this.objCrystalReportViewer.ReportSource = rptVSAssetGame;
                this.objCrystalReportViewer.Show();

            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            */
            try
            {
                clsSPParams spParams = new clsSPParams();
                spParams.Bar_Position_Id = BarPosition_ID;
                spParams.StartDate = startDate.ToString();
                spParams.EndDate = endDate.ToString();
                spParams.BarPositionName = sBarPostion;
                spParams.Site_ID = Site_ID;
                spParams.CompanyName = strCompany;
                spParams.SubCompanyName = strSubCompany;
                spParams.SiteName = strSiteName;
                spParams.BarPositionName = sBarPostion;
                spParams.ReportFilterDateFormat = SettingsEntity.ReportDateTimeFormat;
                spParams.ReportDataDateAloneFormat = SettingsEntity.ReportDataDateAloneFormat;
                spParams.ReportDataDateNTimeFormat = SettingsEntity.ReportDataDateNTimeFormat;
                spParams.ReportPrintDateTimeFormat = SettingsEntity.ReportPrintDateTimeFormat;
                BMC.ReportViewer.RDLReportViewer.Instance.LoadReport("rsp_AssetGameReport", "Asset Game Report", "ENT_VSAssetGameReport", spParams, false);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        //Ep4 Changes
        public void ShowUndeclaredVaultDropReport(string RegionName, int Site_id, string SiteCode, string SiteName, int VaultID)
        {
            /*try
            {
                sVersion = oAsset.GetBMCVersion();

                Database crDatabase = null;
                Tables crTables = null;

                TableLogOnInfo tableLogonInfo = new TableLogOnInfo();
                ConnectionInfo connectionInfo = new ConnectionInfo();

                GetConnectionDetails();
                connectionInfo.ServerName = ServerName;
                connectionInfo.UserID = UserName;
                connectionInfo.Password = Password;
                connectionInfo.DatabaseName = DatabaseName;

                CurrencySymbol = GetCurrencySymbol();
                var UndecVaultdrp = new UndeclaredVaultDrops();
                UndecVaultdrp.SetParameterValue("@Vault_ID", VaultID);
                UndecVaultdrp.SetParameterValue("@Site_ID", Site_id);
                UndecVaultdrp.SetParameterValue("SiteName", SiteName);
                UndecVaultdrp.SetParameterValue("SiteCode", SiteCode);
                UndecVaultdrp.SetParameterValue("RegionName", RegionName);
                UndecVaultdrp.SetParameterValue("CurrencyCulture", Common.Utilities.ExtensionMethods.CurrentSiteCulture);
                UndecVaultdrp.SetParameterValue("CurrencySymbol", CurrencySymbol);
                UndecVaultdrp.SetParameterValue("DateCulture", Common.Utilities.ExtensionMethods.CurrentDateCulture);
                UndecVaultdrp.SetParameterValue("Version", sVersion);

                crDatabase = UndecVaultdrp.Database;
                crTables = crDatabase.Tables;

                foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
                {
                    tableLogonInfo = crTable.LogOnInfo;
                    tableLogonInfo.ConnectionInfo = connectionInfo;
                    crTable.ApplyLogOnInfo(tableLogonInfo);
                }
                this.objCrystalReportViewer.ReportSource = UndecVaultdrp;
                this.objCrystalReportViewer.Show();

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            */

            try
            {
                clsSPParams spParams = new clsSPParams();
                spParams.Vault_Id = VaultID;
                spParams.Site_ID = Site_id;
                spParams.SiteCode = SiteCode;
                spParams.SiteName = SiteName;
                spParams.RegionName = RegionName;
                BMC.ReportViewer.RDLReportViewer.Instance.LoadReport("rsp_Vault_GetUndeclaredDrops", "Undeclared Vault Drops", "ENT_UndeclaredVaultDrops", spParams, false);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

        }
        public void ShowUndeclaredVaultCassettes(string RegionName, int Site_id, string SiteCode, string SiteName, int VaultID)
        {
            /*try
            {
                sVersion = oAsset.GetBMCVersion();

                Database crDatabase = null;
                Tables crTables = null;

                TableLogOnInfo tableLogonInfo = new TableLogOnInfo();
                ConnectionInfo connectionInfo = new ConnectionInfo();

                GetConnectionDetails();
                connectionInfo.ServerName = ServerName;
                connectionInfo.UserID = UserName;
                connectionInfo.Password = Password;
                connectionInfo.DatabaseName = DatabaseName;

                CurrencySymbol = GetCurrencySymbol();

                var UndecVaultCassettesdrp = new UndeclaredVaultCassettes();
                UndecVaultCassettesdrp.SetParameterValue("@Vault_ID", VaultID);
                UndecVaultCassettesdrp.SetParameterValue("@Site_ID", Site_id);
                UndecVaultCassettesdrp.SetParameterValue("SiteName", SiteName);
                UndecVaultCassettesdrp.SetParameterValue("SiteCode", SiteCode);
                UndecVaultCassettesdrp.SetParameterValue("RegionName", RegionName);
                UndecVaultCassettesdrp.SetParameterValue("CurrencyCulture", Common.Utilities.ExtensionMethods.CurrentSiteCulture);
                UndecVaultCassettesdrp.SetParameterValue("CurrencySymbol", CurrencySymbol);
                UndecVaultCassettesdrp.SetParameterValue("DateCulture", Common.Utilities.ExtensionMethods.CurrentDateCulture);
                UndecVaultCassettesdrp.SetParameterValue("Version", sVersion);

                crDatabase = UndecVaultCassettesdrp.Database;
                crTables = crDatabase.Tables;

                foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
                {
                    tableLogonInfo = crTable.LogOnInfo;
                    tableLogonInfo.ConnectionInfo = connectionInfo;
                    crTable.ApplyLogOnInfo(tableLogonInfo);
                }
                this.objCrystalReportViewer.ReportSource = UndecVaultCassettesdrp;
                this.objCrystalReportViewer.Show();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            */
            try
            {
                clsSPParams spParams = new clsSPParams();
                spParams.VaultId = VaultID;
                spParams.Site_ID = Site_id;
                spParams.RegionName = RegionName;
                spParams.SiteCode = SiteCode;
                spParams.SiteName = SiteName;
                spParams.ReportFilterDateFormat = SettingsEntity.ReportDateTimeFormat;
                spParams.ReportDataDateAloneFormat = SettingsEntity.ReportDataDateAloneFormat;
                spParams.ReportDataDateNTimeFormat = SettingsEntity.ReportDataDateNTimeFormat;
                spParams.ReportPrintDateTimeFormat = SettingsEntity.ReportPrintDateTimeFormat;
                BMC.ReportViewer.RDLReportViewer.Instance.LoadReport("rsp_Report_GetUndeclaredVaultCassettes", "Undeclared Vault Cassettes", "ENT_UndeclaredVaultCassettes", spParams, false);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        public void ShowVaultDropHistoryReport(int Site_id, string SiteCode, string SiteName, int VaultID, int VarianceType, DateTime StartDate, DateTime EndDate, string Variance)
        {
            /*try
            {
                sVersion = oAsset.GetBMCVersion();

                Database crDatabase = null;
                Tables crTables = null;

                TableLogOnInfo tableLogonInfo = new TableLogOnInfo();
                ConnectionInfo connectionInfo = new ConnectionInfo();

                GetConnectionDetails();
                connectionInfo.ServerName = ServerName;
                connectionInfo.UserID = UserName;
                connectionInfo.Password = Password;
                connectionInfo.DatabaseName = DatabaseName;

                CurrencySymbol = GetCurrencySymbol();
                var VaultDropHist = new DeclaredVaultDropHistory();
                VaultDropHist.SetParameterValue("@Vault_ID", VaultID);
                VaultDropHist.SetParameterValue("@Site_ID", Site_id);
                VaultDropHist.SetParameterValue("@StartDate", StartDate);
                VaultDropHist.SetParameterValue("@EndDate", EndDate);
                VaultDropHist.SetParameterValue("@VarianceType", VarianceType);
                VaultDropHist.SetParameterValue("SiteName", SiteName);
                VaultDropHist.SetParameterValue("SiteCode", SiteCode);
                VaultDropHist.SetParameterValue("Variance", Variance);
                VaultDropHist.SetParameterValue("Version", sVersion);
                VaultDropHist.SetParameterValue("CurrencyCulture", Common.Utilities.ExtensionMethods.CurrentSiteCulture);
                VaultDropHist.SetParameterValue("CurrencySymbol", CurrencySymbol);
                VaultDropHist.SetParameterValue("DateCulture", Common.Utilities.ExtensionMethods.CurrentDateCulture);

                crDatabase = VaultDropHist.Database;
                crTables = crDatabase.Tables;

                foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
                {
                    tableLogonInfo = crTable.LogOnInfo;
                    tableLogonInfo.ConnectionInfo = connectionInfo;
                    crTable.ApplyLogOnInfo(tableLogonInfo);
                }
                this.objCrystalReportViewer.ReportSource = VaultDropHist;
                this.objCrystalReportViewer.Show();

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            */
            try
            {
                clsSPParams spParams = new clsSPParams();
                spParams.VaultId = VaultID;
                spParams.Site_ID = Site_id;
                spParams.StartDate = StartDate.ToString();
                spParams.EndDate = EndDate.ToString();
                spParams.VarianceName = Variance;
                spParams.SiteCode = SiteCode;
                spParams.SiteName = SiteName;
                spParams.VarianceType = VarianceType;
                spParams.ReportFilterDateFormat = SettingsEntity.ReportDateTimeFormat;
                spParams.ReportDataDateAloneFormat = SettingsEntity.ReportDataDateAloneFormat;
                spParams.ReportDataDateNTimeFormat = SettingsEntity.ReportDataDateNTimeFormat;
                spParams.ReportPrintDateTimeFormat = SettingsEntity.ReportPrintDateTimeFormat;
                BMC.ReportViewer.RDLReportViewer.Instance.LoadReport("rsp_Vault_GetDeclaredDrops", "Declared Vault Drop History", "ENT_DeclaredVaultDropHistory", spParams, false);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        public void ShowVaultDropCassetteDetailsReport(int Site_id, string SiteCode, string SiteName, int VaultID, int VarianceType, DateTime StartDate, DateTime EndDate, string Variance)
        {
            /*try
            {
                sVersion = oAsset.GetBMCVersion();

                Database crDatabase = null;
                Tables crTables = null;

                TableLogOnInfo tableLogonInfo = new TableLogOnInfo();
                ConnectionInfo connectionInfo = new ConnectionInfo();

                GetConnectionDetails();
                connectionInfo.ServerName = ServerName;
                connectionInfo.UserID = UserName;
                connectionInfo.Password = Password;
                connectionInfo.DatabaseName = DatabaseName;

                CurrencySymbol = GetCurrencySymbol();
                var VaultDropCass = new DeclaredVaultDropsCassetteDetails();
                VaultDropCass.SetParameterValue("@Vault_ID", VaultID);
                VaultDropCass.SetParameterValue("@Site_ID", Site_id);
                VaultDropCass.SetParameterValue("@StartDate", StartDate);
                VaultDropCass.SetParameterValue("@EndDate", EndDate);
                VaultDropCass.SetParameterValue("@VarianceType", VarianceType);
                VaultDropCass.SetParameterValue("SiteName", SiteName);
                VaultDropCass.SetParameterValue("SiteCode", SiteCode);
                VaultDropCass.SetParameterValue("Variance", Variance);
                VaultDropCass.SetParameterValue("Version", sVersion);
                VaultDropCass.SetParameterValue("CurrencyCulture", Common.Utilities.ExtensionMethods.CurrentSiteCulture);
                VaultDropCass.SetParameterValue("CurrencySymbol", CurrencySymbol);
                VaultDropCass.SetParameterValue("DateCulture", Common.Utilities.ExtensionMethods.CurrentDateCulture);

                crDatabase = VaultDropCass.Database;
                crTables = crDatabase.Tables;

                foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
                {
                    tableLogonInfo = crTable.LogOnInfo;
                    tableLogonInfo.ConnectionInfo = connectionInfo;
                    crTable.ApplyLogOnInfo(tableLogonInfo);
                }
                this.objCrystalReportViewer.ReportSource = VaultDropCass;
                this.objCrystalReportViewer.Show();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            */
            try
            {
                clsSPParams spParams = new clsSPParams();
                spParams.VaultId = VaultID;
                spParams.Site_ID = Site_id;
                spParams.StartDate = StartDate.ToString();
                spParams.EndDate = EndDate.ToString();
                spParams.Variance = VarianceType;
                spParams.SiteCode = SiteCode;
                spParams.SiteName = SiteName;
                spParams.VarianceType = VarianceType;
                spParams.VarianceName = Variance;
                BMC.ReportViewer.RDLReportViewer.Instance.LoadReport("rsp_Report_GetDeclaredVaultCassettes", "Declared Vault Drops Cassette Details", "ENT_DeclaredVaultDropsCassetteDetails", spParams, false);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

        }


        internal void showServiceCurrentCalls(int? _iCallStatusID, int? _iCallGroupID, string _sDepotIDList, string _sStaffIDList, string _sSiteIDList,
                                        int? _iSubCompanyID, int? _iJobID, string sCallStatus, string sCallGroup, string sDepotList, string sStaffList,
                                        string sSiteList, string sSubCompany, string sJobID)
        {
            try
            {
                sVersion = oAsset.GetBMCVersion();

                //Database crDatabase = null;
                //Tables crTables = null;

                //TableLogOnInfo tableLogonInfo = new TableLogOnInfo();
                //ConnectionInfo connectionInfo = new ConnectionInfo();

                //GetConnectionDetails();
                //connectionInfo.ServerName = ServerName;
                //connectionInfo.UserID = UserName;
                //connectionInfo.Password = Password;
                //connectionInfo.DatabaseName = DatabaseName;

                //CurrencySymbol = GetCurrencySymbol();
                //var rptServiceCurrentCalls = new ServiceCurrentCalls();
                //rptServiceCurrentCalls.SetParameterValue("@CallStatusID", _iCallStatusID);
                //rptServiceCurrentCalls.SetParameterValue("@CallGroupID", _iCallGroupID);
                //rptServiceCurrentCalls.SetParameterValue("@DepotIDList", _sDepotIDList);
                //rptServiceCurrentCalls.SetParameterValue("@StaffIDList", _sStaffIDList);
                //rptServiceCurrentCalls.SetParameterValue("@SiteIDList", _sSiteIDList);
                //rptServiceCurrentCalls.SetParameterValue("@SubCompanyID", _iSubCompanyID);
                //rptServiceCurrentCalls.SetParameterValue("@JobID", _iJobID);
                //rptServiceCurrentCalls.SetParameterValue("sCallStatus", sCallStatus);
                //rptServiceCurrentCalls.SetParameterValue("sCallGroup", sCallGroup);
                //rptServiceCurrentCalls.SetParameterValue("sDepotList", string.IsNullOrEmpty(sDepotList) ? "--ANY--" : sDepotList);
                //rptServiceCurrentCalls.SetParameterValue("sStaffList", string.IsNullOrEmpty(sStaffList) ? "--ANY--" : sStaffList);
                //rptServiceCurrentCalls.SetParameterValue("sSiteList", string.IsNullOrEmpty(sSiteList) ? "All Sites" : sSiteList);
                //rptServiceCurrentCalls.SetParameterValue("sSubCompany", sSubCompany);
                //rptServiceCurrentCalls.SetParameterValue("sJobID", string.IsNullOrEmpty(sJobID) ? "--ANY--" : sJobID);
                //rptServiceCurrentCalls.SetParameterValue("Version", sVersion);
                //rptServiceCurrentCalls.SetParameterValue("CurrentCulture", Common.Utilities.ExtensionMethods.CurrentSiteCulture);
                //rptServiceCurrentCalls.SetParameterValue("DateCulture", Common.Utilities.ExtensionMethods.CurrentDateCulture);

                //crDatabase = rptServiceCurrentCalls.Database;
                //crTables = crDatabase.Tables;

                //foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
                //{
                //    tableLogonInfo = crTable.LogOnInfo;
                //    tableLogonInfo.ConnectionInfo = connectionInfo;
                //    crTable.ApplyLogOnInfo(tableLogonInfo);
                //}
                //this.objCrystalReportViewer.ReportSource = rptServiceCurrentCalls;
                //this.objCrystalReportViewer.Show();


            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        internal void showServiceClosedCalls(DateTime _dtStartDate, DateTime _dtEndDate, int? _iCallRemedyID, string _sDepotIDList, string _sStaffIDList, string _sSiteIDList, int? _iSubCompanyID, int? _iJobID, string sCallRemedy, string sDepotList, string sStaffList, string sSiteList, string sSubCompany, string sJobID, string _sAssetNo)
        {
            try
            {
                sVersion = oAsset.GetBMCVersion();

                //Database crDatabase = null;
                //Tables crTables = null;

                //TableLogOnInfo tableLogonInfo = new TableLogOnInfo();
                //ConnectionInfo connectionInfo = new ConnectionInfo();

                //GetConnectionDetails();
                //connectionInfo.ServerName = ServerName;
                //connectionInfo.UserID = UserName;
                //connectionInfo.Password = Password;
                //connectionInfo.DatabaseName = DatabaseName;

                //CurrencySymbol = GetCurrencySymbol();
                //var rptServiceClosedCalls = new ServiceClosedCalls();
                //rptServiceClosedCalls.SetParameterValue("@StartDate", _dtStartDate);
                //rptServiceClosedCalls.SetParameterValue("@EndDate", _dtEndDate);
                //rptServiceClosedCalls.SetParameterValue("@CallRemedyID", _iCallRemedyID);
                //rptServiceClosedCalls.SetParameterValue("@DepotIDList", _sDepotIDList);
                //rptServiceClosedCalls.SetParameterValue("@StaffIDList", _sStaffIDList);
                //rptServiceClosedCalls.SetParameterValue("@SiteIDList", _sSiteIDList);
                //rptServiceClosedCalls.SetParameterValue("@SubCompanyID", _iSubCompanyID);
                //rptServiceClosedCalls.SetParameterValue("@JobID", _iJobID);
                //rptServiceClosedCalls.SetParameterValue("@MachineStockNo", _sAssetNo);
                //rptServiceClosedCalls.SetParameterValue("sCallRemedy", sCallRemedy);
                //rptServiceClosedCalls.SetParameterValue("sDepotList", string.IsNullOrEmpty(sDepotList) ? "--ANY--" : sDepotList);
                //rptServiceClosedCalls.SetParameterValue("sStaffList", string.IsNullOrEmpty(sStaffList) ? "--ANY--" : sStaffList);
                //rptServiceClosedCalls.SetParameterValue("sSiteList", string.IsNullOrEmpty(sSiteList) ? "All Sites" : sSiteList);
                //rptServiceClosedCalls.SetParameterValue("sSubCompany", sSubCompany);
                //rptServiceClosedCalls.SetParameterValue("sJobID", string.IsNullOrEmpty(sJobID) ? "--ANY--" : sJobID);
                //rptServiceClosedCalls.SetParameterValue("sAssetNo", string.IsNullOrEmpty(_sAssetNo) ? "--ANY--" : _sAssetNo);
                //rptServiceClosedCalls.SetParameterValue("Version", sVersion);
                //rptServiceClosedCalls.SetParameterValue("CurrentCulture", Common.Utilities.ExtensionMethods.CurrentSiteCulture);
                //rptServiceClosedCalls.SetParameterValue("DateCulture", Common.Utilities.ExtensionMethods.CurrentDateCulture);

                //crDatabase = rptServiceClosedCalls.Database;
                //crTables = crDatabase.Tables;

                //foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
                //{
                //    tableLogonInfo = crTable.LogOnInfo;
                //    tableLogonInfo.ConnectionInfo = connectionInfo;
                //    crTable.ApplyLogOnInfo(tableLogonInfo);
                //}
                //this.objCrystalReportViewer.ReportSource = rptServiceClosedCalls;
                //this.objCrystalReportViewer.Show();

                // TODO - Do Report in rdl
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

    }
}
