using System.Windows.Input;

using BMC.Transport;
using BMC.Security;
using System;
using BMC.Common.ExceptionManagement;
using BMC.CashDeskOperator;
using BMC.Common.Utilities;
using BMC.CashDeskOperator.BusinessObjects;
using Audit.BusinessClasses;
using Audit.Transport;
using BMC.Presentation.POS.Helper_classes;

namespace BMC.Presentation.POS.Views
{
    /// <summary>
    /// Interaction logic for PasswordRetry.xaml
    /// </summary>
    public partial class PasswordRetry : IDisposable
    {
        private string _sKeyText = string.Empty;
        private BMC.Security.Interfaces.IUser _user;
        private BMC.Security.Manager.UserManager _userManager;
        private int _securityID;
        private bool bChanged = false;

        public PasswordRetry(int SecurityUserId)
        {
            _securityID = SecurityUserId;
            _userManager = new BMC.Security.Manager.UserManager(oCommonUtilities.CreateInstance().GetConnectionString());
            _user = _userManager.GetUserProfileById(SecurityUserId);
            InitializeComponent();
            txtOldPassword.Focus();
        }

        private void btnCancel_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.DialogResult = false;
           this.Close();
        }

        private void btnOK_Click(object sender, System.Windows.RoutedEventArgs e)
        {

            try
            {
                if (string.IsNullOrEmpty(txtOldPassword.Password))
                {
                    MessageBox.ShowBox("MessageID270", BMC_Icon.Warning);
                    txtOldPassword.Focus();
                    return;
                }

                if (CryptoHelper.CreateHash(txtOldPassword.Password) != SecurityHelper.CurrentUser.Password)
                {
                    MessageBox.ShowBox("MessageID271", BMC_Icon.Warning);
                    txtOldPassword.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtNewPassword.Password))
                {
                    MessageBox.ShowBox("MessageID272", BMC_Icon.Warning);
                    txtOldPassword.Focus();
                    return;
                }
                if (txtNewPassword.Password.Length < 5)
                {
                    MessageBox.ShowBox("MessageID273", BMC_Icon.Warning);
                    txtNewPassword.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtRetypePassword.Password))
                {
                    MessageBox.ShowBox("MessageID274", BMC_Icon.Warning);
                    txtRetypePassword.Focus();
                    return;
                }
                //else if (txtRetypePassword.Password.Length < 5)
                //{
                //    MessageBox.ShowBox("Password length should be atleast 5 characters.", BMC_Icon.Warning, true);
                //    txtRetypePassword.Focus();
                //    return;
                //}
                if (txtOldPassword.Password == txtNewPassword.Password)
                {
                    MessageBox.ShowBox("MessageID276", BMC_Icon.Warning);
                    txtNewPassword.Focus();
                    return;
                }
                if (txtOldPassword.Password == txtRetypePassword.Password)
                {
                    MessageBox.ShowBox("MessageID276", BMC_Icon.Warning);
                    txtRetypePassword.Focus();
                    return;
                }
                if (txtNewPassword.Password != txtRetypePassword.Password)
                {
                    MessageBox.ShowBox("MessageID277", BMC_Icon.Warning);
                    txtRetypePassword.Focus();
                    return;
                }

                if (!PasswordHelper.CheckPasswordStrength(txtNewPassword.Password))
                {
                    MessageBox.ShowBox("MessageID278", BMC_Icon.Warning);
                    txtNewPassword.Password = "";
                    txtRetypePassword.Password = "";
                    txtNewPassword.Focus();
                    return;
                }

