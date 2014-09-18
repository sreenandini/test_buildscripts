using System;
using System.Data;
using System.Collections.Generic;
using System.Configuration;
using System.Xml;
using Microsoft.Win32;
using BMC.DBInterface.CashDeskOperator;
using BMC.Common.ExceptionManagement;
using BMC.Common.LogManagement;
using BMC.Monitoring;
using BMC.Business.CashDeskOperator.WebServices;
using BMC.Transport.CashDeskOperatorEntity;
using System.Text;
using BMC.Common.ConfigurationManagement;
using BMC.Common.Utilities;
namespace BMC.Business.CashDeskOperator
{
   
  public  class FactoryResetBusiness
    {
      
        #region "Private Variables"
            Proxy oWebService=null;
            FactoryResetDataAccess FactoryResetDataAccess = new FactoryResetDataAccess();
            BMCMonitoring oBMCMonitoring = new BMCMonitoring();
            private string strServiceStatus = string.Empty;
        #endregion                                   

        #region "Private Functions"            
        
            private int CheckAuthorizationCodeFromServer(string iAuthCode, int iSiteCode, string TransactionType)
            {
                string sReturnValue = "";
                string sVerifyURL = string.Empty;
                int iResult=-1;
                try
                {
                    if (oWebService == null)
                    {                        
                        ConfigManager.SetConfigurationMode(ConfigManager.ConfigurationMode.AppConfig);
                        sVerifyURL = BMCRegistryHelper.GetRegKeyValue(ConfigManager.Read("RegistryPath"), "BGSWebService");

                        if (sVerifyURL.Length > 0)
                        {
                            oWebService = new Proxy(sVerifyURL, true);
                        }                       
                    }
                    sReturnValue = oWebService.CheckTransactionKey(iSiteCode.ToString(), iAuthCode.ToString(), TransactionType);
                    if (!string.IsNullOrEmpty(sReturnValue))
                    {
                        iResult = Convert.ToInt32(sReturnValue.Substring(0, 1));
                    }                   
                }
                catch (Exception ex)
                {
                    ExceptionManager.Publish(ex);
                    iResult = -1;
                }
                return iResult;
            }

            private bool ResetEnterpriseTransactionKey(string iAuthCode, int iSiteCode)
            {
                if (oWebService == null)
                { oWebService = new Proxy(iSiteCode.ToString()); }
                return oWebService.ResetTransactionKey(iSiteCode.ToString(), iAuthCode);
            }
           
     
        #endregion  "Private Functions"

        #region "Public Function"

            public  bool CheckInstallation()
        {
            return FactoryResetDataAccess.CheckActiveInstallations();                    
        }
        
        public  bool CheckDataToExport()
        {
            return FactoryResetDataAccess.CheckDataToExport();
        }

        public bool RunScripts()
        {
            return FactoryResetDataAccess.RunScripts();
        }

        public bool FactoryResetHistory(bool isCompleted, int ResetModeID, string UserName, ref int FRHistoryID)
        {
            return FactoryResetDataAccess.FactoryResetHistory(isCompleted, ResetModeID, UserName, ref FRHistoryID);
        }
        public bool BackupConstraint(bool BackupConstraint, int ResetModeID)
        {
            return FactoryResetDataAccess.BackupConstraint(BackupConstraint, ResetModeID);
        }

        public bool DeleteAddConstraint(bool DropConstraint)
        {
            return FactoryResetDataAccess.DeleteAddConstraint(DropConstraint);
        }

        public bool ResetTable(int Mode_Id)
        {
            return FactoryResetDataAccess.ResetTable(Mode_Id);
        }
       

