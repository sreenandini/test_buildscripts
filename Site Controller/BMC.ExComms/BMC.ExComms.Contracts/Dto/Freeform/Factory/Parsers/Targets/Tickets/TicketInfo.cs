using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    internal class FFParser_Tgt_Generic_TicketInfo 
        : FFTgtParser_NoSubTargets
    {
        internal FFParser_Tgt_Generic_TicketInfo()
            : base() { }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            FFTgt_B2B_TicketInfo tgt = new FFTgt_B2B_TicketInfo();
            entity = tgt;
            this.ParseBuffer(tgt, rootEntity, buffer, 0, buffer.Length);
            return tgt;
        }
    }

    internal class FFParser_Tgt_MC300_TicketInfo 
        : FFParser_Tgt_Generic_TicketInfo
    {
        internal FFParser_Tgt_MC300_TicketInfo()
            : base() { }
    }

    internal class FFParser_Tgt_MC300_TicketInfo_G2H 
        : FFParser_Tgt_MC300_TicketInfo
    {

        internal FFParser_Tgt_MC300_TicketInfo_G2H()
            : base()
        {
            this.AddSubParsers();
        }

        protected override void AddSubParsersInternal()
        {
            this.AddBufferEntityParser((int)FF_GmuId_TicketMessageTypes.TicketPrinted, (int)FF_AppId_TicketMessageTypes.TicketPrinted, new FFParser_Tgt_MC300_Ticket_Printed_G2H());
            this.AddBufferEntityParser((int)FF_GmuId_TicketMessageTypes.TicketVoid, (int)FF_AppId_TicketMessageTypes.TicketVoid, new FFParser_Tgt_MC300_Ticket_Void_G2H());
            this.AddBufferEntityParser((int)FF_GmuId_TicketMessageTypes.TicketRedemption, (int)FF_AppId_TicketMessageTypes.TicketRedemption, new FFParser_Tgt_MC300_Ticket_Redemption_Request_G2H());
            this.AddBufferEntityParser((int)FF_GmuId_TicketMessageTypes.TicketRedemptionComplete, (int)FF_AppId_TicketMessageTypes.TicketRedemptionComplete, new FFParser_Tgt_MC300_Ticket_Redemption_Close_G2H());
            this.AddBufferEntityParser((int)FF_GmuId_TicketMessageTypes.EnablementResponse, (int)FF_AppId_TicketMessageTypes.EnablementResponse, new FFParser_Tgt_MC300_Ticket_Enablement_G2H());
            this.AddBufferEntityParser((int)FF_GmuId_TicketMessageTypes.TicketPrintStatusResult, (int)FF_AppId_TicketMessageTypes.TicketPrintStatusResult, new FFParser_Tgt_MC300_Ticket_PrintResultStatus_G2H());
            this.AddBufferEntityParser((int)FF_GmuId_TicketMessageTypes.OfflineTicketInfo, (int)FF_AppId_TicketMessageTypes.OfflineTicketInfo, new FFParser_Tgt_MC300_Ticket_Attribute_G2H());
            this.AddBufferEntityParser((int)FF_GmuId_TicketMessageTypes.TicketSequenceNumberUpdate, (int)FF_AppId_TicketMessageTypes.TicketSequenceNumberUpdate, new FFParser_Tgt_MC300_Ticket_SequenceNumber_G2H());
        }
    }

    internal class FFParser_Tgt_MC300_TicketInfo_H2G 
        : FFParser_Tgt_MC300_TicketInfo
    {
        internal FFParser_Tgt_MC300_TicketInfo_H2G()
            : base()
        {
            this.AddSubParsers();
        }

        protected override void AddSubParsersInternal()
        {
            this.AddBufferEntityParser((int)FF_GmuId_TicketMessageTypes.TicketPrinted, (int)FF_AppId_TicketMessageTypes.TicketPrinted, new FFParser_Tgt_MC300_Ticket_Printed_H2G());
            this.AddBufferEntityParser((int)FF_GmuId_TicketMessageTypes.TicketRedemption, (int)FF_AppId_TicketMessageTypes.TicketRedemption, new FFParser_Tgt_MC300_Ticket_Redemption_Response_H2G());
            this.AddBufferEntityParser((int)FF_GmuId_TicketMessageTypes.TicketRedemptionComplete, (int)FF_AppId_TicketMessageTypes.TicketRedemptionComplete, new FFParser_Tgt_MC300_Ticket_Redemption_Close_H2G());
            this.AddBufferEntityParser((int)FF_GmuId_TicketMessageTypes.EnablementRequest, (int)FF_AppId_TicketMessageTypes.EnablementRequest, new FFParser_Tgt_MC300_Ticket_Enablement_H2G());
        }
    }
}
