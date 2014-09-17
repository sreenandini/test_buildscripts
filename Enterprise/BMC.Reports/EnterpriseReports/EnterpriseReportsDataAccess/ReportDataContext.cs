using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Data;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Linq.Expressions;
using System.ComponentModel;
using System;
using BMC.EnterpriseReportsTransport;
using BMC.Security.Manager;
using BMC.Security.Interfaces;
using System.Data.SqlClient;
using BMC.DataAccess;
using BMC.Common.ExceptionManagement;


namespace BMC.EnterpriseReportsDataAccess
{


    [System.Data.Linq.Mapping.DatabaseAttribute(Name = "Enterprise")]
    public class ReportDataContext : System.Data.Linq.DataContext
    {

        private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();


        RoleManager roleManager;


        public ReportDataContext() :
            base(global::BMC.EnterpriseReportsDataAccess.Properties.Settings.Default.EnterpriseConnectionString, mappingSource)
        {
            //OnCreated();
            this.Connection.ConnectionString = DbConnection.EnterpriseConnectionString;
            roleManager = new RoleManager(this.Connection.ConnectionString);

        }
        public IEnumerable<IRole> GetRoleByName(string RoleName)
        {
            List<IRole> oIRole = null;
            IRole Role = roleManager.GetRoleByName(RoleName);
            if (Role == null)
            {
                roleManager.AddRole(RoleName, RoleName);
                Role = roleManager.GetRoleByName(RoleName);

            }
            oIRole = new List<IRole>();
            oIRole.Add(Role);
            return oIRole;
        }

        //public ReportDataContext(string connection) :
        //    base(connection, mappingSource)
        //{
        //    OnCreated();
        //}

        //public ReportDataContext(System.Data.IDbConnection connection) :
        //    base(connection, mappingSource)
        //{
        //    OnCreated();
        //}

        //public ReportDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) :
        //    base(connection, mappingSource)
        //{
        //    OnCreated();
        //}

        //public ReportDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) :
        //    base(connection, mappingSource)
        //{
        //    OnCreated();
        //}

