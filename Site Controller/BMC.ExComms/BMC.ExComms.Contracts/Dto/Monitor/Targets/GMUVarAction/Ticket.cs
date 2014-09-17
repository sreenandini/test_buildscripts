using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Monitor
{
    #region Ticket Number

    #region Ticket Number Request

    /// <summary>
    /// GMU To Host Freeform for Ticket Number Request
    /// </summary>
    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
          Name = "MonTgt_G2H_GVA_TN_Request")]
    [ExCommsMessageKnownType]
    public class MonTgt_G2H_GVA_TN_Request
        : MonTgt_B2B_GMUVarAction, IMonTgt_G2H { }

    #endregion //Ticket Number Request

    #region Ticket Number Response

    /// <summary>
    /// Host To GMU Freeform for Ticket Number Response
    /// </summary>
    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
          Name = "MonTgt_H2G_GVA_TN_Response")]
    [ExCommsMessageKnownType]
    public class MonTgt_H2G_GVA_TN_Response
        : MonTgt_B2B_GMUVarAction, IMonTgt_H2G
    {
        public int TicketNumber { get; set; }
    }

    #endregion //Ticket Number Response

    #region Ticket Number Status

    /// <summary>
    /// GMU To Host Freeform for Ticket Number Status
    /// </summary>
    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
          Name = "MonTgt_G2H_GVA_TN_Status")]
    [ExCommsMessageKnownType]
    public class MonTgt_G2H_GVA_TN_Status
        : MonTgt_B2B_GMUVarAction_Status, IMonTgt_G2H { }

    #endregion //Ticket Number Status

    #endregion //Ticket Number

    #region Ticket Expiration Date

    #region Ticket Expiration Date request

    /// <summary>
    /// GMU to Host Monitor target for Ticket Expiration Date request
    /// </summary>
    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
          Name = "MonTgt_H2G_GVA_TED_Request")]
    [ExCommsMessageKnownType]
    public class MonTgt_G2H_GVA_TED_Request
        : MonTgt_B2B_GMUVarAction, IMonTgt_G2H
    {
        public MonTgt_G2H_GVA_TED_Request() { }
    }

    #endregion //Ticket Expiration Date Request

    #region Ticket Expiration Date

    /// <summary>
    /// GMU to Host monitor target for Ticket Expiration Date
    /// </summary>
    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
          Name = "MonTgt_H2G_GVA_TED_Response")]
    [ExCommsMessageKnownType]
    public class MonTgt_H2G_GVA_TED_Response
        : MonTgt_B2B_GMUVarAction, IMonTgt_H2G
    {
        public MonTgt_H2G_GVA_TED_Response() { }

        [DataMember]
        public DateTime Date { get; set; }

        [DataMember]
        public short ExipreDays { get; set; }
    }

    #endregion //Ticket Expiration Date

    #region Ticket Expiration Date Status

    /// <summary>
    /// GMU To Host monitor target for Ticket Print Date Status
    /// </summary>
    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
          Name = "MonTgt_G2H_GVA_TED_Status")]
    [ExCommsMessageKnownType]
    public class MonTgt_G2H_GVA_TED_Status
        : MonTgt_B2B_GMUVarAction_Status, IMonTgt_G2H
    {
        public MonTgt_G2H_GVA_TED_Status() { }
    }

    #endregion //Ticket Expiration Date Status

    #endregion //Ticket Expiration Date

    #region Ticket Key

    #region Ticket Key Request

    /// <summary>
    /// GMU To Host Freeform for Ticket Key Request
    /// </summary>
    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
          Name = "MonTgt_G2H_GVA_TK_Request")]
    [ExCommsMessageKnownType]
    public class MonTgt_G2H_GVA_TK_Request
        : MonTgt_B2B_GMUVarAction, IMonTgt_G2H { }

    #endregion //Ticket Key Request

    #region Ticket Key Response

    /// <summary>
    /// Host To GMU Freeform for Ticket Key Response
    /// </summary>
    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
          Name = "MonTgt_H2G_GVA_TK_Response")]
    [ExCommsMessageKnownType]
    public class MonTgt_H2G_GVA_TK_Response
        : MonTgt_B2B_GMUVarAction, IMonTgt_H2G
    {
        public string BarcodeKey { get; set; }
    }

    #endregion //Ticket Key Response

    #region Ticket Key Status

    /// <summary>
    /// GMU To Host Freeform for Ticket Key Status
    /// </summary>
    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
          Name = "MonTgt_G2H_GVA_TK_Status")]
    [ExCommsMessageKnownType]
    public class MonTgt_G2H_GVA_TK_Status
        : MonTgt_B2B_GMUVarAction_Status, IMonTgt_G2H { }

    #endregion //Ticket Key Status

    #endregion //Ticket Key
}
