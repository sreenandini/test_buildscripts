using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.ExComms.Contracts.DTO.Freeform;

namespace BMC.ExComms.Contracts.DTO.Monitor
{
    internal abstract class MonTgtParser_Status_PeriodicBase_G2H
        : MonTgtParser_Status_DescriptionBase_G2H
    {
        protected override IMonitorEntity_MsgTgt CreateMonitorStatusTarget(FFTgt_G2H_GMUEvent_StdData tgtSrc)
        {
            return new MonTgt_G2H_Status_Periodic() { 
                Description = this.Description,
            };
        }
    }

    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
        typeof(FFTgt_G2H_GMUEvent_StdData),
        (int)FaultSource.NonPriorityEvent,
        (int)FaultType_NonPriorityEvent.Periodic,
        (int)FF_AppId_GMUEvent_XCodes.PeriodicXC)]
    internal class MonTgtParser_Status_Periodic_G2H
        : MonTgtParser_Status_PeriodicBase_G2H
    {
        internal override string Description
        {
            get { return "Periodic"; }
        }
    }

    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
        typeof(FFTgt_G2H_GMUEvent_StdData),
        (int)FaultSource.NonPriorityEvent,
        (int)FaultType_NonPriorityEvent.InstantPeriodic,
        (int)FF_AppId_GMUEvent_XCodes.InstantPeriodicXC)]
    internal class MonTgtParser_Status_InstanctPeriodic_G2H
        : MonTgtParser_Status_PeriodicBase_G2H
    {
        internal override string Description
        {
            get { return "Instant Periodic"; }
        }
    }

    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
        typeof(FFTgt_G2H_GMUEvent_StdData),
        (int)FaultSource.NonPriorityEvent,
        (int)FaultType_NonPriorityEvent.ForcedPeriodic,
        (int)FF_AppId_GMUEvent_XCodes.ForcedPeriodicXC)]
    internal class MonTgtParser_Status_ForcedPeriodic_G2H
        : MonTgtParser_Status_PeriodicBase_G2H
    {
        internal override string Description
        {
            get { return "Forced Periodic"; }
        }
    }
}
