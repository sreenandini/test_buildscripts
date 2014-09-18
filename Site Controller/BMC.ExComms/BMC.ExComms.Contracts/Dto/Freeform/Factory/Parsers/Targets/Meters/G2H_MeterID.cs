using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    internal abstract class FFParser_MsgEnum_AccMeterID_Generic_G2H 
        : FFEnumParser
    {
        internal FFParser_MsgEnum_AccMeterID_Generic_G2H() { }
    }

    internal sealed class FFParser_MsgEnum_AccMeterID_MC300_G2H 
        : FFParser_MsgEnum_AccMeterID_Generic_G2H
    {
        internal FFParser_MsgEnum_AccMeterID_MC300_G2H()
        {
            this.AddSubParsers();
        }

        protected override void AddSubParsersInternal()
        {
            base.AddSubParsersInternal();
            this.AddBufferEntityParser(typeof(FF_GmuId_AccountingMeterIds), typeof(FF_AppId_AccountingMeterIds), this);
        }
    }
}
