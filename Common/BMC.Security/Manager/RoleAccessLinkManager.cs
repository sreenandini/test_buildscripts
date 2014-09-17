using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Reflection;
using BMC.Security.Entity;
using BMC.Security.Interfaces;

namespace BMC.Security.Manager
{
    public class RoleAccessLinkManager : System.Data.Linq.DataContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Data.Linq.DataContext"/> class by referencing a file source.
        /// </summary>
        /// <param name="connectionString">This argument can be any one of the following:
        ///                     The name of a file where a SQL Server Express database resides.
        ///                     The name of a server where a database is present. In this case the provider uses the default database for a user.
        ///                     A complete connection string. LINQ to SQL just passes the string to the provider without modification.
        ///                 </param>
        public RoleAccessLinkManager(string connectionString)
            : base(connectionString)
        {
        }

        [Function(Name = "dbo.ASPNET_SP_MAPRoleToRoleAccess")]
        private ISingleResult<SProcResult> MapRoleToRoleAccess([Parameter(Name = "RoleID", DbType = "Int")] System.Nullable<int> roleID, [Parameter(Name = "RoleAccessID", DbType = "Int")] System.Nullable<int> roleAccessID)
        {
            IExecuteResult result = ExecuteMethodCall(this, ((MethodInfo)(MethodBase.GetCurrentMethod())), roleID, roleAccessID);
            return ((ISingleResult<SProcResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.ASPNET_SP_RemoveRoleToRoleAccessMapping")]
        private ISingleResult<SProcResult> RemoveRoleToRoleAccessMapping([Parameter(Name = "RoleID", DbType = "Int")] System.Nullable<int> roleID, [Parameter(Name = "RoleAccessID", DbType = "Int")] System.Nullable<int> roleAccessID)
        {
            IExecuteResult result = ExecuteMethodCall(this, ((MethodInfo)(MethodBase.GetCurrentMethod())), roleID, roleAccessID);
            return ((ISingleResult<SProcResult>)(result.ReturnValue));
        }

        [ResultType(typeof(RoleAccess))]
        [Function(Name = "dbo.ASPNET_SP_GetRoleAccessForRole")]
        private ISingleResult<RoleAccess> GetRoleAccessForRole([Parameter(Name = "RoleID", DbType = "Int")] System.Nullable<int> roleID)
        {
            var result = ExecuteMethodCall(this, ((MethodInfo)(MethodBase.GetCurrentMethod())), roleID);
            return ((ISingleResult<RoleAccess>)(result.ReturnValue));
        }

        public IEnumerable<IRoleAccess> GetAccessForRole(IRole role)
        {
            foreach (var roleAccess in GetRoleAccessForRole(role.SecurityRoleID))
                yield return roleAccess;            
        }
        
        public void AssignAccessToRole(int roleID, int roleaccessID)
        {
            MapRoleToRoleAccess(roleID, roleaccessID);
        }

        public void RemoveAccessToRole(int roleID, int roleaccessID)
        {
            RemoveRoleToRoleAccessMapping(roleID, roleaccessID);
        }
    }
}
