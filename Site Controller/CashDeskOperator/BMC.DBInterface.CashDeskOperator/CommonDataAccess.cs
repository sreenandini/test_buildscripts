using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using BMC.DataAccess;
using BMC.Common.ExceptionManagement;
using Microsoft.Win32;
using BMC.Common.Utilities;
using BMC.Common.Security;

namespace BMC.DBInterface.CashDeskOperator
{
    public class CommonDataAccess
    {
        #region "Private Variables"

        //private static string exchangeConnectionString;
        private static DataSet siteDetail;
        private static DataSet initialSettings;
        private static DataSet AppinitialSettings;
        private static string ticketingConnectionString;
        private static string cmpConnectionString;
        private static string strticketlocationcode;
        private static string strGloryPassword;
        private static IDictionary<string, string> dicGloryCDDetails;
        #endregion

        #region "Public Property"

        public static string ExchangeConnectionString
        {
            get
            {
                //if (string.IsNullOrEmpty(exchangeConnectionString))
                //    exchangeConnectionString = GetExchangeConnectionString();

                return ConnectionStringHelper.ExchangeConnectionString;
            }
        }

        public static string TicketingConnectionString
        {
            get
            {
                return ConnectionStringHelper.TicketingConnectionString;
            }
        }

        public static DataSet SiteDetail
        {
            get
            {
                if (siteDetail == null)
                {
                    try
                    {
                        siteDetail = SqlHelper.ExecuteDataset(ConnectionStringHelper.ExchangeConnectionString, CommandType.StoredProcedure, DBConstants.CONSTANT_RSP_RSP_GETSITEDETAILS);
                    }
                    catch (Exception ex)
                    {
                        ExceptionManager.Publish(ex);
                        siteDetail = null;
                    }
                }
                return siteDetail;
            }
        }

        public static DataSet InitialSettings
        {
            get
            {
                //if (initialSettings == null)
                //{
                try
                {
                    initialSettings = SqlHelper.ExecuteDataset(ExchangeConnectionString, CommandType.StoredProcedure, DBConstants.CONSTANT_RSP_GETINITIALSETTINGS);
                }
                catch (Exception ex)
                {
                    ExceptionManager.Publish(ex);
                    initialSettings = null;
                }
                //}
                return initialSettings;
            }
        }


        public static DataSet AppInitialSettings
        {
            get
            {
                //if (initialSettings == null)
                //{
                try
                {
                    AppinitialSettings = SqlHelper.ExecuteDataset(ExchangeConnectionString, CommandType.StoredProcedure, DBConstants.CONSTANT_RSP_GETAPPSETTINGS);
                }
                catch (Exception ex)
                {
                    ExceptionManager.Publish(ex);
                    AppinitialSettings = null;
                }
                //}
                return AppinitialSettings;
            }
        }

