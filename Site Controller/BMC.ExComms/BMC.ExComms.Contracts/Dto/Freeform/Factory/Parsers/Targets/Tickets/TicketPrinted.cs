using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    #region Ticket Printed and Ticket Printed Response
    internal class FFParser_Tgt_Generic_Ticket_Printed
        : FFTgtParser
    {
        internal FFParser_Tgt_Generic_Ticket_Printed()
            : base() { }
    }

    internal class FFParser_Tgt_MC300_Ticket_Printed
        : FFParser_Tgt_Generic_Ticket_Printed
    {
        internal FFParser_Tgt_MC300_Ticket_Printed()
            : base() { }
    }

    internal class FFParser_Tgt_MC300_Ticket_Printed_G2H
        : FFParser_Tgt_MC300_Ticket_Printed
    {
        internal FFParser_Tgt_MC300_Ticket_Printed_G2H()
            : base() { }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            FFTgt_G2H_Ticket_Printed_Request tgt = new FFTgt_G2H_Ticket_Printed_Request();
            tgt.BarCode = buffer.GetBCDValueString(0, 0, 9);
            tgt.Amount = buffer.GetBytesToBCDDouble(9, 4);
            tgt.Type = buffer[13].GetAppId<FF_GmuId_TicketTypes, FF_AppId_TicketTypes>();
            return tgt;
        }

        public override void GetTargetData(IFreeformEntity_MsgTgt tgt, ref List<byte> buffer)
        {
            FFTgt_G2H_Ticket_Printed_Request tgt2 = tgt as FFTgt_G2H_Ticket_Printed_Request;
            buffer.SetBCDValue(tgt2.BarCode, 9);
            buffer.SetBCDValue(tgt2.Amount, 4);
            buffer.SetValue(tgt2.Type.GetGmuIdInt8());
        }
    }

    internal class FFParser_Tgt_MC300_Ticket_Printed_H2G
        : FFParser_Tgt_MC300_Ticket_Printed
    {
        internal FFParser_Tgt_MC300_Ticket_Printed_H2G()
            : base() { }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            FFTgt_H2G_Ticket_Printed_Response tgt = new FFTgt_H2G_Ticket_Printed_Response();
            tgt.Status = buffer[0].GetAppId<FF_GmuId_ResponseStatus_Types, FF_AppId_ResponseStatus_Types>();
            return tgt;
        }

        public override void GetTargetData(IFreeformEntity_MsgTgt tgt, ref List<byte> buffer)
        {
            FFTgt_H2G_Ticket_Printed_Response tgt2 = tgt as FFTgt_H2G_Ticket_Printed_Response;
            buffer.SetValue(tgt2.Status.GetGmuIdInt8());
        }
    }
    #endregion

    #region Ticket Print Result Status
    internal class FFParser_Tgt_Generic_Ticket_PrintResultStatus
        : FFTgtParser
    {
        internal FFParser_Tgt_Generic_Ticket_PrintResultStatus()
            : base() { }
    }

    internal class FFParser_Tgt_MC300_Ticket_PrintResultStatus
        : FFParser_Tgt_Generic_Ticket_PrintResultStatus
    {
        internal FFParser_Tgt_MC300_Ticket_PrintResultStatus()
            : base() { }
    }

    internal class FFParser_Tgt_MC300_Ticket_PrintResultStatus_G2H
        : FFParser_Tgt_MC300_Ticket_PrintResultStatus
    {
        internal FFParser_Tgt_MC300_Ticket_PrintResultStatus_G2H()
            : base() { }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            FFTgt_G2H_TicketPrint_ResultStatus tgt = new FFTgt_G2H_TicketPrint_ResultStatus();
            tgt.GameTicketSequence = buffer.GetBytesToBCDInt16(0, 2);
            tgt.PrintStatus = buffer[2].GetAppId<FF_GmuId_TicketPrintStatus, FF_AppId_TicketPrintStatus>();
            return tgt;
        }

        public override void GetTargetData(IFreeformEntity_MsgTgt tgt, ref List<byte> buffer)
        {
            FFTgt_G2H_TicketPrint_ResultStatus tgt2 = tgt as FFTgt_G2H_TicketPrint_ResultStatus;
            buffer.SetValue(tgt2.GameTicketSequence, 2);
            buffer.SetValue(tgt2.PrintStatus.GetGmuIdInt8());
        }
    }
    #endregion
}
