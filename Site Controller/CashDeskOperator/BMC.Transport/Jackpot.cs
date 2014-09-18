using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace BMC.Transport
{

    public partial class jackpotProcessInfoDTO : jackpotBaseDTO
    {
       

        private string SlotField;

        private string transactiondatefield;

        private string useridfield;

        private string usernamefield;

        private decimal denomfield;
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 13)]
        public string Slot
        {
            get
            {
                return this.SlotField;
            }
            set
            {
                this.SlotField = value;
            }
        }
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 14)]
        public string TransactionDate
        {
            get
            {
                return this.transactiondatefield;
            }
            set
            {
                this.transactiondatefield = value;
            }
        }
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 15)]
        public string UserID
        {
            get
            {
                return this.useridfield;
            }
            set
            {
                this.useridfield = value;
            }
        }
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 16)]
        public string UserName
        {
            get
            {
                return this.usernamefield;
            }
            set
            {
                this.usernamefield = value;
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 17)]
        public decimal Denom
        {
            get
            {
                return this.denomfield;
            }
            set
            {
                this.denomfield = value;
            }
        }
    }


    [DataContract]
    public partial class HandpayEntities
    {
        public HandpayEntities()
        {
        }

        public System.Nullable<int> installation_no
        { get; set; }


        public string strbarcode
        { get; set; }

        public string handpaytype
        { get; set; }

        public DateTime TeDate
        { get; set; }


        public decimal amount
        {  get; set; }
        public int IsManualAttendantPay { get; set; }
    }
}