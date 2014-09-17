using BMC.PlayerGateway.Gateway;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.Common.ExceptionManagement;
using BMC.ExComms.Contracts.DTO.Monitor;

namespace BMC.ExMonitor.Server.Handlers.Precommitment
{
    public class PCGatewayMessages
    {
        private static PCGatewayMessages _instance;

        public static PCGatewayMessages Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new PCGatewayMessages();
                }
                return _instance;
            }
        }

        public void SendPCStatusResponse(PCStatusResponse pCStatusResponse, object requestId, int installationNo)
        {
            try
            {
                MonTgt_H2G_PC_StatusResponse monTgt = new MonTgt_H2G_PC_StatusResponse()
                {
                    //Status = pCStatusResponse.Status,
                    IsPCEnrolled = pCStatusResponse.PCEnrolled,

                    IsDayTimeBasis = pCStatusResponse.IsDayTimeBasis,
                    DayTargetTime = TimeSpan.ParseExact(pCStatusResponse.DayTargetTime, "HHMM", CultureInfo.InvariantCulture),
                    DayCurrentTargetTime = TimeSpan.ParseExact(pCStatusResponse.CurrentDayTimeValue, "HHMM", CultureInfo.InvariantCulture),
                    
                    IsWeekTimeBasis = pCStatusResponse.IsWeekTimeBasis,
                    WeekTargetTime = Convert.ToByte(pCStatusResponse.WeekTargetTime),
                    WeekCurrentTargetTime = Convert.ToByte(pCStatusResponse.CurrentWeekTimeValue),

                    IsMonthTimeBasis = pCStatusResponse.IsMonthTimeBasis,
                    MonthTargetTime = Convert.ToByte(pCStatusResponse.MonthTargetTime),
                    MonthCurrentTargetTime = Convert.ToByte(pCStatusResponse.CurrentMonthTimeValue),

                    IsDayLossBasis = pCStatusResponse.IsDayLossBasis,
                    DayTargetLoss = pCStatusResponse.DayTargetLoss,
                    DayCurrentTargetLoss = pCStatusResponse.CurrentDayLossValue,

                    IsWeekLossBasis = pCStatusResponse.IsWeekLossBasis,
                    WeekTargetLoss = pCStatusResponse.WeekTargetLoss,
                    WeekCurrentTargetLoss = pCStatusResponse.CurrentWeekLossValue,

                    IsMonthLossBasis = pCStatusResponse.IsMonthLossBasis,
                    MonthTargetLoss = pCStatusResponse.MonthTargetLoss,
                    MonthCurrentTargetLoss = pCStatusResponse.CurrentMonthLossValue,

                    IsDayWagerBasis = pCStatusResponse.IsDayWagerBasis,
                    DayTargetWager = pCStatusResponse.DayTargetWagers,
                    DayCurrentTargetWager = pCStatusResponse.CurrentDayWagerValue,

                    IsWeekWagerBasis = pCStatusResponse.IsWeekWagerBasis,
                    WeekTargetWager = pCStatusResponse.WeekTargetWagers,
                    WeekCurrentTargetWager = pCStatusResponse.CurrentWeekWagerValue,

                    IsConsecutiveDaysBasis = pCStatusResponse.IsConsecutiveDaysBasis,
                    TargetConsecutiveDays = pCStatusResponse.TargetConsecutiveDays,
                    CurrentConsecutiveDays = pCStatusResponse.CurrentConsecutiveDays,

                    DisplayMessageLength = pCStatusResponse.DisplayMessage.Length,
                    DisplayMessage = pCStatusResponse.DisplayMessage
                };

                EPIMsgProcessor.Current.SendCommand(installationNo, monTgt);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        public void SendPCEnrollmentResponse(PCEnrollmentParameterResponse pCEnrollmentParameterResponse, object requestId, int installationNo)
        {
            try
            {
                MonTgt_H2G_PC_EnrollmentParameterResponse monTgt = new MonTgt_H2G_PC_EnrollmentParameterResponse()
                {
                    Status = Convert.ToByte(pCEnrollmentParameterResponse.ErrorCode),
                    IsDayTimeBasis = pCEnrollmentParameterResponse.IsDayTimeBasis,
                    DayDefaultTime = TimeSpan.ParseExact(pCEnrollmentParameterResponse.DayDefaultTime, "HHMM", CultureInfo.InvariantCulture),
                    IsDayTimeBasisMandatory = pCEnrollmentParameterResponse.IsDayTimeMandatory,

                    IsWeekTimeBasis = pCEnrollmentParameterResponse.IsWeekTimeBasis,
                    WeekDefaultTime = Convert.ToByte(pCEnrollmentParameterResponse.WeekDefaultTime),
                    IsWeekTimeBasisMandatory = pCEnrollmentParameterResponse.IsWeekTimeMandatory,

                    IsMonthTimeBasis = pCEnrollmentParameterResponse.IsMonthTimeBasis,
                    MonthDefaultTime = Convert.ToByte(pCEnrollmentParameterResponse.MonthDefaultTime),
                    IsMonthTimeBasisMandatory = pCEnrollmentParameterResponse.IsMonthTimeMandatory,

                    IsDayLossBasis = pCEnrollmentParameterResponse.IsDayLossBasis,
                    DayDefaultLossValue = pCEnrollmentParameterResponse.DayDefaultLoss,
                    IsDayLossBasisMandatory = pCEnrollmentParameterResponse.IsMonthLossMandatory,

                    IsWeekLossBasis = pCEnrollmentParameterResponse.IsWeekLossBasis,
                    WeekDefaultLossValue = pCEnrollmentParameterResponse.WeekDefaultLoss,
                    IsWeekLossBasisMandatory = pCEnrollmentParameterResponse.IsWeekLossMandatory,

                    IsDayWagerBasis = pCEnrollmentParameterResponse.IsDayWagerBasis,
                    DayDefaultWager = pCEnrollmentParameterResponse.DayDefaultWager,
                    IsDayWagerBasisMandatory = pCEnrollmentParameterResponse.IsDayWagerMandatory,

                    IsWeekWagerBasis = pCEnrollmentParameterResponse.IsWeekWagerBasis,
                    WeekDefaultWager = pCEnrollmentParameterResponse.WeekDefaultWager,
                    IsWeekWagerBasisMandatory = pCEnrollmentParameterResponse.IsWeekWagerMandatory,

                    IsMonthWagerBasis = pCEnrollmentParameterResponse.IsMonthWagerBasis,
                    MonthDefaultWager = pCEnrollmentParameterResponse.MonthDefaultWager,
                    IsMonthWagerBasisMandatory = pCEnrollmentParameterResponse.IsWeekWagerMandatory,

                    DisplayMessageLength = Convert.ToByte(pCEnrollmentParameterResponse.DisplayMessage.Length),
                    DisplayMessage = pCEnrollmentParameterResponse.DisplayMessage,
                };

                EPIMsgProcessor.Current.SendCommand(installationNo, monTgt);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        public void SendPCPlayerEnrollmentResponse(PCEnrollementResponse pCEnrollmentParameterResponse, object requestId, int installationNo)
        {
            try
            {
                MonTgt_H2G_PC_PlayerEnrollmentResponse monTgt = new MonTgt_H2G_PC_PlayerEnrollmentResponse()
                {
                    ErrorCode = Convert.ToByte(pCEnrollmentParameterResponse.ErrorCode),
                    DisplayMessageLength = pCEnrollmentParameterResponse.ErrorMessage.Length,
                    DisplayMessage = pCEnrollmentParameterResponse.ErrorMessage,
                };

                EPIMsgProcessor.Current.SendCommand(installationNo, monTgt);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
    }
}
