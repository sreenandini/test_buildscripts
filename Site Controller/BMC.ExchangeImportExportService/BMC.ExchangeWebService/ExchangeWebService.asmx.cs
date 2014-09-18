using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml;
using BMC.Common;
using BMC.Common.Compression;
using BMC.Common.ConfigurationManagement;
using BMC.Common.ExceptionManagement;
using BMC.Common.LogManagement;
using BMC.Common.Security;
using BMC.DataAccess;
using BMC.Monitoring;
using Microsoft.Win32;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using BMC.Common.Utilities;
using System.Data.Common;
using System.Threading.Tasks;


namespace BMC.ExchangeWebService
{
    [WebService(Namespace = "http://www.ballytech.com/BallyMultiConnect/WSE/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService] 
    public class ExchangeWebService : WebService
    {

        #region Private Fields
        #endregion

        #region Public Fields
        public AuthenticationInformation AuthenticationInfo;
        #endregion

        #region ctor
        #endregion

        #region "Private Methods"

        private static string GetValueFromSetting(string sConfigValue, string sDefaultValue)
        {
            try
            {
                var configParam = new SqlParameter[3];
                configParam[0] = new SqlParameter
                                 {
                                     ParameterName = "Setting_Name",
                                     Value = sConfigValue,
                                     Direction = ParameterDirection.Input
                                 };


                configParam[1] = new SqlParameter
                             {
                                 ParameterName = "Setting_Default",
                                 Value = sDefaultValue,
                                 Direction = ParameterDirection.Input
                             };

                configParam[2] = new SqlParameter
                                     {
                                         ParameterName = "Setting_Value",
                                         Direction = ParameterDirection.Output,
                                         SqlDbType = SqlDbType.VarChar,
                                         Size = 500
                                     };

                SqlHelper.ExecuteScalar(GetConnectionString(), CommandType.StoredProcedure, "rsp_GetSetting", configParam);
                var settingValue = configParam[2].Value.ToString();
                return settingValue;
            }
            catch
            {
                return sDefaultValue;
            }
        }

        private void CheckSecurity()
        {
            //LogManager.WriteLog("SiteCode:" + AuthenticationInfo.SiteCode, LogManager.enumLogLevel.Info );
            //LogManager.WriteLog("Ent -- EnterprisePasskey:" + AuthenticationInfo.EnterprisePassKey, LogManager.enumLogLevel.Info);
            //LogManager.WriteLog("Ex --  EnterprisePasskey:" + GetEnterprisePasskey(), LogManager.enumLogLevel.Info);
            //LogManager.WriteLog("Ent -- ExchangePasskey:" + AuthenticationInfo.ExchangePassKey, LogManager.enumLogLevel.Info);
            //LogManager.WriteLog("Ex -- ExchangePasskey:" + GetExchangePasskey(), LogManager.enumLogLevel.Info);


            Exception ex;
            if (AuthenticationInfo == null)
            {
                ex = new Exception("ErrorCode: WS1001 No or Missing header Info.  Please validate");
                ExceptionManager.Publish(ex);
                throw ex;
            }
            if (string.IsNullOrEmpty(AuthenticationInfo.EnterprisePassKey) || AuthenticationInfo.EnterprisePassKey != GetEnterprisePasskey())
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

        //private static string GetEnterprisePasskey()
        //{
        //    RegistryKey key = BMCRegistryHelper.GetRegLocalMachine().OpenSubKey(ConfigManager.Read("RegistryPath"));
        //    if (key != null) return key.GetValue("EnterpriseKey").ToString();

        //    ExceptionManager.Publish(new Exception("Enterprise key not set"));
        //    return "";
        //}

        //private static string GetExchangePasskey()
        //{
        //    ConfigManager.SetConfigurationMode(ConfigManager.ConfigurationMode.AppConfig);
        //    RegistryKey key = BMCRegistryHelper.GetRegLocalMachine().OpenSubKey(ConfigManager.Read("RegistryPath"));
        //    if (key != null) return key.GetValue("ExchangeKey").ToString();

        //    ExceptionManager.Publish(new Exception("Exchange key not set"));
        //    return "Exchange";
        //}

        private static string GetEnterprisePasskey()
        {
            return DataBaseServiceHandler.ExecuteScalar<string>(GetConnectionString(), CommandType.StoredProcedure,
                                                                    "rsp_GetEnterprisePassKeys");
        }

        private static string GetExchangePasskey()
        {
            return DataBaseServiceHandler.ExecuteScalar<string>(GetConnectionString(), CommandType.StoredProcedure,
                                                                    "rsp_GetExchangePassKeys");
        }

        private static string GetConnectionString()
        {
            //RegistryKey key;
            ConfigManager.SetConfigurationMode(ConfigManager.ConfigurationMode.AppConfig);
            string SqlConnect = "";
            string sKey = string.Empty;
            //bool bUseHex = true;
            try
            {
                ConfigManager.Read("RegistryPath");
                //key = BMCRegistryHelper.GetRegLocalMachine().OpenSubKey(ConfigManager.Read("RegistryPath"));
                //LogManager.WriteLog("key --" + key, LogManager.enumLogLevel.Info);
                //if (key != null)
                //    SqlConnect = key.GetValue("SQLConnect").ToString();
                SqlConnect = BMCRegistryHelper.GetRegKeyValue(ConfigManager.Read("RegistryPath"), "SQLConnect");
                LogManager.WriteLog("SqlConnect -" + SqlConnect, LogManager.enumLogLevel.Info);
                if (!SqlConnect.ToUpper().Contains("SERVER"))
                {
                    SqlConnect = BMC.Common.Security.CryptEncode.Decrypt(SqlConnect);
                    //BGSGeneral.cConstants objCons = new BGSGeneral.cConstants();
                    //BGSEncryptDecrypt.clsBlowFish objDecrypt = new BGSEncryptDecrypt.clsBlowFish();
                    //sKey = objCons.ENCRYPTIONKEY;
                    //SqlConnect = objDecrypt.DecryptString(ref SqlConnect, ref sKey, ref bUseHex);
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error reading registry:" + ex.Message, LogManager.enumLogLevel.Error);
            }
            return SqlConnect;
        }
        #endregion

        #region "Public Methods"
        [WebMethod, SoapHeader("AuthenticationInfo")]
        [SoapSecurityExtension(Encrypt = EncryptMode.Request)]
        public int HelloWebService(int recieve)
        {
            //CheckSecurity();
            return recieve;
        }

        /// <summary>
        /// This method help us to find the connectivity of Exchange.
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public bool CheckConnectivity()
        {            
            return true;
        }

        [WebMethod, SoapHeader("AuthenticationInfo")]
        [SoapSecurityExtension(Encrypt = EncryptMode.Response)]
        public string GetSiteConnectionString()
        {
            ConfigManager.SetConfigurationMode(ConfigManager.ConfigurationMode.AppConfig);
            string SqlConnect = "";
            string sKey = string.Empty;
            try
            {
                ConfigManager.Read("RegistryPath");
                SqlConnect = BMCRegistryHelper.GetRegKeyValue(ConfigManager.Read("RegistryPath"), "SQLConnect");
                LogManager.WriteLog("SqlConnect -" + SqlConnect, LogManager.enumLogLevel.Info);
                //if (!SqlConnect.ToUpper().Contains("SERVER"))
                //{
                //    SqlConnect = BMC.Common.Security.CryptEncode.Decrypt(SqlConnect);                    
                //}
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error reading registry:" + ex.Message, LogManager.enumLogLevel.Error);
            }
            return SqlConnect;
        }


        [WebMethod, SoapHeader("AuthenticationInfo")]
        [SoapSecurityExtension(Encrypt = EncryptMode.Response)]
        public bool InitializeSite()
        {
            try
            {
                if (AuthenticationInfo == null)
                {
                    ExceptionManager.Publish(new Exception("ErrorCode: WS1001 No or Missing header Info.  Please validate"));
                    return false;
                }

                if (string.IsNullOrEmpty(AuthenticationInfo.ExchangePassKey))
                {
                    ExceptionManager.Publish(new Exception("ErrorCode: WS1004 Exchange Passkey.  Please validate"));
                    return false;
                }

                if (string.IsNullOrEmpty(AuthenticationInfo.EnterprisePassKey))
                {
                    ExceptionManager.Publish(new Exception("ErrorCode: WS1005 Enterprise Passkey.  Please validate"));
                    return false;
                }

                var returnValue = DataBaseServiceHandler.AddParameter<int>("@ReturnValue", DbType.Int32, 0, ParameterDirection.ReturnValue);
                DataBaseServiceHandler.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "usp_InsertPassKeys",
                    DataBaseServiceHandler.AddParameter<string>("@EnterpriseKey", DbType.String, AuthenticationInfo.EnterprisePassKey),
                    DataBaseServiceHandler.AddParameter<string>("@ExchangeKey", DbType.String, AuthenticationInfo.ExchangePassKey),
                    returnValue);

                if (int.Parse(returnValue.Value.ToString()) != 0)
                    return false;
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                return false;
            }

            return true;

            //RegistryKey key = BMCRegistryHelper.GetRegLocalMachine().OpenSubKey(ConfigManager.Read("RegistryPath"), true);
            //if (key != null)
            //{
            //    key.SetValue("ExchangeKey", AuthenticationInfo.ExchangePassKey);
            //    key.SetValue("EnterpriseKey", AuthenticationInfo.EnterprisePassKey);
            //    key.Close();
            //}
            //else
            //    ExceptionManager.Publish(new Exception("Log Path not set in the Config.  Invalid Log path"));

            //return true;

        }

        [WebMethod, SoapHeader("AuthenticationInfo")]
        [SoapSecurityExtension(Encrypt = EncryptMode.Response)]
        public string GetServiceStatus(string strServiceNames, BMCMonitoring.ServiceTypes serviceStatus)
        {
            CheckSecurity();

            string strSiteName = string.Empty;
            try
            {
                var dtServiceStatus = new DataTable("Services");

                var objMonitoring = new BMCMonitoring();
                switch (serviceStatus)
                {
                    case BMCMonitoring.ServiceTypes.All:
                        dtServiceStatus = objMonitoring.GetServiceStatus(strServiceNames, BMCMonitoring.ServiceTypes.All);
                        break;
                    case BMCMonitoring.ServiceTypes.Running:
                        dtServiceStatus = objMonitoring.GetServiceStatus(strServiceNames, BMCMonitoring.ServiceTypes.Running);
                        break;
                    case BMCMonitoring.ServiceTypes.NotRunning:
                        dtServiceStatus = objMonitoring.GetServiceStatus(strServiceNames, BMCMonitoring.ServiceTypes.NotRunning);
                        break;
                }
                var objWriter = new StringWriter();
                dtServiceStatus.WriteXml(objWriter);
                return objWriter.ToString();
            }
            catch
            {
                LogManager.WriteLog("Error in getting status from site " + strSiteName, LogManager.enumLogLevel.Error);
                return "<OutputXML><ErrorMessage>no records found </ErrorMessage></OutputXML> ";
            }
        }

        [WebMethod, SoapHeader("AuthenticationInfo")]
        [SoapSecurityExtension(Encrypt = EncryptMode.Response)]
        public bool StartService(string strServiceName)
        {
            CheckSecurity();

            try
            {
                var objMonitoring = new BMCMonitoring();
                return objMonitoring.StartService(strServiceName);

            }
            catch
            {
                LogManager.WriteLog("Error in starting service " + strServiceName, LogManager.enumLogLevel.Error);
                return false;
            }
        }

        [WebMethod, SoapHeader("AuthenticationInfo")]
        [SoapSecurityExtension(Encrypt = EncryptMode.Response)]
        public bool ImportUser(string xmlString, bool IsAddUser)
        {

            CheckSecurity();

            LogManager.WriteLog("Import User Called...", LogManager.enumLogLevel.Info);
            try
            {
                string UpdatedString = UpdateEmployeeFlags(xmlString);
                LogManager.WriteLog("Updated XML String" + UpdatedString, LogManager.enumLogLevel.Info);

                var returnValue = DataBaseServiceHandler.AddParameter<int>("@ReturnValue", DbType.Int32, 0, ParameterDirection.ReturnValue);
                DataBaseServiceHandler.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "usp_UpdateUserAccessFromXML",
                    DataBaseServiceHandler.AddParameter<string>("@doc", DbType.Xml, UpdatedString),
                    DataBaseServiceHandler.AddParameter<bool>("@AddUser", DbType.Boolean, IsAddUser), returnValue);
                if (int.Parse(returnValue.Value.ToString()) >= 0)
                {
                    LogManager.WriteLog("Updated User Successfully...", LogManager.enumLogLevel.Info);
                    return true;
                }
                else
                {
                    LogManager.WriteLog("Unable to update User - SQL Error " + returnValue.Value.ToString(), LogManager.enumLogLevel.Info);
                    return false;
                }


            }
            catch (Exception ex)
            {
                LogManager.WriteLog(ex.Message, LogManager.enumLogLevel.Error);
                return false;
            }
        }

        private string UpdateEmployeeFlags(string xmlString)
        {
            Dictionary<string, string> getModes = new Dictionary<string, string>();
            XmlDocument xmldoc = new XmlDocument();
            try
            {
                bool IsMasterCard = false;
                bool IsActive = false;
                if (!string.IsNullOrEmpty(xmlString))
                {
                    //Load the xmlstring
                    xmldoc.LoadXml(xmlString);
                    XmlNodeList node = xmldoc.GetElementsByTagName("Mode");

                    foreach (XmlNode childnode in node)
                        getModes.Add(childnode.FirstChild.InnerText, childnode.LastChild.InnerText);

                    node = null;

                    //Associate the Employee flags with employee card
                    node = xmldoc.SelectNodes("//EMPDetails//EMP");
                    string strModes = string.Empty;
                    for (int i = 0; i < node.Count; i++)
                    {
                        XmlNode childnode = node[i] as XmlNode;
                        byte[] Modes = new byte[64];

                        foreach (XmlNode subnode in childnode.ChildNodes)
                        {
                            //Check if it is master card
                            if (subnode.Name.ToUpper() == "ISMASTERCARD")
                            {
                                IsMasterCard = Convert.ToBoolean(subnode.InnerText == "0" ? false : true);
                                if (IsMasterCard)
                                {
                                    //Master card - all flags are set. 
                                    GetFlags(getModes.Where(mode => mode.Value == "W"), Modes);
                                    GetFlags(getModes.Where(mode => mode.Value == "R"), Modes);
                                    GetFlags(getModes.Where(mode => mode.Value == "RW"), Modes);
                                    strModes = String.Concat(Array.ConvertAll(Modes, x => x.ToString("X2")));
                                    break;
                                }
                            }
                            else if (subnode.Name.ToUpper() == "ISACTIVE")
                                IsActive = subnode.InnerText == "1" ? true : false;

                            else if (subnode.Name.ToUpper() == "USERID")
                                strModes = GetIndividualModes(0, Convert.ToInt32(subnode.InnerText), Modes);
                        }
                        strModes = strModes.Insert(0, IsActive ? "1" : "0");
                        strModes = strModes.Insert(1, IsMasterCard ? "1" : "0");
                        childnode.LastChild.InnerText = strModes;
                    }
                }
                return xmldoc.InnerXml;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return string.Empty;
            }
        }

        public string GetIndividualModes(int RoleID, int UserID, byte[] Modes)
        {
            LogManager.WriteLog("Inside GetIndividualModes, RoleId " + RoleID.ToString() + "UserId" + UserID.ToString(), LogManager.enumLogLevel.Info);
            string strModes = null;
            try
            {
                Dictionary<string, string> oModes = new Dictionary<string, string>();

                SqlParameter[] sqlParameters = new SqlParameter[2];
                SqlParameter sqlParameter = new SqlParameter
                {
                    ParameterName = "@RoleID",
                    SqlValue = RoleID,
                    DbType = System.Data.DbType.Int32,
                    Direction = ParameterDirection.Input
                };
                sqlParameters[0] = sqlParameter;
                SqlParameter sqlParameter1 = new SqlParameter
                {
                    ParameterName = "@UserID",
                    SqlValue = UserID,
                    DbType = System.Data.DbType.Int32,
                    Direction = ParameterDirection.Input
                };
                sqlParameters[1] = sqlParameter1;
                
                using (SqlConnection connection = new SqlConnection(GetConnectionString()))
                {                    
                    DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "rsp_GetEmpGMUModes", sqlParameters);
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        LogManager.WriteLog("Looping Data Set" + dr[1].ToString(), LogManager.enumLogLevel.Info);
                        oModes.Add(dr[0].ToString(), dr[1].ToString());
                    }
                    GetFlags(oModes, Modes);
                    strModes = String.Concat(Array.ConvertAll(Modes, x => x.ToString("X2")));
                }
                LogManager.WriteLog("Employee Flag GetIndividualModes:" + strModes, LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return strModes;
        }
        private byte[] GetFlags(IEnumerable<KeyValuePair<string, string>> SelectedModes, byte[] Modes)
        {

            foreach (KeyValuePair<string, string> keys in SelectedModes)
            {
                int SelectedMode = Convert.ToInt32(keys.Key);
                int ModeIndex = SelectedMode / 4;
                int BytePos = SelectedMode % 4;

                switch (keys.Value)
                {
                    case "":
                        {
                            /*Mode Disable*/
                            /*By default all bytes in the array are set to Zero(Mode Disable)*/
                            break;
                        }
                    case "W":
                        {
                            /*Write Only*/
                            Modes[ModeIndex] = Convert.ToByte(Modes[ModeIndex] | Convert.ToByte(0x80 >> ((BytePos * 2) + 1)));
                            break;
                        }
                    case "R":
                        {
                            /*Read Only*/
                            Modes[ModeIndex] = Convert.ToByte(Modes[ModeIndex] | Convert.ToByte(0x80 >> (BytePos * 2)));
                            break;
                        }
                    case "RW":
                        {
                            /*Read Write*/
                            Modes[ModeIndex] = Convert.ToByte(Modes[ModeIndex] | Convert.ToByte(0x80 >> (BytePos * 2)));
                            Modes[ModeIndex] = Convert.ToByte(Modes[ModeIndex] | Convert.ToByte(0x80 >> ((BytePos * 2) + 1)));
                            break;
                        }
                }
            }
            return Modes;
        }

        [WebMethod, SoapHeader("AuthenticationInfo")]
        [SoapSecurityExtension(Encrypt = EncryptMode.Response)]
        public bool ImportRole(string xmlString)
        {
            CheckSecurity();
            LogManager.WriteLog("Import User Role Called...", LogManager.enumLogLevel.Info);
            try
            {
                SqlParameter[] Params = new SqlParameter[1];
                Params[0] = new SqlParameter { ParameterName = "@Doc", Value = xmlString, Direction = ParameterDirection.Input };
                int retValue = DataBaseServiceHandler.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "usp_UpdateUserRoleLinkFromXML",
                    DataBaseServiceHandler.AddParameter<string>("@doc", DbType.Xml, xmlString));
                if (retValue >= 0)
                {
                    LogManager.WriteLog("Updated User Role Successfully...", LogManager.enumLogLevel.Info);
                    return true;
                }
                else
                {
                    LogManager.WriteLog("Unable to update User Role - SQL Error " + retValue.ToString(), LogManager.enumLogLevel.Info);
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
        public bool ImportRoleAccessLnk(string xmlString)
        {
            CheckSecurity();
            LogManager.WriteLog("ImportRoleAccessLink Called...", LogManager.enumLogLevel.Info);
            try
            {
                int retValue = DataBaseServiceHandler.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "usp_UpdateRoleAccessLnkFromXML",
                     DataBaseServiceHandler.AddParameter<string>("@doc", DbType.Xml, xmlString));
                if (retValue >= 0)
                {
                    LogManager.WriteLog("Updated RoleAccessLink Successfully...", LogManager.enumLogLevel.Info);
                    return true;
                }
                else
                {
                    LogManager.WriteLog("Unable to update Roleaccess link - SQL Error " + retValue.ToString(), LogManager.enumLogLevel.Info);
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
        public bool ImportData(string xmlString)
        {
            CheckSecurity();

            LogManager.WriteLog("ImportData Called...", LogManager.enumLogLevel.Info);

            var xmlDocument = new XmlDocument();
            bool bSuccess = false;

            try
            {
                xmlDocument.LoadXml(xmlString);
                if (xmlDocument.DocumentElement != null)
                {
                    var oSiteNodes = xmlDocument.DocumentElement.GetElementsByTagName("Site");
                    var siteCode = oSiteNodes.Item(0).ChildNodes[0].InnerText;
                    var xmlType = oSiteNodes.Item(0).ChildNodes[1].InnerText;
                    var ehid = oSiteNodes.Item(0).ChildNodes[2].InnerText;
                    var xmlFromSite = oSiteNodes.Item(0).ChildNodes[3].InnerXml;

                    var oEventParam = new SqlParameter[4];

                    var sqlParameter = new SqlParameter
                                           {
                                               ParameterName = "Site_Code",
                                               Value = siteCode,
                                               Direction = ParameterDirection.Input
                                           };
                    oEventParam[0] = sqlParameter;

                    sqlParameter = new SqlParameter
                                       {
                                           ParameterName = "Type",
                                           Value = xmlType,
                                           Direction = ParameterDirection.Input
                                       };
                    oEventParam[1] = sqlParameter;

                    sqlParameter = new SqlParameter
                                       {
                                           ParameterName = "SiteXML",
                                           Value = xmlFromSite,
                                           SqlDbType = SqlDbType.Text,
                                           Direction = ParameterDirection.Input
                                       };
                    oEventParam[2] = sqlParameter;

                    sqlParameter = new SqlParameter
                                       {
                                           ParameterName = "EH_ID",
                                           Value = ehid,
                                           SqlDbType = SqlDbType.Int,
                                           Direction = ParameterDirection.Input
                                       };
                    oEventParam[3] = sqlParameter;

                    int recordsInserted = SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "esp_Import_SiteXML", oEventParam);
                    if (recordsInserted > 0)
                        bSuccess = true;
                }
                LogManager.WriteLog("ImportData WS " + "Success value " + bSuccess, LogManager.enumLogLevel.Info);

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
        public bool InsertScheduleJobs(DataTable jobTable)
        {
            CheckSecurity();

            bool isSuccessfull = false;

            if ((jobTable != null) && (jobTable.Rows != null))
            {
                using (var sqlConnection = new SqlConnection(GetConnectionString()))
                {
                    sqlConnection.Open();
                    using (var sqlTransaction = sqlConnection.BeginTransaction())
                    {
                        try
                        {
                            SqlHelper.ExecuteNonQuery(sqlTransaction, CommandType.StoredProcedure, Constants.DeleteScheduleJob);
                            foreach (DataRow row in jobTable.Rows)
                            {
                                var sqlParameters = new SqlParameter[9];

                                sqlParameters[0] = new SqlParameter("@ProfileId", row["ProfileId"]);
                                sqlParameters[1] = new SqlParameter("@Name", row["Name"]);
                                sqlParameters[2] = new SqlParameter("@Description", row["Description"]);
                                sqlParameters[3] = new SqlParameter("@Frequency", row["Frequency"]);
                                sqlParameters[4] = new SqlParameter("@StartTime", row["StartTime"]);
                                sqlParameters[5] = new SqlParameter("@Days", row["Days"]);
                                sqlParameters[6] = new SqlParameter("@Interval", row["Interval"]);
                                sqlParameters[7] = new SqlParameter("@AssemblyName", row["AssemblyName"]);
                                sqlParameters[8] = new SqlParameter("@LastRun", DBNull.Value);

                                SqlHelper.ExecuteNonQuery(sqlTransaction, CommandType.StoredProcedure, Constants.InsertScheduleJob, sqlParameters);
                            }
                            sqlTransaction.Commit();
                            isSuccessfull = true;
                        }
                        catch (Exception ex)
                        {
                            sqlTransaction.Rollback();
                            ExceptionManager.Publish(ex);
                            isSuccessfull = false;
                        }
                    }
                }
            }

            return isSuccessfull;
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
        public bool ImportCompressedData(string compressedData)
        {
            CheckSecurity();

            string actualData = CompressionHelper.Decompress(compressedData);
            return ImportData(actualData);
        }

        [WebMethod, SoapHeader("AuthenticationInfo")]
        [SoapSecurityExtension(Encrypt = EncryptMode.Response)]
        public bool ImportCalendar(string xmlString)
        {
            CheckSecurity();
            bool bSuccess;
            try
            {
                var sqlParameters = new SqlParameter[2];

                var sqlParameter = new SqlParameter
                                       {
                                           ParameterName = "doc",
                                           Value = xmlString,
                                           Direction = ParameterDirection.Input
                                       };
                sqlParameters[0] = sqlParameter;

                sqlParameter = new SqlParameter
                                   {
                                       ParameterName = "IsSuccess",
                                       Direction = ParameterDirection.Output,
                                       SqlDbType = SqlDbType.Int
                                   };
                sqlParameters[1] = sqlParameter;

                SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "usp_ImportCalendarDetailsFromXML", sqlParameters);

                if (int.Parse(sqlParameters[1].Value.ToString()) == 0)
                {
                    bSuccess = true;
                    LogManager.WriteLog("ImportCalendar WS " + "  Success value " + bSuccess, LogManager.enumLogLevel.Info);
                }
                else
                {
                    bSuccess = false;
                    LogManager.WriteLog("ImportCalendar WS " + "  failed due to " + sqlParameters[1].Value, LogManager.enumLogLevel.Info);
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
        public bool ImportSite(string xml)
        {
            CheckSecurity();
            bool bSuccess;

            try
            {
                var sqlParameters = new SqlParameter[2];

                var sqlParameter = new SqlParameter
                                       {
                                           ParameterName = "doc",
                                           Value = xml,
                                           Direction = ParameterDirection.Input
                                       };
                sqlParameters[0] = sqlParameter;

                sqlParameter = new SqlParameter
                                   {
                                       ParameterName = "IsSuccess",
                                       Direction = ParameterDirection.Output,
                                       SqlDbType = SqlDbType.Int
                                   };
                sqlParameters[1] = sqlParameter;

                SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "usp_ImportSiteDetailsFromXML", sqlParameters);

                if (int.Parse(sqlParameters[1].Value.ToString()) == 0)
                {
                    bSuccess = true;
                    LogManager.WriteLog("ImportSite WS " + "  Success value " + bSuccess, LogManager.enumLogLevel.Info);
                }
                else
                {
                    bSuccess = false;
                    LogManager.WriteLog("ImportSite WS " + "  failed due to " + sqlParameters[1].Value, LogManager.enumLogLevel.Info);
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
        public bool ImportCrossTicketing(string xml)
        {
            CheckSecurity();
            bool bSuccess;

            try
            {
                var sqlParameters = new SqlParameter[2];

                var sqlParameter = new SqlParameter
                {
                    ParameterName = "doc",
                    Value = xml,
                    Direction = ParameterDirection.Input
                };
                sqlParameters[0] = sqlParameter;

                sqlParameter = new SqlParameter
                {
                    ParameterName = "IsSuccess",
                    Direction = ParameterDirection.Output,
                    SqlDbType = SqlDbType.Int
                };
                sqlParameters[1] = sqlParameter;

                SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "usp_ImportCrossTicketingDetailsFromXML", sqlParameters);

                if (int.Parse(sqlParameters[1].Value.ToString()) == 0)
                {
                    bSuccess = true;
                    LogManager.WriteLog("ImportCrossTicketing WS " + "  Success value " + bSuccess, LogManager.enumLogLevel.Info);
                }
                else
                {
                    bSuccess = false;
                    LogManager.WriteLog("ImportCrossTicketing WS " + "  failed due to " + sqlParameters[1].Value, LogManager.enumLogLevel.Info);
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
        public bool ImportModel(string xmlString)
        {
            CheckSecurity();
            bool bSuccess;
            try
            {
                var sqlParameters = new SqlParameter[2];
                var sqlParameter = new SqlParameter
                                       {
                                           ParameterName = "doc",
                                           Value = xmlString,
                                           Direction = ParameterDirection.Input
                                       };
                sqlParameters[0] = sqlParameter;
                sqlParameter = new SqlParameter
                                   {
                                       ParameterName = "IsSuccess",
                                       Direction = ParameterDirection.Output,
                                       SqlDbType = SqlDbType.Int
                                   };
                sqlParameters[1] = sqlParameter;

                SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "usp_ImportModelDetails", sqlParameters);

                if (int.Parse(sqlParameters[1].Value.ToString()) == 0)
                {
                    bSuccess = true;
                    LogManager.WriteLog("ImportSite WS " + "  Success value " + bSuccess, LogManager.enumLogLevel.Info);
                }
                else
                {
                    bSuccess = false;
                    LogManager.WriteLog("ImportSite WS " + "  failed due to " + sqlParameters[1].Value, LogManager.enumLogLevel.Info);
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
        public void UpdateBarPositionCentralStatus(string strBarposName, bool bStatus)
        {
            CheckSecurity();

            var sqlParameters = new SqlParameter[2];
            try
            {
                var sqlParameter = new SqlParameter
                                       {
                                           ParameterName = "BarPosName",
                                           Value = strBarposName,
                                           Direction = ParameterDirection.Input
                                       };
                sqlParameters[0] = sqlParameter;

                sqlParameter = new SqlParameter
                                   {
                                       ParameterName = "Status",
                                       Value = bStatus,
                                       SqlDbType = SqlDbType.Bit,
                                       Direction = ParameterDirection.Input
                                   };
                sqlParameters[1] = sqlParameter;

                SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "usp_UpdateBarPositionCentralStatus", sqlParameters);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog(ex.ToString(), LogManager.enumLogLevel.Error);
            }

        }

        [WebMethod, SoapHeader("AuthenticationInfo")]
        [SoapSecurityExtension(Encrypt = EncryptMode.Response)]
        public bool RequestCollectionByDate(string strCollectionByDateDetails, string strSiteCode)
        {
            CheckSecurity();

            bool bSuccess;
            try
            {
                string[] strArrCollectionByDate = strCollectionByDateDetails.Trim().Split(',');
                var sqlParameters = new SqlParameter[4];
                string collectionByDate = strArrCollectionByDate[0].Trim();

                int barPosStartIndex = strArrCollectionByDate[1].Trim().IndexOf(']');
                int barPosEndIndex = strArrCollectionByDate[1].Trim().Length;
                string strBarPos = barPosEndIndex > barPosStartIndex ? strArrCollectionByDate[1].Trim().Substring(barPosStartIndex + 1, (barPosEndIndex - barPosStartIndex - 1)) : "";
                LogManager.WriteLog("ImportCollectionByDate WS " + "  Bar Pos is - " + strBarPos, LogManager.enumLogLevel.Info);

                var sqlParameter = new SqlParameter
                                               {
                                                   ParameterName = "CollectionByDate",
                                                   Value = collectionByDate.Trim(),
                                                   Direction = ParameterDirection.Input
                                               };
                sqlParameters[0] = sqlParameter;

                sqlParameter = new SqlParameter
                                   {
                                       ParameterName = "Site_Code",
                                       Value = strSiteCode.Trim(),
                                       Direction = ParameterDirection.Input
                                   };
                sqlParameters[1] = sqlParameter;

                sqlParameter = new SqlParameter
                                   {
                                       ParameterName = "Bar_Pos",
                                       Value = strBarPos.Trim(),
                                       Direction = ParameterDirection.Input
                                   };
                sqlParameters[2] = sqlParameter;

                sqlParameter = new SqlParameter
                                   {
                                       ParameterName = "IsSuccess",
                                       Direction = ParameterDirection.Output,
                                       SqlDbType = SqlDbType.Int
                                   };
                sqlParameters[3] = sqlParameter;

                SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, Constants.CONSTANT_USP_UPDATECOLLECTIONBYDATEDETAILSEXPORTHISTORY, sqlParameters);

                if (int.Parse(sqlParameters[3].Value.ToString()) == 0)
                {
                    bSuccess = true;
                    LogManager.WriteLog("ImportCollectionByDate WS " + "  Success value " + bSuccess, LogManager.enumLogLevel.Info);
                }
                else
                {
                    bSuccess = false;
                    LogManager.WriteLog("ImportCollectionByDate WS " + "  failed due to " + sqlParameters[3].Value, LogManager.enumLogLevel.Info);
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
        public bool ImportSiteSettings(string xmlString)
        {
            LogManager.WriteLog("ImportSiteSettings called", LogManager.enumLogLevel.Info);
            CheckSecurity();
            bool bSuccess;
            int retValue;
            try
            {
                retValue = DataBaseServiceHandler.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "usp_ImportSiteSettingsFromXML",
                    DataBaseServiceHandler.AddParameter<string>("@doc", DbType.Xml, xmlString));

                if (retValue >= 0)
                {
                    bSuccess = true;
                    LogManager.WriteLog("ImportSiteSetting WS " + "  Success value " + bSuccess, LogManager.enumLogLevel.Info);
                }
                else
                {
                    bSuccess = false;
                    LogManager.WriteLog("ImportSiteSetting WS " + "  failed due to " + retValue.ToString(), LogManager.enumLogLevel.Info);
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
        public string GetSiteStatus()
        {
            CheckSecurity();

            try
            {
                string strServiceNames = GetValueFromSetting("ServiceNames", "BGSExchangeHost");
                var objMonitoring = new BMCMonitoring();
                return objMonitoring.GetSiteStatus(strServiceNames, 10);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error in getting stats from site " + "--" + ex.Message, LogManager.enumLogLevel.Error);
                return "<OutputXML><ErrorMessage>no records found </ErrorMessage></OutputXML> ";
            }

        }

        [WebMethod, SoapHeader("AuthenticationInfo")]
        [SoapSecurityExtension(Encrypt = EncryptMode.Response)]
        public bool ImportAAMSConfigDetails(string xmlString)
        {
            LogManager.WriteLog("ImportAAMSConfigDetails called", LogManager.enumLogLevel.Info);
            CheckSecurity();
            bool bSuccess;

            try
            {
                var sqlParameters = new SqlParameter[2];

                var sqlParameter = new SqlParameter
                {
                    ParameterName = "@doc",
                    Value = xmlString,
                    Direction = ParameterDirection.Input
                };
                sqlParameters[0] = sqlParameter;

                sqlParameter = new SqlParameter
                {
                    ParameterName = "IsSuccess",
                    Direction = ParameterDirection.Output,
                    SqlDbType = SqlDbType.Int
                };
                sqlParameters[1] = sqlParameter;

                SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "usp_InsertAAMSConfigDetailsfromXML", sqlParameters);

                if (int.Parse(sqlParameters[1].Value.ToString()) == 0)
                {
                    bSuccess = true;
                    LogManager.WriteLog("ImportAAMSConfigDetails WS " + "  Success value " + bSuccess, LogManager.enumLogLevel.Info);
                }
                else
                {
                    bSuccess = false;
                    LogManager.WriteLog("ImportAAMSConfigDetails WS " + "  failed due to " + sqlParameters[1].Value.ToString(), LogManager.enumLogLevel.Info);
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
        public bool ImportGameInfoDetails(string xmlString)
        {
            LogManager.WriteLog("ImportGameInfoDetails called", LogManager.enumLogLevel.Info);
            CheckSecurity();
            bool bSuccess;

            try
            {
                var sqlParameters = new SqlParameter[1];

                var sqlParameter = new SqlParameter
                {
                    ParameterName = "@doc",
                    Value = xmlString,
                    Direction = ParameterDirection.Input
                };
                sqlParameters[0] = sqlParameter;



                SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "usp_UpdateInstallationGameInfoFromXML", sqlParameters);


                bSuccess = true;
                LogManager.WriteLog("ImportGameInfoDetails WS  Success", LogManager.enumLogLevel.Info);

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
        public bool ImportAFTInfoDetails(string xmlString)
        {
            LogManager.WriteLog("ImportAFTInfoDetails called", LogManager.enumLogLevel.Info);
            CheckSecurity();
            bool bSuccess;

            try
            {
                var sqlParameters = new SqlParameter[2];

                var sqlParameter = new SqlParameter
                {
                    ParameterName = "@doc",
                    Value = xmlString,
                    Direction = ParameterDirection.Input
                };
                sqlParameters[0] = sqlParameter;

                sqlParameter = new SqlParameter
                {
                    ParameterName = "@PollingStatus",
                    Value = 0,
                    Direction = ParameterDirection.Input
                };
                sqlParameters[1] = sqlParameter;



                SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "usp_InsertMachinePollingFromXML", sqlParameters);


                bSuccess = true;
                LogManager.WriteLog("ImportAFTInfoDetails WS  Success", LogManager.enumLogLevel.Info);

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
        public bool ImportMasterCardDetails(string xmlString)
        {
            LogManager.WriteLog("ImportMasterCardDetails called", LogManager.enumLogLevel.Info);
            CheckSecurity();
            bool bSuccess;

            try
            {
                var sqlParameters = new SqlParameter[2];

                var sqlParameter = new SqlParameter
                {
                    ParameterName = "@doc",
                    Value = xmlString,
                    Direction = ParameterDirection.Input
                };
                sqlParameters[0] = sqlParameter;

                sqlParameter = new SqlParameter
                {
                    ParameterName = "@PollingStatus",
                    Value = 0,
                    Direction = ParameterDirection.Input
                };
                sqlParameters[1] = sqlParameter;



                SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "usp_InsertMasterCardPollingFromXML", sqlParameters);


                bSuccess = true;
                LogManager.WriteLog("ImportMasterCardDetails WS  Success", LogManager.enumLogLevel.Info);

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
        public bool ImportAFTSettingsDetails(string xmlString)
        {
            LogManager.WriteLog("ImportAFTSettingsDetails called", LogManager.enumLogLevel.Info);
            CheckSecurity();
            bool bSuccess = false;

            try
            {
                var sqlParameters = new SqlParameter[2];

                var sqlParameter = new SqlParameter
                {
                    ParameterName = "@doc",
                    Value = xmlString,
                    Direction = ParameterDirection.Input
                };
                sqlParameters[0] = sqlParameter;

                sqlParameter = new SqlParameter
                {
                    ParameterName = "@IsSuccess",
                    Value = false,
                    Direction = ParameterDirection.Output
                };
                sqlParameters[1] = sqlParameter;

                SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "usp_ImportAFTSettingsFromXML", sqlParameters);

                if ((bool)sqlParameters[1].Value)
                {
                    bSuccess = true;
                    LogManager.WriteLog("ImportAFTSettingsDetails WS  Success", LogManager.enumLogLevel.Info);

                }
                else
                {
                    LogManager.WriteLog("Import failed - AFT Setting already exists for the denom", LogManager.enumLogLevel.Info);
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
        public bool ImportGameCappingParameters(string xmlString)
        {
            LogManager.WriteLog("ImportGameCappingDetails called", LogManager.enumLogLevel.Info);
            CheckSecurity();
            bool bSuccess = false;

            try
            {
                var sqlParameters = new SqlParameter[2];

                var sqlParameter = new SqlParameter
                {
                    ParameterName = "@doc",
                    Value = xmlString,
                    Direction = ParameterDirection.Input
                };
                sqlParameters[0] = sqlParameter;

                sqlParameter = new SqlParameter
                {
                    ParameterName = "@IsSuccess",
                    Value = false,
                    Direction = ParameterDirection.Output
                };
                sqlParameters[1] = sqlParameter;

                SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "usp_ImportGameCappingParametersFromXML", sqlParameters);
                bSuccess = true;
                LogManager.WriteLog("ImportGameCappingDetails WS  Success", LogManager.enumLogLevel.Info);

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
        public bool ImportExchangeData(string xmlString, string type)
        {
            CheckSecurity();
            string spName = string.Empty;

            try
            {
                LogManager.WriteLog(type.ToUpper(), LogManager.enumLogLevel.Info);
                switch (type.ToUpper())
                {
                    case "GAMELIBRARY":
                        spName = "usp_UpdateGameLibraryFromEnterprise";
                        break;
                    case "GAMECRC":
                        spName = "usp_UpdateGame_CRCFromEnterprise";
                        break;
                    case "REMOVECRC":
                        spName = "usp_RemoveCRC";
                        break;
                    case "ONDEMANDVERIFICATION":
                        LogManager.WriteLog("Inside Ondemand Verification", LogManager.enumLogLevel.Info);
                        var return_Value = DataBaseServiceHandler.AddParameter<int>("@RETURN_VALUE", DbType.Int32, 0, ParameterDirection.ReturnValue);
                        DataBaseServiceHandler.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "usp_InsertIHVerification",
                            DataBaseServiceHandler.AddParameter<string>("@doc", DbType.Xml, xmlString), return_Value);

                        if (Convert.ToInt32(return_Value.Value) == 1)
                            return true;
                        else
                            throw new Exception("Record was not inserted for ONDEMANDVERIFICATION, return value is " + return_Value.Value.ToString());
                    default:
                        break;
                }

                if (type.ToUpper() != "ONDEMANDVERIFICATION")
                {
                    DataBaseServiceHandler.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, spName,
                            DataBaseServiceHandler.AddParameter<string>("@doc", DbType.Xml, xmlString));
                }
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                return false;
            }
            return true;
        }

        [WebMethod, SoapHeader("AuthenticationInfo")]
        [SoapSecurityExtension(Encrypt = EncryptMode.Response)]
        public bool ImportPasswordChange(string xmlString)
        {
            LogManager.WriteLog("ImportPasswordChange called", LogManager.enumLogLevel.Info);
            CheckSecurity();
            bool bSuccess;

            try
            {
                var sqlParameters = new SqlParameter[1];

                var sqlParameter = new SqlParameter
                {
                    ParameterName = "@doc",
                    Value = xmlString,
                    Direction = ParameterDirection.Input
                };
                sqlParameters[0] = sqlParameter;



                SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "usp_UpdateUserInfoFromXML", sqlParameters);


                bSuccess = true;
                LogManager.WriteLog("ImportPasswordChange WS  Success", LogManager.enumLogLevel.Info);

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
        public bool ImportCodeMaster(string xmlString)
        {
            LogManager.WriteLog("ImportCodeMaster called", LogManager.enumLogLevel.Info);
            CheckSecurity();
            bool bSuccess;

            try
            {
                var sqlParameters = new SqlParameter[1];

                var sqlParameter = new SqlParameter
                {
                    ParameterName = "@doc",
                    Value = xmlString,
                    Direction = ParameterDirection.Input
                };
                sqlParameters[0] = sqlParameter;

                SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "usp_ImportCodeMaster", sqlParameters);

                bSuccess = true;
                LogManager.WriteLog("ImportCodeMaster Success", LogManager.enumLogLevel.Info);

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
        public bool ImportLookupMaster(string xmlString)
        {
            LogManager.WriteLog("ImportLookupMaster called", LogManager.enumLogLevel.Info);
            CheckSecurity();
            bool bSuccess;

            try
            {
                var sqlParameters = new SqlParameter[1];

                var sqlParameter = new SqlParameter
                {
                    ParameterName = "@doc",
                    Value = xmlString,
                    Direction = ParameterDirection.Input
                };
                sqlParameters[0] = sqlParameter;

                SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "usp_ImportLookupMaster", sqlParameters);

                bSuccess = true;
                LogManager.WriteLog("ImportLookupMaster Success", LogManager.enumLogLevel.Info);

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
        public bool ImportLanguageLookup(string xmlString)
        {
            LogManager.WriteLog("ImportLanguageLookup called", LogManager.enumLogLevel.Info);
            CheckSecurity();
            bool bSuccess;

            try
            {
                var sqlParameters = new SqlParameter[1];

                var sqlParameter = new SqlParameter
                {
                    ParameterName = "@doc",
                    Value = xmlString,
                    Direction = ParameterDirection.Input
                };
                sqlParameters[0] = sqlParameter;

                SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "usp_ImportLanguageLookup", sqlParameters);

                bSuccess = true;
                LogManager.WriteLog("ImportLanguageLookup Success", LogManager.enumLogLevel.Info);

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
        public bool ImportMachineCompDetails(string xmlString)
        {
            LogManager.WriteLog("ImportMachineCompDetails called", LogManager.enumLogLevel.Info);
            CheckSecurity();
            bool bSuccess;

            try
            {
                var sqlParameters = new SqlParameter[2];

                var sqlParameter = new SqlParameter
                {
                    ParameterName = "@doc",
                    Value = xmlString,
                    Direction = ParameterDirection.Input
                };
                sqlParameters[0] = sqlParameter;

                sqlParameter = new SqlParameter
                {
                    ParameterName = "@IsSuccess",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                };
                sqlParameters[1] = sqlParameter;

                SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "usp_InsertMachineComponentDetailsfromXML", sqlParameters);

                if (sqlParameters[1].Value.ToString() == "0")
                {
                    bSuccess = true;
                    LogManager.WriteLog("ImportMachineCompDetails WS Success.", LogManager.enumLogLevel.Info);
                }
                else
                {
                    bSuccess = false;
                    LogManager.WriteLog("ImportMachineCompDetails WS Failure. SP Returned Error -" + sqlParameters[1].Value.ToString(), LogManager.enumLogLevel.Info);
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
        public bool ImportComponentDetails(string xmlString)
        {
            LogManager.WriteLog("ImportComponentDetails called", LogManager.enumLogLevel.Info);
            CheckSecurity();
            bool bSuccess;

            try
            {
                var sqlParameters = new SqlParameter[2];

                var sqlParameter = new SqlParameter
                {
                    ParameterName = "@doc",
                    Value = xmlString,
                    Direction = ParameterDirection.Input
                };
                sqlParameters[0] = sqlParameter;

                sqlParameter = new SqlParameter
                {
                    ParameterName = "@IsSuccess",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                };
                sqlParameters[1] = sqlParameter;

                SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "usp_InsertComponentDetailsfromXML", sqlParameters);

                if (sqlParameters[1].Value.ToString() == "0")
                {
                    bSuccess = true;
                    LogManager.WriteLog("ImportComponentDetails WS Success.", LogManager.enumLogLevel.Info);
                }
                else
                {
                    bSuccess = false;
                    LogManager.WriteLog("ImportComponentDetails WS Failure. SP Returned Error -" + sqlParameters[1].Value.ToString(), LogManager.enumLogLevel.Info);
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
        public bool ImportComponentVerificationDetails(string xmlString)
        {
            LogManager.WriteLog("ImportComponentVerificationDetails called", LogManager.enumLogLevel.Info);
            CheckSecurity();
            bool bSuccess;

            try
            {
                var sqlParameters = new SqlParameter[2];

                var sqlParameter = new SqlParameter
                {
                    ParameterName = "@doc",
                    Value = xmlString,
                    Direction = ParameterDirection.Input
                };
                sqlParameters[0] = sqlParameter;

                sqlParameter = new SqlParameter
                {
                    ParameterName = "@IsSuccess",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                };
                sqlParameters[1] = sqlParameter;

                SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "usp_InsertCompVerificationDetailsfromXML", sqlParameters);

                if (sqlParameters[1].Value.ToString() == "0")
                {
                    bSuccess = true;
                    LogManager.WriteLog("ImportComponentVerificationDetails WS Success.", LogManager.enumLogLevel.Info);
                }
                else
                {
                    bSuccess = false;
                    LogManager.WriteLog("ImportComponentVerificationDetails WS Failure. SP Returned Error -" + sqlParameters[1].Value.ToString(), LogManager.enumLogLevel.Info);
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
        public int GetMachineComponentStatus(string strSerialNo, int iCompTypeID)
        {
            LogManager.WriteLog("GetMachineComponentStatus called", LogManager.enumLogLevel.Info);
            CheckSecurity();
            int iSuccess = 5;

            try
            {
                var sqlParameters = new SqlParameter[3];

                var sqlParameter = new SqlParameter
                {
                    ParameterName = "@SerialNo",
                    Value = strSerialNo,
                    Direction = ParameterDirection.Input
                };
                sqlParameters[0] = sqlParameter;

                sqlParameter = new SqlParameter
                {
                    ParameterName = "@CompTypeID",
                    Value = iCompTypeID,
                    Direction = ParameterDirection.Input
                };
                sqlParameters[1] = sqlParameter;

                sqlParameter = new SqlParameter
                {
                    ParameterName = "@IsSucess",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                };
                sqlParameters[2] = sqlParameter;

                //Check the 
                SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "rsp_GetMachineComponentStatus", sqlParameters);

                switch ((int)sqlParameters[2].Value)
                {
                    case 0:
                        iSuccess = 0;
                        LogManager.WriteLog("GetMachineComponentStatus - WS Success.", LogManager.enumLogLevel.Info);
                        break;
                    case 1:
                        iSuccess = 1;
                        LogManager.WriteLog("GetMachineComponentStatus - Failed - Invalid Serial No.", LogManager.enumLogLevel.Info);
                        break;
                    case 2:
                        iSuccess = 2;
                        LogManager.WriteLog("GetMachineComponentStatus - Failed - Invalid Component Type.", LogManager.enumLogLevel.Info);
                        break;
                    case 3:
                        iSuccess = 3;
                        LogManager.WriteLog("GetMachineComponentStatus - Failed - Machine and Component Type is not linked.", LogManager.enumLogLevel.Info);
                        break;
                    case 4:
                        iSuccess = 4;
                        LogManager.WriteLog("GetMachineComponentStatus - Failed - Component verification is in progress.", LogManager.enumLogLevel.Info);
                        break;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                iSuccess = 5;
            }
            return iSuccess;
        }

        [WebMethod, SoapHeader("AuthenticationInfo")]
        [SoapSecurityExtension(Encrypt = EncryptMode.Response)]
        public bool ImportOnDemandVerificationDetails(string strSerialNo, int iCompTypeID)
        {
            LogManager.WriteLog(string.Format("ImportOnDemandVerificationDetails called SerialNo {0} ,CompId {1}", strSerialNo, iCompTypeID), LogManager.enumLogLevel.Info);
            CheckSecurity();
            var bReturn = false;
            string strSerial = string.Empty;
            int iVerType = 0;

            try
            {
                var sqlParameters = new SqlParameter[3];

                string[] args = strSerialNo.Split(new char[] { ':' });
                Int32.TryParse(args[0], out iVerType);
                strSerial = args[1];

                var sqlParameter = new SqlParameter
                {
                    ParameterName = "@SerialNo",
                    Value = strSerial,
                    Direction = ParameterDirection.Input
                };
                sqlParameters[0] = sqlParameter;

                sqlParameter = new SqlParameter
                {
                    ParameterName = "@CompTypeID",
                    Value = iCompTypeID,
                    Direction = ParameterDirection.Input
                };
                sqlParameters[1] = sqlParameter;

                sqlParameter = new SqlParameter
                {
                    ParameterName = "@VerType",
                    Value = iVerType,
                    Direction = ParameterDirection.Input
                };
                sqlParameters[2] = sqlParameter;

                SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "usp_UpdateOnDemandVerificationData", sqlParameters);

                bReturn = true;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                bReturn = false;
            }
            return bReturn;

        }

        [WebMethod, SoapHeader("AuthenticationInfo")]
        [SoapSecurityExtension(Encrypt = EncryptMode.Response)]
        public bool ImportMachineUpdateDetails(string xmlString)
        {
            LogManager.WriteLog("ImportMachineUpdateDetails called", LogManager.enumLogLevel.Info);
            CheckSecurity();
            bool bSuccess;

            try
            {
                var sqlParameters = new SqlParameter[2];

                var sqlParameter = new SqlParameter
                {
                    ParameterName = "@doc",
                    Value = xmlString,
                    Direction = ParameterDirection.Input
                };
                sqlParameters[0] = sqlParameter;

                sqlParameter = new SqlParameter
                {
                    ParameterName = "@IsSuccess",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                };
                sqlParameters[1] = sqlParameter;

                SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "usp_UpdateMachineDetailsfromXML", sqlParameters);

                if (sqlParameters[1].Value.ToString() == "0")
                {
                    bSuccess = true;
                    LogManager.WriteLog("ImportMachineUpdateDetails WS Success.", LogManager.enumLogLevel.Info);
                }
                else
                {
                    bSuccess = false;
                    LogManager.WriteLog("ImportMachineUpdateDetails WS Failure. SP Returned Error -" + sqlParameters[1].Value.ToString(), LogManager.enumLogLevel.Info);
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
        public bool ImportCMPGameTypes(string xmlString)
        {
            LogManager.WriteLog("ImportCMPGameTypes called", LogManager.enumLogLevel.Info);
            CheckSecurity();
            bool bSuccess;

            try
            {
                var sqlParameters = new SqlParameter[2];

                var sqlParameter = new SqlParameter
                {
                    ParameterName = "@doc",
                    Value = xmlString,
                    Direction = ParameterDirection.Input
                };
                sqlParameters[0] = sqlParameter;

                sqlParameter = new SqlParameter
                {
                    ParameterName = "@IsSuccess",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                };
                sqlParameters[1] = sqlParameter;

                SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "usp_ImportCMPGameTypes", sqlParameters);

                if (sqlParameters[1].Value.ToString() == "0")
                {
                    bSuccess = true;
                    LogManager.WriteLog("ImportCMPGameTypes WS Success.", LogManager.enumLogLevel.Info);
                }
                else
                {
                    bSuccess = false;
                    LogManager.WriteLog("ImportCMPGameTypes WS Failure. SP Returned Error -" + sqlParameters[1].Value.ToString(), LogManager.enumLogLevel.Info);
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
        public bool UpdateSiteEnabledStatus(string xmlString)
        {
            LogManager.WriteLog("Inside UpdateSiteEnabledStatus", LogManager.enumLogLevel.Info);

            CheckSecurity();

            bool bSuccess;
            int retValue;

            try
            {
                retValue = DataBaseServiceHandler.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "usp_UpdateSiteEnabledStatus",
                    DataBaseServiceHandler.AddParameter<string>("@xmlDoc", DbType.Xml, xmlString));

                if (retValue >= 0)
                {
                    bSuccess = true;
                    LogManager.WriteLog("UpdateSiteEnabledStatus WS " + "  Success value " + bSuccess, LogManager.enumLogLevel.Info);
                }
                else
                {
                    bSuccess = false;
                    LogManager.WriteLog("UpdateSiteEnabledStatus WS " + "  failed due to " + retValue.ToString(), LogManager.enumLogLevel.Info);
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
        public bool ImportVaultDetails(string xmlString)
        {
            LogManager.WriteLog("Inside ImportVaultDetails", LogManager.enumLogLevel.Info);

            CheckSecurity();

            bool bSuccess;
            int retValue;

            try
            {
                retValue = DataBaseServiceHandler.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "usp_Vault_ImportDeviceDetailsFromXML",
                    DataBaseServiceHandler.AddParameter<string>("@Data", DbType.Xml, xmlString));

                if (retValue >= 0)
                {
                    bSuccess = true;
                    LogManager.WriteLog("ImportVaultDetails WS " + "  Success value " + bSuccess, LogManager.enumLogLevel.Info);
                }
                else
                {
                    bSuccess = false;
                    LogManager.WriteLog("ImportVaultDetails WS " + "  failed due to " + retValue.ToString(), LogManager.enumLogLevel.Info);
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
        public bool ImportVaultDrop(string xmlString)
        {
            //Check security
            CheckSecurity();

            LogManager.WriteLog("ImportVaultDrop Called...", LogManager.enumLogLevel.Info);

            try
            {
                LogManager.WriteLog(xmlString, LogManager.enumLogLevel.Info);

                DataBaseServiceHandler.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "usp_Vault_ImportDrop",
                DataBaseServiceHandler.AddParameter<string>("xml", DbType.Xml, xmlString));

                LogManager.WriteLog("ImportVaultDrop stored", LogManager.enumLogLevel.Info);

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
        public bool ImportVaultTransactionReason(string xmlString)
        {
            //Check security
            CheckSecurity();

            LogManager.WriteLog("ImportVaultTransactionReason Called...", LogManager.enumLogLevel.Info);

            try
            {
                LogManager.WriteLog(xmlString, LogManager.enumLogLevel.Info);

                DataBaseServiceHandler.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "usp_Vault_ImportTransactionReason",
                DataBaseServiceHandler.AddParameter<string>("xml", DbType.Xml, xmlString));

                LogManager.WriteLog("ImportVaultTransactionReason stored", LogManager.enumLogLevel.Info);

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
        public bool ImportVaultTerminateDetails(string xmlString)
        {
            CheckSecurity();
            bool bSuccess;

            LogManager.WriteLog("ImportVaultTerminateDetails Called...", LogManager.enumLogLevel.Info);

            try
            {
                var sqlParameters = new SqlParameter[2];

                var sqlParameter = new SqlParameter
                {
                    ParameterName = "@TerminationXML",
                    Value = xmlString,
                    Direction = ParameterDirection.Input
                };
                sqlParameters[0] = sqlParameter;

                sqlParameter = new SqlParameter
                {
                    ParameterName = "@Status",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                };
                sqlParameters[1] = sqlParameter;

                SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "usp_Vault_ImportTerminationDetails", sqlParameters);

                if (sqlParameters[1].Value.ToString() == "0")
                {
                    bSuccess = true;
                    LogManager.WriteLog("ImportVaultTerminateDetails WS " + "  Success value " + bSuccess, LogManager.enumLogLevel.Info);
                }
                else
                {
                    bSuccess = false;
                    LogManager.WriteLog("ImportVaultTerminateDetails WS " + "  failed due to " + sqlParameters[1].Value.ToString(), LogManager.enumLogLevel.Info);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return false;
            }
            return bSuccess;
        }

        [WebMethod, SoapHeader("AuthenticationInfo")]
        [SoapSecurityExtension(Encrypt = EncryptMode.Response)]
        public bool ImportSiteLicensing(string xmlString)
        {
            LogManager.WriteLog("Inside Import Site Licensing", LogManager.enumLogLevel.Info);
            bool bSuccess = false;
            try
            {
                CheckSecurity();
                try
                {
                    var returnValue = DataBaseServiceHandler.AddParameter<int>("@ReturnValue", DbType.Int32, 0, ParameterDirection.ReturnValue);

                    DataBaseServiceHandler.ExecuteNonQuery(GetConnectionString(),
                                                                   CommandType.StoredProcedure,
                                                                   "usp_SL_ImportSiteLicensingFromXML",
                                                                   DataBaseServiceHandler.AddParameter<string>("@doc", DbType.Xml, xmlString),
                                                                   returnValue);

                    if (int.Parse(returnValue.Value.ToString()) == 0)
                    {
                        bSuccess = true;
                        LogManager.WriteLog("Site Licensing information updated in DB " + "  Success value " + returnValue,
                                        LogManager.enumLogLevel.Info);
                    }
                    else
                    {
                        LogManager.WriteLog("Unable to update Site Licensing information - SQL Error " + returnValue.ToString(), LogManager.enumLogLevel.Info);
                    }
                }
                catch (Exception ex1)
                {
                    LogManager.WriteLog("Exception Occured in Inserting site licensing to DB Exception = " + ex1.Message, LogManager.enumLogLevel.Error);
                    LogManager.WriteLog(ex1.Message, LogManager.enumLogLevel.Error);
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Exception Occured Exception = " + ex.Message, LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
            }
            return bSuccess;
        }
        [WebMethod, SoapHeader("AuthenticationInfo")]
        [SoapSecurityExtension(Encrypt = EncryptMode.Response)]
        public bool ImportActiveLicensing(string xmlString)
        {
            LogManager.WriteLog("Inside ImportActiveLicensing", LogManager.enumLogLevel.Info);
            bool bSuccess = false;
            try
            {
                CheckSecurity();
                try
                {
                    var returnValue = DataBaseServiceHandler.AddParameter<int>("@ReturnValue", DbType.Int32, 0, ParameterDirection.ReturnValue);

                    DataBaseServiceHandler.ExecuteNonQuery(GetConnectionString(),
                                                                   CommandType.StoredProcedure,
                                                                   "usp_SL_ImportActiveSiteLicensingFromXML",
                                                                   DataBaseServiceHandler.AddParameter<string>("@doc", DbType.Xml, xmlString),
                                                                   returnValue);

                    if (int.Parse(returnValue.Value.ToString()) == 0)
                    {
                        bSuccess = true;
                        LogManager.WriteLog("Site Active Licensing information updated in DB " + "  Success value " + returnValue,
                                        LogManager.enumLogLevel.Info);
                    }
                    else
                    {
                        LogManager.WriteLog("Unable to update Site Active Licensing information - SQL Error " + returnValue.ToString(), LogManager.enumLogLevel.Info);
                    }
                }
                catch (Exception ex1)
                {
                    LogManager.WriteLog("Exception Occured in Inserting site Active licensing to DB Exception = " + ex1.Message, LogManager.enumLogLevel.Error);
                    LogManager.WriteLog(ex1.Message, LogManager.enumLogLevel.Error);
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Exception Occured Exception = " + ex.Message, LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
            }
            return bSuccess;
        }
        [WebMethod, SoapHeader("AuthenticationInfo")]
        [SoapSecurityExtension(Encrypt = EncryptMode.Response)]
        public bool ImportRoute(string xmlString)
        {
            CheckSecurity();
            LogManager.WriteLog("Import Route Called...", LogManager.enumLogLevel.Info);
            try
            {
                int retValue = DataBaseServiceHandler.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "usp_CRMUpdateRouteFromXML",
                     DataBaseServiceHandler.AddParameter<string>("@RouteXMl", DbType.Xml, xmlString));
                if (retValue >= 0)
                {
                    LogManager.WriteLog("Updated Route Successfully...", LogManager.enumLogLevel.Info);
                    return true;
                }
                else
                {
                    LogManager.WriteLog("Unable to update Route details - SQL Error " + retValue.ToString(), LogManager.enumLogLevel.Info);
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
        public bool ImportEmpGMUEvents(string xmlString)
        {
            CheckSecurity();
            LogManager.WriteLog("Import EmpGMUEvents Called...", LogManager.enumLogLevel.Info);
            try
            {
                LogManager.WriteLog("Import EmpGMUEvents XML..." + xmlString, LogManager.enumLogLevel.Info);
                int retValue = DataBaseServiceHandler.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "usp_ImportEmpGMUEvent",
                     DataBaseServiceHandler.AddParameter<string>("@EmpEventDetails", DbType.String, xmlString));

                LogManager.WriteLog("RetVal:" + retValue,LogManager.enumLogLevel.Info);
                if (retValue != -99)
                {
                    LogManager.WriteLog("Updated EmpGMUEvents Successfully...", LogManager.enumLogLevel.Info);
                    return true;
                }
                else
                {
                    LogManager.WriteLog("Unable to update EmpGMUEvents details - SQL Error " + retValue.ToString(), LogManager.enumLogLevel.Info);
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
        public bool ImportEmpGMUModes(string xmlString)
        {
            CheckSecurity();
            bool bSuccess = true; ;
            try
            {
                var sqlParameters = new SqlParameter[2];

                var sqlParameter = new SqlParameter
                {
                    ParameterName = "@XML",
                    Value = xmlString,
                    Direction = ParameterDirection.Input
                };
                sqlParameters[0] = sqlParameter;

                sqlParameter = new SqlParameter
                {
                    ParameterName = "@IsSuccess",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                };
                sqlParameters[1] = sqlParameter;

                SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "usp_ImportEmpGMUModes", sqlParameters);

                if (sqlParameters[1].Value.ToString() == "0")
                {
                    bSuccess = true;
                    LogManager.WriteLog("ImportEmpGMUModes WS " + "  Success value " + bSuccess, LogManager.enumLogLevel.Info);
                    byte[] Modes = new byte[64];
                    XmlDocument xdoc = new XmlDocument();
                    xdoc.LoadXml(xmlString);
                    XmlNodeList node = xdoc.SelectNodes("//Role[@ID]");
                    List<int> RoleList = new List<int>();
                    foreach (XmlNode xmlnode in node)
                    {
                        RoleList.Add(Convert.ToInt32(xmlnode.Attributes[0].Value));
                    }
                    foreach (int RoleID in RoleList)
                    {
                        string strModes = GetIndividualModes(RoleID, 0, Modes);
                        var sqlParameters1 = new SqlParameter[2];
                        var sqlParameter1 = new SqlParameter
                        {
                            ParameterName = "@RoleID",
                            Value = RoleID,
                            Direction = ParameterDirection.Input
                        };
                        sqlParameters1[0] = sqlParameter1;

                        sqlParameter1 = new SqlParameter
                        {
                            ParameterName = "@Flag",
                            Value = strModes,
                            Direction = ParameterDirection.Input
                        };
                        sqlParameters1[1] = sqlParameter1;
                        SqlHelper.ExecuteReader(GetConnectionString(), CommandType.StoredProcedure, "usp_UpdateEmployeeFlags", sqlParameters1);
                        LogManager.WriteLog("Employee Flags:" + strModes + "Updated Successfully for Card:" + RoleID, LogManager.enumLogLevel.Info);
                    }
                }
                else
                {
                    bSuccess = false;
                    LogManager.WriteLog("ImportEmpGMUModes WS " + "failed due to " + sqlParameters[1].Value.ToString(), LogManager.enumLogLevel.Info);
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
        public bool UpdateHQLiquidationID(int iHQLiquidationId, int iLiquidationId)
        {
            CheckSecurity();
            LogManager.WriteLog("UpdateHQLiquidationID Called... iHQLiquidationID:" + iHQLiquidationId.ToString() + " iLiquidationID: " + iLiquidationId.ToString(), LogManager.enumLogLevel.Info);
            try
            {
                var sqlParameters = new SqlParameter[2];

                var sqlParameter = new SqlParameter
                {
                    ParameterName = "@HQLiquidationID",
                    Value = iHQLiquidationId,
                    Direction = ParameterDirection.Input
                };
                sqlParameters[0] = sqlParameter;

                sqlParameter = new SqlParameter
                {
                    ParameterName = "@LiquidationID",
                    Value = iLiquidationId,
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Input
                };
                sqlParameters[1] = sqlParameter;

                int retValue = DataBaseServiceHandler.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "usp_UpdateHQLiquidationDetail", sqlParameters);
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
        public bool UpdateHQLiquidationShareID(int iHQLiquidationShareId, int iLiquidationShareId)
        {
            CheckSecurity();
            LogManager.WriteLog("UpdateHQLiquidationShareID Called... iHQLiquidationShareID" + iHQLiquidationShareId.ToString() + " iLiquidationShareID" + iLiquidationShareId.ToString(), LogManager.enumLogLevel.Info);
            try
            {
                var sqlParameters = new SqlParameter[2];

                var sqlParameter = new SqlParameter
                {
                    ParameterName = "@HQLiquidationShareID",
                    Value = iHQLiquidationShareId,
                    Direction = ParameterDirection.Input
                };
                sqlParameters[0] = sqlParameter;

                sqlParameter = new SqlParameter
                {
                    ParameterName = "@LiquidationShareID",
                    Value = iLiquidationShareId,
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Input
                };
                sqlParameters[1] = sqlParameter;

                int retValue = DataBaseServiceHandler.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "usp_UpdateHQLiquidationShareDetail", sqlParameters);
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

        #region RebootGMU
        [WebMethod]
        public string RebootGMU(string assetNames)
        {
            LogManager.WriteLog("Start of RebootMultipleGMU ", LogManager.enumLogLevel.Debug);
            string result = string.Empty;
            try
            {
                RebootGMUTask rebootGMUTask = new RebootGMUTask(GetConnectionString());
                result = rebootGMUTask.RebootMultipleGMU(assetNames);

                // Comment above line and Use line below for testing
                //result = rebootGMUTask.RebootMultipleGMUTest(assetNames);

                LogManager.WriteLog("RebootGMU Result:" + result, LogManager.enumLogLevel.Debug);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            LogManager.WriteLog("End of RebootMultipleGMU - for Asset Name(s):" + assetNames, LogManager.enumLogLevel.Debug);

            return (result);
        }
        #endregion
        
        [WebMethod, SoapHeader("AuthenticationInfo")]
        [SoapSecurityExtension(Encrypt = EncryptMode.Response)]
        public bool UpdateFactoryResetStatus(string xmlString)
        {
            CheckSecurity();
            LogManager.WriteLog("Update Factory Reset Status Called...", LogManager.enumLogLevel.Info);
            try
            {
                int retValue = DataBaseServiceHandler.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "usp_UpdateFactoryResetStatus",
                     DataBaseServiceHandler.AddParameter<string>("@FRStatusXMl", DbType.String, xmlString));
                if (retValue >= 0)
                {
                    LogManager.WriteLog("Updated factory reset status Successfully...", LogManager.enumLogLevel.Info);
                    return true;
                }
                else
                {
                    LogManager.WriteLog("Unable to update factory reset status - SQL Error " + retValue.ToString(), LogManager.enumLogLevel.Info);
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
        public bool ImportMailAlertList(string xmlString)
        {
            CheckSecurity();
            LogManager.WriteLog("Importing Mail Alert List...", LogManager.enumLogLevel.Info);
            try
            {
                int retValue = DataBaseServiceHandler.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "usp_ImportMailAlertListFromXML",
                     DataBaseServiceHandler.AddParameter<string>("@doc", DbType.String, xmlString));

                LogManager.WriteLog("Importing Mail Alert List Successfull...", LogManager.enumLogLevel.Info);
                return true;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog(ex.Message, LogManager.enumLogLevel.Error);
                LogManager.WriteLog("Importing Mail Alert List - Failed", LogManager.enumLogLevel.Info);
                return false;
            }
        }


        [WebMethod, SoapHeader("AuthenticationInfo")]
        [SoapSecurityExtension(Encrypt = EncryptMode.Response)]
        public bool ImportMailServerInfo(string xmlString)
        {
            CheckSecurity();
            LogManager.WriteLog("Importing Mail Server Details...", LogManager.enumLogLevel.Info);
            try
            {
                int retValue = DataBaseServiceHandler.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "usp_ImportMailServerInfoFromXML",
                     DataBaseServiceHandler.AddParameter<string>("@doc", DbType.String, xmlString));

                LogManager.WriteLog("Importing  Mail Server Info  Successfull...", LogManager.enumLogLevel.Info);
                return true;

            }
            catch (Exception ex)
            {
                LogManager.WriteLog(ex.Message, LogManager.enumLogLevel.Error);
                LogManager.WriteLog("Importing Mail Server Info - Failed", LogManager.enumLogLevel.Info);
                return false;
            }
        }

        #endregion
    }    
}