        [Function(Name = "dbo.rsp_GetAllReportsToRolesAccess")]
        public ISingleResult<GetAllReportsToRolesAccess> GetAllReportsToRolesAccess([Parameter(Name = "SecurityRoleID", DbType = "Int")] System.Nullable<int> securityRoleID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), securityRoleID);
            return ((ISingleResult<GetAllReportsToRolesAccess>)(result.ReturnValue));
        }
        [Function(Name = "dbo.rsp_GetCompanyDetails")]
        public ISingleResult<Company> GetCompanyDetails([Parameter(Name= "SecurityUserID",DbType = "Int")] System.Nullable<int> SecurityUserID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())),SecurityUserID);
            return ((ISingleResult<Company>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetSubCompanyDetails")]
        public ISingleResult<SubCompany> GetSubCompanyDetails([Parameter(DbType = "Int")] System.Nullable<int> company,[Parameter(DbType = "Int")] System.Nullable<int> SecurityUserID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), company,SecurityUserID);
            return ((ISingleResult<SubCompany>)(result.ReturnValue));
        }
        [Function(Name = "dbo.rsp_GetRegionDetails")]
        public ISingleResult<SubCompanyRegion> GetRegionDetails([Parameter(DbType = "Int")] System.Nullable<int> subcompany, [Parameter(DbType = "Int")] System.Nullable<int> companyID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), subcompany, companyID);
            return ((ISingleResult<SubCompanyRegion>)(result.ReturnValue));
        }
        [Function(Name = "dbo.rsp_GetAreaDetails")]
        public ISingleResult<SubCompanyArea> GetAreaDetails([Parameter(DbType = "Int")] System.Nullable<int> region, [Parameter(DbType = "Int")] System.Nullable<int> subcompany, [Parameter(DbType = "Int")] System.Nullable<int> companyID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), region,subcompany, companyID);
            return ((ISingleResult<SubCompanyArea>)(result.ReturnValue));
        }
        [Function(Name = "dbo.rsp_GetCategoryDetails")]
        public ISingleResult<Machine_Type> GetCategoryDetails()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<Machine_Type>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetDistrictDetails")]
        public ISingleResult<SubCompanyDistrict> GetDistrictDetails([Parameter(DbType = "Int")] System.Nullable<int> region, [Parameter(DbType = "Int")] System.Nullable<int> area, [Parameter(DbType = "Int")] System.Nullable<int> subcompany_id, [Parameter(DbType = "Int")] System.Nullable<int> companyID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), region, area, subcompany_id,companyID);
            return ((ISingleResult<SubCompanyDistrict>)(result.ReturnValue));
        }
        [Function(Name = "dbo.rsp_GetDepoDetails")]
        public ISingleResult<Depot> GetDepoDetails([Parameter(DbType = "Int")] System.Nullable<int> site)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), site);
            return ((ISingleResult<Depot>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_Report_GetSites")]
        public ISingleResult<Site> GetSites([Parameter(DbType = "Int")] System.Nullable<int> company, [Parameter(DbType = "Int")] System.Nullable<int> subcompany, [Parameter(DbType = "Int")] System.Nullable<int> region, [Parameter(DbType = "Int")] System.Nullable<int> area, [Parameter(DbType = "Int")] System.Nullable<int> Userid)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), company, subcompany, region, area, Userid);
            return ((ISingleResult<Site>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetSiteDetailsForDistrict")]
        public ISingleResult<Site> GetSiteDetailsForDistict([Parameter(DbType = "Int")] System.Nullable<int> company, [Parameter(DbType = "Int")] System.Nullable<int> subcompany, [Parameter(DbType = "Int")] System.Nullable<int> region, [Parameter(DbType = "Int")] System.Nullable<int> area, [Parameter(DbType = "Int")] System.Nullable<int> district, [Parameter(DbType = "Int")] System.Nullable<int> Userid)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), company, subcompany, region, area,district, Userid);
            return ((ISingleResult<Site>)(result.ReturnValue));
        }


        [Function(Name = "dbo.rsp_GetSuppliers")]
        public ISingleResult<Operator> GetSuppliers()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<Operator>)(result.ReturnValue));
        }
        [Function(Name = "dbo.rsp_GetSetting")]
        public int GetSetting([Parameter(Name = "Setting_ID", DbType = "Int")] System.Nullable<int> setting_ID, [Parameter(Name = "Setting_Name", DbType = "VarChar(100)")] string setting_Name, [Parameter(Name = "Setting_Default", DbType = "VarChar(100)")] string setting_Default, [Parameter(Name = "Setting_Value", DbType = "VarChar(100)")] ref string setting_Value)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), setting_ID, setting_Name, setting_Default, setting_Value);
            setting_Value = ((string)(result.GetParameterValue(3)));
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetSetting")]
        public string GetSettingValue([Parameter(Name = "Setting_ID", DbType = "Int")] System.Nullable<int> setting_ID, [Parameter(Name = "Setting_Name", DbType = "VarChar(100)")] string setting_Name, [Parameter(Name = "Setting_Default", DbType = "VarChar(100)")] string setting_Default, [Parameter(Name = "Setting_Value", DbType = "VarChar(100)")] ref string setting_Value)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), setting_ID, setting_Name, setting_Default, setting_Value);
            setting_Value = ((string)(result.GetParameterValue(3)));
            return ((string)(result.ReturnValue));
        }
        [Function(Name = "dbo.rsp_GetZones")]
        public ISingleResult<Zone> GetZones([Parameter(DbType = "Int")] System.Nullable<int> site)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), site);
            return ((ISingleResult<Zone>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetZones_BySubCompany")]
        public ISingleResult<Zone> GetZones([Parameter(DbType = "Int")] System.Nullable<int> site, [Parameter(DbType = "Int")] System.Nullable<int> subCompany, [Parameter(DbType = "Int")] System.Nullable<int> company)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), site, subCompany,company);
            return ((ISingleResult<Zone>)(result.ReturnValue));
        }

        [ResultType(typeof(SProcResult))]
        [Function(Name = "dbo.rsp_GetVersion")]
        public ISingleResult<SProcResult> GetBMCVersion()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<SProcResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetSlots")]
        public ISingleResult<Slot> GetSlots([Parameter(Name = "SITE", DbType = "Int")] System.Nullable<int> sITE, [Parameter(Name = "CompanyID", DbType = "Int")] System.Nullable<int> companyID, [Parameter(Name = "SubCompanyId", DbType = "Int")] System.Nullable<int> subCompanyId)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), sITE, companyID, subCompanyId);
            return ((ISingleResult<Slot>)(result.ReturnValue));
        }

        [ResultType(typeof(SiteCultureInfo))]
        [Function(Name = "dbo.rsp_GetSiteCulture")]
        public ISingleResult<SiteCultureInfo> GetSiteCulture([Parameter(Name = "UserId", DbType = "Int")] System.Nullable<int> userId)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), userId);
            return ((ISingleResult<SiteCultureInfo>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetEmployeeID")]
        public ISingleResult<EmployeeID> GetEmployeeID([Parameter(Name = "Site", DbType = "Int")] System.Nullable<int> site)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), site);
            return ((ISingleResult<EmployeeID>)(result.ReturnValue));
        }        

        [Function(Name = "dbo.rsp_GetEmployeeCardID")]
        public ISingleResult<EmployeeCardID> GetEmployeeCardID([Parameter(Name = "Site", DbType = "Int")] System.Nullable<int> site, [Parameter(Name = "UserID", DbType = "Int")] System.Nullable<int> userID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), site, userID);
            return ((ISingleResult<EmployeeCardID>)(result.ReturnValue));
        }              

        [Function(Name = "dbo.rsp_GetEmployeeCardNumber")]
        public ISingleResult<CardNumberList>GetEmployeeCardNumber()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<CardNumberList>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetEmployeeName")]
        public ISingleResult<EmployeeNameList>GetEmployeeName([Parameter(Name = "CardNumber", DbType = "Int")] System.Nullable<int> cardNumber)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), cardNumber);
            return ((ISingleResult<EmployeeNameList>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetEmployeeCardType")]
        public ISingleResult<CardTypes> GetEmployeeCardTypes()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<CardTypes>)(result.ReturnValue));
        }

       [Function(Name = "dbo.rsp_GetRoleToUser")]
        public ISingleResult<UserRoles> GetRoleToUser([Parameter(Name = "Usertableid", DbType = "Int")] System.Nullable<int> Usertableid)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), Usertableid);
            return ((ISingleResult<UserRoles>)(result.ReturnValue));
        }

       [Function(Name = "dbo.rsp_GetRuleName")]
       public ISingleResult<SiteLicensing> GetRuleName()
       {
           IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
           return ((ISingleResult<SiteLicensing>)(result.ReturnValue));
       }

       public DataSet GetRedeemedTicketByDevice(int Company, int SubCompany, int Site, int Zone, DateTime fromDate, DateTime toDate, string DeviceType, string sSiteIDList)
        {
            DataSet dsRedeem = new DataSet();

            try
            {
                SqlParameter[] objParams = new SqlParameter[9];

                objParams[0] = new SqlParameter("@COMPANY", Company);
                objParams[1] = new SqlParameter("@SUBCOMPANY", SubCompany);
                objParams[2] = new SqlParameter("@SITE", Site);
                objParams[3] = new SqlParameter("@Zone", Zone);
                objParams[4] = new SqlParameter("@startDate", fromDate);
                objParams[5] = new SqlParameter("@endDate", toDate);
                objParams[6] = new SqlParameter("@DeviceType", DeviceType);
                objParams[7] = new SqlParameter("@GROUPBYZONE", false);
                objParams[8] = new SqlParameter("@SiteIDList", sSiteIDList);
                
                SqlHelper.FillDataset(roleManager.Connection.ConnectionString, "rsp_getRedeemedTicketByDevice", dsRedeem, new string[] { "RedeemedTicketByDevice" }, objParams);

                return dsRedeem;

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return null;
            }
        }



       public DataSet GetExpiredVoucherCouponReport(int Company, int SubCompany, int Site, DateTime startDate, DateTime endDate, string sDeviceType, string sSiteIDList)
        {
            DataSet dsExpiredVoucherCoupon = new DataSet();

            try
            {
                SqlParameter[] objParams = new SqlParameter[7];
                objParams[0] = new SqlParameter("@COMPANY", Company);
                objParams[1] = new SqlParameter("@SUBCOMPANY", SubCompany);
                objParams[2] = new SqlParameter("@SITE", Site);
                objParams[3] = new SqlParameter("@startDate", startDate);
                objParams[4] = new SqlParameter("@EndDate", endDate);
                objParams[5] = new SqlParameter("@DeviceType", sDeviceType);
                objParams[6] = new SqlParameter("@SiteIDList", sSiteIDList);


                SqlHelper.FillDataset(roleManager.Connection.ConnectionString, "rsp_GetExpiredVoucherCouponReport", dsExpiredVoucherCoupon, new string[] { "ExpiredVoucherCoupon" }, objParams);

                return dsExpiredVoucherCoupon;

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return null;
            }
        }

        public DataSet GetLiabilityTransferSummaryReport(int Company, int SubCompany, int Zone, int Site, DateTime startDate, DateTime endDate,string sSiteIDList)
        {
            DataSet dsLiabilityTransferSummary = new DataSet();

            try
            {
                SqlParameter[] objParams = new SqlParameter[6];
                objParams[0] = new SqlParameter("@COMPANY", Company);
                objParams[1] = new SqlParameter("@SUBCOMPANY", SubCompany);
                objParams[2] = new SqlParameter("@SITE", Site);
                objParams[3] = new SqlParameter("@STARTDATE", startDate); //new SqlParameter("@ZONE", Zone);
                objParams[4] = new SqlParameter("@ENDDATE", endDate);
                objParams[5] = new SqlParameter("@SiteIDList", sSiteIDList);


                SqlHelper.FillDataset(roleManager.Connection.ConnectionString, "rsp_GetLiabilityTransferSummaryReport", dsLiabilityTransferSummary, new string[] { "LiabilityTransferSummary" }, objParams);

                return dsLiabilityTransferSummary;

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return null;
            }
        }

        public DataSet GetVoucherListingReport(int Company, int SubCompany, int Site, int ZoneID, DateTime startDate, DateTime endDate, string sStatus, string sSlot, string sSiteIDList)
        {
            DataSet dsListing = new DataSet();

            try
            {
                SqlParameter[] objParams = new SqlParameter[10];
                objParams[0] = new SqlParameter("@COMPANY", Company);
                objParams[1] = new SqlParameter("@SUBCOMPANY", SubCompany);
                objParams[2] = new SqlParameter("@SITE", Site);
                objParams[3] = new SqlParameter("@zone", ZoneID);

                objParams[4] = new SqlParameter("@startDate", startDate);
                objParams[5] = new SqlParameter("@endDate", endDate);
                objParams[6] = new SqlParameter("@Status", sStatus);
                objParams[7] = new SqlParameter("@Slot", sSlot);
                objParams[8] = new SqlParameter("@GROUPBYZONE", false);
                objParams[9] = new SqlParameter("@SiteIDList", sSiteIDList);
                SqlHelper.FillDataset(roleManager.Connection.ConnectionString, "rsp_GetVoucherListingReport", dsListing, new string[] { "VoucherListingReport" }, objParams);

                return dsListing;

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return null;
            }

        }

        public DataSet GetPromotionalVoucherListingReport(int Company, int SubCompany, int Site, DateTime startDate, DateTime endDate, string sStatus,string sSiteIDList)
        {
            DataSet dsPromoListing = new DataSet();

            try
            {
                SqlParameter[] objParams = new SqlParameter[7];
                objParams[0] = new SqlParameter("@COMPANY", Company);
                objParams[1] = new SqlParameter("@SUBCOMPANY", SubCompany);
                objParams[2] = new SqlParameter("@SITE", Site);
               // objParams[3] = new SqlParameter("@zone", ZoneID);

                objParams[3] = new SqlParameter("@startDate", startDate);
                objParams[4] = new SqlParameter("@endDate", endDate);
                objParams[5] = new SqlParameter("@Status", sStatus);
                objParams[6] = new SqlParameter("@Status", sSiteIDList);



                SqlHelper.FillDataset(roleManager.Connection.ConnectionString, "rsp_PromotionalVoucherListingReport", dsPromoListing, new string[] { "PromotionalVoucherListingReport" }, objParams);

                return dsPromoListing;

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return null;
            }

        }
        

        public DataSet GetJackpotSlipSummaryDetails(int company, int subCompany, int site, DateTime fromDate,
            DateTime toDate, bool ShowHandpay, bool ShowJackpot, string SiteIDList)
        {
            DataSet dsJackpotSlipDetails = new DataSet();

            try
            {
                SqlParameter[] objParams = new SqlParameter[8];

                objParams[0] = new SqlParameter("@Company", company);
                objParams[1] = new SqlParameter("@SubCompany", subCompany);
                objParams[2] = new SqlParameter("@SITE", site);
                objParams[3] = new SqlParameter("@StartDate", fromDate);
                objParams[4] = new SqlParameter("@EndDate", toDate);
                objParams[5] = new SqlParameter("@ShowHandpay", ShowHandpay);
                objParams[6] = new SqlParameter("@ShowJackpot", ShowJackpot);
                objParams[7] = new SqlParameter("@SiteIDList", SiteIDList);
                SqlHelper.FillDataset(roleManager.Connection.ConnectionString, "rsp_GetJackpotSlipSummaryDetails", dsJackpotSlipDetails, new string[] { "JackpotSlipSummaryDetails" }, objParams);

                return dsJackpotSlipDetails;

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return null;
            }
        }

        public DataSet GetCoinInByPaytableDetails(int company, int subCompany, int site, DateTime fromDate, DateTime toDate, string groupBy, string SiteIDList)
        {
            DataSet dsCoinInByPaytableDetails = new DataSet();

            try
            {
                SqlParameter[] objParams = new SqlParameter[7];

                objParams[0] = new SqlParameter("@Company", company);
                objParams[1] = new SqlParameter("@SubCompany", subCompany);
                objParams[2] = new SqlParameter("@SITE", site);
                objParams[3] = new SqlParameter("@StartDate", fromDate);
                objParams[4] = new SqlParameter("@EndDate", toDate);
                objParams[5] = new SqlParameter("@GroupBy", groupBy);
                objParams[6] = new SqlParameter("@SiteIDList", SiteIDList);

                SqlHelper.FillDataset(roleManager.Connection.ConnectionString, "rsp_CoinInByPayTableReport", dsCoinInByPaytableDetails, new string[] { "CoinInByPayTableReportDetails" }, objParams);

                return dsCoinInByPaytableDetails;

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return null;
            }
        }

        public DataSet GetMultiDenomSlotDetails(int company, int subCompany, int site, DateTime fromDate, DateTime toDate, string groupBy, string SiteIDList)
        {
            DataSet dsMultiDenomSlotDetails = new DataSet();

            try
            {
                SqlParameter[] objParams = new SqlParameter[7];

                objParams[0] = new SqlParameter("@Company", company);
                objParams[1] = new SqlParameter("@SubCompany", subCompany);
                objParams[2] = new SqlParameter("@SITE", site);
                objParams[3] = new SqlParameter("@StartDate", fromDate);
                objParams[4] = new SqlParameter("@EndDate", toDate);
                objParams[5] = new SqlParameter("@GroupBy", groupBy);
                objParams[6] = new SqlParameter("@SiteIDList", SiteIDList);
                SqlHelper.FillDataset(roleManager.Connection.ConnectionString, "rsp_MultiDenomSlotDetailReport", dsMultiDenomSlotDetails, new string[] { "MultiDenomSlotDetails" }, objParams);

                return dsMultiDenomSlotDetails;

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return null;
            }
        }

        public DataSet GetMultiGameSlotDetails(int company, int subCompany, int site, DateTime fromDate, DateTime toDate, string groupBy, string SiteIDList)
        {
            DataSet dsMultiGameSlotDetails = new DataSet();

            try
            {
                SqlParameter[] objParams = new SqlParameter[7];

                objParams[0] = new SqlParameter("@Company", company);
                objParams[1] = new SqlParameter("@SubCompany", subCompany);
                objParams[2] = new SqlParameter("@SITE", site);
                objParams[3] = new SqlParameter("@StartDate", fromDate);
                objParams[4] = new SqlParameter("@EndDate", toDate);
                objParams[5] = new SqlParameter("@GroupBy", groupBy);
                objParams[6] = new SqlParameter("@SiteIDList", SiteIDList);

                SqlHelper.FillDataset(roleManager.Connection.ConnectionString, "rsp_MultiGameSlotDetailReport", dsMultiGameSlotDetails, new string[] { "MultiGameSlotDetails" }, objParams);

                return dsMultiGameSlotDetails;

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return null;
            }
        }

        public DataSet GetMultiDenomPerformanceDetails(int company, int subCompany, int site, DateTime fromDate, DateTime toDate, string SiteIDList)
        {
            DataSet dsMultiDenomPerformanceDetails = new DataSet();

            try
            {
                SqlParameter[] objParams = new SqlParameter[6];

                objParams[0] = new SqlParameter("@Company", company);
                objParams[1] = new SqlParameter("@SubCompany", subCompany);
                objParams[2] = new SqlParameter("@SITE", site);
                objParams[3] = new SqlParameter("@StartDate", fromDate);
                objParams[4] = new SqlParameter("@EndDate", toDate);
                objParams[5] = new SqlParameter("@SiteIDList", SiteIDList);
                SqlHelper.FillDataset(roleManager.Connection.ConnectionString, "rsp_Report_MGMD_DenomPerformance", dsMultiDenomPerformanceDetails, new string[] { "MultiDenomPerformanceDetails" }, objParams);

                return dsMultiDenomPerformanceDetails;

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return null;
            }
        }

        public DataSet GetMultiGamePerformanceDetails(int company, int subCompany, int site, int zone, DateTime fromDate, DateTime toDate, string SiteIDList)
        {
            DataSet dsMultiGamePerformanceDetails = new DataSet();

            try
            {
                SqlParameter[] objParams = new SqlParameter[7];

                objParams[0] = new SqlParameter("@Company", company);
                objParams[1] = new SqlParameter("@SubCompany", subCompany);
                objParams[2] = new SqlParameter("@SITE", site);
                objParams[3] = new SqlParameter("@Zone", zone);
                objParams[4] = new SqlParameter("@StartDate", fromDate);
                objParams[5] = new SqlParameter("@EndDate", toDate);
                objParams[6] = new SqlParameter("@SiteIDList", SiteIDList);
                SqlHelper.FillDataset(roleManager.Connection.ConnectionString, "rsp_Report_MGMD_GamePerformance", dsMultiGamePerformanceDetails, new string[] { "MultiGamePerformanceDetails" }, objParams);

                return dsMultiGamePerformanceDetails;

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return null;
            }
        }

        public DataSet GetMultiGameDetails(int company, int subCompany, int site, int zone, DateTime fromDate, DateTime toDate, string SiteIDList)
        {
            DataSet dsMultiGameDetails = new DataSet();

            try
            {
                SqlParameter[] objParams = new SqlParameter[7];

                objParams[0] = new SqlParameter("@Company", company);
                objParams[1] = new SqlParameter("@SubCompany", subCompany);
                objParams[2] = new SqlParameter("@SITE", site);
                objParams[3] = new SqlParameter("@Zone", zone);
                objParams[4] = new SqlParameter("@StartDate", fromDate);
                objParams[5] = new SqlParameter("@EndDate", toDate);
                objParams[6] = new SqlParameter("@SiteIDList", SiteIDList);
                SqlHelper.FillDataset(roleManager.Connection.ConnectionString, "rsp_Report_MGMD_GameReport", dsMultiGameDetails, new string[] { "MultiGameDetails" }, objParams);

                return dsMultiGameDetails;

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return null;
            }
        }

        //

        public DataSet GetStandardMeterComparisonDetails(int company, int subCompany, int site, int zone, DateTime fromDate, DateTime toDate, float denom, int runtype, string SiteIDList)
        {
            DataSet dsStandardMeterComparisonDetails = new DataSet();

            try
            {
                SqlParameter[] objParams = new SqlParameter[8];

                objParams[0] = new SqlParameter("@company", company);
                objParams[1] = new SqlParameter("@subcompany", subCompany);
                objParams[2] = new SqlParameter("@site", site);
                objParams[3] = new SqlParameter("@zone", zone);
                objParams[4] = new SqlParameter("@startdate", fromDate.Date);
                objParams[5] = new SqlParameter("@enddate", toDate.Date);
                objParams[6] = new SqlParameter("@GROUPBYZONE", false);
                objParams[7] = new SqlParameter("@SiteIDList", SiteIDList);

                SqlHelper.FillDataset(roleManager.Connection.ConnectionString, "rsp_Report_MeterComparison", dsStandardMeterComparisonDetails, new string[] { "MultiGameDetails" }, objParams);

                return dsStandardMeterComparisonDetails;

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return null;
            }
        }

        public DataSet GetSlotMachinePerformaceDetails(int company, int subCompany, int site, DateTime gamingDate, float denom, string machineTypeID, int runType, bool usePhysicalWin, bool includeNonCashable, string SiteIDList)
        {
            DataSet dsSlotMachinePerformaceDetails = new DataSet();

            try
            {
                SqlParameter[] objParams = new SqlParameter[6];

                objParams[0] = new SqlParameter("@company", company);
                objParams[1] = new SqlParameter("@subcompany", subCompany);
                objParams[2] = new SqlParameter("@site", site);
                objParams[3] = new SqlParameter("@gamingdate", gamingDate);
                objParams[4] = new SqlParameter("@IncludeNonCashable", includeNonCashable);
                //objParams[5] = new SqlParameter("@denom", denom);
                //objParams[6] = new SqlParameter("@MachineTypeID", machineTypeID);
                //objParams[7] = new SqlParameter("@runtype", runType);
                //objParams[8] = new SqlParameter("@UsePhysicalWin", usePhysicalWin);
                objParams[5] = new SqlParameter("@SiteIDList", SiteIDList);
                SqlHelper.FillDataset(roleManager.Connection.ConnectionString, "rsp_Report_SlotMachinePerformance", dsSlotMachinePerformaceDetails, new string[] { "SlotMachinePerformaceDetails" }, objParams);

                return dsSlotMachinePerformaceDetails;

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return null;
            }
        }
        //
        public DataSet GetSlotCountComparisonDetails(int company, int subCompany, int site, int zone, DateTime fromDate, DateTime toDate, float denom, int runtype, bool GroupByZone, string SiteIDList)
        {
            DataSet dsSlotCountComparisonDetails = new DataSet();

            try
            {
                SqlParameter[] objParams = new SqlParameter[8];

                objParams[0] = new SqlParameter("@company", company);
                objParams[1] = new SqlParameter("@subcompany", subCompany);
                objParams[2] = new SqlParameter("@site", site);
                objParams[3] = new SqlParameter("@zone", zone);
                objParams[4] = new SqlParameter("@startdate", fromDate.Date);
                objParams[5] = new SqlParameter("@enddate", toDate.Date);
                objParams[6] = new SqlParameter("@GROUPBYZONE", GroupByZone);
                objParams[7] = new SqlParameter("@SiteIDList", SiteIDList);
                SqlHelper.FillDataset(roleManager.Connection.ConnectionString, "rsp_Report_SoftCountComparison", dsSlotCountComparisonDetails, new string[] { "SlotCountComparisonDetails" }, objParams);

                return dsSlotCountComparisonDetails;

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return null;
            }
        }

        public DataSet getDailyEFTFundRevenueDetails(int ncompany, int nsubCompany, int nSite, int zone, string dtGamingDate, string sPeriod, string SiteIDList, ref bool isSuccess)
        {
            DataSet dsDailyEFTFundRevenueDetails = new DataSet();

            try
            {
                SqlParameter[] objParams = new SqlParameter[7];

                objParams[0] = new SqlParameter("@company", ncompany);
                objParams[1] = new SqlParameter("@subcompany", nsubCompany);
                objParams[2] = new SqlParameter("@site", nSite);
                objParams[3] = new SqlParameter("@zone", zone);
                objParams[4] = new SqlParameter("@gamingdate", dtGamingDate);
                objParams[5] = new SqlParameter("@Period", sPeriod);
                objParams[6] = new SqlParameter("@SiteIDList", SiteIDList);

                SqlHelper.FillDataset(roleManager.Connection.ConnectionString, "rsp_Report_DailyElectronicCashRevenue_Crystal",
                                        dsDailyEFTFundRevenueDetails, new string[] { "DailyElectronicCashRevenue" }, objParams);

                return dsDailyEFTFundRevenueDetails;

            }
            catch (Exception ex)
            {
                isSuccess = false;
                ExceptionManager.Publish(ex);
                return null;
            }
        }

        // Added by A.Vinod Kumar on 16/12/2010
        public DataSet GetElecTransferVsRevenueComparisonDetails(int company, int subCompany, int site, string zone, DateTime gamingDate, int ZoneId, bool GroupByZone, string SiteIDList)
        {
            DataSet dsElecTransferVSRevenueComparison = new DataSet();

            try
            {
                SqlParameter[] objParams = new SqlParameter[7];

                objParams[0] = new SqlParameter("@company", company);
                objParams[1] = new SqlParameter("@subcompany", subCompany);
                objParams[2] = new SqlParameter("@site", site);

                //if (zone == "--ALL--")
                //{
                //    objParams[3] = new SqlParameter("@zone", string.Empty);
                //}
                //else
                //    objParams[3] = new SqlParameter("@zone", zone);
       
                objParams[4] = new SqlParameter("@gamingdate", gamingDate);
                objParams[3] = new SqlParameter("@zone", ZoneId);
                objParams[5] = new SqlParameter("@GROUPBYZONE", GroupByZone);
                objParams[6] = new SqlParameter("@SiteIDList", SiteIDList);
                SqlHelper.FillDataset(roleManager.Connection.ConnectionString, "rsp_Report_EFTVersusBMCRevenueComparison", dsElecTransferVSRevenueComparison, new string[] { "ElecTransferVSRevenueComparison" }, objParams);

                return dsElecTransferVSRevenueComparison;

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return null;
            }
        }

        [Function(Name = "dbo.rsp_GetWeeklyLiquidationBatch")]
        public ISingleResult<PeriodEnd> GetWeeklyLiquidationBatch([Parameter(DbType = "Int")] System.Nullable<int> company,
            [Parameter(DbType = "Int")] System.Nullable<int> SubCompany)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), company, SubCompany);
            return ((ISingleResult<PeriodEnd>)(result.ReturnValue));
        }


        [Function(Name = "dbo.rsp_GetBatchDetails")]
        public ISingleResult<BatchDetails> GetBatchDetails([Parameter(Name = "Site", DbType = "VarChar(20)")] string Site)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), Site);
            return ((ISingleResult<BatchDetails>)(result.ReturnValue));
        }

        public int GetAllSiteCount()
        {
            return Convert.ToInt32(SqlHelper.ExecuteScalar(DbConnection.EnterpriseConnectionString, CommandType.StoredProcedure, "rsp_GetAllSiteCount"));
        }

        public DataSet getEFTQuestionableTransactions(int ncompany, int nsubCompany, int nSite, string dtStartDate, string dtEndDate,string SiteIDList)
        {
            DataSet dsEFTQuestionableTransactions = new DataSet();

            try
            {
                SqlParameter[] objParams = new SqlParameter[6];

                objParams[0] = new SqlParameter("@company", ncompany);
                objParams[1] = new SqlParameter("@subcompany", nsubCompany);
                objParams[2] = new SqlParameter("@site", nSite);
                objParams[3] = new SqlParameter("@startdate", dtStartDate);
                objParams[4] = new SqlParameter("@enddate", dtEndDate);
                objParams[5] = new SqlParameter("@SiteIDList", SiteIDList);

                SqlHelper.FillDataset(roleManager.Connection.ConnectionString, "rsp_Report_EFTQuestionableTransactions",
                                        dsEFTQuestionableTransactions, new string[] { "EFTQuestionableTransactions" }, objParams);

                return dsEFTQuestionableTransactions;

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return null;
            }
        }


        public DataSet getEFTSlotActivity(int ncompany, int nsubCompany, int nSite, string dtStartDate, string dtEndDate, string SiteIDList)
        {
            DataSet dsEFTSlotActivity = new DataSet();

            try
            {
                SqlParameter[] objParams = new SqlParameter[6];

                objParams[0] = new SqlParameter("@company", ncompany);
                objParams[1] = new SqlParameter("@subcompany", nsubCompany);
                objParams[2] = new SqlParameter("@site", nSite);
                objParams[3] = new SqlParameter("@startdate", dtStartDate);
                objParams[4] = new SqlParameter("@enddate", dtEndDate);
                objParams[5] = new SqlParameter("@SiteIDList", SiteIDList);

                SqlHelper.FillDataset(roleManager.Connection.ConnectionString, "rsp_Report_EFTSlotActivity",
                                        dsEFTSlotActivity, new string[] { "EFTSlotActivity" }, objParams);

                return dsEFTSlotActivity;

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return null;
            }
        }

        public DataSet getInstallationWinLoss(int ncompany, int nsubCompany, int region,
            int area, int district, int nSite, int category, string dtStartDate, string dtEndDate, string SiteIDList)
        {
            DataSet dsInstallationWinLoss = new DataSet();

            try
            {
                SqlParameter[] objParams = new SqlParameter[10];

                objParams[0] = new SqlParameter("@company", ncompany);
                objParams[1] = new SqlParameter("@subcompany", nsubCompany);
                objParams[2] = new SqlParameter("@region", region);
                objParams[3] = new SqlParameter("@area", area);
                objParams[4] = new SqlParameter("@district", district);
                objParams[5] = new SqlParameter("@site", nSite);
                objParams[6] = new SqlParameter("@category", category);
                objParams[7] = new SqlParameter("@startdate", dtStartDate);
                objParams[8] = new SqlParameter("@enddate", dtEndDate);
                objParams[9] = new SqlParameter("@SiteIDList", SiteIDList);


                SqlHelper.FillDataset(roleManager.Connection.ConnectionString, "rsp_Report_BankingReport_PerInstallation",
                                        dsInstallationWinLoss, new string[] { "InstallationWinLossDetails" }, objParams);

                return dsInstallationWinLoss;

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return null;
            }
        }


        public DataSet GetDailyAccoutingDetails(int ncompany, int nsubCompany, int region,
           int area, int district, int nSite, int category, string dtStartDate, string dtEndDate, string SiteIDList)
        {
            DataSet dsInstallationWinLoss = new DataSet();

            try
            {
                SqlParameter[] objParams = new SqlParameter[9];


                objParams[0] = new SqlParameter("@subcompany", nsubCompany);
                objParams[1] = new SqlParameter("@region", region);
                objParams[2] = new SqlParameter("@area", area);
                objParams[3] = new SqlParameter("@district", district);
                objParams[4] = new SqlParameter("@site", nSite);
                objParams[5] = new SqlParameter("@startdate", dtStartDate);
                objParams[6] = new SqlParameter("@enddate", dtEndDate);
                objParams[7] = new SqlParameter("@company", ncompany);
                objParams[8] = new SqlParameter("@SiteIDList", SiteIDList);


                SqlHelper.FillDataset(roleManager.Connection.ConnectionString, "rsp_REPORT_DailyAccounting_PerInstallation",
                                        dsInstallationWinLoss, new string[] { "DailyAccoutingPerInstallation" }, objParams);

                return dsInstallationWinLoss;

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return null;
            }
        }


        public DataSet getEFTSlotActivityCumulative(int nCompany, int nSubCompany, int nSite, string dtStartDate, string dtEndDate, string SiteIDList)
        {
            DataSet dsEFTSlotActivityCumulative = new DataSet();

            try
            {
                SqlParameter[] objParams = new SqlParameter[6];

                objParams[0] = new SqlParameter("@company", nCompany);
                objParams[1] = new SqlParameter("@subcompany", nSubCompany);
                objParams[2] = new SqlParameter("@site", nSite);
                objParams[3] = new SqlParameter("@startdate", dtStartDate);
                objParams[4] = new SqlParameter("@enddate", dtEndDate);
                objParams[5] = new SqlParameter("@SiteIDList", SiteIDList);


                SqlHelper.FillDataset(roleManager.Connection.ConnectionString, "rsp_Report_EFTSlotActivity",
                                        dsEFTSlotActivityCumulative, new string[] { "EFTSlotActivityCumulative" }, objParams);

                return dsEFTSlotActivityCumulative;

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return null;
            }
        }
        public DataSet GetAssetDetails(int CompanyId, int MachineStatusFlag)
        {
            DataSet dsAssetDetails = new DataSet();

            try
            {
                SqlParameter[] objParams = new SqlParameter[2];

                objParams[0] = new SqlParameter("company", CompanyId);
                objParams[1] = new SqlParameter("StockStatus", MachineStatusFlag);

                dsAssetDetails = SqlHelper.ExecuteDataset(roleManager.Connection.ConnectionString, CommandType.StoredProcedure, "rsp_GetAssetDetailsReport", objParams);
                return dsAssetDetails;

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return null;
            }
        }


        public DataSet getMGMDByGamingDeviceCabinetReport(int ncompany, int nsubCompany, int nSite, int zone, string dtGamingDate, string sPeriod)
        {
            DataSet dsMGMDByGamingDeviceCabinetReport = new DataSet();

            try
            {
                SqlParameter[] objParams = new SqlParameter[7];

                objParams[0] = new SqlParameter("@company", ncompany);
                objParams[1] = new SqlParameter("@subcompany", nsubCompany);
                objParams[2] = new SqlParameter("@site", nSite);
                objParams[3] = new SqlParameter("@zone", zone);
                objParams[4] = new SqlParameter("@gamingdate", dtGamingDate);
                objParams[5] = new SqlParameter("@Period", sPeriod);
                objParams[6] = new SqlParameter("@GROUPBYZONE", false);

                SqlHelper.FillDataset(roleManager.Connection.ConnectionString, "rsp_Report_MGMDByGamingDeviceCabinetReport",
                                        dsMGMDByGamingDeviceCabinetReport, new string[] { "MGMDByGamingDeviceCabinetReport" }, objParams);

                return dsMGMDByGamingDeviceCabinetReport;

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return null;
            }
        }
        public DataSet getMGMDSummaryDetails(int nCompany, int nSubCompany, int nSite, int nZone, string dtgamingDate, string Period, string SiteIDList)
        {
            DataSet dsMGMDSummaryDetails = new DataSet();

            try
            {
                SqlParameter[] objParams = new SqlParameter[8];

                objParams[0] = new SqlParameter("@company", nCompany);
                objParams[1] = new SqlParameter("@subcompany", nSubCompany);
                objParams[2] = new SqlParameter("@site", nSite);
                objParams[3] = new SqlParameter("@Zone", @nZone);
                objParams[4] = new SqlParameter("@gamingdate", dtgamingDate);
                objParams[5] = new SqlParameter("@Period", Period);
                objParams[6] = new SqlParameter("@GROUPBYZONE",false);
                objParams[7] = new SqlParameter("@SiteIDList", SiteIDList);


                SqlHelper.FillDataset(roleManager.Connection.ConnectionString, "rsp_Report_MGMD_SummaryAnalysis1",
                                        dsMGMDSummaryDetails, new string[] { "MGMDSummaryAnalysis" }, objParams);

                return dsMGMDSummaryDetails;

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return null;
            }
        }

        public DataSet getManufacturerPerformanceReport(int ncompany, int nsubCompany, int nSite, string dtGamingDate, string sPeriod, string SiteIDList)
        {
            DataSet dsManufacturerPerformanceReport = new DataSet();

            try
            {
                SqlParameter[] objParams = new SqlParameter[6];

                objParams[0] = new SqlParameter("@company", ncompany);
                objParams[1] = new SqlParameter("@subcompany", nsubCompany);
                objParams[2] = new SqlParameter("@site", nSite);
                objParams[3] = new SqlParameter("@gamingdate", dtGamingDate);
                objParams[4] = new SqlParameter("@Period", sPeriod);
                objParams[5] = new SqlParameter("@SiteIDList", SiteIDList);

                SqlHelper.FillDataset(roleManager.Connection.ConnectionString, "rsp_Report_ManufacturerPerformanceReport",
                                        dsManufacturerPerformanceReport, new string[] { "ManufacturerPerformanceReport" }, objParams);

                return dsManufacturerPerformanceReport;

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return null;
            }
        }
        public DataSet GetLiabilityTransferDetailsReport(int Company, int SubCompany, int Zone, int Site, DateTime startDate, DateTime endDate,string sSiteIDList)
        {
            DataSet dsLiabilityTransferdetails = new DataSet();

            try
            {
                SqlParameter[] objParams = new SqlParameter[6];
                objParams[0] = new SqlParameter("@COMPANY", Company);
                objParams[1] = new SqlParameter("@SUBCOMPANY", SubCompany);
                objParams[2] = new SqlParameter("@SITE", Site);
                objParams[3] = new SqlParameter("@STARTDATE", startDate);
                objParams[4] = new SqlParameter("@ENDDATE", endDate);
                objParams[5] = new SqlParameter("@SiteIDList", sSiteIDList);
                //objParams[5] = new SqlParameter("@ZONE", Zone);


                SqlHelper.FillDataset(roleManager.Connection.ConnectionString, "RSP_GETREPORTLIABILITYTRANSFERSDETAILS", dsLiabilityTransferdetails, new string[] { "LiabilityTransferDetails" }, objParams);

                return dsLiabilityTransferdetails;

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return null;
            }
        }

        public DataSet GetCrossPropertyTicketAnalysisReport(int Company, int SubCompany, int Zone, int Site, DateTime startDate, DateTime endDate,string sSiteIDList)
        {
            DataSet dsTicketAnalysis = new DataSet();

            try
            {
                SqlParameter[] objParams = new SqlParameter[6];
                objParams[0] = new SqlParameter("@COMPANY", Company);
                objParams[1] = new SqlParameter("@SUBCOMPANY", SubCompany);
                objParams[2] = new SqlParameter("@SITE", Site);
                objParams[3] = new SqlParameter("@STARTDATE", startDate);
                objParams[4] = new SqlParameter("@ENDDATE", endDate);
                objParams[5] = new SqlParameter("@SiteIDList", sSiteIDList);
                //objParams[5] = new SqlParameter("@ZONE", Zone);

                SqlHelper.FillDataset(roleManager.Connection.ConnectionString, "rsp_getCrossPropertyTicketAnalysis", dsTicketAnalysis, new string[] { "TicketAnalysisReport" }, objParams);

                return dsTicketAnalysis;

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return null;
            }
        }
        public DataSet GetWinComparisonReport(int Company, int SubCompany, int Zone, int Site, DateTime GamingDate, bool IncludeNonCashable, bool UsePhysicalWin, string Slot, string Period, string SiteIDList, ref bool isSuccess)
        {
            DataSet dsWinComparison = new DataSet();

            try
            {
                SqlParameter[] objParams = new SqlParameter[10];

                objParams[0] = new SqlParameter("@company", Company);
                objParams[1] = new SqlParameter("@subcompany", SubCompany);
                objParams[2] = new SqlParameter("@site", Site);
                objParams[3] = new SqlParameter("@zone", Zone);
                objParams[4] = new SqlParameter("@GameIdentifier", Slot);
                objParams[5] = new SqlParameter("@gamingdate", GamingDate);
                objParams[6] = new SqlParameter("@IncludeNonCashable", IncludeNonCashable);
                objParams[7] = new SqlParameter("@UsePhysicalWin", UsePhysicalWin);
                objParams[8] = new SqlParameter("@Period", Period);
                objParams[9] = new SqlParameter("@SiteIDList", SiteIDList);

                SqlHelper.FillDataset(roleManager.Connection.ConnectionString, "rsp_Report_WinComparison", dsWinComparison, new string[] { "WinComparisonReport" }, 0, objParams);
                return dsWinComparison;

            }
            catch (Exception ex)
            {
                isSuccess = false;
                ExceptionManager.Publish(ex);
                return null;
            }
        }

    public  DataSet GetDeclarationVouchersDetails(int Company, int SubCompany, int Site, string Slot, DateTime startDate, DateTime endDate)
    {
        DataSet dsDeclarationVouchers = new DataSet();

        try
        {
            SqlParameter[] objParams = new SqlParameter[6];

            objParams[0] = new SqlParameter("@company", Company);
            objParams[1] = new SqlParameter("@subcompany", SubCompany);
            objParams[2] = new SqlParameter("@site", Site);
            objParams[3] = new SqlParameter("@Slot", Slot);
            objParams[4] = new SqlParameter("@startdate", startDate);
            objParams[5] = new SqlParameter("@enddate", endDate);


            SqlHelper.FillDataset(roleManager.Connection.ConnectionString, "rsp_CheckForDeclaredVouchers", dsDeclarationVouchers, new string[] { "DeclarationBatchVouchersDetailsReport" }, objParams);

            return dsDeclarationVouchers;

        }
        catch (Exception ex)
        {
            ExceptionManager.Publish(ex);
            return null;
        }
    }
  public DataSet GetVoucherCouponLiabilityReport(int nCompany, int nSubCompany, int nSite, int nZone, DateTime dtIssueDate, string strVoucherStatus, string sDeviceType,string sSiteIDList)
  {

      DataSet dsLiability = new DataSet();

      try
      {
          SqlParameter[] objParams = new SqlParameter[9];
          objParams[0] = new SqlParameter("@COMPANY", nCompany);
          objParams[1] = new SqlParameter("@SUBCOMPANY", nSubCompany);
          objParams[2] = new SqlParameter("@SITE", nSite);
          objParams[3] = new SqlParameter("@zone", nZone);

          objParams[4] = new SqlParameter("@ISSUEDATE", dtIssueDate);
          objParams[5] = new SqlParameter("@VoucherStatus", strVoucherStatus);
          objParams[6] = new SqlParameter("@DeviceType", sDeviceType);
          objParams[7] = new SqlParameter("@GROUPBYZONE", false);
          objParams[8] = new SqlParameter("@SiteIDList", sSiteIDList);



          SqlHelper.FillDataset(roleManager.Connection.ConnectionString, "rsp_GetTicketLiabilityReport", dsLiability, new string[] { "VoucherLiabilityReport" }, objParams);

          return dsLiability;

      }
      catch (Exception ex)
      {
          ExceptionManager.Publish(ex);
          return null;
      }
  }

  public DataSet GetStackerDetails(int Company, int SubCompany, int Area, int Region, int Site, int District, int StackerLevel, string SiteIDList)
  {
      DataSet dsStacker = new DataSet();

      try
      {
          SqlParameter[] objParams = new SqlParameter[8];
          objParams[0] = new SqlParameter("@Company", Company);
          objParams[1] = new SqlParameter("@Subcompany", SubCompany);
          objParams[2] = new SqlParameter("@Region", Region);
          objParams[3] = new SqlParameter("@Area", Area);
          objParams[4] = new SqlParameter("@District", District);          
          objParams[5] = new SqlParameter("@Site", Site);
          objParams[6] = new SqlParameter("@StackerLevel", StackerLevel);
          objParams[7] = new SqlParameter("@SiteIDList", SiteIDList);
          SqlHelper.FillDataset(roleManager.Connection.ConnectionString, "rsp_StackerLevelDetails", dsStacker, new string[] { "StackerLevelDetails" }, objParams);

          return dsStacker;
           
      }
      catch (Exception ex)
      {
          ExceptionManager.Publish(ex);
          return null;
      }
  }


        //GetDropScheduleDetails
  public DataSet GetDropScheduleDetails(int Company, int SubCompany, int Area, int Region, int Site, int District, int StackerLevel, string SiteIDList)
        {
            DataSet dsStacker = new DataSet();

            try
            {
                SqlParameter[] objParams = new SqlParameter[8];
                objParams[0] = new SqlParameter("@company", Company);
                objParams[1] = new SqlParameter("@subcompany", SubCompany);
                objParams[2] = new SqlParameter("@Region", Region);
                objParams[3] = new SqlParameter("@Area", Area);
                objParams[4] = new SqlParameter("@District", District);
                objParams[5] = new SqlParameter("@site", Site);
                objParams[6] = new SqlParameter("@StackerLevel", StackerLevel);
                objParams[7] = new SqlParameter("@SiteIDList", SiteIDList);
                SqlHelper.FillDataset(roleManager.Connection.ConnectionString, "rsp_StackerLevelDetails", dsStacker, new string[] { "DropScheduleDetails" }, objParams);
                return dsStacker;

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return null;
            }
        }

  public DataSet GetEmployeeCardSessionsDetails(int Company, int SubCompany, int Region, int Area, int District, int Site, int EmpID, string EmpCardID, DateTime StartDate, DateTime EndDate, string SiteIDList)
  {
      DataSet dsEmpCard = new DataSet();

      try
      {
          SqlParameter[] objParams = new SqlParameter[11];
          objParams[0] = new SqlParameter("@Company", Company);
          objParams[1] = new SqlParameter("@Subcompany", SubCompany);
          objParams[2] = new SqlParameter("@Region", Region);
          objParams[3] = new SqlParameter("@Area", Area);
          objParams[4] = new SqlParameter("@District", District);
          objParams[5] = new SqlParameter("@Site", Site);
          objParams[6] = new SqlParameter("@UserID", EmpID);
          objParams[7] = new SqlParameter("@EmpCardID", EmpCardID);
          objParams[8] = new SqlParameter("@Startdate", StartDate);
          objParams[9] = new SqlParameter("@Enddate", EndDate);
          objParams[10] = new SqlParameter("@SiteIDList", SiteIDList);

          SqlHelper.FillDataset(roleManager.Connection.ConnectionString, "rsp_GetEmployeeCardSessions", dsEmpCard, new string[] { "EmployeeCardSessionsReport" }, objParams);
          return dsEmpCard;
      }
      catch (Exception ex)
      {
          ExceptionManager.Publish(ex);
          return null;
      }
  }

  public DataSet GetEmployeeCardListDetails(int CardNumber, string EmpName, string CardType, string CardStatus)
  {
      DataSet dsEmpCardList = new DataSet();
      try
      {
          SqlParameter[] objParams = new SqlParameter[4];
          objParams[0] = new SqlParameter("@CardNumber", CardNumber);
          objParams[1] = new SqlParameter("@EmployeeName", EmpName);
          objParams[2] = new SqlParameter("@CardType", CardType);
          objParams[3] = new SqlParameter("@CardStatus", CardStatus);
          

          SqlHelper.FillDataset(roleManager.Connection.ConnectionString, "rsp_GetEmployeeCardList", dsEmpCardList, new string[] { "EmployeeCardListReport" }, objParams);
          return dsEmpCardList;
      }
      catch (Exception ex)
      {
          ExceptionManager.Publish(ex);
          return null;
      }
  }  
  
  public int GetAccountingWinLossDetails(int nCompany, int nSubCompany, int region, int area, int district, int nSite, int zone, int Category, string dtStartDate, string dtEndDate, string SiteIDList)
  {
      DataSet dsAccWinLossDetails = new DataSet();

      try
      {
          SqlParameter[] objParams = new SqlParameter[11];

          objParams[0] = new SqlParameter("@company", nCompany);
          objParams[1] = new SqlParameter("@subcompany", nSubCompany);
          objParams[2] = new SqlParameter("@region", region);
          objParams[3] = new SqlParameter("@area", area);
          objParams[4] = new SqlParameter("@district", district);
          objParams[5] = new SqlParameter("@site", nSite);
          objParams[6] = new SqlParameter("@zone", zone);
          objParams[7] = new SqlParameter("@category", Category);
          objParams[8] = new SqlParameter("@startdate", dtStartDate);
          objParams[9] = new SqlParameter("@enddate", dtEndDate);
          objParams[10] = new SqlParameter("@SiteIDList", SiteIDList);

          object count = SqlHelper.ExecuteScalar(roleManager.Connection.ConnectionString,CommandType.StoredProcedure,
              "rsp_Report_CheckAccountingWinlossRecordExists", objParams);

          return Convert.ToInt32(count);

      }
      catch (Exception ex)
      {
          ExceptionManager.Publish(ex);
          return 0;
      }
  }
