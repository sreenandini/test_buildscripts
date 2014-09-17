using BMC.CoreLib;
using BMC.ExComms.Contracts.DTO.Monitor;
using System;
using System.Collections.Generic;
using System.Linq;
using BMC.PlayerGateway.Gateway;
using BMC.ExComms.DataLayer.MSSQL;

namespace BMC.ExMonitor.Server.Handlers.Precommitment
{
    /// <summary>
    /// Precommitment Status Request Handler
    /// </summary>
    [MonitorHandlerMapping((int)FaultSource.Precommitment, (int)FaultType_Precommitment.StatusRequest)]
    internal class MonitorHandler_PC_33_1 :
        MonitorHandler_PC_Base
    {
        int installationNo = 0;
        protected override bool ProcessG2HMessageInternal(MonMsg_G2H request)
        {
            using (ILogMethod method = Log.LogMethod("MonitorHandler_PC_33_1", "ProcessG2HMessageInternal"))
            {
                try
                {
                    MonTgt_G2H_PC_StatusRequest statusRequest = request.Targets[0] as MonTgt_G2H_PC_StatusRequest;
                    if (statusRequest == null) return false;

                    installationNo = request.InstallationNo;
                    PCEnrollParamorStatusRequest pgStatusRequest = GetStatusRequestEntity(request, statusRequest);
                    HandlerHelper.PlayerGatewayInstance.PCStatusRequest(pgStatusRequest, this.PCStatusRespone);
                    return true;
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }
                return false;
            }
        }

        private PCEnrollParamorStatusRequest GetStatusRequestEntity(MonMsg_G2H request, MonTgt_G2H_PC_StatusRequest statusRequest)
        {
            string encryptedPin = HandlerHelper.Current.GetEncryptedPIN(Crypto.Crypto.AsciiToHex(statusRequest.PlayerPIN, HandlerHelper.Current.Encode));
            string asset = HandlerHelper.Current.GetAssetByStockPrefix(request.Asset);
            DateTime transDate = DateTime.Now;
            
            InstallationDetailsForMSMQ installationDetails = ExCommsDataContext.Current.GetInstallationDetailsByDatapak(request.InstallationNo);
            Log.Info("Encrypted Pin " + encryptedPin);

            PCEnrollParamorStatusRequest pgStatusRequest = new PCEnrollParamorStatusRequest()
            {
                CardNo = request.CardNumber,
                CardLength = request.CardNumber.Length,
                EncryptedPin = encryptedPin,
                EventDate = transDate.Date.ToString("yyyyMMdd"),
                EventTime = transDate.Date.ToString("HHmmss"),
                SlotNo = asset,
                Stand = installationDetails.Bar_Pos_Name,
                BarPosition = installationDetails.Bar_Pos_Name,
                Asset = asset
            };

            return pgStatusRequest;
        }

        private void PCStatusRespone(PreCommitStatusResponse response)
        {
            if (response.ResultStatus == ResponseStatus.Success)
                PCGatewayMessages.Instance.SendPCStatusResponse(response.ReturnValue, response.RequestID, installationNo);
        }
    }
}
