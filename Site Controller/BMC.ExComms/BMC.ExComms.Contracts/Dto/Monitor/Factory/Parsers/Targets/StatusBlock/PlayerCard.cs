using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.ExComms.Contracts.DTO.Freeform;

namespace BMC.ExComms.Contracts.DTO.Monitor
{
    internal abstract class MonTgtParser_Status_PlayerCardBase_G2H
        : MonTgtParser_Status_StdData_G2H
    {
    }

    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
        typeof(FFTgt_G2H_GMUEvent_StdData),        
        (int)FaultSource.PriorityEvent,
        (int)FaultType_PriorityEvent.PlayerCardIn,
        (int)FF_AppId_GMUEvent_XCodes.PlayerCardInInfoXC)]
    internal class MonTgtParser_Status_PlayerCardIn_G2H
        : MonTgtParser_Status_PlayerCardBase_G2H
    {
        protected override IMonitorEntity_MsgTgt CreateMonitorStatusTarget(FFTgt_G2H_GMUEvent_StdData tgtSrc)
        {
            return new MonTgt_G2H_Status_PlayerCardIn()
            {
                CardNumber = tgtSrc.PlayerCard,
            };
        }
    }

    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
        typeof(FFTgt_G2H_GMUEvent_StdData),
        (int)FaultSource.PriorityEvent,
        (int)FaultType_PriorityEvent.PlayercardOut,
        (int)FF_AppId_GMUEvent_XCodes.PlayerCardOutXC)]
    internal class MonTgtParser_Status_PlayerCardOut_G2H
        : MonTgtParser_Status_PlayerCardBase_G2H
    {
        protected override IMonitorEntity_MsgTgt CreateMonitorStatusTarget(FFTgt_G2H_GMUEvent_StdData tgtSrc)
        {
            return new MonTgt_G2H_Status_PlayerCardOut()
            {
                CardNumber = tgtSrc.PlayerCard,
            };
        }
    }
}