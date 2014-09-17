using BMC.CoreLib;
using BMC.CoreLib.Diagnostics;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Reflection;
using System.Text;

namespace BMC.ExComms.DataLayer.MSSQL
{

    public partial class ExCommsDataContext
    {
        public void UpdateSession_MGMD_MeterHistory(string doc)
        {
            using (ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "UpdateSession_MGMD_MeterHistory"))
            {
                Log.Info(PROC, "Update MGMD session data");

                try
                {
                    using (ExCommsSQLDataAccess DataContext = this.GetDataContext())
                    {
                        //DataContext.usp_UpdateSession_MGMD_MeterHistory(doc);
                    }
                }
                catch (Exception ex)
                {
                    Log.Info(PROC, "Update MGMD session data Failed");
                    Log.Exception(ex);
                }
            }
        }

        public void UpdateInstallationGameInfo(int installation_No, int? game_Number, int? max_Bet, int? prog_Group, int? prog_Level,
        string game_Name, string paytables, bool? isInstall, int? isFramed, string game_Payout)
        {
            using (ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "UpdateInstallationGameInfo"))
            {
                Log.Info(PROC, "Update Installation Game Info");

                try
                {
                    using (ExCommsSQLDataAccess DataContext = this.GetDataContext())
                    {
                        DataContext.usp_UpdateInstallationGameInfo(installation_No, game_Number, max_Bet, prog_Group, prog_Level, game_Name, paytables, isInstall, isFramed, game_Payout);
                    }
                }
                catch (Exception ex)
                {
                    Log.Info(PROC, "Update Installation Game Info Failed");
                    Log.Exception(ex);
                }
            }
        }
    }

    public partial class ExCommsSQLDataAccess : System.Data.Linq.DataContext
    {
        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.usp_UpdateSession_MGMD_MeterHistory")]
        public int? usp_UpdateSession_MGMD_MeterHistory([global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "VarChar(8000)")] string doc)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), doc);
            return ((int?)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.usp_UpdateInstallationGameInfo")]
        public int usp_UpdateInstallationGameInfo([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Installation_No", DbType = "Int")] System.Nullable<int> installation_No,
            [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Game_Number", DbType = "Int")] System.Nullable<int> game_Number,
            [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Max_Bet", DbType = "Int")] System.Nullable<int> max_Bet,
            [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Prog_Group", DbType = "Int")] System.Nullable<int> prog_Group,
            [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Prog_Level", DbType = "Int")] System.Nullable<int> prog_Level,
            [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Game_Name", DbType = "VarChar(100)")] string game_Name,
            [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Paytables", DbType = "VarChar(1000)")] string paytables,
            [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "IsInstall", DbType = "Bit")] System.Nullable<bool> isInstall,
            [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "IsFramed", DbType = "Int")] System.Nullable<int> isFramed,
            [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Game_Payout", DbType = "VarChar(50)")] string game_Payout)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), installation_No, game_Number, max_Bet, prog_Group, prog_Level, game_Name, paytables, isInstall, isFramed, game_Payout);
            return ((int)(result.ReturnValue));
        }
    }
}
