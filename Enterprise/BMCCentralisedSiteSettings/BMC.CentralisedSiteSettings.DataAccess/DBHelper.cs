using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using BMC.Common.ConfigurationManagement;
using BMC.Common.ExceptionManagement;
using BMC.Common.LogManagement;
using BMC.DataAccess;
using Microsoft.Win32;

namespace BMC.CentralisedSiteSettings.DataAccess
{
    public class DBHelper
    {
        #region Constants
        private const string CONST_GETSITEDETAILS_PROC = "rsp_GetSiteDetails";
        private const string CONST_GETPROFILEDETAILS_PROC = "rsp_GetProfileDetails";
        private const string CONST_GETSETTINGSDETAILS_PROC = "rsp_GetSettingsDetails";
        private const string CONST_UPDATESETTING_PROC = "usp_UpdateSetting";
        private const string CONST_INSERTSETTING_PROC = "usp_InsertSetting";
        private const string CONST_INSERTPROFILESETTING_PROC = "usp_InsertProfileSetting";
        private const string CONST_GETPROFILENAMEFORSITE_PROC = "rsp_GetProfileNameForSite";
        private const string CONST_UPDATEPROFILEFORSITE_PROC = "usp_UpdateProfileForSite";
        private const string CONST_EXPORTHISTORY_PROC = "usp_export_history";
        private const string CONST_SITESFORPROFILE_PROC = "rsp_GetSitesForProfile";
        #endregion Constants

        #region Private Methods
        /// <summary>
        /// Method to get the Connection String for the Enterprise.
        /// </summary>
        /// <returns></returns>
        private static string GetConnectionString()
        {
            return Common.Utilities.DatabaseHelper.GetConnectionString();
            //string strConnnect = string.Empty;
            //string sKey = string.Empty;
            //bool bUseHex = true;
            //RegistryKey RegKey;
            //try
            //{
            //    ConfigManager.SetConfigurationMode(ConfigManager.ConfigurationMode.AppConfig);
            //    RegKey = Registry.LocalMachine.OpenSubKey(ConfigManager.Read("DBConn").ToString());
            //    strConnnect = RegKey.GetValue("SQLConnect").ToString();
            //    if (string.IsNullOrEmpty(strConnnect))
            //    {
            //        ConfigManager.SetConfigurationMode(ConfigManager.ConfigurationMode.AppConfig);
            //        strConnnect = ConfigManager.Read("EnterpriseConnectionString");
            //        ConfigManager.SetConfigurationMode(ConfigManager.ConfigurationMode.Registry);
            //        return strConnnect;
            //    }
            //    if (!strConnnect.ToUpper().Contains("SERVER"))
            //    {
            //        BGSGeneral.cConstants objCons = new BGSGeneral.cConstants();
            //        BGSEncryptDecrypt.clsBlowFish objDecrypt = new BGSEncryptDecrypt.clsBlowFish();
            //        sKey = objCons.ENCRYPTIONKEY;
            //        strConnnect = objDecrypt.DecryptString(ref strConnnect, ref sKey, ref bUseHex);
            //    }

            //    RegKey.Close();
            //    return strConnnect;
            //}
            //catch (Exception ex)
            //{
            //   LogManager.WriteLog(ex.Message,LogManager.enumLogLevel.Error );
            //    try
            //    {
            //        strConnnect = string.Empty;
            //        if (string.IsNullOrEmpty(strConnnect))
            //        {
            //            ConfigManager.SetConfigurationMode(ConfigManager.ConfigurationMode.AppConfig);
            //            strConnnect = ConfigManager.Read("ConnectionString");
            //            ConfigManager.SetConfigurationMode(ConfigManager.ConfigurationMode.Registry);
            //        }
            //    }
            //    catch (Exception ex1)
            //    {
            //        LogManager.WriteLog(ex1.Message, LogManager.enumLogLevel.Error);
            //    }
            //    return strConnnect;
            //}
        }
        #endregion Private Methods

