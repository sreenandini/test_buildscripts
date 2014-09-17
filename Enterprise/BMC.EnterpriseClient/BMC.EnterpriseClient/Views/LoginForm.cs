using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.EnterpriseClient.Helpers;
using BMC.CoreLib;
using BMC.EnterpriseBusiness.Business;
using BMC.EnterpriseBusiness.Entities;
using BMC.SecurityVB;
using BMC.CoreLib.Diagnostics;
using Audit.Transport;
using BMC.Common;
using System.Threading;
using System.Globalization;

namespace BMC.EnterpriseClient.Views
{
    public partial class LoginForm : BMC.CoreLib.Win32.BMCDialogForm
    {
        private AdminBusiness _business = new AdminBusiness();
        private UserEntity _user = null;
        private int _attempts = 1;
        private int _totalAttempts = 0;
        private BMCSecurityCallMethod _security = new BMCSecurityCallMethod();
        public string _CurrentUserName = "";
        private static IDictionary<AuthenticationResult, string> _errorMessages = null;
        private IDictionary<AuthenticationResult, Control> _controlIds = null;
        private string _description = string.Empty;

        static LoginForm()
        {
            _errorMessages = new Dictionary<AuthenticationResult, string>()
            {
                 { AuthenticationResult.Unauthenticated,BMC.Common.ResourceExtensions.GetResourceTextByKey(null,"Key_INVALID_USR_PWD")},//MessageResources.MSG_INVALID_USR_PWD },
                 { AuthenticationResult.UserNameNotExists, BMC.Common.ResourceExtensions.GetResourceTextByKey(null,"Key_USER_NOT_FOUND")},// MessageResources.MSG_USER_NOT_FOUND },
                 { AuthenticationResult.PasswordNotMatched, BMC.Common.ResourceExtensions.GetResourceTextByKey(null,"Key_PWD_NOT_EXISTS")},// MessageResources.MSG_PWD_NOT_EXISTS },
                 { AuthenticationResult.UserTerminated, BMC.Common.ResourceExtensions.GetResourceTextByKey(null,"Key_USER_TERMINATED")},// MessageResources.MSG_USER_TERMINATED },
                 { AuthenticationResult.UserLocked, BMC.Common.ResourceExtensions.GetResourceTextByKey(null,"Key_USER_LOCKED")},// MessageResources.MSG_USER_LOCKED },
                 { AuthenticationResult.FirstLoginSinceReset, BMC.Common.ResourceExtensions.GetResourceTextByKey(null,"Key_USER_FIRST_LOGIN_RESET")},//, MessageResources.MSG_USER_FIRST_LOGIN_RESET },
                 { AuthenticationResult.PasswordExpired, BMC.Common.ResourceExtensions.GetResourceTextByKey(null,"Key_PWD_EXPIRED")},// MessageResources.MSG_PWD_EXPIRED },
            };
        }

        public LoginForm()
        {
            InitializeComponent();
            SetTagProperty();
            this.Initialize();
        }

        /// <summary>
        /// Assigns the Resource Key names to the controls--Created by kishore sivagnanam
        /// </summary>
        public void SetTagProperty()
        {
            this.btnCancel.Tag = "Key_CancelCaption";
            this.btnOK.Tag = "Key_LoginCaption";
            this.lblPassword.Tag = "Key_PasswordMandatoryColon";
            this.lblUserName.Tag = "Key_UserNameCaptionMandatoryColon";
            //this.Description = (_description = string.Format(this.GetResourceTextByKey("Key_Enterthecredentialstoproceedlogin"), _totalAttempts)); 
            this.Tag = "Key_Login";

        }

