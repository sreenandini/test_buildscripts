using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using BMC.Common.ExceptionManagement;

namespace BMC.MeterAdjustmentTool.Exchange
{
    public abstract class DailyReadBaseData : InstallationExecutorBase
    {
        protected DailyReadBaseData(rsp_GetSiteDetailsResult site)
            : base(site)
        {
           // _resourceManager = DailySchema.ResourceManager;
        }

        public override int VarianceThreshold
        {
            get { return MeterGlobals.ReadVarianceThreshold; }
        }
    }

    public class DailyReadSuspectedData : DailyReadBaseData
    {
        public DailyReadSuspectedData(rsp_GetSiteDetailsResult site)
            : base(site) { }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public override DataSet ExecQuery(ExchangeDataContext context)
        {
            DataSet result = default(DataSet);

            try
            {
                string spName = "pGETSuspectedDailyReads";
                SqlParameter[] sSqlParams = new SqlParameter[4];
                if (this.InstallationID.IsValid())
                    sSqlParams[0] = new SqlParameter("@Installation_ID", this.InstallationID.Value);
                else
                    sSqlParams[0] = new SqlParameter("@Installation_ID", DBNull.Value);
                sSqlParams[1] = new SqlParameter("@PreviousCollDate", this.StartDate.ToString_ddMMMyyyy());
                sSqlParams[2] = new SqlParameter("@CollectionDate", this.EndDate.ToString_ddMMMyyyy());
                sSqlParams[3] = new SqlParameter("@Threshold", this.VarianceThreshold);
                result = context.ExecuteQueryAndGetDataSet(spName, sSqlParams, new string[] { MeterGlobals.ORIGINAL_TABLE });
                //context.TranslateDataSet(result, _resourceManager);
                return result;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            return result;
        }
    }

    public class DailyReadSelectedItemData : DailyReadBaseData
    {
        public DailyReadSelectedItemData(rsp_GetSiteDetailsResult site)
            : base(site) { }

        public int ReadID { get; set; }

        public override DataSet ExecQuery(ExchangeDataContext context)
        {
            DataSet result = default(DataSet);

            try
            {
                string spName = "GetReadDetails";
                SqlParameter[] sSqlParams = new SqlParameter[1];
                sSqlParams[0] = new SqlParameter("@ReadID", this.ReadID);
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

    public class DailyReadUpdateData : InstallationUpdateExecutorBase
    {
        public DailyReadUpdateData(rsp_GetSiteDetailsResult site)
            : base(site) { }

        public int ReadID { get; set; }

        public int? BatchID { get; set; }

        public int? CollectionID { get; set; }

        public override bool ExecQuery(ExchangeDataContext context)
        {
            bool result = false;

            try
            {
                result = (context.UpdateDailyRead(this.ReadID, this.BatchID,
                    this.UserID, this.UserName,
                    this.UpdateSet, this.CurrentSet, this.CollectionID) > 0);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            return result;
        }
    }

    public class DailyRead : ExchangeDataInterface
    {
        public DailyRead(string connectionString)
            : base(connectionString) { }

        protected override bool CanExecute(IParameterInfo parameterInfo)
        {
            Type typeOfT = parameterInfo.GetType();
            return (typeOfT == typeof(DailyReadSuspectedData) ||
                    typeOfT == typeof(DailyReadSelectedItemData) ||
                    typeOfT == typeof(DailyReadUpdateData));
        }
    }
}
