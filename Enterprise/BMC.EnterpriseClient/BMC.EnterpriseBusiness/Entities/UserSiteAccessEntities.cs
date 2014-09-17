using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;

namespace BMC.EnterpriseBusiness.Entities
{
    public class UserSiteAccessEntities
    {
    }

    public class CompaniesResult
    {

        private int _Company_ID;

        private string _Company_Name;

        private int _Sub_Company_ID;

        private string _Sub_Company_Name;

        private bool _isUpdated;

        public CompaniesResult()
        {
            _isUpdated = false;
        }

        public bool IsUpdated
        {
            get { return this._isUpdated; }
            set
            {
                if ((this._isUpdated != value))
                    this._isUpdated = value;
            }
        }
        
        [Column(Storage = "_Company_ID", DbType = "Int NOT NULL")]
        public int Company_ID
        {
            get
            {
                return this._Company_ID;
            }
            set
            {
                if ((this._Company_ID != value))
                {
                    this._Company_ID = value;
                }
            }
        }

        [Column(Storage = "_Company_Name", DbType = "VarChar(50)")]
        public string Company_Name
        {
            get
            {
                return this._Company_Name;
            }
            set
            {
                if ((this._Company_Name != value))
                {
                    this._Company_Name = value;
                }
            }
        }

        [Column(Storage = "_Sub_Company_ID", DbType = "Int NOT NULL")]
        public int Sub_Company_ID
        {
            get
            {
                return this._Sub_Company_ID;
            }
            set
            {
                if ((this._Sub_Company_ID != value))
                {
                    this._Sub_Company_ID = value;
                }
            }
        }

        [Column(Storage = "_Sub_Company_Name", DbType = "VarChar(50)")]
        public string Sub_Company_Name
        {
            get
            {
                return this._Sub_Company_Name;
            }
            set
            {
                if ((this._Sub_Company_Name != value))
                {
                    this._Sub_Company_Name = value;
                }
            }
        }
    }

    public partial class OperatorsResult
    {

        private int _Operator_ID;

        private string _Operator_Name;

        private int _Depot_ID;

        private string _Depot_Name;

        private bool _isUpdated;

        public OperatorsResult()
        {
            _isUpdated = false;
        }

        public bool IsUpdated
        {
            get { return this._isUpdated; }
            set
            {
                if ((this._isUpdated != value))
                    this._isUpdated = value;
            }
        }

        [Column(Storage = "_Operator_ID", DbType = "Int NOT NULL")]
        public int Operator_ID
        {
            get
            {
                return this._Operator_ID;
            }
            set
            {
                if ((this._Operator_ID != value))
                {
                    this._Operator_ID = value;
                }
            }
        }

        [Column(Storage = "_Operator_Name", DbType = "VarChar(50)")]
        public string Operator_Name
        {
            get
            {
                return this._Operator_Name;
            }
            set
            {
                if ((this._Operator_Name != value))
                {
                    this._Operator_Name = value;
                }
            }
        }

        [Column(Storage = "_Depot_ID", DbType = "Int NOT NULL")]
        public int Depot_ID
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

        [Column(Storage = "_Depot_Name", DbType = "VarChar(50)")]
        public string Depot_Name
        {
            get
            {
                return this._Depot_Name;
            }
            set
            {
                if ((this._Depot_Name != value))
                {
                    this._Depot_Name = value;
                }
            }
        }
    }

    public partial class SitesResult
    {

        private int _Company_ID;

        private string _Company_Name;

        private int _Sub_Company_ID;

        private string _Sub_Company_Name;

        private int _Site_ID;

        private string _Site_Name;

        private string _Site_Code;

        private string _Site_Address_2;

        private string _Site_Address_3;

        private bool _isUpdated;

        public SitesResult()
        {
            _isUpdated = false;
        }

        public bool IsUpdated
        {
            get { return this._isUpdated; }
            set
            {
                if ((this._isUpdated != value))
                    this._isUpdated = value;
            }
        }

        [Column(Storage = "_Company_ID", DbType = "Int NOT NULL")]
        public int Company_ID
        {
            get
            {
                return this._Company_ID;
            }
            set
            {
                if ((this._Company_ID != value))
                {
                    this._Company_ID = value;
                }
            }
        }

        [Column(Storage = "_Company_Name", DbType = "VarChar(50)")]
        public string Company_Name
        {
            get
            {
                return this._Company_Name;
            }
            set
            {
                if ((this._Company_Name != value))
                {
                    this._Company_Name = value;
                }
            }
        }

        [Column(Storage = "_Sub_Company_ID", DbType = "Int NOT NULL")]
        public int Sub_Company_ID
        {
            get
            {
                return this._Sub_Company_ID;
            }
            set
            {
                if ((this._Sub_Company_ID != value))
                {
                    this._Sub_Company_ID = value;
                }
            }
        }

