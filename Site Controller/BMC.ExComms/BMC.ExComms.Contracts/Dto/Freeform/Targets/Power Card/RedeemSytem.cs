using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    public class FFTgt_G2H_PowerCard_RedeemStoredValue
        : FFTgt_G2H
    {
        #region Properties
        public double CashableAmount { get; set; }
        public double PromoCashableAmount { get; set; }
        public double PromoNonCashableAmount { get; set; }
        #endregion

        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_PowerCardTypes.RedeemStoredValue;
            }
        }
    }

    public class FFTgt_H2G_PowerCard_RedeemResponse
        : FFTgt_H2G
    {
        #region Properties
        public FF_AppId_PowerCard_RedeemErrorCode Status { get; set; }
        public String DisplayMessage { get; set; }
        #endregion

        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_PowerCardTypes.RedeemResponse;
            }
        }
    }

    public class FFTgt_G2H_PowerCard_RedeemConfirmed
        : FFTgt_G2H
    {
        #region Properties
        public FF_AppId_PowerCard_RedeemErrorCode Status { get; set; }
        public double CashableAmount { get; set; }
        public double PromoCashableAmount { get; set; }
        public double PromoNonCashableAmount { get; set; }
        #endregion

        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_PowerCardTypes.RedeemConfirmed;
            }
        }
    }
}
