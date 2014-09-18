using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using BMC.Common.ConfigurationManagement;
using BMC.Common.ExceptionManagement;
using BMC.DataAccess;
using Microsoft.Win32;
using BMC.Common.LogManagement;
using System.Globalization;
using Microsoft.VisualBasic;
using System.Diagnostics;
using BMC.Common.Utilities;

namespace BMC.HourlyDailyReadJobs
{
    class DBConnect
    {
        #region Declarations
        private static string exchangeConnectionString;
        private String[] format = { "dd MMM yyyy ", "dd MMM yyyy HH:m:ss" };

        System.Threading.ManualResetEvent mEvent = new System.Threading.ManualResetEvent(false);

        #endregion

        #region Private Functions

        private static SqlParameter AddParameter<T>(string ParamName, SqlDbType DataType, T Value, int Size)
        {
            SqlParameter Param = new SqlParameter();
            Param.SqlDbType = DataType;
            Param.ParameterName = ParamName;
            Param.Value = Value;
            Param.Size = Size;
            return Param;
        }

        private static SqlParameter AddParameter<T>(string ParamName, SqlDbType DataType, T Value)
        {
            SqlParameter Param = new SqlParameter();
            Param.SqlDbType = DataType;
            Param.ParameterName = ParamName;
            Param.Value = Value;
            return Param;
        }

        private static SqlParameter AddOutputParameter<T>(string ParamName, SqlDbType DataType, T Value)
        {
            SqlParameter Param = new SqlParameter();
            Param.SqlDbType = DataType;
            Param.ParameterName = ParamName;
            Param.Value = Value;
            Param.Direction = ParameterDirection.Output;
            return Param;
        }

        private static SqlParameter AddReturnParameter<T>(string ParamName, SqlDbType DataType)
        {
            SqlParameter Param = new SqlParameter();
            Param.SqlDbType = DataType;
            Param.ParameterName = ParamName;
            Param.Direction = ParameterDirection.ReturnValue;
            return Param;
        }

