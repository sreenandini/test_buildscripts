using System;
using System.Collections.Generic;
using System.Text;
using BMC.DataAccess;
using BMC.Common.LogManagement;
using Microsoft.Win32;
using BMC.Common.ConfigurationManagement;
using System.Data;
using BMC.Common.Utilities;

namespace SiteStatusService.DBBuilder
{
    public class DBCalls
    {

        public static string GetConnRegSettings() //Check registry for SQl connection settings
        {
            string sKey = string.Empty;
            string SQLConnect = "";
            ConfigManager.SetConfigurationMode(ConfigManager.ConfigurationMode.AppConfig);
            try
            {
                    //BGSGeneral.cConstants objBGSConstants = new BGSGeneral.cConstants();
                    //BGSEncryptDecrypt.clsBlowFish objDecrypt = new BGSEncryptDecrypt.clsBlowFish();
                    //sKey = objBGSConstants.ENCRYPTIONKEY;
                    //SQLConnect = objDecrypt.DecryptString(ref SQLConnect, ref sKey, ref bUseHex);
                    SQLConnect = BMC.Common.Utilities.DatabaseHelper.GetConnectionString();
                return SQLConnect;
              
            }
            catch (Exception ex)
            {
                LogManager.WriteLog(ex.Message, LogManager.enumLogLevel.Error);
                return string.Empty;
            }
        }

        public static string GetSiteName()
        {
            try
            {
                string strSiteName  = SqlHelper.ExecuteScalar(GetConnRegSettings(), System.Data.CommandType.Text, "Select Code from site").ToString();
                return strSiteName;
            }
            catch(Exception ex)       
            {
                LogManager.WriteLog(ex.Message, LogManager.enumLogLevel.Error);
                return string.Empty;
            }
        }

        public static string GetServiceNames()
        {
            try
            {
                string strServiceNames = SqlHelper.ExecuteScalar(GetConnRegSettings(), System.Data.CommandType.Text, "Select Setting_Value from setting where Setting_Name = 'ServiceNames'").ToString();
                return strServiceNames;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog(ex.Message, LogManager.enumLogLevel.Error);
                return string.Empty;                
            }
        }

        public static DataSet GetServiceStatusDetails()
        {
            try
            {
                DataSet dsData = SqlHelper.ExecuteDataset(GetConnRegSettings(), System.Data.CommandType.StoredProcedure, "rsp_GetLastServiceStatusDetails");
                return dsData;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog(ex.Message, LogManager.enumLogLevel.Error);
                return new DataSet();
            }
        }

        public static void UpdateServiceStatusDetails()
        {
            try
            {
                SqlHelper.ExecuteNonQuery(GetConnRegSettings(), System.Data.CommandType.StoredProcedure, "usp_UpdateLastServiceStatusDetails");
            }
            catch (Exception ex)
            {
                LogManager.WriteLog(ex.Message, LogManager.enumLogLevel.Error);
            }
        }
    }
}
