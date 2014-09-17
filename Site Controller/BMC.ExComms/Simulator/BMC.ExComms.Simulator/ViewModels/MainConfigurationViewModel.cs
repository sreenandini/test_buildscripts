using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Windows;
using System.Windows.Input;
using BMC.CoreLib;
using BMC.CoreLib.Collections;
using BMC.CoreLib.Concurrent;
using BMC.CoreLib.Diagnostics;
using BMC.CoreLib.WPF;
using BMC.ExComms.Contracts.DTO.Freeform;
using BMC.ExComms.Contracts.Interfaces;
using BMC.ExComms.Contracts.Proxies;
using BMC.ExComms.Server.ExecutionSteps;
using BMC.ExComms.Simulator.DataLayer;
using BMC.ExComms.Simulator.Handlers;
using BMC.ExComms.Simulator.Models;
using BMC.CoreLib.WPF;
using BMC.CoreLib.Win32;
using System.Windows.Forms;
using System.Threading;
using System.Threading.Tasks;
using BMC.ExComms.Server.Handlers.GMUEvent;
using BMC.ExComms.Server.Handlers.ECash;
using BMC.ExComms.Server.Handlers;
using BMC.ExComms.Server.Handlers.Tickets;
using BMC.ExComms.Server.Handlers.LongPoll;

