using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Reflection;

namespace BMC.EnterpriseDataAccess
{
    public partial class EnterpriseDataContext
    {
        [Function(Name = "dbo.rsp_GetSystemSettings")]
        public ISingleResult<rsp_GetSystemSettingsResult> GetSystemParameterSettings()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<rsp_GetSystemSettingsResult>)(result.ReturnValue));
        }
    }
    public partial class rsp_GetSystemSettingsResult
    {

        private string _BarPosition;

        private string _Company;

        private string _Site;

        private string _SubCompany;

        private string _Zone;

        private System.Nullable<bool> _AutoGenerateModelCode;

        private string _ModelCodePrefix;

        private System.Nullable<int> _ModelCodeMinLength;

        private bool _AutoGenerateStockCode;

        private string _StockCodePrefix;

        private System.Nullable<int> _StockCodeMinLength;

        private System.Nullable<bool> _AllowStockBulkPurchase;

        private System.Nullable<bool> _ForceSiteRepsOnStock;

        private System.Nullable<bool> _ServiceHandheld;

        private string _ServerName;

        private int _Machine_ID;

        private int _Machine_Class_ID;

        private int _Machine_Type_ID;

        private string _RegionCulture;

        private System.Nullable<bool> _IsSiteLicensingEnabled;

        private System.Nullable<bool> _ImportExport_AssetFile;

        private System.Nullable<bool> _IsPowerPromoReportsRequired;

        private System.Nullable<bool> _CentralizedDeclaration;

        private System.Nullable<bool> _IsEmployeecardTrackingEnabled;

        private System.Nullable<bool> _AllowOfflineDeclaration;

        private System.Nullable<bool> _AddShortpayInVoucherOut;

        private System.Nullable<bool> _SystemSettingsDisplayTabVisible;

        private System.Nullable<bool> _SystemSettingsServiceTabVisible;

        public rsp_GetSystemSettingsResult()
        {
        }

        [Column(Storage = "_BarPosition", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string BarPosition
        {
            get
            {
                return this._BarPosition;
            }
            set
            {
                if ((this._BarPosition != value))
                {
                    this._BarPosition = value;
                }
            }
        }

        [Column(Storage = "_Company", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string Company
        {
            get
            {
                return this._Company;
            }
            set
            {
                if ((this._Company != value))
                {
                    this._Company = value;
                }
            }
        }

        [Column(Storage = "_Site", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string Site
        {
            get
            {
                return this._Site;
            }
            set
            {
                if ((this._Site != value))
                {
                    this._Site = value;
                }
            }
        }

        [Column(Storage = "_SubCompany", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string SubCompany
        {
            get
            {
                return this._SubCompany;
            }
            set
            {
                if ((this._SubCompany != value))
                {
                    this._SubCompany = value;
                }
            }
        }

        [Column(Storage = "_Zone", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string Zone
        {
            get
            {
                return this._Zone;
            }
            set
            {
                if ((this._Zone != value))
                {
                    this._Zone = value;
                }
            }
        }

        [Column(Storage = "_AutoGenerateModelCode", DbType = "Bit")]
        public System.Nullable<bool> AutoGenerateModelCode
        {
            get
            {
                return this._AutoGenerateModelCode;
            }
            set
            {
                if ((this._AutoGenerateModelCode != value))
                {
                    this._AutoGenerateModelCode = value;
                }
            }
        }

        [Column(Storage = "_ModelCodePrefix", DbType = "VarChar(10)")]
        public string ModelCodePrefix
        {
            get
            {
                return this._ModelCodePrefix;
            }
            set
            {
                if ((this._ModelCodePrefix != value))
                {
                    this._ModelCodePrefix = value;
                }
            }
        }

        [Column(Storage = "_ModelCodeMinLength", DbType = "Int")]
        public System.Nullable<int> ModelCodeMinLength
        {
            get
            {
                return this._ModelCodeMinLength;
            }
            set
            {
                if ((this._ModelCodeMinLength != value))
                {
                    this._ModelCodeMinLength = value;
                }
            }
        }

        [Column(Storage = "_AutoGenerateStockCode", DbType = "Bit")]
        public bool AutoGenerateStockCode
        {
            get
            {
                return this._AutoGenerateStockCode;
            }
            set
            {
                if ((this._AutoGenerateStockCode != value))
                {
                    this._AutoGenerateStockCode = value;
                }
            }
        }

        [Column(Storage = "_StockCodePrefix", DbType = "VarChar(10)")]
        public string StockCodePrefix
        {
            get
            {
                return this._StockCodePrefix;
            }
            set
            {
                if ((this._StockCodePrefix != value))
                {
                    this._StockCodePrefix = value;
                }
            }
        }

        [Column(Storage = "_StockCodeMinLength", DbType = "Int")]
        public System.Nullable<int> StockCodeMinLength
        {
            get
            {
                return this._StockCodeMinLength;
            }
            set
            {
                if ((this._StockCodeMinLength != value))
                {
                    this._StockCodeMinLength = value;
                }
            }
        }

        [Column(Storage = "_AllowStockBulkPurchase", DbType = "Bit")]
        public System.Nullable<bool> AllowStockBulkPurchase
        {
            get
            {
                return this._AllowStockBulkPurchase;
            }
            set
            {
                if ((this._AllowStockBulkPurchase != value))
                {
                    this._AllowStockBulkPurchase = value;
                }
            }
        }

        [Column(Storage = "_ForceSiteRepsOnStock", DbType = "Bit")]
        public System.Nullable<bool> ForceSiteRepsOnStock
        {
            get
            {
                return this._ForceSiteRepsOnStock;
            }
            set
            {
                if ((this._ForceSiteRepsOnStock != value))
                {
                    this._ForceSiteRepsOnStock = value;
                }
            }
        }

        [Column(Storage = "_ServiceHandheld", DbType = "Bit")]
        public System.Nullable<bool> ServiceHandheld
        {
            get
            {
                return this._ServiceHandheld;
            }
            set
            {
                if ((this._ServiceHandheld != value))
                {
                    this._ServiceHandheld = value;
                }
            }
        }

        [Column(Storage = "_ServerName", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string ServerName
        {
            get
            {
                return this._ServerName;
            }
            set
            {
                if ((this._ServerName != value))
                {
                    this._ServerName = value;
                }
            }
        }

        [Column(Storage = "_Machine_ID", DbType = "Int NOT NULL")]
        public int Machine_ID
        {
            get
            {
                return this._Machine_ID;
            }
            set
            {
                if ((this._Machine_ID != value))
                {
                    this._Machine_ID = value;
                }
            }
        }

        [Column(Storage = "_Machine_Class_ID", DbType = "Int NOT NULL")]
        public int Machine_Class_ID
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

        [Column(Storage = "_Machine_Type_ID", DbType = "Int NOT NULL")]
        public int Machine_Type_ID
        {
            get
            {
                return this._Machine_Type_ID;
            }
            set
            {
                if ((this._Machine_Type_ID != value))
                {
                    this._Machine_Type_ID = value;
                }
            }
        }

        [Column(Storage = "_RegionCulture", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string RegionCulture
        {
            get
            {
                return this._RegionCulture;
            }
            set
            {
                if ((this._RegionCulture != value))
                {
                    this._RegionCulture = value;
                }
            }
        }

        [Column(Storage = "_IsSiteLicensingEnabled", DbType = "Bit")]
        public System.Nullable<bool> IsSiteLicensingEnabled
        {
            get
            {
                return this._IsSiteLicensingEnabled;
            }
            set
            {
                if ((this._IsSiteLicensingEnabled != value))
                {
                    this._IsSiteLicensingEnabled = value;
                }
            }
        }

        [Column(Storage = "_ImportExport_AssetFile", DbType = "Bit")]
        public System.Nullable<bool> ImportExport_AssetFile
        {
            get
            {
                return this._ImportExport_AssetFile;
            }
            set
            {
                if ((this._ImportExport_AssetFile != value))
                {
                    this._ImportExport_AssetFile = value;
                }
            }
        }

        [Column(Storage = "_IsPowerPromoReportsRequired", DbType = "Bit")]
        public System.Nullable<bool> IsPowerPromoReportsRequired
        {
            get
            {
                return this._IsPowerPromoReportsRequired;
            }
            set
            {
                if ((this._IsPowerPromoReportsRequired != value))
                {
                    this._IsPowerPromoReportsRequired = value;
                }
            }
        }

        [Column(Storage = "_CentralizedDeclaration", DbType = "Bit")]
        public System.Nullable<bool> CentralizedDeclaration
        {
            get
            {
                return this._CentralizedDeclaration;
            }
            set
            {
                if ((this._CentralizedDeclaration != value))
                {
                    this._CentralizedDeclaration = value;
                }
            }
        }

        [Column(Storage = "_IsEmployeecardTrackingEnabled", DbType = "Bit")]
        public System.Nullable<bool> IsEmployeecardTrackingEnabled
        {
            get
            {
                return this._IsEmployeecardTrackingEnabled;
            }
            set
            {
                if ((this._IsEmployeecardTrackingEnabled != value))
                {
                    this._IsEmployeecardTrackingEnabled = value;
                }
            }
        }

        [Column(Storage = "_AllowOfflineDeclaration", DbType = "Bit")]
        public System.Nullable<bool> AllowOfflineDeclaration
        {
            get
            {
                return this._AllowOfflineDeclaration;
            }
            set
            {
                if ((this._AllowOfflineDeclaration != value))
                {
                    this._AllowOfflineDeclaration = value;
                }
            }
        }

        [Column(Storage = "_AddShortpayInVoucherOut", DbType = "Bit")]
        public System.Nullable<bool> AddShortpayInVoucherOut
        {
            get
            {
                return this._AddShortpayInVoucherOut;
            }
            set
            {
                if ((this._AddShortpayInVoucherOut != value))
                {
                    this._AddShortpayInVoucherOut = value;
                }
            }
        }

        [Column(Storage = "_SystemSettingsDisplayTabVisible", DbType = "Bit")]
        public System.Nullable<bool> SystemSettingsDisplayTabVisible
        {
            get
            {
                return this._SystemSettingsDisplayTabVisible;
            }
            set
            {
                if ((this._SystemSettingsDisplayTabVisible != value))
                {
                    this._SystemSettingsDisplayTabVisible = value;
                }
            }
        }

        [Column(Storage = "_SystemSettingsServiceTabVisible", DbType = "Bit")]
        public System.Nullable<bool> SystemSettingsServiceTabVisible
        {
            get
            {
                return this._SystemSettingsServiceTabVisible;
            }
            set
            {
                if ((this._SystemSettingsServiceTabVisible != value))
                {
                    this._SystemSettingsServiceTabVisible = value;
                }
            }
        }
    }
}
