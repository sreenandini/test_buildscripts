using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using BMC.CoreLib;

namespace BMC.ExComms.Contracts.DTO.Monitor
{
    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
        Name = "MonTgt_G2H_Meter")]
    [ExCommsMessageKnownType]
    public class MonTgt_G2H_Meter
        : MonTgt_G2H
    {
        [DataMember(Name = "Type", Order = 0)]
        private Mon_MeterTypes _type = Mon_MeterTypes.Unknown;

        [DataMember(Name = "Value", Order = 1)]
        private long? _value = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="MonTgt_G2H_Meter"/> class.
        /// </summary>
        /// <param name="parent">The parent.</param>
        /// <param name="type">The type.</param>
        public MonTgt_G2H_Meter() { }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        public Mon_MeterTypes Type
        {
            get { return _type; }
            set { _type = value; }
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        public long? Value
        {
            get { return _value; }
            set { _value = value; }
        }

        /// <summary>
        /// Gets the coalesce value.
        /// </summary>
        /// <value>The coalesce value.</value>
        public long CoalesceValue
        {
            get
            {
                return _value.SafeValue();
            }
        }

        public override string ToString()
        {
            return string.Format("{0} => {1:D}", this.Type, this.CoalesceValue); ;
        }
    }

    [CollectionDataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
        Name = "MonTgt_G2H_MeterDictionary",
        ItemName = "Meter",
        KeyName = "MeterId",
        ValueName = "MeterValue")]
    public class G2N_Mon_MsgTgt_MeterDictionary
        : SortedDictionary<Mon_MeterTypes, MonTgt_G2H_Meter> { }

    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
        Name = "MonTgt_G2H_Meters")]
    public class MonTgt_G2H_Meters
        : MonTgt_G2H
    {
        public MonTgt_G2H_Meters()
        {
            this.Meters = new G2N_Mon_MsgTgt_MeterDictionary();
        }

        [DataMember(Order = 0)]
        public G2N_Mon_MsgTgt_MeterDictionary Meters { get; set; }
    }
}
