using System;
using System.Threading;
using System.Collections.Generic;
using BMC.EnterpriseBusiness.Business;
using BMC.EnterpriseBusiness.Entities;
using BMC.DataExportToSite.BusinessLogic;
using BMC.Common.LogManagement;
using BMC.Common.ConfigurationManagement;
using System.Xml.Linq;
using BMC.Common.ExceptionManagement;
using Audit.BusinessClasses;
using Audit.Transport;
using BMC.Common.Utilities;

namespace BMC.DataExportToSite.BusinessLogic
{
    class BMCEnterpriseDropScheduler
    {
        private Int32 configurableHour = 1;
        //private Int32 configurableFactor = 5;
        private Int32 dBCheckinMinutes = 60;
        private ManualResetEvent _mreShutDown = new ManualResetEvent(false);
        //private const int WAIT_TIME = 60000; // 1 minute
        int dbChecker = 0;
        private bool RunMinuteThread
        {
            get;
            set;
        }
        private Thread workerThread;
        private bool isDropScheduleEnabled = false;

        private DropScheduleBiz objDropScheduleBiz = DropScheduleBiz.CreateInstance();
        private DropScheduleEntity ScheduleEntity = new DropScheduleEntity();
        
        private void DBScheduleChecker()
        {
            try
            {
                LogManager.WriteLog("DBScheduleChecker entry", LogManager.enumLogLevel.Info);
                ScheduleEntity = null;
                List<DropScheduleEntity> lstobjDropSchedule;                
                lstobjDropSchedule = objDropScheduleBiz.GetDropSchedule(DropScheduler.OmitSeconds(DateTime.Now));
                if (lstobjDropSchedule == null || lstobjDropSchedule.Count == 0)
                {
                    RunMinuteThread = false;
                    LogManager.WriteLog("DBScheduleChecker Unable to get the schedule information for current date", LogManager.enumLogLevel.Info);
                    return;
                }

                ScheduleEntity = lstobjDropSchedule[0];
                ScheduleEntity.NextOcc = DropScheduler.GetNextOcc(ScheduleEntity);
                if (ScheduleEntity.NextOcc == null)
                {
                    RunMinuteThread = false;
                    LogManager.WriteLog("DBScheduleChecker NextOcc is not found", LogManager.enumLogLevel.Info);
                    return;
                }
                LogManager.WriteLog("DBScheduleChecker ScheduleEntity.NextOcc value is " + ((DateTime)ScheduleEntity.NextOcc).ToString("MM/dd/yy H:mm"), LogManager.enumLogLevel.Info);
                if (DropScheduler.GetRemainingHours((DateTime)(ScheduleEntity.NextOcc), DateTime.Now) <= configurableHour)
                {
                    RunMinuteThread = true;
                }
                LogManager.WriteLog("DBScheduleChecker exit", LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        public void MinuteProcess()
        {
            try
            {
                LogManager.WriteLog("MinuteProcess entry", LogManager.enumLogLevel.Info);

                if (DropScheduler.CompareTimes(DateTime.Now, (DateTime)ScheduleEntity.NextOcc))//, (Double)configurableFactor))
                {
                    LogManager.WriteLog("MinuteProcess Schedule Time " + (DateTime.Now).ToString("MM/dd/yy H:mm"), LogManager.enumLogLevel.Info);                    
                    List<ManualDropScheduleEntity> dropAlerts = objDropScheduleBiz.GetManualDropAlertDetails(0, 0, ScheduleEntity.StackerLevelPercentage);
                    string SuccessBarPositionList = string.Empty;
                    string FailedBarPositionList = string.Empty;
                    if (dropAlerts.Count <= 0)
                    {
                        LogManager.WriteLog("MinuteProcess No Stacker reached the percentage : " + ScheduleEntity.StackerLevelPercentage, LogManager.enumLogLevel.Info);
                    }
                    foreach (var dropAlert in dropAlerts)
                    {
                        try
                        {
                            SuccessBarPositionList = "," + dropAlert.BAR_POSITION_NAME.ToString() + SuccessBarPositionList;
                            XElement item = dropAlert.ToString(ScheduleEntity.ScheduleId, "Automatic", (DateTime)ScheduleEntity.NextOcc);
                            LogManager.WriteLog("MinuteProcess XML data " + item, LogManager.enumLogLevel.Debug);
                            String siteCode = item.Element("SiteId").Value;
                            objDropScheduleBiz.STM_Export_History("drop", 1, siteCode, item);
                            objDropScheduleBiz.InsertUpdateDropScheduleHistory(null, ScheduleEntity, (DateTime)ScheduleEntity.NextOcc, 100);
                        }
                        catch (Exception ex)
                        {
                            FailedBarPositionList = "," + dropAlert.BAR_POSITION_NAME.ToString() + FailedBarPositionList;
                            ExceptionManager.Publish(ex);
                            objDropScheduleBiz.InsertUpdateDropScheduleHistory(null, ScheduleEntity, (DateTime)ScheduleEntity.NextOcc, -1);
                        }
                    }
                    if (!String.IsNullOrEmpty(SuccessBarPositionList))
                    {
                        SuccessBarPositionList = SuccessBarPositionList.Substring(1, SuccessBarPositionList.Length - 1);
                    }

                    if (!String.IsNullOrEmpty(FailedBarPositionList))
                    {
                        FailedBarPositionList = FailedBarPositionList.Substring(1, FailedBarPositionList.Length - 1);
                    }

                    try
                    {
                        AuditViewerBusiness business = new AuditViewerBusiness(DatabaseHelper.GetConnectionString());
                        {
                            business.InsertAuditData(new Audit.Transport.Audit_History
                            {
                                EnterpriseModuleName = ModuleNameEnterprise.DropAlert,
                                Audit_Screen_Name = "DropAlert|AutoDropAlert",
                                Audit_Desc = "Auto Drop Alert Performed.",
                                AuditOperationType = OperationType.ADD,                                
                                Audit_User_Name = "System"
                            }, false);

                            business.InsertAuditData(new Audit.Transport.Audit_History
                            {
                                EnterpriseModuleName = ModuleNameEnterprise.DropAlert,
                                Audit_Screen_Name = "DropAlert|AutoDropAlert",
                                Audit_Desc = "SuccessBarPositionList:" + SuccessBarPositionList,
                                AuditOperationType = OperationType.ADD,                                
                                Audit_User_Name = "System"
                            }, false);

                            business.InsertAuditData(new Audit.Transport.Audit_History
                            {
                                EnterpriseModuleName = ModuleNameEnterprise.DropAlert,
                                Audit_Screen_Name = "DropAlert|AutoDropAlert",
                                Audit_Desc = "FailedBarPositionList:" + FailedBarPositionList,
                                AuditOperationType = OperationType.ADD,                                
                                Audit_User_Name = "System"
                            }, false);
                        }
                    }
                    catch (Exception ex)
                    {
                        LogManager.WriteLog("Error While Adding Audit Log for Manual Drop Alert: Error Message:" + ex.Message, LogManager.enumLogLevel.Error);
                    }

                    dbChecker = 0;
                    RunMinuteThread = false;
                    ScheduleEntity.NextOcc = null;
                }

                LogManager.WriteLog("MinuteProcess exit", LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void StartDropScheduler()
        {
            try
            {
                LogManager.WriteLog("StartDropScheduler entry", LogManager.enumLogLevel.Info);
                int MinuteCounter = 12;
                dbChecker = dBCheckinMinutes;
                while (!_mreShutDown.WaitOne(5000))
                {

                    if (dbChecker >= dBCheckinMinutes)
                    {
                        LogManager.WriteLog("StartDropScheduler dbChecker value is " + dbChecker + " DateTime is " + DateTime.Now.ToString("MM/dd/yy H:mm"), LogManager.enumLogLevel.Debug);
                        string setting_Value = "False";
                        objDropScheduleBiz.GetSetting(0, "DropSheduleAlert", "False", ref setting_Value);
                        if (string.Compare(setting_Value.Trim(), "TRUE", true) == 0)
                        {
                        DBScheduleChecker();
                        }
                        else
                        {
                            LogManager.WriteLog("StartDropScheduler:: DropSheduleAlert got disabled in Enterprise",LogManager.enumLogLevel.Info);
                            RunMinuteThread = false;
                        }
                        dbChecker = 0;
                    }
                    if (MinuteCounter == 12)
                    {
                        LogManager.WriteLog("MinuteCounter DateTime is " + DateTime.Now.ToString("MM/dd/yy HH:mm"), LogManager.enumLogLevel.Debug);
                        dbChecker++;
                        if (RunMinuteThread)
                        {
                            MinuteProcess();
                        }
                        MinuteCounter = 0;
                    }
                    MinuteCounter++;
                }
                LogManager.WriteLog("StartDropScheduler end", LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        public void Stop()
        {
            if(isDropScheduleEnabled)
                _mreShutDown.Set();
        }

        public void Start()
        {
            try
            {
                if (isDropScheduleEnabled)
                {
                    LogManager.WriteLog("StartScheduler entry", LogManager.enumLogLevel.Info);
                    workerThread = new Thread((StartDropScheduler)) { Name = "DropSchedulerThread" };
                    workerThread.Priority = ThreadPriority.Normal;
                    workerThread.Start();
                    LogManager.WriteLog("StartScheduler end", LogManager.enumLogLevel.Info);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        public BMCEnterpriseDropScheduler()
        {
            try
            {
                LogManager.WriteLog("Constructor BMCEnterpriseDropScheduler entry", LogManager.enumLogLevel.Info);                

                string setting_Value = "False";

                objDropScheduleBiz.GetSetting(0, "StackerFeature", "False", ref setting_Value);
                isDropScheduleEnabled = string.Compare(setting_Value, "TRUE", true) == 0;
                setting_Value = "1";
                if (isDropScheduleEnabled)
                {
                    objDropScheduleBiz.GetSetting(0, "MinuteThreadCheckinHoursforAutoDrop", "1", ref setting_Value);
                    configurableHour = Convert.ToInt32(setting_Value);
                    setting_Value = "5";
                    objDropScheduleBiz.GetSetting(0, "RetryMinutesForCheckDB", "5", ref setting_Value);
                    dBCheckinMinutes = Convert.ToInt32(setting_Value);
                }
                else
                {
                    LogManager.WriteLog("Constructor BMCEnterpriseDropScheduler Auto Drop Schedule disabled", LogManager.enumLogLevel.Info);
                }
                LogManager.WriteLog("Constructor BMCEnterpriseDropScheduler end", LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
       }
    }
}
