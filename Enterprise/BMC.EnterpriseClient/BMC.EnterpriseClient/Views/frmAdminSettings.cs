using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.EnterpriseBusiness.Business;
using BMC.EnterpriseBusiness.Entities;
using BMC.Common.ExceptionManagement;
using BMC.CoreLib.Win32;
using System.Text.RegularExpressions;
using BMC.Common;

namespace BMC.EnterpriseClient.Views
{
    public partial class frmAdminSettings : Form
    {

         #region Local Variables
        private AdminSettings _AdminSettings = null;
        private AdminSystemSettingsResult objAdminSystemSettingsResult = null;
        #endregion
        string EnrolmentFlagIndex = string.Empty;
        int EnrolmentFlagValueResult = 0;
        int i;
        public bool bUpdatePermission = AppGlobals.Current.HasUserAccess("HQ_Admin_Settings_Edit");
        #region Constructor
        BMC.EnterpriseClient.Helpers.Datawatcher ObjDatawatcher = null;
        TabControl obj = new TabControl();
        public frmAdminSettings()
        {
            try
            {
                InitializeComponent();
                setTagProperty();
                _AdminSettings = new AdminSettings();
                LoadScreen();
                LoadControl();

                ObjDatawatcher = new Helpers.Datawatcher(this);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void setTagProperty()
        {
            this.chkAddSPinVout.Tag = "Key_AddShortpayinVoucherOut";
            this.grpAGSCombination.Tag = "Key_AGSCombination";
            this.lblAllowBulkPurchase.Tag = "Key_Allowbulkpurchaseofassetitems";
            this.chkOfflineDeclaration.Tag = "Key_AllowOfflineDeclaration";
            this.chkAllowBPEnable.Tag = "Key_AllowEnableDisableBarPosition";
            this.lblAssetNoLen.Tag = "Key_AssetNumberMinLength";
            this.lblAssetNoPrefix.Tag = "Key_AssetNumberPrefix";
            this.grpAssetSettings.Tag = "Key_AssetSettings";
            this.lblAutoGenAssetNo.Tag = "Key_AutoGenerateAssetNumber";
            this.lblAutoGenGameCode.Tag = "Key_AutoGenerateGameCodes";
            this.btnCancel.Tag = "Key_Cancel";
            this.chkCentralizedDeclaration.Tag = "Key_CentralizedDeclaration";
            this.btnEdit.Tag = "Key_EditCaption";
            this.lblEnforceCodes.Tag = "Key_EnforceCodesToSite";
            this.lblForceAsset.Tag = "Key_ForceAssetitemstohaveasiterep";
            this.chkGameCappingEnabled.Tag = "Key_GameCappingEnabled";
            this.lblGameCodeLen.Tag = "Key_GameCodeMinLength";
            this.lblGameCodePrefix.Tag = "Key_GameCodePrefixColon";
            this.chkImportExportAssetFile.Tag = "Key_ImportExportAssetFile";
            this.chkEmployeecardTrackingRequired.Tag = "Key_IsEmployeeCardTrackingEnabled";
            this.chkIsSingleEmpCard.Tag = "Key_IsSingleCardEmployee";
            this.chkPowerPomoReportsRequired.Tag = "Key_PowerPromoReports";
            this.lblRegion.Tag = "Key_RegionColon";
            this.chkSiteLicensingRequired.Tag = "Key_SiteLicensingEnabled";
            this.chkSuppressGroupByZone.Tag = "Key_SuppressGroupByZone";
            this.Tag = "Key_SystemSettings";
            this.lblUnallocatedGame.Tag = "Key_UnallocatedGameColon";
            this.lblUnallocatedMachine.Tag = "Key_UnallocatedMachineColon";
            this.lblUnallocatedType.Tag = "Key_UnallocatedTypeColon";
            this.btnUpdate.Tag = "Key_UpdateCaption";
            this.chkValidateAGS.Tag = "Key_ValidateAGSForGMUNoUpdation";
            this.chkPartiallyConfigured.Tag = "Key_ViewSitesPartiallyConfigured";
            this.chkIsAlertEnabled.Tag = "Key_IsAlertEnabled";
            this.chkMailAlert.Tag = "Key_MailAlert";
            this.cb_SendMailFromEnterprise.Tag = "Key_SendMailFromEnterprise";
            this.chkCancelPendingMails.Tag="Key_CancelPendingmails";
            this.cb_IsAutoCalendarEnabled.Tag = "Key_IsAutoCalendarEnabled";
            this.gp_VaultSettings.Tag = "Key_VaultSettings";
            this.btnEditTransactionReason.Tag = "Key_EditTransactionReason";
        }
        #endregion

        #region Events
        #region Stock Tab

        public void LoadControl()
        {
            try
            {
                string AGSValue = string.Empty;

                chkSerial.Text = this.GetResourceTextByKey("Key_Serial");
                chkSerial.Tag = 4;
                chkAsset.Text = this.GetResourceTextByKey("Key_Asset");
                chkAsset.Tag = 8;
                chkGMU.Text = this.GetResourceTextByKey("Key_GMU");
                chkGMU.Tag = 16;

                if (chkSerial.Checked)
                {
                    chkSerial.Checked = false;
                }
                if (chkAsset.Checked)
                {
                    chkAsset.Checked = false;
                }
                if (chkGMU.Checked)
                {
                    chkGMU.Checked = false;
                }

                string strIsEnrolmentComplete = AdminBusiness.GetSetting("IsEnrolmentComplete", string.Empty);
                if (strIsEnrolmentComplete.Trim().ToUpper() == "TRUE")
                {
                    chkSerial.Enabled = false;
                    chkAsset.Enabled = false;
                    chkGMU.Enabled = false;
                    // btnEdit.Enabled = false;
                }
                else
                {
                    chkSerial.Enabled = true;
                    chkAsset.Enabled = true;
                    chkGMU.Enabled = true;
                }

                // Load Enrolment Flags

                AGSValue = AdminBusiness.GetSetting("AGSValue", "0");
                switch (AGSValue)
                {
                    case "4":
                        chkSerial.Checked = true;
                        chkAsset.Checked = false;
                        chkGMU.Checked = false;
                        break;
                    case "8":
                        chkSerial.Checked = false;
                        chkAsset.Checked = true;
                        chkGMU.Checked = false;
                        break;
                    case "16":
                        chkSerial.Checked = false;
                        chkAsset.Checked = false;
                        chkGMU.Checked = true;
                        break;
                    case "12":
                        chkSerial.Checked = true;
                        chkAsset.Checked = true;
                        chkGMU.Checked = false;
                        break;
                    case "20":
                        chkSerial.Checked = true;
                        chkAsset.Checked = false;
                        chkGMU.Checked = true;
                        break;
                    case "24":
                        chkSerial.Checked = false;
                        chkAsset.Checked = true;
                        chkGMU.Checked = true;
                        break;
                    case "28":
                        chkSerial.Checked = true;
                        chkAsset.Checked = true;
                        chkGMU.Checked = true;
                        break;
                    default:
                        break;
                }
                ObjDatawatcher.DataModify = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        private void UpdateAGSCombination()
        {
            try
            {
                if (objAdminSystemSettingsResult.SystemSettings.IsEnrolmentComplete == true)
                {
                    grpAGSCombination.Enabled = false;
                }
                else
                {
                    grpAGSCombination.Enabled = true;
                }

                if (chkSerial.Checked)
                {
                    EnrolmentFlagValueResult = EnrolmentFlagValueResult | Convert.ToInt32(chkSerial.Tag);
                }
                if (chkAsset.Checked)
                {
                    EnrolmentFlagValueResult = EnrolmentFlagValueResult | Convert.ToInt32(chkAsset.Tag);
                }
                if (chkGMU.Checked)
                {
                    EnrolmentFlagValueResult = EnrolmentFlagValueResult | Convert.ToInt32(chkGMU.Tag);
                }

                if (EnrolmentFlagValueResult == 0)
                {
                    this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_ENT_ADMIN_SETTINGS_AGS"), this.GetResourceTextByKey(1,"MSG_AGSCOMBINATION_TITLE"));
                    return;
                }
                else
                {
                    string SettingValue = "True";
                    AGSBusiness objAGSBusiness = new AGSBusiness();
                    int res = objAGSBusiness.InsertOrUpdateAGSSetting(EnrolmentFlagValueResult.ToString());
                    int res1 = objAGSBusiness.InsertOrUpdateSetting("IsEnrolmentComplete", SettingValue);
                }

                // this.Close();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

        }
        private void cmbUnallocatedType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbUnallocatedType.SelectedIndex >= 0)
                {

                    List<MachineClassInfoEntity> _MachineClassInfoEntity = _AdminSettings.GetMachineClassInfo(Convert.ToInt32(cmbUnallocatedType.SelectedValue));
                    cmbUnallocatedGame.DisplayMember = "Machine_Name";
                    cmbUnallocatedGame.ValueMember = "Machine_Class_ID";
                    cmbUnallocatedGame.SelectedIndex = -1;
                    cmbUnallocatedGame.DataSource = _MachineClassInfoEntity;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void cmbUnallocatedGame_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbUnallocatedGame.SelectedIndex >= 0)
                {
                    List<MachineInfoEntity> _MachineInfoEntity = _AdminSettings.GetMachineInfo(Convert.ToInt32(cmbUnallocatedGame.SelectedValue));
                    cmbUnallocatedMachine.DisplayMember = "Machine_Stock_No";
                    cmbUnallocatedMachine.ValueMember = "Machine_ID";
                    cmbUnallocatedMachine.SelectedIndex = -1;
                    cmbUnallocatedMachine.DataSource = _MachineInfoEntity;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void chkAutoGenGameCode_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                txtGameCodePrefix.Enabled = chkAutoGenGameCode.Checked;
                txtGameCodeMinLength.Enabled = chkAutoGenGameCode.Checked;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void chkAutoGenAssetNumber_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                txtAssetNumberMinLength.Enabled = chkAutoGenAssetNumber.Checked;
                txtAssetNumberPrefix.Enabled = chkAutoGenAssetNumber.Checked;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        #region Others Tab
        private void chkCentralizedDeclaration_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                chkOfflineDeclaration.Enabled = chkCentralizedDeclaration.Checked;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void chkSiteLicensingRequired_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (!chkSiteLicensingRequired.Checked && Convert.ToBoolean(objAdminSystemSettingsResult.SystemSettings.IsSiteLicensingEnabled))
                {
                    if (this.ShowQuestionMessageBox(this.GetResourceTextByKey(1, "MSG_ENT_ADMIN_SETTINGS_SITE"), this.Text) == DialogResult.No)
                        chkSiteLicensingRequired.Checked = true;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        #endregion

        #endregion

        #region Common


        private void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                LoadScreen();
                ObjDatawatcher.DataModify = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                int AssetNumberMinLength = 0;
                int PrefixLength = 0;
				string strAllowedChar = "[^a-zA-Z0-9]";
                if (!string.IsNullOrEmpty(txtAssetNumberMinLength.Text.Trim()))
                {
                    AssetNumberMinLength = Convert.ToInt32(txtAssetNumberMinLength.Text.Trim());
                }
                else
                {
                    //validation is done for asset minimum length field should not be empty
                    this.ShowWarningMessageBox(this.GetResourceTextByKey(1, "MSG_ADMIN_SETTINGS_ASSETMINLENGTH_EMPTY"), this.Text);
                    txtAssetNumberMinLength.Focus();
                    return;
                }
                if(string.IsNullOrEmpty(txtGameCodeMinLength.Text.Trim()))
                {
                    //validation is done for game code minimum length field should not be empty
                    this.ShowWarningMessageBox(this.GetResourceTextByKey(1, "MSG_ADMIN_SETTINGS_GAMECODEMINLENGTH_EMPTY"), this.Text);
                    txtGameCodeMinLength.Focus();
                    return;
                }

                if (!string.IsNullOrEmpty(txtAssetNumberPrefix.Text.Trim()))
                    PrefixLength = Convert.ToInt32(txtAssetNumberPrefix.Text.Length);
                if (AssetNumberMinLength > 10)
                {
                    this.ShowWarningMessageBox(this.GetResourceTextByKey(1, "MSG_ENT_ADMIN_SETTINGS_ASSET"), this.Text);
                    txtAssetNumberMinLength.Focus();
                    return;
                }
                if (PrefixLength > AssetNumberMinLength)
                {
                    this.ShowWarningMessageBox(this.GetResourceTextByKey(1, "MSG_ENT_ADMIN_SETTINGS_LENGTH"), this.Text);
                    txtAssetNumberPrefix.Focus();
                    return;
                }
                if (new Regex(strAllowedChar).IsMatch(txtAssetNumberPrefix.Text.Trim()))
                {
                    this.ShowWarningMessageBox(this.GetResourceTextByKey(1, "MSG_ENT_ADMIN_SETTINGS_PREFIX"), this.Text);
                    txtAssetNumberPrefix.Text = "";
                    txtAssetNumberPrefix.Focus();
                    return;
                }
                SystemSettingsEntity modifiedEntity = new SystemSettingsEntity();
                GetValueFromScreen(modifiedEntity);
                if (_AdminSettings.UpdateSystemSettings(modifiedEntity, objAdminSystemSettingsResult.SystemSettings, AppEntryPoint.Current.UserId, AppEntryPoint.Current.UserName))
                {
                    this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_ENT_ADMIN_SETTINGS_SUCCESS"), this.Text);
                    LoadScreen();
                    UpdateAGSCombination();
                }
                else
                {
                    this.ShowWarningMessageBox(this.GetResourceTextByKey(1, "MSG_ENT_ADMIN_SETTINGS_FAILURE"), this.Text);
                }

                ObjDatawatcher.DataModify = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_ENT_ADMIN_SETTINGS_SAVE_CHANGES"), this.Text);
                EnableEditButton(false);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        #endregion

        #endregion

        #region Methods

        #region Common
        private void EnableEditButton(bool value)
        {
            try
            {
                btnUpdate.Visible = bUpdatePermission;
                btnUpdate.Visible = !value;
                btnCancel.Visible = !value;
                btnEdit.Visible = (bUpdatePermission && value);
                grpOtherSettings.Enabled = !value;
                grpAGSCombination.Enabled = !value;
                grpAssetSettings.Enabled = !value;
                gp_VaultSettings.Enabled = !value;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void LoadScreen()
        {
            try
            {

                gp_VaultSettings.Visible = SettingsEntity.IsVaultEnabled;
                objAdminSystemSettingsResult = _AdminSettings.GetSystemSettingDetails();
                EnableEditButton(true);
                chkCentralizedDeclaration.Checked = !chkCentralizedDeclaration.Checked;
                chkAutoGenGameCode.Checked = !chkAutoGenGameCode.Checked;
                chkAutoGenAssetNumber.Checked = !chkAutoGenAssetNumber.Checked;
                if (objAdminSystemSettingsResult != null)
                {
                    #region Add/Remove Display Tab and Services Tab Based on the settings
                    chkAutoGenGameCode.Checked = Convert.ToBoolean(objAdminSystemSettingsResult.SystemSettings.AutoGenerateModelCode);
                    #endregion

                    #region Stock Tab
                    #region AGSCombination
                    if (AppGlobals.Current.HasUserAccess("HQ_Admin_AGSCombination") == true)
                    {
                        if (objAdminSystemSettingsResult.SystemSettings.IsEnrolmentFlag == false)
                        {
                            grpAGSCombination.Visible = false;
                            tblInner.SetRowSpan(grpOtherSettings, 2);
                        }
                    }
                    if (AppGlobals.Current.HasUserAccess("HQ_Admin_AGSCombination") == false)
                    {
                        grpAGSCombination.Visible = false;
                        tblInner.SetRowSpan(grpOtherSettings, 2);
                    }
                    #endregion
                    #region Text and Check Boxes
                    txtGameCodePrefix.Text = objAdminSystemSettingsResult.SystemSettings.ModelCodePrefix;
                    txtGameCodeMinLength.Text = Convert.ToString(objAdminSystemSettingsResult.SystemSettings.ModelCodeMinLength);
                    //chkForceCodeToSite.Checked = Convert.ToBoolean(objAdminSystemSettingsResult.SystemSettings.System_Parameter_Enforce_Masks_To_Site)
                    chkForceSiteRepforAsset.Checked = Convert.ToBoolean(objAdminSystemSettingsResult.SystemSettings.ForceSiteRepsOnStock);
                    chkAllowBulkAssetPurchase.Checked = Convert.ToBoolean(objAdminSystemSettingsResult.SystemSettings.AllowStockBulkPurchase);
                    chkAutoGenAssetNumber.Checked = Convert.ToBoolean(objAdminSystemSettingsResult.SystemSettings.AutoGenerateStockCode);
                    txtAssetNumberMinLength.Text = Convert.ToString(objAdminSystemSettingsResult.SystemSettings.StockCodeMinLength);
                    txtAssetNumberPrefix.Text = objAdminSystemSettingsResult.SystemSettings.StockCodePrefix;
                    #endregion

                    #region ComboBox
                    cmbUnallocatedType.SelectedIndex = -1;
                    cmbUnallocatedType.DisplayMember = "Machine_Type_Code";
                    cmbUnallocatedType.ValueMember = "Machine_Type_ID";
                    cmbUnallocatedType.DataSource = objAdminSystemSettingsResult.MachineTypeInfoEntities;


                    if (objAdminSystemSettingsResult.SystemSettings.Machine_ID > 0)
                    {
                        cmbUnallocatedType.SelectedValue = objAdminSystemSettingsResult.SystemSettings.Machine_Type_ID;
                        cmbUnallocatedGame.SelectedValue = objAdminSystemSettingsResult.SystemSettings.Machine_Class_ID;
                        cmbUnallocatedMachine.SelectedValue = objAdminSystemSettingsResult.SystemSettings.Machine_ID;
                    }

                    #endregion
                    #endregion

                    #region Others Tab
                    #region Text and Check Boxes
                    chkPowerPomoReportsRequired.Checked = Convert.ToBoolean(objAdminSystemSettingsResult.SystemSettings.IsPowerPromoReportsRequired);
                    chkCentralizedDeclaration.Checked = Convert.ToBoolean(objAdminSystemSettingsResult.SystemSettings.CentralizedDeclaration);
                    chkOfflineDeclaration.Checked = Convert.ToBoolean(objAdminSystemSettingsResult.SystemSettings.AllowOfflineDeclaration);
                    chkAddSPinVout.Checked = Convert.ToBoolean(objAdminSystemSettingsResult.SystemSettings.AddShortpayInVoucherOut);
                    chkSiteLicensingRequired.Checked = Convert.ToBoolean(objAdminSystemSettingsResult.SystemSettings.IsSiteLicensingEnabled);
                    chkImportExportAssetFile.Checked = Convert.ToBoolean(objAdminSystemSettingsResult.SystemSettings.ImportExportAssetFile);
                    chkEmployeecardTrackingRequired.Checked = Convert.ToBoolean(objAdminSystemSettingsResult.SystemSettings.IsEmployeecardTrackingEnabled);
                    chkValidateAGS.Checked = Convert.ToBoolean(objAdminSystemSettingsResult.SystemSettings.ValidateAGSForGMU);
                    chkPartiallyConfigured.Checked = Convert.ToBoolean(objAdminSystemSettingsResult.SystemSettings.IsSitesPartiallyConfiguredEnabled);
                    chkGameCappingEnabled.Checked = Convert.ToBoolean(objAdminSystemSettingsResult.SystemSettings.IsGameCappingEnabled);
                    chkSuppressGroupByZone.Checked = Convert.ToBoolean(objAdminSystemSettingsResult.SystemSettings.IsSuppressZoneEnabled);
                    chkIsSingleEmpCard.Checked = Convert.ToBoolean(objAdminSystemSettingsResult.SystemSettings.IsSingleCardEmployee);
                    chkAllowBPEnable.Checked = Convert.ToBoolean(objAdminSystemSettingsResult.SystemSettings.AllowEnableDisableBarPosition);
                    chkIsAlertEnabled.Checked = Convert.ToBoolean(objAdminSystemSettingsResult.SystemSettings.IsAlertEnabled);
                    chkMailAlert.Checked = Convert.ToBoolean(objAdminSystemSettingsResult.SystemSettings.IsEmailAlertEnabled);
                    cb_IsAutoCalendarEnabled.Checked = Convert.ToBoolean(objAdminSystemSettingsResult.SystemSettings.IsAutoCalendarEnabled);
                    cb_SendMailFromEnterprise.Checked = Convert.ToBoolean(objAdminSystemSettingsResult.SystemSettings.SendMailFromEnterprise);
                    chkCancelPendingMails.Checked = Convert.ToBoolean(objAdminSystemSettingsResult.SystemSettings.CancelPendingMails);
                    chkAllowMultipleDrops.Checked = Convert.ToBoolean(objAdminSystemSettingsResult.SystemSettings.AllowMutltipleDrops);
                    #endregion
                    #region ComboBox
                    cmbCulture.DataSource = objAdminSystemSettingsResult.CultureInfoEntities;
                    cmbCulture.ValueMember = "CultureInfo";
                    cmbCulture.DisplayMember = "CultureInfo";
                    cmbCulture.SelectedValue = objAdminSystemSettingsResult.SystemSettings.RegionCulture;
                    #endregion
                    #endregion
                }
                ObjDatawatcher.DataModify = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void GetValueFromScreen(SystemSettingsEntity modifiedEntity)
        {
            try
            {
                #region TabVisibility
                modifiedEntity.SystemSettingsDisplayTabVisible = objAdminSystemSettingsResult.SystemSettings.SystemSettingsDisplayTabVisible;
                modifiedEntity.SystemSettingsServiceTabVisible = objAdminSystemSettingsResult.SystemSettings.SystemSettingsServiceTabVisible;
                #endregion
                #region Display Tab
                modifiedEntity.AutoGenerateModelCode = chkAutoGenGameCode.Checked;
                #endregion

                #region Stock Tab

                #region Text and Check Boxes
                modifiedEntity.ModelCodePrefix = txtGameCodePrefix.Text.Trim();
                modifiedEntity.ModelCodeMinLength = Convert.ToInt32(txtGameCodeMinLength.Text.Trim());
                //chkForceCodeToSite.Checked = Convert.ToBoolean(modifiedEntity.System_Parameter_Enforce_Masks_To_Site)
                modifiedEntity.ForceSiteRepsOnStock = chkForceSiteRepforAsset.Checked;
                modifiedEntity.AllowStockBulkPurchase = chkAllowBulkAssetPurchase.Checked;
                modifiedEntity.AutoGenerateStockCode = chkAutoGenAssetNumber.Checked;
                if (!String.IsNullOrEmpty(txtAssetNumberMinLength.Text.Trim()))
                {
                    modifiedEntity.StockCodeMinLength = Convert.ToInt32(txtAssetNumberMinLength.Text.Trim());
                }
                else
                {
                    modifiedEntity.StockCodeMinLength = 0;
                }
                modifiedEntity.StockCodePrefix = txtAssetNumberPrefix.Text.Trim();
                #endregion

                #region ComboBox
                if (objAdminSystemSettingsResult.SystemSettings.Machine_ID > 0)
                {
                    modifiedEntity.Machine_Type_ID = Convert.ToInt32(cmbUnallocatedType.SelectedValue);
                    modifiedEntity.Machine_Class_ID = Convert.ToInt32(cmbUnallocatedGame.SelectedValue);
                    modifiedEntity.Machine_ID = Convert.ToInt32(cmbUnallocatedMachine.SelectedValue);
                }

                #endregion
                #endregion

                #region AGS
                modifiedEntity.IsEnrolmentComplete = true;
                modifiedEntity.IsEnrolmentFlag = true;
                #endregion

                #region Others Tab
                #region Text and Check Boxes
                modifiedEntity.IsPowerPromoReportsRequired = chkPowerPomoReportsRequired.Checked;
                modifiedEntity.CentralizedDeclaration = chkCentralizedDeclaration.Checked;
                modifiedEntity.ImportExportAssetFile = chkImportExportAssetFile.Checked;
                modifiedEntity.AllowOfflineDeclaration = chkOfflineDeclaration.Checked;
                modifiedEntity.AddShortpayInVoucherOut = chkAddSPinVout.Checked;
                modifiedEntity.IsSiteLicensingEnabled = chkSiteLicensingRequired.Checked;
                modifiedEntity.IsEmployeecardTrackingEnabled = chkEmployeecardTrackingRequired.Checked;
                modifiedEntity.ValidateAGSForGMU = chkValidateAGS.Checked;
                modifiedEntity.IsSitesPartiallyConfiguredEnabled = chkPartiallyConfigured.Checked;
                modifiedEntity.IsGameCappingEnabled = chkGameCappingEnabled.Checked;
                modifiedEntity.IsSuppressZoneEnabled = chkSuppressGroupByZone.Checked;
                modifiedEntity.IsSingleCardEmployee = chkIsSingleEmpCard.Checked;
                modifiedEntity.AllowEnableDisableBarPosition = chkAllowBPEnable.Checked;
                modifiedEntity.IsAlertEnabled = chkIsAlertEnabled.Checked;
                modifiedEntity.IsEmailAlertEnabled = chkMailAlert.Checked;
                modifiedEntity.IsAutoCalendarEnabled = cb_IsAutoCalendarEnabled.Checked;
                modifiedEntity.AllowMutltipleDrops = chkAllowMultipleDrops.Checked;
                modifiedEntity.SendMailFromEnterprise = cb_SendMailFromEnterprise.Checked;
                modifiedEntity.CancelPendingMails = chkCancelPendingMails.Checked;
                #endregion
                #region ComboBox
                modifiedEntity.RegionCulture = Convert.ToString(cmbCulture.SelectedValue);
                #endregion
                #endregion
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        #endregion

        private void txtGameCodeMinLength_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);             
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }


        #endregion

        private void chkIsSingleEmpCard_CheckedChanged(object sender, EventArgs e)
        {
            if (chkIsSingleEmpCard.Checked)
            {
                if (_AdminSettings.IsMultiCardAssociatedToUser())
                {
                    chkIsSingleEmpCard.Checked = false;
                    this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_ENT_ADMIN_SETTINGS_REVOKE"), this.Text);
                }
            }
        }

        private void frmAdminSettings_Load(object sender, EventArgs e)
        {
            this.ResolveResources();
        }

        private void btnEditTransactionReason_Click(object sender, EventArgs e)
        {
            try
            {
                frmVaultTransactionReason f_reason = new frmVaultTransactionReason();
                f_reason.ShowDialog();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
    }
}
