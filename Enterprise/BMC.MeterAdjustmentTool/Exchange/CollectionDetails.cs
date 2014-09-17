using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using BMC.Common.ExceptionManagement;

namespace BMC.MeterAdjustmentTool.Exchange
{
    public abstract class CollectionDetailsBaseData : InstallationExecutorBase
    {
        protected CollectionDetailsBaseData(rsp_GetSiteDetailsResult site)
            : base(site)
        {
            //_resourceManager = CollectionsSchema.ResourceManager;
        }

        public override int VarianceThreshold
        {
            get { return MeterGlobals.CollectionVarianceThreshold; }
        }
    }

    public class CollectionDetailsBatchData : CollectionDetailsBaseData
    {
        public CollectionDetailsBatchData(rsp_GetSiteDetailsResult site)
            : base(site) { }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public override DataSet ExecQuery(ExchangeDataContext context)
        {
            DataSet result = default(DataSet);

            try
            {
                if (this.SiteWebUrl.Contains(MeterGlobals.ExchangeWebUrl114))
                {
                    string spName = "GetBatchDetails";
                    SqlParameter[] sSqlParams = new SqlParameter[3];
                    sSqlParams[0] = new SqlParameter("@StartDate", this.StartDate.GetDayStart());//.ToString_ddMMMyyyy());
                    sSqlParams[1] = new SqlParameter("@EndDate", this.EndDate.GetDayEnd());//.ToString_ddMMMyyyy());
                    sSqlParams[2] = new SqlParameter("@Threshold", this.VarianceThreshold);
                    result = context.ExecuteQueryAndGetDataSet(spName, sSqlParams, new string[] { MeterGlobals.ORIGINAL_TABLE });
                    if (result != null &&
                        result.Tables.Count > 0)
                    {
                        result.Tables[0].TableName = MeterGlobals.TRANSLATED_TABLE;
                    }
                    return result;
                }
                else
                {
                    string spName = "GetBatchDetails";
                    SqlParameter[] sSqlParams = new SqlParameter[4];
                    sSqlParams[0] = new SqlParameter("@StartDate", this.StartDate.GetDayStart());//.ToString_ddMMMyyyy());
                    sSqlParams[1] = new SqlParameter("@EndDate", this.EndDate.GetDayEnd());//.ToString_ddMMMyyyy());
                    sSqlParams[2] = new SqlParameter("@Threshold", this.VarianceThreshold);
                    if (this.InstallationID.IsValid())
                        sSqlParams[3] = new SqlParameter("@Installation_ID", this.InstallationID.Value);
                    else
                        sSqlParams[3] = new SqlParameter("@Installation_ID", DBNull.Value);
                    result = context.ExecuteQueryAndGetDataSet(spName, sSqlParams, new string[] { MeterGlobals.ORIGINAL_TABLE });
                    if (result != null &&
                        result.Tables.Count > 0)
                    {
                        result.Tables[0].TableName = MeterGlobals.TRANSLATED_TABLE;
                    }
                    return result;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            return result;
        }
    }

    public class CollectionDetailsSuspectedData : CollectionDetailsBaseData
    {
        public CollectionDetailsSuspectedData(rsp_GetSiteDetailsResult site)
            : base(site) { }

        public int BatchID { get; set; }

        public override DataSet ExecQuery(ExchangeDataContext context)
        {
            DataSet result = default(DataSet);

            try
            {
                if (this.SiteWebUrl.Contains(MeterGlobals.ExchangeWebUrl114))
                {
                    string spName = "GetRiskyCollections";
                    SqlParameter[] sSqlParams = new SqlParameter[2];
                    sSqlParams[0] = new SqlParameter("@BatchID", this.BatchID);
                    sSqlParams[1] = new SqlParameter("@Threshold", this.VarianceThreshold);
                    result = context.ExecuteQueryAndGetDataSet(spName, sSqlParams, new string[] { MeterGlobals.ORIGINAL_TABLE });
                    //context.TranslateDataSet(result, _resourceManager);
                    return result;
                }
                else
                {
                    string spName = "GetRiskyCollections";
                    SqlParameter[] sSqlParams = new SqlParameter[3];
                    sSqlParams[0] = new SqlParameter("@BatchID", this.BatchID);
                    sSqlParams[1] = new SqlParameter("@Threshold", this.VarianceThreshold);
                    if (this.InstallationID.IsValid())
                        sSqlParams[2] = new SqlParameter("@Installation_ID", this.InstallationID.Value);
                    else
                        sSqlParams[2] = new SqlParameter("@Installation_ID", DBNull.Value);
                    result = context.ExecuteQueryAndGetDataSet(spName, sSqlParams, new string[] { MeterGlobals.ORIGINAL_TABLE });
                    //context.TranslateDataSet(result, _resourceManager);
                    return result;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            return result;
        }
    }

    public class CollectionDetailsSelectedItemData : CollectionDetailsBaseData
    {
        public CollectionDetailsSelectedItemData(rsp_GetSiteDetailsResult site)
            : base(site) { }

        public int CollectionID { get; set; }

        public override DataSet ExecQuery(ExchangeDataContext context)
        {
            DataSet result = default(DataSet);

            try
            {   
                string spName = "GetCollectionDetails";
                SqlParameter[] sSqlParams = new SqlParameter[1];
                sSqlParams[0] = new SqlParameter("@CollectionID", this.CollectionID);
                result = context.ExecuteQueryAndGetDataSet(spName, sSqlParams, new string[] { MeterGlobals.ORIGINAL_TABLE, MeterGlobals.RAMRESET_TABLE });
                //context.TranslateDataSetForEdit(result, _resourceManager);
                return result;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            return result;
        }
    }

    public class CollectionDetailsUpdateData : InstallationUpdateExecutorBase
    {
        public CollectionDetailsUpdateData(rsp_GetSiteDetailsResult site)
            : base(site) { }

        public int? BatchID { get; set; }

        public int? CollectionID { get; set; }

        public override bool ExecQuery(ExchangeDataContext context)
        {
            bool result = false;

            try
            {   
                result = (context.UpdateCollection(this.CollectionID, this.BatchID,
                    this.UserID, this.UserName,
                    this.UpdateSet, this.CurrentSet) > 0);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            return result;
        }
    }

    public class CollectionDetails : ExchangeDataInterface
    {
        public CollectionDetails(string connectionString)
            : base(connectionString) { }

        protected override bool CanExecute(IParameterInfo parameterInfo)
        {
            Type typeOfT = parameterInfo.GetType();
            return (typeOfT == typeof(CollectionDetailsBatchData) ||
                    typeOfT == typeof(CollectionDetailsSuspectedData) ||
                    typeOfT == typeof(CollectionDetailsSelectedItemData) ||
                    typeOfT == typeof(CollectionDetailsUpdateData));
        }
    }
}
