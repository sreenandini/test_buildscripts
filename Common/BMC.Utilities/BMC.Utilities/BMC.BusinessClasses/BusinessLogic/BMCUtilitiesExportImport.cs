using BMC.Common.LogManagement;
using BMC.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Data.Linq;
using BMC.Common.ConfigurationManagement;
using BMC.Common.ExceptionManagement;
using BMC.Common.Utilities;


namespace BMC.BusinessClasses.BusinessLogic
{
    public class BmcUtilitiesExportImport : Interfaces.IBMCUtilities, IDisposable
    {
        public BmcUtilitiesExportImport() { }
        private Dictionary<string, MailMessageGroup> grpEmailSubscribers = null;
        Thread workerThread;

        MailServer serverDetails = null;


        public BmcUtilitiesExportImport(bool value)
        {

        }


        private void GetServerDetails()
        {
            if (Convert.ToBoolean(AlertEngine.GetSetting("IsEmailAlertEnabled")))
            {
                LogManager.WriteLog("Initialize", LogManager.enumLogLevel.Info);
                EmailAlertInputs input = new EmailAlertInputs();

                grpEmailSubscribers = input.GetEmailSubScribers();

                serverDetails = AlertEngine.GetMailServer();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool ProcessEmailAlertData()
        {
            List<rsp_GetSiteListResult> lstSites = null;
            string sSendMail = string.Empty;

            using (DataHelper context = new DataHelper(DatabaseHelper.GetConnectionString()))
            {
                context.GetSetting(0, "SendMailFromEnterprise", "false", ref sSendMail);

                if (Convert.ToBoolean(sSendMail))
                {
                    lstSites = context.GetSiteList().ToList();

                    LogManager.WriteLog("Count" + lstSites.Count.ToString(), LogManager.enumLogLevel.Info);
                    if (lstSites != null && lstSites.Count > 0)
                    {
                        foreach (rsp_GetSiteListResult site in lstSites)
                        {
                            LogManager.WriteLog("Spawning New Thread", LogManager.enumLogLevel.Info);
                            LogManager.WriteLog("SITE CODE :" + site.SITE_CODE, LogManager.enumLogLevel.Info);
                            workerThread = new Thread((ProcessWorkerThread)) { Name = site.SITE_CODE };
                            workerThread.Start(site.SITE_CODE);
                            // workerThread.Start("1002");
                            workerThread.Priority = ThreadPriority.Normal;
                        }
                        // ProcessWorkerThread("1002");
                    }
                }
                else
                    ProcessWorkerThread(string.Empty);
            }
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="SiteCode"></param>
        private void ProcessWorkerThread(object SiteCode)
        {
            int perItemprocessInterval = Convert.ToInt32(ConfigManager.Read("PerItemprocessInterval"));

            while (true)
            {
                try
                {
                    ProcessRecordsForSite(SiteCode.ToString(), perItemprocessInterval);
                }
                catch (Exception ex)
                {
                    ExceptionManager.Publish(ex);
                }
                finally
                { }
            }
        }

        /// <summary>
        /// process alerts for each site.
        /// </summary>
        /// <param name="siteCode"></param>
        /// <param name="recordsCount"></param>
        /// <returns></returns>
        private bool ProcessRecordsForSite(string siteCode, int perItemprocessInterval)
        {
            // declare.
            int? iRecord = 0;
            bool bResult = false;
            string strResult = string.Empty;
            AlertEntity entity = null;
            AlertEngine engine = new AlertEngine();

            List<UnprocessedRecordsForEmailAlertResult> lstUnprocessedAlerts = null;
            //process started.
           // LogManager.WriteLog("Called for site(s) : " + siteCode, LogManager.enumLogLevel.Info);
            int recordsCount = Convert.ToInt32(ConfigManager.Read("RecordCounttoprocess"));

            try
            {
                //Get all unprocessed email alerts.
                using (DataHelper context = new DataHelper(DatabaseHelper.GetConnectionString()))
                {
                    lstUnprocessedAlerts = context.GetUnprocessedRecordsForEmailAlert(siteCode, recordsCount).ToList();
                }

                //Check if there any records to process.
                if (lstUnprocessedAlerts != null && lstUnprocessedAlerts.Count > 0)
                {
                    int count = lstUnprocessedAlerts.Count;

                    LogManager.WriteLog(string.Format(":::> Processing {0:D} Items for Site : {1}  on Thread : {2}.",
                        count, siteCode, Thread.CurrentThread.ManagedThreadId), LogManager.enumLogLevel.Info);

                    //Process each alert.
                    foreach (UnprocessedRecordsForEmailAlertResult alert in lstUnprocessedAlerts)
                    {
                        try
                        {
								GetServerDetails();
                                entity = new AlertEntity();

                                if (grpEmailSubscribers == null) return false;

                                foreach (KeyValuePair<string, MailMessageGroup> keys in grpEmailSubscribers)
                                {
                                    if (keys.Key == alert.AlertType_Name)
                                    {
                                        entity.Name = keys.Key;
                                        entity.MessageGroup = keys.Value;
                                        entity.MessageGroup.MsgContent = "Site Code: " + alert.EMD_SiteCode + "Site Name: "+ alert.SiteName +"---->  Alert Message: " +  alert.EMD_Content ;

                                        if (serverDetails != null) entity.ServerInfo = serverDetails;
                                        bResult = engine.SendMail(entity);
                                    }
                                }
                           
                        }
                        catch (Exception ex)
                        {
                            ExceptionManager.Publish(ex); bResult = false; strResult = ex.Message;
                        }
                        finally
                        {
                            //Update the process status.
                            using (DataHelper context = new DataHelper(DatabaseHelper.GetConnectionString()))
                            {
                                if (bResult)
                                {
                                    //if successfull.
                                    context.UpdateEmailAlertHistoryStatus(
                                        Convert.ToInt32(alert.EMD_ID), strResult, 100);
                                    bResult = true;
                                }
                                else
                                {
                                    //if failure
                                    context.UpdateEmailAlertHistoryStatus(
                                        Convert.ToInt32(alert.EMD_ID), strResult, -1);
                                    bResult = false;
                                }
                            }

                        }
                    }

                }

            }
            catch (Exception ex)
            { ExceptionManager.Publish(ex); }
            return iRecord == 0 ? true : false;
        }

        public void Dispose()
        {
            this.Dispose();
        }


        public void Stop()
        {
            try
            {
                if (workerThread.IsAlive)
                {
                    workerThread.Suspend();
                    workerThread.Abort();
                }
            }
            catch (Exception) { }

        }


        public bool ResetAlertProcessHistory()
        {
            return false;
        }

        public void ProcessAutoCalendar()
        {
            int MinDays = 1;
            LogManager.WriteLog("AC ->BmcUtilitiesExportImport -> Inside ProcessAutoCalendar method", LogManager.enumLogLevel.Info);
            AutoCalendar objAutoCalendar = new AutoCalendar();
            objAutoCalendar.CheckAndUpdateAutoCalendar();
            objAutoCalendar.CheckAndUpdateAlertRecurrence();
            objAutoCalendar.CheckAndCreateCalendar();
            
            try
            {
                MinDays = Convert.ToInt32(ConfigManager.Read("ACMinDaystoActivate"));
            }
            catch (Exception)
            {
                MinDays = 1;
            }
            objAutoCalendar.CheckAndUpdateNewCalendar(MinDays);
        }
    }
}
