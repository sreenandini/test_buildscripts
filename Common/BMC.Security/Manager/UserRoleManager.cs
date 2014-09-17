using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.IO;
using System.Reflection;
using BMC.Security.Interfaces;
using BMC.Security.Entity;

namespace BMC.Security.Manager
{
    public class UserRoleManager : System.Data.Linq.DataContext
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Data.Linq.DataContext"/> class by referencing a file source.
        /// </summary>
        /// <param name="connectionString">This argument can be any one of the following:
        ///                     The name of a file where a SQL Server Express database resides.
        ///                     The name of a server where a database is present. In this case the provider uses the default database for a user.
        ///                     A complete connection string. LINQ to SQL just passes the string to the provider without modification.
        ///                 </param>
        public UserRoleManager(string connectionString)
            : base(connectionString)
        {
        }

        public IEnumerable<IRole> GetRolesForUser(IUser user)
        {
            foreach (var item in GetUserRoleMapping(user.SecurityUserID))
                yield return item;
            
        }
        
        public void AssignUserToRole(IUser user, IRole role)
        {
            foreach (var result in MapUserToRole(role.SecurityRoleID, user.SecurityUserID))
                if (result.Result != "SUCCESS")
                    throw new InvalidDataException("Either User or Role does not exist or a combination of this already exists in Database");
        }

        public void RemoveUserFromRole(IUser user, IRole role)
        {
            RemoveUserRoleMapping(user.SecurityUserID, role.SecurityRoleID);
        }

        #region "Private Method"
        [ResultType(typeof(Role))]
        [Function(Name = "dbo.ASPNET_SP_GetUserRoleMapping")]
        private ISingleResult<Role> GetUserRoleMapping([Parameter(Name = "USERID", DbType = "Int")] System.Nullable<int> uSERID)
        {
            IExecuteResult result = ExecuteMethodCall(this, ((MethodInfo)(MethodBase.GetCurrentMethod())), uSERID);
            return ((ISingleResult<Role>)(result.ReturnValue));
        }
        [ResultType(typeof(SProcResult))]
        [Function(Name = "dbo.ASPNET_SP_MapUserToRole")]
        private ISingleResult<SProcResult> MapUserToRole([Parameter(Name = "RoleID", DbType = "Int")] System.Nullable<int> roleID, [Parameter(Name = "UserID", DbType = "Int")] System.Nullable<int> userID)
        {
            IExecuteResult result = ExecuteMethodCall(this, ((MethodInfo)(MethodBase.GetCurrentMethod())), roleID, userID);
            return ((ISingleResult<SProcResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.ASPNET_SP_RemoveUserRoleMapping")]
        private int RemoveUserRoleMapping([Parameter(Name = "UserID", DbType = "Int")] System.Nullable<int> userID, [Parameter(Name = "RoleID", DbType = "Int")] System.Nullable<int> roleID)
        {
            IExecuteResult result = ExecuteMethodCall(this, ((MethodInfo)(MethodBase.GetCurrentMethod())), userID, roleID);
            return ((int)(result.ReturnValue));
        }
        #endregion


    }
}