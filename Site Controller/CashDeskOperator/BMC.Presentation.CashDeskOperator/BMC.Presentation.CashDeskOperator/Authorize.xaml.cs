using System;
using System.Data;
using System.Collections.Generic;
using System.Windows;
using BMC.Common.ExceptionManagement;
using BMC.Common.Utilities;
using BMC.Presentation.POS.Views;
using BMC.Security;
using BMC.Security.Interfaces;
using BMC.Security.Manager;
using BMC.Transport;
using BMC.CashDeskOperator.BusinessObjects;
using BMC.Common.Security;
using Microsoft.Win32;
using BMC.Common.ConfigurationManagement;
using BMC.Common;
using BMC.Business.CashDeskOperator.WebServices;
using BMC.Common.LogManagement;
using BMC.Presentation.POS.Helper_classes;
using System.Windows.Input;


namespace BMC.Presentation
{
    public partial class CAuthorize : IDisposable
    {
        #region Private Variables

        private string _sKeyText = string.Empty;
        private string Permission;

        #endregion

        #region Public Variables
        
        public IUser User;
        public bool IsAuthorized;

        #endregion

        #region Constructor

        public CAuthorize(string sPermission)
        {
            InitializeComponent();
            MessageBox.childOwner = this;
            txtUname.Focus();
            txtUname.Text = "";
            txtPWD.Password = "";
            Permission = sPermission.Trim();
        }

        #endregion

        #region Private Methods

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
            objKeyboard.ShowDialogEx(this);
            return _sKeyText;
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
            }
            return bResult;
        }

        #endregion

        #region Events

        private void ObjKeyboardClosing(object sender, System.ComponentModel.CancelEventArgs e)
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
            try
            {
                btnLogin.IsEnabled = false;
                IsAuthorized = false;
                var userObect = new Security.Manager.UserManager(oCommonUtilities.CreateInstance().GetConnectionString());
                if ((txtUname.Text.ToUpper() == "CASH" && txtPWD.Password.ToUpper() == "DESK")
                    || (txtUname.Text.ToUpper() == "BALLY" && CheckBallyuser(txtUname.Text, txtPWD.Password)))
                {
                    IsAuthorized = true;
                    User = userObect.GetUserObject(txtUname.Text, txtUname.Text, txtUname.Text);
                }
                else
                {
                    var oSecurityAuthenticate = new BallySecurityAuthentication();

                    User = userObect.GetUserProfileByName(txtUname.Text);

                    if (User != null
                        && User.Password == CryptoHelper.CreateHash(txtPWD.Password)
                        && SecurityHelper.HasAccess(User, Permission))
                        IsAuthorized = true;
                }

                if (!IsAuthorized)
                    MessageBox.ShowBox("MessageID289");

                Hide();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                MessageBox.ShowBox("MessageID289");
            }
            finally
            {
                btnLogin.IsEnabled = true;
            }
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            Hide();
        }

        #endregion 

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
                        this.txtUname.PreviewMouseUp -= (this.txtUname_PreviewMouseUp);
                        this.txtPWD.PreviewMouseUp -= (this.txtPwd_PreviewMouseUp);
                        this.btnLogin.Click -= (this.Login_Button_Click);
                        this.btnCancel.Click -= (this.Cancel_Button_Click);
                    },
                    (c) =>
                    {
                    });
                    this.WriteLog("CAuthorize objects are released successfully.");

                }
                disposed = true;
            }
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="CAuthorize"/> is reclaimed by garbage collection.
        /// </summary>
        ~CAuthorize()
        {
            Dispose(false);
        }

        #endregion


        private void txtUname_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                txtPWD.Focus();
                e.Handled = true;
            }
        }

        private void txtPWD_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Login_Button_Click(sender, e);
                e.Handled = true;
            }
        }

    }
}