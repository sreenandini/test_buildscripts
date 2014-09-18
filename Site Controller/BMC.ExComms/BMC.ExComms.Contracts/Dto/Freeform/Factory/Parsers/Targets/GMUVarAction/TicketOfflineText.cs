using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    #region Maximum Offline Tickets Allowed

    internal class FFParser_Tgt_MC300_GVA_MOT_Allowed_Req_G2H
        : FFParser_Tgt_MC300_GVA_ActionData
    {
        internal FFParser_Tgt_MC300_GVA_MOT_Allowed_Req_G2H()
            : base() { }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            FFTgt_G2H_GVA_MOT_Allowed_Request tgt = new FFTgt_G2H_GVA_MOT_Allowed_Request();
            return tgt;
        }

        public override void GetTargetData(IFreeformEntity_MsgTgt tgt, ref List<byte> buffer)
        {
            FFTgt_G2H_GVA_MOT_Allowed_Request tgt2 = tgt as FFTgt_G2H_GVA_MOT_Allowed_Request;
        }
    }

    internal class FFParser_Tgt_MC300_GVA_MOT_Allowed_Resp_H2G
        : FFParser_Tgt_MC300_GVA_ActionData
    {
        internal FFParser_Tgt_MC300_GVA_MOT_Allowed_Resp_H2G()
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
            FFTgt_H2G_GVA_MOT_Allowed_Response tgt = new FFTgt_H2G_GVA_MOT_Allowed_Response();
            tgt.MaxOfflineTickets = buffer[0];
            return tgt;
        }

        public override void GetTargetData(IFreeformEntity_MsgTgt tgt, ref List<byte> buffer)
        {
            FFTgt_H2G_GVA_MOT_Allowed_Response tgt2 = tgt as FFTgt_H2G_GVA_MOT_Allowed_Response;
            buffer.Add(tgt2.MaxOfflineTickets);
        }
    }

    #endregion

    #region Offline Ticket Text Line 1

    internal class FFParser_Tgt_MC300_GVA_OFFTKT_TxtLine1_Req_G2H
        : FFParser_Tgt_MC300_GVA_ActionData
    {
        internal FFParser_Tgt_MC300_GVA_OFFTKT_TxtLine1_Req_G2H()
            : base() { }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            FFTgt_G2H_GVA_OFFTKT_TxtLine1_Request tgt = new FFTgt_G2H_GVA_OFFTKT_TxtLine1_Request();
            return tgt;
        }

        public override void GetTargetData(IFreeformEntity_MsgTgt tgt, ref List<byte> buffer)
        {
            FFTgt_G2H_GVA_OFFTKT_TxtLine1_Request tgt2 = tgt as FFTgt_G2H_GVA_OFFTKT_TxtLine1_Request;
        }
    }

    internal class FFParser_Tgt_MC300_GVA_OFFTKT_TxtLine1_Resp_H2G
        : FFParser_Tgt_MC300_GVA_ActionData
    {
        internal FFParser_Tgt_MC300_GVA_OFFTKT_TxtLine1_Resp_H2G()
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
            FFTgt_H2G_GVA_OFFTKT_TxtLine1_Response tgt = new FFTgt_H2G_GVA_OFFTKT_TxtLine1_Response();
            tgt.Line1Text = FreeformHelper.GetASCIIStringValueTrim(buffer, 0, 30);
            return tgt;
        }

        public override void GetTargetData(IFreeformEntity_MsgTgt tgt, ref List<byte> buffer)
        {
            FFTgt_H2G_GVA_OFFTKT_TxtLine1_Response tgt2 = tgt as FFTgt_H2G_GVA_OFFTKT_TxtLine1_Response;
            buffer.AddRange(tgt2.Line1Text.GetASCIIBytesValue(30));
        }
    }

    #endregion

    #region Offline Ticket Text Line 2

    internal class FFParser_Tgt_MC300_GVA_OFFTKT_TxtLine2_Req_G2H
        : FFParser_Tgt_MC300_GVA_ActionData
    {
        internal FFParser_Tgt_MC300_GVA_OFFTKT_TxtLine2_Req_G2H()
            : base() { }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            FFTgt_G2H_GVA_OFFTKT_TxtLine2_Request tgt = new FFTgt_G2H_GVA_OFFTKT_TxtLine2_Request();
            return tgt;
        }

        public override void GetTargetData(IFreeformEntity_MsgTgt tgt, ref List<byte> buffer)
        {
            FFTgt_G2H_GVA_OFFTKT_TxtLine2_Request tgt2 = tgt as FFTgt_G2H_GVA_OFFTKT_TxtLine2_Request;
        }
    }

    internal class FFParser_Tgt_MC300_GVA_OFFTKT_TxtLine2_Resp_H2G
        : FFParser_Tgt_MC300_GVA_ActionData
    {
        internal FFParser_Tgt_MC300_GVA_OFFTKT_TxtLine2_Resp_H2G()
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
            FFTgt_H2G_GVA_OFFTKT_TxtLine2_Response tgt = new FFTgt_H2G_GVA_OFFTKT_TxtLine2_Response();
            tgt.Line2Text = FreeformHelper.GetASCIIStringValueTrim(buffer, 0, 30);
            return tgt;
        }

        public override void GetTargetData(IFreeformEntity_MsgTgt tgt, ref List<byte> buffer)
        {
            FFTgt_H2G_GVA_OFFTKT_TxtLine2_Response tgt2 = tgt as FFTgt_H2G_GVA_OFFTKT_TxtLine2_Response;
            buffer.AddRange(tgt2.Line2Text.GetASCIIBytesValue(30));
        }
    }

    #endregion
}