        #region Public Methods
        /// <summary>
        /// Method to get the Sites List.
        /// </summary>
        /// <returns></returns>
        public static DataTable GetSiteList()
        {
            DataSet objDS = new DataSet();
            DataTable objSites = new DataTable();
            try
            {

                objDS = SqlHelper.ExecuteDataset(GetConnectionString(), CONST_GETSITEDETAILS_PROC);
                objSites = objDS.Tables[0];

                foreach (DataRow dr in objSites.Rows)
                {
                    dr["Site_Name"] += " [ " + dr["SettingsProfile_Description"].ToString() + " ]";
                }
                return objDS.Tables[0];
            }
            catch (Exception ex)
            {
                LogManager.WriteLog(ex.Message, LogManager.enumLogLevel.Error);
                return new DataTable();
            }
            finally
            {
                objDS.Dispose();
            }
        }

        public static DataTable GetSiteList(string SiteName, int SiteStatusID)
        {
            DataSet objDS = new DataSet();
            SqlParameter[] Params = new SqlParameter[2];
            try
            {

                Params[0] = new SqlParameter("@Site_Name", SiteName);
                Params[1] = new SqlParameter("@Site_Status", SiteStatusID);
                objDS = SqlHelper.ExecuteDataset(GetConnectionString(), CONST_GETSITEDETAILS_PROC, Params);
                return objDS.Tables[0];
            }
            catch (Exception ex)
            {
                LogManager.WriteLog(ex.Message, LogManager.enumLogLevel.Error);
                return new DataTable();
            }
            finally
            {
                objDS.Dispose();
            }
        }

        /// <summary>
        /// Method to get the Profiles List.
        /// </summary>
        /// <returns></returns>
        public static DataTable GetProfileList()
        {
            DataSet objDS = new DataSet();

            try
            {
                objDS = SqlHelper.ExecuteDataset(GetConnectionString(), CONST_GETPROFILEDETAILS_PROC);
                return objDS.Tables[0];
            }
            catch (Exception ex)
            {
                LogManager.WriteLog(ex.Message, LogManager.enumLogLevel.Error);
                return new DataTable();
            }
            finally
            {
                objDS.Dispose();
            }
        }

        /// <summary>
        /// Method to get the Settings List.
        /// </summary>
        /// <returns></returns>
        public static DataTable GetSettingsList(string strProfileName, bool GetAllSettings)
        {
            DataSet objDS = new DataSet();

            try
            {
                SqlParameter[] objParam = new SqlParameter[4];
                objParam[0] = new SqlParameter("@SettingsProfile_Description", strProfileName);
                objParam[1] = new SqlParameter("@GetAllSettings", GetAllSettings);
                objParam[2] = new SqlParameter("@GETAFTSettings", "false");
                objParam[3] = new SqlParameter("@SiteID", 0);

                objDS = SqlHelper.ExecuteDataset(GetConnectionString(), CONST_GETSETTINGSDETAILS_PROC, objParam);
                return objDS.Tables[0];
            }
            catch (Exception ex)
            {
                LogManager.WriteLog(ex.Message, LogManager.enumLogLevel.Error);
                return new DataTable();
            }
            finally
            {
                objDS.Dispose();
            }
        }

        /// <summary>
        /// Method to update the Individual Settings.
        /// </summary>
        /// <param name="strSettingName"></param>
        /// <param name="strSettingValue"></param>
        /// <param name="strProfileName"></param>
        public static void UpdateSetting(string strSettingName, string strSettingValue, string strProfileName)
        {
            SqlParameter[] objParam = new SqlParameter[3];
            try
            {
                objParam[0] = new SqlParameter("@SettingsProfile_Description", strProfileName);
                objParam[1] = new SqlParameter("@SettingName", strSettingName);
                objParam[2] = new SqlParameter("@SettingValue", strSettingValue);
                SqlHelper.ExecuteNonQuery(GetConnectionString(), CONST_UPDATESETTING_PROC, objParam);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog(ex.Message, LogManager.enumLogLevel.Error);
            }
        }

