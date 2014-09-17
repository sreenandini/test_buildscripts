namespace BMC.MonitoringService
{
    #region Namespaces

    using System.Data.Linq;
    using System.Data.Linq.Mapping;
    using System.Reflection;

    #endregion Namespaces

    #region Public Class

    [System.Data.Linq.Mapping.DatabaseAttribute(Name = "Enterprise")]
    public partial class LinqDBAccessDataContext : System.Data.Linq.DataContext
    {

        private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();

        #region Extensibility Method Definitions
        partial void OnCreated();
        #endregion

        public LinqDBAccessDataContext() :
            base(global::BMC.MonitoringService.Properties.Settings.Default.EnterpriseConnectionString, mappingSource)
        {
            OnCreated();
        }

        public LinqDBAccessDataContext(string connection) :
            base(connection, mappingSource)
        {
            OnCreated();
        }

        public LinqDBAccessDataContext(System.Data.IDbConnection connection) :
            base(connection, mappingSource)
        {
            OnCreated();
        }

        public LinqDBAccessDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) :
            base(connection, mappingSource)
        {
            OnCreated();
        }

        public LinqDBAccessDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) :
            base(connection, mappingSource)
        {
            OnCreated();
        }

        [Function(Name = "dbo.rsp_GetAllSiteDetails")]
        public ISingleResult<SiteDetails> GetAllSiteDetails()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<SiteDetails>)(result.ReturnValue));
        }

        [Function(Name = "dbo.usp_InsertEnterpriseEvents")]
        public int InsertEnterpriseEvents([Parameter(Name = "EventSiteId", DbType = "Int")] System.Nullable<int> eventSiteId, [Parameter(Name = "EventFaultSource", DbType = "Int")] System.Nullable<int> eventFaultSource, [Parameter(Name = "EventFaultType", DbType = "Int")] System.Nullable<int> eventFaultType)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), eventSiteId, eventFaultSource, eventFaultType);
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetAllServices")]
        public ISingleResult<ServiceDetails> GetAllServices()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<ServiceDetails>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetSiteURL")]
        public ISingleResult<GetSpecificSiteDetails> GetSpecificSiteDetails([Parameter(Name = "Site_Id", DbType = "Int")] System.Nullable<int> site_Id)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), site_Id);
            return ((ISingleResult<GetSpecificSiteDetails>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetSiteStatusByID")]
        public ISingleResult<GetSiteStatusByIDResult> GetSiteStatusByID([Parameter(DbType = "Int")] System.Nullable<int> siteID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), siteID);
            return ((ISingleResult<GetSiteStatusByIDResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.usp_UpdateHourlyNotRun")]
        public int UpdateHourlyNotRun([Parameter(Name = "Site_ID", DbType = "Int")] System.Nullable<int> site_ID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), site_ID);
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.usp_UpdateSiteDownAlert")]
        public int UpdateSiteDownAlert([Parameter(Name = "Site_ID", DbType = "Int")] System.Nullable<int> site_ID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), site_ID);
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.usp_UpdateReadNotRun")]
        public int UpdateReadNotRun([Parameter(Name = "Site_ID", DbType = "Int")] System.Nullable<int> site_ID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), site_ID);
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.usp_ResetHourlyNotRun")]
        public int ResetHourlyNotRun()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((int)(result.ReturnValue));
        }


    }

    #endregion Public Class
}
