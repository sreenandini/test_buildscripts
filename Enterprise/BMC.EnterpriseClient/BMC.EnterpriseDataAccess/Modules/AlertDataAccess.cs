using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Reflection;
using System.Text;

namespace BMC.EnterpriseDataAccess
{
    public partial class EnterpriseDataContext
    {
        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.rsp_GetAlertTypes")]
        public ISingleResult<GetAlertTypesResult> GetAlertTypes()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<GetAlertTypesResult>)(result.ReturnValue));
        }
       
        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.esp_InsertEmailAlertSubscribers")]
        public int InsertEmailAlertSubscribers(
               [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "inputxml", DbType = "VarChar(MAX)")] string inputxml,
               [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "SubList", DbType = "VarChar(MAX)")] string subList,
             [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "UserName", DbType = "VarChar(50)")] string UserName,
             [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "UserID", DbType = "Int")] int UserID,
             [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "OldValue", DbType = "VarChar(500)")] string OldValue,
             [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "NewValue", DbType = "VarChar(500)")] string NewValue)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), inputxml,subList, UserName, UserID, OldValue, NewValue);
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.rsp_GetEmailSubscriberDetails")]
        public ISingleResult<EmailSubscriberDetailsResult> GetEmailSubscriberDetails(
            [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "AlertType", DbType = "varchar(50)")] string alertType)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())),alertType);
            return ((ISingleResult<EmailSubscriberDetailsResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.rsp_GetEmailAlertAuditDetails")]
        public ISingleResult<EmailAlertAuditDetailsResult> GetEmailAlertAuditDetails(
            [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "AlertTypeID", DbType = "Int")] System.Nullable<int> alertTypeID,
             [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "SiteCode", DbType = "VarChar(20)")] string SiteCode,
            [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "IsProcessed", DbType = "BIT")] bool IsProcessed
            )
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), alertTypeID, SiteCode,IsProcessed);
            return ((ISingleResult<EmailAlertAuditDetailsResult>)(result.ReturnValue));
        }


        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.esp_InsertMailServerInfo")]
        public int InsertMailServerInfo([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ServerName", DbType = "VarChar(50)")] string serverName, 
            [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "EnableSSL", DbType = "Bit")] System.Nullable<bool> enableSSL, 
            [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "UID", DbType = "VarChar(50)")] string uID, 
            [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "PWD", DbType = "VarChar(50)")] string pWD,
            [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Port", DbType = "Int")] System.Nullable<int> port,
             [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "UserName", DbType = "VarChar(50)")] string UserName,
             [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "UserID", DbType = "Int")] int UserID,
             [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "OldValue", DbType = "VarChar(500)")] string OldValue,
             [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "NewValue", DbType = "VarChar(500)")] string NewValue)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), serverName, enableSSL, uID, pWD, port, UserName, UserID, OldValue, NewValue);
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.rsp_GetMailServerInfo")]
        public ISingleResult<MailServerInfoResult> GetMailServerInfo()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<MailServerInfoResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.usp_UpdateEmailAlertHistoryStatus")]
        public int UpdateEmailAlertHistoryStatus([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ID", DbType = "Int")] System.Nullable<int> iD,
            [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Result", DbType = "VarChar(8000)")] string result,
            [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Status", DbType = "Int")] System.Nullable<int> status)
        {
            IExecuteResult result1 = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), iD, result, status);
            return ((int)(result1.ReturnValue));
        }

    }

    public partial class EmailAlertAuditDetailsResult
    {

        private string _AlertType;

        private string _SiteName;

        private string _SiteCode;

        private short _Status;

        private System.Nullable<System.DateTime> _Date;

        private string _Result;

        private string _Content;

        public EmailAlertAuditDetailsResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_AlertType", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string AlertType
        {
            get
            {
                return this._AlertType;
            }
            set
            {
                if ((this._AlertType != value))
                {
                    this._AlertType = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_SiteName", DbType = "VarChar(50)")]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_SiteCode", DbType = "VarChar(50)")]
        public string SiteCode
        {
            get
            {
                return this._SiteCode;
            }
            set
            {
                if ((this._SiteCode != value))
                {
                    this._SiteCode = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Status", DbType = "SmallInt NOT NULL")]
        public short Status
        {
            get
            {
                return this._Status;
            }
            set
            {
                if ((this._Status != value))
                {
                    this._Status = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Date", DbType = "DateTime")]
        public System.Nullable<System.DateTime> Date
        {
            get
            {
                return this._Date;
            }
            set
            {
                if ((this._Date != value))
                {
                    this._Date = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Result", DbType = "VarChar(2000) NOT NULL", CanBeNull = false)]
        public string Result
        {
            get
            {
                return this._Result;
            }
            set
            {
                if ((this._Result != value))
                {
                    this._Result = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Content", DbType = "VarChar(2000) NOT NULL", CanBeNull = true)]
        public string Content
        {
            get
            {
                return this._Content;
            }
            set
            {
                if ((this._Content != value))
                {
                    this._Content = value;
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

    public partial class GetAlertTypesResult
    {

        private int _ID;

        private string _AlertTypeName;

        public GetAlertTypesResult()
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_AlertTypeName", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string AlertTypeName
        {
            get
            {
                return this._AlertTypeName;
            }
            set
            {
                if ((this._AlertTypeName != value))
                {
                    this._AlertTypeName = value;
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
