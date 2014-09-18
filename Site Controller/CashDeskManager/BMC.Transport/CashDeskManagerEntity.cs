using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.Transport
{
    public class CashDeskManagerEntity
    {

    }

    public class TicketsClaimed
    {
     
        public string TicketsClaimedFrom
        { get; set; }
        public string TicketsClaimedTo
        { get; set; }
    }

    public class Tickets
    {

        public string StartDate
        { get; set; }
        public string EndDate
        { get; set; }

        public string Type
        { get; set; }

        public bool IsLiability
        { get; set; }

        public bool IsClaimedInCashDesk
        { get; set; }

        public string BarCode
        { get; set; }
        public bool IsClaimedInMachine
        { get; set; }

        public bool IsPrintedInMachine
        { get; set; }
        public bool IsPrintedInCashDesk
        { get; set; }


    }
}
