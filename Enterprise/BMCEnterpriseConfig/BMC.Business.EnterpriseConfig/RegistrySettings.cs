using System;
using System.Collections.Generic;
using System.Text;
using BMC.Common.ConfigurationManagement;
using Microsoft.Win32;
using BMC.Common.LogManagement;
using BMC.Common.ExceptionManagement;
using BMC.DBInterface.EnterpriseConfig;

namespace BMC.Business.EnterpriseConfig
{
    public class RegistrySettings
    {
       #region Declaration
            static Dictionary<string, string> RegistryEntries = new Dictionary<string, string>();
            static string[] strSubKeyNames;
            //public static RegistryKey RegKey;
            static string strRegPath = string.Empty;
            static string[] strValueNames;
        #endregion

       #region Enterprise Connection String                  
                //public static string EnterpriseConnectionString()
                //{
                //    string sKey = string.Empty;
                //    bool bUseHex = true;
                //    RegistryKey RegKey1;
                //    string SQLConnect = "";
                //    ConfigManager.SetConfigurationMode(ConfigManager.ConfigurationMode.AppConfig);
                //    try
                //    {
                //        //Registry path  for Enterprise.
                //        RegKey1 = Registry.LocalMachine.OpenSubKey(ConfigManager.Read("RegistryPath"));

                //        BMC.Transport.EnterpriseConfig.EnterpriseConfigRegistryEntities.RegistryKeyValue = ConfigManager.Read("RegistryPath");
                //        SQLConnect = RegKey1.GetValue("SQLConnect").ToString();

                //        //When the connection string is already there before changes (without encryption), then DO NOT decrypt
                //        if (!SQLConnect.ToUpper().Contains("SERVER"))
                //        {

                //            BGSGeneral.cConstants objBGSConstants = new BGSGeneral.cConstants();
                //            BGSEncryptDecrypt.clsBlowFish objDecrypt = new BGSEncryptDecrypt.clsBlowFish();
                //            sKey = objBGSConstants.ENCRYPTIONKEY;
                //            SQLConnect = objDecrypt.DecryptString(ref SQLConnect, ref sKey, ref bUseHex);
                //            LogManager.WriteLog("DB Registry Value " + SQLConnect.Length.ToString(), LogManager.enumLogLevel.Debug);

                //        }
                //        RegKey1.Close();
                //    }
                   
                //    catch (Exception ex)
                //    {
                //        LogManager.WriteLog("EnterpriseConnectionString" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
                //        ExceptionManager.Publish(ex);
                //    }        
           

                //    return SQLConnect;
                //}
            #endregion
        
       #region RetrieveServerDetailsFromRegistry
     
        //public static Dictionary<string, string> GetRegistryEntries(string Registrypath)
        //            {
                       
        //               string[] strOuterValueNames=null;                     
                       
        //                //ConfigManager.SetConfigurationMode(ConfigManager.ConfigurationMode.AppConfig);
        //               try
        //               {
        //                   if (!String.IsNullOrEmpty(Registrypath))
        //                   {
        //                       RegKey = Registry.LocalMachine.OpenSubKey(Registrypath);
        //                       //cashmaster or odbc.ini settings    
        //                       if (RegKey != null)
        //                       {
        //                           strOuterValueNames = RegKey.GetValueNames();
        //                           foreach (string strOuterValues in strOuterValueNames)
        //                           {
        //                               // RegistryEntries.Add(Registrypath.Substring(Registrypath.LastIndexOf("\\") + 1) + '\\' + strOuterValues, RegKey.GetValue(strOuterValues).ToString());
        //                               RegistryEntries.Add(Registrypath + '\\' + strOuterValues, RegKey.GetValue(strOuterValues).ToString());

        //                           }
        //                           //inside cashmaster or odbc.ini
        //                           GetRegistryEntries(RegKey, Registrypath);
        //                       }

        //                   }
        //               }

        //               catch (Exception ex)
        //               {
        //                   LogManager.WriteLog("GetRegistryEntries" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
        //                   ExceptionManager.Publish(ex);
        //               }
        //                return RegistryEntries;
        //            }
        //private static void GetRegistryEntries(RegistryKey regKey,string strRegpath)
        //{
        //    try
        //    {
        //        strSubKeyNames = RegKey.GetSubKeyNames();
        //        foreach (string sKey in strSubKeyNames)
        //        {
        //            if (!String.IsNullOrEmpty(sKey))
        //            {
        //                strRegPath = strRegpath + "\\" + sKey;
        //                RegKey = Registry.LocalMachine.OpenSubKey(strRegPath);
        //                strValueNames = RegKey.GetValueNames();

        //                foreach (string sValue in strValueNames)
        //                {
        //                    //RegistryEntries.Add(sKey + "\\" + sValue, RegKey.GetValue(sValue).ToString());
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
       #endregion
    
        public static bool SetRegistryEntries(Dictionary<string, string> dictSetregistry,string strPath)
        { return RegistryBuilder.SetRegistryEntries(dictSetregistry, strPath);}
       
        public static string EncryptEnterpriseConnection()
        {return RegistryBuilder.EncryptEnterpriseConnection();}
      
        //public static string EncryptEnterpriseConnectionHex()
        //{ return RegistryBuilder.EncryptEnterpriseConnectionHex();}

        //public static bool SetRegistryString(string sKey, string sValue, string sPath)
        //{
        //    return RegistryBuilder.SetRegistryString(sKey, sValue, sPath);
        //}
        //public static string GetRegistryString(string sKey, string sPath, string sDefault)
        //{
        //    return RegistryBuilder.GetRegistryString(sKey, sPath, sDefault);
        //}
    }
}
