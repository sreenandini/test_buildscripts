﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.Transport
{
    public partial class rsp_N_GetNotificationsResult
    {

        private int _NotificationID;

        private string _NotificationItem;

        private string _Notifications;

        private System.Nullable<System.DateTime> _NotifiedDate;

        public rsp_N_GetNotificationsResult()
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Notifications", DbType = "VarChar(5000)")]
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
