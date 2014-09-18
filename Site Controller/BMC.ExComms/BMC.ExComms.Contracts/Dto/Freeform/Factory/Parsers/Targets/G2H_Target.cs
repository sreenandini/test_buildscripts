using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    internal class FFParser_TgtFactory_Generic_G2H : FFTgtParser
    {
        internal FFParser_TgtFactory_Generic_G2H(FFParserDictionary subParsers)
            : base(subParsers) { }
    }

    internal class FFParser_TgtFactory_MC300_G2H : FFParser_TgtFactory_Generic_G2H
    {
        internal FFParser_TgtFactory_MC300_G2H(FFParserDictionary subParsers)
            : base(subParsers)
        {
            this.AddSubParsers();
        }

        protected override void AddSubParsersInternal()
        {
            base.AddSubParsersInternal();

            // GIM
            this.AddBufferEntityParser((int)FF_GmuId_TargetIds.GIM, (int)FF_AppId_TargetIds.GIM,
                new FFParser_Tgt_MC300_GIM_G2H());

            // Status Block (GMU Events)
            this.AddBufferEntityParser((int)FF_GmuId_TargetIds.StatusBlock, (int)FF_AppId_TargetIds.StatusBlock,
                new FFParser_Tgt_MC300_GMUEvent_G2H());

            // Meters
            this.AddBufferEntityParser((int)FF_GmuId_TargetIds.MeterBlock, (int)FF_AppId_TargetIds.MeterBlock,
                new FFParser_Tgt_MC300_AccountingMeters_G2H());

            // Security
            this.AddBufferEntityParser(FreeformHelper.CreateCombinedId((int)FF_GmuId_SessionIds.Security, true, (int)FF_GmuId_TargetIds.Security, true),
                FreeformHelper.CreateCombinedId((int)FF_AppId_SessionIds.Security, true, (int)FF_AppId_TargetIds.Security, true),
                new FFParser_Tgt_MC300_Security_G2H());
            this.AddBufferEntityParser(FreeformHelper.CreateCombinedId((int)FF_GmuId_SessionIds.ECash, true, (int)FF_GmuId_TargetIds.Security, true),
                FreeformHelper.CreateCombinedId((int)FF_AppId_SessionIds.ECash, true, (int)FF_AppId_TargetIds.Security, true),
                new FFParser_Tgt_MC300_Security_G2H());
            this.AddBufferEntityParser(FreeformHelper.CreateCombinedId((int)FF_GmuId_SessionIds.Security, true, (int)FF_GmuId_TargetIds.Security, false),
                FreeformHelper.CreateCombinedId((int)FF_AppId_SessionIds.Security, true, (int)FF_AppId_TargetIds.Security, false),
                new FFParser_Tgt_MC300_Security_G2H());
            this.AddBufferEntityParser(FreeformHelper.CreateCombinedId((int)FF_GmuId_SessionIds.ECash, true, (int)FF_GmuId_TargetIds.Security, false),
                FreeformHelper.CreateCombinedId((int)FF_AppId_SessionIds.ECash, true, (int)FF_AppId_TargetIds.Security, false),
                new FFParser_Tgt_MC300_Security_G2H());

            // Tickets
            this.AddBufferEntityParser((int)FF_GmuId_TargetIds.Tickets, (int)FF_AppId_TargetIds.Tickets,
                new FFParser_Tgt_MC300_TicketInfo_G2H());
            this.AddBufferEntityParser(FreeformHelper.CreateRequestResponseId((int)FF_GmuId_TargetIds.Tickets, true),
                FreeformHelper.CreateRequestResponseId((int)FF_AppId_TargetIds.Tickets, true), 
                new FFParser_Tgt_MC300_TicketInfo_G2H());
            this.AddBufferEntityParser(FreeformHelper.CreateCombinedId((int)FF_GmuId_SessionIds.Tickets, true, (int)FF_GmuId_TargetIds.Security, true),
               FreeformHelper.CreateCombinedId((int)FF_AppId_SessionIds.Tickets, true, (int)FF_AppId_TargetIds.Security, true),
               new FFParser_Tgt_MC300_Encrypted_G2H());

            // ECash
            this.AddBufferEntityParser((int)FF_GmuId_TargetIds.ECash, (int)FF_AppId_TargetIds.ECash,
                new FFParser_Tgt_MC300_EFT_G2H());
            this.AddBufferEntityParser(FreeformHelper.CreateCombinedId((int)FF_GmuId_SessionIds.ECash, false, (int)FF_GmuId_TargetIds.Security, true),
              FreeformHelper.CreateCombinedId((int)FF_AppId_SessionIds.ECash, false, (int)FF_AppId_TargetIds.Security, true),
              new FFParser_Tgt_MC300_Encrypted_G2H());

            // GMU Var Action
            this.AddBufferEntityParser((int)FF_GmuId_TargetIds.GMUVariableAction, (int)FF_AppId_TargetIds.GMUVariableAction,
                new FFParser_Tgt_MC300_GMUVarAction_G2H());

            // Game Message
            this.AddBufferEntityParser((int)FF_GmuId_TargetIds.GameMessage, (int)FF_AppId_TargetIds.GameMessage,
                new FFParser_Tgt_MC300_GameMessage_G2H());

            // PID
            this.AddBufferEntityParser((int)FF_GmuId_TargetIds.PID, (int)FF_AppId_TargetIds.PID,
                new FFParser_Tgt_MC300_PID_G2H());
        }
    }
}
