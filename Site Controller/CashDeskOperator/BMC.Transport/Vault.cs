using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;

namespace BMC.Transport
{
    public enum CassetteTypes : int
    {
        Cassette = 1,
        RejectionCassette = 2,
        Hopper = 50
    }
    public partial class rsp_Vault_GetUndeclaredDropsResult
    {
        private long _Drop_ID;

        private System.Nullable<decimal> _FillAmount;

        private System.Nullable<decimal> _OpeningBalance;

        private System.Nullable<decimal> _BleedAmount;

        private System.Nullable<decimal> _AdjustmentAmount;

        private System.Nullable<decimal> _Meter_Balance;

        private System.Nullable<decimal> _Vault_Balance;

        private System.Nullable<decimal> _Declared_Balance;

        private System.Nullable<bool> _Declared;

        private System.Nullable<bool> _Freezed;

        private System.Nullable<System.DateTime> _CreatedDate;

        private System.Nullable<int> _CreateUser;

        private System.Nullable<System.DateTime> _ModifiedDate;

        private System.Nullable<int> _ModifiedUser;

        private System.Nullable<System.DateTime> _FreezedDate;

        private System.Nullable<int> _FreezeUser;

        private System.Nullable<System.DateTime> _AuditDate;

        private System.Nullable<int> _AuditUser;

        private string _UserName;

        private int _Vault_ID;

        private string _VaultName;

        private string _Manufacturer;

        private string _TypePrefix;

        private System.Nullable<decimal> _VaultCapacity;

        public rsp_Vault_GetUndeclaredDropsResult()
        {
        }

        [Column(Storage = "_VaultCapacity", DbType = "Decimal(18,2)")]
        public System.Nullable<decimal> VaultCapacity
        {
            get
            {
                return this._VaultCapacity;
            }
            set
            {
                if ((this._VaultCapacity != value))
                {
                    this._VaultCapacity = value;
                }
            }
        }

        [Column(Storage = "_Drop_ID", DbType = "BigInt NOT NULL")]
        public long Drop_ID
        {
            get
            {
                return this._Drop_ID;
            }
            set
            {
                if ((this._Drop_ID != value))
                {
                    this._Drop_ID = value;
                }
            }
        }

        [Column(Storage = "_FillAmount", DbType = "Decimal(18,2)")]
        public System.Nullable<decimal> FillAmount
        {
            get
            {
                return this._FillAmount;
            }
            set
            {
                if ((this._FillAmount != value))
                {
                    this._FillAmount = value;
                }
            }
        }

        [Column(Storage = "_OpeningBalance", DbType = "Decimal(18,2)")]
        public System.Nullable<decimal> OpeningBalance
        {
            get
            {
                return this._OpeningBalance;
            }
            set
            {
                if ((this._OpeningBalance != value))
                {
                    this._OpeningBalance = value;
                }
            }
        }

        [Column(Storage = "_BleedAmount", DbType = "Decimal(18,2)")]
        public System.Nullable<decimal> BleedAmount
        {
            get
            {
                return this._BleedAmount;
            }
            set
            {
                if ((this._BleedAmount != value))
                {
                    this._BleedAmount = value;
                }
            }
        }

        [Column(Storage = "_AdjustmentAmount", DbType = "Decimal(18,2)")]
        public System.Nullable<decimal> AdjustmentAmount
        {
            get
            {
                return this._AdjustmentAmount;
            }
            set
            {
                if ((this._AdjustmentAmount != value))
                {
                    this._AdjustmentAmount = value;
                }
            }
        }

        [Column(Storage = "_Meter_Balance", DbType = "Decimal(18,2)")]
        public System.Nullable<decimal> Meter_Balance
        {
            get
            {
                return this._Meter_Balance;
            }
            set
            {
                if ((this._Meter_Balance != value))
                {
                    this._Meter_Balance = value;
                }
            }
        }

        [Column(Storage = "_Vault_Balance", DbType = "Decimal(18,2)")]
        public System.Nullable<decimal> Vault_Balance
        {
            get
            {
                return this._Vault_Balance;
            }
            set
            {
                if ((this._Vault_Balance != value))
                {
                    this._Vault_Balance = value;
                }
            }
        }

        [Column(Storage = "_Declared_Balance", DbType = "Decimal(18,2)")]
        public System.Nullable<decimal> Declared_Balance
        {
            get
            {
                return this._Declared_Balance;
            }
            set
            {
                if ((this._Declared_Balance != value))
                {
                    this._Declared_Balance = value;
                }
            }
        }

        [Column(Storage = "_Declared", DbType = "Bit")]
        public System.Nullable<bool> Declared
        {
            get
            {
                return this._Declared;
            }
            set
            {
                if ((this._Declared != value))
                {
                    this._Declared = value;
                }
            }
        }

        [Column(Storage = "_Freezed", DbType = "Bit")]
        public System.Nullable<bool> Freezed
        {
            get
            {
                return this._Freezed;
            }
            set
            {
                if ((this._Freezed != value))
                {
                    this._Freezed = value;
                }
            }
        }

        [Column(Storage = "_CreatedDate", DbType = "DateTime")]
        public System.Nullable<System.DateTime> CreatedDate
        {
            get
            {
                return this._CreatedDate;
            }
            set
            {
                if ((this._CreatedDate != value))
                {
                    this._CreatedDate = value;
                }
            }
        }

        [Column(Storage = "_CreateUser", DbType = "Int")]
        public System.Nullable<int> CreateUser
        {
            get
            {
                return this._CreateUser;
            }
            set
            {
                if ((this._CreateUser != value))
                {
                    this._CreateUser = value;
                }
            }
        }

        [Column(Storage = "_ModifiedDate", DbType = "DateTime")]
        public System.Nullable<System.DateTime> ModifiedDate
        {
            get
            {
                return this._ModifiedDate;
            }
            set
            {
                if ((this._ModifiedDate != value))
                {
                    this._ModifiedDate = value;
                }
            }
        }

