using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using System.Text;
using BMC.CashDeskOperator.BusinessObjects;
using BMC.Common.ExceptionManagement;
using BMC.Common.LogManagement;
using BMC.Common.ConfigurationManagement;
using BMC.Presentation.POS.Helper_classes;

namespace BMC.Presentation
{
    /// <summary>
    /// Interaction logic for SiteCheckPoints.xaml
    /// </summary>  
    /// 
    public partial class SiteCheckPoints : IDisposable
    {
        #region Declarations

        public event EventHandler MoveKeyboard;        

        #region Delegate declarations
        private delegate bool DelImportProcess();
        private DelImportProcess _checkImportDone;
        #endregion

        private readonly string _sType = "";
        private readonly int _iSiteCode;
        private int   _index;
        private static SiteCheckPoints _instance;
        private readonly SiteSetupConfiguration _oSiteSetupConfiguration = SiteSetupConfiguration.SiteSetupConfigurationInstance;

        readonly ObservableCollection<LoadListView> _loadListViewCollection = new ObservableCollection<LoadListView>();

        private static bool _rImport;
        private static bool _bIsRecovery;

        private readonly Dictionary<string, int> _subProcessStatus = new Dictionary<string, int>();
        private BackgroundWorker _workerTotalProcess;
        private BackgroundWorker _worker2;
        private string[] strListarray = null;
        private string strServiceStatus = string.Empty;
        #region Constants

        private const string Site = "Site";
        private const string Zones = "Zones";
        private const string SiteAlliance = "SiteAlliance";
        private const string BarPositions = "Positions";
        private const string Machines = "Machines";
        private const string Installations = "Installations";
        private const string MeterHistory = "Meter History";
        private const string Daily = "Daily";
        private const string Hourly = "Hourly";
        private const string Collections = "Collections";
        private const string Tickets = "Tickets";
        private const string CashDeskTransactions = "Cash Desk Transactions";
        private const string Events = "Events";
        private const string Settings = "Site Settings";
        private const string Users = "Users";
        private const string UserRoles = "User Roles";
        private const string Calendars = "Calendars";
        private const string Recovery = "recovery";

        #endregion Constants

        public enum ListOrder
        {
            Site = 0,
            Zones = 1,
            BarPositions = 2,
            Machines = 3,
            Installations = 4,
            MeterHistory = 5,
            Daily = 6,
            Hourly = 7,
            Collections = 8,
            Tickets = 9,
            CashDeskTransactions = 10,
            Events = 11,
            Settings = 12,
            Users = 13,
            UserRoles = 14,
            Calendars = 15,
            SiteAlliance = 16
        }

        public enum ListOrderNewSite
        {
            Site = 0,
            Zones = 1,
            BarPositions = 2,
            Machines = 3,
            Installations = 4,           
            Settings = 5,
            Users = 6,
            UserRoles = 7,
            Calendars = 8,
            SiteAlliance = 9
        }

        public enum ImportStatus
        {
            Failed = -1,
            UpdateFailed = -2,
            Completed = 1,
            EnterpriseNoData = 2
        }

        #endregion Declarations

        #region Properties

        private ObservableCollection<LoadListView> LoadListViewCollection
        { get { return _loadListViewCollection; } }
        //public ThreadSafeObservableCollection<LoadListView> LoadListViewCollection
        //{ get { return _LoadThreadSafe; } }        

        #endregion Properties

        #region Constructor

        public SiteCheckPoints()
        {            
                InitializeComponent();
                MessageBox.parentOwner = this;
        }

        public SiteCheckPoints(string sType, int iSiteCode)
        {
            InitializeComponent();
            _sType = sType;
            _iSiteCode = iSiteCode;
            MessageBox.parentOwner = this;
        }

