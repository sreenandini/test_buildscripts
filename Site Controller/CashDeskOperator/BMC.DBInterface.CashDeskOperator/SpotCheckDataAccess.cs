using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Reflection;
using BMC.DataAccess;
using BMC.Transport.CashDeskOperatorEntity;

namespace BMC.DBInterface.CashDeskOperator
{
    public class SpotCheckDataAccess
    {
        private static SpotCheckDataContext spotCheckDataContext = new SpotCheckDataContext(CommonDataAccess.ExchangeConnectionString);

        #region Methods

        /// <summary>
        /// To get installation details
        /// </summary>
        /// <returns></returns>
        public static ISingleResult<rsp_GetInstallationDetailsForSpotCheckResult> GetInstallationDetails()
        {
            return spotCheckDataContext.rsp_GetInstallationDetailsForSpotCheck();
        }

        public static ISingleResult<usp_GetSpotCheckDataByDropResult> GetSpotCheckSummaryDetails(int iInstallation_No, int iPop)
        {
            return spotCheckDataContext.usp_GetSpotCheckDataByDrop(iInstallation_No, iPop);
        }

        #endregion //Methods
    }

    [System.Data.Linq.Mapping.DatabaseAttribute(Name = "Exchange")]
    public partial class SpotCheckDataContext : System.Data.Linq.DataContext
    {
        private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();

        #region Extensibility Method Definitions
        partial void OnCreated();
        #endregion

        public SpotCheckDataContext(string connection) :
            base(connection, mappingSource)
        {
            OnCreated();
            this.CommandTimeout = SqlHelper.LoadCommandTimeout();
        }

        [Function(Name = "dbo.rsp_GetInstallationDetailsForSpotCheck")]
        public ISingleResult<rsp_GetInstallationDetailsForSpotCheckResult> rsp_GetInstallationDetailsForSpotCheck()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<rsp_GetInstallationDetailsForSpotCheckResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.usp_GetSpotCheckDataByDrop")]
        public ISingleResult<usp_GetSpotCheckDataByDropResult> usp_GetSpotCheckDataByDrop([Parameter(Name = "Installation_No", DbType = "Int")] System.Nullable<int> installation_No, [Parameter(Name = "PoP", DbType = "Int")] System.Nullable<int> poP)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), installation_No, poP);
            return ((ISingleResult<usp_GetSpotCheckDataByDropResult>)(result.ReturnValue));
        }
    }
}
