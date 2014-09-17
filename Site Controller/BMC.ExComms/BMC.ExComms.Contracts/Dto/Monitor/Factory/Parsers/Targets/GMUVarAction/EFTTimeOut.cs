using BMC.ExComms.Contracts.DTO.Freeform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Monitor
{
    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
                    typeof(FFTgt_G2H_GVA_EFT_TTimeOut_Request),
                    (int)FaultSource.GMUVarAction,
                    (int)FaultType_GMUVarAction.EFTTransactionTimeoutRequest)]
    internal class MonTgtParser_GVA_EFT_TTimeOut_Request_G2H
        : MonTgtParser_G2H
    {
        protected override IMonitorEntity_MsgTgt CreateMonitorTarget(IMonitorEntity parent, IFreeformEntity_MsgTgt request)
        {
            FFTgt_G2H_GVA_EFT_TTimeOut_Request tgtSrc = request as FFTgt_G2H_GVA_EFT_TTimeOut_Request;
            if (tgtSrc != null)
            {
                MonTgt_G2H_GVA_EFT_TTimeOut_Request tgtDest = new MonTgt_G2H_GVA_EFT_TTimeOut_Request();
                return tgtDest;
            }
            return null;
        }
    }

    [MonTgtParserMappingH2G(FF_FlowDirection.H2G,
        typeof(MonTgt_H2G_GVA_EFT_TTimeOut_Response),
        (int)FaultSource.GMUVarAction,
        (int)FaultType_GMUVarAction.EFTTransactionTimeoutResponse,
        FF_AppId_H2G_PollCodes.FreeformResponse,
        FF_AppId_SessionIds.A1)]
    internal class MonTgtParser_GVA_EFT_TTimeOut_Response_H2G
        : MonTgtParser_H2G
    {
        protected override IFreeformEntity_MsgTgt CreateFreeformTarget(IMonitorEntity_MsgTgt request)
        {
            MonTgt_H2G_GVA_EFT_TTimeOut_Response tgtSrc = request as MonTgt_H2G_GVA_EFT_TTimeOut_Response;
            if (tgtSrc != null)
            {
                FFTgt_B2B_GMUVarAction tgtDest = new FFTgt_B2B_GMUVarAction();
                FFTgt_H2G_GVA_EFT_TTimeOut_Response tgtSub = new FFTgt_H2G_GVA_EFT_TTimeOut_Response()
                {
                    TimeOut = tgtSrc.TimeOut,
                };

                tgtDest.AddTarget(tgtSub);
                return tgtDest;
            }
            return null;
        }
    }

    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
                    typeof(FFTgt_G2H_GVA_EFT_TTimeOut_Status),
                    (int)FaultSource.GMUVarAction,
                    (int)FaultType_GMUVarAction.EFTTransactionTimeoutStatus)]
    internal class MonTgtParser_GVA_EFT_TTimeOut_Status_G2H
        : MonTgtParser_G2H
    {
        protected override IMonitorEntity_MsgTgt CreateMonitorTarget(IMonitorEntity parent, IFreeformEntity_MsgTgt request)
        {
            FFTgt_G2H_GVA_EFT_TTimeOut_Status tgtSrc = request as FFTgt_G2H_GVA_EFT_TTimeOut_Status;
            if (tgtSrc != null)
            {
                MonTgt_G2H_GVA_EFT_TTimeOut_Status tgtDest = new MonTgt_G2H_GVA_EFT_TTimeOut_Status()
                {
                    Status = tgtSrc.Status,
                };
                return tgtDest;
            }
            return null;
        }
    }
}
