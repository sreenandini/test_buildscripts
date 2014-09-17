using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Reflection;
using System.Text;
using BMC.CoreLib;

namespace BMC.ExComms.DataLayer.MSSQL
{
    public partial class ExCommsDataContext
    {
        public rsp_GetSiteDetailsResult GetSiteDetails()
        {
            try
            {
                using (ExCommsSQLDataAccess DataContext = this.GetDataContext())
                {
                    return DataContext.rsp_GetSiteDetails().FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
                return null;
            }
        }
    }

    public partial class ExCommsSQLDataAccess : System.Data.Linq.DataContext
    {
        [Function(Name = "dbo.rsp_GetSiteDetails")]
        public ISingleResult<rsp_GetSiteDetailsResult> rsp_GetSiteDetails()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<rsp_GetSiteDetailsResult>)(result.ReturnValue));
        }
    }

    public partial class rsp_GetSiteDetailsResult
    {

        private int _Site_No;

        private string _Code;

        private string _Name;

        private string _Address;

        private string _Phone;

        private string _Fax;

        private string _EMail;

        private string _Manager;

        private string _Open_Mon;

        private string _Open_Tue;

        private string _Open_Wed;

        private string _Open_Thu;

        private string _Open_Fri;

        private string _Open_Sat;

        private string _Open_Sun;

        private string _Site_Security;

        private System.Nullable<int> _Depot_ID;

        private string _Site_Address_1;

        private string _Site_Address_2;

        private string _Site_Address_3;

        private string _Site_Address_4;

        private string _Site_Address_5;

        private string _Site_PostCode;

        private string _Site_Supplier_Code;

        private System.Nullable<int> _TBR_Enabled;

        private int _Site_CJ_Network_Num;

        private string _Region;

        private string _SiteEvent;

        private string _SiteStatus;

        private bool _Site_Enabled;

        private System.Nullable<int> _IsTITOEnabled;

        private System.Nullable<int> _IsNonCashVoucherEnabled;

        private System.Nullable<int> _IsTITOUpdated;

        private System.Xml.Linq.XElement _SiteServiceStatus;

        private System.Nullable<int> _IsCrossTicketingEnabled;

        private System.Nullable<int> _StackerLimitPercentage;

        private System.Nullable<bool> _IsSiteLicensingEnabled;

        private System.Nullable<bool> _SiteLicensing_DisableGames;

        private string _FactoryResetStatus;

        private System.Nullable<int> _Ext_Site_Code;

        public rsp_GetSiteDetailsResult()
        {
        }

        [Column(Storage = "_Site_No", DbType = "Int NOT NULL")]
        public int Site_No
        {
            get
            {
                return this._Site_No;
            }
            set
            {
                if ((this._Site_No != value))
                {
                    this._Site_No = value;
                }
            }
        }

        [Column(Storage = "_Code", DbType = "VarChar(50)")]
        public string Code
        {
            get
            {
                return this._Code;
            }
            set
            {
                if ((this._Code != value))
                {
                    this._Code = value;
                }
            }
        }

        [Column(Storage = "_Name", DbType = "VarChar(50)")]
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

        [Column(Storage = "_Address", DbType = "NText", UpdateCheck = UpdateCheck.Never)]
        public string Address
        {
            get
            {
                return this._Address;
            }
            set
            {
                if ((this._Address != value))
                {
                    this._Address = value;
                }
            }
        }

        [Column(Storage = "_Phone", DbType = "VarChar(50)")]
        public string Phone
        {
            get
            {
                return this._Phone;
            }
            set
            {
                if ((this._Phone != value))
                {
                    this._Phone = value;
                }
            }
        }

        [Column(Storage = "_Fax", DbType = "VarChar(50)")]
        public string Fax
        {
            get
            {
                return this._Fax;
            }
            set
            {
                if ((this._Fax != value))
                {
                    this._Fax = value;
                }
            }
        }

        [Column(Storage = "_EMail", DbType = "VarChar(50)")]
        public string EMail
        {
            get
            {
                return this._EMail;
            }
            set
            {
                if ((this._EMail != value))
                {
                    this._EMail = value;
                }
            }
        }

        [Column(Storage = "_Manager", DbType = "VarChar(50)")]
        public string Manager
        {
            get
            {
                return this._Manager;
            }
            set
            {
                if ((this._Manager != value))
                {
                    this._Manager = value;
                }
            }
        }

        [Column(Storage = "_Open_Mon", DbType = "VarChar(96)")]
        public string Open_Mon
        {
            get
            {
                return this._Open_Mon;
            }
            set
            {
                if ((this._Open_Mon != value))
                {
                    this._Open_Mon = value;
                }
            }
        }

        [Column(Storage = "_Open_Tue", DbType = "VarChar(96)")]
        public string Open_Tue
        {
            get
            {
                return this._Open_Tue;
            }
            set
            {
                if ((this._Open_Tue != value))
                {
                    this._Open_Tue = value;
                }
            }
        }

        [Column(Storage = "_Open_Wed", DbType = "VarChar(96)")]
        public string Open_Wed
        {
            get
            {
                return this._Open_Wed;
            }
            set
            {
                if ((this._Open_Wed != value))
                {
                    this._Open_Wed = value;
                }
            }
        }

        [Column(Storage = "_Open_Thu", DbType = "VarChar(96)")]
        public string Open_Thu
        {
            get
            {
                return this._Open_Thu;
            }
            set
            {
                if ((this._Open_Thu != value))
                {
                    this._Open_Thu = value;
                }
            }
        }

        [Column(Storage = "_Open_Fri", DbType = "VarChar(96)")]
        public string Open_Fri
        {
            get
            {
                return this._Open_Fri;
            }
            set
            {
                if ((this._Open_Fri != value))
                {
                    this._Open_Fri = value;
                }
            }
        }

        [Column(Storage = "_Open_Sat", DbType = "VarChar(96)")]
        public string Open_Sat
        {
            get
            {
                return this._Open_Sat;
            }
            set
            {
                if ((this._Open_Sat != value))
                {
                    this._Open_Sat = value;
                }
            }
        }

        [Column(Storage = "_Open_Sun", DbType = "VarChar(96)")]
        public string Open_Sun
        {
            get
            {
                return this._Open_Sun;
            }
            set
            {
                if ((this._Open_Sun != value))
                {
                    this._Open_Sun = value;
                }
            }
        }

        [Column(Storage = "_Site_Security", DbType = "NText", UpdateCheck = UpdateCheck.Never)]
        public string Site_Security
        {
            get
            {
                return this._Site_Security;
            }
            set
            {
                if ((this._Site_Security != value))
                {
                    this._Site_Security = value;
                }
            }
        }

        [Column(Storage = "_Depot_ID", DbType = "Int")]
        public System.Nullable<int> Depot_ID
        {
            get
            {
                return this._Depot_ID;
            }
            set
            {
                if ((this._Depot_ID != value))
                {
                    this._Depot_ID = value;
                }
            }
        }

        [Column(Storage = "_Site_Address_1", DbType = "VarChar(50)")]
        public string Site_Address_1
        {
            get
            {
                return this._Site_Address_1;
            }
            set
            {
                if ((this._Site_Address_1 != value))
                {
                    this._Site_Address_1 = value;
                }
            }
        }

        [Column(Storage = "_Site_Address_2", DbType = "VarChar(50)")]
        public string Site_Address_2
        {
            get
            {
                return this._Site_Address_2;
            }
            set
            {
                if ((this._Site_Address_2 != value))
                {
                    this._Site_Address_2 = value;
                }
            }
        }

        [Column(Storage = "_Site_Address_3", DbType = "VarChar(50)")]
        public string Site_Address_3
        {
            get
            {
                return this._Site_Address_3;
            }
            set
            {
                if ((this._Site_Address_3 != value))
                {
                    this._Site_Address_3 = value;
                }
            }
        }

        [Column(Storage = "_Site_Address_4", DbType = "VarChar(50)")]
        public string Site_Address_4
        {
            get
            {
                return this._Site_Address_4;
            }
            set
            {
                if ((this._Site_Address_4 != value))
                {
                    this._Site_Address_4 = value;
                }
            }
        }

        [Column(Storage = "_Site_Address_5", DbType = "VarChar(50)")]
        public string Site_Address_5
        {
            get
            {
                return this._Site_Address_5;
            }
            set
            {
                if ((this._Site_Address_5 != value))
                {
                    this._Site_Address_5 = value;
                }
            }
        }

        [Column(Storage = "_Site_PostCode", DbType = "VarChar(50)")]
        public string Site_PostCode
        {
            get
            {
                return this._Site_PostCode;
            }
            set
            {
                if ((this._Site_PostCode != value))
                {
                    this._Site_PostCode = value;
                }
            }
        }

        [Column(Storage = "_Site_Supplier_Code", DbType = "VarChar(50)")]
        public string Site_Supplier_Code
        {
            get
            {
                return this._Site_Supplier_Code;
            }
            set
            {
                if ((this._Site_Supplier_Code != value))
                {
                    this._Site_Supplier_Code = value;
                }
            }
        }

        [Column(Storage = "_TBR_Enabled", DbType = "Int")]
        public System.Nullable<int> TBR_Enabled
        {
            get
            {
                return this._TBR_Enabled;
            }
            set
            {
                if ((this._TBR_Enabled != value))
                {
                    this._TBR_Enabled = value;
                }
            }
        }

        [Column(Storage = "_Site_CJ_Network_Num", DbType = "Int NOT NULL")]
        public int Site_CJ_Network_Num
        {
            get
            {
                return this._Site_CJ_Network_Num;
            }
            set
            {
                if ((this._Site_CJ_Network_Num != value))
                {
                    this._Site_CJ_Network_Num = value;
                }
            }
        }

        [Column(Storage = "_Region", DbType = "VarChar(10)")]
        public string Region
        {
            get
            {
                return this._Region;
            }
            set
            {
                if ((this._Region != value))
                {
                    this._Region = value;
                }
            }
        }

        [Column(Storage = "_SiteEvent", DbType = "VarChar(300)")]
        public string SiteEvent
        {
            get
            {
                return this._SiteEvent;
            }
            set
            {
                if ((this._SiteEvent != value))
                {
                    this._SiteEvent = value;
                }
            }
        }

        [Column(Storage = "_SiteStatus", DbType = "VarChar(300)")]
        public string SiteStatus
        {
            get
            {
                return this._SiteStatus;
            }
            set
            {
                if ((this._SiteStatus != value))
                {
                    this._SiteStatus = value;
                }
            }
        }

        [Column(Storage = "_Site_Enabled", DbType = "Bit NOT NULL")]
        public bool Site_Enabled
        {
            get
            {
                return this._Site_Enabled;
            }
            set
            {
                if ((this._Site_Enabled != value))
                {
                    this._Site_Enabled = value;
                }
            }
        }

        [Column(Storage = "_IsTITOEnabled", DbType = "Int")]
        public System.Nullable<int> IsTITOEnabled
        {
            get
            {
                return this._IsTITOEnabled;
            }
            set
            {
                if ((this._IsTITOEnabled != value))
                {
                    this._IsTITOEnabled = value;
                }
            }
        }

        [Column(Storage = "_IsNonCashVoucherEnabled", DbType = "Int")]
        public System.Nullable<int> IsNonCashVoucherEnabled
        {
            get
            {
                return this._IsNonCashVoucherEnabled;
            }
            set
            {
                if ((this._IsNonCashVoucherEnabled != value))
                {
                    this._IsNonCashVoucherEnabled = value;
                }
            }
        }

        [Column(Storage = "_IsTITOUpdated", DbType = "Int")]
        public System.Nullable<int> IsTITOUpdated
        {
            get
            {
                return this._IsTITOUpdated;
            }
            set
            {
                if ((this._IsTITOUpdated != value))
                {
                    this._IsTITOUpdated = value;
                }
            }
        }

        [Column(Storage = "_SiteServiceStatus", DbType = "Xml")]
        public System.Xml.Linq.XElement SiteServiceStatus
        {
            get
            {
                return this._SiteServiceStatus;
            }
            set
            {
                if ((this._SiteServiceStatus != value))
                {
                    this._SiteServiceStatus = value;
                }
            }
        }

        [Column(Storage = "_IsCrossTicketingEnabled", DbType = "Int")]
        public System.Nullable<int> IsCrossTicketingEnabled
        {
            get
            {
                return this._IsCrossTicketingEnabled;
            }
            set
            {
                if ((this._IsCrossTicketingEnabled != value))
                {
                    this._IsCrossTicketingEnabled = value;
                }
            }
        }

        [Column(Storage = "_StackerLimitPercentage", DbType = "Int")]
        public System.Nullable<int> StackerLimitPercentage
        {
            get
            {
                return this._StackerLimitPercentage;
            }
            set
            {
                if ((this._StackerLimitPercentage != value))
                {
                    this._StackerLimitPercentage = value;
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

        [Column(Storage = "_SiteLicensing_DisableGames", DbType = "Bit")]
        public System.Nullable<bool> SiteLicensing_DisableGames
        {
            get
            {
                return this._SiteLicensing_DisableGames;
            }
            set
            {
                if ((this._SiteLicensing_DisableGames != value))
                {
                    this._SiteLicensing_DisableGames = value;
                }
            }
        }

        [Column(Storage = "_FactoryResetStatus", DbType = "VarChar(15) NOT NULL", CanBeNull = false)]
        public string FactoryResetStatus
        {
            get
            {
                return this._FactoryResetStatus;
            }
            set
            {
                if ((this._FactoryResetStatus != value))
                {
                    this._FactoryResetStatus = value;
                }
            }
        }

        [Column(Storage = "_Ext_Site_Code", DbType = "Int")]
        public System.Nullable<int> Ext_Site_Code
        {
            get
            {
                return this._Ext_Site_Code;
            }
            set
            {
                if ((this._Ext_Site_Code != value))
                {
                    this._Ext_Site_Code = value;
                }
            }
        }
    }
}
