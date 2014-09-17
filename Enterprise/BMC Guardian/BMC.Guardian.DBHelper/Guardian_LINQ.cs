using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Data;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Linq.Expressions;
using System.ComponentModel;
using System;

namespace BMC.Guardian.DBHelper
{
    [System.Data.Linq.Mapping.DatabaseAttribute(Name = "Enterprise")]
    public partial class GuardianLINQDataContext : System.Data.Linq.DataContext
    {

        private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();

        #region Extensibility Method Definitions
        partial void OnCreated();
        #endregion

        public GuardianLINQDataContext() :
            base(global::BMC.Guardian.DBHelper.Properties.Settings.Default.EnterpriseConnectionString, mappingSource)
        {
            OnCreated();
        }

        public GuardianLINQDataContext(string connection) :
            base(connection, mappingSource)
        {
            OnCreated();
        }

        public GuardianLINQDataContext(System.Data.IDbConnection connection) :
            base(connection, mappingSource)
        {
            OnCreated();
        }

        public GuardianLINQDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) :
            base(connection, mappingSource)
        {
            OnCreated();
        }

        public GuardianLINQDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) :
            base(connection, mappingSource)
        {
            OnCreated();
        }


        [Function(Name = "dbo.rsp_GetViewSiteStatusInfo")]
        public ISingleResult<GetViewSiteStatusInfoResult> GetViewSiteStatusInfo([Parameter(Name = "Site_Code", DbType = "VarChar(50)")] string site_Code, [Parameter(Name = "UserName", DbType = "VarChar(50)")] string userName)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), site_Code,userName);
            return ((ISingleResult<GetViewSiteStatusInfoResult>)(result.ReturnValue));
        }


        [Function(Name = "dbo.rsp_GetGuardianRights")]
        public ISingleResult<GuardianRightsResult> GetGuardianRights([Parameter(Name = "USERID", DbType = "Int")] System.Nullable<int> uSERID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), uSERID);
            return ((ISingleResult<GuardianRightsResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_AuthenticateAndGetUser")]
        public ISingleResult<rsp_AuthenticateAndGetUserResult> rsp_AuthenticateAndGetUser([Parameter(Name = "UserName", DbType = "VarChar(200)")] string userName, [Parameter(Name = "Password", DbType = "VarChar(200)")] string password, [Parameter(Name = "IsAuthenticated", DbType = "Int")] ref System.Nullable<int> isAuthenticated)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), userName, password, isAuthenticated);
            isAuthenticated = ((System.Nullable<int>)(result.GetParameterValue(2)));
            return ((ISingleResult<rsp_AuthenticateAndGetUserResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.usp_LockByUserName")]
        public int usp_LockByUserName([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "UserName", DbType = "VarChar(200)")] string userName)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), userName);
            return ((int)(result.ReturnValue));
        }
    }

    public partial class GuardianRightsResult
    {

        private bool _HQ_GUARDIAN;

        public GuardianRightsResult()
        {
        }

        [Column(Storage = "_HQ_GUARDIAN", DbType = "Bit NOT NULL")]
        public bool HQ_GUARDIAN
        {
            get
            {
                return this._HQ_GUARDIAN;
            }
            set
            {
                if ((this._HQ_GUARDIAN != value))
                {
                    this._HQ_GUARDIAN = value;
                }
            }
        }
    }
    
    public partial class GetViewSiteStatusInfoResult
    {

        private string _HourlyNotRun;

        private string _Region;

        public GetViewSiteStatusInfoResult()
        {
        }

        [Column(Storage = "_HourlyNotRun", DbType = "VarChar(150)")]
        public string HourlyNotRun
        {
            get
            {
                return this._HourlyNotRun;
            }
            set
            {
                if ((this._HourlyNotRun != value))
                {
                    this._HourlyNotRun = value;
                }
            }
        }

        [Column(Storage = "_Region", DbType = "VarChar(50)")]
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

        private string _First_Name;

        private string _Last_Name;

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

        [Column(Storage = "_First_Name", DbType = "VarChar(50)")]
        public string First_Name
        {
            get
            {
                return this._First_Name;
            }
            set
            {
                if ((this._First_Name != value))
                {
                    this._First_Name = value;
                }
            }
        }

        [Column(Storage = "_Last_Name", DbType = "VarChar(50)")]
        public string Last_Name
        {
            get
            {
                return this._Last_Name;
            }
            set
            {
                if ((this._Last_Name != value))
                {
                    this._Last_Name = value;
                }
            }
        }
    }
}
