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
    public abstract class HourlyVTPBaseData : InstallationExecutorBase
    {
        protected HourlyVTPBaseData(rsp_GetSiteDetailsResult site)
            : base(site)
        {
            //_resourceManager = HourlySchema.ResourceManager;
        }

        public override int VarianceThreshold
        {
            get { return MeterGlobals.HourlyVarianceThreshold; }
        }
    }

    public class HourlyVTPSuspectedData : HourlyVTPBaseData
    {
        public HourlyVTPSuspectedData(rsp_GetSiteDetailsResult site)
            : base(site) { }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public override DataSet ExecQuery(ExchangeDataContext context)
        {
            DataSet result = default(DataSet);

            try
            {
                string spName = "pGETSuspectedHourlyReads";
                SqlParameter[] sSqlParams = new SqlParameter[4];
                if (this.InstallationID.IsValid())
                    sSqlParams[0] = new SqlParameter("@Installation_ID", this.InstallationID.Value);
                else
                    sSqlParams[0] = new SqlParameter("@Installation_ID", DBNull.Value);
                sSqlParams[1] = new SqlParameter("@CollectionDate", this.EndDate.ToString_ddMMMyyyy());
                sSqlParams[2] = new SqlParameter("@PreviousCollectionDate", this.StartDate.ToString_ddMMMyyyy());
                sSqlParams[3] = new SqlParameter("@Threshold", this.VarianceThreshold);
                result = context.ExecuteQueryAndGetDataSet(spName, sSqlParams, new string[] { MeterGlobals.ORIGINAL_TABLE });
                //context.TranslateDataSetHourly(result, _resourceManager);
                return result;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            return result;
        }
    }

    public class HourlyVTPSelectedItemData : HourlyVTPBaseData
    {
        public HourlyVTPSelectedItemData(rsp_GetSiteDetailsResult site)
            : base(site) { }

        public DateTime StartDate { get; set; }

        public string Hour { get; set; }

        public override DataSet ExecQuery(ExchangeDataContext context)
        {
            DataSet result = default(DataSet);

            try
            {
                string spName = "PGetHourlyDetails";
                SqlParameter[] sSqlParams = new SqlParameter[3];
                sSqlParams[0] = new SqlParameter("@Installation_ID", this.InstallationID.Value);
                sSqlParams[1] = new SqlParameter("@CollectionDate", this.StartDate.ToString_ddMMMyyyy());
                sSqlParams[2] = new SqlParameter("@Hours", this.Hour);
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

    public class HourlyVTPUpdateData : InstallationUpdateExecutorBase
    {
        public HourlyVTPUpdateData(rsp_GetSiteDetailsResult site)
            : base(site) { }

        public DateTime StartDate { get; set; }

        public string Hour { get; set; }

        public int? BatchID { get; set; }

        public string CollectionID { get; set; }

        public override bool ExecQuery(ExchangeDataContext context)
        {
            bool result = false;

            try
            {
                result = (context.UpdateHourlyDetails(this.InstallationID.Value.ToString(), this.CollectionID, this.StartDate.ToString_ddMMMyyyy(),
                    this.Hour, this.BatchID, this.UserID, this.UserName,
                    this.UpdateSet, this.CurrentSet) > 0);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            return result;
        }
    }

    public class HourlyVTP : ExchangeDataInterface
    {
        public HourlyVTP(string connectionString)
            : base(connectionString) { }

        protected override bool CanExecute(IParameterInfo parameterInfo)
        {
            Type typeOfT = parameterInfo.GetType();
            return (typeOfT == typeof(HourlyVTPSuspectedData) ||
                    typeOfT == typeof(HourlyVTPSelectedItemData) ||
                    typeOfT == typeof(HourlyVTPUpdateData));
        }
    }
}
