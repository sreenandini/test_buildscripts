/*******************************************************************************************************
 *  Revision History
 *  Name            TrackCode   Modified Date   Change Description
 *  Selva Kumar S   S001        27th Jul 2012   Commented TITO based validation in Centralized Cashier
 *                                              Transaction
 * ****************************************************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Management;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Forms;
using System.Xml;
using System.Linq;
using Audit.BusinessClasses;
using Audit.Transport;
using BMC.CashDeskOperator.BusinessObjects;
using BMC.Common.ExceptionManagement;
using BMC.Common.LogManagement;
using BMC.Presentation.POS.Views;
using BMC.Security;
using BMC.Transport;
using Application = System.Windows.Application;
using ListBox = System.Windows.Controls.ListBox;
using System.Media;
using System.IO;
using BMC.Presentation.POS.Helper_classes;
using HC = BMC.Presentation.Helper_classes;
using System.Configuration;
using BMC.CashDeskOperator;
using Microsoft.Win32;
using BMCIPC;
using System.Globalization;
using BMC.Common.Utilities;
using BMC.Transport.CashDeskOperatorEntity;
using System.Windows.Media;
using BMCIPC.CDO;
using BMC.CoreLib.Concurrent;
using System.ComponentModel;
using BMC.CoreLib.WPF;

namespace BMC.Presentation
{
    public partial class MainScreen : IDisposable
    {
        private string _sCurrentViewCaption;
        private bool _SiteLicensingExpired = false;
        private string _sUserName;
        static bool _bLogout;
        public string Skin2 = "Resources/BMCBlueTheme.xaml";
        public string Skin1 = "Resources/BMCGreenTheme.xaml";
        private CShortpays _objShortpays;
        private CFactoryReset _objReset;
        private CFactoryResetOption _oFactoryResetOption;
        public static string StrUserName = string.Empty;
        private ResourceDictionary _currentResourceDictionary;
        private bool _isFirstTime = true;
        readonly DataTable _site;
        [DllImport("user32", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        public static extern IntPtr SetParent(IntPtr HWNDChild, IntPtr HWNDNewParent);
        private CFloorView objFloorView = null;
        private Thread _checkExchangeConnectivityThread;
        private static PromotionVouchers objPromotionView = null;
        private static CPrintPromotionalTickets objPrintTickets = null;

        private string _exchangeConnectionString = string.Empty;
        private int _exchangeServerCheckInterval;
        private Action<string> _loadView = null;
        private Action<object, SelectionChangedEventArgs> _listSelectionChanged = null;
        private Action<bool> _enableControls = null;
        private IDictionary<string, ServiceDetail> _serviceDetails = null;
        private DateTime _servicedatatimestamp;
        private ServiceStatus s_status = null;
        private int iMachineDropIndex = 0;
        private int counter = 0;
        private bool LicenseScreenActive { get; set; }
        private System.Windows.Forms.Timer timerLogoff = null;
        private System.Windows.Forms.Timer siteLicensingTimer = null;
        private System.Windows.Media.LinearGradientBrush LiRed = null;
        private System.Windows.Media.LinearGradientBrush LiGreen = null;
        private bool IsMinimizeBtnEnabled = true;

        private IExecutorService _execService = ExecutorServiceFactory.CreateExecutorService();
        public static event PropertyChangedEventHandler BusyPropertyChanged;
        CPrintPromotionalTickets ticket = null;
        PromoPrint print = null;

        private List<XmlElement> lstMenuItemsForAppLauncher = null;
        bool IsApplauncherEnabled = false;


        public UIElement PromotionActiveElement
        {
            get { return (UIElement)GetValue(PromotionActiveElementProperty); }
            set { SetValue(PromotionActiveElementProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PromotionActiveElement.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PromotionActiveElementProperty =
            DependencyProperty.Register("PromotionActiveElement", typeof(UIElement), typeof(MainScreen),
            new UIPropertyMetadata(new PropertyChangedCallback(OnPromotionActiveElementChanged)));

        public static MainScreen ActiveInstance { get; set; }

       
        public MainScreen()
        {
            LogManager.WriteLog("MainScreen() (START)", LogManager.enumLogLevel.Info);
            GC.Collect();           

            InitializeComponent();
            btn_Notifications.Visibility = Visibility.Collapsed;
            ActiveInstance = this;
            MessageBox.parentOwner = this;
            LicenseScreenActive = false;
            CheckAppLaucherStatus();
            _loadView += new Action<string>(this.LoadView);
            _listSelectionChanged += new Action<object, SelectionChangedEventArgs>(this.ListBoxSelectionChanged);
            CPerformDrop.BusyPropertyChanged += new PropertyChangedEventHandler(propChangedEventHandler);
            CDeclaration.BusyPropertyChanged += new PropertyChangedEventHandler(propChangedEventHandler);
            _enableControls += new Action<bool>(this.EnableControls);
            ColorBrush();
            IsMinimizeBtnEnabled = CheckMinimizeBtnEnabled();
            if (!IsMinimizeBtnEnabled)
            {
                btnMinimize.Visibility = Visibility.Collapsed;
            }

#if INBUILD_XML
            Uri xmlUri = new Uri("/XMLData/Menudata.xml", UriKind.Relative);
            if (string.Compare(((App)Application.Current).CurrentCultureName, "en-US", true) != 0)
            {
                xmlUri = new Uri("/XMLData/Menudata_" + ((App)Application.Current).CurrentCultureName + ".XML", UriKind.Relative);
            }
            var xmlDataProvider = new XmlDataProvider
                                      {
                                          Source = xmlUri,
                                          IsAsynchronous = false,
                                          XPath = "/Root/ImageList/Image"
                                      };
#else
            string baseUri = Path.GetDirectoryName(typeof(MainScreen).Assembly.Location);
            string xmlUri = Path.Combine(baseUri, "Menudata.xml");
            if (string.Compare(((App)Application.Current).CurrentCultureName, "en-US", true) != 0)
            {
                xmlUri = Path.Combine(baseUri, "Menudata_" + ((App)Application.Current).CurrentCultureName + ".XML");
            }
            XmlDocument doc = new XmlDocument();
            doc.Load(xmlUri);
            var xmlDataProvider = new XmlDataProvider
                                      {
                                          Document = doc,
                                          IsAsynchronous = false,
                                          XPath = "/Root/ImageList/Image"
                                      };
#endif

            _exchangeConnectionString = oCommonUtilities.CreateInstance().GetConnectionString();

            lstLeftPane.ItemsSource = xmlDataProvider.Data as IEnumerable;
            bool isThemeRequiered = false;

            //Boolean.TryParse(BMC.Common.ConfigurationManagement.ConfigManager.Read("ThemeRequiered"),out isThemeRequiered);
            //if (isThemeRequiered)
            //    btnTheme.Visibility = Visibility.Visible;
            //else
            btnTheme.Visibility = Visibility.Collapsed;

            _bLogout = false;
            lblDate.Visibility = Visibility.Hidden;
            lblwelcome.Visibility = Visibility.Hidden;
            if (oCommonUtilities.CreateInstance().GetSiteDetails() != null)
                _site = oCommonUtilities.CreateInstance().GetSiteDetails().Tables[0];
            if ((_site != null) && (_site.Rows.Count > 0))
            {
                lblPropertyName.Content = _site.Rows[0]["Name"] + " - " + _site.Rows[0]["Code"];
                Settings.SiteCode = _site.Rows[0]["Code"].ToString();
                Settings.SiteName = _site.Rows[0]["Name"].ToString();
                Settings.Site_Address_1 = _site.Rows[0]["Site_Address_1"].ToString();
                Settings.Site_Address_2 = _site.Rows[0]["Site_Address_2"].ToString();
                string str_Header = "";
                if (Settings.PrintHeaderFormat != null && Settings.PrintHeaderFormat != string.Empty)
                {
                    foreach (string s in Settings.PrintHeaderFormat.Split(new string[1] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        if (_site.Rows[0][s] != null)
                        {
                            str_Header += _site.Rows[0][s] + " ";
                        }
                    }
                    str_Header = str_Header.Remove(str_Header.Length - 1, 1);
                    Settings.PrintHeaderFormat = str_Header;
                }
                else
                {
                    Settings.PrintHeaderFormat = Settings.SiteName;
                }

            }

            int nTimeOutSeconds = Settings.AUTOLOGOFF_TIMEOUT;
            timerLogoff = new System.Windows.Forms.Timer { Interval = 1000, Enabled = true };
            timerLogoff.Tick += (sender, e) =>
            {
                if (IdleTime.Milliseconds <= nTimeOutSeconds) return;
                if (PromotionActiveElement != null || CDeclaration.IsBusy || CPerformDrop.IsBusy)
                {
                    return;
                }

                timerLogoff.Stop();
                if (TimerMessageBox.ShowBox() == "NO")
                {
                    LogOffScreen();
                }
                else
                    timerLogoff.Start();
            };

#if! FLOORVIEW_REFRESH_NEW
            App.siteLicensingClient.SendToServer(App.siteLicensingClientObj, null);
            App.siteLicensingClientObj.ClientReceived += new ClientReceivedEvent(clientObj_SiteLicensingClientReceived);
#else
            this.InitClientSubscriber();
#endif
            _serviceDetails = new SortedDictionary<string, ServiceDetail>(StringComparer.InvariantCultureIgnoreCase);
            CheckForLicenseActivation();
            LogManager.WriteLog("Timer Started for AutoLogoff", LogManager.enumLogLevel.Info);
            LogManager.WriteLog("MainScreen() (END)", LogManager.enumLogLevel.Info);



        }

        void CheckAppLaucherStatus()
        {
            try
            {
                if (ConfigurationManager.AppSettings["AppLauncherGrid"].NullToString() == "True")
                {
                    IsApplauncherEnabled = true;
                    btnAppLauncher.Visibility = Visibility.Visible;
                }
            }
            catch (Exception Ex)
            {
                LogManager.WriteLog("Applauncher Disabled", LogManager.enumLogLevel.Debug);
                ExceptionManager.Publish(Ex);
            }
        }

        private void propChangedEventHandler(object sender, PropertyChangedEventArgs e)
        {
            DisableAllControls(sender);
        }

        private void DisableAllControls(object sender)
        {
            lstLeftPane.IsEnabled = !(CDeclaration.IsBusy || CPerformDrop.IsBusy);



        }


        bool promotionalChecker = false;

        private void PromopropChangedEventHandler(object sender, PropertyChangedEventArgs e)
        {
            promotionalChecker = true;
            this.PromotionalChecker();
        }

        private void PromotionalChecker()
        {
            try
            {
                if (!promotionalChecker) return;

                PromoScreen();
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("PromoScreen Error ", LogManager.enumLogLevel.Info);
                ExceptionManager.Publish(ex);
            }
        }

        private static void OnPromotionActiveElementChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            try
            {
                UIElement oldElement = e.OldValue as UIElement;
                UIElement newElement = e.NewValue as UIElement;
                if (oldElement != newElement)
                {
                    (d as MainScreen).PromoScreen();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        public void PromoScreen()
        {
            ApplicationManager.SyncSendSync((o) =>
            {
                UIElement promotionChild = null;
                pnlContent.Children.Clear();
                bool enable = true;

                if (this.PromotionActiveElement != null)
                {
                    enable = false;
                    promotionChild = this.PromotionActiveElement;
                }
                else
                {
                    promotionChild = new PromotionVouchers();
                }

                btnLogout.IsEnabled = enable;
                btnExit.IsEnabled = enable;
                btnShutdown.IsEnabled = enable;
                UIElement parent = LogicalTreeHelper.GetParent(promotionChild) as UIElement;
                if (parent == null)
                {
                    pnlContent.Children.Add(promotionChild);
                }
            }, null);


        }

        private void LogOffScreen()
        {
            var objLogin = new Login();
            objLogin.MainInstance = this;
            Hide();
            objLogin.Show();
            _bLogout = true;

            AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
            {
                AuditModuleName = ModuleName.Logout,
                Audit_Desc = "Logout Successful for User-" + SecurityHelper.CurrentUser.UserName,
                Audit_Screen_Name = "Logout",
                AuditOperationType = OperationType.ADD
            });

            Close();
            objLogin.DisposeMainInstanceIfAny();
        }

#if FLOORVIEW_REFRESH_NEW
        private CDOCentralServerClientSubcriber _clientSubcriber = null;

        private void InitClientSubscriber()
        {
            _clientSubcriber = new CDOCentralServerClientSubcriber(true);
            if (SiteLicensingConfiguration.SiteLicensingConfigurationInstance.IsSiteLincenseEnabled)
            {
                _clientSubcriber.SetSiteLicensingEvent += OnClientSubcriber_SetSiteLicensingEvent;
                _clientSubcriber.SubscribeSetSiteLicensing(_execService, null);
            }
        }

        private void OnClientSubcriber_SetSiteLicensingEvent(SiteLicensingDataResponse response)
        {
            _SiteLicensingExpired = true;
        }

        private void DestroyClientSubscriber()
        {
            if (_clientSubcriber == null) return;

            _execService.Shutdown();
            _clientSubcriber.SetSiteLicensingEvent -= OnClientSubcriber_SetSiteLicensingEvent;
            _clientSubcriber.Dispose();
        }
#endif

        public string UserName
        {
            get
            {
                return _sUserName;
            }
            set
            {
                if (_sUserName != value)
                {
                    _sUserName = value;

                    StrUserName = _sUserName;
                }
                lblUsername.Text = "    , " + SecurityHelper.CurrentUser.DisplayName + "!";
                lblUsername.ToolTip = FindResource("MainScreen_Welcome").ToString() + lblUsername.Text.Trim();
            }
        }

        private void EnableControls(bool enable)
        {
            lstLeftPane.IsHitTestVisible = enable;
            this.Cursor = (enable ? System.Windows.Input.Cursors.Arrow : System.Windows.Input.Cursors.Wait);
        }

        private void ColorBrush()
        {
            System.Windows.Media.GradientStop[] gtRed = { new System.Windows.Media.GradientStop { Color = System.Windows.Media.Colors.Red, Offset = 0.0 }, new System.Windows.Media.GradientStop { Color = System.Windows.Media.Colors.DarkRed, Offset = 0.9 } };

            LiRed = new System.Windows.Media.LinearGradientBrush(new System.Windows.Media.GradientStopCollection(gtRed.AsEnumerable<System.Windows.Media.GradientStop>()), new System.Windows.Point(0, 0), new System.Windows.Point(1, 1));

            System.Windows.Media.GradientStop[] gtGreen = { new System.Windows.Media.GradientStop { Color = System.Windows.Media.Colors.YellowGreen, Offset = 0.0 }, new System.Windows.Media.GradientStop { Color = System.Windows.Media.Colors.Green, Offset = 0.9 } };

            LiGreen = new System.Windows.Media.LinearGradientBrush(new System.Windows.Media.GradientStopCollection(gtGreen.AsEnumerable<System.Windows.Media.GradientStop>()), new System.Windows.Point(0, 0), new System.Windows.Point(1, 1));
            btnServices.Background = LiGreen;
            lblDatabase.Background = LiGreen;

        }

        #region To retrive the site status either from local machine or remote machine
        public bool IsServiceActive()
        {
            try
            {
                //Get service names from DB
                FactoryResetMethods objFactoryResetMethods = FactoryResetMethods.FactoryResetMethodsInstance;
                var strListarray = objFactoryResetMethods.GetSettingValue("ServiceNames").Split(',');
                this.FillServiceStatusFromXML(BMC.Business.CashDeskOperator.CommonUtilities.GetSiteServiceStatus());
                Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(delegate()
                       {
                           if (s_status != null)
                           {
                               s_status.LoadServicesDetails(_serviceDetails, true, true);
                           }
                       }));

                if ((DateTime.Now - _servicedatatimestamp).Minutes > Settings.ServiceNotRunningInterval)
                {
                    if (_serviceDetails["BMCGuardianService"] != null)
                    {
                        _serviceDetails["BMCGuardianService"].ServiceStatus = "Stopped";
                    }
                    Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(delegate()
                    {
                        if (s_status != null)
                        {
                            s_status.LoadServicesDetails(_serviceDetails, true, false);
                        }
                    }));
                    return false;
                }

                //loop through services one by one and check the status
                foreach (var service in strListarray)
                {
                    if (_serviceDetails.ContainsKey(service))
                    {
                        string strServiceStatus = _serviceDetails[service].ServiceStatus;
                        if (string.Compare(strServiceStatus, "Running", true) != 0)
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("IsServiceActive::" + ex.Message, LogManager.enumLogLevel.Error);
                return false;
            }

        }

        public class ServiceDetail : System.ComponentModel.INotifyPropertyChanged
        {
            private string _ServiceStatus;
            private System.Windows.Media.SolidColorBrush _ForeColor;
            public int SNo { get; set; }
            public string ServiceName { get; set; }
            public static DateTime? LastUpdateDateTime { get; set; }

            public string ServiceStatus
            {
                get
                {
                    return _ServiceStatus;
                }
                set
                {
                    _ServiceStatus = value;
                    if (PropertyChanged != null)
                        PropertyChanged.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs("ServiceStatus"));
                }
            }

            public System.Windows.Media.SolidColorBrush ForeColor { get; set; }

            public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        }


        public void FillServiceStatusFromXML(string siteStatus)
        {
            _serviceDetails.Clear();

            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(siteStatus);

                if (xmlDocument.DocumentElement.HasChildNodes)
                {
                    XmlNode node = xmlDocument.SelectSingleNode("Site/DocumentElement");
                    if ((node != null) && node.HasChildNodes)
                    {
                        foreach (XmlNode node2 in node.ChildNodes)
                        {
                            try
                            {
                                string serviceName = node2["ServiceName"].InnerText.ToString();
                                string serviceStatus = node2["Status"].InnerText.ToString();

                                if (!_serviceDetails.ContainsKey(serviceName))
                                {
                                    ServiceDetail row = new ServiceDetail();
                                    row.ServiceName = serviceName;
                                    row.ServiceStatus = serviceStatus;
                                    row.ForeColor = null;
                                    _serviceDetails.Add(serviceName, row);
                                }
                                else
                                {
                                    _serviceDetails[serviceName].ServiceStatus = serviceStatus;
                                }
                            }
                            catch (Exception ex) { ExceptionManager.Publish(ex); }
                        }
                    }
                    XmlNode node1 = xmlDocument.SelectSingleNode("Site/Status/DateTime");
                    if (node1 != null && node1.InnerText != null && node1.InnerText.Length > 0)
                    {
                        _servicedatatimestamp = DateTime.ParseExact(node1.InnerText, "dd-MM-yyyy hh:mm:ss tt", System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat);
                        ServiceDetail.LastUpdateDateTime = _servicedatatimestamp;
                    }
                }
            }
            catch (Exception exception)
            {
                LogManager.WriteLog("FillServiceStatusFromXML::" + exception.Message, LogManager.enumLogLevel.Error);
            }
        }

        #endregion

        public void LoadFlooView()
        {
            try
            {
                lstLeftPane.SelectedIndex = 0;
                lstLeftPane.ScrollIntoView(lstLeftPane.SelectedItem);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void LoadView(string viewCaption)
        {
            //CollectionHelper.SetCurrentExchangeConnectionString();
            //CollectionHelper.SetCurrentTicketConnectionString();
            //

            #region -S001 START
            /*if (viewCaption=="Tickets" )
                {
                    if (!IsTitoEnabled())
                    {
                        MessageBox.ShowBox("MessageID372", "");
                        return;
                    }
                    
                }*/
            #endregion -S001 END

            if (viewCaption != _sCurrentViewCaption)
            {
                if (objFloorView == null)
                {
                    objFloorView = new CFloorView();
                }
                this.DisposeChildren(true);

                switch (viewCaption)
                {
                    case "Floor View":
                        {
                            pnlContent.Children.Add(objFloorView);
                            objFloorView.Margin = new Thickness(0);
                            break;
                        }
                    case "Grid View":
                        {
                            var objGridView = new CGridView();
                            pnlContent.Children.Add(objGridView);
                            objGridView.Margin = new Thickness(0);
                            //objFloorView.SetFloorStatus(false);
                            break;
                        }
                    case "Tickets":
                        {
                            var objTicketsView = new CTickets();
                            pnlContent.Children.Add(objTicketsView);
                            objTicketsView.Margin = new Thickness(0);
                            break;
                        }
                    case "PromotionalTickets":
                        {
                            PromoScreen();
                            break;
                        }

                    case "CurrentStatus":
                        {
                            var objCurrentStatus = new CPositionCurrentStatus();
                            pnlContent.Children.Add(objCurrentStatus);
                            objCurrentStatus.Margin = new Thickness(0);
                            break;
                        }
                    case "Void":
                        {
                            var objVoid = new CVoid();
                            pnlContent.Children.Add(objVoid);
                            objVoid.Margin = new Thickness(0);
                            break;
                        }
                    case "Reports":
                        {
                            var objAnalysis = new CAnalysis();
                            pnlContent.Children.Add(objAnalysis);
                            objAnalysis.Margin = new Thickness(0);
                            break;
                        }
                    case "Player Club":
                        {
                            var objPlayerClub = new CPlayerClub();
                            pnlContent.Children.Add(objPlayerClub);
                            objPlayerClub.Margin = new Thickness(0);
                            break;
                        }
                    case "Hourly":
                        {
                            var objHourly = new cHourly();
                            pnlContent.Children.Add(objHourly);
                            objHourly.Margin = new Thickness(0);
                            break;
                        }
                    case "ShortPay":
                        {
                            _objShortpays = new CShortpays();
                            pnlContent.Children.Add(_objShortpays);
                            _objShortpays.Margin = new Thickness(0);
                            break;
                        }
                    case "SiteInterrogation":
                        {
                            UCSiteIntegorration objAnalysisDetails = new UCSiteIntegorration(2, DateTime.Now, DateTime.Now, 0);
                            pnlContent.Children.Add(objAnalysisDetails);
                            objAnalysisDetails.Margin = new Thickness(0);
                            break;
                        }
                    case "Current Calls":
                        {
                            var objCurrentCalls = new CCurrentCalls();
                            pnlContent.Children.Add(objCurrentCalls);
                            objCurrentCalls.Margin = new Thickness(0);
                            break;
                        }
                    case "Machine Drop":
                        {
                            var drop = new CDrop();
                            pnlContent.Children.Add(drop);
                            drop.Margin = new Thickness(0);
                            break;
                        }
                    case "Cashier\rTransactions":
                        {
                            var objCashDeskManager = new CCashDeskManager(pnlContent);
                            pnlContent.Children.Add(objCashDeskManager);
                            objCashDeskManager.Margin = new Thickness(0);
                            break;
                        }
                    case "Settings":
                        {
                            var objSettings = new CSettings();
                            pnlContent.Children.Add(objSettings);
                            objSettings.Margin = new Thickness(0);
                            break;
                        }

                    case "AFTSettings":
                        {
                            var objAFTSettings = new CAFTSetting();
                            pnlContent.Children.Add(objAFTSettings);
                            objAFTSettings.Margin = new Thickness(0);
                            break;
                        }
                    case "Audit":
                        {
                            var oAuditView = new CAuditView();
                            pnlContent.Children.Add(oAuditView);
                            oAuditView.Margin = new Thickness(0);
                            break;
                        }
                    case "CustomReports":
                        {
                            var oCustom = new CCustomReports();
                            pnlContent.Children.Add(oCustom);
                            oCustom.Margin = new Thickness(0);
                            break;
                        }
                    case "Details":
                        {
                            var oCustom = new InstallationDetails();
                            pnlContent.Children.Add(oCustom);
                            oCustom.Margin = new Thickness(0);
                            break;

                        }
                    case "SyncTicket":
                        {
                            var oSyncTicket = new SyncTicket();
                            pnlContent.Children.Add(oSyncTicket);
                            oSyncTicket.Margin = new Thickness(0);
                            break;

                        }
                    case "SyncAft":
                        {
                            var oSyncAft = new SyncAft();
                            pnlContent.Children.Add(oSyncAft);
                            oSyncAft.Margin = new Thickness(0);
                            break;

                        }

                    case "SyncEmpCard":
                        {
                            var oSyncEmpCard = new EmployeeCardSync();
                            pnlContent.Children.Add(oSyncEmpCard);
                            oSyncEmpCard.Margin = new Thickness(0);
                            break;

                        }

                    case "Updategmupin":
                        {
                            var oUpdateGMIpin = new UpdateGMIpin();
                            pnlContent.Children.Add(oUpdateGMIpin);
                            oUpdateGMIpin.Margin = new Thickness(0);
                            break;
                        }

                    case "CoinDispenser":
                        {
                            if (Settings.CashDispenserEnabled)
                            {
                                var oCoinDispenser = new CCoinDispenser(this);
                                pnlContent.Children.Add(oCoinDispenser);
                                oCoinDispenser.Margin = new Thickness(0);
                            }
                            break;
                        }
                    case "PlayerInformation":
                        {

                            var oCPlayerData = new CPlayerData();
                            pnlContent.Children.Add(oCPlayerData);
                            oCPlayerData.Margin = new Thickness(0);
                            break;
                        }
                    case "ExceptionVouchers":
                        {
                            var objTicketsView = new CExceptionVoucher();
                            pnlContent.Children.Add(objTicketsView);
                            objTicketsView.Margin = new Thickness(0);
                            break;
                        }
                    case "CrossTicketingSettings":
                        {
                            var objCrossTicketingView = new CrossTicketingSettings();
                            pnlContent.Children.Add(objCrossTicketingView);
                            objCrossTicketingView.Margin = new Thickness(0);
                            break;
                        }
                    case "ReadBasedLiquidation":
                        {
                            var objReadLiquidationView = new CReadLiquidationMain();
                            pnlContent.Children.Add(objReadLiquidationView);
                            objReadLiquidationView.Margin = new Thickness(0);
                            break;
                        }
                    case "Unlock":
                        {
                            var CUnlock = new CUnlock();
                            pnlContent.Children.Add(CUnlock);
                            CUnlock.Margin = new Thickness(0);
                            break;
                        }

                    case "FillVault":
                        {
                            var objCVaultDetails = new CVault();
                            pnlContent.Children.Add(objCVaultDetails);
                            objCVaultDetails.Margin = new Thickness(0);
                            break;
                        }
                    case "NGAEnrol":
                        {
                            var objCVaultDetails = new CNGAEnroll();
                            pnlContent.Children.Add(objCVaultDetails);
                            objCVaultDetails.Margin = new Thickness(0);
                            break;
                        }
                    case "ExportDetails":
                        {
                            var oExportDetails = new CExportDetails();
                            pnlContent.Children.Add(oExportDetails);
                            oExportDetails.Margin = new Thickness(0);
                            break;
                        }
                    case "SpotCheck":
                        {
                            var objSpotCheckView = new CSpotCheck();
                            pnlContent.Children.Add(objSpotCheckView);
                            objSpotCheckView.Margin = new Thickness(0);
                            break;
                        }
                    case "UpdateGMUNo":
                        {
                            CGMUNoUpdate CGMUNo = new CGMUNoUpdate();
                            pnlContent.Children.Add(CGMUNo);
                            CGMUNo.Margin = new Thickness(0);
                            break;
                        }
                    case "GMUPing":
                        {
                            CPingGMU cPingGMU = new CPingGMU();
                            pnlContent.Children.Add(cPingGMU);
                            cPingGMU.Margin = new Thickness(0);
                            break;
                        }

                    case "MachineEnableDisable":
                        {
                            CMachineEnableDisable CMachine = new CMachineEnableDisable();
                            pnlContent.Children.Add(CMachine);
                            CMachine.Margin = new Thickness(0);
                            break;
                        }
                    case "GameCapping":
                        {
                            GameCapping CGameCapping = new GameCapping();
                            pnlContent.Children.Add(CGameCapping);
                            CGameCapping.Margin = new Thickness(0);
                            break;
                        }

                }

                if (pnlContent.Children.Count > 0)
                {
                    UIElement child = pnlContent.Children[0] as UIElement;
                    if (child != null)
                    {
                        child.SetMessageBoxOwner();
                    }
                }

                _sCurrentViewCaption = viewCaption;
            }

        }

        private bool GetPlayerInforEnabled()
        {
            try
            {
                return IsExchangeServer() && ((ConfigurationManager.AppSettings["IsPlayerRatingEnabled"].ToString() == "0") ? false : true);
            }
            catch
            {
                return false;
            }
        }

        public bool IsExchangeServer()
        {
            try
            {
                return BMCRegistryHelper.IsExchangeServer();
                //var regKeyConnectionString = BMCRegistryHelper.GetRegLocalMachine().OpenSubKey("Software\\Honeyframe");

                //if (regKeyConnectionString != null)
                //{
                //    return (string.Compare(regKeyConnectionString.GetValue("InstallationType").ToString(), "ExchangeServer") == 0);
                //}
                //else
                //{
                //    return false;
                //}
            }
            catch
            {
                return false;
            }
        }

        private void ObjFloorViewExitClicked(object sender, EventArgs e)
        {
            /* 
             * Automatically opens the Drop screen to perform Final collection 
             * on clicking the position button when base denom 
             * or Percentage payout changes
            */
            pnlContent.Children.Clear();
            pnlContent.Children.Remove(sender as UIElement);
            lstLeftPane.SelectedIndex = iMachineDropIndex;
            objFloorView.Exit -= ObjFloorViewExitClicked;
        }
        //
        private void ListBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                this.EnableControls(false);
                if (_isFirstTime && SecurityHelper.CurrentUser.SecurityUserID != 0)
                {
                    CheckSecurity();
                    _isFirstTime = false;
                    _sCurrentViewCaption = "";
                }

                string viewCaption = ((XmlElement)((ListBox)sender).SelectedItem).Attributes["ValueName"].Value;
                ThreadPool.QueueUserWorkItem((o) =>
                {
                    this.LoadViewAsync((string)o);
                }, viewCaption);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        void LoadViewExternal(string Caption)
        {
            ThreadPool.QueueUserWorkItem((o) =>
            {
                this.LoadViewAsync((string)o);
            }, Caption);
        }
        //
        private void LoadViewAsync(string viewCaption)
        {
            try
            {
                this.Dispatcher.BeginInvoke(_loadView, System.Windows.Threading.DispatcherPriority.DataBind, new object[] { viewCaption });
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.ShowBox("MessageID334", BMC_Icon.Information, BMC_Button.YesNo) == System.Windows.Forms.DialogResult.No)
                return;
            else
            {
                this.DisposeFloorView();
                Thread.Sleep(200);
                Application.Current.Shutdown();
            }
        }

        //private static bool isBusy;
        //public static bool IsBusy
        //{
        //    get
        //    { return isBusy; }
        //    set
        //    {
        //        isBusy = value;
        //        OnPropertyChanged("IsBusy");
        //    }
        //}

        //// Create the OnPropertyChanged method to raise the event 
        //public static void OnPropertyChanged(string name)
        //{
        //    PropertyChangedEventHandler handler = BusyPropertyChanged;
        //    if (handler != null)
        //    {
        //        handler(new MainScreen(), new PropertyChangedEventArgs(name));
        //    }
        //}



        private void lstLeftPane_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //
            if (CDeclaration.IsBusy || CPerformDrop.IsBusy)
            {
                MessageBox.ShowBox("MessageID368", BMC_Icon.Information);
                e.Handled = false;
                return;
            }
            //
            if (_isFirstTime && SecurityHelper.CurrentUser.SecurityUserID != 0)
            {
                CheckSecurity();
                _isFirstTime = false;
                _sCurrentViewCaption = "";
            }
            LoadView(((XmlElement)((ListBox)sender).SelectedItem).Attributes["ValueName"].Value);

        }

        private void btnLogout_PreviewMouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

            this.DisposeFloorView();
            var objLogin = new Login();
            objLogin.MainInstance = this;
            objLogin.Show();
            _bLogout = true;

            AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
            {
                AuditModuleName = ModuleName.Logout,
                Audit_Desc = "Logout Successful for User-" + SecurityHelper.CurrentUser.UserName,
                Audit_Screen_Name = "Logout",
                AuditOperationType = OperationType.ADD
            });

            Close();
            objLogin.DisposeMainInstanceIfAny();
        }

        private void btnMinimize_PreviewMouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        protected override void OnClosed(EventArgs e)
        {
            try
            {
                if (!_bLogout)
                {
                    this.DisposeFloorView();
                    Thread.Sleep(200);
                    App.Current.Shutdown();

                }
            }
            catch
            {
            }
        }

        private void btnTheme_PreviewMouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var newSkin = new ResourceDictionary { Source = new Uri(Skin1, UriKind.Relative) };

            if (_currentResourceDictionary != null)
            {
                App.Current.Resources.MergedDictionaries.Remove(_currentResourceDictionary);
            }
            _currentResourceDictionary = newSkin;

            App.Current.Resources.MergedDictionaries.Add(_currentResourceDictionary);

            var oldSkin = Skin1;
            Skin1 = Skin2;
            Skin2 = oldSkin;

        }

        private void btnReset_PreviewMouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var dr = MessageBox.ShowBox("MessageID10", BMC_Icon.Question, BMC_Button.YesNo);

            if (dr.ToString().ToUpper() == "NO") return;

            if (_oFactoryResetOption != null)
                _oFactoryResetOption = null;

            _oFactoryResetOption = new CFactoryResetOption();

            _oFactoryResetOption.ShowDialogEx(this);

        }

        private void btnShutdown_PreviewMouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (MessageBox.ShowBox("MessageID12", BMC_Icon.Warning, BMC_Button.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                var mcWin32 = new ManagementClass("Win32_OperatingSystem");
                mcWin32.Get();
                mcWin32.Scope.Options.EnablePrivileges = true;

                AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
                {

                    AuditModuleName = ModuleName.Shutdown,
                    Audit_Screen_Name = "MainScreen",
                    Audit_Desc = "System Shutdown by User-" + SecurityHelper.CurrentUser.UserName,
                    AuditOperationType = OperationType.ADD
                });

                ManagementBaseObject mboShutdownParams = mcWin32.GetMethodParameters("Win32Shutdown");
                // Flag 1 means we want to shut down the system
                mboShutdownParams["Flags"] = "1";
                mboShutdownParams["Reserved"] = "0";
                foreach (ManagementObject manObj in mcWin32.GetInstances())
                {
                    manObj.InvokeMethod("Win32Shutdown", mboShutdownParams, null);
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            lblDate.Text = DateTime.Now.ToString("dd-MMM-yyyy HH:mm:ss");

            if (IsExchangeServer())
            {
                if (!SecurityHelper.HasAccess("CashdeskOperator.MainScreen.cs.Factory"))
                    btnReset.Visibility = Visibility.Collapsed;
                else
                    btnReset.Visibility = Visibility.Visible;
            }
            else
            {
                btnReset.Visibility = Visibility.Collapsed;
            }

            try
            {
                if (Settings.CheckExchangeServerConnectivity)
                {
                    LogManager.WriteLog("CheckExchangeServerConnectivity setting enabled to True",
                                        LogManager.enumLogLevel.Info);

                    _exchangeConnectionString = oCommonUtilities.CreateInstance().GetConnectionString();

                    _exchangeServerCheckInterval = Settings.ExchangeServerConnectivityCheckInterval * 1000;

                    LogManager.WriteLog(
                        string.Format("{0} - {1}", "ExchangeServerConnectivityCheckInterval (in seconds)",
                                      Settings.ExchangeServerConnectivityCheckInterval), LogManager.enumLogLevel.Info);

                    LogManager.WriteLog("Spawning CheckExchangeConnectivityThread...", LogManager.enumLogLevel.Info);

                    if (_exchangeConnectionString != string.Empty &&
                        _exchangeConnectionString.ToUpper().Contains("SERVER"))
                    {
                        _checkExchangeConnectivityThread = new Thread(CheckExchangeConnectivity);
                        _checkExchangeConnectivityThread.Start();

                        LogManager.WriteLog("CheckExchangeConnectivityThread spawned successfully",
                                            LogManager.enumLogLevel.Info);
                    }
                    else
                        LogManager.WriteLog(
                            "Exchange Connection String Empty.CheckExchangeConnectivityThread could not be spawned",
                            LogManager.enumLogLevel.Error);
                }
                else
                    LogManager.WriteLog("CheckExchangeServerConnectivity setting enabled to False",
                                        LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Spawning CheckExchangeConnectivityThread failed", LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
            }
        }

        private void btnLicenseActivation_PreviewMouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            LicenseActivation objLicenseActivation = new LicenseActivation();
            objLicenseActivation.ShowDialogEx(this);
        }

        private void CheckForLicenseActivation()
        {
            try
            {
                SiteLicensingConfiguration oSiteLicensingConfiguration = SiteLicensingConfiguration.SiteLicensingConfigurationInstance;
                List<rsp_SL_GetSiteLicenseDetailsResult> siteLicenseDetailsResultList = new List<rsp_SL_GetSiteLicenseDetailsResult>();
                if (oSiteLicensingConfiguration.IsSiteLincenseEnabled && (SecurityHelper.HasAccess("CashdeskOperator.MainScreen.cs.LicenseActivation")))
                {
                    btnLicenseActivation.Visibility = Visibility.Visible;
                    lblSpace.Visibility = Visibility.Visible;
                }
                else
                {
                    btnLicenseActivation.Visibility = Visibility.Collapsed;
                    lblSpace.Visibility = Visibility.Collapsed;
                }
                if (oSiteLicensingConfiguration.IsSiteLincenseEnabled)
                {
                    siteLicensingTimer = new System.Windows.Forms.Timer { Interval = 1000, Enabled = true };
                    siteLicensingTimer.Tick += SiteLicensingChecker;
                }

            }
            catch (Exception Ex)
            {
                LogManager.WriteLog("CheckForLicenseActivation: [Error] " + Ex.Message, LogManager.enumLogLevel.Error);
            }
        }


        private void CheckSecurity()
        {
            if (SecurityHelper.CurrentUser.SecurityUserID == 0)
                return;
            var items = new List<XmlElement>();
            foreach (var item in lstLeftPane.Items)
            {


                switch (((XmlElement)item).Attributes["ValueName"].Value)
                {
                    case "Floor View":
                        if (SecurityHelper.HasAccess("CashdeskOperator.MainScreen.cs.Floor.View"))
                            if (((XmlElement)item).Attributes["Visible"].Value.ToString().ToUpper() == "Y")
                                items.Add((XmlElement)item);
                        break;
                    case "Grid View":
                        if (SecurityHelper.HasAccess("CashdeskOperator.MainScreen.cs.Grid.View"))
                            if (((XmlElement)item).Attributes["Visible"].Value.ToString().ToUpper() == "Y")
                                items.Add((XmlElement)item);
                        break;
                    case "Details":
                        if (SecurityHelper.HasAccess("BMC.Presentation.POS.Views.InstallationDetails"))
                            if (((XmlElement)item).Attributes["Visible"].Value.ToUpper() == "Y")
                                items.Add((XmlElement)item);
                        break;
                    case "Tickets":
                        //if (!Settings.CAGE_ENABLED)
                        //{
                        if (SecurityHelper.HasAccess("CashdeskOperator.MainScreen.cs.Tickets") || (SecurityHelper.HasAccess("CashdeskOperator.MainScreen.cs.CVoidTicket"))
                            || (SecurityHelper.HasAccess("CashdeskOperator.MainScreen.cs.ManualAttendantPay"))
                            || (SecurityHelper.HasAccess("CashdeskOperator.MainScreen.cs.AttendantPay"))
                            || (SecurityHelper.HasAccess("BMC.Presentation.CRedeemTicket"))
                            || (SecurityHelper.HasAccess("BMC.Presentation.CPrintTicket"))
                            || (SecurityHelper.HasAccess("BMC.Presentation.MultipleVoucher")))
                        {
                            DataTable dtSite = null;
                            if (oCommonUtilities.CreateInstance().GetSiteDetails() != null)
                                dtSite = oCommonUtilities.CreateInstance().GetSiteDetails().Tables[0];
                            if ((dtSite != null) && (dtSite.Rows.Count > 0) && dtSite.Rows[0]["IsTITOEnabled"] != DBNull.Value)
                            {
                                //-S001 START
                                //int intIsTITOEnabled = Convert.ToInt32(dtSite.Rows[0]["IsTITOEnabled"]);
                                //if (intIsTITOEnabled == 1)
                                //-S001 END
                                {
                                    if (Settings.CAGE_ENABLED)
                                    {
                                        if ((!SecurityHelper.HasAccess("CashdeskOperator.MainScreen.cs.AttendantPay")) && (!SecurityHelper.HasAccess("CashdeskOperator.MainScreen.cs.ManualAttendantPay"))) continue;
                                    }
                                    if (((XmlElement)item).Attributes["Visible"].Value.ToUpper() == "Y")
                                        items.Add((XmlElement)item);
                                }

                            }
                        }

                        // }
                        break;
                    case "PromotionalTickets":

                        if ((Settings.IsPromotionalTicketEnabled) && (SecurityHelper.HasAccess("BMC.Presentation.Promotions")))
                        {
                            if ((SecurityHelper.HasAccess("BMC.Presentation.Promotions.Print")) || (SecurityHelper.HasAccess("BMC.Presentation.Promotions.Void")) || (SecurityHelper.HasAccess("BMC.Presentation.Promotions.History")) || (((SecurityHelper.HasAccess("BMC.Presentation.Promotions.TIS")) && (Settings.IsTISEnabled))))
                            {
                                if (((XmlElement)item).Attributes["Visible"].Value.ToUpper() == "Y")
                                    items.Add((XmlElement)item);
                            }
                        }
                        break;

                    case "CurrentStatus":
                        if (
                            SecurityHelper.HasAccess("CashdeskOperator.MainScreen.cs.PositionCurrentStatus") &&
                            (Settings.RegulatoryEnabled && Settings.RegulatoryType == "AAMS"))
                            if (((XmlElement)item).Attributes["Visible"].Value.ToUpper() == "Y")
                                items.Add((XmlElement)item);
                        break;
                    case "Void":
                        if (SecurityHelper.HasAccess("CashdeskOperator.MainScreen.cs.Void"))
                            if (((XmlElement)item).Attributes["Visible"].Value.ToUpper() == "Y")
                                items.Add((XmlElement)item);
                        break;
                    case "Reports":
                        if (SecurityHelper.HasAccess("CashdeskOperator.MainScreen.cs.Reports"))
                            if (((XmlElement)item).Attributes["Visible"].Value.ToUpper() == "Y")
                                items.Add((XmlElement)item);
                        break;
                    case "Player Club":
                        if (SecurityHelper.HasAccess("CashdeskOperator.MainScreen.cs.Player.Club"))
                            if (((XmlElement)item).Attributes["Visible"].Value.ToUpper() == "Y")
                                items.Add((XmlElement)item);
                        break;
                    case "Hourly":
                        if (SecurityHelper.HasAccess("CashdeskOperator.MainScreen.cs.Hourly"))
                            if (((XmlElement)item).Attributes["Visible"].Value.ToUpper() == "Y")
                                items.Add((XmlElement)item);
                        break;
                    case "ShortPay":
                        {
                            if (Settings.ShortPayEnabled)
                            {
                                if (SecurityHelper.HasAccess("CashdeskOperator.MainScreen.cs.Shortpay"))
                                    if (((XmlElement)item).Attributes["Visible"].Value.ToUpper() == "Y")
                                        items.Add((XmlElement)item);
                            }
                        }
                        break;
                    case "SiteInterrogation":
                        if (SecurityHelper.HasAccess("CashdeskOperator.MainScreen.cs.SiteInterrogation"))
                            if (((XmlElement)item).Attributes["Visible"].Value.ToUpper() == "Y")
                                items.Add((XmlElement)item);
                        break;
                    case "Current Calls":
                        if (SecurityHelper.HasAccess("CashdeskOperator.MainScreen.cs.Current.Calls"))
                            if (((XmlElement)item).Attributes["Visible"].Value.ToUpper() == "Y")
                                items.Add((XmlElement)item);
                        break;
                    case "Cashier\rTransactions":
                        if (SecurityHelper.HasAccess("CashdeskOperator.MainScreen.cs.CashDeskMananger"))
                            if (((XmlElement)item).Attributes["Visible"].Value.ToUpper() == "Y")
                                items.Add((XmlElement)item);
                        break;
                    case "Machine Drop":
                        if (SecurityHelper.HasAccess("CashdeskOperator.MainScreen.cs.Machine Drop"))
                            if (((XmlElement)item).Attributes["Visible"].Value.ToUpper() == "Y")
                                items.Add((XmlElement)item);
                        iMachineDropIndex = items.Count - 1;
                        break;
                    case "Settings":
                        if (SecurityHelper.HasAccess("CashdeskOperator.MainScreen.cs.Site Settings"))
                            if (((XmlElement)item).Attributes["Visible"].Value.ToUpper() == "Y")
                                items.Add((XmlElement)item);
                        break;
                    case "Audit":
                        if (SecurityHelper.HasAccess("CashdeskOperator.MainScreen.cs.Audit"))
                            if (((XmlElement)item).Attributes["Visible"].Value.ToUpper() == "Y")
                                items.Add((XmlElement)item);
                        break;
                    case "CustomReports":
                        if (SecurityHelper.HasAccess("CashdeskOperator.MainScreen.cs.CustomReports"))
                            if (((XmlElement)item).Attributes["Visible"].Value.ToUpper() == "Y")
                                items.Add((XmlElement)item);
                        break;
                    case "AFTSettings":
                        if (Settings.IsAFTEnabledForSite)
                        {
                            if (SecurityHelper.HasAccess("CashdeskOperator.MainScreen.cs.AFTSettings"))
                                if (((XmlElement)item).Attributes["Visible"].Value.ToUpper() == "Y")
                                    items.Add((XmlElement)item);
                        }
                        break;
                    case "SyncTicket":
                        if (SecurityHelper.HasAccess("CashdeskOperator.MainScreen.cs.VoucherConfig"))
                            if (((XmlElement)item).Attributes["Visible"].Value.ToUpper() == "Y")
                                items.Add((XmlElement)item);
                        break;
                    case "SyncAft":
                        if (Settings.IsAFTEnabledForSite)
                        {
                            if (SecurityHelper.HasAccess("CashdeskOperator.MainScreen.cs.AFTEnableDisable"))
                                if (((XmlElement)item).Attributes["Visible"].Value.ToUpper() == "Y")
                                    items.Add((XmlElement)item);
                        }
                        break;
                    ///////////////////////
                    case "Updategmupin":
                        if (SecurityHelper.HasAccess("CashdeskOperator.MainScreen.cs.Gmuupdatebin"))
                            if (((XmlElement)item).Attributes["Visible"].Value.ToUpper() == "Y")
                                items.Add((XmlElement)item);
                        break;
                    ///////////////////////////
                    case "CoinDispenser":
                        if (Settings.CashDispenserEnabled)
                        {
                            if (SecurityHelper.HasAccess("CashdeskOperator.MainScreen.cs.CoinDispenser"))
                                if (((XmlElement)item).Attributes["Visible"].Value.ToUpper() == "Y")
                                    items.Add((XmlElement)item);
                        }
                        break;
                    case "ExceptionVouchers":
                        if (Settings.HANDLE_EXCEPTION_PP_TICKETS)
                            if (SecurityHelper.HasAccess("BMC.Presentation.CPpTicket"))
                                if (((XmlElement)item).Attributes["Visible"].Value.ToUpper() == "Y")
                                    items.Add((XmlElement)item);
                        break;
                    case "PlayerInformation":
                        {
                            if (GetPlayerInforEnabled())
                            {
                                if (((XmlElement)item).Attributes["Visible"].Value.ToUpper() == "Y")
                                    items.Add((XmlElement)item);
                            }
                            break;
                        }

                    case "SyncEmpCard":
                        if (SecurityHelper.HasAccess("CashdeskOperator.MainScreen.cs.SyncEmpCard"))
                            if (((XmlElement)item).Attributes["Visible"].Value.ToUpper() == "Y")
                                items.Add((XmlElement)item);
                        break;
                    case "CrossTicketingSettings":
                        if (SecurityHelper.HasAccess("BMC.Presentation.CrossTicketingSettings"))
                            if (((XmlElement)item).Attributes["Visible"].Value.ToUpper() == "Y")
                                items.Add((XmlElement)item);
                        break;
                    case "ReadBasedLiquidation":
                        if ((Settings.LiquidationProfitShare) && (Settings.LiquidationType.ToUpper().Equals("READ")))
                        {
                            if (SecurityHelper.HasAccess("CashdeskOperator.MainScreen.cs.ReadBasedLiquidationMain"))
                                if (((XmlElement)item).Attributes["Visible"].Value.ToUpper() == "Y")
                                    items.Add((XmlElement)item);
                        }
                        break;
                    case "Unlock":
                        if (SecurityHelper.HasAccess("CashdeskOperator.MainScreen.cs.Unlock"))
                        {
                            if (((XmlElement)item).Attributes["Visible"].Value.ToUpper() == "Y")
                                items.Add((XmlElement)item);
                        }
                        break;
                    case "NGAEnrol":
                        if (SecurityHelper.HasAccess("BMC.Presentation.CNGAEnroll"))
                            if (((XmlElement)item).Attributes["Visible"].Value.ToString().ToUpper() == "Y")
                                items.Add((XmlElement)item);
                        break;
                    case "FillVault":
                        if (SecurityHelper.HasAccess("BMC.Presentation.CFillVault"))
                            if (((XmlElement)item).Attributes["Visible"].Value.ToString().ToUpper() == "Y")
                                items.Add((XmlElement)item);
                        break;
                    case "ExportDetails":
                        if (SecurityHelper.HasAccess("CashdeskOperator.MainScreen.cs.ExportDetails") && this.IsExchangeServer())
                        {
                            if (((XmlElement)item).Attributes["Visible"].Value.ToUpper() == "Y")
                                items.Add((XmlElement)item);
                        }
                        break;
                    case "SpotCheck":
                        if (SecurityHelper.HasAccess("CashdeskOperator.MainScreen.cs.SpotCheck"))
                            if (((XmlElement)item).Attributes["Visible"].Value.ToUpper() == "Y")
                                items.Add((XmlElement)item);
                        break;
                    case "UpdateGMUNo":
                        if (SecurityHelper.HasAccess("CashdeskOperator.MainScreen.cs.UpdateGMUNo"))
                        {
                            if (((XmlElement)item).Attributes["Visible"].Value.ToUpper() == "Y")
                                items.Add((XmlElement)item);
                        }
                        break;
                    case "GMUPing":
                        if (SecurityHelper.HasAccess("BMC.Presentation.CGMUPING") && CheckGMuPingToolEnabled() && IsExchangeServer())
                        {
                            if (((XmlElement)item).Attributes["Visible"].Value.ToUpper() == "Y")
                                items.Add((XmlElement)item);
                        }
                        break;

                    case "MachineEnableDisable":
                        if (SecurityHelper.HasAccess("CashdeskOperator.MainScreen.cs.CMachineEnableDisable"))
                        {
                            if (((XmlElement)item).Attributes["Visible"].Value.ToUpper() == "Y")
                                items.Add((XmlElement)item);
                        }
                        break;
                    case "GameCapping":
                        if (Settings.IsGameCappingEnabled)
                            if (SecurityHelper.HasAccess("CashdeskOperator.MainScreen.cs.GameCapping"))
                            {
                                if (((XmlElement)item).Attributes["Visible"].Value.ToUpper() == "Y")
                                    items.Add((XmlElement)item);
                            }
                        break;
                    default:
                        break;
                }
            }

            if (items.Count > 0)
            {
                pnlContent.Children.Clear();
                _sCurrentViewCaption = "";
                lstLeftPane.ItemsSource = items;
                lstLeftPane.SelectedIndex = 0;
                if (IsApplauncherEnabled)
                    lstMenuItemsForAppLauncher = items;

            }
            else
            {
                pnlContent.Children.Clear();
                MessageBox.ShowBox("MessageID14", SecurityHelper.CurrentUser.UserName);
                Application.Current.Shutdown();
            }
        }

        bool CheckGMuPingToolEnabled()
        {
            try
            {
                return bool.Parse(ConfigurationManager.AppSettings["IsPingGMUEnabled"].ToString());
            }
            catch
            {
                return true;
            }
        }

        bool CheckMinimizeBtnEnabled()
        {
            try
            {
                return bool.Parse(ConfigurationManager.AppSettings["IsMinimizeButtonEnabled"].ToString());
            }
            catch
            {
                return true;
            }
        }

        private void btnPassword_PreviewMouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var passwordRetry = new PasswordRetry(SecurityHelper.CurrentUser.SecurityUserID);
            passwordRetry.ShowDialogEx(this);
        }

        private void CheckExchangeConnectivity()
        {
            LogManager.WriteLog("Inside CheckExchangeConnectivity", LogManager.enumLogLevel.Info);

            SoundPlayer HndPayBeep = new SoundPlayer();
            Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(delegate()
                       {
                           lblDate.Visibility = Visibility.Hidden;
                       }));
            try
            {
                if (File.Exists(Settings.Handpay_Wav_Path))
                {
                    HndPayBeep.SoundLocation = Settings.Handpay_Wav_Path;
                }
                else
                {
                    HndPayBeep.Stream = BMC.Presentation.POS.Properties.Resources.HandPay;
                }
            }
            catch (Exception Ex)
            {
                LogManager.WriteLog("Could not load sound File:" + Ex.Message, LogManager.enumLogLevel.Error);
            }

            while (true)
            {
                try
                {

                    if (!String.IsNullOrEmpty(_exchangeConnectionString))
                    {
                        using (SqlConnection objSQLConn = new SqlConnection(System.Text.RegularExpressions.Regex.Replace(_exchangeConnectionString, "CONNECTION TIMEOUT=[0-9]*", "CONNECTION TIMEOUT=5")))
                        {

                            objSQLConn.Open();
                            if (Settings.HandPayBeepEnabled.ToUpper() != "Y")
                            {
                                SqlCommand ocmd = new SqlCommand("Select Top 1 1 from Site With(NOLOCK)", objSQLConn);
                                ocmd.ExecuteNonQuery();
                            }

                            Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(delegate()
                            {
                                lblDatabase.Background = LiGreen;//System.Windows.Media.Brushes.YellowGreen;
                            }));

                            try
                            {

                                if (Settings.HandPayBeepEnabled == "Y")
                                {
                                    if (objSQLConn.State == ConnectionState.Open)
                                    {
                                        SqlCommand oCmd = new SqlCommand("rsp_GETHandPayStatus", objSQLConn);
                                        oCmd.CommandTimeout = 10;
                                        int iHndPay = (int)oCmd.ExecuteScalar();
                                        if (iHndPay > 0)
                                        {
                                            HndPayBeep.PlayLooping();
                                        }
                                        else
                                        {
                                            HndPayBeep.Stop();
                                        }
                                    }
                                }

                            }
                            catch
                            {
                                LogManager.WriteLog("Error->HandPayBeep", LogManager.enumLogLevel.Error);
                                HndPayBeep.Stop();
                                throw;
                            }
                            Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(delegate
                            {
                                pnlStatus.Background = (System.Windows.Media.Brush)FindResource("BGStatusBar");
                            }));

                            try
                            {
                                if (!IsServiceActive())
                                {
                                    Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(delegate()
                                    {
                                        btnServices.Background = LiRed;// System.Windows.Media.Brushes.Red;
                                    }));
                                }
                                else
                                {
                                    Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(delegate()
                                    {
                                        btnServices.Background = LiGreen;// System.Windows.Media.Brushes.YellowGreen;
                                    }));
                                }
                            }
                            catch (Exception)
                            {
                                Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(delegate()
                                {
                                    btnServices.Background = LiRed;// System.Windows.Media.Brushes.Red;
                                }));
                            }
                            Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(delegate()
                                   {
                                       GetNotificationCount();
                                       if (lblwelcome.Visibility == Visibility.Visible)
                                       {
                                           lblwelcome.Visibility = Visibility.Hidden;
                                           lblUsername.Visibility = Visibility.Hidden;
                                           lblDate.Visibility = Visibility.Visible;

                                       }
                                       else
                                       {
                                           lblwelcome.Visibility = Visibility.Visible;
                                           lblUsername.Visibility = Visibility.Visible;
                                           lblDate.Visibility = Visibility.Hidden;
                                       }
                                   }));
                            Thread.Sleep(_exchangeServerCheckInterval);
                        }
                    }
                    else
                    {
                        Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(delegate()
                        {
                            lblDatabase.Background = LiRed;// System.Windows.Media.Brushes.Red;
                            btnServices.Background = LiRed;// System.Windows.Media.Brushes.Red;
                        }));
                    }

                }
                catch (ThreadAbortException)
                {
                    LogManager.WriteLog(string.Format("Thread [{0:D}] was aborted.", Thread.CurrentThread.ManagedThreadId), LogManager.enumLogLevel.Error);

                    Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(delegate()
                    {
                        //pnlStatus.Background = System.Windows.Media.Brushes.DarkRed;
                        lblDatabase.Background = LiRed;// System.Windows.Media.Brushes.Red;

                        btnServices.Background = LiRed;// System.Windows.Media.Brushes.Red;
                    }));


                    Thread.Sleep(_exchangeServerCheckInterval);
                }
                catch (Exception ex)
                {
                    ExceptionManager.Publish(ex);

                    Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(delegate()
                    {
                        //pnlStatus.Background = System.Windows.Media.Brushes.DarkRed;
                        lblDatabase.Background = LiRed;// System.Windows.Media.Brushes.Red;
                        btnServices.Background = LiRed;// System.Windows.Media.Brushes.Red;
                    }));

                    Thread.Sleep(_exchangeServerCheckInterval);

                }
                Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(delegate()
                     {
                         if (lblDatabase.Background == LiRed)
                         {
                             if (s_status != null)
                             {
                                 s_status.LoadServicesDetails(_serviceDetails, false, false);
                             }
                         }
                     }));


            }
        }

        private void GetNotificationCount()
        {
            try
            {
                int NotificationCount = Notifications.CreateInstance().GetNotificationsCount();
                txt_NotificationText.Text = (NotificationCount).ToString();
            }
            catch (Exception ex)
            {
                txt_NotificationText.Text = "0";
                ExceptionManager.Publish(ex);
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                if (PromotionActiveElement != null)
                {
                    e.Cancel = true;
                    return;
                }

                if (_checkExchangeConnectivityThread != null)
                {
                    LogManager.WriteLog("Aborting CheckExchangeConnectivityThread...", LogManager.enumLogLevel.Info);

                    _checkExchangeConnectivityThread.Abort();

                    LogManager.WriteLog("CheckExchangeConnectivityThread aborted successfully", LogManager.enumLogLevel.Info);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            finally
            {
                if (_checkExchangeConnectivityThread != null)
                {
                    _checkExchangeConnectivityThread = null;
                }
            }
        }

        private void btnAbout_PreviewMouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            using (About about = new About())
            {
                about.Owner = this;
                about.ShowDialogEx(this);
            }
        }
        private bool IsTitoEnabled()
        {
            int iTitoStatus = 0;
            try
            {

                using (SqlConnection oConn = new SqlConnection(_exchangeConnectionString))
                {

                    using (SqlCommand oCmd = new SqlCommand("dbo.rsp_GetTITOStatus", oConn))
                    {
                        oConn.Open();
                        oCmd.Connection = oConn;
                        iTitoStatus = Convert.ToInt32(oCmd.ExecuteScalar());
                    }
                }

            }
            catch (Exception Ex)
            {
                LogManager.WriteLog("IsTitoEnabled(): [Error] " + Ex.Message, LogManager.enumLogLevel.Error);
                iTitoStatus = 0;
            }
            return (iTitoStatus == 1) ? true : false;
        }


        /// <summary>
        /// To check whether the site having valid license and allow/restrict the user to login to the site based on the license settings
        /// </summary>
        /// <returns></returns>
        private bool LicenseValidation()
        {
            try
            {
                //return new LicenseValidator(this).Validate;
                LicenseValidator.CurrentWindow = this;
                LicenseValidator objLicenseValidator = LicenseValidator.GetLicenseValidator;
                if (objLicenseValidator != null)
                    return objLicenseValidator.ValidateLicense();

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return false;
        }

        void clientObj_SiteLicensingClientReceived(object lst)
        {
            if (lst != null && Convert.ToString(lst).Equals("SITELICENSING") && !LicenseScreenActive)
                _SiteLicensingExpired = true;
        }

        private void SiteLicensingChecker(object sender, EventArgs e)
        {
            if (!LicenseScreenActive && counter++ >= App.iSiteLicensingCheck)
            {
                counter = 0;
#if !FLOORVIEW_REFRESH_NEW
                App.siteLicensingClientObj.ClientReceived -= clientObj_SiteLicensingClientReceived;
                App.siteLicensingClient.SendToServer(App.siteLicensingClientObj, null);
                App.siteLicensingClientObj.ClientReceived += new ClientReceivedEvent(clientObj_SiteLicensingClientReceived);
#endif
            }
            if (!_SiteLicensingExpired) return;
            if (!LicenseScreenActive)
            {
                _SiteLicensingExpired = false;
                LicenseScreenActive = true;
                siteLicensingTimer.Stop();
                if (!LicenseValidation())
                {
                    LogOffScreen();
                }
                else
                {
                    siteLicensingTimer.Start();
                    LicenseScreenActive = false;
                }
            }
        }

        #region Login Cleanup

        private Login _loginInstance = null;

        public Login LoginInstance
        {
            get { return _loginInstance; }
            set { _loginInstance = value; }
        }

        internal void DisposeLoginInstanceIfAny()
        {
            if (_loginInstance != null)
            {
                Helper_classes.Common.DisposeObject(ref _loginInstance);
                LogManager.WriteLog("|=> Login was successfully disposed.", LogManager.enumLogLevel.Info);
                this.SetDefaultDialogOwner();
            }
        }

        #endregion

        #region Dispose Children Members

        private void DisposeChildren(bool reclaimMemory)
        {
            try
            {
                string objectName = !string.IsNullOrEmpty(_sCurrentViewCaption) ? _sCurrentViewCaption : "MainScreen";
                int count = pnlContent.Children.Count;
                for (int i = count - 1; i >= 0; i--)
                {
                    IDisposable element = pnlContent.Children[i] as IDisposable;
                    if (element != null)
                    {
                        if (element is CFloorView)
                        {
                            LogManager.WriteLog("|=> CFloorView screen persisted.", LogManager.enumLogLevel.Info);
                        }
                        else
                        {
                            BMC.Presentation.Helper_classes.Common.DisposeObject(ref element, false);
                        }
                    }
                    pnlContent.Children.RemoveAt(i);
                }
                if (count > 0)
                {
                    //if (reclaimMemory) WPFMemoryOptimizer.Optimize(objectName);
                    LogManager.WriteLog("|=> MainScreen pnlContent Children disposed successfully.", LogManager.enumLogLevel.Info);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        #endregion

        #region IDisposable Members

        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
#if !FLOORVIEW_REFRESH_NEW
            App.siteLicensingClientObj.ClientReceived -= clientObj_SiteLicensingClientReceived;
#endif
            if (!disposed)
            {
                if (disposing)
                {
                    this.DestroyClientSubscriber();

                    this.CleanupWPFObjectTopControls((i) =>
                    {
                        _loadView -= (this.LoadView);
                        _listSelectionChanged -= (this.ListBoxSelectionChanged);

                        this.Loaded -= this.Window_Loaded;
                        this.Closing -= this.Window_Closing;
                        btnExit.Click -= this.btnExit_Click;

                        btnLogout.PreviewMouseLeftButtonUp -= this.btnLogout_PreviewMouseLeftButtonUp;
                        btnMinimize.PreviewMouseLeftButtonUp -= this.btnMinimize_PreviewMouseLeftButtonUp;
                        btnPassword.PreviewMouseLeftButtonUp -= this.btnPassword_PreviewMouseLeftButtonUp;
                        btnReset.PreviewMouseLeftButtonUp -= this.btnReset_PreviewMouseLeftButtonUp;
                        btnShutdown.PreviewMouseLeftButtonUp -= this.btnShutdown_PreviewMouseLeftButtonUp;
                        btnTheme.PreviewMouseLeftButtonUp -= this.btnTheme_PreviewMouseLeftButtonUp;
                        btnAbout.PreviewMouseLeftButtonUp -= this.btnAbout_PreviewMouseLeftButtonUp;
                        lstLeftPane.SelectionChanged -= lstLeftPane_SelectionChanged;
                        if (siteLicensingTimer != null)
                            siteLicensingTimer.Tick -= SiteLicensingChecker;
                        timerLogoff.Stop();

                        HC.Common.DisposeObject(ref btnLogout);
                        HC.Common.DisposeObject(ref btnMinimize);
                        HC.Common.DisposeObject(ref btnPassword);
                        HC.Common.DisposeObject(ref btnReset);
                        HC.Common.DisposeObject(ref btnShutdown);
                        HC.Common.DisposeObject(ref btnTheme);
                        HC.Common.DisposeObject(ref btnAbout);

                        this.DisposeChildren(false);
                        this.DisposeFloorView();
                        lblwelcome.ClearVisualTransform();
                        this.lstLeftPane.Cleanup_LeftNavPanel();
                        this.ClearTriggers();
                    },
                    (c) =>
                    {
                        if (c is Border) ((Border)c).RenderClose();
                    });
                    LogManager.WriteLog("|=> MainScrren screen objects are released successfully.", LogManager.enumLogLevel.Info);
                }

                disposed = true;
            }
        }

        /// <summary>
        /// Disposes the floor view.
        /// </summary>
        private void DisposeFloorView()
        {
            if (objFloorView != null)
            {
                HC.Common.DisposeObject(ref objFloorView);
            }
        }

        ~MainScreen()
        {
            Dispose(false);
        }

        #endregion

        #region Dispose Children Members

        private void DisposeChildren()
        {
            try
            {
                int count = pnlContent.Children.Count;
                for (int i = count - 1; i >= 0; i--)
                {
                    IDisposable element = pnlContent.Children[i] as IDisposable;
                    if (element != null)
                    {
                        if (element is CFloorView)
                        {
                            LogManager.WriteLog("|=> CFloorView screen persisted.", LogManager.enumLogLevel.Info);
                        }
                        else
                        {
                            BMC.Presentation.Helper_classes.Common.DisposeObject(ref element);
                        }
                    }
                    pnlContent.Children.RemoveAt(i);
                }
                LogManager.WriteLog("|=> MainScreen pnlContent Children disposed successfully.", LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        #endregion

        private void btnServices_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (lblDatabase.Background != LiRed)
                {
                    bool isGuardianRunning = true;
                    if (_serviceDetails["BMCGuardianService"] != null)
                    {
                        isGuardianRunning = !(_serviceDetails["BMCGuardianService"].ServiceStatus == "Stopped");
                    }
                    s_status = new ServiceStatus(_serviceDetails, true, isGuardianRunning);
                    s_status.Owner = this;
                    s_status.ShowDialogEx(this);
                    s_status = null;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("btnServices_Click: " + ex.Message, LogManager.enumLogLevel.Error);
            }

        }

        private void btnGrid_PreviewMouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            try
            {
                if (lstMenuItemsForAppLauncher != null && lstMenuItemsForAppLauncher.Count > 0)
                {
                    string Result = string.Empty;
                    CGridLayout ogrd = new CGridLayout(lstMenuItemsForAppLauncher, this.ListBoxSelectionChanged);
                    ogrd.Owner = this;
                    ogrd.ShowInTaskbar = false;
                    if (ogrd.ShowDialog().Value)
                    {
                        LoadViewExternal(ogrd.Result);
                        lstLeftPane.ScrollIntoView(ogrd.SelectedItem);
                    }
                }
            }
            catch (Exception Ex)
            {

                ExceptionManager.Publish(Ex);
            }
        }

        private void btn_Notifications_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CNotifications about = new CNotifications();
                about.ShowDialogEx(this);
                GetNotificationCount();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

    }

    public static class IdleTime
    {
        public static TimeSpan TimeSpan
        {
            get
            {
                return TimeSpan.FromMilliseconds(Milliseconds);
            }
        }

        public static uint Milliseconds
        {
            get
            {
                var lii = new LASTINPUTINFO
                              {
                                  cbSize = (uint)Marshal.SizeOf(typeof(LASTINPUTINFO))
                              };

                if (!GetLastInputInfo(ref lii))
                    return 0;
                return (uint)Environment.TickCount - lii.dwTime;
            }
        }

        [DllImport("User32.dll")]
        private static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);


        [StructLayout(LayoutKind.Sequential)]
        private struct LASTINPUTINFO
        {
            public uint cbSize;
            public uint dwTime;
        }
    }
}