using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.Transport.CashDeskOperatorEntity
{
    public class PrintReceipt
    {
        public int InstallationNumber { get; set; }
        public string TreasuryType { get; set; }
        public float TreasuryAmount { get; set; }
        public int TreasuryID { get; set; }
        public string TreasuryDate { get; set; }
    }
}
