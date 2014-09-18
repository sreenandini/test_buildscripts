using System;
using System.Collections.Generic;
using System.Text;
using BMC.DataAccess;
using System.Data.SqlClient;
using Microsoft.Win32;
using System.Data;
using BMC.Common.ExceptionManagement;
using BMC.Common.LogManagement;
using BMC.Common.ConfigurationManagement;
using BMC.Common.Utilities;

namespace BMC.ExchangeTicketExportService
{
    public class DataAccess
    {
        #region Private Variables

        private static string strExchnageConnectionString = string.Empty;

        #endregion Private Variables

        #region Properties

        public static string ExchangeConnectionString
        {
            get
            {
                return strExchnageConnectionString;
            }
            set
            {
                strExchnageConnectionString = value;
            }
        }

        #endregion Properties

        #region Constructor
        /// <summary>
        /// Constructor.
        /// </summary>
        public DataAccess()
        {
            ExchangeConnectionString = GetExchangeConnectionString();
        }

        #endregion Constructor

        #region Public Methods

        public DataTable GetDataToExport()
        {
            string strStoredProcedureName = Constants.CONSTANT_RSP_GETTICKETEXPORTRECORDS;
            DataTable dtRecords = new DataTable();

            try
            {
                dtRecords = SqlHelper.ExecuteDataset(ExchangeConnectionString, strStoredProcedureName, null).Tables[0];
            }
            catch (Exception exGetDataToExport)
            {
                dtRecords = new DataTable();
                ExceptionManager.Publish(exGetDataToExport);
            }

            return dtRecords;
        }

        public bool CheckDataToExport()
        {
            string strStoredProcedureName = Constants.CONSTANT_RSP_CHECKTICKETEXPORTRECORDS;
            bool bStatus = false;

            try
            {
                int iRecordCount = Convert.ToInt32(SqlHelper.ExecuteScalar(ExchangeConnectionString, strStoredProcedureName, null));

                if (iRecordCount > 0)
                {
                    bStatus = true;
                }
                else
                {
                    bStatus = false;
                }
            }
            catch (Exception exCheckDataToExport)
            {
                bStatus = false;
                ExceptionManager.Publish(exCheckDataToExport);
            }

            return bStatus;
        }

        public bool CheckCVDataToExport()
        {
            string strStoredProcedureName = Constants.CONSTANT_RSP_CHECKCVEXPORTRECORDS;
            bool bStatus = false;

            try
            {
                int iRecordCount = Convert.ToInt32(SqlHelper.ExecuteScalar(ExchangeConnectionString, strStoredProcedureName, null));

                if (iRecordCount > 0)
                {
                    bStatus = true;
                }
                else
                {
                    bStatus = false;
                }
            }
            catch (Exception exCheckDataToExport)
            {
                bStatus = false;
                ExceptionManager.Publish(exCheckDataToExport);
            }

            return bStatus;
        }

        public DataTable GetCVDataToExport()
        {
            string strStoredProcedureName = Constants.CONSTANT_RSP_GETCVEXPORTRECORDS;
            DataTable dtRecords = new DataTable();

            try
            {
                dtRecords = SqlHelper.ExecuteDataset(ExchangeConnectionString, strStoredProcedureName, null).Tables[0];
            }
            catch (Exception exGetDataToExport)
            {
                dtRecords = new DataTable();
                ExceptionManager.Publish(exGetDataToExport);
            }

            return dtRecords;
        }

        #endregion Public Methods

        #region Static Methods

