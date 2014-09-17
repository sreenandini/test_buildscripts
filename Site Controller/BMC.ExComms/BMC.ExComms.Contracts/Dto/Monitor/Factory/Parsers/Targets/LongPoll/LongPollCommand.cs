using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.ExComms.Contracts.DTO.Freeform;

namespace BMC.ExComms.Contracts.DTO.Monitor
{
    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
                    typeof(FFTgt_G2H_GameMessage_SASCommand),
                    (int)FaultSource.LongPoll,
                    (int)FaultType_LongPollCode.LPC_Custom)]
    internal class MonTgtParser_GameMessage_SASCommand_G2H
        : MonTgtParser_G2H
    {
        protected override IMonitorEntity_MsgTgt CreateMonitorTarget(IMonitorEntity parent, IFreeformEntity_MsgTgt request)
        {
            FFTgt_G2H_GameMessage_SASCommand ffTgt = request as FFTgt_G2H_GameMessage_SASCommand;
            IMonitorEntity_MsgTgt tgtDest = null;

            if (ffTgt != null)
            {
                FF_AppId_LongPollCodes cmd = (FF_AppId_LongPollCodes)ffTgt.LongPollCommand;
                switch (cmd)
                {
                    case FF_AppId_LongPollCodes.LPC_MachineEnable:
                        tgtDest = new MonTgt_G2H_Client_EnableMachine()
                        {
                            FaultType = (int)FaultType_LongPollCode.LPC_EnableMachine,
                        };
                        break;

                    case FF_AppId_LongPollCodes.LPC_MachineDisable:
                        tgtDest = new MonTgt_G2H_Client_DisableMachine()
                        {
                            FaultType = (int)FaultType_LongPollCode.LPC_DisableMachine,
                        };
                        break;

                    default:
                        break;
                }
            }
            return tgtDest;
        }
    }
}
