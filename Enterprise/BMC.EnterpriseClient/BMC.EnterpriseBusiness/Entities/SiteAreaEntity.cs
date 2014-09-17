using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.EnterpriseBusiness.Entities
{
    public partial class SubCompayRegions
    {

        private int _Sub_Company_Region_ID;

        private string _Sub_Company_Region_Name;

        private string _Sub_Company_Region_Description;

        private System.Nullable<int> _Staff_ID;

        private string _Staff_Last_Name;

        private string _Staff_First_Name;

        public SubCompayRegions()
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

        public string Sub_Company_Region_Description
        {
            get
            {
                return this._Sub_Company_Region_Description;
            }
            set
            {
                if ((this._Sub_Company_Region_Description != value))
                {
                    this._Sub_Company_Region_Description = value;
                }
            }
        }

        public System.Nullable<int> Staff_ID
        {
            get
            {
                return this._Staff_ID;
            }
            set
            {
                if ((this._Staff_ID != value))
                {
                    this._Staff_ID = value;
                }
            }
        }

        public string Staff_Last_Name
        {
            get
            {
                return this._Staff_Last_Name;
            }
            set
            {
                if ((this._Staff_Last_Name != value))
                {
                    this._Staff_Last_Name = value;
                }
            }
        }

        public string Staff_First_Name
        {
            get
            {
                return this._Staff_First_Name;
            }
            set
            {
                if ((this._Staff_First_Name != value))
                {
                    this._Staff_First_Name = value;
                }
            }
        }
    }
}
