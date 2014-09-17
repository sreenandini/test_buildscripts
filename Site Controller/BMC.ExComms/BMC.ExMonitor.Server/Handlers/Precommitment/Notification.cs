using BMC.CoreLib;
using BMC.ExComms.Contracts.DTO.Monitor;
using BMC.ExComms.DataLayer.MSSQL;
using BMC.PlayerGateway.Gateway;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExMonitor.Server.Handlers.Precommitment
{
    [MonitorHandlerMapping((int)FaultSource.Precommitment, (int)FaultType_Precommitment.NotificationResponse)]
    internal class MonitorHandler_PC_33_7 :
        MonitorHandler_PC_Base
    {
        protected override bool ProcessG2HMessageInternal(MonMsg_G2H request)
        {
            using (ILogMethod method = Log.LogMethod("MonitorHandler_PC_33_7", "ProcessG2HMessageInternal"))
            {
                try
                {
                    MonTgt_G2H_PC_NotificationResponse notificationResponse = request.Targets[0] as MonTgt_G2H_PC_NotificationResponse;
                    if (notificationResponse == null) return false;

                    PCNotificationResponse pgnotificationResponse = GetNotificationResponseEntity(request, notificationResponse);
                    HandlerHelper.PlayerGatewayInstance.SendPCNotificationResponse(pgnotificationResponse);
                    return true;
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }
                return false;
            }
        }

        private PCNotificationResponse GetNotificationResponseEntity(MonMsg_G2H request, MonTgt_G2H_PC_NotificationResponse notificationResponse)
        {
            InstallationDetailsForMSMQ installationDetails = ExCommsDataContext.Current.GetInstallationDetailsByDatapak(request.InstallationNo);
            string asset = HandlerHelper.Current.GetAssetByStockPrefix(request.Asset);
            DateTime transDate = DateTime.Now;

            PCNotificationResponse pgnotificationResponse = new PCNotificationResponse()
            {
                CardNo = request.CardNumber,
                CardLength = request.CardNumber.Length,
                SlotNo = request.Asset,
                Stand = installationDetails.Bar_Pos_Name,
                EventDate = transDate.Date,
                EventTime = transDate.Date.TimeOfDay,
                SiteCode = request.SiteCode,
                BarPosition = request.Asset,
                Asset = asset
            };

            return pgnotificationResponse;
        }
    }
}
