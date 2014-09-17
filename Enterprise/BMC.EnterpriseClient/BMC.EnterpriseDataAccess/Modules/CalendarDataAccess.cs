using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;
using System.Data.Linq;
using System.Reflection;
using BMC.DataAccess;
using BMC.EnterpriseDataAccess;
using BMC.Common.Utilities;
using BMC.Common.ExceptionManagement;
using System.Data.SqlClient;
using System.Data;

namespace BMC.EnterpriseDataAccess
{

    // DO NOT USE SP NAMES AS FUNCTON NAMES
    public partial class EnterpriseDataContext
    {
        [Function(Name = "dbo.rsp_ISCalendarNameExists")]
        public int rsp_ISCalendarNameExists([Parameter(Name = "CalendarName", DbType = "VarChar(50)")] string calendarName, [Parameter(Name = "CalendarID", DbType = "Int")] System.Nullable<int> calendarID, [Parameter(Name = "NameCount", DbType = "Int")] ref System.Nullable<int> nameCount)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), calendarName, calendarID, nameCount);
            nameCount = ((System.Nullable<int>)(result.GetParameterValue(2)));
            return ((int)(result.ReturnValue));
        }
        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.rsp_CheckIncompleteCalendar")]
        public int CheckIncompleteCalendar([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Calendar_ID", DbType = "Int")] System.Nullable<int> calendar_ID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "RetVal", DbType = "Bit")] ref System.Nullable<bool> retVal)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), calendar_ID, retVal);
            retVal = ((System.Nullable<bool>)(result.GetParameterValue(1)));
            return ((int)(result.ReturnValue));
        }
        [Function(Name = "dbo.rsp_GetLstCalendar")]
        public ISingleResult<rsp_GetLstCalendarResult> rsp_GetLstCalendar()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<rsp_GetLstCalendarResult>)(result.ReturnValue));
        }
        [Function(Name = "dbo.rsp_GetCalendarList")]
        public ISingleResult<rsp_GetCalendarListResult> rsp_GetCalendarList()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<rsp_GetCalendarListResult>)(result.ReturnValue));
        }
        [Function(Name = "dbo.rsp_GetLstCalendarPeriod")]
        public ISingleResult<rsp_GetLstCalendarPeriodResult> rsp_GetLstCalendarPeriod([Parameter(Name = "Calendar_ID", DbType = "Int")] System.Nullable<int> calendar_ID, [Parameter(Name = "Calendar_Period_Number", DbType = "Int")] System.Nullable<int> calendar_Period_Number, [Parameter(Name = "Calendar_Period_ID", DbType = "Int")] System.Nullable<int> calendar_Period_ID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), calendar_ID, calendar_Period_Number, calendar_Period_ID);
            return ((ISingleResult<rsp_GetLstCalendarPeriodResult>)(result.ReturnValue));
        }
        [Function(Name = "dbo.usp_InsertNewCalendar")]
        public int usp_InsertNewCalendar([Parameter(Name = "CalendarDescription", DbType = "VarChar(50)")] string calendarDescription, [Parameter(Name = "CalendarYearStartDate", DbType = "VarChar(30)")] string calendarYearStartDate, [Parameter(Name = "CalendarYearEndDate", DbType = "VarChar(30)")] string calendarYearEndDate)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), calendarDescription, calendarYearStartDate, calendarYearEndDate);
            return ((int)(result.ReturnValue));
        }
        [Function(Name = "dbo.rsp_CheckCalendarDurationExists")]
        public int CheckCalendarDurationExists([Parameter(Name = "CType", DbType = "VarChar(30)")] string cType, [Parameter(Name = "Start_Date", DbType = "VarChar(30)")] string start_Date, [Parameter(Name = "End_Date", DbType = "VarChar(30)")] string end_Date, [Parameter(Name = "Retval", DbType = "Bit")] ref System.Nullable<bool> retval)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), cType, start_Date, end_Date, retval);
            retval = ((System.Nullable<bool>)(result.GetParameterValue(3)));
            return ((int)(result.ReturnValue));
        }
        [Function(Name = "dbo.rsp_GetCalPeriod")]
        public ISingleResult<rsp_GetCalPeriodResult> rsp_GetCalPeriod([Parameter(Name = "Calendar_Period_ID", DbType = "Int")] System.Nullable<int> calendar_Period_ID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), calendar_Period_ID);
            return ((ISingleResult<rsp_GetCalPeriodResult>)(result.ReturnValue));
        }
        [Function(Name = "dbo.rsp_CancelPeriodCalendar")]
        public ISingleResult<rsp_CancelPeriodCalendarResult> rsp_CancelPeriodCalendar([Parameter(Name = "CalendarID", DbType = "Int")] System.Nullable<int> calendarID, [Parameter(Name = "CalendarPeriodNumber", DbType = "Int")] System.Nullable<int> calendarPeriodNumber)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), calendarID, calendarPeriodNumber);
            return ((ISingleResult<rsp_CancelPeriodCalendarResult>)(result.ReturnValue));
        }
        [Function(Name = "dbo.rsp_GetCalendar")]
        public ISingleResult<rsp_GetCalendarResult> rsp_GetCalendar([Parameter(Name = "CalendarID", DbType = "Int")] System.Nullable<int> calendarID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), calendarID);
            return ((ISingleResult<rsp_GetCalendarResult>)(result.ReturnValue));
        }
        [Function(Name = "dbo.rsp_GetCalendarPeriod")]
        public ISingleResult<rsp_GetCalendarPeriodResult> rsp_GetCalendarPeriod([Parameter(Name = "CalendarID", DbType = "Int")] System.Nullable<int> calendarID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), calendarID);
            return ((ISingleResult<rsp_GetCalendarPeriodResult>)(result.ReturnValue));
        }
        [Function(Name = "dbo.rsp_GetCalendarWeek")]
        public ISingleResult<rsp_GetCalendarWeekResult> rsp_GetCalendarWeek([Parameter(Name = "CalendarID", DbType = "Int")] System.Nullable<int> calendarID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), calendarID);
            return ((ISingleResult<rsp_GetCalendarWeekResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetOperatorCal")]
        public ISingleResult<rsp_GetOperatorCalResult> rsp_GetOperatorCal([Parameter(Name = "Operator_ID", DbType = "Int")] System.Nullable<int> operator_ID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), operator_ID);
            return ((ISingleResult<rsp_GetOperatorCalResult>)(result.ReturnValue));
        }
        [Function(Name = "dbo.usp_UpdateCalendar")]
        public int usp_UpdateCalendar([Parameter(Name = "Calendar_ID", DbType = "Int")] System.Nullable<int> calendar_ID, [Parameter(Name = "CalendarDescription", DbType = "VarChar(50)")] string calendarDescription, [Parameter(Name = "Calendar_Year_Start_Date", DbType = "VarChar(30)")] string calendar_Year_Start_Date, [Parameter(Name = "Calendar_Year_End_Date", DbType = "VarChar(30)")] string calendar_Year_End_Date)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), calendar_ID, calendarDescription, calendar_Year_Start_Date, calendar_Year_End_Date);
            return ((int)(result.ReturnValue));
        }
        [Function(Name = "dbo.usp_CalendarClose")]
        public int usp_CalendarClose()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((int)(result.ReturnValue));
        }
        [Function(Name = "dbo.usp_InsertNewCalendarPeriod")]
        public int usp_InsertNewCalendarPeriod([Parameter(Name = "Calendar_Period_Number", DbType = "Int")] System.Nullable<int> calendar_Period_Number, [Parameter(Name = "Calendar_Period_Start_Date", DbType = "VarChar(30)")] string calendar_Period_Start_Date, [Parameter(Name = "Calendar_Period_End_Date", DbType = "VarChar(30)")] string calendar_Period_End_Date, [Parameter(Name = "CalendarID", DbType = "Int")] System.Nullable<int> calendarID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), calendar_Period_Number, calendar_Period_Start_Date, calendar_Period_End_Date, calendarID);
            return ((int)(result.ReturnValue));
        }
        [Function(Name = "dbo.usp_InsertNewCalendarWeek")]
        public int usp_InsertNewCalendarWeek([Parameter(Name = "Calendar_Week_Number", DbType = "Int")] System.Nullable<int> calendar_Week_Number, [Parameter(Name = "Calendar_Week_Start_Date", DbType = "VarChar(30)")] string calendar_Week_Start_Date, [Parameter(Name = "Calendar_Week_End_Date", DbType = "VarChar(30)")] string calendar_Week_End_Date, [Parameter(Name = "CalendarID", DbType = "Int")] System.Nullable<int> calendarID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), calendar_Week_Number, calendar_Week_Start_Date, calendar_Week_End_Date, calendarID);
            return ((int)(result.ReturnValue));
        }
        [Function(Name = "dbo.rsp_CancelWeekCalendar")]
        public ISingleResult<rsp_CancelWeekCalendarResult> rsp_CancelWeekCalendar([Parameter(Name = "CalendarID", DbType = "Int")] System.Nullable<int> calendarID, [Parameter(Name = "CalendarWeekNumber", DbType = "Int")] System.Nullable<int> calendarWeekNumber)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), calendarID, calendarWeekNumber);
            return ((ISingleResult<rsp_CancelWeekCalendarResult>)(result.ReturnValue));
        }
        [Function(Name = "dbo.rsp_GetSubCompanyCalender")]
        public int rsp_GetSubCompanyCalender([Parameter(Name = "CalendarId", DbType = "Int")] System.Nullable<int> calendarId, [Parameter(Name = "SubCompanyActive", DbType = "Int")] ref System.Nullable<int> subCompanyActive)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), calendarId, subCompanyActive);
            subCompanyActive = ((System.Nullable<int>)(result.GetParameterValue(1)));
            return ((int)(result.ReturnValue));
        }
        [Function(Name = "dbo.rsp_GetSubCompanyCalendarActive")]
        public ISingleResult<rsp_GetSubCompanyCalendarActiveResult> rsp_GetSubCompanyCalendarActive([Parameter(Name = "SubCompanyId", DbType = "Int")] System.Nullable<int> subCompanyId)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), subCompanyId);
            return ((ISingleResult<rsp_GetSubCompanyCalendarActiveResult>)(result.ReturnValue));
        }
        [Function(Name = "dbo.rsp_GetOperatorCalendarActive")]
        public ISingleResult<rsp_GetOperatorCalendarActiveResult> rsp_GetOperatorCalendarActive([Parameter(Name = "OperatorId", DbType = "Int")] System.Nullable<int> operatorId)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), operatorId);
            return ((ISingleResult<rsp_GetOperatorCalendarActiveResult>)(result.ReturnValue));
        }
        [Function(Name = "dbo.usp_UpdateWeekCalendar")]
        public int usp_UpdateWeekCalendar([Parameter(Name = "CalendarWeekNumber", DbType = "Int")] System.Nullable<int> calendarWeekNumber, [Parameter(Name = "CalendarWeekStartDate", DbType = "VarChar(30)")] string calendarWeekStartDate, [Parameter(Name = "CalendarWeekEndDate", DbType = "VarChar(30)")] string calendarWeekEndDate, [Parameter(Name = "CalendarId", DbType = "Int")] System.Nullable<int> calendarId)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), calendarWeekNumber, calendarWeekStartDate, calendarWeekEndDate, calendarId);
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.usp_UpdateCalendarPeriod")]
        public int usp_UpdateCalendarPeriod([Parameter(Name = "CalendarPeriodNumber", DbType = "Int")] System.Nullable<int> calendarPeriodNumber, [Parameter(Name = "CalendarPeriodStartDate", DbType = "VarChar(30)")] string calendarPeriodStartDate, [Parameter(Name = "CalendarPeriodEndDate", DbType = "VarChar(30)")] string calendarPeriodEndDate, [Parameter(Name = "CalendarId", DbType = "Int")] System.Nullable<int> calendarId)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), calendarPeriodNumber, calendarPeriodStartDate, calendarPeriodEndDate, calendarId);
            return ((int)(result.ReturnValue));
        }
        [Function(Name = "dbo.rsp_GetOperatorCalendar")]
        public ISingleResult<rsp_GetOperatorCalendarResult> rsp_GetOperatorCalendar([Parameter(Name = "Operator_ID", DbType = "Int")] System.Nullable<int> operator_ID, [Parameter(Name = "Calendar_ID", DbType = "Int")] System.Nullable<int> calendar_ID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), operator_ID, calendar_ID);
            return ((ISingleResult<rsp_GetOperatorCalendarResult>)(result.ReturnValue));
        }
        [Function(Name = "dbo.rsp_GetOperatorByActive")]
        public ISingleResult<rsp_GetOperatorByActiveResult> rsp_GetOperatorByActive([Parameter(Name = "Operator_Id", DbType = "Int")] System.Nullable<int> operator_Id)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), operator_Id);
            return ((ISingleResult<rsp_GetOperatorByActiveResult>)(result.ReturnValue));
        }
        [Function(Name = "dbo.Usp_UpdateOperator")]
        public int Usp_UpdateOperator([Parameter(Name = "Operator_Id", DbType = "Int")] System.Nullable<int> operator_Id)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), operator_Id);
            return ((int)(result.ReturnValue));
        }
        [Function(Name = "dbo.Usp_InsertNewOperatorCalendar")]
        public int Usp_InsertNewOperatorCalendar([Parameter(Name = "Operator_Id", DbType = "Int")] System.Nullable<int> operator_Id, [Parameter(Name = "Calendar_Id", DbType = "Int")] System.Nullable<int> calendar_Id, [Parameter(Name = "Operator_Calendar_Active", DbType = "Bit")] System.Nullable<bool> operator_Calendar_Active)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), operator_Id, calendar_Id, operator_Calendar_Active);
            return ((int)(result.ReturnValue));
        }
        [Function(Name = "dbo.rsp_GetSubcompanyCalendarByCalendarId")]
        public ISingleResult<rsp_GetSubcompanyCalendarByCalendarIdResult> rsp_GetSubcompanyCalendarByCalendarId([Parameter(Name = "Sub_Company_ID", DbType = "Int")] System.Nullable<int> sub_Company_ID, [Parameter(Name = "Calendar_ID", DbType = "Int")] System.Nullable<int> calendar_ID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), sub_Company_ID, calendar_ID);
            return ((ISingleResult<rsp_GetSubcompanyCalendarByCalendarIdResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetSubCompanyCalenderByActive")]
        public ISingleResult<rsp_GetSubCompanyCalenderByActiveResult> rsp_GetSubCompanyCalenderByActive([Parameter(Name = "SubCompanyId", DbType = "Int")] System.Nullable<int> subCompanyId)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), subCompanyId);
            return ((ISingleResult<rsp_GetSubCompanyCalenderByActiveResult>)(result.ReturnValue));
        }
        [Function(Name = "dbo.Usp_UpdateSubComapnyCalendar")]
        public int Usp_UpdateSubComapnyCalendar([Parameter(Name = "Sub_Company_ID", DbType = "Int")] System.Nullable<int> sub_Company_ID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), sub_Company_ID);
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.Usp_InsertNewSubCompanyCalendar")]
        public int Usp_InsertNewSubCompanyCalendar([Parameter(Name = "Sub_Company_ID", DbType = "Int")] System.Nullable<int> sub_Company_ID, [Parameter(Name = "Calendar_ID", DbType = "Int")] System.Nullable<int> calendar_ID, [Parameter(Name = "Sub_Company_Calendar_Active", DbType = "Bit")] System.Nullable<bool> sub_Company_Calendar_Active)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), sub_Company_ID, calendar_ID, sub_Company_Calendar_Active);
            return ((int)(result.ReturnValue));
        }
        [Function(Name = "dbo.rsp_GetCurrentCalendarDetails")]
        public ISingleResult<rsp_GetCurrentCalendarDetailsResult> GetCurrentCalendarDetails([Parameter(DbType = "Int")] System.Nullable<int> calendarId)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), calendarId);
            return ((ISingleResult<rsp_GetCurrentCalendarDetailsResult>)(result.ReturnValue));
        }
        [Function(Name = "dbo.rsp_GetExportCalendar")]
        public ISingleResult<rsp_GetExportCalendarResult> rsp_GetExportCalendar([Parameter(Name = "Sub_Company_ID", DbType = "Int")] System.Nullable<int> sub_Company_ID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), sub_Company_ID);
            return ((ISingleResult<rsp_GetExportCalendarResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetOrganisationInfo")]
        public ISingleResult<rsp_GetCompanyInfoResult> GetCompanyInfo()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<rsp_GetCompanyInfoResult>)(result.ReturnValue));
        }
        [Function(Name = "dbo.rsp_GetOperatorIDForExport")]
        public ISingleResult<rsp_GetExportCalendarResult> rsp_GetOperatorIDForExport([Parameter(Name = "Operator_ID", DbType = "Int")] System.Nullable<int> operator_ID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), operator_ID);
            return ((ISingleResult<rsp_GetExportCalendarResult>)(result.ReturnValue));
        }
        [Function(Name = "dbo.rsp_GetLstCalendarWeek")]
        public ISingleResult<rsp_GetLstCalendarWeekResult> rsp_GetLstCalendarWeek([Parameter(Name = "Calendar_ID", DbType = "Int")] System.Nullable<int> calendar_ID, [Parameter(Name = "Calendar_Week_Number", DbType = "Int")] System.Nullable<int> calendar_Week_Number, [Parameter(Name = "Calendar_Week_ID", DbType = "Int")] System.Nullable<int> calendar_Week_ID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), calendar_ID, calendar_Week_Number, calendar_Week_ID);
            return ((ISingleResult<rsp_GetLstCalendarWeekResult>)(result.ReturnValue));
        }
        [Function(Name = "dbo.rsp_GetSubCompanyCal")]
        public ISingleResult<rsp_GetSubCompanyCalResult> rsp_GetSubCompanyCal([Parameter(Name = "SubCompanyId", DbType = "Int")] System.Nullable<int> subCompanyId)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), subCompanyId);
            return ((ISingleResult<rsp_GetSubCompanyCalResult>)(result.ReturnValue));
        }
        [Function(Name = "dbo.rsp_GetCalWeek")]
        public ISingleResult<rsp_GetCalWeekResult> rsp_GetCalWeek([Parameter(Name = "Calendar_Week_ID", DbType = "Int")] System.Nullable<int> calendar_Week_ID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), calendar_Week_ID);
            return ((ISingleResult<rsp_GetCalWeekResult>)(result.ReturnValue));
        }
        [Function(Name = "dbo.rsp_GetOperatorForCalendar")]
        public ISingleResult<rsp_GetOperatorForCalendarResult> rsp_GetOperatorForCalendar()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<rsp_GetOperatorForCalendarResult>)(result.ReturnValue));
        }
    }

    public partial class rsp_GetLstCalendarWeekResult
    {

        private int _Calendar_Week_ID;

        private System.Nullable<int> _Calendar_ID;

        private System.Nullable<int> _Calendar_Week_Number;

        private string _Calendar_Week_Start_Date;

        private string _Calendar_Week_End_Date;

        private int _Calendar_Period_ID;

        public rsp_GetLstCalendarWeekResult()
        {
        }

        [Column(Storage = "_Calendar_Week_ID", DbType = "Int NOT NULL")]
        public int Calendar_Week_ID
        {
            get
            {
                return this._Calendar_Week_ID;
            }
            set
            {
                if ((this._Calendar_Week_ID != value))
                {
                    this._Calendar_Week_ID = value;
                }
            }
        }

        [Column(Storage = "_Calendar_ID", DbType = "Int")]
        public System.Nullable<int> Calendar_ID
        {
            get
            {
                return this._Calendar_ID;
            }
            set
            {
                if ((this._Calendar_ID != value))
                {
                    this._Calendar_ID = value;
                }
            }
        }

        [Column(Storage = "_Calendar_Week_Number", DbType = "Int")]
        public System.Nullable<int> Calendar_Week_Number
        {
            get
            {
                return this._Calendar_Week_Number;
            }
            set
            {
                if ((this._Calendar_Week_Number != value))
                {
                    this._Calendar_Week_Number = value;
                }
            }
        }

        [Column(Storage = "_Calendar_Week_Start_Date", DbType = "VarChar(30)")]
        public string Calendar_Week_Start_Date
        {
            get
            {
                return this._Calendar_Week_Start_Date;
            }
            set
            {
                if ((this._Calendar_Week_Start_Date != value))
                {
                    this._Calendar_Week_Start_Date = value;
                }
            }
        }

        [Column(Storage = "_Calendar_Week_End_Date", DbType = "VarChar(30)")]
        public string Calendar_Week_End_Date
        {
            get
            {
                return this._Calendar_Week_End_Date;
            }
            set
            {
                if ((this._Calendar_Week_End_Date != value))
                {
                    this._Calendar_Week_End_Date = value;
                }
            }
        }

        [Column(Storage = "_Calendar_Period_ID", DbType = "Int NOT NULL")]
        public int Calendar_Period_ID
        {
            get
            {
                return this._Calendar_Period_ID;
            }
            set
            {
                if ((this._Calendar_Period_ID != value))
                {
                    this._Calendar_Period_ID = value;
                }
            }
        }
    }
    public partial class rsp_GetSubCompanyCalResult
    {

        private int _Sub_Company_ID;

        private System.Nullable<int> _Staff_ID;

        private System.Nullable<bool> _Staff_ID_Default;

        private System.Nullable<int> _Company_ID;

        private System.Nullable<int> _Access_Key_ID;

        private System.Nullable<bool> _Access_Key_ID_Default;

        private System.Nullable<int> _Terms_Group_ID;

        private System.Nullable<bool> _Terms_Group_ID_Default;

        private System.Nullable<int> _Calendar_ID;

        private System.Nullable<int> _Company_Model_Set_ID;

        private string _Sub_Company_Name;

        private string _Sub_Company_Address;

        private string _Sub_Company_Postcode;

        private string _Sub_Company_Switchboard_Phone_No;

        private string _Sub_Company_Contact_Name;

        private string _Sub_Company_Contact_Phone_No;

        private string _Sub_Company_Contact_Email_Address;

        private string _Sub_Company_Invoice_Address;

        private string _Sub_Company_Invoice_Postcode;

        private string _Sub_Company_Invoice_Name;

        private string _Sub_Company_Price_Per_Play;

        private System.Nullable<bool> _Sub_Company_Price_Per_Play_Default;

        private string _Sub_Company_Jackpot;

        private System.Nullable<bool> _Sub_Company_Jackpot_Default;

        private string _Sub_Company_Percentage_Payout;

        private System.Nullable<bool> _Sub_Company_Percentage_Payout_Default;

        private string _Sub_Company_Start_Date;

        private string _Sub_Company_End_Date;

        private string _Sage_Account_Ref;

        private string _Sub_Company_Memo;

        private string _Sub_Company_ANA_Number;

        private System.Nullable<int> _SLA;

        private string _Sub_Company_Logo_Reference;

        private string _Sub_Company_Trade_Type;

        private System.Nullable<bool> _Sub_Company_Use_Split_Rents;

        private string _Sub_Company_Address_1;

        private string _Sub_Company_Address_2;

        private string _Sub_Company_Address_3;

        private string _Sub_Company_Address_4;

        private string _Sub_Company_Address_5;

        private string _Sub_Company_AMEDIS_Code;

        private string _Sub_Company_AMEDIS_Operational_Code;

        private System.Nullable<bool> _Sub_Company_Validate_Terms;

        private System.Nullable<float> _Sub_Company_Validate_Terms_Variance;

        private System.Nullable<bool> _Sub_Company_Suppress_Docket_Print;

        private System.Nullable<bool> _Sub_Company_Post_Print_Dockets;

        private System.Nullable<int> _Sub_Company_Docket_Type;

        private System.Nullable<bool> _Sub_Company_TX_Collection;

        private System.Nullable<bool> _Sub_Company_TX_Collection_Use_Default;

        private System.Nullable<bool> _Sub_Company_TX_Movement;

        private System.Nullable<bool> _Sub_Company_TX_Movement_Use_Default;

        private System.Nullable<bool> _Sub_Company_TX_EDC;

        private System.Nullable<bool> _Sub_Company_TX_EDC_Use_Detault;

        private System.Nullable<int> _Sub_Company_TX_Format;

        private System.Nullable<bool> _Sub_Company_TX_Format_Use_Default;

        private System.Nullable<bool> _Sub_Company_RX_Collection;

        private System.Nullable<bool> _Sub_Company_RX_Collection_Use_Default;

        private System.Nullable<bool> _Sub_Company_RX_Movement;

        private System.Nullable<bool> _Sub_Company_RX_Movement_Use_Default;

        private System.Nullable<bool> _Sub_Company_RX_EDC;

        private System.Nullable<bool> _Sub_Company_RX_EDC_Use_Detault;

        private System.Nullable<int> _Sub_Company_RX_Format;

        private System.Nullable<bool> _Sub_Company_RX_Format_Use_Default;

        private System.Nullable<bool> _Sub_Company_Period_End_Use_Date_Of_Collection;

        private string _Sub_Company_Income_Ledger_Code;

        private string _Sub_Company_Royalty_Ledger_Code;

        private System.Nullable<int> _Sub_Company_Default_Opening_Hours_ID;

        private string _Sub_Company_Account_Name;

        private string _Sub_Company_Sort_Code;

        private string _Sub_Company_Account_No;

        private string _Sub_Company_EDI_Outbox;

        private string _Sub_Company_Leisure_Data_Brewary_Code;

        private System.Nullable<bool> _Sub_Company_Force_Leisure_Data_To_Enterprise;

        public rsp_GetSubCompanyCalResult()
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

        [Column(Storage = "_Calendar_ID", DbType = "Int")]
        public System.Nullable<int> Calendar_ID
        {
            get
            {
                return this._Calendar_ID;
            }
            set
            {
                if ((this._Calendar_ID != value))
                {
                    this._Calendar_ID = value;
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

        [Column(Storage = "_Sub_Company_Address", DbType = "NText", UpdateCheck = UpdateCheck.Never)]
        public string Sub_Company_Address
        {
            get
            {
                return this._Sub_Company_Address;
            }
            set
            {
                if ((this._Sub_Company_Address != value))
                {
                    this._Sub_Company_Address = value;
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

        [Column(Storage = "_Sub_Company_Start_Date", DbType = "VarChar(30)")]
        public string Sub_Company_Start_Date
        {
            get
            {
                return this._Sub_Company_Start_Date;
            }
            set
            {
                if ((this._Sub_Company_Start_Date != value))
                {
                    this._Sub_Company_Start_Date = value;
                }
            }
        }

        [Column(Storage = "_Sub_Company_End_Date", DbType = "VarChar(30)")]
        public string Sub_Company_End_Date
        {
            get
            {
                return this._Sub_Company_End_Date;
            }
            set
            {
                if ((this._Sub_Company_End_Date != value))
                {
                    this._Sub_Company_End_Date = value;
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

        [Column(Storage = "_Sub_Company_Memo", DbType = "NText", UpdateCheck = UpdateCheck.Never)]
        public string Sub_Company_Memo
        {
            get
            {
                return this._Sub_Company_Memo;
            }
            set
            {
                if ((this._Sub_Company_Memo != value))
                {
                    this._Sub_Company_Memo = value;
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

        [Column(Storage = "_SLA", DbType = "Int")]
        public System.Nullable<int> SLA
        {
            get
            {
                return this._SLA;
            }
            set
            {
                if ((this._SLA != value))
                {
                    this._SLA = value;
                }
            }
        }

        [Column(Storage = "_Sub_Company_Logo_Reference", DbType = "VarChar(50)")]
        public string Sub_Company_Logo_Reference
        {
            get
            {
                return this._Sub_Company_Logo_Reference;
            }
            set
            {
                if ((this._Sub_Company_Logo_Reference != value))
                {
                    this._Sub_Company_Logo_Reference = value;
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

        [Column(Storage = "_Sub_Company_AMEDIS_Code", DbType = "VarChar(10)")]
        public string Sub_Company_AMEDIS_Code
        {
            get
            {
                return this._Sub_Company_AMEDIS_Code;
            }
            set
            {
                if ((this._Sub_Company_AMEDIS_Code != value))
                {
                    this._Sub_Company_AMEDIS_Code = value;
                }
            }
        }

        [Column(Storage = "_Sub_Company_AMEDIS_Operational_Code", DbType = "VarChar(10)")]
        public string Sub_Company_AMEDIS_Operational_Code
        {
            get
            {
                return this._Sub_Company_AMEDIS_Operational_Code;
            }
            set
            {
                if ((this._Sub_Company_AMEDIS_Operational_Code != value))
                {
                    this._Sub_Company_AMEDIS_Operational_Code = value;
                }
            }
        }

        [Column(Storage = "_Sub_Company_Validate_Terms", DbType = "Bit")]
        public System.Nullable<bool> Sub_Company_Validate_Terms
        {
            get
            {
                return this._Sub_Company_Validate_Terms;
            }
            set
            {
                if ((this._Sub_Company_Validate_Terms != value))
                {
                    this._Sub_Company_Validate_Terms = value;
                }
            }
        }

        [Column(Storage = "_Sub_Company_Validate_Terms_Variance", DbType = "Real")]
        public System.Nullable<float> Sub_Company_Validate_Terms_Variance
        {
            get
            {
                return this._Sub_Company_Validate_Terms_Variance;
            }
            set
            {
                if ((this._Sub_Company_Validate_Terms_Variance != value))
                {
                    this._Sub_Company_Validate_Terms_Variance = value;
                }
            }
        }

        [Column(Storage = "_Sub_Company_Suppress_Docket_Print", DbType = "Bit")]
        public System.Nullable<bool> Sub_Company_Suppress_Docket_Print
        {
            get
            {
                return this._Sub_Company_Suppress_Docket_Print;
            }
            set
            {
                if ((this._Sub_Company_Suppress_Docket_Print != value))
                {
                    this._Sub_Company_Suppress_Docket_Print = value;
                }
            }
        }

        [Column(Storage = "_Sub_Company_Post_Print_Dockets", DbType = "Bit")]
        public System.Nullable<bool> Sub_Company_Post_Print_Dockets
        {
            get
            {
                return this._Sub_Company_Post_Print_Dockets;
            }
            set
            {
                if ((this._Sub_Company_Post_Print_Dockets != value))
                {
                    this._Sub_Company_Post_Print_Dockets = value;
                }
            }
        }

        [Column(Storage = "_Sub_Company_Docket_Type", DbType = "Int")]
        public System.Nullable<int> Sub_Company_Docket_Type
        {
            get
            {
                return this._Sub_Company_Docket_Type;
            }
            set
            {
                if ((this._Sub_Company_Docket_Type != value))
                {
                    this._Sub_Company_Docket_Type = value;
                }
            }
        }

        [Column(Storage = "_Sub_Company_TX_Collection", DbType = "Bit")]
        public System.Nullable<bool> Sub_Company_TX_Collection
        {
            get
            {
                return this._Sub_Company_TX_Collection;
            }
            set
            {
                if ((this._Sub_Company_TX_Collection != value))
                {
                    this._Sub_Company_TX_Collection = value;
                }
            }
        }

        [Column(Storage = "_Sub_Company_TX_Collection_Use_Default", DbType = "Bit")]
        public System.Nullable<bool> Sub_Company_TX_Collection_Use_Default
        {
            get
            {
                return this._Sub_Company_TX_Collection_Use_Default;
            }
            set
            {
                if ((this._Sub_Company_TX_Collection_Use_Default != value))
                {
                    this._Sub_Company_TX_Collection_Use_Default = value;
                }
            }
        }

        [Column(Storage = "_Sub_Company_TX_Movement", DbType = "Bit")]
        public System.Nullable<bool> Sub_Company_TX_Movement
        {
            get
            {
                return this._Sub_Company_TX_Movement;
            }
            set
            {
                if ((this._Sub_Company_TX_Movement != value))
                {
                    this._Sub_Company_TX_Movement = value;
                }
            }
        }

        [Column(Storage = "_Sub_Company_TX_Movement_Use_Default", DbType = "Bit")]
        public System.Nullable<bool> Sub_Company_TX_Movement_Use_Default
        {
            get
            {
                return this._Sub_Company_TX_Movement_Use_Default;
            }
            set
            {
                if ((this._Sub_Company_TX_Movement_Use_Default != value))
                {
                    this._Sub_Company_TX_Movement_Use_Default = value;
                }
            }
        }

        [Column(Storage = "_Sub_Company_TX_EDC", DbType = "Bit")]
        public System.Nullable<bool> Sub_Company_TX_EDC
        {
            get
            {
                return this._Sub_Company_TX_EDC;
            }
            set
            {
                if ((this._Sub_Company_TX_EDC != value))
                {
                    this._Sub_Company_TX_EDC = value;
                }
            }
        }

        [Column(Storage = "_Sub_Company_TX_EDC_Use_Detault", DbType = "Bit")]
        public System.Nullable<bool> Sub_Company_TX_EDC_Use_Detault
        {
            get
            {
                return this._Sub_Company_TX_EDC_Use_Detault;
            }
            set
            {
                if ((this._Sub_Company_TX_EDC_Use_Detault != value))
                {
                    this._Sub_Company_TX_EDC_Use_Detault = value;
                }
            }
        }

        [Column(Storage = "_Sub_Company_TX_Format", DbType = "Int")]
        public System.Nullable<int> Sub_Company_TX_Format
        {
            get
            {
                return this._Sub_Company_TX_Format;
            }
            set
            {
                if ((this._Sub_Company_TX_Format != value))
                {
                    this._Sub_Company_TX_Format = value;
                }
            }
        }

        [Column(Storage = "_Sub_Company_TX_Format_Use_Default", DbType = "Bit")]
        public System.Nullable<bool> Sub_Company_TX_Format_Use_Default
        {
            get
            {
                return this._Sub_Company_TX_Format_Use_Default;
            }
            set
            {
                if ((this._Sub_Company_TX_Format_Use_Default != value))
                {
                    this._Sub_Company_TX_Format_Use_Default = value;
                }
            }
        }

        [Column(Storage = "_Sub_Company_RX_Collection", DbType = "Bit")]
        public System.Nullable<bool> Sub_Company_RX_Collection
        {
            get
            {
                return this._Sub_Company_RX_Collection;
            }
            set
            {
                if ((this._Sub_Company_RX_Collection != value))
                {
                    this._Sub_Company_RX_Collection = value;
                }
            }
        }

        [Column(Storage = "_Sub_Company_RX_Collection_Use_Default", DbType = "Bit")]
        public System.Nullable<bool> Sub_Company_RX_Collection_Use_Default
        {
            get
            {
                return this._Sub_Company_RX_Collection_Use_Default;
            }
            set
            {
                if ((this._Sub_Company_RX_Collection_Use_Default != value))
                {
                    this._Sub_Company_RX_Collection_Use_Default = value;
                }
            }
        }

        [Column(Storage = "_Sub_Company_RX_Movement", DbType = "Bit")]
        public System.Nullable<bool> Sub_Company_RX_Movement
        {
            get
            {
                return this._Sub_Company_RX_Movement;
            }
            set
            {
                if ((this._Sub_Company_RX_Movement != value))
                {
                    this._Sub_Company_RX_Movement = value;
                }
            }
        }

        [Column(Storage = "_Sub_Company_RX_Movement_Use_Default", DbType = "Bit")]
        public System.Nullable<bool> Sub_Company_RX_Movement_Use_Default
        {
            get
            {
                return this._Sub_Company_RX_Movement_Use_Default;
            }
            set
            {
                if ((this._Sub_Company_RX_Movement_Use_Default != value))
                {
                    this._Sub_Company_RX_Movement_Use_Default = value;
                }
            }
        }

        [Column(Storage = "_Sub_Company_RX_EDC", DbType = "Bit")]
        public System.Nullable<bool> Sub_Company_RX_EDC
        {
            get
            {
                return this._Sub_Company_RX_EDC;
            }
            set
            {
                if ((this._Sub_Company_RX_EDC != value))
                {
                    this._Sub_Company_RX_EDC = value;
                }
            }
        }

        [Column(Storage = "_Sub_Company_RX_EDC_Use_Detault", DbType = "Bit")]
        public System.Nullable<bool> Sub_Company_RX_EDC_Use_Detault
        {
            get
            {
                return this._Sub_Company_RX_EDC_Use_Detault;
            }
            set
            {
                if ((this._Sub_Company_RX_EDC_Use_Detault != value))
                {
                    this._Sub_Company_RX_EDC_Use_Detault = value;
                }
            }
        }

        [Column(Storage = "_Sub_Company_RX_Format", DbType = "Int")]
        public System.Nullable<int> Sub_Company_RX_Format
        {
            get
            {
                return this._Sub_Company_RX_Format;
            }
            set
            {
                if ((this._Sub_Company_RX_Format != value))
                {
                    this._Sub_Company_RX_Format = value;
                }
            }
        }

        [Column(Storage = "_Sub_Company_RX_Format_Use_Default", DbType = "Bit")]
        public System.Nullable<bool> Sub_Company_RX_Format_Use_Default
        {
            get
            {
                return this._Sub_Company_RX_Format_Use_Default;
            }
            set
            {
                if ((this._Sub_Company_RX_Format_Use_Default != value))
                {
                    this._Sub_Company_RX_Format_Use_Default = value;
                }
            }
        }

        [Column(Storage = "_Sub_Company_Period_End_Use_Date_Of_Collection", DbType = "Bit")]
        public System.Nullable<bool> Sub_Company_Period_End_Use_Date_Of_Collection
        {
            get
            {
                return this._Sub_Company_Period_End_Use_Date_Of_Collection;
            }
            set
            {
                if ((this._Sub_Company_Period_End_Use_Date_Of_Collection != value))
                {
                    this._Sub_Company_Period_End_Use_Date_Of_Collection = value;
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

        [Column(Storage = "_Sub_Company_Royalty_Ledger_Code", DbType = "VarChar(20)")]
        public string Sub_Company_Royalty_Ledger_Code
        {
            get
            {
                return this._Sub_Company_Royalty_Ledger_Code;
            }
            set
            {
                if ((this._Sub_Company_Royalty_Ledger_Code != value))
                {
                    this._Sub_Company_Royalty_Ledger_Code = value;
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

        [Column(Storage = "_Sub_Company_EDI_Outbox", DbType = "VarChar(50)")]
        public string Sub_Company_EDI_Outbox
        {
            get
            {
                return this._Sub_Company_EDI_Outbox;
            }
            set
            {
                if ((this._Sub_Company_EDI_Outbox != value))
                {
                    this._Sub_Company_EDI_Outbox = value;
                }
            }
        }

        [Column(Storage = "_Sub_Company_Leisure_Data_Brewary_Code", DbType = "VarChar(50)")]
        public string Sub_Company_Leisure_Data_Brewary_Code
        {
            get
            {
                return this._Sub_Company_Leisure_Data_Brewary_Code;
            }
            set
            {
                if ((this._Sub_Company_Leisure_Data_Brewary_Code != value))
                {
                    this._Sub_Company_Leisure_Data_Brewary_Code = value;
                }
            }
        }

        [Column(Storage = "_Sub_Company_Force_Leisure_Data_To_Enterprise", DbType = "Bit")]
        public System.Nullable<bool> Sub_Company_Force_Leisure_Data_To_Enterprise
        {
            get
            {
                return this._Sub_Company_Force_Leisure_Data_To_Enterprise;
            }
            set
            {
                if ((this._Sub_Company_Force_Leisure_Data_To_Enterprise != value))
                {
                    this._Sub_Company_Force_Leisure_Data_To_Enterprise = value;
                }
            }
        }
    }
    public partial class rsp_GetCalWeekResult
    {

        private int _Calendar_Week_ID;

        private System.Nullable<int> _Calendar_ID;

        private System.Nullable<int> _Calendar_Week_Number;

        private string _Calendar_Week_Start_Date;

        private string _Calendar_Week_End_Date;

        private int _Calendar_Period_ID;

        public rsp_GetCalWeekResult()
        {
        }

        [Column(Storage = "_Calendar_Week_ID", DbType = "Int NOT NULL")]
        public int Calendar_Week_ID
        {
            get
            {
                return this._Calendar_Week_ID;
            }
            set
            {
                if ((this._Calendar_Week_ID != value))
                {
                    this._Calendar_Week_ID = value;
                }
            }
        }

        [Column(Storage = "_Calendar_ID", DbType = "Int")]
        public System.Nullable<int> Calendar_ID
        {
            get
            {
                return this._Calendar_ID;
            }
            set
            {
                if ((this._Calendar_ID != value))
                {
                    this._Calendar_ID = value;
                }
            }
        }

        [Column(Storage = "_Calendar_Week_Number", DbType = "Int")]
        public System.Nullable<int> Calendar_Week_Number
        {
            get
            {
                return this._Calendar_Week_Number;
            }
            set
            {
                if ((this._Calendar_Week_Number != value))
                {
                    this._Calendar_Week_Number = value;
                }
            }
        }

        [Column(Storage = "_Calendar_Week_Start_Date", DbType = "VarChar(30)")]
        public string Calendar_Week_Start_Date
        {
            get
            {
                return this._Calendar_Week_Start_Date;
            }
            set
            {
                if ((this._Calendar_Week_Start_Date != value))
                {
                    this._Calendar_Week_Start_Date = value;
                }
            }
        }

        [Column(Storage = "_Calendar_Week_End_Date", DbType = "VarChar(30)")]
        public string Calendar_Week_End_Date
        {
            get
            {
                return this._Calendar_Week_End_Date;
            }
            set
            {
                if ((this._Calendar_Week_End_Date != value))
                {
                    this._Calendar_Week_End_Date = value;
                }
            }
        }

        [Column(Storage = "_Calendar_Period_ID", DbType = "Int NOT NULL")]
        public int Calendar_Period_ID
        {
            get
            {
                return this._Calendar_Period_ID;
            }
            set
            {
                if ((this._Calendar_Period_ID != value))
                {
                    this._Calendar_Period_ID = value;
                }
            }
        }
    }


    public partial class rsp_GetCompanyInfoResult
    {

        private string _Company_Name;

        private string _Sub_Company_Name;

        private string _Site_Name;

        private System.Nullable<int> _Site_ID;

        private int _Company_ID;

        private System.Nullable<int> _Sub_Company_ID;

        private string _Site_Code;

        private string _SiteStatus;

        public rsp_GetCompanyInfoResult()
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

        [Column(Storage = "_Site_ID", DbType = "Int")]
        public System.Nullable<int> Site_ID
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

        [Column(Storage = "_Sub_Company_ID", DbType = "Int")]
        public System.Nullable<int> Sub_Company_ID
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

        [Column(Storage = "_Site_Code", DbType = "VarChar(50)")]
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

        [Column(Storage = "_SiteStatus", DbType = "VarChar(300)")]
        public string SiteStatus
        {
            get
            {
                return this._SiteStatus;
            }
            set
            {
                if ((this._SiteStatus != value))
                {
                    this._SiteStatus = value;
                }
            }
        }
    }
    public partial class rsp_GetExportCalendarResult
    {

        private int _Site_ID;

        private string _Site_Name;

        public rsp_GetExportCalendarResult()
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
    public partial class rsp_GetCurrentCalendarDetailsResult
    {

        private System.Nullable<int> _Calendar_Week_Number;

        private System.Nullable<int> _Calendar_Period_Number;

        private string _Calendar_Period_End_Date;

        public rsp_GetCurrentCalendarDetailsResult()
        {
        }

        [Column(Storage = "_Calendar_Week_Number", DbType = "Int")]
        public System.Nullable<int> Calendar_Week_Number
        {
            get
            {
                return this._Calendar_Week_Number;
            }
            set
            {
                if ((this._Calendar_Week_Number != value))
                {
                    this._Calendar_Week_Number = value;
                }
            }
        }

        [Column(Storage = "_Calendar_Period_Number", DbType = "Int")]
        public System.Nullable<int> Calendar_Period_Number
        {
            get
            {
                return this._Calendar_Period_Number;
            }
            set
            {
                if ((this._Calendar_Period_Number != value))
                {
                    this._Calendar_Period_Number = value;
                }
            }
        }

        [Column(Storage = "_Calendar_Period_End_Date", DbType = "VarChar(30)")]
        public string Calendar_Period_End_Date
        {
            get
            {
                return this._Calendar_Period_End_Date;
            }
            set
            {
                if ((this._Calendar_Period_End_Date != value))
                {
                    this._Calendar_Period_End_Date = value;
                }
            }
        }
    }
    public partial class rsp_GetSubCompanyCalenderByActiveResult
    {

        private int _Sub_Company_Calendar_ID;

        private System.Nullable<int> _Sub_Company_ID;

        private System.Nullable<int> _Calendar_ID;

        private System.Nullable<bool> _Sub_Company_Calendar_Active;

        public rsp_GetSubCompanyCalenderByActiveResult()
        {
        }

        [Column(Storage = "_Sub_Company_Calendar_ID", DbType = "Int NOT NULL")]
        public int Sub_Company_Calendar_ID
        {
            get
            {
                return this._Sub_Company_Calendar_ID;
            }
            set
            {
                if ((this._Sub_Company_Calendar_ID != value))
                {
                    this._Sub_Company_Calendar_ID = value;
                }
            }
        }

        [Column(Storage = "_Sub_Company_ID", DbType = "Int")]
        public System.Nullable<int> Sub_Company_ID
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

        [Column(Storage = "_Calendar_ID", DbType = "Int")]
        public System.Nullable<int> Calendar_ID
        {
            get
            {
                return this._Calendar_ID;
            }
            set
            {
                if ((this._Calendar_ID != value))
                {
                    this._Calendar_ID = value;
                }
            }
        }

        [Column(Storage = "_Sub_Company_Calendar_Active", DbType = "Bit")]
        public System.Nullable<bool> Sub_Company_Calendar_Active
        {
            get
            {
                return this._Sub_Company_Calendar_Active;
            }
            set
            {
                if ((this._Sub_Company_Calendar_Active != value))
                {
                    this._Sub_Company_Calendar_Active = value;
                }
            }
        }
    }

    public partial class rsp_GetSubcompanyCalendarByCalendarIdResult
    {

        private int _Sub_Company_Calendar_ID;

        private System.Nullable<int> _Sub_Company_ID;

        private System.Nullable<int> _Calendar_ID;

        private System.Nullable<bool> _Sub_Company_Calendar_Active;

        public rsp_GetSubcompanyCalendarByCalendarIdResult()
        {
        }

        [Column(Storage = "_Sub_Company_Calendar_ID", DbType = "Int NOT NULL")]
        public int Sub_Company_Calendar_ID
        {
            get
            {
                return this._Sub_Company_Calendar_ID;
            }
            set
            {
                if ((this._Sub_Company_Calendar_ID != value))
                {
                    this._Sub_Company_Calendar_ID = value;
                }
            }
        }

        [Column(Storage = "_Sub_Company_ID", DbType = "Int")]
        public System.Nullable<int> Sub_Company_ID
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

        [Column(Storage = "_Calendar_ID", DbType = "Int")]
        public System.Nullable<int> Calendar_ID
        {
            get
            {
                return this._Calendar_ID;
            }
            set
            {
                if ((this._Calendar_ID != value))
                {
                    this._Calendar_ID = value;
                }
            }
        }

        [Column(Storage = "_Sub_Company_Calendar_Active", DbType = "Bit")]
        public System.Nullable<bool> Sub_Company_Calendar_Active
        {
            get
            {
                return this._Sub_Company_Calendar_Active;
            }
            set
            {
                if ((this._Sub_Company_Calendar_Active != value))
                {
                    this._Sub_Company_Calendar_Active = value;
                }
            }
        }

    }
    public partial class rsp_GetOperatorByActiveResult
    {

        private int _Operator_Calendar_ID;

        private System.Nullable<int> _Operator_ID;

        private System.Nullable<int> _Calendar_ID;

        private System.Nullable<bool> _Operator_Calendar_Active;

        public rsp_GetOperatorByActiveResult()
        {
        }

        [Column(Storage = "_Operator_Calendar_ID", DbType = "Int NOT NULL")]
        public int Operator_Calendar_ID
        {
            get
            {
                return this._Operator_Calendar_ID;
            }
            set
            {
                if ((this._Operator_Calendar_ID != value))
                {
                    this._Operator_Calendar_ID = value;
                }
            }
        }

        [Column(Storage = "_Operator_ID", DbType = "Int")]
        public System.Nullable<int> Operator_ID
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

        [Column(Storage = "_Calendar_ID", DbType = "Int")]
        public System.Nullable<int> Calendar_ID
        {
            get
            {
                return this._Calendar_ID;
            }
            set
            {
                if ((this._Calendar_ID != value))
                {
                    this._Calendar_ID = value;
                }
            }
        }

        [Column(Storage = "_Operator_Calendar_Active", DbType = "Bit")]
        public System.Nullable<bool> Operator_Calendar_Active
        {
            get
            {
                return this._Operator_Calendar_Active;
            }
            set
            {
                if ((this._Operator_Calendar_Active != value))
                {
                    this._Operator_Calendar_Active = value;
                }
            }
        }
    }

    public partial class rsp_GetOperatorCalendarResult
    {

        private int _Operator_Calendar_ID;

        private System.Nullable<int> _Operator_ID;

        private System.Nullable<int> _Calendar_ID;

        private System.Nullable<bool> _Operator_Calendar_Active;

        public rsp_GetOperatorCalendarResult()
        {
        }

        [Column(Storage = "_Operator_Calendar_ID", DbType = "Int NOT NULL")]
        public int Operator_Calendar_ID
        {
            get
            {
                return this._Operator_Calendar_ID;
            }
            set
            {
                if ((this._Operator_Calendar_ID != value))
                {
                    this._Operator_Calendar_ID = value;
                }
            }
        }

        [Column(Storage = "_Operator_ID", DbType = "Int")]
        public System.Nullable<int> Operator_ID
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

        [Column(Storage = "_Calendar_ID", DbType = "Int")]
        public System.Nullable<int> Calendar_ID
        {
            get
            {
                return this._Calendar_ID;
            }
            set
            {
                if ((this._Calendar_ID != value))
                {
                    this._Calendar_ID = value;
                }
            }
        }

        [Column(Storage = "_Operator_Calendar_Active", DbType = "Bit")]
        public System.Nullable<bool> Operator_Calendar_Active
        {
            get
            {
                return this._Operator_Calendar_Active;
            }
            set
            {
                if ((this._Operator_Calendar_Active != value))
                {
                    this._Operator_Calendar_Active = value;
                }
            }
        }
    }
    public partial class rsp_CancelWeekCalendarResult
    {

        private int _Calendar_Week_ID;

        private System.Nullable<int> _Calendar_ID;

        private System.Nullable<int> _Calendar_Week_Number;

        private string _Calendar_Week_Start_Date;

        private string _Calendar_Week_End_Date;

        private int _Calendar_Period_ID;

        public rsp_CancelWeekCalendarResult()
        {
        }

        [Column(Storage = "_Calendar_Week_ID", DbType = "Int NOT NULL")]
        public int Calendar_Week_ID
        {
            get
            {
                return this._Calendar_Week_ID;
            }
            set
            {
                if ((this._Calendar_Week_ID != value))
                {
                    this._Calendar_Week_ID = value;
                }
            }
        }

        [Column(Storage = "_Calendar_ID", DbType = "Int")]
        public System.Nullable<int> Calendar_ID
        {
            get
            {
                return this._Calendar_ID;
            }
            set
            {
                if ((this._Calendar_ID != value))
                {
                    this._Calendar_ID = value;
                }
            }
        }

        [Column(Storage = "_Calendar_Week_Number", DbType = "Int")]
        public System.Nullable<int> Calendar_Week_Number
        {
            get
            {
                return this._Calendar_Week_Number;
            }
            set
            {
                if ((this._Calendar_Week_Number != value))
                {
                    this._Calendar_Week_Number = value;
                }
            }
        }

        [Column(Storage = "_Calendar_Week_Start_Date", DbType = "VarChar(30)")]
        public string Calendar_Week_Start_Date
        {
            get
            {
                return this._Calendar_Week_Start_Date;
            }
            set
            {
                if ((this._Calendar_Week_Start_Date != value))
                {
                    this._Calendar_Week_Start_Date = value;
                }
            }
        }

        [Column(Storage = "_Calendar_Week_End_Date", DbType = "VarChar(30)")]
        public string Calendar_Week_End_Date
        {
            get
            {
                return this._Calendar_Week_End_Date;
            }
            set
            {
                if ((this._Calendar_Week_End_Date != value))
                {
                    this._Calendar_Week_End_Date = value;
                }
            }
        }

        [Column(Storage = "_Calendar_Period_ID", DbType = "Int NOT NULL")]
        public int Calendar_Period_ID
        {
            get
            {
                return this._Calendar_Period_ID;
            }
            set
            {
                if ((this._Calendar_Period_ID != value))
                {
                    this._Calendar_Period_ID = value;
                }
            }
        }

    }
    public partial class rsp_GetCalendarWeekResult
    {

        private int _Calendar_Week_ID;

        private System.Nullable<int> _Calendar_ID;

        private System.Nullable<int> _Calendar_Week_Number;

        private string _Calendar_Week_Start_Date;

        private string _Calendar_Week_End_Date;

        private int _Calendar_Period_ID;

        public rsp_GetCalendarWeekResult()
        {
        }

        [Column(Storage = "_Calendar_Week_ID", DbType = "Int NOT NULL")]
        public int Calendar_Week_ID
        {
            get
            {
                return this._Calendar_Week_ID;
            }
            set
            {
                if ((this._Calendar_Week_ID != value))
                {
                    this._Calendar_Week_ID = value;
                }
            }
        }

        [Column(Storage = "_Calendar_ID", DbType = "Int")]
        public System.Nullable<int> Calendar_ID
        {
            get
            {
                return this._Calendar_ID;
            }
            set
            {
                if ((this._Calendar_ID != value))
                {
                    this._Calendar_ID = value;
                }
            }
        }

        [Column(Storage = "_Calendar_Week_Number", DbType = "Int")]
        public System.Nullable<int> Calendar_Week_Number
        {
            get
            {
                return this._Calendar_Week_Number;
            }
            set
            {
                if ((this._Calendar_Week_Number != value))
                {
                    this._Calendar_Week_Number = value;
                }
            }
        }

        [Column(Storage = "_Calendar_Week_Start_Date", DbType = "VarChar(30)")]
        public string Calendar_Week_Start_Date
        {
            get
            {
                return this._Calendar_Week_Start_Date;
            }
            set
            {
                if ((this._Calendar_Week_Start_Date != value))
                {
                    this._Calendar_Week_Start_Date = value;
                }
            }
        }

        [Column(Storage = "_Calendar_Week_End_Date", DbType = "VarChar(30)")]
        public string Calendar_Week_End_Date
        {
            get
            {
                return this._Calendar_Week_End_Date;
            }
            set
            {
                if ((this._Calendar_Week_End_Date != value))
                {
                    this._Calendar_Week_End_Date = value;
                }
            }
        }

        [Column(Storage = "_Calendar_Period_ID", DbType = "Int NOT NULL")]
        public int Calendar_Period_ID
        {
            get
            {
                return this._Calendar_Period_ID;
            }
            set
            {
                if ((this._Calendar_Period_ID != value))
                {
                    this._Calendar_Period_ID = value;
                }
            }
        }
    }
    public partial class rsp_GetCalendarResult
    {

        private int _Calendar_ID;

        private string _Calendar_Description;

        private string _Calendar_Year_Start_Date;

        private string _Calendar_Year_End_Date;

        private int _IsCalendarCreatedUsingAutoCalendar;

        private string _CalendarBasedOn;

        public rsp_GetCalendarResult()
        {
        }

        [Column(Storage = "_Calendar_ID", DbType = "Int NOT NULL")]
        public int Calendar_ID
        {
            get
            {
                return this._Calendar_ID;
            }
            set
            {
                if ((this._Calendar_ID != value))
                {
                    this._Calendar_ID = value;
                }
            }
        }

        [Column(Storage = "_Calendar_Description", DbType = "VarChar(50)")]
        public string Calendar_Description
        {
            get
            {
                return this._Calendar_Description;
            }
            set
            {
                if ((this._Calendar_Description != value))
                {
                    this._Calendar_Description = value;
                }
            }
        }

        [Column(Storage = "_Calendar_Year_Start_Date", DbType = "VarChar(30)")]
        public string Calendar_Year_Start_Date
        {
            get
            {
                return this._Calendar_Year_Start_Date;
            }
            set
            {
                if ((this._Calendar_Year_Start_Date != value))
                {
                    this._Calendar_Year_Start_Date = value;
                }
            }
        }

        [Column(Storage = "_Calendar_Year_End_Date", DbType = "VarChar(30)")]
        public string Calendar_Year_End_Date
        {
            get
            {
                return this._Calendar_Year_End_Date;
            }
            set
            {
                if ((this._Calendar_Year_End_Date != value))
                {
                    this._Calendar_Year_End_Date = value;
                }
            }
        }

        [Column(Storage = "_IsCalendarCreatedUsingAutoCalendar", DbType = "Int")]
        public int IsCalendarCreatedUsingAutoCalendar
        {
            get
            {
                return this._IsCalendarCreatedUsingAutoCalendar;
            }
            set
            {
                if ((this._IsCalendarCreatedUsingAutoCalendar != value))
                {
                    this._IsCalendarCreatedUsingAutoCalendar = value;
                }
            }
        }

        [Column(Storage = "_CalendarBasedOn", DbType = "VarChar(9)")]
        public string CalendarBasedOn
        {
            get
            {
                return this._CalendarBasedOn;
            }
            set
            {
                if ((this._CalendarBasedOn != value))
                {
                    this._CalendarBasedOn = value;
                }
            }
        }
    }
    public partial class rsp_GetCalendarPeriodResult
    {

        private int _Calendar_Period_ID;

        private System.Nullable<int> _Calendar_ID;

        private System.Nullable<int> _Calendar_Period_Number;

        private string _Calendar_Period_Start_Date;

        private string _Calendar_Period_End_Date;

        public rsp_GetCalendarPeriodResult()
        {
        }

        [Column(Storage = "_Calendar_Period_ID", DbType = "Int NOT NULL")]
        public int Calendar_Period_ID
        {
            get
            {
                return this._Calendar_Period_ID;
            }
            set
            {
                if ((this._Calendar_Period_ID != value))
                {
                    this._Calendar_Period_ID = value;
                }
            }
        }

        [Column(Storage = "_Calendar_ID", DbType = "Int")]
        public System.Nullable<int> Calendar_ID
        {
            get
            {
                return this._Calendar_ID;
            }
            set
            {
                if ((this._Calendar_ID != value))
                {
                    this._Calendar_ID = value;
                }
            }
        }

        [Column(Storage = "_Calendar_Period_Number", DbType = "Int")]
        public System.Nullable<int> Calendar_Period_Number
        {
            get
            {
                return this._Calendar_Period_Number;
            }
            set
            {
                if ((this._Calendar_Period_Number != value))
                {
                    this._Calendar_Period_Number = value;
                }
            }
        }

        [Column(Storage = "_Calendar_Period_Start_Date", DbType = "VarChar(30)")]
        public string Calendar_Period_Start_Date
        {
            get
            {
                return this._Calendar_Period_Start_Date;
            }
            set
            {
                if ((this._Calendar_Period_Start_Date != value))
                {
                    this._Calendar_Period_Start_Date = value;
                }
            }
        }

        [Column(Storage = "_Calendar_Period_End_Date", DbType = "VarChar(30)")]
        public string Calendar_Period_End_Date
        {
            get
            {
                return this._Calendar_Period_End_Date;
            }
            set
            {
                if ((this._Calendar_Period_End_Date != value))
                {
                    this._Calendar_Period_End_Date = value;
                }
            }
        }
    }
    public partial class rsp_CancelPeriodCalendarResult
    {

        private int _Calendar_Period_ID;

        private System.Nullable<int> _Calendar_ID;

        private System.Nullable<int> _Calendar_Period_Number;

        private string _Calendar_Period_Start_Date;

        private string _Calendar_Period_End_Date;

        public rsp_CancelPeriodCalendarResult()
        {
        }

        [Column(Storage = "_Calendar_Period_ID", DbType = "Int NOT NULL")]
        public int Calendar_Period_ID
        {
            get
            {
                return this._Calendar_Period_ID;
            }
            set
            {
                if ((this._Calendar_Period_ID != value))
                {
                    this._Calendar_Period_ID = value;
                }
            }
        }

        [Column(Storage = "_Calendar_ID", DbType = "Int")]
        public System.Nullable<int> Calendar_ID
        {
            get
            {
                return this._Calendar_ID;
            }
            set
            {
                if ((this._Calendar_ID != value))
                {
                    this._Calendar_ID = value;
                }
            }
        }

        [Column(Storage = "_Calendar_Period_Number", DbType = "Int")]
        public System.Nullable<int> Calendar_Period_Number
        {
            get
            {
                return this._Calendar_Period_Number;
            }
            set
            {
                if ((this._Calendar_Period_Number != value))
                {
                    this._Calendar_Period_Number = value;
                }
            }
        }

        [Column(Storage = "_Calendar_Period_Start_Date", DbType = "VarChar(30)")]
        public string Calendar_Period_Start_Date
        {
            get
            {
                return this._Calendar_Period_Start_Date;
            }
            set
            {
                if ((this._Calendar_Period_Start_Date != value))
                {
                    this._Calendar_Period_Start_Date = value;
                }
            }
        }

        [Column(Storage = "_Calendar_Period_End_Date", DbType = "VarChar(30)")]
        public string Calendar_Period_End_Date
        {
            get
            {
                return this._Calendar_Period_End_Date;
            }
            set
            {
                if ((this._Calendar_Period_End_Date != value))
                {
                    this._Calendar_Period_End_Date = value;
                }
            }
        }
    }
    public partial class rsp_GetCalPeriodResult
    {

        private int _Calendar_Period_ID;

        private System.Nullable<int> _Calendar_ID;

        private System.Nullable<int> _Calendar_Period_Number;

        private string _Calendar_Period_Start_Date;

        private string _Calendar_Period_End_Date;

        public rsp_GetCalPeriodResult()
        {
        }

        [Column(Storage = "_Calendar_Period_ID", DbType = "Int NOT NULL")]
        public int Calendar_Period_ID
        {
            get
            {
                return this._Calendar_Period_ID;
            }
            set
            {
                if ((this._Calendar_Period_ID != value))
                {
                    this._Calendar_Period_ID = value;
                }
            }
        }

        [Column(Storage = "_Calendar_ID", DbType = "Int")]
        public System.Nullable<int> Calendar_ID
        {
            get
            {
                return this._Calendar_ID;
            }
            set
            {
                if ((this._Calendar_ID != value))
                {
                    this._Calendar_ID = value;
                }
            }
        }

        [Column(Storage = "_Calendar_Period_Number", DbType = "Int")]
        public System.Nullable<int> Calendar_Period_Number
        {
            get
            {
                return this._Calendar_Period_Number;
            }
            set
            {
                if ((this._Calendar_Period_Number != value))
                {
                    this._Calendar_Period_Number = value;
                }
            }
        }

        [Column(Storage = "_Calendar_Period_Start_Date", DbType = "VarChar(30)")]
        public string Calendar_Period_Start_Date
        {
            get
            {
                return this._Calendar_Period_Start_Date;
            }
            set
            {
                if ((this._Calendar_Period_Start_Date != value))
                {
                    this._Calendar_Period_Start_Date = value;
                }
            }
        }

        [Column(Storage = "_Calendar_Period_End_Date", DbType = "VarChar(30)")]
        public string Calendar_Period_End_Date
        {
            get
            {
                return this._Calendar_Period_End_Date;
            }
            set
            {
                if ((this._Calendar_Period_End_Date != value))
                {
                    this._Calendar_Period_End_Date = value;
                }
            }
        }
    }
    public partial class rsp_GetSubCompanyCalendarActiveResult
    {

        private int _Sub_Company_Calendar_ID;

        private int _Calendar_ID;

        private System.Nullable<bool> _Sub_Company_Calendar_Active;

        private string _Calendar_Description;

        private string _Calendar_Year_Start_Date;

        private string _Calendar_Year_End_Date;

        private string _Calendar_History;

        public rsp_GetSubCompanyCalendarActiveResult()
        {
        }

        [Column(Storage = "_Sub_Company_Calendar_ID", DbType = "Int NOT NULL")]
        public int Sub_Company_Calendar_ID
        {
            get
            {
                return this._Sub_Company_Calendar_ID;
            }
            set
            {
                if ((this._Sub_Company_Calendar_ID != value))
                {
                    this._Sub_Company_Calendar_ID = value;
                }
            }
        }

        [Column(Storage = "_Calendar_ID", DbType = "Int NOT NULL")]
        public int Calendar_ID
        {
            get
            {
                return this._Calendar_ID;
            }
            set
            {
                if ((this._Calendar_ID != value))
                {
                    this._Calendar_ID = value;
                }
            }
        }

        [Column(Storage = "_Sub_Company_Calendar_Active", DbType = "Bit")]
        public System.Nullable<bool> Sub_Company_Calendar_Active
        {
            get
            {
                return this._Sub_Company_Calendar_Active;
            }
            set
            {
                if ((this._Sub_Company_Calendar_Active != value))
                {
                    this._Sub_Company_Calendar_Active = value;
                }
            }
        }

        [Column(Storage = "_Calendar_Description", DbType = "VarChar(50)")]
        public string Calendar_Description
        {
            get
            {
                return this._Calendar_Description;
            }
            set
            {
                if ((this._Calendar_Description != value))
                {
                    this._Calendar_Description = value;
                }
            }
        }

        [Column(Storage = "_Calendar_Year_Start_Date", DbType = "VarChar(30)")]
        public string Calendar_Year_Start_Date
        {
            get
            {
                return this._Calendar_Year_Start_Date;
            }
            set
            {
                if ((this._Calendar_Year_Start_Date != value))
                {
                    this._Calendar_Year_Start_Date = value;
                }
            }
        }

        [Column(Storage = "_Calendar_Year_End_Date", DbType = "VarChar(30)")]
        public string Calendar_Year_End_Date
        {
            get
            {
                return this._Calendar_Year_End_Date;
            }
            set
            {
                if ((this._Calendar_Year_End_Date != value))
                {
                    this._Calendar_Year_End_Date = value;
                }
            }
        }

        [Column(Storage = "_Calendar_History", DbType = "VarChar(122)")]
        public string Calendar_History
        {
            get
            {
                return this._Calendar_History;
            }
            set
            {
                if ((this._Calendar_History != value))
                {
                    this._Calendar_History = value;
                }
            }
        }
    }

    public partial class rsp_GetSubCompanyCalenderResult
    {

        private System.Nullable<int> _Sub_Company_ID;

        private System.Nullable<int> _Calendar_ID;

        public rsp_GetSubCompanyCalenderResult()
        {
        }

        [Column(Storage = "_Sub_Company_ID", DbType = "Int")]
        public System.Nullable<int> Sub_Company_ID
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

        [Column(Storage = "_Calendar_ID", DbType = "Int")]
        public System.Nullable<int> Calendar_ID
        {
            get
            {
                return this._Calendar_ID;
            }
            set
            {
                if ((this._Calendar_ID != value))
                {
                    this._Calendar_ID = value;
                }
            }
        }
    }

    public partial class rsp_GetLstCalendarResult
    {

        private int _Calendar_ID;

        private string _Calendar_Description;

        private string _Calendar_Year_Start_Date;

        private string _Calendar_Year_End_Date;

        public rsp_GetLstCalendarResult()
        {
        }

        [Column(Storage = "_Calendar_ID", DbType = "Int NOT NULL")]
        public int Calendar_ID
        {
            get
            {
                return this._Calendar_ID;
            }
            set
            {
                if ((this._Calendar_ID != value))
                {
                    this._Calendar_ID = value;
                }
            }
        }

        [Column(Storage = "_Calendar_Description", DbType = "VarChar(50)")]
        public string Calendar_Description
        {
            get
            {
                return this._Calendar_Description;
            }
            set
            {
                if ((this._Calendar_Description != value))
                {
                    this._Calendar_Description = value;
                }
            }
        }

        [Column(Storage = "_Calendar_Year_Start_Date", DbType = "VarChar(30)")]
        public string Calendar_Year_Start_Date
        {
            get
            {
                return this._Calendar_Year_Start_Date;
            }
            set
            {
                if ((this._Calendar_Year_Start_Date != value))
                {
                    this._Calendar_Year_Start_Date = value;
                }
            }
        }

        [Column(Storage = "_Calendar_Year_End_Date", DbType = "VarChar(30)")]
        public string Calendar_Year_End_Date
        {
            get
            {
                return this._Calendar_Year_End_Date;
            }
            set
            {
                if ((this._Calendar_Year_End_Date != value))
                {
                    this._Calendar_Year_End_Date = value;
                }
            }
        }
    }

    public partial class rsp_GetCalendarListResult
    {

        private int _Calendar_ID;

        private string _Calendar_Description;

        private string _Calendar_Year_Start_Date;

        private string _Calendar_Year_End_Date;

        private int _IsCompleteCalendar;

        public rsp_GetCalendarListResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Calendar_ID", DbType = "Int NOT NULL")]
        public int Calendar_ID
        {
            get
            {
                return this._Calendar_ID;
            }
            set
            {
                if ((this._Calendar_ID != value))
                {
                    this._Calendar_ID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Calendar_Description", DbType = "VarChar(50)")]
        public string Calendar_Description
        {
            get
            {
                return this._Calendar_Description;
            }
            set
            {
                if ((this._Calendar_Description != value))
                {
                    this._Calendar_Description = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Calendar_Year_Start_Date", DbType = "VarChar(30)")]
        public string Calendar_Year_Start_Date
        {
            get
            {
                return this._Calendar_Year_Start_Date;
            }
            set
            {
                if ((this._Calendar_Year_Start_Date != value))
                {
                    this._Calendar_Year_Start_Date = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Calendar_Year_End_Date", DbType = "VarChar(30)")]
        public string Calendar_Year_End_Date
        {
            get
            {
                return this._Calendar_Year_End_Date;
            }
            set
            {
                if ((this._Calendar_Year_End_Date != value))
                {
                    this._Calendar_Year_End_Date = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_IsCompleteCalendar", DbType = "Int NOT NULL")]
        public int IsCompleteCalendar
        {
            get
            {
                return this._IsCompleteCalendar;
            }
            set
            {
                if ((this._IsCompleteCalendar != value))
                {
                    this._IsCompleteCalendar = value;
                }
            }
        }
    }

    public partial class rsp_GetOperatorCalResult
    {

        private int _Operator_ID;

        private System.Nullable<int> _Calendar_ID;

        private string _Operator_Name;

        private string _Operator_Address;

        private string _Operator_PostCode;

        private string _Operator_Depot_Phone;

        private string _Operator_Fax;

        private string _Operator_EMail;

        private string _Operator_Contact;

        private string _Operator_Invoice_Address;

        private string _Operator_Invoice_Postcode;

        private string _Operator_Invoice_Name;

        private string _Operator_Start_Date;

        private string _Operator_End_Date;

        private string _Operator_AMEDIS_Code;

        private string _Operator_Logo_Reference;

        private string _Operator_Account_Name;

        private string _Operator_Sort_Code;

        private string _Operator_Account_No;

        public rsp_GetOperatorCalResult()
        {
        }

        [Column(Storage = "_Operator_ID", DbType = "Int NOT NULL")]
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

        [Column(Storage = "_Calendar_ID", DbType = "Int")]
        public System.Nullable<int> Calendar_ID
        {
            get
            {
                return this._Calendar_ID;
            }
            set
            {
                if ((this._Calendar_ID != value))
                {
                    this._Calendar_ID = value;
                }
            }
        }

        [Column(Storage = "_Operator_Name", DbType = "VarChar(50)")]
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

        [Column(Storage = "_Operator_Address", DbType = "NText", UpdateCheck = UpdateCheck.Never)]
        public string Operator_Address
        {
            get
            {
                return this._Operator_Address;
            }
            set
            {
                if ((this._Operator_Address != value))
                {
                    this._Operator_Address = value;
                }
            }
        }

        [Column(Storage = "_Operator_PostCode", DbType = "VarChar(15)")]
        public string Operator_PostCode
        {
            get
            {
                return this._Operator_PostCode;
            }
            set
            {
                if ((this._Operator_PostCode != value))
                {
                    this._Operator_PostCode = value;
                }
            }
        }

        [Column(Storage = "_Operator_Depot_Phone", DbType = "VarChar(15)")]
        public string Operator_Depot_Phone
        {
            get
            {
                return this._Operator_Depot_Phone;
            }
            set
            {
                if ((this._Operator_Depot_Phone != value))
                {
                    this._Operator_Depot_Phone = value;
                }
            }
        }

        [Column(Storage = "_Operator_Fax", DbType = "VarChar(15)")]
        public string Operator_Fax
        {
            get
            {
                return this._Operator_Fax;
            }
            set
            {
                if ((this._Operator_Fax != value))
                {
                    this._Operator_Fax = value;
                }
            }
        }

        [Column(Storage = "_Operator_EMail", DbType = "VarChar(100)")]
        public string Operator_EMail
        {
            get
            {
                return this._Operator_EMail;
            }
            set
            {
                if ((this._Operator_EMail != value))
                {
                    this._Operator_EMail = value;
                }
            }
        }

        [Column(Storage = "_Operator_Contact", DbType = "VarChar(50)")]
        public string Operator_Contact
        {
            get
            {
                return this._Operator_Contact;
            }
            set
            {
                if ((this._Operator_Contact != value))
                {
                    this._Operator_Contact = value;
                }
            }
        }

        [Column(Storage = "_Operator_Invoice_Address", DbType = "NText", UpdateCheck = UpdateCheck.Never)]
        public string Operator_Invoice_Address
        {
            get
            {
                return this._Operator_Invoice_Address;
            }
            set
            {
                if ((this._Operator_Invoice_Address != value))
                {
                    this._Operator_Invoice_Address = value;
                }
            }
        }

        [Column(Storage = "_Operator_Invoice_Postcode", DbType = "VarChar(50)")]
        public string Operator_Invoice_Postcode
        {
            get
            {
                return this._Operator_Invoice_Postcode;
            }
            set
            {
                if ((this._Operator_Invoice_Postcode != value))
                {
                    this._Operator_Invoice_Postcode = value;
                }
            }
        }

        [Column(Storage = "_Operator_Invoice_Name", DbType = "VarChar(50)")]
        public string Operator_Invoice_Name
        {
            get
            {
                return this._Operator_Invoice_Name;
            }
            set
            {
                if ((this._Operator_Invoice_Name != value))
                {
                    this._Operator_Invoice_Name = value;
                }
            }
        }

        [Column(Storage = "_Operator_Start_Date", DbType = "VarChar(30)")]
        public string Operator_Start_Date
        {
            get
            {
                return this._Operator_Start_Date;
            }
            set
            {
                if ((this._Operator_Start_Date != value))
                {
                    this._Operator_Start_Date = value;
                }
            }
        }

        [Column(Storage = "_Operator_End_Date", DbType = "VarChar(30)")]
        public string Operator_End_Date
        {
            get
            {
                return this._Operator_End_Date;
            }
            set
            {
                if ((this._Operator_End_Date != value))
                {
                    this._Operator_End_Date = value;
                }
            }
        }

        [Column(Storage = "_Operator_AMEDIS_Code", DbType = "VarChar(4)")]
        public string Operator_AMEDIS_Code
        {
            get
            {
                return this._Operator_AMEDIS_Code;
            }
            set
            {
                if ((this._Operator_AMEDIS_Code != value))
                {
                    this._Operator_AMEDIS_Code = value;
                }
            }
        }

        [Column(Storage = "_Operator_Logo_Reference", DbType = "VarChar(50)")]
        public string Operator_Logo_Reference
        {
            get
            {
                return this._Operator_Logo_Reference;
            }
            set
            {
                if ((this._Operator_Logo_Reference != value))
                {
                    this._Operator_Logo_Reference = value;
                }
            }
        }

        [Column(Storage = "_Operator_Account_Name", DbType = "VarChar(32)")]
        public string Operator_Account_Name
        {
            get
            {
                return this._Operator_Account_Name;
            }
            set
            {
                if ((this._Operator_Account_Name != value))
                {
                    this._Operator_Account_Name = value;
                }
            }
        }

        [Column(Storage = "_Operator_Sort_Code", DbType = "VarChar(8)")]
        public string Operator_Sort_Code
        {
            get
            {
                return this._Operator_Sort_Code;
            }
            set
            {
                if ((this._Operator_Sort_Code != value))
                {
                    this._Operator_Sort_Code = value;
                }
            }
        }

        [Column(Storage = "_Operator_Account_No", DbType = "VarChar(12)")]
        public string Operator_Account_No
        {
            get
            {
                return this._Operator_Account_No;
            }
            set
            {
                if ((this._Operator_Account_No != value))
                {
                    this._Operator_Account_No = value;
                }
            }
        }
    }
    public partial class rsp_GetLstCalendarPeriodResult
    {

        private int _Calendar_Period_ID;

        private System.Nullable<int> _Calendar_ID;

        private System.Nullable<int> _Calendar_Period_Number;

        private string _Calendar_Period_Start_Date;

        private string _Calendar_Period_End_Date;

        public rsp_GetLstCalendarPeriodResult()
        {
        }

        [Column(Storage = "_Calendar_Period_ID", DbType = "Int NOT NULL")]
        public int Calendar_Period_ID
        {
            get
            {
                return this._Calendar_Period_ID;
            }
            set
            {
                if ((this._Calendar_Period_ID != value))
                {
                    this._Calendar_Period_ID = value;
                }
            }
        }

        [Column(Storage = "_Calendar_ID", DbType = "Int")]
        public System.Nullable<int> Calendar_ID
        {
            get
            {
                return this._Calendar_ID;
            }
            set
            {
                if ((this._Calendar_ID != value))
                {
                    this._Calendar_ID = value;
                }
            }
        }

        [Column(Storage = "_Calendar_Period_Number", DbType = "Int")]
        public System.Nullable<int> Calendar_Period_Number
        {
            get
            {
                return this._Calendar_Period_Number;
            }
            set
            {
                if ((this._Calendar_Period_Number != value))
                {
                    this._Calendar_Period_Number = value;
                }
            }
        }

        [Column(Storage = "_Calendar_Period_Start_Date", DbType = "VarChar(30)")]
        public string Calendar_Period_Start_Date
        {
            get
            {
                return this._Calendar_Period_Start_Date;
            }
            set
            {
                if ((this._Calendar_Period_Start_Date != value))
                {
                    this._Calendar_Period_Start_Date = value;
                }
            }
        }

        [Column(Storage = "_Calendar_Period_End_Date", DbType = "VarChar(30)")]
        public string Calendar_Period_End_Date
        {
            get
            {
                return this._Calendar_Period_End_Date;
            }
            set
            {
                if ((this._Calendar_Period_End_Date != value))
                {
                    this._Calendar_Period_End_Date = value;
                }
            }
        }
    }

    public partial class rsp_GetOperatorForCalendarResult
    {

        private int _Operator_ID;

        private string _Operator_Name;

        public rsp_GetOperatorForCalendarResult()
        {
        }

        [Column(Storage = "_Operator_ID", DbType = "Int NOT NULL")]
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

        [Column(Storage = "_Operator_Name", DbType = "VarChar(50)")]
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

    public partial class rsp_GetOperatorCalendarActiveResult
    {

        private int _Operator_Calendar_ID;

        private int _Calendar_ID;

        private System.Nullable<bool> _Operator_Calendar_Active;

        private string _Calendar_Description;

        private string _Calendar_Year_Start_Date;

        private string _Calendar_Year_End_Date;

        private string _Calendar_History;

        public rsp_GetOperatorCalendarActiveResult()
        {
        }

        [Column(Storage = "_Operator_Calendar_ID", DbType = "Int NOT NULL")]
        public int Operator_Calendar_ID
        {
            get
            {
                return this._Operator_Calendar_ID;
            }
            set
            {
                if ((this._Operator_Calendar_ID != value))
                {
                    this._Operator_Calendar_ID = value;
                }
            }
        }

        [Column(Storage = "_Calendar_ID", DbType = "Int NOT NULL")]
        public int Calendar_ID
        {
            get
            {
                return this._Calendar_ID;
            }
            set
            {
                if ((this._Calendar_ID != value))
                {
                    this._Calendar_ID = value;
                }
            }
        }

        [Column(Storage = "_Operator_Calendar_Active", DbType = "Bit")]
        public System.Nullable<bool> Operator_Calendar_Active
        {
            get
            {
                return this._Operator_Calendar_Active;
            }
            set
            {
                if ((this._Operator_Calendar_Active != value))
                {
                    this._Operator_Calendar_Active = value;
                }
            }
        }

        [Column(Storage = "_Calendar_Description", DbType = "VarChar(50)")]
        public string Calendar_Description
        {
            get
            {
                return this._Calendar_Description;
            }
            set
            {
                if ((this._Calendar_Description != value))
                {
                    this._Calendar_Description = value;
                }
            }
        }

        [Column(Storage = "_Calendar_Year_Start_Date", DbType = "VarChar(30)")]
        public string Calendar_Year_Start_Date
        {
            get
            {
                return this._Calendar_Year_Start_Date;
            }
            set
            {
                if ((this._Calendar_Year_Start_Date != value))
                {
                    this._Calendar_Year_Start_Date = value;
                }
            }
        }

        [Column(Storage = "_Calendar_Year_End_Date", DbType = "VarChar(30)")]
        public string Calendar_Year_End_Date
        {
            get
            {
                return this._Calendar_Year_End_Date;
            }
            set
            {
                if ((this._Calendar_Year_End_Date != value))
                {
                    this._Calendar_Year_End_Date = value;
                }
            }
        }

        [Column(Storage = "_Calendar_History", DbType = "VarChar(122)")]
        public string Calendar_History
        {
            get
            {
                return this._Calendar_History;
            }
            set
            {
                if ((this._Calendar_History != value))
                {
                    this._Calendar_History = value;
                }
            }
        }
    }
}





       
