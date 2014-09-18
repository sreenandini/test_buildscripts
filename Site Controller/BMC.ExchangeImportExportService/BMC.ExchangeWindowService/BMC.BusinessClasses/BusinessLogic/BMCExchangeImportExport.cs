using System;
using System.Data;
using System.Reflection;
using System.Threading;
using BMC.Common;
using BMC.Common.ConfigurationManagement;
using BMC.Common.ExceptionManagement;
using BMC.Common.LogManagement;
using System.Data.SqlClient;
using BMC.DataAccess;

namespace BMC.BusinessClasses.BusinessLogic
{
    internal class BMCExchangeImportExport:Interfaces.IBMCExchangeImportExport
    {
        bool _threadRunning;
        Thread _workerThread;
        Thread _importDataWorkerThread;
        public AutoResetEvent _ServiceStopped=new AutoResetEvent(false);
        
        #region IBMCExchangeImportExport Members

        public bool ExportDataToEnterprise()
        {
            _threadRunning = true;
            _workerThread = new Thread(ThreadStart) { Priority = ThreadPriority.Normal };
            _workerThread.Start();
            return true;
        }

        public bool ImportDataToExchange()
        {
            LogManager.WriteLog("Calling ImportDataToExchange", LogManager.enumLogLevel.Info);
            _importDataWorkerThread = new Thread(ImportData) { Priority = ThreadPriority.Normal };
            _importDataWorkerThread.Start();
            return true;
        }

