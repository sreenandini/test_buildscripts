using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Reflection;

namespace BMC.EnterpriseDataAccess
{
    public partial class EnterpriseDataContext
    {
        [Function(Name = "dbo.rsp_GetSiteAccessCompanies")]
        public ISingleResult<rsp_GetSiteAccessCompaniesResult> GetSiteAccessCompanies()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<rsp_GetSiteAccessCompaniesResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetSiteAccessOperators")]
        public ISingleResult<rsp_GetSiteAccessOperatorsResult> GetSiteAccessOperators()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<rsp_GetSiteAccessOperatorsResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetSiteAccessSites")]
        public ISingleResult<rsp_GetSiteAccessSitesResult> GetSiteAccessSites()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<rsp_GetSiteAccessSitesResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetSiteCustomerAccess")]
        public ISingleResult<rsp_GetSiteCustomerAccessResult> GetSiteCustomerAccess([Parameter(Name = "CustomerAccessID", DbType = "Int")] System.Nullable<int> customerAccessID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), customerAccessID);
            return ((ISingleResult<rsp_GetSiteCustomerAccessResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetStaffCustomerAccess")]
        public ISingleResult<rsp_GetStaffCustomerAccessResult> GetStaffCustomerAccess([Parameter(Name = "Staff_ID", DbType = "Int")] System.Nullable<int> staff_ID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), staff_ID);
            return ((ISingleResult<rsp_GetStaffCustomerAccessResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.esp_InsertCustomerAccessForSite")]
        public int esp_InsertCustomerAccessForSite([Parameter(Name = "CustomerAccessName", DbType = "VarChar(50)")] string customerAccessName)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), customerAccessName);
            return ((int)(result.ReturnValue));
        }
        