        public static void UpdateAppSettings_SortOrder(string AppSettingKey, string AppSettingValue)
        {
            try
            {
                DataBaseServiceHandler.ExecuteNonQuery(
                   ExchangeConnectionString,
                   CommandType.StoredProcedure,
                   DBConstants.CONSTANT_USP_APPSETTINGS_SORTORDER,
                   DataBaseServiceHandler.AddParameter<string>("AppSettingName", DbType.String, AppSettingKey),
                   DataBaseServiceHandler.AddParameter<string>("Value", DbType.String, AppSettingValue));
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        public static void UpdateSettings(string SettingNamem, string SettingValue)
        {
            try
            {
                DataBaseServiceHandler.ExecuteNonQuery(
                    ExchangeConnectionString,
                    CommandType.StoredProcedure,
                    DBConstants.CONSTANT_USP_SETSETTINGSVALUE,
                    DataBaseServiceHandler.AddParameter<string>("SettingName", DbType.String, SettingNamem),
                    DataBaseServiceHandler.AddParameter<string>("SettingValue", DbType.String, SettingValue));

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        public static void UpdateTicketExpire(int value)
        {
            try
            {
                DataBaseServiceHandler.ExecuteNonQuery(
                    TicketingConnectionString,
                    CommandType.StoredProcedure,
                    DBConstants.CONSTANT_USP_SETTICKETEXPIRE,
                    DataBaseServiceHandler.AddParameter<int>("Value", DbType.String, value));

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        public static DataTable GetEmployeeCardPollingData()
        {
            try
            {
                return SqlHelper.ExecuteDataset(ExchangeConnectionString, CommandType.StoredProcedure, "rsp_GetEmployeeCardPollingData",
                    new SqlParameter[]{new SqlParameter("@Installation_No",0)}).Tables[0];
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                return new DataTable();
            }
        }

        public static void UpdateGMUSiteCodeStatus(int Installation_No, int Status)
        {
            try
            {
                DataBaseServiceHandler.ExecuteNonQuery(
                    ExchangeConnectionString,
                    CommandType.StoredProcedure,
                    DBConstants.CONSTANT_USP_SETGMUSITECODESTATUS,
                    DataBaseServiceHandler.AddParameter<int>("@InstallationNo", DbType.Int32, Installation_No),
                    DataBaseServiceHandler.AddParameter<int>("@Status", DbType.Int32, Status));

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        public static void UpdateEmployeeCardPolling(string EmployeecardNo, int InstallationNo)
        {
            try
            {
                DataBaseServiceHandler.ExecuteNonQuery(ExchangeConnectionString, CommandType.StoredProcedure, "usp_UpdateEmployeecardPolling",
                    DataBaseServiceHandler.AddParameter<int>("@InstallationNo", DbType.Int32, InstallationNo),
                    DataBaseServiceHandler.AddParameter<string>("@EmpCardNo ", DbType.String, EmployeecardNo));
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
        }


        //public static string TicketingConnectionString
        //{           
        //  get
        //{
        //  ticketingConnectionString = (ticketingConnectionString == null) ? GetTicketingConnectionString(ExchangeConnectionString) : ticketingConnectionString;
        // return ticketingConnectionString;
        //}
        //}

        public static string CMPConnectionString
        {
            get
            {
                cmpConnectionString = (cmpConnectionString == null) ? GetCMPConnectionString(ExchangeConnectionString) : cmpConnectionString;
                return cmpConnectionString;
            }
        }

        public static string TicketLocationCode
        {
            get
            {
                strticketlocationcode = (strticketlocationcode == null) ? GetTicketingLocCode(ExchangeConnectionString) : strticketlocationcode;
                return strticketlocationcode;
            }

        }

        #endregion

        #region "Private Function"



        /// <summary>
        /// To connect to ticket db 
        /// </summary>
        /// <param name="strConnect"></param>
        /// <returns type=string >Connect</returns>
        public static string GetTicketingConnectionString(string Connect)
        {
            string TicketConnection = string.Empty;
            //try
            //{
            //    SqlParameter[] sqlparams = GetSettingParameterDB(DBConstants.CONST_SP_PARAM_TICKETCONNECTION);
            //    SqlHelper.ExecuteNonQuery(Connect, CommandType.StoredProcedure, DBConstants.CONST_SP_GETSETTING, sqlparams);
            //    TicketConnection = (sqlparams[3].Value != null || sqlparams[3].Value.ToString() != string.Empty) ? Convert.ToString(sqlparams[3].Value) : string.Empty;                
            //}

            //catch (Exception exTicketConnect)
            //{
            //    ExceptionManager.Publish(exTicketConnect);
            //    TicketConnection = string.Empty;
            //}
            TicketConnection = DatabaseHelper.GetTicketingConnectionString();

            return TicketConnection;
        }

        /// <summary>
        /// To connect to CMP db 
        /// </summary>
        /// <param name="strConnect"></param>
        /// <returns type=string >Connect</returns>
        private static string GetCMPConnectionString(string Connect)
        {
            string CMPConnection = string.Empty;
            try
            {
                SqlParameter[] sqlparams = GetSettingParameterDB(DBConstants.CONST_SP_PARAM_CMPCONNECTION);
                SqlHelper.ExecuteNonQuery(Connect, CommandType.StoredProcedure, DBConstants.CONST_SP_GETSETTING, sqlparams);
                CMPConnection = (sqlparams[3].Value != null || sqlparams[3].Value.ToString() != string.Empty) ? Convert.ToString(sqlparams[3].Value) : string.Empty;

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return CMPConnection;
        }

        /// <summary>
        /// To get ticket location code from exchange DB
        /// </summary>
        /// <param name="strConnect"></param>
        /// <returns type=string >Connect</returns>
        private static string GetTicketingLocCode(string Connect)
        {
            string strTicketLocCode = string.Empty;
            try
            {
                SqlParameter[] sqlparams = GetSettingParameterDB(DBConstants.TICKETLOCATIONCODENAME);
                SqlHelper.ExecuteNonQuery(Connect, CommandType.StoredProcedure, DBConstants.CONST_SP_GETSETTING, sqlparams);
                strTicketLocCode = (sqlparams[3].Value != null || sqlparams[3].Value.ToString() != string.Empty) ? Convert.ToString(sqlparams[3].Value) : string.Empty;

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                strTicketLocCode = "";
            }
            return strTicketLocCode;
        }
        #endregion

        #region "Public Functions"


        public DataTable GetInstallationDetails(int DatapakSerialNo, int InstallationNumber, bool ShouldIncludeVirtual, bool ShouldSortbyZone)
        {
            DataTable dtInstallationDetails = new DataTable();
            //DataBaseServiceHandler.ConnectionString = ExchangeConnectionString;
            try
            {
                dtInstallationDetails = DataBaseServiceHandler.Fill(ExchangeConnectionString, CommandType.StoredProcedure, DBConstants.CONST_SP_GET_INSTALLTION_DETAILS_PROC, dtInstallationDetails,
                    DataBaseServiceHandler.AddParameter<int>(DBConstants.CONST_PARAM_DATAPAK_SERIAL, DbType.Int32, DatapakSerialNo),
                    DataBaseServiceHandler.AddParameter<int>(DBConstants.CONST_PARAM_INSTALLATION_NUMBER, DbType.Int32, InstallationNumber),
                    DataBaseServiceHandler.AddParameter<bool>(DBConstants.CONST_PARAM_INCLUDE_VIRTUAL, DbType.Boolean, ShouldIncludeVirtual),
                    DataBaseServiceHandler.AddParameter<bool>(DBConstants.CONST_PARAM_SORT_BY_ZONE, DbType.Boolean, ShouldSortbyZone));

                if (dtInstallationDetails.Rows.Count <= 0)
                    dtInstallationDetails = new DataTable();

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return dtInstallationDetails;
        }

        public static DataTable GetInstallationDetails()
        {
            DataTable dtInstallationDetails = new DataTable();
            //DataBaseServiceHandler.ConnectionString = ExchangeConnectionString;

            try
            {
                dtInstallationDetails = DataBaseServiceHandler.Fill(ExchangeConnectionString, CommandType.StoredProcedure, DBConstants.CONST_SP_GET_INSTALLTION_DETAILS_PROC, dtInstallationDetails);

                if (dtInstallationDetails.Rows.Count <= 0)
                    dtInstallationDetails = new DataTable();

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return dtInstallationDetails;
        }

        public static DataTable GetInstallationDetailsForHourly()
        {
            DataTable dtInstallationDetails = new DataTable();

            try
            {
                dtInstallationDetails = DataBaseServiceHandler.Fill(ExchangeConnectionString, CommandType.StoredProcedure, "rsp_GetAllInstallationDetailsForHourly", dtInstallationDetails);

                if (dtInstallationDetails.Rows.Count <= 0)
                    dtInstallationDetails = new DataTable();

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return dtInstallationDetails;
        }

        public DataTable GetInstallationDetailsForReports(string reportType)
        {
            DataTable dtInstallationDetails = new DataTable();
            //DataBaseServiceHandler.ConnectionString = ExchangeConnectionString;

            try
            {
                dtInstallationDetails = DataBaseServiceHandler.Fill(ExchangeConnectionString, CommandType.StoredProcedure, DBConstants.CONST_SP_GET_INSTALLTION_DETAILS_REPORTS_PROC, dtInstallationDetails,
                    DataBaseServiceHandler.AddParameter<string>(DBConstants.CONST_PARAM_REPORT_TYPE, DbType.String, reportType));

                if (dtInstallationDetails.Rows.Count <= 0)
                    dtInstallationDetails = new DataTable();

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return dtInstallationDetails;
        }

        public static DataTable GetInstallationList()
        {
            DataSet InstallationList = new DataSet();

            try
            {
                SqlHelper.FillDataset(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, DBConstants.CONST_SP_GET_INSTALLTION_LIST_PROC, InstallationList, new string[] { "InstallationList" });
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            if (InstallationList.Tables.Count > 0)
                return InstallationList.Tables[0];
            else
                return new DataTable();
        }

        public DataTable GetActiveZones()
        {
            DataSet dtZones = new DataSet();

            try
            {
                SqlHelper.FillDataset(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, "rsp_GetActiveZones", dtZones, new string[] { "ZoneList" });
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            if (dtZones.Tables.Count > 0)
                return dtZones.Tables[0];
            else
                return new DataTable();
        }

        public DataSet GetMachineDetailsTreasury(string TreasuryNumber)
        {
            DataSet MachineDetailTreasury = new DataSet();

            try
            {
                //DataBaseServiceHandler.ConnectionString = ExchangeConnectionString;
                MachineDetailTreasury = DataBaseServiceHandler.Fill(ExchangeConnectionString, CommandType.StoredProcedure, DBConstants.CONSTANT_RSP_GETMCDETAILSVIATREASURY, MachineDetailTreasury,
                    DataBaseServiceHandler.AddParameter<string>("Treasury_ID", DbType.String, TreasuryNumber));
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return MachineDetailTreasury;
        }

        public DataTable GetUserRoles(string UserName, string Password)
        {
            DataTable userRoles = new DataTable();
            try
            {
                userRoles = DataBaseServiceHandler.Fill(ExchangeConnectionString, CommandType.StoredProcedure, DBConstants.CONST_SP_GET_USER_ROLE_PROC, userRoles,
                    DataBaseServiceHandler.AddParameter<string>(DBConstants.CONST_PARAM_USER_NAME, DbType.String, UserName),
                    DataBaseServiceHandler.AddParameter<string>(DBConstants.CONST_PARAM_USER_PASSWORD, DbType.String, Password));
            }
            catch (Exception ex)
            {
                if (ex.Message == "Connectionstring Not Found.")
                    throw ex;
                else
                    ExceptionManager.Publish(ex);
            }
            return userRoles;
        }

        public DataTable GetAllZones()
        {
            DataSet dtAllZones = new DataSet();

            try
            {
                SqlHelper.FillDataset(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, "rsp_GetAllZones", dtAllZones, new string[] { "ZoneList" });
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            if (dtAllZones.Tables.Count > 0)
                return dtAllZones.Tables[0];
            else
                return new DataTable();
        }

        public DataTable GetMachineCategory()
        {
            DataSet dtMachineCategories = new DataSet();

            try
            {
                SqlHelper.FillDataset(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, "rsp_GetMachineCategory", dtMachineCategories, new string[] { "MachineCategoryList" });
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            if (dtMachineCategories.Tables.Count > 0)
                return dtMachineCategories.Tables[0];
            else
                return new DataTable();
        }

        /// <summary>
        /// Get the settings for CMP Kiosk
        /// </summary>
        /// <param name="sqlparams"></param>
        /// <param name="strConnect"></param>
        /// <returns >string</returns>
        public string ExecuteQuery(string strConnect, SqlParameter[] sqlparams)
        {
            string strReturnValue = string.Empty;
            try
            {
                SqlHelper.ExecuteNonQuery(strConnect, System.Data.CommandType.StoredProcedure, DBConstants.CONST_SP_GETSETTING, sqlparams);
                if (sqlparams[3].Value != null || sqlparams[3].Value.ToString() != string.Empty)
                {
                    strReturnValue = Convert.ToString(sqlparams[3].Value);
                }
                else
                {
                    strReturnValue = string.Empty;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            return strReturnValue;
        }

        #endregion

        #region "Public Static Function"

        #region Glory Cash Dispenser
        public static string GetGloryServerPassword()
        {
            try
            {
                string strLocalGloryPassword = "";

                if (!String.IsNullOrEmpty(strGloryPassword))
                {
                    return strGloryPassword;
                }

                strLocalGloryPassword = BMCRegistryHelper.GetRegKeyValue("Cashmaster", "CDServerPwd");
                strLocalGloryPassword = BMC.Common.Security.CryptEncode.Decrypt(strLocalGloryPassword);
                strGloryPassword = strLocalGloryPassword;

            }
            catch (Exception ex)
            {
                BMC.Common.LogManagement.LogManager.WriteLog("Unable to find Glory Server Password from Registry", BMC.Common.LogManagement.LogManager.enumLogLevel.Info);
                ExceptionManager.Publish(ex);
                return null;
            }
            return strGloryPassword;
        }
        public static IDictionary<string, string> GetGloryServerDetails()
        {
            try
            {
                string[] Separator = new string[1] { ";" };
                Dictionary<string, string> dicGloryCDDetailsInt = new Dictionary<string, string>();

                if (dicGloryCDDetails != null && dicGloryCDDetails.Count > 0)
                {
                    return dicGloryCDDetails;
                }
                string[] strVal = { "ServerName", "PortNo", "UserName", "DeviceName", "SSL" };
                foreach (string sKey in strVal)
                {
                    dicGloryCDDetailsInt.Add(sKey, string.Empty);
                }

                string strGloryConnectionString = BMCRegistryHelper.GetRegKeyValue("Cashmaster", "CDServerInfo");
                strGloryConnectionString = BMC.Common.Security.CryptEncode.Decrypt(strGloryConnectionString);

                string[] sValue = strGloryConnectionString.Split(Separator, StringSplitOptions.RemoveEmptyEntries);
                if (sValue.Count<string>() == 5)
                {
                    dicGloryCDDetailsInt["ServerName"] = sValue[0];
                    dicGloryCDDetailsInt["PortNo"] = sValue[1];
                    dicGloryCDDetailsInt["UserName"] = sValue[2];
                    dicGloryCDDetailsInt["DeviceName"] = sValue[3];
                    dicGloryCDDetailsInt["SSL"] = sValue[4];
                    dicGloryCDDetails = dicGloryCDDetailsInt;
                }
                else
                    return null;


            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return null;
            }
            return dicGloryCDDetails;
        }
        #endregion

        public static SqlParameter AddOutputparameter(string Paramtername, Object ParamValue)
        {
            SqlParameter objParam = new SqlParameter(Paramtername, ParamValue);
            objParam.Direction = ParameterDirection.Output;
            return objParam;
        }

        public static string GetSettingValue(string strSettingName)
        {
            return GetSettingValue(strSettingName, string.Empty);
        }

        public static string GetSettingValue(string strSettingName, string defaultValue)
        {
            SqlParameter ParamValue = new SqlParameter();
            ParamValue.ParameterName = DBConstants.CONST_SP_PARAM_SETTINGVALUE;
            ParamValue.Direction = ParameterDirection.Output;
            ParamValue.Value = string.Empty;
            ParamValue.SqlDbType = SqlDbType.VarChar;
            ParamValue.Size = 300;

            try
            {
                if (strSettingName != null)
                {
                    //DataBaseServiceHandler.ConnectionString = ExchangeConnectionString;
                    DataBaseServiceHandler.ExecuteNonQuery(ExchangeConnectionString, CommandType.StoredProcedure, DBConstants.CONST_SP_GETSETTING,
                        DataBaseServiceHandler.AddParameter<int>(DBConstants.CONST_SP_PARAM_SETTINGID, DbType.Int32, 0, ParameterDirection.Input),
                        DataBaseServiceHandler.AddParameter<string>(DBConstants.CONST_SP_PARAM_SETTINGNAME, DbType.String, strSettingName.Trim()),
                        DataBaseServiceHandler.AddParameter<string>(DBConstants.CONST_SP_PARAM_SETTINGDEFAULT, DbType.String, defaultValue),
                        ParamValue);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            return ParamValue.Value.ToString();
        }

        public static string GetVersion()
        {
            string sResult = DataBaseServiceHandler.ExecuteScalar<string>(ExchangeConnectionString, CommandType.Text, "Select VersionName from VersionHistory");

            return sResult;
        }

        /// <summary>
        /// To set parameters for Get Setting SP
        /// </summary>
        /// <param name="strSettingName">string</param>
        /// <returns type=SqlParameter[] >sp_parames</returns>
        public static SqlParameter[] GetSettingParameterDB(string SettingName)
        {
            SqlParameter[] sp_parames = null;
            try
            {

                if (SettingName != null)
                {
                    sp_parames = new SqlParameter[5];

                    sp_parames[0] = new SqlParameter(DBConstants.CONST_SP_PARAM_SETTINGID, 0);
                    sp_parames[1] = new SqlParameter(DBConstants.CONST_SP_PARAM_SETTINGNAME, SettingName.Trim());
                    sp_parames[2] = new SqlParameter(DBConstants.CONST_SP_PARAM_SETTINGDEFAULT, string.Empty);

                    sp_parames[3] = new SqlParameter();
                    sp_parames[3].ParameterName = DBConstants.CONST_SP_PARAM_SETTINGVALUE;
                    sp_parames[3].Direction = ParameterDirection.Output;
                    sp_parames[3].Value = string.Empty;
                    sp_parames[3].SqlDbType = SqlDbType.VarChar;
                    sp_parames[3].Size = 200;

                    SqlParameter ReturnValue = new SqlParameter();
                    ReturnValue.ParameterName = DBConstants.CONST_SP_PARAM_RETURNVALUE;
                    ReturnValue.Direction = ParameterDirection.ReturnValue;
                    sp_parames[4] = ReturnValue;

                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            return sp_parames;
        }

        public static bool TestSQLConnection(string strConnectionString)
        {
            bool bStatus = false;

            try
            {
                SqlConnection cn = new SqlConnection(strConnectionString);
                cn.Open();
                cn.Close();
                bStatus = true;
            }
            catch (System.Data.SqlClient.SqlException sqex)
            {
                if (sqex.Message == "A network-related or instance-specific error occurred while establishing a connection to SQL Server. The server was not found or was not accessible. Verify that the instance name is correct and that SQL Server is configured to allow remote connections. (provider: Named Pipes Provider, error: 40 - Could not open a connection to SQL Server)")
                {
                    throw sqex;
                }
            }
            catch (Exception extest)
            {
                bStatus = false;
            }

            return bStatus;
        }

        public static bool UserBasedPositionExists(int userID)
        {
            bool bStatus = false;
            SqlParameter[] sqlparams = null;
            try
            {
                sqlparams = new SqlParameter[2];

                sqlparams[0] = new SqlParameter();
                sqlparams[0].ParameterName = "@UserID";
                sqlparams[0].Direction = ParameterDirection.Input;
                sqlparams[0].SqlDbType = SqlDbType.Int;
                sqlparams[0].Value = userID;
                sqlparams[1] = new SqlParameter();
                sqlparams[1].ParameterName = "@IsExists";
                sqlparams[1].Direction = ParameterDirection.Output;
                sqlparams[1].SqlDbType = SqlDbType.Bit;

                object obj = SqlHelper.ExecuteScalar(ExchangeConnectionString, CommandType.StoredProcedure, "dbo.rsp_UserPositionExists", sqlparams);
                if (sqlparams[1].Value != DBNull.Value)
                {
                    bStatus = Convert.ToBoolean(sqlparams[1].Value);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return bStatus;
        }
        #endregion

        /// <summary>
        /// To set parameters for Get Setting SP
        /// </summary>
        /// <param name="strSettingName">string</param>
        /// <returns type=SqlParameter[] >sp_parames</returns>
        public static string GetSiteServiceStatus()
        {
            SqlParameter[] sqlparams = null;
            string siteServiceStatus = string.Empty;

            try
            {
                sqlparams = new SqlParameter[1];

                sqlparams[0] = new SqlParameter();
                sqlparams[0].ParameterName = "@SiteServiceStatus";
                sqlparams[0].Direction = ParameterDirection.Output;
                sqlparams[0].SqlDbType = SqlDbType.Xml;
                sqlparams[0].Size = -1;

                SqlHelper.ExecuteNonQuery(ExchangeConnectionString, CommandType.StoredProcedure, "dbo.rsp_GetSiteServiceStatus", (SqlParameter[])sqlparams);
                if (sqlparams[0].Value != DBNull.Value)
                {
                    siteServiceStatus = sqlparams[0].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            return siteServiceStatus;
        }
    }

    public class ConnectionStringHelper
    {
        private static string exchangeConnectionString;
        private static string ticketingConnectionString;

        public static string ExchangeConnectionString
        {
            get
            {
                if (string.IsNullOrEmpty(exchangeConnectionString))
                    exchangeConnectionString = GetExchangeConnectionString();

                return exchangeConnectionString;
            }
        }

        public static string TicketingConnectionString
        {
            get
            {
                if (string.IsNullOrEmpty(ticketingConnectionString))
                    ticketingConnectionString = GetTicketingConnectionString();

                return ticketingConnectionString;
            }
        }

        public static string GetExchangeConnectionString(string strConnectionString)
        {
            try
            {
                //bool bUseHex = true;

                if (!strConnectionString.ToUpper().Contains("SERVER"))
                {
                    //BGSGeneral.cConstants objBGSConstants = new BGSGeneral.cConstants();
                    //BGSEncryptDecrypt.clsBlowFish objDecrypt = new BGSEncryptDecrypt.clsBlowFish();
                    //string strKey = objBGSConstants.ENCRYPTIONKEY;
                    //strConnectionString = objDecrypt.DecryptString(ref strConnectionString, ref strKey, ref bUseHex);
                    strConnectionString = BMC.Common.Security.CryptEncode.Decrypt(strConnectionString);
                }

                if (!strConnectionString.ToUpper().Contains("SERVER"))
                    // exchangeConnectionString = strConnectionString;
                    //else
                    throw new Exception("Error Decrypting Registry");

            }
            catch (Exception ex)
            {
                if (ex.Message == "Connectionstring Not Found.")
                    throw ex;
                else
                    ExceptionManager.Publish(ex);
            }
            return strConnectionString;
        }

        public static string GetTicketConnectionString(string strTicketConnectionString)
        {
            try
            {
                if (!strTicketConnectionString.ToUpper().Contains("SERVER"))
                {
                    strTicketConnectionString = BMC.Common.Security.CryptEncode.Decrypt(strTicketConnectionString);
                }

                if (!strTicketConnectionString.ToUpper().Contains("SERVER"))
                    //ticketingConnectionString = strTicketConnectionString;
                    //else
                    throw new Exception("Error Decrypting Registry");

            }
            catch (Exception ex)
            {
                if (ex.Message == "Connectionstring Not Found.")
                    throw ex;
                else
                    ExceptionManager.Publish(ex);
            }
            return strTicketConnectionString;
        }

        //public static string SetCurrentExchangeConnectionString()
        //{
        //    try
        //    {
        //        bool bUseHex = true;
        //        RegistryKey regKeyConnectionString = Registry.LocalMachine.OpenSubKey("Software\\Honeyframe\\Cashmaster");
        //        string strConnectionString = regKeyConnectionString.GetValue("SQLConnect").ToString();
        //        regKeyConnectionString.Close();

        //        if (!strConnectionString.ToUpper().Contains("SERVER"))
        //        {
        //            BGSGeneral.cConstants objBGSConstants = new BGSGeneral.cConstants();
        //            BGSEncryptDecrypt.clsBlowFish objDecrypt = new BGSEncryptDecrypt.clsBlowFish();
        //            string strKey = objBGSConstants.ENCRYPTIONKEY;
        //            strConnectionString = objDecrypt.DecryptString(ref strConnectionString, ref strKey, ref bUseHex);
        //        }

        //        if (strConnectionString.ToUpper().Contains("SERVER"))
        //            exchangeConnectionString = strConnectionString;
        //        else
        //            throw new Exception("Error Decrypting Registry");

        //    }
        //    catch (Exception ex)
        //    {
        //        if (ex.Message == "Connectionstring Not Found.")
        //            throw ex;
        //        else
        //            ExceptionManager.Publish(ex);
        //    }
        //    return exchangeConnectionString;
        //}

        //public static string SetCurrentTicketConnectionString()
        //{
        //    try
        //    {
        //        RegistryKey regKeyConnectionString = Registry.LocalMachine.OpenSubKey("Software\\Honeyframe\\Cashmaster");
        //        string strConnectionString = regKeyConnectionString.GetValue("TicketingSQLConnect").ToString();
        //        regKeyConnectionString.Close();

        //        if (!strConnectionString.ToUpper().Contains("SERVER"))
        //        {
        //            strConnectionString = BMC.Common.Security.CryptEncode.Decrypt(strConnectionString);
        //        }

        //        if (strConnectionString.ToUpper().Contains("SERVER"))
        //            ticketingConnectionString = strConnectionString;
        //        else
        //            throw new Exception("Error Decrypting Registry");

        //    }
        //    catch (Exception ex)
        //    {
        //        if (ex.Message == "Connectionstring Not Found.")
        //            throw ex;
        //        else
        //            ExceptionManager.Publish(ex);
        //    }
        //    return ticketingConnectionString;
        //}
        //
        private static string GetExchangeConnectionString()
        {
            try
            {
                if (!string.IsNullOrEmpty(exchangeConnectionString))
                {
                    return exchangeConnectionString;
                }

                string strConnectionString = BMCRegistryHelper.GetRegKeyValue("Cashmaster", "SQLConnect");
                if (!strConnectionString.ToUpper().Contains("SERVER"))
                {

                    strConnectionString = BMC.Common.Security.CryptEncode.Decrypt(strConnectionString);
                   

                }

                if (strConnectionString.ToUpper().Contains("SERVER"))
                    exchangeConnectionString = strConnectionString;
                else
                    throw new Exception("Error Decrypting Registry");

            }
            catch (Exception ex)
            {
                exchangeConnectionString = null;
                if (ex.Message == "Connectionstring Not Found.")
                    throw ex;
                else
                    ExceptionManager.Publish(ex);
            }
            return exchangeConnectionString;
        }//

        private static string GetTicketingConnectionString()
        {
            try
            {
                if (!string.IsNullOrEmpty(ticketingConnectionString))
                {
                    return ticketingConnectionString;
                }
                
                string strConnectionString = BMCRegistryHelper.GetRegKeyValue("Cashmaster", "TicketingSQLConnect");
                if (!strConnectionString.ToUpper().Contains("SERVER"))
                {
                    strConnectionString = BMC.Common.Security.CryptEncode.Decrypt(strConnectionString);
                }

                if (strConnectionString.ToUpper().Contains("SERVER"))
                    ticketingConnectionString = strConnectionString;
                else
                    throw new Exception("Error Decrypting Registry");
            }
            catch (Exception ex)
            {
                ticketingConnectionString = null;
                if (ex.Message == "Connectionstring Not Found.")
                    throw ex;
                else
                    ExceptionManager.Publish(ex);
            }
            return ticketingConnectionString;
        }
    }

}
