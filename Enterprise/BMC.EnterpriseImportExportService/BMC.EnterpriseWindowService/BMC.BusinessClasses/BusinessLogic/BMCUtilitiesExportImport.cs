using BMC.Common.ConfigurationManagement;
using BMC.Common.ExceptionManagement;
using BMC.Common.LogManagement;
using BMC.Common.Utilities;
using BMC.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace BMC.BusinessClasses.BusinessLogic
{
    public class BmcUtilitiesExportImport : Interfaces.IBMCUtilities, IDisposable
    {
        public BmcUtilitiesExportImport() { }
        static ManualResetEvent _QuitProcessing = new ManualResetEvent(false);

        public BmcUtilitiesExportImport(bool value)
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public void ImportAlertData()
        {
            Thread thrThread = new Thread(ProcessAlerts);
            thrThread.Start();
        }

        public void ProcessAlerts()
        {
            List<SiteListResult> lstSites = null;

            using (DataHelperClass context = new DataHelperClass(DatabaseHelper.GetConnectionString()))
            {
                lstSites = context.GetSiteList().ToList();

                if (lstSites != null && lstSites.Count > 0)
                {
                    Thread workerThread;
                    foreach (SiteListResult site in lstSites)
                    {
                        LogManager.WriteLog("Spawning New Thread", LogManager.enumLogLevel.Info);
                        LogManager.WriteLog("SITE CODE :" + site.SITE_CODE, LogManager.enumLogLevel.Info);
                        workerThread = new Thread((ProcessWorkerThread)) { Name = site.SITE_CODE };
                        workerThread.Start(site.SITE_CODE);
                        workerThread.Priority = ThreadPriority.Normal;
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="SiteCode"></param>
        private void ProcessWorkerThread(object SiteCode)
        {
            int perItemprocessInterval;
            try
            {
                //Wait befor processing next data(To avoid CPU load) 
                perItemprocessInterval = Convert.ToInt32(ConfigManager.Read("PerItemProcessMilliseconds"));
            }
            catch
            {
                perItemprocessInterval = 100;
            }
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
        private bool ProcessRecordsForSite(string siteCode, int? recordsCount)
        {
            // declare.
            int? iRecord = 0;
            bool bResult = false;
            string strResult = string.Empty;

            List<UnprocessedRecordsForAlertResult> lstUnprocessedAlerts = null;
            //process started.
          //  LogManager.WriteLog("Called for site(s) : " + siteCode, LogManager.enumLogLevel.Info);

            try
            {
                //Get all unprocessed alerts.
                using (DataHelperClass context = new DataHelperClass(DatabaseHelper.GetConnectionString()))
                {
                    lstUnprocessedAlerts = context.GetUnprocessedRecordsForAlert(siteCode, recordsCount).ToList();
                }

                //Check if there any records to process.
                if (lstUnprocessedAlerts != null && lstUnprocessedAlerts.Count > 0)
                {
                    int count = lstUnprocessedAlerts.Count;

                   // LogManager.WriteLog(string.Format(":::> Processing {0:D} Items for Site : {1}  on Thread : {2}.",
                        count, siteCode, Thread.CurrentThread.ManagedThreadId), LogManager.enumLogLevel.Info);

                    //Process each alert.
                    foreach (UnprocessedRecordsForAlertResult alert in lstUnprocessedAlerts)
                    {
                        try
                        {
                            if (_QuitProcessing.WaitOne((int)recordsCount))
                            {
                                break;
                            }

                            //Update the alert details.
                           using (DataHelperClass context = new DataHelperClass(DatabaseHelper.GetConnectionString()))
                            {
                                context.UpdateAlertAuditDetails(alert.APH_Site_Code, alert.APH_Message, alert.APH_ID, ref iRecord);
                                strResult = string.Empty;
                            }
                            bResult = true;
                        }
                        catch (Exception ex)
                        {
                            ExceptionManager.Publish(ex); bResult = false; strResult = ex.Message;
                        }
                        finally
                        {
                            //Update the process status.
                            using (DataHelperClass context = new DataHelperClass(DatabaseHelper.GetConnectionString()))
                            {
                                if (bResult)
                                {
                                    // if the result is successfull,
                                    context.UpdateAlertHistoryStatus(
                                        Convert.ToInt32(alert.APH_ID), strResult, 100);
                                    bResult = true;
                                }
                                else
                                {
                                    //else update -1
                                    context.UpdateAlertHistoryStatus(
                                        Convert.ToInt32(alert.APH_ID), strResult, -1);
                                    bResult = false;
                                }
                            }

                        }
                    }

                }

            }
            catch (Exception ex)
            { ExceptionManager.Publish(ex); }

            return bResult;
        }

        public void Dispose()
        {
            _QuitProcessing.Set();
        }


        public bool ResetAlertProcessHistory()
        {
            return false;
        }
    }
}