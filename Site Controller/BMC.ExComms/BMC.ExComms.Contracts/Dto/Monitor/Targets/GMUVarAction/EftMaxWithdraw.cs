using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Monitor
{
    #region EFT Max Withdraw Request
    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
          Name = "MonTgt_G2H_GVA_EFTMaxWithdraw_Request")]
    [ExCommsMessageKnownType]
    public class MonTgt_G2H_GVA_EFTMaxWithdraw_Request
        : MonTgt_B2B_GMUVarAction, IMonTgt_G2H { }
    #endregion
   
    #region EFT Withdraw Response

    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
        Name = "MonTgt_H2G_GVA_EFTMAxWithdraw_Response")]
    [ExCommsMessageKnownType]
    public class MonTgt_H2G_GVA_EFTMAxWithdraw_Response
        : MonTgt_B2B_GMUVarAction, IMonTgt_H2G
    {
        public double MaxWithdrawalAmount { get; set; }

    }
    #endregion
   
}
