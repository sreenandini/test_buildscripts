using System;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;

using BMC.Business.CashDeskOperator.WebServices;
using BMC.Common;
using BMC.Common.ConfigurationManagement;
using BMC.Common.ExceptionManagement;
using BMC.Common.LogManagement;
using BMC.DataAccess;
using Microsoft.Win32;
using BMC.Security;
using BMC.Common.Utilities;
using BMCIPC;
using BMC.CoreLib.Diagnostics;
using System.Data.Common;
using System.IO;
using System.Xml.Serialization;

namespace BMC.BusinessClasses.BusinessLogic
{
    internal static class DataHelper
    {
        //private static readonly  ExchangeClient Client = new ExchangeClient();

        //private static bool _ack;
        //private static bool _clientAck;
        private static Proxy _proxy;
        private static string _SiteCode;

        public static Regex CollectionRegex =
            new Regex(
                @"<Treasury_Detail(\w|\b|^|$|\B|\n|\r|\t|\s|\S|\W|\d|\D|)*</Treasury_Detail>|<Door_Event(\w|\b|^|$|\B|\n|\r|\t|\s|\S|\W|\d|\D|)*</Door_Event>|<Fault_Event(\w|\b|^|$|\B|\n|\r|\t|\s|\S|\W|\d|\D|)*</Fault_Event>|<Power_Event(\w|\b|^|$|\B|\n|\r|\t|\s|\S|\W|\d|\D|)*</Power_Event>");

        public static StringBuilder CollectionXMLStringBuilder;

