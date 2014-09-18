using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using BMC.DataAccess;
using BMC.Common.ExceptionManagement;
using BMC.Transport.CashDeskOperatorEntity;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Reflection;

namespace BMC.DBInterface.CashDeskOperator
{
    public partial class ShortPayDataAccess : DataContext
    {
        #region "Private Variables"
        #endregion

        #region "Public Properties"
        #endregion

        #region "Private Function"
        #endregion

        #region Verify ShortPay
        static MappingSource mappingSource = new AttributeMappingSource();

        #region Extensibility Method Definitions
        partial void OnCreated();
        #endregion

        public ShortPayDataAccess(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}

        [Function(Name = "dbo.usp_ApproveShortPay")]
        public int ApproveShortPay([Parameter(Name = "ShortPayIds", DbType = "VarChar(MAX)")] string shortPayIds, [Parameter(Name = "UserID", DbType = "Int")] System.Nullable<int> userID, [Parameter(Name = "TreasuryID", DbType = "Int")] System.Nullable<int> TreasuryID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), shortPayIds, userID, TreasuryID);
            return ((int)(result.ReturnValue));
        }

        public bool CreateShortPayForApproval(Treasury objTreasury,ref int iShortPayID)
        {
            System.Data.SqlClient.SqlParameter ShortPayID = DataBaseServiceHandler.AddParameter<int>("@ShortPayID",
                DbType.Int32, 0, ParameterDirection.InputOutput);

            try
            {
                DataBaseServiceHandler.ExecuteNonQuery(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, DBConstants.CONST_SP_CREATE_SHORTPAY_FOR_APPROVAL,
                    DataBaseServiceHandler.AddParameter<int>("@Installation_No", DbType.Int32, objTreasury.InstallationNumber),
                    DataBaseServiceHandler.AddParameter<int>("@User_No", DbType.Int32, objTreasury.UserID),
                    DataBaseServiceHandler.AddParameter<string>("@Treasury_Type", DbType.String, objTreasury.TreasuryType),
                    DataBaseServiceHandler.AddParameter<string>("@Treasury_Reason", DbType.String, objTreasury.TreasuryReason),
                    DataBaseServiceHandler.AddParameter<double>("@Treasury_Amount", DbType.Double, objTreasury.TreasuryAmount),
                    DataBaseServiceHandler.AddParameter<int>("@Treasury_Reason_Code", DbType.Int32, objTreasury.TreasuryReasonCode),
                    DataBaseServiceHandler.AddParameter<int>("@Treasury_Issuer_User_No", DbType.Int32, objTreasury.TreasuryIssuerUserNo),
                    ShortPayID
                    );
                
                iShortPayID = int.Parse(ShortPayID.Value.ToString());

                return true;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return false;
            }
        }

        [Function(Name = "dbo.usp_RemoveShortPay")]
        public int CancelShortPayForApproval([Parameter(Name = "ShortPayId", DbType = "Int")] System.Nullable<int> shortPayId)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), shortPayId);
            return ((int)(result.ReturnValue));
        }        

        #endregion Verify ShortPay

        #region "Public Function"
        public DataTable GetFailureReasons()
        {
            DataTable FailureReason = new DataTable();
            try
            {
                //DataBaseServiceHandler.ConnectionString = CommonDataAccess.ExchangeConnectionString;
                FailureReason = DataBaseServiceHandler.Fill(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, DBConstants.CONST_SP_GET_FAILURE_REASONS, FailureReason);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);                
            }
            return FailureReason;
        }

        

        public int SaveTreasuryDetails(Treasury objTreasury)
        {
            System.Data.SqlClient.SqlParameter TreasuryNumber = DataBaseServiceHandler.AddParameter<int>(DBConstants.CONST_PARAM_TREASURY_NUMBER, 
                DbType.Int32,0, ParameterDirection.InputOutput);
                
            try
            {
                //DataBaseServiceHandler.ConnectionString = CommonDataAccess.ExchangeConnectionString;
                DataBaseServiceHandler.ExecuteNonQuery(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, DBConstants.CONST_SP_SAVE_TREASURY_PROC,
                    DataBaseServiceHandler.AddParameter<int>(DBConstants.CONST_PARAM_INSTALLATION_NO, DbType.Int32, objTreasury.InstallationNumber),
                    DataBaseServiceHandler.AddParameter<int>(DBConstants.CONST_PARAM_COLLECTION_NO, DbType.Int32, objTreasury.CollectionNumber),
                    DataBaseServiceHandler.AddParameter<int>(DBConstants.CONST_PARAM_USER_ID, DbType.Int32, objTreasury.UserID),
                    DataBaseServiceHandler.AddParameter<string>(DBConstants.CONST_PARAM_TREASURY_TYPE, DbType.String, objTreasury.TreasuryType),
                    DataBaseServiceHandler.AddParameter<string>(DBConstants.CONST_PARAM_TREASURY_REASON, DbType.String, objTreasury.TreasuryReason),
                    //DataBaseServiceHandler.AddParameter<int>(DBConstants.CONST_PARAM_TREASURY_BREAKDOWN_2P, DbType.Int32, objTreasury.TreasuryBreakdown2p),
                    //DataBaseServiceHandler.AddParameter<int>(DBConstants.CONST_PARAM_TREASURY_BREAKDOWN_5P, DbType.Int32, objTreasury.TreasuryBreakdown5p),
                    //DataBaseServiceHandler.AddParameter<int>(DBConstants.CONST_PARAM_TREASURY_BREAKDOWN_10P, DbType.Int32, objTreasury.TreasuryBreakdown10p),
                    //DataBaseServiceHandler.AddParameter<int>(DBConstants.CONST_PARAM_TREASURY_BREAKDOWN_20P, DbType.Int32, objTreasury.TreasuryBreakdown20p),
                    //DataBaseServiceHandler.AddParameter<int>(DBConstants.CONST_PARAM_TREASURY_BREAKDOWN_50P, DbType.Int32, objTreasury.TreasuryBreakdown50p),
                    //DataBaseServiceHandler.AddParameter<int>(DBConstants.CONST_PARAM_TREASURY_BREAKDOWN_100P, DbType.Int32, objTreasury.TreasuryBreakdown100p),
                    //DataBaseServiceHandler.AddParameter<int>(DBConstants.CONST_PARAM_TREASURY_BREAKDOWN_200P, DbType.Int32, objTreasury.TreasuryBreakdown200p),
                    DataBaseServiceHandler.AddParameter<double>(DBConstants.CONST_PARAM_TREASURY_AMOUNT, DbType.Double, objTreasury.TreasuryAmount),
                    DataBaseServiceHandler.AddParameter<int>(DBConstants.CONST_PARAM_TREASURY_ALLOCATED, DbType.Int32, objTreasury.TreasuryAllocated),
                    DataBaseServiceHandler.AddParameter<string>(DBConstants.CONST_PARAM_TREASURY_MEMBERSHIP_NO, DbType.String, objTreasury.TreasuryMembershipNo),
                    DataBaseServiceHandler.AddParameter<int>(DBConstants.CONST_PARAM_TREASURY_REASON_CODE, DbType.Int32, objTreasury.TreasuryReasonCode),
                    DataBaseServiceHandler.AddParameter<int>(DBConstants.CONST_PARAM_TREASURY_ISSUER_USER_NO, DbType.Int32, objTreasury.TreasuryIssuerUserNo),
                    DataBaseServiceHandler.AddParameter<bool>(DBConstants.CONST_PARAM_TREASURY_TEMP, DbType.Boolean, objTreasury.TreasuryTemp),
                    DataBaseServiceHandler.AddParameter<int>(DBConstants.CONST_PARAM_TREASURY_FLOATISSUEDBY, DbType.Int32, objTreasury.TreasuryFloatIssuedBy),
                    DataBaseServiceHandler.AddParameter<long>(DBConstants.CONST_PARAM_TREASURY_AUTHORIZEDUSER_NO, DbType.Int64, objTreasury.AuthorizedUser_No),
                    DataBaseServiceHandler.AddParameter<DateTime>(DBConstants.CONST_PARAM_TREASURY_AUTHORIZED_DATE, DbType.DateTime, objTreasury.Authorized_Date),
                    TreasuryNumber);
                return (int.Parse(TreasuryNumber.Value.ToString()));
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return -1;
            }

        }

        public int SaveReasonDetails(ReasonCode objReason)
        {
            try 
            {
                DataBaseServiceHandler.ExecuteNonQuery(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, DBConstants.CONST_SP_SAVE_REASON_PROC,
                     DataBaseServiceHandler.AddParameter<int>(DBConstants.CONST_PARAM_REASONCODE, DbType.Int32, objReason.Reason_Code),
                    DataBaseServiceHandler.AddParameter<string>(DBConstants.CONST_PARAM_REASONDESCRIPTION, DbType.String, objReason.ReasonDescription) );

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            
            return 1;

        }

        public int DeleteReasonDetails(ReasonCode objReason)
        {
            try
            {
                DataBaseServiceHandler.ExecuteNonQuery(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, DBConstants.CONST_SP_DELETE_REASON_PROC,
                                    DataBaseServiceHandler.AddParameter<string>(DBConstants.CONST_PARAM_REASONDESCRIPTION, DbType.String, objReason.ReasonDescription));

                
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            return 1;

        }

        public DataTable GetTicketingExceptionTable(string TicketNumber)
        {
            DataTable ticketingException = new DataTable();
            try
            {
                //DataBaseServiceHandler.ConnectionString = CommonDataAccess.ExchangeConnectionString;
                ticketingException = DataBaseServiceHandler.Fill(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, DBConstants.CONST_SP_GET_TICKETING_EXCEPTIONS_PROC, ticketingException,
                    DataBaseServiceHandler.AddParameter<string>(DBConstants.CONST_PARAM_TE_TICKET_NUMBER, DbType.Int32, TicketNumber),
                    DataBaseServiceHandler.AddParameter<int>(DBConstants.CONST_PARAM_TE_ID, DbType.Int32, 0));                
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);                
            }
            return ticketingException;
        }

        public int UpdateVoidorExpiredTreasury(VoidOrExpiredTreasury ExpiredTreasury)
        {
            try
            {
                //DataBaseServiceHandler.ConnectionString = CommonDataAccess.ExchangeConnectionString;
                System.Data.SqlClient.SqlParameter outputParameter = DataBaseServiceHandler.AddParameter<int>("@OutVal", DbType.Int32, 0, ParameterDirection.InputOutput);
                DataBaseServiceHandler.ExecuteNonQuery(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, DBConstants.CONST_SP_UPDATE_VOID_EXPIRED_TREASURY_PROC,
                    DataBaseServiceHandler.AddParameter<string>(DBConstants.CONST_PARAM_TRANSACTION_TYPE, DbType.String, ExpiredTreasury.TransactionType),
                    DataBaseServiceHandler.AddParameter<string>(DBConstants.CONST_PARAM_TICKETNUMBERorID, DbType.String, ExpiredTreasury.TicketNumber),
                    DataBaseServiceHandler.AddParameter<string>(DBConstants.CONST_PARAM_TREASURYREASON, DbType.String, ExpiredTreasury.TreasuryReason),
                    outputParameter);

                return int.Parse(outputParameter.Value.ToString());

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);                
            }
            return -1;
        }

        public int UpdateTicketException(int ID, string TicketNumber, string Value)
        {
            try
            {

                //DataBaseServiceHandler.ConnectionString = CommonDataAccess.ExchangeConnectionString;
                return DataBaseServiceHandler.ExecuteNonQuery(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, DBConstants.CONST_SP_UPDATE_TICKET_EXCEPTION_PROC,
                    DataBaseServiceHandler.AddParameter<string>(DBConstants.CONST_PARAM_TE_TICKET_NUMBER, DbType.String, TicketNumber),
                    DataBaseServiceHandler.AddParameter<int>(DBConstants.CONST_PARAM_TE_ID, DbType.Int32, ID),
                    DataBaseServiceHandler.AddParameter<string>(DBConstants.CONST_PARAM_TE_STATUS, DbType.Int32, Value));
                
                
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);                
            }
            return -1;
        }

        public int InsertException(TicketException objException)
        {
            try
            {
                //DataBaseServiceHandler.ConnectionString = CommonDataAccess.ExchangeConnectionString;
                return DataBaseServiceHandler.ExecuteNonQuery(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, DBConstants.CONST_SP_INSERT_EXCEPTION_PROC,
                    DataBaseServiceHandler.AddParameter<int>(DBConstants.CONST_PARAM_INSTALLATION_ID, DbType.Int32, objException.InstallationNumber),
                    DataBaseServiceHandler.AddParameter<int>(DBConstants.CONST_PARAM_EXCEPTION_TYPE, DbType.Int32, objException.ExceptionType),
                    DataBaseServiceHandler.AddParameter<string>(DBConstants.CONST_PARAM_EXCEPTION_DETAILS, DbType.String, objException.ExceptionDetails),
                    DataBaseServiceHandler.AddParameter<string>(DBConstants.CONST_PARAM_EXCEPTION_REFERENCE, DbType.String, objException.Reference),
                    DataBaseServiceHandler.AddParameter<int>(DBConstants.CONST_PARAM_EXCEPTION_USER, DbType.Int32, objException.User));
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return -1;
            }
        }

        public int GetMaxTreasuryID()
        {
            try
            {

                return int.Parse(SqlHelper.ExecuteScalar(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, DBConstants.CONST_SP_GET_MAX_TREASURYID).ToString());
            }
            catch (Exception ex)
            {

                ExceptionManager.Publish(ex);
                return -1;
            }
        }

        #endregion

        #region "Public Static Function"
        #endregion        
        
    }

}
