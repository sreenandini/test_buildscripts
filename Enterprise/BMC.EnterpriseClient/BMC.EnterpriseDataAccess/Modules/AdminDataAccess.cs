using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;
using System.Reflection;
using System.Data.Linq;

namespace BMC.EnterpriseDataAccess
{
    public partial class EnterpriseDataContext
    {
        [Function(Name = "dbo.rsp_AuthenticateUser")]
        public int rsp_AuthenticateUser([Parameter(Name = "UserName", DbType = "VarChar(200)")] string userName, [Parameter(Name = "Password", DbType = "VarChar(200)")] string password, [Parameter(Name = "UserID", DbType = "Int")] ref System.Nullable<int> userID, [Parameter(Name = "IsAuthenticated", DbType = "Int")] ref System.Nullable<int> isAuthenticated)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), userName, password, userID, isAuthenticated);
            userID = ((System.Nullable<int>)(result.GetParameterValue(2)));
            isAuthenticated = ((System.Nullable<int>)(result.GetParameterValue(3)));
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_AuthenticateAndGetUser")]
        public ISingleResult<rsp_AuthenticateAndGetUserResult> rsp_AuthenticateAndGetUser([Parameter(Name = "UserName", DbType = "VarChar(200)")] string userName, [Parameter(Name = "Password", DbType = "VarChar(200)")] string password, [Parameter(Name = "IsAuthenticated", DbType = "Int")] ref System.Nullable<int> isAuthenticated)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), userName, password, isAuthenticated);
            isAuthenticated = ((System.Nullable<int>)(result.GetParameterValue(2)));
            return ((ISingleResult<rsp_AuthenticateAndGetUserResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetUserDetailsByUserID")]
        public ISingleResult<rsp_GetUserDetailsByUserIDResult> GetUserDetailsByUserID([Parameter(Name = "UserID", DbType = "Int")] System.Nullable<int> userID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), userID);
            return ((ISingleResult<rsp_GetUserDetailsByUserIDResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetSetting")]
        public int GetSetting([Parameter(Name = "Setting_ID", DbType = "Int")] System.Nullable<int> setting_ID, [Parameter(Name = "Setting_Name", DbType = "VarChar(100)")] string setting_Name, [Parameter(Name = "Setting_Default", DbType = "VarChar(100)")] string setting_Default, [Parameter(Name = "Setting_Value", DbType = "VarChar(100)")] ref string setting_Value)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), setting_ID, setting_Name, setting_Default, setting_Value);
            setting_Value = ((string)(result.GetParameterValue(3)));
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetSiteSetting")]
        public int GetSiteSetting([Parameter(Name = "Site_Id", DbType = "Int")] System.Nullable<int> site_Id, [Parameter(Name = "SettingMaster_Name", DbType = "VarChar(100)")] string settingMaster_Name, [Parameter(Name = "Setting_Value", DbType = "VarChar(500)")] ref string setting_Value)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), site_Id, settingMaster_Name, setting_Value);
            setting_Value = ((string)(result.GetParameterValue(2)));
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetHQ_User_AccessInfo")]
        public ISingleResult<rsp_GetHQ_User_AccessInfoResult> GetHQ_User_AccessInfo([Parameter(Name = "SecurityUserID", DbType = "Int")] System.Nullable<int> securityUserID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), securityUserID);
            return ((ISingleResult<rsp_GetHQ_User_AccessInfoResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetSplashDetails")]
        public ISingleResult<rsp_GetSplashDetailsResult> GetSplashDetails()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<rsp_GetSplashDetailsResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.usp_InsertExportHistory")]
        public int InsertExportHistory([Parameter(Name = "ID", DbType = "Int")] System.Nullable<int> iD, [Parameter(Name = "Type", DbType = "VarChar(50)")] string type, [Parameter(Name = "Site_Code", DbType = "VarChar(200)")] string site_Code)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), iD, type, site_Code);
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.usp_Insert_ExportHistory")]
        public int Insert_ExportHistory([Parameter(Name = "Reference1", DbType = "VarChar(50)")] string reference1, [Parameter(Name = "Type", DbType = "VarChar(50)")] string type, [Parameter(Name = "UserID", DbType = "Int")] System.Nullable<int> userID, [Parameter(Name = "Site_Code", DbType = "VarChar(200)")] string site_Code)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), reference1, type, userID, site_Code);
            return ((int)(result.ReturnValue));
        }
       
        [Function(Name = "dbo.rsp_GetCustomerAccessViewAllDepots")]
        public ISingleResult<rsp_GetCustomerAccessViewAllDepotsResult> GetCustomerAccessViewAllDepots([Parameter(Name = "StaffId", DbType = "Int")] System.Nullable<int> staffId)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), staffId);
            return ((ISingleResult<rsp_GetCustomerAccessViewAllDepotsResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetCustomerAccessViewAllCompanies")]
        public ISingleResult<rsp_GetCustomerAccessViewAllCompaniesResult> GetCustomerAccessViewAllCompanies([Parameter(Name = "StaffId", DbType = "Int")] System.Nullable<int> staffId)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), staffId);
            return ((ISingleResult<rsp_GetCustomerAccessViewAllCompaniesResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetAllReportsToRolesAccess")]
        public ISingleResult<rsp_GetAllReportsToRolesAccessResult> rsp_GetAllReportsToRolesAccess([Parameter(Name = "SecurityRoleID", DbType = "Int")] System.Nullable<int> securityRoleID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), securityRoleID);
            return ((ISingleResult<rsp_GetAllReportsToRolesAccessResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_ExportAllLiquidationDataToSite")]
        public int rsp_ExportAllLiquidationDataToSite([Parameter(Name = "Site_Code", DbType = "VarChar(50)")] string siteCode)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), siteCode);
            return ((int)(result.ReturnValue));
        }
    }

    public partial class rsp_AuthenticateAndGetUserResult
    {

        private int _SecurityUserID;

        private string _WindowsUserName;

        private string _UserName;

        private char _PASSWORD;

        private System.Nullable<int> _LanguageID;

        private System.Nullable<int> _CurrencyCulture;

        private System.Nullable<int> _DateCulture;

        private System.Nullable<bool> _ChangePassword;

        private System.Nullable<System.DateTime> _CreatedDate;

        private System.Nullable<System.DateTime> _PasswordChangeDate;

        private System.Nullable<bool> _isReset;

        private bool _isLocked;

        private System.Nullable<int> _Staff_ID;

        private string _RoleName;

        private System.Nullable<int> _SecurityRoleID;

        public rsp_AuthenticateAndGetUserResult()
        {
        }

        [Column(Storage = "_SecurityUserID", DbType = "Int NOT NULL")]
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

        [Column(Storage = "_WindowsUserName", DbType = "VarChar(200)")]
        public string WindowsUserName
        {
            get
            {
                return this._WindowsUserName;
            }
            set
            {
                if ((this._WindowsUserName != value))
                {
                    this._WindowsUserName = value;
                }
            }
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

        [Column(Storage = "_PASSWORD", DbType = "VarChar(1) NOT NULL")]
        public char PASSWORD
        {
            get
            {
                return this._PASSWORD;
            }
            set
            {
                if ((this._PASSWORD != value))
                {
                    this._PASSWORD = value;
                }
            }
        }

        [Column(Storage = "_LanguageID", DbType = "Int")]
        public System.Nullable<int> LanguageID
        {
            get
            {
                return this._LanguageID;
            }
            set
            {
                if ((this._LanguageID != value))
                {
                    this._LanguageID = value;
                }
            }
        }

        [Column(Storage = "_CurrencyCulture", DbType = "Int")]
        public System.Nullable<int> CurrencyCulture
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

        [Column(Storage = "_DateCulture", DbType = "Int")]
        public System.Nullable<int> DateCulture
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

        [Column(Storage = "_ChangePassword", DbType = "Bit")]
        public System.Nullable<bool> ChangePassword
        {
            get
            {
                return this._ChangePassword;
            }
            set
            {
                if ((this._ChangePassword != value))
                {
                    this._ChangePassword = value;
                }
            }
        }

        [Column(Storage = "_CreatedDate", DbType = "DateTime")]
        public System.Nullable<System.DateTime> CreatedDate
        {
            get
            {
                return this._CreatedDate;
            }
            set
            {
                if ((this._CreatedDate != value))
                {
                    this._CreatedDate = value;
                }
            }
        }

        [Column(Storage = "_PasswordChangeDate", DbType = "DateTime")]
        public System.Nullable<System.DateTime> PasswordChangeDate
        {
            get
            {
                return this._PasswordChangeDate;
            }
            set
            {
                if ((this._PasswordChangeDate != value))
                {
                    this._PasswordChangeDate = value;
                }
            }
        }

        [Column(Storage = "_isReset", DbType = "Bit")]
        public System.Nullable<bool> isReset
        {
            get
            {
                return this._isReset;
            }
            set
            {
                if ((this._isReset != value))
                {
                    this._isReset = value;
                }
            }
        }

        [Column(Storage = "_isLocked", DbType = "Bit NOT NULL")]
        public bool isLocked
        {
            get
            {
                return this._isLocked;
            }
            set
            {
                if ((this._isLocked != value))
                {
                    this._isLocked = value;
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

        [Column(Storage = "_RoleName", DbType = "VarChar(100)")]
        public string RoleName
        {
            get
            {
                return this._RoleName;
            }
            set
            {
                if ((this._RoleName != value))
                {
                    this._RoleName = value;
                }
            }
        }

        [Column(Storage = "_SecurityRoleID", DbType = "Int")]
        public System.Nullable<int> SecurityRoleID
        {
            get
            {
                return this._SecurityRoleID;
            }
            set
            {
                if ((this._SecurityRoleID != value))
                {
                    this._SecurityRoleID = value;
                }
            }
        }
    }

    public partial class rsp_GetUserDetailsByUserIDResult
    {

        private int _SecurityUserID;

        private string _WindowsUserName;

        private string _UserName;

        private char _PASSWORD;

        private System.Nullable<int> _LanguageID;

        private System.Nullable<int> _CurrencyCulture;

        private System.Nullable<int> _DateCulture;

        private System.Nullable<bool> _ChangePassword;

        private System.Nullable<System.DateTime> _CreatedDate;

        private System.Nullable<System.DateTime> _PasswordChangeDate;

        private System.Nullable<bool> _isReset;

        private bool _isLocked;

        private string _RoleName;

        private System.Nullable<int> _SecurityRoleID;

        public rsp_GetUserDetailsByUserIDResult()
        {
        }

        [Column(Storage = "_SecurityUserID", DbType = "Int NOT NULL")]
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

        [Column(Storage = "_WindowsUserName", DbType = "VarChar(200)")]
        public string WindowsUserName
        {
            get
            {
                return this._WindowsUserName;
            }
            set
            {
                if ((this._WindowsUserName != value))
                {
                    this._WindowsUserName = value;
                }
            }
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

        [Column(Storage = "_PASSWORD", DbType = "VarChar(1) NOT NULL")]
        public char PASSWORD
        {
            get
            {
                return this._PASSWORD;
            }
            set
            {
                if ((this._PASSWORD != value))
                {
                    this._PASSWORD = value;
                }
            }
        }

        [Column(Storage = "_LanguageID", DbType = "Int")]
        public System.Nullable<int> LanguageID
        {
            get
            {
                return this._LanguageID;
            }
            set
            {
                if ((this._LanguageID != value))
                {
                    this._LanguageID = value;
                }
            }
        }

        [Column(Storage = "_CurrencyCulture", DbType = "Int")]
        public System.Nullable<int> CurrencyCulture
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

        [Column(Storage = "_DateCulture", DbType = "Int")]
        public System.Nullable<int> DateCulture
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

        [Column(Storage = "_ChangePassword", DbType = "Bit")]
        public System.Nullable<bool> ChangePassword
        {
            get
            {
                return this._ChangePassword;
            }
            set
            {
                if ((this._ChangePassword != value))
                {
                    this._ChangePassword = value;
                }
            }
        }

        [Column(Storage = "_CreatedDate", DbType = "DateTime")]
        public System.Nullable<System.DateTime> CreatedDate
        {
            get
            {
                return this._CreatedDate;
            }
            set
            {
                if ((this._CreatedDate != value))
                {
                    this._CreatedDate = value;
                }
            }
        }

        [Column(Storage = "_PasswordChangeDate", DbType = "DateTime")]
        public System.Nullable<System.DateTime> PasswordChangeDate
        {
            get
            {
                return this._PasswordChangeDate;
            }
            set
            {
                if ((this._PasswordChangeDate != value))
                {
                    this._PasswordChangeDate = value;
                }
            }
        }

        [Column(Storage = "_isReset", DbType = "Bit")]
        public System.Nullable<bool> isReset
        {
            get
            {
                return this._isReset;
            }
            set
            {
                if ((this._isReset != value))
                {
                    this._isReset = value;
                }
            }
        }

        [Column(Storage = "_isLocked", DbType = "Bit NOT NULL")]
        public bool isLocked
        {
            get
            {
                return this._isLocked;
            }
            set
            {
                if ((this._isLocked != value))
                {
                    this._isLocked = value;
                }
            }
        }

        [Column(Storage = "_RoleName", DbType = "VarChar(100)")]
        public string RoleName
        {
            get
            {
                return this._RoleName;
            }
            set
            {
                if ((this._RoleName != value))
                {
                    this._RoleName = value;
                }
            }
        }

        [Column(Storage = "_SecurityRoleID", DbType = "Int")]
        public System.Nullable<int> SecurityRoleID
        {
            get
            {
                return this._SecurityRoleID;
            }
            set
            {
                if ((this._SecurityRoleID != value))
                {
                    this._SecurityRoleID = value;
                }
            }
        }
    }

    public partial class rsp_GetHQ_User_AccessInfoResult
    {

        private string _Col;

        private System.Nullable<bool> _VALUE;

        public rsp_GetHQ_User_AccessInfoResult()
        {
        }

        [Column(Storage = "_Col", DbType = "NVarChar(128)")]
        public string Col
        {
            get
            {
                return this._Col;
            }
            set
            {
                if ((this._Col != value))
                {
                    this._Col = value;
                }
            }
        }

        [Column(Storage = "_VALUE", DbType = "Bit")]
        public System.Nullable<bool> VALUE
        {
            get
            {
                return this._VALUE;
            }
            set
            {
                if ((this._VALUE != value))
                {
                    this._VALUE = value;
                }
            }
        }
    }

    public partial class rsp_GetSplashDetailsResult
    {

        private string _COPYRIGTINFO;

        private string _PRODUCTVERSION;

        private string _PRODUCTDESC;

        private string _COMPANYNAME;

        private string _PRODUCTNAME;

        public rsp_GetSplashDetailsResult()
        {
        }

        [Column(Storage = "_COPYRIGTINFO", DbType = "VarChar(500)")]
        public string COPYRIGTINFO
        {
            get
            {
                return this._COPYRIGTINFO;
            }
            set
            {
                if ((this._COPYRIGTINFO != value))
                {
                    this._COPYRIGTINFO = value;
                }
            }
        }

        [Column(Storage = "_PRODUCTVERSION", DbType = "VarChar(500)")]
        public string PRODUCTVERSION
        {
            get
            {
                return this._PRODUCTVERSION;
            }
            set
            {
                if ((this._PRODUCTVERSION != value))
                {
                    this._PRODUCTVERSION = value;
                }
            }
        }

        [Column(Storage = "_PRODUCTDESC", DbType = "VarChar(500)")]
        public string PRODUCTDESC
        {
            get
            {
                return this._PRODUCTDESC;
            }
            set
            {
                if ((this._PRODUCTDESC != value))
                {
                    this._PRODUCTDESC = value;
                }
            }
        }

        [Column(Storage = "_COMPANYNAME", DbType = "VarChar(500)")]
        public string COMPANYNAME
        {
            get
            {
                return this._COMPANYNAME;
            }
            set
            {
                if ((this._COMPANYNAME != value))
                {
                    this._COMPANYNAME = value;
                }
            }
        }

        [Column(Storage = "_PRODUCTNAME", DbType = "VarChar(500)")]
        public string PRODUCTNAME
        {
            get
            {
                return this._PRODUCTNAME;
            }
            set
            {
                if ((this._PRODUCTNAME != value))
                {
                    this._PRODUCTNAME = value;
                }
            }
        }
    }

    public partial class rsp_GetCustomerAccessViewAllDepotsResult
    {

        private System.Nullable<bool> _Customer_Access_View_All_Depots;

        public rsp_GetCustomerAccessViewAllDepotsResult()
        {
        }

        [Column(Storage = "_Customer_Access_View_All_Depots", DbType = "Bit")]
        public System.Nullable<bool> Customer_Access_View_All_Depots
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
    }

    public partial class rsp_GetCustomerAccessViewAllCompaniesResult
    {

        private System.Nullable<bool> _Customer_Access_View_All_Companies;

        public rsp_GetCustomerAccessViewAllCompaniesResult()
        {
        }

        [Column(Storage = "_Customer_Access_View_All_Companies", DbType = "Bit")]
        public System.Nullable<bool> Customer_Access_View_All_Companies
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
    }
    public partial class rsp_GetAllReportsToRolesAccessResult
    {

        private int _ReportID;

        private string _ParentName;

        private System.Nullable<int> _ParentID;

        private string _ReportName;

        private string _ReportDescription;

        private System.Nullable<int> _Level;

        private string _ApplicationServer;

        private string _ReportArgName;

        private System.Nullable<bool> _ReportStatus;

        private System.Nullable<bool> _ShowException;

        private System.Nullable<int> _SecurityRoleID;

        private string _MS_ProcedureUsed;

        public rsp_GetAllReportsToRolesAccessResult()
        {
        }

        [Column(Storage = "_ReportID", DbType = "Int NOT NULL")]
        public int ReportID
        {
            get
            {
                return this._ReportID;
            }
            set
            {
                if ((this._ReportID != value))
                {
                    this._ReportID = value;
                }
            }
        }

        [Column(Storage = "_ParentName", DbType = "VarChar(100)")]
        public string ParentName
        {
            get
            {
                return this._ParentName;
            }
            set
            {
                if ((this._ParentName != value))
                {
                    this._ParentName = value;
                }
            }
        }

        [Column(Storage = "_ParentID", DbType = "Int")]
        public System.Nullable<int> ParentID
        {
            get
            {
                return this._ParentID;
            }
            set
            {
                if ((this._ParentID != value))
                {
                    this._ParentID = value;
                }
            }
        }

        [Column(Storage = "_ReportName", DbType = "VarChar(100)")]
        public string ReportName
        {
            get
            {
                return this._ReportName;
            }
            set
            {
                if ((this._ReportName != value))
                {
                    this._ReportName = value;
                }
            }
        }

        [Column(Storage = "_ReportDescription", DbType = "VarChar(100)")]
        public string ReportDescription
        {
            get
            {
                return this._ReportDescription;
            }
            set
            {
                if ((this._ReportDescription != value))
                {
                    this._ReportDescription = value;
                }
            }
        }

        [Column(Name = "[Level]", Storage = "_Level", DbType = "Int")]
        public System.Nullable<int> Level
        {
            get
            {
                return this._Level;
            }
            set
            {
                if ((this._Level != value))
                {
                    this._Level = value;
                }
            }
        }

        [Column(Storage = "_ApplicationServer", DbType = "VarChar(100)")]
        public string ApplicationServer
        {
            get
            {
                return this._ApplicationServer;
            }
            set
            {
                if ((this._ApplicationServer != value))
                {
                    this._ApplicationServer = value;
                }
            }
        }

        [Column(Storage = "_ReportArgName", DbType = "VarChar(100)")]
        public string ReportArgName
        {
            get
            {
                return this._ReportArgName;
            }
            set
            {
                if ((this._ReportArgName != value))
                {
                    this._ReportArgName = value;
                }
            }
        }

        [Column(Storage = "_ReportStatus", DbType = "Bit")]
        public System.Nullable<bool> ReportStatus
        {
            get
            {
                return this._ReportStatus;
            }
            set
            {
                if ((this._ReportStatus != value))
                {
                    this._ReportStatus = value;
                }
            }
        }

        [Column(Storage = "_ShowException", DbType = "Bit")]
        public System.Nullable<bool> ShowException
        {
            get
            {
                return this._ShowException;
            }
            set
            {
                if ((this._ShowException != value))
                {
                    this._ShowException = value;
                }
            }
        }

        [Column(Storage = "_SecurityRoleID", DbType = "Int")]
        public System.Nullable<int> SecurityRoleID
        {
            get
            {
                return this._SecurityRoleID;
            }
            set
            {
                if ((this._SecurityRoleID != value))
                {
                    this._SecurityRoleID = value;
                }
            }
        }

        [Column(Storage = "_MS_ProcedureUsed", DbType = "VarChar(100)")]
        public string MS_ProcedureUsed
        {
            get
            {
                return this._MS_ProcedureUsed;
            }
            set
            {
                if ((this._MS_ProcedureUsed != value))
                {
                    this._MS_ProcedureUsed = value;
                }
            }
        }
    }
}
