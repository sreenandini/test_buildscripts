using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMC.BusinessClasses.BusinessLogic
{
    [Serializable]
    public class AlertEntity
    {
        public string Name { get; set; }
        public MailMessageGroup MessageGroup { get; set; }
        public MailServer ServerInfo { get; set; }
        public Schedule Schedule { get; set; }

    }

    [Serializable]
    public class Schedule
    {
        public bool IsReptitive { get; set; }
        public ScheduleType Type { get; set; }
        public string ExecutionTime { get; set; }
    }

    public enum ScheduleType
    {
        Daily,
        Monthly,
        Hourly,
        Yearly
    }

    [Serializable]
    public class MailMessageGroup
    {
        public string FromAddress { get; set; }
        public string ToList { get; set; }
        public string CCList { get; set; }
        public string BCCList { get; set; }
        public string Subject { get; set; }
        public string MsgContent { get; set; }
        public string AttachementPath { get; set; }
        public string AttachmentType { get; set; }
        public bool IsBodyHtml { get; set; }
        public string Encoding { get; set; }
    }

    [Serializable]
    public class MailServer
    {
        public string ServerName { get; set; }
        public string UserID { get; set; }
        public string Password { get; set; }

        public string Port { get; set; }
        public bool EnableSSL { get; set; }

        public string PickupFolder { get; set; }
    }
}
