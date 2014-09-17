using BMC.CoreLib;
using BMC.ExComms.Contracts.DTO.Monitor;
using BMC.ExComms.DataLayer.MSSQL;
using BMC.PlayerGateway.Gateway;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExMonitor.Server.Handlers.EPI
{
    #region Deposit Request

    /// <summary>
    /// Deposit Request Monitor Handler
    /// </summary>
    internal class MonitorHandler_EPI_23_3 :
        MonitorHandler_EPI_Base
    {
        protected override bool ProcessG2HMessageInternal(MonMsg_G2H request)
        {
            using (ILogMethod method = Log.LogMethod("MonitorHandler_EPI_23_3", "ProcessG2HMessageInternal"))
            {
                try
                {
                    MonTgt_G2H_EFT_DepositRequest monDepositRequest = request.Targets[0] as MonTgt_G2H_EFT_DepositRequest;
                    if (monDepositRequest == null) return false;
                    
                    //DeleteEPIMessage(request.InstallationNo);
                    if (DoDepositRequest(request, monDepositRequest))
                        method.Info("Deposit Request Transfer done");
                    else
                        method.Info("Deposit Request Could not complete your transaction");
                    return true;
                }
                catch (Exception ex)
                {
                    //EPIMsgProcessor.Current.SendEPIMessage(monBalanceResponse);
                    //EPIMsgProcessor.Current.DisplayBallyWelcomeMsg(request.InstallationNo, );
                    method.Exception(ex);
                }
                return false;
            }
        }

        private bool DoDepositRequest(MonMsg_G2H request, MonTgt_G2H_EFT_DepositRequest monDepositRequest)
        {
            Log.Info("Started Deposit Request");
            Log.Info("Card Value : " + monDepositRequest.CardNumber);
            //Log.Info("Encrypted Pin XXXXXXXXXXXXXXXX");
            Log.Info("Started DepositRequest " + monDepositRequest.CardNumber);
            
            InstallationDetailsForMSMQ installationDetails = ExCommsDataContext.Current.GetInstallationDetailsByDatapak(request.InstallationNo);
            HandlerHelper.Current.SaveSDTRequest(monDepositRequest.CardNumber, request.InstallationNo);
            //HandlerHelper.Current.SaveSDTAccountType(monDepositRequest.CardNumber, );

            string asset = HandlerHelper.Current.GetAssetByStockPrefix(request.Asset);
            DateTime transDate = DateTime.Now;

            //Authentication  - To do
            DepositRequest depositRequest = new DepositRequest
            {
                //AccountType = monDepositRequest.ac,
                CashableFunds = Convert.ToInt32((monDepositRequest.CashableAmount * 100)),
                NonCashableFunds = Convert.ToInt32((monDepositRequest.NonCashableAmount * 100)),
                //Authentication = 
                BarPosition = installationDetails.Bar_Pos_Name,
                CardNo = monDepositRequest.CardNumber,
                //EncryptedPin = 
                InstallationNo = request.InstallationNo,
                SlotIndex = installationDetails.Bar_Pos_Name,
                SlotNumber = asset,
                Stand = installationDetails.Bar_Pos_Name,
                TransactionDate = transDate.ToString("yyyyMMdd").PadLeft(8, '0'),
                TransactionID = installationDetails.TransactionID.ToString(),
                TransactionTime = transDate.ToString("HHmmss").PadLeft(6, '0'),
                SiteCode = request.SiteCode
            };

            HandlerHelper.PlayerGatewayInstance.DepositRequest(depositRequest, this.DepositRequestResp);
            Log.Info("Time taken to send message to Gateway " + DateTime.Now.TimeOfDay.ToString());
            return true;
        }

        private void DepositRequestResp(DepositResponse response)
        {
            Log.Info("Deposit Request Response " + response.ResultStatus.ToString());
            if (response.ResultStatus == ResponseStatus.Success)
                SDTMessages.Instance.ProcessAFTInformation(response.ReturnValue, MonitorConstants.DEPOSITRESPONSE, response.RequestID, true);
            else
                SDTMessages.Instance.ProcessAFTInformation(response.ReturnValue, MonitorConstants.DEPOSITRESPONSE, response.RequestID, false);
        }
    }

    #endregion //Deposit Request

    #region Deposit Complete

    internal class MonitorHandler_EPI_23_4 :
        MonitorHandler_EPI_Base
    {
        protected override bool ProcessG2HMessageInternal(MonMsg_G2H request)
        {
            using (ILogMethod method = Log.LogMethod("MonitorHandler_EPI_23_4", "ProcessG2HMessageInternal"))
            {
                try
                {
                    MonTgt_G2H_EFT_DepositComplete monDepositRequest = request.Targets[0] as MonTgt_G2H_EFT_DepositComplete;
                    if (monDepositRequest == null) return false;

                    //DeleteEPIMessage(request.InstallationNo);
                    method.Info("Started Deposit Complete");
                    method.Info("Card Value: " + monDepositRequest.CardNumber);
                    //Log.Info("Encrypted Pin XXXXXXXXXXXXXXXX");

                    if (DoDepositComplete(request, monDepositRequest))
                        method.Info("Deposit Complete Transfer done");
                    else
                        method.Info("Deposit Complete - Could not complete your transaction");
                    return true;
                }
                catch (Exception ex)
                {
                    //EPIMsgProcessor.Current.SendEPIMessage(monBalanceResponse);
                    //EPIMsgProcessor.Current.DisplayBallyWelcomeMsg(request.InstallationNo, );
                    method.Exception(ex);
                }
                return false;
            }
        }

        private bool DoDepositComplete(MonMsg_G2H request, MonTgt_G2H_EFT_DepositComplete monDepositRequest)
        {
            InstallationDetailsForMSMQ installationDetails = ExCommsDataContext.Current.GetInstallationDetailsByDatapak(request.InstallationNo);
            HandlerHelper.Current.SaveSDTRequest(monDepositRequest.CardNumber, request.InstallationNo);
            Log.Info("TransactionID used in Deposit Request " + installationDetails.TransactionID.ToString());

            string asset = HandlerHelper.Current.GetAssetByStockPrefix(request.Asset);
            DateTime transDate = DateTime.Now;

            //Authentication - To do
            DepositRequest depositRequest = new DepositRequest
            {
                //AccountType = monDepositRequest.ac,
                CashableFunds = Convert.ToInt32((monDepositRequest.CashableAmount * 100)),
                NonCashableFunds = Convert.ToInt32((monDepositRequest.NonCashableAmount * 100)),
                //Authentication = 
                BarPosition = installationDetails.Bar_Pos_Name,
                CardNo = monDepositRequest.CardNumber,
                InstallationNo = request.InstallationNo,
                SlotIndex = installationDetails.Bar_Pos_Name,
                SlotNumber = asset,
                Stand = installationDetails.Bar_Pos_Name,
                TransactionDate = transDate.ToString("yyyyMMdd").PadLeft(8, '0'),
                TransactionID = installationDetails.TransactionID.ToString().PadLeft(3, '0').Substring(0, 3),
                TransactionTime = transDate.ToString("HHmmss").PadLeft(6, '0'),
                SiteCode = request.SiteCode
            };

            HandlerHelper.PlayerGatewayInstance.DepositComplete(depositRequest, this.DepositCompleteResp);
            return true;
        }

        private void DepositCompleteResp(DepositCompleteResponse response)
        {
            Log.Info("Deposit Complete Response " + response.ResultStatus.ToString());
            if (response.ResultStatus == ResponseStatus.Success)
                SDTMessages.Instance.ProcessAFTInformation(response.ReturnValue, MonitorConstants.DEPOSITCOMPLETERESPONSE, response.RequestID, true);
            else
                SDTMessages.Instance.ProcessAFTInformation(response.ReturnValue, MonitorConstants.DEPOSITCOMPLETERESPONSE, response.RequestID, false);
        }
    }

    #endregion Deposit Complete
}