        [Function(Name = "dbo.usp_UpdateCompaniesCustomerAccess")]
        public int UpdateCompaniesCustomerAccess([Parameter(Name = "CustomerAccessID", DbType = "Int")] System.Nullable<int> customerAccessID, [Parameter(Name = "SubCompanyID", DbType = "Int")] System.Nullable<int> subCompanyID, [Parameter(Name = "Status", DbType = "Bit")] System.Nullable<bool> status, [Parameter(Name = "IsChecked", DbType = "Bit")] System.Nullable<bool> isChecked)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), customerAccessID, subCompanyID, status, isChecked);
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.usp_UpdateDepotsCustomerAccess")]
        public int UpdateDepotsCustomerAccess([Parameter(Name = "CustomerAccessID", DbType = "Int")] System.Nullable<int> customerAccessID, [Parameter(Name = "DepotID", DbType = "Int")] System.Nullable<int> depotID, [Parameter(Name = "Status", DbType = "Bit")] System.Nullable<bool> status, [Parameter(Name = "IsChecked", DbType = "Bit")] System.Nullable<bool> isChecked)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), customerAccessID, depotID, status, isChecked);
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.USP_EditSecurityProfile")]
        public int EditSecurityProfile([Parameter(Name = "CustomerAccessID", DbType = "Int")] System.Nullable<int> customerAccessID, [Parameter(Name = "SecurityProfileType_ID", DbType = "Int")] System.Nullable<int> securityProfileType_ID, [Parameter(Name = "SecurityProfileType_Value", DbType = "VarChar(100)")] string securityProfileType_Value, [Parameter(Name = "AllowUser", DbType = "Bit")] System.Nullable<bool> allowUser, [Parameter(Name = "Description", DbType = "VarChar(255)")] string description)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), customerAccessID, securityProfileType_ID, securityProfileType_Value, allowUser, description);
            return ((int)(result.ReturnValue));
        }
        [Function(Name = "dbo.rsp_GetCustomerAccessDetails")]
        public ISingleResult<rsp_GetCustomerAccessDetailsResult> GetCustomerAccessDetails()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<rsp_GetCustomerAccessDetailsResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.usp_UpdateStaffCustomerAccess")]
        public int UpdateStaffCustomerAccess([Parameter(Name = "Staff_ID", DbType = "Int")] System.Nullable<int> staff_ID, [Parameter(Name = "Customer_Access_ID", DbType = "Int")] System.Nullable<int> customer_Access_ID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), staff_ID, customer_Access_ID);
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetCustomerAccessSite")]
        public ISingleResult<rsp_GetCustomerAccessSiteResult> GetCustomerAccessSite([Parameter(Name = "CustomerAccessID", DbType = "Int")] System.Nullable<int> customerAccessID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), customerAccessID);
            return ((ISingleResult<rsp_GetCustomerAccessSiteResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetCustomerAccessDepot")]
        public ISingleResult<rsp_GetCustomerAccessDepotResult> GetCustomerAccessDepot([Parameter(Name = "CustomerAccessID", DbType = "Int")] System.Nullable<int> customerAccessID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), customerAccessID);
            return ((ISingleResult<rsp_GetCustomerAccessDepotResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetCustomerAccessCompany")]
        public ISingleResult<rsp_GetCustomerAccessCompanyResult> GetCustomerAccessCompany([Parameter(Name = "CustomerAccessID", DbType = "Int")] System.Nullable<int> customerAccessID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), customerAccessID);
            return ((ISingleResult<rsp_GetCustomerAccessCompanyResult>)(result.ReturnValue));
        }
    }

    public partial class rsp_GetSiteAccessCompaniesResult
    {

        private int _Company_ID;

        private string _Company_Name;

        private int _Sub_Company_ID;

        private string _Sub_Company_Name;

        public rsp_GetSiteAccessCompaniesResult()
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

    public partial class rsp_GetSiteAccessOperatorsResult
    {

        private int _Operator_ID;

        private string _Operator_Name;

        private int _Depot_ID;

        private string _Depot_Name;

        public rsp_GetSiteAccessOperatorsResult()
        {
        }

        [Column(Storage = "_Operator_ID", DbType = "Int NOT NULL")]
        public int Operator_ID
        {
            get
            {
                return this._Operator_ID;
            }
            set
            {
                if ((this._Operator_ID != value))
                {
                    this._Operator_ID = value;
                }
            }
        }

        [Column(Storage = "_Operator_Name", DbType = "VarChar(50)")]
        public string Operator_Name
        {
            get
            {
                return this._Operator_Name;
            }
            set
            {
                if ((this._Operator_Name != value))
                {
                    this._Operator_Name = value;
                }
            }
        }

        [Column(Storage = "_Depot_ID", DbType = "Int NOT NULL")]
        public int Depot_ID
        {
            get
            {
                return this._Depot_ID;
            }
            set
            {
                if ((this._Depot_ID != value))
                {
                    this._Depot_ID = value;
                }
            }
        }

        [Column(Storage = "_Depot_Name", DbType = "VarChar(50)")]
        public string Depot_Name
        {
            get
            {
                return this._Depot_Name;
            }
            set
            {
                if ((this._Depot_Name != value))
                {
                    this._Depot_Name = value;
                }
            }
        }
    }

    public partial class rsp_GetSiteAccessSitesResult
    {

        private int _Company_ID;

        private string _Company_Name;

        private int _Sub_Company_ID;

        private string _Sub_Company_Name;

        private int _Site_ID;

        private string _Site_Name;

        private string _Site_Code;

        private string _Site_Address_2;

        private string _Site_Address_3;

        public rsp_GetSiteAccessSitesResult()
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

        [Column(Storage = "_Site_Address_2", DbType = "VarChar(50)")]
        public string Site_Address_2
        {
            get
            {
                return this._Site_Address_2;
            }
            set
            {
                if ((this._Site_Address_2 != value))
                {
                    this._Site_Address_2 = value;
                }
            }
        }

        [Column(Storage = "_Site_Address_3", DbType = "VarChar(50)")]
        public string Site_Address_3
        {
            get
            {
                return this._Site_Address_3;
            }
            set
            {
                if ((this._Site_Address_3 != value))
                {
                    this._Site_Address_3 = value;
                }
            }
        }
    }

    public partial class rsp_GetSiteCustomerAccessResult
    {

        private int _Customer_Access_ID;

        private string _Customer_Access_Name;

        private bool _Customer_Access_View_All_Companies;

        private bool _Customer_Access_View_All_Depots;

        private bool _Customer_Access_View_All_Sites;

        public rsp_GetSiteCustomerAccessResult()
        {
        }

        [Column(Storage = "_Customer_Access_ID", DbType = "Int NOT NULL")]
        public int Customer_Access_ID
        {
            get
            {
                return this._Customer_Access_ID;
            }
            set
            {
                if ((this._Customer_Access_ID != value))
                {
                    this._Customer_Access_ID = value;
                }
            }
        }

        [Column(Storage = "_Customer_Access_Name", DbType = "VarChar(50)")]
        public string Customer_Access_Name
        {
            get
            {
                return this._Customer_Access_Name;
            }
            set
            {
                if ((this._Customer_Access_Name != value))
                {
                    this._Customer_Access_Name = value;
                }
            }
        }

        [Column(Storage = "_Customer_Access_View_All_Companies", DbType = "Bit NOT NULL")]
        public bool Customer_Access_View_All_Companies
        {
            get
            {
                return this._Customer_Access_View_All_Companies;
            }
            set
            {
                if ((this._Customer_Access_View_All_Companies != value))
                {
                    this._Customer_Access_View_All_Companies = value;
                }
            }
        }

        [Column(Storage = "_Customer_Access_View_All_Depots", DbType = "Bit NOT NULL")]
        public bool Customer_Access_View_All_Depots
        {
            get
            {
                return this._Customer_Access_View_All_Depots;
            }
            set
            {
                if ((this._Customer_Access_View_All_Depots != value))
                {
                    this._Customer_Access_View_All_Depots = value;
                }
            }
        }

        [Column(Storage = "_Customer_Access_View_All_Sites", DbType = "Bit NOT NULL")]
        public bool Customer_Access_View_All_Sites
        {
            get
            {
                return this._Customer_Access_View_All_Sites;
            }
            set
            {
                if ((this._Customer_Access_View_All_Sites != value))
                {
                    this._Customer_Access_View_All_Sites = value;
                }
            }
        }
    }

    public partial class rsp_GetStaffCustomerAccessResult
    {

        private int _Staff_Customer_Access_ID;

        private System.Nullable<int> _Staff_ID;

        private System.Nullable<int> _Customer_Access_ID;

        public rsp_GetStaffCustomerAccessResult()
        {
        }

        [Column(Storage = "_Staff_Customer_Access_ID", DbType = "Int NOT NULL")]
        public int Staff_Customer_Access_ID
        {
            get
            {
                return this._Staff_Customer_Access_ID;
            }
            set
            {
                if ((this._Staff_Customer_Access_ID != value))
                {
                    this._Staff_Customer_Access_ID = value;
                }
            }
        }

        [Column(Storage = "_Staff_ID", DbType = "Int")]
        public System.Nullable<int> Staff_ID
        {
            get
            {
                return this._Staff_ID;
            }
            set
            {
                if ((this._Staff_ID != value))
                {
                    this._Staff_ID = value;
                }
            }
        }

        [Column(Storage = "_Customer_Access_ID", DbType = "Int")]
        public System.Nullable<int> Customer_Access_ID
        {
            get
            {
                return this._Customer_Access_ID;
            }
            set
            {
                if ((this._Customer_Access_ID != value))
                {
                    this._Customer_Access_ID = value;
                }
            }
        }
    }

    public partial class rsp_GetCustomerAccessDetailsResult
    {

        private int _Customer_Access_ID;

        private string _Customer_Access_Name;

        public rsp_GetCustomerAccessDetailsResult()
        {
        }

        [Column(Storage = "_Customer_Access_ID", DbType = "Int NOT NULL")]
        public int Customer_Access_ID
        {
            get
            {
                return this._Customer_Access_ID;
            }
            set
            {
                if ((this._Customer_Access_ID != value))
                {
                    this._Customer_Access_ID = value;
                }
            }
        }

        [Column(Storage = "_Customer_Access_Name", DbType = "VarChar(50)")]
        public string Customer_Access_Name
        {
            get
            {
                return this._Customer_Access_Name;
            }
            set
            {
                if ((this._Customer_Access_Name != value))
                {
                    this._Customer_Access_Name = value;
                }
            }
        }
    }

    public partial class rsp_GetCustomerAccessSiteResult
    {

        private string _SecurityProfileType_Value;

        private System.Nullable<bool> _AllowUser;

        public rsp_GetCustomerAccessSiteResult()
        {
        }

        [Column(Storage = "_SecurityProfileType_Value", DbType = "VarChar(100)")]
        public string SecurityProfileType_Value
        {
            get
            {
                return this._SecurityProfileType_Value;
            }
            set
            {
                if ((this._SecurityProfileType_Value != value))
                {
                    this._SecurityProfileType_Value = value;
                }
            }
        }

        [Column(Storage = "_AllowUser", DbType = "Bit")]
        public System.Nullable<bool> AllowUser
        {
            get
            {
                return this._AllowUser;
            }
            set
            {
                if ((this._AllowUser != value))
                {
                    this._AllowUser = value;
                }
            }
        }
    }

    public partial class rsp_GetCustomerAccessDepotResult
    {

        private int _Depot_ID;

        private string _Depot_Name;

        public rsp_GetCustomerAccessDepotResult()
        {
        }

        [Column(Storage = "_Depot_ID", DbType = "Int NOT NULL")]
        public int Depot_ID
        {
            get
            {
                return this._Depot_ID;
            }
            set
            {
                if ((this._Depot_ID != value))
                {
                    this._Depot_ID = value;
                }
            }
        }

        [Column(Storage = "_Depot_Name", DbType = "VarChar(50)")]
        public string Depot_Name
        {
            get
            {
                return this._Depot_Name;
            }
            set
            {
                if ((this._Depot_Name != value))
                {
                    this._Depot_Name = value;
                }
            }
        }
    }

    public partial class rsp_GetCustomerAccessCompanyResult
    {

        private int _Sub_Company_ID;

        private string _Sub_Company_Name;

        public rsp_GetCustomerAccessCompanyResult()
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
	
}
