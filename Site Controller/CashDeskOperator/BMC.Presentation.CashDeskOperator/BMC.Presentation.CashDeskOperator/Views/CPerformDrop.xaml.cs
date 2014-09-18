using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Data.Linq;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Threading;
using BMC.CashDeskOperator;
using BMC.Security;
using BMC.Transport;
using CheckBox = System.Windows.Controls.CheckBox;
using Cursors = System.Windows.Input.Cursors;

using Audit.Transport;
using Audit.BusinessClasses;
using BMC.CashDeskOperator;
using BMC.CashDeskOperator.BusinessObjects;
using BMC.Business.CashDeskOperator;
using BMC.Common.LogManagement;
using BMC.Common.ExceptionManagement;
using BMC.Presentation.POS.Helper_classes;
using System.Xml.Linq;
using System.Text;
using BMC.Common.ConfigurationManagement;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using BMC.Common.ConfigurationManagement;
using System.Runtime.Remoting.Channels.Tcp;
using BMCIPC;
using System.Configuration;
using System.Data;
using BMC.Presentation.POS.Views;
using System.Windows.Interop;

namespace BMC.Presentation
{
    #region Perform Drop Screen
    //
    public partial class CPerformDrop : IDisposable, INotifyPropertyChanged
    {
        //
        #region Variable Declaration
        //

