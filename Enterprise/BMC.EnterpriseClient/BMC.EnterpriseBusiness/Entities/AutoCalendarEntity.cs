using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.EnterpriseBusiness.Entities
{

    public class AutoCalendarResult
    {
        public List<GetAutoCalendarProfilesDetails> ProfilesDetails { get; set; }
        public List<GetAutoCalendarSubCompanyDetails> SCDetails { get; set; }
    }

    public partial class GetAutoCalendarProfiles
    {

        private int _AutoCalendarProfile_ID;

        private string _AutoCalendarProfile_Name;

        public GetAutoCalendarProfiles()
        {
        }

        public int AutoCalendarProfile_ID
        {
            get
            {
                return this._AutoCalendarProfile_ID;
            }
            set
            {
                if ((this._AutoCalendarProfile_ID != value))
                {
                    this._AutoCalendarProfile_ID = value;
                }
            }
        }

        public string AutoCalendarProfile_Name
        {
            get
            {
                return this._AutoCalendarProfile_Name;
            }
            set
            {
                if ((this._AutoCalendarProfile_Name != value))
                {
                    this._AutoCalendarProfile_Name = value;
                }
            }
        }
    }

    public partial class GetAutoCalendarProfilesDetails
    {
        private int _AutoCalendarProfile_ID;

        private string _AutoCalendarProfile_Name;

        private System.Nullable<bool> _IsAutoCalendarEnabled;

        private int _CalendarCreateBeforeDays;

        private int _CalendarAlertBefore;

        private int _CalendarAlertRecurrence;

        private System.Nullable<bool> _IsCalendarBasedOnDays;

        private int _NewCalendarDayID;

        private bool _SetNewCalendarActive;

        public GetAutoCalendarProfilesDetails()
        {
        }

        public int AutoCalendarProfile_ID
        {
            get
            {
                return this._AutoCalendarProfile_ID;
            }
            set
            {
                if ((this._AutoCalendarProfile_ID != value))
                {
                    this._AutoCalendarProfile_ID = value;
                }
            }
        }

        public string AutoCalendarProfile_Name
        {
            get
            {
                return this._AutoCalendarProfile_Name;
            }
            set
            {
                if ((this._AutoCalendarProfile_Name != value))
                {
                    this._AutoCalendarProfile_Name = value;
                }
            }
        }

        public System.Nullable<bool> IsAutoCalendarEnabled
        {
            get
            {
                return this._IsAutoCalendarEnabled;
            }
            set
            {
                if ((this._IsAutoCalendarEnabled != value))
                {
                    this._IsAutoCalendarEnabled = value;
                }
            }
        }

        public int CalendarCreateBeforeDays
        {
            get
            {
                return this._CalendarCreateBeforeDays;
            }
            set
            {
                if ((this._CalendarCreateBeforeDays != value))
                {
                    this._CalendarCreateBeforeDays = value;
                }
            }
        }

        public int CalendarAlertBefore
        {
            get
            {
                return this._CalendarAlertBefore;
            }
            set
            {
                if ((this._CalendarAlertBefore != value))
                {
                    this._CalendarAlertBefore = value;
                }
            }
        }

        public int CalendarAlertRecurrence
        {
            get
            {
                return this._CalendarAlertRecurrence;
            }
            set
            {
                if ((this._CalendarAlertRecurrence != value))
                {
                    this._CalendarAlertRecurrence = value;
                }
            }
        }

        public System.Nullable<bool> IsCalendarBasedOnDays
        {
            get
            {
                return this._IsCalendarBasedOnDays;
            }
            set
            {
                if ((this._IsCalendarBasedOnDays != value))
                {
                    this._IsCalendarBasedOnDays = value;
                }
            }
        }

        public int NewCalendarDayID
        {
            get
            {
                return this._NewCalendarDayID;
            }
            set
            {
                if ((this._NewCalendarDayID != value))
                {
                    this._NewCalendarDayID = value;
                }
            }
        }

        public bool SetNewCalendarActive
        {
            get
            {
                return this._SetNewCalendarActive;
            }
            set
            {
                if ((this._SetNewCalendarActive != value))
                {
                    this._SetNewCalendarActive = value;
                }
            }
        }
    }

    public partial class GetAutoCalendarSubCompanyDetails
    {
        private System.Nullable<int> _Sub_Company_ID;

        private string _Sub_Company_Name;

        private int _Profilestatus;

        public GetAutoCalendarSubCompanyDetails()
        {
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

        public int Profilestatus
        {
            get
            {
                return this._Profilestatus;
            }
            set
            {
                if ((this._Profilestatus != value))
                {
                    this._Profilestatus = value;
                }
            }
        }
    }

}