using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.EnterpriseBusiness.Entities
{
    public partial class ShareScheduleEntity
    {
        private string _Share_Schedule_Name; 

        private int _Share_Band_ID;

        private System.Nullable<int> _Share_Schedule_ID;

        private string _Share_Band_Name;

        private string _Share_Band_Start_Date;

        private string _Share_Band_End_Date;

        private string _Share_Band_Description;

        private System.Nullable<float> _Share_Band_Supplier_Share;

        private System.Nullable<float> _Share_Band_Site_Share;

        private System.Nullable<float> _Share_Band_Company_Share;

        private System.Nullable<float> _Share_Band_Sec_Company_Share;

        private System.Nullable<float> _Share_Band_Future_Supplier_Share;

        private System.Nullable<float> _Share_Band_Future_Site_Share;

        private System.Nullable<float> _Share_Band_Future_Company_Share;

        private System.Nullable<float> _Share_Band_Future_Sec_Company_Share;

        private string _Share_Band_Future_Start_Date;

        private System.Nullable<float> _Share_Band_Past_Supplier_Share;

        private System.Nullable<float> _Share_Band_Past_Site_Share;

        private System.Nullable<float> _Share_Band_Past_Company_Share;

        private System.Nullable<float> _Share_Band_Past_Sec_Company_Share;

        private string _Share_Band_Past_End_Date;

        private System.Nullable<float> _Share_Band_Supplier_Rent;

        private System.Nullable<float> _Share_Band_Future_Supplier_Rent;

        private System.Nullable<float> _Share_Band_Past_Supplier_Rent;

        private System.Nullable<bool> _Share_Band_Supplier_Rent_Guaranteed;

        private System.Nullable<bool> _Share_Band_Future_Supplier_Rent_Guaranteed;

        private System.Nullable<bool> _Share_Band_Past_Supplier_Rent_Guaranteed;

        private System.Nullable<bool> _Share_Band_Supplier_Share_Guaranteed;

        private System.Nullable<bool> _Share_Band_Future_Supplier_Share_Guaranteed;

        private System.Nullable<bool> _Share_Band_Past_Supplier_Share_Guaranteed;

        private System.Nullable<bool> _Share_Band_Company_Share_Guaranteed;

        private System.Nullable<bool> _Share_Band_Future_Company_Share_Guaranteed;

        private System.Nullable<bool> _Share_Band_Past_Company_Share_Guaranteed;

        private System.Nullable<bool> _Share_Band_Site_Share_Guaranteed;

        private System.Nullable<bool> _Share_Band_Future_Site_Share_Guaranteed;

        private System.Nullable<bool> _Share_Band_Past_Site_Share_Guaranteed;

        private System.Nullable<bool> _Share_Band_Sec_Company_Share_Guaranteed;

        private System.Nullable<bool> _Share_Band_Future_Sec_Company_Share_Guaranteed;

        private System.Nullable<bool> _Share_Band_Past_Sec_Company_Share_Guaranteed;

        private System.Nullable<int> _Machine_Class_ID;

        private string _Machine_Name;

        private string _Machine_BACTA_Code;

        private int _Machine_Class_Share_Band;

        private string _PastBandName;

        private string _BandName;

        private string _FutureBandName;

        private string _Machine_Class_Share_Past_Date;

        private string _Machine_Class_Share_Future_Date;

        private System.Nullable<int> _Share_Band_ID_Past;

        private System.Nullable<int> _Share_Band_ID_Present;

        private System.Nullable<int> _Share_Band_ID_Future;

        private string _Machine_Type_Code;


        private string _Share_Schedule_Start_Date;

        private string _Share_Schedule_End_Date;

        private string _Share_Schedule_Description;

        private System.Nullable<int> _Share_Schedule_No_Bands;

        private string _Share_Schedule_Bands_Name_Type;

        private string _Share_Machine_Change_Date;

        public string Share_Schedule_Name
        {
            get
            {
                return this._Share_Schedule_Name;
            }
            set
            {
                if ((this._Share_Schedule_Name != value))
                {
                    this._Share_Schedule_Name = value;
                }
            }
        }

        public int Share_Band_ID 
        {
             get
            {
                return this._Share_Band_ID;
            }
            set
            {
                if ((this._Share_Band_ID != value))
                {
                    this._Share_Band_ID = value;
                }
            }
        }

        public System.Nullable<int> Share_Schedule_ID 
        {
             get
            {
                return this._Share_Schedule_ID;
            }
            set
            {
                if ((this._Share_Schedule_ID != value))
                {
                    this._Share_Schedule_ID = value;
                }
            }
        }

        public string Share_Band_Name 
        {
             get
            {
                return this._Share_Band_Name;
            }
            set
            {
                if ((this._Share_Band_Name != value))
                {
                    this._Share_Band_Name = value;
                }
            }
        }

        public string Share_Band_Start_Date 
        {
             get
            {
                return this._Share_Band_Start_Date;
            }
            set
            {
                if ((this._Share_Band_Start_Date != value))
                {
                    this._Share_Band_Start_Date = value;
                }
            }
        }

        public string Share_Band_End_Date 
        {
             get
            {
                return this._Share_Band_End_Date;
            }
            set
            {
                if ((this._Share_Band_End_Date != value))
                {
                    this._Share_Band_End_Date = value;
                }
            }
        }

        public string Share_Band_Description 
        {
             get
            {
                return this._Share_Band_Description;
            }
            set
            {
                if ((this._Share_Band_Description != value))
                {
                    this._Share_Band_Description = value;
                }
            }
        }     

        public System.Nullable<float> Share_Band_Supplier_Share 
        {
             get
            {
                return this._Share_Band_Supplier_Share;
            }
            set
            {
                if ((this._Share_Band_Supplier_Share != value))
                {
                    this._Share_Band_Supplier_Share = value;
                }
            }
        }
        public System.Nullable<float> Share_Band_Site_Share 
        {
             get
            {
                return this._Share_Band_Site_Share;
            }
            set
            {
                if ((this._Share_Band_Site_Share != value))
                {
                    this._Share_Band_Site_Share = value;
                }
            }
        }

        public System.Nullable<float> Share_Band_Company_Share 
        {
             get
            {
                return this._Share_Band_Company_Share;
            }
            set
            {
                if ((this._Share_Band_Company_Share != value))
                {
                    this._Share_Band_Company_Share = value;
                }
            }
        }       

        public System.Nullable<float> Share_Band_Sec_Company_Share 
        {
             get
            {
                return this._Share_Band_Sec_Company_Share;
            }
            set
            {
                if ((this._Share_Band_Sec_Company_Share != value))
                {
                    this._Share_Band_Sec_Company_Share = value;
                }
            }
        }

        public System.Nullable<float> Share_Band_Future_Supplier_Share 
        {
             get
            {
                return this._Share_Band_Future_Supplier_Share;
            }
            set
            {
                if ((this._Share_Band_Future_Supplier_Share != value))
                {
                    this._Share_Band_Future_Supplier_Share = value;
                }
            }
        }

        public System.Nullable<float> Share_Band_Future_Site_Share 
        {
             get
            {
                return this._Share_Band_Future_Site_Share;
            }
            set
            {
                if ((this._Share_Band_Future_Site_Share != value))
                {
                    this._Share_Band_Future_Site_Share = value;
                }
            }
        }

        public System.Nullable<float> Share_Band_Future_Company_Share 
        {
             get
            {
                return this._Share_Band_Future_Company_Share;
            }
            set
            {
                if ((this._Share_Band_Future_Company_Share != value))
                {
                    this._Share_Band_Future_Company_Share = value;
                }
            }
        }

        public System.Nullable<float> Share_Band_Future_Sec_Company_Share 
        {
             get
            {
                return this._Share_Band_Future_Sec_Company_Share;
            }
            set
            {
                if ((this._Share_Band_Future_Sec_Company_Share != value))
                {
                    this._Share_Band_Future_Sec_Company_Share = value;
                }
            }
        }

        public string Share_Band_Future_Start_Date 
        {
             get
            {
                return this._Share_Band_Future_Start_Date;
            }
            set
            {
                if ((this._Share_Band_Future_Start_Date != value))
                {
                    this._Share_Band_Future_Start_Date = value;
                }
            }
        }

        public System.Nullable<float> Share_Band_Past_Supplier_Share 
        {
             get
            {
                return this._Share_Band_Past_Supplier_Share;
            }
            set
            {
                if ((this._Share_Band_Past_Supplier_Share != value))
                {
                    this._Share_Band_Past_Supplier_Share = value;
                }
            }
        }

        public System.Nullable<float> Share_Band_Past_Site_Share 
        {
             get
            {
                return this._Share_Band_Past_Site_Share;
            }
            set
            {
                if ((this._Share_Band_Past_Site_Share != value))
                {
                    this._Share_Band_Past_Site_Share = value;
                }
            }
        }

        public System.Nullable<float> Share_Band_Past_Company_Share 
        {
             get
            {
                return this._Share_Band_Past_Company_Share;
            }
            set
            {
                if ((this._Share_Band_Past_Company_Share != value))
                {
                    this._Share_Band_Past_Company_Share = value;
                }
            }
        }

        public System.Nullable<float> Share_Band_Past_Sec_Company_Share 
        {
             get
            {
                return this._Share_Band_Past_Sec_Company_Share;
            }
            set
            {
                if ((this._Share_Band_Past_Sec_Company_Share != value))
                {
                    this._Share_Band_Past_Sec_Company_Share = value;
                }
            }
        }

        public string Share_Band_Past_End_Date 
        {
             get
            {
                return this._Share_Band_Past_End_Date;
            }
            set
            {
                if ((this._Share_Band_Past_End_Date != value))
                {
                    this._Share_Band_Past_End_Date = value;
                }
            }
        }

        public System.Nullable<float> Share_Band_Supplier_Rent 
        {
             get
            {
                return this._Share_Band_Supplier_Rent;
            }
            set
            {
                if ((this._Share_Band_Supplier_Rent != value))
                {
                    this._Share_Band_Supplier_Rent = value;
                }
            }
        }

        public System.Nullable<float> Share_Band_Future_Supplier_Rent 
        {
             get
            {
                return this._Share_Band_Future_Supplier_Rent;
            }
            set
            {
                if ((this._Share_Band_Future_Supplier_Rent != value))
                {
                    this._Share_Band_Future_Supplier_Rent = value;
                }
            }
        }

        public System.Nullable<float> Share_Band_Past_Supplier_Rent 
        {
             get
            {
                return this._Share_Band_Past_Supplier_Rent;
            }
            set
            {
                if ((this._Share_Band_Past_Supplier_Rent != value))
                {
                    this._Share_Band_Past_Supplier_Rent = value;
                }
            }
        }

        public System.Nullable<bool> Share_Band_Supplier_Rent_Guaranteed 
        {
             get
            {
                return this._Share_Band_Supplier_Rent_Guaranteed;
            }
            set
            {
                if ((this._Share_Band_Supplier_Rent_Guaranteed != value))
                {
                    this._Share_Band_Supplier_Rent_Guaranteed = value;
                }
            }
        }

        public System.Nullable<bool> Share_Band_Future_Supplier_Rent_Guaranteed 
        {
             get
            {
                return this._Share_Band_Future_Supplier_Rent_Guaranteed;
            }
            set
            {
                if ((this._Share_Band_Future_Supplier_Rent_Guaranteed != value))
                {
                    this._Share_Band_Future_Supplier_Rent_Guaranteed = value;
                }
            }
        }

        public System.Nullable<bool> Share_Band_Past_Supplier_Rent_Guaranteed 
        {
             get
            {
                return this._Share_Band_Past_Supplier_Rent_Guaranteed;
            }
            set
            {
                if ((this._Share_Band_Past_Supplier_Rent_Guaranteed != value))
                {
                    this._Share_Band_Past_Supplier_Rent_Guaranteed = value;
                }
            }
        }

        public System.Nullable<bool> Share_Band_Supplier_Share_Guaranteed 
        {
             get
            {
                return this._Share_Band_Supplier_Share_Guaranteed;
            }
            set
            {
                if ((this._Share_Band_Supplier_Share_Guaranteed != value))
                {
                    this._Share_Band_Supplier_Share_Guaranteed = value;
                }
            }
        }

        public System.Nullable<bool> Share_Band_Future_Supplier_Share_Guaranteed 
        {
             get
            {
                return this._Share_Band_Future_Supplier_Share_Guaranteed;
            }
            set
            {
                if ((this._Share_Band_Future_Supplier_Share_Guaranteed != value))
                {
                    this._Share_Band_Future_Supplier_Share_Guaranteed = value;
                }
            }
        }

        public System.Nullable<bool> Share_Band_Past_Supplier_Share_Guaranteed 
        {
             get
            {
                return this._Share_Band_Past_Supplier_Share_Guaranteed;
            }
            set
            {
                if ((this._Share_Band_Past_Supplier_Share_Guaranteed != value))
                {
                    this._Share_Band_Past_Supplier_Share_Guaranteed = value;
                }
            }
        }

        public System.Nullable<bool> Share_Band_Company_Share_Guaranteed 
        {
             get
            {
                return this._Share_Band_Company_Share_Guaranteed;
            }
            set
            {
                if ((this._Share_Band_Company_Share_Guaranteed != value))
                {
                    this._Share_Band_Company_Share_Guaranteed = value;
                }
            }
        }

        public System.Nullable<bool> Share_Band_Future_Company_Share_Guaranteed 
        {
             get
            {
                return this._Share_Band_Future_Company_Share_Guaranteed;
            }
            set
            {
                if ((this._Share_Band_Future_Company_Share_Guaranteed != value))
                {
                    this._Share_Band_Future_Company_Share_Guaranteed = value;
                }
            }
        }

        public System.Nullable<bool> Share_Band_Past_Company_Share_Guaranteed 
        {
             get
            {
                return this._Share_Band_Past_Company_Share_Guaranteed;
            }
            set
            {
                if ((this._Share_Band_Past_Company_Share_Guaranteed != value))
                {
                    this._Share_Band_Past_Company_Share_Guaranteed = value;
                }
            }
        }

        public System.Nullable<bool> Share_Band_Site_Share_Guaranteed 
        {
             get
            {
                return this._Share_Band_Site_Share_Guaranteed;
            }
            set
            {
                if ((this._Share_Band_Site_Share_Guaranteed != value))
                {
                    this._Share_Band_Site_Share_Guaranteed = value;
                }
            }
        }

        public System.Nullable<bool> Share_Band_Future_Site_Share_Guaranteed 
        {
             get
            {
                return this._Share_Band_Future_Site_Share_Guaranteed;
            }
            set
            {
                if ((this._Share_Band_Future_Site_Share_Guaranteed != value))
                {
                    this._Share_Band_Future_Site_Share_Guaranteed = value;
                }
            }
        }

        public System.Nullable<bool> Share_Band_Past_Site_Share_Guaranteed 
        {
             get
            {
                return this._Share_Band_Past_Site_Share_Guaranteed;
            }
            set
            {
                if ((this._Share_Band_Past_Site_Share_Guaranteed != value))
                {
                    this._Share_Band_Past_Site_Share_Guaranteed = value;
                }
            }
        }

        public System.Nullable<bool> Share_Band_Sec_Company_Share_Guaranteed 
        {
             get
            {
                return this._Share_Band_Sec_Company_Share_Guaranteed;
            }
            set
            {
                if ((this._Share_Band_Sec_Company_Share_Guaranteed != value))
                {
                    this._Share_Band_Sec_Company_Share_Guaranteed = value;
                }
            }
        }

        public System.Nullable<bool> Share_Band_Future_Sec_Company_Share_Guaranteed 
        {
             get
            {
                return this._Share_Band_Future_Sec_Company_Share_Guaranteed;
            }
            set
            {
                if ((this._Share_Band_Future_Sec_Company_Share_Guaranteed != value))
                {
                    this._Share_Band_Future_Sec_Company_Share_Guaranteed = value;
                }
            }
        }

        public System.Nullable<bool> Share_Band_Past_Sec_Company_Share_Guaranteed
        {
            get
            {
                return this._Share_Band_Past_Sec_Company_Share_Guaranteed;
            }
            set
            {
                if ((this._Share_Band_Past_Sec_Company_Share_Guaranteed != value))
                {
                    this._Share_Band_Past_Sec_Company_Share_Guaranteed = value;
                }
            }
        }

        public System.Nullable<int> Machine_Class_ID
        {
            get
            {
                return this._Machine_Class_ID;
            }
            set
            {
                if ((this._Machine_Class_ID != value))
                {
                    this._Machine_Class_ID = value;
                }
            }
        }

        public string Machine_Name
        {
            get
            {
                return this._Machine_Name;
            }
            set
            {
                if ((this._Machine_Name != value))
                {
                    this._Machine_Name = value;
                }
            }
        }

        public string Machine_BACTA_Code
        {
            get
            {
                return this._Machine_BACTA_Code;
            }
            set
            {
                if ((this._Machine_BACTA_Code != value))
                {
                    this._Machine_BACTA_Code = value;
                }
            }
        }

        public int Machine_Class_Share_Band
        {
            get
            {
                return this._Machine_Class_Share_Band;
            }
            set
            {
                if ((this._Machine_Class_Share_Band != value))
                {
                    this._Machine_Class_Share_Band = value;
                }
            }
        }

        public string PastBandName
        {
            get
            {
                return this._PastBandName;
            }
            set
            {
                if ((this._PastBandName != value))
                {
                    this._PastBandName = value;
                }
            }
        }

        public string BandName
        {
            get
            {
                return this._BandName;
            }
            set
            {
                if ((this._BandName != value))
                {
                    this._BandName = value;
                }
            }
        }

        public string FutureBandName
        {
            get
            {
                return this._FutureBandName;
            }
            set
            {
                if ((this._FutureBandName != value))
                {
                    this._FutureBandName = value;
                }
            }
        }

        public string Machine_Class_Share_Past_Date
        {
            get
            {
                return this._Machine_Class_Share_Past_Date;
            }
            set
            {
                if ((this._Machine_Class_Share_Past_Date != value))
                {
                    this._Machine_Class_Share_Past_Date = value;
                }
            }
        }

        public string Machine_Class_Share_Future_Date
        {
            get
            {
                return this._Machine_Class_Share_Future_Date;
            }
            set
            {
                if ((this._Machine_Class_Share_Future_Date != value))
                {
                    this._Machine_Class_Share_Future_Date = value;
                }
            }
        }

        public System.Nullable<int> Share_Band_ID_Past
        {
            get
            {
                return this._Share_Band_ID_Past;
            }
            set
            {
                if ((this._Share_Band_ID_Past != value))
                {
                    this._Share_Band_ID_Past = value;
                }
            }
        }

       
        public System.Nullable<int> Share_Band_ID_Present
        {
            get
            {
                return this._Share_Band_ID_Present;
            }
            set
            {
                if ((this._Share_Band_ID_Present != value))
                {
                    this._Share_Band_ID_Present = value;
                }
            }
        }

      
        public System.Nullable<int> Share_Band_ID_Future
        {
            get
            {
                return this._Share_Band_ID_Future;
            }
            set
            {
                if ((this._Share_Band_ID_Future != value))
                {
                    this._Share_Band_ID_Future = value;
                }
            }
        }

        public string Machine_Type_Code
        {
            get
            {
                return this._Machine_Type_Code;
            }
            set
            {
                if ((this._Machine_Type_Code != value))
                {
                    this._Machine_Type_Code = value;
                }
            }
        }

        
        public string Share_Schedule_Start_Date
        {
            get
            {
                return this._Share_Schedule_Start_Date;
            }
            set
            {
                if ((this._Share_Schedule_Start_Date != value))
                {
                    this._Share_Schedule_Start_Date = value;
                }
            }
        }

       
        public string Share_Schedule_End_Date
        {
            get
            {
                return this._Share_Schedule_End_Date;
            }
            set
            {
                if ((this._Share_Schedule_End_Date != value))
                {
                    this._Share_Schedule_End_Date = value;
                }
            }
        }

       
        public string Share_Schedule_Description
        {
            get
            {
                return this._Share_Schedule_Description;
            }
            set
            {
                if ((this._Share_Schedule_Description != value))
                {
                    this._Share_Schedule_Description = value;
                }
            }
        }

       
        public System.Nullable<int> Share_Schedule_No_Bands
        {
            get
            {
                return this._Share_Schedule_No_Bands;
            }
            set
            {
                if ((this._Share_Schedule_No_Bands != value))
                {
                    this._Share_Schedule_No_Bands = value;
                }
            }
        }

      
        public string Share_Schedule_Bands_Name_Type
        {
            get
            {
                return this._Share_Schedule_Bands_Name_Type;
            }
            set
            {
                if ((this._Share_Schedule_Bands_Name_Type != value))
                {
                    this._Share_Schedule_Bands_Name_Type = value;
                }
            }
        }

       
        public string Share_Machine_Change_Date
        {
            get
            {
                return this._Share_Machine_Change_Date;
            }
            set
            {
                if ((this._Share_Machine_Change_Date != value))
                {
                    this._Share_Machine_Change_Date = value;
                }
            }
        }
    }
}
