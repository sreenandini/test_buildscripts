using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Data;
using BMC.Common.LogManagement;
using BMC.Common.ExceptionManagement;
using BMC.BusinessClasses.Interfaces;

namespace BMC.BusinessClasses.BusinessLogic
{
    public class STMExportDetails : Interfaces.ISTMExportDetails
    {
        ManualResetEvent m_reset = new ManualResetEvent(false);
        IDictionary<int, int> dic_Occurance = new Dictionary<int, int>();
        bool StopFlag = false;

        public STMExportDetails()
        {

        }

        public bool Start()
        {
            bool retVal = true;
            try
            {
                Thread _WatchSTMExport = new Thread(
                               () =>
                               {
                                   StartMonitorSTM();
                               });
                _WatchSTMExport.Name = "STMExport";
                _WatchSTMExport.Start();
                LogManager.WriteLog("Start: WatchSTMExport Thread Started Successfully ", LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                retVal = false;
            }
            return retVal;
        }

        public void Stop()
        {
            StopMonitorSTM();
        }

        void StartMonitorSTM()
        {
            try
            {
                int Exp_Max = 0;
                int.TryParse(BMC.Common.ConfigurationManagement.ConfigManager.Read("STMExport_MaximumRetry"), out Exp_Max);
                int DB_interval = 0;
                int.TryParse(BMC.Common.ConfigurationManagement.ConfigManager.Read("STMExport_DBHitInterval"), out DB_interval);
                int Evt_interval = 0;
                int.TryParse(BMC.Common.ConfigurationManagement.ConfigManager.Read("STMExport_EventSendInterval"), out Evt_interval);
                BMC.EventsTransmitter.EventTransmitter evt = BMC.EventsTransmitter.EventTransmitter.GetInstance();
                if (evt.IsTransmitterEnabled() == 0)
                {
                    LogManager.WriteLog("StartMonitorSTM : EventTransmitter is disabled in configuration mode", LogManager.enumLogLevel.Info);
                }
                else
                {
                    while (!m_reset.WaitOne(DB_interval))
                    {

                        try
                        {
                            DataTable dtnew = DataHelper.GetRecordsToExportForSTM();
                            foreach (DataRow dr in dtnew.Rows)
                            {
                                int Success = evt.SendMessage(dr["Message"].ToString());
                                if (Success > 0)
                                {
                                    DataHelper.UpdateSTMExportHistoryDetails(Convert.ToInt32(dr["ID"]), 100, null);
                                    LogManager.WriteLog("StartMonitorSTM :  Exported Succeed  Message ID:" + dr["ID"].ToString(), LogManager.enumLogLevel.Info);
                                    m_reset.WaitOne(Evt_interval);
                                }
                                else if (Success == 0)
                                {
                                    DataHelper.UpdateSTMExportHistoryDetails(Convert.ToInt32(dr["ID"]), 100, null);
                                    LogManager.WriteLog("StartMonitorSTM : Exported Succeed[not processed by STM] Message ID:" + dr["ID"].ToString() + " Message: " + dr["Message"].ToString(), LogManager.enumLogLevel.Info);
                                    m_reset.WaitOne(Evt_interval);
                                }
                                else
                                {
                                    int SH_ID = Convert.ToInt32(dr["ID"]);
                                    int Val = 0;
                                    if (dic_Occurance.ContainsKey(SH_ID))
                                    {
                                        dic_Occurance[SH_ID] = dic_Occurance[SH_ID] + 1;
                                        Val = dic_Occurance[SH_ID];
                                    }
                                    else
                                    {
                                        dic_Occurance.Add(SH_ID, 1);
                                    }

                                    LogManager.WriteLog("StartMonitorSTM : Unable to Export Message ID:" + dr["ID"].ToString(), LogManager.enumLogLevel.Error);

                                    if (Val > Exp_Max && Exp_Max > 0)
                                    {
                                        DataHelper.UpdateSTMExportHistoryDetails(SH_ID, -1, "Export Failed");
                                        LogManager.WriteLog("StartMonitorSTM :  Export Failed Message ID:" + dr["ID"].ToString(), LogManager.enumLogLevel.Info);
                                    }
                                    LogManager.WriteLog("StartMonitorSTM : Unable to Export Message ID:" + dr["ID"].ToString(), LogManager.enumLogLevel.Error);
                                    break;
                                }
                            }
                            //Thread.Sleep(3000);
                        }
                        catch (Exception ex)
                        {
                            ExceptionManager.Publish(ex);
                            // m_reset.Set();
                            //break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        void StopMonitorSTM()
        {
            m_reset.Set();
            LogManager.WriteLog("Service Stoped", LogManager.enumLogLevel.Debug);
        }
    }

    public class VaultMessageServiceHandle : IJob
    {
        System.Threading.AutoResetEvent at_vminterval = new System.Threading.AutoResetEvent(false);
        private int VaultMessageDBinterval { get; set; }
        private bool SkipErrorMessage { get; set; }
        IDictionary<string, Func<string, bool>> di_Messages = new Dictionary<string, Func<string, bool>>();

        #region IJob Members

        public void Init()
        {
            try
            {
                LogManager.WriteLog("VaultMessageService Initalizing..... ", LogManager.enumLogLevel.Info);
                VaultMessageDBinterval = Convert.ToInt32(BMC.Common.ConfigurationManagement.ConfigManager.Read("VaultMessageDBinterval"));
                SkipErrorMessage = Convert.ToBoolean(BMC.Common.ConfigurationManagement.ConfigManager.Read("SkipVaultErrorMessage"));
                
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        public void DoWork()
        {
            try
            {
                Thread Th_VMessage = new Thread(() =>
                {
                    ProcessVaultMessages();
                });
                Th_VMessage.Start();
                LogManager.WriteLog("VaultMessageService Thread Started ", LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        public void UnInit()
        {
            try
            {
                at_vminterval.Set();
                LogManager.WriteLog("VaultMessageService Thread Stopped ", LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        public bool CheckSiteStatus()
        {
            throw new NotImplementedException();
        }

        public string GetSettingDetail(string strSetting)
        {
            throw new NotImplementedException();
        }

        #endregion


        void ProcessVaultMessages()
        {
            try
            {

                while (!at_vminterval.WaitOne(VaultMessageDBinterval))
                {
                    if (DataHelper.CheckSqlConnectionExists())
                    {
                      
                        DataTable dt_Messages = DataHelper.GetIncomingMessages();
                        if (dt_Messages.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dt_Messages.Rows)
                            {
                                if (!DataHelper.ProcessIncomingMessages(Convert.ToInt64(dr["RequestID"]), dr["EventType"].ToString(), Convert.ToInt32(dr["EventID"]), dr["XmlData"].ToString(), SkipErrorMessage))
                                {
                                    LogManager.WriteLog("Message Proceed Failed RequestID:" + dr["RequestID"].ToString() + " EventType:" + dr["EventType"].ToString(), LogManager.enumLogLevel.Info);
                                    at_vminterval.WaitOne(VaultMessageDBinterval);
                                    break;
                                }
                                else
                                {
                                    LogManager.WriteLog("Message Proceed Successfully RequestID:" + dr["RequestID"].ToString() + " EventType:" + dr["EventType"].ToString(), LogManager.enumLogLevel.Info);
                                }

                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }




    }
}