//SP8 Base Starts

  public DataSet GetTotalFundsInSummary(int Company, int SubCompany, int Site, string SiteIDList)
  {
      DataSet dsTotalFundsIn = new DataSet();

      try
      {
          SqlParameter[] objParams = new SqlParameter[4];
          objParams[0] = new SqlParameter("@Company", Company);
          objParams[1] = new SqlParameter("@SubCompany", SubCompany);
          objParams[2] = new SqlParameter("@Site", Site);
          objParams[3] = new SqlParameter("@SiteIDList", SiteIDList);

          SqlHelper.FillDataset(roleManager.Connection.ConnectionString, "rsp_Report_TotalFundsIn", dsTotalFundsIn, new string[] { "TotalFundsIn" }, objParams);
          return dsTotalFundsIn;

      }
      catch (Exception ex)
      {
          ExceptionManager.Publish(ex);
          return null;
      }
  }

  public DataSet GetTotalFundsInDetails(int Company, int SubCompany, int Site, int Zone, string SiteIDList)
  {
      DataSet dsTotalFundsIn = new DataSet();

      try
      {
          SqlParameter[] objParams = new SqlParameter[5];
          objParams[0] = new SqlParameter("@Company", Company);
          objParams[1] = new SqlParameter("@SubCompany", SubCompany);
          objParams[2] = new SqlParameter("@Site", Site);
          objParams[3] = new SqlParameter("@Zone", Zone);
          objParams[4] = new SqlParameter("@SiteIDList", SiteIDList);

          SqlHelper.FillDataset(roleManager.Connection.ConnectionString, "rsp_Report_TotalFundsInDetails", dsTotalFundsIn, new string[] { "TotalFundsInDetails" }, objParams);
          return dsTotalFundsIn;

      }
      catch (Exception ex)
      {
          ExceptionManager.Publish(ex);
          return null;
      }
  }
  public DataSet GetVaultBalanceDetails(int Company, int SubCompany, int Site, string SiteIDList)
  {
      DataSet dsVaultBalance = new DataSet();

      try
      {
          SqlParameter[] objParams = new SqlParameter[4];
          objParams[0] = new SqlParameter("@COMPANY", Company);
          objParams[1] = new SqlParameter("@SUBCOMPANY", SubCompany);
          objParams[2] = new SqlParameter("@SITE", Site);
          objParams[3] = new SqlParameter("@SiteIDList", SiteIDList);

          SqlHelper.FillDataset(roleManager.Connection.ConnectionString, "rsp_Report_VaultBalanceReport", dsVaultBalance, new string[] { "VaultBalanceReport" }, objParams);

          return dsVaultBalance;

      }
      catch (Exception ex)
      {
          ExceptionManager.Publish(ex);
          return null;
      }
  }
  public DataSet GetLiquidationExpenseDetails(int Company, int SubCompany, int Region, int Area, int District, int Site, string dtStartDate, string dtEndDate, string SiteIDList)
{
    DataSet dsLiquidationExpense = new DataSet();

    try
    {
        SqlParameter[] objParams = new SqlParameter[9];
        objParams[0] = new SqlParameter("@company", Company);
        objParams[1] = new SqlParameter("@subcompany", SubCompany);
        objParams[2] = new SqlParameter("@region", Region);
        objParams[3] = new SqlParameter("@area", Area);
        objParams[4] = new SqlParameter("@district", District);
        objParams[5] = new SqlParameter("@site", Site);
        objParams[6] = new SqlParameter("@StartDate", dtStartDate);
        objParams[7] = new SqlParameter("@EndDate", dtEndDate);
        objParams[8] = new SqlParameter("@SiteIDList", SiteIDList);

        SqlHelper.FillDataset(roleManager.Connection.ConnectionString, "rsp_LiquidationExpenseReport", dsLiquidationExpense, new string[] { "LiquidationExpenseReport" }, objParams);

        return dsLiquidationExpense;

    }
    catch (Exception ex)
    {
        ExceptionManager.Publish(ex);
        return null;
    }
}

  public DataSet GetPeriodEndLiquidationRevenueDetails(int Company, int SubCompany, int Region, int Area, int District, int Site, string dtStartDate, string dtEndDate, string SiteIDList)
{
    DataSet dsPeriodEndLiquidationRevenue = new DataSet();

    try
    {
        SqlParameter[] objParams = new SqlParameter[9];
        objParams[0] = new SqlParameter("@company", Company);
        objParams[1] = new SqlParameter("@subcompany", SubCompany);
        objParams[2] = new SqlParameter("@region", Region);
        objParams[3] = new SqlParameter("@area", Area);
        objParams[4] = new SqlParameter("@district", District);
        objParams[5] = new SqlParameter("@site", Site);
        objParams[6] = new SqlParameter("@Startdate", dtStartDate);
        objParams[7] = new SqlParameter("@Enddate", dtEndDate);
        objParams[8] = new SqlParameter("@SiteIDList", SiteIDList);
        SqlHelper.FillDataset(roleManager.Connection.ConnectionString, "rsp_PeriodEndLiquidationRevenueReport", dsPeriodEndLiquidationRevenue, new string[] { "PeriodEndLiquidationRevenueReport" }, objParams);

        return dsPeriodEndLiquidationRevenue;

    }
    catch (Exception ex)
    {
        ExceptionManager.Publish(ex);
        return null;
    }
}



