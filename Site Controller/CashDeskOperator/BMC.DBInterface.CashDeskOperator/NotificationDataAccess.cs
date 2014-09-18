using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Reflection;
using System.Data.Linq.Mapping;
using BMC.DataAccess;
using System.Data;
using BMC.Security;
using System.Data.SqlClient;
using BMC.Transport;
using BMC.Common.LogManagement;

namespace BMC.DBInterface.CashDeskOperator
{
    public class NotificationDataAccess : DataContext
    {
        public NotificationDataAccess(IDbConnection dbConnection)
            : base(dbConnection)
        {
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.rsp_N_GetNotificationsCount")]
        public int GetNotificationsCount()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.rsp_N_GetNotifications")]
        public ISingleResult<rsp_N_GetNotificationsResult> GetNotifications()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<rsp_N_GetNotificationsResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.usp_N_UpdateNotifications")]
        public int UpdateNotifications([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "NotificationIds", DbType = "VarChar(MAX)")] string notificationIds, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "AcknowledgedUserID", DbType = "Int")] System.Nullable<int> acknowledgedUserID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), notificationIds, acknowledgedUserID);
            return ((int)(result.ReturnValue));
        }
    }
}
