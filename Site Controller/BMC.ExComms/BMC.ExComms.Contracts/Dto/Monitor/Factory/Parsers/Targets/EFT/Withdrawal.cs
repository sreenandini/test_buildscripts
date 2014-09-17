using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.ExComms.Contracts.DTO.Freeform;

namespace BMC.ExComms.Contracts.DTO.Monitor
{
    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
                    typeof(FFTgt_G2H_EFT_WithdrawalRequest),
                    (int)FaultSource.ECash,
                    (int)FaultType_ECash.WithdrawalRequest)]
    internal class MonTgtParser_EFT_WithdrawalRequest_G2H
        : MonTgtParser_G2H
    {
        protected override IMonitorEntity_MsgTgt CreateMonitorTarget(IMonitorEntity parent, IFreeformEntity_MsgTgt request)
        {
            FFTgt_G2H_EFT_WithdrawalRequest ffTgt = request as FFTgt_G2H_EFT_WithdrawalRequest;
            if (ffTgt != null)
            {
                MonTgt_G2H_EFT_WithdrawalRequest monTgt = new MonTgt_G2H_EFT_WithdrawalRequest()
                {
                    AccountType = ffTgt.AccountType,
                    AmountRequested = ffTgt.AmountRequested,
                    PlayerCardNumber = ffTgt.PlayerCardNumber,
                    Pin = ffTgt.Pin,
                };
                return monTgt;
            }
            return null;
        }
    }
    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
                    typeof(FFTgt_G2H_EFT_WithdrawalComplete),
                    (int)FaultSource.ECash,
                    (int)FaultType_ECash.WithdrawalComplete)]
    internal class MonTgtParser_EFT_WithdrawalComplete_G2H
        : MonTgtParser_G2H
    {
        protected override IMonitorEntity_MsgTgt CreateMonitorTarget(IMonitorEntity parent, IFreeformEntity_MsgTgt request)
        {
            FFTgt_G2H_EFT_WithdrawalComplete ffTgt = request as FFTgt_G2H_EFT_WithdrawalComplete;
            if (ffTgt != null)
            {
                MonTgt_G2H_EFT_WithdrawalComplete monTgt = new MonTgt_G2H_EFT_WithdrawalComplete()
                {
                    NonCashableAmount = ffTgt.NonCashableAmount,
                    CashableAmount = ffTgt.CashableAmount,
                    //ErrorCode = ffTgt.ErrorCode,
                    PlayerCardNumber = ffTgt.PlayerCardNumber,
                };
                return monTgt;
            }
            return null;
        }
    }
}