namespace BMC.ExComms.Simulator.ViewModels
{
    public class MainConfigurationViewModel
        : ViewModelBase,
        IExCommsServerCallback,
        IExecutionStepExecutor,
        IFFMsgTransmitter
    {
        private readonly IExecutorService _executorService = ExecutorServiceFactory.CreateExecutorService();
        private readonly IExecutorService _executorServiceAsync = ExecutorServiceFactory.CreateExecutorService();
        private static string IPADDR = IPAddress.Any.ToString();// Extensions.GetIpAddressString(-1);// "192.168.1.10";

        private SocketTransceiver _socket = null;

        private FFTblGIMInformation _tblGIM = null;
        private FFTblSettings _tblSettings = null;
        private FFTblCardInformation _tblCardNos = null;

        private ExCommsServerCallbackProxy _proxyExecutionStepChanged = null;

        private IDictionary<string, Action<string>> _settingValueActions = null;
        private IDictionary<string, ExecutionStepChangedModelCollection> _executionSteps = null;

        private readonly UdpRawRequestItemModelCollectionByGmu _requestByGmu = new UdpRawRequestItemModelCollectionByGmu();
        private readonly UdpRawResponseItemModelCollectionByGmu _responseByGmu = new UdpRawResponseItemModelCollectionByGmu();

        private readonly GIMInformationModelCollection _gimInfoAll = new GIMInformationModelCollection();
        private readonly PlayerCardInfoModelCollection _playerCardInfos = new PlayerCardInfoModelCollection();
        private readonly EmployeeCardInfoModelCollection _employeeCardInfos = new EmployeeCardInfoModelCollection();
        private readonly ECashTextValueModelCollection _ecashTextValues = new ECashTextValueModelCollection();
        private readonly ECashWithdrawOptionModelCollection _ecashWithdrawOptions = new ECashWithdrawOptionModelCollection();
        private readonly TicketInfoModelCollection _processedTickets = new TicketInfoModelCollection();
        private readonly TicketTypeModelCollection _ticketTypes = new TicketTypeModelCollection();
        private readonly GmuEventCategoryModelCollection _gmuEventCategories = new GmuEventCategoryModelCollection();

        public MainConfigurationViewModel()
        {
            FFMsgHandlerFactory.Initialize(_executorService, FFTgtHandlerDeviceTypes.Simulator, () => { return this; });
            //FFMsgHandlerFactory.Current.ExecutionStepExecutor = this;
            _tblGIM = App.DB.GIMInformation;
            _tblCardNos = App.DB.CardInformation;
            _tblSettings = App.DB.Settings;

            this.ReloadCommand = new DelegateCommand(this.OnReloadCommand, this.CanReloadCommand);
            this.AddGmuCommand = new DelegateCommand(this.OnAddGmuCommand, this.CanAddGmuCommand);
            this.RemoveGmuCommand = new DelegateCommand(this.OnRemoveGmuCommand, this.CanRemoveGmuCommand);
            this.GenerateIPCommand = new DelegateCommand(this.OnGenerateIPCommand, this.CanGenerateIPCommand);
            this.SaveCommand = new DelegateCommand(this.OnSaveCommand, this.CanSaveCommand);
            this.StartGMUCommand = new DelegateCommand(this.OnStartGMUCommand, this.CanStartGMUCommand);

            this.AddPlayerCardCommand = new DelegateCommand(this.OnAddPlayerCardCommand, this.CanAddPlayerCardCommand);
            this.RemovePlayerCardCommand = new DelegateCommand(this.OnRemovePlayerCardCommand, this.CanRemovePlayerCardCommand);
            this.AddEmployeeCardCommand = new DelegateCommand(this.OnAddEmployeeCardCommand, this.CanAddEmployeeCardCommand);
            this.RemoveEmployeeCardCommand = new DelegateCommand(this.OnRemoveEmployeeCardCommand, this.CanRemoveEmployeeCardCommand);

            this.PlayerCardInCommand = new DelegateCommand(this.OnPlayerCardInCommand, this.CanPlayerCardInCommand);
            this.PlayerCardOutCommand = new DelegateCommand(this.OnPlayerCardOutCommand, this.CanPlayerCardOutCommand);
            this.EmployeeCardInCommand = new DelegateCommand(this.OnEmployeeCardInCommand, this.CanEmployeeCardInCommand);
            this.EmployeeCardOutCommand = new DelegateCommand(this.OnEmployeeCardOutCommand, this.CanEmployeeCardOutCommand);

            this.EFTBalanceRequestCommand = new DelegateCommand(this.OnEFTBalanceRequestCommand, this.CanEFTBalanceRequestCommand);

            this.TicketPrintCommand = new DelegateCommand(this.OnTicketPrintCommand, this.CanTicketPrintCommand);
            this.TicketRedeemCommand = new DelegateCommand(this.OnTicketRedeemCommand, this.CanTicketRedeemCommand);
            this.TicketVoidCommand = new DelegateCommand(this.OnTicketVoidCommand, this.CanTicketVoidCommand);
            this.PostEventCommand = new DelegateCommand(this.OnPostEventCommand, this.CanPostEventCommand);

            this.RawRequestCommand = new DelegateCommand(this.OnRawRequestCommand, this.CanRawRequestCommand);

            _executionSteps = new StringConcurrentDictionary<ExecutionStepChangedModelCollection>();
            this.GIMInformations = new GIMInformationModelCollection();
            this.GmuIpGeneration = new GmuIpGenerationModel()
            {
                NetworkInterfaces = WPFExtensions.CreateCollectionView(Extensions.GetActiveNetworkInterfaces(), true),
            };

            this.GIMInformationsAll = WPFExtensions.CreateCollectionView(_gimInfoAll);
            this.GIMInformationsForCard = WPFExtensions.CreateCollectionView(_gimInfoAll);
            this.GIMInformationsAllForRawMessages = WPFExtensions.CreateCollectionView(_gimInfoAll);
            this.GIMInformationsAllForRawMessages.CurrentChanged += GIMInformationsAllForRawMessages_CurrentChanged;
            this.ECashTextValues = WPFExtensions.CreateCollectionView(_ecashTextValues);
            this.ECashTextValuesDisplay = WPFExtensions.CreateCollectionView(_ecashTextValues);
            this.ECashWithdrawOptions = WPFExtensions.CreateCollectionView(_ecashWithdrawOptions);
            this.ProcessedTickets = WPFExtensions.CreateCollectionView(_processedTickets);
            this.TicketTypes = WPFExtensions.CreateCollectionView(_ticketTypes);
            this.GmuEventCategories = WPFExtensions.CreateCollectionView(_gmuEventCategories);
            this.GmuEventCategories.CurrentChanged += GmuEventCategories_CurrentChanged;

            this.RequestItems = new UdpRawRequestItemModelCollection();
            this.ResponseItems = new UdpRawResponseItemModelCollection();
            this.GIMInformationsView = WPFExtensions.CreateCollectionView(this.GIMInformations);
            this.GIMInformationsView.CurrentChanged += GIMInformationsView_CurrentChanged;

            this.PlayerCardInfosEditView = WPFExtensions.CreateCollectionView(_playerCardInfos);
            this.PlayerCardInfosEditView.CurrentChanged += PlayerCardInfosEditView_CurrentChanged;
            this.EmployeeCardInfosEditView = WPFExtensions.CreateCollectionView(_employeeCardInfos);
            this.EmployeeCardInfosEditView.CurrentChanged += EmployeeCardInfosEditView_CurrentChanged;
            this.PlayerCardInfosDisplayView = WPFExtensions.CreateCollectionView(_playerCardInfos);
            this.PlayerCardInfosDisplayView.CurrentChanged += PlayerCardInfosDisplayView_CurrentChanged;
            this.EmployeeCardInfosDisplayView = WPFExtensions.CreateCollectionView(_employeeCardInfos);
            this.EmployeeCardInfosDisplayView.CurrentChanged += EmployeeCardInfosDisplayView_CurrentChanged;

            this.PlayerCardInfosEditView.MoveCurrentToFirst();
            this.EmployeeCardInfosEditView.MoveCurrentToFirst();
            this.PlayerCardInfosDisplayView.MoveCurrentToFirst();
            this.EmployeeCardInfosDisplayView.MoveCurrentToFirst();

            this.InitSocket();
            this.InsertDefaultSettings();
            this.SetSettingValueActions();
            this.Load();
            this.ReloadRecords();
            this.ReloadPlayerCardInfos();
            this.ReloadEmployeeCardInfos();
            this.LoadServerDetails();
        }

        void GmuEventCategories_CurrentChanged(object sender, EventArgs e)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "GIMInformationsAllForRawMessages_CurrentChanged"))
            {
                try
                {
                    GmuEventCategoryModel model = this.GmuEventCategories.CurrentItem as GmuEventCategoryModel;
                    if (model != null)
                    {
                        this.GmuEventTypes = WPFExtensions.CreateCollectionView(model.EventTypes.OrderBy(s => s.Description).Select(s => s).ToList());
                    }
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }
            }
        }

        void GIMInformationsAllForRawMessages_CurrentChanged(object sender, EventArgs e)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "GIMInformationsAllForRawMessages_CurrentChanged"))
            {
                try
                {
                    this.ChangeRequestAndResponse(this.GIMInformationsAllForRawMessages.CurrentItem as GIMInformationModel);
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }
            }
        }

        private void LoadServerDetails()
        {
            _proxyExecutionStepChanged = ExCommsServerProxyFactory.Get(_executorService, ExCommsServerCallbackTypes.ExecutionStepChanged,
                                                                        this, 5000, null);
        }

        private void InsertDefaultSettings()
        {
            _tblSettings.Add("GIMStartingIPAddress", "192.168.10.1");
            _tblSettings.Add("GIMSubnetMask", "255.255.255.0");
            _tblSettings.Add("GIMTotalGMUs", "1");
        }

        private void SetSettingValueActions()
        {
            _settingValueActions = new SortedDictionary<string, Action<string>>()
            {
                { "GIMStartingIPAddress", (v) => { this.GmuIpGeneration.StartingIPAddress = v; } },
                { "GIMSubnetMask", (v) => { this.GmuIpGeneration.SubnetMask = v; } },
                { "GIMTotalGMUs", (v) => { this.GmuIpGeneration.TotalGMUs = v.ConvertToInt32(); } },
            };
        }

        private void Load()
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "Load"))
            {
                try
                {
                    DataTable dt = _tblSettings.GetAllRecords();
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        foreach (var dr in dt.Rows.OfType<DataRow>())
                        {
                            string settingName = dr.Field<string>("SettingName");
                            string settingValue = dr.Field<string>("SettingValue");
                            if (_settingValueActions.ContainsKey(settingName))
                                _settingValueActions[settingName](settingValue);
                        }
                    }
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }
            }
        }

        public ICollectionView GIMInformationsView { get; set; }

        public ICollectionView GIMInformationsAll
        {
            get { return (ICollectionView)GetValue(GIMInformationsAllProperty); }
            set { SetValue(GIMInformationsAllProperty, value); }
        }

        public static readonly DependencyProperty GIMInformationsAllProperty =
            DependencyProperty.Register("GIMInformationsAll", typeof(ICollectionView), typeof(MainConfigurationViewModel), new PropertyMetadata(null));

        public ICollectionView GIMInformationsAllForRawMessages
        {
            get { return (ICollectionView)GetValue(GIMInformationsAllForRawMessagesProperty); }
            set { SetValue(GIMInformationsAllForRawMessagesProperty, value); }
        }

        public static readonly DependencyProperty GIMInformationsAllForRawMessagesProperty =
            DependencyProperty.Register("GIMInformationsAllForRawMessages", typeof(ICollectionView), typeof(MainConfigurationViewModel), new PropertyMetadata(null));

        public ICollectionView GIMInformationsForCard
        {
            get { return (ICollectionView)GetValue(GIMInformationsForCardProperty); }
            set { SetValue(GIMInformationsForCardProperty, value); }
        }

        public static readonly DependencyProperty GIMInformationsForCardProperty =
            DependencyProperty.Register("GIMInformationsForCard", typeof(ICollectionView), typeof(MainConfigurationViewModel), new PropertyMetadata(null));

        public GIMInformationModelCollection GIMInformations
        {
            get { return (GIMInformationModelCollection)GetValue(GIMInformationsProperty); }
            set { SetValue(GIMInformationsProperty, value); }
        }

        public static readonly DependencyProperty GIMInformationsProperty =
            DependencyProperty.Register("GIMInformations", typeof(GIMInformationModelCollection), typeof(MainConfigurationViewModel), new PropertyMetadata(null));

        public GIMInformationModel GIMInformation
        {
            get { return (GIMInformationModel)GetValue(GIMInformationProperty); }
            set { SetValue(GIMInformationProperty, value); }
        }

        public static readonly DependencyProperty GIMInformationProperty =
            DependencyProperty.Register("GIMInformation", typeof(GIMInformationModel), typeof(MainConfigurationViewModel), new PropertyMetadata(null));

        public ExecutionStepChangedModelCollection ExecutionStepsChanged
        {
            get { return (ExecutionStepChangedModelCollection)GetValue(ExecutionStepsChangedProperty); }
            set { SetValue(ExecutionStepsChangedProperty, value); }
        }

        public static readonly DependencyProperty ExecutionStepsChangedProperty =
            DependencyProperty.Register("ExecutionStepsChanged", typeof(ExecutionStepChangedModelCollection), typeof(MainConfigurationViewModel), new PropertyMetadata(null));

        void GIMInformationsView_CurrentChanged(object sender, EventArgs e)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "Method"))
            {
                try
                {
                    this.GIMInformation = this.GIMInformationsView.CurrentItem as GIMInformationModel;
                    this.ExecutionStepsChanged = null;

                    if (this.GIMInformation != null)
                    {
                        if (_executionSteps.ContainsKey(this.GIMInformation.IPAddress))
                        {
                            this.ExecutionStepsChanged = _executionSteps[this.GIMInformation.IPAddress];
                        }
                    }
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }
            }
        }

        void PlayerCardInfosEditView_CurrentChanged(object sender, EventArgs e)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "PlayerInfosEditView_CurrentChanged"))
            {
                try
                {
                    this.PlayerCardInfoEdit = this.PlayerCardInfosEditView.CurrentItem as PlayerCardInfoModel;
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }
            }
        }

        void EmployeeCardInfosEditView_CurrentChanged(object sender, EventArgs e)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "EmployeeInfosEditView_CurrentChanged"))
            {
                try
                {
                    this.EmployeeCardInfoEdit = this.EmployeeCardInfosEditView.CurrentItem as EmployeeCardInfoModel;
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }
            }
        }

        void PlayerCardInfosDisplayView_CurrentChanged(object sender, EventArgs e)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "PlayerInfosDisplayView_CurrentChanged"))
            {
                try
                {
                    this.PlayerCardInfoDisplay = this.PlayerCardInfosDisplayView.CurrentItem as PlayerCardInfoModel;
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }
            }
        }

        void EmployeeCardInfosDisplayView_CurrentChanged(object sender, EventArgs e)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "EmployeeInfosDisplayView_CurrentChanged"))
            {
                try
                {
                    this.EmployeeCardInfoDisplay = this.EmployeeCardInfosDisplayView.CurrentItem as EmployeeCardInfoModel;
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }
            }
        }

        private void ChangeRequestAndResponse(GIMInformationModel model)
        {
            if (model != null)
            {
                string ipAddress = model.IPAddress;
                this.RequestItemsByGmu = null;
                this.ResponseItemsByGmu = null;

                if (model.IsAll)
                {
                    this.RequestItemsByGmu = this.RequestItems;
                    this.ResponseItemsByGmu = this.ResponseItems;
                }
                else if (!ipAddress.IsEmpty())
                {
                    if (_requestByGmu.ContainsKey(ipAddress))
                    {
                        this.RequestItemsByGmu = _requestByGmu[ipAddress];
                    }
                    if (_responseByGmu.ContainsKey(ipAddress))
                    {
                        this.ResponseItemsByGmu = _responseByGmu[ipAddress];
                    }
                }
            }
        }

        public GmuIpGenerationModel GmuIpGeneration
        {
            get { return (GmuIpGenerationModel)GetValue(GmuIpGenerationProperty); }
            set { SetValue(GmuIpGenerationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for GmuIpGeneration.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GmuIpGenerationProperty =
            DependencyProperty.Register("GmuIpGeneration", typeof(GmuIpGenerationModel), typeof(MainConfigurationViewModel), new PropertyMetadata(null));

        public ICollectionView ECashTextValues
        {
            get { return (ICollectionView)GetValue(ECashTextValuesProperty); }
            set { SetValue(ECashTextValuesProperty, value); }
        }

        public static readonly DependencyProperty ECashTextValuesProperty =
            DependencyProperty.Register("ECashTextValues", typeof(ICollectionView), typeof(MainConfigurationViewModel), new PropertyMetadata(null));

        public ICollectionView ECashTextValuesDisplay
        {
            get { return (ICollectionView)GetValue(ECashTextValuesDisplayProperty); }
            set { SetValue(ECashTextValuesDisplayProperty, value); }
        }

        public static readonly DependencyProperty ECashTextValuesDisplayProperty =
            DependencyProperty.Register("ECashTextValuesDisplay", typeof(ICollectionView), typeof(MainConfigurationViewModel), new PropertyMetadata(null));

        public ICollectionView ECashWithdrawOptions
        {
            get { return (ICollectionView)GetValue(ECashWithdrawOptionsProperty); }
            set { SetValue(ECashWithdrawOptionsProperty, value); }
        }

        public static readonly DependencyProperty ECashWithdrawOptionsProperty =
            DependencyProperty.Register("ECashWithdrawOptions", typeof(ICollectionView), typeof(MainConfigurationViewModel), new PropertyMetadata(null));

        public string ECashPIN
        {
            get { return (string)GetValue(ECashPINProperty); }
            set { SetValue(ECashPINProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ECashPIN.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ECashPINProperty =
            DependencyProperty.Register("ECashPIN", typeof(string), typeof(MainConfigurationViewModel), new PropertyMetadata(string.Empty));

        public ICollectionView ProcessedTickets
        {
            get { return (ICollectionView)GetValue(ProcessedTicketsProperty); }
            set { SetValue(ProcessedTicketsProperty, value); }
        }

        public static readonly DependencyProperty ProcessedTicketsProperty =
            DependencyProperty.Register("ProcessedTickets", typeof(ICollectionView), typeof(MainConfigurationViewModel), new PropertyMetadata(null));

        public ICollectionView TicketTypes
        {
            get { return (ICollectionView)GetValue(TicketTypesProperty); }
            set { SetValue(TicketTypesProperty, value); }
        }

        public static readonly DependencyProperty TicketTypesProperty =
            DependencyProperty.Register("TicketTypes", typeof(ICollectionView), typeof(MainConfigurationViewModel), new PropertyMetadata(null));

        public ICollectionView GmuEventCategories
        {
            get { return (ICollectionView)GetValue(GmuEventCategoriesProperty); }
            set { SetValue(GmuEventCategoriesProperty, value); }
        }

        public static readonly DependencyProperty GmuEventCategoriesProperty =
            DependencyProperty.Register("GmuEventCategories", typeof(ICollectionView), typeof(MainConfigurationViewModel), new PropertyMetadata(null));

        public ICollectionView GmuEventTypes
        {
            get { return (ICollectionView)GetValue(GmuEventTypesProperty); }
            set { SetValue(GmuEventTypesProperty, value); }
        }

        public static readonly DependencyProperty GmuEventTypesProperty =
            DependencyProperty.Register("GmuEventTypes", typeof(ICollectionView), typeof(MainConfigurationViewModel), new PropertyMetadata(null));

        #region RequestItems
        public UdpRawRequestItemModelCollection RequestItems
        {
            get { return (UdpRawRequestItemModelCollection)GetValue(RequestItemsProperty); }
            set { SetValue(RequestItemsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RequestItems.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RequestItemsProperty =
            DependencyProperty.Register("RequestItems", typeof(UdpRawRequestItemModelCollection), typeof(MainConfigurationViewModel), new PropertyMetadata(null));
        #endregion

        #region ResponseItems
        public UdpRawResponseItemModelCollection ResponseItems
        {
            get { return (UdpRawResponseItemModelCollection)GetValue(ResponseItemsProperty); }
            set { SetValue(ResponseItemsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ResponseItems.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ResponseItemsProperty =
            DependencyProperty.Register("ResponseItems", typeof(UdpRawResponseItemModelCollection), typeof(MainConfigurationViewModel), new PropertyMetadata(null));
        #endregion

        #region RequestItemsByGmu
        public UdpRawRequestItemModelCollection RequestItemsByGmu
        {
            get { return (UdpRawRequestItemModelCollection)GetValue(RequestItemsByGmuProperty); }
            set { SetValue(RequestItemsByGmuProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RequestItemsByGmu.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RequestItemsByGmuProperty =
            DependencyProperty.Register("RequestItemsByGmu", typeof(UdpRawRequestItemModelCollection), typeof(MainConfigurationViewModel), new PropertyMetadata(null));
        #endregion

        #region ResponseItemsByGmu
        public UdpRawResponseItemModelCollection ResponseItemsByGmu
        {
            get { return (UdpRawResponseItemModelCollection)GetValue(ResponseItemsByGmuProperty); }
            set { SetValue(ResponseItemsByGmuProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ResponseItemsByGmu.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ResponseItemsByGmuProperty =
            DependencyProperty.Register("ResponseItemsByGmu", typeof(UdpRawResponseItemModelCollection), typeof(MainConfigurationViewModel), new PropertyMetadata(null));
        #endregion

        #region PlayerCardInfosEditView

        public ICollectionView PlayerCardInfosEditView
        {
            get { return (ICollectionView)GetValue(PlayerCardInfosEditViewProperty); }
            set { SetValue(PlayerCardInfosEditViewProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PlayerCardInfosEditView.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PlayerCardInfosEditViewProperty =
            DependencyProperty.Register("PlayerCardInfosEditView", typeof(ICollectionView), typeof(MainConfigurationViewModel), new PropertyMetadata(null));

        #endregion

        #region PlayerCardInfoEdit

        public PlayerCardInfoModel PlayerCardInfoEdit
        {
            get { return (PlayerCardInfoModel)GetValue(PlayerCardInfoEditProperty); }
            set { SetValue(PlayerCardInfoEditProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PlayerCardInfoEdit.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PlayerCardInfoEditProperty =
            DependencyProperty.Register("PlayerCardInfoEdit", typeof(PlayerCardInfoModel), typeof(MainConfigurationViewModel), new PropertyMetadata(null));

        #endregion

        #region EmployeeCardInfosEditView

        public ICollectionView EmployeeCardInfosEditView
        {
            get { return (ICollectionView)GetValue(EmployeeCardInfosEditViewProperty); }
            set { SetValue(EmployeeCardInfosEditViewProperty, value); }
        }

        // Using a DependencyProperty as the backing store for EmployeeCardInfosEditView.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EmployeeCardInfosEditViewProperty =
            DependencyProperty.Register("EmployeeCardInfosEditView", typeof(ICollectionView), typeof(MainConfigurationViewModel), new PropertyMetadata(null));

        #endregion

        #region EmployeeCardInfoEdit

        public EmployeeCardInfoModel EmployeeCardInfoEdit
        {
            get { return (EmployeeCardInfoModel)GetValue(EmployeeCardInfoEditProperty); }
            set { SetValue(EmployeeCardInfoEditProperty, value); }
        }

        // Using a DependencyProperty as the backing store for EmployeeCardInfoEdit.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EmployeeCardInfoEditProperty =
            DependencyProperty.Register("EmployeeCardInfoEdit", typeof(EmployeeCardInfoModel), typeof(MainConfigurationViewModel), new PropertyMetadata(null));

        #endregion

        #region PlayerCardInfosDisplayView

        public ICollectionView PlayerCardInfosDisplayView
        {
            get { return (ICollectionView)GetValue(PlayerCardInfosDisplayViewProperty); }
            set { SetValue(PlayerCardInfosDisplayViewProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PlayerCardInfosDisplayView.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PlayerCardInfosDisplayViewProperty =
            DependencyProperty.Register("PlayerCardInfosDisplayView", typeof(ICollectionView), typeof(MainConfigurationViewModel), new PropertyMetadata(null));

        #endregion

        #region PlayerCardInfoDisplay

        public PlayerCardInfoModel PlayerCardInfoDisplay
        {
            get { return (PlayerCardInfoModel)GetValue(PlayerCardInfoDisplayProperty); }
            set { SetValue(PlayerCardInfoDisplayProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PlayerCardInfoDisplay.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PlayerCardInfoDisplayProperty =
            DependencyProperty.Register("PlayerCardInfoDisplay", typeof(PlayerCardInfoModel), typeof(MainConfigurationViewModel), new PropertyMetadata(null));

        #endregion

        #region EmployeeCardInfosDisplayView

        public ICollectionView EmployeeCardInfosDisplayView
        {
            get { return (ICollectionView)GetValue(EmployeeCardInfosDisplayViewProperty); }
            set { SetValue(EmployeeCardInfosDisplayViewProperty, value); }
        }

        // Using a DependencyProperty as the backing store for EmployeeCardInfosDisplayView.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EmployeeCardInfosDisplayViewProperty =
            DependencyProperty.Register("EmployeeCardInfosDisplayView", typeof(ICollectionView), typeof(MainConfigurationViewModel), new PropertyMetadata(null));

        #endregion

        #region EmployeeCardInfoDisplay

        public EmployeeCardInfoModel EmployeeCardInfoDisplay
        {
            get { return (EmployeeCardInfoModel)GetValue(EmployeeCardInfoDisplayProperty); }
            set { SetValue(EmployeeCardInfoDisplayProperty, value); }
        }

        // Using a DependencyProperty as the backing store for EmployeeCardInfoDisplay.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EmployeeCardInfoDisplayProperty =
            DependencyProperty.Register("EmployeeCardInfoDisplay", typeof(EmployeeCardInfoModel), typeof(MainConfigurationViewModel), new PropertyMetadata(null));

        #endregion

        #region Reload Command
        public ICommand ReloadCommand { get; private set; }

        private bool CanReloadCommand(object parameter)
        {
            return true;
        }

        private void OnReloadCommand(object parameter)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "OnReloadCommand");

            try
            {

            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }
        #endregion

        #region Save Command
        public ICommand SaveCommand { get; private set; }

        private bool CanSaveCommand(object parameter)
        {
            return true;
        }

        private void OnSaveCommand(object parameter)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "OnSaveCommand");

            try
            {
                // save the gim information
                var modified = (from g in this.GIMInformations
                                where g._isPropertyModified == true
                                select g);
                if (modified != null)
                {
                    foreach (var info in modified)
                    {
                        _tblGIM.Update(info.RowNo, info.IPAddress, info.AssetNo, info.GmuNo, info.SerialNo,
                                        info.ManufacturerID, info.MACAddress, info.GMUVersion, info.SASVersion);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }
        #endregion

        #region AddGmu Command
        public ICommand AddGmuCommand { get; private set; }

        private bool CanAddGmuCommand(object parameter)
        {
            return true;
        }

        private void OnAddGmuCommand(object parameter)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "OnAddGmuCommand");

            try
            {
                this.AddGmuIPAddress(IPAddress.Loopback.ToString());
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        private void AddGmuIPAddress(string ipAddress)
        {
            GIMInformationModel info = new GIMInformationModel()
            {
                IPAddress = ipAddress,
                AssetNo = string.Empty,
                GmuNo = string.Empty,
                SerialNo = string.Empty,
                ManufacturerID = string.Empty,
                MACAddress = "",
                SASVersion = "601",
                GMUVersion = "Ver-300.05.18a Options",
            };
            int hashCode = info.GetHashCode();
            _tblGIM.Add(hashCode, ipAddress, info.AssetNo, info.GmuNo, info.SerialNo,
                        info.ManufacturerID, info.MACAddress, info.GMUVersion, info.SASVersion);
            this.GIMInformations.AddWithSNo(info);
        }

        #endregion

        #region RemoveGmu Command
        public ICommand RemoveGmuCommand { get; private set; }

        private bool CanRemoveGmuCommand(object parameter)
        {
            return true;
        }

        private void OnRemoveGmuCommand(object parameter)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "OnRemoveGmuCommand");

            try
            {
                if (this.GIMInformation != null)
                {
                    _tblGIM.Delete(this.GIMInformation.RowNo);
                    this.ReloadRecords();
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        #endregion

        #region PopulateIP Command
        public ICommand PopulateIPCommand { get; private set; }

        private bool CanPopulateIPCommand(object parameter)
        {
            return true;
        }

        private void OnPopulateIPCommand(object parameter)
        {
            this.GenerateIPAddresses(parameter, true);
        }
        #endregion

        #region GenerateIP Command
        public ICommand GenerateIPCommand { get; private set; }

        private bool CanGenerateIPCommand(object parameter)
        {
            return true;
        }

        private void OnGenerateIPCommand(object parameter)
        {
            this.GenerateIPAddresses(parameter, false);
        }

        private void GenerateIPAddresses(object parameter, bool populateOnly)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "OnGenerateIPCommand");

            try
            {
                GmuIpGenerationModel ipGen = (parameter as MainConfigurationViewModel).GmuIpGeneration;
                IPAddress ipAddress = ipGen.StartingIPAddress.ToIPAddress();
                IPAddress subnetMask = ipGen.SubnetMask.ToIPAddress();

                if (ipAddress != IPAddress.None &&
                    subnetMask != IPAddress.None &&
                    ipGen.TotalGMUs > 0)
                {
                    string[] ipAddresses = this.PrepareIPAddresses(ipGen.TotalGMUs, ipAddress, subnetMask);
                    NetworkInterface selectedInterface = ipGen.NetworkInterfaces.CurrentItem as NetworkInterface;

                    var dt = _tblGIM.GetAllRecords();
                    DataRow[] rows = null;

                    if (dt != null)
                    {
                        rows = dt.Rows.OfType<DataRow>().ToArray();
                    }

                    for (int i = 0; i < ipGen.TotalGMUs; i++)
                    {
                        string ipAddress2 = ipAddresses[i];
                        if (populateOnly ||
                            this.CreateIPAddress(selectedInterface, ipAddress2, subnetMask.ToString()))
                        {
                            if (rows != null && i < rows.Length)
                            {
                                _tblGIM.UpdateIPAddress(rows[i].Field<long>("RowID"), ipAddress2);
                            }
                            else
                            {
                                this.AddGmuIPAddress(ipAddress2);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
            finally
            {
                this.ReloadRecords();
            }
        }

        private bool CreateIPAddress(NetworkInterface nw, string ipAddress, string mask)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "CreateIPAddress"))
            {
                bool result = default(bool);

                try
                {
                    Process p = new Process();
                    p.StartInfo.FileName = "netsh.exe";

                    // Now build the parameters string, which consists of these parameters and identifiers with spaces between each:
                    // 1. "interface ip" - context for command
                    // 2. "add address" - add an address
                    // 3. "name=" - to which adapter to add the address
                    // 4. "addr=" - which address to add to the adapter
                    // 5. "mask=" - subnet mask of address to add
                    // 6. "gateway=" - add the address also as a gateway for the adapter
                    // 7. "gwmetric=" - set a metric of one so this gateway is used
                    StringBuilder parameters = new StringBuilder();
                    parameters.AppendFormat(" interface ip add address name=\"{0}\" addr={1} mask={2}",
                                            nw.Name, ipAddress, mask);
                    //parameters.Append(" interface ip add address name=\"").Append(winAdapterDesc);
                    //parameters.Append("\" addr=").Append(ipAddress);
                    //parameters.Append(" mask=").Append(mark);
                    //parameters.Append(" gateway=").Append(ipAddress);
                    //parameters.Append(" gwmetric=").Append("1");
                    p.StartInfo.Arguments = parameters.ToString();
                    p.StartInfo.UseShellExecute = false;
                    p.StartInfo.CreateNoWindow = true;
                    p.StartInfo.RedirectStandardOutput = true;
                    p.Start();

                    do
                    {
                        p.WaitForExit(5000);
                        if (p.HasExited) break;
                    } while (true);

                    string output = p.StandardOutput.ReadToEnd();
                    method.InfoV("IP {0} was generated successfully.", ipAddress);
                    return true;
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                    method.InfoV("IP {0} was not able to generated.", ipAddress);
                }

                return result;
            }
        }

        private string[] PrepareIPAddresses(int totalGMUs, IPAddress ipAddress, IPAddress subnet)
        {
            byte[] ipBytes = ipAddress.GetAddressBytes();
            byte[] subnetBytes = subnet.GetAddressBytes();
            UInt32 uiStartIndex = 0;
            UInt32 uiIPSerious = 0;

            UInt32 uiSubs = (
                            Convert.ToUInt32(subnetBytes[0]) << 24) |
                            (Convert.ToUInt32(subnetBytes[1]) << 16) |
                            (Convert.ToUInt32(subnetBytes[2]) << 8) |
                            (Convert.ToUInt32(subnetBytes[3])
                            );

            UInt32 uiStartIP = (
                            Convert.ToUInt32(ipBytes[0]) << 24) |
                            (Convert.ToUInt32(ipBytes[1]) << 16) |
                            (Convert.ToUInt32(ipBytes[2]) << 8) |
                            (Convert.ToUInt32(ipBytes[3])
                            );
            uiIPSerious = uiStartIP & uiSubs;
            uiSubs = ~(uiSubs);
            uiStartIndex = uiSubs & uiStartIP;

            byte[] tempIP = new byte[4];
            UInt32 uiTempIP = 0;
            string sTempIP = string.Empty;

            string[] sVirtualIPs = new string[totalGMUs];
            int j = 0;

            for (UInt32 i = 0; (j < totalGMUs); i++)
            {
                uiTempIP = 0;
                sTempIP = string.Empty;

                tempIP[3] = 0x00;
                tempIP[2] = 0x00;
                tempIP[1] = 0x00;
                tempIP[0] = 0x00;

                uiTempIP = uiIPSerious + uiStartIndex + i;

                tempIP[0] = Convert.ToByte((uiTempIP & 0xFF000000) >> 24);
                tempIP[1] = Convert.ToByte((uiTempIP & 0x00FF0000) >> 16);
                tempIP[2] = Convert.ToByte((uiTempIP & 0x0000FF00) >> 8);
                tempIP[3] = Convert.ToByte(uiTempIP & 0x000000FF);

                if ((tempIP[3] == 0x01) | (tempIP[3] == 0x00) /*0*/| (tempIP[3] == 0xFE)/*254*/ | (tempIP[3] == 0xFF)/*255*/)
                {
                    /*Skip this IP*/
                    continue;
                }
                if ((tempIP[2] == 0xFF)/*255*/ | (tempIP[1] == 0xFF) | (tempIP[0] == 0xFF))
                {
                    /*Skip this IP*/
                    continue;
                }

                IPAddress _TempIP = new IPAddress(tempIP);
                sTempIP = _TempIP.ToString();
                sVirtualIPs[j++] = sTempIP;
            }

            return sVirtualIPs;
        }
        #endregion

        #region StartGMU Command
        public ICommand StartGMUCommand { get; private set; }

        private bool CanStartGMUCommand(object parameter)
        {
            return true;
        }

        private void OnStartGMUCommand(object parameter)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "OnStartGMUCommand");

            try
            {
                this.ExecuteTasksInGmus("Starting GMUs", this.OnInitGIMMessage);
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        private void OnInitGIMMessage(IAsyncProgress2 o, GIMInformationModel model)
        {
            o.UpdateStatus("Starting GMU : " + model.DisplayText);
            FFMsg_G2H msg = FreeformEntityFactory.CreateEntity<FFMsg_G2H>(FF_FlowDirection.G2H,
                               new FFCreateEntityRequest_G2H()
                               {
                                   Command = FF_AppId_G2H_Commands.ResponseRequest,
                                   MessageType = FF_AppId_G2H_MessageTypes.FreeForm,
                                   SessionID = FF_AppId_SessionIds.GIM,
                                   IPAddress = model.IPAddress,
                                   SkipTransactionId = true,
                               });
            FFTgt_B2B_GIM gim = new FFTgt_B2B_GIM()
            {
                GIMData = new FFTgt_G2H_GIM_GameIDInfo()
                {
                    AssetNumber = model.AssetNo,
                    GMUNumber = model.GmuNo,
                    SerialNumber = model.SerialNo,
                    ManufacturerID = model.ManufacturerID,
                    MACAddress = model.MACAddress,
                    SASVersion = model.SASVersion,
                    GMUVersion = model.GMUVersion,
                }
            };
            msg.AddTarget(gim);
            FFMsgHandlerFactory.Current.Execute(msg);
        }

        #endregion

        #region AddPlayerCard Command
        public ICommand AddPlayerCardCommand { get; private set; }

        private bool CanAddPlayerCardCommand(object parameter)
        {
            return true;
        }

        private void OnAddPlayerCardCommand(object parameter)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "OnAddPlayerCardCommand");

            try
            {
                this.AddCardInfo(_playerCardInfos, 0, () => { return new PlayerCardInfoModel(); }, 1);
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        #endregion

        #region RemovePlayerCard Command
        public ICommand RemovePlayerCardCommand { get; private set; }

        private bool CanRemovePlayerCardCommand(object parameter)
        {
            return true;
        }

        private void OnRemovePlayerCardCommand(object parameter)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "OnRemovePlayerCardCommand");

            try
            {
                this.RemoveSelectedCardNos(_playerCardInfos, 0, () => { return new PlayerCardInfoModel(); });
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        #endregion

        #region AddEmployeeCard Command
        public ICommand AddEmployeeCardCommand { get; private set; }

        private bool CanAddEmployeeCardCommand(object parameter)
        {
            return true;
        }

        private void OnAddEmployeeCardCommand(object parameter)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "OnAddEmployeeCardCommand");

            try
            {
                this.AddCardInfo(_employeeCardInfos, 1, () => { return new EmployeeCardInfoModel(); }, 1);
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        #endregion

        #region RemoveEmployeeCard Command
        public ICommand RemoveEmployeeCardCommand { get; private set; }

        private bool CanRemoveEmployeeCardCommand(object parameter)
        {
            return true;
        }

        private void OnRemoveEmployeeCardCommand(object parameter)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "OnRemoveEmployeeCardCommand");

            try
            {
                this.RemoveSelectedCardNos(_employeeCardInfos, 1, () => { return new EmployeeCardInfoModel(); });
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        #endregion

        #region PlayerCardIn Command
        public ICommand PlayerCardInCommand { get; private set; }

        private bool CanPlayerCardInCommand(object parameter)
        {
            return true;
            //return (this.PlayerCardInfoDisplay != null);
        }

        private void OnPlayerCardInCommand(object parameter)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "OnPlayerCardInCommand");

            try
            {
                this.ExecuteTasksInGmus("Sending Player Card In",
                    (o, g) =>
                    {
                        PlayerCardInfoModel model = parameter as PlayerCardInfoModel;
                        if (model != null)
                        {
                            FFMsgHandlerFactory.Current.Execute(GMUStdEventHelper.PlayerCardIn(g.IPAddress, model.CardNo));
                        }
                    });
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        #endregion

        #region PlayerCardOut Command
        public ICommand PlayerCardOutCommand { get; private set; }

        private bool CanPlayerCardOutCommand(object parameter)
        {
            return true;
            ////return (this.PlayerCardInfoDisplay != null);
        }

        private void OnPlayerCardOutCommand(object parameter)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "OnPlayerCardOutCommand");

            try
            {
                this.ExecuteTasksInGmus("Sending Player Card Out",
                    (o, g) =>
                    {
                        PlayerCardInfoModel model = parameter as PlayerCardInfoModel;
                        if (model != null)
                        {
                            FFMsgHandlerFactory.Current.Execute(GMUStdEventHelper.PlayerCardOut(g.IPAddress, model.CardNo));
                        }
                    });
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        #endregion

        #region EmployeeCardIn Command
        public ICommand EmployeeCardInCommand { get; private set; }

        private bool CanEmployeeCardInCommand(object parameter)
        {
            return true;
            //return (this.EmployeeCardInfoDisplay != null);
        }

        private void OnEmployeeCardInCommand(object parameter)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "OnEmployeeCardInCommand");

            try
            {
                this.ExecuteTasksInGmus("Sending Employee Card In",
                    (o, g) =>
                    {
                        EmployeeCardInfoModel model = parameter as EmployeeCardInfoModel;
                        if (model != null)
                        {
                            FFMsgHandlerFactory.Current.Execute(GMUStdEventHelper.EmployeeCardIn(g.IPAddress, model.CardNo));
                        }
                    });
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        #endregion

        #region EmployeeCardOut Command
        public ICommand EmployeeCardOutCommand { get; private set; }

        private bool CanEmployeeCardOutCommand(object parameter)
        {
            return true;
            ////return (this.EmployeeCardInfoDisplay != null);
        }

        private void OnEmployeeCardOutCommand(object parameter)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "OnEmployeeCardOutCommand");

            try
            {
                this.ExecuteTasksInGmus("Sending Employee Card Out",
                    (o, g) =>
                    {
                        EmployeeCardInfoModel model = parameter as EmployeeCardInfoModel;
                        if (model != null)
                        {
                            FFMsgHandlerFactory.Current.Execute(GMUStdEventHelper.EmployeeCardOut(g.IPAddress, model.CardNo));
                        }
                    });
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        #endregion

        #region EFTBalanceRequest Command
        public ICommand EFTBalanceRequestCommand { get; private set; }

        private bool CanEFTBalanceRequestCommand(object parameter)
        {
            return true;
            //return (this.EFTBalanceRequestfoDisplay != null);
        }

        private void OnEFTBalanceRequestCommand(object parameter)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "OnEFTBalanceRequestCommand");

            try
            {
                string pin = this.ECashPIN;
                this.ExecuteTasksInGmus("Sending Blance Request",
                    (o, g) =>
                    {
                        PlayerCardInfoModel model = parameter as PlayerCardInfoModel;
                        if (model != null)
                        {
                            FFMsgHandlerFactory.Current.Execute(ECashHelper.BalanceRequest(g.IPAddress, model.CardNo, pin));
                        }
                    });
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        #endregion

        #region TicketPrint Command
        public ICommand TicketPrintCommand { get; private set; }

        private bool CanTicketPrintCommand(object parameter)
        {
            return true;
            //return (this.TicketPrintfoDisplay != null);
        }

        private void OnTicketPrintCommand(object parameter)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "OnTicketPrintCommand");

            try
            {
                double amount = TypeSystem.GetValueDouble(this.TicketAmount);
                FF_AppId_TicketTypes ticketType = (this.TicketTypes.CurrentItem as TicketTypeModel).Type;
                this.ExecuteTasksInGmus("Sending Ticket Print...",
                    (o, g) =>
                    {
                        FFMsg_G2H msg = TicketsHelper.PrintTicket(g.IPAddress, amount, ticketType);
                        if (msg != null)
                        {
                            FFMsgHandlerFactory.Current.Execute(msg);
                        }
                    });
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        #endregion

        #region TicketRedeem Command
        public ICommand TicketRedeemCommand { get; private set; }

        private bool CanTicketRedeemCommand(object parameter)
        {
            return true;
            //return (this.TicketRedeemfoDisplay != null);
        }

        private void OnTicketRedeemCommand(object parameter)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "OnTicketRedeemCommand");

            try
            {
                string barcode = this.TicketBarcode;
                this.ExecuteTasksInGmus("Sending Ticket Redeem...",
                    (o, g) =>
                    {
                        FFMsg_G2H msg = TicketsHelper.RedeemTicketRequest(g.IPAddress, barcode);
                        if (msg != null)
                        {
                            FFMsgHandlerFactory.Current.Execute(msg);
                        }
                    });
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        #endregion

        #region TicketVoid Command
        public ICommand TicketVoidCommand { get; private set; }

        private bool CanTicketVoidCommand(object parameter)
        {
            return true;
            //return (this.TicketVoidfoDisplay != null);
        }

        private void OnTicketVoidCommand(object parameter)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "OnTicketVoidCommand");

            try
            {
                string barcode = this.TicketBarcode;
                this.ExecuteTasksInGmus("Sending Ticket Void...",
                    (o, g) =>
                    {
                        FFMsg_G2H msg = TicketsHelper.VoidTicket(g.IPAddress, barcode);
                        if (msg != null)
                        {
                            FFMsgHandlerFactory.Current.Execute(msg);
                        }
                    });
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        #endregion

        #region PostEvent Command
        public ICommand PostEventCommand { get; private set; }

        private bool CanPostEventCommand(object parameter)
        {
            return true;
            //return (this.PostEventfoDisplay != null);
        }

        private void OnPostEventCommand(object parameter)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "OnPostEventCommand");

            try
            {
                FF_AppId_GMUEvent_XCodes excepionCode = FF_AppId_GMUEvent_XCodes.None;

                if (parameter != null)
                {
                    excepionCode = (FF_AppId_GMUEvent_XCodes)TypeSystem.GetValueInt(parameter);
                }
                else
                {
                    if (this.GmuEventTypes == null) return;
                    GmuEventTypeModel model = this.GmuEventTypes.CurrentItem as GmuEventTypeModel;
                    if (model != null)
                    {
                        excepionCode = model.ExceptionCode;
                    }
                }
                if (excepionCode == FF_AppId_GMUEvent_XCodes.None) return;

                this.ExecuteTasksInGmus("Sending Gmu Event...",
                    (o, g) =>
                    {
                        FFMsg_G2H msg = GMUStdEventHelper.PostStandardEvent(g.IPAddress, excepionCode);
                        if (msg != null)
                        {
                            FFMsgHandlerFactory.Current.Execute(msg);
                        }
                    });
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        #endregion

        #region RawRequest Command
        public ICommand RawRequestCommand { get; private set; }

        private bool CanRawRequestCommand(object parameter)
        {
            return true;
            //return (this.RawRequestfoDisplay != null);
        }

        private void OnRawRequestCommand(object parameter)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "OnRawRequestCommand");

            try
            {
                string text = this.RawRequest;
                this.ExecuteTasksInGmus("Sending Raw Request...",
                    (o, g) =>
                    {
                        UdpFreeformEntity msg = CreateRawMessage(text);
                        SocketTransceiver.Current.Send(msg);
                    });
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        #endregion

        public string TicketBarcode
        {
            get { return (string)GetValue(TicketBarcodeProperty); }
            set { SetValue(TicketBarcodeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TicketBarcode.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TicketBarcodeProperty =
            DependencyProperty.Register("TicketBarcode", typeof(string), typeof(MainConfigurationViewModel), new PropertyMetadata(""));

        public string TicketAmount
        {
            get { return (string)GetValue(TicketAmountProperty); }
            set { SetValue(TicketAmountProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TicketAmount.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TicketAmountProperty =
            DependencyProperty.Register("TicketAmount", typeof(string), typeof(MainConfigurationViewModel), new PropertyMetadata(""));

        public string RawRequest
        {
            get { return (string)GetValue(RawRequestProperty); }
            set { SetValue(RawRequestProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RawRequest.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RawRequestProperty =
            DependencyProperty.Register("RawRequest", typeof(string), typeof(MainConfigurationViewModel), new PropertyMetadata(""));

        private DialogResult ShowAsyncDialog1(string caption, AsyncWaitCallback task)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "ShowAsyncDialog"))
            {
                try
                {
                    return WPFExtensions.ShowAsyncDialog(App.MainWindow, caption, _executorServiceAsync, task);
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                    return DialogResult.None;
                }
            }
        }

        private void ExecuteTasksInGmus(string caption, Action<IAsyncProgress2, GIMInformationModel> task)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "Method"))
            {
                try
                {
                    GIMInformationModel gimModel = this.GIMInformationsAll.CurrentItem as GIMInformationModel;
                    if (gimModel != null)
                    {
                        this.ShowAsyncDialog1(caption + "...",
                            (o) =>
                            {
                                IAsyncProgress2 o2 = o as IAsyncProgress2;
                                if (!gimModel.IsAll)
                                {
                                    task(o2, gimModel);
                                }
                                else
                                {
                                    int length = 0;
                                    o2.CrossThreadInvoke(() =>
                                    {
                                        length = this.GIMInformations.Count;
                                    });
                                    Parallel.For(0, length,
                                        (i) =>
                                        {
                                            GIMInformationModel g = null;
                                            o2.CrossThreadInvoke(() =>
                                            {
                                                g = this.GIMInformations[i];
                                            });
                                            task(o2, g);
                                        });
                                }
                            });
                    }
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }
            }
        }

        private void ReloadRecords()
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "ReloadRecords"))
            {
                try
                {
                    // GIM informations
                    _gimInfoAll.Clear();
                    _gimInfoAll.Add(new GIMInformationModel()
                    {
                        DisplayText = " - -  A L L  - - ",
                    });

                    this.GIMInformations.Clear();
                    DataTable dt = _tblGIM.GetAllRecords();
                    if (dt != null)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            GIMInformationModel info = new GIMInformationModel()
                            {
                                RowNo = dr.Field<long>("RowID"),
                                IPAddress = dr.Field<string>("IPAddress"),
                                HashCode = dr.Field<int>("HashCode"),
                                AssetNo = dr.Field<string>("AssetNo"),
                                GmuNo = dr.Field<string>("GmuNo"),
                                SerialNo = dr.Field<string>("SerialNo"),
                                ManufacturerID = dr.Field<string>("ManufacturerID"),
                                MACAddress = dr.Field<string>("MACAddress"),
                                GMUVersion = dr.Field<string>("GMUVersion"),
                                SASVersion = dr.Field<string>("SASVersion"),
                            };
                            _gimInfoAll.AddWithSNo(info);
                            this.GIMInformations.AddWithSNo(info);
                            info._isPropertyModified = false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }
                finally
                {
                    this.GIMInformationsAll.MoveCurrentToFirst();
                }
            }
        }

        private void ReloadCardInfos<T>(ObservableCollection<T> infos, int cardType, Func<T> create)
            where T : CardInfoModel
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "ReloadCardInfos"))
            {
                try
                {
                    // Card Nos
                    infos.Clear();
                    var dt = _tblCardNos.GetRecordsByCardType(cardType);
                    if (dt != null)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            T info = create();

                            info.RowNo = dr.Field<long>("RowID");
                            info.HashCode = dr.Field<int>("HashCode");
                            info.CardNo = dr.Field<string>("CardNo");

                            infos.Add(info);
                            info._isPropertyModified = false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }
            }
        }

        private void ReloadPlayerCardInfos()
        {
            this.ReloadCardInfos(_playerCardInfos, 0, () => { return new PlayerCardInfoModel(); });
        }

        private void ReloadEmployeeCardInfos()
        {
            this.ReloadCardInfos(_employeeCardInfos, 1, () => { return new EmployeeCardInfoModel(); });
        }

        private void AddCardInfo<T>(ObservableCollection<T> infos, int cardType, Func<T> create, int cardPrefix)
            where T : CardInfoModel
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "AddCardInfo"))
            {
                try
                {
                    long maxCardNo = _tblCardNos.GetMaxRowID();
                    T info = create();
                    info.CardNo = string.Format("{0}{1:D9}", cardPrefix, (maxCardNo + 1));

                    int hashCode = info.GetHashCode();
                    _tblCardNos.Add(hashCode, info.CardNo, cardType);
                    maxCardNo = _tblCardNos.GetMaxRowID();
                    info.RowNo = maxCardNo;
                    infos.Add(info);
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }
            }
        }

        private void RemoveSelectedCardNos<T>(ObservableCollection<T> infos, int cardType, Func<T> create)
            where T : CardInfoModel
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "ReloadCardInfos"))
            {
                try
                {
                    // Card Nos
                    var selectedCards = (from c in infos
                                         where c.IsSelected
                                         select c);
                    if (selectedCards != null)
                    {
                        foreach (var selectedCard in selectedCards)
                        {
                            _tblCardNos.Delete(selectedCard.RowNo);
                        }
                    }
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }
                finally
                {
                    this.ReloadCardInfos<T>(infos, cardType, create);
                }
            }
        }


        private void InitSocket()
        {
            _socket = new SocketTransceiver(_executorService, IPADDR, 1031);
            _socket.ReceiveUdpEntityData += OnReceiveUdpEntityData;
            _socket.AfterSendUdpEntityData += SocketTransceiver_AfterSendUdpEntityData;
            _socket.Start();
        }

        private void OnReceiveUdpEntityData(UdpFreeformEntity udpEntity)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "Method"))
            {
                try
                {
                    Dispatcher.Invoke(new Action(() =>
                    {
                        this.AddToRawDataCollection<UdpRawResponseItemModel>(this.ResponseItems, udpEntity,
                                   () => { return new UdpRawResponseItemModel(); });
                    }));
                    FFMsgHandlerFactory.Current.Execute(udpEntity.EntityData as IFreeformEntity_Msg);
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }
            }
        }

        private T AddToRawDataCollection<T>(ObservableCollection<T> collection,
                                                UdpFreeformEntity udpEntity,
                                                Func<T> create)
            where T : UdpRawMessageItemModel
        {
            string text = FreeformHelper.GetConvertBytesToHexString(udpEntity.RawData, string.Empty);

            if (collection.Count >= 32767)
                collection.Clear();

            T item = create();

            item.IPAddress = udpEntity.AddressString;
            item.SNo = this.ResponseItems.Count + 1;
            item.ProcessedTime = udpEntity.ProcessDate;
            item.RawDataInHex = text;
            FreeformEntity_Msg msg = (udpEntity.EntityData as FreeformEntity_Msg);
            if (msg != null)
            {
                item.SessionId = msg.SessionID.ToString();
                item.TransactionId = msg.TransactionID;
                item.TargetName = msg.CombinedTargetNames;
            }

            collection.Add(item);
            return item;
        }

        public void RawMessage(ExCommsRawMessageEntity entity)
        {
            throw new NotImplementedException();
        }

        public void ExecutionStepSubscribed(List<ExCommsExecutionStepEntity> entity)
        {
            throw new NotImplementedException();
        }

        public void ExecutionStepChanged(ExCommsExecutionStepEntity entity)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "ExecutionStepChanged"))
            {
                try
                {
                    if (entity != null)
                    {
                        Dispatcher.Invoke(new Action(() =>
                        {
                            ExecutionStepChangedModelCollection steps = null;
                            if (_executionSteps.ContainsKey(entity.GmuIpAddress))
                            {
                                steps = _executionSteps[entity.GmuIpAddress];
                            }
                            else
                            {
                                _executionSteps.Add(entity.GmuIpAddress, (steps = new ExecutionStepChangedModelCollection()));
                            }
                            steps.Add(new ExecutionStepChangedModel()
                            {
                                GmuIpAddress = entity.GmuIpAddress,
                                Steps = entity.Steps,
                            });
                        }));
                    }
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }
            }
        }

        private static UdpFreeformEntity CreateRawMessage(string input)
        {
            byte[] buffer = GetBuffer(input);
            UdpFreeformEntity entity = FreeformEntityFactory.CreateUdpEntity(FF_FlowDirection.G2H, buffer);
            entity.RawData = buffer;
            return entity;
        }

        private static byte[] GetBuffer(string input)
        {
            int idx = input.IndexOf("[");
            if (idx > 0)
            {
                input = input.Substring(idx, input.Length - idx);
            }
            string data = input.Replace("[", "").Replace("]", ",");
            string[] splitted = data.Split(new char[] { ',' });

            byte[] buffer = new byte[splitted.Length - 1];
            for (int i = 0; i < splitted.Length - 1; i++)
            {
                int val = 0;
                try
                {
                    val = Convert.ToInt32(splitted[i], 16);
                }
                catch
                {
                    val = Convert.ToInt32(splitted[i], 10);
                }
                buffer[i] = (byte)val;
            }
            return buffer;
        }

        public bool ProcessMessageG2H(ILogMethod method, FFMsg_G2H message)
        {
            SocketTransceiver.Current.Send(message);
            return true;
        }

        void SocketTransceiver_AfterSendUdpEntityData(UdpFreeformEntity udpEntity)
        {
            Dispatcher.Invoke(new Action(() =>
            {
                var item = this.AddToRawDataCollection<UdpRawRequestItemModel>(this.RequestItems, udpEntity,
                 () => { return new UdpRawRequestItemModel(); });
                this.AddToRawDataCollection<UdpRawRequestItemModel>(_requestByGmu[item], udpEntity,
                () => { return new UdpRawRequestItemModel(); });
            }));
        }

        public bool ProcessMessageH2G(ILogMethod method, FFMsg_H2G message)
        {
            try
            {
                //if (message.SessionID == FF_AppId_SessionIds.GIM &&
                //    (message.PrimaryTarget.PrimaryTarget as FFTgt_H2G_GIM_GameIDInfo).DisplayMessage == "Success!")
                //{
                //    FFMsg_G2H msg = FreeformEntityFactory.CreateEntity<FFMsg_G2H>(FF_FlowDirection.G2H,
                //       new FFCreateEntityRequest_G2H()
                //       {
                //           Command = FF_AppId_G2H_Commands.ResponseRequest,
                //           MessageType = FF_AppId_G2H_MessageTypes.FreeForm,
                //           SessionID = FF_AppId_SessionIds.Security,
                //           TransactionID = 0xB1,
                //           IPAddress = message.IpAddress,
                //       });
                //    FFTgt_B2B_Security tgt = new FFTgt_B2B_Security()
                //    {
                //        SecurityData = new FFTgt_B2B_Security_KeyExchange_Request(),
                //    };
                //    msg.AddTarget(tgt);
                //    FFMsgHandlerFactory.Current.Execute(msg);
                //}
            }
            catch (Exception ex)
            {
                method.Exception(ex);
            }
            return true;
        }

        bool IFFMsgTransmitter.ProcessMessage(FFMsg_G2H message, IList<Contracts.DTO.Monitor.MonitorEntity_MsgTgt> monitorTargets)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "ProcessMessage"))
            {
                bool result = default(bool);

                try
                {
                    switch (message.SessionID)
                    {
                        case FF_AppId_SessionIds.Tickets:
                            {
                                FFTgt_B2B_TicketInfo target = message.GetTarget<FFTgt_B2B_TicketInfo>();
                                if (target != null)
                                {
                                    // print
                                    FFTgt_G2H_Ticket_Printed_Request printedTicket = target.GetTarget<FFTgt_G2H_Ticket_Printed_Request>();
                                    if (printedTicket != null)
                                    {
                                        Dispatcher.Invoke(new Action(() =>
                                        {
                                            TicketInfoModel model = new TicketInfoModel()
                                            {
                                                Barcode = printedTicket.BarCode,
                                                Amount = printedTicket.Amount,
                                                TicketType = printedTicket.Type,
                                                ProcessType = "Print"
                                            };
                                            _processedTickets.AddWithSNo(model);
                                        }));
                                    }
                                }
                            }
                            break;

                        case FF_AppId_SessionIds.GameMessage:
                            {
                                //FFTgt_B2B_TicketInfo target = message.GetTarget<FFTgt_B2B_TicketInfo>();
                                //if (target != null)
                                //{
                                //    // print
                                //    FFTgt_G2H_Ticket_Printed_Request printedTicket = target.GetTarget<FFTgt_G2H_Ticket_Printed_Request>();
                                //    if (printedTicket != null)
                                //    {
                                //        Dispatcher.Invoke(new Action(() =>
                                //        {
                                //            TicketInfoModel model = new TicketInfoModel()
                                //            {
                                //                Barcode = printedTicket.BarCode,
                                //                Amount = printedTicket.Amount,
                                //                TicketType = printedTicket.Type,
                                //                ProcessType = "Print"
                                //            };
                                //            _processedTickets.AddWithSNo(model);
                                //        }));
                                //    }
                                //}
                            }
                            break;

                        default:
                            break;
                    }
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }
                finally
                {
                    SocketTransceiver.Current.Send(message);
                    result = true;
                }

                return result;
            }

            return true;
        }

        bool IFFMsgTransmitter.ProcessMessage(FFMsg_H2G message)
        {
            try
            {
                FFMsg_G2H msgToSend = null;

                switch (message.SessionID)
                {
                    case FF_AppId_SessionIds.Tickets:
                        {
                            FFTgt_B2B_TicketInfo target = message.GetTarget<FFTgt_B2B_TicketInfo>();
                            if (target != null)
                            {
                                // redeem
                                FFTgt_H2G_Ticket_Redemption_Response redeemTicket = target.GetTarget<FFTgt_H2G_Ticket_Redemption_Response>();
                                if (redeemTicket != null)
                                {
                                    Dispatcher.Invoke(new Action(() =>
                                    {
                                        TicketInfoModel model = new TicketInfoModel()
                                        {
                                            Barcode = redeemTicket.Barcode,
                                            Amount = redeemTicket.Amount,
                                            TicketType = redeemTicket.Type,
                                            ProcessType = "Redeem"
                                        };
                                        _processedTickets.AddWithSNo(model);
                                    }));
                                }
                            }
                        }
                        break;

                    case FF_AppId_SessionIds.GameMessage:
                        {
                            FFTgt_H2G_GameMessage_SASCommand target = message.GetTarget<FFTgt_H2G_GameMessage_SASCommand>();
                            if (target != null)
                            {
                                switch ((FF_AppId_LongPollCodes)target.LongPollCommand)
                                {
                                    case FF_AppId_LongPollCodes.LPC_MachineDisable:
                                        msgToSend = LongPollHelper.EnableDisableResponse(message.IpAddress, false);
                                        break;

                                    case FF_AppId_LongPollCodes.LPC_MachineEnable:
                                        msgToSend = LongPollHelper.EnableDisableResponse(message.IpAddress, true);
                                        break;

                                    case FF_AppId_LongPollCodes.LPC_LongPoll_51:
                                        msgToSend = LongPollHelper.TotalGamesResponse(message.IpAddress, 456);
                                        break;

                                    default:
                                        break;
                                }
                            }
                        }
                        break;

                    default:
                        break;
                }

                if (msgToSend != null)
                {
                    FFMsgHandlerFactory.Current.Execute(msgToSend);
                }
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
            }
            return true;
        }
    }
}
