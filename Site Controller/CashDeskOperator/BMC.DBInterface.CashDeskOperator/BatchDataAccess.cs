using System;
using System.Data;
using BMC.Common.LogManagement;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Reflection;
using BMC.Transport;
using BMC.DataAccess;

namespace BMC.DBInterface.CashDeskOperator
{
    public class BatchDataAccess: DataContext
    {
        private static readonly MappingSource MappingSource = new AttributeMappingSource();
       
        public BatchDataAccess(string connection) : 
				base(connection, MappingSource)
		{           
            Connection.ConnectionString = CommonDataAccess.ExchangeConnectionString;
            this.CommandTimeout = SqlHelper.LoadCommandTimeout();
        }
		
		public BatchDataAccess(IDbConnection connection) : 
				base(connection, MappingSource)
		{	}
		
		public BatchDataAccess(string connection, MappingSource mappingSource) : 
				base(connection, mappingSource)
		{}

        public BatchDataAccess(IDbConnection connection, MappingSource mappingSource) : 
				base(connection, mappingSource)
		{	}

        public IMultipleResults GetBatchBreakdown(int collectionID, int installationNo, int batchID)
        {
            try
            {
                return GetBatchHistoryBreakDown(collectionID, installationNo, batchID);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("GetBatchBreakdown::"+ex.Message, LogManager.enumLogLevel.Error);
            }
            return null;
        }

        public IMultipleResults GetBatchBreakdownSummary(int collectionID, int installationNo, int batchID)
        {
            try
            {
                return GetBatchHistoryBreakDownSummary(collectionID, installationNo, batchID);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("GetBatchBreakdownSummary::" + ex.Message, LogManager.enumLogLevel.Error);
            }
            return null;
        }



