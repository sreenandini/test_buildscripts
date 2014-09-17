using System.Linq;
namespace BMC.MonitoringService
{
    #region Namespaces

    using System;
    using System.Data.Linq;
    using BMC.Common.LogManagement;
    using BMC.Common.ExceptionManagement;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using BMC.DataAccess;

    #endregion Namespaces

    #region Public Class

    public class DataHelper
    {
        #region Public Methods

        public ISingleResult<SiteDetails> GetAllSiteDetails()
        {
            try
            {
                lock (this)
                {
                    LogManager.WriteLog("Inside GetAllSiteDetails method", LogManager.enumLogLevel.Info);
                }

                LinqDBAccessDataContext dbAccessContext = new LinqDBAccessDataContext(GetConnectionString());
                return dbAccessContext.GetAllSiteDetails();
            }
            catch (Exception ex)
            {
                lock (this)
                {
                    ExceptionManager.Publish(ex);
                }
                return null;
            }
        }

        public ISingleResult<ServiceDetails> GetAllServiceDetails()
        {
            try
            {
                lock (this)
                {
                    LogManager.WriteLog("Inside GetAllServices method", LogManager.enumLogLevel.Info);
                }

                LinqDBAccessDataContext dbAccessContext = new LinqDBAccessDataContext(GetConnectionString());
                return dbAccessContext.GetAllServices();
            }
            catch (Exception ex)
            {
                lock (this)
                {
                    ExceptionManager.Publish(ex);
                }
                return null;
            }
        }
        
        public bool InsertEnterpriseEvents(int eventSiteId, int eventFaultSource, int eventFaultType)
        {
            bool result = false;

            try
            {
                lock (this)
                {
                    LogManager.WriteLog("Inside InsertEnterpriseEvents method", LogManager.enumLogLevel.Info);
                }

                LinqDBAccessDataContext dbAccessContext = new LinqDBAccessDataContext(GetConnectionString());
                return dbAccessContext.InsertEnterpriseEvents(eventSiteId, eventFaultSource, eventFaultType) == 0 ? true : false;                                
            }
            catch (Exception ex)
            {
                lock (this)
                {
                    ExceptionManager.Publish(ex);
                }
            }
            return result;
        }

        public List<SpecificSiteDetails> GetSpecificSiteDetails()
        {
            try
            {
                lock (this)
                {
                    LogManager.WriteLog("Inside GetSpecificSiteDetails method", LogManager.enumLogLevel.Info);
                }

                LinqDBAccessDataContext dbAccessContext = new LinqDBAccessDataContext(GetConnectionString());
                var result = dbAccessContext.GetSpecificSiteDetails(0);
                return (from records in result
                        select new SpecificSiteDetails
                        {
                            Site_ID = records.Site_ID,
                            Site_Code = records.Site_Code,
                            WebURL = records.WebURL,
                            ReadTime = records.ReadTime,
                            IsCertificateRequired = records.IsCertificateRequired,
                            CertificateIssuer = records.CertificateIssuer
                        }).ToList<SpecificSiteDetails>();

            }
            catch (Exception ex)
            {
                lock (this)
                {
                    ExceptionManager.Publish(ex);
                }
                return null;
            }
        }

        public List<ReadList> GetReadList(List<ReadList> privious, string defaultReadTime)
        {
            try
            {

                lock (this)
                {
                    LogManager.WriteLog("Inside GetReadList method", LogManager.enumLogLevel.Info);
                }
                List<ReadList> current = new List<ReadList>();
                List<ReadList> returnList = new List<ReadList>();
                LinqDBAccessDataContext dbAccessContext = new LinqDBAccessDataContext(GetConnectionString());
                var result = dbAccessContext.GetSpecificSiteDetails(0);
                current = (from records in result
                        select new ReadList
                        {
                            Site_ID = records.Site_ID,
                            Site_Code = records.Site_Code,
                            ReadTime = string.IsNullOrEmpty(records.ReadTime)?defaultReadTime:records.ReadTime
                        }).ToList<ReadList>();

                if (privious == null || privious.Count == 0)
                    return current;
                foreach (ReadList list in current)
                {
                    ReadList temp = privious.Find(X => X.Site_ID == list.Site_ID);
                    privious.Remove(temp);
                    returnList.Add(new ReadList {Site_ID =  list.Site_ID, Site_Code = list.Site_Code, ReadTime= list.ReadTime, IsProcessed = (temp!= null)? temp.IsProcessed:false});
                }
                return returnList;

            }
            catch (Exception ex)
            {
                lock (this)
                {
                    ExceptionManager.Publish(ex);
                }
            }
            return new List<ReadList>();
        }


