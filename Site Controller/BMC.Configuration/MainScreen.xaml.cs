/*******************************************************************************************************
 *  Revision History
 *  Name            TrackCode   Modified Date   Change Description
 *  Selva Kumar S   S001        27th Jul 2012   Save/Load the Cash dispenser server info to and from 
 *                                              windows registry based on cash dispenser DB setting
 *  Selva Kumar S   S002        27th Jul 2012   Save/Load STM EventTransmitter DB settings
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
    using System.Data.SqlClient;
    using System.Windows.Data;
    using System.Security.Cryptography;
    using System.Runtime.InteropServices;
    using Microsoft.SqlServer.Management.Smo;
    using Microsoft.SqlServer.Management.Common;
    using System.Linq;
    using BMC.Common.Utilities;
    using System.Threading;
    using BMC.Security;
    using System.Windows.Threading;
    using BMC.Common.Persistence;

    #endregion

    #region MainScreenClass

    public partial class MainScreen : INotifyPropertyChanged
    {
        #region Declarations

        private string s_UserName = "John Smith";
        static bool b_Logout = false;
        public string Skin1 = "Resources/BMCBlueTheme.xaml";
        public string Skin2 = "Resources/BMCGreenTheme.xaml";
        string strConnection = string.Empty;
        string PCConnection = string.Empty;
        private string ReturnConnectionString = string.Empty;
        List<PropertyClass> ChangedProperty = new List<PropertyClass>();
        DataTable SettingsTable = null;
        private string strRegistryKeyPath = string.Empty;
        private string strScriptPath = string.Empty;
        private string strUpgradeVisible = string.Empty;
        private string[] strListarray = null;
        private string strServiceStatus = string.Empty;
        private PropertyBag PropertyHolder = new PropertyBag();
        private string strUrlvalidate = string.Empty;
        private ObservableCollection<LoadListView> _LoadListViewCollection = new ObservableCollection<LoadListView>();
        private string _exchangeServerName = string.Empty;
        private string sProtocol = string.Empty;
        private ExchangeConfigurationEntity exchangeConfigEntity = new ExchangeConfigurationEntity();
        private GridViewConfiurationEntity gridConfigEntity = new GridViewConfiurationEntity();
        private string classText = string.Empty;
        private string _sKeyText = string.Empty;
        public static bool isCertificateRequired = false;
        public static bool isDHCPEnabled = false;
        private string strClient = string.Empty;
        private string strIsCDEnabled = string.Empty;   //+S001
        private string strCDType = string.Empty;        //+S001
        private string _RangeColor = "#FFFFFF";
        private DataTable gridViewColorRangeDetails = null;
        public bool isValidError = false;
        private List<Key> AllowedInputs = new List<Key>{Key.D0, Key.D1, Key.D2, Key.D3, Key.D4, Key.D5, Key.D6, Key.D7, Key.D8, Key.D9,
                                                        Key.NumPad0, Key.NumPad1, Key.NumPad2, Key.NumPad3, Key.NumPad4, Key.NumPad5, Key.NumPad6, Key.NumPad7, Key.NumPad8, Key.NumPad9,
                                                        Key.OemPeriod, Key.Decimal, Key.Tab};
        private Int32 serviceTimeOut = 5;
        private string sIsTISEnabled = string.Empty; //For TIS
        private string sTISCommunicationModeSetting = string.Empty; //For TIS
        private string strDataFilePath = string.Empty;
        private string strLogFilePath = string.Empty;
        private bool bIsSelectDBCancelled = false;
        #endregion

        #region Constructor

        public MainScreen()
        {
            this.InitializeComponent();
            this.cmbLanIP.Loaded += delegate
            {
                TextBox editableTextBox = cmbLanIP.Template.FindName("PART_EditableTextBox", cmbLanIP) as TextBox;
                if (editableTextBox != null)
                {
                    editableTextBox.PreviewMouseUp -= new MouseButtonEventHandler(editTextBox_PreviewMouseUp);
                    editableTextBox.PreviewMouseUp += new MouseButtonEventHandler(editTextBox_PreviewMouseUp);
                }
            };

            IntialiseDate();
            b_Logout = false;

            if (!Settings.IsLoginRequired)
            {
                lblUsername.Visibility = Visibility.Hidden;
                btnLogout.Visibility = Visibility.Hidden;
            }
            NOGateway.IsChecked = true;
            MessageBox.parentOwner = this;
        }

        #endregion Constructor

        #region Properties

        public string UserName
        {
            get { return s_UserName; }
            set { s_UserName = value; }
        }

        public string DBUserName
        {
            get;
            private set;
        }
        public string DBPassword
        {
            get;
            private set;
        }
        public string DBServername
        {
            get;
            private set;
        }
        public string ErrorMessage
        {
            get;
            private set;
        }

        public string LoadStatus
        {
            get;
            set;
        }
        private ObservableCollection<LoadListView> LoadListViewCollection
        {
            get { return _LoadListViewCollection; }
        }

        public static readonly DependencyProperty TextBackgroundProperty =
            DependencyProperty.Register("BackgroundBrush", typeof(Color), typeof(ControlBoxButtons), new UIPropertyMetadata(null));

        public Color SelectedColor
        {
            get { return (Color)GetValue(SelectedColorProperty); }
            set
            {
                SetValue(SelectedColorProperty, value);
            }
        }

        public static readonly DependencyProperty SelectedColorProperty =
            DependencyProperty.Register("SelectedColor", typeof(Color), typeof(ControlBoxButtons), new UIPropertyMetadata(null));

        public string RangeColor
        {
            get { return _RangeColor; }
            set
            {
                _RangeColor = value;
                OnPropertyChanged("RangeColor");
            }
        }

        #endregion Properties

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

        #region Events

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                //get the settings from the config file      
                GetInitialSettings();

                //Get all the binding IP's
                GetBindingIPS();

                //Retrieve the server settings for Exchange,Ticketing and CMP
                GetSettings();

                //Get the property grid settings
                GetSettingsInfo();

                //Call refresh to save exchange connection when the registry is not having data.
                RefreshControls();

                //Get DHCP Settings.
                GetDHCPSettings();

                //Set DHCP Controls
                RefreshDHCPControls();

                InitializeGridViewColors();

                BindGridViewTypeCombo();

                BindColorRangeListView();

                GetLogPath();

                //Check the visibility of the button upgrade script based on the config setting
                if (strUpgradeVisible.ToUpper() == "FALSE")
                {
                    btnRunUpgradeScript.IsEnabled = false;
                }

                if (SecurityHelper.CurrentUser != null)
                {
                    lblUsername.Text = string.Format("{0}, {1}!", "Welcome", SecurityHelper.CurrentUser.DisplayName);
                }
                else if (UserName != null)
                {
                    lblUsername.Text = string.Format("{0}, {1}!", "Welcome", UserName);
                }
                Int32.TryParse(ConfigManager.Read("ServiceTimeOut"), out serviceTimeOut);
                if (serviceTimeOut <= 0) serviceTimeOut = 5;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("frmBMCExchangeConfig_Load" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
                MessageBox.ShowBox("MessageID74", BMC_Icon.Error);
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
                exchangeConfigEntity.ExchangeInstance = txtExchangeInstance.Text;
                exchangeConfigEntity.ExchangeServerUserName = txtExchangeUsername.Text;
                exchangeConfigEntity.ExchangeServerPassword = txtExchangePassword.Password;


                if (ValidateExchangeServer())
                {
                    bTestConnection = TestConnection(exchangeConfigEntity.ExchangeServer, exchangeConfigEntity.ExchangeServerUserName, exchangeConfigEntity.ExchangeServerPassword, txtExchangeTimeout.Text, txtExchangeInstance.Text, 'E');

                    if (!bTestConnection)
                    {
                        MessageBox.ShowBox("MessageID21", BMC_Icon.Error);
                    }
                    else
                    {
                        bTestConnection = TestConnection(exchangeConfigEntity.ExchangeServer, exchangeConfigEntity.ExchangeServerUserName, exchangeConfigEntity.ExchangeServerPassword, txtExchangeTimeout.Text, txtExchangeInstance.Text, 'T');
                        if (bTestConnection)
                            MessageBox.ShowBox("MessageID128", BMC_Icon.Information);
                        else
                            MessageBox.ShowBox("MessageID19", BMC_Icon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                MessageBox.ShowBox("MessageID74", BMC_Icon.Error);
            }
            finally
            {
                Cursor = System.Windows.Input.Cursors.Arrow;
            }
        }

        private void btnSaveExchangeConnection_Click(object sender, RoutedEventArgs e)
        {
            bool bTestExchangeConnection = false;
            bool bTestTicketingConnection = false;
            bool bInsertSetting = false;
            bool bSetLocCode = false;
            Dictionary<string, string> dictSetregistryentries;
            string strEncryptExchangeConnection = string.Empty;
            string strEncryptExchangeConnectionHex = string.Empty;
            try
            {
                dictSetregistryentries = new Dictionary<string, string>();
                Cursor = System.Windows.Input.Cursors.Wait;

                if (ValidateExchangeServer())
                {
                    if (txtLocCode.Text == string.Empty)
                    {
                        MessageBox.ShowBox("MessageID129", BMC_Icon.Information);
                        txtLocCode.Focus();
                        return;
                    }
                }
                else
                {
                    return;
                }
                bTestExchangeConnection = TestConnection(txtExchangeServer.Text, txtExchangeUsername.Text, txtExchangePassword.Password, txtExchangeTimeout.Text, txtExchangeInstance.Text, 'E');

                //Exchange Server save 
                //if (ValidateText(txtExchangeServer, "Server"))
                //{
                //    if (ValidateText(txtExchangeUsername, "UserName"))
                //    {
                //        System.Windows.Controls.TextBox tempText = new System.Windows.Controls.TextBox();
                //        tempText.Text = txtExchangePassword.Password;
                //        if (ValidateText(tempText, "Password"))
                //        {
                //            if (ValidateText(txtExchangeTimeout, "Connection Timeout"))
                //            {
                //                bTestExchangeConnection = TestConnection(txtExchangeServer.Text, txtExchangeUsername.Text, txtExchangePassword.Password, txtExchangeTimeout.Text, txtExchangeInstance.Text, 'E');
                //                if (!ValidateText(txtExchangeCommandTimeout, "SQLCommandTimeout"))
                //                {
                //                    MessageBox.ShowBox("MessageID136", BMC_Icon.Information);
                //                    txtExchangeCommandTimeout.Focus();
                //                    return;
                //                }
                //            }
                //            else
                //            {
                //                MessageBox.ShowBox("MessageID135", BMC_Icon.Information);
                //                txtExchangeTimeout.Focus();
                //                return;
                //            }
                //        }
                //    }
                //}

                if (bTestExchangeConnection == true)
                {
                    if (!string.IsNullOrEmpty(ReturnConnectionString))
                        ExchangeConfigRegistryEntities.ExchangeConnectionString = ReturnConnectionString;



                    strEncryptExchangeConnection = RegistrySettings.EncryptExchangeConnection();
                    if (!string.IsNullOrEmpty(strEncryptExchangeConnection))
                    {
                        dictSetregistryentries.Add(UIConstants.strSQLConnect, strEncryptExchangeConnection + "+" + "REG_SZ");

                        //Save all Registry Settings under cash master
                        RegistrySettings.SetRegistryEntries(dictSetregistryentries, "Software\\Honeyframe\\" + ExchangeConfigRegistryEntities.RegistryKeyValue);

                        IConfig_Honeyframe honeyframe = ConfigApplicationFactory.GetAny<IConfig_Honeyframe>();
                        if (honeyframe != null)
                        {
                            honeyframe.Honeyframe_SQLCommandTimeout = txtExchangeCommandTimeout.Text;
                        }

                        System.Windows.Forms.Application.DoEvents();
                    }
                    try
                    {
                        this.GetSettings();
                    }
                    catch (Exception Ex)
                    {
                        LogManager.WriteLog("Error reloading setting after Exchange conn save:" + Ex.Message, LogManager.enumLogLevel.Error);
                    }
                }

                bTestTicketingConnection = TestConnection(txtExchangeServer.Text, txtExchangeUsername.Text, txtExchangePassword.Password, txtExchangeTimeout.Text, txtExchangeInstance.Text, 'T');
                if (bTestTicketingConnection == true)
                {
                    if (!string.IsNullOrEmpty(ReturnConnectionString))
                    {
                        ExchangeConfigRegistryEntities.TicketingConnectionString = ReturnConnectionString;
                    }
                    if (!string.IsNullOrEmpty(ExchangeConfigRegistryEntities.TicketingConnectionString))
                    {
                        string ticketServerName = string.Empty;
                        ticketServerName = txtExchangeInstance.Text == string.Empty ? txtExchangeServer.Text : string.Format("{0}\\{1}", txtExchangeServer.Text, txtExchangeInstance.Text);

                        bInsertSetting = RegistrySettings.SetTicketConnectionString(ticketServerName, "Ticketing", txtExchangeUsername.Text, txtExchangePassword.Password, Convert.ToInt32(txtExchangeTimeout.Text));

                        if (bInsertSetting == false)
                        {
                            MessageBox.ShowBox("MessageID26", BMC_Icon.Error);
                        }
                        if (txtLocCode.Text.Length > 0)
                        {
                            bInsertSetting = DBSettings.InsertSettings(UIConstants.TICKETLOCATIONCODENAME, txtLocCode.Text);

                            if (bInsertSetting == false)
                            {
                                MessageBox.ShowBox("MessageID27", BMC_Icon.Error);
                            }

                            bSetLocCode = DBSettings.SetTicketLocationCode(int.Parse(txtLocCode.Text));

                            if (bSetLocCode == false)
                            {
                                MessageBox.ShowBox("MessageID28", BMC_Icon.Error);
                            }
                        }
                        else
                        {
                            MessageBox.ShowBox("MessageID29", BMC_Icon.Information);
                        }
                    }

                }
                if (bTestExchangeConnection)
                {
                    //GetSettings();
                    #region +S001 START
                    //Flag to chk whether cash dispenser to be enabled
                    strIsCDEnabled = DBSettings.GetSettingValue(ReturnConnectionString, "CashDispenserEnabled");
                    strCDType = DBSettings.GetSettingValue(ReturnConnectionString, "CashDispenserType");

                    if (strIsCDEnabled.ToUpper() == "TRUE" && strCDType.ToUpper() == ConfigurationManager.AppSettings.Get("CashDispenserType"))
                    {
                        tiCash_Dispenser.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        if (tiCash_Dispenser.Visibility == Visibility.Visible)
                            tiCash_Dispenser.Visibility = Visibility.Collapsed;
                    }
                    #endregion +S001 END
                    GetServiceStatusToListView();

                }

                if (bTestExchangeConnection)
                {
                    //Check TISEnabled setting
                    sIsTISEnabled = DBSettings.GetSettingValue(ExchangeConfigRegistryEntities.ExchangeConnectionString, "IsTISEnabled");
                    if (sIsTISEnabled.ToUpper() == "TRUE")
                    {
                        tiTISCommunication.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        if (tiTISCommunication.Visibility == Visibility.Visible)
                            tiTISCommunication.Visibility = Visibility.Collapsed;
                    }
                }

                if (bTestExchangeConnection && bTestTicketingConnection)
                {
                    MessageBox.ShowBox("MessageID130", BMC_Icon.Information);
                    RefreshControls();
                    tiCMPConnection.Focus();
                }
                else if ((!bTestExchangeConnection) && bTestTicketingConnection)
                {
                    MessageBox.ShowBox("MessageID21", BMC_Icon.Error);
                    txtExchangeServer.Focus();
                }
                else if (bTestExchangeConnection && (!bTestTicketingConnection))
                {
                    MessageBox.ShowBox("MessageID30", BMC_Icon.Error);
                    txtLocCode.Focus();
                    txtLocCode.SelectionStart = txtLocCode.Text.Length;
                }
                else
                {
                    MessageBox.ShowBox("MessageID131", BMC_Icon.Error);
                    txtExchangeServer.Focus();
                }

                System.Windows.Forms.Application.DoEvents();
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("btnSaveExchangeConnection_Click" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
                MessageBox.ShowBox("MessageID74", BMC_Icon.Error);
            }
            finally
            {
                Cursor = System.Windows.Input.Cursors.Arrow;
            }
        }

        private void btnexchangeDBRestore_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Cursor = System.Windows.Input.Cursors.Wait;
                Sqlrestore sqlRestore = new Sqlrestore("Exchange");
                sqlRestore.ShowDialog();
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("btnexchangeDBRestore_Click" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
                MessageBox.ShowBox("MessageID74", BMC_Icon.Error);
            }
            finally
            {
                Cursor = System.Windows.Input.Cursors.Arrow;
            }
        }

        private void btnTicketDBRestore_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Cursor = System.Windows.Input.Cursors.Wait;
                Sqlrestore sqlRestore = new Sqlrestore("Ticketing");
                sqlRestore.ShowDialog();
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("btnTicketDBRestore_Click" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
                MessageBox.ShowBox("MessageID74", BMC_Icon.Error);
            }
            finally
            {
                Cursor = System.Windows.Input.Cursors.Arrow;
            }
        }

        private void btnCMPDBRestore_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Cursor = System.Windows.Input.Cursors.Wait;
                Sqlrestore sqlRestore = new Sqlrestore("EXTSYSMSG");
                sqlRestore.ShowDialog();
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("btnCMPGatewaySaveChanges_Click" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
                MessageBox.ShowBox("MessageID74", BMC_Icon.Error);
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
            int iEnableDHCP = 0;
            int iEnableEncrypt = 0;
            string strSlotLanIPAddress = string.Empty;
            string strEncryptExchangeConnection = string.Empty;
            string strEncryptPCConnection = string.Empty;
            string strEncryptExchangeConnectionHex = string.Empty;
            bool bTestExchangeConnection = false;
            bool bTestTicketingConnection = false;
            bool bTestCMPConnection = false;
            bool bInsertSetting = false;
            bool bSetLocCode = false;
            string disableMachineOnRemoval = "FALSE";
            string strIPPath = string.Empty;
            Dictionary<string, string> dictSetregistryentries;
            Dictionary<string, string> dictSetNetLoggerRegistryEntry;

            string strSTMEnabled = "0";     //+S002

            try
            {



                if (txtExchangeServer.Text == string.Empty | txtExchangeUsername.Text == string.Empty | txtExchangePassword.Password == string.Empty
                    | txtExchangeTimeout.Text == string.Empty | txtExchangeCommandTimeout.Text == string.Empty)
                {
                    MessageBox.ShowBox("MessageID133", BMC_Icon.Information);
                    txtExchangeServer.Focus();
                    return;
                }
                if (txtLocCode.Text == string.Empty)
                {
                    MessageBox.ShowBox("MessageID129", BMC_Icon.Information);
                    txtLocCode.Focus();
                    return;
                }

                #region +S001 START
                //Validates the txtbox for blank data if Cash Dispenser is enabled before save all settings
                if (strIsCDEnabled.ToUpper() == "TRUE" && strCDType.ToUpper() == ConfigurationManager.AppSettings.Get("CashDispenserType"))
                {
                    if (txtCDServer.Text.Trim() == string.Empty | txtCDServerPort.Text.Trim() == string.Empty |
                        txtCDPassword.Password.Trim() == string.Empty | txtCDUsername.Text.Trim() == string.Empty |
                        txtCDDevicename.Text.Trim() == string.Empty)
                    {
                        MessageBox.ShowBox("MessageID118", BMC_Icon.Information);
                        tiCash_Dispenser.Focus();
                        return;
                    }
                }
                #endregion +S001 END


                if (txtEnterpriseweburl.Text == string.Empty)
                {
                    MessageBox.ShowBox("MessageID102", BMC_Icon.Information);
                    tiServicesSetup.Focus();
                    return;
                }

                //Command Time Out
                BMCRegistryHelper.SetRegKeyValue(string.Empty, UIConstants.SQLCommandTimeOut, RegistryValueKind.String, txtExchangeCommandTimeout.Text);

                if (MessageBox.ShowBox("MessageID24", BMC_Icon.Question, BMC_Button.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    Cursor = System.Windows.Input.Cursors.Wait;

                    dictSetregistryentries = new Dictionary<string, string>();
                    dictSetNetLoggerRegistryEntry = new Dictionary<string, string>();

                    //Exchange Server save 
                    if (ValidateText(txtExchangeServer, "Server"))
                    {
                        if (ValidateText(txtExchangeUsername, "UserName"))
                        {
                            if (ValidateText(txtExchangePassword, "Password"))
                            {
                                if (ValidateText(txtExchangeTimeout, "Connection Timeout"))
                                {
                                    bTestExchangeConnection = TestConnection(txtExchangeServer.Text, txtExchangeUsername.Text, txtExchangePassword.Password, txtExchangeTimeout.Text, txtExchangeInstance.Text, 'E');
                                }
                            }
                        }
                    }
                    if (bTestExchangeConnection == true)
                    {
                        if (!string.IsNullOrEmpty(ReturnConnectionString))
                        {
                            ExchangeConfigRegistryEntities.ExchangeConnectionString = ReturnConnectionString;
                        }

                        strEncryptExchangeConnection = RegistrySettings.EncryptExchangeConnection();

                        if (!string.IsNullOrEmpty(strEncryptExchangeConnection))
                        {
                            dictSetregistryentries.Add(UIConstants.strSQLConnect, strEncryptExchangeConnection + "+" + "REG_SZ");
                        }
                    }
                    else
                    {
                        MessageBox.ShowBox("MessageID25", BMC_Icon.Error);
                    }
                    System.Windows.Forms.Application.DoEvents();

                    //Ticketing server save 
                    if (ValidateExchangeServer())
                    {
                        if (txtLocCode.Text == string.Empty)
                        {
                            MessageBox.ShowBox("MessageID129", BMC_Icon.Information);
                            txtLocCode.Focus();
                            return;
                        }
                        else
                        {
                            bTestTicketingConnection = TestConnection(txtExchangeServer.Text, txtExchangeUsername.Text, txtExchangePassword.Password, txtExchangeTimeout.Text, txtExchangeInstance.Text, 'T');
                        }
                    }
                    if (bTestTicketingConnection == true)
                    {
                        if (!string.IsNullOrEmpty(ReturnConnectionString))
                        {
                            ExchangeConfigRegistryEntities.TicketingConnectionString = ReturnConnectionString;
                        }
                        if (!string.IsNullOrEmpty(ExchangeConfigRegistryEntities.TicketingConnectionString))
                        {
                            string ticketServerName = string.Empty;
                            ticketServerName = txtExchangeInstance.Text == string.Empty ? txtExchangeServer.Text : string.Format("{0}\\{1}", txtExchangeServer.Text, txtExchangeInstance.Text);

                            bInsertSetting = RegistrySettings.SetTicketConnectionString(ticketServerName, "Ticketing", txtExchangeUsername.Text, txtExchangePassword.Password, Convert.ToInt32(txtExchangeTimeout.Text));

                            if (bInsertSetting == false)
                            {
                                MessageBox.ShowBox("MessageID26", BMC_Icon.Error);
                            }
                            if (txtLocCode.Text.Length > 0)
                            {
                                bInsertSetting = DBSettings.InsertSettings(UIConstants.TICKETLOCATIONCODENAME, txtLocCode.Text);

                                if (bInsertSetting == false)
                                {
                                    MessageBox.ShowBox("MessageID27", BMC_Icon.Error);
                                }

                                bSetLocCode = DBSettings.SetTicketLocationCode(int.Parse(txtLocCode.Text));

                                if (bSetLocCode == false)
                                {
                                    MessageBox.ShowBox("MessageID28", BMC_Icon.Error);
                                }
                            }
                            else
                            {
                                MessageBox.ShowBox("MessageID29", BMC_Icon.Information);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.ShowBox("MessageID30", BMC_Icon.Error);
                    }
                    if (chkWebService.IsChecked.Value)
                    {
                        ValidateCMPURL(txtCMPWebURL.Text);
                    }
                    System.Windows.Forms.Application.DoEvents();

                    #region +S001 START
                    //Saves Cash dispenser server details only if cash dispenser is enabled
                    if (strIsCDEnabled.ToUpper() == "TRUE" && strCDType.ToUpper() == ConfigurationManager.AppSettings.Get("CashDispenserType"))
                    {
                        string strCDSetting = txtCDServer.Text + ";" + txtCDServerPort.Text + ";" + txtCDUsername.Text + ";" + txtCDDevicename.Text;

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





                    //CMP Gateway server save
                    //if (Convert.ToBoolean(PTGateway.IsChecked))
                    //{
                    if (ValidateText(txtPCServer, "Server"))
                    {
                        if (ValidateText(txtPCUsername, "UserName"))
                        {
                            if (ValidateText(txtPCPassword, "Password"))
                            {
                                if (ValidateText(txtPCtimeout, "Connection Timeout"))
                                {
                                    bTestCMPConnection = TestConnection(txtPCServer.Text, txtPCUsername.Text, txtPCPassword.Password, txtPCtimeout.Text, txtPCInstance.Text, 'C');
                                }
                            }
                        }
                    }
                    // }
                    //
                    //if (Convert.ToBoolean(PTGateway.IsChecked))
                    //{
                    if (bTestCMPConnection)
                    {
                        if (!string.IsNullOrEmpty(ReturnConnectionString))
                        {
                            ExchangeConfigRegistryEntities.CMPConnectionString = ReturnConnectionString;
                            strEncryptPCConnection = RegistrySettings.EncryptPCConnection();
                            dictSetregistryentries.Add(UIConstants.strPCConnect, strEncryptPCConnection + "+" + "REG_SZ");
                        }
                    }
                    else
                    {
                        // MessageBox.ShowBox("MessageID32", BMC_Icon.Error);
                        dictSetregistryentries.Add(UIConstants.strPCConnect, strEncryptPCConnection + "+" + "REG_SZ");
                    }

                    System.Windows.Forms.Application.DoEvents();

                    bInsertSetting = DBSettings.InsertSettings(UIConstants.CMPMode, chkCMPSocket.IsChecked == true ? "Socket" : "WebService");
                    if (bInsertSetting == false)
                    {
                        MessageBox.ShowBox("MessageID146", BMC_Icon.Error);
                    }

                    dictSetregistryentries.Add(UIConstants.strCMPWebserviceURL.ToString(), txtCMPWebURL.Text.Trim() + "+" + "REG_SZ");

                    //
                    DBBuilder.InsertExportHistory("1", "SITECONFIG");
                    //
                    if (Convert.ToBoolean(PTGateway.IsChecked))
                        SetGateWaySetting("Card_ID_Store", "PTINTERFACE");
                    if (Convert.ToBoolean(SDTGateway.IsChecked))
                        SetGateWaySetting("Card_ID_Store", "SDT");
                    if (Convert.ToBoolean(NOGateway.IsChecked))
                        SetGateWaySetting("Card_ID_Store", "None");

                    bInsertSetting = DBSettings.InsertSettings(UIConstants.PTGatewayIP, (Convert.ToBoolean(PTGateway.IsChecked)) ? txtPTGatewayServerIP.Text : txtGatewayServerIP.Text);
                    if (bInsertSetting == false)
                    {
                        MessageBox.ShowBox("MessageID90", BMC_Icon.Error);
                    }

                    bInsertSetting = DBSettings.InsertSettings(UIConstants.PTGatewayPortNo, txtPTGatewayServerPort.Text);
                    if (bInsertSetting == false)
                    {
                        MessageBox.ShowBox("MessageID91", BMC_Icon.Error);
                    }

                    bInsertSetting = DBSettings.InsertSettings(UIConstants.PTGatewayMsgRspTimeOut, txtPTGatewayTimeOut.Text);
                    if (bInsertSetting == false)
                    {
                        MessageBox.ShowBox("MessageID92", BMC_Icon.Error);
                    }

                    bInsertSetting = DBSettings.InsertSettings(UIConstants.CMPKioskURL, txtCMPKioskURL.Text);
                    if (bInsertSetting == false)
                    {
                        MessageBox.ShowBox("MessageID95", BMC_Icon.Error);
                    }

                    bInsertSetting = DBSettings.InsertSettings(UIConstants.SDTReceiveCAPortNo, txtReceiveCAPortNo.Text);
                    if (bInsertSetting == false)
                    {
                        MessageBox.ShowBox("MessageID112", BMC_Icon.Error);
                    }

                    bInsertSetting = DBSettings.InsertSettings(UIConstants.SDTReceivePTPortNo, txtReceivePTPortNo.Text);
                    if (bInsertSetting == false)
                    {
                        MessageBox.ShowBox("MessageID111", BMC_Icon.Error);
                    }

                    bInsertSetting = DBSettings.InsertSettings(UIConstants.SDTSendCAPortNo, txtSendCAPortNo.Text);
                    if (bInsertSetting == false)
                    {
                        MessageBox.ShowBox("MessageID110", BMC_Icon.Error);
                    }

                    bInsertSetting = DBSettings.InsertSettings(UIConstants.SDTSendPTPortNo, txtSendPTPortNo.Text);
                    if (bInsertSetting == false)
                    {
                        MessageBox.ShowBox("MessageID109", BMC_Icon.Error);
                    }

                    //Certificate Settings Save
                    bInsertSetting = DBSettings.InsertSettings(UIConstants.IsCertificateRequired, chkCertificateRequired.IsChecked == true ? "True" : "False");
                    if (bInsertSetting == false)
                    {
                        MessageBox.ShowBox("MessageID79", BMC_Icon.Error);
                    }

                    bInsertSetting = DBSettings.InsertSettings(UIConstants.CertificateIssuer, txtCertificateIssuer.Text);
                    if (bInsertSetting == false)
                    {
                        MessageBox.ShowBox("MessageID79", BMC_Icon.Error);
                    }

                    if (cmbLanIP.Text == "--Select--")
                    {
                        MessageBox.ShowBox("MessageID84", BMC_Icon.Error);
                    }

                    //Bind IP address
                    if (cmbLanIP.SelectedItem != null)
                    {
                        dictSetregistryentries.Add(UIConstants.strBindIP.ToString(), cmbLanIP.SelectedItem.ToString() + "+" + "REG_SZ");
                        dictSetregistryentries.Add(UIConstants.strDefaultServerIP.ToString(), cmbLanIP.SelectedItem.ToString() + "+" + "REG_SZ");
                        dictSetNetLoggerRegistryEntry.Add(UIConstants.strNetLogger.ToString(), cmbLanIP.SelectedItem.ToString() + "+" + "REG_SZ");
                    }
                    else
                    {
                        MessageBox.ShowBox("MessageID33", BMC_Icon.Error);
                    }

                    System.Windows.Forms.Application.DoEvents();

                    //DHCP Server settings  
                    if (cmbSlotLan.SelectedItem != null)
                    {
                        if (cmbSlotLan.SelectedItem.ToString() == "--Select--")
                        {
                            MessageBox.ShowBox("MessageID85", BMC_Icon.Error);
                        }
                    }

                    if (chkEnableDHCP.IsChecked == true)
                    {
                        iEnableDHCP = 1;
                    }
                    else
                    {
                        iEnableDHCP = 0;
                    }

                    if (chkEnableDHCP.IsChecked == true && txtMultiCastIP.Text != string.Empty)
                    {
                        if (!exchangeConfigEntity.IsValidIP(txtMultiCastIP.Text))
                        {
                            MessageBox.ShowBox("MessageID82", BMC_Icon.Error);
                        }
                    }

                    //Invalid TIS Fields

                    if (tiTISCommunication.Visibility == Visibility.Visible)
                    {
                        if ((chkSocket.IsChecked == true))
                        {
                            if (!exchangeConfigEntity.IsValidIP(txtIPAddress.Text))
                            {
                                MessageBox.ShowBox("MessageID137", BMC_Icon.Error);
                                return;
                            }
                            if ((string.IsNullOrEmpty(txtReceiverPortNumber.Text)) || (txtReceiverPortNumber.Text.Length > 5))
                            {
                                MessageBox.ShowBox("MessageID138", BMC_Icon.Error);
                                return;
                            }
                            if (string.IsNullOrEmpty(txtSenderPortNumber.Text) || (txtSenderPortNumber.Text.Length > 5))
                            {
                                MessageBox.ShowBox("MessageID142", BMC_Icon.Error);
                                return;
                            }
                        }



                        if ((!exchangeConfigEntity.IsValidURL(txtCommServiceURL.Text)) || (string.IsNullOrEmpty(txtCommServiceURL.Text)) || (txtCommServiceURL.Text.Length > 256))
                        {
                            MessageBox.ShowBox("MessageID140", BMC_Icon.Error);
                            return;
                        }


                        if ((string.IsNullOrEmpty(txtTicketPrefix.Text)) || txtTicketPrefix.Text.Length > 1)
                        {
                            MessageBox.ShowBox("MessageID139", BMC_Icon.Error); // Invalid TIS Ticket Prefix
                            txtTicketPrefix.Text = string.Empty;
                            return;
                        }

                        //if ((string.IsNullOrEmpty(txtExternalSiteCode.Text)) || txtExternalSiteCode.Text.Length != 3)
                        //{
                        //    MessageBox.ShowBox("MessageID143", BMC_Icon.Error); // Invalid External Site Code
                        //    txtExternalSiteCode.Text = string.Empty;
                        //}

                        if ((string.IsNullOrEmpty(txtExternalCasinoCode.Text)) || (txtExternalCasinoCode.Text.Length > 50))
                        {
                            MessageBox.ShowBox("MessageID144", BMC_Icon.Error); // Invalid External Casino Code
                            txtExternalCasinoCode.Text = string.Empty;
                            return;
                        }

                        if ((!exchangeConfigEntity.IsValidURL(txtCmdWebServiceURL.Text)) || (string.IsNullOrEmpty(txtCmdWebServiceURL.Text)) || (txtCmdWebServiceURL.Text.Length > 256))
                        {
                            MessageBox.ShowBox("MessageID143", BMC_Icon.Error); // Invalid External Web Service URL

                            return;
                        }

                        if (bTestExchangeConnection)
                        {
                            string TISConnectionMode = string.Empty;
                            string TISDataExchangeConnectionMode = string.Empty;
                            if (chkSocket.IsChecked == true || sTISCommunicationModeSetting.Trim().ToUpper().Equals("SOCKET"))
                                TISConnectionMode = "Socket";
                            else if (chkWebService.IsChecked == true || sTISCommunicationModeSetting.Trim().ToUpper().Equals("WEBSERVICE"))
                                TISConnectionMode = "WebService";
                            //if (chkDataExchageSocket.IsChecked == true)
                            //    TISDataExchangeConnectionMode = "Socket";
                            //else if (chkDataExchangeWebService.IsChecked == true)
                            //    TISDataExchangeConnectionMode = "WebService";
                            string TISIPAddress = txtIPAddress.Text;
                            string TISPortNumber = txtReceiverPortNumber.Text;
                            string TISWebServiceURL = txtCommServiceURL.Text;
                            string TISTicketPrefix = txtTicketPrefix.Text;
                            //  string TISDataExchangeIPAddress = txtDataExchageIPAddress.Text;
                            string TISDataExchangePortNumber = txtSenderPortNumber.Text;
                            string TISExternalWebServiceURL = txtCmdWebServiceURL.Text;
                            // int ExternalSiteCode = Convert.ToInt32(txtExternalSiteCode.Text);
                            string TISExternalCasinoCode = txtExternalCasinoCode.Text;
                            bool DBInsertTISSettings = DBSettings.InsertTISSettings(TISConnectionMode, TISIPAddress, TISPortNumber, TISWebServiceURL, TISTicketPrefix, TISDataExchangePortNumber, TISExternalWebServiceURL, TISExternalCasinoCode);
                            // bool UpdateSiteCode = DBSettings.UpdateExternalSiteCode(ExternalSiteCode,ExternalCasinoCode);


                        }

                    }



                    if (chkEnableDHCP.IsChecked == true && txtInterfaceIP.Text != string.Empty)
                    {
                        if (!ValidateIP(txtInterfaceIP.Text))
                        {
                            MessageBox.ShowBox("MessageID83", BMC_Icon.Error);

                        }
                    }

                    if (chkEnableDHCP.IsChecked == false)
                    {
                        if (txtMultiCastIP.Text == string.Empty)
                        {
                            MessageBox.ShowBox("MessageID80", BMC_Icon.Error);
                        }
                        else
                        {
                            if (!ValidateIP(txtMultiCastIP.Text))
                            {
                                MessageBox.ShowBox("MessageID82", BMC_Icon.Error);
                            }
                        }

                        if (txtInterfaceIP.Text == string.Empty)
                        {
                            MessageBox.ShowBox("MessageID81", BMC_Icon.Error);
                        }
                        else
                        {
                            if (!ValidateIP(txtInterfaceIP.Text))
                            {
                                MessageBox.ShowBox("MessageID83", BMC_Icon.Error);
                            }
                        }
                    }

                    if (cmbSlotLan.SelectedItem != null)
                    {
                        strSlotLanIPAddress = cmbSlotLan.SelectedItem.ToString();
                        dictSetregistryentries.Add(UIConstants.strDHCPServerIP.ToString(), strSlotLanIPAddress.ToString() + "+" + "REG_SZ");
                        dictSetregistryentries.Add(UIConstants.strDHCPEnabled.ToString(), iEnableDHCP.ToString() + "+" + "REG_DWORD");
                        dictSetregistryentries.Add(UIConstants.strMulticastip.ToString(), txtMultiCastIP.Text.ToString().Trim() + "+" + "REG_SZ");
                        dictSetregistryentries.Add(UIConstants.strInterface.ToString(), txtInterfaceIP.Text.ToString().Trim() + "+" + "REG_SZ");
                    }
                    else
                    {
                        MessageBox.ShowBox("MessageID34", BMC_Icon.Error);
                    }

                    //To save log path
                    if (txtFileChoose.Text.Trim() != string.Empty)
                    {
                        if (Directory.Exists(txtFileChoose.Text.Trim()))
                        {
                            BMCRegistryHelper.SetRegKeyValue(string.Empty, UIConstants.DefaultLogDir, RegistryValueKind.String, txtFileChoose.Text.Trim());
                        }
                        else
                        {
                            System.Windows.Forms.DialogResult dlgResult = MessageBox.ShowBox("MessageID160", BMC_Icon.Question, BMC_Button.YesNo);//Folder does not exists. Do you want to create?
                            if (dlgResult == System.Windows.Forms.DialogResult.Yes)
                            {
                                Directory.CreateDirectory(txtFileChoose.Text.Trim());
                                BMCRegistryHelper.SetRegKeyValue(string.Empty, UIConstants.DefaultLogDir, RegistryValueKind.String, txtFileChoose.Text.Trim());
                            }
                            else
                            {
                                MessageBox.ShowBox("MessageID161", BMC_Icon.Information);//Folder does not exists.
                                txtFileChoose.Focus();
                                return;
                            }
                        }
                    }
                    else
                    {
                        MessageBox.ShowBox("MessageID158", BMC_Icon.Information);//Please enter a valid log file path.
                        txtFileChoose.Focus();
                        return;
                    }

                    System.Windows.Forms.Application.DoEvents();

                    //Encrypt enable settings
                    if (chkEnableEncrypt.IsChecked == true)
                        iEnableEncrypt = 1;
                    else
                        iEnableEncrypt = 0;

                    dictSetregistryentries.Add(UIConstants.strEncryptEnable, iEnableEncrypt.ToString() + "+" + "REG_DWORD");
                    System.Windows.Forms.Application.DoEvents();

                    //Encrypt enable settings 
                    if (chkEnableRSAEncrypt.IsChecked == true)
                        iEnableEncrypt = 1;
                    else
                        iEnableEncrypt = 0;

                    dictSetregistryentries.Add(UIConstants.strEnableRSAEncrypt, iEnableEncrypt.ToString() + "+" + "REG_DWORD");

                    //machine disable settings
                    disableMachineOnRemoval = chkDisableMachineonRemoval.IsChecked == true ? "TRUE" : "FALSE";
                    bInsertSetting = DBSettings.InsertSettings(UIConstants.DisableMachineOnRemoval, disableMachineOnRemoval);
                    if (bInsertSetting == false)
                    {
                        MessageBox.ShowBox("MessageID108", BMC_Icon.Error);
                    }

                    //BGS Web service   
                    try
                    {
                        if ((txtEnterpriseweburl.Text.Trim().Length) > 0)
                            if (strUrlvalidate == string.Empty)//If test functionality not used 
                                dictSetregistryentries.Add(UIConstants.strBGSWebservice.ToString(), txtEnterpriseweburl.Text.Trim() + "+" + "REG_SZ");
                            else
                                dictSetregistryentries.Add(UIConstants.strBGSWebservice.ToString(), strUrlvalidate + "+" + "REG_SZ");
                        else
                            MessageBox.ShowBox("MessageID36", BMC_Icon.Error);
                    }
                    catch (Exception ex)
                    {
                        ExceptionManager.Publish(ex);
                    }

                    //LFM Web service  
                    try
                    {
                        if ((txtEnterpriseweburl.Text.Trim().Length) > 0)
                        {
                            string lfmWebService = txtEnterpriseweburl.Text.Trim().ToLower().Replace("enterprise", "LFM");
                            dictSetregistryentries.Add(UIConstants.strLFMWebservice.ToString(), lfmWebService + "+" + "REG_SZ");
                        }
                    }
                    catch (Exception ex)
                    {
                        ExceptionManager.Publish(ex);
                    }

                    #region +S002 START
                    //EVENT TRANSMITTER 
                    //STM

                    strSTMEnabled = (chkEnableTransmit.IsChecked.Value) ? "1" : "0";

                    DBSettings.InsertSettings("IsTransmitterEnabled", strSTMEnabled);
                    if (strSTMEnabled != "0")
                    {
                        if (txtEventServer.Text.Trim() != string.Empty)
                        {
                            DBSettings.InsertSettings("STMServerIP", txtEventServer.Text.Trim());
                        }
                        else
                        {
                            MessageBox.ShowBox("MessageID113", BMC_Icon.Information);
                            TabEventTransmitter.Focus();
                            txtEventServer.Focus();
                            return;
                        }
                        /*if (txtEventServerPort.Text.Trim() != string.Empty)
                        {
                            DBSettings.InsertSettings("STMServerPort", txtEventServerPort.Text.Trim());
                        }
                        else
                        {
                            MessageBox.ShowBox("MessageID114", BMC_Icon.Information);
                            TabEventTransmitter.Focus();
                            txtEventServerPort.Focus();
                            return;
                        }
                        if (txtEventAllowedMessages.Text.Trim() != string.Empty)
                        {
                            DBSettings.InsertSettings("STMAllowedMessages", txtEventAllowedMessages.Text.Trim());
                        }
                        else
                        {
                            MessageBox.ShowBox("MessageID115", BMC_Icon.Information);
                            TabEventTransmitter.Focus();
                            txtEventAllowedMessages.Focus();
                            return;
                        }*/
                    }
                    else
                    {
                        DBSettings.InsertSettings("STMServerIP", txtEventServer.Text.Trim());
                        /*DBSettings.InsertSettings("STMServerPort", txtEventServerPort.Text.Trim());
                        DBSettings.InsertSettings("STMAllowedMessages", txtEventAllowedMessages.Text.Trim());*/
                    }
                    #endregion S002 +END

                    #region CommsConfig
                    string sInstallPath = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
                    string sCommsConfigLocations = string.Empty;
                    string sComConfigFileName = string.Empty;

                    int EnableRSAEncrypt = chkEnableRSAEncrypt.IsChecked == true ? 1 : 0;
                    int EnableEncrypt = chkEnableEncrypt.IsChecked == true ? 1 : 0;
                    int idisableMachineOnRemoval = chkDisableMachineonRemoval.IsChecked == true ? 1 : 0;

                    sComConfigFileName = ConfigurationManager.AppSettings.Get("CommsConfigName");
                    sCommsConfigLocations = ConfigurationManager.AppSettings.Get("CommsConfigLocations");

                    sComConfigFileName = sComConfigFileName == null ? "" : sComConfigFileName;
                    sCommsConfigLocations = sCommsConfigLocations == null ? "" : sCommsConfigLocations;

                    List<string> slstCommsConfigLocations = new List<string>();

                    slstCommsConfigLocations.AddRange(sCommsConfigLocations.Split(','));

                    if (sComConfigFileName.Trim() != string.Empty)
                    {
                        //Writes new config file
                        if (!ComsConfig.SaveServerConfig(sInstallPath, sComConfigFileName, cmbLanIP.Text, cmbLanIP.Text, strSlotLanIPAddress, iEnableDHCP, txtMultiCastIP.Text, txtInterfaceIP.Text, EnableEncrypt, EnableRSAEncrypt, idisableMachineOnRemoval, slstCommsConfigLocations))
                        {
                            LogManager.WriteLog("CommsConfig File Creation error.", LogManager.enumLogLevel.Error);
                        }
                    }
                    #endregion CommsConfig

                    //Save all Registry Settings under cash master
                    RegistrySettings.SetRegistryEntries(dictSetregistryentries, "Software\\Honeyframe\\" + ExchangeConfigRegistryEntities.RegistryKeyValue);
                    System.Windows.Forms.Application.DoEvents();

                    //Save all Registry Settings under NetLogger                    
                    RegistrySettings.SetRegistryEntries(dictSetNetLoggerRegistryEntry, "Software\\Honeyframe\\" + ExchangeConfigRegistryEntities.NetLoggerRegKeyValue);
                    System.Windows.Forms.Application.DoEvents();

                    //Save System settings for settings DB
                    foreach (PropertyClass pc in ChangedProperty)
                        DataBaseServiceHandler.ExecuteNonQuery(QueryType.Text, "Update Setting Set Setting_Value = '" + pc.Value.Trim() + "' Where Setting_Name = '" + pc.Name.Trim() + "'");

                    //Save all Registry Settings under Honeyframe
                    RegistrySettings.SetRegistryEntries(dictSetregistryentries, ExchangeConfigRegistryEntities.HoneyFrameKeyValue);
                    System.Windows.Forms.Application.DoEvents();

                    ChangedProperty = new List<PropertyClass>();
                    System.Windows.Forms.Application.DoEvents();

                    SetRegistryValueForDependOnServiceKey();

                    SaveLogPathForBMCWrap();

                    MessageBox.ShowBox("MessageID37", BMC_Icon.Information);
                    MessageBox.ShowBox("MessageID59", BMC_Icon.Information);

                    tiSummary.IsEnabled = true;
                    tiSummary.Focus();
                    GetSettingsInfo();
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Save Button Settings" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
                MessageBox.ShowBox("MessageID74", BMC_Icon.Error);
            }
            finally
            {
                Cursor = System.Windows.Input.Cursors.Arrow;
            }
        }

        private void btnTicketingTestConnection_Click(object sender, RoutedEventArgs e)
        {
            //bool bTestConnection = false;
            //try
            //{
            //    Cursor = System.Windows.Input.Cursors.Wait;
            //    if (ValidateText(txtticketserver, "Server"))
            //    {
            //        if (ValidateText(txtticketusername, "UserName"))
            //        {
            //            if (ValidateText(txtticketPassword, "Password"))
            //            {
            //                if (ValidateText(txtticketTimeout, "Connection Timeout"))
            //                {
            //                    bTestConnection = TestConnection(txtticketserver.Text, txtticketusername.Text, txtticketPassword.Password, txtticketTimeout.Text, txticketInstance.Text, 'T');
            //                }
            //            }
            //        }
            //    }
            //    if (bTestConnection == true)
            //    {
            //        MessageBox.ShowBox("MessageID18", BMC_Icon.Information);
            //        tiCMPConnection.Focus();
            //    }
            //    else
            //    {
            //        MessageBox.ShowBox("MessageID19", BMC_Icon.Error);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    LogManager.WriteLog("btnTicketingTestConnection_Click" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
            //    ExceptionManager.Publish(ex);
            //    MessageBox.ShowBox("MessageID74", BMC_Icon.Error);
            //}
            //finally
            //{
            //    Cursor = System.Windows.Input.Cursors.Arrow;
            //}
        }

        private void btnRunUpgradeScript_Click(object sender, RoutedEventArgs e)
        {
            FileInfo file = null;
            SqlConnection sqlConnection = null;
            string scriptFile = string.Empty;

            try
            {
                LogManager.WriteLog("Inside btnRunUpgradeScript_Click event", LogManager.enumLogLevel.Info);

                this.Cursor = System.Windows.Input.Cursors.Wait;

                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.InitialDirectory = ConfigManager.Read("DBScriptsDefaultPath") != null ? ConfigManager.Read("DBScriptsDefaultPath") : "C:\\Program Files\\Bally Technologies\\Exchange Server\\Database";

                if (openFileDialog.ShowDialog() == true)
                {
                    file = new FileInfo(openFileDialog.FileName);
                    scriptFile = file.OpenText().ReadToEnd();

                    DBSettings.ExecuteScripts(strConnection, scriptFile);

                    LogManager.WriteLog("Script executed successfully", LogManager.enumLogLevel.Info);

                    MessageBox.ShowBox("MessageID86", BMC_Icon.Information);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                MessageBox.ShowBox("MessageID74", BMC_Icon.Error);
            }
            finally
            {
                if (file != null)
                {
                    file = null;
                }
                if (sqlConnection != null)
                {
                    sqlConnection.Close();
                    sqlConnection.Dispose();
                    sqlConnection = null;
                }

                this.Cursor = System.Windows.Input.Cursors.Arrow;
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
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
                MessageBox.ShowBox("MessageID103", BMC_Icon.Error);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                MessageBox.ShowBox("MessageID104", BMC_Icon.Error);
            }
            finally
            {
                Cursor = System.Windows.Input.Cursors.Arrow;
            }
        }

        //private void chkUseExchangeConnection_Checked(object sender, RoutedEventArgs e)
        //{
        //Use exchange credentials if checked.
        //if (chkUseExchangeConnection.IsChecked == true)
        //{
        //    txtticketserver.Text = txtExchangeServer.Text;
        //    txtticketusername.Text = txtExchangeUsername.Text;
        //    txtticketPassword.Password = txtExchangePassword.Password;
        //    txtticketTimeout.Text = txtExchangeTimeout.Text;
        //    txticketInstance.Text = txtExchangeInstance.Text;

        //    if (tbTicketingPasswordRequired.Visibility == Visibility.Visible)
        //    {
        //        tbTicketingPasswordRequired.Visibility = Visibility.Hidden;
        //        tbTicketingPasswordRequired.ToolTip = null;
        //    }
        //}
        //else
        //{
        //    //Enter new credentials
        //    txtticketserver.Text = string.Empty;
        //    txtticketusername.Text = string.Empty;
        //    txtticketPassword.Password = string.Empty;
        //    txtticketTimeout.Text = string.Empty;
        //    txticketInstance.Text = string.Empty;
        //}
        //}

        private void chkUseExchangeConnect_Checked(object sender, RoutedEventArgs e)
        {
            //Use exchange credentials if checked.
            if (chkUseExchangeConnect.IsChecked == true)
            {
                txtPCServer.Text = txtExchangeServer.Text;
                txtPCUsername.Text = txtExchangeUsername.Text;
                txtPCPassword.Password = txtExchangePassword.Password;
                txtPCtimeout.Text = txtExchangeTimeout.Text;
                txtPCInstance.Text = txtExchangeInstance.Text;

                if (tbPCPasswordRequired.Visibility == Visibility.Visible)
                {
                    tbPCPasswordRequired.Visibility = Visibility.Hidden;
                    tbPCPasswordRequired.ToolTip = null;
                }
            }
            else
            {
                //Enter new credentials
                txtPCServer.Text = string.Empty;
                txtPCUsername.Text = string.Empty;
                txtPCPassword.Password = string.Empty;
                txtPCtimeout.Text = string.Empty;
                txtPCInstance.Text = string.Empty;
            }
        }

        private void chkUseExchangeConnect_Unchecked(object sender, RoutedEventArgs e)
        {
            txtPCServer.Text = string.Empty;
            txtPCUsername.Text = string.Empty;
            txtPCPassword.Password = string.Empty;
            txtPCtimeout.Text = string.Empty;
            txtPCInstance.Text = string.Empty;
            ValidatePasswordBox(txtPCPassword);
        }

        //private void chkUseExchangeConnection_Unchecked(object sender, RoutedEventArgs e)
        //{
        //    txtticketserver.Text = string.Empty;
        //    txtticketusername.Text = string.Empty;
        //    txtticketPassword.Password = string.Empty;
        //    txtticketTimeout.Text = string.Empty;
        //    txticketInstance.Text = string.Empty;
        //    ValidatePasswordBox(txtticketPassword);
        //}

        private void TabItem_GotFocus(object sender, RoutedEventArgs e)
        {
            string ExchangeDBNameValue = "Exchange";
            DataTable dtSummary = new DataTable("Summary");

            dtSummary.Columns.Add("Name", typeof(string));
            dtSummary.Columns.Add("Value", typeof(string));

            dtSummary.Rows.Add("Exchange DB Server", txtExchangeServer.Text);
            dtSummary.Rows.Add("Exchange DB Instance", txtExchangeInstance.Text);
            dtSummary.Rows.Add("Exchange DB Username", txtExchangeUsername.Text);
            dtSummary.Rows.Add("Exchange DB Name", ExchangeDBNameValue);
            dtSummary.Rows.Add("Exchange DB Time Out", txtExchangeTimeout.Text);

            dtSummary.Rows.Add("Ticketing DB Server", txtExchangeServer.Text);
            dtSummary.Rows.Add("Ticketing DB Instance", txtExchangeInstance.Text);
            dtSummary.Rows.Add("Ticketing DB Username", txtExchangeUsername.Text);
            dtSummary.Rows.Add("Ticketing DB Name", "Ticketing");
            dtSummary.Rows.Add("Ticketing DB Time Out", txtExchangeTimeout.Text);
            dtSummary.Rows.Add("Location Code", txtLocCode.Text);

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

            dtSummary.Rows.Add("CMP Socket Mode", chkCMPSocket.IsChecked == true ? "True" : "False");
            dtSummary.Rows.Add("CMP WebService Mode", chkWebService.IsChecked == true ? "True" : "False");
            dtSummary.Rows.Add("CMP WebService URL", txtCMPWebURL.Text);

            dtSummary.Rows.Add("PC DB Server", txtPCServer.Text);
            dtSummary.Rows.Add("PC DB Instance", txtPCInstance.Text);
            dtSummary.Rows.Add("PC DB Username", txtPCUsername.Text);
            dtSummary.Rows.Add("PC DB Name", lblPCDB.Text);
            dtSummary.Rows.Add("PC DB Time Out", txtPCtimeout.Text);

            dtSummary.Rows.Add("Gateway Server IP", txtPTGatewayServerIP.Text);
            dtSummary.Rows.Add("Gateway Server Port No", txtPTGatewayServerPort.Text);
            dtSummary.Rows.Add("Gateway Message Response Time Out", txtPTGatewayTimeOut.Text);
            //dtSummary.Rows.Add("CMP App Username", txtCMPAppUserName.Text);
            dtSummary.Rows.Add("CMP Kiosk URL", txtCMPKioskURL.Text);
            dtSummary.Rows.Add("Gateway SDT Server IP", txtGatewayServerIP.Text);
            dtSummary.Rows.Add("SDT Send CA Port No", txtSendCAPortNo.Text);
            dtSummary.Rows.Add("SDT Send PT Port No", txtSendPTPortNo.Text);
            dtSummary.Rows.Add("SDT Receive CA Port No", txtReceiveCAPortNo.Text);
            dtSummary.Rows.Add("SDT Receive PT Port No", txtReceivePTPortNo.Text);

            dtSummary.Rows.Add("Enterprise Server URL", txtEnterpriseweburl.Text);

            dtSummary.Rows.Add("Certificate Required", chkCertificateRequired.IsChecked == true ? "True" : "False");
            dtSummary.Rows.Add("Certificate Issuer", txtCertificateIssuer.Text);

            dtSummary.Rows.Add("Enable Encrypt", chkEnableEncrypt.IsChecked == true ? "True" : "False");
            dtSummary.Rows.Add("Enable RSA Encrypt", chkEnableRSAEncrypt.IsChecked == true ? "True" : "False");
            dtSummary.Rows.Add("Disable Machine on Removal", chkDisableMachineonRemoval.IsChecked == true ? "True" : "False");
            if (cmbLanIP.SelectedValue != null)
                dtSummary.Rows.Add("Corporate LAN", cmbLanIP.SelectionBoxItem.ToString());
            else
                dtSummary.Rows.Add("Corporate LAN", string.Empty);

            dtSummary.Rows.Add("Enable DHCP", chkEnableDHCP.IsChecked == true ? "True" : "False");
            if (cmbSlotLan.SelectedValue != null)
                dtSummary.Rows.Add("Slot LAN", cmbSlotLan.SelectionBoxItem.ToString());
            else
                dtSummary.Rows.Add("Slot LAN", string.Empty);
            dtSummary.Rows.Add("Multicast IP", txtMultiCastIP.Text);
            dtSummary.Rows.Add("Slot LAN Interface IP", txtInterfaceIP.Text);
            dtSummary.Rows.Add("Log Path", txtFileChoose.Text);

            lvSummary.DataContext = dtSummary;
        }

        private void btnCMPTest_Click(object sender, RoutedEventArgs e)
        {
            bool bTestConnection = false;
            try
            {
                Cursor = System.Windows.Input.Cursors.Wait;
                if (ValidateText(txtPCServer, "Server"))
                {
                    if (ValidateText(txtPCUsername, "UserName"))
                    {
                        if (ValidateText(txtPCPassword, "Password"))
                        {
                            if (ValidateText(txtPCtimeout, "Connection Timeout"))
                            {
                                bTestConnection = TestConnection(txtPCServer.Text, txtPCUsername.Text, txtPCPassword.Password, txtPCtimeout.Text, txtPCInstance.Text, 'C');
                            }
                        }
                    }
                }
                if (bTestConnection == true)
                {
                    MessageBox.ShowBox("MessageID156", BMC_Icon.Information);
                }
                else
                {
                    MessageBox.ShowBox("MessageID157", BMC_Icon.Error);
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("btnCMPGatewayTestConnection_Click" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
                MessageBox.ShowBox("MessageID74", BMC_Icon.Error);
            }
            finally
            {
                Cursor = System.Windows.Input.Cursors.Arrow;
            }
        }

        private void btnCreateMSMQ_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Cursor = System.Windows.Input.Cursors.Wait;

                ProgressBarMain.Value = 0;
                if (ReadServicesSettings.CheckMSMQExists(UIConstants.strExchangeQueuePath) == false)
                {
                    ProgressBarMain.Value = 20;
                    MessageBox.ShowBox("MessageID9", BMC_Icon.Information);
                    ProgressBarMain.Value = 0;
                }
                else
                {
                    ProgressBarMain.Value = 20;
                    if (ReadServicesSettings.CreateMSMQ(UIConstants.strExchangeQueuePath) == true)
                    {
                        ProgressBarMain.Value = 100;
                        MessageBox.ShowBox("MessageID10", BMC_Icon.Information);
                        ProgressBarMain.Value = 0;
                    }
                    else
                    {
                        ProgressBarMain.Value = 20;
                        MessageBox.ShowBox("MessageID11", BMC_Icon.Error);
                        ProgressBarMain.Value = 0;
                    }
                }

            }
            catch (Exception ex)
            {
                if (ex.Message.Equals("Message Queuing has not been installed on this computer."))
                {
                    MessageBox.ShowBox("MessageID12", BMC_Icon.Error);
                    LogManager.WriteLog("btnCreateMSMQ_Click" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
                    ExceptionManager.Publish(ex);
                }
                else
                {
                    MessageBox.ShowBox("MessageID13", BMC_Icon.Error);
                    LogManager.WriteLog("btnCreateMSMQ_Click" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
                    ExceptionManager.Publish(ex);
                }
            }
            finally
            {
                Cursor = System.Windows.Input.Cursors.Arrow;
            }
        }

        private void btnRefreshServices_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Cursor = System.Windows.Input.Cursors.Wait;

                GetServiceStatusToListView();

                foreach (LoadListView item in lvServiceslist.Items)
                {
                    item.Check = 0;
                }

                if (btnSelectAll.Content.ToString().ToUpper().Equals("DESELECT ALL"))
                {
                    btnSelectAll.Content = "Select All";
                }
            }
            finally
            {
                Cursor = System.Windows.Input.Cursors.Arrow;
            }
        }

        private void btnSelectAll_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LogManager.WriteLog("Inside Method", LogManager.enumLogLevel.Info);

                Cursor = System.Windows.Input.Cursors.Wait;

                if (btnSelectAll.Content.ToString().ToUpper().Equals("SELECT ALL"))
                {
                    foreach (LoadListView item in lvServiceslist.Items)
                    {
                        item.Check = 1;
                    }

                    btnSelectAll.Content = "DeSelect All";
                }
                else
                {
                    foreach (LoadListView item in lvServiceslist.Items)
                    {
                        item.Check = 0;
                    }

                    btnSelectAll.Content = "Select All";
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                MessageBox.ShowBox("MessageID74", BMC_Icon.Error);
            }
            finally
            {
                Cursor = System.Windows.Input.Cursors.Arrow;
            }
        }

        private void EnableButtons(bool status)
        {
            btnStartService.IsEnabled = status;
            btnStopService.IsEnabled = status;
            btnRefreshServices.IsEnabled = status;
            btnSelectAll.IsEnabled = status;
        }

        private void btnStartService_Click(object sender, RoutedEventArgs e)
        {
            bool bServiceStatus = false;
            bool bChecked = false;
            ProgressBarMain.Value = 0;
            string serviceName = string.Empty;

            try
            {
                LogManager.WriteLog("Inside Method", LogManager.enumLogLevel.Info);

                Cursor = System.Windows.Input.Cursors.Wait;
                EnableButtons(false);
                var servicesCollection = lvServiceslist.Items;


                foreach (LoadListView item in servicesCollection)
                {
                    if (item.Check == 1)
                    {
                        bChecked = true;
                        item.Check = 0;
                        serviceName = item.ServiceName;

                        bServiceStatus = StartService(item.ServiceName.Trim());

                        System.Windows.Forms.Application.DoEvents();

                        ProgressBarMain.Value += 15;

                        if (bServiceStatus)
                        {
                            item.ServiceStatus = "Running";
                            MessageBox.ShowText(string.Format("{0} - {1} {2}.", FindResource("MessageID2"), serviceName, FindResource("MessageID3")), BMC_Icon.Information);
                            LogManager.WriteLog(string.Format("{0} - {1} {2}.", FindResource("MessageID2"), serviceName, FindResource("MessageID3")), LogManager.enumLogLevel.Info);
                        }
                        else
                        {
                            item.ServiceStatus = "Start Failed";
                            MessageBox.ShowText(string.Format("{0} - {1} {2}.", FindResource("MessageID5"), serviceName, FindResource("MessageID7")), BMC_Icon.Error);
                            LogManager.WriteLog(string.Format("{0} - {1} {2}.", FindResource("MessageID5"), serviceName, FindResource("MessageID7")), LogManager.enumLogLevel.Error);
                        }
                    }
                }

                ProgressBarMain.Value = 100;
                btnSelectAll.Content = "Select All";
                GetServiceStatusToListView();

                if (!bChecked)
                {
                    LogManager.WriteLog("No service(s) selected", LogManager.enumLogLevel.Info);

                    MessageBox.ShowBox("MessageID8", BMC_Icon.Information);
                }
            }
            catch (IndexOutOfRangeException iex)
            {
                MessageBox.ShowText(string.Format("{0} - {1} {2}.", FindResource("MessageID5"), serviceName, FindResource("MessageID7")), BMC_Icon.Error);
                LogManager.WriteLog("btnStartService_Click" + iex.Message.ToString() + iex.Source.ToString(), LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(iex);
            }

            catch (Exception ex)
            {
                MessageBox.ShowText(string.Format("{0} - {1} {2}.", FindResource("MessageID5"), serviceName, FindResource("MessageID7")), BMC_Icon.Error);
                LogManager.WriteLog("btnStartService_Click" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
            }
            finally
            {
                ProgressBarMain.Value = 0;
                Cursor = System.Windows.Input.Cursors.Arrow;
                EnableButtons(true);
            }
        }

        private void btnStopService_Click(object sender, RoutedEventArgs e)
        {
            bool bServiceStatus = false;
            bool bChecked = false;
            ProgressBarMain.Value = 0;
            string serviceName = string.Empty;

            try
            {
                LogManager.WriteLog("Inside Method", LogManager.enumLogLevel.Info);

                Cursor = System.Windows.Input.Cursors.Wait;
                EnableButtons(false);
                var servicesCollection = lvServiceslist.Items;

                foreach (LoadListView item in servicesCollection)
                {
                    if (item.Check == 1)
                    {
                        bChecked = true;
                        item.Check = 0;
                        serviceName = item.ServiceName;

                        bServiceStatus = EndService(item.ServiceName);

                        System.Windows.Forms.Application.DoEvents();

                        ProgressBarMain.Value += 15;

                        if (bServiceStatus)
                        {
                            item.ServiceStatus = "Stopped";
                            MessageBox.ShowText(string.Format("{0} - {1} {2}.", FindResource("MessageID2"), serviceName, FindResource("MessageID4")), BMC_Icon.Information);
                            LogManager.WriteLog(string.Format("{0} - {1} {2}.", FindResource("MessageID2"), serviceName, FindResource("MessageID4")), LogManager.enumLogLevel.Info);
                        }
                        else
                        {
                            item.ServiceStatus = "Pending Stopped";
                            MessageBox.ShowText(string.Format("{0} - {1} {2}.", FindResource("MessageID6"), serviceName, FindResource("MessageID7")), BMC_Icon.Error);
                            LogManager.WriteLog(string.Format("{0} - {1} {2}.", FindResource("MessageID6"), serviceName, FindResource("MessageID7")), LogManager.enumLogLevel.Error);
                        }
                    }
                }

                ProgressBarMain.Value = 100;
                btnSelectAll.Content = "Select All";

                if (!bChecked)
                {
                    LogManager.WriteLog("No service(s) selected", LogManager.enumLogLevel.Info);

                    MessageBox.ShowBox("MessageID8", BMC_Icon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.ShowText(string.Format("{0} - {1} {2}.", FindResource("MessageID6"), serviceName, FindResource("MessageID7")), BMC_Icon.Error);
                LogManager.WriteLog("btnStopService_Click" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
            }
            finally
            {
                ProgressBarMain.Value = 0;
                Cursor = System.Windows.Input.Cursors.Arrow;
                EnableButtons(true);
            }
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

        private void txtLocCode_LostFocus(object sender, RoutedEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            if (regex.IsMatch(txtLocCode.Text))
            {
                MessageBox.ShowBox("MessageID46", BMC_Icon.Error);
                txtLocCode.Text = "1012";
            }
        }

        private void chkEnableDHCP_Checked(object sender, RoutedEventArgs e)
        {
            RefreshDHCPControls();
        }

        private void chkEnableDHCP_Unchecked(object sender, RoutedEventArgs e)
        {
            RefreshDHCPControls();
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

        //private void txtticketPassword_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        //{
        //    if (tbTicketingPasswordRequired.Visibility == Visibility.Visible)
        //    {
        //        tbTicketingPasswordRequired.Visibility = Visibility.Hidden;
        //        tbTicketingPasswordRequired.ToolTip = null;
        //    }
        //}

        //private void txtticketPassword_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        //{
        //    if (txtticketPassword.Password.Trim() == string.Empty)
        //    {
        //        tbTicketingPasswordRequired.Visibility = Visibility.Visible;
        //        ValidatePasswordBox(txtticketPassword);
        //    }
        //}

        //private void txtticketPassword_LostFocus(object sender, RoutedEventArgs e)
        //{
        //    if (txtticketPassword.Password.Trim() == string.Empty)
        //    {
        //        tbTicketingPasswordRequired.Visibility = Visibility.Visible;
        //        ValidatePasswordBox(txtticketPassword);
        //    }
        //}

        private void txtPCPassword_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (tbPCPasswordRequired.Visibility == Visibility.Visible)
            {
                tbPCPasswordRequired.Visibility = Visibility.Hidden;
                tbPCPasswordRequired.ToolTip = null;
            }
        }

        private void txtPCPassword_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (txtPCPassword.Password.Trim() == string.Empty)
            {
                tbPCPasswordRequired.Visibility = Visibility.Visible;
                ValidatePasswordBox(txtPCPassword);
            }
        }

        private void txtPCPassword_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtPCPassword.Password.Trim() == string.Empty)
            {
                tbPCPasswordRequired.Visibility = Visibility.Visible;
                ValidatePasswordBox(txtPCPassword);
            }
        }

        //private void gpTicketSetup_Loaded(object sender, RoutedEventArgs e)
        //{
        //    RefreshTicketControls();
        //}

        private void gpCMPSetup_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshPCControls();
        }

        private void gpWebServiceSetup_Loaded(object sender, RoutedEventArgs e)
        {
            if (txtEnterpriseweburl.Text == string.Empty)
            {
                txtEnterpriseweburl.Text = string.Empty;
            }
            txtEnterpriseweburl.Focus();
        }

        private void gpDHCPSettings_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshDHCPControls();
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
        private void txtMultiCastIP_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!Settings.OnScreenKeyboard)
                return;
            txtMultiCastIP.Text = DisplayKeyboard(txtMultiCastIP.Text, string.Empty);
            txtMultiCastIP.SelectionStart = txtMultiCastIP.Text.Length;
        }

        private void txtIPAddress_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!Settings.OnScreenKeyboard)
                return;
            txtIPAddress.Text = DisplayKeyboard(txtIPAddress.Text, string.Empty);
            txtIPAddress.SelectionStart = txtIPAddress.Text.Length;

        }
        //private void txtDataExchageIPAddress_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        //{
        //    if (!Settings.OnScreenKeyboard)
        //        return;
        //    txtDataExchageIPAddress.Text = DisplayKeyboard(txtDataExchageIPAddress.Text, string.Empty);
        //    txtDataExchageIPAddress.SelectionStart = txtDataExchageIPAddress.Text.Length;
        //}
        private void txtSenderPortNumber_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!Settings.OnScreenKeyboard)
                return;
            txtSenderPortNumber.Text = DisplayNumberPad(txtSenderPortNumber.Text);
            txtSenderPortNumber.SelectionStart = txtSenderPortNumber.Text.Length;
        }
        private void txtTicketPrefix_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!Settings.OnScreenKeyboard)
                return;
            txtTicketPrefix.Text = DisplayNumberPad(txtTicketPrefix.Text);
            txtTicketPrefix.SelectionStart = txtTicketPrefix.Text.Length;
        }

        ////private void txtExternalSiteCode_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        ////{
        ////    if (!Settings.OnScreenKeyboard)
        ////        return;
        ////    txtExternalSiteCode.Text = DisplayNumberPad(txtExternalSiteCode.Text);
        ////    txtExternalSiteCode.SelectionStart = txtExternalSiteCode.Text.Length;
        ////}

        private void txtExternalCasinoCode_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!Settings.OnScreenKeyboard)
                return;

            txtExternalCasinoCode.Text = DisplayKeyboard(txtExternalCasinoCode.Text, string.Empty);
            txtExternalCasinoCode.SelectionStart = txtExternalCasinoCode.Text.Length;
        }
        private void txtInterfaceIP_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!Settings.OnScreenKeyboard)
                return;
            txtInterfaceIP.Text = DisplayKeyboard(txtInterfaceIP.Text, string.Empty);
            txtInterfaceIP.SelectionStart = txtInterfaceIP.Text.Length;
        }

        private void txtExchangePassword_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!Settings.OnScreenKeyboard)
                return;
            txtExchangePassword.Password = DisplayKeyboard(txtExchangePassword.Password, "Pwd");

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

        //private void txtticketserver_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        //{
        //    if (!Settings.OnScreenKeyboard)
        //        return;
        //    txtticketserver.Text = DisplayKeyboard(txtticketserver.Text, string.Empty);
        //    txtticketserver.SelectionStart = txtticketserver.Text.Length;
        //}

        //private void txticketInstance_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        //{
        //    if (!Settings.OnScreenKeyboard)
        //        return;
        //    txticketInstance.Text = DisplayKeyboard(txticketInstance.Text, string.Empty);
        //    txticketInstance.SelectionStart = txticketInstance.Text.Length;
        //}

        //private void txtticketusername_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        //{
        //    if (!Settings.OnScreenKeyboard)
        //        return;
        //    txtticketusername.Text = DisplayKeyboard(txtticketusername.Text, string.Empty);
        //    txtticketusername.SelectionStart = txtticketusername.Text.Length;
        //}

        //private void txtticketPassword_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        //{
        //    if (!Settings.OnScreenKeyboard)
        //        return;
        //    txtticketPassword.Password = DisplayKeyboard(txtticketPassword.Password, "Pwd");

        //    if (txtticketPassword.Password.Trim() == string.Empty)
        //    {
        //        txtticketPassword.Visibility = Visibility.Visible;
        //        ValidatePasswordBox(txtticketPassword);
        //    }
        //    else
        //    {
        //        tbTicketingPasswordRequired.Visibility = Visibility.Hidden;
        //        tbTicketingPasswordRequired.ToolTip = null;
        //    }
        //}

        //private void txtticketTimeout_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        //{
        //    if (!Settings.OnScreenKeyboard)
        //        return;
        //    txtticketTimeout.Text = DisplayNumberPad(txtticketTimeout.Text);
        //    txtticketTimeout.SelectionStart = txtticketTimeout.Text.Length;
        //}

        private void txtLocCode_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!Settings.OnScreenKeyboard)
                return;
            txtLocCode.Text = DisplayNumberPad(txtLocCode);
            txtLocCode.SelectionStart = txtLocCode.Text.Length;
        }

        private void txtPCServer_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!Settings.OnScreenKeyboard)
                return;
            txtPCServer.Text = DisplayKeyboard(txtPCServer.Text, string.Empty);
            txtPCServer.SelectionStart = txtPCServer.Text.Length;
        }

        private void txtPCInstance_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!Settings.OnScreenKeyboard)
                return;
            txtPCInstance.Text = DisplayKeyboard(txtPCInstance.Text, string.Empty);
            txtPCInstance.SelectionStart = txtPCInstance.Text.Length;
        }

        private void txtPCUsername_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!Settings.OnScreenKeyboard)
                return;
            txtPCUsername.Text = DisplayKeyboard(txtPCUsername.Text, string.Empty);
            txtPCUsername.SelectionStart = txtPCUsername.Text.Length;
        }

        private void txtPCPassword_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!Settings.OnScreenKeyboard)
                return;
            txtPCPassword.Password = DisplayKeyboard(txtPCPassword.Password, "Pwd");

            if (txtPCPassword.Password.Trim() == string.Empty)
            {
                txtPCPassword.Visibility = Visibility.Visible;
                ValidatePasswordBox(txtPCPassword);
            }
            else
            {
                tbPCPasswordRequired.Visibility = Visibility.Hidden;
                tbPCPasswordRequired.ToolTip = null;
            }
        }

        private void txtPCtimeout_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!Settings.OnScreenKeyboard)
                return;
            txtPCtimeout.Text = DisplayNumberPad(txtPCtimeout.Text);
            txtPCtimeout.SelectionStart = txtPCtimeout.Text.Length;
        }

        private void txtEnterpriseweburl_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!Settings.OnScreenKeyboard)
                return;
            txtEnterpriseweburl.Text = DisplayKeyboard(txtEnterpriseweburl.Text, string.Empty);
            txtEnterpriseweburl.SelectionStart = txtEnterpriseweburl.Text.Length;
        }

        private void txtCommServiceURL_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!Settings.OnScreenKeyboard)
                return;
            txtCommServiceURL.Text = DisplayKeyboard(txtCommServiceURL.Text, string.Empty);
            txtCommServiceURL.SelectionStart = txtCommServiceURL.Text.Length;
        }

        private void txtCmdWebServiceURL_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!Settings.OnScreenKeyboard)
                return;
            txtCmdWebServiceURL.Text = DisplayKeyboard(txtCmdWebServiceURL.Text, string.Empty);
            txtCmdWebServiceURL.SelectionStart = txtCmdWebServiceURL.Text.Length;
        }



        private void txtCertificateIssuer_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!Settings.OnScreenKeyboard)
                return;
            txtCertificateIssuer.Text = DisplayKeyboard(txtCertificateIssuer.Text, string.Empty);
            txtCertificateIssuer.SelectionStart = txtCertificateIssuer.Text.Length;
        }

        void ObjKeyboardClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (((KeyboardInterface)sender).DialogResult == true)
            {
                _sKeyText = ((KeyboardInterface)sender).KeyString;
            }
        }

        private void chkCertificateRequired_Checked(object sender, RoutedEventArgs e)
        {
            txtCertificateIssuer.IsEnabled = true;
            isCertificateRequired = true;

            if (txtCertificateIssuer.Text == string.Empty)
            {
                txtCertificateIssuer.Text = string.Empty;
            }
        }

        private void chkCertificateRequired_Unchecked(object sender, RoutedEventArgs e)
        {
            txtCertificateIssuer.IsEnabled = false;
            isCertificateRequired = false;

            if (txtCertificateIssuer.Text == string.Empty)
            {
                txtCertificateIssuer.Text = string.Empty;
            }
        }

        private void gpPTGatewaySetup_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshPTGatewayControls();
        }

        private void txtPTGatewayServerIP_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!Settings.OnScreenKeyboard)
                return;
            txtPTGatewayServerIP.Text = DisplayKeyboard(txtPTGatewayServerIP.Text, string.Empty);
            txtPTGatewayServerIP.SelectionStart = txtPTGatewayServerIP.Text.Length;
        }

        private void txtPTGatewayServerPort_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!Settings.OnScreenKeyboard)
                return;
            txtPTGatewayServerPort.Text = DisplayNumberPad(txtPTGatewayServerPort.Text);
            txtPTGatewayServerPort.SelectionStart = txtPTGatewayServerPort.Text.Length;
        }

        private void txtPTGatewayTimeOut_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!Settings.OnScreenKeyboard)
                return;
            txtPTGatewayTimeOut.Text = DisplayNumberPad(txtPTGatewayTimeOut.Text);
            txtPTGatewayTimeOut.SelectionStart = txtPTGatewayTimeOut.Text.Length;
        }

        private void txtCMPKioskURL_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!Settings.OnScreenKeyboard)
                return;
            txtCMPKioskURL.Text = DisplayKeyboard(txtCMPKioskURL.Text, string.Empty);
            txtCMPKioskURL.SelectionStart = txtCMPKioskURL.Text.Length;
        }

        private void cmbSlotLan_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbSlotLan.SelectedItem.ToString() != "--Select--")
            {
                txtInterfaceIP.Text = cmbSlotLan.SelectedItem.ToString();
            }
        }

        void editTextBox_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!Settings.OnScreenKeyboard)
                return;
            cmbLanIP.Text = DisplayKeyboard(cmbLanIP.Text, string.Empty);
        }

        private void txtSendCAPortNo_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!Settings.OnScreenKeyboard)
                return;
            txtSendCAPortNo.Text = DisplayNumberPad(txtSendCAPortNo.Text);
            txtSendCAPortNo.SelectionStart = txtSendCAPortNo.Text.Length;
        }

        private void txtReceiveCAPortNo_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!Settings.OnScreenKeyboard)
                return;
            txtReceiveCAPortNo.Text = DisplayKeyboard(txtReceiveCAPortNo.Text, string.Empty);
            txtReceiveCAPortNo.SelectionStart = txtReceiveCAPortNo.Text.Length;
        }


        private void txtSendPTPortNo_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!Settings.OnScreenKeyboard)
                return;
            txtSendPTPortNo.Text = DisplayKeyboard(txtSendPTPortNo.Text, string.Empty);
            txtSendPTPortNo.SelectionStart = txtSendPTPortNo.Text.Length;
        }

        private void txtReceiverPortNumber_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!Settings.OnScreenKeyboard)
                return;
            txtReceiverPortNumber.Text = DisplayNumberPad(txtReceiverPortNumber.Text);
            txtReceiverPortNumber.SelectionStart = txtReceiverPortNumber.Text.Length;
        }


        private void txtReceivePTPortNo_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!Settings.OnScreenKeyboard)
                return;
            txtReceivePTPortNo.Text = DisplayNumberPad(txtReceivePTPortNo.Text);
            txtReceivePTPortNo.SelectionStart = txtReceivePTPortNo.Text.Length;
        }

        private void txtCMPWebURL_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!Settings.OnScreenKeyboard)
                return;
            txtCMPWebURL.Text = DisplayKeyboard(txtCMPWebURL.Text, string.Empty);
            txtCMPWebURL.SelectionStart = txtCMPWebURL.Text.Length;
        }

        #region +S001 Start
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
            txtCDServerPort.Text = DisplayNumberPad(txtCDServerPort.Text);
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
            strCDType = DBSettings.GetSettingValue(ReturnConnectionString, "CashDispenserType");

            if (strIsCDEnabled.ToUpper() == "TRUE" && strCDType.ToUpper() == ConfigurationManager.AppSettings.Get("CashDispenserType"))
            {
                tiCash_Dispenser.Visibility = Visibility.Visible;
            }
            else
            {
                tiCash_Dispenser.Visibility = Visibility.Collapsed;
            }
        }
        private void tiTISCommunication_Loaded(object sender, RoutedEventArgs e)
        {
            //Check TISEnabled setting
            sIsTISEnabled = DBSettings.GetSettingValue(ReturnConnectionString, "IsTISEnabled");
            if (sIsTISEnabled.ToUpper() == "TRUE")
            {
                tiTISCommunication.Visibility = Visibility.Visible;

                int refExtSiteCode = 0;
                string refExtCasinoCode = string.Empty;
                string sTISCommunicationMode = DBSettings.GetSettingValue(strConnection, "TISConnectionMode");
                string sTISIPAddress = DBSettings.GetSettingValue(strConnection, "TISIPAddress");
                string sTISPortNumber = DBSettings.GetSettingValue(strConnection, "TISPortNumber");
                string sTISWebServiceURL = DBSettings.GetSettingValue(strConnection, "TISWebServiceURL");
                string sTISTicketPrefix = DBSettings.GetSettingValue(strConnection, "TISTicketPrefix");
                string sTISDataExchangeCommMode = DBSettings.GetSettingValue(strConnection, "TISDataExchangeConnectionMode");
                string sTISDataExchangeIPAddress = DBSettings.GetSettingValue(strConnection, "TISDataExchangeIPAddress");
                string sTISDataExchangePortNumber = DBSettings.GetSettingValue(strConnection, "TISDataExchangePortNumber");
                string sTISExtWebServiceURL = DBSettings.GetSettingValue(strConnection, "TISCommandWebServiceURL");
                // int TISExternalSiteCode =  DBSettings.GetExternalSiteCode(strConnection, refExtSiteCode);
                string TISExternalCasinoCode = DBSettings.GetSettingValue(strConnection, "TISExternalCasinoCode");
                sTISCommunicationModeSetting = DBSettings.GetSettingValue(ExchangeConfigRegistryEntities.ExchangeConnectionString, "TISCommunicationMode");
                if (string.IsNullOrEmpty(sTISCommunicationModeSetting)) sTISCommunicationModeSetting = "BOTH";

                if (sTISCommunicationMode.ToUpper() == "SOCKET")
                {
                    chkSocket.IsChecked = true;
                    //txtIPAddress.IsEnabled = true;
                    //txtReceiverPortNumber.IsEnabled = true;
                    //txtCommServiceURL.IsEnabled = false;
                }
                else if (sTISCommunicationMode.ToUpper() == "WEBSERVICE")
                {
                    chkWebService.IsChecked = true;
                    //txtIPAddress.IsEnabled = false;
                    //txtReceiverPortNumber.IsEnabled = false;
                    //txtCommServiceURL.IsEnabled = true;
                }

                if (sTISCommunicationModeSetting.Trim().ToUpper().Equals("WEBSERVICE"))
                {
                    chkSocket.IsChecked = false;
                    chkWebService.IsChecked = true;
                    chkSocket.Visibility = Visibility.Collapsed;
                    chkWebService.Visibility = Visibility.Collapsed;
                    lblCommunicationMode.Visibility = Visibility.Collapsed;
                    lblIPAddress.Visibility = Visibility.Collapsed;
                    txtIPAddress.Visibility = Visibility.Collapsed;
                    lblReceiverPortNumber.Visibility = Visibility.Collapsed;
                    txtReceiverPortNumber.Visibility = Visibility.Collapsed;
                    lblSenderPortNumber.Visibility = Visibility.Collapsed;
                    txtSenderPortNumber.Visibility = Visibility.Collapsed;
                    TISCommunicationMode.Header = "Web Service Configuration";
                    TISSubGridBox.RowDefinitions[0].Height = new GridLength(0);
                    TISSubGridBox.RowDefinitions[2].Height = new GridLength(0);
                    TISSubGridBox.RowDefinitions[3].Height = new GridLength(0);
                    TISSubGridBox.RowDefinitions[4].Height = new GridLength(0);
                }
                else
                {
                    TISSubGridBox.RowDefinitions[0].MinHeight = 45.0;
                    TISSubGridBox.RowDefinitions[2].MinHeight = 45.0;
                    TISSubGridBox.RowDefinitions[3].MinHeight = 45.0;
                }


                //if (sTISDataExchangeCommMode.ToUpper() == "SOCKET")
                //{
                //    chkDataExchageSocket.IsChecked = true;
                //    //txtDataExchageIPAddress.IsEnabled = true;
                //    //txtSenderPortNumber.IsEnabled = true;

                //}
                ////else if (sTISDataExchangeCommMode.ToUpper() == "WEBSERVICE")
                ////{
                ////    chkDataExchangeWebService.IsChecked = true;
                ////    txtDataExchageIPAddress.IsEnabled = false;
                ////    txtSenderPortNumber.IsEnabled = false;

                ////}

                ////if (chkSocket.IsChecked == true)
                ////{
                ////    txtIPAddress.IsEnabled = true;
                ////    txtReceiverPortNumber.IsEnabled = true;
                ////    txtCommServiceURL.IsEnabled = false;

                ////}

                txtIPAddress.Text = sTISIPAddress;
                txtReceiverPortNumber.Text = sTISPortNumber;
                txtCommServiceURL.Text = sTISWebServiceURL;
                txtTicketPrefix.Text = sTISTicketPrefix;
                //txtDataExchageIPAddress.Text = sTISDataExchangeIPAddress;
                txtSenderPortNumber.Text = sTISDataExchangePortNumber;
                txtCmdWebServiceURL.Text = sTISExtWebServiceURL;
                //if (TISExternalSiteCode == 0)
                //{
                //    txtExternalSiteCode.Text = string.Empty;
                //}
                //else
                //{
                //    txtExternalSiteCode.Text = TISExternalSiteCode.ToString();
                //}
                if (string.IsNullOrEmpty(TISExternalCasinoCode))
                {
                    txtExternalCasinoCode.Text = string.Empty;
                }
                else
                {
                    txtExternalCasinoCode.Text = TISExternalCasinoCode.ToString();
                }

            }
            else
            {
                if (tiTISCommunication.Visibility == Visibility.Visible)
                    tiTISCommunication.Visibility = Visibility.Collapsed;
            }
        }

        private void chkWebService_Checked(object sender, RoutedEventArgs e)
        {
            if (chkWebService.IsChecked == true)
            {
                //txtIPAddress.IsEnabled = false;
                //txtReceiverPortNumber.IsEnabled = false;
                //txtCommServiceURL.IsEnabled = true;
            }

        }
        private void chkSocket_Checked(object sender, RoutedEventArgs e)
        {
            if (chkSocket.IsChecked == true)
            {
                if (txtIPAddress != null)
                {
                    //txtIPAddress.IsEnabled = true;
                    //txtReceiverPortNumber.IsEnabled = true;
                    //txtCommServiceURL.IsEnabled = false;
                }
            }

        }

        private void chkDataExchageSocket_Checked(object sender, RoutedEventArgs e)
        {
            //if (chkDataExchageSocket.IsChecked == true)
            //{
            //    if (txtDataExchageIPAddress != null)
            //    {
            //        txtDataExchageIPAddress.IsEnabled = true;
            //        txtSenderPortNumber.IsEnabled = true;

            //    }
            //}
        }
        private void chkDataExchangeWebService_Checked(object sender, RoutedEventArgs e)
        {

            //if (chkDataExchangeWebService.IsChecked == true)
            //{
            //    txtDataExchageIPAddress.IsEnabled = false;
            //    txtSenderPortNumber.IsEnabled = false;

            //}
        }


        private void gpCashDispenserSetup_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshCDControls();
        }

        private void btnSaveCDDetails_Click(object sender, RoutedEventArgs e)
        {
            bool bTestCDConnection = false;
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
                                    bTestCDConnection = true;
                                }
                            }
                        }
                    }
                }

                if (bTestCDConnection == true)
                {
                    strCDSetting = txtCDServer.Text + ";" + txtCDServerPort.Text + ";" + txtCDUsername.Text + ";" + txtCDDevicename.Text;

                    if (chkUseSSL.IsChecked.Value)
                        strCDSetting += ";true";
                    else
                        strCDSetting += ";false";

                    strEncryptCDSetting = RegistryBuilder.EncryptCDSetting(strCDSetting);

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
                            bInsertSetting = RegistrySettings.SetRegistryEntries(dictSetregistryentries, "Software\\Honeyframe\\" + ExchangeConfigRegistryEntities.RegistryKeyValue);
                            System.Windows.Forms.Application.DoEvents();

                            if (bInsertSetting == true)
                                MessageBox.ShowBox("MessageID117", BMC_Icon.Information);
                        }

                        System.Windows.Forms.Application.DoEvents();
                    }
                }
                else
                {
                    MessageBox.ShowBox("MessageID116", BMC_Icon.Information);
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("btnSaveCDDetails_Click" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
                MessageBox.ShowBox("MessageID74", BMC_Icon.Error);
            }
            finally
            {
                Cursor = System.Windows.Input.Cursors.Arrow;
            }
        }
        #endregion +S001 End

        #region +S002 START
        /// <summary>
        /// STM Configuration changes
        /// </summary>        
        private void EventTransmitter_Loaded(object sender, RoutedEventArgs e)
        {
            string strIsTransmitterEnabled = DBSettings.GetSettingValue(strConnection, "IsTransmitterEnabled");
            if (strIsTransmitterEnabled == "0" || strIsTransmitterEnabled == "")
            {
                chkEnableTransmit.IsChecked = false;
            }
            else
            {
                chkEnableTransmit.IsChecked = true;
            }
            txtEventServer.Text = DBSettings.GetSettingValue(strConnection, "STMServerIP");
            /*txtEventServerPort.Text = DBSettings.GetSettingValue(strConnection, "STMServerPort");
            txtEventAllowedMessages.Text = DBSettings.GetSettingValue(strConnection, "STMAllowedMessages");*/
        }



        public void btn_SaveSTMDetails_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string strSTMEnabled;
                strSTMEnabled = (chkEnableTransmit.IsChecked.Value) ? "1" : "0";
                DBSettings.InsertSettings("IsTransmitterEnabled", strSTMEnabled);

                if (strSTMEnabled != "0")
                {
                    if (txtEventServer.Text.Trim() != string.Empty)
                    {
                        DBSettings.InsertSettings("STMServerIP", txtEventServer.Text.Trim());
                    }
                    else
                    {
                        MessageBox.ShowBox("MessageID113", BMC_Icon.Information);
                        return;
                    }
                    /*if (txtEventServerPort.Text.Trim() != string.Empty)
                    {
                        DBSettings.InsertSettings("STMServerPort", txtEventServerPort.Text.Trim());
                    }
                    else
                    {
                        MessageBox.ShowBox("MessageID114", BMC_Icon.Information);
                        return;
                    }
                    if (txtEventAllowedMessages.Text.Trim() != string.Empty)
                    {
                        DBSettings.InsertSettings("STMAllowedMessages", txtEventAllowedMessages.Text.Trim());
                    }
                    else
                    {
                        MessageBox.ShowBox("MessageID115", BMC_Icon.Information);
                        return;
                    }*/
                }
                else
                {
                    DBSettings.InsertSettings("STMServerIP", txtEventServer.Text.Trim());
                    /*DBSettings.InsertSettings("STMServerPort", txtEventServerPort.Text.Trim());
                    DBSettings.InsertSettings("STMAllowedMessages", txtEventAllowedMessages.Text.Trim());*/
                }

                MessageBox.ShowBox("MessageID37", BMC_Icon.Information);

            }
            catch (Exception Ex)
            {
                LogManager.WriteLog(Ex.Message, LogManager.enumLogLevel.Error);
            }
        }

        private void SDTGateway_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                gpSDTGatewaySetup.Visibility = Visibility.Visible;
                PTGateway.IsChecked = false;
                gpPTGatewaySetup.Visibility = Visibility.Collapsed;
                //  gpCMPSetup.IsEnabled = false;
                PTGateway.IsEnabled = true;
                NOGateway.IsChecked = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void PTGateway_Checked(object sender, RoutedEventArgs e)
        {
            gpSDTGatewaySetup.Visibility = Visibility.Collapsed;
            gpPTGatewaySetup.Visibility = Visibility.Visible;
            SDTGateway.IsChecked = false;
            //  gpCMPSetup.IsEnabled = true;
            SDTGateway.IsEnabled = true;
            NOGateway.IsChecked = false;
        }

        private void TabCMP_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                string strConnectionString = "";
                strConnectionString = BMC.Common.Utilities.DatabaseHelper.GetConnectionString();
                DataSet dsInitialSettings = null;
                if (strConnectionString != null)

                    dsInitialSettings = DBSettings.GetInitialSettings(strConnectionString);
                bool IsTISEnabled = Convert.ToBoolean(dsInitialSettings.Tables[0].Rows[0]["IsTISEnabled"].ToString());
                bool PreCommitmentEnabled = Convert.ToBoolean(dsInitialSettings.Tables[0].Rows[0]["PreCommitmentEnabled"].ToString());
                if (IsTISEnabled || PreCommitmentEnabled)
                {
                    gpPTGatewaySetup.Visibility = Visibility.Visible;
                    gpSDTGatewaySetup.Visibility = Visibility.Collapsed;
                    gpCMPSetup.IsEnabled = true;
                }
                else
                {
                    gpSDTGatewaySetup.Visibility = Visibility.Collapsed;
                    gpPTGatewaySetup.Visibility = Visibility.Collapsed;
                    gpCMPSetup.IsEnabled = false;
                }

                if (dsInitialSettings.Tables[0].Rows[0]["CMPMode"].ToString().ToUpper() == "SOCKET")
                {
                    chkCMPSocket.IsChecked = true;
                }
                else if (dsInitialSettings.Tables[0].Rows[0]["CMPMode"].ToString().ToUpper() == "WEBSERVICE")
                {
                    chkCMPWebService.IsChecked = true;
                }

                switch (IsPTGatewayEnabled())
                {
                    case CMPInterface.PT:
                        {
                            gpPTGatewaySetup.Visibility = Visibility.Visible;
                            gpSDTGatewaySetup.Visibility = Visibility.Collapsed;
                            PTGateway.IsChecked = true;
                            //  gpCMPSetup.IsEnabled = true;
                            break;
                        }
                    case CMPInterface.SDT:
                        {
                            gpSDTGatewaySetup.Visibility = Visibility.Visible;
                            gpPTGatewaySetup.Visibility = Visibility.Collapsed;
                            SDTGateway.IsChecked = true;
                            //  gpCMPSetup.IsEnabled = false;
                            break;
                        }
                    default:
                        {
                            gpSDTGatewaySetup.Visibility = Visibility.Collapsed;
                            gpPTGatewaySetup.Visibility = Visibility.Collapsed;
                            // gpCMPSetup.IsEnabled = false;
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void PTGateway_UnChecked(object sender, RoutedEventArgs e)
        {
            //SDTGateway.IsEnabled = false;
            //SDTGateway.Foreground = (SolidColorBrush) SystemColors.ControlTextBrushKey;
        }

        private void SDTGateway_UnChecked(object sender, RoutedEventArgs e)
        {
            //PTGateway.IsEnabled = false;
            //PTGateway.Foreground = (SolidColorBrush) SystemColors.ControlTextBrushKey;
        }

        private void NOGateway_Checked(object sender, RoutedEventArgs e)
        {
            SDTGateway.IsChecked = false;
            PTGateway.IsChecked = false;
            // gpCMPSetup.IsEnabled = false;
            gpPTGatewaySetup.Visibility = Visibility.Collapsed;
            gpSDTGatewaySetup.Visibility = Visibility.Collapsed;

        }

        private void txtEventServer_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!Settings.OnScreenKeyboard)
                return;
            txtEventServer.Text = DisplayKeyboard(txtEventServer.Text, string.Empty);
            txtEventServer.SelectionStart = txtEventServer.Text.Length;
        }

        /*private void txtEventServerPort_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!Settings.OnScreenKeyboard)
                return;
            txtEventServerPort.Text = DisplayKeyboard(txtEventServerPort.Text, string.Empty);
            txtEventServerPort.SelectionStart = txtEventServerPort.Text.Length;
        }

        private void txtEventAllowedMessages_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!Settings.OnScreenKeyboard)
                return;
            txtEventAllowedMessages.Text = DisplayKeyboard(txtEventAllowedMessages.Text, string.Empty);
            txtEventAllowedMessages.SelectionStart = txtEventAllowedMessages.Text.Length;
        }*/

        #endregion +S002 END

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
        /// Validate the passwordbox details.
        /// </summary>
        /// <returns>success or failure</returns>
        /// Method Revision History
        private bool ValidateText(System.Windows.Controls.PasswordBox tBox, string Message)
        {
            bool bStatus = true;
            if (tBox.Password.Length == 0)
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
                MessageBox.ShowBox("MessageID74", BMC_Icon.Error);
            }
            return bTestConnection;
        }

        private bool TestConnectionDB(string ConnectionString)
        {
            SqlConnection objSQLConn = null;
            bool bResult = false;
            SqlConnection.ClearAllPools();

            try
            {
                if (String.IsNullOrEmpty(ConnectionString) == false)
                {
                    objSQLConn = new SqlConnection(ConnectionString);
                    objSQLConn.Open();
                    bResult = true;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("TestConnectionDB" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
            }
            finally
            {
                if (objSQLConn != null)
                {
                    objSQLConn.Close();
                    objSQLConn.Dispose();
                }
            }
            return bResult;
        }
        /// <summary>
        /// Test the DB Connection with the credentials entered
        /// </summary>
        /// <param name="Server"></param>
        /// <param name="UserName"></param>
        /// <param name="Password"></param>
        /// <param name="DataBase"></param>
        ///<param name="ConnectionTimeout"></param> 
        private bool AddServerDetails(string Server, string UserName, string Password, string DataBase, string ConnectionTimeout)
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
                    bResult = TestConnectionDB(ReturnConnectionString);
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("AddServerDetails" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
                MessageBox.ShowBox("MessageID74", BMC_Icon.Error);
            }
            return bResult;
        }

        /// <summary>
        /// Get all the settings value from registry & database   
        /// <param name=""></param>
        /// <returns></returns>
        /// </summary>
        private void GetSettings()
        {
            string strKeyvalue = string.Empty;
            string strWebUrl = string.Empty;
            string strTicketingLocCode = string.Empty;
            string certificateIssuer = string.Empty;

            try
            {

                strConnection = RegistrySettings.ExchangeConnectionString();
                PCConnection = RegistrySettings.PCConnectionString();
                if (!String.IsNullOrEmpty(PCConnection))
                {
                    Dictionary<string, string> PCServerEntries = Credentials.RetrieveServerDetails(PCConnection);
                    GetCMPServerSettings(PCServerEntries);
                }
                if (!String.IsNullOrEmpty(strConnection))
                {
                    try
                    {
                        SqlConnection sqlConnection = new SqlConnection(strConnection);

                        Dictionary<string, string> ServerEntries = Credentials.RetrieveServerDetails(strConnection);
                        //Loading services from DB
                        ConfigManager.SetConfigurationMode(ConfigManager.ConfigurationMode.AppConfig);
                        if (ConfigManager.Read("ServicesListFromDB") != null)
                        {
                            if (ConfigManager.Read("ServicesListFromDB").ToUpper() == "TRUE")
                            {
                                strListarray = null;
                                strListarray = DBSettings.GetSettingValue(strConnection, "ServiceNames").Split(',');
                                LoadServicesToListView(strListarray);
                            }
                        }
                        GetExchangeServerSettings(ServerEntries);
                        //Commented CMP Settings
                        //  string strCMPConnectionString = DBSettings.CMPConnectionString(strConnection);
                        //    Dictionary<string, string> CMPServerEntries = Credentials.RetrieveServerDetails(strCMPConnectionString);


                        try
                        {
                            string strTicketingConnectionString = RegistrySettings.TicketConnectionString();
                            //Dictionary<string, string> TicketingServerEntries = Credentials.RetrieveServerDetails(strTicketingConnectionString);
                            // GetTicketingServerSettings(TicketingServerEntries);
                        }
                        catch (Exception ex)
                        {
                            ExceptionManager.Publish(ex);
                        }

                        strTicketingLocCode = DBSettings.TicketingLocCodeString(strConnection);
                        txtLocCode.Text = strTicketingLocCode;

                        strClient = DBSettings.ClientNameString(strConnection);

                        isCertificateRequired = DBSettings.GetCertificateSettings("IsCertificateRequired", strConnection) == "True" ? true : false;
                        chkCertificateRequired.IsChecked = isCertificateRequired;
                        certificateIssuer = DBSettings.GetCertificateSettings("CertificateIssuer", strConnection);

                        if (certificateIssuer != string.Empty)
                        {
                            txtCertificateIssuer.Text = certificateIssuer;
                        }
                        txtCertificateIssuer.IsEnabled = isCertificateRequired;
                        // Loading PT Gateway Settings Starts

                        string ptGatewayServerIP = DBSettings.GetSettingValue(strConnection, UIConstants.PTGatewayIP);
                        txtPTGatewayServerIP.Text = ptGatewayServerIP;
                        txtGatewayServerIP.Text = ptGatewayServerIP;

                        string ptGatewayServerPortNo = DBSettings.GetSettingValue(strConnection, UIConstants.PTGatewayPortNo);
                        txtPTGatewayServerPort.Text = ptGatewayServerPortNo;

                        string ptGatewayMsgResponseTimeOut = DBSettings.GetSettingValue(strConnection, UIConstants.PTGatewayMsgRspTimeOut);
                        txtPTGatewayTimeOut.Text = ptGatewayMsgResponseTimeOut;

                        string cmpKioskURL = DBSettings.GetSettingValue(strConnection, UIConstants.CMPKioskURL);
                        txtCMPKioskURL.Text = cmpKioskURL;

                        string strSendPTPortNo = DBSettings.GetSettingValue(strConnection, UIConstants.SDTSendPTPortNo);
                        txtSendPTPortNo.Text = strSendPTPortNo;

                        string strSendCAPortNo = DBSettings.GetSettingValue(strConnection, UIConstants.SDTSendCAPortNo);
                        txtSendCAPortNo.Text = strSendCAPortNo;

                        string strReceiveCAPortNo = DBSettings.GetSettingValue(strConnection, UIConstants.SDTReceiveCAPortNo);
                        txtReceiveCAPortNo.Text = strReceiveCAPortNo;

                        string strReceivePTPortNo = DBSettings.GetSettingValue(strConnection, UIConstants.SDTReceivePTPortNo);
                        txtReceivePTPortNo.Text = strReceivePTPortNo;

                        // Loading PT Gateway Settings Ends

                        string disableMachineOnRemoval = DBSettings.GetSettingValue(strConnection, UIConstants.DisableMachineOnRemoval);
                        chkDisableMachineonRemoval.IsChecked = string.IsNullOrEmpty(disableMachineOnRemoval) ? false : Convert.ToBoolean(disableMachineOnRemoval);
                    }
                    catch (ArgumentException aEx)
                    {
                        ExceptionManager.Publish(aEx);
                    }


                    try
                    {
                        strKeyvalue = BMCRegistryHelper.GetRegKeyValue(ExchangeConfigRegistryEntities.RegistryKeyValue + @"\BMCDHCP", "serverip");
                    }
                    catch (Exception ex)
                    {
                        ExceptionManager.Publish(ex);
                    }

                    if (!string.IsNullOrWhiteSpace(strKeyvalue))
                    {
                        if (cmbSlotLan.Items.IndexOf(strKeyvalue) == -1)
                        {
                            cmbSlotLan.Items.Add(strKeyvalue);
                        }
                        cmbSlotLan.SelectedIndex = cmbSlotLan.Items.IndexOf(strKeyvalue);
                    }
                    else
                    {
                        cmbSlotLan.SelectedIndex = 0;
                    }

                    strKeyvalue = string.Empty;
                    try
                    {
                        strKeyvalue = BMCRegistryHelper.GetRegKeyValue(ExchangeConfigRegistryEntities.RegistryKeyValue + @"\Exchange", "enabledhcp");
                    }
                    catch (Exception ex)
                    {
                        ExceptionManager.Publish(ex);
                    }
                    if (!string.IsNullOrEmpty(strKeyvalue))
                    {
                        isDHCPEnabled = Convert.ToBoolean(int.Parse(strKeyvalue));
                        chkEnableDHCP.IsChecked = isDHCPEnabled;
                    }
                    strKeyvalue = string.Empty;
                    try
                    {
                        strKeyvalue = BMCRegistryHelper.GetRegKeyValue(ExchangeConfigRegistryEntities.RegistryKeyValue + @"\Exchange\Events", "encryptenable");
                    }
                    catch (Exception ex)
                    {
                        ExceptionManager.Publish(ex);
                    }

                    if (!string.IsNullOrEmpty(strKeyvalue))
                    {
                        chkEnableEncrypt.IsChecked = Convert.ToBoolean(int.Parse(strKeyvalue));
                    }

                    strKeyvalue = string.Empty;
                    try
                    {
                        strKeyvalue = BMCRegistryHelper.GetRegKeyValue(ExchangeConfigRegistryEntities.RegistryKeyValue + @"\Exchange\Events", "rsaenable");
                    }
                    catch (Exception ex)
                    {
                        ExceptionManager.Publish(ex);
                    }
                    if (!string.IsNullOrEmpty(strKeyvalue))
                    {
                        chkEnableRSAEncrypt.IsChecked = Convert.ToBoolean(int.Parse(strKeyvalue));
                    }

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
                        if (cmbLanIP.Items.IndexOf(strKeyvalue) == -1)
                        {
                            cmbLanIP.Items.Add(strKeyvalue);
                        }
                        cmbLanIP.SelectedIndex = cmbLanIP.Items.IndexOf(strKeyvalue);
                    }
                    else
                    {
                        cmbLanIP.SelectedIndex = 0;
                    }

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
                }

                try
                {
                    strKeyvalue = BMCRegistryHelper.GetRegKeyValue(ExchangeConfigRegistryEntities.RegistryKeyValue, "CMPWebserviceURL");
                    LogManager.WriteLog("CMPWebserviceURL" + strKeyvalue, LogManager.enumLogLevel.Info);
                }
                catch (Exception ex)
                {
                    ExceptionManager.Publish(ex);
                }

                if (string.IsNullOrEmpty(strKeyvalue))
                {
                    txtCMPWebURL.Text = string.Empty;
                }
                else
                {
                    txtCMPWebURL.Text = strKeyvalue;
                    LogManager.WriteLog("Assign CMPWebserviceURL " + strKeyvalue, LogManager.enumLogLevel.Info);
                }

                #region +S001 START

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
            }
            // }
            //}
            catch (Exception ex)
            {
                LogManager.WriteLog("GetSettings" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
                MessageBox.ShowBox("MessageID74", BMC_Icon.Error);
            }
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
                string ExchangeDBNameValue = "Exchange";
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
                            ExchangeDBNameValue = objKeyValue.Value;
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
                MessageBox.ShowBox("MessageID74", BMC_Icon.Error);
            }
        }

        /// <summary>
        /// Display the CMP server details.
        /// <param name="CMPServerEntries"></param>
        /// <returns></returns>
        /// </summary>        
        private void GetCMPServerSettings(Dictionary<string, string> CMPServerEntries)
        {
            try
            {
                if (CMPServerEntries != null)
                {
                    foreach (KeyValuePair<string, string> objKeyValue in CMPServerEntries)
                    {
                        if (objKeyValue.Key.ToUpper() == "SERVER")
                        {
                            txtPCServer.Text = objKeyValue.Value;
                        }
                        else if (objKeyValue.Key.ToUpper() == "UID")
                        {
                            txtPCUsername.Text = objKeyValue.Value;
                        }
                        else if (objKeyValue.Key.ToUpper() == "PASSWORD")
                        {
                            txtPCPassword.Password = objKeyValue.Value;
                        }
                        else if (objKeyValue.Key.ToUpper() == "DATABASE")
                        {
                            lblPCDB.Text = objKeyValue.Value;
                        }
                        else if (objKeyValue.Key.ToUpper() == "TIMEOUT")
                        {
                            txtPCtimeout.Text = objKeyValue.Value;
                        }
                        else if (objKeyValue.Key.ToUpper() == "INSTANCE")
                        {
                            txtPCInstance.Text = objKeyValue.Value;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("GetPCServerSettings" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
                MessageBox.ShowBox("MessageID74", BMC_Icon.Error);
            }
        }

        /// <summary>
        /// Display the Ticketing server details.
        /// <param name="CMPServerEntries"></param>
        /// <returns></returns>
        /// </summary>        
        //private void GetTicketingServerSettings(Dictionary<string, string> TicketingServerSettings)
        //{
        //    try
        //    {
        //        if (TicketingServerSettings != null)
        //        {
        //            foreach (KeyValuePair<string, string> objKeyValue in TicketingServerSettings)
        //            {
        //                if (objKeyValue.Key.ToUpper() == "SERVER")
        //                {
        //                    txtticketserver.Text = objKeyValue.Value;
        //                }
        //                else if (objKeyValue.Key.ToUpper() == "UID")
        //                {
        //                    txtticketusername.Text = objKeyValue.Value;
        //                }
        //                else if (objKeyValue.Key.ToUpper() == "PASSWORD")
        //                {
        //                    txtticketPassword.Password = objKeyValue.Value;
        //                }
        //                else if (objKeyValue.Key.ToUpper() == "DATABASE")
        //                {
        //                    lblticketDBname.Text = objKeyValue.Value;
        //                }
        //                else if (objKeyValue.Key.ToUpper() == "TIMEOUT")
        //                {
        //                    txtticketTimeout.Text = objKeyValue.Value;
        //                }
        //                else if (objKeyValue.Key.ToUpper() == "INSTANCE")
        //                {
        //                    txticketInstance.Text = objKeyValue.Value;
        //                }

        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LogManager.WriteLog("GetTicketingServerSettings" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
        //        ExceptionManager.Publish(ex);
        //        MessageBox.ShowBox("MessageID74", BMC_Icon.Error);
        //    }
        //}

        /// <summary>
        /// Load Services information to lisview
        /// </summary>
        /// <param name="strListarray"></param>
        private void LoadServicesToListView(string[] strListarray)
        {
            BMCMonitoring objBMCMonitoring = new BMCMonitoring();
            DataTable dtServicesStatus = new DataTable();
            StringBuilder strServicelist = new StringBuilder();

            for (int i = 0; i < strListarray.Length; i++)
            {
                strServicelist.Append(strListarray[i] + ",");
            }

            dtServicesStatus = objBMCMonitoring.GetServiceStatus(strServicelist.ToString(), BMCMonitoring.ServiceTypes.All);

            if (_LoadListViewCollection.Count > 0) { _LoadListViewCollection.Clear(); }

            foreach (DataRow dataRow in dtServicesStatus.Rows)
            {
                _LoadListViewCollection.Add(new LoadListView { Check = 0, ServiceName = dataRow[0].ToString(), ServiceStatus = dataRow[1].ToString() });
            }

            lvServiceslist.ItemsSource = LoadListViewCollection;
        }

        /// <summary>
        /// To refresh the list view with the current status of the services
        /// </summary>   
        private void GetServiceStatusToListView()
        {
            if (!String.IsNullOrEmpty(strConnection))
            {
                Dictionary<string, string> ServerEntries = Credentials.RetrieveServerDetails(strConnection);
                //Loading services from DB
                ConfigManager.SetConfigurationMode(ConfigManager.ConfigurationMode.AppConfig);
                if (ConfigManager.Read("ServicesListFromDB") != null)
                {
                    if (ConfigManager.Read("ServicesListFromDB").ToUpper() == "TRUE")
                    {
                        strListarray = null;
                        strListarray = DBSettings.GetSettingValue(strConnection, "ServiceNames").Split(',');
                        LoadServicesToListView(strListarray);
                    }
                }
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
                txtExchangeServer.Focus();
                txtExchangeServer.SelectionStart = txtExchangeServer.Text.Length;
                txtExchangeUsername.Text = string.Empty;
                ValidatePasswordBox(txtExchangePassword);
                txtExchangeTimeout.Text = "30";
                //txtticketserver.Text = string.Empty;
                //tiTicketingConnection.IsEnabled = false;
                tiCMPConnection.IsEnabled = false;
                tiServicesSetup.IsEnabled = false;
                TabEventTransmitter.IsEnabled = false;
                tiSummary.IsEnabled = false;
                gpLocalBindIP.IsEnabled = false;
                gpDHCPSettings.IsEnabled = false;
                //btnSaveSettings.IsEnabled = false;                
                if (strUpgradeVisible.ToUpper() == "FALSE")
                {
                    btnRunUpgradeScript.IsEnabled = false;
                }
                return;
            }
            else
            {
                txtExchangeServer.Focus();
                txtExchangeServer.SelectionStart = txtExchangeServer.Text.Length;
                //tiTicketingConnection.IsEnabled = true;
                tiCMPConnection.IsEnabled = true;
                tiServicesSetup.IsEnabled = true;
                tiSummary.IsEnabled = true;
                gpLocalBindIP.IsEnabled = true;
                gpDHCPSettings.IsEnabled = true;
                btnSaveSettings.IsEnabled = true;
                if (strUpgradeVisible.ToUpper() == "TRUE")
                {
                    btnRunUpgradeScript.IsEnabled = true;
                }
                TabEventTransmitter.IsEnabled = true;
            }
            if (txtExchangeCommandTimeout.Text == string.Empty)
            {
                txtExchangeCommandTimeout.Text = BMCRegistryHelper.GetRegKeyValue(string.Empty, UIConstants.SQLCommandTimeOut, "60");
                LogManager.WriteLog("Default Exchange Command TimeOut", LogManager.enumLogLevel.Info);
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
                        MessageBox.ShowBox("MessageID16", BMC_Icon.Information);
                        btnSaveSettings.IsEnabled = true;
                    }
                    else
                        MessageBox.ShowBox("MessageID17", BMC_Icon.Error);

                }
                else
                {
                    iReceiveValue = -1;
                    MessageBox.ShowBox("MessageID17", BMC_Icon.Error);
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
                MessageBox.ShowBox("MessageID14", BMC_Icon.Information);
            }
            else
            {
                //if (!sUrl.Contains(".asmx"))
                if (!sUrl.Contains("EnterpriseWebservice"))
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
                        MessageBox.ShowBox("MessageID15", BMC_Icon.Error);
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
                            MessageBox.ShowBox("MessageID15", BMC_Icon.Error);
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
                        if (ValidatePasswordBox(txtExchangePassword))
                        {
                            if (ValidateText(txtExchangeTimeout, "Connection Timeout"))
                            {
                                bTestExchangeConnection = TestConnection(txtExchangeServer.Text, txtExchangeUsername.Text, txtExchangePassword.Password, txtExchangeTimeout.Text, txtExchangeInstance.Text, 'E');
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
            }
        }

        /// <summary>
        /// To start the selected service  
        /// <param name="strServicename">string</param>
        /// <returns name="service status">bool</returns>
        /// </summary>              
        private bool StartService(string strServicename)
        {
            try
            {
                LogManager.WriteLog("Inside Method", LogManager.enumLogLevel.Info);
                BMCMonitoring objBMCMonitoring = new BMCMonitoring();
                return objBMCMonitoring.StartService(strServicename, new TimeSpan(0, serviceTimeOut, 0));
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("StartService" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
                MessageBox.ShowBox("MessageID74", BMC_Icon.Error);
                return false;
            }
        }

        /// <summary>
        /// To End the selected service 
        /// <param name="strServicename">string</param>
        /// <returns name="service status">bool</returns>
        /// </summary>     
        private bool EndService(string strServicename)
        {
            try
            {
                BMCMonitoring objBMCMonitoring = new BMCMonitoring();
                return objBMCMonitoring.EndService(strServicename, new TimeSpan(0, serviceTimeOut, 0));
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("EndService" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
                MessageBox.ShowBox("MessageID74", BMC_Icon.Error);
                return false;
            }
        }

        /// <summary>
        /// To get all the service settings when the form loaded       
        /// <param name=""></param>
        /// <returns></returns>
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
                        else if (objKeys.Key.ToString() == "UpgradeScript")
                        {
                            strScriptPath = objKeys.Value.ToString();
                        }
                        else if (objKeys.Key.ToString() == "VisibleUpgradeScript")
                        {
                            strUpgradeVisible = objKeys.Value.ToString();
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
                    MessageBox.ShowBox("Config file not found.", BMC_Icon.Error);
                }
            }
            catch (ConfigurationException confiex)
            {
                LogManager.WriteLog("GetInitialSettings" + confiex.Message.ToString() + confiex.Source.ToString(), LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(confiex);
                MessageBox.ShowBox("MessageID74", BMC_Icon.Error);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("GetInitialSettings" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
                MessageBox.ShowBox("MessageID74", BMC_Icon.Error);
            }
        }

        /// <summary>
        /// Get all the binding IP's         
        /// <param name=""></param>
        /// <returns></returns>
        /// </summary>
        private void GetBindingIPS()
        {
            string[] strIPAddressList = null;

            try
            {
                strIPAddressList = ReadServicesSettings.GetAllLocalIP();
                if (strIPAddressList.Length > 0)
                {
                    cmbLanIP.Items.Add("--Select--");
                    cmbSlotLan.Items.Add("--Select--");

                    if (strIPAddressList.Length == 1)
                    {
                        cmbLanIP.Items.Add(strIPAddressList[0].ToString());
                        if (cmbLanIP.Items.Count > 1)
                        {
                            cmbLanIP.SelectedItem = strIPAddressList[0].ToString();
                        }
                        else
                        {
                            cmbLanIP.SelectedIndex = 0;
                        }


                        cmbSlotLan.Items.Add(strIPAddressList[0].ToString());
                        if (cmbSlotLan.Items.Count > 1)
                        {
                            cmbSlotLan.SelectedItem = strIPAddressList[0].ToString();
                        }
                        else
                        {
                            cmbSlotLan.SelectedIndex = 0;
                        }
                    }
                    else if (strIPAddressList.Length > 1)
                    {
                        for (int i = 0; i < strIPAddressList.Length; i++)
                        {
                            if (strIPAddressList[i] != null && strIPAddressList[i] != string.Empty)
                            {
                                //if (ValidateIP(strIPAddressList[i].ToString()) == true)
                                //{
                                cmbLanIP.Items.Add(strIPAddressList[i].ToString());
                                //}

                                if (ValidateIP(strIPAddressList[i].ToString()))
                                {
                                    cmbSlotLan.Items.Add(strIPAddressList[i].ToString());
                                }
                                //else
                                //{
                                //    MessageBox.ShowBox("MessageID74", BMC_Icon.Error);
                                //}
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("GetBindingIPS" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
                MessageBox.ShowBox("MessageID74", BMC_Icon.Error);
            }
        }

        /// <summary>
        /// Validations done for the IP address
        /// </summary>
        /// <param name="strCheckIP"></param>        
        public bool ValidateIP(string strCheckIP)
        {
            //string[] strIParray;
            bool bReturn = false;
            //int iCheckValue = 0;

            try
            {
                System.Net.IPAddress ipaddr = null;
                bReturn = System.Net.IPAddress.TryParse(strCheckIP, out ipaddr);
                if (bReturn)
                {
                    LogManager.WriteLog("ValidateIP Returns" + bReturn + "Type " + ipaddr.AddressFamily.ToString(), LogManager.enumLogLevel.Error);
                }

            }
            catch (IndexOutOfRangeException iex)
            {
                LogManager.WriteLog("ValidateIP" + iex.Message.ToString() + iex.Source.ToString(), LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(iex);
                MessageBox.ShowBox("MessageID74", BMC_Icon.Error);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("ValidateIP" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
                MessageBox.ShowBox("MessageID74", BMC_Icon.Error);
            }
            return bReturn;
        }

        /// <summary>
        /// Get DHCP Settings
        /// </summary>
        private void GetDHCPSettings()
        {
            string strKeyvalue = string.Empty;
            try
            {
                if (!String.IsNullOrEmpty(strConnection))
                {
                    strKeyvalue = BMCRegistryHelper.GetRegKeyValue("CashMaster\\Exchange", "multicastip");
                    if (!string.IsNullOrWhiteSpace(strKeyvalue))
                    {
                        txtMultiCastIP.Text = strKeyvalue;
                    }
                    else
                    {
                        txtMultiCastIP.Text = "239.192.1.1";
                        BMCRegistryHelper.SetRegKeyValue("CashMaster\\Exchange", "multicastip", RegistryValueKind.String, txtMultiCastIP.Text);
                        System.Windows.Forms.Application.DoEvents();
                    }
                    strKeyvalue = string.Empty;
                    strKeyvalue = BMCRegistryHelper.GetRegKeyValue("CashMaster\\Exchange", "interface");
                    if (!string.IsNullOrWhiteSpace(strKeyvalue))
                    {
                        txtInterfaceIP.Text = strKeyvalue;
                    }
                }
            }
            catch (Exception ex)
            {
                if (string.IsNullOrEmpty(txtMultiCastIP.Text))
                {
                    txtMultiCastIP.Text = "239.192.1.1";
                }
                LogManager.WriteLog("GetDHCPSettings" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
                MessageBox.ShowBox("MessageID74", BMC_Icon.Error);
            }
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
                MessageBox.ShowBox("MessageID159", BMC_Icon.Error);//Error while loading logs file path.
            }
        }

        /// <summary>
        /// Enable or Disable control based on the registry entry for the first time   
        /// <param name=""></param>
        /// <returns></returns>
        /// </summary>
        private void RefreshDHCPControls()
        {
            if (chkEnableDHCP.IsChecked == true)
            {
                txtMultiCastIP.IsEnabled = false;
                txtInterfaceIP.IsEnabled = false;
                isDHCPEnabled = true;
            }
            else
            {
                txtMultiCastIP.IsEnabled = true;
                txtInterfaceIP.IsEnabled = true;
                isDHCPEnabled = false;
            }
            if (txtMultiCastIP.Text == string.Empty)
            {
                txtMultiCastIP.Text = string.Empty;
            }
            else
            {
                txtMultiCastIP.Text = txtMultiCastIP.Text;
            }
            if (txtInterfaceIP.Text == string.Empty)
            {
                txtInterfaceIP.Text = string.Empty;
            }
            else
            {
                txtInterfaceIP.Text = txtInterfaceIP.Text;
            }
        }

        //private void RefreshTicketControls()
        //{
        //    if ((txtticketserver.Text == string.Empty) || (txtticketusername.Text == string.Empty) || (txtticketPassword.Password == string.Empty))
        //    {
        //        if (txtticketserver.Text == string.Empty) { txtticketserver.Text = string.Empty; }
        //        if (txtticketusername.Text == string.Empty) { txtticketusername.Text = string.Empty; }
        //        ValidatePasswordBox(txtticketPassword);
        //        txtticketTimeout.Text = "30";
        //        txtLocCode.Text = "1001";
        //    }
        //    else
        //    {
        //        if (tbTicketingPasswordRequired.Visibility == Visibility.Visible)
        //        {
        //            tbTicketingPasswordRequired.Visibility = Visibility.Hidden;
        //            tbTicketingPasswordRequired.ToolTip = null;
        //        }
        //    }
        //    txtticketserver.Focus();
        //}

        private void RefreshPCControls()
        {
            if ((txtPCServer.Text == string.Empty) || (txtPCUsername.Text == string.Empty) || (txtPCPassword.Password == string.Empty))
            {
                txtPCServer.Text = string.Empty;
                txtPCUsername.Text = string.Empty;
                ValidatePasswordBox(txtPCPassword);
                txtPCtimeout.Text = "30";
            }
            else
            {
                if (tbPCPasswordRequired.Visibility == Visibility.Visible)
                {
                    tbPCPasswordRequired.Visibility = Visibility.Hidden;
                    tbPCPasswordRequired.ToolTip = null;
                }
            }
            txtPCServer.Focus();
        }

        private void RefreshPTGatewayControls()
        {
            if (txtPTGatewayServerIP.Text == string.Empty) { txtPTGatewayServerIP.Text = string.Empty; }
            if (txtPTGatewayServerPort.Text == string.Empty) { txtPTGatewayServerPort.Text = string.Empty; }
            if (txtPTGatewayTimeOut.Text == string.Empty) { txtPTGatewayTimeOut.Text = string.Empty; }
            if (txtCMPKioskURL.Text == string.Empty) { txtCMPKioskURL.Text = string.Empty; }

            txtPTGatewayServerIP.Focus();
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
                    //case "txtticketPassword":
                    //    tbTicketingPasswordRequired.Visibility = Visibility.Visible;
                    //    break;
                    case "txtPCPassword":
                        tbPCPasswordRequired.Visibility = Visibility.Visible;
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
            objKeyboard.Owner = this;
            objKeyboard.ShowDialog();
            return _sKeyText;
        }

        private string DisplayNumberPad(string keytext)
        {
            string strNumberPadText = string.Empty;
            NumberPadWind ObjNumberpadWind = new NumberPadWind();

            try
            {

                ObjNumberpadWind.ValueText = keytext;
                ObjNumberpadWind.Owner = this;

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

        private void SetRegistryValueForDependOnServiceKey()
        {
            RegistryKey RegKey = null;

            try
            {
                LogManager.WriteLog("Inside SetRegistryValueForDependOnServiceKey method", LogManager.enumLogLevel.Info);

                string dependOnServiceKey = ConfigurationManager.AppSettings.Get("DependOnServiceKey");
                string dependOnServiceNameForSQLInstance = ConfigurationManager.AppSettings.Get("DependOnServiceNameForSQLInstance");
                string dependOnServiceValue = txtExchangeInstance.Text.Trim() == string.Empty ? "SQLServerAgent"
                                        : string.Format("{0}{1}{2}", dependOnServiceNameForSQLInstance, "$", txtExchangeInstance.Text);

                string[] services = DBSettings.GetSettingValue(strConnection, "ServiceNames").Split(',');

                string registryPathControlSet001 = ConfigurationManager.AppSettings.Get("RegistryPathControlSet001");

                foreach (string serviceName in services)
                {
                    try
                    {
                        RegKey = BMCRegistryHelper.GetRegLocalMachine().OpenSubKey(string.Format("{0}\\{1}", registryPathControlSet001, serviceName), true);
                        if (RegKey == null) continue;
                        object RegSubKey = RegKey.GetValue(dependOnServiceKey);
                        if (RegSubKey == null)
                            RegKey.CreateSubKey(dependOnServiceKey);
                        RegKey.SetValue(dependOnServiceKey, dependOnServiceValue);
                        RegSubKey = null;
                    }
                    catch (Exception ex)
                    {
                        ExceptionManager.Publish(ex);
                    }
                    finally
                    {
                        if (RegKey != null) { RegKey.Close(); RegKey = null; }
                    }
                }

                string registryPathControlSet002 = ConfigurationManager.AppSettings.Get("RegistryPathControlSet002");

                foreach (string serviceName in services)
                {
                    try
                    {
                        RegKey = BMCRegistryHelper.GetRegLocalMachine().OpenSubKey(string.Format("{0}\\{1}", registryPathControlSet002, serviceName), true);
                        if (RegKey == null) continue;
                        object RegSubKey = RegKey.GetValue(dependOnServiceKey);
                        if (RegSubKey == null)
                            RegKey.CreateSubKey(dependOnServiceKey);
                        RegKey.SetValue(dependOnServiceKey, dependOnServiceValue);
                        RegSubKey = null;
                    }
                    catch (Exception ex)
                    {
                        ExceptionManager.Publish(ex);
                    }
                    finally
                    {
                        if (RegKey != null) { RegKey.Close(); RegKey = null; }
                    }
                }

                string registryPathControlSet004 = ConfigurationManager.AppSettings.Get("RegistryPathControlSet004");

                foreach (string serviceName in services)
                {
                    try
                    {
                        RegKey = BMCRegistryHelper.GetRegLocalMachine().OpenSubKey(string.Format("{0}\\{1}", registryPathControlSet004, serviceName), true);
                        if (RegKey == null) continue;
                        object RegSubKey = RegKey.GetValue(dependOnServiceKey);
                        if (RegSubKey == null)
                            RegKey.CreateSubKey(dependOnServiceKey);
                        RegKey.SetValue(dependOnServiceKey, dependOnServiceValue);
                        RegSubKey = null;
                    }
                    catch (Exception ex)
                    {
                        ExceptionManager.Publish(ex);
                    }
                    finally
                    {
                        if (RegKey != null) { RegKey.Close(); RegKey = null; }
                    }
                }

                string registryPathCurrentControlSet = ConfigurationManager.AppSettings.Get("RegistryPathCurrentControlSet");

                foreach (string serviceName in services)
                {
                    try
                    {
                        RegKey = BMCRegistryHelper.GetRegLocalMachine().OpenSubKey(string.Format("{0}\\{1}", registryPathCurrentControlSet, serviceName), true);
                        if (RegKey == null) continue;
                        object RegSubKey = RegKey.GetValue(dependOnServiceKey);
                        if (RegSubKey == null)
                            RegKey.CreateSubKey(dependOnServiceKey);
                        RegKey.SetValue(dependOnServiceKey, dependOnServiceValue);
                        RegSubKey = null;
                    }
                    catch (Exception ex)
                    {
                        ExceptionManager.Publish(ex);
                    }
                    finally
                    {
                        if (RegKey != null) { RegKey.Close(); RegKey = null; }
                    }
                }

                LogManager.WriteLog("RegistryValue set successfully for DependOnService Key.", LogManager.enumLogLevel.Info);

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        public void SaveLogPathForBMCWrap()
        {
            LogManager.WriteLog("Inside SaveLogPath", LogManager.enumLogLevel.Info);
            string newPath = txtFileChoose.Text;

            try
            {
                string strFileName = Directory.GetCurrentDirectory() + "\\BMCWrap\\FileList.xml";

                XmlDocument xmldoc = new XmlDocument();
                try
                {
                    LogManager.WriteLog("Loading the file to change the log path", LogManager.enumLogLevel.Info);
                    xmldoc.Load(strFileName);
                }
                catch (Exception ex)
                {
                    LogManager.WriteLog("Failed to update  Log path", LogManager.enumLogLevel.Error);
                    ExceptionManager.Publish(ex);
                }

                XmlNodeList lnode = xmldoc.SelectNodes("fullrecord/Record");

                foreach (XmlNode xnode in lnode)
                {

                    try
                    {
                        if ((xnode.ChildNodes[2].InnerText.ToString() == "LOGS") && (xnode.ChildNodes[0].Name == "FilePath"))
                        {
                            xnode.ChildNodes[0].InnerText = newPath;
                            break;
                        }
                    }
                    catch (Exception ex)
                    {
                        ExceptionManager.Publish(ex);
                        continue;
                    }
                }
                xmldoc.Save(strFileName);
            }



            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

        }

        private bool ValidateCMPURL(string sUrl)
        {
            string sWebExtension = string.Empty;
            Regex objRegexUrlvalidate = new Regex("^(http|ftp|https)://?");
            MatchCollection objMatchCollect;
            string strCMPPort = ConfigurationManager.AppSettings.Get("CMPWebServicePort");
            string cmpURLValidate = string.Empty;
            string cmpProtocol = "http://";
            bool bCMPWebServicePortRequired = Convert.ToBoolean(ConfigurationManager.AppSettings.Get("CMPeWebServicePortRequired"));

            if (sUrl.Trim().Length < 0 || sUrl.Trim().Length == 0)
            {
                MessageBox.ShowBox("MessageID147", BMC_Icon.Information);
                return false;
            }

            if (sUrl.Contains("GatewayService"))
            {
                if (sUrl.Contains(cmpProtocol))
                {
                    cmpURLValidate = sUrl.Trim();
                    objMatchCollect = objRegexUrlvalidate.Matches(cmpURLValidate);
                    if (objMatchCollect.Count > 0)
                        return true;
                    else
                    {
                        MessageBox.ShowBox("MessageID148", BMC_Icon.Error);
                        return false;
                    }
                }
                else
                {
                    MessageBox.ShowBox("MessageID148", BMC_Icon.Error);
                    txtCMPWebURL.Focus();
                    return false;
                }
            }
            else
            {
                sWebExtension = ConfigManager.Read("CMPServiceExtension");
                if (sUrl.Contains(cmpProtocol))
                {
                    cmpURLValidate = sUrl.Trim() + sWebExtension.Trim();
                }
                else
                {
                    if (cmpProtocol.Substring(0, cmpProtocol.Length - 3) == "https")
                    {
                        if (bCMPWebServicePortRequired)
                        {
                            cmpURLValidate = cmpProtocol.Trim() + sUrl.Trim() + ":" + strCMPPort + sWebExtension.Trim();
                        }
                        else
                        {
                            cmpURLValidate = cmpProtocol.Trim() + sUrl.Trim() + sWebExtension.Trim();
                        }
                    }
                    else
                    {
                        cmpURLValidate = cmpProtocol.Trim() + sUrl.Trim() + ":" + strCMPPort + sWebExtension.Trim();
                    }
                }
                txtCMPWebURL.Text = cmpURLValidate;
                return true;
            }
        }

        #endregion Private Methods

        private static CMPInterface IsPTGatewayEnabled()
        {
            string sCard = string.Empty;
            try
            {
                sCard = Convert.ToString(BMCRegistryHelper.GetRegKeyValue("Cashmaster\\Exchange\\EPI", "Card_ID_Store", "")).ToUpper();

                switch (sCard)
                {
                    case "PTINTERFACE":
                        return CMPInterface.PT;
                    case "SDT":
                        return CMPInterface.SDT;
                    default:
                        return CMPInterface.None;
                }
            }
            catch (Exception ex)
            {
                return CMPInterface.None;
            }
        }

        #region GridViewSetup

        private void InitializeGridViewColors()
        {
            try
            {
                LogManager.WriteLog("Inside InitializeGridViewColors", LogManager.enumLogLevel.Info);

                cmbColor.Items.Add(Colors.White);
                cmbColor.Items.Add(Colors.Yellow);
                cmbColor.Items.Add(Colors.Orange);
                cmbColor.Items.Add(Colors.Tomato);
                cmbColor.Items.Add(Colors.Magenta);
                cmbColor.Items.Add(Colors.Red);
                cmbColor.Items.Add(Colors.Brown);
                cmbColor.Items.Add(Colors.MidnightBlue);
                cmbColor.Items.Add(Colors.YellowGreen);
                cmbColor.Items.Add(Colors.SlateGray);
                cmbColor.Items.Add(Colors.SlateBlue);
                cmbColor.Items.Add(Colors.ForestGreen);
                cmbColor.Items.Add(Colors.Maroon);
                cmbColor.Items.Add(Colors.Pink);
                cmbColor.Items.Add(Colors.Olive);
                cmbColor.Items.Add(Colors.DeepSkyBlue);
                cmbColor.Items.Add(Colors.LawnGreen);
                cmbColor.Items.Add(Colors.Purple);
                cmbColor.Items.Add(Colors.MediumOrchid);
                cmbColor.Items.Add(Colors.Cyan);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void BindGridViewTypeCombo()
        {
            try
            {
                LogManager.WriteLog("Inside BindColorRangeListView", LogManager.enumLogLevel.Info);

                DataTable gridViewTypeDetails = DBSettings.GetGridViewTypeDetails(strConnection).Tables[0];

                cmbGridViewType.ItemsSource = ((System.ComponentModel.IListSource)gridViewTypeDetails).GetList();
                cmbGridViewType.DataContext = gridViewTypeDetails.DefaultView;

                cmbGridViewType.DisplayMemberPath = "Description";
                cmbGridViewType.SelectedValuePath = "ID";
                cmbGridViewType.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void BindColorRangeListView()
        {
            try
            {
                LogManager.WriteLog("Inside BindColorRangeListView", LogManager.enumLogLevel.Info);

                gridViewColorRangeDetails = DBSettings.GetGridViewColorRangeDetails(Convert.ToInt16(cmbGridViewType.SelectedValue), strConnection).Tables[0];

                System.Windows.Data.Binding bind = new System.Windows.Data.Binding();
                bind.Source = gridViewColorRangeDetails;

                lstPosDetailsGridView.SetBinding(ListView.ItemsSourceProperty, bind);
                lstPosDetailsGridView.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private bool ValidateStartValue()
        {
            double currentStartValue;
            double currentEndValue;
            double startValue;
            double endValue;

            try
            {
                LogManager.WriteLog("Inside ValidateStartValue", LogManager.enumLogLevel.Info);

                if (string.IsNullOrEmpty(txtStartValue.Text))
                {
                    MessageBox.ShowBox("MessageID122", BMC_Icon.Error);
                    return false;
                }

                Regex numericRegEx = new Regex(@"^\d+(\,\d{3})?(\.\d{1,2})?$");
                if (!numericRegEx.IsMatch(txtStartValue.Text))
                {
                    MessageBox.ShowBox("MessageID153", BMC_Icon.Error);
                    txtStartValue.Focus();
                    return false;
                }

                currentStartValue = Convert.ToDouble(txtStartValue.Text);
                currentEndValue = Convert.ToDouble(txtEndValue.Text);

                if (currentStartValue == currentEndValue)
                {
                    MessageBox.ShowBox("MessageID127", BMC_Icon.Error);
                    return false;
                }

                if (currentStartValue > currentEndValue)
                {
                    MessageBox.ShowBox("MessageID119", BMC_Icon.Error);
                    return false;
                }

                foreach (DataRow dataRow in gridViewColorRangeDetails.Rows)
                {
                    startValue = Convert.ToDouble(dataRow["StartValue"]);
                    endValue = Convert.ToDouble(dataRow["EndValue"]);
                    if ((currentStartValue >= startValue && currentStartValue <= endValue) || (currentEndValue >= startValue && currentEndValue <= endValue)
                        || (startValue >= currentStartValue && startValue <= currentEndValue))
                    {
                        MessageBox.ShowBox("MessageID120", BMC_Icon.Error);
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return true;
        }

        private bool ValidateStartValueforWinLoss()
        {
            double currentStartValue;
            double currentEndValue;
            double startValue;
            double endValue;

            try
            {
                LogManager.WriteLog("Inside ValidateStartValue", LogManager.enumLogLevel.Info);

                if (string.IsNullOrEmpty(txtStartValue.Text))
                {
                    MessageBox.ShowBox("MessageID122", BMC_Icon.Error);
                    return false;
                }

                Regex numericRegEx = new Regex(@"^(\-)?\d+(\,\d{3})?(\.\d{1,2})?$");
                if (!numericRegEx.IsMatch(txtStartValue.Text))
                {
                    MessageBox.ShowBox("MessageID153", BMC_Icon.Error);
                    return false;
                }

                currentStartValue = Convert.ToDouble(txtStartValue.Text);
                currentEndValue = Convert.ToDouble(txtEndValue.Text);

                if (currentStartValue == currentEndValue)
                {
                    MessageBox.ShowBox("MessageID127", BMC_Icon.Error);
                    return false;
                }

                if (currentStartValue > currentEndValue)
                {
                    MessageBox.ShowBox("MessageID119", BMC_Icon.Error);
                    return false;
                }

                foreach (DataRow dataRow in gridViewColorRangeDetails.Rows)
                {
                    startValue = Convert.ToDouble(dataRow["StartValue"]);
                    endValue = Convert.ToDouble(dataRow["EndValue"]);

                    if ((currentStartValue >= startValue && currentStartValue <= endValue) || (currentEndValue >= startValue && currentEndValue <= endValue)
                        || (startValue >= currentStartValue && startValue <= currentEndValue))
                    {
                        MessageBox.ShowBox("MessageID120", BMC_Icon.Error);
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return true;
        }

        private bool ValidateEndValue()
        {
            double currentEndValue;
            double startValue;
            double endValue;

            try
            {
                LogManager.WriteLog("Inside ValidateEndValue", LogManager.enumLogLevel.Info);

                if (string.IsNullOrEmpty(txtEndValue.Text))
                {
                    MessageBox.ShowBox("MessageID123", BMC_Icon.Error);
                    return false;
                }

                Regex numericRegEx = new Regex(@"^\d+(\,\d{3})?(\.\d{1,2})?$");
                if (!numericRegEx.IsMatch(txtEndValue.Text))
                {
                    MessageBox.ShowBox("MessageID154", BMC_Icon.Error);
                    txtEndValue.Focus();
                    return false;
                }

                currentEndValue = Convert.ToDouble(txtEndValue.Text);

                foreach (DataRow dataRow in gridViewColorRangeDetails.Rows)
                {
                    startValue = Convert.ToDouble(dataRow["StartValue"]);
                    endValue = Convert.ToDouble(dataRow["EndValue"]);

                    if (currentEndValue >= startValue && currentEndValue == endValue)
                    {
                        MessageBox.ShowBox("MessageID121", BMC_Icon.Error);
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return true;
        }

        private bool ValidateEndValueForWinLoss()
        {
            double currentEndValue;
            double startValue;
            double endValue;

            try
            {
                LogManager.WriteLog("Inside ValidateEndValue", LogManager.enumLogLevel.Info);

                if (string.IsNullOrEmpty(txtEndValue.Text))
                {
                    MessageBox.ShowBox("MessageID123", BMC_Icon.Error);
                    return false;
                }

                Regex numericRegEx = new Regex(@"^(\-)?\d+(\,\d{3})?(\.\d{1,2})?$");
                if (!numericRegEx.IsMatch(txtEndValue.Text))
                {
                    MessageBox.ShowBox("MessageID154", BMC_Icon.Error);
                    return false;
                }

                currentEndValue = Convert.ToDouble(txtEndValue.Text);

                foreach (DataRow dataRow in gridViewColorRangeDetails.Rows)
                {
                    startValue = Convert.ToDouble(dataRow["StartValue"]);
                    endValue = Convert.ToDouble(dataRow["EndValue"]);

                    if (currentEndValue >= startValue && currentEndValue == endValue)
                    {
                        MessageBox.ShowBox("MessageID121", BMC_Icon.Error);
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return true;
        }

        private void RefreshGridViewControls()
        {
            try
            {
                LogManager.WriteLog("Inside RefreshGridViewControls", LogManager.enumLogLevel.Info);

                txtStartValue.IsEnabled = true;
                txtEndValue.IsEnabled = true;

                txtStartValue.Text = string.Empty;
                txtEndValue.Text = string.Empty;
                cmbColor.SelectedItem = Colors.White;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void cmbGridViewType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BindColorRangeListView();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LogManager.WriteLog("Inside btnSave_Click", LogManager.enumLogLevel.Info);

                bool canContinue = true;

                if (cmbGridViewType.Text == "Win/Loss")
                {
                    if (txtStartValue.IsEnabled == true)
                    {
                        canContinue = ValidateStartValueforWinLoss();
                        if (canContinue)
                            canContinue = ValidateEndValueForWinLoss();
                    }
                }
                else
                {
                    if (txtStartValue.IsEnabled == true)
                    {
                        canContinue = ValidateStartValue();
                        if (canContinue)
                            canContinue = ValidateEndValue();
                    }
                }

                if (canContinue)
                {
                    LogManager.WriteLog("Inside btnSave_Click", LogManager.enumLogLevel.Info);

                    int result = DBSettings.InsertOrUpdateGridViewColorRangeDetails(Convert.ToInt16(cmbGridViewType.SelectedValue), Convert.ToDecimal(txtStartValue.Text),
                        Convert.ToDecimal(txtEndValue.Text), SelectedColor.ToString(), strConnection);

                    if (result != -1)
                        MessageBox.ShowBox("MessageID149", BMC_Icon.Information);
                    else
                        MessageBox.ShowBox("MessageID150", BMC_Icon.Information);

                    BindColorRangeListView();
                }
            }
            catch (Exception ex)
            {
                MessageBox.ShowBox("MessageID150", BMC_Icon.Information);
                ExceptionManager.Publish(ex);
            }
        }



        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LogManager.WriteLog("Inside btnSave_Click", LogManager.enumLogLevel.Info);

                if (lstPosDetailsGridView.SelectedIndex != -1)
                {
                    int result = DBSettings.DeleteGridViewColorRangeDetails(Convert.ToInt16(cmbGridViewType.SelectedValue), Convert.ToDecimal(txtStartValue.Text),
                        Convert.ToDecimal(txtEndValue.Text), strConnection);

                    if (result != -1)
                        MessageBox.ShowBox("MessageID151", BMC_Icon.Information);
                    else
                        MessageBox.ShowBox("MessageID152", BMC_Icon.Information);

                    BindColorRangeListView();
                }
                else
                {
                    MessageBox.ShowBox("MessageID124", BMC_Icon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.ShowBox("MessageID152", BMC_Icon.Information);
                ExceptionManager.Publish(ex);
            }
        }

        private void tiGridViewSetup_Loaded(object sender, RoutedEventArgs e)
        {
            BindColorRangeListView();
        }

        private void lstPosDetailsGridView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                LogManager.WriteLog("Inside lstPosDetailsGridView_SelectionChanged", LogManager.enumLogLevel.Info);

                if (lstPosDetailsGridView.SelectedIndex != -1)
                {
                    DataRowView dataRowView = (DataRowView)lstPosDetailsGridView.SelectedItem;
                    txtStartValue.Text = dataRowView[1].ToString();
                    txtEndValue.Text = dataRowView[2].ToString();
                    cmbColor.SelectedItem = (Color)ColorConverter.ConvertFromString(dataRowView[3].ToString());

                    txtStartValue.IsEnabled = false;
                    txtEndValue.IsEnabled = false;
                }
                else
                {
                    RefreshGridViewControls();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LogManager.WriteLog("Inside btnRefresh_Click", LogManager.enumLogLevel.Info);

                BindGridViewTypeCombo();
                BindColorRangeListView();
                txtStartValue.Text = string.Empty;
                txtEndValue.Text = string.Empty;
                cmbColor.SelectedItem = Colors.White;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void gbGridViewSetup_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                LogManager.WriteLog("Inside gbGridViewSetup_Loaded", LogManager.enumLogLevel.Info);
                BindGridViewTypeCombo();
                txtStartValue.Text = string.Empty;
                txtEndValue.Text = string.Empty;
                cmbColor.SelectedItem = Colors.White;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void txtStartValue_KeyDown(object sender, KeyEventArgs e)
        {
            switch (cmbGridViewType.Text)
            {
                case "Buy-In":
                    if (!AllowedInputs.Contains(e.Key) && e.Key != Key.OemMinus && e.Key != Key.Subtract)
                        e.Handled = true;
                    break;
                case "Win/Loss":
                    if (!AllowedInputs.Contains(e.Key) && e.Key != Key.OemMinus && e.Key != Key.Subtract)
                        e.Handled = true;
                    break;
                case "Time Played":
                    if (!AllowedInputs.Contains(e.Key))
                        e.Handled = true;
                    break;
                default:
                    break;
            }
        }

        private void txtEndValue_KeyDown(object sender, KeyEventArgs e)
        {
            switch (cmbGridViewType.Text)
            {
                case "Buy-In":
                    if (!AllowedInputs.Contains(e.Key) && e.Key != Key.OemMinus && e.Key != Key.Subtract)
                        e.Handled = true;
                    break;
                case "Win/Loss":
                    if (!AllowedInputs.Contains(e.Key) && e.Key != Key.OemMinus && e.Key != Key.Subtract)
                        e.Handled = true;
                    break;
                case "Time Played":
                    if (!AllowedInputs.Contains(e.Key))
                        e.Handled = true;
                    break;
                default:
                    break;
            }
        }

        private void txtStartValue_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!Settings.OnScreenKeyboard)
                return;
            txtStartValue.Text = DisplayKeyboard(txtStartValue.Text, string.Empty);
            txtStartValue.SelectionStart = txtStartValue.Text.Length;
        }

        private void txtEndValue_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!Settings.OnScreenKeyboard)
                return;
            txtEndValue.Text = DisplayKeyboard(txtEndValue.Text, string.Empty);
            txtEndValue.SelectionStart = txtEndValue.Text.Length;
        }

        #endregion GridViewSetup

        private void txtGatewayServerIP_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!Settings.OnScreenKeyboard)
                return;
            txtGatewayServerIP.Text = DisplayKeyboard(txtGatewayServerIP.Text, string.Empty);
            txtGatewayServerIP.SelectionStart = txtGatewayServerIP.Text.Length;

        }

        private static void SetGateWaySetting(string sKey, string sVal)
        {

            try
            {
                BMCRegistryHelper.SetRegKeyValue("Cashmaster\\Exchange\\EPI", sKey, RegistryValueKind.String, sVal);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }


        public Label LoadingLabel { get; set; }
        public delegate void MainScreenUpdateStatus(string Status);
        public MainScreenUpdateStatus _UpdateStatus = null;

        private bool ValidateExchangeServer()
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
                                MessageBox.ShowBox("MessageID162", BMC_Icon.Information);//Exchange command timeout cannot be empty.
                                txtExchangeCommandTimeout.Focus();
                                return false;
                            }
                        }
                        else
                        {
                            MessageBox.ShowBox("MessageID163", BMC_Icon.Information);//Exchange timeout cannot be empty.
                            txtExchangeTimeout.Focus();
                            return false;
                        }
                    }
                    else
                    {
                        MessageBox.ShowBox("MessageID164", BMC_Icon.Information);//Database password cannot be empty.
                        txtExchangePassword.Focus();
                        return false;
                    }
                }
                else
                {
                    MessageBox.ShowBox("MessageID165", BMC_Icon.Information);//Database username cannot be empty.
                    txtExchangeUsername.Focus();
                    return false;
                }
            }
            else
            {
                MessageBox.ShowBox("MessageID166", BMC_Icon.Information);//Database server name cannot be empty.
                txtExchangeServer.Focus();
                return false;
            }
        }

        private void btnGenerateDBRestore_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ValidateExchangeServer())
                {
                    DBUserName = txtExchangeUsername.Text;
                    DBPassword = txtExchangePassword.Password;
                    DBServername = txtExchangeServer.Text;

                    if (txtExchangeInstance.Text.Trim().Length > 0)
                    {
                        DBServername = DBServername + "\\" + txtExchangeInstance.Text.Trim();
                    }

                    if (!TestConnectionDB(GetConnectionString("Master")))
                    {
                        MessageBox.ShowBox("MessageID132", BMC_Icon.Information);
                        ErrorMessage = string.Format("{0} ", FindResource("MessageID132"));
                        return;
                    }
                    Cursor = System.Windows.Input.Cursors.Wait;
                    //ProgressBarGenerateDB.Visibility = Visibility.Visible;

                    //LoadWindow ld = new LoadWindow("");

                    //ld.Topmost = true;
                    LoadingWindow ld = new LoadingWindow();

                    ld.Owner = this;
                    ld.Topmost = true;
                    Label _lblstatus = ld.lblStatus;
                    ld.Show();
                    LayoutRoot.IsEnabled = false;
                    BackgroundWorker bg = new BackgroundWorker();
                    bg.DoWork += new DoWorkEventHandler(bg_DoWork);
                    bg.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bg_RunWorkerCompleted);
                    bg.WorkerReportsProgress = false;
                    bg.WorkerSupportsCancellation = false;
                    bg.RunWorkerAsync(_lblstatus);
                    // CreateDB();
                }

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("btnGenerateDBRestore_Click" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
                MessageBox.ShowBox("MessageID74", BMC_Icon.Error);
            }
            finally
            {
                Cursor = System.Windows.Input.Cursors.Arrow;
            }


        }

        void bg_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            bool IsSucc = (bool)e.Result;
            if (bIsSelectDBCancelled)
            {
                return;
            }
            if (IsSucc)
            {
                SynchronizationContext syn = SynchronizationContext.Current;
                SendOrPostCallback callba = delegate(object IsSucceed)
                {
                    MessageBox.ShowText(FindResource("MessageID134").ToString(), BMC_Icon.Information);
                };
                syn.Post(callba, IsSucc);
            }
            else
            {
                if (isValidError)
                {
                    string updgradeScriptDefaultPath = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName) + Path.DirectorySeparatorChar + ConfigurationManager.AppSettings.Get("DBScriptsDefaultPath");
                    string upgradeScriptFileName = ConfigurationManager.AppSettings.Get("UpgradeScriptFileName");
                    XmlDocument xdoc = new XmlDocument();
                    xdoc.Load(updgradeScriptDefaultPath + Path.DirectorySeparatorChar + upgradeScriptFileName);
                    XmlNodeList xlist = xdoc.SelectNodes("//Databases/name");//Databases

                    foreach (XmlNode XDBName in xlist)
                    {
                        string DBName = XDBName.InnerText;
                        DBBuilder.DropDatabase(GetConnectionString("Master"), DBName);
                    }
                }
                SynchronizationContext syn = SynchronizationContext.Current;
                SendOrPostCallback callba = delegate(object IsSucceed)
                {
                    MessageBox.ShowText(ErrorMessage, isValidError ? BMC_Icon.Error : BMC_Icon.Information);
                };
                syn.Post(callba, IsSucc);
            }

            Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
            {
                if (this.OwnedWindows.Count > 0)
                {
                    LayoutRoot.IsEnabled = true;
                    Window wd = this.OwnedWindows[0];
                    if (wd.Title.Equals("LoadingWindow"))
                    {
                        wd.Close();
                    }
                }
            });
        }

        void bg_DoWork(object sender, DoWorkEventArgs e)
        {

            e.Result = CreateDB((Label)e.Argument);

            Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
            {
                if (this.OwnedWindows.Count > 0)
                {
                    LayoutRoot.IsEnabled = true;
                    Window wd = this.OwnedWindows[0];
                    if (wd.Title.Equals("LoadingWindow"))
                    {
                        wd.Close();
                    }

                }

            });
        }

        private bool CreateDB(Label lblStatus)//ProgessWpfApplication.LoadWindow ldWin
        {
            string sSQLServerDetails = string.Empty;
            bool bDBExists = false;
            bool bExchangeDBExists = false;
            bool bTicketingDBExists = false;
            bool bExtSysMsgDBExists = false;
            ErrorMessage = "";
            isValidError = false;
            bool retVal = true;
            try
            {
                string updgradeScriptDefaultPath = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName) + Path.DirectorySeparatorChar + ConfigurationManager.AppSettings.Get("DBScriptsDefaultPath");
                string upgradeScriptFileName = ConfigurationManager.AppSettings.Get("UpgradeScriptFileName");
                LogManager.WriteLog(string.Format("Loading DBScript Xml {0}", upgradeScriptFileName), LogManager.enumLogLevel.Info);
                XmlDocument xdoc = new XmlDocument();
                xdoc.Load(updgradeScriptDefaultPath + Path.DirectorySeparatorChar + upgradeScriptFileName);
                XmlNodeList xlist = xdoc.SelectNodes("//Databases/name");//Databases
                List<string> IsExistDB = new List<string>();
                List<string> lstDBtoCreate = new List<string>();
                foreach (XmlNode XDBName in xlist)
                {

                    string DBName = XDBName.InnerText;
                    sSQLServerDetails = GetConnectionString(DBName);
                    Dispatcher.BeginInvoke(DispatcherPriority.Background, (SendOrPostCallback)delegate { lblStatus.SetValue(System.Windows.Controls.Label.ContentProperty, "Connecting to Server " + DBServername + " ..."); }, null);
                    if (!TestConnectionDB(GetConnectionString("Master")))
                    {

                        ErrorMessage = string.Format("{0} ", FindResource("MessageID132"));
                        return false;
                    }
                    Dispatcher.BeginInvoke(DispatcherPriority.Background, (SendOrPostCallback)delegate { lblStatus.SetValue(System.Windows.Controls.Label.ContentProperty, "Checking " + DBName + " DB. If Exists!"); }, null);
                    bDBExists = DBSettings.CheckDBExists(sSQLServerDetails, DBName, 0);

                    if (bDBExists)
                    {
                        IsExistDB.Add(DBName);
                        LogManager.WriteLog(string.Format("{0} {1}", DBName, FindResource("MessageID50")), LogManager.enumLogLevel.Info);
                        retVal = false;
                        if (DBName.ToUpper() == "EXCHANGE")
                        {
                            bExchangeDBExists = true;
                        }
                        if (DBName.ToUpper() == "TICKETING")
                        {
                            bTicketingDBExists = true;
                        }
                        if (DBName.ToUpper() == "EXTSYSMSG")
                        {
                            bExtSysMsgDBExists = true;
                        }
                        continue;
                    }
                    else
                    {
                        lstDBtoCreate.Add(DBName);
                    }
                }
                if (lstDBtoCreate.Count > 0)
                {
                    strDataFilePath = string.Empty;
                    strLogFilePath = string.Empty;
                    bIsSelectDBCancelled = false;

                    Dispatcher.Invoke(new Action(() =>
                    {
                        SelectDBFiles _SelectDBFiles = new SelectDBFiles(bExchangeDBExists, bTicketingDBExists, bExtSysMsgDBExists, txtPCServer.Text);
                        _SelectDBFiles.Topmost = true;
                        _SelectDBFiles.ShowDialog();
                        strDataFilePath = _SelectDBFiles.StrDataFilePath;
                        strLogFilePath = _SelectDBFiles.StrLogFilePath;
                        bIsSelectDBCancelled = _SelectDBFiles.bIsCancelled;
                    }));

                    if (string.IsNullOrEmpty(strDataFilePath) || string.IsNullOrEmpty(strLogFilePath))
                    {
                        return false;
                    }

                    foreach (string DBName in lstDBtoCreate)
                    {
                        //dbScriptsDefaultPath = Is64Bit() ? ConfigurationManager.AppSettings.Get("DBScriptsDefaultPath64") : ConfigurationManager.AppSettings.Get("DBScriptsDefaultPath");

                        if (!Directory.Exists(updgradeScriptDefaultPath + Path.DirectorySeparatorChar + DBName))
                            continue;
                        //
                        Dispatcher.BeginInvoke(DispatcherPriority.Background, (SendOrPostCallback)delegate { lblStatus.SetValue(System.Windows.Controls.Label.ContentProperty, "Creating " + DBName + " DB..."); }, null);

                        if (!DBSettings.CreateDatabase(GetConnectionString("Master"), strDataFilePath, strLogFilePath, DBName.Trim()))
                        {
                            ErrorMessage = string.Format("{0} {1} {2}.", FindResource("MessageID55"), DBName);
                            //MessageBox.ShowText(string.Format("{0} {1} {2}.", FindResource("MessageID55"), DBName), BMC_Icon.Error);
                            isValidError = true;
                            return false;
                        }
                        LogManager.WriteLog(string.Format("{0} - {1}", "Empty DB created. Running Updgrade Scripts for database", DBName), LogManager.enumLogLevel.Info);

                        if (!ExecuteDatabaseScripts(DBName, lblStatus))
                        {
                            ErrorMessage = "Unable to create Exchange and Ticketing Database";
                            isValidError = true;
                            return false;
                        }
                        //if (DBName.ToUpper() == "EXCHANGE" | DBName.ToUpper() == "TICKETING")
                        //{
                        //    if (Convert.ToBoolean(ConfigurationManager.AppSettings.Get("AutoRunUpgradeScriptAfterDBRestore")))
                        //    {
                        //        try
                        //        {
                        //            RunDatabaseUpgradeScripts(DBName);
                        //        }
                        //        catch (Exception ex)
                        //        {
                        //            ExceptionManager.Publish(ex);

                        //            if (DBName.ToUpper() == "EXCHANGE")
                        //            {
                        //               //MessageBox.ShowBox("MessageID106", BMC_Icon.Error);
                        //                ErrorMessage = FindResource("MessageID74").ToString();
                        //            }
                        //            if (DBName.ToUpper() == "TICKETING")
                        //            {
                        //                //MessageBox.ShowBox("MessageID107", BMC_Icon.Error);
                        //                ErrorMessage = FindResource("MessageID74").ToString();
                        //            }
                        //        }
                        //    }
                        //    LogManager.WriteLog(string.Format("{0} - {1}", "Updgrade Scripts run successfully for database", DBName), LogManager.enumLogLevel.Info);
                        //}
                        LogManager.WriteLog(string.Format("{0} - {1}", "Database Created Complete.", DBName), LogManager.enumLogLevel.Info);

                    }
                }
                if (!retVal && IsExistDB.Count > 0)
                {

                    if (IsExistDB.Count == 2)
                    {
                        ErrorMessage = string.Format("{0} {1}", IsExistDB[0] + " and " + IsExistDB[1], FindResource("MessageID50"));
                    }
                    else
                    {
                        ErrorMessage = string.Format("{0} {1}", IsExistDB[0], FindResource("MessageID50"));
                    }
                }
                return retVal;
            }
            catch (Exception ex)
            {
                isValidError = true;
                ErrorMessage = FindResource("MessageID74").ToString();
                LogManager.WriteLog(FindResource("MessageID74").ToString(), LogManager.enumLogLevel.Info);
                ExceptionManager.Publish(ex);
                return false;
            }
            finally
            {
                //Cursor = System.Windows.Input.Cursors.Arrow;
            }



        }

        private void RunDatabaseUpgradeScripts(string DBName)
        {
            FileInfo file = null;
            SqlConnection sqlConnection = null;
            string scriptFile = string.Empty;

            try
            {
                LogManager.WriteLog("Inside RunDatabaseUpgradeScripts", LogManager.enumLogLevel.Info);

                List<string> UpgradeScriptFiles = GetUpgradeScriptFiles(DBName);

                string dbScriptsDefaultPath = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName) + Path.DirectorySeparatorChar + ConfigurationManager.AppSettings.Get("DBScriptsDefaultPath");
                string scriptFilePath = string.Empty;

                foreach (string scriptFileName in UpgradeScriptFiles)
                {
                    try
                    {
                        scriptFilePath = string.Format("{0}\\{1}", dbScriptsDefaultPath, scriptFileName);
                        file = new FileInfo(scriptFilePath);
                        scriptFile = file.OpenText().ReadToEnd();
                        DBSettings.ExecuteScripts(GetConnectionString(DBName), scriptFile);
                        LogManager.WriteLog(string.Format("{0}-{1}", scriptFileName, "executed successfully"), LogManager.enumLogLevel.Info);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        if (file != null)
                        {
                            file = null;
                        }
                        if (sqlConnection != null)
                        {
                            sqlConnection.Close();
                            sqlConnection.Dispose();
                            sqlConnection = null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.ShowBox("MessageID74", BMC_Icon.Error);
                ExceptionManager.Publish(ex);
            }
        }

        private List<string> GetUpgradeScriptFiles(string DBName)
        {
            XmlDocument xmlDocument = new XmlDocument();
            List<string> UpgradeScriptFiles = new List<string>();

            try
            {
                LogManager.WriteLog("Inside GetUpgradeScriptFiles", LogManager.enumLogLevel.Info);

                string updgradeScriptDefaultPath = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName) + Path.DirectorySeparatorChar + ConfigurationManager.AppSettings.Get("DBScriptsDefaultPath");
                string upgradeScriptFileName = ConfigurationManager.AppSettings.Get("UpgradeScriptFileName");

                string upgradeScriptPath = string.Format("{0}\\{1}", updgradeScriptDefaultPath, upgradeScriptFileName);

                xmlDocument.Load(upgradeScriptPath);

                if (DBName.ToUpper() == "EXCHANGE")
                {
                    foreach (XmlNode xmlNodes in xmlDocument)
                    {
                        if (xmlNodes.HasChildNodes)
                        {
                            foreach (XmlNode xNodes in xmlNodes)
                            {
                                switch (xNodes.Name.ToUpper())
                                {
                                    case "EXCHANGE":
                                        foreach (XmlNode xNode in xNodes)
                                        {
                                            UpgradeScriptFiles.Add(xNode.InnerText.Trim());
                                        }
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }
                    }
                }
                if (DBName.ToUpper() == "TICKETING")
                {
                    foreach (XmlNode xmlNodes in xmlDocument)
                    {
                        if (xmlNodes.HasChildNodes)
                        {
                            foreach (XmlNode xNodes in xmlNodes)
                            {
                                switch (xNodes.Name.ToUpper())
                                {
                                    case "TICKETING":
                                        foreach (XmlNode xNode in xNodes)
                                        {
                                            UpgradeScriptFiles.Add(xNode.InnerText.Trim());
                                        }
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }
                    }
                }

                LogManager.WriteLog("UpgradeScriptFiles fetched successfully", LogManager.enumLogLevel.Info);

                return UpgradeScriptFiles;
            }
            catch (Exception ex)
            {
                MessageBox.ShowBox("MessageID74", BMC_Icon.Error);
                ExceptionManager.Publish(ex);
                throw ex;
            }
            finally
            {
                if (xmlDocument != null)
                { xmlDocument = null; }
            }
        }

        private bool ExecuteDatabaseScripts(string DBName, Label lblStat)
        {

            string scriptFile = string.Empty;

            try
            {
                LogManager.WriteLog("Inside ExecuteScripts", LogManager.enumLogLevel.Info);

                string updgradeScriptDefaultPath = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName) + Path.DirectorySeparatorChar + ConfigurationManager.AppSettings.Get("DBScriptsDefaultPath");
                string upgradeScriptFileName = ConfigurationManager.AppSettings.Get("UpgradeScriptFileName");
                string upgradeScriptPath = string.Format("{0}\\{1}", updgradeScriptDefaultPath, upgradeScriptFileName);

                string DBScriptsDefaultPath = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName) + Path.DirectorySeparatorChar + ConfigurationManager.AppSettings.Get("DBScriptsDefaultPath");
                XmlDocument xdoc = new XmlDocument();
                xdoc.Load(updgradeScriptDefaultPath + Path.DirectorySeparatorChar + upgradeScriptFileName);
                XmlNodeList xlist = xdoc.SelectNodes("//Order/name");

                foreach (XmlNode xscriptFName in xlist)
                {

                    if ((!File.Exists(updgradeScriptDefaultPath + Path.DirectorySeparatorChar + DBName + Path.DirectorySeparatorChar + xscriptFName.InnerText + ".sql") && (!File.Exists(updgradeScriptDefaultPath + Path.DirectorySeparatorChar + DBName + Path.DirectorySeparatorChar + xscriptFName.InnerText + ".enpt"))))
                        continue;
                    Dispatcher.BeginInvoke(DispatcherPriority.Background, (SendOrPostCallback)delegate { lblStat.SetValue(System.Windows.Controls.Label.ContentProperty, "Executing " + DBName + " " + xscriptFName.InnerText + " Scripts.."); }, null);

                    string scriptFileNameSql = Directory.GetFiles(updgradeScriptDefaultPath + Path.DirectorySeparatorChar + DBName, "*.sql").Where(d => Path.GetFileNameWithoutExtension(d).Equals(xscriptFName.InnerText.Trim())).SingleOrDefault();
                    string scriptFileNameEnpt = Directory.GetFiles(updgradeScriptDefaultPath + Path.DirectorySeparatorChar + DBName, "*.enpt").Where(d => Path.GetFileNameWithoutExtension(d).Equals(xscriptFName.InnerText.Trim())).SingleOrDefault();
                    if (string.IsNullOrEmpty(scriptFileNameSql) && string.IsNullOrEmpty(scriptFileNameEnpt))
                        continue;

                    try
                    {
                        if (!string.IsNullOrEmpty(scriptFileNameSql))
                        {
                            if (xscriptFName.InnerText.ToUpper() == "SCHEMAS")
                            {
                                StreamReader sr = File.OpenText(scriptFileNameSql);
                                scriptFile = sr.ReadToEnd();
                                scriptFile = scriptFile.Replace("C:\\ExchangeDatabase", strDataFilePath);
                                sr.Close();
                                DBSettings.ExecuteScripts(GetConnectionString(DBName), scriptFile);
                                LogManager.WriteLog(string.Format("{0}-{1}", scriptFileNameSql, "executed successfully"), LogManager.enumLogLevel.Info);
                            }
                            else
                            {
                                //_UpdateStatus("Creating the " + xscriptFName.InnerText + Path.GetFileName(scriptFileNameSql));
                                StreamReader sr = File.OpenText(scriptFileNameSql);
                                scriptFile = sr.ReadToEnd();
                                sr.Close();
                                DBSettings.ExecuteScripts(GetConnectionString(DBName), scriptFile);
                                LogManager.WriteLog(string.Format("{0}-{1}", scriptFileNameSql, "executed successfully"), LogManager.enumLogLevel.Info);
                            }
                        }

                        if (!string.IsNullOrEmpty(scriptFileNameEnpt))
                        {
                            if (xscriptFName.InnerText.ToUpper() == "SCHEMAS")
                            {
                                scriptFile = DecryptFile(scriptFileNameEnpt);
                                scriptFile = scriptFile.Replace("C:\\ExchangeDatabase", strDataFilePath);
                                LogManager.WriteLog(scriptFile, LogManager.enumLogLevel.Info);
                                DBSettings.ExecuteScripts(GetConnectionString(DBName), scriptFile);
                                LogManager.WriteLog(string.Format("{0}-{1}", scriptFileNameEnpt, "executed successfully"), LogManager.enumLogLevel.Info);
                            }
                            else
                            {
                                scriptFile = DecryptFile(scriptFileNameEnpt);
                                DBSettings.ExecuteScripts(GetConnectionString(DBName), scriptFile);
                                LogManager.WriteLog(string.Format("{0}-{1}", scriptFileNameEnpt, "executed successfully"), LogManager.enumLogLevel.Info);
                            }
                        }


                    }
                    catch (Exception ex)
                    {

                        ExceptionManager.Publish(ex);
                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return false;
            }
        }

        private string DecryptFile(string inputFile)
        {
            try
            {
                string sExcrytionKey = @"!@#$%^&*";
                string sqlscript = "";
                UnicodeEncoding UE = new UnicodeEncoding();
                byte[] key = UE.GetBytes(sExcrytionKey);

                using (FileStream fsCrypt = new FileStream(inputFile, FileMode.Open))
                {
                    RijndaelManaged RMCrypto = new RijndaelManaged();

                    using (CryptoStream cs = new CryptoStream(fsCrypt, RMCrypto.CreateDecryptor(key, key), CryptoStreamMode.Read))
                    {

                        FileStream fsOut = new FileStream(Path.GetDirectoryName(inputFile) + "\\Temp.sql", FileMode.Create);

                        int data;
                        while ((data = cs.ReadByte()) != -1)
                            fsOut.WriteByte((byte)data);
                        fsOut.Flush();
                        fsOut.Dispose();
                        fsOut.Close();
                    }
                }
                StreamReader sr = File.OpenText(Path.GetDirectoryName(inputFile) + "\\Temp.sql");
                sqlscript = sr.ReadToEnd();
                sr.Close();
                File.Delete(Path.GetDirectoryName(inputFile) + "\\Temp.sql");
                return sqlscript;

            }
            catch (Exception ex)
            {
                MessageBox.ShowBox("MessageID74", BMC_Icon.Error);
                ExceptionManager.Publish(ex);
                return "";
            }

        }

        private string GetConnectionString(string DBname)
        {
            string strConnectionString = string.Empty;
            if (String.IsNullOrEmpty(DBServername) == false && String.IsNullOrEmpty(DBUserName) == false && String.IsNullOrEmpty(DBPassword) == false)
            {
                strConnectionString = "Server = " + DBServername + ";Uid = " + DBUserName + ";pwd = " + DBPassword;
            }
            if (!string.IsNullOrEmpty(DBname))
            {
                strConnectionString += ";DATABASE=" + DBname + ";";
                strConnectionString += "CONNECTION TIMEOUT=15;";
            }
            //LogManager.WriteLog("GetConnectionString():" + strConnectionString, LogManager.enumLogLevel.Debug);
            return strConnectionString;
        }

        public bool Is64Bit()
        {
            bool retVal;

            IsWow64Process(Process.GetCurrentProcess().Handle, out retVal);

            return retVal;
        }
        [DllImport("kernel32.dll", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IsWow64Process([In] IntPtr hProcess, [Out] out bool lpSystemInfo);

        private void txtExchangeCommandTimeout_PreviewExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Command == ApplicationCommands.Copy || e.Command == ApplicationCommands.Cut || e.Command == ApplicationCommands.Paste)
            {
                e.Handled = true;
            }
        }

        private void btnPCSaveConnection_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bool bTestPCConnection = false;
                bool bInsertSetting = false;
                Dictionary<string, string> dictSetregistryentries;
                string strEncryptPCConnection = string.Empty;
                dictSetregistryentries = new Dictionary<string, string>();
                bTestPCConnection = TestConnection(txtPCServer.Text, txtPCUsername.Text, txtPCPassword.Password, txtExchangeTimeout.Text, txtPCInstance.Text, 'C');
                if (bTestPCConnection)
                {
                    if (bTestPCConnection)
                    {
                        if (!string.IsNullOrEmpty(ReturnConnectionString))
                            ExchangeConfigRegistryEntities.CMPConnectionString = ReturnConnectionString;

                        strEncryptPCConnection = RegistrySettings.EncryptPCConnection();
                        if (!string.IsNullOrEmpty(strEncryptPCConnection))
                        {
                            dictSetregistryentries.Add(UIConstants.strPCConnect, strEncryptPCConnection + "+" + "REG_SZ");
                            //Save all Registry Settings under cash master
                            RegistrySettings.SetRegistryEntries(dictSetregistryentries, "Software\\Honeyframe\\" + ExchangeConfigRegistryEntities.RegistryKeyValue);
                            MessageBox.ShowBox("MessageID155", BMC_Icon.Information);
                            tiServicesSetup.Focus();

                        }

                        //bInsertSetting = DBSettings.InsertSettings(UIConstants.PTGatewayIP, (Convert.ToBoolean(PTGateway.IsChecked)) ? txtPTGatewayServerIP.Text : txtGatewayServerIP.Text);
                        //if (bInsertSetting == false)
                        //{
                        //    MessageBox.ShowBox("MessageID90", BMC_Icon.Error);
                        //}

                        //bInsertSetting = DBSettings.InsertSettings(UIConstants.PTGatewayPortNo, txtPTGatewayServerPort.Text);
                        //if (bInsertSetting == false)
                        //{
                        //    MessageBox.ShowBox("MessageID91", BMC_Icon.Error);
                        //}

                        //bInsertSetting = DBSettings.InsertSettings(UIConstants.PTGatewayMsgRspTimeOut, txtPTGatewayTimeOut.Text);
                        //if (bInsertSetting == false)
                        //{
                        //    MessageBox.ShowBox("MessageID92", BMC_Icon.Error);
                        //}

                        //bInsertSetting = DBSettings.InsertSettings(UIConstants.CMPKioskURL, txtCMPKioskURL.Text);
                        //if (bInsertSetting == false)
                        //{
                        //    MessageBox.ShowBox("MessageID95", BMC_Icon.Error);
                        //}

                        //bInsertSetting = DBSettings.InsertSettings(UIConstants.SDTReceiveCAPortNo, txtReceiveCAPortNo.Text);
                        //if (bInsertSetting == false)
                        //{
                        //    MessageBox.ShowBox("MessageID112", BMC_Icon.Error);
                        //}

                        //bInsertSetting = DBSettings.InsertSettings(UIConstants.SDTReceivePTPortNo, txtReceivePTPortNo.Text);
                        //if (bInsertSetting == false)
                        //{
                        //    MessageBox.ShowBox("MessageID111", BMC_Icon.Error);
                        //}

                        //bInsertSetting = DBSettings.InsertSettings(UIConstants.SDTSendCAPortNo, txtSendCAPortNo.Text);
                        //if (bInsertSetting == false)
                        //{
                        //    MessageBox.ShowBox("MessageID110", BMC_Icon.Error);
                        //}

                        //bInsertSetting = DBSettings.InsertSettings(UIConstants.SDTSendPTPortNo, txtSendPTPortNo.Text);
                        //if (bInsertSetting == false)
                        //{
                        //    MessageBox.ShowBox("MessageID109", BMC_Icon.Error);
                        //}

                        try
                        {
                            // this.GetSettings();
                        }
                        catch (Exception Ex)
                        {
                            LogManager.WriteLog("Error reloading setting after Exchange conn save:" + Ex.Message, LogManager.enumLogLevel.Error);
                        }
                    }
                    System.Windows.Forms.Application.DoEvents();
                }
                else
                {
                    // MessageBox.ShowBox("MessageID32", BMC_Icon.Error);
                    dictSetregistryentries.Add(UIConstants.strPCConnect, "" + "+" + "REG_SZ");
                    RegistrySettings.SetRegistryEntries(dictSetregistryentries, "Software\\Honeyframe\\" + ExchangeConfigRegistryEntities.RegistryKeyValue);
                    MessageBox.ShowBox("MessageID32", BMC_Icon.Error);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                MessageBox.ShowBox("MessageID74", BMC_Icon.Error);
            }
            finally
            {
                Cursor = System.Windows.Input.Cursors.Arrow;
            }
        }

        private void txtIPAddress_TextChanged(object sender, TextChangedEventArgs e)
        {

        }



        private void txtTicketPrefix_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            CheckIsNumeric(e);

        }
        private void CheckIsNumeric(TextCompositionEventArgs e)
        {
            int result;

            if (!(int.TryParse(e.Text, out result) || e.Text == "."))
            {
                e.Handled = true;
            }
        }

        private void txtReceiverPortNumber_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            CheckIsNumeric(e);
        }

        private void txtSenderPortNumber_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            CheckIsNumeric(e);
        }

        private void txtIPAddress_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            CheckIsNumeric(e);
        }

        private void txtTicketPrefix_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtTicketPrefix.Text.Length > 1)
            {
                MessageBox.ShowBox("MessageID139", BMC_Icon.Error); // Invalid TIS Ticket Prefix
                txtTicketPrefix.Text = string.Empty;
            }
        }

        private void txtIPAddress_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtIPAddress.Text.Length > 15)
            {
                MessageBox.ShowBox("MessageID137", BMC_Icon.Error);
                txtIPAddress.Text = string.Empty;
            }
        }

        private void txtReceiverPortNumber_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtReceiverPortNumber.Text.Length > 5)
            {
                MessageBox.ShowBox("MessageID138", BMC_Icon.Error);
                txtReceiverPortNumber.Text = string.Empty;
            }
        }

        private void txtSenderPortNumber_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtSenderPortNumber.Text.Length > 5)
            {
                MessageBox.ShowBox("MessageID142", BMC_Icon.Error);
                txtSenderPortNumber.Text = string.Empty;
            }
        }

        private void txtExternalCasinoCode_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtExternalCasinoCode.Text.Length > 50)
            {
                MessageBox.ShowBox("MessageID144", BMC_Icon.Error);
                txtExternalCasinoCode.Text = string.Empty;
            }
        }

        private void txtCommServiceURL_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtCommServiceURL.Text.Length > 255)
            {
                MessageBox.ShowBox("MessageID140", BMC_Icon.Error);
                txtCommServiceURL.Text = string.Empty;
            }
        }

        private void txtCmdWebServiceURL_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtCmdWebServiceURL.Text.Length > 255)
            {
                MessageBox.ShowBox("MessageID143", BMC_Icon.Error);
                txtCmdWebServiceURL.Text = string.Empty;
            }

        }

        private void chkCMPWebService_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (chkCMPWebService.IsChecked == true)
                {
                    NOGateway.Visibility = Visibility.Collapsed;
                    PTGateway.Visibility = Visibility.Collapsed;
                    SDTGateway.Visibility = Visibility.Collapsed;
                    txtblkCMPWebURL.Visibility = Visibility.Visible;
                    txtCMPWebURL.Visibility = Visibility.Visible;
                    NOGateway.IsChecked = true;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void chkCMPSocket_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (chkCMPSocket.IsChecked == true)
                {
                    NOGateway.Visibility = Visibility.Visible;
                    PTGateway.Visibility = Visibility.Visible;
                    SDTGateway.Visibility = Visibility.Visible;
                    txtblkCMPWebURL.Visibility = Visibility.Collapsed;
                    txtCMPWebURL.Visibility = Visibility.Collapsed;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
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

    #region List View Class

    public class LoadListView : DependencyObject, INotifyPropertyChanged
    {
        #region Properties

        public static readonly DependencyProperty CheckProperty =
            DependencyProperty.Register("Check", typeof(int), typeof(LoadListView), new UIPropertyMetadata(null));

        public static readonly DependencyProperty ServiceNameProperty =
            DependencyProperty.Register("ServiceName", typeof(string), typeof(LoadListView), new UIPropertyMetadata(null));

        public static readonly DependencyProperty ServiceStatusProperty =
            DependencyProperty.Register("ServiceStatus", typeof(string), typeof(LoadListView), new UIPropertyMetadata(null));

        public int Check
        {
            get { return (int)GetValue(CheckProperty); }
            set { SetValue(CheckProperty, value); }
        }

        public string ServiceName
        {
            get { return (string)GetValue(ServiceNameProperty); }
            set { SetValue(ServiceNameProperty, value); }
        }

        public string ServiceStatus
        {
            get { return (string)GetValue(ServiceStatusProperty); }
            set { SetValue(ServiceStatusProperty, value); }
        }

        #endregion Properties

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
    }

    #endregion

    #region GridViewConfiurationEntity

    public class GridViewConfiurationEntity : ValidationRule, INotifyPropertyChanged
    {
        #region Private Members

        private string _FieldName = string.Empty;
        private string _errorMessage = string.Empty;
        private string _startValue = string.Empty;
        private string _endValue = string.Empty;

        #endregion Private Members

        #region Public Properties

        public string FieldName
        {
            get { return _FieldName; }
            set { _FieldName = value; }
        }

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { _errorMessage = value; }
        }

        public string StartValue
        {
            get { return _startValue; }
            set { _startValue = value; OnPropertyChanged("StartValue"); }
        }

        public string EndValue
        {
            get { return _endValue; }
            set { _endValue = value; OnPropertyChanged("EndValue"); }
        }

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
            ValidationResult result = null;
            try
            {
                result = new ValidationResult(true, null);

                if (string.IsNullOrEmpty(value.ToString()))
                {
                    result = new ValidationResult(false, "Value cannot be empty");
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            return result;
        }

        #endregion
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
        private string _exchangeCommandTimeOut = string.Empty;

        private string _ticketingServer = string.Empty;
        private string _ticketingInstance = string.Empty;
        private string _ticketingServerUserName = string.Empty;
        private string _ticketingServerPassword = string.Empty;
        private string _ticketingTimeOut = string.Empty;
        private string _ticketingLocationCode = string.Empty;

        private string _cmpServer = string.Empty;
        private string _cmpInstance = string.Empty;
        private string _cmpServerUserName = string.Empty;
        private string _cmpServerPassword = string.Empty;
        private string _cmpTimeOut = string.Empty;

        private string _enterpriseServerURL = string.Empty;

        private string _multicastIP = string.Empty;
        private string _slotInterfaceIP = string.Empty;

        private string certificateIssuer = string.Empty;

        private string _FieldName = string.Empty;

        private string _ptGatewayServerIP = string.Empty;
        private string _ptGatewayServerPort = string.Empty;
        private string _ptGatewayTimeOut = string.Empty;
        private string _cmpAppUserName = string.Empty;
        private string _cmpAppPassword = string.Empty;
        private string _cmpKioskURL = string.Empty;
        private string _receiveCAPortNo = string.Empty;
        private string _receivePTPortNo = string.Empty;
        private string _sendCAPortNo = string.Empty;
        private string _sendPTPortNo = string.Empty;

        #region +S001 START
        private string _cdServer = string.Empty;
        private string _cdServerPort = string.Empty;
        private string _cdUsername = string.Empty;
        private string _cdDevicename = string.Empty;
        #endregion +S001 END

        private string _cMPWebServerURL = string.Empty;
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

        public string ExchangeCommandTimeOut
        {
            get { return _exchangeCommandTimeOut; }
            set { _exchangeCommandTimeOut = value; }
        }

        public string TicketingServer
        {
            get { return _ticketingServer; }
            set { _ticketingServer = value; OnPropertyChanged("TicketingServer"); }
        }

        public string TicketingInstance
        {
            get { return _ticketingInstance; }
            set { _ticketingInstance = value; }
        }

        public string TicketingServerUserName
        {
            get { return _ticketingServerUserName; }
            set { _ticketingServerUserName = value; OnPropertyChanged("TicketingInstance"); }
        }

        public string TicketingServerPassword
        {
            get { return _ticketingServerPassword; }
            set { _ticketingServerPassword = value; OnPropertyChanged("TicketingServerPassword"); }
        }

        public string TicketingTimeOut
        {
            get { return _ticketingTimeOut; }
            set { _ticketingTimeOut = value; OnPropertyChanged("TicketingTimeOut"); }
        }

        public string TicketingLocationCode
        {
            get { return _ticketingLocationCode; }
            set { _ticketingLocationCode = value; OnPropertyChanged("TicketingLocationCode"); }
        }

        public string CMPServer
        {
            get { return _cmpServer; }
            set { _cmpServer = value; OnPropertyChanged("CMPServer"); }
        }

        public string CMPInstance
        {
            get { return _cmpInstance; }
            set { _cmpInstance = value; }
        }

        public string CMPServerUserName
        {
            get { return _cmpServerUserName; }
            set { _cmpServerUserName = value; OnPropertyChanged("CMPServerUserName"); }
        }

        public string CMPServerPassword
        {
            get { return _cmpServerPassword; }
            set { _cmpServerPassword = value; OnPropertyChanged("CMPServerPassword"); }
        }

        public string CMPTimeOut
        {
            get { return _cmpTimeOut; }
            set { _cmpTimeOut = value; OnPropertyChanged("CMPTimeOut"); }
        }

        public string EnterpriseServerURL
        {
            get { return _enterpriseServerURL; }
            set { _enterpriseServerURL = value; OnPropertyChanged("EnterpriseServerURL"); }
        }

        public string MulticastIP
        {
            get { return _multicastIP; }
            set { _multicastIP = value; OnPropertyChanged("MulticastIP"); }
        }

        public string SlotInterfaceIP
        {
            get { return _slotInterfaceIP; }
            set { _slotInterfaceIP = value; OnPropertyChanged("SlotInterfaceIP"); }
        }

        public string CertificateIssuer
        {
            get { return certificateIssuer; }
            set { certificateIssuer = value; OnPropertyChanged("CertificateIssuer"); }
        }

        public string FieldName
        {
            get { return _FieldName; }
            set { _FieldName = value; }
        }

        public string PTGatewayServerIP
        {
            get { return _ptGatewayServerIP; }
            set { _ptGatewayServerIP = value; OnPropertyChanged("PTGatewayServerIP"); }
        }


        public string PTGatewayServerPort
        {
            get { return _ptGatewayServerPort; }
            set { _ptGatewayServerPort = value; OnPropertyChanged("PTGatewayServerPort"); }
        }

        public string PTGatewayTimeOut
        {
            get { return _ptGatewayTimeOut; }
            set { _ptGatewayTimeOut = value; OnPropertyChanged("PTGatewayTimeOut"); }
        }

        public string CMPAppUserName
        {
            get { return _cmpAppUserName; }
            set { _cmpAppUserName = value; OnPropertyChanged("CMPAppUserName"); }
        }

        public string CMPAppPassword
        {
            get { return _cmpAppPassword; }
            set { _cmpAppPassword = value; OnPropertyChanged("CMPAppPassword"); }
        }

        public string CMPKioskURL
        {
            get { return _cmpKioskURL; }
            set { _cmpKioskURL = value; OnPropertyChanged("CMPKioskURL"); }
        }

        public string ReceiveCAPortNo
        {
            get { return _receiveCAPortNo; }
            set { _receiveCAPortNo = value; OnPropertyChanged("ReceiveCAPortNo"); }
        }

        public string ReceivePTPortNo
        {
            get { return _receivePTPortNo; }
            set { _receivePTPortNo = value; OnPropertyChanged("ReceiveCAPortNo"); }
        }

        public string SendCAPortNo
        {
            get { return _sendCAPortNo; }
            set { _sendCAPortNo = value; OnPropertyChanged("SendCAPortNo"); }
        }

        public string SendPTPortNo
        {
            get { return _sendPTPortNo; }
            set { _sendPTPortNo = value; OnPropertyChanged("SendPTPortNo"); }
        }

        #region +S001 START
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

        public string CMPWebServerURL
        {
            get { return _cMPWebServerURL; }
            set { _cMPWebServerURL = value; OnPropertyChanged("CMPWebServerURL"); }
        }

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

        #region Private Method

        public bool IsValidIP(string addr)
        {
            //create our match pattern
            string pattern = @"^([1-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])(\.([0-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])){3}$";
            //create our Regular Expression object
            Regex check = new Regex(pattern);
            //boolean variable to hold the status
            bool valid = false;
            //check to make sure an ip address was provided
            if (addr == "")
            {
                //no address provided so return false
                valid = false;
            }
            else
            {
                //address provided so use the IsMatch Method
                //of the Regular Expression object
                valid = check.IsMatch(addr, 0);
            }
            //return the results
            return valid;
        }

        public bool IsValidURL(string url)
        {
            string pattern = @"^http\://[a-zA-Z0-9\-\.]+\.[a-zA-Z]{2,3}(/\S*)?$";
            Regex reg = new Regex(pattern);
            //return reg.IsMatch(url);
            return true;
        }

        #endregion Private Method

        #region Public Method

        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            ValidationResult result = new ValidationResult(true, null);
            try
            {
                string inputString = (value.ToString().Trim() ?? string.Empty).ToString();
                if (this.FieldName == "CertificateIssuer" && !MainScreen.isCertificateRequired)
                {
                    return result;
                }
                if (this.FieldName == "MulticastIP" && MainScreen.isDHCPEnabled || this.FieldName == "SlotInterfaceIP" && MainScreen.isDHCPEnabled)
                {
                    return result;
                }
                if (this.FieldName == "MulticastIP" && value.ToString() != string.Empty || this.FieldName == "SlotInterfaceIP" && value.ToString() != string.Empty
                    || this.FieldName == "PTGatewayServerIP" && value.ToString() != string.Empty)
                {
                    result = new ValidationResult(IsValidIP(value.ToString()), "Please enter valid ip address");
                    return result;
                }
                if (this.FieldName == "CMPKioskURL" && value.ToString() != string.Empty)
                {
                    result = new ValidationResult(IsValidURL(value.ToString()), "Please enter valid URL");
                    return result;
                }
                if (inputString.Length < this.MinimumLength | inputString.Length > this.MaximumLength)
                {
                    result = new ValidationResult(false, this.ErrorMessage);
                    return result;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                MessageBox.ShowBox("MessageID74", BMC_Icon.Error);
            }
            return result;
        }

        #endregion Public Method
    }

    #endregion

    #region Class GridRowBackColor
    [ValueConversion(typeof(string), typeof(Brush))]
    public class GridRowBackColor : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var bc = new BrushConverter();
            return (Brush)bc.ConvertFrom(value.ToString());
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
    #endregion

    #region Class CustomerDetailsConstants
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
    #endregion

    #region Class ColorToBrushConverter
    [ValueConversion(typeof(Color), typeof(Brush))]
    public class ColorToBrushConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (targetType != typeof(Brush)) return null;
            if (!(value is Color)) return null;
            SolidColorBrush scb = new SolidColorBrush((Color)value);
            return scb;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
    #endregion

    #region Enum
    public enum CMPInterface
    {
        None,
        PT,
        SDT

    }
    #endregion Enum
}