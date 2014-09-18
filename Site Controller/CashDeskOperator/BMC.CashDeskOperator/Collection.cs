using System;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Reflection;
using BMC.Transport;

namespace BMC.CashDeskOperator
{
    internal class CollectionDataContext : DataContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Data.Linq.DataContext"/> class by referencing the connection used by the .NET Framework.
        /// </summary>


        public CollectionDataContext(IDbConnection dbConnection)
            : base(dbConnection)
        {
        }

        [Function(Name = "dbo.rsp_GetInstalledMachineForCollection")]
        public ISingleResult<CollectionMachine> GetInstalledMachineForCollection()
        {
            var result = ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<CollectionMachine>)(result.ReturnValue));
        }
        [Function(Name = "dbo.rsp_GetInstStatusForAutoDrop")]
        public ISingleResult<StockNumber> GetInstStatusForAutoDrop([Parameter(Name = "InstallationNos", DbType = "VarChar(MAX)")] string installationNos)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), installationNos);
            return ((ISingleResult<StockNumber>)(result.ReturnValue));
        }
        [Function(Name = "dbo.usp_autodropsession")]
        public int UpdateAutoDropSession()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((int)(result.ReturnValue));
        }
        [Function(Name = "dbo.GetAutoDropSessionByBatch", IsComposable = true)]
        public string AutoDropSession([Parameter(Name = "BatchNo", DbType = "Int")] System.Nullable<int> BatchID)
        {
            return (string)(this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), BatchID).ReturnValue);
        }
        [Function(Name = "dbo.usp_RevertFinalDropStatus")]
        public int RevertFinalDropStatus([Parameter(Name = "InstallationNo", DbType = "Int")] int installation_No)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), installation_No);
            return ((int)(result.ReturnValue));
        }
        [Function(Name = "dbo.rsp_GetNotdisabledMachines")]
        public ISingleResult<InstallationData> GetNotdisabledMachines([Parameter(Name = "InstallationNos", DbType = "VarChar(MAX)")] string InstallationNos)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), InstallationNos);
            return ((ISingleResult<InstallationData>)(result.ReturnValue));
        }
        [Function(Name = "dbo.rsp_GetStackerEventNotReceived")]
        public ISingleResult<InstallationData> GetStackerEventNotReceived([Parameter(Name = "InstallationNos", DbType = "VarChar(MAX)")] string InstallationNos)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), InstallationNos);
            return ((ISingleResult<InstallationData>)(result.ReturnValue));
        }
        [Function(Name = "dbo.rsp_GetActiveDropSession")]
        public int GetActiveDropSession()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((int)(result.ReturnValue));
        }
        [Function(Name = "dbo.fn_IsDropSessionCompleted", IsComposable = true)]
        public bool IsDropSessionCompleted([Parameter(Name = "AutoDropSessionNos", DbType = "VarChar(MAX)")] string AutoDropSessionNos)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), AutoDropSessionNos);
            return Convert.ToBoolean(result.ReturnValue);
        }
        [Function(Name = "dbo.IsBatchProcessedByAnotherUser", IsComposable = true)]
        public bool IsBatchProcessedByAnotherUser([Parameter(Name = "BatchNo", DbType = "Int")] int BatchNo, [Parameter(Name = "PartCollectionNo", DbType = "Int")] int PartCollectionNo)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), BatchNo,PartCollectionNo);
            return Convert.ToBoolean(result.ReturnValue);
        }
        [Function(Name = "dbo.rsp_GetUndeclaredCollectionForAutoDrop")]
        public ISingleResult<UndeclaredCollection> GetUndeclaredCollectionForAutoDrop([Parameter(Name = "MachineID", DbType = "VarChar(200)")] string machineID, [Parameter(Name = "AddToExisting", DbType = "Bit")] System.Nullable<bool> addToExisting)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), machineID, addToExisting);
            return ((ISingleResult<UndeclaredCollection>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.rsp_IsUndeclaredPartCollection")]
        public ISingleResult<rsp_IsUndeclaredPartCollectionResult> IsUndeclaredPartCollection([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Installation_no", DbType = "Int")] System.Nullable<int> installation_no)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), installation_no);
            return ((ISingleResult<rsp_IsUndeclaredPartCollectionResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetActiveDropInstallations")]
        public ISingleResult<InstallationData> GetDropActiveSessionData()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<InstallationData>)(result.ReturnValue));
        }
        [Function(Name = "dbo.usp_CreateDropSession")]
        public int CreateDropSession([Parameter(Name = "InstallationNos", DbType = "VarChar(MAX)")] string InstallationNos,
                                     [Parameter(Name = "FinalDrop", DbType = "Int")] System.Nullable<int> FinalDrop,
                                     [Parameter(Name = "UserNo", DbType = "Int")] System.Nullable<int> UserNo,
                                     [Parameter(Name = "BatchNo", DbType = "Int")] System.Nullable<int> BatchNo,
                                     [Parameter(Name = "Batch_Type", DbType = "VARCHAR(50)")] string BatchType,
                                     [Parameter(Name = "Batch_Machine", DbType = "VARCHAR(50)")] string BatchMachine)
        {

            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), InstallationNos, FinalDrop, UserNo, BatchNo, BatchType, BatchMachine);
            return ((int)(result.ReturnValue));
        }
        [Function(Name = "dbo.rsp_GetRouteCollection")]
        public ISingleResult<RouteCollection> GetRouteCollection()
        {
            var result = ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<RouteCollection>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetCollectionBatchByMachine")]
        public ISingleResult<CollectionBatchByMachine> GetCollectionBatchByMachine([Parameter(Name = "MachineName", DbType = "VarChar(200)")] string machineName)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), machineName);
            return ((ISingleResult<CollectionBatchByMachine>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetBarPositionByRouteNo")]
        public ISingleResult<BarPositionRouteNo> GetBarPositionByRouteNo([Parameter(Name = "Route_Name", DbType = "VarChar(200)")] string route_Name)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), route_Name);
            return ((ISingleResult<BarPositionRouteNo>)(result.ReturnValue));
        }

        [Function(Name = "dbo.usp_CreateCollectionBatch")]
        public int CreateCollectionBatch([Parameter(Name = "Collection_Batch_Name", DbType = "VarChar(200)")] string collection_Batch_Name, [Parameter(Name = "Collection_Batch_Machine", DbType = "VarChar(200)")] string collection_Batch_Machine, [Parameter(Name = "User_No", DbType = "Int")] System.Nullable<int> user_No, [Parameter(Name = "ExisitngBatchNo", DbType = "Int")] ref System.Nullable<int> exisitngBatchNo)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), collection_Batch_Name, collection_Batch_Machine, user_No, exisitngBatchNo);
            exisitngBatchNo = ((System.Nullable<int>)(result.GetParameterValue(3)));
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.usp_PerformCollection")]
        public int PerformCollection([Parameter(Name = "Coll_Batch_No", DbType = "Int")] System.Nullable<int> coll_Batch_No, [Parameter(Name = "Installation", DbType = "Int")] System.Nullable<int> installation, [Parameter(Name = "Machine", DbType = "VarChar(100)")] string machine, [Parameter(Name = "Defloat", DbType = "Bit")] System.Nullable<bool> defloat, [Parameter(Name = "ErrorMsg", DbType = "VarChar(500)")] ref string errorMsg, [Parameter(Name = "User_no", DbType = "Int")] int User_no)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), coll_Batch_No, installation, machine, defloat, errorMsg, User_no);
            errorMsg = ((string)(result.GetParameterValue(4)));
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.usp_CreatePartCollection")]
        public int CreatePartCollection([Parameter(Name = "batch_id", DbType = "Int")] System.Nullable<int> batch_id, [Parameter(Name = "User_No", DbType = "Int")] System.Nullable<int> user_No, [Parameter(Name = "Installation_No", DbType = "Int")] System.Nullable<int> installation_No, [Parameter(Name = "Part_Collection_Date", DbType = "DateTime")] System.Nullable<System.DateTime> part_Collection_Date, [Parameter(Name = "Part_Collection_Date_Performed", DbType = "DateTime")] System.Nullable<System.DateTime> part_Collection_Date_Performed, [Parameter(Name = "Part_Collection_Machine", DbType = "VarChar(200)")] string part_Collection_Machine, [Parameter(Name = "PartCollectionID", DbType = "Int")] ref System.Nullable<int> partCollectionID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())),batch_id, user_No, installation_No, part_Collection_Date, part_Collection_Date_Performed, part_Collection_Machine, partCollectionID);
            partCollectionID = ((System.Nullable<int>)(result.GetParameterValue(6)));
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetUndeclaredCollection")]
        public ISingleResult<UndeclaredCollection> GetUndeclaredCollection([Parameter(Name = "MachineID", DbType = "VarChar(200)")] string machineID, [Parameter(Name = "AddToExisting", DbType = "Bit")] System.Nullable<bool> addToExisting, [Parameter(Name = "IsPartDeclaration", DbType = "Bit")] System.Nullable<bool> IsPartDeclaration)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), machineID, addToExisting,IsPartDeclaration);
            return ((ISingleResult<UndeclaredCollection>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetUndeclaredCollectionByBatchNo")]
        public ISingleResult<UndeclaredCollectionBatch> GetUndeclaredCollectionByBatchNo([Parameter(Name = "BatchNo", DbType = "Int")] System.Nullable<int> batchNo)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), batchNo);
            return ((ISingleResult<UndeclaredCollectionBatch>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_UndeclaredDropBatch")]
        public ISingleResult<UndeclaredCollection> GetUndeclaredDropBatch()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<UndeclaredCollection>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetUndeclaredCollectionByBatchNo")]
        public ISingleResult<UndeclaredCollectionBatch> GetFilteredUndeclaredCollectionByBatchNo([Parameter(Name = "BatchNo", DbType = "Int")] System.Nullable<int> batchNo,
            [Parameter(Name = "FilterBy", DbType = "Int")] System.Nullable<int> filterBy,
            [Parameter(Name = "FilterValue", DbType = "VarChar(500)")] string filterValue)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), batchNo, filterBy, filterValue);
            return ((ISingleResult<UndeclaredCollectionBatch>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetUndeclaredCollectionByCollectionNo")]
        public UndeclaredCollectionBatch GetUndeclaredCollectionByCollectionNo([Parameter(Name = "collectionNo", DbType = "Int")] System.Nullable<int> collectionNo)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), collectionNo);
            return ((UndeclaredCollectionBatch)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetSetting")]
        public int GetSetting([Parameter(Name = "Setting_ID", DbType = "Int")] System.Nullable<int> setting_ID, [Parameter(Name = "Setting_Name", DbType = "VarChar(100)")] string setting_Name, [Parameter(Name = "Setting_Default", DbType = "VarChar(8000)")] string setting_Default, [Parameter(Name = "Setting_Value", DbType = "VarChar(8000)")] ref string setting_Value)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), setting_ID, setting_Name, setting_Default, setting_Value);
            setting_Value = ((string)(result.GetParameterValue(3)));
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetUndeclaredPartCollectionByMachine")]
        public ISingleResult<rsp_GetUndeclaredPartCollectionByMachineResult> GetUndeclaredPartCollectionByMachine([Parameter(Name = "Machine_No", DbType = "VarChar(200)")] string machine_No,[Parameter(Name = "BatchNo", DbType = "Int")] System.Nullable<int> batchNo,
            [Parameter(Name = "FilterBy", DbType = "Int")] System.Nullable<int> filterBy,
            [Parameter(Name = "FilterValue", DbType = "VarChar(500)")] string filterValue)
        {
            //int batchNo,
            //DeclarationFilterBy filterBy, string filterValue)
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), machine_No, batchNo, filterBy, filterValue);
            return ((ISingleResult<rsp_GetUndeclaredPartCollectionByMachineResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetUndeclaredPartCollectionByCollectionNo")]
        public ISingleResult<rsp_GetUndeclaredPartCollectionByMachineResult> GetUndeclaredPartCollectionByCollectionNo([Parameter(Name = "CollectionNo", DbType = "Int")] int CollectionNo, 
             [Parameter(Name = "FilterBy", DbType = "Int")] System.Nullable<int> filterBy,
             [Parameter(Name = "FilterValue", DbType = "VarChar(500)")] string filterValue)
        {
            //int batchNo,
            //DeclarationFilterBy filterBy, string filterValue)
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), CollectionNo,  filterBy, filterValue);
            return ((ISingleResult<rsp_GetUndeclaredPartCollectionByMachineResult>)(result.ReturnValue));
        }

       


        [Function(Name = "dbo.usp_Export_History")]
        public int InsertIntoExportHistory([Parameter(Name = "Reference1", DbType = "VarChar(10)")] string reference1, [Parameter(Name = "Reference2", DbType = "VarChar(10)")] string reference2, [Parameter(Name = "Type", DbType = "VarChar(30)")] string type, [Parameter(Name = "Status", DbType = "VarChar(1)")] System.Nullable<char> status)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), reference1, reference2, type, status);
            return ((int)(result.ReturnValue));
        }       
        [Function(Name = "dbo.usp_ClearEventsForMachine")]
        public int UpdateEventDetails([Parameter(Name = "User_Id", DbType = "Int")] System.Nullable<int> userId, 
                                        [Parameter(Name = "Installation_No", DbType = "Int")] System.Nullable<int> installationNo)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), userId, installationNo);
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.usp_InsertCollectionDeclaration")]
        public int InsertCollectionDeclaration(
                    [Parameter(Name = "Collection_Type", DbType = "VarChar(2)")] string collection_Type,
                    [Parameter(Name = "Installation_No", DbType = "Int")] System.Nullable<int> installation_No,
                    [Parameter(Name = "Collection_No", DbType = "Int")] System.Nullable<int> collection_No,
                    [Parameter(Name = "User_ID", DbType = "Int")] System.Nullable<int> user_ID,
                    [Parameter(Name = "Manual", DbType = "Bit")] System.Nullable<bool> manual,
                    [Parameter(DbType = "Bit")] System.Nullable<bool> forceCash,
                    [Parameter(DbType = "Bit")] System.Nullable<bool> forceMeters,
                    [Parameter(Name = "VATRate", DbType = "Float")] System.Nullable<double> vATRate,
                    [Parameter(Name = "HandHeldsActive", DbType = "Bit")] System.Nullable<bool> handHeldsActive,
                    [Parameter(Name = "DeclarationCoinsIn", DbType = "Money")] System.Nullable<decimal> declarationCoinsIn,
                    [Parameter(Name = "DeclarationCoinsOut", DbType = "Money")] System.Nullable<decimal> declarationCoinsOut,
                    [Parameter(Name = "DeclarationCoinDrop", DbType = "Money")] System.Nullable<decimal> declarationCoinDrop,
                    [Parameter(Name = "DeclarationHandPay", DbType = "Money")] System.Nullable<decimal> declarationHandPay,
                    [Parameter(Name = "DeclarationExternalCredit", DbType = "Money")] System.Nullable<decimal> declarationExternalCredit,
                    [Parameter(Name = "DeclarationGamesBet", DbType = "Int")] System.Nullable<int> declarationGamesBet,
                    [Parameter(Name = "DeclarationGamesWon", DbType = "Int")] System.Nullable<int> declarationGamesWon,
                    [Parameter(Name = "DeclarationNotes", DbType = "Money")] System.Nullable<decimal> declarationNotes,
                    [Parameter(Name = "DeclarationCashCashOut", DbType = "Money")] System.Nullable<decimal> declarationCashCashOut,
                    [Parameter(Name = "DeclarationCashTokensOut", DbType = "Money")] System.Nullable<decimal> declarationCashTokensOut,
                    [Parameter(Name = "DeclarationCashTokenRefills", DbType = "Money")] System.Nullable<decimal> declarationCashTokenRefills,
                    [Parameter(Name = "DeclarationCoinBreakDown1p", DbType = "Money")] System.Nullable<decimal> declarationCoinBreakDown1p,
                    [Parameter(Name = "DeclarationCoinBreakDown2p", DbType = "Money")] System.Nullable<decimal> declarationCoinBreakDown2p,
                    [Parameter(Name = "DeclarationCoinBreakDown5p", DbType = "Money")] System.Nullable<decimal> declarationCoinBreakDown5p,
                    [Parameter(Name = "DeclarationCoinBreakDown10p", DbType = "Money")] System.Nullable<decimal> declarationCoinBreakDown10p,
                    [Parameter(Name = "DeclarationCoinBreakDown20p", DbType = "Money")] System.Nullable<decimal> declarationCoinBreakDown20p,
                    [Parameter(Name = "DeclarationCoinBreakDown50p", DbType = "Money")] System.Nullable<decimal> declarationCoinBreakDown50p,
                    [Parameter(Name = "DeclarationCoinBreakDown100p", DbType = "Money")] System.Nullable<decimal> declarationCoinBreakDown100p,
                    [Parameter(Name = "DeclarationCoinBreakDown200p", DbType = "Money")] System.Nullable<decimal> declarationCoinBreakDown200p,
                    [Parameter(Name = "DeclarationCoinBreakDown500p", DbType = "Money")] System.Nullable<decimal> declarationCoinBreakDown500p,
                    [Parameter(Name = "DeclarationCoinBreakDown1000p", DbType = "Money")] System.Nullable<decimal> declarationCoinBreakDown1000p,
                    [Parameter(Name = "DeclarationCoinBreakDown2000p", DbType = "Money")] System.Nullable<decimal> declarationCoinBreakDown2000p,
                    [Parameter(Name = "DeclarationCoinBreakDown5000p", DbType = "Money")] System.Nullable<decimal> declarationCoinBreakDown5000p,
                    [Parameter(Name = "DeclarationCoinBreakDown10000p", DbType = "Money")] System.Nullable<decimal> declarationCoinBreakDown10000p,
                    [Parameter(Name = "DeclarationCoinBreakDown20000p", DbType = "Money")] System.Nullable<decimal> declarationCoinBreakDown20000p,
                    [Parameter(Name = "DeclarationCoinBreakDown50000p", DbType = "Money")] System.Nullable<decimal> declarationCoinBreakDown50000p,
                    [Parameter(Name = "DeclarationCoinBreakDown100000p", DbType = "Money")] System.Nullable<decimal> declarationCoinBreakDown100000p,
                    [Parameter(Name = "DeclarationTicketValue", DbType = "Money")] System.Nullable<decimal> declarationTicketValue,
                    [Parameter(Name = "DeclarationTicketQty", DbType = "Int")] System.Nullable<int> declarationTicketQty,
                    [Parameter(Name = "DeclarationMetersCashIn", DbType = "Money")] System.Nullable<decimal> declarationMetersCashIn,
                    [Parameter(Name = "DeclarationMetersCashOut", DbType = "Money")] System.Nullable<decimal> declarationMetersCashOut,
                    [Parameter(Name = "DeclarationMetersTokensIn", DbType = "Money")] System.Nullable<decimal> declarationMetersTokensIn,
                    [Parameter(Name = "DeclarationMetersTokensOut", DbType = "Money")] System.Nullable<decimal> declarationMetersTokensOut,
                    [Parameter(Name = "DeclarationMetersPrize", DbType = "Money")] System.Nullable<decimal> declarationMetersPrize,
                    [Parameter(Name = "DeclarationMetersJukebox", DbType = "Money")] System.Nullable<decimal> declarationMetersJukebox,
                    [Parameter(Name = "DeclarationMetersTournament", DbType = "Money")] System.Nullable<decimal> declarationMetersTournament,
                    [Parameter(Name = "DeclarationMetersRefills", DbType = "Money")] System.Nullable<decimal> declarationMetersRefills)
        {
            IExecuteResult result = ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), collection_Type, installation_No, collection_No, user_ID, manual, forceCash, forceMeters, vATRate, handHeldsActive, declarationCoinsIn, declarationCoinsOut, declarationCoinDrop, declarationHandPay, declarationExternalCredit, declarationGamesBet, declarationGamesWon, declarationNotes, declarationCashCashOut, declarationCashTokensOut, declarationCashTokenRefills, declarationCoinBreakDown1p, declarationCoinBreakDown2p, declarationCoinBreakDown5p, declarationCoinBreakDown10p, declarationCoinBreakDown20p, declarationCoinBreakDown50p, declarationCoinBreakDown100p, declarationCoinBreakDown200p, declarationCoinBreakDown500p, declarationCoinBreakDown1000p, declarationCoinBreakDown2000p, declarationCoinBreakDown5000p, declarationCoinBreakDown10000p, declarationCoinBreakDown20000p, declarationCoinBreakDown50000p, declarationCoinBreakDown100000p, declarationTicketValue, declarationTicketQty, declarationMetersCashIn, declarationMetersCashOut, declarationMetersTokensIn, declarationMetersTokensOut, declarationMetersPrize, declarationMetersJukebox, declarationMetersTournament, declarationMetersRefills);
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetCollectionWeekData")]
        public ISingleResult<CollectionWeekData> GetCollectionWeekData([Parameter(Name = "Week", DbType = "Int")] System.Nullable<int> Week)
        {
            IExecuteResult result = ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), Week);
            return ((ISingleResult<CollectionWeekData>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetCollectionBatchData")]
        public ISingleResult<CollectionBatchData> GetCollectionBatchData([Parameter(Name = "NumberOfRecords", DbType = "Int")] System.Nullable<int> numberOfRecords)
        {
            IExecuteResult result = ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), numberOfRecords);
            return ((ISingleResult<CollectionBatchData>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetPartCollectionBatchData")]
        public ISingleResult<PartCollectionBatchData> GetPartCollectionBatchData([Parameter(Name = "NumberOfRecords", DbType = "Int")] System.Nullable<int> numberOfRecords)
        {
            IExecuteResult result = ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), numberOfRecords);
            return ((ISingleResult<PartCollectionBatchData>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetDeclaredTicket")]
        public ISingleResult<rsp_GetDeclaredTicketResult> rsp_GetDeclaredTicket([Parameter(Name = "Collection_ID", DbType = "Int")] System.Nullable<int> collection_ID, [Parameter(Name = "Installation_ID", DbType = "Int")] System.Nullable<int> installation_ID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), collection_ID, installation_ID);
            return ((ISingleResult<rsp_GetDeclaredTicketResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.usp_InsertDeclaredTicket")]
        public int usp_InsertDeclaredTicket([Parameter(Name = "Barcode", DbType = "VarChar(50)")] string barcode, [Parameter(Name = "Value", DbType = "Money")] System.Nullable<decimal> value, [Parameter(Name = "User", DbType = "Int")] System.Nullable<int> user, [Parameter(Name = "Printed_Installation_ID", DbType = "Int")] System.Nullable<int> printed_Installation_ID, [Parameter(Name = "Printed_Collection_ID", DbType = "Int")] System.Nullable<int> printed_Collection_ID, [Parameter(Name = "Inserted_Installation_ID", DbType = "Int")] System.Nullable<int> inserted_Installation_ID, [Parameter(Name = "Inserted_Collection_ID", DbType = "Int")] System.Nullable<int> inserted_Collection_ID, [Parameter(DbType = "Int")] ref System.Nullable<int> retDeclaredTicketID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), barcode, value, user, printed_Installation_ID, printed_Collection_ID, inserted_Installation_ID, inserted_Collection_ID, retDeclaredTicketID);
            retDeclaredTicketID = ((System.Nullable<int>)(result.GetParameterValue(7)));
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.dsp_DeleteDeclaredTicket")]
        public int dsp_DeleteDeclaredTicket([Parameter(Name = "Ticket_ID", DbType = "Int")] System.Nullable<int> ticket_ID, [Parameter(Name = "Installation_ID", DbType = "Int")] System.Nullable<int> installation_ID, [Parameter(Name = "Collection_ID", DbType = "Int")] System.Nullable<int> collection_ID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), ticket_ID, installation_ID, collection_ID);
            return ((int)(result.ReturnValue));
        }
        [Function(Name = "dbo.rsp_GetValidationLength")]
        public int rsp_GetValidationLength([Parameter(Name = "@Installation_No", DbType = "Int")] System.Nullable<int> installation_ID, [Parameter(Name = "@Validation_Length", DbType = "Int")] ref System.Nullable<int> validationLength)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), installation_ID, validationLength);
            validationLength = ((System.Nullable<int>)(result.GetParameterValue(1)));
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_IsPaidTicket")]
        public int rsp_IsPaidTicket([Parameter(Name = "@BarCode", DbType = "VarChar(40)")] string barcode, [Parameter(Name = "@Installation_No", DbType = "Int")] System.Nullable<int> installation_ID, [Parameter(Name = "@Count", DbType = "Int")] ref System.Nullable<int> count, [Parameter(Name = "@Amt", DbType = "Int")] ref System.Nullable<int> Amt)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())),barcode, installation_ID, count,Amt);
            count = ((System.Nullable<int>)(result.GetParameterValue(2)));
            Amt = ((System.Nullable<int>)(result.GetParameterValue(3)));
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.usp_UpdateVoucherCollection")]
        public int usp_UpdateVoucherCollection([Parameter(Name = "BarCode", DbType = "VarChar(40)")] string barCode, [Parameter(Name = "Value", DbType = "VarChar(5)")] string value)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), barCode, value);
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetCashCounterCollection")]
        public ISingleResult<CashCounterCollectionResult> GetCashCounterCollection([Parameter(Name = "Batch_No", DbType = "Int")] System.Nullable<int> batch_No)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), batch_No);
            return ((ISingleResult<CashCounterCollectionResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.fnIsEventUnCleared", IsComposable = true)]
        public System.Nullable<bool> fnIsEventUnCleared([Parameter(Name = "Installation_No", DbType = "Int")] System.Nullable<int> installation_No)
        {
            return ((System.Nullable<bool>)(this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), installation_No).ReturnValue));
        }

        [Function(Name = "dbo.IsEventUnCleared", IsComposable = true)]
        public System.Nullable<bool> IsEventUnCleared([Parameter(Name = "InstallationNos", DbType = "VARCHAR(MAX)")] System.String installationNos)
        {
            return ((System.Nullable<bool>)(this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), installationNos).ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetMachineDetailsForDrop")]
        public ISingleResult<GetMachineDetailsForDropResult> GetHandPayPlayCreditStatus([Parameter(Name = "InstallNo", DbType = "Int")] System.Nullable<int> installNo)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), installNo);
            return ((ISingleResult<GetMachineDetailsForDropResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetStatusForDrop")]
        public ISingleResult<CreditStatus> GetHandPayPlayCreditStatus([Parameter(Name = "InstallNos", DbType = "VARCHAR(MAX)")] System.String installNos)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), installNos);
            return ((ISingleResult<CreditStatus>)(result.ReturnValue));
        }

        [Function(Name = "dbo.fnHasUndeclaredCollection", IsComposable = true)]
        public System.Nullable<bool> HasUndeclaredCollection([Parameter(DbType = "Int")] System.Nullable<int> installation_No)
        {
            return ((System.Nullable<bool>)(this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), installation_No).ReturnValue));
        }

        [Function(Name = "dbo.IsAuthorized", IsComposable = true)]
        public System.Nullable<bool> IsAuthorized([Parameter(DbType = "Int")] System.Nullable<int> USERID, [Parameter(DbType = "VARCHAR(200)")] string ObjectName)
        {
            return ((System.Nullable<bool>)(this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), USERID, ObjectName).ReturnValue));
        }

        [Function(Name = "dbo.IsMachinesNotDisabled", IsComposable = true)]
        public string IsMachinesNotDisabled([Parameter(DbType = "VARCHAR(MAX)")] string installationNos)
        {
            return ((string)(this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), installationNos).ReturnValue));
        }

        [Function(Name = "dbo.IsStackerEventNotReceived", IsComposable = true)]
        public string IsStackerEventNotReceived([Parameter(DbType = "VARCHAR(MAX)")] string installationNos)
        {
            return ((string)(this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), installationNos).ReturnValue));
        }

        [Function(Name = "dbo.GetEftIn", IsComposable = true)]
        public System.Nullable<float> GetEftIn([Parameter(DbType = "Int")] System.Nullable<int> collection_No)
        {
            return ((System.Nullable<float>)(this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), collection_No).ReturnValue));
        }

        [Function(Name = "dbo.GetEftOut", IsComposable = true)]
        public System.Nullable<float> GetEftOut([Parameter(DbType = "Int")] System.Nullable<int> collection_No)
        {
            return ((System.Nullable<float>)(this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), collection_No).ReturnValue));
        }

        [Function(Name = "dbo.usp_AcceptDeclarationforAutoCollection")]
        public int AcceptDeclarationforAutoCollection([Parameter(Name = "Batch_No", DbType = "Int")] System.Nullable<int> batch_No, [Parameter(Name = "ErrorMsg", DbType = "VarChar(500)")] ref string errorMsg)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), batch_No, errorMsg);
            errorMsg = ((string)(result.GetParameterValue(1)));
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.fn_getTicketsInValueByBatch", IsComposable = true)]
        public System.Nullable<float> GetVoucherInValueByCollectionNo([Parameter(Name = "collection_no", DbType = "Int")] System.Nullable<int> collection_no)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), collection_no);
            return ((System.Nullable<float>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_IsNoteCounterVisible")]
        public int IsNoteCounterVisible([Parameter(Name = "IsVisible", DbType = "BIT")] ref bool IsVisible)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), IsVisible);
            IsVisible = ((bool)(result.GetParameterValue(0)));
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.usp_DiscardExceptionVoucherChanges")]
        public int DiscardExceptionVoucherChanges([Parameter(Name = "Collection_No", DbType = "Int")] int collection_No, 
            [Parameter(Name = "Installation_No", DbType = "Int")] int installation_No,
            [Parameter(Name = "CommitChanges", DbType = "BIT")] bool commitChanges)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), collection_No, installation_No, commitChanges);
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetDropAlertData")]
        public ISingleResult<GetDropAlertDataResult> GetDropAlertData([Parameter(Name = "BatchNo", DbType = "Int")] System.Nullable<int> batchNo)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), batchNo);
            return ((ISingleResult<GetDropAlertDataResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.usp_STM_Export_History")]
        public int Insert_STM_Export_History([Parameter(Name = "Type", DbType = "VarChar(50)")] string type, [Parameter(Name = "ClientID", DbType = "Int")] System.Nullable<int> clientID, [Parameter(Name = "Site_Code", DbType = "VarChar(50)")] string site_Code, [Parameter(Name = "XmlMessage", DbType = "Xml")] System.Xml.Linq.XElement xmlMessage)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), type, clientID, site_Code, xmlMessage);
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.usp_ResetStackerLevel")]
        public int ResetStackerLevel([Parameter(Name = "InstallationNos", DbType = "VarChar(MAX)")] string installationNos)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), installationNos);
            return ((int)(result.ReturnValue));
        }
        [Function(Name = "dbo.usp_Part_SaveDeclaration")]
        public int SavePartDeclaration([Parameter(Name = "Machine", DbType = "VarChar(150)")] string Machine, [Parameter(Name = "User_ID", DbType = "Int")] System.Nullable<int> User_ID, [Parameter(Name = "Part_Collection_No", DbType = "Int")] System.Nullable<int> Part_Collection_No)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), Machine, User_ID, Part_Collection_No);
            return ((int)(result.ReturnValue));
        }


        [Function(Name = "dbo.rsp_CheckPreviousDeclarationStatus")]
        public int CheckPreviousDeclarationStatus([Parameter(Name = "PartCollectionId", DbType = "Int")] System.Nullable<int> PartCollectionId, [Parameter(Name = "CollectionId", DbType = "Int")] System.Nullable<int> CollectionId, [Parameter(Name = "InstallationNo", DbType = "Int")] System.Nullable<int> InstallationNo)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), PartCollectionId, CollectionId, InstallationNo);
            return ((int)(result.ReturnValue));
        }

        


      


        public partial class CheckPreviousDeclarationStatusResult
        {

            private System.Nullable<int> _returnValue;

            public CheckPreviousDeclarationStatusResult()
            {
            }

            [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "", Storage = "_returnValue", DbType = "Int")]
            public System.Nullable<int> ReturnValue
            {
                get
                {
                    return this._returnValue;
                }
                set
                {
                    if ((this._returnValue != value))
                    {
                        this._returnValue = value;
                    }
                }
            }
        }

        public Table<Installation> Installations
        {
            get
            {
                return GetTable<Installation>();
            }
        }

        public Table<Collection> Collections
        {
            get
            {
                return GetTable<Collection>();
            }
        }

        public Table<Event> Events
        {
            get
            {
                return GetTable<Event>();
            }
        }

        public Table<Part_Collection> PartCollections
        {
            get
            {
                return GetTable<Part_Collection>();
            }
        }

        public partial class GetMachineDetailsForDropResult
        {

            private System.Nullable<int> _Installation_No;

            private int _inplay;

            private int _IsHandPayUnProcessed;

            private int _isCardedPlay;

            public GetMachineDetailsForDropResult()
            {
            }

            [Column(Storage = "_Installation_No", DbType = "Int")]
            public System.Nullable<int> Installation_No
            {
                get
                {
                    return this._Installation_No;
                }
                set
                {
                    if ((this._Installation_No != value))
                    {
                        this._Installation_No = value;
                    }
                }
            }

            [Column(Storage = "_inplay", DbType = "Int NOT NULL")]
            public int inplay
            {
                get
                {
                    return this._inplay;
                }
                set
                {
                    if ((this._inplay != value))
                    {
                        this._inplay = value;
                    }
                }
            }

            [Column(Storage = "_IsHandPayUnProcessed", DbType = "Int NOT NULL")]
            public int IsHandPayUnProcessed
            {
                get
                {
                    return this._IsHandPayUnProcessed;
                }
                set
                {
                    if ((this._IsHandPayUnProcessed != value))
                    {
                        this._IsHandPayUnProcessed = value;
                    }
                }
            }

            [Column(Storage = "_isCardedPlay", DbType = "Int NOT NULL")]
            public int isCardedPlay
            {
                get
                {
                    return this._isCardedPlay;
                }
                set
                {
                    if ((this._isCardedPlay != value))
                    {
                        this._isCardedPlay = value;
                    }
                }
            }
        }

       
    }

    

    [Flags]
    public enum CollectionType
    {
        FullCollection,
        PartCollection,
        DefloatCollection
    }
}
