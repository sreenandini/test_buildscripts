using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using BMC.CoreLib;

namespace BMC.ExComms.Contracts.DTO
{
    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO",
        Name = "SubscriptionRequestEntity")]
    [ExCommsMessageKnownType]
    public class SubscriptionRequestEntity : DisposableObject
    {
        public SubscriptionRequestEntity() { }

        [DataMember(Order = 0)]
        public string IPAddress { get; set; }
    }

    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO",
        Name = "SubscribeRequestEntity")]
    [ExCommsMessageKnownType]
    public class SubscribeRequestEntity : SubscriptionRequestEntity
    {
        public SubscribeRequestEntity() { }
    }

    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO",
        Name = "UnsubscribeRequestEntity")]
    [ExCommsMessageKnownType]
    public class UnsubscribeRequestEntity : SubscriptionRequestEntity
    {
        public UnsubscribeRequestEntity() { }
    }
}
