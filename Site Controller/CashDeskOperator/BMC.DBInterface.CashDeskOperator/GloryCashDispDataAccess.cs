using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using BMC.Transport;
using BMC.DataAccess;
using BMC.Common.ExceptionManagement;
using System.Data;

namespace BMC.DBInterface.CashDeskOperator
{
    public class GloryCashDispDataAccess
    {
        public bool InsertGloryOpenSessDetails(GloryCashDetails gcd, ref int Seqid)
        {
            bool retVal = true;
            try
            {
                SqlParameter[] ObjParams = new SqlParameter[13];
                ObjParams[0] = new SqlParameter(DBConstants.CONST_PARAM_TRANSACTION_TYPE, gcd.TransactionType);
                ObjParams[1] = new SqlParameter(DBConstants.CONST_PARAM_TRANSACTIONSTARTTIME, gcd.TransactionStarttime);
                ObjParams[2] = new SqlParameter(DBConstants.CONST_PARAM_TRANSACTIONENDTIME, gcd.TransactionEndtime);
                ObjParams[3] = new SqlParameter(DBConstants.CONST_PARAM_TRANSACTIONAMOUNT, gcd.TransactionAmount);
                ObjParams[4] = new SqlParameter(DBConstants.CONST_PARAM_TICKETNO, gcd.TicketNo);
                ObjParams[5] = new SqlParameter(DBConstants.CONST_PARAM_VALIDATIONNO, gcd.ValidationNo);
                ObjParams[6] = new SqlParameter(DBConstants.CONST_PARAM_ASSETNO, gcd.AssetNo);
                ObjParams[7] = new SqlParameter(DBConstants.CONST_PARAM_USERID, gcd.UserID);
                ObjParams[8] = new SqlParameter(DBConstants.CONST_PARAM_SESSIONID, gcd.SessionID);
                ObjParams[9] = new SqlParameter(DBConstants.CONST_PARAM_DEVICE, gcd.Device);
                ObjParams[10] = new SqlParameter(DBConstants.CONST_PARAM_STATUS, gcd.Status);
                ObjParams[11] = new SqlParameter(DBConstants.CONST_PARAM_ERRORCODE, gcd.ErrorCode);

                ObjParams[12] = new SqlParameter
                {
                    ParameterName = DBConstants.CONST_PARAM_SEQID,
                    Value = 0,
                    Direction = ParameterDirection.Output
                };
                Seqid = Convert.ToInt32(SqlHelper.ExecuteScalar(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, DBConstants.CONSTANT_SP_USP_INSERT_GLORYCDDETAILS, ObjParams));
                if (Seqid < 0)
                {
                    retVal = false;
                }
                else
                {
                    Seqid = Convert.ToInt32(ObjParams[12].Value);
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                retVal = false;
            }
            return retVal;
        }

        public bool UpdateGloryDetails(GloryCashDetails gcd)
        {
            bool retVal = true;
            try
            {
                SqlParameter[] ObjParams = new SqlParameter[3];


                ObjParams[0] = new SqlParameter(DBConstants.CONST_PARAM_SEQID, gcd.Sequenceid);
                ObjParams[1] = new SqlParameter(DBConstants.CONST_PARAM_STATUS, gcd.Status);
                ObjParams[2] = new SqlParameter(DBConstants.CONST_PARAM_ERRORCODE, gcd.ErrorCode);
                int Val = SqlHelper.ExecuteNonQuery(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, DBConstants.CONSTANT_SP_USP_UPDATE_GLORYCDDETAILS, ObjParams);
                if (Val < 0)
                {
                    retVal = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                retVal = false;
            }
            return retVal;
        }
    }
}
