using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    #region Ticket Number

    internal class FFParser_Tgt_MC300_GVA_TN_Req_G2H
        : FFParser_Tgt_MC300_GVA_ActionData
    {
        internal FFParser_Tgt_MC300_GVA_TN_Req_G2H()
            : base() { }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            if (buffer.Length == 1)
            {
                FFTgt_G2H_GVA_TN_Status tgt = new FFTgt_G2H_GVA_TN_Status();
                tgt.Status = buffer[0].GetAppId<FF_GmuId_ResponseStatus_Types, FF_AppId_ResponseStatus_Types>();
                return tgt;
            }
            else
            {
                FFTgt_G2H_GVA_TN_Request tgt = new FFTgt_G2H_GVA_TN_Request();
                return tgt;
            }
        }

        public override void GetTargetData(IFreeformEntity_MsgTgt tgt, ref List<byte> buffer)
        {
            if (tgt is FFTgt_G2H_GVA_TN_Status)
            {
                FFTgt_G2H_GVA_TN_Status tgt2 = tgt as FFTgt_G2H_GVA_TN_Status;
                buffer.Add(tgt2.Status.GetGmuIdInt8());
            }
            else
            {
                FFTgt_G2H_GVA_TN_Request tgt2 = tgt as FFTgt_G2H_GVA_TN_Request;
            }
        }
    }

    internal class FFParser_Tgt_MC300_GVA_TN_Resp_H2G
        : FFParser_Tgt_MC300_GVA_ActionData
    {
        internal FFParser_Tgt_MC300_GVA_TN_Resp_H2G()
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
            FFTgt_H2G_GVA_TN_Response tgt = new FFTgt_H2G_GVA_TN_Response();
            tgt.TicketNumber = FreeformHelper.GetBytesToBCDInt32(buffer, 0, 3);
            return tgt;
        }

        public override void GetTargetData(IFreeformEntity_MsgTgt tgt, ref List<byte> buffer)
        {
            FFTgt_H2G_GVA_TN_Response tgt2 = tgt as FFTgt_H2G_GVA_TN_Response;
            buffer.AddRange(tgt2.TicketNumber.GetBCDToBytes(3));
        }
    }

    #endregion //Ticket Number

    #region Ticket Print Date

    internal class FFParser_Tgt_MC300_GVA_TPD_Req_G2H
        : FFParser_Tgt_MC300_GVA_ActionData
    {
        internal FFParser_Tgt_MC300_GVA_TPD_Req_G2H()
            : base() { }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            if (buffer.Length == 1)
            {
                FFTgt_G2H_GVA_TPD_Status tgt = new FFTgt_G2H_GVA_TPD_Status();
                tgt.Status = buffer[0].GetAppId<FF_GmuId_ResponseStatus_Types, FF_AppId_ResponseStatus_Types>();
                return tgt;
            }
            else
            {
                FFTgt_G2H_GVA_TPD_Request tgt = new FFTgt_G2H_GVA_TPD_Request();
                return tgt;
            }
        }

        public override void GetTargetData(IFreeformEntity_MsgTgt tgt, ref List<byte> buffer)
        {
            if (tgt is FFTgt_G2H_GVA_TPD_Status)
            {
                FFTgt_G2H_GVA_TPD_Status tgt2 = tgt as FFTgt_G2H_GVA_TPD_Status;
                buffer.Add(tgt2.Status.GetGmuIdInt8());
            }
            else
            {
                FFTgt_G2H_GVA_TPD_Request tgt2 = tgt as FFTgt_G2H_GVA_TPD_Request;
            }
        }
    }

    internal class FFParser_Tgt_MC300_GVA_TPD_Resp_H2G
        : FFParser_Tgt_MC300_GVA_ActionData
    {
        internal FFParser_Tgt_MC300_GVA_TPD_Resp_H2G()
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
            FFTgt_H2G_GVA_TPD_Response tgt = new FFTgt_H2G_GVA_TPD_Response();
            tgt.Date = buffer.GetBytesToBCDDateTimeUSTimeZone(0, 3);
            return tgt;
        }

        public override void GetTargetData(IFreeformEntity_MsgTgt tgt, ref List<byte> buffer)
        {
            FFTgt_H2G_GVA_TPD_Response tgt2 = tgt as FFTgt_H2G_GVA_TPD_Response;
            buffer.AddRange(tgt2.Date.GetBCDToBytesUSTimeZone(3));
        }
    }

    #endregion

    #region Ticket Expiration Date

    internal class FFParser_Tgt_MC300_GVA_TED_Req_G2H
        : FFParser_Tgt_MC300_GVA_ActionData
    {
        internal FFParser_Tgt_MC300_GVA_TED_Req_G2H()
            : base() { }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            if (buffer.Length == 1)
            {
                FFTgt_G2H_GVA_TED_Status tgt = new FFTgt_G2H_GVA_TED_Status();
                tgt.Status = buffer[0].GetAppId<FF_GmuId_ResponseStatus_Types, FF_AppId_ResponseStatus_Types>();
                return tgt;
            }
            else
            {
                FFTgt_G2H_GVA_TED_Request tgt = new FFTgt_G2H_GVA_TED_Request();
                return tgt;
            }
        }

        public override void GetTargetData(IFreeformEntity_MsgTgt tgt, ref List<byte> buffer)
        {
            if (tgt is FFTgt_G2H_GVA_TED_Status)
            {
                FFTgt_G2H_GVA_TED_Status tgt2 = tgt as FFTgt_G2H_GVA_TED_Status;
                buffer.Add(tgt2.Status.GetGmuIdInt8());
            }
            else
            {
                FFTgt_G2H_GVA_TED_Request tgt2 = tgt as FFTgt_G2H_GVA_TED_Request;
            }
        }
    }

    internal class FFParser_Tgt_MC300_GVA_TED_Resp_H2G
        : FFParser_Tgt_MC300_GVA_ActionData
    {
        internal FFParser_Tgt_MC300_GVA_TED_Resp_H2G()
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
            FFTgt_H2G_GVA_TED_Response tgt = new FFTgt_H2G_GVA_TED_Response();
            tgt.Date = buffer.GetBytesToBCDDateTimeUSTimeZone(0, 3);
            tgt.ExipreDays = FreeformHelper.GetBytesToBCDInt16(buffer, 3, 2);
            return tgt;
        }

        public override void GetTargetData(IFreeformEntity_MsgTgt tgt, ref List<byte> buffer)
        {
            FFTgt_H2G_GVA_TED_Response tgt2 = tgt as FFTgt_H2G_GVA_TED_Response;
            buffer.AddRange(tgt2.Date.GetBCDToBytesUSTimeZone(3));
            buffer.AddRange(tgt2.ExipreDays.GetBCDToBytes(2));
        }
    }

    #endregion

    #region Restricted Ticket Expiration Days

    internal class FFParser_Tgt_MC300_GVA_RTE_Days_Req_G2H
        : FFParser_Tgt_MC300_GVA_ActionData
    {
        internal FFParser_Tgt_MC300_GVA_RTE_Days_Req_G2H()
            : base() { }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            if (buffer.Length == 1)
            {
                FFTgt_G2H_GVA_RTE_Days_Status tgt = new FFTgt_G2H_GVA_RTE_Days_Status();
                tgt.Status = buffer[0].GetAppId<FF_GmuId_ResponseStatus_Types, FF_AppId_ResponseStatus_Types>();
                return tgt;
            }
            else
            {
                FFTgt_G2H_GVA_RTE_Days_Request tgt = new FFTgt_G2H_GVA_RTE_Days_Request();
                return tgt;
            }
        }

        public override void GetTargetData(IFreeformEntity_MsgTgt tgt, ref List<byte> buffer)
        {
            if (tgt is FFTgt_G2H_GVA_RTE_Days_Status)
            {
                FFTgt_G2H_GVA_RTE_Days_Status tgt2 = tgt as FFTgt_G2H_GVA_RTE_Days_Status;
                buffer.Add(tgt2.Status.GetGmuIdInt8());
            }
            else
            {
                FFTgt_G2H_GVA_RTE_Days_Request tgt2 = tgt as FFTgt_G2H_GVA_RTE_Days_Request;
            }
        }
    }

    internal class FFParser_Tgt_MC300_GVA_RTE_Days_Resp_H2G
        : FFParser_Tgt_MC300_GVA_ActionData
    {
        internal FFParser_Tgt_MC300_GVA_RTE_Days_Resp_H2G()
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
            FFTgt_H2G_GVA_RTE_Days_Response tgt = new FFTgt_H2G_GVA_RTE_Days_Response();
            tgt.ExipreDays = FreeformHelper.GetBytesToBCDInt16(buffer, 0, 2);
            return tgt;
        }

        public override void GetTargetData(IFreeformEntity_MsgTgt tgt, ref List<byte> buffer)
        {
            FFTgt_H2G_GVA_RTE_Days_Response tgt2 = tgt as FFTgt_H2G_GVA_RTE_Days_Response;
            buffer.AddRange(tgt2.ExipreDays.GetBCDToBytes(2));
        }
    }

    #endregion //Restricted Ticket Expiration Days

    #region Ticket Key

    internal class FFParser_Tgt_MC300_GVA_TK_Req_G2H
        : FFParser_Tgt_MC300_GVA_ActionData
    {
        internal FFParser_Tgt_MC300_GVA_TK_Req_G2H()
            : base() { }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            if (buffer.Length == 1)
            {
                FFTgt_G2H_GVA_TK_Status tgt = new FFTgt_G2H_GVA_TK_Status();
                tgt.Status = buffer[0].GetAppId<FF_GmuId_ResponseStatus_Types, FF_AppId_ResponseStatus_Types>();
                return tgt;
            }
            else
            {
                FFTgt_G2H_GVA_TK_Request tgt = new FFTgt_G2H_GVA_TK_Request();
                return tgt;
            }
        }

        public override void GetTargetData(IFreeformEntity_MsgTgt tgt, ref List<byte> buffer)
        {
            if (tgt is FFTgt_G2H_GVA_TK_Status)
            {
                FFTgt_G2H_GVA_TK_Status tgt2 = tgt as FFTgt_G2H_GVA_TK_Status;
                buffer.Add(tgt2.Status.GetGmuIdInt8());
            }
            else
            {
                FFTgt_G2H_GVA_TK_Request tgt2 = tgt as FFTgt_G2H_GVA_TK_Request;
            }
        }
    }

    internal class FFParser_Tgt_MC300_GVA_TK_Resp_H2G
        : FFParser_Tgt_MC300_GVA_ActionData
    {
        internal FFParser_Tgt_MC300_GVA_TK_Resp_H2G()
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
            FFTgt_H2G_GVA_TK_Response tgt = new FFTgt_H2G_GVA_TK_Response();
            tgt.BarcodeKey = buffer.GetASCIIStringValue(0, 16);
            return tgt;
        }

        public override void GetTargetData(IFreeformEntity_MsgTgt tgt, ref List<byte> buffer)
        {
            FFTgt_H2G_GVA_TK_Response tgt2 = tgt as FFTgt_H2G_GVA_TK_Response;
            buffer.AddRange(tgt2.BarcodeKey.GetASCIIBytesValue(16));
        }
    }

    #endregion //Ticket Key

    #region Enable Printing of Restricted Tickets

    internal class FFParser_Tgt_MC300_GVA_EnablePrint_RT_Req_G2H
        : FFParser_Tgt_MC300_GVA_ActionData
    {
        internal FFParser_Tgt_MC300_GVA_EnablePrint_RT_Req_G2H()
            : base() { }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            if (buffer.Length == 1)
            {
                FFTgt_G2H_GVA_EnablePrint_RT_Status tgt = new FFTgt_G2H_GVA_EnablePrint_RT_Status();
                tgt.Status = buffer[0].GetAppId<FF_GmuId_ResponseStatus_Types, FF_AppId_ResponseStatus_Types>();
                return tgt;
            }
            else
            {
                FFTgt_G2H_GVA_EnablePrint_RT_Request tgt = new FFTgt_G2H_GVA_EnablePrint_RT_Request();
                return tgt;
            }
        }

        public override void GetTargetData(IFreeformEntity_MsgTgt tgt, ref List<byte> buffer)
        {
            if (tgt is FFTgt_G2H_GVA_EnablePrint_RT_Status)
            {
                FFTgt_G2H_GVA_EnablePrint_RT_Status tgt2 = tgt as FFTgt_G2H_GVA_EnablePrint_RT_Status;
                buffer.Add(tgt2.Status.GetGmuIdInt8());
            }
            else
            {
                FFTgt_G2H_GVA_EnablePrint_RT_Request tgt2 = tgt as FFTgt_G2H_GVA_EnablePrint_RT_Request;
            }
        }
    }

    internal class FFParser_Tgt_MC300_GVA_EnablePrint_RT_Resp_H2G
        : FFParser_Tgt_MC300_GVA_ActionData
    {
        internal FFParser_Tgt_MC300_GVA_EnablePrint_RT_Resp_H2G()
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

    #endregion

#if REVIEWED
    #region Restricted Ticket Expiration Days

    internal class FFParser_Tgt_MC300_GVA_EnablePrint_RT_Req_G2H : FFParser_Tgt_MC300_GVA_ActionData
    {
        internal FFParser_Tgt_MC300_GVA_EnablePrint_RT_Req_G2H()
            : base() { }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            FFTgt_G2H_GVA_EnablePrint_RT_Request tgt = new FFTgt_G2H_GVA_EnablePrint_RT_Request();
            return tgt;
        }
    }

    internal class FFParser_Tgt_MC300_GVA_EnablePrint_RT_Resp_H2G : FFParser_Tgt_MC300_GVA_ActionData
    {
        internal FFParser_Tgt_MC300_GVA_EnablePrint_RT_Resp_H2G()
            : base() { }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            FFTgt_H2G_GVA_EnablePrint_RT_Response tgt = new FFTgt_H2G_GVA_EnablePrint_RT_Response();
            tgt.EnableRT = (FF_GmuId_GmuVarAction_EnablePrint_RT)buffer[0];
            return tgt;
        }
    }

    internal class FFParser_Tgt_MC300_GVA_EnablePrint_RT_Status_H2G : FFParser_Tgt_MC300_GVA_ActionData
    {
        internal FFParser_Tgt_MC300_GVA_EnablePrint_RT_Status_H2G()
            : base() { }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            FFTgt_G2H_GVA_EnablePrint_RT_Status tgt = new FFTgt_G2H_GVA_EnablePrint_RT_Status();
            tgt.Status = (FF_ResponseStatus)buffer[0];
            return tgt;
        }
    }

    #endregion //Restricted Ticket Expiration Days

    #region Enhanced validation – Voucher Profile

    internal class FFParser_Tgt_MC300_GVA_VV_Profile_Req_G2H : FFParser_Tgt_MC300_GVA_ActionData
    {
        internal FFParser_Tgt_MC300_GVA_VV_Profile_Req_G2H()
            : base() { }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            FFTgt_G2H_GVA_VP_Request tgt = new FFTgt_G2H_GVA_VP_Request();
            return tgt;
        }
    }

    internal class FFParser_Tgt_MC300_GVA_VV_Profile_Resp_H2G : FFParser_Tgt_MC300_GVA_ActionData
    {
        internal FFParser_Tgt_MC300_GVA_VV_Profile_Resp_H2G()
            : base() { }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            FFTgt_H2G_GVA_VP_Response tgt = new FFTgt_H2G_GVA_VP_Response();
            tgt.MaxValidationID = FreeformHelper.GetValue<int>(buffer, 0, 1);
            tgt.MinLevelValidationID = FreeformHelper.GetValue<int>(buffer, 1, 2);
            return tgt;
        }
    }

    #endregion //Enhanced validation – Voucher Profile

    #region Enhanced validation – Voucher Validation Data

    internal class FFParser_Tgt_MC300_GVA_VV_Data_Req_G2H : FFParser_Tgt_MC300_GVA_ActionData
    {
        internal FFParser_Tgt_MC300_GVA_VV_Data_Req_G2H()
            : base() { }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            FFTgt_G2H_GVA_VV_Data_Request tgt = new FFTgt_G2H_GVA_VV_Data_Request();
            tgt.ValidationRequestID = FreeformHelper.GetValue<int>(buffer, 0, 9);
            tgt.NumValidationID = FreeformHelper.GetValue<int>(buffer, 9, 1);
            return tgt;
        }
    }

    internal class FFParser_Tgt_MC300_GVA_VV_Data_Resp_H2G : FFParser_Tgt_MC300_GVA_ActionData
    {
        internal FFParser_Tgt_MC300_GVA_VV_Data_Resp_H2G()
            : base() { }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            FFTgt_H2G_GVA_VV_Data_Response tgt = new FFTgt_H2G_GVA_VV_Data_Response();
            tgt.ValidationRequestID = FreeformHelper.GetValue<int>(buffer, 0, 9);
            tgt.ValidationIDs = FreeformHelper.CopyToBuffer(buffer, 9, buffer.Length - 9);
            return tgt;
        }
    }

    #endregion //Enhanced validation – Voucher Validation Data
#endif
}
