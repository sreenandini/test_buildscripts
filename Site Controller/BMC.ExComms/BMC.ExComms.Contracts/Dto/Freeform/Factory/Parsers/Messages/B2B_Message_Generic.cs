using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.CoreLib;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    internal interface IFFParser_MsgFactory_Generic_B2B
        : IFFParser
    {
        byte DataLength { get; }
    }

    internal abstract class FFParser_MsgFactory_Generic_B2B
        : FFMsgParser, IFFParser_MsgFactory_Generic_B2B
    {
        internal FFParser_MsgFactory_Generic_B2B(FFParserDictionary subParsers, IFFTgtParser targetParser)
            : base(subParsers, targetParser) { }

        public virtual byte DataLength
        {
            get
            {
                return 2;
            }
        }
    }
}
