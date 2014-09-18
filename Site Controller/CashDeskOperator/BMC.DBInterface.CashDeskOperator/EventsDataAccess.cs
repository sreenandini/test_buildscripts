using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using BMC.DataAccess;
using BMC.Common.ExceptionManagement;
using BMC.Common;
using BMC.Common.LogManagement;

namespace BMC.DBInterface.CashDeskOperator
{
    public class EventsDataAccess
    {
        public DataTable GetEventsDetails(DateTime startDate, DateTime endDatetime, string strBarPos,
                                          int showCleared, string strEventType, int iPageSize, int LegalEvent)
        {
            DataSet dsEventDetails = new DataSet();
            try
            {
                SqlParameter[] objParams = new SqlParameter[7];
                objParams[0] = new SqlParameter("@Bar_Pos_Name", strBarPos);
                objParams[1] = new SqlParameter("@StartDate", startDate);
                objParams[2] = new SqlParameter("@EndDate", endDatetime);
                objParams[3] = new SqlParameter("@ShowCleared", showCleared);
                objParams[4] = new SqlParameter("@Event_Type", strEventType);
                objParams[5] = new SqlParameter("@PageSize", iPageSize);
                objParams[6] = new SqlParameter("@LegalEvent", LegalEvent);
                
                SqlHelper.FillDataset(CommonDataAccess.ExchangeConnectionString, "rsp_GetPositionEventDetails_Paging", dsEventDetails, new string[] { "Events" }, objParams);

                if (dsEventDetails.Tables.Count > 0)
                {   
                    return dsEventDetails.Tables[0];
                }
                else
                {
                    return new DataTable(); 
                }
 
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return new DataTable();
            }
            
        }

        /// <summary>
        /// Get Cleared/Uncleared Events 
        /// </summary>
        /// <returns></returns>
        public bool CheckForUnclearedEvents()
        {
            int unclearedEvents = 0;
            try
            {
                LogManager.WriteLog("Inside CheckForUnclearedEvents method", LogManager.enumLogLevel.Info);
                SqlParameter[] sqlParam = new SqlParameter[1];
                SqlParameter param = new SqlParameter();
                param.SqlDbType = SqlDbType.Int;
                param.Direction = ParameterDirection.Output;
                param.ParameterName = "@UnclearedEvents";
                sqlParam[0] = param;

                SqlHelper.ExecuteNonQuery(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, "rsp_IsUnclearedEventsAvailable", sqlParam);
                unclearedEvents = Convert.ToInt32(sqlParam[0].Value);
                return unclearedEvents == 0 ? true : false;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return false;
            }
        }

        public bool UpdateEventDetails(string clearType, string eventType, int eventNo, int installationNo)
        {
            int errorCode = 0;
            string errorMessage = string.Empty;

            try
            {
                LogManager.WriteLog("Inside Method", LogManager.enumLogLevel.Info);

                DataBaseServiceHandler.ExecuteNonQuery(CommonDataAccess.ExchangeConnectionString,
                    CommandType.StoredProcedure, DBConstants.CONST_SP_USP_CLEAREVENTDETAILS,
                    DataBaseServiceHandler.AddParameter<int>(DBConstants.CONST_PARAM_USER_ID, DbType.Int32, BMC.Security.SecurityHelper.CurrentUser.SecurityUserID, ParameterDirection.Input),
                    DataBaseServiceHandler.AddParameter<string>(DBConstants.CONST_PARAM_CLEAR_TYPE, DbType.String, clearType, ParameterDirection.Input),
                    DataBaseServiceHandler.AddParameter<string>(DBConstants.CONST_PARAM_EVENTTYPE, DbType.String, eventType, ParameterDirection.Input),
                    DataBaseServiceHandler.AddParameter<int>(DBConstants.CONST_PARAM_EVENT_NO, DbType.Int32, eventNo, ParameterDirection.Input),
                    DataBaseServiceHandler.AddParameter<int>(DBConstants.CONST_PARAM_INSTALATION_NO, DbType.Int32, installationNo, ParameterDirection.Input),
                    DataBaseServiceHandler.AddParameter<int>(DBConstants.CONST_PARAM_ERROR_CODE, DbType.Int32, errorCode, ParameterDirection.Output),
                    DataBaseServiceHandler.AddParameter<string>(DBConstants.CONST_PARAM_ERROR_MESSAGE, DbType.String, errorMessage, ParameterDirection.Output));

                LogManager.WriteLog(string.Format("{0} - {1}", "Error Code", errorCode.ToString()), LogManager.enumLogLevel.Info);
                LogManager.WriteLog(string.Format("{0} - {1}", "Error Message", errorMessage), LogManager.enumLogLevel.Info); 

                return errorCode == 0 ? true : false;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return false;                
            }
        }

        public string FillEventType()
        {
            string strEventTypes = string.Empty;

            try
            {
                strEventTypes = CommonDataAccess.GetSettingValue("EVENTTYPELIST");                
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                strEventTypes = string.Empty;
            }
            return strEventTypes;
        }
    }
}
