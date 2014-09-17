using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using BMC.Common.ExceptionManagement;
using BMC.Common.LogManagement;
using System.ComponentModel;
using BMC.Business.ExchangeConfig;
using BMC.CashDeskOperator.BusinessObjects;
using System.IO;
using BMC.Common.Utilities;

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
                LogManager.WriteLog("Entering Cash Desk Operator Client Configuration EXE", LogManager.enumLogLevel.Debug);
                BMCRegistryHelper.ActiveInstallationType = BMCCategorizedInstallationTypes.Exchange;
                LogManager.WriteLog("BMCRegistryHelper.InstallationType is :" + BMCRegistryHelper.InstallationType, LogManager.enumLogLevel.Debug);
                
                CreateLogsFolder();

                LogManager.WriteLog("Inside Application_Startup", LogManager.enumLogLevel.Info);

                DataSet dsInitialSettings = DBSettings.GetInitialSettings(oCommonUtilities.CreateInstance().GetConnectionString());

                if (dsInitialSettings == null)
                {
                    Settings.OnScreenKeyboard = Convert.ToBoolean(ConfigurationManager.AppSettings.Get("OnScreenKeyboard"));
                }
                else
                {
                    Settings.OnScreenKeyboard = dsInitialSettings.Tables[0].Rows[0]["USE_ON_SCREEN_KEYBOARD"].ToString() != string.Empty ?
                        Convert.ToBoolean(dsInitialSettings.Tables[0].Rows[0]["USE_ON_SCREEN_KEYBOARD"].ToString()) :
                        Convert.ToBoolean(ConfigurationManager.AppSettings.Get("OnScreenKeyboard"));
                }
                try
                {
                    Settings.IsLoginRequired = true;
                    if (ConfigurationManager.AppSettings.Get("IsLoginRequired") != null)
                    {
                        Settings.IsLoginRequired = Convert.ToBoolean(ConfigurationManager.AppSettings.Get("IsLoginRequired"));
                    }
                }
                catch (Exception ex)
                {
                    ExceptionManager.Publish(ex);
                }
                if (Settings.IsLoginRequired)
                {
                    StartupUri = new Uri("Login.xaml", UriKind.Relative);
                }
                else
                {
                    StartupUri = new Uri("MainScreen.xaml", UriKind.Relative);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                MessageBox.ShowBox("MessageID33", BMC_Icon.Error);
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
