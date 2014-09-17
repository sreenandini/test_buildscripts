using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.ExComms.Contracts.DTO.Freeform;

namespace BMC.ExComms.Contracts.DTO.Monitor
{

    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
                    typeof(FFTgt_G2H_TPD),
                    (int)FaultSource.GMUVarAction,
                    (int)FaultType_GMUVarAction.TicketPrintDateRequest)]
        internal class MonTgtParser_GVA_TicketPrintDate_Request_G2H
        : MonTgtParser_G2H
        {
            protected override IMonitorEntity_MsgTgt CreateMonitorTarget(IMonitorEntity parent, IFreeformEntity_MsgTgt request)
            {
                FFTgt_G2H_TPD tgtSrc = request as FFTgt_G2H_TPD;
                if (tgtSrc != null)
                {
                    MonTgt_G2H_GVA_TicketPrintDate_Request tgtDest = new MonTgt_G2H_GVA_TicketPrintDate_Request();
                    return tgtDest;
                }
                return null;
            }
        }

    [MonTgtParserMappingH2G(FF_FlowDirection.H2G,
        typeof(MonTgt_H2G_GVA_TicketPrintDate_Response),
        (int)FaultSource.GMUVarAction,
        (int)FaultType_GMUVarAction.TicketPrintDateResponse,
        FF_AppId_H2G_PollCodes.FreeformResponse,
        FF_AppId_SessionIds.A1)]

        internal class MonTgtParser_GVA_TicketPrintDate_Request_H2G
           : MonTgtParser_H2G
        {
            protected override IFreeformEntity_MsgTgt CreateFreeformTarget(IMonitorEntity_MsgTgt request)
            {
                MonTgt_H2G_GVA_TicketPrintDate_Response tgtSrc = request as MonTgt_H2G_GVA_TicketPrintDate_Response;
                if (tgtSrc != null)
                {
                    FFTgt_B2B_GMUVarAction tgtDest = new FFTgt_B2B_GMUVarAction();
                    FFTgt_H2G_TPD_Request tgtSub = new FFTgt_H2G_TPD_Request()
                    {
                        Date = tgtSrc.TicketDate,
                    };

                    tgtDest.AddTarget(tgtSub);
                    return tgtDest;
                }
                return null;
            }
        }



     [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
                    typeof(FFTgt_G2H_TPD_Status),
                    (int)FaultSource.GMUVarAction,
                    (int)FaultType_GMUVarAction.TicketPrintDateStatus)]
        internal class MonTgtParser_GVA_TicketPrintDate_Status_G2H
            : MonTgtParser_G2H
        {
            protected override IMonitorEntity_MsgTgt CreateMonitorTarget(IMonitorEntity parent, IFreeformEntity_MsgTgt request)
            {
                FFTgt_G2H_TPD_Status tgtSrc = request as FFTgt_G2H_TPD_Status;
                if (tgtSrc != null)
                {
                    MonTgt_G2H_GVA_TicketPrintDate_Status tgtDest = new MonTgt_G2H_GVA_TicketPrintDate_Status()
                    {
                        Status = tgtSrc.Status,
                    };
                    return tgtDest;
                }
                return null;
            }
        }


}
