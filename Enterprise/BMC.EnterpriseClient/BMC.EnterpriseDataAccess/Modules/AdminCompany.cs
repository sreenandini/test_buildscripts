using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;
using System.Data.Linq;
using System.Reflection;

namespace BMC.EnterpriseDataAccess
{
    public partial class EnterpriseDataContext
    {
        
        [Function(Name = "dbo.rsp_ecGetCompanyAdminDetails")]
        [ResultType(typeof(TermsResult))]
        [ResultType(typeof(AccessKeyResult))]
        [ResultType(typeof(StaffResult))]
        [ResultType(typeof(CompanyResult))]
        public IMultipleResults GetCompanyAdminDetails([Parameter(Name = "CompanyID", DbType = "Int")] int companyID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), companyID);
            return ((IMultipleResults)(result.ReturnValue));
        }

        [Function(Name = "dbo.usp_ecUpdateCompanyDetails")]
        public ISingleResult<usp_ecUpdateCompanyDetailsResult> UpdateCompanyDetails([Parameter(Name = "Company_ID", DbType = "Int")] System.Nullable<int> company_ID, [Parameter(Name = "Company_Name", DbType = "VarChar(50)")] string company_Name, [Parameter(Name = "Company_Switchboard_Phone_No", DbType = "VarChar(15)")] string company_Switchboard_Phone_No, [Parameter(Name = "Company_Address_1", DbType = "VarChar(50)")] string company_Address_1, [Parameter(Name = "Company_Address_2", DbType = "VarChar(50)")] string company_Address_2, [Parameter(Name = "Company_Address_3", DbType = "VarChar(50)")] string company_Address_3, [Parameter(Name = "Company_Address_4", DbType = "VarChar(50)")] string company_Address_4, [Parameter(Name = "Company_Address_5", DbType = "VarChar(50)")] string company_Address_5, [Parameter(Name = "Company_Postcode", DbType = "VarChar(15)")] string company_Postcode, [Parameter(Name = "Company_Contact_Name", DbType = "VarChar(50)")] string company_Contact_Name, [Parameter(Name = "Company_Contact_Phone_No", DbType = "VarChar(15)")] string company_Contact_Phone_No, [Parameter(Name = "Company_Contact_Email_Address", DbType = "VarChar(50)")] string company_Contact_Email_Address, [Parameter(Name = "Company_Invoice_Name", DbType = "VarChar(50)")] string company_Invoice_Name, [Parameter(Name = "Company_Invoice_Address", DbType = "NText")] string company_Invoice_Address, [Parameter(Name = "Company_Invoice_Postcode", DbType = "VarChar(15)")] string company_Invoice_Postcode)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), company_ID, company_Name, company_Switchboard_Phone_No, company_Address_1, company_Address_2, company_Address_3, company_Address_4, company_Address_5, company_Postcode, company_Contact_Name, company_Contact_Phone_No, company_Contact_Email_Address, company_Invoice_Name, company_Invoice_Address, company_Invoice_Postcode);
            return ((ISingleResult<usp_ecUpdateCompanyDetailsResult>)(result.ReturnValue));
        }



        [Function(Name = "dbo.usp_ecUpdateCompanyAdminDefaults")]
        public ISingleResult<usp_ecUpdateCompanyAdminDefaultsResult> UpdateCompanyAdminDefaults([Parameter(Name = "Company_ID", DbType = "Int")] System.Nullable<int> company_ID, [Parameter(DbType = "Bit")] System.Nullable<bool> bAccess_Key_ID, [Parameter(DbType = "Bit")] System.Nullable<bool> bCompany_Jackpot, [Parameter(DbType = "Bit")] System.Nullable<bool> bCompany_Percentage_Payout, [Parameter(DbType = "Bit")] System.Nullable<bool> bCompany_Price_Per_Play, [Parameter(DbType = "Bit")] System.Nullable<bool> bStaff_ID, [Parameter(DbType = "Bit")] System.Nullable<bool> bTerms_Group_ID, [Parameter(Name = "Value", DbType = "BigInt")] System.Nullable<long> value, [Parameter(Name = "CascadeType", DbType = "Int")] System.Nullable<int> cascadeType, [Parameter(Name = "IsDefault", DbType = "Bit")] System.Nullable<bool> isDefault, [Parameter(Name = "Audit_User_ID", DbType = "Int")] System.Nullable<int> audit_User_ID, [Parameter(Name = "Audit_User_Name", DbType = "VarChar(200)")] string audit_User_Name, [Parameter(Name = "AuditOperationType", DbType = "VarChar(100)")] string auditOperationType)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), company_ID, bAccess_Key_ID, bCompany_Jackpot, bCompany_Percentage_Payout, bCompany_Price_Per_Play, bStaff_ID, bTerms_Group_ID, value, cascadeType, isDefault, audit_User_ID, audit_User_Name, auditOperationType);
          return ((ISingleResult<usp_ecUpdateCompanyAdminDefaultsResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetCompanyDetailsForAdmin")]
        public ISingleResult<rsp_GetCompanyDetailsResult> rsp_GetCompanyDetails([Parameter(Name = "SecurityUserID", DbType = "Int")] System.Nullable<int> SecurityUserID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())),SecurityUserID);
            return ((ISingleResult<rsp_GetCompanyDetailsResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetSubCompanyDetailsForAdmin")]
        public ISingleResult<rsp_GetSubCompanyDetailsResult> rsp_GetSubCompanyDetails([Parameter(Name = "company",DbType = "Int")] System.Nullable<int> company,[Parameter(Name = "SecurityUserID",DbType = "Int")] System.Nullable<int> SecurityUserID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())) , company,SecurityUserID);
            return ((ISingleResult<rsp_GetSubCompanyDetailsResult>)(result.ReturnValue));
        }
    }
    /// <summary>
    /// 
    /// </summary>
    #region "ucAdminCompany.cs'
    public partial class usp_ecUpdateCompanyDetailsResult
    {

        private System.Nullable<int> _CompanyID;

        public usp_ecUpdateCompanyDetailsResult()
        {
        }

        [Column(Storage = "_CompanyID", DbType = "Int")]
        public System.Nullable<int> CompanyID
        {
            get
            {
                return this._CompanyID;
            }
            set
            {
                if ((this._CompanyID != value))
                {
                    this._CompanyID = value;
                }
            }
        }
    }
    public partial class TermsResult
    {

        private int _Terms_Group_ID;

        private string _Terms_Group_Name;

        public TermsResult()
        {
        }

        [Column(Storage = "_Terms_Group_ID", DbType = "Int NOT NULL")]
        public int Terms_Group_ID
        {
            get
            {
                return this._Terms_Group_ID;
            }
            set
            {
                if ((this._Terms_Group_ID != value))
                {
                    this._Terms_Group_ID = value;
                }
            }
        }

        [Column(Storage = "_Terms_Group_Name", DbType = "VarChar(50)")]
        public string Terms_Group_Name
        {
            get
            {
                return this._Terms_Group_Name;
            }
            set
            {
                if ((this._Terms_Group_Name != value))
                {
                    this._Terms_Group_Name = value;
                }
            }
        }
    }
    public partial class AccessKeyResult
    {

        private int _Access_Key_ID;

        private string _Access_Key_Name;

        private string _Access_Key_Ref;

        private string _Access_Key_Manufacturer;

        private string _Access_Key_Type;

        public AccessKeyResult()
        {
        }

        [Column(Storage = "_Access_Key_ID", DbType = "Int NOT NULL")]
        public int Access_Key_ID
        {
            get
            {
                return this._Access_Key_ID;
            }
            set
            {
                if ((this._Access_Key_ID != value))
                {
                    this._Access_Key_ID = value;
                }
            }
        }

        [Column(Storage = "_Access_Key_Name", DbType = "VarChar(50)")]
        public string Access_Key_Name
        {
            get
            {
                return this._Access_Key_Name;
            }
            set
            {
                if ((this._Access_Key_Name != value))
                {
                    this._Access_Key_Name = value;
                }
            }
        }

        [Column(Storage = "_Access_Key_Ref", DbType = "VarChar(50)")]
        public string Access_Key_Ref
        {
            get
            {
                return this._Access_Key_Ref;
            }
            set
            {
                if ((this._Access_Key_Ref != value))
                {
                    this._Access_Key_Ref = value;
                }
            }
        }

        [Column(Storage = "_Access_Key_Manufacturer", DbType = "VarChar(50)")]
        public string Access_Key_Manufacturer
        {
            get
            {
                return this._Access_Key_Manufacturer;
            }
            set
            {
                if ((this._Access_Key_Manufacturer != value))
                {
                    this._Access_Key_Manufacturer = value;
                }
            }
        }

        [Column(Storage = "_Access_Key_Type", DbType = "VarChar(50)")]
        public string Access_Key_Type
        {
            get
            {
                return this._Access_Key_Type;
            }
            set
            {
                if ((this._Access_Key_Type != value))
                {
                    this._Access_Key_Type = value;
                }
            }
        }
    }
    public partial class StaffResult
    {

        private int _Staff_ID;

        private string _Staff_Last_Name;

        private string _Staff_First_Name;

        public StaffResult()
        {
        }

        [Column(Storage = "_Staff_ID", DbType = "Int NOT NULL")]
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

        [Column(Storage = "_Staff_Last_Name", DbType = "VarChar(50)")]
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

        [Column(Storage = "_Staff_First_Name", DbType = "VarChar(50)")]
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
    }
    public partial class CompanyResult
    {

        private string _Company_Name;

        private string _Company_Switchboard_Phone_No;

        private string _Company_Address_1;

        private string _Company_Address_2;

        private string _Company_Address_3;

        private string _Company_Address_4;

        private string _Company_Address_5;

        private string _Company_Postcode;

        private string _Company_Contact_Name;

        private string _Company_Contact_Phone_No;

        private string _Company_Contact_Email_Address;

        private string _Company_Price_Per_Play;

        private string _Company_Jackpot;

        private string _Company_Percentage_Payout;

        private System.Nullable<int> _Terms_Group_ID;

        private System.Nullable<int> _Access_Key_ID;

        private System.Nullable<int> _Staff_ID;

        private string _Company_Invoice_Name;

        private string _Company_Invoice_Address;

        private string _Company_Invoice_Postcode;

        public CompanyResult()
        {
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

        [Column(Storage = "_Company_Switchboard_Phone_No", DbType = "VarChar(15)")]
        public string Company_Switchboard_Phone_No
        {
            get
            {
                return this._Company_Switchboard_Phone_No;
            }
            set
            {
                if ((this._Company_Switchboard_Phone_No != value))
                {
                    this._Company_Switchboard_Phone_No = value;
                }
            }
        }

        [Column(Storage = "_Company_Address_1", DbType = "VarChar(50)")]
        public string Company_Address_1
        {
            get
            {
                return this._Company_Address_1;
            }
            set
            {
                if ((this._Company_Address_1 != value))
                {
                    this._Company_Address_1 = value;
                }
            }
        }

        [Column(Storage = "_Company_Address_2", DbType = "VarChar(50)")]
        public string Company_Address_2
        {
            get
            {
                return this._Company_Address_2;
            }
            set
            {
                if ((this._Company_Address_2 != value))
                {
                    this._Company_Address_2 = value;
                }
            }
        }

        [Column(Storage = "_Company_Address_3", DbType = "VarChar(50)")]
        public string Company_Address_3
        {
            get
            {
                return this._Company_Address_3;
            }
            set
            {
                if ((this._Company_Address_3 != value))
                {
                    this._Company_Address_3 = value;
                }
            }
        }

        [Column(Storage = "_Company_Address_4", DbType = "VarChar(50)")]
        public string Company_Address_4
        {
            get
            {
                return this._Company_Address_4;
            }
            set
            {
                if ((this._Company_Address_4 != value))
                {
                    this._Company_Address_4 = value;
                }
            }
        }

        [Column(Storage = "_Company_Address_5", DbType = "VarChar(50)")]
        public string Company_Address_5
        {
            get
            {
                return this._Company_Address_5;
            }
            set
            {
                if ((this._Company_Address_5 != value))
                {
                    this._Company_Address_5 = value;
                }
            }
        }

        [Column(Storage = "_Company_Postcode", DbType = "VarChar(10)")]
        public string Company_Postcode
        {
            get
            {
                return this._Company_Postcode;
            }
            set
            {
                if ((this._Company_Postcode != value))
                {
                    this._Company_Postcode = value;
                }
            }
        }

        [Column(Storage = "_Company_Contact_Name", DbType = "VarChar(50)")]
        public string Company_Contact_Name
        {
            get
            {
                return this._Company_Contact_Name;
            }
            set
            {
                if ((this._Company_Contact_Name != value))
                {
                    this._Company_Contact_Name = value;
                }
            }
        }

        [Column(Storage = "_Company_Contact_Phone_No", DbType = "VarChar(15)")]
        public string Company_Contact_Phone_No
        {
            get
            {
                return this._Company_Contact_Phone_No;
            }
            set
            {
                if ((this._Company_Contact_Phone_No != value))
                {
                    this._Company_Contact_Phone_No = value;
                }
            }
        }

        [Column(Storage = "_Company_Contact_Email_Address", DbType = "VarChar(50)")]
        public string Company_Contact_Email_Address
        {
            get
            {
                return this._Company_Contact_Email_Address;
            }
            set
            {
                if ((this._Company_Contact_Email_Address != value))
                {
                    this._Company_Contact_Email_Address = value;
                }
            }
        }

        [Column(Storage = "_Company_Price_Per_Play", DbType = "VarChar(50)")]
        public string Company_Price_Per_Play
        {
            get
            {
                return this._Company_Price_Per_Play;
            }
            set
            {
                if ((this._Company_Price_Per_Play != value))
                {
                    this._Company_Price_Per_Play = value;
                }
            }
        }

        [Column(Storage = "_Company_Jackpot", DbType = "VarChar(10)")]
        public string Company_Jackpot
        {
            get
            {
                return this._Company_Jackpot;
            }
            set
            {
                if ((this._Company_Jackpot != value))
                {
                    this._Company_Jackpot = value;
                }
            }
        }

        [Column(Storage = "_Company_Percentage_Payout", DbType = "VarChar(50)")]
        public string Company_Percentage_Payout
        {
            get
            {
                return this._Company_Percentage_Payout;
            }
            set
            {
                if ((this._Company_Percentage_Payout != value))
                {
                    this._Company_Percentage_Payout = value;
                }
            }
        }

        [Column(Storage = "_Terms_Group_ID", DbType = "Int")]
        public System.Nullable<int> Terms_Group_ID
        {
            get
            {
                return this._Terms_Group_ID;
            }
            set
            {
                if ((this._Terms_Group_ID != value))
                {
                    this._Terms_Group_ID = value;
                }
            }
        }

        [Column(Storage = "_Access_Key_ID", DbType = "Int")]
        public System.Nullable<int> Access_Key_ID
        {
            get
            {
                return this._Access_Key_ID;
            }
            set
            {
                if ((this._Access_Key_ID != value))
                {
                    this._Access_Key_ID = value;
                }
            }
        }

        [Column(Storage = "_Staff_ID", DbType = "Int")]
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

        [Column(Storage = "_Company_Invoice_Name", DbType = "VarChar(50)")]
        public string Company_Invoice_Name
        {
            get
            {
                return this._Company_Invoice_Name;
            }
            set
            {
                if ((this._Company_Invoice_Name != value))
                {
                    this._Company_Invoice_Name = value;
                }
            }
        }

        [Column(Storage = "_Company_Invoice_Address", DbType = "NText", UpdateCheck = UpdateCheck.Never)]
        public string Company_Invoice_Address
        {
            get
            {
                return this._Company_Invoice_Address;
            }
            set
            {
                if ((this._Company_Invoice_Address != value))
                {
                    this._Company_Invoice_Address = value;
                }
            }
        }

        [Column(Storage = "_Company_Invoice_Postcode", DbType = "VarChar(15)")]
        public string Company_Invoice_Postcode
        {
            get
            {
                return this._Company_Invoice_Postcode;
            }
            set
            {
                if ((this._Company_Invoice_Postcode != value))
                {
                    this._Company_Invoice_Postcode = value;
                }
            }
        }
    }
    public partial class usp_ecUpdateCompanyAdminDefaultsResult
    {

        private System.Nullable<int> _ID;

        private string _FieldName;
        private string _Type;

        public usp_ecUpdateCompanyAdminDefaultsResult()
        {
        }

        [Column(Storage = "_ID", DbType = "Int")]
        public System.Nullable<int> ID
        {
            get
            {
                return this._ID;
            }
            set
            {
                if ((this._ID != value))
                {
                    this._ID = value;
                }
            }
        }

        [Column(Storage = "_FieldName", DbType = "VarChar(100)")]
        public string FieldName
        {
            get
            {
                return this._FieldName;
            }
            set
            {
                if ((this._FieldName != value))
                {
                    this._FieldName = value;
                }
            }
        }
        
        [Column(Storage = "_Type", DbType = "VarChar(30)")]
        public string Type
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
    }

    //public partial class ecrspGetCompanySubCompanyResult
    //{

    //    private int _Sub_Company_ID;

    //    public ecrspGetCompanySubCompanyResult()
    //    {
    //    }

    //    [Column(Storage = "_Sub_Company_ID", DbType = "Int NOT NULL")]
    //    public int Sub_Company_ID
    //    {
    //        get
    //        {
    //            return this._Sub_Company_ID;
    //        }
    //        set
    //        {
    //            if ((this._Sub_Company_ID != value))
    //            {
    //                this._Sub_Company_ID = value;
    //            }
    //        }
    //    }
    //}
    //public partial class ecrspGetCompanysiteResult
    //{

    //    private int _site_id;

    //    public ecrspGetCompanysiteResult()
    //    {
    //    }

    //    [Column(Storage = "_site_id", DbType = "Int NOT NULL")]
    //    public int site_id
    //    {
    //        get
    //        {
    //            return this._site_id;
    //        }
    //        set
    //        {
    //            if ((this._site_id != value))
    //            {
    //                this._site_id = value;
    //            }
    //        }
    //    }
    //}
    //public partial class ecrspGetCompanyBarposResult
    //{

    //    private int _Bar_Position_ID;

    //    public ecrspGetCompanyBarposResult()
    //    {
    //    }

    //    [Column(Storage = "_Bar_Position_ID", DbType = "Int NOT NULL")]
    //    public int Bar_Position_ID
    //    {
    //        get
    //        {
    //            return this._Bar_Position_ID;
    //        }
    //        set
    //        {
    //            if ((this._Bar_Position_ID != value))
    //            {
    //                this._Bar_Position_ID = value;
    //            }
    //        }
    //    }
    //}



    #endregion

    public partial class rsp_GetCompanyDetailsResult
    {

        private int _Company_ID;

        private string _Company_Name;

        public rsp_GetCompanyDetailsResult()
        {
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
    }

    public partial class rsp_GetSubCompanyDetailsResult
    {

        private int _Sub_Company_ID;

        private string _Sub_Company_Name;

        public rsp_GetSubCompanyDetailsResult()
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
