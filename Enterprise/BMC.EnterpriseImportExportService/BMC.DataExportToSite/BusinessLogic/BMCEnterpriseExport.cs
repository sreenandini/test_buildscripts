using System;
using System.Data;
using System.Threading;
using BMC.Common;
using BMC.Common.ExceptionManagement;
using BMC.Common.LogManagement;
using BMC.Common.ConfigurationManagement;
using BMC.BusinessClasses.Proxy;
using System.Xml;
using BMC.CoreLib.Concurrent;
using BMC.CoreLib.Configuration;
using BMC.CoreLib;
using BMC.CoreLib.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using BMC.Migration114.Business;
using System.Xml.Linq;

namespace BMC.DataExportToSite.BusinessLogic
{
    public class BMCEnterpriseExport : Interfaces.IBMCEnterpriseExport
    {
        public static bool IsRunning;
        public static DataTable _dtSiteList;
        public static ManualResetEvent _ServiceStopped = new ManualResetEvent(false);
        public static List<string> _str12_4_ExportLiset = new List<string>();

        #region Performance Settings added by A.Vinod Kumar on 5:17 PM 08/06/12
        private bool _limitThreadCount = false;
        private IExecutorService _executorService = null;
        private ConfigAppSettingWatcher _appWathcer = null;
        private IThreadPoolExecutor<SiteInfo> _executor = null;
        private int _threadCount = 0;
        private ThreadPoolType _poolType = ThreadPoolType.FreeThreads;
        private int _timeIntervalToCheckForNewSite = 0;
        private int _perItemProcessMilliseconds = 0;
        private int _populateImportHistoryThreadIntervalInSecs = 0;
        private int _proxyTimeoutInMilliseconds = 100000;

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

        public BMCEnterpriseExport() { }

        public BMCEnterpriseExport(bool value)
        {
            this.Load();
        }

