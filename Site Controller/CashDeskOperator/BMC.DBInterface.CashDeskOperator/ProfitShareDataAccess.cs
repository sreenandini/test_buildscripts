using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using BMC.DataAccess;
using BMC.Common.Utilities;
using BMC.Common.LogManagement;
namespace BMC.DBInterface.CashDeskOperator
{
    public class ProfitShareDataAccess
    {
        public DataTable GetProfitShareGroupList()
        {
            try
            {
                string GetProfitShareGroupNameList = "rsp_GetProfitShareGroupNameList";
                DataTable dtPSgroupList = new DataTable();
                dtPSgroupList = DataBaseServiceHandler.Fill(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, GetProfitShareGroupNameList, dtPSgroupList);
                return dtPSgroupList;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog(ex.Message, LogManager.enumLogLevel.Error);
                return null;
            }

        }
        public DataTable GetExpenseShareGroupList()
        {
            try
            {
                string GetExpenseShareGroupNameList = "rsp_GetExpenseShareGroupNameList";
                DataTable dtESgroupList = new DataTable();
                dtESgroupList = DataBaseServiceHandler.Fill(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, GetExpenseShareGroupNameList, dtESgroupList);
                return dtESgroupList;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog(ex.Message, LogManager.enumLogLevel.Error);
                return null;
            }

        }
        public DataTable GetPayPeriodList()
        {
            try
            {
                string GetPayPeriodList = "rsp_GetPayPeriods";
                DataTable dtPPList = new DataTable();
                dtPPList = DataBaseServiceHandler.Fill(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, GetPayPeriodList, dtPPList);
                return dtPPList;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog(ex.Message, LogManager.enumLogLevel.Error);
                return null;
            }

        }
       
    }
}
