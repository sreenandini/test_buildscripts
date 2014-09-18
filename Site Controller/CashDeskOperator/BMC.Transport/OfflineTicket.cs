using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.Transport.CashDeskOperatorEntity
{
    public class OfflineTicket
    {
        public int InstallationNumber { get; set; }
        public float PayableValue { get; set; }
        public int UserID { get; set; }
        public string TicketBarCode { get; set; }
        public string CustomerDetails { get; set; }
        public DateTime RedeemedDateTime { get; set; }
        
        public string MachineName
        {
            get
            {
                return Environment.MachineName;
            }
        }
        public string UserName
        {
            get
            {
                return Environment.UserName;
            }
        }


    }
}
