using System;
using System.Threading;
using System.Collections.Generic;
using BMC.EnterpriseBusiness.Business;
using BMC.EnterpriseBusiness.Entities;
using BMC.DataExportToSite.BusinessLogic;
using BMC.Common.LogManagement;
using BMC.Common.ConfigurationManagement;
using System.Xml.Linq;
using BMC.Common.ExceptionManagement;

namespace BMC.DataExportToSite.BusinessLogic
{
    class SiteLicensingExpiryChecker
    {
        private ManualResetEvent _mreShutDown = new ManualResetEvent(false);
        private DataHelper dataHelper = new DataHelper();
        private Thread workerThread;
        private DropScheduleBiz objDropScheduleBiz = DropScheduleBiz.CreateInstance();
        private string SiteLicensingSTMDurationInHours = string.Empty;
        private void ReadIntFromConfig(string key, Int32 default_Value, ref Int32 value)
        {
            try
            {
                value = Convert.ToInt32(ConfigManager.Read(key));
            }
            catch (Exception)
            {

            }
            value = (value <= 0) ? default_Value : value;
        }
        /// <summary>
        /// Run the License Expiry checker
        /// </summary>
        private void StartSiteLicensingExpiryChecker()
        {
            try
            {
                LogManager.WriteLog("Site Licensing ==> StartSiteLicensingExpiryChecker:: entry", LogManager.enumLogLevel.Info);
                // int MinuteCounter = 1;
                // int StmCounter = 0;

                int SLTimeInterval = 0;
                int SourceTime = DateTime.Now.Hour;
                //int CurrentTime = DateTime.Now.Hour;
                int DayCounter = 0;
                ReadIntFromConfig("SLTimeInterval", 60, ref SLTimeInterval);
                while (!_mreShutDown.WaitOne(SLTimeInterval * 1000))
                {
                    try
                    {
                        if (!isSiteLicensingEnabled)
                        {
                            LogManager.WriteLog("Site Licensing ==> StartSiteLicensingExpiryChecker:: Site Licensing Feature Disabled for the Site", LogManager.enumLogLevel.Info);
                            _mreShutDown.Set();
                            return;
                        }
                        LogManager.WriteLog("Site Licensing ==> StartSiteLicensingExpiryChecker:: Minute Check DateTime is " + DateTime.Now.ToString("MM/dd/yy HH:mm"), LogManager.enumLogLevel.Debug);

                        dataHelper.UpdateExpiryDateForSL();

                        int LastUpdatedSTMAlert = 0;
                        string sLastUpdatedSTMAlert = string.Empty;
                        objDropScheduleBiz.GetSetting(0, "STMAlertLastUpdatedTime", string.Empty, ref sLastUpdatedSTMAlert);
                        if (!String.IsNullOrEmpty(sLastUpdatedSTMAlert))
                            LastUpdatedSTMAlert = Convert.ToInt32(sLastUpdatedSTMAlert);


                        string SiteLicenseWarningDuration = string.Empty;
                        string SiteLicenseWarningDurationSetting = string.Empty;
                        objDropScheduleBiz.GetSetting(0, "SiteLicensingSTMDurationInDay", string.Empty, ref SiteLicenseWarningDurationSetting);
                        SiteLicenseWarningDuration = SiteLicenseWarningDurationSetting;

                        if (LastUpdatedSTMAlert != DateTime.Now.Day)
                        {
                            LogManager.WriteLog("Source Time Check", LogManager.enumLogLevel.Info);
                            LogManager.WriteLog("Last Updated STM Alert : " + LastUpdatedSTMAlert, LogManager.enumLogLevel.Info);
                            DayCounter++;
                            LogManager.WriteLog("Day Counter : " + DayCounter, LogManager.enumLogLevel.Info);
                        }

                        LogManager.WriteLog("STM Alert For Site Licensing -" + IsSTMAlertForSiteLicensing.ToString(), LogManager.enumLogLevel.Info);
                        LogManager.WriteLog("Day Counter : " + DayCounter, LogManager.enumLogLevel.Info);
                        LogManager.WriteLog("License Expiry Warning Duration-" + SiteLicenseWarningDuration.ToString(), LogManager.enumLogLevel.Info);
                        LogManager.WriteLog("Is Site Licensing Enabled -" + isSiteLicensingEnabled.ToString(), LogManager.enumLogLevel.Info);

                        if (IsSTMAlertForSiteLicensing && (DayCounter >= Convert.ToInt32(SiteLicenseWarningDuration.ToString())) && isSiteLicensingEnabled)
                        {
                            LastUpdatedSTMAlert = DateTime.Now.Day;

                            DayCounter = 0;

                            dataHelper.SiteLicensingExpiryWarning(LastUpdatedSTMAlert);
                        }
                        
                    }
                    catch (Exception ex)
                    {
                        ExceptionManager.Publish(ex);
                    }
                }
                LogManager.WriteLog("Site Licensing ==> StartSiteLicensingExpiryChecker:: end", LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// Stop the thread
        /// </summary>
        public void Stop()
        {
            _mreShutDown.Set();
        }

        /// <summary>
        /// Start the thread if site Licensing feature enabled
        /// </summary>
        public void Start()
        {
            try
            {
                if (isSiteLicensingEnabled)
                {
                    LogManager.WriteLog("Site Licensing ==> SiteLicensingExpiryChecker:: StartScheduler entry", LogManager.enumLogLevel.Info);
                    workerThread = new Thread((StartSiteLicensingExpiryChecker)) { Name = "SiteLicensingExpiryCheckerThread" };
                    workerThread.Priority = ThreadPriority.Normal;
                    workerThread.Start();
                    LogManager.WriteLog("Site Licensing ==> SiteLicensingExpiryChecker:: StartScheduler end", LogManager.enumLogLevel.Info);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        public bool isSiteLicensingEnabled
        {
            get
            {
                string setting_Value = string.Empty;
                objDropScheduleBiz.GetSetting(0, "IsSiteLicensingEnabled", "False", ref setting_Value);
                return string.Compare(setting_Value.Trim().ToUpper(), "TRUE", true) == 0;
            }
        }

        private bool? _isSTMAlertForSiteLicensing = null;
        public bool IsSTMAlertForSiteLicensing
        {
            get
            {
                if (_isSTMAlertForSiteLicensing == null)
                {
                    string setting_Value = string.Empty;
                    objDropScheduleBiz.GetSetting(0, "STMAlertForSiteLicensing", "False", ref setting_Value);
                    _isSTMAlertForSiteLicensing = string.Compare(setting_Value.Trim().ToUpper(), "TRUE", true) == 0;
                }
                return Convert.ToBoolean(_isSTMAlertForSiteLicensing);
            }
        }


        /// <summary>
        /// License Expiry check weather any license configured for site got expired.
        /// If expired means it will take the action based on the license policy. 
        /// </summary>
        public SiteLicensingExpiryChecker()
        {
            try
            {
                LogManager.WriteLog("Site Licensing ==> Constructor:: entry", LogManager.enumLogLevel.Info);
                if (!isSiteLicensingEnabled)
                {
                    LogManager.WriteLog("Site Licensing ==> Constructor:: Site Licensing Feature Disabled for the Site", LogManager.enumLogLevel.Info);
                }
                LogManager.WriteLog("Site Licensing ==> Constructor:: end", LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
    }
}
