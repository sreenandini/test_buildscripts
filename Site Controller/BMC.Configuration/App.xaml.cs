using System;
using System.Configuration;
using System.Data;
//using BMC.CashDeskOperator.BusinessObjects;
using System.IO;
using System.Windows;
using BMC.Business.ExchangeConfig;
using BMC.Common.ExceptionManagement;
using BMC.Common.Utilities;
using BMC.Common.LogManagement;

namespace BMC.ExchangeConfig
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        public App()
        {
            InitializeComponent();
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            try
            {
                LogManager.WriteLog("Entering Cash Desk Operator Configuration EXE", LogManager.enumLogLevel.Debug);
                BMCRegistryHelper.ActiveInstallationType = BMCCategorizedInstallationTypes.Exchange;
                LogManager.WriteLog("BMCRegistryHelper.InstallationType is :" + BMCRegistryHelper.InstallationType, LogManager.enumLogLevel.Debug);
                
                CreateLogsFolder();

                LogManager.WriteLog("Inside Application_Startup", LogManager.enumLogLevel.Info);
                string strConnectionString = "";
                try
                {
                    strConnectionString = BMC.Common.Utilities.DatabaseHelper.GetConnectionString();
                }
                catch
                {
                }

                DataSet dsInitialSettings = null;
                if (strConnectionString != "")
                    DBSettings.GetInitialSettings(strConnectionString);

                if (dsInitialSettings == null)
                {
                    Settings.OnScreenKeyboard = Convert.ToBoolean(ConfigurationManager.AppSettings.Get("OnScreenKeyboard"));
                }
                else
                {
                    Settings.OnScreenKeyboard = dsInitialSettings.Tables[0].Rows[0]["USE_ON_SCREEN_KEYBOARD"].ToString() != string.Empty ?
                        Convert.ToBoolean(dsInitialSettings.Tables[0].Rows[0]["USE_ON_SCREEN_KEYBOARD"].ToString()) :
                        Convert.ToBoolean(ConfigurationManager.AppSettings.Get("OnScreenKeyboard"));
                    try
                    {
                        Settings.CopyRightInfo = (dsInitialSettings.Tables[0].Rows[0]["COPYRIGTINFO"] != null) ? Convert.ToString(dsInitialSettings.Tables[0].Rows[0]["COPYRIGTINFO"]) : string.Empty;
                    }
                    catch
                    {
                        Settings.CopyRightInfo = string.Empty;
                    }
                }

                Settings.IsLoginRequired = ConfigurationManager.AppSettings.Get("IsLoginRequired") != string.Empty ?
                    Convert.ToBoolean(ConfigurationManager.AppSettings.Get("IsLoginRequired")) : false;

                if (Settings.IsLoginRequired)
                {
                    StartupUri = new Uri("Login.xaml", UriKind.Relative);
                }
                else
                {
                    StartupUri = new Uri("MainScreen.xaml", UriKind.Relative);
                }

                LogManager.WriteLog("Application_Startup Success", LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                MessageBox.ShowBox("MessageID74", BMC_Icon.Error);
            }
        }

        private void CreateLogsFolder()
        {
            try
            {
                string logPath = ConfigurationManager.AppSettings.Get("DefaultLogPath");

                if (!Directory.Exists(logPath))
                {
                    if (Directory.Exists(logPath.Substring(0, 2)))
                    {
                        Directory.CreateDirectory(logPath);
                    }
                }
            }
            catch (Exception)
            { }
        }
    }
}
