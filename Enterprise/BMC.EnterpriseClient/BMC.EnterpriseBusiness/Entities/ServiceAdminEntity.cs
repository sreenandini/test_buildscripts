using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace BMC.EnterpriseBusiness.Entities
{
    public class CallGroup
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Reference { get; set; }
        public bool Downtime { get; set; }
        public DateTime? EndDate { get; set; }
        public bool LogEngineerChange { get; set; }
    }

    public class CallFault
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public string Description { get; set; }
        public string Reference { get; set; }
        public DateTime? EndDate { get; set; }
    }

    public class CallRemedy
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int? Reference { get; set; }
        public bool Downtime { get; set; }
        public DateTime? EndDate { get; set; }
    }

    public class CallSource
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Reference { get; set; }
    }

}
