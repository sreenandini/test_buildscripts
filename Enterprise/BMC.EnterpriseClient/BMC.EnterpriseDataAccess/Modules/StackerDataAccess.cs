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
        [Function(Name = "dbo.rsp_IsStackerInUse")]
        public int IsStackerInUse([Parameter(Name = "StackerId", DbType = "Int")] System.Nullable<int> stackerId, [Parameter(Name = "Status", DbType = "Int")] ref System.Nullable<int> status)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), stackerId, status);
            status = ((System.Nullable<int>)(result.GetParameterValue(1)));
            return ((int)(result.ReturnValue));
        }
        
        [Function(Name = "dbo.rsp_GetStackerDetails")]
        public ISingleResult<rsp_GetStackerDetailsResult> GetStackerDetails([Parameter(Name = "StackerCheck", DbType = "Bit")] System.Nullable<bool> stackerCheck)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), stackerCheck);
            return ((ISingleResult<rsp_GetStackerDetailsResult>)(result.ReturnValue));
        }
        [Function(Name = "dbo.rsp_GetActiveStackers")]
        public ISingleResult<rsp_GetStackerDetailsResult> GetActiveStackerDetails()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<rsp_GetStackerDetailsResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.usp_InsertStackerDetails")]
        public int usp_InsertStackerDetails([Parameter(Name = "StackerName", DbType = "VarChar(255)")] string stackerName, [Parameter(Name = "StackerSize", DbType = "Int")] System.Nullable<int> stackerSize, [Parameter(Name = "StackerDescription", DbType = "VarChar(255)")] string stackerDescription, [Parameter(Name = "StackerStatus", DbType = "Bit")] System.Nullable<bool> stackerStatus)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), stackerName, stackerSize, stackerDescription, stackerStatus);
            return ((int)(result.ReturnValue));
        }
        [Function(Name = "dbo.usp_DeleteStackerDetails")]
        public int usp_DeleteStackerDetails([Parameter(Name = "StackerId", DbType = "Int")] System.Nullable<int> stackerId, [Parameter(Name = "Status", DbType = "Int")] ref System.Nullable<int> status)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), stackerId, status);
            status = ((System.Nullable<int>)(result.GetParameterValue(1)));
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.usp_UpdateStackerDetails")]
        public int usp_UpdateStackerDetails([Parameter(Name = "StackerID", DbType = "Int")] System.Nullable<int> stackerID, [Parameter(Name = "StackerName", DbType = "VarChar(255)")] string stackerName, [Parameter(Name = "StackerSize", DbType = "Int")] System.Nullable<int> stackerSize, [Parameter(Name = "StackerDescription", DbType = "VarChar(255)")] string stackerDescription, [Parameter(Name = "StackerStatus", DbType = "Bit")] System.Nullable<bool> stackerStatus)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), stackerID, stackerName, stackerSize, stackerDescription, stackerStatus);
            return ((int)(result.ReturnValue));
        }
        [Function(Name = "dbo.rsp_CheckStackerNameExists")]
        public int rsp_CheckStackerNameExists([Parameter(Name = "StackerName", DbType = "VarChar(255)")] string stackerName, [Parameter(Name = "NameCount", DbType = "Int")] ref System.Nullable<int> nameCount, [Parameter(Name = "StackerID", DbType = "Int")] System.Nullable<int> stackerID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), stackerName, nameCount, stackerID);
            nameCount = ((System.Nullable<int>)(result.GetParameterValue(1)));
            return ((int)(result.ReturnValue));
        }
    }

    public partial class rsp_GetStackerDetailsResult
    {

        private int _Stacker_Id;

        private string _StackerName;

        private int _StackerSize;

        private bool _StackerStatus;

        private string _StackerDescription;

        public rsp_GetStackerDetailsResult()
        {
        }

        [Column(Storage = "_Stacker_Id", DbType = "Int NOT NULL")]
        public int Stacker_Id
        {
            get
            {
                return this._Stacker_Id;
            }
            set
            {
                if ((this._Stacker_Id != value))
                {
                    this._Stacker_Id = value;
                }
            }
        }

        [Column(Storage = "_StackerName", DbType = "VarChar(255) NOT NULL", CanBeNull = false)]
        public string StackerName
        {
            get
            {
                return this._StackerName;
            }
            set
            {
                if ((this._StackerName != value))
                {
                    this._StackerName = value;
                }
            }
        }

        [Column(Storage = "_StackerSize", DbType = "Int NOT NULL")]
        public int StackerSize
        {
            get
            {
                return this._StackerSize;
            }
            set
            {
                if ((this._StackerSize != value))
                {
                    this._StackerSize = value;
                }
            }
        }

        [Column(Storage = "_StackerStatus", DbType = "Bit NOT NULL")]
        public bool StackerStatus
        {
            get
            {
                return this._StackerStatus;
            }
            set
            {
                if ((this._StackerStatus != value))
                {
                    this._StackerStatus = value;
                }
            }
        }

        [Column(Storage = "_StackerDescription", DbType = "VarChar(255)")]
        public string StackerDescription
        {
            get
            {
                return this._StackerDescription;
            }
            set
            {
                if ((this._StackerDescription != value))
                {
                    this._StackerDescription = value;
                }
            }
        }
    }


   
}