                try
                {
                    _user.Password = txtNewPassword.Password;
                    bChanged = _userManager.ChangePassword(_user);

                    if (bChanged)
                    {
                        SecurityHelper.CurrentUser.Password = txtNewPassword.Password;

                        userDataContext uDC = new userDataContext(oCommonUtilities.CreateInstance().GetConnectionString());
                        uDC.Export_History(_securityID.ToString(), "", "CHANGEPASSWORD", null);
                        MessageBox.ShowBox("MessageID279", BMC_Icon.Information);
                        txtOldPassword.Password = "";
                        txtNewPassword.Password = "";
                        txtRetypePassword.Password = "";

                        AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
                        {

                            AuditModuleName = ModuleName.Password,
                            Audit_Screen_Name = "ChangePassword",
                            Audit_Desc = "Password changed by user.",
                            AuditOperationType = OperationType.MODIFY
                        });

                        this.DialogResult = true;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.ShowBox("MessageID301", BMC_Icon.Information);
                        AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
                        {

                            AuditModuleName = ModuleName.Password,
                            Audit_Screen_Name = "ChangePassword",
                            Audit_Desc = "Unable to change password.",
                            AuditOperationType = OperationType.MODIFY
                        });
                    }


                }
                catch (Exception ex)
                {
                    ExceptionManager.Publish(ex);

                    MessageBox.ShowBox("MessageID301", BMC_Icon.Information);
                    AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
                    {

                        AuditModuleName = ModuleName.Password,
                        Audit_Screen_Name = "ChangePassword",
                        Audit_Desc = "Unable to change password.",
                        AuditOperationType = OperationType.MODIFY
                    });
                }

            }
            finally
            {
                btnOK.IsEnabled = true;
            }
          }



        public string DisplayKeyboard(string keyText, string type)
        {
            _sKeyText = "";

            var objKeyboard = new KeyboardInterface();
            if (type == "Pwd")
            {
                objKeyboard.IsPwd = true;
            }
            objKeyboard.Closing += ObjKeyboardClosing;
            objKeyboard.KeyString = keyText;
            objKeyboard.Top = Top + Height - objKeyboard.Height;
            objKeyboard.Left = Left + Width / 2 - objKeyboard.Width / 2;
            objKeyboard.ShowDialog();
            return _sKeyText;
        }
        void ObjKeyboardClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (((KeyboardInterface)sender).DialogResult == true)
            {
                _sKeyText = ((KeyboardInterface)sender).KeyString;
            }
        }

        private void txtOldPassword_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!Settings.OnScreenKeyboard)
                return;
            txtOldPassword.Password = DisplayKeyboard(txtOldPassword.Password, "Pwd");
            txtOldPassword.SelectAll();
        }

        private void txtNewPassword_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!Settings.OnScreenKeyboard)
                return;
            txtNewPassword.Password = DisplayKeyboard(txtNewPassword.Password, "Pwd");
            txtNewPassword.SelectAll();
        }

        private void txtRetypePassword_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!Settings.OnScreenKeyboard)
                return;
            txtRetypePassword.Password = DisplayKeyboard(txtRetypePassword.Password, "Pwd");
            txtRetypePassword.SelectAll();
        }

        #region IDisposable Members

        /// <summary>
        /// Variable used to identity whether this object is already disposed or not.
        /// </summary>
        private bool disposed = false;

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        private void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    this.CleanupWPFObjectTopControls((i) =>
                    {
                        // events
                        this.txtOldPassword.PreviewMouseUp -= (this.txtOldPassword_PreviewMouseUp);
                        this.txtNewPassword.PreviewMouseUp -= (this.txtNewPassword_PreviewMouseUp);
                        this.txtRetypePassword.PreviewMouseUp -= (this.txtRetypePassword_PreviewMouseUp);
                        this.btnOK.Click -= (this.btnOK_Click);
                        this.btnCancel.Click -= (this.btnCancel_Click);
                    },
                    (c) =>
                    {
                    });
                    this.WriteLog("PasswordRetry objects are released successfully.");

                }
                disposed = true;
            }
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="PasswordRetry"/> is reclaimed by garbage collection.
        /// </summary>
        ~PasswordRetry()
        {
            Dispose(false);
        }

        #endregion   
    }
}
