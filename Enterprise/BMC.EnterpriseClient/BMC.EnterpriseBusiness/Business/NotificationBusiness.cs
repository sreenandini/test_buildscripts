using BMC.Common.ExceptionManagement;
using BMC.EnterpriseBusiness.Entities;
using BMC.EnterpriseDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.EnterpriseBusiness.Business
{
    public class NotificationBusiness
    {
        private static NotificationBusiness _NotificationBiz;
        public static NotificationBusiness CreateInstance()
        {
            if (_NotificationBiz == null)
                _NotificationBiz = new NotificationBusiness();

            return _NotificationBiz;
        }

        public List<GetNotificationsEntity> GetNotifications()
        {
            List<GetNotificationsEntity> obcoll = null;
            try
            {
                List<rsp_GetNotificationsResult> NotificationList;
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    NotificationList = DataContext.GetNotifications().ToList();
                }

                obcoll = (from obj in NotificationList
                          select new GetNotificationsEntity
                          {
                              NotificationID= obj.NotificationID,
                              NotificationItem = obj.NotificationItem,
                              Notifications = obj.Notifications,
                              NotifiedDate = obj.NotifiedDate
                          }).ToList<GetNotificationsEntity>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obcoll;
        }

        public int UpdateNotifications(string NotificationIDs,int UserID)
        {
            try
            {
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    return DataContext.UpdateNotifications(NotificationIDs, UserID);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return 0;
            }
        }

        public int GetNotificationsCount()
        {
            try
            {
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    return DataContext.GetNotificationsCount();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return 0;
            }
        }
    }
}
