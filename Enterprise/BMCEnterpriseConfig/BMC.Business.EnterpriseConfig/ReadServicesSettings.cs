using System;
using System.Collections.Generic;
using System.Text;
using System.Messaging;
using System.Data;
using BMC.Monitoring;
using BMC.Transport.EnterpriseConfig;
using Microsoft.Win32;
using System.Net;
using BMC.Common.ExceptionManagement;
using BMC.Common.LogManagement;
namespace BMC.Business.EnterpriseConfig
{
    public class ReadServicesSettings

    {
        
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
       
        public static bool CheckMSMQExists(string strMSMQEnterprisepath)
        {
            bool bReturn = true;
            try
            {

                if (MessageQueue.Exists(strMSMQEnterprisepath) == true)
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
       
        public static bool CreateMSMQ(string strMSMQEnterprisepath)
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
                                MessageQueue.Create(strMSMQEnterprisepath, true);
                                MessageQueue objMessagqQueue = new MessageQueue(strMSMQEnterprisepath);
                                objMessagqQueue.SetPermissions("Everyone", MessageQueueAccessRights.FullControl);
                                bReturn = true;
                                break;
                            }
                        }
                        else if (dtMSMQServicesStatus.Rows[i][0].ToString() == "MSMQ" && dtMSMQServicesStatus.Rows[i][1].ToString() == "Running")
                        {
                            
                            MessageQueue.Create(strMSMQEnterprisepath, true);
                            MessageQueue objMessagqQueue = new MessageQueue(strMSMQEnterprisepath);
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
       
        //public static int TestWebUrl(string strUrl)
        //{
        //    int iReceiveValue=0;
        //    try
        //    {
        //        //BGSWSService objBGSWebProxy = new BGSWSService(strUrl);
        //        Proxy oProxy = new Proxy(strUrl, true);
        //        iReceiveValue = oProxy.InitiateWebService();               
        //    }
        //    catch (Exception ex)
        //    {
        //        LogManager.WriteLog("TestWebUrl" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
        //        ExceptionManager.Publish(ex);                
        //    }
        //    return iReceiveValue;
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
                        strarrIPAddressList = new string[arrIPaddress.Length];
                        strarrIPAddressList.Initialize();
                        for (int i = 0; i < arrIPaddress.Length; i++)
                        {
                            strarrIPAddressList.SetValue(arrIPaddress[i].ToString(), i);
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
        public static bool DSNSettings(string strEnterpriseDB,string strEnterpriseInstance)
        { //Construct an ODBC DSN so the reports etc can connect successfully
            RegistryKey RegKey;
            string strServerName;
            bool bDSNDone = false;
            string[] strSubKeysarray = null;
            string strRegpath = string.Empty;
            Dictionary<string, string> dictSetregistryentries;
            bool bFindLeisure = false;
            bool bFindDataSources = false;
            try
            {                
                dictSetregistryentries = new Dictionary<string, string>();
                strRegpath = EnterpriseConfigRegistryEntities.ODBCRegKeyValue;
                RegKey = Registry.LocalMachine.OpenSubKey(strRegpath, true);
                strSubKeysarray= RegKey.GetSubKeyNames();
               foreach (string strRegkeys in strSubKeysarray)
               {
                   if (strRegkeys.Equals("Leisure SQL"))
                   {
                       bFindLeisure = true;                      
                   }
                  
                   if (strRegkeys.Equals("ODBC Data Sources"))
                   {
                       bFindDataSources = true;                       
                   }                   

               }
               if (bFindLeisure == false)
               {
                   
                   Registry.LocalMachine.CreateSubKey(strRegpath + "\\Leisure SQL");
                   dictSetregistryentries.Add("Database", strEnterpriseDB);
                   dictSetregistryentries.Add("Description", "");
                   dictSetregistryentries.Add("Driver", "C:\\WINDOWS\\System32\\SQLSRV32.dll");
                   dictSetregistryentries.Add("Language", "british");
                   dictSetregistryentries.Add("Lastuser", EnterpriseConfigRegistryEntities.User.ToString());
                   strServerName = EnterpriseConfigRegistryEntities.Server.ToString();
                   if (string.IsNullOrEmpty(strEnterpriseInstance))
                   {
                       strServerName += "\\" + strEnterpriseInstance;
                   }
                   dictSetregistryentries.Add("Server", strServerName);
                   RegistrySettings.SetRegistryEntries(dictSetregistryentries, strRegpath + "\\Leisure SQL");
                   //EnterpriseConfigRegistryEntities.ODBCRegKeyValue = strRegpath + "\\Leisure SQL";
               }
               if (bFindDataSources == false)
               {
                   RegKey = Registry.LocalMachine.CreateSubKey(strRegpath+"\\ODBC Data Sources");
                   dictSetregistryentries.Add("Leisure SQL", "SQL Server");
                   RegistrySettings.SetRegistryEntries(dictSetregistryentries, strRegpath + "\\ODBC Data Sources");
               }               
                RegKey.Close();
               
                bDSNDone=true;
                
            }          
            catch (Exception ex)
            {
                LogManager.WriteLog("GetAllLocalIP" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
                bDSNDone = false;
            }
            return bDSNDone;
        }

    }
}
