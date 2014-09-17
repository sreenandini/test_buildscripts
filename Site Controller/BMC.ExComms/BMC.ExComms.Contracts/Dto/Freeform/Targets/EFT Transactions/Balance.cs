using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    #region Balance Request

    /// <summary>
    /// GMU to Host Freeform for Balnace Request
    /// </summary>
    public class FFTgt_G2H_EFT_BalanceRequest
        : FFTgt_G2H_EFT_PlayerAndPinDetails
    {
        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_EFT_TransactionTypes.BalanceRequest;
            }
        }
    }

    #endregion //Balance Request

    #region Balance Response

    /// <summary>
    /// GMU to Host Freeform for Balnace Response
    /// </summary>
    public class FFTgt_H2G_EFT_BalanceResponse
        : FFTgt_B2B_EFT_PlayerAndMessageInfo, IFFTgt_H2G
    {
        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_EFT_TransactionTypes.BalanceResponse;
            }
        }
    }

    #endregion //Balance Response
}
