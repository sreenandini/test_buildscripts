using System;
using System.Drawing;
using System.Windows.Forms;
using BMC.MeterAdjustmentTool;
using BMC.MeterAdjustmentTool.Helpers;
using BMC.Common.ExceptionManagement;
using System.Data.Linq;
using BMC.Common;

namespace BMC.MeterAdjustmentTool
{
    public partial class LoginForm : DialogFormBase
    {
        private string _userName = string.Empty;
        private string _password = string.Empty;

        public LoginForm()
        {
            InitializeComponent();

            // Set Tags for controls
            SetTagProperty();
        }

        private void SetTagProperty()
        {
            lblUserName.Tag = "Key_UserNameColon";
            lblPassword.Tag = "Key_PasswordCaptionColon";
            btnSignIn.Tag = "Key_LoginCaption";
        }

        protected override void LoadChanges()
        {
            base.LoadChanges();

            // For externalization
            this.ResolveResources();
            this.Text = Extensions.AppTitle + this.GetResourceTextByKey("Key_LoginFormTitle");   //" - [Login]";
        }

        private void btnSignIn_Click(object sender, EventArgs e)
        {
            this.OnAcceptButtonClicked();
        }

        protected override bool SaveChanges()
        {
            bool result = default(bool);

            try
            {
                int? isAuthenticated = null;
                int isAuthenticatedValue = 0;
                using (EnterpriseDataContextHelper db = new EnterpriseDataContextHelper())
                {
                    ISingleResult<rsp_AuthenticateAndGetUserResult> userResults = db.FuncAuthenticateAndGetUser(_userName,
                        MD5Hash.CreateHash(_password), ref isAuthenticated);
                    if (isAuthenticated.IsValid())
                    {
                        isAuthenticatedValue = isAuthenticated.Value;
                        result = (isAuthenticatedValue == 1);

                        if (result)
                        {
                            foreach (rsp_AuthenticateAndGetUserResult userResult in userResults)
                            {
                                this.Login = new LoginDetail()
                                {
                                    UserID = userResult.SecurityUserID,
                                    UserName = userResult.UserName
                                };
                                break;
                            }

                        }
                    }
                }

                switch (isAuthenticatedValue)
                {
                    case -3:
                        this.ShowInfoMessageBox(this.GetResourceTextByKey(1,"MSG_USER_NOT_FOUND"));        //"User name does not exist.");
                        break;

                    case -4:
                        this.ShowInfoMessageBox(this.GetResourceTextByKey(1,"MSG_PWD_NOT_EXISTS"));        //"Invalid password entered.");
                        break;

                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                Win32Extensions.ShowErrorMessageBox(ex.Message);
            }

            return result;
        }

        public override bool ValidateUI()
        {
            _userName = txtUserName.Text.Trim();
            _password = txtPassword.Text.Trim();

            if (_userName.IsEmpty())
            {
                this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_MAT_USERNAME_REQUIRED"));        //"User name is required.");
                txtUserName.Focus();
                return false;
            }
            else if (_password.IsEmpty())
            {
                this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_MAT_PASSWORD_REQUIRED"));        //"Password is required.");
                txtPassword.Focus();
                return false;
            }

            return true;
        }

        public LoginDetail Login { get; private set; }
    }
}
