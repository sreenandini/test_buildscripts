using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{

    #region Indexed Key Exchange Start
    internal class FFParser_Tgt_Generic_IndexKeyExchange_Start : FFTgtParser
    {
        internal FFParser_Tgt_Generic_IndexKeyExchange_Start()
            : base() { }
    }

    internal class FFParser_Tgt_MC300_IndexKeyExchange_Start : FFParser_Tgt_Generic_IndexKeyExchange_Start
    {
        internal FFParser_Tgt_MC300_IndexKeyExchange_Start()
            : base() { }
    }

    internal class FFParser_Tgt_MC300_IndexKeyExchange_Start_G2H : FFParser_Tgt_MC300_IndexKeyExchange_Start
    {
        internal FFParser_Tgt_MC300_IndexKeyExchange_Start_G2H()
            : base() { }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            FFTgt_B2B_Security_Indexed_KeyExchange_Start tgt = new FFTgt_B2B_Security_Indexed_KeyExchange_Start();
            tgt.KeyIndex = (FF_AppId_KeyExchange)buffer[0];
            tgt.Key = FreeformHelper.CopyToBuffer(buffer, 1, 9);

            return tgt;
        }
    }

    internal class FFParser_Tgt_MC300_IndexKeyExchange_Start_H2G : FFParser_Tgt_MC300_IndexKeyExchange_Start
    {
        internal FFParser_Tgt_MC300_IndexKeyExchange_Start_H2G()
            : base() { }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            FFTgt_B2B_Security_Indexed_KeyExchange_Start tgt = new FFTgt_B2B_Security_Indexed_KeyExchange_Start();
            tgt.KeyIndex = (FF_AppId_KeyExchange)buffer[0];
            tgt.Key = FreeformHelper.CopyToBuffer(buffer, 1, 9);
            return tgt;
        }
    }

    #endregion

    #region Indexed Key Exchange End

    internal class FFParser_Tgt_Generic_IndexKeyExchange_End : FFTgtParser
    {
        internal FFParser_Tgt_Generic_IndexKeyExchange_End()
            : base() { }
    }

    internal class FFParser_Tgt_MC300_IndexKeyExchange_End : FFParser_Tgt_Generic_IndexKeyExchange_End
    {
        internal FFParser_Tgt_MC300_IndexKeyExchange_End()
            : base() { }
    }

    internal class FFParser_Tgt_MC300_IndexKeyExchange_End_G2H : FFParser_Tgt_MC300_IndexKeyExchange_End
    {
        internal FFParser_Tgt_MC300_IndexKeyExchange_End_G2H()
            : base() { }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            FFTgt_B2B_Security_Indexed_KeyExchange_End tgt = new FFTgt_B2B_Security_Indexed_KeyExchange_End();
            tgt.KeyIndex = (FF_AppId_KeyExchange)buffer[0];
            tgt.Key = FreeformHelper.CopyToBuffer(buffer, 1, 9);
            return tgt;
        }
    }

    internal class FFParser_Tgt_MC300_IndexKeyExchange_End_H2G : FFParser_Tgt_MC300_IndexKeyExchange_End
    {
        internal FFParser_Tgt_MC300_IndexKeyExchange_End_H2G()
            : base() { }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            FFTgt_B2B_Security_Indexed_KeyExchange_End tgt = new FFTgt_B2B_Security_Indexed_KeyExchange_End();
            tgt.KeyIndex = (FF_AppId_KeyExchange)buffer[0];
            tgt.Key = FreeformHelper.CopyToBuffer(buffer, 1, 9);
            return tgt;
        }
    }
    #endregion
}
