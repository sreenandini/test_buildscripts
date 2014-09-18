using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    internal class FFParser_Tgt_MC300_GVA_EnablePrint_RT_Req_G2H
        : FFParser_Tgt_MC300_GVA_Ticket
    {
        internal FFParser_Tgt_MC300_GVA_EnablePrint_RT_Req_G2H()
            : base() { }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            FFTgt_G2H_GVA_EnablePrint_RT_Request tgt = new FFTgt_G2H_GVA_EnablePrint_RT_Request();
            return tgt;
        }
    }

    internal class FFParser_Tgt_MC300_GVA_EnablePrint_RT_Resp_H2G
        : FFParser_Tgt_MC300_GVA_Ticket
    {
        internal FFParser_Tgt_MC300_GVA_EnablePrint_RT_Resp_H2G()
            : base() { }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            FFTgt_H2G_GVA_EnablePrint_RT_Response tgt = new FFTgt_H2G_GVA_EnablePrint_RT_Response();
            tgt.EnableRestrictedTickets = buffer[0].GetAppId<FF_GmuId_PrintRestrictedTicket, FF_AppId_PrintRestrictedTicket>();
            return tgt;
        }

        public override void GetTargetData(IFreeformEntity_MsgTgt tgt, ref List<byte> buffer)
        {
            FFTgt_H2G_GVA_EnablePrint_RT_Response tgt2 = tgt as FFTgt_H2G_GVA_EnablePrint_RT_Response;
            buffer.Add(tgt2.EnableRestrictedTickets.GetGmuIdInt8());
        }
    }

    internal class FFParser_Tgt_MC300_GVA_EnablePrint_RT_Status_G2H
        : FFParser_Tgt_MC300_GVA_Ticket
    {
        internal FFParser_Tgt_MC300_GVA_EnablePrint_RT_Status_G2H()
            : base() { }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            FFTgt_G2H_GVA_EnablePrint_RT_Status tgt = new FFTgt_G2H_GVA_EnablePrint_RT_Status();
            tgt.Status = buffer[0].GetAppId<FF_GmuId_ResponseStatus_Types, FF_AppId_ResponseStatus_Types>();
            return tgt;
        }
    }
}