        public void ExportInstallationDataToEnterprise()
        {
            try
            {
                DataHelper.GetExportInstallationDetails();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
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


        public void StopThread()
        {
           
            _ServiceStopped.Set(); 

            _threadRunning = false;

            if (_workerThread.IsAlive)
            {
                _workerThread.Abort();
            }
        }

        public bool CheckSiteStatus()
        {
            LogManager.WriteLog("Inside CheckSiteStatus method", LogManager.enumLogLevel.Info);

            return DataHelper.GetSiteStatus();
        }
        
        public string GetSettingDetail(string strSetting)
        {
            LogManager.WriteLog("Inside GetSettingDetail method", LogManager.enumLogLevel.Info);

            return DataHelper.GetSettingFromDB(strSetting);
        }

        #endregion
        #region "private methods"
        
        private void ThreadStart()
        {
            DataSet objdsAllExportData;
            bool success;
            bool breakResult;
            var strMethodName = MethodBase.GetCurrentMethod().Name;
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

            // EmptyRecordCountCheck
            int emptyRecordCountCheck = 0;
            try
            {
                emptyRecordCountCheck = Convert.ToInt32(ConfigManager.Read("EmptyRecordCountCheck"));
            }
            catch { }
            emptyRecordCountCheck = Math.Max(10, emptyRecordCountCheck);
            LogManager.WriteLog("EmptyRecordCountCheck : " + emptyRecordCountCheck, LogManager.enumLogLevel.Info);

            int emptyCount = 0;
            while (_threadRunning)
            {
                try
                {
                    objdsAllExportData = DataHelper.GetAllExportData();

                    if (objdsAllExportData == null || objdsAllExportData.Tables[0].Rows.Count <= 0)
                    {
                        emptyCount++;
                        if (emptyCount >= emptyRecordCountCheck)
                        {
                            LogManager.WriteLog(strMethodName + "::" + "Either there No Records to be Exported Or Previous Records are still being processed.", LogManager.enumLogLevel.Error);
                            DataHelper.ResetInProgressEhRecords();
                            emptyCount = 0;
                        }
                    }
                    else
                    {
                        emptyCount = 0;
                        foreach (DataRow row in objdsAllExportData.Tables[0].Rows)
                        {
                            try
                            {
                                LogManager.WriteLog("::: (START) Processing EH_ID : " + row[Constants.CONSTANT_COL_EH_ID].ToString(), LogManager.enumLogLevel.Info);
                            }
                            catch { }

                            LogManager.WriteLog(row[Constants.CONSTANT_COL_EH_TYPE].ToString(), LogManager.enumLogLevel.Debug);
                            if (_ServiceStopped.WaitOne(PerItemProcessInterval))
                            {
                                break;
                            }
                            success = false;
                            string defaultStatus = "100";

                            try
                            {
                                switch (row[Constants.CONSTANT_COL_EH_TYPE].ToString().Trim().ToUpper())
                                {
                                    case "SITECONFIG":
                                        success = DataHelper.ExportSiteConfig();
                                        break;
                                    case "COLLECTIONDETAILS":
                                        success = DataHelper.GetExportCollectionDetails(row[Constants.CONSTANT_COL_EH_REFERENCE].ToString(),
                                                row[Constants.CONSTANT_COL_EH_ID].ToString());
                                        break;
                                    case "COLLECTION":
                                    case "BATCH":
                                        success =
                                            DataHelper.GetExportCollectionBatch(
                                                row[Constants.CONSTANT_COL_EH_REFERENCE].ToString(),
                                                row[Constants.CONSTANT_COL_EH_ID].ToString());
                                        break;
                                    case "MOVEMENT":
                                        break;
                                    case "VAULTBALANCE":
                                        LogManager.WriteLog("Inside Case : VAULTBALANCE", LogManager.enumLogLevel.Info);
                                        success = DataHelper.ExportVaultBalance(int.Parse(row[Constants.CONSTANT_COL_EH_ID].ToString()), int.Parse(row[Constants.CONSTANT_COL_EH_REFERENCE].ToString()));
                                        break;
                                    case "VAULTDROP":
                                        LogManager.WriteLog("Inside Case : VAULTDROP", LogManager.enumLogLevel.Info);
                                        success = DataHelper.ExportVaultDrop(int.Parse(row[Constants.CONSTANT_COL_EH_REFERENCE].ToString()), row[Constants.CONSTANT_COL_EH_ID].ToString());
                                        break;
                                    case "VAULTTRANSACTIONEVENT":
                                        LogManager.WriteLog("Inside Case : VAULTTRANSACTIONEVENT", LogManager.enumLogLevel.Info);
                                        success = DataHelper.ExportVaultTransactionEvent(int.Parse(row[Constants.CONSTANT_COL_EH_REFERENCE].ToString()), row[Constants.CONSTANT_COL_EH_ID].ToString());
                                        break;
                                    case "VAULTEVENT":
                                        LogManager.WriteLog("Inside Case : VAULTEVENT", LogManager.enumLogLevel.Info);
                                        success = DataHelper.ExportVaultEvent(int.Parse(row[Constants.CONSTANT_COL_EH_REFERENCE].ToString()), row[Constants.CONSTANT_COL_EH_ID].ToString());
                                        break;
                                    case "VAULTTRANSACTION":
                                        LogManager.WriteLog("Inside Case : VAULTTRANSACTION" + int.Parse(row[Constants.CONSTANT_COL_EH_ID].ToString()) + "Transaction Id is -->" + int.Parse(row[Constants.CONSTANT_COL_EH_REFERENCE].ToString()), LogManager.enumLogLevel.Info);
                                        success = DataHelper.ExportVaultTransaction(int.Parse(row[Constants.CONSTANT_COL_EH_ID].ToString()), int.Parse(row[Constants.CONSTANT_COL_EH_REFERENCE].ToString()));
                                        break;
                                    case "ENROLLVAULT":
                                        LogManager.WriteLog("Inside Case : ENROLLVAULT" + int.Parse(row[Constants.CONSTANT_COL_EH_ID].ToString()) + "Installation Id is -->" + int.Parse(row[Constants.CONSTANT_COL_EH_REFERENCE].ToString()), LogManager.enumLogLevel.Info);
                                        success = DataHelper.ExportEnrollVault(int.Parse(row[Constants.CONSTANT_COL_EH_ID].ToString()), int.Parse(row[Constants.CONSTANT_COL_EH_REFERENCE].ToString()));
                                        break;
                                    case "STACKERLEVEL":
                                        success = DataHelper.ExportStackerLevelData(int.Parse(row[Constants.CONSTANT_COL_EH_ID].ToString()), int.Parse(row[Constants.CONSTANT_COL_EH_REFERENCE].ToString()));
                                        break;
                                    case "LIQUIDATIONDETAILS":
                                        success = DataHelper.ExportLiquidationDetails(int.Parse(row[Constants.CONSTANT_COL_EH_ID].ToString()), int.Parse(row[Constants.CONSTANT_COL_EH_REFERENCE].ToString()));
                                        break;
                                    case "LIQUIDATIONSHAREDETAILS":
                                        success = DataHelper.ExportLiquidationShareDetails(int.Parse(row[Constants.CONSTANT_COL_EH_ID].ToString()), int.Parse(row[Constants.CONSTANT_COL_EH_REFERENCE].ToString()));
                                        break;
                                    case "GLORYAUDIT":
                                        success = DataHelper.ExportGloryCDAudit(int.Parse(row[Constants.CONSTANT_COL_EH_ID].ToString()), int.Parse(row[Constants.CONSTANT_COL_EH_REFERENCE].ToString()));
                                        break;
                                    case "NEWINSTALLATION":
                                        success = DataHelper.ExportNewInstallation(int.Parse(row[Constants.CONSTANT_COL_EH_ID].ToString()), int.Parse(row[Constants.CONSTANT_COL_EH_REFERENCE].ToString()));
                                        break;
                                    case "INSTALLATIONFROMEH":
                                        break;
                                    case "REMOVEINSTALLATION":
                                        success = DataHelper.ExportRemoveInstallation(int.Parse(row[Constants.CONSTANT_COL_EH_ID].ToString()), int.Parse(row[Constants.CONSTANT_COL_EH_REFERENCE].ToString()));
                                        break;
                                    case "CONVERTINSTALLATION":
                                        success = DataHelper.ExportConvertOrGmuChangeInstallation(int.Parse(row[Constants.CONSTANT_COL_EH_ID].ToString()), int.Parse(row[Constants.CONSTANT_COL_EH_REFERENCE].ToString()), false);
                                        break;
                                    case "GMUCHANGEINSTALLATION":
                                        success = DataHelper.ExportConvertOrGmuChangeInstallation(int.Parse(row[Constants.CONSTANT_COL_EH_ID].ToString()), int.Parse(row[Constants.CONSTANT_COL_EH_REFERENCE].ToString()), true);
                                        break;
                                    case "METERHISTORY":
                                        success = DataHelper.GetExportMeterHistoryData(Int32.Parse(row["EH_Reference1"].ToString()), row[Constants.CONSTANT_COL_EH_ID].ToString());
                                        break;
                                    case "DAILYREAD":
                                    case "DAILY":
                                        LogManager.WriteLog("Read ID" + Constants.CONSTANT_COL_EH_REFERENCE, LogManager.enumLogLevel.Info);
                                        success = DataHelper.GetExportReadData(Convert.ToInt32(row[Constants.CONSTANT_COL_EH_REFERENCE]), row[Constants.CONSTANT_COL_EH_ID].ToString(), ref defaultStatus);
										if(defaultStatus =="300")
											LogManager.WriteLog("Read XML is Empty " + Constants.CONSTANT_COL_EH_REFERENCE, LogManager.enumLogLevel.Info);
                                        break;
                                    case "VTP":
                                    case "HOURLYSTATISTICS":
                                        success = DataHelper.GetExportHourlyStatisticsData(row[Constants.CONSTANT_COL_EH_ID].ToString());
                                        break;
                                    case "DOOREVENT":
                                    case "POWEREVENT":
                                    case "FAULTEVENT":
                                    case "GENERALEVENTS":
                                        success = DataHelper.GetExportSiteEvents(row[Constants.CONSTANT_COL_EH_ID].ToString());
                                        break;
                                    case "TREASURY":
                                        success = ExportTreasuryEntry(row[Constants.CONSTANT_COL_EH_ID].ToString(), "TREASURY");
                                        break;
                                    case "INSTALLATIONDETAILS":
                                        success = DataHelper.ExportInstallationUpdate(int.Parse(row[Constants.CONSTANT_COL_EH_REFERENCE].ToString()));
                                        break;
                                    case "MULTIGAMEINSTALL":
                                        success = DataHelper.GetMGMDID(int.Parse(row[Constants.CONSTANT_COL_EH_REFERENCE].ToString()));
                                        break;
                                    case "GAMEDETAIL":
                                        success = DataHelper.ExportGameDetails(int.Parse(row[Constants.CONSTANT_COL_EH_ID].ToString()), int.Parse(row[Constants.CONSTANT_COL_EH_REFERENCE].ToString()));
                                        break;
                                    case "PAYTABLE":
                                        success = DataHelper.ExportPaytableDetails(int.Parse(row[Constants.CONSTANT_COL_EH_ID].ToString()), int.Parse(row[Constants.CONSTANT_COL_EH_REFERENCE].ToString()));
                                        break;
                                    case "GAMESESSION":
                                        success = DataHelper.ExportSessionDetails(int.Parse(row[Constants.CONSTANT_COL_EH_ID].ToString()), int.Parse(row[Constants.CONSTANT_COL_EH_REFERENCE].ToString()));
                                        break;
                                    case "INSTALLATIONGAMEINFO":
                                        success = DataHelper.ExportInstallationGameInfo(int.Parse(row[Constants.CONSTANT_COL_EH_REFERENCE].ToString()));
                                        break;
                                    case "MACHINECLASS":
                                        success = DataHelper.ExportMachineClass(int.Parse(row[Constants.CONSTANT_COL_EH_ID].ToString()), int.Parse(row[Constants.CONSTANT_COL_EH_REFERENCE].ToString()));
                                        break;
                                    case "MACHINE":
                                        success = DataHelper.ExportMachineDetails(int.Parse(row[Constants.CONSTANT_COL_EH_ID].ToString()), int.Parse(row[Constants.CONSTANT_COL_EH_REFERENCE].ToString()));
                                        break;
                                    case "GAMECHANGE":
                                        success = DataHelper.ExportGameChangeForInstallation(int.Parse(row[Constants.CONSTANT_COL_EH_REFERENCE].ToString()));
                                        break;
                                    case "GAMECAPPING":
                                        success = DataHelper.ExportGameCappingDetails(int.Parse(row[Constants.CONSTANT_COL_EH_ID].ToString()), int.Parse(row[Constants.CONSTANT_COL_EH_REFERENCE].ToString()));
                                        break;
                                    case "CLOSEINSTALLATION":
                                        success = DataHelper.CloseInstallation(int.Parse(row[Constants.CONSTANT_COL_EH_REFERENCE].ToString()));
                                        break;
                                    case "INSTALLATIONCHANGEDENOM":
                                        success = DataHelper.ExportInstallationDenomChange(int.Parse(row[Constants.CONSTANT_COL_EH_REFERENCE].ToString()));
                                        break;
                                    case "INSTALLATIONSTATUSUPDATE":
                                        success = DataHelper.ExportInstallationStatusUpdate(int.Parse(row[Constants.CONSTANT_COL_EH_REFERENCE].ToString()));
                                        break;
                                    case "CHANGEPASSWORD":
                                        success = DataHelper.ExportChangePassword(int.Parse(row[Constants.CONSTANT_COL_EH_REFERENCE].ToString()));
                                        break;
                                    case "VLTVERIFICATION":
                                    case "AAMSVERIFICATION":
                                        success = DataHelper.ExportVerificationStatus(row[Constants.CONSTANT_COL_EH_REFERENCE].ToString(), row[Constants.CONSTANT_COL_EH_TYPE].ToString().Trim().ToUpper());
                                        break;
                                    case "AAMSCONFIG":
                                        success = DataHelper.ExportAssetBADStatus(int.Parse(row[Constants.CONSTANT_COL_EH_REFERENCE].ToString()));
                                        break;
                                    case "MACHINEMAINTENANCE":
                                        success = DataHelper.ExportDetailsforMachineMaintenance(int.Parse(row[Constants.CONSTANT_COL_EH_ID].ToString()), int.Parse(row[Constants.CONSTANT_COL_EH_REFERENCE].ToString()));
                                        break;
                                    case "MAINTENANCESESSION":
                                        success = DataHelper.ExportMaintenanceSession(int.Parse(row[Constants.CONSTANT_COL_EH_ID].ToString()), int.Parse(row[Constants.CONSTANT_COL_EH_REFERENCE].ToString()));
                                        break;
                                    case "MAINTENANCEHISTORY":
                                        success = DataHelper.ExportMaintenanceHistory(int.Parse(row[Constants.CONSTANT_COL_EH_ID].ToString()), int.Parse(row[Constants.CONSTANT_COL_EH_REFERENCE].ToString()));
                                        break;
                                    case "MAINTENANCEREASONCATEGORY":
                                        success = DataHelper.ExportMaintenanceReasonCategory(int.Parse(row[Constants.CONSTANT_COL_EH_ID].ToString()), int.Parse(row[Constants.CONSTANT_COL_EH_REFERENCE].ToString()));
                                        break;
                                    case "AUDIT":
                                        success = DataHelper.ExportAuditHistory(int.Parse(row[Constants.CONSTANT_COL_EH_REFERENCE].ToString()));
                                        break;
                                    case "AFTAUDIT":
                                        success = DataHelper.ExportAFTAuditHistory(int.Parse(row[Constants.CONSTANT_COL_EH_REFERENCE].ToString()));
                                        break;
                                    case "AFTTRANSACTIONS":
                                        success = DataHelper.ExportAFTTransactions(int.Parse(row[Constants.CONSTANT_COL_EH_REFERENCE].ToString()));
                                        break;
                                    //case "COMPONENTDETAILS":
                                    //    success = DataHelper.ExportComponentDetails(int.Parse(row[Constants.CONSTANT_COL_EH_REFERENCE].ToString()));
                                    //    break;
                                    //case "MACHINECOMPONENTDETAILS":
                                    //    success = DataHelper.ExportMachineComponentDetails(int.Parse(row[Constants.CONSTANT_COL_EH_REFERENCE].ToString()));
                                    //    break;
                                    //case "COMPVERIFICATIONRECORD":
                                    //    success = DataHelper.ExportComponentVerificationDetails(int.Parse(row[Constants.CONSTANT_COL_EH_REFERENCE].ToString()));
                                    //    break;
                                    case "INSTALLATIONGAMEDATA":
                                        success = DataHelper.ExportInstallationGameData(int.Parse(row[Constants.CONSTANT_COL_EH_REFERENCE].ToString()));
                                        break;
                                    case "REINSTATE":
                                        success = DataHelper.ExportReInstateData(int.Parse(row[Constants.CONSTANT_COL_EH_REFERENCE].ToString()));
                                        break;
                                    case "GAMEPAYTABLEDETAILS":
                                        success = DataHelper.ExportGamePaytableDetailsforInstallation(int.Parse(row[Constants.CONSTANT_COL_EH_ID].ToString()), int.Parse(row[Constants.CONSTANT_COL_EH_REFERENCE].ToString()));
                                        break;
                                    case "BATCHEXPCOMPLETE":
                                        success = DataHelper.ExportBatchCompleteStatus(int.Parse(row[Constants.CONSTANT_COL_EH_REFERENCE].ToString()), row[Constants.CONSTANT_COL_EH_ID].ToString());
                                        break;
                                    case "SITELICENSING":
                                        success = DataHelper.UpdateLicenseKey(int.Parse(row[Constants.CONSTANT_COL_EH_REFERENCE].ToString()));
                                        break;
                                    case "EMPCARDSESSIONS":
                                        success = DataHelper.ExportEmpCardSessions(int.Parse(row[Constants.CONSTANT_COL_EH_REFERENCE].ToString()), int.Parse(row[Constants.CONSTANT_COL_EH_ID].ToString()));
                                        break;
									case "FUND":
	                                    success = DataHelper.ExportFundDetails(int.Parse(row[Constants.CONSTANT_COL_EH_REFERENCE].ToString()), row[Constants.CONSTANT_COL_EH_ID].ToString());
	                                    break;
                                    case "FACTORYRESET":
                                        success = DataHelper.SetFactoryReset(int.Parse(row[Constants.CONSTANT_COL_EH_REFERENCE].ToString()),row[Constants.CONSTANT_COL_EH_ID].ToString());
	                                    break;
                                    case "COMMONDATA":
                                        success = DataHelper.GetHQInstallationNo(int.Parse(row[Constants.CONSTANT_COL_EH_REFERENCE].ToString()), row[Constants.CONSTANT_COL_EH_ID].ToString());
                                        break;
                                    case "ALERTS":
                                         success = DataHelper.ExportAlertDetails(int.Parse(row[Constants.CONSTANT_COL_EH_REFERENCE].ToString()), row[Constants.CONSTANT_COL_EH_ID].ToString());
                                        break;
                                    case "EMAILALERTSTATUS":
                                        success = DataHelper.ExportMailAlertStatus(int.Parse(row[Constants.CONSTANT_COL_EH_REFERENCE].ToString()), row[Constants.CONSTANT_COL_EH_ID].ToString());
                                        break;

                                    default:
                                        success = true;
                                        break;
                                }
                            }
                            catch (Exception ex)
                            {
                                ExceptionManager.Publish(ex);
                                success = false;
                            }
                            finally
                            {
                                if (success)
                                {
                                    DataHelper.UpdateExportHistoryTableWithStatus(int.Parse(row[Constants.CONSTANT_COL_EH_ID].ToString()), defaultStatus);
                                    breakResult = true;
                                }
                                else
                                {
                                    DataHelper.UpdateExportHistoryTableWithStatus(int.Parse(row[Constants.CONSTANT_COL_EH_ID].ToString()), "-1");
                                    breakResult = false;
                                }

                                try
                                {
                                    LogManager.WriteLog("::: (END) Processing EH_ID : " + row[Constants.CONSTANT_COL_EH_ID].ToString() + ", Status : " + success, LogManager.enumLogLevel.Info);
                                }
                                catch { }
                            }
                            if (!breakResult)
                            {
                                DataHelper.ResetInProgressEhRecords();
                                break;
                            }
                        }
                    }

                    Thread.Sleep(Convert.ToInt32(ConfigManager.Read(Constants.CONSTANT_DIALUP_TIMERINTERVAL)) * 1000);
                }
                catch (Exception ex)
                {
                    ExceptionManager.Publish(ex);
                }
            }

        }

        private bool ExportTreasuryEntry(string ehid, string type)
        {
            bool bUpdateStatus = false;
            int iEnterpriseTreasuryID = DataHelper.GetExportTreasuryData(ehid, type);

            if (iEnterpriseTreasuryID > 0)
            {
                bUpdateStatus = DataHelper.UpdateTreasuryIDinExchange(iEnterpriseTreasuryID, ehid);
            }
            if (bUpdateStatus)
            {
                LogManager.WriteLog("TREASURY ::" + "EXPORTED Treasury Entry DATA SUCCESSFULLY ::", LogManager.enumLogLevel.Info);
                return true;
            }
            throw new Exception();
        }

        private void ImportData()
        {
            LogManager.WriteLog("Inside ImportData", LogManager.enumLogLevel.Info);

            while (true)
            {
                LogManager.WriteLog("Import Thread Triggered", LogManager.enumLogLevel.Info);
                var objdtImportHistory = new DataTable();
                bool status;
                var resultStatus = string.Empty;
                try
                {
                    objdtImportHistory = DataHelper.GetRecordsToBeImported();
                    LogManager.WriteLog("The count of Import Records is :" + objdtImportHistory.Rows.Count, LogManager.enumLogLevel.Info);
                }
                catch (Exception ex)
                {
                    ExceptionManager.Publish(ex);
                }
                if (objdtImportHistory.Rows.Count > 0)
                {
                    foreach (DataRow dr in objdtImportHistory.Rows)
                    {
                        status = false;

                        try
                        {
                            switch (dr["IH_Type"].ToString().Trim().ToUpper())
                            {
                                case "SITESETUP":
                                    status = DataHelper.ImportSite(dr["IH_Details"].ToString());
                                    break;
                                case "MODEL":
                                    status = DataHelper.ImportModel(dr["IH_Details"].ToString());
                                    break;
                                case "CALENDAR":
                                    status = DataHelper.ImportCalendar(dr["IH_Details"].ToString());
                                    break;
                                case "MACHINEENABLEDISABLE":
                                    status = DataHelper.MachineStatus(dr["IH_Details"].ToString(), int.Parse(dr["IH_EH_ID"].ToString()));
                                    break;
                                case "MACHINENOTEACCEPTOR":
                                    status = DataHelper.MachineAcceptorStatus(dr["IH_Details"].ToString(), int.Parse(dr["IH_EH_ID"].ToString()));
                                    break;
                                case "SITESETTINGS":
                                    status = DataHelper.UpdateSetting(dr["IH_Details"].ToString());
                                    break;
                                case "AUTOINSTALLATION":
                                    //status = DataHelper.ImportInstallation(dr["IH_Details"].ToString());
                                    status = true;
                                    break;
                                case "STACKER":
                                    status = DataHelper.ImportStackerDetails(dr["IH_Details"].ToString());
                                    break;
                                case "SHAREHOLDER":
                                    status = DataHelper.ImportShareHolderDetails(dr["IH_Details"].ToString());
                                    break;
                                case "EXPENSESHARE":
                                    status = DataHelper.ImportExpenseShareDetails(dr["IH_Details"].ToString());
                                    break;
                                case "PROFITSHARE":
                                    status = DataHelper.ImportProfitShareDetails(dr["IH_Details"].ToString());
                                    break;
                                case "EXPENSESHAREGROUP":
                                    status = DataHelper.ImportExpenseShareGroupDetails(dr["IH_Details"].ToString());
                                    break;
                                case "PROFITSHAREGROUP":
                                    status = DataHelper.ImportProfitShareGroupDetails(dr["IH_Details"].ToString());
                                    break;
                                case "LIQUIDATIONDETAILS":
                                    status = DataHelper.ImportLiquidationDetails(dr["IH_Details"].ToString());
                                    break;
                                case "LIQUIDATIONSHAREDETAILS":
                                    status = DataHelper.ImportLiquidationShareDetails(dr["IH_Details"].ToString());
                                    break;
                                case "GAMECATEGORY":
                                    status = DataHelper.ImportGameCategory(dr["IH_Details"].ToString());
                                    break;
                                case "GAMETITLE":
                                    status = DataHelper.ImportGameTitle(dr["IH_Details"].ToString());
                                    break;
                                case "PAYTABLE":
                                    status = DataHelper.ImportPaytable(dr["IH_Details"].ToString());
                                    break;
                                case "GAMELIBRARY_MAPPING":
                                    status = DataHelper.ImportGameLibraryMapping(dr["IH_Details"].ToString());
                                    break;
                                case "MANUFACTURER_DETAILS":
                                    status = DataHelper.ImportManufacturerDetails(dr["IH_Details"].ToString());
                                    break;
                                case "ENTCOLLBATCH":
                                    status = DataHelper.ImportDeclaredCollectionDetails(dr["IH_Details"].ToString());
                                    break;
                                case "SITELICENSING":
									//if(SiteLicensingExpiryChecker.Instance != null)
									//{
                                    //	SiteLicensingExpiryChecker.Instance.ValidateLicenseEnabled();//Start or Stop Timer and Reset the timer based on Site Licensing data changes
									//}
                                    status = true;
                                    break;
                                case "DELETEZONE":
                                    status = DataHelper.DeleteZone(dr["IH_Details"].ToString());
                                    break;
                                default:
                                    status = true;
                                    break;
                            }
                        }
                        catch (Exception ex)
                        {
                            ExceptionManager.Publish(ex);
                            status = false;
                            resultStatus = ex.Message;
                        }
                        finally
                        {
                            try
                            {
                                if (!status)
                                {
                                    DataHelper.UpdateProcessDetailsForImportHistory(Int32.Parse(dr["IH_ID"].ToString()), resultStatus, -1);
                                }
                                else
                                {
                                    DataHelper.UpdateProcessDetailsForImportHistory(Int32.Parse(dr["IH_ID"].ToString()), resultStatus, 100);
                                }

                            }
                            catch (Exception ex1)
                            {

                                ExceptionManager.Publish(ex1);
                            }
                        }
                    }
                }
                Thread.Sleep(1000 * 60);
            }
        }


        #endregion

    }
}
