using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Reflection;
using BMC.Security.Entity;

namespace BMC.Security.DataContext
{
    internal class SecurityContext : System.Data.Linq.DataContext
    {
        #region Fields

        private IEnumerable<AuthorizationInfo> _authorizationInfo;

        #endregion

        #region Construction

        /// <summary>
        /// Initializes a new instance of the <see cref="SecurityContext"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        public SecurityContext(string connectionString)
            : base(connectionString)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <value>The user.</value>
        internal User User { get; private set; }

        internal IEnumerable<AuthorizationInfo> AuthorizationInfos { get { return _authorizationInfo; } }

        #endregion

        #region Private Methods

        /// <summary>
        /// Gets the data.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        /// <param name="windowsUserName">Name of the windows user.</param>
        /// <returns></returns>
        [ResultType(typeof(User))]
        [ResultType(typeof(AuthorizationInfo))]
        [Function(Name = "dbo.ASPNET_SP_GetSecurityInformation")]
        private IMultipleResults GetData([Parameter(Name = "UserName", DbType = "VarChar(200)")] string userName, [Parameter(Name = "Password", DbType = "VarChar(200)")] string password, [Parameter(Name = "WindowsUserName", DbType = "VarChar(200)")] string windowsUserName)
        {
            this.User = null;
            IExecuteResult result = ExecuteMethodCall(this, ((MethodInfo)(MethodBase.GetCurrentMethod())), userName, password, windowsUserName);
            return ((IMultipleResults)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetSetting")]
        private int GetSetting([Parameter(Name = "Setting_ID", DbType = "Int")] System.Nullable<int> setting_ID, [Parameter(Name = "Setting_Name", DbType = "VarChar(100)")] string setting_Name, [Parameter(Name = "Setting_Default", DbType = "VarChar(100)")] string setting_Default, [Parameter(Name = "Setting_Value", DbType = "VarChar(100)")] ref string setting_Value)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), setting_ID, setting_Name, setting_Default, setting_Value);
            setting_Value = ((string)(result.GetParameterValue(3)));
            return ((int)(result.ReturnValue));
        }


        public System.Data.Linq.Table<UserTable> Users
        {
            get
            {
                return this.GetTable<UserTable>();
            }
        }



        /// <summary>
        /// Loads the data.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        /// <param name="windowsUserName">Name of the windows user.</param>
        private void LoadData(string userName, string password, string windowsUserName)
        {
            IMultipleResults multipleResults = GetData(userName, password, windowsUserName);

            IList<User> users = multipleResults.GetResult<User>().ToList();
            if (users.Count > 0)
            {
                User = users[0];
                _authorizationInfo = multipleResults.GetResult<AuthorizationInfo>().ToList();
            }
        }


        #endregion

        #region Public Methods

        /// <summary>
        /// Loads the data.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        public void LoadData(string userName, string password)
        {
            var user = new User(userName, password);
            LoadData(user.UserName, user.Password, null);
        }

        /// <summary>
        /// Loads the data.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        public void LoadData(string userName, string password,int UserNo)
        {
            var user = new User(userName, password,UserNo);
            LoadData(user.UserName, user.Password, UserNo);
        }

        public IEnumerable<AuthorizationInfo> GetAuthorizationInfo(string username, string password)
        {
            IMultipleResults multipleResults = GetData(username, password,"");
            IEnumerable<AuthorizationInfo> authorizationInfo = null;

            IList<User> users = multipleResults.GetResult<User>().ToList();
            if (users != null && users.Count > 0)
            {
                User = users[0];
                authorizationInfo = multipleResults.GetResult<AuthorizationInfo>().ToList();
            }
            return authorizationInfo;
        }

        /// <summary>
        /// Loads the data.
        /// </summary>
        /// <param name="windowsUserName">Name of the windows user.</param>
        public void LoadData(string windowsUserName)
        {
            LoadData(null, null, windowsUserName);
        }

        public string GetCultureInfo(int userId)
        {

            var k = from user in Users
                    where user.SecurityUserID == userId
                    select user.CultureInfo;

            foreach (var queryable in k)
                return queryable;

            return "en-US";
        }


        public string GetCurrencyCultureInfo(int userId)
        {

            var k = from user in Users
                    where user.SecurityUserID == userId
                    select user.CurrencyCulture;

            foreach (var queryable in k)
                return queryable;

            return "en-US";
        }


        public string GetDateCultureInfo(int userId)
        {

            var k = from user in Users
                    where user.SecurityUserID == userId
                    select user.DateCulture;

            foreach (var queryable in k)
                return queryable;

            return "en-US";
        }

        public string GetSettingValue(string settingName, string defaultValue)
        {
            string refString = string.Empty;
            GetSetting(null, settingName, defaultValue, ref refString);
            return refString;
        }

        public int GetExpiryDays()
        {
            int returnValue;
            string refSTring = GetSettingValue("Login_Expiry_No_of_Days", "60");
            if (!int.TryParse(refSTring, out returnValue))
                returnValue = 60;
            return returnValue;
        }

        public int GetNumberOfAttempts()
        {
            int returnValue;
            string refSTring = GetSettingValue("Login_Max_No_Of_Attempts", "3");
            if (!int.TryParse(refSTring, out returnValue))
                returnValue = 60;
            return returnValue;
        }

        public bool IsRegulatoryEnabled()
        {
            bool IsRegulatory = false;

            string refSTring = GetSettingValue("IsRegulatoryEnabled", "False");
            if (!bool.TryParse(refSTring, out IsRegulatory))
                IsRegulatory = false;
            return IsRegulatory;
        }

        public bool IsAFTEnabledForSite()
        {
            bool IsAFTEnabledForSite = false;

            string refSTring = GetSettingValue("IsAFTEnabledForSite", "False");
            if (!bool.TryParse(refSTring, out IsAFTEnabledForSite))
                IsAFTEnabledForSite = false;
            return IsAFTEnabledForSite;
        }

        public bool IsSuppressZoneEnabled()
        {
            bool IsSuppressZoneEnabled = false;
            string refSTring = GetSettingValue("IsSuppressZoneEnabled", "False");
            if (!bool.TryParse(refSTring, out IsSuppressZoneEnabled))
                IsSuppressZoneEnabled = false;
            return IsSuppressZoneEnabled;
        }

        #endregion


    }

    [Table(Name = "dbo.[User]")]
    public partial class UserTable
    {

        private System.Nullable<int> _SecurityUserID;

        private string _CultureInfo;
        private string _CurrencyCulture;
        private string _DateCulture;

        public UserTable()
        {
        }

        [Column(Storage = "_SecurityUserID", DbType = "Int")]
        public System.Nullable<int> SecurityUserID
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

        [Column(Storage = "_CultureInfo", DbType = "VarChar(10)")]
        public string CultureInfo
        {
            get
            {
                return this._CultureInfo;
            }
            set
            {
                if ((this._CultureInfo != value))
                {
                    this._CultureInfo = value;
                }
            }
        }

        [Column(Storage = "_CurrencyCulture", DbType = "VarChar(10)")]
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

        [Column(Storage = "_DateCulture", DbType = "VarChar(10)")]
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
    }
}