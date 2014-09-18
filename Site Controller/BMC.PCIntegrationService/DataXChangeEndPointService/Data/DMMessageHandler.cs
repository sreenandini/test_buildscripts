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
using BMC.Common.ExceptionManagement;

namespace DataXChangeEndPointService.Data
{
    public static class DMMessageHandler
    {
        public static Thread _processThread = null;
        public static bool _bRunThread;
        public static bool _bInitialized;
        private static DMResponseBusiness objResponseBusiness;
        public static int iMaxRows;
        private static ExchangeClient _exchangeClient;
        private static Sector203Data m_SectorData;
        public static ManualResetEventSlim _prcoessEvent;
        private static int _waitTime;

        static DMMessageHandler()
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

                objResponseBusiness = DMResponseBusiness.ResponseBusinessInstance;
                _processThread = new Thread(ProcessMessages);
                _processThread.Start();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static void ProcessMessages()
        {
            DataSet dsMessages;
            byte[] bFreeFormMsg = { };

         
            while (_bRunThread)
            {
                if (_prcoessEvent.Wait(_waitTime))
                {
                    Thread.Sleep(10);
                    continue;
                }

                LogManager.WriteLog("Inside the DM Message thread.", LogManager.enumLogLevel.Info);
                while (true)
                {
                      LogManager.WriteLog("Inside the DM Message While loop.", LogManager.enumLogLevel.Info);
                    objResponseBusiness = new DMResponseBusiness();
                    int iRecordsToProcess = 0;
                    dsMessages = objResponseBusiness.GetDataForSendToComms(ref iRecordsToProcess);
                    if ((dsMessages == null || dsMessages.Tables.Count <= 0 || dsMessages.Tables[0].Rows.Count <= 0))
                    {
                        if (iRecordsToProcess > 0)
                        {
                            LogManager.WriteLog("No DM records matches, but stil DM records are pending to process: " + iRecordsToProcess.ToString(), LogManager.enumLogLevel.Info);
                            Thread.Sleep(100);
                            continue;
                        }
                    }
                    else
                    {
                        foreach (DataRow row in dsMessages.Tables[0].Rows)
                        {
                            string strResponseMessage = Convert.ToString(row["DM_ActualMessage"]);
                            int iInstallationNo = Convert.ToInt32(row["Installation_No"]);

                            Thread.Sleep(10);

                            LogManager.WriteLog("Send the DM Message to iView", LogManager.enumLogLevel.Info);
                            if (SendFreeFormMsgToComms(strResponseMessage, iInstallationNo))
                            {
                                objResponseBusiness.DeleteSentDMMessage(Convert.ToInt32(row["DM_ID"]));
                                LogManager.WriteLog("Sent Directed Message to Ex Comms Successfully for the Installation : " + iInstallationNo.ToString(), LogManager.enumLogLevel.Info);
                                _prcoessEvent.Set();
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

        private static bool SendFreeFormMsgToComms(string Response, int iInstallationNo)
        {
            try
            {
                LogManager.WriteLog("Send DM Message to Comms", LogManager.enumLogLevel.Info);
                byte[] bFreeFormMsg = StringtoBytes(Response);
                m_SectorData.PutCommandDataVB(bFreeFormMsg);
                m_SectorData.Command = Convert.ToByte(83);

                //_exchangeClient.RequestExWriteSector(iInstallationNo, 203, m_SectorData);
                _exchangeClient.ServerRequestExWriteSector(iInstallationNo, 203, m_SectorData);
                LogManager.WriteLog("Send DM Message to Comms successfull", LogManager.enumLogLevel.Info);
                return true;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog(ex.Message, LogManager.enumLogLevel.Error);
            }
            return false;
        }

        private static byte[] StringtoBytes(string Message)
        {
            byte[] bValues = null;
            try
            {
                System.Text.ASCIIEncoding oEncoder = new System.Text.ASCIIEncoding();
                bValues = oEncoder.GetBytes(Message);

                LogManager.WriteLog("String to Bytes" + bValues.ToString().Length,
                    LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return bValues;
        }


    }
}
