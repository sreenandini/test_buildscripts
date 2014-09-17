using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Monitor
{
    #region EFT Char Request
    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
          Name = "MonTgt_G2H_GVA_EFTCHAR_Request")]
    [ExCommsMessageKnownType]
    public class MonTgt_G2H_GVA_EFTCHAR_Request
        : MonTgt_B2B_GMUVarAction, IMonTgt_G2H { }
    #endregion

    #region EFT CHAR Response
    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
          Name = "MonTgt_H2G_GVA_EFTCHAR_Response")]
    [ExCommsMessageKnownType]
    public class MonTgt_H2G_GVA_EFTCHAR_Response
        : MonTgt_B2B_GMUVarAction, IMonTgt_H2G
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

    #endregion //Ticket Number Response
    #region EFT Char Status
    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
          Name = "MonTgt_G2H_GVA_EFTChar_Status")]
    [ExCommsMessageKnownType]
    public class MonTgt_G2H_GVA_EFTChar_Status
        : MonTgt_B2B_GMUVarAction_Status, IMonTgt_G2H { }

    #endregion 
}
