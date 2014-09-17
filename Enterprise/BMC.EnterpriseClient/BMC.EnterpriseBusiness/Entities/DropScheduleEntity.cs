using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace BMC.EnterpriseBusiness.Entities
{
    public partial class RegionNameModel
    {
        private int _Sub_Company_Region_ID;

        private string _Sub_Company_Region_Name;

        public RegionNameModel()
        {
        }

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
    public partial class SiteDetailsModel
    {
        private int _Site_ID;

        private string _Site_Name;

        public SiteDetailsModel()
        {
        }

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

    public enum DropAlertTypes : int
    {
        Automatic = 1,
        Manual=2
    }

    public enum ScheduleOccurances : int
    {
        Daily = 1,
        Weekly,
        Monthly
    }

    public enum EndOptions : int
    {
        NoEndDate = 1,
        EndAfterOccurance,
        EndByDate
    }

    public partial class DropScheduleEntity
    {
        public int ScheduleId { get; set; }
        public string ScheduleName { get; set; }            
        public DateTime? ScheduleTime { get; set; }
        public byte StackerLevelPercentage { get; set; }
        public ScheduleOccurances ScheduleOccurance { get; set; }        
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }//Auto Calculate
        public EndOptions EndOption { get; set; }
        public int TotalOccurances { get; set; }
        public int WeekDays { get; set; }
        public string SelectedWeekDays { get; set; }
        public int DayOfMonth { get; set; }
        public int MonthDuration { get; set; }
        public DateTime? NextOcc { get; set; }
        public bool IsActive { get; set; }
        public DropAlertTypes DropAlertType { get; set; }
        public int RegionId{ get; set; }
        public int SiteId { get; set; }
    }

    public partial class ManualDropScheduleEntity
    {             

        private int _INSTALLATION_ID;
		
		private System.Nullable<int> _BAR_POSITION_ID;
		
		private System.Nullable<int> _MACHINE_ID;
		
        private string _VERSION_NAME;
		
		private string _BAR_POSITION_NAME;
		
		private int _SITE_ID;

        private string _Sub_Company_Area_Name;
		
		private string _SITE_NAME;
		
		private string _SITE_CODE;

        private string _Sub_Company_Region_Name;
		
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
		
		public ManualDropScheduleEntity()
		{
		}
		
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
		
        public string VERSION_NAME
        {
            get
            {
                return this._VERSION_NAME;
            }
            set
            {
                if ((this._VERSION_NAME != value))
                {
                    this._VERSION_NAME = value;
                }
            }
        }
		
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

        public string Sub_Company_Area_Name
        {
            get
            {
                return this._Sub_Company_Area_Name;
            }
            set
            {
                if ((this._Sub_Company_Area_Name != value))
                {
                    this._Sub_Company_Area_Name = value;
                }
            }
        }
		
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

        public XElement ToString(Int32 dropScheduleId, String dropType, DateTime dropDate)
        {
            //StringBuilder build = new StringBuilder(string.Empty);
            //build.Append(String.Format("<{0}>{1}</{0}>\r\n","Source","DROP"));
            //build.Append(String.Format("<{0}>{1}</{0}>\r\n", "BMCVersion", this.VERSION_NAME));
            //build.Append("<ExceptionCode>001</ExceptionCode>\r\n");
            //build.Append("<OperatorId>000</OperatorId>\r\n");
            //build.Append(String.Format("<{0}>{1}</{0}>\r\n", "SubCode", ""));
            //build.Append(String.Format("<{0}>{1}</{0}>\r\n", "Company", this.COMPANY_NAME));
            //build.Append(String.Format("<{0}>{1}</{0}>\r\n", "Region", this.Sub_Company_Region_Name));
            //build.Append(String.Format("<{0}>{1}</{0}>\r\n", "Area", this.Sub_Company_Area_Name));
            //build.Append(String.Format("<{0}>{1}</{0}>\r\n", "SiteId", this.SITE_CODE));
            //build.Append(String.Format("<{0}>{1}</{0}>\r\n", "SiteName", this.SITE_NAME));
            //build.Append(String.Format("<{0}>{1}</{0}>\r\n", "Asset", this.AssetNumber));   
            //build.Append(String.Format("<{0}>{1}</{0}>\r\n", "Stand", this.BAR_POSITION_NAME));
            //build.Append(String.Format("<{0}>{1}</{0}>\r\n", "DropType", dropType));
            //build.Append(String.Format("<{0}>{1}</{0}>\r\n", "DropDate", dropDate.ToString("yyyy-MM-dd HH:mm:ss")));
            //build.Append(String.Format("<{0}>{1}</{0}>\r\n", "DropPositionsList", this.BAR_POSITION_NAME));
            //build.Append(String.Format("<{0}>{1}</{0}>\r\n", "DropScheduleId", dropScheduleId.ToString()));
            //build.Append(String.Format("<{0}>{1}</{0}>\r\n", "BatchNumber", ""));
            //build.Append(String.Format("<{0}>{1}</{0}>\r\n", "EmployeeCardNumber", ""));
            //build.Append(String.Format("<{0}>{1}</{0}>\r\n", "EmployeeName", this.EmployeeName));
            //build.Append(String.Format("<{0}>{1}</{0}>\r\n", "MessageDateTime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
           // string temp = String.Format("<{0}>\r\n{1}</{0}>","BMCRequest", build.ToString());

            XElement xml_stack =
                          new XElement("BMCRequest",
                          new XElement("Source", "DROP"),
                          new XElement("BMCVersion", this.VERSION_NAME),
                          new XElement("ExceptionCode", "001"),
                          new XElement("OperatorId", "000"),
                          new XElement("SubCode", ""),
                          new XElement("Company", this.COMPANY_NAME),
                          new XElement("Region", this.Sub_Company_Region_Name),
                          new XElement("Area", this.Sub_Company_Area_Name),
                          new XElement("SiteId", this.SITE_CODE),
                          new XElement("SiteName", this.SITE_NAME),
                          new XElement("Asset", this.AssetNumber),
                          new XElement("Stand", this.BAR_POSITION_NAME),
                          new XElement("DropType", dropType),
                          new XElement("DropDate", dropDate.ToString("yyyy-MM-dd HH:mm:ss")),
                          new XElement("DropPositionsList", this.BAR_POSITION_NAME),
                          new XElement("DropScheduleId", dropScheduleId.ToString()),
                          new XElement("BatchNumber", ""),
                          new XElement("EmployeeCardNumber", ""),
                          new XElement("EmployeeName", this.EmployeeName),
                          new XElement("MessageDateTime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
                          
            return xml_stack;
	    }
	}
	
    public partial class SiteRegion
    {
        public String RegionName { get; set; }
        public int RegionID { get; set; }
        public int SiteId { get; set; }
        public byte ManualStackLevelPercent { get; set; }
    }
}