        public static SiteCheckPoints SiteCheckPointsInstance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SiteCheckPoints();
                }
                return _instance;
            }
        }

        #endregion

        #region Static methods

        //static void ImportBatchComplete(IAsyncResult ar)
        //{

        //    System.Runtime.Remoting.Messaging.AsyncResult iar = (System.Runtime.Remoting.Messaging.AsyncResult)ar;
        //    delReseedCollectionBatch state = (delReseedCollectionBatch)iar.AsyncDelegate;
        //    rBatchImport = state.EndInvoke(ar);
        //    // return bResult;
        //}
        static void ImportComplete(IAsyncResult ar)
        {

            System.Runtime.Remoting.Messaging.AsyncResult iar = (System.Runtime.Remoting.Messaging.AsyncResult)ar;
            DelImportProcess state = (DelImportProcess)iar.AsyncDelegate;
            _rImport = state.EndInvoke(ar);
            // return bResult;
        }

        #endregion

        #region Member functions

        private void LoadData()
        {
            DataTable dtTemp = new DataTable();
           List<string> itemToremove = new List<string>();

           try
           {
               dtTemp = _oSiteSetupConfiguration.GetTableDetails();

                if (_bIsRecovery)
                {
                    _subProcessStatus.Add(Site, 0);
                    _subProcessStatus.Add(Zones, 0);
                    _subProcessStatus.Add(BarPositions, 0);
                    _subProcessStatus.Add(Machines, 0);
                    _subProcessStatus.Add(Installations, 0);
                    _subProcessStatus.Add(MeterHistory, 0);
                    _subProcessStatus.Add(Daily, 0);
                    _subProcessStatus.Add(Hourly, 0);
                    _subProcessStatus.Add(Collections, 0);
                    _subProcessStatus.Add(Tickets, 0);
                    _subProcessStatus.Add(CashDeskTransactions, 0);
                    _subProcessStatus.Add(Events, 0);
                    _subProcessStatus.Add(Settings, 0);
                    _subProcessStatus.Add(Users, 0);
                    _subProcessStatus.Add(UserRoles, 0);
                    _subProcessStatus.Add(Calendars, 0);
                    _subProcessStatus.Add(SiteAlliance, 0);
                }
                else
                {
                    _subProcessStatus.Add(Site, 0);
                    _subProcessStatus.Add(Zones, 0);
                    _subProcessStatus.Add(BarPositions, 0);
                    _subProcessStatus.Add(Machines, 0);
                    _subProcessStatus.Add(Installations, 0);
                    _subProcessStatus.Add(Settings, 0);
                    _subProcessStatus.Add(Users, 0);
                    _subProcessStatus.Add(UserRoles, 0);
                    _subProcessStatus.Add(Calendars, 0);
                    _subProcessStatus.Add(SiteAlliance, 0);
                }

               if (dtTemp != null)
               {
                   if (_bIsRecovery)
                   {
                       foreach (DataRow dr in dtTemp.Rows)
                       {
                           _loadListViewCollection.Add(new LoadListView { Data = dr[0].ToString(), StatusDescription = (dr[1].ToString().ToUpper() == "FALSE") ? "Not Started" : "Completed", Progress = (dr[1].ToString().ToUpper() == "FALSE") ? 0 : 100, Status = dr[1].ToString() });
                       }
                   }
                   else
                   {
                       foreach (DataRow dr in dtTemp.Rows)
                       {
                           if (dr["TableNames"].ToString() == Site ||
                              dr["TableNames"].ToString() == Zones ||
                              dr["TableNames"].ToString() == BarPositions ||
                              dr["TableNames"].ToString() == Machines ||
                              dr["TableNames"].ToString() == Installations ||
                              dr["TableNames"].ToString() == Settings ||
                              dr["TableNames"].ToString() == Users||
                                dr["TableNames"].ToString() == UserRoles||
                               dr["TableNames"].ToString() == Calendars ||
                               dr["TableNames"].ToString() == SiteAlliance)
                           {
                               _loadListViewCollection.Add(new LoadListView { Data = dr[0].ToString(), StatusDescription = (dr[1].ToString().ToUpper() == "FALSE") ? "Not Started" : "Completed", Progress = (dr[1].ToString().ToUpper() == "FALSE") ? 0 : 100, Status = dr[1].ToString() });
                           }
                       }
                   }
               }               
           }
           catch (Exception ex)
           {
               ExceptionManager.Publish(ex);
           }
            finally
            {
                if (dtTemp != null) { dtTemp.Dispose(); }
            }
        }

        private void StartProcess()
        {
            _workerTotalProcess = new BackgroundWorker
                                      {
                                          WorkerReportsProgress = true,
                                          WorkerSupportsCancellation = true
                                      };
            _workerTotalProcess.DoWork += _workerTotalProcess_DoWork;
            _workerTotalProcess.ProgressChanged += _workerTotalProcess_ProgressChanged;
            _workerTotalProcess.RunWorkerCompleted += _workerTotalProcess_RunWorkerCompleted;
            _workerTotalProcess.RunWorkerAsync();
        }

        private bool ImportProcess()
        {
            return _bIsRecovery ? ProcessRecovery() : ProcessNewSite();
        }

        private bool ProcessRecovery()
        {
            var bReturn = false;
            Dictionary<int, string> dictCheckPoints;

            try
            {
                dictCheckPoints = _oSiteSetupConfiguration.GetCheckPointsStatus(0);

                if (dictCheckPoints.Count > 0)
                {
                    dictCheckPoints = _oSiteSetupConfiguration.GetCheckPointsStatus(1);

                    if (dictCheckPoints != null)
                    {
                        foreach (var checkKey in dictCheckPoints)
                        {
                            switch (checkKey.Value)
                            {
                                case Site:
                                    {
                                        MaintainCheckPointsListView(checkKey.Value);
                                        bReturn = (ImportSiteDetails()) ? true : false;
                                        if (bReturn == false) { return false; }
                                        break;
                                    }

                                case Zones:
                                    {
                                        MaintainCheckPointsListView(checkKey.Value);
                                        bReturn = (ImportZones()) ? true : false;
                                        if (bReturn == false) { return false; }
                                        break;
                                    }

                                case BarPositions:
                                    {
                                        MaintainCheckPointsListView(checkKey.Value);
                                        bReturn = (ImportBarPositions()) ? true : false;
                                        if (bReturn == false) { return false; }
                                        break;
                                    }

                                case Machines:
                                    {
                                        MaintainCheckPointsListView(checkKey.Value);
                                        bReturn = (ImportMachines()) ? true : false;
                                        if (bReturn == false) { return false; }
                                        break;
                                    }

                                case Installations:
                                    {
                                        MaintainCheckPointsListView(checkKey.Value);
                                        bReturn = (ImportInstallations()) ? true : false;
                                        if (bReturn == false) { return false; }
                                        break;
                                    }

                                case MeterHistory:
                                    {
                                        MaintainCheckPointsListView(checkKey.Value);
                                        bReturn = (ImportMeterHistory()) ? true : false;
                                        if (bReturn == false) { return false; }
                                        break;
                                    }

                                case Daily:
                                    {
                                        MaintainCheckPointsListView(checkKey.Value);
                                        bReturn = (ImportDaily()) ? true : false;
                                        if (bReturn == false) { return false; }
                                        break;
                                    }

                                case Hourly:
                                    {
                                        MaintainCheckPointsListView(checkKey.Value);
                                        bReturn = (ImportHourly()) ? true : false;
                                        if (bReturn == false) { return false; }
                                        break;
                                    }

                                case Collections:
                                    {
                                        MaintainCheckPointsListView(checkKey.Value);
                                        bReturn = (ImportBatch()) ? true : false;
                                        if (bReturn == false) { return false; }
                                        break;
                                    }

                                case Tickets:
                                    {
                                        MaintainCheckPointsListView(checkKey.Value);
                                        bReturn = (ImportTickets()) ? true : false;
                                        if (bReturn == false) { return false; }
                                        break;
                                    }

                                case CashDeskTransactions:
                                    {
                                        MaintainCheckPointsListView(checkKey.Value);
                                        bReturn = (ImportCashDeskTransactions()) ? true : false;
                                        if (bReturn == false) { return false; }
                                        break;
                                    }
                                case Events:
                                    {
                                        MaintainCheckPointsListView(checkKey.Value);
                                        bReturn = (ImportAllEvents()) ? true : false;
                                        if (bReturn == false) { return false; }
                                        break;
                                    }

                                case Settings:
                                    {
                                        MaintainCheckPointsListView(checkKey.Value);
                                        bReturn = (ImportSiteSettings()) ? true : false;
                                        if (bReturn == false) { return false; }
                                        break;
                                    }
                                case Users:
                                    {
                                        MaintainCheckPointsListView(checkKey.Value);
                                        bReturn = (ImportUserDetails()) ? true : false;
                                        if (bReturn == false) { return false; }
                                        break;
                                    }
                                case UserRoles:
                                    {
                                        MaintainCheckPointsListView(checkKey.Value);
                                        bReturn = (ImportUserRoles()) ? true : false;
                                        if (bReturn == false) { return false; }
                                        break;
                                    }
                                case Calendars:
                                    {
                                        MaintainCheckPointsListView(checkKey.Value);
                                         bReturn = (ImportCalendars()) ? true : false;
                                        if (bReturn == false) { return false; }
                                        break;
                                    }
                                case SiteAlliance:
                                    {
                                        MaintainCheckPointsListView(checkKey.Value);
                                        bReturn = (ImportSiteAlliance()) ? true : false;
                                        if (bReturn == false) { return false; }
                                        break;
                                    }

                                default:
                                    {
                                        break;
                                    }
                            }
                        }
                    }
                }
                else
                {
                    bReturn = (ImportUserDetails()) ? true : false;
                    if (!bReturn)
                    {
                        RemoveItemSubProcessStatus(Users);
                        return bReturn;
                    }
                    bReturn = (ImportUserRoles()) ? true : false;
                    if (!bReturn)
                    {
                        RemoveItemSubProcessStatus(UserRoles);
                        return bReturn;
                    }

                    bReturn = (ImportSiteDetails()) ? true : false;
                    if (!bReturn)
                    {
                        RemoveItemSubProcessStatus(Site);
                        return bReturn;
                    }

                    bReturn = (ImportZones()) ? true : false;
                    if (!bReturn)
                    {
                        RemoveItemSubProcessStatus(Zones);
                        return bReturn;
                    }

                    bReturn = (ImportBarPositions()) ? true : false;
                    if (!bReturn)
                    {
                        RemoveItemSubProcessStatus(BarPositions);
                        return bReturn;
                    }

                    bReturn = (ImportMachines()) ? true : false;
                    if (!bReturn)
                    {
                        RemoveItemSubProcessStatus(Machines);
                        return bReturn;
                    }

                    bReturn = (ImportInstallations()) ? true : false;
                    if (!bReturn)
                    {
                        RemoveItemSubProcessStatus(Installations);
                        return bReturn;
                    }

                    bReturn = (ImportMeterHistory()) ? true : false;
                    if (bReturn == false)
                    {
                        RemoveItemSubProcessStatus(MeterHistory);
                        return bReturn;
                    }

                    bReturn = (ImportDaily()) ? true : false;
                    if (!bReturn)
                    {
                        RemoveItemSubProcessStatus(Daily);
                        return bReturn;
                    }

                    bReturn = (ImportHourly()) ? true : false;
                    if (!bReturn)
                    {
                        RemoveItemSubProcessStatus(Hourly);
                        return bReturn;
                    }

                    bReturn = (ImportBatch()) ? true : false;
                    if (!bReturn)
                    {
                        RemoveItemSubProcessStatus(Collections);
                        return bReturn;
                    }

                    bReturn = (ImportTickets()) ? true : false;
                    if (!bReturn)
                    {
                        RemoveItemSubProcessStatus(Tickets);
                        return bReturn;
                    }

                    bReturn = (ImportCashDeskTransactions()) ? true : false;
                    if (!bReturn)
                    {
                        RemoveItemSubProcessStatus(CashDeskTransactions);
                        return bReturn;
                    }

                    bReturn = (ImportAllEvents()) ? true : false;
                    if (!bReturn)
                    {
                        RemoveItemSubProcessStatus(Events);
                        return bReturn;
                    }

                    bReturn = (ImportSiteSettings()) ? true : false;
                    if (!bReturn)
                    {
                        RemoveItemSubProcessStatus(Settings);
                        return bReturn;
                    }
                    
                    bReturn = (ImportCalendars()) ? true : false;
                    if (!bReturn)
                    {
                        RemoveItemSubProcessStatus(Calendars);
                        return bReturn;
                    }

                    bReturn = (ImportSiteAlliance()) ? true : false;
                    if (!bReturn)
                    {
                        RemoveItemSubProcessStatus(SiteAlliance);
                        return bReturn;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            _rImport = bReturn;
            return bReturn;
        }

        private bool ProcessNewSite()
        {
            bool bReturn = false;
            Dictionary<int, string> dictCheckPoints = new Dictionary<int, string>();

            try
            {
                dictCheckPoints = _oSiteSetupConfiguration.GetCheckPointsStatus(0);

                if (dictCheckPoints.Count > 0)
                {
                    dictCheckPoints = _oSiteSetupConfiguration.GetCheckPointsStatus(1);

                    if (dictCheckPoints != null)
                    {
                        foreach (KeyValuePair<int, string> CheckKey in dictCheckPoints)
                        {
                            switch (CheckKey.Value.ToString())
                            {
                                case Site:
                                    {
                                        MaintainCheckPointsListView(CheckKey.Value.ToString());
                                        bReturn = (ImportSiteDetails()) ? true : false;
                                        if (bReturn == false) { return bReturn; }
                                        break;
                                    }

                                case Zones:
                                    {
                                        MaintainCheckPointsListView(CheckKey.Value.ToString());
                                        bReturn = (ImportZones()) ? true : false; ;
                                        if (bReturn == false) { return bReturn; }
                                        break;
                                    }

                                case BarPositions:
                                    {
                                        MaintainCheckPointsListView(CheckKey.Value.ToString());
                                        bReturn = (ImportBarPositions()) ? true : false;
                                        if (bReturn == false) { return bReturn; }
                                        break;
                                    }

                                case Machines:
                                    {
                                        MaintainCheckPointsListView(CheckKey.Value.ToString());
                                        bReturn = (ImportMachines()) ? true : false;
                                        if (bReturn == false) { return bReturn; }
                                        break;
                                    }

                                case Installations:
                                    {
                                        MaintainCheckPointsListView(CheckKey.Value.ToString());
                                        bReturn = (ImportInstallations()) ? true : false;
                                        if (bReturn == false) { return bReturn; }
                                        break;
                                    }                              

                                case Settings:
                                    {
                                        MaintainCheckPointsListView(CheckKey.Value.ToString());
                                        bReturn = (ImportSiteSettings()) ? true : false;
                                        if (bReturn == false) { return bReturn; }
                                        break;
                                    }
                                case Users:
                                    {
                                        MaintainCheckPointsListView(CheckKey.Value.ToString());
                                        bReturn = (ImportUserDetails()) ? true : false;
                                        if (bReturn == false) { return bReturn; }
                                        break;
                                    }
                                case UserRoles:
                                    {
                                        MaintainCheckPointsListView(CheckKey.Value.ToString());
                                        bReturn = (ImportUserRoles()) ? true : false;
                                        if (bReturn == false) { return bReturn; }
                                        break;
                                    }
                                case Calendars:
                                    {
                                        MaintainCheckPointsListView(CheckKey.Value.ToString());
                                        bReturn = (ImportCalendars()) ? true : false;
                                        if (bReturn == false) { return bReturn; }
                                        break;
                                    }
                                case SiteAlliance:
                                    {
                                        MaintainCheckPointsListView(CheckKey.Value.ToString());
                                        bReturn = (ImportSiteAlliance()) ? true : false; ;
                                        if (bReturn == false) { return bReturn; }
                                        break;
                                    }
                                default:
                                    {
                                        break;
                                    }
                            }
                        }
                    }
                }
                else
                {
                    bReturn = (ImportSiteDetails()) ? true : false;
                    if (!bReturn)
                    {
                        RemoveItemSubProcessStatus(Site);
                        return bReturn;
                    }

                    bReturn = (ImportZones()) ? true : false;
                    if (!bReturn)
                    {
                        RemoveItemSubProcessStatus(Zones);
                        return bReturn;
                    }

                    bReturn = (ImportBarPositions()) ? true : false;
                    if (!bReturn)
                    {
                        RemoveItemSubProcessStatus(BarPositions);
                        return bReturn;
                    }

                    bReturn = (ImportMachines()) ? true : false;
                    if (!bReturn)
                    {
                        RemoveItemSubProcessStatus(Machines);
                        return bReturn;
                    }

                    bReturn = (ImportInstallations()) ? true : false;
                    if (!bReturn)
                    {
                        RemoveItemSubProcessStatus(Installations);
                        return bReturn;
                    }  

                    bReturn = (ImportSiteSettings()) ? true : false;
                    if (!bReturn)
                    {
                        RemoveItemSubProcessStatus(Settings);
                        return bReturn;
                    }
                    bReturn = (ImportUserDetails()) ? true : false;
                    if (!bReturn)
                    {
                        RemoveItemSubProcessStatus(Users);
                        return bReturn;
                    }
                    bReturn = (ImportUserRoles()) ? true : false;
                    if (!bReturn)
                    {
                        RemoveItemSubProcessStatus(UserRoles);
                        return bReturn;
                    }
                    bReturn = (ImportCalendars()) ? true : false;
                    if (!bReturn)
                    {
                        RemoveItemSubProcessStatus(Calendars);
                        return bReturn;
                    }
                    bReturn = (ImportSiteAlliance()) ? true : false;

                    if (!bReturn)
                    {
                        RemoveItemSubProcessStatus(SiteAlliance);
                        return bReturn;
                    }


                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            _rImport = bReturn;
            return bReturn;
        }

        private bool ImportSiteDetails()
        {
            bool lImportSiteDetails = false;
            bool bUpdateCheckpoints = false;
            int iSiteStatus = 0;

            try
            {
                CallDispatcherListView(Convert.ToInt32(ListOrder.Site), 10, false, "In Progress");
                lImportSiteDetails = (_oSiteSetupConfiguration.ImportSiteDetails(_iSiteCode) < 0) ? false : true;
                
                if (lImportSiteDetails)
                {
                    CallDispatcherListView(Convert.ToInt32(ListOrder.Site), 50, false, "In Progress");
                    iSiteStatus = _oSiteSetupConfiguration.UpdateSiteStatus(_iSiteCode, "PARTIALLYCONFIGURED");

                    if (SiteStatusMessage(iSiteStatus, "PartiallyConfigured"))
                    {
                        bUpdateCheckpoints = _oSiteSetupConfiguration.UpdateCheckPoints(_iSiteCode, 1, Site);
                        if (bUpdateCheckpoints)
                        {
                            _subProcessStatus.Remove(Site);
                            _subProcessStatus.Add(Site, 3);
                            CallDispatcherListView(Convert.ToInt32(ListOrder.Site), 100, true, "Completed");
                            _worker2.ReportProgress(_index + 7);
                        }
                        else
                        { CallDispatcherListView(Convert.ToInt32(ListOrder.Site), 0, false, "Failed"); }
                    }
                    else
                    { CallDispatcherListView(Convert.ToInt32(ListOrder.Site), 0, false, "Failed"); }
                }
                else
                { bUpdateCheckpoints = false; }

                LogManager.WriteLog("Update check points status for table sites" + bUpdateCheckpoints.ToString(), LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return bUpdateCheckpoints;
        }

        private bool ImportSiteAlliance()
        {
            bool lSiteAlliance = false;
            bool bUpdateCheckpoints = false;

            try
            {
                LogManager.WriteLog("Inside ImportSiteAlliance Start", LogManager.enumLogLevel.Info);
                CallDispatcherListView(Convert.ToInt32(ListOrder.SiteAlliance), 10, false, "In Progress");
                LogManager.WriteLog("AFTER CallDispatcherListView", LogManager.enumLogLevel.Info);
                lSiteAlliance = (_oSiteSetupConfiguration.ImportSiteAlliance(_iSiteCode) < 0) ? false : true;

                LogManager.WriteLog("_oSiteSetupConfiguration.ImportSiteAlliance CALL | lSiteAlliance=" + lSiteAlliance.ToString(), LogManager.enumLogLevel.Info);

                if (lSiteAlliance == true)
                {
                    bUpdateCheckpoints = _oSiteSetupConfiguration.UpdateCheckPoints(_iSiteCode, 1, SiteAlliance);
                    LogManager.WriteLog("bUpdateCheckpoints=" + lSiteAlliance.ToString(), LogManager.enumLogLevel.Info);

                    if (bUpdateCheckpoints == true)
                    {
                        CallDispatcherListView(Convert.ToInt32(ListOrderNewSite.SiteAlliance), 100, true, "Completed");
                        _subProcessStatus.Remove(SiteAlliance);
                        _subProcessStatus.Add(SiteAlliance, 3);
                        _worker2.ReportProgress(_index + 7);
                    }
                    else { CallDispatcherListView(Convert.ToInt32(ListOrderNewSite.SiteAlliance), 0, false, "Failed"); }
                }
                else
                { bUpdateCheckpoints = false; }

                LogManager.WriteLog("Update check points status for table SiteAlliance  " + bUpdateCheckpoints.ToString(), LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            return bUpdateCheckpoints;
        }
        
        private bool ImportZones()
        {
            bool lZones = false;
            bool bUpdateCheckpoints = false;

            try
            {
                CallDispatcherListView(Convert.ToInt32(ListOrder.Zones), 20, false, "In Progress");
                lZones = (_oSiteSetupConfiguration.ImportZones(_iSiteCode) < 0) ? false : true;

                if (lZones == true)
                {
                    bUpdateCheckpoints = _oSiteSetupConfiguration.UpdateCheckPoints(_iSiteCode, 1, Zones);
                    if (bUpdateCheckpoints == true)
                    {
                        CallDispatcherListView(Convert.ToInt32(ListOrder.Zones), 100, true, "Completed");
                        _subProcessStatus.Remove(Zones);
                        _subProcessStatus.Add(Zones, 3);
                        _worker2.ReportProgress(_index + 7);
                    }
                    else { CallDispatcherListView(Convert.ToInt32(ListOrder.Zones), 0, false, "Failed"); }
                }
                else
                { bUpdateCheckpoints = false; }

                LogManager.WriteLog("Update check points status for table zones  " + bUpdateCheckpoints.ToString(), LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            return bUpdateCheckpoints;
        }

        private bool ImportBarPositions()
        {
            bool lBarPositions = false;
            bool bUpdateCheckpoints = false;

            try
            {
                CallDispatcherListView(Convert.ToInt32(ListOrder.BarPositions), 10, false, "In Progress");
                lBarPositions = (_oSiteSetupConfiguration.ImportBarPositions(_iSiteCode) < 0) ? false : true;

                if (lBarPositions)
                {
                    bUpdateCheckpoints = _oSiteSetupConfiguration.UpdateCheckPoints(_iSiteCode, 1, BarPositions);

                    if (bUpdateCheckpoints)
                    {
                        CallDispatcherListView(Convert.ToInt32(ListOrder.BarPositions), 100, true, "Completed");
                        _subProcessStatus.Remove(BarPositions);
                        _subProcessStatus.Add(BarPositions, 3);
                        _worker2.ReportProgress(_index + 7);
                    }
                    else
                    {
                        CallDispatcherListView(Convert.ToInt32(ListOrder.BarPositions), 0, false, "Failed");
                    }
                }
                else
                { bUpdateCheckpoints = false; }

                LogManager.WriteLog("Update check points status for table BarPositions" + bUpdateCheckpoints.ToString(), LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            { ExceptionManager.Publish(ex); }

            return bUpdateCheckpoints;
        }

        private bool ImportMachines()
        {
            bool lMachines = false;
            bool bUpdateCheckpoints = false;

            try
            {
                CallDispatcherListView(Convert.ToInt32(ListOrder.Machines), 10, false, "In Progress");
                lMachines = (_oSiteSetupConfiguration.ImportMachines(_iSiteCode) == false) ? false : true;

                if (lMachines)
                {
                    bUpdateCheckpoints = _oSiteSetupConfiguration.UpdateCheckPoints(_iSiteCode, 1, Machines);
                    if (bUpdateCheckpoints == true)
                    {
                        CallDispatcherListView(Convert.ToInt32(ListOrder.Machines), 100, true, "Completed");
                        _subProcessStatus.Remove(Machines);
                        _subProcessStatus.Add(Machines, 3);
                        _worker2.ReportProgress(_index + 7);
                    }
                    else
                    {
                        CallDispatcherListView(Convert.ToInt32(ListOrder.Machines), 0, false, "Failed");
                    }
                }
                else
                {
                    CallDispatcherListView(Convert.ToInt32(ListOrder.Machines), 0, false, "Failed");
                    bUpdateCheckpoints = false;
                }

                LogManager.WriteLog("Update check points status for table Machines" + bUpdateCheckpoints.ToString(), LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            { ExceptionManager.Publish(ex); }

            return bUpdateCheckpoints;
        }

        private bool ImportInstallations()
        {
            bool lInstallations = false;
            bool bUpdateCheckpoints = false;

            try
            {
                CallDispatcherListView(Convert.ToInt32(ListOrder.Installations), 10, false, "In Progress");
                lInstallations = (_oSiteSetupConfiguration.ImportInstallations(_iSiteCode) < 0) ? false : true;

                if (lInstallations)
                {
                    bUpdateCheckpoints = _oSiteSetupConfiguration.UpdateCheckPoints(_iSiteCode, 1, Installations);
                    if (bUpdateCheckpoints == true)
                    {
                        CallDispatcherListView(Convert.ToInt32(ListOrder.Installations), 100, true, "Completed");
                        _subProcessStatus.Remove(Installations);
                        _subProcessStatus.Add(Installations, 3);
                        _worker2.ReportProgress(_index + 7);
                    }
                    else
                    {
                        CallDispatcherListView(Convert.ToInt32(ListOrder.Installations), 0, false, "Failed");
                    }
                }
                else
                { bUpdateCheckpoints = false; }

                LogManager.WriteLog("Update check points status for table Installations" + bUpdateCheckpoints.ToString(), LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            { ExceptionManager.Publish(ex); }

            return bUpdateCheckpoints;
        }

        private bool ImportBatch()
        {
            bool bUpdateCheckpoints = false;
            bool lImportBatch = false;

            try
            {
                CallDispatcherListView(Convert.ToInt32(ListOrder.Collections), 10, false, "In Progress");
                lImportBatch = (_oSiteSetupConfiguration.ImportCollections(_iSiteCode) < 0) ? false : true;

                if (lImportBatch)
                {
                    bUpdateCheckpoints = _oSiteSetupConfiguration.UpdateCheckPoints(_iSiteCode, 1, Collections);
                    if (bUpdateCheckpoints == true)
                    {
                        CallDispatcherListView(Convert.ToInt32(ListOrder.Collections), 100, true, "Completed");
                        _subProcessStatus.Remove(Collections);
                        _subProcessStatus.Add(Collections, 3);
                        _worker2.ReportProgress(_index + 7);
                    }
                    else
                    {
                        CallDispatcherListView(Convert.ToInt32(ListOrder.Collections), 0, false, "Failed");
                    }
                }
                else
                { bUpdateCheckpoints = false; }

                LogManager.WriteLog("Update check points status for table Batch" + bUpdateCheckpoints.ToString(), LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            { ExceptionManager.Publish(ex); }

            return bUpdateCheckpoints;
        }

        private bool ImportMeterHistory()
        {
            bool lMeterHistory = false;
            bool bUpdateCheckpoints = false;

            try
            {
                CallDispatcherListView(Convert.ToInt32(ListOrder.MeterHistory), 10, false, "In Progress");
                lMeterHistory = (_oSiteSetupConfiguration.ImportLatestMeterHistory(_iSiteCode) < 0) ? false : true;

                if (lMeterHistory)
                {

                    bUpdateCheckpoints = _oSiteSetupConfiguration.UpdateCheckPoints(_iSiteCode, 1, MeterHistory);
                    if (bUpdateCheckpoints == true)
                    {
                        CallDispatcherListView(Convert.ToInt32(ListOrder.MeterHistory), 100, true, "Completed");
                        _subProcessStatus.Remove(MeterHistory);
                        _subProcessStatus.Add(MeterHistory, 3);
                        _worker2.ReportProgress(_index + 7);
                    }
                    else
                    { CallDispatcherListView(Convert.ToInt32(ListOrder.MeterHistory), 0, false, "Failed"); }
                }
                else
                { bUpdateCheckpoints = false; }
                LogManager.WriteLog("Update check points status for table MeterHistory" + bUpdateCheckpoints.ToString(), LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            { ExceptionManager.Publish(ex); }
            return bUpdateCheckpoints;
        }

        private bool ImportTickets()
        {
            bool bUpdateCheckpoints = false;
            bool bImportTicket = false;

            try
            {
                CallDispatcherListView(Convert.ToInt32(ListOrder.Tickets), 10, false, "In Progress");
                bImportTicket = _oSiteSetupConfiguration.ImportTickets(_iSiteCode) == false ? false : true;
                if (bImportTicket == true)
                {
                    bUpdateCheckpoints = _oSiteSetupConfiguration.UpdateCheckPoints(_iSiteCode, 1, Tickets);
                    if (!bUpdateCheckpoints)
                    {
                        CallDispatcherListView(Convert.ToInt32(ListOrder.Tickets), 0, false, "Failed");
                    }
                    else
                    {
                        CallDispatcherListView(Convert.ToInt32(ListOrder.Tickets), 100, true, "Completed");
                        _subProcessStatus.Remove(Tickets);
                        _subProcessStatus.Add(Tickets, 3);
                        _worker2.ReportProgress(_index + 7);
                    }
                }
                else
                {
                    bUpdateCheckpoints = false;
                    CallDispatcherListView(Convert.ToInt32(ListOrder.Tickets), 0, false, "Failed");
                }

                LogManager.WriteLog("Update check points status for table Tickets" + bUpdateCheckpoints.ToString(), LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                bUpdateCheckpoints = false;
                ExceptionManager.Publish(ex);
            }

            return bUpdateCheckpoints;
        }

        private bool ImportDaily()
        {
            bool bUpdateCheckpoints = false;
            bool bImportDaily = false;

            try
            {
                CallDispatcherListView(Convert.ToInt32(ListOrder.Daily), 10, false, "In Progress");

                bImportDaily = (_oSiteSetupConfiguration.ImportDailyRead(_iSiteCode) < 0) ? false : true;
                if (bImportDaily == true)
                {
                    bUpdateCheckpoints = _oSiteSetupConfiguration.UpdateCheckPoints(_iSiteCode, 1, Daily);
                    if (bUpdateCheckpoints == false)
                    { CallDispatcherListView(Convert.ToInt32(ListOrder.Daily), 0, false, "Failed"); }
                    else
                    {
                        CallDispatcherListView(Convert.ToInt32(ListOrder.Daily), 100, true, "Completed");
                        _subProcessStatus.Remove(Daily);
                        _subProcessStatus.Add(Daily, 3);
                        _worker2.ReportProgress(_index + 7);
                    }
                }
                else
                {
                    bUpdateCheckpoints = false;
                    CallDispatcherListView(Convert.ToInt32(ListOrder.Daily), 0, false, "Failed");
                }

                LogManager.WriteLog("Update check points status for table Daily: " + bUpdateCheckpoints.ToString(), LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            { ExceptionManager.Publish(ex); }
            return bUpdateCheckpoints;
        }

        private bool ImportHourly()
        {
            bool bUpdateCheckpoints = false;
            bool bImportHourly = false;
            try
            {
                CallDispatcherListView(Convert.ToInt32(ListOrder.Hourly), 10, false, "In Progress");

                bImportHourly = (_oSiteSetupConfiguration.ImportHourly(_iSiteCode) < 0) ? false : true;
                if (bImportHourly == true)
                {
                    bUpdateCheckpoints = _oSiteSetupConfiguration.UpdateCheckPoints(_iSiteCode, 1, Hourly);
                    if (bUpdateCheckpoints == false)
                    { CallDispatcherListView(Convert.ToInt32(ListOrder.Hourly), 0, false, "Failed"); }
                    else
                    {
                        CallDispatcherListView(Convert.ToInt32(ListOrder.Hourly), 100, true, "Completed");
                        _subProcessStatus.Remove(Hourly);
                        _subProcessStatus.Add(Hourly, 3);
                        _worker2.ReportProgress(_index + 7);
                    }
                }
                else
                {
                    bUpdateCheckpoints = false;
                    CallDispatcherListView(Convert.ToInt32(ListOrder.Hourly), 0, false, "Failed");
                }
                LogManager.WriteLog("Update check points status for table Hourly: " + bUpdateCheckpoints.ToString(), LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            { ExceptionManager.Publish(ex); }
            return bUpdateCheckpoints;
        }

        private bool ImportAllEvents()
        {
            bool bUpdateCheckpoints = false;
            bool bImportEvents = false;
            try
            {
                CallDispatcherListView(Convert.ToInt32(ListOrder.Events), 10, false, "In Progress");

                bImportEvents = (_oSiteSetupConfiguration.ImportAllEvents(_iSiteCode) < 0) ? false : true;
                if (bImportEvents == true)
                {
                    bUpdateCheckpoints = _oSiteSetupConfiguration.UpdateCheckPoints(_iSiteCode, 1, Events);
                    if (bUpdateCheckpoints == false)
                    { CallDispatcherListView(Convert.ToInt32(ListOrder.Events), 0, false, "Failed"); }
                    else
                    {
                        CallDispatcherListView(Convert.ToInt32(ListOrder.Events), 100, true, "Completed");
                        _subProcessStatus.Remove(Events);
                        _subProcessStatus.Add(Events, 3);
                        _worker2.ReportProgress(_index + 7);
                    }
                }
                else
                {
                    bUpdateCheckpoints = false;
                    CallDispatcherListView(Convert.ToInt32(ListOrder.Events), 0, false, "Failed");
                }
                LogManager.WriteLog("Update check points status for table Events: " + bUpdateCheckpoints.ToString(), LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            { ExceptionManager.Publish(ex); }
            return bUpdateCheckpoints;
        }

        private bool ImportSiteSettings()
        {
            bool lSiteSettings = false;
            bool bUpdateCheckpoints = false;

            try
            {
                if (_bIsRecovery)
                {
                    CallDispatcherListView(Convert.ToInt32(ListOrder.Settings), 20, false, "In Progress");
                    lSiteSettings = (_oSiteSetupConfiguration.ImportSystemSettings(_iSiteCode) < 0) ? false : true;

                    if (lSiteSettings == true)
                    {
                        bUpdateCheckpoints = _oSiteSetupConfiguration.UpdateCheckPoints(_iSiteCode, 1, Settings);
                        if (bUpdateCheckpoints == true)
                        {
                            CallDispatcherListView(Convert.ToInt32(ListOrder.Settings), 100, true, "Completed");
                            _subProcessStatus.Remove(Settings);
                            _subProcessStatus.Add(Settings, 3);
                            _worker2.ReportProgress(_index + 7);
                        }
                        else { CallDispatcherListView(Convert.ToInt32(ListOrder.Settings), 0, false, "Failed"); }
                    }
                    else
                    { bUpdateCheckpoints = false; }

                    LogManager.WriteLog("Update check points status for table " + Settings + " "+ bUpdateCheckpoints.ToString(), LogManager.enumLogLevel.Info);
                }
                else
                {
                    CallDispatcherListView(Convert.ToInt32(ListOrderNewSite.Settings), 20, false, "In Progress");
                    lSiteSettings = (_oSiteSetupConfiguration.ImportSystemSettings(_iSiteCode) < 0) ? false : true;

                    if (lSiteSettings == true)
                    {
                        bUpdateCheckpoints = _oSiteSetupConfiguration.UpdateCheckPoints(_iSiteCode, 1, Settings);
                        if (bUpdateCheckpoints == true)
                        {
                            CallDispatcherListView(Convert.ToInt32(ListOrderNewSite.Settings), 100, true, "Completed");
                            _subProcessStatus.Remove(Settings);
                            _subProcessStatus.Add(Settings, 3);
                            _worker2.ReportProgress(_index + 7);
                        }
                        else { CallDispatcherListView(Convert.ToInt32(ListOrderNewSite.Settings), 0, false, "Failed"); }
                    }
                    else
                    { bUpdateCheckpoints = false; }

                    LogManager.WriteLog("Update check points status for table " + Settings + " " + bUpdateCheckpoints.ToString(), LogManager.enumLogLevel.Info);
                }

                //Restart all the services which had been stopped prior to recovery.
                if (ConfigManager.Read("ServicesListFromDB") != null)
                {
                    if (ConfigManager.Read("ServicesListFromDB").ToUpper() == "TRUE")
                    {
                        strListarray = null;
                        strListarray = _oSiteSetupConfiguration.GetSettingValue("ServiceNames").Split(',');
                        StartAllServices();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            return bUpdateCheckpoints;
        }

        private bool ImportCashDeskTransactions()
        {
            bool lImportTreasury = false;
            bool bUpdateCheckpoints = false;

            try
            {
                CallDispatcherListView(Convert.ToInt32(ListOrder.CashDeskTransactions), 20, false, "In Progress");
                lImportTreasury = (_oSiteSetupConfiguration.ImportCashDeskTransactions(_iSiteCode) < 0) ? false : true;

                if (lImportTreasury == true)
                {
                    bUpdateCheckpoints = _oSiteSetupConfiguration.UpdateCheckPoints(_iSiteCode, 1, CashDeskTransactions);
                    if (bUpdateCheckpoints == true)
                    {
                        CallDispatcherListView(Convert.ToInt32(ListOrder.CashDeskTransactions), 100, true, "Completed");
                        _subProcessStatus.Remove(Settings);
                        _subProcessStatus.Add(Settings, 3);
                        _worker2.ReportProgress(_index + 7);
                    }
                    else { CallDispatcherListView(Convert.ToInt32(ListOrder.CashDeskTransactions), 0, false, "Failed"); }
                }
                else
                { bUpdateCheckpoints = false; }

                LogManager.WriteLog("Update check points status for table Treasury  " + bUpdateCheckpoints.ToString(), LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            return bUpdateCheckpoints;
        }

        private bool ImportUserDetails()
        {
            bool lUserDetails = false;
            bool bUpdateCheckpoints = false;

            try
            {
                if (_bIsRecovery)
                {
                    CallDispatcherListView(Convert.ToInt32(ListOrder.Users), 20, false, "In Progress");
                    lUserDetails = (_oSiteSetupConfiguration.ImportUserDetails(_iSiteCode) < 0) ? false : true;

                    if (lUserDetails == true)
                    {
                        bUpdateCheckpoints = _oSiteSetupConfiguration.UpdateCheckPoints(_iSiteCode, 1, Users);
                        if (bUpdateCheckpoints == true)
                        {
                            CallDispatcherListView(Convert.ToInt32(ListOrder.Users), 100, true, "Completed");
                            _subProcessStatus.Remove(Users);
                            _subProcessStatus.Add(Users, 3);
                            _worker2.ReportProgress(_index + 7);
                        }
                        else { CallDispatcherListView(Convert.ToInt32(ListOrder.Users), 0, false, "Failed"); }
                    }
                    else
                    { bUpdateCheckpoints = false; }

                    LogManager.WriteLog("Update check points status for table " + Users + " "+ bUpdateCheckpoints.ToString(), LogManager.enumLogLevel.Info);
                }
                else
                {
                    CallDispatcherListView(Convert.ToInt32(ListOrderNewSite.Users), 20, false, "In Progress");
                    lUserDetails = (_oSiteSetupConfiguration.ImportUserDetails(_iSiteCode) < 0) ? false : true;

                    if (lUserDetails == true)
                    {
                        bUpdateCheckpoints = _oSiteSetupConfiguration.UpdateCheckPoints(_iSiteCode, 1, Users);
                        if (bUpdateCheckpoints == true)
                        {
                            CallDispatcherListView(Convert.ToInt32(ListOrderNewSite.Users), 100, true, "Completed");
                            _subProcessStatus.Remove(Users);
                            _subProcessStatus.Add(Users, 3);
                            _worker2.ReportProgress(_index + 7);
                        }
                        else { CallDispatcherListView(Convert.ToInt32(ListOrderNewSite.Users), 0, false, "Failed"); }
                    }
                    else
                    { bUpdateCheckpoints = false; }

                    LogManager.WriteLog("Update check points status for table " + Users + " " + bUpdateCheckpoints.ToString(), LogManager.enumLogLevel.Info);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            return bUpdateCheckpoints;
        }

        private bool ImportUserRoles()
        {
            bool bUpdateCheckpoints = false;
            bool bImportUserRoles = false;

            try
            {                
                    if (_bIsRecovery)
                    {
                        CallDispatcherListView(Convert.ToInt32(ListOrder.UserRoles), 10, false, "In Progress");

                        bImportUserRoles = (_oSiteSetupConfiguration.ImportUserRoles(_iSiteCode) < 0) ? false : true;
                        if (bImportUserRoles == true)
                        {
                            bUpdateCheckpoints = _oSiteSetupConfiguration.UpdateCheckPoints(_iSiteCode, 1, UserRoles);
                            if (bUpdateCheckpoints == false)
                            { CallDispatcherListView(Convert.ToInt32(ListOrder.UserRoles), 0, false, "Failed"); }
                            else
                            {
                                CallDispatcherListView(Convert.ToInt32(ListOrder.UserRoles), 100, true, "Completed");
                                _subProcessStatus.Remove(UserRoles);
                                _subProcessStatus.Add(UserRoles, 3);
                                _worker2.ReportProgress(_index + 7);
                            }
                        }
                        else
                        {
                            bUpdateCheckpoints = false;
                            CallDispatcherListView(Convert.ToInt32(ListOrder.UserRoles), 0, false, "Failed");
                        }

                        LogManager.WriteLog("Update check points status for table " + UserRoles + " : " + bUpdateCheckpoints.ToString(), LogManager.enumLogLevel.Info);
                    }
                    else
                    {
                        CallDispatcherListView(Convert.ToInt32(ListOrderNewSite.UserRoles), 10, false, "In Progress");

                        bImportUserRoles = (_oSiteSetupConfiguration.ImportUserRoles(_iSiteCode) < 0) ? false : true;
                        if (bImportUserRoles == true)
                        {
                            bUpdateCheckpoints = _oSiteSetupConfiguration.UpdateCheckPoints(_iSiteCode, 1, UserRoles);
                            if (bUpdateCheckpoints == false)
                            { CallDispatcherListView(Convert.ToInt32(ListOrderNewSite.UserRoles), 0, false, "Failed"); }
                            else
                            {
                                CallDispatcherListView(Convert.ToInt32(ListOrderNewSite.UserRoles), 100, true, "Completed");
                                _subProcessStatus.Remove(UserRoles);
                                _subProcessStatus.Add(UserRoles, 3);
                                _worker2.ReportProgress(_index + 7);
                            }
                        }
                        else
                        {
                            bUpdateCheckpoints = false;
                            CallDispatcherListView(Convert.ToInt32(ListOrderNewSite.UserRoles), 0, false, "Failed");
                        }

                        LogManager.WriteLog("Update check points status for table " + UserRoles + " : " + bUpdateCheckpoints.ToString(), LogManager.enumLogLevel.Info);
                    }
                }
            catch (Exception ex)
            { ExceptionManager.Publish(ex); }
            return bUpdateCheckpoints;
        }

        private bool ImportCalendars()
        {
            bool bUpdateCheckpoints = false;
            bool bImportCalendars = false;

            try
            {
                if (_bIsRecovery)
                {
                    CallDispatcherListView(Convert.ToInt32(ListOrder.Calendars), 10, false, "In Progress");

                    bImportCalendars = (_oSiteSetupConfiguration.ImportCalendars(_iSiteCode) < 0) ? false : true;
                    bImportCalendars = (_oSiteSetupConfiguration.ImportAAMSDetails(_iSiteCode) < 0) ? false : true;
                    bImportCalendars = (_oSiteSetupConfiguration.ImportInstallationGameInfo(_iSiteCode) < 0) ? false : true;
                    //Component Verification.
                    _oSiteSetupConfiguration.ImportComponentDetails(_iSiteCode);
                    //Game Details.
                    _oSiteSetupConfiguration.ImportOtherGameDetails(_iSiteCode);
                    //Update Seed Values
                    _oSiteSetupConfiguration.UpdateSeedValues(_iSiteCode);

                    if (bImportCalendars == true)
                    {
                        bUpdateCheckpoints = _oSiteSetupConfiguration.UpdateCheckPoints(_iSiteCode, 1, Calendars);
                        if (bUpdateCheckpoints == false)
                        { CallDispatcherListView(Convert.ToInt32(ListOrder.Calendars), 0, false, "Failed"); }
                        else
                        {
                            CallDispatcherListView(Convert.ToInt32(ListOrder.Calendars), 100, true, "Completed");
                            _subProcessStatus.Remove(Calendars);
                            _subProcessStatus.Add(Calendars, 3);
                            _worker2.ReportProgress(_index + 7);
                        }
                    }
                    else
                    {
                        bUpdateCheckpoints = false;
                        CallDispatcherListView(Convert.ToInt32(ListOrder.Calendars), 0, false, "Failed");
                    }

                    LogManager.WriteLog("Update check points status for table " + Calendars + " : " + bUpdateCheckpoints.ToString(), LogManager.enumLogLevel.Info);
                }
                else
                {
                    CallDispatcherListView(Convert.ToInt32(ListOrderNewSite.Calendars), 10, false, "In Progress");

                    bImportCalendars = (_oSiteSetupConfiguration.ImportCalendars(_iSiteCode) < 0) ? false : true;
                    bImportCalendars = (_oSiteSetupConfiguration.ImportAAMSDetails(_iSiteCode) < 0) ? false : true;
                    bImportCalendars = (_oSiteSetupConfiguration.ImportInstallationGameInfo(_iSiteCode) < 0) ? false : true;
                    //Component Verification.
                    _oSiteSetupConfiguration.ImportComponentDetails(_iSiteCode);
                    //Game Details.
                    _oSiteSetupConfiguration.ImportOtherGameDetails(_iSiteCode);

                    if (bImportCalendars == true)
                    {
                        bUpdateCheckpoints = _oSiteSetupConfiguration.UpdateCheckPoints(_iSiteCode, 1, Calendars);
                        if (bUpdateCheckpoints == false)
                        { CallDispatcherListView(Convert.ToInt32(ListOrderNewSite.Calendars), 0, false, "Failed"); }
                        else
                        {
                            CallDispatcherListView(Convert.ToInt32(ListOrderNewSite.Calendars), 100, true, "Completed");
                            _subProcessStatus.Remove(Calendars);
                            _subProcessStatus.Add(Calendars, 3);
                            _worker2.ReportProgress(_index + 7);
                        }
                    }
                    else
                    {
                        bUpdateCheckpoints = false;
                        CallDispatcherListView(Convert.ToInt32(ListOrderNewSite.Calendars), 0, false, "Failed");
                    }

                    LogManager.WriteLog("Update check points status for table " + Calendars + " : " + bUpdateCheckpoints.ToString(), LogManager.enumLogLevel.Info);
                }
            }
            catch (Exception ex)
            { ExceptionManager.Publish(ex); }
            return bUpdateCheckpoints;
        }

        private bool SiteStatusMessage(int iSiteStatus, string sMessage)
        {
            bool bStatus = false;

            switch (iSiteStatus)
            {
                case 3:
                    {
                        LogManager.WriteLog("Update the site status to " + sMessage + " !", LogManager.enumLogLevel.Info);
                        bStatus = true;
                        break;
                    }
                case 2:
                    {
                        LogManager.WriteLog("Could not update the site status to " + sMessage + " !", LogManager.enumLogLevel.Error);
                        bStatus = false;
                        break;
                    }
                case 1:
                    {
                        LogManager.WriteLog("Could not update the Enterprise site status to " + sMessage + " !", LogManager.enumLogLevel.Error);
                        bStatus = false;
                        break;
                    }
                default:
                    {
                        LogManager.WriteLog("Could not update the site status in Site and Enterprise to " + sMessage + " !", LogManager.enumLogLevel.Error);
                        bStatus = false;
                        break;
                    }
            }

            return bStatus;
        }

        private void CallDispatcherListView(int index, int Percentage, bool Status, string Message)
        {
            Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
            {
                OnListViewItem_CollectionChanged(index, Percentage, Status, Message);
                lvCheckpoints.ScrollIntoView(index);
            });
        }

        private void MaintainCheckPointsListView(string sDataType)
        {
            Dictionary<string, int> NamesToRemove = new Dictionary<string, int>();
            foreach (KeyValuePair<string, int> GetKey in _subProcessStatus)
            {
                if (GetKey.Key.ToString() == sDataType)
                {
                    if (GetKey.Value < 3)
                    {
                        NamesToRemove.Add(GetKey.Key, GetKey.Value);
                    }
                }
            }
            foreach (KeyValuePair<string, int> NameToRemove in NamesToRemove)
            {
                _subProcessStatus.Remove(NameToRemove.Key);
                _subProcessStatus.Add(NameToRemove.Key, NameToRemove.Value + 1);
            }
        }

        private void RemoveItemSubProcessStatus(string sDataType)
        {
            Dictionary<string, int> NamesToRemove = new Dictionary<string, int>();
            foreach (KeyValuePair<string, int> GetKey in _subProcessStatus)
            {
                if (GetKey.Key.ToString() == sDataType)
                {
                    if (GetKey.Value < 3)
                    {
                        NamesToRemove.Add(GetKey.Key, GetKey.Value);
                    }
                }
            }
            foreach (KeyValuePair<string, int> NameToRemove in NamesToRemove)
            {
                _subProcessStatus.Remove(NameToRemove.Key);
            }
        }

        private void StartAllServices()
        {
            StringBuilder strServicelist = new StringBuilder();
            bool bServiceStatus = false;
            //Stop all serivces before doing clearing data
            if (strListarray != null)
            {
                for (int i = 0; i < strListarray.Length; i++)
                {
                    if (strListarray[i] != null && strListarray[i] != string.Empty)
                    {
                        strServicelist.Append(strListarray[i].ToString() + ",");
                        strServiceStatus = _oSiteSetupConfiguration.GetServiceStatus(strListarray[i]);
                        if (strServiceStatus.ToUpper() == "NOSERVICE")
                        {
                            LogManager.WriteLog(strListarray[i] + "Service not found", LogManager.enumLogLevel.Info);
                        }
                        else
                        {
                            bServiceStatus = _oSiteSetupConfiguration.StartService(strListarray[i].ToString());
                            System.Windows.Forms.Application.DoEvents();
                            if (bServiceStatus)
                            {
                                LogManager.WriteLog("Status of  " + strListarray[i] + "is" + bServiceStatus.ToString(), LogManager.enumLogLevel.Info);
                            }
                            else
                            {
                                LogManager.WriteLog("Status of  " + strListarray[i] + "is" + bServiceStatus.ToString(), LogManager.enumLogLevel.Info);
                            }
                        }
                    }
                }
            }
        }
        #endregion

        #region Events

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_sType.ToLower() == Recovery)
                {                    
                    tbHeader.Text = FindResource("SiteRecovery") as string;
                    _bIsRecovery = true;
                }
                else
                {                    
                    tbHeader.Text = FindResource("SiteAutoConfiguration") as string;
                    _bIsRecovery = false;
                }             

                btnRecoverAgain.Visibility = Visibility.Hidden;

                LoadData();
                lvCheckpoints.ItemsSource = LoadListViewCollection;
                StartProcess();

                LogManager.WriteLog("SiteCheckpoints started for the Action: " + _sType, LogManager.enumLogLevel.Debug);

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("SiteCheckpoints started for the Action: " + _sType, LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
            }
        }

        private void Canvas_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (MoveKeyboard != null)
            {
                MoveKeyboard.Invoke(this, EventArgs.Empty);
            }
        }

        private void _workerTotalProcess_DoWork(object sender, DoWorkEventArgs e)
        {
            _worker2 = sender as BackgroundWorker;
           
            _index = _index + 1;

            _checkImportDone = new DelImportProcess(ImportProcess);
            IAsyncResult ar = _checkImportDone.BeginInvoke(new AsyncCallback(ImportComplete), new object());
            while (!ar.IsCompleted)
            {
                ar.AsyncWaitHandle.WaitOne(1, false);
            }

            if (ar.IsCompleted == true)
            {
                if (_rImport == true)
                {
                    //_workerTotalProcess_RunWorkerCompleted(sender, new RunWorkerCompletedEventArgs(ar.AsyncState, null, false));
                }
                else
                {
                    e.Cancel = true;
                    _worker2.CancelAsync();
                    return;
                }
            }
            if (_worker2.CancellationPending)
            {
                e.Cancel = true;
                return;
            }
        }

        private void _workerTotalProcess_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //if (_index == 100) { _index = 7; }
            ProgressBarMain.Value = ProgressBarMain.Value + 7;
        }

        private void _workerTotalProcess_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            int iSiteStatus = 0;

            if (e.Cancelled == true)
            {
                ProgressBarMain.Value = 0;
                btnRecoverAgain.Visibility = Visibility.Visible;
                MessageBox.ShowBox("MessageID15", BMC_Icon.Information, BMC_Button.OK, tbHeader.Text);
            }
            else
            {
                Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
                {
                    iSiteStatus = _oSiteSetupConfiguration.UpdateSiteStatus(_iSiteCode, "FULLYCONFIGURED");
                    SiteStatusMessage(iSiteStatus, "FullyConfigured");
                    ProgressBarMain.Value = 100;
                    btnCancel.Visibility = Visibility.Hidden;
                    System.Windows.Forms.DialogResult dr = MessageBox.ShowBox("MessageID16", BMC_Icon.Information, BMC_Button.OK, tbHeader.Text);

                    if (dr.ToString() == "OK")
                    {
                        //_instance = null;
                        //this.Close();  
                        App.Current.Shutdown();                                                                     
                        //Login objLogin = new Login();
                       // Hide();
                        //objLogin.Show();                        
                        //Close();
                    }
                });               
            }
        }

        private void OnListViewItem_CollectionChanged(int index, int progPercentage, bool CheckStatus, string sDescription)
        {
            try
            {
                ObservableCollection<LoadListView> view = (ObservableCollection<LoadListView>)lvCheckpoints.ItemsSource;

                view[index].Progress = progPercentage;
                view[index].Status = CheckStatus.ToString();
                view[index].StatusDescription = sDescription;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog(ex.Message + ex.Source, LogManager.enumLogLevel.Debug);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                btnCancel.IsEnabled = false;
                System.Windows.Forms.DialogResult dr;
                dr = MessageBox.ShowBox("MessageID17", BMC_Icon.Question, BMC_Button.YesNo, tbHeader.Text);

                if (dr.ToString().ToUpper() == "NO")
                {
                    return;
                }
                else
                {
                    //this.Visibility = Visibility.Hidden;                   
                    _worker2.CancelAsync();
                    _workerTotalProcess.CancelAsync();
                    _instance = null;
                    FactoryResetMethods oFactoryMethods = FactoryResetMethods.FactoryResetMethodsInstance;
                    _oSiteSetupConfiguration.UpdateAllCheckPoints(0);
                    oFactoryMethods.RunScripts();
                    App.Current.Shutdown();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            finally
            {
                btnCancel.IsEnabled = true;
            }
        }

        private void btnRecoverAgain_Click(object sender, RoutedEventArgs e)
        {
            StartProcess();
            btnRecoverAgain.Visibility = Visibility.Hidden;
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            //this.Close();                       
            System.Windows.Forms.DialogResult dr;
            dr = MessageBox.ShowBox("MessageID18", BMC_Icon.Question, BMC_Button.YesNo);

            if (dr.ToString() == "No")
            {
                return;
            }
            else
            {
                //this.Visibility = Visibility.Hidden;
                // _workerTotalProcess.CancelAsync();
                App.Current.Shutdown();
                _instance = null;
            }
        }

        #endregion Events

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
                        this.MyWindow.Loaded -= (this.UserControl_Loaded);
                        this.Root.MouseDown -= (this.Canvas_MouseDown);
                        this.btnExit.Click -= (this.btnExit_Click);
                        this.btnCancel.Click -= (this.btnCancel_Click);
                        this.btnRecoverAgain.Click -= (this.btnRecoverAgain_Click);
                    },
                    (c) =>
                    {
                    });
                    this.WriteLog("SiteCheckPoints objects are released successfully.");

                }
                disposed = true;
            }
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="SiteCheckPoints"/> is reclaimed by garbage collection.
        /// </summary>
        ~SiteCheckPoints()
        {
            Dispose(false);
        }

        #endregion
    }

    #region List View Class
    public class LoadListView : DependencyObject, INotifyPropertyChanged
    {

        public static readonly DependencyProperty DataProperty =
            DependencyProperty.Register("Data", typeof(string), typeof(LoadListView), new UIPropertyMetadata(null));

        public static readonly DependencyProperty StatusProperty =
            DependencyProperty.Register("Status", typeof(string), typeof(LoadListView), new UIPropertyMetadata(null));

        public static readonly DependencyProperty ProgressProperty =
            DependencyProperty.Register("Progress", typeof(int), typeof(LoadListView), new UIPropertyMetadata(null));

        public static readonly DependencyProperty StatusDescriptionProperty =
           DependencyProperty.Register("StatusDescription", typeof(string), typeof(LoadListView), new UIPropertyMetadata(null));

        public string Data
        {
            get { return (string)GetValue(DataProperty); }
            set { SetValue(DataProperty, value); }
        }
        public string Status
        {
            get { return (string)GetValue(StatusProperty); }
            set { SetValue(StatusProperty, value); }
        }
        public int Progress
        {
            get { return (int)GetValue(ProgressProperty); }
            set { SetValue(ProgressProperty, value); }
        }
        public string StatusDescription
        {
            get { return (string)GetValue(StatusDescriptionProperty); }
            set { SetValue(StatusDescriptionProperty, value); }
        }


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

        #region INotifyPropertyChanged Members

        //event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
        //{
        //    //add { throw new NotImplementedException(); }
        //    //remove { throw new NotImplementedException(); }
        //}

        #endregion
    }
    #endregion

}

