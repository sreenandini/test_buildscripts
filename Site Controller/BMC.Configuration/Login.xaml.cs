#region Namespaces

using System;
using System.Windows;
using BMC.Common.ExceptionManagement;
using BMC.Security.Interfaces;
using BMC.Security;
using BMC.Common;
using BMC.Common.Security;
//using BMC.CashDeskOperator.BusinessObjects;
using BMC.Business.CashDeskOperator.WebServices;
using BMC.Business.ExchangeConfig;
using BMC.Common.LogManagement;
using BMC.Transport.ExchangeConfig;
using System.Data.SqlClient;
using System.Windows.Input;

#endregion Namespaces

namespace BMC.ExchangeConfig
{
    public partial class Login
	{
        #region Declarations

        //private readonly oCommonUtilities _oCommonutilities = oCommonUtilities.CreateInstance();      
        private string _sKeyText = string.Empty;
        private bool isValidConnectionString = false;
        #endregion

        #region Constructor

        public Login()
		{   
            this.InitializeComponent();

            LogManager.WriteLog("Inside Login Constructor", LogManager.enumLogLevel.Info);

            ExchangeConfigRegistryEntities.ExchangeConnectionString = RegistrySettings.ExchangeConnectionString();
            //RefreshControls();
            txtUname.Focus();            
            // Insert code required on object creation below this point.

            try
            {
                SqlConnection sqlConnection = new SqlConnection(ExchangeConfigRegistryEntities.ExchangeConnectionString);                
                isValidConnectionString = true;
                tbCopyrightInfo.Text = Settings.CopyRightInfo; 
            }
            catch (ArgumentException aEx)
            {   
                ExceptionManager.Publish(aEx);
                isValidConnectionString = false;
            }

            MessageBox.parentOwner = this;
            txtPWD.CommandBindings.Add(new CommandBinding(ApplicationCommands.Paste, DisablePastePasswordField));
        }

        #endregion Constructor

        #region Events

        private void DisablePastePasswordField(object sender, ExecutedRoutedEventArgs e)
        {
            e.Handled = true;
        }

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

        private void txtEnterpriseServerURL_PreviewMouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (!Settings.OnScreenKeyboard)
                return;
            txtEnterpriseServerURL.Text = DisplayKeyboard(string.Empty, string.Empty);
            txtEnterpriseServerURL.SelectAll();
        }

        private void Login_Button_Click(object sender, RoutedEventArgs e)
        {
            LoginApplication();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void RefreshControls()
        {
            if (ExchangeConfigRegistryEntities.ExchangeConnectionString == string.Empty)
            {
                grdMessage.Visibility = Visibility.Visible;
                grdEnterpriseDetails.Visibility = Visibility.Visible;
            }
            else
            {
                grdMessage.Visibility = Visibility.Hidden;
                grdEnterpriseDetails.Visibility = Visibility.Hidden;
            }
        }

        #endregion Events

        #region Private Methods

        public static SecurityHelper.LoginResults Checkuser(string strUser, string strPass)
        {
            try
            {
                IUser user;
                SecurityHelper.CreateInstance(ExchangeConfigRegistryEntities.ExchangeConnectionString, false);
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
                MessageBox.ShowBox("MessageID74", BMC_Icon.Error);
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
                MessageBox.ShowBox("MessageID74", BMC_Icon.Error);
            }
            return bResult;
        }

        private void LoginApplication()
        {
            try
            {
               
                if (txtUname.Text == string.Empty)
                {
                    MessageBox.ShowBox("MessageID69", BMC_Icon.Information);
                    return;
                }
                if (txtPWD.Password == string.Empty)
                {
                    MessageBox.ShowBox("MessageID70", BMC_Icon.Information);
                    return;
                }

                if (ExchangeConfigRegistryEntities.ExchangeConnectionString == string.Empty || !isValidConnectionString ||
                    !DBSettings.GetSiteInfo() || !DBSettings.GetUserInfo())
                {
                    if (txtUname.Text.ToUpper() == "CASH" && txtPWD.Password.ToUpper() == "DESK")
                    {
                        var objMainScreen = new MainScreen { UserName = txtUname.Text };
                        objMainScreen.Show();
                        Hide();
                    }
                    else
                    {
                        MessageBox.ShowBox("MessageID56", BMC_Icon.Error);
                    }
                }
                else
                {
                    var loginResult = Checkuser(txtUname.Text, txtPWD.Password);

                    if (loginResult == SecurityHelper.LoginResults.LoginSuccesful)
                    {
                        if (SecurityHelper.HasAccess("BMC.ExchangeConfig.Login"))
                        {
                            var objMainScreen = new MainScreen { UserName = SecurityHelper.CurrentUser.UserName };
                            objMainScreen.Show();
                            Hide();
                        }
                        else
                        {
                            MessageBox.ShowBox("MessageID105", BMC_Icon.Error, BMC_Button.OK);
                            return;
                        }
                       
                    }
                    else
                    {
                        if ((loginResult == SecurityHelper.LoginResults.PasswordExpired) || (loginResult == SecurityHelper.LoginResults.LoginReset))
                        {
                            if (loginResult == SecurityHelper.LoginResults.PasswordExpired)
                            {
                                MessageBox.ShowBox("MessageID71", BMC_Icon.Information);
                                this.Close();
                            }
                            else
                            {
                                MessageBox.ShowBox("MessageID72", BMC_Icon.Information);
                                this.Close();
                            }
                        }
                        else if (loginResult == SecurityHelper.LoginResults.MaxAttemptsExceeded)
                        {
                            MessageBox.ShowBox("MessageID73", BMC_Icon.Error);
                            this.Close();
                        }
                        else
                        {
                            MessageBox.ShowBox("MessageID56", BMC_Icon.Error);
                        }
                    }
                }
            }
            catch (ArgumentNullException anex)
            {
                ExceptionManager.Publish(anex);
                MessageBox.ShowBox("MessageID87", BMC_Icon.Error);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                MessageBox.ShowBox("MessageID74", BMC_Icon.Error);
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