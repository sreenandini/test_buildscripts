using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Reflection;
using System.Text;

namespace BMC.EnterpriseDataAccess
{
    public partial class EnterpriseDataContext
    {
        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.usp_InsertOrUpdateShareSchedule")]
        public int AddOrUpdateShareSchedule([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Share_Schedule_ID", DbType = "Int")] System.Nullable<int> share_Schedule_ID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Share_Schedule_Name", DbType = "VarChar(50)")] string share_Schedule_Name, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Share_Schedule_Description", DbType = "VarChar(50)")] string share_Schedule_Description, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Share_Schedule_No_Bands", DbType = "Int")] System.Nullable<int> share_Schedule_No_Bands, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Share_Schedule_Bands_Name_Type", DbType = "VarChar(10)")] string share_Schedule_Bands_Name_Type, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Share_Schedule_IDOut", DbType = "Int")] ref System.Nullable<int> share_Schedule_IDOut)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), share_Schedule_ID, share_Schedule_Name, share_Schedule_Description, share_Schedule_No_Bands, share_Schedule_Bands_Name_Type, share_Schedule_IDOut);
            share_Schedule_IDOut = ((System.Nullable<int>)(result.GetParameterValue(5)));
            return ((int)(result.ReturnValue));
        }
        

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.rsp_GetShareBand")]
        public ISingleResult<rsp_GetShareBandResult> GetShareBandDetails([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Share_Schedule_ID", DbType = "Int")] System.Nullable<int> share_Schedule_ID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), share_Schedule_ID);
            return ((ISingleResult<rsp_GetShareBandResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.usp_InsertOrUpdateShareBand")]
        public int AddOrUpdateShareBand(
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Share_Schedule_Name", DbType = "VarChar(50)")] string share_Schedule_Name,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Share_Band_ID", DbType = "Int")] System.Nullable<int> share_Band_ID,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Share_Schedule_ID", DbType = "Int")] System.Nullable<int> share_Schedule_ID,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Share_Band_Name", DbType = "VarChar(50)")] string share_Band_Name,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Share_Band_Start_Date", DbType = "VarChar(30)")] string share_Band_Start_Date,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Share_Band_End_Date", DbType = "VarChar(30)")] string share_Band_End_Date,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Share_Band_Description", DbType = "VarChar(50)")] string share_Band_Description,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Share_Band_Supplier_Share", DbType = "Real")] System.Nullable<float> share_Band_Supplier_Share,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Share_Band_Site_Share", DbType = "Real")] System.Nullable<float> share_Band_Site_Share,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Share_Band_Company_Share", DbType = "Real")] System.Nullable<float> share_Band_Company_Share,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Share_Band_Sec_Company_Share", DbType = "Real")] System.Nullable<float> share_Band_Sec_Company_Share,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Share_Band_Future_Supplier_Share", DbType = "Real")] System.Nullable<float> share_Band_Future_Supplier_Share,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Share_Band_Future_Site_Share", DbType = "Real")] System.Nullable<float> share_Band_Future_Site_Share,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Share_Band_Future_Company_Share", DbType = "Real")] System.Nullable<float> share_Band_Future_Company_Share,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Share_Band_Future_Sec_Company_Share", DbType = "Real")] System.Nullable<float> share_Band_Future_Sec_Company_Share,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Share_Band_Future_Start_Date", DbType = "VarChar(30)")] string share_Band_Future_Start_Date,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Share_Band_Past_Supplier_Share", DbType = "Real")] System.Nullable<float> share_Band_Past_Supplier_Share,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Share_Band_Past_Site_Share", DbType = "Real")] System.Nullable<float> share_Band_Past_Site_Share,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Share_Band_Past_Company_Share", DbType = "Real")] System.Nullable<float> share_Band_Past_Company_Share,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Share_Band_Past_Sec_Company_Share", DbType = "Real")] System.Nullable<float> share_Band_Past_Sec_Company_Share,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Share_Band_Past_End_Date", DbType = "VarChar(30)")] string share_Band_Past_End_Date,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Share_Band_Supplier_Rent", DbType = "Real")] System.Nullable<float> share_Band_Supplier_Rent,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Share_Band_Future_Supplier_Rent", DbType = "Real")] System.Nullable<float> share_Band_Future_Supplier_Rent,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Share_Band_Past_Supplier_Rent", DbType = "Real")] System.Nullable<float> share_Band_Past_Supplier_Rent,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Share_Band_Supplier_Rent_Guaranteed", DbType = "Bit")] System.Nullable<bool> share_Band_Supplier_Rent_Guaranteed,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Share_Band_Future_Supplier_Rent_Guaranteed", DbType = "Bit")] System.Nullable<bool> share_Band_Future_Supplier_Rent_Guaranteed,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Share_Band_Past_Supplier_Rent_Guaranteed", DbType = "Bit")] System.Nullable<bool> share_Band_Past_Supplier_Rent_Guaranteed,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Share_Band_Supplier_Share_Guaranteed", DbType = "Bit")] System.Nullable<bool> share_Band_Supplier_Share_Guaranteed,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Share_Band_Future_Supplier_Share_Guaranteed", DbType = "Bit")] System.Nullable<bool> share_Band_Future_Supplier_Share_Guaranteed,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Share_Band_Past_Supplier_Share_Guaranteed", DbType = "Bit")] System.Nullable<bool> share_Band_Past_Supplier_Share_Guaranteed,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Share_Band_Company_Share_Guaranteed", DbType = "Bit")] System.Nullable<bool> share_Band_Company_Share_Guaranteed,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Share_Band_Future_Company_Share_Guaranteed", DbType = "Bit")] System.Nullable<bool> share_Band_Future_Company_Share_Guaranteed,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Share_Band_Past_Company_Share_Guaranteed", DbType = "Bit")] System.Nullable<bool> share_Band_Past_Company_Share_Guaranteed,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Share_Band_Site_Share_Guaranteed", DbType = "Bit")] System.Nullable<bool> share_Band_Site_Share_Guaranteed,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Share_Band_Future_Site_Share_Guaranteed", DbType = "Bit")] System.Nullable<bool> share_Band_Future_Site_Share_Guaranteed,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Share_Band_Past_Site_Share_Guaranteed", DbType = "Bit")] System.Nullable<bool> share_Band_Past_Site_Share_Guaranteed,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Share_Band_Sec_Company_Share_Guaranteed", DbType = "Bit")] System.Nullable<bool> share_Band_Sec_Company_Share_Guaranteed,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Share_Band_Future_Sec_Company_Share_Guaranteed", DbType = "Bit")] System.Nullable<bool> share_Band_Future_Sec_Company_Share_Guaranteed,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Share_Band_Past_Sec_Company_Share_Guaranteed", DbType = "Bit")] System.Nullable<bool> share_Band_Past_Sec_Company_Share_Guaranteed)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), share_Schedule_Name, share_Band_ID, share_Schedule_ID, share_Band_Name, share_Band_Start_Date, share_Band_End_Date, share_Band_Description, share_Band_Supplier_Share, share_Band_Site_Share, share_Band_Company_Share, share_Band_Sec_Company_Share, share_Band_Future_Supplier_Share, share_Band_Future_Site_Share, share_Band_Future_Company_Share, share_Band_Future_Sec_Company_Share, share_Band_Future_Start_Date, share_Band_Past_Supplier_Share, share_Band_Past_Site_Share, share_Band_Past_Company_Share, share_Band_Past_Sec_Company_Share, share_Band_Past_End_Date, share_Band_Supplier_Rent, share_Band_Future_Supplier_Rent, share_Band_Past_Supplier_Rent, share_Band_Supplier_Rent_Guaranteed, share_Band_Future_Supplier_Rent_Guaranteed, share_Band_Past_Supplier_Rent_Guaranteed, share_Band_Supplier_Share_Guaranteed, share_Band_Future_Supplier_Share_Guaranteed, share_Band_Past_Supplier_Share_Guaranteed, share_Band_Company_Share_Guaranteed, share_Band_Future_Company_Share_Guaranteed, share_Band_Past_Company_Share_Guaranteed, share_Band_Site_Share_Guaranteed, share_Band_Future_Site_Share_Guaranteed, share_Band_Past_Site_Share_Guaranteed, share_Band_Sec_Company_Share_Guaranteed, share_Band_Future_Sec_Company_Share_Guaranteed, share_Band_Past_Sec_Company_Share_Guaranteed);
            return ((int)(result.ReturnValue));
        }

               

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.rsp_GetMachineClassShareBand")]
        public ISingleResult<rsp_GetMachineClassShareBandResult> GetMachineClassShareBand([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Share_Schedule_ID", DbType = "Int")] System.Nullable<int> share_Schedule_ID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), share_Schedule_ID);
            return ((ISingleResult<rsp_GetMachineClassShareBandResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.usp_InsertOrUpdateMachineClassShareBand")]
        public int AddOrUpdateMachineClassShareBand([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Machine_Class_Share_Band", DbType = "Int")] System.Nullable<int> machine_Class_Share_Band, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Machine_Class_ID", DbType = "Int")] System.Nullable<int> machine_Class_ID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Share_Band_ID", DbType = "Int")] System.Nullable<int> share_Band_ID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Share_Band_ID_Future", DbType = "Int")] System.Nullable<int> share_Band_ID_Future, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Machine_Class_Share_Future_Date", DbType = "VarChar(30)")] string machine_Class_Share_Future_Date, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Share_Band_ID_Past", DbType = "Int")] System.Nullable<int> share_Band_ID_Past, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Machine_Class_Share_Past_Date", DbType = "VarChar(30)")] string machine_Class_Share_Past_Date)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), machine_Class_Share_Band, machine_Class_ID, share_Band_ID, share_Band_ID_Future, machine_Class_Share_Future_Date, share_Band_ID_Past, machine_Class_Share_Past_Date);
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.rsp_GetMachineClass")]
        public ISingleResult<rsp_GetMachineClassResult> GetMachineClass([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "SearchCriteria", DbType = "VarChar(50)")] string searchCriteria)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), searchCriteria);
            return ((ISingleResult<rsp_GetMachineClassResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.usp_DeleteMachineClassShareBand")]
        public int DeleteMachineClassShareBand([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Machine_Class_Share_Band", DbType = "Int")] System.Nullable<int> machine_Class_Share_Band)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), machine_Class_Share_Band);
            return ((int)(result.ReturnValue));
        }

       
        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.rsp_GetShareSchedule")]
        public ISingleResult<rsp_GetShareScheduleResult> GetShareScheduleDetails([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Share_Schedule_ID", DbType = "Int")] System.Nullable<int> share_Schedule_ID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), share_Schedule_ID);
            return ((ISingleResult<rsp_GetShareScheduleResult>)(result.ReturnValue));
        }

 	}

    public partial class rsp_GetShareScheduleResult
    {

        private int _Share_Schedule_ID;

        private string _Share_Schedule_Name;

        private string _Share_Schedule_Start_Date;

        private string _Share_Schedule_End_Date;

        private string _Share_Schedule_Description;

        private System.Nullable<int> _Share_Schedule_No_Bands;

        private string _Share_Schedule_Bands_Name_Type;

        private string _Share_Machine_Change_Date;

        public rsp_GetShareScheduleResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Share_Schedule_ID", DbType = "Int NOT NULL")]
        public int Share_Schedule_ID
        {
            get
            {
                return this._Share_Schedule_ID;
            }
            set
            {
                if ((this._Share_Schedule_ID != value))
                {
                    this._Share_Schedule_ID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Share_Schedule_Name", DbType = "VarChar(50)")]
        public string Share_Schedule_Name
        {
            get
            {
                return this._Share_Schedule_Name;
            }
            set
            {
                if ((this._Share_Schedule_Name != value))
                {
                    this._Share_Schedule_Name = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Share_Schedule_Start_Date", DbType = "VarChar(30)")]
        public string Share_Schedule_Start_Date
        {
            get
            {
                return this._Share_Schedule_Start_Date;
            }
            set
            {
                if ((this._Share_Schedule_Start_Date != value))
                {
                    this._Share_Schedule_Start_Date = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Share_Schedule_End_Date", DbType = "VarChar(30)")]
        public string Share_Schedule_End_Date
        {
            get
            {
                return this._Share_Schedule_End_Date;
            }
            set
            {
                if ((this._Share_Schedule_End_Date != value))
                {
                    this._Share_Schedule_End_Date = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Share_Schedule_Description", DbType = "VarChar(50)")]
        public string Share_Schedule_Description
        {
            get
            {
                return this._Share_Schedule_Description;
            }
            set
            {
                if ((this._Share_Schedule_Description != value))
                {
                    this._Share_Schedule_Description = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Share_Schedule_No_Bands", DbType = "Int")]
        public System.Nullable<int> Share_Schedule_No_Bands
        {
            get
            {
                return this._Share_Schedule_No_Bands;
            }
            set
            {
                if ((this._Share_Schedule_No_Bands != value))
                {
                    this._Share_Schedule_No_Bands = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Share_Schedule_Bands_Name_Type", DbType = "VarChar(10)")]
        public string Share_Schedule_Bands_Name_Type
        {
            get
            {
                return this._Share_Schedule_Bands_Name_Type;
            }
            set
            {
                if ((this._Share_Schedule_Bands_Name_Type != value))
                {
                    this._Share_Schedule_Bands_Name_Type = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Share_Machine_Change_Date", DbType = "VarChar(30)")]
        public string Share_Machine_Change_Date
        {
            get
            {
                return this._Share_Machine_Change_Date;
            }
            set
            {
                if ((this._Share_Machine_Change_Date != value))
                {
                    this._Share_Machine_Change_Date = value;
                }
            }
        }
    }
    public partial class rsp_GetShareBandResult
    {

        private int _Share_Band_ID;

        private System.Nullable<int> _Share_Schedule_ID;

        private string _Share_Band_Name;

        private string _Share_Band_Start_Date;

        private string _Share_Band_End_Date;

        private string _Share_Band_Description;

        private System.Nullable<float> _Share_Band_Supplier_Share;

        private System.Nullable<float> _Share_Band_Site_Share;

        private System.Nullable<float> _Share_Band_Company_Share;

        private System.Nullable<float> _Share_Band_Sec_Company_Share;

        private System.Nullable<float> _Share_Band_Future_Supplier_Share;

        private System.Nullable<float> _Share_Band_Future_Site_Share;

        private System.Nullable<float> _Share_Band_Future_Company_Share;

        private System.Nullable<float> _Share_Band_Future_Sec_Company_Share;

        private string _Share_Band_Future_Start_Date;

        private System.Nullable<float> _Share_Band_Past_Supplier_Share;

        private System.Nullable<float> _Share_Band_Past_Site_Share;

        private System.Nullable<float> _Share_Band_Past_Company_Share;

        private System.Nullable<float> _Share_Band_Past_Sec_Company_Share;

        private string _Share_Band_Past_End_Date;

        private System.Nullable<float> _Share_Band_Supplier_Rent;

        private System.Nullable<float> _Share_Band_Future_Supplier_Rent;

        private System.Nullable<float> _Share_Band_Past_Supplier_Rent;

        private System.Nullable<bool> _Share_Band_Supplier_Rent_Guaranteed;

        private System.Nullable<bool> _Share_Band_Future_Supplier_Rent_Guaranteed;

        private System.Nullable<bool> _Share_Band_Past_Supplier_Rent_Guaranteed;

        private System.Nullable<bool> _Share_Band_Supplier_Share_Guaranteed;

        private System.Nullable<bool> _Share_Band_Future_Supplier_Share_Guaranteed;

        private System.Nullable<bool> _Share_Band_Past_Supplier_Share_Guaranteed;

        private System.Nullable<bool> _Share_Band_Company_Share_Guaranteed;

        private System.Nullable<bool> _Share_Band_Future_Company_Share_Guaranteed;

        private System.Nullable<bool> _Share_Band_Past_Company_Share_Guaranteed;

        private System.Nullable<bool> _Share_Band_Site_Share_Guaranteed;

        private System.Nullable<bool> _Share_Band_Future_Site_Share_Guaranteed;

        private System.Nullable<bool> _Share_Band_Past_Site_Share_Guaranteed;

        private System.Nullable<bool> _Share_Band_Sec_Company_Share_Guaranteed;

        private System.Nullable<bool> _Share_Band_Future_Sec_Company_Share_Guaranteed;

        private System.Nullable<bool> _Share_Band_Past_Sec_Company_Share_Guaranteed;

        public rsp_GetShareBandResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Share_Band_ID", DbType = "Int NOT NULL")]
        public int Share_Band_ID
        {
            get
            {
                return this._Share_Band_ID;
            }
            set
            {
                if ((this._Share_Band_ID != value))
                {
                    this._Share_Band_ID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Share_Schedule_ID", DbType = "Int")]
        public System.Nullable<int> Share_Schedule_ID
        {
            get
            {
                return this._Share_Schedule_ID;
            }
            set
            {
                if ((this._Share_Schedule_ID != value))
                {
                    this._Share_Schedule_ID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Share_Band_Name", DbType = "VarChar(50)")]
        public string Share_Band_Name
        {
            get
            {
                return this._Share_Band_Name;
            }
            set
            {
                if ((this._Share_Band_Name != value))
                {
                    this._Share_Band_Name = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Share_Band_Start_Date", DbType = "VarChar(30)")]
        public string Share_Band_Start_Date
        {
            get
            {
                return this._Share_Band_Start_Date;
            }
            set
            {
                if ((this._Share_Band_Start_Date != value))
                {
                    this._Share_Band_Start_Date = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Share_Band_End_Date", DbType = "VarChar(30)")]
        public string Share_Band_End_Date
        {
            get
            {
                return this._Share_Band_End_Date;
            }
            set
            {
                if ((this._Share_Band_End_Date != value))
                {
                    this._Share_Band_End_Date = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Share_Band_Description", DbType = "VarChar(50)")]
        public string Share_Band_Description
        {
            get
            {
                return this._Share_Band_Description;
            }
            set
            {
                if ((this._Share_Band_Description != value))
                {
                    this._Share_Band_Description = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Share_Band_Supplier_Share", DbType = "Real")]
        public System.Nullable<float> Share_Band_Supplier_Share
        {
            get
            {
                return this._Share_Band_Supplier_Share;
            }
            set
            {
                if ((this._Share_Band_Supplier_Share != value))
                {
                    this._Share_Band_Supplier_Share = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Share_Band_Site_Share", DbType = "Real")]
        public System.Nullable<float> Share_Band_Site_Share
        {
            get
            {
                return this._Share_Band_Site_Share;
            }
            set
            {
                if ((this._Share_Band_Site_Share != value))
                {
                    this._Share_Band_Site_Share = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Share_Band_Company_Share", DbType = "Real")]
        public System.Nullable<float> Share_Band_Company_Share
        {
            get
            {
                return this._Share_Band_Company_Share;
            }
            set
            {
                if ((this._Share_Band_Company_Share != value))
                {
                    this._Share_Band_Company_Share = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Share_Band_Sec_Company_Share", DbType = "Real")]
        public System.Nullable<float> Share_Band_Sec_Company_Share
        {
            get
            {
                return this._Share_Band_Sec_Company_Share;
            }
            set
            {
                if ((this._Share_Band_Sec_Company_Share != value))
                {
                    this._Share_Band_Sec_Company_Share = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Share_Band_Future_Supplier_Share", DbType = "Real")]
        public System.Nullable<float> Share_Band_Future_Supplier_Share
        {
            get
            {
                return this._Share_Band_Future_Supplier_Share;
            }
            set
            {
                if ((this._Share_Band_Future_Supplier_Share != value))
                {
                    this._Share_Band_Future_Supplier_Share = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Share_Band_Future_Site_Share", DbType = "Real")]
        public System.Nullable<float> Share_Band_Future_Site_Share
        {
            get
            {
                return this._Share_Band_Future_Site_Share;
            }
            set
            {
                if ((this._Share_Band_Future_Site_Share != value))
                {
                    this._Share_Band_Future_Site_Share = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Share_Band_Future_Company_Share", DbType = "Real")]
        public System.Nullable<float> Share_Band_Future_Company_Share
        {
            get
            {
                return this._Share_Band_Future_Company_Share;
            }
            set
            {
                if ((this._Share_Band_Future_Company_Share != value))
                {
                    this._Share_Band_Future_Company_Share = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Share_Band_Future_Sec_Company_Share", DbType = "Real")]
        public System.Nullable<float> Share_Band_Future_Sec_Company_Share
        {
            get
            {
                return this._Share_Band_Future_Sec_Company_Share;
            }
            set
            {
                if ((this._Share_Band_Future_Sec_Company_Share != value))
                {
                    this._Share_Band_Future_Sec_Company_Share = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Share_Band_Future_Start_Date", DbType = "VarChar(30)")]
        public string Share_Band_Future_Start_Date
        {
            get
            {
                return this._Share_Band_Future_Start_Date;
            }
            set
            {
                if ((this._Share_Band_Future_Start_Date != value))
                {
                    this._Share_Band_Future_Start_Date = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Share_Band_Past_Supplier_Share", DbType = "Real")]
        public System.Nullable<float> Share_Band_Past_Supplier_Share
        {
            get
            {
                return this._Share_Band_Past_Supplier_Share;
            }
            set
            {
                if ((this._Share_Band_Past_Supplier_Share != value))
                {
                    this._Share_Band_Past_Supplier_Share = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Share_Band_Past_Site_Share", DbType = "Real")]
        public System.Nullable<float> Share_Band_Past_Site_Share
        {
            get
            {
                return this._Share_Band_Past_Site_Share;
            }
            set
            {
                if ((this._Share_Band_Past_Site_Share != value))
                {
                    this._Share_Band_Past_Site_Share = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Share_Band_Past_Company_Share", DbType = "Real")]
        public System.Nullable<float> Share_Band_Past_Company_Share
        {
            get
            {
                return this._Share_Band_Past_Company_Share;
            }
            set
            {
                if ((this._Share_Band_Past_Company_Share != value))
                {
                    this._Share_Band_Past_Company_Share = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Share_Band_Past_Sec_Company_Share", DbType = "Real")]
        public System.Nullable<float> Share_Band_Past_Sec_Company_Share
        {
            get
            {
                return this._Share_Band_Past_Sec_Company_Share;
            }
            set
            {
                if ((this._Share_Band_Past_Sec_Company_Share != value))
                {
                    this._Share_Band_Past_Sec_Company_Share = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Share_Band_Past_End_Date", DbType = "VarChar(30)")]
        public string Share_Band_Past_End_Date
        {
            get
            {
                return this._Share_Band_Past_End_Date;
            }
            set
            {
                if ((this._Share_Band_Past_End_Date != value))
                {
                    this._Share_Band_Past_End_Date = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Share_Band_Supplier_Rent", DbType = "Real")]
        public System.Nullable<float> Share_Band_Supplier_Rent
        {
            get
            {
                return this._Share_Band_Supplier_Rent;
            }
            set
            {
                if ((this._Share_Band_Supplier_Rent != value))
                {
                    this._Share_Band_Supplier_Rent = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Share_Band_Future_Supplier_Rent", DbType = "Real")]
        public System.Nullable<float> Share_Band_Future_Supplier_Rent
        {
            get
            {
                return this._Share_Band_Future_Supplier_Rent;
            }
            set
            {
                if ((this._Share_Band_Future_Supplier_Rent != value))
                {
                    this._Share_Band_Future_Supplier_Rent = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Share_Band_Past_Supplier_Rent", DbType = "Real")]
        public System.Nullable<float> Share_Band_Past_Supplier_Rent
        {
            get
            {
                return this._Share_Band_Past_Supplier_Rent;
            }
            set
            {
                if ((this._Share_Band_Past_Supplier_Rent != value))
                {
                    this._Share_Band_Past_Supplier_Rent = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Share_Band_Supplier_Rent_Guaranteed", DbType = "Bit")]
        public System.Nullable<bool> Share_Band_Supplier_Rent_Guaranteed
        {
            get
            {
                return this._Share_Band_Supplier_Rent_Guaranteed;
            }
            set
            {
                if ((this._Share_Band_Supplier_Rent_Guaranteed != value))
                {
                    this._Share_Band_Supplier_Rent_Guaranteed = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Share_Band_Future_Supplier_Rent_Guaranteed", DbType = "Bit")]
        public System.Nullable<bool> Share_Band_Future_Supplier_Rent_Guaranteed
        {
            get
            {
                return this._Share_Band_Future_Supplier_Rent_Guaranteed;
            }
            set
            {
                if ((this._Share_Band_Future_Supplier_Rent_Guaranteed != value))
                {
                    this._Share_Band_Future_Supplier_Rent_Guaranteed = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Share_Band_Past_Supplier_Rent_Guaranteed", DbType = "Bit")]
        public System.Nullable<bool> Share_Band_Past_Supplier_Rent_Guaranteed
        {
            get
            {
                return this._Share_Band_Past_Supplier_Rent_Guaranteed;
            }
            set
            {
                if ((this._Share_Band_Past_Supplier_Rent_Guaranteed != value))
                {
                    this._Share_Band_Past_Supplier_Rent_Guaranteed = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Share_Band_Supplier_Share_Guaranteed", DbType = "Bit")]
        public System.Nullable<bool> Share_Band_Supplier_Share_Guaranteed
        {
            get
            {
                return this._Share_Band_Supplier_Share_Guaranteed;
            }
            set
            {
                if ((this._Share_Band_Supplier_Share_Guaranteed != value))
                {
                    this._Share_Band_Supplier_Share_Guaranteed = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Share_Band_Future_Supplier_Share_Guaranteed", DbType = "Bit")]
        public System.Nullable<bool> Share_Band_Future_Supplier_Share_Guaranteed
        {
            get
            {
                return this._Share_Band_Future_Supplier_Share_Guaranteed;
            }
            set
            {
                if ((this._Share_Band_Future_Supplier_Share_Guaranteed != value))
                {
                    this._Share_Band_Future_Supplier_Share_Guaranteed = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Share_Band_Past_Supplier_Share_Guaranteed", DbType = "Bit")]
        public System.Nullable<bool> Share_Band_Past_Supplier_Share_Guaranteed
        {
            get
            {
                return this._Share_Band_Past_Supplier_Share_Guaranteed;
            }
            set
            {
                if ((this._Share_Band_Past_Supplier_Share_Guaranteed != value))
                {
                    this._Share_Band_Past_Supplier_Share_Guaranteed = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Share_Band_Company_Share_Guaranteed", DbType = "Bit")]
        public System.Nullable<bool> Share_Band_Company_Share_Guaranteed
        {
            get
            {
                return this._Share_Band_Company_Share_Guaranteed;
            }
            set
            {
                if ((this._Share_Band_Company_Share_Guaranteed != value))
                {
                    this._Share_Band_Company_Share_Guaranteed = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Share_Band_Future_Company_Share_Guaranteed", DbType = "Bit")]
        public System.Nullable<bool> Share_Band_Future_Company_Share_Guaranteed
        {
            get
            {
                return this._Share_Band_Future_Company_Share_Guaranteed;
            }
            set
            {
                if ((this._Share_Band_Future_Company_Share_Guaranteed != value))
                {
                    this._Share_Band_Future_Company_Share_Guaranteed = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Share_Band_Past_Company_Share_Guaranteed", DbType = "Bit")]
        public System.Nullable<bool> Share_Band_Past_Company_Share_Guaranteed
        {
            get
            {
                return this._Share_Band_Past_Company_Share_Guaranteed;
            }
            set
            {
                if ((this._Share_Band_Past_Company_Share_Guaranteed != value))
                {
                    this._Share_Band_Past_Company_Share_Guaranteed = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Share_Band_Site_Share_Guaranteed", DbType = "Bit")]
        public System.Nullable<bool> Share_Band_Site_Share_Guaranteed
        {
            get
            {
                return this._Share_Band_Site_Share_Guaranteed;
            }
            set
            {
                if ((this._Share_Band_Site_Share_Guaranteed != value))
                {
                    this._Share_Band_Site_Share_Guaranteed = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Share_Band_Future_Site_Share_Guaranteed", DbType = "Bit")]
        public System.Nullable<bool> Share_Band_Future_Site_Share_Guaranteed
        {
            get
            {
                return this._Share_Band_Future_Site_Share_Guaranteed;
            }
            set
            {
                if ((this._Share_Band_Future_Site_Share_Guaranteed != value))
                {
                    this._Share_Band_Future_Site_Share_Guaranteed = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Share_Band_Past_Site_Share_Guaranteed", DbType = "Bit")]
        public System.Nullable<bool> Share_Band_Past_Site_Share_Guaranteed
        {
            get
            {
                return this._Share_Band_Past_Site_Share_Guaranteed;
            }
            set
            {
                if ((this._Share_Band_Past_Site_Share_Guaranteed != value))
                {
                    this._Share_Band_Past_Site_Share_Guaranteed = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Share_Band_Sec_Company_Share_Guaranteed", DbType = "Bit")]
        public System.Nullable<bool> Share_Band_Sec_Company_Share_Guaranteed
        {
            get
            {
                return this._Share_Band_Sec_Company_Share_Guaranteed;
            }
            set
            {
                if ((this._Share_Band_Sec_Company_Share_Guaranteed != value))
                {
                    this._Share_Band_Sec_Company_Share_Guaranteed = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Share_Band_Future_Sec_Company_Share_Guaranteed", DbType = "Bit")]
        public System.Nullable<bool> Share_Band_Future_Sec_Company_Share_Guaranteed
        {
            get
            {
                return this._Share_Band_Future_Sec_Company_Share_Guaranteed;
            }
            set
            {
                if ((this._Share_Band_Future_Sec_Company_Share_Guaranteed != value))
                {
                    this._Share_Band_Future_Sec_Company_Share_Guaranteed = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Share_Band_Past_Sec_Company_Share_Guaranteed", DbType = "Bit")]
        public System.Nullable<bool> Share_Band_Past_Sec_Company_Share_Guaranteed
        {
            get
            {
                return this._Share_Band_Past_Sec_Company_Share_Guaranteed;
            }
            set
            {
                if ((this._Share_Band_Past_Sec_Company_Share_Guaranteed != value))
                {
                    this._Share_Band_Past_Sec_Company_Share_Guaranteed = value;
                }
            }
        }
    }

    public partial class rsp_GetMachineClassShareBandResult
    {

        private System.Nullable<int> _Machine_Class_ID;

        private string _Machine_Name;

        private string _Machine_BACTA_Code;

        private int _Machine_Class_Share_Band;

        private string _PastBandName;

        private System.Nullable<int> _Share_Band_ID_Past;

        private string _BandName;

        private System.Nullable<int> _Share_Band_ID;

        private string _FutureBandName;

        private System.Nullable<int> _Share_Band_ID_Future;

        private string _Machine_Class_Share_Past_Date;

        private string _Machine_Class_Share_Future_Date;

        public rsp_GetMachineClassShareBandResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Machine_Class_ID", DbType = "Int")]
        public System.Nullable<int> Machine_Class_ID
        {
            get
            {
                return this._Machine_Class_ID;
            }
            set
            {
                if ((this._Machine_Class_ID != value))
                {
                    this._Machine_Class_ID = value;
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Machine_BACTA_Code", DbType = "VarChar(50)")]
        public string Machine_BACTA_Code
        {
            get
            {
                return this._Machine_BACTA_Code;
            }
            set
            {
                if ((this._Machine_BACTA_Code != value))
                {
                    this._Machine_BACTA_Code = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Machine_Class_Share_Band", DbType = "Int NOT NULL")]
        public int Machine_Class_Share_Band
        {
            get
            {
                return this._Machine_Class_Share_Band;
            }
            set
            {
                if ((this._Machine_Class_Share_Band != value))
                {
                    this._Machine_Class_Share_Band = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_PastBandName", DbType = "VarChar(50)")]
        public string PastBandName
        {
            get
            {
                return this._PastBandName;
            }
            set
            {
                if ((this._PastBandName != value))
                {
                    this._PastBandName = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Share_Band_ID_Past", DbType = "Int")]
        public System.Nullable<int> Share_Band_ID_Past
        {
            get
            {
                return this._Share_Band_ID_Past;
            }
            set
            {
                if ((this._Share_Band_ID_Past != value))
                {
                    this._Share_Band_ID_Past = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_BandName", DbType = "VarChar(50)")]
        public string BandName
        {
            get
            {
                return this._BandName;
            }
            set
            {
                if ((this._BandName != value))
                {
                    this._BandName = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Share_Band_ID", DbType = "Int")]
        public System.Nullable<int> Share_Band_ID
        {
            get
            {
                return this._Share_Band_ID;
            }
            set
            {
                if ((this._Share_Band_ID != value))
                {
                    this._Share_Band_ID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_FutureBandName", DbType = "VarChar(50)")]
        public string FutureBandName
        {
            get
            {
                return this._FutureBandName;
            }
            set
            {
                if ((this._FutureBandName != value))
                {
                    this._FutureBandName = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Share_Band_ID_Future", DbType = "Int")]
        public System.Nullable<int> Share_Band_ID_Future
        {
            get
            {
                return this._Share_Band_ID_Future;
            }
            set
            {
                if ((this._Share_Band_ID_Future != value))
                {
                    this._Share_Band_ID_Future = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Machine_Class_Share_Past_Date", DbType = "VarChar(30)")]
        public string Machine_Class_Share_Past_Date
        {
            get
            {
                return this._Machine_Class_Share_Past_Date;
            }
            set
            {
                if ((this._Machine_Class_Share_Past_Date != value))
                {
                    this._Machine_Class_Share_Past_Date = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Machine_Class_Share_Future_Date", DbType = "VarChar(30)")]
        public string Machine_Class_Share_Future_Date
        {
            get
            {
                return this._Machine_Class_Share_Future_Date;
            }
            set
            {
                if ((this._Machine_Class_Share_Future_Date != value))
                {
                    this._Machine_Class_Share_Future_Date = value;
                }
            }
        }
    }

    public partial class rsp_GetMachineClassResult
    {

        private string _Machine_Type_Code;

        private string _Machine_Name;

        private string _Machine_BACTA_Code;

        private int _Machine_Class_ID;

        public rsp_GetMachineClassResult()
        {
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Machine_BACTA_Code", DbType = "VarChar(50)")]
        public string Machine_BACTA_Code
        {
            get
            {
                return this._Machine_BACTA_Code;
            }
            set
            {
                if ((this._Machine_BACTA_Code != value))
                {
                    this._Machine_BACTA_Code = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Machine_Class_ID", DbType = "Int NOT NULL")]
        public int Machine_Class_ID
        {
            get
            {
                return this._Machine_Class_ID;
            }
            set
            {
                if ((this._Machine_Class_ID != value))
                {
                    this._Machine_Class_ID = value;
                }
            }
        }
    }
    
}
