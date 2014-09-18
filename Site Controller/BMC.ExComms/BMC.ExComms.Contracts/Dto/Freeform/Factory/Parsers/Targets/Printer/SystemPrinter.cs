using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    internal class FFParser_Tgt_Generic_SystemPrinter
        : FFTgtParser
    {
        internal FFParser_Tgt_Generic_SystemPrinter()
            : base() { }
    }

    internal class FFParser_Tgt_MC300_SystemPrinter
        : FFParser_Tgt_Generic_SystemPrinter
    {
        internal FFParser_Tgt_MC300_SystemPrinter()
            : base() { }
    }

    internal class FFParser_Tgt_MC300_SystemPrinter_G2H
        : FFParser_Tgt_MC300_SystemPrinter
    {
        internal FFParser_Tgt_MC300_SystemPrinter_G2H()
            : base()
        {
            this.AddSubParsers();
        }

        protected override void AddSubParsersInternal()
        {
            this.AddBufferEntityParser((int)FF_GmuId_SystemPrinter.PrinterStatus, (int)FF_AppId_SystemPrinter.PrinterStatus, new FFParser_Tgt_MC300_SystemPrinter_PrintStatus_G2H());
        }
    }

    internal class FFParser_Tgt_MC300_SystemPrinter_H2G
        : FFParser_Tgt_MC300_SystemPrinter
    {

        internal FFParser_Tgt_MC300_SystemPrinter_H2G()
            : base() { this.AddSubParsers(); }

        protected override void AddSubParsersInternal()
        {
            this.AddBufferEntityParser((int)FF_GmuId_SystemPrinter.PrintString, (int)FF_AppId_SystemPrinter.PrintString, new FFParser_Tgt_MC300_SystemPrinter_PrintString_H2G());
            this.AddBufferEntityParser((int)FF_GmuId_SystemPrinter.PrintStringEnd, (int)FF_AppId_SystemPrinter.PrintStringEnd, new FFParser_Tgt_MC300_SystemPrinter_PrintStringEnd_H2G());
            this.AddBufferEntityParser((int)FF_GmuId_SystemPrinter.SetPrintCompValue, (int)FF_AppId_SystemPrinter.SetPrintCompValue, new FFParser_Tgt_MC300_SystemPrinter_SetPrintCompValue_H2G());
            this.AddBufferEntityParser((int)FF_GmuId_SystemPrinter.ComVoucherRequest, (int)FF_AppId_SystemPrinter.ComVoucherRequest, new FFParser_Tgt_MC300_SystemPrinter_CompVoucherRequest_H2G());
            this.AddBufferEntityParser((int)FF_GmuId_SystemPrinter.PrintJobCancel, (int)FF_AppId_SystemPrinter.PrintJobCancel, new FFParser_Tgt_MC300_SystemPrinter_PrintJobCancel_H2G());
        }
    }

    internal class FFParser_Tgt_MC300_SystemPrinter_PrintStatus_G2H
        : FFParser_Tgt_MC300_SystemPrinter
    {
        internal FFParser_Tgt_MC300_SystemPrinter_PrintStatus_G2H()
            : base() { }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            FFTgt_G2H_SystemPrinter_PrintStatus tgt = new FFTgt_G2H_SystemPrinter_PrintStatus();
            tgt.Status = (FF_AppId_SystemPrinter_PrinterStatus)buffer[0];
            return tgt;
        }

        public override void GetTargetData(IFreeformEntity_MsgTgt tgt, ref List<byte> buffer)
        {
            FFTgt_G2H_SystemPrinter_PrintStatus tgt2 = tgt as  FFTgt_G2H_SystemPrinter_PrintStatus;
            buffer.Add(tgt2.Status.GetGmuIdInt8());
        }
    }

    internal class FFParser_Tgt_MC300_SystemPrinter_PrintString_H2G
        : FFParser_Tgt_MC300_SystemPrinter
    {
        internal FFParser_Tgt_MC300_SystemPrinter_PrintString_H2G()
            : base() { }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            FFTgt_H2G_SystemPrinter_PrintString tgt = new FFTgt_H2G_SystemPrinter_PrintString();
            tgt.TextToPrint = buffer.GetASCIIStringValue();
            return tgt;
        }

        public override void GetTargetData(IFreeformEntity_MsgTgt tgt, ref List<byte> buffer)
        {
            FFTgt_H2G_SystemPrinter_PrintString tgt2 = tgt as  FFTgt_H2G_SystemPrinter_PrintString;
            buffer.AddRange(tgt2.TextToPrint.GetASCIIBytesValue());
        }
    }

    internal class FFParser_Tgt_MC300_SystemPrinter_PrintStringEnd_H2G
        : FFParser_Tgt_MC300_SystemPrinter
    {
        internal FFParser_Tgt_MC300_SystemPrinter_PrintStringEnd_H2G()
            : base() { }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            FFTgt_H2G_SystemPrinter_PrintStringEnd tgt = new FFTgt_H2G_SystemPrinter_PrintStringEnd();
            tgt.Response = (FF_AppId_SystemPrinter_PrinterResponse)buffer[0];
            return tgt;
        }

        public override void GetTargetData(IFreeformEntity_MsgTgt tgt, ref List<byte> buffer)
        {
            FFTgt_H2G_SystemPrinter_PrintStringEnd tgt2 = tgt as FFTgt_H2G_SystemPrinter_PrintStringEnd;
            buffer.Add(tgt2.Response.GetGmuIdInt8());
        }
    }

    internal class FFParser_Tgt_MC300_SystemPrinter_SetPrintCompValue_H2G
        : FFParser_Tgt_MC300_SystemPrinter
    {
        internal FFParser_Tgt_MC300_SystemPrinter_SetPrintCompValue_H2G()
            : base() { }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            FFTgt_H2G_SystemPrinter_SetPrintCompValue tgt = new FFTgt_H2G_SystemPrinter_SetPrintCompValue();
            tgt.Values = buffer;
            return tgt;
        }

        public override void GetTargetData(IFreeformEntity_MsgTgt tgt, ref List<byte> buffer)
        {
            FFTgt_H2G_SystemPrinter_SetPrintCompValue tgt2 = tgt as FFTgt_H2G_SystemPrinter_SetPrintCompValue;
            buffer.AddRange(tgt2.Values);
        }
    }

    internal class FFParser_Tgt_MC300_SystemPrinter_CompVoucherRequest_H2G
        : FFParser_Tgt_MC300_SystemPrinter
    {
        internal FFParser_Tgt_MC300_SystemPrinter_CompVoucherRequest_H2G()
            : base() { }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            FFTgt_H2G_SystemPrinter_CompVoucherRequest tgt = new FFTgt_H2G_SystemPrinter_CompVoucherRequest();
            tgt.PlayerId = buffer.GetBCDValueString(0, 0, 4);
            tgt.PinNumber = buffer.GetBCDValueString(0, 4, 3);
            tgt.VoucherAmount = buffer.GetBCDDoubleHexValue(7, buffer.Length);
            return tgt;
        }

        public override void GetTargetData(IFreeformEntity_MsgTgt tgt, ref List<byte> buffer)
        {
            FFTgt_H2G_SystemPrinter_CompVoucherRequest tgt2 = tgt as FFTgt_H2G_SystemPrinter_CompVoucherRequest;
            buffer.AddRange(tgt2.PlayerId.GetBCDToBytes(4));
            buffer.AddRange(tgt2.PinNumber.GetBCDToBytes(4));
            buffer.AddRange(tgt2.VoucherAmount.GetBCDToBytes(4));
        }
    }

    internal class FFParser_Tgt_MC300_SystemPrinter_PrintJobCancel_H2G
        : FFParser_Tgt_MC300_SystemPrinter
    {
        internal FFParser_Tgt_MC300_SystemPrinter_PrintJobCancel_H2G()
            : base() { }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            FFTgt_H2G_SystemPrinter_PrintJobCancel tgt = new FFTgt_H2G_SystemPrinter_PrintJobCancel();
            return tgt;
        }
    }
}