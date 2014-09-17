using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.Common.ExceptionManagement;
using BMC.EnterpriseBusiness.Entities;
using BMC.EnterpriseBusiness.Business;
using Audit.Transport;
using BMC.EnterpriseClient.Helpers;
using BMC.Common;

namespace BMC.EnterpriseClient.Views
{
    public partial class frmVaultCopy : Form
    {

         BMC.EnterpriseClient.Helpers.Datawatcher objDatawatcher = null;

        int _iSrcVault_ID;
        public frmVaultCopy()
        {
            InitializeComponent();
            SetTagProperty();
            this.ResolveResources();
            objDatawatcher = new Helpers.Datawatcher(this);
        }
        public frmVaultCopy(int Vault_id, String Vault_name)
            : this()
        {
            lbl_OldVault.Text = Vault_name;
            _iSrcVault_ID = Vault_id;
            SetTagProperty();
            this.ResolveResources();
       }

        /// <summary>
        /// Assigns the Resource Key names to the controls--Created by kishore sivagnanam
        /// </summary>
        public void SetTagProperty()
        {
            this.btn_Save.Tag = "Key_SaveCaption";
            this.lbl_NewVaultName.Tag = "Key_NameMandatory";
            this.lbl_NewVaultSerial.Tag = "Key_SerialNoMandatory";
            this.btn_Close.Tag = "Key_CloseCaption";
            this.chk_ClearOnSave.Tag = "Key_Clearonsave";
            this.Tag = "Key_CopyVaultDevice";
            this.lbl_newVault.Tag = "Key_NewVaultDetailsColon";
            this.lbl_VaultName.Tag = "Key_SourceVault";
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_NewVaultName.Text.Trim() == string.Empty)
                {
                    Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_VAULT_NAME_EMPTY"), this.Text);
                    return;
                }
                if (txt_newVaultSerial.Text.Trim() == string.Empty)
                {
                    Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_SERIAL_NUMBER_EMPTY"), this.Text);
                    return;

                }

                VaultAdmin obj = new VaultAdmin();
                int result = obj.UpdateVaultCopy(txt_NewVaultName.Text, txt_newVaultSerial.Text, AppGlobals.Current.UserId, (int)ModuleIDEnterprise.VaultManager, ModuleNameEnterprise.VaultManager.ToString(), "Vault Admin", _iSrcVault_ID);
                if (result == 0)
                {
                    Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_COPY_SUCCESS"), this.Text);
                    if (chk_ClearOnSave.Checked)
                    {
                        txt_NewVaultName.Clear();
                        txt_newVaultSerial.Clear();
                    }
                }
                else if (result == 1)
                {
                    Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_DUPLICATE_VAULT_NAME"), this.Text);
                }
                else if (result == 2)
                {
                    Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_FAILED_UPDATING_VAULT_DEVICE"), this.Text);
                }
                else
                {
                    Win32Extensions.ShowErrorMessageBox(this, this.GetResourceTextByKey(1, "MSG_FAILED_UPDATING_CASSETTE_DETAILS"), this.Text);
                }
                txt_NewVaultName.Focus();
                
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                Win32Extensions.ShowErrorMessageBox(this, this.GetResourceTextByKey(1, "MSG_UPDATE_FAILED"), this.Text);
            }

        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
