using BMC.DBInterface.CashDeskOperator;
using BMC.Transport;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace BMC.Business.CashDeskOperator
{
    public class NotificationBiz
    {
        NotificationDataAccess objNotification = new NotificationDataAccess(new SqlConnection(CommonUtilities.ExchangeConnectionString));

        public int GetNotificationsCount()
        {
            return objNotification.GetNotificationsCount();
        }

        public List<rsp_N_GetNotificationsResult> GetNotifications()
        {
            List<rsp_N_GetNotificationsResult> lstNotifications = null;
            lstNotifications = objNotification.GetNotifications().ToList<rsp_N_GetNotificationsResult>();
            return lstNotifications;
        }

        public int UpdateNotifications(string NotificationIDs, int UserID)
        {
            return objNotification.UpdateNotifications(NotificationIDs, UserID);
        }
    }
}
