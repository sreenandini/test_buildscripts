using BMC.ExComms.Contracts.DTO.Freeform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Monitor
{
    #region GMU Variable Action

    /// <summary>
    /// GMU to Host GMU Event Data Information
    /// </summary>
    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
       Name = "MonTgt_B2B_GMUVarAction_Data")]
    public class MonTgt_B2B_GMUVarAction_Data 
        : MonTgt_B2B { }

    #endregion //GMU Variable Action

    #region Variable Action Status

    /// <summary>
    /// GMU To Host monitor target for Variable Action Cardless Play Timeout Status
    /// </summary>
    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
       Name = "MonTgt_G2H_GMUVarAction_Status")]
    public class MonTgt_G2H_GMUVarAction_Status
        : MonTgt_B2B_GMUVarAction_Data, IMonTgt_G2H
    {

        public MonTgt_G2H_GMUVarAction_Status() { }

        /// <summary>
        /// 0 -> Error, 1 -> Success
        /// </summary>
        [DataMember]
        public FF_AppId_ResponseStatus_Types Status { get;  set; }
    }

    #endregion //Variable Action Status
}
