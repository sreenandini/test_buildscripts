using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.ExComms.Contracts.DTO.Freeform;

namespace BMC.ExComms.Contracts.DTO.Monitor
{
    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
                    typeof(FFTgt_G2H_EFT_BalanceRequest),
                    (int)FaultSource.ECash,
                    (int)FaultType_ECash.AFTAccountList)]
    internal class MonTgtParser_EFT_Balance_G2H
        : MonTgtParser_G2H
    {
        protected override IMonitorEntity_MsgTgt CreateMonitorTarget(IMonitorEntity parent, IFreeformEntity_MsgTgt request)
        {
            FFTgt_G2H_EFT_BalanceRequest ffTgt = request as FFTgt_G2H_EFT_BalanceRequest;
            if (ffTgt != null)
            {
                MonTgt_G2H_EFT_BalanceRequest monTgt = new MonTgt_G2H_EFT_BalanceRequest()
                {
                    PlayerCardNumber = ffTgt.PlayerCardNumber,
                    Pin = ffTgt.Pin,
                };
                return monTgt;
            }
            return null;
        }
    }
}
