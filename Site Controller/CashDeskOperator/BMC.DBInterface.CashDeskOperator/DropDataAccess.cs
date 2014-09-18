using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using BMC.DataAccess;
using BMC.Common;
using BMC.Common.ExceptionManagement;
using System.Data.Linq.Mapping;
using System.Data.Linq;
using System.Reflection;
using Microsoft.Win32;
using BMC.Common.Utilities;
using BMC.Common.ConfigurationManagement;


namespace BMC.DBInterface.CashDeskOperator
{
    public class DropDataAccess
    {
        public DataTable GetCurrentMachines()
        {
            try
            {
                return (SqlHelper.ExecuteDataset(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, Constants.CONSTANT_USP_GETINSTALLATIONS)).Tables[0];
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                return null;
            }
        }
      
        public DataTable GetMeterList(string display, int record_No, int hour_No)
        {
            DataSet dtMeters = null;

            try
            {
                //DataBaseServiceHandler.ConnectionString = CommonDataAccess.ExchangeConnectionString;
                dtMeters = DataBaseServiceHandler.Fill(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, "RecordType", dtMeters, DataBaseServiceHandler.AddParameter<string>("", DbType.String, display),
                            DataBaseServiceHandler.AddParameter<int>("id", DbType.Int32, record_No),
                            DataBaseServiceHandler.AddParameter<int>("HourNo", DbType.Int32, hour_No));

                return dtMeters.Tables[0];
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                return null;
            }
        }

        public bool GetSGVISetting()
        {
            bool bSGVISetting = false;

            try
            {
                bSGVISetting = Convert.ToBoolean(GetSettingFromDB("SGVI_Enabled"));

                return bSGVISetting;
            }
            catch (Exception ex)
            {
                return bSGVISetting;
            }
        }

        /// <summary>
        /// Get the settings for CMP Kiosk
        /// </summary>
        /// <param name="sqlparams"></param>
        /// <param name="strConnect"></param>
        /// <returns >string</returns>
        private string GetSettingFromDB(string strSetting)
        {
            string strReturnValue = string.Empty;
            try
            {
                SqlParameter[] sqlparams = GetSettingParameterDB(strSetting);
                SqlHelper.ExecuteNonQuery(GetExchangeConnectionString(), System.Data.CommandType.StoredProcedure, DBConstants.CONST_SP_GETSETTING, sqlparams);
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

        /// <summary>
        /// To set parameters for Get Setting SP
        /// </summary>
        /// <param name="strSettingName">string</param>
        /// <returns type=SqlParameter[] >sp_parames</returns>
        private SqlParameter[] GetSettingParameterDB(string SettingName)
        {
            SqlParameter[] sp_parames = null;
            try
            {

                if (SettingName != null)
                {
                    sp_parames = new SqlParameter[4];

                    sp_parames[0] = new SqlParameter(DBConstants.CONST_SP_PARAM_SETTINGID, 0);
                    sp_parames[1] = new SqlParameter(DBConstants.CONST_SP_PARAM_SETTINGNAME, SettingName.Trim());
                    sp_parames[2] = new SqlParameter(DBConstants.CONST_SP_PARAM_SETTINGDEFAULT, string.Empty);

                    sp_parames[3] = new SqlParameter();
                    sp_parames[3].ParameterName = DBConstants.CONST_SP_PARAM_SETTINGVALUE;
                    sp_parames[3].Direction = ParameterDirection.Output;
                    sp_parames[3].Value = string.Empty;
                    sp_parames[3].SqlDbType = SqlDbType.VarChar;
                    sp_parames[3].Size = 8000;

                    //SqlParameter ReturnValue = new SqlParameter();
                    //ReturnValue.ParameterName = DBConstants.CONST_SP_PARAM_RETURNVALUE;
                    //ReturnValue.Direction = ParameterDirection.ReturnValue;
                    //sp_parames[4] = ReturnValue;

                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            return sp_parames;
        }

        public string GetExchangeConnectionString()
        {
            string strConnectionString = "";

            try
            {
               
                //RegistryKey regKeyConnectionString = Registry.LocalMachine.OpenSubKey("Software\\Honeyframe\\Cashmaster");
                //strConnectionString = regKeyConnectionString.GetValue("SQLConnect").ToString();
                //regKeyConnectionString.Close();

                strConnectionString = DatabaseHelper.GetExchangeConnectionString();

                //if (!strConnectionString.ToUpper().Contains("SERVER"))
                //{
                //    strConnectionString = BMC.Common.Security.CryptEncode.Decrypt(strConnectionString);
                //}

                return strConnectionString;
            }
            catch 
            {
                strConnectionString = "";
                return strConnectionString;
            }
        }

    }
}
