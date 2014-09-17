using System;
using System.Data;
using System.Data.SqlClient;
using System.Xml;
using BMC.Common;
using BMC.Common.ConfigurationManagement;
using BMC.Common.ExceptionManagement;
using BMC.Common.LogManagement;
using BMC.DataAccess;
using Microsoft.Win32;

namespace BMC.BusinessClasses.BusinessLogic
{
    internal class DataHelper
    {
        private static Proxy.Proxy _proxy;
       
        #region STM
        public static DataTable GetRecordsToExportForSTM()
        {
            SqlConnection connection = null;
            var dataset = new DataSet();

            try
            {
                connection = GetSqlConnection();
                if (connection != null)
                {
                    dataset = SqlHelper.ExecuteDataset(connection,
                                                       CommandType.StoredProcedure,
                                                       Constants.CONSTANT_RSP_EXPORTSTM);
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



        public static DataTable GetAllSiteDetails()
        {
            SqlConnection connection = GetSqlConnection();
            try
            {
                var siteDetails = SqlHelper.ExecuteDataset(connection, CommandType.Text,
                                                               "Select Site_Name, WebURL, Last_Updated_Time from site");
                return siteDetails.Tables[0];
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return new DataTable();
            }
            finally
            {
                if (connection != null && connection.State != ConnectionState.Closed)
                    connection.Close();
            }
        }

        /// <summary>
        /// Function to Call the SPROC-USP_GetUnprocessedRecordsFromImportHistory
        /// This would select the list of Rows that are not Processed 
        /// from the IMPORT HISTORY
        /// </summary>
        /// <returns>Returns DataTable containing rows from IMPORT HISTORY Table whose IH_Status is 0</returns>
        public static DataTable GetRecordsToBeImported(String strSideCode)
        {
            SqlConnection connection = null;
            var dataset = new DataSet();

            var oSiteParam = new SqlParameter[1];
            var oParam = new SqlParameter
                             {
                                 ParameterName = "SITE_CODE",
                                 Value = strSideCode,
                                 SqlDbType = SqlDbType.VarChar,
                                 Direction = ParameterDirection.Input
                             };
            oSiteParam[0] = oParam;
            try
            {
                connection = GetSqlConnection();
                if (connection != null)
                {
                    dataset = SqlHelper.ExecuteDataset(connection,
                                                       CommandType.StoredProcedure,
                                                       Constants.CONSTANT_USP_GETUNPROCESSEDRECORSFROMIH,
                                                       oSiteParam);
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

            return dataset.Tables.Count > 0 ? dataset.Tables[0] : new DataTable();
        }

        /// <summary>
        /// Function to Call the SPROC-USP_GetUnprocessedRecordsFromImportHistory
        /// This would select the list of Rows that are not Processed 
        /// from the IMPORT HISTORY
        /// </summary>
        /// <returns>Returns DataTable containing rows from IMPORT HISTORY Table whose IH_Status is 0</returns>
        public static DataTable GetRecordsToBeImportedForSites(String strSideCode,int iPreviousIHID,int iLimitRecordCount)
        {
            SqlConnection connection = null;
            var dataset = new DataSet();

            var oSiteParam = new SqlParameter[3];
            var oParam = new SqlParameter
            {
                ParameterName = "SITE_LIST",
                Value = strSideCode,
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                Size = 8000
            };
            var oParam1 = new SqlParameter
            {
                ParameterName = "Previous_IH_ID",
                Value = iPreviousIHID,
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Input,
                Size = 8000
            };
            var oParam2 = new SqlParameter
            {
                ParameterName = "RecordsCount",
                Value = iLimitRecordCount,
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Input,
                Size = 8000
            };
            oSiteParam[0] = oParam;
            oSiteParam[1] = oParam1;
            oSiteParam[2] = oParam2;

            try
            {
                connection = GetSqlConnection();
                if (connection != null)
                {
                    dataset = SqlHelper.ExecuteDataset(connection,
                                                       CommandType.StoredProcedure,
                                                       Constants.CONSTANT_USP_GETUNPROCESSEDRECORSFROMIH_FORSITES,
                                                       oSiteParam);
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

            return dataset.Tables.Count > 0 ? dataset.Tables[0] : new DataTable();
        }


        /// <summary>        
        /// Get list of all Sites in Enterprise
        /// </summary>
        /// <returns>Returns DataTable containing rows from SITE TABLE</returns>
        public static DataTable GetSiteList()
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

        public static bool LogSiteEvent(string sEventXML)
        {
            LogManager.WriteLog("LogSiteEvent Called...", LogManager.enumLogLevel.Info);

            var xmlDocument = new XmlDocument();

            var strSuccesstrEventIDs = string.Empty;
            var failureEventIDs = string.Empty;

            bool @event = false;
            try
            {
                xmlDocument.LoadXml(sEventXML);
                XmlNodeList oEventIDNodeList = xmlDocument.DocumentElement.GetElementsByTagName("Event");

                foreach (XmlNode oXMLEventNode in oEventIDNodeList)
                {
                    string eventID = oXMLEventNode.ChildNodes[0].InnerXml;
                    string installationNo = oXMLEventNode.ChildNodes[1].InnerXml;
                    string faultSource = oXMLEventNode.ChildNodes[2].InnerXml;
                    string faultType = oXMLEventNode.ChildNodes[3].InnerXml;
                    string faultDateTime = oXMLEventNode.ChildNodes[4].InnerXml;
                    string siteName = oXMLEventNode.ChildNodes[5].InnerXml;
                    string faultDetails = oXMLEventNode.ChildNodes[6].InnerXml;
                    string eventAutoClosed = oXMLEventNode.ChildNodes[8].InnerXml;
                    string CardNumber = oXMLEventNode.ChildNodes[9].InnerXml;                    
                    bool isCardInserted = Convert.ToBoolean(Convert.ToInt16(oXMLEventNode.ChildNodes[10].InnerXml));
                    string ErrorCodeNumber = oXMLEventNode.ChildNodes[11].InnerText;
                    



                    var oEventParam = new SqlParameter[11];


                    var parameter = new SqlParameter
                                                 {
                                                     ParameterName = "InstallationID",
                                                     Value = installationNo,
                                                     SqlDbType = SqlDbType.Int,
                                                     Direction = ParameterDirection.Input
                                                 };
                    oEventParam[0] = parameter;
                  
                    parameter = new SqlParameter
                                    {
                                        ParameterName = "Site_Name",
                                        Value = siteName,
                                        Direction = ParameterDirection.Input
                                    };
                    oEventParam[1] = parameter;
                  
                    parameter = new SqlParameter
                                    {
                                        ParameterName = "Fault_Source_ID",
                                        Value = faultSource,
                                        Direction = ParameterDirection.Input
                                    };
                    oEventParam[2] = parameter;
                  
                    parameter = new SqlParameter
                                    {
                                        ParameterName = "Fault_Type_ID",
                                        Value = faultType,
                                        Direction = ParameterDirection.Input
                                    };
                    oEventParam[3] = parameter;
                    parameter = new SqlParameter
                                    {
                                        ParameterName = "@Fault_Details",
                                        Value = faultDetails,
                                        Direction = ParameterDirection.Input
                                    };
                    oEventParam[4] = parameter;
                    parameter = new SqlParameter
                                    {
                                        ParameterName = "DateTime",
                                        Value = faultDateTime,
                                        Direction = ParameterDirection.Input
                                    };
                    oEventParam[5] = parameter;
                    parameter = new SqlParameter
                    {
                        ParameterName = "@Event_Auto_Closed",
                        Value = eventAutoClosed,
                        Direction = ParameterDirection.Input
                    };
                    oEventParam[6] = parameter;
                   
                    parameter = new SqlParameter
                    {
                        ParameterName = "@CardNumber",
                        Value = CardNumber,
                        Direction = ParameterDirection.Input
                    };
                    oEventParam[7] = parameter;
                  
                    parameter = new SqlParameter
                    {
                        ParameterName = "@isCardInserted",
                        Value = isCardInserted,                     
                        Direction = ParameterDirection.Input
                    };
                    oEventParam[8] = parameter;
                    parameter = new SqlParameter
                    {
                        ParameterName = "@ErrorCodeNumber",
                        Value = ErrorCodeNumber,
                        Direction = ParameterDirection.Input
                    };
                    oEventParam[9] = parameter; 
                    parameter = new SqlParameter("@RETURN_VALUE", SqlDbType.Int) { Direction = ParameterDirection.ReturnValue };
                    oEventParam[10] = parameter;                    
                    SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure,
                                              "usp_LogSiteEvent",
                                              oEventParam);
                    if (int.Parse(oEventParam[10].Value.ToString()) == 0)
                        @event = true;

                    if (@event)
                    {
                        strSuccesstrEventIDs = strSuccesstrEventIDs + eventID + ",";
                        LogManager.WriteLog("Successful call " + strSuccesstrEventIDs, LogManager.enumLogLevel.Info);
                    }
                    else
                    {
                        failureEventIDs = failureEventIDs + eventID + ",";
                        LogManager.WriteLog("Failure call " + strSuccesstrEventIDs, LogManager.enumLogLevel.Info);
                    }
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                throw;
            }
            return @event;
        }

        private static SqlConnection GetSqlConnection()
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

        private static string GetConnectionString()
        {
            //bool bUseHex = true;
            //RegistryKey registryKey;
            //string connectionString = "";
            //ConfigManager.SetConfigurationMode(ConfigManager.ConfigurationMode.AppConfig);
            //try
            //{
            //    registryKey = Registry.LocalMachine.OpenSubKey(ConfigManager.Read("RegistryPath"));
            //    connectionString = registryKey.GetValue("SQLConnect").ToString();

            //    if (!connectionString.ToUpper().Contains("SERVER"))
            //    {
            //        var objBGSConstants = new cConstants();
            //        var objDecrypt = new clsBlowFish();
            //        string key = objBGSConstants.ENCRYPTIONKEY;
            //        connectionString = objDecrypt.DecryptString(ref connectionString, ref key, ref bUseHex);
            //    }
            //    registryKey.Close();
            //}
            //catch (Exception ex)
            //{
            //    ExceptionManager.Publish(ex);
            //}
            //return connectionString;
            return BMC.Common.Utilities.DatabaseHelper.GetConnectionString();
        }


        /// <summary>
        /// Import the hourly statisics data.
        /// </summary>
        /// <param name="hourlyStatisticsXml">The hourly statistics XML.</param>
        /// <returns></returns>
        public static bool ImportHourlyStatisticsData(string hourlyStatisticsXml)
        {
            LogManager.WriteLog("ImportHourlyStatisticsData Called...", LogManager.enumLogLevel.Info);

            var doc = new XmlDocument();
            try
            {
                doc.LoadXml(hourlyStatisticsXml);
                var sqlParameters = new SqlParameter[2];
                sqlParameters[0] = new SqlParameter("doc", doc.OuterXml);
                sqlParameters[1] = new SqlParameter("IsSuccess", SqlDbType.Int) { Direction = ParameterDirection.Output };
                SqlHelper.ExecuteNonQuery(GetConnectionString(),
                                          CommandType.StoredProcedure,
                                          Constants.CONSTANT_USP_IMPORTHOURLYSTATISTICS, sqlParameters);

                if (sqlParameters[1].SqlValue != null)
                {
                    LogManager.WriteLog(
                        "ImportHourlyStatisticsData WS" + "  iSuccessVal value " + sqlParameters[1].SqlValue,
                        LogManager.enumLogLevel.Info);

                    return (int.Parse(sqlParameters[1].SqlValue.ToString()) == 0);
                }
                else
                {
                    LogManager.WriteLog(
                     "Unable to get the ImportHourlyStatisticsData query status.",
                     LogManager.enumLogLevel.Info);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return false;
        }

        public static bool UpdateProcessDetailsForImportHistory(int iID, string sResult, int iStatus)
        {
            bool isCallSuccess;

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
                                        Constants.CONSTANT_USP_IMPORTHISTORYDETAILS,
                                        sqlParameters);
                isCallSuccess = true;
            }
            catch (Exception ex)
            {
                isCallSuccess = false;
                ExceptionManager.Publish(ex);
            }

            return isCallSuccess;
        }

        public static bool InsertRead(string strXML)
        {
            LogManager.WriteLog("InsertRead Called...", LogManager.enumLogLevel.Info);

            bool bSuccess = false;
            var xmlDoc = new XmlDocument();

            xmlDoc.LoadXml(strXML);
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

                    SqlHelper.ExecuteNonQuery(GetConnectionString(),
                                              CommandType.StoredProcedure,
                                              Constants.CONSTANT_USP_INSERTINTOREAD, sqlParameters);

                    if (sqlParameters[1].Value.ToString() == "0")
                        bSuccess = true;
                    LogManager.WriteLog("ImportData WS " + "  Success value " + bSuccess, LogManager.enumLogLevel.Info);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                throw;
            }

            return bSuccess;
        }

        public static bool ImportMeterHistory(string strXML)
        {
            bool bSuccess = false;

            LogManager.WriteLog("ImportMeterHistory Called...", LogManager.enumLogLevel.Info);

            var objXMLDoc = new XmlDocument();

            objXMLDoc.LoadXml(strXML);
            XmlNodeList xmlNodes = objXMLDoc.ChildNodes[0].ChildNodes;
            bool isRAMRESET_PrevMeter_Available = false;

            try
            {
                foreach (XmlNode objXmlNode in xmlNodes)
                {                    
                    // modified by A.Vinod Kumar for skipping the empty Current record for RAMRESET
                    string innerXml = objXmlNode.InnerXml;
                    bool isRAMRESET_CurMeter_Available = true;
                    if (!string.IsNullOrEmpty(innerXml)) {
                        int idxRAMRESET = innerXml.IndexOf("<MH_Process>RAMRESET</MH_Process>", StringComparison.InvariantCultureIgnoreCase);                        
                        if (idxRAMRESET != -1) {
                            if (!isRAMRESET_PrevMeter_Available) {
                                isRAMRESET_PrevMeter_Available = (innerXml.IndexOf("<MH_Type>P</MH_Type>", idxRAMRESET, (innerXml.Length - idxRAMRESET), StringComparison.InvariantCultureIgnoreCase) != -1);
                            } else {
                                isRAMRESET_CurMeter_Available = (innerXml.IndexOf("<MH_Type>C</MH_Type>", idxRAMRESET, (innerXml.Length - idxRAMRESET), StringComparison.InvariantCultureIgnoreCase) != -1);
                            }
                        } else {
                            if (isRAMRESET_PrevMeter_Available) isRAMRESET_CurMeter_Available = false;
                        }
                    }

                    // special case for missing current record
                    if (!isRAMRESET_CurMeter_Available) {
                        bSuccess = true;
                        LogManager.WriteLog(
                            "ImportData WS - MeterHistory. " + " Empty current record has been skipped.",
                            LogManager.enumLogLevel.Info);
                        return bSuccess;
                    }

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

                    SqlHelper.ExecuteNonQuery(GetConnectionString(),
                                              CommandType.StoredProcedure,
                                              Constants.CONSTANT_USP_INSERTMETERHISTORY,
                                              sqlParameters);
                    if (int.Parse(sqlParameters[1].Value.ToString()) == 0)
                    {
                        LogManager.WriteLog(
                            "ImportData WS - MeterHistory" + " SQL Error Occured " + sqlParameters[1].Value,
                            LogManager.enumLogLevel.Info);
                        throw new Exception("ImportData WS - MeterHistory" + "  SQL Error Occured  " +
                                            sqlParameters[1].Value);
                    }
                    if (int.Parse(sqlParameters[1].Value.ToString()) < 0)
                    {
                        LogManager.WriteLog(
                            "ImportData WS - MeterHistory" +
                            " 1.Record already processed,2.Link reference not found::: " + sqlParameters[1].Value,
                            LogManager.enumLogLevel.Info);
                        throw new Exception("ImportData WS - MeterHistory" +
                                            " 1.Record already processed,2.Link reference not found::: " +
                                            sqlParameters[1].Value);
                    }
                    if (int.Parse(sqlParameters[1].Value.ToString()) > 0)
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
                throw;
            }

            return bSuccess;
        }

        public static bool ImportPaytableDetails(string PT_Xml)
        {
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

        public static bool ImportGameSessionDetails(string Session_Xml)
        {
            try
            {
                DataBaseServiceHandler.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "usp_UpdateGameSessionDetails",
                    DataBaseServiceHandler.AddParameter<string>("doc", DbType.Xml, Session_Xml));

                return true;
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                return false;
            }
        }

        public static bool ImportStackerLevelDetails(string strXML)
        {
            bool retval = true;
            try
            {
                var sqlParameters = new SqlParameter[2];

                sqlParameters[0] = new SqlParameter
                {
                    ParameterName = "doc",
                    Value = strXML,
                    Direction = ParameterDirection.Input
                };
                sqlParameters[1] = new SqlParameter
                {
                    ParameterName = "IsSuccess",
                    Value = 0,
                    Direction = ParameterDirection.Output
                };
                if (DataBaseServiceHandler.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "usp_ImportStackerLevelDetailsFromXML",
                   sqlParameters) <= 0)
                {
                    retval = false;
                }
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                retval = false;
            } 
            return retval;
        }
        /// <summary>
        /// LIQUIDATIONDETAILS
        /// </summary>
        /// <param name="strXML"></param>
        /// <returns></returns>
        public static bool ImportLiquidationDetails(string strXML, string siteCode)
        {
            bool retval = true;
            try
            {
                var sqlParameters = new SqlParameter[4];

                sqlParameters[0] = new SqlParameter
                {
                    ParameterName = "doc",
                    Value = strXML,
                    Direction = ParameterDirection.Input
                };

                sqlParameters[1] = new SqlParameter
                {
                    ParameterName = "IsSuccess",
                    Value = 0,
                    Direction = ParameterDirection.Output
                };

                sqlParameters[2] = new SqlParameter
                {
                    ParameterName = "HQ_Liquidation_Id",
                    Value = 0,
                    Direction = ParameterDirection.Output
                };

                sqlParameters[3] = new SqlParameter
                {
                    ParameterName = "Liquidation_Id",
                    Value = 0,
                    Direction = ParameterDirection.Output
                };

                int returnVal = DataBaseServiceHandler.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "usp_ImportLiquidationDetailsFromXML",
                   sqlParameters);

                if (sqlParameters[2].Value != null && sqlParameters[3].Value != null && sqlParameters[2].Value.ToString() != string.Empty && sqlParameters[3].Value.ToString() != string.Empty
                                && Convert.ToInt32(sqlParameters[2].Value.ToString()) > 0 && Convert.ToInt32(sqlParameters[3].Value.ToString()) > 0)
                {
                     _proxy = new Proxy.Proxy(siteCode);
                    var webUrl = SqlHelper.ExecuteScalar(GetConnectionString(), CommandType.Text,
                                                            "Select WebURL From Site Where Site_Code = " + siteCode.Trim()).ToString();
                    _proxy.WebURL = webUrl;
                    _proxy.UpdateHQLiquidationID(Convert.ToInt32(sqlParameters[2].Value.ToString()), Convert.ToInt32(sqlParameters[3].Value.ToString()));
                    return true;
                }

                if (returnVal <= 0)
                {
                    retval = false;
                }
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                retval = false;
            }
            return retval;
        }
        public static bool ImportLiquidationShareDetails(string strXML, string siteCode)
        {
            bool retval = true;
            try
            {
                var sqlParameters = new SqlParameter[4];

                sqlParameters[0] = new SqlParameter
                {
                    ParameterName = "doc",
                    Value = strXML,
                    Direction = ParameterDirection.Input
                };

                sqlParameters[1] = new SqlParameter
                {
                    ParameterName = "IsSuccess",
                    Value = 0,
                    Direction = ParameterDirection.Output
                };

                sqlParameters[2] = new SqlParameter
                {
                    ParameterName = "HQ_LiquidationShare_Id",
                    Value = 0,
                    Direction = ParameterDirection.Output
                };

                sqlParameters[3] = new SqlParameter
                {
                    ParameterName = "LiquidationShare_Id",
                    Value = 0,
                    Direction = ParameterDirection.Output
                };
                
                int returnVal = (DataBaseServiceHandler.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "usp_ImportLiquidationShareDetailsFromXML",
                   sqlParameters));

                if (sqlParameters[2].Value != null && sqlParameters[3].Value != null && sqlParameters[2].Value.ToString() != string.Empty && sqlParameters[3].Value.ToString() != string.Empty
                                && Convert.ToInt32(sqlParameters[2].Value.ToString()) > 0 && Convert.ToInt32(sqlParameters[3].Value.ToString()) > 0)
                {
                     _proxy = new Proxy.Proxy(siteCode);
                    var webUrl = SqlHelper.ExecuteScalar(GetConnectionString(), CommandType.Text,
                                                            "Select WebURL From Site Where Site_Code = " + siteCode.Trim()).ToString();
                    _proxy.WebURL = webUrl;
                    _proxy.UpdateHQLiquidationShareID(Convert.ToInt32(sqlParameters[2].Value.ToString()), Convert.ToInt32(sqlParameters[3].Value.ToString()));
                    return true;
                }

                if (returnVal <0)
                {
                    retval = false;
                }
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                retval = false;
            }
            return retval;
        }
        /// <summary>
        
        public static bool ImportGloryAuditDetails(string strXML)
        {
            bool retval = true;
            try
            {
                var sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter
                {
                    ParameterName = "doc",
                    Value = strXML,
                    Direction = ParameterDirection.Input
                };

                if (DataBaseServiceHandler.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "usp_ImportGloryAuditDetailsFromXML",
                   sqlParameters) <= 0)
                {
                    retval = false;
                }
            }
            catch (Exception Ex)
            {
                LogManager.WriteLog("ImportGloryAuditDetails  Failed... ", LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(Ex);
                retval = false;
            }
            return retval;
        }
        public static bool ImportMachineClassDetails(string MachineClassXml)
        {
            int return_value = 0;

            try
            {
                DataBaseServiceHandler.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "usp_UpdateNewMachineClass",
                    DataBaseServiceHandler.AddParameter<string>("doc", DbType.Xml, MachineClassXml),
                    DataBaseServiceHandler.AddParameter<int>("IsSuccess", DbType.Int32, return_value, ParameterDirection.Output));

                if (return_value > 0)
                    return true;

                return false;
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                return false;
            }
        }

        public static void GetImportCalendar(string siteID, int ehID, string siteCode, string calendarType)
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

                bool isCallSuccess = InvokeBgswsAdminWs(strXMLExportCalendarData,
                                                         Constants.CONSTANT_WEBMETHOD_IMPORTCALENDAR,
                                                         siteCode);

                if ((isCallSuccess == false))
                    UpdateExportHistoryTableWithStatus(ehID, "-1");
                else
                    UpdateExportHistoryTableWithStatus(ehID, "100");
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ConfigManager.Read(Constants.CONSTANT_LOGPATH) + "_Export.txt", ex);
                UpdateExportHistoryTableWithStatus(ehID, "-1");
            }
        }

