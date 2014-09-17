using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.EnterpriseBusiness.Entities
{
    public class Vault_RegionsForDrop
    {
        private int _Sub_Company_Region_ID;

        private string _Sub_Company_Region_Name;

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
    }

    public class Vault_SitesForDrop
    {
        private int _Site_ID;

        private string _Site_Name;

        private string _Site_Code;


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

    public class Vault_UndeclaredDrops : ICloneable
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

        private System.Nullable<long> _Site_Drop_Ref;

        private System.Nullable<int> _Site_ID;

        private string _UserName;

        private int _Vault_ID;

        private string _Name;

        private string _Type_Prefix;

        private System.Nullable<decimal> _BMCVariance;

        private System.Nullable<decimal> _VaultVariance;

        private string _Manufacturer_Name;

        private string _AuditNote;

        private bool _CanFreeze;

        private string _IsCentralDeclaration;

        private bool _IsWebServiceEnabled;

        private System.Nullable<decimal> _Capacity;

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

        public System.Nullable<long> Site_Drop_Ref
        {
            get
            {
                return this._Site_Drop_Ref;
            }
            set
            {
                if ((this._Site_Drop_Ref != value))
                {
                    this._Site_Drop_Ref = value;
                }
            }
        }

        public System.Nullable<int> Site_ID
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

        public System.Nullable<decimal> BMCVariance
        {
            get
            {
                return this._BMCVariance;
            }
            set
            {
                if ((this._BMCVariance != value))
                {
                    this._BMCVariance = value;
                }
            }
        }

        public System.Nullable<decimal> VaultVariance
        {
            get
            {
                return this._VaultVariance;
            }
            set
            {
                if ((this._VaultVariance != value))
                {
                    this._VaultVariance = value;
                }
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
                if ((this._Manufacturer_Name != value))
                {
                    this._Manufacturer_Name = value;
                }
            }
        }

        public string AuditNote
        {
            get
            {
                return this._AuditNote;
            }
            set
            {
                if ((this._AuditNote != value))
                {
                    this._AuditNote = value;
                }
            }
        }

        public bool CanFreeze
        {
            get
            {
                return this._CanFreeze;
            }
            set
            {
                if ((this._CanFreeze != value))
                {
                    this._CanFreeze = value;
                }
            }
        }

        public string IsCentralDeclaration
        {
            get
            {
                return this._IsCentralDeclaration;
            }
            set
            {
                if ((this._IsCentralDeclaration != value))
                {
                    this._IsCentralDeclaration = value;
                }
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
                if ((this._IsWebServiceEnabled != value))
                {
                    this._IsWebServiceEnabled = value;
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

        public object Clone()
        {
            return this.MemberwiseClone();
        }


    }

    public class Vault_CassetteDropDetailsResult
    {
        private string _Cassette_Name;

        private int _Type;

        private long _Drop_ID;

        private int _Cassette_ID;

        private decimal _Denom;

        private System.Nullable<decimal> _MeterBalance;

        private decimal _VaultBalance;

        private System.Nullable<decimal> _DeclaredBalance;

        private System.Nullable<decimal> _AuditBalance;

        private System.Nullable<decimal> _FillAmount;

        private System.Nullable<decimal> _BleedAmount;

        private System.Nullable<decimal> _AdjustmentAmount;

        private System.Nullable<System.DateTime> _dtCreated;

        private System.Nullable<System.DateTime> _dtUpdated;

        private System.Nullable<System.DateTime> _AudtiDate;

        private decimal _MaxFillAmount;

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

        public int Type
        {
            get
            {
                return this._Type;
            }
            set
            {
                if ((this._Type != value))
                {
                    this._Type = value;
                }
            }
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

        public decimal Denom
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

        public System.Nullable<decimal> MeterBalance
        {
            get
            {
                return this._MeterBalance;
            }
            set
            {
                if ((this._MeterBalance != value))
                {
                    this._MeterBalance = value;
                }
            }
        }

        public decimal VaultBalance
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

        public System.Nullable<decimal> AuditBalance
        {
            get
            {
                return this._AuditBalance;
            }
            set
            {
                if ((this._AuditBalance != value))
                {
                    this._AuditBalance = value;
                }
            }
        }

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

        public System.Nullable<System.DateTime> dtCreated
        {
            get
            {
                return this._dtCreated;
            }
            set
            {
                if ((this._dtCreated != value))
                {
                    this._dtCreated = value;
                }
            }
        }

        public System.Nullable<System.DateTime> dtUpdated
        {
            get
            {
                return this._dtUpdated;
            }
            set
            {
                if ((this._dtUpdated != value))
                {
                    this._dtUpdated = value;
                }
            }
        }

        public System.Nullable<System.DateTime> AudtiDate
        {
            get
            {
                return this._AudtiDate;
            }
            set
            {
                if ((this._AudtiDate != value))
                {
                    this._AudtiDate = value;
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
    }

    public partial class Vault_GetDeclaredDropsForAudit
    {

        private long _Drop_ID;

        private int _Cassette_ID;

        private System.Nullable<double> _Denom;

        private System.Nullable<decimal> _VaultBalance;

        private System.Nullable<decimal> _DeclaredBalance;

        private System.Nullable<decimal> _AuditBalance;

        private System.Nullable<decimal> _FillAmount;

        private System.Nullable<decimal> _BleedAmount;

        private System.Nullable<decimal> _AdjustmentAmount;

        private System.Nullable<System.DateTime> _dtCreated;

        private System.Nullable<System.DateTime> _dtUpdated;

        private System.Nullable<System.DateTime> _AudtiDate;

        private System.Nullable<bool> _IsFrozen;

        private System.Nullable<decimal> _Declared_Balance;

        private System.Nullable<decimal> _Meter_Balance;

        private System.Nullable<decimal> _Vault_Balance;

        private string _AuditNote;

        private System.Nullable<int> _FrozeUser;

        private string _Cassette_Name;

        private System.Nullable<int> _Type;

        private System.Nullable<bool> _IsVaultWebServiceEnabled;

        private System.Nullable<decimal> _MaxFillAmount;

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

        public System.Nullable<decimal> AuditBalance
        {
            get
            {
                return this._AuditBalance;
            }
            set
            {
                if ((this._AuditBalance != value))
                {
                    this._AuditBalance = value;
                }
            }
        }

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

        public System.Nullable<System.DateTime> dtCreated
        {
            get
            {
                return this._dtCreated;
            }
            set
            {
                if ((this._dtCreated != value))
                {
                    this._dtCreated = value;
                }
            }
        }

        public System.Nullable<System.DateTime> dtUpdated
        {
            get
            {
                return this._dtUpdated;
            }
            set
            {
                if ((this._dtUpdated != value))
                {
                    this._dtUpdated = value;
                }
            }
        }

        public System.Nullable<System.DateTime> AudtiDate
        {
            get
            {
                return this._AudtiDate;
            }
            set
            {
                if ((this._AudtiDate != value))
                {
                    this._AudtiDate = value;
                }
            }
        }

        public System.Nullable<bool> IsFrozen
        {
            get
            {
                return this._IsFrozen;
            }
            set
            {
                if ((this._IsFrozen != value))
                {
                    this._IsFrozen = value;
                }
            }
        }

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

        public string AuditNote
        {
            get
            {
                return this._AuditNote;
            }
            set
            {
                if ((this._AuditNote != value))
                {
                    this._AuditNote = value;
                }
            }
        }

        public System.Nullable<int> FrozeUser
        {
            get
            {
                return this._FrozeUser;
            }
            set
            {
                if ((this._FrozeUser != value))
                {
                    this._FrozeUser = value;
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

        public System.Nullable<int> Type
        {
            get
            {
                return this._Type;
            }
            set
            {
                if ((this._Type != value))
                {
                    this._Type = value;
                }
            }
        }

        public System.Nullable<bool> IsVaultWebServiceEnabled
        {
            get
            {
                return this._IsVaultWebServiceEnabled;
            }
            set
            {
                if ((this._IsVaultWebServiceEnabled != value))
                {
                    this._IsVaultWebServiceEnabled = value;
                }
            }
        }

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


    public partial class Vault_GetTransactionReason
    {

        private int _Reason_ID;

        private string _Reason_Description;

        private string _ReasonType;

        public Vault_GetTransactionReason()
        {
        }
        public int SNo
        {
            get;
            set;
        }
       
        public int Reason_ID
        {
            get
            {
                return this._Reason_ID;
            }
            set
            {
                if ((this._Reason_ID != value))
                {
                    this._Reason_ID = value;
                }
            }
        }

        public string ReasonType
        {
            get
            {
                return this._ReasonType;
            }
            set
            {
                if ((this._ReasonType != value))
                {
                    this._ReasonType = value;
                }
            }
        }
        
        public string Reason_Description
        {
            get
            {
                return this._Reason_Description;
            }
            set
            {
                if ((this._Reason_Description != value))
                {
                    this._Reason_Description = value;
                }
            }
        }
    }
}