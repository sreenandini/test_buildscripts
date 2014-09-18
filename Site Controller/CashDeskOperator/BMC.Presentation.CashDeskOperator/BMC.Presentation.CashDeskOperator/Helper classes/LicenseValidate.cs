using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.CashDeskOperator.BusinessObjects;
using BMC.Transport;
using BMC.Security;
using System.Windows;
using BMC.Common.LogManagement;
using System.Globalization;
using BMC.Common.Utilities;

namespace BMC.Presentation
{
    public class LicenseValidator
    {

        private static Window _currentWindow = null;
        private static LicenseValidator _LicenseValidator = null;
        public LicenseActivation objLicenseActivation = null;
        private SiteLicensingConfiguration _licensingConfig = SiteLicensingConfiguration.SiteLicensingConfigurationInstance;
        private static bool _lincenseScreenRequired = false; 

        public static LicenseValidator GetLicenseValidator
        {
            get
            {
                if (_LicenseValidator == null)
                    _LicenseValidator = new LicenseValidator();
                _lincenseScreenRequired = SecurityHelper.HasAccess("CashdeskOperator.MainScreen.cs.LicenseActivation");
                return _LicenseValidator;
            }
        }
        public static Window CurrentWindow
        {
            get
            {
                return _currentWindow;
            }
            set
            {
                _currentWindow = value;
            }
        }

        /// <summary>
        /// Validate License will allow the user to enter into application or not
        /// </summary>
        //public bool Validate
        //{
        //    get
        //    {
        //        return ValidateLicense();
        //    }
        //}
        private LicenseValidator() 
        {
            
        }

        private void ShowLicenseScreen(string LicenseMessage, string WarningMessage, string Params)
        {
            if (!_lincenseScreenRequired)
            {
                MessageBox.ShowBox(WarningMessage, BMC_Icon.Warning, BMC_Button.OK, Params);
                return;
            }
            objLicenseActivation = new LicenseActivation(LicenseMessage); 
            objLicenseActivation.Owner = CurrentWindow;
            CurrentWindow.ShowInTaskbar = false;
            objLicenseActivation.ShowDialog();
            CurrentWindow.ShowInTaskbar = true;
        }

