using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Reflection;
using System.Data.Linq.Mapping;
using BMC.DataAccess;
using System.Data;
using BMC.Security;
using System.Data.SqlClient;
using BMC.Transport;
using BMC.Common.LogManagement;

namespace BMC.DBInterface.CashDeskOperator
{
    public class VaultDataAccess : DataContext
    {

        public VaultDataAccess(IDbConnection dbConnection)
            : base(dbConnection)
        {
        }
        public DataTable GetVaultDevices()
        {
            DataSet oDs = SqlHelper.ExecuteDataset(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure,
                       "rsp_Vault_GetDeviceDetails");
            if (oDs == null || oDs.Tables.Count == 0)
            {
                LogManager.WriteLog("VaultDataAccess->GetVaultDevices: No data returned from DB", LogManager.enumLogLevel.Debug);
                return null;
            }
            return oDs.Tables[0];
        }
        public DataTable GetTransactionReasons()
        {
            DataSet oDs = SqlHelper.ExecuteDataset(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure,
                       "rsp_Vault_GetTransactionReasons");

            if (oDs == null || oDs.Tables.Count == 0)
            {
                LogManager.WriteLog("VaultDataAccess->GetTransactionReasons: No data returned from DB", LogManager.enumLogLevel.Debug);
                return null;
            }
            return oDs.Tables[0];

        }
        public DataSet GetVaultTransactionEvents(int Vault_Id, string Type, int No_Of_Records, string SearchKey, DateTime StartDate, DateTime EndDate)
        {

            SqlParameter[] Params = new SqlParameter[5];         
            Params[0] = new SqlParameter("@EventType", Type);
            Params[1] = new SqlParameter("@No_Of_Records", No_Of_Records);
            Params[2] = new SqlParameter("@SearchKey", SearchKey);
            Params[3] = new SqlParameter("@StartDate", StartDate);
            Params[4] = new SqlParameter("@EndDate", EndDate);
            DataSet oDs = SqlHelper.ExecuteDataset(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure,
                       "rsp_Vault_TransactionHistory", Params);

            return oDs;

        }

        public DataTable FillVault(int Device_ID, decimal Amount, decimal CurrentBalance, int iWitDrawl, string Type, int Reason_id, ref int iResult, bool sendAlert)
        {

            SqlParameter[] Params = new SqlParameter[11];
            Params[0] = new SqlParameter("@Device_ID", Device_ID);
            Params[1] = new SqlParameter("@FillAmount", Amount);
            Params[2] = new SqlParameter("@CurrentBalance", CurrentBalance);
            Params[3] = new SqlParameter("@User_ID", SecurityHelper.CurrentUser.SecurityUserID);
            Params[4] = new SqlParameter("@WithDrawFlag", iWitDrawl);
            Params[5] = new SqlParameter("@Type", Type);
            Params[6] = new SqlParameter("@ReturnBalance", 1);
            Params[7] = new SqlParameter("@Transaction_Reason_ID", Reason_id);
            Params[8] = new SqlParameter("@Result", 0);
            Params[8].Direction = ParameterDirection.Output;
            Params[9] = new SqlParameter("@CassetteXML", DBNull.Value);
            Params[10] = new SqlParameter("@SendAlert", sendAlert);


            //Using Sql adapter as out param did not work in SQL helper 
            SqlDataAdapter oAdapter = null;
            SqlCommand oCmd = new SqlCommand("usp_Vault_FillVault", new SqlConnection(CommonDataAccess.ExchangeConnectionString));
            oCmd.CommandType = CommandType.StoredProcedure;
            oCmd.Parameters.AddRange(Params);
            oAdapter = new SqlDataAdapter(oCmd);
            DataSet oDs = new DataSet();
            oAdapter.Fill(oDs);
            //Result =-1 if curren 
            iResult = Convert.ToInt32(Params[8].Value);
            if (oDs == null || oDs.Tables.Count == 0)
            {
                LogManager.WriteLog("VaultDataAccess->FillVault: No data returned from DB", LogManager.enumLogLevel.Debug);
                return null;
            }
            return oDs.Tables[0];
        }
        public DataTable GetFillHistory(int Device_ID, int No_Of_Records,int TransTypeID)
        {

            SqlParameter[] Params = new SqlParameter[3];
            Params[0] = new SqlParameter("@Device_ID", Device_ID);
            Params[1] = new SqlParameter("@No_Of_Records", No_Of_Records);
            Params[2] = new SqlParameter("@Type_ID", TransTypeID);

            DataSet oDs = SqlHelper.ExecuteDataset(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, "rsp_Vault_GetFillHistory", Params);
            if (oDs == null || oDs.Tables.Count == 0)
            {
                LogManager.WriteLog("VaultDataAccess->GetFillHistory: No data returned from DB", LogManager.enumLogLevel.Debug);
                return null;
            }
            return oDs.Tables[0];
        }

