using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.CoreLib;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    internal class FFParser_MsgFactory_MC300_G2H : FFParser_MsgFactory_Generic_G2H
    {
        internal FFParser_MsgFactory_MC300_G2H(FFParserDictionary subParsers, IFFTgtParser targetParser)
            : base(subParsers, targetParser)
        {
            this.RegisterMessageParsers();
        }

        private void RegisterMessageParsers()
        {
            using (ILogMethod method = Log.LogMethod("", ".cctor()"))
            {
                try
                {
                    IFFParser parser1 = new FFParser_Msg_MC300_G2H_1(this) { TargetParser = this.TargetParser };
                    IFFParser parser2 = new FFParser_Msg_MC300_G2H_2(this) { TargetParser = this.TargetParser };
                    IFFParser parser3 = new FFParser_Msg_MC300_G2H_3(this) { TargetParser = this.TargetParser };

                    this.AddBufferEntityParser((int)-1, -1, parser1);
                    this.AddBufferEntityParser((int)FF_GmuId_G2H_Commands.ACK, (int)FF_AppId_G2H_Commands.ACK, parser1);
                    this.AddBufferEntityParser((int)FF_GmuId_G2H_Commands.Freeform2, (int)FF_AppId_G2H_Commands.Freeform2, parser2);
                    this.AddBufferEntityParser((int)FF_GmuId_G2H_Commands.Freeform3NoResponse, (int)FF_AppId_G2H_Commands.Freeform3NoResponse, parser3);
                    this.AddBufferEntityParser((int)FF_GmuId_G2H_Commands.Freeform3Response, (int)FF_AppId_G2H_Commands.Freeform3Response, parser3);
                    this.AddBufferEntityParser((int)FF_GmuId_G2H_Commands.GMUInitA0, (int)FF_AppId_G2H_Commands.GMUInitA0, parser1);
                    this.AddBufferEntityParser((int)FF_GmuId_G2H_Commands.NACK, (int)FF_AppId_G2H_Commands.NACK, parser1);
                    this.AddBufferEntityParser((int)FF_GmuId_G2H_Commands.None, (int)FF_AppId_G2H_Commands.None, parser1);
                    this.AddBufferEntityParser((int)FF_GmuId_G2H_Commands.ResponseRequest, (int)FF_AppId_G2H_Commands.ResponseRequest, parser1);
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }
            }
        }
    }

    internal class FFParser_Msg_MC300_G2H_1 : FFParser_Msg_Generic_G2H_1
    {
        internal FFParser_Msg_MC300_G2H_1(IFFParser_MsgFactory_Generic_B2B parentParser)
            : base(parentParser) { }
    }

    internal class FFParser_Msg_MC300_G2H_2 : FFParser_Msg_Generic_G2H_2
    {
        internal FFParser_Msg_MC300_G2H_2(IFFParser_MsgFactory_Generic_B2B parentParser)
            : base(parentParser) { }
    }

    internal class FFParser_Msg_MC300_G2H_3 : FFParser_Msg_Generic_G2H_3
    {
        internal FFParser_Msg_MC300_G2H_3(IFFParser_MsgFactory_Generic_B2B parentParser)
            : base(parentParser) { }
    }
}
