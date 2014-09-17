using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.EnterpriseBusiness.Entities
{
    public partial class UserAuditEntity
    {
        public string Staff_Last_Name { get; set; }
        public string Staff_First_Name { get; set; }
        public string Staff_Title { get; set; }
        public string Staff_Address { get; set; }
        public string Staff_Postcode { get; set; }
        public string Staff_Phone_No { get; set; }
        public string Staff_Mobile_No { get; set; }
        public string Staff_Extension_No { get; set; }
        public string Staff_Job_Title { get; set; }
        public string Staff_Department { get; set; }      
        public string Staff_Username { get; set; }
        public string WindowsUserName { get; set; }
        public string UserLevel { get; set; }
        public string Email_Address { get; set; }
        public string Staff_Personnel_No { get; set;}
        public string Staff_Notes { get; set; }
        public string Staff_IsACollector {get; set;}
        public bool Staff_IsAnEngineer{get; set;}
        public bool Staff_IsARepresentative{get; set;}
        public bool Staff_IsAStockController{get; set;}
        public string Operator_Name { get; set; }
        public string Depot_Name { get; set; }
        public string ServiceArea { get; set; }
        public string User_Language { get; set; }
        public string Currency_Format { get; set; }
        public string Date_Format { get; set; }
        public System.Nullable<bool> Staff_Terminated { get; set; }        
        public string CustomerAccessNotIncluded { get; set; }
        public string CustomerAccessIncluded { get; set; }
        public string Password { get; set; }
        public string Confirm_Password { get; set; }
       
    }


    public partial class ServiceAreasDetailsResult
    {

        private int _Service_Area_ID;

        private string _Service_Area_Name;

        public ServiceAreasDetailsResult()
        {
        }

        public int Service_Area_ID
        {
            get
            {
                return this._Service_Area_ID;
            }
            set
            {
                if ((this._Service_Area_ID != value))
                {
                    this._Service_Area_ID = value;
                }
            }
        }

        public string Service_Area_Name
        {
            get
            {
                return this._Service_Area_Name;
            }
            set
            {
                if ((this._Service_Area_Name != value))
                {
                    this._Service_Area_Name = value;
                }
            }
        }
    }

    public partial class CustomerAccessDetailsResult
    {

        private int _Customer_Access_ID;

        private string _Customer_Access_Name;

        public CustomerAccessDetailsResult()
        {
        }

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
    }

    public partial class UserGroupDetailsResult
    {

        private int _User_Group_ID;

        private string _User_Group_Name;

        private System.Nullable<int> _HQ_User_Access_ID;

        public UserGroupDetailsResult()
        {
        }

        public int User_Group_ID
        {
            get
            {
                return this._User_Group_ID;
            }
            set
            {
                if ((this._User_Group_ID != value))
                {
                    this._User_Group_ID = value;
                }
            }
        }

        public string User_Group_Name
        {
            get
            {
                return this._User_Group_Name;
            }
            set
            {
                if ((this._User_Group_Name != value))
                {
                    this._User_Group_Name = value;
                }
            }
        }

        public System.Nullable<int> HQ_User_Access_ID
        {
            get
            {
                return this._HQ_User_Access_ID;
            }
            set
            {
                if ((this._HQ_User_Access_ID != value))
                {
                    this._HQ_User_Access_ID = value;
                }
            }
        }
    }

    public partial class StaffDetailsResult
    {

        private int _Staff_ID;

        private System.Nullable<int> _Operator_ID;

        private System.Nullable<int> _User_Group_ID;

        private string _Staff_First_Name;

        private string _Staff_Last_Name;

        private string _Staff_Title;

        private string _Staff_Address;

        private string _Staff_Postcode;

        private string _Staff_Phone_No;

        private string _Staff_Extension_No;

        private string _Staff_Mobile_No;

        private string _Staff_Job_Title;

        private string _Staff_Department;

        private System.Nullable<bool> _Staff_IsACollector;

        private System.Nullable<bool> _Staff_IsAnEngineer;

        private System.Nullable<bool> _Staff_IsARepresentative;

        private System.Nullable<bool> _Staff_IsAStockController;

        private string _Staff_Start_Date;

        private string _Staff_End_Date;

        private string _Staff_Username;

        private string _Staff_Password;

        private System.Nullable<int> _Depot_ID;

        private System.Nullable<int> _Service_Area_ID;

        private System.Nullable<int> _Supplier_ID;

        private string _Staff_Personel_No;

        private System.Nullable<bool> _Staff_Terminated;

        private string _Staff_Notes;

        private string _Email_Address;

        private System.Nullable<int> _UserTableID;

        public StaffDetailsResult()
        {
        }

        public int Staff_ID
        {
            get
            {
                return this._Staff_ID;
            }
            set
            {
                if ((this._Staff_ID != value))
                {
                    this._Staff_ID = value;
                }
            }
        }

        public System.Nullable<int> Operator_ID
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

        public System.Nullable<int> User_Group_ID
        {
            get
            {
                return this._User_Group_ID;
            }
            set
            {
                if ((this._User_Group_ID != value))
                {
                    this._User_Group_ID = value;
                }
            }
        }

        public string Staff_First_Name
        {
            get
            {
                return this._Staff_First_Name;
            }
            set
            {
                if ((this._Staff_First_Name != value))
                {
                    this._Staff_First_Name = value;
                }
            }
        }

        public string Staff_Last_Name
        {
            get
            {
                return this._Staff_Last_Name;
            }
            set
            {
                if ((this._Staff_Last_Name != value))
                {
                    this._Staff_Last_Name = value;
                }
            }
        }

        public string Staff_Title
        {
            get
            {
                return this._Staff_Title;
            }
            set
            {
                if ((this._Staff_Title != value))
                {
                    this._Staff_Title = value;
                }
            }
        }

        public string Staff_Address
        {
            get
            {
                return this._Staff_Address;
            }
            set
            {
                if ((this._Staff_Address != value))
                {
                    this._Staff_Address = value;
                }
            }
        }

        public string Staff_Postcode
        {
            get
            {
                return this._Staff_Postcode;
            }
            set
            {
                if ((this._Staff_Postcode != value))
                {
                    this._Staff_Postcode = value;
                }
            }
        }

        public string Staff_Phone_No
        {
            get
            {
                return this._Staff_Phone_No;
            }
            set
            {
                if ((this._Staff_Phone_No != value))
                {
                    this._Staff_Phone_No = value;
                }
            }
        }

        public string Staff_Extension_No
        {
            get
            {
                return this._Staff_Extension_No;
            }
            set
            {
                if ((this._Staff_Extension_No != value))
                {
                    this._Staff_Extension_No = value;
                }
            }
        }

        public string Staff_Mobile_No
        {
            get
            {
                return this._Staff_Mobile_No;
            }
            set
            {
                if ((this._Staff_Mobile_No != value))
                {
                    this._Staff_Mobile_No = value;
                }
            }
        }

        public string Staff_Job_Title
        {
            get
            {
                return this._Staff_Job_Title;
            }
            set
            {
                if ((this._Staff_Job_Title != value))
                {
                    this._Staff_Job_Title = value;
                }
            }
        }

        public string Staff_Department
        {
            get
            {
                return this._Staff_Department;
            }
            set
            {
                if ((this._Staff_Department != value))
                {
                    this._Staff_Department = value;
                }
            }
        }

        public System.Nullable<bool> Staff_IsACollector
        {
            get
            {
                return this._Staff_IsACollector;
            }
            set
            {
                if ((this._Staff_IsACollector != value))
                {
                    this._Staff_IsACollector = value;
                }
            }
        }

        public System.Nullable<bool> Staff_IsAnEngineer
        {
            get
            {
                return this._Staff_IsAnEngineer;
            }
            set
            {
                if ((this._Staff_IsAnEngineer != value))
                {
                    this._Staff_IsAnEngineer = value;
                }
            }
        }

        public System.Nullable<bool> Staff_IsARepresentative
        {
            get
            {
                return this._Staff_IsARepresentative;
            }
            set
            {
                if ((this._Staff_IsARepresentative != value))
                {
                    this._Staff_IsARepresentative = value;
                }
            }
        }

        public System.Nullable<bool> Staff_IsAStockController
        {
            get
            {
                return this._Staff_IsAStockController;
            }
            set
            {
                if ((this._Staff_IsAStockController != value))
                {
                    this._Staff_IsAStockController = value;
                }
            }
        }

        public string Staff_Start_Date
        {
            get
            {
                return this._Staff_Start_Date;
            }
            set
            {
                if ((this._Staff_Start_Date != value))
                {
                    this._Staff_Start_Date = value;
                }
            }
        }

        public string Staff_End_Date
        {
            get
            {
                return this._Staff_End_Date;
            }
            set
            {
                if ((this._Staff_End_Date != value))
                {
                    this._Staff_End_Date = value;
                }
            }
        }

        public string Staff_Username
        {
            get
            {
                return this._Staff_Username;
            }
            set
            {
                if ((this._Staff_Username != value))
                {
                    this._Staff_Username = value;
                }
            }
        }

        public string Staff_Password
        {
            get
            {
                return this._Staff_Password;
            }
            set
            {
                if ((this._Staff_Password != value))
                {
                    this._Staff_Password = value;
                }
            }
        }

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

        public System.Nullable<int> Service_Area_ID
        {
            get
            {
                return this._Service_Area_ID;
            }
            set
            {
                if ((this._Service_Area_ID != value))
                {
                    this._Service_Area_ID = value;
                }
            }
        }

        public System.Nullable<int> Supplier_ID
        {
            get
            {
                return this._Supplier_ID;
            }
            set
            {
                if ((this._Supplier_ID != value))
                {
                    this._Supplier_ID = value;
                }
            }
        }

        public string Staff_Personel_No
        {
            get
            {
                return this._Staff_Personel_No;
            }
            set
            {
                if ((this._Staff_Personel_No != value))
                {
                    this._Staff_Personel_No = value;
                }
            }
        }

        public System.Nullable<bool> Staff_Terminated
        {
            get
            {
                return this._Staff_Terminated;
            }
            set
            {
                if ((this._Staff_Terminated != value))
                {
                    this._Staff_Terminated = value;
                }
            }
        }

        public string Staff_Notes
        {
            get
            {
                return this._Staff_Notes;
            }
            set
            {
                if ((this._Staff_Notes != value))
                {
                    this._Staff_Notes = value;
                }
            }
        }

        public string Email_Address
        {
            get
            {
                return this._Email_Address;
            }
            set
            {
                if ((this._Email_Address != value))
                {
                    this._Email_Address = value;
                }
            }
        }


        public System.Nullable<int> UserTableID
        {
            get
            {
                return this._UserTableID;
            }
            set
            {
                if ((this._UserTableID != value))
                {
                    this._UserTableID = value;
                }
            }
        }
    }

    public partial class StaffNameResult
    {

        private int _Staff_ID;

        private string _Staff_First_Name;

        private string _Staff_Last_Name;

        private int _UserTableID;

        public StaffNameResult()
        {
        }

        public int Staff_ID
        {
            get
            {
                return this._Staff_ID;
            }
            set
            {
                if ((this._Staff_ID != value))
                {
                    this._Staff_ID = value;
                }
            }
        }

        public string Staff_Name
        {
            get;
            set;
        }

        public string Staff_Representative_Name
        {
            get;
            set;
        }
        public string Staff_First_Name
        {
            get
            {
                return this._Staff_First_Name;
            }
            set
            {
                if ((this._Staff_First_Name != value))
                {
                    this._Staff_First_Name = value;
                }
            }
        }

        public string Staff_Last_Name
        {
            get
            {
                return this._Staff_Last_Name;
            }
            set
            {
                if ((this._Staff_Last_Name != value))
                {
                    this._Staff_Last_Name = value;
                }
            }
        }

        public int UserTableID
        {
            get
            {
                return this._UserTableID;
            }
            set
            {
                if ((this._UserTableID != value))
                {
                    this._UserTableID = value;
                }
            }
        }
    }

    public partial class UserLanguagesDetailsResult
    {

        private int _LanguageID;

        private string _LanguageName;

        private string _CultureInfo;

        private string _LanguageDisplayName;

        public UserLanguagesDetailsResult()
        {
        }

        public string LanguageDisplayName
        {
            get
            {
                return this._LanguageDisplayName;
            }
            set
            {
                if ((this._LanguageDisplayName != value))
                {
                    this._LanguageDisplayName = value;
                }
            }
        }

        public int LanguageID
        {
            get
            {
                return this._LanguageID;
            }
            set
            {
                if ((this._LanguageID != value))
                {
                    this._LanguageID = value;
                }
            }
        }

        public string LanguageName
        {
            get
            {
                return this._LanguageName;
            }
            set
            {
                if ((this._LanguageName != value))
                {
                    this._LanguageName = value;
                }
            }
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

    public partial class StaffCustomerAccessResult
    {

        private int _Staff_Customer_Access_ID;

        private System.Nullable<int> _Staff_ID;

        private System.Nullable<int> _Customer_Access_ID;

        public StaffCustomerAccessResult()
        {
        }


        public int Staff_Customer_Access_ID
        {
            get
            {
                return this._Staff_Customer_Access_ID;
            }
            set
            {
                if ((this._Staff_Customer_Access_ID != value))
                {
                    this._Staff_Customer_Access_ID = value;
                }
            }
        }


        public System.Nullable<int> Staff_ID
        {
            get
            {
                return this._Staff_ID;
            }
            set
            {
                if ((this._Staff_ID != value))
                {
                    this._Staff_ID = value;
                }
            }
        }


        public System.Nullable<int> Customer_Access_ID
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
    }

    public partial class UserDetailsResult
    {

        private int _SecurityUserID;

        private string _WindowsUserName;

        private string _UserName;

        private string _Password;

        private System.Nullable<int> _LanguageID;

        private System.Nullable<int> _CurrencyCulture;

        private System.Nullable<int> _DateCulture;

        private System.Nullable<bool> _ChangePassword;

        private System.Nullable<System.DateTime> _CreatedDate;

        private System.Nullable<System.DateTime> _PasswordChangeDate;

        private System.Nullable<bool> _isReset;

        private bool _isLocked;

        public UserDetailsResult()
        {
        }

        public int SecurityUserID
        {
            get
            {
                return this._SecurityUserID;
            }
            set
            {
                if ((this._SecurityUserID != value))
                {
                    this._SecurityUserID = value;
                }
            }
        }

        public string WindowsUserName
        {
            get
            {
                return this._WindowsUserName;
            }
            set
            {
                if ((this._WindowsUserName != value))
                {
                    this._WindowsUserName = value;
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

        public string Password
        {
            get
            {
                return this._Password;
            }
            set
            {
                if ((this._Password != value))
                {
                    this._Password = value;
                }
            }
        }

        public System.Nullable<int> LanguageID
        {
            get
            {
                return this._LanguageID;
            }
            set
            {
                if ((this._LanguageID != value))
                {
                    this._LanguageID = value;
                }
            }
        }

        public System.Nullable<int> CurrencyCulture
        {
            get
            {
                return this._CurrencyCulture;
            }
            set
            {
                if ((this._CurrencyCulture != value))
                {
                    this._CurrencyCulture = value;
                }
            }
        }

        public System.Nullable<int> DateCulture
        {
            get
            {
                return this._DateCulture;
            }
            set
            {
                if ((this._DateCulture != value))
                {
                    this._DateCulture = value;
                }
            }
        }

        public System.Nullable<bool> ChangePassword
        {
            get
            {
                return this._ChangePassword;
            }
            set
            {
                if ((this._ChangePassword != value))
                {
                    this._ChangePassword = value;
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

        public System.Nullable<System.DateTime> PasswordChangeDate
        {
            get
            {
                return this._PasswordChangeDate;
            }
            set
            {
                if ((this._PasswordChangeDate != value))
                {
                    this._PasswordChangeDate = value;
                }
            }
        }

        public System.Nullable<bool> isReset
        {
            get
            {
                return this._isReset;
            }
            set
            {
                if ((this._isReset != value))
                {
                    this._isReset = value;
                }
            }
        }

        public bool isLocked
        {
            get
            {
                return this._isLocked;
            }
            set
            {
                if ((this._isLocked != value))
                {
                    this._isLocked = value;
                }
            }
        }
    }

    public partial class StaffDepotResult
    {

        private System.Nullable<int> _Depot_ID;

        public StaffDepotResult()
        {
        }

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
    }
    public partial class UserLockStatusResult
    {
        public string RESULT { get; set; }
    }

    public partial class ActiveSiteDetailsforuserResult
    {

        private int _Site_ID;

        private string _Site_Code;

        private string _Site_Name;

        private string _SC_ExchangeConnectionSting;

        private string _SC_TicketConnectionSting;

        public ActiveSiteDetailsforuserResult()
        {
        }

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

        public string SC_ExchangeConnectionSting
        {
            get
            {
                return this._SC_ExchangeConnectionSting;
            }
            set
            {
                if ((this._SC_ExchangeConnectionSting != value))
                {
                    this._SC_ExchangeConnectionSting = value;
                }
            }
        }

        public string SC_TicketConnectionSting
        {
            get
            {
                return this._SC_TicketConnectionSting;
            }
            set
            {
                if ((this._SC_TicketConnectionSting != value))
                {
                    this._SC_TicketConnectionSting = value;
                }
            }
        }
    }

    //public partial class OperatorandDepotDetailsResult
    //{

    //    private int _Operator_ID;

    //    private string _Operator_Name;

    //    private System.Nullable<int> _Depot_ID;

    //    private string _Depot_Name;

    //    public OperatorandDepotDetailsResult()
    //    {
    //    }

    //    public int Operator_ID
    //    {
    //        get
    //        {
    //            return this._Operator_ID;
    //        }
    //        set
    //        {
    //            if ((this._Operator_ID != value))
    //            {
    //                this._Operator_ID = value;
    //            }
    //        }
    //    }

    //    public string Operator_Name
    //    {
    //        get
    //        {
    //            return this._Operator_Name;
    //        }
    //        set
    //        {
    //            if ((this._Operator_Name != value))
    //            {
    //                this._Operator_Name = value;
    //            }
    //        }
    //    }

    //    public System.Nullable<int> Depot_ID
    //    {
    //        get
    //        {
    //            return this._Depot_ID;
    //        }
    //        set
    //        {
    //            if ((this._Depot_ID != value))
    //            {
    //                this._Depot_ID = value;
    //            }
    //        }
    //    }

    //    public string Depot_Name
    //    {
    //        get
    //        {
    //            return this._Depot_Name;
    //        }
    //        set
    //        {
    //            if ((this._Depot_Name != value))
    //            {
    //                this._Depot_Name = value;
    //            }
    //        }
    //    }
    //}
}
