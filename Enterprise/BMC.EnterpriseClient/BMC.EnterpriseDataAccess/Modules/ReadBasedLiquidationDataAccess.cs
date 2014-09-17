using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Reflection;

namespace BMC.EnterpriseDataAccess
{
    public partial class EnterpriseDataContext
    {
        [Function(Name = "dbo.rsp_GetActiveSiteDetailsBySetting")]
        public ISingleResult<rsp_GetActiveSiteDetailsBySettingResult> GetActiveSiteDetailsBySetting([Parameter(Name = "UserID", DbType = "Int")] System.Nullable<int> userID, [Parameter(Name = "SettingName", DbType = "VarChar(200)")] string settingName)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), userID, settingName);
            return ((ISingleResult<rsp_GetActiveSiteDetailsBySettingResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetActiveSitesForUser")]
        public ISingleResult<rsp_GetActiveSitesForUserResult> GetActiveSitesForUser([Parameter(Name = "UserID", DbType = "Int")] System.Nullable<int> userID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), userID);
            return ((ISingleResult<rsp_GetActiveSitesForUserResult>)(result.ReturnValue));
        }
    }

    public partial class rsp_GetActiveSiteDetailsBySettingResult
    {

        private int _Site_ID;

        private string _Site_Code;

        private string _Site_Name;

        private string _DisplayName;

        public rsp_GetActiveSiteDetailsBySettingResult()
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

        [Column(Storage = "_DisplayName", DbType = "VarChar(103)")]
        public string DisplayName
        {
            get
            {
                return this._DisplayName;
            }
            set
            {
                if ((this._DisplayName != value))
                {
                    this._DisplayName = value;
                }
            }
        }
    }

    public partial class rsp_GetActiveSitesForUserResult
    {

        private int _Site_ID;

        private string _Site_Code;

        private string _Site_Name;

        private string _DisplayName;

        public rsp_GetActiveSitesForUserResult()
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

        [Column(Storage = "_DisplayName", DbType = "VarChar(103)")]
        public string DisplayName
        {
            get
            {
                return this._DisplayName;
            }
            set
            {
                if ((this._DisplayName != value))
                {
                    this._DisplayName = value;
                }
            }
        }
    }
}
