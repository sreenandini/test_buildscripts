using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.EnterpriseBusiness.Entities
{
  public static  class SystemParameterEntity
    {
          // private string _BarPosition;

          //private string _Company;

          //private string _Site;

          //private string _SubCompany;

          //private string _Zone;

          //private bool _AutoGenerateModelCode;

          //private string _ModelCodePrefix;

          //private int _ModelCodeMinLength;

          private static bool _AutoGenerateStockCode;

          //private string _StockCodePrefix;

          //private int _StockCodeMinLength;

          //private bool _AllowStockBulkPurchase;

          //private bool _ForceSiteRepsOnStock;

          //private bool _ServiceHandheld;

          //private string _ServerName;

          //private int _Machine_ID;

          //private int _Machine_Class_ID;

          //private int _Machine_Type_ID;

          //private string _RegionCulture;

          //private bool _IsSiteLicensingEnabled;

          //private bool _ImportExport_AssetFile;

          //private bool _IsPowerPromoReportsRequired;

          //private bool _CentralizedDeclaration;

          //private bool _IsEmployeecardTrackingEnabled;

          //private bool _AllowOfflineDeclaration;

          //private bool _AddShortpayInVoucherOut;

          //private bool _SystemSettingsDisplayTabVisible;

          //private bool _SystemSettingsServiceTabVisible;

         

          
          //public string BarPosition
          //{
          //    get
          //    {
          //        return this._BarPosition;
          //    }
          //    set
          //    {
          //        if ((this._BarPosition != value))
          //        {
          //            this._BarPosition = value;
          //        }
          //    }
          //}

          
          //public string Company
          //{
          //    get
          //    {
          //        return this._Company;
          //    }
          //    set
          //    {
          //        if ((this._Company != value))
          //        {
          //            this._Company = value;
          //        }
          //    }
          //}

          
          //public string Site
          //{
          //    get
          //    {
          //        return this._Site;
          //    }
          //    set
          //    {
          //        if ((this._Site != value))
          //        {
          //            this._Site = value;
          //        }
          //    }
          //}

          
          //public string SubCompany
          //{
          //    get
          //    {
          //        return this._SubCompany;
          //    }
          //    set
          //    {
          //        if ((this._SubCompany != value))
          //        {
          //            this._SubCompany = value;
          //        }
          //    }
          //}

          
          //public string Zone
          //{
          //    get
          //    {
          //        return this._Zone;
          //    }
          //    set
          //    {
          //        if ((this._Zone != value))
          //        {
          //            this._Zone = value;
          //        }
          //    }
          //}

          
          //public bool AutoGenerateModelCode
          //{
          //    get
          //    {
          //        return this._AutoGenerateModelCode;
          //    }
          //    set
          //    {
          //        if ((this._AutoGenerateModelCode != value))
          //        {
          //            this._AutoGenerateModelCode = value;
          //        }
          //    }
          //}

          
          //public string ModelCodePrefix
          //{
          //    get
          //    {
          //        return this._ModelCodePrefix;
          //    }
          //    set
          //    {
          //        if ((this._ModelCodePrefix != value))
          //        {
          //            this._ModelCodePrefix = value;
          //        }
          //    }
          //}

          
          //public int ModelCodeMinLength
          //{
          //    get
          //    {
          //        return this._ModelCodeMinLength;
          //    }
          //    set
          //    {
          //        if ((this._ModelCodeMinLength != value))
          //        {
          //            this._ModelCodeMinLength = value;
          //        }
          //    }
          //}

          
          public  static bool AutoGenerateStockCode
          {
              get
              {
                  return _AutoGenerateStockCode;
              }
              set
              {
                  if ((_AutoGenerateStockCode != value))
                  {
                      _AutoGenerateStockCode = value;
                  }
              }
          }

          
          //public string StockCodePrefix
          //{
          //    get
          //    {
          //        return this._StockCodePrefix;
          //    }
          //    set
          //    {
          //        if ((this._StockCodePrefix != value))
          //        {
          //            this._StockCodePrefix = value;
          //        }
          //    }
          //}

          
          //public int StockCodeMinLength
          //{
          //    get
          //    {
          //        return this._StockCodeMinLength;
          //    }
          //    set
          //    {
          //        if ((this._StockCodeMinLength != value))
          //        {
          //            this._StockCodeMinLength = value;
          //        }
          //    }
          //}

          
          //public bool AllowStockBulkPurchase
          //{
          //    get
          //    {
          //        return this._AllowStockBulkPurchase;
          //    }
          //    set
          //    {
          //        if ((this._AllowStockBulkPurchase != value))
          //        {
          //            this._AllowStockBulkPurchase = value;
          //        }
          //    }
          //}

          
          //public bool ForceSiteRepsOnStock
          //{
          //    get
          //    {
          //        return this._ForceSiteRepsOnStock;
          //    }
          //    set
          //    {
          //        if ((this._ForceSiteRepsOnStock != value))
          //        {
          //            this._ForceSiteRepsOnStock = value;
          //        }
          //    }
          //}

          
          //public bool ServiceHandheld
          //{
          //    get
          //    {
          //        return this._ServiceHandheld;
          //    }
          //    set
          //    {
          //        if ((this._ServiceHandheld != value))
          //        {
          //            this._ServiceHandheld = value;
          //        }
          //    }
          //}

          
          //public string ServerName
          //{
          //    get
          //    {
          //        return this._ServerName;
          //    }
          //    set
          //    {
          //        if ((this._ServerName != value))
          //        {
          //            this._ServerName = value;
          //        }
          //    }
          //}

        
          //public int Machine_ID
          //{
          //    get
          //    {
          //        return this._Machine_ID;
          //    }
          //    set
          //    {
          //        if ((this._Machine_ID != value))
          //        {
          //            this._Machine_ID = value;
          //        }
          //    }
          //}

         
          //public int Machine_Class_ID
          //{
          //    get
          //    {
          //        return this._Machine_Class_ID;
          //    }
          //    set
          //    {
          //        if ((this._Machine_Class_ID != value))
          //        {
          //            this._Machine_Class_ID = value;
          //        }
          //    }
          //}

          
          //public int Machine_Type_ID
          //{
          //    get
          //    {
          //        return this._Machine_Type_ID;
          //    }
          //    set
          //    {
          //        if ((this._Machine_Type_ID != value))
          //        {
          //            this._Machine_Type_ID = value;
          //        }
          //    }
          //}

         
          //public string RegionCulture
          //{
          //    get
          //    {
          //        return this._RegionCulture;
          //    }
          //    set
          //    {
          //        if ((this._RegionCulture != value))
          //        {
          //            this._RegionCulture = value;
          //        }
          //    }
          //}

         
          //public bool IsSiteLicensingEnabled
          //{
          //    get
          //    {
          //        return this._IsSiteLicensingEnabled;
          //    }
          //    set
          //    {
          //        if ((this._IsSiteLicensingEnabled != value))
          //        {
          //            this._IsSiteLicensingEnabled = value;
          //        }
          //    }
          //}
          //public bool ImportExport_AssetFile
          //{
          //    get
          //    {
          //        return this._ImportExport_AssetFile;
          //    }
          //    set
          //    {
          //        if ((this._ImportExport_AssetFile != value))
          //        {
          //            this._ImportExport_AssetFile = value;
          //        }
          //    }
          //}

         
          //public bool IsPowerPromoReportsRequired
          //{
          //    get
          //    {
          //        return this._IsPowerPromoReportsRequired;
          //    }
          //    set
          //    {
          //        if ((this._IsPowerPromoReportsRequired != value))
          //        {
          //            this._IsPowerPromoReportsRequired = value;
          //        }
          //    }
          //}

       
          //public bool CentralizedDeclaration
          //{
          //    get
          //    {
          //        return this._CentralizedDeclaration;
          //    }
          //    set
          //    {
          //        if ((this._CentralizedDeclaration != value))
          //        {
          //            this._CentralizedDeclaration = value;
          //        }
          //    }
          //}

         
          //public bool IsEmployeecardTrackingEnabled
          //{
          //    get
          //    {
          //        return this._IsEmployeecardTrackingEnabled;
          //    }
          //    set
          //    {
          //        if ((this._IsEmployeecardTrackingEnabled != value))
          //        {
          //            this._IsEmployeecardTrackingEnabled = value;
          //        }
          //    }
          //}

          
          //public bool AllowOfflineDeclaration
          //{
          //    get
          //    {
          //        return this._AllowOfflineDeclaration;
          //    }
          //    set
          //    {
          //        if ((this._AllowOfflineDeclaration != value))
          //        {
          //            this._AllowOfflineDeclaration = value;
          //        }
          //    }
          //}

          
          //public bool AddShortpayInVoucherOut
          //{
          //    get
          //    {
          //        return this._AddShortpayInVoucherOut;
          //    }
          //    set
          //    {
          //        if ((this._AddShortpayInVoucherOut != value))
          //        {
          //            this._AddShortpayInVoucherOut = value;
          //        }
          //    }
          //}

         
          //public bool SystemSettingsDisplayTabVisible
          //{
          //    get
          //    {
          //        return this._SystemSettingsDisplayTabVisible;
          //    }
          //    set
          //    {
          //        if ((this._SystemSettingsDisplayTabVisible != value))
          //        {
          //            this._SystemSettingsDisplayTabVisible = value;
          //        }
          //    }
          //}

         
          //public bool SystemSettingsServiceTabVisible
          //{
          //    get
          //    {
          //        return this._SystemSettingsServiceTabVisible;
          //    }
          //    set
          //    {
          //        if ((this._SystemSettingsServiceTabVisible != value))
          //        {
          //            this._SystemSettingsServiceTabVisible = value;
          //        }
          //    }
          //}
      }
    }

