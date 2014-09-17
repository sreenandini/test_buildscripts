using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.CoreLib;
using BMC.CoreLib.Xml;

namespace BMC.CoreLib.Messages
{
    /// <summary>
    /// Polled Event Message Meters
    /// </summary>
    public class PolledEventMessageMeters : RawMessage
    {
        /// <summary>
        /// Polled Event Message
        /// </summary>
        private PolledEventMessage _parent = null;

        /// <summary>
        /// Collection of counters
        /// </summary>
        private IDictionary<PolledEventMessageMeterType, PolledEventMessageMeter> _currentMeters = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="PolledEventMessageMeters"/> class.
        /// </summary>
        /// <param name="parent">The parent.</param>
        internal PolledEventMessageMeters(PolledEventMessage parent)
            : base(string.Empty)
        {
            _parent = parent;
            _currentMeters = new SortedDictionary<PolledEventMessageMeterType, PolledEventMessageMeter>();
        }

        /// <summary>
        /// Adds the specified meter id.
        /// </summary>
        /// <param name="meterId">The meter id.</param>
        /// <param name="value">The value.</param>
        /// <returns>Queue message counter.</returns>
        public PolledEventMessageMeter Add(string meterId, string value)
        {
            return this.Add(TypeSystem.GetValueInt(meterId), TypeSystem.GetValueInt64(value));
        }

        /// <summary>
        /// Adds the specified meter id.
        /// </summary>
        /// <param name="meterId">The meter id.</param>
        /// <param name="value">The value.</param>
        /// <returns>Queue message counter.</returns>
        public PolledEventMessageMeter Add(int meterId, string value)
        {
            return this.Add(meterId, TypeSystem.GetValueInt64(value));
        }

        /// <summary>
        /// Adds the specified meter id.
        /// </summary>
        /// <param name="meterId">The meter id.</param>
        /// <param name="value">The value.</param>
        /// <returns>Queue message counter.</returns>
        public PolledEventMessageMeter Add(int meterId, long value)
        {
            PolledEventMessageMeterType type = PolledEventMessageMeterType.Unknown;
            if (Enum.IsDefined(typeof(PolledEventMessageMeterType), meterId)) type = (PolledEventMessageMeterType)meterId;
            if (type == PolledEventMessageMeterType.Unknown) return null;
            return this.Add(type, value);
        }

        /// <summary>
        /// Adds the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="value">The value.</param>
        /// <returns>Queue message counter.</returns>
        public PolledEventMessageMeter Add(PolledEventMessageMeterType type, long value)
        {
            PolledEventMessageMeter counter = this[type];
            counter.Value = value;
            return counter;
        }

        /// <summary>
        /// Gets or sets the <see cref="BMC.ExchangeMonitor.Core.MSMQ.QueueMessageMeter"/> with the specified ERROR.
        /// </summary>
        /// <value></value>
        public PolledEventMessageMeter this[PolledEventMessageMeterType type]
        {
            get
            {
                PolledEventMessageMeter counter = null;

                if (!_currentMeters.ContainsKey(type))
                {
                    counter = new PolledEventMessageMeter(_parent, type);
                    _currentMeters.Add(type, counter);
                }
                else
                {
                    counter = _currentMeters[type];
                }

                return counter;
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
            using (XmlWriterHelper helper = XmlWriterHelper.GetHelper(true))
            {
                helper.WriteStartElement("Meters")
                    .WriteAction((h) =>
                    {
                        foreach (PolledEventMessageMeter meter in _currentMeters.Values)
                        {
                            meter.WriteXmlElement(h);
                        }
                    })
                    .WriteEndElement(); ;
                return helper.ToString();
            }
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
            bool isNew = false;
            try
            {
                if (helper == null)
                {
                    isNew = true;
                    helper = XmlWriterHelper.GetHelper();
                }

                helper
                    .WriteStartElement("sector")
                    .WriteAction((h) =>
                    {
                        foreach (PolledEventMessageMeter meter in _currentMeters.Values)
                        {
                            meter.RawMessageDataInternal(h);
                        }
                    })
                    .WriteEndElement();
            }
            catch { }
            finally
            {
                if (isNew && helper != null)
                {
                    Extensions.DisposeObject(ref helper);
                }
            }
            return string.Empty;
        }
        #endregion
    }
}
