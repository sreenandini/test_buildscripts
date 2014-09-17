using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Monitor
{
    #region EFT Withdraw Request
    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
          Name = "MonTgt_G2H_GVA_EFTWithdraw_Request")]
    [ExCommsMessageKnownType]
    public class MonTgt_G2H_GVA_EFTWithdraw_Request
        : MonTgt_B2B_GMUVarAction, IMonTgt_G2H { }
    #endregion
    #region EFT Withdraw Response

    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
        Name = "MonTgt_H2G_GVA_EFTWithdraw_Response")]
    [ExCommsMessageKnownType]
    public class MonTgt_H2G_GVA_EFTWithdraw_Response
        : MonTgt_B2B_GMUVarAction, IMonTgt_H2G
    {
        public double WithdrawalAmount_option1 { get; set; }
        public double WithdrawalAmount_option2 { get; set; }
        public double WithdrawalAmount_option3 { get; set; }
        public double WithdrawalAmount_option4 { get; set; }
    }
#endregion
#region  EFT withdraw Status

    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
          Name = "MonTgt_G2H_GVA_EFTWitjhdraw_Status")]
    [ExCommsMessageKnownType]
    public class MonTgt_G2H_GVA_EFTWitjhdraw_Status
        : MonTgt_B2B_GMUVarAction_Status, IMonTgt_G2H { }

#endregion
}