        private int ExecuteDailyRead(int InstallationNumber, DateTime dtTheDate)
        {
            int iResult = -1;
            int iValue = 0;
            try
            {
                var ReturnValue = AddReturnParameter<int>(DBConstants.CONST_PARAM_RETURNVALUE, SqlDbType.Int);
                iValue = SqlHelper.ExecuteNonQuery(ExchangeConnectionString, System.Data.CommandType.StoredProcedure, DBConstants.CONST_SP_USP_DAILY_READ,
                    AddParameter<int>(DBConstants.CONST_PARAM_THEINSTALLATION, SqlDbType.Int, InstallationNumber),
                    AddParameter<DateTime>(DBConstants.CONST_PARAM_THEDATETIME, SqlDbType.DateTime, Convert.ToDateTime(dtTheDate.ToString(format[1], DateTimeFormatInfo.InvariantInfo))), ReturnValue);
                if (iValue > 0)
                {
                    if (ReturnValue != null)
                    {
                        if (ReturnValue.Value != DBNull.Value || ReturnValue.Value.ToString() != string.Empty)
                        {
                            iResult = int.Parse(ReturnValue.Value.ToString());
                        }
                    }

                }

                LogManager.WriteLog("SP Excecution for DailyRead: " + iValue, LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                iResult = -1;
            }
            return iResult;
        }

        private static DataTable GetInstallationDetails(int DatapakSerialNo, int InstallationNumber, bool ShouldIncludeVirtual, bool ShouldSortbyZone)
        {
            DataSet dsinstallationDetails;
            DataTable dtInstallationDetails = new DataTable();

            try
            {

                //dsinstallationDetails = SqlHelper.ExecuteDataset(ExchangeConnectionString, System.Data.CommandType.StoredProcedure, DBConstants.CONST_SP_GET_INSTALLTION_DETAILS_PROC,
                //   AddParameter<int>(DBConstants.CONST_PARAM_DATAPAK_SERIAL, SqlDbType.Int, DatapakSerialNo),
                //    AddParameter<int>(DBConstants.CONST_PARAM_INSTALLATION_NUMBER, SqlDbType.Int, InstallationNumber),
                //    AddParameter<bool>(DBConstants.CONST_PARAM_INCLUDE_VIRTUAL, SqlDbType.Bit, ShouldIncludeVirtual),
                //    AddParameter<bool>(DBConstants.CONST_PARAM_SORT_BY_ZONE, SqlDbType.Bit, ShouldSortbyZone));
                dsinstallationDetails = SqlHelper.ExecuteDataset(exchangeConnectionString, CommandType.Text, "SELECT * FROM installation WHERE end_date IS NULL");
                if (dsinstallationDetails != null)
                    if (dsinstallationDetails.Tables.Count > 0)
                        dtInstallationDetails = dsinstallationDetails.Tables[0];
                    else dtInstallationDetails = new DataTable();

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return dtInstallationDetails;
        }

        private int ExecuteHourlyVTP(int InstallationNumber, DateTime dtTheDate, int iTheHour, bool isRead)
        {
            int iResult = -1;
            int iValue = 0;
            try
            {

                var ReturnValue = AddReturnParameter<int>(DBConstants.CONST_PARAM_RETURNVALUE, SqlDbType.Int);
                iValue = SqlHelper.ExecuteNonQuery(ExchangeConnectionString, System.Data.CommandType.StoredProcedure, DBConstants.CONST_SP_USP_HOURLY_VTP,
                    AddParameter<int>(DBConstants.CONST_PARAM_THEINSTALLATION, SqlDbType.Int, InstallationNumber),
                    AddParameter<DateTime>(DBConstants.CONST_PARAM_THEDATETIME, SqlDbType.DateTime, dtTheDate),
                    AddParameter<int>(DBConstants.CONST_PARAM_THEHOUR, SqlDbType.TinyInt, iTheHour),
                    AddParameter<bool>(DBConstants.CONST_PARAM_ISREAD, SqlDbType.Bit, isRead),
                    ReturnValue);
                if (iValue > 0)
                {
                    if (ReturnValue != null)
                    {
                        if (ReturnValue.Value != DBNull.Value || ReturnValue.Value.ToString() != string.Empty)
                        {
                            iResult = int.Parse(ReturnValue.Value.ToString());
                        }
                    }

                }

                LogManager.WriteLog("SP Excecution for HourlyVTP: " + iValue, LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                iResult = -1;
            }
            return iResult;
        }

        private int ExecuteHourlyFill(int InstallationNumber, decimal fTotalAmountOnFill, decimal fCurrentBalance)
        {
            int iValue = 0;
            try
            {
                var ReturnValue = AddReturnParameter<int>(DBConstants.CONST_PARAM_RETURNVALUE, SqlDbType.Int);
                iValue = SqlHelper.ExecuteNonQuery(ExchangeConnectionString, System.Data.CommandType.StoredProcedure, DBConstants.CONST_SP_USP_VAULT_HOURLYFILL,
                    AddParameter<int>(DBConstants.CONST_PARAM_THEINSTALLATION, SqlDbType.Int, InstallationNumber),
                    AddParameter<decimal>(DBConstants.CONST_PARAM_TOTALAMOUNTONFILL, SqlDbType.Decimal, fTotalAmountOnFill),
                    AddParameter<decimal>(DBConstants.CONST_PARAM_CURRENTBALANCE, SqlDbType.Decimal, fCurrentBalance));

                LogManager.WriteLog("SP Excecution for HourlyFill: " + iValue, LogManager.enumLogLevel.Info);

                return iValue;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return -1;
            }
        }

        private int ExecuteHourlyFunds(int InstallationNumber)
        {
            int iValue = 0;
            try
            {

                var ReturnValue = AddReturnParameter<int>(DBConstants.CONST_PARAM_RETURNVALUE, SqlDbType.Int);
                iValue = SqlHelper.ExecuteNonQuery(ExchangeConnectionString, System.Data.CommandType.StoredProcedure, DBConstants.CONST_SP_USP_HOURLY_FUNDS,
                    AddParameter<int>(DBConstants.CONST_PARAM_THEINSTALLATION, SqlDbType.Int, InstallationNumber));

                LogManager.WriteLog("SP Excecution for HourlyFunds: " + iValue, LogManager.enumLogLevel.Info);
                return iValue;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return -1;
            }
        }

        private DataTable GetVaultBalance(int iValutId)
        {
            DataSet dsVaultBalance;
            DataTable dtVaultBalance = new DataTable();

            try
            {
                dsVaultBalance = SqlHelper.ExecuteDataset(ExchangeConnectionString, CommandType.StoredProcedure, DBConstants.CONST_SP_USP_VAULT_GETBALANCE,
                                    AddParameter<int>(DBConstants.CONST_PARAM_VAULTID, SqlDbType.Int, iValutId));
                if (dsVaultBalance != null)
                    if (dsVaultBalance.Tables.Count > 0)
                        dtVaultBalance = dsVaultBalance.Tables[0];
                    else dtVaultBalance = new DataTable();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return dtVaultBalance;
        }

        private int GetVaultID()
        {
            try
            {
                var oArrayParam = new SqlParameter[1];

                var oParam = new SqlParameter
                {
                    ParameterName = DBConstants.CONST_PARAM_RETURNVALUE,
                    Value = -1,
                    Direction = ParameterDirection.Output
                };
                oArrayParam[0] = oParam;

                SqlHelper.ExecuteNonQuery(ExchangeConnectionString, System.Data.CommandType.StoredProcedure, DBConstants.CONST_SP_RSP_GETVAULTID, oArrayParam);
                LogManager.WriteLog("SP Excecution for GetVaultID: " + oArrayParam[0].Value.ToString(), LogManager.enumLogLevel.Info);
                return Convert.ToInt32(oArrayParam[0].Value);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return -1;
        }

        private int ExportVaultCurrentBalance(int iValutId, int iAlertLevel, decimal dVaultLevel, decimal dCapacity, decimal dCurrentBalance)
        {
            int iValue = 0;
            try
            {
                var ReturnValue = AddReturnParameter<int>(DBConstants.CONST_PARAM_RETURNVALUE, SqlDbType.Int);
                iValue = SqlHelper.ExecuteNonQuery(ExchangeConnectionString, System.Data.CommandType.StoredProcedure, DBConstants.CONST_SP_RSP_EXPORTVAULTCURRENTBALANCE,
                    AddParameter<int>(DBConstants.CONST_PARAM_VAULTID, SqlDbType.Int, iValutId),
                    AddParameter<int>(DBConstants.CONST_PARAM_ALERTLEVEL, SqlDbType.Int, iAlertLevel),
                    AddParameter<decimal>(DBConstants.CONST_PARAM_CURRENTVAULTLEVEL, SqlDbType.Decimal, dVaultLevel),
                    AddParameter<decimal>(DBConstants.CONST_PARAM_CAPACITY, SqlDbType.Decimal, dCapacity),
                    AddParameter<decimal>(DBConstants.CONST_PARAM_CURRENTBALANCE, SqlDbType.Decimal, dCurrentBalance));

                LogManager.WriteLog("SP Excecution for STM Export Vault CurrentBalance: " + iValue, LogManager.enumLogLevel.Info);
                return iValue;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return -1;
            }
        }


        /// <summary>
        /// Gets the connection string.
        /// </summary>
        /// <returns></returns>
        private static string _GetExchangeConnectionString()
        {
            string strConnectionString = string.Empty;
            // bool bUseHex;
            try
            {
                if (!string.IsNullOrEmpty(exchangeConnectionString))
                {
                    return exchangeConnectionString;
                }
                exchangeConnectionString = DatabaseHelper.GetExchangeConnectionString();
            }
            catch (Exception ex)
            {
                if (ex.Message == "Connectionstring Not Found.")
                    throw ex;
                else
                    ExceptionManager.Publish(ex);
            }
            return exchangeConnectionString;
        }


        #endregion  Private Functions

        #region Public Functions

        public static string ExchangeConnectionString
        {
            get
            {
                if (string.IsNullOrEmpty(exchangeConnectionString))
                    exchangeConnectionString = _GetExchangeConnectionString();

                return exchangeConnectionString;
            }
        }

        public static bool CheckSqlConnectionExists()
        {
            bool retval = false;
            try
            {
                using (SqlConnection cn = new SqlConnection(ExchangeConnectionString))
                {
                    cn.Open();
                    retval = true;
                }
            }
            catch (Exception)
            {
                LogManager.WriteLog("CheckSqlConnection --> Unable to establish connection", LogManager.enumLogLevel.Error);
            }
            return retval;
        }

        public static void InitialSettings()
        {
            int HourlyCount = 0;
            DataSet initialSettings = null;
            try
            {
                initialSettings = SqlHelper.ExecuteDataset(ExchangeConnectionString, CommandType.StoredProcedure, DBConstants.CONSTANT_RSP_GETINITIALSETTINGS);
                if (initialSettings != null && initialSettings.Tables.Count > 0)
                {
                    if (initialSettings.Tables[0].Rows.Count > 0)
                    {
                        HourlyCount = (initialSettings.Tables[0].Rows[0]["HourlyTry"].ToString() != string.Empty) ? Convert.ToInt32(initialSettings.Tables[0].Rows[0]["HourlyTry"].ToString()) : 0;
                        HourlyDailyEntity.HourlyTry = HourlyCount == 0 ? 1 : HourlyCount;
                        HourlyDailyEntity.DailyyTry = (initialSettings.Tables[0].Rows[0]["DailyTry"].ToString() != string.Empty) ? Convert.ToInt32(initialSettings.Tables[0].Rows[0]["DailyTry"].ToString()) : 1;
                        HourlyDailyEntity.DailyAutoReadTime = initialSettings.Tables[0].Rows[0]["DailyAutoReadTime"].ToString();
                        HourlyDailyEntity.HourlyReadInterval = (initialSettings.Tables[0].Rows[0]["HourlyDailyServiceInterval"].ToString() != string.Empty) ? Convert.ToInt32(initialSettings.Tables[0].Rows[0]["HourlyDailyServiceInterval"].ToString()) : 0;
                        HourlyDailyEntity.BusinessDayAdjustment = (initialSettings.Tables[0].Rows[0]["BusinessDayAdjustment"].ToString() != string.Empty) ? Convert.ToInt32(initialSettings.Tables[0].Rows[0]["BusinessDayAdjustment"].ToString()) : -1;
                    }
                }
            }
            catch (Exception ex)
            {
                initialSettings = null;
                //HourlyDailyEventLog.WriteToEventLog(HourlyDailyEntity.EventLogName, "InitialSettings: ", "Message: " + ex.Message + "Source: " + ex.Source,EventLogEntryType.Error);
                ExceptionManager.Publish(ex);
            }


        }

        public bool RunHourlyVTPService()
        {
            LogManager.WriteLog("Running Hourly VTP service...", LogManager.enumLogLevel.Info);
            int WaitTime;
            try
            {
                WaitTime = Convert.ToInt32(ConfigManager.Read("WAITONEMilliSeconds"));
            }
            catch
            {
                WaitTime = 100;
            }

            DataTable dtInstallations = new DataTable();
            DateTime dtTheDateTime;
            int iTheHour = 0;

            int iReturnValue = -1;
            bool bReturn = false;
            bool isRead = false;

            DataTable dtVaultBalance = new DataTable();
            decimal dTotalAmountOnFill = 0.0M;
            decimal dCurrentBalance = 0.0M;

            try
            {
                dtInstallations = GetInstallationDetails(0, 0, true, false);
                HourlyDailyEntity.HourlyTry = (HourlyDailyEntity.HourlyTry != 0) ? HourlyDailyEntity.HourlyTry : 3;
                if (HourlyDailyEntity.ShouldReadRunWithHourly)
                {
                    LogManager.WriteLog("Check whether Read is run with Hourly. DailyAutoReadTime " + HourlyDailyEntity.DailyAutoReadTime.Split(':')[0].ToString() + " Current Hour " + DateTime.Now.Hour.ToString(), LogManager.enumLogLevel.Info);
                    isRead = HourlyDailyEntity.DailyAutoReadTime.Split(':')[0].ToString() == DateTime.Now.Hour.ToString() ? true : false;

                    // Fix for Read run twice for a day - Begin
                    if (isRead)
                    {
                        HourlyDailyEntity.HasReadRunWithHourly = true;
                        HourlyDailyEntity.IsReadInHourly = true;
                    }

                    LogManager.WriteLog("Read run with Hourly is " + isRead.ToString(), LogManager.enumLogLevel.Info);
                    // Fix for Read run twice for a day - End
                }


                if (dtInstallations.Rows.Count > 0)
                {
                    LogManager.WriteLog("Invoke Vault Balance", LogManager.enumLogLevel.Info);
                    int ivalutId = 1;
                    ivalutId = GetVaultID();
                    if (ivalutId <= 0)
                        LogManager.WriteLog("No Vault found for the site.", LogManager.enumLogLevel.Info);

                    dtVaultBalance = GetVaultBalance(ivalutId);
                    LogManager.WriteLog("Vault Balance Rows count" + dtVaultBalance.Rows.Count.ToString(), LogManager.enumLogLevel.Info);
                    if (dtVaultBalance.Rows.Count >= 1)
                    {
                        dTotalAmountOnFill = Convert.ToDecimal(dtVaultBalance.Rows[0]["TotalAmountOnFill"]);
                        dCurrentBalance = Convert.ToDecimal(dtVaultBalance.Rows[0]["CurrentBalance"]);
                        LogManager.WriteLog("TotalAmountOnFill, CurrentBalance " + dTotalAmountOnFill.ToString() + ", " + dCurrentBalance.ToString(), LogManager.enumLogLevel.Info);
                    }

                    foreach (DataRow dr in dtInstallations.Rows)
                    {
                        if (dr["Installation_No"] != DBNull.Value)
                        {
                            for (int i = 1; i <= HourlyDailyEntity.HourlyTry; i++)
                            {


                                //string sSerial = string.Empty;
                                //if (dr["Datapak_Serial"] == DBNull.Value)
                                //{
                                //    sSerial = (dr["Machine_Manufacturers_Serial_No"] == DBNull.Value) ? string.Empty : dr["Machine_Manufacturers_Serial_No"].ToString();
                                //}
                                //else
                                //{
                                //    sSerial = dr["Datapak_Serial"].ToString();
                                //}                               
                                if (DateTime.Now.Hour == 0)
                                {

                                    dtTheDateTime = DateTime.Today.Date.AddDays(-1);
                                    dtTheDateTime.Date.ToString(format[0], DateTimeFormatInfo.InvariantInfo);
                                    iTheHour = 24;
                                    LogManager.WriteLog("Started hourly for datetime= " + dtTheDateTime.ToString() + " Started hourly for Hour= " + iTheHour.ToString(), LogManager.enumLogLevel.Info);
                                }
                                else
                                {
                                    dtTheDateTime = DateTime.Now;
                                    dtTheDateTime.ToString(format[1], DateTimeFormatInfo.InvariantInfo);
                                    iTheHour = DateTime.Now.Hour;
                                    LogManager.WriteLog("Started hourly for datetime= " + dtTheDateTime.ToString() + " Started hourly for Hour= " + iTheHour.ToString(), LogManager.enumLogLevel.Info);
                                }

                                LogManager.WriteLog("Invoke ExecuteHourlyVTP Installation No:" + dr["Installation_No"].ToString() + ", Date: " + dtTheDateTime + ", Hour: " + iTheHour + ", Isread: " + isRead, LogManager.enumLogLevel.Info);
                                iReturnValue = ExecuteHourlyVTP(int.Parse(dr["Installation_No"].ToString()), dtTheDateTime, iTheHour, isRead);
                                switch (iReturnValue)
                                {
                                    case 0:

                                        i = HourlyDailyEntity.HourlyTry;
                                        LogManager.WriteLog("Hourly for DPNo=" + dr["Installation_No"].ToString() + " -Passed!", LogManager.enumLogLevel.Info);
                                        break;
                                    case 1:
                                        LogManager.WriteLog("Hourly for  DPNo= " + dr["Installation_No"].ToString() + " -Invalid Installation!", LogManager.enumLogLevel.Info);
                                        break;
                                    case 2:
                                        LogManager.WriteLog("Hourly for DPNo= " + dr["Installation_No"].ToString() + " -Invalid Installation!", LogManager.enumLogLevel.Info);
                                        break;
                                    default:
                                        LogManager.WriteLog("Hourly for DPNo= " + dr["Installation_No"].ToString() + " -'Other' Error-", LogManager.enumLogLevel.Info);
                                        break;
                                }

                                if (i != HourlyDailyEntity.HourlyTry && mEvent.WaitOne(WaitTime))
                                {
                                    break;
                                }

                            }
                        }
                    }
  				foreach (DataRow dr in dtInstallations.Rows)
                    {
                        if (dr["Installation_No"] != DBNull.Value)
                        {
                            if (mEvent.WaitOne(WaitTime))
                            {
                                break;
                            }

                            LogManager.WriteLog("Invoke Funds Installation No:" + dr["Installation_No"].ToString(), LogManager.enumLogLevel.Info);
                            iReturnValue = ExecuteHourlyFunds(int.Parse(dr["Installation_No"].ToString()));

                            if (iReturnValue > 0)
                                LogManager.WriteLog("Funds for DPNo=" + dr["Installation_No"].ToString() + " -Passed!", LogManager.enumLogLevel.Info);
                            else
                                LogManager.WriteLog("Funds for DPNo= " + dr["Installation_No"].ToString() + " -Error-", LogManager.enumLogLevel.Info);

                            LogManager.WriteLog("Invoke Fills Installation No:" + dr["Installation_No"].ToString(), LogManager.enumLogLevel.Info);
                            iReturnValue = ExecuteHourlyFill(int.Parse(dr["Installation_No"].ToString()), dTotalAmountOnFill, dCurrentBalance);

                            if (iReturnValue > 0)
                                LogManager.WriteLog("Fills for DPNo=" + dr["Installation_No"].ToString() + " -Passed!", LogManager.enumLogLevel.Info);
                            else
                                LogManager.WriteLog("Fills for DPNo= " + dr["Installation_No"].ToString() + " -Error-", LogManager.enumLogLevel.Info);
                        }
                    }

                    if (dtVaultBalance.Rows.Count >= 1)
                    {
                        decimal dCurrentLevel = 0.0M;
                        decimal dCapacity = 0.0M;
                        int iAlertLevel = 0;
                        int iDeviceId = 0;

                        dCurrentLevel = Convert.ToDecimal(dtVaultBalance.Rows[0]["CurrentLevel"]);
                        iAlertLevel = Convert.ToInt32(dtVaultBalance.Rows[0]["Alert_Level"]);
                        iDeviceId = Convert.ToInt32(dtVaultBalance.Rows[0]["Vault_ID"]);
                        dCapacity = Convert.ToDecimal(dtVaultBalance.Rows[0]["Capacity"]);

                        LogManager.WriteLog("CurrentLevel, Alert_Level,  Vault_ID " + dCurrentLevel.ToString() + ", " + iAlertLevel.ToString() + ", " + iDeviceId.ToString(), LogManager.enumLogLevel.Info);

                        if ((dCurrentLevel <= iAlertLevel) && (GetSettingFromDB("VaultAlert", "TRUE").ToUpper() == "TRUE"))
                        {
                            LogManager.WriteLog("inside STM Export", LogManager.enumLogLevel.Info);
                            if (ExportVaultCurrentBalance(iDeviceId, iAlertLevel, dCurrentLevel, dCapacity, dCurrentBalance) > 0)
                                LogManager.WriteLog("STM Export for Vault current balnce -Passed!", LogManager.enumLogLevel.Info);
                            else
                                LogManager.WriteLog("STM Export for Vault current balnce -Failed!", LogManager.enumLogLevel.Info);
                        }
                    }
                    bReturn = true;
                }

                LogManager.WriteLog("Hourly data has been generated successfully.", LogManager.enumLogLevel.Info);
            }

            catch (Exception ex)
            {
                bReturn = false;
                //HourlyDailyEventLog.WriteToEventLog(HourlyDailyEntity.EventLogName, "RunHourlyVTPService: ", ex.Message + " " + ex.Source,EventLogEntryType.Error);
                ExceptionManager.Publish(ex);
            }
            return bReturn;
        }

        public bool UpdateHourlyStatsGamingday()
        {
            int iValue = 0;
            bool bReturn = false;
            try
            {
                /**
                 * Author: Melvi Miranda
                 * Date: 19/04/2012
                 * 
                 * Update hourly stats table by installation
                 **/
                Dictionary<int, string> dictActiveInstallations = new Dictionary<int, string>();
                #region "Get all the Active Installation numbers"
                using (SqlConnection conn = new SqlConnection(ExchangeConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("dbo.GetAllActiveInstallationDetails"))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        conn.Open();
                        cmd.Connection = conn;

                        using (SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            while (rdr.Read())
                            {
                                int installationNo = rdr.GetInt32(rdr.GetOrdinal("installation_no"));
                                string barPostion = rdr.GetString(rdr.GetOrdinal("bar_pos_name"));
                                dictActiveInstallations.Add(installationNo, barPostion);
                            }
                            rdr.Close();
                        }
                    }// end of using
                }
                #endregion

                #region "Update Hourly statistics per InstallationsToProcess(configurable)"

                string strInstallations = "";
                string strPositions = "";
                int countInstallations = 0;
                int InstallationsToProcess = 0;
                DateTime dtLastRundate = DateTime.Now;

                InstallationsToProcess = Convert.ToInt32(ConfigManager.Read("NumberOfInstallationsToProcess"));

                //Get the Last Hourly RunDate
                object result = SqlHelper.ExecuteScalar(ExchangeConnectionString, CommandType.StoredProcedure, "GetLastHourlyRunDate");
                dtLastRundate = Convert.ToDateTime(result);
                LogManager.WriteLog("LastRundate - " + Convert.ToString(dtLastRundate), LogManager.enumLogLevel.Info);

                //Loop through the active installations.
                foreach (KeyValuePair<int, string> kvpActiveInstallation in dictActiveInstallations)
                {
                    int installationNo = kvpActiveInstallation.Key;
                    countInstallations++;
                    strInstallations += installationNo.ToString() + ",";
                    strPositions += kvpActiveInstallation.Value + ",";
                    if (InstallationsToProcess > 0)
                    {
                        if ((countInstallations % InstallationsToProcess) == 0) //Do job for X number installations at a time
                        {
                            strInstallations = strInstallations.Remove(strInstallations.Length - 1); //removing the last comma ","
                            strPositions = strPositions.Remove(strPositions.Length - 1);
                            iValue = SqlHelper.ExecuteNonQuery(ExchangeConnectionString, CommandType.StoredProcedure, DBConstants.CONST_SP_ESP_COLLATEHSGAMINGDAY, new SqlParameter("@Installations", strInstallations), new SqlParameter("@MaxDate", dtLastRundate));
                            LogManager.WriteLog("Hourly Statistics updated successfully for positions " + strPositions + "!", LogManager.enumLogLevel.Info);
                            //reset the counter and installation bar position strings.
                            countInstallations = 0;
                            strInstallations = "";
                            strPositions = "";
                        }
                    }
                }//end foreach

                //check if any pending installations are to be read.
                if (strInstallations.Length > 0)
                {
                    strInstallations = strInstallations.Remove(strInstallations.Length - 1);
                    strPositions = strPositions.Remove(strPositions.Length - 1);

                    iValue = SqlHelper.ExecuteNonQueryWithCommandTimeOut(ExchangeConnectionString, CommandType.StoredProcedure, DBConstants.CONST_SP_ESP_COLLATEHSGAMINGDAY, 30, new SqlParameter("@Installations", strInstallations), new SqlParameter("@MaxDate", dtLastRundate));

                    LogManager.WriteLog("Last Hourly Statistics updated successfully for positions " + strPositions + "!", LogManager.enumLogLevel.Info);

                    countInstallations = 0;
                    strInstallations = "";
                    strPositions = "";
                }
                bReturn = true;
                #endregion
            }
            catch (Exception ex)
            {
                bReturn = false;
                ExceptionManager.Publish(ex);
            }
            return bReturn;
        }

        public bool RunDailyReadService()
        {
            int WaitTime;
            try
            {
                WaitTime = Convert.ToInt32(ConfigManager.Read("WAITONEMilliSeconds"));
            }
            catch
            {
                WaitTime = 100;
            }

            TimeSpan ts = DateTime.Now.TimeOfDay;
            double iSeconds = 0;
            DataTable dtInstallations = new DataTable();
            int iReturnValue = -1;
            bool bReturn = false;
            try
            {
                HourlyDailyEntity.LastAutoRead = (HourlyDailyEntity.LastAutoRead != null) ? HourlyDailyEntity.LastAutoRead : DateTime.Today.Date.AddDays(-1);
                if (Convert.ToDateTime(DateTime.Now.ToShortDateString()) > HourlyDailyEntity.LastAutoRead)
                {
                    //if (Microsoft.VisualBasic.DateAndTime.DateDiff(Microsoft.VisualBasic.DateInterval.Day, HourlyDailyEntity.LastAutoRead, DateTime.Now))
                    //int iDayDiff = (DateTime.Now.Day - HourlyDailyEntity.LastAutoRead.Day);               
                    //if (iDayDiff > 0)
                    //{
                    if (DBConnect.CheckSqlConnectionExists())
                    {
                        DBConnect.InitialSettings();
                        ts = DateTime.Now - DateTime.Parse(HourlyDailyEntity.DailyAutoReadTime);
                        iSeconds = ts.TotalSeconds;
                        if (iSeconds > 0)
                        {

                            LogManager.WriteLog("Now: " + DateTime.Now.ToString() + " DailyAutoReadTime: " + HourlyDailyEntity.DailyAutoReadTime.ToString() + " Time span diff. between now and DailyAutoReadTime in Seconds: " + ts.Seconds.ToString(), LogManager.enumLogLevel.Debug);
                            dtInstallations = GetInstallationDetails(0, 0, true, false);
                            if (dtInstallations.Rows.Count > 0)
                            {
                                HourlyDailyEntity.DailyyTry = (HourlyDailyEntity.DailyyTry != 0) ? HourlyDailyEntity.DailyyTry : 3;
                                foreach (DataRow dr in dtInstallations.Rows)
                                {


                                    if (dr["Installation_No"] != DBNull.Value)
                                    {
                                        for (int i = 1; i <= HourlyDailyEntity.DailyyTry; i++)
                                        {
                                            LogManager.WriteLog("Call ExecuteDailyRead for InstNo=" + int.Parse(dr["Installation_No"].ToString()) + " and read date:" + DateTime.Now.AddDays(HourlyDailyEntity.BusinessDayAdjustment).ToString(), LogManager.enumLogLevel.Info);
                                            iReturnValue = ExecuteDailyRead(int.Parse(dr["Installation_No"].ToString()),
                                                DateTime.Now.AddDays(HourlyDailyEntity.BusinessDayAdjustment));
                                            switch (iReturnValue)
                                            {
                                                case 0:

                                                    i = HourlyDailyEntity.DailyyTry;
                                                    LogManager.WriteLog("Daily Read for InstNo=" + dr["Installation_No"].ToString() + " -Passed!", LogManager.enumLogLevel.Info);
                                                    break;
                                                case 1:
                                                    break;
                                                case 2:
                                                    LogManager.WriteLog("Daily Read for InstNo== " + dr["Installation_No"].ToString() + " --READ for today all ready exists!", LogManager.enumLogLevel.Info);
                                                    break;
                                                default:
                                                    LogManager.WriteLog("Daily Read for InstNo= " + dr["Installation_No"].ToString() + " -'Other' Error-", LogManager.enumLogLevel.Info);
                                                    break;
                                            }
                                            if (i != HourlyDailyEntity.DailyyTry && mEvent.WaitOne(WaitTime))
                                            {
                                                break;
                                            }
                                        }
                                    }
                                }
                                bReturn = true;
                            }
                            LogManager.WriteLog("DailyRead data has been generated successfully.", LogManager.enumLogLevel.Info);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                bReturn = false;
                //HourlyDailyEventLog.WriteToEventLog(HourlyDailyEntity.EventLogName, "RunDailyReadService: ", "Message: " + ex.Message + "Source: " + ex.Source, EventLogEntryType.Error);
                LogManager.WriteLog("Time span difference between now and last auto read - Days: " + ts.Days.ToString() + "LastAutoRead: " + HourlyDailyEntity.LastAutoRead.ToString(), LogManager.enumLogLevel.Debug);
                ExceptionManager.Publish(ex);
            }
            return bReturn;


        }

        public bool GetSiteStatus()
        {
            LogManager.WriteLog("Inside GetSiteStatus method", LogManager.enumLogLevel.Info);

            object result = SqlHelper.ExecuteScalar(ExchangeConnectionString,
                                                          "rsp_GetSiteStatus");

            return Convert.ToBoolean(result);
        }

        public string GetSettingFromDB(string strSetting, string strDefault)
        {
            string strReturnValue = string.Empty;
            try
            {
                LogManager.WriteLog("Inside GetSettingFromDB method", LogManager.enumLogLevel.Info);

                SqlParameter[] sqlparams = new SqlParameter[5];

                sqlparams[0] = new SqlParameter("@Setting_ID", 0);
                sqlparams[1] = new SqlParameter("@Setting_Name", strSetting.Trim());
                sqlparams[2] = new SqlParameter("@Setting_Default", strDefault);

                sqlparams[3] = new SqlParameter();
                sqlparams[3].ParameterName = "Setting_Value";
                sqlparams[3].Direction = ParameterDirection.Output;
                sqlparams[3].Value = string.Empty;
                sqlparams[3].SqlDbType = SqlDbType.VarChar;
                sqlparams[3].Size = 100;

                SqlParameter ReturnValue = new SqlParameter();
                ReturnValue.ParameterName = "RETURN_VALUE";
                ReturnValue.Direction = ParameterDirection.ReturnValue;
                sqlparams[4] = ReturnValue;

                SqlHelper.ExecuteNonQuery(ExchangeConnectionString, System.Data.CommandType.StoredProcedure, "rsp_getSetting", sqlparams);
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

        public static bool CalculateMGMDDelta()
        {
            try
            {
                SqlHelper.ExecuteNonQuery(exchangeConnectionString, System.Data.CommandType.StoredProcedure, DBConstants.CONST_SP_USP_MGMDUPDATE_JOB);
                return true;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("DBConnect.CalculateMGMDDelta()->Exception:" + ex.Message, LogManager.enumLogLevel.Error);
                return false;
            }
        }

        //Stacker Level
        public DataTable CheckStackerLevelStatus()
        {
            LogManager.WriteLog("Inside CheckStackerLevelStatus method", LogManager.enumLogLevel.Info);

            DataSet result = SqlHelper.ExecuteDataset(ExchangeConnectionString,
                                                          "rsp_GetStackerDetails");

            return result.Tables.Count > 0 ? result.Tables[0] : null;
        }

        public static DataTable GetVersion()
        {
            LogManager.WriteLog("Inside GetVersion method", LogManager.enumLogLevel.Info);

            DataSet result = SqlHelper.ExecuteDataset(ExchangeConnectionString,
                                                          "rsp_getVersion_SiteName");

            return result.Tables.Count > 0 ? result.Tables[0] : null;
        }

        public bool ExportToSTM(string Type, string Site_Code, string XmlMsg)
        {
            LogManager.WriteLog("ExportToSTM Xml-->" + XmlMsg, LogManager.enumLogLevel.Info);
            try
            {

                SqlParameter[] sqlparams = new SqlParameter[4];
                sqlparams[0] = new SqlParameter("Type", Type);
                sqlparams[1] = new SqlParameter("ClientID", 1);
                sqlparams[2] = new SqlParameter("Site_Code", Site_Code);
                sqlparams[3] = new SqlParameter("XmlMessage", XmlMsg);
                int result = SqlHelper.ExecuteNonQuery(ExchangeConnectionString, "usp_STM_Export_History", sqlparams);

                return result > 0;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("ExportToSTM Failed Type:" + Type, LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
                return false;
            }
        }

        public bool UpdateStackerAlertStatus(int Installation_No, bool STMAlertStatus)
        {
            LogManager.WriteLog("Inside UpdateStackerAlertStatus", LogManager.enumLogLevel.Debug);
            try
            {
                SqlParameter[] sqlparams = new SqlParameter[2];
                sqlparams[0] = new SqlParameter("Installation_No", Installation_No);
                sqlparams[1] = new SqlParameter("STMAlertProcessed", STMAlertStatus);
                int result = SqlHelper.ExecuteNonQuery(ExchangeConnectionString, "usp_UpdateStackerAlertStatus", sqlparams);

                return result > 0;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("UpdateStackerAlertStatus->Exception:" + ex.Message, LogManager.enumLogLevel.Error);
                return false;
            }
        }

        #endregion Public Functions

    }
}
