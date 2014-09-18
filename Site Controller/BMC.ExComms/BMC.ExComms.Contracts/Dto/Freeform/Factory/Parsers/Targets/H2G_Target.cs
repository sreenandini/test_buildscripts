using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    internal class FFParser_TgtFactory_Generic_H2G : FFTgtParser
    {
        internal FFParser_TgtFactory_Generic_H2G(FFParserDictionary subParsers)
            : base(subParsers) { }
    }

    internal class FFParser_TgtFactory_MC300_H2G : FFParser_TgtFactory_Generic_H2G
    {
        internal FFParser_TgtFactory_MC300_H2G(FFParserDictionary subParsers)
            : base(subParsers)
        {
            this.AddSubParsers();
        }

        protected override void AddSubParsersInternal()
        {
            base.AddSubParsersInternal();

            // GIM
            this.AddBufferEntityParser((int)FF_GmuId_TargetIds.GIM, (int)FF_AppId_TargetIds.GIM,
                new FFParser_Tgt_MC300_GIM_H2G());

            // Security
            this.AddBufferEntityParser(FreeformHelper.CreateCombinedId((int)FF_GmuId_SessionIds.Security, true, (int)FF_GmuId_TargetIds.Security, true),
                FreeformHelper.CreateCombinedId((int)FF_AppId_SessionIds.Security, true, (int)FF_AppId_TargetIds.Security, true),
                new FFParser_Tgt_MC300_Security_H2G());
            this.AddBufferEntityParser(FreeformHelper.CreateCombinedId((int)FF_GmuId_SessionIds.ECash, true, (int)FF_GmuId_TargetIds.Security, true),
                FreeformHelper.CreateCombinedId((int)FF_AppId_SessionIds.ECash, true, (int)FF_AppId_TargetIds.Security, true),
                new FFParser_Tgt_MC300_Security_H2G());
            this.AddBufferEntityParser(FreeformHelper.CreateCombinedId((int)FF_GmuId_SessionIds.Security, true, (int)FF_GmuId_TargetIds.Security, false),
                FreeformHelper.CreateCombinedId((int)FF_AppId_SessionIds.Security, true, (int)FF_AppId_TargetIds.Security, false),
                new FFParser_Tgt_MC300_Security_H2G());
            this.AddBufferEntityParser(FreeformHelper.CreateCombinedId((int)FF_GmuId_SessionIds.ECash, true, (int)FF_GmuId_TargetIds.Security, false),
                FreeformHelper.CreateCombinedId((int)FF_AppId_SessionIds.ECash, true, (int)FF_AppId_TargetIds.Security, false),
                new FFParser_Tgt_MC300_Security_H2G());

            // Tickets
            this.AddBufferEntityParser((int)FF_GmuId_TargetIds.Tickets, (int)FF_AppId_TargetIds.Tickets,
                new FFParser_Tgt_MC300_TicketInfo_H2G());
            this.AddBufferEntityParser(FreeformHelper.CreateRequestResponseId((int)FF_GmuId_TargetIds.Tickets, true),
                FreeformHelper.CreateRequestResponseId((int)FF_AppId_TargetIds.Tickets, true),
                new FFParser_Tgt_MC300_TicketInfo_H2G());
            this.AddBufferEntityParser(FreeformHelper.CreateCombinedId((int)FF_GmuId_SessionIds.Tickets, true, (int)FF_GmuId_TargetIds.Security, true),
               FreeformHelper.CreateCombinedId((int)FF_GmuId_SessionIds.Tickets, true, (int)FF_GmuId_TargetIds.Security, true),
               new FFParser_Tgt_MC300_Encrypted_H2G());

            // ECash
            this.AddBufferEntityParser((int)FF_GmuId_TargetIds.ECash, (int)FF_AppId_TargetIds.ECash,
                new FFParser_Tgt_MC300_EFT_H2G());
            this.AddBufferEntityParser(FreeformHelper.CreateCombinedId((int)FF_GmuId_SessionIds.ECash, false, (int)FF_GmuId_TargetIds.Security, true),
              FreeformHelper.CreateCombinedId((int)FF_AppId_SessionIds.ECash, false, (int)FF_AppId_TargetIds.Security, true),
              new FFParser_Tgt_MC300_Encrypted_H2G());

            // GMU Var Action
            this.AddBufferEntityParser((int)FF_GmuId_TargetIds.GMUVariableAction, (int)FF_AppId_TargetIds.GMUVariableAction,
                new FFParser_Tgt_MC300_GMUVarAction_H2G());

            // Game Message
            this.AddBufferEntityParser((int)FF_GmuId_TargetIds.GameMessage, (int)FF_AppId_TargetIds.GameMessage,
                new FFParser_Tgt_MC300_GameMessage_H2G());

            // PID
            this.AddBufferEntityParser((int)FF_GmuId_TargetIds.PID, (int)FF_AppId_TargetIds.PID,
                new FFParser_Tgt_MC300_PID_H2G());
        }
    }
}