public DataSet GetPromoSummaryDetails(int Company, int SubCompany,int Region, int Area, int District, int Site, string StartDate, string EndDate,string sSiteIDList)
{
    DataSet dsPromoDetails = new DataSet();

    try
    {
        SqlParameter[] objParams = new SqlParameter[9];
        objParams[0] = new SqlParameter("@COMPANY", Company);
        objParams[1] = new SqlParameter("@SUBCOMPANY", SubCompany);
        objParams[2] = new SqlParameter("@REGION", Region);
        objParams[3] = new SqlParameter("@AREA", Area);
        objParams[4] = new SqlParameter("@DISTRICT", District);
        objParams[5] = new SqlParameter("@SITE", Site);
        objParams[6] = new SqlParameter("@STARTDATE", StartDate);
        objParams[7] = new SqlParameter("@ENDDATE", EndDate);
        objParams[8] = new SqlParameter("@SiteIDList", sSiteIDList);

        SqlHelper.FillDataset(roleManager.Connection.ConnectionString, "rsp_SelectPromotionHistoryReport", dsPromoDetails, new string[] { "PromoSummaryReport" }, objParams);

        return dsPromoDetails;

    }
    catch (Exception ex)
    {
        ExceptionManager.Publish(ex);
        return null;
    }
}       
//(Ep4 Changes)
public DataSet GetCashDispDrop(int Company, int SubCompany, int Site, string Status, bool IncludeZero, int UserID, string StartDate, string EndDate, string SiteIDList)
{
    DataSet dsCashDispDrop = new DataSet();
    try
    {
        SqlParameter[] objParams = new SqlParameter[9];
        objParams[0] = new SqlParameter("@Company", Company);
        objParams[1] = new SqlParameter("@Subcompany", SubCompany);
        objParams[2] = new SqlParameter("@Site", Site);
        objParams[3] = new SqlParameter("@VaultStatus", Status);
        objParams[4] = new SqlParameter("@IncludeZero", IncludeZero);
        objParams[5] = new SqlParameter("@UserID", UserID);
        objParams[6] = new SqlParameter("@StartDate", StartDate);
        objParams[7] = new SqlParameter("@EndDate", EndDate);
        objParams[8] = new SqlParameter("@SiteIDList", SiteIDList);

        SqlHelper.FillDataset(roleManager.Connection.ConnectionString, "rsp_Report_VaultDrop", dsCashDispDrop, new string[] { "CashDispenserDrop" }, objParams);
        return dsCashDispDrop;
    }
    catch (Exception ex)
    {
        ExceptionManager.Publish(ex);
        return null;
    }
}

