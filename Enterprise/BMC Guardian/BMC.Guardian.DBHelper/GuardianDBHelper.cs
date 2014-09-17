using BMC.Common.ConfigurationManagement;
using BMC.Common.ExceptionManagement;
using BMC.DataAccess;
using Microsoft.Win32;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using BMC.Guardian.Transport;
using BMC.Common.LogManagement;
using BMC.Common;

namespace BMC.Guardian.DBHelper
{


    public static class GuardianDBHelper
    {
        private static readonly string ConnectionString = string.Empty;
        private static readonly string FIFOStatusPROC = "rsp_GetFIFOStatus";
        private static readonly string SiteDetailsProc = "rsp_GetSiteDetailsGuardian";
        private static readonly string StatusHistoryProc = "rsp_GetSiteStatusHistory";
        private static readonly string StatusHistoryProcByTimeStamp = "rsp_GetSiteStatusHistoryByTimeStamp";
        //
        private static readonly string ToGetInstallationNumber = "rsp_GetInstallationNoForAssetNo";
        //


        static GuardianDBHelper()
        {
            ConnectionString = GetConnectionString();
        }

        public static string EnterpriseConnectionString
        { get { return GetConnectionString(); } }

        private static SqlParameter[] AddParametersForSiteDetails(string Sitename, bool IsActiveChecked, Int32 securityUserID, Int32 status, Int32 statusInterval)
        {
            SqlParameter[] parameterArray = new SqlParameter[5];
            parameterArray[0] = new SqlParameter("@Site_Name", Sitename);
            parameterArray[1] = new SqlParameter("@Site_Status", Convert.ToInt32(IsActiveChecked));
            parameterArray[2] = new SqlParameter("@SecurityUserID", securityUserID);
            parameterArray[3] = new SqlParameter("@Status", status);
            parameterArray[4] = new SqlParameter("@StatusInterval", statusInterval);
            return parameterArray;
        }


        private static string GetConnectionString()
        {
            //string ConnString = string.Empty;
            //try
            //{
            //    RegistryKey key = Registry.LocalMachine.OpenSubKey(ConfigManager.Read("RegistryPath"));
            //    ConnString = key.GetValue("SQLConnect").ToString();
            //    key.Close();
            //    if (string.IsNullOrEmpty(ConnString))
            //    {
            //        return ConfigurationSettings.AppSettings["EnterpriseConnectionString"];
            //    }
            //    if (!ConnString.ToUpper().Contains("SERVER"))
            //    {
            //        cConstants constants = new cConstantsClass();
            //        clsBlowFish fish = new clsBlowFishClass();
            //        bool isTextInHex = true;
            //        string eNCRYPTIONKEY = constants.ENCRYPTIONKEY;
            //        return fish.DecryptString(ref ConnString, ref eNCRYPTIONKEY, ref isTextInHex);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    LogManager.WriteLog("GetConnectionString::" + ex.Message, LogManager.enumLogLevel.Error);
            //}
            //return ConnString;
            return BMC.Common.Utilities.DatabaseHelper.GetConnectionString();
        }

