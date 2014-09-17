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
        private MonTgt_G2H_Meter_MeterType _type = MonTgt_G2H_Meter_MeterType.Unknown;

        [DataMember(Name = "Value", Order = 1)]
        private long? _value = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="MonTgt_G2H_Meter" /> class.
        /// </summary>
        public MonTgt_G2H_Meter() { }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        public MonTgt_G2H_Meter_MeterType Type
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

        public int CoalesceIntValue
        {
            get
            {
                return (int)_value.SafeValue();
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
        : SortedDictionary<MonTgt_G2H_Meter_MeterType, MonTgt_G2H_Meter>
    {
        public long GetMeterValue(MonTgt_G2H_Meter_MeterType meterType)
        {
            using (ILogMethod method = Log.LogMethod("G2N_Mon_MsgTgt_MeterDictionary", "GetMeterValue"))
            {
                long result = default(long);

                try
                {
                    if (this.ContainsKey(meterType))
                    {
                        result = this[meterType].CoalesceValue;
                    }
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }

                return result;
            }
        }
    }

    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
        Name = "MonTgt_G2H_Meters")]
    public class MonTgt_G2H_Meters
        : MonTgt_G2H, IMonTgt_Secondary
    {
        public MonTgt_G2H_Meters()
        {
            this.Meters = new G2N_Mon_MsgTgt_MeterDictionary();
        }

        [DataMember()]
        public G2N_Mon_MsgTgt_MeterDictionary Meters { get; set; }

        [DataMember()]
        public string Source { get; set; }
    }
}
