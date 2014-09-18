using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.Transport.CashDeskOperatorEntity
{
    public class TicketException
    {
        public int InstallationNumber { get; set; }
        public string ExceptionDetails { get; set; }
        public int ExceptionType { get; set; }
        public string Reference { get; set; }
        public int User { get; set; }        
    }
}
