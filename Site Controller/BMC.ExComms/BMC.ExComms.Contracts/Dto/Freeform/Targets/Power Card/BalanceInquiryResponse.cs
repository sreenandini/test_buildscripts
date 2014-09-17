using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    public class FFTgt_H2G_PowerCard_BalanceInquiryResponse 
        : FFTgt_H2G
    {
        #region Properties
        public double CashableAmount { get; set; }
        public double PromoCashableAmount { get; set; }
        public double PromoNonCashableAmount { get; set; }
        public byte DisplayMessageLength { get; set; }
        public String DisplayMessage { get; set; }
        #endregion

        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_PowerCardTypes.BalanceInquiry;
            }
        }
    }
}
