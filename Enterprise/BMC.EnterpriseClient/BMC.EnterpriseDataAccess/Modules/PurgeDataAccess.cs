using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;
using System.Data.Linq;
using System.Reflection;

namespace BMC.EnterpriseDataAccess
{
    public partial class EnterpriseDataContext
    {
        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.usp_InsertPurgeCategories")]
        public int InsertPurgeCategories([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "doc", DbType = "VarChar(max)")] string sXML)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), sXML);
            return ((int)(result.ReturnValue));
        }
        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.rsp_GetPurgeTableList")]
        public ISingleResult<PurgeTableListResult> GetPurgeTableList()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<PurgeTableListResult>)(result.ReturnValue));
        }
    }

    public partial class PurgeTableListResult
    {

        private int _PT_Id;

        private string _Tablename;

        private string _Alias;

        public PurgeTableListResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_PT_Id", DbType = "Int NOT NULL")]
        public int PT_Id
        {
            get
            {
                return this._PT_Id;
            }
            set
            {
                if ((this._PT_Id != value))
                {
                    this._PT_Id = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Tablename", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string Tablename
        {
            get
            {
                return this._Tablename;
            }
            set
            {
                if ((this._Tablename != value))
                {
                    this._Tablename = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Alias", DbType = "VarChar(100)")]
        public string Alias
        {
            get
            {
                return this._Alias;
            }
            set
            {
                if ((this._Alias != value))
                {
                    this._Alias = value;
                }
            }
        }
    }
}
