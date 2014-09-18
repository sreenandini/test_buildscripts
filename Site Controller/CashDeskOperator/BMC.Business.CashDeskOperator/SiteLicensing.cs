using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using BMC.Common.ConfigurationManagement;
using BMC.Common.ExceptionManagement;
using BMC.Common.LogManagement;
using BMC.DBInterface.CashDeskOperator;
using BMC.Business.CashDeskOperator.WebServices;
using BMC.Transport;
using Microsoft.Win32;
using BMC.Security;

namespace BMC.Business.CashDeskOperator
{
    public class SiteLicensingBusiness
    {
        #region Data Members

        private SiteLicensingDataAccess SiteLicensingDataAccess = new SiteLicensingDataAccess();
        private Proxy oProxy = null;
        private string sVerifyURL = string.Empty;
        private List<SiteLicenseDetailsEntity> _SiteLicenseList = null;
        SiteLicenseDetailsEntity _ActiveLicense = null;
        SiteLicenseDetailsEntity _ExpiredLicense = null;
        SiteLicenseDetailsEntity _CancelledLicense = null;
        SiteLicenseDetailsEntity _NextActiveLicense = null;

        #endregion //Data Members

        #region Enum

        /// <summary>
        /// Enum for License Key Status - should be same as Enterprise.SL_KeyStatus table value
        /// </summary>
        public enum LicenseKeyStatus
        {
            UnDefined = 0,
            Created = 1,
            Active = 2,
            Expired = 3,
            Cancelled = 4
        }

        #endregion //Enum

        #region Constructor
        
        public SiteLicensingBusiness()
        {
            //ConfigManager.SetConfigurationMode(ConfigManager.ConfigurationMode.AppConfig);
        }

        #endregion //Constructor

        #region Methods

        public int CheckLicenseKey(string sLicenseKey, string sSiteCode, string sUserName)
        {
            int iValue = 0;
            iValue = CheckLicenseKeyFromServer(sLicenseKey, sSiteCode, sUserName);
            LogManager.WriteLog("ChecklicenseKeyFromServer from server:  " + iValue.ToString(), LogManager.enumLogLevel.Info);
            return iValue;
        }

        private int CheckLicenseKeyFromServer(string sLicenseKey, string sSiteCode, string sUserName)
        {
            int iResult = -1;
            try
            {
                if (oProxy == null)
                    oProxy = new Proxy(sSiteCode.ToString());
                iResult = Convert.ToInt32(oProxy.CheckLicenseKey(sLicenseKey, sSiteCode, sUserName));
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                iResult = -1;
            }
            return iResult;
        }

