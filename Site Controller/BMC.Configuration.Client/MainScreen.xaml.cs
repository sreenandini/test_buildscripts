/*******************************************************************************************************
 *  Revision History
 *  Name            TrackCode   Modified Date   Change Description
 *  Selva Kumar S   S001        31st Jul 2012   Save/Load the Cash dispenser server info to and from 
 *                                              windows registry based on cash dispenser DB setting
 * ****************************************************************************************************/

namespace BMC.ExchangeConfig
{
    #region Namespaces

    using System;
    using System.IO;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Xml;
    using System.Data;
    using System.Text;
    using System.Collections.Generic;
    using BMC.Business.ExchangeConfig;
    using BMC.Monitoring;
    using BMC.Transport.ExchangeConfig;
    using BMC.DBInterface.ExchangeConfig;
    using BMC.Common.LogManagement;
    using BMC.Common.ExceptionManagement;
    using System.Diagnostics;
    using System.Text.RegularExpressions;
    using BMC.Common.ConfigurationManagement;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Configuration;
    using Microsoft.Win32;
    using System.Windows.Input;
    using BMC.Common.Utilities;

    #endregion

    #region MainScreenClass

    public partial class MainScreen
    {
        #region Declarations

        private string s_UserName = "John Smith";
        static bool b_Logout = false;
        public string Skin1 = "Resources/BMCBlueTheme.xaml";
        public string Skin2 = "Resources/BMCGreenTheme.xaml";
        string strConnection = string.Empty;
        private string ReturnConnectionString = string.Empty;
        List<PropertyClass> ChangedProperty = new List<PropertyClass>();
        DataTable SettingsTable = null;
        private string strRegistryKeyPath = string.Empty;
        private string strScriptPath = string.Empty;
        private string strUpgradeVisible = string.Empty;        
        private string strServiceStatus = string.Empty;
        private PropertyBag PropertyHolder = new PropertyBag();        
        private string strUrlvalidate = string.Empty;        
        private string _exchangeServerName = string.Empty;
        private string sProtocol = string.Empty;
        private ExchangeConfigurationEntity exchangeConfigEntity = new ExchangeConfigurationEntity();        
        private string classText = string.Empty;
        private string _sKeyText = string.Empty;
        private string strIsCDEnabled = string.Empty;   //+S001
        private string strCDType = string.Empty;        //+S001
        #endregion

        #region Constructor

        public MainScreen()
        {   
            this.InitializeComponent();
            IntialiseDate();
            b_Logout = false;

            if (!Settings.IsLoginRequired)
            {
                lblUsername.Visibility = Visibility.Hidden;
                btnLogout.Visibility = Visibility.Hidden;
            }

            MessageBox.parentOwner = this;
            txtExchangePassword.CommandBindings.Add(new CommandBinding(ApplicationCommands.Paste,DisablePastePasswordField));
        }
       
        #endregion Constructor

        #region Properties

        public string UserName
        {
            get { return s_UserName; }
            set { s_UserName = value; }
        }
       
        public static readonly DependencyProperty TextBackgroundProperty =
            DependencyProperty.Register("BackgroundBrush", typeof(Color), typeof(ControlBoxButtons), new UIPropertyMetadata(null));

        #endregion Properties

        #region Events

        private void DisablePastePasswordField(object sender, ExecutedRoutedEventArgs e)
        {
            e.Handled = true;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                //get the settings from the config file      
                GetInitialSettings();

                //Retrieve the server settings for Exchange,Ticketing and CMP
                GetSettings();

                //Get Log path
                GetLogPath();

                RefreshControls();
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("frmBMCExchangeConfig_Load" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
            }
        }

        void Menu_Clicked(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }

        private void btnExchangeTestConnection_Click(object sender, RoutedEventArgs e)
        {
            bool bTestConnection = false;
            try
            {
                Cursor = System.Windows.Input.Cursors.Wait;

                exchangeConfigEntity.ExchangeServer = txtExchangeServer.Text;
                exchangeConfigEntity.ExchangeServerUserName = txtExchangeUsername.Text;
                exchangeConfigEntity.ExchangeServerPassword = txtExchangePassword.Password;

                if (ValidateExchangeServer())
                {
                    bTestConnection = TestConnection(exchangeConfigEntity.ExchangeServer, exchangeConfigEntity.ExchangeServerUserName, exchangeConfigEntity.ExchangeServerPassword, txtExchangeTimeout.Text, txtExchangeInstance.Text, 'E');

                    if (bTestConnection)
                    {
                        MessageBox.ShowBox("MessageID20", BMC_Icon.Information);

                        #region +S001 START
                        strIsCDEnabled = DBSettings.GetSettingValue(strConnection, "CashDispenserEnabled");
                        strCDType = DBSettings.GetSettingValue(strConnection, "CashDispenserType");

                        if (strIsCDEnabled.ToUpper() == "TRUE" && strCDType.ToUpper() == ConfigurationManager.AppSettings.Get("CashDispenserType"))
                        {
                            tiCash_Dispenser.Visibility = Visibility.Visible;
                            tiCash_Dispenser.Focus();
                        }
                        else
                        {
                            if (tiCash_Dispenser.Visibility == Visibility.Visible)
                                tiCash_Dispenser.Visibility = Visibility.Collapsed;
                        }
                        #endregion +S001 END
                    }
                    else
                    {
                        MessageBox.ShowBox("MessageID34", BMC_Icon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                MessageBox.ShowBox("MessageID33", BMC_Icon.Error);
            }
            finally
            {
                Cursor = System.Windows.Input.Cursors.Arrow;
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void lstLeftPane_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadView((((System.Windows.Controls.ListBox)sender).SelectedItem as XmlElement).Attributes["Caption"].Value);
        }

        private void btnLogout_PreviewMouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Login objLogin = new Login();
            this.Hide();
            objLogin.Show();
            b_Logout = true;            
            this.Close();
        }

        protected override void OnClosed(EventArgs e)
        {
            if (!b_Logout)
                App.Current.Shutdown();
        }

        private void btnTheme_PreviewMouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ResourceDictionary newSkin = new ResourceDictionary();
            newSkin.Source = new Uri(Skin1, UriKind.Relative);

            App.Current.Resources.MergedDictionaries.Clear();
            App.Current.Resources.MergedDictionaries.Add(newSkin);

            string oldSkin = Skin1;
            Skin1 = Skin2;
            Skin2 = oldSkin;
        }

        private void btnSaveSettings_Click(object sender, RoutedEventArgs e)
        {

            Regex objRegexUrlvalidate = new Regex("^(http|ftp|https)://(www\\.)?.+\\.(com|net|org|asmx)$");
            MatchCollection objMatchCollect;

            string strEncryptExchangeConnection = string.Empty;
            string strEncryptExchangeConnectionHex = string.Empty;
            bool bTestExchangeConnection = false;
            Dictionary<string, string> dictSetregistryentries;
            Dictionary<string, string> dictSetNetLoggerRegistryEntry;
            try
            {
                if (MessageBox.ShowBox("MessageID6", BMC_Icon.Question, BMC_Button.YesNo) == System.Windows.Forms.DialogResult.Yes)                    
                {
                    Cursor = System.Windows.Input.Cursors.Wait;
                    dictSetregistryentries = new Dictionary<string, string>();
                    dictSetNetLoggerRegistryEntry = new Dictionary<string, string>();

                    #region +S001 START
                    //Validates the txtbox for blank data if Cash Dispenser is enabled before save all settings
                    strIsCDEnabled = DBSettings.GetSettingValue(strConnection, "CashDispenserEnabled");
                    strCDType = DBSettings.GetSettingValue(strConnection, "CashDispenserType");

                    if (strIsCDEnabled.ToUpper() == "TRUE" && strCDType.ToUpper() == ConfigurationManager.AppSettings.Get("CashDispenserType"))
                    {
                        if (txtCDServer.Text.Trim() == string.Empty | txtCDServerPort.Text.Trim() == string.Empty |
                            txtCDPassword.Password.Trim() == string.Empty | txtCDUsername.Text.Trim() == string.Empty |
                            txtCDDevicename.Text.Trim() == string.Empty)
                        {
                            MessageBox.ShowBox("MessageID41", BMC_Icon.Information);
                            tiCash_Dispenser.Focus();
                            return;
                        }
                    }
                    #endregion +S001 END

                    //Exchange Server save 
                    if (ValidateText(txtExchangeServer, "Server"))
                    {
                        if (ValidateText(txtExchangeServer, "UserName"))
                        {
                            if (ValidatePasswordBox(txtExchangePassword))
                            {
                                if (ValidateText(txtExchangeTimeout, "Connection Timeout"))
                                {
                                    bTestExchangeConnection = TestConnection(txtExchangeServer.Text, txtExchangeUsername.Text, txtExchangePassword.Password, txtExchangeTimeout.Text,txtExchangeInstance.Text, 'E');
                                 
                                }
                            }
                        }
                    }
                    if (bTestExchangeConnection == true)
                    {
                        if (!string.IsNullOrEmpty(strConnection))
                            ExchangeConfigRegistryEntities.ExchangeConnectionString = strConnection;
                        strEncryptExchangeConnection = RegistrySettings.EncryptExchangeConnection();
                        if (!string.IsNullOrEmpty(strEncryptExchangeConnection))
                        {
                            dictSetregistryentries.Add(UIConstants.strSQLConnect, strEncryptExchangeConnection + "+" + "REG_SZ");                            
                        }
                        strEncryptExchangeConnectionHex = RegistrySettings.EncryptExchangeConnectionHex();
                        if (!string.IsNullOrEmpty(strEncryptExchangeConnectionHex))
                        {
                            dictSetregistryentries.Add(UIConstants.strSQLConnectEx, strEncryptExchangeConnectionHex + "+" + "REG_SZ");                            
                        }
                                                
                        //Ticket Connection Save
                        try
                        {
                            LogManager.WriteLog("Saving Ticketing Connection String...", LogManager.enumLogLevel.Info);

                            string ticketServerName = string.Empty;
                            ticketServerName = txtExchangeInstance.Text.Trim() == string.Empty ? txtExchangeServer.Text : string.Format("{0}\\{1}", txtExchangeServer.Text, txtExchangeInstance.Text);

                            RegistrySettings.SetTicketConnectionString(ticketServerName, UIConstants.strTicketingDBName, txtExchangeUsername.Text, txtExchangePassword.Password, Convert.ToInt32(txtExchangeTimeout.Text));

                            LogManager.WriteLog("Ticketing Connection String saved successfully.", LogManager.enumLogLevel.Info);
                        }
                        catch (Exception ex)
                        {
                            ExceptionManager.Publish(ex);
                        }
                    }
                    else
                    {
                        MessageBox.ShowBox("MessageID7", BMC_Icon.Error);
                    }

                    System.Windows.Forms.Application.DoEvents();

                    #region +S001 START
                    //Saves Cash dispenser server details only if the Cash Dispenser is enabled
                    if (strIsCDEnabled.ToUpper() == "TRUE" && strCDType.ToUpper() == ConfigurationManager.AppSettings.Get("CashDispenserType"))
                    {
                        string strCDSetting = txtCDServer.Text.Trim() + ";" + txtCDServerPort.Text.Trim() + ";" + txtCDUsername.Text.Trim() + ";" + txtCDDevicename.Text.Trim();

                        if (chkUseSSL.IsChecked.Value)
                            strCDSetting += ";true";
                        else
                            strCDSetting += ";false";

                        string strEncryptCDSetting = RegistryBuilder.EncryptCDSetting(strCDSetting);

                        if (!string.IsNullOrEmpty(strEncryptCDSetting))
                        {
                            dictSetregistryentries.Add(UIConstants.strCDServerInfo, strEncryptCDSetting + "+" + "REG_SZ");

                            strEncryptCDSetting = string.Empty;

                            strCDSetting = txtCDPassword.Password;

                            strEncryptCDSetting = RegistryBuilder.EncryptCDSetting(strCDSetting);

                            if (!string.IsNullOrEmpty(strEncryptCDSetting))
                            {
                                dictSetregistryentries.Add(UIConstants.strCDServerPwd, strEncryptCDSetting + "+" + "REG_SZ");
                            }
                        }
                        System.Windows.Forms.Application.DoEvents();
                    }
                    #endregion +S001 END

                    if ((txtEnterpriseweburl.Text.Trim().Length) > 0)
                    {   
                        if (strUrlvalidate == string.Empty)//If test functionality not used                             
                            dictSetregistryentries.Add(UIConstants.strBGSWebservice.ToString(), txtEnterpriseweburl.Text.Trim() + "+" + "REG_SZ");
                        else
                            dictSetregistryentries.Add(UIConstants.strBGSWebservice.ToString(), strUrlvalidate + "+" + "REG_SZ");
                    }
                    else
                        MessageBox.ShowBox("MessageID8", BMC_Icon.Error);

                    System.Windows.Forms.Application.DoEvents();

                    //Bind IP address                    
                    if (txtBindIPAddress.Text != string.Empty)
                    {
                        dictSetregistryentries.Add(UIConstants.strBindIP.ToString(), txtBindIPAddress.Text.ToString() + "+" + "REG_SZ");
                    }  
                   
                    // Defaulut IP Address
                    if (txtDefaultServerIP.Text != string.Empty)
                    {
                        dictSetregistryentries.Add(UIConstants.strDefaultServerIP.ToString(), txtDefaultServerIP.Text.ToString() + "+" + "REG_SZ");
                    }                    

                    #region CommsConfig
                    string sInstallPath = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
                    string sCommsConfigLocations = string.Empty;
                    string sComConfigFileName = string.Empty;
                    
                    sComConfigFileName = ConfigurationManager.AppSettings.Get("CommsConfigName");
                    sCommsConfigLocations = ConfigurationManager.AppSettings.Get("CommsConfigLocations");

                    sComConfigFileName = sComConfigFileName == null ? "" : sComConfigFileName;
                    sCommsConfigLocations = sCommsConfigLocations == null ? "" : sCommsConfigLocations;

                    List<string> slstCommsConfigLocations = new List<string>();

                    slstCommsConfigLocations.AddRange(sCommsConfigLocations.Split(','));

                    if (sComConfigFileName.Trim() != string.Empty)
                    {
                        //Writes new config file
                        if (!ComsConfig.SaveClientConfig(sInstallPath, sComConfigFileName, txtDefaultServerIP.Text.Trim(), txtBindIPAddress.Text.Trim(), slstCommsConfigLocations))
                        {
                            LogManager.WriteLog("CommsConfig File Creation error.", LogManager.enumLogLevel.Error);
                        }
                    }
                    #endregion CommsConfig

                    //Command Time Out
                    BMCRegistryHelper.SetRegKeyValue(string.Empty, UIConstants.SQLCommandTimeOut, RegistryValueKind.String, txtExchangeCommandTimeout.Text);

                    //DSN settings
                    ExchangeConfigRegistryEntities.ODBCRegKeyValue = UIConstants.strODBCRegPath;
                    System.Windows.Forms.Application.DoEvents();

                    //Save all Registry Settings under cash master
                    RegistrySettings.SetRegistryEntries(dictSetregistryentries, "Software\\Honeyframe\\"+ExchangeConfigRegistryEntities.RegistryKeyValue);

                    //Save all Registry Settings under Honeyframe
                    RegistrySettings.SetRegistryEntries(dictSetregistryentries, ExchangeConfigRegistryEntities.HoneyFrameKeyValue);
                    System.Windows.Forms.Application.DoEvents();

                    //Save all Registry Settings under NetLogger                    
                    RegistrySettings.SetRegistryEntries(dictSetNetLoggerRegistryEntry, "Software\\Honeyframe\\" + ExchangeConfigRegistryEntities.NetLoggerRegKeyValue);
                    
                    //To save log path
                    if (txtFileChoose.Text.Trim() != string.Empty)
                    {
                        if (Directory.Exists(txtFileChoose.Text.Trim()))
                        {
                            BMCRegistryHelper.SetRegKeyValue(string.Empty, UIConstants.DefaultLogDir, RegistryValueKind.String, txtFileChoose.Text.Trim());
                        }
                        else
                        {
                            System.Windows.Forms.DialogResult dlgResult = MessageBox.ShowBox("MessageID46", BMC_Icon.Question, BMC_Button.YesNo);//Folder does not exists. Do you want to create?
                            if (dlgResult == System.Windows.Forms.DialogResult.Yes)
                            {
                                Directory.CreateDirectory(txtFileChoose.Text.Trim());
                                BMCRegistryHelper.SetRegKeyValue(string.Empty, UIConstants.DefaultLogDir, RegistryValueKind.String, txtFileChoose.Text.Trim());
                            }
                            else
                            {
                                MessageBox.ShowBox("MessageID47", BMC_Icon.Information);//Folder does not exists.
                                txtFileChoose.Focus();
                                return;
                            }
                        }
                    }
                    else
                    {
                        MessageBox.ShowBox("MessageID44", BMC_Icon.Information);//Please enter a valid logfile path.
                        txtFileChoose.Focus();
                        return;
                    }

                    System.Windows.Forms.Application.DoEvents();


                    MessageBox.ShowBox("MessageID9", BMC_Icon.Information);

                    tiServicesSetup.IsEnabled = true;
                    tiSummary.IsEnabled = true;
                    tiSummary.Focus();
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Save Button Settings" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
            }
            finally
            {
                Cursor = System.Windows.Input.Cursors.Arrow;
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("btnExit_Click" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
                MessageBox.ShowBox("MessageID33", BMC_Icon.Error);
            }
        }

        private void btnTestURL_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Cursor = System.Windows.Input.Cursors.Wait;

                CheckWebService(txtEnterpriseweburl.Text);
            }            
            catch (System.Security.Authentication.AuthenticationException aEx)
            {
                ExceptionManager.Publish(aEx);
                MessageBox.ShowBox("MessageID38", BMC_Icon.Error);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                MessageBox.ShowBox("MessageID5", BMC_Icon.Error);
            }
            finally
            {
                Cursor = System.Windows.Input.Cursors.Arrow;
            }
        }

        private void TabItem_GotFocus(object sender, RoutedEventArgs e)
        {
            DataTable dtSummary = new DataTable("Summary");

            dtSummary.Columns.Add("Name", typeof(string));
            dtSummary.Columns.Add("Value", typeof(string));

            dtSummary.Rows.Add("Exchange DB Server", txtExchangeServer.Text);
            dtSummary.Rows.Add("Exchange DB Instance", txtExchangeInstance.Text);
            dtSummary.Rows.Add("Exchange DB Username", txtExchangeUsername.Text);
            dtSummary.Rows.Add("Exchange DB Name", tbExchangeDBNameValue.Text);
            dtSummary.Rows.Add("Exchange DB Time Out", txtExchangeTimeout.Text);
            dtSummary.Rows.Add("Exchange DB Command Time Out", txtExchangeCommandTimeout.Text);

            #region +S001 START
            //Populate Summay tab only if Cash dispenser is enabled
            if (strIsCDEnabled.ToUpper() == "TRUE" && strCDType.ToUpper() == ConfigurationManager.AppSettings.Get("CashDispenserType"))
            {
                dtSummary.Rows.Add("Cash Dispenser Server", txtCDServer.Text);
                dtSummary.Rows.Add("Cash Dispenser Server Port", txtCDServerPort.Text);
                dtSummary.Rows.Add("Cash Dispenser Username", txtCDUsername.Text);
                dtSummary.Rows.Add("Cash Dispenser Device", txtCDDevicename.Text);
                dtSummary.Rows.Add("Cash Dispenser SSL Connection", (chkUseSSL.IsChecked.Value) ? "true" : "false");
            }
            #endregion +S001 END

            dtSummary.Rows.Add("Enterprise Server URL", txtEnterpriseweburl.Text);

            dtSummary.Rows.Add("Bind IP Address", txtBindIPAddress.Text);

            dtSummary.Rows.Add("Default Server IP", txtDefaultServerIP.Text);

            dtSummary.Rows.Add("Log Path", txtFileChoose.Text);

            lvSummary.DataContext = dtSummary;
        }

        private void pbSetting_GetValue(object sender, PropertySpecEventArgs e)
        {
            e.Value = BMC.Common.Utilities.Common.GetRowValue<string>(SettingsTable.Select("Name = '" + e.Property.Name + "'")[0], "Value");
        }

        private void pbSetting_SetValue(object sender, PropertySpecEventArgs e)
        {
            PropertyClass Pb = new PropertyClass();
            Pb.Name = e.Property.Name;
            Pb.Value = e.Value.ToString();
            ChangedProperty.Add(Pb);
            SettingsTable.Select("Name = '" + e.Property.Name + "'")[0]["Value"] = e.Value;
        }

        //private void btnCreateODBCDSN_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        Cursor = System.Windows.Input.Cursors.Wait;

        //        ODBCDSNSetup odbcDSNSetup = new ODBCDSNSetup(txtExchangeServer.Text, txtExchangeUsername.Text, txtExchangePassword.Password);
        //        odbcDSNSetup.ShowDialog();
        //    }
        //    finally
        //    {
        //        Cursor = System.Windows.Input.Cursors.Arrow;
        //    }
        //}

        private void chkTrustedSite_Checked(object sender, RoutedEventArgs e)
        {
        }

        private void chkTrustedSite_Unchecked(object sender, RoutedEventArgs e)
        {
           
        }

        private void txtExchangePassword_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (tbExchangePasswordRequired.Visibility == Visibility.Visible)
            {
                tbExchangePasswordRequired.Visibility = Visibility.Hidden;
                txtExchangePassword.ToolTip = null;
            }
        }

        private void txtExchangePassword_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (txtExchangePassword.Password.Trim() == string.Empty)
            {
                tbExchangePasswordRequired.Visibility = Visibility.Visible;
                ValidatePasswordBox(txtExchangePassword);
            }
        }

