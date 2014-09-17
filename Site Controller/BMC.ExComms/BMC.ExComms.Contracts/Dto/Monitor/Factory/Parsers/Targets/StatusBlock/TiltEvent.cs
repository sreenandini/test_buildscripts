using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.ExComms.Contracts.DTO.Freeform;

namespace BMC.ExComms.Contracts.DTO.Monitor
{
    [ErrorEventCategory("Tilt")]
    internal abstract class MonTgtParser_Status_TiltEventBase_G2H
        : MonTgtParser_Status_DescriptionBase_G2H
    {
        protected override IMonitorEntity_MsgTgt CreateMonitorStatusTarget(FFTgt_G2H_GMUEvent_StdData tgtSrc)
        {
            return new MonTgt_G2H_Status_TiltEvent()
            {
                Description = this.Description,
            };
        }
    }

    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
       typeof(FFTgt_G2H_GMUEvent_StdData),
       (int)FaultSource.TiltEvent,
       (int)FaultType_TiltEvent.Reel1Tilt41,
       (int)FF_AppId_GMUEvent_XCodes.Tilt41XC)]
    internal class MonTgtParser_Status_TiltEvent_0x41_G2H
        : MonTgtParser_Status_TiltEventBase_G2H
    {
        internal override string Description
        {
            get { return "Reel 1 Tilt.Tilt41"; }
        }
    }

    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
       typeof(FFTgt_G2H_GMUEvent_StdData),
       (int)FaultSource.TiltEvent,
       (int)FaultType_TiltEvent.Reel2Tilt42,
       (int)FF_AppId_GMUEvent_XCodes.Tilt42XC)]
    internal class MonTgtParser_Status_TiltEvent_0x42_G2H
        : MonTgtParser_Status_TiltEventBase_G2H
    {
        internal override string Description
        {
            get { return "Reel 2 Tilt.Tilt42"; }
        }
    }

    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
       typeof(FFTgt_G2H_GMUEvent_StdData),
       (int)FaultSource.TiltEvent,
       (int)FaultType_TiltEvent.Reel3Tilt43,
       (int)FF_AppId_GMUEvent_XCodes.Tilt43XC)]
    internal class MonTgtParser_Status_TiltEvent_0x43_G2H
        : MonTgtParser_Status_TiltEventBase_G2H
    {
        internal override string Description
        {
            get { return "Reel 3 Tilt.Tilt43"; }
        }
    }

    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
       typeof(FFTgt_G2H_GMUEvent_StdData),
       (int)FaultSource.TiltEvent,
       (int)FaultType_TiltEvent.Reel4Tilt44,
       (int)FF_AppId_GMUEvent_XCodes.Tilt44XC)]
    internal class MonTgtParser_Status_TiltEvent_0x44_G2H
        : MonTgtParser_Status_TiltEventBase_G2H
    {
        internal override string Description
        {
            get { return "Reel 4 Tilt.Tilt44"; }
        }
    }

    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
       typeof(FFTgt_G2H_GMUEvent_StdData),
       (int)FaultSource.TiltEvent,
       (int)FaultType_TiltEvent.Reel5Tilt45,
       (int)FF_AppId_GMUEvent_XCodes.Tilt45XC)]
    internal class MonTgtParser_Status_TiltEvent_0x45_G2H
        : MonTgtParser_Status_TiltEventBase_G2H
    {
        internal override string Description
        {
            get { return "Reel 5 Tilt.Tilt45"; }
        }
    }

    //[MonTgtParserMappingG2H(FF_FlowDirection.G2H,
    //   typeof(FFTgt_G2H_GMUEvent_StdData),
    //   (int)FaultSource.TiltEvent,
    //   (int)FaultType_TiltEvent.Tilt47,
    //   (int)FF_AppId_GMUEvent_XCodes.Tilt47XC)]
    //internal class MonTgtParser_Status_TiltEvent_0x47_G2H
    //    : MonTgtParser_Status_TiltEventBase_G2H
    //{
    //    internal override string Description
    //    {
    //        get { return "Tilt.Tilt47"; }
    //    }
    //}

    //[MonTgtParserMappingG2H(FF_FlowDirection.G2H,
    //   typeof(FFTgt_G2H_GMUEvent_StdData),
    //   (int)FaultSource.TiltEvent,
    //   (int)FaultType_TiltEvent.Tilt48,
    //   (int)FF_AppId_GMUEvent_XCodes.Tilt48XC)]
    //internal class MonTgtParser_Status_TiltEvent_0x48_G2H
    //    : MonTgtParser_Status_TiltEventBase_G2H
    //{
    //    internal override string Description
    //    {
    //        get { return "Tilt.Tilt48"; }
    //    }
    //}

    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
       typeof(FFTgt_G2H_GMUEvent_StdData),
       (int)FaultSource.TiltEvent,
       (int)FaultType_TiltEvent.SlotMachineTilt64,
       (int)FF_AppId_GMUEvent_XCodes.Tilt64XC)]
    internal class MonTgtParser_Status_TiltEvent_0x64_G2H
        : MonTgtParser_Status_TiltEventBase_G2H
    {
        internal override string Description
        {
            get { return "Slot machine tilt.Tilt64"; }
        }
    }

    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
       typeof(FFTgt_G2H_GMUEvent_StdData),
       (int)FaultSource.TiltEvent,
       (int)FaultType_TiltEvent.ReelSpinAfterIndexTilt81,
       (int)FF_AppId_GMUEvent_XCodes.Tilt81XC)]
    internal class MonTgtParser_Status_TiltEvent_0x81_G2H
        : MonTgtParser_Status_TiltEventBase_G2H
    {
        internal override string Description
        {
            get { return "Reel spin after index.Tilt81"; }
        }
    }

    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
       typeof(FFTgt_G2H_GMUEvent_StdData),
       (int)FaultSource.TiltEvent,
       (int)FaultType_TiltEvent.ReelSpinAfterIndexTilt82,
       (int)FF_AppId_GMUEvent_XCodes.Tilt82XC)]
    internal class MonTgtParser_Status_TiltEvent_0x82_G2H
        : MonTgtParser_Status_TiltEventBase_G2H
    {
        internal override string Description
        {
            get { return "Reel spin after index.Tilt82"; }
        }
    }

    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
       typeof(FFTgt_G2H_GMUEvent_StdData),
       (int)FaultSource.TiltEvent,
       (int)FaultType_TiltEvent.ReelSpinAfterIndexTilt83,
       (int)FF_AppId_GMUEvent_XCodes.Tilt83XC)]
    internal class MonTgtParser_Status_TiltEvent_0x83_G2H
        : MonTgtParser_Status_TiltEventBase_G2H
    {
        internal override string Description
        {
            get { return "Reel spin after index.Tilt83"; }
        }
    }

    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
       typeof(FFTgt_G2H_GMUEvent_StdData),
       (int)FaultSource.TiltEvent,
       (int)FaultType_TiltEvent.ReelSpinAfterIndexTilt84,
       (int)FF_AppId_GMUEvent_XCodes.Tilt84XC)]
    internal class MonTgtParser_Status_TiltEvent_0x84_G2H
        : MonTgtParser_Status_TiltEventBase_G2H
    {
        internal override string Description
        {
            get { return "Reel spin after index.Tilt84"; }
        }
    }

    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
       typeof(FFTgt_G2H_GMUEvent_StdData),
       (int)FaultSource.TiltEvent,
       (int)FaultType_TiltEvent.ReelSpinAfterIndexTilt85,
       (int)FF_AppId_GMUEvent_XCodes.Tilt85XC)]
    internal class MonTgtParser_Status_TiltEvent_0x85_G2H
        : MonTgtParser_Status_TiltEventBase_G2H
    {
        internal override string Description
        {
            get { return "Reel spin after index.Tilt85"; }
        }
    }
}
