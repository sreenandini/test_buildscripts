using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;
using System.Data.Linq;
using BMC.DataAccess;
using System.Reflection;

namespace BMC.DBInterface.CashDeskOperator
{
    [DatabaseAttribute(Name = "Exchange")]
    public partial class ApplicationLockDataAccess : DataContext
    {
        static MappingSource mappingSource = new AttributeMappingSource();

        #region Extensibility Method Definitions
        partial void OnCreated();
        #endregion

        public ApplicationLockDataAccess(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
            this.CommandTimeout = SqlHelper.LoadCommandTimeout();
		}

        [Function(Name = "dbo.usp_UpdateAppLockState")]
        public int UpdateAppLockState([Parameter(Name = "Lock_IDs", DbType = "VarChar(MAX)")] string lock_IDs)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), lock_IDs);
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetLockTypes")]
        [ResultType(typeof(LockTypesResult))]
        [ResultType(typeof(ApplicationTypesResult))]
        public IMultipleResults GetLockTypes()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return (IMultipleResults)result.ReturnValue;
        }

        [Function(Name = "dbo.rsp_GetLockDetails")]
        public ISingleResult<rsp_GetLockDetailsResult> GetLockDetails([Parameter(Name = "Lock_Application", DbType = "VarChar(50)")] string lock_Application, [Parameter(Name = "Lock_Type", DbType = "VarChar(50)")] string lock_Type)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), lock_Application, lock_Type);
            return ((ISingleResult<rsp_GetLockDetailsResult>)(result.ReturnValue));
        }
    }

    public partial class ApplicationTypesResult
    {

        private string _Lock_Application;

        public ApplicationTypesResult()
        {
        }

        [Column(Storage = "_Lock_Application", DbType = "VarChar(50)")]
        public string Lock_Application
        {
            get
            {
                return this._Lock_Application;
            }
            set
            {
                if ((this._Lock_Application != value))
                {
                    this._Lock_Application = value;
                }
            }
        }
    }

    public partial class LockTypesResult
    {

        private string _Lock_Type;

        public LockTypesResult()
        {
        }

        [Column(Storage = "_Lock_Type", DbType = "VarChar(50)")]
        public string Lock_Type
        {
            get
            {
                return this._Lock_Type;
            }
            set
            {
                if ((this._Lock_Type != value))
                {
                    this._Lock_Type = value;
                }
            }
        }
    }

    public partial class rsp_GetLockDetailsResult
    {

        private int _Lock_ID;

        private string _Lock_User;

        private string _Lock_Machine;

        private string _Lock_Application;

        private string _Lock_Type;

        private string _Lock_Identifier;

        private System.Nullable<System.DateTime> _Lock_Created;

        public rsp_GetLockDetailsResult()
        {
        }

        [Column(Storage = "_Lock_ID", DbType = "Int NOT NULL")]
        public int Lock_ID
        {
            get
            {
                return this._Lock_ID;
            }
            set
            {
                if ((this._Lock_ID != value))
                {
                    this._Lock_ID = value;
                }
            }
        }

        [Column(Storage = "_Lock_User", DbType = "VarChar(50)")]
        public string Lock_User
        {
            get
            {
                return this._Lock_User;
            }
            set
            {
                if ((this._Lock_User != value))
                {
                    this._Lock_User = value;
                }
            }
        }

        [Column(Storage = "_Lock_Machine", DbType = "VarChar(50)")]
        public string Lock_Machine
        {
            get
            {
                return this._Lock_Machine;
            }
            set
            {
                if ((this._Lock_Machine != value))
                {
                    this._Lock_Machine = value;
                }
            }
        }

        [Column(Storage = "_Lock_Application", DbType = "VarChar(50)")]
        public string Lock_Application
        {
            get
            {
                return this._Lock_Application;
            }
            set
            {
                if ((this._Lock_Application != value))
                {
                    this._Lock_Application = value;
                }
            }
        }

        [Column(Storage = "_Lock_Type", DbType = "VarChar(50)")]
        public string Lock_Type
        {
            get
            {
                return this._Lock_Type;
            }
            set
            {
                if ((this._Lock_Type != value))
                {
                    this._Lock_Type = value;
                }
            }
        }

        [Column(Storage = "_Lock_Identifier", DbType = "VarChar(100)")]
        public string Lock_Identifier
        {
            get
            {
                return this._Lock_Identifier;
            }
            set
            {
                if ((this._Lock_Identifier != value))
                {
                    this._Lock_Identifier = value;
                }
            }
        }

        [Column(Storage = "_Lock_Created", DbType = "DateTime")]
        public System.Nullable<System.DateTime> Lock_Created
        {
            get
            {
                return this._Lock_Created;
            }
            set
            {
                if ((this._Lock_Created != value))
                {
                    this._Lock_Created = value;
                }
            }
        }
    }
}
