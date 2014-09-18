using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.CoreLib;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    internal class FFFactory_MC300_G2H 
        : FFFactory_Generic_G2H
    {
        internal FFFactory_MC300_G2H()
        {
            _targetParser = new FFParser_TgtFactory_MC300_G2H(new FFParserDictionary());
            _messageParser = new FFParser_MsgFactory_MC300_G2H(new FFParserDictionary(), _targetParser);
        }
    }

    internal class FFFactory_MC300_H2G
        : FFFactory_Generic_H2G
    {
        internal FFFactory_MC300_H2G()
        {
            _targetParser = new FFParser_TgtFactory_MC300_H2G(new FFParserDictionary());
            _messageParser = new FFParser_MsgFactory_MC300_H2G(new FFParserDictionary(), _targetParser);
        }
    }
}
