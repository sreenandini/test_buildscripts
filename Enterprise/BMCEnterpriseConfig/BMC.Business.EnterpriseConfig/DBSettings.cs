using System;
using System.Collections.Generic;
using System.Text;
using BMC.DBInterface.EnterpriseConfig;
using System.Data;
using BMC.Common.ExceptionManagement;
using BMC.Common.LogManagement;

namespace BMC.Business.EnterpriseConfig
{
    public class DBSettings
    {
        #region Declaration
        static Dictionary<string, string> DBEntries = new Dictionary<string, string>();
        #endregion Declaration

        #region MeterAnalysis Connection String

        public static string MeterAnalysisConnectionString(string EnterpriseConnection)
        {
            string strConnectionString = string.Empty;            
            int SettingID = 0;
            try
            {
                Dictionary<int, string> dtSetting = new Dictionary<int, string>();
                dtSetting = DBBuilder.GetSetting(EnterpriseConnection,

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

        #endregion MeterAnalysis Connection String


        public static string GetSettingValue(string EnterpriseConnection,string strSettingName)
        {
            string sResult = string.Empty;
            int SettingID = 0;
            try
            {
                if (strSettingName != null && strSettingName != string.Empty)
                {
                    Dictionary<int, string> dtSetting = new Dictionary<int, string>();
                    dtSetting = DBBuilder.GetSetting(EnterpriseConnection,

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
        //public static List<string> GetServers()
        //{
        //    return DBBuilder.GetServers();
        //}

        //public static List<string> GetDatabases(string Server, string User, string Pass)
        //{
        //    return DBBuilder.GetDataBases(Server, User, Pass);
        //}

        public static bool RestoreDB(string strType,Dictionary<string, string> ServerInfo)
        {
            return DBBuilder.RestoreDB(strType, ServerInfo);
        }
        public static bool InsertSettings(string strSettingName,string strSettingValue)
        {
            return DBBuilder.InsertSettings(strSettingName, strSettingValue);

        }
        public static bool SetTicketLocationCode(int iLocCode)
        {
            return DBBuilder.SetTicketLocationCodeDB(iLocCode);

        }

        public static bool CheckDBExists(string SQLConnection, string DBName, int Timeout)
        {
          return  DBBuilder.CheckifDBExists(SQLConnection, DBName, Timeout);
        }
        public static bool RunFactoryResetScripts(string strScriptToRun)
        {
            return DBBuilder.RunFactoryResetScriptsDB(strScriptToRun);
        }
        public static DataTable GetInitialSettings()
        {
            return DBBuilder.GetInitialSettings();
        }
    }
}
