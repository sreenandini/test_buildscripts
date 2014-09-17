using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Reflection;
using BMC.Security.Entity;
using BMC.Security.Interfaces;

namespace BMC.Security.Manager
{
    public class RoleAccessManager : System.Data.Linq.DataContext
    {
        public RoleAccessManager(string connectionString)
            : base(connectionString)
        {
        }
        [ResultType(typeof(SProcResult))]
        [Function(Name = "dbo.ASPNET_SP_CreateRoleAccess")]
        private ISingleResult<SProcResult> CreateRoleAccess([Parameter(Name = "ParentID", DbType = "Int")] System.Nullable<int> parentID, [Parameter(Name = "RoleAccessName", DbType = "VarChar(200)")] string roleAccessName, [Parameter(Name = "Description", DbType = "VarChar(200)")] string description)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodBase.GetCurrentMethod())), parentID, roleAccessName, description);
            return ((ISingleResult<SProcResult>)(result.ReturnValue));
        }
        [ResultType(typeof(RoleAccess))]
        [Function(Name = "dbo.ASPNET_SP_GetRoleAccess")]
        private ISingleResult<RoleAccess> GetRoleAccess([Parameter(Name = "RoleAccessID", DbType = "Int")] System.Nullable<int> roleAccessID, [Parameter(Name = "RoleAccessName", DbType = "VarChar(200)")] string roleAccessName)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodBase.GetCurrentMethod())), roleAccessID, roleAccessName);
            return ((ISingleResult<RoleAccess>)(result.ReturnValue));
        }
        [ResultType(typeof(SProcResult))]
        [Function(Name = "dbo.ASPNET_SP_UpdateRoleAccess")]
        private ISingleResult<SProcResult> UpdateRoleAccess([Parameter(Name = "RoleAccessID", DbType = "Int")] System.Nullable<int> roleAccessID, [Parameter(Name = "ParentID", DbType = "Int")] System.Nullable<int> parentID, [Parameter(Name = "RoleAccessName", DbType = "VarChar(200)")] string roleAccessName, [Parameter(Name = "Description", DbType = "VarChar(200)")] string description)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodBase.GetCurrentMethod())), roleAccessID, parentID, roleAccessName, description);
            return ((ISingleResult<SProcResult>)(result.ReturnValue));
        }
        [ResultType(typeof(SProcResult))]
        [Function(Name = "dbo.ASPNET_SP_RemoveRoleAccess")]
        private ISingleResult<SProcResult> RemoveRoleAccess([Parameter(Name = "RoleAccessID", DbType = "Int")] System.Nullable<int> roleAccessID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodBase.GetCurrentMethod())), roleAccessID);
            return ((ISingleResult<SProcResult>)(result.ReturnValue));
        }

        public bool Add(int? parentID, string roleAccessName, string description)
        {
            foreach (var role in CreateRoleAccess(parentID, roleAccessName, description))
                return (role.Result == "SUCCESS");

            return false;
        }


        public bool Delete(int roleAccessID)
        {
            foreach (var role in RemoveRoleAccess(roleAccessID))
                return (role.Result == "SUCCESS");

            return false;
        }

        public IRoleAccess GetRoleAccessByID(int roleAccessId)
        {
            foreach (var roleAccess  in GetRoleAccess(roleAccessId, ""))
                return roleAccess;

            return null;
        }

        public IRoleAccess GetRoleAccessByName(string roleAccessName)
        {
            foreach (var roleAccess in GetRoleAccess(0, roleAccessName))
                return roleAccess;

            return null;
        }

        public IEnumerable<IRoleAccess> GetAllRoleAccess()
        {
            foreach (var item in GetRoleAccess(0, ""))
                yield return item;            
        }

        public bool Modify(int roleAccessID,  string roleAccessname, string roleAccessDescription)
        {
            var roleaccess = GetRoleAccessByID(roleAccessID);

            foreach (var result in UpdateRoleAccess(roleAccessID, roleaccess.ParentID, roleAccessname, roleAccessDescription))
                return (result.Result == "SUCCESS");

            return false;
        }
        public bool ModifyParent(int roleAccessID, int? parentID)
        {
            var roleaccess = GetRoleAccessByID(roleAccessID);

            foreach (var result in UpdateRoleAccess(roleAccessID, parentID, roleaccess.RoleAccessName, roleaccess.Description))
                return (result.Result == "SUCCESS");

            return false;
        }

    }
}
