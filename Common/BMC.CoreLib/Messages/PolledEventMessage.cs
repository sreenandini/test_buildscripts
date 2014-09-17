using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using BMC.CoreLib.Xml;
using BMC.CoreLib;
using BMC.CoreLib.Concurrent;

namespace BMC.CoreLib.Messages
{
    /// <summary>
    /// Polled Event Message
    /// </summary>
    public class PolledEventMessage : RawMessage, IExecutorKey
    {
        private const string DATE_TIME_FORMAT = "dd/MM/yyyy hh:mm:ss";

        /// <summary>
        /// Initializes a new instance of the <see cref="PolledEventMessage"/> class.
        /// </summary>
        internal PolledEventMessage() : this(null) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PolledEventMessage"/> class.
        /// </summary>
        /// <param name="rawMessage">The raw message.</param>
        internal PolledEventMessage(string rawMessage)
            : base(rawMessage)
        {
            this.Meters = new PolledEventMessageMeters(this);
            this.MetersReceived = new PolledEventMessageMeters(this);
        }

        #region Message Properties
        /// <summary>
        /// Bar position
        /// </summary>
        private string _barPosition = string.Empty;

        /// <summary>
        /// Installallation No
        /// </summary>
        private int _installationNo = 0;

        /// <summary>
        /// Gets or sets the meters.
        /// </summary>
        /// <value>The meters.</value>
        public PolledEventMessageMeters Meters { get; private set; }

        /// <summary>
        /// Gets or sets the meters received.
        /// </summary>
        /// <value>The meters received.</value>
        public PolledEventMessageMeters MetersReceived { get; private set; }

        /// <summary>
        /// Gets or sets the site code.
        /// </summary>
        /// <value>The site code.</value>
        public string SiteCode { get; internal set; }

        /// <summary>
        /// Gets or sets the bar position.
        /// </summary>
        /// <value>The bar position.</value>
        public string BarPosition
        {
            get
            {
                return _barPosition;
            }
            set
            {
                _barPosition = value;
                this.BarPositionInt = TypeSystem.GetValueInt(value);
            }
        }

        /// <summary>
        /// Gets or sets the bar position int.
        /// </summary>
        /// <value>The bar position int.</value>
        public int BarPositionInt { get; private set; }

        /// <summary>
        /// Gets the name of the bar position.
        /// </summary>
        /// <value>The name of the bar position.</value>
        public string BarPositionName
        {
            get
            {
                //if (this.Installation != null)
                //    return this.Installation.Bar_Pos_Name;
                return string.Empty;
            }
        }

        /// <summary>
        /// Gets or sets the fault source.
        /// </summary>
        /// <value>The fault source.</value>
        public int FaultSource { get; internal set; }

        /// <summary>
        /// Gets or sets the type of the fault.
        /// </summary>
        /// <value>The type of the fault.</value>
        public int FaultType { get; internal set; }

        /// <summary>
        /// Gets or sets the installation no.
        /// </summary>
        /// <value>The installation no.</value>
        public int InstallationNo
        {
            get
            {
                return _installationNo;
            }
            set
            {
                _installationNo = value;
            }
        }

        /// <summary>
        /// Gets or sets the serial no.
        /// </summary>
        /// <value>The serial no.</value>
        public string SerialNo
        {
            get
            {
                return _installationNo.ToString();
            }
        }

        /// <summary>
        /// Gets or sets the fault message.
        /// </summary>
        /// <value>The fault message.</value>
        public string FaultMessage { get; internal set; }

        /// <summary>
        /// Gets or sets the event value.
        /// </summary>
        /// <value>The event value.</value>
        public string EventValue { get; internal set; }

        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        /// <value>The date.</value>
        public DateTime Date { get; internal set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public string Description { get; internal set; }
        #endregion

        #region Methods
        public void SetValues(params object[] values)
        {
            if (values != null && values.Length > 0)
            {
                if (values.Length > 0) this.SiteCode = values[0].ToStringSafe();
                if (values.Length > 1) this.BarPosition = values[1].ToStringSafe();
                if (values.Length > 2) this.FaultSource = TypeSystem.GetValueInt(values[2]);
                if (values.Length > 3) this.FaultType = TypeSystem.GetValueInt(values[3]);
                if (values.Length > 4) this.InstallationNo = TypeSystem.GetValueInt(values[4]);
                if (values.Length > 5) this.FaultMessage = values[5].ToStringSafe();
                if (values.Length > 6) this.Date = TypeSystem.GetValueDateTimeFormat(values[6], DATE_TIME_FORMAT);
                if (values.Length > 7) this.EventValue = values[7].ToStringSafe();
                if (values.Length > 8) this.Description = values[8].ToStringSafe();
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="System.Int64"/> with the specified type.
        /// </summary>
        /// <value></value>
        public long this[PolledEventMessageMeterType type]
        {
            get
            {
                return this.Meters[type].CoalesceValue;
            }
            set
            {
                this.Meters[type].Value = value;
            }
        }
        #endregion

        #region Raw Message Properties
        /// <summary>
        /// Gets the raw message internal.
        /// </summary>
        /// <param name="helper"></param>
        /// <returns></returns>
        /// <value>The raw message internal.</value>
        internal override string RawMessageDataInternal(XmlWriterHelper helper)
        {
            string result = string.Empty;

            using (XmlWriterHelper helper2 = XmlWriterHelper.GetHelper())
            {
                try
                {
                    helper2
                        .WriteStartElement("Polled_Event")
                        .WriteElementString("Site_Code", this.SiteCode)
                        .WriteElementString("Bar_Pos", this.BarPosition)
                        .WriteElementString("Fault_Source", this.FaultSource)
                        .WriteElementString("Fault_Type", this.FaultType)
                        .WriteElementString("Installation", this.InstallationNo)
                        .WriteElementString("Serial_No", this.SerialNo)
                        .WriteElementString("Fault_Message", this.FaultMessage)
                        .WriteElementString("Date", this.Date, DATE_TIME_FORMAT)
                        .WriteElementString("Event_Value", this.EventValue)
                        .WriteAction((h) =>
                        {
                            this.Meters.RawMessageDataInternal(h);
                        })
                        .WriteElementString("Description", this.Description)
                        .WriteEndElement();
                }
                catch { }
                finally
                {
                    result = helper2.ToString();
                }
            }

            return result;
        }
        #endregion

        #region IExecutorKey Members

        public string UniqueKey
        {
            get { return this.InstallationNo.ToString(); }
        }

        #endregion
    }
}
