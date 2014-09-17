using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.ExComms.Contracts.DTO.Freeform;

namespace BMC.ExComms.Contracts.DTO.Monitor
{
    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
                    typeof(FFTgt_G2H_GVA_EFT_MaxWithdraw_Request),
                    (int)FaultSource.GMUVarAction,
                    (int)FaultType_GMUVarAction.MaxWithdrawParameterRequest)]
    internal class MonTgtParser_GVA_EFTMaxwithdraw_Request_G2H
    : MonTgtParser_G2H
    {
        protected override IMonitorEntity_MsgTgt CreateMonitorTarget(IMonitorEntity parent, IFreeformEntity_MsgTgt request)
        {
            FFTgt_G2H_GVA_EFT_MaxWithdraw_Request tgtSrc = request as FFTgt_G2H_GVA_EFT_MaxWithdraw_Request;
            if (tgtSrc != null)
            {
                MonTgt_G2H_GVA_EFTMaxWithdraw_Request tgtDest = new MonTgt_G2H_GVA_EFTMaxWithdraw_Request();
                return tgtDest;
            }
            return null;
        }
    }

    [MonTgtParserMappingH2G(FF_FlowDirection.H2G,
      typeof(MonTgt_H2G_GVA_EFTMAxWithdraw_Response),
      (int)FaultSource.GMUVarAction,
      (int)FaultType_GMUVarAction.MaxWithdrawParameterResponse,
      FF_AppId_H2G_PollCodes.FreeformResponse,
      FF_AppId_SessionIds.A1)]

    internal class MonTgtParser_GVA_EFTMAxWithdraw_Request_H2G
       : MonTgtParser_H2G
    {
        protected override IFreeformEntity_MsgTgt CreateFreeformTarget(IMonitorEntity_MsgTgt request)
        {
            MonTgt_H2G_GVA_EFTMAxWithdraw_Response tgtSrc = request as MonTgt_H2G_GVA_EFTMAxWithdraw_Response;
            if (tgtSrc != null)
            {
                FFTgt_B2B_GMUVarAction tgtDest = new FFTgt_B2B_GMUVarAction();
                FFTgt_H2G_GVA_EFT_MaxWithdraw_Response tgtSub = new FFTgt_H2G_GVA_EFT_MaxWithdraw_Response()
                {
                   MaxElectronicWithdrawalAmount=tgtSrc.MaxWithdrawalAmount
                };

                tgtDest.AddTarget(tgtSub);
                return tgtDest;
            }
            return null;
        }
    }
}
