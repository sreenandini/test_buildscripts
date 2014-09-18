using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    #region Ticket Redemption Request and Ticket Redemption Response
    internal class FFParser_Tgt_Generic_Ticket_Redemption 
        : FFTgtParser
    {
        internal FFParser_Tgt_Generic_Ticket_Redemption()
            : base() { }
    }

    internal class FFParser_Tgt_MC300_Ticket_Redemption 
        : FFParser_Tgt_Generic_Ticket_Redemption
    {
        internal FFParser_Tgt_MC300_Ticket_Redemption()
            : base() { }
    }

    internal class FFParser_Tgt_MC300_Ticket_Redemption_Request_G2H 
        : FFParser_Tgt_MC300_Ticket_Redemption
    {
        internal FFParser_Tgt_MC300_Ticket_Redemption_Request_G2H()
            : base() { }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            FFTgt_G2H_Ticket_Redemption_Request tgt = new FFTgt_G2H_Ticket_Redemption_Request();
            tgt.Barcode = buffer.GetBCDValueString(0, 0, 9);
            return tgt;
        }

        public override void GetTargetData(IFreeformEntity_MsgTgt tgt, ref List<byte> buffer)
        {
            FFTgt_G2H_Ticket_Redemption_Request tgt2 = tgt as FFTgt_G2H_Ticket_Redemption_Request;
            buffer.SetBCDValue(tgt2.Barcode, 9);
        }
    }

    internal class FFParser_Tgt_MC300_Ticket_Redemption_Response_H2G 
        : FFParser_Tgt_MC300_Ticket_Redemption
    {
        internal FFParser_Tgt_MC300_Ticket_Redemption_Response_H2G()
            : base() { }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            FFTgt_H2G_Ticket_Redemption_Response tgt = new FFTgt_H2G_Ticket_Redemption_Response();
            tgt.Barcode = buffer.GetBCDValueString(0, 0, 9);
            tgt.Amount = buffer.GetBytesToBCDDouble(9, 4);
            tgt.Type = buffer[13].GetAppId<FF_GmuId_TicketTypes, FF_AppId_TicketTypes>();
            return tgt;
        }

        public override void GetTargetData(IFreeformEntity_MsgTgt tgt, ref List<byte> buffer)
        {
            FFTgt_H2G_Ticket_Redemption_Response tgt2 = tgt as FFTgt_H2G_Ticket_Redemption_Response;
            buffer.SetBCDValue(tgt2.Barcode, 9);
            buffer.SetBCDValue(tgt2.Amount, 4);
            buffer.SetValue(tgt2.Type.GetGmuIdInt8());
        }
    }
    
    #endregion

    #region Ticket Redemption Close

    internal class FFParser_Tgt_Generic_Ticket_Redemption_Close 
        : FFTgtParser
    {
        internal FFParser_Tgt_Generic_Ticket_Redemption_Close()
            : base() { }
    }

    internal class FFParser_Tgt_MC300_Ticket_Redemption_Close 
        : FFParser_Tgt_Generic_Ticket_Redemption_Close
    {
        internal FFParser_Tgt_MC300_Ticket_Redemption_Close()
            : base() { }
    }

    internal class FFParser_Tgt_MC300_Ticket_Redemption_Close_G2H 
        : FFParser_Tgt_MC300_Ticket_Redemption_Close
    {
        internal FFParser_Tgt_MC300_Ticket_Redemption_Close_G2H()
            : base() { }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            FFTgt_G2H_Ticket_Redemption_Close tgt = new FFTgt_G2H_Ticket_Redemption_Close();
            tgt.Status = buffer[0].GetAppId<FF_GmuId_TicketRedemption_Close_Status,FF_AppId_TicketRedemption_Close_Status>();
            tgt.Barcode = buffer.GetBCDValueString(0, 1, 9);
            tgt.Amount = buffer.GetBytesToBCDDouble(10, 4);
            tgt.Type = buffer[14].GetAppId<FF_GmuId_TicketTypes, FF_AppId_TicketTypes>();
            return tgt;
        }

        public override void GetTargetData(IFreeformEntity_MsgTgt tgt, ref List<byte> buffer)
        {
            FFTgt_G2H_Ticket_Redemption_Close tgt2 = tgt as FFTgt_G2H_Ticket_Redemption_Close;
            buffer.SetValue(tgt2.Status.GetGmuIdInt8());
            buffer.SetBCDValue(tgt2.Barcode, 9);
            buffer.SetBCDValue(tgt2.Amount, 4);
            buffer.SetValue(tgt2.Type.GetGmuIdInt8());
        }
    }

    internal class FFParser_Tgt_MC300_Ticket_Redemption_Close_H2G 
        : FFParser_Tgt_MC300_Ticket_Redemption_Close
    {
        internal FFParser_Tgt_MC300_Ticket_Redemption_Close_H2G()
            : base() { }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            FFTgt_H2G_Ticket_Redemption_Close tgt = new FFTgt_H2G_Ticket_Redemption_Close();
            tgt.Status = buffer[0].GetAppId<FF_GmuId_ResponseStatus_Types, FF_AppId_ResponseStatus_Types>();
            return tgt;
        }

        public override void GetTargetData(IFreeformEntity_MsgTgt tgt, ref List<byte> buffer)
        {
            FFTgt_H2G_Ticket_Redemption_Close tgt2 = tgt as FFTgt_H2G_Ticket_Redemption_Close;
            buffer.SetValue(tgt2.Status.GetGmuIdInt8());
        }
    } 
    #endregion
}