        public DataTable GetBalance(int Device_ID, ref int result)
        {

            SqlParameter[] Params = new SqlParameter[3];
            Params[0] = new SqlParameter("@Vault_id", Device_ID);
            Params[1] = new SqlParameter("@User_ID", SecurityHelper.CurrentUser.SecurityUserID);
            Params[2] = new SqlParameter("@Result", result);
            Params[2].Direction = ParameterDirection.Output;
            DataSet oDs = SqlHelper.ExecuteDataset(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, "usp_Vault_GetBalanceWithRefresh", Params);
            if (oDs == null || oDs.Tables.Count == 0)
            {
                LogManager.WriteLog("VaultDataAccess->GetBalance: No data returned from DB", LogManager.enumLogLevel.Debug);
                return null;
            }
            return oDs.Tables[0];
        }

        public DataTable DropVault(int Device_ID, int Reason_id, bool FinalDrop, ref int result)
        {

            SqlParameter[] Params = new SqlParameter[6];
            Params[0] = new SqlParameter("@Vault_id", Device_ID);
            Params[1] = new SqlParameter("@User_ID", SecurityHelper.CurrentUser.SecurityUserID);
            Params[2] = new SqlParameter("@Transaction_Reason_ID", Reason_id);
            Params[3] = new SqlParameter("@CassetteXML", DBNull.Value);
            Params[4] = new SqlParameter("@IsFinalDrop", FinalDrop);
            Params[5] = new SqlParameter("@Result", result);
            Params[5].Direction = ParameterDirection.Output;
            DataSet oDs = SqlHelper.ExecuteDataset(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, "usp_Vault_Drop", Params);
            if (oDs == null || oDs.Tables.Count == 0)
            {
                LogManager.WriteLog("VaultDataAccess->GetBalance: No data returned from DB", LogManager.enumLogLevel.Debug);
                return null;
            }
            return oDs.Tables[0];
        }



