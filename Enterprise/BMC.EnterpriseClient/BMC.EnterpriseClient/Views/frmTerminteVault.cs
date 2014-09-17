using BMC.EnterpriseBusiness.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.Common.ExceptionManagement;
using BMC.EnterpriseClient.Helpers;
using BMC.EnterpriseBusiness.Business;
using Audit.Transport;
using BMC.Common.LogManagement;
using BMC.Common;

namespace BMC.EnterpriseClient.Views
{
    public partial class frmTerminateVault : Form
    {
        Vault_GetAllDevices _objVault;
        VaultAdmin _VaultBiz;
        public frmTerminateVault()
        {
            InitializeComponent();
            SetTagProperty();
        }
        public frmTerminateVault(Vault_GetAllDevices VaultDevice):this()
        {
            _objVault = VaultDevice;            
        }
        /// <summary>
        /// Assigns the Resource Key names to the controls--Created by kishore sivagnanam
        /// </summary>
        public void SetTagProperty()
        {
            this.btnNo.Tag = "Key_NoCaption";
            this.btnYes.Tag = "Key_Yes";
            this.label4.Tag = "Key_ReasonMandatory";
            this.label2.Tag = "Key_SiteCodeColon";
            this.label3.Tag = "Key_SiteNameColon";
            this.Tag = "Key_TerminateVault";
            this.label5.Tag = "Key_VaultDeviceTerminateMsg";
            this.label1.Tag = "Key_VaultNameColon";

        }
        private void frmTerminateVault_Load(object sender, EventArgs e)
        {
            this.ResolveResources();
            txtVaultName.Text = _objVault.Vault;
            txtSiteName.Text = _objVault.Site_Name;
            txtSiteCode.Text = _objVault.Site_Code;
            txtTerminateReason.Clear();
        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtTerminateReason.Text.Trim() == string.Empty)
                {
                    Win32Extensions.ShowErrorMessageBox(this, this.GetResourceTextByKey(1, "MSG_ENTER_REASON_TERMINATION"),this.Text);
                    txtTerminateReason.Focus();
                    return;
                }
                if (_objVault.Vault_ID <= 0)
                    return;
                _VaultBiz = new VaultAdmin();
               int Result = _VaultBiz.Vault_Termination(_objVault.Vault_ID, AppGlobals.Current.UserId, (int)ModuleIDEnterprise.VaultManager,
                                            ModuleIDEnterprise.VaultManager.ToString(), "Terminate Vault", txtTerminateReason.Text);
               if (Result == 0)
               {
                   Win32Extensions.ShowInfoMessageBox(this,this.GetResourceTextByKey(1, "MSG_VAULT") + " " + _objVault.Vault + this.GetResourceTextByKey(1, "MSG_HAS_TERMINATED"),this.Text);
               }
               else
               {
                   LogManager.WriteLog("frmTerminateVault->btnYes_Click(): Error code from DB while terminating :" + Result.ToString(), LogManager.enumLogLevel.Error);
               }

            }
            catch(Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            this.DialogResult = DialogResult.Yes;
            this.Close();
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            this.Close();
        }

    }
}
