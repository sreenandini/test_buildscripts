using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.Transport
{
    public class TicketExceptions
    {

        public string SEGM
        { get; set; }

        public string Machine
        { get; set; }

        public double CurrentCashDesk
        { get; set; }
        public int CashDeskClaimedQty
        { get; set; }
        public int CashDeskPrintedQty
        { get; set; }

        public string Reference
        { get; set; }

        public string Details
        { get; set; }
        public double currEGM
        { get; set; }

        public int MachineClaimedQty
        { get; set; }

        public int MachinePrintedQty
        { get; set; }

        public float currValue
        { get; set; }
        public string Type
        { get; set; }

        public string Position
        { get; set; }

        public string TransactionType
        { get; set; }

        public string Zone
        { get; set; }
        public string PrintDate
        { get; set; }
        public string PayDate
        { get; set; }

        public string ClaimedTerminal
        { get; set; }

        public string Ticket
        { get; set; }

        public double Value
        { get; set; }

        public string Asset
        { get; set; }

        public string PayDevice
        { get; set; }

        public int COLINSTALLID
        { get; set; }

        public string CreateCompleted
        { get; set; }
        public string Status
        { get; set; }
        public float cTicketTotal
        { get; set; }
        public float cExceptionsTotal
        { get; set; }
        public bool bExceptionRecordFound
        { get; set; }

        public string VoucherStatus
        { get; set; }
        public bool TicketAddedtoList
        { get; set; }

        public string Amount
        { get; set; }

        public string TreasuryDate { get; set; }
        public string TreasuryDateTime { get; set; }
        public string TreasuryTime { get; set; }
        public string TreasuryType { get; set; }
        public double TreasuryAmount { get; set; }
        public string ZoneName { get; set; }
        public string MachineName { get; set; }
        public string PositionName { get; set; }
        public int HandpayQty { get; set; }
        public int JackPotQty { get; set; }
        public int ProgQty { get; set; }
        public int RefundQty { get; set; }
        public int RefillQty { get; set; }
        public int ShortQty { get; set; }
        public int FloatQty { get; set; }
    }
}