        [Function(Name = "dbo.rsp_Vault_GetUndeclaredDrops")]
        [ResultType(typeof(rsp_Vault_GetUndeclaredDropsResult))]
        [ResultType(typeof(rsp_GetVaultCassetteDropsResult))]
        public IMultipleResults GetUndeclaredDrops(bool? getCassette)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), getCassette);
            return (IMultipleResults)(result.ReturnValue);
        }

        [Function(Name = "dbo.usp_Vault_UpdateVaultDrops")]
        public int UpdateVaultDrops([Parameter(Name = "DeclaredBalance", DbType = "Decimal(18,2)")] System.Nullable<decimal> declaredBalance,
            [Parameter(Name = "Declared", DbType = "Bit")] System.Nullable<bool> declared,
            [Parameter(Name = "DropID", DbType = "BigInt")] System.Nullable<long> dropID,
           [Parameter(Name = "ModifiedUser", DbType = "Int")] System.Nullable<int> ModifiedUser,
            [Parameter(Name = "CassetteXML", DbType = "Xml")] System.Xml.Linq.XElement cassetteXML)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), declaredBalance, declared, dropID, ModifiedUser, cassetteXML);
            return ((int)(result.ReturnValue));
        }


        [Function(Name = "dbo.rsp_Vault_GetCassetteDetails")]
        public ISingleResult<rsp_GetNGADetailsResult> GetCassetteDetails([Parameter(Name = "VaultID", DbType = "Int")] System.Nullable<int> vaultID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), vaultID);
            return ((ISingleResult<rsp_GetNGADetailsResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_NGA_GetName")]
        public ISingleResult<rsp_GetNGANameResult> GetNGAName([Parameter(Name = "Type", DbType = "VarChar(150)")] string type)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), type);
            return ((ISingleResult<rsp_GetNGANameResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_NGA_GetTypes")]
        public ISingleResult<rsp_GetNGATypesResult> GetNGATypes()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<rsp_GetNGATypesResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.usp_NGA_Enroll")]
        public int EnrollNGA([Parameter(Name = "Installation_No", DbType = "Int")] System.Nullable<int> installation_No, [Parameter(Name = "USER_ID", DbType = "Int")] System.Nullable<int> uSER_ID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), installation_No, uSER_ID);
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.usp_Vault_FillVault")]
        public ISingleResult<usp_Vault_FillVaultResult> Vault_FillVault([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Device_ID", DbType = "Int")] System.Nullable<int> device_ID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "FillAmount", DbType = "Decimal(15,2)")] System.Nullable<decimal> fillAmount, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "CurrentBalance", DbType = "Decimal(15,2)")] System.Nullable<decimal> currentBalance, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "User_ID", DbType = "Int")] System.Nullable<int> user_ID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "WithDrawFlag", DbType = "Int")] System.Nullable<int> withDrawFlag, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Type", DbType = "VarChar(10)")] string type, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ReturnBalance", DbType = "Bit")] System.Nullable<bool> returnBalance, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Transaction_Reason_ID", DbType = "Int")] System.Nullable<int> transaction_Reason_ID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Result", DbType = "Int")] ref System.Nullable<int> result, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "CassetteXML", DbType = "Xml")] System.Xml.Linq.XElement cassetteXML, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "SendAlert", DbType = "Bit")] System.Nullable<bool> sendAlert)
        {
            IExecuteResult result1 = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), device_ID, fillAmount, currentBalance, user_ID, withDrawFlag, type, returnBalance, transaction_Reason_ID, result, cassetteXML, sendAlert);
            result = ((System.Nullable<int>)(result1.GetParameterValue(8)));
            return ((ISingleResult<usp_Vault_FillVaultResult>)(result1.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.usp_Vault_Drop")]
        public ISingleResult<usp_Vault_DropResult> Vault_Drop([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Vault_ID", DbType = "Int")] System.Nullable<int> vault_ID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "User_ID", DbType = "Int")] System.Nullable<int> user_ID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Transaction_Reason_ID", DbType = "Int")] System.Nullable<int> transaction_Reason_ID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "CassetteXML", DbType = "Xml")] System.Xml.Linq.XElement cassetteXML, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "IsFinalDrop", DbType = "Bit")] System.Nullable<bool> isFinalDrop, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Result", DbType = "Int")] ref int result)
        {
            IExecuteResult result1 = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), vault_ID, user_ID, transaction_Reason_ID, cassetteXML, isFinalDrop, result);
            result = ((System.Nullable<int>)(result1.GetParameterValue(5)) ?? 0);
            return ((ISingleResult<usp_Vault_DropResult>)(result1.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.rsp_NGA_GetCassetteDetails")]
        public ISingleResult<NGA_GetCassetteDetailsResult> NGA_GetCassetteDetails([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "VaultID", DbType = "Int")] System.Nullable<int> vaultID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), vaultID);
            return ((ISingleResult<NGA_GetCassetteDetailsResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.rsp_Vault_CheckStandardFillsCount")]
        public ISingleResult<rsp_Vault_CheckStandardFillsCountResult> CheckStandardFillsCount([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Vault_ID", DbType = "Int")] System.Nullable<int> vault_ID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), vault_ID);
            return ((ISingleResult<rsp_Vault_CheckStandardFillsCountResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.rsp_Vault_GetFillHistoryDetails")]
        public ISingleResult<rsp_Vault_GetFillHistoryDetailsResult> GetFillHistoryDetails([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Transaction_ID", DbType = "BigInt")] System.Nullable<long> transaction_ID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), transaction_ID);
            return ((ISingleResult<rsp_Vault_GetFillHistoryDetailsResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.rsp_Vault_GetTransactionTypes")]
        public ISingleResult<rsp_Vault_GetTransactionTypesResult> GetTransactionTypes()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<rsp_Vault_GetTransactionTypesResult>)(result.ReturnValue));
        }


    }
}
