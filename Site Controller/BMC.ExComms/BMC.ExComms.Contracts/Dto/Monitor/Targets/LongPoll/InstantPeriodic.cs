using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Monitor
{
    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
       Name = "MonTgt_H2G_LP_InstantPeriodic")]
    [ExCommsMessageKnownType]
    public class MonTgt_H2G_LP_InstantPeriodic
        : MonTgt_H2G
    {
        public MonTgt_H2G_LP_InstantPeriodic() { }

        /// <summary>
        /// Configured Interval
        /// </summary>
        [DataMember]
        public byte ConfiguredInterval { get; set; }

        /// <summary>
        /// Lower Order Interval
        /// </summary>
        [DataMember]
        public byte LowerOrderInterval { get; set; }

        /// <summary>
        /// Higer Order Interval
        /// </summary>
        [DataMember]
        public byte HigherOrderInterval { get; set; }
    }
}