public DataSet GetCashDispVariance(int Company, int SubCompany, int Site, string Status, bool IncludeZero, string StartDate, string EndDate, int UserID, string SiteIDList)
{
    DataSet dsCashDispVar = new DataSet();
    try
    {
        SqlParameter[] objParams = new SqlParameter[9];
        objParams[0] = new SqlParameter("@Company", Company);
        objParams[1] = new SqlParameter("@Subcompany", SubCompany);
        objParams[2] = new SqlParameter("@Site", Site);
        objParams[3] = new SqlParameter("@VaultStatus", Status);
        objParams[4] = new SqlParameter("@IncludeZero", IncludeZero);
        objParams[5] = new SqlParameter("@StartDate", StartDate);
        objParams[6] = new SqlParameter("@EndDate", EndDate);
        objParams[7] = new SqlParameter("@UserID", UserID);
        objParams[8] = new SqlParameter("@SiteIDList", SiteIDList);

        SqlHelper.FillDataset(roleManager.Connection.ConnectionString, "rsp_Report_VaultVariance", dsCashDispVar, new string[] { "CashDispenserVariance" }, objParams);
        return dsCashDispVar;
    }
    catch (Exception ex)
    {
        ExceptionManager.Publish(ex);
        return null;
    }
}

public DataSet GetVaultConfigurationDetails(int Company, int SubCompany, int Site, string VaultStatus, int UserID)
{
    DataSet dsVaultConfig = new DataSet();

    try
    {
        SqlParameter[] objParams = new SqlParameter[5];
        objParams[0] = new SqlParameter("@COMPANY", Company);
        objParams[1] = new SqlParameter("@SUBCOMPANY", SubCompany);
        objParams[2] = new SqlParameter("@SITE", Site);
        objParams[3] = new SqlParameter("@userID", UserID);
        objParams[4] = new SqlParameter("@VaultStatus", VaultStatus);

        SqlHelper.FillDataset(roleManager.Connection.ConnectionString, "rsp_Report_VaultConfigDetails", dsVaultConfig, new string[] { "VaultConfigReport" }, objParams);

        return dsVaultConfig;

    }
    catch (Exception ex)
    {
        ExceptionManager.Publish(ex);
        return null;
    }
}

