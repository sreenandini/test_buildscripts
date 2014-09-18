using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.Business.CashDeskOperator;
using BMC.Transport;

namespace BMC.CashDeskOperator.BusinessObjects
{
    public class SiteLicensingConfiguration : ISiteLicensing
    {
        private static SiteLicensingConfiguration _SiteLicensingConfiguration = null;
        private SiteLicensingBusiness oSiteLicensingbusiness = new SiteLicensingBusiness();

        #region Private Constructor
       
        private SiteLicensingConfiguration() { }
       
        #endregion

        public static SiteLicensingConfiguration SiteLicensingConfigurationInstance
        {
            get
            {
                if (_SiteLicensingConfiguration == null)
                _SiteLicensingConfiguration = new SiteLicensingConfiguration();
                return _SiteLicensingConfiguration;
            }
        }

        #region ISiteLicensing Members

        public bool IsLicensenceAvailable { get { return oSiteLicensingbusiness.IsLicensenceAvailable; } }

        public bool IsSiteLincenseEnabled { get { return oSiteLicensingbusiness.IsSiteLincenseEnabled(); } }

        public int CheckSiteLicenseKey(string sLicenseKey, string sSiteCode, string sUserName) { return oSiteLicensingbusiness.CheckLicenseKey(sLicenseKey, sSiteCode, sUserName); }

        public void UpdateLicenseStaus(string sLicenseKey, int iLicenseKeyStatus, int iUserID) { oSiteLicensingbusiness.UpdateLicenseStaus(sLicenseKey, iLicenseKeyStatus, iUserID); }



        public void GetSiteLicenseList() {  oSiteLicensingbusiness.GetSiteLicenseList();  }

        public int GetSetting(string setting_Name, string setting_Default, ref string setting_Value)
        {
            return oSiteLicensingbusiness.GetSetting(null, setting_Name, setting_Default, ref setting_Value);
        }

        public SiteLicenseDetailsEntity ActiveLicense { get { return oSiteLicensingbusiness.ActiveLicense; } }

        public SiteLicenseDetailsEntity NextActiveLicense { get { return oSiteLicensingbusiness.NextActiveLicense; } }

        public SiteLicenseDetailsEntity ExpiredLicense { get { return oSiteLicensingbusiness.ExpiredLicense; } }

        public SiteLicenseDetailsEntity CancelledLicense { get { return oSiteLicensingbusiness.CancelledLicense; } }

        public int CheckLicenseKey(string licenseKey, string userName)
        {
            return oSiteLicensingbusiness.CheckLicenseKey(licenseKey, userName);
        }

        public int UpdateSlotStatus(bool siteLicensing_DisableGames)
        {
            return oSiteLicensingbusiness.UpdateSlotStatus(siteLicensing_DisableGames);
        }

        public SiteLicenseDetailsEntity CurrentLicense()
        {
            if (!oSiteLicensingbusiness.IsSiteLincenseEnabled())
            {
                return null;
            }
			GetSiteLicenseList();
            SiteLicenseDetailsEntity license = oSiteLicensingbusiness.ActiveLicense;
            if (license != null) return license;
            SiteLicenseDetailsEntity cancelledLicense = oSiteLicensingbusiness.CancelledLicense;
            if (cancelledLicense != null)
                 return cancelledLicense;
            return oSiteLicensingbusiness.ExpiredLicense;
        }



        
        #endregion ISiteLicensing Members

    }
}
