using System;
using System.Data;
using System.Reflection;
using System.Threading;
using BMC.Common;
using BMC.Common.ConfigurationManagement;
using BMC.Common.ExceptionManagement;
using BMC.Common.LogManagement;
using System.Data.SqlClient;
using BMC.CoreLib.Concurrent;
using BMC.CoreLib.Configuration;
using BMC.CoreLib.Diagnostics;
using BMC.CoreLib;
using System.Collections.Generic;
using System.Linq;
using BMC.Migration114.Business;

namespace BMC.BusinessClasses.BusinessLogic
{
    public class BMCEnterpriseImportExport : Interfaces.IBMCEnterpriseExportImport
    {

        #region IBMCEnterpriseExportImport Members
        static ManualResetEvent _QuitProcessing = new ManualResetEvent(false);
        static ManualResetEvent _StopService = new ManualResetEvent(false);
        private static volatile DataTable _dtSiteList = new DataTable();

        public BMCEnterpriseImportExport() { }

        #region Performance Settings added by A.Vinod Kumar on 5:17 PM 08/06/12
        private bool _limitThreadCount = false;
        int _LimitRecordCount = 1;
        private IExecutorService _executorService = null;
        private ConfigAppSettingWatcher _appWathcer = null;
        private IThreadPoolExecutor<SiteInfo> _executor = null;
        private int _threadCount = 0;
        private ThreadPoolType _poolType = ThreadPoolType.FreeThreads;
        private int _timeIntervalToCheckForNewSite = 0;
        private int _perItemProcessMilliseconds = 0;
        private int _populateImportHistoryThreadIntervalInSecs = 0;
        private bool _Log_All = false;
        private bool _Is_11_4_Migration = false;
        private string _114ExchangeWebUrl = string.Empty;
        private InitializeStatus _initStatus = InitializeStatus.Uninitialized;
        private object _lockThread = new object();
        private bool _isDynamic = false;
        

        private class SiteInfo : DisposableObject, IExecutorKeyThread
        {
            private string _siteCodes = string.Empty;

            public SiteInfo(string siteCode)
            {
                this.SiteCode = siteCode;
                this.SiteCodes = string.Empty;
            }

            public string SiteCode { get; private set; }

            public string SiteCodes
            {
                get { return _siteCodes; }
                set
                {
                    if (!_siteCodes.IsEmpty())
                    {
                        _siteCodes += ",";
                    }
                    _siteCodes += value;
                }
            }

            #region IExecutorKey Members

            public string UniqueKey
            {
                get { return this.SiteCode; }
            }

            #endregion

            #region IExecutorKeyThread Members

            public int GetThreadIndex(int capacity)
            {
                return (Convert.ToInt32(UniqueKey) % capacity);
            }

            #endregion
        }

        public BMCEnterpriseImportExport(bool value)
        {
            this.Load();
        }

        private void Load()
        {
            _appWathcer = new ConfigAppSettingWatcher();
            _appWathcer
                .Register("LimitThreadCount", (s) => { _limitThreadCount = Convert.ToBoolean(s); })
                .Register("ThreadCount", (s) => { _threadCount = Convert.ToInt32(s); })
                .Register("ThreadPoolType", (s) => { _poolType = (ThreadPoolType)(Convert.ToInt32(s)); })
                .Register("PerItemProcessMilliseconds", (s) => { _perItemProcessMilliseconds = Convert.ToInt32(s); })
                .Register("TimeIntervalToCheckForNewSite", (s) => { _timeIntervalToCheckForNewSite = (1000 * Convert.ToInt32(s)); })
                .Register("PopulateImportHistoryThreadIntervalInSecs", (s) => { _populateImportHistoryThreadIntervalInSecs = (Convert.ToInt32(s)); })
                .Register("LimitRecordCount", (s) => { _LimitRecordCount = Convert.ToInt32(s); })
                .Register("Log_All", (s) => { _Log_All = Convert.ToBoolean(s); })
                //Settings for processing 11.4 records Starts
                .Register("Is_11_4_Migration", (s) => { _Is_11_4_Migration = Convert.ToBoolean(s); })
                .Register("114ExchangeWebUrl", (s) => { _114ExchangeWebUrl = s.ToUpper(); });
                //Settings for processing 11.4 records Ends
                
            _appWathcer.Start();

            if (_limitThreadCount)
            {
                _siteHash = new SortedDictionary<string, SiteInfo>(StringComparer.InvariantCultureIgnoreCase);
                _siteLists = new List<SiteInfo>();

                _isDynamic = (_threadCount <= 0);
                _poolType = (_isDynamic ? ThreadPoolType.BlockDynamic : ThreadPoolType.BlockQueue);
                _executorService = ExecutorServiceFactory.CreateExecutorService();
                _executor = ThreadPoolExecutorFactory.CreateThreadPool<SiteInfo>(_executorService, _poolType, _threadCount, 1);
                _executor.ProcessItem += new ExecutorProcessItemHandler<SiteInfo>(OnExecutor_ProcessItem);
                SharedData.ActiveLogger.WriteToExternalLog += new WriteToExternalLogHandler(ActiveLogger_WriteToExternalLog);
            }
        }

