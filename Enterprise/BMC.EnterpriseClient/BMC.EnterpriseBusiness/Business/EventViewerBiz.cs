using BMC.EnterpriseBusiness.Entities;
using BMC.EnterpriseDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.EnterpriseBusiness.Business
{
    public class EventViewerBiz
    {
        #region Local Declaration

        private static EventViewerBiz _EventViewerBiz;

        #endregion Local Declaration

        #region Instance Method
        /// <summary>
        /// Creates the instance.
        /// </summary>
        /// <returns>MaintenanceBiz Object</returns>
        public static EventViewerBiz CreateInstance()
        {
            if (_EventViewerBiz == null)
                _EventViewerBiz = new EventViewerBiz();

            return _EventViewerBiz;
        }
        #endregion Instance Method

        public List<EnterpriseEvent> GetEnterpriseEvents(DateTime startDate, DateTime endDate, int siteID)
        {
            List<EnterpriseEvent> lstRetEnterpriseEvents = null;
            try
            {
                List<rsp_GetEnterpriseEventsResult> lstEnterpriseEvents;
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    lstEnterpriseEvents = DataContext.GetEnterpriseEvents(startDate, endDate, siteID).ToList();
                }
                lstRetEnterpriseEvents = (from Records in lstEnterpriseEvents
                                          select new EnterpriseEvent
                                              {
                                                  Site_name = Records.Site_Name,
                                                  Date_and_time_of_event = Records.Evt_Datetime,
                                                  Details_of_the_event = Records.Details_of_the_event,
                                                  Description_of_event = Records.Description_of_event
                                              }).ToList<EnterpriseEvent>();

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return lstRetEnterpriseEvents;
        }

        public List<SiteEvent> GetSiteEvents(DateTime startDate, DateTime endDate, int siteID, int eventType, int showautoclosed)
        {
            List<SiteEvent> lstRetSiteEvent = null;
            try
            {
                List<Rsp_GetEventsResult> lstSiteEvent;
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    lstSiteEvent = DataContext.GetSiteEvents(startDate, endDate, siteID,0, eventType, showautoclosed).ToList();
                }
                lstRetSiteEvent = (from Records in lstSiteEvent
                                          select new SiteEvent
                                              {
                                                  Site_name = Records.Site_name,
                                                  Position = Records.Position,
                                                  Game_Title = Records.Game_Title,
                                                  Date_and_time_of_event = Records.Date_and_time_of_event,
                                                  Description_of_event = Records.Description_of_event,
                                                  Details_of_the_event = Records.Details_of_the_event,
                                                  Event_Auto_Closed = Records.Event_Auto_Closed
                                              }).ToList<SiteEvent>();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return lstRetSiteEvent;
        }

        public List<SitesResult> GetSiteDetails()
        {
            List<SitesResult> lstRetSiteDetails = null;
            try
            {
                List<rsp_GetSiteAccessSitesResult> lstSiteDetails;
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    lstSiteDetails = DataContext.GetSiteAccessSites().ToList();
                }
                lstRetSiteDetails = (from Records in lstSiteDetails
                                     orderby Records.Site_Name ascending
                                     select new SitesResult
                                      {
                                          Site_ID = Records.Site_ID,
                                          Site_Name = Records.Site_Name
                                      }).ToList<SitesResult>();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return lstRetSiteDetails;
        }
    }
}
