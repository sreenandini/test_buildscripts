using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using DataXChangeEndPointService.Business;
using System.Configuration;
using System.Data;
using System.Linq;
using ComExchangeLib;
using BMC.Common.LogManagement;

namespace DataXChangeEndPointService.Data
{
    public static class MessageHandler
    {
        public static Thread _processThread = null;
        public static bool _bRunThread;
        public static bool _bInitialized;
        private static ResponseBusiness objResponseBusiness;
        public static int iMaxRows;
        private static ExchangeClient _exchangeClient;
        private static Sector203Data m_SectorData;
        public static ManualResetEventSlim _prcoessEvent;
        private static int _waitTime;

        static MessageHandler()
        {
            _bInitialized = false;
        }

        public static void Initialize()
        {
            if (!_bInitialized)
            {
                _bRunThread = true;
                _prcoessEvent = new ManualResetEventSlim(true);
                _waitTime = Convert.ToInt32(ConfigurationManager.AppSettings["WaitTime"].ToString());
                iMaxRows = Convert.ToInt32(ConfigurationManager.AppSettings["MaxRows"].ToString());

                _exchangeClient = new ExchangeClient();
                _exchangeClient.InitialiseExchange(0);
                m_SectorData = new Sector203Data();

                objResponseBusiness = ResponseBusiness.ResponseBusinessInstance;
                _processThread = new Thread(ProcessMessages);
                _processThread.Start();
            }
        }

        public static void ProcessMessages()
        {
            DataSet dsMessages;
            byte[] bFreeFormMsg = { };

            while (_bRunThread)
            {
                if (_prcoessEvent.Wait(_waitTime))
                {
                    Thread.Sleep(10);
                    //LogManager.WriteLog("manual reset event.", LogManager.enumLogLevel.Info);
                    continue;
                }

                LogManager.WriteLog("Inside the thread.", LogManager.enumLogLevel.Info);
                while (true)
                {
                    int iRecordsToProcess = 0;
                    dsMessages = objResponseBusiness.GetDataForSendToComms(ref iRecordsToProcess);
                    if ((dsMessages == null || dsMessages.Tables.Count <= 0 || dsMessages.Tables[0].Rows.Count <= 0))
                    {
                        if (iRecordsToProcess > 0)
                        {
                            LogManager.WriteLog("No records matches, but stil records are pending to process: " + iRecordsToProcess.ToString(), LogManager.enumLogLevel.Info);
                            Thread.Sleep(100);
                            continue;
                        }
                    }
                    else
                    {
                        foreach (DataRow row in dsMessages.Tables[0].Rows)
                        {
                            string strResponseMessage = Convert.ToString(row["PC_ST_ActualMessage"]);
                            int iInstallationNo = Convert.ToInt32(row["Installation_No"]);

                            Thread.Sleep(10);
                            switch (strResponseMessage.Substring(2, 2))
                            {
                                case "06":
                                    bFreeFormMsg = objResponseBusiness.GetCardInFreeFormMessage(strResponseMessage);
                                    if (SendFreeFormMsgToComms(bFreeFormMsg, iInstallationNo))
                                    {
                                        objResponseBusiness.UpdateSentFreeFormMsgToCommsStatus(Convert.ToInt32(row["PC_ST_ID"]), true);
                                        LogManager.WriteLog("Sent Card in Response PC06 data to Ex Comms Successfully for the Installation : " + iInstallationNo.ToString(), LogManager.enumLogLevel.Info);
                                        _prcoessEvent.Set();
                                    }
                                    break;

                                case "03":
                                    bFreeFormMsg = objResponseBusiness.GetApproachLimitFreeFormMessage(strResponseMessage);
                                    if (SendFreeFormMsgToComms(bFreeFormMsg, iInstallationNo))
                                    {
                                        objResponseBusiness.UpdateSentFreeFormMsgToCommsStatus(Convert.ToInt32(row["PC_ST_ID"]), true);
                                        LogManager.WriteLog("Sent Approach Limit PC03 data to Ex Comms Successfully for the Installation : " + iInstallationNo.ToString(), LogManager.enumLogLevel.Info);
                                        _prcoessEvent.Set();
                                    }
                                    break;

                                case "04":
                                    bFreeFormMsg = objResponseBusiness.GetLimitReachedFreeFormMessage(strResponseMessage);
                                    if (SendFreeFormMsgToComms(bFreeFormMsg, iInstallationNo))
                                    {
                                        objResponseBusiness.UpdateSentFreeFormMsgToCommsStatus(Convert.ToInt32(row["PC_ST_ID"]), true);
                                        LogManager.WriteLog("Sent Limit PC04 data to Ex Comms Successfully for the Installation : " + iInstallationNo.ToString(), LogManager.enumLogLevel.Info);
                                        _prcoessEvent.Set();
                                    }
                                    break;

                                case "07":
                                    bFreeFormMsg = objResponseBusiness.GetRelaxedLimitFreeFormMessage(strResponseMessage);
                                    if (SendFreeFormMsgToComms(bFreeFormMsg, iInstallationNo))
                                    {
                                        objResponseBusiness.UpdateSentFreeFormMsgToCommsStatus(Convert.ToInt32(row["PC_ST_ID"]), true);
                                        LogManager.WriteLog("Sent Limit PC07 data to Ex Comms Successfully for the Installation : " + iInstallationNo.ToString(), LogManager.enumLogLevel.Info);
                                        _prcoessEvent.Set();
                                    }
                                    break;
                            }
                        }
                        if (iRecordsToProcess > iMaxRows)
                        {
                            LogManager.WriteLog("Records to process is greater than Max rows:" + iRecordsToProcess.ToString(), LogManager.enumLogLevel.Info);
                            Thread.Sleep(100);
                            continue;
                        }
                    }
                    break;
                }

                Thread.Sleep(10000);
            }
        }

        public static void StopProcess()
        {
            _prcoessEvent.Set();
            _bRunThread = false;
            if (_processThread.IsAlive)
            {
                _processThread.Abort();
            }
        }

        private static bool SendFreeFormMsgToComms(byte[] bFreeFormMsg, int iInstallationNo)
        {
            try
            {
                m_SectorData.PutCommandDataVB(bFreeFormMsg);
                m_SectorData.Command = 0xA0;

                _exchangeClient.RequestExWriteSector(iInstallationNo, 203, m_SectorData);
                return true;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog(ex.Message, LogManager.enumLogLevel.Error);
            }
            return false;
        }
    }
}