        public static event PropertyChangedEventHandler BusyPropertyChanged;
        public event PropertyChangedEventHandler PropertyChanged;
        private const int NOTSET = -1;
        private int _macCnt = 0;
        private int _currentThreadProcessed = 0;
        private int _NoOfMachinesPerThread = 0;
        private int _WaitTime = 1;
        private readonly object _lockProgess = new object();
        private readonly object _lockMachine = new object();
        private readonly object _lockInstallations = new object();
        private readonly object _lockBarPositions = new object();
        private readonly object _lockDrop = new object();
        private readonly LockHandler _lockHandler = new LockHandler();
        private readonly CollectionHelper _collectionHelper = new CollectionHelper();
        private readonly int? _userNo = SecurityHelper.CurrentUser.SecurityUserID;
        private bool _zoneSelected = false;
        private bool _finalDrop = false;
        private const string MACHINEADMIN = "MACHINEADMIN";
        private int[] _selectedInstallationNos = null;
        private string[] _selectedBarPositions = null;
        private bool disposed = false;
        private bool IsEventCleared = false;
        private bool isADropStarted = false;
        private int _InstallationnumberMR = 0;
        private int _batchnumberfordropPrint = 0;
        private ObservableCollection<ZoneCollection> _zoneData = new ObservableCollection<ZoneCollection>();
        private ObservableCollection<CollectionMachine> _machineData = new ObservableCollection<CollectionMachine>();
        BackgroundWorker _bgLoadScreen = new BackgroundWorker();
        BackgroundWorker[] _bgPerformDropArray = null;
        System.Threading.ManualResetEvent _mEvent = new System.Threading.ManualResetEvent(false);
        System.Threading.ManualResetEvent[] _mDropEvent = null;
        List<CollectionMachine> LstMachinesMasterList = null; //Filled during Initial Load of the screen
        int _SelectedRoute = 0;
        public bool IsPartCollectionDeclaration = false;
        //
        #endregion Variable Declaration
        //
        #region Enum Declaration
        private enum DropStatus
        {
            PART_DROP_SUCCESS = 2,
            NORMAL_DROP_SUCCESS = 4,
            DROP_FAILURE = 5,
            AUTO_DROP_SUCCESS = 6
        }
        #endregion Enum Declaration
        //
        #region Constructor
        //
        public CPerformDrop()
        {
            /* 
             * Call the OnScreenInitialize in back ground to load the screen with default values 
             * in the same time when the screen is prepared to launch.
             */
            try
            {
                InitializeComponent();
                _NoOfMachinesPerThread = Convert.ToInt32(ConfigurationManager.AppSettings["NoOfDropMachinesPerThread"]);
                if (_NoOfMachinesPerThread == 0) _NoOfMachinesPerThread = 1;
                _WaitTime = Convert.ToInt32(ConfigurationManager.AppSettings["PerformDropWaitTime"]);
                if (_WaitTime == 0) _WaitTime = 1;
                pgBarMachineDrop.Visibility = Visibility.Collapsed;
                txtPGStatus.Visibility = Visibility.Collapsed;
                _bgLoadScreen.DoWork += OnScreenInitialize;
                _bgLoadScreen.RunWorkerAsync();
                btnPrintDrop.Visibility = Visibility.Collapsed;
                if (Settings.DropSummaryReport == true && Settings.CentralizedDeclaration == true)
                {
                    //btnPrintDrop.Visibility = Visibility.Visible;
                }

            }
            catch (Exception ex) { LogError("CPerformDrop", ex); }
        }
        //
        #endregion Constructor
        //
        #region Background Thread Methods
        //
        void OnScreenInitialize(object sender, DoWorkEventArgs e)
        {
            /* 
             * Populate the active machines and set the default values for the drop screen
             */
            try
            {
                LogInfo("START Loading Drop Screen");
                PopulateActiveMachine(false);
                Dispatcher.Invoke(DispatcherPriority.Normal,
                                           (ThreadStart)delegate
                                           {
                                               InitializeScreen();
                                           });
            }
            catch (Exception ex) { LogError("OnScreenInitialize", ex); }
        }
        //
        void OnDropInitialize(object sender, DoWorkEventArgs e)
        {
            /* 
             * Get all the active machines and start performing the drop. 
             * After each drop performed update the progress and send the status 
             * to OnDropComplete delegate to add audit
             */
            try
            {
                int Start = (e.Argument as CollectionParams)._UniqueId * _NoOfMachinesPerThread;
                for (int i = Start; i < Start + _NoOfMachinesPerThread; i++)
                {
                    try
                    {
                        if (i > TOTAL_SELECTED_MACHINES - 1) continue;
                        try
                        {
                            lock (_lockDrop)
                            {
                                if (IsEventCleared)
                                {
                                    _collectionHelper.UpdateEventDetails(BMC.Security.SecurityHelper.CurrentUser.SecurityUserID, GetSelectedInstallations[i]);
                                }
                                if (_collectionHelper.PerformCollection(GetSelectedInstallations[i], (e.Argument as CollectionParams)._BatchID, _userNo, (e.Argument as CollectionParams)._collectiontype))
                                {
                                    if ((e.Argument as CollectionParams)._collectiontype == CollectionType.PartCollection)
                                        UpdateDropSuccess(i, DropStatus.PART_DROP_SUCCESS);
                                    else
                                        UpdateDropSuccess(i, DropStatus.NORMAL_DROP_SUCCESS);
                                }
                                else
                                    UpdateDropFailure(i);
                            }
                        }
                        catch (Exception ex)
                        {
                            UpdateDropFailure(i);
                            LogError("OnDropInitialize-PerformCollection", ex);
                        }
                        try
                        {
                            lock (_lockProgess)
                            {
                                _macCnt++;
                                UpdateProgressStatus(_macCnt + " Of " + TOTAL_SELECTED_MACHINES, (_macCnt * 100) / TOTAL_SELECTED_MACHINES);
                            }
                        }
                        catch (Exception ex) { LogError("OnDropInitialize-DropProgress", ex); }

                        if (_mDropEvent[(e.Argument as CollectionParams)._UniqueId].WaitOne(_WaitTime))
                        {
                            break;
                        }
                    }
                    catch (Exception ex) { LogError("OnDropInitialize-ForLoop", ex); }
                }
                e.Result = e.Argument;
            }
            catch (Exception ex) { LogError("OnDropInitialize", ex); }
        }
        //
        void OnDropComplete(object sender, RunWorkerCompletedEventArgs e)
        {
            /* 
             * Get all the active machines from the event args and audit the success and failure. 
             * Populate the undeclared batch in the drop down
             * Update the auto drop session and send an alert to STM
             */
            try
            {
                CollectionParams _params = e.Result as CollectionParams;
                _currentThreadProcessed++;
                if (_params._MaxThreads != _currentThreadProcessed) return;
                _currentThreadProcessed = 0;

                Audit(_params._BatchID, _params._collectiontype, SUCESS_BAR_POSITIONS, true);
                Audit(_params._BatchID, _params._collectiontype, FAILED_BAR_POSITIONS, false);

                ResetAutoDropSession();
                ResetDropBatch();
                EnableDisableNormalDropScreen(true);
                MarkMachineDropStatus(false, false, NOTSET, NOTSET, NOTSET, NOTSET.ToString(), NOTSET, NOTSET);
                txtPGStatus.Visibility = Visibility.Collapsed;
                PopulateActiveMachine(true);
                LogInfo("END Perform Drop");
                if (Settings.DropAlert) SendDropAlerttoSTM(_params._BatchID, GetDropType(_params._collectiontype));
                IsBusy = false;
            }
            catch (Exception ex)
            {
                LogError("OnDropComplete", ex);
                EnableDisableNormalDropScreen(true);
                IsBusy = false;
            }
        }
        //
        #endregion Background Thread Methods
        //
        #region Data Holder
        //
        public ObservableCollection<ZoneCollection> ZoneDataProperty
        {
            get
            {
                return _zoneData;
            }
            set
            {
                _zoneData = value;
                if (PropertyChanged != null)
                {
                    try
                    {
                        PropertyChanged.Invoke(this, new PropertyChangedEventArgs("ZoneDataProperty"));
                    }
                    catch (Exception ex)
                    {
                        LogManager.WriteLog("ZoneDataProperty : " + ex.Message, LogManager.enumLogLevel.Error);
                        ExceptionManager.Publish(ex);
                    }
                }
            }
        }
        //
        public ObservableCollection<CollectionMachine> MachineDataProperty
        {
            get
            {
                return _machineData;
            }
            set
            {
                _machineData = value;
                if (PropertyChanged != null)
                {
                    try
                    {
                        PropertyChanged.Invoke(this, new PropertyChangedEventArgs("MachineDataProperty"));
                    }
                    catch (Exception ex)
                    {
                        LogManager.WriteLog("MachineDataProperty : " + ex.Message, LogManager.enumLogLevel.Error);
                        ExceptionManager.Publish(ex);
                    }
                }
            }
        }
        //
        private string FAILED_BAR_POSITIONS { get; set; }
        //
        private string SUCESS_BAR_POSITIONS { get; set; }
        //
        private string FAILED_INSTALLAITONS { get; set; }
        //
        private string SUCESS_INSTALLAITONS { get; set; }
        //
        private int TOTAL_SELECTED_MACHINES { get; set; }
        //
        private string SELECTED_MACHINES { get; set; }
        //
        private string AUTO_DROP_SESSIONS { get; set; }
        //
        private int[] GetSelectedInstallations
        {
            get
            {
                lock (_lockInstallations)
                {
                    return _selectedInstallationNos;
                }
            }
        }
        //
        private string[] GetSelectedBarPositions
        {
            get
            {
                lock (_lockBarPositions)
                {
                    return _selectedBarPositions;
                }
            }
        }
        //
        #endregion Machine Data Holder
        //
        #region Functionality Methods
        //
        #region Screen Load Methods
        //
        public void PopulateActiveMachine(bool bRefresh)
        {
            /* 
             * We have two data template for displaying the machines in perform drop screen i.e, 
             * 1. Machine based template - list of active machines
             * 2. Zone based template - {key = zone name, machines - list of active machines under the zone name}
             * 
             */
            if (LstMachinesMasterList == null || bRefresh)
                LstMachinesMasterList = _collectionHelper.GetInstalledMachineForCollection().ToList();

            try
            {
                List<CollectionMachine> tempList = null;

                try
                {
                    //Filtering based on route.
                    if (_SelectedRoute > 0)
                        tempList = LstMachinesMasterList.Where(mac => mac.Route_No == ((RouteCollection)cboMachineType.Items[cboMachineType.SelectedIndex]).Route_No).ToList();
                    else
                        tempList = LstMachinesMasterList.Where(mac => mac.Route_No == 0).ToList();
                }
                catch (Exception Ex)
                {
                    ExceptionManager.Publish(Ex);
                    tempList = LstMachinesMasterList;

                }
                _machineData.Clear();
                _zoneData.Clear();

                foreach (var collectionMachine in tempList)
                {
                    _machineData.Add(collectionMachine);
                }
                if (_zoneSelected)
                {
                    tempList = tempList.OrderBy(mac => mac.Zone_Name).ToList();
                }

                foreach (var collectionMachine in tempList)
                {
                    if (_zoneData.Where(m => m.ZoneName == collectionMachine.Zone_Name).Count() != 1)
                        _zoneData.Add(new ZoneCollection() { ZoneName = collectionMachine.Zone_Name, Machines = new ObservableCollection<CollectionMachine>() });

                    _zoneData.Where(m => m.ZoneName == collectionMachine.Zone_Name).SingleOrDefault().Machines.Add(collectionMachine);
                }
            }
            catch (Exception ex) { LogError("PopulateActiveMachine", ex); }
        }
        //
        public void PopulateRouteDetails()
        {
            try
            {
                /* Add the routes to the machine type drop down. Currently we have two route i.e,
                 * 1. All Machines System - All the machines will be selected from the list
                 * 2. Custom - All the machines will be de-selected from the list
                 */
                List<RouteCollection> routes = new List<RouteCollection>();
                routes.Add(new RouteCollection() { Route_No = -1, Route_Name = FindResource("AllMachinesSystem") as string, Route_Default = false });
                routes.Add(new RouteCollection() { Route_No = 0, Route_Name = FindResource("Custom") as string, Route_Default = false });

                foreach (var list in _collectionHelper.GetRouteCollection())
                {

                    routes.Add(new RouteCollection
                    {
                        Route_No = list.Route_No,
                        Route_Name = list.Route_Name,
                        Route_Default = list.Route_Default
                    });
                }

                cboMachineType.ItemsSource = routes;
            }
            catch (Exception ex) { LogError("PopulateRouteDetails", ex); }
        }
        //
        private void LogError(string methodName, Exception ex)
        {
            LogManager.WriteLog("CPerformDrop Error in " + methodName + " : " + ex.Message, LogManager.enumLogLevel.Error);
            ExceptionManager.Publish(ex);
        }
        //
        private void LogInfo(string content)
        {
            LogManager.WriteLog("CPerformDrop Info - " + content + " : " +
                DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fff tt"), LogManager.enumLogLevel.Info);
        }
        //
        private void InitializeScreen()
        {
            try
            {
                /* Initialize screen will load the required details listed below for the screen
                 * 1. Display the machine template by default.
                 * 2. Populate the route details in the route drop down.
                 * 3. Enable Standard drop and New Batch check box by default.
                 * 4. Initialize Auto Drop current session if Auto Drop enabled.
                 */
                DisplayByMachineTemplate();
                PopulateRouteDetails();
                rdoFullCount.IsChecked = true;
                rdoExistingBatch.IsEnabled = false;
                rdoNewBatch.IsChecked = true;
                ResetDropBatch();
                if (cboDropBatch.Items.Count > 0)
                {
                    rdoExistingBatch.IsEnabled = true;
                    rdoExistingBatch.IsChecked = true;
                    cboDropBatch.IsEnabled = true;
                }
                btnPerformDrop.Visibility = Visibility.Visible;
                btnStartDrop.Visibility = Visibility.Collapsed;
                btnStopDrop.Visibility = Visibility.Collapsed;
                InitializeAutoDropSession();
                LogInfo("END Loading Drop Screen");
            }
            catch (Exception ex) { LogError("InitializeScreen", ex); }
        }
        //
        #endregion Screen Load Methods
        //
        #region Auto Drop Methods
        //
        private void InitializeAutoDropSession()
        {
            //
            try
            {
                /* InitializeAutoDropSession will be called if Auto Drop is enabled.
                 * if drop session is active then display the screen with it previos session status
                 */
                if (Settings.AutoDropEnabled && !rdoFinalCount.IsChecked.Value && !rdoInterimCount.IsChecked.Value)
                {
                    AUTO_DROP_SESSIONS = string.Empty;
                    List<BMC.Transport.InstallationData> activeDropSessions = _collectionHelper.GetDropActiveSessionData();

                    if (activeDropSessions.Count() > 0)
                    {
                        isADropStarted = true;
                        UnCheckAll();
                        foreach (BMC.Transport.InstallationData Installation in activeDropSessions)
                        {
                            MarkAutoDropSessions(Installation.AutoDropSessionNo);

                            MarkMachineDropStatus(true, false, Installation.StackerEventReceived ? 6 : NOTSET,
                                                    Installation.Installation_No, NOTSET, NOTSET.ToString(), NOTSET, NOTSET);
                        }
                        if (activeDropSessions[0].FinalDrop) rdoFinalCount.IsChecked = true;
                        MarkSelectedMachines();
                        EnableDisableAutoDropScreen(false);
                    }
                    else
                    {
                        EnableDisableAutoDropScreen(true);
                    }
                }
            }
            catch (Exception ex) { LogError("InitializeAutoDropSession", ex); }

        }
        //
        private void MarkAutoDropSessions(long autoDropSessionNo)
        {
            AUTO_DROP_SESSIONS += (string.IsNullOrEmpty(AUTO_DROP_SESSIONS) ? "" : ", ") + autoDropSessionNo.ToString();
        }
        //
        private string AutoDropSession(int? BatchNo)
        {
            try
            {
                return _collectionHelper.AutoDropSession(BatchNo);
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        //
        private void StartDropSession()
        {
            try
            {
                _collectionHelper.CreateDropSession(SELECTED_MACHINES, (rdoFinalCount.IsChecked == true ? 1 : 0), _userNo, ((bool)rdoExistingBatch.IsChecked ? ((UndeclaredCollection)cboDropBatch.SelectedItem).Collection_Batch_No : 0), ((RouteCollection)cboMachineType.Items[cboMachineType.SelectedIndex]).Route_No > 0 ? "Custom" : cboMachineType.Text);
            }
            catch (Exception ex) { LogError("StartDropSession", ex); }
        }
        //
        private void EnableDisableAutoDropScreen(bool isEnable)
        {
            btnStopDrop.Visibility = isEnable ? Visibility.Collapsed : Visibility.Visible;
            btnPerformDrop.Visibility = Visibility.Collapsed;
            cboMachineType.IsEnabled = isEnable;
            chkZone.IsEnabled = isEnable;
            //tvMachineList.IsEnabled = isEnable;
            Batch.IsEnabled = isEnable;
            RdDrop.IsEnabled = isEnable;
            btnStartDrop.Visibility = isEnable ? Visibility.Visible : Visibility.Collapsed;
        }
        //
        void clientObj_AutoDropClientReceived(object lst)
        {
            if (lst != null)
            {
                LogManager.WriteLog("Stacker Event Received", LogManager.enumLogLevel.Info);
                try
                {
                    if (lst is int)
                    {
                        MarkMachineDropStatus(true, false, Convert.ToInt32(DropStatus.AUTO_DROP_SUCCESS),
                            Convert.ToInt32(lst), NOTSET, NOTSET.ToString(), NOTSET, NOTSET, 0);
                    }
                }
                catch (Exception ex) { LogError("clientObj_AutoDropClientReceived", ex); }
            }
        }
        //
        private void ResetAutoDropSession()
        {
            if (Settings.AutoDropEnabled && (!rdoFinalCount.IsChecked.Value) && (!rdoInterimCount.IsChecked.Value))
            {
                _collectionHelper.UpdateAutoDropSession();
                InitializeAutoDropSession();
                _lockHandler.DeleteLockRecord(0, "", "AUTODROP", "STOP", "STOP");
                _lockHandler.DeleteLockRecord(0, "", "AUTODROP", "START", "START");
            }
        }

        private void ResetDropBatch()
        {
            try
            {
                cboDropBatch.ItemsSource = (new CollectionHelper()).GetUndeclaredCollection(false, false);

                if (cboDropBatch.Items.Count > 0)
                {
                    rdoExistingBatch.IsEnabled = true;
                    rdoExistingBatch.IsChecked = true;
                    cboDropBatch.IsEnabled = true;
                }
                pgBarMachineDrop.Visibility = Visibility.Collapsed;
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
        }
        //
        #endregion Normal Drop Methods
        //
        #region Normal Drop Methods
        //
        private static bool isBusy;
        public static bool IsBusy
        {
            get
            { return isBusy; }
            set
            {
                isBusy = value;
                OnPropertyChanged("IsBusy");
            }
        }

        // Create the OnPropertyChanged method to raise the event 
        public static void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = BusyPropertyChanged;
            if (handler != null)
            {
                handler(new CPerformDrop(), new PropertyChangedEventArgs(name));
            }
        }


        //
        private void EnableDisableNormalDropScreen(bool Enable)
        {
            btnPerformDrop.IsEnabled = Enable;
            cboMachineType.IsEnabled = Enable;
            chkZone.IsEnabled = Enable;
            tvMachineList.IsEnabled = Enable;
            Batch.IsEnabled = Enable;
            RdDrop.IsEnabled = Enable;
        }
        //
        private void MarkMachineDropStatus(bool isSelected, bool isFinalDrop, int dropStatus, int where_InstallationNo, int where_DropStatus,
                                        string where_ZoneName, int where_InstallationFloatStatus, int where_RouteNo, int isDeclaredstatus = -1)
        {


            lock (_lockMachine)
            {
                var Machines = (_zoneSelected == true) ?
                        ZoneDataProperty.SelectMany(m => m.Machines).
                        Where(a => a.Installation_No == (where_InstallationNo == -1 ? a.Installation_No : where_InstallationNo)).
                        Where(a => a.IsCollectable == (where_DropStatus == -1 ? a.IsCollectable : where_DropStatus)).
                        Where(a => a.Zone_Name == (where_ZoneName == "-1" ? a.Zone_Name : where_ZoneName)).
                        Where(a => a.Installation_Float_Status == (where_InstallationFloatStatus == -1 ? a.Installation_Float_Status : where_InstallationFloatStatus)).
                        Where(a => a.Route_No == (where_RouteNo == -1 ? a.Route_No : where_RouteNo))
                        :
                        MachineDataProperty.Where(m => m.Installation_No == (where_InstallationNo == -1 ? m.Installation_No : where_InstallationNo)).
                        Where(a => a.IsCollectable == (where_DropStatus == -1 ? a.IsCollectable : where_DropStatus)).
                        Where(a => a.Installation_Float_Status == (where_InstallationFloatStatus == -1 ? a.Installation_Float_Status : where_InstallationFloatStatus)).
                        Where(a => a.Route_No == (where_RouteNo == -1 ? a.Route_No : where_RouteNo));

                foreach (CollectionMachine Machine in Machines)
                {

                    Machine.IsCollectable = (dropStatus == -1) ? ((isADropStarted && (Machine.IsCollectable == -1 || Machine.IsCollectable == 1)) ? dropStatus : Machine.IsCollectable) : dropStatus;
                    Machine.IsSelected = (dropStatus == -1 && Machine.IsCollectable != -1 || Machine.Installation_Float_Status == 1) ? Machine.IsSelected : isSelected;
                    Machine.IsDeclared = (isDeclaredstatus == -1 ? Machine.IsDeclared : isDeclaredstatus);
                    if (isADropStarted)
                    {
                        Machine.IsSelected = isSelected;
                    }
                    else if (dropStatus == -1 && Machine.IsCollectable == 1 && Machine.Installation_Float_Status == 0)
                    {
                        Machine.IsSelected = isSelected;
                    }

                    if ((!isADropStarted) && Machine.IsCollectable == -1)
                    {
                        Machine.IsCollectable = 1;
                    }
                    if (isFinalDrop)
                    {
                        Machine.Installation_Float_Status = 1;
                        Machine.FinalCollection_Status = 0;
                    }
                }

            }
        }
        //
        private void PerformDrop(ref bool bExit, bool bManualDrop)
        {
            try
            {
                int BatchNo = 0;
                int maxThreads = 0;
                _macCnt = 0;
                _currentThreadProcessed = 0;


                
                if (Settings.AllowMultipleDrops && rdoExistingBatch.IsChecked.Value && rdoFullCount.IsChecked.Value)
                {
                    foreach (int InstallationNo in GetSelectedInstallations)
                    {
                        if(_collectionHelper.CheckPreviousDeclarationStatus(0,0,InstallationNo) > 0)
                        {
                            MessageBox.ShowBox("MessageID900", BMC_Icon.Error);
                            bExit = true;
                            EnableDisableNormalDropScreen(true);
                            return;
                        }
                    }
                }

                if (_collectionHelper != null && (rdoInterimCount.IsChecked.Value || rdoFinalCount.IsChecked.Value))
                {
                    List<BMC.Transport.InstallationData> activeDropSessions = _collectionHelper.GetDropActiveSessionData();
                    foreach (int InstallationNo in GetSelectedInstallations)
                    {
                        var lockCount = _lockHandler.GetLoclRecord(0, "", "", "INST", InstallationNo.ToString()).Count();
                        if (lockCount <= 0)
                        {
                            lockCount = activeDropSessions.Count(i => i.Installation_No == InstallationNo);
                        }
                        if (lockCount > 0)
                        {
                            MessageBox.ShowBox("MessageID82", BMC_Icon.Error);
                            bExit = true;
                            EnableDisableNormalDropScreen(true);
                            return;
                        }
                    }
                }
                
                //

                if (rdoInterimCount.IsChecked != true) //Skip events clear for part collection
                    IsEventCleared = ClearEvents();

                DoFinalDrop(bManualDrop);

                if (!PerformLock())
                {
                    bExit = true;
                    EnableDisableNormalDropScreen(true);
                    return;
                }

                MarkSelectedMachines();

                if (TOTAL_SELECTED_MACHINES <= 0)
                {
                    bExit = true;
                    EnableDisableNormalDropScreen(true);
                    return;
                }
                LogManager.WriteLog("Assigned Value to dropType : " + GetDropType(GetCollectionType()), LogManager.enumLogLevel.Info);

                BatchNo = CreateBatch();

                LogInfo("START Perform Drop");
                IsBusy = true;
                maxThreads = (TOTAL_SELECTED_MACHINES / _NoOfMachinesPerThread) + (TOTAL_SELECTED_MACHINES % _NoOfMachinesPerThread);
                _bgPerformDropArray = new BackgroundWorker[maxThreads];
                _mDropEvent = new ManualResetEvent[maxThreads];
                pgBarMachineDrop.Visibility = Visibility.Visible;
                txtPGStatus.Visibility = Visibility.Visible;
                txtPGStatus.Text = "";
                pgBarMachineDrop.Value = 0;
                for (int i = 0; i < maxThreads; i++)
                {
                    _mDropEvent[i] = new ManualResetEvent(false);
                    _bgPerformDropArray[i] = new BackgroundWorker();
                    _bgPerformDropArray[i].DoWork += OnDropInitialize;
                    _bgPerformDropArray[i].RunWorkerCompleted += OnDropComplete;
                    _bgPerformDropArray[i].RunWorkerAsync(new CollectionParams()
                    {
                        _BatchID = BatchNo,
                        _collectiontype = GetCollectionType(),
                        _UniqueId = i,
                        _MaxThreads = maxThreads
                    });
                    _bgPerformDropArray[i].WorkerSupportsCancellation = true;
                }


            }
            catch (Exception ex) { LogError("PerformDrop", ex); }
        }

        private int CreateBatch()
        {
            int? batchID = 0;
            if (rdoInterimCount.IsChecked != true)
                if ((bool)rdoExistingBatch.IsChecked)
                    batchID = ((UndeclaredCollection)cboDropBatch.SelectedItem).Collection_Batch_No;
                else
                    if (rdoInterimCount.IsChecked != true)
                        _collectionHelper.CreateCollectionBatch(((RouteCollection)cboMachineType.Items[cboMachineType.SelectedIndex]).Route_No == 0 ? "Custom" : cboMachineType.Text, _userNo, ref batchID);

            return Convert.ToInt32(batchID);


        }
        //
        private void DoFinalDrop(bool bManualDrop)
        {
            MachineManagerLazyInitializer manager = null;

            try
            {

                if (rdoFinalCount.IsChecked == true)
                {
                    //if ((!Settings.AutoDropEnabled) || bManualDrop)

                    InstallationDataContext installationDataContext =
                                    new InstallationDataContext(oCommonUtilities.CreateInstance().GetConnectionString());


                    foreach (CreditStatus credits in _collectionHelper.GetHandPayPlayCreditStatus(SELECTED_MACHINES))
                    {
                        if (_mEvent.WaitOne(10))
                        {
                            break;
                        }


                        _InstallationnumberMR = Convert.ToInt32(credits.Installation_No);

                        if (credits.IsHandPayUnProcessed != 0 || credits.isCardedPlay != 0 || credits.inplay != 0)
                        {

                            MarkMachineDropStatus(false, false, NOTSET, Convert.ToInt32(credits.Installation_No), NOTSET, NOTSET.ToString(), NOTSET, NOTSET);
                            string strMsg = (credits.IsHandPayUnProcessed != 0) ? System.Windows.Application.Current.FindResource("MessageID313").ToString() :
                                (credits.inplay != 0) ? System.Windows.Application.Current.FindResource("MessageID316").ToString() : System.Windows.Application.Current.FindResource("MessageID317").ToString();
                            strMsg += " (" + GetStockNo(credits.Installation_No.Value) + ")";
                            MessageBox.ShowBox(strMsg, BMC_Icon.Warning, true);

                        }
                        else if (credits.isGMUUPdate != 0)
                        {
                            MarkMachineDropStatus(false, false, NOTSET, Convert.ToInt32(credits.Installation_No), NOTSET, NOTSET.ToString(), NOTSET, NOTSET);
                            string strMsg = System.Windows.Application.Current.FindResource("MessageID316a").ToString().Replace("@@@@@@",GetStockNo(credits.Installation_No.Value));                           
                            MessageBox.ShowBox(strMsg, BMC_Icon.Warning, true);
                        }
                        else
                        {
                            try
                            {
                                manager = new MachineManagerLazyInitializer();
                                int nSuccess = 0;
                                if (Settings.Disable_Machine_On_Drop)
                                    nSuccess = manager.GetMachineManager().DisableMachineFromUI(Convert.ToInt32(credits.Installation_No));
                                else
                                    LogManager.WriteLog("CPerformDrop->DoFinalDrop: Skip Disable Machine as Disable_Machine_On_Drop is false", LogManager.enumLogLevel.Info);


                                if (nSuccess != 0)
                                {
                                    if (!(Settings.NoWaitForDisableMachine))
                                    {
                                        MarkMachineDropStatus(false, false, NOTSET, Convert.ToInt32(credits.Installation_No), NOTSET, NOTSET.ToString(), NOTSET, NOTSET);
                                        MessageBox.ShowBox("MessageID361", BMC_Icon.Warning, GetStockNo(Convert.ToInt32(credits.Installation_No)));
                                    }
                                }

                                //Allow Machine Removal in CDO when CentralizedDeclaration declaration is Enabled 
                                //Allow Machine Removal in CDO when multiple drops is Enabled 
                                if (Settings.IsFinalDropRequiredForRemoval&&(Settings.AllowMultipleDrops || Settings.Allow_Machine_Removal) && rdoFinalCount.IsChecked == true)
                                {
                                    installationDataContext = new InstallationDataContext(oCommonUtilities.CreateInstance().GetConnectionString());
                                    installationDataContext.updateInstallationFloatStatus(_InstallationnumberMR);
                                }
                                //Allow Machine Removal in CDO when CentralizedDeclaration declaration is Enabled 

                            }
                            finally
                            {
                                if (manager != null)
                                {
                                    manager.ReleaseMachineManager();
                                    manager = null;
                                }
                            }

                        }

                        //}
                    }
                }
            }
            catch (Exception ex) { LogError("DoFinalDrop", ex); }
        }
        //
        private bool ClearEvents()
        {
            bool retVal = true;
            try
            {
                if (_collectionHelper.IsEventsUnCleared(SELECTED_MACHINES))
                {
                    if (!(Settings.ClearEventsOnFinalDrop.ToUpper() == "AUTO" || (Settings.AutoDropEnabled && !rdoFinalCount.IsChecked.Value)))
                        if (MessageBox.ShowBox("MessageID315", BMC_Icon.Question, BMC_Button.YesNo) == System.Windows.Forms.DialogResult.Yes)
                        {
                            if (!Security.SecurityHelper.HasAccess("CashdeskOperator.FloorView.cs.Events.ClearEvents"))
                            {
                                MessageBox.ShowBox("MessageID333", BMC_Icon.Information, BMC_Button.OK);
                                retVal = false;
                            }
                        }
                        else
                        {
                            retVal = false;
                        }
                }

            }
            catch (Exception ex) { LogError("ClearEvents", ex); retVal = false; }
            return retVal;
        }
        //
        private void MarkSelectedMachines()
        {
            try
            {
                List<CollectionMachine> collectionMachines = new List<CollectionMachine>();
                _selectedInstallationNos = null;
                _selectedBarPositions = null;
                int i = 0;
                TOTAL_SELECTED_MACHINES = 0;
                SELECTED_MACHINES = string.Empty;
                if (_zoneSelected)
                {
                    foreach (var zoneCollection in ZoneDataProperty)
                        collectionMachines.AddRange(zoneCollection.Machines.Where(m => m.IsSelected == true).Select(m => m));
                }
                else
                {
                    collectionMachines.AddRange(MachineDataProperty.Where(m => m.IsSelected == true).Select(m => m));
                }
                //
                TOTAL_SELECTED_MACHINES = collectionMachines.Count();
                _selectedInstallationNos = new int[TOTAL_SELECTED_MACHINES];
                _selectedBarPositions = new string[TOTAL_SELECTED_MACHINES];
                //
                foreach (CollectionMachine Machine in collectionMachines)
                {
                    _selectedInstallationNos[i] = Machine.Installation_No;
                    _selectedBarPositions[i] = Machine.Bar_Pos_Name;
                    SELECTED_MACHINES += (string.IsNullOrEmpty(SELECTED_MACHINES) ? "" : ", ") + Machine.Installation_No.ToString();
                    i++;
                }
            }
            catch (Exception ex) { LogError("MarkSelectedMachines", ex); }
        }
        //
        private void UnCheckAll()
        {
            if (chkZone.IsChecked.Value)
                CheckAllZone();
            MarkMachineDropStatus(false, false, NOTSET, NOTSET, NOTSET, NOTSET.ToString(), NOTSET, NOTSET);
        }
        //
        private void CheckAll()
        {
            if (chkZone.IsChecked.Value)
                CheckAllZone();
            MarkMachineDropStatus(true, false, NOTSET, NOTSET, NOTSET, NOTSET.ToString(), NOTSET, NOTSET);
        }
        //
        private string GetDropType(CollectionType Type)
        {
            return ((Type == CollectionType.FullCollection) ? "Full" : (Type == CollectionType.PartCollection) ? "Partial" : "Defloat");
        }

        private void DisableFinalDropOnAutoDrop(bool isEnable)
        {
            btnStartDrop.Visibility = (Settings.AutoDropEnabled && isEnable) ? Visibility.Visible : Visibility.Collapsed;
            btnPerformDrop.Visibility = (Settings.AutoDropEnabled && isEnable) ? Visibility.Collapsed : Visibility.Visible;
        }
        //
        private void DisableInterimDropOnAutoDrop(bool isEnable)
        {
            btnStartDrop.Visibility = (Settings.AutoDropEnabled && isEnable) ? Visibility.Visible : Visibility.Collapsed;
            btnPerformDrop.Visibility = (Settings.AutoDropEnabled && isEnable) ? Visibility.Collapsed : Visibility.Visible;
        }
        private CollectionType GetCollectionType()
        {
            return ((rdoFullCount.IsChecked == true) ? CollectionType.FullCollection : (rdoInterimCount.IsChecked == true) ?
                CollectionType.PartCollection : CollectionType.DefloatCollection);
        }
        //
        private bool PerformLock()
        {
            try
            {
                if (_lockHandler.GetLoclRecord(0, "", MACHINEADMIN, "", "").Count() > 0)
                {
                    MessageBox.ShowBox("MessageID81", BMC_Icon.Error);
                    return false;
                }

                foreach (int InstallationNo in GetSelectedInstallations)
                {
                    var lockCount = _lockHandler.GetLoclRecord(0, "", "", "INST", InstallationNo.ToString()).Count();
                    //if (lockCount <= 0 && _collectionHelper != null && rdoFinalCount.IsChecked.Value)
                    //{
                    //    List<BMC.Transport.InstallationData> activeDropSessions = _collectionHelper.GetDropActiveSessionData();
                    //    lockCount = activeDropSessions.Count(i => i.Installation_No == InstallationNo);
                    //}
                    if (lockCount <= 0) continue;
                    if (TOTAL_SELECTED_MACHINES > 0)
                    {
                        MessageBox.ShowBox("MessageID82", BMC_Icon.Error);
                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                LogError("PerformLock", ex);
                return false;
            }
        }
        //
        private void UpdateDropSuccess(int i, DropStatus dropStatus)
        {
            try
            {
                MarkMachineDropStatus(false, _finalDrop, Convert.ToInt32(dropStatus), GetSelectedInstallations[i], NOTSET, NOTSET.ToString(), NOTSET, NOTSET);
                SUCESS_BAR_POSITIONS += (string.IsNullOrEmpty(SUCESS_BAR_POSITIONS) ? "" : ", ") + GetSelectedBarPositions[i];
                SUCESS_INSTALLAITONS += (string.IsNullOrEmpty(SUCESS_INSTALLAITONS) ? "" : ", ") + GetSelectedInstallations[i].ToString();
            }
            catch (Exception ex) { LogError("UpdateDropSuccess", ex); }
        }
        //
        private void UpdateDropFailure(int i)
        {
            try
            {
                MarkMachineDropStatus(false, false, Convert.ToInt32(DropStatus.DROP_FAILURE), GetSelectedInstallations[i], NOTSET, NOTSET.ToString(), NOTSET, NOTSET);
                FAILED_BAR_POSITIONS += (string.IsNullOrEmpty(FAILED_BAR_POSITIONS) ? "" : ", ") + GetSelectedBarPositions[i];
                FAILED_INSTALLAITONS += (string.IsNullOrEmpty(FAILED_INSTALLAITONS) ? "" : ", ") + GetSelectedInstallations[i].ToString();
            }
            catch (Exception ex) { LogError("UpdateDropFailure", ex); }
        }
        //
        private void UpdateProgressStatus(string progress, int iProMac)
        {
            /* 
             * Used dispatcher as the UI thread will be accessed from another thread
             */
            System.Windows.Application.Current.Dispatcher.Invoke((ThreadStart)delegate
            {
                txtPGStatus.Text = progress;
                pgBarMachineDrop.Value = iProMac;
            });
        }
        //
        private void Audit(int batchID, CollectionType collectionType, string BarPos, bool Success)
        {
            /* 
             * Audit the success and failure of the drop performed
             */

            try
            {
                if (!string.IsNullOrEmpty(BarPos))
                {
                    Audit.Transport.Audit_History AH = new Audit.Transport.Audit_History();

                    AH.AuditModuleName = ModuleName.MachineDrop;

                    AH.Audit_Screen_Name = (Settings.AutoDropEnabled && collectionType == BMC.CashDeskOperator.CollectionType.FullCollection) ? "AutoDrop|Drop" : "MachineDrop|Drop";
                    if (batchID == 0) // Part_Collection 
                        AH.Audit_Desc = "Batch: " + batchID + ",DropType: " + GetDropType(collectionType);
                    else
                        AH.Audit_Desc = "Batch: " + batchID + ",DropType: " + GetDropType(collectionType) + AutoDropSession(batchID);
                    AH.AuditOperationType = OperationType.ADD;
                    AH.Audit_Field = Success ? "Machines Dropped" : "Drop Failed Machines";

                    if (Success && Settings.CentralizedDeclaration && batchID > 0)
                        _collectionHelper.InsertIntoExportHistory(batchID);

                    AH.Audit_New_Vl = BarPos;

                    if (AH.Audit_New_Vl.EndsWith(", "))
                        AH.Audit_New_Vl = AH.Audit_New_Vl.Substring(0, AH.Audit_New_Vl.Length - 2);

                    AuditViewerBusiness.InsertAuditData(AH);
                }
            }
            catch (Exception ex) { LogError("Audit", ex); }

        }
        //
        private void DisplayByZoneTemplate()
        {
            var displayByZone = (FindResource("DisplayByZone") as HierarchicalDataTemplate);
            tvMachineList.ItemsSource = ZoneDataProperty;
            tvMachineList.ItemTemplate = displayByZone;
        }
        //
        private void DisplayByMachineTemplate()
        {
            var displayByZone = (FindResource("DisplayByMachines") as HierarchicalDataTemplate);
            tvMachineList.ItemsSource = MachineDataProperty;
            tvMachineList.ItemTemplate = displayByZone;
        }
        //
        #endregion Normal Drop Methods
        //
        #region STM Methods
        //
        private string GetStockNo(int where_InstallationNo)
        {
            var Machines = (_zoneSelected == true) ?
                    ZoneDataProperty.SelectMany(m => m.Machines).
                    Where(a => a.Installation_No == where_InstallationNo) :
                    MachineDataProperty.Where(m => m.Installation_No == where_InstallationNo);

            foreach (CollectionMachine Machine in Machines)
            {
                return Machine.Stock_No;
            }
            return "";
            //
        }
        //
        private void SendDropAlerttoSTM(int? batchID, String strDropType)
        {
            int NoOfMaxPositionsPerAlert = 250;
            string BarPositionList = string.Empty;
            string[] BarPosArray = null;
            string[] InstallationArray = null;
            string SendBarPositions = string.Empty;
            string InstationNoList = string.Empty;

            try
            {
                LogInfo("START STM Alert");
                if (ConfigManager.Read("NoOfMaxPositionsPerAlert") != null)
                {
                    NoOfMaxPositionsPerAlert = Convert.ToInt32(ConfigManager.Read("NoOfMaxPositionsPerAlert"));
                }

                if (ConfigManager.Read("SendSingleDropAlert") != null)
                {
                    if (ConfigManager.Read("SendSingleDropAlert").ToUpper() == "TRUE")
                        NoOfMaxPositionsPerAlert = 1;
                }

                if (NoOfMaxPositionsPerAlert == 0) NoOfMaxPositionsPerAlert = 1;

                BarPosArray = SUCESS_BAR_POSITIONS.Split(',');
                InstallationArray = SUCESS_INSTALLAITONS.Split(',');

                if (batchID != null && batchID != 0)
                {
                    var dropAlertData = _collectionHelper.GetDropAlertData((int)batchID);

                    foreach (var dropAlert in dropAlertData)
                    {
                        BarPositionList = "," + SUCESS_BAR_POSITIONS + ",";
                        for (int i = NoOfMaxPositionsPerAlert; i < BarPosArray.Count() - 2; i = i + NoOfMaxPositionsPerAlert)
                        {
                            SendBarPositions = BarPositionList.Substring(1, BarPositionList.IndexOf("," + BarPosArray[i] + ",") + BarPosArray[i].Length);
                            BarPositionList = BarPositionList.Substring(SendBarPositions.Length + 1);
                            ExportToSTM(dropAlert, SendBarPositions, BarPosArray[i], strDropType, GetStockNo(Convert.ToInt32(InstallationArray[i])));
                        }
                        //
                        if (BarPositionList.Length > 0)
                        {
                            SendBarPositions = BarPositionList.Substring(1, BarPositionList.Length - 2);
                            ExportToSTM(dropAlert, SendBarPositions, BarPosArray[BarPosArray.Count() - 1], strDropType,
                                GetStockNo(Convert.ToInt32(InstallationArray[InstallationArray.Count() - 1])));
                        }
                        SendBarPositions = string.Empty;
                    }

                    if (!String.IsNullOrEmpty(SUCESS_INSTALLAITONS))
                    {
                        _collectionHelper.ResetStackerLevel(SUCESS_INSTALLAITONS);
                    }

                }
                LogInfo("END STM Alert");
            }
            catch (Exception ex) { LogError("SendDropAlerttoSTM", ex); }
        }
        //
        private void ExportToSTM(GetDropAlertDataResult dropAlert, string sendBarPositions, string stand, string dropType, string asset)
        {
            try
            {
                dropAlert.DropPositionsList = sendBarPositions;
                dropAlert.Stand = stand;
                dropAlert.DropType = dropType;
                dropAlert.Asset = asset;
                XElement XMLData = dropAlert.GetXml();
                String siteCode = XMLData.Element("SiteId").Value;
                _collectionHelper.Insert_STM_Export_History("DROP", 1, siteCode, XMLData);
            }
            catch (Exception ex) { LogError("ExportToSTM", ex); }
        }
        //
        #endregion STM Methods
        //
        #endregion Functionality Methods
        //
        #region Event Methods
        //
        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            int iResult = 0;
            LockHandler Lock = new LockHandler();
            //
            try
            {
                MarkSelectedMachines();
                if (TOTAL_SELECTED_MACHINES <= 0)
                {
                    MessageBox.ShowBox(iResult == -1 ? "MessageID378" : "MessageID80", BMC_Icon.Information);
                    return;
                }
                if ((rdoFinalCount.IsChecked == true) && (!AbandonCreditonAutoDrop()))
                {
                    return;
                }
                int m_count = 0;
                StringBuilder strMsg = new StringBuilder();
                foreach (int installation_no in _selectedInstallationNos)
                {
                    if (m_count > 3)
                    {
                        break;
                    }
                    if (!_collectionHelper.IsUndeclaredPartCollection(installation_no))
                    {
                        strMsg.Append(GetStockNo(installation_no) + ",");
                        m_count++;
                    }

                }
                if (strMsg.Length > 0)
                {
                    strMsg = strMsg.Remove(strMsg.Length-1, 1);
                    if (m_count > 3)
                    {
                        strMsg.Append("... ");
                    }
                    MessageBox.ShowBox(System.Windows.Application.Current.FindResource("MessageID82a").ToString().Replace("@@@@@", strMsg.ToString()),BMC_Icon.Information, true);
                    PopulateActiveMachine(true);
                    return;
                }
                //
                iResult = Lock.InsertLockRecord(0, "", "AUTODROP", "START", "START");
                LogManager.WriteLog("AUTODROP Lock Result: " + iResult.ToString(), LogManager.enumLogLevel.Debug);
                if (iResult == 1)
                {
                    MessageBox.ShowBox("MessageID380", BMC_Icon.Error);
                    InitializeAutoDropSession();
                    return;
                }
                //
                StartDropSession();
                //
                InitializeAutoDropSession();
                isADropStarted = true;

            }
            catch (Exception ex)
            {
                EnableDisableAutoDropScreen(true);
                Lock.DeleteLockRecord(0, "", "AUTODROP", "START", "START");
                LogError("btnStart_Click", ex);
                isADropStarted = false;
            }

        }
        //
        private void btnStopDrop_Click(object sender, RoutedEventArgs e)
        {
            int iResult = 0;
            bool bExit = false;
            bool bManualDrop = false;
            string Asset = string.Empty;
            try
            {
                if (_collectionHelper.IsDropSessionCompleted(AUTO_DROP_SESSIONS))
                {
                    MessageBox.ShowBox("MessageID466", BMC_Icon.Information);

                    if (_lockHandler != null)
                    {
                        foreach (int InstallationNo in GetSelectedInstallations)
                        {
                            var lockCount = _lockHandler.GetLoclRecord(0, "", "", "INST", InstallationNo.ToString()).Count();
                            isADropStarted = false;
                            if (lockCount > 0)
                            {

                                MarkMachineDropStatus(false, false, Convert.ToInt32(DropStatus.NORMAL_DROP_SUCCESS), InstallationNo, NOTSET, NOTSET.ToString(), NOTSET, NOTSET);
                            }
                        }
                    }
                    InitializeAutoDropSession();
                    MarkMachineDropStatus(false, false, NOTSET, NOTSET, NOTSET, NOTSET.ToString(), NOTSET, NOTSET);
                    PopulateActiveMachine(true);
                    return;
                }
                //
                bool More_AUTO_DROP_SESSIONS = (AUTO_DROP_SESSIONS.IndexOf(',') != -1);
                if (Settings.IsMachineBasedAutoDrop)
                {
                    List<BMC.Transport.InstallationData> activeDropSessions = _collectionHelper.GetDropActiveSessionData();

                    if (activeDropSessions.Count() > 0)
                    {
                        if ((AUTO_DROP_SESSIONS == activeDropSessions[0].AutoDropSessionNo.ToString())
                            ||
                            (More_AUTO_DROP_SESSIONS && AUTO_DROP_SESSIONS.Split(',').Any(obj => obj.ToString() == activeDropSessions[0].AutoDropSessionNo.ToString())))
                        {
                            if (activeDropSessions[0].Batch_Machine.ToUpper() != Environment.MachineName.ToUpper())
                            {

                                LogManager.WriteLog("AUTODROP Performed in another machine in :" + activeDropSessions[0].Batch_Machine.ToUpper() + "; AUTODROP SessionID:" + AUTO_DROP_SESSIONS, LogManager.enumLogLevel.Debug);
                                MessageBox.ShowBox("MessageID528", BMC_Icon.Information, BMC_Button.OK, activeDropSessions[0].Batch_Machine.ToUpper());
                                return;
                            }
                        }
                    }
                }

                iResult = _lockHandler.InsertLockRecord(0, "", "AUTODROP", "STOP", "STOP");
                LogManager.WriteLog("AUTODROP Lock Result: " + iResult.ToString(), LogManager.enumLogLevel.Debug);
                if (iResult == 1)
                {
                    MessageBox.ShowBox("MessageID380", BMC_Icon.Error);
                    return;
                }
                //
                isADropStarted = false;
                if (Settings.ForceManualDrop)
                {
                    Asset = _collectionHelper.IsStackerEventNotReceived(SELECTED_MACHINES);
                    if (!string.IsNullOrEmpty(Asset))
                        if (MessageBox.ShowBox((Asset.IndexOf(',') != -1) ? "MessageID447" : "MessageID447a", BMC_Icon.Question, BMC_Button.YesNo, Asset) == System.Windows.Forms.DialogResult.Yes)
                        {
                            bManualDrop = true;
                        }
                }
                if (!bManualDrop) UnCheckAll();
                //
                MarkSelectedMachines();
                //
                if (TOTAL_SELECTED_MACHINES <= 0)
                {
                    ResetAutoDropSession();
                    ResetDropBatch();
                    return;
                }
                //
                PerformDrop(ref bExit, bManualDrop);
                //
                if (bExit)
                {
                    ResetAutoDropSession();
                    ResetDropBatch();
                    return;
                }
            }
            catch (Exception ex)
            {
                EnableDisableAutoDropScreen(false);
                _lockHandler.DeleteLockRecord(0, "", "AUTODROP", "STOP", "STOP");
                LogError("btnStopDrop_Click", ex);
                isADropStarted = true;
            }
        }
        //
        private void btnPerformDrop_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bool bExit = true;
                if (MessageBox.ShowBox("MessageID202", BMC_Icon.Question, BMC_Button.YesNo) == DialogResult.No) return;
                MarkSelectedMachines();
                if (TOTAL_SELECTED_MACHINES <= 0)
                {
                    MessageBox.ShowBox("MessageID80", BMC_Icon.Warning);
                    return;
                }
                EnableDisableNormalDropScreen(false);
                PerformDrop(ref bExit, false);
            }
            catch (Exception ex) { LogError("btnPerformDrop_Click", ex); }
        }
        //;
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            App.client.SendToServer(App.clientObj, null);
            App.clientObj.ClientReceived += new ClientReceivedEvent(clientObj_AutoDropClientReceived);

            if ((!Settings.CentralizedDeclaration) && Security.SecurityHelper.HasAccess("BMC.Presentation.CPerformDrop.PartCollectionDrop") && (Settings.IsPartCollectionEnabled))
            {
                rdoInterimCount.Visibility = Visibility.Visible;
            }
            else
            {
                rdoInterimCount.Visibility = Visibility.Collapsed;
            }
            IsPartCollectionDeclaration = Security.SecurityHelper.HasAccess("BMC.Presentation.CDeclaration.PartCollectionDeclaration");
        }
        //
        private void chkZone_Checked(object sender, RoutedEventArgs e)
        {
            _zoneSelected = true;
            DisplayByZoneTemplate();
            DisplaySelectedMachines();
        }
        //
        private void chkZone_UnChecked(object sender, RoutedEventArgs e)
        {
            _zoneSelected = false;
            DisplayByMachineTemplate();
            DisplaySelectedMachines();
        }
        //
        private void CheckAllZone()
        {

            if (((RouteCollection)cboMachineType.Items[cboMachineType.SelectedIndex]).Route_No == 0)
                foreach (ZoneCollection zn in ZoneDataProperty)
                {
                    int n = 0;
                    n = zn.Machines.ToList().FindAll(m => m.IsSelected == true).Count;
                    if (zn.Machines.Count != n)
                        zn.IsSelected = false;
                }
            else// if (((RouteCollection)cboMachineType.Items[cboMachineType.SelectedIndex]).Route_No == -1)
                foreach (ZoneCollection zn in ZoneDataProperty)
                {
                    int n = 0;
                    n = zn.Machines.ToList().FindAll(m => m.IsSelected == true).Count;
                    if (zn.Machines.Count == n)
                        zn.IsSelected = true;
                }
        }

        private bool AbandonCreditonAutoDrop()
        {
            bool retval = false;
            try
            {
                List<FloorStatusData> actualResults = new List<FloorStatusData>();
                using (InstallationDataContext installationDataContext2 =
                       new InstallationDataContext(oCommonUtilities.CreateInstance().GetConnectionString()))
                {
                    actualResults = installationDataContext2.GetSlotStatus("", -1);
                }
                if (actualResults != null && actualResults.Count > 0)
                {

                    var InPlayStatus = from a in actualResults
                                       where (((SlotMachineStatus)Enum.Parse(typeof(SlotMachineStatus), a.Slot_Status) == SlotMachineStatus.MachineInPlay)
                                       && _selectedInstallationNos.Any(o => o.ToString() == a.Install_No.ToString()))
                                       select a;
                    if (InPlayStatus.Count() > 0)
                    {
                        StringBuilder str_InPlayStatus = new StringBuilder();
                        foreach (var obj in InPlayStatus)
                        {
                            str_InPlayStatus.AppendLine("Machine Number: " + obj.Asset_No + " Bar Position: " + obj.Bar_Pos_Name);
                        }
                        ScrollableMessageBox s_box = new ScrollableMessageBox(System.Windows.Application.Current.FindResource("MessageID523").ToString(), str_InPlayStatus.ToString());
                        s_box.Owner = MessageBox.parentOwner;
                        s_box.ShowDialogEx(this);
                        retval = s_box._isOk;

                    }
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return retval;
        }


        private void cboMachineType_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            try
            {
                DisplaySelectedMachines();
            }
            catch (Exception ex) { LogError("cboMachineType_SelectionChanged", ex); }
        }
        //
        private void DisplaySelectedMachines()
        {
            _SelectedRoute = 0;
            if (((RouteCollection)cboMachineType.Items[cboMachineType.SelectedIndex]).Route_No == 0)
            {
                PopulateActiveMachine(false);
                UnCheckAll();
            }
            else if (((RouteCollection)cboMachineType.Items[cboMachineType.SelectedIndex]).Route_No == -1)
            {
                PopulateActiveMachine(false);
                CheckAll();
            }
            else
            {

                _SelectedRoute = ((RouteCollection)cboMachineType.Items[cboMachineType.SelectedIndex]).Route_No;
                PopulateActiveMachine(false);
                CheckAll();
                MarkMachineDropStatus(true, false, NOTSET, NOTSET, NOTSET, NOTSET.ToString(), NOTSET, ((RouteCollection)cboMachineType.Items[cboMachineType.SelectedIndex]).Route_No);

            }

            InitializeAutoDropSession();
        }
        //
        private void rdoFinalCount_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                _finalDrop = true;
                DisableFinalDropOnAutoDrop(false);
                UnCheckAll();
                if (cboMachineType.Items.Count >= 2)
                    cboMachineType.SelectedIndex = 1;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        //
        private void rdoFinalCount_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                _finalDrop = false;
                DisableFinalDropOnAutoDrop(true);
                CheckAll();
                if (cboMachineType.Items.Count >= 1)
                    cboMachineType.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        //
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            MarkMachineDropStatus(true, false, NOTSET, NOTSET, 1, (sender as CheckBox).Content.ToString(), 0, NOTSET);
        }
        //
        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            MarkMachineDropStatus(false, false, NOTSET, NOTSET, 1, (sender as CheckBox).Content.ToString(), 0, NOTSET);
        }
        //
        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            App.clientObj.ClientReceived -= clientObj_AutoDropClientReceived;
        }
        //
        #endregion Event Methods
        //
        #region IDisposable Members
        //
        /// <summary>
        /// Variable used to identity whether this object is already disposed or not.
        /// </summary>


        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            _mEvent.Set();
            foreach (ManualResetEvent mResetevent in _mDropEvent)
            {
                mResetevent.Set();
            }
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
                        this.btnPerformDrop.Click -= (this.btnPerformDrop_Click);
                        this.cboMachineType.SelectionChanged -= (this.cboMachineType_SelectionChanged);
                        this.chkZone.Checked -= (this.chkZone_Checked);
                        this.chkZone.Unchecked -= (this.chkZone_Checked);

                    },
                    (c) =>
                    {
                    });
                    this.WriteLog("CPerformDrop objects are released successfully.");

                }
                disposed = true;
            }
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="CPerformDrop"/> is reclaimed by garbage collection.
        /// </summary>
        ~CPerformDrop()
        {
            Dispose(false);
        }

        #endregion

        private void btnPrintDrop_Click(object sender, RoutedEventArgs e)
        {
            try
            {


                _batchnumberfordropPrint = ((UndeclaredCollection)cboDropBatch.SelectedItem).Collection_Batch_No;

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                MessageBox.ShowBox("MessageID521", BMC_Icon.Error);

            }

            var list = _collectionHelper.GetUndeclaredCollectionByBatchNo(_batchnumberfordropPrint);
            if (list == null)
            {
                return;
            }
            PrintonDrop((IList<UndeclaredCollectionRecord>)list, SecurityHelper.CurrentUser.UserName);

        }
        private void PrintonDrop(IList<UndeclaredCollectionRecord> collectionRecords, string userName)
        {
            // DataSet DeclarationPrint = CollectionExtensions.ToDataSet<UndeclaredCollectionRecord>(((IEnumerable<UndeclaredCollectionRecord>)Source), "DeclarationPrint");

            int batchNo = 0;
            int nMachineCount = 0;
            string sDropType = string.Empty;
            var lineHeader = new DataTable();
            var lineItem = new DataTable("DeclarationPrint");
            var isFirstRecord = true;
            ILiquidationDetails details = LiquidationBusinessObject.CreateInstance();
            bool bSGVIEnable = false;
            string sAutoTicketDeclare = string.Empty;

            lineHeader.Columns.Add("Date");
            lineHeader.Columns.Add("DropNo");
            lineHeader.Columns.Add("NoOfMachine");
            lineHeader.Columns.Add("user");

            lineHeader.Columns.Add("Bills");
            lineHeader.Columns.Add("CoinsIn");
            lineHeader.Columns.Add("TicketsIn");
            lineHeader.Columns.Add("EFTIn");
            lineHeader.Columns.Add("TotalCashIn");

            lineHeader.Columns.Add("TicketsOut");
            lineHeader.Columns.Add("EFTOut");
            lineHeader.Columns.Add("CancelledCredits");
            lineHeader.Columns.Add("Jackpots");
            lineHeader.Columns.Add("CoinsOut");
            lineHeader.Columns.Add("TotalCashOut");


            lineHeader.Columns.Add("Asset");
            lineHeader.Columns.Add("Pos");
            //lineHeader.Columns.Add("NetWin");

            lineItem.Columns.Add("Asset", typeof(string));
            lineItem.Columns.Add("Pos", typeof(string));

            lineItem.Columns.Add("Bills", typeof(decimal));
            lineItem.Columns.Add("CoinsIn", typeof(decimal));
            lineItem.Columns.Add("TicketsIn", typeof(decimal));
            lineItem.Columns.Add("EFTIn", typeof(decimal));
            lineItem.Columns.Add("TotalCashIn", typeof(decimal));


            lineItem.Columns.Add("TicketsOut");
            //lineItem.Columns.Add("EFTOut");
            lineItem.Columns.Add("CancelledCredits", typeof(decimal));
            //lineItem.Columns.Add("Jackpots");
            //lineItem.Columns.Add("CoinsOut"); 
            //lineItem.Columns.Add("TotalCashOut");

            //lineItem.Columns.Add("NetWin");

            try
            {
                batchNo = _batchnumberfordropPrint;
                nMachineCount = collectionRecords.Count - 1;
                bSGVIEnable = (Convert.ToBoolean(details.GetSetting("SGVI_Enabled")) && (details.GetSetting("Client") == "SGVI"));
                sAutoTicketDeclare = Convert.ToString(details.GetSetting("TicketDeclarationMethod"));

                foreach (var collectionRecord in collectionRecords)
                {
                    //var dr = isFirstRecord ? lineHeader.NewRow() : lineItem.NewRow();
                    //if (isFirstRecord) { isFirstRecord = false; 

                    //                     continue;
                    //}
                    var dr = lineItem.NewRow();
                    sDropType = collectionRecord.Type == "defloat" ? "Final" : "Standard";
                    dr["Bills"] = collectionRecord.TotalBillValue;
                    dr["CoinsIn"] = collectionRecord.TotalCoinsValue;
                    dr["TicketsIn"] = collectionRecord.TicketsInValue;
                    dr["EFTIn"] = collectionRecord.EFTInValue;
                    dr["TotalCashIn"] = (collectionRecord.TotalBillValue + collectionRecord.TotalCoinsValue + collectionRecord.TicketsInValue);
                    //dr["TotalCashIn"] = (collectionRecord.TotalBillValue + collectionRecord.TotalCoinsValue + collectionRecord.TicketsInValue)+ collectionRecord.EFTInValue);


                    if (bSGVIEnable)
                    {
                        dr["TicketsOut"] = collectionRecord.TicketsOutValue;// + collectionRecord.ShortPayValue);
                        dr["CancelledCredits"] = collectionRecord.AttendantPayValue;
                    }
                    else
                    {
                        if (sAutoTicketDeclare.ToUpper() == "AUTO")
                            dr["TicketsOut"] = collectionRecord.TicketsOutValue;
                        else
                            dr["TicketsOut"] = (collectionRecord.TicketsOutValue + collectionRecord.ShortPayValue);
                        dr["CancelledCredits"] = collectionRecord.AttendantPayValue + collectionRecord.JackpotValue;
                    }
                    //dr["Jackpots"] = collectionRecord.Jackpot;
                    //dr["CoinsOut"] = ((decimal)collectionRecord.CoinOutValue).GetUniversalCurrencyFormatWithSymbol(); 
                    //dr["EFTOut"] = ((decimal)0).GetUniversalCurrencyFormatWithSymbol();
                    //dr["TotalCashOut"] = (collectionRecord.TicketsOutValue + collectionRecord.ShortPayValue + collectionRecord.HandpayValue + collectionRecord.JackpotValue + collectionRecord.CoinOutValue).GetUniversalCurrencyFormatWithSymbol();

                    //dr["NetWin"] = ((collectionRecord.TotalBillValue + collectionRecord.TotalCoinsValue + collectionRecord.TicketsInValue + collectionRecord.EFTInValue)
                    //    - (collectionRecord.TicketsOutValue + collectionRecord.HandpayValue + collectionRecord.JackpotValue + collectionRecord.ShortPayValue + collectionRecord.CoinOutValue)).GetUniversalCurrencyFormatWithSymbol();

                    //if (isFirstRecord)
                    //{
                    //    dr["Date"] = DateTime.Now.GetUniversalDateFormat();

                    //    dr["DropNo"] = collectionRecords[1].CollectionBatchNo == 0
                    //                       ? (object)"Interm Collection"
                    //                       : collectionRecords[1].CollectionBatchNo;

                    //    dr["NoOfMachine"] = collectionRecords.Count - 1;
                    //    dr["user"] = userName;
                    //    lineHeader.Rows.Add(dr);
                    //}
                    //else
                    //{
                    dr["Asset"] = collectionRecord.AssetNo;
                    dr["Pos"] = collectionRecord.Position;
                    lineItem.Rows.Add(dr);
                    // }
                    isFirstRecord = false;

                }

                DataSet DSDeclaration = new DataSet("DeclarationPrint");
                DSDeclaration.Tables.Add(lineItem);

                IReports objReports = ReportsBusinessObject.CreateInstance();

                if (DSDeclaration.Tables[0].Rows.Count == 0)
                {
                    MessageBox.ShowBox("MessageID261", BMC_Icon.Information);
                    return;
                }

                using (CReportViewer cReportViewer = new CReportViewer())
                {
                    cReportViewer.DropSummaryReport(DSDeclaration, userName, batchNo, nMachineCount, sDropType);
                    cReportViewer.SetOwner(Window.GetWindow(this));
                    cReportViewer.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                MessageBox.ShowBox("MessageID262", BMC_Icon.Error);
            }

        }

        private void rdoInterimCount_Checked_1(object sender, RoutedEventArgs e)
        {
            DisableInterimDropOnAutoDrop(false);
        }

        private void rdoInterimCount_Unchecked_1(object sender, RoutedEventArgs e)
        {
            DisableInterimDropOnAutoDrop(true);
        }


    }
    //
    #endregion Perform Drop Screen
    //
    #region Zone data collection
    public class ZoneCollection : INotifyPropertyChanged
    {
        private string _zoneName;
        private bool _isSelected;
        private ObservableCollection<CollectionMachine> _machines;
        public event PropertyChangedEventHandler PropertyChanged;

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("IsSelected"));
                }
            }
        }
        public string ZoneName
        {
            get { return _zoneName; }
            set
            {
                _zoneName = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("ZoneName"));
                }
            }
        }
        public ObservableCollection<CollectionMachine> Machines
        {
            get { return _machines; }
            set
            {
                _machines = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Machines"));
                }
            }
        }
    }
    #endregion Zone data collection
    //
    #region Collection Parameters
    public class CollectionParams
    {
        public int _UniqueId { get; set; }
        public int _BatchID { get; set; }
        public int _MaxThreads { get; set; }
        public CollectionType _collectiontype { get; set; }
    }
    #endregion Collection Parameters
}