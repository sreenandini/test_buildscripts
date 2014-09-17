using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.ExComms.Contracts.DTO.Freeform;

namespace BMC.ExComms.Contracts.DTO.Monitor
{
    [ErrorEventCategory("Printer")]
    internal abstract class MonTgtParser_Status_PrinterEvent_G2H
        : MonTgtParser_Status_DescriptionBase_G2H
    {
        protected override IMonitorEntity_MsgTgt CreateMonitorStatusTarget(FFTgt_G2H_GMUEvent_StdData tgtSrc)
        {
            return new MonTgt_G2H_Status_PrinterEvent()
            {
                Description = this.Description,
            };
        }
    }

    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
       typeof(FFTgt_G2H_GMUEvent_StdData),
       (int)FaultSource.PrinterEvent,
       (int)FaultType_PrinterEvent.PrinterCommunicationError,
       (int)FF_AppId_GMUEvent_XCodes.PrinterComErrorXC)]
    internal class MonTgtParser_Status_PrinterEvent_0x1A_G2H
        : MonTgtParser_Status_PrinterEvent_G2H
    {
        internal override string Description
        {
            get { return "Printer communication error"; }
        }
    }

    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
       typeof(FFTgt_G2H_GMUEvent_StdData),
       (int)FaultSource.PrinterEvent,
       (int)FaultType_PrinterEvent.PrinterPaperEmpty,
       (int)FF_AppId_GMUEvent_XCodes.PrinterPaperOutXC)]
    internal class MonTgtParser_Status_PrinterEvent_0x1B_G2H
        : MonTgtParser_Status_PrinterEvent_G2H
    {
        internal override string Description
        {
            get { return "Printer paper empty"; }
        }
    }

    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
       typeof(FFTgt_G2H_GMUEvent_StdData),
       (int)FaultSource.PrinterEvent,
       (int)FaultType_PrinterEvent.PrinterPaperLow,
       (int)FF_AppId_GMUEvent_XCodes.PrinterPaperLowXC)]
    internal class MonTgtParser_Status_PrinterEvent_0x1C_G2H
        : MonTgtParser_Status_PrinterEvent_G2H
    {
        internal override string Description
        {
            get { return "Printer paper low"; }
        }
    }

    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
       typeof(FFTgt_G2H_GMUEvent_StdData),
       (int)FaultSource.PrinterEvent,
       (int)FaultType_PrinterEvent.PrinterPowerOff,
       (int)FF_AppId_GMUEvent_XCodes.PrinterPowerOffXC)]
    internal class MonTgtParser_Status_PrinterEvent_0x1D_G2H
        : MonTgtParser_Status_PrinterEvent_G2H
    {
        internal override string Description
        {
            get { return "Printer power off"; }
        }
    }

    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
       typeof(FFTgt_G2H_GMUEvent_StdData),
       (int)FaultSource.PrinterEvent,
       (int)FaultType_PrinterEvent.PrinterPowerOn,
       (int)FF_AppId_GMUEvent_XCodes.PrinterPowerOnXC)]
    internal class MonTgtParser_Status_PrinterEvent_0x1E_G2H
        : MonTgtParser_Status_PrinterEvent_G2H
    {
        internal override string Description
        {
            get { return "Printer power on"; }
        }
    }

    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
       typeof(FFTgt_G2H_GMUEvent_StdData),
       (int)FaultSource.PrinterEvent,
       (int)FaultType_PrinterEvent.ReplacePrinterRibbon,
       (int)FF_AppId_GMUEvent_XCodes.PrinterRibbonXC)]
    internal class MonTgtParser_Status_PrinterEvent_0x1F_G2H
        : MonTgtParser_Status_PrinterEvent_G2H
    {
        internal override string Description
        {
            get { return "Replace printer ribbon"; }
        }
    }

    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
       typeof(FFTgt_G2H_GMUEvent_StdData),
       (int)FaultSource.PrinterEvent,
       (int)FaultType_PrinterEvent.PrinterCarriageJammed,
       (int)FF_AppId_GMUEvent_XCodes.PrinterJamXC)]
    internal class MonTgtParser_Status_PrinterEvent_0x27_G2H
        : MonTgtParser_Status_PrinterEvent_G2H
    {
        internal override string Description
        {
            get { return "Printer carriage jammed"; }
        }
    }
}
