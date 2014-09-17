using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Reflection;

namespace BMC.EnterpriseDataAccess
{
    public partial class EnterpriseDataContext
    {
        [Function(Name = "dbo.usp_GetMachineClassList")]
        public ISingleResult<GetMachineClassNemeResult> GetMachineClassNames([Parameter(Name = "TypeID", DbType = "Int")] System.Nullable<int> typeID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), typeID);
            return ((ISingleResult<GetMachineClassNemeResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetMachineInfo")]
        public ISingleResult<GetMachineInfoResult> GetMachineInfo([Parameter(Name = "MachineClassID", DbType = "Int")] System.Nullable<int> machineClassID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), machineClassID);
            return ((ISingleResult<GetMachineInfoResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.usp_UpdateSystemSettings")]
        public int UpdateSystemSettings(
                    [Parameter(Name = "BarPosition", DbType = "VarChar(50)")] string barPosition,
                    [Parameter(Name = "Company", DbType = "VarChar(50)")] string company,
                    [Parameter(Name = "SubCompany", DbType = "VarChar(50)")] string subCompany,
                    [Parameter(Name = "Site", DbType = "VarChar(50)")] string site,
                    [Parameter(Name = "Zone", DbType = "VarChar(50)")] string zone,
                    [Parameter(Name = "AutoGenerateModelCode", DbType = "Bit")] System.Nullable<bool> autoGenerateModelCode,
                    [Parameter(Name = "ModelCodePrefix", DbType = "VarChar(10)")] string modelCodePrefix,
                    [Parameter(Name = "ModelCodeMinLength", DbType = "Int")] System.Nullable<int> modelCodeMinLength,
                    [Parameter(Name = "AutoGenerateStockCode", DbType = "Bit")] System.Nullable<bool> autoGenerateStockCode,
                    [Parameter(Name = "StockCodePrefix", DbType = "VarChar(10)")] string stockCodePrefix,
                    [Parameter(Name = "StockCodeMinLength", DbType = "Int")] System.Nullable<int> stockCodeMinLength,
                    [Parameter(Name = "AllowStockBulkPurchase", DbType = "Bit")] System.Nullable<bool> allowStockBulkPurchase,
                    [Parameter(Name = "ForceSiteRepsOnStock", DbType = "Bit")] System.Nullable<bool> forceSiteRepsOnStock,
                    [Parameter(Name = "ServiceHandheld", DbType = "Bit")] System.Nullable<bool> serviceHandheld,
                    [Parameter(Name = "ServerName", DbType = "VarChar(50)")] string serverName,
                    [Parameter(Name = "RegionCulture", DbType = "VarChar(50)")] string regionCulture,
                    [Parameter(Name = "IsSiteLicensingEnabled", DbType = "VarChar(10)")] string isSiteLicensingEnabled,
                    [Parameter(Name = "ImportExportAssetFile", DbType = "VarChar(10)")] string importExportAssetFile,
                    [Parameter(Name = "IsEnrolmentFlag", DbType = "VarChar(10)")] string isEnrolmentFlag,
                    [Parameter(Name = "IsEnrolmentComplete", DbType = "VarChar(10)")] string isEnrolmentComplete,
                    [Parameter(Name = "IsPowerPromoReportsRequired", DbType = "VarChar(10)")] string isPowerPromoReportsRequired,
                    [Parameter(Name = "CentralizedDeclaration", DbType = "VarChar(10)")] string centralizedDeclaration,
                    [Parameter(Name = "IsEmployeecardTrackingEnabled", DbType = "VarChar(10)")] string isEmployeecardTrackingEnabled,
                    [Parameter(Name = "AllowOfflineDeclaration", DbType = "VarChar(10)")] string allowOfflineDeclaration,
                    [Parameter(Name = "AddShortpayInVoucherOut", DbType = "VarChar(10)")] string addShortpayInVoucherOut,
                    [Parameter(Name = "SystemSettingsDisplayTabVisible", DbType = "VarChar(10)")] string systemSettingsDisplayTabVisible,
                    [Parameter(Name = "SystemSettingsServiceTabVisible", DbType = "VarChar(10)")] string systemSettingsServiceTabVisible,
                    [Parameter(Name = "ValidateAGSForGMU", DbType = "VarChar(10)")] string validateAGSForGMU,
                    [Parameter(Name = "VIEWPARTIALLYCONFIGUREDSITES", DbType = "VarChar(10)")] string vIEWPARTIALLYCONFIGUREDSITES,
                    [Parameter(Name = "IsGameCappingEnabled", DbType = "VarChar(10)")] string isGameCappingEnabled,
                    [Parameter(Name = "IsSuppressZoneEnabled", DbType = "VarChar(10)")] string isSuppressZoneEnabled,
                    [Parameter(Name = "IsSingleCardEmployee", DbType = "VarChar(10)")] string IsSingleCardEmployee,
                    [Parameter(Name = "AllowEnableDisableBarPosition", DbType = "VarChar(10)")] string AllowEnableDisableBarPosition,
                    [Parameter(Name = "IsAlertEnabled", DbType = "VarChar(10)")] string IsAlertEnabled,
                    [Parameter(Name = "IsEmailAlertEnabled", DbType = "VarChar(10)")] string IsEmailAlertEnabled,
                    [Parameter(Name = "IsAutoCalendarEnabled", DbType = "VarChar(10)")] string IsAutoCalendarEnabled,
                    [Parameter(Name = "SendMailFromEnterprise", DbType = "VarChar(10)")] string SendMailFromEnterprise,
                    [Parameter(Name = "CancelPendingMails", DbType = "VarChar(10)")] string CancelPendingMails,
                    [Parameter(Name = "AllowMultipleDrops", DbType = "VarChar(10)")] string AllowMultipleDrops)  
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), barPosition, company, subCompany, 
                site, zone, autoGenerateModelCode, modelCodePrefix, modelCodeMinLength, autoGenerateStockCode, stockCodePrefix, stockCodeMinLength, 
                allowStockBulkPurchase, forceSiteRepsOnStock, serviceHandheld, serverName, regionCulture, isSiteLicensingEnabled, importExportAssetFile, 
                isEnrolmentFlag, isEnrolmentComplete, isPowerPromoReportsRequired, centralizedDeclaration, isEmployeecardTrackingEnabled, allowOfflineDeclaration, 
                addShortpayInVoucherOut, systemSettingsDisplayTabVisible, systemSettingsServiceTabVisible, validateAGSForGMU, vIEWPARTIALLYCONFIGUREDSITES,
                isGameCappingEnabled, isSuppressZoneEnabled, IsSingleCardEmployee, AllowEnableDisableBarPosition, IsAlertEnabled, IsEmailAlertEnabled,
                IsAutoCalendarEnabled, SendMailFromEnterprise, CancelPendingMails,AllowMultipleDrops);
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_IsMultiCardAssociatedToUser")]
        public ISingleResult<rsp_IsMultiCardAssociatedToUserResult> IsMultiCardAssociatedToUser()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<rsp_IsMultiCardAssociatedToUserResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.usp_InsertAuditData")]
        public int InsertAuditData([Parameter(Name = "User_ID", DbType = "Int")] System.Nullable<int> user_ID, [Parameter(Name = "User_Name", DbType = "VarChar(50)")] string user_Name, [Parameter(Name = "Module_ID", DbType = "Int")] System.Nullable<int> module_ID, [Parameter(Name = "Module_Name", DbType = "VarChar(50)")] string module_Name, [Parameter(Name = "Screen_Name", DbType = "VarChar(50)")] string screen_Name, [Parameter(Name = "Slot", DbType = "VarChar(50)")] string slot, [Parameter(Name = "Aud_Field", DbType = "VarChar(500)")] string aud_Field, [Parameter(Name = "Old_Value", DbType = "VarChar(500)")] string old_Value, [Parameter(Name = "New_Value", DbType = "VarChar(500)")] string new_Value, [Parameter(Name = "Aud_Desc", DbType = "VarChar(500)")] string aud_Desc, [Parameter(Name = "Operation_Type", DbType = "VarChar(25)")] string operation_Type)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), user_ID, user_Name, module_ID, module_Name, screen_Name, slot, aud_Field, old_Value, new_Value, aud_Desc, operation_Type);
            return ((int)(result.ReturnValue));
        }
        [Function(Name = "dbo.rsp_GetSystemSettings")]
        [ResultType(typeof(SystemSettingsResult))]
        [ResultType(typeof(CultureInfoResult))]
        [ResultType(typeof(MachineTypeInfoResult))]
        public IMultipleResults GetSystemSettings()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((IMultipleResults)(result.ReturnValue));
        }


    }

    public partial class SystemSettingsResult
    {

        private string _BarPosition;

        private string _Company;

        private string _Site;

        private string _SubCompany;

        private string _Zone;

        private System.Nullable<bool> _AutoGenerateModelCode;

        private string _ModelCodePrefix;

        private System.Nullable<int> _ModelCodeMinLength;

        private System.Nullable<bool> _AutoGenerateStockCode;

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

        private System.Nullable<bool> _IsEnrolmentFlag;

        private System.Nullable<bool> _IsEnrolmentComplete;

        private System.Nullable<bool> _IsPowerPromoReportsRequired;

        private System.Nullable<bool> _CentralizedDeclaration;

        private System.Nullable<bool> _IsEmployeecardTrackingEnabled;

        private System.Nullable<bool> _IsSingleCardEmployee;

        private System.Nullable<bool> _AllowOfflineDeclaration;

        private System.Nullable<bool> _AddShortpayInVoucherOut;

        private System.Nullable<bool> _SystemSettingsDisplayTabVisible;     

        private System.Nullable<bool> _SystemSettingsServiceTabVisible;

        private System.Nullable<bool> _ValidateAGSForGMU;

        private System.Nullable<bool> _VIEWPARTIALLYCONFIGUREDSITES;

        private System.Nullable<bool> _IsGameCappingEnabled;

        private System.Nullable<bool> _IsSuppressZoneEnabled;

        private System.Nullable<bool> _AllowEnableDisableBarPosition;

        private System.Nullable<bool> _IsAlertEnabled;

        private System.Nullable<bool> _IsEmailAlertEnabled;

        private System.Nullable<bool> _IsAutoCalendarEnabled;

        private System.Nullable<bool> _SendMailFromEnterprise;

        private System.Nullable<bool> _CancelPendingMails;

        private System.Nullable<bool> _AllowMultipleDrops;

        public SystemSettingsResult()
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
        public System.Nullable<bool> AutoGenerateStockCode
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

        [Column(Storage = "_IsEnrolmentFlag", DbType = "Bit")]
        public System.Nullable<bool> IsEnrolmentFlag
        {
            get
            {
                return this._IsEnrolmentFlag;
            }
            set
            {
                if ((this._IsEnrolmentFlag != value))
                {
                    this._IsEnrolmentFlag = value;
                }
            }
        }

        [Column(Storage = "_IsEnrolmentComplete", DbType = "Bit")]
        public System.Nullable<bool> IsEnrolmentComplete
        {
            get
            {
                return this._IsEnrolmentComplete;
            }
            set
            {
                if ((this._IsEnrolmentComplete != value))
                {
                    this._IsEnrolmentComplete = value;
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

        [Column(Storage = "_ValidateAGSForGMU", DbType = "Bit")]
        public System.Nullable<bool> ValidateAGSForGMU
        {
            get
            {
                return this._ValidateAGSForGMU;
            }
            set
            {
                if ((this._ValidateAGSForGMU != value))
                {
                    this._ValidateAGSForGMU = value;
                }
            }
        }

        [Column(Storage = "_VIEWPARTIALLYCONFIGUREDSITES", DbType = "Bit")]
        public System.Nullable<bool> VIEWPARTIALLYCONFIGUREDSITES
        {
            get
            {
                return this._VIEWPARTIALLYCONFIGUREDSITES;
            }
            set
            {
                if ((this._VIEWPARTIALLYCONFIGUREDSITES != value))
                {
                    this._VIEWPARTIALLYCONFIGUREDSITES = value;
                }
            }
        }
        [Column(Storage = "_IsGameCappingEnabled", DbType = "Bit")]
        public System.Nullable<bool> IsGameCappingEnabled
        {
            get
            {
                return this._IsGameCappingEnabled;
            }
            set
            {
                if ((this._IsGameCappingEnabled != value))
                {
                    this._IsGameCappingEnabled = value;
                }
            }
        }
        [Column(Storage = "_IsSuppressZoneEnabled", DbType = "Bit")]
        public System.Nullable<bool> IsSuppressZoneEnabled
        {
            get
            {
                return this._IsSuppressZoneEnabled;
            }
            set
            {
                if ((this._IsSuppressZoneEnabled != value))
                {
                    this._IsSuppressZoneEnabled = value;
                }
            }
        }

        [Column(Storage = "_IsSingleCardEmployee", DbType = "Bit")]
        public System.Nullable<bool> IsSingleCardEmployee
        {
            get
            {
                return this._IsSingleCardEmployee;
            }
            set
            {
                if ((this._IsSingleCardEmployee != value))
                {
                    this._IsSingleCardEmployee = value;
                }
            }
        }

        [Column(Storage = "_AllowEnableDisableBarPosition", DbType = "Bit")]
        public System.Nullable<bool> AllowEnableDisableBarPosition
        {
            get
            {
                return this._AllowEnableDisableBarPosition;
            }
            set
            {
                if ((this._AllowEnableDisableBarPosition != value))
                {
                    this._AllowEnableDisableBarPosition = value;
                }
            }
        }

        [Column(Storage = "_IsAlertEnabled", DbType = "Bit")]
        public System.Nullable<bool> IsAlertEnabled
        {
            get
            {
                return this._IsAlertEnabled;
            }
            set
            {
                if ((this._IsAlertEnabled != value))
                {
                    this._IsAlertEnabled = value;
                }
            }
        }

        [Column(Storage = "_IsEmailAlertEnabled", DbType = "Bit")]
        public System.Nullable<bool> IsEmailAlertEnabled
        {
            get
            {
                return this._IsEmailAlertEnabled;
            }
            set
            {
                if ((this._IsEmailAlertEnabled != value))
                {
                    this._IsEmailAlertEnabled = value;
                }
            }
        }

        [Column(Storage = "_IsAutoCalendarEnabled", DbType = "Bit")]
        public System.Nullable<bool> IsAutoCalendarEnabled
        {
            get
            {
                return this._IsAutoCalendarEnabled;
            }
            set
            {
                if ((this._IsAutoCalendarEnabled != value))
                {
                    this._IsAutoCalendarEnabled = value;
                }
            }
        }

        [Column(Storage = "_SendMailFromEnterprise", DbType = "Bit")]
        public System.Nullable<bool> SendMailFromEnterprise
        {
            get
            {
                return this._SendMailFromEnterprise;
            }
            set
            {
                if ((this._SendMailFromEnterprise != value))
                {
                    this._SendMailFromEnterprise = value;
                }
            }
        }

 [Column(Storage = "_CancelPendingMails", DbType = "Bit")]
        public System.Nullable<bool> CancelPendingMails
        {
            get
            {
                return this._CancelPendingMails;
            }
            set
            {
                if ((this._CancelPendingMails != value))
                {
                    this._CancelPendingMails = value;
                }
            }
        }

        [Column(Storage = "_AllowMultipleDrops", DbType = "Bit")]
        public System.Nullable<bool> AllowMultipleDrops
        {
            get
            {
                return this._AllowMultipleDrops;
            }
            set
            {
                if ((this._AllowMultipleDrops != value))
                {
                    this._AllowMultipleDrops = value;
                }
            }
        }
    }

    public partial class GetMachineClassNemeResult
    {

        private int _Machine_Class_ID;

        private string _Machine_Name;

        public GetMachineClassNemeResult()
        {
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

        [Column(Storage = "_Machine_Name", DbType = "VarChar(50)")]
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
    }


    public partial class CultureInfoResult
    {

        private string _CultureInfo;

        public CultureInfoResult()
        {
        }

        [Column(Storage = "_CultureInfo", DbType = "VarChar(6)")]
        public string CultureInfo
        {
            get
            {
                return this._CultureInfo;
            }
            set
            {
                if ((this._CultureInfo != value))
                {
                    this._CultureInfo = value;
                }
            }
        }
    }

    public partial class MachineTypeInfoResult
    {

        private int _Machine_Type_ID;

        private string _Machine_Type_Code;

        public MachineTypeInfoResult()
        {
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

        [Column(Storage = "_Machine_Type_Code", DbType = "VarChar(50)")]
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
    }

    public partial class GetMachineInfoResult
    {

        private int _Machine_ID;

        private string _Machine_Stock_No;

        public GetMachineInfoResult()
        {
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

        [Column(Storage = "_Machine_Stock_No", DbType = "VarChar(50)")]
        public string Machine_Stock_No
        {
            get
            {
                return this._Machine_Stock_No;
            }
            set
            {
                if ((this._Machine_Stock_No != value))
                {
                    this._Machine_Stock_No = value;
                }
            }
        }
    }

    public partial class rsp_IsMultiCardAssociatedToUserResult
    {

        private System.Nullable<bool> _IsMultiCardAssociatedToUser;

        public rsp_IsMultiCardAssociatedToUserResult()
        {
        }

        [Column(Storage = "_IsMultiCardAssociatedToUser", DbType = "Bit")]
        public System.Nullable<bool> IsMultiCardAssociatedToUser
        {
            get
            {
                return this._IsMultiCardAssociatedToUser;
            }
            set
            {
                if ((this._IsMultiCardAssociatedToUser != value))
                {
                    this._IsMultiCardAssociatedToUser = value;
                }
            }
        }
    }
}

