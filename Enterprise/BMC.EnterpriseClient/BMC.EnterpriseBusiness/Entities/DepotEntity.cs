using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.EnterpriseBusiness.Entities
{
    public class DepotEntity
    {
        private int _Depot_ID;

        private string _Depot_Name;

        private string _Depot_Address;

        private string _Depot_Postcode;

        private string _Depot_Contact_Name;

        private string _Depot_AMEDIS_Depot_Code;

        private System.Nullable<int> _Supplier_ID;

        private string _Depot_Reference;

        private System.Nullable<bool> _Depot_Service;

        private string _Depot_Financial_Code;

        private string _Depot_Account_Name;

        private string _Depot_Sort_Code;

        private string _Depot_Account_No;

        private string _Depot_Phone_Number;

        private string _Depot_Street_Number;

        private string _Depot_Province;

        private string _Depot_Municipality;

        private string _Depot_Cadastral_Code;

        private System.Nullable<int> _Depot_Area;

        private System.Nullable<int> _Depot_Location_Type;

        private System.Nullable<int> _Depot_Toponym;

        private System.Nullable<int> _Depot_Code;

        private System.Nullable<int> _Depot_Closed;

        private System.Nullable<System.DateTime> _Depot_ActivationDate;

        private System.Nullable<System.DateTime> _Depot_DeletionDate;

        private System.Nullable<System.DateTime> _Depot_LastUpdateDate;

        private System.Nullable<int> _Depot_Status;

        public DepotEntity()
        {
        }

        public int Depot_ID
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


        public string Depot_Name
        {
            get
            {
                return this._Depot_Name;
            }
            set
            {
                if ((this._Depot_Name != value))
                {
                    this._Depot_Name = value;
                }
            }
        }


        public string Depot_Address
        {
            get
            {
                return this._Depot_Address;
            }
            set
            {
                if ((this._Depot_Address != value))
                {
                    this._Depot_Address = value;
                }
            }
        }


        public string Depot_Postcode
        {
            get
            {
                return this._Depot_Postcode;
            }
            set
            {
                if ((this._Depot_Postcode != value))
                {
                    this._Depot_Postcode = value;
                }
            }
        }


        public string Depot_Contact_Name
        {
            get
            {
                return this._Depot_Contact_Name;
            }
            set
            {
                if ((this._Depot_Contact_Name != value))
                {
                    this._Depot_Contact_Name = value;
                }
            }
        }


        public string Depot_AMEDIS_Depot_Code
        {
            get
            {
                return this._Depot_AMEDIS_Depot_Code;
            }
            set
            {
                if ((this._Depot_AMEDIS_Depot_Code != value))
                {
                    this._Depot_AMEDIS_Depot_Code = value;
                }
            }
        }


        public System.Nullable<int> Supplier_ID
        {
            get
            {
                return this._Supplier_ID;
            }
            set
            {
                if ((this._Supplier_ID != value))
                {
                    this._Supplier_ID = value;
                }
            }
        }


        public string Depot_Reference
        {
            get
            {
                return this._Depot_Reference;
            }
            set
            {
                if ((this._Depot_Reference != value))
                {
                    this._Depot_Reference = value;
                }
            }
        }


        public System.Nullable<bool> Depot_Service
        {
            get
            {
                return this._Depot_Service;
            }
            set
            {
                if ((this._Depot_Service != value))
                {
                    this._Depot_Service = value;
                }
            }
        }


        public string Depot_Financial_Code
        {
            get
            {
                return this._Depot_Financial_Code;
            }
            set
            {
                if ((this._Depot_Financial_Code != value))
                {
                    this._Depot_Financial_Code = value;
                }
            }
        }


        public string Depot_Account_Name
        {
            get
            {
                return this._Depot_Account_Name;
            }
            set
            {
                if ((this._Depot_Account_Name != value))
                {
                    this._Depot_Account_Name = value;
                }
            }
        }


        public string Depot_Sort_Code
        {
            get
            {
                return this._Depot_Sort_Code;
            }
            set
            {
                if ((this._Depot_Sort_Code != value))
                {
                    this._Depot_Sort_Code = value;
                }
            }
        }


        public string Depot_Account_No
        {
            get
            {
                return this._Depot_Account_No;
            }
            set
            {
                if ((this._Depot_Account_No != value))
                {
                    this._Depot_Account_No = value;
                }
            }
        }


        public string Depot_Phone_Number
        {
            get
            {
                return this._Depot_Phone_Number;
            }
            set
            {
                if ((this._Depot_Phone_Number != value))
                {
                    this._Depot_Phone_Number = value;
                }
            }
        }


        public string Depot_Street_Number
        {
            get
            {
                return this._Depot_Street_Number;
            }
            set
            {
                if ((this._Depot_Street_Number != value))
                {
                    this._Depot_Street_Number = value;
                }
            }
        }


        public string Depot_Province
        {
            get
            {
                return this._Depot_Province;
            }
            set
            {
                if ((this._Depot_Province != value))
                {
                    this._Depot_Province = value;
                }
            }
        }


        public string Depot_Municipality
        {
            get
            {
                return this._Depot_Municipality;
            }
            set
            {
                if ((this._Depot_Municipality != value))
                {
                    this._Depot_Municipality = value;
                }
            }
        }


        public string Depot_Cadastral_Code
        {
            get
            {
                return this._Depot_Cadastral_Code;
            }
            set
            {
                if ((this._Depot_Cadastral_Code != value))
                {
                    this._Depot_Cadastral_Code = value;
                }
            }
        }


        public System.Nullable<int> Depot_Area
        {
            get
            {
                return this._Depot_Area;
            }
            set
            {
                if ((this._Depot_Area != value))
                {
                    this._Depot_Area = value;
                }
            }
        }


        public System.Nullable<int> Depot_Location_Type
        {
            get
            {
                return this._Depot_Location_Type;
            }
            set
            {
                if ((this._Depot_Location_Type != value))
                {
                    this._Depot_Location_Type = value;
                }
            }
        }


        public System.Nullable<int> Depot_Toponym
        {
            get
            {
                return this._Depot_Toponym;
            }
            set
            {
                if ((this._Depot_Toponym != value))
                {
                    this._Depot_Toponym = value;
                }
            }
        }


        public System.Nullable<int> Depot_Code
        {
            get
            {
                return this._Depot_Code;
            }
            set
            {
                if ((this._Depot_Code != value))
                {
                    this._Depot_Code = value;
                }
            }
        }


        public System.Nullable<int> Depot_Closed
        {
            get
            {
                return this._Depot_Closed;
            }
            set
            {
                if ((this._Depot_Closed != value))
                {
                    this._Depot_Closed = value;
                }
            }
        }


        public System.Nullable<System.DateTime> Depot_ActivationDate
        {
            get
            {
                return this._Depot_ActivationDate;
            }
            set
            {
                if ((this._Depot_ActivationDate != value))
                {
                    this._Depot_ActivationDate = value;
                }
            }
        }


        public System.Nullable<System.DateTime> Depot_DeletionDate
        {
            get
            {
                return this._Depot_DeletionDate;
            }
            set
            {
                if ((this._Depot_DeletionDate != value))
                {
                    this._Depot_DeletionDate = value;
                }
            }
        }


        public System.Nullable<System.DateTime> Depot_LastUpdateDate
        {
            get
            {
                return this._Depot_LastUpdateDate;
            }
            set
            {
                if ((this._Depot_LastUpdateDate != value))
                {
                    this._Depot_LastUpdateDate = value;
                }
            }
        }


        public System.Nullable<int> Depot_Status
        {
            get
            {
                return this._Depot_Status;
            }
            set
            {
                if ((this._Depot_Status != value))
                {
                    this._Depot_Status = value;
                }
            }
        }
    }

}
