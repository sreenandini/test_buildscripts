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

        [Function(Name = "dbo.rsp_GetSubCompanyDefault")]
        [ResultType(typeof(TermsgroupDefaults))]
        [ResultType(typeof(AcessKeyDefaultResult))]
        [ResultType(typeof(RepresentitiveDefaultResult))]
        public IMultipleResults GetSubCompanyDefault([Parameter(Name = "SiteID", DbType = "Int")] System.Nullable<int> siteID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), siteID);
            return ((IMultipleResults)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_DefaultScreenApply")]
        [ResultType(typeof(DefaultTerms_Group_IDResult))]
        [ResultType(typeof(DefaultAccess_Key_IDResult))]
        [ResultType(typeof(DefaultSub_CompanyStaff_IDResult))]

        public IMultipleResults DefaultScreenApply([Parameter(Name = "Site_ID", DbType = "Int")] System.Nullable<int> site_ID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), site_ID);
            return ((IMultipleResults)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetTermsGroup")]
        public ISingleResult<rsp_GetTermsGroupResult>rsp_GetTermsGroup()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<rsp_GetTermsGroupResult>)(result.ReturnValue));
        }
        [Function(Name = "dbo.rsp_GetAccesskey")]
        public ISingleResult<rsp_GetAccesskeyResult> rsp_GetAccesskey()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<rsp_GetAccesskeyResult>)(result.ReturnValue));
        }
        [Function(Name = "dbo.rsp_GetRepresentativecheck")]
        public ISingleResult<rsp_GetRepresentativecheckResult>GetRepresentativecheck()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<rsp_GetRepresentativecheckResult>)(result.ReturnValue));
        }
   
        [Function(Name = "dbo.rsp_UpdateAccesskey")]
        public int UpdateAccesskey([Parameter(Name = "Site_ID", DbType = "Int")] System.Nullable<int> site_ID, [Parameter(Name = "Access_Key_ID", DbType = "Int")] System.Nullable<int> access_Key_ID, [Parameter(Name = "Access_Key_ID_Default", DbType = "Bit")] System.Nullable<bool> access_Key_ID_Default)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), site_ID, access_Key_ID, access_Key_ID_Default);
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_UpdateRepresentitive")]
        public int UpdateRepresentitive([Parameter(Name = "Site_ID", DbType = "Int")] System.Nullable<int> site_ID, [Parameter(Name = "Staff_ID", DbType = "Int")] System.Nullable<int> staff_ID, [Parameter(Name = "Staff_ID_Default", DbType = "Bit")] System.Nullable<bool> staff_ID_Default)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), site_ID, staff_ID, staff_ID_Default);
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_UpdateTermsGroup")]
        public int UpdateTermsGroup([Parameter(Name = "Site_ID", DbType = "Int")] System.Nullable<int> site_ID, [Parameter(Name = "Terms_Group_ID", DbType = "Int")] System.Nullable<int> terms_Group_ID, [Parameter(Name = "Terms_Group_ID_Default", DbType = "Bit")] System.Nullable<bool> terms_Group_ID_Default)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), site_ID, terms_Group_ID, terms_Group_ID_Default);
            return ((int)(result.ReturnValue));
        }
        

    public partial class TermsgroupDefaults
    {

        private System.Nullable<int> _Terms_group_id;

        private string _Terms_group_Name;
        
        [Column(Storage = "_Terms_group_id", DbType = "Int NOT NULL")]
        public System.Nullable<int> Terms_group_id
        {
            get
            {
                return this._Terms_group_id;
            }
            set
            {
                if ((this._Terms_group_id != value))
                {
                    this._Terms_group_id = value;
                }
            }
        }

        [Column(Storage = "_Terms_group_Name", DbType = "VarChar(50)")]
        public string Terms_group_Name
        {
            get
            {
                return this._Terms_group_Name;
            }
            set
            {
                if ((this._Terms_group_Name != value))
                {
                    this._Terms_group_Name = value;
                }
            }
        }
    }

    public partial class AcessKeyDefaultResult
    {

        private System.Nullable<int> _Access_Key_ID;

        private string _Access_Key_Name;

        public AcessKeyDefaultResult()
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
    public partial class RepresentitiveDefaultResult
    {

        private System.Nullable<int> _Staff_ID;

        private string _Staff_Last_Name;

        private string _Staff_First_Name;

        public RepresentitiveDefaultResult()
        {
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



    public partial class DefaultTerms_Group_IDResult
    {

        private System.Nullable<int> _Terms_Group_ID;

        public DefaultTerms_Group_IDResult()
        {
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
    }
    public partial class DefaultAccess_Key_IDResult
    {

        private System.Nullable<int> _Access_Key_ID;

        public DefaultAccess_Key_IDResult()
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
    }

    public partial class DefaultSub_CompanyStaff_IDResult
    {

        private System.Nullable<int> _Staff_ID;

        public DefaultSub_CompanyStaff_IDResult()
        {
        }

        [Column(Storage = "_Staff_ID", DbType = "Int")]
        public System.Nullable<int> DStaff_ID
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
    public partial class rsp_GetTermsGroupResult
    {

        private int _Terms_Group_ID;

        private string _Terms_Group_Name;

        public rsp_GetTermsGroupResult()
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

    public partial class rsp_GetAccesskeyResult
    {

        private int _Access_Key_ID;

        private string _Access_Key_Name;

        private string _Access_Key_Ref;

        private string _Access_Key_Manufacturer;

        private string _Access_Key_Type;

        public rsp_GetAccesskeyResult()
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

    public partial class rsp_GetRepresentativecheckResult
    {

        private string _Staff_Last_Name;

        private string _Staff_First_Name;

        private int _Staff_ID;

        public rsp_GetRepresentativecheckResult()
        {
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
    



}
}














