using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Reflection;
using BMC.SiteLicensing.Business;
using System.Linq.Expressions;
using BMC.DataAccess;
using System.Data;
using System.Data.SqlClient;
using BMC.Common.LogManagement;
using BMC.Common.ConfigurationManagement;

namespace BMC.SiteLicensing.DataAccess
{
    public class DataManipulator
    {
        #region Properties
        /// <summary>
        /// Holds the connection string
        /// </summary>
        public static string EnterpriseConnectionString
        { get { return GetConnectionString(); } }

        #endregion Properties

        #region Static Methods

        #region Common Methods

        /// <summary>
        /// To get the connection string from BMC Common utilities
        /// </summary>
        /// <returns></returns>
        public static string GetConnectionString()
        {
            try
            {
                return BMC.Common.Utilities.DatabaseHelper.GetConnectionString();
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error in DM.GetConnectionString method" + "-Error Message-" + ex.Message, LogManager.enumLogLevel.Error);
                return string.Empty;
            }
        }

        public static ISingleResult<rsp_GetSiteLicensingRightsResult> GetSiteLicensingRightsResult()
        {
            LicensingDataContext licensingDataContext = new LicensingDataContext(EnterpriseConnectionString);
            return licensingDataContext.rsp_GetSiteLicensingRights(BusinessLogic.iLoginStaffID);
        }

        #endregion //Common Methods

        #region Key Generation methods

        /// <summary>
        /// Validated selected dates with existing license dates
        /// </summary>
        /// <param name="siteID"></param>
        /// <param name="dtStartDate"></param>
        /// <param name="dtExpiryDate"></param>
        /// <returns></returns>
        public static int ValidateLicenseDates(int siteID, DateTime dtStartDate, DateTime dtExpiryDate)
        {
            LicensingDataContext licensingDataContext = new LicensingDataContext(EnterpriseConnectionString);
            return licensingDataContext.rsp_SL_ValidateLicenseDates(siteID, dtStartDate, dtExpiryDate);
        }

        /// <summary>
        /// Insert License information
        /// </summary>
        /// <param name="dtStartDate"></param>
        /// <param name="dtExpiryDate"></param>
        /// <param name="iRuleID"></param>
        /// <param name="iSiteID"></param>
        /// <param name="strLicenseKey"></param>
        /// <param name="iAlterBeforeDays"></param>
        /// <param name="iKeyStatusID"></param>
        /// <param name="iUserID"></param>
        /// <returns></returns>
        public static int InsertLicenseInfo(DateTime dtStartDate, DateTime dtExpiryDate, int iRuleID, int iSiteID, String strLicenseKey, short iAlterBeforeDays, int iKeyStatusID, int iUserID)
        {
            LicensingDataContext licensingDataContext = new LicensingDataContext(EnterpriseConnectionString);
            return licensingDataContext.usp_SL_InsertLicenseDetails(dtStartDate, dtExpiryDate, iRuleID, iSiteID, strLicenseKey, iAlterBeforeDays, iKeyStatusID, iUserID);
        }

        #endregion Key Generation Methods

        #region Rule Information Methods

        #region  Full Table

        /// <summary>
        /// Displays the full Rule table
        /// </summary>
        /// <returns></returns>
        public static Table<SL_Rule> GetRuleTable()
        {
            LicensingDataContext dc = new LicensingDataContext(EnterpriseConnectionString);
            return dc.GetTable<SL_Rule>();
        }

        #endregion

        /// <summary>
        /// Get All Rule Names from the Database
        /// </summary>
        /// <returns></returns>
        public static String[] GetAllRuleNames()
        {
            LicensingDataContext dc = new LicensingDataContext(EnterpriseConnectionString);
            return dc.SL_Rules.Select(X => X.RuleName).ToArray<String>();
        }

