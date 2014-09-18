using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using BMC.DataAccess;
using BMC.Transport.CashDeskOperatorEntity;
using BMC.Common.ExceptionManagement;
using System.Data.SqlClient;

namespace BMC.DBInterface.CashDeskOperator
{
    public class RedeemOfflineDataAccess
    {
        #region "Private Variables"
        #endregion

        #region "Public Properties"
        #endregion

        #region "Private Function"
        #endregion

        #region "Public Function"


        public bool SaveOfflineTicketDetails(OfflineTicket offlineTicket, out int treasuryNo)
        {
            treasuryNo = 0;

            try
            {
                SqlParameter[] paramss = new SqlParameter[9];
                paramss[0] = new SqlParameter(DBConstants.CONST_PARAM_iINSTALLATION_NUMBER, offlineTicket.InstallationNumber);
                paramss[1] = new SqlParameter(DBConstants.CONST_PARAM_sTICKET_NUMBER, offlineTicket.TicketBarCode);
                paramss[2] = new SqlParameter(DBConstants.CONST_PARAM_dDate, DateTime.Now);
                paramss[3] = new SqlParameter(DBConstants.CONST_PARAM_sWINDOWS_USER,  offlineTicket.UserName);
                paramss[4] = new SqlParameter(DBConstants.CONST_PARAM_iUSERID, offlineTicket.UserID);
                paramss[5] = new SqlParameter(DBConstants.CONST_PARAM_sWORKSTATION, offlineTicket.MachineName);
                paramss[6] = new SqlParameter(DBConstants.CONST_PARAM_sCUSTOMER_DETAILS,  offlineTicket.CustomerDetails);
                paramss[7] = new SqlParameter(DBConstants.CONST_PARAM_iVALUE,  offlineTicket.PayableValue);
                paramss[8] = new SqlParameter(DBConstants.CONST_PARAM_TreasuryNo, treasuryNo);
                paramss[8].DbType = DbType.Int32;
                paramss[8].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, DBConstants.CONST_SP_SAVE_OFFLINE_TICKET_PROC, paramss);
                treasuryNo = Convert.ToInt32(paramss[8].Value);

                return true;

                //DataBaseServiceHandler.ExecuteScalar<int>(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, DBConstants.CONST_SP_SAVE_OFFLINE_TICKET_PROC,
                //                    DataBaseServiceHandler.AddParameter<int>(DBConstants.CONST_PARAM_iINSTALLATION_NUMBER, DbType.Int32, offlineTicket.InstallationNumber),
                //                    DataBaseServiceHandler.AddParameter<string>(DBConstants.CONST_PARAM_sTICKET_NUMBER, DbType.String, offlineTicket.TicketBarCode),
                //                    DataBaseServiceHandler.AddParameter<DateTime>(DBConstants.CONST_PARAM_dDate, DbType.DateTime, DateTime.Now),
                //                    DataBaseServiceHandler.AddParameter<string>(DBConstants.CONST_PARAM_sWINDOWS_USER, DbType.String, offlineTicket.UserName),
                //                    DataBaseServiceHandler.AddParameter<int>(DBConstants.CONST_PARAM_iUSERID, DbType.String, offlineTicket.UserID),
                //                    DataBaseServiceHandler.AddParameter<string>(DBConstants.CONST_PARAM_sWORKSTATION, DbType.String, offlineTicket.MachineName),
                //                    DataBaseServiceHandler.AddParameter<string>(DBConstants.CONST_PARAM_sCUSTOMER_DETAILS, DbType.String, offlineTicket.CustomerDetails),
                //                    DataBaseServiceHandler.AddParameter<float>(DBConstants.CONST_PARAM_iVALUE, DbType.Decimal, offlineTicket.PayableValue));
                //return true;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);                
            }
            return false;
        }

        #endregion

        #region "Public Static Function"
        #endregion

    }
}