public DataSet GetCashDispenserAccountingReport(int company, int subcompany, int site, DateTime startdate, DateTime enddate, int userid, string SiteIDList)
{
    DataSet dsCashDispenserAccounting = new DataSet();
    try
    {
        SqlParameter[] objParams = new SqlParameter[7];
        objParams[0] = new SqlParameter("@Company", company);
        objParams[1] = new SqlParameter("@Subcompany", subcompany);
        objParams[2] = new SqlParameter("@Site", site);
        objParams[3] = new SqlParameter("@Startdate", startdate);
        objParams[4] = new SqlParameter("@Enddate", enddate);
        objParams[5] = new SqlParameter("@UserID", userid);
        objParams[6] = new SqlParameter("@SiteIDList", SiteIDList);

        SqlHelper.FillDataset(roleManager.Connection.ConnectionString, "rsp_Report_VaultTransactions", dsCashDispenserAccounting, new string[] { "CashDispenserAccountingReport" }, objParams);
        return dsCashDispenserAccounting;
    }
    catch(Exception ex)
    {
        ExceptionManager.Publish(ex);
        return null;
    }
}
public DataSet GetCashDispTransaction(int Company, int SubCompany, int Site, string TransType, string StartDate, string EndDate, int UserID, string SiteIDList)
{
    DataSet dsCashDispTrans = new DataSet();

    try
    {
        SqlParameter[] objParams = new SqlParameter[8];
        objParams[0] = new SqlParameter("@COMPANY", Company);
        objParams[1] = new SqlParameter("@SUBCOMPANY", SubCompany);
        objParams[2] = new SqlParameter("@SITE", Site);
        objParams[3] = new SqlParameter("@TransactionType", TransType);
        objParams[4] = new SqlParameter("@StartDate", StartDate);
        objParams[5] = new SqlParameter("@EndDate", EndDate);
        objParams[6] = new SqlParameter("@UserID", UserID);
        objParams[7] = new SqlParameter("@SiteIDList", SiteIDList);

        SqlHelper.FillDataset(roleManager.Connection.ConnectionString, "rsp_Report_VaultTransactionsDetails", dsCashDispTrans, new string[] { "CashDispTrans" }, objParams);
        return dsCashDispTrans;
    }
    catch (Exception ex)
    {
        ExceptionManager.Publish(ex);
        return null;
    }
}

