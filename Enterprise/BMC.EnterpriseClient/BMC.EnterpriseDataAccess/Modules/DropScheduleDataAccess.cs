using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;
using System.Data.Linq;
using System.Reflection;

namespace BMC.EnterpriseDataAccess
{
    public partial class EnterpriseDataContext
    {
        [Function(Name = "dbo.rsp_GetRegion")]
        public ISingleResult<rsp_GetRegionResult> GetRegion()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<rsp_GetRegionResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetSiteOnRegion")]
        public ISingleResult<rsp_GetSiteOnRegionResult> GetSiteOnRegion([Parameter(Name = "RegionId", DbType = "Int")] System.Nullable<int> regionId)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), regionId);
            return ((ISingleResult<rsp_GetSiteOnRegionResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.usp_STM_Export_History")]
        public int STM_Export_History([Parameter(Name = "Type", DbType = "VarChar(50)")] string type, [Parameter(Name = "ClientID", DbType = "Int")] System.Nullable<int> clientID, [Parameter(Name = "Site_Code", DbType = "VarChar(50)")] string site_Code, [Parameter(Name = "XmlMessage", DbType = "Xml")] System.Xml.Linq.XElement xmlMessage)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), type, clientID, site_Code, xmlMessage);
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetDropScheduleAuto")]
        public ISingleResult<rsp_GetDropScheduleAutoResult> GetDropScheduleAuto([Parameter(Name = "ScheduleID", DbType = "Int")] System.Nullable<int> scheduleID, [Parameter(Name = "CurrentDate", DbType = "DateTime")] System.Nullable<System.DateTime> currentDate)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), scheduleID, currentDate);
            return ((ISingleResult<rsp_GetDropScheduleAutoResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.usp_InsertUpdateDropSchedule")]
        public int InsertUpdateDropSchedule(
                    [Parameter(Name = "ScheduleID", DbType = "Int")] System.Nullable<int> scheduleID,
                    [Parameter(Name = "Name", DbType = "VarChar(50)")] string name,
                    [Parameter(Name = "ScheduleTime", DbType = "DateTime")] System.Nullable<System.DateTime> scheduleTime,
                    [Parameter(Name = "StackerLevel", DbType = "TinyInt")] System.Nullable<byte> stackerLevel,
                    [Parameter(Name = "ScheduleType", DbType = "TinyInt")] System.Nullable<byte> scheduleType,
                    [Parameter(Name = "StartDate", DbType = "DateTime")] System.Nullable<System.DateTime> startDate,
                    [Parameter(Name = "EndDate", DbType = "DateTime")] System.Nullable<System.DateTime> endDate,
                    [Parameter(Name = "OccurrenceType", DbType = "TinyInt")] System.Nullable<byte> occurrenceType,
                    [Parameter(Name = "TotalOccurrence", DbType = "Int")] System.Nullable<int> totalOccurrence,
                    [Parameter(Name = "WeekDays", DbType = "TinyInt")] System.Nullable<byte> weekDays,
                    [Parameter(Name = "MonthDuration", DbType = "Int")] System.Nullable<int> monthDuration,
                    [Parameter(Name = "DateofMonth", DbType = "TinyInt")] System.Nullable<byte> dateofMonth,
                    [Parameter(Name = "NextOcc", DbType = "DateTime")] System.Nullable<System.DateTime> nextOcc,
                    [Parameter(Name = "IsActive", DbType = "Bit")] System.Nullable<bool> isActive,
                    [Parameter(Name = "DropAlertType", DbType = "Int")] System.Nullable<int> dropAlertType,
                    [Parameter(Name = "RegionId", DbType = "Int")] System.Nullable<int> regionId,
                    [Parameter(Name = "SiteId", DbType = "Int")] System.Nullable<int> siteId,
                    [Parameter(Name = "ScheduleIDOut", DbType = "Int")] ref System.Nullable<int> scheduleIDOut)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), scheduleID, name, scheduleTime, stackerLevel, scheduleType, startDate, endDate, occurrenceType, totalOccurrence, weekDays, monthDuration, dateofMonth, nextOcc, isActive, dropAlertType, regionId, siteId, scheduleIDOut);
            scheduleIDOut = ((System.Nullable<int>)(result.GetParameterValue(17)));
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.usp_InsertUpdateDropScheduleHistory")]
        public int InsertUpdateDropScheduleHistory(
                    [Parameter(Name = "DropScheduleHistoryID", DbType = "Int")] System.Nullable<int> dropScheduleHistoryID,
                    [Parameter(Name = "ScheduleID", DbType = "Int")] System.Nullable<int> scheduleID,
                    [Parameter(Name = "Name", DbType = "VarChar(50)")] string name,
                    [Parameter(Name = "ScheduleTime", DbType = "DateTime")] System.Nullable<System.DateTime> scheduleTime,
                    [Parameter(Name = "StackerLevel", DbType = "TinyInt")] System.Nullable<byte> stackerLevel,
                    [Parameter(Name = "ScheduleType", DbType = "TinyInt")] System.Nullable<byte> scheduleType,
                    [Parameter(Name = "StartDate", DbType = "DateTime")] System.Nullable<System.DateTime> startDate,
                    [Parameter(Name = "EndDate", DbType = "DateTime")] System.Nullable<System.DateTime> endDate,
                    [Parameter(Name = "OccurrenceType", DbType = "TinyInt")] System.Nullable<byte> occurrenceType,
                    [Parameter(Name = "TotalOccurrence", DbType = "Int")] System.Nullable<int> totalOccurrence,
                    [Parameter(Name = "WeekDays", DbType = "TinyInt")] System.Nullable<byte> weekDays,
                    [Parameter(Name = "MonthDuration", DbType = "Int")] System.Nullable<int> monthDuration,
                    [Parameter(Name = "DateofMonth", DbType = "TinyInt")] System.Nullable<byte> dateofMonth,
                    [Parameter(Name = "NextOcc", DbType = "DateTime")] System.Nullable<System.DateTime> nextOcc,
                    [Parameter(Name = "IsActive", DbType = "Bit")] System.Nullable<bool> isActive,
                    [Parameter(Name = "ExecutedTime", DbType = "DateTime")] System.Nullable<System.DateTime> executedTime,
                    [Parameter(Name = "Status", DbType = "Int")] System.Nullable<int> status)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), dropScheduleHistoryID, scheduleID, name, scheduleTime, stackerLevel, scheduleType, startDate, endDate, occurrenceType, totalOccurrence, weekDays, monthDuration, dateofMonth, nextOcc, isActive, executedTime, status);
            return ((int)(result.ReturnValue));
        }


        [Function(Name = "dbo.rsp_GetManualDropAlertDetails")]
        public ISingleResult<rsp_GetManualDropAlertDetailsResult> GetManualDropAlertDetails([Parameter(Name = "Region", DbType = "Int")] System.Nullable<int> region, [Parameter(Name = "SiteID", DbType = "Int")] System.Nullable<int> siteID, [Parameter(Name = "StackerLevel", DbType = "Int")] System.Nullable<int> stackerLevel, [Parameter(Name = "StaffID", DbType = "Int")] System.Nullable<int> staffID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), region, siteID, stackerLevel, staffID);
            return ((ISingleResult<rsp_GetManualDropAlertDetailsResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.usp_DeleteDropSchedule")]
        public int DeleteDropSchedule([Parameter(Name = "ScheduleID", DbType = "Int")] System.Nullable<int> scheduleID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), scheduleID);
            return ((int)(result.ReturnValue));
        }       
    }


    public partial class rsp_GetRegionResult
    {

        private int _Sub_Company_Region_ID;

        private string _Sub_Company_Region_Name;

        public rsp_GetRegionResult()
        {
        }

        [Column(Storage = "_Sub_Company_Region_ID", DbType = "Int NOT NULL")]
        public int Sub_Company_Region_ID
        {
            get
            {
                return this._Sub_Company_Region_ID;
            }
            set
            {
                if ((this._Sub_Company_Region_ID != value))
                {
                    this._Sub_Company_Region_ID = value;
                }
            }
        }

        [Column(Storage = "_Sub_Company_Region_Name", DbType = "VarChar(50)")]
        public string Sub_Company_Region_Name
        {
            get
            {
                return this._Sub_Company_Region_Name;
            }
            set
            {
                if ((this._Sub_Company_Region_Name != value))
                {
                    this._Sub_Company_Region_Name = value;
                }
            }
        }
    }

    public partial class rsp_GetSiteOnRegionResult
    {

        private int _Site_ID;

        private string _Site_Name;

        public rsp_GetSiteOnRegionResult()
        {
        }

        [Column(Storage = "_Site_ID", DbType = "Int NOT NULL")]
        public int Site_ID
        {
            get
            {
                return this._Site_ID;
            }
            set
            {
                if ((this._Site_ID != value))
                {
                    this._Site_ID = value;
                }
            }
        }

        [Column(Storage = "_Site_Name", DbType = "VarChar(50)")]
        public string Site_Name
        {
            get
            {
                return this._Site_Name;
            }
            set
            {
                if ((this._Site_Name != value))
                {
                    this._Site_Name = value;
                }
            }
        }
    }

    public partial class rsp_GetDropScheduleAutoResult
    {

        private int _ScheduleID;

        private string _ScheduleName;

        private System.Nullable<System.DateTime> _ScheduleTime;

        private System.Nullable<byte> _StackerLevel;

        private System.Nullable<byte> _ScheduleType;

        private System.Nullable<System.DateTime> _StartDate;

        private System.Nullable<System.DateTime> _EndDate;

        private byte _OccurrenceType;

        private System.Nullable<int> _TotalOccurrence;

        private System.Nullable<byte> _WeekDays;

        private System.Nullable<int> _MonthDuration;

        private System.Nullable<byte> _DateofMonth;

        private System.Nullable<System.DateTime> _NextOcc;

        private System.Nullable<bool> _IsActive;

        public rsp_GetDropScheduleAutoResult()
        {
        }

        [Column(Storage = "_ScheduleID", DbType = "Int NOT NULL")]
        public int ScheduleID
        {
            get
            {
                return this._ScheduleID;
            }
            set
            {
                if ((this._ScheduleID != value))
                {
                    this._ScheduleID = value;
                }
            }
        }

        [Column(Storage = "_ScheduleName", DbType = "VarChar(50)")]
        public string ScheduleName
        {
            get
            {
                return this._ScheduleName;
            }
            set
            {
                if ((this._ScheduleName != value))
                {
                    this._ScheduleName = value;
                }
            }
        }

        [Column(Storage = "_ScheduleTime", DbType = "DateTime")]
        public System.Nullable<System.DateTime> ScheduleTime
        {
            get
            {
                return this._ScheduleTime;
            }
            set
            {
                if ((this._ScheduleTime != value))
                {
                    this._ScheduleTime = value;
                }
            }
        }

        [Column(Storage = "_StackerLevel", DbType = "TinyInt")]
        public System.Nullable<byte> StackerLevel
        {
            get
            {
                return this._StackerLevel;
            }
            set
            {
                if ((this._StackerLevel != value))
                {
                    this._StackerLevel = value;
                }
            }
        }

        [Column(Storage = "_ScheduleType", DbType = "TinyInt")]
        public System.Nullable<byte> ScheduleType
        {
            get
            {
                return this._ScheduleType;
            }
            set
            {
                if ((this._ScheduleType != value))
                {
                    this._ScheduleType = value;
                }
            }
        }

        [Column(Storage = "_StartDate", DbType = "DateTime")]
        public System.Nullable<System.DateTime> StartDate
        {
            get
            {
                return this._StartDate;
            }
            set
            {
                if ((this._StartDate != value))
                {
                    this._StartDate = value;
                }
            }
        }

        [Column(Storage = "_EndDate", DbType = "DateTime")]
        public System.Nullable<System.DateTime> EndDate
        {
            get
            {
                return this._EndDate;
            }
            set
            {
                if ((this._EndDate != value))
                {
                    this._EndDate = value;
                }
            }
        }

        [Column(Storage = "_OccurrenceType", DbType = "TinyInt NOT NULL")]
        public byte OccurrenceType
        {
            get
            {
                return this._OccurrenceType;
            }
            set
            {
                if ((this._OccurrenceType != value))
                {
                    this._OccurrenceType = value;
                }
            }
        }

        [Column(Storage = "_TotalOccurrence", DbType = "Int")]
        public System.Nullable<int> TotalOccurrence
        {
            get
            {
                return this._TotalOccurrence;
            }
            set
            {
                if ((this._TotalOccurrence != value))
                {
                    this._TotalOccurrence = value;
                }
            }
        }

        [Column(Storage = "_WeekDays", DbType = "TinyInt")]
        public System.Nullable<byte> WeekDays
        {
            get
            {
                return this._WeekDays;
            }
            set
            {
                if ((this._WeekDays != value))
                {
                    this._WeekDays = value;
                }
            }
        }

        [Column(Storage = "_MonthDuration", DbType = "Int")]
        public System.Nullable<int> MonthDuration
        {
            get
            {
                return this._MonthDuration;
            }
            set
            {
                if ((this._MonthDuration != value))
                {
                    this._MonthDuration = value;
                }
            }
        }

        [Column(Storage = "_DateofMonth", DbType = "TinyInt")]
        public System.Nullable<byte> DateofMonth
        {
            get
            {
                return this._DateofMonth;
            }
            set
            {
                if ((this._DateofMonth != value))
                {
                    this._DateofMonth = value;
                }
            }
        }

        [Column(Storage = "_NextOcc", DbType = "DateTime")]
        public System.Nullable<System.DateTime> NextOcc
        {
            get
            {
                return this._NextOcc;
            }
            set
            {
                if ((this._NextOcc != value))
                {
                    this._NextOcc = value;
                }
            }
        }

        [Column(Storage = "_IsActive", DbType = "Bit")]
        public System.Nullable<bool> IsActive
        {
            get
            {
                return this._IsActive;
            }
            set
            {
                if ((this._IsActive != value))
                {
                    this._IsActive = value;
                }
            }
        }
    }

    public partial class rsp_GetManualDropAlertDetailsResult
    {

        private int _INSTALLATION_ID;

        private System.Nullable<int> _BAR_POSITION_ID;

        private System.Nullable<int> _MACHINE_ID;

        private string _VERSIONNAME;

        private string _BAR_POSITION_NAME;

        private int _SITE_ID;

        private string _SUB_COMPANY_AREA_NAME;

        private string _SITE_NAME;

        private string _SITE_CODE;

        private string _SUB_COMPANY_REGION_NAME;

        private int _SUB_COMPANY_ID;

        private string _Sub_Company_Name;

        private int _COMPANY_ID;

        private string _COMPANY_NAME;

        private string _AssetNumber;

        private int _STACKER_ID;

        private int _STACKERSIZE;

        private System.Nullable<int> _TOTALQTY;

        private System.Nullable<int> _PERCENTAGE;

        private string _EmployeeName;

        public rsp_GetManualDropAlertDetailsResult()
        {
        }

        [Column(Storage = "_INSTALLATION_ID", DbType = "Int NOT NULL")]
        public int INSTALLATION_ID
        {
            get
            {
                return this._INSTALLATION_ID;
            }
            set
            {
                if ((this._INSTALLATION_ID != value))
                {
                    this._INSTALLATION_ID = value;
                }
            }
        }

        [Column(Storage = "_BAR_POSITION_ID", DbType = "Int")]
        public System.Nullable<int> BAR_POSITION_ID
        {
            get
            {
                return this._BAR_POSITION_ID;
            }
            set
            {
                if ((this._BAR_POSITION_ID != value))
                {
                    this._BAR_POSITION_ID = value;
                }
            }
        }

        [Column(Storage = "_MACHINE_ID", DbType = "Int")]
        public System.Nullable<int> MACHINE_ID
        {
            get
            {
                return this._MACHINE_ID;
            }
            set
            {
                if ((this._MACHINE_ID != value))
                {
                    this._MACHINE_ID = value;
                }
            }
        }

        [Column(Storage = "_VERSIONNAME", DbType = "VarChar(20)")]
        public string VERSIONNAME
        {
            get
            {
                return this._VERSIONNAME;
            }
            set
            {
                if ((this._VERSIONNAME != value))
                {
                    this._VERSIONNAME = value;
                }
            }
        }

        [Column(Storage = "_BAR_POSITION_NAME", DbType = "VarChar(50)")]
        public string BAR_POSITION_NAME
        {
            get
            {
                return this._BAR_POSITION_NAME;
            }
            set
            {
                if ((this._BAR_POSITION_NAME != value))
                {
                    this._BAR_POSITION_NAME = value;
                }
            }
        }

        [Column(Storage = "_SITE_ID", DbType = "Int NOT NULL")]
        public int SITE_ID
        {
            get
            {
                return this._SITE_ID;
            }
            set
            {
                if ((this._SITE_ID != value))
                {
                    this._SITE_ID = value;
                }
            }
        }

        [Column(Storage = "_SUB_COMPANY_AREA_NAME", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string SUB_COMPANY_AREA_NAME
        {
            get
            {
                return this._SUB_COMPANY_AREA_NAME;
            }
            set
            {
                if ((this._SUB_COMPANY_AREA_NAME != value))
                {
                    this._SUB_COMPANY_AREA_NAME = value;
                }
            }
        }

        [Column(Storage = "_SITE_NAME", DbType = "VarChar(50)")]
        public string SITE_NAME
        {
            get
            {
                return this._SITE_NAME;
            }
            set
            {
                if ((this._SITE_NAME != value))
                {
                    this._SITE_NAME = value;
                }
            }
        }

        [Column(Storage = "_SITE_CODE", DbType = "VarChar(50)")]
        public string SITE_CODE
        {
            get
            {
                return this._SITE_CODE;
            }
            set
            {
                if ((this._SITE_CODE != value))
                {
                    this._SITE_CODE = value;
                }
            }
        }

        [Column(Storage = "_SUB_COMPANY_REGION_NAME", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string SUB_COMPANY_REGION_NAME
        {
            get
            {
                return this._SUB_COMPANY_REGION_NAME;
            }
            set
            {
                if ((this._SUB_COMPANY_REGION_NAME != value))
                {
                    this._SUB_COMPANY_REGION_NAME = value;
                }
            }
        }

        [Column(Storage = "_SUB_COMPANY_ID", DbType = "Int NOT NULL")]
        public int SUB_COMPANY_ID
        {
            get
            {
                return this._SUB_COMPANY_ID;
            }
            set
            {
                if ((this._SUB_COMPANY_ID != value))
                {
                    this._SUB_COMPANY_ID = value;
                }
            }
        }

        [Column(Storage = "_Sub_Company_Name", DbType = "VarChar(50)")]
        public string Sub_Company_Name
        {
            get
            {
                return this._Sub_Company_Name;
            }
            set
            {
                if ((this._Sub_Company_Name != value))
                {
                    this._Sub_Company_Name = value;
                }
            }
        }

        [Column(Storage = "_COMPANY_ID", DbType = "Int NOT NULL")]
        public int COMPANY_ID
        {
            get
            {
                return this._COMPANY_ID;
            }
            set
            {
                if ((this._COMPANY_ID != value))
                {
                    this._COMPANY_ID = value;
                }
            }
        }

        [Column(Storage = "_COMPANY_NAME", DbType = "VarChar(50)")]
        public string COMPANY_NAME
        {
            get
            {
                return this._COMPANY_NAME;
            }
            set
            {
                if ((this._COMPANY_NAME != value))
                {
                    this._COMPANY_NAME = value;
                }
            }
        }

        [Column(Storage = "_AssetNumber", DbType = "VarChar(50)")]
        public string AssetNumber
        {
            get
            {
                return this._AssetNumber;
            }
            set
            {
                if ((this._AssetNumber != value))
                {
                    this._AssetNumber = value;
                }
            }
        }

        [Column(Storage = "_STACKER_ID", DbType = "Int NOT NULL")]
        public int STACKER_ID
        {
            get
            {
                return this._STACKER_ID;
            }
            set
            {
                if ((this._STACKER_ID != value))
                {
                    this._STACKER_ID = value;
                }
            }
        }

        [Column(Storage = "_STACKERSIZE", DbType = "Int NOT NULL")]
        public int STACKERSIZE
        {
            get
            {
                return this._STACKERSIZE;
            }
            set
            {
                if ((this._STACKERSIZE != value))
                {
                    this._STACKERSIZE = value;
                }
            }
        }

        [Column(Storage = "_TOTALQTY", DbType = "Int")]
        public System.Nullable<int> TOTALQTY
        {
            get
            {
                return this._TOTALQTY;
            }
            set
            {
                if ((this._TOTALQTY != value))
                {
                    this._TOTALQTY = value;
                }
            }
        }

        [Column(Storage = "_PERCENTAGE", DbType = "Int")]
        public System.Nullable<int> PERCENTAGE
        {
            get
            {
                return this._PERCENTAGE;
            }
            set
            {
                if ((this._PERCENTAGE != value))
                {
                    this._PERCENTAGE = value;
                }
            }
        }

        [Column(Storage = "_EmployeeName", DbType = "NVarChar(100) NOT NULL", CanBeNull = false)]
        public string EmployeeName
        {
            get
            {
                return this._EmployeeName;
            }
            set
            {
                if ((this._EmployeeName != value))
                {
                    this._EmployeeName = value;
                }
            }
        }
    }
}