        /// <summary>
        /// Method to insert the Individual Settings.
        /// </summary>
        /// <param name="strSettingName"></param>
        /// <param name="strSettingValue"></param>
        /// <param name="strProfileName"></param>
        public static void InsertSetting(string strSettingName, string strSettingValue, string strProfileName)
        {
            SqlParameter[] objParam = new SqlParameter[3];
            try
            {
                objParam[0] = new SqlParameter("@SettingsProfile_Description", strProfileName);
                objParam[1] = new SqlParameter("@SettingName", strSettingName);
                objParam[2] = new SqlParameter("@SettingValue", strSettingValue);

                SqlHelper.ExecuteNonQuery(GetConnectionString(), CONST_INSERTSETTING_PROC, objParam);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog(ex.Message, LogManager.enumLogLevel.Error);
            }
        }

        public static int InsertProfile(string strProfileName)
        {
            int iRetVal = 0;
            SqlParameter[] objParam = new SqlParameter[1];
            try
            {
                objParam[0] = new SqlParameter("@SettingsProfile_Description", strProfileName);
                iRetVal = SqlHelper.ExecuteNonQuery(GetConnectionString(), CONST_INSERTPROFILESETTING_PROC, objParam);                                  
            }
            catch (Exception ex)
            {
                LogManager.WriteLog(ex.Message, LogManager.enumLogLevel.Error);
            }
            return iRetVal;
        }

        public static void InsertAllSetting(string strProfileName, bool GetAllSettings)
        {
            DataTable dtSettings = new DataTable();
            SqlParameter[] objParam = new SqlParameter[3];
            dtSettings = GetSettingsList("Default Profile", GetAllSettings);
            try
            {
                foreach (DataRow dr in dtSettings.Rows)
                {
                    objParam = new SqlParameter[3];
                    objParam[0] = new SqlParameter("@SettingsProfile_Description", strProfileName);
                    objParam[1] = new SqlParameter("@SettingName", dr["Name"].ToString());
                    objParam[2] = new SqlParameter("@SettingValue", dr["Value"].ToString());

                    SqlHelper.ExecuteNonQuery(GetConnectionString(), CONST_INSERTSETTING_PROC, objParam);
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog(ex.Message, LogManager.enumLogLevel.Error);
            }
            finally
            {
                dtSettings.Dispose();
            }
        }

        public static string GetProfileNameForSite(string strSiteName)
        {
            string strProfileName = string.Empty;
            SqlParameter[] objParam = new SqlParameter[1];
            try
            {
                objParam[0] = new SqlParameter("@Site_ID", strSiteName);
                strProfileName = SqlHelper.ExecuteScalar(GetConnectionString(), CONST_GETPROFILENAMEFORSITE_PROC, objParam).ToString();
            }
            catch (Exception ex)
            {
                LogManager.WriteLog(ex.Message, LogManager.enumLogLevel.Error);
            }
            return strProfileName;
        }

        public static void UpdateProfileForSite(string strSiteCode, string strProfileName)
        {
            SqlParameter[] objParam = new SqlParameter[2];
            try
            {
                objParam[0] = new SqlParameter("@Site_ID", strSiteCode);
                objParam[1] = new SqlParameter("@SettingsProfile_Description", strProfileName);
                SqlHelper.ExecuteNonQuery(GetConnectionString(), CONST_UPDATEPROFILEFORSITE_PROC, objParam);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog(ex.Message, LogManager.enumLogLevel.Error);
            }
        }

        public static void InsertExportHistory(int iSiteID, string strReference)
        {
            SqlParameter[] objParam = new SqlParameter[3];
            objParam[0] = new SqlParameter("@Site_ID", iSiteID);
            objParam[1] = new SqlParameter("@Type", "SITESETTINGS");
            objParam[2] = new SqlParameter("@Reference1", iSiteID.ToString());
            try
            {
                SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, CONST_EXPORTHISTORY_PROC, objParam);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog(ex.Message, LogManager.enumLogLevel.Error);
            }

        }

        public static DataTable GetSitesForProfile(string strProfileName)
        {
            SqlParameter[] objparam = new SqlParameter[1];
            objparam[0] = new SqlParameter("@SettingsProfile_Description", strProfileName);
            try
            {
                return SqlHelper.ExecuteDataset(GetConnectionString(), CONST_SITESFORPROFILE_PROC, objparam).Tables[0];
            }
            catch (Exception ex)
            {
                LogManager.WriteLog(ex.Message, LogManager.enumLogLevel.Error);
                return new DataTable();
            }
        }
        #endregion Public Methods
    }
}
