using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.CashDeskOperator.BusinessObjects;
using BMC.Transport;
using BMCIPC;
using BMC.Common.ConfigurationManagement;
using BMC.Common.LogManagement;
using System.Timers;
using BMC.Common.ExceptionManagement;

namespace BMC.BusinessClasses.BusinessLogic
{
    public class SiteLicensingExpiryChecker : IDisposable
    {
        private SiteLicensingConfiguration oSiteLicensingConfiguration = null;
        private static SiteLicensingExpiryChecker _objSiteLicensingExpiryChecker = null;
        private const string messageString = "SITELICENSING";
        //DateTime? lastMessageDate = null;
        DateTime? lastActiveWarningMessage = null;
        //DateTime? lastCancelledWarningMessage = null;
        //private Server g_oserver = null;

        public static SiteLicensingExpiryChecker Instance
        {
            get
            {
                try
                {
                    if (_objSiteLicensingExpiryChecker == null)
                        _objSiteLicensingExpiryChecker = new SiteLicensingExpiryChecker();
                    return _objSiteLicensingExpiryChecker;
                }
                catch (Exception ex)
                {
                    ExceptionManager.Publish(ex);
                    _objSiteLicensingExpiryChecker = new SiteLicensingExpiryChecker();
                    return _objSiteLicensingExpiryChecker;
                }
            }
        }

        private SiteLicensingExpiryChecker()
        {
            try
            {
                //LogManager.WriteLog("SiteLicensingExpiryChecker ==> Constructer start ", LogManager.enumLogLevel.Info);
                
                //LogManager.WriteLog("SiteLicensingExpiryChecker ==> Constructer End ", LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        public SiteLicensingDataResponse GetSiteLicenseValidation()
        {
            try
            {
                oSiteLicensingConfiguration = SiteLicensingConfiguration.SiteLicensingConfigurationInstance;
                if (!oSiteLicensingConfiguration.IsSiteLincenseEnabled)
                {
                    LogManager.WriteLog("SiteLicensingExpiryChecker ==> Site Licensing Disabled", LogManager.enumLogLevel.Debug);
                    return null;
                }
                oSiteLicensingConfiguration.GetSiteLicenseList();
                SiteLicenseDetailsEntity activeLicense = oSiteLicensingConfiguration.ActiveLicense;
                if (activeLicense != null)
                {
                    LogManager.WriteLog("SiteLicensingExpiryChecker ==> Active License Available with Expiry Date " + activeLicense.ExpireDate.ToString(), LogManager.enumLogLevel.Info);
                    DateTime date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, activeLicense.ExpireDate.Hour, activeLicense.ExpireDate.Minute, activeLicense.ExpireDate.Second);
                    //timer.Interval = (ticks > 0) ? (ticks + 25000) : 60000;//25 seconds added for grace period. It will avoid to lock the site
                    TimeSpan tsDate = activeLicense.ExpireDate.Subtract(DateTime.Now);
                    if (!activeLicense.AlertRequired || tsDate.Days > activeLicense.AlertBeforeDays.GetValueOrDefault())
                    {
                        LogManager.WriteLog("SiteLicensingExpiryChecker ==> Active License does not require any validation", LogManager.enumLogLevel.Info);
                        LogManager.WriteLog("SiteLicensingExpiryChecker ==> Timer_Elapsed end", LogManager.enumLogLevel.Info);
                        return null;
                    }
                    return null;
                }
                SiteLicenseDetailsEntity cancelledLicense = oSiteLicensingConfiguration.CancelledLicense;
                SiteLicenseDetailsEntity expiredLicense = oSiteLicensingConfiguration.ExpiredLicense;
                if ((cancelledLicense != null && !cancelledLicense.ValidationRequired)
                    || (cancelledLicense == null && expiredLicense != null && !expiredLicense.ValidationRequired))
                {
                    LogManager.WriteLog("SiteLicensingExpiryChecker ==> License does not require any validation", LogManager.enumLogLevel.Info);
                    return null;
                }
                if ((cancelledLicense != null && cancelledLicense.DisableGames)
                    || (cancelledLicense == null && expiredLicense != null && expiredLicense.DisableGames))
                {
                    if (cancelledLicense != null)
                        LogManager.WriteLog("SiteLicensingExpiryChecker ==> Active License get cancelled", LogManager.enumLogLevel.Info);
                    else if (expiredLicense != null)
                        LogManager.WriteLog("SiteLicensingExpiryChecker ==> Expired License Found", LogManager.enumLogLevel.Info);
                        oSiteLicensingConfiguration.UpdateSlotStatus(true);
                }
                if ((cancelledLicense != null && cancelledLicense.LockSite)
                    || (cancelledLicense == null && expiredLicense != null && expiredLicense.LockSite))
                {
                    return this.GetSiteLicenseData(cancelledLicense == null ? expiredLicense : cancelledLicense);
                }
                if ((cancelledLicense != null && cancelledLicense.WarningOnly)
                    || (cancelledLicense == null && expiredLicense != null && expiredLicense.WarningOnly))
                {
                        return null;
                }
               return this.GetSiteLicenseData(null);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            return null;
        }

        public SiteLicensingDataResponse GetSiteLicenseData(SiteLicenseDetailsEntity entity)
        {
            return new SiteLicensingDataResponse()
            {
                Response = new List<SiteLicensingData>()
                {

                }
            };
        }

        public void Start()
        {
            //LogManager.WriteLog("SiteLicensingExpiryChecker ==> Start Method start", LogManager.enumLogLevel.Info);
            ////timer.Enabled = true;
            ////timer.Start();
            //LogManager.WriteLog("SiteLicensingExpiryChecker ==> Start Method End", LogManager.enumLogLevel.Info);

        }

        public void Stop()
        {
            //LogManager.WriteLog("SiteLicensingExpiryChecker ==> Stop Method start", LogManager.enumLogLevel.Info);
            ////timer.Enabled = false;
            ////timer.Stop();
            //LogManager.WriteLog("SiteLicensingExpiryChecker ==> Stop Method End", LogManager.enumLogLevel.Info);
        }

        private void Reset()
        {
           // LogManager.WriteLog("SiteLicensingExpiryChecker ==> Reset Method start", LogManager.enumLogLevel.Info);
           // //timer.Interval = 60000;
           //// lastMessageDate = null;
           // LogManager.WriteLog("SiteLicensingExpiryChecker ==> Reset Method End", LogManager.enumLogLevel.Info);
        }

        /// <summary>
        /// Based on feature enable/disable it will start or stop the Site Licensing Timer
        /// </summary>
        public void ValidateLicenseEnabled()
        {
            //LogManager.WriteLog("SiteLicensingExpiryChecker ==> ValidateLicenseEnabled Method start", LogManager.enumLogLevel.Info);
            //if (oSiteLicensingConfiguration.IsSiteLincenseEnabled)
            //{
            //    this.Start();
            //    this.Reset();
            //}
            //else
            //{
            //    this.Stop();
            //}
            //LogManager.WriteLog("SiteLicensingExpiryChecker ==> ValidateLicenseEnabled Method end", LogManager.enumLogLevel.Info);
        }

        #region IDisposable Members

        public void Dispose()
        {
            this.Stop();
        }

        #endregion
    }
}
