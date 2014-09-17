using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;
using System.Reflection;
using System.Data.Linq;

namespace BMC.EnterpriseDataAccess
{
    public partial class EnterpriseDataContext
    {
        [Function(Name = "dbo.rsp_GetTermsGroup")]
        public ISingleResult<TermsGroupResult> GetTermsGroup()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<TermsGroupResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.usp_InsertOrUpdateTermsGroup")]
        public int InsertOrUpdateTermsGroup([Parameter(Name = "TermsGroupName", DbType = "VarChar(50)")] string TermsGroupName, [Parameter(Name = "TermsGroupID", DbType = "Int")] System.Nullable<int> TermsGroupID, [Parameter(Name = "TermsGroupIDOut", DbType = "Int")] ref System.Nullable<int> TermsGroupIDOut)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), TermsGroupName, TermsGroupID, TermsGroupIDOut);
            TermsGroupIDOut = ((System.Nullable<int>)(result.GetParameterValue(2)));
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.usp_InsertTermsProfiles")]
        public int InsertTermsProfiles([Parameter(Name = "TermGroupID", DbType = "Int")] System.Nullable<int> termGroupID, [Parameter(Name = "MachineID", DbType = "Int")] System.Nullable<int> machineID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), termGroupID, machineID);
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetTermsProfileForGroupID")]
        public ISingleResult<TermsProfilesResultForGroupID> GetTermsProfileResultForGroupID([Parameter(Name = "SelectedTermGroupID", DbType = "Int")] System.Nullable<int> selectedTermGroupID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), selectedTermGroupID);
            return ((ISingleResult<TermsProfilesResultForGroupID>)(result.ReturnValue));
        }

        [Function(Name = "dbo.[rsp_GetUnassignedMachineTypes]")]
        public ISingleResult<UnAssignedMachineTypes> GetUnassignedMachineTypes([Parameter(Name = "TermsGroupID", DbType = "Int")] System.Nullable<int> TermsGroupID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), TermsGroupID);
            return ((ISingleResult<UnAssignedMachineTypes>)(result.ReturnValue));
        }

        [Function(Name = "dbo.usp_DeleteTermsProfiles")]
        public int DeleteTermsProfiles([Parameter(Name = "Terms_Profile_ID", DbType = "Int")] System.Nullable<int> termsProfileID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), termsProfileID);
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.usp_CopyTermsGroupProfile")]
        public int CopyTermsGroupProfile([Parameter(Name = "TermsGroupName", DbType = "VarChar(50)")] string termsGroupName, [Parameter(Name = "TermGroupID", DbType = "Int")] System.Nullable<int> _termsGroupToCopy)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), termsGroupName, _termsGroupToCopy);
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetTermsGroupCountForInstallation")]
        public ISingleResult<InstallationCount> GetTermsGroupCountForInstallation([Parameter(Name = "TermsGroupID", DbType = "Int")] System.Nullable<int> TermsGroupID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), TermsGroupID);
            return ((ISingleResult<InstallationCount>)result.ReturnValue);
        }

        [Function(Name = "dbo.usp_DeleteTermsProfilesAndGroup")]
        public int DeleteTermsGroup([Parameter(Name = "TermsGroupID", DbType = "Int")] System.Nullable<int> TermGroupID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), TermGroupID);
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.usp_AddOrCopyTermsProfilesForMachineTypes")]
        public int AddOrCopyTermsProfileForMachineTypes([Parameter(Name = "MachineTypeID", DbType = "Int")] System.Nullable<int> MachineTypeID, [Parameter(Name = "TermsGroupID", DbType = "Int")] System.Nullable<int> TermsGroupID, [Parameter(Name = "TermsProfileID", DbType = "Int")] System.Nullable<int> TermsProfileID, [Parameter(Name = "ProcessType", DbType = "VarChar(10)")] string processType)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), MachineTypeID, TermsGroupID, TermsProfileID, processType);
            return ((int)(result.ReturnValue));
        }

        public partial class TermsGroupResult
        {
            private int _Terms_Group_ID;

            private string _Terms_Group_Name;

            public TermsGroupResult()
            {
            }

            [Column(Storage = "_Terms_Group_ID", DbType = "Int NOT NULL")]
            public int Terms_Group_ID
            {
                get
                {
                    return this._Terms_Group_ID;
                }
                set
                {
                    if ((this._Terms_Group_ID != value))
                    {
                        this._Terms_Group_ID = value;
                    }
                }
            }

            [Column(Storage = "_Terms_Group_Name", DbType = "VarChar(50)")]
            public string Terms_Group_Name
            {
                get
                {
                    return this._Terms_Group_Name;
                }
                set
                {
                    if ((this._Terms_Group_Name != value))
                    {
                        this._Terms_Group_Name = value;
                    }
                }
            }
        }

        public partial class TermsProfilesResultForGroupID
        {
            private System.Nullable<int> _Machine_Type_ID;

            private int _Terms_Profile_ID;

            private string _Machine_Type_Code;

            public TermsProfilesResultForGroupID()
            {
            }

            [Column(Storage = "_Machine_Type_ID", DbType = "INT")]
            public System.Nullable<int> Machine_Type_ID
            {
                get
                {
                    return this._Machine_Type_ID;
                }
                set
                {
                    if ((this._Machine_Type_ID != value))
                    {
                        this._Machine_Type_ID = value;
                    }
                }
            }

            [Column(Storage = "_Terms_Profile_ID", DbType = "INT NOT NULL")]
            public int Terms_Profile_ID
            {
                get
                {
                    return this._Terms_Profile_ID;
                }
                set
                {
                    if ((this._Terms_Profile_ID != value))
                    {
                        this._Terms_Profile_ID = value;
                    }
                }
            }

            [Column(Storage = "_Machine_Type_Code", DbType = "VarChar(50)")]
            public string Machine_Type_Code
            {
                get
                {
                    return this._Machine_Type_Code;
                }
                set
                {
                    if ((this._Machine_Type_Code != value))
                    {
                        this._Machine_Type_Code = value;
                    }
                }
            }
        }


        public partial class InstallationCount
        {

            private System.Nullable<int> _active;

            private System.Nullable<int> _inactive;


            [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_active", DbType = "Int")]
            public System.Nullable<int> active
            {
                get
                {
                    return this._active;
                }
                set
                {
                    if ((this._active != value))
                    {
                        this._active = value;
                    }
                }
            }

            [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_inactive", DbType = "Int")]
            public System.Nullable<int> inactive
            {
                get
                {
                    return this._inactive;
                }
                set
                {
                    if ((this._inactive != value))
                    {
                        this._inactive = value;
                    }
                }
            }
        }

        public partial class UnAssignedMachineTypes
        {

            private System.Nullable<int> _Machine_Type_ID;

            private string _Machine_Type_Code;

            public UnAssignedMachineTypes()
            {
            }

            [Column(Storage = "_Machine_Type_ID", DbType = "INT")]
            public System.Nullable<int> Machine_Type_ID
            {
                get
                {
                    return this._Machine_Type_ID;
                }
                set
                {
                    if ((this._Machine_Type_ID != value))
                    {
                        this._Machine_Type_ID = value;
                    }
                }
            }

            [Column(Storage = "_Machine_Type_Code", DbType = "VarChar(50)")]
            public string Machine_Type_Code
            {
                get
                {
                    return this._Machine_Type_Code;
                }
                set
                {
                    if ((this._Machine_Type_Code != value))
                    {
                        this._Machine_Type_Code = value;
                    }
                }
            }
        }
    }
}