        private void txtExchangePassword_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtExchangePassword.Password.Trim() == string.Empty)
            {
                tbExchangePasswordRequired.Visibility = Visibility.Visible;
                ValidatePasswordBox(txtExchangePassword);
            }
        }

        private void gpWebServiceSetup_Loaded(object sender, RoutedEventArgs e)
        {
            if (txtEnterpriseweburl.Text == string.Empty)
            {
                txtEnterpriseweburl.Text = string.Empty;
            }
            txtEnterpriseweburl.Focus();
        }

        private void txtExchangeServer_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!Settings.OnScreenKeyboard)
                return;
            txtExchangeServer.Text = DisplayKeyboard(txtExchangeServer.Text, string.Empty);
            txtExchangeServer.SelectionStart = txtExchangeServer.Text.Length;
        }

        private void txtExchangeInstance_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!Settings.OnScreenKeyboard)
                return;
            txtExchangeInstance.Text = DisplayKeyboard(txtExchangeInstance.Text, string.Empty);
            txtExchangeInstance.SelectionStart = txtExchangeInstance.Text.Length;
        }

        private void txtExchangeUsername_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!Settings.OnScreenKeyboard)
                return;
            txtExchangeUsername.Text = DisplayKeyboard(txtExchangeUsername.Text, string.Empty);
            txtExchangeUsername.SelectionStart = txtExchangeUsername.Text.Length;
        }

        private void txtExchangeTimeout_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!Settings.OnScreenKeyboard)
                return;
            txtExchangeTimeout.Text = DisplayNumberPad(txtExchangeTimeout);
            txtExchangeTimeout.SelectionStart = txtExchangeTimeout.Text.Length;
        }
        private void txtExchangeCommandTimeout_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!Settings.OnScreenKeyboard)
                return;
            
            string sCmdTimeOut = string.Empty;

            sCmdTimeOut = DisplayNumberPad(txtExchangeCommandTimeout);

            txtExchangeCommandTimeout.Text = String.IsNullOrEmpty(sCmdTimeOut) ? "0" : sCmdTimeOut;
            txtExchangeCommandTimeout.SelectionStart = txtExchangeCommandTimeout.Text.Length;
        }
        private void txtExchangePassword_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!Settings.OnScreenKeyboard)
                return;
            txtExchangePassword.Password = DisplayKeyboard(string.Empty, "Pwd");

            if (txtExchangePassword.Password.Trim() == string.Empty)
            {
                txtExchangePassword.Visibility = Visibility.Visible;
                ValidatePasswordBox(txtExchangePassword);
            }
            else
            {
                tbExchangePasswordRequired.Visibility = Visibility.Hidden;
                tbExchangePasswordRequired.ToolTip = null;
            }
        }

        private void txtEnterpriseweburl_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!Settings.OnScreenKeyboard)
                return;
            txtEnterpriseweburl.Text = DisplayKeyboard(txtEnterpriseweburl.Text, string.Empty);
            txtEnterpriseweburl.SelectionStart = txtEnterpriseweburl.Text.Length;
        }

        void ObjKeyboardClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (((KeyboardInterface)sender).DialogResult == true)
            {
                _sKeyText = ((KeyboardInterface)sender).KeyString;
            }
        }

        #region +S001 START
        private void txtCDServer_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!Settings.OnScreenKeyboard)
                return;
            txtCDServer.Text = DisplayKeyboard(txtCDServer.Text, string.Empty);
            txtCDServer.SelectionStart = txtCDServer.Text.Length;
        }

        private void txtCDServerPort_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!Settings.OnScreenKeyboard)
                return;
            txtCDServerPort.Text = DisplayKeyboard(txtCDServerPort.Text, string.Empty);
            txtCDServerPort.SelectionStart = txtCDServerPort.Text.Length;
        }

        private void txtCDUsername_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!Settings.OnScreenKeyboard)
                return;
            txtCDUsername.Text = DisplayKeyboard(txtCDUsername.Text, string.Empty);
            txtCDUsername.SelectionStart = txtCDUsername.Text.Length;
        }

        private void txtCDPassword_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!Settings.OnScreenKeyboard)
                return;
            txtCDPassword.Password = DisplayKeyboard(txtCDPassword.Password, "Pwd");

            if (txtCDPassword.Password.Trim() == string.Empty)
            {
                txtCDPassword.Visibility = Visibility.Visible;
                ValidatePasswordBox(txtCDPassword);
            }
            else
            {
                tbCDPasswordRequired.Visibility = Visibility.Hidden;
                tbCDPasswordRequired.ToolTip = null;
            }
        }

        private void txtCDDevicename_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!Settings.OnScreenKeyboard)
                return;
            txtCDDevicename.Text = DisplayKeyboard(txtCDDevicename.Text, string.Empty);
            txtCDDevicename.SelectionStart = txtCDDevicename.Text.Length;
        }

        private void txtCDPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtCDPassword.Password.Trim() == string.Empty)
            {
                txtCDPassword.Visibility = Visibility.Visible;
                ValidatePasswordBox(txtCDPassword);
            }
            else
            {
                tbCDPasswordRequired.Visibility = Visibility.Hidden;
                tbCDPasswordRequired.ToolTip = null;
            }
        }

        private void txtCDPassword_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtCDPassword.Password.Trim() == string.Empty)
            {
                txtCDPassword.Visibility = Visibility.Visible;
                ValidatePasswordBox(txtCDPassword);
            }
            else
            {
                tbCDPasswordRequired.Visibility = Visibility.Hidden;
                tbCDPasswordRequired.ToolTip = null;
            }
        }

        private void txtCDPassword_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtCDPassword.Password.Trim() == string.Empty)
            {
                txtCDPassword.Visibility = Visibility.Visible;
                ValidatePasswordBox(txtCDPassword);
            }
            else
            {
                tbCDPasswordRequired.Visibility = Visibility.Hidden;
                tbCDPasswordRequired.ToolTip = null;
            }
        }

        private void txtCDServerPort_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsNumber(e.Text, e.Text.Length - 1))
            {
                e.Handled = true;
            }
        }

        private void tiCash_Dispenser_Loaded(object sender, RoutedEventArgs e)
        {
            strIsCDEnabled = DBSettings.GetSettingValue(strConnection, "CashDispenserEnabled");
            strCDType = DBSettings.GetSettingValue(strConnection, "CashDispenserType");

            if (strIsCDEnabled.ToUpper() == "TRUE" && strCDType.ToUpper() == ConfigurationManager.AppSettings.Get("CashDispenserType"))
            {
                tiCash_Dispenser.Visibility = Visibility.Visible;
            }
            else
            {
                tiCash_Dispenser.Visibility = Visibility.Collapsed;
            }
        }

        private void gpCashDispenserSetup_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshCDControls();
        }

        private void btnSaveCDDetails_Click(object sender, RoutedEventArgs e)
        {
            bool bIsReadyToStore = false;
            bool bInsertSetting = false;
            Dictionary<string, string> dictSetregistryentries;
            string strCDSetting = string.Empty;
            string strEncryptCDSetting = string.Empty;
            try
            {
                Cursor = System.Windows.Input.Cursors.Wait;

                //Cash Dispenser Server save 
                if (ValidateText(txtCDServer, "Server"))
                {
                    if (ValidateText(txtCDUsername, "UserName"))
                    {
                        System.Windows.Controls.TextBox tempText = new System.Windows.Controls.TextBox();
                        tempText.Text = txtCDPassword.Password;
                        if (ValidateText(tempText, "Password"))
                        {
                            if (ValidateText(txtCDServerPort, "Server Port"))
                            {
                                if (ValidateText(txtCDDevicename, "Device Name"))
                                {
                                    bIsReadyToStore = true;
                                }
                            }
                        }
                    }
                }

                if (bIsReadyToStore == true)
                {
                    strCDSetting = txtCDServer.Text.Trim() + ";" + txtCDServerPort.Text.Trim() + ";" + txtCDUsername.Text.Trim() + ";" + txtCDDevicename.Text.Trim();

                    strEncryptCDSetting = RegistryBuilder.EncryptCDSetting(strCDSetting);

                    if (chkUseSSL.IsChecked.Value)
                        strCDSetting += ";true";
                    else
                        strCDSetting += ";false";

                    if (!string.IsNullOrEmpty(strEncryptCDSetting))
                    {
                        dictSetregistryentries = new Dictionary<string, string>();
                        dictSetregistryentries.Add(UIConstants.strCDServerInfo, strEncryptCDSetting + "+" + "REG_SZ");

                        strEncryptCDSetting = string.Empty;

                        strCDSetting = txtCDPassword.Password;

                        strEncryptCDSetting = RegistryBuilder.EncryptCDSetting(strCDSetting);

                        if (!string.IsNullOrEmpty(strEncryptCDSetting))
                        {
                            dictSetregistryentries.Add(UIConstants.strCDServerPwd, strEncryptCDSetting + "+" + "REG_SZ");

                            //Save all Registry Settings under cash master
                            bInsertSetting = RegistrySettings.SetRegistryEntries(dictSetregistryentries, ExchangeConfigRegistryEntities.RegistryKeyValue);
                            System.Windows.Forms.Application.DoEvents();

                            if (bInsertSetting == true)
                                MessageBox.ShowBox("MessageID40", BMC_Icon.Information);
                        }

                        System.Windows.Forms.Application.DoEvents();
                    }
                }
                else
                {
                    MessageBox.ShowBox("MessageID39", BMC_Icon.Information);
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("btnSaveCDDetails_Click" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
                MessageBox.ShowBox("MessageID33", BMC_Icon.Error);
            }
            finally
            {
                Cursor = System.Windows.Input.Cursors.Arrow;
            }
        }
        #endregion +S001 End

        #endregion Events

        #region Private Methods

        /// <summary>
        /// Initialise date
        /// </summary>
        private void IntialiseDate()
        {
            lblDate.Text = DateTime.Today.ToString("dddd, MMMM dd, yyyy");
            lblDate.Foreground = Brushes.White;
        }

        /// <summary>
        /// Load View
        /// </summary>
        /// <param name="ViewCaption"></param>
        private void LoadView(string ViewCaption)
        {

        }

        /// <summary>
        /// Validate the textbox details.
        /// </summary>
        /// <returns>success or failure</returns>
        /// Method Revision History
        private bool ValidateText(System.Windows.Controls.TextBox tBox, string Message)
        {
            bool bStatus = true;
            if (tBox.Text.Length == 0)
            {
                bStatus = false;
            }
            return bStatus;
        }

        /// <summary>
        /// Test database Connection
        /// </summary>
        /// <param name="strServer"></param>
        /// <param name="strUsername"></param>
        /// <param name="strPassword"></param>
        /// <param name="strTimeOut"></param>
        /// <param name="strInstance"></param>
        /// <param name="chDatabase"></param>
        /// <returns></returns>
        private bool TestConnection(string strServer, string strUsername, string strPassword, string strTimeOut, string strInstance, char chDatabase)
        {
            bool bTestConnection = false;
            string strServerName = string.Empty;
            try
            {
                if (strInstance.Trim().Length > 0)
                {
                    strServer = strServer + "\\" + strInstance;
                }
                //Test DB Connection for Exchange.
                if (chDatabase == 'E')
                {
                    bTestConnection = AddServerDetails(strServer, strUsername, strPassword, UIConstants.strExchangeDBName, strTimeOut);
                }
                else if (chDatabase == 'T')
                {
                    bTestConnection = AddServerDetails(strServer, strUsername, strPassword, UIConstants.strTicketingDBName, strTimeOut);
                }
                else if (chDatabase == 'C')
                {
                    bTestConnection = AddServerDetails(strServer, strUsername, strPassword, UIConstants.strCMPDBName, strTimeOut);
                }

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("ExchangeTestConnection" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
                MessageBox.ShowBox("MessageID33", BMC_Icon.Error);
            }
            return bTestConnection;
        }

        /// <summary>
        /// Test the DB Connection with the credentials entered
        /// </summary>
        /// <param name="Server"></param>
        /// <param name="UserName"></param>
        /// <param name="Password"></param>
        /// <param name="DataBase"></param>
        ///<param name="ConnectionTimeout"></param> 
        private bool AddServerDetails(string Server, string UserName, string Password, string DataBase, string ConnectionTimeout )
        {
            bool bResult = false;
            Dictionary<string, string> objServerDetails = new Dictionary<string, string>();
            try
            {
                objServerDetails.Add("SERVER", Server);
                objServerDetails.Add("UID", UserName);
                objServerDetails.Add("PWD", Password);
                objServerDetails.Add("DATABASE", DataBase);
                objServerDetails.Add("CONNECTION TIMEOUT", ConnectionTimeout);

                ReturnConnectionString = Credentials.MakeConnectionString(objServerDetails);
                if (!String.IsNullOrEmpty(ReturnConnectionString))
                {
                    bResult = Credentials.TestConnectionDB(ReturnConnectionString);
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("AddServerDetails" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
                MessageBox.ShowBox("MessageID33", BMC_Icon.Error);
            }
            return bResult;
        }
        
        /// <summary>
        /// Display the exchange server details.
        /// <param name="ServerEntries"></param>
        /// <returns></returns>
        /// </summary>       
        private void GetExchangeServerSettings(Dictionary<string, string> ServerEntries)
        {
            try
            {
                if (ServerEntries != null)
                {
                    foreach (KeyValuePair<string, string> objKeyValue in ServerEntries)
                    {
                        if (objKeyValue.Key.ToUpper() == "SERVER")
                        {
                            txtExchangeServer.Text = objKeyValue.Value;
                        }
                        else if (objKeyValue.Key.ToUpper() == "UID")
                        {
                            txtExchangeUsername.Text = objKeyValue.Value;
                        }
                        else if (objKeyValue.Key.ToUpper() == "PASSWORD")
                        {                            
                            txtExchangePassword.Password = objKeyValue.Value;                            
                        }
                        else if (objKeyValue.Key.ToUpper() == "DATABASE")
                        {
                            tbExchangeDBNameValue.Text = objKeyValue.Value;
                        }
                        else if (objKeyValue.Key.ToUpper() == "TIMEOUT")
                        {
                            txtExchangeTimeout.Text = objKeyValue.Value;
                        }
                        else if (objKeyValue.Key.ToUpper() == "INSTANCE")
                        {
                            txtExchangeInstance.Text = objKeyValue.Value;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("GetExchangeServerSettings" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
                MessageBox.ShowBox("MessageID33", BMC_Icon.Error);
            }
        }

        /// <summary>
        /// Enable or Disable control based on the registry entry for the first time   
        /// <param name=""></param>
        /// <returns></returns>
        /// </summary>
        private void RefreshControls()
        {
            if ((txtExchangeServer.Text == string.Empty) || (txtExchangeUsername.Text == string.Empty) || (txtExchangePassword.Password == string.Empty))
            {
                txtExchangeServer.Text = string.Empty;
                txtExchangeUsername.Text = string.Empty;
                ValidatePasswordBox(txtExchangePassword);
                txtExchangeTimeout.Text = "30";
               
                tiServicesSetup.IsEnabled = false;
                tiSummary.IsEnabled = false;
                txtExchangeServer.Focus();
            }
            else
            {   
                tiServicesSetup.IsEnabled = true;
                tiSummary.IsEnabled = true;
                txtExchangeServer.Focus();
            }
            txtExchangeCommandTimeout.Text = BMCRegistryHelper.GetRegKeyValue(string.Empty, UIConstants.SQLCommandTimeOut, "60");
        }

        /// <summary>
        /// Get Log path details
        /// </summary>
        private void GetLogPath()
        {
            string strKeyvalue = string.Empty;
            try
            {
                strKeyvalue = BMCRegistryHelper.GetRegKeyValue(string.Empty, UIConstants.DefaultLogDir, "C:\\Logs");
                if (!string.IsNullOrWhiteSpace(strKeyvalue))
                {
                    txtFileChoose.Text = strKeyvalue;
                }
                else
                {
                    txtFileChoose.Text = "C:\\Logs";
                }
            }
            catch (Exception ex)
            {
                txtFileChoose.Text = "C:\\Logs";
                ExceptionManager.Publish(ex);
                MessageBox.ShowBox("MessageID45", BMC_Icon.Error);//Error while loading logs file path.
            }
        }

        /// <summary>
        /// Check Web service hitting  
        /// <param name="strURL">string</param>
        /// <returns>integer</returns>
        /// </summary>
        private int CheckWebService(string strURL)
        {
            int iReceiveValue = -1;

            try
            {
                if (ValidateURL(strURL))
                {
                    iReceiveValue = ReadServicesSettings.TestWebUrl(strUrlvalidate);

                    if (iReceiveValue >= 0)
                    {
                        MessageBox.ShowBox("MessageID4", BMC_Icon.Information);
                        btnSaveSettings.IsEnabled = true;
                    }
                    else
                        MessageBox.ShowBox("MessageID5", BMC_Icon.Error);

                }
                else
                {
                    iReceiveValue = -1;
                    MessageBox.ShowBox("MessageID5", BMC_Icon.Error);
                }
            }
            catch (System.Security.Authentication.AuthenticationException)
            {
                throw new System.Security.Authentication.AuthenticationException("The remote certificate is invalid according to the validation procedure");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReceiveValue;
        }

        private bool ValidateURL(string sUrl)
        {
            bool bReturn = false;
            string sWebExtension = string.Empty;
            Regex objRegexUrlvalidate = new Regex("^(http|ftp|https)://(www\\.)?.+\\.(com|net|org|asmx)$");
            MatchCollection objMatchCollect;
            string enterprisePort = ConfigurationManager.AppSettings.Get("EnterpriseWebServicePort");
            bool entepriseWebServicePortRequired = Convert.ToBoolean(ConfigurationManager.AppSettings.Get("EntepriseWebServicePortRequired"));

            if (sUrl.Trim().Length < 0 || sUrl.Trim().Length == 0)
            {
                MessageBox.ShowBox("MessageID2", BMC_Icon.Information);                                
            }
            else
            {
                if (!sUrl.Contains(".asmx"))
                {
                    sWebExtension = ConfigManager.Read("WebserviceExtension");
                    LoadConfig();
                    if (!sUrl.Contains(sProtocol))
                    {
                        if (sProtocol.Substring(0, sProtocol.Length - 3) == "https")
                        {
                            if (entepriseWebServicePortRequired)
                            {
                                strUrlvalidate = sProtocol.Trim() + sUrl.Trim() + ":" + enterprisePort + sWebExtension.Trim();
                            }
                            else
                            {
                                strUrlvalidate = sProtocol.Trim() + sUrl.Trim() + sWebExtension.Trim();
                            }
                        }
                        else
                        {
                            strUrlvalidate = sProtocol.Trim() + sUrl.Trim() + sWebExtension.Trim();
                        }
                    }
                    else
                    {
                        strUrlvalidate = sUrl.Trim() + sWebExtension.Trim();
                    }
                    txtEnterpriseweburl.Text = strUrlvalidate;
                    bReturn = true;
                }
                else
                {
                    LoadConfig();
                    if (!sUrl.Contains(sProtocol))
                    {
                        MessageBox.ShowBox("MessageID3", BMC_Icon.Error);                            
                        txtEnterpriseweburl.Focus();
                        bReturn = false;
                    }
                    else
                    {
                        strUrlvalidate = sUrl.Trim();
                        objMatchCollect = objRegexUrlvalidate.Matches(strUrlvalidate);
                        if (objMatchCollect.Count > 0)
                            bReturn = true;
                        else
                        {
                            MessageBox.ShowBox("MessageID3", BMC_Icon.Error);                                                                                    
                            bReturn = false;
                        }
                    }
                }


            }
            return bReturn;
        }

        /// <summary>
        /// Get Exchange Information
        /// </summary>
        private void GetSettingsInfo()
        {
            bool bTestExchangeConnection = false;
            try
            {
                LogManager.WriteLog("Getting Exchange Connection String", LogManager.enumLogLevel.Debug);

                if (ValidateText(txtExchangeServer, "Server"))
                {
                    if (ValidateText(txtExchangeUsername, "UserName"))
                    {   
                        if(ValidatePasswordBox(txtExchangePassword))
                        {
                            if (ValidateText(txtExchangeTimeout, "Connection Timeout"))
                            {
                                bTestExchangeConnection = TestConnection(txtExchangeServer.Text, txtExchangeUsername.Text, txtExchangePassword.Password, txtExchangeTimeout.Text,txtExchangeInstance.Text, 'E');
                            }
                        }
                    }
                }
                if (bTestExchangeConnection == true)
                {
                    if (!string.IsNullOrEmpty(ReturnConnectionString))
                        DataBaseServiceHandler.ConnectionString = ReturnConnectionString;
                }

                LogManager.WriteLog("Exchange Connection String" + DataBaseServiceHandler.ConnectionString.Length.ToString(), LogManager.enumLogLevel.Debug);
                SettingsTable = new DataTable();
                DataBaseServiceHandler.Fill(QueryType.Text, "Select [ID] = Setting_ID , [Name] = Setting_Name, [Value] = Isnull(Setting_Value,'') From Setting", SettingsTable);
                PropertyBag pbSetting = new PropertyBag();

                if (SettingsTable != null && SettingsTable.Rows.Count > 0)
                {
                    LogManager.WriteLog("Setting Table Rows Count   " + SettingsTable.Rows.Count.ToString(), LogManager.enumLogLevel.Debug);
                }
                else
                {
                    LogManager.WriteLog("Could not populate Settings Table", LogManager.enumLogLevel.Debug);
                }

                pbSetting.GetValue += new PropertySpecEventHandler(pbSetting_GetValue);
                pbSetting.SetValue += new PropertySpecEventHandler(pbSetting_SetValue);

                foreach (DataRow dr in SettingsTable.Rows)
                {
                    if (BMC.Common.Utilities.Common.GetRowValue<string>(dr, "Value").ToUpper() == "TRUE" || BMC.Common.Utilities.Common.GetRowValue<string>(dr, "Value").ToUpper() == "FALSE")
                        if (BMC.Common.Utilities.Common.GetRowValue<string>(dr, "Value").ToUpper() == "TRUE")
                            pbSetting.Properties.Add(new PropertySpec(BMC.Common.Utilities.Common.GetRowValue<string>(dr, "Name"), typeof(bool), "BMC Category", null, true));
                        else
                            pbSetting.Properties.Add(new PropertySpec(BMC.Common.Utilities.Common.GetRowValue<string>(dr, "Name"), typeof(bool), "BMC Category", null, false));
                    else
                        pbSetting.Properties.Add(new PropertySpec(BMC.Common.Utilities.Common.GetRowValue<string>(dr, "Name"), typeof(string), "BMC Category", null, BMC.Common.Utilities.Common.GetRowValue<string>(dr, "Value")));

                }
                if (pbSetting != null && pbSetting.Properties.Count > 0)
                {
                    LogManager.WriteLog("PBSetting Properties Count    " + pbSetting.Properties.Count.ToString(), LogManager.enumLogLevel.Debug);
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("GetSettingsInfo" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
                MessageBox.ShowBox("MessageID33", BMC_Icon.Error);
            }
        }

        /// <summary>
        /// To Get Initial Settings
        /// </summary>
        private void GetInitialSettings()
        {
            try
            {
                Dictionary<string, string> objKeycCollections = ReadServicesSettings.GetKeys("appSettings");

                if (objKeycCollections != null)
                {
                    foreach (KeyValuePair<string, string> objKeys in objKeycCollections)

                        if (objKeys.Key.ToString() == "RegistryPath")
                        {
                            strRegistryKeyPath = objKeys.Value.ToString();
                            if (!string.IsNullOrEmpty(strRegistryKeyPath))
                            {
                                ExchangeConfigRegistryEntities.RegistryKeyValue = strRegistryKeyPath;
                            }

                        }
                        else if (objKeys.Key.ToString() == "NetLoggerPath")
                        {
                            strRegistryKeyPath = objKeys.Value.ToString();
                            if (!string.IsNullOrEmpty(strRegistryKeyPath))
                            {
                                ExchangeConfigRegistryEntities.NetLoggerRegKeyValue = strRegistryKeyPath;
                            }
                        }
                        else if (objKeys.Key.ToString() == "HoneyFramePath")
                        {
                            strRegistryKeyPath = objKeys.Value.ToString();
                            if (!string.IsNullOrEmpty(strRegistryKeyPath))
                            {
                                ExchangeConfigRegistryEntities.HoneyFrameKeyValue = strRegistryKeyPath;
                            }
                        }
                        else if (objKeys.Key.ToString() == "HoneyFramePath")
                        {
                            strRegistryKeyPath = objKeys.Value.ToString();
                            if (!string.IsNullOrEmpty(strRegistryKeyPath))
                            {
                                ExchangeConfigRegistryEntities.HoneyFrameKeyValue = strRegistryKeyPath;
                            }
                        }
                        //else if (objKeys.Key.ToString().ToUpper() == "PROTOCOL")
                        //{
                        //    if (objKeys.Value.ToString().ToUpper() == "HTTPS://")
                        //        chkTrustedSite.IsChecked = true;
                        //    else
                        //        chkTrustedSite.IsChecked = false;
                        //}
                }
                else
                {   
                    MessageBox.ShowBox("MessageID1", BMC_Icon.Error);
                }
            }
            catch (ConfigurationException confiex)
            {
                LogManager.WriteLog("GetInitialSettings" + confiex.Message.ToString() + confiex.Source.ToString(), LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(confiex);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("GetInitialSettings" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
            }
        }

        private void GetSettings()
        {
            string strKeyvalue = string.Empty;
            string strWebUrl = string.Empty;

            try
            {

                strConnection = RegistrySettings.ExchangeConnectionString();

                if (!String.IsNullOrEmpty(strConnection))
                {
                    Dictionary<string, string> ServerEntries = Credentials.RetrieveServerDetails(strConnection);

                    GetExchangeServerSettings(ServerEntries);

                   // Dictionary<string, string> ExchangeRegistryEntries = RegistrySettings.GetRegistryEntries(ExchangeConfigRegistryEntities.RegistryKeyValue);
                    //foreach (KeyValuePair<string, string> KVPServer in ExchangeRegistryEntries)
                    //{
                    strKeyvalue = string.Empty;
                    try
                    {
                        strKeyvalue = BMCRegistryHelper.GetRegKeyValue(ExchangeConfigRegistryEntities.RegistryKeyValue, "bgswebservice");
                    }
                    catch (Exception ex)
                    {
                        ExceptionManager.Publish(ex);
                    }
                    if (!string.IsNullOrEmpty(strKeyvalue))
                    {
                        strWebUrl = strKeyvalue;
                        txtEnterpriseweburl.Text = strWebUrl;

                        if (strWebUrl.ToUpper().Contains("HTTPS"))
                        {
                            chkTrustedSite.IsChecked = true;
                        }
                        else
                        {
                            chkTrustedSite.IsChecked = false;
                        }
                    }
                    else
                    {
                        txtEnterpriseweburl.Text = string.Empty;
                    }


                        //strKeyvalue = KVPServer.Key.Substring(KVPServer.Key.LastIndexOf('\\') + 1);
                        //if (strKeyvalue == "BGSWebService")
                        //{
                        //    if (!string.IsNullOrEmpty(KVPServer.Value.ToString()))
                        //    {
                        //        strWebUrl = KVPServer.Value.ToString();
                        //        txtEnterpriseweburl.Text = strWebUrl;

                        //        if (strWebUrl.ToUpper().Contains("HTTPS"))
                        //        {
                        //            chkTrustedSite.IsChecked = true;
                        //        }
                        //        else
                        //        {
                        //            chkTrustedSite.IsChecked = false;
                        //        }
                        //    }
                        //}     

                    strKeyvalue = string.Empty;
                    try
                    {
                        strKeyvalue = BMCRegistryHelper.GetRegKeyValue(ExchangeConfigRegistryEntities.RegistryKeyValue + @"\Exchange", "bindipaddress");
                    }
                    catch (Exception ex)
                    {
                        ExceptionManager.Publish(ex);
                    }
                    if (!string.IsNullOrEmpty(strKeyvalue))
                    {
                        txtBindIPAddress.Text = strKeyvalue;
                    }
                        //if (strKeyvalue.ToLower() == "bindipaddress")
                        //{
                        //    if (!string.IsNullOrEmpty(KVPServer.Value.ToString()))
                        //    {
                        //        txtBindIPAddress.Text = KVPServer.Value.ToString();
                        //    }
                        //}
                    strKeyvalue = string.Empty;
                    try
                    {
                        strKeyvalue = BMCRegistryHelper.GetRegKeyValue(ExchangeConfigRegistryEntities.RegistryKeyValue + @"\Exchange", "default_server_ip");
                    }
                    catch (Exception ex)
                    {
                        ExceptionManager.Publish(ex);
                    }

                    if (!string.IsNullOrEmpty(strKeyvalue))
                    {
                         txtDefaultServerIP.Text = strKeyvalue;
                    }                            
                     
                        #region +S001 START
                        //Reads Cash dispenser information from registry. Decrypts the values and display in coresponding txtbox

                    strKeyvalue = string.Empty;

                    try
                    {
                        strKeyvalue = BMCRegistryHelper.GetRegKeyValue(ExchangeConfigRegistryEntities.RegistryKeyValue, "cdserverinfo");
                    }
                    catch (Exception ex)
                    {
                        ExceptionManager.Publish(ex);
                    }
                    if (!string.IsNullOrEmpty(strKeyvalue))
                    {
                        string strDecryptCDSetting = RegistryBuilder.DecryptCDSetting(strKeyvalue);
                        if (!String.IsNullOrEmpty(strDecryptCDSetting.Trim()))
                        {
                            string[] strCDSetting = strDecryptCDSetting.Split(';');
                            txtCDServer.Text = strCDSetting[0];
                            txtCDServerPort.Text = strCDSetting[1];
                            txtCDUsername.Text = strCDSetting[2];
                            txtCDDevicename.Text = strCDSetting[3];
                            chkUseSSL.IsChecked = Convert.ToBoolean(strCDSetting[4]);
                        }
                    }

                    strKeyvalue = string.Empty;
                    try
                    {
                        strKeyvalue = BMCRegistryHelper.GetRegKeyValue(ExchangeConfigRegistryEntities.RegistryKeyValue, "cdserverpwd");
                    }
                    catch (Exception ex)
                    {
                        ExceptionManager.Publish(ex);
                    }
                    if (!string.IsNullOrEmpty(strKeyvalue))
                    {
                        txtCDPassword.Password = RegistryBuilder.DecryptCDSetting(strKeyvalue);
                    }
                    #endregion +S001 END
                    //}
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("GetSettings" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// Validations done for the IP address
        /// </summary>
        /// <param name="strCheckIP"></param>        
        public bool ValidateIP(string strCheckIP)
        {
            string[] strIParray;
            bool bReturn = false;
            int iCheckValue = 0;

            try
            {
                if (!string.IsNullOrEmpty(strCheckIP))
                {
                    if (strCheckIP.IndexOf(".", 0) > 0)
                    {
                        strIParray = strCheckIP.ToString().Split('.');

                        if (strIParray.LongLength == 4)
                        {
                            for (int i = 0; i <= strIParray.Length - 1; i++)
                            {
                                if (strIParray[i] != null && strIParray[i] != string.Empty)
                                {
                                    iCheckValue = int.Parse(strIParray[i].ToString());
                                    if (iCheckValue > 0)
                                    {
                                        if (iCheckValue <= 255 && iCheckValue >= 0)
                                        {
                                            bReturn = true;
                                        }
                                        else
                                        {
                                            bReturn = false;
                                            break;

                                        }
                                    }
                                }
                                else
                                {
                                    bReturn = false;
                                }
                            }
                        }
                    }
                    else
                    {
                        bReturn = false;
                    }
                }
                else
                {
                    bReturn = false;
                }
            }
            catch (IndexOutOfRangeException iex)
            {
                LogManager.WriteLog("ValidateIP" + iex.Message.ToString() + iex.Source.ToString(), LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(iex);
                MessageBox.ShowBox("MessageID33", BMC_Icon.Error);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("ValidateIP" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
                MessageBox.ShowBox("MessageID33", BMC_Icon.Error);
            }
            return bReturn;
        }

        void LoadConfig()
        {
            if (chkTrustedSite.IsChecked == true)
            {
                sProtocol = "https://";
            }
            else
            {
                sProtocol = "http://";
            }    
        }

        #region +S001 START
        private void RefreshCDControls()
        {
            if (txtCDServer.Text == string.Empty) { txtCDServer.Text = string.Empty; }
            if (txtCDServerPort.Text == string.Empty) { txtCDServerPort.Text = string.Empty; }
            if (txtCDUsername.Text == string.Empty) { txtCDUsername.Text = string.Empty; }
            if (!ValidatePasswordBox(txtCDPassword)) { txtCDPassword.Password = string.Empty; }
            if (txtCDDevicename.Text == string.Empty) { txtCDDevicename.Text = string.Empty; }
        }
        #endregion +S001 END

        private bool ValidatePasswordBox(PasswordBox pBox)
        {
            if (pBox.Password.Trim() == string.Empty)
            {
                pBox.ToolTip = "Please enter Password";

                switch (pBox.Name)
                {
                    case "txtExchangePassword":
                        tbExchangePasswordRequired.Visibility = Visibility.Visible;
                        break;

                    #region +S001 START
                    case "txtCDPassword":
                        tbCDPasswordRequired.Visibility = Visibility.Visible;
                        break;
                    #endregion +S001 END
                }

                return false;

            }
            return true;
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

        private string DisplayNumberPad(TextBox Control)
        {
            string strNumberPadText = string.Empty;
            NumberPadWind ObjNumberpadWind = new NumberPadWind();

            string keytext = Control.Text;
            try
            {
                ObjNumberpadWind.ucTicketEntry.MaxLength = Control.MaxLength;
                ObjNumberpadWind.ValueText = keytext;

                if (ObjNumberpadWind.ShowDialog() == true)
                {
                    if (ObjNumberpadWind.ValueText == "")
                        strNumberPadText = "0";
                    else
                        strNumberPadText = ObjNumberpadWind.ValueText;
                }
            }
            catch (Exception ex)
            {
                strNumberPadText = ObjNumberpadWind.ValueText;
                ObjNumberpadWind.Close();
                ExceptionManager.Publish(ex);
            }
            return strNumberPadText;
        }

        private void SomeNumericKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            try
            {
                if (e.KeyboardDevice.Modifiers == ModifierKeys.Shift || (!CustomerDetailsConstants.AllowedNumerics.Contains(e.Key)))
                {
                    //int keyNumber = (int)e.Key;

                    //if (keyNumber > 69 || keyNumber < 44)
                    //{
                    //    if (!CustomerDetailsConstants.AllowedNumerics.Contains(e.Key)) { e.Handled = true; }
                    e.Handled = true;
                    //}
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
       
        #endregion Private Methods

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            if ((txtExchangeServer.Text == string.Empty))
            {
                txtExchangeServer.Text = string.Empty;                
            }
            if (txtExchangeUsername.Text == string.Empty)
            {
                txtExchangeUsername.Text = string.Empty;
            }
            if (txtExchangePassword.Password == string.Empty)
            {
                ValidatePasswordBox(txtExchangePassword);
            }
            if (txtExchangeTimeout.Text == string.Empty)
            {
                txtExchangeTimeout.Text = "30";
            }
            if (txtExchangeCommandTimeout.Text == string.Empty)
            {
                txtExchangeCommandTimeout.Text = BMCRegistryHelper.GetRegKeyValue(string.Empty, UIConstants.SQLCommandTimeOut, "60");
            }
        }

        private void btnExchangeSaveConnection_Click(object sender, RoutedEventArgs e)
        {
            bool bTestExchangeConnection = false;
            Dictionary<string, string> dictSetregistryentries;
            string strEncryptExchangeConnection = string.Empty;
            string strEncryptExchangeConnectionHex = string.Empty;
                       try
            {
                dictSetregistryentries = new Dictionary<string, string>();
                Cursor = System.Windows.Input.Cursors.Wait;

                //Exchange Server save 
                if (ValidateExchangeServer())
                {
                    bTestExchangeConnection = TestConnection(txtExchangeServer.Text, txtExchangeUsername.Text, txtExchangePassword.Password, txtExchangeTimeout.Text, txtExchangeInstance.Text, 'E');

                    if (bTestExchangeConnection == true)
                    {
                        if (!string.IsNullOrEmpty(ReturnConnectionString))
                            ExchangeConfigRegistryEntities.ExchangeConnectionString = ReturnConnectionString;
                        strEncryptExchangeConnection = RegistrySettings.EncryptExchangeConnection();
                        if (!string.IsNullOrEmpty(strEncryptExchangeConnection))
                        {
                            dictSetregistryentries.Add(UIConstants.strSQLConnect, strEncryptExchangeConnection + "+" + "REG_SZ");
                            strEncryptExchangeConnectionHex = RegistrySettings.EncryptExchangeConnectionHex();
                            if (!string.IsNullOrEmpty(strEncryptExchangeConnectionHex))
                            {
                                dictSetregistryentries.Add(UIConstants.strSQLConnectEx, strEncryptExchangeConnectionHex + "+" + "REG_SZ");
                            }

                            //Command Time Out
                            BMCRegistryHelper.SetRegKeyValue(string.Empty, UIConstants.SQLCommandTimeOut, RegistryValueKind.String, txtExchangeCommandTimeout.Text);

                            //Save all Registry Settings under cash master
                            RegistrySettings.SetRegistryEntries(dictSetregistryentries, "Software\\Honeyframe\\" + ExchangeConfigRegistryEntities.RegistryKeyValue);
                            System.Windows.Forms.Application.DoEvents();

                            MessageBox.ShowBox("MessageID12", BMC_Icon.Information);
                            GetSettings();
                            tiServicesSetup.IsEnabled = true;
                            tiServicesSetup.Focus();

                            #region +S001 START
                            //Flag to chk whether cash dispenser to be enabled
                            strIsCDEnabled = DBSettings.GetSettingValue(strConnection, "CashDispenserEnabled");
                            strCDType = DBSettings.GetSettingValue(strConnection, "CashDispenserType");

                            if (strIsCDEnabled.ToUpper() == "TRUE" && strCDType.ToUpper() == ConfigurationManager.AppSettings.Get("CashDispenserType"))
                            {
                                tiCash_Dispenser.Visibility = Visibility.Visible;
                                tiCash_Dispenser.Focus();
                            }
                            else
                            {
                                if (tiCash_Dispenser.Visibility == Visibility.Visible)
                                    tiCash_Dispenser.Visibility = Visibility.Collapsed;
                            }
                            #endregion +S001 END
                        }
                    }
                    else
                    {
                        MessageBox.ShowBox("MessageID13", BMC_Icon.Error);
                    }
                }

                System.Windows.Forms.Application.DoEvents();
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("btnSaveExchangeConnection_Click" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
                MessageBox.ShowBox("MessageID33", BMC_Icon.Error);
            }
            finally
            {
                Cursor = System.Windows.Input.Cursors.Arrow;
            }
        }

        private void tiServicesSetup_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtEnterpriseweburl.Text == string.Empty)
            {
                txtEnterpriseweburl.Text = string.Empty;
            }
            txtEnterpriseweburl.Focus();
        }

        private void txtDefaultServerIP_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!Settings.OnScreenKeyboard)
                return;
            txtDefaultServerIP.Text = DisplayKeyboard(txtDefaultServerIP.Text, string.Empty);
            txtDefaultServerIP.SelectionStart = txtDefaultServerIP.Text.Length;
        }

        private void txtBindIPAddress_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!Settings.OnScreenKeyboard)
                return;
            txtBindIPAddress.Text = DisplayKeyboard(txtBindIPAddress.Text, string.Empty);
            txtBindIPAddress.SelectionStart = txtBindIPAddress.Text.Length;
        }
        private void txtExchangeCommandTimeout_PreviewExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Command == ApplicationCommands.Copy || e.Command == ApplicationCommands.Cut || e.Command == ApplicationCommands.Paste)
            {
                e.Handled = true;
            }
        }

        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            string FolderPath = null;
            System.Windows.Forms.FolderBrowserDialog FdbrowserLogPath = new System.Windows.Forms.FolderBrowserDialog();

            if (FdbrowserLogPath.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                FolderPath = FdbrowserLogPath.SelectedPath;
                txtFileChoose.Text = FolderPath;
            }
        }

        public bool ValidateExchangeServer()
        {
            if (ValidateText(txtExchangeServer, "Server"))
            {
                if (ValidateText(txtExchangeUsername, "txtExchangeUsername"))
                {
                    if (ValidatePasswordBox(txtExchangePassword))
                    {
                        if (ValidateText(txtExchangeTimeout, "Connection Timeout"))
                        {
                            if (ValidateText(txtExchangeCommandTimeout, "txtExchangeCommandTimeout"))
                            {
                                return true;
                            }
                            else
                            {
                                MessageBox.ShowBox("MessageID48", BMC_Icon.Information);//Exchange command timeout cannot be empty.
                                txtExchangeCommandTimeout.Focus();
                                return false;
                            }
                        }
                        else
                        {
                            MessageBox.ShowBox("MessageID49", BMC_Icon.Information);//Exchange timeout cannot be empty.
                            txtExchangeTimeout.Focus();
                            return false;
                        }
                    }
                    else
                    {
                        MessageBox.ShowBox("MessageID50", BMC_Icon.Information);//Database password cannot be empty.
                        txtExchangePassword.Focus();
                        return false;
                    }
                }
                else
                {
                    MessageBox.ShowBox("MessageID51", BMC_Icon.Information);//Database username cannot be empty.
                    txtExchangeUsername.Focus();
                    return false;
                }
            }
            else
            {
                MessageBox.ShowBox("MessageID52", BMC_Icon.Information);//Database server name cannot be empty.
                txtExchangeServer.Focus();
                return false;
            }
        }

    }
    
    #endregion

    #region PropertyClass

    public class PropertyClass
    {
        #region Public Declarations

        public string ID;
        public string Name;
        public string Value;

        #endregion Public Declarations
    }

    #endregion

    #region ExchangeConfigurationEntity Class

    public class ExchangeConfigurationEntity : ValidationRule, INotifyPropertyChanged
    {
        #region Private Declarations

        private int _minimumLength = -1;
        private int _maximumLength = -1;
        private string _errorMessage;

        private string _exchangeServer = string.Empty;
        private string _exchangeInstance = string.Empty;
        private string _exchangeServerUserName = string.Empty;
        private string _exchangeServerPassword = string.Empty;
        private string _exchangeTimeOut = string.Empty;
      //  private string _exchangeCommandTimeOut = string.Empty;

        private string _enterpriseServerURL = string.Empty;

        #region +S001 START
        private string _cdServer = string.Empty;
        private string _cdServerPort = string.Empty;
        private string _cdUsername = string.Empty;
        private string _cdDevicename = string.Empty;
        #endregion +S001 END

        #endregion Private Declarations

        #region Public Properties

        public int MinimumLength
        {
            get { return _minimumLength; }
            set { _minimumLength = value; }
        }

        public int MaximumLength
        {
            get { return _maximumLength; }
            set { _maximumLength = value; }
        }

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { _errorMessage = value; }
        }

        public string ExchangeServer
        {
            get { return _exchangeServer; }
            set { _exchangeServer = value; OnPropertyChanged("ExchangeServer"); }
        }

        public string ExchangeInstance
        {
            get { return _exchangeInstance; }
            set { _exchangeInstance = value; }
        }

        public string ExchangeServerUserName
        {
            get { return _exchangeServerUserName; }
            set { _exchangeServerUserName = value; OnPropertyChanged("ExchangeServerUserName"); }
        }

        public string ExchangeServerPassword
        {
            get { return _exchangeServerPassword; }
            set { _exchangeServerPassword = value; OnPropertyChanged("ExchangeServerPassword"); }
        }

        public string ExchangeTimeOut
        {
            get { return _exchangeTimeOut; }
            set { _exchangeTimeOut = value; OnPropertyChanged("ExchangeTimeOut"); }
        }

        public string EnterpriseServerURL
        {
            get { return _enterpriseServerURL; }
            set { _enterpriseServerURL = value; OnPropertyChanged("EnterpriseServerURL"); }
        }

        #region +S001 START
        //Validate the txtbox present in cash dispenser tab
        public string CDServer
        {
            get { return _cdServer; }
            set { _cdServer = value; OnPropertyChanged("CDServer"); }
        }
        public string CDServerPort
        {
            get { return _cdServerPort; }
            set { _cdServerPort = value; OnPropertyChanged("CDServerPort"); }
        }
        public string CDUsername
        {
            get { return _cdUsername; }
            set { _cdUsername = value; OnPropertyChanged("CDUsername"); }
        }
        public string CDDevicename
        {
            get { return _cdDevicename; }
            set { _cdDevicename = value; OnPropertyChanged("CDDevicename"); }
        }
        #endregion +S001 END

        #endregion Public Properties

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        #region Public Method

        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            ValidationResult result = new ValidationResult(true, null);
            try
            {
                string inputString = (value.ToString().Trim() ?? string.Empty).ToString();
                if (inputString.Length < this.MinimumLength | inputString.Length > this.MaximumLength)
                {
                    result = new ValidationResult(false, this.ErrorMessage);
                    return result;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                MessageBox.ShowBox("MessageID33", BMC_Icon.Error);
            }
            return result;
        }

        #endregion Public Method
    }

    #endregion

    public class CustomerDetailsConstants
    {
        public static List<Key> AllowedAlphabets = new List<Key>         
            {    
                Key.A,
                Key.B,
                Key.C,
                Key.D,
                Key.E,
                Key.F,
                Key.G,
                Key.H,
                Key.I,
                Key.J,
                Key.K,
                Key.L,
                Key.M,
                Key.N,
                Key.O,
                Key.P,
                Key.Q,
                Key.R,
                Key.S,
                Key.T,
                Key.U,
                Key.V,
                Key.W,
                Key.X,
                Key.Y,
                Key.Z,         
                

                Key.Enter,
                Key.Back,
                Key.Delete,
                Key.LeftAlt,
                Key.RightAlt,
                Key.Left,
                Key.Right,
                Key.LeftShift,
                Key.RightShift,                
                Key.Home,
                Key.End,
                Key.Tab,
                Key.Space

        };
        public static List<Key> AllowedNumerics = new List<Key>         
            {    
                Key.D0, 
                Key.D1,
                Key.D2,
                Key.D3,
                Key.D4,
                Key.D5,
                Key.D6,
                Key.D7,
                Key.D8,
                Key.D9,

                Key.NumPad0,
                Key.NumPad1,
                Key.NumPad2,
                Key.NumPad3,
                Key.NumPad4,
                Key.NumPad5,
                Key.NumPad6,
                Key.NumPad7,
                Key.NumPad8,
                Key.NumPad9,               
                
                Key.Enter,
                Key.Back,
                Key.Delete,
                //Key.LeftAlt,
                //Key.RightAlt,
                Key.Left,
                Key.Right,
                //Key.LeftShift,
                //Key.RightShift,                
                Key.Home,
                Key.End,
                Key.Tab


        };

        public static List<Key> AllowedSpecialCharacters = new List<Key> 
        {
            Key.OemQuestion,
            Key.OemComma
        };
    }
}