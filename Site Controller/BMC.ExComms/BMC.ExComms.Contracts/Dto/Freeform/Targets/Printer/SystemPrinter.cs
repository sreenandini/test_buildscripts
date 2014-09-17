using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    public class FFTgt_B2B_SystemPrinter
        : FFTgt_B2B, IFFTgt_B2B
    {
        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_TargetIds.Printer;
            }
        }

        public FFTgt_B2B_SystemPrinter_Data SystemPrinterData { get; set; }
    }

    public class FFTgt_B2B_SystemPrinter_Data
        : FFTgt_B2B { }

    public class FFTgt_H2G_SystemPrinter_PrintString
        : FFTgt_B2B_SystemPrinter_Data, IFFTgt_H2G
    {
        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_SystemPrinter.PrintString;
            }
        }

        #region Properties
        public string TextToPrint { get; set; }
        #endregion
    }

    public class FFTgt_H2G_SystemPrinter_PrintStringEnd
       : FFTgt_B2B_SystemPrinter_Data, IFFTgt_H2G
    {
        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_SystemPrinter.PrintStringEnd;
            }
        }
        #region Properties
        public FF_AppId_SystemPrinter_PrinterResponse Response { get; set; }
        #endregion
    }

    public class FFTgt_H2G_SystemPrinter_SetPrintCompValue
       : FFTgt_B2B_SystemPrinter_Data, IFFTgt_H2G
    {
        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_SystemPrinter.SetPrintCompValue;
            }
        }
        #region Properties
        public byte[] Values { get; set; }
        #endregion
    }

    public class FFTgt_H2G_SystemPrinter_CompVoucherRequest
       : FFTgt_B2B_SystemPrinter_Data, IFFTgt_H2G
    {
        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_SystemPrinter.ComVoucherRequest;
            }
        }

        #region Properties
        public string PlayerId { get; set; }
        public string PinNumber { get; set; }
        public double VoucherAmount { get; set; }
        #endregion
    }

    public class FFTgt_H2G_SystemPrinter_PrintJobCancel
       : FFTgt_B2B_SystemPrinter_Data, IFFTgt_H2G
    {
        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_SystemPrinter.PrintJobCancel;
            }
        }
    }

    public class FFTgt_G2H_SystemPrinter_PrintStatus
       : FFTgt_B2B_SystemPrinter_Data, IFFTgt_G2H
    {
        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_SystemPrinter.PrinterStatus;
            }
        }

        public FF_AppId_SystemPrinter_PrinterStatus Status { get; set; }
    }
}
