/*******************************************************************************************************
 *  Revision History
 *  Name            TrackCode   Modified Date   Change Description
 *  Selva Kumar S   S001        27th Jul 2012   Encrypt and Decrypt Cash Dispenser server informations
 *                                              
 * ****************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using BMC.Common.ConfigurationManagement;
using Microsoft.Win32;
using BMC.Common.LogManagement;
using BMC.Common.ExceptionManagement;
using BMC.Transport.ExchangeConfig;



namespace BMC.DBInterface.ExchangeConfig
{
    public static class RegistryBuilder
    {
        private static bool binner = false;

        public static bool SetRegistryEntries(Dictionary<string, string> dictSetregistry, string strPath, int old)
        {

            RegistryKey Regkeyinner = null;
            RegistryKey RegKey = null;
            string[] strSubKeysarray = null;
            string strKey = string.Empty;
            string strRoute = string.Empty;
            string[] strCheckRoute = null;
            string[] strTypeArray = null;
            string strType = string.Empty;
            string strValue = string.Empty;
            string strSubRoute = string.Empty;

            try
            {
                RegKey = Registry.LocalMachine.OpenSubKey(strPath, true);
                strRoute = strPath.Substring(strPath.LastIndexOf("\\") + 1);
                foreach (KeyValuePair<string, string> KVPServer in dictSetregistry)
                {
                    strValue = "";
                    strType = "";
                    RegKey = Registry.LocalMachine.OpenSubKey(strPath, true);
                    strCheckRoute = KVPServer.Key.Split('\\');
                    if (KVPServer.Key.Split('\\').Length > 1)
                        strSubRoute = strCheckRoute[strCheckRoute.Length - 2];
                    else
                        strSubRoute = strCheckRoute[strCheckRoute.Length - 1];
                    if (strRoute == strSubRoute) // parent and subroute same eg:CashMaster
                    {
                        char[] delimter = { '+' };

                        strTypeArray = KVPServer.Value.ToString().Split('+');
                        string[] strSplitValue = KVPServer.Value.ToString().Split(delimter, StringSplitOptions.RemoveEmptyEntries);
                        if (strSplitValue.Length > 2)
                        {

                            for (int i = 0; i < strSplitValue.Length; i++)
                            {
                                if (i == strSplitValue.Length - 1)
                                {
                                    strType = strSplitValue[i];
                                }
                                else
                                {
                                    strValue = strValue + strSplitValue[i] + '+';
                                }
                            }
                            strValue = strValue.Remove(strValue.Length - 1);
                        }
                        else if (strTypeArray != null)
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
            finally
            {
                if (RegKey != null)
                {
                    RegKey.Close();
                }

                if (Regkeyinner != null)
                {
                    Regkeyinner.Close();
                }
            }

            return true;
        }

        public static bool SetRegistryEntries(Dictionary<string, string> dictSetregistry, string strPath)
        {

            string strKey = string.Empty;
            string strRoute = string.Empty;
            string strType = string.Empty;
            string strValue = string.Empty;
            string strSubRoute = string.Empty;
            KeyValuePair<string, string> temp = new KeyValuePair<string, string>();
            try
            {
                //RegKey = Registry.LocalMachine.OpenSubKey(strPath, true);
                foreach (KeyValuePair<string, string> KVPServer in dictSetregistry)
                {
                    temp = KVPServer;
                    if (KVPServer.Key.Contains("\\") && KVPServer.Value.Contains("+"))
                        BMC.Common.Utilities.BMCRegistryHelper.SetRegKeyValue(KVPServer.Key.Substring(0, KVPServer.Key.LastIndexOf('\\')), 
                        KVPServer.Key.Substring(KVPServer.Key.LastIndexOf('\\')+1), 
                        KVPServer.Value.Substring(KVPServer.Value.LastIndexOf('+')+1).Contains("REG_SZ") ? RegistryValueKind.String : RegistryValueKind.DWord, 
                        KVPServer.Value.Substring(0, KVPServer.Value.LastIndexOf('+')).Trim());
                    else if(KVPServer.Key.Contains("\\"))
                        BMC.Common.Utilities.BMCRegistryHelper.SetRegKeyValue(KVPServer.Key.Substring(0, KVPServer.Key.LastIndexOf('\\')),
                        KVPServer.Key.Substring(KVPServer.Key.LastIndexOf('\\')+1),
                         RegistryValueKind.String ,
                        KVPServer.Value);
                    else if(KVPServer.Value.Contains("+"))
                         BMC.Common.Utilities.BMCRegistryHelper.SetRegKeyValue("", 
                        KVPServer.Key, 
                        KVPServer.Value.Substring(KVPServer.Value.LastIndexOf('+')+1).Contains("REG_SZ") ? RegistryValueKind.String : RegistryValueKind.DWord, 
                        KVPServer.Value.Substring(0, KVPServer.Value.LastIndexOf('+')-1));
                    else
                        BMC.Common.Utilities.BMCRegistryHelper.SetRegKeyValue("", 
                        KVPServer.Key,
                        RegistryValueKind.String,
                        KVPServer.Value);
                    }
                }
            catch (Exception ex)
            {
                LogManager.WriteLog(temp.Key + " :   " + temp.Value, LogManager.enumLogLevel.Error);
                LogManager.WriteLog("SetRegistryEntries" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
                return false;
            }
            return true;
        }

        private static string GetSubKeys(RegistryKey SubKey, string strSubRoute)
        {
            string skeyname = "";

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
                            skeyname = GetSubKeys(keysub, strSubRoute);
                            if (!string.IsNullOrEmpty(skeyname))
                                return skeyname;
                        }
                    }
                }
            }

            return skeyname;
        }

        public static string EncryptExchangeConnection()
        {
            string strEncrypKey = string.Empty;
            string strReturnEncryptConnection = string.Empty;
            string ExchangeConnectionString = string.Empty;

            try
            {
                ExchangeConnectionString = ExchangeConfigRegistryEntities.ExchangeConnectionString;
                LogManager.WriteLog("First time Encypted :" + ExchangeConnectionString.Length.ToString(), LogManager.enumLogLevel.Debug);
                if (!string.IsNullOrEmpty(ExchangeConnectionString))
                {
                    strReturnEncryptConnection = BMC.Common.Security.CryptEncode.Encrypt(ExchangeConnectionString);
                    LogManager.WriteLog(" Encypted :" + strReturnEncryptConnection.Length.ToString(), LogManager.enumLogLevel.Debug);
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("EncryptExchangeConnection" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
            }

            return strReturnEncryptConnection;

        }
        public static string EncryptPCConnection()
        {
            string strEncrypKey = string.Empty;
            string strReturnEncryptConnection = string.Empty;
            string PCConnectionString = string.Empty;

            try
            {
                PCConnectionString = ExchangeConfigRegistryEntities.CMPConnectionString;
                LogManager.WriteLog("First time PC Connection string Encrypted :" + PCConnectionString.Length.ToString(), LogManager.enumLogLevel.Debug);
                if (!string.IsNullOrEmpty(PCConnectionString))
                {
                    strReturnEncryptConnection = BMC.Common.Security.CryptEncode.Encrypt(PCConnectionString);
                    LogManager.WriteLog(" Encypted :" + strReturnEncryptConnection.Length.ToString(), LogManager.enumLogLevel.Debug);
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("EncryptPCConnection" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
            }

            return strReturnEncryptConnection;

        }

        public static string EncryptExchangeConnectionHex()
        {
            string strEncrypKey = string.Empty;
            //bool bUseHex = false;
            //BGSEncryptDecrypt.clsBlowFish objEncrypt = null;
            //BGSGeneral.cConstants objConstant = null;
            string strReturnEncryptConnection = string.Empty;
            string ExchangeConnectionString = string.Empty;

            try
            {
                ExchangeConnectionString = ExchangeConfigRegistryEntities.ExchangeConnectionString;
                LogManager.WriteLog("First time Encypted for Hex:" + ExchangeConnectionString.Length.ToString(), LogManager.enumLogLevel.Debug);
                if (!string.IsNullOrEmpty(ExchangeConnectionString))
                {
                    //objEncrypt = new BGSEncryptDecrypt.clsBlowFish();
                    //objConstant = new BGSGeneral.cConstants();
                    //strEncrypKey = objConstant.ENCRYPTIONKEY;
                    //bUseHex = false;
                    //strReturnEncryptConnection = objEncrypt.EncryptString(ref ExchangeConnectionString, ref strEncrypKey, ref bUseHex);
                    strReturnEncryptConnection = BMC.Common.Security.CryptEncode.Encrypt(ExchangeConnectionString);
                    LogManager.WriteLog(" Encypted :" + strReturnEncryptConnection.Length.ToString(), LogManager.enumLogLevel.Debug);
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("EncryptExchangeConnection" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
            }

            return strReturnEncryptConnection;

        }

        public static bool SetRegistryString(string sKey, string sValue, string sPath)
        {
            RegistryKey _RgKey = Registry.LocalMachine.OpenSubKey(sPath, true);
            _RgKey.SetValue(sKey, sValue, RegistryValueKind.String);
            _RgKey.Close();
            return true;
        }

        public static string GetRegistryString(string sKey, string sPath, string sDefault)
        {
            try
            {
                return Registry.LocalMachine.OpenSubKey(sPath, true).GetValue(sKey, sDefault).ToString();

            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                return sDefault;
            }
        }


        #region +S001 START
        public static string EncryptCDSetting(string strCDSetting)
        {
            string strEncrypKey = string.Empty;
            //bool bUseHex = false;
            //BGSEncryptDecrypt.clsBlowFish objEncrypt = null;
            //BGSGeneral.cConstants objConstant = null;
            string strReturnEncryptSetting = string.Empty;

            try
            {
                LogManager.WriteLog("First time Encypted :" + strCDSetting.Length.ToString(), LogManager.enumLogLevel.Debug);
                if (!string.IsNullOrEmpty(strCDSetting))
                {
                    //objEncrypt = new BGSEncryptDecrypt.clsBlowFish();
                    //objConstant = new BGSGeneral.cConstants();
                    //strEncrypKey = objConstant.ENCRYPTIONKEY;
                    //bUseHex = true;
                    //strReturnEncryptSetting = objEncrypt.EncryptString(ref strCDSetting, ref strEncrypKey, ref bUseHex);
                    strReturnEncryptSetting = BMC.Common.Security.CryptEncode.Encrypt(strCDSetting);
                    LogManager.WriteLog(" Encypted :" + strReturnEncryptSetting.Length.ToString(), LogManager.enumLogLevel.Debug);
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("EncryptCDSettings" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
            }

            return strReturnEncryptSetting;

        }

        public static string DecryptCDSetting(string strEncryptCDSetting)
        {

            string strKey = string.Empty;
            string strDecryptCDSetting = string.Empty;
            //BGSGeneral.cConstants objBGSConstants = new BGSGeneral.cConstants();
            //BGSEncryptDecrypt.clsBlowFish objDecrypt = new BGSEncryptDecrypt.clsBlowFish();

            try
            {
                if (!string.IsNullOrEmpty(strEncryptCDSetting))
                {
                    //strKey = objBGSConstants.ENCRYPTIONKEY;
                    //strDecryptCDSetting = objDecrypt.DecryptString(ref strEncryptCDSetting, ref strKey, ref bUseHex);
                    strDecryptCDSetting = BMC.Common.Security.CryptEncode.Decrypt(strEncryptCDSetting);
                    LogManager.WriteLog("Cash dispenser server details" + strDecryptCDSetting.Length.ToString(), LogManager.enumLogLevel.Debug);
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("DecryptCDSettings" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
            }

            return strDecryptCDSetting;
        }
        #endregion +S001 END

    }
}