        [Column(Storage = "_ModifiedUser", DbType = "Int")]
        public System.Nullable<int> ModifiedUser
        {
            get
            {
                return this._ModifiedUser;
            }
            set
            {
                if ((this._ModifiedUser != value))
                {
                    this._ModifiedUser = value;
                }
            }
        }

        [Column(Storage = "_FreezedDate", DbType = "DateTime")]
        public System.Nullable<System.DateTime> FreezedDate
        {
            get
            {
                return this._FreezedDate;
            }
            set
            {
                if ((this._FreezedDate != value))
                {
                    this._FreezedDate = value;
                }
            }
        }

        [Column(Storage = "_FreezeUser", DbType = "Int")]
        public System.Nullable<int> FreezeUser
        {
            get
            {
                return this._FreezeUser;
            }
            set
            {
                if ((this._FreezeUser != value))
                {
                    this._FreezeUser = value;
                }
            }
        }

        [Column(Storage = "_AuditDate", DbType = "DateTime")]
        public System.Nullable<System.DateTime> AuditDate
        {
            get
            {
                return this._AuditDate;
            }
            set
            {
                if ((this._AuditDate != value))
                {
                    this._AuditDate = value;
                }
            }
        }

        [Column(Storage = "_AuditUser", DbType = "Int")]
        public System.Nullable<int> AuditUser
        {
            get
            {
                return this._AuditUser;
            }
            set
            {
                if ((this._AuditUser != value))
                {
                    this._AuditUser = value;
                }
            }
        }

        [Column(Storage = "_UserName", DbType = "VarChar(102) NOT NULL", CanBeNull = false)]
        public string UserName
        {
            get
            {
                return this._UserName;
            }
            set
            {
                if ((this._UserName != value))
                {
                    this._UserName = value;
                }
            }
        }

        [Column(Storage = "_Vault_ID", DbType = "Int NOT NULL")]
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

        [Column(Storage = "_VaultName", DbType = "VarChar(150)")]
        public string VaultName
        {
            get
            {
                return this._VaultName;
            }
            set
            {
                if ((this._VaultName != value))
                {
                    this._VaultName = value;
                }
            }
        }

        [Column(Storage = "_Manufacturer", DbType = "VarChar(50)")]
        public string Manufacturer
        {
            get
            {
                return this._Manufacturer;
            }
            set
            {
                if ((this._Manufacturer != value))
                {
                    this._Manufacturer = value;
                }
            }
        }

        [Column(Storage = "_TypePrefix", DbType = "VarChar(10)")]
        public string TypePrefix
        {
            get
            {
                return this._TypePrefix;
            }
            set
            {
                if ((this._TypePrefix != value))
                {
                    this._TypePrefix = value;
                }
            }
        }
    }

    public partial class rsp_GetNGADetailsResult
    {

        private string _Serial_No;

        private System.Nullable<int> _Alert_Level;

        private System.Nullable<decimal> _Capacity;

        private System.Nullable<System.DateTime> _Created_Date;

        private string _Manufacturer;

        private string _VaultType;

        private int _Cassette_ID;

        private string _Cassette_Name;

        private int _Cassette_Type;

        private float _Denom;

        private int _CassetteAlertLevel;

        private decimal _StandardFillAmount;

        private decimal _MinFillAmount;

        private decimal _MaxFillAmount;

        private decimal _MinBleedAmount;

        private decimal _MaxBleedAmount;

        private System.Nullable<decimal> _CurrentBalance;

        private int _StandardQuantity;

        private System.Nullable<bool> _CanChangeDenom;

        private System.Nullable<bool> _DroppedRecently;

        private System.Nullable<bool> _FillRejection;

        public rsp_GetNGADetailsResult()
        {
        }

        [Column(Storage = "_Serial_No", DbType = "VarChar(30)")]
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

        [Column(Storage = "_Alert_Level", DbType = "Int")]
        public System.Nullable<int> Alert_Level
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

        [Column(Storage = "_Capacity", DbType = "Decimal(0,0)")]
        public System.Nullable<decimal> Capacity
        {
            get
            {
                return this._Capacity;
            }
            set
            {
                if ((this._Capacity != value))
                {
                    this._Capacity = value;
                }
            }
        }

        [Column(Storage = "_Created_Date", DbType = "DateTime")]
        public System.Nullable<System.DateTime> Created_Date
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