        /// <summary>
        /// To check whether the site having valid license and allow/restrict the user to login to the site based on the license settings
        /// </summary>
        /// <returns></returns>
        /// 
        public bool ValidateLicense()
        {
            try
            {
                LogManager.WriteLog(string.Format("Checking for the site  [{0}], whether site licensing is mandatory- ", Settings.SiteCode), LogManager.enumLogLevel.Info);
                if (!_licensingConfig.IsSiteLincenseEnabled) return true;

                _licensingConfig.GetSiteLicenseList();

                LogManager.WriteLog(string.Format("Checking for the active license for the Site- " + Settings.SiteCode), LogManager.enumLogLevel.Info);
                if (!_licensingConfig.IsLicensenceAvailable) 
                {
                    LogManager.WriteLog(string.Format("Site {0} doesn't have any License. So user not allowed to login.", Settings.SiteCode), LogManager.enumLogLevel.Info);
                    ShowLicenseScreen(Convert.ToString(Application.Current.FindResource("MessageID449")), "MessageID471","");
                    return false;
                }

                SiteLicenseDetailsEntity activeLicense = _licensingConfig.ActiveLicense;

                if (activeLicense != null)
                {
                    LogManager.WriteLog(string.Format("License check has been completed and Site [{0}] having the valid license.", Settings.SiteCode), LogManager.enumLogLevel.Info);
                    if (!activeLicense.ValidationRequired) return true;

                    TimeSpan tsDate = activeLicense.ExpireDate.Subtract(DateTime.Now);
                    if (Convert.ToInt64(tsDate.TotalDays) > activeLicense.AlertBeforeDays.GetValueOrDefault()) return true;
                    SiteLicenseDetailsEntity NextActiveLicense = _licensingConfig.NextActiveLicense;
                    if (NextActiveLicense != null)
                    {
                        TimeSpan tsDate1 = NextActiveLicense.StartDate.Subtract(activeLicense.ExpireDate);
                        string settingValue = string.Empty;
                        _licensingConfig.GetSetting("GraceTime", "5", ref settingValue);
                        Int32 graceTime = 15;
                        Int32.TryParse(settingValue, out graceTime);
                        LogManager.WriteLog(
                            string.Format("License is going to be expired in {0} days for the Site {1}.\r\nCurrent License Expired date {2}\r\nNext License Available from {3}", tsDate.Days, 
                            Settings.SiteCode, 
                            activeLicense.ExpireDate.ToString(GetDateFormat()),
                            NextActiveLicense.StartDate.ToString(GetDateFormat())), 
                            LogManager.enumLogLevel.Info);
                        if(tsDate1.TotalSeconds > graceTime)
                        {
                            MessageBox.ShowBox("SL_MessageID463", BMC_Icon.Warning, BMC_Button.OK, activeLicense.ExpireDate.ToString(GetDateFormat()), 
                                _licensingConfig.NextActiveLicense.StartDate.ToString(GetDateFormat())); //License is going  expires on dd/MM/yyyy. Please activate a new license for the Site before the expiry date.
                        }
                    }
                    else
                    {
                        LogManager.WriteLog(string.Format("License is going to be expired in {0} days for the Site {1}.", tsDate.Days, Settings.SiteCode), LogManager.enumLogLevel.Info);
                        MessageBox.ShowBox("MessageID452", BMC_Icon.Warning, BMC_Button.OK, activeLicense.ExpireDate.ToString(GetDateFormat())); //License is going  expires on dd/MM/yyyy. Please activate a new license for the Site before the expiry date.
                    }
                    return true;
                }

                SiteLicenseDetailsEntity cancelledLicense = _licensingConfig.CancelledLicense;
                if ((cancelledLicense != null) && (!cancelledLicense.ValidationRequired))
                {
                    return true;
                }
                else if ((cancelledLicense != null) && (cancelledLicense.LockSite))
                {
                    LogManager.WriteLog(string.Format("Site [{0}] doesn't have any active license and user not allowed to login to the site.", Settings.SiteCode), LogManager.enumLogLevel.Info);
                    ShowLicenseScreen(Convert.ToString(Application.Current.FindResource("MessageID476")), "MessageID473","");
                    return false;
                }
                else if ((cancelledLicense != null) && (cancelledLicense.WarningOnly))
                {
                    LogManager.WriteLog(string.Format("Site [{0}] doesn't have any active license but user is allowed to login to the site. cancelledLicense.WarningOnly", Settings.SiteCode), LogManager.enumLogLevel.Info);
                    MessageBox.ShowBox("MessageID474", BMC_Icon.Warning, BMC_Button.OK);//The Active License for the Site was cancelled. Please activate a new License for the Site.
                    return true;
                }
                else if ((cancelledLicense != null) && (cancelledLicense.DisableGames))
                {
                    LogManager.WriteLog(string.Format("Site [{0}] doesn't have any active license but user is allowed to login to the site. cancelledLicense.DisableGames", Settings.SiteCode), LogManager.enumLogLevel.Info);
                    MessageBox.ShowBox("MessageID475", BMC_Icon.Warning, BMC_Button.OK);//The Active License for the Site was cancelled. All Slot Machines are disabled based on the license policy. Please activate a new License for the Site.
                    return true;
                }
                SiteLicenseDetailsEntity expireLicense = _licensingConfig.ExpiredLicense;
                String expiryDate = string.Empty;
                if (expireLicense != null && expireLicense.ExpireDate != null)
                {
                    expiryDate = expireLicense.ExpireDate.ToString(GetDateFormat());
                }

                if ((expireLicense != null) && (!expireLicense.ValidationRequired))
                {
                    return true;
                }
                else if ((expireLicense != null) && (expireLicense.LockSite))
                {
                    LogManager.WriteLog(string.Format("Site [{0}] doesn't have any active license and user not allowed to login to the site.", Settings.SiteCode), LogManager.enumLogLevel.Info);
                    ShowLicenseScreen(string.Format(Application.Current.FindResource("MessageID450").ToString(),expiryDate), "MessageID472", expiryDate);
                    return false;
                }
                else if ((expireLicense != null) && (expireLicense.WarningOnly))
                {
                    LogManager.WriteLog(string.Format("Site [{0}] doesn't have any active license but user is allowed to login to the site. expireLicense.WarningOnly", Settings.SiteCode), LogManager.enumLogLevel.Info);
                    MessageBox.ShowBox("MessageID451", BMC_Icon.Warning, BMC_Button.OK, expiryDate); //License has been expired on @@@@@@. Please activate the site with new valid license key.
                    return true;
                }
                else if ((expireLicense != null) && (expireLicense.DisableGames))
                {
                    LogManager.WriteLog(string.Format("Site [{0}] doesn't have any active license but user is allowed to login to the site. ExpireLicense.DisableGames", Settings.SiteCode), LogManager.enumLogLevel.Info);
                    MessageBox.ShowBox("MessageID467", BMC_Icon.Warning, BMC_Button.OK, expiryDate); //Site License has expired on @@@@@@. All Slot Machines are disabled based on the license policy. Please activate a new License for the Site.
                    return true;
                }
                else
                {
                    LogManager.WriteLog(string.Format("Site [{0}] doesn't have any active license and user not allowed to login to the site.", Settings.SiteCode), LogManager.enumLogLevel.Info);
                    ShowLicenseScreen(Convert.ToString(Application.Current.FindResource("MessageID449")), "MessageID471","");
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Exception Occured in ValidateLicense method " + ex.Message + "\r\n\r\n" + ex.StackTrace, LogManager.enumLogLevel.Info);
            }
            return false;
        }

        private String GetDateFormat()
        {
            CultureInfo currentDateCultureFormater = new CultureInfo(ExtensionMethods.CurrentDateCulture);
            String Format = "d";
            if (currentDateCultureFormater != null)
                Format = currentDateCultureFormater.DateTimeFormat.ShortDatePattern + " " + currentDateCultureFormater.DateTimeFormat.ShortTimePattern;
            return Format;
        }
    }
}
