using BMC.CoreLib;
using BMC.CoreLib.Diagnostics;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Data.Linq.Mapping;

namespace BMC.ExComms.DataLayer.MSSQL
{
    public partial class ExCommsDataContext
    {
        public bool CreateDoorEvent(int? installationNo, int faultType, bool polled, DateTime eventDate)
        {
            bool bRet = false;

            try
            {
                using (ExCommsSQLDataAccess DataContext = this.GetDataContext())
                {
                    DataContext.InsertDoorEvent(installationNo, faultType, polled, eventDate);
                    bRet = true;
                }
            }
            catch (Exception ex)
            {
                bRet = false;
                Log.Exception(ex);
            }

            return bRet;
        }

        public bool UpdateFloorStatus(int? installationNo,  DateTime EventDate, int? Status)
        {
            bool bRet = false;

            try
            {
                using (ExCommsSQLDataAccess DataContext = this.GetDataContext())
                {
                    DataContext.UpdateFloorStatus(installationNo, EventDate, null,Status,null,null,"",null,null,null,"","",null);
                    bRet = true;
                }
            }
            catch (Exception ex)
            {
                bRet = false;
                Log.Exception(ex);
            }

            return bRet;
        }

        public bool CreateFaultEvent(int installationNo,int faultSource,int faultType , string faulDescription, bool polled, DateTime eventDate,string employeeCardNo)
        {
            bool bRet = false;

            try
            {
                using (ExCommsSQLDataAccess DataContext = this.GetDataContext())
                {
                    DataContext.InsertFaultEvent(installationNo, faultSource, faulDescription , polled, null ,eventDate, employeeCardNo);
                    bRet = true;
                }
            }
            catch (Exception ex)
            {
                bRet = false;
                Log.Exception(ex);
            }

            return bRet;
        }

        public bool CreatePowerEvent(int installation_ID,int faultType,bool polled,DateTime eventDate)
        {
            bool bRet = false;

            try
            {
                using (ExCommsSQLDataAccess DataContext = this.GetDataContext())
                {
                    DataContext.usp_InsertPowerEvent(installation_ID, faultType, polled, eventDate);
                    bRet = true;
                }
            }
            catch (Exception ex)
            {
                bRet = false;
                Log.Exception(ex);
            }

            return bRet;
        }

        public bool CheckforPowerUpRamClear(int installationNo,string type)
        {
            bool bRet = false;

            try
            {
                using (ExCommsSQLDataAccess DataContext = this.GetDataContext())
                {
                    DataContext.CheckforPowerUpRamClear(installationNo, type);
                    bRet = true;
                }
            }
            catch (Exception ex)
            {
                bRet = false;
                Log.Exception(ex);
            }

            return bRet;
        }

        public bool UpdateGMUSiteCodeStatus(int installationNo, int status)
        {
            bool bRet = false;

            try
            {
                using (ExCommsSQLDataAccess DataContext = this.GetDataContext())
                {
                    DataContext.UpdateGMUSiteCodeStatus(installationNo, status);
                    bRet = true;
                }
            }
            catch (Exception ex)
            {
                bRet = false;
                Log.Exception(ex);
            }

            return bRet;
        }

        public bool InsertGeneralEvents(int installationNo,int faultType,bool polled,DateTime eventDate,int faultSource)
        {
            bool bRet = false;

            try
            {
                using (ExCommsSQLDataAccess DataContext = this.GetDataContext())
                {
                    DataContext.InsertGeneralEvents(installationNo, faultType, polled, eventDate, faultSource);
                    bRet = true;
                }
            }
            catch (Exception ex)
            {
                bRet = false;
                Log.Exception(ex);
            }

            return bRet;
        }

        public bool UpdateFloorFinancialSession(int? installationNo, string type, string voucherID)
        {
            bool bRet = false;

            try
            {
                using (ExCommsSQLDataAccess DataContext = this.GetDataContext())
                {
                    DataContext.usp_UpdateFloorFinancialSession(installationNo, type, voucherID);
                    bRet = true;
                }
            } 
            catch (Exception ex)
            {
                bRet = false;
                Log.Exception(ex); 
            }

            return bRet;
        }

        public EPIDetailsResult GetEPIDetails(int installationNo)
        {
            EPIDetailsResult epiDetailsResult = null;
            try
            {
                using (ExCommsSQLDataAccess DataContext = this.GetDataContext())
                {
                    epiDetailsResult = DataContext.GetEPIDetails(installationNo).FirstOrDefault();
                }
                return epiDetailsResult;
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
                return new EPIDetailsResult();
            }
        }
    }

