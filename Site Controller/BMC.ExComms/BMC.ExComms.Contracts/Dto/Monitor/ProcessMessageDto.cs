using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using BMC.CoreLib;
using BMC.ExComms.Contracts.DTO;
using BMC.ExComms.Contracts.DTO.Monitor;

namespace BMC.ExComms.Contracts.DTO.Monitor
{
    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
        Name = "ExMonServer4CommsServerCallbackTypes")]
    [ExCommsMessageKnownType]
    public enum ExMonServer4CommsServerCallbackTypes
    {
        [EnumMember]
        ProcessH2GMessage = 0,
    }

    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
        Name = "ExMonServer4MonProcessorCallbackTypes")]
    [ExCommsMessageKnownType]
    public enum ExMonServer4MonProcessorCallbackTypes
    {
        [EnumMember]
        ProcessG2HMessage = 0,
    }

    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
        Name = "ExMonServer4MonClientCallbackTypes")]
    [ExCommsMessageKnownType]
    public enum ExMonServer4MonClientCallbackTypes
    {
        [EnumMember]
        ProcessG2HMessage = 0,
    }

    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
        Name = "ProcessG2HMessageEntity")]
    [ExCommsMessageKnownType]
    public class ProcessG2HMessageEntity : SubscribeRequestEntity
    {
        public ProcessG2HMessageEntity() { }
    }

    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
        Name = "ProcessH2GMessageEntity")]
    [ExCommsMessageKnownType]
    public class ProcessH2GMessageEntity : SubscribeRequestEntity
    {
        public ProcessH2GMessageEntity() { }
    }

    public class ExCommsServerSubscribeEntityBase : DisposableObject
    {
        public SubscriptionRequestEntity Entity { get; set; }
    }

    public class ExMonServer4CommsServerSubscribeEntity : ExCommsServerSubscribeEntityBase
    {
        public ExMonServer4CommsServerCallbackTypes CallbackType { get; set; }
    }

    public class ExMonServer4MonProcessorSubscribeEntity : ExCommsServerSubscribeEntityBase
    {
        public ExMonServer4MonProcessorCallbackTypes CallbackType { get; set; }
    }
}
