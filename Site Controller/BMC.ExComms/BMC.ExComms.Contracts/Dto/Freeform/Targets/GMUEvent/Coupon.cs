using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    public class FFTgt_G2H_GMUEvent_CouponData
        : FFTgt_B2B_GMUEventData_Primary
    {
        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_GMUEvent_DataSetIds.Coupon;
            }
        }

        public byte CashlessTransactionType  { get; set; }
        public double CreditMeterLimit { get; set; }
        public double CreditMeterBalance { get; set; }
        public string CouponSerialNumber { get; set; }
        public byte GameDoninationCode { get; set; }
    }
}