    public partial class ExCommsSQLDataAccess : System.Data.Linq.DataContext
    {
        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.usp_InsertDoorEvent")]
        public void InsertDoorEvent([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Installation_ID", DbType = "Int")] System.Nullable<int> installation_ID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "FaultType", DbType = "Int")] System.Nullable<int> faultType, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Polled", DbType = "Bit")] System.Nullable<bool> polled, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Event_Date", DbType = "DateTime")] System.Nullable<System.DateTime> event_Date)
        {
            this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), installation_ID, faultType, polled, event_Date);
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.usp_UpdateFloorStatus")]
        public int UpdateFloorStatus([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Installation_No", DbType = "Int")] System.Nullable<int> installation_No, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "SystemLastUpdate", DbType = "DateTime")] System.Nullable<System.DateTime> systemLastUpdate, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "MeterLastUpdate", DbType = "DateTime")] System.Nullable<System.DateTime> meterLastUpdate, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "DoorStatus", DbType = "Int")] System.Nullable<int> doorStatus, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "PowerStatus", DbType = "Int")] System.Nullable<int> powerStatus, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "EPIStatus", DbType = "Int")] System.Nullable<int> ePIStatus, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "EPIDetails", DbType = "VarChar(50)")] string ePIDetails, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Datapak_Variant", DbType = "Int")] System.Nullable<int> datapak_Variant, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Datapak_LastEventNo", DbType = "Int")] System.Nullable<int> datapak_LastEventNo, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Datapak_PollingStatus", DbType = "Int")] System.Nullable<int> datapak_PollingStatus, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "EmpCardNo", DbType = "VarChar(50)")] string empCardNo, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "EmpCardInTime", DbType = "VarChar(50)")] string empCardInTime, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "GMU_Machine_Status", DbType = "Int")] System.Nullable<int> gMU_Machine_Status)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), installation_No, systemLastUpdate, meterLastUpdate, doorStatus, powerStatus, ePIStatus, ePIDetails, datapak_Variant, datapak_LastEventNo, datapak_PollingStatus, empCardNo, empCardInTime, gMU_Machine_Status);
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.usp_InsertFaultEvent")]
        public void InsertFaultEvent([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Installation_ID", DbType = "Int")] System.Nullable<int> installation_ID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "FaultSource", DbType = "Int")] System.Nullable<int> faultSource, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "FaultDesc", DbType = "VarChar(50)")] string faultDesc, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Polled", DbType = "Bit")] System.Nullable<bool> polled, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Event_Fault", DbType = "Int")] System.Nullable<int> event_Fault, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Event_Date", DbType = "DateTime")] System.Nullable<System.DateTime> event_Date, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "EmpCardNo", DbType = "VarChar(50)")] string empCardNo)
        {
            this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), installation_ID, faultSource, faultDesc, polled, event_Fault, event_Date, empCardNo);
        }

        [Function(Name = "dbo.usp_InsertPowerEvent")]
        public int usp_InsertPowerEvent([Parameter(Name = "Installation_ID", DbType = "Int")] System.Nullable<int> installation_ID, [Parameter(Name = "FaultType", DbType = "Int")] System.Nullable<int> faultType, [Parameter(Name = "Polled", DbType = "Bit")] System.Nullable<bool> polled, [Parameter(Name = "Event_Date", DbType = "DateTime")] System.Nullable<System.DateTime> event_Date)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), installation_ID, faultType, polled, event_Date);
            return ((int)(result.ReturnValue));
        }
        
        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.usp_CheckforPowerUpRamClear")]
        public int CheckforPowerUpRamClear([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Installation_No", DbType = "Int")] System.Nullable<int> installation_No, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Type", DbType = "VarChar(10)")] string type)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), installation_No, type);
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.usp_UpdateGMUSiteCodeStatus")]
        public int UpdateGMUSiteCodeStatus([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InstallationNo", DbType = "Int")] System.Nullable<int> installationNo, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Status", DbType = "Int")] System.Nullable<int> status)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), installationNo, status);
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.usp_InsertGeneralEvents")]
        public int InsertGeneralEvents([Parameter(Name = "Installation_ID", DbType = "Int")] System.Nullable<int> installation_ID, [Parameter(Name = "FaultType", DbType = "Int")] System.Nullable<int> faultType, [Parameter(Name = "Polled", DbType = "Bit")] System.Nullable<bool> polled, [Parameter(Name = "Event_Date", DbType = "DateTime")] System.Nullable<System.DateTime> event_Date, [Parameter(Name = "FaultSource", DbType = "Int")] System.Nullable<int> faultSource)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), installation_ID, faultType, polled, event_Date, faultSource);
            return ((int)(result.ReturnValue));
        }
    }

    public partial class usp_UpdateFloorFinancialSessionResult
    {
        private string _Setting_Value;

        public usp_UpdateFloorFinancialSessionResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Setting_Value", DbType = "VarChar(8000)")]
        public string Setting_Value
        {
            get
            {
                return this._Setting_Value;
            }
            set
            {
                if ((this._Setting_Value != value))
                {
                    this._Setting_Value = value;
                }
            }
        }
    }
}
