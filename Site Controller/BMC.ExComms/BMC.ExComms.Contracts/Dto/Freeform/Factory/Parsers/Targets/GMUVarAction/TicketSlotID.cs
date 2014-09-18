using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    internal class FFParser_Tgt_MC300_GVA_TSSlotID_Req_G2H
        : FFParser_Tgt_MC300_GVA_ActionData
    {
        internal FFParser_Tgt_MC300_GVA_TSSlotID_Req_G2H()
            : base() { }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            if (buffer.Length == 1)
            {
                FFTgt_G2H_GVA_TSSlotID_Status tgt = new FFTgt_G2H_GVA_TSSlotID_Status();
                tgt.Status = buffer[0].GetAppId<FF_GmuId_ResponseStatus_Types, FF_AppId_ResponseStatus_Types>();
                return tgt;
            }
            else
            {
                FFTgt_G2H_GVA_TSSlotID_Request tgt = new FFTgt_G2H_GVA_TSSlotID_Request();
                return tgt;
            }
        }

        public override void GetTargetData(IFreeformEntity_MsgTgt tgt, ref List<byte> buffer)
        {
            if (tgt is FFTgt_G2H_GVA_TSSlotID_Status)
            {
                FFTgt_G2H_GVA_TSSlotID_Status tgt2 = tgt as FFTgt_G2H_GVA_TSSlotID_Status;
                buffer.Add(tgt2.Status.GetGmuIdInt8());
            }
            else
            {
                FFTgt_G2H_GVA_TSSlotID_Request tgt2 = tgt as FFTgt_G2H_GVA_TSSlotID_Request;
            }
        }
    }

    internal class FFParser_Tgt_MC300_GVA_TSSlotID_Resp_H2G
        : FFParser_Tgt_MC300_GVA_ActionData
    {
        internal FFParser_Tgt_MC300_GVA_TSSlotID_Resp_H2G()
            : base() { }

        public override FF_FlowInitiation FlowInitiation
        {
            get
            {
                return FF_FlowInitiation.Host;
            }
        }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            FFTgt_H2G_GVA_TSSlotID_Response tgt = new FFTgt_H2G_GVA_TSSlotID_Response();
            tgt.SlotID = FreeformHelper.GetBytesToBCDInt32(buffer, 0, 3);
            return tgt;
        }

        public override void GetTargetData(IFreeformEntity_MsgTgt tgt, ref List<byte> buffer)
        {
            FFTgt_H2G_GVA_TSSlotID_Response tgt2 = tgt as FFTgt_H2G_GVA_TSSlotID_Response;
            buffer.AddRange(tgt2.SlotID.GetBCDToBytes(3));
        }
    }
}
