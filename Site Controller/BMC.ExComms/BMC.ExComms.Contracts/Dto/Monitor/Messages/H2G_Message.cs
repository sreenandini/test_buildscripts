using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Monitor
{
    public interface IMonMsg_H2G
           : IMonitorEntity_Msg { }
    
    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
        Name = "MonMsg_H2G")]
    [ExCommsMessageKnownType]
    public class MonMsg_H2G
        : MonitorEntity_Msg, IMonMsg_H2G
    {
        public MonMsg_H2G() { }

        public MonMsg_H2G(FaultSource faultSource, int faultType)
        {
            this.FaultSource = (int) faultSource;
            this.FaultType = faultType;
        }
    }
}
