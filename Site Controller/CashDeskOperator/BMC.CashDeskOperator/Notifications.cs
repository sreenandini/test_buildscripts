using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BMC.Transport;
using BMC.Business.CashDeskOperator;

namespace BMC.CashDeskOperator
{
    public class Notifications
    {
        NotificationBiz objNotificationBiz = new NotificationBiz();
        private static Notifications _Notifications = null;
        public static Notifications CreateInstance()
        {
            if (_Notifications == null)
                _Notifications = new Notifications();
            return _Notifications;
        }

        public int GetNotificationsCount()
        {
            return objNotificationBiz.GetNotificationsCount();
        }

        public List<rsp_N_GetNotificationsResult> GetNotifications()
        {
            return objNotificationBiz.GetNotifications();
        }

        public int UpdateNotifications(string NotificationIDs, int UserID)
        {
            return objNotificationBiz.UpdateNotifications(NotificationIDs, UserID);
        }
    }
}