        public static FIFOStatus GetFIFOStatus()
        {
            FIFOStatus fifoStatus;
            try
            {
                using (SqlDataReader reader = SqlHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, FIFOStatusPROC))
                {
                    if (reader.Read())
                    {
                        FIFOStatus status = new FIFOStatus();
                        status.LastRecord = reader["LastRecord"].ToString();
                        status.UnProcessed = reader["UnProcessed"].ToString();
                        return status;
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("DBHelper.GetFifoStatus::" + ex.Message, LogManager.enumLogLevel.Error);
            }
            fifoStatus = new FIFOStatus();
            fifoStatus.LastRecord = string.Empty;
            fifoStatus.UnProcessed = string.Empty;
            return fifoStatus;
        }

        public static int GetSiteActiveStatus(string siteName)
        {
            return Convert.ToInt32(GetSiteColumn(siteName, "Site_Status_ID"));
        }

        public static string GetSiteCode(string strSiteName)
        {
            return GetSiteColumn(strSiteName, "Site_Code");
        }
        public static string GetSiteStatus(string siteName)
        {
            return GetSiteColumn(siteName, "Site_Status");
        }

        public static string GetSiteURL(string siteName)
        {
            return GetSiteColumn(siteName, "WEBUrl");
        }

        private static string GetSiteColumn(string siteName, string ColumnName)
        {
            if (!string.IsNullOrEmpty(siteName))
            {
                try
                {
                    using (SqlDataReader reader = SqlHelper.ExecuteReader(ConnectionString, SiteDetailsProc, AddParametersForSiteDetails(siteName, false, 0,0,0)))
                    {
                        if (reader.Read())
                        {
                            return reader[ColumnName].ToString();
                        }
                    }
                }
                catch (Exception exception)
                {
                    LogManager.WriteLog("DBHelper.GetSiteColumn::" + exception.Message, LogManager.enumLogLevel.Error);
                    return string.Empty;
                }
            }
            return string.Empty;
        }

        public static GuardianDataSet.SiteDetailsDataTable GetSiteList(bool isActiveChecked, Int32 securityUserID, Int32 status, Int32 StatusInterval)
        {
            GuardianDataSet.SiteDetailsDataTable siteDetails = new GuardianDataSet.SiteDetailsDataTable();
            try
            {
                GuardianDataSet dataSet = new GuardianDataSet();
                SqlHelper.FillDataset(ConnectionString, SiteDetailsProc, dataSet, new string[] { "SiteDetails" }, AddParametersForSiteDetails("", isActiveChecked, securityUserID, status, StatusInterval));
                siteDetails = dataSet.SiteDetails;
            }
            catch (Exception exception)
            {
                LogManager.WriteLog("DBHelper.GetSiteList::" + exception.Message, LogManager.enumLogLevel.Error);
            }
            return siteDetails;
        }

        public static DataTable GetSiteStatusHistory(string SiteCode)
        {
            DataSet HistorySet = new DataSet(); ;
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("SiteCode", SiteCode);
                SqlHelper.FillDataset(ConnectionString, StatusHistoryProc, HistorySet, new string[] { "SiteDetails" }, param);
                return HistorySet.Tables[0];
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("DBHelper.GetSiteHistory::" + ex.Message, LogManager.enumLogLevel.Error);
                return new DataTable();
            }
        }

        public static DataTable GetSiteStatusHistoryByTimeStamp(string SiteCode, DateTime timestamp)
        {
            DataSet HistorySet = new DataSet(); ;
            try
            {
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("SiteCode", SiteCode);
                param[1] = new SqlParameter("TimeStamp", timestamp);
                SqlHelper.FillDataset(ConnectionString, StatusHistoryProcByTimeStamp, HistorySet, new string[] { "SiteDetails" }, param);
                return HistorySet.Tables[0];
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("DBHelper.GetSiteStatusHistoryByTimeStamp::" + ex.Message, LogManager.enumLogLevel.Error);
                return new DataTable();
            }
        }

        public static void UpdateSiteStatus(string xml, string siteCode)
        {
            try
            {
                LogManager.WriteLog("DBHelper.UpdateSiteStatus Entry", LogManager.enumLogLevel.Info);
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("SiteName", siteCode);
                param[1] = new SqlParameter("xml", xml);
                SqlHelper.ExecuteNonQuery(ConnectionString, Constants.CONSTANT_USP_UPDATESITESTATS, param);
                LogManager.WriteLog("DBHelper.UpdateSiteStatus Exit", LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("DBHelper.GetSiteStatus::" + ex.Message, LogManager.enumLogLevel.Error);
            }
        }

        /// <summary>
        /// ///////////////////////////////
        /// </summary>
        /// <param name="Bar_Position_ID"></param>

        public static int GetInstallationNo(string Asset)
        {
            try
            {
                LogManager.WriteLog("DBHelper.GetInstallationNo Entry", LogManager.enumLogLevel.Info);
                int result = 0;

                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@Machine_Stock_No", Asset);
                param[1] = new SqlParameter("@Installation_No", SqlDbType.Int);
                param[1].Direction = ParameterDirection.Output;

                LogManager.WriteLog("DBHelper.GetInstallationNo Exit", LogManager.enumLogLevel.Info);
                SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.StoredProcedure, ToGetInstallationNumber, (SqlParameter[])param);
                if (param[1].Value != DBNull.Value)
                {
                     Int32.TryParse(param[1].Value.ToString(), out result);
                }
                return result;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("DBHelper.GetInstallationNo::" + ex.Message, LogManager.enumLogLevel.Error);
                return 0;
            }
        }
        /////////////////////////////        

    }
}
