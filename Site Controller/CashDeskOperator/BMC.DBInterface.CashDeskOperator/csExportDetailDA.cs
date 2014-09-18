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
    public partial class ExportDetailDA : DataContext
    {
        static MappingSource mappingSource = new AttributeMappingSource();

        #region Extensibility Method Definitions
        partial void OnCreated();
        #endregion

        public ExportDetailDA(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
            this.CommandTimeout = SqlHelper.LoadCommandTimeout();
		}   

        [Function(Name = "dbo.rsp_ReadUnExportedData")]
        public ISingleResult<rsp_ReadUnExportedDataResult> ReadUnExportedData([Parameter(Name = "Type", DbType = "VarChar(30)")] string type)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), type);
            return ((ISingleResult<rsp_ReadUnExportedDataResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetStatusType")]
        public ISingleResult<rsp_GetStatusTypeResult> GetStatusType()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<rsp_GetStatusTypeResult>)(result.ReturnValue));
        }
        [Function(Name = "dbo.usp_UpdateUnExportedData")]
        public int UpdateUnExportedData([Parameter(Name = "New_Status", DbType = "Int")] System.Nullable<int> new_Status, [Parameter(Name = "EH_ID", DbType = "VarChar(MAX)")] string eH_ID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), new_Status, eH_ID);
            return ((int)(result.ReturnValue));
        }
    }

    public partial class rsp_ReadUnExportedDataResult
    {

        private int _ID;

        private System.Nullable<System.DateTime> _Date;

        private string _Reference;

        private string _ExportType;

        private string _Status;

        public rsp_ReadUnExportedDataResult()
        {
        }

        [Column(Storage = "_ID", DbType = "Int NOT NULL")]
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

        [Column(Storage = "_Date", DbType = "DateTime")]
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

        [Column(Storage = "_Reference", DbType = "VarChar(10)")]
        public string Reference
        {
            get
            {
                return this._Reference;
            }
            set
            {
                if ((this._Reference != value))
                {
                    this._Reference = value;
                }
            }
        }

        [Column(Storage = "_ExportType", DbType = "VarChar(30)")]
        public string ExportType
        {
            get
            {
                return this._ExportType;
            }
            set
            {
                if ((this._ExportType != value))
                {
                    this._ExportType = value;
                }
            }
        }

        [Column(Storage = "_Status", DbType = "VarChar(14) NOT NULL", CanBeNull = false)]
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

    public partial class rsp_GetStatusTypeResult
    {

        private int _Type;

        private string _Description;

        public rsp_GetStatusTypeResult()
        {
        }

        [Column(Storage = "_Type", DbType = "Int NOT NULL")]
        public int Type
        {
            get
            {
                return this._Type;
            }
            set
            {
                if ((this._Type != value))
                {
                    this._Type = value;
                }
            }
        }

        [Column(Storage = "_Description", DbType = "VarChar(30) NOT NULL", CanBeNull = false)]
        public string Description
        {
            get
            {
                return this._Description;
            }
            set
            {
                if ((this._Description != value))
                {
                    this._Description = value;
                }
            }
        }
    }
}
