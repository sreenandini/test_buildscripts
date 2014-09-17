using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{

    #region  EFT CHAR
    #region Request
    public class FFTgt_G2H_GVA_EFT_CHAR_Request : FFTgt_B2B_GMUVarAction_Data, IFFTgt_G2H { }
    #endregion

    #region Response
    public class FFTgt_H2G_GVA_EFT_Char_Response
        : FFTgt_B2B_GMUVarAction_Data, IFFTgt_H2G
    {

        public bool IsEftEnabled { get; set; }
        public bool IsCashableDpositEnabled { get; set; }
        public bool IsNon_CashableDepositEnabled { get; set; }
        public bool IsPointsWithdrawalEnabled { get; set; }
        public bool ISCashWithdrawalEnabled { get; set; }
        public bool IsPartialTransferEnabled { get; set; }
        public bool IsAutoDepositEnabledForNonCashableOnCardOut { get; set; }
        public bool IsAutoDepositEnabledForCashableOnCardOut { get; set; }
        public bool IsOfferEnabled { get; set; }
        public bool IsCashlessSmartCardEnabled { get; set; }
        public bool IsFullDownloadEnabled { get; set; }
        public bool IsAutoTopEnabled { get; set; }
        public bool IsAutoDownloadEnabled { get; set; }


    }

    #endregion

    #region Status
    public class FFTgt_G2H_EFT_CHAR_Status
       : FFTgt_G2H_GMUVariableAction_Status, IFFTgt_G2H { }

    #endregion
    #endregion

    #region EFT Withdraw
    #region Request
    public class FFTgt_G2H_GVA_EFT_Withdraw_Request : FFTgt_B2B_GMUVarAction_Data, IFFTgt_G2H { }
    #endregion
    #region Response

    public class FFTgt_H2G_GVA_EFT_Withdraw_Response
        : FFTgt_B2B_GMUVarAction_Data, IFFTgt_H2G
    {

        public double WithdrawalAmount_option1 { get; set; }
        public double WithdrawalAmount_option2 { get; set; }
        public double WithdrawalAmount_option3 { get; set; }
        public double WithdrawalAmount_option4 { get; set; }

    }

    #endregion
    #region Status
    public class FFTgt_G2H_EFT_Withdraw_Status
   : FFTgt_G2H_GMUVariableAction_Status, IFFTgt_G2H { }
    #endregion
    #endregion

    #region MaxWithdrawParameter
    #region Request

    public class FFTgt_G2H_GVA_EFT_MaxWithdraw_Request : FFTgt_B2B_GMUVarAction_Data, IFFTgt_G2H
    {


    }

    #endregion
    #region Response

    public class FFTgt_H2G_GVA_EFT_MaxWithdraw_Response
        : FFTgt_B2B_GMUVarAction_Data, IFFTgt_H2G
    {
        public double MaxElectronicWithdrawalAmount { get; set; }
    }

    #endregion
    #endregion
}
