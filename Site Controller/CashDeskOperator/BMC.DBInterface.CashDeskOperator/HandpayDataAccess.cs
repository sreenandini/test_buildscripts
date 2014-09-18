using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using BMC.DataAccess;
using BMC.Common.ExceptionManagement;
using System.Linq;
using System.Text;
using BMC.Transport.CashDeskOperatorEntity;

namespace BMC.DBInterface.CashDeskOperator
{
    public partial class HandpayDataAccess
    {
        CommonDataAccess commonDataAccess = new CommonDataAccess();

        #region "Private Variables"
        #endregion

        #region "Public Properties"
        #endregion

        #region "Private Function"
        #endregion

        #region "Public Function"

        /// <summary>
        /// Get Unprocessed handpays
        /// </summary>
        /// <param name=""></param>
        /// <returns >Datatable</returns>
        public DataTable GetUnProcessedHandPays()
        {
            try
            {
                return SqlHelper.ExecuteDataset(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, DBConstants.CONST_SP_RSP_GETHANDPAYS).Tables[0];
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return new DataTable();
            }
        }

        public DataTable GetUnProcessedHandPays(int InstallationNo)
        {
            DataTable dtHandPays = null;

            try
            {
                SqlParameter[] objSQL = new SqlParameter[1];

                SqlParameter objParam1 = new SqlParameter();
                objParam1.ParameterName = "Installation_no";
                objParam1.Direction = ParameterDirection.Input;
                objParam1.SqlDbType = SqlDbType.Int;
                objParam1.Value = InstallationNo;
                objSQL[0] = objParam1;
                dtHandPays = SqlHelper.ExecuteDataset(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, DBConstants.CONST_SP_RSP_GETHANDPAYS, objSQL).Tables[0];

                return dtHandPays;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return new DataTable();
            }
        }

        /// <summary>
        /// Save Handpay, short pay details in Treasury Table
        /// </summary>
        /// <param name="objTreasury">Treasuries</param>
        /// <returns >int</returns>
        public int SaveTreasuryDetails(Treasury objTreasury)
        {
            try
            {
                SqlParameter[] ObjParams = new SqlParameter[22];
                ObjParams[0] = new SqlParameter(DBConstants.CONST_PARAM_INSTALLATION_NO, objTreasury.InstallationNumber);
                ObjParams[1] = new SqlParameter(DBConstants.CONST_PARAM_COLLECTION_NO, objTreasury.CollectionNumber);
                ObjParams[2] = new SqlParameter(DBConstants.CONST_PARAM_USER_ID, objTreasury.UserID);
                ObjParams[3] = new SqlParameter(DBConstants.CONST_PARAM_TREASURY_TYPE, objTreasury.TreasuryType);
                ObjParams[4] = new SqlParameter(DBConstants.CONST_PARAM_TREASURY_REASON, objTreasury.TreasuryReason);
                ObjParams[5] = new SqlParameter(DBConstants.CONST_PARAM_TREASURY_BREAKDOWN_2P, objTreasury.TreasuryBreakdown2p);
                ObjParams[6] = new SqlParameter(DBConstants.CONST_PARAM_TREASURY_BREAKDOWN_5P, objTreasury.TreasuryBreakdown5p);
                ObjParams[7] = new SqlParameter(DBConstants.CONST_PARAM_TREASURY_BREAKDOWN_10P, objTreasury.TreasuryBreakdown10p);
                ObjParams[8] = new SqlParameter(DBConstants.CONST_PARAM_TREASURY_BREAKDOWN_20P, objTreasury.TreasuryBreakdown20p);
                ObjParams[9] = new SqlParameter(DBConstants.CONST_PARAM_TREASURY_BREAKDOWN_50P, objTreasury.TreasuryBreakdown50p);
                ObjParams[10] = new SqlParameter(DBConstants.CONST_PARAM_TREASURY_BREAKDOWN_100P, objTreasury.TreasuryBreakdown100p);
                ObjParams[11] = new SqlParameter(DBConstants.CONST_PARAM_TREASURY_BREAKDOWN_200P, objTreasury.TreasuryBreakdown200p);
                ObjParams[12] = new SqlParameter(DBConstants.CONST_PARAM_TREASURY_AMOUNT, objTreasury.TreasuryAmount);
                ObjParams[13] = new SqlParameter(DBConstants.CONST_PARAM_TREASURY_ALLOCATED, objTreasury.TreasuryAllocated);
                ObjParams[14] = new SqlParameter(DBConstants.CONST_PARAM_TREASURY_MEMBERSHIP_NO, objTreasury.TreasuryMembershipNo);
                ObjParams[15] = new SqlParameter(DBConstants.CONST_PARAM_TREASURY_REASON_CODE, objTreasury.TreasuryReasonCode);
                ObjParams[16] = new SqlParameter(DBConstants.CONST_PARAM_TREASURY_ISSUER_USER_NO, objTreasury.TreasuryIssuerUserNo);
                ObjParams[17] = new SqlParameter(DBConstants.CONST_PARAM_TREASURY_TEMP, objTreasury.TreasuryTemp);
                ObjParams[18] = new SqlParameter(DBConstants.CONST_PARAM_TREASURY_FLOATISSUEDBY, objTreasury.TreasuryFloatIssuedBy);
                ObjParams[19] = new SqlParameter("@AuthorizedUser_No", objTreasury.AuthorizedUser_No);
                ObjParams[20] = new SqlParameter("@Authorized_Date", objTreasury.Authorized_Date);
                ObjParams[21] = new SqlParameter(DBConstants.CONST_PARAM_TREASURY_NUMBER, 0);
                ObjParams[21].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, DBConstants.CONST_SP_SAVE_TREASURY_PROC, ObjParams);
                return (int.Parse(ObjParams[21].Value.ToString()));
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return -1;
            }
        }