        /// <summary>
        /// Fetch similar settings rule names
        /// </summary>
        /// <param name="validationRequired"></param>
        /// <param name="lockSite"></param>
        /// <param name="disableGames"></param>
        /// <param name="warningOnly"></param>
        /// <param name="alertRequired"></param>
        /// <returns></returns>
        public static String[] GetSimilarRules(bool validationRequired, bool lockSite, bool disableGames, bool warningOnly,
            bool alertRequired)
        {
            LicensingDataContext dc = new LicensingDataContext(EnterpriseConnectionString);
            return dc.SL_Rules.Where(Y => Y.ValidationRequired == validationRequired && Y.LockSite == lockSite && Y.DisableGames == disableGames && Y.WarningOnly == warningOnly && Y.AlertRequired == alertRequired).Select(X => X.RuleName).ToArray<String>();
        }

        /// <summary>
        /// Get the rule id for the rule name
        /// </summary>
        /// <param name="ruleName"></param>
        /// <returns></returns>
        public static int GetRuleID(String ruleName)
        {
            LicensingDataContext dc = new LicensingDataContext(EnterpriseConnectionString);
            int count = dc.SL_Rules.Count(Y => Y.RuleName == ruleName);
            if (count == 1)
                return dc.SL_Rules.Where(Y => Y.RuleName == ruleName).Single().RuleID;
            else
                return -1;
        }

        /// <summary>
        /// Fetch rule Information
        /// </summary>
        /// <param name="ruleName"></param>
        /// <returns></returns>
        public static SL_Rule GetRuleInformation(String ruleName)
        {
            LicensingDataContext dc = new LicensingDataContext(EnterpriseConnectionString);
            return (SL_Rule)(dc.SL_Rules.Where(X => X.RuleName == ruleName).SingleOrDefault());
        }

        /// <summary>
        /// Get Associated Sites
        /// </summary>
        /// <param name="iRuleID"></param>
        /// <returns></returns>
        public static List<String> GetAssociatedSites(Int32 iRuleID, Int32 iLoginUserID)
        {
            LicensingDataContext dc = new LicensingDataContext(EnterpriseConnectionString);
            List<String> lstSiteCodes = dc.GetAssociatedSites(iRuleID, iLoginUserID).Select(X => X.Site_Name).ToList<string>();
            return lstSiteCodes;
        }

        /// <summary>
        /// Finding rule is associated with any license to warn user before its getting to update 
        /// </summary>
        /// <param name="ruleName"></param>
        /// <returns></returns>
        public static bool IsRuleAssociated(String ruleName)
        {
            LicensingDataContext dc = new LicensingDataContext(EnterpriseConnectionString);
            int ruleID = GetRuleID(ruleName);
            if (dc.SL_LicenseInfos.First(X => X.RuleID == ruleID) != null)   //SL_Rules.Where(X => X.RuleName == ruleName).SingleOrDefault());
                return true;
            return false;
        }

        /// <summary>
        /// List of Rules with IDs
        /// </summary>
        /// <param name="iSubCompanyId"></param>
        /// <returns></returns>
        public static ISingleResult<rsp_SL_GetRuleDetailsResult> GetRuleDetails()
        {
            LicensingDataContext licensingDataContext = new LicensingDataContext(EnterpriseConnectionString);
            return licensingDataContext.rsp_SL_GetRuleDetails();
        }



        public static ISingleResult<rsp_SL_GetuserName> GetUSerDetails()
        {
            LicensingDataContext licensingDataContext = new LicensingDataContext(EnterpriseConnectionString);
            return licensingDataContext.rsp_SL_GetuserName();
        }

        #region Inserting, Updating Data

        /// <summary>
        /// Update the rule based on the rule ID
        /// or Insert new row
        /// </summary>
        /// <param name="ruleName"></param>
        /// <param name="validationRequired"></param>
        /// <param name="lockSite"></param>
        /// <param name="disableGames"></param>
        /// <param name="warningOnly"></param>
        /// <param name="alertRequired"></param>
        /// <param name="userId"></param>
        public static int InsertorUpdateRule(string ruleName,
            bool validationRequired, bool lockSite, bool disableGames, bool warningOnly,
            bool alertRequired, int userId)
        {
            LicensingDataContext dc = new LicensingDataContext(EnterpriseConnectionString);
            return dc.usp_SL_InsertorUpdateRule(ruleName, validationRequired, lockSite, disableGames, warningOnly, alertRequired, userId);
        }

