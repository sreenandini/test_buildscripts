using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.SiteLicensing.DataAccess;
using System.Text.RegularExpressions;
using BMC.SiteLicensing.Business;
using BMC.Common.ExceptionManagement;
using BMC.Common.LogManagement;
using BMC.Common.Security;
using System.Diagnostics;
using System.Threading;
using System.Globalization;
using BMC.Common.Utilities;
using BMC.Common.ConfigurationManagement;
using BMC.Common;
using System.Data.Linq;
using BMC.CoreLib.Win32;
using System.IO;

namespace BMCSiteLicensing
{
    public partial class frmSiteLicensing : Form
    {
        #region Data Members

        private Int32 iSiteId = -1;
        private int ilicenseDetailsSelectedIndex = -1;
        private List<rsp_SL_GetLicenseDetailsResult> licenseDetailsResultList = null;
        private List<rsp_SL_GetLicenseHistoryDetailsResult> licenseHistorySearchResultList = null;
        private List<rsp_GetSiteLicensingRightsResult> siteLicensingRightsResultList = null;
        private Form frmNew = null;
        private TextBox txtNewRuleName = null;
        private int iPreviousIndex = -1;
        private int iCurrentIndex = -1;
        private bool isUpdated = true;
        private bool bRulePermission = true;
        private bool ShowActivateLicense = false;
        DateTimeFormatInfo currentDateCultureFormater;
        Int32 LicenseDefaultDay = 7;
        ToolTip ToolTip1 = null;
        private TextBox textBox;
        private decimal _oldValue;
        private string _dateFormater = string.Empty;
        private string allText, allCapsText = string.Empty;
        #endregion //Data Members

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public frmSiteLicensing()
        {
            InitializeComponent();
            LoadUserDetails();

            // Set Tags for controls
            SetTagProperty();            
        }

