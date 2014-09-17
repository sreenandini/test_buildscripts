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
    #region Enrollment Parameter Request

    [MonitorHandlerMapping((int)FaultSource.Precommitment, (int)FaultType_Precommitment.EnrollmentParamRequest)]
    internal class MonitorHandler_PC_33_8 :
        MonitorHandler_PC_Base
    {
        int installationNo;
        protected override bool ProcessG2HMessageInternal(MonMsg_G2H request)
        {
            using (ILogMethod method = Log.LogMethod("MonitorHandler_PC_33_8", "ProcessG2HMessageInternal"))
            {
                try
                {
                    MonTgt_G2H_PC_EnrollmentParameterRequest enrollParamStatReq = request.Targets[0] as MonTgt_G2H_PC_EnrollmentParameterRequest;
                    if (enrollParamStatReq == null) return false;

                    installationNo = request.InstallationNo;
                    PCEnrollParamorStatusRequest pgEnrollParamStatReq = GetPCEnrollParamorStatusRequestEntity(request, enrollParamStatReq);
                    HandlerHelper.PlayerGatewayInstance.PCEnrollParameterRequest(pgEnrollParamStatReq, this.PCEnrollParameterResponseInfo);
                    return true;
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }
                return false;
            }
        }

        private PCEnrollParamorStatusRequest GetPCEnrollParamorStatusRequestEntity(MonMsg_G2H request, MonTgt_G2H_PC_EnrollmentParameterRequest enrollParamStatReq)
        {
            string encryptedPin = HandlerHelper.Current.GetEncryptedPIN(Crypto.Crypto.AsciiToHex(enrollParamStatReq.PlayerPIN, HandlerHelper.Current.Encode));
            string asset = HandlerHelper.Current.GetAssetByStockPrefix(request.Asset);
            DateTime transDate = DateTime.Now;
            InstallationDetailsForMSMQ installationDetails = ExCommsDataContext.Current.GetInstallationDetailsByDatapak(request.InstallationNo);

            PCEnrollParamorStatusRequest pgEnrollParamStatReq = new PCEnrollParamorStatusRequest()
            {
                CardNo = request.CardNumber,
                CardLength = request.CardNumber.Length,
                EncryptedPin = encryptedPin,
                EventDate = transDate.Date.ToString("yyyyMMdd"),
                EventTime = transDate.Date.ToString("HHmmss"),
                SlotNo = installationDetails.Bar_Pos_Name,
                Stand = asset,
                BarPosition = installationDetails.Bar_Pos_Name
            };
            return pgEnrollParamStatReq;
        }

        private void PCEnrollParameterResponseInfo(PreCommitEnrollmentParameterResponse response)
        {
            if (response.ResultStatus == ResponseStatus.Success)
                PCGatewayMessages.Instance.SendPCEnrollmentResponse(response.ReturnValue, response.RequestID, installationNo);
        }
    }

    #endregion //Enrollment Parameter Request

    #region Player Enrollment Parameter Request

    [MonitorHandlerMapping((int)FaultSource.Precommitment, (int)FaultType_Precommitment.PlayerEnrollmentRequest)]
    internal class MonitorHandler_PC_33_10 :
        MonitorHandler_PC_Base
    {
        int installationNo = 0;
        protected override bool ProcessG2HMessageInternal(MonMsg_G2H request)
        {
            using (ILogMethod method = Log.LogMethod("MonitorHandler_PC_33_8", "ProcessG2HMessageInternal"))
            {
                try
                {
                    MonTgt_G2H_PC_PlayerEnrollmentRequest playerEnrollmentReq = request.Targets[0] as MonTgt_G2H_PC_PlayerEnrollmentRequest;
                    if (playerEnrollmentReq == null) return false;

                    installationNo = request.InstallationNo;
                    PCEnrollmentRequest pgEnrollParamStatReq = GetPCPlayerEnrollmentRequestEntity(request, playerEnrollmentReq);
                    HandlerHelper.PlayerGatewayInstance.PCEnrollmentRequestCall(pgEnrollParamStatReq, this.PCPlayerEnrollmentResponseInfo);
                    return true;
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }
                return false;
            }
        }

        private PCEnrollmentRequest GetPCPlayerEnrollmentRequestEntity(MonMsg_G2H request, MonTgt_G2H_PC_PlayerEnrollmentRequest playerEnrollmentReq)
        {
            string asset = HandlerHelper.Current.GetAssetByStockPrefix(request.Asset);
            DateTime transDate = DateTime.Now;
            InstallationDetailsForMSMQ installationDetails = ExCommsDataContext.Current.GetInstallationDetailsByDatapak(request.InstallationNo);

            PCEnrollmentRequest pgEnrollParamStatReq = new PCEnrollmentRequest()
            {
                CardNo = request.CardNumber,
                CardLength = request.CardNumber.Length,
                SlotNo = installationDetails.Bar_Pos_Name,
                Stand = asset,
                BarPosition = installationDetails.Bar_Pos_Name,
                EventDate = transDate.Date.ToString("yyyyMMdd"),
                EventTime = transDate.Date.ToString("HHmmss"),

                IsDayTimeBasis = playerEnrollmentReq.IsDayTimeBasis,
                DayTargetTime = playerEnrollmentReq.DayDefaultTime.ToString(),

                IsWeekTimeBasis = playerEnrollmentReq.IsWeekTimeBasis,
                WeekTargetTime = playerEnrollmentReq.WeekTargetTime.ToString(),

                IsMonthTimeBasis = playerEnrollmentReq.IsMonthTimeBasis,
                MonthTargetTime = playerEnrollmentReq.MonthTargetTime.ToString(),

                IsDayLossBasis = playerEnrollmentReq.IsDayLossBasis,
                DayTargetLoss =  Convert.ToInt32((playerEnrollmentReq.DayTargetLossValue * 100)),

                IsWeekLossBasis = playerEnrollmentReq.IsWeekLossBasis,
                WeekTargetLoss =  Convert.ToInt32((playerEnrollmentReq.WeekTargetLossValue * 100)),

                IsMonthLossBasis = playerEnrollmentReq.IsMonthLossBasis,
                MonthTargetLoss =  Convert.ToInt32((playerEnrollmentReq.MonthTargetLossValue * 100)),

                IsDayWagerBasis = playerEnrollmentReq.IsDayWagerBasis,
                DayTargetWagers =  Convert.ToInt32((playerEnrollmentReq.DayTargetWager* 100)),

                IsWeekWagerBasis = playerEnrollmentReq.IsWeekWagerBasis,
                WeekTargetWagers =  Convert.ToInt32((playerEnrollmentReq.WeekTargetWager* 100)),

                IsMonthWagerBasis = playerEnrollmentReq.IsMonthWagerBasis,
                MonthTargetWagers = Convert.ToInt32((playerEnrollmentReq.MonthTargetWager * 100))

            };
            return pgEnrollParamStatReq;
        }

        private void PCPlayerEnrollmentResponseInfo(PreCommitEnrollmentResponse response)
        {
            if (response.ResultStatus == ResponseStatus.Success)
                PCGatewayMessages.Instance.SendPCPlayerEnrollmentResponse(response.ReturnValue, response.RequestID, installationNo);
        }
    }

    #endregion //Player Enrollment Parameter Request
}
