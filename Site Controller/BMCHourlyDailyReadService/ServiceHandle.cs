
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.Common;
using BMC.Common.ExceptionManagement;
using BMC.Common.LogManagement;
using BMC.DataAccess;
using System.Data;
using System.Diagnostics;
using System.Threading;
using System.Xml.Linq;
using BMC.Common.Utilities;


namespace BMC.HourlyDailyReadJobs
{

    class HourlyServiceHandle : IJob
    {
        private string _ConnectionString = string.Empty;

        public HourlyServiceHandle()
        {
            _ConnectionString = DBConnect.ExchangeConnectionString;
        }
        #region IJob Members

        /// <summary>
        /// Intialize the the job.
        /// </summary>
        public void Init()
        {
            HourlyDailyEventLog.WriteToEventLog(HourlyDailyEntity.EventLogName, "Hourly Service Init: ", "Hourly Service started.", EventLogEntryType.Information);
        }

        /// <summary>
        /// This method must have the implementation of the work that is going to perform by this job.
        /// </summary>
        public void DoWork()
        {
            //Dictionary<string, string> dictServiceentries = null;
            //RegistrySetting oRegistrySetting = null;
            try
            {
                DBConnect oDbConnect = new DBConnect();
                if ((oDbConnect.RunHourlyVTPService()) && (HourlyDailyEntity.IsReadInHourly) && (HourlyDailyEntity.HasReadRunWithHourly))
                {
                    LogManager.WriteLog("Read has been run with Hourly, so update the registry entries for last auto read.", LogManager.enumLogLevel.Info);
              //      dictServiceentries = new Dictionary<string, string>();
                   // oRegistrySetting = new RegistrySetting();
                //    dictServiceentries.Add(HourlyDailyEntity.sRegistryPath + "\\\\LastAutoRead", DateTime.Now.ToString("dd MMM yyyy"));
                  //  oRegistrySetting.SetRegistryEntries(dictServiceentries, HourlyDailyEntity.sRegistryPath);
                    BMCRegistryHelper.SetRegKeyValue(HourlyDailyEntity.sRegistryPath, "LastAutoRead", Microsoft.Win32.RegistryValueKind.String, DateTime.Now.ToString("dd MMM yyyy"));
                    HourlyDailyEntity.IsReadInHourly = false;
                }
                oDbConnect.UpdateHourlyStatsGamingday();
                //HourlyDailyEventLog.WriteToEventLog(HourlyDailyEntity.EventLogName, "Hourly Service DoWork: ", "Hourly has been executed successfully.", EventLogEntryType.SuccessAudit);
            }
            catch (Exception ex)
            {
                // HourlyDailyEventLog.WriteToEventLog(HourlyDailyEntity.EventLogName, "Hourly Service DoWork: ", "Message: " + ex.Message + "Source: " + ex.Source, EventLogEntryType.Error);
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// Unintialize the the job.
        /// </summary>
        public void UnInit()
        {
            HourlyDailyEventLog.WriteToEventLog(HourlyDailyEntity.EventLogName, "Hourly Service UnInit: ", "Hourly Service stopped.", EventLogEntryType.Information);
        }

        public bool CheckSiteStatus()
        {
            LogManager.WriteLog("Inside CheckSiteStatus method", LogManager.enumLogLevel.Info);

            DBConnect oDbConnect = new DBConnect();
            return oDbConnect.GetSiteStatus();
        }

        public string GetSettingDetail(string strSetting)
        {
            LogManager.WriteLog("Inside GetSettingDetail method", LogManager.enumLogLevel.Info);

            DBConnect oDbConnect = new DBConnect();
            return oDbConnect.GetSettingFromDB(strSetting, string.Empty);
        }

        #endregion
    }
    class DailyReadServiceHandle : IJob
    {
        private string _ConnectionString = string.Empty;

        public DailyReadServiceHandle()
        {
            _ConnectionString = DBConnect.ExchangeConnectionString;
        }
        #region IJob Members

        /// <summary>
        /// Intialize the the job.
        /// </summary>
        public void Init()
        {
            HourlyDailyEventLog.WriteToEventLog(HourlyDailyEntity.EventLogName, "DailyService Init: ", "Read Service started.", EventLogEntryType.Information);
        }

        /// <summary>
        /// This method must have the implementation of the work that is going to perform by this job.
        /// </summary>
        public void DoWork()
        {
            //Dictionary<string, string> dictServiceentries = null;
            RegistrySetting oRegistrySetting = null;

            try
            {
                DBConnect oDbConnect = new DBConnect();
                // Fix for Read run twice for a day - Begin
                if (HourlyDailyEntity.HasReadRunWithHourly)
                {
                    LogManager.WriteLog("Read has been run with Hourly, so skip the daily Read process", LogManager.enumLogLevel.Info);
                    return;
                }
                // Fix for Read run twice for a day - End

                if (oDbConnect.RunDailyReadService())
                {
                    //dictServiceentries = new Dictionary<string, string>();
                    oRegistrySetting = new RegistrySetting();
                    //dictServiceentries.Add(HourlyDailyEntity.sRegistryPath + "\\\\LastAutoRead", DateTime.Now.ToString("dd MMM yyyy"));
                    //oRegistrySetting.SetRegistryEntries(dictServiceentries, HourlyDailyEntity.sRegistryPath);
                    BMCRegistryHelper.SetRegKeyValue(HourlyDailyEntity.sRegistryPath, "LastAutoRead", Microsoft.Win32.RegistryValueKind.String, DateTime.Now.ToString("dd MMM yyyy"));
                }
                //HourlyDailyEventLog.WriteToEventLog(HourlyDailyEntity.EventLogName, "DailyService DoWork: ", "Daily Read has been executed successfully.", EventLogEntryType.SuccessAudit);
            }
            catch (Exception ex)
            {
                // HourlyDailyEventLog.WriteToEventLog(HourlyDailyEntity.EventLogName, "DailyService DoWork: ", "Message: " + ex.Message + "Source: " + ex.Source, EventLogEntryType.Error);
                ExceptionManager.Publish(ex);
            }

        }

        /// <summary>
        /// Unintialize the the job.
        /// </summary>
        public void UnInit()
        {
            HourlyDailyEventLog.WriteToEventLog(HourlyDailyEntity.EventLogName, "DailyService UnInit: ", "Read Service stopped.", EventLogEntryType.Information);
        }

        public bool CheckSiteStatus()
        {
            throw new NotImplementedException();
        }

        public string GetSettingDetail(string strSetting)
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    class StackerServiceHandle : IJob
    {

        System.Threading.AutoResetEvent at_stinterval = new System.Threading.AutoResetEvent(false);
        private int Stackerinterval { get; set; }

        private string StackerExceptionCode { get; set; }
        #region IJob Members

        public void Init()
        {
            try
            {
                LogManager.WriteLog("StackerServiceHandle Initalizing..... ", LogManager.enumLogLevel.Info);
                Stackerinterval = Convert.ToInt32(BMC.Common.ConfigurationManagement.ConfigManager.Read("StackerInterval"));
                StackerExceptionCode = BMC.Common.ConfigurationManagement.ConfigManager.Read("StackerExceptionCode");


            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        public void DoWork()
        {
            try
            {
                Thread Th_Stacker = new Thread(() =>
                    {
                        Check_StackerAlert();
                    });
                Th_Stacker.Start();
                LogManager.WriteLog("StackerServiceHandle Thread Started ", LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        void Check_StackerAlert()
        {
            try
            {

                while (!at_stinterval.WaitOne(Stackerinterval))
                {
                    if (DBConnect.CheckSqlConnectionExists())
                    {
                        FrameStackerXml();
                    }

                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void FrameStackerXml()
        {
            try
            {
                DBConnect db_st = new DBConnect();
                string strAlert = string.Empty;
                string strAlertTracking = string.Empty;
                string Version = string.Empty;
                //StackerLevelTracking - If it is true, then start the Stacker tracking
                strAlertTracking = GetSettingDetail("StackerLevelTracking");
                DataTable dtVer = DBConnect.GetVersion();
                if (dtVer != null && dtVer.Rows.Count > 0)
                {
                    Version = dtVer.Rows[0]["VersionName"].ToString();
                }
                if (strAlertTracking != null && strAlertTracking.Trim().ToUpper().Equals("TRUE"))
                {
                    DataTable dt_stack = db_st.CheckStackerLevelStatus();


                            // Stacker Level Alert - If it is true, then STM alert will be sent
                            strAlert = GetSettingDetail("StackerLevelAlert");
                            if (strAlert != null && strAlert.Trim().ToUpper().Equals("TRUE"))
                            {

                                if (dt_stack != null && dt_stack.Rows.Count > 0)
                                {
                                    foreach (DataRow dr in dt_stack.Rows)
                                    {
                                        XElement xml_stack =
                                         new XElement("BMCRequest",
                                         new XElement("Source", "STACK"),
                                         new XElement("BMCVersion", Version),
                                         new XElement("ExceptionCode", StackerExceptionCode),
                                         new XElement("OperatorId", "000"),
                                         new XElement("SubCode", ""),
                                         new XElement("Company", dr["CompanyName"] ?? string.Empty),
                                         new XElement("Region", dr["RegionName"] ?? string.Empty),
                                         new XElement("Area", dr["AreaName"] ?? string.Empty),
                                         new XElement("SiteId", dr["SiteId"].ToString()),
                                         new XElement("SiteName", dr["SiteName"] ?? string.Empty),
                                         new XElement("Asset", dr["Asset"] ?? string.Empty),
                                         new XElement("Stand", dr["Stand"] ?? string.Empty),
                                         new XElement("StackerLevel", dr["StackerLevel"].ToString()),
                                         new XElement("StackerExceedLimit", dr["StackerExceedLimit"].ToString()),
                                         new XElement("MessageDateTime", string.Format("{0:G}", Convert.ToDateTime(dr["MessageDateTime"]))));
                                        if (db_st.ExportToSTM("STACK", dr["SiteId"].ToString(), xml_stack.ToString()))
                                        {
                                            LogManager.WriteLog("ExportToSTM Succeed Type: STACK", LogManager.enumLogLevel.Info);
                                            if (db_st.UpdateStackerAlertStatus(Convert.ToInt32(dr["Installation_No"]), true))
                                            {
                                                LogManager.WriteLog("Stacker Alert Status Updated Successfully", LogManager.enumLogLevel.Info);
                                            }   
                                        }

                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                       ExceptionManager.Publish(ex);
                LogManager.WriteLog("FrameStackerXml Exception Message : " + ex.Message, LogManager.enumLogLevel.Error);
            }
        }
        public void UnInit()
        {
            try
            {
                at_stinterval.Set();
                LogManager.WriteLog("StackerServiceHandle Thread Stoped ", LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        public bool CheckSiteStatus()
        {
            throw new NotImplementedException();
        }

        public string GetSettingDetail(string strSetting)
        {
            LogManager.WriteLog("Inside GetSettingDetail method", LogManager.enumLogLevel.Info);

            DBConnect oDbConnect = new DBConnect();
            return oDbConnect.GetSettingFromDB(strSetting, string.Empty);
        }

        #endregion
    }

   

}
