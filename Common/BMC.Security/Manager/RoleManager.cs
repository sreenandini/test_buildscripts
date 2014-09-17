using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Reflection;
using BMC.Security.Entity;
using BMC.Security.Interfaces;

namespace BMC.Security.Manager
{
    public class RoleManager : System.Data.Linq.DataContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Data.Linq.DataContext"/> class by referencing a file source.
        /// </summary>
        /// <param name="connectionString">This argument can be any one of the following:
        ///                     The name of a file where a SQL Server Express database resides.
        ///                     The name of a server where a database is present. In this case the provider uses the default database for a user.
        ///                     A complete connection string. LINQ to SQL just passes the string to the provider without modification.
        ///                 </param>
        public RoleManager(string connectionString)
            : base(connectionString)
        {
        }

        public bool AddRole(string roleName, string roleDescription)
        {
            foreach (var role in CreateRole(roleName, roleDescription))
                return (role.Result == "SUCCESS");

            return false;
        }

        public bool DeleteRole(int securityRoleID)
        {
            foreach (var role in RemoveRole(securityRoleID))
                return (role.Result == "SUCCESS");

            return false;
        }

        public IRole GetRoleByID(int securityRoleID)
        {
            foreach (var role in GetRole(securityRoleID, ""))
                return role;
            
            return null;
        }

        public IRole GetRoleByName(string roleName)
        {
            foreach (var role in GetRole(0, roleName))
                return role;

            return null;
        }

        public IEnumerable<IRole> GetAllRoles()
        {
            foreach (var role in GetRole(0,""))
                yield return role;
            
        }

        public bool ModifyDescription(int securityRoleID, string roleName, string roleDescription)
        {
            foreach (var result in UpdateRole(securityRoleID, roleName, roleDescription))
                return (result.Result == "SUCCESS");

            return false;
        }

        #region Private Method
        [ResultType(typeof(SProcResult))]
        [Function(Name = "dbo.ASPNET_SP_CreateRole")]
        private ISingleResult<SProcResult> CreateRole([Parameter(Name = "RoleName", DbType = "VarChar(100)")] string roleName, [Parameter(Name = "RoleDescription", DbType = "VarChar(300)")] string roleDescription)
        {
            IExecuteResult result = ExecuteMethodCall(this, ((MethodInfo)(MethodBase.GetCurrentMethod())), roleName, roleDescription);
            return ((ISingleResult<SProcResult>)(result.ReturnValue));
        }
        [ResultType(typeof(SProcResult))]
        [Function(Name = "dbo.ASPNET_SP_RemoveRole")]
        private ISingleResult<SProcResult> RemoveRole([Parameter(Name = "RoleID", DbType = "Int")] System.Nullable<int> roleID)
        {
            IExecuteResult result = ExecuteMethodCall(this, ((MethodInfo)(MethodBase.GetCurrentMethod())), roleID);
            return ((ISingleResult<SProcResult>)(result.ReturnValue));
        }

        [ResultType(typeof(Role))]
        [Function(Name = "dbo.ASPNET_SP_GetRole")]
        private ISingleResult<Role> GetRole([Parameter(Name = "ID", DbType = "Int")] System.Nullable<int> iD, [Parameter(Name = "Name", DbType = "VarChar(200)")] string name)
        {
            IExecuteResult result = ExecuteMethodCall(this, ((MethodInfo)(MethodBase.GetCurrentMethod())), iD, name);
            return ((ISingleResult<Role>)(result.ReturnValue));
        }
        [ResultType(typeof(SProcResult))]
        [Function(Name = "dbo.ASPNET_SP_UpdateRole")]
        private ISingleResult<SProcResult> UpdateRole([Parameter(Name = "RoleID", DbType = "Int")] System.Nullable<int> roleID, [Parameter(Name = "RoleName", DbType = "VarChar(100)")] string roleName, [Parameter(Name = "RoleDescription", DbType = "VarChar(300)")] string roleDescription)
        {
            IExecuteResult result = ExecuteMethodCall(this, ((MethodInfo)(MethodBase.GetCurrentMethod())), roleID, roleName, roleDescription);
            return ((ISingleResult<SProcResult>)(result.ReturnValue));
        }
        #endregion
    }

    
}