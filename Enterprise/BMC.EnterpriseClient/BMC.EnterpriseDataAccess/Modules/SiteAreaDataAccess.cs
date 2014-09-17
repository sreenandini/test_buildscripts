using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Reflection;

namespace BMC.EnterpriseDataAccess
{
    public partial class EnterpriseDataContext
    {
        [Function(Name = "dbo.rsp_getSubCompayRegions")]
        public ISingleResult<rsp_getSubCompayRegionsResult> GetSubCompayRegions([Parameter(Name = "Site_ID", DbType = "Int")] System.Nullable<int> site_ID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), site_ID);
            return ((ISingleResult<rsp_getSubCompayRegionsResult>)(result.ReturnValue));
        }
    }

    public partial class rsp_getSubCompayRegionsResult
    {

        private int _Sub_Company_Region_ID;

        private string _Sub_Company_Region_Name;

        private string _Sub_Company_Region_Description;

        private System.Nullable<int> _Staff_ID;

        private string _Staff_Last_Name;

        private string _Staff_First_Name;

        public rsp_getSubCompayRegionsResult()
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
}