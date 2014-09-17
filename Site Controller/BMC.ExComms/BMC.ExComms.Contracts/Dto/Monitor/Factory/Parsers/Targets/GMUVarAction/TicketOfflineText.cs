using BMC.ExComms.Contracts.DTO.Freeform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Monitor
{
    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
                    typeof(FFTgt_G2H_GVA_OfflineT_TxtLine1_Req),
                    (int)FaultSource.GMUVarAction,
                    (int)FaultType_GMUVarAction.OfflineTicketTextLine1Request)]
    internal class MonTgtParser_GVA_OfflineT_TxtLine1_Request_G2H
        : MonTgtParser_G2H
    {
        protected override IMonitorEntity_MsgTgt CreateMonitorTarget(IMonitorEntity parent, IFreeformEntity_MsgTgt request)
        {
            FFTgt_G2H_GVA_OfflineT_TxtLine1_Req tgtSrc = request as FFTgt_G2H_GVA_OfflineT_TxtLine1_Req;
            if (tgtSrc != null)
            {
                MonTgt_G2H_GVA_OfflineT_TxtLine1_Req tgtDest = new MonTgt_G2H_GVA_OfflineT_TxtLine1_Req();
                return tgtDest;
            }
            return null;
        }
    }

    [MonTgtParserMappingH2G(FF_FlowDirection.H2G,
        typeof(MonTgt_H2G_GVA_OfflineT_TxtLine1_Resp),
        (int)FaultSource.GMUVarAction,
        (int)FaultType_GMUVarAction.OfflineTicketTextLine1Response,
        FF_AppId_H2G_PollCodes.FreeformResponse,
        FF_AppId_SessionIds.A1)]
    internal class MonTgtParser_GVA_OfflineT_TxtLine1_Response_H2G
        : MonTgtParser_H2G
    {
        protected override IFreeformEntity_MsgTgt CreateFreeformTarget(IMonitorEntity_MsgTgt request)
        {
            MonTgt_H2G_GVA_OfflineT_TxtLine1_Resp tgtSrc = request as MonTgt_H2G_GVA_OfflineT_TxtLine1_Resp;
            if (tgtSrc != null)
            {
                FFTgt_B2B_GMUVarAction tgtDest = new FFTgt_B2B_GMUVarAction();
                FFTgt_H2G_GVA_OfflineT_TxtLine1_Resp tgtSub = new FFTgt_H2G_GVA_OfflineT_TxtLine1_Resp()
                {
                    Line1Text = tgtSrc.Line1Text,
                };

                tgtDest.AddTarget(tgtSub);
                return tgtDest;
            }
            return null;
        }
    }

    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
                    typeof(FFTgt_G2H_GVA_OfflineT_TxtLine2_Req),
                    (int)FaultSource.GMUVarAction,
                    (int)FaultType_GMUVarAction.OfflineTicketTextLine2Request)]
    internal class MonTgtParser_GVA_OfflineT_TxtLine2_Request_G2H
        : MonTgtParser_G2H
    {
        protected override IMonitorEntity_MsgTgt CreateMonitorTarget(IMonitorEntity parent, IFreeformEntity_MsgTgt request)
        {
            FFTgt_G2H_GVA_OfflineT_TxtLine2_Req tgtSrc = request as FFTgt_G2H_GVA_OfflineT_TxtLine2_Req;
            if (tgtSrc != null)
            {
                MonTgt_G2H_GVA_OfflineT_TxtLine2_Req tgtDest = new MonTgt_G2H_GVA_OfflineT_TxtLine2_Req();
                return tgtDest;
            }
            return null;
        }
    }

    [MonTgtParserMappingH2G(FF_FlowDirection.H2G,
        typeof(MonTgt_H2G_GVA_OfflineT_TxtLine2_Resp),
        (int)FaultSource.GMUVarAction,
        (int)FaultType_GMUVarAction.OfflineTicketTextLine2Response,
        FF_AppId_H2G_PollCodes.FreeformResponse,
        FF_AppId_SessionIds.A1)]
    internal class MonTgtParser_GVA_OfflineT_TxtLine2_Response_H2G
        : MonTgtParser_H2G
    {
        protected override IFreeformEntity_MsgTgt CreateFreeformTarget(IMonitorEntity_MsgTgt request)
        {
            MonTgt_H2G_GVA_OfflineT_TxtLine2_Resp tgtSrc = request as MonTgt_H2G_GVA_OfflineT_TxtLine2_Resp;
            if (tgtSrc != null)
            {
                FFTgt_B2B_GMUVarAction tgtDest = new FFTgt_B2B_GMUVarAction();
                FFTgt_H2G_GVA_OfflineT_TxtLine2_Resp tgtSub = new FFTgt_H2G_GVA_OfflineT_TxtLine2_Resp()
                {
                    Line2Text = tgtSrc.Line2Text,
                };

                tgtDest.AddTarget(tgtSub);
                return tgtDest;
            }
            return null;
        }
    }
}
