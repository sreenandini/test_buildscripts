using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using BMC.CoreLib;
using BMC.CoreLib.Diagnostics;

namespace BMC.EBSComms.Contracts.Dto
{
    public interface Is2sMessage_Items
    {
        object[] Items { get; set; }
    }

    public interface Is2sMessage_Item
    {
        object Item { get; set; }
    }

    public partial class s2sMessage : Is2sMessage_Items
    {
        [NonSerialized]
        private string _p_propertyId = null;

        [XmlIgnore]
        public string p_propertyId
        {
            get { return _p_propertyId; }
            set { _p_propertyId = value; }
        }

        [NonSerialized]
        private s2sAck _ack = null;

        [XmlIgnore]
        public s2sAck p_ack
        {
            get { return _ack; }
            set { _ack = value; }
        }

        [NonSerialized]
        private s2sHeader _header = null;

        [XmlIgnore]
        public s2sHeader p_header
        {
            get { return _header; }
            set { _header = value; }
        }

        [NonSerialized]
        private s2sBody _body = null;

        [XmlIgnore]
        public s2sBody p_body
        {
            get { return _body; }
            set { _body = value; }
        }
    }

    public partial class s2sBody : Is2sMessage_Items
    {
        [NonSerialized]
        private configuration _configuration = null;

        [XmlIgnore]
        public configuration p_configuration
        {
            get { return _configuration; }
            set { _configuration = value; }
        }

        [NonSerialized]
        private infoUpdate _infoUpdate = null;

        [XmlIgnore]
        public infoUpdate p_infoUpdate
        {
            get { return _infoUpdate; }
            set { _infoUpdate = value; }
        }
    }

    public partial class configuration : Is2sMessage_Item
    {
        [NonSerialized]
        private configurationInfo _configurationInfo = null;

        [XmlIgnore]
        public configurationInfo p_configurationInfo
        {
            get { return _configurationInfo; }
            set { _configurationInfo = value; }
        }
    }

    public partial class infoUpdate : Is2sMessage_Item
    {
        [NonSerialized]
        private infoUpdateData _infoUpdateData = null;

        [XmlIgnore]
        public infoUpdateData p_infoUpdateData
        {
            get { return _infoUpdateData; }
            set { _infoUpdateData = value; }
        }
    }

    public partial class infoUpdateData 
    {
        private string actionField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified, 
            Namespace = "http://www.gamingstandards.com/s2s/schemas/v1.2.6/")]
        public string action
        {
            get
            {
                return this.actionField;
            }
            set
            {
                this.actionField = value;
            }
        }
    }

    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(TypeName = "machine", AnonymousType = true, Namespace = "http://www.gamingstandards.com/s2s/schemas/v1.2.6/")]
    [System.Xml.Serialization.XmlRootAttribute(ElementName = "machine", Namespace = "http://www.gamingstandards.com/s2s/schemas/v1.2.6/", IsNullable = false)]
    public partial class machine_infoUpdate
    {

        private string machineIdField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public string machineId
        {
            get
            {
                return this.machineIdField;
            }
            set
            {
                this.machineIdField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(TypeName = "zone", AnonymousType = true, Namespace = "http://www.gamingstandards.com/s2s/schemas/v1.2.6/")]
    [System.Xml.Serialization.XmlRootAttribute(ElementName = "zone", Namespace = "http://www.gamingstandards.com/s2s/schemas/v1.2.6/", IsNullable = false)]
    public partial class zone_infoUpdate
    {

        private string zoneIdField;


        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public string zoneId
        {
            get
            {
                return this.zoneIdField;
            }
            set
            {
                this.zoneIdField = value;
            }
        }
    }

    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(TypeName = "game", AnonymousType = true, Namespace = "http://www.gamingstandards.com/s2s/schemas/v1.2.6/")]
    [System.Xml.Serialization.XmlRootAttribute(ElementName = "game", Namespace = "http://www.gamingstandards.com/s2s/schemas/v1.2.6/", IsNullable = false)]
    public partial class game_infoUpdate
    {

        private string gameIdField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public string gameId
        {
            get
            {
                return this.gameIdField;
            }
            set
            {
                this.gameIdField = value;
            }
        }
    }
}
