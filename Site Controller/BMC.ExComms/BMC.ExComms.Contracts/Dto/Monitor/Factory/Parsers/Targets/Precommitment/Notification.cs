using BMC.CoreLib;
using BMC.ExComms.Contracts.DTO.Freeform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Monitor
{
    [MonTgtParserMappingG2H(FF_FlowDirection.H2G,
                    typeof(FFTgt_H2G_PC_ApproachingNotificationMessage),
                    (int)FaultSource.Precommitment,
                    (int)FaultType_Precommitment.ApproachingLimit,
                    new int[]
                    {
                        (int)FF_AppId_SessionIds.Pre_commitmentFacility,
                        (int)FF_AppId_TargetIds.Precommitment,
                        (int)FF_AppId_PreCommitment_Action.ApproachingLimit
                    })]
    internal class MonTgtParser_PC_ApproachingNotificationMessage_H2G
        : MonTgtParser_H2G
    {
        protected override IFreeformEntity_MsgTgt CreateFreeformTarget(IMonitorEntity parent, IMonitorEntity_MsgTgt request)
        {
            using (ILogMethod method = Log.LogMethod("MonTgtParser_PC_ApproachingNotificationMessage_H2G", "CreateMonitorTarget"))
            {
                try
                {
                    MonTgt_H2G_PC_ApproachingNotificationMessage monTgt = request as MonTgt_H2G_PC_ApproachingNotificationMessage;
                    if (monTgt == null) return null;

                    FFTgt_H2G_PC_ApproachingNotificationMessage ffTgt = new FFTgt_H2G_PC_ApproachingNotificationMessage()
                    {
                        DisplayTime = monTgt.DisplayTime,
                        DisplayInterval = monTgt.DisplayInterval,
                        HandlePulls = monTgt.HandlePulls,
                        RatingInterval = monTgt.RatingInterval,
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
                    typeof(FFTgt_H2G_PC_LimitReachedNotificationMessage),
                    (int)FaultSource.Precommitment,
                    (int)FaultType_Precommitment.LimiReached,
                    new int[]
                    {
                        (int)FF_AppId_SessionIds.Pre_commitmentFacility,
                        (int)FF_AppId_TargetIds.Precommitment,
                        (int)FF_AppId_PreCommitment_Action.LimitReached
                    })]
    internal class MonTgtParser_PC_LimitReachedNotificationMessage_H2G
        : MonTgtParser_H2G
    {
        protected override IFreeformEntity_MsgTgt CreateFreeformTarget(IMonitorEntity parent, IMonitorEntity_MsgTgt request)
        {
            using (ILogMethod method = Log.LogMethod("MonTgtParser_PC_LimitReachedNotificationMessage_H2G", "CreateMonitorTarget"))
            {
                try
                {
                    MonTgt_H2G_PC_LimitReachedNotificationMessage monTgt = request as MonTgt_H2G_PC_LimitReachedNotificationMessage;
                    if (monTgt == null) return null;

                    FFTgt_H2G_PC_LimitReachedNotificationMessage ffTgt = new FFTgt_H2G_PC_LimitReachedNotificationMessage()
                    {
                        LockType = monTgt.LockType,
                        DisplayTime = monTgt.DisplayTime,
                        DisplayInterval = monTgt.DisplayInterval,
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
                    typeof(FFTgt_H2G_PC_RelaxedLimitEffectiveNotificationMsg),
                    (int)FaultSource.Precommitment,
                    (int)FaultType_Precommitment.RelaxedLimit,
                    new int[]
                    {
                        (int)FF_AppId_SessionIds.Pre_commitmentFacility,
                        (int)FF_AppId_TargetIds.Precommitment,
                        (int)FF_AppId_PreCommitment_Action.RelaxedLimit
                    })]
    internal class MonTgtParser_PC_RelaxedLimitEffectiveNotificationMsg_H2G
        : MonTgtParser_H2G
    {
        protected override IFreeformEntity_MsgTgt CreateFreeformTarget(IMonitorEntity parent, IMonitorEntity_MsgTgt request)
        {
            using (ILogMethod method = Log.LogMethod("MonTgtParser_PC_RelaxedLimitEffectiveNotificationMsg_H2G", "CreateMonitorTarget"))
            {
                try
                {
                    MonTgt_H2G_PC_RelaxedLimitEffectiveNotificationMsg monTgt = request as MonTgt_H2G_PC_RelaxedLimitEffectiveNotificationMsg;
                    if (monTgt == null) return null;

                    FFTgt_H2G_PC_RelaxedLimitEffectiveNotificationMsg ffTgt = new FFTgt_H2G_PC_RelaxedLimitEffectiveNotificationMsg()
                    {
                        IsDayTimeBasisChanged = monTgt.IsDayTimeBasisChanged,
                        DayNewTargetTime = monTgt.DayNewTargetTime,
                        DayOldTargetTime = monTgt.DayOldTargetTime,

                        IsWeekTimeBasisChanged = monTgt.IsWeekTimeBasisChanged,
                        WeekNewTargetTime = monTgt.WeekNewTargetTime,
                        WeekOldTargetTime = monTgt.WeekOldTargetTime,

                        IsMonthTimeBasisChanged = monTgt.IsMonthTimeBasisChanged,
                        MonthNewTargetTime = monTgt.MonthNewTargetTime,
                        MonthOldTargetTime = monTgt.MonthOldTargetTime,

                        IsDayLossBasisChanged = monTgt.IsDayLossBasisChanged,
                        DayNewTargetLoss = monTgt.DayNewTargetLoss,
                        DayOldTargetLoss = monTgt.DayOldTargetLoss,

                        IsWeekLossBasisChanged = monTgt.IsWeekLossBasisChanged,
                        WeekNewTargetLoss = monTgt.WeekNewTargetLoss,
                        WeekOldTargetLoss = monTgt.WeekOldTargetLoss,

                        IsMonthLossBasisChanged = monTgt.IsMonthLossBasisChanged,
                        MonthNewTargetLoss = monTgt.MonthNewTargetLoss,
                        MonthOldTargetLoss = monTgt.MonthOldTargetLoss,

                        IsDayWagerBasisChanged = monTgt.IsDayWagerBasisChanged,
                        DayNewTargetWager = monTgt.DayNewTargetWager,
                        DayOldTargetWager = monTgt.DayOldTargetWager,

                        IsWeekWagerBasisChanged = monTgt.IsWeekWagerBasisChanged,
                        WeekNewTargetWager = monTgt.WeekNewTargetWager,
                        WeekOldTargetWager = monTgt.WeekOldTargetWager,

                        IsMonthWagerBasisChanged = monTgt.IsMonthWagerBasisChanged,
                        MonthNewTargetWager = monTgt.MonthNewTargetWager,
                        MonthOldTargetWager = monTgt.MonthOldTargetWager,
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

    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
                    typeof(FFTgt_G2H_PC_NotificationResponse),
                    (int)FaultSource.Precommitment,
                    (int)FaultType_Precommitment.NotificationResponse,
                    new int[]
                    {
                        (int)FF_AppId_SessionIds.Pre_commitmentFacility,
                        (int)FF_AppId_TargetIds.Precommitment,
                        (int)FF_AppId_PreCommitment_Action.NotificationResponseAck
                    })]
    internal class MonTgtParser_PC_NotificationResponse_G2H
        : MonTgtParser_G2H
    {
        protected override IMonitorEntity_MsgTgt CreateMonitorTarget(IMonitorEntity parent, IFreeformEntity_MsgTgt request)
        {
            using (ILogMethod method = Log.LogMethod("MonTgtParser_PC_NotificationResponse_G2H", "CreateMonitorTarget"))
            {
                try
                {
                    FFTgt_G2H_PC_NotificationResponse ffTgt = request as FFTgt_G2H_PC_NotificationResponse;
                    if (ffTgt == null) return null;

                    MonTgt_G2H_PC_NotificationResponse monTgt = new MonTgt_G2H_PC_NotificationResponse()
                    {
                        AcknowledgementType = ffTgt.AcknowledgementType,
                        PlayerAccNoLen= ffTgt.PlayerAccNoLen,
                        PlayerAccNo = ffTgt.PlayerAccNo
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
}
