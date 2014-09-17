#region Namespaces

using System;
using System.Windows;
using BMC.Common.ExceptionManagement;
using BMC.Security.Interfaces;
using BMC.Security;
using BMC.Common;
using BMC.Common.Security;
using BMC.CashDeskOperator.BusinessObjects;

#endregion Namespaces

namespace BMC.ExchangeConfig
{
    public partial class Login
	{
        #region Declarations

        //private readonly oCommonUtilities _oCommonutilities = oCommonUtilities.CreateInstance();      
        private string _sKeyText = string.Empty;

        #endregion

        #region Constructor

        public Login()
		{   
            this.InitializeComponent();

            txtUname.Focus();            
            // Insert code required on object creation below this point.

            MessageBox.parentOwner = this;
        }

        #endregion Constructor

        #region Events

        void objKeyboard_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (((KeyboardInterface)sender).DialogResult == true)
            {
                _sKeyText = ((KeyboardInterface)sender).KeyString;
            }
        }

        private void txtUname_PreviewMouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (!Settings.OnScreenKeyboard)
                return;
            txtUname.Text = DisplayKeyboard(txtUname.Text, string.Empty);
            txtUname.SelectionStart = txtUname.Text.Length;
        }

        private void txtPwd_PreviewMouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (!Settings.OnScreenKeyboard)
                return;
            txtPWD.Password = DisplayKeyboard(string.Empty, "Pwd");
            txtPWD.SelectAll();
        }

        private void Login_Button_Click(object sender, RoutedEventArgs e)
        {
            LoginApplication();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        #endregion Events

        #region Private Methods

        public static SecurityHelper.LoginResults Checkuser(string strUser, string strPass)
        {
            try
            {
                IUser user;
                SecurityHelper.CreateInstance(oCommonUtilities.CreateInstance().GetConnectionString(), false);
                var result = SecurityHelper.Login(strUser, strPass, out user);
                if (result == SecurityHelper.LoginResults.LoginSuccesful || result == SecurityHelper.LoginResults.PasswordExpired)
                {
                    clsSecurity.UserID = user.SecurityUserID;
                    clsSecurity.UserName = user.UserName;
                }
                return result;
            }
            catch (ArgumentNullException anex)
            {
                throw anex;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                MessageBox.ShowBox("MessageID33", BMC_Icon.Error);
            }
            return SecurityHelper.LoginResults.LoginFailed;
        }

        private bool CheckBallyuser(string strUser, string strPass)
        {
            bool bResult;

            try
            {
                var oSecurityAuthenticate = new BallySecurityAuthentication();
                var objProperty = new BallySecurityProperty { UserName = strUser, Password = strPass };

                if (objProperty.UserName.ToUpper() != "BALLY")
                {
                    objProperty.Password = oSecurityAuthenticate.EncryptUser(objProperty);
                    bResult = oSecurityAuthenticate.ValidateUser(objProperty);
                }
                else
                    bResult = oSecurityAuthenticate.ValidateUser(objProperty);
            }
            catch (Exception ex)
            {
                bResult = false;
                ExceptionManager.Publish(ex);
                MessageBox.ShowBox("MessageID33", BMC_Icon.Error);
            }
            return bResult;
        }

        private void LoginApplication()
        {
            try
            {
                if (txtUname.Text.ToUpper() == "BALLY")
                {
                    if (CheckBallyuser(txtUname.Text, txtPWD.Password))
                    {
                        var objMainScreen = new MainScreen { UserName = txtUname.Text };
                        objMainScreen.Show();
                        Hide();
                    }
                    else
                    {
                        MessageBox.ShowBox("MessageID36", BMC_Icon.Error);
                    }
                }
                else
                {
                    var loginResult = Checkuser(txtUname.Text, txtPWD.Password);

                    if (loginResult == SecurityHelper.LoginResults.LoginSuccesful)
                    {
                        var objMainScreen = new MainScreen { UserName = SecurityHelper.CurrentUser.UserName };
                        objMainScreen.Show();

                        Hide();
                    }
                    else
                    {
                        if ((loginResult == SecurityHelper.LoginResults.PasswordExpired) || (loginResult == SecurityHelper.LoginResults.LoginReset))
                        {
                            if (loginResult == SecurityHelper.LoginResults.PasswordExpired)
                            {
                                MessageBox.ShowBox("MessageID30", BMC_Icon.Information);
                                this.Close();
                            }
                            else
                            {
                                MessageBox.ShowBox("MessageID31", BMC_Icon.Information);
                                this.Close();
                            }
                        }
                        else if (loginResult == SecurityHelper.LoginResults.MaxAttemptsExceeded)
                        {
                            MessageBox.ShowBox("MessageID32", BMC_Icon.Error);
                            this.Close();
                        }
                        else
                        {
                            MessageBox.ShowBox("MessageID17", BMC_Icon.Error);
                        }
                    }
                }
            }
            catch (ArgumentNullException anex)
            {
                ExceptionManager.Publish(anex);
                MessageBox.ShowBox("MessageID37", BMC_Icon.Error);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                MessageBox.ShowBox("MessageID33", BMC_Icon.Error);
            }
        }

        private string DisplayKeyboard(string keyText, string type)
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

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
                LoginApplication();
        }

        #endregion Private Methods        
    }
}