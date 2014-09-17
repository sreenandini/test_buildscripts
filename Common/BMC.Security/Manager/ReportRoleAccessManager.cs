using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Reflection;
using BMC.Security.Entity;
using BMC.Security.Interfaces;

namespace BMC.Security.Manager
{
    public class ReportRoleAccessManager : System.Data.Linq.DataContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Data.Linq.DataContext"/> class by referencing a file source.
        /// </summary>
        /// <param name="connectionString">This argument can be any one of the following:
        ///                     The name of a file where a SQL Server Express database resides.
        ///                     The name of a server where a database is present. In this case the provider uses the default database for a user.
        ///                     A complete connection string. LINQ to SQL just passes the string to the provider without modification.
        ///                 </param>
        public ReportRoleAccessManager(string connectionString)
            : base(connectionString)
        {
        }

        [Function(Name = "dbo.usp_MAPReportToRoleAccess")]
        public int MAPReportToRoleAccess([Parameter(Name = "RoleID", DbType = "Int")] System.Nullable<int> roleID, [Parameter(Name = "ReportID", DbType = "Int")] System.Nullable<int> reportID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), roleID, reportID);
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.usp_RemoveMAPReportToRoleAccess")]
        public int RemoveMAPReportToRoleAccess([Parameter(Name = "RoleID", DbType = "Int")] System.Nullable<int> roleID, [Parameter(Name = "ReportID", DbType = "Int")] System.Nullable<int> reportID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), roleID, reportID);
            return ((int)(result.ReturnValue));
        }
      
        

        public void AssignAccessToRole(int roleID, int reportID)
            
        {
            MAPReportToRoleAccess(roleID, reportID);
            
        }

        public void RemoveAccessToRole(int roleID, int reportID)
        {
            RemoveMAPReportToRoleAccess(roleID, reportID);
        }
    }
}
