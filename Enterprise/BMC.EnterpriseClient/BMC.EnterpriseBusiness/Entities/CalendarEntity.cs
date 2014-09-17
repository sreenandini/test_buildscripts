using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.EnterpriseBusiness.Entities
{
    #region EntityClass
    public partial class CalendarEntity
    {

        private int _Calendar_ID;

        private string _Calendar_Description;

        private DateTime _Calendar_Year_Start_Date;

        private DateTime _Calendar_Year_End_Date;

        private int _IsCalendarCreatedUsingAutoCalendar;

        private string _CalendarBasedOn;

        public CalendarEntity()
        {
        }


        public int Calendar_ID
        {
            get
            {
                return this._Calendar_ID;
            }
            set
            {
                if ((this._Calendar_ID != value))
                {
                    this._Calendar_ID = value;
                }
            }
        }


        public string Calendar_Description
        {
            get
            {
                return this._Calendar_Description;
            }
            set
            {
                if ((this._Calendar_Description != value))
                {
                    this._Calendar_Description = value;
                }
            }
        }


        public DateTime Calendar_Year_Start_Date
        {
            get
            {
                return this._Calendar_Year_Start_Date;
            }
            set
            {
                if ((this._Calendar_Year_Start_Date != value))
                {
                    this._Calendar_Year_Start_Date = value;
                }
            }
        }


        public DateTime Calendar_Year_End_Date
        {
            get
            {
                return this._Calendar_Year_End_Date;
            }
            set
            {
                if ((this._Calendar_Year_End_Date != value))
                {
                    this._Calendar_Year_End_Date = value;
                }
            }
        }

        public int IsCalendarCreatedUsingAutoCalendar
        {
            get
            {
                return this._IsCalendarCreatedUsingAutoCalendar;
            }
            set
            {
                if ((this._IsCalendarCreatedUsingAutoCalendar != value))
                {
                    this._IsCalendarCreatedUsingAutoCalendar = value;
                }
            }
        }

        public string CalendarBasedOn
        {
            get
            {
                return this._CalendarBasedOn;
            }
            set
            {
                if ((this._CalendarBasedOn != value))
                {
                    this._CalendarBasedOn = value;
                }
            }
        }
    }

    public partial class GetCalendarListEntity
    {

        private int _Calendar_ID;

        private string _Calendar_Description;

        private DateTime _Calendar_Year_Start_Date;

        private DateTime _Calendar_Year_End_Date;

        private int _IsCompleteCalendar;

        public GetCalendarListEntity()
        {
        }

        public int Calendar_ID
        {
            get
            {
                return this._Calendar_ID;
            }
            set
            {
                if ((this._Calendar_ID != value))
                {
                    this._Calendar_ID = value;
                }
            }
        }

        public string Calendar_Description
        {
            get
            {
                return this._Calendar_Description;
            }
            set
            {
                if ((this._Calendar_Description != value))
                {
                    this._Calendar_Description = value;
                }
            }
        }

        public DateTime Calendar_Year_Start_Date
        {
            get
            {
                return this._Calendar_Year_Start_Date;
            }
            set
            {
                if ((this._Calendar_Year_Start_Date != value))
                {
                    this._Calendar_Year_Start_Date = value;
                }
            }
        }

        public DateTime Calendar_Year_End_Date
        {
            get
            {
                return this._Calendar_Year_End_Date;
            }
            set
            {
                if ((this._Calendar_Year_End_Date != value))
                {
                    this._Calendar_Year_End_Date = value;
                }
            }
        }

        public int IsCompleteCalendar
        {
            get
            {
                return this._IsCompleteCalendar;
            }
            set
            {
                if ((this._IsCompleteCalendar != value))
                {
                    this._IsCompleteCalendar = value;
                }
            }
        }
    }

    public partial class CalendarPeriod
    {

        private int _Calendar_Period_ID;

        private System.Nullable<int> _Calendar_ID;

        private System.Nullable<int> _Calendar_Period_Number;

        private DateTime _Calendar_Period_Start_Date;

        private DateTime _Calendar_Period_End_Date;

        public CalendarPeriod()
        {
        }


        public int Calendar_Period_ID
        {
            get
            {
                return this._Calendar_Period_ID;
            }
            set
            {
                if ((this._Calendar_Period_ID != value))
                {
                    this._Calendar_Period_ID = value;
                }
            }
        }


        public System.Nullable<int> Calendar_ID
        {
            get
            {
                return this._Calendar_ID;
            }
            set
            {
                if ((this._Calendar_ID != value))
                {
                    this._Calendar_ID = value;
                }
            }
        }


        public System.Nullable<int> Calendar_Period_Number
        {
            get
            {
                return this._Calendar_Period_Number;
            }
            set
            {
                if ((this._Calendar_Period_Number != value))
                {
                    this._Calendar_Period_Number = value;
                }
            }
        }


        public DateTime Calendar_Period_Start_Date
        {
            get
            {
                return this._Calendar_Period_Start_Date;
            }
            set
            {
                if ((this._Calendar_Period_Start_Date != value))
                {
                    this._Calendar_Period_Start_Date = value;
                }
            }
        }


        public DateTime Calendar_Period_End_Date
        {
            get
            {
                return this._Calendar_Period_End_Date;
            }
            set
            {
                if ((this._Calendar_Period_End_Date != value))
                {
                    this._Calendar_Period_End_Date = value;
                }
            }
        }
    }
    public partial class CalendarWeek
    {

        private int _Calendar_Week_ID;

        private System.Nullable<int> _Calendar_ID;

        private System.Nullable<int> _Calendar_Week_Number;

        private DateTime _Calendar_Week_Start_Date;

        private DateTime _Calendar_Week_End_Date;

        private int _Calendar_Period_ID;

        public CalendarWeek()
        {
        }


        public int Calendar_Week_ID
        {
            get
            {
                return this._Calendar_Week_ID;
            }
            set
            {
                if ((this._Calendar_Week_ID != value))
                {
                    this._Calendar_Week_ID = value;
                }
            }
        }


        public System.Nullable<int> Calendar_ID
        {
            get
            {
                return this._Calendar_ID;
            }
            set
            {
                if ((this._Calendar_ID != value))
                {
                    this._Calendar_ID = value;
                }
            }
        }


        public System.Nullable<int> Calendar_Week_Number
        {
            get
            {
                return this._Calendar_Week_Number;
            }
            set
            {
                if ((this._Calendar_Week_Number != value))
                {
                    this._Calendar_Week_Number = value;
                }
            }
        }


        public DateTime Calendar_Week_Start_Date
        {
            get
            {
                return this._Calendar_Week_Start_Date;
            }
            set
            {
                if ((this._Calendar_Week_Start_Date != value))
                {
                    this._Calendar_Week_Start_Date = value;
                }
            }
        }


        public DateTime Calendar_Week_End_Date
        {
            get
            {
                return this._Calendar_Week_End_Date;
            }
            set
            {
                if ((this._Calendar_Week_End_Date != value))
                {
                    this._Calendar_Week_End_Date = value;
                }
            }
        }


        public int Calendar_Period_ID
        {
            get
            {
                return this._Calendar_Period_ID;
            }
            set
            {
                if ((this._Calendar_Period_ID != value))
                {
                    this._Calendar_Period_ID = value;
                }
            }
        }
    }
    public partial class CompanyCalEntity
    {
        private string _Company_Name;
        private int _Company_ID;
        private int? _Sub_Company_ID;
        private string _Sub_Company_Name;
        public string Company_Name
        {
            get
            {
                return this._Company_Name;
            }
            set
            {
                if ((this._Company_Name != value))
                {
                    this._Company_Name = value;
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
        public int Company_ID
        {
            get
            {
                return this._Company_ID;
            }
            set
            {
                if ((this._Company_ID != value))
                {
                    this._Company_ID = value;
                }
            }
        }
        public int? Sub_Company_ID
        {
            get
            {
                return this._Sub_Company_ID;
            }
            set
            {
                if ((this._Sub_Company_ID != value))
                {
                    this._Sub_Company_ID = value;
                }
            }
        }
    }
  
    public partial class Operator_Calendar
    {

        private int _Operator_Calendar_ID;

        private System.Nullable<int> _Operator_ID;

        private System.Nullable<int> _Calendar_ID;

        private string _Operator_Name;

        private System.Nullable<bool> _Operator_Calendar_Active;

        public Operator_Calendar()
        {
        }

        public string Operator_Name
        {
            get
            {
                return this._Operator_Name;
            }
            set
            {
                if ((this._Operator_Name != value))
                {
                    this._Operator_Name = value;
                }
            }
        }

        public int Operator_Calendar_ID
        {
            get
            {
                return this._Operator_Calendar_ID;
            }
            set
            {
                if ((this._Operator_Calendar_ID != value))
                {
                    this._Operator_Calendar_ID = value;
                }
            }
        }


        public System.Nullable<int> Operator_ID
        {
            get
            {
                return this._Operator_ID;
            }
            set
            {
                if ((this._Operator_ID != value))
                {
                    this._Operator_ID = value;
                }
            }
        }


        public System.Nullable<int> Calendar_ID
        {
            get
            {
                return this._Calendar_ID;
            }
            set
            {
                if ((this._Calendar_ID != value))
                {
                    this._Calendar_ID = value;
                }
            }
        }


        public System.Nullable<bool> Operator_Calendar_Active
        {
            get
            {
                return this._Operator_Calendar_Active;
            }
            set
            {
                if ((this._Operator_Calendar_Active != value))
                {
                    this._Operator_Calendar_Active = value;
                }
            }
        }
    }
    public partial class SubCompanyCalendar
    {

        private int _Sub_Company_Calendar_ID;

        private System.Nullable<int> _Sub_Company_ID;

        private System.Nullable<int> _Calendar_ID;

        private System.Nullable<bool> _Sub_Company_Calendar_Active;

        public SubCompanyCalendar()
        {
        }


        public int Sub_Company_Calendar_ID
        {
            get
            {
                return this._Sub_Company_Calendar_ID;
            }
            set
            {
                if ((this._Sub_Company_Calendar_ID != value))
                {
                    this._Sub_Company_Calendar_ID = value;
                }
            }
        }


        public System.Nullable<int> Sub_Company_ID
        {
            get
            {
                return this._Sub_Company_ID;
            }
            set
            {
                if ((this._Sub_Company_ID != value))
                {
                    this._Sub_Company_ID = value;
                }
            }
        }


        public System.Nullable<int> Calendar_ID
        {
            get
            {
                return this._Calendar_ID;
            }
            set
            {
                if ((this._Calendar_ID != value))
                {
                    this._Calendar_ID = value;
                }
            }
        }


        public System.Nullable<bool> Sub_Company_Calendar_Active
        {
            get
            {
                return this._Sub_Company_Calendar_Active;
            }
            set
            {
                if ((this._Sub_Company_Calendar_Active != value))
                {
                    this._Sub_Company_Calendar_Active = value;
                }
            }
        }
    }
    public partial class CurrentCalendarDetails
    {

        private System.Nullable<int> _Calendar_Week_Number;

        private System.Nullable<int> _Calendar_Period_Number;

        private DateTime _Calendar_Period_End_Date;

        public CurrentCalendarDetails()
        {
        }

        public System.Nullable<int> Calendar_Week_Number
        {
            get
            {
                return this._Calendar_Week_Number;
            }
            set
            {
                if ((this._Calendar_Week_Number != value))
                {
                    this._Calendar_Week_Number = value;
                }
            }
        }

        public System.Nullable<int> Calendar_Period_Number
        {
            get
            {
                return this._Calendar_Period_Number;
            }
            set
            {
                if ((this._Calendar_Period_Number != value))
                {
                    this._Calendar_Period_Number = value;
                }
            }
        }

        public DateTime Calendar_Period_End_Date
        {
            get
            {
                return this._Calendar_Period_End_Date;
            }
            set
            {
                if ((this._Calendar_Period_End_Date != value))
                {
                    this._Calendar_Period_End_Date = value;
                }
            }
        }
    }
    public partial class ExportCalendar
    {

        private int _Site_ID;

        private string _Site_Name;

        public ExportCalendar()
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
    public partial class Operator_Cal
    {
        private int _Operator_ID;
        private System.Nullable<int> _Calendar_ID;
        private string _Operator_Name;     

        public Operator_Cal()
        {
        }
        
        public int Operator_ID
        {
            get
            {
                return this._Operator_ID;
            }
            set
            {
                if ((this._Operator_ID != value))
                {
                    this._Operator_ID = value;
                }
            }
        }        
        public System.Nullable<int> Calendar_ID
        {
            get
            {
                return this._Calendar_ID;
            }
            set
            {
                if ((this._Calendar_ID != value))
                {
                    this._Calendar_ID = value;
                }
            }
        }        
        public string Operator_Name
        {
            get
            {
                return this._Operator_Name;
            }
            set
            {
                if ((this._Operator_Name != value))
                {
                    this._Operator_Name = value;
                }
            }
        }    
       
    }
    public partial class SubCompanyCal
    {

        private int _Sub_Company_ID;
        private string _Sub_Company_Name;       
        private System.Nullable<int> _Company_ID;        
        private System.Nullable<int> _Calendar_ID;   
      
        public SubCompanyCal()
        {
        }        

        public int Sub_Company_ID
        {
            get
            {
                return this._Sub_Company_ID;
            }
            set
            {
                if ((this._Sub_Company_ID != value))
                {
                    this._Sub_Company_ID = value;
                }
            }
        }      
        public System.Nullable<int> Company_ID
        {
            get
            {
                return this._Company_ID;
            }
            set
            {
                if ((this._Company_ID != value))
                {
                    this._Company_ID = value;
                }
            }
        }    
        public System.Nullable<int> Calendar_ID
        {
            get
            {
                return this._Calendar_ID;
            }
            set
            {
                if ((this._Calendar_ID != value))
                {
                    this._Calendar_ID = value;
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
   
    }

    public partial class GetSubCompanyCalendarActive
    {

        private int _Sub_Company_Calendar_ID;

        private int _Calendar_ID;

        private System.Nullable<bool> _Sub_Company_Calendar_Active;

        private string _Calendar_Description;

        private DateTime _Calendar_Year_Start_Date;

        private DateTime _Calendar_Year_End_Date;

        private string _Calendar_History;

        public GetSubCompanyCalendarActive()
        {
        }


        public int Sub_Company_Calendar_ID
        {
            get
            {
                return this._Sub_Company_Calendar_ID;
            }
            set
            {
                if ((this._Sub_Company_Calendar_ID != value))
                {
                    this._Sub_Company_Calendar_ID = value;
                }
            }
        }


        public int Calendar_ID
        {
            get
            {
                return this._Calendar_ID;
            }
            set
            {
                if ((this._Calendar_ID != value))
                {
                    this._Calendar_ID = value;
                }
            }
        }


        public System.Nullable<bool> Sub_Company_Calendar_Active
        {
            get
            {
                return this._Sub_Company_Calendar_Active;
            }
            set
            {
                if ((this._Sub_Company_Calendar_Active != value))
                {
                    this._Sub_Company_Calendar_Active = value;
                }
            }
        }


        public string Calendar_Description
        {
            get
            {
                return this._Calendar_Description;
            }
            set
            {
                if ((this._Calendar_Description != value))
                {
                    this._Calendar_Description = value;
                }
            }
        }


        public DateTime Calendar_Year_Start_Date
        {
            get
            {
                return this._Calendar_Year_Start_Date;
            }
            set
            {
                if ((this._Calendar_Year_Start_Date != value))
                {
                    this._Calendar_Year_Start_Date = value;
                }
            }
        }


        public DateTime Calendar_Year_End_Date
        {
            get
            {
                return this._Calendar_Year_End_Date;
            }
            set
            {
                if ((this._Calendar_Year_End_Date != value))
                {
                    this._Calendar_Year_End_Date = value;
                }
            }
        }


        public string Calendar_History
        {
            get
            {
                return this._Calendar_History;
            }
            set
            {
                if ((this._Calendar_History != value))
                {
                    this._Calendar_History = value;
                }
            }
        }
    }

    public partial class GetOperatorCalendarActive
    {

        private int _Operator_Calendar_ID;

        private int _Calendar_ID;

        private System.Nullable<bool> _Operator_Calendar_Active;

        private string _Calendar_Description;

        private DateTime _Calendar_Year_Start_Date;

        private DateTime _Calendar_Year_End_Date;

        private string _Calendar_History;

        public GetOperatorCalendarActive()
        {
        }


        public int Operator_Calendar_ID
        {
            get
            {
                return this._Operator_Calendar_ID;
            }
            set
            {
                if ((this._Operator_Calendar_ID != value))
                {
                    this._Operator_Calendar_ID = value;
                }
            }
        }


        public int Calendar_ID
        {
            get
            {
                return this._Calendar_ID;
            }
            set
            {
                if ((this._Calendar_ID != value))
                {
                    this._Calendar_ID = value;
                }
            }
        }


        public System.Nullable<bool> Operator_Calendar_Active
        {
            get
            {
                return this._Operator_Calendar_Active;
            }
            set
            {
                if ((this._Operator_Calendar_Active != value))
                {
                    this._Operator_Calendar_Active = value;
                }
            }
        }


        public string Calendar_Description
        {
            get
            {
                return this._Calendar_Description;
            }
            set
            {
                if ((this._Calendar_Description != value))
                {
                    this._Calendar_Description = value;
                }
            }
        }


        public DateTime Calendar_Year_Start_Date
        {
            get
            {
                return this._Calendar_Year_Start_Date;
            }
            set
            {
                if ((this._Calendar_Year_Start_Date != value))
                {
                    this._Calendar_Year_Start_Date = value;
                }
            }
        }


        public DateTime Calendar_Year_End_Date
        {
            get
            {
                return this._Calendar_Year_End_Date;
            }
            set
            {
                if ((this._Calendar_Year_End_Date != value))
                {
                    this._Calendar_Year_End_Date = value;
                }
            }
        }


        public string Calendar_History
        {
            get
            {
                return this._Calendar_History;
            }
            set
            {
                if ((this._Calendar_History != value))
                {
                    this._Calendar_History = value;
                }
            }
        }
    }
    #endregion EntityClass
}





