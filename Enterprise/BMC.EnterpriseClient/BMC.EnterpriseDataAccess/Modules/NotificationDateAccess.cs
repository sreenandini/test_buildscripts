using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;
using System.Data.Linq;
using System.Reflection;
using BMC.DataAccess;
using BMC.EnterpriseDataAccess;
using BMC.Common.Utilities;
using BMC.Common.ExceptionManagement;
using System.Data.SqlClient;
using System.Data;

namespace BMC.EnterpriseDataAccess
{
    public partial class EnterpriseDataContext
    {
        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.rsp_N_GetNotifications")]
        public ISingleResult<rsp_GetNotificationsResult> GetNotifications()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<rsp_GetNotificationsResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.usp_N_UpdateNotifications")]
        public int UpdateNotifications([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "NotificationIds", DbType = "VarChar(MAX)")] string notificationIds, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "AcknowledgedUserID", DbType = "Int")] System.Nullable<int> acknowledgedUserID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), notificationIds, acknowledgedUserID);
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.rsp_N_GetNotificationsCount")]
        public int GetNotificationsCount()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((int)(result.ReturnValue));
        }
    }

    public partial class rsp_GetNotificationsResult
    {

        private int _NotificationID;

        private string _NotificationItem;

        private string _Notifications;

        private System.Nullable<System.DateTime> _NotifiedDate;

        public rsp_GetNotificationsResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_NotificationID", DbType = "Int NOT NULL")]
        public int NotificationID
        {
            get
            {
                return this._NotificationID;
            }
            set
            {
                if ((this._NotificationID != value))
                {
                    this._NotificationID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_NotificationItem", DbType = "VarChar(200)")]
        public string NotificationItem
        {
            get
            {
                return this._NotificationItem;
            }
            set
            {
                if ((this._NotificationItem != value))
                {
                    this._NotificationItem = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Notifications", DbType = "VarChar(MAX)")]
        public string Notifications
        {
            get
            {
                return this._Notifications;
            }
            set
            {
                if ((this._Notifications != value))
                {
                    this._Notifications = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_NotifiedDate", DbType = "DateTime")]
        public System.Nullable<System.DateTime> NotifiedDate
        {
            get
            {
                return this._NotifiedDate;
            }
            set
            {
                if ((this._NotifiedDate != value))
                {
                    this._NotifiedDate = value;
                }
            }
        }
    }

}
