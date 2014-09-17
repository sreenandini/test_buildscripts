using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using BMC.CoreLib.Diagnostics;
using BMC.CoreLib;
using System.Xml.Serialization;
using System.Xml.Schema;

namespace BMC.EBSComms.Contracts.Dto
{
    public partial class s2sHeader
    {
        public s2sHeader()
        {
            this.dateTimeSent = DateTime.Now;
        }
    }

    public partial class configuration
    {
        public configuration()
        {
            this.dateTime = DateTime.Now;
        }
    }

    public partial class getConfiguration
    {
        private getConfigurationGetCasino[] getCasinoField;

        private getConfigurationGetDenomination[] getDenominationField;

        private getConfigurationGetManufacturer[] getManufacturerField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("getCasino", Order = 45)]
        public getConfigurationGetCasino[] getCasino
        {
            get
            {
                return this.getCasinoField;
            }
            set
            {
                this.getCasinoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("getDenomination", Order = 46)]
        public getConfigurationGetDenomination[] getDenomination
        {
            get
            {
                return this.getDenominationField;
            }
            set
            {
                this.getDenominationField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("getManufacturer", Order = 47)]
        public getConfigurationGetManufacturer[] getManufacturer
        {
            get
            {
                return this.getManufacturerField;
            }
            set
            {
                this.getManufacturerField = value;
            }
        }

        public void ParseAnyElements()
        {
            ModuleProc PROC = new ModuleProc("", "ParseAnyElements");

            try
            {
                if (this.Any == null || this.Any.Length == 0) return;

                foreach (var item in this.Any)
                {
                    if (item.NamespaceURI == s2sHelper.NS_BS2S)
                    {
                        try
                        {
                            XElement xElm = XElement.Parse(item.OuterXml);

                            switch (item.LocalName.ToLower())
                            {
                                case "getcasino":
                                    {
                                        getConfigurationGetCasino o = new getConfigurationGetCasino();
                                        o.casinoId = xElm.GetAttributeValue("casinoId", s2sHelper.NS_BS2S);
                                        this.getCasino = new getConfigurationGetCasino[] { o };
                                    }
                                    break;

                                case "getdenomination":
                                    {
                                        getConfigurationGetDenomination o = new getConfigurationGetDenomination();
                                        o.denomId = xElm.GetAttributeValue("denomId", s2sHelper.NS_BS2S);
                                        this.getDenomination = new getConfigurationGetDenomination[] { o };
                                    }
                                    break;

                                case "getmanufacturer":
                                    {
                                        getConfigurationGetManufacturer o = new getConfigurationGetManufacturer();
                                        o.manufacturerId = xElm.GetAttributeValue("manufacturerId", s2sHelper.NS_BS2S);
                                        this.getManufacturer = new getConfigurationGetManufacturer[] { o };
                                    }
                                    break;

                                default:
                                    break;
                            }
                        }
                        catch (Exception ex)
                        {
                            Log.Exception(PROC, ex);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }
    }

    public partial class configurationInfo
    {
        private casino[] casinoField;

        private denomination[] denominationField;

        private manufacturer[] manufacturerField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("casino", Namespace = "http://www.ballytech.com/s2s/schemas/v1.0.0/", Order = 45)]
        public casino[] casino
        {
            get
            {
                return this.casinoField;
            }
            set
            {
                this.casinoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("denomination", Namespace = "http://www.ballytech.com/s2s/schemas/v1.0.0/", Order = 46)]
        public denomination[] denomination
        {
            get
            {
                return this.denominationField;
            }
            set
            {
                this.denominationField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("manufacturer", Namespace = "http://www.ballytech.com/s2s/schemas/v1.0.0/", Order = 47)]
        public manufacturer[] manufacturer
        {
            get
            {
                return this.manufacturerField;
            }
            set
            {
                this.manufacturerField = value;
            }
        }
    }

    public partial class machine
    {
        private string manufacturerIdField;
        private string casinoIdField;
        private string machineTypeField;
        private string zoneIdField;
        private string bankIdField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 11)]
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
        [System.Xml.Serialization.XmlElementAttribute(Order = 12)]
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
        [System.Xml.Serialization.XmlElementAttribute(Order = 13)]
        public string machineType
        {
            get
            {
                return this.machineTypeField;
            }
            set
            {
                this.machineTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 14)]
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

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 15)]
        public string bankId
        {
            get
            {
                return this.bankIdField;
            }
            set
            {
                this.bankIdField = value;
            }
        }
    }
}
