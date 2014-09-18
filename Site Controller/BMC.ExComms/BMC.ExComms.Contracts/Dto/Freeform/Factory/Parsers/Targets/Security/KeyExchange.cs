using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    internal class FFParser_Tgt_Generic_Security_KeyExchange
        : FFTgtParser_NoSubTargets
    {
        internal FFParser_Tgt_Generic_Security_KeyExchange()
            : base() { }
    }

    internal class FFParser_Tgt_MC300_Security_KeyExchange
        : FFParser_Tgt_Generic_Security_KeyExchange
    {
        internal FFParser_Tgt_MC300_Security_KeyExchange()
            : base() { }
    }

    internal abstract class FFParser_Tgt_MC300_Security_KeyExchange_DataFlow_1
        : FFParser_Tgt_MC300_Security_KeyExchange
    {
        internal FFParser_Tgt_MC300_Security_KeyExchange_DataFlow_1()
            : base() { }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            if (buffer.Length == 9)
            {
                FFTgt_B2B_Security_KeyExchange_End tgt = new FFTgt_B2B_Security_KeyExchange_End
                {
                    PartialKey = buffer
                };
                return tgt;
            }
            else
            {
                FFTgt_B2B_Security_KeyExchange_Request tgt = new FFTgt_B2B_Security_KeyExchange_Request();
                return tgt;
            }
        }

        public override void GetTargetData(IFreeformEntity_MsgTgt tgt, ref List<byte> buffer)
        {
            if (tgt is FFTgt_B2B_Security_KeyExchange_Request)
            {
                FFTgt_B2B_Security_KeyExchange_Request tgt2 = (FFTgt_B2B_Security_KeyExchange_Request)tgt;
            }
            else if (tgt is FFTgt_B2B_Security_KeyExchange_End)
            {
                FFTgt_B2B_Security_KeyExchange_End tgt2 = (FFTgt_B2B_Security_KeyExchange_End)tgt;
                buffer.AddRange(tgt2.PartialKey.CopyToBuffer(0, 9));
            }

        }
    }

    internal abstract class FFParser_Tgt_MC300_Security_KeyExchange_DataFlow_2
        : FFParser_Tgt_MC300_Security_KeyExchange
    {
        internal FFParser_Tgt_MC300_Security_KeyExchange_DataFlow_2()
            : base() { }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            if (buffer.Length == 9)
            {
                FFTgt_B2B_Security_KeyExchange_PartialKey tgt = new FFTgt_B2B_Security_KeyExchange_PartialKey
                {
                    PartialKey = buffer
                };
                return tgt;
            }
            else
            {
                FFTgt_B2B_Security_KeyExchange_Status tgt = new FFTgt_B2B_Security_KeyExchange_Status()
                {
                    Status = buffer[0].GetAppId<FF_GmuId_ResponseStatus_Types, FF_AppId_ResponseStatus_Types>(),
                };
                return tgt;
            }
        }

        public override void GetTargetData(IFreeformEntity_MsgTgt tgt, ref List<byte> buffer)
        {
            if (tgt is FFTgt_B2B_Security_KeyExchange_PartialKey)
            {
                FFTgt_B2B_Security_KeyExchange_PartialKey tgt2 = (FFTgt_B2B_Security_KeyExchange_PartialKey)tgt;
                buffer.AddRange(tgt2.PartialKey.CopyToBuffer(0, 9));
            }
            else if (tgt is FFTgt_B2B_Security_KeyExchange_Status)
            {
                FFTgt_B2B_Security_KeyExchange_Status tgt2 = (FFTgt_B2B_Security_KeyExchange_Status)tgt;
                buffer.Add(tgt2.Status.GetGmuIdInt8());
            }
        }
    }

    internal class FFParser_Tgt_MC300_Security_KeyExchange_GmuInitiated_G2H
        : FFParser_Tgt_MC300_Security_KeyExchange_DataFlow_1
    {
        public override FF_FlowInitiation FlowInitiation
        {
            get
            {
                return FF_FlowInitiation.Gmu;
            }
        }
    }

    internal class FFParser_Tgt_MC300_Security_KeyExchange_GmuInitiated_H2G
        : FFParser_Tgt_MC300_Security_KeyExchange_DataFlow_2
    {
        public override FF_FlowInitiation FlowInitiation
        {
            get
            {
                return FF_FlowInitiation.Host;
            }
        }
    }

    internal class FFParser_Tgt_MC300_Security_KeyExchange_HostInitiated_H2G
        : FFParser_Tgt_MC300_Security_KeyExchange_DataFlow_1
    {
        public override FF_FlowInitiation FlowInitiation
        {
            get
            {
                return FF_FlowInitiation.Host;
            }
        }
    }

    internal class FFParser_Tgt_MC300_Security_KeyExchange_HostInitiated_G2H
        : FFParser_Tgt_MC300_Security_KeyExchange_DataFlow_2
    {
        public override FF_FlowInitiation FlowInitiation
        {
            get
            {
                return FF_FlowInitiation.Gmu;
            }
        }
    }
}
