using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Resources;
using System.IO;
using BMC.Common.ExceptionManagement;
using BMC.MeterAdjustmentTool;

namespace BMC.MeterAdjustmentTool.Exchange
{
    public abstract class MGMDDeltaBaseData : InstallationExecutorBase
    {
        protected MGMDDeltaBaseData(rsp_GetSiteDetailsResult site)
            : base(site)
        {
           // _resourceManager = MGMDDeltaSchema.ResourceManager;
        }

        public override int VarianceThreshold
        {
            get { return MeterGlobals.MGMDVarianceThreshold; }
        }
    }

    public class MGMDDeltaSuspectedData : MGMDDeltaBaseData
    {
        public MGMDDeltaSuspectedData(rsp_GetSiteDetailsResult site)
            : base(site) { }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public override DataSet ExecQuery(ExchangeDataContext context)
        {
            DataSet result = default(DataSet);

            try
            {
                string spName = "rsp_GetSuspectedMGMDDeltas";
                SqlParameter[] sSqlParams = new SqlParameter[4];
                if (this.InstallationID.IsValid())
                    sSqlParams[0] = new SqlParameter("@Installation_No", this.InstallationID.Value);
                else
                    sSqlParams[0] = new SqlParameter("@Installation_No", DBNull.Value);
                sSqlParams[1] = new SqlParameter("@StartDate", this.StartDate.GetDayStart());
                sSqlParams[2] = new SqlParameter("@EndDate", this.EndDate.GetDayEnd());
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

    public class MGMDDeltaSelectedItemData : MGMDDeltaBaseData
    {
        public MGMDDeltaSelectedItemData(rsp_GetSiteDetailsResult site)
            : base(site) { }

        public int SessionID { get; set; }

        public override DataSet ExecQuery(ExchangeDataContext context)
        {
            DataSet result = default(DataSet);

            try
            {
                string spName = "rsp_GetMGMDDeltaDetails";
                SqlParameter[] sSqlParams = new SqlParameter[1];
                sSqlParams[0] = new SqlParameter("@SessionID", this.SessionID);
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

    public class MGMDDeltaUpdateData : InstallationUpdateExecutorBase
    {
        public MGMDDeltaUpdateData(rsp_GetSiteDetailsResult site)
            : base(site) { }

        public int SessionID { get; set; }

        public override bool ExecQuery(ExchangeDataContext context)
        {
            bool result = false;

            try
            {
                result = (context.FuncUpdateMGMDDelta(this.SessionID,
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

    public class MGMDDelta : ExchangeDataInterface
    {
        public MGMDDelta(string connectionString)
            : base(connectionString) { }

        protected override bool CanExecute(IParameterInfo parameterInfo)
        {
            Type typeOfT = parameterInfo.GetType();
            return (typeOfT == typeof(MGMDDeltaSuspectedData) ||
                    typeOfT == typeof(MGMDDeltaSelectedItemData) ||
                    typeOfT == typeof(MGMDDeltaUpdateData));
        }
    }
}
