using System;
using System.Collections.Generic;
using System.Text;
using System.Messaging;
using System.Data;
using BMC.Business.CashDeskOperator.WebServices;
using BMC.Monitoring;
using BMC.Transport.ExchangeConfig;
using Microsoft.Win32;
using System.Net;
using BMC.Common.ExceptionManagement;
using BMC.Common.LogManagement;
namespace BMC.Business.ExchangeConfig
{
    public class ReadServicesSettings

    {
        /// <summary>
        /// To get all the config settings from the class ReadConfigSettings
        /// <Author>Vineetha Mathew</Author>
        /// <DateCreated>Date Created 08-Dec-2008</DateCreated>
        /// <param name="sectionname">string</param>
        /// <returns></returns>
        /// </summary>
        /// Method Revision History
        ///
        /// Author             Date              Description
        /// ---------------------------------------------------
        ///Vineetha Mathew      08-12-2008      Created     
        public static Dictionary<string, string> GetKeys(string sectionname) 
        {
            Dictionary<string, string> objKeycCollections = null;
            try
            {
                objKeycCollections = ReadConfigSettings<string>.GetKeys("appSettings");
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("GetKeys" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);             
            }

            return objKeycCollections;
        }
        /// <summary>
        /// Check Message Queue for Exchange is already created or not
        /// <Author>Vineetha Mathew</Author>
        /// <DateCreated>Date Created 11-Dec-2008</DateCreated>
        /// <param name=strMSMQExchangepath>string</param>
        /// <returns>false</returns>
        /// </summary>      
        /// Method Revision History
        ///
        /// Author             Date              Description
        /// ---------------------------------------------------
        ///Vineetha Mathew      11-12-2008      Created
        public static bool CheckMSMQExists(string strMSMQExchangepath)
        {
            bool bReturn = true;
            try
            {

                if (MessageQueue.Exists(strMSMQExchangepath) == true)
                {
                    bReturn = false;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("CheckMSMQExists" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
            }

            return bReturn;
        }
        /// <summary>
        /// Check Message Queue for Exchange if not there and check the MSMQ service is running 
        /// if not running start it and create queue.else if service is already running create the queue simply.
        /// <Author>Vineetha Mathew</Author>
        /// <DateCreated>Date Created 11-Dec-2008</DateCreated>
        /// <param name=strMSMQExchangepath>string</param>
        /// <returns>true</returns>
        /// </summary>      
        /// Method Revision History
        ///
        /// Author             Date              Description
        /// ---------------------------------------------------
        ///Vineetha Mathew      11-12-2008      Created
        public static bool CreateMSMQ(string strMSMQExchangepath)
        {
            bool bMSMQServiceStatus = false;
            DataTable dtMSMQServicesStatus = new DataTable();
            BMCMonitoring objBMCMonitoring = new BMCMonitoring();
            bool bReturn = false;

            try
            {
                
                dtMSMQServicesStatus = objBMCMonitoring.GetServiceStatus("MSMQ", BMCMonitoring.ServiceTypes.All);

                if (dtMSMQServicesStatus.Rows.Count > 0)
                {
                    for (int i = 0; i < dtMSMQServicesStatus.Rows.Count; i++)
                    {
                        
                        if (dtMSMQServicesStatus.Rows[i][0].ToString() == "MSMQ" && dtMSMQServicesStatus.Rows[i][1].ToString() == "Stopped")
                        {
                            bMSMQServiceStatus = objBMCMonitoring.StartService("MSMQ");
                            if (bMSMQServiceStatus == true)
                            {
                                
                                System.Threading.Thread.Sleep(5000);
                                MessageQueue.Create(strMSMQExchangepath, true);
                                MessageQueue objMessagqQueue = new MessageQueue(strMSMQExchangepath);
                                objMessagqQueue.SetPermissions("Everyone", MessageQueueAccessRights.FullControl);
                                bReturn = true;
                                break;
                            }
                        }
                        else if (dtMSMQServicesStatus.Rows[i][0].ToString() == "MSMQ" && dtMSMQServicesStatus.Rows[i][1].ToString() == "Running")
                        {
                            
                            MessageQueue.Create(strMSMQExchangepath, true);
                            MessageQueue objMessagqQueue = new MessageQueue(strMSMQExchangepath);
                            objMessagqQueue.SetPermissions("Everyone", MessageQueueAccessRights.FullControl);
                            bReturn = true;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("CreateMSMQ" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
                bReturn = false;
            }
            return bReturn;
        }
        /// <summary>        
        /// Testing enterprise web url to check web services is working
        /// <Author>Vineetha Mathew</Author>
        /// <DateCreated>Date Created 11-Dec-2008</DateCreated>
        /// <param name=iSendValue>integer</param>
        /// <returns>iReceiveValue</returns>
        /// </summary>      
        /// Method Revision History
        ///
        /// Author             Date              Description
        /// ---------------------------------------------------
        ///Vineetha Mathew      11-12-2008      Created
        public static int TestWebUrl(string strUrl)
        {
            int iReceiveValue=-1;
            try
            {
                //BGSWSService objBGSWebProxy = new BGSWSService(strUrl);
                Proxy oProxy = new Proxy(strUrl, true);
                iReceiveValue = oProxy.InitiateWebService();               
            }
            catch (Exception ex)
            {   
                throw ex.InnerException;                
            }
            return iReceiveValue;
        }
        /// <summary>        
        /// Getting registry entries for DHCP server settings
        /// <Author>Vineetha Mathew</Author>
        /// <DateCreated>Date Created 15-Dec-2008</DateCreated>
        /// <param name=iSendValue>integer</param>
        /// <returns>iReceiveValue</returns>
        /// </summary>      
        /// Method Revision History
        ///
        /// Author             Date              Description
        /// ---------------------------------------------------
        ///Vineetha Mathew      15-12-2008      Created
        //public static bool GetRegistryEntries(string strRegistryKeyValue)
        //{
        //    ExchangeConfigRegistryEntities.RegistryKeyValue = strRegistryKeyValue;
        //    Dictionary<string, string> RegistryEntries = RegistrySettings.GetRegistryEntries(ExchangeConfigRegistryEntities.RegistryKeyValue);
        //    foreach (KeyValuePair<string, string> KVPServer in RegistryEntries)
        //    {
        //        //MessageBox.Show(KVPServer.Key + " " + KVPServer.Value);
        //    }
        //}
       
        public static string[] GetAllLocalIP()
        {
            string[] strarrIPAddressList=null;
            IPHostEntry objIPHostEntry =null;
            IPAddress[] arrIPaddress = null;
            try
            {
             objIPHostEntry = Dns.GetHostEntry(Dns.GetHostName());
             if (objIPHostEntry!=null)
                {
                    arrIPaddress = objIPHostEntry.AddressList;                    
                }
                if (arrIPaddress != null)
                {
                    if (arrIPaddress.Length > 0)
                    {
                        strarrIPAddressList = new string[arrIPaddress.Length+1];
                        strarrIPAddressList.Initialize();

                        strarrIPAddressList.SetValue(Dns.GetHostName(), 0);

                        for (int i = 0; i < arrIPaddress.Length; i++)
                        {
                            strarrIPAddressList.SetValue(arrIPaddress[i].ToString(), i+1);
                        }
                    }
                    else
                    {
                        strarrIPAddressList = null;
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("GetAllLocalIP" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
            }
            return strarrIPAddressList;
        }

        //public static bool DSNSettings(bool IsSqlAuthentication)
        //{ //Construct an ODBC DSN so the reports etc can connect successfully
        //    RegistryKey RegKey;
        //    string strServerName=string.Empty;
        //    bool bDSNDone = false;
        //    string[] strSubKeysarray = null;
        //    string strRegpath = string.Empty;
        //    Dictionary<string, string> dictSetregistryentries;
        //    bool bFindLeisure = false;
        //    bool bFindDataSources = false;
        //    try
        //    {                
        //        dictSetregistryentries = new Dictionary<string, string>();
        //        strRegpath = ExchangeConfigRegistryEntities.ODBCRegKeyValue;
        //        RegKey = Registry.LocalMachine.OpenSubKey(strRegpath, true);
        //        strSubKeysarray= RegKey.GetSubKeyNames();
        //       foreach (string strRegkeys in strSubKeysarray)
        //       {
        //           if (strRegkeys.Equals(ExchangeConfigRegistryEntities.DataSourceReferenceName))
        //           {
        //               bFindLeisure = true;                      
        //           }
                  
        //           if (strRegkeys.Equals("ODBC Data Sources"))
        //           {
        //               bFindDataSources = true;                       
        //           }                   

        //       }
        //       if (bFindLeisure == false)
        //       {
        //           //Trusted_Connection -Yes
        //           Registry.LocalMachine.CreateSubKey(strRegpath + "\\" + ExchangeConfigRegistryEntities.DataSourceReferenceName );
        //           dictSetregistryentries.Add("Database", ExchangeConfigRegistryEntities.DefaultDatabase + "+" + "REG_SZ");
        //           dictSetregistryentries.Add("Description", ExchangeConfigRegistryEntities.ODBCDescription + "+" + "REG_SZ");
        //           dictSetregistryentries.Add("Driver", "C:\\WINDOWS\\System32\\SQLSRV32.dll" + "+" + "REG_SZ");
        //           dictSetregistryentries.Add("Language", ExchangeConfigRegistryEntities.DefaultLanguage + "+" + "REG_SZ");
        //           dictSetregistryentries.Add("Lastuser", ExchangeConfigRegistryEntities.ODBCUsername + "+" + "REG_SZ");
        //           strServerName = ExchangeConfigRegistryEntities.ODBCServer; 
        //           if (!string.IsNullOrEmpty(ExchangeConfigRegistryEntities.ODBCServerInstance))
        //           {
        //                strServerName = ExchangeConfigRegistryEntities.ODBCServer+"\\" +
        //                                        ExchangeConfigRegistryEntities.ODBCServerInstance;
        //           }
        //           dictSetregistryentries.Add("Server", strServerName + "+" + "REG_SZ");
        //           if (IsSqlAuthentication==false)
        //               dictSetregistryentries.Add("Trusted_Connection", "Yes" + "+" + "REG_SZ");
        //           else
        //               dictSetregistryentries.Add("Trusted_Connection", "No" + "+" + "REG_SZ");
        //           RegistrySettings.SetRegistryEntries(dictSetregistryentries, strRegpath + "\\Leisure SQL");

        //           //ExchangeConfigRegistryEntities.ODBCRegKeyValue = strRegpath + "\\Leisure SQL";
        //       }
        //       else
        //       {
        //           Registry.LocalMachine.CreateSubKey(strRegpath + "\\" + ExchangeConfigRegistryEntities.DataSourceReferenceName );
        //           dictSetregistryentries.Add("Database", ExchangeConfigRegistryEntities.DefaultDatabase + "+" + "REG_SZ");
        //           dictSetregistryentries.Add("Description", ExchangeConfigRegistryEntities.ODBCDescription + "+" + "REG_SZ");
        //           dictSetregistryentries.Add("Driver", "C:\\WINDOWS\\System32\\SQLSRV32.dll" + "+" + "REG_SZ");
        //           dictSetregistryentries.Add("Language", ExchangeConfigRegistryEntities.DefaultLanguage + "+" + "REG_SZ");
        //           dictSetregistryentries.Add("Lastuser", ExchangeConfigRegistryEntities.ODBCUsername + "+" + "REG_SZ");
        //           strServerName = ExchangeConfigRegistryEntities.ODBCServer;
        //           if (!string.IsNullOrEmpty(ExchangeConfigRegistryEntities.ODBCServerInstance))
        //           {
        //               strServerName = ExchangeConfigRegistryEntities.ODBCServer + "\\" +
        //                                       ExchangeConfigRegistryEntities.ODBCServerInstance;
        //           }
        //           dictSetregistryentries.Add("Server", strServerName + "+" + "REG_SZ");
        //           if (IsSqlAuthentication == false)
        //               dictSetregistryentries.Add("Trusted_Connection", "Yes" + "+" + "REG_SZ");
        //           else
        //               dictSetregistryentries.Add("Trusted_Connection", "No" + "+" + "REG_SZ");
        //           RegistrySettings.SetRegistryEntries(dictSetregistryentries, strRegpath + "\\" + ExchangeConfigRegistryEntities.DataSourceReferenceName);
        //           //ExchangeConfigRegistryEntities.ODBCRegKeyValue = strRegpath + "\\Leisure SQL";
             
        //       }
        //       dictSetregistryentries = null;
        //       dictSetregistryentries = new Dictionary<string, string>();
        //       if (bFindDataSources == false)
        //       {
        //           RegKey = Registry.LocalMachine.CreateSubKey(strRegpath + "\\ODBC Data Sources");
        //           dictSetregistryentries.Add(ExchangeConfigRegistryEntities.DataSourceReferenceName, "SQL Server" + "+" + "REG_SZ");
        //           RegistrySettings.SetRegistryEntries(dictSetregistryentries, strRegpath + "\\ODBC Data Sources");
        //       }
        //       else
        //       {
        //           RegKey = Registry.LocalMachine.OpenSubKey(strRegpath + "\\ODBC Data Sources");
        //           dictSetregistryentries.Add(ExchangeConfigRegistryEntities.DataSourceReferenceName, "SQL Server" + "+" + "REG_SZ");
        //           RegistrySettings.SetRegistryEntries(dictSetregistryentries, strRegpath + "\\ODBC Data Sources");
              
        //       }
        //        RegKey.Close();
               
        //        bDSNDone=true;
                
        //    }          
        //    catch (Exception ex)
        //    {
        //        LogManager.WriteLog("DSNSettings" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
        //        ExceptionManager.Publish(ex);
        //        bDSNDone = false;
        //    }
        //    return bDSNDone;
        //}

    }
}
