using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Reflection;

namespace BMC.EnterpriseDataAccess
{
    public partial class EnterpriseDataContext
    {
        [Function(Name = "dbo.rsp_TransactionKeyStatus")]
        public ISingleResult<rsp_TransactionKeyStatusResult> GetTransactionKeyStatus([Parameter(Name = "Siteid", DbType = "Int")] System.Nullable<int> siteid)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), siteid);
            return ((ISingleResult<rsp_TransactionKeyStatusResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.usp_UpdateAuthorizationKey")]
        public int UpdateAuthorizationKey([Parameter(Name = "SiteID", DbType = "Int")] System.Nullable<int> siteID, [Parameter(Name = "UserId", DbType = "Int")] System.Nullable<int> userId, [Parameter(Name = "Type", DbType = "Int")] System.Nullable<int> type, [Parameter(Name = "TransactionKey", DbType = "VarChar(200)")] string transactionKey)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), siteID, userId, type, transactionKey);
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.usp_UpdateTransactionKeyStatus")]
        public int UpdateTransactionKeyStatus([Parameter(Name = "TransactionKeyId", DbType = "Int")] System.Nullable<int> transactionKeyId, [Parameter(Name = "UserID", DbType = "Int")] System.Nullable<int> userID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), transactionKeyId, userID);
            return ((int)(result.ReturnValue));
        }
    }

    public partial class rsp_TransactionKeyStatusResult
    {

        private int _TransactionKeyId;

        private string _TransactionKey;

        private string _TransactionFlagName;

        private System.DateTime _CreatedDate;

        private System.Nullable<System.DateTime> _ExpiryDate;

        private string _Staff_First_Name;

        private string _Staff_Last_Name;

        private string _Status;

        public rsp_TransactionKeyStatusResult()
        {
        }

        [Column(Storage = "_TransactionKeyId", DbType = "Int NOT NULL")]
        public int TransactionKeyId
        {
            get
            {
                return this._TransactionKeyId;
            }
            set
            {
                if ((this._TransactionKeyId != value))
                {
                    this._TransactionKeyId = value;
                }
            }
        }

        [Column(Storage = "_TransactionKey", DbType = "VarChar(200) NOT NULL", CanBeNull = false)]
        public string TransactionKey
        {
            get
            {
                return this._TransactionKey;
            }
            set
            {
                if ((this._TransactionKey != value))
                {
                    this._TransactionKey = value;
                }
            }
        }

        [Column(Storage = "_TransactionFlagName", DbType = "VarChar(200) NOT NULL", CanBeNull = false)]
        public string TransactionFlagName
        {
            get
            {
                return this._TransactionFlagName;
            }
            set
            {
                if ((this._TransactionFlagName != value))
                {
                    this._TransactionFlagName = value;
                }
            }
        }

        [Column(Storage = "_CreatedDate", DbType = "DateTime NOT NULL")]
        public System.DateTime CreatedDate
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

        [Column(Storage = "_ExpiryDate", DbType = "DateTime")]
        public System.Nullable<System.DateTime> ExpiryDate
        {
            get
            {
                return this._ExpiryDate;
            }
            set
            {
                if ((this._ExpiryDate != value))
                {
                    this._ExpiryDate = value;
                }
            }
        }

        [Column(Storage = "_Staff_First_Name", DbType = "VarChar(50)")]
        public string Staff_First_Name
        {
            get
            {
                return this._Staff_First_Name;
            }
            set
            {
                if ((this._Staff_First_Name != value))
                {
                    this._Staff_First_Name = value;
                }
            }
        }

        [Column(Storage = "_Staff_Last_Name", DbType = "VarChar(50)")]
        public string Staff_Last_Name
        {
            get
            {
                return this._Staff_Last_Name;
            }
            set
            {
                if ((this._Staff_Last_Name != value))
                {
                    this._Staff_Last_Name = value;
                }
            }
        }

        [Column(Storage = "_Status", DbType = "VarChar(6) NOT NULL", CanBeNull = false)]
        public string Status
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
    }
}
