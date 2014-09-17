using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Data;
using System.Data.Linq.Mapping;
using System.Reflection;
using System.Text;
using BMC.SiteLicensing.DataAccess;
using Audit.BusinessClasses;
using Audit.Transport;
using System.Windows.Forms;
using BMC.Common.LogManagement;
using System.Security.Cryptography;
using BMC.Common.ExceptionManagement;
using BMC.Common;
using System.Text.RegularExpressions;

namespace BMC.SiteLicensing.Business
{
    public static class BusinessLogic
    {
        #region DataMember

        private static Dictionary<Int32, string> slLicenseKeyStatus = null;

        private static string PASSWORD_CHARS_LCASE = "abcdefghjkmnopqrstwxyz";
        private static string PASSWORD_CHARS_UCASE = "ABCDEFGHJKLMNPQRSTWXYZ";
        private static string PASSWORD_CHARS_NUMERIC = "234567890";

        public static int iLoginStaffID = 0;
        public static int iLoginSeqID = 0;
        public static string sLoginUserName = string.Empty;
        
        #endregion //DataMember

        #region Static Constructor

        static BusinessLogic()
        {
            slLicenseKeyStatus = DataManipulator.GetSiteLicenseKeyStatus().ToDictionary(keyStatusResult => keyStatusResult.KeyStatusID, keyStatusResult => keyStatusResult.KeyText);
        }

        #endregion //Static Constructor

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

        #region Business Methods

        #region Common Methods

        /// <summary>
        /// To get list of Company/Subcompany/Site/KeyStatus/Rule values
        /// </summary>
        /// <param name="sListName"></param>
        /// <param name="iID"></param>
        /// <param name="sDefaultItem"></param>
        /// <returns></returns>
        public static Dictionary<Int32, string> GetList(string sListName, Int32 iID, string sDefaultItem)
        {
            Dictionary<int, string> comboValueDictionary = new Dictionary<int, string>();
            try
            {
                if (!String.IsNullOrEmpty(sDefaultItem.Trim()))
                {
                    comboValueDictionary.Add(0, sDefaultItem);
                }
                switch (sListName)
                {
                    case "Company":
                        IEnumerable<rsp_GetCompanyDetailsResult> companyDetailsResults = DataManipulator.GetCompanyDetailsResult(iLoginSeqID);
                        comboValueDictionary = comboValueDictionary.Concat(companyDetailsResults.ToDictionary(companyDetailsResult => companyDetailsResult.Company_ID, companyDetailsResult => companyDetailsResult.Company_Name)).ToDictionary(e => e.Key, e => e.Value);
                        break;
                    case "SubCompany":
                        IEnumerable<rsp_GetSubCompanyDetailsResult> subCompanyDetailsResults = DataManipulator.GetSubCompanyDetailsResult(iID, iLoginSeqID);
                        comboValueDictionary = comboValueDictionary.Concat(subCompanyDetailsResults.ToDictionary(subCompanyDetailsResult => subCompanyDetailsResult.Sub_Company_ID, subCompanyDetailsResult => subCompanyDetailsResult.Sub_Company_Name)).ToDictionary(e => e.Key, e => e.Value);
                        break;
                    case "Site":
                         IEnumerable<rsp_SL_GetValidSiteInfoResult> siteInfoResultsHistory = DataManipulator.GetSiteInfoResultHistory(iID, iLoginStaffID);
                         comboValueDictionary = comboValueDictionary.Concat(siteInfoResultsHistory.ToDictionary(siteInfoResult => siteInfoResult.Site_ID, siteInfoResult => siteInfoResult.Site_Name)).ToDictionary(e => e.Key, e => e.Value);
                        break;                      
                    case "KeyStatus":
                        IEnumerable<rsp_SL_GetKeyStatusResult> keyStatusResults = DataManipulator.GetSiteLicenseKeyStatus();
                        keyStatusResults = keyStatusResults.Select(x => new rsp_SL_GetKeyStatusResult { KeyStatusID = x.KeyStatusID, KeyText = ResourceExtensions.GetResourceTextByKey(new Control(), "Key_SiteLicensing_KeyStatus_" + new Regex("[;\\/:*?\"<>|&'_ ]").Replace(x.KeyText, string.Empty)) });
                        comboValueDictionary = comboValueDictionary.Concat(keyStatusResults.OrderBy(X => X.KeyText).ToDictionary(keyStatusResult => keyStatusResult.KeyStatusID, keyStatusResult => keyStatusResult.KeyText)).ToDictionary(e => e.Key, e => e.Value);
                        break;
                    case "Rule":
                        IEnumerable<rsp_SL_GetRuleDetailsResult> ruleDetailsResults = DataManipulator.GetRuleDetails();
                        comboValueDictionary = comboValueDictionary.Concat(ruleDetailsResults.ToDictionary(ruleInfoResult => ruleInfoResult.RuleID, ruleInfoResult => ruleInfoResult.RuleName)).ToDictionary(e => e.Key, e => e.Value);
                        break;
                    case "user":
                        IEnumerable<rsp_SL_GetuserName> UserResults = DataManipulator.GetUSerDetails();
                        comboValueDictionary = comboValueDictionary.Concat(UserResults.ToDictionary(UserNameResults => UserNameResults.Staff_ID, UserNameResults => UserNameResults.Staff_Name)).ToDictionary(e => e.Key, e => e.Value);
                         break;
                    case "SiteSC":
                          IEnumerable<rsp_GetSiteInfoResult> siteInfoResults = DataManipulator.GetSiteInfoResult(iID, iLoginStaffID);
                        comboValueDictionary = comboValueDictionary.Concat(siteInfoResults.ToDictionary(siteInfoResult => siteInfoResult.Site_ID, siteInfoResult => siteInfoResult.Site_Name)).ToDictionary(e => e.Key, e => e.Value);
                        break;

                }
                return comboValueDictionary;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return comboValueDictionary;
            }
        }

