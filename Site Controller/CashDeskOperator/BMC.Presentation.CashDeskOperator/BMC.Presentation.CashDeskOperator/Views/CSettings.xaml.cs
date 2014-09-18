using System;
using System.Data;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using BMC.CashDeskOperator.BusinessObjects;
using BMC.Common.ConfigurationManagement;
using BMC.Common.ExceptionManagement;
using BMC.Common.LogManagement;
using Microsoft.Win32;
using System.Diagnostics;
using BMC.Presentation.POS.Helper_classes;
using BMC.Transport;
using BMC.Business.CashDeskOperator;
using BMC.Common.Utilities;
using BMC.CoreLib;
using System.Windows.Media;

namespace BMC.Presentation
{
    /// <summary>
    /// Interaction logic for CSettings.xaml
    /// </summary>
    public partial class CSettings : UserControl, IDisposable
    {
        #region Constructor
        /// <summary>
        /// Constructor.
        /// </summary>
        public CSettings()
        {
            InitializeComponent();
        }
        #endregion Constructor

        #region Events
        /// <summary>
        /// User Control load event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();

            if (!Security.SecurityHelper.HasAccess("CashdeskOperator.MainScreen.cs.Site Settings.btnSystemConfigParameters"))
                btnSystemConfigParameters.Visibility = Visibility.Hidden;

        }
        #endregion Events

        #region Public Methods
        /// <summary>
        /// Loads the Settings Data.
        /// </summary>
        /// <param name="strProfileName"></param>
        public void LoadData()
        {
            try
            {
                DataSet dsSiteProfile = new DataSet();
              
          
                DataTable dtSettingListCopy = new DataTable();
                ISettingsDetails objCDO = SettingsBusinessObject.CreateInstance();


                dsSiteProfile = objCDO.GetSettingDetails();

                DataTable dtSettingList = dsSiteProfile.Tables[1];
                DataTable dtSiteProfileName =dsSiteProfile.Tables[0];

                dtSettingListCopy = dtSettingList.Copy();

                //Get list of settings to be skipped.
                string strEventTypes = string.Empty;
                string[] strList;

                strEventTypes = objCDO.FillSettingsToBeSkipped();
                strList = strEventTypes.Split(',');
                int iColumn = -1;
                
                SiteLicensingConfiguration oSiteLicensingConfiguration = SiteLicensingConfiguration.SiteLicensingConfigurationInstance;
                SiteLicenseDetailsEntity currentLicense = (oSiteLicensingConfiguration!= null)?oSiteLicensingConfiguration.CurrentLicense():null;

                foreach (DataRow row in dtSiteProfileName.Rows)
                {
                    txtProfileName.Text = row["ProfileName"].ToString();
                }

                foreach (DataRow dr in dtSettingListCopy.Rows)
                {
                    iColumn++;
                    if (currentLicense != null)
                    {
                        if (dr[1].ToString().Equals("LICENSE DISABLE GAMES"))
                        {
                            dtSettingList.Rows[iColumn][2] = currentLicense.DisableGames;
                        }
                        else if (dr[1].ToString().Equals("LICENSE EXPIRY DATE"))
                        {
                            dtSettingList.Rows[iColumn][2] = currentLicense.ExpireDate;
                        }
                        else if(dr[1].ToString().Equals("LICENSE LOCK SITE"))
                        {
                            dtSettingList.Rows[iColumn][2] = currentLicense.LockSite;
                        }
                        else if(dr[1].ToString().Equals("LICENSE STATUS"))
                        {
                            dtSettingList.Rows[iColumn][2] = ((SiteLicensing.LicenseKeyStatus)(currentLicense.KeyStatusID)).ToString();
                        }
                        else if (dr[1].ToString().Equals("LICENSE VALIDATION REQUIRED"))
                        {
                            dtSettingList.Rows[iColumn][2] = currentLicense.ValidationRequired;
                        }
                        else if (dr[1].ToString().Equals("LICENSE WARNING ONLY"))
                        {
                            dtSettingList.Rows[iColumn][2] = currentLicense.WarningOnly;
                        }
                    }
                    foreach (String str in strList)
                    {
                        if (dr[1].ToString() == str)
                        {
                            dtSettingList.Rows.RemoveAt(iColumn);
                            iColumn--;
                        }
                    }
                }

                lstSettingDetails.DataContext = dtSettingList;
            }
            catch (Exception exLoadPropertyGrid)
            {
                ExceptionManager.Publish(exLoadPropertyGrid);
            }
        }
        #endregion Public Methods