        [Column(Storage = "_Manufacturer", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string Manufacturer
        {
            get
            {
                return this._Manufacturer;
            }
            set
            {
                if ((this._Manufacturer != value))
                {
                    this._Manufacturer = value;
                }
            }
        }

        [Column(Storage = "_VaultType", DbType = "VarChar(10)")]
        public string VaultType
        {
            get
            {
                return this._VaultType;
            }
            set
            {
                if ((this._VaultType != value))
                {
                    this._VaultType = value;
                }
            }
        }

        [Column(Storage = "_Cassette_ID", DbType = "Int NOT NULL")]
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

        [Column(Storage = "_Cassette_Name", DbType = "VarChar(150) NOT NULL", CanBeNull = false)]
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

        [Column(Storage = "_Cassette_Type", DbType = "Int NOT NULL")]
        public int Cassette_Type
        {
            get
            {
                return this._Cassette_Type;
            }
            set
            {
                if ((this._Cassette_Type != value))
                {
                    this._Cassette_Type = value;
                }
            }
        }

        [Column(Storage = "_Denom", DbType = "FLOAT NOT NULL")]
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

        [Column(Storage = "_CassetteAlertLevel", DbType = "Int NOT NULL")]
        public int CassetteAlertLevel
        {
            get
            {
                return this._CassetteAlertLevel;
            }
            set
            {
                if ((this._CassetteAlertLevel != value))
                {
                    this._CassetteAlertLevel = value;
                }
            }
        }

        [Column(Storage = "_StandardFillAmount", DbType = "Decimal(18,2) NOT NULL")]
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

        [Column(Storage = "_MinFillAmount", DbType = "Decimal(18,2) NOT NULL")]
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

        [Column(Storage = "_MaxFillAmount", DbType = "Decimal(18,2) NOT NULL")]
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

        [Column(Storage = "_MinBleedAmount", DbType = "Decimal(18,2) NOT NULL")]
        public decimal MinBleedAmount
        {
            get
            {
                return this._MinBleedAmount;
            }
            set
            {
                if ((this._MinBleedAmount != value))
                {
                    this._MinBleedAmount = value;
                }
            }
        }

        [Column(Storage = "_MaxBleedAmount", DbType = "Decimal(18,2) NOT NULL")]
        public decimal MaxBleedAmount
        {
            get
            {
                return this._MaxBleedAmount;
            }
            set
            {
                if ((this._MaxBleedAmount != value))
                {
                    this._MaxBleedAmount = value;
                }
            }
        }

        [Column(Storage = "_CurrentBalance", DbType = "Decimal(18,2)")]
        public System.Nullable<decimal> CurrentBalance
        {
            get
            {
                return this._CurrentBalance;
            }
            set
            {
                if ((this._CurrentBalance != value))
                {
                    this._CurrentBalance = value;
                }
            }
        }

        [Column(Storage = "_StandardQuantity", DbType = "Int NOT NULL")]
        public int StandardQuantity
        {
            get
            {
                return this._StandardQuantity;
            }
            set
            {
                if ((this._StandardQuantity != value))
                {
                    this._StandardQuantity = value;
                }
            }
        }

        [Column(Storage = "_CanChangeDenom", DbType = "Bit")]
        public System.Nullable<bool> CanChangeDenom
        {
            get
            {
                return this._CanChangeDenom;
            }
            set
            {
                if ((this._CanChangeDenom != value))
                {
                    this._CanChangeDenom = value;
                }
            }
        }

        [Column(Storage = "_DroppedRecently", DbType = "Bit")]
        public System.Nullable<bool> DroppedRecently
        {
            get
            {
                return this._DroppedRecently;
            }
            set
            {
                if ((this._DroppedRecently != value))
                {
                    this._DroppedRecently = value;
                }
            }
        }

        [Column(Storage = "_FillRejection", DbType = "Bit")]
        public System.Nullable<bool> FillRejection
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
    }

    public partial class rsp_GetNGANameResult
    {

        private int _NGAID;

        private string _NGAName;

        private int _Installation_No;

        private bool _IsEnrolled;

        public rsp_GetNGANameResult()
        {
        }

        [Column(Storage = "_NGAID", DbType = "Int NOT NULL")]
        public int NGAID
        {
            get
            {
                return this._NGAID;
            }
            set
            {
                if ((this._NGAID != value))
                {
                    this._NGAID = value;
                }
            }
        }

        [Column(Storage = "_NGAName", DbType = "VarChar(150) NOT NULL", CanBeNull = false)]
        public string NGAName
        {
            get
            {
                return this._NGAName;
            }
            set
            {
                if ((this._NGAName != value))
                {
                    this._NGAName = value;
                }
            }
        }

        [Column(Storage = "_Installation_No", DbType = "Int NOT NULL")]
        public int Installation_No
        {
            get
            {
                return this._Installation_No;
            }
            set
            {
                if ((this._Installation_No != value))
                {
                    this._Installation_No = value;
                }
            }
        }

        [Column(Storage = "_IsEnrolled", DbType = "Bit")]
        public bool IsEnrolled
        {
            get
            {
                return this._IsEnrolled;
            }
            set
            {
                if ((this._IsEnrolled != value))
                {
                    this._IsEnrolled = value;
                }
            }
        }
    }

    public partial class rsp_GetNGATypesResult
    {

        private int _Type_ID;

        private string _Name;

        private string _Description;

        public rsp_GetNGATypesResult()
        {
        }

        [Column(Storage = "_Type_ID", DbType = "Int NOT NULL")]
        public int Type_ID
        {
            get
            {
                return this._Type_ID;
            }
            set
            {
                if ((this._Type_ID != value))
                {
                    this._Type_ID = value;
                }
            }
        }

        [Column(Storage = "_Name", DbType = "VarChar(150) NOT NULL", CanBeNull = false)]
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

        [Column(Storage = "_Description", DbType = "VarChar(300)")]
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
    }

    public partial class usp_Vault_FillVaultResult
    {

        private int _Vault_ID;

        private string _Name;

        private string _Serial_NO;

        private int _Alert_Level;

        private long _Fill_ID;

        private System.Nullable<decimal> _FillAmount;

        private decimal _TotalAmountOnFill;

        private decimal _CurrentBalance;

        private int _Manufacturer_ID;

        private string _Manufacturer_Name;

        private string _Type_Prefix;

        private decimal _Capacity;

        private System.Nullable<decimal> _CurrentLevel;

        private System.Nullable<System.DateTime> _CreatedDate;

        private string _CreateUser;

        private bool _IsWebServiceEnabled;

        public usp_Vault_FillVaultResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Vault_ID", DbType = "Int NOT NULL")]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Name", DbType = "VarChar(150)")]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Serial_NO", DbType = "VarChar(30)")]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Alert_Level", DbType = "Int NOT NULL")]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Fill_ID", DbType = "BigInt NOT NULL")]
        public long Fill_ID
        {
            get
            {
                return this._Fill_ID;
            }
            set
            {
                if ((this._Fill_ID != value))
                {
                    this._Fill_ID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_FillAmount", DbType = "Decimal(17,2)")]
        public System.Nullable<decimal> FillAmount
        {
            get
            {
                return this._FillAmount;
            }
            set
            {
                if ((this._FillAmount != value))
                {
                    this._FillAmount = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TotalAmountOnFill", DbType = "Decimal(18,2) NOT NULL")]
        public decimal TotalAmountOnFill
        {
            get
            {
                return this._TotalAmountOnFill;
            }
            set
            {
                if ((this._TotalAmountOnFill != value))
                {
                    this._TotalAmountOnFill = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CurrentBalance", DbType = "Decimal(18,2) NOT NULL")]
        public decimal CurrentBalance
        {
            get
            {
                return this._CurrentBalance;
            }
            set
            {
                if ((this._CurrentBalance != value))
                {
                    this._CurrentBalance = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Manufacturer_ID", DbType = "Int NOT NULL")]
        public int Manufacturer_ID
        {
            get
            {
                return this._Manufacturer_ID;
            }
            set
            {
                if ((this._Manufacturer_ID != value))
                {
                    this._Manufacturer_ID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Manufacturer_Name", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string Manufacturer_Name
        {
            get
            {
                return this._Manufacturer_Name;
            }
            set
            {
                if ((this._Manufacturer_Name != value))
                {
                    this._Manufacturer_Name = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Type_Prefix", DbType = "VarChar(10) NOT NULL", CanBeNull = false)]
        public string Type_Prefix
        {
            get
            {
                return this._Type_Prefix;
            }
            set
            {
                if ((this._Type_Prefix != value))
                {
                    this._Type_Prefix = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Capacity", DbType = "Decimal(18,2) NOT NULL")]
        public decimal Capacity
        {
            get
            {
                return this._Capacity;
            }
            set
            {
                if ((this._Capacity != value))
                {
                    this._Capacity = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CurrentLevel", DbType = "Decimal(18,2)")]
        public System.Nullable<decimal> CurrentLevel
        {
            get
            {
                return this._CurrentLevel;
            }
            set
            {
                if ((this._CurrentLevel != value))
                {
                    this._CurrentLevel = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CreatedDate", DbType = "DateTime")]
        public System.Nullable<System.DateTime> CreatedDate
        {
            get
            {
                return this._CreatedDate;
            }
            set
            {
                if ((this._CreatedDate != value))
                {
                    this._CreatedDate = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CreateUser", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string CreateUser
        {
            get
            {
                return this._CreateUser;
            }
            set
            {
                if ((this._CreateUser != value))
                {
                    this._CreateUser = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_IsWebServiceEnabled", DbType = "Bit NOT NULL")]
        public bool IsWebServiceEnabled
        {
            get
            {
                return this._IsWebServiceEnabled;
            }
            set
            {
                if ((this._IsWebServiceEnabled != value))
                {
                    this._IsWebServiceEnabled = value;
                }
            }
        }
    }

    public partial class usp_Vault_DropResult
    {

        private int _Vault_ID;

        private string _Name;

        private string _Serial_NO;

        private int _Alert_Level;

        private long _Fill_ID;

        private System.Nullable<decimal> _FillAmount;

        private decimal _TotalAmountOnFill;

        private decimal _CurrentBalance;

        private int _Manufacturer_ID;

        private string _Manufacturer_Name;

        private string _Type_Prefix;

        private decimal _Capacity;

        private System.Nullable<decimal> _CurrentLevel;

        private System.Nullable<System.DateTime> _CreatedDate;

        private string _CreateUser;

        private bool _IsWebServiceEnabled;

        public usp_Vault_DropResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Vault_ID", DbType = "Int NOT NULL")]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Name", DbType = "VarChar(150)")]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Serial_NO", DbType = "VarChar(30)")]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Alert_Level", DbType = "Int NOT NULL")]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Fill_ID", DbType = "BigInt NOT NULL")]
        public long Fill_ID
        {
            get
            {
                return this._Fill_ID;
            }
            set
            {
                if ((this._Fill_ID != value))
                {
                    this._Fill_ID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_FillAmount", DbType = "Decimal(17,2)")]
        public System.Nullable<decimal> FillAmount
        {
            get
            {
                return this._FillAmount;
            }
            set
            {
                if ((this._FillAmount != value))
                {
                    this._FillAmount = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TotalAmountOnFill", DbType = "Decimal(18,2) NOT NULL")]
        public decimal TotalAmountOnFill
        {
            get
            {
                return this._TotalAmountOnFill;
            }
            set
            {
                if ((this._TotalAmountOnFill != value))
                {
                    this._TotalAmountOnFill = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CurrentBalance", DbType = "Decimal(18,2) NOT NULL")]
        public decimal CurrentBalance
        {
            get
            {
                return this._CurrentBalance;
            }
            set
            {
                if ((this._CurrentBalance != value))
                {
                    this._CurrentBalance = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Manufacturer_ID", DbType = "Int NOT NULL")]
        public int Manufacturer_ID
        {
            get
            {
                return this._Manufacturer_ID;
            }
            set
            {
                if ((this._Manufacturer_ID != value))
                {
                    this._Manufacturer_ID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Manufacturer_Name", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string Manufacturer_Name
        {
            get
            {
                return this._Manufacturer_Name;
            }
            set
            {
                if ((this._Manufacturer_Name != value))
                {
                    this._Manufacturer_Name = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Type_Prefix", DbType = "VarChar(10) NOT NULL", CanBeNull = false)]
        public string Type_Prefix
        {
            get
            {
                return this._Type_Prefix;
            }
            set
            {
                if ((this._Type_Prefix != value))
                {
                    this._Type_Prefix = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Capacity", DbType = "Decimal(18,2) NOT NULL")]
        public decimal Capacity
        {
            get
            {
                return this._Capacity;
            }
            set
            {
                if ((this._Capacity != value))
                {
                    this._Capacity = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CurrentLevel", DbType = "Decimal(18,2)")]
        public System.Nullable<decimal> CurrentLevel
        {
            get
            {
                return this._CurrentLevel;
            }
            set
            {
                if ((this._CurrentLevel != value))
                {
                    this._CurrentLevel = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CreatedDate", DbType = "DateTime")]
        public System.Nullable<System.DateTime> CreatedDate
        {
            get
            {
                return this._CreatedDate;
            }
            set
            {
                if ((this._CreatedDate != value))
                {
                    this._CreatedDate = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CreateUser", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string CreateUser
        {
            get
            {
                return this._CreateUser;
            }
            set
            {
                if ((this._CreateUser != value))
                {
                    this._CreateUser = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_IsWebServiceEnabled", DbType = "Bit NOT NULL")]
        public bool IsWebServiceEnabled
        {
            get
            {
                return this._IsWebServiceEnabled;
            }
            set
            {
                if ((this._IsWebServiceEnabled != value))
                {
                    this._IsWebServiceEnabled = value;
                }
            }
        }
    }

    public partial class NGA_GetCassetteDetailsResult
    {

        private string _Serial_No;

        private System.Nullable<int> _Alert_Level;

        private System.Nullable<decimal> _VaultCapacity;

        private System.Nullable<System.DateTime> _Created_Date;

        private string _Manufacturer;

        private string _VaultType;

        private int _Cassette_ID;

        private System.Nullable<long> _ID;

        private string _Cassette_Name;

        private int _Cassette_Type;

        private float _Denom;

        private int _CassetteAlertLevel;

        private decimal _StandardFillAmount;

        private decimal _MinFillAmount;

        private decimal _MaxFillAmount;

        private decimal _MinBleedAmount;

        private decimal _MaxBleedAmount;

        public NGA_GetCassetteDetailsResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Serial_No", DbType = "VarChar(30)")]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Alert_Level", DbType = "Int")]
        public System.Nullable<int> Alert_Level
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_VaultCapacity", DbType = "Decimal(18,2)")]
        public System.Nullable<decimal> VaultCapacity
        {
            get
            {
                return this._VaultCapacity;
            }
            set
            {
                if ((this._VaultCapacity != value))
                {
                    this._VaultCapacity = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Created_Date", DbType = "DateTime")]
        public System.Nullable<System.DateTime> Created_Date
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Manufacturer", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string Manufacturer
        {
            get
            {
                return this._Manufacturer;
            }
            set
            {
                if ((this._Manufacturer != value))
                {
                    this._Manufacturer = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_VaultType", DbType = "VarChar(10)")]
        public string VaultType
        {
            get
            {
                return this._VaultType;
            }
            set
            {
                if ((this._VaultType != value))
                {
                    this._VaultType = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Cassette_ID", DbType = "Int NOT NULL")]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ID", DbType = "BigInt")]
        public System.Nullable<long> ID
        {
            get
            {
                return this._ID;
            }
            set
            {
                if ((this._ID != value))
                {
                    this._ID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Cassette_Name", DbType = "VarChar(150) NOT NULL", CanBeNull = false)]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Cassette_Type", DbType = "Int NOT NULL")]
        public int Cassette_Type
        {
            get
            {
                return this._Cassette_Type;
            }
            set
            {
                if ((this._Cassette_Type != value))
                {
                    this._Cassette_Type = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Denom", DbType = "Float NOT NULL")]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CassetteAlertLevel", DbType = "Int NOT NULL")]
        public int CassetteAlertLevel
        {
            get
            {
                return this._CassetteAlertLevel;
            }
            set
            {
                if ((this._CassetteAlertLevel != value))
                {
                    this._CassetteAlertLevel = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_StandardFillAmount", DbType = "Decimal(18,2) NOT NULL")]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_MinFillAmount", DbType = "Decimal(18,2) NOT NULL")]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_MaxFillAmount", DbType = "Decimal(18,2) NOT NULL")]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_MinBleedAmount", DbType = "Decimal(18,2) NOT NULL")]
        public decimal MinBleedAmount
        {
            get
            {
                return this._MinBleedAmount;
            }
            set
            {
                if ((this._MinBleedAmount != value))
                {
                    this._MinBleedAmount = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_MaxBleedAmount", DbType = "Decimal(18,2) NOT NULL")]
        public decimal MaxBleedAmount
        {
            get
            {
                return this._MaxBleedAmount;
            }
            set
            {
                if ((this._MaxBleedAmount != value))
                {
                    this._MaxBleedAmount = value;
                }
            }
        }
    }

    public partial class rsp_GetVaultCassetteDropsResult
    {

        private long _Drop_ID;

        private int _Cassette_ID;

        private string _Cassette_Name;

        private System.Nullable<float> _Denom;

        private System.Nullable<decimal> _VaultBalance;

        private System.Nullable<decimal> _DeclaredBalance;

        private bool _EnableControls;

        private string _CassetteType_Name;

        private int _CassetteType_ID;

        private System.Nullable<decimal> _MaxFillAmount;

        public rsp_GetVaultCassetteDropsResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Drop_ID", DbType = "BigInt NOT NULL")]
        public long Drop_ID
        {
            get
            {
                return this._Drop_ID;
            }
            set
            {
                if ((this._Drop_ID != value))
                {
                    this._Drop_ID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Cassette_ID", DbType = "Int NOT NULL")]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Cassette_Name", DbType = "VarChar(150) NOT NULL", CanBeNull = false)]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Denom", DbType = "FLOAT")]
        public System.Nullable<float> Denom
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_VaultBalance", DbType = "Decimal(18,2)")]
        public System.Nullable<decimal> VaultBalance
        {
            get
            {
                return this._VaultBalance;
            }
            set
            {
                if ((this._VaultBalance != value))
                {
                    this._VaultBalance = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_DeclaredBalance", DbType = "Decimal(18,2)")]
        public System.Nullable<decimal> DeclaredBalance
        {
            get
            {
                return this._DeclaredBalance;
            }
            set
            {
                if ((this._DeclaredBalance != value))
                {
                    this._DeclaredBalance = value;

                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_EnableControls", DbType = "Bit NOT NULL")]
        public bool EnableControls
        {
            get
            {
                return this._EnableControls;
            }
            set
            {
                if ((this._EnableControls != value))
                {
                    this._EnableControls = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CassetteType_Name", DbType = "VarChar(50)")]
        public string CassetteType_Name
        {
            get
            {
                return this._CassetteType_Name;
            }
            set
            {
                if ((this._CassetteType_Name != value))
                {
                    this._CassetteType_Name = value;
                }
            }

        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CassetteType_ID", DbType = "Int NOT NULL")]
        public int CassetteType_ID
        {
            get
            {
                return this._CassetteType_ID;
            }
            set
            {
                if ((this._CassetteType_ID != value))
                {
                    this._CassetteType_ID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_MaxFillAmount", DbType = "Decimal(18,2)")]
        public System.Nullable<decimal> MaxFillAmount
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


    }
    public partial class rsp_Vault_GetFillHistoryDetailsResult
    {

        private System.Nullable<long> _ID;

        private string _Cassette_Name;

        private System.Nullable<double> _Denom;

        private System.Nullable<decimal> _FillAmount;

        private System.Nullable<decimal> _InitialBalance;

        private System.Nullable<decimal> _VaultBalance;

        private System.Nullable<System.DateTime> _TransactionDate;

        private string _UserName;

        private string _Manufacturer;

        private string _TypePrefix;

        public rsp_Vault_GetFillHistoryDetailsResult()
        {
        }

        public bool EnableControls
        {
            get;
            set;
        }
        public object FontColor
        {
            get;
            set;
        }
        public object CustomFontWeight
        {
            get;
            set;
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ID", DbType = "BigInt")]
        public System.Nullable<long> ID
        {
            get
            {
                return this._ID;
            }
            set
            {
                if ((this._ID != value))
                {
                    this._ID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Cassette_Name", DbType = "VarChar(150) NOT NULL", CanBeNull = false)]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Denom", DbType = "Float")]
        public System.Nullable<double> Denom
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_FillAmount", DbType = "Decimal(18,2)")]
        public System.Nullable<decimal> FillAmount
        {
            get
            {
                return this._FillAmount;
            }
            set
            {
                if ((this._FillAmount != value))
                {
                    this._FillAmount = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_InitialBalance", DbType = "Decimal(18,2)")]
        public System.Nullable<decimal> InitialBalance
        {
            get
            {
                return this._InitialBalance;
            }
            set
            {
                if ((this._InitialBalance != value))
                {
                    this._InitialBalance = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_VaultBalance", DbType = "Decimal(18,2)")]
        public System.Nullable<decimal> VaultBalance
        {
            get
            {
                return this._VaultBalance;
            }
            set
            {
                if ((this._VaultBalance != value))
                {
                    this._VaultBalance = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TransactionDate", DbType = "DateTime")]
        public System.Nullable<System.DateTime> TransactionDate
        {
            get
            {
                return this._TransactionDate;
            }
            set
            {
                if ((this._TransactionDate != value))
                {
                    this._TransactionDate = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_UserName", DbType = "VarChar(50)")]
        public string UserName
        {
            get
            {
                return this._UserName;
            }
            set
            {
                if ((this._UserName != value))
                {
                    this._UserName = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Manufacturer", DbType = "VarChar(50)")]
        public string Manufacturer
        {
            get
            {
                return this._Manufacturer;
            }
            set
            {
                if ((this._Manufacturer != value))
                {
                    this._Manufacturer = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TypePrefix", DbType = "VarChar(10)")]
        public string TypePrefix
        {
            get
            {
                return this._TypePrefix;
            }
            set
            {
                if ((this._TypePrefix != value))
                {
                    this._TypePrefix = value;
                }
            }
        }
    }

    public partial class rsp_Vault_CheckStandardFillsCountResult
    {

        private System.Nullable<int> _FillCount;

        public rsp_Vault_CheckStandardFillsCountResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_FillCount", DbType = "Int")]
        public System.Nullable<int> FillCount
        {
            get
            {
                return this._FillCount;
            }
            set
            {
                if ((this._FillCount != value))
                {
                    this._FillCount = value;
                }
            }
        }
    }


    public partial class rsp_Vault_GetTransactionTypesResult
    {

        private int _TYPE_ID;

        private string _Type_Description;

        public rsp_Vault_GetTransactionTypesResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TYPE_ID", DbType = "Int NOT NULL")]
        public int TYPE_ID
        {
            get
            {
                return this._TYPE_ID;
            }
            set
            {
                if ((this._TYPE_ID != value))
                {
                    this._TYPE_ID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Type_Description", DbType = "VarChar(50)")]
        public string Type_Description
        {
            get
            {
                return this._Type_Description;
            }
            set
            {
                if ((this._Type_Description != value))
                {
                    this._Type_Description = value;
                }
            }
        }
    }
    public partial class CassetteDropsResult : INotifyPropertyChanged
    {

        private long _Drop_ID;

        private int _Cassette_ID;

        private string _Cassette_Name;

        private System.Nullable<float> _Denom;

        private System.Nullable<decimal> _VaultBalance;

        private decimal _DeclaredBalance;

        private bool _EnableControls;

        private int _Quantity;

        private bool _IsChecked;

        public bool IsChecked
        {
            get
            {
                return this._IsChecked;
            }

            set
            {
                if (_IsChecked != value)
                {
                    _IsChecked = value;
                    if (this.PropertyChanged != null)
                        PropertyChanged.Invoke(this, new PropertyChangedEventArgs("IsChecked"));
                }
            }
        }

        public int Quantity
        {

            get
            {
                return _Quantity;
            }
            set
            {
                if (_Quantity != value)
                {
                    _Quantity = value;
                    if (PropertyChanged != null)
                        PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Quantity"));
                }
            }
        }

        public object FontColor
        {
            get;
            set;
        }

        public object CustomFontWeight
        {
            get;
            set;
        }

        public CassetteDropsResult()
        {
        }


        public decimal MaxFillAmount
        {
            get;
            set;
        }

        public long Drop_ID
        {
            get
            {
                return this._Drop_ID;
            }
            set
            {
                if ((this._Drop_ID != value))
                {
                    this._Drop_ID = value;
                }
            }
        }

        public bool IsBillCounterAmountEditable
        {
            get;
            set;
        }

        public bool IsBillCounterQuantityEditable
        {
            get;
            set;
        }

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


        public string CassetteType_Name
        {
            get;
            set;
        }


        public int CassetteType_ID
        {
            get;
            set;
        }


        public System.Nullable<float> Denom
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


        public System.Nullable<decimal> VaultBalance
        {
            get
            {
                return this._VaultBalance;
            }
            set
            {
                if ((this._VaultBalance != value))
                {
                    this._VaultBalance = value;
                }
            }
        }


        public decimal DeclaredBalance
        {
            get
            {
                return this._DeclaredBalance;
            }
            set
            {
                if ((this._DeclaredBalance != value))
                {
                    this._DeclaredBalance = value;
                    if (this.PropertyChanged != null)
                        this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("DeclaredBalance"));

                }
            }
        }

        public bool EnableControls
        {
            get
            {
                return this._EnableControls;
            }
            set
            {
                if ((this._EnableControls != value))
                {
                    this._EnableControls = value;
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }

    public partial class GetUndeclaredVaultDrops : INotifyPropertyChanged
    {

        private bool _ToDeclared;

        private decimal _Declared_Balance;

        public List<CassetteDropsResult> Cassettes
        {
            get;
            set;
        }

        public bool IsEmptyCassette
        {
            get;
            set;
        }

        public decimal VaultCapacity
        {
            get;
            set;
        }

        public long Drop_ID { get; set; }

        public int Vault_ID { get; set; }

        public string UserName { get; set; }

        public System.Nullable<decimal> FillAmount { get; set; }

        public System.Nullable<decimal> BleedAmount { get; set; }

        public System.Nullable<decimal> OpeningBalance { get; set; }

        public System.Nullable<decimal> AdjustmentAmount { get; set; }

        public System.Nullable<decimal> Meter_Balance { get; set; }

        public System.Nullable<decimal> Vault_Balance { get; set; }

        public decimal Declared_Balance
        {
            get
            {
                return this._Declared_Balance;
            }
            set
            {
                if (_Declared_Balance != value)
                {
                    _Declared_Balance = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("Declared_Balance"));
                    }
                }
            }
        }

        public System.Nullable<bool> Declared { get; set; }

        public System.Nullable<bool> Freezed { get; set; }

        public System.Nullable<System.DateTime> CreatedDate { get; set; }

        public System.Nullable<int> CreateUser { get; set; }

        public System.Nullable<System.DateTime> ModifiedDate { get; set; }

        public System.Nullable<int> ModifiedUser { get; set; }

        public System.Nullable<System.DateTime> FreezedDate { get; set; }

        public System.Nullable<int> FreezeUser { get; set; }

        public System.Nullable<System.DateTime> AuditDate { get; set; }

        public System.Nullable<int> AuditUser { get; set; }

        public string Manufacturer { get; set; }

        public string VaultName { get; set; }

        public string TypePrefix { get; set; }

        public bool ToDeclared
        {
            get
            {
                return this._ToDeclared;
            }
            set
            {
                if (_ToDeclared != value)
                {
                    _ToDeclared = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("ToDeclared"));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private int _BillsTotal;

        private decimal _TotalCoinsValueAsCurrency;

        public int BillsTotal
        {
            get { return _BillsTotal; }
            set
            {
                _BillsTotal = value;
                if (PropertyChanged != null)
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("BillsTotal"));
            }
        }

        public decimal TotalCoinsValueAsCurrency
        {
            get { return _TotalCoinsValueAsCurrency; }
            set
            {
                _TotalCoinsValueAsCurrency = value;
                if (PropertyChanged != null)
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("TotalCoinsValueAsCurrency"));
            }
        }
    }



    public partial class GetNGADetailsResult : INotifyPropertyChanged
    {

        private string _Serial_No;

        private System.Nullable<int> _Alert_Level;

        private System.Nullable<decimal> _Capacity;

        private System.Nullable<System.DateTime> _Created_Date;

        private string _Manufacturer;

        private string _VaultType;

        private int _Cassette_ID;

        private string _Cassette_Name;

        private int _Cassette_Type;

        private float _Denom;

        private int _CassetteAlertLevel;

        private decimal _StandardFillAmount;

        private decimal _MinFillAmount;

        private decimal _MaxFillAmount;

        private decimal _MinBleedAmount;

        private decimal _MaxBleedAmount;

        private System.Nullable<decimal> _CurrentBalance;

        private int _Quantity;

        private bool _CanChangeDenom;

        private decimal _Total;

        private decimal _Amount;

        public decimal OldAmount
        {
            get;
            set;
        }

        public bool DroppedRecently
        {
            get;
            set;
        }

        public bool IsStandardFill
        {
            get;
            set;
        }
        public GetNGADetailsResult()
        {
        }
        public bool EnableTotal
        {
            get;
            set;
        }

        public object FontColor
        {
            get;
            set;
        }

        public object CustomFontWeight
        {
            get;
            set;
        }

        public bool IsDROP
        {
            get;
            set;
        }

        public bool IsNotFinalDrop
        {
            get;
            set;
        }

        private bool _IsChecked;

        public bool IsChecked
        {
            get
            {
                return this._IsChecked;
            }

            set
            {
                if (_IsChecked != value)
                {
                    _IsChecked = value;
                    if (!_IsChecked && !IsStandardFill)
                    {
                        Quantity = 0;
                        Amount = 0;
                    }
                    if (this.PropertyChanged != null)
                    {
                        PropertyChanged.Invoke(this, new PropertyChangedEventArgs("IsChecked"));
                        PropertyChanged.Invoke(this, new PropertyChangedEventArgs("AmountEditing"));
                        PropertyChanged.Invoke(this, new PropertyChangedEventArgs("CountEditing"));
                    }
                }
            }
        }

        public bool FillRejection
        {
            get;
            set;
        }

        public bool AmountEditing
        {
            get
            {
                return this._IsChecked && Settings.IsBillCounterAmountEditable;
            }
        }
        public bool CountEditing
        {
            get
            {
                return this._IsChecked && !Settings.IsBillCounterAmountEditable;
            }
        }

        public decimal Total
        {
            get
            {
                return System.Math.Round(this._Total, 2);

            }


            set
            {
                if (this._Total != value)
                {
                    this._Total = value;

                    if (PropertyChanged != null)
                        PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Total"));
                }
            }
        }

        public bool EnableControls
        {
            get;
            set;
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


        public System.Nullable<int> Alert_Level
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


        public System.Nullable<decimal> Capacity
        {
            get
            {
                return this._Capacity;
            }
            set
            {
                if ((this._Capacity != value))
                {
                    this._Capacity = value;
                }
            }
        }


        public System.Nullable<System.DateTime> Created_Date
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


        public string Manufacturer
        {
            get
            {
                return this._Manufacturer;
            }
            set
            {
                if ((this._Manufacturer != value))
                {
                    this._Manufacturer = value;
                }
            }
        }


        public string VaultType
        {
            get
            {
                return this._VaultType;
            }
            set
            {
                if ((this._VaultType != value))
                {
                    this._VaultType = value;
                }
            }
        }


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


        public int Cassette_Type
        {
            get
            {
                return this._Cassette_Type;
            }
            set
            {
                if ((this._Cassette_Type != value))
                {
                    this._Cassette_Type = value;
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


                    if (PropertyChanged != null)
                        PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Denom"));
                }
            }
        }


        public int CassetteAlertLevel
        {
            get
            {
                return this._CassetteAlertLevel;
            }
            set
            {
                if ((this._CassetteAlertLevel != value))
                {
                    this._CassetteAlertLevel = value;
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


        public decimal MinBleedAmount
        {
            get
            {
                return this._MinBleedAmount;
            }
            set
            {
                if ((this._MinBleedAmount != value))
                {
                    this._MinBleedAmount = value;
                }
            }
        }


        public decimal MaxBleedAmount
        {
            get
            {
                return this._MaxBleedAmount;
            }
            set
            {
                if ((this._MaxBleedAmount != value))
                {
                    this._MaxBleedAmount = value;
                }
            }
        }

        public System.Nullable<decimal> CurrentBalance
        {
            get
            {
                return this._CurrentBalance;
            }
            set
            {
                if ((this._CurrentBalance != value))
                {
                    this._CurrentBalance = value;
                }
            }
        }

        public decimal Amount
        {
            get
            {
                return System.Math.Round(this._Amount, 2);
            }
            set
            {
                if ((this._Amount != value))
                {
                    this._Amount = value;
                    Total = Amount + (CurrentBalance ?? 0);
                    if (PropertyChanged != null)
                        PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Amount"));
                }
            }
        }

        public int Quantity
        {
            get
            {
                return this._Quantity;
            }
            set
            {
                if ((this._Quantity != value))
                {
                    this._Quantity = value;
                    Amount = Convert.ToDecimal(_Denom) * Quantity;
                    if (this.PropertyChanged != null)
                        PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Quantity"));
                }
            }
        }


        public bool CanChangeDenom
        {
            get
            {
                return this._CanChangeDenom;
            }
            set
            {
                if ((this._CanChangeDenom != value))
                {
                    this._CanChangeDenom = value;
                }
            }
        }

        public List<DenomCombo> lstDenoms
        {
            get;
            set;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }

    public class DenomCombo
    {
        public float DenomValue { get; set; }
        public int CassetteTypes { get; set; }
    }

    public enum VaultTransactionType
    {
        StandardFill,
        Fill,
        Bleed,
        PositiveAdjustment,
        NegativeAdjustment,
        StandardDrop,
        FinalDrop
    }

    public partial class GetNGANameResult
    {

        private int _NGAID;

        private string _NGAName;

        private int _Installation_No;
        private bool _IsEnrolled;

        public GetNGANameResult()
        {
        }


        public int NGAID
        {
            get
            {
                return this._NGAID;
            }
            set
            {
                if ((this._NGAID != value))
                {
                    this._NGAID = value;
                }
            }
        }


        public string NGAName
        {
            get
            {
                return this._NGAName;
            }
            set
            {
                if ((this._NGAName != value))
                {
                    this._NGAName = value;
                }
            }
        }


        public int Installation_No
        {
            get
            {
                return this._Installation_No;
            }
            set
            {
                if ((this._Installation_No != value))
                {
                    this._Installation_No = value;
                }
            }
        }


        public bool IsEnrolled
        {
            get
            {
                return this._IsEnrolled;
            }
            set
            {
                if ((this._IsEnrolled != value))
                {
                    this._IsEnrolled = value;
                }
            }
        }
    }

    public partial class GetNGATypesResult
    {

        private int _Type_ID;

        private string _Name;

        private string _Description;

        public GetNGATypesResult()
        {
        }


        public int Type_ID
        {
            get
            {
                return this._Type_ID;
            }
            set
            {
                if ((this._Type_ID != value))
                {
                    this._Type_ID = value;
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
    }

    //public class DropDetails
    //{
    //    public string Drop_ID { get; set; }
    //    public string CreatedDate { get; set; }
    //}
}
