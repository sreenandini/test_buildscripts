using System;
using System.Collections.Generic;
using System.Text;
using BMC.DBInterface.ExchangeConfig;
using System.Data;
using BMC.Common.ExceptionManagement;
using BMC.Common.LogManagement;

namespace BMC.Business.ExchangeConfig
{
    public class DBSettings
    {
        #region Declaration
        static Dictionary<string, string> DBEntries = new Dictionary<string, string>();
        #endregion Declaration

        #region Public Static Methods
        /// <summary>
        /// Get CMP Connection String
        /// </summary>
        /// <param name="ExchangeConnection"></param>
        /// <returns>CMP Connection String</returns>
        /// Method Revision History
        ///
        /// Author             Date              Description
        /// ---------------------------------------------------
        /// Anuradha        10-Dec-2008        Intial Version 
        /// 
        public static string CMPConnectionString(string ExchangeConnection)
        {
            string strConnectionString = string.Empty;            
            int SettingID = 0;
            try
            {
                Dictionary<int, string> dtSetting = new Dictionary<int, string>();
                dtSetting = DBBuilder.GetSetting(ExchangeConnection,

                DBBuilder.AddParameter<int>("@Setting_ID", System.Data.DbType.Int32, SettingID, 32),
                DBBuilder.AddParameter<string>("@Setting_Name", System.Data.DbType.String, DBConstants.CMPCONNECTIONSETTING, 500),
                DBBuilder.AddParameter<string>("@Setting_Default", System.Data.DbType.String, string.Empty, 500),
                DBBuilder.AddOutputParameter<string>("@Setting_Value", System.Data.DbType.String, string.Empty, 500));

                foreach (KeyValuePair<int, string> objValue in dtSetting)
                {
                    strConnectionString = objValue.Value.ToString();
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("CMPConnectionString" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
            }
            return strConnectionString;
        }

        /// <summary>
        /// Get Ticketing Connection String
        /// </summary>
        /// <param name="ExchangeConnection"></param>
        /// <returns>Ticketing Connection String</returns>
        /// Method Revision History
        ///
        /// Author             Date              Description
        /// ---------------------------------------------------
        /// Anuradha        10-Dec-2008        Intial Version 
        /// 
        public static string TicketingConnectionString(string ExchangeConnection)
        {
            string strTicketingConnectionString = string.Empty;            
            int SettingID = 0;
            try
            {
                Dictionary<int, string> dtSetting = new Dictionary<int, string>();
                dtSetting = DBBuilder.GetSetting(ExchangeConnection,

                DBBuilder.AddParameter<int>("@Setting_ID", System.Data.DbType.Int32, SettingID, 32),
                DBBuilder.AddParameter<string>("@Setting_Name", System.Data.DbType.String, DBConstants.TICKETINGCONNECTIONSETTING, 500),
                DBBuilder.AddParameter<string>("@Setting_Default", System.Data.DbType.String, string.Empty, 500),
                DBBuilder.AddOutputParameter<string>("@Setting_Value", System.Data.DbType.String, string.Empty, 500));

                foreach (KeyValuePair<int, string> objValue in dtSetting)
                {
                    strTicketingConnectionString = objValue.Value.ToString();
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("TicketingConnectionString" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
            }
            return strTicketingConnectionString;
        }

        /// <summary>
        /// Get Ticketing Connection String
        /// </summary>
        /// <param name="ExchangeConnection"></param>
        /// <returns>Ticketing Connection String</returns>
        /// Method Revision History
        ///
        /// Author             Date              Description
        /// ---------------------------------------------------
        /// Anuradha        10-Dec-2008        Intial Version 
        /// 
        public static string TicketingLocCodeString(string ExchangeConnection)
        {
            string strTicketingLocCodeString = string.Empty;          
            int SettingID = 0;
            try
            {
                Dictionary<int, string> dtSetting = new Dictionary<int, string>();
                dtSetting = DBBuilder.GetSetting(ExchangeConnection,

                DBBuilder.AddParameter<int>("@Setting_ID", System.Data.DbType.Int32, SettingID, 32),
                DBBuilder.AddParameter<string>("@Setting_Name", System.Data.DbType.String, DBConstants.TICKETLOCATIONCODENAME, 500),
                DBBuilder.AddParameter<string>("@Setting_Default", System.Data.DbType.String, string.Empty, 500),
                DBBuilder.AddOutputParameter<string>("@Setting_Value", System.Data.DbType.String, string.Empty, 500));

                foreach (KeyValuePair<int, string> objValue in dtSetting)
                {
                    strTicketingLocCodeString = objValue.Value.ToString();
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("TicketingConnectionString" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
            }
            return strTicketingLocCodeString;
        }

        public static string GetCertificateSettings(string certificateInformation, string ExchangeConnection)
        {
            string certificateInformationString = string.Empty;
            int SettingID = 0;
            try
            {
                Dictionary<int, string> dtSetting = new Dictionary<int, string>();
                dtSetting = DBBuilder.GetSetting(ExchangeConnection,

                DBBuilder.AddParameter<int>("@Setting_ID", System.Data.DbType.Int32, SettingID, 32),
                DBBuilder.AddParameter<string>("@Setting_Name", System.Data.DbType.String, certificateInformation, 500),
                DBBuilder.AddParameter<string>("@Setting_Default", System.Data.DbType.String, string.Empty, 500),
                DBBuilder.AddOutputParameter<string>("@Setting_Value", System.Data.DbType.String, string.Empty, 500));

                foreach (KeyValuePair<int, string> objValue in dtSetting)
                {
                    certificateInformationString = objValue.Value.ToString();
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("CertificateInformationString" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
            }
            return certificateInformationString;
        }

        public static string ClientNameString(string ExchangeConnection)
        {
            string strClientNameString = string.Empty;
            int SettingID = 0;
            try
            {
                Dictionary<int, string> dtSetting = new Dictionary<int, string>();
                dtSetting = DBBuilder.GetSetting(ExchangeConnection,

                DBBuilder.AddParameter<int>("@Setting_ID", System.Data.DbType.Int32, SettingID, 32),
                DBBuilder.AddParameter<string>("@Setting_Name", System.Data.DbType.String, DBConstants.ClientName, 500),
                DBBuilder.AddParameter<string>("@Setting_Default", System.Data.DbType.String, string.Empty, 500),
                DBBuilder.AddOutputParameter<string>("@Setting_Value", System.Data.DbType.String, string.Empty, 500));

                foreach (KeyValuePair<int, string> objValue in dtSetting)
                {
                    strClientNameString = objValue.Value.ToString();
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("ClientNameString" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
            }
            return strClientNameString;
        }

        public static string GetSettingValue(string ExchangeConnection,string strSettingName)
        {
            string sResult = string.Empty;
            int SettingID = 0;
            try
            {
                if (strSettingName != null && strSettingName != string.Empty)
                {
                    Dictionary<int, string> dtSetting = new Dictionary<int, string>();
                    dtSetting = DBBuilder.GetSetting(ExchangeConnection,

                  DBBuilder.AddParameter<int>("@Setting_ID", System.Data.DbType.Int32, SettingID, 32),
                  DBBuilder.AddParameter<string>("@Setting_Name", System.Data.DbType.String, strSettingName, 500),
                  DBBuilder.AddParameter<string>("@Setting_Default", System.Data.DbType.String, string.Empty, 500),
                DBBuilder.AddOutputParameter<string>("@Setting_Value", System.Data.DbType.String, string.Empty, 500));

                    foreach (KeyValuePair<int, string> objValue in dtSetting)
                    {
                        sResult = objValue.Value.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            return sResult;
        }

        public static List<string> GetServers()
        {
            return DBBuilder.GetServers();
        }

        //public static List<string> GetDatabases(string Server, string User, string Pass)
        //{
        //    return DBBuilder.GetDataBases(Server, User, Pass);
        //}

        //public static List<string> GetLanguages(string Server, string User, string Pass)
        //{
        //    return DBBuilder.GetLanguages(Server, User, Pass);
        //}

        public static bool RestoreDB(string strType,Dictionary<string, string> ServerInfo)
        {
            return DBBuilder.RestoreDB(strType, ServerInfo);
        }

        public static bool InsertSettings(string strSettingName,string strSettingValue)
        {
            return DBBuilder.InsertSettings(strSettingName, strSettingValue);
        }

        public static bool InsertTISSettings(string TISConnectionMode, string TISIPAddress, string TISPortNumber, string TISWebServiceURL, string TISTicketPrefix, string TISDataExchangePortNumber, string TISExternalWebServiceURL, string TISExternalCasinoCode)
        {
            return DBBuilder.InsertTISSettings(TISConnectionMode, TISIPAddress, TISPortNumber, TISWebServiceURL, TISTicketPrefix, TISDataExchangePortNumber, TISExternalWebServiceURL, TISExternalCasinoCode);
        }

        public static bool SetTicketLocationCode(int iLocCode)
        {
            return DBBuilder.SetTicketLocationCodeDB(iLocCode);
        }

        public static bool CheckDBExists(string SQLConnection, string DBName, int Timeout)
        {
          return  DBBuilder.CheckifDBExists(SQLConnection, DBName, Timeout);
        }

        public static bool RestoreDatabase(string SQLConnection, string sBackUPPath, string sRestorePath, string sDatabaseName)
        {
            return DBBuilder.RestoreDatabase(SQLConnection, sBackUPPath, sRestorePath, sDatabaseName);
        }
        public static bool CreateDatabase(string SQLConnection, string _strDataFilePath, string _strLogFilePath, string sDatabaseName)
        {
            return DBBuilder.CreateDatabase(SQLConnection, _strDataFilePath, _strLogFilePath, sDatabaseName);
        }
        public static bool DropDatabase(string SQLConnection, string sDatabaseName)
        {
            return DBBuilder.DropDatabase(SQLConnection, sDatabaseName);
        }
        public static void ExecuteScripts(string SQLConnection, string scriptFile)
        {
            DBBuilder.ExecuteScripts(SQLConnection, scriptFile);
        }

		public static bool GetSiteInfo()
        {
            return DBBuilder.GetSiteCount();
        }

        public static bool GetUserInfo()
        {
            return DBBuilder.GetUsersCount();
        }

        public static DataSet GetInitialSettings(string ExchangeConnectionString)
        {
            return DBBuilder.GetInitialSettings(ExchangeConnectionString);
        }

        public static DataSet GetGridViewColorRangeDetails(int gvtID, string ExchangeConnectionString)
        {
            return DBBuilder.GetGridViewColorRangeDetails(gvtID, ExchangeConnectionString);
        }

        public static DataSet GetGridViewTypeDetails(string ExchangeConnectionString)
        {
            return DBBuilder.GetGridViewTypeDetails(ExchangeConnectionString);
        }

        public static int InsertOrUpdateGridViewColorRangeDetails(int gvtID, decimal startValue, decimal endValue, string hexValue, string ExchangeConnectionString)
        {
            return DBBuilder.InsertOrUpdateGridViewColorRangeDetails(gvtID, startValue, endValue, hexValue, ExchangeConnectionString);
        }

        public static int DeleteGridViewColorRangeDetails(int gvtID, decimal startValue, decimal endValue, string ExchangeConnectionString)
        {
            return DBBuilder.DeleteGridViewColorRangeDetails(gvtID, startValue, endValue, ExchangeConnectionString);
        }

        #endregion Public Static Methods
    }
}
