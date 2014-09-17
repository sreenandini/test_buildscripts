using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Data;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Linq.Expressions;
using System.ComponentModel;
using System;
using System.Data.SqlClient;
using BMC.DataAccess;
using BMC.Common.ExceptionManagement;
using BMC.Common.LogManagement;

namespace BMCCashierTransactions
{
    [System.Data.Linq.Mapping.DatabaseAttribute(Name = "Enterprise")]
    public partial class CDMDataContext : System.Data.Linq.DataContext
    {

        private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();

        #region Extensibility Method Definitions
        partial void OnCreated();
        #endregion

        public CDMDataContext() :
            base(global::BMCCashierTransactions.Properties.Settings.Default.EnterpriseConnectionString, mappingSource)
        {
           // OnCreated();
            this.Connection.ConnectionString = CashDeskManagerDataAccess.GetConnectionString();
            
        }

        [Function(Name = "dbo.rsp_GetCompanyDetails")]
        public ISingleResult<Company> GetCompanyDetails()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<Company>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetSubCompanyDetails")]
        public ISingleResult<SubCompany> GetSubCompanyDetails([Parameter(DbType = "Int")] System.Nullable<int> company)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), company);
            return ((ISingleResult<SubCompany>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetSites")]
        public ISingleResult<Site> GetSiteDetails([Parameter(DbType = "Int")] System.Nullable<int> subcompany, [Parameter(DbType = "Int")] System.Nullable<int> region, [Parameter(DbType = "Int")] System.Nullable<int> area)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), subcompany, region, area);
            return ((ISingleResult<Site>)(result.ReturnValue));
        }


        [ResultType(typeof(SProcResult))]
        [Function(Name = "dbo.rsp_GetVersion")]
        public ISingleResult<SProcResult> GetBMCVersion()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<SProcResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetSite_Code_Name")]
        public ISingleResult<SiteDetails> GetSite_Code_Name([Parameter(Name = "SiteID", DbType = "Int")] System.Nullable<int> siteID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), siteID);
            return ((ISingleResult<SiteDetails>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetRegion_Site")]
        public ISingleResult<Region> GetRegion_Site([Parameter(Name = "Site", DbType = "Int")] System.Nullable<int> site)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), site);
            return ((ISingleResult<Region>)(result.ReturnValue));
        }

        [ResultType(typeof(SiteCultureInfo))]
        [Function(Name = "dbo.rsp_GetSiteCulture")]
        public ISingleResult<SiteCultureInfo> GetSiteCulture([Parameter(Name = "UserId", DbType = "Int")] System.Nullable<int> userId)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), userId);
            return ((ISingleResult<SiteCultureInfo>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetCashierTransactions")]
        public ISingleResult<CashierTransactions> GetCashierTransactions(
            [Parameter(DbType = "DateTime")] System.Nullable<System.DateTime> startdate,
            [Parameter(DbType = "DateTime")] System.Nullable<System.DateTime> enddate,
            [Parameter(Name = "SITE", DbType = "Int")] System.Nullable<int> sITE,
            [Parameter(Name = "Route_No", DbType = "Int")] System.Nullable<int> Route_No,
            [Parameter(Name = "User_No", DbType = "Int")] System.Nullable<int> User_No)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), startdate, enddate, sITE, Route_No, User_No);
            return ((ISingleResult<CashierTransactions>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_CRMGetRoutesBySiteID")]
        public ISingleResult<CRMGetRoutesBySiteID> CRMGetRoutesBySiteID([Parameter(Name = "Site_ID", DbType = "Int")] System.Nullable<int> site_ID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), site_ID);
            return ((ISingleResult<CRMGetRoutesBySiteID>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_CRMGetActiveRoutesBySiteID")]
        public ISingleResult<CRMGetRoutesBySiteID> CRMGetActiveRoutesBySiteID([Parameter(Name = "Site_ID", DbType = "Int")] System.Nullable<int> site_ID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), site_ID);
            return ((ISingleResult<CRMGetRoutesBySiteID>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_CRMGetRoutedAssetsByRoute")]
        public ISingleResult<CRMGetRoutedAssetsByRoute> CRMGetRoutedAssetsByRoute([Parameter(Name = "Route_No", DbType = "Int")] System.Nullable<int> route_No)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), route_No);
            return ((ISingleResult<CRMGetRoutedAssetsByRoute>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetCashierTransactionSettings")]
        public ISingleResult<rsp_GetCashierTransactionSettingsResult> GetInitialSettings()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<rsp_GetCashierTransactionSettingsResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetUserDetailsBySite")]
        public ISingleResult<UserDetailsBySiteResult> GetUserDetailsBySite([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Site_No", DbType = "Int")] System.Nullable<int> site_No)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), site_No);
            return ((ISingleResult<UserDetailsBySiteResult>)(result.ReturnValue));
        }
       
    }

    public partial class Company
    {

        private int _Company_ID;

        private string _Company_Name;

        public Company()
        {
        }

        [Column(Storage = "_Company_ID", DbType = "Int NOT NULL")]
        public int Company_ID
        {
            get
            {
                return this._Company_ID;
            }
            set
            {
                if ((this._Company_ID != value))
                {
                    this._Company_ID = value;
                }
            }
        }

        [Column(Storage = "_Company_Name", DbType = "VarChar(50)")]
        public string Company_Name
        {
            get
            {
                return this._Company_Name;
            }
            set
            {
                if ((this._Company_Name != value))
                {
                    this._Company_Name = value;
                }
            }
        }
    }

    public partial class SubCompany
    {

        private int _Sub_Company_ID;

        private string _Sub_Company_Name;

        public SubCompany()
        {
        }

        [Column(Storage = "_Sub_Company_ID", DbType = "Int NOT NULL")]
        public int Sub_Company_ID
        {
            get
            {
                return this._Sub_Company_ID;
            }
            set
            {
                if ((this._Sub_Company_ID != value))
                {
                    this._Sub_Company_ID = value;
                }
            }
        }

        [Column(Storage = "_Sub_Company_Name", DbType = "VarChar(50)")]
        public string Sub_Company_Name
        {
            get
            {
                return this._Sub_Company_Name;
            }
            set
            {
                if ((this._Sub_Company_Name != value))
                {
                    this._Sub_Company_Name = value;
                }
            }
        }
    }

    public partial class Site
    {

        private int _Site_ID;

        private string _Site_Code;

        private string _Site_Name;

        private string _Region;

        public Site()
        {
        }

        [Column(Storage = "_Site_ID", DbType = "Int NOT NULL")]
        public int Site_ID
        {
            get
            {
                return this._Site_ID;
            }
            set
            {
                if ((this._Site_ID != value))
                {
                    this._Site_ID = value;
                }
            }
        }

        [Column(Storage = "_Site_Code", DbType = "VarChar(50)")]
        public string Site_Code
        {
            get
            {
                return this._Site_Code;
            }
            set
            {
                if ((this._Site_Code != value))
                {
                    this._Site_Code = value;
                }
            }
        }

        [Column(Storage = "_Site_Name", DbType = "VarChar(50)")]
        public string Site_Name
        {
            get
            {
                return this._Site_Name;
            }
            set
            {
                if ((this._Site_Name != value))
                {
                    this._Site_Name = value;
                }
            }
        }

        [Column(Storage = "_Region", DbType = "VarChar(10)")]
        public string Region
        {
            get
            {
                return this._Region;
            }
            set
            {
                if ((this._Region != value))
                {
                    this._Region = value;
                }
            }
        }
    }

    public class SProcResult
    {

        private string _RESULT;

        [Column(Storage = "_RESULT", DbType = "VarChar(8) NOT NULL", CanBeNull = false)]
        public string Result
        {
            get
            {
                return _RESULT;
            }
            set
            {
                if ((_RESULT != value))
                {
                    _RESULT = value;
                }
            }
        }
    }

    public partial class SiteDetails
    {

        private string _Site_Code;

        private string _Site_Name;

        public SiteDetails()
        {
        }

        [Column(Storage = "_Site_Code", DbType = "VarChar(50)")]
        public string Site_Code
        {
            get
            {
                return this._Site_Code;
            }
            set
            {
                if ((this._Site_Code != value))
                {
                    this._Site_Code = value;
                }
            }
        }

        [Column(Storage = "_Site_Name", DbType = "VarChar(50)")]
        public string Site_Name
        {
            get
            {
                return this._Site_Name;
            }
            set
            {
                if ((this._Site_Name != value))
                {
                    this._Site_Name = value;
                }
            }
        }
    }

    public partial class Region
    {

        private string _REGION;

        public Region()
        {
        }

        [Column(Storage = "_REGION", DbType = "VarChar(10)")]
        public string REGION
        {
            get
            {
                return this._REGION;
            }
            set
            {
                if ((this._REGION != value))
                {
                    this._REGION = value;
                }
            }
        }
    }

    public partial class SiteCultureInfo
    {
        private string _UserName;
        private string _LanguageCulture;
        private string _CurrencyCulture;
        private string _DateCulture;
        private string _RegionCulture;

        public SiteCultureInfo()
        {
        }

        [Column(Storage = "_UserName", DbType = "VarChar(200)")]
        public string UserName
        {
            get
            {
                return this._UserName;
            }
            set
            {
                if ((this._UserName != value))
                {
                    this._UserName = value;
                }
            }
        }

        [Column(Storage = "_LanguageCulture", DbType = "VarChar(6)")]
        public string LanguageCulture
        {
            get
            {
                return this._LanguageCulture;
            }
            set
            {
                if ((this._LanguageCulture != value))
                {
                    this._LanguageCulture = value;
                }
            }
        }

        [Column(Storage = "_CurrencyCulture", DbType = "VarChar(6)")]
        public string CurrencyCulture
        {
            get
            {
                return this._CurrencyCulture;
            }
            set
            {
                if ((this._CurrencyCulture != value))
                {
                    this._CurrencyCulture = value;
                }
            }
        }

        [Column(Storage = "_DateCulture", DbType = "VarChar(6)")]
        public string DateCulture
        {
            get
            {
                return this._DateCulture;
            }
            set
            {
                if ((this._DateCulture != value))
                {
                    this._DateCulture = value;
                }
            }
        }

        [Column(Storage = "_RegionCulture", DbType = "VarChar(50)")]
        public string RegionCulture
        {
            get
            {
                return this._RegionCulture;
            }
            set
            {
                if ((this._RegionCulture != value))
                {
                    this._RegionCulture = value;
                }
            }
        }
    }

    public partial class CashierTransactions
    {

        private System.Nullable<double> _CDPaidAmount;

        private System.Nullable<double> _CDPaidCount;

        private System.Nullable<double> _CDPrintedAmount;

        private System.Nullable<double> _CDPrintedCount;

        private System.Nullable<double> _HandPayAmount;

        private System.Nullable<double> _HandPayCount;

        private System.Nullable<double> _ShortPayAmount;

        private System.Nullable<double> _ShortPayCount;

        private System.Nullable<double> _JackpotAmount;

        private System.Nullable<double> _JackpotCount;

        private System.Nullable<double> _ProgAmount;

        private System.Nullable<double> _ProgCount;

        private System.Nullable<double> _VoidAmount;

        private System.Nullable<double> _VoidCount;

        private System.Nullable<double> _MCPaidAmount;

        private System.Nullable<double> _MCPaidCount;

        private System.Nullable<double> _MCPrintAmount;

        private System.Nullable<double> _MCPrintCount;

        private System.Nullable<double> _ActiveCashableVoucherAmount;

        private System.Nullable<double> _ActiveCashableVoucherCount;

        private System.Nullable<double> _VoidTicketsAmount;

        private System.Nullable<double> _VoidTicketsCount;

        private System.Nullable<double> _VoidVoucherAmount;

        private System.Nullable<double> _VoidVoucherCount;

        private System.Nullable<double> _CancelledAmount;

        private System.Nullable<double> _CancelledCount;

        private System.Nullable<double> _ExpiredAmount;

        private System.Nullable<double> _ExpiredCount;

        private System.Nullable<double> _TicketInExceptionAmount;

        private System.Nullable<double> _TicketInExceptionCount;

        private System.Nullable<double> _TicketOutExceptionAmount;

        private System.Nullable<double> _TicketOutExceptionCount;

        private System.Nullable<double> _CashableVoucherLiabilityAmount;

        private System.Nullable<double> _CashableVoucherLiabilityCount;

        private System.Nullable<double> _PromoCashableAmount;

        private System.Nullable<double> _PromoCashableCount;

        private System.Nullable<double> _NonCashableINAmount;

        private System.Nullable<double> _NonCashableINCount;

        private System.Nullable<double> _NonCashableOutAmount;

        private System.Nullable<double> _NonCashableOutCount;

        private System.Nullable<double> _OfflineVoucherAmount;

        private System.Nullable<double> _OfflineVoucherCount;

        public CashierTransactions()
        {
        }

        [Column(Storage = "_CDPaidAmount", DbType = "Float")]
        public System.Nullable<double> CDPaidAmount
        {
            get
            {
                return this._CDPaidAmount;
            }
            set
            {
                if ((this._CDPaidAmount != value))
                {
                    this._CDPaidAmount = value;
                }
            }
        }

        [Column(Storage = "_CDPaidCount", DbType = "Float")]
        public System.Nullable<double> CDPaidCount
        {
            get
            {
                return this._CDPaidCount;
            }
            set
            {
                if ((this._CDPaidCount != value))
                {
                    this._CDPaidCount = value;
                }
            }
        }

        [Column(Storage = "_CDPrintedAmount", DbType = "Float")]
        public System.Nullable<double> CDPrintedAmount
        {
            get
            {
                return this._CDPrintedAmount;
            }
            set
            {
                if ((this._CDPrintedAmount != value))
                {
                    this._CDPrintedAmount = value;
                }
            }
        }

        [Column(Storage = "_CDPrintedCount", DbType = "Float")]
        public System.Nullable<double> CDPrintedCount
        {
            get
            {
                return this._CDPrintedCount;
            }
            set
            {
                if ((this._CDPrintedCount != value))
                {
                    this._CDPrintedCount = value;
                }
            }
        }

        [Column(Storage = "_HandPayAmount", DbType = "Float")]
        public System.Nullable<double> HandPayAmount
        {
            get
            {
                return this._HandPayAmount;
            }
            set
            {
                if ((this._HandPayAmount != value))
                {
                    this._HandPayAmount = value;
                }
            }
        }

        [Column(Storage = "_HandPayCount", DbType = "Float")]
        public System.Nullable<double> HandPayCount
        {
            get
            {
                return this._HandPayCount;
            }
            set
            {
                if ((this._HandPayCount != value))
                {
                    this._HandPayCount = value;
                }
            }
        }

        [Column(Storage = "_ShortPayAmount", DbType = "Float")]
        public System.Nullable<double> ShortPayAmount
        {
            get
            {
                return this._ShortPayAmount;
            }
            set
            {
                if ((this._ShortPayAmount != value))
                {
                    this._ShortPayAmount = value;
                }
            }
        }

        [Column(Storage = "_ShortPayCount", DbType = "Float")]
        public System.Nullable<double> ShortPayCount
        {
            get
            {
                return this._ShortPayCount;
            }
            set
            {
                if ((this._ShortPayCount != value))
                {
                    this._ShortPayCount = value;
                }
            }
        }

        [Column(Storage = "_JackpotAmount", DbType = "Float")]
        public System.Nullable<double> JackpotAmount
        {
            get
            {
                return this._JackpotAmount;
            }
            set
            {
                if ((this._JackpotAmount != value))
                {
                    this._JackpotAmount = value;
                }
            }
        }

        [Column(Storage = "_JackpotCount", DbType = "Float")]
        public System.Nullable<double> JackpotCount
        {
            get
            {
                return this._JackpotCount;
            }
            set
            {
                if ((this._JackpotCount != value))
                {
                    this._JackpotCount = value;
                }
            }
        }

        [Column(Storage = "_ProgAmount", DbType = "Float")]
        public System.Nullable<double> ProgAmount
        {
            get
            {
                return this._ProgAmount;
            }
            set
            {
                if ((this._ProgAmount != value))
                {
                    this._ProgAmount = value;
                }
            }
        }

        [Column(Storage = "_ProgCount", DbType = "Float")]
        public System.Nullable<double> ProgCount
        {
            get
            {
                return this._ProgCount;
            }
            set
            {
                if ((this._ProgCount != value))
                {
                    this._ProgCount = value;
                }
            }
        }

        [Column(Storage = "_VoidAmount", DbType = "Float")]
        public System.Nullable<double> VoidAmount
        {
            get
            {
                return this._VoidAmount;
            }
            set
            {
                if ((this._VoidAmount != value))
                {
                    this._VoidAmount = value;
                }
            }
        }

        [Column(Storage = "_VoidCount", DbType = "Float")]
        public System.Nullable<double> VoidCount
        {
            get
            {
                return this._VoidCount;
            }
            set
            {
                if ((this._VoidCount != value))
                {
                    this._VoidCount = value;
                }
            }
        }

        [Column(Storage = "_MCPaidAmount", DbType = "Float")]
        public System.Nullable<double> MCPaidAmount
        {
            get
            {
                return this._MCPaidAmount;
            }
            set
            {
                if ((this._MCPaidAmount != value))
                {
                    this._MCPaidAmount = value;
                }
            }
        }

        [Column(Storage = "_MCPaidCount", DbType = "Float")]
        public System.Nullable<double> MCPaidCount
        {
            get
            {
                return this._MCPaidCount;
            }
            set
            {
                if ((this._MCPaidCount != value))
                {
                    this._MCPaidCount = value;
                }
            }
        }

        [Column(Storage = "_MCPrintAmount", DbType = "Float")]
        public System.Nullable<double> MCPrintAmount
        {
            get
            {
                return this._MCPrintAmount;
            }
            set
            {
                if ((this._MCPrintAmount != value))
                {
                    this._MCPrintAmount = value;
                }
            }
        }

        [Column(Storage = "_MCPrintCount", DbType = "Float")]
        public System.Nullable<double> MCPrintCount
        {
            get
            {
                return this._MCPrintCount;
            }
            set
            {
                if ((this._MCPrintCount != value))
                {
                    this._MCPrintCount = value;
                }
            }
        }

        [Column(Storage = "_ActiveCashableVoucherAmount", DbType = "Float")]
        public System.Nullable<double> ActiveCashableVoucherAmount
        {
            get
            {
                return this._ActiveCashableVoucherAmount;
            }
            set
            {
                if ((this._ActiveCashableVoucherAmount != value))
                {
                    this._ActiveCashableVoucherAmount = value;
                }
            }
        }

        [Column(Storage = "_ActiveCashableVoucherCount", DbType = "Float")]
        public System.Nullable<double> ActiveCashableVoucherCount
        {
            get
            {
                return this._ActiveCashableVoucherCount;
            }
            set
            {
                if ((this._ActiveCashableVoucherCount != value))
                {
                    this._ActiveCashableVoucherCount = value;
                }
            }
        }

        [Column(Storage = "_VoidTicketsAmount", DbType = "Float")]
        public System.Nullable<double> VoidTicketsAmount
        {
            get
            {
                return this._VoidTicketsAmount;
            }
            set
            {
                if ((this._VoidTicketsAmount != value))
                {
                    this._VoidTicketsAmount = value;
                }
            }
        }

        [Column(Storage = "_VoidTicketsCount", DbType = "Float")]
        public System.Nullable<double> VoidTicketsCount
        {
            get
            {
                return this._VoidTicketsCount;
            }
            set
            {
                if ((this._VoidTicketsCount != value))
                {
                    this._VoidTicketsCount = value;
                }
            }
        }

        [Column(Storage = "_VoidVoucherAmount", DbType = "Float")]
        public System.Nullable<double> VoidVoucherAmount
        {
            get
            {
                return this._VoidVoucherAmount;
            }
            set
            {
                if ((this._VoidVoucherAmount != value))
                {
                    this._VoidVoucherAmount = value;
                }
            }
        }

        [Column(Storage = "_VoidVoucherCount", DbType = "Float")]
        public System.Nullable<double> VoidVoucherCount
        {
            get
            {
                return this._VoidVoucherCount;
            }
            set
            {
                if ((this._VoidVoucherCount != value))
                {
                    this._VoidVoucherCount = value;
                }
            }
        }

        [Column(Storage = "_CancelledAmount", DbType = "Float")]
        public System.Nullable<double> CancelledAmount
        {
            get
            {
                return this._CancelledAmount;
            }
            set
            {
                if ((this._CancelledAmount != value))
                {
                    this._CancelledAmount = value;
                }
            }
        }

        [Column(Storage = "_CancelledCount", DbType = "Float")]
        public System.Nullable<double> CancelledCount
        {
            get
            {
                return this._CancelledCount;
            }
            set
            {
                if ((this._CancelledCount != value))
                {
                    this._CancelledCount = value;
                }
            }
        }

        [Column(Storage = "_ExpiredAmount", DbType = "Float")]
        public System.Nullable<double> ExpiredAmount
        {
            get
            {
                return this._ExpiredAmount;
            }
            set
            {
                if ((this._ExpiredAmount != value))
                {
                    this._ExpiredAmount = value;
                }
            }
        }

        [Column(Storage = "_ExpiredCount", DbType = "Float")]
        public System.Nullable<double> ExpiredCount
        {
            get
            {
                return this._ExpiredCount;
            }
            set
            {
                if ((this._ExpiredCount != value))
                {
                    this._ExpiredCount = value;
                }
            }
        }

        [Column(Storage = "_TicketInExceptionAmount", DbType = "Float")]
        public System.Nullable<double> TicketInExceptionAmount
        {
            get
            {
                return this._TicketInExceptionAmount;
            }
            set
            {
                if ((this._TicketInExceptionAmount != value))
                {
                    this._TicketInExceptionAmount = value;
                }
            }
        }

        [Column(Storage = "_TicketInExceptionCount", DbType = "Float")]
        public System.Nullable<double> TicketInExceptionCount
        {
            get
            {
                return this._TicketInExceptionCount;
            }
            set
            {
                if ((this._TicketInExceptionCount != value))
                {
                    this._TicketInExceptionCount = value;
                }
            }
        }

        [Column(Storage = "_TicketOutExceptionAmount", DbType = "Float")]
        public System.Nullable<double> TicketOutExceptionAmount
        {
            get
            {
                return this._TicketOutExceptionAmount;
            }
            set
            {
                if ((this._TicketOutExceptionAmount != value))
                {
                    this._TicketOutExceptionAmount = value;
                }
            }
        }

        [Column(Storage = "_TicketOutExceptionCount", DbType = "Float")]
        public System.Nullable<double> TicketOutExceptionCount
        {
            get
            {
                return this._TicketOutExceptionCount;
            }
            set
            {
                if ((this._TicketOutExceptionCount != value))
                {
                    this._TicketOutExceptionCount = value;
                }
            }
        }

        [Column(Storage = "_CashableVoucherLiabilityAmount", DbType = "Float")]
        public System.Nullable<double> CashableVoucherLiabilityAmount
        {
            get
            {
                return this._CashableVoucherLiabilityAmount;
            }
            set
            {
                if ((this._CashableVoucherLiabilityAmount != value))
                {
                    this._CashableVoucherLiabilityAmount = value;
                }
            }
        }

        [Column(Storage = "_CashableVoucherLiabilityCount", DbType = "Float")]
        public System.Nullable<double> CashableVoucherLiabilityCount
        {
            get
            {
                return this._CashableVoucherLiabilityCount;
            }
            set
            {
                if ((this._CashableVoucherLiabilityCount != value))
                {
                    this._CashableVoucherLiabilityCount = value;
                }
            }
        }

        [Column(Storage = "_PromoCashableAmount", DbType = "Float")]
        public System.Nullable<double> PromoCashableAmount
        {
            get
            {
                return this._PromoCashableAmount;
            }
            set
            {
                if ((this._PromoCashableAmount != value))
                {
                    this._PromoCashableAmount = value;
                }
            }
        }

        [Column(Storage = "_PromoCashableCount", DbType = "Float")]
        public System.Nullable<double> PromoCashableCount
        {
            get
            {
                return this._PromoCashableCount;
            }
            set
            {
                if ((this._PromoCashableCount != value))
                {
                    this._PromoCashableCount = value;
                }
            }
        }

        [Column(Storage = "_NonCashableINAmount", DbType = "Float")]
        public System.Nullable<double> NonCashableINAmount
        {
            get
            {
                return this._NonCashableINAmount;
            }
            set
            {
                if ((this._NonCashableINAmount != value))
                {
                    this._NonCashableINAmount = value;
                }
            }
        }

        [Column(Storage = "_NonCashableINCount", DbType = "Float")]
        public System.Nullable<double> NonCashableINCount
        {
            get
            {
                return this._NonCashableINCount;
            }
            set
            {
                if ((this._NonCashableINCount != value))
                {
                    this._NonCashableINCount = value;
                }
            }
        }

        [Column(Storage = "_NonCashableOutAmount", DbType = "Float")]
        public System.Nullable<double> NonCashableOutAmount
        {
            get
            {
                return this._NonCashableOutAmount;
            }
            set
            {
                if ((this._NonCashableOutAmount != value))
                {
                    this._NonCashableOutAmount = value;
                }
            }
        }

        [Column(Storage = "_NonCashableOutCount", DbType = "Float")]
        public System.Nullable<double> NonCashableOutCount
        {
            get
            {
                return this._NonCashableOutCount;
            }
            set
            {
                if ((this._NonCashableOutCount != value))
                {
                    this._NonCashableOutCount = value;
                }
            }
        }


        [Column(Storage = "_OfflineVoucherAmount", DbType = "Float")]
        public System.Nullable<double> OfflineVoucherAmount
        {
            get
            {
                return this._OfflineVoucherAmount;
            }
            set
            {
                if ((this._OfflineVoucherAmount != value))
                {
                    this._OfflineVoucherAmount = value;
                }
            }
        }

        [Column(Storage = "_OfflineVoucherCount", DbType = "Float")]
        public System.Nullable<double> OfflineVoucherCount
        {
            get
            {
                return this._OfflineVoucherCount;
            }
            set
            {
                if ((this._OfflineVoucherCount != value))
                {
                    this._OfflineVoucherCount = value;
                }
            }
        }
    }

    public partial class CRMGetRoutesBySiteID
    {

        private int _Route_No;

        private string _Route_Name;

        private System.Nullable<int> _Site_ID;

        private System.Nullable<bool> _Active;

        private char _CanDelete;

        private char _Modified;

        public CRMGetRoutesBySiteID()
        {
        }

        [Column(Storage = "_Route_No", DbType = "Int NOT NULL")]
        public int Route_No
        {
            get
            {
                return this._Route_No;
            }
            set
            {
                if ((this._Route_No != value))
                {
                    this._Route_No = value;
                }
            }
        }

        [Column(Storage = "_Route_Name", DbType = "VarChar(50)")]
        public string Route_Name
        {
            get
            {
                return this._Route_Name;
            }
            set
            {
                if ((this._Route_Name != value))
                {
                    this._Route_Name = value;
                }
            }
        }

        [Column(Storage = "_Site_ID", DbType = "Int")]
        public System.Nullable<int> Site_ID
        {
            get
            {
                return this._Site_ID;
            }
            set
            {
                if ((this._Site_ID != value))
                {
                    this._Site_ID = value;
                }
            }
        }

        [Column(Storage = "_Active", DbType = "Bit")]
        public System.Nullable<bool> Active
        {
            get
            {
                return this._Active;
            }
            set
            {
                if ((this._Active != value))
                {
                    this._Active = value;
                }
            }
        }

        [Column(Storage = "_CanDelete", DbType = "VarChar(1) NOT NULL")]
        public char CanDelete
        {
            get
            {
                return this._CanDelete;
            }
            set
            {
                if ((this._CanDelete != value))
                {
                    this._CanDelete = value;
                }
            }
        }

        [Column(Storage = "_Modified", DbType = "VarChar(1) NOT NULL")]
        public char Modified
        {
            get
            {
                return this._Modified;
            }
            set
            {
                if ((this._Modified != value))
                {
                    this._Modified = value;
                }
            }
        }
    }

    public partial class CRMGetRoutedAssetsByRoute
    {

        private int _Bar_Position_ID;

        private string _Bar_Position_Name;

        private string _Machine_Manufacturers_Serial_No;

        private string _Machine_Stock_No;

        private System.Nullable<int> _Route_ID;

        public CRMGetRoutedAssetsByRoute()
        {
        }

        [Column(Storage = "_Bar_Position_ID", DbType = "Int NOT NULL")]
        public int Bar_Position_ID
        {
            get
            {
                return this._Bar_Position_ID;
            }
            set
            {
                if ((this._Bar_Position_ID != value))
                {
                    this._Bar_Position_ID = value;
                }
            }
        }

        [Column(Storage = "_Bar_Position_Name", DbType = "VarChar(50)")]
        public string Bar_Position_Name
        {
            get
            {
                return this._Bar_Position_Name;
            }
            set
            {
                if ((this._Bar_Position_Name != value))
                {
                    this._Bar_Position_Name = value;
                }
            }
        }

        [Column(Storage = "_Machine_Manufacturers_Serial_No", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string Machine_Manufacturers_Serial_No
        {
            get
            {
                return this._Machine_Manufacturers_Serial_No;
            }
            set
            {
                if ((this._Machine_Manufacturers_Serial_No != value))
                {
                    this._Machine_Manufacturers_Serial_No = value;
                }
            }
        }

        [Column(Storage = "_Machine_Stock_No", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string Machine_Stock_No
        {
            get
            {
                return this._Machine_Stock_No;
            }
            set
            {
                if ((this._Machine_Stock_No != value))
                {
                    this._Machine_Stock_No = value;
                }
            }
        }

        [Column(Storage = "_Route_ID", DbType = "Int")]
        public System.Nullable<int> Route_ID
        {
            get
            {
                return this._Route_ID;
            }
            set
            {
                if ((this._Route_ID != value))
                {
                    this._Route_ID = value;
                }
            }
        }
    }

    public partial class rsp_GetCashierTransactionSettingsResult
    {
        private System.Nullable<bool> _EnableCashdeskReconciliation;

        private System.Nullable<bool> _EnableCashdeskMovement;

        private System.Nullable<bool> _EnableSystemBalancing;

        public rsp_GetCashierTransactionSettingsResult()
        {
        }

        [Column(Storage = "_EnableCashdeskReconciliation", DbType = "Bit")]
        public System.Nullable<bool> EnableCashdeskReconciliation
        {
            get
            {
                return _EnableCashdeskReconciliation;
            }
            set
            {
                if ((_EnableCashdeskReconciliation != value))
                {
                    _EnableCashdeskReconciliation = value;
                }
            }
        }

        [Column(Storage = "_EnableCashdeskMovement", DbType = "Bit")]
        public System.Nullable<bool> EnableCashdeskMovement
        {
            get
            {
                return _EnableCashdeskMovement;
            }
            set
            {
                if ((_EnableCashdeskMovement != value))
                {
                    _EnableCashdeskMovement = value;
                }
            }
        }

        [Column(Storage = "_EnableSystemBalancing", DbType = "Bit")]
        public System.Nullable<bool> EnableSystemBalancing
        {
            get
            {
                return _EnableSystemBalancing;
            }
            set
            {
                if ((_EnableSystemBalancing != value))
                {
                    _EnableSystemBalancing = value;
                }
            }
        }
    }

    public partial class UserDetailsBySiteResult
    {

        private int _SecurityUserID;

        private string _UserName;

        public UserDetailsBySiteResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_SecurityUserID", DbType = "Int NOT NULL")]
        public int SecurityUserID
        {
            get
            {
                return this._SecurityUserID;
            }
            set
            {
                if ((this._SecurityUserID != value))
                {
                    this._SecurityUserID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_UserName", DbType = "VarChar(101)")]
        public string UserName
        {
            get
            {
                return this._UserName;
            }
            set
            {
                if ((this._UserName != value))
                {
                    this._UserName = value;
                }
            }
        }
    }
}