        #endregion Inserting, Updating Data

        #endregion Rule Information Methods

        #region View/Cancel License Methods

        /// <summary>
        /// To get Company, SubCompany, Site and site License status
        /// </summary>
        /// <returns></returns>
        public static ISingleResult<rsp_SL_GetSitesWithLicenseDetailsResult> GetSitesList(Int32 iLoginUserID)
        {
            LicensingDataContext licensingDataContext = new LicensingDataContext(EnterpriseConnectionString);
            return licensingDataContext.rsp_SL_GetSitesWithLicenseDetails(iLoginUserID);
        }

        /// <summary>
        /// To load the licenses based on the selected site
        /// </summary>
        /// <param name="iSiteId"></param>
        /// <returns></returns>
        public static ISingleResult<rsp_SL_GetLicenseDetailsResult> GetLicenseDetails(Int32 iSiteId)
        {
            LogManager.WriteLog("Loading the License details for the Site: " + iSiteId, LogManager.enumLogLevel.Info);
            LicensingDataContext licensingDataContext = new LicensingDataContext(EnterpriseConnectionString);
            return licensingDataContext.rsp_SL_GetLicenseDetails(iSiteId);
        }

        /// <summary>
        /// To update license key staus
        /// </summary>
        /// <param name="iLicenseInfoId"></param>
        /// <param name="iLicenseKeyStatus"></param>
        /// <returns></returns>
        public static int UpdateLicenseStaus(Int32 iLicenseInfoId, Int32 iLicenseKeyStatus, Int32 iLoginUserID)
        {
            LicensingDataContext licensingDataContext = new LicensingDataContext(EnterpriseConnectionString);
            return licensingDataContext.usp_SL_UpdateLicenseStatus(iLicenseInfoId, iLicenseKeyStatus, iLoginUserID);
        }

        #endregion View/Cancel License Methods


        /// <summary>
        /// To update license key staus
        /// </summary>
        /// <param name="iLicenseInfoId"></param>
        /// <param name="iLicenseKeyStatus"></param>
        /// <returns></returns>
        #region Activate License Methods

        public static int ActivateLicenseStaus(Int32 iLicenseInfoId, Int32 iLicenseKeyStatus, Int32 iLoginUserID)
        {
            LicensingDataContext licensingDataContext = new LicensingDataContext(EnterpriseConnectionString);
            int i =licensingDataContext.usp_SL_UpdateActiveLicense(iLicenseInfoId, iLicenseKeyStatus, iLoginUserID);
            return i;
        }


        #endregion Activate License Methods



        #region License Search Methods

        /// <summary>
        /// Based on the input search parameters fetch the search results
        /// </summary>
        /// <param name="iCompanyId"></param>
        /// <param name="iSubCompanyId"></param>
        /// <param name="iSiteId"></param>
        /// <param name="iStartDate"></param>
        /// <param name="iExpireDate"></param>
        /// <param name="iKeyStatus"></param>
        /// <param name="bValidationReq"></param>
        /// <param name="bLockSite"></param>
        /// <param name="bDisableEGM"></param>
        /// <param name="bWarningOnly"></param>
        /// <returns></returns>
        public static ISingleResult<rsp_SL_GetLicenseHistoryDetailsResult> GetLicenseHistorySearch(Int32? iCompanyId, Int32? iSubCompanyId, Int32? iSiteId, DateTime? iFromStartDate, DateTime? iToStartDate, DateTime? iFromExpiryDate, DateTime? iToExpiryDate, Int32? iKeyStatus, bool? bValidationReq, bool? bLockSite, bool? bDisableEGM, bool? bWarningOnly, bool? bAlertRequired, Int32? iLoginUserID, int cmbcreateByvalue, int cmbActivatedByvalue, int cmbCancelvalue)
        {
            LicensingDataContext licensingDataContext = new LicensingDataContext(EnterpriseConnectionString);
            return licensingDataContext.rsp_SL_GetLicenseHistoryDetails(iCompanyId, iSubCompanyId, iSiteId, iFromStartDate, iToStartDate, iFromExpiryDate, iToExpiryDate, iKeyStatus, bValidationReq, bLockSite, bDisableEGM, bWarningOnly, bAlertRequired, iLoginUserID,cmbcreateByvalue,cmbActivatedByvalue,cmbCancelvalue);
        }

