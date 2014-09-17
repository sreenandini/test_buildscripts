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
        
        [Function(Name = "dbo.usp_UpdateServiceCallDetails")]
        public int UpdateServiceCallDetails([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "FaultName", DbType = "VarChar(100)")] string faultName, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ToMail", DbType = "Int")] System.Nullable<int> toMail)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), faultName, toMail);
            return ((int)(result.ReturnValue));
        }
        [Function(Name = "dbo.rsp_GetAllFaultGroup")]
        public ISingleResult<rsp_GetAllFaultGroupResult> GetAllFaultGroup()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<rsp_GetAllFaultGroupResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetAllFaultsByGroup")]
        public ISingleResult<rsp_GetAllFaultsByGroupResult> GetAllFaultsByGroup([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "FaultGroupID", DbType = "Int")] System.Nullable<int> faultGroupID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), faultGroupID);
            return ((ISingleResult<rsp_GetAllFaultsByGroupResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetAllGMUConfiguration")]
        public ISingleResult<rsp_GetAllGMUConfigurationResult> GetAllGMUConfiguration()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<rsp_GetAllGMUConfigurationResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.usp_UpdateMailConfiguration")]
        public int UpdateMailDetailsInGMUConfiguration([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ID", DbType = "Int")] System.Nullable<int> iD, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "To", DbType = "VarChar(1000)")] string to, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "CC", DbType = "VarChar(1000)")] string cC)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), iD, to, cC);
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.usp_UpdateGMUConfiguration")]
        public int UpdateGMUConfiguration([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Description", DbType = "VarChar(50)")] string description, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "CreateServiceCall", DbType = "Bit")] System.Nullable<bool> createServiceCall, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "CloseServiceCall", DbType = "Bit")] System.Nullable<bool> closeServiceCall, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ToMail", DbType = "Bit")] System.Nullable<bool> toMail, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Fault", DbType = "VarChar(60)")] string fault, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Type", DbType = "Int")] System.Nullable<int> type, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Datapak_Fault_ID", DbType = "Int")] System.Nullable<int> datapak_Fault_ID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Call_Fault_ID", DbType = "Int")] System.Nullable<int> call_Fault_ID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), description, createServiceCall, closeServiceCall, toMail, fault, type, datapak_Fault_ID, call_Fault_ID);
            return ((int)(result.ReturnValue));
        }
        
    }
    public partial class rsp_GetAllGMUConfigurationResult
    {

        private System.Nullable<int> _Code;

        private System.Nullable<int> _subcode;

        private System.Nullable<int> _SourceProtocol;

        private string _Fault;

        private System.Nullable<bool> _CreateServiceCall;

        private System.Nullable<bool> _CloseServiceCall;

        private System.Nullable<int> _SourceID;

        private System.Nullable<int> _Type;

        private string _Description;

        private System.Nullable<bool> _ToMail;

        private int _Datapak_Fault_ID;

        private System.Nullable<int> _Call_Fault_ID;

        private string _Mail_CC;

        private string _Mail_TO;

        private System.Nullable<int> _Call_Group_ID;

        public rsp_GetAllGMUConfigurationResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Code", DbType = "Int")]
        public System.Nullable<int> Code
        {
            get
            {
                return this._Code;
            }
            set
            {
                if ((this._Code != value))
                {
                    this._Code = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_subcode", DbType = "Int")]
        public System.Nullable<int> subcode
        {
            get
            {
                return this._subcode;
            }
            set
            {
                if ((this._subcode != value))
                {
                    this._subcode = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_SourceProtocol", DbType = "Int")]
        public System.Nullable<int> SourceProtocol
        {
            get
            {
                return this._SourceProtocol;
            }
            set
            {
                if ((this._SourceProtocol != value))
                {
                    this._SourceProtocol = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Fault", DbType = "VarChar(50)")]
        public string Fault
        {
            get
            {
                return this._Fault;
            }
            set
            {
                if ((this._Fault != value))
                {
                    this._Fault = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CreateServiceCall", DbType = "Bit")]
        public System.Nullable<bool> CreateServiceCall
        {
            get
            {
                return this._CreateServiceCall;
            }
            set
            {
                if ((this._CreateServiceCall != value))
                {
                    this._CreateServiceCall = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CloseServiceCall", DbType = "Bit")]
        public System.Nullable<bool> CloseServiceCall
        {
            get
            {
                return this._CloseServiceCall;
            }
            set
            {
                if ((this._CloseServiceCall != value))
                {
                    this._CloseServiceCall = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_SourceID", DbType = "Int")]
        public System.Nullable<int> SourceID
        {
            get
            {
                return this._SourceID;
            }
            set
            {
                if ((this._SourceID != value))
                {
                    this._SourceID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Type", DbType = "Int")]
        public System.Nullable<int> Type
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Description", DbType = "VarChar(60)")]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ToMail", DbType = "Bit")]
        public System.Nullable<bool> ToMail
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Datapak_Fault_ID", DbType = "Int NOT NULL")]
        public int Datapak_Fault_ID
        {
            get
            {
                return this._Datapak_Fault_ID;
            }
            set
            {
                if ((this._Datapak_Fault_ID != value))
                {
                    this._Datapak_Fault_ID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Call_Fault_ID", DbType = "Int")]
        public System.Nullable<int> Call_Fault_ID
        {
            get
            {
                return this._Call_Fault_ID;
            }
            set
            {
                if ((this._Call_Fault_ID != value))
                {
                    this._Call_Fault_ID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Mail_CC", DbType = "VarChar(500)")]
        public string Mail_CC
        {
            get
            {
                return this._Mail_CC;
            }
            set
            {
                if ((this._Mail_CC != value))
                {
                    this._Mail_CC = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Mail_TO", DbType = "VarChar(500)")]
        public string Mail_TO
        {
            get
            {
                return this._Mail_TO;
            }
            set
            {
                if ((this._Mail_TO != value))
                {
                    this._Mail_TO = value;
                }
            }
        }
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Call_Group_ID", DbType = "Int")]
        public System.Nullable<int> Call_Group_ID
        {
            get
            {
                return this._Call_Group_ID;
            }
            set
            {
                if ((this._Call_Group_ID != value))
                {
                    this._Call_Group_ID = value;
                }
            }
        }
    }
    public partial class rsp_GetAllFaultsByGroupResult
    {

        private int _Call_Fault_ID;

        private string _Call_Fault_Description;

        public rsp_GetAllFaultsByGroupResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Call_Fault_ID", DbType = "Int NOT NULL")]
        public int Call_Fault_ID
        {
            get
            {
                return this._Call_Fault_ID;
            }
            set
            {
                if ((this._Call_Fault_ID != value))
                {
                    this._Call_Fault_ID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Call_Fault_Description", DbType = "VarChar(50)")]
        public string Call_Fault_Description
        {
            get
            {
                return this._Call_Fault_Description;
            }
            set
            {
                if ((this._Call_Fault_Description != value))
                {
                    this._Call_Fault_Description = value;
                }
            }
        }
    }
    public partial class rsp_GetAllFaultGroupResult
    {

        private int _Call_Group_ID;

        private string _Call_Group_Description;

        public rsp_GetAllFaultGroupResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Call_Group_ID", DbType = "Int NOT NULL")]
        public int Call_Group_ID
        {
            get
            {
                return this._Call_Group_ID;
            }
            set
            {
                if ((this._Call_Group_ID != value))
                {
                    this._Call_Group_ID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Call_Group_Description", DbType = "VarChar(50)")]
        public string Call_Group_Description
        {
            get
            {
                return this._Call_Group_Description;
            }
            set
            {
                if ((this._Call_Group_Description != value))
                {
                    this._Call_Group_Description = value;
                }
            }
        }
    }

}
