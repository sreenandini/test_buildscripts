using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    internal class FFParser_Tgt_Generic_GMUEvent 
        : FFTgtParser_NoSubTargets
    {
        internal FFParser_Tgt_Generic_GMUEvent()
            : base() { }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            FFTgt_G2H_GMUEvent tgt = new FFTgt_G2H_GMUEvent();
            entity = tgt;
            this.ParseBuffer(tgt, rootEntity, buffer, 0, buffer.Length);
            return tgt;
        }
    }

    internal class FFParser_Tgt_MC300_GMUEvent 
        : FFParser_Tgt_Generic_GMUEvent
    {
        internal FFParser_Tgt_MC300_GMUEvent()
            : base()
        {
            this.AddSubParsers();
        }

        protected override void AddSubParsersInternal()
        {
            this.AddBufferEntityParser((int)FF_GmuId_GMUEvent_DataSetIds.Standard, (int)FF_AppId_GMUEvent_DataSetIds.Standard, new FFParser_Tgt_MC300_GMUEvent_Standard());
            this.AddBufferEntityParser((int)FF_GmuId_GMUEvent_DataSetIds.Ticket, (int)FF_AppId_GMUEvent_DataSetIds.Ticket, new FFParser_Tgt_MC300_GMUEvent_Ticket());
            this.AddBufferEntityParser((int)FF_GmuId_GMUEvent_DataSetIds.EFT, (int)FF_AppId_GMUEvent_DataSetIds.EFT, new FFParser_Tgt_MC300_GMUEvent_EFT());
            this.AddBufferEntityParser((int)FF_GmuId_GMUEvent_DataSetIds.Coupon, (int)FF_AppId_GMUEvent_DataSetIds.Coupon, new FFParser_Tgt_MC300_GMUEvent_Coupon());
            this.AddBufferEntityParser((int)FF_GmuId_GMUEvent_DataSetIds.Printer, (int)FF_AppId_GMUEvent_DataSetIds.Printer, new FFParser_Tgt_MC300_GMUEvent_Printer());
            this.AddBufferEntityParser((int)FF_GmuId_GMUEvent_DataSetIds.NoteAcceptor, (int)FF_AppId_GMUEvent_DataSetIds.NoteAcceptor, new FFParser_Tgt_MC300_GMUEvent_NoteAcceptor());
            this.AddBufferEntityParser((int)FF_GmuId_GMUEvent_DataSetIds.TaggedStatus, (int)FF_AppId_GMUEvent_DataSetIds.TaggedStatus, new FFParser_Tgt_MC300_GMUEvent_TaggedStatus());
        }
    }

    internal class FFParser_Tgt_MC300_GMUEvent_G2H
        : FFParser_Tgt_MC300_GMUEvent
    {
        internal FFParser_Tgt_MC300_GMUEvent_G2H()
            : base()
        {
        }
    }
}
