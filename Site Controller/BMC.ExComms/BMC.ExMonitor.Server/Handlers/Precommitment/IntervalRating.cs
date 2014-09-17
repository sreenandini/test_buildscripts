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
    /// <summary>
    /// 
    /// </summary>
    [MonitorHandlerMapping((int)FaultSource.Precommitment, (int)FaultType_Precommitment.IntervalRating)]
    internal class MonitorHandler_PC_33_12 :
        MonitorHandler_PC_Base
    {
        protected override bool ProcessG2HMessageInternal(MonMsg_G2H request)
        {
            using (ILogMethod method = Log.LogMethod("MonitorHandler_PC_33_12", "ProcessG2HMessageInternal"))
            {
                try
                {
                    PCIntervalRatings pgIntervalRating = GetIntervalRatingEntity(request);
                    HandlerHelper.PlayerGatewayInstance.PCIntervalRatingUpdates(pgIntervalRating);
                    return true;
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }
                return false;
            }
        }

        private PCIntervalRatings GetIntervalRatingEntity(MonMsg_G2H request)
        {
            InstallationDetailsForMSMQ installationDetails = ExCommsDataContext.Current.GetInstallationDetailsByDatapak(request.InstallationNo);
            MeterDeltaForPCSessionResult meterdelta = ExCommsDataContext.Current.GetMeterDeltaForPCSession(request.InstallationNo, request.CardNumber, "IR");
            string asset = HandlerHelper.Current.GetAssetByStockPrefix(request.Asset);
            DateTime endDate = DateTime.Now;

            PCIntervalRatings pgStatusRequest = new PCIntervalRatings()
            {
                CardNo = request.CardNumber,
                CardLength = request.CardNumber.Length,
                SlotNo = asset,
                Stand = installationDetails.Bar_Pos_Name,
                StartDate = meterdelta.SessionStartDate.GetValueOrDefault().ToString("yyyyMMdd"),
                StartTime = meterdelta.SessionStartDate.GetValueOrDefault().ToString("HHmmss"),
                EndDate = endDate.Date.ToString("yyyyMMdd"),
                EndTime = endDate.ToString("HHmmss"),
                CoinsBet = Convert.ToInt64(meterdelta.CoinsIn),
                CoinsWon = Convert.ToInt64(meterdelta.CoinsOut),
                GamesPlayed =  Convert.ToInt64(meterdelta.GamesPlayed),
                BarPosition = installationDetails.Bar_Pos_Name,
                Asset = asset,
                SiteCode = request.SiteCode,
                RatingBasis = HandlerHelper.Current.PCRatingBasis
            };
            return pgStatusRequest;
        }
    }
}
