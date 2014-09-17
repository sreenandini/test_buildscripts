using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.CoreLib;
using BMC.CoreLib.Win32;
using BMC.EnterpriseBusiness.Business;
using BMC.SecurityVB;
using BMC.EnterpriseClient.Helpers;
using Audit.Transport;
using BMC.Common;
namespace BMC.EnterpriseClient.Views
{
    public partial class ChangePasswordForm : BMC.CoreLib.Win32.BMCDialogForm
    {
        private readonly string _passwordHash = null;
        private BMCSecurityCallMethod _security = null;
        private AuthenticationResult _lastAuthenticationResult = AuthenticationResult.Unauthenticated;

        public ChangePasswordForm(BMCSecurityCallMethod security, AuthenticationResult lastAuthenticationResult)
        {
            _lastAuthenticationResult = lastAuthenticationResult;
            _security = security;
            InitializeComponent();
            setTagProperty();
            _passwordHash = AppGlobals.Current.UserPasswordHash;
        }

        private void setTagProperty()
        {

            this.btnCancel.Tag = "Key_CancelCaption";
            this.btnOK.Tag = "Key_OKCaption";
            this.lblPassword.Tag = "Key_NewPasswordMandatoryColon";
            this.lblConfirmPassword.Tag = "Key_ConfirmPasswordMandatory";
            this.lblOldPassword.Tag = "Key_OldPasswordMandatory";
            this.Tag = "Key_ChangePassword";
            this.Description = this.GetResourceTextByKey("Key_ChangePasswordDescription");
            this.ResolveResources();
        }

        protected override void LoadChanges()
        {
            base.LoadChanges();
            //this.Description = TextResources.CHANGE_PWD_DESCRIPTION;
        }

        protected override bool ValidateChanges()
        {
            if (txtOldPassword.Text.IsEmpty())
            {
                txtOldPassword.RaiseInfoMessageAndReturnV(this, vldCustom, vldSummary, this.GetResourceTextByKey(1,"MSG_CHG_PWD_EMPTY_OLD_PWD")); //MessageResources.MSG_CHG_PWD_EMPTY_OLD_PWD);
                txtOldPassword.Focus();
                txtOldPassword.SelectAll();
                return false;
            }

            string passwordHash = AdminBusiness.CreateHash(txtOldPassword.Text);
            if (passwordHash != _passwordHash)
            {
                txtOldPassword.RaiseInfoMessageAndReturnV(this, vldCustom, vldSummary, this.GetResourceTextByKey(1, "MSG_CHG_PWD_INVALID_OLD"));// MessageResources.MSG_CHG_PWD_INVALID_OLD);
                txtOldPassword.Focus();
                txtOldPassword.SelectAll();
                return false;
            }
            else if (txtNewPassword.Text.IsEmpty())
            {
                txtNewPassword.RaiseInfoMessageAndReturnV(this, vldCustom, vldSummary, this.GetResourceTextByKey(1, "MSG_CHG_PWD_EMPTY_NEW_PWD"));// MessageResources.MSG_CHG_PWD_EMPTY_NEW_PWD);
                txtNewPassword.Focus();
                txtNewPassword.SelectAll();
                return false;
            }
            else if (txtNewPassword.Text.Length < 5)
            {
                txtNewPassword.RaiseInfoMessageAndReturnV(this, vldCustom, vldSummary, this.GetResourceTextByKey(1, "MSG_CHG_PWD_LENGTH"));// MessageResources.MSG_CHG_PWD_LENGTH);
                txtNewPassword.Focus();
                txtNewPassword.SelectAll();
                return false;
            }
            else if (txtConfirmPassword.Text.IsEmpty())
            {
                txtConfirmPassword.RaiseInfoMessageAndReturnV(this, vldCustom, vldSummary, this.GetResourceTextByKey(1, "MSG_CHG_PWD_EMPTY_CONFIRM_PWD"));// MessageResources.MSG_CHG_PWD_EMPTY_CONFIRM_PWD);
                txtConfirmPassword.Focus();
                txtConfirmPassword.SelectAll();
                return false;
            }
            else if (!txtConfirmPassword.Text.Equals(txtNewPassword.Text))
            {
                txtConfirmPassword.RaiseInfoMessageAndReturnV(this, vldCustom, vldSummary, this.GetResourceTextByKey(1, "MSG_CHG_PWD_NOT_MATCHED"));// MessageResources.MSG_CHG_PWD_NOT_MATCHED);
                txtConfirmPassword.Focus();
                txtConfirmPassword.SelectAll();
                return false;
            }
            else if (txtOldPassword.Text.Equals(txtNewPassword.Text))
            {
                txtNewPassword.RaiseInfoMessageAndReturnV(this, vldCustom, vldSummary, this.GetResourceTextByKey(1, "MSG_CHG_PWD_OLD_NEW_SAME"));// MessageResources.MSG_CHG_PWD_OLD_NEW_SAME);
                txtNewPassword.Focus();
                txtNewPassword.SelectAll();
                return false;
            }
            
            // password strength
            string newPassword = txtNewPassword.Text;
            if (!_security.CheckPasswordStrength(ref newPassword))
            {
                txtNewPassword.RaiseInfoMessageAndReturnV(this, vldCustom, vldSummary, this.GetResourceTextByKey(1, "MSG_CHG_PWD_STRENGTH"));// MessageResources.MSG_CHG_PWD_STRENGTH);
                txtNewPassword.Focus();
                txtNewPassword.SelectAll();
                return false;
            }

            string userId = AppGlobals.Current.UserId.ToString();
            string userName = AppGlobals.Current.UserName;
            bool changed = (bool)_security.ChangePassword(ref userId, ref newPassword, ref userName);
            AdminBusiness _business = new AdminBusiness();
            if (changed)
            {
                AppGlobals.Current.UserPassword = newPassword;
                _business.AuditModifiedData(ModuleNameEnterprise.ChangePassword, "Enterprise change password screen", "New password", "", "", AppGlobals.Current.UserId, AppGlobals.Current.UserName," Successful.");
                BMC.EnterpriseClient.Helpers.Win32Extensions.ShowInfoMessageBox(this, this.GetResourceTextByKey(1, "MSG_CHG_PWD_SUCCESSS"),this.Text);// MessageResources.MSG_CHG_PWD_SUCCESSS);
            }
            else
            {
                string message = (_lastAuthenticationResult == AuthenticationResult.Unauthenticated ? "Success" : _lastAuthenticationResult.ToString());
                _business.InsertNewAuditEntry(ModuleNameEnterprise.ChangePassword, "Enterprise change password screen", "Password", "", AppGlobals.Current.UserId, AppGlobals.Current.UserName, message);
                txtNewPassword.RaiseInfoMessageAndReturnV(this, vldCustom, vldSummary, this.GetResourceTextByKey(1, "MSG_CHG_PWD_FAILURE"));  //MessageResources.MSG_CHG_PWD_FAILURE);
                txtNewPassword.Focus();
                txtNewPassword.SelectAll();
                return false;
            }

            return base.ValidateChanges();
        }

        protected override void SaveChanges()
        {
            base.SaveChanges();
        }

        private void ChangePasswordForm_Load(object sender, EventArgs e)
        {
            this.ResolveResources();
        }
    }
}
