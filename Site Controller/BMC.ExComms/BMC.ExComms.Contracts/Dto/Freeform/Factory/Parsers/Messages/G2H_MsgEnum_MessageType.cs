using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    internal abstract class FFParser_Msg_MessageType_Generic_G2H : FFEnumParser
    {
        internal FFParser_Msg_MessageType_Generic_G2H() { }
    }

    internal sealed class FFParser_Msg_MessageType_MC300_G2H : FFParser_Msg_MessageType_Generic_G2H
    {
        internal FFParser_Msg_MessageType_MC300_G2H()
        {
            this.AddSubParsers();
        }

        protected override void AddSubParsersInternal()
        {
            base.AddSubParsersInternal();
            this.AddBufferEntityParser(typeof(FF_GmuId_G2H_MessageTypes), typeof(FF_AppId_G2H_MessageTypes), this);
        }
    }
}
