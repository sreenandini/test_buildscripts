using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.CoreLib.Xml;

namespace BMC.CoreLib.Messages
{
    /// <summary>
    /// Polled Event Message Meter
    /// </summary>
    public class PolledEventMessageMeter : RawMessage
    {
        /// <summary>
        /// Polled Event Message
        /// </summary>
        private PolledEventMessage _parent = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="PolledEventMessageMeter"/> class.
        /// </summary>
        /// <param name="parent">The parent.</param>
        /// <param name="type">The type.</param>
        internal PolledEventMessageMeter(PolledEventMessage parent, PolledEventMessageMeterType type)
            : base(string.Empty)
        {
            _parent = parent;
            this.Type = type;
        }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        public PolledEventMessageMeterType Type { get; private set; }

        /// <summary>
        /// Meter Value
        /// </summary>
        private long? _value = null;

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

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            using (XmlWriterHelper helper = XmlWriterHelper.GetHelper())
            {
                this.WriteXmlElement(helper);
                return helper.ToString();
            }
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        internal void WriteXmlElement(XmlWriterHelper helper)
        {
            helper
               .WriteStartElement("M")
               .WriteAttribute("id", (int)this.Type)
               .WriteString(this.CoalesceValue)
               .WriteEndElement();
        }

        #region Raw Message Properties
        /// <summary>
        /// Gets the raw message internal.
        /// </summary>
        /// <param name="helper"></param>
        /// <returns></returns>
        /// <value>The raw message internal.</value>
        internal override string RawMessageDataInternal(XmlWriterHelper helper)
        {
            helper
               .WriteStartElement("counter")
               .WriteAttribute("counterid", (int)this.Type)
               .WriteString(this.CoalesceValue)
               .WriteEndElement();
            return string.Empty;
        }
        #endregion
    }
}
