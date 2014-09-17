using BMC.CoreLib;
using BMC.CoreLib.Data;
using BMC.CoreLib.Diagnostics;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Linq;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Data.Linq.Mapping;

namespace BMC.ExComms.DataLayer.MSSQL
{

    public partial class ExCommsDataContext
    {
        public void CopySNAPMeterHistoryTo(int installation_No, string process, string type)
        {
            using (ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "CopySNAPMeterHistoryTo"))
            {
                try
                {
                    using (ExCommsSQLDataAccess DataContext = this.GetDataContext())
                    {
                        DataContext.usp_Copy_MH_Snap_To(process, type, installation_No);
                    }
                }
                catch (Exception ex)
                {
                    Log.Exception(ex);
                }
            }
        }

        public void UpdateFloorStatus(int installation_No, DateTime? systemLastUpdate,
            DateTime? meterLastUpdate, System.Nullable<int> doorStatus,
            System.Nullable<int> powerStatus,
            System.Nullable<int> ePIStatus,
            string ePIDetails,
            System.Nullable<int> datapak_Variant,
            System.Nullable<int> datapak_LastEventNo,
            System.Nullable<int> datapak_PollingStatus,
            string empCardNo,
            string empCardInTime,
            System.Nullable<int> gMU_Machine_Status)
        {
            using (ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "UpdateFloorStatus"))
            {
                try
                {
                    using (ExCommsSQLDataAccess DataContext = this.GetDataContext())
                    {
                        DataContext.usp_UpdateFloorStatus(installation_No, systemLastUpdate, meterLastUpdate,
                            doorStatus, powerStatus, ePIStatus, ePIDetails,
                            datapak_Variant, datapak_LastEventNo, datapak_PollingStatus,
                            empCardNo, empCardInTime, gMU_Machine_Status);
                    }
                }
                catch (Exception ex)
                {
                    Log.Exception(ex);
                }
            }
        }

        public bool AddFaultEvent(int Installation_No, int faultSource, int faultType, string sFaultDesc, bool bPolled, DateTime dEventDate, string empCardNo = "")
        {
            using (ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "AddFaultEvent"))
            {
                try
                {
                    using (ExCommsSQLDataAccess DataContext = this.GetDataContext())
                    {
                        return (DataContext.usp_InsertFaultEvent(Installation_No, faultSource, sFaultDesc,
                               bPolled, faultType, dEventDate, empCardNo) == 0);
                    }
                }
                catch (Exception ex)
                {
                    Log.Exception(ex);
                    return false;
                }
            }
        }

        public void DeleteGMULoginEntry(int installation_No)
        {
            using (ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "DeleteGMULoginEntry"))
            {
                try
                {
                    using (ExCommsSQLDataAccess DataContext = this.GetDataContext())
                    {
                        DataContext.usp_RemoveGMULogin(installation_No);
                    }
                }
                catch (Exception ex)
                {
                    Log.Exception(ex);
                }
            }
        }

        public int GetDenomValueByCode(int code)
        {
            int denom = 0;
            using (ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "GetDenomValueByCode"))
            {
                try
                {
                    using (ExCommsSQLDataAccess DataContext = this.GetDataContext())
                    {
                        ExCommsSQLDataAccess.rsp_GetDenomByCodeResult denom_code = DataContext.rsp_GetDenomByCode(code).SingleOrDefault();
                        if (denom_code != null)
                            denom = denom_code.Column1;
                    }
                }
                catch (Exception ex)
                {
                    Log.Exception(ex);
                }
            }
            return denom;
        }

        public void UpdateReEnrollMachineStatus(int installation_No, byte status)
        {
            using (ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "UpdateReEnrollMachineStatus"))
            {
                try
                {
                    using (ExCommsSQLDataAccess DataContext = this.GetDataContext())
                    {
                        DataContext.usp_UpdateReEnrollStatus(installation_No, status);
                    }
                }
                catch (Exception ex)
                {
                    Log.Exception(ex);
                }
            }
        }

        public void GetSASVersionFromGMULogin(int installationNo, ref string gmuVersion)
        {
            using (ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "GetSASVersionFromGMULogin"))
            {
                try
                {
                    using (ExCommsSQLDataAccess DataContext = this.GetDataContext())
                    {
                        DataContext.rsp_GetSASVersionFromGMULogin(installationNo, ref gmuVersion);
                    }
                }
                catch (Exception ex)
                {
                    Log.Exception(ex);
                }
            }
        }

        public void CheckToIncludeGameDetails(int installationNo, ref int? gameDetails)
        {
            using (ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "CheckToIncludeGameDetails"))
            {
                try
                {
                    using (ExCommsSQLDataAccess DataContext = this.GetDataContext())
                    {
                        DataContext.rsp_CheckToIncludeGameDetails(installationNo, ref gameDetails);
                    }
                }
                catch (Exception ex)
                {
                    Log.Exception(ex);
                }
            }
        }

        public void UpdateInPlayStatus(int gMUNumber, string current_Credit, ref int? errorCode)
        {
            using (ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "UpdateInPlayStatus"))
            {
                try
                {
                    using (ExCommsSQLDataAccess DataContext = this.GetDataContext())
                    {
                        DataContext.usp_UpdateInPlayStatus(gMUNumber, current_Credit, ref errorCode);
                    }
                }
                catch (Exception ex)
                {
                    Log.Exception(ex);
                }
            }
        }

        public void UpdateBaseDenomAndGamesCount(int installation_No, int denom, int games_Count, int maxBet, string percentagePayout)
        {
            using (ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "UpdateBaseDenomAndGamesCount"))
            {
                try
                {
                    using (ExCommsSQLDataAccess DataContext = this.GetDataContext())
                    {
                        DataContext.usp_UpdateBaseDenomAndGamesCount(installation_No, denom, games_Count, maxBet, percentagePayout);
                    }
                }
                catch (Exception ex)
                {
                    Log.Exception(ex);
                }
            }
        }

        public void UpdateGamesCount(int installation_No, int games_Count)
        {
            using (ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "UpdateGamesCount"))
            {
                try
                {
                    using (ExCommsSQLDataAccess DataContext = this.GetDataContext())
                    {
                        DataContext.usp_UpdateGamesCount(installation_No, games_Count);
                    }
                }
                catch (Exception ex)
                {
                    Log.Exception(ex);
                }
            }
        }

        public void UpdateInstallationGameInfo(int installation_No, int game_Number, int max_Bet, int prog_Group, int prog_Level, string game_Name, string paytables, bool isInstall, int isFramed, string game_Payout)
        {
            using (ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "UpdateInstallationGameInfo"))
            {
                try
                {
                    using (ExCommsSQLDataAccess DataContext = this.GetDataContext())
                    {
                        DataContext.usp_UpdateInstallationGameInfo(installation_No, game_Number, max_Bet, prog_Group, prog_Level, game_Name, paytables, isInstall, isFramed, game_Payout);
                    }
                }
                catch (Exception ex)
                {
                    Log.Exception(ex);
                }
            }
        }
    }

    public partial class ExCommsSQLDataAccess : System.Data.Linq.DataContext
    {
        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.usp_Copy_MH_Snap_To")]
        public int usp_Copy_MH_Snap_To([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Process", DbType = "VarChar(10)")] string process, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Type", DbType = "VarChar(1)")] string type, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Installation_No", DbType = "Int")] System.Nullable<int> installation_No)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), process, type, installation_No);
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.usp_CheckforPowerUpRamClear")]
        public int usp_CheckforPowerUpRamClear([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Installation_No", DbType = "Int")] System.Nullable<int> installation_No, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Type", DbType = "VarChar(10)")] string type)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), installation_No, type);
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.usp_UpdateFloorStatus")]
        public int usp_UpdateFloorStatus([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Installation_No", DbType = "Int")] System.Nullable<int> installation_No, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "SystemLastUpdate", DbType = "DateTime")] System.Nullable<System.DateTime> systemLastUpdate,
            [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "MeterLastUpdate", DbType = "DateTime")] System.Nullable<System.DateTime> meterLastUpdate,
            [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "DoorStatus", DbType = "Int")] System.Nullable<int> doorStatus,
            [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "PowerStatus", DbType = "Int")] System.Nullable<int> powerStatus,
            [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "EPIStatus", DbType = "Int")] System.Nullable<int> ePIStatus,
            [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "EPIDetails", DbType = "VarChar(50)")] string ePIDetails,
            [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Datapak_Variant", DbType = "Int")] System.Nullable<int> datapak_Variant,
            [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Datapak_LastEventNo", DbType = "Int")] System.Nullable<int> datapak_LastEventNo,
            [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Datapak_PollingStatus", DbType = "Int")] System.Nullable<int> datapak_PollingStatus,
            [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "EmpCardNo", DbType = "VarChar(50)")] string empCardNo,
            [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "EmpCardInTime", DbType = "VarChar(50)")] string empCardInTime,
            [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "GMU_Machine_Status", DbType = "Int")] System.Nullable<int> gMU_Machine_Status)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), installation_No, systemLastUpdate, meterLastUpdate, doorStatus, powerStatus, ePIStatus, ePIDetails, datapak_Variant, datapak_LastEventNo, datapak_PollingStatus, empCardNo, empCardInTime, gMU_Machine_Status);
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.usp_InsertFaultEvent")]
        public int usp_InsertFaultEvent([Parameter(Name = "Installation_ID", DbType = "Int")] System.Nullable<int> installation_ID, [Parameter(Name = "FaultSource", DbType = "Int")] System.Nullable<int> faultSource, [Parameter(Name = "FaultDesc", DbType = "VarChar(50)")] string faultDesc, [Parameter(Name = "Polled", DbType = "Bit")] System.Nullable<bool> polled, [Parameter(Name = "Event_Fault", DbType = "Int")] System.Nullable<int> event_Fault, [Parameter(Name = "Event_Date", DbType = "DateTime")] System.Nullable<System.DateTime> event_Date, [Parameter(Name = "EmpCardNo", DbType = "VarChar(50)")] string empCardNo)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), installation_ID, faultSource, faultDesc, polled, event_Fault, event_Date, empCardNo);
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.usp_RemoveGMULogin")]
        public int usp_RemoveGMULogin([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InstallationNo", DbType = "Int")] System.Nullable<int> installationNo)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), installationNo);
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.rsp_GetDenomByCode")]
        public ISingleResult<rsp_GetDenomByCodeResult> rsp_GetDenomByCode([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Code", DbType = "Int")] System.Nullable<int> code)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), code);
            return ((ISingleResult<rsp_GetDenomByCodeResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.usp_UpdateReEnrollStatus")]
        public int usp_UpdateReEnrollStatus([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Installation_No", DbType = "Int")] System.Nullable<int> installation_No, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Status", DbType = "TinyInt")] System.Nullable<byte> status)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), installation_No, status);
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.rsp_GetSASVersionFromGMULogin")]
        public int rsp_GetSASVersionFromGMULogin([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Installation_No", DbType = "Int")] System.Nullable<int> installation_No, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "GL_SASVersion", DbType = "VarChar(20)")] ref string gL_SASVersion)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), installation_No, gL_SASVersion);
            gL_SASVersion = ((string)(result.GetParameterValue(1)));
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.rsp_CheckToIncludeGameDetails")]
        public int rsp_CheckToIncludeGameDetails([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InstallationNo", DbType = "Int")] System.Nullable<int> installationNo, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "GetGameDetails", DbType = "Int")] ref System.Nullable<int> getGameDetails)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), installationNo, getGameDetails);
            getGameDetails = ((System.Nullable<int>)(result.GetParameterValue(1)));
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.usp_UpdateInPlayStatus")]
        public int usp_UpdateInPlayStatus([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "GMUNumber", DbType = "Int")] System.Nullable<int> gMUNumber, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Current_Credit", DbType = "VarChar(50)")] string current_Credit, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ErrorCode", DbType = "Int")] ref System.Nullable<int> errorCode)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), gMUNumber, current_Credit, errorCode);
            errorCode = ((System.Nullable<int>)(result.GetParameterValue(2)));
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.usp_UpdateBaseDenomAndGamesCount")]
        public int usp_UpdateBaseDenomAndGamesCount([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Installation_No", DbType = "Int")] System.Nullable<int> installation_No, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Denom", DbType = "Int")] System.Nullable<int> denom, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Games_Count", DbType = "Int")] System.Nullable<int> games_Count, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "MaxBet", DbType = "Int")] System.Nullable<int> maxBet, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "PercentagePayout", DbType = "VarChar(10)")] string percentagePayout)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), installation_No, denom, games_Count, maxBet, percentagePayout);
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.usp_UpdateGamesCount")]
        public int usp_UpdateGamesCount([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Installation_No", DbType = "Int")] System.Nullable<int> installation_No, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Games_Count", DbType = "Int")] System.Nullable<int> games_Count)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), installation_No, games_Count);
            return ((int)(result.ReturnValue));
        }

        //[global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.usp_UpdateInstallationGameInfo")]
        //public int usp_UpdateInstallationGameInfo([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Installation_No", DbType = "Int")] System.Nullable<int> installation_No, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Game_Number", DbType = "Int")] System.Nullable<int> game_Number, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Max_Bet", DbType = "Int")] System.Nullable<int> max_Bet, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Prog_Group", DbType = "Int")] System.Nullable<int> prog_Group, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Prog_Level", DbType = "Int")] System.Nullable<int> prog_Level, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Game_Name", DbType = "VarChar(100)")] string game_Name, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Paytables", DbType = "VarChar(1000)")] string paytables, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "IsInstall", DbType = "Bit")] System.Nullable<bool> isInstall, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "IsFramed", DbType = "Int")] System.Nullable<int> isFramed, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Game_Payout", DbType = "VarChar(50)")] string game_Payout)
        //{
        //    IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), installation_No, game_Number, max_Bet, prog_Group, prog_Level, game_Name, paytables, isInstall, isFramed, game_Payout);
        //    return ((int)(result.ReturnValue));
        //}

        public partial class rsp_GetDenomByCodeResult
        {

            private int _Column1;

            public rsp_GetDenomByCodeResult()
            {
            }

            [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "", Storage = "_Column1", DbType = "Int NOT NULL")]
            public int Column1
            {
                get
                {
                    return this._Column1;
                }
                set
                {
                    if ((this._Column1 != value))
                    {
                        this._Column1 = value;
                    }
                }
            }
        }
    }


}