        public static Dictionary<Int32, string> GetSiteList(string sListName, Int32 companyID, Int32 subCompanyID, string sDefaultItem)
        {
            Dictionary<int, string> comboValueDictionary = new Dictionary<int, string>();
            try
            {
                if (!String.IsNullOrEmpty(sDefaultItem.Trim()))
                {
                    comboValueDictionary.Add(0, sDefaultItem);
                }
                IEnumerable<rsp_SL_GetSitesResult> siteInfoResults = DataManipulator.GetSiteInfoResult(companyID, subCompanyID, iLoginStaffID);
                comboValueDictionary = comboValueDictionary.Concat(siteInfoResults.ToDictionary(siteInfoResult => siteInfoResult.Site_ID, siteInfoResult => siteInfoResult.Site_Name)).ToDictionary(e => e.Key, e => e.Value);
                return comboValueDictionary;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return comboValueDictionary;
            }
        }

        /// <summary>
        /// To convert input value into Integer type
        /// </summary>
        /// <param name="ObjValue"></param>
        /// <returns></returns>
        public static Int32 ToInteger(object ObjValue)
        {
            try
            {
                int objValue1;
                if (Int32.TryParse(Convert.ToString(ObjValue), out objValue1))
                    return objValue1;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return 0;
        }

        /// <summary>
        /// To keep track the events/operation in the audit table for auditing purpose
        /// </summary>
        /// <param name="sAuditDesc"></param>
        /// <param name="sField"></param>
        /// <param name="sPrevValue"></param>
        /// <param name="sNewValue"></param>
        public static void AuditLicense(string sScreenName, string sAuditFunc, string sAuditDesc, string sField, string sPrevValue, string sNewValue)
        {
            try
            {
                //Calling Audit Method
                Audit_History AH = new Audit_History();
                //Populate required Values
                AH.EnterpriseModuleName = ModuleNameEnterprise.SiteLicensing;
                AH.Audit_Screen_Name = sScreenName;
                AH.Audit_Desc = sAuditFunc + ": " + sAuditDesc + " .. [" + sField + "] : " + sPrevValue + " -> " + sNewValue;
                AH.AuditOperationType = OperationType.MODIFY;
                AH.Audit_Field = sField;
                AH.Audit_User_ID = iLoginSeqID;
                AH.Audit_User_Name = sLoginUserName;
                AH.Audit_New_Vl = sNewValue; //current value
                AH.Audit_Old_Vl = sPrevValue;  // previous value
                AuditViewerBusiness AVB = new AuditViewerBusiness(Common.Utilities.DatabaseHelper.GetConnectionString());
                AVB.InsertAuditData(AH, true);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// To get the License Key Status Text besed on the KeyStatus Id
        /// </summary>
        /// <param name="iKeyStatus"></param>
        /// <returns></returns>
        public static string GetLicenseKeyStatusByKeyId(Int32 iKeyStatus)
        {
            try
            {
                string sValue = string.Empty;
                if (slLicenseKeyStatus.TryGetValue(iKeyStatus, out sValue)) return sValue;
                return string.Empty;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return string.Empty;
            }
        }


        /// <summary>
        /// Method to add log info in audit table for new row fields
        /// </summary>
        public static void AuditNewEntry(string sScreenName, string sAuditFunc, string sAuditDesc, string sField, string sNewValue)
        {
            try
            {
            //Calling Audit Method
            Audit_History AH = new Audit_History();
            //Populate required Values            
            AH.EnterpriseModuleName = ModuleNameEnterprise.SiteLicensing;
            AH.Audit_Screen_Name = sScreenName;
            AH.Audit_Desc = sAuditFunc + ": " + sAuditDesc +  ": " + sNewValue;
            AH.AuditOperationType = OperationType.ADD;
            AH.Audit_Field = sField;
            AH.Audit_New_Vl = sNewValue;

            AH.Audit_User_ID = iLoginSeqID;
            AH.Audit_User_Name = sLoginUserName;

            AuditViewerBusiness AVB = new AuditViewerBusiness(Common.Utilities.DatabaseHelper.GetConnectionString());
            AVB.InsertAuditData(AH,true);

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        public static string GetCultureInfo()
        {
            return DataManipulator.GetCultureInfo();
        }

        #endregion //Common Methods

        #region Cancel License

        /// <summary>
        /// To update the particular license status as Cancelled
        /// </summary>
        /// <param name="licenseDetailsResultList"></param>
        /// <param name="iSelectedIndex"></param>
        /// <returns></returns>
        public static bool CancelLicense(List<rsp_SL_GetLicenseDetailsResult> licenseDetailsResultList, Int32 iSelectedIndex)
        {
            try
            {
                int result = -1;
                LogManager.WriteLog("Updating the License details for the License: " + licenseDetailsResultList[iSelectedIndex].LicenseInfoID.ToString() + " from the status " 
                                        + licenseDetailsResultList[iSelectedIndex].KeyText + " to " + LicenseKeyStatus.Cancelled.ToString(), LogManager.enumLogLevel.Info);
                AuditViewerBusiness.CreateInstance(DataManipulator.GetConnectionString());
                AuditLicense("View/Cancel License", "Cancel license", "Site Code: [" + licenseDetailsResultList[iSelectedIndex].SiteCode + "], License Key: " + licenseDetailsResultList[iSelectedIndex].Licensekey, "KeyStatus", licenseDetailsResultList[iSelectedIndex].KeyText, LicenseKeyStatus.Cancelled.ToString());
                result = DataManipulator.UpdateLicenseStaus(licenseDetailsResultList[iSelectedIndex].LicenseInfoID, (int)(LicenseKeyStatus.Cancelled), iLoginStaffID);

                if (result == 0) return true;
                return false;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return false;
            }
        }

        #endregion Cancel License

        #region Activate License

        /// <summary>
        /// To update the particular license status as Activated
        /// </summary>
        /// <param name="licenseDetailsResultList"></param>
        /// <param name="iSelectedIndex"></param>
        /// <returns></returns>
        /// 

        public static int ActivateLicense(List<rsp_SL_GetLicenseDetailsResult> licenseDetailsResultList, Int32 iSelectedIndex)
        {
            int result = 0;
            try
            {
                
                LogManager.WriteLog("Updating the License details for the License: " + licenseDetailsResultList[iSelectedIndex].LicenseInfoID.ToString() + " from the status "
                                        + licenseDetailsResultList[iSelectedIndex].KeyText + " to " + LicenseKeyStatus.Active.ToString(), LogManager.enumLogLevel.Info);
                AuditViewerBusiness.CreateInstance(DataManipulator.GetConnectionString());
                AuditLicense("Activate License", "Activate license", "Site Code: [" + licenseDetailsResultList[iSelectedIndex].SiteCode + "], License Key: " + licenseDetailsResultList[iSelectedIndex].Licensekey, "KeyStatus", licenseDetailsResultList[iSelectedIndex].KeyText, LicenseKeyStatus.Active.ToString());
                result = DataManipulator.ActivateLicenseStaus(licenseDetailsResultList[iSelectedIndex].LicenseInfoID, (int)(LicenseKeyStatus.Active), iLoginStaffID);

                //if (result == 0) return true;

                return result; 
            }
                
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return 0;
            }

        }

        #endregion

        #region Rule Information Methods

        /// <summary>
        /// Returns array of rule names stored in the database
        /// </summary>
        /// <returns></returns>
        public static String[] GetAllRuleNames()
        {
            try
            {
                return DataManipulator.GetAllRuleNames();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return null;
        }

        /// <summary>
        /// Fetch the rule information for a perticular rule name
        /// </summary>
        /// <param name="ruleName"></param>
        /// <returns></returns>
        public static SL_Rule GetRuleInformation(String ruleName)
        {
            try
            {
                return DataManipulator.GetRuleInformation(ruleName);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return null;
        }

        /// <summary>
        /// Get the rulID for a perticular rule Name
        /// </summary>
        /// <param name="ruleName"></param>
        /// <returns></returns>
        public static int GetRuleID(String ruleName)
        {
            try
            {
                return DataManipulator.GetRuleID(ruleName);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return -1;
        }

        /// <summary>
        /// Get the similar rule names for the rule information
        /// </summary>
        /// <param name="validationRequired"></param>
        /// <param name="lockSite"></param>
        /// <param name="disableGames"></param>
        /// <param name="warningOnly"></param>
        /// <param name="alertRequired"></param>
        /// <returns></returns>
        public static String[] GetSimilarRules(bool validationRequired, bool lockSite, bool disableGames, bool warningOnly, bool alertRequired)
        {
            try
            {
                return DataManipulator.GetSimilarRules(validationRequired, lockSite, disableGames, warningOnly, alertRequired);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return null;

        }

        /// <summary>
        /// Finding rule is associated with any License
        /// </summary>
        /// <param name="ruleName"></param>
        /// <returns></returns>
        public static bool IsRuleAssociated(String ruleName)
        {
            try
            {
                return DataManipulator.IsRuleAssociated(ruleName);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return false;
        }

        /// <summary>
        /// Create or update the rule information in database
        /// </summary>
        /// <param name="ruleName"></param>
        /// <param name="validationRequired"></param>
        /// <param name="lockSite"></param>
        /// <param name="disableGames"></param>
        /// <param name="warningOnly"></param>
        /// <param name="alertRequired"></param>
        /// <returns></returns>
        public static int InsertOrUpdateRule(string ruleName, bool validationRequired, bool lockSite, bool disableGames, bool warningOnly,
                                               bool alertRequired)
        {
            try
            {
                int iResult = 1;
                AuditRuleInformation(ruleName, validationRequired, lockSite, disableGames, warningOnly, alertRequired);
                iResult = DataManipulator.InsertorUpdateRule(ruleName, validationRequired, lockSite, disableGames, warningOnly, alertRequired, iLoginStaffID);
                return iResult;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return 1;
        }

        /// <summary>
        /// Verify for changes / Insert and make entry in Audit table
        /// </summary>
        /// <param name="sRuleName"></param>
        /// <param name="bValidationRequired"></param>
        /// <param name="bLockSite"></param>
        /// <param name="bDisableGames"></param>
        /// <param name="bWarningOnly"></param>
        /// <param name="bAlertRequired"></param>
        public static void AuditRuleInformation(string sRuleName, bool bValidationRequired, bool bLockSite, bool bDisableGames, bool bWarningOnly,
                                               bool bAlertRequired)
        {
            try
            {
            SL_Rule objSL_Rule = GetRuleInformation(sRuleName);
            if (objSL_Rule != null)
            {
                if (objSL_Rule.ValidationRequired != bValidationRequired)
                {
                    AuditLicense("Rule Information", "Edit Rule Information", "Validation Required", "ValidationRequired", objSL_Rule.ValidationRequired.ToString(), bValidationRequired.ToString());
                }
                if (objSL_Rule.LockSite != bLockSite)
                {
                    AuditLicense("Rule Information", "Edit Rule Information", "Lock Site", "LockSite", objSL_Rule.LockSite.ToString(), bLockSite.ToString());
                }
                if (objSL_Rule.DisableGames != bDisableGames)
                {
                    AuditLicense("Rule Information", "Edit Rule Information", "Disable Games", "DisableGames", objSL_Rule.DisableGames.ToString(), bDisableGames.ToString());
                }
                if (objSL_Rule.WarningOnly != bWarningOnly)
                {
                    AuditLicense("Rule Information", "Edit Rule Information", "Warning Only", "WarningOnly", objSL_Rule.WarningOnly.ToString(), bWarningOnly.ToString());
                }
                if (objSL_Rule.AlertRequired != bAlertRequired)
                {
                    AuditLicense("Rule Information", "Edit Rule Information", "Alert Required", "AlertRequired", objSL_Rule.AlertRequired.ToString(), bAlertRequired.ToString());
                }
            }
            else
            {
                AuditNewEntry("Rule Information", "Add Rule Information", "Rule Name", "RuleName", sRuleName.ToString());
                AuditNewEntry("Rule Information", "Add Rule Information", "Validation Required", "ValidationRequired", bValidationRequired.ToString());
                AuditNewEntry("Rule Information", "Add Rule Information", "Lock Site", "LockSite", bLockSite.ToString());
                AuditNewEntry("Rule Information", "Add Rule Information", "Disable Games", "DisableGames", bDisableGames.ToString());
                AuditNewEntry("Rule Information", "Add Rule Information", "Warning Only", "WarningOnly", bWarningOnly.ToString());
                AuditNewEntry("Rule Information", "Add Rule Information", "Alert Required", "AlertRequired", bAlertRequired.ToString());

            }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        #endregion Rule Information Methods

        #region Key Generation Methods


        /// <summary>
        /// Validate Dates with existing License dates
        /// </summary>
        /// <param name="siteID"></param>
        /// <param name="dtStartDate"></param>
        /// <param name="dtExpiryDate"></param>
        /// <returns></returns>
        public static string ValidateDates(int siteID, DateTime dtStartDate, DateTime dtExpiryDate)
        {
            try
            {
                int iResult = DataManipulator.ValidateLicenseDates(siteID, dtStartDate, dtExpiryDate);
                if (iResult == 0)
                    return "success";
                else if (iResult == 1)
                    return "A License already exists for the Site in the selected time period.\r\nPlease change the time period.";
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return "Unknown Error";
        }

        /// <summary>
        /// Generates a random String.
        /// </summary>
        /// <param name="minLength">
        /// Minimum String length.
        /// </param>
        /// <param name="maxLength">
        /// Maximum String length.
        /// </param>
        /// <returns>
        /// Randomly generated String.
        /// </returns>
        /// <remarks>
        /// The length of the generated String will be determined at
        /// random and it will fall with the range determined by the
        /// function parameters.
        /// </remarks>
        public static string Generate(int minLength,
                                      int maxLength)
        {
            // Make sure that input parameters are valid.
            if (minLength <= 0 || maxLength <= 0 || minLength > maxLength)
                return null;

            // Create a local array containing supported password characters
            // grouped by types. You can remove character groups from this
            // array, but doing so will weaken the password strength.
            char[][] charGroups = new char[][] 
        {
            PASSWORD_CHARS_LCASE.ToCharArray(),
            PASSWORD_CHARS_UCASE.ToCharArray(),
            PASSWORD_CHARS_NUMERIC.ToCharArray()
        };

            // Use this array to track the number of unused characters in each
            // character group.
            int[] charsLeftInGroup = new int[charGroups.Length];

            // Initially, all characters in each group are not used.
            for (int i = 0; i < charsLeftInGroup.Length; i++)
                charsLeftInGroup[i] = charGroups[i].Length;

            // Use this array to track (iterate through) unused character groups.
            int[] leftGroupsOrder = new int[charGroups.Length];

            // Initially, all character groups are not used.
            for (int i = 0; i < leftGroupsOrder.Length; i++)
                leftGroupsOrder[i] = i;

            // Because we cannot use the default randomizer, which is based on the
            // current time (it will produce the same "random" number within a
            // second), we will use a random number generator to seed the
            // randomizer.

            // Use a 4-byte array to fill it with random bytes and convert it then
            // to an integer value.
            byte[] randomBytes = new byte[4];

            // Generate 4 random bytes.
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            rng.GetBytes(randomBytes);

            // Convert 4 bytes into a 32-bit integer value.
            int seed = (randomBytes[0] & 0x7f) << 24 |
                        randomBytes[1] << 16 |
                        randomBytes[2] << 8 |
                        randomBytes[3];

            // Now, this is real randomization.
            Random random = new Random(seed);

            // This array will hold password characters.
            char[] password = null;

            // Allocate appropriate memory for the password.
            if (minLength < maxLength)
                password = new char[random.Next(minLength, maxLength + 1)];
            else
                password = new char[minLength];

            // Index of the next character to be added to password.
            int nextCharIdx;

            // Index of the next character group to be processed.
            int nextGroupIdx;

            // Index which will be used to track not processed character groups.
            int nextLeftGroupsOrderIdx;

            // Index of the last non-processed character in a group.
            int lastCharIdx;

            // Index of the last non-processed group.
            int lastLeftGroupsOrderIdx = leftGroupsOrder.Length - 1;

            // Generate password characters one at a time.
            for (int i = 0; i < password.Length; i++)
            {
                // If only one character group remained unprocessed, process it;
                // otherwise, pick a random character group from the unprocessed
                // group list. To allow a special character to appear in the
                // first position, increment the second parameter of the Next
                // function call by one, i.e. lastLeftGroupsOrderIdx + 1.
                if (lastLeftGroupsOrderIdx == 0)
                    nextLeftGroupsOrderIdx = 0;
                else
                    nextLeftGroupsOrderIdx = random.Next(0,
                                                         lastLeftGroupsOrderIdx);

                // Get the actual index of the character group, from which we will
                // pick the next character.
                nextGroupIdx = leftGroupsOrder[nextLeftGroupsOrderIdx];

                // Get the index of the last unprocessed characters in this group.
                lastCharIdx = charsLeftInGroup[nextGroupIdx] - 1;

                // If only one unprocessed character is left, pick it; otherwise,
                // get a random character from the unused character list.
                if (lastCharIdx == 0)
                    nextCharIdx = 0;
                else
                    nextCharIdx = random.Next(0, lastCharIdx + 1);

                // Add this character to the password.
                password[i] = charGroups[nextGroupIdx][nextCharIdx];

                // If we processed the last character in this group, start over.
                if (lastCharIdx == 0)
                    charsLeftInGroup[nextGroupIdx] =
                                              charGroups[nextGroupIdx].Length;
                // There are more unprocessed characters left.
                else
                {
                    // Swap processed character with the last unprocessed character
                    // so that we don't pick it until we process all characters in
                    // this group.
                    if (lastCharIdx != nextCharIdx)
                    {
                        char temp = charGroups[nextGroupIdx][lastCharIdx];
                        charGroups[nextGroupIdx][lastCharIdx] =
                                    charGroups[nextGroupIdx][nextCharIdx];
                        charGroups[nextGroupIdx][nextCharIdx] = temp;
                    }
                    // Decrement the number of unprocessed characters in
                    // this group.
                    charsLeftInGroup[nextGroupIdx]--;
                }

                // If we processed the last group, start all over.
                if (lastLeftGroupsOrderIdx == 0)
                    lastLeftGroupsOrderIdx = leftGroupsOrder.Length - 1;
                // There are more unprocessed groups left.
                else
                {
                    // Swap processed group with the last unprocessed group
                    // so that we don't pick it until we process all groups.
                    if (lastLeftGroupsOrderIdx != nextLeftGroupsOrderIdx)
                    {
                        int temp = leftGroupsOrder[lastLeftGroupsOrderIdx];
                        leftGroupsOrder[lastLeftGroupsOrderIdx] =
                                    leftGroupsOrder[nextLeftGroupsOrderIdx];
                        leftGroupsOrder[nextLeftGroupsOrderIdx] = temp;
                    }
                    // Decrement the number of unprocessed groups.
                    lastLeftGroupsOrderIdx--;
                }
            }

            // Convert password characters into a string and return the result.
            return new string(password);
        }

        /// <summary>
        /// Insert license info 
        /// </summary>
        /// <param name="dtStartDate"></param>
        /// <param name="dtExpiryDate"></param>
        /// <param name="iRuleID"></param>
        /// <param name="iSiteID"></param>
        /// <param name="strLicenseKey"></param>
        /// <param name="iAlterBeforeDays"></param>
        /// <param name="iKeyStatusID"></param>
        /// <returns></returns>
        public static String InsertLicenseInfo(DateTime dtStartDate, DateTime dtExpiryDate, KeyValuePair<int, String> kVRuleInfo, KeyValuePair<int, String> kVSiteInfo, String strLicenseKey, short iAlterBeforeDays, int iKeyStatusID)
        {
            try
            {
                AuditNewEntry("Key Generation", "Add License Info", "Start Date", "StartDate", dtStartDate.ToShortDateString());
                AuditNewEntry("Key Generation", "Add License Info", "Expiry Date", "ExpiryDate", dtExpiryDate.ToShortDateString());
                AuditNewEntry("Key Generation", "Add License Info", "Rule Name", "RuleName", kVRuleInfo.Value);
                AuditNewEntry("Key Generation", "Add License Info", "Site Name", "SiteName", kVSiteInfo.Value);
                AuditNewEntry("Key Generation", "Add License Info", "License Key", "LicenseKey", strLicenseKey);
                if(iAlterBeforeDays > 0)
                    AuditNewEntry("Key Generation", "Add License Info", "Alert Before Days", "AlertBeforeDays", iAlterBeforeDays.ToString());
                AuditNewEntry("Key Generation", "Add License Info", "Key Status", "KeyStatus", LicenseKeyStatus.Created.ToString());
                int iResult = DataManipulator.InsertLicenseInfo(dtStartDate, dtExpiryDate, kVRuleInfo.Key, kVSiteInfo.Key, strLicenseKey, iAlterBeforeDays, iKeyStatusID, iLoginStaffID);
                if (iResult == 0)
                    return "success";
            }
            catch(Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return "Unknown Error";
        }

        /// <summary>
        /// Fetch Assiciated Site for the rule
        /// </summary>
        /// <param name="ruleName"></param>
        /// <returns></returns>
        public static List<String> GetAssociatedSites(Int32 iRuleID)
        {
            try
            {
                return DataManipulator.GetAssociatedSites(iRuleID, iLoginStaffID);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return null;
        }

        #endregion Key Generation Methods

        #endregion //Business Methods
    }
}
