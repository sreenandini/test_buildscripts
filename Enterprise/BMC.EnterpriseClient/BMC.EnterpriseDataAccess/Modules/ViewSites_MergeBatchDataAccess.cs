using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;
using System.Reflection;
using System.Data.Linq;

namespace BMC.EnterpriseDataAccess
{
    public partial class EnterpriseDataContext
    {

        [Function(Name = "dbo.FnGetBatch", IsComposable = true)]
        public System.Nullable<int> FnGetBatch([Parameter(Name = "BatchID", DbType = "Int")] System.Nullable<int> batchID, [Parameter(Name = "Siteid", DbType = "Int")] System.Nullable<int> siteid)
        {
            return ((System.Nullable<int>)(this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), batchID, siteid).ReturnValue));
        }

        [Function(Name = "dbo.FnGetDeletedBatch", IsComposable = true)]
        public string FnGetDeletedBatch([Parameter(Name = "BatchID", DbType = "Int")] System.Nullable<int> batchID)
        {
            return ((string)(this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), batchID).ReturnValue));
        }

        [Function(Name = "dbo.usp_Create_MergeBatch")]
        public int usp_Create_MergeBatch([Parameter(Name = "DeletedBatchNo", DbType = "Int")] System.Nullable<int> deletedBatchNo, [Parameter(Name = "MergedBatchNo", DbType = "Int")] System.Nullable<int> mergedBatchNo)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), deletedBatchNo, mergedBatchNo);
            return ((int)(result.ReturnValue));
        }
        [Function(Name = "dbo.usp_Merge_Batch")]
        public int usp_Merge_Batch([Parameter(Name = "DeletedBatchNo", DbType = "Int")] System.Nullable<int> deletedBatchNo, [Parameter(Name = "MergedBatchNo", DbType = "Int")] System.Nullable<int> mergedBatchNo)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), deletedBatchNo, mergedBatchNo);
            return ((int)(result.ReturnValue));
        }
        [Function(Name = "dbo.usp_DeMerge_Batch")]
        public int usp_DeMerge_Batch([Parameter(Name = "DeletedBatchNo", DbType = "Int")] System.Nullable<int> deletedBatchNo, [Parameter(Name = "MergedBatchNo", DbType = "Int")] System.Nullable<int> mergedBatchNo)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), deletedBatchNo, mergedBatchNo);
            return ((int)(result.ReturnValue));
        }
        [Function(Name = "dbo.FnGetMergeBatchList", IsComposable = true)]
        public string FnGetMergeBatchList([Parameter(Name = "MinBatchID", DbType = "Int")] System.Nullable<int> minBatchID, [Parameter(Name = "Siteid", DbType = "Int")] System.Nullable<int> siteid, [Parameter(Name = "Exclude", DbType = "Bit")] System.Nullable<bool> exclude)
        {
            return ((string)(this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), minBatchID, siteid, exclude).ReturnValue));
        }

        [Function(Name = "dbo.usp_Delete_Batch")]
        public int usp_Delete_Batch([Parameter(Name = "DeletedBatchNo", DbType = "Int")] System.Nullable<int> deletedBatchNo)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), deletedBatchNo);
            return ((int)(result.ReturnValue));
        }
        [Function(Name = "dbo.esp_Calculate_Merge_Batch_Negative_Net")]
        public int esp_Calculate_Merge_Batch_Negative_Net([Parameter(Name = "PreviousBatchNo", DbType = "Int")] System.Nullable<int> previousBatchNo, [Parameter(Name = "CurrentBatchNo", DbType = "Int")] System.Nullable<int> currentBatchNo, [Parameter(Name = "RETAILER_SHARE", DbType = "Float")] System.Nullable<double> rETAILER_SHARE)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), previousBatchNo, currentBatchNo, rETAILER_SHARE);
            return ((int)(result.ReturnValue));
        }



    }
}
