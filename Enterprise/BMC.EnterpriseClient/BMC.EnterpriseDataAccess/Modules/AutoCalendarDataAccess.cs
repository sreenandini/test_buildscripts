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
    public partial class EnterpriseDataContext
    {
        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.rsp_AC_GetAutoCalendarProfiles")]
        public ISingleResult<rsp_AC_GetAutoCalendarProfilesResult> GetAutoCalendarProfiles()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<rsp_AC_GetAutoCalendarProfilesResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.rsp_AC_GetAutoCalendarProfilesDetails")]
        [ResultType(typeof(rsp_AC_GetAutoCalendarProfilesDetailsResult))]
        [ResultType(typeof(rsp_AC_GetAutoCalendarSubCompanyDetailsResult))]
        public IMultipleResults GetAutoCalendarProfilesDetails([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "AutoCalendarProfile_ID", DbType = "Int")] System.Nullable<int> autoCalendarProfile_ID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), autoCalendarProfile_ID);
            return ((IMultipleResults)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.rsp_AC_VerifyAutoCalendarProfiles")]
        public int VerifyAutoCalendarProfiles([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "AutoCalendarProfile_Name", DbType = "VarChar(20)")] string autoCalendarProfile_Name, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "AutoCalendarProfile_ID", DbType = "Int")] System.Nullable<int> autoCalendarProfile_ID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), autoCalendarProfile_Name, autoCalendarProfile_ID);
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.usp_AC_UpdateAutoCalendarProfiles")]
        public int UpdateAutoCalendarProfiles([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "AutoCalendarProfile_ID", DbType = "Int")] System.Nullable<int> autoCalendarProfile_ID,
                                              [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "AutoCalendarProfile_Name", DbType = "VarChar(20)")] string autoCalendarProfile_Name,
                                              [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "IsAutoCalendarEnabled", DbType = "Bit")] System.Nullable<bool> isAutoCalendarEnabled,
                                              [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "CalendarCreateBeforeDays", DbType = "Int")] System.Nullable<int> calendarCreateBeforeDays,
                                              [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "CalendarAlertBefore", DbType = "Int")] System.Nullable<int> calendarAlertBefore,
                                              [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "CalendarAlertRecurrence", DbType = "Int")] System.Nullable<int> calendarAlertRecurrence,
                                              [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "IsCalendarBasedOnDays", DbType = "Bit")] System.Nullable<bool> isCalendarBasedOnDays,
                                              [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "NewCalendarDayID", DbType = "Int")] System.Nullable<int> newCalendarDayID,
                                              [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "SetNewCalendarActive", DbType = "Bit")] bool setNewCalendarActive,
                                              [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "AssignProfiles", DbType = "VarChar(2000)")] string assignProfiles,
                                              [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "OperationType", DbType = "VarChar(20)")] string operationType)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), autoCalendarProfile_ID, autoCalendarProfile_Name, isAutoCalendarEnabled, calendarCreateBeforeDays, calendarAlertBefore, calendarAlertRecurrence, isCalendarBasedOnDays, newCalendarDayID, setNewCalendarActive, assignProfiles, operationType);
            return ((int)(result.ReturnValue));
        }
    }

    public partial class rsp_AC_GetAutoCalendarProfilesResult
    {

        private int _AutoCalendarProfile_ID;

        private string _AutoCalendarProfile_Name;

        public rsp_AC_GetAutoCalendarProfilesResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_AutoCalendarProfile_ID", DbType = "Int NOT NULL")]
        public int AutoCalendarProfile_ID
        {
            get
            {
                return this._AutoCalendarProfile_ID;
            }
            set
            {
                if ((this._AutoCalendarProfile_ID != value))
                {
                    this._AutoCalendarProfile_ID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_AutoCalendarProfile_Name", DbType = "VarChar(20) NOT NULL", CanBeNull = false)]
        public string AutoCalendarProfile_Name
        {
            get
            {
                return this._AutoCalendarProfile_Name;
            }
            set
            {
                if ((this._AutoCalendarProfile_Name != value))
                {
                    this._AutoCalendarProfile_Name = value;
                }
            }
        }
    }

    public partial class rsp_AC_GetAutoCalendarProfilesDetailsResult
    {
        private int _AutoCalendarProfile_ID;

        private string _AutoCalendarProfile_Name;

        private System.Nullable<bool> _IsAutoCalendarEnabled;

        private int _CalendarCreateBeforeDays;

        private int _CalendarAlertBefore;

        private int _CalendarAlertRecurrence;

        private System.Nullable<int> _Sub_Company_ID;

        private string _Sub_Company_Name;

        private int _Profilestatus;

        private System.Nullable<bool> _IsCalendarBasedOnDays;

        private int _NewCalendarDayID;

        private bool _SetNewCalendarActive;

        public rsp_AC_GetAutoCalendarProfilesDetailsResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_AutoCalendarProfile_ID", DbType = "Int NOT NULL")]
        public int AutoCalendarProfile_ID
        {
            get
            {
                return this._AutoCalendarProfile_ID;
            }
            set
            {
                if ((this._AutoCalendarProfile_ID != value))
                {
                    this._AutoCalendarProfile_ID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_AutoCalendarProfile_Name", DbType = "VarChar(20) NOT NULL", CanBeNull = false)]
        public string AutoCalendarProfile_Name
        {
            get
            {
                return this._AutoCalendarProfile_Name;
            }
            set
            {
                if ((this._AutoCalendarProfile_Name != value))
                {
                    this._AutoCalendarProfile_Name = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_IsAutoCalendarEnabled", DbType = "Bit")]
        public System.Nullable<bool> IsAutoCalendarEnabled
        {
            get
            {
                return this._IsAutoCalendarEnabled;
            }
            set
            {
                if ((this._IsAutoCalendarEnabled != value))
                {
                    this._IsAutoCalendarEnabled = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CalendarCreateBeforeDays", DbType = "Int NOT NULL")]
        public int CalendarCreateBeforeDays
        {
            get
            {
                return this._CalendarCreateBeforeDays;
            }
            set
            {
                if ((this._CalendarCreateBeforeDays != value))
                {
                    this._CalendarCreateBeforeDays = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CalendarAlertBefore", DbType = "Int NOT NULL")]
        public int CalendarAlertBefore
        {
            get
            {
                return this._CalendarAlertBefore;
            }
            set
            {
                if ((this._CalendarAlertBefore != value))
                {
                    this._CalendarAlertBefore = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CalendarAlertRecurrence", DbType = "Int NOT NULL")]
        public int CalendarAlertRecurrence
        {
            get
            {
                return this._CalendarAlertRecurrence;
            }
            set
            {
                if ((this._CalendarAlertRecurrence != value))
                {
                    this._CalendarAlertRecurrence = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Sub_Company_ID", DbType = "Int")]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Profilestatus", DbType = "Int")]
        public int Profilestatus
        {
            get
            {
                return this._Profilestatus;
            }
            set
            {
                if ((this._Profilestatus != value))
                {
                    this._Profilestatus = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_IsCalendarBasedOnDays", DbType = "Bit")]
        public System.Nullable<bool> IsCalendarBasedOnDays
        {
            get
            {
                return this._IsCalendarBasedOnDays;
            }
            set
            {
                if ((this._IsCalendarBasedOnDays != value))
                {
                    this._IsCalendarBasedOnDays = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_NewCalendarDayID", DbType = "Int")]
        public int NewCalendarDayID
        {
            get
            {
                return this._NewCalendarDayID;
            }
            set
            {
                if ((this._NewCalendarDayID != value))
                {
                    this._NewCalendarDayID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_SetNewCalendarActive", DbType = "Bit")]
        public bool SetNewCalendarActive
        {
            get
            {
                return this._SetNewCalendarActive;
            }
            set
            {
                if ((this._SetNewCalendarActive != value))
                {
                    this._SetNewCalendarActive = value;
                }
            }
        }
    }

    public partial class rsp_AC_GetAutoCalendarSubCompanyDetailsResult
    {
        private System.Nullable<int> _Sub_Company_ID;

        private string _Sub_Company_Name;

        private int _Profilestatus;

        public rsp_AC_GetAutoCalendarSubCompanyDetailsResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Sub_Company_ID", DbType = "Int")]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Profilestatus", DbType = "Int")]
        public int Profilestatus
        {
            get
            {
                return this._Profilestatus;
            }
            set
            {
                if ((this._Profilestatus != value))
                {
                    this._Profilestatus = value;
                }
            }
        }
    }
}
