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
        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.rsp_GetCallGroups")]
        public ISingleResult<rsp_GetCallGroupsResult> rsp_GetCallGroups()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<rsp_GetCallGroupsResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.rsp_GetCallRemedy")]
        public ISingleResult<rsp_GetCallRemedyResult> rsp_GetCallRemedy()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<rsp_GetCallRemedyResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.rsp_GetCallSource")]
        public ISingleResult<rsp_GetCallSourceResult> rsp_GetCallSource()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<rsp_GetCallSourceResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.rsp_GetCallFaultsByGroupID")]
        public ISingleResult<rsp_GetCallFaultsByGroupIDResult> rsp_GetCallFaultsByGroupID([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "GroupID", DbType = "Int")] System.Nullable<int> groupID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), groupID);
            return ((ISingleResult<rsp_GetCallFaultsByGroupIDResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.usp_InsertOrUpdateCallSource")]
        public int usp_InsertOrUpdateCallSource([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "SourceId", DbType = "Int")] System.Nullable<int> sourceId, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Description", DbType = "VarChar(30)")] string description, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Reference", DbType = "VarChar(10)")] string reference, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "SourceIdOut", DbType = "Int")] ref System.Nullable<int> sourceIdOut)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), sourceId, description, reference, sourceIdOut);
            sourceIdOut = ((System.Nullable<int>)(result.GetParameterValue(3)));
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.usp_InsertOrUpdateCallFault")]
        public int usp_InsertOrUpdateCallFault([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "FaultId", DbType = "Int")] System.Nullable<int> faultId, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "GroupId", DbType = "Int")] System.Nullable<int> groupId, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Description", DbType = "VarChar(50)")] string description, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Reference", DbType = "VarChar(20)")] string reference, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "EndDate", DbType = "DateTime")] System.Nullable<System.DateTime> endDate, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "FaultIdOut", DbType = "Int")] ref System.Nullable<int> faultIdOut)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), faultId, groupId, description, reference, endDate, faultIdOut);
            faultIdOut = ((System.Nullable<int>)(result.GetParameterValue(5)));
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.usp_InsertOrUpdateCallGroup")]
        public int usp_InsertOrUpdateCallGroup([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "GroupId", DbType = "Int")] System.Nullable<int> groupId, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Description", DbType = "VarChar(50)")] string description, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Reference", DbType = "VarChar(50)")] string reference, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "IsDowntime", DbType = "Bit")] System.Nullable<bool> isDowntime, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "IsLogEngineerChange", DbType = "Bit")] System.Nullable<bool> isLogEngineerChange, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "EndDate", DbType = "DateTime")] System.Nullable<System.DateTime> endDate, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "GroupIDOut", DbType = "Int")] ref System.Nullable<int> groupIDOut)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), groupId, description, reference, isDowntime, isLogEngineerChange, endDate, groupIDOut);
            groupIDOut = ((System.Nullable<int>)(result.GetParameterValue(6)));
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.usp_InsertOrUpdateCallRemedy")]
        public int usp_InsertOrUpdateCallRemedy([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "RemedyId", DbType = "Int")] System.Nullable<int> remedyId, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Description", DbType = "VarChar(50)")] string description, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Reference", DbType = "Int")] System.Nullable<int> reference, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "IsDowntime", DbType = "Bit")] System.Nullable<bool> isDowntime, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "EndDate", DbType = "DateTime")] System.Nullable<System.DateTime> endDate, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "RemedyIdOut", DbType = "Int")] ref System.Nullable<int> remedyIdOut)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), remedyId, description, reference, isDowntime, endDate, remedyIdOut);
            remedyIdOut = ((System.Nullable<int>)(result.GetParameterValue(5)));
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.rsp_CanRemoveCallGroup")]
        public int rsp_CanRemoveCallGroup([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Call_Group_ID", DbType = "Int")] System.Nullable<int> call_Group_ID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Result", DbType = "Bit")] ref System.Nullable<bool> result)
        {
            IExecuteResult result1 = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), call_Group_ID, result);
            result = ((System.Nullable<bool>)(result1.GetParameterValue(1)));
            return ((int)(result1.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.rsp_CanRemoveCallFault")]
        public int rsp_CanRemoveCallFault([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Call_Fault_ID", DbType = "Int")] System.Nullable<int> call_Fault_ID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Call_Group_ID", DbType = "Int")] System.Nullable<int> call_Group_ID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Result", DbType = "Bit")] ref System.Nullable<bool> result)
        {
            IExecuteResult result1 = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), call_Fault_ID, call_Group_ID, result);
            result = ((System.Nullable<bool>)(result1.GetParameterValue(2)));
            return ((int)(result1.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.rsp_CanRemoveCallRemedy")]
        public int rsp_CanRemoveCallRemedy([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Call_Remedy_ID", DbType = "Int")] System.Nullable<int> call_Remedy_ID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Result", DbType = "Bit")] ref System.Nullable<bool> result)
        {
            IExecuteResult result1 = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), call_Remedy_ID, result);
            result = ((System.Nullable<bool>)(result1.GetParameterValue(1)));
            return ((int)(result1.ReturnValue));
        }       

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.rsp_CanRemoveCallSource")]
        public int rsp_CanRemoveCallSource([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Call_Source_ID", DbType = "Int")] System.Nullable<int> call_Source_ID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Result", DbType = "Bit")] ref System.Nullable<bool> result)
        {
            IExecuteResult result1 = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), call_Source_ID, result);
            result = ((System.Nullable<bool>)(result1.GetParameterValue(1)));
            return ((int)(result1.ReturnValue));
        }
    }

    public partial class rsp_GetCallGroupsResult
    {

        private int _Call_Group_ID;

        private string _Call_Group_Description;

        private string _Call_Group_Reference;

        private System.Nullable<bool> _Call_Group_Downtime;

        private System.Nullable<System.DateTime> _Call_Group_End_Date;

        private System.Nullable<bool> _Call_Group_Log_Engineer_Change;

        public rsp_GetCallGroupsResult()
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Call_Group_Reference", DbType = "VarChar(50)")]
        public string Call_Group_Reference
        {
            get
            {
                return this._Call_Group_Reference;
            }
            set
            {
                if ((this._Call_Group_Reference != value))
                {
                    this._Call_Group_Reference = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Call_Group_Downtime", DbType = "Bit")]
        public System.Nullable<bool> Call_Group_Downtime
        {
            get
            {
                return this._Call_Group_Downtime;
            }
            set
            {
                if ((this._Call_Group_Downtime != value))
                {
                    this._Call_Group_Downtime = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Call_Group_End_Date", DbType = "DateTime")]
        public System.Nullable<System.DateTime> Call_Group_End_Date
        {
            get
            {
                return this._Call_Group_End_Date;
            }
            set
            {
                if ((this._Call_Group_End_Date != value))
                {
                    this._Call_Group_End_Date = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Call_Group_Log_Engineer_Change", DbType = "Bit")]
        public System.Nullable<bool> Call_Group_Log_Engineer_Change
        {
            get
            {
                return this._Call_Group_Log_Engineer_Change;
            }
            set
            {
                if ((this._Call_Group_Log_Engineer_Change != value))
                {
                    this._Call_Group_Log_Engineer_Change = value;
                }
            }
        }
    }

    public partial class rsp_GetCallRemedyResult
    {

        private int _Call_Remedy_ID;

        private string _Call_Remedy_Description;

        private System.Nullable<int> _Call_Remedy_Reference;

        private System.Nullable<bool> _Call_Remedy_Attract_Downtime;

        private System.Nullable<System.DateTime> _Call_Remedy_End_Date;

        public rsp_GetCallRemedyResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Call_Remedy_ID", DbType = "Int NOT NULL")]
        public int Call_Remedy_ID
        {
            get
            {
                return this._Call_Remedy_ID;
            }
            set
            {
                if ((this._Call_Remedy_ID != value))
                {
                    this._Call_Remedy_ID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Call_Remedy_Description", DbType = "VarChar(50)")]
        public string Call_Remedy_Description
        {
            get
            {
                return this._Call_Remedy_Description;
            }
            set
            {
                if ((this._Call_Remedy_Description != value))
                {
                    this._Call_Remedy_Description = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Call_Remedy_Reference", DbType = "Int")]
        public System.Nullable<int> Call_Remedy_Reference
        {
            get
            {
                return this._Call_Remedy_Reference;
            }
            set
            {
                if ((this._Call_Remedy_Reference != value))
                {
                    this._Call_Remedy_Reference = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Call_Remedy_Attract_Downtime", DbType = "Bit")]
        public System.Nullable<bool> Call_Remedy_Attract_Downtime
        {
            get
            {
                return this._Call_Remedy_Attract_Downtime;
            }
            set
            {
                if ((this._Call_Remedy_Attract_Downtime != value))
                {
                    this._Call_Remedy_Attract_Downtime = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Call_Remedy_End_Date", DbType = "DateTime")]
        public System.Nullable<System.DateTime> Call_Remedy_End_Date
        {
            get
            {
                return this._Call_Remedy_End_Date;
            }
            set
            {
                if ((this._Call_Remedy_End_Date != value))
                {
                    this._Call_Remedy_End_Date = value;
                }
            }
        }
    }

    public partial class rsp_GetCallSourceResult
    {

        private int _Call_Source_ID;

        private string _Call_Source_Description;

        private string _Call_Source_Reference;

        public rsp_GetCallSourceResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Call_Source_ID", DbType = "Int NOT NULL")]
        public int Call_Source_ID
        {
            get
            {
                return this._Call_Source_ID;
            }
            set
            {
                if ((this._Call_Source_ID != value))
                {
                    this._Call_Source_ID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Call_Source_Description", DbType = "VarChar(30)")]
        public string Call_Source_Description
        {
            get
            {
                return this._Call_Source_Description;
            }
            set
            {
                if ((this._Call_Source_Description != value))
                {
                    this._Call_Source_Description = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Call_Source_Reference", DbType = "VarChar(10)")]
        public string Call_Source_Reference
        {
            get
            {
                return this._Call_Source_Reference;
            }
            set
            {
                if ((this._Call_Source_Reference != value))
                {
                    this._Call_Source_Reference = value;
                }
            }
        }
    }

    public partial class rsp_GetCallFaultsByGroupIDResult
    {

        private int _Call_Fault_ID;

        private int _Call_Group_ID;

        private string _Call_Fault_Description;

        private string _Call_Fault_Reference;

        private System.Nullable<System.DateTime> _Call_Fault_End_Date;

        public rsp_GetCallFaultsByGroupIDResult()
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Call_Fault_Reference", DbType = "VarChar(20)")]
        public string Call_Fault_Reference
        {
            get
            {
                return this._Call_Fault_Reference;
            }
            set
            {
                if ((this._Call_Fault_Reference != value))
                {
                    this._Call_Fault_Reference = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Call_Fault_End_Date", DbType = "DateTime")]
        public System.Nullable<System.DateTime> Call_Fault_End_Date
        {
            get
            {
                return this._Call_Fault_End_Date;
            }
            set
            {
                if ((this._Call_Fault_End_Date != value))
                {
                    this._Call_Fault_End_Date = value;
                }
            }
        }
    }
}
