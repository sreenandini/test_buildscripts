using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Reflection;
using System.Text;

namespace BMC.BusinessClasses.BusinessLogic
{
    [global::System.Data.Linq.Mapping.DatabaseAttribute(Name = "Enterprise")]
    public partial class DataHelperClass : System.Data.Linq.DataContext, IDisposable
    {

        private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();

        #region Extensibility Method Definitions
        partial void OnCreated();
        #endregion

       
        public DataHelperClass(string connection) :
            base(connection, mappingSource)
        {
            OnCreated();
        }

        public DataHelperClass(System.Data.IDbConnection connection) :
            base(connection, mappingSource)
        {
            OnCreated();
        }

        public DataHelperClass(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) :
            base(connection, mappingSource)
        {
            OnCreated();
        }

        public DataHelperClass(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) :
            base(connection, mappingSource)
        {
            OnCreated();
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.rsp_GetUnprocessedRecordsForAlert")]
        public ISingleResult<UnprocessedRecordsForAlertResult> GetUnprocessedRecordsForAlert([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "SiteCode", DbType = "VarChar(20)")] string siteCode, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "RecordsCount", DbType = "Int")] System.Nullable<int> recordsCount)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), siteCode, recordsCount);
            return ((ISingleResult<UnprocessedRecordsForAlertResult>)(result.ReturnValue));
        }
        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.usp_UpdateAlertAuditDetails")]
        public int UpdateAlertAuditDetails([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "SiteCode", DbType = "VarChar(20)")] string siteCode,
            [global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "VarChar(MAX)")] string doc,
             [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "APHID", DbType = "BIGINT")] long APHID,
            [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "IsSuccess", DbType = "Int")] ref System.Nullable<int> isSuccess)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), siteCode, doc, APHID,isSuccess);
            isSuccess = ((System.Nullable<int>)(result.GetParameterValue(3)));
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.rsp_GetSiteList")]
        public ISingleResult<SiteListResult> GetSiteList()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<SiteListResult>)(result.ReturnValue));
        }
        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.usp_UpdateAlertHistoryStatus")]
        public int UpdateAlertHistoryStatus([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ID", DbType = "Int")] System.Nullable<int> iD, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Result", DbType = "VarChar(8000)")] string result, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Status", DbType = "Int")] System.Nullable<int> status)
        {
            IExecuteResult result1 = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), iD, result, status);
            return ((int)(result1.ReturnValue));
        }

    }
    public partial class UnprocessedRecordsForAlertResult
    {

        private long _APH_ID;

        private string _APH_Site_Code;

        private string _APH_Type;

        private string _APH_Message;

        private System.Nullable<short> _APH_Status;

        private System.DateTime _APE_Received_Date;

        private System.Nullable<System.DateTime> _APH_Processed_Date;

        private string _APH_Result;

        public UnprocessedRecordsForAlertResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_APH_ID", DbType = "BigInt NOT NULL")]
        public long APH_ID
        {
            get
            {
                return this._APH_ID;
            }
            set
            {
                if ((this._APH_ID != value))
                {
                    this._APH_ID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_APH_Site_Code", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string APH_Site_Code
        {
            get
            {
                return this._APH_Site_Code;
            }
            set
            {
                if ((this._APH_Site_Code != value))
                {
                    this._APH_Site_Code = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_APH_Type", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string APH_Type
        {
            get
            {
                return this._APH_Type;
            }
            set
            {
                if ((this._APH_Type != value))
                {
                    this._APH_Type = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_APH_Message", DbType = "VARCHAR(MAX) NOT NULL", CanBeNull = true)]
        public string APH_Message
        {
            get
            {
                return this._APH_Message;
            }
            set
            {
                if ((this._APH_Message != value))
                {
                    this._APH_Message = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_APH_Status", DbType = "SmallInt")]
        public System.Nullable<short> APH_Status
        {
            get
            {
                return this._APH_Status;
            }
            set
            {
                if ((this._APH_Status != value))
                {
                    this._APH_Status = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_APE_Received_Date", DbType = "DateTime NOT NULL")]
        public System.DateTime APE_Received_Date
        {
            get
            {
                return this._APE_Received_Date;
            }
            set
            {
                if ((this._APE_Received_Date != value))
                {
                    this._APE_Received_Date = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_APH_Processed_Date", DbType = "DateTime")]
        public System.Nullable<System.DateTime> APH_Processed_Date
        {
            get
            {
                return this._APH_Processed_Date;
            }
            set
            {
                if ((this._APH_Processed_Date != value))
                {
                    this._APH_Processed_Date = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_APH_Result", DbType = "VarChar(2000)")]
        public string APH_Result
        {
            get
            {
                return this._APH_Result;
            }
            set
            {
                if ((this._APH_Result != value))
                {
                    this._APH_Result = value;
                }
            }
        }
    }


    public partial class SiteListResult
    {

        private string _SITE_CODE;

        public SiteListResult()
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
}
