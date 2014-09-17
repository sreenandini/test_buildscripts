using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Reflection;

namespace BMC.EnterpriseDataAccess
{
    public partial class EnterpriseDataContext
    {
        #region For Vault Undeclared Drops

        [Function(Name = "dbo.rsp_Vault_GetAllRegions")]
        public ISingleResult<rsp_Vault_GetAllRegionsResult> Vault_GetAllRegions()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<rsp_Vault_GetAllRegionsResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_Vault_GetSitesbasedonRegion")]
        public ISingleResult<rsp_Vault_GetSitesForDropResult> Vault_GetSitesbasedonRegion([Parameter(Name = "Region_Id", DbType = "Int")] System.Nullable<int> region_Id, [Parameter(Name = "User_id", DbType = "Int")] System.Nullable<int> user_id)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), region_Id, user_id);
            return ((ISingleResult<rsp_Vault_GetSitesForDropResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_Vault_GetUndeclaredDrops")]
        public ISingleResult<rsp_Vault_GetUndeclaredDropsResult> Vault_GetUndeclaredDrops([Parameter(Name = "Vault_ID", DbType = "Int")] System.Nullable<int> vault_ID, [Parameter(Name = "Site_ID", DbType = "Int")] System.Nullable<int> site_ID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), vault_ID, site_ID);
            return ((ISingleResult<rsp_Vault_GetUndeclaredDropsResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.rsp_Vault_GetCassetteDropDetails")]
        public ISingleResult<rsp_Vault_GetCassetteDropDetailsResult> Vault_GetCassetteDropDetails([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Drop_ID", DbType = "BigInt")] System.Nullable<long> drop_ID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), drop_ID);
            return ((ISingleResult<rsp_Vault_GetCassetteDropDetailsResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.usp_Vault_UpdateVaultDrops")]
        public int Vault_UpdateVaultDrops([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "DeclaredBalance", DbType = "Decimal(15,2)")] System.Nullable<decimal> declaredBalance, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Declared", DbType = "Bit")] System.Nullable<bool> declared, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "DropID", DbType = "BigInt")] System.Nullable<long> dropID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "SiteCode", DbType = "VarChar(50)")] string siteCode, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "UserId", DbType = "Int")] System.Nullable<int> userId, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "CassetteXML", DbType = "Xml")] System.Xml.Linq.XElement cassetteXML)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), declaredBalance, declared, dropID, siteCode, userId, cassetteXML);
            return ((int)(result.ReturnValue));
        }

        #endregion

        #region For Declared Vault Drops

        [Function(Name = "dbo.rsp_Vault_GetSitesForDrop")]
        public ISingleResult<rsp_Vault_GetSitesForDropResult> GetVaultDeclarationSiteDetails([Parameter(Name = "User_id", DbType = "Int")] System.Nullable<int> user_id)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), user_id);
            return ((ISingleResult<rsp_Vault_GetSitesForDropResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_Vault_GetDeclaredDrops")]
        public ISingleResult<rsp_Vault_GetUndeclaredDropsResult> Vault_GetDeclaredDrops([Parameter(Name = "Vault_ID", DbType = "Int")] System.Nullable<int> vault_ID, [Parameter(Name = "Site_ID", DbType = "Int")] System.Nullable<int> site_ID, [Parameter(Name = "StartDate", DbType = "DateTime")] System.Nullable<System.DateTime> startDate, [Parameter(Name = "EndDate", DbType = "DateTime")] System.Nullable<System.DateTime> endDate, [Parameter(Name = "VarianceType", DbType = "Int")] System.Nullable<int> variancetype)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), vault_ID, site_ID, startDate, endDate, variancetype);
            return ((ISingleResult<rsp_Vault_GetUndeclaredDropsResult>)(result.ReturnValue));
        }

        #endregion

        #region For VaultAuditData

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.rsp_Vault_GetDeclaredDropsForAudit")]
        public ISingleResult<rsp_Vault_GetDeclaredDropsForAuditResult> Vault_GetDeclaredDropsForAudit([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "DropId", DbType = "BIGINT")] System.Nullable<long> dropId)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), dropId);
            return ((ISingleResult<rsp_Vault_GetDeclaredDropsForAuditResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.usp_Vault_UpdateAuditData")]
        public int Vault_UpdateAuditData([Parameter(Name = "DropID", DbType = "BigInt")] System.Nullable<long> dropID, [Parameter(Name = "BmcTotal", DbType = "Decimal")] System.Nullable<decimal> bmcTotal, [Parameter(Name = "VaultTotal", DbType = "Decimal")] System.Nullable<decimal> vaultTotal, [Parameter(Name = "DeclaredBalance", DbType = "Decimal")] System.Nullable<decimal> declaredBalance, [Parameter(Name = "AuditNotes", DbType = "VarChar(500)")] string auditNotes, [Parameter(Name = "Freezed", DbType = "Bit")] System.Nullable<bool> freezed, [Parameter(Name = "UserId", DbType = "Int")] System.Nullable<int> userId, [Parameter(Name = "SiteCode", DbType = "VarChar(10)")] string siteCode, [Parameter(Name = "FreezePrevious", DbType = "Bit")] System.Nullable<bool> FreezePrevious, [Parameter(Name = "CassetteDetails", DbType = "VarChar(MAX)")] string CassetteDetails)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), dropID, bmcTotal, vaultTotal, declaredBalance, auditNotes, freezed, userId, siteCode, FreezePrevious, CassetteDetails);
            return ((int)(result.ReturnValue));
        }

        #endregion

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.rsp_Vault_GetTransactionReasons")]
        public ISingleResult<rsp_Vault_GetTransactionReasonsResult> rsp_Vault_GetTransactionReasons()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<rsp_Vault_GetTransactionReasonsResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.usp_Vault_UpdateTransactionReason")]
        public int usp_Vault_UpdateTransactionReason([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Reason_ID", DbType = "Int")] System.Nullable<int> reason_ID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Reason_Description", DbType = "VarChar(50)")] string reason_Description)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), reason_ID, reason_Description);
            return ((int)(result.ReturnValue));
        }
    }

    public partial class rsp_Vault_GetAllRegionsResult
    {

        private int _Sub_Company_Region_ID;

        private string _Sub_Company_Region_Name;

        public rsp_Vault_GetAllRegionsResult()
        {
        }

        [Column(Storage = "_Sub_Company_Region_ID", DbType = "Int NOT NULL")]
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

        [Column(Storage = "_Sub_Company_Region_Name", DbType = "VarChar(50)")]
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

    public partial class rsp_Vault_GetSitesForDropResult
    {

        private int _Site_ID;

        private string _Site_Name;

        private string _Site_Code;

        private int _Vault_ID;

        private string _NAME;

        private string _Manufacturer_Name;

        private string _Type_Prefix;

        public rsp_Vault_GetSitesForDropResult()
        {
        }

        [Column(Storage = "_Site_ID", DbType = "Int NOT NULL")]
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

        [Column(Storage = "_Site_Name", DbType = "VarChar(50)")]
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

        [Column(Storage = "_Site_Code", DbType = "VarChar(50)")]
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

        [Column(Storage = "_NAME", DbType = "VarChar(150)")]
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

        [Column(Storage = "_Manufacturer_Name", DbType = "VarChar(50)")]
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

        [Column(Storage = "_Type_Prefix", DbType = "VarChar(10)")]
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

        private System.Nullable<bool> _CanFreeze;

        private string _IsCentralDeclaration;

        private bool _IsWebServiceEnabled;

        private System.Nullable<decimal> _Capacity;

        public rsp_Vault_GetUndeclaredDropsResult()
        {
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

        [Column(Storage = "_FillAmount", DbType = "Decimal(0,0)")]
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

        [Column(Storage = "_OpeningBalance", DbType = "Decimal(0,0)")]
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

        [Column(Storage = "_BleedAmount", DbType = "Decimal(0,0)")]
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

        [Column(Storage = "_AdjustmentAmount", DbType = "Decimal(0,0)")]
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

        [Column(Storage = "_Meter_Balance", DbType = "Decimal(0,0)")]
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

        [Column(Storage = "_Vault_Balance", DbType = "Decimal(0,0)")]
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

        [Column(Storage = "_Declared_Balance", DbType = "Decimal(0,0)")]
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

        [Column(Storage = "_Site_Drop_Ref", DbType = "BigInt")]
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

        [Column(Storage = "_Site_ID", DbType = "Int")]
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

        [Column(Storage = "_Name", DbType = "VarChar(150)")]
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

        [Column(Storage = "_Type_Prefix", DbType = "VarChar(10)")]
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

        [Column(Storage = "_BMCVariance", DbType = "Decimal(0,0)")]
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

        [Column(Storage = "_VaultVariance", DbType = "Decimal(0,0)")]
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

        [Column(Storage = "_Manufacturer_Name", DbType = "VarChar(150)")]
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

        [Column(Storage = "_AuditNote", DbType = "VarChar(500)")]
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

        [Column(Storage = "_CanFreeze", DbType = "Bit")]
        public System.Nullable<bool> CanFreeze
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

        [Column(Storage = "_IsCentralDeclaration", DbType = "VarChar(20) NOT NULL", CanBeNull = false)]
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

        [Column(Storage = "_IsWebServiceEnabled", DbType = "Bit")]
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
    }

    public partial class rsp_Vault_GetCassetteDropDetailsResult
    {

        private string _Cassette_Name;

        private int _Type;

        private long _Drop_ID;

        private int _Cassette_ID;

        private System.Nullable<decimal> _Denom;

        private System.Nullable<decimal> _MeterBalance;

        private System.Nullable<decimal> _VaultBalance;

        private System.Nullable<decimal> _DeclaredBalance;

        private System.Nullable<decimal> _AuditBalance;

        private System.Nullable<decimal> _FillAmount;

        private System.Nullable<decimal> _BleedAmount;

        private System.Nullable<decimal> _AdjustmentAmount;

        private System.Nullable<System.DateTime> _dtCreated;

        private System.Nullable<System.DateTime> _dtUpdated;

        private System.Nullable<System.DateTime> _AudtiDate;

        private decimal _MaxFillAmount;

        public rsp_Vault_GetCassetteDropDetailsResult()
        {
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Type", DbType = "Int NOT NULL")]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Denom", DbType = "Float")]
        public System.Nullable<decimal> Denom
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_MeterBalance", DbType = "Decimal(18,2)")]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_AuditBalance", DbType = "Decimal(18,2)")]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_BleedAmount", DbType = "Decimal(18,2)")]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_AdjustmentAmount", DbType = "Decimal(18,2)")]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_dtCreated", DbType = "DateTime")]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_dtUpdated", DbType = "DateTime")]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_AudtiDate", DbType = "DateTime")]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_MaxFillAmount", DbType = "Decimal(18,2)")]
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

    public partial class rsp_Vault_GetDeclaredDropsForAuditResult
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

        public rsp_Vault_GetDeclaredDropsForAuditResult()
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_AuditBalance", DbType = "Decimal(18,2)")]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_BleedAmount", DbType = "Decimal(18,2)")]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_AdjustmentAmount", DbType = "Decimal(18,2)")]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_dtCreated", DbType = "DateTime")]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_dtUpdated", DbType = "DateTime")]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_AudtiDate", DbType = "DateTime")]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_IsFrozen", DbType = "Bit")]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Declared_Balance", DbType = "Decimal(18,2)")]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Meter_Balance", DbType = "Decimal(18,2)")]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Vault_Balance", DbType = "Decimal(18,2)")]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_AuditNote", DbType = "VarChar(500)")]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_FrozeUser", DbType = "Int")]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Type", DbType = "Int")]
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

        //
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_IsVaultWebServiceEnabled", DbType = "Bit")]
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


    public partial class rsp_Vault_GetTransactionReasonsResult
    {

        private int _Reason_ID;

        private string _ReasonType;

        private string _Reason_Description;

        public rsp_Vault_GetTransactionReasonsResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Reason_ID", DbType = "Int NOT NULL")]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ReasonType", DbType = "VarChar(14) NOT NULL", CanBeNull = false)]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Reason_Description", DbType = "VarChar(50)")]
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
