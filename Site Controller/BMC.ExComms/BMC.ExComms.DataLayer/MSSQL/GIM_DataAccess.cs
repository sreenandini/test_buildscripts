using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.ExComms.Contracts.DTO.Monitor;
using BMC.Common.ExceptionManagement;
using System.Data.Linq;
using System.Reflection;
using System.Data.Linq.Mapping;
using BMC.CoreLib;

namespace BMC.ExComms.DataLayer.MSSQL
{
    public partial class ExCommsDataContext 
    {
        public bool InsertGMULogin(MonTgt_G2H_GIM_GameIDInfo monTgt_Msg, string iPAddress, ref int? installationNo, ref int? assetNo, ref string gamePrefix)
        {
            int? status = 0;
            try
            {
                using (ExCommsSQLDataAccess DataContext = this.GetDataContext())
                {
                    DataContext.usp_InsertAndGet_GMU_Login(null,
                                                        iPAddress,
                                                        monTgt_Msg.MACAddress,
                                                        monTgt_Msg.AssetNumber,
                                                        monTgt_Msg.GMUNumber,
                                                        monTgt_Msg.SerialNumber,
                                                        monTgt_Msg.SASVersion,
                                                        monTgt_Msg.GMUVersion,
                                                        ref installationNo,
                                                        ref status,
                                                        ref assetNo,
                                                        ref gamePrefix);
                    Log.Info("InsertGMULogin: Asset No : " + assetNo.ToString() + ", Poker Game Prefix : " + gamePrefix);
                    return true;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return false;
            }
        }
    }

    public partial class ExCommsSQLDataAccess : System.Data.Linq.DataContext
    {
        [Function(Name = "dbo.usp_InsertAndGet_GMU_Login")]
        public int usp_InsertAndGet_GMU_Login([Parameter(Name = "GL_Code", DbType = "Int")] System.Nullable<int> gL_Code, [Parameter(Name = "GL_IP", DbType = "VarChar(20)")] string gL_IP, [Parameter(Name = "GL_MAC", DbType = "VarChar(20)")] string gL_MAC, [Parameter(Name = "GL_Asset_No", DbType = "VarChar(5)")] string gL_Asset_No, [Parameter(Name = "GL_GMU_No", DbType = "VarChar(5)")] string gL_GMU_No, [Parameter(Name = "GL_Machine_Serial_No", DbType = "VarChar(50)")] string gL_Machine_Serial_No, [Parameter(Name = "GL_SAS_Version", DbType = "VarChar(20)")] string gL_SAS_Version, [Parameter(Name = "GL_GMU_Version", DbType = "VarChar(200)")] string gL_GMU_Version, [Parameter(Name = "Installation_No", DbType = "Int")] ref System.Nullable<int> installation_No, [Parameter(Name = "Status", DbType = "Int")] ref System.Nullable<int> status, [Parameter(Name = "Asset_No_Int", DbType = "Int")] ref System.Nullable<int> asset_No_Int, [Parameter(Name = "PokerGamePrefix", DbType = "VarChar(150)")] ref string pokerGamePrefix)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), gL_Code, gL_IP, gL_MAC, gL_Asset_No, gL_GMU_No, gL_Machine_Serial_No, gL_SAS_Version, gL_GMU_Version, installation_No, status, asset_No_Int, pokerGamePrefix);
            installation_No = ((System.Nullable<int>)(result.GetParameterValue(8)));
            status = ((System.Nullable<int>)(result.GetParameterValue(9)));
            asset_No_Int = ((System.Nullable<int>)(result.GetParameterValue(10)));
            pokerGamePrefix = ((string)(result.GetParameterValue(11)));
            return ((int)(result.ReturnValue));
        }
    }
}
