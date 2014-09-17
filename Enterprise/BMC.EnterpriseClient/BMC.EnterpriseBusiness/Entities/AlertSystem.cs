using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace BMC.EnterpriseBusiness.Business
{
    public class AlertSystem
    {

    }

    public class AlertTypes
    {
        public int AlertTypeID { get; set; }

        public string AlertTypeName { get; set; }
    }

    public class AlertLink
    {
        public int AlertTypeID { get; set; }

        public string SubscriberID { get; set; }
    }



    [XmlRoot(ElementName = "EmailAlerts", Namespace = "")]
    public class EmailAlertEntity
    {
        public int AlertTypeId { get; set; }

        public string AlertTypeName { get; set; }

        public string Subject { get; set; }

        public string FromMail { get; set; }

        public string ToMail { get; set; }

        public string CCMail { get; set; }

        public string BCCMail { get; set; }
    }

    public class SiteEntity
    {
        public int SiteID { get; set; }

        public string SiteCode { get; set; }

        public string SiteName { get; set; }
    }

    public class AlertAuditEntity
    {
        public string AlertType { get; set; }

        public string Content { get; set; }

        public string SiteName { get; set; }

        public string SiteCode { get; set; }

        public int Status { get; set; }

        public string Date { get; set; }

        public string Result { get; set; }

        public string RowColor { get; set; } 

      

    }

    [Serializable]
    public class MailServer
    {
        public string ServerName { get; set; }

        public string UserID { get; set; }

        public string Password { get; set; }

        public string Port { get; set; }

        public bool EnableSSL { get; set; }

    }
}

