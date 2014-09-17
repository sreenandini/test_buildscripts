using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Reflection;
namespace BMC.EnterpriseDataAccess
{
    public partial class EnterpriseDataContext
    {
        [Function(Name = "dbo.rsp_ecGetSubCompanyAdminDetails")]
        [ResultType(typeof(SubCompanyResult))]
        [ResultType(typeof(SubCompanyRegionResult))]
        [ResultType(typeof(CompanyDefaultsResult))]
        [ResultType(typeof(TermsResult))]
        [ResultType(typeof(SubCompanyAccessInfoResult))]
        [ResultType(typeof(SubCompanyCompanyInfoResult))]
        [ResultType(typeof(SubCompanyModelInfoResult))]
        [ResultType(typeof(SubCompanyHourInfoResult))]
        [ResultType(typeof(SubCompanyStaffInfoResult))]
        [ResultType(typeof(SubCompanyJackpotInfoResult))]
        public IMultipleResults GetSubCompanyAdminDetails([Parameter(Name = "SubCompanyID", DbType = "Int")] int subCompanyID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), subCompanyID);
            return ((IMultipleResults)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_ecGetSubCompanyAreaDetails")]
        public ISingleResult<rsp_ecGetSubCompanyAreaDetailsResult> GetSubCompanyAreaDetails([Parameter(Name = "SubCompanyRegionID", DbType = "Int")] System.Nullable<int> subCompanyRegionID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), subCompanyRegionID);
            return ((ISingleResult<rsp_ecGetSubCompanyAreaDetailsResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_ecGetSubCompanyDistrictDetails")]
        public ISingleResult<rsp_ecGetSubCompanyDistrictDetailsResult> GetSubCompanyDistrictDetails([Parameter(Name = "SubCompanyAreaID", DbType = "Int")] System.Nullable<int> subCompanyAreaID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), subCompanyAreaID);
            return ((ISingleResult<rsp_ecGetSubCompanyDistrictDetailsResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_ecGetSubCompanyRegionDetails")]
        public ISingleResult<SubCompanyRegionResult> GetSubCompanyRegionDetails([Parameter(Name = "SubCompanyID", DbType = "Int")] System.Nullable<int> subCompanyID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), subCompanyID);
            return ((ISingleResult<SubCompanyRegionResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.usp_ecUpdateSubCompanyDistrict")]
        public int UpdateSubCompanyDistrict([Parameter(Name = "SubCompanyDistrictID", DbType = "Int")] System.Nullable<int> subCompanyDistrictID, [Parameter(Name = "SubCompanyDistrictName", DbType = "VarChar(50)")] string subCompanyDistrictName, [Parameter(Name = "SubCompanyDistrictDescription", DbType = "VarChar(50)")] string subCompanyDistrictDescription, [Parameter(Name = "Staff_ID", DbType = "Int")] System.Nullable<int> staff_ID, [Parameter(Name = "SubCompanyAreaID", DbType = "Int")] System.Nullable<int> subCompanyAreaID, [Parameter(Name = "ErrorCode", DbType = "Int")] ref System.Nullable<int> errorCode, [Parameter(Name = "ErrorMessage", DbType = "VarChar(100)")] ref string errorMessage)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), subCompanyDistrictID, subCompanyDistrictName, subCompanyDistrictDescription, staff_ID, subCompanyAreaID, errorCode, errorMessage);
            errorCode = ((System.Nullable<int>)(result.GetParameterValue(5)));
            errorMessage = ((string)(result.GetParameterValue(6)));
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.usp_ecUpdateSubCompanyArea")]
        public int UpdateSubCompanyArea([Parameter(Name = "SubCompanyAreaID", DbType = "Int")] System.Nullable<int> subCompanyAreaID, [Parameter(Name = "SubCompanyAreaName", DbType = "VarChar(50)")] string subCompanyAreaName, [Parameter(Name = "SubCompanyAreaDescription", DbType = "VarChar(50)")] string subCompanyAreaDescription, [Parameter(Name = "Staff_ID", DbType = "Int")] System.Nullable<int> staff_ID, [Parameter(Name = "SubCompanyRegionID", DbType = "Int")] System.Nullable<int> subCompanyRegionID, [Parameter(Name = "ErrorCode", DbType = "Int")] ref System.Nullable<int> errorCode, [Parameter(Name = "ErrorMessage", DbType = "VarChar(100)")] ref string errorMessage)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), subCompanyAreaID, subCompanyAreaName, subCompanyAreaDescription, staff_ID, subCompanyRegionID, errorCode, errorMessage);
            errorCode = ((System.Nullable<int>)(result.GetParameterValue(5)));
            errorMessage = ((string)(result.GetParameterValue(6)));
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.usp_ecUpdateSubCompanyRegion")]
        public int UpdateSubCompanyRegion([Parameter(Name = "SubCompanyRegionID", DbType = "Int")] System.Nullable<int> subCompanyRegionID, [Parameter(Name = "SubCompanyRegionName", DbType = "VarChar(50)")] string subCompanyRegionName, [Parameter(Name = "SubCompanyRegionDescription", DbType = "VarChar(50)")] string subCompanyRegionDescription, [Parameter(Name = "Staff_ID", DbType = "Int")] System.Nullable<int> staff_ID, [Parameter(Name = "SubCompanyID", DbType = "Int")] System.Nullable<int> subCompanyID, [Parameter(Name = "CompanyID", DbType = "Int")] System.Nullable<int> companyID, [Parameter(Name = "ErrorCode", DbType = "Int")] ref System.Nullable<int> errorCode, [Parameter(Name = "ErrorMessage", DbType = "VarChar(100)")] ref string errorMessage)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), subCompanyRegionID, subCompanyRegionName, subCompanyRegionDescription, staff_ID, subCompanyID, companyID, errorCode, errorMessage);
            errorCode = ((System.Nullable<int>)(result.GetParameterValue(6)));
            errorMessage = ((string)(result.GetParameterValue(7)));
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.usp_ecUpdateSubCompany")]
        public int UpdateSubCompany(
                    [Parameter(Name = "Sub_Company_ID", DbType = "Int")] ref System.Nullable<int> sub_Company_ID,
                    [Parameter(Name = "Sub_Company_Name", DbType = "VarChar(50)")] string sub_Company_Name,
                    [Parameter(Name = "Company_ID", DbType = "Int")] System.Nullable<int> company_ID,
                    [Parameter(Name = "Sub_Company_Switchboard_Phone_No", DbType = "VarChar(15)")] string sub_Company_Switchboard_Phone_No,
                    [Parameter(Name = "Sub_Company_Address_1", DbType = "VarChar(50)")] string sub_Company_Address_1,
                    [Parameter(Name = "Sub_Company_Address_2", DbType = "VarChar(50)")] string sub_Company_Address_2,
                    [Parameter(Name = "Sub_Company_Address_3", DbType = "VarChar(50)")] string sub_Company_Address_3,
                    [Parameter(Name = "Sub_Company_Address_4", DbType = "VarChar(50)")] string sub_Company_Address_4,
                    [Parameter(Name = "Sub_Company_Address_5", DbType = "VarChar(50)")] string sub_Company_Address_5,
                    [Parameter(Name = "Sub_Company_Postcode", DbType = "VarChar(15)")] string sub_Company_Postcode,
                    [Parameter(Name = "Sub_Company_ANA_Number", DbType = "VarChar(20)")] string sub_Company_ANA_Number,
                    [Parameter(Name = "Sub_Company_Income_Ledger_Code", DbType = "VarChar(20)")] string sub_Company_Income_Ledger_Code,
                    [Parameter(Name = "Sage_Account_Ref", DbType = "VarChar(50)")] string sage_Account_Ref,
                    [Parameter(Name = "Company_Model_Set_ID", DbType = "Int")] System.Nullable<int> company_Model_Set_ID,
                    [Parameter(Name = "Sub_Company_Trade_Type", DbType = "VarChar(50)")] string sub_Company_Trade_Type,
                    [Parameter(Name = "Sub_Company_Contact_Name", DbType = "VarChar(50)")] string sub_Company_Contact_Name,
                    [Parameter(Name = "Sub_Company_Contact_Phone_No", DbType = "VarChar(15)")] string sub_Company_Contact_Phone_No,
                    [Parameter(Name = "Sub_Company_Contact_Email_Address", DbType = "VarChar(100)")] string sub_Company_Contact_Email_Address,
                    [Parameter(Name = "Sub_Company_Use_Split_Rents", DbType = "Bit")] System.Nullable<bool> sub_Company_Use_Split_Rents,
                    [Parameter(Name = "Sub_Company_Price_Per_Play_Default", DbType = "Bit")] System.Nullable<bool> sub_Company_Price_Per_Play_Default,
                    [Parameter(Name = "Sub_Company_Price_Per_Play", DbType = "VarChar(50)")] string sub_Company_Price_Per_Play,
                    [Parameter(Name = "Sub_Company_Jackpot_Default", DbType = "Bit")] System.Nullable<bool> sub_Company_Jackpot_Default,
                    [Parameter(Name = "Sub_Company_Jackpot", DbType = "VarChar(50)")] string sub_Company_Jackpot,
                    [Parameter(Name = "Sub_Company_Percentage_Payout_Default", DbType = "Bit")] System.Nullable<bool> sub_Company_Percentage_Payout_Default,
                    [Parameter(Name = "Sub_Company_Percentage_Payout", DbType = "VarChar(50)")] string sub_Company_Percentage_Payout,
                    [Parameter(Name = "Terms_Group_ID_Default", DbType = "Bit")] System.Nullable<bool> terms_Group_ID_Default,
                    [Parameter(Name = "Terms_Group_ID", DbType = "Int")] System.Nullable<int> terms_Group_ID,
                    [Parameter(Name = "Access_Key_ID_Default", DbType = "Bit")] System.Nullable<bool> access_Key_ID_Default,
                    [Parameter(Name = "Access_Key_ID", DbType = "Int")] System.Nullable<int> access_Key_ID,
                    [Parameter(Name = "Staff_ID_Default", DbType = "Bit")] System.Nullable<bool> staff_ID_Default,
                    [Parameter(Name = "Staff_ID", DbType = "Int")] System.Nullable<int> staff_ID,
                    [Parameter(Name = "Sub_Company_Default_Opening_Hours_ID", DbType = "Int")] System.Nullable<int> sub_Company_Default_Opening_Hours_ID,
                    [Parameter(Name = "Sub_Company_Invoice_Name", DbType = "VarChar(50)")] string sub_Company_Invoice_Name,
                    [Parameter(Name = "Sub_Company_Invoice_Address", DbType = "NText")] string sub_Company_Invoice_Address,
                    [Parameter(Name = "Sub_Company_Invoice_Postcode", DbType = "VarChar(15)")] string sub_Company_Invoice_Postcode,
                    [Parameter(Name = "Sub_Company_Account_Name", DbType = "VarChar(32)")] string sub_Company_Account_Name,
                    [Parameter(Name = "Sub_Company_Account_No", DbType = "VarChar(12)")] string sub_Company_Account_No,
                    [Parameter(Name = "Sub_Company_Sort_Code", DbType = "VarChar(8)")] string sub_Company_Sort_Code,
                    [Parameter(Name = "UpdateAllSites", DbType = "Bit")] System.Nullable<bool> updateAllSites)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), sub_Company_ID, sub_Company_Name, company_ID, sub_Company_Switchboard_Phone_No, sub_Company_Address_1, sub_Company_Address_2, sub_Company_Address_3, sub_Company_Address_4, sub_Company_Address_5, sub_Company_Postcode, sub_Company_ANA_Number, sub_Company_Income_Ledger_Code, sage_Account_Ref, company_Model_Set_ID, sub_Company_Trade_Type, sub_Company_Contact_Name, sub_Company_Contact_Phone_No, sub_Company_Contact_Email_Address, sub_Company_Use_Split_Rents, sub_Company_Price_Per_Play_Default, sub_Company_Price_Per_Play, sub_Company_Jackpot_Default, sub_Company_Jackpot, sub_Company_Percentage_Payout_Default, sub_Company_Percentage_Payout, terms_Group_ID_Default, terms_Group_ID, access_Key_ID_Default, access_Key_ID, staff_ID_Default, staff_ID, sub_Company_Default_Opening_Hours_ID, sub_Company_Invoice_Name, sub_Company_Invoice_Address, sub_Company_Invoice_Postcode, sub_Company_Account_Name, sub_Company_Account_No, sub_Company_Sort_Code, updateAllSites);
            sub_Company_ID = ((System.Nullable<int>)(result.GetParameterValue(0)));
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.usp_ecUpdateSubCompanyAdminDefaults")]
        public ISingleResult<usp_ecUpdateSubCompanyAdminDefaultsResult> UpdateSubCompanyAdminDefaults(
                    [Parameter(Name = "Company_ID", DbType = "Int")] System.Nullable<int> company_ID,
                    [Parameter(DbType = "Bit")] System.Nullable<bool> bAccess_Key_ID,
                    [Parameter(DbType = "Bit")] System.Nullable<bool> bCompany_Jackpot,
                    [Parameter(DbType = "Bit")] System.Nullable<bool> bCompany_Percentage_Payout,
                    [Parameter(DbType = "Bit")] System.Nullable<bool> bCompany_Price_Per_Play,
                    [Parameter(DbType = "Bit")] System.Nullable<bool> bStaff_ID,
                    [Parameter(DbType = "Bit")] System.Nullable<bool> bTerms_Group_ID,
                    [Parameter(Name = "Value", DbType = "BigInt")] System.Nullable<long> value,
                    [Parameter(Name = "CascadeType", DbType = "Int")] System.Nullable<int> cascadeType,
                    [Parameter(Name = "Level", DbType = "Int")] System.Nullable<int> level,
                    [Parameter(Name = "IsDefault", DbType = "Bit")] System.Nullable<bool> isDefault,
                    [Parameter(Name = "Audit_User_ID", DbType = "Int")] System.Nullable<int> audit_User_ID,
                    [Parameter(Name = "Audit_User_Name", DbType = "VarChar(50)")] string audit_User_Name,
                    [Parameter(Name = "AuditOperationType", DbType = "VarChar(100)")] string auditOperationType,
                    [Parameter(Name = "Audit_ModuleName", DbType = "VarChar(50)")] string audit_ModuleName,
                    [Parameter(Name = "SubCompanyID", DbType = "Int")] System.Nullable<int> subCompanyID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), company_ID, bAccess_Key_ID, bCompany_Jackpot, bCompany_Percentage_Payout, bCompany_Price_Per_Play, bStaff_ID, bTerms_Group_ID, value, cascadeType, level, isDefault, audit_User_ID, audit_User_Name, auditOperationType, audit_ModuleName, subCompanyID);
            return ((ISingleResult<usp_ecUpdateSubCompanyAdminDefaultsResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.IsSubCompanyExist", IsComposable = true)]
        public System.Nullable<bool> IsSubCompanyExist([Parameter(Name = "SubCompanyName", DbType = "VarChar(50)")] string subCompanyName, [Parameter(Name = "CompanyID", DbType = "Int")] System.Nullable<int> companyID, [Parameter(Name = "SubCompanyID", DbType = "Int")] System.Nullable<int> subCompanyID)
        {
            return ((System.Nullable<bool>)(this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), subCompanyName, companyID, subCompanyID).ReturnValue));
        }
    }

    public partial class SubCompanyResult
    {

        private string _Sub_Company_Name;

        private System.Nullable<int> _Company_ID;

        private string _Sub_Company_Switchboard_Phone_No;

        private string _Sub_Company_Address_1;

        private string _Sub_Company_Address_2;

        private string _Sub_Company_Address_3;

        private string _Sub_Company_Address_4;

        private string _Sub_Company_Address_5;

        private string _Sub_Company_Postcode;

        private string _Sub_Company_ANA_Number;

        private string _Sub_Company_Income_Ledger_Code;

        private string _Sage_Account_Ref;

        private System.Nullable<int> _Company_Model_Set_ID;

        private string _Sub_Company_Trade_Type;

        private string _Sub_Company_Trade_Type1;

        private string _Sub_Company_Contact_Name;

        private string _Sub_Company_Contact_Phone_No;

        private string _Sub_Company_Contact_Email_Address;

        private System.Nullable<bool> _Sub_Company_Use_Split_Rents;

        private System.Nullable<bool> _Sub_Company_Price_Per_Play_Default;

        private string _Sub_Company_Price_Per_Play;

        private System.Nullable<bool> _Sub_Company_Jackpot_Default;

        private string _Sub_Company_Jackpot;

        private System.Nullable<bool> _Sub_Company_Percentage_Payout_Default;

        private string _Sub_Company_Percentage_Payout;

        private System.Nullable<bool> _Terms_Group_ID_Default;

        private System.Nullable<int> _Terms_Group_ID;

        private System.Nullable<bool> _Access_Key_ID_Default;

        private System.Nullable<int> _Access_Key_ID;

        private System.Nullable<bool> _Staff_ID_Default;

        private System.Nullable<int> _Staff_ID;

        private System.Nullable<int> _Sub_Company_Default_Opening_Hours_ID;

        private string _Sub_Company_Invoice_Name;

        private string _Sub_Company_Invoice_Address;

        private string _Sub_Company_Invoice_Postcode;

        private string _Sub_Company_Account_Name;

        private string _Sub_Company_Account_No;

        private string _Sub_Company_Sort_Code;

        public SubCompanyResult()
        {
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

        [Column(Storage = "_Company_ID", DbType = "Int")]
        public System.Nullable<int> Company_ID
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

        [Column(Storage = "_Sub_Company_Switchboard_Phone_No", DbType = "VarChar(15)")]
        public string Sub_Company_Switchboard_Phone_No
        {
            get
            {
                return this._Sub_Company_Switchboard_Phone_No;
            }
            set
            {
                if ((this._Sub_Company_Switchboard_Phone_No != value))
                {
                    this._Sub_Company_Switchboard_Phone_No = value;
                }
            }
        }

        [Column(Storage = "_Sub_Company_Address_1", DbType = "VarChar(50)")]
        public string Sub_Company_Address_1
        {
            get
            {
                return this._Sub_Company_Address_1;
            }
            set
            {
                if ((this._Sub_Company_Address_1 != value))
                {
                    this._Sub_Company_Address_1 = value;
                }
            }
        }

        [Column(Storage = "_Sub_Company_Address_2", DbType = "VarChar(50)")]
        public string Sub_Company_Address_2
        {
            get
            {
                return this._Sub_Company_Address_2;
            }
            set
            {
                if ((this._Sub_Company_Address_2 != value))
                {
                    this._Sub_Company_Address_2 = value;
                }
            }
        }

        [Column(Storage = "_Sub_Company_Address_3", DbType = "VarChar(50)")]
        public string Sub_Company_Address_3
        {
            get
            {
                return this._Sub_Company_Address_3;
            }
            set
            {
                if ((this._Sub_Company_Address_3 != value))
                {
                    this._Sub_Company_Address_3 = value;
                }
            }
        }

        [Column(Storage = "_Sub_Company_Address_4", DbType = "VarChar(50)")]
        public string Sub_Company_Address_4
        {
            get
            {
                return this._Sub_Company_Address_4;
            }
            set
            {
                if ((this._Sub_Company_Address_4 != value))
                {
                    this._Sub_Company_Address_4 = value;
                }
            }
        }

        [Column(Storage = "_Sub_Company_Address_5", DbType = "VarChar(50)")]
        public string Sub_Company_Address_5
        {
            get
            {
                return this._Sub_Company_Address_5;
            }
            set
            {
                if ((this._Sub_Company_Address_5 != value))
                {
                    this._Sub_Company_Address_5 = value;
                }
            }
        }

        [Column(Storage = "_Sub_Company_Postcode", DbType = "VarChar(15)")]
        public string Sub_Company_Postcode
        {
            get
            {
                return this._Sub_Company_Postcode;
            }
            set
            {
                if ((this._Sub_Company_Postcode != value))
                {
                    this._Sub_Company_Postcode = value;
                }
            }
        }

        [Column(Storage = "_Sub_Company_ANA_Number", DbType = "VarChar(20)")]
        public string Sub_Company_ANA_Number
        {
            get
            {
                return this._Sub_Company_ANA_Number;
            }
            set
            {
                if ((this._Sub_Company_ANA_Number != value))
                {
                    this._Sub_Company_ANA_Number = value;
                }
            }
        }

        [Column(Storage = "_Sub_Company_Income_Ledger_Code", DbType = "VarChar(20)")]
        public string Sub_Company_Income_Ledger_Code
        {
            get
            {
                return this._Sub_Company_Income_Ledger_Code;
            }
            set
            {
                if ((this._Sub_Company_Income_Ledger_Code != value))
                {
                    this._Sub_Company_Income_Ledger_Code = value;
                }
            }
        }

        [Column(Storage = "_Sage_Account_Ref", DbType = "VarChar(50)")]
        public string Sage_Account_Ref
        {
            get
            {
                return this._Sage_Account_Ref;
            }
            set
            {
                if ((this._Sage_Account_Ref != value))
                {
                    this._Sage_Account_Ref = value;
                }
            }
        }

        [Column(Storage = "_Company_Model_Set_ID", DbType = "Int")]
        public System.Nullable<int> Company_Model_Set_ID
        {
            get
            {
                return this._Company_Model_Set_ID;
            }
            set
            {
                if ((this._Company_Model_Set_ID != value))
                {
                    this._Company_Model_Set_ID = value;
                }
            }
        }

        [Column(Storage = "_Sub_Company_Trade_Type", DbType = "VarChar(50)")]
        public string Sub_Company_Trade_Type
        {
            get
            {
                return this._Sub_Company_Trade_Type;
            }
            set
            {
                if ((this._Sub_Company_Trade_Type != value))
                {
                    this._Sub_Company_Trade_Type = value;
                }
            }
        }

        [Column(Storage = "_Sub_Company_Trade_Type1", DbType = "VarChar(50)")]
        public string Sub_Company_Trade_Type1
        {
            get
            {
                return this._Sub_Company_Trade_Type1;
            }
            set
            {
                if ((this._Sub_Company_Trade_Type1 != value))
                {
                    this._Sub_Company_Trade_Type1 = value;
                }
            }
        }

        [Column(Storage = "_Sub_Company_Contact_Name", DbType = "VarChar(50)")]
        public string Sub_Company_Contact_Name
        {
            get
            {
                return this._Sub_Company_Contact_Name;
            }
            set
            {
                if ((this._Sub_Company_Contact_Name != value))
                {
                    this._Sub_Company_Contact_Name = value;
                }
            }
        }

        [Column(Storage = "_Sub_Company_Contact_Phone_No", DbType = "VarChar(15)")]
        public string Sub_Company_Contact_Phone_No
        {
            get
            {
                return this._Sub_Company_Contact_Phone_No;
            }
            set
            {
                if ((this._Sub_Company_Contact_Phone_No != value))
                {
                    this._Sub_Company_Contact_Phone_No = value;
                }
            }
        }

        [Column(Storage = "_Sub_Company_Contact_Email_Address", DbType = "VarChar(100)")]
        public string Sub_Company_Contact_Email_Address
        {
            get
            {
                return this._Sub_Company_Contact_Email_Address;
            }
            set
            {
                if ((this._Sub_Company_Contact_Email_Address != value))
                {
                    this._Sub_Company_Contact_Email_Address = value;
                }
            }
        }

        [Column(Storage = "_Sub_Company_Use_Split_Rents", DbType = "Bit")]
        public System.Nullable<bool> Sub_Company_Use_Split_Rents
        {
            get
            {
                return this._Sub_Company_Use_Split_Rents;
            }
            set
            {
                if ((this._Sub_Company_Use_Split_Rents != value))
                {
                    this._Sub_Company_Use_Split_Rents = value;
                }
            }
        }

        [Column(Storage = "_Sub_Company_Price_Per_Play_Default", DbType = "Bit")]
        public System.Nullable<bool> Sub_Company_Price_Per_Play_Default
        {
            get
            {
                return this._Sub_Company_Price_Per_Play_Default;
            }
            set
            {
                if ((this._Sub_Company_Price_Per_Play_Default != value))
                {
                    this._Sub_Company_Price_Per_Play_Default = value;
                }
            }
        }

        [Column(Storage = "_Sub_Company_Price_Per_Play", DbType = "VarChar(50)")]
        public string Sub_Company_Price_Per_Play
        {
            get
            {
                return this._Sub_Company_Price_Per_Play;
            }
            set
            {
                if ((this._Sub_Company_Price_Per_Play != value))
                {
                    this._Sub_Company_Price_Per_Play = value;
                }
            }
        }

        [Column(Storage = "_Sub_Company_Jackpot_Default", DbType = "Bit")]
        public System.Nullable<bool> Sub_Company_Jackpot_Default
        {
            get
            {
                return this._Sub_Company_Jackpot_Default;
            }
            set
            {
                if ((this._Sub_Company_Jackpot_Default != value))
                {
                    this._Sub_Company_Jackpot_Default = value;
                }
            }
        }

        [Column(Storage = "_Sub_Company_Jackpot", DbType = "VarChar(50)")]
        public string Sub_Company_Jackpot
        {
            get
            {
                return this._Sub_Company_Jackpot;
            }
            set
            {
                if ((this._Sub_Company_Jackpot != value))
                {
                    this._Sub_Company_Jackpot = value;
                }
            }
        }

        [Column(Storage = "_Sub_Company_Percentage_Payout_Default", DbType = "Bit")]
        public System.Nullable<bool> Sub_Company_Percentage_Payout_Default
        {
            get
            {
                return this._Sub_Company_Percentage_Payout_Default;
            }
            set
            {
                if ((this._Sub_Company_Percentage_Payout_Default != value))
                {
                    this._Sub_Company_Percentage_Payout_Default = value;
                }
            }
        }

        [Column(Storage = "_Sub_Company_Percentage_Payout", DbType = "VarChar(50)")]
        public string Sub_Company_Percentage_Payout
        {
            get
            {
                return this._Sub_Company_Percentage_Payout;
            }
            set
            {
                if ((this._Sub_Company_Percentage_Payout != value))
                {
                    this._Sub_Company_Percentage_Payout = value;
                }
            }
        }

        [Column(Storage = "_Terms_Group_ID_Default", DbType = "Bit")]
        public System.Nullable<bool> Terms_Group_ID_Default
        {
            get
            {
                return this._Terms_Group_ID_Default;
            }
            set
            {
                if ((this._Terms_Group_ID_Default != value))
                {
                    this._Terms_Group_ID_Default = value;
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

        [Column(Storage = "_Access_Key_ID_Default", DbType = "Bit")]
        public System.Nullable<bool> Access_Key_ID_Default
        {
            get
            {
                return this._Access_Key_ID_Default;
            }
            set
            {
                if ((this._Access_Key_ID_Default != value))
                {
                    this._Access_Key_ID_Default = value;
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

        [Column(Storage = "_Staff_ID_Default", DbType = "Bit")]
        public System.Nullable<bool> Staff_ID_Default
        {
            get
            {
                return this._Staff_ID_Default;
            }
            set
            {
                if ((this._Staff_ID_Default != value))
                {
                    this._Staff_ID_Default = value;
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

        [Column(Storage = "_Sub_Company_Default_Opening_Hours_ID", DbType = "Int")]
        public System.Nullable<int> Sub_Company_Default_Opening_Hours_ID
        {
            get
            {
                return this._Sub_Company_Default_Opening_Hours_ID;
            }
            set
            {
                if ((this._Sub_Company_Default_Opening_Hours_ID != value))
                {
                    this._Sub_Company_Default_Opening_Hours_ID = value;
                }
            }
        }

        [Column(Storage = "_Sub_Company_Invoice_Name", DbType = "VarChar(50)")]
        public string Sub_Company_Invoice_Name
        {
            get
            {
                return this._Sub_Company_Invoice_Name;
            }
            set
            {
                if ((this._Sub_Company_Invoice_Name != value))
                {
                    this._Sub_Company_Invoice_Name = value;
                }
            }
        }

        [Column(Storage = "_Sub_Company_Invoice_Address", DbType = "NText", UpdateCheck = UpdateCheck.Never)]
        public string Sub_Company_Invoice_Address
        {
            get
            {
                return this._Sub_Company_Invoice_Address;
            }
            set
            {
                if ((this._Sub_Company_Invoice_Address != value))
                {
                    this._Sub_Company_Invoice_Address = value;
                }
            }
        }

        [Column(Storage = "_Sub_Company_Invoice_Postcode", DbType = "VarChar(15)")]
        public string Sub_Company_Invoice_Postcode
        {
            get
            {
                return this._Sub_Company_Invoice_Postcode;
            }
            set
            {
                if ((this._Sub_Company_Invoice_Postcode != value))
                {
                    this._Sub_Company_Invoice_Postcode = value;
                }
            }
        }

        [Column(Storage = "_Sub_Company_Account_Name", DbType = "VarChar(32)")]
        public string Sub_Company_Account_Name
        {
            get
            {
                return this._Sub_Company_Account_Name;
            }
            set
            {
                if ((this._Sub_Company_Account_Name != value))
                {
                    this._Sub_Company_Account_Name = value;
                }
            }
        }

        [Column(Storage = "_Sub_Company_Account_No", DbType = "VarChar(12)")]
        public string Sub_Company_Account_No
        {
            get
            {
                return this._Sub_Company_Account_No;
            }
            set
            {
                if ((this._Sub_Company_Account_No != value))
                {
                    this._Sub_Company_Account_No = value;
                }
            }
        }

        [Column(Storage = "_Sub_Company_Sort_Code", DbType = "VarChar(8)")]
        public string Sub_Company_Sort_Code
        {
            get
            {
                return this._Sub_Company_Sort_Code;
            }
            set
            {
                if ((this._Sub_Company_Sort_Code != value))
                {
                    this._Sub_Company_Sort_Code = value;
                }
            }
        }
    }

    public partial class SubCompanyRegionResult
    {

        private int _Sub_Company_Region_ID;

        private string _Sub_Company_Region_Name;

        private string _Sub_Company_Region_Description;

        private System.Nullable<int> _Staff_ID;

        private string _Staff_Last_Name;

        private string _Staff_First_Name;

        public SubCompanyRegionResult()
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

        [Column(Storage = "_Sub_Company_Region_Description", DbType = "VarChar(50)")]
        public string Sub_Company_Region_Description
        {
            get
            {
                return this._Sub_Company_Region_Description;
            }
            set
            {
                if ((this._Sub_Company_Region_Description != value))
                {
                    this._Sub_Company_Region_Description = value;
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

    public partial class SubCompanyAccessInfoResult
    {

        private System.Nullable<int> _Access_Key_ID;

        private string _Access_Key_Name;

        public SubCompanyAccessInfoResult()
        {
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
    }

    public partial class SubCompanyStaffInfoResult
    {

        private string _StaffName;

        private int _Staff_ID;

        public SubCompanyStaffInfoResult()
        {
        }

        [Column(Storage = "_StaffName", DbType = "VarChar(102)")]
        public string StaffName
        {
            get
            {
                return this._StaffName;
            }
            set
            {
                if ((this._StaffName != value))
                {
                    this._StaffName = value;
                }
            }
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
    }

    public partial class SubCompanyCompanyInfoResult
    {

        private int _Company_ID;

        private string _Company_Name;

        public SubCompanyCompanyInfoResult()
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

    public partial class SubCompanyHourInfoResult
    {

        private int _Standard_Opening_Hours_ID;

        private string _Standard_Opening_Hours_Description;

        public SubCompanyHourInfoResult()
        {
        }

        [Column(Storage = "_Standard_Opening_Hours_ID", DbType = "Int NOT NULL")]
        public int Standard_Opening_Hours_ID
        {
            get
            {
                return this._Standard_Opening_Hours_ID;
            }
            set
            {
                if ((this._Standard_Opening_Hours_ID != value))
                {
                    this._Standard_Opening_Hours_ID = value;
                }
            }
        }

        [Column(Storage = "_Standard_Opening_Hours_Description", DbType = "VarChar(50)")]
        public string Standard_Opening_Hours_Description
        {
            get
            {
                return this._Standard_Opening_Hours_Description;
            }
            set
            {
                if ((this._Standard_Opening_Hours_Description != value))
                {
                    this._Standard_Opening_Hours_Description = value;
                }
            }
        }
    }

    public partial class SubCompanyJackpotInfoResult
    {

        private string _Column1;

        public SubCompanyJackpotInfoResult()
        {
        }

        [Column(Storage = "_Column1", DbType = "VarChar(10) NOT NULL", CanBeNull = false)]
        public string Column1
        {
            get
            {
                return this._Column1;
            }
            set
            {
                if ((this._Column1 != value))
                {
                    this._Column1 = value;
                }
            }
        }
    }

    public partial class SubCompanyModelInfoResult
    {

        private int _Company_Model_Set_ID;

        private string _Company_Model_Set_Description;

        public SubCompanyModelInfoResult()
        {
        }

        [Column(Storage = "_Company_Model_Set_ID", DbType = "Int NOT NULL")]
        public int Company_Model_Set_ID
        {
            get
            {
                return this._Company_Model_Set_ID;
            }
            set
            {
                if ((this._Company_Model_Set_ID != value))
                {
                    this._Company_Model_Set_ID = value;
                }
            }
        }

        [Column(Storage = "_Company_Model_Set_Description", DbType = "VarChar(50)")]
        public string Company_Model_Set_Description
        {
            get
            {
                return this._Company_Model_Set_Description;
            }
            set
            {
                if ((this._Company_Model_Set_Description != value))
                {
                    this._Company_Model_Set_Description = value;
                }
            }
        }
    }

    public partial class rsp_ecGetSubCompanyAreaDetailsResult
    {

        private int _Sub_Company_Area_ID;

        private string _Sub_Company_Area_Name;

        private string _Sub_Company_Area_Description;

        private System.Nullable<int> _Staff_ID;

        private string _Staff_Last_Name;

        private string _Staff_First_Name;

        public rsp_ecGetSubCompanyAreaDetailsResult()
        {
        }

        [Column(Storage = "_Sub_Company_Area_ID", DbType = "Int NOT NULL")]
        public int Sub_Company_Area_ID
        {
            get
            {
                return this._Sub_Company_Area_ID;
            }
            set
            {
                if ((this._Sub_Company_Area_ID != value))
                {
                    this._Sub_Company_Area_ID = value;
                }
            }
        }

        [Column(Storage = "_Sub_Company_Area_Name", DbType = "VarChar(50)")]
        public string Sub_Company_Area_Name
        {
            get
            {
                return this._Sub_Company_Area_Name;
            }
            set
            {
                if ((this._Sub_Company_Area_Name != value))
                {
                    this._Sub_Company_Area_Name = value;
                }
            }
        }

        [Column(Storage = "_Sub_Company_Area_Description", DbType = "VarChar(50)")]
        public string Sub_Company_Area_Description
        {
            get
            {
                return this._Sub_Company_Area_Description;
            }
            set
            {
                if ((this._Sub_Company_Area_Description != value))
                {
                    this._Sub_Company_Area_Description = value;
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

    public partial class rsp_ecGetSubCompanyDistrictDetailsResult
    {

        private int _Sub_Company_District_ID;

        private string _Sub_Company_District_Name;

        private string _Sub_Company_District_Description;

        private System.Nullable<int> _Staff_ID;

        private string _Staff_Last_Name;

        private string _Staff_First_Name;

        public rsp_ecGetSubCompanyDistrictDetailsResult()
        {
        }

        [Column(Storage = "_Sub_Company_District_ID", DbType = "Int NOT NULL")]
        public int Sub_Company_District_ID
        {
            get
            {
                return this._Sub_Company_District_ID;
            }
            set
            {
                if ((this._Sub_Company_District_ID != value))
                {
                    this._Sub_Company_District_ID = value;
                }
            }
        }

        [Column(Storage = "_Sub_Company_District_Name", DbType = "VarChar(50)")]
        public string Sub_Company_District_Name
        {
            get
            {
                return this._Sub_Company_District_Name;
            }
            set
            {
                if ((this._Sub_Company_District_Name != value))
                {
                    this._Sub_Company_District_Name = value;
                }
            }
        }

        [Column(Storage = "_Sub_Company_District_Description", DbType = "VarChar(50)")]
        public string Sub_Company_District_Description
        {
            get
            {
                return this._Sub_Company_District_Description;
            }
            set
            {
                if ((this._Sub_Company_District_Description != value))
                {
                    this._Sub_Company_District_Description = value;
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

    public partial class CompanyDefaultsResult
    {

        private string _Company_Percentage_Payout;

        private string _Company_Price_Per_Play;

        private string _Company_Jackpot;

        private System.Nullable<int> _Terms_Group_ID;

        private System.Nullable<int> _Access_Key_ID;

        private System.Nullable<int> _Staff_ID;

        public CompanyDefaultsResult()
        {
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
    }

    public partial class usp_ecUpdateSubCompanyAdminDefaultsResult
    {

        private System.Nullable<int> _CascadeType;

        public usp_ecUpdateSubCompanyAdminDefaultsResult()
        {
        }

        [Column(Storage = "_CascadeType", DbType = "Int")]
        public System.Nullable<int> CascadeType
        {
            get
            {
                return this._CascadeType;
            }
            set
            {
                if ((this._CascadeType != value))
                {
                    this._CascadeType = value;
                }
            }
        }
    }
}
