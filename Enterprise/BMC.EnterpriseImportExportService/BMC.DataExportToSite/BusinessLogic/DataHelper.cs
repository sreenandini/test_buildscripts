using System;
using System.Data;
using System.Data.SqlClient;
using BMC.BusinessClasses.Proxy;
using BMC.Common;
using BMC.Common.ExceptionManagement;
using BMC.Common.ConfigurationManagement;
using BMC.Common.LogManagement;
using BMC.DataAccess;
using System.Threading;
using System.Xml;
using BMC.Security;

namespace BMC.DataExportToSite.BusinessLogic
{
    public class DataHelper
    {
        //private Proxy _proxy;
        private const string SITE_LICENSING_KEY = "B411y51T";
        public DataHelper()
        {
            this.ProxyTimeoutInMilliseconds = 100000;
        }

        internal int ProxyTimeoutInMilliseconds { get; set; }

        public string GetVersion(String strSiteCode)
        {
            var dataset = new DataSet();

            try
            {
                dataset = SqlHelper.ExecuteDataset(GetConnectionString(),
                                                   CommandType.Text,
                                                   "Select WebURL From Site Where Site_Code = " + strSiteCode);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            LogManager.WriteLog("Count, WebURL: " + dataset.Tables.Count + ", " + dataset.Tables[0].Rows[0][0].ToString(), LogManager.enumLogLevel.Error);
            return dataset.Tables.Count > 0 ? dataset.Tables[0].Rows[0][0].ToString() : string.Empty;
        }

        public bool ResetInProgressEhRecords()
        {
            bool bSuccess;
            try
            {
                SqlHelper.ExecuteScalar(GetConnectionString(), CommandType.StoredProcedure,
                                        Constants.CONSTANT_RSP_RESETINPROGRESSEHRECORDSINENTERPRISE);
                bSuccess = true;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                bSuccess = false;
            }

            return bSuccess;
        }

        public DataSet GetAllExportData(string sitecode)
        {
            DataSet objdsAllExportData = null;
            try
            {
                objdsAllExportData = SqlHelper.ExecuteDataset(GetConnectionString(), Constants.CONSTANT_USP_ALLEXPORTDATA, DataBaseServiceHandler.AddParameter<string>("@SiteCode", DbType.String, sitecode));
                return objdsAllExportData;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return objdsAllExportData;
            }
        }

        public DataSet GetAllExportDataForSite(String strSideCode)
        {
            var dataset = new DataSet();

            var oSiteParam = new SqlParameter[1];
            var oParam = new SqlParameter
            {
                ParameterName = "SITE_LIST",
                Value = strSideCode,
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                Size = 8000
            };
            oSiteParam[0] = oParam;
            try
            {
                dataset = SqlHelper.ExecuteDataset(GetConnectionString(),
                                                   CommandType.StoredProcedure,
                                                   Constants.CONSTANT_USP_ALLEXPORTDATAFORSITES,
                                                   oSiteParam);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            return dataset;
        }

        public void GetImportCalendar(string siteID, int ehID, string siteCode, string calendarType)
        {

            try
            {
                var oExportDetails = new SqlParameter[2];
                var oParam = new SqlParameter
                {
                    ParameterName = "Site_ID",
                    Value = siteID,
                    Direction = ParameterDirection.Input
                };
                oExportDetails[0] = oParam;

                var oparam2 = new SqlParameter
                {
                    ParameterName = "Calendar_Type",
                    Value = calendarType,
                    Direction = ParameterDirection.Input
                };
                oExportDetails[1] = oparam2;

                var strXMLExportCalendarData = (SqlHelper.ExecuteScalar(GetConnectionString(),
                                                                           CommandType.StoredProcedure,
                                                                           Constants.CONSTANT_USP_EXPORTCALENDAR,
                                                                           oExportDetails)).ToString();

                bool isCallSuccess = InvokeBgswsAdminWs(strXMLExportCalendarData, ehID.ToString(),
                                                         Constants.CONSTANT_WEBMETHOD_IMPORTCALENDAR,
                                                         siteCode);

                if ((isCallSuccess == false))
                    UpdateExportHistoryTableWithStatus(ehID, "-1");
                else
                    UpdateExportHistoryTableWithStatus(ehID, "100");
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                UpdateExportHistoryTableWithStatus(ehID, "-1");
            }
        }

        public int UpdateExportHistoryTableWithStatus(int ehID, string ehStatus)
        {
            int iNoOfRowsAffected;


            try
            {
                var oParams = new SqlParameter[2];

                var oParam = new SqlParameter
                {
                    ParameterName = "EH_ID",
                    Value = ehID,
                    Direction = ParameterDirection.Input
                };
                oParams[0] = oParam;

                oParam = new SqlParameter
                {
                    ParameterName = "EH_Status",
                    Value = ehStatus,
                    Direction = ParameterDirection.Input
                };
                oParams[1] = oParam;

                iNoOfRowsAffected = SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure,
                                                              Constants.CONSTANT_USP_UPDATEEXPORTHISTORY, oParams);

                if ((ehStatus == "NULL") || (ehStatus == "X"))
                {
                    LogManager.WriteLog("The export for EH ID" + ehID + " has failed.", LogManager.enumLogLevel.Info);
                }
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                iNoOfRowsAffected = -1;
            }
            return iNoOfRowsAffected;
        }

        public void GetImportModel(string modelID, int ehID, string siteCode)
        {
            try
            {
                AutoResetEvent _Reset = new AutoResetEvent(false);
                int PerItemProcessInterval;
                try
                {
                    //Wait befor processing next data(To avoid CPU load) 
                    PerItemProcessInterval = Convert.ToInt32(ConfigManager.Read("PerItemProcessMilliseconds"));
                }
                catch
                {
                    PerItemProcessInterval = 100;
                }

                var sqlParameters = new SqlParameter[1];
                var sqlParameter = new SqlParameter
                {
                    ParameterName = "ID",
                    Value = modelID,
                    Direction = ParameterDirection.Input
                };
                sqlParameters[0] = sqlParameter;
                //var xmlExportModelData = (SqlHelper.ExecuteScalar(GetConnectionString(),
                //                                                     CommandType.StoredProcedure,
                //                                                     Constants.CONSTANT_RSP_EXPORTMODEL, sqlParameters)).
                //    ToString();

                //var isCallSuccess = InvokeBgswsAdminWs(xmlExportModelData,ehID.ToString(), Constants.CONSTANT_WEBMETHOD_IMPORTMODEL, siteCode);
                DataSet oDs = SqlHelper.ExecuteDataset(GetConnectionString(), CommandType.StoredProcedure, Constants.CONSTANT_RSP_EXPORTMODEL, sqlParameters);
                Boolean isCallSuccess = false;
                foreach (DataRow oDr in oDs.Tables[0].Rows)
                {
                    LogManager.WriteLog("Machine Model:Start Export", LogManager.enumLogLevel.Debug);
                    isCallSuccess = InvokeBgswsAdminWs(oDr[0].ToString(), ehID.ToString(), Constants.CONSTANT_WEBMETHOD_IMPORTMODEL, siteCode);
                    if (isCallSuccess == false)
                    {
                        LogManager.WriteLog("Machine Model:Export Failure", LogManager.enumLogLevel.Debug);
                        break;
                    }
                    LogManager.WriteLog("Machine Model Exported", LogManager.enumLogLevel.Debug);
                    _Reset.WaitOne(PerItemProcessInterval);
                }

                if (isCallSuccess == false)
                    UpdateExportHistoryTableWithStatus(ehID, "-1");
                else
                    UpdateExportHistoryTableWithStatus(ehID, "100");
            }
            catch (Exception ex)
            {
                UpdateExportHistoryTableWithStatus(ehID, "-1");
                ExceptionManager.Publish(ex);
            }
        }

        public void GetImportSite(String siteID, int ehID, string siteCode)
        {
            try
            {

                var oExportDetails = new SqlParameter[1];
                var oParam = new SqlParameter
                {
                    ParameterName = "Site_ID",
                    Value = siteID,
                    Direction = ParameterDirection.Input
                };
                oExportDetails[0] = oParam;

                var strXMLExportSiteData = (SqlHelper.ExecuteScalar(GetConnectionString(),
                                                                       CommandType.StoredProcedure,
                                                                       Constants.CONSTANT_USP_EXPORTSITE,
                                                                       oExportDetails)).ToString();

                bool isCallSuccess = InvokeBgswsAdminWs(strXMLExportSiteData, ehID.ToString(), Constants.CONSTANT_WEBMETHOD_IMPORTSITE, siteCode);

                if ((isCallSuccess == false))
                    UpdateExportHistoryTableWithStatus(ehID, "-1");
                else
                    UpdateExportHistoryTableWithStatus(ehID, "100");
            }

            catch (Exception ex)
            {
                UpdateExportHistoryTableWithStatus(ehID, "-1");
                ExceptionManager.Publish(ex);
            }
        }

        public void GetImportCrossTicketing(int ehID, string siteCode)
        {
            try
            {
                LogManager.WriteLog("Cross Ticketing:Start Export", LogManager.enumLogLevel.Debug);
                var oExportDetails = new SqlParameter[1];
                var oParam = new SqlParameter
                {
                    ParameterName = "Site_Code",
                    Value = siteCode,
                    Direction = ParameterDirection.Input
                };
                oExportDetails[0] = oParam;

                var strXMLExportCrossTicketingData = (SqlHelper.ExecuteScalar(GetConnectionString(),
                                                                       CommandType.StoredProcedure,
                                                                       Constants.CONSTANT_RSP_EXPORTCROSSTICKETING,
                                                                       oExportDetails)).ToString();

                bool isCallSuccess = InvokeBgswsAdminWs(strXMLExportCrossTicketingData, ehID.ToString(), Constants.CONSTANT_WEBMETHOD_IMPORTCROSSTICKETING, siteCode);
                if ((isCallSuccess == false))
                {
                    LogManager.WriteLog("Cross Ticketing:Export Failure", LogManager.enumLogLevel.Debug);
                    UpdateExportHistoryTableWithStatus(ehID, "-1");
                }
                else
                {
                    LogManager.WriteLog("Cross Ticketing Exported", LogManager.enumLogLevel.Debug);
                    UpdateExportHistoryTableWithStatus(ehID, "100");
                }
            }

            catch (Exception ex)
            {
                UpdateExportHistoryTableWithStatus(ehID, "-1");
                ExceptionManager.Publish(ex);
            }
        }

        public void ExportMachineNoteAcceptorEnableDisableStatus(int ehID, bool shouldEnable)
        {
            string query = "SELECT bar_position_name + ','+ EH_SITE_Code + ',' + WebURL FROM Bar_Position  INNER JOIN Export_History ON Bar_POSITION_ID = EH_Reference1 AND EH_ID = " +
                               ehID + " INNER JOIN SITE ON SITE_Code = EH_Site_Code ";

            string barPostionDetails = SqlHelper.ExecuteScalar(GetConnectionString(), CommandType.Text, query).ToString();

            if (String.IsNullOrEmpty(barPostionDetails))
            {
                LogManager.WriteLog("No Bar Position Returned for the the EH_ID: " + ehID, LogManager.enumLogLevel.Info);
                UpdateExportHistoryTableWithStatus(ehID, "-1");
                return;
            }

            if (barPostionDetails.Split(',').Length != 3)
            {
                LogManager.WriteLog("Invalid Data for the Argument SQL statement: " + query,
                                    LogManager.enumLogLevel.Info);
                UpdateExportHistoryTableWithStatus(ehID, "-1");
                return;
            }

            string customXML = "<MACHINENOTEACCEPTOR><BARPOSITION>";
            customXML += barPostionDetails.Split(',')[0];
            customXML += "</BARPOSITION><ACTION>";
            customXML += shouldEnable ? "ENABLE" : "DISABLE";
            customXML += "</ACTION></MACHINENOTEACCEPTOR>";
            if (WrapExportXMLData(customXML, ehID.ToString(), "MACHINENOTEACCEPTOR", barPostionDetails.Split(',')[2], barPostionDetails.Split(',')[1]))
                UpdateExportHistoryTableWithStatus(ehID, "100");
            else
                UpdateExportHistoryTableWithStatus(ehID, "-1");

        }

        public void ExportMachineEnableStatus(int ehID, bool shouldEnable)
        {
            string query = "SELECT bar_position_name + ','+ EH_SITE_Code + ',' + WebURL FROM Bar_Position  INNER JOIN Export_History ON Bar_POSITION_ID = EH_Reference1 AND EH_ID = " +
                               ehID + " INNER JOIN SITE ON SITE_Code = EH_Site_Code ";

            string barPostionDetails = SqlHelper.ExecuteScalar(GetConnectionString(), CommandType.Text, query).ToString();

            if (String.IsNullOrEmpty(barPostionDetails))
            {
                LogManager.WriteLog("No Bar Position Returned for the the EH_ID: " + ehID, LogManager.enumLogLevel.Info);
                UpdateExportHistoryTableWithStatus(ehID, "-1");
                return;
            }

            if (barPostionDetails.Split(',').Length != 3)
            {
                LogManager.WriteLog("Invalid Data for the Argument SQL statement: " + query,
                                    LogManager.enumLogLevel.Info);
                UpdateExportHistoryTableWithStatus(ehID, "-1");
                return;
            }

            string customXML = "<MACHINEENABLEDISABLE><BARPOSITION>";
            customXML += barPostionDetails.Split(',')[0];
            customXML += "</BARPOSITION><ACTION>";
            customXML += shouldEnable ? "ENABLE" : "DISABLE";
            customXML += "</ACTION></MACHINEENABLEDISABLE>";
            if (WrapExportXMLData(customXML, ehID.ToString(), "MachineEnableDisable",
                                     barPostionDetails.Split(',')[2], barPostionDetails.Split(',')[1]))
            {
                UpdateExportHistoryTableWithStatus(ehID, "100");
                DataBaseServiceHandler.ExecuteNonQuery(GetConnectionString(), CommandType.Text,
                    "Update tBP Set tBP.Bar_Position_Machine_Enabled = '" + (shouldEnable ? "1" : "0") + "' FROM Bar_Position tBP INNER JOIN Export_History tEH ON tEH.EH_Reference1 = tBP.Bar_Position_ID AND tEH.EH_ID = " + ehID);
            }
            else
                UpdateExportHistoryTableWithStatus(ehID, "-1");
        }

        public void ExportStackerDetails(int StackerID, int ehID, string SiteCode)
        {
            var sqlParameters = new SqlParameter[1];
            try
            {
                LogManager.WriteLog("EXPORT Stacker Details StackerID - " + StackerID + " Site Code -" + SiteCode + "Started.", LogManager.enumLogLevel.Info);
                sqlParameters[0] = new SqlParameter
                {
                    ParameterName = "@Stacker_Id",
                    Value = StackerID,
                    Direction = ParameterDirection.Input
                };

                var StackerInfoDetailsXML = (SqlHelper.ExecuteScalar(GetConnectionString(), CommandType.StoredProcedure, "rsp_ExportStackerInfoDetails", sqlParameters)).ToString();
                bool isCallSuccess = InvokeBgswsAdminWs(StackerInfoDetailsXML, ehID.ToString(), "STACKER", SiteCode);
                if (isCallSuccess)
                {
                    UpdateExportHistoryTableWithStatus(ehID, "100");
                    LogManager.WriteLog("EXPORT Stacker Details StackerID - " + StackerID + " Site Code -" + SiteCode + "Completed.", LogManager.enumLogLevel.Info);
                }
                else
                {
                    UpdateExportHistoryTableWithStatus(ehID, "-1");
                    LogManager.WriteLog("EXPORT Stacker Details StackerID - " + StackerID + " Site Code -" + SiteCode + "Failed.", LogManager.enumLogLevel.Info);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                UpdateExportHistoryTableWithStatus(ehID, "-1");
            }
        }
        /// <summary
        /// shareHolder
        /// </summary>
        /// <param name="ShareHolder_ID"></param>
        /// <param name="ehID"></param>
        /// <param name="SiteCode"></param>
        public void ExportShareHolderDetails(int ShareHolderID, int ehID, string SiteCode)
        {
            var sqlParameters = new SqlParameter[1];
            try
            {
                LogManager.WriteLog("EXPORT ShareHolder Details ShareHolder_ID - " + ShareHolderID + " Site Code -" + SiteCode + "Started.", LogManager.enumLogLevel.Info);
                sqlParameters[0] = new SqlParameter
                {
                    ParameterName = "@ShareHolderId",
                    Value = ShareHolderID,
                    Direction = ParameterDirection.Input
                };

                var ShareHolderInfoDetailsXML = (SqlHelper.ExecuteScalar(GetConnectionString(), CommandType.StoredProcedure, "rsp_ExportShareHolderInfoDetails", sqlParameters)).ToString();
                bool isCallSuccess = InvokeBgswsAdminWs(ShareHolderInfoDetailsXML, ehID.ToString(), "SHAREHOLDER", SiteCode);
                if (isCallSuccess)
                {
                    UpdateExportHistoryTableWithStatus(ehID, "100");
                    LogManager.WriteLog("EXPORT ShareHolder Details ShareHolder_ID - " + ShareHolderID + " Site Code -" + SiteCode + "Completed.", LogManager.enumLogLevel.Info);
                }
                else
                {
                    UpdateExportHistoryTableWithStatus(ehID, "-1");
                    LogManager.WriteLog("EXPORT ShareHolder Details ShareHolder_ID - " + ShareHolderID + " Site Code -" + SiteCode + "Failed.", LogManager.enumLogLevel.Info);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                UpdateExportHistoryTableWithStatus(ehID, "-1");
            }
        }

        public void ExportExpenseShareDetails(int ExpenseShareID, int ehID, string SiteCode)
        {
            var sqlParameters = new SqlParameter[1];
            try
            {
                LogManager.WriteLog("EXPORT ExpenseShare Details ExpenseShareID - " + ExpenseShareID + " Site Code -" + SiteCode + "Started.", LogManager.enumLogLevel.Info);
                sqlParameters[0] = new SqlParameter
                {
                    ParameterName = "@ExpenseShareId",
                    Value = ExpenseShareID,
                    Direction = ParameterDirection.Input
                };

                var ExpenseShareInfoDetailsXML = (SqlHelper.ExecuteScalar(GetConnectionString(), CommandType.StoredProcedure, "rsp_ExportExpenseShareInfoDetails", sqlParameters)).ToString();
                bool isCallSuccess = InvokeBgswsAdminWs(ExpenseShareInfoDetailsXML, ehID.ToString(), "EXPENSESHARE", SiteCode);
                if (isCallSuccess)
                {
                    UpdateExportHistoryTableWithStatus(ehID, "100");
                    LogManager.WriteLog("EXPORT ExpenseShare Details ExpenseShareID - " + ExpenseShareID + " Site Code -" + SiteCode + "Completed.", LogManager.enumLogLevel.Info);
                }
                else
                {
                    UpdateExportHistoryTableWithStatus(ehID, "-1");
                    LogManager.WriteLog("EXPORT ExpenseShare Details ExpenseShareID - " + ExpenseShareID + " Site Code -" + SiteCode + "Failed.", LogManager.enumLogLevel.Info);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                UpdateExportHistoryTableWithStatus(ehID, "-1");
            }
        }

        public void ExportProfitShareDetails(int ProfitShareID, int ehID, string SiteCode)
        {
            var sqlParameters = new SqlParameter[1];
            try
            {
                LogManager.WriteLog("EXPORT ProfitShare Details ProfitShareID - " + ProfitShareID + " Site Code -" + SiteCode + "Started.", LogManager.enumLogLevel.Info);
                sqlParameters[0] = new SqlParameter
                {
                    ParameterName = "@ProfitShareId",
                    Value = ProfitShareID,
                    Direction = ParameterDirection.Input
                };

                var ProfitShareInfoDetailsXML = (SqlHelper.ExecuteScalar(GetConnectionString(), CommandType.StoredProcedure, "rsp_ExportProfitShareInfoDetails", sqlParameters)).ToString();
                bool isCallSuccess = InvokeBgswsAdminWs(ProfitShareInfoDetailsXML, ehID.ToString(), "PROFITSHARE", SiteCode);
                if (isCallSuccess)
                {
                    UpdateExportHistoryTableWithStatus(ehID, "100");
                    LogManager.WriteLog("EXPORT ProfitShare Details ProfitShareID - " + ProfitShareID + " Site Code -" + SiteCode + "Completed.", LogManager.enumLogLevel.Info);
                }
                else
                {
                    UpdateExportHistoryTableWithStatus(ehID, "-1");
                    LogManager.WriteLog("EXPORT ProfitShare Details ProfitShareID - " + ProfitShareID + " Site Code -" + SiteCode + "Failed.", LogManager.enumLogLevel.Info);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                UpdateExportHistoryTableWithStatus(ehID, "-1");
            }
        }
        public void ExportExpenseShareGroupDetails(int ExpenseShareGroupID, int ehID, string SiteCode)
        {
            var sqlParameters = new SqlParameter[1];
            try
            {
                LogManager.WriteLog("EXPORT ExpenseShareGroup Details ExpenseShareGroupID - " + ExpenseShareGroupID + " Site Code -" + SiteCode + "Started.", LogManager.enumLogLevel.Info);
                sqlParameters[0] = new SqlParameter
                {
                    ParameterName = "@ExpenseShareGroupId",
                    Value = ExpenseShareGroupID,
                    Direction = ParameterDirection.Input
                };

                var ExpenseShareGroupInfoDetailsXML = (SqlHelper.ExecuteScalar(GetConnectionString(), CommandType.StoredProcedure, "rsp_ExportExpenseShareGroupInfoDetails", sqlParameters)).ToString();
                bool isCallSuccess = InvokeBgswsAdminWs(ExpenseShareGroupInfoDetailsXML, ehID.ToString(), "EXPENSESHAREGROUP", SiteCode);
                if (isCallSuccess)
                {
                    UpdateExportHistoryTableWithStatus(ehID, "100");
                    LogManager.WriteLog("EXPORT ExpenseShareGroup Details ExpenseShareGroupID - " + ExpenseShareGroupID + " Site Code -" + SiteCode + "Completed.", LogManager.enumLogLevel.Info);
                }
                else
                {
                    UpdateExportHistoryTableWithStatus(ehID, "-1");
                    LogManager.WriteLog("EXPORT ExpenseShareGroup Details ExpenseShareGroupID - " + ExpenseShareGroupID + " Site Code -" + SiteCode + "Failed.", LogManager.enumLogLevel.Info);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                UpdateExportHistoryTableWithStatus(ehID, "-1");
            }
        }
        public void ExportProfitShareGroupDetails(int ProfitShareGroupID, int ehID, string SiteCode)
        {
            var sqlParameters = new SqlParameter[1];
            try
            {
                LogManager.WriteLog("EXPORT ProfitShareGroup Details ProfitShareGroupID - " + ProfitShareGroupID + " Site Code -" + SiteCode + "Started.", LogManager.enumLogLevel.Info);
                sqlParameters[0] = new SqlParameter
                {
                    ParameterName = "@ProfitShareGroupId",
                    Value = ProfitShareGroupID,
                    Direction = ParameterDirection.Input
                };

                var ProfitShareGroupInfoDetailsXML = (SqlHelper.ExecuteScalar(GetConnectionString(), CommandType.StoredProcedure, "rsp_ExportProfitShareGroupInfoDetails", sqlParameters)).ToString();
                bool isCallSuccess = InvokeBgswsAdminWs(ProfitShareGroupInfoDetailsXML, ehID.ToString(), "PROFITSHAREGROUP", SiteCode);
                if (isCallSuccess)
                {
                    UpdateExportHistoryTableWithStatus(ehID, "100");
                    LogManager.WriteLog("EXPORT ProfitShareGroup Details ProfitShareGroupID - " + ProfitShareGroupID + " Site Code -" + SiteCode + "Completed.", LogManager.enumLogLevel.Info);
                }
                else
                {
                    UpdateExportHistoryTableWithStatus(ehID, "-1");
                    LogManager.WriteLog("EXPORT ProfitShareGroup Details ProfitShareGroupID - " + ProfitShareGroupID + " Site Code -" + SiteCode + "Failed.", LogManager.enumLogLevel.Info);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                UpdateExportHistoryTableWithStatus(ehID, "-1");
            }
        }
        public void ExportLiquidationDetails(int LiquidationId, int ehID, string SiteCode)
        {
            var sqlParameters = new SqlParameter[1];
            try
            {
                LogManager.WriteLog("EXPORT Liquidation Details LiquidationID - " + LiquidationId + " Site Code -" + SiteCode + "Started.", LogManager.enumLogLevel.Info);
                sqlParameters[0] = new SqlParameter
                {
                    ParameterName = "@LiquidationId",
                    Value = LiquidationId,
                    Direction = ParameterDirection.Input
                };

                var LiquidationInfoDetailsXML = (SqlHelper.ExecuteScalar(GetConnectionString(), CommandType.StoredProcedure, "rsp_ExportLiquidationInfoDetails", sqlParameters)).ToString();
                bool isCallSuccess = InvokeBgswsAdminWs(LiquidationInfoDetailsXML, ehID.ToString(), "LIQUIDATIONDETAILS", SiteCode);
                if (isCallSuccess)
                {
                    UpdateExportHistoryTableWithStatus(ehID, "100");
                    LogManager.WriteLog("EXPORT Liquidation Details LiquidationIdID - " + LiquidationId + " Site Code -" + SiteCode + "Completed.", LogManager.enumLogLevel.Info);
                }
                else
                {
                    UpdateExportHistoryTableWithStatus(ehID, "-1");
                    LogManager.WriteLog("EXPORT Liquidation Details LiquidationID - " + LiquidationId + " Site Code -" + SiteCode + "Failed.", LogManager.enumLogLevel.Info);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                UpdateExportHistoryTableWithStatus(ehID, "-1");
            }
        }
        public void ExportLiquidationShareDetails(int LiquidationShareId, int ehID, string SiteCode)
        {
            var sqlParameters = new SqlParameter[1];
            try
            {
                LogManager.WriteLog("EXPORT LiquidationShare Details LiquidationID - " + LiquidationShareId + " Site Code -" + SiteCode + "Started.", LogManager.enumLogLevel.Info);
                sqlParameters[0] = new SqlParameter
                {
                    ParameterName = "@LiquidationShareId",
                    Value = LiquidationShareId,
                    Direction = ParameterDirection.Input
                };

                var LiquidationShareInfoDetailsXML = (SqlHelper.ExecuteScalar(GetConnectionString(), CommandType.StoredProcedure, "rsp_ExportLiquidationShareInfoDetails", sqlParameters)).ToString();
                bool isCallSuccess = InvokeBgswsAdminWs(LiquidationShareInfoDetailsXML, ehID.ToString(), "LIQUIDATIONSHAREDETAILS", SiteCode);
                if (isCallSuccess)
                {
                    UpdateExportHistoryTableWithStatus(ehID, "100");
                    LogManager.WriteLog("EXPORT LiquidationShare Details LiquidationIdID - " + LiquidationShareId + " Site Code -" + SiteCode + "Completed.", LogManager.enumLogLevel.Info);
                }
                else
                {
                    UpdateExportHistoryTableWithStatus(ehID, "-1");
                    LogManager.WriteLog("EXPORT LiquidationShare Details LiquidationID - " + LiquidationShareId + " Site Code -" + SiteCode + "Failed.", LogManager.enumLogLevel.Info);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                UpdateExportHistoryTableWithStatus(ehID, "-1");
            }
        }
        /// <summary>


        public bool WrapExportXMLData(string sXMLData, string ehid, string type, string url, string siteCode)
        {
            try
            {
                LogManager.WriteLog("Inside WrapExportXMLData ", LogManager.enumLogLevel.Error);
                Proxy _proxy = new Proxy(siteCode);
                _proxy.Timeout = this.ProxyTimeoutInMilliseconds;
                //{WebURL = url};
                _proxy.WebURL = url;

                var sqlParameters = new SqlParameter[3];
                var sqlParameter = new SqlParameter
                {
                    ParameterName = "XMLData",
                    Value = sXMLData,
                    Direction = ParameterDirection.Input
                };
                sqlParameters[0] = sqlParameter;

                var oParamResult = new SqlParameter
                {
                    ParameterName = "TYPE",
                    Value = type,
                    Direction = ParameterDirection.Input
                };
                sqlParameters[1] = oParamResult;

                var oParamStatus = new SqlParameter
                {
                    ParameterName = "EH_ID",
                    Value = ehid,
                    Direction = ParameterDirection.Input
                };
                sqlParameters[2] = oParamStatus;

                LogManager.WriteLog("sXMLData: " + sXMLData, LogManager.enumLogLevel.Error);
                DataSet objDataset = SqlHelper.ExecuteDataset(GetConnectionString(), Constants.CONSTANT_RSP_WRAPSITEDETAILS, sqlParameters);
                if (objDataset.Tables.Count <= 0 || objDataset.Tables[0].Rows.Count <= 0 || string.IsNullOrEmpty(Convert.ToString(objDataset.Tables[0].Rows[0][0])))
                {
                    return false;
                }
                LogManager.WriteLog("sXMLData: " + sXMLData, LogManager.enumLogLevel.Error);
                sXMLData = string.Empty;
                foreach (DataRow row in objDataset.Tables[0].Rows)
                {
                    sXMLData += Convert.ToString(row[0]);
                }
                sXMLData = sXMLData.Replace(Environment.NewLine, "");
                sXMLData = sXMLData.Replace("\r", "");
                sXMLData = sXMLData.Replace("\n", "");
                sXMLData = sXMLData.Replace("\r\n", "");

                LogManager.WriteLog("sXMLData: " + sXMLData, LogManager.enumLogLevel.Error);
                return _proxy.ImportData(sXMLData);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return false;
        }

        public void SetCollectionByDateBarPositions(string strCollectionByDate, string strSiteCode, int ehID)
        {
            try
            {
                if (InvokeBgswsAdminWsForCollectionByDate(strCollectionByDate, strSiteCode))
                    UpdateExportHistoryTableWithStatus(ehID, "100");
                else
                    UpdateExportHistoryTableWithStatus(ehID, "-1");
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                UpdateExportHistoryTableWithStatus(ehID, "-1");
            }
        }

        public void GetImportSettings(string siteID, int exportHistoryID, string siteCode)
        {
            try
            {
                var sqlParam = new SqlParameter("Site_ID", Convert.ToInt32(siteID));
                string exportSiteData = (SqlHelper.ExecuteScalar(GetConnectionString(),
                                                                 CommandType.StoredProcedure,
                                                                 "rsp_GetSiteSettingsInXML", sqlParam)).ToString();

                if (InvokeBgswsAdminWs(exportSiteData, exportHistoryID.ToString(), "ImportSiteSettings", siteCode))
                    UpdateExportHistoryTableWithStatus(exportHistoryID, "100");
                else
                    UpdateExportHistoryTableWithStatus(exportHistoryID, "-1");

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                UpdateExportHistoryTableWithStatus(exportHistoryID, "-1");
            }
        }

        public void GetImportAFTInfo(int InstallationID, int ehID, string SiteCode)
        {

            try
            {

                LogManager.WriteLog(ConfigManager.Read(Constants.CONSTANT_LOGPATH) + "_Export.txt", "Import AFT Info for - " + InstallationID + " Site Code -" + SiteCode + "Started.", LogManager.enumLogLevel.Info);

                var sqlParam = new SqlParameter("Installation_ID", InstallationID);
                string exportAFTData = (SqlHelper.ExecuteScalar(GetConnectionString(),
                                                                 CommandType.StoredProcedure,
                                                                 "rsp_GetMachineAFTEnabledSetting", sqlParam)).ToString();

                LogManager.WriteLog("XML String " + exportAFTData, LogManager.enumLogLevel.Info);
                if (InvokeBgswsAdminWs(exportAFTData, ehID.ToString(), "AFTEnableDisable", SiteCode))
                {
                    LogManager.WriteLog("Update the export history status as succesfull", LogManager.enumLogLevel.Info);
                    UpdateExportHistoryTableWithStatus(ehID, "100");
                }
                else
                {
                    LogManager.WriteLog("Update the export history status as faiure", LogManager.enumLogLevel.Info);
                    UpdateExportHistoryTableWithStatus(ehID, "-1");
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                LogManager.WriteLog("Update the export history status as faiure", LogManager.enumLogLevel.Info);
                UpdateExportHistoryTableWithStatus(ehID, "-1");
            }
        }

        public void GetImportMasterEmployeeCardInfo(string EmployeeCard, int ehID, string SiteCode)
        {

            try
            {

                LogManager.WriteLog(ConfigManager.Read(Constants.CONSTANT_LOGPATH) + "_Export.txt", "Import Master card  Info for - " + EmployeeCard
                    + " Site Code -" + SiteCode + "Started.", LogManager.enumLogLevel.Info);

                SqlParameter[] sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter("@EmpCardNumber", EmployeeCard);
                sqlParam[1] = new SqlParameter("@SiteCode", SiteCode);

                string exportMasterCardData = (SqlHelper.ExecuteScalar(GetConnectionString(),
                                                                 CommandType.StoredProcedure,
                                                                 "rsp_GetMasterCardInfo", sqlParam)).ToString();

                LogManager.WriteLog("XML String " + exportMasterCardData, LogManager.enumLogLevel.Info);
                if (InvokeBgswsAdminWs(exportMasterCardData, ehID.ToString(), "MASTERCARDENABLE", SiteCode))
                {
                    LogManager.WriteLog("Update the export history status as succesfull", LogManager.enumLogLevel.Info);
                    UpdateExportHistoryTableWithStatus(ehID, "100");
                }
                else
                {
                    LogManager.WriteLog("Update the export history status as faiure", LogManager.enumLogLevel.Info);
                    UpdateExportHistoryTableWithStatus(ehID, "-1");
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                LogManager.WriteLog("Update the export history status as faiure", LogManager.enumLogLevel.Info);
                UpdateExportHistoryTableWithStatus(ehID, "-1");
            }
        }


        public void GetAFTSettings(int Denom, int EH_Id, string SiteCode)
        {
            try
            {

                LogManager.WriteLog(ConfigManager.Read(Constants.CONSTANT_LOGPATH) + "_Export.txt", "Export AFT Info for Denom - " + Denom +
                    " Site Code -" + SiteCode + "Started.", LogManager.enumLogLevel.Info);

                SqlParameter[] sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter("Denom ", Denom);
                sqlParam[1] = new SqlParameter("SiteCode", SiteCode);

                string exportAFTData = (SqlHelper.ExecuteScalar(GetConnectionString(),
                                                                 CommandType.StoredProcedure,
                                                                 "rsp_GetAFTSettingsinXML", sqlParam)).ToString();

                LogManager.WriteLog("XML String " + exportAFTData, LogManager.enumLogLevel.Info);
                if (InvokeBgswsAdminWs(exportAFTData, EH_Id.ToString(), "AFTSettings", SiteCode))
                {
                    LogManager.WriteLog("Update the export history status as successfull", LogManager.enumLogLevel.Info);
                    UpdateExportHistoryTableWithStatus(EH_Id, "100");
                }
                else
                {
                    LogManager.WriteLog("Update the export history status as faiure", LogManager.enumLogLevel.Info);
                    UpdateExportHistoryTableWithStatus(EH_Id, "-1");
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                LogManager.WriteLog("Update the export history status as faiure", LogManager.enumLogLevel.Info);
                UpdateExportHistoryTableWithStatus(EH_Id, "-1");
            }
        }


        public void GetImportUserRoles(int UserID, int ehID, string SiteCode)
        {
            var sqlParameters = new SqlParameter[1];
            try
            {
                sqlParameters[0] = new SqlParameter
                {
                    ParameterName = "UserID",
                    Value = UserID,
                    Direction = ParameterDirection.Input
                };
                var xmlUserRolesData = (SqlHelper.ExecuteScalar(GetConnectionString(), CommandType.StoredProcedure, "rsp_getUserRoleLinkinXML", sqlParameters)).ToString();
                bool isCallSuccess = InvokeBgswsAdminWs(xmlUserRolesData, ehID.ToString(), "USERROLE", SiteCode);
                if (isCallSuccess)
                {
                    UpdateExportHistoryTableWithStatus(ehID, "100");
                    LogManager.WriteLog("User Role Updated to site " + SiteCode, LogManager.enumLogLevel.Info);
                }
                else
                {
                    UpdateExportHistoryTableWithStatus(ehID, "-1");
                    LogManager.WriteLog("Unable to update user role to site " + SiteCode, LogManager.enumLogLevel.Info);
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                UpdateExportHistoryTableWithStatus(ehID, "-1");
            }
        }

        public void GetImportGameInfo(int InstallationID, int ehID, string SiteCode)
        {
            var sqlParameters = new SqlParameter[1];
            try
            {
                LogManager.WriteLog("Import Game Info for - " + InstallationID + " Site Code -" + SiteCode + "Started.", LogManager.enumLogLevel.Info);

                sqlParameters[0] = new SqlParameter
                {
                    ParameterName = "Installation_ID",
                    Value = InstallationID,
                    Direction = ParameterDirection.Input
                };
                var InstallationGameInfoXML = (SqlHelper.ExecuteScalar(GetConnectionString(), CommandType.StoredProcedure, "rsp_GetInstallationGameInfoinXML", sqlParameters)).ToString();
                bool isCallSuccess = InvokeBgswsAdminWs(InstallationGameInfoXML, ehID.ToString(), "GAMEINFO", SiteCode);
                if (isCallSuccess)
                {
                    UpdateExportHistoryTableWithStatus(ehID, "100");
                    LogManager.WriteLog("Import Game Info for - " + InstallationID + " Site Code -" + SiteCode + "Completed.", LogManager.enumLogLevel.Info);
                }
                else
                {
                    UpdateExportHistoryTableWithStatus(ehID, "-1");
                    LogManager.WriteLog("Import Game Info for - " + InstallationID + " Site Code -" + SiteCode + "Failed.", LogManager.enumLogLevel.Info);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                UpdateExportHistoryTableWithStatus(ehID, "-1");
            }
        }

        public void GetImportRoleAccessLinks(int RoleID, int ehID, string SiteCode)
        {
            var sqlParameters = new SqlParameter[1];
            try
            {
                sqlParameters[0] = new SqlParameter
                {
                    ParameterName = "RoleID",
                    Value = RoleID,
                    Direction = ParameterDirection.Input
                };
                var xmlRolesData = (SqlHelper.ExecuteScalar(GetConnectionString(), CommandType.StoredProcedure, "rsp_GetRoleAccessRoleLnkinXML", sqlParameters)).ToString();
                bool isCallSuccess = InvokeBgswsAdminWs(xmlRolesData, ehID.ToString(), "ROLEACCESSLINK", SiteCode);
                if (isCallSuccess)
                {
                    UpdateExportHistoryTableWithStatus(ehID, "100");
                    LogManager.WriteLog("RoleAccess link Updated to site " + SiteCode, LogManager.enumLogLevel.Info);
                }
                else
                {
                    UpdateExportHistoryTableWithStatus(ehID, "-1");
                    LogManager.WriteLog("Unable to update role access link to site " + SiteCode, LogManager.enumLogLevel.Info);
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                UpdateExportHistoryTableWithStatus(ehID, "-1");
            }

        }

        public void GetImportUser(int UserID, int ehID, string SiteCode, string MethodToInvoke, bool OldSite)
        {
            var sqlParameters = new SqlParameter[1];
            bool isCallSuccess = false;
            try
            {
                sqlParameters[0] = new SqlParameter
                {
                    ParameterName = "UserID",
                    Value = UserID,
                    Direction = ParameterDirection.Input
                };
                
                var xmlUserData = string.Empty;

                if (OldSite)
                    xmlUserData = (SqlHelper.ExecuteScalar(GetConnectionString(), CommandType.StoredProcedure, "rsp_GetUsersInXML1211", sqlParameters)).ToString();
                else
                    xmlUserData = (SqlHelper.ExecuteScalar(GetConnectionString(), CommandType.StoredProcedure, "rsp_GetUsersInXML", sqlParameters)).ToString();


                if (MethodToInvoke.ToUpper() == "ADDUSER")
                {
                    isCallSuccess = InvokeBgswsAdminWsForUser(xmlUserData, true, SiteCode);
                }
                else if (MethodToInvoke.ToUpper() == "REMOVEUSER")
                {
                    isCallSuccess = InvokeBgswsAdminWsForUser(xmlUserData, false, SiteCode);
                }
                if (isCallSuccess)
                {
                    UpdateExportHistoryTableWithStatus(ehID, "100");
                    LogManager.WriteLog("User Updated to site " + SiteCode, LogManager.enumLogLevel.Info);
                }
                else
                {
                    UpdateExportHistoryTableWithStatus(ehID, "-1");
                    LogManager.WriteLog("Unable to update User to site " + SiteCode, LogManager.enumLogLevel.Info);
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                UpdateExportHistoryTableWithStatus(ehID, "-1");
            }
        }

        public void GetImportAutoInstallation(int installationid, int ehID, string siteCode)
        {
            try
            {

                var sqlParameters = new SqlParameter[1];
                var sqlParameter = new SqlParameter
                {
                    ParameterName = "@Installation_ID",
                    Value = installationid,
                    Direction = ParameterDirection.Input
                };
                sqlParameters[0] = sqlParameter;
                var xmlExportAutoInstallation = (SqlHelper.ExecuteScalar(GetConnectionString(),
                                                                     CommandType.StoredProcedure,
                                                                     Constants.CONSTANT_RSP_EXPORTAUTOINSTALLATION, sqlParameters)).ToString();
                if (!string.IsNullOrEmpty(xmlExportAutoInstallation))
                {

                    var webUrl = SqlHelper.ExecuteScalar(GetConnectionString(), CommandType.Text,
                                                       "Select WebURL From Site Where Site_Code = " + siteCode.Trim()).ToString();

                    if (!string.IsNullOrEmpty(webUrl))
                    {

                        if (WrapExportXMLData(xmlExportAutoInstallation, ehID.ToString(), "AUTOINSTALLATION", webUrl, siteCode))
                            UpdateExportHistoryTableWithStatus(ehID, "100");
                        else
                            UpdateExportHistoryTableWithStatus(ehID, "-1");
                    }
                    else
                    {
                        LogManager.WriteLog("GetAutoInstallation: Sorry no Weburl found for ehid: " + ehID.ToString() + " Site_Code:" + siteCode.ToString(), LogManager.enumLogLevel.Info);
                    }
                }
                else
                {
                    LogManager.WriteLog("GetAutoInstallation: Sorry no Installation found for ehid: " + ehID.ToString() + " Site_Code:" + siteCode.ToString(), LogManager.enumLogLevel.Info);
                }
            }
            catch (Exception ex)
            {
                UpdateExportHistoryTableWithStatus(ehID, "-1");
                ExceptionManager.Publish(ex);
            }
        }

        public void GetAAMSConfigRecord(int iAAMSID, int ehID, string siteCode)
        {
            try
            {
                var sqlParameters = new SqlParameter[1];
                var sqlParameter = new SqlParameter
                {
                    ParameterName = "@AAMS_DETAILS_ID",
                    Value = iAAMSID,
                    Direction = ParameterDirection.Input
                };
                sqlParameters[0] = sqlParameter;
                var xmlExportAAMSDetails = (SqlHelper.ExecuteScalar(GetConnectionString(),
                                                                     CommandType.StoredProcedure,
                                                                     Constants.CONSTANT_RSP_GETAAMSDETAILSFOREXPORT, sqlParameters)).ToString();
                if (!string.IsNullOrEmpty(xmlExportAAMSDetails))
                {
                    if (InvokeBgswsAdminWs(xmlExportAAMSDetails, ehID.ToString(), "ImportAAMSConfigDetails", siteCode))
                        UpdateExportHistoryTableWithStatus(ehID, "100");
                    else
                        UpdateExportHistoryTableWithStatus(ehID, "-1");
                }
                else
                {
                    LogManager.WriteLog("GetAAMSConfigRecord: Sorry no data found for ehid: " + ehID.ToString() + " Site_Code:" + siteCode.ToString(), LogManager.enumLogLevel.Info);
                }
            }
            catch (Exception ex)
            {
                UpdateExportHistoryTableWithStatus(ehID, "-1");
                ExceptionManager.Publish(ex);
            }
        }

        public DataTable GetSiteList()
        {
            SqlConnection connection = null;
            var dsSiteList = new DataSet();

            try
            {
                connection = GetSqlConnection();

                if (connection != null)
                {
                    dsSiteList = SqlHelper.ExecuteDataset(connection,
                                                          CommandType.StoredProcedure,
                                                          Constants.CONSTANT_RSP_GETSITELIST);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            finally
            {
                if (connection != null && connection.State != ConnectionState.Closed)
                    connection.Close();
            }

            return dsSiteList.Tables.Count > 0 ? dsSiteList.Tables[0] : new DataTable();
        }

        private SqlConnection GetSqlConnection()
        {
            SqlConnection sqlConnection = null;
            try
            {
                string connectionString = GetConnectionString();
                sqlConnection = new SqlConnection { ConnectionString = connectionString };
                sqlConnection.Open();
                return sqlConnection;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return sqlConnection;
            }
        }

        public string GetConnectionString()
        {
            return Common.Utilities.DatabaseHelper.GetConnectionString();
        }
        private bool InvokeBgswsAdminWs(string xmlToSend, string ehid, string wsToInvoke, string siteCode)
        {
            bool isCallSuccess = false;
            Proxy _proxy = new Proxy(siteCode);
            _proxy.Timeout = this.ProxyTimeoutInMilliseconds;
            //var webUrl = SqlHelper.ExecuteScalar(GetConnectionString(), CommandType.Text,
            //                                        "Select WebURL From Site Where Site_Code = " + siteCode.Trim()).ToString();
            var webUrl = _proxy.WebURL;
            try
            {
                //_proxy.WebURL = webUrl; //This is created inside the proxy constructor.

                if (wsToInvoke == Constants.CONSTANT_WEBMETHOD_IMPORTMETERHISTORY)
                {
                    isCallSuccess = _proxy.ImportData(xmlToSend);
                }
                else if (wsToInvoke == Constants.CONSTANT_WEBMETHOD_IMPORTSITE)
                {
                    isCallSuccess = _proxy.ImportSite(xmlToSend);
                }
                // Employee Card Events and Modes changes 
                else if (wsToInvoke == "EMPGMUMODES")
                {
                    isCallSuccess = _proxy.ImportEmpGMUModes(xmlToSend);
                }
                else if (wsToInvoke == "EMPGMUEVENTS")
                {
                    isCallSuccess = _proxy.ImportEmpGMUEvents(xmlToSend);
                }   
                 else if (wsToInvoke == "SHAREHOLDER")
                {
                    isCallSuccess = WrapExportXMLData(xmlToSend, ehid, "SHAREHOLDER", webUrl, siteCode);
                }
                else if (wsToInvoke == "EXPENSESHAREGROUP")
                {
                    isCallSuccess = WrapExportXMLData(xmlToSend, ehid, "EXPENSESHAREGROUP", webUrl, siteCode);

                }
                else if (wsToInvoke == "PROFITSHAREGROUP")
                {
                    isCallSuccess = WrapExportXMLData(xmlToSend, ehid, "PROFITSHAREGROUP", webUrl, siteCode);

                }
                else if (wsToInvoke == "EXPENSESHARE")
                {
                    isCallSuccess = WrapExportXMLData(xmlToSend, ehid, "EXPENSESHARE", webUrl, siteCode);

                }
                else if (wsToInvoke == "PROFITSHARE")
                {
                    isCallSuccess = WrapExportXMLData(xmlToSend, ehid, "PROFITSHARE", webUrl, siteCode);

                }
                
                else if (wsToInvoke == "LIQUIDATIONDETAILS")
                {
                    isCallSuccess = WrapExportXMLData(xmlToSend, ehid, "LIQUIDATIONDETAILS", webUrl, siteCode);
                }
                else if (wsToInvoke == "LIQUIDATIONSHAREDETAILS")
                {
                    isCallSuccess = WrapExportXMLData(xmlToSend, ehid, "LIQUIDATIONSHAREDETAILS", webUrl, siteCode);
                }
                else if (wsToInvoke == Constants.CONSTANT_WEBMETHOD_IMPORTCROSSTICKETING)
                {
                    isCallSuccess = _proxy.ImportCrossTicketing(xmlToSend);
                }
                else if (wsToInvoke == Constants.CONSTANT_WEBMETHOD_IMPORTCALENDAR)
                {
                    isCallSuccess = _proxy.ImportCalendar(xmlToSend);
                }
                else if (wsToInvoke == Constants.CONSTANT_WEBMETHOD_IMPORTMODEL)
                {
                    isCallSuccess = _proxy.ImportModel(xmlToSend);
                }
                else if (wsToInvoke == "ImportSiteSettings")
                {
                    isCallSuccess = _proxy.ImportSiteSettings(xmlToSend);
                }
                else if (wsToInvoke == "USERROLE")
                {
                    isCallSuccess = _proxy.ImportRole(xmlToSend);
                }
                else if (wsToInvoke == "ROLEACCESSLINK")
                {
                    isCallSuccess = _proxy.ImportRoleAccessLnk(xmlToSend);
                }
                else if (wsToInvoke == "ImportAAMSConfigDetails")
                {
                    isCallSuccess = _proxy.ImportAAMSConfigDetails(xmlToSend);
                }
                else if (wsToInvoke == "GAMEINFO")
                {
                    isCallSuccess = _proxy.ImportGameInfoDetails(xmlToSend);
                }
                else if ((wsToInvoke == "GAMELIBRARY") || (wsToInvoke == "GAMECRC") || (wsToInvoke == "REMOVECRC"))
                {
                    isCallSuccess = _proxy.ImportExchangeData(xmlToSend, wsToInvoke);
                }
                else if (wsToInvoke == "CHANGEPASSWORD")
                {
                    isCallSuccess = _proxy.ImportPasswordChange(xmlToSend);
                }
                else if (wsToInvoke == "AFTEnableDisable")
                {
                    LogManager.WriteLog("Calling Proxy method" + wsToInvoke, LogManager.enumLogLevel.Info);
                    isCallSuccess = _proxy.ImportAFTInfoDetails(xmlToSend);
                }
                else if (wsToInvoke == "MASTERCARDENABLE")
                {
                    LogManager.WriteLog("Calling Proxy method" + wsToInvoke, LogManager.enumLogLevel.Info);
                    isCallSuccess = _proxy.ImportMasterCardDetails(xmlToSend);
                }
                else if (wsToInvoke == "AFTSettings")
                {
                    LogManager.WriteLog("Calling  AFT Proxy method" + wsToInvoke, LogManager.enumLogLevel.Info);
                    isCallSuccess = _proxy.ImportAFTSettingsDetails(xmlToSend);
                }
                else if (wsToInvoke == "CodeMaster")
                {
                    isCallSuccess = _proxy.ImportCodeMaster(xmlToSend);
                }
                else if (wsToInvoke == "LookupMaster")
                {
                    isCallSuccess = _proxy.ImportLookupMaster(xmlToSend);
                }
                else if (wsToInvoke == "LanguageLookup")
                {
                    isCallSuccess = _proxy.ImportLanguageLookup(xmlToSend);
                }
                else if (wsToInvoke == "MACHINECOMPDETAILS")
                {
                    isCallSuccess = _proxy.ImportMachineCompDetails(xmlToSend);
                }
                else if (wsToInvoke == "COMPONENTDETAILS")
                {
                    isCallSuccess = _proxy.ImportComponentDetails(xmlToSend);
                }
                else if (wsToInvoke == "ONDEMANDVERIFICATION")
                {
                    string[] args = xmlToSend.Split(new char[] { ',' });
                    LogManager.WriteLog("Calling On demand verifcation Method with params " + args[0] + " " + args[1], LogManager.enumLogLevel.Info);
                    isCallSuccess = _proxy.ImportOnDemandVerificationDetails(args[0], Convert.ToInt32(args[1]));
                }
                else if (wsToInvoke == "GAMECATEGORY")
                {
                    isCallSuccess = WrapExportXMLData(xmlToSend, ehid, "GAMECATEGORY", webUrl, siteCode);
                }
                else if (wsToInvoke == "GAMETITLE")
                {
                    isCallSuccess = WrapExportXMLData(xmlToSend, ehid, "GAMETITLE", webUrl, siteCode);
                }
                else if (wsToInvoke == "PAYTABLE")
                {
                    isCallSuccess = WrapExportXMLData(xmlToSend, ehid, "PAYTABLE", webUrl, siteCode);
                }
                else if (wsToInvoke == "GAMELIBRARY_MAPPING")
                {
                    isCallSuccess = WrapExportXMLData(xmlToSend, ehid, "GAMELIBRARY_MAPPING", webUrl, siteCode);
                }
                else if (wsToInvoke == "MANUFACTURER_DETAILS")
                {
                    isCallSuccess = WrapExportXMLData(xmlToSend, ehid, "MANUFACTURER_DETAILS", webUrl, siteCode);
                }
                else if (wsToInvoke == "MACHINEUPDATE")
                {
                    isCallSuccess = _proxy.ImportMachineUpdateDetails(xmlToSend);
                }
                else if (wsToInvoke == "GAMECAPPING")
                {
                    isCallSuccess = _proxy.ImportGameCappingParameters(xmlToSend);
                }

                else if (wsToInvoke == "CMPGameType")
                {
                    isCallSuccess = _proxy.ImportCMPGameTypes(xmlToSend);
                }
                else if (wsToInvoke == "STACKER")
                {
                    // isCallSuccess = _proxy.ExportStackerDetails(xmlToSend);
                    isCallSuccess = WrapExportXMLData(xmlToSend, ehid, "STACKER", webUrl, siteCode);
                }
                else if (wsToInvoke == "USERDETAILS")
                {
                    isCallSuccess = _proxy.ImportUser(xmlToSend, true);
                }
                else if (wsToInvoke == "ENABLESITE" | wsToInvoke == "DISABLESITE")
                {
                    isCallSuccess = _proxy.UpdateSiteEnabledStatus(xmlToSend);
                }
                else if (wsToInvoke == "SITELICENSING")
                {
                    LogManager.WriteLog("Site Licensing Proxy", LogManager.enumLogLevel.Debug);
                    isCallSuccess = _proxy.ImportSiteLicensing(xmlToSend);
                }
                else if (wsToInvoke == "ACTIVELICENSE")
                {
                    LogManager.WriteLog("Active Licensing Proxy", LogManager.enumLogLevel.Debug);
                    isCallSuccess = _proxy.ImportActiveLicensing(xmlToSend);

                }
                else if (wsToInvoke == "EXPORTROUTE")
                {
                    LogManager.WriteLog("Export Route Proxy", LogManager.enumLogLevel.Debug);
                    isCallSuccess = _proxy.ImportRoute(xmlToSend);
                }
                else if (wsToInvoke == "VAULTDEVICEDETAILS")
                {
                    isCallSuccess = _proxy.ImportVaultDetails(xmlToSend);
                }
                else if (wsToInvoke == "VAULTTRANSACTIONREASON")
                {
                    isCallSuccess = _proxy.ImportVaultTransactionReason(xmlToSend);
                }
                else if (wsToInvoke == "FACTORYRESET_STATUS")
                {
                    LogManager.WriteLog("Calling  FACTORYRESET_STATUS Method :: " + wsToInvoke + "\nXML\n" + xmlToSend, LogManager.enumLogLevel.Info);
                    isCallSuccess = _proxy.UpdateFactoryResetStatus(xmlToSend);
                }
                else if (wsToInvoke == "VAULTDROP")
                {
                    isCallSuccess = _proxy.ImportVaultDrop(xmlToSend);
                }
                else if (wsToInvoke == "TERMINATEVAULT")
                {
                    isCallSuccess = _proxy.ImportVaultTerminateDetails(xmlToSend);
                }
                else if (wsToInvoke == "MAILLIST")
                {
                    isCallSuccess = _proxy.ImportMailAlertList(xmlToSend);
                }
                else if (wsToInvoke == "MAILSERVER")
                {
                    isCallSuccess = _proxy.ImportMailServerInfo(xmlToSend);
                }
                else if (wsToInvoke == "DELETEZONE")
                {
                    isCallSuccess = WrapExportXMLData(xmlToSend, ehid, "DELETEZONE", webUrl, siteCode);
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("error in calling webservice" + ex.Message, LogManager.enumLogLevel.Info);
                ExceptionManager.Publish(ex);
                isCallSuccess = false;
            }
            return isCallSuccess;
        }

        private bool InvokeBgswsAdminWsForCollectionByDate(string strCollectionByDateDetails, string strSiteCode)
        {
            var isCallSuccess = false;
            Proxy _proxy = new Proxy(strSiteCode);
            _proxy.Timeout = this.ProxyTimeoutInMilliseconds;
            //var webUrl = SqlHelper.ExecuteScalar(GetConnectionString(), CommandType.Text,
            //                                           "Select WebURL From Site Where Site_Code = " + strSiteCode.Trim()).ToString();
            try
            {
                //_proxy.WebURL = webUrl;
                isCallSuccess = _proxy.RequestCollectionByDate(strCollectionByDateDetails, strSiteCode);

                return isCallSuccess;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return isCallSuccess;
            }
        }
        private bool InvokeBgswsAdminWsForUser(string xmlToSend, bool isAdd, string siteCode)
        {
            bool isCallSuccess = false;
            Proxy _proxy = new Proxy(siteCode);
            _proxy.Timeout = this.ProxyTimeoutInMilliseconds;
            //var webUrl = SqlHelper.ExecuteScalar(GetConnectionString(), CommandType.Text,
            //                                        "Select WebURL From Site Where Site_Code = " + siteCode.Trim()).ToString();
            try
            {
                //_proxy.WebURL = webUrl;
                isCallSuccess = _proxy.ImportUser(xmlToSend, isAdd);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                isCallSuccess = false;
            }
            return isCallSuccess;
        }

        public DataTable GetVerificationExportData()
        {
            try
            {
                return SqlHelper.ExecuteDataset(GetConnectionString(), CommandType.StoredProcedure, "rsp_GetVerificationExportData").Tables[0];
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return new DataTable();
            }
        }

        public void UpdateLGE_EHStatus(int LGE_ID, int Status)
        {
            try
            {
                DataBaseServiceHandler.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "usp_UpdateLGEStatus",
                    DataBaseServiceHandler.AddParameter<int>("@LGE_EH_ID", DbType.Int32, LGE_ID),
                    DataBaseServiceHandler.AddParameter<int>("@LGE_EH_Status", DbType.Int32, Status));
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
        }

        public void ExportGameLibrary(int Game_ID, int EH_ID, string Site_Code)
        {
            try
            {
                var sqlParameters = new SqlParameter[1];
                var sqlParameter = new SqlParameter
                {
                    ParameterName = "@GameID",
                    Value = Game_ID,
                    Direction = ParameterDirection.Input
                };
                sqlParameters[0] = sqlParameter;
                var xmlExportDetails = (SqlHelper.ExecuteScalar(GetConnectionString(),
                                                                     CommandType.StoredProcedure,
                                                                     "rsp_ExportGameDetailsToXML", sqlParameters)).ToString();
                if (!string.IsNullOrEmpty(xmlExportDetails))
                {
                    if (InvokeBgswsAdminWs(xmlExportDetails, EH_ID.ToString(), "GAMELIBRARY", Site_Code))
                        UpdateExportHistoryTableWithStatus(EH_ID, "100");
                    else
                        UpdateExportHistoryTableWithStatus(EH_ID, "-1");
                }
                else
                {
                    LogManager.WriteLog("String was empty for Game Details", LogManager.enumLogLevel.Info);
                    UpdateExportHistoryTableWithStatus(EH_ID, "-1");
                }
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                UpdateExportHistoryTableWithStatus(EH_ID, "-1");
            }
        }

        public void ExportGameCRC(int Game_ID, int EH_ID, string Site_Code)
        {
            try
            {
                var sqlParameters = new SqlParameter[1];
                var sqlParameter = new SqlParameter
                {
                    ParameterName = "@GameID",
                    Value = Game_ID,
                    Direction = ParameterDirection.Input
                };
                sqlParameters[0] = sqlParameter;
                var xmlExportDetails = (SqlHelper.ExecuteScalar(GetConnectionString(),
                                                                     CommandType.StoredProcedure,
                                                                     "rsp_ExportCRCToXML", sqlParameters)).ToString();
                if (!string.IsNullOrEmpty(xmlExportDetails))
                {
                    if (InvokeBgswsAdminWs(xmlExportDetails, EH_ID.ToString(), "GAMECRC", Site_Code))
                        UpdateExportHistoryTableWithStatus(EH_ID, "100");
                    else
                        UpdateExportHistoryTableWithStatus(EH_ID, "-1");
                }
                else
                {
                    LogManager.WriteLog("String was empty for Game CRC Details", LogManager.enumLogLevel.Info);
                    UpdateExportHistoryTableWithStatus(EH_ID, "100");
                }
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                UpdateExportHistoryTableWithStatus(EH_ID, "-1");
            }
        }

        public void ExportRemoveCRC(string cRC, int EH_ID, string Site_Code)
        {
            try
            {
                string xmlExportDetails = "<REMOVECRC><CRC>" + cRC + "</CRC></REMOVECRC>";

                if (InvokeBgswsAdminWs(xmlExportDetails, EH_ID.ToString(), "REMOVECRC", Site_Code))
                    UpdateExportHistoryTableWithStatus(EH_ID, "100");
                else
                    UpdateExportHistoryTableWithStatus(EH_ID, "-1");

            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                UpdateExportHistoryTableWithStatus(EH_ID, "-1");
            }
        }

        public void UpdateInProgressEH(string Site_Code)
        {
            try
            {
                DataBaseServiceHandler.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "usp_ResetInProgressRecordsinEH",
                    DataBaseServiceHandler.AddParameter<string>("@Site_Code", DbType.String, Site_Code));
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
        }

        public void UpdateInProgressEHForSites(string Site_Code)
        {
            try
            {
                DataBaseServiceHandler.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "usp_ResetInProgressRecordsinEHForSites",
                    DataBaseServiceHandler.AddParameter<string>("@Site_List", DbType.String, Site_Code));
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
        }

        public void UpdateInProgressLGE_EH()
        {
            try
            {
                DataBaseServiceHandler.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "usp_UpdateInProgressLGEStatus");
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
        }

        public void ExportPasswordChange(int userID, int ehID, string SiteCode)
        {
            var sqlParameters = new SqlParameter[1];
            try
            {
                LogManager.WriteLog("EXPORT CHANGE PASSWORD for - " + userID + " Site Code -" + SiteCode + "Started.", LogManager.enumLogLevel.Info);

                sqlParameters[0] = new SqlParameter
                {
                    ParameterName = "@User_ID",
                    Value = userID,
                    Direction = ParameterDirection.Input
                };
                var UserInfoDetailsXML = (SqlHelper.ExecuteScalar(GetConnectionString(), CommandType.StoredProcedure, "rsp_ExportUserInfoDetails", sqlParameters)).ToString();
                bool isCallSuccess = InvokeBgswsAdminWs(UserInfoDetailsXML, ehID.ToString(), "CHANGEPASSWORD", SiteCode);
                if (isCallSuccess)
                {
                    UpdateExportHistoryTableWithStatus(ehID, "100");
                    LogManager.WriteLog("EXPORT CHANGE PASSWORD for - " + userID + " Site Code -" + SiteCode + "Completed.", LogManager.enumLogLevel.Info);
                }
                else
                {
                    UpdateExportHistoryTableWithStatus(ehID, "-1");
                    LogManager.WriteLog("EXPORT CHANGE PASSWORD for - " + userID + " Site Code -" + SiteCode + "Failed.", LogManager.enumLogLevel.Info);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                UpdateExportHistoryTableWithStatus(ehID, "-1");
            }
        }

        public void ExportCodeMaster(int CodeMasterID, int ehID, string SiteCode)
        {
            try
            {
                var sqlParameters = new SqlParameter[1];
                LogManager.WriteLog("Export CodeMaster for Code - " + CodeMasterID + " Site Code -" + SiteCode + "Started.", LogManager.enumLogLevel.Info);

                sqlParameters[0] = new SqlParameter
                {
                    ParameterName = "@CodeMasterID",
                    Value = CodeMasterID,
                    Direction = ParameterDirection.Input
                };

                var xmlExportCodeMaster = (SqlHelper.ExecuteScalar(GetConnectionString(), CommandType.StoredProcedure, "rsp_ExportCodeMaster", sqlParameters)).ToString();
                bool isCallSuccess = InvokeBgswsAdminWs(xmlExportCodeMaster, ehID.ToString(), "CodeMaster", SiteCode);
                if (isCallSuccess)
                {
                    UpdateExportHistoryTableWithStatus(ehID, "100");
                    LogManager.WriteLog("Export CodeMaster for Code - " + CodeMasterID + " Site Code -" + SiteCode + "Completed.", LogManager.enumLogLevel.Info);
                }
                else
                {
                    UpdateExportHistoryTableWithStatus(ehID, "-1");
                    LogManager.WriteLog("Export CodeMaster for Code - " + CodeMasterID + " Site Code -" + SiteCode + "Failed.", LogManager.enumLogLevel.Info);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                UpdateExportHistoryTableWithStatus(ehID, "-1");
            }
        }

        public void ExportLookupMaster(int LookupMasterID, int ehID, string SiteCode)
        {
            try
            {
                var sqlParameters = new SqlParameter[1];
                LogManager.WriteLog("Export LookupMaster for ID - " + LookupMasterID + " Site Code -" + SiteCode + "Started.", LogManager.enumLogLevel.Info);

                sqlParameters[0] = new SqlParameter
                {
                    ParameterName = "@LookupMasterID",
                    Value = LookupMasterID,
                    Direction = ParameterDirection.Input
                };

                var xmlExportLookupMaster = (SqlHelper.ExecuteScalar(GetConnectionString(), CommandType.StoredProcedure, "rsp_ExportLookupMaster", sqlParameters)).ToString();
                bool isCallSuccess = InvokeBgswsAdminWs(xmlExportLookupMaster, ehID.ToString(), "LookupMaster", SiteCode);
                if (isCallSuccess)
                {
                    UpdateExportHistoryTableWithStatus(ehID, "100");
                    LogManager.WriteLog("Export LookupMaster for ID - " + LookupMasterID + " Site Code -" + SiteCode + "Completed.", LogManager.enumLogLevel.Info);
                }
                else
                {
                    UpdateExportHistoryTableWithStatus(ehID, "-1");
                    LogManager.WriteLog("Export LookupMaster for ID - " + LookupMasterID + " Site Code -" + SiteCode + "Failed.", LogManager.enumLogLevel.Info);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                UpdateExportHistoryTableWithStatus(ehID, "-1");
            }
        }

        public void ExportLanguageLookup(int LanguageLookupID, int ehID, string SiteCode)
        {
            try
            {
                var sqlParameters = new SqlParameter[1];
                LogManager.WriteLog("Export LanguageLookup for ID - " + LanguageLookupID + " Site Code -" + SiteCode + "Started.", LogManager.enumLogLevel.Info);

                sqlParameters[0] = new SqlParameter
                {
                    ParameterName = "@LanguageLookupID",
                    Value = LanguageLookupID,
                    Direction = ParameterDirection.Input
                };

                var xmlExportLanguageLookup = (SqlHelper.ExecuteScalar(GetConnectionString(), CommandType.StoredProcedure, "rsp_ExportLanguageLookup", sqlParameters)).ToString();
                bool isCallSuccess = InvokeBgswsAdminWs(xmlExportLanguageLookup, ehID.ToString(), "LanguageLookup", SiteCode);
                if (isCallSuccess)
                {
                    UpdateExportHistoryTableWithStatus(ehID, "100");
                    LogManager.WriteLog("Export LanguageLookup for ID - " + LanguageLookupID + " Site Code -" + SiteCode + "Completed.", LogManager.enumLogLevel.Info);
                }
                else
                {
                    UpdateExportHistoryTableWithStatus(ehID, "-1");
                    LogManager.WriteLog("Export LanguageLookup for ID - " + LanguageLookupID + " Site Code -" + SiteCode + "Failed.", LogManager.enumLogLevel.Info);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                UpdateExportHistoryTableWithStatus(ehID, "-1");
            }
        }

        /// <summary>
        /// Export Machine Component Details.
        /// </summary>
        /// <param name="MachineID"></param>
        /// <param name="ehID"></param>
        /// <param name="SiteCode"></param>
        public void ExportMachineComponentDetails(int MachineID, int ehID, string SiteCode)
        {
            try
            {
                var sqlParameters = new SqlParameter[1];
                LogManager.WriteLog("Export Machine Component Details for EH ID - " + ehID + ". Machine ID - " + MachineID + ". Site Code -" + SiteCode + "Started.", LogManager.enumLogLevel.Info);

                sqlParameters[0] = new SqlParameter
                {
                    ParameterName = "@Machine_ID",
                    Value = MachineID,
                    Direction = ParameterDirection.Input
                };

                var xml = (SqlHelper.ExecuteScalar(GetConnectionString(), CommandType.StoredProcedure, "rsp_GetMachineComponentDetailsForExport", sqlParameters)).ToString();
                bool isCallSuccess = InvokeBgswsAdminWs(xml, ehID.ToString(), "MACHINECOMPDETAILS", SiteCode);
                if (isCallSuccess)
                {
                    UpdateExportHistoryTableWithStatus(ehID, "100");
                    LogManager.WriteLog("Export Machine Component Details for EH ID - " + ehID + ". Machine ID - " + MachineID + ". Site Code -" + SiteCode + "Completed.", LogManager.enumLogLevel.Info);
                }
                else
                {
                    UpdateExportHistoryTableWithStatus(ehID, "-1");
                    LogManager.WriteLog("Export Machine Component Details for EH ID - " + ehID + ". Machine ID - " + MachineID + ". Site Code -" + SiteCode + "Failed.", LogManager.enumLogLevel.Info);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                UpdateExportHistoryTableWithStatus(ehID, "-1");
            }
        }

        /// <summary>
        /// Export Component Details.
        /// </summary>
        /// <param name="MachineID"></param>
        /// <param name="ehID"></param>
        /// <param name="SiteCode"></param>
        public void ExportComponentDetails(int CompID, int ehID, string SiteCode)
        {
            try
            {
                var sqlParameters = new SqlParameter[1];
                LogManager.WriteLog("Export Component Details for EH ID - " + ehID + ". Comp ID - " + CompID + ". Site Code -" + SiteCode + "Started.", LogManager.enumLogLevel.Info);

                sqlParameters[0] = new SqlParameter
                {
                    ParameterName = "@CCD_ID",
                    Value = CompID,
                    Direction = ParameterDirection.Input
                };

                var xml = (SqlHelper.ExecuteScalar(GetConnectionString(), CommandType.StoredProcedure, "rsp_GetComponentDetailsForExport", sqlParameters)).ToString();
                bool isCallSuccess = InvokeBgswsAdminWs(xml, ehID.ToString(), "COMPONENTDETAILS", SiteCode);
                if (isCallSuccess)
                {
                    UpdateExportHistoryTableWithStatus(ehID, "100");
                    LogManager.WriteLog("Export Component Details for EH ID - " + ehID + ". Comp ID - " + CompID + ". Site Code -" + SiteCode + "Completed.", LogManager.enumLogLevel.Info);
                }
                else
                {
                    UpdateExportHistoryTableWithStatus(ehID, "-1");
                    LogManager.WriteLog("Export Component Details for EH ID - " + ehID + ". Comp ID - " + CompID + ". Site Code -" + SiteCode + "Failed.", LogManager.enumLogLevel.Info);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                UpdateExportHistoryTableWithStatus(ehID, "-1");
            }
        }


        /// <summary>
        /// Export Component Details.
        /// </summary>
        /// <param name="MachineID"></param>
        /// <param name="ehID"></param>
        /// <param name="SiteCode"></param>
        public void ExportOnDemandDetails(string strSerialCompID, int ehID, string SiteCode)
        {
            try
            {
                LogManager.WriteLog("Export On Demand Details for EH ID - " + ehID + ". Comp ID - " + strSerialCompID + ". Site Code -" + SiteCode + "Started.", LogManager.enumLogLevel.Info);


                bool isCallSuccess = InvokeBgswsAdminWs(strSerialCompID, ehID.ToString(), "ONDEMANDVERIFICATION", SiteCode);
                if (isCallSuccess)
                {
                    UpdateExportHistoryTableWithStatus(ehID, "100");
                    LogManager.WriteLog("Export On Demand Details for EH ID - " + ehID + ". Comp ID - " + strSerialCompID + ". Site Code -" + SiteCode + "Completed.", LogManager.enumLogLevel.Info);
                }
                else
                {
                    UpdateExportHistoryTableWithStatus(ehID, "-1");
                    LogManager.WriteLog("Export On Demand Details for EH ID - " + ehID + ". Comp ID - " + strSerialCompID + ". Site Code -" + SiteCode + "Failed.", LogManager.enumLogLevel.Info);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                UpdateExportHistoryTableWithStatus(ehID, "-1");
            }
        }

        public DataTable GetRequestVerificationForComponent()
        {
            try
            {
                return SqlHelper.ExecuteDataset(GetConnectionString(), CommandType.StoredProcedure, "rsp_GetUnverifiedComponentsData").Tables[0];
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                return new DataTable();
            }
        }

        public void UpdateVerifiedComponents(int iVerID, int iCompID, string strSerialNo)
        {
            try
            {
                DataBaseServiceHandler.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "usp_UpdateUnverifiedComponentsData",
                    DataBaseServiceHandler.AddParameter<int>("@CVD_ID", DbType.Int32, iVerID),
                    DataBaseServiceHandler.AddParameter<int>("@ComponentID", DbType.Int32, iCompID),
                    DataBaseServiceHandler.AddParameter<string>("@Serial_No", DbType.String, strSerialNo));
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
        }

        public void ExportGameCategory(int GameCategoryID, int ehID, string SiteCode)
        {
            try
            {
                var sqlParameters = new SqlParameter[1];
                LogManager.WriteLog("Export GameCategory for Code - " + GameCategoryID + " Site Code -" + SiteCode + "Started.", LogManager.enumLogLevel.Info);

                sqlParameters[0] = new SqlParameter
                {
                    ParameterName = "@GameCategoryID",
                    Value = GameCategoryID,
                    Direction = ParameterDirection.Input
                };

                var xmlExportGameCategory = (SqlHelper.ExecuteScalar(GetConnectionString(), CommandType.StoredProcedure, "rsp_ExportGameCategory", sqlParameters)).ToString();
                bool isCallSuccess = InvokeBgswsAdminWs(xmlExportGameCategory, ehID.ToString(), "GAMECATEGORY", SiteCode);
                if (isCallSuccess)
                {
                    UpdateExportHistoryTableWithStatus(ehID, "100");
                    LogManager.WriteLog("Export GameCategory for Code - " + GameCategoryID + " Site Code -" + SiteCode + "Completed.", LogManager.enumLogLevel.Info);
                }
                else
                {
                    UpdateExportHistoryTableWithStatus(ehID, "-1");
                    LogManager.WriteLog("Export GameCategory for Code - " + GameCategoryID + " Site Code -" + SiteCode + "Failed.", LogManager.enumLogLevel.Info);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                UpdateExportHistoryTableWithStatus(ehID, "-1");
            }
        }

        public void ExportGameTitle(int GameTitleID, int ehID, string SiteCode)
        {
            try
            {
                var sqlParameters = new SqlParameter[1];
                LogManager.WriteLog("Export GameTitle for Code - " + GameTitleID + " Site Code -" + SiteCode + "Started.", LogManager.enumLogLevel.Info);

                sqlParameters[0] = new SqlParameter
                {
                    ParameterName = "@GameTitleID",
                    Value = GameTitleID,
                    Direction = ParameterDirection.Input
                };

                var xmlExportGameTitle = (SqlHelper.ExecuteScalar(GetConnectionString(), CommandType.StoredProcedure, "rsp_ExportGameTitle", sqlParameters)).ToString();
                bool isCallSuccess = InvokeBgswsAdminWs(xmlExportGameTitle, ehID.ToString(), "GAMETITLE", SiteCode);
                if (isCallSuccess)
                {
                    UpdateExportHistoryTableWithStatus(ehID, "100");
                    LogManager.WriteLog("Export GameTitle for Code - " + GameTitleID + " Site Code -" + SiteCode + "Completed.", LogManager.enumLogLevel.Info);
                }
                else
                {
                    UpdateExportHistoryTableWithStatus(ehID, "-1");
                    LogManager.WriteLog("Export GameTitle for Code - " + GameTitleID + " Site Code -" + SiteCode + "Failed.", LogManager.enumLogLevel.Info);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                UpdateExportHistoryTableWithStatus(ehID, "-1");
            }
        }

        public void ExportPaytable(int PaytableID, int ehID, string SiteCode)
        {
            try
            {
                var sqlParameters = new SqlParameter[1];
                LogManager.WriteLog("Export Paytable for Code - " + PaytableID + " Site Code -" + SiteCode + "Started.", LogManager.enumLogLevel.Info);

                sqlParameters[0] = new SqlParameter
                {
                    ParameterName = "@PaytableID",
                    Value = PaytableID,
                    Direction = ParameterDirection.Input
                };

                var xmlExportPaytable = (SqlHelper.ExecuteScalar(GetConnectionString(), CommandType.StoredProcedure, "rsp_ExportPaytable", sqlParameters)).ToString();
                bool isCallSuccess = InvokeBgswsAdminWs(xmlExportPaytable, ehID.ToString(), "PAYTABLE", SiteCode);
                if (isCallSuccess)
                {
                    UpdateExportHistoryTableWithStatus(ehID, "100");
                    LogManager.WriteLog("Export Paytable for Code - " + PaytableID + " Site Code -" + SiteCode + "Completed.", LogManager.enumLogLevel.Info);
                }
                else
                {
                    UpdateExportHistoryTableWithStatus(ehID, "-1");
                    LogManager.WriteLog("Export Paytable for Code - " + PaytableID + " Site Code -" + SiteCode + "Failed.", LogManager.enumLogLevel.Info);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                UpdateExportHistoryTableWithStatus(ehID, "-1");
            }
        }

        public void ExportGameLibraryMapping(int GameLibraryID, int ehID, string SiteCode)
        {
            try
            {
                var sqlParameters = new SqlParameter[1];
                LogManager.WriteLog("Export GameLibraryMapping for Code - " + GameLibraryID + " Site Code -" + SiteCode + "Started.", LogManager.enumLogLevel.Info);

                sqlParameters[0] = new SqlParameter
                {
                    ParameterName = "@GameLibraryID",
                    Value = GameLibraryID,
                    Direction = ParameterDirection.Input
                };

                var xmlExportGameLibrary = (SqlHelper.ExecuteScalar(GetConnectionString(), CommandType.StoredProcedure, "rsp_ExportGameLibraryMapping", sqlParameters)).ToString();
                bool isCallSuccess = InvokeBgswsAdminWs(xmlExportGameLibrary, ehID.ToString(), "GAMELIBRARY_MAPPING", SiteCode);
                if (isCallSuccess)
                {
                    UpdateExportHistoryTableWithStatus(ehID, "100");
                    LogManager.WriteLog("Export GameLibraryMapping for Code - " + GameLibraryID + " Site Code -" + SiteCode + "Completed.", LogManager.enumLogLevel.Info);
                }
                else
                {
                    UpdateExportHistoryTableWithStatus(ehID, "-1");
                    LogManager.WriteLog("Export GameLibraryMapping for Code - " + GameLibraryID + " Site Code -" + SiteCode + "Failed.", LogManager.enumLogLevel.Info);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                UpdateExportHistoryTableWithStatus(ehID, "-1");
            }
        }

        public void ExportMachineUpdateDetails(int MacID, int ehID, string SiteCode)
        {
            try
            {
                var sqlParameters = new SqlParameter[1];
                LogManager.WriteLog("ExportMachineUpdateDetails for id - " + MacID.ToString() + " Site Code -" + SiteCode + "Started.", LogManager.enumLogLevel.Info);

                sqlParameters[0] = new SqlParameter
                {
                    ParameterName = "@Mac_ID",
                    Value = MacID,
                    Direction = ParameterDirection.Input
                };

                var xmlExportGameLibrary = (SqlHelper.ExecuteScalar(GetConnectionString(), CommandType.StoredProcedure, "rsp_GetMachineUpdateDetailsForExport", sqlParameters)).ToString();
                bool isCallSuccess = InvokeBgswsAdminWs(xmlExportGameLibrary, ehID.ToString(), "MACHINEUPDATE", SiteCode);
                if (isCallSuccess)
                {
                    UpdateExportHistoryTableWithStatus(ehID, "100");
                    LogManager.WriteLog("ExportMachineUpdateDetails for id - " + MacID.ToString() + " Site Code -" + SiteCode + "Completed.", LogManager.enumLogLevel.Info);
                }
                else
                {
                    UpdateExportHistoryTableWithStatus(ehID, "-1");
                    LogManager.WriteLog("ExportMachineUpdateDetails for id - " + MacID.ToString() + " Site Code -" + SiteCode + "Failed.", LogManager.enumLogLevel.Info);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                UpdateExportHistoryTableWithStatus(ehID, "-1");
            }
        }

        public void ExportManufacturerDetails(int ManufacturerID, int ehID, string SiteCode)
        {
            try
            {
                var sqlParameters = new SqlParameter[1];
                LogManager.WriteLog("Export Maufacturer Details for Code - " + ManufacturerID + " Site Code -" + SiteCode + "Started.", LogManager.enumLogLevel.Info);

                sqlParameters[0] = new SqlParameter
                {
                    ParameterName = "@ManufacturerID",
                    Value = ManufacturerID,
                    Direction = ParameterDirection.Input
                };

                DataSet objDataset = SqlHelper.ExecuteDataset(GetConnectionString(), CommandType.StoredProcedure, "rsp_ExportManufacturerDetails", sqlParameters);
                if (objDataset.Tables.Count <= 0 || objDataset.Tables[0].Rows.Count <= 0 || string.IsNullOrEmpty(Convert.ToString(objDataset.Tables[0].Rows[0][0])))
                {
                    UpdateExportHistoryTableWithStatus(ehID, "-1");
                    LogManager.WriteLog("Export Maufacturer Details for Code - " + ManufacturerID + " Site Code -" + SiteCode + "Failed.\r\n Dataset is empty", LogManager.enumLogLevel.Info);
                    return;
                }
                string xmlExportManufacturerDetails = string.Empty;
                foreach (DataRow row in objDataset.Tables[0].Rows)
                {
                    xmlExportManufacturerDetails += Convert.ToString(row[0]);
                }
                // (Convert.ToString(objDataset.Tables[0].Rows[0][0]) + "").Trim();
                bool isCallSuccess = InvokeBgswsAdminWs(xmlExportManufacturerDetails, ehID.ToString(), "MANUFACTURER_DETAILS", SiteCode);
                if (isCallSuccess)
                {
                    UpdateExportHistoryTableWithStatus(ehID, "100");
                    LogManager.WriteLog("Export Maufacturer Details for Code - " + ManufacturerID + " Site Code -" + SiteCode + "Completed.", LogManager.enumLogLevel.Info);
                }
                else
                {
                    UpdateExportHistoryTableWithStatus(ehID, "-1");
                    LogManager.WriteLog("Export Maufacturer Details for Code - " + ManufacturerID + " Site Code -" + SiteCode + "Failed.", LogManager.enumLogLevel.Info);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                UpdateExportHistoryTableWithStatus(ehID, "-1");
            }
        }

        public void UpdateSiteEnabledStatus(string siteID, int ehID, string siteCode, string type)
        {
            try
            {
                var sqlParam = new SqlParameter("@Site_ID", Convert.ToInt32(siteID));

                string siteStatus = type == "ENABLESITE" ? "1" : "0";
                bool siteEnabledStatus = type == "ENABLESITE" ? true : false;

                string customXML = "<Site><SiteStatus><Site_Enabled>" + siteStatus + "</Site_Enabled></SiteStatus></Site>";

                bool isCallSuccess = InvokeBgswsAdminWs(customXML, ehID.ToString(), type, siteCode);

                if (isCallSuccess)
                {
                    UpdateExportHistoryTableWithStatus(ehID, "100");
                    UpdateSiteStatus(Convert.ToInt32(siteID), siteEnabledStatus);
                    LogManager.WriteLog("Update Site Enabled Status For Site - " + siteCode + "Completed.", LogManager.enumLogLevel.Info);
                }
                else
                {
                    UpdateExportHistoryTableWithStatus(ehID, "-1");
                    LogManager.WriteLog("Update Site Enabled Status For Site - " + siteCode + "Failed.", LogManager.enumLogLevel.Error);
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                UpdateExportHistoryTableWithStatus(ehID, "-1");
            }
        }

        private void UpdateSiteStatus(int siteID, bool siteStatus)
        {
            int iNoOfRowsAffected;

            try
            {
                var oParams = new SqlParameter[2];

                var oParam = new SqlParameter
                {
                    ParameterName = "@Site_ID",
                    Value = siteID,
                    DbType = DbType.Int16,
                    Direction = ParameterDirection.Input
                };
                oParams[0] = oParam;

                oParam = new SqlParameter
                {
                    ParameterName = "@Site_Enabled",
                    Value = siteStatus,
                    DbType = DbType.Boolean,
                    Direction = ParameterDirection.Input
                };
                oParams[1] = oParam;

                iNoOfRowsAffected = SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure,
                                                              "usp_UpdateSiteEnabledStatus", oParams);

                if (iNoOfRowsAffected == 1)
                {
                    LogManager.WriteLog(string.Format("{0} - {1}", "Site Staus Updated Successfully for Site ID", siteID),
                        LogManager.enumLogLevel.Info);
                }
                else
                {
                    LogManager.WriteLog(string.Format("{0} - {1}", "Site Staus Update Failed for Site ID", siteID),
                        LogManager.enumLogLevel.Info);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        internal int GetAFTStatus(string SiteCode)
        {
            int iNoOfRowsAffected = Convert.ToInt32(SqlHelper.ExecuteScalar(GetConnectionString(), CommandType.StoredProcedure,
                                                              "rsp_GetAFTSettingsStatus", new SqlParameter("SiteCode", SiteCode)).ToString());
            return iNoOfRowsAffected;
        }

        internal void ExportCMPGameTypes(int MachineID, int ehID, string SiteCode)
        {
            try
            {
                var sqlParameters = new SqlParameter[1];
                LogManager.WriteLog("ExportCMPGameTypes for id - " + MachineID.ToString() + " Site Code -" + SiteCode + "Started.", LogManager.enumLogLevel.Info);

                sqlParameters[0] = new SqlParameter
                {
                    ParameterName = "@MachineID",
                    Value = MachineID,
                    Direction = ParameterDirection.Input
                };

                var xmlExportGameTypes = (SqlHelper.ExecuteScalar(GetConnectionString(), CommandType.StoredProcedure, "rsp_GetCMPGameTypesForExport", sqlParameters)).ToString();
                bool isCallSuccess = InvokeBgswsAdminWs(xmlExportGameTypes, ehID.ToString(), "CMPGameType", SiteCode);
                if (isCallSuccess)
                {
                    UpdateExportHistoryTableWithStatus(ehID, "100");
                    LogManager.WriteLog("ExportCMPGameTypes for machine id - " + MachineID.ToString() + " Site Code -" + SiteCode + "Completed.", LogManager.enumLogLevel.Info);
                }
                else
                {
                    UpdateExportHistoryTableWithStatus(ehID, "-1");
                    LogManager.WriteLog("ExportCMPGameTypes for machine id - " + MachineID.ToString() + " Site Code -" + SiteCode + "Failed.", LogManager.enumLogLevel.Info);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                UpdateExportHistoryTableWithStatus(ehID, "-1");
            }
        }

        public bool GetExportCollectionBatch(string batchID, string ehid, string sSiteCode)
        {
            LogManager.WriteLog("Inside GetExportCollectionBatch", LogManager.enumLogLevel.Info);
            System.Threading.AutoResetEvent _Reset = new System.Threading.AutoResetEvent(false);
            var oXmlDetails = new XmlDocument();
            int PerItemProcessInterval;
            try
            {
                //Wait befor processing next data(To avoid CPU load) 
                PerItemProcessInterval = Convert.ToInt32(ConfigManager.Read("PerItemProcessMilliseconds"));
            }
            catch
            {
                PerItemProcessInterval = 100;
            }

            var oExportCollection = new SqlParameter[2];
            var oParam = new SqlParameter
            {
                ParameterName = "@Site_Code",
                Value = sSiteCode,
                Direction = ParameterDirection.Input
            };
            oExportCollection[0] = oParam;

            oParam = new SqlParameter
            {
                ParameterName = "@Batch_Id",
                Value = batchID,
                Direction = ParameterDirection.Input
            };
            oExportCollection[1] = oParam;

            LogManager.WriteLog("Before execute RSP_EXPORTINSERTEDCOLLECTIONDECLARATION", LogManager.enumLogLevel.Info);
            DataSet exportCollectionData = SqlHelper.ExecuteDataset(GetConnectionString(),
                                                                      CommandType.StoredProcedure,
                                                                      Constants.CONSTANT_RSP_EXPORTINSERTEDCOLLECTIONDECLARATION,
                                                                      oExportCollection);
            LogManager.WriteLog("After execute RSP_EXPORTINSERTEDCOLLECTIONDECLARATION", LogManager.enumLogLevel.Info);
            LogManager.WriteLog("Result count" + exportCollectionData.Tables.Count.ToString(), LogManager.enumLogLevel.Info);

            bool wsCallSuccess = false;
            if (exportCollectionData.Tables.Count > 0)
            {
                LogManager.WriteLog("Exporting Batch Total iteration:" + exportCollectionData.Tables[0].Rows.Count.ToString(), LogManager.enumLogLevel.Info);
                var webUrl = SqlHelper.ExecuteScalar(GetConnectionString(), CommandType.Text,
                                                       "Select WebURL From Site Where Site_Code = " + sSiteCode.Trim()).ToString();

                for (int iIndex = 0; iIndex < exportCollectionData.Tables[0].Rows.Count; iIndex++)
                {
                    LogManager.WriteLog("Exporting Batch Iteration Index: " + iIndex.ToString(), LogManager.enumLogLevel.Info);
                    wsCallSuccess = WrapExportXMLData(exportCollectionData.Tables[0].Rows[iIndex][0].ToString(), ehid, "ENTCOLLBATCH", webUrl, sSiteCode);
                    if (!wsCallSuccess)
                    {
                        LogManager.WriteLog("Error Exporting Batch ", LogManager.enumLogLevel.Error);
                        UpdateExportHistoryTableWithStatus(Convert.ToInt32(ehid), "-1");
                        break;
                    }

                    UpdateExportHistoryTableWithStatus(Convert.ToInt32(ehid), "100");
                    //Wait befor processing next data(To avoid CPU load) 
                    _Reset.WaitOne(PerItemProcessInterval);
                }
            }

            throw new Exception();
        }
        public void ExportVaultDetails(string VaultID, int ehID, string SiteCode)
        {
            try
            {
                var sqlParameters = new SqlParameter[1];
                LogManager.WriteLog("Export Vault Details for the site of VaultID - " + VaultID + " Site Code -" + SiteCode + "Started.", LogManager.enumLogLevel.Info);
                sqlParameters[0] = new SqlParameter
                {
                    ParameterName = "@Vault_ID",
                    Value = VaultID,
                    Direction = ParameterDirection.Input
                };
                var xmlExportVaultDetails = (SqlHelper.ExecuteScalar(GetConnectionString(), CommandType.StoredProcedure, "rsp_Vault_GetDeviceDetailsForExport", sqlParameters)).ToString();
                bool isCallSuccess = InvokeBgswsAdminWs(xmlExportVaultDetails, ehID.ToString(), "VAULTDEVICEDETAILS", SiteCode);
                if (isCallSuccess)
                {
                    UpdateExportHistoryTableWithStatus(ehID, "100");
                    LogManager.WriteLog("Export Vault Details for site of VaultID - " + VaultID + " Site Code -" + SiteCode + "Completed.", LogManager.enumLogLevel.Info);
                }
                else
                {
                    UpdateExportHistoryTableWithStatus(ehID, "-1");
                    LogManager.WriteLog("Export Vault Details for the site of VaultID - " + VaultID + " Site Code -" + SiteCode + "Failed.", LogManager.enumLogLevel.Info);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                UpdateExportHistoryTableWithStatus(ehID, "-1");
            }
        }

        /// <summary>
        /// Generate XML for Site Licensing and export to the perticular Site
        /// </summary>
        /// <param name="iLicenseInfoID"></param>
        /// <param name="ehID"></param>
        /// <param name="SiteCode"></param>
        public void ExportLicenseInfo(int iLicenseInfoID, int ehID, string SiteCode)
        {
            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[3];
                SqlParameter[] sqlParams = new SqlParameter[3];
                LogManager.WriteLog("Export License Details for Code - " + iLicenseInfoID + " Site Code - " + SiteCode + "Started.", LogManager.enumLogLevel.Info);
                sqlParams[0] = new SqlParameter
                {
                    ParameterName = "@LicenseInfoID",
                    Value = iLicenseInfoID,
                    Direction = ParameterDirection.Input
                };
                
                sqlParams[1] = new SqlParameter
                {
                    ParameterName = "@ExpiryDate",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 100,
                    //Value = SiteLicensingCryptoHelper.Encrypt(licenseEncriptedValue.Split(',')[0], SITE_LICENSING_KEY),
                    Direction = ParameterDirection.Output
                };
                sqlParams[2] = new SqlParameter
                {
                    ParameterName = "@LicenseKey",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 100,
                    //Value = SiteLicensingCryptoHelper.Encrypt(licenseEncriptedValue.Split(',')[1], SITE_LICENSING_KEY),
                    Direction = ParameterDirection.Output
                };

                SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "rsp_SL_GetLicenseDateandKey", sqlParams);
                if (sqlParams[1] == null || String.IsNullOrEmpty(Convert.ToString(sqlParams[1].Value)))
                {
                    LogManager.WriteLog("License Expiry date is Empty. No License Details Returned for the the iLicenseInfoID: " + iLicenseInfoID, LogManager.enumLogLevel.Info);
                    UpdateExportHistoryTableWithStatus(ehID, "-1");
                    return;
                }
                if (sqlParams[2] == null || String.IsNullOrEmpty(Convert.ToString(sqlParams[2].Value)))
                {
                    LogManager.WriteLog("License Key is Empty. No License Details Returned for the the iLicenseInfoID: " + iLicenseInfoID, LogManager.enumLogLevel.Info);
                    UpdateExportHistoryTableWithStatus(ehID, "-1");
                    return;
                }

                sqlParameters[0] = new SqlParameter
                {
                    ParameterName = "@LicenseInfoID",
                    Value = iLicenseInfoID,
                    Direction = ParameterDirection.Input
                };
                sqlParameters[1] = new SqlParameter
                {
                    ParameterName = "@ExpiryDate",
                    Value = SiteLicensingCryptoHelper.Encrypt(Convert.ToString(sqlParams[1].Value), SITE_LICENSING_KEY),
                    Direction = ParameterDirection.Input
                };
                sqlParameters[2] = new SqlParameter
                {
                    ParameterName = "@LicenseKey",
                    Value = SiteLicensingCryptoHelper.Encrypt(Convert.ToString(sqlParams[2].Value), SITE_LICENSING_KEY),
                    Direction = ParameterDirection.Input
                };

                var xmlExportLicenseDetails = (SqlHelper.ExecuteScalar(GetConnectionString(), CommandType.StoredProcedure, "rsp_SL_ExportLicenseInfo", sqlParameters)).ToString();
                bool isCallSuccess = InvokeBgswsAdminWs(xmlExportLicenseDetails, ehID.ToString(), "SITELICENSING", SiteCode);
                if (isCallSuccess)
                {
                    UpdateExportHistoryTableWithStatus(ehID, "100");
                    LogManager.WriteLog("Export License Details for Code - " + iLicenseInfoID + " Site Code -" + SiteCode + "Completed.", LogManager.enumLogLevel.Info);
                }
                else
                {
                    UpdateExportHistoryTableWithStatus(ehID, "-1");
                    LogManager.WriteLog("Export License Details for Code - " + iLicenseInfoID + " Site Code -" + SiteCode + "Failed.", LogManager.enumLogLevel.Info);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                UpdateExportHistoryTableWithStatus(ehID, "-1");
            }
        }

        public void ActiveLicense(int iLicenseInfoID,int ehID,string Sitecode)
        {
            try
            {

               
                SqlParameter[] sqlParams = new SqlParameter[1];
                LogManager.WriteLog("Export License Details for Code Active License- " + iLicenseInfoID + " Site Code - " + Sitecode + "Started.", LogManager.enumLogLevel.Info);
                sqlParams[0] = new SqlParameter
                {
                    ParameterName = "@LicenseInfoID",
                    Value = iLicenseInfoID,
                    Direction = ParameterDirection.Input
                };

                var xmlExportLicenseactiveDetails = (SqlHelper.ExecuteScalar(GetConnectionString(), CommandType.StoredProcedure, "rsp_SL_ExportActiveLicenseInfo", sqlParams)).ToString();
                bool isCallSuccess = InvokeBgswsAdminWs(xmlExportLicenseactiveDetails, ehID.ToString(), "ACTIVELICENSE", Sitecode);
                if (isCallSuccess)
                {
                    UpdateExportHistoryTableWithStatus(ehID, "100");
                    LogManager.WriteLog("Export License Details for Code Active License - " + iLicenseInfoID + " Site Code -" + Sitecode + "Completed.", LogManager.enumLogLevel.Info);
                }
                else
                {
                    UpdateExportHistoryTableWithStatus(ehID, "-1");
                    LogManager.WriteLog("Export License Details for Code Active License - " + iLicenseInfoID + " Site Code -" + Sitecode + "Failed.", LogManager.enumLogLevel.Info);
                }
               

                
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }


        /// <summary>
        ///  Generate XML for Exporting GameCapping Details.
        /// </summary>
        /// <param name="GameCapID"></param>
        /// <param name="EH_ID"></param>
        /// <param name="SiteCode"></param>
        internal void GetGameCappingParameters(int GameCapID, int EH_ID, string SiteCode)
        {
            try
            {

                LogManager.WriteLog(ConfigManager.Read(Constants.CONSTANT_LOGPATH) + "_Export.txt", "Export GameCapping parameters - " + GameCapID +
                    " Site Code -" + SiteCode + "Started.", LogManager.enumLogLevel.Info);

                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@Site", SiteCode);
                
                string exportAFTData = (SqlHelper.ExecuteScalar(GetConnectionString(),
                                                                 CommandType.StoredProcedure,
                                                                 "rsp_GetGameCappingParametersinXML", sqlParam)).ToString();

                LogManager.WriteLog("XML String " + exportAFTData, LogManager.enumLogLevel.Info);
                if (InvokeBgswsAdminWs(exportAFTData, EH_ID.ToString(), "GAMECAPPING", SiteCode))
                {
                    LogManager.WriteLog("Update the export history status as successfull", LogManager.enumLogLevel.Info);
                    UpdateExportHistoryTableWithStatus(EH_ID, "100");
                }
                else
                {
                    LogManager.WriteLog("Update the export history status as faiure", LogManager.enumLogLevel.Info);
                    UpdateExportHistoryTableWithStatus(EH_ID, "-1");
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                LogManager.WriteLog("Update the export history status as faiure", LogManager.enumLogLevel.Info);
                UpdateExportHistoryTableWithStatus(EH_ID, "-1");
            }
        }

        /// <summary>
        /// Generate XML for Exporting Employee card tracking.
        /// </summary>
        /// <param name="iLicenseInfoID"></param>
        /// <param name="ehID"></param>
        /// <param name="SiteCode"></param>

        internal void GetUserDetails(int UserID, int EH_ID, string SiteCode)
        {
            try
            {

                LogManager.WriteLog(ConfigManager.Read(Constants.CONSTANT_LOGPATH) + "_Export.txt", "Export User Details For Employee - " + UserID +
                    " Site Code -" + SiteCode + "Started.", LogManager.enumLogLevel.Info);

                SqlParameter[] sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter("UserId", UserID);
                sqlParam[1] = new SqlParameter("SiteCode", SiteCode);


                string exportUserData = (SqlHelper.ExecuteScalar(GetConnectionString(),
                                                                 CommandType.StoredProcedure,
                                                                 "rsp_GetEmployeeUsersinXML", sqlParam)).ToString();

                LogManager.WriteLog("XML String " + exportUserData, LogManager.enumLogLevel.Info);
                if (InvokeBgswsAdminWs(exportUserData, EH_ID.ToString(), "USERDETAILS", SiteCode))
                {
                    LogManager.WriteLog("Update the export history status as successfull", LogManager.enumLogLevel.Info);
                    UpdateExportHistoryTableWithStatus(EH_ID, "100");
                }
                else
                {
                    LogManager.WriteLog("Update the export history status as faiure", LogManager.enumLogLevel.Info);
                    UpdateExportHistoryTableWithStatus(EH_ID, "-1");
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                LogManager.WriteLog("Update the export history status as faiure", LogManager.enumLogLevel.Info);
                UpdateExportHistoryTableWithStatus(EH_ID, "-1");
            }
        }

        internal void UpdateExpiryDateForSL()
        {
            try
            {
                DataBaseServiceHandler.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "usp_SL_UpdateExpiryDate");
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        internal void SiteLicensingExpiryWarning(int LastUpdatedSTMAlertHour)
        {
            try
            {
                var sqlParameters = new SqlParameter[1];
                LogManager.WriteLog("STM Export For Site License ", LogManager.enumLogLevel.Info);

                sqlParameters[0] = new SqlParameter
                {
                    ParameterName = "@LastUpdatedSTMAlertHour",
                    Value = LastUpdatedSTMAlertHour,
                    Direction = ParameterDirection.Input
                };
                LogManager.WriteLog("License Expiry Warning - DB", LogManager.enumLogLevel.Info);
                DataBaseServiceHandler.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "usp_SendSiteLicenseExpiryWarning",sqlParameters);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        public void ExportRoute(string SiteCode, int ehID)
        {

            try
            {
                var sqlParameters = new SqlParameter[1];
                LogManager.WriteLog("Export Route for Site Code -" + SiteCode + "Started.", LogManager.enumLogLevel.Info);

                sqlParameters[0] = new SqlParameter
                {
                    ParameterName = "@SiteCode",
                    Value = SiteCode,
                    Direction = ParameterDirection.Input
                };

                var xmlRoute = (SqlHelper.ExecuteScalar(GetConnectionString(), CommandType.StoredProcedure, "rsp_CRMGetRouteAsXML", sqlParameters)).ToString();
                bool isCallSuccess = InvokeBgswsAdminWs(xmlRoute, ehID.ToString(), "EXPORTROUTE", SiteCode);
                if (isCallSuccess)
                {
                    UpdateExportHistoryTableWithStatus(ehID, "100");
                    LogManager.WriteLog("Export Route for Site Code -" + SiteCode + "Completed.", LogManager.enumLogLevel.Info);
                }
                else
                {
                    UpdateExportHistoryTableWithStatus(ehID, "-1");
                    LogManager.WriteLog("Export Route for Site Code -" + SiteCode + "Failed.", LogManager.enumLogLevel.Info);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                UpdateExportHistoryTableWithStatus(ehID, "-1");
            }
        }

        internal void UpdateDisableGameForSL(int iLicenseInfoID)
        {
            var sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter
            {
                ParameterName = "@LicenseInfoID",
                Value = iLicenseInfoID,
                Direction = ParameterDirection.Input
            };
            try
            {
                DataBaseServiceHandler.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "usp_SL_UpdateDisableGame", sqlParameters);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        internal void GetMasterCardtobeReset(string EmployeeCard, int Eh_ID, string SiteCode)
        {
            try
            {

                LogManager.WriteLog(ConfigManager.Read(Constants.CONSTANT_LOGPATH) + "_Export.txt", "Import Master card  Info for - " + EmployeeCard
                    + " Site Code -" + SiteCode + "Started.", LogManager.enumLogLevel.Info);

                SqlParameter[] sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter("@EmpCard", EmployeeCard);
                sqlParam[1] = new SqlParameter("@SiteCode", SiteCode);

                string exportMasterCardData = (SqlHelper.ExecuteScalar(GetConnectionString(),
                                                                 CommandType.StoredProcedure,
                                                                 "rsp_GetMasterCardInfo", sqlParam)).ToString();

                LogManager.WriteLog("XML String " + exportMasterCardData, LogManager.enumLogLevel.Info);
                if (InvokeBgswsAdminWs(exportMasterCardData, Eh_ID.ToString(), "RESETMASTERCARDFLAG", SiteCode))
                {
                    LogManager.WriteLog("Update the export history status as succesfull", LogManager.enumLogLevel.Info);
                    UpdateExportHistoryTableWithStatus(Eh_ID, "100");
                }
                else
                {
                    LogManager.WriteLog("Update the export history status as faiure", LogManager.enumLogLevel.Info);
                    UpdateExportHistoryTableWithStatus(Eh_ID, "-1");
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                LogManager.WriteLog("Update the export history status as faiure", LogManager.enumLogLevel.Info);
                UpdateExportHistoryTableWithStatus(Eh_ID, "-1");
            }
        }

        internal void ExportResetStatus(string SiteID, int ehID, string SiteCode)
        {
            try
            {
                var sqlParameters = new SqlParameter[1];
                LogManager.WriteLog("Export factory reset status for the site - " + SiteID + " Site Code - " + SiteCode + " Started.", LogManager.enumLogLevel.Info);
                sqlParameters[0] = new SqlParameter
                {
                    ParameterName = "@SiteID",
                    Value = SiteID,
                    Direction = ParameterDirection.Input
                };
                var xmlExportVaultDetails = (SqlHelper.ExecuteScalar(GetConnectionString(), CommandType.StoredProcedure, "rsp_getFactoryResetStatus", sqlParameters)).ToString();
                bool isCallSuccess = InvokeBgswsAdminWs(xmlExportVaultDetails, ehID.ToString(), "FACTORYRESET_STATUS", SiteCode);
                if (isCallSuccess)
                {
                    UpdateExportHistoryTableWithStatus(ehID, "100");
                    LogManager.WriteLog("Export factory reset status for the site - " + SiteID + " Site Code - " + SiteCode + " Completed.", LogManager.enumLogLevel.Info);
                }
                else
                {
                    UpdateExportHistoryTableWithStatus(ehID, "-1");
                    LogManager.WriteLog("Export factory reset status for the site - " + SiteID + " Site Code - " + SiteCode + " Failed.", LogManager.enumLogLevel.Info);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                UpdateExportHistoryTableWithStatus(ehID, "-1");
            }
        }
        public bool ExportVaultDrop(int Drop_Id, int ehid, string SiteCode)
        {
            string XMLString = string.Empty;

            try
            {
                LogManager.WriteLog("Export Vault Drop Details for the Drop ID - " + Drop_Id + " Site Code -" + SiteCode + "Started.", LogManager.enumLogLevel.Info);


                XMLString = DataBaseServiceHandler.ExecuteScalar<string>(GetConnectionString(), CommandType.StoredProcedure, "rsp_Vault_ExportDrop",
                          DataBaseServiceHandler.AddParameter<int>("@Drop_Id", DbType.Int32, Drop_Id));

                bool isCallSuccess = InvokeBgswsAdminWs(XMLString, ehid.ToString(), "VAULTDROP", SiteCode);
                if (isCallSuccess)
                {
                    UpdateExportHistoryTableWithStatus(ehid, "100");
                    LogManager.WriteLog("Export  Vault Drop Details for Drop ID - " + Drop_Id + " Site Code -" + SiteCode + "Completed.", LogManager.enumLogLevel.Info);
                }
                else
                {
                    UpdateExportHistoryTableWithStatus(ehid, "-1");
                    LogManager.WriteLog("Export  Vault Drop Details for the Drop ID - " + Drop_Id + " Site Code -" + SiteCode + "Failed.", LogManager.enumLogLevel.Info);
                }

                return false;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("ExportVaultDrop status update Failed to Export: MH ID: " + Drop_Id + " And EH ID : " + ehid, LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);

                return false;
            }
        }
        public bool ExportVaultTransactionReason(int ReasonID, int ehid, string SiteCode)
        {
            string XMLString = string.Empty;

            try
            {
                LogManager.WriteLog("Export Vault Transaction Reason for the Reason ID - " + ReasonID + " Site Code -" + SiteCode + "Started.", LogManager.enumLogLevel.Info);


                XMLString = DataBaseServiceHandler.ExecuteScalar<string>(GetConnectionString(), CommandType.StoredProcedure, "rsp_Vault_ExportTransactionReason",
                          DataBaseServiceHandler.AddParameter<int>("@Reason_Id", DbType.Int32, ReasonID));

                bool isCallSuccess = InvokeBgswsAdminWs(XMLString, ehid.ToString(), "VAULTTRANSACTIONREASON", SiteCode);
                if (isCallSuccess)
                {
                    UpdateExportHistoryTableWithStatus(ehid, "100");
                    LogManager.WriteLog("Export Vault Transaction Reason for the Reason ID - " + ReasonID + " Site Code -" + SiteCode + "Completed.", LogManager.enumLogLevel.Info);
                }
                else
                {
                    UpdateExportHistoryTableWithStatus(ehid, "-1");
                    LogManager.WriteLog("Export Vault Transaction Reason for the Reason ID - " + ReasonID + " Site Code -" + SiteCode + "Failed.", LogManager.enumLogLevel.Info);
                }

                return false;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Export Vault Transaction Reason update Failed to Export: Re " + ReasonID + " And EH ID : " + ehid, LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);

                return false;
            }
        }
        public bool ExportVaultTermination(int Vault_ID, int ehid, string SiteCode)
        {

            
            string XMLString = string.Empty;

            try
            {
                LogManager.WriteLog("Export Vault Terminate Details for the Vault ID - " + Vault_ID + " Site Code -" + SiteCode + "Started.", LogManager.enumLogLevel.Info);


                XMLString = DataBaseServiceHandler.ExecuteScalar<string>(GetConnectionString(), CommandType.StoredProcedure, "rsp_Vault_GetTerminationDetailsForExport",
                          DataBaseServiceHandler.AddParameter<int>("@NGADevice_ID", DbType.Int32, Vault_ID));

                bool isCallSuccess = InvokeBgswsAdminWs(XMLString, ehid.ToString(), "TERMINATEVAULT", SiteCode);
                if (isCallSuccess)
                {
                    UpdateExportHistoryTableWithStatus(ehid, "100");
                    LogManager.WriteLog("Export  Vault Terminate Details for the Vault ID - " + Vault_ID + " Site Code -" + SiteCode + "Completed.", LogManager.enumLogLevel.Info);
                }
                else
                {
                    UpdateExportHistoryTableWithStatus(ehid, "-1");
                    LogManager.WriteLog("Export  Vault Terminate Details for the Vault ID - " + Vault_ID + " Site Code -" + SiteCode + "Failed.", LogManager.enumLogLevel.Info);
                }

                return false;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("ExportVaultTermination status update Failed to Export: MH ID: " + Vault_ID + " And EH ID : " + ehid, LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);

                return false;
            }
        }        
       
        /// <summary>
        ///  Generate XML for Exporting GameCapping Details.
        /// </summary>
        /// <param name="GameCapID"></param>
        /// <param name="EH_ID"></param>
        /// <param name="SiteCode"></param>
        internal void ExportEmpGMUModes(int ModeID, int EH_ID, string SiteCode)
        {
            try
            {
                var sqlParameters = new SqlParameter[1];
               
                sqlParameters[0] = new SqlParameter
                {
                    ParameterName = "@EH_ID",
                    Value = ModeID,
                    Direction = ParameterDirection.Input
                };
                string ExportEmpModeData = (SqlHelper.ExecuteScalar(GetConnectionString(),
                                                                 CommandType.StoredProcedure,
                                                                 "rsp_GetEmpEventsorModesinXML", sqlParameters)).ToString();

                LogManager.WriteLog("XML String " + ExportEmpModeData, LogManager.enumLogLevel.Info);
                if (InvokeBgswsAdminWs(ExportEmpModeData, EH_ID.ToString(), "EMPGMUMODES", SiteCode))
                {
                    LogManager.WriteLog("Update the export history status as successfull", LogManager.enumLogLevel.Info);
                    UpdateExportHistoryTableWithStatus(EH_ID, "100");
                }
                else
                {
                    LogManager.WriteLog("Update the export history status as faiure", LogManager.enumLogLevel.Info);
                    UpdateExportHistoryTableWithStatus(EH_ID, "-1");
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                LogManager.WriteLog("Update the export history status as faiure", LogManager.enumLogLevel.Info);
                UpdateExportHistoryTableWithStatus(EH_ID, "-1");
            }
        }

        /// <summary>
        ///  Generate XML for Exporting EMPGMUEVENTS Details.
        /// </summary>
        /// <param name="EventID"></param>
        /// <param name="EH_ID"></param>
        /// <param name="SiteCode"></param>
        internal void ExportEmpGMUEvents(int EventID, int EH_ID, string SiteCode)
        {
            try
            {
                var sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter
                {
                    ParameterName = "@EH_ID",
                    Value = EventID,
                    Direction = ParameterDirection.Input
                };
                string ExportEmpModeData = (SqlHelper.ExecuteScalar(GetConnectionString(),
                                                                 CommandType.StoredProcedure,
                                                                 "rsp_GetEmpEventsorModesinXML", sqlParameters)).ToString();

                LogManager.WriteLog("XML String " + ExportEmpModeData, LogManager.enumLogLevel.Info);
                if (InvokeBgswsAdminWs(ExportEmpModeData, EH_ID.ToString(), "EMPGMUEVENTS", SiteCode))
                {
                    LogManager.WriteLog("Update the export history status as successfull", LogManager.enumLogLevel.Info);
                    UpdateExportHistoryTableWithStatus(EH_ID, "100");
                }
                else
                {
                    LogManager.WriteLog("Update the export history status as faiure", LogManager.enumLogLevel.Info);
                    UpdateExportHistoryTableWithStatus(EH_ID, "-1");
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                LogManager.WriteLog("Update the export history status as faiure", LogManager.enumLogLevel.Info);
                UpdateExportHistoryTableWithStatus(EH_ID, "-1");
            }
        }

        internal void ExportMailSubscribers(int EmailID, int EH_ID, string SiteCode)
        {
            try
            {

                string ExportMailSubscribersData = (SqlHelper.ExecuteScalar(GetConnectionString(),
                                                                 CommandType.StoredProcedure,
                                                                 "rsp_GetAllEmailSubscribersinXML", null)).ToString();

                LogManager.WriteLog("XML String " + ExportMailSubscribersData, LogManager.enumLogLevel.Info);
                if (InvokeBgswsAdminWs(ExportMailSubscribersData, EH_ID.ToString(), "MAILLIST", SiteCode))
                {
                    LogManager.WriteLog("Update the export history status as successfull", LogManager.enumLogLevel.Info);
                    UpdateExportHistoryTableWithStatus(EH_ID, "100");
                }
                else
                {
                    LogManager.WriteLog("Update the export history status as faiure", LogManager.enumLogLevel.Info);
                    UpdateExportHistoryTableWithStatus(EH_ID, "-1");
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                LogManager.WriteLog("Update the export history status as faiure", LogManager.enumLogLevel.Info);
                UpdateExportHistoryTableWithStatus(EH_ID, "-1");
            }
        }

        internal void ExportMailServerInfo(int EmailID, int EH_ID, string SiteCode)
        {
            try
            {

                string ExportServerInfoData = (SqlHelper.ExecuteScalar(GetConnectionString(),
                                                                 CommandType.StoredProcedure,
                                                                 "rsp_GetAllMailServerInfoinXML", null)).ToString();

                LogManager.WriteLog("XML String " + ExportServerInfoData, LogManager.enumLogLevel.Info);
                if (InvokeBgswsAdminWs(ExportServerInfoData, EH_ID.ToString(), "MAILSERVER", SiteCode))
                {
                    LogManager.WriteLog("Update the export history status as successfull", LogManager.enumLogLevel.Info);
                    UpdateExportHistoryTableWithStatus(EH_ID, "100");
                }
                else
                {
                    LogManager.WriteLog("Update the export history status as faiure", LogManager.enumLogLevel.Info);
                    UpdateExportHistoryTableWithStatus(EH_ID, "-1");
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                LogManager.WriteLog("Update the export history status as faiure", LogManager.enumLogLevel.Info);
                UpdateExportHistoryTableWithStatus(EH_ID, "-1");
            }
        }

        internal void ExportZoneInfo(int zone_ID, int EH_ID, string SiteCode)
        {
            try
            {
                var sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter
                {
                    ParameterName = "@ZoneID",
                    Value = zone_ID,
                    Direction = ParameterDirection.Input
                };
                string ExportZoneInfoData = (SqlHelper.ExecuteScalar(GetConnectionString(),
                                                                 CommandType.StoredProcedure,
                                                                 "rsp_GetZoneInfoinXML", sqlParameters)).ToString();

                LogManager.WriteLog("XML String " + ExportZoneInfoData, LogManager.enumLogLevel.Info);
                if (InvokeBgswsAdminWs(ExportZoneInfoData, EH_ID.ToString(), "DELETEZONE", SiteCode))
                {
                    LogManager.WriteLog("Update the export history status as successfull", LogManager.enumLogLevel.Info);
                    UpdateExportHistoryTableWithStatus(EH_ID, "100");
                }
                else
                {
                    LogManager.WriteLog("Update the export history status as faiure", LogManager.enumLogLevel.Info);
                    UpdateExportHistoryTableWithStatus(EH_ID, "-1");
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                LogManager.WriteLog("Update the export history status as faiure", LogManager.enumLogLevel.Info);
                UpdateExportHistoryTableWithStatus(EH_ID, "-1");
            }
        }
    }
} 