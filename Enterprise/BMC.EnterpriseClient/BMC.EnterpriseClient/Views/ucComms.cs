using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.EnterpriseBusiness.Business;
using BMC.SecurityVB;
using BMC.EnterpriseBusiness.Entities;
using System.Security.Cryptography;
using BMC.Common.ExceptionManagement;
using Audit.BusinessClasses;
using Audit.Transport;
using BMC.Common.Utilities;
using BMC.CoreLib.Win32;
using Microsoft.Win32;
using BMC.Common;

namespace BMC.EnterpriseClient.Views
{
    public partial class ucComms : UserControl, IAdminSite
    {
        #region Local Variables
        /*Private Variable declarations*/
        SiteDetails _sdobj = new SiteDetails();
        Random _rnd = new Random(0); /*For pass key generation*/
        bool _SiteStatus;
        string _errorMessage = string.Empty;
        private AdminBusiness _businessAdmin = null;

        #endregion

        #region Properties
        /*Properties declarations*/
        public bool SiteStatus
        {
            get
            {
                return _SiteStatus;
            }
            set
            {
                btnEnableDisableSite.Text = (value) ? this.GetResourceTextByKey("Key_DisableSite") : this.GetResourceTextByKey("Key_EnableSite");       // "Disable Site" : "Enable Site";
                _SiteStatus = value;
            }
        }
        public int SiteID
        {
            get;
            set;
        }
        private string SiteCode
        {
            get;
            set;
        }

        #endregion

        #region IAdminSite Members

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>