        /// <summary>
        /// Parameterised Constructor
        /// </summary>
        /// <param name="iLoginID"></param>
        /// <param name="sLoginUname"></param>
        public frmSiteLicensing(string sLoginUname, string iLoginID, string iLoginSeqID)
        {
            try
            {
                InitializeComponent();

                BusinessLogic.iLoginStaffID = Convert.ToInt32(CryptographyHelper.Decrypt(iLoginID)); 
                BusinessLogic.sLoginUserName = CryptographyHelper.Decrypt(sLoginUname);
                BusinessLogic.iLoginSeqID = Convert.ToInt32(CryptographyHelper.Decrypt(iLoginSeqID)); 

                ExtensionMethods.CurrentDateCulture = BusinessLogic.GetCultureInfo();
                if (ExtensionMethods.CurrentDateCulture == null)
                    ExtensionMethods.CurrentDateCulture = ConfigManager.Read("GetDefaultCultureForDate");
                currentDateCultureFormater = new CultureInfo(ExtensionMethods.CurrentDateCulture).DateTimeFormat;
                //License Default day will be read from Config File
                //If the given number is not an integer or negative value means it will set the default 7 days as License Default day
                LicenseDefaultDay = (Int32.TryParse(ConfigManager.Read("LicenseDefaultDay"), out LicenseDefaultDay))
                    ?
                    (LicenseDefaultDay < 0) ? 7 : LicenseDefaultDay : 7;
                ToolTip1 = new ToolTip();
                textBox = (TextBox)numUDAlertBefor.Controls[1];
                textBox.TextChanged += TextBoxOnTextChanged;

                // Set Tags for controls
                SetTagProperty();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        #endregion //Constructor

        #region Events

        #region Common Events

        /// <summary>
        /// Loading the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmSiteLicensing_Load(object sender, EventArgs e)
        {
            // For externalization
            this.ResolveResources();

            // Get "All" & "ALL" Texts from Resource
            allText = this.GetResourceTextByKey("Key_All");
            allCapsText = this.GetResourceTextByKey("Key_ALLCaps");

            LoadPage();
            LoadUserDetails();

            // For ToolStripMenuItem
            addNewToolStripMenuItem.Text = this.GetResourceTextByKey(addNewToolStripMenuItem.Tag.ToString());
            editToolStripMenuItem.Text = this.GetResourceTextByKey(editToolStripMenuItem.Tag.ToString());
            activateLicenseToolStripMenuItem.Text = this.GetResourceTextByKey(activateLicenseToolStripMenuItem.Tag.ToString());
            cancelLicenseToolStripMenuItem.Text = this.GetResourceTextByKey(cancelLicenseToolStripMenuItem.Tag.ToString());
            exportToolStripMenuItem.Text = this.GetResourceTextByKey(exportToolStripMenuItem.Tag.ToString());
        }

        private void SetTagProperty()
        {
            this.btnActivateLicense.Tag = "Key_ActivateLicenseCaption";
            this.btnClose.Tag = "Key_CloseCaption";
            this.btnUpdate.Tag = "Key_EditCaption";
            this.btnGenerate.Tag = "Key_GenerateCaption";
            this.btn_Refresh.Tag = "Key_Refresh";
            this.btnSLUpdate.Tag = "Key_UpdateCaption";
            this.lblinDays.Tag = "Key_indayscolon";
            this.lblCompName.Tag = "Key_CompanyNameMandatory";
            this.lblExpiryDate.Tag = "Key_ExpiryDateMandatory";
            this.lblLicenseKey.Tag = "Key_LicenseKeyMandatory";
            this.lblLSRuleName.Tag = "Key_RuleNameMandatory";
            this.lblSiteName.Tag = "Key_SiteNameMandatory";
            this.lblStartDate.Tag = "Key_StartDateMandatory";
            this.lblSubCompName.Tag = "Key_SubCompanyNameMandatory";
            this.lblActivatedBy.Tag = "Key_ActivatedByColon";
            this.btnAddNew.Tag = "Key_AddNewCaption";
            this.lblAlertBefor.Tag = "Key_AlertBeforeColon";
            this.chkRSAlertreq.Tag = "Key_AlertRequired";
            this.chkLSAlertReq.Tag = "Key_AlertRequired";
            this.lblAlertRequired.Tag = "Key_AlertRequiredColon";           
            this.button1.Tag = "Key_ClearCaption";
            this.btnLHClear.Tag = "Key_ClearCaption";
            this.btnCancelLicense.Tag = "Key_CancelLicense";
            this.lblCancelBy.Tag = "Key_CancelledByColon";
            this.lblSCCompName.Tag = "Key_CompanyNameColon";
            this.lblcreate.Tag = "Key_CreatedByColon";
            this.chkRSDisableEGMs.Tag = "Key_DisableEGMs";
            this.chkLSDisableEGMs.Tag = "Key_DisableEGMs";
            this.lblSCDisableEGM.Tag = "Key_DisableEGMsColon";
            this.lblFromExpiryDate.Tag = "Key_FromLicenseExpiredDate";
            this.lblfromStartDate.Tag = "Key_FromLicenseStartDate";
            this.grpgenLicense.Tag = "Key_GenerateLicense";
            this.tabPgKeyGeneration.Tag = "Key_KeyGeneration";
            this.lblSCKeyStatus.Tag = "Key_KeyStatusColon";
            this.grpLicenseDetails.Tag = "Key_LicenseDetails";
            this.tabPgLicenseGen.Tag = "Key_LicenseGeneration";
            this.tabPgLicenseHistory.Tag = "Key_LicenseHistory";
            this.grpLicenseSettings.Tag = "Key_LicenseSettings";
            this.chkRSLockSite.Tag = "Key_LockSite";
            this.chkLSLockSite.Tag = "Key_LockSite";
            this.lblSCLockSite.Tag = "Key_LockSiteColon";
            this.groupBox2.Tag = "Key_RuleAssociatedSites";
            this.tabPgRuleINfo.Tag = "Key_RuleInformation";
            this.lblRSRuleName.Tag = "Key_RuleNameColon";
            this.grpRuleNames.Tag = "Key_RuleNames";
            this.grpRuleSettings.Tag = "Key_RuleSettings";
            this.btnSearch.Tag = "Key_SearchCaption";
            this.grpSearchCriteria.Tag = "Key_SearchCriteria";
            this.grpSearchResults.Tag = "Key_SearchResults";
            this.grpSiteList.Tag = "Key_SiteList";
            this.lblSCSiteName.Tag = "Key_SiteNameColon";
            this.grpSiteSelection.Tag = "Key_SiteSelection";
            this.lblSCSubCompName.Tag = "Key_SubCompanyNameColon";
            this.lblToExpiryDate.Tag = "Key_ToLicenseExpiredDate";
            this.lblToStartDate.Tag = "Key_ToLicenseStartDate";
            this.grpUserFilter.Tag = "Key_UserFilter";
            this.grpValidationParam.Tag = "Key_ValidationParameters";
            this.grpValidationParameter.Tag = "Key_ValidationParameters";
            this.grpSCValidationParam.Tag = "Key_ValidationParameters";
            this.chkRSValidationReq.Tag = "Key_ValidationRequired";
            this.chkLSValidationReq.Tag = "Key_ValidationRequired";
            this.lblSCValidationReq.Tag = "Key_ValidationRequiredColon";
            this.tabPgViewCancelLicense.Tag = "Key_ViewCancelActivateLicense";
            this.chkRSWarningOnly.Tag = "Key_WarningOnly";
            this.chkLSWarningOnly.Tag = "Key_WarningOnly";
            this.lblSCWarningOnly.Tag = "Key_WarningOnlyColon";
            this.addNewToolStripMenuItem.Tag = "Key_AddNewCaption";
            this.editToolStripMenuItem.Tag = "Key_EditCaption";
            this.activateLicenseToolStripMenuItem.Tag = "Key_ActivateLicenseCaption";
            this.cancelLicenseToolStripMenuItem.Tag = "Key_CancelLicense";
            this.exportToolStripMenuItem.Tag = "Key_Export";
        }

        private void LoadPage()
        {
            try
            {
                string trueKey = this.GetResourceTextByKey("Key_True");
                string falseKey = this.GetResourceTextByKey("Key_False");

                object[] items = new object[] { allCapsText, trueKey, falseKey };
                // Set ComboBox Items
                LoadComboBox(ref cmbSCValidationReq,items);
                LoadComboBox(ref cmbSCLockSite,items);
                LoadComboBox(ref cmbSCDisableEGM,items);
                LoadComboBox(ref cmbSCWarningOnly,items);
                LoadComboBox(ref cmbSCAlertRequired,items);
              

                CreateNodesinTreeView();
                OnPgLicenseHistoryEnter();
                ResetKeyGeneration();
                //HideButtons();
                //RuleInfo_Load();
                if (!CheckAuthentication())
                {
                    this.Close();
                    return;
                }
                HideButtons();
                RuleInfo_Load();
                LogManager.WriteLog("Loading the Site Licensing application", LogManager.enumLogLevel.Info);

                _dateFormater = currentDateCultureFormater.ShortDatePattern + " " + "HH:mm";
                dtpkStartDate.Format = DateTimePickerFormat.Custom;
                dtpkStartDate.CustomFormat = _dateFormater;
                dtpkExpiryDate.Format = DateTimePickerFormat.Custom;
                dtpkExpiryDate.CustomFormat = _dateFormater;
                dtpkFromExpiryDate.Format = DateTimePickerFormat.Custom;
                dtpkFromExpiryDate.CustomFormat = currentDateCultureFormater.ShortDatePattern;
                dtpkToExpiryDate.Format = DateTimePickerFormat.Custom;
                dtpkToExpiryDate.CustomFormat = currentDateCultureFormater.ShortDatePattern;
                dtpkFromStartDate.Format = DateTimePickerFormat.Custom;
                dtpkFromStartDate.CustomFormat = currentDateCultureFormater.ShortDatePattern;
                dtpkToStartDate.Format = DateTimePickerFormat.Custom;
                dtpkToStartDate.CustomFormat = currentDateCultureFormater.ShortDatePattern;
                //string s = DateTime.Now.GetUniversalDateTimeFormatWithoutSeconds();
                sC_ViewCancelLicense.Panel2MinSize = 250;
                sC_ViewCancelLicense.Panel1MinSize = 200;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void LoadComboBox(ref BmcComboBox comboBox,object[] items)
        {
            comboBox.Items.Clear();
            comboBox.Items.AddRange(items);
        }

        /// <summary>
        /// Close the Site Licensing window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                isUpdated = true;
                NotifyChanges(lstRuleNames.SelectedIndex);
                if (isUpdated)
                    this.Close();
                LogManager.WriteLog("Closed the Site Licensing application", LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// Rule Information loaded if the selected tab is "Rule Information"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabLicenseGen_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                HideButtons();
                if (((TabControl)sender).SelectedTab == tabPgKeyGeneration && cmbLSRuleName.Enabled)
                {
                    string temp = cmbLSRuleName.Text;
                    decimal temp1 = numUDAlertBefor.Value;
                    FillCombo(cmbLSRuleName, BusinessLogic.GetList("Rule", 0, this.GetResourceTextByKey("Key_SelectRule")), "RuleID", "RuleName");
                    cmbLSRuleName.Text = temp;
                    numUDAlertBefor.Value = (numUDAlertBefor.Maximum < temp1) ? numUDAlertBefor.Maximum : temp1;
                }
                //    if (BusinessLogic.GetAllRuleNames().Count() != lstRuleNames.Items.Count || !isUpdated)
                //    {
                //        ((TabControl)sender).SelectedIndex = 0;
                //    }
                //    else if (((TabControl)sender).SelectedIndex == 0)
                //    {
                //        iPreviousIndex = -1;
                //        //RuleInfo_Load();
                //    }
                //    else if (((TabControl)sender).SelectedIndex == 1)
                //    {
                //        //ResetKeyGeneration();
                //    }
                //    else
                //    {
                //        iPreviousIndex = -1;
                //    }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// Rule Information loaded if the selected tab is "Rule Information"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabSiteLicensing_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                HideButtons();
                if (((TabControl)sender).SelectedTab == tabPgLicenseHistory)
                {
                    dtpkFromStartDate.Text = dtpkFromExpiryDate.Value.ToShortDateString();
                    dtpkToStartDate.Text = dtpkToStartDate.Value.ToShortDateString();
                    dtpkFromExpiryDate.Text = dtpkFromExpiryDate.Value.ToShortDateString();
                    dtpkToExpiryDate.Text = dtpkToExpiryDate.Value.ToShortDateString();
                }

                //if (BusinessLogic.GetAllRuleNames().Count() != lstRuleNames.Items.Count || !isUpdated)
                //{
                //    ((TabControl)sender).SelectedIndex = 0;
                //}
                //else if (((TabControl)sender).SelectedIndex == 0 && this.tabLicenseGen.SelectedIndex == 0)
                //{
                //    iPreviousIndex = -1;
                //    //RuleInfo_Load();
                //}
                //else if (((TabControl)sender).SelectedIndex == 0 && this.tabLicenseGen.SelectedIndex ==1)
                //{
                //    //ResetKeyGeneration();
                //}
                //else
                //{
                //    iPreviousIndex = -1;
                //}

                //if (((TabControl)sender).SelectedIndex == 2)
                //{
                //    btnSearch.Visible = true;
                //    btnLHClear.Visible = true;
                //}
                //else if (((TabControl)sender).SelectedIndex == 1)
                //{
                //    btnCancelLicense.Visible = true;
                //}
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        #endregion Common Events

        #region Rule Information Events

        /// <summary>
        /// Load the settings based on rule name selection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstRuleNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //if (iPreviousIndex != -1)
                //{
                //    iCurrentIndex = lstRuleNames.SelectedIndex;
                //    isUpdated = true;
                //    NotifyChanges(iPreviousIndex);
                //    if (!isUpdated)
                //    {
                //        iCurrentIndex = -1;
                //        return;
                //    }
                //    if (iPreviousIndex == lstRuleNames.SelectedIndex && iCurrentIndex != -1)
                //    {
                //        lstRuleNames.SelectedIndexChanged -= lstRuleNames_SelectedIndexChanged;
                //        lstRuleNames.SelectedIndex = iCurrentIndex;
                //        lstRuleNames.SelectedIndexChanged += lstRuleNames_SelectedIndexChanged;
                //    }
                //}

                ResetRuleInfo();
                iPreviousIndex = lstRuleNames.SelectedIndex;
                iCurrentIndex = -1;
                editToolStripMenuItem.Visible = (lstRuleNames.SelectedIndices.Count > 0);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// If Validation required is unChecked then all remaining check boxes will be disabled
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkRSValidationReq_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkRSValidationReq.Checked == false)
                {
                    chkRSLockSite.Checked = false;
                    chkRSDisableEGMs.Checked = false;
                    chkRSWarningOnly.Checked = false;
                    chkRSAlertreq.Checked = false;
                    grpValidationParam.Enabled = false;
                }
                else
                {
                    if (btnUpdate.Text != this.GetResourceTextByKey("Key_EditCaption"))   //"&Edit")
                        grpValidationParam.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// IF warning Only checked then Lock Site and Disable games will be unchecked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkRSWarningOnly_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkRSWarningOnly.Checked == true)
                {
                    chkRSLockSite.Checked = false;
                    chkRSDisableEGMs.Checked = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// If Lock Site Checked then warning only will be unchecked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkRSLockSite_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkRSLockSite.Checked == true)
                {
                    chkRSWarningOnly.Checked = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// If Disable Games Checked then warning only will be unchecked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkRSDisableEGMs_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkRSDisableEGMs.Checked == true)
                {
                    chkRSWarningOnly.Checked = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// Update button for update or insert the data in Rules Table
        /// Validations:
        /// 1. If Validation required is checked then atleast one validation parameter should be checked
        /// 2. If another rule contains the same settings then warn the user
        /// 3. If the rule has no changes then intimate to the user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnUpdate.Text == this.GetResourceTextByKey("Key_EditCaption"))  // Key_EditCaption
                {
                    this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_SL_UPDATE_CHANGE"), this.Text);   // ("Make Sure You Update Your Changes");
                    chkRSValidationReq.Enabled = true;
                    if (chkRSValidationReq.Checked)
                        grpValidationParam.Enabled = true;
                    btnUpdate.Text = this.GetResourceTextByKey("Key_CancelCaption");   // Key_CancelCaption
                    btnAddNew.Text = this.GetResourceTextByKey("Key_UpdateCaption");   // Key_UpdateCaption
                    lstRuleNames.Enabled = false;

                }
                else if (btnUpdate.Text == this.GetResourceTextByKey("Key_CancelCaption"))   // Key_CancelCaption
                {
                    RuleInfo_Load();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// Get and insert the new rule name Listbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddNew_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnAddNew.Text == this.GetResourceTextByKey("Key_UpdateCaption"))   // Key_UpdateCaption
                {
                    UpdateRule();
                }
                else
                {
                    this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_SL_UPDATE_CHANGE"), this.Text);
                    txtRSRuleName.Text = "";
                    lstRuleNames.SelectedItem = null;
                    txtRSRuleName.Enabled = true;
                    txtRSRuleName.Focus();
                    btnAddNew.Text = this.GetResourceTextByKey("Key_UpdateCaption");   // Key_UpdateCaption
                    btnUpdate.Text = this.GetResourceTextByKey("Key_CancelCaption");   // Key_CancelCaption
                    btnUpdate.Enabled = true;
                    lstRuleNames.Enabled = false;
                    chkRSValidationReq.Enabled = true;
                    if (chkRSValidationReq.Checked)
                        grpValidationParam.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// Validate and return the new site name
        /// Validation:
        /// 1. Finds rule name is not empty
        /// 2. Verify rulename does not contain any special chaecter(Except '-', '_' and ' ')
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnNewOk_Click(object sender, EventArgs e)
        {
            try
            {
                txtNewRuleName_Validation(txtNewRuleName);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }


        private void txtNewRuleName_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                String key = Convert.ToString(e.KeyChar);
                Int32 keyValue = Convert.ToInt32(e.KeyChar);
                if (key == "\r")//    r notifies return key
                {
                    btnNewOk_Click(null, null);
                }
                else if (keyValue == 27)
                {
                    frmNew.DialogResult = DialogResult.Cancel;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// Verify and notify for unsaved rule
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabPgRuleINfo_Leave(object sender, EventArgs e)
        {
            try
            {
                if (btnClose.Focused || btnAddNew.Focused || btnUpdate.Focused)
                    return;
                isUpdated = true;
                NotifyChanges(lstRuleNames.SelectedIndex);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        #endregion Rule Information Events

        #region Key Generation Events


        /// <summary>
        /// Company name combo selected index chaged event - to filter subcompany combo based the company selection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbCompName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbCompName.SelectedIndex > 0)
                {
                    FillCombo(cmbSubCompName, BusinessLogic.GetList("SubCompany", BusinessLogic.ToInteger(cmbCompName.SelectedValue), this.GetResourceTextByKey("Key_SelectSubCompany")), "Sub_Company_ID", "Sub_Company_Name");
                }
                else
                {
                    cmbSubCompName.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }


        /// <summary>
        /// SubCompany name combo selected index chaged event - to filter site combo based the subcompany selection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbSubCompName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbSubCompName.SelectedIndex > 0)
                {
                    FillCombo(cmbSiteName, BusinessLogic.GetList("Site", BusinessLogic.ToInteger(cmbSubCompName.SelectedValue), this.GetResourceTextByKey("Key_SelectSite")), "Site_ID", "Site_Name");
                }
                else
                {
                    cmbSiteName.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }


        /// <summary>
        /// Set the start date min value from the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grpLicenseSettings_EnabledChanged(object sender, EventArgs e)
        {
            try
            {
                if (grpLicenseSettings.Enabled)
                {
                    dtpkStartDate.MinDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, 00);
                    dtpkStartDate.Value = dtpkStartDate.MinDate;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// Setting min value for expiry date based on start date
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtpkStartDate_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                dtpkExpiryDate.MinDate = dtpkStartDate.Value.AddDays(LicenseDefaultDay).AddSeconds(59 - dtpkStartDate.MinDate.Second);
                dtpkExpiryDate.Value = dtpkExpiryDate.MinDate;
                if (numUDAlertBefor.Enabled)
                {
                    SetAlterBeforMinMax();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// Based on expiry date value change set min and max value for numeric updown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtpkExpiryDate_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (numUDAlertBefor.Enabled)
                {
                    SetAlterBeforMinMax();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// Load rule Information based on the rule selection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbLSRuleName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbLSRuleName.SelectedIndex > 0)
                {
                    SL_Rule objSL_Rule = BusinessLogic.GetRuleInformation(((KeyValuePair<int, String>)cmbLSRuleName.SelectedItem).Value);
                    if (objSL_Rule != null)
                    {
                        chkLSValidationReq.Checked = objSL_Rule.ValidationRequired;
                        chkLSLockSite.Checked = objSL_Rule.LockSite;
                        chkLSDisableEGMs.Checked = objSL_Rule.DisableGames;
                        chkLSWarningOnly.Checked = objSL_Rule.WarningOnly;
                        chkLSAlertReq.Checked = objSL_Rule.AlertRequired;
                        List<String> lstSiteCodes = BusinessLogic.GetAssociatedSites(objSL_Rule.RuleID);
                        int iLength = 0;
                        foreach (String sItem in lstSiteCodes)
                        {
                            if (sItem.Length > iLength)
                                iLength = sItem.Length;
                        }
                        if (lstSiteCodes.Count > 0)
                        {
                            dtgvAssociatedSites.DataSource = lstSiteCodes.Select(x => new { Value = x }).ToList();
                            dtgvAssociatedSites.Columns["Value"].HeaderText = this.GetResourceTextByKey("Key_SiteName");    //"Site Name";
                            dtgvAssociatedSites.Columns["Value"].Width = dtgvAssociatedSites.Width - 4;
                        }
                        else
                        {
                            dtgvAssociatedSites.DataSource = null;
                        }
                    }
                }
                else
                {
                    ResetRuleControls();
                    dtgvAssociatedSites.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            finally
            {
                SetAlterBeforMinMax();
            }
        }

        /// <summary>
        /// Based on alter required checked value we are enabling / disabling Numeric Up down 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkLSAlertReq_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkLSAlertReq.Checked)
                {
                    numUDAlertBefor.Enabled = true;
                    //lblAlertBefor.Enabled = true;
                    //lblinDays.Enabled = true;
                }
                else
                {
                    numUDAlertBefor.Enabled = false;
                    //lblAlertBefor.Enabled = false;
                    //lblinDays.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// Set min/ max values once enabled changed for numeric up down
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void numUDAlertBefor_EnabledChanged(object sender, EventArgs e)
        {
            try
            {
                //SetAlterBeforMinMax();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// 
        /// in a Numeric Up/Down box
        /// </summary>
        private void numUDAlertBefor_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                _oldValue = numUDAlertBefor.Value;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// Generate License Key and Update in Database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateData())
                {
                    LogManager.WriteLog("Generating a License key for the Site: " + BusinessLogic.ToInteger(cmbSiteName.SelectedValue), LogManager.enumLogLevel.Info);
                    txtLicenseKey.Text = BusinessLogic.Generate(15, 15);

                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// Update Data in Database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSLUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateData())
                {
                    if (String.IsNullOrEmpty(txtLicenseKey.Text.Trim()))
                    {
                        this.ShowWarningMessageBox(this.GetResourceTextByKey(1, "MSG_SL_LICENSE_GEN"),this.Text);   // ("Please generate a License key.");
                        btnGenerate.Focus();
                        return;
                    }
                    String strResult = BusinessLogic.InsertLicenseInfo(dtpkStartDate.Value, dtpkExpiryDate.Value, (KeyValuePair<int, String>)cmbLSRuleName.SelectedItem,
                                ((KeyValuePair<int, String>)cmbSiteName.SelectedItem), txtLicenseKey.Text, (short)numUDAlertBefor.Value, 1);
                    if (strResult.Contains("success"))
                    {
                        LogManager.WriteLog("License Info has been updated for the Site: " + BusinessLogic.ToInteger(cmbSiteName.SelectedValue), LogManager.enumLogLevel.Info);
                        this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_SL_LICENSE_UPDATESUCCESS"), this.Text);   // ("License information updated successfully.");
                        OpenTextFile(cmbCompName.Text, cmbSubCompName.Text, cmbSiteName.Text, txtLicenseKey.Text);
                        ResetKeyGeneration();
                        CreateNodesinTreeView();
                    }
                    else
                    {
                        this.ShowWarningMessageBox(this.GetResourceTextByKey(1, "MSG_SL_LICENSE_UPDATEFAILED"), this.Text);
                            //("An error occurred while updating the License information. \r\nPlease try again later.");
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

        }

        /// <summary>
        /// Reset Key generation Page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                ResetKeyGeneration();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// Enable/or disavle the controlls after key generation
        /// </summary>
        /// <param name="sender"></param>

        private void txtLicenseKey_TextChanged(object sender, EventArgs e)
        {
            try
            {
                bool isEnabled = string.IsNullOrEmpty(txtLicenseKey.Text.Trim());
                btnGenerate.Enabled = isEnabled;
                grpSiteSelection.Enabled = isEnabled;
                grpLicenseSettings.Enabled = isEnabled;

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        #endregion //Key Generation Events

        #region View/Cancel License Events
        /// <summary>
        /// In the site list tree, if user selects any site node it will populate license details for the selected site
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvSiteList_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                //Cursor.Current = Cursors.WaitCursor;
                string strNodeKey = string.Empty;
                ilicenseDetailsSelectedIndex = -1;
                strNodeKey = e.Node.Name.Substring(0, 2);
                Int32 iLastIndex = e.Node.Name.LastIndexOf("#") + 1;
                //tvSiteList.SelectedNode = e.Node;

                if (iLastIndex >= 0)
                {
                    Int32 iNodeId = BusinessLogic.ToInteger(e.Node.Name.Substring(iLastIndex));
                    //get the Node id and change options in Group by combo
                    //set values of IDs at higher level to 0 and lower level to -1
                    if (strNodeKey.Equals("SI"))
                        iSiteId = iNodeId;
                    else
                        iSiteId = -1;

                    IEnumerable<rsp_SL_GetLicenseDetailsResult> licenseDetailsResults = DataManipulator.GetLicenseDetails(iSiteId);
                    licenseDetailsResultList = licenseDetailsResults.ToList();

                    if (licenseDetailsResultList.Count > 0)
                    {
                        //set the datasource for the license detail grid
                        dtgvLicenseDetails.DataSource = licenseDetailsResultList;
                        dtgvLicenseDetails.Columns["LicenseInfoID"].Visible = false;
                        dtgvLicenseDetails.Enabled = true;
                        FormatLicenseDetailsGridHeader();
                    }
                    else
                    {
                        dtgvLicenseDetails.DataSource = null;
                        dtgvLicenseDetails.Enabled = false;
                        btnCancelLicense.Enabled = false;
                        btnActivateLicense.Enabled = false;

                    }
                }
                else
                {
                    iSiteId = -1;
                }
                //tvSiteList.SelectedNode = e.Node;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            finally
            {
                // System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            }
        }

        /// <summary>
        /// license details grid row enter event - to save selected row index in the license details grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtgvLicenseDetails_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                btnActivateLicense.Enabled = false;
                btnCancelLicense.Enabled = false;

                ilicenseDetailsSelectedIndex = e.RowIndex;
                if (ilicenseDetailsSelectedIndex >= 0)
                {
                    Int32 iKeyStatus = licenseDetailsResultList[ilicenseDetailsSelectedIndex].KeyStatusID;
                    btnActivateLicense.Enabled = (iKeyStatus == (int)(BusinessLogic.LicenseKeyStatus.Created));
                    btnCancelLicense.Enabled = ((iKeyStatus == (int)(BusinessLogic.LicenseKeyStatus.Active)) || (iKeyStatus == (int)(BusinessLogic.LicenseKeyStatus.Created)));
                    cancelLicenseToolStripMenuItem.Enabled = btnCancelLicense.Enabled;
                    activateLicenseToolStripMenuItem.Enabled = btnActivateLicense.Enabled;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// Cancel license button click event - to cancel the selected license in the grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancelLicense_Click(object sender, EventArgs e)
        {
            try
            {
                int iKeyStatus = 0;
                iKeyStatus = licenseDetailsResultList[ilicenseDetailsSelectedIndex].KeyStatusID;

                if (iKeyStatus == 3 || iKeyStatus == 4)
                {
                    this.ShowWarningMessageBox(this.GetResourceTextByKey(1, "MSG_SL_LICENSE_CANCEL"),this.Text);
                        //("Only Active and Created License can be cancelled.");
                    return;
                }

                if (this.ShowQuestionMessageBox(((iKeyStatus == 2) ? this.GetResourceTextByKey(1, "MSG_SL_LICENSE_CANCEL_LOCK") : "") + this.GetResourceTextByKey(1, "MSG_SL_LICENSE_CANCEL_PROCEED")) == DialogResult.No) return;

                LogManager.WriteLog("Cancelling the License " + licenseDetailsResultList[ilicenseDetailsSelectedIndex].LicenseInfoID.ToString(), LogManager.enumLogLevel.Info);
                if (BusinessLogic.CancelLicense(licenseDetailsResultList, ilicenseDetailsSelectedIndex))
                {
                    LogManager.WriteLog("The License " + licenseDetailsResultList[ilicenseDetailsSelectedIndex].LicenseInfoID.ToString() + " has been cancelled successfully", LogManager.enumLogLevel.Info);
                    CreateNodesinTreeView();
                    //dtgvLicenseDetails.DataSource = null;
                    this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_SL_CANCELSUCCESS"),this.Text);    //("The License has been cancelled successfully.");
                }
                else
                    this.ShowWarningMessageBox(this.GetResourceTextByKey(1, "MSG_SL_CANCELFAILED"),this.Text);   //("An error occurred while cancelling the License. \r\nPlease try again later.");
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// View/Cancel page enter event - when user enter into this page, to load the site tree
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void tabPgViewCancelLicense_Enter(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        //CreateNodesinTreeView();
        //        //if (btnHideSiteList.Text.Trim() == ">>")
        //        //    ShowSiteList();
        //        //btnCancelLicense.Enabled = false;
        //    }
        //    catch (Exception ex)
        //    {
        //        ExceptionManager.Publish(ex);
        //    }
        //    finally
        //    {
        //        //btnCancelLicense.Visible = true;
        //    }
        //}

        /// <summary>
        /// Enable or Disable cancel button based on grid row count
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 

        private void btnActivateLicense_Click(object sender, EventArgs e)
        {
            bool refreshTreeView = true;
            try
            {
                int iKeyStatus = 0;
                iKeyStatus = licenseDetailsResultList[ilicenseDetailsSelectedIndex].KeyStatusID;
                if (iKeyStatus == 1)
                {

                    int result = BusinessLogic.ActivateLicense(licenseDetailsResultList, ilicenseDetailsSelectedIndex);
                    if (result == 0)
                    {
                        LogManager.WriteLog("The License " + licenseDetailsResultList[ilicenseDetailsSelectedIndex].LicenseInfoID.ToString() + " has been cancelled successfully", LogManager.enumLogLevel.Info);

                        this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_SL_ACTIVATESUCCESS"), this.Text);   //("The License has been Activated successfully.");
                    }

                    else if (result == 2)
                    {
                        this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_SL_ACTIVATED"), this.Text);   // ("The License already activated");
                    }
                    else if (result == 1)
                    {
                        this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_SL_EXPIRED"), this.Text);    //("The License has been Expired ");
                    }

                    else if (result == 3)
                    {
                        this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_SL_CANCELLED"), this.Text);    //("The License has been cancelled");
                        
                    }
                    else
                    {
                        refreshTreeView = false;
                        this.ShowWarningMessageBox(this.GetResourceTextByKey(1, "MSG_SL_ACTIVATEFAILED"), this.Text);   //("An error occurred while activation the License. \r\nPlease try again later.");
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            finally
            {
                if (refreshTreeView) CreateNodesinTreeView();
            }
            
        }
        private void dtgvLicenseDetails_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                //btnCancelLicense.Enabled = (dtgvLicenseDetails.RowCount > 0) ? true : false;
                //btnActivateLicense.Enabled = (dtgvLicenseDetails.RowCount > 0) ? true : false;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        ///// <summary>
        ///// Hide or Visible Site List
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void btnHideSiteList_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (btnHideSiteList.Text.Trim() == "<<")
        //        {
        //            HideSiteList();
        //        }
        //        else
        //        {
        //            ShowSiteList();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ExceptionManager.Publish(ex);
        //    }

        //}

        #endregion View/Cancel License Events

        #region License History Events

        /// <summary>
        /// to fetch the search results based on the search parameters
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                IEnumerable<rsp_SL_GetLicenseHistoryDetailsResult> licenseHistorySearchResults = DataManipulator.GetLicenseHistorySearch(BusinessLogic.ToInteger(cmbSCCompanyName.SelectedValue)
                                                                                                    , BusinessLogic.ToInteger(cmbSCSubCompanyName.SelectedValue)
                                                                                                    , BusinessLogic.ToInteger(cmbSCSiteName.SelectedValue)
                                                                                                    , dtpkFromStartDate.Value
                                                                                                    , dtpkToStartDate.Value
                                                                                                    , dtpkFromExpiryDate.Value
                                                                                                    , dtpkToExpiryDate.Value
                                                                                                    , BusinessLogic.ToInteger(cmbSCKeyStatus.SelectedValue)
                                                                                                    , GetBooleanComboValue(cmbSCValidationReq)
                                                                                                    , GetBooleanComboValue(cmbSCLockSite)
                                                                                                    , GetBooleanComboValue(cmbSCDisableEGM)
                                                                                                    , GetBooleanComboValue(cmbSCWarningOnly)
                                                                                                    , GetBooleanComboValue(cmbSCAlertRequired)
                                                                                                    , BusinessLogic.iLoginStaffID,
                                                                                                     Convert.ToInt32(cmbcreateBy.SelectedValue)
                                                                                                    , Convert.ToInt32(cmbActivatedBy.SelectedValue)
                                                                                                    , Convert.ToInt32(cmbCancelBy.SelectedValue));

                licenseHistorySearchResultList = licenseHistorySearchResults.ToList();

                if (licenseHistorySearchResultList.Count > 0)
                {
                    //set the datasource for the license history search result grid
                    dtGVSearchResults.DataSource = licenseHistorySearchResultList;
                    FormatSearchresultsGrid();
                    FormatSearchresultsGridHeader();
                }
                else
                {
                    dtGVSearchResults.DataSource = null;
                    this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_SL_NO_INFO"), this.Text);   //("No Information available for the selected criteria.");
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// Company name combo selected index chaged event - to filter subcompany combo based the company selection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbSCCompanyName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbSCCompanyName.SelectedIndex < 0) return;
                FillCombo(cmbSCSubCompanyName, BusinessLogic.GetList("SubCompany", BusinessLogic.ToInteger(cmbSCCompanyName.SelectedValue), allCapsText), "Sub_Company_ID", "Sub_Company_Name");
                FillCombo(cmbSCSiteName, BusinessLogic.GetSiteList("Site", BusinessLogic.ToInteger(cmbSCCompanyName.SelectedValue), BusinessLogic.ToInteger(cmbSCSubCompanyName.SelectedValue), allCapsText), "Site_ID", "Site_Name");
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// SubCompany name combo selected index chaged event - to filter site combo based the subcompany selection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbSCSubCompanyName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbSCSubCompanyName.SelectedIndex < 0) return;
                FillCombo(cmbSCSiteName, BusinessLogic.GetSiteList("Site", BusinessLogic.ToInteger(cmbSCCompanyName.SelectedValue), BusinessLogic.ToInteger(cmbSCSubCompanyName.SelectedValue), allCapsText), "Site_ID", "Site_Name");
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnLHClear_Click(object sender, EventArgs e)
        {
            try
            {
                ResetSearchControls();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }


        /// <summary>
        /// To Set the minimum value to the dtpkToStartDate
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtpkFromStartDate_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                dtpkFromExpiryDate.MinDate = dtpkFromStartDate.Value;
                dtpkToStartDate.MinDate = dtpkFromStartDate.Value;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// To Set the minimum value to the dtpkToExpiryDate
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtpkFromExpiryDate_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                dtpkToExpiryDate.MinDate = dtpkFromExpiryDate.Value;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        #endregion License History Events

        #endregion //Events

        #region Custom Methods

        #region Common Methods

        public void HideButtons()
        {
            try
            {
                bool isVisible = (tabSiteLicensing.SelectedTab == tabPgLicenseHistory);
                btnSearch.Visible = isVisible;
                btnLHClear.Visible = false;

                isVisible = tabSiteLicensing.SelectedTab == tabPgLicenseGen && tabLicenseGen.SelectedTab == tabPgRuleINfo;
                btnUpdate.Visible = isVisible && bRulePermission;
                btnAddNew.Visible = isVisible && bRulePermission;
                addNewToolStripMenuItem.Enabled = isVisible && bRulePermission;
                editToolStripMenuItem.Enabled = isVisible && (lstRuleNames.Items.Count > 0) && bRulePermission;
                lstRuleNames.Enabled = isVisible && !txtRSRuleName.Enabled;

                isVisible = tabSiteLicensing.SelectedTab == tabPgLicenseGen && tabLicenseGen.SelectedTab == tabPgKeyGeneration;
                btnSLUpdate.Visible = isVisible;
                button1.Visible = isVisible;

                isVisible = (tabSiteLicensing.SelectedTab == tabPgViewCancelLicense);
                btnCancelLicense.Visible = isVisible;
                btnActivateLicense.Visible = isVisible;
                btn_Refresh.Visible = true;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// Check the permission level for the user and restrict to the site licensing access
        /// </summary>
        /// <returns></returns>
        private bool CheckAuthentication()
        {
            try
            {
                IEnumerable<rsp_GetSiteLicensingRightsResult> siteLicensingRightsResult = DataManipulator.GetSiteLicensingRightsResult();
                siteLicensingRightsResultList = siteLicensingRightsResult.ToList();
                if (siteLicensingRightsResultList.Count <= 0) return false;

                if (!siteLicensingRightsResultList[0].HQ_Admin_SiteLicensing.GetValueOrDefault())
                {
                    this.ShowWarningMessageBox(this.GetResourceTextByKey(1, "MSG_SL_NO_ACCESS"), this.Text);    //("You don’t have permission to access Site Licensing functionality.");
                    return false;
                }

                if (!siteLicensingRightsResultList[0].HQ_Admin_SiteLicensing_LicenseGen.GetValueOrDefault())
                    tabSiteLicensing.Controls.Remove(tabPgLicenseGen);
                if (!siteLicensingRightsResultList[0].HQ_Admin_SiteLicensing_LicenseGen_RuleInfo.GetValueOrDefault())
                    tabLicenseGen.Controls.Remove(tabPgRuleINfo);
                if (!siteLicensingRightsResultList[0].HQ_Admin_SiteLicensing_LicenseGen_KeyGen.GetValueOrDefault())
                    tabLicenseGen.Controls.Remove(tabPgKeyGeneration);
                if (!siteLicensingRightsResultList[0].HQ_Admin_SiteLicensing_ViewCancelLicense.GetValueOrDefault())
                    tabSiteLicensing.Controls.Remove(tabPgViewCancelLicense);
                //if (siteLicensingRightsResultList[0].HQ_Admin_SiteLicensing_ActivateLicense.GetValueOrDefault())
                //    ShowActivateLicense = true;
                if (!siteLicensingRightsResultList[0].HQ_Admin_SiteLicensing_LicenseHistory.GetValueOrDefault())
                    tabSiteLicensing.Controls.Remove(tabPgLicenseHistory);
                if (!siteLicensingRightsResultList[0].HQ_Admin_SiteLicensing_LicenseGen_RuleInfo_AddorEditRule.GetValueOrDefault())
                {
                    btnAddNew.Enabled = false;
                    btnUpdate.Enabled = false;
                    bRulePermission = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            return true;
        }

        ///// <summary>
        ///// Set tooltip to the ComboBoxes 
        ///// </summary>
        ///// <param name="comboBox1"></param>
        ///// <param name="length"></param>
        //private void SetToolTip(ComboBox comboBox1, int length)
        //{
        //    try
        //    {
        //        String caption = "" + comboBox1.Text;
        //        ToolTip1.SetToolTip(comboBox1, (caption.Trim().Length > length) ? caption : "");
        //        ToolTip1.AutoPopDelay = 2000;
        //        ToolTip1.InitialDelay = 0;
        //        ToolTip1.ReshowDelay = 100;
        //        ToolTip1.ShowAlways = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        ExceptionManager.Publish(ex);
        //    }
        //}

        #endregion //Common Methods

        #region Rule Information Methods

        /// <summary>
        /// Validation:
        /// 1. Finds rule name is not empty
        /// 2. Verify rulename does not contain any special chaecter(Except '-', '_' and ' ')
        /// </summary>
        /// <param name="txtTmpRuleName"></param>
        private void txtNewRuleName_Validation(TextBox txtTmpRuleName)
        {
            try
            {
                String txt = txtTmpRuleName.Text;
                if (String.IsNullOrEmpty(txt.Trim()))
                {
                    this.ShowWarningMessageBox(this.GetResourceTextByKey(1, "MSG_SL_NAME_NOTEMPTY"), this.Text);    //("Rule Name should not be empty.");
                    txtTmpRuleName.Clear();
                    txtTmpRuleName.Focus();
                }
                else if (!Char.IsLetter(txt.Trim()[0]))//Regex.IsMatch(txt, "(?=^[a-zA-z]?)*"))//Regex.IsMatch(txt, "[^a-zA-z0-9\\s-_]"))
                {
                    this.ShowWarningMessageBox(this.GetResourceTextByKey(1, "MSG_SL_NAME_ALPHABET"), this.Text);     //("Rule name should start with Alphabet.");
                    txtTmpRuleName.Clear();
                    txtTmpRuleName.Focus();
                }
                else
                {
                    frmNew.DialogResult = DialogResult.OK;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// Verify for similar rule setting and notify to user for similarity
        /// If user needs similar setting then return true else false
        /// </summary>
        /// <param name="ruleName"></param>
        /// <param name="validationRequired"></param>
        /// <param name="lockSite"></param>
        /// <param name="disableGames"></param>
        /// <param name="warningOnly"></param>
        /// <param name="alertRequired"></param>
        /// <returns></returns>
        private bool NeedUpdate(string ruleName, bool validationRequired, bool lockSite, bool disableGames, bool warningOnly, bool alertRequired)
        {
            try
            {
                String[] similarRules = BusinessLogic.GetSimilarRules(validationRequired, lockSite, disableGames, warningOnly, alertRequired);
                bool continueUpdate = false;
                String ruleNames = "";
                if (similarRules.Count() > 0)
                {
                    foreach (String item in similarRules)
                    {
                        ruleNames += item + "\r\n";
                    }

                    if (similarRules.Contains(ruleName) || ruleNames.Trim().ToLower().Equals(ruleName.Trim().ToLower()))
                    {
                        this.ShowWarningMessageBox(this.GetResourceTextByKey(1, "MSG_SL_RULESTATE"), this.Text);     //("The rule is in updated state.");
                    }
                    else
                    {
                        DialogResult result = this.ShowQuestionMessageBox(string.Format(this.GetResourceTextByKey(1, "MSG_SL_SAMESETTINGS"), ruleNames));

                        //DialogResult result = this.ShowQuestionMessageBox("The following Rule(s) have the same settings:\r\n"
                        //                                     + ruleNames
                        //                                     + "\r\n\r\nDo you still want to update the Rule?");
                        
                        if (result == DialogResult.Yes)
                            continueUpdate = true;
                    }
                }
                else
                {
                    continueUpdate = true;
                }
                return continueUpdate;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return false;
            }
        }

        /// <summary>
        /// Check and return true for any changes
        /// </summary>
        /// <returns></returns>
        private bool CheckForChange()
        {
            try
            {
                if (String.IsNullOrEmpty(txtRSRuleName.Text))
                    return false;
                SL_Rule objSL_Rule = BusinessLogic.GetRuleInformation(txtRSRuleName.Text);
                if (objSL_Rule != null)
                {
                    if (txtRSRuleName.Text != objSL_Rule.RuleName ||
                       chkRSValidationReq.Checked != objSL_Rule.ValidationRequired ||
                       chkRSLockSite.Checked != objSL_Rule.LockSite ||
                       chkRSDisableEGMs.Checked != objSL_Rule.DisableGames ||
                       chkRSWarningOnly.Checked != objSL_Rule.WarningOnly ||
                       chkRSAlertreq.Checked != objSL_Rule.AlertRequired)
                    {
                        return true;
                    }
                }
                else if (BusinessLogic.GetAllRuleNames().Count() != lstRuleNames.Items.Count)
                    return true;
                return false;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return false;
            }
        }

        /// <summary>
        /// Find and notify for unsaved rule information
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        private void NotifyChanges(int index)
        {
            try
            {
                if (String.IsNullOrEmpty(txtRSRuleName.Text) || index < 0 || lstRuleNames.Items.Count <= index)
                    return;
                if (!bRulePermission)
                {
                    ResetRuleInfo();
                    return;
                }
                SL_Rule objSL_Rule = BusinessLogic.GetRuleInformation(lstRuleNames.Items[index].ToString());
                if (objSL_Rule != null)
                {
                    if (txtRSRuleName.Text != objSL_Rule.RuleName ||
                       chkRSValidationReq.Checked != objSL_Rule.ValidationRequired ||
                       chkRSLockSite.Checked != objSL_Rule.LockSite ||
                       chkRSDisableEGMs.Checked != objSL_Rule.DisableGames ||
                       chkRSWarningOnly.Checked != objSL_Rule.WarningOnly ||
                       chkRSAlertreq.Checked != objSL_Rule.AlertRequired)
                    {
                        lstRuleNames.SelectedIndexChanged -= lstRuleNames_SelectedIndexChanged;
                        lstRuleNames.SelectedIndex = iPreviousIndex;
                        lstRuleNames.SelectedIndexChanged += lstRuleNames_SelectedIndexChanged;

                       // DialogResult result = this.ShowQuestionMessageBox("The Rule – " + txtRSRuleName.Text + " is not saved.\r\n\r\nDo you want to save the Rule?");
                        DialogResult result = this.ShowQuestionMessageBox(string.Format(this.GetResourceTextByKey(1, "MSG_SL_SAVERULE"), txtRSRuleName.Text));
                        if (result == DialogResult.Yes)
                        {
                            isUpdated = false;
                            UpdateRule();
                        }
                        else
                        {
                            ResetRuleInfo();
                            ResetRuleCtrls();
                        }
                    }
                    else
                    {
                        isUpdated = true;
                    }
                }
                else
                {
                    if (iPreviousIndex != -1)
                    {
                        lstRuleNames.SelectedIndexChanged -= lstRuleNames_SelectedIndexChanged;
                        lstRuleNames.SelectedIndex = iPreviousIndex;
                        lstRuleNames.SelectedIndexChanged += lstRuleNames_SelectedIndexChanged;
                    }
                    isUpdated = false;
                    CheckUnSavedRule();
                }
                return;
            }
            catch (Exception exNotifyChanges)
            {
                ExceptionManager.Publish(exNotifyChanges);
            }
        }

        /// <summary>
        /// Custom Dialog to get the New Rule Name
        /// </summary>
        /// <returns></returns>
        private String ShowCustomDialog()
        {
            try
            {
                frmNew = new Form();
                frmNew.Text = "New Rule Name";
                frmNew.Height = 123;
                frmNew.Width = 300;
                frmNew.MaximizeBox = false;
                frmNew.MinimizeBox = false;
                frmNew.ShowIcon = false;
                frmNew.FormBorderStyle = FormBorderStyle.FixedSingle;
                frmNew.StartPosition = FormStartPosition.CenterParent;
                Label lblRuleName = new Label();
                lblRuleName.AutoSize = true;
                lblRuleName.Location = new System.Drawing.Point(10, 20);
                lblRuleName.Size = new System.Drawing.Size(60, 13);
                lblRuleName.TabIndex = 0;
                lblRuleName.Text = "* Rule Name :";
                frmNew.Controls.Add(lblRuleName);

                txtNewRuleName = new TextBox();
                txtNewRuleName.Location = new Point(84, 17);
                txtNewRuleName.Size = new System.Drawing.Size(202, 20);
                txtNewRuleName.TabIndex = 1;
                txtNewRuleName.MaxLength = 25;
                txtNewRuleName.Focus();
                frmNew.Controls.Add(txtNewRuleName);
                txtNewRuleName.KeyPress += new KeyPressEventHandler(txtNewRuleName_KeyPress);

                Button btnNewOk = new Button { Text = "OK", Top = 53, Left = 65 };
                btnNewOk.Click += new EventHandler(btnNewOk_Click);
                frmNew.Controls.Add(btnNewOk);

                Button btnCancle = new Button { Text = "Cancel", Top = 53, Left = 161 };
                btnCancle.DialogResult = DialogResult.Cancel;
                frmNew.Controls.Add(btnCancle);

                if (frmNew.ShowDialog() == DialogResult.OK && !String.IsNullOrEmpty(txtNewRuleName.Text.Trim()))
                {
                    return txtNewRuleName.Text.Trim();
                }
                else
                {
                    return string.Empty;
                }
            }
            catch (Exception exShowCustomDialog)
            {
                ExceptionManager.Publish(exShowCustomDialog);
                return string.Empty;
            }
        }

        /// <summary>
        /// Validate and update / create the rule
        /// </summary>
        private void UpdateRule()
        {
            try
            {
                bool isValid = true;
                if (txtRSRuleName.Enabled)
                {
                    bool isAdded = false;
                    if (string.IsNullOrEmpty(txtRSRuleName.Text))
                    {
                        this.ShowWarningMessageBox(this.GetResourceTextByKey(1, "MSG_SL_NAME_NOTEMPTY"), this.Text);    //("Rule Name should not be empty.");
                        txtRSRuleName.Focus();
                        return;
                    }
                    else
                    {
                        foreach (String item in lstRuleNames.Items)
                        {
                            if (item.Trim().ToLower().Equals(txtRSRuleName.Text.Trim().ToLower()))
                            {
                                isAdded = true;
                                break;
                            }
                        }
                        if (isAdded)
                        {
                            this.ShowWarningMessageBox(this.GetResourceTextByKey(1, "MSG_SL_RULE_EXISTS"), this.Text);     //("Rule Name entered already exists.");
                            txtRSRuleName.Focus();
                            return;
                        }
                    }

                }
                if (chkRSValidationReq.Checked == true && chkRSLockSite.Checked == false && chkRSDisableEGMs.Checked == false && chkRSWarningOnly.Checked == false)
                {
                    this.ShowWarningMessageBox(this.GetResourceTextByKey(1, "MSG_SL_RULE_PARAM"), this.Text); 
                    //this.ShowWarningMessageBox("Please select atleast one validation parameter \r\n[Lock Site, Disable Game or Warning Only]");
                    isValid = false;
                }
                else
                {
                    bool isRuleAssociated = false;
                    if (CheckForChange())
                    {
                        isRuleAssociated = BusinessLogic.IsRuleAssociated(txtRSRuleName.Text);
                        if (isRuleAssociated)
                        {
                            DialogResult result = this.ShowQuestionMessageBox(this.GetResourceTextByKey(1, "MSG_SL_RULE_LICENSE"), this.Text); 
                                //("This Rule is associated with existing Site License(s)."
                               // + "\r\nModifying this rule will affect those License settings."
                                //+ "\r\n\r\nDo you still want to update the Rule?");
                            if (result == DialogResult.No)
                            {
                                RuleInfo_Load(txtRSRuleName.Text);
                                txtRSRuleName.Enabled = false;
                                return;
                            }
                        }
                    }

                    bool continueUpdate = NeedUpdate(txtRSRuleName.Text, chkRSValidationReq.Checked, chkRSLockSite.Checked, chkRSDisableEGMs.Checked, chkRSWarningOnly.Checked, chkRSAlertreq.Checked);
                    if (continueUpdate)
                    {
                        LogManager.WriteLog("Updating the Rule: " + txtRSRuleName.Text, LogManager.enumLogLevel.Info);
                        int result = BusinessLogic.InsertOrUpdateRule(txtRSRuleName.Text, chkRSValidationReq.Checked, chkRSLockSite.Checked, chkRSDisableEGMs.Checked, chkRSWarningOnly.Checked, chkRSAlertreq.Checked);
                        if (result == 0)
                        {
                            this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_SL_RULE_UPDATESUCCESS"), this.Text);    //("Rule information updated successfully.");
                            isUpdated = true;
                            txtRSRuleName.Enabled = false;
                            LogManager.WriteLog("The Rule " + txtRSRuleName.Text + " has been updated successfully", LogManager.enumLogLevel.Info);
                        }
                        else if (result == 1)
                        {
                            txtRSRuleName.Enabled = false;
                            LogManager.WriteLog("Failure when updating the rule " + txtRSRuleName.Text, LogManager.enumLogLevel.Info);
                            this.ShowWarningMessageBox(this.GetResourceTextByKey(1, "MSG_SL_RULE_UPDATEFAILED"), this.Text); 
                                //("An error occurred while updating the Rule information. \r\nPlease try again later.");
                        }
                    }
                }
                int count = BusinessLogic.GetAllRuleNames().Count();

                // Key_UpdateCaption
                if (this.btnAddNew.Text == this.GetResourceTextByKey("Key_UpdateCaption") && (count == lstRuleNames.Items.Count + 1 || count == lstRuleNames.Items.Count) && isValid)
                {
                    //ResetRuleInfo();
                    //ResetRuleCtrls();
                    RuleInfo_Load(txtRSRuleName.Text);
                    txtRSRuleName.Enabled = false;
                }
            }
            catch (Exception exUpdateRule)
            {
                ExceptionManager.Publish(exUpdateRule);
            }
        }

        /// <summary>
        /// Loading rules in list box and selecting first rule
        /// </summary>
        private void RuleInfo_Load()
        {
            try
            {
                lstRuleNames.Items.Clear();
                String[] allRules = BusinessLogic.GetAllRuleNames();
                if (allRules != null)
                    lstRuleNames.Items.AddRange(allRules);
                if (lstRuleNames.Items.Count > 0)
                {
                    lstRuleNames.SelectedItem = lstRuleNames.Items[0];
                    ResetRuleCtrls();
                }
                else
                {
                    ResetControls();
                    btnUpdate.Enabled = false;
                    chkRSValidationReq.Enabled = false;
                    btnAddNew.Text = this.GetResourceTextByKey("Key_AddNewCaption");      // Key_AddNewCaption
                    btnUpdate.Text = this.GetResourceTextByKey("Key_EditCaption");        // Key_EditCaption
                }

            }
            catch (Exception exUpdateRule)
            {
                ExceptionManager.Publish(exUpdateRule);
            }
            finally
            {
                editToolStripMenuItem.Enabled = (lstRuleNames.Items.Count > 0) && bRulePermission;
            }
        }

        /// <summary>
        /// Loading rules in list box and selecting first rule
        /// </summary>
        private void RuleInfo_Load(string rule_Name)
        {
            try
            {
                RuleInfo_Load();
                lstRuleNames.SelectedItem = rule_Name;
            }
            catch (Exception exUpdateRule)
            {
                ExceptionManager.Publish(exUpdateRule);
            }
        }

        private void ResetRuleCtrls()
        {
            chkRSValidationReq.Enabled = false;
            grpValidationParam.Enabled = false;
            txtRSRuleName.Enabled = false;
            btnAddNew.Text = this.GetResourceTextByKey("Key_AddNewCaption");     // Key_AddNewCaption
            btnUpdate.Text = this.GetResourceTextByKey("Key_EditCaption");       // Key_EditCaption
            lstRuleNames.Enabled = true;
        }

        /// <summary>
        /// Reset all check boxes and text boxes
        /// </summary>
        private void ResetControls()
        {
            try
            {
                txtRSRuleName.Text = "";
                txtRSRuleName.Enabled = false;
                chkRSValidationReq.Checked = false;
                chkRSLockSite.Checked = false;
                chkRSDisableEGMs.Checked = false;
                chkRSWarningOnly.Checked = false;
                chkRSAlertreq.Checked = false;
                grpValidationParam.Enabled = false;
                lstRuleNames.Enabled = true;
            }
            catch (Exception exUpdateRule)
            {
                ExceptionManager.Publish(exUpdateRule);
            }
        }

        /// <summary>
        /// Based on the selected role we are reseting the values
        /// </summary>
        private void ResetRuleInfo()
        {
            try
            {
                string name = string.IsNullOrWhiteSpace(lstRuleNames.Text) ? txtRSRuleName.Text : lstRuleNames.Text;
                SL_Rule objSL_Rule = BusinessLogic.GetRuleInformation(name);
                if (btnUpdate.Enabled == false && bRulePermission)
                {
                    btnUpdate.Enabled = true;
                    chkRSValidationReq.Enabled = true;
                }
                if (objSL_Rule != null)
                {
                    txtRSRuleName.Text = name;
                    chkRSValidationReq.Checked = objSL_Rule.ValidationRequired;
                    chkRSLockSite.Checked = objSL_Rule.LockSite;
                    chkRSDisableEGMs.Checked = objSL_Rule.DisableGames;
                    chkRSWarningOnly.Checked = objSL_Rule.WarningOnly;
                    chkRSAlertreq.Checked = objSL_Rule.AlertRequired;
                }
                else
                {
                    ResetControls();
                    txtRSRuleName.Text = name;
                }
            }
            catch (Exception exUpdateRule)
            {
                ExceptionManager.Publish(exUpdateRule);
            }
        }

        /// <summary>
        /// Verify and notify for Unsaved new rule
        /// </summary>
        private void CheckUnSavedRule()
        {
            try
            {
                if (BusinessLogic.GetAllRuleNames().Count() != lstRuleNames.Items.Count)
                {
                    //DialogResult result = this.ShowQuestionMessageBox("The Rule – " + txtRSRuleName.Text + " is not saved.\r\n\r\nDo you want to save the Rule?");
                    DialogResult result = this.ShowQuestionMessageBox(string.Format(this.GetResourceTextByKey(1, "MSG_SL_SAVERULE"), txtRSRuleName.Text));

                    if (result == DialogResult.Yes)
                    {
                        isUpdated = false;
                        UpdateRule();
                    }
                    else
                    {
                        isUpdated = true;
                        ResetRuleCtrls();
                        if (iPreviousIndex != -1)
                        {
                            lstRuleNames.SelectedIndexChanged -= lstRuleNames_SelectedIndexChanged;
                            lstRuleNames.Items.RemoveAt(iPreviousIndex);
                            lstRuleNames.SelectedIndexChanged += lstRuleNames_SelectedIndexChanged;
                        }
                        if (iCurrentIndex != -1)
                        {
                            lstRuleNames.SelectedIndex = iCurrentIndex;
                        }
                    }
                }
            }
            catch (Exception exCheckUnSavedRule)
            {
                ExceptionManager.Publish(exCheckUnSavedRule);
            }
        }

        #endregion Rule Information Methods

        #region Key Generation Methods

        /// <summary>
        /// Load the Key Generation Tab
        /// </summary>
        private void ResetKeyGeneration()
        {
            try
            {
                FillCombo(cmbCompName, BusinessLogic.GetList("Company", 0, this.GetResourceTextByKey("Key_SelectCompany")), "Company_ID", "Company_Name");
                FillCombo(cmbLSRuleName, BusinessLogic.GetList("Rule", 0, this.GetResourceTextByKey("Key_SelectRule")), "RuleID", "RuleName");
                ResetRuleControls();
                dtpkStartDate.MinDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, 00);
                dtpkStartDate.Value = dtpkStartDate.MinDate;
                dtpkExpiryDate.Value = dtpkExpiryDate.MinDate;
                tableLayoutPanel1.SetColumnSpan(tableLayoutPanel1.GetControlFromPosition(0, 0), 2);
                txtLicenseKey.Text = string.Empty;

                if (cmbLSRuleName.Items.Count > 0)
                    cmbLSRuleName.SelectedIndex = 0;

                cmbSiteName.DataSource = null;
                cmbSubCompName.DataSource = null;
                dtgvAssociatedSites.DataSource = null;
                numUDAlertBefor.Value = 0;

            }
            catch (Exception exResetKeyGeneration)
            {
                ExceptionManager.Publish(exResetKeyGeneration);
            }

        }

        private void ResetRuleControls()
        {
            chkLSValidationReq.Checked = false;
            chkLSLockSite.Checked = false;
            chkLSDisableEGMs.Checked = false;
            chkLSWarningOnly.Checked = false;
            chkLSAlertReq.Checked = false;


        }

        /// <summary>
        /// Validate License dates with other active/created Licenses
        /// </summary>
        /// <returns></returns>
        private bool ValidateData()
        {
            try
            {
                if (cmbCompName.SelectedIndex <= 0)
                {
                    this.ShowWarningMessageBox(this.GetResourceTextByKey(1, "MSG_SL_SELECT_COMPANY"), this.Text);    //("Please select a Company.");
                    cmbCompName.Focus();
                    return false;
                }
                if (cmbSubCompName.SelectedIndex <= 0)
                {
                    this.ShowWarningMessageBox(this.GetResourceTextByKey(1, "MSG_SL_SELECT_SUBCOMPANY"), this.Text);     //("Please select a Sub Company.");
                    cmbSubCompName.Focus();
                    return false;
                }
                int siteID = 0;
                String strResult = "";
                if (cmbSiteName.SelectedIndex > 0)
                {
                    siteID = ((KeyValuePair<int, String>)cmbSiteName.SelectedItem).Key;
                }
                else
                {
                    this.ShowWarningMessageBox(this.GetResourceTextByKey(1, "MSG_SL_SELECT_SITE"), this.Text);     //("Please select a Site.");
                    cmbSiteName.Focus();
                    return false;
                }
                if (cmbLSRuleName.SelectedIndex <= 0)
                {
                    this.ShowWarningMessageBox(this.GetResourceTextByKey(1, "MSG_SL_SELECT_RULE"), this.Text);     //("Please select a Rule.");
                    cmbLSRuleName.Focus();
                    return false;
                }
                
                strResult = BusinessLogic.ValidateDates(siteID, dtpkStartDate.Value, dtpkExpiryDate.Value);
                if (strResult.Contains("success"))
                {
                    return true;
                }
                else
                {
                    if (strResult.Contains("License already exists"))
                        strResult = this.GetResourceTextByKey(1, "MSG_SL_LICENSE_EXISTS");
                    else
                        strResult = this.GetResourceTextByKey(1, "MSG_SL_UNKNOWNERROR");
                    this.ShowWarningMessageBox(strResult,this.Text);
                    dtpkStartDate.Focus();
                    return false;
                }
            }
            catch (Exception exValidateLicenseDates)
            {
                ExceptionManager.Publish(exValidateLicenseDates);
                return false;
            }
        }

        /// <summary>
        /// Set min and max numbers for numeric up and down
        /// </summary>
        private void SetAlterBeforMinMax()
        {
            try
            {
                if (numUDAlertBefor.Enabled)
                {
                    TimeSpan span = dtpkExpiryDate.Value.Subtract(dtpkStartDate.Value);
                    numUDAlertBefor.Minimum = (span.Days > 0) ? 1 : 0;
                    numUDAlertBefor.Maximum = (span.Days < 30) ? span.Days : 30;
                    numUDAlertBefor.Value = (span.Days < 7) ? span.Days : 7;
                }
                else
                {
                    numUDAlertBefor.Minimum = 0;
                    numUDAlertBefor.Maximum = 0;
                    numUDAlertBefor.Value = 0;
                }
            }
            catch (Exception exSetAlterBeforMinMax)
            {
                ExceptionManager.Publish(exSetAlterBeforMinMax);
            }
        }

        private void OpenTextFile(string companyName, string subCompanyName, string siteName, string licenseKey)
        {
            try
            {
                string stmpFileName = string.Empty;
                
                stmpFileName = Path.GetTempFileName();

                String text = this.GetResourceTextByKey("Key_CompanyNameColon") + " " + companyName + "\r\n";               // "Company Name : " 
                text += this.GetResourceTextByKey("Key_SubCompanyNameColon") + " " + subCompanyName + "\r\n";              //  "SubCompany Name : " 
                text += this.GetResourceTextByKey("Key_SiteNameColon") + " " + siteName + "\r\n";                          //  "Site Name : "
                text += this.GetResourceTextByKey("Key_LicenseKeyValue") + " " + licenseKey + "\r\n";                       // "License Key Value : "

                File.AppendAllText(stmpFileName, text);

                Process process = new Process(); 
                
                process.StartInfo.FileName = @"notepad.exe";
                process.StartInfo.Arguments = stmpFileName;
                process.EnableRaisingEvents = true;

                process.Start(); // It will start Notepad process
                process.WaitForExit();

                File.Delete(stmpFileName);
            }
            catch (Exception exResetKeyGeneration)
            {
                ExceptionManager.Publish(exResetKeyGeneration);
            }
        }


        /// <summary>
        /// This Event to restrict the numeric up and down value between maximum and minimum
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        private void TextBoxOnTextChanged(object sender, EventArgs eventArgs)
        {
            try
            {
                if (((TextBox)sender).Text.Trim().Length > 0)
                {
                    decimal newValue = 0;
                    Decimal.TryParse(((TextBox)sender).Text, out newValue);
                    if (newValue > numUDAlertBefor.Maximum || newValue < numUDAlertBefor.Minimum)
                        ((TextBox)sender).Text = _oldValue.ToString();
                }
                else
                {
                    ((TextBox)sender).Text = Convert.ToString(numUDAlertBefor.Minimum);
                    numUDAlertBefor.Select(0, numUDAlertBefor.Text.Length);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        #endregion Key Generation Methods

        #region View/Cancel License

        /// <summary>
        /// To create Company -> SubCompany -> Site tree view
        /// </summary>
        private void CreateNodesinTreeView()
        {
            try
            {
                Int32 iOldSubComp = 0, iOldCompany = 0, iOldSite = 0;
                TreeNode tnAllCompNode = new TreeNode();
                TreeNode firstSite = null;
                TreeNode node = tvSiteList.SelectedNode;
                tvSiteList.Nodes.Clear();

                tnAllCompNode = tvSiteList.Nodes.Add("ALL", this.GetResourceTextByKey("Key_AllCompanies"), 8, 8);
                //tvSiteList.SelectedNode = tnAllCompNode;

                IEnumerable<rsp_SL_GetSitesWithLicenseDetailsResult> getSiteDetailsResults = DataManipulator.GetSitesList(BusinessLogic.iLoginStaffID);

                foreach (rsp_SL_GetSitesWithLicenseDetailsResult getSiteDetailsResult in getSiteDetailsResults)
                {
                    //Add company Node
                    if (iOldCompany != getSiteDetailsResult.Company_ID)
                    {
                        tvSiteList.Nodes["ALL"].Nodes.Add("CO,#" + getSiteDetailsResult.Company_ID.ToString(), getSiteDetailsResult.Company_Name.ToString(), 5, 5);
                        tvSiteList.Nodes["ALL"].ExpandAll();
                        iOldCompany = getSiteDetailsResult.Company_ID;
                    }
                    //Add subcompany if available in record
                    if (iOldSubComp != getSiteDetailsResult.Sub_Company_ID)
                    {
                        //find the parent where this node needs to be added
                        TreeNode tnNode = tvSiteList.Nodes.Find("CO,#" + getSiteDetailsResult.Company_ID.ToString(), true)[0];
                        //if found add the node to parent
                        if (tnNode != null)
                        {
                            tnNode.Nodes.Add("SC,#" + getSiteDetailsResult.Sub_Company_ID.ToString(), getSiteDetailsResult.Sub_Company_Name.ToString(), 6, 6);
                            tnNode.ExpandAll();
                            iOldSubComp = getSiteDetailsResult.Sub_Company_ID;
                        }
                    }
                    //Add site to the respective nodes
                    if (iOldSite != getSiteDetailsResult.Site_ID)
                    {
                        TreeNode tnNode;
                        if (!string.IsNullOrEmpty(getSiteDetailsResult.Sub_Company_ID.ToString()))
                        {
                            tnNode = tvSiteList.Nodes.Find("SC,#" + getSiteDetailsResult.Sub_Company_ID.ToString(), true)[0];
                        }
                        else
                        {
                            tnNode = tvSiteList.Nodes.Find("CO,#" + getSiteDetailsResult.Company_ID.ToString(), true)[0];
                        }
                        if (tnNode != null)
                        {
                            TreeNode tempNode = tnNode.Nodes.Add("SI,#" + getSiteDetailsResult.Site_ID.ToString(), getSiteDetailsResult.Site_Name + "[" + getSiteDetailsResult.Site_Code + "]", 7, 7);

                            tnNode = tvSiteList.Nodes.Find("SI,#" + getSiteDetailsResult.Site_ID.ToString(), true)[0];
                            tnNode.Tag = getSiteDetailsResult.Company_Name + "###" + getSiteDetailsResult.Sub_Company_Name + "###" + getSiteDetailsResult.Site_Name;
                            if (firstSite == null)
                                firstSite = tnNode;
                            int iSiteStatus = 0;
                            iSiteStatus = BusinessLogic.ToInteger(getSiteDetailsResult.SiteStatus);
                            switch (iSiteStatus)
                            {
                                case 1:
                                    tnNode.ImageIndex = 1;
                                    tnNode.SelectedImageIndex = 1;
                                    break;
                                case 2:
                                    tnNode.ImageIndex = 2;
                                    tnNode.SelectedImageIndex = 2;
                                    break;
                                case 3:
                                    tnNode.ImageIndex = 3;
                                    tnNode.SelectedImageIndex = 3;
                                    break;
                                case 4:
                                    tnNode.ImageIndex = 4;
                                    tnNode.SelectedImageIndex = 4;
                                    break;
                            }
                            tnNode.ExpandAll();
                        }
                    }
                    FormatLicenseDetailsGrid();
                }
                tvSiteList.ExpandAll();
                dtgvLicenseDetails.DataSource = null;
                if (node != null)
                {
                    TreeNode node1 = tvSiteList.Nodes.Find(node.Name, true).FirstOrDefault();
                    tvSiteList.SelectedNode = node1;
                }
                else if (firstSite != null)
                {
                    tvSiteList.SelectedNode = firstSite;
                }
            }
            catch (Exception exCreateNodesinTreeView)
            {
                ExceptionManager.Publish(exCreateNodesinTreeView);
            }
        }

        /// <summary>
        /// To set the font setting for License details grid
        /// </summary>
        private void FormatLicenseDetailsGrid()
        {
            try
            {
                Font font = new Font("Verdana", 8, FontStyle.Regular);
                dtgvLicenseDetails.DefaultCellStyle.Font = font;
            }
            catch (Exception exCreateNodesinTreeView)
            {
                ExceptionManager.Publish(exCreateNodesinTreeView);
            }
        }

        /// <summary>
        /// To set the license details grid heder text and its width
        /// </summary>
        private void FormatLicenseDetailsGridHeader()
        {
            try
            {
                String dateFormat = _dateFormater + currentDateCultureFormater.TimeSeparator + "ss";// Regex.Replace(currentDateCultureFormater.ShortDatePattern + " " + currentDateCultureFormater.ShortTimePattern, "/", " ");
                dtgvLicenseDetails.Columns["KeyStatusID"].Visible = false;

                dtgvLicenseDetails.Columns["SiteName"].HeaderText = this.GetResourceTextByKey("Key_SiteName");    //"Site Name";
                dtgvLicenseDetails.Columns["SiteName"].Width = 130;

                dtgvLicenseDetails.Columns["SiteCode"].HeaderText = this.GetResourceTextByKey("Key_SiteCode");    //"Site Code";
                dtgvLicenseDetails.Columns["SiteCode"].Width = 130;

                dtgvLicenseDetails.Columns["Licensekey"].HeaderText = this.GetResourceTextByKey("Key_LicenseKey");    //"License Key";
                dtgvLicenseDetails.Columns["Licensekey"].Width = 140;

                dtgvLicenseDetails.Columns["StartDate"].HeaderText = this.GetResourceTextByKey("Key_StartDateTime");    //"Start Date and Time";
                dtgvLicenseDetails.Columns["StartDate"].Width = 178;
                dtgvLicenseDetails.Columns["StartDate"].DefaultCellStyle.Format = dateFormat;

                dtgvLicenseDetails.Columns["ExpiryDate"].HeaderText = this.GetResourceTextByKey("Key_ExpiryDateTime");    //"Expiry Date and Time";
                dtgvLicenseDetails.Columns["ExpiryDate"].Width = 185;
                dtgvLicenseDetails.Columns["ExpiryDate"].DefaultCellStyle.Format = dateFormat;

                dtgvLicenseDetails.Columns["KeyText"].HeaderText = this.GetResourceTextByKey("Key_KeyStatus");    //"Key Status";
                dtgvLicenseDetails.Columns["KeyText"].Width = 120;

                dtgvLicenseDetails.Columns["ValidationRequired"].HeaderText = this.GetResourceTextByKey("Key_ValidationRequired");    //"Validation Required";
                dtgvLicenseDetails.Columns["ValidationRequired"].Width = 190;

                dtgvLicenseDetails.Columns["LockSite"].HeaderText = this.GetResourceTextByKey("Key_LockSite");    //"Lock Site";
                dtgvLicenseDetails.Columns["LockSite"].Width = 100;

                dtgvLicenseDetails.Columns["DisableGames"].HeaderText = this.GetResourceTextByKey("Key_DisableGames");    //"Disable Games";
                dtgvLicenseDetails.Columns["DisableGames"].Width = 122;

                dtgvLicenseDetails.Columns["WarningOnly"].HeaderText = this.GetResourceTextByKey("Key_WarningOnly");    //"Warning Only";
                dtgvLicenseDetails.Columns["WarningOnly"].Width = 120;

                dtgvLicenseDetails.Columns["AlertRequired"].HeaderText = this.GetResourceTextByKey("Key_AlertRequired");    //"Alert Required";
                dtgvLicenseDetails.Columns["AlertRequired"].Width = 140;

                dtgvLicenseDetails.Columns["AlertBefore"].HeaderText = this.GetResourceTextByKey("Key_AlertBefore");    //"Alert Before (in days)";
                dtgvLicenseDetails.Columns["AlertBefore"].Width = 190;

                dtgvLicenseDetails.Columns["GeneratedBy"].HeaderText = this.GetResourceTextByKey("Key_GeneratedBy");    //"Generated By";
                dtgvLicenseDetails.Columns["GeneratedBy"].Width = 132;

                dtgvLicenseDetails.Columns["GeneratedDateTime"].HeaderText = this.GetResourceTextByKey("Key_GeneratedDateTime");    //"Generated Date and Time";
                dtgvLicenseDetails.Columns["GeneratedDateTime"].Width = 215;
                dtgvLicenseDetails.Columns["GeneratedDateTime"].DefaultCellStyle.Format = dateFormat;

                dtgvLicenseDetails.Columns["ActivatedBy"].HeaderText = this.GetResourceTextByKey("Key_ActivatedBy");    //"Activated By";
                dtgvLicenseDetails.Columns["ActivatedBy"].Width = 132;

                dtgvLicenseDetails.Columns["ActivatedDateTime"].HeaderText = this.GetResourceTextByKey("Key_ActivatedDateTime");    //"Activated Date and Time";
                dtgvLicenseDetails.Columns["ActivatedDateTime"].Width = 215;
                dtgvLicenseDetails.Columns["ActivatedDateTime"].DefaultCellStyle.Format = dateFormat;

                dtgvLicenseDetails.Columns["CancelledBy"].HeaderText = this.GetResourceTextByKey("Key_CancelledBy");    //"Cancelled By";
                dtgvLicenseDetails.Columns["CancelledBy"].Width = 132;

                dtgvLicenseDetails.Columns["CancelledDateTime"].HeaderText = this.GetResourceTextByKey("Key_CancelledDateTime");    //"Cancelled Date and Time";
                dtgvLicenseDetails.Columns["CancelledDateTime"].Width = 215;
                dtgvLicenseDetails.Columns["CancelledDateTime"].DefaultCellStyle.Format = dateFormat;
            }
            catch (Exception exFormatLicenseDetailsGridHeader)
            {
                ExceptionManager.Publish(exFormatLicenseDetailsGridHeader);
            }
        }

        ///// <summary>
        ///// Hide Site List control for Expand
        ///// </summary>
        //private void HideSiteList()
        //{
        //    try
        //    {
        //        sC_ViewCancelLicense.Panel1Collapsed = true;
        //        sC_ViewCancelLicense.Panel1.Hide();
        //        //grpSiteList.Visible = false;
        //        btnHideSiteList.Text = ">>";
        //        pnlLicenseDetails.Location = new Point(0, 0);
        //        pnlLicenseDetails.Size = new Size(656, pnlLicenseDetails.Size.Height);
        //        grpLicenseDetails.Size = new Size(pnlLicenseDetails.Size.Width - 12, grpLicenseDetails.Size.Height);
        //        dtgvLicenseDetails.Size = new Size(grpLicenseDetails.Size.Width - 13, dtgvLicenseDetails.Size.Height);
        //        btnCancelLicense.Location = new Point((pnlLicenseDetails.Size.Width / 2) - (btnCancelLicense.Size.Width / 2), btnCancelLicense.Location.Y);
        //    }
        //    catch (Exception ex)
        //    {
        //        ExceptionManager.Publish(ex);
        //    }
        //}

        ///// <summary>
        ///// Show site list control for collapse
        ///// </summary>
        //private void ShowSiteList()
        //{
        //    //grpSiteList.Visible = true;
        //    try
        //    {
        //        sC_ViewCancelLicense.Panel1Collapsed = false;
        //        sC_ViewCancelLicense.Panel1.Show();
        //        btnHideSiteList.Text = "<<";
        //        pnlLicenseDetails.Location = new Point(173, 3);
        //        pnlLicenseDetails.Size = new Size(485, pnlLicenseDetails.Size.Height);
        //        grpLicenseDetails.Size = new Size(pnlLicenseDetails.Size.Width - 12, grpLicenseDetails.Size.Height);
        //        dtgvLicenseDetails.Size = new Size(grpLicenseDetails.Size.Width - 13, dtgvLicenseDetails.Size.Height);
        //        btnCancelLicense.Location = new Point((pnlLicenseDetails.Size.Width / 2) - (btnCancelLicense.Size.Width / 2), btnCancelLicense.Location.Y);

        //    }
        //    catch (Exception ex)
        //    {
        //        ExceptionManager.Publish(ex);
        //    }
        //}

        #endregion View/Cancel License

        #region License History Methods

        /// <summary>
        /// when user enter license history tab, to load the combo values
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnPgLicenseHistoryEnter()
        {
            try
            {
                FillCombo(cmbSCCompanyName, BusinessLogic.GetList("Company", 0, allCapsText), "Company_ID", "Company_Name");
                FillCombo(cmbSCSubCompanyName, BusinessLogic.GetList("SubCompany", 0, allCapsText), "Sub_Company_ID", "Sub_Company_Name");
                FillCombo(cmbSCSiteName, BusinessLogic.GetList("SiteSC", 0, allCapsText), "Site_ID", "Site_Name");
                FillCombo(cmbSCKeyStatus, BusinessLogic.GetList("KeyStatus", 0, allCapsText), "KeyStatusID", "KeyStatus");
                ResetSearchControls();
                dtGVSearchResults.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
                dtGVSearchResults.AllowUserToResizeRows = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// To set the font setting for search results grid
        /// </summary>
        private void FormatSearchresultsGrid()
        {
            try
            {
                Font font = new Font("Verdana", 8, FontStyle.Regular);
                dtGVSearchResults.DefaultCellStyle.Font = font;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// To set the search results grid heder text and its width
        /// </summary>
        private void FormatSearchresultsGridHeader()
        {
            try
            {
                String dateFormat = _dateFormater + currentDateCultureFormater.TimeSeparator + "ss";// Regex.Replace(currentDateCultureFormater.ShortDatePattern + " " + currentDateCultureFormater.ShortTimePattern, "/", " ");

                dtGVSearchResults.Columns["CompanyName"].Tag = "Key_CompanyName";   //"Company Name";
                dtGVSearchResults.Columns["CompanyName"].Width = 148;

                dtGVSearchResults.Columns["SubCompanyName"].Tag = "Key_SubCompanyName";    //"Sub Company Name";
                dtGVSearchResults.Columns["SubCompanyName"].Width = 180;

                dtGVSearchResults.Columns["SiteName"].Tag = "Key_SiteName";    //"Site Name";
                dtGVSearchResults.Columns["SiteName"].Width = 130;

                dtGVSearchResults.Columns["SiteCode"].Tag = "Key_SiteCode";    //"Site Code";
                dtGVSearchResults.Columns["SiteCode"].Width = 130;

                dtGVSearchResults.Columns["Licensekey"].Tag = "Key_LicenseKey";    //"License Key";
                dtGVSearchResults.Columns["Licensekey"].Width = 130;

                dtGVSearchResults.Columns["LicenseInfoID"].Visible = false;
                dtGVSearchResults.Columns["KeyStatusID"].Visible = false;

                dtGVSearchResults.Columns["StartDate"].Tag = "Key_StartDateTime";    //"Start Date and Time";
                dtGVSearchResults.Columns["StartDate"].Width = 170;
                dtGVSearchResults.Columns["StartDate"].DefaultCellStyle.Format = dateFormat;

                dtGVSearchResults.Columns["ExpireDate"].Tag = "Key_ExpiryDateTime";    //"Expiry Date and Time";
                dtGVSearchResults.Columns["ExpireDate"].Width = 175;
                dtGVSearchResults.Columns["ExpireDate"].DefaultCellStyle.Format = dateFormat;

                dtGVSearchResults.Columns["KeyText"].Tag = "Key_KeyStatus";    //"Key Status";
                dtGVSearchResults.Columns["KeyText"].Width = 120;

                dtGVSearchResults.Columns["RuleName"].Tag = "Key_RuleName";    //"Rule Name";
                dtGVSearchResults.Columns["RuleName"].Width = 120;

                dtGVSearchResults.Columns["ValidationRequired"].Tag = "Key_ValidationRequired";    //"Validation Required";
                dtGVSearchResults.Columns["ValidationRequired"].Width = 190;

                dtGVSearchResults.Columns["LockSite"].Tag = "Key_LockSite";    //"Lock Site";
                dtGVSearchResults.Columns["LockSite"].Width = 100;

                dtGVSearchResults.Columns["DisableGames"].Tag = "Key_DisableGames";    //"Disable Games";
                dtGVSearchResults.Columns["DisableGames"].Width = 120;

                dtGVSearchResults.Columns["WarningOnly"].Tag = "Key_WarningOnly";    //"Warning Only";
                dtGVSearchResults.Columns["WarningOnly"].Width = 120;

                dtGVSearchResults.Columns["AlertRequired"].Tag = "Key_AlertRequired";    //"Alert Required";
                dtGVSearchResults.Columns["AlertRequired"].Width = 140;

                dtGVSearchResults.Columns["AlertBefore"].Tag = "Key_AlertBefore";    //"Alert Before (in days)";
                dtGVSearchResults.Columns["AlertBefore"].Width = 190;

                dtGVSearchResults.Columns["GeneratedBy"].Tag = "Key_GeneratedBy";    // "Generated By";
                dtGVSearchResults.Columns["GeneratedBy"].Width = 132;

                dtGVSearchResults.Columns["GeneratedDateTime"].Tag = "Key_GeneratedDateTime";    //"Generated Date and Time";
                dtGVSearchResults.Columns["GeneratedDateTime"].Width = 215;
                dtGVSearchResults.Columns["GeneratedDateTime"].DefaultCellStyle.Format = dateFormat;

                dtGVSearchResults.Columns["ActivatedBy"].Tag = "Key_ActivatedBy";    //"Activated By";
                dtGVSearchResults.Columns["ActivatedBy"].Width = 132;

                dtGVSearchResults.Columns["ActivatedDateTime"].Tag = "Key_ActivatedDateTime";    //"Activated Date and Time";
                dtGVSearchResults.Columns["ActivatedDateTime"].Width = 215;
                dtGVSearchResults.Columns["ActivatedDateTime"].DefaultCellStyle.Format = dateFormat;


                dtGVSearchResults.Columns["CancelledBy"].Tag = "Key_CancelledBy";    //"Cancelled By";
                dtGVSearchResults.Columns["CancelledBy"].Width = 132;

                dtGVSearchResults.Columns["CancelledDateTime"].Tag = "Key_CancelledDateTime";    //"Cancelled Date and Time";
                dtGVSearchResults.Columns["CancelledDateTime"].Width = 215;
                dtGVSearchResults.Columns["CancelledDateTime"].DefaultCellStyle.Format = dateFormat;

                dtGVSearchResults.ResolveResources();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// assign the SP output results to the combo value and display members
        /// </summary>
        /// <param name="comboBox"></param>
        /// <param name="comboValueDictionary"></param>
        /// <param name="strValueMember"></param>
        /// <param name="strDisplayMember"></param>
        private void FillCombo(ComboBox comboBox, Dictionary<Int32, string> comboValueDictionary, string strValueMember, string strDisplayMember)
        {
            try
            {
                comboBox.DataSource = null;
                comboBox.ValueMember = "Key";
                comboBox.DisplayMember = "Value";
                comboBox.DataSource = new BindingSource(comboValueDictionary, null);
                comboBox.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// To reset search parameter controld
        /// </summary>
        private void ResetSearchControls()
        {
            try
            {
                cmbSCCompanyName.SelectedIndex = 0;
                cmbSCSubCompanyName.SelectedIndex = 0;
                cmbSCSiteName.SelectedIndex = 0;
                cmbSCKeyStatus.SelectedIndex = 0;
                cmbSCValidationReq.SelectedIndex = 0;
                cmbSCLockSite.SelectedIndex = 0;
                cmbSCDisableEGM.SelectedIndex = 0;
                cmbSCWarningOnly.SelectedIndex = 0;
                cmbSCAlertRequired.SelectedIndex = 0;
                dtpkFromStartDate.Value = DateTime.Now;
                dtpkToStartDate.Value = DateTime.Now.AddDays(7);
                dtpkFromExpiryDate.Value = DateTime.Now;
                dtpkToExpiryDate.Value = DateTime.Now.AddDays(14);
                dtGVSearchResults.DataSource = null;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// To get true or false value based on the combo selection
        /// </summary>
        /// <param name="comboBox"></param>
        /// <returns></returns>
        private bool? GetBooleanComboValue(ComboBox comboBox)
        {
            try
            {
                switch (comboBox.SelectedIndex)
                {
                    case 1:
                        return true;
                    case 2:
                        return false;
                    default:
                        return null;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return null;
            }
        }

        #endregion //License History Methods

        private void sC_ViewCancelLicense_Panel2_Resize(object sender, EventArgs e)
        {
            btnCancelLicense.Location = new Point((sC_ViewCancelLicense.Panel2.Width / 2) - (btnCancelLicense.Size.Width / 2), btnCancelLicense.Location.Y);
        }

        #endregion //Custom Methods


        private void btn_Refresh_Click(object sender, EventArgs e)
        {
            try
            {
                
                LoadPage();
                LoadUserDetails();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (tvSiteList.SelectedNode == null) return;
                string[] names = Regex.Split(Convert.ToString(tvSiteList.SelectedNode.Tag), "###");
                if (names.Count() == 3)
                    OpenTextFile(names[0], names[1], names[2], licenseDetailsResultList[ilicenseDetailsSelectedIndex].Licensekey);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void tvSiteList_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            tvSiteList.SelectedNode = e.Node;
        }

        private void tabPgKeyGeneration_Enter(object sender, EventArgs e)
        {
            try
            {


            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }




        //
        #region start User based Search

        private void LoadUserDetails()
        {

            try
            {
                FillCombo(cmbActivatedBy, BusinessLogic.GetList("user", 0, allText), "Staff_ID", "Staff_Name");

                FillCombo(cmbCancelBy, BusinessLogic.GetList("user", 0, allText), "Staff_ID", "Staff_Name");

                FillCombo(cmbcreateBy, BusinessLogic.GetList("user", 0, allText), "Staff_ID", "Staff_Name");
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

        }

        #endregion  End of User based Search
        //
        private void grpSCValidationParam_Enter(object sender, EventArgs e)
        {

        }

        private void grpSCValidationParam_Enter_1(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel7_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
