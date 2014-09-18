using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.Transport.CashDeskOperatorEntity
{
    public class HandPays
    {
        public string BarPositionName { get; set; }
        public string MachineClassName { get; set; }
        public string InstallationReference { get; set; }
        public long TreasuryEntryID { get; set; }
        public string TreasuryEntryDate { get; set; }
        public float TreasuryEntryValue { get; set; }
        public string TreasuryEntryHandpayType { get; set; }
        public long InstallationNumber { get; set; }

    }
}
