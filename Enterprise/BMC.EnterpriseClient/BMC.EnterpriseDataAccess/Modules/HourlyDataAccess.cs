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
        [Function(Name = "dbo.rsp_24HourStatisticsByType3")]
        public ISingleResult<rsp_24HourStatisticsByType3Result> rsp_24HourStatisticsByType3([Parameter(DbType = "Int")] System.Nullable<int> starthour, [Parameter(DbType = "Int")] System.Nullable<int> rows, [Parameter(Name = "DataType", DbType = "VarChar(50)")] string dataType, [Parameter(DbType = "Int")] System.Nullable<int> category, [Parameter(DbType = "Int")] System.Nullable<int> zone, [Parameter(DbType = "Int")] System.Nullable<int> position, [Parameter(DbType = "DateTime")] System.Nullable<System.DateTime> date, [Parameter(DbType = "Int")] System.Nullable<int> site)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), starthour, rows, dataType, category, zone, position, date, site);
            return ((ISingleResult<rsp_24HourStatisticsByType3Result>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetHourlyStatisticsTypes")]
        public ISingleResult<rsp_GetHourlyStatisticsTypesResult> rsp_GetHourlyStatisticsTypes()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<rsp_GetHourlyStatisticsTypesResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetSiteInfo")]
        public ISingleResult<rsp_GetSiteInfoResult> rsp_GetSiteInfo([Parameter(Name = "SubCompanyId", DbType = "Int")] System.Nullable<int> subCompanyId)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), subCompanyId);
            return ((ISingleResult<rsp_GetSiteInfoResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetHourlyFilterByInfo")]
        public ISingleResult<rsp_GetHourlyFilterByInfoResult> rsp_GetHourlyFilterByInfo([Parameter(Name = "FilterBy", DbType = "Int")] System.Nullable<int> filterBy, [Parameter(Name = "FilterById", DbType = "Int")] System.Nullable<int> filterById)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), filterBy, filterById);
            return ((ISingleResult<rsp_GetHourlyFilterByInfoResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.rsp_GetBarPositionsEnrolledOnGamingDay")]
        public ISingleResult<rsp_GetBarPositionsEnrolledOnGamingDayResult> rsp_GetBarPositionsEnrolledOnGamingDay([global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "DateTime")] System.Nullable<System.DateTime> date, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "Int")] System.Nullable<int> starthour)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), date, starthour);
            return ((ISingleResult<rsp_GetBarPositionsEnrolledOnGamingDayResult>)(result.ReturnValue));
        }
    }

    public partial class rsp_24HourStatisticsByType3Result
    {
        private System.Nullable<int> _ID;

        private System.DateTime _Date;

        private string _Bar_Position_Name;

        private string _Machine_Name;

        private string _Machine_Category;

        private System.Nullable<double> _HS_Hour1_Value;

        private System.Nullable<double> _HS_Hour2_Value;

        private System.Nullable<double> _HS_Hour3_Value;

        private System.Nullable<double> _HS_Hour4_Value;

        private System.Nullable<double> _HS_Hour5_Value;

        private System.Nullable<double> _HS_Hour6_Value;

        private System.Nullable<double> _HS_Hour7_Value;

        private System.Nullable<double> _HS_Hour8_Value;

        private System.Nullable<double> _HS_Hour9_Value;

        private System.Nullable<double> _HS_Hour10_Value;

        private System.Nullable<double> _HS_Hour11_Value;

        private System.Nullable<double> _HS_Hour12_Value;

        private System.Nullable<double> _HS_Hour13_Value;

        private System.Nullable<double> _HS_Hour14_Value;

        private System.Nullable<double> _HS_Hour15_Value;

        private System.Nullable<double> _HS_Hour16_Value;

        private System.Nullable<double> _HS_Hour17_Value;

        private System.Nullable<double> _HS_Hour18_Value;

        private System.Nullable<double> _HS_Hour19_Value;

        private System.Nullable<double> _HS_Hour20_Value;

        private System.Nullable<double> _HS_Hour21_Value;

        private System.Nullable<double> _HS_Hour22_Value;

        private System.Nullable<double> _HS_Hour23_Value;

        private System.Nullable<double> _HS_Hour24_Value;

        private System.Nullable<double> _Total;

        public rsp_24HourStatisticsByType3Result()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ID", DbType = "Int")]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Date", DbType = "DateTime NOT NULL")]
        public System.DateTime Date
        {
            get
            {
                return this._Date;
            }
            set
            {
                if ((this._Date != value))
                {
                    this._Date = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Bar_Position_Name", DbType = "VarChar(50)")]
        public string Bar_Position_Name
        {
            get
            {
                return this._Bar_Position_Name;
            }
            set
            {
                if ((this._Bar_Position_Name != value))
                {
                    this._Bar_Position_Name = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Machine_Name", DbType = "VarChar(50)")]
        public string Machine_Name
        {
            get
            {
                return this._Machine_Name;
            }
            set
            {
                if ((this._Machine_Name != value))
                {
                    this._Machine_Name = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Machine_Category", DbType = "VarChar(50)")]
        public string Machine_Category
        {
            get
            {
                return this._Machine_Category;
            }
            set
            {
                if ((this._Machine_Category != value))
                {
                    this._Machine_Category = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_HS_Hour1_Value", DbType = "Float")]
        public System.Nullable<double> HS_Hour1_Value
        {
            get
            {
                return this._HS_Hour1_Value;
            }
            set
            {
                if ((this._HS_Hour1_Value != value))
                {
                    this._HS_Hour1_Value = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_HS_Hour2_Value", DbType = "Float")]
        public System.Nullable<double> HS_Hour2_Value
        {
            get
            {
                return this._HS_Hour2_Value;
            }
            set
            {
                if ((this._HS_Hour2_Value != value))
                {
                    this._HS_Hour2_Value = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_HS_Hour3_Value", DbType = "Float")]
        public System.Nullable<double> HS_Hour3_Value
        {
            get
            {
                return this._HS_Hour3_Value;
            }
            set
            {
                if ((this._HS_Hour3_Value != value))
                {
                    this._HS_Hour3_Value = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_HS_Hour4_Value", DbType = "Float")]
        public System.Nullable<double> HS_Hour4_Value
        {
            get
            {
                return this._HS_Hour4_Value;
            }
            set
            {
                if ((this._HS_Hour4_Value != value))
                {
                    this._HS_Hour4_Value = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_HS_Hour5_Value", DbType = "Float")]
        public System.Nullable<double> HS_Hour5_Value
        {
            get
            {
                return this._HS_Hour5_Value;
            }
            set
            {
                if ((this._HS_Hour5_Value != value))
                {
                    this._HS_Hour5_Value = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_HS_Hour6_Value", DbType = "Float")]
        public System.Nullable<double> HS_Hour6_Value
        {
            get
            {
                return this._HS_Hour6_Value;
            }
            set
            {
                if ((this._HS_Hour6_Value != value))
                {
                    this._HS_Hour6_Value = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_HS_Hour7_Value", DbType = "Float")]
        public System.Nullable<double> HS_Hour7_Value
        {
            get
            {
                return this._HS_Hour7_Value;
            }
            set
            {
                if ((this._HS_Hour7_Value != value))
                {
                    this._HS_Hour7_Value = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_HS_Hour8_Value", DbType = "Float")]
        public System.Nullable<double> HS_Hour8_Value
        {
            get
            {
                return this._HS_Hour8_Value;
            }
            set
            {
                if ((this._HS_Hour8_Value != value))
                {
                    this._HS_Hour8_Value = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_HS_Hour9_Value", DbType = "Float")]
        public System.Nullable<double> HS_Hour9_Value
        {
            get
            {
                return this._HS_Hour9_Value;
            }
            set
            {
                if ((this._HS_Hour9_Value != value))
                {
                    this._HS_Hour9_Value = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_HS_Hour10_Value", DbType = "Float")]
        public System.Nullable<double> HS_Hour10_Value
        {
            get
            {
                return this._HS_Hour10_Value;
            }
            set
            {
                if ((this._HS_Hour10_Value != value))
                {
                    this._HS_Hour10_Value = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_HS_Hour11_Value", DbType = "Float")]
        public System.Nullable<double> HS_Hour11_Value
        {
            get
            {
                return this._HS_Hour11_Value;
            }
            set
            {
                if ((this._HS_Hour11_Value != value))
                {
                    this._HS_Hour11_Value = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_HS_Hour12_Value", DbType = "Float")]
        public System.Nullable<double> HS_Hour12_Value
        {
            get
            {
                return this._HS_Hour12_Value;
            }
            set
            {
                if ((this._HS_Hour12_Value != value))
                {
                    this._HS_Hour12_Value = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_HS_Hour13_Value", DbType = "Float")]
        public System.Nullable<double> HS_Hour13_Value
        {
            get
            {
                return this._HS_Hour13_Value;
            }
            set
            {
                if ((this._HS_Hour13_Value != value))
                {
                    this._HS_Hour13_Value = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_HS_Hour14_Value", DbType = "Float")]
        public System.Nullable<double> HS_Hour14_Value
        {
            get
            {
                return this._HS_Hour14_Value;
            }
            set
            {
                if ((this._HS_Hour14_Value != value))
                {
                    this._HS_Hour14_Value = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_HS_Hour15_Value", DbType = "Float")]
        public System.Nullable<double> HS_Hour15_Value
        {
            get
            {
                return this._HS_Hour15_Value;
            }
            set
            {
                if ((this._HS_Hour15_Value != value))
                {
                    this._HS_Hour15_Value = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_HS_Hour16_Value", DbType = "Float")]
        public System.Nullable<double> HS_Hour16_Value
        {
            get
            {
                return this._HS_Hour16_Value;
            }
            set
            {
                if ((this._HS_Hour16_Value != value))
                {
                    this._HS_Hour16_Value = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_HS_Hour17_Value", DbType = "Float")]
        public System.Nullable<double> HS_Hour17_Value
        {
            get
            {
                return this._HS_Hour17_Value;
            }
            set
            {
                if ((this._HS_Hour17_Value != value))
                {
                    this._HS_Hour17_Value = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_HS_Hour18_Value", DbType = "Float")]
        public System.Nullable<double> HS_Hour18_Value
        {
            get
            {
                return this._HS_Hour18_Value;
            }
            set
            {
                if ((this._HS_Hour18_Value != value))
                {
                    this._HS_Hour18_Value = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_HS_Hour19_Value", DbType = "Float")]
        public System.Nullable<double> HS_Hour19_Value
        {
            get
            {
                return this._HS_Hour19_Value;
            }
            set
            {
                if ((this._HS_Hour19_Value != value))
                {
                    this._HS_Hour19_Value = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_HS_Hour20_Value", DbType = "Float")]
        public System.Nullable<double> HS_Hour20_Value
        {
            get
            {
                return this._HS_Hour20_Value;
            }
            set
            {
                if ((this._HS_Hour20_Value != value))
                {
                    this._HS_Hour20_Value = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_HS_Hour21_Value", DbType = "Float")]
        public System.Nullable<double> HS_Hour21_Value
        {
            get
            {
                return this._HS_Hour21_Value;
            }
            set
            {
                if ((this._HS_Hour21_Value != value))
                {
                    this._HS_Hour21_Value = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_HS_Hour22_Value", DbType = "Float")]
        public System.Nullable<double> HS_Hour22_Value
        {
            get
            {
                return this._HS_Hour22_Value;
            }
            set
            {
                if ((this._HS_Hour22_Value != value))
                {
                    this._HS_Hour22_Value = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_HS_Hour23_Value", DbType = "Float")]
        public System.Nullable<double> HS_Hour23_Value
        {
            get
            {
                return this._HS_Hour23_Value;
            }
            set
            {
                if ((this._HS_Hour23_Value != value))
                {
                    this._HS_Hour23_Value = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_HS_Hour24_Value", DbType = "Float")]
        public System.Nullable<double> HS_Hour24_Value
        {
            get
            {
                return this._HS_Hour24_Value;
            }
            set
            {
                if ((this._HS_Hour24_Value != value))
                {
                    this._HS_Hour24_Value = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Total", DbType = "Float")]
        public System.Nullable<double> Total
        {
            get
            {
                return this._Total;
            }
            set
            {
                if ((this._Total != value))
                {
                    this._Total = value;
                }
            }
        }
    }

    public partial class rsp_GetHourlyStatisticsTypesResult
    {

        private int _HST_ID;

        private string _HST_Type;

        private string _HST_Desc;

        public rsp_GetHourlyStatisticsTypesResult()
        {
        }

        [Column(Storage = "_HST_ID", DbType = "Int NOT NULL")]
        public int HST_ID
        {
            get
            {
                return this._HST_ID;
            }
            set
            {
                if ((this._HST_ID != value))
                {
                    this._HST_ID = value;
                }
            }
        }

        [Column(Storage = "_HST_Type", DbType = "VarChar(50)")]
        public string HST_Type
        {
            get
            {
                return this._HST_Type;
            }
            set
            {
                if ((this._HST_Type != value))
                {
                    this._HST_Type = value;
                }
            }
        }

        [Column(Storage = "_HST_Desc", DbType = "VarChar(8000)")]
        public string HST_Desc
        {
            get
            {
                return this._HST_Desc;
            }
            set
            {
                if ((this._HST_Desc != value))
                {
                    this._HST_Desc = value;
                }
            }
        }
    }
    
    public partial class rsp_GetSiteInfoResult
    {

        private int _Site_ID;

        private string _Site_Name;

        public rsp_GetSiteInfoResult()
        {
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
    }

    public partial class rsp_GetHourlyFilterByInfoResult
    {
        private int _FilterById;

        private string _FilterByName;

        public rsp_GetHourlyFilterByInfoResult()
        {
        }

        [Column(Storage = "_FilterById", DbType = "Int NOT NULL")]
        public int FilterById
        {
            get
            {
                return this._FilterById;
            }
            set
            {
                if ((this._FilterById != value))
                {
                    this._FilterById = value;
                }
            }
        }

        [Column(Storage = "_FilterByName", DbType = "VarChar(50)")]
        public string FilterByName
        {
            get
            {
                return this._FilterByName;
            }
            set
            {
                if ((this._FilterByName != value))
                {
                    this._FilterByName = value;
                }
            }
        }
    }

    public partial class rsp_GetBarPositionsEnrolledOnGamingDayResult
    {

        private string _Bar_Position_Name;

        public rsp_GetBarPositionsEnrolledOnGamingDayResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Bar_Position_Name", DbType = "VarChar(50)")]
        public string Bar_Position_Name
        {
            get
            {
                return this._Bar_Position_Name;
            }
            set
            {
                if ((this._Bar_Position_Name != value))
                {
                    this._Bar_Position_Name = value;
                }
            }
        }
    }
}
