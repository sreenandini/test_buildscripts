using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.ExComms.Contracts.DTO.Freeform;

namespace BMC.ExComms.Contracts.DTO.Monitor
{
    internal abstract class MonTgtParser_Status_EmployeeCardBase_G2H
        : MonTgtParser_Status_StdData_G2H
    {
    }

    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
        typeof(FFTgt_G2H_GMUEvent_StdData),        
        (int)FaultSource.PriorityEvent,
        (int)FaultType_PriorityEvent.EmployeeCardIn,
        (int)FF_AppId_GMUEvent_XCodes.EmpCardInXC)]
    internal class MonTgtParser_Status_EmployeeCardIn_G2H
        : MonTgtParser_Status_EmployeeCardBase_G2H
    {
        protected override IMonitorEntity_MsgTgt CreateMonitorStatusTarget(FFTgt_G2H_GMUEvent_StdData tgtSrc)
        {
            return new MonTgt_G2H_Status_EmployeeCardIn()
            {
                CardNumber = tgtSrc.PlayerCard,
            };
        }
    }

    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
        typeof(FFTgt_G2H_GMUEvent_StdData),
        (int)FaultSource.PriorityEvent,
        (int)FaultType_PriorityEvent.EmployeecardOut,
        (int)FF_AppId_GMUEvent_XCodes.EmpCardOutXC)]
    internal class MonTgtParser_Status_EmployeeCardOut_G2H
        : MonTgtParser_Status_EmployeeCardBase_G2H
    {
        protected override IMonitorEntity_MsgTgt CreateMonitorStatusTarget(FFTgt_G2H_GMUEvent_StdData tgtSrc)
        {
            return new MonTgt_G2H_Status_EmployeeCardOut()
            {
                CardNumber = tgtSrc.PlayerCard,
            };
        }
    }
}