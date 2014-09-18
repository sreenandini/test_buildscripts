using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    internal class FFParser_Tgt_MC300_GVA_EFT_TTimeOut_Req_G2H
        : FFParser_Tgt_MC300_GVA_Ticket
    {
        internal FFParser_Tgt_MC300_GVA_EFT_TTimeOut_Req_G2H()
            : base() { }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            FFTgt_G2H_GVA_EFT_TTimeOut_Request tgt = new FFTgt_G2H_GVA_EFT_TTimeOut_Request();
            return tgt;
        }
    }

    internal class FFParser_Tgt_MC300_GVA_EFT_TTimeOut_Resp_H2G
        : FFParser_Tgt_MC300_GVA_Ticket
    {
        internal FFParser_Tgt_MC300_GVA_EFT_TTimeOut_Resp_H2G()
            : base() { }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            FFTgt_H2G_GVA_EFT_TTimeOut_Response tgt = new FFTgt_H2G_GVA_EFT_TTimeOut_Response();
            tgt.TimeOut = FreeformHelper.GetBytesToNumberInt32(buffer, 0, 1);
            return tgt;
        }

        public override void GetTargetData(IFreeformEntity_MsgTgt tgt, ref List<byte> buffer)
        {
            FFTgt_H2G_GVA_EFT_TTimeOut_Response tgt2 = tgt as FFTgt_H2G_GVA_EFT_TTimeOut_Response;
            buffer.AddRange(tgt2.TimeOut.GetNumberToBytes(1));
        }
    }

    internal class FFParser_Tgt_MC300_GVA_EFT_TTimeOut_Status_G2H
        : FFParser_Tgt_MC300_GVA_Ticket
    {
        internal FFParser_Tgt_MC300_GVA_EFT_TTimeOut_Status_G2H()
            : base() { }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            FFTgt_G2H_GVA_EFT_TTimeOut_Status tgt = new FFTgt_G2H_GVA_EFT_TTimeOut_Status();
            tgt.Status = buffer[0].GetAppId<FF_GmuId_ResponseStatus_Types, FF_AppId_ResponseStatus_Types>();
            return tgt;
        }
    }
}
