using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace BMC.VaultInterface.Business.Model
{
    [Serializable]
    [XmlRoot("Event")]
    public class EventModel
    {
        private int _SiteID;
        [XmlElement("DeviceID")]
        public string DeviceID { get; set; }

        [XmlElement("SiteID")]
        public int SiteID
        {
            get
            {
                return _SiteID;
            }
            set
            {
                _SiteID = value;
            }
        }

        [XmlElement("EventID")]
        public int EventID { get; set; }

        [XmlElement("EventDescription")]
        public string EventDescription { get; set; }

        [XmlElement("EventDateTime")]
        public DateTime EventDateTime { get; set; }

        [XmlElement("CassetteDetails", Type = typeof(CassetteDetails))]
        public List<CassetteDetails> CassetteDetail { get; set; }
    }

    [Serializable]
    [XmlRoot("Events")]
    public class EventsModel
    {
        [XmlElement("Event")]
        public List<EventModel> Events { get; set; }
    }

    [Serializable]
    [XmlRoot("CassetteDetails")]
    public class CassetteDetails
    {
        [XmlElement("CassetteNumber")]
        public string CassetteNumber { get; set; }

        [XmlElement("CassetteLevel")]
        public int CassetteLevel { get; set; }

        [XmlElement("CassetteDenom")]
        public int CassetteDenom { get; set; }

        [XmlElement("CassetteDescription")]
        public string CassetteDescription { get; set; }

    }

}