        private void Initialize()
        {
            ModuleProc PROC = new ModuleProc("", "Initialize");

            try
            {
                _totalAttempts = _security.GetNumberOfAttempts().ConvertToInt32();
                tblButtons.ColumnStyles[3].Width = 12;
                tblButtons.RowStyles[1].Height = 10;
                tblButtons.Height += 10;
                this.Description = (_description = string.Format(this.GetResourceTextByKey("Key_Enterthecredentialstoproceedlogin"), _totalAttempts));
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
            finally
            {
                this.InitControlIds();
            }
        }

        private void InitControlIds()
        {
            _controlIds = new Dictionary<AuthenticationResult, Control>()
            {
                 { AuthenticationResult.Unauthenticated, txtPassword },
                 { AuthenticationResult.UserNameNotExists, txtUserName },
                 { AuthenticationResult.PasswordNotMatched, txtPassword },
                 { AuthenticationResult.UserTerminated, txtUserName },
                 { AuthenticationResult.UserLocked, txtUserName },
                 { AuthenticationResult.FirstLoginSinceReset, txtPassword },
                 { AuthenticationResult.PasswordExpired, txtPassword },
            };
        }

        public UserEntity User
        {
            get { return _user; }
        }

        public AuthenticationResult AuthenticationResult { get; set; }

        protected override void LoadChanges()
        {
            base.LoadChanges();

            txtUserName.Text = AppSettings.Current.LastSavedUser;
            if (!txtUserName.Text.IsEmpty())
            {
                txtPassword.Select();
            }
            else
            {
                txtUserName.Select();
            }
        }

        protected override bool ValidateChanges()
        {
            string userName = txtUserName.Text;
            string password = txtPassword.Text;

            if (userName.IsEmpty())
                return txtUserName.RaiseInfoMessageAndReturnV(this, vldCustom, vldSummary, this.GetResourceTextByKey(1, "MSG_USER_EMPTY"));// MessageResources.MSG_USER_EMPTY);            
            else if (password.IsEmpty())
                return txtPassword.RaiseInfoMessageAndReturnV(this, vldCustom, vldSummary, this.GetResourceTextByKey(1, "MSG_PWD_EMPTY"));//MessageResources.MSG_PWD_EMPTY);

            if (_CurrentUserName != userName)
            {
                _attempts = 1;
            }
            UserEntity user = null;
            AdminBusiness.AuthenticateAndGetUserResult result = _business.AuthenticateAndGetUser(userName, password, ref user);
            this.AuthenticationResult = result.Result;
            if (result.Result != AuthenticationResult.Authenticated)
            {
                string message = string.Empty;
                if (_errorMessages.ContainsKey(result.Result))
                {
                    message = _errorMessages[result.Result];
                    if (this.AuthenticationResult == AuthenticationResult.UserTerminated || this.AuthenticationResult == AuthenticationResult.UserNameNotExists || this.AuthenticationResult == AuthenticationResult.UserLocked)
                    {
                        _attempts = 0;

                    }
                }
                if (result.Exception != null)
                {
                    message += Environment.NewLine +
                                Environment.NewLine +
                                new string('=', 60) +
                                Environment.NewLine +
                                Environment.NewLine +
                                result.Exception.Message;
                }

                if (result.Result != AuthenticationResult.UserLocked)
                {
                    if (_totalAttempts > 0 &&
                        _attempts >= _totalAttempts)
                    {
                        message = _errorMessages[AuthenticationResult.UserLocked];
                        if (user != null)
                        {
                            string userId = user.SecurityUserID.ToString();
                            _security.LockUser(ref userId);
                        }
                    }
                }

                Control ctl = txtPassword;
                if (_controlIds.ContainsKey(result.Result))
                {
                    ctl = _controlIds[result.Result];
                }
                ctl.RaiseWarningMessageAndReturnV(this, vldCustom, vldSummary, message);
                if (ctl is TextBox)
                {
                    ((TextBox)ctl).SelectAll();
                }
                if ((result.Result == AuthenticationResult.FirstLoginSinceReset)
                    || (result.Result == AuthenticationResult.PasswordExpired))
                {
                    this.ShowInfoMessageBox(_errorMessages[result.Result], this.Text);
                    if (user != null)
                    {
                        this.SetCredentials(user, password);
                    }
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                this.Description = _description + " (Attempt " +
                    Math.Min(_attempts, _totalAttempts).ToString() +
                    " of " + _totalAttempts.ToString() + ")";
                if (this.AuthenticationResult != AuthenticationResult.UserTerminated && this.AuthenticationResult != AuthenticationResult.UserNameNotExists && this.AuthenticationResult != AuthenticationResult.UserLocked)
                {
                    _attempts++;
                }
                _business.InsertNewAuditEntry(ModuleNameEnterprise.Login, "Enterprise Login", "", "", 0, txtUserName.Text, AuthenticationResult.ToString());

                _CurrentUserName = userName;
                return false;
            }

            //Make sure that there is a way of logging in in a totally blank database!
            if (userName == "friday" &&
                password == "lard" &&
                user.UserName == "admin")
            {
                password = "admin";
            }
            _user = user;

            //User language based date format
            CultureInfo currentCulture = CultureInfo.InvariantCulture;
            switch (_user.LanguageID)
            {
                case 1:
                    currentCulture = new CultureInfo("en-US");
                    break;
                case 2: 
                    currentCulture = new CultureInfo("en-GB");
                    break;
                case 3:
                    currentCulture = new CultureInfo("es-ar");
                    break;
                case 4:
                    currentCulture = new CultureInfo("it-IT");
                    break;
            }
            Thread.CurrentThread.CurrentCulture = currentCulture;


            this.SetCredentials(user, password);
            _business.InsertNewAuditEntry(ModuleNameEnterprise.Login, "Enterprise Login", "", "", user.SecurityUserID, txtUserName.Text, " Successful.");
            return base.ValidateChanges();
        }

        private void SetCredentials(UserEntity user, string password)
        {
            AppGlobals.Current.UserId = user.SecurityUserID;
            AppGlobals.Current.UserName = user.UserName;
            AppGlobals.Current.UserPassword = password;
            AppSettings.Current.LastSavedUser = txtUserName.Text;

            AppSettings.Current.Save();
        }

        protected override void SaveChanges()
        {
            base.SaveChanges();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            this.ResolveResources();
        }
    }
}