        public void LoadDetails(AdminSiteEntity entity)
        {
            try
            {
                if (entity != null)
                {
                    SiteID = entity.Site_ID;
                    txtWebURL.Text = entity.WebURL + "";
                    SiteCode = entity.Site_Code + "";
                }
                else
                {
                    SiteID = 0;
                }
                if (SiteID == 0)
                {
                    btnExportSiteSetup.Enabled = false;
                    grpExportDetails.Enabled = false;  
                    SiteStatus = true;
                    btnExportKeys.Enabled = false;
                }
                else
                {
                    AdminSiteEntity status = _sdobj.GetSiteStatus(SiteID);
                    SiteStatus = (status == null) ? false : status.Site_Enabled;
                    entity.Site_Enabled = SiteStatus;
                    SiteKeyExists oSiteKeyExists = _sdobj.IsSiteKeyExists(SiteID);
                    btnExportSiteSetup.Enabled = oSiteKeyExists.IsExchangeKeyAvailable.HasValue?oSiteKeyExists.IsExchangeKeyAvailable.Value:false;
                    grpExportDetails.Enabled = oSiteKeyExists.IsExchangeKeyAvailable.HasValue ? oSiteKeyExists.IsExchangeKeyAvailable.Value : false;
                }

                string EnableDisableSiteButtonVisible = string.Empty;
                _sdobj.GetSetting(null, "EnableDisableSiteButtonVisible", "FALSE", ref EnableDisableSiteButtonVisible);
                if (EnableDisableSiteButtonVisible.Trim().ToUpper().Equals("FALSE"))
                {
                    btnEnableDisableSite.Visible = false;
                }
                else
                {
                    btnEnableDisableSite.Visible = true;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            finally
            {
                if (SiteID != 0 && !AppGlobals.Current.HasUserAccess("HQ_Admin_Customers_Site_Edit"))
                {
                    txtWebURL.Enabled = false;
                    btnExportSiteSetup.Enabled = false;
                    grpExportDetails.Enabled = false;
                    btnExportKeys.Enabled = false;
                    btnEnableDisableSite.Enabled = false;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public bool SaveDetails(AdminSiteEntity entity)
        {
            try
            {

                int? Result = 0;
                if (entity == null || string.IsNullOrEmpty(txtWebURL.Text.Trim()))
                {
                    if (string.IsNullOrEmpty(txtWebURL.Text.Trim()))
                    {
                        this.ShowWarningMessageBox(this.GetResourceTextByKey(1, "MSG_URL_EMPTY"), this.ParentForm.Text);   // "Web URL can not be empty."
                        txtWebURL.Focus();
                    }
                    return false;

                }
                else if ((_sdobj.CheckWebURLExists(SiteID, txtWebURL.Text.Trim(), Result)) == 1)
                {
                    this.ShowWarningMessageBox(this.GetResourceTextByKey(1, "MSG_URL_EXISTS"), this.ParentForm.Text);    //"The URL entered is already assigned to another Site. Please provide a different URL."
                    txtWebURL.Focus();
                    return false;
                }
                else
                {
                    entity.WebURL = txtWebURL.Text.Trim();
                    entity.Site_Enabled = SiteStatus;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return false;

            }
            return true;
        }

        #endregion

        #region Constructor

        public ucComms()
        {
            InitializeComponent();
            _businessAdmin = new AdminBusiness();

            // Set Tags for controls
            SetTagProperty();

        }

        private void SetTagProperty()
        {
            this.lblSiteWebURL.Tag = "Key_SiteWebURLMandatory";
            this.btnEnableDisableSite.Tag = "Key_EnableSite";
            this.btnExportKeys.Tag = "Key_ExportEnterpriseandSiteKeys";
            this.btnExportSiteSetup.Tag = "Key_ExporttoSiteCaption1";

            this.chkExportSiteSetup.Tag = "Key_ExportSiteDetailsToSite";
            this.chkExportSiteCalendar.Tag = "Key_ExportCurrentCalendarToSite";
            this.chkExportModelsToSite.Tag = "Key_ExportModelDetailstoSite";
            this.chkExportGames.Tag = "Key_ExportGameDetailstoSite";
        }

        #endregion
        
        #region Methods
        /// <summary>
        /// Custom Validator send true for successful validation and false for any failure
        /// </summary>
        /// <returns></returns>
        private bool CustomValidator()
        {
            try
            {
                if (String.IsNullOrEmpty(txtWebURL.Text.Trim()))
                {
                    this.ShowWarningMessageBox(this.GetResourceTextByKey(1, "MSG_ENTER_WEBURL"), this.ParentForm.Text);        //"Please enter a Web URL to Export the Details.";
                    return false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Based on the error code return from the SP. This message will show in the Message Box
        /// </summary>
        /// <param name="errorNumber"></param>
        /// <param name="successMessage"></param>
        private bool ErrorChecker(int errorNumber, string successMessage)
        {
            bool result = false;
            try
            {
                if (errorNumber == 0)
                {
                    if (!string.IsNullOrEmpty(successMessage.Trim()))
                        this.ShowInfoMessageBox(successMessage, this.ParentForm.Text);
                    result = true;
                }
                else
                {
                    if(!string.IsNullOrEmpty(_errorMessage.Trim()))
                        this.ShowErrorMessageBox(this.GetResourceTextByKey(1,_errorMessage.Trim()), this.ParentForm.Text);
                    else
                        this.ShowErrorMessageBox(this.GetResourceTextByKey(1, "MSG_SL_UNKNOWNERROR"), this.ParentForm.Text);        //"Unknown Error";
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return result;
        }
        #endregion

        #region Event_Handler
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnExportSiteSetup_Click(object sender, EventArgs e)
        {
            if (!chkExportSiteSetup.Checked && !chkExportModelsToSite.Checked && !chkExportSiteCalendar.Checked && !chkExportGames.Checked)
            {
                return;
            }

            if (chkExportSiteSetup.Checked)
            {
                try
                {
                    if (this.CustomValidator())
                    {
                        this.ErrorChecker(_sdobj.ExportSiteSetup(SiteID.ToString(), SiteID, ref _errorMessage), this.GetResourceTextByKey(1, ""));     // "Site Details Exported Successfully."
                    }
                }
                catch (Exception ex)
                {
                    ExceptionManager.Publish(ex);
                }
            }

            if (chkExportModelsToSite.Checked)
            {
                try
                {
                    if (this.CustomValidator())
                    {
                        this.ErrorChecker(_sdobj.ExportModel("ALL", SiteID, ref _errorMessage), this.GetResourceTextByKey(1, ""));              //"Model details exported succcessfully."
                    }
                }
                catch (Exception ex)
                {
                    ExceptionManager.Publish(ex);
                } 
            }

            if (chkExportSiteCalendar.Checked)
            {
                try
                {
                    if (this.CustomValidator())
                    {
                        this.ErrorChecker(_sdobj.ExportCalendar(SiteID.ToString(), SiteID, ref _errorMessage), this.GetResourceTextByKey(1, ""));              // "Calendar Details Exported Successfully.";
                    }
                }
                catch (Exception ex)
                {
                    ExceptionManager.Publish(ex);
                } 
            }

            if (chkExportGames.Checked)
            {
                try
                {
                    if (this.CustomValidator())
                    {
                        this.ErrorChecker(_sdobj.ExportGameLibrary(SiteID, ref _errorMessage), this.GetResourceTextByKey(1, ""));              //"Game Details Exported Successfully.";
                    }
                }
                catch (Exception ex)
                {
                    ExceptionManager.Publish(ex);
                } 
            }

            if (string.IsNullOrEmpty(_errorMessage.Trim()))
                this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_SITEDETAILS_EXPORT_SUCCESS"), this.ParentForm.Text);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnExportModelsToSite_Click(object sender, EventArgs e)
        {
            
        }

        private void BtnExportSiteCalendar_Click(object sender, EventArgs e)
        {
            
        }

        private void btnExportGames_Click(object sender, EventArgs e)
        {
            
        }

        private void btnExportKeys_Click(object sender, EventArgs e)
        {
            try
            {
                BMCSecurityCallMethod EncryptSec = new BMCSecurityCallMethod();
                string PassKey = string.Empty;
                byte[] buffer = new byte[16];
                RandomNumberGenerator prov = RNGCryptoServiceProvider.Create();
                prov.GetBytes(buffer);
                string randomkey = Encoding.Unicode.GetString(buffer);
                string original = SiteCode + EncryptSec.GetHashString(SiteCode + randomkey);
                PassKey = Convert.ToString(EncryptSec.Encrypt(original));

                if (!IsEnterpriseServer())
                {
                    this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_EXPORTKEY"), this.ParentForm.Text);        //"Please export key from Enterprise Server.";
                    return;
                }

                if (this.ErrorChecker(_sdobj.UpdatePasskey(SiteID, PassKey, ref _errorMessage), ""))
                {
                    if (EncryptSec.WebCall(SiteCode, BMC.Common.Utilities.DatabaseHelper.GetConnectionString()))
                    {
                        if (SettingsEntity.LiquidationProfitShare)
                        {
                            _businessAdmin.ExportLiquidationDetailsToSite(SiteCode);
                        }
                        this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_KEYSEXPORT_SUCCESS"), this.ParentForm.Text);        //"Keys exported to Exchange Successfully.";
                        btnExportSiteSetup.Enabled = true;
                        grpExportDetails.Enabled = true;
                    }
                    else
                    {
                        this.ShowWarningMessageBox(this.GetResourceTextByKey(1, "MSG_KEYSEXPORT_FAILED"), this.ParentForm.Text);        //"Failed to export keys to Exchange. Check Exchange connectivity";
                    }
                }                
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                this.ShowWarningMessageBox(this.GetResourceTextByKey(1, "MSG_KEYSEXPORT_FAILED"), this.ParentForm.Text);        //"Failed to export keys to Exchange. Check Exchange connectivity";
            }
        }

        private void btnEnableDisableSite_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (this.ErrorChecker(_sdobj.EnableorDisableSite(SiteID, !SiteStatus, ref _errorMessage), ""))
                {
                  
                        AuditViewerBusiness business = new AuditViewerBusiness(DatabaseHelper.GetConnectionString());
                        {
                            business.InsertAuditData(new Audit.Transport.Audit_History
                            {
                                EnterpriseModuleName = ModuleNameEnterprise.UserSiteAdmin,
                                Audit_Screen_Name = "Site Administrator",
                                Audit_Desc = "Site " + "'" + SiteCode + "' " + ((SiteStatus) ? "Disabled" : "Enabled") + " [Site_Enabled]: '" + ((SiteStatus) ? "Disabled" : "Enabled") + "' --> '" + ((!SiteStatus) ? "Disabled" : "Enabled") + "'",
                                AuditOperationType = OperationType.MODIFY,
                                Audit_User_ID = AppEntryPoint.Current.UserId,
                                Audit_User_Name = AppEntryPoint.Current.UserName,
                                Audit_Field = "Site_Enabled",
                                Audit_Old_Vl = Convert.ToInt32(SiteStatus).ToString(),
                                Audit_New_Vl = Convert.ToInt32(!SiteStatus).ToString(),
                            }, false);
                        }
                    
                    SiteStatus = !SiteStatus;
                    //this.ShowInfoMessageBox("Site " + ((!SiteStatus) ? "Disabled" : "Enabled") + " Successfully.");
                    this.ShowInfoMessageBox((!SiteStatus) ? this.GetResourceTextByKey(1, "") : this.GetResourceTextByKey(1, ""), this.ParentForm.Text);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            //Need to do audit for enable and disable

        }

        #endregion

        #region User Defined Function
        private bool IsEnterpriseServer()
        {
            bool bIsEnterpriseServer = BMCRegistryHelper.IsEnterpriseServer();

            /*RegistryKey regKeyConnectionString = Registry.LocalMachine.OpenSubKey("Software\\Honeyframe");
            if (regKeyConnectionString != null && regKeyConnectionString.GetValue("InstallationType") != null)
            {
                if (regKeyConnectionString.GetValue("InstallationType").ToString() == "EnterpriseServer")
                    bIsEnterpriseServer = true;
                else
                    bIsEnterpriseServer = false;
            }
            else
                bIsEnterpriseServer = false;
            */

            return bIsEnterpriseServer;
        }
        #endregion

        private void ucComms_Load(object sender, EventArgs e)
        {
            // For externalization
            this.ResolveResources();
            btnExportSiteSetup.Enabled = false;
        }

        private void chkExportSiteSetup_CheckedChanged(object sender, EventArgs e)
        {
            if (chkExportSiteSetup.Checked || chkExportModelsToSite.Checked || chkExportSiteCalendar.Checked || chkExportGames.Checked)
            {
                btnExportSiteSetup.Enabled = true;
            }
            else
            {
                btnExportSiteSetup.Enabled = false;
            }
        }
    }
}
