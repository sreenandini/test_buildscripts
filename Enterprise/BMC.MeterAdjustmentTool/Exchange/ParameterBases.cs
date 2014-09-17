using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Resources;
using BMC.MeterAdjustmentTool;

namespace BMC.MeterAdjustmentTool.Exchange
{
    public abstract class ParameterInfoBase : DisposableObject, IParameterInfo
    {
        protected ParameterInfoBase(rsp_GetSiteDetailsResult site)
        {
            this.Site = site;
        }

        #region IParameterInfo Members

        public rsp_GetSiteDetailsResult Site { get; set; }

        public virtual int VarianceThreshold
        {
            get
            {
                return MeterGlobals.ReadVarianceThreshold;
            }
        }

        #endregion
    }

    public abstract class SqlQueryExecutorBase<R> : ParameterInfoBase, IQueryExecutor<R>
    {
        protected ResourceManager _resourceManager = null;

        public SqlQueryExecutorBase(rsp_GetSiteDetailsResult site)
            : base(site) { }

        #region ISqlQueryInfo Members

        public abstract R ExecQuery(ExchangeDataContext context);

        #endregion
    }

    public abstract class InstallationExecutorBase : SqlQueryExecutorBase<DataSet>
    {
        protected InstallationExecutorBase(rsp_GetSiteDetailsResult site)
            : base(site) { }

        public int? InstallationID { get; set; }

        public string SiteWebUrl { get; set; }
    }

    public abstract class InstallationUpdateExecutorBase : SqlQueryExecutorBase<bool>, IUpdateSet
    {
        protected InstallationUpdateExecutorBase(rsp_GetSiteDetailsResult site)
            : base(site) { }

        public int? InstallationID { get; set; }

        public string UpdateSet { get; set; }

        public string CurrentSet { get; set; }

        public int? UserID { get; set; }

        public string UserName { get; set; }

        public string SiteWebUrl { get; set; }
    }    
}
