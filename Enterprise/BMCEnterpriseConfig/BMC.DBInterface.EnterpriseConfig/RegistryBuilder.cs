using System;
using System.Collections.Generic;
using System.Text;
using BMC.Common.ConfigurationManagement;
using Microsoft.Win32;
using BMC.Common.LogManagement;
using BMC.Common.ExceptionManagement;
using BMC.Transport.EnterpriseConfig;



namespace BMC.DBInterface.EnterpriseConfig
{
  public static class RegistryBuilder
    {
     private static bool binner = false;

      public static bool SetRegistryEntries(Dictionary<string, string> dictSetregistry, string strPath)
      {

          RegistryKey Regkeyinner;
          RegistryKey RegKey;
          string[] strSubKeysarray = null;                        
          string strKey = string.Empty;
          string strRoute = string.Empty;
          string[] strCheckRoute = null;
          string[] strTypeArray=null;
          string strType=string.Empty;
          string strValue = string.Empty;
          string strSubRoute = string.Empty;

          try
          {              
              RegKey = Registry.LocalMachine.OpenSubKey(strPath, true);
              strRoute=strPath.Substring(strPath.LastIndexOf("\\")+1);
              foreach (KeyValuePair<string, string> KVPServer in dictSetregistry)
              {
                  RegKey = Registry.LocalMachine.OpenSubKey(strPath, true);
                  strCheckRoute = KVPServer.Key.Split('\\');
                  strSubRoute = strCheckRoute[strCheckRoute.Length - 2];
                  if (strRoute == strSubRoute) // parent and subroute same eg:CashMaster
                  {
                      strTypeArray = KVPServer.Value.ToString().Split('+');
                      if (strTypeArray != null)
                      {
                          strValue = strTypeArray[0].ToString();
                          strType = strTypeArray[1].ToString();
                      }
                      if (strType == "REG_SZ")
                      {
                          RegKey.SetValue(strCheckRoute[strCheckRoute.Length - 1], strValue, RegistryValueKind.String);
                      }
                      else if (strType == "REG_DWORD")
                      {
                          RegKey.SetValue(strCheckRoute[strCheckRoute.Length - 1], strValue, RegistryValueKind.DWord);
                      }
                     // RegKey.SetValue(strCheckRoute[strCheckRoute.Length - 1], KVPServer.Value);
                  }
                  else
                  {
                      strSubKeysarray = RegKey.GetSubKeyNames(); // subkeyroute and subroute same eg:CashMaster
                      if (strSubKeysarray.Length > 0)
                      {                          
                          strKey = KVPServer.Key.Substring(KVPServer.Key.LastIndexOf("\\") + 1);                          
                            binner = false;
                          string strRegKey = GetSubKeys(RegKey, strSubRoute);
                          if (!string.IsNullOrEmpty(strRegKey))
                          {                                     
                              Regkeyinner = RegKey.OpenSubKey(strRegKey, true);
                              if (Regkeyinner == null)
                              {
                                  //RegKey = Registry.LocalMachine.OpenSubKey("Software" + @"\\" + KVPServer.Key.Remove(KVPServer.Key.LastIndexOf("\\")).Replace(@"\", @"\\").ToString(), true);
                                  RegKey = Registry.LocalMachine.OpenSubKey(strPath + KVPServer.Key.Remove(KVPServer.Key.LastIndexOf("\\")).Replace(@"\", @"\\").ToString(), true);
                                  strTypeArray = KVPServer.Value.ToString().Split('+');
                                  if (strTypeArray != null)
                                  {
                                      strValue = strTypeArray[0].ToString();
                                      strType = strTypeArray[1].ToString();
                                  }
                                  if (strType == "REG_SZ")
                                  {
                                      RegKey.SetValue(strKey, strValue, RegistryValueKind.String);
                                  }
                                  else if (strType == "REG_DWORD")
                                  {
                                      RegKey.SetValue(strKey, strValue, RegistryValueKind.DWord);
                                  }
                              }
                              else
                              {
                                  strTypeArray = KVPServer.Value.ToString().Split('+');
                                  if (strTypeArray != null)
                                  {
                                      strValue = strTypeArray[0].ToString();
                                      strType = strTypeArray[1].ToString();
                                  }
                                  if (strType == "REG_SZ")
                                  {
                                      Regkeyinner.SetValue(strKey, strValue, RegistryValueKind.String);
                                  }
                                  else if (strType == "REG_DWORD")
                                  {
                                      Regkeyinner.SetValue(strKey, strValue, RegistryValueKind.DWord);
                                  }
                              }

                              
                          }                                  
                      }
                      else
                      {
                          strTypeArray = KVPServer.Value.ToString().Split('+');
                          if (strTypeArray != null)
                          {
                              strValue = strTypeArray[0].ToString();
                              strType = strTypeArray[1].ToString();
                          }
                          if (strType == "REG_SZ")
                          {
                              RegKey.SetValue(KVPServer.Key, strValue, RegistryValueKind.String);
                          }
                          else if (strType == "REG_DWORD")
                          {
                              RegKey.SetValue(KVPServer.Key, strValue, RegistryValueKind.DWord);
                          }
                          //RegKey.SetValue(KVPServer.Key, KVPServer.Value);
                          RegKey.SetValue(KVPServer.Key, strValue);
                      }
                  }                
                }
          }                   
          catch (Exception ex)
          {
              LogManager.WriteLog("SetRegistryEntries" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
              ExceptionManager.Publish(ex);
              return false;
          }
          return true;
      }

      private static string GetSubKeys(RegistryKey SubKey, string strSubRoute)
      {
          string skeyname ="";
          
          foreach (string keyname in SubKey.GetSubKeyNames())
          {
              try
              {
                  foreach (string keynamesub in SubKey.GetSubKeyNames())
                  {
                      using (RegistryKey key = SubKey.OpenSubKey(keynamesub))
                      {
                          if (keynamesub == strSubRoute)
                          {
                              skeyname = keynamesub;
                              binner = true;
                              return skeyname;
                          }

                      }
                  }
              }
              catch (System.Security.SecurityException ex)
              {
                  ExceptionManager.Publish(ex);  
              }
              if (binner == false)
              {
                  using (RegistryKey keysub = SubKey.OpenSubKey(keyname))
                  {
                      if (binner == false)
                      {
                         skeyname= GetSubKeys(keysub, strSubRoute);
                          if (!string.IsNullOrEmpty(skeyname))
                            return skeyname;
                      }
                  }
              }
          }
         
          return skeyname;
      }
        
      //public static string EncryptEnterpriseConnection()
      //{ 
      //    string strEncrypKey = string.Empty;
      //    bool bUseHex = false;
      //    BGSEncryptDecrypt.clsBlowFish objEncrypt = null;
      //    BGSGeneral.cConstants objConstant = null;
      //    string strReturnEncryptConnection = string.Empty;
      //    string EnterpriseConnectionString = string.Empty;

      //    try
      //    {
      //        EnterpriseConnectionString=EnterpriseConfigRegistryEntities.EnterpriseConnectionString;
      //        LogManager.WriteLog("First time Encypted :"+EnterpriseConnectionString.Length.ToString(), LogManager.enumLogLevel.Debug);
      //        if (!string.IsNullOrEmpty(EnterpriseConnectionString))
      //        {
      //            objEncrypt = new BGSEncryptDecrypt.clsBlowFish();
      //            objConstant = new BGSGeneral.cConstants();
      //            strEncrypKey = objConstant.ENCRYPTIONKEY;
      //            bUseHex = true;
      //            strReturnEncryptConnection = objEncrypt.EncryptString(ref EnterpriseConnectionString,ref strEncrypKey,ref bUseHex);
      //            LogManager.WriteLog(" Encypted :" + strReturnEncryptConnection.Length.ToString(), LogManager.enumLogLevel.Debug);
      //        }
      //    }
      //    catch (Exception ex)
      //    {
      //        LogManager.WriteLog("EncryptEnterpriseConnection" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
      //        ExceptionManager.Publish(ex);             
      //    }
         
      //    return strReturnEncryptConnection;

      //}

      public static string EncryptEnterpriseConnection()
      {
          string strEncrypKey = string.Empty;
          string strReturnEncryptConnection = string.Empty;
          string EnterpriseConnectionString = string.Empty;

          try
          {
              EnterpriseConnectionString = EnterpriseConfigRegistryEntities.EnterpriseConnectionString;
              LogManager.WriteLog("First time Encypted :" + EnterpriseConnectionString.Length.ToString(), LogManager.enumLogLevel.Debug);
              if (!string.IsNullOrEmpty(EnterpriseConnectionString))
              {
                  strReturnEncryptConnection = BMC.Common.Utilities.DatabaseHelper.StoreConnectionString(EnterpriseConnectionString).ToString();
                  LogManager.WriteLog(" Encypted :" + strReturnEncryptConnection.Length.ToString(), LogManager.enumLogLevel.Debug);
              }
          }
          catch (Exception ex)
          {
              LogManager.WriteLog("EncryptEnterpriseConnection" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
              ExceptionManager.Publish(ex);
          }

          return strReturnEncryptConnection;

      }
      
      //public static string EncryptEnterpriseConnectionHex()
      //{
      //    string strEncrypKey = string.Empty;
      //    bool bUseHex = false;
      //    BGSEncryptDecrypt.clsBlowFish objEncrypt = null;
      //    BGSGeneral.cConstants objConstant = null;
      //    string strReturnEncryptConnection = string.Empty;
      //    string EnterpriseConnectionString = string.Empty;

      //    try
      //    {
      //        EnterpriseConnectionString = EnterpriseConfigRegistryEntities.EnterpriseConnectionString;
      //        LogManager.WriteLog("First time Encypted for Hex:" + EnterpriseConnectionString.Length.ToString(), LogManager.enumLogLevel.Debug);
      //        if (!string.IsNullOrEmpty(EnterpriseConnectionString))
      //        {
      //            objEncrypt = new BGSEncryptDecrypt.clsBlowFish();
      //            objConstant = new BGSGeneral.cConstants();
      //            strEncrypKey = objConstant.ENCRYPTIONKEY;
      //            bUseHex = false;
      //            strReturnEncryptConnection = objEncrypt.EncryptString(ref EnterpriseConnectionString, ref strEncrypKey, ref bUseHex);
      //            LogManager.WriteLog(" Encypted :" + strReturnEncryptConnection.Length.ToString(), LogManager.enumLogLevel.Debug);
      //        }
      //    }
      //    catch (Exception ex)
      //    {
      //        LogManager.WriteLog("EncryptEnterpriseConnection" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
      //        ExceptionManager.Publish(ex);
      //    }

      //    return strReturnEncryptConnection;

      //}
      //public static bool SetRegistryString(string sKey, string sValue, string sPath)
      //{
      //    Registry.LocalMachine.OpenSubKey(sPath, true).SetValue(sKey, sValue, RegistryValueKind.String);
      //    return true;
      //}
      //public static string GetRegistryString(string sKey, string sPath, string sDefault)
      //{
      //    try
      //    {
      //        return Registry.LocalMachine.OpenSubKey(sPath, true).GetValue(sKey, sDefault).ToString();

      //    }
      //    catch (Exception Ex)
      //    {
      //        ExceptionManager.Publish(Ex);
      //        return sDefault;
      //    }
      //}
    }
}
