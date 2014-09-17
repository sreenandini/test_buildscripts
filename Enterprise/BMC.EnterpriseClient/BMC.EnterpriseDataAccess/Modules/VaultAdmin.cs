using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Reflection;

namespace BMC.EnterpriseDataAccess
{
    partial class  EnterpriseDataContext
    {
        #region Load Methods

        [Function(Name = "dbo.rsp_Vault_GetAllDevices")]
        public ISingleResult<rsp_Vault_GetAllDevicesResult> Vault_GetAllDevices([Parameter(Name = "User_id", DbType = "Int")] System.Nullable<int> user_id)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), user_id);
            return ((ISingleResult<rsp_Vault_GetAllDevicesResult>)(result.ReturnValue));
        }

        //Load Vault details based on the vault selected
        [Function(Name = "dbo.rsp_Vault_GetVaultDetails")]
        public ISingleResult<rsp_Vault_GetVaultDetailsResult> Vault_GetVaultDetails([Parameter(Name = "Vault_ID", DbType = "Int")] System.Nullable<int> Vault_ID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), Vault_ID);
            return ((ISingleResult<rsp_Vault_GetVaultDetailsResult>)(result.ReturnValue));
        }

        //Load Manaufacturers
        [Function(Name = "dbo.rsp_Vault_GetAllManufacturers")]
        public ISingleResult<rsp_Vault_GetAllManufacturersResult> rsp_Vault_GetAllManufacturers()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<rsp_Vault_GetAllManufacturersResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_Vault_GetallUnassignedSites")]
        [ResultType(typeof(rsp_Vault_GetallUnassignedDevices))]
        [ResultType(typeof(rsp_Vault_GetallUnassignedSites))]
        public IMultipleResults rsp_Vault_GetallUnassignedDevices([Parameter(Name = "User_ID", DbType = "Int")] System.Nullable<int> user_ID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), user_ID);
            return ((IMultipleResults)(result.ReturnValue));
        }

        //Load Casette Details
        [Function(Name = "dbo.rsp_Vault_GetCassetteDetails")]
        public ISingleResult<rsp_Vault_GetCassetteDetailsResult> Vault_GetCassetteDetails([Parameter(Name = "Vault_id", DbType = "Int")] System.Nullable<int> vault_id, [Parameter(Name = "CassetteType", DbType = "Int")] System.Nullable<int> cassetteType)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), vault_id, cassetteType);
            return ((ISingleResult<rsp_Vault_GetCassetteDetailsResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.usp_Vault_AssignToSite")]
        public int AssignToSite([Parameter(DbType = "Xml")] System.Xml.Linq.XElement xml, [Parameter(Name = "User_ID", DbType = "Int")] System.Nullable<int> user_ID, [Parameter(Name = "Module_ID", DbType = "Int")] System.Nullable<int> module_ID, [Parameter(Name = "Module_Name", DbType = "VarChar(150)")] string module_Name, [Parameter(Name = "Screen_Name", DbType = "VarChar(150)")] string screen_Name)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), xml, user_ID, module_ID, module_Name, screen_Name);
            return ((int)(result.ReturnValue));
        }


        #endregion

        #region Update Methods

        //update Vault details
        [Function(Name = "dbo.usp_Vault_UpdateDevice")]
        public ISingleResult<usp_Vault_UpdateDeviceResult> Vault_UpdateDevice(
                    [Parameter(Name = "Vault_ID", DbType = "Int")] System.Nullable<int> vault_ID,
                    [Parameter(Name = "Name", DbType = "VarChar(150)")] string name,
                    [Parameter(Name = "Serial_NO", DbType = "VarChar(30)")] string serial_NO,
                    [Parameter(Name = "Active", DbType = "Bit")] System.Nullable<bool> active,
                    [Parameter(Name = "Site_ID", DbType = "Int")] System.Nullable<int> site_ID,
                    [Parameter(Name = "Alert_Level", DbType = "Int")] System.Nullable<int> alert_Level,
                    [Parameter(Name = "User_ID", DbType = "Int")] System.Nullable<int> user_ID,
                    [Parameter(Name = "Module_ID", DbType = "Int")] System.Nullable<int> module_ID,
                    [Parameter(Name = "Module_Name", DbType = "VarChar(150)")] string module_Name,
                    [Parameter(Name = "Screen_Name", DbType = "VarChar(150)")] string screen_Name,
                    [Parameter(Name = "Manufacturer_ID", DbType = "Int")] System.Nullable<int> manufacturer_ID,
                    [Parameter(Name = "Type_Prefix", DbType = "VarChar(10)")] string type_Prefix,
                    [Parameter(Name = "Capacity", DbType = "Decimal")] System.Nullable<decimal> capacity,
                    [Parameter(Name = "NoofCoinHopper", DbType = "Int")] System.Nullable<int> noofCoinHopper,
                    [Parameter(Name = "NoofCassettes", DbType = "Int")] System.Nullable<int> noofCassettes,
                    [Parameter(Name = "IsWebServiceEnabled", DbType = "Bit")] System.Nullable<bool> isWebServiceEnabled,
                    [Parameter(Name = "NGA_Type", DbType = "VarChar(50)")] string nGA_Type,
                    [Parameter(Name = "Description", DbType = "VarChar(200)")] string description,
                    [Parameter(Name = "StandardFillAmount", DbType = "Decimal")] System.Nullable<decimal> standardFillAmount,
                    [Parameter(Name = "AutoAdjustEnabled", DbType = "Bit")] System.Nullable<bool> AutoAdjustEnabled,
                    [Parameter(Name = "FillRejection", DbType = "Bit")] System.Nullable<bool> FillRejection
            
            )
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), vault_ID, name, serial_NO, active, site_ID, alert_Level, user_ID, module_ID, module_Name, screen_Name, manufacturer_ID, type_Prefix, capacity, noofCoinHopper, noofCassettes, isWebServiceEnabled, nGA_Type, description, standardFillAmount, AutoAdjustEnabled, FillRejection);
            return ((ISingleResult<usp_Vault_UpdateDeviceResult>)(result.ReturnValue));
        }

        //Update Financial Data
        [Function(Name = "dbo.usp_Vault_UpdateFinanceDetails")]
        public ISingleResult<usp_Vault_UpdateFinanceDetailsResult> Vault_UpdateFinanceDetails([Parameter(Name = "Vault_ID", DbType = "Int")] System.Nullable<int> vault_ID, [Parameter(Name = "PurchasePrice", DbType = "Decimal")] System.Nullable<decimal> purchasePrice, [Parameter(Name = "PurchaseInvoice", DbType = "VarChar(50)")] string purchaseInvoice, [Parameter(Name = "PurchaseDate", DbType = "DateTime")] System.Nullable<System.DateTime> purchaseDate, [Parameter(DbType = "DateTime")] System.Nullable<System.DateTime> depreciationDate, [Parameter(Name = "SoldPrice", DbType = "Decimal")] System.Nullable<decimal> soldPrice, [Parameter(Name = "SoldInvoice", DbType = "VarChar(50)")] string soldInvoice, [Parameter(Name = "SoldDate", DbType = "DateTime")] System.Nullable<System.DateTime> soldDate, [Parameter(Name = "User_ID", DbType = "Int")] System.Nullable<int> user_ID, [Parameter(Name = "Module_ID", DbType = "Int")] System.Nullable<int> module_ID, [Parameter(Name = "Module_Name", DbType = "VarChar(150)")] string module_Name, [Parameter(Name = "Screen_Name", DbType = "VarChar(150)")] string screen_Name)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), vault_ID, purchasePrice, purchaseInvoice, purchaseDate, depreciationDate, soldPrice, soldInvoice, soldDate, user_ID, module_ID, module_Name, screen_Name);
            return ((ISingleResult<usp_Vault_UpdateFinanceDetailsResult>)(result.ReturnValue));
        }

        //Update Cassette Details
        [Function(Name = "dbo.usp_Vault_UpdateCassetteDetails")]
        public int Vault_UpdateCassetteDetails(
                    [Parameter(Name = "Cassette_ID", DbType = "Int")] System.Nullable<int> cassette_ID,
                    [Parameter(Name = "Vault_ID", DbType = "Int")] System.Nullable<int> vault_ID,
                    [Parameter(Name = "Cassette_Name", DbType = "VarChar(150)")] string cassette_Name,
                    [Parameter(Name = "Type", DbType = "Int")] System.Nullable<int> type,
                    [Parameter(Name = "Denom", DbType = "Float")] System.Nullable<float> denom,
                    [Parameter(Name = "IsActive", DbType = "Bit")] System.Nullable<bool> isActive,
                    [Parameter(Name = "AlertLevel", DbType = "Int")] System.Nullable<int> alertLevel,
                    [Parameter(Name = "StandardFillAmount", DbType = "Decimal")] System.Nullable<decimal> standardFillAmount,
                    [Parameter(Name = "MaxFillAmount", DbType = "Decimal")] System.Nullable<decimal> maxFillAmount,
                    [Parameter(Name = "Description", DbType = "VarChar(150)")] string description,
                    [Parameter(Name = "User_ID", DbType = "Int")] System.Nullable<int> user_ID,
                    [Parameter(Name = "Module_ID", DbType = "Int")] System.Nullable<int> module_ID,
                    [Parameter(Name = "Module_Name", DbType = "VarChar(150)")] string module_Name,
                    [Parameter(Name = "Screen_Name", DbType = "VarChar(150)")] string screen_Name)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), cassette_ID, vault_ID, cassette_Name, type, denom, isActive, alertLevel, standardFillAmount, maxFillAmount, description, user_ID, module_ID, module_Name, screen_Name);
            return ((int)(result.ReturnValue));
        }

        //Terminate Vault
        [Function(Name = "dbo.usp_Vault_TerminateDevice")]
        public int Vault_TerminateDevice([Parameter(Name = "Vault_ID", DbType = "Int")] System.Nullable<int> vault_ID, [Parameter(Name = "User_ID", DbType = "Int")] System.Nullable<int> user_ID, [Parameter(Name = "Module_ID", DbType = "Int")] System.Nullable<int> module_ID, [Parameter(Name = "Module_Name", DbType = "VarChar(150)")] string module_Name, [Parameter(Name = "Screen_Name", DbType = "VarChar(150)")] string screen_Name, [Parameter(Name = "Description", DbType = "VarChar(150)")] string description)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), vault_ID, user_ID, module_ID, module_Name, screen_Name, description);
            return ((int)(result.ReturnValue));
        }
        #endregion
        
        //COPY
        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.usp_Vault_CopyDevice")]
        public int Vault_CopyDevice([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Name", DbType = "VarChar(150)")] string name, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Serial_NO", DbType = "VarChar(30)")] string serial_NO, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "User_ID", DbType = "Int")] System.Nullable<int> user_ID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Module_ID", DbType = "Int")] System.Nullable<int> module_ID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Module_Name", DbType = "VarChar(150)")] string module_Name, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Screen_Name", DbType = "VarChar(150)")] string screen_Name, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "SrcVault_ID", DbType = "Int")] System.Nullable<int> srcVault_ID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), name, serial_NO, user_ID, module_ID, module_Name, screen_Name, srcVault_ID);
            return ((int)(result.ReturnValue));
        }
	}

    public partial class rsp_Vault_GetAllDevicesResult
    {

        private string _Vault;

        private int _Vault_ID;

        private int _Site_ID;

        private string _Site_Name;

        private string _Site_Code;

        private string _Serial_No;

        private bool _Active;

        private string _Status;

        public rsp_Vault_GetAllDevicesResult()
        {
        }

        [Column(Storage = "_Vault", DbType = "VarChar(150)")]
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

        [Column(Storage = "_Serial_No", DbType = "VarChar(150)")]
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

        [Column(Storage = "_Active", DbType = "Bit")]
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

        [Column(Storage = "_Status", DbType = "VarChar(50)")]
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

    public partial class rsp_Vault_GetVaultDetailsResult
    {

        private int _Vault_ID;

        private string _Site_Name;

        private string _NAME;

        private string _Serial_NO;

        private System.Nullable<int> _Site_ID;

        private System.Nullable<int> _Alert_Level;

        private System.Nullable<System.DateTime> _Created_Date;

        private System.Nullable<System.DateTime> _End_Date;

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

        
            
        public rsp_Vault_GetVaultDetailsResult()
        {
        }
        [Column(Storage = "_Capacity", DbType = "Decimal(15,2)")]

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

        [Column(Storage = "_Serial_NO", DbType = "VarChar(30)")]
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

        [Column(Storage = "_Active", DbType = "Bit")]
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

        [Column(Storage = "_End_Date", DbType = "DateTime")]
        public System.Nullable<System.DateTime> End_Date
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

        [Column(Storage = "_Manufacturer_ID", DbType = "Int")]
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

        [Column(Storage = "_Type_Prefix", DbType = "VarChar(10)")]
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

        [Column(Storage = "_NoofCassettes", DbType = "Int")]
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
        
        [Column(Storage = "_NoofCoinHopper", DbType = "Int")]
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

        [Column(Storage = "_PurchasePrice", DbType = "Decimal(0,0)")]
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

        [Column(Storage = "_PurchaseInvoice", DbType = "VarChar(50)")]
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

        [Column(Storage = "_PurchaseDate", DbType = "DateTime")]
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

        [Column(Storage = "_depreciationDate", DbType = "DateTime")]
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

        [Column(Storage = "_SoldPrice", DbType = "Decimal(0,0)")]
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

        [Column(Storage = "_SoldInvoice", DbType = "VarChar(50)")]
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

        [Column(Storage = "_SoldDate", DbType = "DateTime")]
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

        [Column(Storage = "_Description", DbType = "VarChar(150)")]
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

        [Column(Storage = "_IsAssigned", DbType = "Bit")]
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

        [Column(Storage = "_IsPurchased", DbType = "Bit")]
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

        [Column(Storage = "_IsConfigured", DbType = "Bit")]
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

        [Column(Storage = "_StandaradFillAmount", DbType = "Decimal(0,0)")]
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

        [Column(Storage = "_IsSiteUpdated", DbType = "Bit")]
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

        [Column(Storage = "_FillRejection", DbType = "Bit")]
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
        [Column(Storage = "_AutoAdjustEnabled", DbType = "Bit")]
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

  

    }

    public partial class usp_Vault_UpdateFinanceDetailsResult
    {

        private System.Nullable<int> _Vault_ID;

        public usp_Vault_UpdateFinanceDetailsResult()
        {
        }

        [Column(Storage = "_Vault_ID", DbType = "Int")]
        public System.Nullable<int> Vault_ID
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

    }

    public partial class rsp_Vault_GetCassetteDetailsResult
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

        public rsp_Vault_GetCassetteDetailsResult()
        {
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

        [Column(Storage = "_TYPE", DbType = "Int NOT NULL")]
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

        [Column(Storage = "_Denom", DbType = "Float")]
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

        [Column(Storage = "_IsActive", DbType = "Bit NOT NULL")]
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

        [Column(Storage = "_AlertLevel", DbType = "Int NOT NULL")]
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

        [Column(Storage = "_StandardFillAmount", DbType = "Decimal(0,0) NOT NULL")]
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

        [Column(Storage = "_MinFillAmount", DbType = "Decimal(0,0) NOT NULL")]
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

        [Column(Storage = "_MaxFillAmount", DbType = "Decimal(0,0) NOT NULL")]
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

        [Column(Storage = "_DESCRIPTION", DbType = "VarChar(150)")]
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
        
	public partial class rsp_Vault_GetAllSitesResult
	{
		
		private int _Site_ID;
		
		private string _Site_Name;
		
		public rsp_Vault_GetAllSitesResult()
		{
		}
		
		[Column(Storage="_Site_ID", DbType="Int NOT NULL")]
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
		
		[Column(Storage="_Site_Name", DbType="VarChar(50)")]
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
    
    public partial class usp_Vault_UpdateDeviceResult
    {

        private System.Nullable<int> _Vault_ID;

        public usp_Vault_UpdateDeviceResult()
        {
        }

        [Column(Storage = "_Vault_ID", DbType = "Int")]
        public System.Nullable<int> Vault_ID
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
    }
        
    public partial class rsp_Vault_GetAllManufacturersResult
    {

        private int _Manufacturer_ID;

        private string _Manufacturer_Name;

        public rsp_Vault_GetAllManufacturersResult()
        {
        }

        [Column(Storage = "_Manufacturer_ID", DbType = "Int NOT NULL")]
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
    }
    
    public class SProcVaultResult
    {

        private string _RESULT;

        [Column(Storage = "_RESULT", DbType = "VarChar(8) NOT NULL", CanBeNull = false)]
        public string Result
        {
            get
            {
                return _RESULT;
            }
            set
            {
                if ((_RESULT != value))
                {
                    _RESULT = value;
                }
            }
        }
    }

    #region "Assign to sites"

    public partial class rsp_Vault_GetallUnassignedDevices
    {

        private int _NGADevice_ID;

        private string _Name;

        public rsp_Vault_GetallUnassignedDevices()
        {
        }

        [Column(Storage = "_NGADevice_ID", DbType = "Int NOT NULL")]
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
    }
    public partial class rsp_Vault_GetallUnassignedSites
    {

        private int _Site_ID;

        private string _Site_Name;

        private string _Site_Code;

        public rsp_Vault_GetallUnassignedSites()
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
    }

    #endregion

}
