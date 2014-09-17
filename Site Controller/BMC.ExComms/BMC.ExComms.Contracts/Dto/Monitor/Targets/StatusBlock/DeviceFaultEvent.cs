using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Monitor
{
    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
        Name = "MonTgt_G2H_Status_DeviceFaultEvent")]
    [ExCommsMessageKnownType]
    public class MonTgt_G2H_Status_DeviceFaultEvent
        : MonTgt_G2H
    {
        public MonTgt_G2H_Status_DeviceFaultEvent()
        {
        }

        public string Description { get; set; }
    }
}
