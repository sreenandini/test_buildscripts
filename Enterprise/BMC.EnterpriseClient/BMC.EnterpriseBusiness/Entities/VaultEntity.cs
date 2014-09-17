using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.EnterpriseBusiness.Entities
{
    public partial class Vault_GetAllDevices
    {
        private string _Vault;

        private int _Vault_ID;

        private string _Serial_No;

        private int _Site_ID;

        private string _Site_Name;

        private string _Site_Code;

        private bool _Active;

        private string _Status;

        public string Vault
        {
            get
            {
                return this._Vault;
            }
            set
            {
                if ((this._Vault != value))
                {
                    this._Vault = value;
                }
            }
        }

        public string Serial_No
        {
            get
            {
                return this._Serial_No;
            }
            set
            {
                if ((this._Serial_No != value))
                {
                    this._Serial_No = value;
                }
            }
        }

        public int Vault_ID
        {
            get
            {
                return this._Vault_ID;
            }
            set
            {
                if ((this._Vault_ID != value))
                {
                    this._Vault_ID = value;
                }
            }
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

        public string Site_Code
        {
            get
            {
                return this._Site_Code;
            }
            set
            {
                if ((this._Site_Code != value))
                {
                    this._Site_Code = value;
                }
            }
        }

        public bool Active
        {
            get
            {
                return this._Active;
            }
            set
            {
                if ((this._Active != value))
                {
                    this._Active = value;
                }
            }
        }

        public string Status
        {
            get
            {
                return this._Status;
            }
            set
            {
                if ((this._Status != value))
                {
                    this._Status = value;
                }
            }
        }
    }

    public partial class Vault_Site
    {

        private int _Site_ID;

        private string _Site_Name;



        public Vault_Site()
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
                this._Site_ID = value;
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

                this._Site_Name = value;
            }
        }

    }

    public partial class VaultDetails : ICloneable
    {
        private int _Vault_ID;

        private string _Site_Name;

        private string _NAME;

        private string _Serial_NO;

        private int _Site_ID;

        private int _Alert_Level;

        private DateTime _Created_Date;

        private DateTime _End_Date;

        private bool _Active;

        string _Type_Prefix;

        int _Manufacturer_ID;

        decimal _Capacity;

        private int _NoofCoinHopper;

        private int _NoofCassettes;

        private bool _IsWebServiceEnabled;

        private System.Nullable<decimal> _PurchasePrice;

        private string _PurchaseInvoice;

        private System.Nullable<System.DateTime> _PurchaseDate;

        private System.Nullable<System.DateTime> _depreciationDate;

        private System.Nullable<decimal> _SoldPrice;

        private string _SoldInvoice;

        private System.Nullable<System.DateTime> _SoldDate;

        private string _Description;

        private bool _IsAssigned;

        private bool _IsPurchased;

        private bool _IsConfigured;

        private System.Nullable<decimal> _StandaradFillAmount;

        private bool _IsSiteUpdated;

        private bool _FillRejection;

        private bool _AutoAdjustEnabled;


        public VaultDetails()
        {
        }


        public int NoofCassettes
        {
            get
            {
                return this._NoofCassettes;
            }
            set
            {
                if ((this._NoofCassettes != value))
                {
                    this._NoofCassettes = value;
                }
            }
        }

        public int NoofCoinHopper
        {
            get
            {
                return this._NoofCoinHopper;
            }
            set
            {
                if ((this._NoofCoinHopper != value))
                {
                    this._NoofCoinHopper = value;
                }
            }
        }

        public int Vault_ID
        {
            get
            {
                return this._Vault_ID;
            }
            set
            {
                if ((this._Vault_ID != value))
                {
                    this._Vault_ID = value;
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

        public string NAME
        {
            get
            {
                return this._NAME;
            }
            set
            {
                if ((this._NAME != value))
                {
                    this._NAME = value;
                }
            }
        }


        public string Serial_NO
        {
            get
            {
                return this._Serial_NO;
            }
            set
            {
                if ((this._Serial_NO != value))
                {
                    this._Serial_NO = value;
                }
            }
        }
        public bool Active
        {
            get
            {
                return this._Active;
            }
            set
            {
                if ((this._Active != value))
                {
                    this._Active = value;
                }
            }
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


        public int Alert_Level
        {
            get
            {
                return this._Alert_Level;
            }
            set
            {
                if ((this._Alert_Level != value))
                {
                    this._Alert_Level = value;
                }
            }
        }


        public DateTime Created_Date
        {
            get
            {
                return this._Created_Date;
            }
            set
            {
                if ((this._Created_Date != value))
                {
                    this._Created_Date = value;
                }
            }
        }


        public DateTime End_Date
        {
            get
            {
                return this._End_Date;
            }
            set
            {
                if ((this._End_Date != value))
                {
                    this._End_Date = value;
                }
            }
        }

        public int Manufacturer_ID
        {
            get
            {
                return this._Manufacturer_ID;
            }
            set
            {

                this._Manufacturer_ID = value;
            }
        }
        public string Type_Prefix
        {
            get
            {
                return this._Type_Prefix;
            }
            set
            {

                this._Type_Prefix = value;
            }
        }

        public decimal Capacity
        {
            get
            {
                return this._Capacity;
            }
            set
            {

                this._Capacity = value;
            }
        }


        public bool IsWebServiceEnabled
        {
            get
            {
                return this._IsWebServiceEnabled;
            }
            set
            {

                this._IsWebServiceEnabled = value;

            }
        }

        public System.Nullable<decimal> PurchasePrice
        {
            get
            {
                return this._PurchasePrice;
            }
            set
            {
                if ((this._PurchasePrice != value))
                {
                    this._PurchasePrice = value;
                }
            }
        }

        public string PurchaseInvoice
        {
            get
            {
                return this._PurchaseInvoice;
            }
            set
            {
                if ((this._PurchaseInvoice != value))
                {
                    this._PurchaseInvoice = value;
                }
            }
        }

        public System.Nullable<System.DateTime> PurchaseDate
        {
            get
            {
                return this._PurchaseDate;
            }
            set
            {
                if ((this._PurchaseDate != value))
                {
                    this._PurchaseDate = value;
                }
            }
        }

        public System.Nullable<System.DateTime> depreciationDate
        {
            get
            {
                return this._depreciationDate;
            }
            set
            {
                if ((this._depreciationDate != value))
                {
                    this._depreciationDate = value;
                }
            }
        }

        public System.Nullable<decimal> SoldPrice
        {
            get
            {
                return this._SoldPrice;
            }
            set
            {
                if ((this._SoldPrice != value))
                {
                    this._SoldPrice = value;
                }
            }
        }

        public string Description
        {
            get
            {
                return this._Description;
            }
            set
            {
                if ((this._Description != value))
                {
                    this._Description = value;
                }
            }
        }

        public string SoldInvoice
        {
            get
            {
                return this._SoldInvoice;
            }
            set
            {
                if ((this._SoldInvoice != value))
                {
                    this._SoldInvoice = value;
                }
            }
        }

        public System.Nullable<System.DateTime> SoldDate
        {
            get
            {
                return this._SoldDate;
            }
            set
            {
                if ((this._SoldDate != value))
                {
                    this._SoldDate = value;
                }
            }
        }

        public bool IsAssigned
        {
            get
            {
                return this._IsAssigned;
            }
            set
            {
                if ((this._IsAssigned != value))
                {
                    this._IsAssigned = value;
                }
            }
        }

        public bool IsPurchased
        {
            get
            {
                return this._IsPurchased;
            }
            set
            {
                if ((this._IsPurchased != value))
                {
                    this._IsPurchased = value;
                }
            }
        }

        public bool IsConfigured
        {
            get
            {
                return this._IsConfigured;
            }
            set
            {
                if ((this._IsConfigured != value))
                {
                    this._IsConfigured = value;
                }
            }
        }

        public System.Nullable<decimal> StandaradFillAmount
        {
            get
            {
                return this._StandaradFillAmount;
            }
            set
            {
                if ((this._StandaradFillAmount != value))
                {
                    this._StandaradFillAmount = value;
                }
            }
        }

        public bool IsSiteUpdated
        {
            get
            {
                return this._IsSiteUpdated;
            }
            set
            {
                if ((this._IsSiteUpdated != value))
                {
                    this._IsSiteUpdated = value;
                }
            }
        }
        
        public bool FillRejection
        {
            get
            {
                return this._FillRejection;
            }
            set
            {
                if ((this._FillRejection != value))
                {
                    this._FillRejection = value;
                }
            }
        }
        
        public bool AutoAdjustEnabled
        {
            get
            {
                return this._AutoAdjustEnabled;
            }
            set
            {
                if ((this._AutoAdjustEnabled != value))
                {
                    this._AutoAdjustEnabled = value;
                }
            }
        }

        public override string ToString()
        {
            string strVal = "";

            foreach (System.Reflection.PropertyInfo p_info in this.GetType().GetProperties())
            {
                strVal += p_info.GetValue(this, null) + ",";
            }
            return strVal;
        }

        public object Clone()
        {
            return base.MemberwiseClone();

        }
    }

    public partial class VaultManufacturers
    {

        private int _Manufacturer_ID;

        private string _Manufacturer_Name;

        public VaultManufacturers()
        {
        }


        public int Manufacturer_ID
        {
            get
            {
                return this._Manufacturer_ID;
            }
            set
            {

                this._Manufacturer_ID = value;

            }
        }


        public string Manufacturer_Name
        {
            get
            {
                return this._Manufacturer_Name;
            }
            set
            {

                this._Manufacturer_Name = value;

            }
        }
    }

    public partial class Vault_GetCassetteDetails
    {
        private int _Cassette_ID;

        private string _Cassette_Name;

        private int _TYPE;

        private float _Denom;

        private bool _IsActive;

        private int _AlertLevel;

        private decimal _StandardFillAmount;

        private decimal _MinFillAmount;

        private decimal _MaxFillAmount;

        private string _DESCRIPTION;

        public int Cassette_ID
        {
            get
            {
                return this._Cassette_ID;
            }
            set
            {
                if ((this._Cassette_ID != value))
                {
                    this._Cassette_ID = value;
                }
            }
        }

        public string Cassette_Name
        {
            get
            {
                return this._Cassette_Name;
            }
            set
            {
                if ((this._Cassette_Name != value))
                {
                    this._Cassette_Name = value;
                }
            }
        }

        public int TYPE
        {
            get
            {
                return this._TYPE;
            }
            set
            {
                if ((this._TYPE != value))
                {
                    this._TYPE = value;
                }
            }
        }

        public float Denom
        {
            get
            {
                return this._Denom;
            }
            set
            {
                if ((this._Denom != value))
                {
                    this._Denom = value;
                }
            }
        }

        public bool IsActive
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

        public int AlertLevel
        {
            get
            {
                return this._AlertLevel;
            }
            set
            {
                if ((this._AlertLevel != value))
                {
                    this._AlertLevel = value;
                }
            }
        }

        public decimal StandardFillAmount
        {
            get
            {
                return this._StandardFillAmount;
            }
            set
            {
                if ((this._StandardFillAmount != value))
                {
                    this._StandardFillAmount = value;
                }
            }
        }

        public decimal MinFillAmount
        {
            get
            {
                return this._MinFillAmount;
            }
            set
            {
                if ((this._MinFillAmount != value))
                {
                    this._MinFillAmount = value;
                }
            }
        }

        public decimal MaxFillAmount
        {
            get
            {
                return this._MaxFillAmount;
            }
            set
            {
                if ((this._MaxFillAmount != value))
                {
                    this._MaxFillAmount = value;
                }
            }
        }

        public string DESCRIPTION
        {
            get
            {
                return this._DESCRIPTION;
            }
            set
            {
                if ((this._DESCRIPTION != value))
                {
                    this._DESCRIPTION = value;
                }
            }
        }


    }

    #region "Assign to site"
    public class AssignToSiteData
    {
        public List<UnassignedDevice> Devices { get; set; }
        public List<UnassignedSite> Sites { get; set; }
    }
    public class UnassignedDevice
    {

        private int _NGADevice_ID;

        private string _Name;
        public bool IsMapped { get; set; }

        public UnassignedDevice()
        {
        }

        public int NGADevice_ID
        {
            get
            {
                return this._NGADevice_ID;
            }
            set
            {
                if ((this._NGADevice_ID != value))
                {
                    this._NGADevice_ID = value;
                }
            }
        }

        public string Name
        {
            get
            {
                return this._Name;
            }
            set
            {
                if ((this._Name != value))
                {
                    this._Name = value;
                }
            }
        }
    }

    public class UnassignedSite
    {

        private int _Site_ID;

        private string _Site_Name;

        private string _Site_Code;

        public bool IsMapped { get; set; }

        public UnassignedDevice Mapped_Vault { get; set; }

        public UnassignedSite()
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

        public string Site_Code
        {
            get
            {
                return this._Site_Code;
            }
            set
            {
                if ((this._Site_Code != value))
                {
                    this._Site_Code = value;
                }
            }
        }
    }

    #endregion

}