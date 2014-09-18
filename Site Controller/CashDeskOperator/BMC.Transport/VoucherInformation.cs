using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.Transport
{
    public class VoucherInformation
    {
        public int SiteCode { get; set; }
        public string BarCode { get; set; }
        public string TicketType { get; set; }
        public string PrintDeviceName { get; set; }
        public string PayDeviceName { get; set; }
        public string VoidDeviceName { get; set; }
        public string ErrorDeviceName { get; set; }
        public string PrintedDate { get; set; }
        public string ExpiryDate { get; set; }
       public string EffectiveDate { get; set; }

        public string RedeemedDate { get; set; }
        public string VoidedDate { get; set; }
        public Decimal TicketAmount { get; set; }
        public string VoucherStatus { get; set; }
        public string ErrorDescription { get; set; }
        public string VoucherIssuedUser { get; set; }
        public string VoucherRedeemedUser { get; set; }
        public string VoucherVoidUser { get; set; }
        public int RedeemSiteCode { get; set; }
    }
}
