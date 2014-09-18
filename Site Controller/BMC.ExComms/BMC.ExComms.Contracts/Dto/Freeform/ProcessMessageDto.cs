using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using BMC.CoreLib;
using BMC.ExComms.Contracts.DTO;
using BMC.ExComms.Contracts.DTO.Monitor;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Freeform",
        Name = "ExCommsServerCallbackTypes")]
    [ExCommsMessageKnownType]
    public enum ExCommsServerCallbackTypes
    {
        [EnumMember]
        RawMessage = 0,
        [EnumMember]
        ExecutionStepSubscribed = 1,
        [EnumMember]
        ExecutionStepChanged = 2,
    }

    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Freeform",
        Name = "ExCommsRawMessageEntity")]
    [ExCommsMessageKnownType]
    public class ExCommsRawMessageEntity : SubscribeRequestEntity
    {
        public ExCommsRawMessageEntity() { }
    }

    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Freeform",
        Name = "ExCommsExecutionStepEntity")]
    [ExCommsMessageKnownType]
    public class ExCommsExecutionStepEntity : SubscribeRequestEntity
    {
        public ExCommsExecutionStepEntity()
        {
            this.Steps = new List<string>();
        }

        [DataMember]
        public string GmuIpAddress { get; set; }

        [DataMember]
        public List<string> Steps { get; set; }
    }
}