        /// <summary>
        /// Launch BMC.ExchangeConfig.Client.exe if Config Parameters option is enabled in Enterprise
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSystemConfigParameters_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                btnSystemConfigParameters.IsEnabled = false;
                string installationPath = "";
                //string installationType = "";
                string ExchangeConfigFileName = "";
                string processName = "";

                ConfigManager.SetConfigurationMode(ConfigManager.ConfigurationMode.AppConfig);
                //RegistryKey installationPathkey = BMCRegistryHelper.GetRegLocalMachine().OpenSubKey();
                //installationPath = BMCRegistryHelper.GetRegKeyValue(ConfigManager.Read("ExchangeClientInstallationPath"), "InstallationPath").ToString();
                installationPath = Extensions.GetStartupDirectory();
                //installationType = installationPathkey.GetValue("InstallationType").ToString();
                bool isExecutableFound = false;


                if (BMCRegistryHelper.IsExchangeClient())
                {
                    ExchangeConfigFileName = "BMC.ExchangeConfig.Client.exe";
                }
                else if (BMCRegistryHelper.IsExchangeServer())
                {
                    ExchangeConfigFileName = "BMC.ExchangeConfig.exe";
                }
                //Removing "exe" string from ExchangeConfigFileName
                processName = ExchangeConfigFileName.Substring(0, ExchangeConfigFileName.Length - 4);
                Process[] bmcClientConfigProcesses = Process.GetProcessesByName(processName);
                if (bmcClientConfigProcesses != null && bmcClientConfigProcesses.Length > 0) { return; }

                try
                {
                    DirectoryInfo dirInfor = new DirectoryInfo(installationPath);
                    // Get all files whose names starts with FileName Passed 
                    FileInfo[] filesInfo = dirInfor.GetFiles(ExchangeConfigFileName +
                        "*", SearchOption.TopDirectoryOnly);
                    foreach (FileInfo fi in filesInfo)
                    {
                        if (fi.Name.Equals(ExchangeConfigFileName))
                        {
                            LogManager.WriteLog("Path.Combine(installationPath.Trim() , ExchangeConfigFileName): " + Path.Combine(installationPath.Trim(), ExchangeConfigFileName), LogManager.enumLogLevel.Info);
                            isExecutableFound = true;
                            System.Diagnostics.Process.Start(Path.Combine(installationPath.Trim() , ExchangeConfigFileName));
                            break;
                        }
                    }
                    if (!isExecutableFound)
                    {
                        MessageBox.ShowBox("MessageID327", BMC_Icon.Error, BMC_Button.OK, ExchangeConfigFileName, installationPath);
                        LogManager.WriteLog(string.Format("{0} is not available in {1}", ExchangeConfigFileName, installationPath), LogManager.enumLogLevel.Info);
                    }
                    LogManager.WriteLog(string.Format("Launched {0}", ExchangeConfigFileName), LogManager.enumLogLevel.Info);
                }
                catch (Exception SystemConfigLaunchException)
                {
                    ExceptionManager.Publish(SystemConfigLaunchException);
                }
            }
            finally
            {
                btnSystemConfigParameters.IsEnabled = true;
            }
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
                        this.UserControl.Loaded -= (this.UserControl_Loaded);
                        this.btnSystemConfigParameters.Click -= (this.btnSystemConfigParameters_Click);
                    },
                    (c) =>
                    {
                    });
                    LogManager.WriteLog("|=> CSettings objects are released successfully.", LogManager.enumLogLevel.Info);
                }
                disposed = true;
            }
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="CSettings"/> is reclaimed by garbage collection.
        /// </summary>
        ~CSettings()
        {
            Dispose(false);
        }

        #endregion

        /// <summary>
        /// highlight if the setting is changed in backend.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstSettingDetails_LoadingRow(object sender, Microsoft.Windows.Controls.DataGridRowEventArgs e)
        {

            if (e.Row.Item != null)
            {
                DataRowView item = e.Row.Item as DataRowView;

                //check if the row exists
                if (item.Row != null)
                {
                    //if any setting is modified,
                    if (Convert.ToBoolean(item.Row["IsModified"]))
                        e.Row.Background = new SolidColorBrush(Colors.Violet);
                    else
                        e.Row.SetResourceReference(Control.BackgroundProperty, "RowBG");
                }
            }
        }
    }
}
