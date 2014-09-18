using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{

     internal class FFParser_Tgt_Generic_GVA_EFT
        : FFTgtParser
    {
        internal FFParser_Tgt_Generic_GVA_EFT()
            : base() { }
    }

    internal class FFParser_Tgt_MC300_GVA_EFT 
        : FFParser_Tgt_Generic_GVA_EFT
    {
        internal FFParser_Tgt_MC300_GVA_EFT ()
            : base() { }
    }
    #region EFT CHAR Request
    internal class FFParser_Tgt_MC300_GVA_Eftchar_Req_G2H : FFParser_Tgt_MC300_GVA_EFT
    {
        internal FFParser_Tgt_MC300_GVA_Eftchar_Req_G2H()
            : base() { }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            FFTgt_G2H_GVA_EFT_CHAR_Request tgt = new FFTgt_G2H_GVA_EFT_CHAR_Request();
            return tgt;
        }
    }
    #endregion 

# region EFT CHAR Response
    internal class FFParser_Tgt_MC300_GVA_EFTCHAR_Resp_H2G
           : FFParser_Tgt_MC300_GVA_EFT
    {
        internal FFParser_Tgt_MC300_GVA_EFTCHAR_Resp_H2G()
            : base() { }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            FFTgt_H2G_GVA_EFT_Char_Response tgt = new FFTgt_H2G_GVA_EFT_Char_Response();
            // Check wih Vinoth For Adding Boolean
          
            return tgt;
        }

        public override void GetTargetData(IFreeformEntity_MsgTgt tgt, ref List<byte> buffer)
        {
            FFTgt_H2G_GVA_EFT_Char_Response tgt2 = tgt as FFTgt_H2G_GVA_EFT_Char_Response;
            // Check wih Vinoth 
        }
    }

#endregion

#region EFT Char Staus

    internal class FFParser_Tgt_MC300_GVA_EFTCHAR_Status_H2G : FFParser_Tgt_MC300_GVA_EFT
    {
        internal FFParser_Tgt_MC300_GVA_EFTCHAR_Status_H2G()
            : base() { }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            FFTgt_G2H_EFT_CHAR_Status tgt = new FFTgt_G2H_EFT_CHAR_Status();
            tgt.Status = buffer[0].GetAppId<FF_GmuId_ResponseStatus_Types, FF_AppId_ResponseStatus_Types>();
            return tgt;
        }
    }
#endregion
}
