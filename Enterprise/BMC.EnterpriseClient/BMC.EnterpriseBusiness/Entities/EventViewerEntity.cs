using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.EnterpriseBusiness.Entities
{
    public class SiteEvent
    {

        private string _Site_name;

        private string _Position;

        private string _Game_Title;

        private System.Nullable<System.DateTime> _Date_and_time_of_event;

        private string _Details_of_the_event;

        private string _Description_of_event;

        private string _Event_Auto_Closed;

        public SiteEvent()
        {
        }

        public string Site_name
        {
            get
            {
                return this._Site_name;
            }
            set
            {
                if ((this._Site_name != value))
                {
                    this._Site_name = value;
                }
            }
        }

        public string Position
        {
            get
            {
                return this._Position;
            }
            set
            {
                if ((this._Position != value))
                {
                    this._Position = value;
                }
            }
        }

        public string Game_Title
        {
            get
            {
                return this._Game_Title;
            }
            set
            {
                if ((this._Game_Title != value))
                {
                    this._Game_Title = value;
                }
            }
        }

        public System.Nullable<System.DateTime> Date_and_time_of_event
        {
            get
            {
                return this._Date_and_time_of_event;
            }
            set
            {
                if ((this._Date_and_time_of_event != value))
                {
                    this._Date_and_time_of_event = value;
                }
            }
        }

        public string Description_of_event
        {
            get
            {
                return this._Description_of_event;
            }
            set
            {
                if ((this._Description_of_event != value))
                {
                    this._Description_of_event = value;
                }
            }
        }

        public string Details_of_the_event
        {
            get
            {
                return this._Details_of_the_event;
            }
            set
            {
                if ((this._Details_of_the_event != value))
                {
                    this._Details_of_the_event = value;
                }
            }
        }

        public string Event_Auto_Closed
        {
            get
            {
                return this._Event_Auto_Closed;
            }
            set
            {
                if ((this._Event_Auto_Closed != value))
                {
                    this._Event_Auto_Closed = value;
                }
            }
        }
    }

    public class EnterpriseEvent
    {

        private string _Site_Name;

        private System.Nullable<System.DateTime> _Evt_Datetime;

        private string _Description_of_event;

        private string _Details_of_the_event;

        public EnterpriseEvent()
        {
        }

        public string Site_name
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

        public System.Nullable<System.DateTime> Date_and_time_of_event
        {
            get
            {
                return this._Evt_Datetime;
            }
            set
            {
                if ((this._Evt_Datetime != value))
                {
                    this._Evt_Datetime = value;
                }
            }
        }

        public string Description_of_event
        {
            get
            {
                return this._Description_of_event;
            }
            set
            {
                if ((this._Description_of_event != value))
                {
                    this._Description_of_event = value;
                }
            }
        }

        public string Details_of_the_event
        {
            get
            {
                return this._Details_of_the_event;
            }
            set
            {
                if ((this._Details_of_the_event != value))
                {
                    this._Details_of_the_event = value;
                }
            }
        }
    }
}
