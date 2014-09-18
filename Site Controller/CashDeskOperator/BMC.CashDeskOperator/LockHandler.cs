using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Reflection;
using System.Text;
using BMC.Business.CashDeskOperator;
using BMC.Transport;

namespace BMC.CashDeskOperator
{
    [System.Data.Linq.Mapping.DatabaseAttribute(Name = "Exchange")]
    public class LockHandler : System.Data.Linq.DataContext
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Data.Linq.DataContext"/> class by referencing the connection used by the .NET Framework.
        /// </summary>
        /// <param name="connection">The connection used by the .NET Framework.
        ///                 </param>
        public LockHandler()
            : base(CommonUtilities.ExchangeConnectionString)
        {
        }

        public LockHandler(String ExchangeConn)
            : base(CommonUtilities.SiteConnectionString(ExchangeConn))
        {
        }

        [Function(Name = "dbo.usp_Insert_Lock_Record")]
        public int InsertLockRecord([Parameter(Name = "User_ID", DbType = "Int")] System.Nullable<int> user_ID, [Parameter(Name = "Machine", DbType = "VarChar(50)")] string machine, [Parameter(Name = "Application", DbType = "VarChar(50)")] string application, [Parameter(Name = "Type", DbType = "VarChar(50)")] string type, [Parameter(Name = "Identifier", DbType = "VarChar(100)")] string identifier)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), Security.SecurityHelper.CurrentUser.SecurityUserID, System.Environment.MachineName, application, type, identifier);
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.dsp_Delete_Lock_Record")]
        public int DeleteLockRecord([Parameter(Name = "User_ID", DbType = "Int")] System.Nullable<int> user_ID, [Parameter(Name = "Machine", DbType = "VarChar(50)")] string machine, [Parameter(Name = "Application", DbType = "VarChar(50)")] string application, [Parameter(Name = "Type", DbType = "VarChar(50)")] string type, [Parameter(Name = "Identifier", DbType = "VarChar(100)")] string identifier)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), BMC.Security.SecurityHelper.CurrentUser.SecurityUserID, System.Environment.MachineName, application, type, identifier);
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_Get_Lock_Record")]
        public ISingleResult<LockRecords> GetLoclRecord([Parameter(Name = "User_ID", DbType = "Int")] System.Nullable<int> user_ID, [Parameter(Name = "Machine", DbType = "VarChar(50)")] string machine, [Parameter(Name = "Application", DbType = "VarChar(50)")] string application, [Parameter(Name = "Type", DbType = "VarChar(50)")] string type, [Parameter(Name = "Identifier", DbType = "VarChar(100)")] string identifier)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), BMC.Security.SecurityHelper.CurrentUser.SecurityUserID, machine, application, type, identifier);
            return ((ISingleResult<LockRecords>)(result.ReturnValue));
        }
    }
}