        [Function(Name = "dbo.rsp_GetBatchHistoryBreakDown")]
        [ResultType(typeof(CollectionView))]
        [ResultType(typeof(CollectionRecord))]
        [ResultType(typeof(TreasuryUser))]
        [ResultType(typeof(InstallationRecord))]
        [ResultType(typeof(PartCollectionUser))]
        [ResultType(typeof(TreasuryByShortpay))]
        [ResultType(typeof(DoorEventRecord))]
        [ResultType(typeof(FaultEventRecord))]
        [ResultType(typeof(PowerEventRecord))]
        public IMultipleResults GetBatchHistoryBreakDown([Parameter(Name = "@collection_ID", DbType = "Int")] int? collectionID, [Parameter(Name = "Installation_ID", DbType = "Int")] int? installationID, [Parameter(Name = "Batch_ID", DbType = "Int")] int? batchID)
        {
            IExecuteResult result = ExecuteMethodCall(this, ((MethodInfo)(MethodBase.GetCurrentMethod())), collectionID, installationID, batchID);
            return ((IMultipleResults)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetTreasuryDetails")]
        public ISingleResult<TreasuryUser> GetTreasuryDetails([Parameter(Name = "@collection_ID", DbType = "Int")] int? collectionID)
        {
            IExecuteResult result = ExecuteMethodCall(this, ((MethodInfo)(MethodBase.GetCurrentMethod())), collectionID);
            return ((ISingleResult<TreasuryUser>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetBatchHistoryBreakDownSummary")]
        [ResultType(typeof(CollectionView))]
        [ResultType(typeof(CollectionbatchDataView))]
        [ResultType(typeof(BatchDataView))]
        [ResultType(typeof(DoorEventRecord))]
        [ResultType(typeof(FaultEventRecord))]
        [ResultType(typeof(PowerEventRecord))]
        public IMultipleResults GetBatchHistoryBreakDownSummary([Parameter(Name = "@collection_ID", DbType = "Int")] int? collectionID, [Parameter(Name = "Installation_ID", DbType = "Int")] int? installationID, [Parameter(Name = "Batch_ID", DbType = "Int")] int? batchID)
        {
            IExecuteResult result = ExecuteMethodCall(this, ((MethodInfo)(MethodBase.GetCurrentMethod())), collectionID, installationID, batchID);
            return ((IMultipleResults)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_getWeekBreakdownSummary")]
        [ResultType(typeof(VW_S_CollectionWeekData))]
        [ResultType(typeof(CollectionView))]
        public IMultipleResults GetWeekBreakdownSummary([Parameter(Name ="@Week_ID", DbType = "Int")] int? weekID)
        {
            IExecuteResult result = ExecuteMethodCall(this, ((MethodInfo)(MethodBase.GetCurrentMethod())), weekID);
            return ((IMultipleResults)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_AssetVarianceHistory")]
        public ISingleResult<rsp_AssetVarianceHistoryResult> AssetVarianceHistory([Parameter(Name = "@installation_id", DbType = "Int")] int? installationID, [Parameter(Name = "@rows", DbType = "Int")] int? rows)
        {
            IExecuteResult result = ExecuteMethodCall(this, ((MethodInfo)(MethodBase.GetCurrentMethod())), installationID, rows);
            return ((ISingleResult<rsp_AssetVarianceHistoryResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetAFTAssets")]
        public ISingleResult<AftAssets> GetAFTAssets()
        {
            IExecuteResult result = ExecuteMethodCall(this, ((MethodInfo)(MethodBase.GetCurrentMethod())));
            return ((ISingleResult<AftAssets>)(result.ReturnValue));
        }

        [Function(Name = "dbo.usp_UpdateAftStatus")]
        public int UpdateAftStatus([Parameter(Name = "InstallationNo", DbType = "INT")] int InstallationNo, [Parameter(Name = "Status", DbType = "INT")] int Status)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), InstallationNo, Status);
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.fnGetDropType", IsComposable = true)]
        public string GetDropType([Parameter(DbType = "Int")] System.Nullable<int> collection_No)
        {
            return Convert.ToString(this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), collection_No).ReturnValue);
        }

        //[Function(Name = "dbo.rsp_GetBatchBreakdownhistory")]
        //public ISingleResult<CollectionView> GetBatchBreakdownhistory([Parameter(Name = "Batch_ID", DbType = "Int")] int? batchID, 
        //    [Parameter(Name = "Week_ID", DbType = "Int")] int? WeekID)
        //{
        //    IExecuteResult result = ExecuteMethodCall(this, ((MethodInfo)(MethodBase.GetCurrentMethod())), batchID, WeekID);
        //    return ((ISingleResult<CollectionView>)(result.ReturnValue));
        //}

        [Function(Name = "dbo.rsp_GetBatchBreakdownhistory")]
        public ISingleResult<CollectionView> GetBatchBreakdownhistory([Parameter(Name = "Batch_ID", DbType = "Int")] System.Nullable<int> batch_ID, [Parameter(Name = "Week_ID", DbType = "Int")] System.Nullable<int> week_ID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), batch_ID, week_ID);
            return ((ISingleResult<CollectionView>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetBatchBreakdownTotal")]
        public ISingleResult<CollectionbatchDataView> GetBatchBreakdownTotal([Parameter(Name = "Batch_ID", DbType = "Int")] int? batchID                                                                                )
        {
            IExecuteResult result = ExecuteMethodCall(this, ((MethodInfo)(MethodBase.GetCurrentMethod())), batchID);
            return ((ISingleResult<CollectionbatchDataView>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetBatchBreakdownOthers")]
        public ISingleResult<BatchDataView> GetBatchBreakdownOthers([Parameter(Name = "Batch_ID", DbType = "Int")] int? batchID)
        {
            IExecuteResult result = ExecuteMethodCall(this, ((MethodInfo)(MethodBase.GetCurrentMethod())), batchID);
            return ((ISingleResult<BatchDataView>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetDoorEventData")]
        public ISingleResult<DoorEventRecord> GetDoorEventData([Parameter(Name = "@collection_ID", DbType = "Int")] int? collectionID, [Parameter(Name = "Installation_ID", DbType = "Int")] int? installationID, [Parameter(Name = "Top", DbType = "Int")] int? Top)
        {
            IExecuteResult result = ExecuteMethodCall(this, ((MethodInfo)(MethodBase.GetCurrentMethod())), collectionID, installationID, Top);
            return ((ISingleResult<DoorEventRecord>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetFaultEventData")]
        public ISingleResult<FaultEventRecord> GetFaultEventData([Parameter(Name = "@collection_ID", DbType = "Int")] int? collectionID, [Parameter(Name = "Installation_ID", DbType = "Int")] int? installationID, [Parameter(Name = "Top", DbType = "Int")] int? Top)
        {
            IExecuteResult result = ExecuteMethodCall(this, ((MethodInfo)(MethodBase.GetCurrentMethod())), collectionID, installationID, Top);
            return ((ISingleResult<FaultEventRecord>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetPowerEventData")]
        public ISingleResult<PowerEventRecord> GetPowerEventData([Parameter(Name = "@collection_ID", DbType = "Int")] int? collectionID, [Parameter(Name = "Installation_ID", DbType = "Int")] int? installationID, [Parameter(Name = "Top", DbType = "Int")] int? Top)
        {
            IExecuteResult result = ExecuteMethodCall(this, ((MethodInfo)(MethodBase.GetCurrentMethod())), collectionID, installationID, Top);
            return ((ISingleResult<PowerEventRecord>)(result.ReturnValue));
        }


        [Function(Name = "dbo.rsp_GetWeekBreakDownHistory")]
        public ISingleResult<CollectionView> GetWeekBreakDownHistory([Parameter(Name = "@Week_ID", DbType = "Int")] int? weekID)
        {
            IExecuteResult result = ExecuteMethodCall(this, ((MethodInfo)(MethodBase.GetCurrentMethod())), weekID);
            return ((ISingleResult<CollectionView>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetWeekBreakDownTotal")]
        public ISingleResult<VW_S_CollectionWeekData> GetWeekBreakDownTotal([Parameter(Name = "@Week_ID", DbType = "Int")] int? weekID)
        {
            IExecuteResult result = ExecuteMethodCall(this, ((MethodInfo)(MethodBase.GetCurrentMethod())), weekID);
            return ((ISingleResult<VW_S_CollectionWeekData>)(result.ReturnValue));
        }
    }
}
