using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using BMC.DataAccess;
using BMC.Common.ExceptionManagement;
using BMC.Common;

namespace BMC.DBInterface.CashDeskOperator
{
    public class FieldServiceDataAccess
    {
        #region "Private Variables"
        #endregion

        #region "Public Properties"
        #endregion

        #region "Private Function"
        #endregion

        #region "Public Function"

        public string GetCurrentSiteCode()
        {
            string SiteCode = string.Empty;
            if (CommonDataAccess.SiteDetail.Tables.Count > 0 && CommonDataAccess.SiteDetail.Tables[0].Rows.Count > 0)
                foreach (DataRow dr in CommonDataAccess.SiteDetail.Tables[0].Rows)
                    SiteCode = Common.TypeHandler.GetRowValue<string>(dr, "Code");

            return SiteCode;
        }

        public string GetCurrentBarPositionNames()
        {
            DataTable CurrentServiceCalls = new DataTable();
            string BarPositionNames = string.Empty;
            
            try
            {
                //DataBaseServiceHandler.ConnectionString = CommonDataAccess.ExchangeConnectionString;
                CurrentServiceCalls = DataBaseServiceHandler.Fill(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, DBConstants.CONST_SP_RSP_GETCURRENTBARPOSITIONNAMES, CurrentServiceCalls,
                    DataBaseServiceHandler.AddParameter<string>(DBConstants.CONST_PARAM_SITE_CODE, DbType.String, GetCurrentSiteCode()),
                    DataBaseServiceHandler.AddParameter<int>("@RETURN_VALUE", DbType.Int32, 0, ParameterDirection.InputOutput));

                foreach (DataRow dr in CurrentServiceCalls.Rows)                    
                    BarPositionNames += Common.TypeHandler.GetRowValue<string>(dr,"Bar_Pos_Name");
               
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return BarPositionNames;
        }

        public int GetCurrentBarPosCount()
        {
            int BarPositionCount = 0;

            try
            {
                //DataBaseServiceHandler.ConnectionString = CommonDataAccess.ExchangeConnectionString;
                BarPositionCount = DataBaseServiceHandler.ExecuteScalar<int>(CommonDataAccess.ExchangeConnectionString, CommandType.Text, "SELECT COUNT(*) FROM Bar_Position WHERE End_Date IS NULL");                
            }
            catch (Exception ex)
            {                
                ExceptionManager.Publish(ex);                
            }
            return BarPositionCount;
        }

        public string PrepareCashDeskEvent(int InstallationNumber, int FaultType)
        {
            //DataBaseServiceHandler.ConnectionString = CommonDataAccess.ExchangeConnectionString;
            try
            {
                return DataBaseServiceHandler.ExecuteScalar<string>(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, DBConstants.CONST_SP_RSP_CASHDESKEVENT,
                            DataBaseServiceHandler.AddParameter<int>("@InstallationNo", DbType.Int32, InstallationNumber),
                            DataBaseServiceHandler.AddParameter<int>("@FaultType", DbType.Int32, FaultType));
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
            return string.Empty;
        }

        public DataTable GetPositionList()
        {
            try
            {
                return SqlHelper.ExecuteDataset(CommonDataAccess.ExchangeConnectionString, 
                    CommandType.StoredProcedure, 
                    DBConstants.CONST_SP_RSP_GETCURRENTBARPOSITIONNAMES).Tables[0];
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                return null;
            }
        }

        #endregion

        #region "Public Static Function"
        #endregion

    }
}
