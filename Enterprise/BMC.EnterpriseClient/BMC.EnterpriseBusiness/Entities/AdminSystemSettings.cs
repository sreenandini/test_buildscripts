using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.EnterpriseBusiness.Entities
{
    public class AdminSystemSettingsResult
    {
        public SystemSettingsEntity SystemSettings { get; set; }
        public List<CultureInfoEntity> CultureInfoEntities { get; set; }
        public List<MachineTypeInfoEntity> MachineTypeInfoEntities { get; set; }
    }

    public class SystemSettingsEntity
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

        private System.Nullable<bool> _ImportExport_AssetFile;

        private System.Nullable<bool> _IsEnrolmentFlag;

        private System.Nullable<bool> _IsEnrolmentComplete;
		
        private System.Nullable<bool> _IsSiteLicensingEnabled;

        private System.Nullable<bool> _IsPowerPromoReportsRequired;

        private System.Nullable<bool> _CentralizedDeclaration;

        private System.Nullable<bool> _IsEmployeecardTrackingEnabled;
        
        private System.Nullable<bool> _IsSingleCardEmployee;

        private System.Nullable<bool> _AllowOfflineDeclaration;

        private System.Nullable<bool> _AddShortpayInVoucherOut;

        private System.Nullable<bool> _SystemSettingsDisplayTabVisible;

        private System.Nullable<bool> _SystemSettingsServiceTabVisible;

        private System.Nullable<bool> _AllowEnableDisableBarPosition;

        private System.Nullable<bool> _AlertEnabled;

        private System.Nullable<bool> _EmailAlertEnabled;

        private System.Nullable<bool> _IsAutoCalendarEnabled;

        private System.Nullable<bool> _SendMailFromEnterprise;

        private System.Nullable<bool> _CancelPendingMails;

        private System.Nullable<bool> _AllowMutltipleDrops; 


        public SystemSettingsEntity()
        {
        }


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

        public System.Nullable<bool> ImportExportAssetFile
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

        public System.Nullable<bool> ValidateAGSForGMU
        {
            get;
            set;
        }

        public System.Nullable<bool> IsSitesPartiallyConfiguredEnabled
        {
            get;
            set;
        }
        public System.Nullable<bool> IsGameCappingEnabled
        {
            get;
            set;
        }
        public System.Nullable<bool> IsSuppressZoneEnabled
        {
            get;
            set;
        }
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

        public System.Nullable<bool> IsAlertEnabled
        {
            get
            {
                return this._AlertEnabled;
            }
            set
            {
                if ((this._AlertEnabled != value))
                {
                    this._AlertEnabled = value;

                }
            }
        }


        public System.Nullable<bool> IsEmailAlertEnabled
        {
            get
            {
                return this._EmailAlertEnabled;
            }
            set
            {
                if ((this._EmailAlertEnabled != value))
                {
                    this._EmailAlertEnabled = value;
                }
            }
        }

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
        public System.Nullable<bool> AllowMutltipleDrops
        {
            get
            {
                return this._AllowMutltipleDrops;
            }
            set
            {
                if ((this._AllowMutltipleDrops != value))
                {
                    this._AllowMutltipleDrops = value;
                }
            }
        }
    }

    public class CultureInfoEntity
    {

        private string _CultureInfo;

        public CultureInfoEntity()
        {
        }


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

    public class MachineTypeInfoEntity
    {

        private int _Machine_Type_ID;

        private string _Machine_Type_Code;

        public MachineTypeInfoEntity()
        {
        }


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

    public partial class MachineClassInfoEntity
    {

        private int _Machine_Class_ID;

        private string _Machine_Name;

        public MachineClassInfoEntity()
        {
        }

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

    public class MachineInfoEntity
    {

        private int _Machine_ID;

        private string _Machine_Stock_No;

        public MachineInfoEntity()
        {
        }

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

    public class IsMultiCardAssociatedToUser
    {

        private System.Nullable<bool> _IsMultiCardAssociatedToUser;

        public IsMultiCardAssociatedToUser()
        {
        }

        public System.Nullable<bool> MultiCardAssociatedToUser
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
