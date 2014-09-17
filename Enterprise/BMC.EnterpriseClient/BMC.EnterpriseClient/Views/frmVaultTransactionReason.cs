using BMC.Common.ExceptionManagement;
using BMC.CoreLib.Win32;
using BMC.EnterpriseBusiness.Business;
using BMC.EnterpriseBusiness.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using BMC.Common;
using BMC.Common.LogManagement;

namespace BMC.EnterpriseClient.Views
{
    public partial class frmVaultTransactionReason : BMC.EnterpriseClient.Helpers.BMCExtendedForm
    {
        public frmVaultTransactionReason()
        {
            InitializeComponent();
        }

        private void frmVaultTransactionReason_Load(object sender, EventArgs e)
        {
            try
            {
                SetTagProperty();
                LoadReasonsFromDB();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        void LoadReasonsFromDB()
        {
            try
            {
                LogManager.WriteLog("Load Vault Transaction Reasons From DB", LogManager.enumLogLevel.Info);
                VaultDeclarationBusiness vaultDeclarationBiz = new VaultDeclarationBusiness();
                List<Vault_GetTransactionReason> lst_reason = vaultDeclarationBiz.GetTransactionReasons();
                dview_reason.DataSource = lst_reason;
                ShowControls(false);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        void ShowControls(bool flag)
        {           
            txt_reason.Enabled = flag;
        }
       
        private void btnNew_Click(object sender, EventArgs e)
        {

            btnEdit.Text = this.GetResourceTextByKey("Key_SaveCaption");
            btnNew.Enabled = false;            
            txt_reason.Text = "";
            txt_reason.Enabled = true;
            txt_reason.Focus();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnEdit.Text == this.GetResourceTextByKey("Key_SaveCaption"))
                {
                    string ErrorMsg = "";
                    if (ValidateControls(ref ErrorMsg))
                    {
                        if (!btnNew.Enabled)
                        {
                            VaultDeclarationBusiness vaultDeclarationBiz = new VaultDeclarationBusiness();
                            vaultDeclarationBiz.UpdateTransactionReason(-1, txt_reason.Text);
                        }
                        else
                        {
                            Vault_GetTransactionReason reason = dview_reason.SelectedRows[0].DataBoundItem as Vault_GetTransactionReason;
                            VaultDeclarationBusiness vaultDeclarationBiz = new VaultDeclarationBusiness();
                            vaultDeclarationBiz.UpdateTransactionReason(reason.Reason_ID, txt_reason.Text);
                        }
                        LoadReasonsFromDB();
                        btnEdit.Text = this.GetResourceTextByKey("Key_EditCaption");
                    }
                    else
                    {
                        this.ShowInfoMessageBox(ErrorMsg , this.Text);
                    }

                }
                else
                {
                    btnEdit.Text = this.GetResourceTextByKey("Key_SaveCaption");
                    txt_reason.Enabled = true;
                    txt_reason.Focus();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                // Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_MACHINEDETAILS_RETRIVE"), this.Text);            // "Unable to retrive machine details");
            }
        }

        bool ValidateControls(ref string ErrorMsg)
        {
            bool retval = true;
            try
            {
                if (String.IsNullOrEmpty(txt_reason.Text))
                {
                    ErrorMsg = this.GetResourceTextByKey(1, "MSG_VAULT_TRANSREASONEMPTY"); 
                    retval = false;
                }
                else if (Regex.IsMatch(txt_reason.Text, @"^$|[^a-zA-Z0-9\s]", RegexOptions.CultureInvariant))
                {
                    ErrorMsg = this.GetResourceTextByKey(1, "MSG_VAULT_TRANSREASONALPHANUMERIC");  
                    retval = false;
                }

            }
            catch (Exception ex)
            {
                retval = false;
                ErrorMsg = ex.Message;
            }
            return retval;
        }

        /// <summary>
        /// Assigns the Resource Key names to the controls--Created by kishore sivagnanam
        /// </summary>
        public void SetTagProperty()
        {
            try
            {
                this.Tag = "Key_VaultTransactionReason_Name";
                this.btnNew.Tag = "Key_NewCaption";
                this.btnEdit.Tag = "Key_EditCaption";
                this.lblTransactionReason.Tag = "Key_TransactionReason";
                this.clmSno.Tag = "Key_SNo";
                this.clmType.Tag = "Key_TypeSingle";
                this.clmDescription.Tag = "Key_Description";                
                this.ResolveResources();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                
            }
        }

        private void dview_reason_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dview_reason.SelectedRows.Count == 0)
                {
                    txt_reason.Text = "";
                    txt_reason.Enabled = true;
                    btnEdit.Text = this.GetResourceTextByKey("Key_EditCaption");
                }
                else
                {

                    txt_reason.Text = (dview_reason.SelectedRows[0].DataBoundItem as Vault_GetTransactionReason).Reason_Description;
                    txt_reason.Enabled = false;
                    btnNew.Enabled = true;
                    btnEdit.Text = this.GetResourceTextByKey("Key_EditCaption");
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

    }
}