        void ActiveLogger_WriteToExternalLog(string formattedMessage, LogEntryType type, object extra)
        {
            LogManager.WriteLog(formattedMessage, LogManager.enumLogLevel.Info);
        }

        void OnExecutor_ProcessItem(BMCEnterpriseImportExport.SiteInfo item)
        {
            ProcessRecordsForSites(item, _perItemProcessMilliseconds);
            item.Dispose();
        }

        private void ProcessRecordsForSites(SiteInfo item, int PerItemProcessInterval)
        {
            ModuleProc PROC = new ModuleProc("BMCEnterpriseImportExport", "ProcessRecordsForSites");
           
            
            try
            {
                int iPreviousIHID = 0;
                while (!_executorService.WaitForShutdown(_populateImportHistoryThreadIntervalInSecs))
                {
                    try
                    {
                        if (!item.SiteCodes.IsEmpty())
                        {
                            this.ProcessRecordsForSite(item.SiteCodes, PerItemProcessInterval, ref iPreviousIHID);
                        }
                    }
                    catch (Exception ex)
                    {
                        Log.Exception(PROC, ex);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        private void ProcessRecordsForSite(string siteCode, int PerItemProcessInterval, ref int iPreviousIHID)
        {
            
            ModuleProc PROC = new ModuleProc("BMCEnterpriseImportExport", "ProcessRecordsForSite");
            if (_Log_All)
                Log.Info(PROC, "Called for site(s) : " + siteCode);

            try
            {
                //if (_Log_All)
                //    LogManager.WriteLog("ProcessRecordsForSite Previous Record:" + iPreviousIHID.ToString(), LogManager.enumLogLevel.Debug);

                DataTable dtRecords = DataHelper.GetRecordsToBeImportedForSites(siteCode.ToString(), 0, _LimitRecordCount);

                if (dtRecords.Rows.Count > 0)
                {
                    int count = dtRecords.Rows.Count;
                    if (_Log_All)
                        LogManager.WriteLog(string.Format(":::> Processing {0:D} Items for Site : {1}  on Thread : {2}.",
                            count, siteCode, Thread.CurrentThread.ManagedThreadId), LogManager.enumLogLevel.Info);

                    bool bBreakResult = false;
                    foreach (DataRow dr in dtRecords.Rows)
                    {
                        if (_QuitProcessing.WaitOne(PerItemProcessInterval))
                        {
                            break;
                        }
                        bool bResult = false;
                        string strResult = string.Empty;
                        bool is114Record = false;

                        try
                        {
                            if (_Is_11_4_Migration)
                            {
                                string sWebURL = dr["114Site_WebURL"].ToString().ToUpper();

                                if (sWebURL.Contains(_114ExchangeWebUrl))
                                {
                                    is114Record = true;
                                }
                            }

                            if (_Log_All)
                            {
                                LogManager.WriteLog(string.Format("### [START : {0}, Thread : {1:D}] Site Type : {2}, Site Code : {3}, IH_Type : {4}",
                                            dr["IH_ID"].ToString(), Thread.CurrentThread.ManagedThreadId, (is114Record ? "11.4" : "12.4"), dr["IH_Site_Code"].ToString(),
                                            dr["IH_Type"].ToString()), LogManager.enumLogLevel.Info);
                            }
                        }
                        catch { }

                        try
                        {
                            if (is114Record)
                            {
                                bResult = Import_11_4_ExchangeDetails.ProcessData(dr);
                            }
                            else
                            {
                                switch (dr["IH_Type"].ToString().ToUpper())
                                {
                                    case "EVENT":
                                        break;
                                    case "METER_HISTORY":
                                        bResult =
                                            DataHelper.ImportMeterHistory(dr["IH_Details"].ToString());
                                        break;
                                    case "COLLECTION":
                                        bResult =
                                            DataHelper.ImportCollection(dr["IH_Details"].ToString());
                                        break;
                                    case "STACKERLEVEL":
                                        bResult =
                                            DataHelper.ImportStackerLevelDetails(dr["IH_Details"].ToString());
                                        break;
                                    case "LIQUIDATIONDETAILS":
                                        bResult =
                                            DataHelper.ImportLiquidationDetails(dr["IH_Details"].ToString(), dr["IH_Site_Code"].ToString());
                                        break;
                                    case "LIQUIDATIONSHAREDETAILS":
                                        bResult =
                                            DataHelper.ImportLiquidationShareDetails(dr["IH_Details"].ToString(), dr["IH_Site_Code"].ToString());
                                        break;
                                    case "GLORYAUDIT":
                                        bResult =
                                            DataHelper.ImportGloryAuditDetails(dr["IH_Details"].ToString());
                                        break;
                                    case "COLLECTIONDETAILS":
                                        bResult =
                                            DataHelper.ImportIndividualCollectionDetails(
                                                dr["IH_Details"].ToString());
                                        break;
                                    case "DAILY":
                                        bResult = DataHelper.InsertRead(dr["IH_Details"].ToString());
                                        break;
                                    case "HOURLY":
                                        bResult =
                                            DataHelper.ImportHourlyStatisticsData(
                                                dr["IH_Details"].ToString());
                                        break;
                                    case "LOGSITEEVENT":
                                        bResult = DataHelper.LogSiteEvent(dr["IH_Details"].ToString());
                                        break;
                                    case "PAYTABLE":
                                        bResult = DataHelper.ImportPaytableDetails(dr["IH_Details"].ToString());
                                        break;
                                    case "GAMESESSION":
                                        bResult = DataHelper.ImportGameSessionDetails(dr["IH_Details"].ToString());
                                        break;
                                    case "MACHINECLASS":
                                        bResult = DataHelper.ImportMachineClassDetails(dr["IH_Details"].ToString());
                                        break;
                                    case "CHANGEPASSWORD":
                                        bResult = DataHelper.ImportPasswordChange(dr["IH_Details"].ToString());
                                        break;
                                    case "MACHINEMAINTENANCE":
                                        bResult = DataHelper.ImportMachineMaintenance(dr["IH_Details"].ToString());
                                        break;
                                    case "MAINTENANCESESSION":
                                        bResult = DataHelper.ImportMaintenanceSession(dr["IH_Details"].ToString());
                                        break;
                                    case "MAINTENANCEHISTORY":
                                        bResult = DataHelper.ImportMaintenanceHistory(dr["IH_Details"].ToString());
                                        break;
                                    case "MAINTENANCEREASONCATEGORY":
                                        bResult = DataHelper.ImportMaintenanceReasonCategory(dr["IH_Details"].ToString());
                                        break;
                                    case "REINSTATE":
                                        bResult = DataHelper.ImportReInstateData(dr["IH_Details"].ToString());
                                        break;
                                    case "GAMEPAYTABLEDETAILS":
                                        bResult = DataHelper.ImportGamePaytableDetails(dr["IH_Details"].ToString());
                                        break;
                                    case "BATCHEXPCOMPLETE":
                                        bResult = DataHelper.ImportBatchExportCompletedStatus(dr["IH_Details"].ToString());
                                        break;
                                    case "FUND":
                                        bResult = DataHelper.ImportFundDetails(dr["IH_Details"].ToString());
                                        break;
                                    case "FACTORYRESET":
                                        bResult = DataHelper.DoFactoryReset(dr["IH_Site_Code"].ToString(), dr["IH_Details"].ToString());
                                        break;
                                    case "VAULTDROP":
                                        bResult = DataHelper.ImportVaultDropDetails(dr["IH_Details"].ToString());
                                        break;
                                    case "VAULTTRANSACTIONEVENT":
                                        bResult = DataHelper.ImportVaultTransactionEventDetails(dr["IH_Details"].ToString());
                                        break;
                                    case "VAULTEVENT":
                                        bResult = DataHelper.ImportVaultEventDetails(dr["IH_Details"].ToString());
                                        break;
                                    case "VAULTBALANCE":
                                        bResult = DataHelper.ImportVaultBalanceDetails(dr["IH_Details"].ToString());
                                        break;
                                    case "VAULTTRANSACTION":
                                        bResult = DataHelper.ImportVaultTransactionDetails(dr["IH_Details"].ToString());
                                        break;
                                    case "ENROLLVAULT":
                                        bResult = DataHelper.ImportVaultEnrollmentDetails(dr["IH_Details"].ToString());
                                        break;

                                    default:
                                        throw new Exception("TYPE NOT FOUND:" + dr["IH_Type"]);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            ExceptionManager.Publish(ex);
                            strResult = ex.Message;
                            bResult = false;
                        }
                        finally
                        {
                            if (bResult)
                            {
                                DataHelper.UpdateProcessDetailsForImportHistory(
                                    Int32.Parse(dr["IH_ID"].ToString()), strResult, 100);
                                bBreakResult = false;
                            }
                            else
                            {
                                DataHelper.UpdateProcessDetailsForImportHistory(
                                    Int32.Parse(dr["IH_ID"].ToString()), strResult, -1);
                                bBreakResult = true;
                            }
                            try
                            {
                                iPreviousIHID = Int32.Parse(dr["IH_ID"].ToString());
                            }
                            catch (Exception Ex)
                            {
                                ExceptionManager.Publish(Ex);
                                LogManager.WriteLog("ProcessRecordsForSite Previous Record Set to 0", LogManager.enumLogLevel.Debug);
                                iPreviousIHID = 0;
                            }

                            try
                            {
                                if (_Log_All)
                                {
                                    LogManager.WriteLog(string.Format("### [END : {0}, Thread : {1:D}] Site Type : {2}, Site Code : {3}, IH_Type : {4}, Status : {5}",
                                            dr["IH_ID"].ToString(), Thread.CurrentThread.ManagedThreadId, (is114Record ? "11.4" : "12.4"), dr["IH_Site_Code"].ToString(),
                                            dr["IH_Type"].ToString(), (bResult ? "100" : "-1")), LogManager.enumLogLevel.Info);
                                }
                            }
                            catch { }
                        }

                        if (bBreakResult)
                        {
                            DataHelper.ResetInProgressIhRecords(siteCode.ToString());
                            break;
                        }
                    }
                }
                else
                {
                    if (_Log_All)
                        LogManager.WriteLog(string.Format(":::> No Items found for Site : {0} on Thread : {1}.",
                            siteCode, Thread.CurrentThread.ManagedThreadId), LogManager.enumLogLevel.Info);

                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        private void Unload()
        {
            _appWathcer.Dispose();
            if (_limitThreadCount)
            {
                _executorService.AwaitTermination(TimeSpan.Zero);
            }
        }


        public bool ImportDataToEnterprise()
        {
            if (_limitThreadCount)
            {
                Thread th = null;
                Extensions.InitializeThreadFunc(ref th, ref _initStatus, _lockThread,
                    new ThreadStart(this.ImportDataToEnterprise_Optimized), "ImportDataToEnterprise_Optimized_");
                return true;
            }
            else
            {
                return this.ImportDataToEnterprise_Legacy();
            }
        }

        private void ImportDataToEnterprise_Optimized()
        {
            ModuleProc PROC = new ModuleProc("BMCEnterpriseImportExport", "ImportDataToEnterprise_Optimized");
            _initStatus = InitializeStatus.Completed;

            try
            {
                this.InitSiteLists();

                while (!_executorService.WaitForShutdown(_populateImportHistoryThreadIntervalInSecs))
                {
                    try
                    {
                        this.PopulateSites();
                    }
                    catch (Exception ex)
                    {
                        Log.Exception(PROC, ex);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        private IDictionary<string, SiteInfo> _siteHash = null;
        private IList<SiteInfo> _siteLists = null;

        private void InitSiteLists()
        {
            ModuleProc PROC = new ModuleProc("BMCEnterpriseImportExport", "InitSiteLists");
            try
            {
                if (!_isDynamic)
                {
                    for (int i = 0; i < _threadCount; i++)
                    {
                        SiteInfo si = new SiteInfo(i.ToString());
                        _executor.QueueWorkerItem(si);
                        _siteLists.Add(si);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        private void PopulateSites()
        {
            ModuleProc PROC = new ModuleProc("BMCEnterpriseImportExport", "PopulateSites");
            try
            {
                DataTable dt = DataHelper.GetSiteList();

                if (dt != null)
                {
                    var newSites = (from k in dt.AsEnumerable()
                                    join d in _siteHash.Keys.OfType<string>()
                                    on k.Field<string>("SITE_CODE") equals d
                                    into matched
                                    from m in matched.DefaultIfEmpty()
                                    where m == null
                                    select new { SiteCode = k.Field<string>("SITE_CODE") });
                    if (newSites != null)
                    {
                        foreach (var newSite in newSites)
                        {
                            string siteCodeString = newSite.SiteCode;
                            int siteCode = TypeSystem.GetValueInt(siteCodeString);
                            if (_isDynamic)
                            {
                                SiteInfo si = new SiteInfo(siteCodeString)
                                {
                                    SiteCodes = siteCodeString
                                };
                                _executor.QueueWorkerItem(si);
                                _siteHash.Add(siteCodeString, si);
                            }
                            else
                            {
                                int index = (siteCode % (_threadCount - 1));
                                if (index >= 0 && index < _siteLists.Count)
                                {
                                    SiteInfo si = _siteLists[index];
                                    si.SiteCodes = siteCodeString;
                                    _siteHash.Add(siteCodeString, si);
                                }
                            }
                        }
                    }
                }
                else
                {
                    LogManager.WriteLog("No sites available for processing.", LogManager.enumLogLevel.Info);
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }
        #endregion

        public bool ImportDataToEnterprise_Legacy()
        {
            _dtSiteList = DataHelper.GetSiteList();

            LogManager.WriteLog("Total Site Count :" + _dtSiteList.Rows.Count, LogManager.enumLogLevel.Info);
            if (_dtSiteList.Rows.Count > 1)
            {
                LogManager.WriteLog("SITE COUNT MORE THAN 1 ENTERING INTO LOOP TO CREATE MULTIPLE THREADS",
                                    LogManager.enumLogLevel.Info);


                Thread workerThread;
                for (int i = 0; i < _dtSiteList.Rows.Count; i++)
                {
                    LogManager.WriteLog("Spawning New Thread", LogManager.enumLogLevel.Info);
                    LogManager.WriteLog("SITE CODE :" + _dtSiteList.Rows[i]["SITE_CODE"], LogManager.enumLogLevel.Info);
                    workerThread = new Thread((ProcessWorkerThread)) { Name = _dtSiteList.Rows[i]["SITE_CODE"].ToString() };
                    workerThread.Start(_dtSiteList.Rows[i]["SITE_CODE"].ToString());
                    workerThread.Priority = ThreadPriority.Normal;
                }
            }
            else
            {
                if (_dtSiteList.Rows.Count == 1)
                {
                    LogManager.WriteLog("Spawning New Thread", LogManager.enumLogLevel.Info);
                    LogManager.WriteLog("SITE CODE :" + _dtSiteList.Rows[0]["SITE_CODE"], LogManager.enumLogLevel.Info);
                    var workerThread = new Thread((ProcessWorkerThread)) { Name = _dtSiteList.Rows[0]["SITE_CODE"].ToString() };
                    workerThread.Start(_dtSiteList.Rows[0]["SITE_CODE"].ToString());
                    workerThread.Priority = ThreadPriority.Normal;
                }
            }

            var workerTreadForChecNewSite = new Thread(CheckForNewSite);
            workerTreadForChecNewSite.Start();
            return true;

        }

        public bool ResetImportHistory()
        {
            bool bRecordsReset = DataHelper.ResetInProgressIhRecords("");
            if (bRecordsReset)
                LogManager.WriteLog("records resetted in import history", LogManager.enumLogLevel.Info);
            else
                LogManager.WriteLog("records not resetted in import history", LogManager.enumLogLevel.Info);

            return true;
        }

        public bool ResetExportHistory()
        {
            bool bRecordsReset = DataHelper.ResetInProgressEhRecords();
            if (bRecordsReset)
                LogManager.WriteLog("records resetted in export history", LogManager.enumLogLevel.Info);
            else
                LogManager.WriteLog("records not resetted in export history", LogManager.enumLogLevel.Info);

            return true;
        }

        #endregion

        #region

        private void CheckForNewSite()
        {
            DataTable dtNewSiteList = null;

            while (true)
            {
                dtNewSiteList = DataHelper.GetSiteList();
                foreach (DataRow dr in dtNewSiteList.Rows)
                {
                    _dtSiteList.DefaultView.Sort = "SITE_CODE";

                    if (_dtSiteList.DefaultView.Find(dr["SITE_CODE"].ToString()) != -1) continue;

                    LogManager.WriteLog("Spawning New Thread", LogManager.enumLogLevel.Info);
                    LogManager.WriteLog("SITE CODE :" + dr["SITE_CODE"], LogManager.enumLogLevel.Info);

                    var workerThread = new Thread((ProcessWorkerThread)) { Name = dr["SITE_CODE"].ToString() };

                    workerThread.Start(dr["SITE_CODE"].ToString());
                    workerThread.Priority = ThreadPriority.Normal;

                    var drNewSiteList = _dtSiteList.NewRow();

                    drNewSiteList["SITE_CODE"] = dr["SITE_CODE"];
                    _dtSiteList.Rows.Add(drNewSiteList);
                }

                if (_QuitProcessing.WaitOne((int.Parse(ConfigManager.Read("TimeIntervalToCheckForNewSite")) * 1000))) break;
            }
        }

        private void ProcessWorkerThread(Object strSiteName)
        {
            int PerItemProcessInterval;
            try
            {
                //Wait befor processing next data(To avoid CPU load) 
                PerItemProcessInterval = Convert.ToInt32(ConfigManager.Read("PerItemProcessMilliseconds"));
            }
            catch
            {
                PerItemProcessInterval = 100;
            }
            string siteCode = strSiteName.ToString();
            int iPreviousIHID = 0;
            while (true)
            {

                try
                {
                    ProcessRecordsForSite(siteCode, PerItemProcessInterval,ref iPreviousIHID);
                    if (_QuitProcessing.WaitOne(int.Parse(ConfigManager.Read("PopulateImportHistoryThreadIntervalInSecs")))) break;
                }
                catch (Exception ex)
                {
                    ExceptionManager.Publish(ex);
                }
                finally
                {
                    _StopService.Set();
                }
            }
        }
        public void Stop()
        {
            this.Unload();
            _QuitProcessing.Set();
            LogManager.WriteLog("Service stop Initiated", LogManager.enumLogLevel.Debug);
            if (_StopService.WaitOne(2000))
            {
                LogManager.WriteLog("Service stoped after wait", LogManager.enumLogLevel.Debug);
            }
            LogManager.WriteLog("Service stoped", LogManager.enumLogLevel.Debug);
        }

        #endregion

    }
}
