using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.ExComms.Contracts.DTO.Freeform;

namespace BMC.ExComms.Contracts.DTO.Monitor
{
    internal abstract class MonTgtParser_Status_CreditBase_G2H
        : MonTgtParser_Status_StdData_G2H
    {
        protected override IMonitorEntity_MsgTgt CreateMonitorStatusTarget(FFTgt_G2H_GMUEvent_StdData tgtSrc)
        {
            return new MonTgt_G2H_Status_Credit() { };
        }
    }

    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
        typeof(FFTgt_G2H_GMUEvent_StdData),
        (int)FaultSource.PriorityEvent,
        (int)FaultType_PriorityEvent.ZeroCredit,
        (int)FF_AppId_GMUEvent_XCodes.ZeroCurCreditsXC)]
    internal class MonTgtParser_Status_ZeroCredit_Priority_G2H
        : MonTgtParser_Status_CreditBase_G2H
    {
    }

    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
        typeof(FFTgt_G2H_GMUEvent_StdData),
        (int)FaultSource.NonPriorityEvent,
        (int)FaultType_NonPriorityEvent.ZeroCredit,
        (int)FF_AppId_GMUEvent_XCodes.EXTRA_ZeroCurCreditsXC)] // TODO
    internal class MonTgtParser_Status_ZeroCredit_NonPriority_G2H
        : MonTgtParser_Status_CreditBase_G2H
    {
    }

    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
        typeof(FFTgt_G2H_GMUEvent_StdData),
        (int)FaultSource.PriorityEvent,
        (int)FaultType_PriorityEvent.NonZeroCredit,
        (int)FF_AppId_GMUEvent_XCodes.ZeroToCreditsXC)]
    internal class MonTgtParser_Status_NonZeroCredit_G2H
        : MonTgtParser_Status_CreditBase_G2H
    {
    }
}