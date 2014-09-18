using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using System.Data.SqlClient;
using BMC.DataAccess;
using BMC.Common.ExceptionManagement;
using BMC.Common.Utilities;

namespace DataXChangeEndPointService.Data
{
    public class ResponseDataAccess
    {
        #region Variables
        
        private static string _ConnectionString;
        private static bool? _IsPCEnabled;
        private static bool? _IsExtendedPlayer;
        private static int? _BreakPeriodInterval;
        private static string _BreakPeriodMessage;
        private static int? _BreakPeriodDisplayTime;
        private static string _RatingBasis;

        #endregion //Variables

        #region Properties

        public static string ConnectionString
        {
            get
            {
                if (string.IsNullOrEmpty(_ConnectionString))
                    _ConnectionString = GetExchangeConnectionString();

                return _ConnectionString;
            }
        }

        public static bool IsPCEnabled
        {
            get
            {
                try
                {
                    if (_IsPCEnabled == null)
                    {
                        _IsPCEnabled = GetSettingDB("PreCommitmentEnabled").ToUpper() == "TRUE" ? true : false;
                    }

                }
                catch { _IsPCEnabled = null; }
                return _IsPCEnabled.GetValueOrDefault();
            }
        }

        public static bool IsExtendedPlayer
        {
            get
            {
                try
                {
                    if (_IsExtendedPlayer == null)
                    {
                        _IsExtendedPlayer = GetSettingDB("IsExtendedPlayer").ToUpper() == "TRUE" ? true : false;
                    }

                }
                catch { _IsExtendedPlayer = null; }
                return _IsExtendedPlayer.GetValueOrDefault();
            }
        }

        public static string BreakPeriodMessage
        {
            get
            {
                try
                {
                    if (string.IsNullOrEmpty(_BreakPeriodMessage))
                    {
                        _BreakPeriodMessage = GetSettingDB("BreakPeriodMessage").Substring(0, 100);
                    }

                }
                catch { _BreakPeriodMessage = "Have a break!!!"; }
                return _BreakPeriodMessage;
            }
        }

        public static int BreakPeriodInterval
        {
            get
            {
                try
                {
                    if (_BreakPeriodInterval == null)
                    {
                        _BreakPeriodInterval = Convert.ToInt32(GetSettingDB("BreakPeriodInterval"));
                    }

                }
                catch { _BreakPeriodInterval = 900; }
                return _BreakPeriodInterval.GetValueOrDefault();
            }
        }

        public static int BreakPeriodDisplayTime
        {
            get
            {
                try
                {
                    if (_BreakPeriodDisplayTime == null)
                    {
                        _BreakPeriodDisplayTime = Convert.ToInt32(GetSettingDB("BreakPeriodFadeOutTime"));
                    }

                }
                catch { _BreakPeriodDisplayTime = 15; }
                return _BreakPeriodDisplayTime.GetValueOrDefault();
            }
        }

        public static string RatingBasis
        {
            get
            {
                try
                {
                    if (string.IsNullOrEmpty(_RatingBasis))
                    {
                        _RatingBasis = GetSettingDB("PreCommitmentRatingBasis");
                    }

                }
                catch { _RatingBasis = "Time"; }
                return _RatingBasis;
            }
        }

        #endregion //Properties

        #region Public Methods

