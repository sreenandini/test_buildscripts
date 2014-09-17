using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Data;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Linq.Expressions;
using System.ComponentModel;
using System;

namespace BMC.DataAccess
{

    [global::System.Data.Linq.Mapping.DatabaseAttribute(Name = "Enterprise")]
    public partial class DataHelper : System.Data.Linq.DataContext, IDisposable
    {

        private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();

        #region Extensibility Method Definitions
        partial void OnCreated();
        #endregion

        public DataHelper() :
            base(global::BMC.DataAccess.Properties.Settings.Default.EnterpriseConnectionString, mappingSource)
        {
            OnCreated();
        }

        public DataHelper(string connection) :
            base(connection, mappingSource)
        {
            OnCreated();
        }

        public DataHelper(System.Data.IDbConnection connection) :
            base(connection, mappingSource)
        {
            OnCreated();
        }

        public DataHelper(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) :
            base(connection, mappingSource)
        {
            OnCreated();
        }

        public DataHelper(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) :
            base(connection, mappingSource)
        {
            OnCreated();
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.rsp_GetUnprocessedRecordsForEmailAlert")]
        public ISingleResult<UnprocessedRecordsForEmailAlertResult> GetUnprocessedRecordsForEmailAlert([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "SiteCode", DbType = "VarChar(20)")] string siteCode, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "RecordsCount", DbType = "Int")] System.Nullable<int> recordsCount)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), siteCode, recordsCount);
            return ((ISingleResult<UnprocessedRecordsForEmailAlertResult>)(result.ReturnValue));
        }


        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.rsp_GetSiteList")]
        public ISingleResult<rsp_GetSiteListResult> GetSiteList()
        {   
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<rsp_GetSiteListResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.usp_UpdateEmailAlertHistoryStatus")]
        public int UpdateEmailAlertHistoryStatus([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ID", DbType = "Int")] System.Nullable<int> iD,
            [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Result", DbType = "VarChar(8000)")] string result,
            [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Status", DbType = "Int")] System.Nullable<int> status)
        {
            IExecuteResult result1 = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), iD, result, status);
            return ((int)(result1.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.rsp_GetEmailSubscriberDetails")]
        public ISingleResult<EmailSubscriberDetailsResult> GetEmailAlertSubscribers([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "AlertType", DbType = "varchar(50)")] string alertType)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), alertType);
            return ((ISingleResult<EmailSubscriberDetailsResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.rsp_GetMailServerInfo")]
        public ISingleResult<MailServerInfoResult> GetMailServerInfo()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<MailServerInfoResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetSetting")]
        public int GetSetting([Parameter(Name = "Setting_ID", DbType = "Int")] System.Nullable<int> setting_ID, [Parameter(Name = "Setting_Name", DbType = "VarChar(100)")] string setting_Name, [Parameter(Name = "Setting_Default", DbType = "VarChar(8000)")] string setting_Default, [Parameter(Name = "Setting_Value", DbType = "VarChar(8000)")] ref string setting_Value)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), setting_ID, setting_Name, setting_Default, setting_Value);
            setting_Value = ((string)(result.GetParameterValue(3)));
            return ((int)(result.ReturnValue));
        }

        #region AutoCalendar
        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.usp_AC_CheckAndUpdateAutoCalendar")]
        public int CheckAndUpdateAutoCalendar()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.usp_AC_CheckAndUpdateAlertRecurrence")]
        public int CheckAndUpdateAlertRecurrence()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.usp_AC_CheckAndCreateCalendar")]
        public int CheckAndCreateCalendar()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.usp_AC_CheckAndUpdateNewCalendar")]
        public int CheckAndUpdateNewCalendar([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "MinDays", DbType = "Int")] int minDays)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), minDays);
            return ((int)(result.ReturnValue));
        }

        #endregion

    }

    public partial class UnprocessedRecordsForEmailAlertResult
    {

        private long _EMD_ID;

        private string _AlertType_Name;

        private string _EMD_Content;

        private System.Nullable<short> _EMD_Sent_Mail_Status;

        private string _EMD_SiteCode;

        private string _SiteName;

        public UnprocessedRecordsForEmailAlertResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_EMD_ID", DbType = "BigInt NOT NULL")]
        public long EMD_ID
        {
            get
            {
                return this._EMD_ID;
            }
            set
            {
                if ((this._EMD_ID != value))
                {
                    this._EMD_ID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_AlertType_Name", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string AlertType_Name
        {
            get
            {
                return this._AlertType_Name;
            }
            set
            {
                if ((this._AlertType_Name != value))
                {
                    this._AlertType_Name = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_EMD_Content", DbType = "VarChar(MAX) NOT NULL", CanBeNull = false)]
        public string EMD_Content
        {
            get
            {
                return this._EMD_Content;
            }
            set
            {
                if ((this._EMD_Content != value))
                {
                    this._EMD_Content = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_EMD_Sent_Mail_Status", DbType = "SmallInt")]
        public System.Nullable<short> EMD_Sent_Mail_Status
        {
            get
            {
                return this._EMD_Sent_Mail_Status;
            }
            set
            {
                if ((this._EMD_Sent_Mail_Status != value))
                {
                    this._EMD_Sent_Mail_Status = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_EMD_SiteCode", DbType = "VarChar(2000)")]
        public string EMD_SiteCode
        {
            get
            {
                return this._EMD_SiteCode;
            }
            set
            {
                if ((this._EMD_SiteCode != value))
                {
                    this._EMD_SiteCode = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_SiteName", DbType = "VarChar(250)")]
        public string SiteName
        {
            get
            {
                return this._SiteName;
            }
            set
            {
                if ((this._SiteName != value))
                {
                    this._SiteName = value;
                }
            }
        }
    }

    public partial class EmailSubscriberDetailsResult
    {

        private long _AS_ID;

        private int _ID;

        private string _TypeName;

        private string _SUBJECT;

        private string _ToMail;

        private string _CCMail;

        private string _BCCMail;

        private string _FromMail;

        public EmailSubscriberDetailsResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_AS_ID", DbType = "BigInt NOT NULL")]
        public long AS_ID
        {
            get
            {
                return this._AS_ID;
            }
            set
            {
                if ((this._AS_ID != value))
                {
                    this._AS_ID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ID", DbType = "Int NOT NULL")]
        public int ID
        {
            get
            {
                return this._ID;
            }
            set
            {
                if ((this._ID != value))
                {
                    this._ID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TypeName", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string TypeName
        {
            get
            {
                return this._TypeName;
            }
            set
            {
                if ((this._TypeName != value))
                {
                    this._TypeName = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_SUBJECT", DbType = "VarChar(500)")]
        public string SUBJECT
        {
            get
            {
                return this._SUBJECT;
            }
            set
            {
                if ((this._SUBJECT != value))
                {
                    this._SUBJECT = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ToMail", DbType = "VarChar(MAX) NOT NULL", CanBeNull = false)]
        public string ToMail
        {
            get
            {
                return this._ToMail;
            }
            set
            {
                if ((this._ToMail != value))
                {
                    this._ToMail = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CCMail", DbType = "VarChar(MAX)")]
        public string CCMail
        {
            get
            {
                return this._CCMail;
            }
            set
            {
                if ((this._CCMail != value))
                {
                    this._CCMail = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_BCCMail", DbType = "VarChar(MAX)")]
        public string BCCMail
        {
            get
            {
                return this._BCCMail;
            }
            set
            {
                if ((this._BCCMail != value))
                {
                    this._BCCMail = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_FromMail", DbType = "VarChar(MAX)")]
        public string FromMail
        {
            get
            {
                return this._FromMail;
            }
            set
            {
                if ((this._FromMail != value))
                {
                    this._FromMail = value;
                }
            }
        }
    }

    public partial class rsp_GetSiteListResult
    {

        private string _SITE_CODE;

        public rsp_GetSiteListResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_SITE_CODE", DbType = "VarChar(50)")]
        public string SITE_CODE
        {
            get
            {
                return this._SITE_CODE;
            }
            set
            {
                if ((this._SITE_CODE != value))
                {
                    this._SITE_CODE = value;
                }
            }
        }
    }

    public partial class MailServerInfoResult
    {

        private int _ID;

        private string _ServerName;

        private int _Port;

        private System.Nullable<bool> _EnableSSL;

        private string _UID;

        private string _PWD;

        public MailServerInfoResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ID", DbType = "Int NOT NULL")]
        public int ID
        {
            get
            {
                return this._ID;
            }
            set
            {
                if ((this._ID != value))
                {
                    this._ID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ServerName", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string ServerName
        {
            get
            {
                return this._ServerName;
            }
            set
            {
                if ((this._ServerName != value))
                {
                    this._ServerName = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Port", DbType = "Int NOT NULL")]
        public int Port
        {
            get
            {
                return this._Port;
            }
            set
            {
                if ((this._Port != value))
                {
                    this._Port = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_EnableSSL", DbType = "Bit")]
        public System.Nullable<bool> EnableSSL
        {
            get
            {
                return this._EnableSSL;
            }
            set
            {
                if ((this._EnableSSL != value))
                {
                    this._EnableSSL = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_UID", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string UID
        {
            get
            {
                return this._UID;
            }
            set
            {
                if ((this._UID != value))
                {
                    this._UID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_PWD", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string PWD
        {
            get
            {
                return this._PWD;
            }
            set
            {
                if ((this._PWD != value))
                {
                    this._PWD = value;
                }
            }
        }
    }
}
