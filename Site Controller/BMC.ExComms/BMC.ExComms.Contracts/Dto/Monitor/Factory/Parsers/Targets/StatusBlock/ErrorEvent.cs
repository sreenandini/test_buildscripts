using BMC.ExComms.Contracts.DTO.Freeform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Monitor
{
    [ErrorEventCategory("Error")]
    internal abstract class MonTgtParser_Status_ErrorEventBase_G2H
        : MonTgtParser_Status_DescriptionBase_G2H
    {
        protected override IMonitorEntity_MsgTgt CreateMonitorStatusTarget(FFTgt_G2H_GMUEvent_StdData tgtSrc)
        {
            return new MonTgt_G2H_Status_ErrorEvent()
            {
                Description = this.Description,
            };
        }
    }

    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
       typeof(FFTgt_G2H_GMUEvent_StdData),
       (int)FaultSource.ErrorEvent,
       (int)FaultType_ErrorEvent.TooManyBadPINs,
       (int)FF_AppId_GMUEvent_XCodes.TooManyBadPINs)]
    internal class MonTgtParser_Status_ErrorEvent_0x02_G2H
        : MonTgtParser_Status_ErrorEventBase_G2H
    {
        internal override string Description
        {
            get { return "Too Many Bad PINs"; }
        }
    }

    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
       typeof(FFTgt_G2H_GMUEvent_StdData),
       (int)FaultSource.ErrorEvent,
       (int)FaultType_ErrorEvent.DivMalfunXC,
       (int)FF_AppId_GMUEvent_XCodes.DivMalfunXC)]
    internal class MonTgtParser_Status_ErrorEvent_0x09_G2H
        : MonTgtParser_Status_ErrorEventBase_G2H
    {
        internal override string Description
        {
            get { return "DivMalfunXC"; }
        }
    }

    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
       typeof(FFTgt_G2H_GMUEvent_StdData),
       (int)FaultSource.ErrorEvent,
       (int)FaultType_ErrorEvent.IllegalCardremoval,
       (int)FF_AppId_GMUEvent_XCodes.IllegalCardremoval)]
    internal class MonTgtParser_Status_ErrorEvent_0x13_G2H
        : MonTgtParser_Status_ErrorEventBase_G2H
    {
        internal override string Description
        {
            get { return "Illegal Card removal"; }
        }
    }

    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
       typeof(FFTgt_G2H_GMUEvent_StdData),
       (int)FaultSource.ErrorEvent,
       (int)FaultType_ErrorEvent.BadMagCardReader,
       (int)FF_AppId_GMUEvent_XCodes.BadMagCardReader)]
    internal class MonTgtParser_Status_ErrorEvent_0x14_G2H
        : MonTgtParser_Status_ErrorEventBase_G2H
    {
        internal override string Description
        {
            get { return "Bad mag card reader"; }
        }
    }

    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
       typeof(FFTgt_G2H_GMUEvent_StdData),
       (int)FaultSource.ErrorEvent,
       (int)FaultType_ErrorEvent.AcceptorLargeBuyIn,
       (int)FF_AppId_GMUEvent_XCodes.LargeBuyIn)]
    internal class MonTgtParser_Status_ErrorEvent_0x15_G2H
        : MonTgtParser_Status_ErrorEventBase_G2H
    {
        internal override string Description
        {
            get { return "Acceptor large buy-in"; }
        }
    }

    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
       typeof(FFTgt_G2H_GMUEvent_StdData),
       (int)FaultSource.ErrorEvent,
       (int)FaultType_ErrorEvent.AcceptorBadPay,
       (int)FF_AppId_GMUEvent_XCodes.BillVendXC)]
    internal class MonTgtParser_Status_ErrorEvent_0x18_G2H
        : MonTgtParser_Status_ErrorEventBase_G2H
    {
        internal override string Description
        {
            get { return "Acceptor bad pay"; }
        }
    }

    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
       typeof(FFTgt_G2H_GMUEvent_StdData),
       (int)FaultSource.ErrorEvent,
       (int)FaultType_ErrorEvent.BonusPointRollover,
       (int)FF_AppId_GMUEvent_XCodes.WETOverflowXC)]
    internal class MonTgtParser_Status_ErrorEvent_0x20_G2H
        : MonTgtParser_Status_ErrorEventBase_G2H
    {
        internal override string Description
        {
            get { return "Bonus point rollover"; }
        }
    }

    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
       typeof(FFTgt_G2H_GMUEvent_StdData),
       (int)FaultSource.ErrorEvent,
       (int)FaultType_ErrorEvent.BadMachinePayAmt,
       (int)FF_AppId_GMUEvent_XCodes.IncorrectPayoutXC)]
    internal class MonTgtParser_Status_ErrorEvent_0x31_G2H
        : MonTgtParser_Status_ErrorEventBase_G2H
    {
        internal override string Description
        {
            get { return "Bad Machine Pay amt"; }
        }
    }

    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
       typeof(FFTgt_G2H_GMUEvent_StdData),
       (int)FaultSource.ErrorEvent,
       (int)FaultType_ErrorEvent.ResetDuringPayout,
       (int)FF_AppId_GMUEvent_XCodes.Tilt47XC)]
    internal class MonTgtParser_Status_ErrorEvent_0x47_G2H
        : MonTgtParser_Status_ErrorEventBase_G2H
    {
        internal override string Description
        {
            get { return "Reset during payout"; }
        }
    }

    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
       typeof(FFTgt_G2H_GMUEvent_StdData),
       (int)FaultSource.ErrorEvent,
       (int)FaultType_ErrorEvent.ExtraCoinsPaidOut,
       (int)FF_AppId_GMUEvent_XCodes.Tilt48XC)]
    internal class MonTgtParser_Status_ErrorEvent_0x48_G2H
        : MonTgtParser_Status_ErrorEventBase_G2H
    {
        internal override string Description
        {
            get { return "Extra coins paid out"; }
        }
    }

    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
       typeof(FFTgt_G2H_GMUEvent_StdData),
       (int)FaultSource.ErrorEvent,
       (int)FaultType_ErrorEvent.NoDataOnMagCard,
       (int)FF_AppId_GMUEvent_XCodes.NoCardDataXC)]
    internal class MonTgtParser_Status_ErrorEvent_0x50_G2H
        : MonTgtParser_Status_ErrorEventBase_G2H
    {
        internal override string Description
        {
            get { return "No data on mag card"; }
        }
    }

    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
       typeof(FFTgt_G2H_GMUEvent_StdData),
       (int)FaultSource.ErrorEvent,
       (int)FaultType_ErrorEvent.GMUMalfunction,
       (int)FF_AppId_GMUEvent_XCodes.GMUMalfunctionXC)]
    internal class MonTgtParser_Status_ErrorEvent_0x55_G2H
        : MonTgtParser_Status_ErrorEventBase_G2H
    {
        internal override string Description
        {
            get { return "GMU malfunction"; }
        }
    }

    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
       typeof(FFTgt_G2H_GMUEvent_StdData),
       (int)FaultSource.ErrorEvent,
       (int)FaultType_ErrorEvent.WinWithNoHandlePull,
       (int)FF_AppId_GMUEvent_XCodes.WinWithNoHandlePull)]
    internal class MonTgtParser_Status_ErrorEvent_0x57_G2H
        : MonTgtParser_Status_ErrorEventBase_G2H
    {
        internal override string Description
        {
            get { return "Win with no handle pull"; }
        }
    }

    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
       typeof(FFTgt_G2H_GMUEvent_StdData),
       (int)FaultSource.ErrorEvent,
       (int)FaultType_ErrorEvent.WinWithNoCoinIn,
       (int)FF_AppId_GMUEvent_XCodes.WinWithNoCoinIn)]
    internal class MonTgtParser_Status_ErrorEvent_0x58_G2H
        : MonTgtParser_Status_ErrorEventBase_G2H
    {
        internal override string Description
        {
            get { return "Win with no coin in"; }
        }
    }

    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
       typeof(FFTgt_G2H_GMUEvent_StdData),
       (int)FaultSource.ErrorEvent,
       (int)FaultType_ErrorEvent.HopperCantPay,
       (int)FF_AppId_GMUEvent_XCodes.Tilt59XC)]
    internal class MonTgtParser_Status_ErrorEvent_0x59_G2H
        : MonTgtParser_Status_ErrorEventBase_G2H
    {
        internal override string Description
        {
            get { return "Hopper can’t pay"; }
        }
    }

    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
       typeof(FFTgt_G2H_GMUEvent_StdData),
       (int)FaultSource.ErrorEvent,
       (int)FaultType_ErrorEvent.TooManyBillsRejected,
       (int)FF_AppId_GMUEvent_XCodes.TooManyBillsRejected)]
    internal class MonTgtParser_Status_ErrorEvent_0x86_G2H
        : MonTgtParser_Status_ErrorEventBase_G2H
    {
        internal override string Description
        {
            get { return "Too many bills rejected"; }
        }
    }

    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
       typeof(FFTgt_G2H_GMUEvent_StdData),
       (int)FaultSource.ErrorEvent,
       (int)FaultType_ErrorEvent.AcceptorMalfunction,
       (int)FF_AppId_GMUEvent_XCodes.VendRamMalXC)]
    internal class MonTgtParser_Status_ErrorEvent_0x87_G2H
        : MonTgtParser_Status_ErrorEventBase_G2H
    {
        internal override string Description
        {
            get { return "Acceptor malfunction"; }
        }
    }

    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
       typeof(FFTgt_G2H_GMUEvent_StdData),
       (int)FaultSource.ErrorEvent,
       (int)FaultType_ErrorEvent.CantReadMagCard,
       (int)FF_AppId_GMUEvent_XCodes.BadCardReadsXC)]
    internal class MonTgtParser_Status_ErrorEvent_0x88_G2H
        : MonTgtParser_Status_ErrorEventBase_G2H
    {
        internal override string Description
        {
            get { return "Can't read mag card"; }
        }
    }

    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
       typeof(FFTgt_G2H_GMUEvent_StdData),
       (int)FaultSource.ErrorEvent,
       (int)FaultType_ErrorEvent.GameMemoryMalfunction,
       (int)FF_AppId_GMUEvent_XCodes.Tilt95XC)]
    internal class MonTgtParser_Status_ErrorEvent_0x95_G2H
        : MonTgtParser_Status_ErrorEventBase_G2H
    {
        internal override string Description
        {
            get { return "Game memory malfunction"; }
        }
    }

    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
       typeof(FFTgt_G2H_GMUEvent_StdData),
       (int)FaultSource.ErrorEvent,
       (int)FaultType_ErrorEvent.GMUMetersReset,
       (int)FF_AppId_GMUEvent_XCodes.GMUMetersResetXC)]
    internal class MonTgtParser_Status_ErrorEvent_0x98_G2H
        : MonTgtParser_Status_ErrorEventBase_G2H
    {
        internal override string Description
        {
            get { return "GMU meters reset"; }
        }
    }
}