        #region STM
        public static DataTable GetRecordsToExportForSTM()
        {

            var dataset = new DataSet();
            try
            {

                dataset = SqlHelper.ExecuteDataset(GetConnectionString(),
                                                   CommandType.StoredProcedure,
                                                   Constants.CONSTANT_RSP_EXPORTSTM);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            return dataset.Tables.Count > 0 ? dataset.Tables[0] : new DataTable();
        }

        public static bool UpdateSTMExportHistoryDetails(int iID, int iStatus, string Result)
        {
            bool retVal;

            var sqlParameters = new SqlParameter[3];
            var oParamID = new SqlParameter
            {
                ParameterName = "ID",
                Value = iID,
                Direction = ParameterDirection.Input
            };
            sqlParameters[0] = oParamID;

            var oParamStatus = new SqlParameter
            {
                ParameterName = "Status",
                Value = iStatus,
                Direction = ParameterDirection.Input
            };
            sqlParameters[1] = oParamStatus;

            var oParamResult = new SqlParameter
            {
                ParameterName = "Result",
                Value = Result,
                Direction = ParameterDirection.Input
            };
            sqlParameters[2] = oParamResult;



            try
            {
                SqlHelper.ExecuteNonQuery(GetConnectionString(),
                                        Constants.CONSTANT_USP_UPDATESTMEXPORTHISTORY,
                                        sqlParameters);
                retVal = true;
            }
            catch (Exception ex)
            {
                retVal = false;
                ExceptionManager.Publish(ex);
            }

            return retVal;
        }
        #endregion

        private static string GetConnectionString()
        {
            // bool bUseHex = true;
            //RegistryKey key;
            string sqlConnect = "";
            ConfigManager.SetConfigurationMode(ConfigManager.ConfigurationMode.AppConfig);
            try
            {
                //key = BMCRegistryHelper.GetRegLocalMachine().OpenSubKey(ConfigManager.Read("RegistryPath"));
                //sqlConnect = key.GetValue("SQLConnect").ToString();
                sqlConnect = BMCRegistryHelper.GetRegKeyValue(ConfigManager.Read("RegistryPath"), "SQLConnect");
                if (!sqlConnect.ToUpper().Contains("SERVER"))
                {
                    //var bgsConstants = new cConstants();
                    //var objDecrypt = new clsBlowFish();
                    //string encryptionkey = bgsConstants.ENCRYPTIONKEY;
                    //sqlConnect = objDecrypt.DecryptString(ref sqlConnect, ref encryptionkey, ref bUseHex);
                    sqlConnect = BMC.Common.Security.CryptEncode.Decrypt(sqlConnect);
                }
                //key.Close();
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return sqlConnect;
        }

        public static DataSet GetAllExportData()
        {
            DataSet objdsAllExportData = null;

            try
            {
                objdsAllExportData = SqlHelper.ExecuteDataset(GetConnectionString(),
                                                              Constants.CONSTANT_USP_ALLEXPORTDATA);

                return objdsAllExportData;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return objdsAllExportData;
            }
        }

        public static bool ResetInProgressEhRecords()
        {
            bool bSuccess;
            try
            {
                SqlHelper.ExecuteScalar(GetConnectionString(),
                                        CommandType.StoredProcedure,
                                        Constants.CONSTANT_RSP_RESETINPROGRESSEHRECORDS);
                bSuccess = true;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                bSuccess = false;
            }

            return bSuccess;
        }

        public static bool GetExportMeterHistoryData(int mhID, string ehid)
        {
            var sqlParameters = new SqlParameter[1];
            var sqlParameter = new SqlParameter
                                   {
                                       ParameterName = "MH_ID",
                                       Value = mhID,
                                       Direction = ParameterDirection.Input
                                   };
            sqlParameters[0] = sqlParameter;

            string xmlExportMeterHistory = SqlHelper.ExecuteScalar(GetConnectionString(),
                                                                   Constants.CONSTANT_USP_EXPORTMETERHISTORY,
                                                                   sqlParameters).ToString();


            if (xmlExportMeterHistory.Trim() == string.Empty)
            {
                LogManager.WriteLog("METER_HISTORY ::" + "GET EXPORT DATA FAILED :: - XML Data Blank MH ID: " + mhID + " And EH ID : " + ehid, LogManager.enumLogLevel.Error);
                return false;
            }
            if (WrapExportXMLData(xmlExportMeterHistory, ehid, "METER_HISTORY"))
                return true;

            throw new Exception("Meter History Record Failed to Export: MH ID: " + mhID + " And EH ID : " + ehid);
        }

        public static bool GetExportReadData(int readID, string ehid, ref string defaultStatus)
        {
            var sqlParameters = new SqlParameter[1];
            var sqlParameter = new SqlParameter
                                   {
                                       ParameterName = "EH_ID",
                                       Value = ehid,
                                       Direction = ParameterDirection.Input
                                   };
            sqlParameters[0] = sqlParameter;

            string xmlExportReadRecord = SqlHelper.ExecuteScalar(GetConnectionString(),
                                                                 CommandType.StoredProcedure,
                                                                 Constants.CONSTANT_USP_EXPORTREADRECORD, sqlParameters)
                .ToString();

            if (string.IsNullOrEmpty(xmlExportReadRecord))
            {
                defaultStatus = "300";
                return true; 
            }

            if (WrapExportXMLData(xmlExportReadRecord, ehid, "DAILY"))
                return true;
            throw new Exception("ExportReadData Failed : Read ID" + readID + " EH ID :" + ehid);
        }

        public static string GetExportInstallationDetails()
        {
            string strXMLExportInstallationDetails = string.Empty;
            string sOutputXML = string.Empty;

            try
            {
                var sqlParameters = new SqlParameter[0];

                strXMLExportInstallationDetails = SqlHelper.ExecuteScalar(GetConnectionString(),
                                                                          Constants.
                                                                              CONSTANT_USP_EXPORTINSTALLATIONDETAILS,
                                                                          sqlParameters).ToString();

                bool isSuccess = InvokeBgswsAdminWs(strXMLExportInstallationDetails,
                                                    Constants.CONSTANT_WEBMETHOD_INSTALLATION_DETAILS, ref sOutputXML);

                if (isSuccess == false)
                    LogManager.WriteLog(" Unable to export the Installation Details ", LogManager.enumLogLevel.Info);
                else
                    LogManager.WriteLog(" Successfully exported the the Installation Details ",
                                        LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return strXMLExportInstallationDetails;
        }

        public static bool GetExportHourlyStatisticsData(string ehid)
        {
            var sqlParameters = new SqlParameter[1];
            var sqlParameter = new SqlParameter
                                   {
                                       ParameterName = "EH_ID",
                                       Value = ehid,
                                       Direction = ParameterDirection.Input
                                   };
            sqlParameters[0] = sqlParameter;

            string strXmlExportHourlyStatistics = SqlHelper.ExecuteScalar(GetConnectionString(),
                                                                          CommandType.StoredProcedure,
                                                                          Constants.CONSTANT_USP_EXPORTHourlyStatistics,
                                                                          sqlParameters).ToString();

            if (strXmlExportHourlyStatistics.Trim().Length > 0)
            {
                bool wsCallSuccess = WrapExportXMLData(strXmlExportHourlyStatistics, ehid, "HOURLY");
                if (wsCallSuccess)
                {
                    LogManager.WriteLog("HOURLY ::" + "EXPORTED HS DATA SUCCESSFULLY ::", LogManager.enumLogLevel.Error);
                    return true;
                }
                throw new Exception("HOURLY ::" + "EXPORTED HS DATA SUCCESSFULLY :: EHID :" + ehid);
            }
            LogManager.WriteLog("HOURLY ::" + "EXPORT HS DATA FAILED :: - XML Data Blank", LogManager.enumLogLevel.Error);
            return false;
        }

        public static bool GetExportSiteEvents(string ehid)
        {
            var sqlParameter = new SqlParameter
                                   {
                                       ParameterName = "EH_ID",
                                       Value = ehid,
                                       Direction = ParameterDirection.Input
                                   };

            var sqlParameters = new SqlParameter[1];
            sqlParameters[0] = sqlParameter;

            string eventXML = SqlHelper.ExecuteScalar(GetConnectionString(),
                                                      Constants.CONSTANT_USP_EXPORTSiteEvents,
                                                      sqlParameters).ToString();

            if (WrapExportXMLData(eventXML, ehid, "LOGSITEEVENT"))
                return true;

            throw new Exception();
        }

        private static bool InvokeBgswsAdminWs(string xmlToSend, string webServiceToInvoke, ref string outputXML)
        {
            bool isCallSuccessful = false;
            try
            {
                _proxy = GetWebService();

                if (webServiceToInvoke == Constants.CONSTANT_WEBMETHOD_HOURLYSTATISTICS)
                {
                    outputXML = _proxy.ImportHourlyStatisticsData(xmlToSend);
                    isCallSuccessful = true;
                }
                else if (webServiceToInvoke == Constants.CONSTANT_WEBMETHOD_IMPORTMETERHISTORY)
                {
                    isCallSuccessful = _proxy.ImportMeterHistory(xmlToSend);
                }
                else if (webServiceToInvoke == Constants.CONSTANT_WEBMETHOD_READRECORD)
                {
                    _proxy.InsertRead(xmlToSend);
                    isCallSuccessful = true;
                }
                else if (webServiceToInvoke == Constants.CONSTANT_WEBMETHOD_SITEEVENTRECORD)
                {
                    outputXML = _proxy.LogSiteEvent(xmlToSend);
                    if (outputXML != "")
                        isCallSuccessful = true;
                }
                else if (webServiceToInvoke == Constants.CONSTANT_WEBMETHOD_INSTALLATION_DETAILS)
                {
                    string strOutXML = _proxy.GetInstallationNumber(xmlToSend);
                    if (strOutXML != "")
                    {
                        UpdateExhchangeWithEnterpriseInstallationNumber(strOutXML);
                        isCallSuccessful = true;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                isCallSuccessful = false;
            }
            return isCallSuccessful;
        }

        private static void UpdateExhchangeWithEnterpriseInstallationNumber(string sXMLInstallationDetails)
        {
            try
            {
                var sqlParameters = new SqlParameter[1];

                var sqlParameter = new SqlParameter
                                       {
                                           ParameterName = "@doc",
                                           Value = sXMLInstallationDetails,
                                           Direction = ParameterDirection.Input
                                       };
                sqlParameters[0] = sqlParameter;

                SqlHelper.ExecuteNonQuery(GetConnectionString(),
                                          CommandType.StoredProcedure,
                                          Constants.CONSTANT_USP_INSTALLATIONWITHXML, sqlParameters).ToString();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        public static bool ExportRemoveInstallation(int ehID, int installationID)
        {
            bool bResult;
            try
            {
                var sqlParameters = new SqlParameter[1];

                var sqlParameter = new SqlParameter
                                       {
                                           ParameterName = "@Installation_No",
                                           Value = installationID,
                                           Direction = ParameterDirection.Input
                                       };
                sqlParameters[0] = sqlParameter;

                string xmlString = SqlHelper.ExecuteScalar(GetConnectionString(),
                                                           CommandType.StoredProcedure,
                                                           "rsp_GetRemoveInstallationXML", sqlParameters).ToString();

                var parameters = new SqlParameter[3];
                parameters[0] = new SqlParameter
                                    {
                                        ParameterName = "XMLData",
                                        Value = xmlString,
                                        Direction = ParameterDirection.Input
                                    };

                parameters[1] = new SqlParameter
                                    {
                                        ParameterName = "TYPE",
                                        Value = "REMOVEINSTALLATION",
                                        Direction = ParameterDirection.Input
                                    };
                parameters[2] = new SqlParameter
                                    {
                                        ParameterName = "EH_ID",
                                        Value = ehID,
                                        Direction = ParameterDirection.Input
                                    };
                xmlString = SqlHelper.ExecuteScalar(GetConnectionString(),
                                                    Constants.CONSTANT_RSP_WRAPSITEDETAILS,
                                                    parameters).ToString();
                xmlString = xmlString.Replace(Environment.NewLine, "");
                xmlString = xmlString.Replace("\r", "");
                xmlString = xmlString.Replace("\n", "");
                xmlString = xmlString.Replace("\r\n", "");
                _proxy = GetWebService();

                if (
                    int.Parse(
                        _proxy.EnhancedEnrollmentForOffline(xmlString, Proxy.InstallationType.RemoveInstallation).ToString()) >
                    0)
                    bResult = true;
                else
                    throw new Exception("Remove INSTALLATION For EHID: " + ehID + " Failed");
            }
            catch (Exception ex)
            {
                bResult = false;
                ExceptionManager.Publish(ex);
            }
            return bResult;
        }

        public static bool ExportConvertOrGmuChangeInstallation(int ehID, int installationID, bool isGmuChange)
        {
            bool bResult;
            try
            {
                Proxy.InstallationType enumInstallationType;
                var oNewParams = new SqlParameter[1];
                var oOldParams = new SqlParameter[1];
                var exportMhParam = new SqlParameter[3];
                var parameterID = new SqlParameter();
                var oNewParam = new SqlParameter();
                var oOldParam = new SqlParameter();
                var oParamResult = new SqlParameter();
                var oParamStatus = new SqlParameter();
                _proxy = GetWebService();

                oNewParam.ParameterName = "@Installation_No";
                oNewParam.Value = installationID;
                oNewParam.Direction = ParameterDirection.Input;
                oNewParams[0] = oNewParam;

                int oldInstallationID = int.Parse(SqlHelper.ExecuteScalar(GetConnectionString(),
                                                                          CommandType.Text,
                                                                          "Select Isnull(Previous_Installation, 0) As Previous_Installation From installation Where Installation_No = " +
                                                                          installationID).ToString());
                if (oldInstallationID == 0)
                    throw new Exception("Invalid Previous Installation No");

                oOldParam.ParameterName = "@Installation_No";
                oOldParam.Value = oldInstallationID;
                oOldParam.Direction = ParameterDirection.Input;
                oOldParams[0] = oOldParam;

                string strNewXML = SqlHelper.ExecuteScalar(GetConnectionString(), CommandType.StoredProcedure,
                                                           "rsp_GetInstallationXML", oNewParams).ToString();
                string strOldXML = SqlHelper.ExecuteScalar(GetConnectionString(), CommandType.StoredProcedure,
                                                           "rsp_GetRemoveInstallationXML", oOldParams).ToString();

                parameterID.ParameterName = "XMLData";
                parameterID.Value = strNewXML + strOldXML;
                parameterID.Direction = ParameterDirection.Input;
                exportMhParam[0] = parameterID;

                oParamResult.ParameterName = "TYPE";
                if (isGmuChange)
                {
                    oParamResult.Value = "GMUCHANGEINSTALLATION";
                    enumInstallationType = Proxy.InstallationType.GmuChange;
                }
                else
                {
                    oParamResult.Value = "CONVERTINSTALLATION";
                    enumInstallationType = Proxy.InstallationType.ConvertInstallation;
                }

                oParamResult.Direction = ParameterDirection.Input;
                exportMhParam[1] = oParamResult;

                oParamStatus.ParameterName = "EH_ID";
                oParamStatus.Value = ehID;
                oParamStatus.Direction = ParameterDirection.Input;
                exportMhParam[2] = oParamStatus;

                string strXML = SqlHelper.ExecuteScalar(GetConnectionString(),
                                                        Constants.CONSTANT_RSP_WRAPSITEDETAILS,
                                                        exportMhParam).ToString();
                strXML = strXML.Replace(Environment.NewLine, "");
                strXML = strXML.Replace("\r", "");
                strXML = strXML.Replace("\n", "");
                strXML = strXML.Replace("\r\n", "");
                int result = int.Parse(_proxy.EnhancedEnrollmentForOffline(strXML, enumInstallationType).ToString());
                LogManager.WriteLog(
                    "(NEW INSTALLATION) Resulting HQ Installation ID For EHID: " + ehID + " is " + result,
                    LogManager.enumLogLevel.Info);
                if (result > 0)
                {
                    SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.Text,
                                              "Update Installation Set HQ_Installation_No = " + result +
                                              " Where Installation_No = " + installationID);
                    bResult = true;
                }
                else
                    throw new Exception("(CONVERT INSTALLATION) Resulting HQ Installation ID For EHID: " + ehID + " is " +
                                        result);
            }
            catch (Exception ex)
            {
                bResult = false;
                ExceptionManager.Publish(ex);
            }
            return bResult;
        }

        public static bool ExportNewInstallation(int ehID, int installationID)
        {
            bool bResult;
            try
            {
                var oParams = new SqlParameter[5];
                oParams[0] = new SqlParameter
                                 {
                                     ParameterName = "@Installation_No",
                                     Value = installationID,
                                     Direction = ParameterDirection.Input
                                 };

                oParams[1] = new SqlParameter
                                 {
                                     ParameterName = "@MACHINECLASSID",
                                     Value = 0,
                                     Direction = ParameterDirection.Input
                                 };

                oParams[2] = new SqlParameter
                                 {
                                     ParameterName = "@SERIALNO",
                                     Value = "",
                                     Direction = ParameterDirection.Input
                                 };

                oParams[3] = new SqlParameter
                                 {
                                     ParameterName = "@ALTERNATESERIALNO",
                                     Value = "",
                                     Direction = ParameterDirection.Input
                                 };

                oParams[4] = new SqlParameter
                                 {
                                     ParameterName = "@isOffLine",
                                     Value = 1,
                                     Direction = ParameterDirection.Input
                                 };


                string xmlString = SqlHelper.ExecuteScalar(GetConnectionString(),
                                                           CommandType.StoredProcedure,
                                                           Constants.CONSTANT_RSP_GETINSTALLATIONXML, oParams).ToString();

                var sqlParameters = new SqlParameter[3];
                var oParamID = new SqlParameter
                                   {
                                       ParameterName = "XMLData",
                                       Value = xmlString,
                                       Direction = ParameterDirection.Input
                                   };
                sqlParameters[0] = oParamID;

                var oParamResult = new SqlParameter
                                       {
                                           ParameterName = "TYPE",
                                           Value = "NEWINSTALLATION",
                                           Direction = ParameterDirection.Input
                                       };
                sqlParameters[1] = oParamResult;

                var oParamStatus = new SqlParameter
                                       {
                                           ParameterName = "EH_ID",
                                           Value = ehID,
                                           Direction = ParameterDirection.Input
                                       };
                sqlParameters[2] = oParamStatus;

                xmlString = SqlHelper.ExecuteScalar(GetConnectionString(),
                                                    Constants.CONSTANT_RSP_WRAPSITEDETAILS,
                                                    sqlParameters).ToString();
                xmlString = xmlString.Replace(Environment.NewLine, "");
                xmlString = xmlString.Replace("\r", "");
                xmlString = xmlString.Replace("\n", "");
                xmlString = xmlString.Replace("\r\n", "");
                _proxy = GetWebService();

                int hqid =
                    int.Parse(
                        _proxy.EnhancedEnrollmentForOffline(xmlString, Proxy.InstallationType.NewInstallation).ToString());

                LogManager.WriteLog(
                    "(NEW INSTALLATION) Resulting HQ Installation ID For EHID: " + ehID + " is " + hqid,
                    LogManager.enumLogLevel.Info);
                if (hqid > 0)
                {
                    SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.Text,
                                              "Update Installation Set HQ_Installation_No = " + hqid +
                                              " Where Installation_No = " + installationID);
                    bResult = true;
                }
                else
                    throw new Exception("(NEW INSTALLATION) Resulting HQ Installation ID For EHID: " + ehID +
                                        " is " + hqid);
            }
            catch (Exception ex)
            {
                bResult = false;
                ExceptionManager.Publish(ex);
            }
            return bResult;
        }

        public static int UpdateExportHistoryTableWithStatus(int ehID, string ehStatus)
        {
            int iNoOfRowsAffected;

            try
            {
                var sqlParameters = new SqlParameter[2];

                var oParam = new SqlParameter
                                 {
                                     ParameterName = "EH_ID",
                                     Value = ehID,
                                     Direction = ParameterDirection.Input
                                 };
                sqlParameters[0] = oParam;

                oParam = new SqlParameter
                             {
                                 ParameterName = "EH_Status",
                                 Value = ehStatus,
                                 Direction = ParameterDirection.Input
                             };
                sqlParameters[1] = oParam;

                iNoOfRowsAffected = SqlHelper.ExecuteNonQuery(GetConnectionString(),
                                                              CommandType.StoredProcedure,
                                                              Constants.CONSTANT_USP_UPDATEEXPORTHISTORY, sqlParameters);

                if ((ehStatus == "-1"))
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

        public static bool GetExportCollectionBatch(String batchID, string ehid)
        {
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

            var oExportCollection = new SqlParameter[1];
            var oParam = new SqlParameter
                             {
                                 ParameterName = "pvcCollection_No",
                                 Value = batchID,
                                 Direction = ParameterDirection.Input
                             };
            oExportCollection[0] = oParam;


            DataSet exportCollectionData = SqlHelper.ExecuteDataset(GetConnectionString(),
                                                                      CommandType.StoredProcedure,
                                                                      Constants.CONSTANT_USP_EXPORTCOLLECTIONBATCH,
                                                                      oExportCollection);
            bool wsCallSuccess = false;
            if (exportCollectionData.Tables.Count > 0)
            {
                LogManager.WriteLog("Exporting Batch Total iteration:" + exportCollectionData.Tables[0].Rows.Count.ToString(), LogManager.enumLogLevel.Info);
                for (int iIndex = 0; iIndex < exportCollectionData.Tables[0].Rows.Count; iIndex++)
                {
                    LogManager.WriteLog("Exporting Batch Iteration Index: " + iIndex.ToString(), LogManager.enumLogLevel.Info);
                    wsCallSuccess = WrapExportXMLData(exportCollectionData.Tables[0].Rows[iIndex][0].ToString(), ehid, "COLLECTION");
                    if (!wsCallSuccess)
                    {
                        LogManager.WriteLog("Error Exporting Batch ", LogManager.enumLogLevel.Error);
                        break;
                    }
                    //Wait befor processing next data(To avoid CPU load) 
                    _Reset.WaitOne(PerItemProcessInterval);
                }
            }


            if (wsCallSuccess)
            {
                LogManager.WriteLog("Updating Collection Details for export", LogManager.enumLogLevel.Error);
                string sReturn = SqlHelper.ExecuteScalar(GetConnectionString(), CommandType.StoredProcedure, "usp_InsertCollectionDetailsExportData", new SqlParameter { ParameterName = "iCollection_Batch_No", Value = batchID, Direction = ParameterDirection.Input }).ToString();
                LogManager.WriteLog("Total Collection Details to Export: " + sReturn, LogManager.enumLogLevel.Error);

                try
                {
                    LogManager.WriteLog("Insert into export history for Batch Export Complete " + sReturn, LogManager.enumLogLevel.Info);
                    var sqlParameters = new SqlParameter[4];

                    var oParameter = new SqlParameter
                    {
                        ParameterName = "Reference1",
                        Value = batchID,
                        Direction = ParameterDirection.Input
                    };
                    sqlParameters[0] = oParameter;

                    oParameter = new SqlParameter
                    {
                        ParameterName = "Reference2",
                        Value = "",
                        Direction = ParameterDirection.Input
                    };
                    sqlParameters[1] = oParameter;

                    oParameter = new SqlParameter
                    {
                        ParameterName = "Type",
                        Value = "BATCHEXPCOMPLETE",
                        Direction = ParameterDirection.Input
                    };
                    sqlParameters[2] = oParameter;

                    SqlHelper.ExecuteNonQuery(GetConnectionString(),
                                                                  CommandType.StoredProcedure,
                                                                  Constants.CONSTANT_USP_EXPORT_HISTORY, sqlParameters);
                    LogManager.WriteLog("Inserted the BATCHEXPCOMPLETE into export history for Batch Export Complete " + sReturn, LogManager.enumLogLevel.Info);

                }
                catch (Exception ex)
                {
                    ExceptionManager.Publish(ex);
                }

                //var exportIndividualCollDetailsData = GetExportIndividualCollDetails(batchID);
                //oXmlDetails.LoadXml(exportIndividualCollDetailsData);

                //var oXmlNodes1 = oXmlDetails.DocumentElement.GetElementsByTagName("TreasuryDetails");
                //var oXmlNodes2 = oXmlDetails.DocumentElement.GetElementsByTagName("PartCollections");
                //var oXmlNodes3 = oXmlDetails.DocumentElement.GetElementsByTagName("DoorEvents");
                //var oXmlNodes4 = oXmlDetails.DocumentElement.GetElementsByTagName("PowerEvents");
                //var oXmlNodes5 = oXmlDetails.DocumentElement.GetElementsByTagName("FaultEvents");
                //var oXmlNodes6 = oXmlDetails.DocumentElement.GetElementsByTagName("AFTTransactions");
                //var oXmlNodes7 = oXmlDetails.DocumentElement.GetElementsByTagName("CashIn1P");
                //var oXmlNodes8 = oXmlDetails.DocumentElement.GetElementsByTagName("CollectionTicket");

                //if ((oXmlNodes1.Count > 0) || (oXmlNodes2.Count > 0) || (oXmlNodes3.Count > 0) || (oXmlNodes4.Count > 0) ||
                //    (oXmlNodes5.Count > 0) || (oXmlNodes6.Count > 0) || (oXmlNodes7.Count > 0) || (oXmlNodes8.Count > 0))
                //{
                //    if (CollectionRegex.Matches(exportIndividualCollDetailsData).Count >
                //        int.Parse(ConfigManager.Read("LimitDetailsCount")))
                //        wsCallSuccess = SplitDetails(exportIndividualCollDetailsData, ehid);
                //    else
                //        wsCallSuccess = WrapExportXMLData(exportIndividualCollDetailsData, ehid,
                //                                           "COLLECTIONDETAILS");
                //}

                LogManager.WriteLog("COLLECTION ::" + "EXPORTED BATCH DATA SUCCESSFULLY(DETAILS PENDING) ::",
                                    LogManager.enumLogLevel.Debug);
                return true;
            }
            throw new Exception();
        }
        public static bool GetExportCollectionDetails(String Collection_no, string ehid)
        {
            bool wsCallSuccess;

            LogManager.WriteLog("COLLECTIONDETAILS" + Collection_no, LogManager.enumLogLevel.Debug);

            var exportIndividualCollDetailsData = GetExportIndividualCollDetails(Collection_no);
            wsCallSuccess = WrapExportXMLData(exportIndividualCollDetailsData, ehid, "COLLECTIONDETAILS");

            if (wsCallSuccess)
            {
                LogManager.WriteLog("COLLECTIONDETAILS EXPORT SUCCESS" + Collection_no, LogManager.enumLogLevel.Debug);
                return true;
            }
            throw new Exception();

        }

        private static bool SplitDetails(string details, string ehid)
        {
            var wsCallSuccess = false;
            CollectionXMLStringBuilder = new StringBuilder();

            var site =
                FormatString("<CollectionDetails>" +
                             Regex.Match(details, @"<Site(\w|\b|^|$|\B|\n|\r|\t|\s|\S|\W|\d|\D|)*</Site>").Value);

            var i = 0;

            var collDetails = int.Parse(ConfigManager.Read("LimitDetailsCount"));

            foreach (Match match in CollectionRegex.Matches(details))
            {
                if (CollectionXMLStringBuilder.Length == 0)
                {
                    CollectionXMLStringBuilder.Append(site);
                    AddStartTags(match.Value);
                }

                if (i != 0)
                {
                    if ((CollectionXMLStringBuilder.ToString().EndsWith("</Door_Event>")) &&
                        !(match.Value.StartsWith("<Door_Event>")))
                    {
                        CollectionXMLStringBuilder.Append("</DoorEvents>");
                        AddStartTags(match.Value);
                    }
                    else if ((CollectionXMLStringBuilder.ToString().EndsWith("</Fault_Event>")) &&
                             !(match.Value.StartsWith("<Fault_Event>")))
                    {
                        CollectionXMLStringBuilder.Append("</FaultEvents>");
                        AddStartTags(match.Value);
                    }
                    else if ((CollectionXMLStringBuilder.ToString().EndsWith("</Power_Event>")) &&
                             !(match.Value.StartsWith("<Power_Event>")))
                    {
                        CollectionXMLStringBuilder.Append("</PowerEvents>");
                        AddStartTags(match.Value);
                    }
                    else if ((CollectionXMLStringBuilder.ToString().EndsWith("</Treasury_Detail>")) &&
                             !(match.Value.StartsWith("<Treasury_Detail>")))
                    {
                        CollectionXMLStringBuilder.Append("</TreasuryDetails>");
                        AddStartTags(match.Value);
                    }
                    else if ((CollectionXMLStringBuilder.ToString().EndsWith("</AFT_Tran>")) &&
                             !(match.Value.StartsWith("<AFT_Tran>")))
                    {
                        CollectionXMLStringBuilder.Append("</AFTTransactions>");
                        AddStartTags(match.Value);
                    }
                    else if ((CollectionXMLStringBuilder.ToString().EndsWith("</Cash_In_1P>")) &&
                             !(match.Value.StartsWith("<Cash_In_1P>")))
                    {
                        CollectionXMLStringBuilder.Append("</CashIn1P>");
                        AddStartTags(match.Value);
                    }
                    else if ((CollectionXMLStringBuilder.ToString().EndsWith("</Col_Ticket>")) &&
                             !(match.Value.StartsWith("<Col_Ticket>")))
                    {
                        CollectionXMLStringBuilder.Append("</CollectionTicket>");
                        AddStartTags(match.Value);
                    }
                }

                CollectionXMLStringBuilder.Append(match.Value);

                if ((i % collDetails == 0) && (i != 0))
                {
                    AddEndTags(match.Value);

                    CollectionXMLStringBuilder.Append("</CollectionDetails>");
                    wsCallSuccess = WrapExportXMLData(CollectionXMLStringBuilder.ToString(), ehid, "COLLECTIONDETAILS");

                    if (!wsCallSuccess)
                        throw new Exception("One part of Collection Details import failed");

                    CollectionXMLStringBuilder = new StringBuilder();
                }
                i++;
            }

            if (CollectionXMLStringBuilder.Length > 0)
            {
                AddEndTags(CollectionXMLStringBuilder.ToString());
                CollectionXMLStringBuilder.Append("</CollectionDetails>");
                wsCallSuccess = WrapExportXMLData(CollectionXMLStringBuilder.ToString(), ehid, "COLLECTIONDETAILS");

                if (!wsCallSuccess)
                    throw new Exception("One part of Collection Details import failed");
            }

            return wsCallSuccess;
        }

        private static string FormatString(string inputString)
        {
            var stringBuilder = new StringBuilder(inputString);
            stringBuilder.Replace(Environment.NewLine, string.Empty);
            stringBuilder.Replace("\r", string.Empty);
            stringBuilder.Replace("\n", string.Empty);
            stringBuilder.Replace("\r\n", string.Empty);
            return stringBuilder.ToString();
        }

        private static void AddStartTags(string strValue)
        {
            if (strValue.StartsWith("<Door_Event>"))
                CollectionXMLStringBuilder.Append("<DoorEvents>");
            else if (strValue.StartsWith("<Fault_Event>"))
                CollectionXMLStringBuilder.Append("<FaultEvents>");
            else if (strValue.StartsWith("<Power_Event>"))
                CollectionXMLStringBuilder.Append("<PowerEvents>");
            else if (strValue.StartsWith("<Treasury_Detail>"))
                CollectionXMLStringBuilder.Append("<TreasuryDetails>");
            else if (strValue.StartsWith("<AFT_Tran>"))
                CollectionXMLStringBuilder.Append("<AFTTransactions>");
            else if (strValue.StartsWith("<Cash_In_1P>"))
                CollectionXMLStringBuilder.Append("<CashIn1P>");
            else if (strValue.StartsWith("<Col_Ticket>"))
                CollectionXMLStringBuilder.Append("<CollectionTicket>");
        }

        private static void AddEndTags(string strvalue)
        {
            if (strvalue.EndsWith("</Door_Event>"))
                CollectionXMLStringBuilder.Append("</DoorEvents>");
            else if (strvalue.EndsWith("</Fault_Event>"))
                CollectionXMLStringBuilder.Append("</FaultEvents>");
            else if (strvalue.EndsWith("</Power_Event>"))
                CollectionXMLStringBuilder.Append("</PowerEvents>");
            else if (strvalue.EndsWith("</Treasury_Detail>"))
                CollectionXMLStringBuilder.Append("</TreasuryDetails>");
            else if (strvalue.StartsWith("<AFT_Tran>"))
                CollectionXMLStringBuilder.Append("<AFTTransactions>");
            else if (strvalue.StartsWith("<Cash_In_1P>"))
                CollectionXMLStringBuilder.Append("<CashIn1P>");
            else if (strvalue.StartsWith("<Col_Ticket>"))
                CollectionXMLStringBuilder.Append("<CollectionTicket>");
        }

        //public static void RefreshExchangeClientComExchange()
        //{
        //    ObjMachineMgr = new MachineManager();
        //}

        public static string GetExportIndividualCollDetails(string Collection_No)
        {
            var strXMLExportIndividualCollDetailsData = string.Empty;

            var sqlParameters = new SqlParameter[1];
            var sqlParameter = new SqlParameter
                                 {
                                     ParameterName = "iCollection_No",
                                     Value = Collection_No,
                                     Direction = ParameterDirection.Input
                                 };
            sqlParameters[0] = sqlParameter;

            strXMLExportIndividualCollDetailsData = (SqlHelper.ExecuteScalar(GetConnectionString(),
                                                                             CommandType.StoredProcedure,
                                                                             Constants.
                                                                                 CONSTANT_USP_EXPORTINDIVIDUALCOLLDETAILS,
                                                                             sqlParameters)).ToString();

            return strXMLExportIndividualCollDetailsData;
        }

        public static DataTable GetRecordsToBeImported()
        {
            var recordsToImport = new DataSet();

            try
            {
                recordsToImport = SqlHelper.ExecuteDataset(GetConnectionString(),
                                                 CommandType.StoredProcedure,
                                                 Constants.CONSTANT_USP_GETRECORDSTOBEIMPORTED);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            return recordsToImport.Tables.Count > 0 ? recordsToImport.Tables[0] : new DataTable();
        }

        public static void UpdateProcessDetailsForImportHistory(int iID, string sResult, int iStatus)
        {
            var sqlParameters = new SqlParameter[3];
            var oParamID = new SqlParameter { ParameterName = "ID", Value = iID, Direction = ParameterDirection.Input };
            sqlParameters[0] = oParamID;

            var oParamResult = new SqlParameter
                                   {
                                       ParameterName = "Result",
                                       Value = sResult,
                                       Direction = ParameterDirection.Input
                                   };
            sqlParameters[1] = oParamResult;

            var oParamStatus = new SqlParameter
                                   {
                                       ParameterName = "Status",
                                       Value = iStatus,
                                       Direction = ParameterDirection.Input
                                   };
            sqlParameters[2] = oParamStatus;

            try
            {
                SqlHelper.ExecuteScalar(GetConnectionString(),
                                        Constants.CONSTANT_USP_DIALUPDATEPROCESSDETAILS,
                                        sqlParameters);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private static Proxy GetWebService()
        {
            if (_proxy == null)
            {
                var siteCode = DataBaseServiceHandler.ExecuteScalar<string>(GetConnectionString(), CommandType.Text,
                                                                            "select code from Site");

                _SiteCode = siteCode;


                _proxy = new Proxy(siteCode);
            }

            _proxy.AuthenticationInformationValue.ExchangePassKey = _proxy.GetExchangePasskey();
            _proxy.AuthenticationInformationValue.EnterprisePassKey = _proxy.GetEnterprisePasskey();

            return _proxy;
        }

        public static bool UpdateSetting(string xmlString)
        {
            var bSuccess = false;
            var sqlParameters = new SqlParameter[1];

            try
            {
                sqlParameters[0] = new SqlParameter("doc", xmlString);
                try
                {
                    var recordInserted = SqlHelper.ExecuteNonQuery(GetConnectionString(),
                                                                   CommandType.StoredProcedure,
                                                                   "usp_ImportSiteSettingsFromXML", sqlParameters);
                    if (recordInserted > 0) bSuccess = true;
                    LogManager.WriteLog("Update Site settings updated in DB " + "  Success value " + bSuccess,
                                        LogManager.enumLogLevel.Info);
                }
                catch (Exception ex1)
                {
                    LogManager.WriteLog("Exception Occured in Inserting setting to DB", LogManager.enumLogLevel.Error);
                    LogManager.WriteLog(ex1.Message, LogManager.enumLogLevel.Error);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                bSuccess = false;
            }
            return bSuccess;
        }

        public static bool WrapExportXMLData(string sXMLData, string ehid, string type)
        {
            try
            {
                _proxy = GetWebService();
                LogManager.WriteLog("Lenght of Initial sXMLData : " + sXMLData.Length, LogManager.enumLogLevel.Info);

                var sqlParameters = new SqlParameter[3];
                var oParamID = new SqlParameter
                                   {
                                       ParameterName = "XMLData",
                                       Value = sXMLData,
                                       Direction = ParameterDirection.Input
                                   };
                sqlParameters[0] = oParamID;

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

                sXMLData = SqlHelper.ExecuteScalar(GetConnectionString(),
                                                   Constants.CONSTANT_RSP_WRAPSITEDETAILS,
                                                   sqlParameters).ToString();
                sXMLData = sXMLData.Replace(Environment.NewLine, "");
                sXMLData = sXMLData.Replace("\r", "");
                sXMLData = sXMLData.Replace("\n", "");
                sXMLData = sXMLData.Replace("\r\n", "");

                return _proxy.ImportData(sXMLData);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return false;
        }

        internal static bool ExportInstallationUpdate(int installationID)
        {
            var oSqlParam = new SqlParameter[1];
            _proxy = GetWebService();
            var oParamID = new SqlParameter
                               {
                                   ParameterName = "InstallationID",
                                   Value = installationID,
                                   Direction = ParameterDirection.Input
                               };
            oSqlParam[0] = oParamID;

            LogManager.WriteLog("InstallationUpdate :" + installationID, LogManager.enumLogLevel.Info);

            var xmlString = SqlHelper.ExecuteScalar(GetConnectionString(),
                                                    Constants.CONSTANT_RSP_GETINSTALLATIONUPDATE, oSqlParam).ToString();
            return _proxy.ImportInstallationUpdate(xmlString);
        }

        internal static int GetExportTreasuryData(string ehid, string type)
        {

            var iEnterpriseTreasuryID = 0;


            try
            {
                var strTreasuryValues = (CheckIfEnterpriseInstallationNumberExists(ehid)).Split(',');
                var enterpriseInstallationNo = Convert.ToInt32(strTreasuryValues[0]);
                var treasuryID = Convert.ToInt32(strTreasuryValues[1]);

                if (enterpriseInstallationNo > 0)
                {
                    var oTreasuryParam = new SqlParameter[1];
                    var oParamID = new SqlParameter
                                       {
                                           ParameterName = "TreasuryNo",
                                           Value = treasuryID,
                                           Direction = ParameterDirection.Input
                                       };
                    oTreasuryParam[0] = oParamID;

                    LogManager.WriteLog("Treasury No" + treasuryID, LogManager.enumLogLevel.Info);
                    var strTreasuryXML = SqlHelper.ExecuteScalar(GetConnectionString(),
                                                                    Constants.CONSTANT_RSP_EXPORTTREASURYDETAILS,
                                                                    oTreasuryParam).ToString();
                    iEnterpriseTreasuryID = InsertTreasuryEntriesinEnterprise(strTreasuryXML, ehid, type);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return iEnterpriseTreasuryID;
        }

        private static int InsertTreasuryEntriesinEnterprise(string strXMLTreasury, string ehid, string type)
        {
            try
            {
                int treasuryID = 0;
                _proxy = GetWebService();
                LogManager.WriteLog("Length of Initial sXMLData : " + strXMLTreasury.Length,
                                    LogManager.enumLogLevel.Info);

                if (strXMLTreasury.Length > 0)
                {
                    var sqlParameters = new SqlParameter[3];
                    var oParamID = new SqlParameter
                                       {
                                           ParameterName = "XMLData",
                                           Value = strXMLTreasury,
                                           Direction = ParameterDirection.Input
                                       };
                    sqlParameters[0] = oParamID;

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

                    var xmlString = SqlHelper.ExecuteScalar(GetConnectionString(),
                                                               Constants.CONSTANT_RSP_WRAPSITEDETAILS,
                                                               sqlParameters).ToString();
                    xmlString = xmlString.Replace(Environment.NewLine, "");
                    xmlString = xmlString.Replace("\r", "");
                    xmlString = xmlString.Replace("\n", "");
                    xmlString = xmlString.Replace("\r\n", "");

                    treasuryID = _proxy.InsertTreasuryEntries(xmlString);
                }
                return treasuryID != 999 ? treasuryID : 0;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error while inserting treasury entries in Enterprise",
                                    LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
                return -99;
            }
        }

        internal static string CheckIfEnterpriseInstallationNumberExists(string ehid)
        {
            var strInstTreasuryID = string.Empty;
            try
            {
                var sqlParameters = new SqlParameter[1];
                var parameter = new SqlParameter
                                   {
                                       ParameterName = "@EHID",
                                       Value = ehid,
                                       Direction = ParameterDirection.Input
                                   };
                sqlParameters[0] = parameter;

                LogManager.WriteLog("Checking if HQ_Installation ID Exists", LogManager.enumLogLevel.Info);
                var objdtTreasury = SqlHelper.ExecuteDataset(GetConnectionString(),
                                                                   Constants.CONSTANT_RSP_ENTERPRISEINSTALLATIONNO,
                                                                   sqlParameters).Tables[0];

                if (objdtTreasury.Rows.Count > 0)
                {
                    foreach (DataRow row in objdtTreasury.Rows)
                    {
                        strInstTreasuryID += row["INSTALLATION NO"] + "," + row["TREASURY NO"];
                    }
                }
                return strInstTreasuryID;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return strInstTreasuryID;
            }
        }

        public static bool UpdateTreasuryIDinExchange(int enterpriseTreasury, string ehid)
        {
            var bUpdateTreasuryStatus = false;
            try
            {
                var oTreasuryInstParam = new SqlParameter[3];
                var oParamID = new SqlParameter
                                   {
                                       ParameterName = "@EnterpriseTreasuryId",
                                       Value = enterpriseTreasury,
                                       Direction = ParameterDirection.Input
                                   };
                oTreasuryInstParam[0] = oParamID;

                var oEhParamID = new SqlParameter
                                     {
                                         ParameterName = "@EHID",
                                         Value = int.Parse(ehid),
                                         Direction = ParameterDirection.Input
                                     };
                oTreasuryInstParam[1] = oEhParamID;


                bUpdateTreasuryStatus = SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure,
                                                                  Constants.CONSTANT_USP_UPDATETREASURYID,
                                                                  oTreasuryInstParam) > 0;
                return bUpdateTreasuryStatus;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return bUpdateTreasuryStatus;
            }
            finally
            {
                if (bUpdateTreasuryStatus)
                    UpdateExportHistoryTableWithStatus(int.Parse(ehid), "100");
                else
                    UpdateExportHistoryTableWithStatus(int.Parse(ehid), "-1");
            }
        }

        public static bool GetMGMDID(int MGMD_ID)
        {
            string MGMDXML = string.Empty;
            _proxy = GetWebService();
            int HQ_MGMD;

            try
            {
                MGMDXML = DataBaseServiceHandler.ExecuteScalar<string>(GetConnectionString(), CommandType.StoredProcedure, "rsp_Export_MGMDInstallation",
                    DataBaseServiceHandler.AddParameter<int>("MGMD_ID", DbType.Int32, MGMD_ID));

                MGMDXML = MGMDXML.Replace(Environment.NewLine, "");
                MGMDXML = MGMDXML.Replace("\r", "");
                MGMDXML = MGMDXML.Replace("\n", "");
                MGMDXML = MGMDXML.Replace("\r\n", "");

                HQ_MGMD = _proxy.GetMultiGameInstallID(MGMDXML);

                if (HQ_MGMD > 0)
                    if (DataBaseServiceHandler.ExecuteNonQuery(GetConnectionString(), CommandType.Text, "UPDATE dbo.MGMD_Installation SET HQ_MGMD_ID = " + HQ_MGMD.ToString() + " WHERE MGMD_ID = " + MGMD_ID.ToString()) > 0)
                        return true;

                return false;

            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                return false;
            }
        }

        public static bool ExportGameDetails(int EH_ID, int Game_ID)
        {
            string GameXML = string.Empty;
            int HQ_Game_ID;
            _proxy = GetWebService();

            try
            {
                GameXML = DataBaseServiceHandler.ExecuteScalar<string>(GetConnectionString(), CommandType.StoredProcedure, "rsp_ExportGameLibraryDetails",
                    DataBaseServiceHandler.AddParameter<int>("Game_ID", DbType.Int32, Game_ID));

                GameXML = GameXML.Replace(Environment.NewLine, "");
                GameXML = GameXML.Replace("\r", "");
                GameXML = GameXML.Replace("\n", "");
                GameXML = GameXML.Replace("\r\n", "");

                HQ_Game_ID = _proxy.GetGameLibraryID(GameXML);

                if (HQ_Game_ID > 0)
                    if (DataBaseServiceHandler.ExecuteNonQuery(GetConnectionString(), CommandType.Text, "UPDATE dbo.Game_Library SET MG_HQ_Game_ID = " + HQ_Game_ID + ", MG_GAME_TITLE = 'Unassigned GameTitle' WHERE MG_Game_ID = " + Game_ID) > 0)
                        return true;
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
            return false;
        }

        public static bool ExportPaytableDetails(int EH_ID, int Paytable_ID)
        {
            string PayTableXML = string.Empty;
            _proxy = GetWebService();

            try
            {
                PayTableXML = DataBaseServiceHandler.ExecuteScalar<string>(GetConnectionString(), CommandType.StoredProcedure, "rsp_Export_PaytableDetails",
                    DataBaseServiceHandler.AddParameter<int>("Paytable_ID", DbType.Int32, Paytable_ID));

                if (_proxy.ImportPaytableDetails(PayTableXML))
                    return true;

                return false;
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                return false;
            }
        }

        public static bool ExportSessionDetails(int EH_ID, int Session_ID)
        {
            string SessionXML = string.Empty;

            try
            {
                //SessionXML = DataBaseServiceHandler.ExecuteScalar<string>(GetConnectionString(), CommandType.StoredProcedure, "rsp_Export_SessionDetails",
                //  DataBaseServiceHandler.AddParameter<int>("Session_ID", DbType.Int32, Session_ID));

                SessionXML = DataBaseServiceHandler.ExecuteXMLReader<string>(GetConnectionString(), CommandType.StoredProcedure, "rsp_Export_SessionDetails",
                  DataBaseServiceHandler.AddParameter<int>("Session_ID", DbType.Int32, Session_ID));

                if (WrapExportXMLData(SessionXML, EH_ID.ToString(), "GAMESESSION"))
                    return true;

                return false;
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                return false;
            }
        }

        #region Import Calendar From Enterprise

        public static bool ImportCalendar(string sXML)
        {
            var bSuccess = false;

            try
            {
                var oArrayParam = new SqlParameter[1];

                var oParam = new SqlParameter
                                 {
                                     ParameterName = "doc",
                                     Value = sXML,
                                     Direction = ParameterDirection.Input
                                 };
                oArrayParam[0] = oParam;
                var recordsInserted = SqlHelper.ExecuteNonQuery(GetConnectionString(),
                                                                CommandType.StoredProcedure,
                                                                Constants.CONSTANT_USP_IMPORTCALENDERDETAILS, oArrayParam);
                if (recordsInserted > 0)
                    bSuccess = true;
                LogManager.WriteLog("ImportCalendar WS " + "  Success value " + bSuccess, LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                bSuccess = false;
            }
            return bSuccess;
        }

        #endregion

        #region Enable/Disable Machine Status

        public static bool MachineStatus(string machineDetails, int exportHistoryID)
        {
            const string xPath = @"/MACHINEENABLEDISABLE";
            var oData = new XmlDocument();
            oData.LoadXml(machineDetails);
            var root = oData.DocumentElement;

            var nodes = root.SelectNodes(xPath);

            var barPosition = nodes[0].SelectSingleNode("BARPOSITION").InnerText;
            var machineOperation = nodes[0].SelectSingleNode("ACTION").InnerText;
            LogManager.WriteLog(" Inside MachinStatus New Log: BarPosNo: " + barPosition + " ---- machineOperation: " + machineOperation + " --- ExportHistoryID: " + exportHistoryID, LogManager.enumLogLevel.Info);
            //if (ChangeMachineStatus(barPosition, machineOperation.ToUpper() == "ENABLE" ? true : false))
            //{
            //    _proxy = GetWebService();
            //    if (_proxy.UpdateBarPositionCentralStatusBySiteID(exportHistoryID))
            if (machineOperation.ToUpper() == "ENABLE")
                UpdateMachineStatusinExchange(barPosition, true, true, exportHistoryID);
            else
                UpdateMachineStatusinExchange(barPosition, true, false, exportHistoryID);
            //    else
            //        throw new Exception("Error updating to Web Service. While " + machineOperation +
            //                            "ing the machine in Bar Position :" + barPosition);
            //}
            //else
            //{
            //    throw new Exception("Unable to " + machineOperation + " the machine in Bar Position :" + barPosition);
            //}
            return true;
        }

        public static bool MachineAcceptorStatus(string machineDetails, int exportHistoryID)
        {
            const string xPath = @"/MACHINENOTEACCEPTOR";

            var oData = new XmlDocument();
            oData.LoadXml(machineDetails);
            var root = oData.DocumentElement;

            var nodes = root.SelectNodes(xPath);

            var barPosition = nodes[0].SelectSingleNode("BARPOSITION").InnerText;
            var machineOperation = nodes[0].SelectSingleNode("ACTION").InnerText;
            LogManager.WriteLog(" Inside MachineAcceptorStatus New Log: BarPosNo: " + barPosition + " ---- machineOperation: " + machineOperation + " --- ExportHistoryID: " + exportHistoryID, LogManager.enumLogLevel.Info);
            //if (ChangeMachineAcceptorStatus(barPosition, machineOperation.ToUpper() == "ENABLE" ? true : false))
            //{
            //    _proxy = GetWebService();
            //    if (_proxy.UpdateBarPositionCentralStatusBySiteID(exportHistoryID))
            if (machineOperation.ToUpper() == "ENABLE")
                UpdateMachineStatusinExchange(barPosition, false, true, exportHistoryID);
            else
                UpdateMachineStatusinExchange(barPosition, false, false, exportHistoryID);
            //    else
            //        throw new Exception("Error updating to Web Service. While " + machineOperation +
            //                            "ing the machine in Bar Position :" + barPosition);
            //}
            //else
            //{
            //    throw new Exception("Unable to " + machineOperation + " the machine in Bar Position :" + barPosition);
            //}
            return true;
        }


        //private static void client_ACK(MessageAck messageAck)
        //{
        //    try
        //    {
        //        _ack = messageAck.ACK;
        //        _clientAck = true;
        //    }
        //    catch (Exception)
        //    {
        //        LogManager.WriteLog("exception" + messageAck.ACK, LogManager.enumLogLevel.Info);
        //    }

        //    LogManager.WriteLog("MessageAck.ACK" + messageAck.ACK, LogManager.enumLogLevel.Info);
        //}

        //public static void client_ServerUpdate()
        //{
        //    Client.RefreshActiveUPDs("");
        //}

        //public static bool SendSector201_Comexchange(ref string sCommandText, ref int lCommand, ref byte[] bytArray,
        //                                             ref int lLength, int lDataPak)
        //{
        //    bool sendSector201ComexchangeReturn;
        //    var oSector201 = new Sector201Data();
        //    DateTime dStartTime = DateTime.Now;
        //    bool bTimedOut = false;

        //    try
        //    {
        //        LogManager.WriteLog("[" + sCommandText + "] " + "Started", LogManager.enumLogLevel.Info);

        //        if (lDataPak == 0)
        //        {
        //            return false;
        //        }


        //        oSector201.Command = (byte) lCommand;
        //        oSector201.PutCommandDataVB(bytArray);


        //        Client.RequestExWriteSector(lDataPak, 201, oSector201);


        //        while ((!_clientAck) && (!bTimedOut))
        //        {
        //            Application.DoEvents();
        //            if (DateTime.Now > dStartTime.AddSeconds(15))
        //            {
        //                bTimedOut = true;
        //                LogManager.WriteLog("Timed out : " + lDataPak, LogManager.enumLogLevel.Info);
        //            }
        //        }

        //        if (_ack)
        //        {
        //            LogManager.WriteLog("201 command success", LogManager.enumLogLevel.Info);
        //            sendSector201ComexchangeReturn = true;
        //        }
        //        else
        //        {
        //            LogManager.WriteLog("201 command failure", LogManager.enumLogLevel.Info);
        //            sendSector201ComexchangeReturn = false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LogManager.WriteLog("[" + sCommandText + "] " + ex.Message, LogManager.enumLogLevel.Info);
        //        sendSector201ComexchangeReturn = false;
        //    }
        //    finally
        //    {
        //        if (Marshal.IsComObject(oSector201))
        //        {
        //            Marshal.ReleaseComObject(oSector201);
        //        }
        //        _clientAck = false;
        //        _ack = false;
        //    }
        //    return sendSector201ComexchangeReturn;
        //}


        private static void UpdateMachineStatusinExchange(string strBarposNo, bool bMachine, bool bStatus, int exportHistoryId)
        {
            var objParam = new SqlParameter[4];
            try
            {
                var sqlParameter = new SqlParameter
                                      {
                                          ParameterName = "BarPosNo",
                                          Value = strBarposNo,
                                          Direction = ParameterDirection.Input
                                      };
                objParam[0] = sqlParameter;

                sqlParameter = new SqlParameter
                                   {
                                       ParameterName = "isMachine",
                                       Value = bMachine,
                                       Direction = ParameterDirection.Input,
                                       SqlDbType = SqlDbType.Bit
                                   };
                objParam[1] = sqlParameter;

                sqlParameter = new SqlParameter
                                   {
                                       ParameterName = "Status",
                                       Value = bStatus,
                                       SqlDbType = SqlDbType.Bit,
                                       Direction = ParameterDirection.Input
                                   };
                objParam[2] = sqlParameter;


                sqlParameter = new SqlParameter
                                   {
                                       ParameterName = "exportHistory",
                                       Value = exportHistoryId,
                                       SqlDbType = SqlDbType.Int,
                                       Direction = ParameterDirection.Input
                                   };
                objParam[3] = sqlParameter;

                SqlHelper.ExecuteNonQuery(GetConnectionString(),
                                          CommandType.StoredProcedure,
                                          "usp_UpdateBarPositionForMachineControl",
                                          objParam);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog(ex.ToString(), LogManager.enumLogLevel.Error);
            }
        }

        #endregion

        #region Import Site Details From Enterprise

        public static bool ImportSite(string sXML)
        {
            var bSuccess = false;

            try
            {
                var oArrayParam = new SqlParameter[1];

                var xdoc = new XmlDocument();
                xdoc.LoadXml(sXML);
                var hashValue = xdoc.DocumentElement.SelectSingleNode(@"/SITESETUP/SITE/SitePassKey").InnerText;

                //RegistryKey key;
                ConfigManager.SetConfigurationMode(ConfigManager.ConfigurationMode.AppConfig);
                try
                {
                    //key = BMCRegistryHelper.GetRegLocalMachine().OpenSubKey(ConfigManager.Read("RegistryPath"), true);
                    //key.SetValue("HashValue", hashValue);
                    //key.Close();
                    BMCRegistryHelper.SetRegKeyValue(ConfigManager.Read("RegistryPath"), "HashValue", RegistryValueKind.String, hashValue);

                }
                catch (Exception ex)
                {
                    ExceptionManager.Publish(ex);
                }

                var oParam = new SqlParameter
                                 {
                                     ParameterName = "doc",
                                     Value = sXML,
                                     Direction = ParameterDirection.Input
                                 };
                oArrayParam[0] = oParam;
                int recordsInserted = SqlHelper.ExecuteNonQuery(GetConnectionString(),
                                                                CommandType.StoredProcedure, Constants.CONSTANT_USP_IMPORTSITEDETAILS, oArrayParam);

                if (recordsInserted > 0)
                    bSuccess = true;

                LogManager.WriteLog("ImportSite WS " + "  Success value " + bSuccess, LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                bSuccess = false;
            }
            return bSuccess;
        }

        #endregion

        #region Import Model Details From Enterprise

        public static bool ImportModel(string sXML)
        {
            bool bSuccess = false;
            try
            {
                var oArrayParam = new SqlParameter[1];
                var oParam = new SqlParameter
                                 {
                                     ParameterName = "doc",
                                     Value = sXML,
                                     Direction = ParameterDirection.Input
                                 };
                oArrayParam[0] = oParam;
                int recordsInserteed = SqlHelper.ExecuteNonQuery(GetConnectionString(),
                                                                 CommandType.StoredProcedure,
                                                                 Constants.CONSTANT_USP_IMPORTMODELDETAILS,
                                                                 oArrayParam);
                if (recordsInserteed > 0)
                    bSuccess = true;
                LogManager.WriteLog("ImportModel WS " + "  Success value " + bSuccess, LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                bSuccess = false;
            }
            return bSuccess;
        }

        #endregion

        #region Auto Installation Import
        //public static bool ImportInstallation(string sXML)
        //{
        //    bool bSuccess = false;

        //    try
        //    {
        //        ///Store the installation data in database
        //        var oArrayParam = new SqlParameter[2];
        //        var oParam = new SqlParameter
        //        {
        //            ParameterName = "doc",
        //            Value = sXML,
        //            Direction = ParameterDirection.Input
        //        };
        //        var outParam = new SqlParameter
        //        {
        //            ParameterName = "@Installation_No",
        //            Value = 0,
        //            Direction = ParameterDirection.Output
        //        };
        //        oArrayParam[0] = oParam;
        //        oArrayParam[1] = outParam;

        //        int recordsInserted = SqlHelper.ExecuteNonQuery(GetConnectionString(),
        //                                                         CommandType.StoredProcedure,
        //                                                         "usp_ImportInstallation",//Constants.CONSTANT_USP_IMPORTINSTALLATION,
        //                                                         oArrayParam);
        //        LogManager.WriteLog("ImportInstallation usp_ImportInstallation return value: " + recordsInserted, LogManager.enumLogLevel.Info);
        //        int InstallationNO = Int32.Parse(oArrayParam[1].Value.ToString());

        //        if (InstallationNO > 0)//For new installation
        //        {
        //            //ADD UDP to POLLING LIST
        //            bSuccess = true;
        //            int Bar_Pos_Port, Polling_ID, PollType;
        //            String Server = "";
        //            Polling_ID = 0;
        //            PollType = 7;



        //            var oArrayParamInstall = new SqlParameter[1];
        //            var oParamInstall = new SqlParameter
        //            {
        //                ParameterName = "@Installation_No",
        //                Value = InstallationNO,
        //                Direction = ParameterDirection.Input
        //            };
        //            oArrayParamInstall[0] = oParamInstall;

        //            //Get the details like Port number, polling id etc...
        //            DataSet dsInstallation = SqlHelper.ExecuteDataset(GetConnectionString(), CommandType.StoredProcedure, "rsp_GetInstallationdetails", oArrayParamInstall);
        //            if (dsInstallation.Tables[0].Rows.Count > 0)
        //            {
        //                Bar_Pos_Port = Int32.Parse(dsInstallation.Tables[0].Rows[0]["Bar_Pos_Port"].ToString());
        //                //Add the Installation Number to Polling list                    
        //                bool Success = ObjMachineMgr.AddUDPToList(InstallationNO, Bar_Pos_Port, PollType, Polling_ID, "");
        //                LogManager.WriteLog("ImportInstallation ADD UDP TO LIST for Installation No" + recordsInserted + "  Success value " + bSuccess, LogManager.enumLogLevel.Info);
        //            }
        //            else
        //            {
        //                bool Success = ObjMachineMgr.RemoveUDPFromList(InstallationNO);
        //                LogManager.WriteLog("ImportInstallation REMOVE FROM LIST for Installation No" + recordsInserted + "  Success value " + bSuccess, LogManager.enumLogLevel.Info);

        //            }

        //        }
        //        //else if (recordsInserted == 1)//Removal of installation
        //        //{
        //        //    bSuccess = true;
        //        //    bool Success = ObjMachineMgr.RemoveUDPFromList(InstallationNO);
        //        //    LogManager.WriteLog("ImportInstallation REMOVE FROM LIST for Installation No" + recordsInserted + "  Success value " + bSuccess, LogManager.enumLogLevel.Info);

        //        //}
        //        else
        //        {
        //            bSuccess = false;
        //            LogManager.WriteLog("ImportInstallation WS Other CODE!!! " + "  Success value " + bSuccess, LogManager.enumLogLevel.Info);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ExceptionManager.Publish(ex);
        //        bSuccess = false;
        //    }
        //    return bSuccess;

        //}
        #endregion

        public static bool ExportInstallationGameInfo(int Installation_No)
        {
            string GameInfoXML = string.Empty;
            bool ExportStatus = false;
            _proxy = GetWebService();

            try
            {
                //Create the xml for InstallationGameInfo
                GameInfoXML = DataBaseServiceHandler.ExecuteScalar<string>(GetConnectionString(), CommandType.StoredProcedure,
                    "rsp_ExportInstallationGameInfo",
                    DataBaseServiceHandler.AddParameter<int>("InstallationNo", DbType.Int32, Installation_No));

                if (string.IsNullOrEmpty(GameInfoXML))
                    return true;

                GameInfoXML = GameInfoXML.Replace(Environment.NewLine, "");
                GameInfoXML = GameInfoXML.Replace("\r", "");
                GameInfoXML = GameInfoXML.Replace("\n", "");
                GameInfoXML = GameInfoXML.Replace("\r\n", "");

                LogManager.WriteLog("Game Export XML " + GameInfoXML, LogManager.enumLogLevel.Info);

                //insert the game info in corresponding Enteprise
                ExportStatus = _proxy.InsertInstallationGameInfo(GameInfoXML);
                LogManager.WriteLog("Status for Game Export " + ExportStatus.ToString(), LogManager.enumLogLevel.Info);

                if (ExportStatus)
                {
                    //if successfull, remove the deleted games from Installation game info table in exchange
                    DataBaseServiceHandler.ExecuteScalar<string>(GetConnectionString(), CommandType.StoredProcedure,
                    "usp_CheckChangedDefaultGame",
                    DataBaseServiceHandler.AddParameter<int>("Installation_No", DbType.Int32, Installation_No));
                    LogManager.WriteLog("removed the games succesfully ", LogManager.enumLogLevel.Info);

                }
                return ExportStatus;

            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                return false;
            }
        }

        public static bool ExportMachineClass(int EH_ID, int Machine_Class_No)
        {
            string MCXml = string.Empty;

            try
            {
                if (_proxy == null)
                    _proxy = GetWebService();

                MCXml = DataBaseServiceHandler.ExecuteScalar<string>(GetConnectionString(), CommandType.StoredProcedure, "rsp_ExportNewMachineClass",
                            DataBaseServiceHandler.AddParameter<int>("Machine_Class_No", DbType.Int32, Machine_Class_No));

                MCXml = MCXml.Replace(Environment.NewLine, "");
                MCXml = MCXml.Replace("\r", "");
                MCXml = MCXml.Replace("\n", "");
                MCXml = MCXml.Replace("\r\n", "");

                if (_proxy.UpdateDetailsFromXML("MACHINECLASS", MCXml))
                    return true;

                return false;
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                return false;
            }
        }

        public static bool ExportMachineDetails(int EH_ID, int Machine_No)
        {
            string MCXml = string.Empty;

            try
            {
                if (_proxy == null)
                    _proxy = GetWebService();

                MCXml = DataBaseServiceHandler.ExecuteScalar<string>(GetConnectionString(), CommandType.StoredProcedure, "rsp_ExportMachine",
                            DataBaseServiceHandler.AddParameter<int>("MachineNo", DbType.Int32, Machine_No));

                MCXml = MCXml.Replace(Environment.NewLine, "");
                MCXml = MCXml.Replace("\r", "");
                MCXml = MCXml.Replace("\n", "");
                MCXml = MCXml.Replace("\r\n", "");

                if (_proxy.UpdateDetailsFromXML("MACHINE", MCXml))
                    return true;

                return false;
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                return false;
            }
        }

        public static bool ExportGameChangeForInstallation(int InstallationID)
        {
            string XMLString = string.Empty;
            try
            {
                if (_proxy == null)
                    _proxy = GetWebService();

                XMLString = DataBaseServiceHandler.ExecuteScalar<string>(GetConnectionString(), CommandType.StoredProcedure, "rsp_getInstallationGameinXML",
                            DataBaseServiceHandler.AddParameter<int>("@InstallationNo", DbType.Int32, InstallationID));
                XMLString = XMLString.Replace(Environment.NewLine, "");
                XMLString = XMLString.Replace("\r", "");
                XMLString = XMLString.Replace("\n", "");
                XMLString = XMLString.Replace("\r\n", "");
                return _proxy.UpdateDetailsFromXML("GAMECHANGE", XMLString);
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                return false;
            }
        }

        public static bool CloseInstallation(int InstallationID)
        {
            string XMLString = string.Empty;
            try
            {
                if (_proxy == null)
                    _proxy = GetWebService();

                XMLString = DataBaseServiceHandler.ExecuteScalar<string>(GetConnectionString(), CommandType.StoredProcedure, "rsp_GetCloseInstallationXML",
                            DataBaseServiceHandler.AddParameter<int>("@InstallationNo", DbType.Int32, InstallationID));
                XMLString = XMLString.Replace(Environment.NewLine, "");
                XMLString = XMLString.Replace("\r", "");
                XMLString = XMLString.Replace("\n", "");
                XMLString = XMLString.Replace("\r\n", "");

                MachineManagerInterface machineManagerInterface = new MachineManagerInterface();

                if (!machineManagerInterface.RemoveUDPFromList(InstallationID))
                    return false;

                DataBaseServiceHandler.ExecuteNonQuery(GetConnectionString(), CommandType.Text, "DELETE FROM dbo.GMU_Login WHERE GL_Code = " + InstallationID.ToString());

                LogManager.WriteLog("CloseInstallation REMOVE FROM LIST for Installation No" + InstallationID.ToString() + "  Succeeded", LogManager.enumLogLevel.Info);

                if (_proxy.UpdateDetailsFromXML("CLOSEINSTALLATION", XMLString))
                    return true;
                //bool bSuccess = ObjMachineMgr.RemoveUDPFromList(InstallationID);


                return false;
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                return false;
            }
        }

        public static bool ExportInstallationDenomChange(int InstallationID)
        {
            string XMLString = string.Empty;
            int HQInstallationID = 0;
            try
            {
                if (_proxy == null)
                    _proxy = GetWebService();

                XMLString = DataBaseServiceHandler.ExecuteScalar<string>(GetConnectionString(), CommandType.StoredProcedure, "rsp_GetDenomChangeinXML",
                            DataBaseServiceHandler.AddParameter<int>("@InstallationNo", DbType.Int32, InstallationID));
                XMLString = XMLString.Replace(Environment.NewLine, "");
                XMLString = XMLString.Replace("\r", "");
                XMLString = XMLString.Replace("\n", "");
                XMLString = XMLString.Replace("\r\n", "");
                XmlDocument oXmlDetails = new XmlDocument();
                oXmlDetails.LoadXml(XMLString.Trim());
                string oXmlNodes2 = oXmlDetails.DocumentElement.GetElementsByTagName("Bar_Pos_No").Item(0).InnerText;
                string oXmlNodes1 = oXmlDetails.DocumentElement.GetElementsByTagName("HQ_Installation_No").Item(0).InnerText;

                MachineManagerInterface machineManagerInterface = new MachineManagerInterface();
                machineManagerInterface.AddUDPToList(InstallationID, Convert.ToInt32(oXmlNodes2));


                if (Convert.ToInt32(oXmlNodes1) == 0)
                {
                    HQInstallationID = _proxy.UpdateDenomChange(XMLString.Trim());
                    if (HQInstallationID > 0)
                    {
                        DataBaseServiceHandler.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "usp_updateHQInstallationID",
                        DataBaseServiceHandler.AddParameter<int>("@InstallationNo", DbType.Int32, InstallationID),
                        DataBaseServiceHandler.AddParameter<int>("@HQInstallationNo", DbType.Int32, HQInstallationID));
                    }
                    else
                        return false;
                }
                //else
                //{
                //    HQInstallationID = Convert.ToInt32(oXmlNodes1);
                //}
                //if (HQInstallationID>0 )
                //{
                //    ObjMachineMgr.RefreshUDPList();
                //    return ObjMachineMgr.AddUDPToList(InstallationID, Convert.ToInt32(oXmlNodes2), 7, 0, "");
                //}
                return true;
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                return false;
            }
        }
        public static bool ExportInstallationStatusUpdate(int InstallationID)
        {
            string XMLString = string.Empty;
            try
            {
                _proxy = GetWebService();

                XMLString = DataBaseServiceHandler.ExecuteScalar<string>(GetConnectionString(), CommandType.StoredProcedure, "rsp_GetInstallationStatusUpdateInXML",
                           DataBaseServiceHandler.AddParameter<int>("@InstallationNo", DbType.Int32, InstallationID));
                return _proxy.UpdateDetailsFromXML("INSTALLATIONSTATUSUPDATE", XMLString);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return false;
            }
        }

        public static bool ExportChangePassword(int UserID)
        {
            string XMLString = string.Empty;
            try
            {
                _proxy = GetWebService();
                XMLString = DataBaseServiceHandler.ExecuteScalar<string>(GetConnectionString(), CommandType.StoredProcedure, "rsp_ExportUserInfoDetails",
                           DataBaseServiceHandler.AddParameter<int>("@User_ID", DbType.Int32, UserID));

                var siteCode = DataBaseServiceHandler.ExecuteScalar<string>(GetConnectionString(), CommandType.Text,
                                                                        "select code from Site");

                // return _proxy.UpdateDetailsFromXML("CHANGEPASSWORD", XMLString);
                return _proxy.ImportPasswordChange(XMLString, UserID, siteCode, "CHANGEPASSWORD");
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return false;
            }
        }

        public static bool ExportVerificationStatus(string Reference, string Type)
        {
            string XMLString = string.Empty;

            try
            {
                _proxy = GetWebService();

                XMLString = DataBaseServiceHandler.ExecuteScalar<string>(GetConnectionString(), CommandType.StoredProcedure, "rsp_GetVLTDetailsinXML",
                                DataBaseServiceHandler.AddParameter<string>("@Serial", DbType.String, Reference),
                                DataBaseServiceHandler.AddParameter<string>("@MessageAAMSID", DbType.String, ""),
                                DataBaseServiceHandler.AddParameter<string>("@Type", DbType.String, Type));

                if (XMLString != "")
                    return _proxy.UpdateDetailsFromXML("VLTVERIFICATION", XMLString);
                else
                    LogManager.WriteLog("Data for VLT Verification is empty", LogManager.enumLogLevel.Info);

                return false;
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                return false;
            }
        }

        public static bool ExportAssetBADStatus(int BADReferenceID)
        {
            string XMLString = string.Empty;

            try
            {
                if (_proxy == null)
                {
                    _proxy = GetWebService();
                }

                XMLString = DataBaseServiceHandler.ExecuteScalar<string>(GetConnectionString(), CommandType.StoredProcedure, "rsp_GetBADAAMSStatusInXML",
                           DataBaseServiceHandler.AddParameter<int>("@BADReferenceID", DbType.Int32, BADReferenceID));

                return _proxy.UpdateDetailsFromXML("AAMSCONFIG", XMLString);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return false;
            }
        }

        public static bool ExportDetailsforMachineMaintenance(int eh_id, int MachineMaintenanceNo)
        {
            string XMLString = string.Empty;

            try
            {
                if (_proxy == null)
                {
                    _proxy = GetWebService();
                }

                XMLString = DataBaseServiceHandler.ExecuteScalar<string>(GetConnectionString(), CommandType.StoredProcedure, "rsp_GetDetailsforMachineMaintenanceinXML",
                           DataBaseServiceHandler.AddParameter<int>("@MachineMaintenanceNo", DbType.Int32, MachineMaintenanceNo));

                return WrapExportXMLData(XMLString, eh_id.ToString(), "MACHINEMAINTENANCE");

                //return _proxy.UpdateDetailsFromXML("MACHINEMAINTENANCE", XMLString); 
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return false;
            }
        }

        public static bool ExportMaintenanceSession(int eh_id, int MaintenanceSessionID)
        {
            string XMLString = string.Empty;

            try
            {
                if (_proxy == null)
                {
                    _proxy = GetWebService();
                }

                XMLString = DataBaseServiceHandler.ExecuteScalar<string>(GetConnectionString(), CommandType.StoredProcedure, "rsp_GetMaintenanceSession",
                           DataBaseServiceHandler.AddParameter<int>("@MaintenanceSessionID", DbType.Int32, MaintenanceSessionID));

                return WrapExportXMLData(XMLString, eh_id.ToString(), "MAINTENANCESESSION");

                //return _proxy.UpdateDetailsFromXML("MAINTENANCESESSION", XMLString);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return false;
            }
        }

        public static bool ExportMaintenanceHistory(int eh_id, int MaintenanceHistoryID)
        {
            string XMLString = string.Empty;

            try
            {
                if (_proxy == null)
                {
                    _proxy = GetWebService();
                }

                XMLString = DataBaseServiceHandler.ExecuteScalar<string>(GetConnectionString(), CommandType.StoredProcedure, "rsp_GetMaintenanceHistory",
                           DataBaseServiceHandler.AddParameter<int>("@MaintenanceHistoryID", DbType.Int32, MaintenanceHistoryID));

                return WrapExportXMLData(XMLString, eh_id.ToString(), "MAINTENANCEHISTORY");

                //return _proxy.UpdateDetailsFromXML("MAINTENANCEHISTORY", XMLString);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return false;
            }
        }

        public static bool ExportMaintenanceReasonCategory(int eh_id, int MaintenanceReasonCategoryID)
        {
            string XMLString = string.Empty;

            try
            {
                if (_proxy == null)
                {
                    _proxy = GetWebService();
                }

                XMLString = DataBaseServiceHandler.ExecuteScalar<string>(GetConnectionString(), CommandType.StoredProcedure, "rsp_GetMaintenanceReasonCategory",
                           DataBaseServiceHandler.AddParameter<int>("@MaintenanceReasonCategoryID", DbType.Int32, MaintenanceReasonCategoryID));

                return WrapExportXMLData(XMLString, eh_id.ToString(), "MAINTENANCEREASONCATEGORY");

                //return _proxy.UpdateDetailsFromXML("MAINTENANCEREASONCATEGORY", XMLString);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return false;
            }
        }

        public static bool ExportAuditHistory(int AuditHistoryID)
        {
            string XMLString = string.Empty;

            try
            {
                if (_proxy == null)
                {
                    _proxy = GetWebService();
                }

                XMLString = DataBaseServiceHandler.ExecuteScalar<string>(GetConnectionString(), CommandType.StoredProcedure, "rsp_GetAuditHistory",
                           DataBaseServiceHandler.AddParameter<int>("@AuditHistoryID", DbType.Int32, AuditHistoryID));

                return _proxy.UpdateDetailsFromXML("AUDIT", XMLString);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return false;
            }
        }

        public static bool ExportAFTAuditHistory(int AFTAuditHistoryID)
        {
            string XMLString = string.Empty;

            try
            {
                if (_proxy == null)
                {
                    _proxy = GetWebService();
                }

                XMLString = DataBaseServiceHandler.ExecuteScalar<string>(GetConnectionString(), CommandType.StoredProcedure, "rsp_GetAFTAuditHistory",
                           DataBaseServiceHandler.AddParameter<int>("@AFTAuditHistoryID", DbType.Int32, AFTAuditHistoryID));

                return _proxy.UpdateDetailsFromXML("AFTAUDIT", XMLString);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return false;
            }
        }

        public static bool ExportReInstateData(int HQ_I_ID)
        {
            string XMLString = string.Empty;

            try
            {
                if (_proxy == null)
                {
                    _proxy = GetWebService();
                }

                XMLString = DataBaseServiceHandler.ExecuteScalar<string>(GetConnectionString(), CommandType.StoredProcedure, "rsp_GetReInstateData",
                           DataBaseServiceHandler.AddParameter<int>("@Installation_No", DbType.Int32, HQ_I_ID));

                return WrapExportXMLData(XMLString, HQ_I_ID.ToString(), "REINSTATE");

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return false;
            }
        }

        public static bool ExportAFTTransactions(int InstallationNo)
        {
            string XMLString = string.Empty;

            LogManager.WriteLog("Started Exporting AFT Transactions", LogManager.enumLogLevel.Info);
            try
            {
                if (_proxy == null)
                {
                    _proxy = GetWebService();
                    LogManager.WriteLog("Created Proxy", LogManager.enumLogLevel.Info);
                }

                XMLString = DataBaseServiceHandler.ExecuteScalar<string>(GetConnectionString(), CommandType.StoredProcedure, "rsp_GetAFTTransactions",
                           DataBaseServiceHandler.AddParameter<int>("@AFTAuditNo", DbType.Int32, InstallationNo));

                XMLString = XMLString.Replace(Environment.NewLine, "");
                XMLString = XMLString.Replace("\r", "");
                XMLString = XMLString.Replace("\n", "");
                XMLString = XMLString.Replace("\r\n", "");

                LogManager.WriteLog("XML String " + XMLString, LogManager.enumLogLevel.Info);

                return _proxy.UpdateDetailsFromXML("AFTTRANSACTION", XMLString);
                LogManager.WriteLog("Updated XML Details for AFT Transactions", LogManager.enumLogLevel.Info);

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return false;
            }
        }

        public static bool ImportGameCategory(string sXML)
        {
            bool bSuccess = false;
            try
            {
                var oArrayParam = new SqlParameter[1];
                var oParam = new SqlParameter
                {
                    ParameterName = "doc",
                    Value = sXML,
                    Direction = ParameterDirection.Input
                };
                oArrayParam[0] = oParam;
                int recordsInserteed = SqlHelper.ExecuteNonQuery(GetConnectionString(),
                                                                 CommandType.StoredProcedure,
                                                                 "usp_ImportGameCategory",
                                                                 oArrayParam);
                if (recordsInserteed > 0)
                    bSuccess = true;
                LogManager.WriteLog("Import Game Category " + "  Success value " + bSuccess, LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                bSuccess = false;
            }
            return bSuccess;
        }

        public static bool ImportGameTitle(string sXML)
        {
            bool bSuccess = false;
            try
            {
                var oArrayParam = new SqlParameter[1];
                var oParam = new SqlParameter
                {
                    ParameterName = "doc",
                    Value = sXML,
                    Direction = ParameterDirection.Input
                };
                oArrayParam[0] = oParam;
                int recordsInserteed = SqlHelper.ExecuteNonQuery(GetConnectionString(),
                                                                 CommandType.StoredProcedure,
                                                                 "usp_ImportGameTitle",
                                                                 oArrayParam);
                if (recordsInserteed > 0)
                    bSuccess = true;
                LogManager.WriteLog("Import GameTitle WS " + "  Success value " + bSuccess, LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                bSuccess = false;
            }
            return bSuccess;
        }

        public static bool ImportPaytable(string sXML)
        {
            bool bSuccess = false;
            try
            {
                var oArrayParam = new SqlParameter[1];
                var oParam = new SqlParameter
                {
                    ParameterName = "doc",
                    Value = sXML,
                    Direction = ParameterDirection.Input
                };
                oArrayParam[0] = oParam;
                int recordsInserteed = SqlHelper.ExecuteNonQuery(GetConnectionString(),
                                                                 CommandType.StoredProcedure,
                                                                 "usp_ImportPaytable",
                                                                 oArrayParam);
                if (recordsInserteed > 0)
                    bSuccess = true;
                LogManager.WriteLog("Import Paytable WS " + "  Success value " + bSuccess, LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                bSuccess = false;
            }
            return bSuccess;
        }

        public static bool ImportGameLibraryMapping(string sXML)
        {
            bool bSuccess = false;
            try
            {
                var oArrayParam = new SqlParameter[1];
                var oParam = new SqlParameter
                {
                    ParameterName = "doc",
                    Value = sXML,
                    Direction = ParameterDirection.Input
                };
                oArrayParam[0] = oParam;
                int recordsInserteed = SqlHelper.ExecuteNonQuery(GetConnectionString(),
                                                                 CommandType.StoredProcedure,
                                                                 "usp_ImportGameLibraryMapping",
                                                                 oArrayParam);
                if (recordsInserteed >= 0)
                    bSuccess = true;
                LogManager.WriteLog("Import GameLibraryMapping WS " + "  Success value " + bSuccess, LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                bSuccess = false;
            }
            return bSuccess;
        }

        ///// <summary>
        ///// Update Component Details.
        ///// </summary>
        ///// <param name="CompID"></param>
        ///// <returns></returns>
        //public static bool ExportComponentDetails(int CompID)
        //{
        //    string XMLString = string.Empty;

        //    try
        //    {
        //        if (_proxy == null)
        //        {
        //            _proxy = GetWebService();
        //        }

        //        XMLString = DataBaseServiceHandler.ExecuteScalar<string>(GetConnectionString(), CommandType.StoredProcedure, "rsp_GetCompDetailsForExport",
        //                   DataBaseServiceHandler.AddParameter<int>("@CCD_ID", DbType.Int32, CompID));

        //        return _proxy.UpdateDetailsFromXML("COMPONENTDETAILS", XMLString);
        //    }
        //    catch (Exception ex)
        //    {
        //        ExceptionManager.Publish(ex);
        //        return false;
        //    }
        //}

        ///// <summary>
        ///// Update Machine Component details.
        ///// </summary>
        ///// <param name="MachineID"></param>
        ///// <returns></returns>
        //public static bool ExportMachineComponentDetails(int MachineID)
        //{
        //    string XMLString = string.Empty;

        //    try
        //    {
        //        if (_proxy == null)
        //        {
        //            _proxy = GetWebService();
        //        }

        //        XMLString = DataBaseServiceHandler.ExecuteScalar<string>(GetConnectionString(), CommandType.StoredProcedure, "rsp_GetMachineCompDetailsForExport",
        //                   DataBaseServiceHandler.AddParameter<int>("@Machine_ID", DbType.Int32, MachineID));

        //        return _proxy.UpdateDetailsFromXML("MACHINECOMPONENTDETAILS", XMLString);
        //    }
        //    catch (Exception ex)
        //    {
        //        ExceptionManager.Publish(ex);
        //        return false;
        //    }
        //}

        ///// <summary>
        ///// Update Machine Component details.
        ///// </summary>
        ///// <param name="MachineID"></param>
        ///// <returns></returns>
        //public static bool ExportComponentVerificationDetails(int VerID)
        //{
        //    string XMLString = string.Empty;

        //    try
        //    {
        //        if (_proxy == null)
        //        {
        //            _proxy = GetWebService();
        //        }

        //        XMLString = DataBaseServiceHandler.ExecuteScalar<string>(GetConnectionString(), CommandType.StoredProcedure, "rsp_GetCompVerificationRecordForExport",
        //                   DataBaseServiceHandler.AddParameter<int>("@Verification_ID", DbType.Int32, VerID));

        //        return _proxy.UpdateDetailsFromXML("COMPVERIFICATIONRECORD", XMLString);
        //    }
        //    catch (Exception ex)
        //    {
        //        ExceptionManager.Publish(ex);
        //        return false;
        //    }
        //}

        public static bool ImportManufacturerDetails(string sXML)
        {
            bool bSuccess = false;
            try
            {
                var oArrayParam = new SqlParameter[1];
                var oParam = new SqlParameter
                {
                    ParameterName = "doc",
                    Value = sXML,
                    Direction = ParameterDirection.Input
                };
                oArrayParam[0] = oParam;
                int recordsInserteed = SqlHelper.ExecuteNonQuery(GetConnectionString(),
                                                                 CommandType.StoredProcedure,
                                                                 "usp_ImportManufacturerDetails",
                                                                 oArrayParam);
                if (recordsInserteed > 0)
                    bSuccess = true;
                LogManager.WriteLog("Import Maufacturer Details" + "  Success value " + bSuccess, LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                bSuccess = false;
            }
            return bSuccess;
        }

        public static bool ImportDeclaredCollectionDetails(string sXML)
        {
            LogManager.WriteLog("Inside ImportDeclaredCollectionDetails", LogManager.enumLogLevel.Info);
            bool bSuccess = false;
            int iRetValue = -1;
            try
            {
                var oArrayParam = new SqlParameter[2];
                var oParam = new SqlParameter
                {
                    ParameterName = "doc",
                    Value = sXML,
                    DbType = DbType.AnsiString,
                    Direction = ParameterDirection.Input
                };
                oArrayParam[0] = oParam;

                oParam = new SqlParameter
                {
                    ParameterName = "IsSuccess",
                    Value = iRetValue,
                    Direction = ParameterDirection.Output
                };
                oArrayParam[1] = oParam;

                SqlHelper.ExecuteNonQuery(GetConnectionString(),
                                                                 CommandType.StoredProcedure,
                                                                 "usp_ImportEnterpriseCollectionDeclaration",
                                                                 oArrayParam);

                if (Convert.ToInt32(oArrayParam[1].Value) == 0)
                    bSuccess = true;

                LogManager.WriteLog("Import Declared Collection Details" + "  Success value " + bSuccess, LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                bSuccess = false;
            }
            return bSuccess;
        }

        public static bool DeleteZone(string sXML)
        {
            bool bSuccess = false;
            try
            {
                var oArrayParam = new SqlParameter[1];
                var oParam = new SqlParameter
                {
                    ParameterName = "doc",
                    Value = sXML,
                    Direction = ParameterDirection.Input
                };
                oArrayParam[0] = oParam;
                int recordsDeleted = SqlHelper.ExecuteNonQuery(GetConnectionString(),
                                                                 CommandType.StoredProcedure,
                                                                 "usp_DeleteZone",
                                                                 oArrayParam);
                if (recordsDeleted >= 0)
                    bSuccess = true;
                LogManager.WriteLog("Delete Zone WS " + "  Success value " + bSuccess, LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                bSuccess = false;
            }
            return bSuccess;
        }

        public static bool ExportInstallationGameData(int Installation_No)
        {
            string GameDataXML = string.Empty;
            bool ExportStatus = false;
            _proxy = GetWebService();

            try
            {
                //Create the xml for InstallationGameData
                GameDataXML = DataBaseServiceHandler.ExecuteScalar<string>(GetConnectionString(), CommandType.StoredProcedure,
                    "rsp_GetInstallationGameData",
                    DataBaseServiceHandler.AddParameter<int>("InstallationNo", DbType.Int32, Installation_No));

                if (string.IsNullOrEmpty(GameDataXML))
                    return true;

                GameDataXML = GameDataXML.Replace(Environment.NewLine, "");
                GameDataXML = GameDataXML.Replace("\r", "");
                GameDataXML = GameDataXML.Replace("\n", "");
                GameDataXML = GameDataXML.Replace("\r\n", "");

                LogManager.WriteLog("Game Data Export XML " + GameDataXML, LogManager.enumLogLevel.Info);

                //insert the game info in corresponding Enteprise
                ExportStatus = _proxy.ImportInstallationGameData(GameDataXML);
                LogManager.WriteLog("Status for Game Data Export " + ExportStatus.ToString(), LogManager.enumLogLevel.Info);

                return ExportStatus;

            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                return false;
            }
        }

        public static bool ExportGamePaytableDetailsforInstallation(int eh_id, int igpi_id)
        {
            string XMLString = string.Empty;

            try
            {
                if (_proxy == null)
                {
                    _proxy = GetWebService();
                }

                XMLString = DataBaseServiceHandler.ExecuteScalar<string>(GetConnectionString(), CommandType.StoredProcedure, "rsp_ExportInstallationGamePaytableDetails",
                           DataBaseServiceHandler.AddParameter<int>("@IGPI_ID", DbType.Int32, igpi_id));

                return WrapExportXMLData(XMLString, eh_id.ToString(), "GAMEPAYTABLEDETAILS");

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return false;
            }
        }

        public static bool GetSiteStatus()
        {
            LogManager.WriteLog("Inside GetSiteStatus method", LogManager.enumLogLevel.Info);

            object result = SqlHelper.ExecuteScalar(GetConnectionString(),
                                                          "rsp_GetSiteStatus");

            return Convert.ToBoolean(result);
        }

        public static string GetSettingFromDB(string strSetting)
        {
            string strReturnValue = string.Empty;
            try
            {
                LogManager.WriteLog("Inside GetSettingFromDB method", LogManager.enumLogLevel.Info);

                SqlParameter[] sqlparams = new SqlParameter[5];

                sqlparams[0] = new SqlParameter("@Setting_ID", 0);
                sqlparams[1] = new SqlParameter("@Setting_Name", strSetting.Trim());
                sqlparams[2] = new SqlParameter("@Setting_Default", string.Empty);

                sqlparams[3] = new SqlParameter();
                sqlparams[3].ParameterName = "Setting_Value";
                sqlparams[3].Direction = ParameterDirection.Output;
                sqlparams[3].Value = string.Empty;
                sqlparams[3].SqlDbType = SqlDbType.VarChar;
                sqlparams[3].Size = 100;

                SqlParameter ReturnValue = new SqlParameter();
                ReturnValue.ParameterName = "RETURN_VALUE";
                ReturnValue.Direction = ParameterDirection.ReturnValue;
                sqlparams[4] = ReturnValue;

                SqlHelper.ExecuteNonQuery(GetConnectionString(), System.Data.CommandType.StoredProcedure, "rsp_getSetting", sqlparams);
                if (sqlparams[3].Value != null || sqlparams[3].Value.ToString() != string.Empty)
                {
                    strReturnValue = Convert.ToString(sqlparams[3].Value);
                }
                else
                {
                    strReturnValue = string.Empty;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            return strReturnValue;
        }

        public class Parameters
        {
            public int InstallationNo;
            public int BarPositionNo;
        }

        internal static bool ExportSiteConfig()
        {
            LogManager.WriteLog("Entring ExportSiteConfig", LogManager.enumLogLevel.Debug);
            bool result = false;
            try
            {
                string sXml = string.Empty;
                var siteCode = DataBaseServiceHandler.ExecuteScalar<string>(GetConnectionString(), CommandType.Text,
                                                                            "select code from Site");
                if (!string.IsNullOrEmpty(siteCode))
                    sXml = "<SITECONFIG><siteCode>" + siteCode.ToString() + "</siteCode>"
                         + "<ExchangeConnection>" + ExchangeEncryptedConnectionString() + "</ExchangeConnection>"
                         + "<TicketConnection>" + TicketingEncryptedConnectionString() + "</TicketConnection></SITECONFIG>";
                if (!string.IsNullOrEmpty(sXml))
                {
                    if (_proxy == null)
                        _proxy = GetWebService();

                    result = _proxy.ImportSiteConfig(sXml);
                }
                if (result)
                    LogManager.WriteLog("Exported site config successfully", LogManager.enumLogLevel.Info);
                else
                    LogManager.WriteLog("Exported site config not successful", LogManager.enumLogLevel.Error);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("ExportSiteConfig" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
            }
            return result;
        }
        //
        internal static string ExchangeEncryptedConnectionString()
        {
            string sKey = string.Empty;
           // RegistryKey RegKey1;
            string SQLConnect = "";
            ConfigManager.SetConfigurationMode(ConfigManager.ConfigurationMode.AppConfig);
            try
            {
                //RegKey1 = BMCRegistryHelper.GetRegLocalMachine().OpenSubKey(ConfigManager.Read("RegistryPath"));
                SQLConnect = BMCRegistryHelper.GetRegKeyValue(ConfigManager.Read("RegistryPath"), "SQLConnect");
                //SQLConnect = RegKey1.GetValue("SQLConnect").ToString();
            }

            catch (Exception ex)
            {
                LogManager.WriteLog("ExchangeEncryptedConnectionString" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
            }
            return SQLConnect;
        }
        //
        internal static string TicketingEncryptedConnectionString()
        {
            string sKey = string.Empty;
           // RegistryKey RegKey1;
            string SQLConnect = "";
            ConfigManager.SetConfigurationMode(ConfigManager.ConfigurationMode.AppConfig);
            try
            {
               // RegKey1 = BMCRegistryHelper.GetRegLocalMachine().OpenSubKey(ConfigManager.Read("RegistryPath"));
                SQLConnect = BMCRegistryHelper.GetRegKeyValue(ConfigManager.Read("RegistryPath"), "TicketingSQLConnect");
            }

            catch (Exception ex)
            {
                LogManager.WriteLog("TicketingEncryptedConnectionString" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
            }
            return SQLConnect;
        }

        public static bool ImportStackerDetails(string sXML)
        {
            bool bSuccess = false;
            try
            {
                var oArrayParam = new SqlParameter[2];
                var oParam = new SqlParameter
                {
                    ParameterName = "doc",
                    Value = sXML,
                    Direction = ParameterDirection.Input
                };
                oArrayParam[0] = oParam;
                var oParamS = new SqlParameter
                {
                    ParameterName = "IsSuccess",
                    Value = 0,
                    Direction = ParameterDirection.Output
                };
                oArrayParam[1] = oParamS;
                int recordsInserted = SqlHelper.ExecuteNonQuery(GetConnectionString(),
                                                                 CommandType.StoredProcedure,
                                                                 "usp_ImportStackerDetailsFromXML",
                                                                 oArrayParam);
                if (recordsInserted > 0)
                {
                    bSuccess = true;
                    LogManager.WriteLog("Import Stacker WS " + "  Success value " + bSuccess, LogManager.enumLogLevel.Info);
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                bSuccess = false;
            }
            return bSuccess;
        }
        /// <summary>
        /// shareholders
        /// </summary>
        /// <param name="sXML"></param>
        /// <returns></returns>
        public static bool ImportShareHolderDetails(string sXML)
        {
            bool bSuccess = false;
            try
            {
                var oArrayParam = new SqlParameter[2];
                var oParam = new SqlParameter
                {
                    ParameterName = "doc",
                    Value = sXML,
                    Direction = ParameterDirection.Input
                };
                oArrayParam[0] = oParam;
                var oParamS = new SqlParameter
                {
                    ParameterName = "IsSuccess",
                    Value = 0,

                    Direction = ParameterDirection.Output
                };
                oArrayParam[1] = oParamS;
                int recordsInserted = SqlHelper.ExecuteNonQuery(GetConnectionString(),
                                                                 CommandType.StoredProcedure,
                                                                 "usp_ImportShareHolderDetailsFromXML",
                                                                 oArrayParam);
                if (recordsInserted > 0)
                {
                    bSuccess = true;
                    LogManager.WriteLog("Import ShareHolder WS " + "  Success value " + bSuccess, LogManager.enumLogLevel.Info);
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                bSuccess = false;
            }
            return bSuccess;
        }
        public static bool ImportExpenseShareDetails(string sXML)
        {
            bool bSuccess = false;
            try
            {
                var oArrayParam = new SqlParameter[2];
                var oParam = new SqlParameter
                {
                    ParameterName = "doc",
                    Value = sXML,
                    Direction = ParameterDirection.Input
                };
                oArrayParam[0] = oParam;
                var oParamS = new SqlParameter
                {
                    ParameterName = "IsSuccess",
                    Value = 0,
                    Direction = ParameterDirection.Output
                };
                oArrayParam[1] = oParamS;
                int recordsInserted = SqlHelper.ExecuteNonQuery(GetConnectionString(),
                                                                 CommandType.StoredProcedure,
                                                                 "usp_ImportExpenseShareDetailsFromXML",
                                                                 oArrayParam);
                if (recordsInserted > 0)
                {
                    bSuccess = true;
                    LogManager.WriteLog("Import ExpenseShare WS " + "  Success value " + bSuccess, LogManager.enumLogLevel.Info);
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                bSuccess = false;
            }
            return bSuccess;
        }
        public static bool ImportProfitShareDetails(string sXML)
        {
            bool bSuccess = false;
            try
            {
                var oArrayParam = new SqlParameter[2];
                var oParam = new SqlParameter
                {
                    ParameterName = "doc",
                    Value = sXML,
                    Direction = ParameterDirection.Input
                };
                oArrayParam[0] = oParam;
                var oParamS = new SqlParameter
                {
                    ParameterName = "IsSuccess",
                    Value = 0,

                    Direction = ParameterDirection.Output
                };
                oArrayParam[1] = oParamS;
                int recordsInserted = SqlHelper.ExecuteNonQuery(GetConnectionString(),
                                                                 CommandType.StoredProcedure,
                                                                 "usp_ImportProfitShareDetailsFromXML",
                                                                 oArrayParam);
                if (recordsInserted > 0)
                {
                    bSuccess = true;
                    LogManager.WriteLog("Import ProfitShare WS " + "  Success value " + bSuccess, LogManager.enumLogLevel.Info);
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                bSuccess = false;
            }
            return bSuccess;
        }
        public static bool ImportExpenseShareGroupDetails(string sXML)
        {
            bool bSuccess = false;
            try
            {
                var oArrayParam = new SqlParameter[2];
                var oParam = new SqlParameter
                {
                    ParameterName = "doc",
                    Value = sXML,
                    Direction = ParameterDirection.Input
                };
                oArrayParam[0] = oParam;
                var oParamS = new SqlParameter
                {
                    ParameterName = "IsSuccess",
                    Value = 0,
                    Direction = ParameterDirection.Output
                };
                oArrayParam[1] = oParamS;
                int recordsInserted = SqlHelper.ExecuteNonQuery(GetConnectionString(),
                                                                 CommandType.StoredProcedure,
                                                                 "usp_ImportExpenseShareGroupDetailsFromXML",
                                                                 oArrayParam);
                if (recordsInserted > 0)
                {
                    bSuccess = true;
                    LogManager.WriteLog("Import ExpenseShareGroup WS " + "  Success value " + bSuccess, LogManager.enumLogLevel.Info);
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                bSuccess = false;
            }
            return bSuccess;
        }
        public static bool ImportProfitShareGroupDetails(string sXML)
        {
            bool bSuccess = false;
            try
            {
                var oArrayParam = new SqlParameter[2];
                var oParam = new SqlParameter
                {
                    ParameterName = "doc",
                    Value = sXML,
                    Direction = ParameterDirection.Input
                };
                oArrayParam[0] = oParam;
                var oParamS = new SqlParameter
                {
                    ParameterName = "IsSuccess",
                    Value = 0,
                    Direction = ParameterDirection.Output
                };
                oArrayParam[1] = oParamS;
                int recordsInserted = SqlHelper.ExecuteNonQuery(GetConnectionString(),
                                                                 CommandType.StoredProcedure,
                                                                 "usp_ImportProfitShareGroupDetailsFromXML",
                                                                 oArrayParam);
                if (recordsInserted > 0)
                {
                    bSuccess = true;
                    LogManager.WriteLog("Import ProfitShareGroup WS " + "  Success value " + bSuccess, LogManager.enumLogLevel.Info);
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                bSuccess = false;
            }
            return bSuccess;
        }
        public static bool ImportLiquidationDetails(string sXML)
        {
            bool bSuccess = false;
            try
            {
                var oArrayParam = new SqlParameter[4];
                var oParam = new SqlParameter
                {
                    ParameterName = "doc",
                    Value = sXML,
                    Direction = ParameterDirection.Input
                };
                oArrayParam[0] = oParam;
                
                var oParamS = new SqlParameter
                {
                    ParameterName = "IsSuccess",
                    Value = 0,
                    Direction = ParameterDirection.Output
                };
                oArrayParam[1] = oParamS;

                oParamS = new SqlParameter
                {
                    ParameterName = "HQ_Liquidation_Id",
                    Value = 0,
                    Direction = ParameterDirection.Output
                };
                oArrayParam[2] = oParamS;

                oParamS = new SqlParameter
                {
                    ParameterName = "Liquidation_Id",
                    Value = 0,
                    Direction = ParameterDirection.Output
                };
                oArrayParam[3] = oParamS;

                int recordsInserted = SqlHelper.ExecuteNonQuery(GetConnectionString(),
                                                                 CommandType.StoredProcedure,
                                                                 "usp_ImportLiquidationDetailsFromXML",
                                                                 oArrayParam);

                if (oArrayParam[2].Value != null && oArrayParam[3].Value != null && oArrayParam[2].Value.ToString() != string.Empty && oArrayParam[3].Value.ToString() != string.Empty
                            && Convert.ToInt32(oArrayParam[2].Value.ToString()) > 0 && Convert.ToInt32(oArrayParam[3].Value.ToString()) > 0)
                {
                    if (_proxy == null)
                    {
                        _proxy = GetWebService();
                    }
                    _proxy.UpdateSiteLiquidationID(Convert.ToInt32(oArrayParam[2].Value.ToString()), Convert.ToInt32(oArrayParam[3].Value.ToString()));
                    return true;
                }
                
                if (recordsInserted > 0)
                {
                    bSuccess = true;
                    LogManager.WriteLog("Import LiquidationDetails WS " + "  Success value " + bSuccess, LogManager.enumLogLevel.Info);
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                bSuccess = false;
            }
            return bSuccess;
        }
        public static bool ImportLiquidationShareDetails(string sXML)
        {
            bool bSuccess = false;
            try
            {
                var oArrayParam = new SqlParameter[4];
                var oParam = new SqlParameter
                {
                    ParameterName = "doc",
                    Value = sXML,
                    Direction = ParameterDirection.Input
                };

                oArrayParam[0] = oParam;
                var oParamS = new SqlParameter
                {
                    ParameterName = "IsSuccess",
                    Value = 0,
                    Direction = ParameterDirection.Output
                };
                oArrayParam[1] = oParamS;

                oParamS = new SqlParameter
                {
                    ParameterName = "HQ_LiquidationShare_Id",
                    Value = 0,
                    Direction = ParameterDirection.Output
                };
                oArrayParam[2] = oParamS;

                oParamS = new SqlParameter
                {
                    ParameterName = "LiquidationShare_Id",
                    Value = 0,
                    Direction = ParameterDirection.Output
                };
                oArrayParam[3] = oParamS;

                int recordsInserted = SqlHelper.ExecuteNonQuery(GetConnectionString(),
                                                                 CommandType.StoredProcedure,
                                                                 "usp_ImportLiquidationShareDetailsFromXML",
                                                                 oArrayParam);

                if (oArrayParam[2].Value != null && oArrayParam[3].Value != null && oArrayParam[2].Value.ToString() != string.Empty && oArrayParam[3].Value.ToString() != string.Empty
                            && Convert.ToInt32(oArrayParam[2].Value.ToString()) > 0 && Convert.ToInt32(oArrayParam[3].Value.ToString()) > 0)
                {
                    if (_proxy == null)
                    {
                        _proxy = GetWebService();
                    }
                    _proxy.UpdateSiteLiquidationShareID(Convert.ToInt32(oArrayParam[2].Value.ToString()), Convert.ToInt32(oArrayParam[3].Value.ToString()));
                    return true;
                }


                if (recordsInserted > 0)
                {
                    bSuccess = true;
                    LogManager.WriteLog("Import LiquidationShareDetails WS " + "  Success value " + bSuccess, LogManager.enumLogLevel.Info);
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                bSuccess = false;
            }
            return bSuccess;
        }
        /// <summary>
        public static bool ExportStackerLevelData(int EH_ID, int Installation_No)
        {
            bool retVal = true;

            try
            {
                var sqlParameters = new SqlParameter[1];
                var sqlParameter = new SqlParameter
                {
                    ParameterName = "Installation_No",
                    Value = Installation_No,
                    Direction = ParameterDirection.Input
                };
                sqlParameters[0] = sqlParameter;

                string strXmlExportStackerLevel = SqlHelper.ExecuteScalar(GetConnectionString(),
                                                                              CommandType.StoredProcedure,
                                                                              "rsp_ExportStackerLevelInfoDetails",
                                                                              sqlParameters).ToString();

                if (strXmlExportStackerLevel.Trim().Length > 0)
                {
                    retVal = WrapExportXMLData(strXmlExportStackerLevel, EH_ID.ToString(), "STACKERLEVEL");
                    if (retVal)
                    {
                        LogManager.WriteLog("STACKER LEVEL ::" + "EXPORTED SL DATA SUCCESSFULLY ::", LogManager.enumLogLevel.Info);

                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("STACKER LEVEL ::" + "EXPORT SL DATA FAILED :: - XML Data Blank", LogManager.enumLogLevel.Error);
                retVal = false;
            }
            return retVal;
        }

        public static bool ExportGameCappingDetails(int EH_ID, int GameCappingID)
        {
            int Hq_GameCapID = 0;
            bool bresult = false;

            try
            {
                var siteCode = DataBaseServiceHandler.ExecuteScalar<string>(GetConnectionString(), CommandType.Text,
                                                                       "select code from Site");
                _proxy = GetWebService();
                var sqlParameters = new SqlParameter[2];
                var sqlParameter = new SqlParameter
                {
                    ParameterName = "GameCappingID",
                    Value = GameCappingID,
                    Direction = ParameterDirection.Input
                };
                sqlParameters[0] = sqlParameter;

                var sqlParameter1 = new SqlParameter
                {
                    ParameterName = "SiteCode",
                    Value = siteCode,
                    Direction = ParameterDirection.Input
                };
                sqlParameters[1] = sqlParameter1;
                string strXmlExportGameCappingDetails = SqlHelper.ExecuteScalar(GetConnectionString(),
                                                                              CommandType.StoredProcedure,
                                                                              "rsp_ExportGameCappingInfoDetails",
                                                                              sqlParameters).ToString();

                strXmlExportGameCappingDetails = strXmlExportGameCappingDetails.Replace(Environment.NewLine, "");
                strXmlExportGameCappingDetails = strXmlExportGameCappingDetails.Replace("\r", "");
                strXmlExportGameCappingDetails = strXmlExportGameCappingDetails.Replace("\n", "");
                strXmlExportGameCappingDetails = strXmlExportGameCappingDetails.Replace("\r\n", "");

                if (strXmlExportGameCappingDetails.Trim().Length > 0)
                {
                    Hq_GameCapID = _proxy.ImportGameCapping(strXmlExportGameCappingDetails);
                    LogManager.WriteLog("HQ_GameCappingID " + Hq_GameCapID, LogManager.enumLogLevel.Info);
                    if (Hq_GameCapID > 0)
                    {
                        var sqlParms = new SqlParameter[2];
                        var sqlParms1 = new SqlParameter
                        {
                            ParameterName = "HQ_GameCappingID",
                            Value = Hq_GameCapID,
                            Direction = ParameterDirection.Input
                        };                       
                        sqlParms[0] = sqlParms1;

                        var sqlParms2 = new SqlParameter
                        {
                            ParameterName = "GameCappingID",
                            Value = GameCappingID,
                            Direction = ParameterDirection.Input
                        };
                        sqlParms[1] = sqlParms2;                       
                        SqlHelper.ExecuteScalar(GetConnectionString(), CommandType.StoredProcedure,
                                                                              "usp_UpdateGameCappingHQID",
                                                                              sqlParms);
                        LogManager.WriteLog("HQ_id GAME CAPPING DETAILS Updated " + bresult, LogManager.enumLogLevel.Info);
                        LogManager.WriteLog("GAME CAPPING DETAILS LEVEL ::" + "EXPORTED SL DATA SUCCESSFULLY ::", LogManager.enumLogLevel.Info);
                        bresult = true;                       
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("GAME CAPPING DETAILS ::" + "EXPORT SL DATA FAILED :: - XML Data Blank", LogManager.enumLogLevel.Error);                
            }

            return bresult;
        }

        /// <summary>
        /// //////
        /// LIQUIDATIONDETAILS
        /// </summary>
        /// <param name="EH_ID"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool ExportLiquidationDetails(int EH_ID, int LiquidationId)
        {
            bool retVal = true;

            try
            {
                var sqlParameters = new SqlParameter[1];
                var sqlParameter = new SqlParameter
                {
                    ParameterName = "LiquidationId",
                    Value = LiquidationId,
                    Direction = ParameterDirection.Input
                };
                sqlParameters[0] = sqlParameter;

                string strXmlExportLiquidationDetails = SqlHelper.ExecuteScalar(GetConnectionString(),
                                                                              CommandType.StoredProcedure,
                                                                              "rsp_ExportLiquidationInfoDetails",
                                                                              sqlParameters).ToString();

                if (strXmlExportLiquidationDetails.Trim().Length > 0)
                {
                    retVal = WrapExportXMLData(strXmlExportLiquidationDetails, EH_ID.ToString(), "LIQUIDATIONDETAILS");
                    if (retVal)
                    {
                        LogManager.WriteLog("LIQUIDATION DETAILS LEVEL ::" + "EXPORTED SL DATA SUCCESSFULLY ::", LogManager.enumLogLevel.Info);

                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("LIQUIDATION DETAILS ::" + "EXPORT SL DATA FAILED :: - XML Data Blank", LogManager.enumLogLevel.Error);
                retVal = false;
            }
            return retVal;
        }

        public static bool ExportLiquidationShareDetails(int EH_ID, int LiquidationShareId)
        {
            bool retVal = true;

            try
            {
                var sqlParameters = new SqlParameter[1];
                var sqlParameter = new SqlParameter
                {
                    ParameterName = "LiquidationShareId",
                    Value = LiquidationShareId,
                    Direction = ParameterDirection.Input
                };
                sqlParameters[0] = sqlParameter;

                string strXmlExportLiquidationShareDetails = SqlHelper.ExecuteScalar(GetConnectionString(),
                                                                              CommandType.StoredProcedure,
                                                                              "rsp_ExportLiquidationShareInfoDetails",
                                                                              sqlParameters).ToString();

                if (strXmlExportLiquidationShareDetails.Trim().Length > 0)
                {
                    retVal = WrapExportXMLData(strXmlExportLiquidationShareDetails, EH_ID.ToString(), "LIQUIDATIONSHAREDETAILS");
                    if (retVal)
                    {
                        LogManager.WriteLog("LIQUIDATIONSHARE DETAILS LEVEL ::" + "EXPORTED SL DATA SUCCESSFULLY ::", LogManager.enumLogLevel.Info);

                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("LIQUIDATIONSHARE DETAILS ::" + "EXPORT SL DATA FAILED :: - XML Data Blank", LogManager.enumLogLevel.Error);
                retVal = false;
            }
            return retVal;
        }
        /// <summary>></summary>

        public static bool ExportGloryCDAudit(int EH_ID, int id)
        {
            bool retVal = true;

            try
            {
                var sqlParameters = new SqlParameter[1];
                var sqlParameter = new SqlParameter
                {
                    ParameterName = "id",
                    Value = id,
                    Direction = ParameterDirection.Input
                };
                sqlParameters[0] = sqlParameter;

                string strXmlExportGloryCDAudit = SqlHelper.ExecuteScalar(GetConnectionString(),
                                                                              CommandType.StoredProcedure,
                                                                              "rsp_ExportGloryAuditDetails",
                                                                              sqlParameters).ToString();
                if (strXmlExportGloryCDAudit.Trim().Length > 0)
                {
                    retVal = WrapExportXMLData(strXmlExportGloryCDAudit, EH_ID.ToString(), "GLORYAUDIT");
                    if (retVal)
                    {
                        LogManager.WriteLog("GLORY AUDIT::" + "EXPORTED GloryAudit DATA SUCCESSFULLY ::", LogManager.enumLogLevel.Info);
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("GLORY AUDIT ::" + "EXPORT GloryAudit DATA FAILED :: - XML Data Blank", LogManager.enumLogLevel.Error);
                retVal = false;
            }
            return retVal;
        }
        public static bool ExportBatchCompleteStatus(int iBatchID, string ehid)
        {
            string XMLString = string.Empty;

            try
            {
                if (_proxy == null)
                {
                    _proxy = GetWebService();
                }

                XMLString = DataBaseServiceHandler.ExecuteScalar<string>(GetConnectionString(), CommandType.StoredProcedure, "rsp_Export_BatchExportCompleteStatus",
                           DataBaseServiceHandler.AddParameter<int>("@Batch_No", DbType.Int32, iBatchID));

                if (WrapExportXMLData(XMLString, ehid, "BATCHEXPCOMPLETE"))
                    return true;

                throw new Exception("Batch Export complete status update Failed to Export: MH ID: " + iBatchID + " And EH ID : " + ehid);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return false;
            }
        }
        public static bool ExportFundDetails(int iFundID, string ehid)
        {
            string XMLString = string.Empty;
            try
            {
                if (_proxy == null)
                {
                    _proxy = GetWebService();
                }
                XMLString = DataBaseServiceHandler.ExecuteScalar<string>(GetConnectionString(), CommandType.StoredProcedure, "rsp_ExportFundDetails",
                           DataBaseServiceHandler.AddParameter<int>("@Fund_No", DbType.Int32, iFundID));
                if (WrapExportXMLData(XMLString, ehid, "FUND"))
                    return true;
                LogManager.WriteLog("Fund details Failed to Export: Fund ID:" + iFundID + " And EH ID : " + ehid, LogManager.enumLogLevel.Error);
                return false;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return false;
            }
        }               
        public static bool ExportVaultEvent(int Event_ID, string ehid)
        {
            string XMLString = string.Empty;
            bool bStatus = false;

            try
            {
                if (_proxy == null)
                {
                    _proxy = GetWebService();
                }

                XMLString = DataBaseServiceHandler.ExecuteScalar<string>(GetConnectionString(), CommandType.StoredProcedure, "rsp_Vault_GetEventforExport",
                           DataBaseServiceHandler.AddParameter<int>("@Event_ID", DbType.Int32, Event_ID));
                if (XMLString.Trim().Length > 0)
                {
                    LogManager.WriteLog("ExportVaultEvent:-->" + "WrappingXMLData:--->" + XMLString, LogManager.enumLogLevel.Info);
                    bool wsCallSuccess = WrapExportXMLData(XMLString, ehid.ToString(), "VAULTEVENT");
                    if (wsCallSuccess)
                    {
                        LogManager.WriteLog("EXPORT_VAULT_EVENT:" + "Exported Vault Drop Data Succcesfully ::", LogManager.enumLogLevel.Info);
                        bStatus = true;
                    }
                    else
                    {
                        LogManager.WriteLog("ExportVaultEvent :" + "status update Failed to Export: MH ID: " + Event_ID + " And EH ID : " + ehid, LogManager.enumLogLevel.Error);
                        bStatus = false;
                    }
                }
                return bStatus;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("ExportVaultEvent status update Failed to Export: MH ID: " + Event_ID + " And EH ID : " + ehid, LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);

                return bStatus;
            }
        }
        public static bool ExportVaultBalance(int EH_ID, int Installation_No)
        {
            var sqlParameters = new SqlParameter[1];
            var sqlParameter = new SqlParameter
            {
                ParameterName = "Installation_No",
                Value = Installation_No,
                Direction = ParameterDirection.Input
            };
            sqlParameters[0] = sqlParameter;
            string strXmlExportVaultBalance = SqlHelper.ExecuteScalar(GetConnectionString(),
               CommandType.StoredProcedure, "rsp_ExportVaultBalanceDetails", sqlParameters).ToString();
            if (strXmlExportVaultBalance.Trim().Length > 0)
            {
                bool wsCallSuccess = WrapExportXMLData(strXmlExportVaultBalance, EH_ID.ToString(), "VAULTBALANCE");
                if (wsCallSuccess)
                {
                    LogManager.WriteLog("VAULT BALANCE:" + "EXPORTED Vault Balance DATA SUCCESSFULLY ::", LogManager.enumLogLevel.Info);
                    return true;
                }
            }
            LogManager.WriteLog("VAULT BALANCE:" + "EXPORT Vault Balance DATA FAILED :: - XML Data Blank", LogManager.enumLogLevel.Error);
            return false;
        }

        public static bool ExportVaultDrop(int Drop_Id, string ehid)
        {
            string XMLString = string.Empty;
            bool bStatus = false;

            try
            {
                if (_proxy == null)
                {
                    _proxy = GetWebService();
                }

                XMLString = DataBaseServiceHandler.ExecuteScalar<string>(GetConnectionString(), CommandType.StoredProcedure, "rsp_Vault_ExportDrop",
                           DataBaseServiceHandler.AddParameter<int>("@Drop_Id", DbType.Int32, Drop_Id));
                if (XMLString.Trim().Length > 0)
                {
                    LogManager.WriteLog("ExportVaultDrop:-->" + "WrappingXMLData:--->" + XMLString, LogManager.enumLogLevel.Info);
                    bool wsCallSuccess = WrapExportXMLData(XMLString, ehid.ToString(), "VAULTDROP");
                    if (wsCallSuccess)
                    {
                        LogManager.WriteLog("EXPORT_VAULT_DROP:" + "Exported Vault Drop Data Succcesfully ::", LogManager.enumLogLevel.Info);
                        bStatus = true;
                    }
                    else
                    {
                        LogManager.WriteLog("ExportVaultDrop  status update Failed to Export: MH ID: " + Drop_Id + " And EH ID : " + ehid, LogManager.enumLogLevel.Error);
                        bStatus = false;
                    }
                }
                return bStatus;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("ExportVaultDrop status update Failed to Export: MH ID: " + Drop_Id + " And EH ID : " + ehid, LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);

                return bStatus;
            }
        }

        public static bool ExportVaultTransactionEvent(int Event_ID, string ehid)
        {
            string XMLString = string.Empty;
            bool bStatus = false;

            try
            {
                if (_proxy == null)
                {
                    _proxy = GetWebService();
                }

                XMLString = DataBaseServiceHandler.ExecuteScalar<string>(GetConnectionString(), CommandType.StoredProcedure, "rsp_Vault_GetTransactionEventsforExport",
                           DataBaseServiceHandler.AddParameter<int>("@Event_ID", DbType.Int32, Event_ID));

                if (XMLString.Trim().Length > 0)
                {
                    LogManager.WriteLog("ExportVaultTransactionEvent:-->" + "WrappingXMLData:--->" + XMLString, LogManager.enumLogLevel.Info);
                    bool wsCallSuccess = WrapExportXMLData(XMLString, ehid.ToString(), "VAULTTRANSACTIONEVENT");
                    if (wsCallSuccess)
                    {
                        LogManager.WriteLog("EXPORT_VAULT_TRANSACTION_EVENT:" + "Exported Vault Drop Data Succcesfully ::", LogManager.enumLogLevel.Info);
                        bStatus = true;
                    }
                    else
                    {
                        LogManager.WriteLog("ExportVaultTransactionEvent  status update Failed to Export: MH ID: " + Event_ID + " And EH ID : " + ehid, LogManager.enumLogLevel.Error);
                        bStatus = false;
                    }
                }
                return bStatus;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("ExportVaultTransactionEvent status update Failed to Export: MH ID: " + Event_ID + " And EH ID : " + ehid, LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);

                return bStatus;
            }
        }

        public static bool ExportVaultTransaction(int ehid, int Transaction_Id)
        {
            string XMLString = string.Empty;
            bool bStatus = false;

            try
            {
                if (_proxy == null)
                {
                    _proxy = GetWebService();
                }
                LogManager.WriteLog("Transaction_Id for ExportVaultTransaction is : " + Transaction_Id, LogManager.enumLogLevel.Info);

                XMLString = DataBaseServiceHandler.ExecuteScalar<string>(GetConnectionString(), CommandType.StoredProcedure, "rsp_Vault_GetTransactionForExport",
                           DataBaseServiceHandler.AddParameter<int>("@Transaction_Id", DbType.Int32, Transaction_Id));
                if (XMLString.Trim().Length > 0)
                {
                    LogManager.WriteLog("ExportVaultTransaction:-->" + "WrappingXMLData:--->" + XMLString, LogManager.enumLogLevel.Info);
                    bool wsCallSuccess = WrapExportXMLData(XMLString, ehid.ToString(), "VAULTTRANSACTION");
                    if (wsCallSuccess)
                    {
                        LogManager.WriteLog("EXPORT_TRANSACTION:" + "Exported Vault Transaction Data Succcesfully ::", LogManager.enumLogLevel.Info);
                        bStatus = true;
                    }
                    else
                    {
                        LogManager.WriteLog("ExportVaultTransaction :" + "status update Failed to Export: MH ID: " + Transaction_Id + " And EH ID : " + ehid, LogManager.enumLogLevel.Error);
                        bStatus = false;
                    }
                }
                return bStatus;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("ExportVaultTransaction :" + "status update Failed to Export: MH ID: " + Transaction_Id + " And EH ID : " + ehid, LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);

                return bStatus;
            }
        }

        public static bool ExportEnrollVault(int ehid, int Installation_No)
        {
            string XMLString = string.Empty;
            bool bStatus = false;

            try
            {
                if (_proxy == null)
                {
                    _proxy = GetWebService();
                }
                LogManager.WriteLog("Installation_No for ExportEnrollVault is : " + Installation_No, LogManager.enumLogLevel.Info);

                XMLString = DataBaseServiceHandler.ExecuteScalar<string>(GetConnectionString(), CommandType.StoredProcedure, "rsp_Vault_EnrollmentInXML",
                           DataBaseServiceHandler.AddParameter<int>("@Installation_No", DbType.Int32, Installation_No));
                if (XMLString.Trim().Length > 0)
                {
                    LogManager.WriteLog("ExportVaultTransaction:-->" + "WrappingXMLData:--->" + XMLString, LogManager.enumLogLevel.Info);
                    bool wsCallSuccess = WrapExportXMLData(XMLString, ehid.ToString(), "ENROLLVAULT");
                    if (wsCallSuccess)
                    {
                        LogManager.WriteLog("ExportEnrollVault:" + "Export Enroll Vault Data Succcesfully ::", LogManager.enumLogLevel.Info);
                        bStatus = true;
                    }
                    else
                    {
                        LogManager.WriteLog("ExportEnrollVault :" + "status update Failed to Export: Installation ID: " + Installation_No + " And EH ID : " + ehid, LogManager.enumLogLevel.Error);
                        bStatus = false;
                    }
                }
                return bStatus;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("ExportEnrollVault :" + "status update Failed to Export: Installation ID: " + Installation_No + " And EH ID : " + ehid, LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);

                return bStatus;
            }
        } 

        internal static bool ExportEmpCardSessions(int EmpID, int EH_ID)
        {
            string EmpCardSessionsXML = string.Empty;
            bool ExportStatus = false;
            _proxy = GetWebService();

            try
            {
                //Create the xml for InstallationGameData
                EmpCardSessionsXML = DataBaseServiceHandler.ExecuteScalar<string>(GetConnectionString(), CommandType.StoredProcedure,
                    "rsp_GetEmployeeCardSessionDetails",
                    DataBaseServiceHandler.AddParameter<int>("@EmpID", DbType.Int32, EmpID));

                LogManager.WriteLog("Employee card sessions XML : " + EmpCardSessionsXML, LogManager.enumLogLevel.Info);
                if (string.IsNullOrEmpty(EmpCardSessionsXML))
                    return true;

                EmpCardSessionsXML = EmpCardSessionsXML.Replace(Environment.NewLine, "");
                EmpCardSessionsXML = EmpCardSessionsXML.Replace("\r", "");
                EmpCardSessionsXML = EmpCardSessionsXML.Replace("\n", "");
                EmpCardSessionsXML = EmpCardSessionsXML.Replace("\r\n", "");

                LogManager.WriteLog("Employee Card Session Data Export XML " + EmpCardSessionsXML, LogManager.enumLogLevel.Info);

                //insert the game info in corresponding Enteprise
                ExportStatus = _proxy.ImportEmployeeCardSessionData(EmpCardSessionsXML);
                LogManager.WriteLog("Status for Game Data Export " + ExportStatus.ToString(), LogManager.enumLogLevel.Info);

                return ExportStatus;

            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                return false;
            }
        }

        public static bool UpdateLicenseKey(int iLicenseInfoId)
        {
            try
            {
                LogManager.WriteLog("Inside UpdateLicenseKey method", LogManager.enumLogLevel.Info);

                SqlParameter[] sqlparams = new SqlParameter[5];

                sqlparams[0] = new SqlParameter("@LicenseInfoId", iLicenseInfoId);
                sqlparams[1] = new SqlParameter();
                sqlparams[1].ParameterName = "LicenseKey";
                sqlparams[1].Direction = ParameterDirection.Output;
                sqlparams[1].Value = string.Empty;
                sqlparams[1].SqlDbType = SqlDbType.VarChar;
                sqlparams[1].Size = 100;

                sqlparams[2] = new SqlParameter();
                sqlparams[2].ParameterName = "UserName";
                sqlparams[2].Direction = ParameterDirection.Output;
                sqlparams[2].Value = string.Empty;
                sqlparams[2].SqlDbType = SqlDbType.VarChar;
                sqlparams[2].Size = 50;

                sqlparams[3] = new SqlParameter();
                sqlparams[3].ParameterName = "SiteCode";
                sqlparams[3].Direction = ParameterDirection.Output;
                sqlparams[3].Value = string.Empty;
                sqlparams[3].SqlDbType = SqlDbType.VarChar;
                sqlparams[3].Size = 50;
                sqlparams[4] = new SqlParameter();
                sqlparams[4].ParameterName = "ActivatedDateTime";
                sqlparams[4].Direction = ParameterDirection.Output;
                sqlparams[4].Value = null;
                sqlparams[4].SqlDbType = SqlDbType.DateTime;

                SqlHelper.ExecuteNonQuery(GetConnectionString(), System.Data.CommandType.StoredProcedure, "rsp_SL_GetLicenseKey", sqlparams);
                if (sqlparams[3].Value != null || sqlparams[3].Value.ToString() != string.Empty)
                {
                    if (_proxy == null)
                    {
                        _proxy = GetWebService();
                    }
                    _proxy.UpdateLicenseActivation(SiteLicensingCryptoHelper.Decrypt(sqlparams[1].Value.ToString(), "B411y51T"), sqlparams[3].Value.ToString(), sqlparams[2].Value.ToString(), Convert.ToDateTime(sqlparams[4].Value));
                    return true;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            return false;
        }

        public static bool SetFactoryReset(int Mode_ID,string EH_ID)
        {
            var sqlParameters = new SqlParameter[1];
            var sqlParameter = new SqlParameter
            {
                ParameterName = "Mode_ID",
                Value = Mode_ID,
                Direction = ParameterDirection.Input
            };
            sqlParameters[0] = sqlParameter;

            string strXmlFactoryReset = SqlHelper.ExecuteScalar(GetConnectionString(),
                                                                          CommandType.StoredProcedure,
                                                                          "rsp_GetFactoryDetail",
                                                                          sqlParameters).ToString();

            if (strXmlFactoryReset.Trim().Length > 0)
            {
                bool wsCallSuccess = WrapExportXMLData(strXmlFactoryReset, EH_ID, "FACTORYRESET");
                if (wsCallSuccess)
                {
                    LogManager.WriteLog("RESET DETAIL ::" + "EXPORTED FR DETAIL SUCCESSFULLY ::", LogManager.enumLogLevel.Error);
                    return true;
                }

                throw new Exception("RESET DETAIL  ::" + "EXPORTED FR DETAIL SUCCESSFULLY :: EHID :" + EH_ID);
            }
            LogManager.WriteLog("RESET DETAIL  ::" + "EXPORTED FR DETAIL FAILED :: - XML Data Blank", LogManager.enumLogLevel.Error);
            return false;
        }

        public static bool GetHQInstallationNo(int Installation_No, string EH_ID)
        {
            string sXMLStockNo = string.Empty;            

            try
            {
                var sqlParameters = new SqlParameter[1];
                var sqlParameter = new SqlParameter
                {
                    ParameterName = "Installation_No",
                    Value = Installation_No,
                    Direction = ParameterDirection.Input
                };
                sqlParameters[0] = sqlParameter;

                sXMLStockNo = SqlHelper.ExecuteScalar(GetConnectionString(),CommandType.StoredProcedure,"rsp_GetMachineDetail",sqlParameters).ToString();

                if (sXMLStockNo.Trim().Length > 0)
                {
                    string strDataType = string.Empty;
                    string strData = string.Empty;
                    try
                    {
                        LogManager.WriteLog("GetHQInstallationNo->Parse request Info :" + sXMLStockNo, LogManager.enumLogLevel.Info);
                        XmlDocument xmlDoc = new XmlDocument();
                        xmlDoc.LoadXml(sXMLStockNo);
                        strDataType = xmlDoc.GetElementsByTagName("Type").Item(0).InnerXml;
                        XmlNodeList Node = xmlDoc.GetElementsByTagName("Data");
                        if (Node.Count > 0)
                            strData = Node.Item(0).InnerXml;
                        
                        if (string.IsNullOrEmpty(strData))
                            return false;
                    }
                    catch (Exception Ex)
                    {
                        ExceptionManager.Publish(Ex);
                        return false;
                    }
                    
                    _proxy = GetWebService();
                    string strEnterpriseResponse = _proxy.GetCommonData(DataHelper._SiteCode.ToString(), strDataType, sXMLStockNo);
                    LogManager.WriteLog("GetHQInstallationNo->Enterprise response: " + strEnterpriseResponse, LogManager.enumLogLevel.Info);


                    SqlParameter[] sqlparams = new SqlParameter[2];
                    sqlparams[0] = new SqlParameter("@DataType", strDataType);
                    sqlparams[1] = new SqlParameter("@XMLDATA", strEnterpriseResponse);

                    if (strEnterpriseResponse != string.Empty)
                    {
                        SqlHelper.ExecuteNonQuery(GetConnectionString(), System.Data.CommandType.StoredProcedure, "usp_ImportCommonData", sqlparams);
                        LogManager.WriteLog("GetHQInstallationNo->Updated to DB", LogManager.enumLogLevel.Info);
                        return true;
                    }                    
                }

            }
            catch (Exception ex)
            {
               ExceptionManager.Publish(ex);
            }

            return false;
        }
        
        internal static bool ImportCommonData(string Data)
        {
            
            try
            {
                string strDataType= string.Empty;
                string strData= string.Empty;


                try
                {
                /*  --Sample input xml  for commondata import                  
                    <XmlData>
                    <Type>MANUFACTURER</Type>
                    <Data>testDate</Data>
                    </XmlData>
                 * */
                   LogManager.WriteLog("ImportCommonData->Parse request Info :" + Data, LogManager.enumLogLevel.Info);
                   XmlDocument xmlDoc = new XmlDocument();
                   xmlDoc.LoadXml(Data);
                   strDataType = xmlDoc.GetElementsByTagName("Type").Item(0).InnerXml;
                   XmlNodeList Node = xmlDoc.GetElementsByTagName("Data");
                   if (Node.Count>0)
                       strData = Node.Item(0).InnerXml;

                }
                catch(Exception Ex)
                {
                    ExceptionManager.Publish(Ex);
                    return false;
                }
                _proxy = GetWebService();
                string strEnterpriseResponse = _proxy.GetCommonData(DataHelper._SiteCode.ToString(), strDataType, strData);
                LogManager.WriteLog("ImportCommonData->Enterprise request complete", LogManager.enumLogLevel.Info);


                SqlParameter[] sqlparams = new SqlParameter[2];
                sqlparams[0] = new SqlParameter("@DataType", strDataType);
                sqlparams[1] = new SqlParameter("@XMLDATA", strEnterpriseResponse);

                if (strEnterpriseResponse != string.Empty)
                {
                    SqlHelper.ExecuteNonQuery(GetConnectionString(), System.Data.CommandType.StoredProcedure, "usp_ImportCommonData", sqlparams);
                    LogManager.WriteLog("ImportCommonData->Updated to DB", LogManager.enumLogLevel.Info);
                }
                else
                    LogManager.WriteLog("ImportCommonData->No Data from Enterprise", LogManager.enumLogLevel.Info);



                return true;

            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                return false;
            }
        }

        public static string GetServiceNames()
        {
            try
            {
                return SqlHelper.ExecuteScalar(GetConnectionString(), System.Data.CommandType.Text, "Select Setting_Value from setting where Setting_Name = 'ServiceNames'").ToString();
            }
            catch (Exception ex)
            {
                LogManager.WriteLog(ex.Message, LogManager.enumLogLevel.Error);
                return string.Empty;
            }
        }

        public static FloorPositionDto GetFloorPosition(int userID)
        {
            LogManager.WriteLog("GetFloorPosition called.", LogManager.enumLogLevel.Info);
            FloorPositionDto result = new FloorPositionDto();

            try
            {
                using (BMC.CoreLib.Data.Database db = BMC.CoreLib.Data.DbFactory.OpenDB(GetConnectionString()))
                {
                    DbParameter[] parameters = db.CreateParameters(1);
                    parameters[0] = db.CreateParameter("@UserID", DbType.Int32, userID);
                    DataSet ds = db.ExecuteDataset("dbo.rsp_GetFloorPosition", parameters);
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dt = ds.Tables[0];
                        foreach (DataRow dr in dt.Rows)
                        {
                            result.Modify(userID, dr.Field<int>("Bar_Pos_No"),
                                         dr.Field<int>("FloorLeft"),
                                         dr.Field<int>("FloorTop"),
                                         dr.Field<int>("RowNo"));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            return result;
        }

        public static void SaveFloorPosition(int slotID, int securityUserID, int topPosition, int leftPosition)
        {
            LogManager.WriteLog("SaveFloorPosition called.", LogManager.enumLogLevel.Info);

            try
            {
                DataBaseServiceHandler.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "usp_SetBarPositionDetailsForCashDeskoperator",
                    DataBaseServiceHandler.AddParameter<int>("@Bar_Pos_No", DbType.Int32, slotID),
                    DataBaseServiceHandler.AddParameter<int>("@SecurityUserID", DbType.Int32, securityUserID),
                    DataBaseServiceHandler.AddParameter<int>("@Top", DbType.Int32, topPosition),
                    DataBaseServiceHandler.AddParameter<int>("@Left", DbType.Int32, leftPosition),
                    DataBaseServiceHandler.AddParameter<string>("@ComputerName", DbType.String, Environment.MachineName));
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
        }


        #region IncomingVaultMessages

        public static bool CheckSqlConnectionExists()
        {
            bool retval = false;
            try
            {
                using (SqlConnection cn = new SqlConnection(GetConnectionString()))
                {
                    retval = true;
                }
            }
            catch (Exception)
            {
                LogManager.WriteLog("CheckSqlConnection --> Unable to establish connection", LogManager.enumLogLevel.Error);
            }
            return retval;
        }
        //Vault Incoming Messages
        public static DataTable GetIncomingMessages()
        {
            var vaultMessages = new DataSet();
            try
            {

                vaultMessages = SqlHelper.ExecuteDataset(GetConnectionString(), CommandType.StoredProcedure, "rsp_Vault_GetIncomingMessages");
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            return vaultMessages.Tables.Count > 0 ? vaultMessages.Tables[0] : new DataTable();
        }

        public static bool ProcessIncomingMessages(long RequestID, string EventType, int EventID, string XmlData, bool SkipErrorMessage)
        {
            LogManager.WriteLog("Inside ProcessIncomingMessages", LogManager.enumLogLevel.Debug);
            try
            {
                SqlParameter[] sqlparams = new SqlParameter[5];
                sqlparams[0] = new SqlParameter("RequestID", RequestID);
                sqlparams[1] = new SqlParameter("EventType", EventType.ToUpper());
                sqlparams[2] = new SqlParameter("EventID", EventID);
                sqlparams[3] = new SqlParameter("XmlData", XmlData);
                sqlparams[4] = new SqlParameter("SkipErrorMessage", SkipErrorMessage.ToString());

                int result = SqlHelper.ExecuteNonQuery(GetConnectionString(), "rsp_Vault_ProcessMessages", sqlparams);

                return result > 0;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("ProcessIncomingMessages->Exception:" + ex.Message, LogManager.enumLogLevel.Error);
                return false;
            }
        }
     
   
        /// <summary>
        /// Export Alert Details to Enterprise.
        /// </summary>
        /// <param name="EHReferenceID"></param>
        /// <param name="EHID"></param>
        /// <returns></returns>
        public static bool ExportAlertDetails(long EHReferenceID, string EHID)
        {
            bool bResult = false;
            AlertDetails detail = null;
            try
            {
                // assign the parameters
                SqlParameter[] sqlparams = new SqlParameter[2];
                sqlparams[0] = new SqlParameter("EhReference", EHReferenceID);
                sqlparams[1] = new SqlParameter("EhID", EHID);

                //get the alert details.
                DataSet ds = SqlHelper.ExecuteDataset(GetConnectionString(), "rsp_GetAlertDetails", sqlparams);
                if (ds.Tables.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        detail = new AlertDetails
                        {
                            AlertMessage = row["AlertMessage"].ToString(),
                            AlertType = row["AlertType"].ToString(),
                            ID = (long)row["ID"],
                            AlertStatus = Convert.ToInt16(row["AlertStatus"]),
                            AlertReceivedOn = Convert.ToDateTime(row["AlertReceivedOn"]),
                            SiteCode = row["SiteCode"].ToString()
                        };
                    }
                }

                //invoke the proxy to send details to enterprise.
                if (_proxy == null)
                {
                    _proxy = GetWebService();
                }
                bool bStatus = _proxy.ImportAlertDetails(detail.SiteCode, Serialize(detail).ToString());

                if (bStatus)
                {
                    //Update the status in alert history table.
                    sqlparams[0] = new SqlParameter("ID", detail.ID);
                    sqlparams[1] = new SqlParameter("Status", bStatus);

                    int iresult = SqlHelper.ExecuteNonQuery(GetConnectionString(), "usp_UpdateAlertExportStatus", sqlparams);
                    bResult = true;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("GetAlertDetails->Exception:" + ex.Message, LogManager.enumLogLevel.Error);
                detail = new AlertDetails();
            }
            return bResult;
        }

        /// <summary>
        /// Export Email Alert Status Details to Enterprise.
        /// </summary>
        /// <param name="EHReferenceID"></param>
        /// <param name="EHID"></param>
        /// <returns></returns>
        public static bool ExportMailAlertStatus(long EHReferenceID, string EHID)
        {
            bool bResult = false;
            EmailAlertStatusDetails detail = null;
            try
            {
                // assign the parameters
                SqlParameter[] sqlparams = new SqlParameter[2];
                sqlparams[0] = new SqlParameter("EhReference", EHReferenceID);
                sqlparams[1] = new SqlParameter("EhID", EHID);

                //get the alert details.
                object objXML = SqlHelper.ExecuteScalar(GetConnectionString(), "rsp_GetEmailAlertStatusDetails", sqlparams);

                string AlertXML = objXML == null ? string.Empty : objXML.ToString();

                if (!string.IsNullOrEmpty(AlertXML))
                {
                    //invoke the proxy to send details to enterprise.
                    if (_proxy == null)
                    {
                        _proxy = GetWebService();
                    }
                    bool bStatus = _proxy.ImportEmailAlertStatusDetails(AlertXML);
                    bResult = bStatus;
                }

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("ExportMailAlertStatus->Exception:" + ex.Message, LogManager.enumLogLevel.Error);
                detail = new EmailAlertStatusDetails();
            }
            return bResult;
        }

        // source: source object instance to serialize
        // target: target stream to write into.
        static StringWriter Serialize(object source)
        {
            StringWriter stringwriter = new StringWriter();
            XmlWriterSettings settings = new XmlWriterSettings();
            try
            {

                settings.OmitXmlDeclaration = true;
                settings.Indent = true;

                using (XmlWriter writer = XmlWriter.Create(stringwriter, settings))
                {
                    XmlSerializer serializer = new XmlSerializer(source.GetType());

                    XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
                    namespaces.Add(string.Empty, string.Empty);

                    serializer.Serialize(writer, source, namespaces);
                }
            }
            catch (Exception ex)
            { ExceptionManager.Publish(ex); }

            return stringwriter;
        }
    #endregion  
    }
}