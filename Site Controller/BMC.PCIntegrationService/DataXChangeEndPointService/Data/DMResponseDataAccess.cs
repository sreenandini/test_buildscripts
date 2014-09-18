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
using BMC.PlayerGateway.Gateway;
using FreeForm;

namespace DataXChangeEndPointService.Data
{
    public class DMResponseDataAccess
    {
        #region Variables

        private static string _ConnectionString;
        private static bool? _IsDMEnabled;

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

        public static bool IsDMEnabled
        {
            get
            {
                try
                {
                    if (_IsDMEnabled == null)
                    {
                        _IsDMEnabled = GetSettingDB("USE_DIRECTED_MESSAGING").ToUpper() == "TRUE" ? true : false;
                    }

                }
                catch { _IsDMEnabled = null; }
                return _IsDMEnabled.GetValueOrDefault();
            }
        }


        #endregion //Properties

        #region Public Methods

        public bool InsertDMMessages(DMMessages dmMessages)
        {
            try
            {
                SqlParameter[] ObjParams = new SqlParameter[15];
                ObjParams[0] = new SqlParameter(DBConstants.CONST_PARAM_DM_CARD_NO, dmMessages.CardNumber);
                ObjParams[1] = new SqlParameter(DBConstants.CONST_PARAM_DM_SLOT_NO, dmMessages.SlotNumber);
                ObjParams[2] = new SqlParameter(DBConstants.CONST_PARAM_DM_STAND, dmMessages.SlotNumber);
                ObjParams[3] = new SqlParameter(DBConstants.CONST_PARAM_DM_FIRSTNAME, dmMessages.FirstName);
                ObjParams[4] = new SqlParameter(DBConstants.CONST_PARAM_DM_BIRTHDAY, dmMessages.Birthday);
                ObjParams[5] = new SqlParameter(DBConstants.CONST_PARAM_DM_ACTUALMESSAGE, dmMessages.DisplayMessage);
                ObjParams[6] = new SqlParameter(DBConstants.CONST_PARAM_DM_TYPE, dmMessages.TransactionCode);
                ObjParams[7] = new SqlParameter(DBConstants.CONST_PARAM_DM_DISPLAYCONTROL, dmMessages.DisplayControl);
                ObjParams[8] = new SqlParameter(DBConstants.CONST_PARAM_DM_CONDITIONALMASK, dmMessages.ConditionalMask);
                ObjParams[9] = new SqlParameter(DBConstants.CONST_PARAM_DM_TOTALSEGMENTS, dmMessages.TotalSegments);
                ObjParams[10] = new SqlParameter(DBConstants.CONST_PARAM_DM_DM_SEGMENTNUMBER, dmMessages.SegmentNumber);
                ObjParams[11] = new SqlParameter(DBConstants.CONST_PARAM_DM_EPICONTROL1, dmMessages.EPIControl1);
                ObjParams[12] = new SqlParameter(DBConstants.CONST_PARAM_DM_EPICONTROL2, dmMessages.EPIControl2);
                ObjParams[13] = new SqlParameter(DBConstants.CONST_PARAM_DM_EPICONTROL3, dmMessages.EPIControl3);
                ObjParams[14] = new SqlParameter(DBConstants.CONST_PARAM_DM_EPICONTROL4, dmMessages.EPIControl4);
                //ObjParams[15] = new SqlParameter(DBConstants.CONST_PARAM_DM_CIR_Sent_TO_Comms
                //ObjParams[16] = new SqlParameter(DBConstants.CONST_PARAM_DM_Sent_FF_TO_Comms 
                //ObjParams[17] = new SqlParameter(DBConstants.CONST_PARAM_DM_CIR_COMMS_DATA   



                if (SqlHelper.ExecuteNonQuery(ConnectionString, DBConstants.SP_USP_INSERTDMNOTIFICATIONRESPONSES, ObjParams) > -1)
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

                DataSet ds = SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, DBConstants.SP_RSP_GET_DMMESSAGES, ObjParams);
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

        public bool DeleteSentDMMessage(int iPC_ST_ID)
        {
            try
            {
                SqlParameter[] ObjParams = new SqlParameter[1];
                ObjParams[0] = new SqlParameter(DBConstants.CONST_PARAM_DM_ID, iPC_ST_ID);

                if (SqlHelper.ExecuteNonQuery(ConnectionString, DBConstants.DSP_DELETEDIRECTMESSAGE, ObjParams) > -1)
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