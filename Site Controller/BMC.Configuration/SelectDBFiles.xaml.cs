namespace BMC.ExchangeConfig
{
    #region Namespaces

    using System;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Documents;
    using System.Windows.Input;
    using BMC.Business.ExchangeConfig;
    using BMC.Common.ExceptionManagement;
    using BMC.Common.LogManagement;
    using BMC.Common.ConfigurationManagement;
    using System.Xml;
    using System.Configuration;
    using System.Data.SqlClient;
    using Microsoft.SqlServer.Management.Smo;
    using System.IO;
    using Microsoft.SqlServer.Management.Common;
    using System.Reflection;
    using System.Windows.Data;
    using System.Globalization;

    #endregion Namespaces

	public partial class SelectDBFiles : Window
	{
        private delegate bool DirectoryExistsDelegate(string folder);

        List<string> lstMdfFile = new List<string>();
        List<string> lstLdfFile = new List<string>();
        private string _sKeyText = string.Empty;

        System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();

        private string initText = @"c$\ExchangeDatabase";

        #region Properties

        private string _strDataFilePath;
        private string _strLogFilePath;

        private bool _bIsCancelled = false;

        public string StrDataFilePath
        {
            get { return _strDataFilePath; }
        }

        public string StrLogFilePath
        {
            get { return _strLogFilePath; }
        }

        public bool bIsCancelled
        {
            get { return _bIsCancelled; }
        }

        #endregion Properties

        public SelectDBFiles(bool _bExchangeDBExists, bool _bTicketingDBExists, bool _bExtSysMsgDBExists, string _strServerName)
        {
            this.InitializeComponent();
            if (!_bExchangeDBExists)
            {
                lstMdfFile.Add("Exchange.mdf");
                lstLdfFile.Add("Exchange.ldf");
            }
            if (!_bTicketingDBExists)
            {
                lstMdfFile.Add("Ticketing.mdf");
                lstLdfFile.Add("Ticketing.ldf");
            }
            if (!_bExtSysMsgDBExists)
            {
                lstMdfFile.Add("ExtSysMsg.mdf");
                lstLdfFile.Add("ExtSysMsg.ldf");
            }

            txtDataServer.Text = @"\\" + _strServerName + @"\";
            txtLogServer.Text = @"\\" + _strServerName + @"\";

            txtDataServer.ToolTip= txtDataServer.Text;
            txtLogServer.ToolTip = txtLogServer.Text;

            txtDataFilePath.Text=initText;
            txtLogFilePath.Text=initText;

            txtDataFilePath.GotKeyboardFocus += new KeyboardFocusChangedEventHandler(txtFilePath_GotKeyboardFocus);
            txtDataFilePath.LostKeyboardFocus += new KeyboardFocusChangedEventHandler(txtFilePath_LostKeyboardFocus);

            txtLogFilePath.GotKeyboardFocus += new KeyboardFocusChangedEventHandler(txtFilePath_GotKeyboardFocus);
            txtLogFilePath.LostKeyboardFocus += new KeyboardFocusChangedEventHandler(txtFilePath_LostKeyboardFocus);
        }

        private void txtFilePath_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (sender is TextBox)
            {
                if (((TextBox)sender).Text.Trim().Equals(""))
                {
                    ((TextBox)sender).Text = initText;
                }
            }
        }

        private void txtFilePath_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (sender is TextBox)
            {
                if (((TextBox)sender).Text == initText)
                {
                    ((TextBox)sender).Text = "";
                }
            }
        }

        //private void txtDataFilePath_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        //{
        //    if (sender is TextBox)
        //    {
        //        if (((TextBox)sender).Text.Trim().Equals(""))
        //        {
        //            ((TextBox)sender).Text = initText;
        //        }
        //    }
        //}

        //private void txtDataFilePath_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        //{
        //    if (sender is TextBox)
        //    {
        //        if (((TextBox)sender).Text == initText)
        //        {
        //            ((TextBox)sender).Text = "";
        //        }
        //    }
        //}

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {   
        }

        bool DirectoryExistsTimeout(string path, int millisecondsTimeout)
        {
            try
            {
                DirectoryExistsDelegate callback = new DirectoryExistsDelegate(Directory.Exists);
                IAsyncResult result = callback.BeginInvoke(path, null, null);

                if (result.AsyncWaitHandle.WaitOne(millisecondsTimeout, false))
                {
                    return callback.EndInvoke(result);
                }
                else
                {
                    callback.EndInvoke(result);

                    return false;
                }
            }

            catch (Exception)
            {
                return false;
            }
        }

        private void btn_OK_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string strDataFilePath = txtDataServer.Text + txtDataFilePath.Text.Trim().Replace(':', '$');
                string strlogFilePath = txtLogServer.Text + txtLogFilePath.Text.Trim().Replace(':', '$');

                if (txtDataFilePath.Text.Trim() == string.Empty)
                {
                    MessageBox.ShowText("Please select Database Data File Path", BMC_Icon.Information);
                    txtDataFilePath.Focus();
                    return;
                }
                if (txtLogFilePath.Text.Trim() == string.Empty)
                {
                    MessageBox.ShowText("Please select Database Log File Path", BMC_Icon.Information);
                    txtLogFilePath.Focus();
                    return;
                }

                if (!DirectoryExistsTimeout(strDataFilePath, 5000))
                {
                    MessageBox.ShowText("Data File Path is not valid. Please select a valid path", BMC_Icon.Error);
                    txtDataFilePath.SelectAll();
                    txtDataFilePath.Focus();
                    return;
                }

                if (!DirectoryExistsTimeout(strlogFilePath, 5000))
                {
                    MessageBox.ShowText("Log File Path is not valid. Please select a valid path", BMC_Icon.Error);
                    txtLogFilePath.SelectAll();
                    txtLogFilePath.Focus();
                    return;
                }

                foreach (string mdf in lstMdfFile)
                {
                    if (File.Exists(strDataFilePath + "\\" + mdf))
                    {
                        MessageBox.ShowText("Select another Database Data File Path. " + mdf + " already located in this path", BMC_Icon.Error);
                        txtDataFilePath.SelectAll();
                        txtDataFilePath.Focus();
                        return;
                    }
                }

                foreach (string ldf in lstLdfFile)
                {
                    if (File.Exists(strlogFilePath + "\\" + ldf))
                    {
                        MessageBox.ShowText("Select another Database Log File Path. " + ldf + " already located in this path", BMC_Icon.Error);
                        txtDataFilePath.SelectAll();
                        txtDataFilePath.Focus();
                        return;
                    }
                }
                _strDataFilePath = strDataFilePath;
                _strLogFilePath = strlogFilePath;
                this.Close();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            _bIsCancelled = true;
            this.Close();
        }

        private void txtDataFilePath_PreviewMouseUp_1(object sender, MouseButtonEventArgs e)
        {
            if (!Settings.OnScreenKeyboard)
                return;
            txtDataFilePath.Text = DisplayKeyboard(txtDataFilePath.Text, string.Empty);
            txtDataFilePath.SelectionStart = txtDataFilePath.Text.Length;
        }

        private void txtLogFilePath_PreviewMouseUp_1(object sender, MouseButtonEventArgs e)
        {
            if (!Settings.OnScreenKeyboard)
                return;
            txtLogFilePath.Text = DisplayKeyboard(txtLogFilePath.Text, string.Empty);
            txtLogFilePath.SelectionStart = txtLogFilePath.Text.Length;
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
            objKeyboard.Top = Top + Height/2;
            objKeyboard.Left = Left + Width / 2 - objKeyboard.Width / 2;
            objKeyboard.Owner = this;
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

    }
    #region ToolTipVisibilityClass
    internal sealed class IsStringNonemptyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !String.IsNullOrEmpty(value as string);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    #endregion
}