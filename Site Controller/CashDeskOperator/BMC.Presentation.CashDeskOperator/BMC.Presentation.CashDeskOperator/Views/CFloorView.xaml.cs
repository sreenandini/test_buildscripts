using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Threading;
using BMC.CashDeskOperator;
using BMC.CashDeskOperator.BusinessObjects;
using BMC.Common.ConfigurationManagement;
using BMC.Common.ExceptionManagement;
using BMC.Common.LogManagement;
using BMC.Presentation.Helper_classes;
using HC = BMC.Presentation.Helper_classes;
using BMC.Presentation.POS;
using BMC.Presentation.POS.Helper_classes;
using BMC.Security;
using BMC.Transport;
using System.Windows.Media.Animation;
using System.ComponentModel;
using Audit.BusinessClasses;
using Audit.Transport;
using BMC.Common.Utilities;
using System.Threading;
using System.Diagnostics;
using System.Windows.Media;
using BMCIPC;
using BMC.CoreLib.Concurrent;
using BMCIPC.CDO;
using BMC.Presentation.POS.UserControls;
using BMC.CoreLib;
using BMC.CoreLib.Collections;
using System.Xml.Linq;

namespace BMC.Presentation
{
    public partial class CFloorView : IDisposable, INotifyPropertyChanged
    {
        #region Variables
        public event CancelEventHandler Exit;
        private readonly IAnalysis _analysisBusinessObject = AnalysisBusinessObject.CreateInstance();
        public readonly DispatcherTimer _timer;
        private double panelHeight;
        List<FloorStatusData> barPositions;
        private bool isPostBack = false;
        private bool _SortChecked = false;
        private bool _Empty = true;
        private bool _Carded = true;
        private bool _EmpCarded = true;
        private bool _GameInstallationAAMSPending = true;
        private bool _GamePlay = true;
        private bool _NotinPlay = true;
        private bool _SlotInstallationAAMSPending = true;
        private bool _VLNoMeters = true;
        private bool _VLTRemoved = true;
        private bool _VLTunderMaintenance = true;
        private bool _FloatUnDeclared = true;
        private bool _UnclearedEvent = true;
        private bool _ClearedEvent = true;
        private bool _ForceFinalCollection = true;
        private bool _StackerRemoved = true;
        private bool _GMUConnectivity = true;
        private DateTime _slotRefreshTime;
        private bool _CommsDown = true;
        private bool _GameDown = true;
        
#if !NO_PERFORMANCE_FIX
        private ManualResetEvent _eventShutdown = null;
        internal object _clearLock = new object();
        private ThinEvent _teFloorActive = new ThinEvent(true);
        internal ThinEvent _teFloorPositions = new ThinEvent(true);
#endif

#if !FLOORVIEW_REFRESH_NEW
        Thread _thCheckRemoteObj = null;
#else
        private IExecutorService _executorFloorView = ExecutorServiceFactory.CreateExecutorService();
        private CDOCentralServerClientSubcriber _clientSubcriber = null;
        private int _userID = CDOSettings.Current.LoggedInUser.SecurityUserID;
        private FloorPositionKeyDto _floorKey = null;
#endif
        #endregion

        #region Code added by A.Vinod Kumar for Slot machine grouping

        internal SlotMachineCollection _collection = null;

        private class LegendInfo
        {
            public LegendInfo(SlotMachineStatus status, string resourceName)
            {
                this.Status = status;
                this.Description = Application.Current.FindResource(resourceName).ToString();
            }

            public SlotMachineStatus Status { get; set; }

            public string Description { get; set; }
        }

        private IDictionary<string, LegendInfo> _slotMachineStatus = null;
        private IDictionary<SlotMachineStatus, bool> _slotStatusFilter = null;

        internal Brush _slotMachineGroupBgBrush = null;
        internal Brush _slotMachineGroupFgBrush = null;

        private FloorPositionDto _floorPosition = new FloorPositionDto();
        internal bool _floorViewUserLocations = false;

        private void InitSlotStatusLegendDetails()
        {
            _slotMachineStatus = new SortedDictionary<string, LegendInfo>()
            {
                { "EmptyPosition", new LegendInfo(SlotMachineStatus.EmptyPosition, "CFloorView_xaml_chkEmpty") },
                { "NotInPlay", new LegendInfo(SlotMachineStatus.NotInPlay, "CFloorView_xaml_chkNotInPlay") },
                { "InstallationCompletedNonMetered", new LegendInfo(SlotMachineStatus.InstallationCompletedNonMetered, "CFloorView_xaml_chkVLNoMeters") },
                { "NonCardedPlay", new LegendInfo(SlotMachineStatus.NonCardedPlay, "CFloorView_xaml_chkHandpay") },
                { "NonCardedHandPay",new LegendInfo(SlotMachineStatus.NonCardedHandPay, "CFloorView_xaml_chkHandpay") },
                { "CardedPlay", new LegendInfo(SlotMachineStatus.CardedPlay, "CFloorView_xaml_chkCarded") },
                { "EmpCardedPlay", new LegendInfo(SlotMachineStatus.EmpCardedPlay, "CFloorView_xaml_chkEmpCarded") },
                { "CardedHandPay", new LegendInfo(SlotMachineStatus.CardedHandPay, "CFloorView_xaml_chkCarded") },
                { "MachineInPlay", new LegendInfo(SlotMachineStatus.MachineInPlay, "CFloorView_xaml_chkHandpay") },
                { "GoldClubCardedPlay", new LegendInfo(SlotMachineStatus.GoldClubCardedPlay, "CFloorView_xaml_chkEmpty") },
                { "VLTInstallationAAMSPending", new LegendInfo(SlotMachineStatus.VLTInstallationAAMSPending, "CFloorView_xaml_chkSlotInstallationAAMSPending") },
                { "GameInstallationAAMSPending", new LegendInfo(SlotMachineStatus.GameInstallationAAMSPending, "CFloorView_xaml_chkGameInstallationAAMSPending") },
                { "VLTunderMaintenance", new LegendInfo(SlotMachineStatus.VLTunderMaintenance, "CFloorView_xaml_chkVLTunderMaintenance") },
                { "VLTunderUnauthorizedMaintenance", new LegendInfo(SlotMachineStatus.VLTunderUnauthorizedMaintenance, "CFloorView_xaml_chkEmpty") },
                { "FloatCollection", new LegendInfo(SlotMachineStatus.FloatCollection, "CFloorView_xaml_chkVLTRemoved") },
                { "ForceFinalCollection", new LegendInfo(SlotMachineStatus.ForceFinalCollection, "CFloorView_xaml_chkForceFinalCollection") },
                { "GMUConnectivity", new LegendInfo(SlotMachineStatus.GMUConnectivity, "CFloorView_xaml_chkGMUCommunication") },
                { "CommsDown", new LegendInfo(SlotMachineStatus.CommsDown, "CFloorView_xaml_chkCommsDwn") },
                { "GameDown", new LegendInfo(SlotMachineStatus.GameDown, "CFloorView_xaml_chkGameDwn") },
                { "StackerRemoved", new LegendInfo(SlotMachineStatus.StackerRemoved, "CFloorView_xaml_chkStackerRemoved") },
            };

            this.DisplayGroupMachines = BMC.CoreLib.Extensions.GetAppSettingValueBool("DisplayGroupMachines", false);
            chkGroupBy.Visibility = (this.DisplayGroupMachines ? Visibility.Visible : Visibility.Collapsed);            

            _slotStatusFilter = new SortedDictionary<SlotMachineStatus, bool>();
            foreach (var item in Enum.GetValues(typeof(SlotMachineStatus)))
            {
                _slotStatusFilter.Add((SlotMachineStatus)item, true);
            }

            LinearGradientBrush brush = new LinearGradientBrush();
            brush.StartPoint = new Point(0, 0.5);
            brush.StartPoint = new Point(1, 0.5);
            brush.GradientStops.Add(new GradientStop(Colors.White, 0));
            brush.GradientStops.Add(new GradientStop(Color.FromArgb(0xFF, 0xB8, 0xCD, 0xCE), 0.7));
            brush.GradientStops.Add(new GradientStop(Color.FromArgb(0xFF, 0xDB, 0xE6, 0xE6), 0.9));
            _slotMachineGroupBgBrush = brush;
            _slotMachineGroupFgBrush = new SolidColorBrush(Colors.Black);

            _floorKey = new FloorPositionKeyDto(_userID, 0);
        }

        public bool DisplayGroupMachines { get; set; }

        internal string GetSlotStatusDecription(string slotStatus)
        {
            if (_slotMachineStatus.ContainsKey(slotStatus))
                return _slotMachineStatus[slotStatus].Description;
            else
                return string.Empty;
        }

        internal SlotMachineStatus GetSlotStatus(string slotStatus)
        {
            if (_slotMachineStatus.ContainsKey(slotStatus))
                return _slotMachineStatus[slotStatus].Status;
            else
                return SlotMachineStatus.EmptyPosition;
        }

