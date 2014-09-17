using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    public class FFTgt_G2H_PowerCard_BalanceInquiry
        : FFTgt_G2H
    {
        #region Properties

        public String RawCard { get; set; }
        public String CasinoId { get; set; }
        public String AlternateId { get; set; }
        public String PlayerCardNumber { get; set; }
        public string PIN { get; set; }

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
