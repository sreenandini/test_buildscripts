using BMC.ExComms.Contracts.DTO.Freeform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Monitor
{
    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
                    typeof(FFTgt_G2H_GVA_EnablePrint_RT_Request),
                    (int)FaultSource.GMUVarAction,
                    (int)FaultType_GMUVarAction.EnablePrintingOfRestrictedTicketsRequest)]
    internal class MonTgtParser_GVA_EnablePrint_RT_Request_G2H
        : MonTgtParser_G2H
    {
        protected override IMonitorEntity_MsgTgt CreateMonitorTarget(IMonitorEntity parent, IFreeformEntity_MsgTgt request)
        {
            FFTgt_G2H_GVA_EnablePrint_RT_Request tgtSrc = request as FFTgt_G2H_GVA_EnablePrint_RT_Request;
            if (tgtSrc != null)
            {
                MonTgt_G2H_GVA_EnablePrint_RT_Request tgtDest = new MonTgt_G2H_GVA_EnablePrint_RT_Request();
                return tgtDest;
            }
            return null;
        }
    }

    [MonTgtParserMappingH2G(FF_FlowDirection.H2G,
        typeof(MonTgt_H2G_GVA_EnablePrint_RT_Response),
        (int)FaultSource.GMUVarAction,
        (int)FaultType_GMUVarAction.EnablePrintingOfRestrictedTicketsResponse,
        FF_AppId_H2G_PollCodes.FreeformResponse,
        FF_AppId_SessionIds.A1)]
    internal class MonTgtParser_GVA_EnablePrint_RT_Response_H2G
        : MonTgtParser_H2G
    {
        protected override IFreeformEntity_MsgTgt CreateFreeformTarget(IMonitorEntity_MsgTgt request)
        {
            MonTgt_H2G_GVA_EnablePrint_RT_Response tgtSrc = request as MonTgt_H2G_GVA_EnablePrint_RT_Response;
            if (tgtSrc != null)
            {
                FFTgt_B2B_GMUVarAction tgtDest = new FFTgt_B2B_GMUVarAction();
                FFTgt_H2G_GVA_EnablePrint_RT_Response tgtSub = new FFTgt_H2G_GVA_EnablePrint_RT_Response()
                {
                    EnableRestrictedTickets = tgtSrc.EnableRestrictedTickets,
                };

                tgtDest.AddTarget(tgtSub);
                return tgtDest;
            }
            return null;
        }
    }

    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
                    typeof(FFTgt_G2H_GVA_EnablePrint_RT_Status),
                    (int)FaultSource.GMUVarAction,
                    (int)FaultType_GMUVarAction.EnablePrintingOfRestrictedTicketsStatus)]
    internal class MonTgtParser_GVA_EnablePrint_RT_Status_G2H
        : MonTgtParser_G2H
    {
        protected override IMonitorEntity_MsgTgt CreateMonitorTarget(IMonitorEntity parent, IFreeformEntity_MsgTgt request)
        {
            FFTgt_G2H_GVA_EnablePrint_RT_Status tgtSrc = request as FFTgt_G2H_GVA_EnablePrint_RT_Status;
            if (tgtSrc != null)
            {
                MonTgt_G2H_GVA_EnablePrint_RT_Status tgtDest = new MonTgt_G2H_GVA_EnablePrint_RT_Status()
                {
                    Status = tgtSrc.Status,
                };
                return tgtDest;
            }
            return null;
        }
    }
}
