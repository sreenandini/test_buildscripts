using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Monitor
{

    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
        Name = "MonTgt_B2B_GameCappingData")]
    [ExCommsMessageKnownType]
    public class MonTgt_B2B_GameCappingData : MonTgt_B2B { }

    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
        Name = "MonTgt_H2G_GameCapping")]
    [ExCommsMessageKnownType]
    public class MonTgt_H2G_GameCapping
       : MonTgt_B2B_GameCappingData
    {
        #region Constructor
        public MonTgt_H2G_GameCapping()
        {
        }
        #endregion Constructor

        #region Properties

        [DataMember]
        public byte DisplayMessageLength { get; set; }

        [DataMember]
        public string DisplayMessage { get; set; }

        [DataMember]
        public bool IsEnabled { get; set; }

        [DataMember]
        public bool IsMaxCappingExceeded { get; set; }

        [DataMember]
        public bool IsSelfCapping { get; set; }

        [DataMember]
        public bool IsAllowed { get; set; }

        [DataMember]
        public byte Option1 { get; set; }
        
        [DataMember]
        public byte Option2 { get; set; }
        
        [DataMember]
        public byte Option3 { get; set; }

        [DataMember]
        public byte Option4 { get; set; }

        [DataMember]
        public byte Option5 { get; set; }

        [DataMember]
        public bool AutoRelease { get; set; }

        [DataMember]
        public bool ValidPin { get; set; }

        [DataMember]
        public byte MinutesToExpire { get; set; }
        #endregion Properties
    }

    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
        Name = "MonTgt_G2H_GameCapping_Info")]
    [ExCommsMessageKnownType]
    public abstract class MonTgt_G2H_GameCapping_Info
       : MonTgt_G2H
    {
        #region Constructor
        public MonTgt_G2H_GameCapping_Info()
        {
        }
        #endregion Constructor

        #region Properties
        [DataMember]
        public string PlayerCardNumber { get; set; }
        #endregion Properties
    }

    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
        Name = "MonTgt_G2H_GameCapping_StartEnd")]
    [ExCommsMessageKnownType]
    public class MonTgt_G2H_GameCapping_StartEnd
       : MonTgt_G2H_GameCapping_Info
    {
        #region Constructor
        public MonTgt_G2H_GameCapping_StartEnd()
        {
        }
        #endregion Constructor

        #region Properties
        [DataMember]
        public bool IsCapStart { get; set; }

        [DataMember]
        public string EmployeeCardNumber { get; set; }

        [DataMember]
        public string PlayerPIN { get; set; }
        #endregion Properties
    }

    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
        Name = "MonTgt_G2H_GameCapping_TimeLeft")]
    [ExCommsMessageKnownType]
    public class MonTgt_G2H_GameCapping_TimeLeft
       : MonTgt_G2H_GameCapping_Info
    {
        #region Constructor
        public MonTgt_G2H_GameCapping_TimeLeft()
        {
        }
        #endregion Constructor

        #region Properties
        [DataMember]
        public byte NumberOfMinutesToExpire { get; set; }
        #endregion Properties
    }
}
