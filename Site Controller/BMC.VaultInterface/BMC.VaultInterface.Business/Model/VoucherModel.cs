using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace BMC.VaultInterface.Business.Model
{
    [Serializable]
    [XmlRoot("Voucher")]
    public class VoucherModel
    {
        [XmlElement("BarCode")]
        public string BarCode { get; set; }

        [XmlElement("Amount")]
        public int Amount { get; set; }

        [XmlElement("SiteID")]
        public string SiteID { get; set; }

        [XmlElement("RedeemedDateTime")]
        public DateTime RedeemedDateTime { get; set; }

        [XmlElement("RedeemedAsset")]
        public string RedeemedAsset { get; set; }

        [XmlElement("PrintedAsset")]
        public string PrintedAsset { get; set; }

        [XmlElement("PrintedDateTime ")]
        public DateTime PrintedDateTime { get; set; }

        [XmlElement("ExpiryDate")]
        public DateTime ExpiryDate { get; set; }

        [XmlElement("Type", Type = typeof(VoucherTypeModel))]
        public VoucherTypeModel Type { get; set; }

    }

    [Serializable]
    [XmlRoot("Vouchers")]
    public class VouchersModel
    {
        [XmlElement("Voucher")]
        public List<VoucherModel> Voucher { get; set; }
    }

    [Serializable]
    [XmlRoot("VoucherType")]
    public enum VoucherTypeModel
    {
        Cashable = 0,
        NonCashable = 1
    }
}
