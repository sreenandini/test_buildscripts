using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.ExComms.Contracts.DTO.Freeform;

namespace BMC.ExComms.Contracts.DTO.Monitor
{
    [ErrorEventCategory("Device Error")]
    internal abstract class MonTgtParser_Status_DeviceFaultEvent_G2H
        : MonTgtParser_Status_DescriptionBase_G2H
    {
        protected override IMonitorEntity_MsgTgt CreateMonitorStatusTarget(FFTgt_G2H_GMUEvent_StdData tgtSrc)
        {
            return new MonTgt_G2H_Status_DeviceFaultEvent()
            {
                Description = this.Description,
            };
        }
    }

    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
       typeof(FFTgt_G2H_GMUEvent_StdData),
       (int)FaultSource.DeviceFaultEvent,
       (int)FaultType_DeviceFaultEvent.AcceptorHopperJam,
       (int)FF_AppId_GMUEvent_XCodes.AcceptorHopperJamXC)]
    internal class MonTgtParser_Status_DeviceFaultEvent_0x03_G2H
        : MonTgtParser_Status_DeviceFaultEvent_G2H
    {
        internal override string Description
        {
            get { return "Acceptor Hopper Jam"; }
        }
    }

    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
       typeof(FFTgt_G2H_GMUEvent_StdData),
       (int)FaultSource.DeviceFaultEvent,
       (int)FaultType_DeviceFaultEvent.AcceptorCantvend,
       (int)FF_AppId_GMUEvent_XCodes.AcceptorCantvendXC)]
    internal class MonTgtParser_Status_DeviceFaultEvent_0x16_G2H
        : MonTgtParser_Status_DeviceFaultEvent_G2H
    {
        internal override string Description
        {
            get { return "Acceptor can’t vend"; }
        }
    }

    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
       typeof(FFTgt_G2H_GMUEvent_StdData),
       (int)FaultSource.DeviceFaultEvent,
       (int)FaultType_DeviceFaultEvent.AcceptorRunawayHopper,
       (int)FF_AppId_GMUEvent_XCodes.BallDropXC)]
    internal class MonTgtParser_Status_DeviceFaultEvent_0x19_G2H
        : MonTgtParser_Status_DeviceFaultEvent_G2H
    {
        internal override string Description
        {
            get { return "Acceptor runaway hopper"; }
        }
    }

    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
       typeof(FFTgt_G2H_GMUEvent_StdData),
       (int)FaultSource.DeviceFaultEvent,
       (int)FaultType_DeviceFaultEvent.CoinOutJam,
       (int)FF_AppId_GMUEvent_XCodes.Tilt54XC)]
    internal class MonTgtParser_Status_DeviceFaultEvent_0x54_G2H
        : MonTgtParser_Status_DeviceFaultEvent_G2H
    {
        internal override string Description
        {
            get { return "Coin out jam"; }
        }
    }

    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
       typeof(FFTgt_G2H_GMUEvent_StdData),
       (int)FaultSource.DeviceFaultEvent,
       (int)FaultType_DeviceFaultEvent.BillCassetteIsFull,
       (int)FF_AppId_GMUEvent_XCodes.StackerFullXC)]
    internal class MonTgtParser_Status_DeviceFaultEvent_0x67_G2H
        : MonTgtParser_Status_DeviceFaultEvent_G2H
    {
        internal override string Description
        {
            get { return "Bill cassette is full"; }
        }
    }

    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
       typeof(FFTgt_G2H_GMUEvent_StdData),
       (int)FaultSource.DeviceFaultEvent,
       (int)FaultType_DeviceFaultEvent.BillCassetteIsJammed,
       (int)FF_AppId_GMUEvent_XCodes.StackerJammedXC)]
    internal class MonTgtParser_Status_DeviceFaultEvent_0x68_G2H
        : MonTgtParser_Status_DeviceFaultEvent_G2H
    {
        internal override string Description
        {
            get { return "Bill cassette is jammed"; }
        }
    }

    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
       typeof(FFTgt_G2H_GMUEvent_StdData),
       (int)FaultSource.DeviceFaultEvent,
       (int)FaultType_DeviceFaultEvent.AcceptorNotResponding,
       (int)FF_AppId_GMUEvent_XCodes.AcceptorNotRespondingXC)]
    internal class MonTgtParser_Status_DeviceFaultEvent_0x69_G2H
        : MonTgtParser_Status_DeviceFaultEvent_G2H
    {
        internal override string Description
        {
            get { return "Acceptor not responding"; }
        }
    }

    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
       typeof(FFTgt_G2H_GMUEvent_StdData),
       (int)FaultSource.DeviceFaultEvent,
       (int)FaultType_DeviceFaultEvent.AcceptorFunctioningAgain,
       (int)FF_AppId_GMUEvent_XCodes.VendMalFixedXC)]
    internal class MonTgtParser_Status_DeviceFaultEvent_0x70_G2H
        : MonTgtParser_Status_DeviceFaultEvent_G2H
    {
        internal override string Description
        {
            get { return "Acceptor functioning again"; }
        }
    }

    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
       typeof(FFTgt_G2H_GMUEvent_StdData),
       (int)FaultSource.DeviceFaultEvent,
       (int)FaultType_DeviceFaultEvent.CoinInJam,
       (int)FF_AppId_GMUEvent_XCodes.Tilt90XC)]
    internal class MonTgtParser_Status_DeviceFaultEvent_0x90_G2H
        : MonTgtParser_Status_DeviceFaultEvent_G2H
    {
        internal override string Description
        {
            get { return "Coin in jam"; }
        }
    }

    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
       typeof(FFTgt_G2H_GMUEvent_StdData),
       (int)FaultSource.DeviceFaultEvent,
       (int)FaultType_DeviceFaultEvent.CoinDropSwitchStuck,
       (int)FF_AppId_GMUEvent_XCodes.Tilt91XC)]
    internal class MonTgtParser_Status_DeviceFaultEvent_0x91_G2H
        : MonTgtParser_Status_DeviceFaultEvent_G2H
    {
        internal override string Description
        {
            get { return "Coin drop switch stuck"; }
        }
    }

    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
       typeof(FFTgt_G2H_GMUEvent_StdData),
       (int)FaultSource.DeviceFaultEvent,
       (int)FaultType_DeviceFaultEvent.AcceptorJammed,
       (int)FF_AppId_GMUEvent_XCodes.BillJammedXC)]
    internal class MonTgtParser_Status_DeviceFaultEvent_0x92_G2H
        : MonTgtParser_Status_DeviceFaultEvent_G2H
    {
        internal override string Description
        {
            get { return "Acceptor jammed"; }
        }
    }

    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
       typeof(FFTgt_G2H_GMUEvent_StdData),
       (int)FaultSource.DeviceFaultEvent,
       (int)FaultType_DeviceFaultEvent.TooManyCoinsIn,
       (int)FF_AppId_GMUEvent_XCodes.Tilt93XC)]
    internal class MonTgtParser_Status_DeviceFaultEvent_0x93_G2H
        : MonTgtParser_Status_DeviceFaultEvent_G2H
    {
        internal override string Description
        {
            get { return "Too many coins in"; }
        }
    }

    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
       typeof(FFTgt_G2H_GMUEvent_StdData),
       (int)FaultSource.DeviceFaultEvent,
       (int)FaultType_DeviceFaultEvent.DisplayFault,
       (int)FF_AppId_GMUEvent_XCodes.GameDispComErrXC)]
    internal class MonTgtParser_Status_DeviceFaultEvent_0xA3_G2H
        : MonTgtParser_Status_DeviceFaultEvent_G2H
    {
        internal override string Description
        {
            get { return "Display fault"; }
        }
    }

    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
       typeof(FFTgt_G2H_GMUEvent_StdData),
       (int)FaultSource.DeviceFaultEvent,
       (int)FaultType_DeviceFaultEvent.TouchScreenError,
       (int)FF_AppId_GMUEvent_XCodes.TouchScreenFaultXC)]
    internal class MonTgtParser_Status_DeviceFaultEvent_0xA4_G2H
        : MonTgtParser_Status_DeviceFaultEvent_G2H
    {
        internal override string Description
        {
            get { return "Touch Screen error"; }
        }
    }

    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
       typeof(FFTgt_G2H_GMUEvent_StdData),
       (int)FaultSource.DeviceFaultEvent,
       (int)FaultType_DeviceFaultEvent.LowBatteryCondition,
       (int)FF_AppId_GMUEvent_XCodes.LowRamBatteryXC)]
    internal class MonTgtParser_Status_DeviceFaultEvent_0xA5_G2H
        : MonTgtParser_Status_DeviceFaultEvent_G2H
    {
        internal override string Description
        {
            get { return "Low battery condition"; }
        }
    }

    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
       typeof(FFTgt_G2H_GMUEvent_StdData),
       (int)FaultSource.DeviceFaultEvent,
       (int)FaultType_DeviceFaultEvent.GameEPROMSignatureFault,
       (int)FF_AppId_GMUEvent_XCodes.EPROMSigFailXC)]
    internal class MonTgtParser_Status_DeviceFaultEvent_0xA6_G2H
        : MonTgtParser_Status_DeviceFaultEvent_G2H
    {
        internal override string Description
        {
            get { return "Game EPROM Signature Fault"; }
        }
    }

    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
       typeof(FFTgt_G2H_GMUEvent_StdData),
       (int)FaultSource.DeviceFaultEvent,
       (int)FaultType_DeviceFaultEvent.SlotPrinterFault,
       (int)FF_AppId_GMUEvent_XCodes.SlotPrinterFaultXC)]
    internal class MonTgtParser_Status_DeviceFaultEvent_0xB0_G2H
        : MonTgtParser_Status_DeviceFaultEvent_G2H
    {
        internal override string Description
        {
            get { return "Slot Printer Fault"; }
        }
    }
}
