using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;
using System.Reflection;
using System.Data.Linq;

namespace BMC.EnterpriseDataAccess
{
    public partial class EnterpriseDataContext
    {
        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.rsp_GetCompanyName")]
        public ISingleResult<CompanyInfo> GetAllCompanyNames()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<CompanyInfo>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.rsp_GetSubCompanyNameForCompanyID")]
        public ISingleResult<SubCompanyInfo> GetAllSubCompanyNamesForCompanyID([Parameter(Name = "CompanyID", DbType = "INT")] int CompanyID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), CompanyID);
            return ((ISingleResult<SubCompanyInfo>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.rsp_GetSiteNameForSubCompanyID")]
        public ISingleResult<SiteInfo> GetAllSiteNamesForSubCompanyID([Parameter(Name = "SubCompanyID", DbType = "INT")] int SubCompanyID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), SubCompanyID);
            return ((ISingleResult<SiteInfo>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.usp_UpdateBarPositionWithTermsInfo")]
        public int UpdateBarPositionWithTermsInfo([Parameter(Name = "ValidationFlag", DbType = "BIT")] bool ValidationFlag,
                                               [Parameter(Name = "PreDate", DbType = "DATETIME")] DateTime DtPreDate,
                                               [Parameter(Name = "PostDate", DbType = "DATETIME")] DateTime dtPostDate,
                                               [Parameter(Name = "SiteID", DbType = "INT")] int SiteID,
                                               [Parameter(Name = "CompanyID", DbType = "INT")] int CompanyID,
                                               [Parameter(Name = "SubCompanyID", DbType = "INT")] int SubCompanyID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), ValidationFlag, DtPreDate, dtPostDate, SiteID, CompanyID, SubCompanyID);
            return ((int)(result.ReturnValue));
        }

        public class CompanyInfo
        {

            private string _Company_Name;

            private int _Company_ID;

            public CompanyInfo()
            {
            }

            [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Company_Name", DbType = "VarChar(50)")]
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

            [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Company_ID", DbType = "Int NOT NULL")]
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

        public class SubCompanyInfo
        {

            private string _Sub_Company_Name;

            private int _Sub_Company_ID;

            public SubCompanyInfo()
            {
            }

            [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Sub_Company_Name", DbType = "VarChar(50)")]
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

            [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Sub_Company_ID", DbType = "Int NOT NULL")]
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
        }

        public class SiteInfo
        {

            private string _Site_Name;

            private int _Site_ID;

            public SiteInfo()
            {
            }

            [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Site_Name", DbType = "VarChar(50)")]
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
        }

    }
}
