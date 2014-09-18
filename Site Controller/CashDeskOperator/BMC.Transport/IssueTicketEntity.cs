using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.Transport.CashDeskOperatorEntity
{
    public class IssueTicketEntity
    {
        public long Pence { get; set; }
        public string Type { get; set; }
        public double dblValue { get; set; }
        public long lnglValue { get; set; }
        public DateTime Date { get; set; }
        public long TicketID { get; set; }
        public string FullTicketNumber { get; set; }
        public string BarCode { get; set; }
        public int NumberOfDays { get; set; }
        public string TicketHeader { get; set; }
        public string VoidDate { get; set; }
    }
}
