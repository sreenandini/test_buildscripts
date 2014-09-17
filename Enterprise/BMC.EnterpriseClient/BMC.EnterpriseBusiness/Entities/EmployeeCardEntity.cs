using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.EnterpriseBusiness.Entities
{
    public class EmployeeCardEntity
    {
        public int EmpID { get; set; }

        public string EmployeeCardNumber { get; set; }

        public string EmployeeName { get; set; }

        public bool? IsActive { get; set; }

        public bool? IsMasterCard { get; set; }

        public string CardType { get; set; }

        public int? UserID { get; set; }

        public string Mapped { get; set; }

        public bool? IsChecked { get; set; }

        public string SiteCode { get; set; }

        public int? EmpCardLevel { get; set; }

    }

    public class EmployeeCardTypes
    {
        public int EmpCardTypeID { get; set; }
        public string EmpCardType { get; set; }
    }
    public class CardLevel
    {
        public int? CardLevelID { get; set; }
        public int RoleID { get; set; }
    }
    public class EmployeeEventGroupTypes
    {
        public int EmpEventGroupID { get; set; }
        public string EmpEventGroupType { get; set; }
    }

    public class EmployeeModeGroup
    {
        public string GMUMode { get; set; }
        public int GMUModeGroupID { get; set; }
        public int GMUModeID { get; set; }
        public string ModeDescription { get; set; }
        public string ModeName { get; set; }
    }

    public class EmployeeEvents
    {
        public string GMUEventName { get; set; }
        public int GMUEventID { get; set; }
        public string GMUEventDescription { get; set; }
        public int GMUEventGroupID { get; set; }
        public int Event_Fault_Type { get; set; }
        public int Event_Fault_Source { get; set; }
    }
    public class EmployeeGMUModeGroup
    {
        public string EmpCardNumber { get; set; }
        public int EmpGMUModeId { get; set; }
        public int GMUModeId { get; set; }
        public string GMUMode { get; set; }
        public int GMUModeGroupID { get; set; }
        public int? EmpGMUModeGroup { get; set; }
    }
    public class EmployeeEventsGroup
    {
        public string EmpCardNumber { get; set; }
        public int EmpGMUEventId { get; set; }
        public int GMUEventID { get; set; }
        public int GMUEventGroupID { get; set; }
    }
    public class MarkGMUEventsorModes
    {
        public int GMUEventorModeID { get; set; }
        public bool isDelete { get; set; }
        public bool isNew { get; set; }
    }
}
