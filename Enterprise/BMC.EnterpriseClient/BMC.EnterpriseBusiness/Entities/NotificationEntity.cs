using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.EnterpriseBusiness.Entities
{
    public partial class GetNotificationsEntity
    {

        private System.Nullable<int> _NotificationID;

        private string _NotificationItem;

        private string _Notifications;

        private System.Nullable<System.DateTime> _NotifiedDate;

        public GetNotificationsEntity()
        {
        }

        public System.Nullable<int> NotificationID
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
