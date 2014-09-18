using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.Transport.CashDeskOperatorEntity
{
    public class VoidTreasuryNegativeTransaction
    {
        public int TreasuryNumber { get; set; }
        public string TreasuryDate { get; set; }
        public string TreasuryTime { get; set; }
        public int UserID { get; set; }
    }
}
