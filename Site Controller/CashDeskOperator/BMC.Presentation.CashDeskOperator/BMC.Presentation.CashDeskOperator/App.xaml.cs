using System;
using System.ComponentModel;
using System.Globalization;
using System.Threading;
using System.Windows;
using BMC.CashDeskOperator.BusinessObjects;
using BMC.Common.ExceptionManagement;
using Microsoft.Win32;
using BMC.Common.LogManagement;
using BMC.Common.Utilities;
using BMC.Transport;
using System.Net;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Remoting.Channels;
using BMCIPC;
using BMC.Common.ConfigurationManagement;
using BMC.CoreLib.WPF.Controls;
using System.Windows.Threading;
using BMCIPC.CDO;
using BMC.CoreLib;

namespace BMC.Presentation
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary> 
    public partial class App : INotifyPropertyChanged
    {

        #region Declarations
        public static ICDOMSMQContract contract = null;
        private readonly oCommonUtilities _oCommonutilities = oCommonUtilities.CreateInstance();
        ResourceDictionary _currentCultureDictionary;
        ResourceDictionary _currentMessageCultureDictionary;
        public static ClientObject clientObj;
        public static Client client;
#if !FLOORVIEW_REFRESH_NEW
        public static ClientObject siteLicensingClientObj;
        public static Client siteLicensingClient;
#endif

        public string Skin2 = "Resources/BMCBlueTheme.xaml";
        public string Skin1 = "Resources/BMCDebugTheme.xaml";

        public static int iInteraval = 15;
        public static int iSiteLicensingCheck = 0;
        #endregion

        #region public Properties

        private string _currentCulture = "en-US";
        public string CurrentCultureName
        {
            get { return _currentCulture; }
            set
            {

                _currentCulture = value;
                LoadCultureStrings(value);

                if (PropertyChanged != null)
                {
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("CurrentCultureName"));
                }
            }
        }
        #endregion

        /// <summary>
        /// Application Entry Point.
        /// </summary>
        [System.STAThreadAttribute()]
        public static void Main()
        {
            LogManager.WriteLog("Entering Cash Desk Operator EXE", LogManager.enumLogLevel.Debug);
            BMCRegistryHelper.ActiveInstallationType = BMCCategorizedInstallationTypes.Exchange;
            LogManager.WriteLog("BMCRegistryHelper.InstallationTypes is :" + BMCRegistryHelper.ActiveInstallationType, LogManager.enumLogLevel.Debug);

            string strDefaultServerIP = string.Empty;
            strDefaultServerIP = BMCRegistryHelper.GetRegKeyValue("Cashmaster\\Exchange", "Default_Server_IP");
            clientObj = new ClientObject();
            client = new Client("tcp://" + strDefaultServerIP + ConfigManager.Read("ServerIPCUrl"), "RemotingClient", typeof(IServerObject));
            client.SendToServer(clientObj, null);

            int itmpInterval = Int32.Parse(ConfigManager.Read("IPCInterval")) * 3;
            Int32.TryParse(ConfigManager.Read("RemoteServerConnectionCheck"), out iSiteLicensingCheck);
            if (iSiteLicensingCheck <= 0)
            {
                iSiteLicensingCheck = 600;
            }
            if (itmpInterval > 15)
            {
                iInteraval = itmpInterval;
            }

#if !FLOORVIEW_REFRESH_NEW
            siteLicensingClientObj = new ClientObject();
            siteLicensingClient = new Client("tcp://" + strDefaultServerIP + ConfigManager.Read("SiteLicensingServerIPCUrl"), "SiteLicensingClient", typeof(ISiteLicenseServerObject));
            siteLicensingClient.SendToServer(siteLicensingClientObj, null);
#endif
            BMC.PlayerGateway.GatewaySettings.ConnectionString = DatabaseHelper.GetConnectionString();
            AppStartUp();
        }

        public static void AppStartUp()
        {
            try
            {
                CDOSettings.LoadChanges(null);
                BMC.Presentation.App app = new BMC.Presentation.App();
                Dispatcher.CurrentDispatcher.UnhandledException += CurrentDispatcher_UnhandledException;
                app.InitializeComponent();
                app.Run();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            finally
            {
            }
        }

        static void CurrentDispatcher_UnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {

        }

        public App()
        {
            InitializeComponent();

            var isDebug = Common.ConfigurationManagement.ConfigManager.Read("isDebug");

            CurrentCultureName = Common.ConfigurationManagement.ConfigManager.Read("GetDefaultCultureForUserLanguage");

            try
            {
                WpfListViewSort.HeaderAscendingTemplate = App.Current.Resources["HeaderArrowUp"] as DataTemplate;
                WpfListViewSort.HeaderDescendingTemplte = App.Current.Resources["HeaderArrowDown"] as DataTemplate;
            }
            catch { }
        }

        internal void LoadCultureStrings(string name)
        {
            ResourceDictionary newCultureDictionary = null;
            ResourceDictionary newMessageCultureDictionary = null;
            try
            {
                newCultureDictionary = LoadComponent(new Uri("/Resources/" + name + ".xaml", UriKind.Relative)) as ResourceDictionary;
                newMessageCultureDictionary = LoadComponent(new Uri("/Resources/Messages_" + name + ".xaml", UriKind.Relative)) as ResourceDictionary;

            }
            catch (Exception exception) { ExceptionManager.Publish(exception); }

            if (newCultureDictionary != null)
            {
                if (_currentCultureDictionary != null)
                    Current.Resources.MergedDictionaries.Remove(_currentCultureDictionary);

                if (_currentMessageCultureDictionary != null)
                    Current.Resources.MergedDictionaries.Remove(_currentMessageCultureDictionary);

                _currentCultureDictionary = newCultureDictionary;
                _currentMessageCultureDictionary = newMessageCultureDictionary;

                this.CurrentUICulture = new CultureInfo(name, false);
                this.CurrentCulture = new CultureInfo(name, false);

                Thread.CurrentThread.CurrentUICulture = this.CurrentUICulture;
                //Change Request #203622 fix.
                //Thread.CurrentThread.CurrentCulture = this.CurrentCulture;               

                Resources.MergedDictionaries.Add(newCultureDictionary);

                if (newMessageCultureDictionary != null)
                    Resources.MergedDictionaries.Add(newMessageCultureDictionary);
            }
        }

        public CultureInfo CurrentUICulture { get; set; }
        public CultureInfo CurrentCulture { get; set; }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            GC.Collect();
            try
            {
                //Removed the Pre- Requisites Check and added it to Splash Screen.
                StartupUri = new Uri("Splash.xaml", UriKind.Relative);

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            App.client.DisconnectServer(App.clientObj);
#if !FLOORVIEW_REFRESH_NEW
            App.siteLicensingClient.DisconnectServer(App.siteLicensingClientObj);
#endif
        }
    }
}
