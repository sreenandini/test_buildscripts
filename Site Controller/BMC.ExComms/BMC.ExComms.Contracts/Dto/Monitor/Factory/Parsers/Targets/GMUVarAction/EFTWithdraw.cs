using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.ExComms.Contracts.DTO.Freeform;

namespace BMC.ExComms.Contracts.DTO.Monitor
{
    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
                    typeof(FFTgt_G2H_GVA_EFT_Withdraw_Request),
                    (int)FaultSource.GMUVarAction,
                    (int)FaultType_GMUVarAction.EFTWithdrawalAmountsRequest)]
    internal class MonTgtParser_GVA_EFTwithdraw_Request_G2H
    : MonTgtParser_G2H
    {
        protected override IMonitorEntity_MsgTgt CreateMonitorTarget(IMonitorEntity parent, IFreeformEntity_MsgTgt request)
        {
            FFTgt_G2H_GVA_EFT_Withdraw_Request tgtSrc = request as FFTgt_G2H_GVA_EFT_Withdraw_Request;
            if (tgtSrc != null)
            {
                MonTgt_G2H_GVA_EFTWithdraw_Request tgtDest = new MonTgt_G2H_GVA_EFTWithdraw_Request();
                return tgtDest;
            }
            return null;
        }
    }
    [MonTgtParserMappingH2G(FF_FlowDirection.H2G,
        typeof(FFTgt_H2G_GVA_EFT_Withdraw_Response),
        (int)FaultSource.GMUVarAction,
        (int)FaultType_GMUVarAction.EFTWithdrawalAmountsResponse,
        FF_AppId_H2G_PollCodes.FreeformResponse,
        FF_AppId_SessionIds.A1)]

    internal class MonTgtParser_GVA_EFTWithdraw_Request_H2G
       : MonTgtParser_H2G
    {
        protected override IFreeformEntity_MsgTgt CreateFreeformTarget(IMonitorEntity_MsgTgt request)
        {
            MonTgt_H2G_GVA_EFTWithdraw_Response tgtSrc = request as MonTgt_H2G_GVA_EFTWithdraw_Response;
            if (tgtSrc != null)
            {
                FFTgt_B2B_GMUVarAction tgtDest = new FFTgt_B2B_GMUVarAction();
                FFTgt_H2G_GVA_EFT_Withdraw_Response tgtSub = new FFTgt_H2G_GVA_EFT_Withdraw_Response()
                {
                  WithdrawalAmount_option1=tgtSrc.WithdrawalAmount_option1,
                  WithdrawalAmount_option2 = tgtSrc.WithdrawalAmount_option2,
                  WithdrawalAmount_option3 = tgtSrc.WithdrawalAmount_option3,
                  WithdrawalAmount_option4 = tgtSrc.WithdrawalAmount_option4
                };

                tgtDest.AddTarget(tgtSub);
                return tgtDest;
            }
            return null;
        }
    }
     [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
                    typeof(FFTgt_G2H_EFT_Withdraw_Status),
                    (int)FaultSource.GMUVarAction,
                    (int)FaultType_GMUVarAction.EFTWithdrawalAmountsStatus)]
    internal class MonTgtParser_GVA_EFTWithdraw_Status_G2H
         : MonTgtParser_G2H
    {
        protected override IMonitorEntity_MsgTgt CreateMonitorTarget(IMonitorEntity parent, IFreeformEntity_MsgTgt request)
        {
            FFTgt_G2H_EFT_Withdraw_Status tgtSrc = request as FFTgt_G2H_EFT_Withdraw_Status;
            if (tgtSrc != null)
            {
                MonTgt_G2H_GVA_EFTWitjhdraw_Status tgtDest = new MonTgt_G2H_GVA_EFTWitjhdraw_Status()
                {
                    Status = tgtSrc.Status,
                };
                return tgtDest;
            }
            return null;
        }
    }

}