        /// <summary>
        /// To select all the companies
        /// </summary>
        /// <returns></returns>
        public static ISingleResult<rsp_GetCompanyDetailsResult> GetCompanyDetailsResult(int SecurityUserID)
        {
            LicensingDataContext licensingDataContext = new LicensingDataContext(EnterpriseConnectionString);
            return licensingDataContext.rsp_GetCompanyDetails(SecurityUserID);
        }

        /// <summary>
        /// To select all the subcompanies or specific subcompanied based on the selected company
        /// </summary>
        /// <param name="iCompanyId"></param>
        /// <returns></returns>
        public static ISingleResult<rsp_GetSubCompanyDetailsResult> GetSubCompanyDetailsResult(Int32 iCompanyId, int SecurityUSerID)
        {
            LicensingDataContext licensingDataContext = new LicensingDataContext(EnterpriseConnectionString);
            return licensingDataContext.rsp_GetSubCompanyDetails(iCompanyId, SecurityUSerID);
        }

        /// <summary>
        /// To select all the sites or specific sites based on the selected subcompany
        /// </summary>
        /// <param name="iSubCompanyId"></param>
        /// <returns></returns>
        public static ISingleResult<rsp_GetSiteInfoResult> GetSiteInfoResult(Int32 iSubCompanyId, Int32 iLoginUserID)
        {
            LicensingDataContext licensingDataContext = new LicensingDataContext(EnterpriseConnectionString);
            return licensingDataContext.rsp_GetSiteInfo(iSubCompanyId, iLoginUserID);
        }


        public static ISingleResult<rsp_SL_GetValidSiteInfoResult> GetSiteInfoResultHistory(Int32 iSubCompanyId, Int32 iLoginUserID)
        {
            LicensingDataContext licensingDataContext = new LicensingDataContext(EnterpriseConnectionString);
            return licensingDataContext.rsp_SL_GetValidSiteInfo(iSubCompanyId, iLoginUserID);
        }

        /// <summary>
        /// To select all the License key status
        /// </summary>
        /// <returns></returns>
        public static ISingleResult<rsp_SL_GetKeyStatusResult> GetSiteLicenseKeyStatus()
        {
            LicensingDataContext licensingDataContext = new LicensingDataContext(EnterpriseConnectionString);
            return licensingDataContext.rsp_SL_GetKeyStatus();
        }

        /// <summary>
        /// To select all the sites or specific sites based on the selected subcompany
        /// </summary>
        /// <param name="iSubCompanyId"></param>
        /// <returns></returns>
        public static ISingleResult<rsp_SL_GetSitesResult> GetSiteInfoResult(Int32 iCompanyId, Int32 iSubCompanyId, Int32 iLoginUserID)
        {
            LicensingDataContext licensingDataContext = new LicensingDataContext(EnterpriseConnectionString);
            return licensingDataContext.GetSitesByCompanyID(iCompanyId, iSubCompanyId, iLoginUserID);
        }

        public static string GetCultureInfo()
        {
            LicensingDataContext licensingDataContext = new LicensingDataContext(EnterpriseConnectionString);
            rsp_GetSystemSettingsResult result = licensingDataContext.GetSystemSettings().FirstOrDefault();
            if (result == null)
                return null;
            return result.RegionCulture;
        }

        #endregion License Search Methods

        #endregion //Static Methods
    }
}