        public void UpdateLicenseStaus(string sLicenseKey, int iLicenseKeyStatus, int iUserID)
        {
            try
            {
                SiteLicensingDataAccess.UpdateLicenseStaus(SiteLicensingCryptoHelper.Encrypt(sLicenseKey, "B411y51T"), iLicenseKeyStatus, iUserID);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        public bool IsSiteLincenseEnabled()
        {

            return SiteLicensingDataAccess.IsSiteLincenseEnabled();
        }

        public bool IsLicensenceAvailable
        {
            get
            {
                return (_SiteLicenseList.Count > 0);
            }
        }

        public int GetSetting(int? setting_ID, string setting_Name, string setting_Default, ref string setting_Value)
        {
            return SiteLicensingDataAccess.GetSetting(setting_ID, setting_Name, setting_Default, ref setting_Value);
        }

        public void GetSiteLicenseList()
        {
            IEnumerable<SiteLicenseDetailsEntity> licenseDetailsResults = SiteLicensingDataAccess.GetSiteLicenseDetailsResult().Select(X => 
                new SiteLicenseDetailsEntity 
                { 
                    StartDate = X.StartDate,
                    ExpireDate = Convert.ToDateTime(SiteLicensingCryptoHelper.Decrypt(X.ExpireDate, "B411y51T")),
                    RuleID = X.RuleID,
                    AlertBeforeDays = X.AlertBeforeDays,
                    KeyStatusID = X.KeyStatusID,
                    LicenseKey = X.LicenseKey,
                    ValidationRequired = X.ValidationRequired,
                    LockSite = X.LockSite,
                    DisableGames = X.DisableGames,
                    WarningOnly = X.WarningOnly,
                    AlertRequired = X.AlertRequired,
                    CreatedDateTime = X.CreatedDateTime,
                    ActivatedDateTime = X.ActivatedDateTime,
                    UpdatedDateTime = X.UpdatedDateTime
                    
            });
            _ActiveLicense = null;
            _ExpiredLicense = null;
            _CancelledLicense = null;
            _NextActiveLicense = null;
            _SiteLicenseList = licenseDetailsResults.ToList();
        }

        public SiteLicenseDetailsEntity ActiveLicense
        {
            get
            {
                if (_ActiveLicense == null)
                    _ActiveLicense = _SiteLicenseList.Where(siteLicenseDetail => (siteLicenseDetail.KeyStatusID == Convert.ToInt32(LicenseKeyStatus.Active)
                                                                                                && siteLicenseDetail.StartDate <= DateTime.Now 
                                                                                                && siteLicenseDetail.ExpireDate >= DateTime.Now))
                                                                                                .FirstOrDefault() as SiteLicenseDetailsEntity; 
                return _ActiveLicense;
            }
        }

        public SiteLicenseDetailsEntity NextActiveLicense
        {
            get
            {
                if (ActiveLicense != null && _NextActiveLicense == null)
                    _NextActiveLicense = _SiteLicenseList.Where(siteLicenseDetail => (siteLicenseDetail.KeyStatusID == Convert.ToInt32(LicenseKeyStatus.Active)
                                                                                                && siteLicenseDetail.StartDate >= ActiveLicense.ExpireDate &&
                                                                                                siteLicenseDetail.ExpireDate >= DateTime.Now))
                                                                                                .OrderBy(siteLicenseDet => siteLicenseDet.StartDate)
                                                                                                .FirstOrDefault() as SiteLicenseDetailsEntity;
                return _NextActiveLicense;
            }
        }

        public SiteLicenseDetailsEntity ExpiredLicense
        {
            get
            {
                if (_ExpiredLicense == null)
                    _ExpiredLicense = _SiteLicenseList.Where(siteLicenseDetail => ((siteLicenseDetail.KeyStatusID == Convert.ToInt32(LicenseKeyStatus.Expired))
                                                || ((siteLicenseDetail.StartDate < DateTime.Now
                                                && siteLicenseDetail.ExpireDate < DateTime.Now)
                                                    && siteLicenseDetail.KeyStatusID == Convert.ToInt32(LicenseKeyStatus.Active)))
                                                    && siteLicenseDetail.ActivatedDateTime != null)
                                                    .OrderByDescending(siteLicenseDet => siteLicenseDet.ExpireDate)
                                                    .FirstOrDefault() as SiteLicenseDetailsEntity;
                return _ExpiredLicense;
            }
        }


        public SiteLicenseDetailsEntity CancelledLicense
        {
            get
            {
                if (_CancelledLicense == null)
                {
                    SiteLicenseDetailsEntity _entity = _SiteLicenseList.Where(siteLicenseDetail => siteLicenseDetail.StartDate < DateTime.Now 
                                                    && siteLicenseDetail.ExpireDate < DateTime.Now
                                                    && siteLicenseDetail.KeyStatusID == Convert.ToInt32(LicenseKeyStatus.Active) 
                                                    && siteLicenseDetail.ActivatedDateTime != null)
                                                    .OrderByDescending(siteLicenseDet => siteLicenseDet.ExpireDate)
                                                    .FirstOrDefault() as SiteLicenseDetailsEntity;
                    if(_entity == null)
                    _entity = _SiteLicenseList.Where(siteLicenseDetail =>
                        (siteLicenseDetail.KeyStatusID == Convert.ToInt32(LicenseKeyStatus.Expired)
                        || (siteLicenseDetail.KeyStatusID == Convert.ToInt32(LicenseKeyStatus.Cancelled)
                        && siteLicenseDetail.StartDate < DateTime.Now)) && siteLicenseDetail.ActivatedDateTime != null)
                                                   .OrderByDescending(siteLicenseDet => siteLicenseDet.UpdatedDateTime)
                                                   .FirstOrDefault() as SiteLicenseDetailsEntity;
                    if (_entity != null && _entity.KeyStatusID == Convert.ToInt32(LicenseKeyStatus.Cancelled))
                        _CancelledLicense = _entity;
                    else
                        _ExpiredLicense = _entity;
                }
                return _CancelledLicense;
            }
        }

        public int CheckLicenseKey(string licenseKey, string userName)
        {
            return SiteLicensingDataAccess.CheckLicenseKey(SiteLicensingCryptoHelper.Encrypt(licenseKey.Trim(), "B411y51T"), userName);
        }

        public int UpdateSlotStatus(bool siteLicensing_DisableGames)
        {
            return SiteLicensingDataAccess.UpdateSlotStatus(siteLicensing_DisableGames);

        }

        #endregion //Methods
    }
}