        public bool GroupByMachines
        {
            get { return (bool)GetValue(GroupByMachinesProperty); }
            set { SetValue(GroupByMachinesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for GroupByMachines.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GroupByMachinesProperty =
            DependencyProperty.Register("GroupByMachines", typeof(bool), typeof(CFloorView), new UIPropertyMetadata(false, new PropertyChangedCallback(OnGroupByMachinesPropertyChanged)));

        private static void OnGroupByMachinesPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            try
            {
                CFloorView fv = d as CFloorView;
                if (fv != null)
                {
                    bool changed = (bool)e.NewValue;
                    fv.chkRePosition.Visibility = (!changed ? Visibility.Visible : Visibility.Hidden);
                    if (changed)
                    {
                        fv.chkRePosition.IsChecked = false;
                    }
                    fv.FilterAndSortSlotMachines();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void InitClientSubscriber()
        {
            _floorViewUserLocations = BMC.CoreLib.Extensions.GetAppSettingValueBool("FloorViewUserLocations", false);
            _clientSubcriber = new CDOCentralServerClientSubcriber(true);
            _clientSubcriber.SetSlotStatusEvent += new SetSlotStatusHandler(this.SetSlotStatus);
            _clientSubcriber.SubscribeSetSlotStatus(_executorFloorView, _teFloorActive.WaitHandle);
            if (_floorViewUserLocations)
            {
                _clientSubcriber.SetFloorPositionEvent += new SetFloorPositionHandler(this.SetFloorPosition);
                _clientSubcriber.SubscribeSetFloorPosition(_executorFloorView, _teFloorActive.WaitHandle);
            }
        }

        private void DestroyClientSubscriber()
        {
            _executorFloorView.Shutdown();
            _clientSubcriber.SetSlotStatusEvent -= this.SetSlotStatus;
            _clientSubcriber.SetFloorPositionEvent -= this.SetFloorPosition;
            _clientSubcriber.Dispose();
        }

        private void InitFloorPosition()
        {
            _teFloorPositions.Reset();
            _floorPosition.Clear();
            if (_floorViewUserLocations)
            {
                ThreadPool.QueueUserWorkItem((c) =>
                {
                    var positions = _clientSubcriber.GetFloorPosition(CDOSettings.Current.LoggedInUser.SecurityUserID);
                    this.SetFloorPosition(positions);

                    _teFloorPositions.Set();
                });
            }
        }

        private void SetFloorPosition(FloorPositionResponse positions)
        {
            try
            {
                if (positions != null)
                {
                    this.SetFloorPosition(positions.Response);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void SetFloorPosition(FloorPositionDto positions)
        {
            _floorPosition.Copy(positions);
        }

        internal FloorPositionDataDto GetFloorPosition(int barPositionID)
        {
            _floorKey.BarPosition = barPositionID;
            if (_floorPosition.ContainsKey(_floorKey))
            {
                return _floorPosition[_floorKey];
            }
            return null;
        }

        internal void NotifyFloorPosition(FloorPositionDto positions)
        {
            _clientSubcriber.NotifyFloorPosition(positions);
        }

        private void InitAssetNameDisplay()
        {
            try
            {
                if (Settings.DisplayGameNameInFloorView)
                {
                    chkSortAsset.Content = this.FindResource("CFloorView_xaml_chkSortGameName").ToString();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        #endregion

        #region Constructor
        public CFloorView()
        {

            LogManager.WriteLog("CFloorView() (START)", LogManager.enumLogLevel.Info);
            InitializeComponent();

            _collection = new SlotMachineCollection(this);
            if (!SecurityHelper.HasAccess("CashdeskOperator.MainScreen.cs.Floor.View.UnLockPosition"))
            {
                chkRePosition.Visibility = Visibility.Hidden;
                spSortOptions.Visibility = Visibility.Hidden;
            }

            this.DataContext = this;
            chkSortPos.IsChecked = (Settings.SiteFloorViewState == "AUTOMATIC");
            if (chkSortPos.IsChecked == true)
            {
                oCommonUtilities.CreateInstance().UpdateSettings("SiteFloorViewState", "MANNUAL");
            }

            pnlFloor.AllowDragging = false;

            chkSortPos.IsChecked = AppSettings.FlrView_SortBy_Position;
            chkSortAsset.IsChecked = AppSettings.FlrView_SortBy_Asset;
            chkStackerRemovedContent.Visibility = Settings.AutoDropEnabled ? Visibility.Visible : Visibility.Collapsed;
            chkStackerRemoved.Visibility = Settings.AutoDropEnabled ? Visibility.Visible : Visibility.Collapsed;

#if! FLOORVIEW_REFRESH_NEW
            _eventShutdown = new ManualResetEvent(false);
            App.siteLicensingClient.SendToServer(App.siteLicensingClientObj, null);
            App.siteLicensingClientObj.ClientReceived += new ClientReceivedEvent(clientObj_FloorClientReceived);            
            _thCheckRemoteObj = new Thread(CheckRemoteObjectAlive);            
#else
            this.InitClientSubscriber();
#endif

            InitSlotStatusLegendDetails();
            LogManager.WriteLog("CFloorView() (END)", LogManager.enumLogLevel.Info);
            this.InitAssetNameDisplay();
        }

        #endregion

        #region Properties
        public bool FloatUnDeclared
        {
            get { return _FloatUnDeclared; }
            set
            {
                _FloatUnDeclared = value;
                OnPropertyChanged("FloatUnDeclared"); FilterAndSortSlotMachines();
            }
        }
        public bool UnclearedEvent
        {
            get { return _UnclearedEvent; }
            set
            {
                _UnclearedEvent = value;
                OnPropertyChanged("UnclearedEvent"); FilterAndSortSlotMachines();
            }
        }

        public bool ClearedEvent
        {
            get { return _ClearedEvent; }
            set
            {
                _ClearedEvent = value;
                OnPropertyChanged("ClearedEvent"); FilterAndSortSlotMachines();
            }
        }

        public bool Empty
        {
            get { return _Empty; }
            set
            {
                _Empty = value;
                OnPropertyChanged("Empty"); FilterAndSortSlotMachines();
            }
        }
        public bool Carded
        {
            get { return _Carded; }
            set
            {
                _Carded = value;
                OnPropertyChanged("Carded"); FilterAndSortSlotMachines();
            }
        }

        public bool EmpCarded
        {
            get { return _EmpCarded; }
            set
            {
                _EmpCarded = value;
                OnPropertyChanged("EmpCarded"); FilterAndSortSlotMachines();
            }
        }

        public bool GMUConnectivity
        {
            get { return _GMUConnectivity; }
            set
            {
                _GMUConnectivity = value;
                OnPropertyChanged("GMUConnectivity"); FilterAndSortSlotMachines();
            }
        }
        public bool ForceFinalCollection
        {
            get { return _ForceFinalCollection; }
            set
            {
                _ForceFinalCollection = value;
                OnPropertyChanged("ForceFinalCollection"); FilterAndSortSlotMachines();
            }
        }

        public bool StackerRemoved
        {
            get { return _StackerRemoved; }
            set
            {
                _StackerRemoved = value;
                OnPropertyChanged("StackerRemoved"); FilterAndSortSlotMachines();
            }
        }

        public bool GameInstallationAAMSPending
        {
            get { return _GameInstallationAAMSPending; }
            set
            {
                _GameInstallationAAMSPending = value;
                OnPropertyChanged("GameInstallationAAMSPending"); FilterAndSortSlotMachines();
            }
        }
        public bool GamePlay
        {
            get { return _GamePlay; }
            set
            {
                _GamePlay = value;
                OnPropertyChanged("GamePlay"); FilterAndSortSlotMachines();
            }
        }
        public bool NotinPlay
        {
            get { return _NotinPlay; }
            set
            {
                _NotinPlay = value;
                OnPropertyChanged("NotinPlay"); FilterAndSortSlotMachines();
            }
        }
        public bool SlotInstallationAAMSPending
        {
            get { return _SlotInstallationAAMSPending; }
            set
            {
                _SlotInstallationAAMSPending = value;
                OnPropertyChanged("SlotInstallationAAMSPending"); FilterAndSortSlotMachines();
            }
        }
        public bool VLNoMeters
        {
            get { return _VLNoMeters; }
            set
            {
                _VLNoMeters = value;
                OnPropertyChanged("VLNoMeters"); FilterAndSortSlotMachines();
            }
        }
        public bool VLTRemoved
        {
            get { return _VLTRemoved; }
            set
            {
                _VLTRemoved = value;
                OnPropertyChanged("VLTRemoved"); FilterAndSortSlotMachines();
            }
        }
        public bool VLTunderMaintenance
        {
            get { return _VLTunderMaintenance; }
            set
            {
                _VLTunderMaintenance = value;
                OnPropertyChanged("VLTunderMaintenance"); FilterAndSortSlotMachines();
            }
        }

        public bool CommsDown
        {
            get { return _CommsDown; }
            set
            {
                _CommsDown = value;
                OnPropertyChanged("CommsDown"); FilterAndSortSlotMachines();
            }
        }

        public bool GameDown
        {
            get { return _GameDown; }
            set
            {
                _GameDown = value;
                OnPropertyChanged("GameDown"); FilterAndSortSlotMachines();
            }
        }
        #endregion

        #region Triggers
        private EventTrigger OpenAnimation(RoutedEvent RE)
        {
            EventTrigger eventTrigger = new EventTrigger(RE);
            BeginStoryboard beginStoryBoard = new BeginStoryboard();
            Storyboard storyBoard = new Storyboard();
            DoubleAnimation doubleAnimation = new DoubleAnimation();
            doubleAnimation.From = 0;
            doubleAnimation.To = panelHeight;
            doubleAnimation.Duration = new Duration(new TimeSpan(5000000));
            Storyboard.SetTargetName(doubleAnimation, "spPanel");
            Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath("(StackPanel.Height)"));
            storyBoard.Children.Add(doubleAnimation);
            beginStoryBoard.Storyboard = storyBoard;
            eventTrigger.Actions.Add(beginStoryBoard);
            return eventTrigger;
        }

        private EventTrigger CloseAnimation(RoutedEvent RE)
        {
            EventTrigger eventTrigger = new EventTrigger(RE);
            BeginStoryboard beginStoryBoard = new BeginStoryboard();
            Storyboard storyBoard = new Storyboard();
            DoubleAnimation doubleAnimation = new DoubleAnimation();
            doubleAnimation.From = panelHeight;
            doubleAnimation.To = 0;
            doubleAnimation.Duration = new Duration(new TimeSpan(5000000));
            Storyboard.SetTargetName(doubleAnimation, "spPanel");
            Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath("(StackPanel.Height)"));
            storyBoard.Children.Add(doubleAnimation);
            beginStoryBoard.Storyboard = storyBoard;
            eventTrigger.Actions.Add(beginStoryBoard);
            return eventTrigger;
        }

        private void CloseLegendPanel()
        {
            try
            {
                if (spPanel.Height > 0)
                {
                    Storyboard storyBoard = new Storyboard();
                    DoubleAnimation doubleAnimation = new DoubleAnimation();
                    doubleAnimation.From = panelHeight;
                    doubleAnimation.To = 0;
                    doubleAnimation.Duration = new Duration(new TimeSpan(5000000));
                    Storyboard.SetTarget(doubleAnimation, spPanel);
                    Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath("(StackPanel.Height)"));
                    storyBoard.Children.Add(doubleAnimation);
                    storyBoard.Begin();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        #endregion

        #region Events

        private void ObjPosDetailsExitClicked(object sender, EventArgs e)
        {
            LayoutRoot.Children.Remove(sender as UIElement);
            clientObj_FloorClientReceived(null);
            this.SetMessageBoxOwner();

            // WPF PERFORMANCE ISSUE : Not Removing Event Handlers on Objects may Keep Objects Alive
            if (sender != null)
            {
                if (sender is PosDetails)
                {
                    try
                    {
                        ((PosDetails)sender).Exit -= ObjPosDetailsExitClicked;
                        LogManager.WriteLog("|=> PosDetails Exit Event successfully removed.", LogManager.enumLogLevel.Info);
                    }
                    catch { }
                }
                else if (sender is EnrollmentWnd)
                {
                    try
                    {
                        ((EnrollmentWnd)sender).Exit -= ObjPosDetailsExitClicked;
                        LogManager.WriteLog("|=> EnrollmentWnd Exit Event successfully removed.", LogManager.enumLogLevel.Info);
                    }
                    catch { }
                }
            }
            this.ChildWindow = null;
        }

        /// <summary>
        /// Sets the slot mouse up event.
        /// </summary>
        /// <param name="newMachine">The new machine.</param>
        internal void SetSlotMouseUpEvent(SlotMachine newMachine)
        {
            // unsubscribe the existing event
            this.ClearSlotMouseUpEvent(newMachine);

            try
            {
                newMachine.MouseLeftButtonUp += this.SlotMouseUpEvent;
                newMachine.MouseLeftButtonUp += this.txtLegend__MouseDown;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        /// <summary>
        /// Clears the slot mouse up event.
        /// </summary>
        /// <param name="newMachine">The new machine.</param>
        internal void ClearSlotMouseUpEvent(SlotMachine newMachine)
        {
            try
            {
                newMachine.MouseLeftButtonUp -= this.SlotMouseUpEvent;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void SlotMouseUpEvent(object sender, MouseButtonEventArgs e)
        {
            int machineStatusFlag = 0;
            string siteCode = string.Empty;
            bool needRefresh = false;

            try
            {
                SlotMachine _slotMachine = (SlotMachine)sender;

                try
                {
                    LogManager.WriteLog("Inside SlotMouseUpEvent for : " + _slotMachine.SlotNumberString, LogManager.enumLogLevel.Info);
                }
                catch { }

                if (pnlFloor.IsElementBeingDragged)
                {
                    bool isSaved = false;
                    chkSortPos.IsChecked = false;
                    chkSortAsset.IsChecked = false;
                    SaveFloorPosition(_slotMachine, false, ref isSaved);
                    if (isSaved) CDOSettings.Current.Save();
                    return;
                }

                if (!SecurityHelper.HasAccess("BMC.Presentation.PosDetails"))
                {
                    MessageBox.ShowBox("MessageID324", BMC_Icon.Warning, BMC_Button.OK);
                    return;
                }

                if (_slotMachine.Status == SlotMachineStatus.InstallationCompletedNonMetered
                        || _slotMachine.Status == SlotMachineStatus.GameInstallationAAMSPending
                        || _slotMachine.Status == SlotMachineStatus.ForceFinalCollection)
                {
                    if (!Security.SecurityHelper.HasAccess("CashdeskOperator.FloorView.cs.RemoveMachine"))
                    {
                        MessageBox.ShowBox("MessageID328", BMC_Icon.Information, BMC_Button.OK);
                        return;
                    }

                    if (_slotMachine.Status == SlotMachineStatus.ForceFinalCollection)
                    {
                        if (MessageBox.ShowBox("MessageID379", BMC_Icon.Information, BMC_Button.OKCancel)
                            == System.Windows.Forms.DialogResult.OK)
                        {
                            HandpayBusinessObject.CreateInstance().ProcessHandPayOnDenomChange(_slotMachine.SlotNumberString,
                                Security.SecurityHelper.CurrentUser.User_No);
                            Exit.Invoke(this, new CancelEventArgs());
                        }
                        return;
                    }

                    if (_slotMachine.Status == SlotMachineStatus.InstallationCompletedNonMetered)
                    {
                        if (MessageBox.ShowBox("MessageID323", BMC_Icon.Information, BMC_Button.OKCancel)
                            == System.Windows.Forms.DialogResult.Cancel)
                            return;
                    }

                    Cursor = System.Windows.Input.Cursors.Wait;

                    InstallationDataContext objRemoveContext =
                                            new InstallationDataContext(oCommonUtilities.CreateInstance().GetConnectionString());
                    int nDisMachine = 0;

                    foreach (var IP in objRemoveContext.GetDisableMachine(_slotMachine.InstallationNo))
                    {
                        nDisMachine = IP.DisMachine;
                    }

                    EnrollmentErrorCodes ErrorCode = EnrollmentBusinessObject.CreateInstance().RemoveMachine(
                                            _slotMachine.InstallationNo, machineStatusFlag, siteCode, nDisMachine);
                    needRefresh = true;
                    Cursor = System.Windows.Input.Cursors.Arrow;

                    switch (ErrorCode)
                    {
                        case EnrollmentErrorCodes.DatabaseError:
                            {
                                MessageBox.ShowBox("MessageID206");
                                Audit_Error("Database Error", _slotMachine.AssetNumber);
                                break;
                            }
                        case EnrollmentErrorCodes.EnterpriseWebServiceCommunicationFailure:
                            {
                                MessageBox.ShowBox("MessageID207");
                                Audit_Error("Enterprise WebService Communication Failure", _slotMachine.AssetNumber);
                                break;
                            }
                        case EnrollmentErrorCodes.RemoveFromPollingListFailure:
                            {
                                MessageBox.ShowBox("MessageID208");
                                Audit_Error("Unable to remove from Polling list", _slotMachine.AssetNumber);
                                break;
                            }
                        case EnrollmentErrorCodes.ExchangeHostServiceNotRunning:
                            {
                                Audit_Error("Unable to remove from Polling list: Timeout occured", _slotMachine.AssetNumber);
                                if (MessageBox.ShowBox("MessageID359", BMC_Icon.Question, BMC_Button.YesNo)
                                    == System.Windows.Forms.DialogResult.No)
                                    return;
                                //Calling Remove Machine with Disable Machine command as false, Since first attempt to Disable Failed.
                                ErrorCode = EnrollmentBusinessObject.CreateInstance().RemoveMachine(
                                    _slotMachine.InstallationNo, machineStatusFlag, siteCode, 0);

                                switch (ErrorCode)
                                {
                                    case EnrollmentErrorCodes.DatabaseError:
                                        {
                                            MessageBox.ShowBox("MessageID206");
                                            Audit_Error("Database Error", _slotMachine.AssetNumber);
                                            break;
                                        }
                                    case EnrollmentErrorCodes.EnterpriseWebServiceCommunicationFailure:
                                        {
                                            MessageBox.ShowBox("MessageID207");
                                            Audit_Error("Enterprise WebService Communication Failure", _slotMachine.AssetNumber);
                                            break;
                                        }
                                    case EnrollmentErrorCodes.RemoveFromPollingListFailure:
                                        {
                                            MessageBox.ShowBox("MessageID208");
                                            Audit_Error("Unable to remove from Polling list", _slotMachine.AssetNumber);
                                            break;
                                        }
                                    case EnrollmentErrorCodes.ExchangeHostServiceNotRunning:
                                        {
                                            Audit_Error("Unable to remove from Polling list: Timeout occured", _slotMachine.AssetNumber);
                                            MessageBox.ShowBox("MessageID360", BMC_Icon.Error);
                                            break;
                                        }
                                    case EnrollmentErrorCodes.Success:
                                        MessageBox.ShowBox("MessageID209");

                                        AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
                                        {

                                            AuditModuleName = ModuleName.RemoveMachine,
                                            Audit_Screen_Name = "Position Details|Remove Machine",
                                            Audit_Desc = "Machine Removed from Position: "
                                                         + Convert.ToInt32(_slotMachine.SlotNumber).ToString(),
                                            AuditOperationType = OperationType.MODIFY,
                                            Audit_Slot = ((SlotMachine)sender).AssetNumber,
                                            Audit_Field = "Position",
                                            Audit_Old_Vl = Convert.ToInt32(_slotMachine.SlotNumber).ToString()
                                        });
                                        break;


                                }
                                return;

                                break;
                            }
                        case EnrollmentErrorCodes.Success:
                            MessageBox.ShowBox("MessageID209");

                            AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
                            {

                                AuditModuleName = ModuleName.RemoveMachine,
                                Audit_Screen_Name = "Position Details|Remove Machine",
                                Audit_Desc = "Machine Removed from Position: "
                                             + Convert.ToInt32(_slotMachine.SlotNumber).ToString(),
                                AuditOperationType = OperationType.MODIFY,
                                Audit_Slot = ((SlotMachine)sender).AssetNumber,
                                Audit_Field = "Position",
                                Audit_Old_Vl = Convert.ToInt32(_slotMachine.SlotNumber).ToString()
                            });
                            break;


                    }
                    return;
                }
                if (_slotMachine.InstallationNo > 0)
                {
                    this.CloseLegendPanel();
                    var pos = new PosDetails(_slotMachine);
                    pos.Exit += ObjPosDetailsExitClicked;
                    this.ChildWindow = pos;
                    pos.Margin = new Thickness(0);
                    LayoutRoot.Children.Add(pos);
                }
                else if (string.IsNullOrWhiteSpace(_slotMachine.AssetNumber) &&
                         SecurityHelper.HasAccess("CashdeskOperator.FloorView.cs.InstallMachine"))
                {
                    this.CloseLegendPanel();
                    var enrollment = new EnrollmentWnd(_slotMachine.SlotNumberString);
                    enrollment.Exit += ObjPosDetailsExitClicked;
                    this.ChildWindow = enrollment;
                    enrollment.Margin = new Thickness(0);
                    LayoutRoot.Children.Add(enrollment);
                }
                else
                {
                    MessageBox.ShowBox("MessageID437");
                }
                return;

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            finally
            {
                if (needRefresh)
                {
                    SlotMachineCollectionFactory.ClearMachines(this);
                    clientObj_FloorClientReceived(null);
                }
            }
        }

        private void chkSortPos_Checked(object sender, RoutedEventArgs e)
        {
            _SortChecked = false;
            if (chkSortAsset.IsChecked.Value)
            {
                chkSortAsset.IsChecked = false;
            }
            FilterAndSortSlotMachines();

            oCommonUtilities.CreateInstance().UpdateAppSettingSortOrder("FlrView_SortBy_Position", chkSortPos.IsChecked.Value.ToString());
            oCommonUtilities.CreateInstance().UpdateAppSettingSortOrder("FlrView_SortBy_Asset", chkSortAsset.IsChecked.Value.ToString());
        }

        private void chkSortAsset_Checked(object sender, RoutedEventArgs e)
        {
            _SortChecked = true;
            if (chkSortPos.IsChecked.Value)
            {
                chkSortPos.IsChecked = false;
            }
            FilterAndSortSlotMachines();

            oCommonUtilities.CreateInstance().UpdateAppSettingSortOrder("FlrView_SortBy_Asset", chkSortAsset.IsChecked.Value.ToString());
            oCommonUtilities.CreateInstance().UpdateAppSettingSortOrder("FlrView_SortBy_Position", chkSortPos.IsChecked.Value.ToString());
        }

        private void chkSortPos_Unchecked(object sender, RoutedEventArgs e)
        {
            FilterAndSortSlotMachines();

            oCommonUtilities.CreateInstance().UpdateAppSettingSortOrder("FlrView_SortBy_Asset", chkSortAsset.IsChecked.Value.ToString());
            oCommonUtilities.CreateInstance().UpdateAppSettingSortOrder("FlrView_SortBy_Position", chkSortPos.IsChecked.Value.ToString());
        }

        private void chkRePosition_Checked(object sender, RoutedEventArgs e)
        {
            pnlFloor.AllowDragging = true;
        }

        private void chkRePosition_Unchecked(object sender, RoutedEventArgs e)
        {
            pnlFloor.AllowDragging = false;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            _teFloorActive.Set();
            this.InitFloorPosition();
            clientObj_FloorClientReceived(null);
            if (!isPostBack)
            {

                foreach (var item in spPanel.Children)
                    if (((UIElement)item).Visibility == Visibility.Visible)
                        panelHeight += ((UIElement)item).RenderSize.Height;

                panelHeight += 10;

                txtLegend.Triggers.Add(OpenAnimation(MouseDownEvent));
                txtLegend_.Triggers.Add(CloseAnimation(MouseDownEvent));
                isPostBack = true;
            }
#if !FLOORVIEW_REFRESH_NEW
            if (_thCheckRemoteObj.ThreadState == System.Threading.ThreadState.Unstarted)
            {
                _thCheckRemoteObj.Start();
            }
#endif
        }

        private void txtLegend_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtLegend.Visibility = Visibility.Hidden;
            txtLegend_.Visibility = Visibility.Visible;
        }

        private void txtLegend__MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtLegend_.Visibility = Visibility.Hidden;
            txtLegend.Visibility = Visibility.Visible;

        }

        #endregion

        #region Methods

#if! FLOORVIEW_REFRESH_NEW
        private void CheckRemoteObjectAlive()
        {
            try
            {
                int iWaitForInMilliSec = App.iInteraval * 1000;

                while (!_eventShutdown.WaitOne(iWaitForInMilliSec))
                {
                    if (DateTime.Now.Subtract(_slotRefreshTime).Seconds > App.iInteraval - 3)
                    {
                        App.siteLicensingClientObj.ClientReceived -= clientObj_FloorClientReceived;
                        App.siteLicensingClient.SendToServer(App.siteLicensingClientObj, null);
                        App.siteLicensingClientObj.ClientReceived += new ClientReceivedEvent(clientObj_FloorClientReceived);
                    }
                }
            }
            catch
            {
            }
        }
#endif

        private void Audit_Error(string sDesc, string sAsset)
        {
            AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
            {

                AuditModuleName = ModuleName.RemoveMachine,
                Audit_Screen_Name = "Position Details|Remove Machine",
                Audit_Desc = sDesc,
                AuditOperationType = OperationType.MODIFY,
                Audit_Slot = sAsset
            });
        }

        public void ClearChildren()
        {
            try
            {
                foreach (var child in pnlFloor.Children)
                {
                    ClearChild((SlotMachine)child);
                }
                pnlFloor.Children.Clear();
            }
            catch (Exception)
            {
                LogManager.WriteLog("ClearChildren() => Unable to clear the children.", LogManager.enumLogLevel.Error);
            }


        }

        public void ClearChild(SlotMachine slotMachine)
        {
            slotMachine.MouseLeftButtonUp -= SlotMouseUpEvent;
            BindingOperations.ClearAllBindings(slotMachine);
            slotMachine.Dispose();
            slotMachine = null;
        }

        #region To solve the performance related problems. (A.Vinod Kumar on 12/12/2011)
        /*  Author  :   A.Vinod Kumar
         *  Date    :   12/12/2011
         *  Purpose :   To solve the performance related problems.
         */

        void clientObj_FloorClientReceived(object oLst)
        {
            const string PROC = "[clientObj_FloorClientReceived]";

            try
            {
                _slotRefreshTime = DateTime.Now;
                if (!_teFloorActive.IsSignalled) return;

                if (oLst == null)
                {
                    using (InstallationDataContext installationDataContext2 =
                        new InstallationDataContext(oCommonUtilities.CreateInstance().GetConnectionString()))
                    {
                        oLst = installationDataContext2.GetSlotStatus("", -1);
                    }
                }

                this.OnRefreshFloorView(oLst as List<FloorStatusData>);
            }
            catch (ThreadAbortException)
            {
                LogManager.WriteLog(PROC + " => Thread was instructed to close.", LogManager.enumLogLevel.Error);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog(PROC + " => Exception occured in function.", LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
            }
        }

        private object _refreshLock = new object();

        public void SetSlotStatus(FloorStatusDataResponse response)
        {
            try
            {
                if (!_teFloorActive.IsCompleted) return;

                LogManager.WriteLog(":=> Received data from server to refresh floorview at : " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff"), LogManager.enumLogLevel.Info);
                if (response == null ||
                    response.Response == null) return;

                this.OnRefreshFloorView(response.Response, false);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        void OnRefreshFloorView(List<FloorStatusData> actualResults)
        {
            this.OnRefreshFloorView(actualResults, true);
        }

        /// <summary>
        /// Gets the floor status information and redraw the controls on the scrren.
        /// </summary>        
        void OnRefreshFloorView(List<FloorStatusData> actualResults, bool logData)
        {
            const string PROC = "[OnRefreshFloorView]";

            //lock (_refreshLock)
            {
                try
                {
                    if (actualResults == null) return;

                    if (_SortChecked)
                    {
                        if (Settings.DisplayGameNameInFloorView)
                        {
                            //List<FloorStatusData> temp = new List<FloorStatusData>();
                            //temp.AddRange((actualResults.Where(m => !string.IsNullOrWhiteSpace(m.Game_Name))
                            //.OrderBy(m => m.Game_Name).ToList()));

                            //temp.AddRange(actualResults.Where(m => string.IsNullOrWhiteSpace(m.Game_Name)).OrderBy(m => m.Game_Name).ToList());
                            //actualResults = temp;


                            actualResults = ((actualResults.Where(m => !string.IsNullOrWhiteSpace(m.Game_Name))
                             .OrderBy(m => m.Game_Name)).Union((actualResults.Where(m => string.IsNullOrWhiteSpace(m.Game_Name))
                             .OrderBy(m => m.Game_Name)))).ToList();
                        }
                        else
                        {
                            actualResults = actualResults.OrderBy(m => m.Asset_No).ToList();
                        }
                    }
                    else
                        actualResults = actualResults.OrderBy(m => m.Bar_Pos_Name).ToList();

                    barPositions = actualResults;
                    List<FloorStatusData> newbarPositions = null;

                    try
                    {
                        {
                            if (actualResults != null)
                            {
                                newbarPositions = (from a in actualResults
                                                   where
                                                   (_Empty && (SlotMachineStatus)Enum.Parse(typeof(SlotMachineStatus), a.Slot_Status) == SlotMachineStatus.EmptyPosition) ||
                                                   (((_GMUConnectivity && (SlotMachineStatus)Enum.Parse(typeof(SlotMachineStatus), a.Slot_Status) == SlotMachineStatus.GMUConnectivity) ||
                                                   (_ForceFinalCollection && (SlotMachineStatus)Enum.Parse(typeof(SlotMachineStatus), a.Slot_Status) == SlotMachineStatus.ForceFinalCollection) ||
                                                    (_StackerRemoved && a.StackerEventReceived.HasValue && a.StackerEventReceived.Value) ||
                                                   (_Carded && (SlotMachineStatus)Enum.Parse(typeof(SlotMachineStatus), a.Slot_Status) == SlotMachineStatus.CardedPlay ||
                                                            (SlotMachineStatus)Enum.Parse(typeof(SlotMachineStatus), a.Slot_Status) == SlotMachineStatus.CardedHandPay) ||
                                                   (_EmpCarded && Settings.IsEmployeeCardTrackingEnabled && (SlotMachineStatus)Enum.Parse(typeof(SlotMachineStatus), a.Slot_Status) == SlotMachineStatus.EmpCardedPlay) ||
                                                   (_GameInstallationAAMSPending && (SlotMachineStatus)Enum.Parse(typeof(SlotMachineStatus), a.Slot_Status) == SlotMachineStatus.GameInstallationAAMSPending) ||
                                                   (_GamePlay && (SlotMachineStatus)Enum.Parse(typeof(SlotMachineStatus), a.Slot_Status) == SlotMachineStatus.MachineInPlay ||
                                                            (SlotMachineStatus)Enum.Parse(typeof(SlotMachineStatus), a.Slot_Status) == SlotMachineStatus.NonCardedHandPay) ||
                                                   (_NotinPlay && (SlotMachineStatus)Enum.Parse(typeof(SlotMachineStatus), a.Slot_Status) == SlotMachineStatus.NotInPlay) ||
                                                   (_SlotInstallationAAMSPending && (SlotMachineStatus)Enum.Parse(typeof(SlotMachineStatus), a.Slot_Status) == SlotMachineStatus.VLTInstallationAAMSPending) ||
                                                   (_VLNoMeters && (SlotMachineStatus)Enum.Parse(typeof(SlotMachineStatus), a.Slot_Status) == SlotMachineStatus.InstallationCompletedNonMetered) ||
                                                   (_VLTRemoved && (SlotMachineStatus)Enum.Parse(typeof(SlotMachineStatus), a.Slot_Status) == SlotMachineStatus.FloatCollection) ||
                                                   (_VLTunderMaintenance && ((SlotMachineStatus)Enum.Parse(typeof(SlotMachineStatus), a.Slot_Status) == SlotMachineStatus.VLTunderMaintenance ||
                                                            (SlotMachineStatus)Enum.Parse(typeof(SlotMachineStatus), a.Slot_Status) == SlotMachineStatus.VLTunderUnauthorizedMaintenance))  ||
                                                   (_CommsDown && (SlotMachineStatus)Enum.Parse(typeof(SlotMachineStatus), a.Slot_Status) == SlotMachineStatus.CommsDown) ||
                                                   (_GameDown && (SlotMachineStatus)Enum.Parse(typeof(SlotMachineStatus), a.Slot_Status) == SlotMachineStatus.GameDown)) 
                                                    &&
                                                   ((_ClearedEvent && (SlotMachineStatus)Enum.Parse(typeof(SlotMachineStatus), a.Slot_Status) != SlotMachineStatus.EmptyPosition &&
                                                            a.UnClearedEvent == false) ||
                                                   (_UnclearedEvent && (SlotMachineStatus)Enum.Parse(typeof(SlotMachineStatus), a.Slot_Status) != SlotMachineStatus.EmptyPosition &&
                                                            a.UnClearedEvent == true))) && (a.IsCollectable >= 1 || (_FloatUnDeclared && a.IsCollectable < 1))
                                                   select a).ToList();
                                actualResults = null;
                            }
                        }
                    }
                    catch (ThreadAbortException)
                    {
                        LogManager.WriteLog(PROC + " => Thread was instructed to close.", LogManager.enumLogLevel.Error);
                    }
                    catch
                    {
                        LogManager.WriteLog(PROC + " => Unable to get the floor view data from database.", LogManager.enumLogLevel.Error);
                    }
                    if (newbarPositions != null)
                    {
                        SlotMachineCollectionFactory.RefreshData(this, newbarPositions);
                        if(logData) LogManager.WriteLog(PROC + " => Floor view refreshed.", LogManager.enumLogLevel.Info);
                    }
                }
                catch (ThreadAbortException)
                {
                    LogManager.WriteLog(PROC + " => Thread was instructed to close.", LogManager.enumLogLevel.Error);
                }
                catch (Exception ex)
                {
                    LogManager.WriteLog(PROC + " => Exception occured in function.", LogManager.enumLogLevel.Error);
                    ExceptionManager.Publish(ex);
                }
            }
        }

        private void FilterAndSortSlotMachines()
        {
            if (barPositions != null)
            {
                clientObj_FloorClientReceived(barPositions);
            }
        }
        #endregion

        internal void CalculatePositionAndPaint(ref double widthmargin, ref double heightmargin, ref int curPos, SlotMachine newMachine)
        {
            var perRow = int.Parse(pnlFloor.Width.ToString()) /
                         (int.Parse(newMachine.Width.ToString()) + 10);

            if ((perRow / curPos == 1) && (perRow % curPos == 0))
            {
                Canvas.SetLeft(newMachine, widthmargin);
                Canvas.SetTop(newMachine, heightmargin);
                newMachine.Left = widthmargin;
                newMachine.Top = heightmargin;

                widthmargin = 10;
                heightmargin = heightmargin + newMachine.Height + 20;
                curPos = 0;
            }
            else
            {

                Canvas.SetLeft(newMachine, widthmargin);
                Canvas.SetTop(newMachine, heightmargin);
                newMachine.Left = widthmargin;
                newMachine.Top = heightmargin;

                widthmargin = widthmargin + newMachine.Width + 10;
            }

            curPos += 1;
        }

        internal void CalculateGroupedSlotMachines(SlotMachineGroupPair group, int slotPerWidth)
        {
            try
            {
                var slotLength = group.SlotMachineKeys.Count;
                int slotPerHeight = Convert.ToInt32(Math.Ceiling((double)(slotLength) / slotPerWidth));
                var ch = (slotPerHeight * SlotMachine.SlotMachineHeightGap) + SlotMachine.SlotMachineGap;
                group.SlotsHeight = ch;
            }
            catch { }
        }

        internal void SaveFloorPosition(SlotMachine slotMachinePosition, bool isDefault, ref bool isSaved)
        {
            if (!isDefault || chkRePosition.IsChecked.Value)
            {
                int userID = CDOSettings.Current.LoggedInUser.SecurityUserID;
                int left = TypeSystem.GetValueInt(Canvas.GetLeft(slotMachinePosition));
                int top = TypeSystem.GetValueInt(Canvas.GetTop(slotMachinePosition));

                //if (CFloorView._FirstDragPosition)
                //{
                //    _analysisBusinessObject.SaveUserBasedFloorPosition(SlotMachineCollectionFactory.ToXml(this), userID);
                //    CFloorView._FirstDragPosition = false;
                //}

                _analysisBusinessObject.SaveFloorPosition(slotMachinePosition.SlotID, userID,
                                                             top,
                                                             left);
                int currentZIndex = Canvas.GetZIndex(slotMachinePosition);

                if (_floorViewUserLocations)
                {
                    FloorPositionDataDto positionData = slotMachinePosition.PositionData;
                    FloorPositionDto position = new FloorPositionDto()
                    {
                       { 
                           new FloorPositionKeyDto(userID, slotMachinePosition.SlotID),
                           new FloorPositionDataDto(slotMachinePosition.SlotID, left, top, currentZIndex) 
                       },
                    };
                    _clientSubcriber.NotifyFloorPosition(position);
                }
            }
        }

        #endregion

        #region IDisposable Members

        private bool disposed;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
#if! FLOORVIEW_REFRESH_NEW
                    _eventShutdown.Set();
                    App.siteLicensingClientObj.ClientReceived -= clientObj_FloorClientReceived;
#else
                    this.DestroyClientSubscriber();
#endif

                    SlotMachineCollectionFactory.ClearMachines(this);
                    SlotMachineCollectionFactory.Release(this);
                    this.ReleaseControls();
                }
                disposed = true;
            }
        }

        private void ReleaseControls()
        {
            try
            {
                this.CleanupWPFObjectTopControls((i) =>
                {
                    if (barPositions != null)
                        barPositions.Clear();

                    // unsubscribe the events
                    chkRePosition.Checked -= chkRePosition_Checked;
                    chkRePosition.Unchecked -= chkRePosition_Unchecked;
                    chkSortPos.Checked -= chkSortPos_Checked;
                    txtLegend.MouseDown -= txtLegend_MouseDown;
                    txtLegend_.MouseDown -= txtLegend__MouseDown;
                    this.Loaded -= this.UserControl_Loaded;

                    this.ChildWindow = null;
                    this.pnlFloor.DisposeWPFObjectUIElement((c) =>
                    {
                        if (c is ScrollContentPresenter) ((ScrollContentPresenter)c).RemoveChildAdornerLayerLayoutUpdated();
                    });
                    this.StopAndRemoveStoryBoard("EmptySlotShow", "EmptySlotHide",
                        "NotInPlayShow", "NotInPlayHide",
                        "HandPayShow", "HandPayHide",
                        "InPlayShow", "InPlayHide",
                        "CardedShow", "CardedHide",
                        "GoldCardShow", "GoldCardHide");
                },
                (c) =>
                {
                });
                LogManager.WriteLog("|=> CFloorView objects are released successfully.", LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        ~CFloorView()
        {
            Dispose(false);
        }
        #endregion

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

        #region Child Window Members
        private IDisposable _childWindow = null;
        private IDisposable ChildWindow
        {
            get { return _childWindow; }
            set
            {
                if (_childWindow != null)
                {
                    HC.Common.DisposeObject(ref _childWindow, true);
                }
                _childWindow = value;
            }
        }
        #endregion

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            _teFloorActive.Reset();
        }
    }

    #region To solve the performance related problems. (A.Vinod Kumar on 12/12/2011)
    /*  Author  :   A.Vinod Kumar
     *  Date    :   12/12/2011
     *  Purpose :   To solve the performance related problems.
     */

    /// <summary>
    /// Slot machine pair
    /// </summary>
    public class SlotMachinePair
    {
        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        /// <value>The key.</value>
        public string Key { get; set; }

        /// <summary>
        /// Gets or sets the name of the position.
        /// </summary>
        /// <value>The name of the position.</value>
        public string PositionName { get; set; }

        /// <summary>
        /// Gets or sets the asset no.
        /// </summary>
        /// <value>The asset no.</value>
        public string AssetNo { get; set; }

        /// <summary>
        /// Gets or sets the machine.
        /// </summary>
        /// <value>The machine.</value>
        public SlotMachine Machine { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is added.
        /// </summary>
        /// <value><c>true</c> if this instance is added; otherwise, <c>false</c>.</value>
        public bool IsAdded { get; set; }

        /// <summary>
        /// Gets or sets the index of the row.
        /// </summary>
        /// <value>The index of the row.</value>
        public int RowIndex { get; set; }
    }

    public class SlotMachineGroupPair
    {
        public SlotMachineGroupPair()
        {
            this.SlotMachineKeys = new List<string>();
        }

        public IList<string> SlotMachineKeys { get; private set; }

        public SlotMachineGroup MachineGroup { get; set; }

        public SlotMachineStatus Status { get; set; }

        //public Brush OuterColor { get; set; }

        public bool IsAdded { get; set; }

        public int SlotsHeight { get; set; }

        public double Left { get; set; }

        public double Top { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public string DisplayText { get; set; }
    }

    /// <summary>
    /// Slot machine collection
    /// </summary>
    public class SlotMachineCollection : IDisposable
    {
        /// <summary>
        /// Collection of slot machines
        /// </summary>
        private IDictionary<string, SlotMachinePair> _slotMachines = null;

        private IDictionary<string, SlotMachineGroupPair> _slotMachineGroups = null;

        /// <summary>
        /// Floor view screen
        /// </summary>
        private CFloorView _floorView = null;

        /// <summary>
        /// Thread dispatcher
        /// </summary>
        protected Dispatcher _dispatcher = null;

        /// <summary>
        /// Parent control
        /// </summary>
        protected DragCanvas _canvas = null;

        /// <summary>
        /// Allow Dragging
        /// </summary>
        private bool _allowDragging = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="SlotMachineCollection"/> class.
        /// </summary>
        /// <param name="floorView">The floor view.</param>
        public SlotMachineCollection(CFloorView floorView)
        {
            _floorView = floorView;
            _dispatcher = floorView.Dispatcher;
            _canvas = floorView.pnlFloor;
            _slotMachines = new SortedDictionary<string, SlotMachinePair>(StringComparer.InvariantCultureIgnoreCase);
            _slotMachineGroups = new SortedDictionary<string, SlotMachineGroupPair>(StringComparer.InvariantCultureIgnoreCase);
            bool.TryParse(ConfigManager.Read("AllowDragging"), out _allowDragging);
        }

        /// <summary>
        /// Clears the machine.
        /// </summary>
        /// <param name="machine">The machine.</param>
        private void ClearMachine(SlotMachine machine)
        {
            try
            {
                _floorView.ClearSlotMouseUpEvent(machine);
                _canvas.Children.Remove(machine);
                machine.ContextMenu = null;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// Clears the machines.
        /// </summary>
        internal void ClearMachines()
        {
            const string PROC = "[ClearMachines]";

            try
            {
                _dispatcher.Invoke(new Action(
                   delegate()
                   {
                       foreach (KeyValuePair<string, SlotMachinePair> pair in _slotMachines)
                       {
                           SlotMachinePair slotPair = pair.Value;
                           slotPair.IsAdded = false;
                           this.ClearMachine(slotPair.Machine);
                       }
                       _canvas.Children.Clear();
                   }));
            }
            catch (ThreadAbortException)
            {
                LogManager.WriteLog(PROC + " => Thread was instructed to close.", LogManager.enumLogLevel.Error);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog(PROC + " => Exception occured in function.", LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
            }
        }


        public XDocument ToXML()
        {
            //const string PROC = "[ToXML]";
            
            try
            {
                List<SlotMachine> slots = _slotMachines.Values.Select(x => x.Machine).ToList();
                
                XElement element = new XElement("Positions");
                XDocument doc = new XDocument(new XDeclaration("1.0", "ISO-8859-1", null), element);
                foreach (SlotMachine slot in slots)
                {
                    element.Add(new XElement("Position", new XElement("Bar_Pos_No", slot.SlotNumber),
                        //new XElement("SecurityUserID", CDOSettings.Current.LoggedInUser.UserID),
                        new XElement("Top", slot.Top),
                        new XElement("Left", slot.Left)
                        ));
		 
                }
                return doc;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return new XDocument();
        }
        
        
        /// <summary>
        /// Refreshes the data.
        /// </summary>
        /// <param name="barPositions">The bar positions.</param>
        public void RefreshData(List<FloorStatusData> barPositions)
        {
            const string PROC = "[RefreshData]";
            double widthmargin = 10;
            double heightmargin = 20;
            var curPos = 1;
            bool clearMachines = false;
            bool groupby = false;
            _dispatcher.Invoke(new Action(
                   delegate()
                   {
                       groupby = _floorView.GroupByMachines;
                   }));
            bool hasPositionSaved = false;
            _floorView._teFloorPositions.Wait(TimeSpan.FromSeconds(30)); // wait for worst case seconds to retrive floor position from exchange server

            int userId = CDOSettings.Current.LoggedInUser.SecurityUserID;
            FloorPositionDto savedPositions = null;            

            try
            {
                // clear all the slot machine groupings
                if (_floorView.DisplayGroupMachines)
                {
                    foreach (var slotMachineGroupItem in _slotMachineGroups.Values)
                    {
                        slotMachineGroupItem.SlotMachineKeys.Clear();
                        if (!groupby)
                            _dispatcher.Invoke(new Action(
                                delegate()
                                {
                                    this.RemoveSlotMachineGroupFromUI(slotMachineGroupItem);
                                }));
                    }
                }

                // add or update the slot machines
                int rowIndex = 1;
                foreach (FloorStatusData position in barPositions)
                {
                    string key = position.Bar_Pos_Name;

                    _dispatcher.Invoke(new Action(
                        delegate()
                        {
                            SlotMachine newMachine = null;
                            SlotMachinePair pair = null;

                            if (_slotMachines.ContainsKey(key))
                            {
                                pair = _slotMachines[key];
                                newMachine = pair.Machine;
                            }
                            else
                            {
                                newMachine = new SlotMachine()
                                {
                                    SlotNumber = Convert.ToInt32(position.Bar_Pos_Name).ToString(),
                                    SlotNumberString = position.Bar_Pos_Name.ToString(),
                                    SlotID = position.Bar_Pos_No,
                                    AssetNumber = position.Asset_No
                                };
                                pair = new SlotMachinePair
                                {
                                    Key = key,
                                    PositionName = position.Bar_Pos_Name,
                                    AssetNo = position.Asset_No,
                                    Machine = newMachine
                                };
                                _slotMachines.Add(key, pair);
                            }

                            // slot status grouping
                            newMachine.SlotStatusString = position.Slot_Status;
                            SlotMachineGroupPair groupPair = null;
                            IList<string> groupItems = null;
                            if (_floorView.DisplayGroupMachines)
                            {
                                if (!_slotMachineGroups.ContainsKey(position.Slot_Status))
                                {
                                    groupPair = new SlotMachineGroupPair()
                                    {
                                        Status = _floorView.GetSlotStatus(position.Slot_Status),
                                        MachineGroup = new SlotMachineGroup()
                                        {
                                            Width = _canvas.Width - 10,
                                            Height = 30,
                                        },
                                        DisplayText = _floorView.GetSlotStatusDecription(position.Slot_Status),
                                    };
                                    _slotMachineGroups.Add(position.Slot_Status, groupPair);
                                }
                                else
                                {
                                    groupPair = _slotMachineGroups[position.Slot_Status];
                                }
                                groupItems = groupPair.SlotMachineKeys;
                                groupItems.Add(key);
                            }

                            // display game name instead of asset no
                            string assetNo = position.Asset_No;
                            if (Settings.DisplayGameNameInFloorView)
                            {
                                assetNo = position.Game_Name;
                            }

                            // Fix for Asset Number not displayed after finishing the Enrollment 
                            pair.AssetNo = assetNo;
                            newMachine.AssetNumber = assetNo;

                            pair.RowIndex = rowIndex++;
                            newMachine.Top = Convert.ToDouble(position.FLOORTOP);
                            newMachine.Left = Convert.ToDouble(position.FLOORLEFT);
                            _floorView.SetSlotMouseUpEvent(newMachine);

                            if (position.Install_No != 0)
                            {
                                newMachine.InstallationNo = position.Install_No;
                                newMachine.IsEventUnCleared = !(newMachine.IsEventUnCleared);
                                newMachine.IsEventUnCleared = (bool)position.UnClearedEvent;
                                newMachine.Status = (SlotMachineStatus)Enum.Parse(typeof(SlotMachineStatus), position.Slot_Status);
                                newMachine.StackerEventReceived = position.StackerEventReceived.HasValue ? position.StackerEventReceived.Value : false;
                                newMachine.IsCollectable = position.IsCollectable < 1;
                                if (newMachine.StackerEventReceived)
                                {
                                    newMachine.OuterRoundColor = Brushes.Goldenrod;
                                    newMachine.InnerRoundColor = Brushes.IndianRed;
                                }
                            }
                            else
                            {
                                newMachine.Status = SlotMachineStatus.EmptyPosition;
                                newMachine.InstallationNo = 0;
                            }
                            if (_floorView.DisplayGroupMachines &&
                                groupPair.MachineGroup.OuterColor == null)
                            {
                                groupPair.MachineGroup.OuterColor = _floorView._slotMachineGroupBgBrush;// newMachine.OuterRoundColor;
                                groupPair.MachineGroup.OuterForeColor = _floorView._slotMachineGroupFgBrush;// newMachine.ForeColorBrush;
                            }
                        }));
                }

                // remove the slot machine
                List<SlotMachinePair> removedMachines = (from a in _slotMachines
                                                         join b in barPositions
                                                         on a.Key equals b.Bar_Pos_Name
                                                         into matched
                                                         from c in matched.DefaultIfEmpty()
                                                         where c == null
                                                         select new SlotMachinePair
                                                         {
                                                             Key = a.Key,
                                                             Machine = a.Value.Machine
                                                         }).ToList();
                if (removedMachines != null)
                {
                    foreach (SlotMachinePair removedMachine in removedMachines)
                    {
                        _dispatcher.Invoke(new Action(
                        delegate()
                        {
                            this.ClearMachine(removedMachine.Machine);
                        }));
                        _slotMachines.Remove(removedMachine.Key);
                    }
                }

                // finally add the slot machines to the UI
                _dispatcher.Invoke(new Action(
                    delegate()
                    {
#if DEBUG
                        Stopwatch watch = new Stopwatch();
                        watch.Start();
#endif

                        try
                        {
                            if (groupby)
                            {
                                // draw the headers
                                int slotPerWidth = Convert.ToInt32((_floorView.Width - SlotMachine.SlotMachineGap) / SlotMachine.SlotMachineWidthGap);
                                int gap = SlotMachine.SlotMachineGap;
                                double y = gap;
                                foreach (var group in _slotMachineGroups)
                                {
                                    string title = group.Key;
                                    SlotMachineGroupPair groupPair = group.Value;
                                    SlotMachineGroup machineGroup = groupPair.MachineGroup;
                                    int slots = groupPair.SlotMachineKeys.Count;

                                    if (slots <= 0)
                                    {
                                        this.RemoveSlotMachineGroupFromUI(groupPair);
                                        continue;
                                    }
                                    this.AddSlotMachineGroupToUI(groupPair);

                                    groupPair.Left = 0;
                                    groupPair.Top = y;
                                    machineGroup.DisplayText = groupPair.DisplayText + " (" + slots.ToString() + ")";
                                    Canvas.SetLeft(machineGroup, groupPair.Left);
                                    Canvas.SetTop(machineGroup, groupPair.Top);
                                    _floorView.CalculateGroupedSlotMachines(groupPair, slotPerWidth);

                                    int row = 0;
                                    int column = 0;
                                    double sx = groupPair.Left + gap;
                                    double sy = groupPair.Top + machineGroup.Height + gap;
                                    foreach (var slotKey in groupPair.SlotMachineKeys)
                                    {
                                        SlotMachinePair slotPair = _slotMachines[slotKey];
                                        SlotMachine newMachine = slotPair.Machine;

                                        if (column >= slotPerWidth)
                                        {
                                            column = 0;
                                            row++;
                                            sx = groupPair.Left + gap;
                                            sy += SlotMachine.SlotMachineHeightGap;
                                        }

                                        Canvas.SetLeft(newMachine, sx);
                                        Canvas.SetTop(newMachine, sy);
                                        this.AddSlotMachineToUI(slotPair, newMachine);

                                        sx += SlotMachine.SlotMachineWidthGap;
                                        column++;
                                    }
                                    y += machineGroup.Height + groupPair.SlotsHeight + 5;
                                }

                                _canvas.Height = y + SlotMachine.SlotMachineGap;
                                _floorView.Border_Bottom.Margin = new Thickness(-50, 0, 0, 0);
                            }
                            else
                            {
                                List<string> orderedData = (from o in _slotMachines
                                                            orderby o.Value.RowIndex
                                                            select o.Key).ToList();
                                if (orderedData.Count > 72)
                                {
                                    _canvas.Height = (barPositions.Count / 12) * 110;
                                    _floorView.Border_Bottom.Width = 780;
                                    _floorView.Border_Bottom.Margin = new Thickness(-50, 0, 0, 0);
                                }

                                foreach (string slotKey in orderedData)
                                {
                                    SlotMachinePair slotPair = _slotMachines[slotKey];
                                    SlotMachine newMachine = slotPair.Machine;
                                    FloorPositionDataDto floorPosition = null;

                                    if (_floorView._floorViewUserLocations)
                                    {
                                        floorPosition = _floorView.GetFloorPosition(newMachine.SlotID);
                                        if (floorPosition != null)
                                        {
                                            newMachine.PositionData = floorPosition;
                                        }
                                    }

                                    UIElement dragging = _canvas.ElementBeingDragged;
                                    bool isDragging = false;
                                    if (dragging != null && dragging == newMachine)
                                    {
                                        isDragging = true;
                                    }

                                    if (!isDragging && _floorView._floorViewUserLocations)
                                    {
                                        if (floorPosition != null)
                                        {
                                            newMachine.Left = floorPosition.Left;
                                            newMachine.Top = floorPosition.Top;
                                            DragCanvas.SetZIndex(newMachine, floorPosition.RowNo);
                                        }
                                    }

                                    if (newMachine != null && !isDragging)
                                    {
                                        if (newMachine.Left > 0 || newMachine.Top > 0)
                                        {
                                            if ((_floorView.chkSortPos.IsChecked.Value) ||
                                                (_floorView.chkSortAsset.IsChecked.Value))
                                            {
                                                newMachine.Top = 0;
                                                newMachine.Left = 0;

                                                // Don't uncomment these lines, we should not reposition the slot machines during loading.
                                                // Slot machines position should be saved only during dragging.
                                                //if (newMachine.Status != SlotMachineStatus.EmptyPosition)
                                                //{
                                                //    _floorView.SaveFloorPosition(newMachine, true, ref hasPositionSaved);
                                                //}
                                                _floorView.CalculatePositionAndPaint(ref widthmargin, ref heightmargin, ref curPos, newMachine);
                                            }
                                            else
                                            {
                                                Canvas.SetLeft(newMachine, Convert.ToDouble(newMachine.Left));
                                                Canvas.SetTop(newMachine, Convert.ToDouble(newMachine.Top));
                                            }
                                        }
                                        else
                                        {
                                              _floorView.CalculatePositionAndPaint(ref widthmargin, ref heightmargin, ref curPos, newMachine);
                                              if (_floorView._floorViewUserLocations)
                                              {
                                                  if (!(_floorView.chkSortPos.IsChecked.Value &&
                                                        _floorView.chkSortAsset.IsChecked.Value))
                                                  {
                                                      if (savedPositions == null) savedPositions = new FloorPositionDto();
                                                      savedPositions.Modify(userId, newMachine.SlotID, TypeSystem.GetValueInt(newMachine.Left), TypeSystem.GetValueInt(newMachine.Top), 0);
                                                  }
                                              }
                                        }

                                        //// save the initial positions
                                        //if (CFloorView._FirstDragPosition)
                                        //{
                                        //    if (savedPositions == null) savedPositions = new FloorPositionDto();
                                        //    savedPositions.Modify(userId, newMachine.SlotID, TypeSystem.GetValueInt(newMachine.Left), TypeSystem.GetValueInt(newMachine.Top), 0);
                                        //}

                                        this.AddSlotMachineToUI(slotPair, newMachine);
                                    }
                                }
                            }
                        }
                        catch (ThreadAbortException)
                        {
                            LogManager.WriteLog(PROC + " => Thread was instructed to close.", LogManager.enumLogLevel.Error);
                        }
                        catch (Exception ex)
                        {
                            ExceptionManager.Publish(ex);
                        }
                        finally
                        {
#if DEBUG
                            watch.Stop();
                            LogManager.WriteLog(PROC + string.Format(" => Total time taken [{0}] to load the slot machines into UI.", watch.Elapsed.ToString()), LogManager.enumLogLevel.Info);
#endif
                            if (hasPositionSaved)
                            {
                                CDOSettings.Current.Save();
                            }

                            // notify the initial positions to all the clients logged in with the current user id
                            if (savedPositions != null && savedPositions.Count > 0)
                            {
                                _floorView.NotifyFloorPosition(savedPositions);                                
                            }
                            //if (CFloorView._FirstDragPosition)
                            //{
                            //    CFloorView._FirstDragPosition = false;
                            //}
                        }
                    }));

                if (clearMachines)
                {
                    LogManager.WriteLog(PROC + " => Unable to refresh. Clear machines called instead.", LogManager.enumLogLevel.Error);
                }
            }
            catch (ThreadAbortException)
            {
                LogManager.WriteLog(PROC + " => Thread was instructed to close.", LogManager.enumLogLevel.Error);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog(PROC + " => Exception occured in function.", LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
            }
            finally
            {
#if DEBUG
                LogManager.WriteLog(PROC + " => Finished.", LogManager.enumLogLevel.Error);
#endif
            }
        }

        private void AddSlotMachineToUI(SlotMachinePair slotPair, SlotMachine newMachine)
        {
            DragCanvas.SetCanBeDragged(newMachine, _allowDragging);
            DispatcherObject parent = LogicalTreeHelper.GetParent(newMachine);

            if (!slotPair.IsAdded ||
                parent == null)
            {
                //if (!Monitor.TryEnter(_floorView._clearLock, 5))
                //{
                //    clearMachines = true;
                //    break;
                //}
                _canvas.Children.Add(newMachine);
                slotPair.IsAdded = true;
            }
        }

        private void AddSlotMachineGroupToUI(SlotMachineGroupPair slotGroupPair)
        {
            SlotMachineGroup newMachineGroup = slotGroupPair.MachineGroup;
            DispatcherObject parent = LogicalTreeHelper.GetParent(newMachineGroup);

            if (!slotGroupPair.IsAdded ||
                parent == null)
            {
                _canvas.Children.Add(newMachineGroup);
                slotGroupPair.IsAdded = true;
            }
        }

        private void RemoveSlotMachineGroupFromUI(SlotMachineGroupPair slotGroupPair)
        {
            SlotMachineGroup newMachineGroup = slotGroupPair.MachineGroup;
            DispatcherObject parent = LogicalTreeHelper.GetParent(newMachineGroup);

            if (slotGroupPair.IsAdded &&
                parent != null)
            {
                //if (!Monitor.TryEnter(_floorView._clearLock, 5))
                //{
                //    clearMachines = true;
                //    break;
                //}
                _canvas.Children.Remove(newMachineGroup);
                slotGroupPair.IsAdded = false;
            }
        }

        #region IDisposable Members

        private bool disposed;

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
                    this.ClearMachines();
                }
                disposed = true;
            }
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="SlotMachineCollection"/> is reclaimed by garbage collection.
        /// </summary>
        ~SlotMachineCollection()
        {
            Dispose(false);
        }
        #endregion
    }

    /// <summary>
    /// Slot machine collection factory
    /// </summary>
    internal static class SlotMachineCollectionFactory
    {
        /// <summary>
        /// Initializes the <see cref="SlotMachineCollectionFactory"/> class.
        /// </summary>
        static SlotMachineCollectionFactory() { }

        /// <summary>
        /// Gets the collection.
        /// </summary>
        /// <param name="sortBy">The sort by.</param>
        /// <param name="floorView">The floor view.</param>
        /// <param name="barPositions">The bar positions.</param>
        private static SlotMachineCollection GetCollection(CFloorView floorView)
        {
            const string PROC = "[GetCollection]";
            SlotMachineCollection collection = null;

            try
            {
                collection = floorView._collection;
            }
            catch (ThreadAbortException)
            {
                LogManager.WriteLog(PROC + " => Thread was instructed to close.", LogManager.enumLogLevel.Error);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog(PROC + " => Exception occured in function.", LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
            }
            finally
            {
#if DEBUG
                LogManager.WriteLog(PROC + " => Finished.", LogManager.enumLogLevel.Error);
#endif
            }

            return collection;
        }

        /// <summary>
        /// Gets the collection.
        /// </summary>
        /// <param name="sortBy">The sort by.</param>
        /// <param name="floorView">The floor view.</param>
        /// <param name="barPositions">The bar positions.</param>
        internal static void RefreshData(CFloorView floorView, List<FloorStatusData> barPositions)
        {
            const string PROC = "[RefreshData]";

            try
            {
                SlotMachineCollection collection = GetCollection(floorView);
                if (collection != null)
                {
                    collection.RefreshData(barPositions);
                }
            }
            catch (ThreadAbortException)
            {
                LogManager.WriteLog(PROC + " => Thread was instructed to close.", LogManager.enumLogLevel.Error);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog(PROC + " => Exception occured in function.", LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
            }
            finally
            {
#if DEBUG
                LogManager.WriteLog(PROC + " => Finished.", LogManager.enumLogLevel.Error);
#endif
            }
        }

        /// <summary>
        /// Clears the machines.
        /// </summary>
        /// <param name="floorView">The floor view.</param>
        internal static void ClearMachines(CFloorView floorView)
        {
            const string PROC = "[ClearMachines]";

            try
            {
                SlotMachineCollection collection = GetCollection(floorView);
                if (collection != null)
                {
                    collection.ClearMachines();
                }
            }
            catch (ThreadAbortException)
            {
                LogManager.WriteLog(PROC + " => Thread was instructed to close.", LogManager.enumLogLevel.Error);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog(PROC + " => Exception occured in function.", LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
            }
            finally
            {
#if DEBUG
                LogManager.WriteLog(PROC + " => Finished.", LogManager.enumLogLevel.Error);
#endif
            }
        }

        /// <summary>
        /// Releases this instance.
        /// </summary>
        internal static void Release(CFloorView floorView)
        {
            const string PROC = "[Release]";

            try
            {
                SlotMachineCollection collection = GetCollection(floorView);
                if (collection != null)
                {
                    collection.Dispose();
                }
            }
            catch (ThreadAbortException)
            {
                LogManager.WriteLog(PROC + " => Thread was instructed to close.", LogManager.enumLogLevel.Error);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog(PROC + " => Exception occured in function.", LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
            }
            finally
            {
#if DEBUG
                LogManager.WriteLog(PROC + " => Finished.", LogManager.enumLogLevel.Error);
#endif
            }
        }

        internal static XDocument ToXml(CFloorView floorView)
        {
            const string PROC = "[ToXml]";

            try
            {
                SlotMachineCollection collection = GetCollection(floorView);
                if (collection == null)
                {
                    return null;
                }

                return collection.ToXML();
            }
            catch (ThreadAbortException)
            {
                LogManager.WriteLog(PROC + " => Thread was instructed to close.", LogManager.enumLogLevel.Error);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog(PROC + " => Exception occured in function.", LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
            }
            finally
            {
#if DEBUG
                LogManager.WriteLog(PROC + " => Finished.", LogManager.enumLogLevel.Error);
#endif
            }
            return null;
        }
    }
    #endregion
}