public DataSet GetCashDispLevelDetails(int Company, int SubCompany, int Region, int Area, int District, int Site, int InventoryLevel, string InventoryType, int UserID, string SiteIDList)
{
    DataSet dsGetCashDispLevel = new DataSet();
    try
    {
        SqlParameter[] objParams = new SqlParameter[10];
        objParams[0] = new SqlParameter("@Company", Company);
        objParams[1] = new SqlParameter("@SubCompany", SubCompany);
        objParams[2] = new SqlParameter("@Region", Region);
        objParams[3] = new SqlParameter("@Area", Area);
        objParams[4] = new SqlParameter("@District", District);
        objParams[5] = new SqlParameter("@Site", Site);
        objParams[6] = new SqlParameter("@InventoryLevel", InventoryLevel);
        objParams[7] = new SqlParameter("@InventoryType", InventoryType);
        objParams[8] = new SqlParameter("@UserID", UserID);
        objParams[9] = new SqlParameter("@SiteIDList", SiteIDList);

        SqlHelper.FillDataset(roleManager.Connection.ConnectionString, "rsp_Report_VaultLevelDetails", dsGetCashDispLevel, new string[] { "CashDispLevel" }, objParams);
        return dsGetCashDispLevel;
    }
    catch (Exception ex)
    {
        ExceptionManager.Publish(ex);
        return null;
    }
}
public DataSet GetCashDispCassettesInventoryStatus(int Company, int SubCompany, int Site, int UserID, string SiteIDList)
{
    DataSet dsGetCashDispCassettesStatus = new DataSet();
    try
    {
        SqlParameter[] objparams = new SqlParameter[5];
        objparams[0] = new SqlParameter("@Company", Company);
        objparams[1] = new SqlParameter("@SubCompany", SubCompany);
        objparams[2] = new SqlParameter("@Site", Site);        
        objparams[3] = new SqlParameter("@UserID", UserID);
        objparams[4] = new SqlParameter("@SiteIDList", SiteIDList);

        SqlHelper.FillDataset(roleManager.Connection.ConnectionString, "rsp_Report_VaultCassettesInventoryStatus", dsGetCashDispCassettesStatus, new string[] {"CashDispCassette"}, objparams);
        return dsGetCashDispCassettesStatus;        
    }
    catch (Exception ex)
    {
        ExceptionManager.Publish(ex);
        return null;
    }        
}
//EP4 Changes Ends
public DataSet GetCashDispCassettesAccountingDetail(int Company, int SubCompany, int Site, string StartDate, string EndDate, int UserID, string SiteIDList)
{
    DataSet dsGetCashDispCassettesAccDetail = new DataSet();
    try
    {
        SqlParameter[] objparams = new SqlParameter[7];
        objparams[0] = new SqlParameter("@Company", Company);
        objparams[1] = new SqlParameter("@SubCompany", SubCompany);
        objparams[2] = new SqlParameter("@Site", Site);        
        objparams[3] = new SqlParameter("@StartDate", StartDate);
        objparams[4] = new SqlParameter("@EndDate", EndDate);
        objparams[5] = new SqlParameter("@UserID", UserID);
        objparams[6] = new SqlParameter("@SiteIDList", SiteIDList);

        SqlHelper.FillDataset(roleManager.Connection.ConnectionString, "rsp_Report_VaultCassetteTransactions", dsGetCashDispCassettesAccDetail, new string[] { "CashDispCassAcc" }, objparams);
        return dsGetCashDispCassettesAccDetail;
    }
    catch (Exception ex)
    {
        ExceptionManager.Publish(ex);
        return null;
    }
}

 }
}