        [Column(Storage = "_Sub_Company_Name", DbType = "VarChar(50)")]
        public string Sub_Company_Name
        {
            get
            {
                return this._Sub_Company_Name;
            }
            set
            {
                if ((this._Sub_Company_Name != value))
                {
                    this._Sub_Company_Name = value;
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

    }

    public partial class SiteCustomerAccessResult
    {

        private int _Customer_Access_ID;

        private string _Customer_Access_Name;

        private bool _Customer_Access_View_All_Companies;

        private bool _Customer_Access_View_All_Depots;

        private bool _Customer_Access_View_All_Sites;

        public SiteCustomerAccessResult()
        {
        }

        [Column(Storage = "_Customer_Access_ID", DbType = "Int NOT NULL")]
        public int Customer_Access_ID
        {
            get
            {
                return this._Customer_Access_ID;
            }
            set
            {
                if ((this._Customer_Access_ID != value))
                {
                    this._Customer_Access_ID = value;
                }
            }
        }

        [Column(Storage = "_Customer_Access_Name", DbType = "VarChar(50)")]
        public string Customer_Access_Name
        {
            get
            {
                return this._Customer_Access_Name;
            }
            set
            {
                if ((this._Customer_Access_Name != value))
                {
                    this._Customer_Access_Name = value;
                }
            }
        }

        [Column(Storage = "_Customer_Access_View_All_Companies", DbType = "Bit NOT NULL")]
        public bool Customer_Access_View_All_Companies
        {
            get
            {
                return this._Customer_Access_View_All_Companies;
            }
            set
            {
                if ((this._Customer_Access_View_All_Companies != value))
                {
                    this._Customer_Access_View_All_Companies = value;
                }
            }
        }

        [Column(Storage = "_Customer_Access_View_All_Depots", DbType = "Bit NOT NULL")]
        public bool Customer_Access_View_All_Depots
        {
            get
            {
                return this._Customer_Access_View_All_Depots;
            }
            set
            {
                if ((this._Customer_Access_View_All_Depots != value))
                {
                    this._Customer_Access_View_All_Depots = value;
                }
            }
        }

        [Column(Storage = "_Customer_Access_View_All_Sites", DbType = "Bit NOT NULL")]
        public bool Customer_Access_View_All_Sites
        {
            get
            {
                return this._Customer_Access_View_All_Sites;
            }
            set
            {
                if ((this._Customer_Access_View_All_Sites != value))
                {
                    this._Customer_Access_View_All_Sites = value;
                }
            }
        }
    }

    public partial class CustomerAccessSiteResult
    {

        private string _SecurityProfileType_Value;

        private System.Nullable<bool> _AllowUser;

        public CustomerAccessSiteResult()
        {
        }

        [Column(Storage = "_SecurityProfileType_Value", DbType = "VarChar(100)")]
        public string SecurityProfileType_Value
        {
            get
            {
                return this._SecurityProfileType_Value;
            }
            set
            {
                if ((this._SecurityProfileType_Value != value))
                {
                    this._SecurityProfileType_Value = value;
                }
            }
        }

        [Column(Storage = "_AllowUser", DbType = "Bit")]
        public System.Nullable<bool> AllowUser
        {
            get
            {
                return this._AllowUser;
            }
            set
            {
                if ((this._AllowUser != value))
                {
                    this._AllowUser = value;
                }
            }
        }        
    }

    public partial class CustomerAccessDepotResult
    {

        private int _Depot_ID;

        private string _Depot_Name;

        public CustomerAccessDepotResult()
        {
        }

        [Column(Storage = "_Depot_ID", DbType = "Int NOT NULL")]
        public int Depot_ID
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

        [Column(Storage = "_Depot_Name", DbType = "VarChar(50)")]
        public string Depot_Name
        {
            get
            {
                return this._Depot_Name;
            }
            set
            {
                if ((this._Depot_Name != value))
                {
                    this._Depot_Name = value;
                }
            }
        }
    }

    public partial class CustomerAccessCompanyResult
    {

        private int _Sub_Company_ID;

        private string _Sub_Company_Name;

        public CustomerAccessCompanyResult()
        {
        }

        [Column(Storage = "_Sub_Company_ID", DbType = "Int NOT NULL")]
        public int Sub_Company_ID
        {
            get
            {
                return this._Sub_Company_ID;
            }
            set
            {
                if ((this._Sub_Company_ID != value))
                {
                    this._Sub_Company_ID = value;
                }
            }
        }

        [Column(Storage = "_Sub_Company_Name", DbType = "VarChar(50)")]
        public string Sub_Company_Name
        {
            get
            {
                return this._Sub_Company_Name;
            }
            set
            {
                if ((this._Sub_Company_Name != value))
                {
                    this._Sub_Company_Name = value;
                }
            }
        }
    }
	
}
