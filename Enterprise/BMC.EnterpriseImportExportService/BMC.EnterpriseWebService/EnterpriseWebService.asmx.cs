using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml;
using BMC.Common;
using BMC.Common.Utilities;
using BMC.Common.Compression;
using BMC.Common.ConfigurationManagement;
using BMC.Common.ExceptionManagement;
using BMC.Common.LogManagement;
using BMC.Common.Security;
using BMC.DataAccess;
using Microsoft.Win32;

namespace BMC.EnterpriseWebService
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    public class EnterpriseWebService : WebService
    {
        #region Private Fields

        private SqlTransaction _sqlTran;

        #endregion

        #region Public Fields

        public AuthenticationInformation AuthenticationInfo;

        #endregion

        #region Ctor

        #endregion

        #region Private Methods

        private static string GetEnterprisePasskey()
        {
            //RegistryKey key = Registry.LocalMachine.OpenSubKey(ConfigManager.Read("RegistryPath"));
            //if (key != null) return key.GetValue("EnterpriseKey").ToString();
            return BMCRegistryHelper.GetRegKeyValue(ConfigManager.Read("RegistryPath"), "EnterpriseKey");
            ExceptionManager.Publish(new Exception("Enterprise key not set"));
            return "";
        }

        private string GetExchangePasskey()
        {
            ConfigManager.SetConfigurationMode(ConfigManager.ConfigurationMode.AppConfig);
            //RegistryKey key = Registry.LocalMachine.OpenSubKey(ConfigManager.Read("RegistryPath"));
            //if (key != null)
            //{
            return DataBaseServiceHandler.ExecuteScalar<string>
                    (GetConnectionString(),
                     CommandType.Text,
                     "Select isNull(ExchangeKey, '0000') As ExchangePasskey from Site Where Site_Code = '" +
                     AuthenticationInfo.SiteCode + "'");
            //}

            ExceptionManager.Publish(new Exception("Exchange key not set"));
            return "Exchange";
        }
        //
        private static string GetConnectionString()
        {
            //RegistryKey regKey;
            //string sqlConnect = "";
            //ConfigManager.SetConfigurationMode(ConfigManager.ConfigurationMode.AppConfig);
            //try
            //{
            //    ConfigManager.Read("RegistryPath");
            //    regKey = Registry.LocalMachine.OpenSubKey(ConfigManager.Read("RegistryPath"));
            //    if (regKey == null) throw new InvalidDataException("ConnectionString not found");
            //    return sqlConnect = regKey.GetValue("SQLConnect").ToString();
            //}
            //catch (Exception ex)
            //{
            //    LogManager.WriteLog("Error reading registry:" + ex.Message, LogManager.enumLogLevel.Error);
            //}
            //return sqlConnect;
            return Common.Utilities.DatabaseHelper.GetConnectionString();
        }

        private static void RemoveEscapeCharsFromXml(ref string sXmlData)
        {
            LogManager.WriteLog("Removing esc chars from string", LogManager.enumLogLevel.Debug);

            sXmlData = sXmlData.Replace(Environment.NewLine, "");
            sXmlData = sXmlData.Replace("\r", "");
            sXmlData = sXmlData.Replace("\n", "");
            sXmlData = sXmlData.Replace("\r\n", "");
        }

        private void CheckSecurity()
        {
            Exception ex;
            if (AuthenticationInfo == null)
            {
                ex = new Exception("ErrorCode: WS1001 No or Missing header Info.  Please validate");
                ExceptionManager.Publish(ex);
                throw ex;
            }
            if (string.IsNullOrEmpty(AuthenticationInfo.EnterprisePassKey) ||
                AuthenticationInfo.EnterprisePassKey != GetEnterprisePasskey())
            {
                ex = new Exception("ErrorCode: WS1002 EnterprisePassKey is Null, Empty or Invalid.  Please validate");
                ExceptionManager.Publish(ex);
                throw ex;
            }
            if (AuthenticationInfo.ExchangePassKey != GetExchangePasskey())
            {
                ex = new Exception("ErrorCode: WS1003 ExchangePassKey is Null, Empty or Invalid.  Please validate");
                ExceptionManager.Publish(ex);
                throw ex;
            }
        }

        #endregion

        #region Public Methods

        [WebMethod, SoapHeader("AuthenticationInfo")]
        [SoapSecurityExtension(Encrypt = EncryptMode.Response)]
        public bool ImportData(string xmlString)
        {
            CheckSecurity();
            if (xmlString == null) throw new ArgumentNullException("xmlString");

            LogManager.WriteLog("ImportData Called...", LogManager.enumLogLevel.Info);

            var oSiteXmlDoc = new XmlDocument();
            bool bSuccess = false;

            try
            {
                oSiteXmlDoc.LoadXml(xmlString);
                if (oSiteXmlDoc.DocumentElement == null) throw new InvalidDataException("Input XML not in correct format");
                XmlNodeList oSiteNodes = oSiteXmlDoc.DocumentElement.GetElementsByTagName("Site");
                string sSiteCode = oSiteNodes.Item(0).ChildNodes[0].InnerText;
                string strXmlType = oSiteNodes.Item(0).ChildNodes[1].InnerText;
                if (strXmlType == "VAULTDROP" || strXmlType == "VAULTTRANSACTIONEVENT" || strXmlType == "VAULTEVENT" || strXmlType == "VAULTBALANCE")
                    LogManager.WriteLog("ImportData --> XMLType --> " + strXmlType, LogManager.enumLogLevel.Info);
                string sEhid = oSiteNodes.Item(0).ChildNodes[2].InnerText;
                string strXmlFromSite = oSiteNodes.Item(0).ChildNodes[3].InnerXml;
                if (strXmlFromSite.ToUpper().Contains(@"<MH_PROCESS>__RAMRESET</MH_PROCESS>") || strXmlFromSite.ToUpper().Contains(@"<MH_PROCESS>PARTIAL</MH_PROCESS>"))
                {
                    return true;
                }
                //if (strXmlType.ToUpper()=="__RAMRESET" || strXmlType.ToUpper()=="PARTIAL")
                //{
                //    return true;
                //}

                var oEventParam = new SqlParameter[4];
                const string strSpName = "esp_Import_SiteXML";

                var objSqlParam = new SqlParameter
                                      {
                                          ParameterName = "Site_Code",
                                          Value = sSiteCode,
                                          Direction = ParameterDirection.Input
                                      };
                oEventParam[0] = objSqlParam;

                objSqlParam = new SqlParameter
                                  {
                                      ParameterName = "Type",
                                      Value = strXmlType,
                                      Direction = ParameterDirection.Input
                                  };
                oEventParam[1] = objSqlParam;

                objSqlParam = new SqlParameter
                                  {
                                      ParameterName = "SiteXML",
                                      Value = strXmlFromSite,
                                      SqlDbType = SqlDbType.Text,
                                      Direction = ParameterDirection.Input
                                  };
                oEventParam[2] = objSqlParam;

                objSqlParam = new SqlParameter
                                  {
                                      ParameterName = "EH_ID",
                                      Value = sEhid,
                                      SqlDbType = SqlDbType.Int,
                                      Direction = ParameterDirection.Input
                                  };
                oEventParam[3] = objSqlParam;

                int iRecordInserted = SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure,
                                                                strSpName, oEventParam);
                if (iRecordInserted > 0)
                    bSuccess = true;
                LogManager.WriteLog("ImportData WS " + "  Success value " + bSuccess, LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                bSuccess = false;
            }
            return bSuccess;
        }

        [WebMethod, SoapHeader("AuthenticationInfo")]
        [SoapSecurityExtension(Encrypt = EncryptMode.Response)]
        public bool ImportSiteConfig(string xmlString)
        {
            CheckSecurity();
            if (xmlString == null) throw new ArgumentNullException("xmlString");

            LogManager.WriteLog("ImportSiteConfig Called...", LogManager.enumLogLevel.Info);

            bool bSuccess = false;

            try
            {
                var oEventParam = new SqlParameter[4];
                const string strSpName = "usp_importSiteConfigFromExchange";

                var objSqlParam = new SqlParameter
                {
                    ParameterName = "XML",
                    Value = xmlString,
                    SqlDbType = SqlDbType.Xml,
                    Direction = ParameterDirection.Input
                };
                oEventParam[0] = objSqlParam;

                object iRecordInserted = SqlHelper.ExecuteScalar(GetConnectionString(), CommandType.StoredProcedure,
                                                                strSpName, oEventParam);
                if (Convert.ToInt32(iRecordInserted) == 0)
                    bSuccess = true;
                LogManager.WriteLog("ImportSiteConfig WS " + "  Success value " + bSuccess, LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("ImportSiteConfig" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
                bSuccess = false;
            }
            return bSuccess;
        }

        [WebMethod, SoapHeader("AuthenticationInfo")]
        [SoapSecurityExtension(Encrypt = EncryptMode.Response)]
        public int ImportGameCapping(string xmlString)
        {
            CheckSecurity();
            if (xmlString == null) throw new ArgumentNullException("xmlString");

            LogManager.WriteLog("ImportGameCapping Called...", LogManager.enumLogLevel.Info);

            int bSuccess = -1;

            try
            {
                var oEventParam = new SqlParameter[2];                

                var objSqlParam = new SqlParameter
                {
                    ParameterName = "XML",
                    Value = xmlString,                    
                    Direction = ParameterDirection.Input
                };
                oEventParam[0] = objSqlParam;

                var objSqlParam1 = new SqlParameter
                {
                    ParameterName = "IsSuccess",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                };
                oEventParam[1] = objSqlParam1;

                SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "usp_ImportGameCappingDetailsFromXML",
                                                       oEventParam);

                int iSuccessVal = int.Parse(oEventParam[1].SqlValue.ToString());

                if (iSuccessVal > 0)
                    bSuccess = iSuccessVal;

                LogManager.WriteLog("ImportGameCapping WS " + "  Success value " + bSuccess, LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("ImportGameCapping" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
            }
            return bSuccess;
        }

        [WebMethod, SoapHeader("AuthenticationInfo")]
        [SoapSecurityExtension(Encrypt = EncryptMode.Response)]
        public bool ImportCompressedData(string compressedData)
        {
            CheckSecurity();

            var actualData = CompressionHelper.Decompress(compressedData);
            return ImportData(actualData);
        }

        [WebMethod, SoapHeader("AuthenticationInfo")]
        [SoapSecurityExtension(Encrypt = EncryptMode.Response)]
        public string LogSiteEvent(string sEventXml)
        {
            CheckSecurity();

            LogManager.WriteLog("LogSiteEvent Called...", LogManager.enumLogLevel.Info);
            var oEventXmLsDoc = new XmlDocument();

            string strSuccesstrEventIDs = string.Empty;
            string strFailureEventIDs = string.Empty;
            string strErrorMEssage = string.Empty;

            try
            {


                oEventXmLsDoc.LoadXml(sEventXml);
                if (oEventXmLsDoc.DocumentElement != null)
                {


                    XmlNodeList oEventIDNodeList = oEventXmLsDoc.DocumentElement.GetElementsByTagName("Event");
                    bool bDbCallSuccess = false;

                    foreach (XmlNode oXmlEventNode in oEventIDNodeList)
                    {
                        var strEventID = oXmlEventNode.ChildNodes[0].InnerXml;
                        var strInstallationNo = oXmlEventNode.ChildNodes[1].InnerXml;
                        var strFaultSource = oXmlEventNode.ChildNodes[2].InnerXml;
                        var strFaultType = oXmlEventNode.ChildNodes[3].InnerXml;
                        var strFaultDateTime = oXmlEventNode.ChildNodes[4].InnerXml;
                        var strSiteName = oXmlEventNode.ChildNodes[5].InnerXml;
                        var strFaultDetails = oXmlEventNode.ChildNodes[6].InnerXml;

                        var oEventParam = new SqlParameter[8];

                        var sqlParameter = new SqlParameter
                                                        {
                                                            ParameterName = "InstallationID",
                                                            Value = strInstallationNo,
                                                            SqlDbType = SqlDbType.Int,
                                                            Direction = ParameterDirection.Input
                                                        };
                        oEventParam[0] = sqlParameter;

                        sqlParameter = new SqlParameter
                                          {
                                              ParameterName = "Site_Name",
                                              Value = strSiteName,
                                              Direction = ParameterDirection.Input
                                          };
                        oEventParam[1] = sqlParameter;

                        sqlParameter = new SqlParameter
                                          {
                                              ParameterName = "Fault_Source_ID",
                                              Value = strFaultSource,
                                              Direction = ParameterDirection.Input
                                          };
                        oEventParam[2] = sqlParameter;

                        sqlParameter = new SqlParameter
                                          {
                                              ParameterName = "Fault_Type_ID",
                                              Value = strFaultType,
                                              Direction = ParameterDirection.Input
                                          };
                        oEventParam[3] = sqlParameter;

                        sqlParameter = new SqlParameter
                                          {
                                              ParameterName = "@Fault_Details",
                                              Value = strFaultDetails,
                                              Direction = ParameterDirection.Input
                                          };
                        oEventParam[4] = sqlParameter;

                        sqlParameter = new SqlParameter
                                          {
                                              ParameterName = "DateTime",
                                              Value = strFaultDateTime,
                                              Direction = ParameterDirection.Input
                                          };
                        oEventParam[5] = sqlParameter;

                        sqlParameter = new SqlParameter
                        {
                            ParameterName = "Event_Auto_Closed",
                            Value = 0,
                            Direction = ParameterDirection.Input
                        };
                        oEventParam[6] = sqlParameter;


                        sqlParameter = new SqlParameter("@RETURN_VALUE", SqlDbType.Int) { Direction = ParameterDirection.ReturnValue };
                        oEventParam[7] = sqlParameter;

                        SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "usp_LogSiteEvent", oEventParam);
                        if (int.Parse(oEventParam[6].Value.ToString()) == 0)
                            bDbCallSuccess = true;
                        else if (oEventParam[6].Value.ToString() == "-99")
                            strErrorMEssage = "Open service already exists.";

                        if (bDbCallSuccess)
                        {
                            strSuccesstrEventIDs = strSuccesstrEventIDs + strEventID + ",";
                            LogManager.WriteLog("Successful call " + strSuccesstrEventIDs, LogManager.enumLogLevel.Info);
                        }
                        else
                        {
                            strFailureEventIDs = strFailureEventIDs + strEventID + ",";
                            LogManager.WriteLog("Failure call " + strSuccesstrEventIDs, LogManager.enumLogLevel.Info);
                        }
                    }
                }

                if (strSuccesstrEventIDs.Length > 1)
                    strSuccesstrEventIDs = strSuccesstrEventIDs.Remove(strSuccesstrEventIDs.Length - 1, 1);

                if (strFailureEventIDs.Length > 1)
                    strFailureEventIDs = strFailureEventIDs.Remove(strFailureEventIDs.Length - 1, 1);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                strErrorMEssage = ex.Message;
            }

            return "<OutputXML><ErrorMessage>" + strErrorMEssage + "</ErrorMessage><SuccesstrEventIDs>" +
                                  strSuccesstrEventIDs + "</SuccesstrEventIDs><FailureEventIDs>" + strFailureEventIDs +
                                  "</FailureEventIDs></OutputXML>";
        }

        [WebMethod, SoapHeader("AuthenticationInfo")]
        [SoapSecurityExtension(Decrypt = DecryptMode.Request)]
        public int HelloWebService(int recieve)
        {
            //CheckSecurity();
            return recieve;
        }

        [WebMethod, SoapHeader("AuthenticationInfo")]
        [SoapSecurityExtension(Encrypt = EncryptMode.Response)]
        public string ImportHourlyStatisticsData(string xmlString)
        {
            CheckSecurity();

            LogManager.WriteLog("ImportHourlyStatisticsData Called...", LogManager.enumLogLevel.Info);


            var successHsiDs = string.Empty;
            var failureHsiDs = string.Empty;
            var strErrorMessage = string.Empty;
            var oHstrXmlDoc = new XmlDocument();

            try
            {
                oHstrXmlDoc.LoadXml(xmlString);
                var objXmlhsNodeList = oHstrXmlDoc.DocumentElement.GetElementsByTagName("Hourly_Statistics");

                foreach (XmlNode objXmlhsNode in objXmlhsNodeList)
                {
                    var hsid = objXmlhsNode.SelectSingleNode("HS_ID").InnerText;
                    var outerXml = objXmlhsNode.OuterXml;

                    var objSqlhsParams = new SqlParameter[2];


                    var objSqlParam = new SqlParameter
                                          {
                                              ParameterName = "doc",
                                              Value = outerXml,
                                              Direction = ParameterDirection.Input
                                          };
                    objSqlhsParams[0] = objSqlParam;

                    objSqlParam = new SqlParameter
                                      {
                                          ParameterName = "IsSuccess",
                                          SqlDbType = SqlDbType.Int,
                                          Direction = ParameterDirection.Output
                                      };
                    objSqlhsParams[1] = objSqlParam;

                    SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "usp_InsertHourlyStatisticsfromXML",
                                                           objSqlhsParams);

                    int iSuccessVal = int.Parse(objSqlhsParams[1].SqlValue.ToString());
                    LogManager.WriteLog("ImportHourlyStatisticsData WS" + "  iSuccessVal value " + iSuccessVal,
                                        LogManager.enumLogLevel.Info);

                    if (iSuccessVal == 0)
                        successHsiDs = successHsiDs + hsid + ",";
                    else
                        failureHsiDs = failureHsiDs + hsid + ",";
                }

                if (successHsiDs.Length > 1)
                    successHsiDs = successHsiDs.Remove(successHsiDs.Length - 1, 1); //Remove the additional ','

                if (failureHsiDs.Length > 1)
                    failureHsiDs = failureHsiDs.Remove(failureHsiDs.Length - 1, 1); //Remove the additional ','
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                strErrorMessage = ex.Message;
            }

            return "<OutputXML><ErrorMessage>" + strErrorMessage + "</ErrorMessage><SuccessHSIDs>" +
                                  successHsiDs + "</SuccessHSIDs><FailureHSIDs>" + failureHsiDs +
                                  "</FailureHSIDs></OutputXML>";
        }

        [WebMethod, SoapHeader("AuthenticationInfo")]
        [SoapSecurityExtension(Encrypt = EncryptMode.Response)]
        public int InsertTreasuryEntries(string strXml)
        {
            CheckSecurity();

            string strSiteCode;
            string strXmlType;
            string strEhid;
            var oSiteXmlDoc = new XmlDocument();
            string strXmlTreasuryEntry;
            int iTreasuryId = 0;

            try
            {
                oSiteXmlDoc.LoadXml(strXml);
                var oSiteNodes = oSiteXmlDoc.DocumentElement.GetElementsByTagName("Site");
                strSiteCode = oSiteNodes.Item(0).ChildNodes[0].InnerText;
                strXmlType = oSiteNodes.Item(0).ChildNodes[1].InnerText;
                strEhid = oSiteNodes.Item(0).ChildNodes[2].InnerText;
                strXmlTreasuryEntry = oSiteNodes.Item(0).ChildNodes[3].InnerXml;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return iTreasuryId;
            }

            const string strSpInsertTreasuryName = "usp_InsertTreasuryfromXML";
            try
            {
                LogManager.WriteLog("Inserting into Treasury Started", LogManager.enumLogLevel.Info);

                var objSqlParamTreasury = new SqlParameter[2];
                var objSqlParam = new SqlParameter
                                      {
                                          ParameterName = "@doc",
                                          Value = strXmlTreasuryEntry,
                                          Direction = ParameterDirection.Input
                                      };
                objSqlParamTreasury[0] = objSqlParam;

                objSqlParam = new SqlParameter
                                  {
                                      ParameterName = "TreasuryID",
                                      SqlDbType = SqlDbType.Int,
                                      Direction = ParameterDirection.Output
                                  };
                objSqlParamTreasury[1] = objSqlParam;

                SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure,
                                          strSpInsertTreasuryName, objSqlParamTreasury);

                if (Convert.ToInt32(objSqlParamTreasury[1].Value) > 0)
                {
                    iTreasuryId = Convert.ToInt32(objSqlParamTreasury[1].Value);
                    LogManager.WriteLog("Inserting into Treasury done succesfully. With Treasury ID" + iTreasuryId,
                                        LogManager.enumLogLevel.Info);
                }
                else if (Convert.ToInt32(objSqlParamTreasury[1].Value) < 0)
                {
                    iTreasuryId = -1;
                    LogManager.WriteLog("Tried to Insert into Treasury. Failed with Code" + iTreasuryId,
                                        LogManager.enumLogLevel.Info);
                }
                else
                {
                    iTreasuryId = 0;
                    LogManager.WriteLog("Tried to Insert into Treasury. Failed with Code" + iTreasuryId,
                                        LogManager.enumLogLevel.Info);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            if (iTreasuryId > 0)
            {
                var oEventParam = new SqlParameter[4];

                var objSqlParam = new SqlParameter
                                      {
                                          ParameterName = "Site_Code",
                                          Value = strSiteCode,
                                          Direction = ParameterDirection.Input
                                      };
                oEventParam[0] = objSqlParam;

                objSqlParam = new SqlParameter
                                  {
                                      ParameterName = "Type",
                                      Value = strXmlType,
                                      Direction = ParameterDirection.Input
                                  };
                oEventParam[1] = objSqlParam;

                objSqlParam = new SqlParameter
                                  {
                                      ParameterName = "SiteXML",
                                      Value = strXmlTreasuryEntry,
                                      SqlDbType = SqlDbType.Text,
                                      Direction = ParameterDirection.Input
                                  };
                oEventParam[2] = objSqlParam;

                objSqlParam = new SqlParameter
                                  {
                                      ParameterName = "EH_ID",
                                      Value = strEhid,
                                      SqlDbType = SqlDbType.Int,
                                      Direction = ParameterDirection.Input
                                  };
                oEventParam[3] = objSqlParam;

                SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "esp_Import_SiteXML",
                                          oEventParam);
            }

            return iTreasuryId;
        }

        [WebMethod, SoapHeader("AuthenticationInfo")]
        [SoapSecurityExtension(Encrypt = EncryptMode.Response)]
        public bool InsertRead(string xmlString)
        {
            CheckSecurity();

            LogManager.WriteLog("InsertRead Called...", LogManager.enumLogLevel.Info);

            var xmlDoc = new XmlDocument();

            xmlDoc.LoadXml(xmlString);
            XmlNodeList xmlNodes = xmlDoc.ChildNodes[0].ChildNodes;

            try
            {
                foreach (XmlNode objXmlNode in xmlNodes)
                {
                    var sqlParameters = new SqlParameter[2];
                    var sqlParameter = new SqlParameter
                                           {
                                               ParameterName = "@doc",
                                               Value = objXmlNode.OuterXml,
                                               Direction = ParameterDirection.Input
                                           };
                    sqlParameters[0] = sqlParameter;

                    sqlParameter = new SqlParameter
                                       {
                                           ParameterName = "IsSuccess",
                                           SqlDbType = SqlDbType.Int,
                                           Direction = ParameterDirection.Output
                                       };
                    sqlParameters[1] = sqlParameter;

                    SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "esp_InsertReadRecordfromXML",
                                              sqlParameters);

                    if (sqlParameters[1].Value.ToString() == "0")
                        return true;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return false;
        }

        /// <summary>
        /// Function is called when a new Installation is created in Exchange. On successfull creation an HQInstallationID
        /// is generated and shared across as a common ID for further transactions between the Exchange and Enterprise
        /// </summary>
        /// <Author></Author>
        /// <param name="installationData">XML string containing Details of the installation</param>
        /// <returns>Returns the HQInstallationID once the data is Saved successfully.  If otherwise, following is returned
        /// (-1) - invalid bar position
        /// (-2) - invalid machine type code
        /// (-3) - invalid game title supplied
        /// (-4) - invalid asset number
        /// (-5) - General SQL Error
        /// </returns> 
        [WebMethod, SoapHeader("AuthenticationInfo")]
        [SoapSecurityExtension(Encrypt = EncryptMode.Response)]
        public Int32 CreateInstallation(string installationData)
        {
            LogManager.WriteLog("CreateInstallation Called...", LogManager.enumLogLevel.Info);
            LogManager.WriteLog(installationData, LogManager.enumLogLevel.Info);

            CheckSecurity();
            try
            {
                RemoveEscapeCharsFromXml(ref installationData);

                var sqlParamsExportBarPositions = new SqlParameter[1];
                var sqlParamsExportBarPosition = new SqlParameter
                                                     {
                                                         ParameterName = "@XML",
                                                         Value = installationData,
                                                         DbType = DbType.AnsiString,
                                                         Direction = ParameterDirection.Input
                                                     };
                sqlParamsExportBarPositions[0] = sqlParamsExportBarPosition;
                LogManager.WriteLog("EnhancedEnrollment: Create Installation", LogManager.enumLogLevel.Info);

                int iResult;
                if (_sqlTran == null)
                {
                    iResult =
                        Int32.Parse(
                            SqlHelper.ExecuteScalar(GetConnectionString(), "USP_GetHQInstallationID",
                                                    sqlParamsExportBarPositions).ToString());
                }
                else
                {
                    iResult =
                        Int32.Parse(
                            SqlHelper.ExecuteScalar(_sqlTran, "USP_GetHQInstallationID", sqlParamsExportBarPositions).
                                ToString());
                }
                LogManager.WriteLog("Return Value For NEW Installation: " + iResult, LogManager.enumLogLevel.Info);
                return iResult;
            }
            catch (SqlException ex)
            {
                ExceptionManager.Publish(ex);
                return -99;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return -90;
            }
        }

        /// <summary>
        /// This function is called for Offline Machine Enrollment.  This will take care of Creation, Convertion and Removal of Installation
        /// </summary>
        /// <param name="strXML">XML string containing Details of the installation</param>
        /// <param name="installType">Type of the install.</param>
        /// <returns>
        /// Returns the Positive integer if Saved successfully.  If otherwise, Transaction is rolledback and a negative number is returns indicating Error"
        /// </returns>
        [WebMethod, SoapHeader("AuthenticationInfo")]
        [SoapSecurityExtension(Encrypt = EncryptMode.Response)]
        public Int32 EnhancedEnrollmentForOffline(string strXML, InstallationType installType)
        {
            LogManager.WriteLog("Step 1", LogManager.enumLogLevel.Info);
            CheckSecurity();
            string strNewInstallationXML = string.Empty;
            string strOldInstallationXML = string.Empty;
            string strFinalXML = string.Empty;
            string strSiteCode = string.Empty;
            string strXMLType = string.Empty;
            string strEHID = string.Empty;
            var SqlConnect = new SqlConnection(GetConnectionString());
            var objXMLDoc = new XmlDocument();
            int iResult;
            iResult = -91;
            LogManager.WriteLog("Step 2", LogManager.enumLogLevel.Info);
            LogManager.WriteLog(strXML, LogManager.enumLogLevel.Info);
            LogManager.WriteLog(installType.ToString(), LogManager.enumLogLevel.Info);
            try
            {
                SqlConnect.Open();
                _sqlTran = SqlConnect.BeginTransaction();
                LogManager.WriteLog("Step 3", LogManager.enumLogLevel.Info);
                objXMLDoc.LoadXml(strXML);
                XmlNodeList oSiteNodes = objXMLDoc.DocumentElement.GetElementsByTagName("Site");
                strSiteCode = oSiteNodes.Item(0).ChildNodes[0].InnerText;
                strXMLType = oSiteNodes.Item(0).ChildNodes[1].InnerText;
                strEHID = oSiteNodes.Item(0).ChildNodes[2].InnerText;
                LogManager.WriteLog("Step 4", LogManager.enumLogLevel.Info);
                switch (installType)
                {
                    case InstallationType.NewInstallation:
                        LogManager.WriteLog("New Installation Entered", LogManager.enumLogLevel.Info);
                        strNewInstallationXML = oSiteNodes.Item(0).ChildNodes[3].InnerXml;
                        strFinalXML = strNewInstallationXML;
                        iResult = CreateInstallation(strNewInstallationXML);
                        break;
                    case InstallationType.GmuChange:
                        LogManager.WriteLog("GMU Installation Entered", LogManager.enumLogLevel.Info);
                        strNewInstallationXML = "<NEWINSTALLATION>" +
                                                oSiteNodes.Item(0).ChildNodes[3].ChildNodes[0].InnerXml +
                                                "</NEWINSTALLATION>";
                        strOldInstallationXML = "<OLDINSTALLATION>" +
                                                oSiteNodes.Item(0).ChildNodes[3].ChildNodes[1].InnerXml +
                                                "</OLDINSTALLATION>";
                        strFinalXML = strOldInstallationXML + strNewInstallationXML;
                        iResult = ConvertInstallation(strOldInstallationXML, strNewInstallationXML);
                        break;
                    case InstallationType.ConvertInstallation:
                        LogManager.WriteLog("Convert Installation Entered", LogManager.enumLogLevel.Info);
                        strNewInstallationXML = "<NEWINSTALLATION>" +
                                                oSiteNodes.Item(0).ChildNodes[3].ChildNodes[0].InnerXml +
                                                "</NEWINSTALLATION>";
                        strOldInstallationXML = "<OLDINSTALLATION>" +
                                                oSiteNodes.Item(0).ChildNodes[3].ChildNodes[1].InnerXml +
                                                "</OLDINSTALLATION>";
                        strFinalXML = strOldInstallationXML + strNewInstallationXML;
                        iResult = ConvertInstallation(strOldInstallationXML, strNewInstallationXML);
                        break;
                    case InstallationType.PlannedConversion:
                        LogManager.WriteLog("Planned Conversion Entered", LogManager.enumLogLevel.Info);
                        strNewInstallationXML = oSiteNodes.Item(0).ChildNodes[3].InnerXml;
                        strFinalXML = strNewInstallationXML;
                        iResult = PlannedConversion(strNewInstallationXML);
                        break;
                    case InstallationType.RemoveInstallation:
                        LogManager.WriteLog("Remove Installation Entered", LogManager.enumLogLevel.Info);
                        strOldInstallationXML = oSiteNodes.Item(0).ChildNodes[3].InnerXml;
                        strFinalXML = strOldInstallationXML;
                        iResult = CloseInstallation(strOldInstallationXML);
                        break;
                    default:
                        break;
                }
                LogManager.WriteLog("iResult " + iResult, LogManager.enumLogLevel.Info);
                if (iResult > 0 && int.Parse(strEHID) != 0)
                {
                    var oEventParam = new SqlParameter[4];

                    var sqlParameter = new SqlParameter
                                           {
                                               ParameterName = "Site_Code",
                                               Value = strSiteCode,
                                               Direction = ParameterDirection.Input
                                           };
                    oEventParam[0] = sqlParameter;

                    sqlParameter = new SqlParameter
                                       {
                                           ParameterName = "Type",
                                           Value = strXMLType,
                                           Direction = ParameterDirection.Input
                                       };
                    oEventParam[1] = sqlParameter;

                    sqlParameter = new SqlParameter
                                       {
                                           ParameterName = "SiteXML",
                                           Value = strFinalXML,
                                           SqlDbType = SqlDbType.Text,
                                           Direction = ParameterDirection.Input
                                       };
                    oEventParam[2] = sqlParameter;

                    sqlParameter = new SqlParameter
                                       {
                                           ParameterName = "EH_ID",
                                           Value = strEHID,
                                           SqlDbType = SqlDbType.Int,
                                           Direction = ParameterDirection.Input
                                       };
                    oEventParam[3] = sqlParameter;

                    SqlHelper.ExecuteNonQuery(_sqlTran, CommandType.StoredProcedure, "esp_Import_SiteXML", oEventParam);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                iResult = -100;
            }
            finally
            {
                if (iResult > 0)
                    _sqlTran.Commit();
                else
                    _sqlTran.Rollback();
                SqlConnect.Close();
                _sqlTran = null;
            }


            return iResult;
        }

        /// <summary>
        /// Function is called when Installation is has to be Removed/Closed from the Exchange. On successfull removal 
        /// Flag 0 is returned Else
        /// (-1) - invalid site code
        /// (-2) - invalid installation id
        /// (-3) - installation is already closed
        /// (-4) - general sql error
        /// </summary>
        /// <Author></Author>
        /// <param name="InstallationXML">XML string containing Details of the installation</param>
        /// <returns>Returns the HQInstallationID once the data is Saved successfully.  If otherwise, following is returned
        /// (-1) - invalid bar position
        /// (-2) - invalid machine type code
        /// (-3) - invalid game title supplied
        /// (-4) - invalid asset number
        /// (-5) - General SQL Error
        /// </returns>  
        [WebMethod, SoapHeader("AuthenticationInfo")]
        [SoapSecurityExtension(Encrypt = EncryptMode.Response)]
        public Int32 CloseInstallation(string InstallationXML)
        {
            CheckSecurity();
            LogManager.WriteLog("CloseInstallation Called...", LogManager.enumLogLevel.Info);
            LogManager.WriteLog(InstallationXML, LogManager.enumLogLevel.Info);
            try
            {
                RemoveEscapeCharsFromXml(ref InstallationXML);

                var objSQLParamsExportBarPositions = new SqlParameter[1];
                var objSQLParam = new SqlParameter();
                objSQLParam.ParameterName = "@XML";
                objSQLParam.Value = InstallationXML;
                objSQLParam.DbType = DbType.String;
                objSQLParam.Direction = ParameterDirection.Input;
                objSQLParamsExportBarPositions[0] = objSQLParam;

                //Call the Stored Procedure
                LogManager.WriteLog("EnhancedEnrollment: Close Installation", LogManager.enumLogLevel.Info);
                int iResult;
                if (_sqlTran == null)
                {
                    iResult =
                        Int32.Parse(
                            SqlHelper.ExecuteScalar(GetConnectionString(), "Usp_RemoveInstallation",
                                                    objSQLParamsExportBarPositions).ToString());
                }
                else
                {
                    iResult =
                        Int32.Parse(
                            SqlHelper.ExecuteScalar(_sqlTran, "Usp_RemoveInstallation", objSQLParamsExportBarPositions).
                                ToString());
                }
                LogManager.WriteLog("Return Value For Remove Installation: " + iResult, LogManager.enumLogLevel.Info);
                return iResult;
            }
            catch (SqlException sqlEx)
            {
                ExceptionManager.Publish(sqlEx);
                return -99;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return -90;
            }
        }

        /// <summary>
        /// Function is called when an old Installation is has to be Removed/Closed and A new installation has to be replaced. On successfull operation
        /// HQ installation ID will be sent to the Callee
        /// </summary>
        /// <param name="OldInstallationXML">String containing Details of Exisiting Installation</param>
        /// <param name="NewInstallationXML">String containing Details of New Installation</param>
        /// <returns>
        /// Returns the HQInstallationID once the data is Saved successfully.  If otherwise, following is returned
        /// (-1)  - if Installation Does not Exists
        /// (-2)  - if Site Does not Exists
        /// (-3)  - if Installation already Closed
        /// (-4)  - if Machine Details Could Not be Updated
        /// (-5)  - if Installation Details could not be updated
        /// (-6)  - invalid bar position
        /// (-7)  - invalid machine type code
        /// (-8)  - invalid game title supplied
        /// (-9)  - invalid asset number
        /// (-10) - General SQL Error
        /// </returns>
        [WebMethod, SoapHeader("AuthenticationInfo")]
        [SoapSecurityExtension(Encrypt = EncryptMode.Response)]
        public Int32 ConvertInstallation(string OldInstallationXML, string NewInstallationXML)
        {
            CheckSecurity();
            LogManager.WriteLog("OldInstallationXML Called...", LogManager.enumLogLevel.Info);
            try
            {
                RemoveEscapeCharsFromXml(ref OldInstallationXML);
                RemoveEscapeCharsFromXml(ref NewInstallationXML);

                var objSQLParamstrXML = new SqlParameter[2];
                var objOldInstallationParam = new SqlParameter();
                var objNewInstallationParam = new SqlParameter();

                objOldInstallationParam.ParameterName = "@OLDXML";
                objOldInstallationParam.Value = OldInstallationXML;
                objOldInstallationParam.DbType = DbType.String;
                objOldInstallationParam.Direction = ParameterDirection.Input;
                objSQLParamstrXML[0] = objOldInstallationParam;

                objNewInstallationParam = new SqlParameter();
                objNewInstallationParam.ParameterName = "@NEWXML";
                objNewInstallationParam.Value = NewInstallationXML;
                objNewInstallationParam.DbType = DbType.String;
                objNewInstallationParam.Direction = ParameterDirection.Input;
                objSQLParamstrXML[1] = objNewInstallationParam;

                //Call the Stored Procedure
                LogManager.WriteLog("EnhancedEnrollment: Convert Installation", LogManager.enumLogLevel.Info);
                LogManager.WriteLog(OldInstallationXML, LogManager.enumLogLevel.Info);
                LogManager.WriteLog(NewInstallationXML, LogManager.enumLogLevel.Info);
                int iResult;
                if (_sqlTran == null)
                {
                    iResult =
                        Int32.Parse(
                            SqlHelper.ExecuteScalar(GetConnectionString(), "usp_RemoveOldAndAddNewInstallation",
                                                    objSQLParamstrXML).ToString());
                }
                else
                {
                    iResult =
                        Int32.Parse(
                            SqlHelper.ExecuteScalar(_sqlTran, "usp_RemoveOldAndAddNewInstallation", objSQLParamstrXML).
                                ToString());
                }
                LogManager.WriteLog("Return Value For GMUCHANGE/CONVERT Installation: " + iResult,
                                    LogManager.enumLogLevel.Info);
                return iResult;
            }
            catch (SqlException SQLEx)
            {
                ExceptionManager.Publish(SQLEx);
                return -99;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return -90;
            }
        }


        [WebMethod, SoapHeader("AuthenticationInfo")]
        [SoapSecurityExtension(Encrypt = EncryptMode.Response)]
        public Int32 SwapInstallation(string InstallationXML)
        {
            CheckSecurity();
            LogManager.WriteLog("SwapInstallation Called...", LogManager.enumLogLevel.Info);
            LogManager.WriteLog(InstallationXML, LogManager.enumLogLevel.Info);

            try
            {
                RemoveEscapeCharsFromXml(ref InstallationXML);

                var objSQLParamsSwapInstallation = new SqlParameter[2];
                var objSQLParam = new SqlParameter();
                objSQLParam.ParameterName = "@XML";
                objSQLParam.Value = InstallationXML;
                objSQLParam.DbType = DbType.String;
                objSQLParam.Direction = ParameterDirection.Input;
                objSQLParamsSwapInstallation[0] = objSQLParam;

                objSQLParam = new SqlParameter();
                objSQLParam.ParameterName = "@HQInstallationNo";
                objSQLParam.SqlDbType = SqlDbType.Int;
                objSQLParam.Direction = ParameterDirection.Output;
                objSQLParamsSwapInstallation[1] = objSQLParam;

                SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "usp_SwapInstallation",
                                          objSQLParamsSwapInstallation);
                return (int.Parse(objSQLParamsSwapInstallation[1].Value.ToString()));
            }
            catch (SqlException sqlEx)
            {
                ExceptionManager.Publish(sqlEx);
                return -99;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return -90;
            }
        }

        [WebMethod, SoapHeader("AuthenticationInfo")]
        [SoapSecurityExtension(Encrypt = EncryptMode.Response)]
        public bool ImportInstallationUpdate(string installationXML)
        {
            CheckSecurity();

            try
            {
                var sqlParameters = new SqlParameter[1];
                var sqlParameter = new SqlParameter
                                       {
                                           ParameterName = "XML",
                                           Value = installationXML,
                                           Direction = ParameterDirection.Input
                                       };
                sqlParameters[0] = sqlParameter;

                if (
                    SqlHelper.ExecuteScalar(GetConnectionString(), CommandType.StoredProcedure,
                                            "usp_UpdateInstallationDetails", sqlParameters).ToString() == "UPDATED")
                    return true;
                return false;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return false;
            }
        }

        [WebMethod, SoapHeader("AuthenticationInfo")]
        [SoapSecurityExtension(Encrypt = EncryptMode.Response)]
        public string GetInstallationNumber(string xmlString)
        {
            CheckSecurity();

            LogManager.WriteLog("GetInstallationNumber Called...", LogManager.enumLogLevel.Info);

            try
            {
                var oEventParam = new SqlParameter[1];

                var sqlParameter = new SqlParameter
                                       {
                                           ParameterName = "@doc",
                                           Value = xmlString,
                                           Direction = ParameterDirection.Input
                                       };
                oEventParam[0] = sqlParameter;

                return
                    SqlHelper.ExecuteScalar(ConfigManager.Read("SQLConnect"), CommandType.StoredProcedure, "rsp_GetEnterpriseInstallationNoUsingXML",
                                            oEventParam).ToString();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return "";
        }

        [WebMethod, SoapHeader("AuthenticationInfo")]
        [SoapSecurityExtension(Encrypt = EncryptMode.Response)]
        public bool ImportMeterHistory(string xmlString)
        {
            CheckSecurity();

            bool bSuccess = false;

            LogManager.WriteLog("ImportMeterHistory Called...", LogManager.enumLogLevel.Info);

            var objXMLDoc = new XmlDocument();

            objXMLDoc.LoadXml(xmlString);
            XmlNodeList xmlNodes = objXMLDoc.ChildNodes[0].ChildNodes;

            try
            {
                foreach (XmlNode objXmlNode in xmlNodes)
                {
                    var sqlParameters = new SqlParameter[2];
                    var sqlParameter = new SqlParameter
                                           {
                                               ParameterName = "@doc",
                                               Value = objXmlNode.OuterXml,
                                               Direction = ParameterDirection.Input
                                           };
                    sqlParameters[0] = sqlParameter;

                    sqlParameter = new SqlParameter
                                       {
                                           ParameterName = "IsSuccess",
                                           SqlDbType = SqlDbType.Int,
                                           Direction = ParameterDirection.Output
                                       };
                    sqlParameters[1] = sqlParameter;

                    SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "usp_InsertMeterHistoryfromXML",
                                              sqlParameters);
                    if (int.Parse(sqlParameters[1].Value.ToString()) == 0)
                    {
                        bSuccess = false;
                        LogManager.WriteLog(
                            "ImportData WS - MeterHistory" + " SQL Error Occured " + sqlParameters[1].Value,
                            LogManager.enumLogLevel.Info);
                    }
                    else if (int.Parse(sqlParameters[1].Value.ToString()) < 0)
                    {
                        bSuccess = false;
                        LogManager.WriteLog(
                            "ImportData WS - MeterHistory" +
                            " 1.Record already processed,2.Link reference not found::: " + sqlParameters[1].Value,
                            LogManager.enumLogLevel.Info);
                    }
                    else if (int.Parse(sqlParameters[1].Value.ToString()) > 0)
                    {
                        bSuccess = true;
                        LogManager.WriteLog(
                            "ImportData WS - MeterHistory" + "  Insert Success  " + sqlParameters[1].Value,
                            LogManager.enumLogLevel.Info);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                bSuccess = false;
            }

            return bSuccess;
        }

        [WebMethod, SoapHeader("AuthenticationInfo")]
        [SoapSecurityExtension(Encrypt = EncryptMode.Response)]
        public bool UpdateBarPositionCentralStatusBySiteID(int exportHistoryID)
        {
            CheckSecurity();



            try
            {
                var objParam = new SqlParameter[1];
                objParam[0] = new SqlParameter
                                      {
                                          ParameterName = "@EH_ID",
                                          Value = exportHistoryID,
                                          Direction = ParameterDirection.Input
                                      };

                SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure,
                                          "usp_UpdateMachineEnableDisableStatus", objParam);
                return true;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog(ex.ToString(), LogManager.enumLogLevel.Error);
            }
            return false;
        }

        [WebMethod, SoapHeader("AuthenticationInfo")]
        [SoapSecurityExtension(Encrypt = EncryptMode.Response)]
        public bool UpdateSiteStatsInEnterprise(string strSiteName, string strStatusXML)
        {
            CheckSecurity();

            try
            {
                var sqlParameters = new SqlParameter[2];
                sqlParameters[0] = new SqlParameter("@SiteName", strSiteName);
                sqlParameters[1] = new SqlParameter("@SiteStatus", strStatusXML);
                SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "usp_UpdateSiteStats", sqlParameters);
                return true;
            }
            catch
            {
                LogManager.WriteLog("Error in getting stats from site ", LogManager.enumLogLevel.Error);
                return false;
            }
        }

        [WebMethod, SoapHeader("AuthenticationInfo")]
        [SoapSecurityExtension(Encrypt = EncryptMode.Response)]
        public string GetPlannedConversion(string assetNumber)
        {
            CheckSecurity();
            var strReturnXML = string.Empty;
            var oExportDetails = new SqlParameter[2];
            var oParamModel = new SqlParameter();
            var oParamAsset = new SqlParameter();
            var oSiteXMLDoc = new XmlDocument();
            var subDoc = new XmlDocument();

            oParamModel.ParameterName = "ID";
            oParamModel.Value = "";
            oParamModel.Direction = ParameterDirection.Input;
            oExportDetails[0] = oParamModel;

            oParamAsset.ParameterName = "AssetNumber";
            oParamAsset.Value = assetNumber;
            oParamAsset.Direction = ParameterDirection.Input;
            oExportDetails[1] = oParamAsset;

            var strXML = (SqlHelper.ExecuteScalar(GetConnectionString(), CommandType.StoredProcedure, Constants.CONSTANT_RSP_EXPORTMODEL,
                                                     oExportDetails)).ToString();

            if (strXML == null)
                return "";
            if (strXML.Trim() == "")
                return "";
            strXML = "<root>" + strXML + "</root>";
            oSiteXMLDoc.LoadXml(strXML);
            XmlNodeList oSiteNodes = oSiteXMLDoc.DocumentElement.GetElementsByTagName("MODEL");

            subDoc.LoadXml(strXML);
            XmlNodeList subList = subDoc.DocumentElement.GetElementsByTagName("tMT");

            foreach (XmlNode node in oSiteNodes.Item(0).ChildNodes[0].ChildNodes)
                if (node.Name == "Machine_Name")
                {
                    strReturnXML = node.InnerText + "~";
                    break;
                }

            foreach (XmlNode element in subList.Item(0).ChildNodes)
                if (element.Name == "Machine_Type_Code")
                {
                    strReturnXML = element.InnerText + "~" + strReturnXML;
                    break;
                }

            return strReturnXML;
        }

        /// <summary>
        /// Function is called when an old Installation is has to be Removed/Closed and A new installation has to be replaced. On successfull operation
        /// HQ installation ID will be sent to the Callee
        /// </summary>
        /// <param name="installationXML">String containing Details of New Installation</param>
        /// <returns>
        /// Returns the HQInstallationID once the data is Saved successfully.  If otherwise, following is returned
        /// (-1)  - if Installation Does not Exists
        /// (-2)  - if Site Does not Exists
        /// (-3)  - if Installation already Closed
        /// (-4)  - if Machine Details Could Not be Updated
        /// (-5)  - if Installation Details could not be updated
        /// (-6)  - invalid bar position
        /// (-7)  - invalid machine type code
        /// (-8)  - invalid game title supplied
        /// (-9)  - invalid asset number
        /// (-10) - General SQL Error
        /// </returns>
        [WebMethod, SoapHeader("AuthenticationInfo")]
        [SoapSecurityExtension(Encrypt = EncryptMode.Response)]
        public Int32 PlannedConversion(string installationXML)
        {
            CheckSecurity();
            LogManager.WriteLog("PlannedConversion Called...", LogManager.enumLogLevel.Info);
            LogManager.WriteLog(installationXML, LogManager.enumLogLevel.Info);

            try
            {
                RemoveEscapeCharsFromXml(ref installationXML);
                int iResult;
                var sqlParamsExportBarPositions = new SqlParameter[1];
                var sqlParamsExportBarPosition = new SqlParameter
                                                     {
                                                         ParameterName = "@XML",
                                                         Value = installationXML,
                                                         DbType = DbType.String,
                                                         Direction = ParameterDirection.Input
                                                     };
                sqlParamsExportBarPositions[0] = sqlParamsExportBarPosition;
                LogManager.WriteLog("EnhancedEnrollment: Create Installation", LogManager.enumLogLevel.Info);

                if (_sqlTran == null)
                {
                    iResult =
                        Int32.Parse(
                            SqlHelper.ExecuteScalar(GetConnectionString(), "USP_UpdatePlannedConversionByAsset",
                                                    sqlParamsExportBarPositions).ToString());
                }
                else
                {
                    iResult =
                        Int32.Parse(
                            SqlHelper.ExecuteScalar(_sqlTran, "USP_UpdatePlannedConversionByAsset",
                                                    sqlParamsExportBarPositions).ToString());
                }
                LogManager.WriteLog("Return Value For Planned Conversion Installation: " + iResult,
                                    LogManager.enumLogLevel.Info);
                return iResult;
            }
            catch (SqlException sqlEx)
            {
                ExceptionManager.Publish(sqlEx);
                return -99;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return -90;
            }
        }

        [WebMethod, SoapHeader("AuthenticationInfo")]
        [SoapSecurityExtension(Encrypt = EncryptMode.Response)]
        public string GetCurrentServiceCalls(string strSiteCode, string strStartBarPosNo, string strLastBarPosNo)
        {
            CheckSecurity();

            string strOutputXML = string.Empty;
            bool bDbCallSuccess = true;


            try
            {
                LogManager.WriteLog("GetCurrentServiceCalls Called...", LogManager.enumLogLevel.Info);

                var oServiceParam = new SqlParameter[4];
                var sqlParameter = new SqlParameter
                                                {
                                                    ParameterName = "SITECODE",
                                                    Value = strSiteCode,
                                                    SqlDbType = SqlDbType.VarChar,
                                                    Direction = ParameterDirection.Input
                                                };
                oServiceParam[0] = sqlParameter;

                sqlParameter = new SqlParameter
                                   {
                                       ParameterName = "STARTBARPOSNO",
                                       Value = strStartBarPosNo,
                                       SqlDbType = SqlDbType.VarChar,
                                       Direction = ParameterDirection.Input
                                   };
                oServiceParam[1] = sqlParameter;

                sqlParameter = new SqlParameter
                                   {
                                       ParameterName = "LASTBARPOSNO",
                                       Value = strLastBarPosNo,
                                       SqlDbType = SqlDbType.VarChar,
                                       Direction = ParameterDirection.Input
                                   };
                oServiceParam[2] = sqlParameter;

                sqlParameter = new SqlParameter("@RETURN_VALUE", SqlDbType.Int) { Direction = ParameterDirection.ReturnValue };
                oServiceParam[3] = sqlParameter;

                var ds = new DataSet();

                SqlHelper.FillDataset(GetConnectionString(), CommandType.StoredProcedure, "rsp_GetCurrentServiceCalls", ds, null,
                                      oServiceParam);

                foreach (DataTable dt in ds.Tables)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        strOutputXML = strOutputXML + dr[0];
                    }
                }

                if (int.Parse(oServiceParam[2].Value.ToString()) == 0)
                    bDbCallSuccess = true;

                if (bDbCallSuccess)
                {
                    LogManager.WriteLog("Successful call " + "GetCurrentServiceCalls", LogManager.enumLogLevel.Info);
                }
                else
                {
                    LogManager.WriteLog("Failure call " + "GetCurrentServiceCalls", LogManager.enumLogLevel.Info);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            return strOutputXML;
        }

        [WebMethod, SoapHeader("AuthenticationInfo")]
        [SoapSecurityExtension(Encrypt = EncryptMode.Response)]
        public DataTable GetOpenServiceCalls(string siteCode, string barPos)
        {
            CheckSecurity();

            try
            {
                var sqlParameters = new SqlParameter[2];

                var sqlParameter = new SqlParameter
                                 {
                                     ParameterName = "Site_Code",
                                     Direction = ParameterDirection.Input,
                                     Value = siteCode
                                 };
                sqlParameters[0] = sqlParameter;

                var param2 = new SqlParameter
                                 {
                                     ParameterName = "Bar_Pos",
                                     Direction = ParameterDirection.Input,
                                     Value = barPos
                                 };
                sqlParameters[1] = param2;

                var openServiceCalls = SqlHelper.ExecuteDataset(GetConnectionString(), CommandType.StoredProcedure, "rsp_FetchOpenServiceCalls", sqlParameters).
                    Tables[0];

                return openServiceCalls;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return new DataTable();
            }
        }

        [WebMethod, SoapHeader("AuthenticationInfo")]
        [SoapSecurityExtension(Encrypt = EncryptMode.Response)]
        public DataTable GetServiceNotes(string jobID)
        {
            CheckSecurity();


            try
            {
                var oparams = new SqlParameter[1];

                var param = new SqlParameter
                                {
                                    ParameterName = "JobID",
                                    Direction = ParameterDirection.Input,
                                    Value = jobID
                                };
                oparams[0] = param;

                return
                    SqlHelper.ExecuteDataset(GetConnectionString(), CommandType.StoredProcedure, "rsp_GetServiceNotes",
                                             oparams).Tables[0];
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return new DataTable();
            }
        }

        [WebMethod, SoapHeader("AuthenticationInfo")]
        [SoapSecurityExtension(Encrypt = EncryptMode.Response)]
        public int CloseServiceCall(int serviceID, string jobID, int remedy, int userId, string notes)
        {
            CheckSecurity();

            var oparams = new SqlParameter[6];

            var param1 = new SqlParameter
                             {
                                 ParameterName = "Service_ID",
                                 Direction = ParameterDirection.Input,
                                 Value = serviceID
                             };
            oparams[0] = param1;

            var param2 = new SqlParameter
                             {
                                 ParameterName = "Service_Allocated_Job_No",
                                 Direction = ParameterDirection.Input,
                                 Value = jobID
                             };
            oparams[1] = param2;

            var param3 = new SqlParameter
                             {
                                 ParameterName = "RemedyID",
                                 Direction = ParameterDirection.Input,
                                 Value = remedy
                             };
            oparams[2] = param3;

            var param4 = new SqlParameter
                             {
                                 ParameterName = "UserID",
                                 Direction = ParameterDirection.Input,
                                 Value = userId
                             };
            oparams[3] = param4;

            var param5 = new SqlParameter
                             {
                                 ParameterName = "Notes",
                                 Direction = ParameterDirection.Input,
                                 Value = notes
                             };
            oparams[4] = param5;

            var retParam = new SqlParameter
                               {
                                   ParameterName = "@RETURN_VALUE",
                                   SqlDbType = SqlDbType.Int,
                                   Direction = ParameterDirection.ReturnValue
                               };
            oparams[5] = retParam;

            try
            {
                SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "usp_CloseServiceCalls",
                                          oparams);

                if (retParam.Value.ToString() != "10")
                    return 99;
                return 10;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return -1;
            }
        }

        [WebMethod, SoapHeader("AuthenticationInfo")]
        [SoapSecurityExtension(Encrypt = EncryptMode.Response)]
        public int InsertServiceNotes(string jobID, string notes, string userName)
        {
            CheckSecurity();


            var oparams = new SqlParameter[4];

            var param1 = new SqlParameter
                             {
                                 ParameterName = "Service_ID",
                                 Direction = ParameterDirection.Input,
                                 Value = jobID
                             };
            oparams[0] = param1;

            var param2 = new SqlParameter
                             {
                                 ParameterName = "Notes",
                                 Direction = ParameterDirection.Input,
                                 Value = notes
                             };
            oparams[1] = param2;

            var param3 = new SqlParameter
                             {
                                 ParameterName = "User",
                                 Direction = ParameterDirection.Input,
                                 Value = userName
                             };
            oparams[2] = param3;

            var retParam = new SqlParameter
                               {
                                   ParameterName = "@RETURN_VALUE",
                                   SqlDbType = SqlDbType.Int,
                                   Direction = ParameterDirection.ReturnValue
                               };
            oparams[3] = retParam;

            try
            {
                SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "usp_InsertServiceNotes",
                                          oparams);

                if (retParam.Value.ToString() != "0")
                    return 99;
                return int.Parse(retParam.Value.ToString());
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return -1;
            }
        }

        [WebMethod, SoapHeader("AuthenticationInfo")]
        [SoapSecurityExtension(Encrypt = EncryptMode.Response)]
        public int EscalateService(string jobID, int UserID)
        {
            CheckSecurity();


            var oparams = new SqlParameter[3];

            var param1 = new SqlParameter
                             {
                                 ParameterName = "Service_ID",
                                 Direction = ParameterDirection.Input,
                                 Value = jobID
                             };
            oparams[0] = param1;

            var param2 = new SqlParameter
                             {
                                 ParameterName = "UserID",
                                 Direction = ParameterDirection.Input,
                                 Value = UserID
                             };
            oparams[1] = param2;

            var retParam = new SqlParameter
                               {
                                   ParameterName = "@RETURN_VALUE",
                                   SqlDbType = SqlDbType.Int,
                                   Direction = ParameterDirection.ReturnValue
                               };
            oparams[2] = retParam;

            try
            {
                SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "usp_EscalateServiceCall",
                                          oparams);

                return int.Parse(retParam.Value.ToString());
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return -1;
            }
        }

        [WebMethod, SoapHeader("AuthenticationInfo")]
        [SoapSecurityExtension(Encrypt = EncryptMode.Response)]
        public DataTable GetBarPositionStatus(string siteCode)
        {
            CheckSecurity();

            var ds = new DataSet();
            var dtBarPosition = new DataTable("BarPositions");
            try
            {
                LogManager.WriteLog("Get Bar Positions Machine Status function called...", LogManager.enumLogLevel.Info);
                var sqlParameters = new SqlParameter[1];
                sqlParameters[0] = new SqlParameter("@SiteCode", siteCode);

                SqlHelper.FillDataset(GetConnectionString(), CommandType.StoredProcedure,
                                      Constants.CONSTANT_RSP_GETBARPOSITIONMACHINESTATUS, ds, null, sqlParameters);

                dtBarPosition = ds.Tables[0];

                LogManager.WriteLog("Successful call " + "Get Bar Positions Machine Status",
                                    LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Failure call " + "Get Bar Positions Machine Status", LogManager.enumLogLevel.Info);
                ExceptionManager.Publish(ex);
            }
            return dtBarPosition;
        }

        [WebMethod, SoapHeader("AuthenticationInfo")]
        [SoapSecurityExtension(Encrypt = EncryptMode.Response)]
        public DataTable GetWeeklyCollectionDetails(string strSiteCode, int iWeekID, int iNoOfRecords)
        {
            CheckSecurity();

            var dtCollection = new DataTable();

            try
            {
                var sqlParameters = new SqlParameter[2];

                LogManager.WriteLog("GetWeeklyCollectionDetails Called...", LogManager.enumLogLevel.Info);
                sqlParameters[0] = new SqlParameter("@Site_Code", strSiteCode);

                if (iWeekID == 0)
                {
                    if (iNoOfRecords == 0) iNoOfRecords = 1;
                    sqlParameters[1] = new SqlParameter("@NoOfRecords", iNoOfRecords);
                    dtCollection =
                        SqlHelper.ExecuteDataset(GetConnectionString(), "rsp_GetWeeklyCollectionSummary", sqlParameters).Tables[0];
                }
                else
                {
                    sqlParameters[1] = new SqlParameter("@Week_ID", iWeekID);
                    dtCollection =
                        SqlHelper.ExecuteDataset(GetConnectionString(), "rsp_getWeeklyCollectionDetails", sqlParameters).Tables[0];
                }
                return dtCollection;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return dtCollection;
            }
        }

        [WebMethod, SoapHeader("AuthenticationInfo")]
        [SoapSecurityExtension(Encrypt = EncryptMode.Response)]
        public string GetHqInstallationID()
        {
            CheckSecurity();
            try
            {
                return (SqlHelper.ExecuteScalar(GetConnectionString(), CommandType.StoredProcedure, "rsp_GetHQInstallationID")).ToString();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return "";
            }
        }


        [WebMethod, SoapHeader("AuthenticationInfo")]
        [SoapSecurityExtension(Encrypt = EncryptMode.Response)]
        public DataTable GetCashDeskServiceFaults()
        {
            CheckSecurity();

            var ds = new DataSet();

            try
            {
                LogManager.WriteLog("GetCashDeskServiceFaults Called...", LogManager.enumLogLevel.Info);
                const string query = "SELECT Datapak_Fault_Code, Datapak_Fault_Supplementary_Code, Datapak_Fault_Text FROM Datapak_Fault WHERE Datapak_Fault_Code = 300";

                SqlHelper.FillDataset(GetConnectionString(), CommandType.Text, query, ds, null);
                return ds.Tables[0];

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Failure call " + "GetCashDeskServiceFaults", LogManager.enumLogLevel.Info);
                ExceptionManager.Publish(ex);
                return new DataTable();
            }

        }

        [WebMethod, SoapHeader("AuthenticationInfo")]
        [SoapSecurityExtension(Encrypt = EncryptMode.Response)]
        public DataTable GetRemedies()
        {
            CheckSecurity();

            try
            {
                return SqlHelper.ExecuteDataset(GetConnectionString(), CommandType.Text, "SELECT * FROM dbo.Call_Remedy WHERE Call_Remedy_End_Date IS NULL").Tables[0];
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return new DataTable();
            }
        }

        [WebMethod, SoapHeader("AuthenticationInfo")]
        [SoapSecurityExtension(Encrypt = EncryptMode.Response)]
        public string UpdateInstallationGame(string strInstallationXML)
        {
            CheckSecurity();

            string strReturn;

            var sqlParameters = new SqlParameter[2];

            var objParam = new SqlParameter
                               {
                                   ParameterName = "doc",
                                   SqlDbType = SqlDbType.Xml,
                                   Direction = ParameterDirection.Input,
                                   Value = strInstallationXML
                               };
            sqlParameters[0] = objParam;

            objParam = new SqlParameter
                           {
                               ParameterName = "return",
                               SqlDbType = SqlDbType.Xml,
                               Direction = ParameterDirection.Output
                           };
            sqlParameters[1] = objParam;

            try
            {
                SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, Constants.CONSTANT_USP_INSERTINSTALLATIONGAME, sqlParameters);
                strReturn = sqlParameters[1].Value.ToString();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                strReturn = "Error";
            }

            return strReturn;
        }

        [WebMethod, SoapHeader("AuthenticationInfo")]
        [SoapSecurityExtension(Encrypt = EncryptMode.Response)]
        public DataTable GetAssetDetails(string assetNo, string transitSiteCode)
        {
            CheckSecurity();

            var ds = new DataSet();
            try
            {
                var oEventParam = new SqlParameter[2];

                var sqlParameterAsset = new SqlParameter
                {
                    ParameterName = "@AssetNo",
                    Value = assetNo,
                    Direction = ParameterDirection.Input
                };
                oEventParam[0] = sqlParameterAsset;

                var sqlParameterTransitSite = new SqlParameter
                {
                    ParameterName = "@TransitSiteCode",
                    Value = transitSiteCode,
                    Direction = ParameterDirection.Input
                };
                oEventParam[1] = sqlParameterTransitSite;

                SqlHelper.FillDataset(GetConnectionString(), CommandType.StoredProcedure, "rsp_GetAssetDetails", ds,
                                      null, oEventParam);

                ds.Tables[0].TableName = "AssetDetails";
                ds.DataSetName = "AssetDetails";

                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Failure call " + " GetAssetDetails ", LogManager.enumLogLevel.Info);
                ExceptionManager.Publish(ex);
                return null;
            }
        }

        [SoapSecurityExtension(Encrypt = EncryptMode.Response)]
        public bool ValidateUser(string userName, string password)
        {
            try
            {
                LogManager.WriteLog("Inside ValidateUser method", LogManager.enumLogLevel.Info);

                object objResult = null;
                string query = string.Empty;

                query = string.Format("SELECT * FROM [User] WHERE UserName = '{0}' AND Password = '{1}'", userName, password);

                objResult = SqlHelper.ExecuteScalar(GetConnectionString(), CommandType.Text, query);
                return objResult != null ? true : false;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return false;
            }
        }

        #region Import InstallationGameInfo
        ///<summary>
        /// Insert the installation game info into corresponding tables and returns true on success.
        /// </summary>
        /// <param name="installationGameXML">String containing Details of Installation Game Info</param>
        /// <returns>
        /// Returns true once the data is Saved successfully. 
        /// </returns>


        [WebMethod, SoapHeader("AuthenticationInfo")]
        [SoapSecurityExtension(Encrypt = EncryptMode.Response)]
        public bool InsertInstallationGameInfo(string xmlString)
        {
            //check security
            CheckSecurity();

            LogManager.WriteLog("InsertInstallationGameInfo Called...", LogManager.enumLogLevel.Info);

            try
            {
                //var xmlDoc = new XmlDocument();

                //xmlDoc.LoadXml(xmlString);
                //XmlNodeList xmlNodes = xmlDoc.DocumentElement.ChildNodes;
                LogManager.WriteLog(xmlString, LogManager.enumLogLevel.Info);

                var outParam = DataBaseServiceHandler.AddParameter<int>("IsSuccess", DbType.Int32, 0, ParameterDirection.Output);

                DataBaseServiceHandler.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "rsp_ImportInstallationGameInfo",
                    DataBaseServiceHandler.AddParameter<string>("doc", DbType.String, xmlString), outParam);

                LogManager.WriteLog("Game Info details stored", LogManager.enumLogLevel.Info);

                if (Convert.ToInt32(outParam.Value) == 0)
                    return true;

                //foreach (XmlNode objXmlNode in xmlNodes)
                //{
                //    var sqlParameters = new SqlParameter[2];
                //    var sqlParameter = new SqlParameter
                //    {
                //        ParameterName = "@doc",
                //        Value = objXmlNode.OuterXml,
                //        Direction = ParameterDirection.Input
                //    };
                //    sqlParameters[0] = sqlParameter;

                //    sqlParameter = new SqlParameter
                //    {
                //        ParameterName = "IsSuccess",
                //        SqlDbType = SqlDbType.Int,
                //        Direction = ParameterDirection.Output
                //    };
                //    sqlParameters[1] = sqlParameter;

                //    //Insert the installation game info details.
                //    SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "rsp_ImportInstallationGameInfo",
                //                              sqlParameters);

                //    LogManager.WriteLog("Game Info details stored", LogManager.enumLogLevel.Info);
                //    if (sqlParameters[1].Value.ToString() == "0")
                //        return true;
                //}
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return false;
        }
        #endregion

        #region Import Site Settings From Enterprise WebMethod

        /// <summary>
        /// This webmethod is for importing the site settings to exchange
        /// </summary>
        /// <Author></Author>
        /// <DateCreated></DateCreated>
        /// <Parameters>XML string</Parameters>
        /// <returns>boolean</returns>
        ///
        /// Method Revision History
        ///
        /// Author             Date              Description
        /// ---------------------------------------------------
        /// Renjish           16-02-09          Created
        /// 

        [WebMethod, SoapHeader("AuthenticationInfo")]
        [SoapSecurityExtension(Encrypt = EncryptMode.Response)]
        public bool ImportSiteSettings(string strXML)
        {

            CheckSecurity();


            string strSPName = string.Empty;
            bool bSuccess = false;

            try
            {
                SqlParameter[] objSiteSQLParams = new SqlParameter[2];

                strSPName = "usp_ImportSiteSettingsFromXML";

                SqlParameter objSQLParam = new SqlParameter();
                objSQLParam.ParameterName = "XMLData";
                objSQLParam.Value = strXML;
                objSQLParam.Direction = ParameterDirection.Input;
                objSiteSQLParams[0] = objSQLParam;

                objSQLParam = new SqlParameter();
                objSQLParam.Direction = ParameterDirection.ReturnValue;
                objSiteSQLParams[1] = objSQLParam;

                SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, strSPName, objSiteSQLParams);

                if (int.Parse(objSiteSQLParams[1].Value.ToString()) == 0)
                    bSuccess = true;
                else
                {
                    bSuccess = false;
                    LogManager.WriteLog("ImportSite WS " + "  failed due to " + objSiteSQLParams[1].Value.ToString(), LogManager.enumLogLevel.Info);
                }
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                bSuccess = false;
            }
            return bSuccess;
        }

        #endregion Import Site Settings From Enterprise WebMethod

        #region Ticket Details Import To Enterprise

        /// <summary>
        /// Web method for importing voucher details.
        /// </summary>
        /// <Author>Renjish N</Author>
        /// <DateCreated>10 Sep 09</DateCreated>
        /// <Parameters>XML data with voucher details</Parameters>
        /// <returns>Bool value with success/failure status</returns>
        ///
        /// Method Revision History
        ///
        /// Author             Date              Description
        /// ---------------------------------------------------
        /// Renjish N          10 Sep 09            Created

        [WebMethod, SoapHeader("AuthenticationInfo")]
        [SoapSecurityExtension(Encrypt = EncryptMode.Response)]
        public bool ImportVoucherDetails(string strXMLData)
        {
            CheckSecurity();

            bool bSuccess = false;
            try
            {
                SqlParameter[] oSQLParams = new SqlParameter[2];

                SqlParameter objSQLParam = new SqlParameter();
                objSQLParam.ParameterName = "doc";
                objSQLParam.Value = strXMLData;
                objSQLParam.Direction = ParameterDirection.Input;
                oSQLParams[0] = objSQLParam;

                objSQLParam = new SqlParameter();
                objSQLParam.ParameterName = "IsSuccess";
                objSQLParam.Direction = ParameterDirection.Output;
                objSQLParam.SqlDbType = SqlDbType.Int;
                oSQLParams[1] = objSQLParam;

                SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "RSP_IMPORTVOUCHERDETAILS", oSQLParams);

                if (int.Parse(oSQLParams[1].Value.ToString()) == 0)
                    bSuccess = true;
                else
                {
                    bSuccess = false;
                    LogManager.WriteLog("ImportVoucherDetails WS " + "  failed due to " + oSQLParams[1].Value.ToString(), LogManager.enumLogLevel.Info);
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
        /// Web method for importing ticket exception details.
        /// </summary>
        /// <Author>Renjish N</Author>
        /// <DateCreated>10 Sep 09</DateCreated>
        /// <Parameters>XML data with ticket exception details</Parameters>
        /// <returns>Bool value with success/failure status</returns>
        ///
        /// Method Revision History
        ///
        /// Author             Date              Description
        /// ---------------------------------------------------
        /// Renjish N          10 Sep 09            Created

        [WebMethod, SoapHeader("AuthenticationInfo")]
        [SoapSecurityExtension(Encrypt = EncryptMode.Response)]
        public bool ImportTicketExceptionDetails(string strXMLData, int iSiteCode)
        {
            CheckSecurity();

            bool bSuccess = false;
            try
            {
                SqlParameter[] oSQLParams = new SqlParameter[3];

                SqlParameter objSQLParam = new SqlParameter();
                objSQLParam.ParameterName = "doc";
                objSQLParam.Value = strXMLData;
                objSQLParam.Direction = ParameterDirection.Input;
                oSQLParams[0] = objSQLParam;

                objSQLParam = new SqlParameter();
                objSQLParam.ParameterName = "iSiteID";
                objSQLParam.Value = iSiteCode;
                objSQLParam.Direction = ParameterDirection.Input;
                oSQLParams[1] = objSQLParam;

                objSQLParam = new SqlParameter();
                objSQLParam.ParameterName = "IsSuccess";
                objSQLParam.Direction = ParameterDirection.Output;
                objSQLParam.SqlDbType = SqlDbType.Int;
                oSQLParams[2] = objSQLParam;

                SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "RSP_IMPORTTICKETEXCEPTIONDETAILS", oSQLParams);

                if (int.Parse(oSQLParams[2].Value.ToString()) == 0)
                {
                    bSuccess = true;
                    LogManager.WriteLog("ImportTicketExceptionDetails WS " + " - Successfully completed.", LogManager.enumLogLevel.Info);
                }
                else
                {
                    bSuccess = false;
                    LogManager.WriteLog("ImportTicketExceptionDetails WS " + "  failed due to " + oSQLParams[1].Value.ToString(), LogManager.enumLogLevel.Info);
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
        /// Web method for importing Promotions details.
        /// </summary>
        /// <Author>Durga Devi M</Author>
        /// <DateCreated>16 Sep 2013</DateCreated>
        /// <Parameters>XML data with Promotions details</Parameters>
        /// <returns>Bool value with success/failure status</returns>
        ///
      

        [WebMethod, SoapHeader("AuthenticationInfo")]
        [SoapSecurityExtension(Encrypt = EncryptMode.Response)]
        public bool ImportPromotionsDetails(string strXMLData, int iSiteCode)
        {
            CheckSecurity();

            bool bSuccess = false;
            try
            {
                SqlParameter[] oSQLParams = new SqlParameter[3];

                SqlParameter objSQLParam = new SqlParameter();
                objSQLParam.ParameterName = "doc";
                objSQLParam.Value = strXMLData;
                objSQLParam.Direction = ParameterDirection.Input;
                oSQLParams[0] = objSQLParam;

                objSQLParam = new SqlParameter();
                objSQLParam.ParameterName = "iSiteID";
                objSQLParam.Value = iSiteCode;
                objSQLParam.Direction = ParameterDirection.Input;
                oSQLParams[1] = objSQLParam;

                objSQLParam = new SqlParameter();
                objSQLParam.ParameterName = "IsSuccess";
                objSQLParam.Direction = ParameterDirection.Output;
                objSQLParam.SqlDbType = SqlDbType.Int;
                oSQLParams[2] = objSQLParam;

                SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "RSP_IMPORTPROMOTIONSDETAILS", oSQLParams);

                if (int.Parse(oSQLParams[2].Value.ToString()) == 0)
                {
                    bSuccess = true;
                    LogManager.WriteLog("ImportPromotionsDetails WS " + " - Successfully completed.", LogManager.enumLogLevel.Info);
                }
                else
                {
                    bSuccess = false;
                    LogManager.WriteLog("ImportPromotionsDetails WS " + "  failed due to " + oSQLParams[1].Value.ToString(), LogManager.enumLogLevel.Info);
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
        /// Web method for importing External Voucher details.
        /// </summary>
        /// <Author>Durga Devi M</Author>
        /// <DateCreated>16 Sep 2013</DateCreated>
        /// <Parameters>XML data with External Voucher details</Parameters>
        /// <returns>Bool value with success/failure status</returns>
        ///


        [WebMethod, SoapHeader("AuthenticationInfo")]
        [SoapSecurityExtension(Encrypt = EncryptMode.Response)]
        public bool ImportExternalVoucherDetails(string strXMLData, int iSiteCode)
        {
            CheckSecurity();

            bool bSuccess = false;
            try
            {
                SqlParameter[] oSQLParams = new SqlParameter[3];

                SqlParameter objSQLParam = new SqlParameter();
                objSQLParam.ParameterName = "doc";
                objSQLParam.Value = strXMLData;
                objSQLParam.Direction = ParameterDirection.Input;
                oSQLParams[0] = objSQLParam;

                objSQLParam = new SqlParameter();
                objSQLParam.ParameterName = "iSiteID";
                objSQLParam.Value = iSiteCode;
                objSQLParam.Direction = ParameterDirection.Input;
                oSQLParams[1] = objSQLParam;

                objSQLParam = new SqlParameter();
                objSQLParam.ParameterName = "IsSuccess";
                objSQLParam.Direction = ParameterDirection.Output;
                objSQLParam.SqlDbType = SqlDbType.Int;
                oSQLParams[2] = objSQLParam;
                

                SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "RSP_IMPORTEXTERNALVOUCHERDETAILS", oSQLParams);

                if (int.Parse(oSQLParams[2].Value.ToString()) == 0)
                {
                    bSuccess = true;
                    LogManager.WriteLog("ImportExternalVoucherDetails WS " + " - Successfully completed.", LogManager.enumLogLevel.Info);
                }
                else
                {
                    bSuccess = false;
                    LogManager.WriteLog("ImportExternalVoucherDetails WS " + "  failed due to " + oSQLParams[1].Value.ToString(), LogManager.enumLogLevel.Info);
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
        /// Web method for importing Promotional Tickets details.
        /// </summary>
        /// <Author>Durga Devi M</Author>
        /// <DateCreated>16 Sep 2013</DateCreated>
        /// <Parameters>XML data with Promotional Tickets details</Parameters>
        /// <returns>Bool value with success/failure status</returns>
        ///


        [WebMethod, SoapHeader("AuthenticationInfo")]
        [SoapSecurityExtension(Encrypt = EncryptMode.Response)]
        public bool ImportPromotionalTicketsDetails(string strXMLData, int iSiteCode)
        {
            CheckSecurity();

            bool bSuccess = false;
            try
            {
                SqlParameter[] oSQLParams = new SqlParameter[3];

                SqlParameter objSQLParam = new SqlParameter();
                objSQLParam.ParameterName = "doc";
                objSQLParam.Value = strXMLData;
                objSQLParam.Direction = ParameterDirection.Input;
                oSQLParams[0] = objSQLParam;

                objSQLParam = new SqlParameter();
                objSQLParam.ParameterName = "iSiteID";
                objSQLParam.Value = iSiteCode;
                objSQLParam.Direction = ParameterDirection.Input;
                oSQLParams[1] = objSQLParam;

                objSQLParam = new SqlParameter();
                objSQLParam.ParameterName = "IsSuccess";
                objSQLParam.Direction = ParameterDirection.Output;
                objSQLParam.SqlDbType = SqlDbType.Int;
                oSQLParams[2] = objSQLParam;

                SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "RSP_IMPORTPROMOTIONALTICKETSDETAILS", oSQLParams);

                if (int.Parse(oSQLParams[2].Value.ToString()) == 0)
                {
                    bSuccess = true;
                    LogManager.WriteLog("ImportPromotionsDetails WS " + " - Successfully completed.", LogManager.enumLogLevel.Info);
                }
                else
                {
                    bSuccess = false;
                    LogManager.WriteLog("ImportPromotionsDetails WS " + "  failed due to " + oSQLParams[1].Value.ToString(), LogManager.enumLogLevel.Info);
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
        /// Web method for importing device details.
        /// </summary>
        /// <Author>Renjish N</Author>
        /// <DateCreated>10 Sep 09</DateCreated>
        /// <Parameters>XML data with device details</Parameters>
        /// <returns>Bool value with success/failure status</returns>
        ///
        /// Method Revision History
        ///
        /// Author             Date              Description
        /// ---------------------------------------------------
        /// Renjish N          10 Sep 09            Created

        [WebMethod, SoapHeader("AuthenticationInfo")]
        [SoapSecurityExtension(Encrypt = EncryptMode.Response)]
        public bool ImportDeviceDetails(string strXMLData)
        {
            CheckSecurity();

            bool bSuccess = false;
            try
            {
                SqlParameter[] oSQLParams = new SqlParameter[2];

                SqlParameter objSQLParam = new SqlParameter();
                objSQLParam.ParameterName = "doc";
                objSQLParam.Value = strXMLData;
                objSQLParam.Direction = ParameterDirection.Input;
                oSQLParams[0] = objSQLParam;

                objSQLParam = new SqlParameter();
                objSQLParam.ParameterName = "IsSuccess";
                objSQLParam.Direction = ParameterDirection.Output;
                objSQLParam.SqlDbType = SqlDbType.Int;
                oSQLParams[1] = objSQLParam;

                SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "RSP_IMPORTDEVICEDETAILS", oSQLParams);

                if (int.Parse(oSQLParams[1].Value.ToString()) == 0)
                    bSuccess = true;
                else
                {
                    bSuccess = false;
                    LogManager.WriteLog("ImportDeviceDetails WS " + "  failed due to " + oSQLParams[1].Value.ToString(), LogManager.enumLogLevel.Info);
                }
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                bSuccess = false;
            }

            return bSuccess;
        }

        #endregion Ticket Details Import To Enterprise

        #region "Insert handpay"
        [WebMethod, SoapHeader("AuthenticationInfo")]
        [SoapSecurityExtension(Encrypt = EncryptMode.Response)]
        public bool InsertHandpay(string xmlString)
        {
            CheckSecurity();

            LogManager.WriteLog("InsertHandpay Called...", LogManager.enumLogLevel.Info);
            string strSPName = "usp_InsertHandpayFromXML";
            bool bSuccess = false;
            try
            {
                SqlParameter[] objSiteSQLParams = new SqlParameter[2];

                SqlParameter objSQLParam = new SqlParameter();
                objSQLParam.ParameterName = "doc";
                objSQLParam.Value = xmlString;
                objSQLParam.Direction = ParameterDirection.Input;
                objSiteSQLParams[0] = objSQLParam;

                objSQLParam = new SqlParameter();
                objSQLParam.Direction = ParameterDirection.ReturnValue;
                objSiteSQLParams[1] = objSQLParam;

                SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, strSPName, objSiteSQLParams);

                if (int.Parse(objSiteSQLParams[1].Value.ToString()) == 0)
                {
                    bSuccess = true;
                    LogManager.WriteLog("Import Handpay WS " + "  Success value " + bSuccess.ToString(), LogManager.enumLogLevel.Info);
                }
                else
                {
                    bSuccess = false;
                    LogManager.WriteLog("Import Handpay WS " + "  failed due to " + objSiteSQLParams[1].Value.ToString(), LogManager.enumLogLevel.Info);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                bSuccess = false;
            }
            return bSuccess;
        }
        #endregion "Insert handpay"

        #endregion

        #region Assorted Objects

        #region InstallationType enum

        public enum InstallationType
        {
            NewInstallation,
            GmuChange,
            ConvertInstallation,
            RemoveInstallation,
            PlannedConversion
        }

        #endregion

        #region ServiceTypes enum

        public enum ServiceTypes
        {
            All,
            Running,
            NotRunning
        }

        #endregion

        public class PlannedAssetInfo
        {
            public string GameCategory = string.Empty;
            public string GameTitle = string.Empty;
            public string ModelXML = string.Empty;
        }

        #endregion

        #region Site Recovery

        [WebMethod()]
        public int InitiateWebService()
        {
            return 1;
        }

        //[WebMethod, SoapHeader("AuthenticationInfo")]
        //[SoapSecurityExtension(Encrypt = EncryptMode.Response)]
        [WebMethod()]
        public string CheckTransactionKey(string Site_Code, string TransactionKey, string TransactionType)
        {
            //CheckSecurity();

            string SuccessCode = "";

            LogManager.WriteLog("CheckTransactionKey Called...", LogManager.enumLogLevel.Info);

            string strStoredProcedureName = string.Empty;

            try
            {
                SqlParameter[] oSQLParams = new SqlParameter[4];
                SqlParameter objSQLParam = new SqlParameter();
                objSQLParam.ParameterName = "@Site_Code";
                objSQLParam.Value = Site_Code;
                objSQLParam.Direction = ParameterDirection.Input;
                oSQLParams[0] = objSQLParam;

                objSQLParam = new SqlParameter();
                objSQLParam.ParameterName = "@TransactionKey";
                objSQLParam.Value = TransactionKey;
                objSQLParam.Direction = ParameterDirection.Input;
                oSQLParams[1] = objSQLParam;

                objSQLParam = new SqlParameter();
                objSQLParam.ParameterName = "@TransactionType";
                objSQLParam.Value = TransactionType;
                objSQLParam.Direction = ParameterDirection.Input;
                oSQLParams[2] = objSQLParam;

                objSQLParam = new SqlParameter();
                objSQLParam.ParameterName = "@IsAuthenticated";
                objSQLParam.SqlDbType = SqlDbType.NVarChar;
                objSQLParam.Size = 520;
                objSQLParam.Direction = ParameterDirection.Output;
                oSQLParams[3] = objSQLParam;

                strStoredProcedureName = "rsp_CheckTransactionKey";

                SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, strStoredProcedureName, oSQLParams);

                SuccessCode = oSQLParams[3].Value.ToString();

                LogManager.WriteLog("CheckTransactionKey Executed Successfully: key returns " + SuccessCode, LogManager.enumLogLevel.Info);

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return SuccessCode;
            }

            return SuccessCode;
        }

        [WebMethod, SoapHeader("AuthenticationInfo")]
        [SoapSecurityExtension(Encrypt = EncryptMode.Response)]
        public string GetSiteDetails(string Site_Code)
        {
            //GetSiteDetails from Enterprise on Site code verification send from Exchange
            CheckSecurity();


            string SiteId;
            string strStoredProcedureName = string.Empty;
            object objResult = null;
            string strSiteDetailsXMLData = string.Empty;

            LogManager.WriteLog("GetSiteDetails Called...", LogManager.enumLogLevel.Info);

            try
            {
                strStoredProcedureName = "rsp_ExportSiteDetails";
                objResult = SqlHelper.ExecuteScalar(GetConnectionString(), CommandType.Text, "select Site_ID from site where site_code=" + Site_Code);
                if (objResult == null)
                    strSiteDetailsXMLData = "";
                else
                {
                    SiteId = objResult.ToString();
                    SqlParameter[] oSQLParams = new SqlParameter[1];
                    SqlParameter objSQLParam = new SqlParameter();
                    objSQLParam.ParameterName = "@Site_ID";
                    objSQLParam.Value = SiteId;
                    objSQLParam.Direction = ParameterDirection.Input;
                    oSQLParams[0] = objSQLParam;

                    strSiteDetailsXMLData = SqlHelper.ExecuteScalar(GetConnectionString(), strStoredProcedureName, oSQLParams).ToString();
                    LogManager.WriteLog(strSiteDetailsXMLData.Length.ToString(), LogManager.enumLogLevel.Debug);
                    LogManager.WriteLog("GetSiteDetails Completed...", LogManager.enumLogLevel.Info);
                }
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                strSiteDetailsXMLData = "";
            }
            return strSiteDetailsXMLData;
        }

        [WebMethod, SoapHeader("AuthenticationInfo")]
        [SoapSecurityExtension(Encrypt = EncryptMode.Response)]
        public string GetInstallationData(int siteId)
        {
            CheckSecurity();

            LogManager.WriteLog("GetInstallationData Called...", LogManager.enumLogLevel.Info);
            string strInstallationstrXmlData = string.Empty;

            try
            {
                var oSqlParamsExportInstallations = new SqlParameter[1];
                var objSqlParam = new SqlParameter
                {
                    ParameterName = "Site_Id",
                    Value = siteId,
                    Direction = ParameterDirection.Input
                };
                oSqlParamsExportInstallations[0] = objSqlParam;
                const string strStoredProcedureName = "usp_GetInstallations";
                strInstallationstrXmlData = SqlHelper.ExecuteScalar(GetConnectionString(), strStoredProcedureName, oSqlParamsExportInstallations).ToString();
                LogManager.WriteLog(strInstallationstrXmlData.Length.ToString(), LogManager.enumLogLevel.Debug);
                LogManager.WriteLog("Installations Exported", LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return strInstallationstrXmlData;
            }


            return strInstallationstrXmlData;

        }

        [WebMethod, SoapHeader("AuthenticationInfo")]
        [SoapSecurityExtension(Encrypt = EncryptMode.Response)]
        public string GetAAMSDetails(int siteId)
        {
            CheckSecurity();

            LogManager.WriteLog("GetAAMSDetails Called...", LogManager.enumLogLevel.Info);
            string strInstallationstrXmlData = string.Empty;

            try
            {
                var oSqlParamsExportInstallations = new SqlParameter[1];
                var objSqlParam = new SqlParameter
                {
                    ParameterName = "Site_Id",
                    Value = siteId,
                    Direction = ParameterDirection.Input
                };
                oSqlParamsExportInstallations[0] = objSqlParam;
                const string strStoredProcedureName = "rsp_GetAAMSDetails";
                strInstallationstrXmlData = SqlHelper.ExecuteScalar(GetConnectionString(), strStoredProcedureName, oSqlParamsExportInstallations).ToString();
                LogManager.WriteLog(strInstallationstrXmlData.Length.ToString(), LogManager.enumLogLevel.Debug);
                LogManager.WriteLog("AAMSDetails Exported", LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return strInstallationstrXmlData;
            }


            return strInstallationstrXmlData;

        }

        [WebMethod, SoapHeader("AuthenticationInfo")]
        [SoapSecurityExtension(Encrypt = EncryptMode.Response)]
        public string GetInstallationGameInfo(int siteId)
        {
            CheckSecurity();

            LogManager.WriteLog("GetInstallationGameInfo Called...", LogManager.enumLogLevel.Info);
            string strInstallationstrXmlData = string.Empty;

            try
            {
                var oSqlParamsExportInstallations = new SqlParameter[1];
                var objSqlParam = new SqlParameter
                {
                    ParameterName = "Site_Id",
                    Value = siteId,
                    Direction = ParameterDirection.Input
                };
                oSqlParamsExportInstallations[0] = objSqlParam;
                const string strStoredProcedureName = "rsp_GetInstallationGameInfo";
                strInstallationstrXmlData = SqlHelper.ExecuteScalar(GetConnectionString(), strStoredProcedureName, oSqlParamsExportInstallations).ToString();
                LogManager.WriteLog(strInstallationstrXmlData.Length.ToString(), LogManager.enumLogLevel.Debug);
                LogManager.WriteLog("InstallationGameInfo Exported", LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return strInstallationstrXmlData;
            }


            return strInstallationstrXmlData;

        }

        [WebMethod, SoapHeader("AuthenticationInfo")]
        [SoapSecurityExtension(Encrypt = EncryptMode.Response)]

        public string GetSiteAllianceData(int siteId)
        {
            CheckSecurity();

            try
            {
                var sqlParamsExportSiteAlliances = new SqlParameter[1];
                var sqlParamsExportSiteAlliance = new SqlParameter
                {
                    ParameterName = "Site_Id",
                    Value = siteId,
                    Direction = ParameterDirection.Input
                };
                sqlParamsExportSiteAlliances[0] = sqlParamsExportSiteAlliance;
                return
                    SqlHelper.ExecuteScalar(GetConnectionString(), "usp_GetSiteAlliance", sqlParamsExportSiteAlliance).
                        ToString();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return "";
            }
        }

        [WebMethod, SoapHeader("AuthenticationInfo")]
        [SoapSecurityExtension(Encrypt = EncryptMode.Response)]

        public string GetZonesData(int siteId)
        {
            CheckSecurity();

            try
            {
                var sqlParamsExportZones = new SqlParameter[1];
                var sqlParamsExportZone = new SqlParameter
                {
                    ParameterName = "Site_Id",
                    Value = siteId,
                    Direction = ParameterDirection.Input
                };
                sqlParamsExportZones[0] = sqlParamsExportZone;
                return
                    SqlHelper.ExecuteScalar(GetConnectionString(), "usp_GetZones", sqlParamsExportZones).
                        ToString();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return "";
            }
        }

        [WebMethod, SoapHeader("AuthenticationInfo")]
        [SoapSecurityExtension(Encrypt = EncryptMode.Response)]
        public string GetBarPositionsData(int siteId)
        {
            CheckSecurity();

            try
            {
                var sqlParamsExportBarPositions = new SqlParameter[1];
                var sqlParamsExportBarPosition = new SqlParameter
                {
                    ParameterName = "Site_Id",
                    Value = siteId,
                    Direction = ParameterDirection.Input
                };
                sqlParamsExportBarPositions[0] = sqlParamsExportBarPosition;

                return
                    SqlHelper.ExecuteScalar(GetConnectionString(), "usp_GetBarpositions", sqlParamsExportBarPositions)
                        .ToString();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return "";
            }

        }

        [WebMethod, SoapHeader("AuthenticationInfo")]
        [SoapSecurityExtension(Encrypt = EncryptMode.Response)]
        public string GetMachineData(int siteId)
        {
            CheckSecurity();

            try
            {
                var sqlParamsExportMachines = new SqlParameter[1];
                var sqlParamsExportMachine = new SqlParameter
                {
                    ParameterName = "Site_Id",
                    Value = siteId,
                    Direction = ParameterDirection.Input
                };
                sqlParamsExportMachines[0] = sqlParamsExportMachine;
                return
                    SqlHelper.ExecuteScalar(GetConnectionString(), "usp_GetMachines", sqlParamsExportMachines).
                        ToString();

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return "";
            }
        }

        [WebMethod, SoapHeader("AuthenticationInfo")]
        [SoapSecurityExtension(Encrypt = EncryptMode.Response)]
        public DataTable GetLatestMeterHistory(string strInstallationXML)
        {
            CheckSecurity();
            DataTable dtTable = new DataTable();

            LogManager.WriteLog("GetLatestMeterHistoryData Called...", LogManager.enumLogLevel.Info);


            try
            {
                var sqlParamsExportLatestMeterHistory = new SqlParameter[1];
                var sqlParameter = new SqlParameter
                {
                    ParameterName = "@doc",
                    Value = strInstallationXML,
                    Direction = ParameterDirection.Input
                };
                sqlParamsExportLatestMeterHistory[0] = sqlParameter;

                //dtTable = SqlHelper.ExecuteDataset(GetConnectionString(), "usp_GetLatestMeterHistory",
                //                             sqlParamsExportLatestMeterHistory).Tables[0];
                dtTable = (SqlHelper.ExecuteDataset(GetConnectionString(), "usp_GetLatestMeterHistory",
                                             sqlParamsExportLatestMeterHistory).Tables.Count > 0) ? SqlHelper.ExecuteDataset(GetConnectionString(), "usp_GetLatestMeterHistory",
                                             sqlParamsExportLatestMeterHistory).Tables[0] : dtTable;

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                dtTable = new DataTable();
            }

            return dtTable;
        }

        [WebMethod, SoapHeader("AuthenticationInfo")]
        [SoapSecurityExtension(Encrypt = EncryptMode.Response)]
        public DataTable GetLatestSiteBatchID(int siteCode, int XDays)
        {
            CheckSecurity();

            DataTable dtTable = new DataTable();

            LogManager.WriteLog("GetLatestSiteBatchID Called...", LogManager.enumLogLevel.Info);

            try
            {
                var sqlParameters = new SqlParameter[2];
                var sqlParameter = new SqlParameter
                {
                    ParameterName = "@SiteCode",
                    Value = siteCode.ToString(),
                    Direction = ParameterDirection.Input
                };
                sqlParameters[0] = sqlParameter;

                var sqlParameter1 = new SqlParameter
                {
                    ParameterName = "@Xdays",
                    Value = XDays,
                    Direction = ParameterDirection.Input
                };
                sqlParameters[1] = sqlParameter1;

                //dtTable = SqlHelper.ExecuteDataset(GetConnectionString(), "rsp_GetBatchNumbers",
                //                             sqlParameters).Tables[0];
                dtTable = (SqlHelper.ExecuteDataset(GetConnectionString(), "rsp_GetBatchNumbers",
                                             sqlParameters).Tables.Count > 0) ? SqlHelper.ExecuteDataset(GetConnectionString(), "rsp_GetBatchNumbers",
                                             sqlParameters).Tables[0] : dtTable;


            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                dtTable = new DataTable();
            }

            return dtTable;
        }

        [WebMethod, SoapHeader("AuthenticationInfo")]
        [SoapSecurityExtension(Encrypt = EncryptMode.Response)]
        public string GetSiteTickets(int iSiteCode, int iRecords)
        {
            CheckSecurity();

            LogManager.WriteLog("GetSiteTickets Called...", LogManager.enumLogLevel.Info);
            string strStoredProcedureName = string.Empty;
            string strXML = string.Empty;

            try
            {
                SqlParameter[] oSQLParamsExportSiteTickets = new SqlParameter[2];
                SqlParameter objSQLParam = new SqlParameter();
                objSQLParam.ParameterName = "@iSiteID";
                objSQLParam.Value = iSiteCode;
                objSQLParam.Direction = ParameterDirection.Input;
                oSQLParamsExportSiteTickets[0] = objSQLParam;

                objSQLParam = new SqlParameter();
                objSQLParam.ParameterName = "@NoofDays";
                objSQLParam.Value = iRecords;
                objSQLParam.Direction = ParameterDirection.Input;
                oSQLParamsExportSiteTickets[1] = objSQLParam;

                strStoredProcedureName = "rsp_GetSiteVoucherDetails";

                strXML = SqlHelper.ExecuteScalar(GetConnectionString(), CommandType.StoredProcedure, strStoredProcedureName, oSQLParamsExportSiteTickets).ToString();

                LogManager.WriteLog("GetSiteTickets Exported", LogManager.enumLogLevel.Info);
                LogManager.WriteLog("GetSiteTickets Exported: " + strXML, LogManager.enumLogLevel.Debug);

            }
            catch (Exception exGetSiteTickets)
            {
                ExceptionManager.Publish(exGetSiteTickets);
                strXML = string.Empty;
            }

            return strXML;
        }

        [WebMethod, SoapHeader("AuthenticationInfo")]
        [SoapSecurityExtension(Encrypt = EncryptMode.Response)]
        public DataTable GetSiteTicketExceptions(int iSiteCode, int iRecords)
        {
            CheckSecurity();

            LogManager.WriteLog("GetSiteTicketExceptions Called...", LogManager.enumLogLevel.Info);
            string strStoredProcedureName = string.Empty;
            DataTable dtSiteTickets = null;

            try
            {
                SqlParameter[] oSQLParamsExportSiteTickets = new SqlParameter[2];
                SqlParameter objSQLParam = new SqlParameter();
                objSQLParam.ParameterName = "@iSiteID";
                objSQLParam.Value = iSiteCode;
                objSQLParam.Direction = ParameterDirection.Input;
                oSQLParamsExportSiteTickets[0] = objSQLParam;

                objSQLParam = new SqlParameter();
                objSQLParam.ParameterName = "@NoofDays";
                objSQLParam.Value = iRecords;
                objSQLParam.Direction = ParameterDirection.Input;
                oSQLParamsExportSiteTickets[1] = objSQLParam;

                strStoredProcedureName = "rsp_GetSiteTicketExceptionDetails";

                // dtSiteTickets = SqlHelper.ExecuteDataset(GetConnectionString(), strStoredProcedureName, oSQLParamsExportSiteTickets).Tables[0];
                dtSiteTickets = (SqlHelper.ExecuteDataset(GetConnectionString(), strStoredProcedureName, oSQLParamsExportSiteTickets).Tables.Count > 0) ? SqlHelper.ExecuteDataset(GetConnectionString(), strStoredProcedureName, oSQLParamsExportSiteTickets).Tables[0] : dtSiteTickets;
                LogManager.WriteLog("GetSiteTicketExceptions Exported", LogManager.enumLogLevel.Info);
                LogManager.WriteLog("GetSiteTicketExceptions Exported: " + dtSiteTickets.Rows.Count.ToString(), LogManager.enumLogLevel.Debug);

            }
            catch (Exception exGetSiteTicketExceptions)
            {
                ExceptionManager.Publish(exGetSiteTicketExceptions);
                dtSiteTickets = new DataTable();
            }

            return dtSiteTickets;
        }

        //[WebMethod, SoapHeader("AuthenticationInfo")]
        //[SoapSecurityExtension(Encrypt = EncryptMode.Response)]
        //public DataTable GetSitePromotions(int iSiteCode, int iRecords)
        //{
        //    CheckSecurity();

        //    LogManager.WriteLog("GetSitePromotions Called...", LogManager.enumLogLevel.Info);
        //    string strStoredProcedureName = string.Empty;
        //    DataTable dtSitePromotions = null;

        //    try
        //    {
        //        SqlParameter[] oSQLParamsExportSiteTickets = new SqlParameter[1];
        //        SqlParameter objSQLParam = new SqlParameter();
        //        objSQLParam.ParameterName = "@iSiteID";
        //        objSQLParam.Value = iSiteCode;
        //        objSQLParam.Direction = ParameterDirection.Input;
        //        oSQLParamsExportSiteTickets[0] = objSQLParam;

                

        //        strStoredProcedureName = "rsp_GetSitePromotionsDetails";

        //        // dtSiteTickets = SqlHelper.ExecuteDataset(GetConnectionString(), strStoredProcedureName, oSQLParamsExportSiteTickets).Tables[0];
        //        dtSitePromotions = (SqlHelper.ExecuteDataset(GetConnectionString(), strStoredProcedureName, oSQLParamsExportSiteTickets).Tables.Count > 0) ? SqlHelper.ExecuteDataset(GetConnectionString(), strStoredProcedureName, oSQLParamsExportSiteTickets).Tables[0] : dtSiteTickets;
        //        LogManager.WriteLog("GetSitePromotions Exported", LogManager.enumLogLevel.Info);
        //        LogManager.WriteLog("GetSitePromotions Exported: " + dtSitePromotions.Rows.Count.ToString(), LogManager.enumLogLevel.Debug);

        //    }
        //    catch (Exception exGetSiteTicketExceptions)
        //    {
        //        ExceptionManager.Publish(exGetSiteTicketExceptions);
        //        dtSitePromotions = new DataTable();
        //    }

        //    return dtSitePromotions;
        //}

        [WebMethod, SoapHeader("AuthenticationInfo")]
        [SoapSecurityExtension(Encrypt = EncryptMode.Response)]
        public DataTable GetSiteDeviceDetails(int iSiteCode)
        {
            CheckSecurity();

            LogManager.WriteLog("GetSiteDeviceDetails Called...", LogManager.enumLogLevel.Info);
            string strStoredProcedureName = string.Empty;
            DataTable dtSiteTickets = null;

            try
            {
                SqlParameter[] oSQLParamsExportSiteTickets = new SqlParameter[1];
                SqlParameter objSQLParam = new SqlParameter();
                objSQLParam.ParameterName = "@iSiteID";
                objSQLParam.Value = iSiteCode;
                objSQLParam.Direction = ParameterDirection.Input;
                oSQLParamsExportSiteTickets[0] = objSQLParam;

                strStoredProcedureName = "rsp_GetSiteDeviceDetails";

                // dtSiteTickets = SqlHelper.ExecuteDataset(GetConnectionString(), strStoredProcedureName, oSQLParamsExportSiteTickets).Tables[0];
                dtSiteTickets = (SqlHelper.ExecuteDataset(GetConnectionString(), strStoredProcedureName, oSQLParamsExportSiteTickets).Tables.Count > 0) ?
                    SqlHelper.ExecuteDataset(GetConnectionString(), strStoredProcedureName, oSQLParamsExportSiteTickets).Tables[0] : dtSiteTickets;
                LogManager.WriteLog("GetSiteDeviceDetails Exported", LogManager.enumLogLevel.Info);
                LogManager.WriteLog("GetSiteDeviceDetails Exported: " + dtSiteTickets.Rows.Count.ToString(), LogManager.enumLogLevel.Debug);

            }
            catch (Exception exGetSiteTicketExceptions)
            {
                ExceptionManager.Publish(exGetSiteTicketExceptions);
                dtSiteTickets = new DataTable();
            }

            return dtSiteTickets;
        }

        [WebMethod, SoapHeader("AuthenticationInfo")]
        [SoapSecurityExtension(Encrypt = EncryptMode.Response)]
        public DataTable GetSiteWorkstationDetails(int iSiteCode)
        {
            CheckSecurity();

            LogManager.WriteLog("GetSiteWorkstationDetails Called...", LogManager.enumLogLevel.Info);
            string strStoredProcedureName = string.Empty;
            DataTable dtSiteWorkStations = null;

            try
            {
                SqlParameter[] oSQLParamsExportSiteWorkStations = new SqlParameter[1];
                SqlParameter objSQLParam = new SqlParameter();
                objSQLParam.ParameterName = "@iSiteID";
                objSQLParam.Value = iSiteCode;
                objSQLParam.Direction = ParameterDirection.Input;
                oSQLParamsExportSiteWorkStations[0] = objSQLParam;

                strStoredProcedureName = "rsp_GetSiteWorkstationDetails";

                dtSiteWorkStations = (SqlHelper.ExecuteDataset(GetConnectionString(), strStoredProcedureName, oSQLParamsExportSiteWorkStations).Tables.Count > 0) ?
                    SqlHelper.ExecuteDataset(GetConnectionString(), strStoredProcedureName, oSQLParamsExportSiteWorkStations).Tables[0] : dtSiteWorkStations;
                LogManager.WriteLog("GetSiteWorkstationDetails Exported: " + dtSiteWorkStations.Rows.Count.ToString(), LogManager.enumLogLevel.Debug);

            }
            catch (Exception exGetSiteWorkStations)
            {
                ExceptionManager.Publish(exGetSiteWorkStations);
                dtSiteWorkStations = new DataTable();
            }

            return dtSiteWorkStations;
        }

        [WebMethod, SoapHeader("AuthenticationInfo")]
        [SoapSecurityExtension(Encrypt = EncryptMode.Response)]
        public DataTable GetHandpays(int iSiteCode, int iRecords)
        {
            CheckSecurity();

            LogManager.WriteLog("GetHandpays Called...", LogManager.enumLogLevel.Info);
            string strStoredProcedureName = string.Empty;
            DataTable dtHandpays = null;

            try
            {
                SqlParameter[] oSQLParamsExportHandpays = new SqlParameter[2];
                SqlParameter objSQLParam = new SqlParameter();
                objSQLParam.ParameterName = "iSiteID";
                objSQLParam.Value = iSiteCode;
                objSQLParam.Direction = ParameterDirection.Input;
                oSQLParamsExportHandpays[0] = objSQLParam;

                objSQLParam = new SqlParameter();
                objSQLParam.ParameterName = "@NoofDays";
                objSQLParam.Value = iRecords;
                objSQLParam.Direction = ParameterDirection.Input;
                oSQLParamsExportHandpays[1] = objSQLParam;

                strStoredProcedureName = "rsp_GetSiteHandpays";

                //dtHandpays = SqlHelper.ExecuteDataset(GetConnectionString(), strStoredProcedureName, oSQLParamsExportHandpays).Tables[0];
                dtHandpays = (SqlHelper.ExecuteDataset(GetConnectionString(), strStoredProcedureName, oSQLParamsExportHandpays).Tables.Count > 0) ?
                    SqlHelper.ExecuteDataset(GetConnectionString(), strStoredProcedureName, oSQLParamsExportHandpays).Tables[0] : dtHandpays;
                LogManager.WriteLog("GetHandpays Exported: " + dtHandpays.Rows.Count.ToString(), LogManager.enumLogLevel.Debug);
                LogManager.WriteLog("GetHandpays Exported", LogManager.enumLogLevel.Info);

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return dtHandpays;
            }

            return dtHandpays;
        }

        [WebMethod, SoapHeader("AuthenticationInfo")]
        [SoapSecurityExtension(Encrypt = EncryptMode.Response)]
        public string GetCashDeskTransactions(string strInstallationXML)
        {
            CheckSecurity();

            DataSet dsResult;
            string strReturn = string.Empty;
            LogManager.WriteLog("GetCashDeskTransactions Called...", LogManager.enumLogLevel.Info);

            try
            {
                var sqlParamsCashDeskTransactions = new SqlParameter[1];
                var sqlParameter = new SqlParameter
                {
                    ParameterName = "@doc",
                    Value = strInstallationXML,
                    Direction = ParameterDirection.Input
                };
                sqlParamsCashDeskTransactions[0] = sqlParameter;

                LogManager.WriteLog(strInstallationXML, LogManager.enumLogLevel.Info);
                dsResult = SqlHelper.ExecuteDataset(GetConnectionString(), "rsp_GetCashDeskTransactions",
                                         sqlParamsCashDeskTransactions);
                if (dsResult != null)
                {
                    if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
                    {
                        dsResult.DataSetName = "Transactions";
                        strReturn = dsResult.GetXml();
                        LogManager.WriteLog("GetCashDeskTransactions Exported: " + dsResult.Tables[0].Rows.Count, LogManager.enumLogLevel.Debug);
                    }
                    else
                    {
                        strReturn = string.Empty;
                        LogManager.WriteLog("GetCashDeskTransactions Exported: No Rows selected", LogManager.enumLogLevel.Debug);
                    }
                }
                LogManager.WriteLog("GetCashDeskTransactions Executed Successfully", LogManager.enumLogLevel.Info);

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                strReturn = string.Empty;
            }

            return strReturn;
        }
        [WebMethod, SoapHeader("AuthenticationInfo")]
        [SoapSecurityExtension(Encrypt = EncryptMode.Response)]
        public string GetCollections(int BatchId)
        {
            CheckSecurity();
            string ReturnXML;

            LogManager.WriteLog("GetCollections Called...", LogManager.enumLogLevel.Info);

            string strStoredProcedureName = string.Empty;

            try
            {
                SqlParameter[] oSQLParams = new SqlParameter[2];
                SqlParameter objSQLParam = new SqlParameter();
                objSQLParam.ParameterName = "@pvcCollection_No";
                objSQLParam.Value = BatchId;
                objSQLParam.Direction = ParameterDirection.Input;
                oSQLParams[0] = objSQLParam;

                objSQLParam = new SqlParameter();
                objSQLParam.ParameterName = "@SiteCode";
                objSQLParam.Value = AuthenticationInfo.SiteCode;
                objSQLParam.Direction = ParameterDirection.Input;
                oSQLParams[1] = objSQLParam;

                strStoredProcedureName = "rsp_ExportCollectionForXML";

                ReturnXML = SqlHelper.ExecuteScalar(GetConnectionString(), CommandType.StoredProcedure, strStoredProcedureName, oSQLParams).ToString();
                LogManager.WriteLog("GetCollections : " + ReturnXML.Length.ToString(), LogManager.enumLogLevel.Debug);
                LogManager.WriteLog("GetCollections Executed Successfully", LogManager.enumLogLevel.Info);

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return String.Empty;
            }

            return ReturnXML;
        }

        [WebMethod, SoapHeader("AuthenticationInfo")]
        [SoapSecurityExtension(Encrypt = EncryptMode.Response)]
        public string GetTreasuryDetails(int BatchId)
        {
            CheckSecurity();
            string ReturnXML;

            LogManager.WriteLog("GetTreasuryDetails Called...", LogManager.enumLogLevel.Info);

            string strStoredProcedureName = string.Empty;

            try
            {
                SqlParameter[] oSQLParams = new SqlParameter[2];
                SqlParameter objSQLParam = new SqlParameter();
                objSQLParam.ParameterName = "@BatchNo";
                objSQLParam.Value = BatchId;
                objSQLParam.Direction = ParameterDirection.Input;
                oSQLParams[0] = objSQLParam;

                objSQLParam = new SqlParameter();
                objSQLParam.ParameterName = "@SiteCode";
                objSQLParam.Value = AuthenticationInfo.SiteCode;
                objSQLParam.Direction = ParameterDirection.Input;
                oSQLParams[1] = objSQLParam;

                strStoredProcedureName = "rsp_ExportTreasuryXML";

                ReturnXML = SqlHelper.ExecuteScalar(GetConnectionString(), CommandType.StoredProcedure, strStoredProcedureName, oSQLParams).ToString();
                LogManager.WriteLog("GetTreasuryDetails : " + ReturnXML.Length.ToString(), LogManager.enumLogLevel.Debug);
                LogManager.WriteLog("GetTreasuryDetails Executed Successfully", LogManager.enumLogLevel.Info);

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return String.Empty;
            }

            return ReturnXML;
        }

        [WebMethod, SoapHeader("AuthenticationInfo")]
        [SoapSecurityExtension(Encrypt = EncryptMode.Response)]
        public string GetEventsDetails(int BatchId)
        {
            CheckSecurity();
            string ReturnXML;

            LogManager.WriteLog("GetEventsDetails Called...", LogManager.enumLogLevel.Info);

            string strStoredProcedureName = string.Empty;

            try
            {
                SqlParameter[] oSQLParams = new SqlParameter[2];
                SqlParameter objSQLParam = new SqlParameter();
                objSQLParam.ParameterName = "@BatchNo";
                objSQLParam.Value = BatchId;
                objSQLParam.Direction = ParameterDirection.Input;
                oSQLParams[0] = objSQLParam;

                objSQLParam = new SqlParameter();
                objSQLParam.ParameterName = "@SiteCode";
                objSQLParam.Value = AuthenticationInfo.SiteCode;
                objSQLParam.Direction = ParameterDirection.Input;
                oSQLParams[1] = objSQLParam;

                strStoredProcedureName = "rsp_ExportEventDetails";

                ReturnXML = SqlHelper.ExecuteScalar(GetConnectionString(), CommandType.StoredProcedure, strStoredProcedureName, oSQLParams).ToString();
                LogManager.WriteLog("GetEventsDetails : " + ReturnXML.Length.ToString(), LogManager.enumLogLevel.Debug);
                LogManager.WriteLog("GetEventsDetails Executed Successfully", LogManager.enumLogLevel.Info);

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return String.Empty;
            }

            return ReturnXML;
        }

        [WebMethod, SoapHeader("AuthenticationInfo")]
        [SoapSecurityExtension(Encrypt = EncryptMode.Response)]
        public DataTable GetDailyReads(string Site_Code, int xWeeks)
        {
            CheckSecurity();

            DataTable dtReturn = new DataTable();
            SqlParameter objSQLParam = null;
            LogManager.WriteLog("GetDailyReads Called...", LogManager.enumLogLevel.Info);

            string strStoredProcedureName = string.Empty;

            try
            {
                SqlParameter[] oSQLParams = new SqlParameter[2];
                objSQLParam = new SqlParameter();
                objSQLParam.ParameterName = "@Site_Code";
                objSQLParam.Value = Site_Code;
                objSQLParam.Direction = ParameterDirection.Input;
                oSQLParams[0] = objSQLParam;

                objSQLParam = new SqlParameter();
                objSQLParam.ParameterName = "@XDays";
                objSQLParam.Value = xWeeks;
                objSQLParam.Direction = ParameterDirection.Input;
                oSQLParams[1] = objSQLParam;

                strStoredProcedureName = "rsp_GetSiteDailyReads";

                dtReturn = SqlHelper.ExecuteDataset(GetConnectionString(), CommandType.StoredProcedure, strStoredProcedureName, oSQLParams).Tables[0];
                LogManager.WriteLog("GetDailyReads : " + dtReturn.Rows.Count.ToString(), LogManager.enumLogLevel.Debug);
                LogManager.WriteLog("GetDailyReads Executed Successfully", LogManager.enumLogLevel.Info);

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                dtReturn = new DataTable();
            }

            return dtReturn;
        }

        [WebMethod, SoapHeader("AuthenticationInfo")]
        [SoapSecurityExtension(Encrypt = EncryptMode.Response)]
        public string GetHourly(string Site_Code, int xWeeks)
        {
            CheckSecurity();

            string strXML = string.Empty;
            SqlParameter objSQLParam = null;
            LogManager.WriteLog("GetHourly Called...", LogManager.enumLogLevel.Info);

            string strStoredProcedureName = string.Empty;

            try
            {
                SqlParameter[] oSQLParams = new SqlParameter[2];
                objSQLParam = new SqlParameter();
                objSQLParam.ParameterName = "@Site_Code";
                objSQLParam.Value = Site_Code;
                objSQLParam.Direction = ParameterDirection.Input;
                oSQLParams[0] = objSQLParam;

                objSQLParam = new SqlParameter();
                objSQLParam.ParameterName = "@XDays";
                objSQLParam.Value = xWeeks;
                objSQLParam.Direction = ParameterDirection.Input;
                oSQLParams[1] = objSQLParam;

                strStoredProcedureName = "rsp_GetSiteHourlyData";

                strXML = SqlHelper.ExecuteScalar(GetConnectionString(), CommandType.StoredProcedure, strStoredProcedureName, oSQLParams).ToString();

                LogManager.WriteLog("GetHourly : " + strXML, LogManager.enumLogLevel.Debug);
                LogManager.WriteLog("GetHourly Executed Successfully", LogManager.enumLogLevel.Info);

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                strXML = string.Empty;
            }

            return strXML;
        }

        [WebMethod, SoapHeader("AuthenticationInfo")]
        [SoapSecurityExtension(Encrypt = EncryptMode.Response)]
        public string GetAllEvents(string Site_Code, int xWeeks)
        {
            CheckSecurity();

            string strSiteSettingsXMLData = "";
            SqlParameter objSQLParam = null;
            LogManager.WriteLog("GetAllEvents Called...", LogManager.enumLogLevel.Info);

            string strStoredProcedureName = string.Empty;

            try
            {
                SqlParameter[] oSQLParams = new SqlParameter[2];
                objSQLParam = new SqlParameter();
                objSQLParam.ParameterName = "@Site_Code";
                objSQLParam.Value = Site_Code;
                objSQLParam.Direction = ParameterDirection.Input;
                oSQLParams[0] = objSQLParam;

                objSQLParam = new SqlParameter();
                objSQLParam.ParameterName = "@XDays";
                objSQLParam.Value = xWeeks;
                objSQLParam.Direction = ParameterDirection.Input;
                oSQLParams[1] = objSQLParam;

                strStoredProcedureName = "rsp_GetSiteAllEvents";

                strSiteSettingsXMLData = SqlHelper.ExecuteScalar(GetConnectionString(), CommandType.StoredProcedure, strStoredProcedureName, oSQLParams).ToString();

                LogManager.WriteLog("GetAllEvents returns: " + strSiteSettingsXMLData.Length.ToString(), LogManager.enumLogLevel.Debug);
                LogManager.WriteLog("GetAllEvents Executed Successfully", LogManager.enumLogLevel.Info);

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return strSiteSettingsXMLData;
            }

            return strSiteSettingsXMLData;
        }

        [WebMethod, SoapHeader("AuthenticationInfo")]
        [SoapSecurityExtension(Encrypt = EncryptMode.Response)]
        public string GetSystemSettings(string Site_Code)
        {
            CheckSecurity();

            DataTable dtReturn = new DataTable();
            string SiteId = "";
            string strStoredProcedureName = string.Empty;
            object objResult = null;
            string strSiteSettingsXMLData = string.Empty;

            LogManager.WriteLog("GetSystemSettings Called...", LogManager.enumLogLevel.Info);

            try
            {
                objResult = SqlHelper.ExecuteScalar(GetConnectionString(), CommandType.Text, "select Site_ID from site where site_code=" + Site_Code);
                if (objResult == null)
                    strSiteSettingsXMLData = "";
                else
                {
                    SiteId = objResult.ToString();

                    SqlParameter[] oSQLParams = new SqlParameter[1];
                    SqlParameter objSQLParam = new SqlParameter();
                    objSQLParam.ParameterName = "@Site_ID";
                    objSQLParam.Value = SiteId;
                    objSQLParam.Direction = ParameterDirection.Input;
                    oSQLParams[0] = objSQLParam;

                    strStoredProcedureName = "rsp_GetSiteSettingsInXML";

                    strSiteSettingsXMLData = SqlHelper.ExecuteScalar(GetConnectionString(), CommandType.StoredProcedure, strStoredProcedureName, oSQLParams).ToString();
                    LogManager.WriteLog("GetSystemSettings...returns..:" + strSiteSettingsXMLData.Length.ToString(), LogManager.enumLogLevel.Debug);
                    LogManager.WriteLog("GetSystemSettings Executed Successfully", LogManager.enumLogLevel.Info);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                strSiteSettingsXMLData = "";
            }

            return strSiteSettingsXMLData;
        }


        [WebMethod, SoapHeader("AuthenticationInfo")]
        [SoapSecurityExtension(Encrypt = EncryptMode.Response)]
        public string GetLookupMasterSettings(string Site_Code)
        {
            CheckSecurity();

            DataTable dtReturn = new DataTable();
            //string SiteId = "";
            string strStoredProcedureName = string.Empty;
            //object objResult = null;
            string strSiteSettingsXMLData = string.Empty;

            LogManager.WriteLog("GetLookupMasterSettings Called...", LogManager.enumLogLevel.Info);

            try
            {


                strStoredProcedureName = "rsp_ExportLookupMasterALL";

                strSiteSettingsXMLData = SqlHelper.ExecuteScalar(GetConnectionString(), CommandType.StoredProcedure, strStoredProcedureName).ToString();
                LogManager.WriteLog("GetLookupMasterSettings...returns..:" + strSiteSettingsXMLData.Length.ToString(), LogManager.enumLogLevel.Debug);
                LogManager.WriteLog("GetLookupMasterSettings Executed Successfully", LogManager.enumLogLevel.Info);

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                strSiteSettingsXMLData = "";
            }

            return strSiteSettingsXMLData;
        }

        [WebMethod, SoapHeader("AuthenticationInfo")]
        [SoapSecurityExtension(Encrypt = EncryptMode.Response)]
        public bool SetSiteStatusEnterprise(string Site_Code, string sMessage)
        {
            CheckSecurity();
            bool bSuccess = false;
            LogManager.WriteLog("SetSiteStatusEnterprise Called...", LogManager.enumLogLevel.Info);

            try
            {
                int iRecordInserted = SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.Text, "Update Site Set SiteStatus='" + sMessage + "' where Site_Code=" + Site_Code);
                if (iRecordInserted > 0)
                {
                    bSuccess = true;
                }
                Object _Site_ID = SqlHelper.ExecuteScalar(GetConnectionString(), CommandType.Text, "select Site_ID from Site  where Site_Code=" + Site_Code);
                SqlParameter param;
                param = new SqlParameter("@SiteId", _Site_ID);
                SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "usp_EBS_UpdateSiteDetails",param);
                               
                LogManager.WriteLog("SetSiteStatusEnterprise Executed Successfully", LogManager.enumLogLevel.Info);

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                bSuccess = false;
            }

            return bSuccess;
        }

        [WebMethod, SoapHeader("AuthenticationInfo")]
        [SoapSecurityExtension(Encrypt = EncryptMode.Response)]
        public string GetOtherMachineDetailsForRecovery(string Site_Code)
        {
            CheckSecurity();

            try
            {
                var sqlParamsExportMachines = new SqlParameter[2];
                var sqlParamsExportMachine = new SqlParameter
                {
                    ParameterName = "ID",
                    Value = "All",
                    Direction = ParameterDirection.Input
                };

                var sqlParamsExportMac = new SqlParameter
                {
                    ParameterName = "AssetNumber",
                    Value = string.Empty,
                    Direction = ParameterDirection.Input
                };

                sqlParamsExportMachines[0] = sqlParamsExportMachine;
                sqlParamsExportMachines[1] = sqlParamsExportMac;
                return
                    SqlHelper.ExecuteScalar(GetConnectionString(), "rsp_ExportModelDetails", sqlParamsExportMachines).
                        ToString();

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return "";
            }
        }

        [WebMethod, SoapHeader("AuthenticationInfo")]
        [SoapSecurityExtension(Encrypt = EncryptMode.Response)]
        public bool ResetTransactionKey(string Site_Code, string TransactionKey)
        {
            CheckSecurity();

            LogManager.WriteLog("ResetTransactionKey Called...", LogManager.enumLogLevel.Info);

            string strStoredProcedureName = string.Empty;

            try
            {
                SqlParameter[] oSQLParams = new SqlParameter[2];
                SqlParameter objSQLParam = new SqlParameter();
                objSQLParam.ParameterName = "@Site_Code";
                objSQLParam.Value = Site_Code;
                objSQLParam.Direction = ParameterDirection.Input;
                oSQLParams[0] = objSQLParam;

                objSQLParam = new SqlParameter();
                objSQLParam.ParameterName = "@TransactionKey";
                objSQLParam.Value = TransactionKey;
                objSQLParam.Direction = ParameterDirection.Input;
                oSQLParams[1] = objSQLParam;

                strStoredProcedureName = "usp_UpdateTransactionKey";

                SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, strStoredProcedureName, oSQLParams);

                LogManager.WriteLog("ResetTransactionKey Executed Successfully: key returns " + "true", LogManager.enumLogLevel.Info);

                return true;

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return false;
            }
        }

        [WebMethod, SoapHeader("AuthenticationInfo")]
        [SoapSecurityExtension(Encrypt = EncryptMode.Response)]
        public string CheckNGA(string Site_Code, string strMACList)
        {
            CheckSecurity();

            LogManager.WriteLog("CheckNGA Called...", LogManager.enumLogLevel.Info);

            string strStoredProcedureName = string.Empty;
            string strReturnCode = "-1";
            string strResultSet = string.Empty;

            string[] strMacListArray = strMACList.Split(',');

            foreach (string strMAC in strMacListArray)
            {
                try
                {
                    strResultSet = strResultSet + strMAC + '/';
                    SqlParameter[] oSQLParams = new SqlParameter[3];
                    SqlParameter objSQLParam = new SqlParameter();
                    objSQLParam.ParameterName = "@Site_Code";
                    objSQLParam.Value = Site_Code;
                    objSQLParam.Direction = ParameterDirection.Input;
                    oSQLParams[0] = objSQLParam;

                    objSQLParam = new SqlParameter();
                    objSQLParam.ParameterName = "@MAC_ADDDRESS";
                    objSQLParam.Value = strMAC;
                    objSQLParam.Direction = ParameterDirection.Input;
                    oSQLParams[1] = objSQLParam;

                    objSQLParam = new SqlParameter();
                    objSQLParam.ParameterName = "@IsAuthenticated";
                    objSQLParam.SqlDbType = SqlDbType.Int;
                    objSQLParam.Direction = ParameterDirection.Output;
                    oSQLParams[2] = objSQLParam;

                    strStoredProcedureName = "usp_UpdateNonGamingAsset";

                    SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, strStoredProcedureName, oSQLParams);

                    strReturnCode = oSQLParams[2].Value.ToString();

                    strResultSet = strResultSet + strReturnCode + ',';

                    if (strReturnCode == "2" || strReturnCode == "3" || strReturnCode == "4")
                    {
                        LogManager.WriteLog("CheckNGA Executed Successfully: key returns " + strResultSet, LogManager.enumLogLevel.Info);
                        return strResultSet;
                    }

                }
                catch (Exception ex)
                {
                    ExceptionManager.Publish(ex);
                    strResultSet = strResultSet + "-1,";
                }
            }

            LogManager.WriteLog("CheckNGA Executed Successfully: key returns " + strResultSet, LogManager.enumLogLevel.Info);
            return strResultSet;
        }

        [WebMethod, SoapHeader("AuthenticationInfo")]
        [SoapSecurityExtension(Encrypt = EncryptMode.Response)]
        public string GetAllUserDetails(string Site_Code)
        {
            CheckSecurity();

            string strAllUserDetailsXMLData = string.Empty;
            SqlParameter objSQLParam = null;
            LogManager.WriteLog("GetAllUserDetails Called...", LogManager.enumLogLevel.Info);

            string strStoredProcedureName = string.Empty;

            try
            {
                SqlParameter[] oSQLParams = new SqlParameter[1];
                objSQLParam = new SqlParameter();
                objSQLParam.ParameterName = "@Site_Code";
                objSQLParam.Value = Site_Code;
                objSQLParam.Direction = ParameterDirection.Input;
                oSQLParams[0] = objSQLParam;

                strStoredProcedureName = "rsp_GetSiteAllUsersinXML";

                strAllUserDetailsXMLData = SqlHelper.ExecuteScalar(GetConnectionString(), CommandType.StoredProcedure, strStoredProcedureName, oSQLParams).ToString();

                LogManager.WriteLog("GetAllUserDetails returns: " + strAllUserDetailsXMLData.Length.ToString(), LogManager.enumLogLevel.Debug);
                LogManager.WriteLog("GetAllUserDetails Executed Successfully", LogManager.enumLogLevel.Info);

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return strAllUserDetailsXMLData;
            }

            return strAllUserDetailsXMLData;
        }

        [WebMethod, SoapHeader("AuthenticationInfo")]
        [SoapSecurityExtension(Encrypt = EncryptMode.Response)]
        public string GetAllUserRoles(string Site_Code)
        {
            CheckSecurity();

            string strAllUserRolesXMLData = string.Empty;
            LogManager.WriteLog("GetAllUserRoles Called...", LogManager.enumLogLevel.Info);

            string strStoredProcedureName = string.Empty;

            try
            {
                strStoredProcedureName = "rsp_GetSiteAllRoleAccessRoleLnkinXML";

                strAllUserRolesXMLData = SqlHelper.ExecuteScalar(GetConnectionString(), CommandType.StoredProcedure, strStoredProcedureName).ToString();

                LogManager.WriteLog("GetAllUserRoles returns: " + strAllUserRolesXMLData.Length.ToString(), LogManager.enumLogLevel.Debug);
                LogManager.WriteLog("GetAllUserRoles Executed Successfully", LogManager.enumLogLevel.Info);

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return strAllUserRolesXMLData;
            }

            return strAllUserRolesXMLData;
        }

        [WebMethod, SoapHeader("AuthenticationInfo")]
        [SoapSecurityExtension(Encrypt = EncryptMode.Response)]
        public string GetAllUserRolesLinks(string Site_Code)
        {
            CheckSecurity();

            string strAllUserRolesLinksXMLData = string.Empty;
            SqlParameter objSQLParam = null;
            LogManager.WriteLog("GetAllUserRolesLinks Called...", LogManager.enumLogLevel.Info);

            string strStoredProcedureName = string.Empty;

            try
            {
                SqlParameter[] oSQLParams = new SqlParameter[1];
                objSQLParam = new SqlParameter();
                objSQLParam.ParameterName = "@Site_Code";
                objSQLParam.Value = Site_Code;
                objSQLParam.Direction = ParameterDirection.Input;
                oSQLParams[0] = objSQLParam;

                strStoredProcedureName = "rsp_GetSiteAllUserRoleLinkinXML";

                strAllUserRolesLinksXMLData = SqlHelper.ExecuteScalar(GetConnectionString(), CommandType.StoredProcedure, strStoredProcedureName, oSQLParams).ToString();

                LogManager.WriteLog("GetAllUserRolesLinks returns: " + strAllUserRolesLinksXMLData.Length.ToString(), LogManager.enumLogLevel.Debug);
                LogManager.WriteLog("GetAllUserRolesLinks Executed Successfully", LogManager.enumLogLevel.Info);

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return strAllUserRolesLinksXMLData;
            }

            return strAllUserRolesLinksXMLData;
        }

        [WebMethod, SoapHeader("AuthenticationInfo")]
        [SoapSecurityExtension(Encrypt = EncryptMode.Response)]
        public string GetAllObjects()
        {
            CheckSecurity();

            string strAllObjects = string.Empty;
            LogManager.WriteLog("GetAllObjects Called...", LogManager.enumLogLevel.Info);

            try
            {
                strAllObjects = SqlHelper.ExecuteScalar(GetConnectionString(), CommandType.StoredProcedure, "rsp_GetExchangeAdminObject").ToString();
                LogManager.WriteLog("GetAllObjects returns: " + strAllObjects.Length.ToString(), LogManager.enumLogLevel.Debug);
                LogManager.WriteLog("GetAllObjects Executed Successfully", LogManager.enumLogLevel.Info);

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return strAllObjects;
            }

            return strAllObjects;
        }

        [WebMethod, SoapHeader("AuthenticationInfo")]
        [SoapSecurityExtension(Encrypt = EncryptMode.Response)]
        public string GetAllRoleAccessObjectRightLnk()
        {
            CheckSecurity();

            string strAllRoleAccessObjectRightLnk = string.Empty;
            LogManager.WriteLog("GetAllRoleAccessObjectRightLnk Called...", LogManager.enumLogLevel.Info);
            try
            {
                strAllRoleAccessObjectRightLnk = SqlHelper.ExecuteScalar(GetConnectionString(), CommandType.StoredProcedure, "rsp_GetRoleAccessObjectRightLnk").ToString();
                LogManager.WriteLog("GetAllRoleAccessObjectRightLnk returns: " + strAllRoleAccessObjectRightLnk.Length.ToString(), LogManager.enumLogLevel.Debug);
                LogManager.WriteLog("GetAllRoleAccessObjectRightLnk Executed Successfully", LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return strAllRoleAccessObjectRightLnk;
            }

            return strAllRoleAccessObjectRightLnk;
        }

        [WebMethod, SoapHeader("AuthenticationInfo")]
        [SoapSecurityExtension(Encrypt = EncryptMode.Response)]
        public string GetCalendars(string Site_Code)
        {
            CheckSecurity();

            string strCalendarsXMLData = string.Empty;
            SqlParameter objSQLParam = null;
            DataTable dtCalendar = null;
            SqlParameter[] oSQLParams = null;

            LogManager.WriteLog("GetCalendars Called...", LogManager.enumLogLevel.Info);

            string strStoredProcedureName = string.Empty;

            try
            {
                oSQLParams = new SqlParameter[1];
                objSQLParam = new SqlParameter();
                objSQLParam.ParameterName = "@Site_Code";
                objSQLParam.Value = Site_Code;
                objSQLParam.Direction = ParameterDirection.Input;
                oSQLParams[0] = objSQLParam;

                strStoredProcedureName = "rsp_GetCalendars";

                dtCalendar = (SqlHelper.ExecuteDataset(GetConnectionString(), CommandType.StoredProcedure, strStoredProcedureName, oSQLParams).Tables.Count > 0) ?
                    SqlHelper.ExecuteDataset(GetConnectionString(), CommandType.StoredProcedure, strStoredProcedureName, oSQLParams).Tables[0] : new DataTable();

                LogManager.WriteLog("Active calendar in enterprise: " + dtCalendar.Rows.Count.ToString() + " for the site id:" + dtCalendar.Rows[0]["Site_ID"].ToString(), LogManager.enumLogLevel.Debug);

                if (dtCalendar.Rows.Count > 0)
                {
                    oSQLParams = null;
                    objSQLParam = null;
                    strStoredProcedureName = string.Empty;

                    oSQLParams = new SqlParameter[2];

                    objSQLParam = new SqlParameter();
                    objSQLParam.ParameterName = "@Site_id";
                    objSQLParam.Value = Convert.ToInt32(dtCalendar.Rows[0]["Site_ID"].ToString());
                    objSQLParam.Direction = ParameterDirection.Input;
                    oSQLParams[0] = objSQLParam;

                    objSQLParam = new SqlParameter();
                    objSQLParam.ParameterName = "@Calendar_Type";
                    objSQLParam.Value = "S-CALENDAR";
                    objSQLParam.Direction = ParameterDirection.Input;
                    oSQLParams[1] = objSQLParam;

                    strStoredProcedureName = "rsp_ExportCalendarDetails";

                    strCalendarsXMLData = SqlHelper.ExecuteScalar(GetConnectionString(), CommandType.StoredProcedure, strStoredProcedureName, oSQLParams).ToString();

                    LogManager.WriteLog("GetCalendars returns: " + strCalendarsXMLData.Length.ToString(), LogManager.enumLogLevel.Debug);
                    LogManager.WriteLog("GetCalendars Executed Successfully", LogManager.enumLogLevel.Info);

                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            return strCalendarsXMLData;
        }

        [WebMethod, SoapHeader("AuthenticationInfo")]
        [SoapSecurityExtension(Encrypt = EncryptMode.Response)]
        public string GetComponentDetails(string Site_Code)
        {
            CheckSecurity();

            LogManager.WriteLog("GetComponentDetails Called...", LogManager.enumLogLevel.Info);
            string strComponentDetailsXmlData = string.Empty;
            SqlConnection conn;
            try
            {
                const string strStoredProcedureName = "rsp_GetComponentDetailsForSiteConfig";
                conn = new SqlConnection(GetConnectionString());

                SqlDataReader objReaderData = SqlHelper.ExecuteReader(conn, CommandType.StoredProcedure, strStoredProcedureName);
                while (objReaderData.Read())
                {
                    strComponentDetailsXmlData = strComponentDetailsXmlData + objReaderData[0].ToString();
                }

                LogManager.WriteLog(strComponentDetailsXmlData.Length.ToString(), LogManager.enumLogLevel.Debug);
                LogManager.WriteLog("GetComponentDetails Exported", LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                strComponentDetailsXmlData = string.Empty;
            }

            return strComponentDetailsXmlData;
        }

        [WebMethod, SoapHeader("AuthenticationInfo")]
        [SoapSecurityExtension(Encrypt = EncryptMode.Response)]
        public string GetOtherGameDetails(string Site_Code)
        {
            CheckSecurity();

            LogManager.WriteLog("GetOtherGameDetails Called...", LogManager.enumLogLevel.Info);
            string strCatDetailsXmlData = string.Empty;
            string strTitleDetailsXmlData = string.Empty;
            string strLibraryDetailsXmlData = string.Empty;
            string strStoredProcedureName = string.Empty;

            try
            {
                var oSqlParams = new SqlParameter[1];
                var objSqlParam = new SqlParameter
                {
                    ParameterName = "Site_Id",
                    Value = Site_Code,
                    Direction = ParameterDirection.Input
                };
                oSqlParams[0] = objSqlParam;

                strStoredProcedureName = "rsp_GetGameCategoryForSiteConfig";
                strCatDetailsXmlData = SqlHelper.ExecuteScalar(GetConnectionString(), strStoredProcedureName, oSqlParams).ToString();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                strCatDetailsXmlData = string.Empty;
            }

            try
            {
                var oSqlParams = new SqlParameter[1];
                var objSqlParam = new SqlParameter
                {
                    ParameterName = "Site_Id",
                    Value = Site_Code,
                    Direction = ParameterDirection.Input
                };
                oSqlParams[0] = objSqlParam;

                strStoredProcedureName = "rsp_GetGameTitleForSiteConfig";
                strTitleDetailsXmlData = SqlHelper.ExecuteScalar(GetConnectionString(), strStoredProcedureName, oSqlParams).ToString();

                LogManager.WriteLog("GetOtherGameDetails Exported", LogManager.enumLogLevel.Info);
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                strTitleDetailsXmlData = string.Empty;
            }

            try
            {
                var oSqlParams = new SqlParameter[1];
                var objSqlParam = new SqlParameter
                {
                    ParameterName = "Site_Id",
                    Value = Site_Code,
                    Direction = ParameterDirection.Input
                };
                oSqlParams[0] = objSqlParam;

                strStoredProcedureName = "rsp_GetGameLibraryForSiteConfig";
                strLibraryDetailsXmlData = SqlHelper.ExecuteScalar(GetConnectionString(), strStoredProcedureName, oSqlParams).ToString();

                LogManager.WriteLog("GetOtherGameDetails Exported", LogManager.enumLogLevel.Info);
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                strLibraryDetailsXmlData = string.Empty;
            }

            return strCatDetailsXmlData + "|" + strTitleDetailsXmlData + "|" + strLibraryDetailsXmlData;
        }

        [WebMethod, SoapHeader("AuthenticationInfo")]
        [SoapSecurityExtension(Encrypt = EncryptMode.Response)]
        public string GetGameLibrary(string Site_Code)
        {
            CheckSecurity();

            LogManager.WriteLog("GetGameLibrary Called...", LogManager.enumLogLevel.Info);
            string strLibraryDetailsXmlData = string.Empty;
            string strStoredProcedureName = string.Empty;

            try
            {
                var oSqlParams = new SqlParameter[1];
                var objSqlParam = new SqlParameter
                {
                    ParameterName = "Site_Id",
                    Value = Site_Code,
                    Direction = ParameterDirection.Input
                };
                oSqlParams[0] = objSqlParam;

                strStoredProcedureName = "rsp_GetGameLibraryForSiteConfig";
                strLibraryDetailsXmlData = SqlHelper.ExecuteScalar(GetConnectionString(), strStoredProcedureName, oSqlParams).ToString();

                LogManager.WriteLog("GetGameLibrary Exported", LogManager.enumLogLevel.Info);
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                strLibraryDetailsXmlData = string.Empty;
            }

            return strLibraryDetailsXmlData;
        }

        [WebMethod, SoapHeader("AuthenticationInfo")]
        [SoapSecurityExtension(Encrypt = EncryptMode.Response)]
        public string GetPayTable(string Site_Code)
        {
            CheckSecurity();

            LogManager.WriteLog("GetPayTable Called...", LogManager.enumLogLevel.Info);
            string strPaytableXmlData = string.Empty;
            string strStoredProcedureName = string.Empty;

            try
            {
                var oSqlParams = new SqlParameter[1];
                var objSqlParam = new SqlParameter
                {
                    ParameterName = "Site_Id",
                    Value = Site_Code,
                    Direction = ParameterDirection.Input
                };
                oSqlParams[0] = objSqlParam;

                strStoredProcedureName = "rsp_GetPayTableForSiteConfig";
                strPaytableXmlData = SqlHelper.ExecuteScalar(GetConnectionString(), strStoredProcedureName, oSqlParams).ToString();

                LogManager.WriteLog("GetPayTable Exported", LogManager.enumLogLevel.Info);
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                strPaytableXmlData = string.Empty;
            }

            return strPaytableXmlData;
        }

        [WebMethod, SoapHeader("AuthenticationInfo")]
        [SoapSecurityExtension(Encrypt = EncryptMode.Response)]
        public string GetInstallationGamePayTableInfo(string Site_Code)
        {
            CheckSecurity();

            LogManager.WriteLog("GetInstallationGamePayTableInfo Called...", LogManager.enumLogLevel.Info);
            string strPaytableXmlData = string.Empty;
            string strStoredProcedureName = string.Empty;

            try
            {
                var oSqlParams = new SqlParameter[1];
                var objSqlParam = new SqlParameter
                {
                    ParameterName = "Site_Id",
                    Value = Site_Code,
                    Direction = ParameterDirection.Input
                };
                oSqlParams[0] = objSqlParam;

                strStoredProcedureName = "rsp_GetInstallationGamePayTableInfoForSiteConfig";
                strPaytableXmlData = SqlHelper.ExecuteScalar(GetConnectionString(), strStoredProcedureName, oSqlParams).ToString();

                LogManager.WriteLog("GetInstallationGamePayTableInfo Exported", LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                strPaytableXmlData = string.Empty;
            }

            return strPaytableXmlData;
        }

        [WebMethod, SoapHeader("AuthenticationInfo")]
        [SoapSecurityExtension(Encrypt = EncryptMode.Response)]
        public string GetSeedValues(string Site_Code, string Tables)
        {
            CheckSecurity();

            LogManager.WriteLog("GetSeedValues Called...: " + Tables, LogManager.enumLogLevel.Info);

            string sReturn = "";
            try
            {
                string[] Table = Tables.Split(',');

                foreach (string tab in Table)
                {
                    if (tab != null && tab.Length > 0)
                    {
                        SqlParameter[] param = new SqlParameter[3];

                        param[0] = new SqlParameter("@Site_Code", SqlDbType.Int);
                        param[0].Value = Int32.Parse(Site_Code);

                        param[1] = new SqlParameter("@Table", SqlDbType.VarChar, 100);
                        param[1].Value = tab;

                        param[2] = new SqlParameter("@SeedValue", SqlDbType.Int);
                        param[2].Direction = ParameterDirection.Output;

                        SqlConnection objcon = new SqlConnection(GetConnectionString());
                        using (objcon)
                        {
                            objcon.Open();
                            SqlCommand cmd = new SqlCommand("rsp_GetSeedValue", objcon);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddRange(param);
                            cmd.ExecuteNonQuery();
                            objcon.Close();

                            sReturn = sReturn + tab + ":" + param[2].SqlValue.ToString() + ",";
                            LogManager.WriteLog("Seed Value: " + sReturn, LogManager.enumLogLevel.Info);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            return sReturn;
        }

        [WebMethod, SoapHeader("AuthenticationInfo")]
        [SoapSecurityExtension(Encrypt = EncryptMode.Response)]
        public string GetAFTTransactions(string Site_Code)
        {
            CheckSecurity();

            string strXML = string.Empty;
            SqlParameter objSQLParam = null;
            LogManager.WriteLog("GetAFTTransactions Called...", LogManager.enumLogLevel.Info);

            string strStoredProcedureName = string.Empty;

            try
            {
                SqlParameter[] oSQLParams = new SqlParameter[1];
                objSQLParam = new SqlParameter();
                objSQLParam.ParameterName = "@Site_Code";
                objSQLParam.Value = Site_Code;
                objSQLParam.Direction = ParameterDirection.Input;
                oSQLParams[0] = objSQLParam;

                strStoredProcedureName = "rsp_GetSiteAFTTransactionsInXML";

                strXML = SqlHelper.ExecuteScalar(GetConnectionString(), CommandType.StoredProcedure, strStoredProcedureName, oSQLParams).ToString();

                LogManager.WriteLog("GetAFTTransactions : " + strXML, LogManager.enumLogLevel.Debug);
                LogManager.WriteLog("GetAFTTransactions Executed Successfully", LogManager.enumLogLevel.Info);

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                strXML = string.Empty;
            }

            return strXML;
        }


        [WebMethod, SoapHeader("AuthenticationInfo")]
        [SoapSecurityExtension(Encrypt = EncryptMode.Response)]
        public string GetSiteAuditHistoryDetails(string Site_Code)
        {
            CheckSecurity();

            string strXML = string.Empty;
            SqlParameter objSQLParam = null;
            LogManager.WriteLog("GetSiteAuditHistoryDetails Called...", LogManager.enumLogLevel.Info);

            string strStoredProcedureName = string.Empty;

            try
            {
                SqlParameter[] oSQLParams = new SqlParameter[1];
                objSQLParam = new SqlParameter();
                objSQLParam.ParameterName = "@Site_Code";
                objSQLParam.Value = Site_Code;
                objSQLParam.Direction = ParameterDirection.Input;
                oSQLParams[0] = objSQLParam;

                strStoredProcedureName = "rsp_GetSiteAuditHistoryDetailsInXML";

                strXML = SqlHelper.ExecuteScalar(Common.Utilities.DatabaseHelper.GetAuditConnectionString(), CommandType.StoredProcedure, strStoredProcedureName, oSQLParams).ToString();

                LogManager.WriteLog("GetSiteAuditHistoryDetails : " + strXML, LogManager.enumLogLevel.Debug);
                LogManager.WriteLog("GetSiteAuditHistoryDetails Executed Successfully", LogManager.enumLogLevel.Info);

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                strXML = string.Empty;
            }

            return strXML;
        }

        #endregion Site Recovery

        #region MGMD

        [WebMethod, SoapHeader("AuthenticationInfo")]
        [SoapSecurityExtension(Encrypt = EncryptMode.Response)]
        public int GetMultiGameInstallID(string NewXML)
        {
            CheckSecurity();

            try
            {
                var outParam = DataBaseServiceHandler.AddParameter<int>("Install", DbType.Int32, 0, ParameterDirection.Output);
                DataBaseServiceHandler.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "usp_InsertMGMDInstallation",
                    DataBaseServiceHandler.AddParameter<string>("doc", DbType.Xml, NewXML), outParam);

                return int.Parse(outParam.Value.ToString());
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                return -1;
            }
        }

        [WebMethod, SoapHeader("AuthenticationInfo")]
        [SoapSecurityExtension(Encrypt = EncryptMode.Response)]
        public int GetGameLibraryID(string NewXML)
        {
            CheckSecurity();

            try
            {
                var outParam = DataBaseServiceHandler.AddParameter<int>("Game_ID", DbType.Int32, 0, ParameterDirection.Output);
                DataBaseServiceHandler.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "usp_UpdateGameLibrary",
                    DataBaseServiceHandler.AddParameter<string>("doc", DbType.Xml, NewXML), outParam);

                return int.Parse(outParam.Value.ToString());
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                return -1;
            }
        }

        [WebMethod, SoapHeader("AuthenticationInfo")]
        [SoapSecurityExtension(Encrypt = EncryptMode.Response)]
        public bool ImportPaytableDetails(string PT_Xml)
        {
            CheckSecurity();

            try
            {
                DataBaseServiceHandler.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "usp_UpdatePayTable",
                    DataBaseServiceHandler.AddParameter<string>("doc", DbType.Xml, PT_Xml));

                return true;
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                return false;
            }
        }

        #endregion MGMD

        [WebMethod, SoapHeader("AuthenticationInfo")]
        [SoapSecurityExtension(Encrypt = EncryptMode.Response)]
        public bool UpdateDetailsFromXML(string UpdationType, string XMLString)
        {
            string ProcedureName = string.Empty;
            SqlParameter[] param;
            CheckSecurity();
            try
            {
                switch (UpdationType.ToUpper())
                {
                    case "GAMECHANGE":
                        ProcedureName = "usp_UpdateGameForInstallationfromXML";
                        break;
                    case "CLOSEINSTALLATION":
                        ProcedureName = "usp_CloseInstallationFromXML";
                        break;
                    case "MACHINECLASS":
                        param = new SqlParameter[2];
                        param[0] = new SqlParameter("doc", XMLString);
                        SqlParameter param1 = new SqlParameter();
                        param1.ParameterName = "IsSuccess";
                        param1.Direction = ParameterDirection.Output;
                        param1.SqlDbType = SqlDbType.Int;
                        param1.Value = 0;
                        param[1] = param1;
                        SqlConnection objcon = new SqlConnection(GetConnectionString());
                        using (objcon)
                        {
                            objcon.Open();
                            SqlCommand cmd = new SqlCommand("usp_UpdateNewMachineClass", objcon);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddRange(param);
                            cmd.ExecuteNonQuery();
                            objcon.Close();
                        }
                        //SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "usp_UpdateNewMachineClass", param);
                        if (int.Parse((param[1].Value.ToString())) > 0)
                            return true;
                        else
                            return false;
                    case "MACHINE":
                        param = new SqlParameter[1];
                        param[0] = new SqlParameter("doc", XMLString);
                        SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "usp_updateMachine", param);
                        LogManager.WriteLog("Machine updated", LogManager.enumLogLevel.Info);
                        return true;
                    case "AAMSVERIFY":
                        param = new SqlParameter[2];
                        param[0] = new SqlParameter("SiteCode", XMLString);
                        param[1] = new SqlParameter("IsVerified", 0);
                        param[1].Direction = ParameterDirection.Output;
                        SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "rsp_verifySiteAAMSStatus", param);
                        if (int.Parse((param[1].Value.ToString())) > 0)
                            return true;
                        else
                            return false;

                    case "INSTALLATIONSTATUSUPDATE":
                        param = new SqlParameter[1];
                        param[0] = new SqlParameter("doc", XMLString);
                        SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "usp_updateInstallationStatusFromXML", param);
                        LogManager.WriteLog("Installation Status updated", LogManager.enumLogLevel.Info);
                        return true;
                    case "CHANGEPASSWORD":
                        param = new SqlParameter[1];
                        param[0] = new SqlParameter("doc", XMLString);
                        SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "usp_UpdateUserFromXML", param);
                        LogManager.WriteLog("Updated password & re exported to exchanges", LogManager.enumLogLevel.Info);
                        return true;
                    case "VLTVERIFICATION":
                        param = new SqlParameter[1];
                        param[0] = new SqlParameter("doc", XMLString);
                        SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "usp_UpdateVLTVerificationStatus", param);
                        LogManager.WriteLog("Updated VLT Verification Status", LogManager.enumLogLevel.Info);
                        return true;
                    case "AAMSCONFIG":
                        param = new SqlParameter[1];
                        param[0] = new SqlParameter("doc", XMLString);
                        SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "usp_UpdateBarPositionMachineEnabledStatus", param);
                        LogManager.WriteLog("Updated Bar Position Machine Enabled Status", LogManager.enumLogLevel.Info);
                        return true;
                    case "MACHINEMAINTENANCE":
                        param = new SqlParameter[1];
                        param[0] = new SqlParameter("doc", XMLString);
                        SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "usp_UpdateMachineMaintenanceStatus ", param);
                        LogManager.WriteLog("Updated Machine Maintenance Status", LogManager.enumLogLevel.Info);
                        return true;
                    case "MAINTENANCESESSION":
                        param = new SqlParameter[1];
                        param[0] = new SqlParameter("doc", XMLString);
                        SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "usp_ImportMaintenanceSession", param);
                        LogManager.WriteLog("Updated MaintenanceSession", LogManager.enumLogLevel.Info);
                        return true;
                    case "MAINTENANCEHISTORY":
                        param = new SqlParameter[1];
                        param[0] = new SqlParameter("doc", XMLString);
                        SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "usp_ImportMaintenanceHistory", param);
                        LogManager.WriteLog("Updated MaintenanceHistory", LogManager.enumLogLevel.Info);
                        return true;
                    case "MAINTENANCEREASONCATEGORY":
                        param = new SqlParameter[1];
                        param[0] = new SqlParameter("doc", XMLString);
                        SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "usp_ImportMaintenanceReasonCategory", param);
                        LogManager.WriteLog("Updated Maintenance Reason and Category", LogManager.enumLogLevel.Info);
                        return true;
                    case "AUDIT":
                        param = new SqlParameter[1];
                        param[0] = new SqlParameter("doc", XMLString);
                        SqlHelper.ExecuteNonQuery(Common.Utilities.DatabaseHelper.GetAuditConnectionString(), CommandType.StoredProcedure, "usp_ImportAuditHistory", param);
                        LogManager.WriteLog("Updated Audit", LogManager.enumLogLevel.Info);
                        return true;
                    case "AFTAUDIT":
                        param = new SqlParameter[1];
                        param[0] = new SqlParameter("doc", XMLString);
                        SqlHelper.ExecuteNonQuery(Common.Utilities.DatabaseHelper.GetAuditConnectionString(), CommandType.StoredProcedure, "usp_ImportAFTAuditHistory", param);
                        LogManager.WriteLog("Updated AFT Audit", LogManager.enumLogLevel.Info);
                        return true;
                    case "AFTTRANSACTION":
                        param = new SqlParameter[1];
                        LogManager.WriteLog("Inside importing AFT transactions", LogManager.enumLogLevel.Info);
                        param[0] = new SqlParameter("doc", XMLString);
                        //string connectionstring = "server=ws-in327;uid=sa;pwd=sa123;database=Enterprise;connect timeout=60;";
                        SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure,
                            "usp_ImportAFTTransaction", param);
                        LogManager.WriteLog("Updated AFT Transactions", LogManager.enumLogLevel.Info);
                        return true;

                    case "COMPONENTDETAILS":
                        param = new SqlParameter[2];
                        param[0] = new SqlParameter("doc", XMLString);
                        param[1] = new SqlParameter("IsSuccess", 0);
                        param[1].Direction = ParameterDirection.Output;
                        SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "usp_InsertCompDetailsfromXML", param);
                        if (Convert.ToInt32(param[1].Value) == 0)
                        {
                            LogManager.WriteLog("COMPONENTDETAILS Update - Completed.", LogManager.enumLogLevel.Info);
                            return true;
                        }
                        else
                        {
                            LogManager.WriteLog("COMPONENTDETAILS Update - Failed.", LogManager.enumLogLevel.Info);
                            return false;
                        }
                    case "MACHINECOMPONENTDETAILS":
                        param = new SqlParameter[2];
                        param[0] = new SqlParameter("doc", XMLString);
                        param[1] = new SqlParameter("IsSuccess", 0);
                        param[1].Direction = ParameterDirection.Output;
                        SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "usp_InsertMachineCompDetailsfromXML", param);
                        if (Convert.ToInt32(param[1].Value) == 0)
                        {
                            LogManager.WriteLog("MACHINECOMPONENTDETAILS Update - Completed.", LogManager.enumLogLevel.Info);
                            return true;
                        }
                        else
                        {
                            LogManager.WriteLog("MACHINECOMPONENTDETAILS Update - Failed.", LogManager.enumLogLevel.Info);
                            return false;
                        }
                    case "COMPVERIFICATIONRECORD":
                        param = new SqlParameter[2];
                        param[0] = new SqlParameter("doc", XMLString);
                        param[1] = new SqlParameter("IsSuccess", 0);
                        param[1].Direction = ParameterDirection.Output;
                        SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "usp_InsertCompVerificationDetailsfromXML", param);
                        if (Convert.ToInt32(param[1].Value) == 0)
                        {
                            LogManager.WriteLog("COMPVERIFICATIONRECORD Update - Completed.", LogManager.enumLogLevel.Info);
                            return true;
                        }
                        else
                        {
                            LogManager.WriteLog("COMPVERIFICATIONRECORD Update - Failed.", LogManager.enumLogLevel.Info);
                            return false;
                        }
                    case "AUTHENTICATECOMPONENT":
                        param = new SqlParameter[2];
                        param[0] = new SqlParameter("SerialNo", XMLString);
                        param[1] = new SqlParameter("IsSuccess", 0);
                        param[1].Direction = ParameterDirection.Output;
                        SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "usp_InsertAuthenticateComponentDetailsfromXML", param);
                        if (Convert.ToInt32(param[1].Value) == 0)
                        {
                            LogManager.WriteLog("AUTHENTICATECOMPONENT Update - Completed.", LogManager.enumLogLevel.Info);
                            return true;
                        }
                        else
                        {
                            LogManager.WriteLog("AUTHENTICATECOMPONENT Update - Failed.", LogManager.enumLogLevel.Info);
                            return false;
                        }
                    case "COMPONENTCOUNT":
                        param = new SqlParameter[2];
                        param[0] = new SqlParameter("doc", XMLString);
                        param[1] = new SqlParameter("IsSuccess", 0);
                        param[1].Direction = ParameterDirection.Output;
                        SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "usp_InsertCompCountDetailsfromXML", param);
                        if (Convert.ToInt32(param[1].Value) == 0)
                        {
                            LogManager.WriteLog("COMPONENTCOUNT Update - Completed.", LogManager.enumLogLevel.Info);
                            return true;
                        }
                        else
                        {
                            LogManager.WriteLog("COMPONENTCOUNT Update - Failed.", LogManager.enumLogLevel.Info);
                            return false;
                        }
                    default:
                        break;
                }
                if (UpdationType.ToUpper() != "MACHINECLASS")
                {
                    DataBaseServiceHandler.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, ProcedureName,
                        DataBaseServiceHandler.AddParameter<string>("doc", DbType.Xml, XMLString));
                }
                return true;
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                return false;
            }
        }

        [WebMethod, SoapHeader("AuthenticationInfo")]
        [SoapSecurityExtension(Encrypt = EncryptMode.Response)]
        public int UpdateDenomChange(string XMLString)
        {
            string ProcedureName = string.Empty;
            //int InstallID=0;
            CheckSecurity();
            try
            {
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@doc", XMLString);
                SqlParameter param1 = new SqlParameter();
                param1.ParameterName = "Installation_ID";
                param1.Direction = ParameterDirection.Output;
                param1.SqlDbType = SqlDbType.Int;
                param1.Value = 0;
                param[1] = param1;
                //param[1].Direction = ParameterDirection.Output;
                SqlConnection objcon = new SqlConnection(GetConnectionString());
                using (objcon)
                {
                    objcon.Open();
                    SqlCommand cmd = new SqlCommand("usp_UpdateInstallationDenomChange", objcon);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddRange(param);
                    cmd.ExecuteNonQuery();
                    objcon.Close();
                }

                //SqlParameter[] param = new SqlParameter[2];
                //param[0] = new SqlParameter("@doc", XMLString);
                //param[1] = new SqlParameter("@Installation_ID", InstallID);
                //param[1].Direction = ParameterDirection.Output;
                //SqlHelper.ExecuteNonQuery(GetConnectionString(), "usp_UpdateInstallationDenomChange", param);
                //LogManager.WriteLog(param[1].Value.ToString(), LogManager.enumLogLevel.Info);

                return Convert.ToInt32(param[1].Value);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return 0;
            }
        }

        [WebMethod, SoapHeader("AuthenticationInfo")]
        [SoapSecurityExtension(Encrypt = EncryptMode.Response)]
        public bool ImportPasswordChange(string sXMLString, int nUserID, string sSiteCode, string sType)
        {
            bool bSuccess = false;
            var oEventParam = new SqlParameter[4];
            const string strSpName = "esp_Import_SiteXML";

            try
            {
                var objSqlParam = new SqlParameter
                {
                    ParameterName = "Site_Code",
                    Value = sSiteCode,
                    Direction = ParameterDirection.Input
                };
                oEventParam[0] = objSqlParam;

                objSqlParam = new SqlParameter
                {
                    ParameterName = "Type",
                    Value = sType,
                    Direction = ParameterDirection.Input
                };
                oEventParam[1] = objSqlParam;

                objSqlParam = new SqlParameter
                {
                    ParameterName = "SiteXML",
                    Value = sXMLString,
                    SqlDbType = SqlDbType.Text,
                    Direction = ParameterDirection.Input
                };
                oEventParam[2] = objSqlParam;

                objSqlParam = new SqlParameter
                {
                    ParameterName = "EH_ID",
                    Value = nUserID,
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Input
                };
                oEventParam[3] = objSqlParam;

                int iRecordInserted = SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure,
                                                                strSpName, oEventParam);
                if (iRecordInserted > 0)
                    bSuccess = true;
                LogManager.WriteLog("ImportPasswordChange WebMethod" + " Success: " + bSuccess, LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                bSuccess = false;
            }
            return bSuccess;
        }

        [WebMethod, SoapHeader("AuthenticationInfo")]
        [SoapSecurityExtension(Encrypt = EncryptMode.Response)]
        public DataTable GetActiveSiteDetails()
        {
            CheckSecurity();

            try
            {
                return SqlHelper.ExecuteDataset(GetConnectionString(), CommandType.StoredProcedure, "RSP_GETACTIVESITEDETAILS", null).Tables[0];
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return new DataTable();
            }
        }
        [WebMethod, SoapHeader("AuthenticationInfo")]
        [SoapSecurityExtension(Encrypt = EncryptMode.Response)]
        public DataTable GetActiveSiteDetailsForUser(int iSecurityUserID)
        {
            CheckSecurity();

            try
            {
                SqlParameter[] oParams = new SqlParameter[1];
                oParams[0] = new SqlParameter("@SecurityUserID", iSecurityUserID);
                return SqlHelper.ExecuteDataset(GetConnectionString(), CommandType.StoredProcedure, "rsp_GetActiveSiteDetailsforuser", oParams).Tables[0];
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return new DataTable();
            }
        }


        [WebMethod, SoapHeader("AuthenticationInfo")]
        [SoapSecurityExtension(Encrypt = EncryptMode.Response)]
        public DataTable GetInTransitAssetforSite(string siteCode)
        {
            CheckSecurity();

            var ds = new DataSet();
            try
            {
                var oEventParam = new SqlParameter[1];

                var sqlParameterAsset = new SqlParameter
                {
                    ParameterName = "@Site_Code",
                    Value = siteCode,
                    Direction = ParameterDirection.Input
                };
                oEventParam[0] = sqlParameterAsset;

                SqlHelper.FillDataset(GetConnectionString(), CommandType.StoredProcedure, "rsp_GetInTransitAssetforSite", ds,
                                      null, oEventParam);

                ds.Tables[0].TableName = "InTransitAsset";
                ds.DataSetName = "InTransitAsset";

                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Failure call " + " GetInTransitAssetforSite ", LogManager.enumLogLevel.Info);
                ExceptionManager.Publish(ex);
                return null;
            }
        }

        [WebMethod, SoapHeader("AuthenticationInfo")]
        [SoapSecurityExtension(Encrypt = EncryptMode.Response)]
        public bool ImportInstallationGameData(string xmlString)
        {
            //Check security
            CheckSecurity();

            LogManager.WriteLog("ImportInstallationGameData Called...", LogManager.enumLogLevel.Info);

            try
            {
                LogManager.WriteLog(xmlString, LogManager.enumLogLevel.Info);

                DataBaseServiceHandler.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "rsp_ImportInstallationGameDataFromXML",
                    DataBaseServiceHandler.AddParameter<string>("doc", DbType.String, xmlString));

                LogManager.WriteLog("Game Data details stored", LogManager.enumLogLevel.Info);

                return true;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return false;
            }
        }

        [WebMethod, SoapHeader("AuthenticationInfo")]
        [SoapSecurityExtension(Encrypt = EncryptMode.Response)]
        public bool UpdateGMUNumber(string xmlString)
        {
            bool retVal = false;
            //Check security
            CheckSecurity();

            LogManager.WriteLog("UpdateGMUNumber Called...", LogManager.enumLogLevel.Info);

            try
            {
                LogManager.WriteLog(xmlString, LogManager.enumLogLevel.Info);

                retVal = (DataBaseServiceHandler.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "rsp_UpdateAGSDetailsFromXML",
                      DataBaseServiceHandler.AddParameter<string>("doc", DbType.String, xmlString),
                      DataBaseServiceHandler.AddParameter<bool>("IsSuccess", DbType.Boolean, retVal, ParameterDirection.Output)) > 0);

                LogManager.WriteLog("GMU Number Updated Successfully", LogManager.enumLogLevel.Info);

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);

            }
            return retVal;
        }

        [WebMethod, SoapHeader("AuthenticationInfo")]
        [SoapSecurityExtension(Encrypt = EncryptMode.Response)]
        public DateTime GetSystemLocalDateTime()
        {
            //Check security
            CheckSecurity();

            LogManager.WriteLog("GetSystemLocalDateTime Called...", LogManager.enumLogLevel.Info);

            try
            {
                return DateTime.Now;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return DateTime.Now;
            }
        }


        //[WebMethod, SoapHeader("AuthenticationInfo")]
        //[SoapSecurityExtension(Encrypt = EncryptMode.Response)]
        [WebMethod()]
        public int CheckLicenseKey(string sLicenseKey, string sSitecode, string sUserName)
        {
            //CheckSecurity();

            int SuccessCode = 0;

            LogManager.WriteLog("CheckLicenseKey Called...", LogManager.enumLogLevel.Info);

            string strStoredProcedureName = string.Empty;

            try
            {
                SqlParameter[] oSQLParams = new SqlParameter[4];
                SqlParameter objSQLParam = new SqlParameter();
                objSQLParam.ParameterName = "@LicenseKey";
                objSQLParam.Value = sLicenseKey;
                objSQLParam.Direction = ParameterDirection.Input;
                oSQLParams[0] = objSQLParam;

                objSQLParam = new SqlParameter();
                objSQLParam.ParameterName = "@SiteCode";
                objSQLParam.Value = sSitecode;
                objSQLParam.Direction = ParameterDirection.Input;
                oSQLParams[1] = objSQLParam;

                objSQLParam = new SqlParameter();
                objSQLParam.ParameterName = "@UserName";
                objSQLParam.Value = sUserName;
                objSQLParam.Direction = ParameterDirection.Input;
                oSQLParams[2] = objSQLParam;

                objSQLParam = new SqlParameter();
                objSQLParam.ParameterName = "@Result";
                objSQLParam.SqlDbType = SqlDbType.Int;
                objSQLParam.Direction = ParameterDirection.Output;
                oSQLParams[3] = objSQLParam;

                strStoredProcedureName = "rsp_SL_CheckLicenseKey";
                SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, strStoredProcedureName, oSQLParams);
                SuccessCode = Convert.ToInt32(oSQLParams[3].Value);

                LogManager.WriteLog("ChecklicenseKey Executed Successfully: key returns " + SuccessCode.ToString(), LogManager.enumLogLevel.Info);

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return SuccessCode;
            }

            return SuccessCode;
        }


        [WebMethod, SoapHeader("AuthenticationInfo")]
        [SoapSecurityExtension(Encrypt = EncryptMode.Response)]
        public bool ImportEmployeeCardSessionData(string xmlString)
        {
            //Check security
            CheckSecurity();

            LogManager.WriteLog("ImportEmployeeCardSessionData Called...", LogManager.enumLogLevel.Info);

            try
            {
                LogManager.WriteLog(xmlString, LogManager.enumLogLevel.Info);

                DataBaseServiceHandler.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "usp_ImportEmployeeCardSessionData",
                    DataBaseServiceHandler.AddParameter<string>("doc", DbType.String, xmlString));

                LogManager.WriteLog("Employee Card session details stored", LogManager.enumLogLevel.Info);

                return true;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return false;
            }
        }


        [WebMethod, SoapHeader("AuthenticationInfo")]
        [SoapSecurityExtension(Encrypt = EncryptMode.Response)]
        public int UpdateLicenseActivation(string sLicenseKey, string sSitecode, string sUserName, DateTime dtActivatedDateTime)
        {
            //CheckSecurity();

            int SuccessCode = 0;

            LogManager.WriteLog("UpdateLicenseActivation Called...", LogManager.enumLogLevel.Info);

            string strStoredProcedureName = string.Empty;

            try
            {
                SqlParameter[] oSQLParams = new SqlParameter[5];
                SqlParameter objSQLParam = new SqlParameter();
                objSQLParam.ParameterName = "@LicenseKey";
                objSQLParam.Value = sLicenseKey;
                objSQLParam.Direction = ParameterDirection.Input;
                oSQLParams[0] = objSQLParam;

                objSQLParam = new SqlParameter();
                objSQLParam.ParameterName = "@SiteCode";
                objSQLParam.Value = sSitecode;
                objSQLParam.Direction = ParameterDirection.Input;
                oSQLParams[1] = objSQLParam;

                objSQLParam = new SqlParameter();
                objSQLParam.ParameterName = "@UserName";
                objSQLParam.Value = sUserName;
                objSQLParam.Direction = ParameterDirection.Input;
                oSQLParams[2] = objSQLParam;

                objSQLParam = new SqlParameter();
                objSQLParam.ParameterName = "@ActivatedDateTime";
                objSQLParam.Value = dtActivatedDateTime;
                objSQLParam.Direction = ParameterDirection.Input;
                oSQLParams[3] = objSQLParam;

                objSQLParam = new SqlParameter();
                objSQLParam.ParameterName = "@Result";
                objSQLParam.SqlDbType = SqlDbType.Int;
                objSQLParam.Direction = ParameterDirection.Output;
                oSQLParams[4] = objSQLParam;

                strStoredProcedureName = "usp_SL_UpdateLicenseActivation";
                SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, strStoredProcedureName, oSQLParams);
                SuccessCode = Convert.ToInt32(oSQLParams[4].Value);

                LogManager.WriteLog("UpdateLicenseActivation Executed Successfully: key returns " + SuccessCode.ToString(), LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return SuccessCode;
        }

        [WebMethod, SoapHeader("AuthenticationInfo")]
        [SoapSecurityExtension(Encrypt = EncryptMode.Response)]
        public bool UpdateSiteLiquidationID(int iHQLiquidationID, int iSiteLiquidationID)
        {
            CheckSecurity();
            LogManager.WriteLog("UpdateSiteLiquidationID Called... iHQLiquidationID: " + iHQLiquidationID.ToString() + "iLiquidationID: " + iSiteLiquidationID.ToString(), LogManager.enumLogLevel.Info);
            try
            {
                var sqlParameters = new SqlParameter[2];

                var sqlParameter = new SqlParameter
                {
                    ParameterName = "@HQLiquidationID",
                    Value = iHQLiquidationID,
                    Direction = ParameterDirection.Input
                };
                sqlParameters[0] = sqlParameter;

                sqlParameter = new SqlParameter
                {
                    ParameterName = "@LiquidationID",
                    Value = iSiteLiquidationID,
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Input
                };
                sqlParameters[1] = sqlParameter;

                int retValue = DataBaseServiceHandler.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "usp_UpdateSiteLiquidationDetail", sqlParameters);
                if (retValue >= 0)
                {
                    LogManager.WriteLog("Updated HQLiquidationID Successfully...", LogManager.enumLogLevel.Info);
                    return true;
                }
                else
                {
                    LogManager.WriteLog("Unable to update HQLiquidationID", LogManager.enumLogLevel.Info);
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog(ex.Message, LogManager.enumLogLevel.Error);
                return false;
            }
        }

        [WebMethod, SoapHeader("AuthenticationInfo")]
        [SoapSecurityExtension(Encrypt = EncryptMode.Response)]
        public bool UpdateSiteLiquidationShareID(int iHQLiquidationShareID, int iSiteLiquidationShareID)
        {
            CheckSecurity();
            LogManager.WriteLog("UpdateHQLiquidationShareID Called... iHQLiquidationShareID" + iHQLiquidationShareID.ToString() + " iLiquidationShareID: " + iSiteLiquidationShareID.ToString(), LogManager.enumLogLevel.Info);
            try
            {
                var sqlParameters = new SqlParameter[2];

                var sqlParameter = new SqlParameter
                {
                    ParameterName = "@HQLiquidationShareID",
                    Value = iHQLiquidationShareID,
                    Direction = ParameterDirection.Input
                };
                sqlParameters[0] = sqlParameter;

                sqlParameter = new SqlParameter
                {
                    ParameterName = "@LiquidationShareID",
                    Value = iSiteLiquidationShareID,
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Input
                };
                sqlParameters[1] = sqlParameter;

                int retValue = DataBaseServiceHandler.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "usp_UpdateSiteLiquidationShareDetail", sqlParameters);
                if (retValue >= 0)
                {
                    LogManager.WriteLog("Updated HQLiquidationShareID Successfully...", LogManager.enumLogLevel.Info);
                    return true;
                }
                else
                {
                    LogManager.WriteLog("Unable to update HQLiquidationShareID", LogManager.enumLogLevel.Info);
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog(ex.Message, LogManager.enumLogLevel.Error);
                return false;
            }
        }

        [WebMethod, SoapHeader("AuthenticationInfo")]
        [SoapSecurityExtension(Encrypt = EncryptMode.Response)]
        public bool ImportAlertDetails(string SiteCode, string XMLData)
        {
            bool bResult = false;
            try
            {
                LogManager.WriteLog(string.Format("ImportAlertDetails->Executed : SiteCode: {0}:  XMLData: {1} ", SiteCode, XMLData), LogManager.enumLogLevel.Info);

                SqlParameter[] oSQLParams = new SqlParameter[3];

                SqlParameter objSQLParam = new SqlParameter();
                objSQLParam.ParameterName = "@SiteCode";
                objSQLParam.Value = SiteCode;
                objSQLParam.Direction = ParameterDirection.Input;
                oSQLParams[0] = objSQLParam;

                objSQLParam = new SqlParameter();
                objSQLParam.ParameterName = "@doc";
                objSQLParam.Value = XMLData;
                objSQLParam.Direction = ParameterDirection.Input;
                oSQLParams[1] = objSQLParam;

                objSQLParam = new SqlParameter();
                objSQLParam.ParameterName = "@IsSuccess";
                objSQLParam.Value = 0;
                objSQLParam.Direction = ParameterDirection.Output;
                oSQLParams[2] = objSQLParam;


                SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "usp_ImportAlertDetails", oSQLParams);

                if (Convert.ToBoolean(oSQLParams[2].Value))
                    bResult = true;

                LogManager.WriteLog(string.Format("ImportAlertDetails->Response : " + bResult.ToString()), LogManager.enumLogLevel.Info);
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
            finally
            {
                LogManager.WriteLog("ImportAlertDetails Execution complete", LogManager.enumLogLevel.Info);
            }
            return bResult;
        }

        [WebMethod, SoapHeader("AuthenticationInfo")]
        [SoapSecurityExtension(Encrypt = EncryptMode.Response)]
        public bool ImportEmailAlertStatusDetails(string XMLData)
        {
            bool bResult = false;
            try
            {
                LogManager.WriteLog(string.Format("ImportEmailAlertStatusDetails->Executed  XMLData: {0} ",  XMLData), LogManager.enumLogLevel.Info);

                SqlParameter[] oSQLParams = new SqlParameter[2];

                SqlParameter objSQLParam = new SqlParameter();
      
                objSQLParam = new SqlParameter();
                objSQLParam.ParameterName = "@doc";
                objSQLParam.Value = XMLData;
                objSQLParam.Direction = ParameterDirection.Input;
                oSQLParams[0] = objSQLParam;

                objSQLParam = new SqlParameter();
                objSQLParam.ParameterName = "@IsSuccess";
                objSQLParam.Value = 0;
                objSQLParam.Direction = ParameterDirection.Output;
                oSQLParams[1] = objSQLParam;


                SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "usp_ImportEmailAlertAuditDetails", oSQLParams);

                if (Convert.ToBoolean(oSQLParams[1].Value))
                    bResult = true;

                LogManager.WriteLog(string.Format("ImportEmailAlertStatusDetails->Response : " + bResult.ToString()), LogManager.enumLogLevel.Info);
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
            finally
            {
                LogManager.WriteLog("ImportAlertDetails Execution complete", LogManager.enumLogLevel.Info);
            }
            return bResult;
        }



        [WebMethod]
        public string GetCommonData(string SiteCode,string DataType, string XMLData)
        {
            try
            {


                LogManager.WriteLog(string.Format("GetCommonData->Executed : SiteCode: {0} DataType: {1} XMLData: {2} ", SiteCode, DataType, XMLData), LogManager.enumLogLevel.Info);
                
                SqlParameter[] oSQLParams = new SqlParameter[3];

                SqlParameter objSQLParam = new SqlParameter();
                objSQLParam.ParameterName = "@SiteCode";
                objSQLParam.Value = SiteCode;
                objSQLParam.Direction = ParameterDirection.Input;
                oSQLParams[0] = objSQLParam;

                objSQLParam = new SqlParameter();
                objSQLParam.ParameterName = "@DataType";
                objSQLParam.Value = DataType;
                objSQLParam.Direction = ParameterDirection.Input;
                oSQLParams[1] = objSQLParam;

                objSQLParam = new SqlParameter();
                objSQLParam.ParameterName = "@XMLData";
                objSQLParam.Value = XMLData;
                objSQLParam.Direction = ParameterDirection.Input;
                oSQLParams[2] = objSQLParam;

                string strResponse = SqlHelper.ExecuteScalar(GetConnectionString(), CommandType.StoredProcedure, "rsp_GetCommonData", oSQLParams).ToString();
                LogManager.WriteLog(string.Format("GetCommonData->Response : " + strResponse) , LogManager.enumLogLevel.Info);
                return strResponse;

            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                return string.Empty;
            }
            finally
            {
                LogManager.WriteLog("GetCommonData Execution complete", LogManager.enumLogLevel.Info);
            }

        }
    }
}
