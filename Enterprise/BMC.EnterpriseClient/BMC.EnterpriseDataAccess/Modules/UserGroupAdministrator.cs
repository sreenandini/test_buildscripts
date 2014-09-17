using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Reflection;

namespace BMC.EnterpriseDataAccess
{
    public partial class EnterpriseDataContext
    {
        [Function(Name = "dbo.rsp_getUserGroup")]
        public ISingleResult<rsp_getUserGroupResult> GetUserGroup()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<rsp_getUserGroupResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_getUser_Access")]
        public ISingleResult<rsp_getUser_AccessResult> GetUser_Access([Parameter(Name = "User_Group", DbType = "Int")] System.Nullable<int> user_Group)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), user_Group);
            return ((ISingleResult<rsp_getUser_AccessResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.usp_newUserGroup")]
        public int NewUserGroup([Parameter(Name = "NewGroup", DbType = "NVarChar(100)")] string newGroup, [Parameter(Name = "Result", DbType = "Int")] ref System.Nullable<int> result)
        {
            IExecuteResult result1 = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), newGroup, result);
            result = ((System.Nullable<int>)(result1.GetParameterValue(1)));
            return ((int)(result1.ReturnValue));
        }

        [Function(Name = "dbo.usp_UpdateUser_Access")]
        public int UpdateUser_Access([Parameter(Name = "UserGroupId", DbType = "Int")] System.Nullable<int> userGroupId, [Parameter(Name = "XMLDoc", DbType = "VarChar(MAX)")] string xMLDoc, [Parameter(Name = "IsSuccess", DbType = "Int")] ref System.Nullable<int> isSuccess)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), userGroupId, xMLDoc, isSuccess);
            isSuccess = ((System.Nullable<int>)(result.GetParameterValue(2)));
            return ((int)(result.ReturnValue));
        }
        [Function(Name = "dbo.usp_UpdateSuperUser")]
        public int usp_Updatesuperuser([Parameter(Name = "UserID", DbType = "Int")] System.Nullable<int> userID, [Parameter(Name = "IsSuperUser", DbType = "Bit")] ref System.Nullable<bool> isSuperUser)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), userID, isSuperUser);
            isSuperUser = ((System.Nullable<bool>)(result.GetParameterValue(1)));
            return ((int)(result.ReturnValue));
        }
    }
    public partial class rsp_getUserGroupResult
    {

        private int _User_Group_ID;

        private string _User_Group_Name;

        public rsp_getUserGroupResult()
        {
        }

        [Column(Storage = "_User_Group_ID", DbType = "Int NOT NULL")]
        public int User_Group_ID
        {
            get
            {
                return this._User_Group_ID;
            }
            set
            {
                if ((this._User_Group_ID != value))
                {
                    this._User_Group_ID = value;
                }
            }
        }

        [Column(Storage = "_User_Group_Name", DbType = "VarChar(50)")]
        public string User_Group_Name
        {
            get
            {
                return this._User_Group_Name;
            }
            set
            {
                if ((this._User_Group_Name != value))
                {
                    this._User_Group_Name = value;
                }
            }
        }
    }

    public partial class rsp_getUser_AccessResult
    {

        private int _User_Group_ID;

        private int _Access_Id;

        private string _Access_Key;

        private string _Access_Name;

        private int _Access_Parent_Id;

        private bool _Access_Value;

        public rsp_getUser_AccessResult()
        {
        }

        [Column(Storage = "_User_Group_ID", DbType = "Int NOT NULL")]
        public int User_Group_ID
        {
            get
            {
                return this._User_Group_ID;
            }
            set
            {
                if ((this._User_Group_ID != value))
                {
                    this._User_Group_ID = value;
                }
            }
        }

        [Column(Storage = "_Access_Id", DbType = "Int NOT NULL")]
        public int Access_Id
        {
            get
            {
                return this._Access_Id;
            }
            set
            {
                if ((this._Access_Id != value))
                {
                    this._Access_Id = value;
                }
            }
        }

        [Column(Storage = "_Access_Key", DbType = "VarChar(100)")]
        public string Access_Key
        {
            get
            {
                return this._Access_Key;
            }
            set
            {
                if ((this._Access_Key != value))
                {
                    this._Access_Key = value;
                }
            }
        }

        [Column(Storage = "_Access_Name", DbType = "VarChar(100) NOT NULL", CanBeNull = false)]
        public string Access_Name
        {
            get
            {
                return this._Access_Name;
            }
            set
            {
                if ((this._Access_Name != value))
                {
                    this._Access_Name = value;
                }
            }
        }

        [Column(Storage = "_Access_Parent_Id", DbType = "Int NOT NULL")]
        public int Access_Parent_Id
        {
            get
            {
                return this._Access_Parent_Id;
            }
            set
            {
                if ((this._Access_Parent_Id != value))
                {
                    this._Access_Parent_Id = value;
                }
            }
        }

        [Column(Storage = "_Access_Value", DbType = "Bit NOT NULL")]
        public bool Access_Value
        {
            get
            {
                return this._Access_Value;
            }
            set
            {
                if ((this._Access_Value != value))
                {
                    this._Access_Value = value;
                }
            }
        }
    }
}
