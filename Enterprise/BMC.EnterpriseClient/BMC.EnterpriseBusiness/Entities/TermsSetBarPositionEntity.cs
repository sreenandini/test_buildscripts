﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.EnterpriseBusiness.Entities
{
    public partial class CompanyInfo
    {
        private int _Company_ID;
        private string _Company_Name;

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
    }

    public partial class SubCompanyInfo
    {
        private int _Sub_Company_ID;
        private string _Sub_Company_Name;

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

    public partial class SiteInfo
    {
        private int _Site_ID;
        private string _Site_Name;

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
}