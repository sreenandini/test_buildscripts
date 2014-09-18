using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.CoreLib;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    internal class FFParser_MsgFactory_MC300_H2G 
        : FFParser_MsgFactory_Generic_H2G
    {
        internal FFParser_MsgFactory_MC300_H2G(FFParserDictionary subParsers, IFFTgtParser targetParser)
            : base(subParsers, targetParser)
        {
            this.RegisterMessageParsers();
        }

        public override byte DataLength
        {
            get
            {
                return 1;
            }
        }

        private void RegisterMessageParsers()
        {
            using (ILogMethod method = Log.LogMethod("", ".cctor()"))
            {
                try
                {
                    IFFParser parser1 = new FFParser_Msg_MC300_H2G_1(this) { TargetParser = this.TargetParser };
                    IFFParser parser2 = new FFParser_Msg_MC300_H2G_2(this) { TargetParser = this.TargetParser };
                    IFFParser parser3 = new FFParser_Msg_MC300_H2G_3(this) { TargetParser = this.TargetParser };

                    this.AddBufferEntityParser((int)-1, -1, parser1);
                    this.AddBufferEntityParser((int)FF_GmuId_H2G_PollCodes._RESERVED, (int)FF_AppId_H2G_PollCodes._RESERVED, parser1);
                    this.AddBufferEntityParser((int)FF_GmuId_H2G_PollCodes.BonusPointMultiplier, (int)FF_AppId_H2G_PollCodes.BonusPointMultiplier, parser1);
                    this.AddBufferEntityParser((int)FF_GmuId_H2G_PollCodes.ConditionalDisplay, (int)FF_AppId_H2G_PollCodes.ConditionalDisplay, parser1);
                    this.AddBufferEntityParser((int)FF_GmuId_H2G_PollCodes.ConditionalForcedExceptionCode, (int)FF_AppId_H2G_PollCodes.ConditionalForcedExceptionCode, parser1);
                    this.AddBufferEntityParser((int)FF_GmuId_H2G_PollCodes.ECash, (int)FF_AppId_H2G_PollCodes.ECash, parser1);
                    this.AddBufferEntityParser((int)FF_GmuId_H2G_PollCodes.ECashEnable, (int)FF_AppId_H2G_PollCodes.ECashEnable, parser1);
                    this.AddBufferEntityParser((int)FF_GmuId_H2G_PollCodes.ForcedPeriodic, (int)FF_AppId_H2G_PollCodes.ForcedPeriodic, parser1);
                    this.AddBufferEntityParser((int)FF_GmuId_H2G_PollCodes.Freeform2, (int)FF_AppId_H2G_PollCodes.Freeform2, parser2);
                    this.AddBufferEntityParser((int)FF_GmuId_H2G_PollCodes.Freeform2Poll, (int)FF_AppId_H2G_PollCodes.Freeform2Poll, parser2);
                    this.AddBufferEntityParser((int)FF_GmuId_H2G_PollCodes.Freeform2Resend, (int)FF_AppId_H2G_PollCodes.Freeform2Resend, parser2);
                    this.AddBufferEntityParser((int)FF_GmuId_H2G_PollCodes.Freeform3NoResponse, (int)FF_AppId_H2G_PollCodes.Freeform3NoResponse, parser3);
                    this.AddBufferEntityParser((int)FF_GmuId_H2G_PollCodes.Freeform3Response, (int)FF_AppId_H2G_PollCodes.Freeform3Response, parser3);
                    this.AddBufferEntityParser((int)FF_GmuId_H2G_PollCodes.FreeformNoResponse, (int)FF_AppId_H2G_PollCodes.FreeformNoResponse, parser1);
                    this.AddBufferEntityParser((int)FF_GmuId_H2G_PollCodes.FreeformResponse, (int)FF_AppId_H2G_PollCodes.FreeformResponse, parser1);
                    this.AddBufferEntityParser((int)FF_GmuId_H2G_PollCodes.JackpotResponse, (int)FF_AppId_H2G_PollCodes.JackpotResponse, parser1);
                    this.AddBufferEntityParser((int)FF_GmuId_H2G_PollCodes.None, (int)FF_AppId_H2G_PollCodes.None, parser1);
                    this.AddBufferEntityParser((int)FF_GmuId_H2G_PollCodes.Poll, (int)FF_AppId_H2G_PollCodes.Poll, parser1);
                    this.AddBufferEntityParser((int)FF_GmuId_H2G_PollCodes.PollNews, (int)FF_AppId_H2G_PollCodes.PollNews, parser1);
                    this.AddBufferEntityParser((int)FF_GmuId_H2G_PollCodes.Promo, (int)FF_AppId_H2G_PollCodes.Promo, parser1);
                    this.AddBufferEntityParser((int)FF_GmuId_H2G_PollCodes.TransportACK, (int)FF_AppId_H2G_PollCodes.TransportACK, parser1);
                    this.AddBufferEntityParser((int)FF_GmuId_H2G_PollCodes.TransportACKDelayedOrPlayerInfo, (int)FF_AppId_H2G_PollCodes.TransportACKDelayedOrPlayerInfo, parser1);
                    this.AddBufferEntityParser((int)FF_GmuId_H2G_PollCodes.TransportNACK, (int)FF_AppId_H2G_PollCodes.TransportNACK, parser1);
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }
            }
        }
    }

    internal class FFParser_Msg_MC300_H2G_1 
        : FFParser_Msg_Generic_H2G_1
    {
        internal FFParser_Msg_MC300_H2G_1(IFFParser_MsgFactory_Generic_B2B parentParser)
            : base(parentParser) { }
    }

    internal class FFParser_Msg_MC300_H2G_2 
        : FFParser_Msg_Generic_H2G_2
    {
        internal FFParser_Msg_MC300_H2G_2(IFFParser_MsgFactory_Generic_B2B parentParser)
            : base(parentParser) { }
    }

    internal class FFParser_Msg_MC300_H2G_3 
        : FFParser_Msg_Generic_H2G_3
    {
        internal FFParser_Msg_MC300_H2G_3(IFFParser_MsgFactory_Generic_B2B parentParser)
            : base(parentParser) { }
    }
}
