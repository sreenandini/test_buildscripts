using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.ExComms.Contracts.DTO.Freeform;

namespace BMC.ExComms.Contracts.DTO.Monitor
{
    [ErrorEventCategory("Door")]
    internal abstract class MonTgtParser_Status_DoorEventBase_G2H
        : MonTgtParser_Status_DescriptionBase_G2H
    {
        protected override IMonitorEntity_MsgTgt CreateMonitorStatusTarget(FFTgt_G2H_GMUEvent_StdData tgtSrc)
        {
            return new MonTgt_G2H_Status_DoorEvent()
            {
                Description = this.Description,
            };
        }
    }

    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
       typeof(FFTgt_G2H_GMUEvent_StdData),
       (int)FaultSource.DoorEvent,
       (int)FaultType_DoorEvent.GameMPURemoved,
       (int)FF_AppId_GMUEvent_XCodes.GameMPUOutXC)]
    internal class MonTgtParser_Status_DoorEvent_0x32_G2H
        : MonTgtParser_Status_DoorEventBase_G2H
    {
        internal override string Description
        {
            get { return "Game MPU removed"; }
        }
    }

    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
       typeof(FFTgt_G2H_GMUEvent_StdData),
       (int)FaultSource.DoorEvent,
       (int)FaultType_DoorEvent.GameMPUReinstalled,
       (int)FF_AppId_GMUEvent_XCodes.GameMPUInXC)]
    internal class MonTgtParser_Status_DoorEvent_0x33_G2H
        : MonTgtParser_Status_DoorEventBase_G2H
    {
        internal override string Description
        {
            get { return "Game MPU reinstalled"; }
        }
    }

    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
       typeof(FFTgt_G2H_GMUEvent_StdData),
       (int)FaultSource.DoorEvent,
       (int)FaultType_DoorEvent.AuxFillDoorOpened,
       (int)FF_AppId_GMUEvent_XCodes.FillDoorOpenedXC)]
    internal class MonTgtParser_Status_DoorEvent_0x35_G2H
        : MonTgtParser_Status_DoorEventBase_G2H
    {
        internal override string Description
        {
            get { return "Aux fill door opened"; }
        }
    }

    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
       typeof(FFTgt_G2H_GMUEvent_StdData),
       (int)FaultSource.DoorEvent,
       (int)FaultType_DoorEvent.AuxFillDoorClosed,
       (int)FF_AppId_GMUEvent_XCodes.FillDoorClosedXC)]
    internal class MonTgtParser_Status_DoorEvent_0x36_G2H
        : MonTgtParser_Status_DoorEventBase_G2H
    {
        internal override string Description
        {
            get { return "Aux fill door closed"; }
        }
    }

    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
       typeof(FFTgt_G2H_GMUEvent_StdData),
       (int)FaultSource.DoorEvent,
       (int)FaultType_DoorEvent.AcceptorRemoved,
       (int)FF_AppId_GMUEvent_XCodes.AcceptorRemovedXC)]
    internal class MonTgtParser_Status_DoorEvent_0x66_G2H
        : MonTgtParser_Status_DoorEventBase_G2H
    {
        internal override string Description
        {
            get { return "Acceptor removed"; }
        }
    }

    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
       typeof(FFTgt_G2H_GMUEvent_StdData),
       (int)FaultSource.DoorEvent,
       (int)FaultType_DoorEvent.SlotDoorOpened,
       (int)FF_AppId_GMUEvent_XCodes.SlotDoorOpenedXC)]
    internal class MonTgtParser_Status_DoorEvent_0x71_G2H
        : MonTgtParser_Status_DoorEventBase_G2H
    {
        internal override string Description
        {
            get { return "Slot door opened"; }
        }
    }

    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
       typeof(FFTgt_G2H_GMUEvent_StdData),
       (int)FaultSource.DoorEvent,
       (int)FaultType_DoorEvent.SlotDoorClosed,
       (int)FF_AppId_GMUEvent_XCodes.SlotDoorClosedXC)]
    internal class MonTgtParser_Status_DoorEvent_0x72_G2H
        : MonTgtParser_Status_DoorEventBase_G2H
    {
        internal override string Description
        {
            get { return "Slot door closed"; }
        }
    }

    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
       typeof(FFTgt_G2H_GMUEvent_StdData),
       (int)FaultSource.DoorEvent,
       (int)FaultType_DoorEvent.DropDoorOpened,
       (int)FF_AppId_GMUEvent_XCodes.DropDoorOpenedXC)]
    internal class MonTgtParser_Status_DoorEvent_0x73_G2H
        : MonTgtParser_Status_DoorEventBase_G2H
    {
        internal override string Description
        {
            get { return "Drop Door opened"; }
        }
    }

    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
       typeof(FFTgt_G2H_GMUEvent_StdData),
       (int)FaultSource.DoorEvent,
       (int)FaultType_DoorEvent.DropDoorClosed,
       (int)FF_AppId_GMUEvent_XCodes.DropDoorClosedXC)]
    internal class MonTgtParser_Status_DoorEvent_0x74_G2H
        : MonTgtParser_Status_DoorEventBase_G2H
    {
        internal override string Description
        {
            get { return "Drop door closed"; }
        }
    }

    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
       typeof(FFTgt_G2H_GMUEvent_StdData),
       (int)FaultSource.DoorEvent,
       (int)FaultType_DoorEvent.AcceptorDoorOpened,
       (int)FF_AppId_GMUEvent_XCodes.VendDoorOpenedXC)]
    internal class MonTgtParser_Status_DoorEvent_0x75_G2H
        : MonTgtParser_Status_DoorEventBase_G2H
    {
        internal override string Description
        {
            get { return "Acceptor door opened"; }
        }
    }

    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
       typeof(FFTgt_G2H_GMUEvent_StdData),
       (int)FaultSource.DoorEvent,
       (int)FaultType_DoorEvent.AcceptorDoorClosed,
       (int)FF_AppId_GMUEvent_XCodes.VendDoorClosedXC)]
    internal class MonTgtParser_Status_DoorEvent_0x76_G2H
        : MonTgtParser_Status_DoorEventBase_G2H
    {
        internal override string Description
        {
            get { return "Acceptor door closed"; }
        }
    }

    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
       typeof(FFTgt_G2H_GMUEvent_StdData),
       (int)FaultSource.DoorEvent,
       (int)FaultType_DoorEvent.BillCassetteRemoved,
       (int)FF_AppId_GMUEvent_XCodes.StackerRemovedXC)]
    internal class MonTgtParser_Status_DoorEvent_0x79_G2H
        : MonTgtParser_Status_DoorEventBase_G2H
    {
        internal override string Description
        {
            get { return "Bill cassette removed"; }
        }
    }

    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
       typeof(FFTgt_G2H_GMUEvent_StdData),
       (int)FaultSource.DoorEvent,
       (int)FaultType_DoorEvent.BillCassetteReInserted,
       (int)FF_AppId_GMUEvent_XCodes.StackerReInsertedXC)]
    internal class MonTgtParser_Status_DoorEvent_0x80_G2H
        : MonTgtParser_Status_DoorEventBase_G2H
    {
        internal override string Description
        {
            get { return "Bill cassette ReInserted"; }
        }
    }

    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
       typeof(FFTgt_G2H_GMUEvent_StdData),
       (int)FaultSource.DoorEvent,
       (int)FaultType_DoorEvent.BillCassetteDoorOpened,
       (int)FF_AppId_GMUEvent_XCodes.CashDoorOpenedXC)]
    internal class MonTgtParser_Status_DoorEvent_0x96_G2H
        : MonTgtParser_Status_DoorEventBase_G2H
    {
        internal override string Description
        {
            get { return "Bill cassette door opened"; }
        }
    }

    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
       typeof(FFTgt_G2H_GMUEvent_StdData),
       (int)FaultSource.DoorEvent,
       (int)FaultType_DoorEvent.BillCassetteDoorClosed,
       (int)FF_AppId_GMUEvent_XCodes.CashDoorClosedXC)]
    internal class MonTgtParser_Status_DoorEvent_0x97_G2H
        : MonTgtParser_Status_DoorEventBase_G2H
    {
        internal override string Description
        {
            get { return "Bill cassette door closed"; }
        }
    }

    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
       typeof(FFTgt_G2H_GMUEvent_StdData),
       (int)FaultSource.DoorEvent,
       (int)FaultType_DoorEvent.MPUCompartmentOpened,
       (int)FF_AppId_GMUEvent_XCodes.MPUDoorOpenXC)]
    internal class MonTgtParser_Status_DoorEvent_0xA7_G2H
        : MonTgtParser_Status_DoorEventBase_G2H
    {
        internal override string Description
        {
            get { return "MPU compartment opened"; }
        }
    }

    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
       typeof(FFTgt_G2H_GMUEvent_StdData),
       (int)FaultSource.DoorEvent,
       (int)FaultType_DoorEvent.MPUCompartmentClosed,
       (int)FF_AppId_GMUEvent_XCodes.MPUDoorClosedXC)]
    internal class MonTgtParser_Status_DoorEvent_0xA8_G2H
        : MonTgtParser_Status_DoorEventBase_G2H
    {
        internal override string Description
        {
            get { return "MPU compartment closed"; }
        }
    }

    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
       typeof(FFTgt_G2H_GMUEvent_StdData),
       (int)FaultSource.DoorEvent,
       (int)FaultType_DoorEvent.GMUCompartmentOpened,
       (int)FF_AppId_GMUEvent_XCodes.GMUDoorOpenXC)]
    internal class MonTgtParser_Status_DoorEvent_0xA9_G2H
        : MonTgtParser_Status_DoorEventBase_G2H
    {
        internal override string Description
        {
            get { return "GMU compartment opened"; }
        }
    }

    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
       typeof(FFTgt_G2H_GMUEvent_StdData),
       (int)FaultSource.DoorEvent,
       (int)FaultType_DoorEvent.GMUCompartmentClosed,
       (int)FF_AppId_GMUEvent_XCodes.GMUDoorClosedXC)]
    internal class MonTgtParser_Status_DoorEvent_0xAA_G2H
        : MonTgtParser_Status_DoorEventBase_G2H
    {
        internal override string Description
        {
            get { return "GMU compartment closed"; }
        }
    }

    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
       typeof(FFTgt_G2H_GMUEvent_StdData),
       (int)FaultSource.DoorEvent,
       (int)FaultType_DoorEvent.PowerOffCardCageAccess,
       (int)FF_AppId_GMUEvent_XCodes.POffCardCageAccessXC)]
    internal class MonTgtParser_Status_DoorEvent_0xC8_G2H
        : MonTgtParser_Status_DoorEventBase_G2H
    {
        internal override string Description
        {
            get { return "Power Off Card Cage Access"; }
        }
    }

    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
       typeof(FFTgt_G2H_GMUEvent_StdData),
       (int)FaultSource.DoorEvent,
       (int)FaultType_DoorEvent.PowerOffSlotDoorAccess,
       (int)FF_AppId_GMUEvent_XCodes.POffSlotDoorAccessXC)]
    internal class MonTgtParser_Status_DoorEvent_0xC9_G2H
        : MonTgtParser_Status_DoorEventBase_G2H
    {
        internal override string Description
        {
            get { return "Power Off Slot Door Access"; }
        }
    }

    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
       typeof(FFTgt_G2H_GMUEvent_StdData),
       (int)FaultSource.DoorEvent,
       (int)FaultType_DoorEvent.PowerOffCashBoxDoorAccess,
       (int)FF_AppId_GMUEvent_XCodes.POffCashBoxAccessXC)]
    internal class MonTgtParser_Status_DoorEvent_0xCA_G2H
        : MonTgtParser_Status_DoorEventBase_G2H
    {
        internal override string Description
        {
            get { return "Power Off Cash Box Door Access"; }
        }
    }

    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
       typeof(FFTgt_G2H_GMUEvent_StdData),
       (int)FaultSource.DoorEvent,
       (int)FaultType_DoorEvent.PowerOffDropDoorAccess,
       (int)FF_AppId_GMUEvent_XCodes.POffDropDoorAccessXC)]
    internal class MonTgtParser_Status_DoorEvent_0xCB_G2H
        : MonTgtParser_Status_DoorEventBase_G2H
    {
        internal override string Description
        {
            get { return "Power Off Drop Door Access"; }
        }
    }
}
