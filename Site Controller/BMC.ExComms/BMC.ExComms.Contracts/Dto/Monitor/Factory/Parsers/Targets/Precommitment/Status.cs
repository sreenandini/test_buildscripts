using BMC.CoreLib;
using BMC.ExComms.Contracts.DTO.Freeform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Monitor
{
    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
                    typeof(FFTgt_G2H_PC_StatusRequest),
                    (int)FaultSource.Precommitment,
                    (int)FaultType_Precommitment.StatusRequest,
                    new int[]
                    {
                        (int)FF_AppId_SessionIds.Pre_commitmentFacility,
                        (int)FF_AppId_TargetIds.Precommitment,
                        (int)FF_AppId_PreCommitment_Action.Status
                    })]
    internal class MonTgtParser_PC_StatusRequest_G2H
        : MonTgtParser_G2H
    {
        protected override IMonitorEntity_MsgTgt CreateMonitorTarget(IMonitorEntity parent, IFreeformEntity_MsgTgt request)
        {
            using (ILogMethod method = Log.LogMethod("MonTgtParser_PC_StatusRequest_G2H", "CreateMonitorTarget"))
            {
                try
                {
                    FFTgt_G2H_PC_StatusRequest ffTgt = request as FFTgt_G2H_PC_StatusRequest;
                    if (ffTgt == null) return null;

                    MonTgt_G2H_PC_StatusRequest monTgt = new MonTgt_G2H_PC_StatusRequest()
                    {
                        PlayerAccNoLen = ffTgt.PlayerAccNoLen,
                        PlayerAccNo = ffTgt.PlayerAccNo,
                        PlayerPIN = ffTgt.PlayerPIN,
                    };
                    return monTgt;
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                    return null;
                }
            }
        }
    }

    [MonTgtParserMappingG2H(FF_FlowDirection.H2G,
                    typeof(FFTgt_H2G_PC_StatusResponse),
                    (int)FaultSource.Precommitment,
                    (int)FaultType_Precommitment.StatusResponse,
                    new int[]
                    {
                        (int)FF_AppId_SessionIds.Pre_commitmentFacility,
                        (int)FF_AppId_TargetIds.Precommitment,
                        (int)FF_AppId_PreCommitment_Action.Status
                    })]
    internal class MonTgtParser_PC_StatusResponse_H2G
        : MonTgtParser_H2G
    {
        protected override IFreeformEntity_MsgTgt CreateFreeformTarget(IMonitorEntity parent, IMonitorEntity_MsgTgt request)
        {
            using (ILogMethod method = Log.LogMethod("MonTgtParser_PC_StatusResponse_H2G", "CreateMonitorTarget"))
            {
                try
                {
                    MonTgt_H2G_PC_StatusResponse monTgt = request as MonTgt_H2G_PC_StatusResponse;
                    if (monTgt == null) return null;

                    FFTgt_H2G_PC_StatusResponse ffTgt = new FFTgt_H2G_PC_StatusResponse()
                    {
                        Status = monTgt.Status,
                        IsPCEnrolled = monTgt.IsPCEnrolled,

                        IsDayTimeBasis = monTgt.IsDayTimeBasis,
                        DayTargetTime = monTgt.DayTargetTime,
                        DayCurrentTargetTime = monTgt.DayCurrentTargetTime,

                        IsWeekTimeBasis = monTgt.IsWeekTimeBasis,
                        WeekTargetTime = monTgt.WeekTargetTime,
                        WeekCurrentTargetTime = monTgt.WeekCurrentTargetTime,

                        IsMonthTimeBasis = monTgt.IsMonthTimeBasis,
                        MonthTargetTime = monTgt.MonthTargetTime,
                        MonthCurrentTargetTime = monTgt.MonthCurrentTargetTime,

                        IsDayLossBasis = monTgt.IsDayLossBasis,
                        DayTargetLoss = monTgt.DayTargetLoss,
                        DayCurrentTargetLoss = monTgt.DayCurrentTargetLoss,

                        IsWeekLossBasis = monTgt.IsWeekLossBasis,
                        WeekTargetLoss = monTgt.WeekTargetLoss,
                        WeekCurrentTargetLoss = monTgt.WeekCurrentTargetLoss,

                        IsMonthLossBasis = monTgt.IsMonthLossBasis,
                        MonthTargetLoss = monTgt.MonthTargetLoss,
                        MonthCurrentTargetLoss = monTgt.MonthCurrentTargetLoss,

                        IsDayWagerBasis = monTgt.IsDayWagerBasis,
                        DayTargetWager = monTgt.DayTargetWager,
                        DayCurrentTargetWager = monTgt.DayCurrentTargetWager,

                        IsWeekWagerBasis = monTgt.IsWeekWagerBasis,
                        WeekTargetWager = monTgt.WeekTargetWager,
                        WeekCurrentTargetWager = monTgt.WeekCurrentTargetWager,

                        IsConsecutiveDaysBasis = monTgt.IsConsecutiveDaysBasis,
                        TargetConsecutiveDays = monTgt.TargetConsecutiveDays,
                        CurrentConsecutiveDays = monTgt.CurrentConsecutiveDays,

                        DisplayMessageLength = monTgt.DisplayMessageLength,
                        DisplayMessage = monTgt.DisplayMessage
                    };
                    return ffTgt;
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                    return null;
                }
            }
        }
    }

    [MonTgtParserMappingG2H(FF_FlowDirection.H2G,
                    typeof(FFTgt_H2G_PC_StatusResponsePlayerCardIn),
                    (int)FaultSource.Precommitment,
                    (int)FaultType_Precommitment.StatusResponsePlayerCardIn,
                    new int[]
                    {
                        (int)FF_AppId_SessionIds.Pre_commitmentFacility,
                        (int)FF_AppId_TargetIds.Precommitment,
                        (int)FF_AppId_PreCommitment_Action.Status
                    })]
    internal class MonTgtParser_PC_StatusResponsePlayerCardIn_H2G
        : MonTgtParser_H2G
    {
        protected override IFreeformEntity_MsgTgt CreateFreeformTarget(IMonitorEntity parent, IMonitorEntity_MsgTgt request)
        {
            using (ILogMethod method = Log.LogMethod("MonTgtParser_PC_StatusResponsePlayerCardIn_H2G", "CreateMonitorTarget"))
            {
                try
                {
                    MonTgt_H2G_PC_StatusResponsePlayerCardIn monTgt = request as MonTgt_H2G_PC_StatusResponsePlayerCardIn;
                    if (monTgt == null) return null;

                    FFTgt_H2G_PC_StatusResponsePlayerCardIn ffTgt = new FFTgt_H2G_PC_StatusResponsePlayerCardIn()
                    {
                        PCEnrolled = monTgt.PCEnrolled,
                        HandlePulls = monTgt.HandlePulls,
                        RatingInterval = monTgt.RatingInterval,
                    };
                    return ffTgt;
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                    return null;
                }
            }
        }
    }
}