        /// <summary>
        /// Get the list of available installation
        /// </summary>
        /// <returns>datatable of list of installation</returns>
        public DataTable GetInstallationList()
        {
            try
            {
                DataSet dsInstallationList = new DataSet();

                SqlHelper.FillDataset(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, DBConstants.CONST_SP_GET_INSTALLTION_LIST_PROC, dsInstallationList, new string[] { "InstallationList" });
                if (dsInstallationList.Tables.Count > 0)
                {
                    return dsInstallationList.Tables[0];
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
        /// Update Ticket Exception Table
        /// </summary>
        /// <param name="objExpiredTreasury">VoidOrExpiredTreasury</param>
        /// <returns >int</returns>
        public int UpdateVoidorExpiredTreasury(VoidOrExpiredTreasury objExpiredTreasury)
        {
            try
            {

                SqlParameter[] ObjParams = new SqlParameter[4];
                ObjParams[0] = new SqlParameter(DBConstants.CONST_PARAM_TRANSACTION_TYPE, objExpiredTreasury.TransactionType);
                ObjParams[1] = new SqlParameter(DBConstants.CONST_PARAM_TICKETNUMBERorID, objExpiredTreasury.TicketNumber);
                ObjParams[2] = new SqlParameter(DBConstants.CONST_PARAM_TREASURYREASON, objExpiredTreasury.TreasuryReason);
                ObjParams[3] = new SqlParameter("@OutVal", 0);
                ObjParams[3].Direction = ParameterDirection.Output;
                return SqlHelper.ExecuteNonQuery(CommonDataAccess.ExchangeConnectionString, DBConstants.CONST_SP_UPDATE_VOID_EXPIRED_TREASURY_PROC, ObjParams);

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return -1;
            }
        }

        public DataTable GetTicketingExceptionTable(string strTicketNumber)
        {
            try
            {
                SqlParameter[] ObjParams = new SqlParameter[2];
                ObjParams[0] = new SqlParameter(DBConstants.CONST_PARAM_TE_TICKET_NUMBER, strTicketNumber);
                ObjParams[1] = new SqlParameter(DBConstants.CONST_PARAM_TE_ID, null);
                return SqlHelper.ExecuteDataset(CommonDataAccess.ExchangeConnectionString, DBConstants.CONST_SP_GET_TICKETING_EXCEPTIONS_PROC, ObjParams).Tables[0];
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return new DataTable();
            }
        }

        /// <summary>
        /// Update Ticket Final Status in Ticket Exception Table
        /// </summary>
        /// <param name="strTicketNumber">string</param>
        /// <returns >bool</returns>
        public bool UpdateTicketException_FinalStatus(string strTicketNumber)
        {
            try
            {
                SqlParameter[] ObjParams = new SqlParameter[1];
                ObjParams[0] = new SqlParameter(DBConstants.CONST_PARAM_TE_TICKET_NUMBER, strTicketNumber);
                if (SqlHelper.ExecuteNonQuery(CommonDataAccess.ExchangeConnectionString, DBConstants.CONST_SP_UPDATE_TICKETEXCEPTION, ObjParams) > -1)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return false;
            }
        }


        public DateTime? GetTreasuryDateTime(int Treasury_ID)
        {
            try
            {
                SqlParameter[] ObjParams = new SqlParameter[1];
                ObjParams[0] = new SqlParameter("Treasury_ID", Treasury_ID);
                DateTime dtTreasury = (DateTime)SqlHelper.ExecuteScalar(CommonDataAccess.ExchangeConnectionString, "rsp_GetTreasury_Date", ObjParams);
                return dtTreasury;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return null;
            }
        }
        #region GCD
        public bool RollbackProcessHandPay(int Ticket_ExceptionID, int Treasury_No)
        {

            try
            {
                SqlParameter[] paramss = new SqlParameter[2];
                paramss[0] = new SqlParameter(DBConstants.CONST_PARAM_TreasuryNo, Treasury_No);
                paramss[1] = new SqlParameter(DBConstants.CONST_PARAM_TicketException_ID, Ticket_ExceptionID);

                int Retval = SqlHelper.ExecuteNonQuery(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, DBConstants.CONST_SP_ROLLBACK_PROCESS_HANDPAY, paramss);
                if (Retval >= 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return false;

        }

        public bool ExportHandPay(int Treasury_No)
        {

            try
            {
                SqlParameter[] paramss = new SqlParameter[2];
                paramss[0] = new SqlParameter(DBConstants.CONST_PARAM_Reference1, Treasury_No);
                paramss[1] = new SqlParameter(DBConstants.CONST_PARAM_Export_Type, "TREASURY");

                SqlHelper.ExecuteNonQuery(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, DBConstants.CONST_SP_EXPORT_HISTORY, paramss);

                return true;

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return false;
        }
        #endregion

        #endregion

        #region "Public Static Function"
        #endregion


        public List<BarPositions> getBarPositions()
        {
            LinqDataAccessDataContext context = new LinqDataAccessDataContext(CommonDataAccess.ExchangeConnectionString);
            return context.GetBarPositions().ToList<BarPositions>();

        }
    }
}
