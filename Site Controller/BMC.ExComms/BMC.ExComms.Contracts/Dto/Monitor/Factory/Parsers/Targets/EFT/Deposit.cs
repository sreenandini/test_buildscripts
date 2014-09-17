using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.ExComms.Contracts.DTO.Freeform;

namespace BMC.ExComms.Contracts.DTO.Monitor
{
    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
                    typeof(FFTgt_G2H_EFT_DepositRequest),
                    (int)FaultSource.ECash,
                    (int)FaultType_ECash.DepositRequest,
                    new int[]
                    {
                        (int)FF_AppId_SessionIds.ECash,
                        (int)FF_AppId_TargetIds.ECash,
                        (int)FF_AppId_EFT_TransactionTypes.DepositRequest
                    })]
    internal class MonTgtParser_EFT_DepositRequest_G2H
        : MonTgtParser_G2H
    {
        protected override IMonitorEntity_MsgTgt CreateMonitorTarget(IMonitorEntity parent, IFreeformEntity_MsgTgt request)
        {
            FFTgt_G2H_EFT_DepositRequest ffTgt = request as FFTgt_G2H_EFT_DepositRequest;
            if (ffTgt != null)
            {
                MonTgt_G2H_EFT_DepositRequest monTgt = new MonTgt_G2H_EFT_DepositRequest()
                {
                    NonCashableAmount = ffTgt.NonCashableAmount,
                    CashableAmount = ffTgt.CashableAmount,
                    PlayerCardNumber = ffTgt.PlayerCardNumber,
                    Pin = ffTgt.Pin,
                };
                return monTgt;
            }
            return null;
        }
    }

    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
                    typeof(FFTgt_G2H_EFT_DepositComplete),
                    (int)FaultSource.ECash,
                    (int)FaultType_ECash.Deposit,
                    new int[]
                    {
                        (int)FF_AppId_SessionIds.ECash,
                        (int)FF_AppId_TargetIds.ECash,
                        (int)FF_AppId_EFT_TransactionTypes.DepositComplete
                    })]
    internal class MonTgtParser_EFT_DepositComplete_G2H
        : MonTgtParser_G2H
    {
        protected override IMonitorEntity_MsgTgt CreateMonitorTarget(IMonitorEntity parent, IFreeformEntity_MsgTgt request)
        {
            FFTgt_G2H_EFT_DepositComplete ffTgt = request as FFTgt_G2H_EFT_DepositComplete;
            if (ffTgt != null)
            {
                MonTgt_G2H_EFT_DepositComplete monTgt = new MonTgt_G2H_EFT_DepositComplete()
                {
                    NonCashableAmount = ffTgt.NonCashableAmount,
                    CashableAmount = ffTgt.CashableAmount,
                    GMUErrorCode = ffTgt.ErrorCode,
                    PlayerCardNumber = ffTgt.PlayerCardNumber
                };
                return monTgt;
            }
            return null;
        }
    }
}
