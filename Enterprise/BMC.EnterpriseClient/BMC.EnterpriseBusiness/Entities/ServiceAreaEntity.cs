using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.EnterpriseBusiness.Entities
{
    public class ServiceAreaEntity
    {
        private int _Service_Area_ID;

        private System.Nullable<int> _Depot_ID;

        private string _Service_Area_Name;

        private string _Service_Area_Description;

        private string _Service_Area_Notes;

        public ServiceAreaEntity()
        {
        }

        public int Service_Area_ID
        {
            get
            {
                return this._Service_Area_ID;
            }
            set
            {
                if ((this._Service_Area_ID != value))
                {
                    this._Service_Area_ID = value;
                }
            }
        }

        public System.Nullable<int> Depot_ID
        {
            get
            {
                return this._Depot_ID;
            }
            set
            {
                if ((this._Depot_ID != value))
                {
                    this._Depot_ID = value;
                }
            }
        }

        public string Service_Area_Name
        {
            get
            {
                return this._Service_Area_Name;
            }
            set
            {
                if ((this._Service_Area_Name != value))
                {
                    this._Service_Area_Name = value;
                }
            }
        }

        public string Service_Area_Description
        {
            get
            {
                return this._Service_Area_Description;
            }
            set
            {
                if ((this._Service_Area_Description != value))
                {
                    this._Service_Area_Description = value;
                }
            }
        }

        public string Service_Area_Notes
        {
            get
            {
                return this._Service_Area_Notes;
            }
            set
            {
                if ((this._Service_Area_Notes != value))
                {
                    this._Service_Area_Notes = value;
                }
            }
        }
    }
}