        public List<SiteStatusEntity> GetSiteStatusByID(Int32 siteID)
        {
            try
            {
                lock (this)
                {
                    LogManager.WriteLog("Inside GetSiteStatusByID method", LogManager.enumLogLevel.Info);
                }

                LinqDBAccessDataContext dbAccessContext = new LinqDBAccessDataContext(GetConnectionString());
                var result = dbAccessContext.GetSiteStatusByID(siteID);
                return (from item in result
                        select new SiteStatusEntity
                        {
                            Site_ID = item.Site_ID,
                            Site_Status = item.Site_Status
                        }).ToList<SiteStatusEntity>();


            }
            catch (Exception ex)
            {
                lock (this)
                {
                    ExceptionManager.Publish(ex);
                }
            }
            return null;
        }

        public void UpdateSiteStatus(string xml, string siteCode)
        {
            try
            {
                lock (this)
                {
                    LogManager.WriteLog("Inside UpdateSiteStatus method", LogManager.enumLogLevel.Info);
                }

                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("SiteName", siteCode);
                param[1] = new SqlParameter("xml", xml);
                SqlHelper.ExecuteNonQuery(GetConnectionString(), BMC.Common.Constants.CONSTANT_USP_UPDATESITESTATS, param);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        public Int32 UpdateSiteDownAlert(int siteID)
        {
            try
            {
                lock (this)
                {
                    LogManager.WriteLog("Inside UpdateSiteDownAlert method", LogManager.enumLogLevel.Info);
                }

                LinqDBAccessDataContext dbAccessContext = new LinqDBAccessDataContext(GetConnectionString());
                return dbAccessContext.UpdateSiteDownAlert(siteID);
            }
            catch (Exception ex)
            {
                lock (this)
                {
                    ExceptionManager.Publish(ex);
                }
                return 1;
            }
        }

        public Int32 UpdateHourlyNotRun(int siteID)
        {
            try
            {
                lock (this)
                {
                    LogManager.WriteLog("Inside UpdateHourlyNotRun method", LogManager.enumLogLevel.Info);
                }

                LinqDBAccessDataContext dbAccessContext = new LinqDBAccessDataContext(GetConnectionString());
                return dbAccessContext.UpdateHourlyNotRun(siteID);
            }
            catch (Exception ex)
            {
                lock (this)
                {
                    ExceptionManager.Publish(ex);
                }
                return 1;
            }
        }

        public Int32 UpdateReadNotRun(int siteID)
        {
            try
            {
                lock (this)
                {
                    LogManager.WriteLog("Inside UpdateReadNotRun method", LogManager.enumLogLevel.Info);
                }

                LinqDBAccessDataContext dbAccessContext = new LinqDBAccessDataContext(GetConnectionString());
                return dbAccessContext.UpdateReadNotRun(siteID);
            }
            catch (Exception ex)
            {
                lock (this)
                {
                    ExceptionManager.Publish(ex);
                }
                return 1;
            }
        }

        public Int32 ResetHourlyNotRun()
        {
            try
            {
                lock (this)
                {
                    LogManager.WriteLog("Inside ResetHourlyNotRun method", LogManager.enumLogLevel.Info);
                }

                LinqDBAccessDataContext dbAccessContext = new LinqDBAccessDataContext(GetConnectionString());
                return dbAccessContext.ResetHourlyNotRun();
            }
            catch (Exception ex)
            {
                lock (this)
                {
                    ExceptionManager.Publish(ex);
                }
                return 1;
            }
        }


        #endregion Public Methods

        #region Private Methods

        private string GetConnectionString()
        {
            lock (this)
            {
                LogManager.WriteLog("Inside GetConnectionString method", LogManager.enumLogLevel.Info);
            }

            return Common.Utilities.DatabaseHelper.GetConnectionString();
        }

        #endregion Private Methods

    }

    #endregion Public Class
}