        private void Load()
        {
            LogManager.WriteLog("Current Directory:" + AppDomain.CurrentDomain.BaseDirectory, LogManager.enumLogLevel.Info);
            XDocument xmlDoc = XDocument.Load(AppDomain.CurrentDomain.BaseDirectory + "\\12_4_ExportList.xml");
            var lstExportType = xmlDoc.Descendants("Export").Elements().ToList();
            lstExportType.ForEach(item => _str12_4_ExportLiset.Add(item.Value));

            _appWathcer = new ConfigAppSettingWatcher();
            _appWathcer
                .Register("LimitThreadCount", (s) => { _limitThreadCount = Convert.ToBoolean(s); })
                .Register("ThreadCount", (s) => { _threadCount = Convert.ToInt32(s); })
                .Register("ThreadPoolType", (s) => { _poolType = (ThreadPoolType)(Convert.ToInt32(s)); })
                .Register("PerItemProcessMilliseconds", (s) => { _perItemProcessMilliseconds = Convert.ToInt32(s); })
                .Register("TimeIntervalToCheckForNewSite", (s) => { _timeIntervalToCheckForNewSite = (1000 * Convert.ToInt32(s)); })
                .Register("PopulateImportHistoryThreadIntervalInSecs", (s) => { _populateImportHistoryThreadIntervalInSecs = (Convert.ToInt32(s)); })
                .Register("ProxyTimeoutInSeconds", (s) =>
                {
                    int timeout = TypeSystem.GetValueInt(s);
                    if (timeout < 0) timeout = 100;
                    _proxyTimeoutInMilliseconds = (timeout * 1000);
                });
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

        void OnExecutor_ProcessItem(BMCEnterpriseExport.SiteInfo item)
        {
            ProcessRecordsForSites(item, _perItemProcessMilliseconds);
            item.Dispose();
        }

        private void ProcessRecordsForSites(SiteInfo item, int PerItemProcessInterval)
        {
            ModuleProc PROC = new ModuleProc("BMCEnterpriseExport", "ProcessRecordsForSite");

            try
            {
                while (!_executorService.WaitForShutdown(_populateImportHistoryThreadIntervalInSecs))
                {
                    try
                    {
                        if (!item.SiteCodes.IsEmpty())
                        {
                            this.ProcessRecordsForSite(item.SiteCodes, PerItemProcessInterval);
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

        private void ProcessRecordsForSite(string siteCode, int PerItemProcessInterval)
        {
            ModuleProc PROC = new ModuleProc("BMCEnterpriseExport", "ProcessRecordsForSite");
            var dataHelper = new DataHelper();
            dataHelper.ProxyTimeoutInMilliseconds = _proxyTimeoutInMilliseconds;
            DataSet objdsAllExportData;
            Log.Info(PROC, "Called for site(s) : " + siteCode);

            //11.4 Migration Code - Begins
            try
            {
                string[] strSite_Code = siteCode.Split(',');
                bool bOldSite = false;

                string[] arSkipTypes = Convert.ToString(ConfigManager.Read("SkipTypes")).Split(',');
                string sSkipStatus = Convert.ToString(ConfigManager.Read("SkipStatus"));
                bool bIs_11_4Migration = Convert.ToBoolean(ConfigManager.Read("Is_11_4_Migration"));
                foreach (string sSite_Code in strSite_Code)
                {
                    
                    if (bIs_11_4Migration)
                    {
                        string strWebURL = dataHelper.GetVersion(sSite_Code.ToString());

                        if (string.IsNullOrEmpty(strWebURL))
                        {
                            LogManager.WriteLog("Site Web URL is empty for the Site" + sSite_Code.ToString(), LogManager.enumLogLevel.Error);
                            continue;
                        }

                        if (strWebURL.ToUpper().Contains("BGSWSADMIN"))
                        {
                            LogManager.WriteLog("Processing 11.4 Site" + sSite_Code.ToString(), LogManager.enumLogLevel.Info);
                            ExportDataToExchange_114(PerItemProcessInterval);
                            continue;
                        }
                    }

                    //11.4 Migration Code - Ends

                    objdsAllExportData = dataHelper.GetAllExportDataForSite(sSite_Code);

                    if (objdsAllExportData == null || objdsAllExportData.Tables[0].Rows.Count <= 0)
                    {
                        LogManager.WriteLog("No Records to Export for Site Code: " + sSite_Code, LogManager.enumLogLevel.Error);

                        if (objdsAllExportData != null)
                            LogManager.WriteLog("The record count should be 0-" + objdsAllExportData.Tables[0].Rows.Count.ToString(), LogManager.enumLogLevel.Info);
                    }

                    foreach (DataRow row in objdsAllExportData.Tables[0].Rows)
                    {
                        if (_ServiceStopped.WaitOne(PerItemProcessInterval))
                        {
                            break;
                        }

                        try
                        {
                            Log.InfoV(PROC, "Processing Export Item : {0}, for Site : {1}",
                                row[Constants.CONSTANT_COL_EH_ID].ToString(), row["EH_Site_Code"].ToString());
                        }
                        catch { }

                        #region Skip records
                        //Introduced for 12.1.1 SP8 EP4 migration to skip new export type records to non migrated sites.

                        bOldSite = row["EH_Skip"].ToString() == "1" || row["EH_Skip"].ToString().ToUpper() == "TRUE" ? true : false;
                        if (bOldSite
                            && arSkipTypes.Contains(row[Constants.CONSTANT_COL_EH_TYPE].ToString().ToUpper().Trim()))
                        {
                            dataHelper.UpdateExportHistoryTableWithStatus(Convert.ToInt32(row[Constants.CONSTANT_COL_EH_ID]), sSkipStatus);
                            Log.InfoV(PROC, "Skipped Export Item : {0}, for Site : {1}",
                                row[Constants.CONSTANT_COL_EH_ID].ToString(), row["EH_Site_Code"].ToString());
                            continue;
                        }
                        #endregion Skip records

                        switch (row[Constants.CONSTANT_COL_EH_TYPE].ToString().ToUpper().Trim())
                        {
                            case "O-CALENDAR":
                            case "S-CALENDAR":
                                dataHelper.GetImportCalendar(row[Constants.CONSTANT_COL_EH_REFERENCE].ToString(),
                                                      Convert.ToInt32(row[Constants.CONSTANT_COL_EH_ID]),
                                                      row["EH_Site_Code"].ToString(),
                                                      row[Constants.CONSTANT_COL_EH_TYPE].ToString());
                                break;
                            case "STACKER":
                                dataHelper.ExportStackerDetails(Convert.ToInt32(row[Constants.CONSTANT_COL_EH_REFERENCE].ToString()),
                                                       Convert.ToInt32(row[Constants.CONSTANT_COL_EH_ID]),
                                                       row["EH_Site_Code"].ToString());
                                break;
                            ////////// 
                            case "SHAREHOLDER":
                                dataHelper.ExportShareHolderDetails(Convert.ToInt32(row[Constants.CONSTANT_COL_EH_REFERENCE].ToString()),
                                                       Convert.ToInt32(row[Constants.CONSTANT_COL_EH_ID]),
                                                       row["EH_Site_Code"].ToString());
                                break;

                            case "EXPENSESHARE":
                                dataHelper.ExportExpenseShareDetails(Convert.ToInt32(row[Constants.CONSTANT_COL_EH_REFERENCE].ToString()),
                                                       Convert.ToInt32(row[Constants.CONSTANT_COL_EH_ID]),
                                                       row["EH_Site_Code"].ToString());
                                break;

                            case "PROFITSHARE":
                                dataHelper.ExportProfitShareDetails(Convert.ToInt32(row[Constants.CONSTANT_COL_EH_REFERENCE].ToString()),
                                                       Convert.ToInt32(row[Constants.CONSTANT_COL_EH_ID]),
                                                       row["EH_Site_Code"].ToString());
                                break;
                            case "PROFITSHAREGROUP":
                                dataHelper.ExportProfitShareGroupDetails(Convert.ToInt32(row[Constants.CONSTANT_COL_EH_REFERENCE].ToString()),
                                                       Convert.ToInt32(row[Constants.CONSTANT_COL_EH_ID]),
                                                       row["EH_Site_Code"].ToString());
                                break;
                            case "EXPENSESHAREGROUP":
                                dataHelper.ExportExpenseShareGroupDetails(Convert.ToInt32(row[Constants.CONSTANT_COL_EH_REFERENCE].ToString()),
                                                       Convert.ToInt32(row[Constants.CONSTANT_COL_EH_ID]),
                                                       row["EH_Site_Code"].ToString());
                                break;
                            case "LIQUIDATIONDETAILS":
                                dataHelper.ExportLiquidationDetails(Convert.ToInt32(row[Constants.CONSTANT_COL_EH_REFERENCE].ToString()),
                                                       Convert.ToInt32(row[Constants.CONSTANT_COL_EH_ID]),
                                                       row["EH_Site_Code"].ToString());
                                break;
                            case "LIQUIDATIONSHAREDETAILS":
                                dataHelper.ExportLiquidationShareDetails(Convert.ToInt32(row[Constants.CONSTANT_COL_EH_REFERENCE].ToString()),
                                                       Convert.ToInt32(row[Constants.CONSTANT_COL_EH_ID]),
                                                       row["EH_Site_Code"].ToString());
                                break;
                            ///////

                            case "MODEL":
                                dataHelper.GetImportModel(row[Constants.CONSTANT_COL_EH_REFERENCE].ToString(),
                                                       Convert.ToInt32(row[Constants.CONSTANT_COL_EH_ID]),
                                                       row["EH_Site_Code"].ToString());
                                break;
                            case "SITESETUP":
                                dataHelper.GetImportSite(row[Constants.CONSTANT_COL_EH_REFERENCE].ToString(),
                                                      Convert.ToInt32(row[Constants.CONSTANT_COL_EH_ID]),
                                                      row["EH_Site_Code"].ToString());
                                break;
                            case "CROSSTICKETING":
                                LogManager.WriteLog("Inside Cross Ticketing Case", LogManager.enumLogLevel.Info);
                                dataHelper.GetImportCrossTicketing(Convert.ToInt32(row[Constants.CONSTANT_COL_EH_ID]),
                                                      row["EH_Site_Code"].ToString());
                                break;
                            case "NOTEACCEPTORENABLE":
                                dataHelper.ExportMachineNoteAcceptorEnableDisableStatus(Convert.ToInt32(row[Constants.CONSTANT_COL_EH_ID]), true);
                                break;
                            case "NOTEACCEPTORDISABLE":
                                dataHelper.ExportMachineNoteAcceptorEnableDisableStatus(Convert.ToInt32(row[Constants.CONSTANT_COL_EH_ID])
                                    , false);
                                break;
                            case "MACHINEENABLE":
                                dataHelper.ExportMachineEnableStatus(Convert.ToInt32(row[Constants.CONSTANT_COL_EH_ID]), true);
                                break;
                            case "MACHINEDISABLE":
                                dataHelper.ExportMachineEnableStatus(Convert.ToInt32(row[Constants.CONSTANT_COL_EH_ID]), false);
                                break;
                            case "GETCOLLBYDATE":
                                dataHelper.SetCollectionByDateBarPositions(
                                    row[Constants.CONSTANT_COL_EH_REFERENCE].ToString(),
                                    row[Constants.CONSTANT_COL_EH_SITE_CODE].ToString(),
                                    Convert.ToInt32(row[Constants.CONSTANT_COL_EH_ID]));
                                break;

                            case "SITESETTINGS":
                                dataHelper.GetImportSettings(row[Constants.CONSTANT_COL_EH_REFERENCE].ToString(),
                                                   int.Parse(row[Constants.CONSTANT_COL_EH_ID].ToString()),
                                                   row[Constants.CONSTANT_COL_EH_SITE_CODE].ToString());
                                break;

                            case "AFTENABLEDISABLE":
                                LogManager.WriteLog("Site " + row[Constants.CONSTANT_COL_EH_SITE_CODE].ToString(), LogManager.enumLogLevel.Info);
                                LogManager.WriteLog("AFT Status " + dataHelper.GetAFTStatus(row[Constants.CONSTANT_COL_EH_SITE_CODE].ToString()).ToString(), LogManager.enumLogLevel.Info);
                                //if (dataHelper.GetAFTStatus(row[Constants.CONSTANT_COL_EH_SITE_CODE].ToString()) == 0)
                                //{
                                LogManager.WriteLog("Send Enable/Disable", LogManager.enumLogLevel.Info);
                                dataHelper.GetImportAFTInfo(int.Parse(row[Constants.CONSTANT_COL_EH_REFERENCE].ToString()),
                                    int.Parse(row[Constants.CONSTANT_COL_EH_ID].ToString()),
                                                       row[Constants.CONSTANT_COL_EH_SITE_CODE].ToString());
                                // }
                                break;
                            case "AFTSETTINGS":
                                dataHelper.GetAFTSettings(int.Parse(row[Constants.CONSTANT_COL_EH_REFERENCE].ToString()),
                                   int.Parse(row[Constants.CONSTANT_COL_EH_ID].ToString()),
                                                      row[Constants.CONSTANT_COL_EH_SITE_CODE].ToString());
                                break;
                            case "MASTERCARDENABLE":
                                dataHelper.GetImportMasterEmployeeCardInfo(row[Constants.CONSTANT_COL_EH_REFERENCE].ToString(),
                                   int.Parse(row[Constants.CONSTANT_COL_EH_ID].ToString()),
                                                      row[Constants.CONSTANT_COL_EH_SITE_CODE].ToString());
                                break;

                            case "RESETMASTERCARDFLAG":

                                dataHelper.GetMasterCardtobeReset(row[Constants.CONSTANT_COL_EH_REFERENCE].ToString(),
                                  int.Parse(row[Constants.CONSTANT_COL_EH_ID].ToString()),
                                                     row[Constants.CONSTANT_COL_EH_SITE_CODE].ToString());
                                break;
                            case "ADDUSER":
                            case "UPDATEUSER":
                            case "TERMINATEUSER":
                                dataHelper.GetImportUser(int.Parse(row[Constants.CONSTANT_COL_EH_REFERENCE].ToString()), int.Parse(row[Constants.CONSTANT_COL_EH_ID].ToString()),
                                                   row[Constants.CONSTANT_COL_EH_SITE_CODE].ToString(), "ADDUSER", bOldSite);
                                break;
                            case "REMOVEUSER":
                                dataHelper.GetImportUser(int.Parse(row[Constants.CONSTANT_COL_EH_REFERENCE].ToString()), int.Parse(row[Constants.CONSTANT_COL_EH_ID].ToString()),
                                                   row[Constants.CONSTANT_COL_EH_SITE_CODE].ToString(), "REMOVEUSER", bOldSite);
                                break;
                            case "USERROLE":
                                dataHelper.GetImportUserRoles(int.Parse(row[Constants.CONSTANT_COL_EH_REFERENCE].ToString()), int.Parse(row[Constants.CONSTANT_COL_EH_ID].ToString()),
                                                   row[Constants.CONSTANT_COL_EH_SITE_CODE].ToString());
                                break;
                            case "ROLEACCESSLINK":
                                dataHelper.GetImportRoleAccessLinks(int.Parse(row[Constants.CONSTANT_COL_EH_REFERENCE].ToString()), int.Parse(row[Constants.CONSTANT_COL_EH_ID].ToString()),
                                                   row[Constants.CONSTANT_COL_EH_SITE_CODE].ToString());
                                break;
                            case "AUTOINSTALLATION":
                                dataHelper.GetImportAutoInstallation(int.Parse(row[Constants.CONSTANT_COL_EH_REFERENCE].ToString()), int.Parse(row[Constants.CONSTANT_COL_EH_ID].ToString()),
                                                   row[Constants.CONSTANT_COL_EH_SITE_CODE].ToString());
                                break;
                            case "AAMSCONFIG":
                                dataHelper.GetAAMSConfigRecord(int.Parse(row[Constants.CONSTANT_COL_EH_REFERENCE].ToString()), int.Parse(row[Constants.CONSTANT_COL_EH_ID].ToString()),
                                                   row[Constants.CONSTANT_COL_EH_SITE_CODE].ToString());
                                break;
                            case "GAMEINFO":
                                dataHelper.GetImportGameInfo(int.Parse(row[Constants.CONSTANT_COL_EH_REFERENCE].ToString()), int.Parse(row[Constants.CONSTANT_COL_EH_ID].ToString()),
                                                  row[Constants.CONSTANT_COL_EH_SITE_CODE].ToString());
                                break;
                            case "GAMELIBRARY":
                                dataHelper.ExportGameLibrary(int.Parse(row[Constants.CONSTANT_COL_EH_REFERENCE].ToString()), int.Parse(row[Constants.CONSTANT_COL_EH_ID].ToString()),
                                                  row[Constants.CONSTANT_COL_EH_SITE_CODE].ToString());
                                break;
                            case "GAMECRC":
                                dataHelper.ExportGameCRC(int.Parse(row[Constants.CONSTANT_COL_EH_REFERENCE].ToString()), int.Parse(row[Constants.CONSTANT_COL_EH_ID].ToString()),
                                                  row[Constants.CONSTANT_COL_EH_SITE_CODE].ToString());
                                break;
                            case "REMOVECRC":
                                dataHelper.ExportRemoveCRC(row[Constants.CONSTANT_COL_EH_REFERENCE].ToString(), int.Parse(row[Constants.CONSTANT_COL_EH_ID].ToString()),
                                                  row[Constants.CONSTANT_COL_EH_SITE_CODE].ToString());
                                break;
                            case "CHANGEPASSWORD":
                                dataHelper.ExportPasswordChange(int.Parse(row[Constants.CONSTANT_COL_EH_REFERENCE].ToString()), int.Parse(row[Constants.CONSTANT_COL_EH_ID].ToString()),
                                                  row[Constants.CONSTANT_COL_EH_SITE_CODE].ToString());
                                break;
                            case "CODEMASTER":
                                dataHelper.ExportCodeMaster(int.Parse(row[Constants.CONSTANT_COL_EH_REFERENCE].ToString()), int.Parse(row[Constants.CONSTANT_COL_EH_ID].ToString()),
                                                  row[Constants.CONSTANT_COL_EH_SITE_CODE].ToString());
                                break;
                            case "LOOKUPMASTER":
                                dataHelper.ExportLookupMaster(int.Parse(row[Constants.CONSTANT_COL_EH_REFERENCE].ToString()), int.Parse(row[Constants.CONSTANT_COL_EH_ID].ToString()),
                                                  row[Constants.CONSTANT_COL_EH_SITE_CODE].ToString());
                                break;
                            case "LANGUAGELOOKUP":
                                dataHelper.ExportLanguageLookup(int.Parse(row[Constants.CONSTANT_COL_EH_REFERENCE].ToString()), int.Parse(row[Constants.CONSTANT_COL_EH_ID].ToString()),
                                                  row[Constants.CONSTANT_COL_EH_SITE_CODE].ToString());
                                break;
                            case "MACHINECOMPDETAILS":
                                dataHelper.ExportMachineComponentDetails(int.Parse(row[Constants.CONSTANT_COL_EH_REFERENCE].ToString()), int.Parse(row[Constants.CONSTANT_COL_EH_ID].ToString()),
                                                  row[Constants.CONSTANT_COL_EH_SITE_CODE].ToString());
                                break;
                            case "COMPONENTDETAILS":
                                dataHelper.ExportComponentDetails(int.Parse(row[Constants.CONSTANT_COL_EH_REFERENCE].ToString()), int.Parse(row[Constants.CONSTANT_COL_EH_ID].ToString()),
                                                  row[Constants.CONSTANT_COL_EH_SITE_CODE].ToString());
                                break;
                            case "ONDEMANDVERIFICATION":
                                dataHelper.ExportOnDemandDetails(row[Constants.CONSTANT_COL_EH_REFERENCE].ToString(), int.Parse(row[Constants.CONSTANT_COL_EH_ID].ToString()),
                                                  row[Constants.CONSTANT_COL_EH_SITE_CODE].ToString());
                                break;
                            case "GAMECATEGORY":
                                dataHelper.ExportGameCategory(int.Parse(row[Constants.CONSTANT_COL_EH_REFERENCE].ToString()), int.Parse(row[Constants.CONSTANT_COL_EH_ID].ToString()),
                                                  row[Constants.CONSTANT_COL_EH_SITE_CODE].ToString());
                                break;
                            case "GAMETITLE":
                                dataHelper.ExportGameTitle(int.Parse(row[Constants.CONSTANT_COL_EH_REFERENCE].ToString()), int.Parse(row[Constants.CONSTANT_COL_EH_ID].ToString()),
                                                  row[Constants.CONSTANT_COL_EH_SITE_CODE].ToString());
                                break;
                            case "PAYTABLE":
                                dataHelper.ExportPaytable(int.Parse(row[Constants.CONSTANT_COL_EH_REFERENCE].ToString()), int.Parse(row[Constants.CONSTANT_COL_EH_ID].ToString()),
                                                  row[Constants.CONSTANT_COL_EH_SITE_CODE].ToString());
                                break;
                            case "GAMELIBRARY_MAPPING":
                                dataHelper.ExportGameLibraryMapping(int.Parse(row[Constants.CONSTANT_COL_EH_REFERENCE].ToString()), int.Parse(row[Constants.CONSTANT_COL_EH_ID].ToString()),
                                                  row[Constants.CONSTANT_COL_EH_SITE_CODE].ToString());
                                break;
                            case "MACHINEUPDATE":
                                dataHelper.ExportMachineUpdateDetails(int.Parse(row[Constants.CONSTANT_COL_EH_REFERENCE].ToString()), int.Parse(row[Constants.CONSTANT_COL_EH_ID].ToString()),
                                                  row[Constants.CONSTANT_COL_EH_SITE_CODE].ToString());
                                break;
                            case "MANUFACTURER_DETAILS":
                                dataHelper.ExportManufacturerDetails(int.Parse(row[Constants.CONSTANT_COL_EH_REFERENCE].ToString()), int.Parse(row[Constants.CONSTANT_COL_EH_ID].ToString()),
                                                  row[Constants.CONSTANT_COL_EH_SITE_CODE].ToString());
                                break;
                            case "ENABLESITE":
                                dataHelper.UpdateSiteEnabledStatus(row[Constants.CONSTANT_COL_EH_REFERENCE].ToString(), int.Parse(row[Constants.CONSTANT_COL_EH_ID].ToString()),
                                                  row[Constants.CONSTANT_COL_EH_SITE_CODE].ToString(), "ENABLESITE");
                                break;
                            case "DISABLESITE":
                                dataHelper.UpdateSiteEnabledStatus(row[Constants.CONSTANT_COL_EH_REFERENCE].ToString(), int.Parse(row[Constants.CONSTANT_COL_EH_ID].ToString()),
                                                  row[Constants.CONSTANT_COL_EH_SITE_CODE].ToString(), "DISABLESITE");
                                break;
                            case "CMPGAMETYPE":
                                dataHelper.ExportCMPGameTypes(int.Parse(row[Constants.CONSTANT_COL_EH_REFERENCE].ToString()), int.Parse(row[Constants.CONSTANT_COL_EH_ID].ToString()),
                                                  row[Constants.CONSTANT_COL_EH_SITE_CODE].ToString());
                                break;
                            case "DECCOLLBATCH":
                                dataHelper.GetExportCollectionBatch(row[Constants.CONSTANT_COL_EH_REFERENCE].ToString(), row[Constants.CONSTANT_COL_EH_ID].ToString(), row[Constants.CONSTANT_COL_EH_SITE_CODE].ToString());
                                break;
                            case "SITELICENSING":
                                LogManager.WriteLog("Site Licensing", LogManager.enumLogLevel.Debug);
                                LogManager.WriteLog("Site Licensing " + row[Constants.CONSTANT_COL_EH_REFERENCE].ToString() + row[Constants.CONSTANT_COL_EH_ID].ToString()
                                                   + row[Constants.CONSTANT_COL_EH_SITE_CODE].ToString(), LogManager.enumLogLevel.Info);
                                dataHelper.ExportLicenseInfo(int.Parse(row[Constants.CONSTANT_COL_EH_REFERENCE].ToString()), int.Parse(row[Constants.CONSTANT_COL_EH_ID].ToString()),
                                                  row[Constants.CONSTANT_COL_EH_SITE_CODE].ToString());

                                break;
                            case "ACTIVELICENSE":
                                LogManager.WriteLog("Active License", LogManager.enumLogLevel.Debug);
                                LogManager.WriteLog("Site Licensing " + row[Constants.CONSTANT_COL_EH_REFERENCE].ToString() + row[Constants.CONSTANT_COL_EH_ID].ToString()
                                                  + row[Constants.CONSTANT_COL_EH_SITE_CODE].ToString(), LogManager.enumLogLevel.Info);
                                dataHelper.ActiveLicense(int.Parse(row[Constants.CONSTANT_COL_EH_REFERENCE].ToString()), int.Parse(row[Constants.CONSTANT_COL_EH_ID].ToString()),
                                                   row[Constants.CONSTANT_COL_EH_SITE_CODE].ToString());

                                break;
                            case "USERDETAILS":
                                dataHelper.GetUserDetails(int.Parse(row[Constants.CONSTANT_COL_EH_REFERENCE].ToString()),
                                   int.Parse(row[Constants.CONSTANT_COL_EH_ID].ToString()),
                                                      row[Constants.CONSTANT_COL_EH_SITE_CODE].ToString());
                                break;
                            case "ROUTE":
                                LogManager.WriteLog("Route", LogManager.enumLogLevel.Debug);
                                dataHelper.ExportRoute(row[Constants.CONSTANT_COL_EH_SITE_CODE].ToString(), int.Parse(row[Constants.CONSTANT_COL_EH_ID].ToString()));
                                break;
                            case "VAULTDEVICE":
                                dataHelper.ExportVaultDetails(row[Constants.CONSTANT_COL_EH_REFERENCE].ToString(), Convert.ToInt32(row[Constants.CONSTANT_COL_EH_ID].ToString()), row[Constants.CONSTANT_COL_EH_SITE_CODE].ToString());
                                break;
                            case "GAMECAPPING":
                                 dataHelper.GetGameCappingParameters(int.Parse(row[Constants.CONSTANT_COL_EH_REFERENCE].ToString()),int.Parse(row[Constants.CONSTANT_COL_EH_ID].ToString()),row[Constants.CONSTANT_COL_EH_SITE_CODE].ToString());
                                break;
                            case "FACTORYRESET_STATUS":
                                dataHelper.ExportResetStatus(row[Constants.CONSTANT_COL_EH_REFERENCE].ToString(), Convert.ToInt32(row[Constants.CONSTANT_COL_EH_ID].ToString()), row[Constants.CONSTANT_COL_EH_SITE_CODE].ToString());
                                break;
                            case "VAULTDROP":
                                dataHelper.ExportVaultDrop(int.Parse(row[Constants.CONSTANT_COL_EH_REFERENCE].ToString()), Convert.ToInt32(row[Constants.CONSTANT_COL_EH_ID].ToString()), row[Constants.CONSTANT_COL_EH_SITE_CODE].ToString());
                                break;
                            case "VAULTTRANSACTIONREASON":
                                dataHelper.ExportVaultTransactionReason(int.Parse(row[Constants.CONSTANT_COL_EH_REFERENCE].ToString()), Convert.ToInt32(row[Constants.CONSTANT_COL_EH_ID].ToString()), row[Constants.CONSTANT_COL_EH_SITE_CODE].ToString());
                                break;
                            case "TERMINATEVAULT":
                                dataHelper.ExportVaultTermination(int.Parse(row[Constants.CONSTANT_COL_EH_REFERENCE].ToString()), Convert.ToInt32(row[Constants.CONSTANT_COL_EH_ID].ToString()), row[Constants.CONSTANT_COL_EH_SITE_CODE].ToString());
                                break;
                            case "EMPGMUMODES":
                                dataHelper.ExportEmpGMUModes(int.Parse(row[Constants.CONSTANT_COL_EH_REFERENCE].ToString()), Convert.ToInt32(row[Constants.CONSTANT_COL_EH_ID].ToString()), row[Constants.CONSTANT_COL_EH_SITE_CODE].ToString());
                                break;
                            case "EMPGMUEVENTS":
                                dataHelper.ExportEmpGMUEvents(int.Parse(row[Constants.CONSTANT_COL_EH_REFERENCE].ToString()), Convert.ToInt32(row[Constants.CONSTANT_COL_EH_ID].ToString()), row[Constants.CONSTANT_COL_EH_SITE_CODE].ToString());
                                break;
                            case "MAILSUBSCRIBERS":
                                dataHelper.ExportMailSubscribers(int.Parse(row[Constants.CONSTANT_COL_EH_REFERENCE].ToString()), Convert.ToInt32(row[Constants.CONSTANT_COL_EH_ID].ToString()), row[Constants.CONSTANT_COL_EH_SITE_CODE].ToString());
                                break;
                            case "MAILSERVERINFO":
                                dataHelper.ExportMailServerInfo(int.Parse(row[Constants.CONSTANT_COL_EH_REFERENCE].ToString()), Convert.ToInt32(row[Constants.CONSTANT_COL_EH_ID].ToString()), row[Constants.CONSTANT_COL_EH_SITE_CODE].ToString());
                                break;
                            case "DELETEZONE":
                                dataHelper.ExportZoneInfo(int.Parse(row[Constants.CONSTANT_COL_EH_REFERENCE].ToString()), Convert.ToInt32(row[Constants.CONSTANT_COL_EH_ID].ToString()), row[Constants.CONSTANT_COL_EH_SITE_CODE].ToString());
                                break;
                            default:
                                break;
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
                // Reset the intermediate records
                dataHelper.UpdateInProgressEHForSites(siteCode);
            }
        }

        //11.4 Migration Code - Begins

        private bool ExportDataToExchange_114(int PerItemProcessInterval)
        {
            string methodName = "IBMCEnterpriseExportImport.ExportDataToExchange";

            try
            {
                //Get All the Data that needs to be Exported
                DataSet exportData = BMCEnterpriseExportImport114.GetAllExportData();
                var dataHelper = new DataHelper();

                //If there are no records to Export,gracefully exit from method by writing a log statment
                if ((exportData == null) || (exportData.Tables[0].Rows.Count <= 0))
                {
                    LogManager.WriteLog(methodName + "::" + "No Records to Export", LogManager.enumLogLevel.Error);
                    return true;
                }
                foreach (DataRow row in exportData.Tables[0].Rows)
                {
                    if (_ServiceStopped.WaitOne(PerItemProcessInterval))
                    {
                        break;
                    }

                    switch (row[Constants.CONSTANT_COL_EH_TYPE].ToString().ToUpper())
                    {
                        case "O-CALENDAR":
                        case "S-CALENDAR":
                            if (ExportCalendar(row[Constants.CONSTANT_COL_EH_REFERENCE].ToString(), Convert.ToInt32(row[Constants.CONSTANT_COL_EH_ID]), row["EH_Site_Code"].ToString(), row[Constants.CONSTANT_COL_EH_TYPE].ToString()))
                                LogManager.WriteLog(String.Format("{0}::EXPORTED CALENDAR DATA SUCCESSFULLY ::", methodName), LogManager.enumLogLevel.Error);
                            break;
                        case "MODEL":
                            if (ExportModel(row[Constants.CONSTANT_COL_EH_REFERENCE].ToString(), Convert.ToInt32(row[Constants.CONSTANT_COL_EH_ID]), row["EH_Site_Code"].ToString()))
                                LogManager.WriteLog(String.Format("{0}::EXPORTED MODEL DATA SUCCESSFULLY ::", methodName), LogManager.enumLogLevel.Error);
                            break;
                        case "SCHEDULEPROFILE":
                            if (ExportScheduleProfileExportModel(row[Constants.CONSTANT_COL_EH_REFERENCE].ToString(), Convert.ToInt32(row[Constants.CONSTANT_COL_EH_ID]), row["EH_Site_Code"].ToString()))
                                LogManager.WriteLog(String.Format("{0}::EXPORTED Schedule DATA SUCCESSFULLY ::", methodName), LogManager.enumLogLevel.Error);
                            break;
                        case "SITESETUP":
                            if (ExportSite(row[Constants.CONSTANT_COL_EH_REFERENCE].ToString(), Convert.ToInt32(row[Constants.CONSTANT_COL_EH_ID]), row["EH_Site_Code"].ToString()))
                                LogManager.WriteLog(String.Format("{0}::EXPORTED SITE DATA SUCCESSFULLY ::", methodName), LogManager.enumLogLevel.Error);
                            break;
                        //Newly added for machine control.
                        case "NOTEACCEPTORENABLE":
                            //call the method to enable noteacceptor.
                            if (ExportBarPositionsForNoteAcceptorEnable(Convert.ToInt32(row[Constants.CONSTANT_COL_EH_ID])))
                                LogManager.WriteLog(String.Format("{0}::NOTE ACCEPTOR ENABLED SUCCESSFULLY ::", methodName), LogManager.enumLogLevel.Error);
                            break;
                        case "NOTEACCEPTORDISABLE":
                            //call the method to disable noteacceptor.
                            if (ExportBarPositionsForNoteAcceptorDisable(Convert.ToInt32(row[Constants.CONSTANT_COL_EH_ID])))
                                LogManager.WriteLog(String.Format("{0}:: NOTE ACCEPTOR DISABLED SUCCESSFULLY ::", methodName), LogManager.enumLogLevel.Error);
                            break;

                        case "MACHINEENABLE":
                            //call the method to enable machines.
                            if (ExportBarPositionsForMachineEnable(Convert.ToInt32(row[Constants.CONSTANT_COL_EH_ID])))
                                LogManager.WriteLog(String.Format("{0}::MACHINE ENABLED SUCCESSFULLY ::", methodName), LogManager.enumLogLevel.Error);
                            break;
                        case "MACHINEDISABLE":
                            //call the method to disable machines.
                            if (ExportBarPositionsForMachineDisable(Convert.ToInt32(row[Constants.CONSTANT_COL_EH_ID])))
                                LogManager.WriteLog(String.Format("{0}::MACHINE DISABLED SUCCESSFULLY ::", methodName), LogManager.enumLogLevel.Error);
                            break;
                        case "GETCOLLBYDATE":
                            if (ExportBarPositionsForCollectionsByDate(row[Constants.CONSTANT_COL_EH_REFERENCE].ToString(), row[Constants.CONSTANT_COL_EH_SITE_CODE].ToString(), Convert.ToInt32(row[Constants.CONSTANT_COL_EH_ID])))
                                LogManager.WriteLog(String.Format("{0}::EXPORTED COLLECTION BY DATE DATA SUCCESSFULLY ::", methodName), LogManager.enumLogLevel.Error);
                            break;
                        case "EDITCOLLECTION":
                            if (ExportEditedCollectionsToExchange(row[Constants.CONSTANT_COL_EH_REFERENCE].ToString(), row[Constants.CONSTANT_COL_EH_SITE_CODE].ToString(), Convert.ToInt32(row[Constants.CONSTANT_COL_EH_ID])))
                                LogManager.WriteLog(String.Format("{0}::EXPORTED EDIT COLLECTION DATA SUCCESSFULLY ::", methodName), LogManager.enumLogLevel.Error);
                            break;

                        case "SCHEDULEJOBS":
                            if (ExportScheduleJobs(int.Parse(row[Constants.CONSTANT_COL_EH_ID].ToString()), int.Parse(row[Constants.CONSTANT_COL_EH_REFERENCE].ToString()), row[Constants.CONSTANT_COL_EH_SITE_CODE].ToString()))
                                LogManager.WriteLog(String.Format("{0}::EXPORTED SCHEDULE JOBS DATA SUCCESSFULLY ::", methodName), LogManager.enumLogLevel.Error);
                            break;
                        default:
                            _str12_4_ExportLiset.ForEach(item => {
                                                                    if (item.ToUpper().Trim().Equals(row[Constants.CONSTANT_COL_EH_TYPE].ToString().ToUpper()))
                                                                    {
                                                                        LogManager.WriteLog(String.Format("{0}::Skipped to export since 11.4 site ::", row[Constants.CONSTANT_COL_EH_TYPE].ToString()), LogManager.enumLogLevel.Info);
                                                                        dataHelper.UpdateExportHistoryTableWithStatus(Convert.ToInt32(row[Constants.CONSTANT_COL_EH_ID]), "100");
                                                                    }

                                                                    _ServiceStopped.WaitOne(PerItemProcessInterval);
                                                                 });
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return false;
            }
            return true;
        }

        /// <summary>
        /// This method checks for any Calendar that
        /// needs to be uploaded to Database.
        /// </summary>
        /// <returns>a boolean value. Whether are not a calendar upload has happened</returns>
        private bool ExportCalendar(string siteID, int exportHistoryID, string siteCode, string calendarType)
        {
            try
            {
                BMCEnterpriseExportImport114.GetImportCalendar(siteID, exportHistoryID, siteCode, calendarType);
                LogManager.WriteLog("Export Calendar Data", LogManager.enumLogLevel.Debug);
                return true;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return false;
            }
        }

        /// <summary>
        /// This method checks for any Calendar that
        /// needs to be uploaded to Database.
        /// </summary>
        /// <returns>a boolean value. Whether are not a calendar upload has happened</returns>
        private bool ExportModel(string modelID, int exportHistoryID, string siteCode)
        {
            try
            {
                BMCEnterpriseExportImport114.GetImportModel(modelID, exportHistoryID, siteCode);
                LogManager.WriteLog("Export Model Data", LogManager.enumLogLevel.Debug);
                return true;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelID"></param>
        /// <param name="exportHistoryID"></param>
        /// <param name="siteCode"></param>
        /// <returns></returns>
        private bool ExportScheduleProfileExportModel(string modelID, int exportHistoryID, string siteCode)
        {
            return false;
        }

        /// <summary>
        /// This method checks for any Calendar that
        /// needs to be uploaded to Database.
        /// </summary>
        /// <returns>a boolean value. Whether are not a calendar upload has happened</returns>
        private bool ExportSite(string siteID, int exportHistoryID, string siteCode)
        {
            try
            {
                BMCEnterpriseExportImport114.GetImportSite(siteID, exportHistoryID, siteCode);
                LogManager.WriteLog("Export Site Data", LogManager.enumLogLevel.Debug);
                return true;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return false;
            }
        }

        /// <summary>
        /// This method gets the bar position ids for note acceptor disable from export history 
        /// that needs to be exported to exchange and calls the webservice to disable the
        /// noteacceptor in exchange.
        /// <returns>a boolean value returning the status of process</returns>
        private bool ExportBarPositionsForNoteAcceptorEnable(int exportHistoryID)
        {
            try
            {
                LogManager.WriteLog(String.Format("Calling NoteAcceptorEnable Command.... for EH_ID: {0}", exportHistoryID), LogManager.enumLogLevel.Info);
                if (BMCEnterpriseExportImport114.ExportMachineNoteAcceptorEnableDisableStatus(exportHistoryID, true))
                    LogManager.WriteLog("Note Acceptor Enabled", LogManager.enumLogLevel.Debug);
                return true;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return false;
            }
        }

        /// <summary>
        /// This method gets the bar position ids for note acceptor disable from export history 
        /// that needs to be exported to exchange and calls the webservice to disable the
        /// noteacceptor in exchange.
        /// </summary>
        /// <Author>Anuradha</Author>
        /// <DateCreated>13-June-2008</DateCreated>
        /// <returns>a boolean value returning the status of process</returns>
        ///
        /// Revision History
        ///
        /// Author             Date              Description
        /// ---------------------------------------------------
        /// 
        private bool ExportBarPositionsForNoteAcceptorDisable(int exportHistoryID)
        {
            try
            {
                LogManager.WriteLog(String.Format("Calling NoteAcceptorDisableable Command.... for exportHistoryID: {0}", exportHistoryID), LogManager.enumLogLevel.Info);
                if (BMCEnterpriseExportImport114.ExportMachineNoteAcceptorEnableDisableStatus(exportHistoryID, false))
                    LogManager.WriteLog("Note Acceptor Disabled", LogManager.enumLogLevel.Debug);
                return true;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exportHistoryID"></param>
        /// <returns></returns>
        private bool ExportBarPositionsForMachineEnable(int exportHistoryID)
        {
            try
            {
                LogManager.WriteLog(String.Format("Calling MachineDisable Command.... for EH_ID: {0}", exportHistoryID), LogManager.enumLogLevel.Info);
                if (BMCEnterpriseExportImport114.ExportMachineEnableStatus(exportHistoryID, true))
                {
                    BMCEnterpriseExportImport114.UpdateExportHistoryTableWithStatus(exportHistoryID, "100", null);
                    return true;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                BMCEnterpriseExportImport114.UpdateExportHistoryTableWithStatus(exportHistoryID, "-1", null);
            }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exportHistoryID"></param>
        /// <returns></returns>
        private bool ExportBarPositionsForMachineDisable(int exportHistoryID)
        {
            try
            {
                LogManager.WriteLog(String.Format("Calling MachineDisable Command.... for EH_ID: {0}", exportHistoryID), LogManager.enumLogLevel.Info);
                if (BMCEnterpriseExportImport114.ExportMachineEnableStatus(exportHistoryID, false))
                {
                    BMCEnterpriseExportImport114.UpdateExportHistoryTableWithStatus(exportHistoryID, "100", null);
                    return true;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                BMCEnterpriseExportImport114.UpdateExportHistoryTableWithStatus(exportHistoryID, "-1", null);
            }
            return false;
        }

        /// <summary>
        /// This method sends the required information for Collections by date, from export history 
        /// that needs to be exported to exchange and calls the webservice to update the same in exchange.
        /// </summary>
        private bool ExportBarPositionsForCollectionsByDate(string collectionByDateDetails, string siteCode, int exportHistoryID)
        {
            try
            {
                return BMCEnterpriseExportImport114.SetCollectionByDateBarPositions(collectionByDateDetails, siteCode, exportHistoryID);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return false;
        }

        /// <summary>
        /// This method sends the required information for Edited Collections , from export history 
        /// that needs to be exported back to exchange and calls the webservice to update the same in exchange.
        /// </summary>
        private bool ExportEditedCollectionsToExchange(string referenceID, string siteCode, int exportHistoryID)
        {
            try
            {
                if (BMCEnterpriseExportImport114.ImportEditCollectionToExchange(Convert.ToInt64(referenceID), siteCode, exportHistoryID))
                {
                    LogManager.WriteLog("Edited Collection exported to Exchange .", LogManager.enumLogLevel.Info);
                    return true;
                }
                else
                {
                    LogManager.WriteLog("Unable to export Edited Collection to Exchange.", LogManager.enumLogLevel.Debug);
                    return false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exportHistoryID"></param>
        /// <param name="jobID"></param>
        /// <param name="siteCode"></param>
        /// <returns></returns>
        private bool ExportScheduleJobs(int exportHistoryID, int jobID, string siteCode)
        {
            try
            {
                if (BMCEnterpriseExportImport114.ExportScheduledJob(exportHistoryID, jobID, siteCode))
                {
                    LogManager.WriteLog("Schedule Jobs exported to Exchange .", LogManager.enumLogLevel.Info);
                    return true;
                }
                else
                {
                    LogManager.WriteLog("Unable to export Schedule Jobs to Exchange.", LogManager.enumLogLevel.Debug);
                    return false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return false;
        }

        //11.4 Migration Code - Ends

        private void Unload()
        {
            _appWathcer.Dispose();
            if (_limitThreadCount)
            {
                _executorService.AwaitTermination(TimeSpan.Zero);
            }
        }

        public void Stop()
        {
            _ServiceStopped.Set();
            IsRunning = false;
            this.Unload();
        }

        public void ExportDataToExchange()
        {
            if (_limitThreadCount)
            {
                Thread th = null;
                CoreLib.Extensions.InitializeThreadFunc(ref th, ref _initStatus, _lockThread,
                    new ThreadStart(this.ExportDataToExchange_Optimized), "ExportDataToExchange_Optimized_");
            }
            else
            {
                this.ExportDataToExchange_Legacy();
            }
        }

        private void ExportDataToExchange_Optimized()
        {
            ModuleProc PROC = new ModuleProc("BMCEnterpriseExport", "ExportDataToExchange_Optimized");
            _initStatus = InitializeStatus.Completed;
            var dataHelper = new DataHelper();

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
            var dataHelper = new DataHelper();

            try
            {
                DataTable dt = dataHelper.GetSiteList();

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
                                int index = (siteCode % (_threadCount));
                                if (index >= 0 && index < _siteLists.Count)
                                {
                                    SiteInfo si = _siteLists[index];
                                    si.SiteCodes = siteCodeString;
                                    _siteHash.Add(siteCodeString, si);
                                    Log.InfoV(PROC, "::> Site Code : {0}, Thread Index : {1:D}", siteCodeString, index);
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

        #region IBMCEnterpriseExport Members

        public void ExportDataToExchange_Legacy()
        {
            var dataHelper = new DataHelper();
            Thread workerThread;
            try
            {
                _dtSiteList = dataHelper.GetSiteList();
                for (int i = 0; i < _dtSiteList.Rows.Count; i++)
                {
                    try
                    {
                        LogManager.WriteLog("Spawning New Thread", LogManager.enumLogLevel.Info);
                        LogManager.WriteLog("SITE CODE :" + _dtSiteList.Rows[i]["SITE_CODE"], LogManager.enumLogLevel.Info);

                        workerThread = new Thread((ExportDataBySiteCode)) { Name = _dtSiteList.Rows[i]["SITE_CODE"].ToString() };
                        workerThread.Start(_dtSiteList.Rows[i]["SITE_CODE"].ToString());
                        workerThread.Priority = ThreadPriority.Normal;
                    }
                    catch (Exception exthread)
                    {
                        ExceptionManager.Publish(exthread);
                    }
                }

                //var thread = new Thread(StartVerificationExport) {Name = "AAMS Verification"};
                //thread.Start();
                //thread.Priority = ThreadPriority.Normal;
            }
            catch (Exception exExportDataToExchange)
            {
                ExceptionManager.Publish(exExportDataToExchange);
            }

            var workerTreadForChecNewSite = new Thread(CheckForNewSite);
            workerTreadForChecNewSite.Start();

        }

        private void CheckForNewSite()
        {
            var dataHelper = new DataHelper();
            DataTable dtNewSiteList;
            while (true)
            {
                dtNewSiteList = dataHelper.GetSiteList();
                foreach (DataRow dr in dtNewSiteList.Rows)
                {
                    try
                    {
                        try
                        {
                            _dtSiteList.DefaultView.Sort = "SITE_CODE ASC";
                        }
                        catch (Exception ex)
                        {
                            ExceptionManager.Publish(ex);
                        }

                        DataColumn[] dcPk = new DataColumn[1];
                        // Set Primary Key
                        dcPk[0] = _dtSiteList.Columns["SITE_CODE"];
                        _dtSiteList.PrimaryKey = dcPk;

                        if (_dtSiteList.DefaultView.Find(dr["SITE_CODE"].ToString()) != -1) continue;

                        LogManager.WriteLog("Spawning New Thread", LogManager.enumLogLevel.Info);
                        LogManager.WriteLog("SITE CODE :" + dr["SITE_CODE"], LogManager.enumLogLevel.Info);

                        var workerThread = new Thread((ExportDataBySiteCode)) { Name = dr["SITE_CODE"].ToString() };
                        workerThread.Start(dr["SITE_CODE"].ToString());
                        workerThread.Priority = ThreadPriority.Normal;

                        var drNewSiteList = _dtSiteList.NewRow();

                        drNewSiteList["SITE_CODE"] = dr["SITE_CODE"];
                        _dtSiteList.Rows.Add(drNewSiteList);
                    }
                    catch (Exception exCheckForNewSite)
                    {
                        ExceptionManager.Publish(exCheckForNewSite);
                    }
                }

                if (_ServiceStopped.WaitOne(int.Parse(ConfigManager.Read("TimeIntervalToCheckForNewSite")) * 1000)) break;
            }
        }

        private void ExportDataBySiteCode(object sitecode)
        {
            int PerItemProcessInterval = 0;

            try
            {
                //Wait befor processing next data(To avoid CPU load) 
                PerItemProcessInterval = Convert.ToInt32(ConfigManager.Read("PerItemProcessMilliseconds"));
            }
            catch
            {
                PerItemProcessInterval = 100;
            }
            while (IsRunning)
            {
                ProcessRecordsForSite(sitecode.ToString(), PerItemProcessInterval);
                if (_ServiceStopped.WaitOne(Convert.ToInt32(ConfigManager.Read("TimerIntervalinSecs")) * 1000)) break;
            }
        }


        public bool ResetExportHistory()
        {
            var bRecordsReset = (new DataHelper()).ResetInProgressEhRecords();
            if (bRecordsReset)
                LogManager.WriteLog("records resetted in export history", LogManager.enumLogLevel.Info);
            else
                LogManager.WriteLog("records not resetted in export history", LogManager.enumLogLevel.Info);

            return true;
        }

        private void StartVerificationExport()
        {
            var dataHelper = new DataHelper();
            DataTable dtExportData;

            while (IsRunning)
            {
                try
                {
                    dtExportData = dataHelper.GetVerificationExportData();

                    if ((dtExportData.Rows.Count <= 0) || (dtExportData == null))
                    {
                        LogManager.WriteLog("No records to be exported for verification", LogManager.enumLogLevel.Info);
                        dataHelper.UpdateInProgressLGE_EH();
                    }

                    foreach (DataRow row in dtExportData.Rows)
                    {

                        XmlDocument doc = new XmlDocument();
                        XmlElement root = doc.CreateElement("AAMS_IH_Verification");
                        doc.AppendChild(root);

                        XmlElement entID = doc.CreateElement("Enterprise_ID");
                        entID.InnerText = row["LGE_EH_ID"].ToString();
                        XmlElement vltSerial = doc.CreateElement("VLT_Serial");
                        vltSerial.InnerText = row["Machine_Manufacturers_Serial_No"].ToString();
                        XmlElement AAMSID = doc.CreateElement("AAMS_Message_ID");
                        AAMSID.InnerText = row["LGE_EH_AAMS_Message_ID"].ToString();
                        root.AppendChild(entID);
                        root.AppendChild(vltSerial);
                        root.AppendChild(AAMSID);

                        LogManager.WriteLog(doc.OuterXml, LogManager.enumLogLevel.Debug);

                        Proxy proxyClass = new Proxy(row["Site_Code"].ToString());
                        if (proxyClass.ImportExchangeData(doc.OuterXml, "ONDEMANDVERIFICATION"))
                            dataHelper.UpdateLGE_EHStatus(int.Parse(row["LGE_EH_ID"].ToString()), 50);
                    }
                }
                catch (Exception Ex)
                {
                    ExceptionManager.Publish(Ex);
                }

                Thread.Sleep(Convert.ToInt32(ConfigManager.Read("TimerIntervalinSecs")) * 1000);
            }

        }

        public void VerifyVLTComponents()
        {
            try
            {
                int iCompId = 0;
                string strSerialNo = string.Empty;
                int iVerID = 0;
                var dataHelper = new DataHelper();

                LogManager.WriteLog("CV - Request for VerifyVLTComponents", LogManager.enumLogLevel.Info);

                DataTable dtUnprocessedRows = dataHelper.GetRequestVerificationForComponent();

                if (dtUnprocessedRows.Rows.Count > 0)
                {
                    LogManager.WriteLog("CV - Request for VerifyVLTComponents Count - " + dtUnprocessedRows.Rows.Count.ToString(), LogManager.enumLogLevel.Info);

                    foreach (DataRow dr in dtUnprocessedRows.Rows)
                    {
                        iCompId = (int)dr["ComponentID"];
                        strSerialNo = dr["MachineSerialNo"].ToString();
                        iVerID = (int)dr["VerificationID"];

                        LogManager.WriteLog("CV - Before Update - Verification details for Ver ID - " + iVerID.ToString(), LogManager.enumLogLevel.Info);
                        dataHelper.UpdateVerifiedComponents(iVerID, iCompId, strSerialNo);
                        LogManager.WriteLog("CV - Updated Verification details for Ver ID - " + iVerID.ToString(), LogManager.enumLogLevel.Info);
                    }
                }
                else
                {
                    LogManager.WriteLog("CV - Request for VerifyVLTComponents Count is Zero. No record to process", LogManager.enumLogLevel.Info);
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("CV - Error in Request for VerifyVLTComponents ErrorMessage.", LogManager.enumLogLevel.Info);
                ExceptionManager.Publish(ex);
            }
        }

        #endregion
    }
}