        /// <summary>
        /// To get the status of a particular service             
        /// <param name="strServiceName">string</param>     
        public string GetServiceStatus(string strServiceName)
        {
          
            DataTable dtServicesStatus = new DataTable();
            StringBuilder strServicelist = new StringBuilder();
            try
            {
                dtServicesStatus = oBMCMonitoring.GetServiceStatus(strServiceName, BMCMonitoring.ServiceTypes.All);               
                if (dtServicesStatus.Rows.Count > 0)
                {
                    for (int j = 0; j < dtServicesStatus.Columns.Count; j++)
                    {
                        for (int i = 0; i < dtServicesStatus.Rows.Count; i++)
                        {
                            if (j != dtServicesStatus.Columns.Count - 1)
                            {
                                if (dtServicesStatus.Rows[i][j].ToString() == strServiceName && dtServicesStatus.Rows[i][j + 1].ToString() == "Stopped")
                                {
                                    strServiceStatus = "STOP";
                                }
                                else if (dtServicesStatus.Rows[i][j].ToString() == strServiceName && dtServicesStatus.Rows[i][j + 1].ToString() == "Running")
                                {
                                    strServiceStatus = "RUN";

                                }
                                else if (dtServicesStatus.Rows[i][j].ToString() == strServiceName && dtServicesStatus.Rows[i][j + 1].ToString() == "Pending")
                                {
                                    strServiceStatus = "PEND";
                                }
                                else if (dtServicesStatus.Rows[i][j].ToString() == strServiceName && dtServicesStatus.Rows[i][j + 1].ToString() == "Service not found")
                                {
                                    strServiceStatus = "NOSERVICE";
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }

            }
            catch (IndexOutOfRangeException iex)
            {
                LogManager.WriteLog("GetServiceStatus" + iex.Message.ToString() + iex.Source.ToString(), LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(iex);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("GetServiceStatus" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
            }
            finally
            {
                dtServicesStatus.Dispose();
            }
            return strServiceStatus;
        }

        /// <summary>
        /// To End the selected service       
        /// <returns name="service status">bool</returns>
        /// </summary>     
        public bool EndService(string strServicename)
        {
            try
            {              
                return oBMCMonitoring.EndService(strServicename);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("EndService" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
                return false;
            }
        }

        /// <summary>
        /// To Start the selected service       
        /// <returns name="service status">bool</returns>
        /// </summary>     
        public bool StartService(string strServicename)
        {
            try
            {
                return oBMCMonitoring.StartService(strServicename);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("StartService" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
                return false;
            }
        }

        public bool TestConnectionToDB(Dictionary<string, string> Credentials)
        {
            string strReturnConnect = "";
            bool bTestConnect = false;

            try
            {
                strReturnConnect = DatabaseCredentials.DatabaseCredentialsinstance.TestConnection(Credentials);
                bTestConnect=FactoryResetDataAccess.TestConnectionToDB(strReturnConnect);
            }
            catch(Exception ex)
            {
                bTestConnect=false;
            }

            return bTestConnect;
        }

        public bool TestConnectionToDB(string Connectionstring)
        {            
            bool bTestConnect = false;

            try
            {
                bTestConnect = FactoryResetDataAccess.TestConnectionToDB(Connectionstring);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                bTestConnect = false;
            }

            return bTestConnect;
        }

        public int CreateSqlDatabaseBackUp(FactoryReset objFactoryReset)
            {
                return FactoryResetDataAccess.CreateSqlDatabaseBackUp(objFactoryReset);
            }
      
      public int CreateDBZip(FactoryReset objFactoryReset)
        {
            return FactoryResetDataAccess.CreateDBZip(objFactoryReset);
        }

        /// <summary>
        /// To check the authorization code     
        /// <returns name="service status">bool</returns>
        /// </summary>     
        public int CheckAuthorizationCode(string iAuthCode)
        {
            int iSiteCode;
            int iValue = 0;
            iSiteCode = Convert.ToInt32(BMC.Transport.Settings.SiteCode.Trim());//FactoryResetDataAccess.GetSiteCode();
            if (iSiteCode>0)
                iValue = CheckAuthorizationCodeFromServer(iAuthCode, iSiteCode,"Reset");

            LogManager.WriteLog("Site Code from Site:  " + iSiteCode.ToString(), LogManager.enumLogLevel.Info);
            LogManager.WriteLog("CheckAuthorizationCode from server:  " + iValue.ToString(), LogManager.enumLogLevel.Info);
            return iValue;
        }

        public bool ResetTransactionKey(string iAuthCode)
        {
            try
            {
                int iSiteCode = Convert.ToInt32(BMC.Transport.Settings.SiteCode.Trim());
                return ResetEnterpriseTransactionKey(iAuthCode, iSiteCode);
            }
            catch (Exception exResetTransactionKey)
            {
                LogManager.WriteLog("Reset Transaction Key:  " + exResetTransactionKey.Message.ToString(), LogManager.enumLogLevel.Info);
                return false;
            }
        }

        #endregion

        public class ReadConfigSettings<T> 
        {
            private static Configuration _config = null;
            #region Private Constructor
             private ReadConfigSettings() { }
            #endregion

            public static Configuration Config
            {
                get
                {
                    if (_config == null)
                    {
                        _config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                        ConfigurationManager.RefreshSection("appSettings");
                    }
                    return _config;
                }
            }

            public static Dictionary<string, string> GetKeys(string sectionname)
            {
                XmlDocument objXml = new XmlDocument();
                XmlNode objXmlnode = null;
                XmlNodeList objXmlNodelist = null;
                Dictionary<string, string> objDictionary = new Dictionary<string, string>();

                try
                {
                    //oXml.Load(sPath);
                    //objXml.Load(System.Reflection.Assembly.GetExecutingAssembly().Location + ".config");
                    if (System.IO.File.Exists(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile) == true)
                    {
                        objXml.Load(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
                    }
                    else
                    {
                        return null;
                    }

                    objXmlnode = objXml.DocumentElement.SelectSingleNode("//" + sectionname);

                    if (objXmlnode == null)
                    {
                        return null;
                    }
                    else
                    {
                        if (objXmlnode.HasChildNodes == true)
                        {
                            objXmlNodelist = objXmlnode.SelectNodes("//" + sectionname);
                        }
                        else
                        {
                            return null;
                        }


                        if (objXmlNodelist != null)
                        {
                            foreach (XmlNode oXnode in objXmlNodelist)
                            {

                                if (oXnode != null)
                                {
                                    foreach (XmlNode oXchildnode in oXnode.ChildNodes)
                                    {
                                        objDictionary.Add(oXchildnode.Attributes.GetNamedItem("key").InnerText, oXchildnode.Attributes.GetNamedItem("value").InnerText);
                                    }

                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogManager.WriteLog("GetKeys" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
                    ExceptionManager.Publish(ex);
                }
                return objDictionary;
            }
        }

        public class ReadRegistrySettings
        {
            private static ReadRegistrySettings _ReadRegistrySettinginstance = null;

            #region Variables
            static Dictionary<string, string> RegistryEntries = new Dictionary<string, string>();
            static string[] strSubKeyNames;
            //public static RegistryKey RegKey;
            static string strRegPath = string.Empty;
            static string[] strValueNames;
            #endregion

            #region Private Constructor
            private ReadRegistrySettings() { }
            #endregion

            public static ReadRegistrySettings ReadRegistrySettinginstance
            {
                get
                {
                    if (_ReadRegistrySettinginstance == null)
                        _ReadRegistrySettinginstance = new ReadRegistrySettings();

                    return _ReadRegistrySettinginstance;
                }
            }

            //public Dictionary<string, string> GetRegistryEntries(string Registrypath)
            //{
            //    string[] strOuterValueNames = null;

            //    try
            //    {
            //        if (!String.IsNullOrEmpty(Registrypath))
            //        {
            //            RegKey = BMCRegistryHelper.GetRegLocalMachine().OpenSubKey(Registrypath);
            //            //cashmaster or odbc.ini settings                           
            //            strOuterValueNames = RegKey.GetValueNames();
            //            foreach (string strOuterValues in strOuterValueNames)
            //            {
            //                // RegistryEntries.Add(Registrypath.Substring(Registrypath.LastIndexOf("\\") + 1) + '\\' + strOuterValues, RegKey.GetValue(strOuterValues).ToString());
            //                RegistryEntries.Add(Registrypath + '\\' + strOuterValues, RegKey.GetValue(strOuterValues).ToString());

            //            }
            //            //inside cashmaster or odbc.ini
            //            GetRegistryEntries(RegKey, Registrypath);

            //        }
            //    }

            //    catch (Exception ex)
            //    {
            //        LogManager.WriteLog("GetRegistryEntries" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
            //        ExceptionManager.Publish(ex);
            //    }
            //    return RegistryEntries;
            //}

            //private void GetRegistryEntries(RegistryKey regKey, string strRegpath)
            //{
            //    try
            //    {
            //        strSubKeyNames = RegKey.GetSubKeyNames();
            //        foreach (string sKey in strSubKeyNames)
            //        {
            //            if (!String.IsNullOrEmpty(sKey))
            //            {
            //                strRegPath = strRegpath + "\\" + sKey;
            //                RegKey = BMCRegistryHelper.GetRegLocalMachine().OpenSubKey(strRegPath);                            
            //                strValueNames = RegKey.GetValueNames();

            //                foreach (string sValue in strValueNames)
            //                {
            //                    RegistryEntries.Add(strRegPath + "\\" + sValue, RegKey.GetValue(sValue).ToString());
            //                }

            //                if (RegKey.SubKeyCount > 0)
            //                {
            //                    GetRegistryEntries(RegKey, strRegPath);
            //                }
            //            }
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        LogManager.WriteLog("GetRegistryEntries" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
            //        ExceptionManager.Publish(ex);
            //    }
            //}

            //public bool SetRegistryEntries(Dictionary<string, string> dictSetregistry, string strPath)
            //{
            //    try
            //    {
            //        RegistryKey regKey = BMCRegistryHelper.GetRegLocalMachine().OpenSubKey(strPath,true);
                    
            //        foreach (var item in dictSetregistry)
            //        {
            //            regKey.SetValue(item.Key, item.Value);
            //        }

            //        regKey.Close();
            //    }
            //    catch
            //    {
            //        return false;
            //    }

            //    return true;
            //}

        }

        public class DatabaseCredentials
        {
            private static DatabaseCredentials _DatabaseCredentialsinstance = null;



            #region Private Constructor
            private DatabaseCredentials() { }
            #endregion

            public static DatabaseCredentials DatabaseCredentialsinstance
            {
                get
                {
                    if (_DatabaseCredentialsinstance == null)
                        _DatabaseCredentialsinstance = new DatabaseCredentials();

                    return _DatabaseCredentialsinstance;
                }
            }

            public Dictionary<string, string> RetrieveServerDetails(string ConnectionString)
            {
                string strServerName = string.Empty;
                Dictionary<string, string> ReturnDetails = new Dictionary<string, string>();
                string[] strConnection = null;
                try
                {

                    if (ConnectionString != string.Empty)
                    {
                        strConnection = ConnectionString.Split(';');

                        foreach (string sConn in strConnection)
                        {
                            if (sConn.ToUpper().Contains("SERVER"))
                            {
                                if (sConn.ToUpper().Contains("\\"))
                                {
                                    string[] arrServer = sConn.Split('\\');

                                    ReturnDetails.Add("SERVER", arrServer[0].Substring(arrServer[0].IndexOf("=") + 1));
                                    ReturnDetails.Add("INSTANCE", arrServer[1]);
                                }
                                else
                                {
                                    ReturnDetails.Add("SERVER", sConn.Substring(sConn.IndexOf("=") + 1));
                                }
                            }
                            if (sConn.ToUpper().Contains("UID"))
                            {
                                ReturnDetails.Add("UID", sConn.Substring(sConn.IndexOf("=") + 1));
                            }
                            if (sConn.ToUpper().Contains("PWD"))
                            {
                                ReturnDetails.Add("PASSWORD", sConn.Substring(sConn.IndexOf("=") + 1));
                            }
                            if (sConn.ToUpper().Contains("DATABASE"))
                            {
                                ReturnDetails.Add("DATABASE", sConn.Substring(sConn.IndexOf("=") + 1));
                            }
                            if (sConn.ToUpper().Contains("CONNECTION TIMEOUT"))
                            {
                                ReturnDetails.Add("TIMEOUT", sConn.Substring(sConn.IndexOf("=") + 1));
                            }


                        }
                    }
                }
                catch (Exception ex)
                {
                    LogManager.WriteLog("RetrieveServerDetails" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
                    ExceptionManager.Publish(ex);
                }

                return ReturnDetails;
            }

            public string MakeConnectionString(Dictionary<string, string> Credentials)
            {
                string strConnectionstring = string.Empty;
                try
                {
                    if (Credentials != null)
                    {
                        foreach (KeyValuePair<string, string> objKeyValue in Credentials)
                        {
                            strConnectionstring += objKeyValue.Key + "=" + objKeyValue.Value + ";";
                        }
                    }
                }
                catch (Exception ex)
                {
                    ExceptionManager.Publish(ex);
                }
                return strConnectionstring;
            }

            public bool GetServerDetails(string ConnectionString)
            {
                string strConnect = "";
                Dictionary<string, string> ServerEntries = null;


                // return DBBuilder.TestConnectionDB(ConnectionString);
                strConnect = CommonUtilities.ExchangeConnectionString;
                if (!String.IsNullOrEmpty(strConnect))
                {
                    ServerEntries = RetrieveServerDetails(strConnect);

                }
                return false;
            }

            public string TestConnection(Dictionary<string, string> Credentials)
            {
                string strReturnConnect = "";
                try
                {
                    strReturnConnect = MakeConnectionString(Credentials);

                    if (!String.IsNullOrEmpty(strReturnConnect))
                    {
                        return strReturnConnect;
                    }
                }
                catch (Exception ex)
                {
                    strReturnConnect = ""; ;
                }
               return strReturnConnect;
            }

            public string GetSettingValue(string settingname)
            {
                string servicelist = (CommonDataAccess.GetSettingValue(settingname)) != null ? CommonDataAccess.GetSettingValue("ServiceNames") : string.Empty;
                return servicelist;
            }
        }
    }
  
  
}
