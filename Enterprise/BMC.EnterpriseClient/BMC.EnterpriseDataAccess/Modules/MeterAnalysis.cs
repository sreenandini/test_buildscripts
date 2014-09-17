using BMC.Common.LogManagement;
using BMC.Common.Utilities;
using BMC.DataAccess;
using System;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Data.SqlClient;
using System.Reflection;

namespace BMC.EnterpriseDataAccess
{
    public partial class MeterAnalysisDataContext
    {
        public DataTable GetMeterAnalysisData(SqlParameter[] sqlParams,string SP_Name)
        {
            DataSet dsMainGrid = new DataSet();

            try
            {
                string strConnectionString = MeterAnalysisDataContextHelper.ConnectionString;

                if (!string.IsNullOrEmpty(strConnectionString))
                {
                    SqlHelper.FillDataset(strConnectionString, SP_Name, dsMainGrid, new string[] { "MainGrid" }, sqlParams);
                    return dsMainGrid.Tables[0];
                }
                else
                {
                    return new DataTable();
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error in getting data for main grid --" + ex.Message, LogManager.enumLogLevel.Error);
                return new DataTable();
            }
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.rsp_GetCommonData")]
        [ResultType(typeof(Operator))]
        [ResultType(typeof(Machine_Type))]
        [ResultType(typeof(rsp_Manufacturer))]
        [ResultType(typeof(rsp_GetGameCategoryResult))]
        public IMultipleResults GetMeterAnalysis_CommonData()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((IMultipleResults)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.rsp_GetGameTitleForCategory")]
        public ISingleResult<rsp_GetGameTitleForCategory> GetGameTitleForCategory([Parameter(Name = "Game_Category_ID", DbType = "Int")] System.Nullable<int> game_Category_ID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), game_Category_ID);
            return ((ISingleResult<rsp_GetGameTitleForCategory>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.rsp_GetDepotList")]
        public ISingleResult<rsp_GetDepoDetailsResult> GetDepoList([Parameter(DbType = "Int")] System.Nullable<int> SupplierID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), SupplierID);
            return ((ISingleResult<rsp_GetDepoDetailsResult>)(result.ReturnValue));
        }
        
        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.rsp_GetBaseDenomForMA")]
        public ISingleResult<MGMDInstallationDenom> GetBaseDenomForMeterAnalysis([Parameter(Name = "GameTitleID",DbType= "Int")]System.Nullable<int> iGameTitleID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())),iGameTitleID);
            return ((ISingleResult<MGMDInstallationDenom>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.rsp_GetPayoutValue")]
        public ISingleResult<PaytablePercent> GetPayoutPercent([Parameter(Name = "MGMD_Denom", DbType = "Int")]System.Nullable<int> iMGMD_Denom, [Parameter(Name = "GameTitleID", DbType = "Int")]System.Nullable<int> iGameTitleID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), iMGMD_Denom, iGameTitleID);
            return ((ISingleResult<PaytablePercent>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.rsp_GetSiteDetailsForMA")]
        public ISingleResult<rsp_GetSiteDetailsForMAResult> GetOrganisationHierarchy([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "SiteID", DbType = "Int")] System.Nullable<int> siteID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "DistrictID", DbType = "Int")] System.Nullable<int> districtID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "AreaID", DbType = "Int")] System.Nullable<int> areaID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "RegionID", DbType = "Int")] System.Nullable<int> regionID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "SubCompanyID", DbType = "Int")] System.Nullable<int> subCompanyID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "CompanyID", DbType = "Int")] System.Nullable<int> companyID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "SiteStatusID", DbType = "Int")] System.Nullable<int> siteStatusID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "UserID", DbType = "Int")] System.Nullable<int> userID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), siteID, districtID, areaID, regionID, subCompanyID, companyID, siteStatusID, userID);
            return ((ISingleResult<rsp_GetSiteDetailsForMAResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.rsp_GetMachineClassList")]
        public ISingleResult<rsp_GetGameTitleForCategory> GetMachineClassList([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "TypeID", DbType = "Int")] System.Nullable<int> typeID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), typeID);
            return ((ISingleResult<rsp_GetGameTitleForCategory>)(result.ReturnValue));
        }

    }

    public partial class Operator
    {

        private int _Operator_ID;

        private string _Operator_Name;

        public Operator()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Operator_ID", DbType = "Int NOT NULL")]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Operator_Name", DbType = "VarChar(50)")]
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
    }

    public partial class Machine_Type
    {

        private int _Machine_Type_ID;

        private string _Machine_Type_Code;

        public Machine_Type()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Machine_Type_ID", DbType = "Int NOT NULL")]
        public int Machine_Type_ID
        {
            get
            {
                return this._Machine_Type_ID;
            }
            set
            {
                if ((this._Machine_Type_ID != value))
                {
                    this._Machine_Type_ID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Machine_Type_Code", DbType = "VarChar(50)")]
        public string Machine_Type_Code
        {
            get
            {
                return this._Machine_Type_Code;
            }
            set
            {
                if ((this._Machine_Type_Code != value))
                {
                    this._Machine_Type_Code = value;
                }
            }
        }
    }

    public partial class rsp_Manufacturer
    {

        private int _Manufacturer_ID;

        private string _Manufacturer_Name;

        public rsp_Manufacturer()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Manufacturer_ID", DbType = "Int NOT NULL")]
        public int Manufacturer_ID
        {
            get
            {
                return this._Manufacturer_ID;
            }
            set
            {
                if ((this._Manufacturer_ID != value))
                {
                    this._Manufacturer_ID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Manufacturer_Name", DbType = "VarChar(50)")]
        public string Manufacturer_Name
        {
            get
            {
                return this._Manufacturer_Name;
            }
            set
            {
                if ((this._Manufacturer_Name != value))
                {
                    this._Manufacturer_Name = value;
                }
            }
        }
    }

    public partial class rsp_GetGameTitleForCategory
    {

        private int _Game_Title_ID;

        private string _Game_Title;

        public rsp_GetGameTitleForCategory()
        {
        }

        [Column(Storage = "_Game_Title_ID", DbType = "Int NOT NULL")]
        public int Game_Title_ID
        {
            get
            {
                return this._Game_Title_ID;
            }
            set
            {
                if ((this._Game_Title_ID != value))
                {
                    this._Game_Title_ID = value;
                }
            }
        }
        
        [Column(Storage = "_Game_Title", DbType = "VarChar(100)")]
        public string Game_Title
        {
            get
            {
                return this._Game_Title;
            }
            set
            {
                if ((this._Game_Title != value))
                {
                    this._Game_Title = value;
                }
            }
        }
    }

    public partial class MGMDInstallationDenom
    {

        private System.Nullable<int> _MGMD_Denom;

        private System.Nullable<decimal> _MGMD_Denom_Value;

        public MGMDInstallationDenom()
        {

        }

		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_MGMD_Denom", DbType="Int")]
		public System.Nullable<int> MGMD_Denom
		{
			get
			{
				return this._MGMD_Denom;
			}
			set
			{
				if ((this._MGMD_Denom != value))
				{
					this._MGMD_Denom = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_MGMD_Denom_Value", DbType="Decimal(10,2)")]
		public System.Nullable<decimal> MGMD_Denom_Value
		{
			get
			{
				return this._MGMD_Denom_Value;
			}
			set
			{
				if ((this._MGMD_Denom_Value != value))
				{
					this._MGMD_Denom_Value = value;
				}
			}
		}
    }

    public partial class PaytablePercent
    {
        private System.Nullable<decimal> _TheoreticalPayout;
        private float _TheoPayout;

        public PaytablePercent()
        {

        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TheoPayout", DbType = "FLOAT")]
        public float TheoPayout
        {
            get
            {
                return this._TheoPayout;
            }
            set
            {
                if ((this._TheoPayout != value))
                {
                    this._TheoPayout = value;
                }
            }
        }
        
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TheoreticalPayout", DbType = "Decimal(10,2)")]
        public System.Nullable<decimal> TheoreticalPayout
        {
            get
            {
                return this._TheoreticalPayout;
            }
            set
            {
                if ((this._TheoreticalPayout != value))
                {
                    this._TheoreticalPayout = value;
                }
            }
        }
    }

    public partial class rsp_GetSiteDetailsForMAResult
    {

        private string _Company_Name;

        private string _Sub_Company_Name;

        private string _Site_Name;

        private string _Sub_Company_Area_Name;

        private string _Sub_Company_District_Name;

        private string _Sub_Company_Region_Name;

        private int _Site_ID;

        private int _Company_ID;

        private int _Sub_Company_ID;

        private string _Site_Code;

        private System.Nullable<int> _Sub_Company_Area_ID;

        private System.Nullable<int> _Sub_Company_District_ID;

        private System.Nullable<int> _Sub_Company_Region_ID;

        public rsp_GetSiteDetailsForMAResult()
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Sub_Company_Area_Name", DbType = "VarChar(50)")]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Sub_Company_District_Name", DbType = "VarChar(50)")]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Sub_Company_Region_Name", DbType = "VarChar(50)")]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Sub_Company_Area_ID", DbType = "Int")]
        public System.Nullable<int> Sub_Company_Area_ID
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Sub_Company_District_ID", DbType = "Int")]
        public System.Nullable<int> Sub_Company_District_ID
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Sub_Company_Region_ID", DbType = "Int")]
        public System.Nullable<int> Sub_Company_Region_ID
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
    }
}
