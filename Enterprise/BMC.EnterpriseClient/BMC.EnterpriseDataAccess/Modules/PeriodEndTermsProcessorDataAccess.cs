using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Reflection;

namespace BMC.EnterpriseDataAccess
{
    public partial class EnterpriseDataContext
    {
        [Function(Name = "dbo.rsp_GetTermsConfigForPeriodEnd")]
        public ISingleResult<AvailableSchedules> GetAvailableSchedules()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<AvailableSchedules>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetFirstOpenPeriodEnd")]
        public ISingleResult<PeriodEndDate> GetFirstOpenPeriodEndDate([Parameter(Name = "currentDate", DbType = "VarChar(30)")]string periodEndDate)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), periodEndDate);
            return (ISingleResult<PeriodEndDate>)(result.ReturnValue);
        }

        [Function(Name = "dbo.rsp_GetDropForPeriodEndBySubCompany")]
        public ISingleResult<SubCompanyDetails> GetSubCompanyResult([Parameter(Name = "PeriodEndDate", DbType = "VarChar(30)")]string periodEndDate)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), periodEndDate);
            return ((ISingleResult<SubCompanyDetails>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_PECollectionExceptions")]
        public ISingleResult<CompanyExceptionCollection> GetCompanyExceptionCollection([Parameter(DbType = "Int")] System.Nullable<int> period_end_id)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), period_end_id);
            return ((ISingleResult<CompanyExceptionCollection>)(result.ReturnValue));
        }

        [Function(Name = "dbo.usp_CascadeUpdateSubCompany")]
        public int CascadeUpdateSubCompany([Parameter(Name = "CascadeOptions", DbType = "VarChar(20)")] string cascadeOptions, [Parameter(Name = "Value", DbType = "VarChar(40)")] string value, [Parameter(Name = "CascadeType", DbType = "VarChar(40)")] string cascadeType, [Parameter(Name = "Id", DbType = "Int")] System.Nullable<int> id, [Parameter(Name = "SetAsDefault", DbType = "Char(1)")] System.Nullable<char> setAsDefault, [Parameter(Name = "User_ID", DbType = "Int")] System.Nullable<int> user_ID, [Parameter(Name = "User_Name", DbType = "VarChar(50)")] string user_Name, [Parameter(Name = "Module_ID", DbType = "Int")] System.Nullable<int> module_ID, [Parameter(Name = "Module_Name", DbType = "VarChar(50)")] string module_Name)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), cascadeOptions, value, cascadeType, id, setAsDefault, user_ID, user_Name, module_ID, module_Name);
            return (int)result.ReturnValue;
        }

        [Function(Name = "dbo.rsp_GetCollectionsForPeriodEnd")]
        public ISingleResult<CollectionIds> GetCollectionsForPeriodEnd([Parameter(Name = "Period_End_ID", DbType = "Int")] System.Nullable<int> period_End_ID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), period_End_ID);
            return ((ISingleResult<CollectionIds>)(result.ReturnValue));
        }

        [Function(Name = "dbo.usp_ConfirmPeriodEnd")]
        public int ConfirmPeriodEnd([Parameter(Name = "Period_End_ID", DbType = "Int")] System.Nullable<int> period_End_ID, [Parameter(Name = "Sub_Company_ID", DbType = "Int")] System.Nullable<int> sub_Company_ID, [Parameter(Name = "Period_End_Doc_No", DbType = "Int")] System.Nullable<int> period_End_Doc_No)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), period_End_ID, sub_Company_ID, period_End_Doc_No);
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.usp_CreatePeriodEndDocNo")]
        public int CreatePeriodEndDocNo([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Period_End_DocNo", DbType = "VarChar(10)")] ref string period_End_DocNo)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), period_End_DocNo);
            period_End_DocNo = ((string)(result.GetParameterValue(0)));
            return ((int)(result.ReturnValue));
        }

        //[Function(Name = "dbo.usp_ConfirmPeriodEnd")]
        //public int ConfirmPeriodEnd([Parameter(Name = "Period_End_ID", DbType = "Int")] System.Nullable<int> period_End_ID, [Parameter(Name = "Sub_Company_ID", DbType = "Int")] System.Nullable<int> sub_Company_ID, [Parameter(Name = "Period_End_Doc_No", DbType = "Int")] System.Nullable<int> period_End_Doc_No)
        //{
        //    IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), period_End_ID, sub_Company_ID, period_End_Doc_No);
        //    return ((int)(result.ReturnValue));
        //}

        //SetPeriodEndDocNo

        //      .Parameters("@Period_End_ID") = lPeriodEndID
        //    .Parameters("@Sub_Company_ID") = lSubComp
        //    .Parameters("@Period_End_Doc_No") = lDocNo
    }

    public partial class CollectionIds
    {

        private int _collection_id;

        public CollectionIds()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_collection_id", DbType = "Int NOT NULL")]
        public int collection_id
        {
            get
            {
                return this._collection_id;
            }
            set
            {
                if ((this._collection_id != value))
                {
                    this._collection_id = value;
                }
            }
        }
    }


    public partial class PeriodEndDate
    {

        private System.Nullable<System.DateTime> _myDate;


        [Column(Storage = "_myDate", DbType = "DateTime")]
        public System.Nullable<System.DateTime> myDate
        {
            get
            {
                return this._myDate;
            }
            set
            {
                if ((this._myDate != value))
                {
                    this._myDate = value;
                }
            }
        }
    }

    public partial class AvailableSchedules
    {

        private string _Terms_Group_Name;

        private int _Terms_Group_ID;

        private System.Nullable<float> _Site_Share;

        private System.Nullable<float> _Operator_share;

        private System.Nullable<float> _Company_Share;

        public AvailableSchedules()
        {
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

        [Column(Storage = "_Site_Share", DbType = "Real")]
        public System.Nullable<float> Site_Share
        {
            get
            {
                return this._Site_Share;
            }
            set
            {
                if ((this._Site_Share != value))
                {
                    this._Site_Share = value;
                }
            }
        }

        [Column(Storage = "_Operator_share", DbType = "Real")]
        public System.Nullable<float> Operator_share
        {
            get
            {
                return this._Operator_share;
            }
            set
            {
                if ((this._Operator_share != value))
                {
                    this._Operator_share = value;
                }
            }
        }

        [Column(Storage = "_Company_Share", DbType = "Real")]
        public System.Nullable<float> Company_Share
        {
            get
            {
                return this._Company_Share;
            }
            set
            {
                if ((this._Company_Share != value))
                {
                    this._Company_Share = value;
                }
            }
        }
    }

    public partial class SubCompanyDetails
    {

        private string _Sub_Company_Name;

        private int _sub_company_id;

        private System.Nullable<int> _company_id;

        private int _Period_ID;

        private string _Period_Start;

        private string _Period_End;

        private System.Nullable<decimal> _Total_Net;

        public SubCompanyDetails()
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

        [Column(Storage = "_sub_company_id", DbType = "Int NOT NULL")]
        public int sub_company_id
        {
            get
            {
                return this._sub_company_id;
            }
            set
            {
                if ((this._sub_company_id != value))
                {
                    this._sub_company_id = value;
                }
            }
        }

        [Column(Storage = "_company_id", DbType = "Int")]
        public System.Nullable<int> company_id
        {
            get
            {
                return this._company_id;
            }
            set
            {
                if ((this._company_id != value))
                {
                    this._company_id = value;
                }
            }
        }

        [Column(Storage = "_Period_ID", DbType = "Int NOT NULL")]
        public int Period_ID
        {
            get
            {
                return this._Period_ID;
            }
            set
            {
                if ((this._Period_ID != value))
                {
                    this._Period_ID = value;
                }
            }
        }

        [Column(Storage = "_Period_Start", DbType = "VarChar(30)")]
        public string Period_Start
        {
            get
            {
                return this._Period_Start;
            }
            set
            {
                if ((this._Period_Start != value))
                {
                    this._Period_Start = value;
                }
            }
        }

        [Column(Storage = "_Period_End", DbType = "VarChar(30)")]
        public string Period_End
        {
            get
            {
                return this._Period_End;
            }
            set
            {
                if ((this._Period_End != value))
                {
                    this._Period_End = value;
                }
            }
        }

        [Column(Storage = "_Total_Net", DbType = "Decimal(20,2)")]
        public System.Nullable<decimal> Total_Net
        {
            get
            {
                return this._Total_Net;
            }
            set
            {
                if ((this._Total_Net != value))
                {
                    this._Total_Net = value;
                }
            }
        }
    }

    public partial class CompanyExceptionCollection
    {

        private string _SiteName;

        private int _Site_ID;

        private string _Site_Code;

        private System.Nullable<double> _CashIn;

        private System.Nullable<double> _CashOut;

        private System.Nullable<double> _TicketIn;

        private System.Nullable<double> _Bills;

        private System.Nullable<double> _TicketOut;

        private System.Nullable<double> _Handpays;

        private System.Nullable<double> _Net;

        private System.Nullable<int> _Machines;

        private System.Nullable<int> _CollCount;

        private System.Nullable<int> _BatchCount;

        private System.Nullable<int> _Week_Id;

        private System.Nullable<int> _Batch_Id;

        private System.Nullable<int> _WeekNumber;

        private string _WeekStartDate;

        private string _WeekEndDate;

        public CompanyExceptionCollection()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_SiteName", DbType = "VarChar(50)")]
        public string SiteName
        {
            get
            {
                return this._SiteName;
            }
            set
            {
                if ((this._SiteName != value))
                {
                    this._SiteName = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Site_ID", DbType = "Int NOT NULL")]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Site_Code", DbType = "VarChar(50)")]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CashIn", DbType = "Float")]
        public System.Nullable<double> CashIn
        {
            get
            {
                return this._CashIn;
            }
            set
            {
                if ((this._CashIn != value))
                {
                    this._CashIn = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CashOut", DbType = "Float")]
        public System.Nullable<double> CashOut
        {
            get
            {
                return this._CashOut;
            }
            set
            {
                if ((this._CashOut != value))
                {
                    this._CashOut = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TicketIn", DbType = "Float")]
        public System.Nullable<double> TicketIn
        {
            get
            {
                return this._TicketIn;
            }
            set
            {
                if ((this._TicketIn != value))
                {
                    this._TicketIn = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Bills", DbType = "Float")]
        public System.Nullable<double> Bills
        {
            get
            {
                return this._Bills;
            }
            set
            {
                if ((this._Bills != value))
                {
                    this._Bills = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TicketOut", DbType = "Float")]
        public System.Nullable<double> TicketOut
        {
            get
            {
                return this._TicketOut;
            }
            set
            {
                if ((this._TicketOut != value))
                {
                    this._TicketOut = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Handpays", DbType = "Float")]
        public System.Nullable<double> Handpays
        {
            get
            {
                return this._Handpays;
            }
            set
            {
                if ((this._Handpays != value))
                {
                    this._Handpays = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Net", DbType = "Float")]
        public System.Nullable<double> Net
        {
            get
            {
                return this._Net;
            }
            set
            {
                if ((this._Net != value))
                {
                    this._Net = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Machines", DbType = "Int")]
        public System.Nullable<int> Machines
        {
            get
            {
                return this._Machines;
            }
            set
            {
                if ((this._Machines != value))
                {
                    this._Machines = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CollCount", DbType = "Int")]
        public System.Nullable<int> CollCount
        {
            get
            {
                return this._CollCount;
            }
            set
            {
                if ((this._CollCount != value))
                {
                    this._CollCount = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_BatchCount", DbType = "Int")]
        public System.Nullable<int> BatchCount
        {
            get
            {
                return this._BatchCount;
            }
            set
            {
                if ((this._BatchCount != value))
                {
                    this._BatchCount = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Week_Id", DbType = "Int")]
        public System.Nullable<int> Week_Id
        {
            get
            {
                return this._Week_Id;
            }
            set
            {
                if ((this._Week_Id != value))
                {
                    this._Week_Id = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Batch_Id", DbType = "Int")]
        public System.Nullable<int> Batch_Id
        {
            get
            {
                return this._Batch_Id;
            }
            set
            {
                if ((this._Batch_Id != value))
                {
                    this._Batch_Id = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_WeekNumber", DbType = "Int")]
        public System.Nullable<int> WeekNumber
        {
            get
            {
                return this._WeekNumber;
            }
            set
            {
                if ((this._WeekNumber != value))
                {
                    this._WeekNumber = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_WeekStartDate", DbType = "VarChar(30)")]
        public string WeekStartDate
        {
            get
            {
                return this._WeekStartDate;
            }
            set
            {
                if ((this._WeekStartDate != value))
                {
                    this._WeekStartDate = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_WeekEndDate", DbType = "VarChar(30)")]
        public string WeekEndDate
        {
            get
            {
                return this._WeekEndDate;
            }
            set
            {
                if ((this._WeekEndDate != value))
                {
                    this._WeekEndDate = value;
                }
            }
        }
    }
}
