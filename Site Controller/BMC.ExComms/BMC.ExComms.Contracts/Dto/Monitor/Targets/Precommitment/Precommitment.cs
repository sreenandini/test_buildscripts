using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Monitor
{
    #region Precommitment

    /// <summary>
    /// GMU to Host (Or) Host to GMU Precommitment monitor target
    /// </summary>
    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
        Name = "MonTgt_B2B_Precommitment")]
    [ExCommsMessageKnownType]
    public class MonTgt_B2B_Precommitment
        : MonTgt_B2B, IMonTgt_B2B
    {

        #region Constructor
        
        public MonTgt_B2B_Precommitment()
        {
        }

        #endregion Constructor

        #region Properties

        /// <summary>
        /// Precommitment sub target data
        /// </summary>
        [DataMember]
        public MonTgt_B2B_Precommitment_Data PrecommitmentData { get; set; }

        #endregion //Properties
    }

    /// <summary>
    /// GMU to Host (Or) Host to GMU Precommitment monitor sub target data
    /// </summary>
    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
        Name = "MonTgt_B2B_Precommitment_Data")]
    [ExCommsMessageKnownType]
    public class MonTgt_B2B_Precommitment_Data
        : MonTgt_B2B, IMonTgt_B2B
    {

        #region Constructor

        public MonTgt_B2B_Precommitment_Data()
        {
        }

        #endregion Constructor
    }
    
    #endregion Precommitment

    #region Player Account Details

    /// <summary>
    /// GMU to Host (Or) Host to GMU Precommitment player Account details
    /// </summary>
    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
        Name = "MonTgt_B2B_PC_Player_AccDetails")]
    [ExCommsMessageKnownType]
    public class MonTgt_B2B_PC_Player_AccDetails
        : MonTgt_B2B_Precommitment_Data, IMonTgt_B2B
    {
        #region Constructor
        public MonTgt_B2B_PC_Player_AccDetails()
        {
        }
        #endregion Constructor

        #region Properties

        /// <summary>
        /// Player Account Number Length
        /// </summary>
        [DataMember]
        public int PlayerAccNoLen { get; set; }

        /// <summary>
        /// Player Account Number
        /// </summary>
        [DataMember]
        public string PlayerAccNo { get; set; }

        #endregion //Properties
    }

    #endregion //Player Account Details

}
