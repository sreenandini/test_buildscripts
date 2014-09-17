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

    #endregion Namespaces

    #region Class Sqlrestore

    /// <summary>
	/// Interaction logic for Sqlrestore.xaml
	/// </summary>
	public partial class Sqlrestore : Window
	{
        #region Declaration

        private const string strBMCConfig = "BMC Exchange Configuration";
        private string strType = string.Empty;
        private string _sKeyText = string.Empty;

        #endregion

        #region Constructor

        public Sqlrestore()
        {
            this.InitializeComponent();

            MessageBox.childOwner = this;
        }

        public Sqlrestore(string Type)
		{
			this.InitializeComponent();

            this.strType = Type;

            MessageBox.childOwner = this;
        }

        #endregion

        #region events

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {   
            GetSQLServers();
            txtPassword.Focus();
            SetStatus(false);
            txtDataBases.Text = strType;
            ValidatePasswordBox(txtPassword);
        }

        private void btnConnect_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ValidateDetails(txtUsername, "Username"))
                {
                    if (ValidateDetails(txtPassword, "Password"))
                    {
                        if (cmbServers.Items.Count > 0)
                        {
                            if (cmbServers.SelectedIndex < 0){}                            
                            else
                            {
                                AddServerDetails(cmbServers.SelectedItem.ToString(), txtUsername.Text, txtPassword.Password);
                            }
                            LogManager.WriteLog("Combo selection:" + cmbServers.SelectedItem.ToString(), LogManager.enumLogLevel.Debug);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.ShowBox("MessageID74", BMC_Icon.Error);
                ExceptionManager.Publish(ex);
                LogManager.WriteLog("lblConnectDB_LinkClicked:" + ex.Message + ex.Source.ToString(), LogManager.enumLogLevel.Error);
            }
        }

        private void btnRestore_Click(object sender, RoutedEventArgs e)
        {
            string blankDBFile              =   string.Empty;
            string dbScriptsDefaultPath     =   string.Empty;
            string backupFileName           =   string.Empty;
            string sSQLServerDetails        =   string.Empty;
            bool bDBExists                  =   false;
            try
            {
                Cursor = System.Windows.Input.Cursors.Wait;

                sSQLServerDetails = GetConnectionString();
                bDBExists = DBSettings.CheckDBExists(sSQLServerDetails, txtDataBases.Text, 60);

                if (bDBExists == true)
                {
                    MessageBox.ShowText(string.Format("{0} {1}", txtDataBases.Text, FindResource("MessageID50")), BMC_Icon.Information);
                    LogManager.WriteLog(string.Format("{0} {1}", txtDataBases.Text, FindResource("MessageID50")), LogManager.enumLogLevel.Info);
                }
                else
                {
                    dbScriptsDefaultPath = ConfigurationManager.AppSettings.Get("DBScriptsDefaultPath");

                    if (this.strType.ToUpper() == "EXCHANGE")
                    {
                        blankDBFile = ConfigurationManager.AppSettings.Get("ExchangeBlankDB");
                    }
                    if (this.strType.ToUpper() == "TICKETING")
                    {
                        blankDBFile = ConfigurationManager.AppSettings.Get("TicketingBlankDB");
                    }
                    if (this.strType.ToUpper() == "EXTSYSMSG")
                    {
                        blankDBFile = ConfigurationManager.AppSettings.Get("EXTSYSMSGBlankDB");
                    }

                    backupFileName = string.Format("{0}\\{1}", dbScriptsDefaultPath, blankDBFile);

                    // Create a new connection to the Server
                    ServerConnection serverConnection = new ServerConnection(cmbServers.SelectedItem.ToString());

                    // Log in using SQL authentication instead of Windows authentication
                    serverConnection.LoginSecure = false;

                    // Give the login username
                    serverConnection.Login = txtUsername.Text;

                    // Give the login password
                    serverConnection.Password = txtPassword.Password;

                    // Create a new SQL Server object using the connection we created
                    Server server = new Server(serverConnection);

                    // Create a new database restore operation
                    Restore rstDatabase = new Restore();

                    // Set the restore type to a database restore
                    rstDatabase.Action = RestoreActionType.Database;

                    // Set the database that we want to perform the restore on
                    rstDatabase.Database = txtDataBases.Text;

                    // Set the backup device from which we want to restore the db
                    BackupDeviceItem bkpDevice = new BackupDeviceItem(backupFileName, DeviceType.File);

                    // Add the backup device to the restore type
                    rstDatabase.Devices.Add(bkpDevice);

                    // Optional. ReplaceDatabase property ensures that any existing copy of the database is overwritten.
                    rstDatabase.ReplaceDatabase = true;

                    // Perform the restore
                    rstDatabase.SqlRestore(server);

                    LogManager.WriteLog(string.Format("{0} - {1}", "Database Restore Complete. Running Updgrade Scripts for database", txtDataBases.Text), LogManager.enumLogLevel.Info);

                    if (this.strType.ToUpper() == "EXCHANGE" | this.strType.ToUpper() == "TICKETING")
                    {
                        if (Convert.ToBoolean(ConfigurationManager.AppSettings.Get("AutoRunUpgradeScriptAfterDBRestore")))
                        {
                            try
                            {
                                RunDatabaseUpgradeScripts();
                            }
                            catch (Exception ex)
                            {
                                ExceptionManager.Publish(ex);

                                if (this.strType.ToUpper() == "EXCHANGE")
                                {
                                    MessageBox.ShowBox("MessageID106", BMC_Icon.Error);
                                }
                                if (this.strType.ToUpper() == "TICKETING")
                                {
                                    MessageBox.ShowBox("MessageID107", BMC_Icon.Error);
                                }
                            }
                        }

                        LogManager.WriteLog(string.Format("{0} - {1}", "Updgrade Scripts run successfully for database", txtDataBases.Text), LogManager.enumLogLevel.Info);
                    }

                    MessageBox.ShowText(string.Format("{0} {1} {2}.", FindResource("MessageID53"), txtDataBases.Text, FindResource("MessageID54")), BMC_Icon.Information);
                }
            }            
            catch (Exception ex)
            {
                MessageBox.ShowBox("MessageID74", BMC_Icon.Error);
                ExceptionManager.Publish(ex);                
            }
            finally
            {
                btnRestore.IsEnabled = true;
                Cursor = System.Windows.Input.Cursors.Arrow;
            }
        }

        private void btnClose_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			this.Close();
		}

        private void txtUsername_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!Settings.OnScreenKeyboard)
                return;
            txtUsername.Text = DisplayKeyboard(txtUsername.Text, string.Empty);

            if (txtUsername.Text.Trim() == string.Empty)
            {
                txtUsername.Visibility = Visibility.Visible;
                txtUsername.ToolTip = "Please enter Username";
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

        private void txtUsername_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtUsername.Text.Trim() == string.Empty)
            {
                tbLoginRequired.Visibility = Visibility.Visible;
                ValidateTextBox(txtUsername);
            }
        }

        private void txtUsername_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtUsername.Text.Trim() == string.Empty)
            {
                tbLoginRequired.Visibility = Visibility.Visible;
                ValidateTextBox(txtUsername);
            }
        }

        private void txtUsername_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtUsername.Visibility == Visibility.Visible)
            {
                tbLoginRequired.Visibility = Visibility.Hidden;
                txtUsername.ToolTip = null;
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

        private void txtPassword_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtPassword.Password.Trim() == string.Empty)
            {
                tbPasswordRequired.Visibility = Visibility.Visible;
                ValidatePasswordBox(txtPassword);
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (tbPasswordRequired.Visibility == Visibility.Visible)
            {
                tbPasswordRequired.Visibility = Visibility.Hidden;
                tbPasswordRequired.ToolTip = null;
            }
        }

        #endregion

        #region Private Methods

        private bool ValidateDetails(TextBox tBox, string Message)
        {
            bool bResult = false;
            try
            {
                if (tBox.Text.Length == 0)
                {   
                    bResult = false;
                }
                else
                {   
                    bResult = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.ShowBox("MessageID74", BMC_Icon.Error);
                ExceptionManager.Publish(ex);
            }
            return bResult;
        }

        private bool ValidateDetails(PasswordBox pBox, string Message)
        {
            bool bResult = false;
            try
            {
                if (pBox.Password.Length == 0)
                {   
                    bResult = false;
                }
                else
                {   
                    bResult = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.ShowBox("MessageID74", BMC_Icon.Error);
                ExceptionManager.Publish(ex);
            }
            return bResult;
        }

        private void GetSQLServers()
        {
            //Retrieve the available servers.
            List<string> ListResult = DBSettings.GetServers();
            for (int j = 0; j < ListResult.Count; j++)
            {
                cmbServers.Items.Add(ListResult[j]);
            }
            if (this.cmbServers.Items.Count > 0)
                this.cmbServers.SelectedIndex = 0;
            else
                this.cmbServers.Text = "<No available SQL Servers>";
        }

        private void SetStatus(bool status)
        {   
            //txtDataBases.IsEnabled = status;
            btnRestore.IsEnabled = status;
        }

        private void AddServerDetails(string Server, string UserName, string Password)
        {
            bool bResult = false;
            try
            {
                Dictionary<string, string> objServerDetails = new Dictionary<string, string>();
                objServerDetails.Add("SERVER", Server);
                objServerDetails.Add("UID", UserName);
                objServerDetails.Add("PWD", Password);
                objServerDetails.Add("TIMEOUT", "60");

                string ReturnConnectionString = Credentials.MakeConnectionString(objServerDetails);
                //LogManager.WriteLog("ReturnConnectionString:" + ReturnConnectionString, LogManager.enumLogLevel.Debug);
                if (!String.IsNullOrEmpty(ReturnConnectionString))
                {
                    bResult = Credentials.TestConnectionDB(ReturnConnectionString);
                    if (bResult == true)
                    {
                        MessageBox.ShowBox("MessageID48", BMC_Icon.Information);
                        SetStatus(true);                        
                    }
                    else
                    {
                        MessageBox.ShowBox("MessageID49", BMC_Icon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.ShowBox("MessageID74", BMC_Icon.Error);
                ExceptionManager.Publish(ex);
                LogManager.WriteLog("AddServerDetails" + ex.Message + ex.Source.ToString(), LogManager.enumLogLevel.Error);
            }
        }

        private string GetConnectionString()
        {
            string strConnectionString = string.Empty;
            if (cmbServers.SelectedItem != null && String.IsNullOrEmpty(txtUsername.Text) == false && String.IsNullOrEmpty(txtPassword.Password) == false)
            {
                strConnectionString = "Server = " + cmbServers.SelectedItem.ToString() + ";Uid = " + txtUsername.Text + ";pwd = " + txtPassword.Password;
            }
            //LogManager.WriteLog("GetConnectionString():" + strConnectionString, LogManager.enumLogLevel.Debug);
            return strConnectionString;
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

        private void RunDatabaseUpgradeScripts()
        {   
            FileInfo file               =   null;
            SqlConnection sqlConnection =   null;
            Server server               =   null;
            string scriptFile           =   string.Empty;

            try
            {
                LogManager.WriteLog("Inside RunDatabaseUpgradeScripts", LogManager.enumLogLevel.Info);

                List<string> UpgradeScriptFiles = GetUpgradeScriptFiles();

                string dbScriptsDefaultPath     =   ConfigurationManager.AppSettings.Get("DBScriptsDefaultPath");
                string scriptFilePath           =   string.Empty;
                
                foreach (string scriptFileName in UpgradeScriptFiles)
                {
                    try
                    {
                        scriptFilePath = string.Format("{0}\\{1}", dbScriptsDefaultPath, scriptFileName);
                        file = new FileInfo(scriptFilePath);
                        scriptFile = file.OpenText().ReadToEnd();

                        sqlConnection = new SqlConnection(GetConnectionString());
                        server = new Server(new ServerConnection(sqlConnection));

                        int result = server.ConnectionContext.ExecuteNonQuery(scriptFile);

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
                        if (server != null)
                        {
                            server = null;
                        }
                    }
                }                
            }
            catch (Exception ex)
            {   
                throw ex;
            }
        }

        private List<string> GetUpgradeScriptFiles()
        {
            XmlDocument xmlDocument         =   new XmlDocument();
            List<string> UpgradeScriptFiles =   new List<string>();

            try
            {
                LogManager.WriteLog("Inside GetUpgradeScriptFiles", LogManager.enumLogLevel.Info);

                string updgradeScriptDefaultPath    =   ConfigurationManager.AppSettings.Get("UpdgradeScriptDefaultPath");
                string upgradeScriptFileName        =   ConfigurationManager.AppSettings.Get("UpgradeScriptFileName");

                string upgradeScriptPath            =   string.Format("{0}\\{1}", updgradeScriptDefaultPath, upgradeScriptFileName);

                xmlDocument.Load(upgradeScriptPath);

                if (this.strType.ToUpper() == "EXCHANGE")
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
                if (this.strType.ToUpper() == "TICKETING")
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
                throw ex;
            }
            finally
            {
                if (xmlDocument != null)
                { xmlDocument = null; }
            }
        }

        #endregion

    }

    #endregion
}