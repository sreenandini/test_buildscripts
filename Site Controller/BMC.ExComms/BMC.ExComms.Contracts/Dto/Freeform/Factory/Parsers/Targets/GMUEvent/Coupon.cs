using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    internal class FFParser_Tgt_MC300_GMUEvent_Coupon : FFTgtParser
    {
        internal FFParser_Tgt_MC300_GMUEvent_Coupon()
            : base() { }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            FFTgt_G2H_GMUEvent_CouponData tgt = new FFTgt_G2H_GMUEvent_CouponData();
            tgt.CashlessTransactionType = buffer[0];
            tgt.CreditMeterLimit = FreeformHelper.GetBCDValue<short>(buffer, 1, 2);
            tgt.CreditMeterBalance = FreeformHelper.GetBCDValue<short>(buffer, 3, 2);
            tgt.CouponSerialNumber = FreeformHelper.GetBCDValueString(buffer, 0, 5, 8);
            tgt.GameDoninationCode = FreeformHelper.GetBytesToNumberUInt8(buffer, 13, 1); ;            
            return tgt;
        }
    }


}
