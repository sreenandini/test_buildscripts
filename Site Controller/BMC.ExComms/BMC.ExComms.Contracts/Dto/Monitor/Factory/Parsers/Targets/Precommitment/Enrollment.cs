using BMC.CoreLib;
using BMC.ExComms.Contracts.DTO.Freeform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Monitor
{
    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
                    typeof(FFTgt_G2H_PC_EnrollmentParameterRequest),
                    (int)FaultSource.Precommitment,
                    (int)FaultType_Precommitment.EnrollmentParamRequest,
                    new int[]
                    {
                        (int)FF_AppId_SessionIds.Pre_commitmentFacility,
                        (int)FF_AppId_TargetIds.Precommitment,
                        (int)FF_AppId_PreCommitment_Action.EnrollmentParam
                    })]
    internal class MonTgtParser_PC_EnrollmentParameterRequest_G2H
        : MonTgtParser_G2H
    {
        protected override IMonitorEntity_MsgTgt CreateMonitorTarget(IMonitorEntity parent, IFreeformEntity_MsgTgt request)
        {
            using (ILogMethod method = Log.LogMethod("MonTgtParser_PC_EnrollmentParameterRequest_G2H", "CreateMonitorTarget"))
            {
                try
                {
                    FFTgt_G2H_PC_EnrollmentParameterRequest ffTgt = request as FFTgt_G2H_PC_EnrollmentParameterRequest;
                    if (ffTgt == null) return null;

                    MonTgt_G2H_PC_EnrollmentParameterRequest monTgt = new MonTgt_G2H_PC_EnrollmentParameterRequest()
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
                    typeof(FFTgt_G2H_PC_StatusRequest),
                    (int)FaultSource.Precommitment,
                    (int)FaultType_Precommitment.EnrollmentParamResponse,
                    new int[]
                    {
                        (int)FF_AppId_SessionIds.Pre_commitmentFacility,
                        (int)FF_AppId_TargetIds.Precommitment,
                        (int)FF_AppId_PreCommitment_Action.EnrollmentParam
                    })]
    internal class MonTgtParser_PC_EnrollmentParameterResponse_H2G
        : MonTgtParser_H2G
    {
        protected override IFreeformEntity_MsgTgt CreateFreeformTarget(IMonitorEntity parent, IMonitorEntity_MsgTgt request)
        {
            using (ILogMethod method = Log.LogMethod("MonTgtParser_PC_EnrollmentParameterResponse_H2G", "CreateMonitorTarget"))
            {
                try
                {
                    MonTgt_H2G_PC_EnrollmentParameterResponse monTgt = request as MonTgt_H2G_PC_EnrollmentParameterResponse;
                    if (monTgt == null) return null;

                    FFTgt_H2G_PC_EnrollmentParameterResponse ffTgt = new FFTgt_H2G_PC_EnrollmentParameterResponse()
                    {
                        Status = monTgt.Status,
                        IsDayTimeBasis = monTgt.IsDayTimeBasis,
                        DayDefaultTime = monTgt.DayDefaultTime,
                        IsDayTimeBasisMandatory = monTgt.IsDayTimeBasisMandatory,

                        IsWeekTimeBasis = monTgt.IsWeekTimeBasis,
                        WeekDefaultTime = monTgt.WeekDefaultTime,
                        IsWeekTimeBasisMandatory = monTgt.IsWeekTimeBasisMandatory,

                        IsMonthTimeBasis = monTgt.IsMonthTimeBasis,
                        MonthDefaultTime = monTgt.MonthDefaultTime,
                        IsMonthTimeBasisMandatory = monTgt.IsMonthTimeBasisMandatory,

                        IsDayLossBasis = monTgt.IsDayLossBasis,
                        DayDefaultLossValue = monTgt.DayDefaultLossValue,
                        IsDayLossBasisMandatory = monTgt.IsMonthLossBasisMandatory,

                        IsWeekLossBasis = monTgt.IsWeekLossBasis,
                        WeekDefaultLossValue = monTgt.WeekDefaultLossValue,
                        IsWeekLossBasisMandatory = monTgt.IsWeekLossBasisMandatory,

                        IsDayWagerBasis = monTgt.IsDayWagerBasis,
                        DayDefaultWager = monTgt.DayDefaultWager,
                        IsDayWagerBasisMandatory = monTgt.IsDayWagerBasisMandatory,

                        IsWeekWagerBasis = monTgt.IsWeekWagerBasis,
                        WeekDefaultWager = monTgt.WeekDefaultWager,
                        IsWeekWagerBasisMandatory = monTgt.IsWeekWagerBasisMandatory,

                        IsMonthWagerBasis = monTgt.IsMonthWagerBasis,
                        MonthDefaultWager = monTgt.MonthDefaultWager,
                        IsMonthWagerBasisMandatory = monTgt.IsWeekWagerBasisMandatory,

                        DisplayMessageLength = monTgt.DisplayMessageLength,
                        DisplayMessage = monTgt.DisplayMessage,
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
                    typeof(FFTgt_G2H_PC_PlayerEnrollmentRequest),
                    (int)FaultSource.Precommitment,
                    (int)FaultType_Precommitment.PlayerEnrollmentRequest,
                    new int[]
                    {
                        (int)FF_AppId_SessionIds.Pre_commitmentFacility,
                        (int)FF_AppId_TargetIds.Precommitment,
                        (int)FF_AppId_PreCommitment_Action.EnrollmentResponse
                    })]
    internal class MonTgtParser_PC_PlayerEnrollmentRequest_G2H
        : MonTgtParser_G2H
    {
        protected override IMonitorEntity_MsgTgt CreateMonitorTarget(IMonitorEntity parent, IFreeformEntity_MsgTgt request)
        {
            using (ILogMethod method = Log.LogMethod("MonTgtParser_PC_PlayerEnrollmentRequest_G2H", "CreateMonitorTarget"))
            {
                try
                {
                    FFTgt_G2H_PC_PlayerEnrollmentRequest ffTgt = request as FFTgt_G2H_PC_PlayerEnrollmentRequest;
                    if (ffTgt == null) return null;

                    MonTgt_G2H_PC_PlayerEnrollmentRequest monTgt = new MonTgt_G2H_PC_PlayerEnrollmentRequest()
                    {
                        PlayerAccNoLen = ffTgt.PlayerAccNoLen,
                        PlayerAccNo = ffTgt.PlayerAccNo,
                        
                        IsDayTimeBasis = ffTgt.IsDayTimeBasis,
                        DayDefaultTime = ffTgt.DayDefaultTime,

                        IsWeekTimeBasis = ffTgt.IsWeekTimeBasis,
                        WeekTargetTime = ffTgt.WeekTargetTime,

                        IsMonthTimeBasis = ffTgt.IsMonthTimeBasis,
                        MonthTargetTime = ffTgt.MonthTargetTime,

                        IsDayLossBasis = ffTgt.IsDayLossBasis,
                        DayTargetLossValue = ffTgt.DayTargetLossValue,

                        IsWeekLossBasis = ffTgt.IsWeekLossBasis,
                        WeekTargetLossValue = ffTgt.WeekTargetLossValue,

                        IsMonthLossBasis = ffTgt.IsMonthLossBasis,
                        MonthTargetLossValue = ffTgt.MonthTargetLossValue,

                        IsDayWagerBasis = ffTgt.IsDayWagerBasis,
                        DayTargetWager = ffTgt.DayTargetWager,

                        IsWeekWagerBasis = ffTgt.IsWeekWagerBasis,
                        WeekTargetWager = ffTgt.WeekTargetWager,

                        IsMonthWagerBasis = ffTgt.IsMonthWagerBasis,
                        MonthTargetWager = ffTgt.MonthTargetWager,
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
                    typeof(FFTgt_H2G_PC_PlayerEnrollmentResponse),
                    (int)FaultSource.Precommitment,
                    (int)FaultType_Precommitment.EnrollmentParamResponse,
                    new int[]
                    {
                        (int)FF_AppId_SessionIds.Pre_commitmentFacility,
                        (int)FF_AppId_TargetIds.Precommitment,
                        (int)FF_AppId_PreCommitment_Action.EnrollmentResponse
                    })]
    internal class MonTgtParser_PC_PlayerEnrollmentResponse_H2G
        : MonTgtParser_H2G
    {
        protected override IFreeformEntity_MsgTgt CreateFreeformTarget(IMonitorEntity parent, IMonitorEntity_MsgTgt request)
        {
            using (ILogMethod method = Log.LogMethod("MonTgtParser_PC_PlayerEnrollmentResponse_H2G", "CreateMonitorTarget"))
            {
                try
                {
                    MonTgt_H2G_PC_PlayerEnrollmentResponse monTgt = request as MonTgt_H2G_PC_PlayerEnrollmentResponse;
                    if (monTgt == null) return null;

                    FFTgt_H2G_PC_PlayerEnrollmentResponse ffTgt = new FFTgt_H2G_PC_PlayerEnrollmentResponse()
                    {
                        ErrorCode = monTgt.ErrorCode,
                        DisplayMessageLength = monTgt.DisplayMessageLength,
                        DisplayMessage = monTgt.DisplayMessage,
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
