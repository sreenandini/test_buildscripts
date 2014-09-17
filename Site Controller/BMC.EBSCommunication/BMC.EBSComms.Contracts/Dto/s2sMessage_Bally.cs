using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using BMC.CoreLib;
using BMC.CoreLib.Diagnostics;

namespace BMC.EBSComms.Contracts.Dto
{
    public static class s2sHelper
    {
        public const string NS_GSA = "http://www.gamingstandards.com/s2s/schemas/v1.2.6/";
        public const string NS_BS2S = "http://www.ballytech.com/s2s/schemas/v1.0.0/";
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.ballytech.com/s2s/schemas/v1.0.0/")]
    public partial class getConfigurationGetCasino
    {
        private string casinoIdField;

        public getConfigurationGetCasino() { }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string casinoId
        {
            get
            {
                return this.casinoIdField;
            }
            set
            {
                this.casinoIdField = value;
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.ballytech.com/s2s/schemas/v1.0.0/")]
    public partial class casino
    {

        private string casinoIdField;

        private string casinoNameField;

        private bool casinoActiveField;

        private System.Xml.XmlElement[] anyField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public string casinoId
        {
            get
            {
                return this.casinoIdField;
            }
            set
            {
                this.casinoIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public string casinoName
        {
            get
            {
                return this.casinoNameField;
            }
            set
            {
                this.casinoNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
        public bool casinoActive
        {
            get
            {
                return this.casinoActiveField;
            }
            set
            {
                this.casinoActiveField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAnyElementAttribute(Order = 3)]
        public System.Xml.XmlElement[] Any
        {
            get
            {
                return this.anyField;
            }
            set
            {
                this.anyField = value;
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(TypeName = "casino", AnonymousType = true, Namespace = "http://www.ballytech.com/s2s/schemas/v1.0.0/")]
    public partial class casino_infoUpdate
    {

        private string casinoIdField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public string casinoId
        {
            get
            {
                return this.casinoIdField;
            }
            set
            {
                this.casinoIdField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.ballytech.com/s2s/schemas/v1.0.0/")]
    public partial class getConfigurationGetDenomination
    {
        private string denomIdField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string denomId
        {
            get
            {
                return this.denomIdField;
            }
            set
            {
                this.denomIdField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.ballytech.com/s2s/schemas/v1.0.0/")]
    public partial class denomination
    {

        private string denominationIdField;

        private string denominationNameField;

        private string denominationValueField;

        private bool denominationActiveField;

        private System.Xml.XmlElement[] anyField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public string denominationId
        {
            get
            {
                return this.denominationIdField;
            }
            set
            {
                this.denominationIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public string denominationName
        {
            get
            {
                return this.denominationNameField;
            }
            set
            {
                this.denominationNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
        public string denominationValue
        {
            get
            {
                return this.denominationValueField;
            }
            set
            {
                this.denominationValueField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 3)]
        public bool denominationActive
        {
            get
            {
                return this.denominationActiveField;
            }
            set
            {
                this.denominationActiveField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAnyElementAttribute(Order = 4)]
        public System.Xml.XmlElement[] Any
        {
            get
            {
                return this.anyField;
            }
            set
            {
                this.anyField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(TypeName = "denomination", AnonymousType = true, Namespace = "http://www.ballytech.com/s2s/schemas/v1.0.0/")]
    public partial class denomination_infoUpdate
    {

        private string denominationIdField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public string denominationId
        {
            get
            {
                return this.denominationIdField;
            }
            set
            {
                this.denominationIdField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.ballytech.com/s2s/schemas/v1.0.0/")]
    public partial class getConfigurationGetManufacturer
    {
        private string manufacturerIdField;

        public getConfigurationGetManufacturer()
        {
            int i = 5;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string manufacturerId
        {
            get
            {
                return this.manufacturerIdField;
            }
            set
            {
                this.manufacturerIdField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.ballytech.com/s2s/schemas/v1.0.0/")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.ballytech.com/s2s/schemas/v1.0.0/", IsNullable = false)]
    public partial class manufacturer
    {

        private string manufacturerIdField;

        private string manufacturerNameField;

        private string manufacturerValueField;

        private bool manufacturerActiveField;

        private System.Xml.XmlElement[] anyField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public string manufacturerId
        {
            get
            {
                return this.manufacturerIdField;
            }
            set
            {
                this.manufacturerIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public string manufacturerName
        {
            get
            {
                return this.manufacturerNameField;
            }
            set
            {
                this.manufacturerNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
        public string manufacturerValue
        {
            get
            {
                return this.manufacturerValueField;
            }
            set
            {
                this.manufacturerValueField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 3)]
        public bool manufacturerActive
        {
            get
            {
                return this.manufacturerActiveField;
            }
            set
            {
                this.manufacturerActiveField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAnyElementAttribute(Order = 4)]
        public System.Xml.XmlElement[] Any
        {
            get
            {
                return this.anyField;
            }
            set
            {
                this.anyField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(TypeName = "manufacturer", AnonymousType = true, Namespace = "http://www.ballytech.com/s2s/schemas/v1.0.0/")]
    [System.Xml.Serialization.XmlRootAttribute(ElementName = "manufacturer", Namespace = "http://www.ballytech.com/s2s/schemas/v1.0.0/", IsNullable = false)]
    public partial class manufacturer_infoUpdate
    {

        private string manufacturerIdField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public string manufacturerId
        {
            get
            {
                return this.manufacturerIdField;
            }
            set
            {
                this.manufacturerIdField = value;
            }
        }
    }
}