        public bool InsertPCMessages(string strCarNo, string strSlotNo, string strStand, int? iHandle_Pulls, string strRatingInterval, string strMessage, char? cLockType, string strAckType, string strBreakPeriod, bool bPCEnrolled)
        {
            try
            {
                SqlParameter[] ObjParams = new SqlParameter[10];
                ObjParams[0] = new SqlParameter(DBConstants.CONST_PARAM_PC_CARD_NO, strCarNo);
                ObjParams[1] = new SqlParameter(DBConstants.CONST_PARAM_PC_SLOTNO, strSlotNo);
                ObjParams[2] = new SqlParameter(DBConstants.CONST_PARAM_PC_STAND, strStand);
                ObjParams[3] = new SqlParameter(DBConstants.CONST_PARAM_PC_HANDLEPULLS, iHandle_Pulls.HasValue ? iHandle_Pulls : null);
                ObjParams[4] = new SqlParameter(DBConstants.CONST_PARAM_PC_RATINGINTERVAL, strRatingInterval);
                ObjParams[5] = new SqlParameter(DBConstants.CONST_PARAM_PC_DISPLAYMESSAGE, strMessage);
                ObjParams[6] = new SqlParameter(DBConstants.CONST_PARAM_PC_LOCKTYPE, cLockType.GetValueOrDefault());
                ObjParams[7] = new SqlParameter(DBConstants.CONST_PARAM_PC_ACKTYPE, strAckType);
                ObjParams[8] = new SqlParameter(DBConstants.CONST_PARAM_PC_BREAKPERIOD, strBreakPeriod);
                ObjParams[9] = new SqlParameter(DBConstants.CONST_PARAM_PC_ENROLLED, bPCEnrolled);

                if (SqlHelper.ExecuteNonQuery(ConnectionString, DBConstants.SP_USP_INSERTPCNOTIFICATIONRESPONSE, ObjParams) > -1)
                {
                    return true;
                }
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            return false;
        }

        public bool InsertCardInResponseFromPC(string strCarNo, string strMsgType, string strMsgCode, string strSlotNo, string strStand, string strMsg, DateTime dtReceived, int iHandle_Pulls, string strRatingInterval, string strBreakPeriod, bool bPCEnrolled)
        {
            try
            {
                SqlParameter[] ObjParams = new SqlParameter[11];
                ObjParams[0] = new SqlParameter(DBConstants.CONST_PARAM_PC_CARD_NO, strCarNo);
                ObjParams[1] = new SqlParameter(DBConstants.CONST_PARAM_PC_MESSAGE_TYPE, strMsgType);
                ObjParams[2] = new SqlParameter(DBConstants.CONST_PARAM_PC_MESSAGE_CODE, strMsgCode);
                ObjParams[3] = new SqlParameter(DBConstants.CONST_PARAM_PC_SLOTNO, strSlotNo);
                ObjParams[4] = new SqlParameter(DBConstants.CONST_PARAM_PC_STAND, strStand);
                ObjParams[5] = new SqlParameter(DBConstants.CONST_PARAM_PC_MESSAGE, strMsg);
                ObjParams[6] = new SqlParameter(DBConstants.CONST_PARAM_PC_RECEIVED_DATE, dtReceived);
                ObjParams[7] = new SqlParameter(DBConstants.CONST_PARAM_PC_HANDLEPULLS, iHandle_Pulls);
                ObjParams[8] = new SqlParameter(DBConstants.CONST_PARAM_PC_RATINGINTERVAL, strRatingInterval);
                ObjParams[9] = new SqlParameter(DBConstants.CONST_PARAM_PC_BREAKPERIOD, strBreakPeriod);
                ObjParams[10] = new SqlParameter(DBConstants.CONST_PARAM_PC_ENROLLED, bPCEnrolled);

                if (SqlHelper.ExecuteNonQuery(ConnectionString, DBConstants.SP_ESP_INSERTPCCARDRESPONSE, ObjParams) > -1)
                {
                    return true;
                }
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            return false;
        }

        public DataSet GetDataForSendToComms(int iMaxRows, ref int iRecordsToProcess)
        {
            try
            {
                SqlParameter[] ObjParams = new SqlParameter[2];
                ObjParams[0] = new SqlParameter(DBConstants.CONST_PARAM_MAX_ROWS, iMaxRows);
                ObjParams[1] = new SqlParameter();
                ObjParams[1].ParameterName = DBConstants.CONST_PARAM_NOOFROWSTOPROCESS;
                ObjParams[1].Direction = ParameterDirection.Output;
                ObjParams[1].Value = 0;
                ObjParams[1].SqlDbType = SqlDbType.Int;

                DataSet ds = SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, DBConstants.RSP_GET_PC_SERVERTRACKING, ObjParams);
                if (ObjParams[1].Value != null || ObjParams[1].Value.ToString() != string.Empty)
                {
                    iRecordsToProcess = Convert.ToInt32(ObjParams[1].Value);
                }
                else
                {
                    iRecordsToProcess = 0;
                }
                return ds;
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            return new DataSet();
        }

        public bool UpdateSentFreeFormMsgToCommsStatus(int iPC_ST_ID, bool bStatus)
        {
            try
            {
                SqlParameter[] ObjParams = new SqlParameter[2];
                ObjParams[0] = new SqlParameter(DBConstants.CONST_PARAM_PC_CARD_NO, iPC_ST_ID);
                ObjParams[1] = new SqlParameter(DBConstants.CONST_PARAM_PC_ST_SENT_FF_TO_COMMS_STATUS, bStatus);

                if (SqlHelper.ExecuteNonQuery(ConnectionString, DBConstants.USP_UPDATESENTFREEFORMTOCOMMSSTATUS, ObjParams) > -1)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return false;
        }

        public static string GetSettingDB(string Setting)
        {
            string bReturnValue = string.Empty;
            try
            {
                SqlParameter[] sqlparams = GetSettingParameterDB(Setting);
                SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.StoredProcedure,
                            "rsp_getSetting", sqlparams);
                if (sqlparams[3].Value != null || sqlparams[3].Value.ToString() != string.Empty)
                {
                    bReturnValue = sqlparams[3].Value.ToString();
                }
                else
                {
                    bReturnValue = string.Empty;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            return bReturnValue;
        }

        public int GetInstallationByBarPosition(string strBarPosition)
        {
            int iReturnValue = 0;
            try
            {
                SqlParameter[] ObjParams = new SqlParameter[2];
                ObjParams[0] = new SqlParameter(DBConstants.CONST_PARAM_BAR_POSITION_NAME, strBarPosition);
                ObjParams[1] = new SqlParameter();
                ObjParams[1].ParameterName = DBConstants.CONST_PARAM_INSTALLATION_NO;
                ObjParams[1].Direction = ParameterDirection.Output;
                ObjParams[1].Value = 0;
                ObjParams[1].SqlDbType = SqlDbType.Int;

                SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.StoredProcedure,
                                                            DBConstants.SP_RSP_GETINSTALLATIONNOBYBARPOSITION, ObjParams);

                if (ObjParams[1].Value != null)
                {
                    iReturnValue = Convert.ToInt32(ObjParams[1].Value);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return iReturnValue;
        }

        #endregion //Public Methods

        #region Private Methods

        private static string GetExchangeConnectionString()
        {
            return DatabaseHelper.GetExchangeConnectionString();
        }

        /// <summary>
        /// To set parameters for Get Setting SP
        /// </summary>
        /// <param name="strSettingName">string</param>
        /// <returns type=SqlParameter[] >sp_parames</returns>
        private static SqlParameter[] GetSettingParameterDB(string SettingName)
        {
            SqlParameter[] sp_parames = null;
            try
            {

                if (SettingName != null)
                {
                    sp_parames = new SqlParameter[4];

                    sp_parames[0] = new SqlParameter("@Setting_ID", 0);
                    sp_parames[1] = new SqlParameter("@Setting_Name", SettingName.Trim());
                    sp_parames[2] = new SqlParameter("@Setting_Default", string.Empty);

                    sp_parames[3] = new SqlParameter();
                    sp_parames[3].ParameterName = "@Setting_Value";
                    sp_parames[3].Direction = ParameterDirection.Output;
                    sp_parames[3].Value = string.Empty;
                    sp_parames[3].SqlDbType = SqlDbType.VarChar;
                    sp_parames[3].Size = 8000;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return sp_parames;
        }

        #endregion //Private Methods
    }

}
