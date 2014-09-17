namespace BMCTicketingConfig
{
    using System.Data.Linq;
    using System.Data.Linq.Mapping;
    using System.Data;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Linq;
    using System.Linq.Expressions;
    using System.ComponentModel;
    using System;


    [System.Data.Linq.Mapping.DatabaseAttribute(Name = "Enterprise")]
    public partial class DBClass : System.Data.Linq.DataContext
    {

        private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();

        #region Extensibility Method Definitions
        partial void OnCreated();
        #endregion

        public DBClass() : base(global::BMCTicketingConfig.Properties.Settings.Default.EnterpriseConnectionString, mappingSource)
        {
            OnCreated();
        }

        public DBClass(string connection) :
            base(connection, mappingSource)
        {
            OnCreated();
        }

        public DBClass(System.Data.IDbConnection connection) :
            base(connection, mappingSource)
        {
            OnCreated();
        }

        public DBClass(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) :
            base(connection, mappingSource)
        {
            OnCreated();
        }

        public DBClass(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) :
            base(connection, mappingSource)
        {
            OnCreated();
        }

        [Function(Name = "dbo.rsp_GetFullyConfiguredSites")]
        public ISingleResult<GetSitesResult> GetSites([Parameter(Name = "SITECODE", DbType = "VarChar(50)")] string sITECODE)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), sITECODE);
            return ((ISingleResult<GetSitesResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.esp_InsertSiteAlliance")]
        public int InsertIntoSiteAlliance([Parameter(Name = "ClientSiteCode", DbType = "VarChar(50)")] string clientSiteCode, [Parameter(Name = "HostSiteCode", DbType = "VarChar(50)")] string hostSiteCode, [Parameter(Name = "HostSiteURL", DbType = "VarChar(2000)")] string hostSiteURL, [Parameter(Name = "IsCashableRedeemable", DbType = "Bit")] System.Nullable<bool> isCashableRedeemable, [Parameter(Name = "IsPromoRedeemable", DbType = "Bit")] System.Nullable<bool> isPromoRedeemable)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), clientSiteCode, hostSiteCode, hostSiteURL, isCashableRedeemable, isPromoRedeemable);
            return ((int)(result.ReturnValue));
        }
    }
}