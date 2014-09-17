using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using BMC.ExComms.Contracts.DTO.Freeform;

namespace BMC.ExComms.Contracts.DTO.Monitor
{
    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
       Name = "MonTgt_H2G_EFT_SystemEnable")]
    [ExCommsMessageKnownType]
    public class MonTgt_H2G_EFT_SystemEnable
        : MonTgt_B2B_EFT_Transactions, IMonTgt_H2G
    {
        public MonTgt_H2G_EFT_SystemEnable()
        {
        }
    }

    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
       Name = "MonTgt_G2H_EFT_SystemEnable")]
    [ExCommsMessageKnownType]
    public class MonTgt_G2H_EFT_SystemEnable
        : MonTgt_B2B_EFT_Transactions, IMonTgt_G2H
    {
        public MonTgt_G2H_EFT_SystemEnable()
        {
        }

        [DataMember]
        public FF_AppId_EFT_ResponseTypes ResponseType { get; set; }
    }

    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
       Name = "MonTgt_H2G_EFT_SystemDisable")]
    [ExCommsMessageKnownType]
    public class MonTgt_H2G_EFT_SystemDisable
        : MonTgt_B2B_EFT_Transactions, IMonTgt_H2G
    {
        public MonTgt_H2G_EFT_SystemDisable()
        {
        }
    }

    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
       Name = "MonTgt_G2H_EFT_SystemDisable")]
    [ExCommsMessageKnownType]
    public class MonTgt_G2H_EFT_SystemDisable
        : MonTgt_B2B_EFT_Transactions, IMonTgt_G2H
    {
        public MonTgt_G2H_EFT_SystemDisable()
        {
        }

        [DataMember]
        public FF_AppId_EFT_ResponseTypes ResponseType { get; set; }
    }
}
