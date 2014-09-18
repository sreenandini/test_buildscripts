using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.Transport
{
    public class TreasruryItems
    {
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
