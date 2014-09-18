using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace BMC.VaultInterface.Business.Model
{
    [Serializable]
    [XmlRoot("Handpay")]
    public class HandpayModel
    {
        [XmlElement("Asset")]
        public string Asset { get; set; }

        [XmlElement("RedeemedAsset")]
        public string RedeemedAsset { get; set; }
        
        [XmlElement("SiteID")]
        public int SiteID { get; set; }

        [XmlElement("Amount")]
        public int Amount { get; set; }

        [XmlElement("RedeemDateTime")]
        public DateTime RedeemDateTime { get; set; }

        [XmlElement("GeneratedDateTime")]
        public DateTime GeneratedDateTime { get; set; }        

        [XmlElement("Type", Type = typeof(HandpayTypeModel))]
        public HandpayTypeModel Type { get; set; }
    }

    [Serializable]
    [XmlRoot("Handpays")]
    public class HandpaysModel
    {
        [XmlElement("Handpay")]
        public List<HandpayModel> Handpays { get; set; }
    }

    [Serializable]
    [XmlRoot("HandpayType")]
    public enum HandpayTypeModel
    {
        Handpay = 0,
        Jackpot = 1,
        MysteryJackpot = 2,
        ProgressiveJackpot = 3
    }
}