        // the method was added by Sudarsan S on 24-05-2008 for FIFO collection
        public static void GetImportSite(String siteID, int ehID, string siteCode)
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

                bool isCallSuccess = InvokeBgswsAdminWs(strXMLExportSiteData, Constants.CONSTANT_WEBMETHOD_IMPORTSITE, siteCode);

                if ((isCallSuccess == false))
                    UpdateExportHistoryTableWithStatus(ehID, "-1");
                else
                    UpdateExportHistoryTableWithStatus(ehID, "100");
            }

            catch (Exception ex)
            {
                UpdateExportHistoryTableWithStatus(ehID, "-1");
                ExceptionManager.Publish(ConfigManager.Read(Constants.CONSTANT_LOGPATH) + "_Export.txt", ex);
            }
        }

        /// <summary>
        /// Gets the import settings.
        /// </summary>
        /// <param name="siteID">The site ID.</param>
        /// <param name="exportHistoryID">The export history ID.</param>
        /// <param name="siteCode">The site code.</param>
        /// <returns></returns>
        public static void GetImportSettings(string siteID, int exportHistoryID, string siteCode)
        {
            try
            {
                var sqlParam = new SqlParameter("Site_ID", Convert.ToInt32(siteID));
                string exportSiteData = (SqlHelper.ExecuteScalar(GetConnectionString(),
                                                                 CommandType.StoredProcedure,
                                                                 "rsp_GetSiteSettingsInXML", sqlParam)).ToString();

                if (InvokeBgswsAdminWs(exportSiteData, "ImportSiteSettings", siteCode))
                    UpdateExportHistoryTableWithStatus(exportHistoryID, "100");
                else
                    UpdateExportHistoryTableWithStatus(exportHistoryID, "-1");

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ConfigManager.Read(Constants.CONSTANT_LOGPATH) + "_Export.txt", ex);
                UpdateExportHistoryTableWithStatus(exportHistoryID, "-1");
            }
        }

        public static void GetImportUserRoles(int UserID, int ehID, string SiteCode)
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
                bool isCallSuccess = InvokeBgswsAdminWs(xmlUserRolesData, "USERROLE", SiteCode);
                if (isCallSuccess)
                {
                    UpdateExportHistoryTableWithStatus(ehID, "100");
                    LogManager.WriteLog(ConfigManager.Read(Constants.CONSTANT_LOGPATH) + "_Export.txt", "User Role Updated to site " + SiteCode, LogManager.enumLogLevel.Info);
                }
                else
                {
                    UpdateExportHistoryTableWithStatus(ehID, "-1");
                    LogManager.WriteLog(ConfigManager.Read(Constants.CONSTANT_LOGPATH) + "_Export.txt", "Unable to update user role to site " + SiteCode, LogManager.enumLogLevel.Info);
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ConfigManager.Read(Constants.CONSTANT_LOGPATH) + "_Export.txt", ex);
                UpdateExportHistoryTableWithStatus(ehID, "-1");
            }
        }

        public static void GetImportGameInfo(int InstallationID, int ehID, string SiteCode)
        {
            var sqlParameters = new SqlParameter[1];
            try
            {
                LogManager.WriteLog(ConfigManager.Read(Constants.CONSTANT_LOGPATH) + "_Export.txt", "Import Game Info for - " + InstallationID + " Site Code -" + SiteCode + "Started.", LogManager.enumLogLevel.Info);

                sqlParameters[0] = new SqlParameter
                {
                    ParameterName = "Installation_ID",
                    Value = InstallationID,
                    Direction = ParameterDirection.Input
                };
                var InstallationGameInfoXML = (SqlHelper.ExecuteScalar(GetConnectionString(), CommandType.StoredProcedure, "rsp_GetInstallationGameInfoinXML", sqlParameters)).ToString();
                bool isCallSuccess = InvokeBgswsAdminWs(InstallationGameInfoXML, "GAMEINFO", SiteCode);
                if (isCallSuccess)
                {
                    UpdateExportHistoryTableWithStatus(ehID, "100");
                    LogManager.WriteLog(ConfigManager.Read(Constants.CONSTANT_LOGPATH) + "_Export.txt", "Import Game Info for - " + InstallationID + " Site Code -" + SiteCode + "Completed.", LogManager.enumLogLevel.Info);
                }
                else
                {
                    UpdateExportHistoryTableWithStatus(ehID, "-1");
                    LogManager.WriteLog(ConfigManager.Read(Constants.CONSTANT_LOGPATH) + "_Export.txt", "Import Game Info for - " + InstallationID + " Site Code -" + SiteCode + "Failed.", LogManager.enumLogLevel.Info);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ConfigManager.Read(Constants.CONSTANT_LOGPATH) + "_Export.txt", ex);
                UpdateExportHistoryTableWithStatus(ehID, "-1");
            }
        }

        public static void GetImportRoleAccessLinks(int RoleID, int ehID, string SiteCode)
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
                bool isCallSuccess = InvokeBgswsAdminWs(xmlRolesData, "ROLEACCESSLINK", SiteCode);
                if (isCallSuccess)
                {
                    UpdateExportHistoryTableWithStatus(ehID, "100");
                    LogManager.WriteLog(ConfigManager.Read(Constants.CONSTANT_LOGPATH) + "_Export.txt", "RoleAccess link Updated to site " + SiteCode, LogManager.enumLogLevel.Info);
                }
                else
                {
                    UpdateExportHistoryTableWithStatus(ehID, "-1");
                    LogManager.WriteLog(ConfigManager.Read(Constants.CONSTANT_LOGPATH) + "_Export.txt", "Unable to update role access link to site " + SiteCode, LogManager.enumLogLevel.Info);
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ConfigManager.Read(Constants.CONSTANT_LOGPATH) + "_Export.txt", ex);
                UpdateExportHistoryTableWithStatus(ehID, "-1");
            }

        }

        public static void GetImportUser(int UserID, int ehID, string SiteCode, string MethodToInvoke)
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
                var xmlUserData = (SqlHelper.ExecuteScalar(GetConnectionString(), CommandType.StoredProcedure, "rsp_GetUsersInXML", sqlParameters)).ToString();

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
                    LogManager.WriteLog(ConfigManager.Read(Constants.CONSTANT_LOGPATH) + "_Export.txt", "User Updated to site " + SiteCode, LogManager.enumLogLevel.Info);
                }
                else
                {
                    UpdateExportHistoryTableWithStatus(ehID, "-1");
                    LogManager.WriteLog(ConfigManager.Read(Constants.CONSTANT_LOGPATH) + "_Export.txt", "Unable to update User to site " + SiteCode, LogManager.enumLogLevel.Info);
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ConfigManager.Read(Constants.CONSTANT_LOGPATH) + "_Export.txt", ex);
                UpdateExportHistoryTableWithStatus(ehID, "-1");
            }
        }

        public static void GetImportModel(string modelID, int ehID, string siteCode)
        {
            try
            {
                var sqlParameters = new SqlParameter[1];
                var sqlParameter = new SqlParameter
                                 {
                                     ParameterName = "ID",
                                     Value = modelID,
                                     Direction = ParameterDirection.Input
                                 };
                sqlParameters[0] = sqlParameter;
                var xmlExportModelData = (SqlHelper.ExecuteScalar(GetConnectionString(),
                                                                     CommandType.StoredProcedure,
                                                                     Constants.CONSTANT_RSP_EXPORTMODEL, sqlParameters)).
                    ToString();

                var isCallSuccess = InvokeBgswsAdminWs(xmlExportModelData, Constants.CONSTANT_WEBMETHOD_IMPORTMODEL, siteCode);
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

        private static bool InvokeBgswsAdminWsForUser(string xmlToSend, bool isAdd, string siteCode)
        {
            bool isCallSuccess = false;
            _proxy = new Proxy.Proxy(siteCode);
            var webUrl = SqlHelper.ExecuteScalar(GetConnectionString(), CommandType.Text,
                                                    "Select WebURL From Site Where Site_Code = " + siteCode.Trim()).ToString();
            try
            {
                _proxy.WebURL = webUrl;
                isCallSuccess = _proxy.ImportUser(xmlToSend, isAdd);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                isCallSuccess = false;
            }
            return isCallSuccess;
        }

        private static bool InvokeBgswsAdminWs(string xmlToSend, string wsToInvoke, string siteCode)
        {
            bool isCallSuccess = false;
            _proxy = new Proxy.Proxy(siteCode);
            var webUrl = SqlHelper.ExecuteScalar(GetConnectionString(), CommandType.Text,
                                                    "Select WebURL From Site Where Site_Code = " + siteCode.Trim()).ToString();
            try
            {
                _proxy.WebURL = webUrl;

                if (wsToInvoke == Constants.CONSTANT_WEBMETHOD_IMPORTMETERHISTORY)
                {
                    isCallSuccess = _proxy.ImportData(xmlToSend);
                }
                else if (wsToInvoke == Constants.CONSTANT_WEBMETHOD_IMPORTSITE)
                {
                    isCallSuccess = _proxy.ImportSite(xmlToSend);
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

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ConfigManager.Read(Constants.CONSTANT_LOGPATH) + "_Export.txt", ex);
                isCallSuccess = false;
            }
            return isCallSuccess;
        }

        private static bool InvokeBgswsAdminWsForCollectionByDate(string strCollectionByDateDetails, string strSiteCode)
        {
            var isCallSuccess = false;
            _proxy = new Proxy.Proxy(strSiteCode);
            var webUrl = SqlHelper.ExecuteScalar(GetConnectionString(), CommandType.Text,
                                                       "Select WebURL From Site Where Site_Code = " + strSiteCode.Trim()).ToString();
            try
            {
                _proxy.WebURL = webUrl;
                isCallSuccess = _proxy.RequestCollectionByDate(strCollectionByDateDetails, strSiteCode);

                return isCallSuccess;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return isCallSuccess;
            }
        }

        public static int UpdateExportHistoryTableWithStatus(int ehID, string ehStatus)
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
                    LogManager.WriteLog(ConfigManager.Read(Constants.CONSTANT_LOGPATH) + "_Export.txt", "The export for EH ID" + ehID + " has failed.", LogManager.enumLogLevel.Info);
                }
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ConfigManager.Read(Constants.CONSTANT_LOGPATH) + "_Export.txt", ex);
                iNoOfRowsAffected = -1;
            }
            return iNoOfRowsAffected;
        }

        public static DataSet GetAllExportData()
        {
            DataSet objdsAllExportData = null;

            try
            {
                objdsAllExportData = SqlHelper.ExecuteDataset(GetConnectionString(), Constants.CONSTANT_USP_ALLEXPORTDATA);

                return objdsAllExportData;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return objdsAllExportData;
            }
        }

        public static int CanDataBeProcessed(string ehid, String ihid)
        {
            var sqlParameters = new SqlParameter[2];
            var sqlParameter = new SqlParameter
                                   {
                                       ParameterName = "@EH_ID",
                                       Value = int.Parse(ehid),
                                       DbType = DbType.Int32,
                                       Direction = ParameterDirection.Input
                                   };


            sqlParameters[0] = sqlParameter;
            sqlParameter = new SqlParameter
                               {
                                   ParameterName = "@IH_ID",
                                   Value = int.Parse(ihid),
                                   DbType = DbType.Int32,
                                   Direction = ParameterDirection.Input
                               };
            sqlParameters[1] = sqlParameter;

            return
                int.Parse(
                    SqlHelper.ExecuteScalar(GetConnectionString(), Constants.CONSTANT_USP_CANDATABEPROCESSED,
                                            sqlParameters).ToString().Trim().ToUpper());
        }

        public static void SetCollectionByDateBarPositions(string strCollectionByDate, string strSiteCode, int ehID)
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
                ExceptionManager.Publish(ConfigManager.Read(Constants.CONSTANT_LOGPATH) + "_Export.txt", ex);
                UpdateExportHistoryTableWithStatus(ehID, "-1");
            }
        }

        public static void ExportMachineNoteAcceptorEnableDisableStatus(int ehID, bool shouldEnable)
        {
            string query = "SELECT bar_position_name + ','+ EH_SITE_Code + ',' + WebURL FROM Bar_Position  INNER JOIN Export_History ON Bar_POSITION_ID = EH_Reference1 AND EH_ID = " +
                               ehID + " INNER JOIN SITE ON SITE_Code = EH_Site_Code ";

            string barPostionDetails = SqlHelper.ExecuteScalar(GetConnectionString(), CommandType.Text, query).ToString();

            if (String.IsNullOrEmpty(barPostionDetails))
            {
                LogManager.WriteLog(ConfigManager.Read(Constants.CONSTANT_LOGPATH) + "_Export.txt", "No Bar Position Returned for the the EH_ID: " + ehID, LogManager.enumLogLevel.Info);
                UpdateExportHistoryTableWithStatus(ehID, "-1");
                return;
            }

            if (barPostionDetails.Split(',').Length != 3)
            {
                LogManager.WriteLog(ConfigManager.Read(Constants.CONSTANT_LOGPATH) + "_Export.txt", "Invalid Data for the Argument SQL statement: " + query,
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

        public static void ExportMachineEnableStatus(int ehID, bool shouldEnable)
        {
            string query = "SELECT bar_position_name + ','+ EH_SITE_Code + ',' + WebURL FROM Bar_Position  INNER JOIN Export_History ON Bar_POSITION_ID = EH_Reference1 AND EH_ID = " +
                               ehID + " INNER JOIN SITE ON SITE_Code = EH_Site_Code ";

            string barPostionDetails = SqlHelper.ExecuteScalar(GetConnectionString(), CommandType.Text, query).ToString();

            if (String.IsNullOrEmpty(barPostionDetails))
            {
                LogManager.WriteLog(ConfigManager.Read(Constants.CONSTANT_LOGPATH) + "_Export.txt", "No Bar Position Returned for the the EH_ID: " + ehID, LogManager.enumLogLevel.Info);
                UpdateExportHistoryTableWithStatus(ehID, "-1");
                return;
            }

            if (barPostionDetails.Split(',').Length != 3)
            {
                LogManager.WriteLog(ConfigManager.Read(Constants.CONSTANT_LOGPATH) + "_Export.txt", "Invalid Data for the Argument SQL statement: " + query,
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
                UpdateExportHistoryTableWithStatus(ehID, "100");
            else
                UpdateExportHistoryTableWithStatus(ehID, "-1");
        }


        public static bool WrapExportXMLData(string sXMLData, string ehid, string type, string url, string siteCode)
        {
            try
            {
                _proxy = new Proxy.Proxy(siteCode);
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

                sXMLData =
                    SqlHelper.ExecuteScalar(GetConnectionString(), Constants.CONSTANT_RSP_WRAPSITEDETAILS, sqlParameters).ToString();
                sXMLData = sXMLData.Replace(Environment.NewLine, "");
                sXMLData = sXMLData.Replace("\r", "");
                sXMLData = sXMLData.Replace("\n", "");
                sXMLData = sXMLData.Replace("\r\n", "");

                return _proxy.ImportData(sXMLData);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ConfigManager.Read(Constants.CONSTANT_LOGPATH) + "_Export.txt", ex);
            }
            return false;
        }



        public static bool ResetInProgressIhRecords(string siteCode)
        {
            bool bSuccess;
            try
            {
                var oParams = new SqlParameter[1];
                var oSiteCodeParam = new SqlParameter
                                         {
                                             ParameterName = "SiteCode",
                                             Value = "SiteCode",
                                             Direction = ParameterDirection.Input
                                         };
                oParams[0] = oSiteCodeParam;

                if (siteCode.Trim() == "")
                    SqlHelper.ExecuteScalar(GetConnectionString(), CommandType.StoredProcedure,
                                            Constants.CONSTANT_RSP_RESETINPROGRESSIHRECORDSTOENTERPRISE);
                else
                    SqlHelper.ExecuteScalar(GetConnectionString(), CommandType.StoredProcedure,
                                            Constants.CONSTANT_RSP_RESETINPROGRESSIHRECORDSTOENTERPRISE, oParams);

                bSuccess = true;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                bSuccess = false;
            }

            return bSuccess;
        }

        public static bool ResetInProgressEhRecords()
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

        public static void GetImportAutoInstallation(int installationid, int ehID, string siteCode)
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
                        LogManager.WriteLog(ConfigManager.Read(Constants.CONSTANT_LOGPATH) + "_Export.txt", "GetAutoInstallation: Sorry no Weburl found for ehid: " + ehID.ToString() + " Site_Code:" + siteCode.ToString(), LogManager.enumLogLevel.Info);
                    }
                }
                else
                {
                    LogManager.WriteLog(ConfigManager.Read(Constants.CONSTANT_LOGPATH) + "_Export.txt", "GetAutoInstallation: Sorry no Installation found for ehid: " + ehID.ToString() + " Site_Code:" + siteCode.ToString(), LogManager.enumLogLevel.Info);
                }
            }
            catch (Exception ex)
            {
                UpdateExportHistoryTableWithStatus(ehID, "-1");
                ExceptionManager.Publish(ConfigManager.Read(Constants.CONSTANT_LOGPATH) + "_Export.txt", ex);
            }
        }

        #region Import Collection related methods

        public static bool ImportCollection(string strXML)
        {
            bool isSuccess;
            int iCommandTimeout;

            var sqlParameters = new SqlParameter[2];

            sqlParameters[0] = new SqlParameter
                                   {
                                       ParameterName = "doc",
                                       Value = strXML,
                                       Direction = ParameterDirection.Input
                                   };


            sqlParameters[1] = new SqlParameter
                                   {
                                       ParameterName = "IsSuccess",
                                       SqlDbType = SqlDbType.VarChar,
                                       Size = 500,
                                       Value = "dummy",
                                       Direction = ParameterDirection.Output
                                   };


            try
            {
                iCommandTimeout = int.Parse(ConfigManager.Read("CommandTimeOut"));
            }
            catch (Exception ex)
            {
                iCommandTimeout = 60;
                ExceptionManager.Publish(ex);
                LogManager.WriteLog("CommandTimeout not set. setting a default value of 60",
                                    LogManager.enumLogLevel.Warning);
            }

            SqlHelper.ExecuteNonQueryWithCommandTimeOut(GetConnectionString(),
                                                        CommandType.StoredProcedure,
                                                        Constants.CONSTANT_USP_INSERTCOLLECTIONFROMXML,
                                                        iCommandTimeout,
                                                        sqlParameters);

            if (sqlParameters[1].Value.ToString().Trim().ToUpper() == "Success".Trim().ToUpper())
            {
                isSuccess = true;
                LogManager.WriteLog("ImportCollection WS " + "  Success value " + isSuccess, LogManager.enumLogLevel.Info);
            }
            else
            {
                throw new Exception(sqlParameters[1].Value.ToString());
            }
            return true;
        }


        public static bool ImportIndividualCollectionDetails(string strXML)
        {
            bool bSuccess;
            int iCommandTimeout;

            var sqlParameters = new SqlParameter[2];

            var sqlParameter = new SqlParameter
                                   {
                                       ParameterName = "doc",
                                       Value = strXML,
                                       Direction = ParameterDirection.Input
                                   };
            sqlParameters[0] = sqlParameter;

            sqlParameter = new SqlParameter
                               {
                                   ParameterName = "IsSuccess",
                                   Direction = ParameterDirection.Output,
                                   SqlDbType = SqlDbType.VarChar,
                                   Size = 500
                               };
            sqlParameters[1] = sqlParameter;


            try
            {
                iCommandTimeout = int.Parse(ConfigManager.Read("CommandTimeOut"));
            }
            catch (Exception ex)
            {
                iCommandTimeout = 60;
                LogManager.WriteLog("CommandTimeout not set. setting a default value of 60, Throwing Exception",
                                    LogManager.enumLogLevel.Warning);
                ExceptionManager.Publish(ex);
            }

            SqlHelper.ExecuteNonQueryWithCommandTimeOut(GetConnectionString(),
                                                        CommandType.StoredProcedure,
                                                        Constants.CONSTANT_USP_INSERTINDVCOLLECTIONFROMXML,
                                                        iCommandTimeout,
                                                        sqlParameters);


            if (sqlParameters[1].Value.ToString().Trim().ToUpper() == "Success".Trim().ToUpper())
            {
                bSuccess = true;
                LogManager.WriteLog("ImportIndividualCollectionDetails WS " + "  Success value " + bSuccess,
                                    LogManager.enumLogLevel.Info);
            }
            else
            {
                throw new Exception(sqlParameters[1].Value.ToString());
            }
            return true;
        }

        #endregion

        public static void GetAAMSConfigRecord(int iAAMSID, int ehID, string siteCode)
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
                    if (InvokeBgswsAdminWs(xmlExportAAMSDetails, "ImportAAMSConfigDetails", siteCode))
                        UpdateExportHistoryTableWithStatus(ehID, "100");
                    else
                        UpdateExportHistoryTableWithStatus(ehID, "-1");
                }
                else
                {
                    LogManager.WriteLog(ConfigManager.Read(Constants.CONSTANT_LOGPATH) + "_Export.txt", "GetAAMSConfigRecord: Sorry no data found for ehid: " + ehID.ToString() + " Site_Code:" + siteCode.ToString(), LogManager.enumLogLevel.Info);
                }
            }
            catch (Exception ex)
            {
                UpdateExportHistoryTableWithStatus(ehID, "-1");
                ExceptionManager.Publish(ConfigManager.Read(Constants.CONSTANT_LOGPATH) + "_Export.txt", ex);
            }
        }


        public static bool ImportPasswordChange(string sXml)
        {
            try
            {
                DataBaseServiceHandler.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "usp_UpdateUserInfoFromXML",
                    DataBaseServiceHandler.AddParameter<string>("doc", DbType.Xml, sXml));

                return true;
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                return false;
            }
        }

        public static bool ImportMachineMaintenance(string sXml)
        {
            try
            {
                DataBaseServiceHandler.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "usp_UpdateMachineMaintenanceStatus",
                    DataBaseServiceHandler.AddParameter<string>("doc", DbType.Xml, sXml));

                LogManager.WriteLog("Updated Machine Maintenance Status", LogManager.enumLogLevel.Info);

                return true;
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                return false;
            }
        }

        public static bool ImportMaintenanceSession(string sXml)
        {
            try
            {
                DataBaseServiceHandler.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "usp_ImportMaintenanceSession",
                    DataBaseServiceHandler.AddParameter<string>("doc", DbType.Xml, sXml));

                LogManager.WriteLog("Updated MaintenanceSession", LogManager.enumLogLevel.Info);

                return true;
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                return false;
            }
        }

        public static bool ImportMaintenanceHistory(string sXml)
        {
            try
            {
                DataBaseServiceHandler.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "usp_ImportMaintenanceHistory",
                    DataBaseServiceHandler.AddParameter<string>("doc", DbType.Xml, sXml));

                LogManager.WriteLog("Updated MaintenanceHistory", LogManager.enumLogLevel.Info);

                return true;
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                return false;
            }
        }

        public static bool ImportMaintenanceReasonCategory(string sXml)
        {
            try
            {
                DataBaseServiceHandler.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "usp_ImportMaintenanceReasonCategory",
                    DataBaseServiceHandler.AddParameter<string>("doc", DbType.Xml, sXml));

                LogManager.WriteLog("Updated Maintenance Reason and Category", LogManager.enumLogLevel.Info);

                return true;
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                return false;
            }
        }

        public static bool ImportReInstateData(string sXml)
        {
            try
            {
                DataBaseServiceHandler.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "usp_ImportReInstateData",
                    DataBaseServiceHandler.AddParameter<string>("doc", DbType.Xml, sXml));

                LogManager.WriteLog("Updated ReInstate Data", LogManager.enumLogLevel.Info);

                return true;
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                return false;
            }
        }

        public static bool ImportGamePaytableDetails(string sXml)
        {
            try
            {
                DataBaseServiceHandler.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "usp_UpdateInstallationGamePaytableInfo",
                    DataBaseServiceHandler.AddParameter<string>("doc", DbType.Xml, sXml));

                LogManager.WriteLog("Updated Game Paytable Details", LogManager.enumLogLevel.Info);

                return true;
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                return false;
            }
        }

        public static bool ImportBatchExportCompletedStatus(string sXml)
        {
            try
            {
                DataBaseServiceHandler.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "usp_Import_BatchExportCompleteStatus",
                    DataBaseServiceHandler.AddParameter<string>("doc", DbType.AnsiString, sXml));

                LogManager.WriteLog("Updated Batch Export Completed status.", LogManager.enumLogLevel.Info);

                return true;
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                return false;
            }
        }
        public static bool ImportFundDetails(string sXml)
        {
            try
            {
                var sqlParameters = new SqlParameter[2];
                sqlParameters[0] = new SqlParameter("doc", sXml);
                sqlParameters[1] = new SqlParameter("IsSuccess", SqlDbType.Int) { Direction = ParameterDirection.Output };

                DataBaseServiceHandler.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "usp_Import_FundDetails", sqlParameters);
                if (int.Parse(sqlParameters[1].Value.ToString()) == 0)
                {
                    LogManager.WriteLog("Updated Fund Details Successfully.", LogManager.enumLogLevel.Info);
                    return true;
                }

                LogManager.WriteLog("Error in update Fund Details.", LogManager.enumLogLevel.Info);
                return false;
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                return false;
            }
        }

        public static bool DoFactoryReset(string Site_Code, string sXml)
        {
            try
            {
                var sqlParameters = new SqlParameter[3];
                sqlParameters[0] = new SqlParameter("Site_Code", Site_Code);
                sqlParameters[1] = new SqlParameter("doc", sXml);
                sqlParameters[2] = new SqlParameter("IsSuccess", SqlDbType.Int) { Direction = ParameterDirection.Output };

                DataBaseServiceHandler.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "usp_FactoryResetProcess", sqlParameters);
                if (int.Parse(sqlParameters[2].Value.ToString()) == 0)
                {
                    LogManager.WriteLog("Reset Completed for site :: " + Site_Code, LogManager.enumLogLevel.Info);
                    return true;
                }

                LogManager.WriteLog("Error while reseting site :: " + Site_Code, LogManager.enumLogLevel.Info);
                return false;
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                return false;
            }
        }
        #region Vault Methods

        public static bool ImportVaultDropDetails(string sXml)
        {
            try
            {
                LogManager.WriteLog("inside ImportVaultDropDetails", LogManager.enumLogLevel.Info);
                var sqlParameters = new SqlParameter[2];
                sqlParameters[0] = new SqlParameter("xml", sXml);
                sqlParameters[1] = new SqlParameter("IsSuccess", SqlDbType.Int) { Direction = ParameterDirection.Output };
                DataBaseServiceHandler.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "usp_Vault_ImportDrop", sqlParameters);
                if (int.Parse(sqlParameters[1].Value.ToString()) == 0)
                {
                    LogManager.WriteLog("Updated Vault Drop Details Successfully.", LogManager.enumLogLevel.Info);
                    return true;
                }

                LogManager.WriteLog("Error in updating Vault Drop Details.", LogManager.enumLogLevel.Info);
                return false;
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                return false;
            }
        }

        public static bool ImportVaultTransactionEventDetails(string sXml)
        {
            try
            {
                LogManager.WriteLog("inside ImportVaultTransactionEventDetails", LogManager.enumLogLevel.Info);
                var sqlParameters = new SqlParameter[2];
                sqlParameters[0] = new SqlParameter("xml", sXml);
                sqlParameters[1] = new SqlParameter("IsSuccess", SqlDbType.Int) { Direction = ParameterDirection.Output };
                DataBaseServiceHandler.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "usp_Vault_ImportTransactionEvent", sqlParameters);
                if (int.Parse(sqlParameters[1].Value.ToString()) == 0)
                {
                    LogManager.WriteLog("Updated Vault Transaction Event Details Successfully.", LogManager.enumLogLevel.Info);
                    return true;
                }

                LogManager.WriteLog("Error in updating Vault Transaction Event Details.", LogManager.enumLogLevel.Info);
                return false;
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                return false;
            }
        }

        public static bool ImportVaultEventDetails(string sXml)
        {
            try
            {
                LogManager.WriteLog("inside ImportVaultEventDetails", LogManager.enumLogLevel.Info);
                var sqlParameters = new SqlParameter[2];
                sqlParameters[0] = new SqlParameter("xml", sXml);
                sqlParameters[1] = new SqlParameter("IsSuccess", SqlDbType.Int) { Direction = ParameterDirection.Output };
                DataBaseServiceHandler.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "usp_Vault_ImportEvent", sqlParameters);
                if (int.Parse(sqlParameters[1].Value.ToString()) == 0)
                {
                    LogManager.WriteLog("Updated Vault Event Details Successfully.", LogManager.enumLogLevel.Info);
                    return true;
                }

                LogManager.WriteLog("Error in updating Vault Event Details.", LogManager.enumLogLevel.Info);
                return false;
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                return false;
            }
        }

        public static bool ImportVaultBalanceDetails(string sXml)
        {
            bool retval = true;
            try
            {
                LogManager.WriteLog("inside ImportVaultBalanceDetails", LogManager.enumLogLevel.Info);
                var sqlParameters = new SqlParameter[2];
                sqlParameters[0] = new SqlParameter("doc", sXml);
                sqlParameters[1] = new SqlParameter("IsSuccess", SqlDbType.Int) { Direction = ParameterDirection.Output };
                DataBaseServiceHandler.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "usp_ImportVaultBalanceDetailsFromXML", sqlParameters);
                if (int.Parse(sqlParameters[1].Value.ToString()) == 0)
                {
                    LogManager.WriteLog("Updated Vault Balance Details Successfully.", LogManager.enumLogLevel.Info);
                    return true;
                }
                LogManager.WriteLog("Error in updating Vault Balance Details.", LogManager.enumLogLevel.Info);
                return false;
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                retval = false;
            }
            return retval;
        }

        public static bool ImportVaultTransactionDetails(string sXml)
        {
            bool retval = true;
            try
            {
                LogManager.WriteLog("inside ImportVaultTransactionDetails", LogManager.enumLogLevel.Info);
                var sqlParameters = new SqlParameter[2];
                sqlParameters[0] = new SqlParameter("@xml", sXml);
                sqlParameters[1] = new SqlParameter("IsSuccess", SqlDbType.Int) { Direction = ParameterDirection.Output };
                DataBaseServiceHandler.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "usp_Vault_ImportTransaction", sqlParameters);
                if (int.Parse(sqlParameters[1].Value.ToString()) == 0)
                {
                    LogManager.WriteLog("Updated Vault Transaction Details Successfully.", LogManager.enumLogLevel.Info);
                    return true;
                }
                LogManager.WriteLog("Error in updating Vault Transaction Details.", LogManager.enumLogLevel.Info);
                return false;
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                retval = false;
            }
            return retval;
        }

        public static bool ImportVaultEnrollmentDetails(string sXml)
        {
            bool retval = true;
            try
            {
                LogManager.WriteLog("inside ImportVaultTransactionDetails", LogManager.enumLogLevel.Info);
                var sqlParameters = new SqlParameter[2];
                sqlParameters[0] = new SqlParameter("@xml", sXml);
                sqlParameters[1] = new SqlParameter("IsSuccess", SqlDbType.Int) { Direction = ParameterDirection.Output };
                DataBaseServiceHandler.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "usp_Vault_ImportEnrollmentDetails", sqlParameters);
                if (int.Parse(sqlParameters[1].Value.ToString()) == 0)
                {
                    LogManager.WriteLog("Updated Vault Enrollment Details Successfully.", LogManager.enumLogLevel.Info);
                    return true;
                }
                LogManager.WriteLog("Error in updating Vault Enrollment Details.", LogManager.enumLogLevel.Info);
                return false;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                retval = false;
            }
            return retval;
        }

        #endregion
    }
}