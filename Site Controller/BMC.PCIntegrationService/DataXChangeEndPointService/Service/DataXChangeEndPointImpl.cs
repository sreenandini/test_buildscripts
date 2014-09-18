using BMC.Common.LogManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using DataXChangeEndPointService.Business;
using BMC.Common.ExceptionManagement;
using ComExchangeLib;
using System.Threading;
using DataXChangeEndPointService.Data;

namespace DataXChangeEndPointService.Service
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class DataXChangeEndPointImpl : DataXChangeEndPoint
    {

        #region DataMembers

        private ResponseBusiness objResponseBusiness = ResponseBusiness.ResponseBusinessInstance;

        private DMResponseBusiness oDMResponses = DMResponseBusiness.ResponseBusinessInstance;

        #endregion //DataMembers

        #region Constructor

        public DataXChangeEndPointImpl()
        {
        }

        #endregion //Constructor

        #region DataXChangeEndPoint Members

        public sendResponseResponse1 sendResponse(sendResponseRequest request)
        {
            sendResponseResponse1 resp = new sendResponseResponse1();

            try
            {
                string sendResponse = request.sendResponse.responseMessage;
                LogManager.WriteLog("In sendResponse Method \r\n"
                                    + "Response Message: " + sendResponse + "\r\n"
                                    + "Source: " + request.sendResponse.source + "\r\n"
                                    + "Site: " + request.sendResponse.siteNumber.ToString(), LogManager.enumLogLevel.Info);

                if (string.IsNullOrEmpty(sendResponse)) return resp;
                if (sendResponse.Length < 4) return resp;

                string MessageType = sendResponse.Substring(0, 2);
                string TransactionCode = sendResponse.Substring(2, 2);

                switch (MessageType)
                {
                    case "DM":
                        {
                            switch (TransactionCode)
                            {
                                case "31":
                                case "32":
                                case "33":
                                    {
                                        bool bResult = oDMResponses.InsertDMresponses(sendResponse);
                                        if (bResult)
                                        {
                                            LogManager.WriteLog("Inserted the DM messages into the DB : Success", LogManager.enumLogLevel.Info);
                                            DMMessageHandler._prcoessEvent.Reset();
                                            Thread.Sleep(100);
                                        }
                                        break;
                                    }
                                
                                default:
                                    break;
                            }
                            break;
                        }

                    case "PC":
                        {
                            switch (TransactionCode)
                            {

                                case "06":
                                    if (objResponseBusiness.InsertCardInResponseFromPC(sendResponse))
                                    {
                                        LogManager.WriteLog("Inserted the Card in response into the DB : Success", LogManager.enumLogLevel.Info);
                                        MessageHandler._prcoessEvent.Reset();
                                        Thread.Sleep(100);
                                    }

                                    else
                                    {
                                        LogManager.WriteLog("Inserted the Card in response into the DB : Failure", LogManager.enumLogLevel.Info);
                                    }

                                    break;

                                case "03":
                                    if (objResponseBusiness.InsertApproachLimitNotification(sendResponse))
                                    {
                                        LogManager.WriteLog("Inserted the Approaching Limit Notification into the DB : Success", LogManager.enumLogLevel.Info);
                                        MessageHandler._prcoessEvent.Reset();
                                    }

                                    else
                                    {
                                        LogManager.WriteLog("Inserted the  Approaching Limit Notification into the DB : Failure", LogManager.enumLogLevel.Info);
                                    }

                                    break;

                                case "04":
                                    if (objResponseBusiness.InsertLimitReachedNotification(sendResponse))
                                    {
                                        LogManager.WriteLog("Inserted the Limit Reached Notification into the DB : Success", LogManager.enumLogLevel.Info);
                                        MessageHandler._prcoessEvent.Reset();
                                    }

                                    else
                                    {
                                        LogManager.WriteLog("Inserted the  Limit Reached Notification into the DB : Failure", LogManager.enumLogLevel.Info);
                                    }

                                    break;

                                case "07":
                                    if (objResponseBusiness.InsertRelaxedLimitdNotification(sendResponse))
                                    {
                                        LogManager.WriteLog("Inserted the Relaxed Limit Notification into the DB : Success", LogManager.enumLogLevel.Info);
                                        MessageHandler._prcoessEvent.Reset();
                                    }

                                    else
                                    {
                                        LogManager.WriteLog("Inserted the Relaxed Limit Notification into the DB : Failure", LogManager.enumLogLevel.Info);
                                    }

                                    break;
                            }
                        }
                        break;
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return resp;
        }

        #endregion //DataXChangeEndPoint Members

    }
}
