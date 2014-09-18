using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{    
    internal class FFParser_Tgt_Generic_Ticket_Enablement 
        : FFTgtParser
    {
        internal FFParser_Tgt_Generic_Ticket_Enablement()
            : base() { }
    }

    internal class FFParser_Tgt_MC300_Ticket_Enablement 
        : FFParser_Tgt_Generic_Ticket_Enablement
    {
        internal FFParser_Tgt_MC300_Ticket_Enablement()
            : base() { }
    }

    internal class FFParser_Tgt_MC300_Ticket_Enablement_H2G 
        : FFParser_Tgt_MC300_Ticket_Enablement
    {
        internal FFParser_Tgt_MC300_Ticket_Enablement_H2G()
            : base() { }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            FFTgt_H2G_TicketingEnablement_Request tgt = new FFTgt_H2G_TicketingEnablement_Request();
            tgt.Command = buffer[0].GetAppId<FF_GmuId_TicketEnablement_Request_Command, FF_AppId_TicketEnablement_Request_Command>();
            return tgt;
        }

        public override void GetTargetData(IFreeformEntity_MsgTgt tgt, ref List<byte> buffer)
        {
            FFTgt_H2G_TicketingEnablement_Request tgt2 = tgt as FFTgt_H2G_TicketingEnablement_Request;
            buffer.SetValue(tgt2.Command.GetGmuIdInt8());
        }
    }

    internal class FFParser_Tgt_MC300_Ticket_Enablement_G2H 
        : FFParser_Tgt_MC300_Ticket_Enablement
    {
        internal FFParser_Tgt_MC300_Ticket_Enablement_G2H()
            : base() { }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            FFTgt_G2H_TicketingEnablement tgt = new FFTgt_G2H_TicketingEnablement();
            tgt.Status = buffer[0].GetAppId<FF_GmuId_TicketEnablement_Response_Status, FF_AppId_TicketEnablement_Response_Status>();
            return tgt;
        }

        public override void GetTargetData(IFreeformEntity_MsgTgt tgt, ref List<byte> buffer)
        {
            FFTgt_G2H_TicketingEnablement tgt2 = tgt as FFTgt_G2H_TicketingEnablement;
            buffer.SetValue(tgt2.Status.GetGmuIdInt8());
        }
    }
}