        public static string GetExchangeConnectionString()
        {
            try
            {
                if (!string.IsNullOrEmpty(ExchangeConnectionString))
                {
                    return ExchangeConnectionString;
                }

               ExchangeConnectionString = BMC.Common.Utilities.DatabaseHelper.GetExchangeConnectionString();

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            return ExchangeConnectionString;
        }

        public static string GetVoucherDetailsToExport(int iVoucherID)
        {
            string strStoredProcedureName = Constants.CONSTANT_RSP_GETVOUCHERDETAILSTOEXPORT;
            string strXMLData = string.Empty;

            try
            {
                SqlParameter[] oSQLParam = new SqlParameter[1];
                SqlParameter oParam = new SqlParameter();
                oParam.ParameterName = "VoucherID";
                oParam.Value = iVoucherID;
                oParam.Direction = ParameterDirection.Input;
                oSQLParam[0] = oParam;

                strXMLData = SqlHelper.ExecuteScalar(ExchangeConnectionString, strStoredProcedureName, oSQLParam).ToString();
            }
            catch (Exception exCheckDataToExport)
            {
                strXMLData = string.Empty;
                ExceptionManager.Publish(exCheckDataToExport);
            }

            return strXMLData;
        }

        public static string GetTicketExceptionDetailsToExport(int iTEID)
        {
            string strStoredProcedureName = Constants.CONSTANT_RSP_GETTICKETEXCEPTIONDETAILSTOEXPORT;
            string strXMLData = string.Empty;

            try
            {
                SqlParameter[] oSQLParam = new SqlParameter[1];
                SqlParameter oParam = new SqlParameter();
                oParam.ParameterName = "TEID";
                oParam.Value = iTEID;
                oParam.Direction = ParameterDirection.Input;
                oSQLParam[0] = oParam;

                strXMLData = SqlHelper.ExecuteScalar(ExchangeConnectionString, strStoredProcedureName, oSQLParam).ToString();
            }
            catch (Exception exCheckDataToExport)
            {
                strXMLData = string.Empty;
                ExceptionManager.Publish(exCheckDataToExport);
            }

            return strXMLData;
        }

        public static string GetDeviceDetailsToExport(int iDeviceID)
        {
            string strStoredProcedureName = Constants.CONSTANT_RSP_GETDEVICEDETAILSTOEXPORT;
            string strXMLData = string.Empty;

            try
            {
                SqlParameter[] oSQLParam = new SqlParameter[1];
                SqlParameter oParam = new SqlParameter();
                oParam.ParameterName = "DeviceID";
                oParam.Value = iDeviceID;
                oParam.Direction = ParameterDirection.Input;
                oSQLParam[0] = oParam;

                strXMLData = SqlHelper.ExecuteScalar(ExchangeConnectionString, strStoredProcedureName, oSQLParam).ToString();
            }
            catch (Exception exCheckDataToExport)
            {
                strXMLData = string.Empty;
                ExceptionManager.Publish(exCheckDataToExport);
            }

            return strXMLData;
        }


        public static string GetPromotionsDetailsToExport(int iPrtomotionsID)
        {
            string strStoredProcedureName = Constants.CONSTANT_RSP_GETPROMOTIONSDETAILSTOEXPORT;
            string strXMLData = string.Empty;

            try
            {
                SqlParameter[] oSQLParam = new SqlParameter[1];
                SqlParameter oParam = new SqlParameter();
                oParam.ParameterName = "PromotionalID";
                oParam.Value = iPrtomotionsID;
                oParam.Direction = ParameterDirection.Input;
                oSQLParam[0] = oParam;

                strXMLData = SqlHelper.ExecuteScalar(ExchangeConnectionString, strStoredProcedureName, oSQLParam).ToString();
            }
            catch (Exception exCheckDataToExport)
            {
                strXMLData = string.Empty;
                ExceptionManager.Publish(exCheckDataToExport);
            }

            return strXMLData;
        }

        public static string GetPromotionalTicketsDetailsToExport(int iPrtomotionalTicketID)
        {
            string strStoredProcedureName = Constants.CONSTANT_RSP_GETPROMOTIONALTICKETSDETAILSTOEXPORT;
            string strXMLData = string.Empty;

            try
            {
                SqlParameter[] oSQLParam = new SqlParameter[1];
                SqlParameter oParam = new SqlParameter();
                oParam.ParameterName = "PromotionalTicketID";
                oParam.Value = iPrtomotionalTicketID;
                oParam.Direction = ParameterDirection.Input;
                oSQLParam[0] = oParam;

                strXMLData = SqlHelper.ExecuteScalar(ExchangeConnectionString, strStoredProcedureName, oSQLParam).ToString();
            }
            catch (Exception exCheckDataToExport)
            {
                strXMLData = string.Empty;
                ExceptionManager.Publish(exCheckDataToExport);
            }

            return strXMLData;
        }


        public static string GetTISPromotionalTicketsDetailsToExport(int iVoucherID)
        {
            string strStoredProcedureName = Constants.CONSTANT_RSP_GETTISPROMOTIONALTICKETSDETAILSTOEXPORT;
            string strXMLData = string.Empty;

            try
            {
                SqlParameter[] oSQLParam = new SqlParameter[1];
                SqlParameter oParam = new SqlParameter();
                oParam.ParameterName = "VoucherID";
                oParam.Value = iVoucherID;
                oParam.Direction = ParameterDirection.Input;
                oSQLParam[0] = oParam;

                strXMLData = SqlHelper.ExecuteScalar(ExchangeConnectionString, strStoredProcedureName, oSQLParam).ToString();
            }
            catch (Exception exCheckDataToExport)
            {
                strXMLData = string.Empty;
                ExceptionManager.Publish(exCheckDataToExport);
            }

            return strXMLData;
        }

        public static string GetExternalVoucherDetailsToExport(int iVoucherID)
        {
            string strStoredProcedureName = Constants.CONSTANT_RSP_GETEXTERNALVOUCHERDETAILSTOEXPORT;
            string strXMLData = string.Empty;

            try
            {
                SqlParameter[] oSQLParam = new SqlParameter[1];
                SqlParameter oParam = new SqlParameter();
                oParam.ParameterName = "VoucherID";
                oParam.Value = iVoucherID;
                oParam.Direction = ParameterDirection.Input;
                oSQLParam[0] = oParam;

                strXMLData = SqlHelper.ExecuteScalar(ExchangeConnectionString, strStoredProcedureName, oSQLParam).ToString();
            }
            catch (Exception exCheckDataToExport)
            {
                strXMLData = string.Empty;
                ExceptionManager.Publish(exCheckDataToExport);
            }

            return strXMLData;
        }

        
        

        public static bool UpdateExportStatus(int iTEHID, int iStatus)
        {
            string strStoredProcedureName = Constants.CONSTANT_USP_UPDATETICKETEXPORTSTATUS;
            bool bStatus = false;
            int iQryResult = 0;

            try
            {
                SqlParameter[] oSQLParam = new SqlParameter[2];
                SqlParameter oParam = new SqlParameter();
                oParam.ParameterName = "TEH_ID";
                oParam.Value = iTEHID;
                oParam.Direction = ParameterDirection.Input;
                oSQLParam[0] = oParam;

                oParam = new SqlParameter();
                oParam.ParameterName = "Status";
                oParam.Value = iStatus;
                oParam.Direction = ParameterDirection.Input;
                oSQLParam[1] = oParam;

                iQryResult = SqlHelper.ExecuteNonQuery(ExchangeConnectionString, strStoredProcedureName, oSQLParam);

                if (iQryResult > 0)
                {
                    bStatus = true;
                }
            }
            catch (Exception exCheckDataToExport)
            {
                bStatus = false;
                ExceptionManager.Publish(exCheckDataToExport);
            }

            return bStatus;
        }

        public static string GetSiteCode()
        {
            string strSiteCode = string.Empty;

            try
            {
                strSiteCode = SqlHelper.ExecuteScalar(ExchangeConnectionString, CommandType.Text, "SELECT TOP 1 Code FROM SITE").ToString();
            }
            catch (Exception exGetSiteCode)
            {
                strSiteCode = string.Empty;
                ExceptionManager.Publish(exGetSiteCode);
            }

            return strSiteCode;
        }

        public static bool UpdateCVExportStatus(int iCVEHID, int iStatus)
        {
            string strStoredProcedureName = Constants.CONSTANT_USP_UPDATECVEXPORTSTATUS;
            bool bStatus = false;
            int iQryResult = 0;

            try
            {
                SqlParameter[] oSQLParam = new SqlParameter[2];
                SqlParameter oParam = new SqlParameter();
                oParam.ParameterName = "CVEH_ID";
                oParam.Value = iCVEHID;
                oParam.Direction = ParameterDirection.Input;
                oSQLParam[0] = oParam;

                oParam = new SqlParameter();
                oParam.ParameterName = "Status";
                oParam.Value = iStatus;
                oParam.Direction = ParameterDirection.Input;
                oSQLParam[1] = oParam;

                iQryResult = SqlHelper.ExecuteNonQuery(ExchangeConnectionString, strStoredProcedureName, oSQLParam);

                if (iQryResult > 0)
                {
                    bStatus = true;
                }
            }
            catch (Exception exCheckDataToExport)
            {
                bStatus = false;
                ExceptionManager.Publish(exCheckDataToExport);
            }

            return bStatus;
        }

        /// <summary>
        /// Update Component Details.
        /// </summary>
        /// <param name="CompID"></param>
        /// <returns></returns>
        public static string ExportComponentDetails(int CompID)
        {
            string strStoredProcedureName = "rsp_GetCompDetailsForExport";
            string strXMLData = string.Empty;

            try
            {
                SqlParameter[] oSQLParam = new SqlParameter[1];
                SqlParameter oParam = new SqlParameter();
                oParam.ParameterName = "CCD_ID";
                oParam.Value = CompID;
                oParam.Direction = ParameterDirection.Input;
                oSQLParam[0] = oParam;

                strXMLData = SqlHelper.ExecuteScalar(ExchangeConnectionString, strStoredProcedureName, oSQLParam).ToString();
            }
            catch (Exception exCheckDataToExport)
            {
                strXMLData = string.Empty;
                ExceptionManager.Publish(exCheckDataToExport);
            }

            return strXMLData;
        }

        /// <summary>
        /// Update Machine Component details.
        /// </summary>
        /// <param name="MachineID"></param>
        /// <returns></returns>
        public static string ExportMachineComponentDetails(int MachineID)
        {
            string strStoredProcedureName = "rsp_GetMachineCompDetailsForExport";
            string strXMLData = string.Empty;

            try
            {
                SqlParameter[] oSQLParam = new SqlParameter[1];
                SqlParameter oParam = new SqlParameter();
                oParam.ParameterName = "Machine_ID";
                oParam.Value = MachineID;
                oParam.Direction = ParameterDirection.Input;
                oSQLParam[0] = oParam;

                strXMLData = SqlHelper.ExecuteScalar(ExchangeConnectionString, strStoredProcedureName, oSQLParam).ToString();
            }
            catch (Exception exCheckDataToExport)
            {
                strXMLData = string.Empty;
                ExceptionManager.Publish(exCheckDataToExport);
            }

            return strXMLData;
        }

        /// <summary>
        /// Update Machine Component details.
        /// </summary>
        /// <param name="MachineID"></param>
        /// <returns></returns>
        public static string ExportComponentVerificationDetails(int VerID)
        {
            string strStoredProcedureName = "rsp_GetCompVerificationRecordForExport";
            string strXMLData = string.Empty;

            try
            {
                SqlParameter[] oSQLParam = new SqlParameter[1];
                SqlParameter oParam = new SqlParameter();
                oParam.ParameterName = "Verification_ID";
                oParam.Value = VerID;
                oParam.Direction = ParameterDirection.Input;
                oSQLParam[0] = oParam;

                strXMLData = SqlHelper.ExecuteScalar(ExchangeConnectionString, strStoredProcedureName, oSQLParam).ToString();
            }
            catch (Exception exCheckDataToExport)
            {
                strXMLData = string.Empty;
                ExceptionManager.Publish(exCheckDataToExport);
            }

            return strXMLData;
        }

        public static string GetSettingFromDB(string SettingName, string SettingDefault)
        {
            string strReturnSetting = string.Empty;
            try
            {
                SqlParameter[] sqlparams = GetSettingParameters(SettingName, SettingDefault);
                SqlHelper.ExecuteNonQuery(ExchangeConnectionString, CommandType.StoredProcedure, Constants.RSP_GETSETTING, sqlparams);

                if (sqlparams[3].Value != null || sqlparams[3].Value.ToString() != string.Empty)
                {
                    strReturnSetting = Convert.ToString(sqlparams[3].Value);
                }
                else
                {
                    strReturnSetting = string.Empty;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            return strReturnSetting;
        }

        private static SqlParameter[] GetSettingParameters(string SettingName, string SettingDefault)
        {
            SqlParameter[] sqlparams = null;
            try
            {

                if (SettingName != null)
                {
                    sqlparams = new SqlParameter[5];

                    sqlparams[0] = new SqlParameter(Constants.CONST_SP_PARAM_SETTINGID, string.Empty);
                    sqlparams[1] = new SqlParameter(Constants.CONST_SP_PARAM_SETTINGNAME, SettingName.Trim());
                    sqlparams[2] = new SqlParameter(Constants.CONST_SP_PARAM_SETTINGDEFAULT, SettingDefault);

                    sqlparams[3] = new SqlParameter();
                    sqlparams[3].ParameterName = Constants.CONST_SP_PARAM_SETTINGVALUE;
                    sqlparams[3].Direction = ParameterDirection.Output;
                    sqlparams[3].Value = string.Empty;
                    sqlparams[3].SqlDbType = SqlDbType.VarChar;
                    sqlparams[3].Size = 100;

                    SqlParameter ReturnValue = new SqlParameter();
                    ReturnValue.ParameterName = Constants.CONST_SP_PARAM_RETURNVALUE;
                    ReturnValue.Direction = ParameterDirection.ReturnValue;
                    sqlparams[4] = ReturnValue;

                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return sqlparams;
        }

        /// <summary>
        /// Export Authenticate Component details.
        /// </summary>
        /// <param name="MachineID"></param>
        /// <returns></returns>
        public static string ExportAuthenticateComponent(int iMachineID)
        {
            string strStoredProcedureName = "rsp_GetAuthenticateComponentDetailsForExport";
            string strXMLData = string.Empty;

            try
            {
                SqlParameter[] oSQLParam = new SqlParameter[1];
                SqlParameter oParam = new SqlParameter();
                oParam.ParameterName = "Machine_ID";
                oParam.Value = iMachineID;
                oParam.Direction = ParameterDirection.Input;
                oSQLParam[0] = oParam;

                strXMLData = SqlHelper.ExecuteScalar(ExchangeConnectionString, strStoredProcedureName, oSQLParam).ToString();
            }
            catch (Exception exCheckDataToExport)
            {
                strXMLData = string.Empty;
                ExceptionManager.Publish(exCheckDataToExport);
            }

            return strXMLData;
        }

        /// <summary>
        /// Export Component Count details.
        /// </summary>
        /// <param name="MachineID"></param>
        /// <returns></returns>
        public static string ExportComponentCount(int iMachineID)
        {
            string strStoredProcedureName = "rsp_GetComponentCountDetailsForExport";
            string strXMLData = string.Empty;

            try
            {
                SqlParameter[] oSQLParam = new SqlParameter[1];
                SqlParameter oParam = new SqlParameter();
                oParam.ParameterName = "Machine_ID";
                oParam.Value = iMachineID;
                oParam.Direction = ParameterDirection.Input;
                oSQLParam[0] = oParam;

                strXMLData = SqlHelper.ExecuteScalar(ExchangeConnectionString, strStoredProcedureName, oSQLParam).ToString();
            }
            catch (Exception exCheckDataToExport)
            {
                strXMLData = string.Empty;
                ExceptionManager.Publish(exCheckDataToExport);
            }

            return strXMLData;
        }

        public static bool GetSiteStatus()
        {
            LogManager.WriteLog("Inside GetSiteStatus method", LogManager.enumLogLevel.Info);

            object result = SqlHelper.ExecuteScalar(ExchangeConnectionString,
                                                          "rsp_GetSiteStatus");

            return Convert.ToBoolean(result);
        }

        #endregion Static Methods
    }
}
