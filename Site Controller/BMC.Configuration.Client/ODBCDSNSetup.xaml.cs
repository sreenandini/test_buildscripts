namespace BMC.ExchangeConfig
{
    #region Namespaces

    using System;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Documents;
    using System.Windows.Input;
    using BMC.Transport.ExchangeConfig;
    using BMC.Common.ExceptionManagement;
    using BMC.Business.ExchangeConfig;
    using System.ComponentModel;

    #endregion Namespaces

    #region ODBCDSNSetup Class

    /// <summary>
    /// Interaction logic for ODBCDSNSetup.xaml
    /// </summary>
    public partial class ODBCDSNSetup : Window
    {
        #region Declarations

        private DSNConfigurationEntity dsnConfigurationEntity = new DSNConfigurationEntity();
        private string _sKeyText = string.Empty;

        #endregion Declarations

        #region Constructor

        public ODBCDSNSetup()
        {
            InitializeComponent();
            MessageBox.childOwner = this;
        }

        public ODBCDSNSetup(string sServer, string sUserName, string sPwd)
        {
            try
            {   
                InitializeComponent();
                ExchangeConfigRegistryEntities.ODBCServer = sServer;
                ExchangeConfigRegistryEntities.ODBCUsername = sUserName;
                ExchangeConfigRegistryEntities.ODBCPwd = sPwd;
            }
            catch (Exception ex)
            {
                MessageBox.ShowBox("MessageID33", BMC_Icon.Error);
                ExceptionManager.Publish(ex);
            }
            MessageBox.childOwner = this;
        }

        #endregion Constructor

        #region Events

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                LoadServers();
                LoadDatabases();
                LoadLanguages();
                RefreshControls();
            }
            catch (Exception ex)
            {
                MessageBox.ShowBox("MessageID33", BMC_Icon.Error);
                ExceptionManager.Publish(ex);
            }
        }

        private void btnTestConnection_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string connectionstring = string.Empty;
                if (rbSQLAuthentication.IsChecked == true)
                {
                    if (ValidateText(txtDSReferName, "Data Source Reference Name"))
                    {
                        if (ValidateText(cmbServer, "Server"))
                        {
                            if (ValidateTextBox(txtLoginName))
                            {
                                if (ValidatePasswordBox(txtPassword))
                                {
                                    if (ValidateText(cmbDefaultDB, "Default Database"))
                                    {
                                        if (ValidateText(cmbDefaultLang, "Default Language"))
                                        {
                                            connectionstring = "DRIVER={SQL Server};" +
                                                                        "SERVER=" + cmbServer.Text.ToString() +
                                                                        ";Trusted_connection=No" +
                                                                        ";DATABASE=" + cmbDefaultDB.Text.ToString() +
                                                                        ";Uid=" + txtLoginName.Text +
                                                                        ";Pwd=" + txtPassword.Password + ";";                                            

                                            if (BMC.DBInterface.ExchangeConfig.DBBuilder.TestODBCConnection(connectionstring))
                                                MessageBox.ShowBox("MessageID20", BMC_Icon.Information);
                                            else
                                                MessageBox.ShowBox("MessageID34", BMC_Icon.Error);
                                        }
                                        else
                                        {
                                            MessageBox.ShowBox("MessageID26", BMC_Icon.Information);
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.ShowBox("MessageID25", BMC_Icon.Information);
                                    }
                                }
                                else
                                {
                                    MessageBox.ShowBox("MessageID29", BMC_Icon.Information);
                                }
                            }
                            else
                            {
                                MessageBox.ShowBox("MessageID28", BMC_Icon.Information);
                            }
                        }
                        else
                        {
                            MessageBox.ShowBox("MessageID27", BMC_Icon.Information);
                        }
                    }
                }
                else
                {
                    if (ValidateText(txtDSReferName, "Data Source Reference Name"))
                    {
                        if (ValidateText(cmbServer, "Server"))
                        {
                            if (ValidateText(cmbDefaultDB, "Default Database"))
                            {
                                if (ValidateText(cmbDefaultLang, "Default Language"))
                                {
                                    connectionstring = "DRIVER={SQL Server};" +
                                                                   "SERVER=" + cmbServer.Text.ToString() +
                                                                   ";Trusted_connection=YES" +
                                                                   ";DATABASE=" + cmbDefaultDB.Text.ToString();
                                    if (BMC.DBInterface.ExchangeConfig.DBBuilder.TestODBCConnection(connectionstring))
                                        MessageBox.ShowBox("MessageID20", BMC_Icon.Information);
                                }
                                else
                                {
                                    MessageBox.ShowBox("MessageID26", BMC_Icon.Information);
                                }
                            }
                            else
                            {
                                MessageBox.ShowBox("MessageID25", BMC_Icon.Information);
                            }
                        }
                        else
                        {
                            MessageBox.ShowBox("MessageID27", BMC_Icon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.ShowBox("MessageID33", BMC_Icon.Error);
                ExceptionManager.Publish(ex);
            }
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = string.Empty;

            try
            {
                ExchangeConfigRegistryEntities.ODBCRegKeyValue = UIConstants.strODBCRegPath;
                if (txtDSReferName.Text != string.Empty)
                {
                    ExchangeConfigRegistryEntities.DataSourceReferenceName = (txtDSReferName.Text != string.Empty ? txtDSReferName.Text : "Leisure SQL");
                    ExchangeConfigRegistryEntities.ODBCDescription = txtDescription.Text;
                    if (cmbServer.Text != string.Empty || cmbServer.Text != null)
                    {
                        ExchangeConfigRegistryEntities.ODBCServer = (cmbServer.Text != null ? cmbServer.Text.ToString() : "(local)");
                        ExchangeConfigRegistryEntities.DefaultDatabase = (cmbDefaultDB.Text != null ? cmbDefaultDB.Text.ToString() : "Exchange");
                        ExchangeConfigRegistryEntities.DefaultLanguage = (cmbDefaultLang.Text != null ? cmbDefaultLang.Text.ToString() : "british");
                        if (rbSQLAuthentication.IsChecked == true)
                        {
                            if (ValidateTextBox(txtLoginName))
                            {   
                                if (ValidatePasswordBox(txtPassword))
                                {
                                    if (ValidateText(cmbDefaultDB, "Default Database"))
                                    {
                                        if (ValidateText(cmbDefaultLang, "Default Language"))
                                        {
                                            ExchangeConfigRegistryEntities.ODBCUsername = txtLoginName.Text;
                                            ExchangeConfigRegistryEntities.ODBCPwd = txtPassword.Password;

                                            connectionString = "DRIVER={SQL Server};" +
                                                                        "SERVER=" + cmbServer.Text.ToString() +
                                                                        ";Trusted_connection=No" +
                                                                        ";DATABASE=" + cmbDefaultDB.Text.ToString() +
                                                                        ";Uid=" + txtLoginName.Text +
                                                                        ";Pwd=" + txtPassword.Password + ";";

                                            //if (BMC.DBInterface.ExchangeConfig.DBBuilder.TestODBCConnection(connectionString))
                                            //{
                                            //    if (ReadServicesSettings.DSNSettings(true))
                                            //    MessageBox.ShowBox("MessageID21", BMC_Icon.Information);
                                            //}
                                            //else
                                            //{
                                            //    MessageBox.ShowBox("MessageID35", BMC_Icon.Error);
                                            //}   
                                        }
                                        else
                                        {
                                            MessageBox.ShowBox("MessageID26", BMC_Icon.Information);
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.ShowBox("MessageID25", BMC_Icon.Information);
                                    }
                                }
                                else
                                {
                                    MessageBox.ShowBox("MessageID29", BMC_Icon.Information);
                                }
                            }
                            else
                            {
                                MessageBox.ShowBox("MessageID28", BMC_Icon.Information);
                            }
                        }
                        else
                        {
                            if (ValidateText(cmbDefaultDB, "Default Database"))
                            {
                                //if (ValidateText(cmbDefaultLang, "Default Language"))
                                //{
                                //    if (ReadServicesSettings.DSNSettings(false))
                                //    {
                                //        MessageBox.ShowBox("MessageID21", BMC_Icon.Information);
                                //    }
                                //}
                                //else
                                //{
                                //    MessageBox.ShowBox("MessageID26", BMC_Icon.Information);
                                //}
                            }
                            else
                            {
                                MessageBox.ShowBox("MessageID25", BMC_Icon.Information);
                            }
                        }
                    }
                    else
                        MessageBox.ShowBox("MessageID27", BMC_Icon.Information);                              
                }
                else
                    MessageBox.ShowBox("MessageID23", BMC_Icon.Information);                          
            }
            catch (Exception ex)
            {
                MessageBox.ShowBox("MessageID33", BMC_Icon.Error);
                ExceptionManager.Publish(ex);
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Cursor = System.Windows.Input.Cursors.Wait;               

                txtDescription.Text = string.Empty;
                rbWindowsAuthentication.IsChecked = true;
                rbSQLAuthentication.IsChecked = false;

                LoadServers();
                LoadDatabases();
                LoadLanguages();

                MessageBox.ShowBox("MessageID24", BMC_Icon.Information);
            }
            finally
            {
                Cursor = System.Windows.Input.Cursors.Arrow;               
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void rbSqlAuthentication_Checked(object sender, RoutedEventArgs e)
        {   
            RefreshControls();
            txtLoginName.Text = ExchangeConfigRegistryEntities.ODBCUsername != null ? ExchangeConfigRegistryEntities.ODBCUsername : string.Empty;
            txtPassword.Password = ExchangeConfigRegistryEntities.ODBCPwd;
            ValidateTextBox(txtLoginName);
            ValidatePasswordBox(txtPassword);            
        }

        private void rbSqlAuthentication_Unchecked(object sender, RoutedEventArgs e)
        {   
            txtLoginName.Text = ExchangeConfigRegistryEntities.ODBCUsername;
            txtPassword.Password = ExchangeConfigRegistryEntities.ODBCPwd;
        }

        private void rbWindowsAuthentication_Unchecked(object sender, RoutedEventArgs e)
        {   
            txtLoginName.Text = string.Empty;
            txtPassword.Password = string.Empty;
        }

        private void rbWindowsAuthentication_Checked(object sender, RoutedEventArgs e)
        {
            try
            {   
                RefreshControls();                
            }
            catch (Exception ex)
            {
                MessageBox.ShowBox("MessageID33", BMC_Icon.Error);
                ExceptionManager.Publish(ex);
            }
        }

        private void txtPassword_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (tbPasswordRequired.Visibility == Visibility.Visible)
            {
                tbPasswordRequired.Visibility = Visibility.Hidden;
                tbPasswordRequired.ToolTip = null;
            }
        }

        private void txtPassword_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (txtPassword.Password.Trim() == string.Empty)
            {
                tbPasswordRequired.Visibility = Visibility.Visible;
                ValidatePasswordBox(txtPassword);
            }
        }

        private void txtPassword_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtPassword.Password.Trim() == string.Empty)
            {
                tbPasswordRequired.Visibility = Visibility.Visible;
                ValidatePasswordBox(txtPassword);
            }
        }

        private void txtLoginName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtLoginName.Text.Trim() == string.Empty)
            {
                tbLoginRequired.Visibility = Visibility.Visible;
                ValidateTextBox(txtLoginName);
            }
        }

        private void txtLoginName_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (txtLoginName.Text.Trim() == string.Empty)
            {
                tbLoginRequired.Visibility = Visibility.Visible;
                ValidateTextBox(txtLoginName);
            }
        }

        private void txtLoginName_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (tbLoginRequired.Visibility == Visibility.Visible)
            {
                tbLoginRequired.Visibility = Visibility.Hidden;
                tbLoginRequired.ToolTip = null;
            }
        }

        private void txtDescription_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!Settings.OnScreenKeyboard)
                return;
            txtDescription.Text = DisplayKeyboard(txtDescription.Text, string.Empty);
            txtDescription.SelectionStart = txtDescription.Text.Length;
        }

        private void txtLoginName_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!Settings.OnScreenKeyboard)
                return;
            txtLoginName.Text = DisplayKeyboard(txtLoginName.Text, string.Empty);

            if (txtLoginName.Text.Trim() == string.Empty)
            {
                txtLoginName.Visibility = Visibility.Visible;
                txtLoginName.ToolTip = "Please enter Username";
                tbLoginRequired.Visibility = Visibility.Visible;
            }
            else
            {
                tbLoginRequired.Visibility = Visibility.Hidden;
                tbLoginRequired.ToolTip = null;
            }
        }

        private void txtPassword_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!Settings.OnScreenKeyboard)
                return;

            txtPassword.Password = DisplayKeyboard(string.Empty, "Pwd");

            if (txtPassword.Password.Trim() == string.Empty)
            {
                txtPassword.Visibility = Visibility.Visible;
                txtPassword.ToolTip = "Please enter Password";
                tbPasswordRequired.Visibility = Visibility.Visible;
            }
            else
            {
                tbPasswordRequired.Visibility = Visibility.Hidden;
                tbPasswordRequired.ToolTip = null;
            }
        }

        #endregion Events

        #region Private Methods

        private void LoadServers()
        {
            try
            {
                //Retrieve the available servers.
                List<string> ListResult = DBSettings.GetServers();
                for (int j = 0; j < ListResult.Count; j++)
                {
                    cmbServer.Items.Add(ListResult[j]);
                }
                if (this.cmbServer.Items.Count > 0)
                    this.cmbServer.SelectedIndex = 0;
                else
                    this.cmbServer.Text = "<No available SQL Servers>";
            }
            catch (Exception ex)
            {
                MessageBox.ShowBox("MessageID33", BMC_Icon.Error);
                ExceptionManager.Publish(ex);
            }
        }

        private void LoadDatabases()
        {
            //try
            //{
            //    //Retrieve the available Databases.
            //    List<string> ListResult = DBSettings.GetDatabases(ExchangeConfigRegistryEntities.ODBCServer,
            //    ExchangeConfigRegistryEntities.ODBCUsername, ExchangeConfigRegistryEntities.ODBCPwd);
            //    for (int j = 0; j < ListResult.Count; j++)
            //    {
            //        cmbDefaultDB.Items.Add(ListResult[j]);
            //    }
            //    if (this.cmbDefaultDB.Items.Count == 0)
            //    {
            //        this.cmbDefaultDB.Items.Add("<No available SQL Databases>");                    
            //    }
            //    this.cmbDefaultDB.SelectedIndex = 0;
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.ShowBox("MessageID33", BMC_Icon.Error);
            //    ExceptionManager.Publish(ex);
            //}
        }

        private void LoadLanguages()
        {
            ////Retrieve the available Languages.
            //try
            //{
            //    List<string> ListResult = DBSettings.GetLanguages(ExchangeConfigRegistryEntities.ODBCServer,
            //    ExchangeConfigRegistryEntities.ODBCUsername, ExchangeConfigRegistryEntities.ODBCPwd);
            //    for (int j = 0; j < ListResult.Count; j++)
            //    {   
            //        this.cmbDefaultLang.Items.Add(ListResult[j]);
            //    }
            //    if (this.cmbDefaultLang.Items.Count == 0)
            //    {
            //        this.cmbDefaultLang.Items.Add("<No available SQL Languages>");
            //    }                
            //    this.cmbDefaultLang.SelectedIndex = 0;
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.ShowBox("MessageID33", BMC_Icon.Error);
            //    ExceptionManager.Publish(ex);
            //}
        }

        private bool ValidateText(System.Windows.Controls.TextBox tBox, string Message)
        {
            bool bStatus = false;
            if (tBox.Text.Length == 0)
            {
                bStatus = false;
            }
            else
            {
                bStatus = true;
            }
            return bStatus;
        }

        private bool ValidateText(System.Windows.Controls.ComboBox tBox, string Message)
        {
            bool bStatus = false;
            if (tBox.Text == string.Empty || tBox.Text.ToUpper() == "<NO AVAILABLE SQL DATABASES>")
            {
                bStatus = false;
            }
            else
            {
                bStatus = true;
            }
            return bStatus;
        }

        private bool ValidateTextBox(System.Windows.Controls.TextBox tBox)
        {
            if (tBox.Text == string.Empty)
            {
                tBox.ToolTip = "Please enter Username";
                tbLoginRequired.Visibility = Visibility.Visible;

                return false;
            }
            else
            {
                return true;
            }
        }

        private bool ValidatePasswordBox(PasswordBox pBox)
        {
            if (pBox.Password == string.Empty)
            {
                pBox.ToolTip = "Please enter Password";
                tbPasswordRequired.Visibility = Visibility.Visible;

                return false;
            }
            else
            {
                return true;
            }
        }

        private void RefreshControls()
        {
            if (tbLoginName != null)
            {
                if (rbWindowsAuthentication.IsChecked == true)
                {
                    tbLoginName.IsEnabled = false;
                    tbPassword.IsEnabled = false;
                    txtLoginName.Text = string.Empty;
                    txtLoginName.IsEnabled = false;
                    txtPassword.Password = string.Empty;
                    txtPassword.IsEnabled = false;

                    tbLoginRequired.Visibility = Visibility.Hidden;
                    tbLoginRequired.ToolTip = null;

                    tbPasswordRequired.Visibility = Visibility.Hidden;
                    tbPasswordRequired.ToolTip = null;
                }
                else
                {
                    tbLoginName.IsEnabled = true;
                    tbPassword.IsEnabled = true;
                    txtLoginName.IsEnabled = true;
                    txtPassword.IsEnabled = true;
                }
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

        #endregion Private Methods
    }

    #endregion

    #region DSNConfigurationEntity Class

    public class DSNConfigurationEntity : ValidationRule, INotifyPropertyChanged
    {
        #region Private Declarations

        private int _minimumLength = -1;
        private int _maximumLength = -1;
        private string _errorMessage;

        private string _loginName = string.Empty;
        private string _password = string.Empty;

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

        public string LoginName
        {
            get { return _loginName; }
            set { _loginName = value; OnPropertyChanged("LoginName"); }
        }

        public string Password
        {
            get { return _password; }
            set { _password = value; OnPropertyChanged("Password"); }
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

        #region Public Methods

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
                MessageBox.ShowBox("MessageID33", BMC_Icon.Error);
                ExceptionManager.Publish(ex);
            }
            return result;
        }

        #endregion Public Methods
    }

    #endregion
}
