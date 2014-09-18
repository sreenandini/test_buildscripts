using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    internal abstract class FFParser_MsgEnum_SessionID_Generic_B2B : FFEnumParser
    {
        internal FFParser_MsgEnum_SessionID_Generic_B2B() { }
    }

    internal sealed class FFParser_MsgEnum_SessionID_MC300_B2B : FFParser_MsgEnum_SessionID_Generic_B2B
    {
        internal FFParser_MsgEnum_SessionID_MC300_B2B()
        {
            this.AddSubParsers();
        }

        protected override void AddSubParsersInternal()
        {
            base.AddSubParsersInternal();
            this.AddBufferEntityParser(typeof(FF_GmuId_SessionIds), typeof(FF_AppId_SessionIds), this);
        }
    }
}
