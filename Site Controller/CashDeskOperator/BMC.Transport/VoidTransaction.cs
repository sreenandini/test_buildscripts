using System;

namespace BMC.Transport.CashDeskOperatorEntity
{
    public class VoidTranCreate
    {
        public string TreasuryID { get; set; }
        public DateTime Date { get; set; }
        public string UserNo { get; set; }
    }
}
