using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Monitor
{
    public interface IMonitorEntity_MsgTgt : IMonitorEntity { }

    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
        Name = "MonitorEntity_MsgTgt")]
    [ExCommsMessageKnownType]
    public class MonitorEntity_MsgTgt
        : MonitorEntity, IMonitorEntity_MsgTgt
    {
        public MonitorEntity_MsgTgt()
        {
            this.FillTargetInfo();
        }

        public override string UniqueKey
        {
            get { return string.Empty; }
        }
    }

    public interface IMonTgt_G2H : IMonitorEntity_MsgTgt { }

    public interface IMonTgt_H2G : IMonitorEntity_MsgTgt { }

    public interface IMonTgt_B2B
        : IMonTgt_G2H, IMonTgt_H2G { }

    public interface IMonTgt_Secondary : IMonitorEntity_MsgTgt { }

    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
        Name = "MonTgt_G2H")]
    [ExCommsMessageKnownType]
    public class MonTgt_G2H
        : MonitorEntity_MsgTgt, IMonTgt_G2H { }

    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
        Name = "MonTgt_H2G")]
    [ExCommsMessageKnownType]
    public class MonTgt_H2G
        : MonitorEntity_MsgTgt, IMonTgt_H2G { }

    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
        Name = "MonTgt_B2B")]
    [ExCommsMessageKnownType]
    public class MonTgt_B2B
        : MonitorEntity_MsgTgt { }    
}
