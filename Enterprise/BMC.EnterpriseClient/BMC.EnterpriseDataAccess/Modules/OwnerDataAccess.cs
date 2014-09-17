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

        [Function(Name = "dbo.rsp_GetCompanyName")]
        public ISingleResult<rsp_GetCompanyNameResult> GetCompanyName()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<rsp_GetCompanyNameResult>)(result.ReturnValue));
        }
        [Function(Name = "dbo.rsp_Getscompany")]
        public ISingleResult<rsp_GetscompanyResult> Getscompany([Parameter(DbType = "Int")] System.Nullable<int> company)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), company);
            return ((ISingleResult<rsp_GetscompanyResult>)(result.ReturnValue));
        }

        //[Function(Name = "dbo.rsp_SendSubCompanyValue")]
        //public ISingleResult<rsp_SendSubCompanyValueResult> SendSubCompanyValue([Parameter(Name = "SubCompanyID", DbType = "Int")] System.Nullable<int> subCompanyID)
        //{
        //    IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), subCompanyID);
        //    return ((ISingleResult<rsp_SendSubCompanyValueResult>)(result.ReturnValue));
        //}

        [Function(Name = "dbo.rsp_GetCompanyAccesstoCustomer")]
        public ISingleResult<rsp_GetCompanyAccesstoCustomerResult>GetCompanyAccesstoCustomer([Parameter(Name = "StaffId", DbType = "Int")] System.Nullable<int> staffId)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), staffId);
            return ((ISingleResult<rsp_GetCompanyAccesstoCustomerResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetCustomerAccessSubCompany")]
        public ISingleResult<rsp_GetCustomerAccessSubCompanyResult>GetCustomerAccessSubCompany([Parameter(Name = "CompanyID", DbType = "Int")] System.Nullable<int> companyID, [Parameter(Name = "StaffId", DbType = "Int")] System.Nullable<int> staffId)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), companyID, staffId);
            return ((ISingleResult<rsp_GetCustomerAccessSubCompanyResult>)(result.ReturnValue));
        }
        
        public partial class rsp_GetCompanyNameResult
        {

            private string _Company_Name;

            private int _Company_ID;

            public rsp_GetCompanyNameResult()
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
        }

        public partial class rsp_GetscompanyResult
        {

            private int _Sub_Company_ID;

            private string _Sub_Company_Name;

            public rsp_GetscompanyResult()
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
    
    //public partial class rsp_SendSubCompanyValueResult
    //{

    //    private int _Sub_Company_Region_ID;

    //    private string _Sub_Company_Region_Name;

    //    private System.Nullable<int> _Staff_ID;

    //    private string _Staff_Last_Name;

    //    private string _Staff_First_Name;

    //    public rsp_SendSubCompanyValueResult()
    //    {
    //    }

    //    [Column(Storage = "_Sub_Company_Region_ID", DbType = "Int NOT NULL")]
    //    public int Sub_Company_Region_ID
    //    {
    //        get
    //        {
    //            return this._Sub_Company_Region_ID;
    //        }
    //        set
    //        {
    //            if ((this._Sub_Company_Region_ID != value))
    //            {
    //                this._Sub_Company_Region_ID = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_Sub_Company_Region_Name", DbType = "VarChar(50)")]
    //    public string Sub_Company_Region_Name
    //    {
    //        get
    //        {
    //            return this._Sub_Company_Region_Name;
    //        }
    //        set
    //        {
    //            if ((this._Sub_Company_Region_Name != value))
    //            {
    //                this._Sub_Company_Region_Name = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_Staff_ID", DbType = "Int")]
    //    public System.Nullable<int> Staff_ID
    //    {
    //        get
    //        {
    //            return this._Staff_ID;
    //        }
    //        set
    //        {
    //            if ((this._Staff_ID != value))
    //            {
    //                this._Staff_ID = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_Staff_Last_Name", DbType = "VarChar(50)")]
    //    public string Staff_Last_Name
    //    {
    //        get
    //        {
    //            return this._Staff_Last_Name;
    //        }
    //        set
    //        {
    //            if ((this._Staff_Last_Name != value))
    //            {
    //                this._Staff_Last_Name = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_Staff_First_Name", DbType = "VarChar(50)")]
    //    public string Staff_First_Name
    //    {
    //        get
    //        {
    //            return this._Staff_First_Name;
    //        }
    //        set
    //        {
    //            if ((this._Staff_First_Name != value))
    //            {
    //                this._Staff_First_Name = value;
    //            }
    //        }
    //    }

    //}

    public partial class rsp_GetCompanyAccesstoCustomerResult
    {

        private int _Company_ID;

        private string _Company_Name;

        public rsp_GetCompanyAccesstoCustomerResult()
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

    public partial class rsp_GetCustomerAccessSubCompanyResult
    {

        private int _Sub_Company_ID;

        private string _Sub_Company_Name;

        public rsp_GetCustomerAccessSubCompanyResult()
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

        [Column(Storage = "_Sub_Company_Name", DbType = "VarChar(61)")]
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

