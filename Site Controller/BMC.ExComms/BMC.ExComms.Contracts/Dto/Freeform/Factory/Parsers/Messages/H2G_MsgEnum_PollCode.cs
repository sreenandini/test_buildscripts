using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    internal abstract class FFParser_MsgEnum_PollCode_Generic_H2G : FFEnumParser
    {
        internal FFParser_MsgEnum_PollCode_Generic_H2G() { }
    }

    internal sealed class FFParser_MsgEnum_PollCode_MC300_H2G : FFParser_MsgEnum_PollCode_Generic_H2G
    {
        internal FFParser_MsgEnum_PollCode_MC300_H2G()
        {
            this.AddSubParsers();
        }

        protected override void AddSubParsersInternal()
        {
            base.AddSubParsersInternal();
            this.AddBufferEntityParser(typeof(FF_GmuId_H2G_PollCodes), typeof(FF_AppId_H2G_PollCodes), this);
        }
    }
}
