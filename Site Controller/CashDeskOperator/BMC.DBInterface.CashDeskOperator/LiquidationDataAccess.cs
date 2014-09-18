using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.Transport;
using BMC.DataAccess;
using System.Data;
using System.Data.SqlClient;
using BMC.Common.ExceptionManagement;
using System.Data.Linq.Mapping;
using System.Data.Linq;
using System.Reflection;


namespace BMC.DBInterface.CashDeskOperator
{
    public class LiquidationDataAccess 
    {
        public LiquidationDataAccess()
        {
        }

        public List<LiquidationSummary> GetLiquidationSummaryDetails(int BatchNo)
        {
            List<LiquidationSummary> lstLiquidation = new List<LiquidationSummary>();
            SqlDataReader reader = null;
            LiquidationSummary Liquid = null;

            try
            {
                SqlParameter[] parem = new SqlParameter[1];
                parem[0] = new SqlParameter("@Batch_No", BatchNo);


                 reader = SqlHelper.ExecuteReader(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, "rsp_REPORT_SGVI_LiquidationSummary",
                     parem);

                while (reader.Read())
                {
                    Liquid = new LiquidationSummary();
                    Liquid.Date_Collected = Convert.ToDateTime(reader["Date_Collected"]);
                    Liquid.Retailer_Name = reader["Retailer_Name"].ToString();
                    Liquid.Gross = Convert.ToDecimal(reader["Gross"]);
                    Liquid.Net = Convert.ToDecimal(reader["Net"]);
                    Liquid.Net_Percentage = Convert.ToDecimal(reader["Net_Percentage"]);
                    Liquid.Percentage_Setting = Convert.ToDecimal(reader["Percentage_Setting"]);
                    Liquid.Retail_Negative_Net = Convert.ToDecimal(reader["Retail_Negative_Net"]);
                    Liquid.Retailer = Convert.ToDecimal(reader["Retailer"]);
                    Liquid.Retailer_Share = Convert.ToDecimal(reader["Retailer_Share"]);
                    Liquid.Tickets_Paid = Convert.ToDecimal(reader["Tickets_Paid"]);
                    Liquid.Tickets_Expected = Convert.ToDecimal(reader["Tickets_Expected"]);
                    Liquid.Advance_To_Retailer = Convert.ToDecimal(reader["Advance_To_Retailer"]);
                    Liquid.Balance_Due = Convert.ToDecimal(reader["Balance_Due"]);
                    lstLiquidation.Add(Liquid);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            finally
            {
                if (reader != null) reader.Dispose();
                
            }
            return lstLiquidation;
        }

        public void UpdateBatchAdvance(int BatchNo, decimal AdvanceRetailer)
        {
            SqlParameter[] parem = new SqlParameter[2];
            parem[0] = new SqlParameter("@BatchNo", BatchNo);
            parem[1] = new SqlParameter("@Collection_Batch_Advance_Value", AdvanceRetailer);

            SqlHelper.ExecuteNonQuery(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, "usp_Insert_Collection_Batch_Advance",
                parem);
        }

        public void CalculateRetailerNegative(int BatchNo)
        {
            SqlParameter[] parem = new SqlParameter[1];
            parem[0] = new SqlParameter("@Batch_No", BatchNo);

            SqlHelper.ExecuteNonQuery(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, "esp_Calculate_Batch_Negative_Net",
                parem);
        }

        public string GetSetting(string strSettingName)
        {
            SqlParameter ParamValue = new SqlParameter();
            ParamValue.ParameterName = DBConstants.CONST_SP_PARAM_SETTINGVALUE;
            ParamValue.Direction = ParameterDirection.Output;
            ParamValue.Value = string.Empty;
            ParamValue.SqlDbType = SqlDbType.VarChar;
            ParamValue.Size = 200;

            try
            {
                if (strSettingName != null)
                {
                    //DataBaseServiceHandler.ConnectionString = ExchangeConnectionString;
                        DataBaseServiceHandler.ExecuteNonQuery(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, DBConstants.CONST_SP_GETSETTING,
                        DataBaseServiceHandler.AddParameter<int>(DBConstants.CONST_SP_PARAM_SETTINGID, DbType.Int32, 0, ParameterDirection.Input),
                        DataBaseServiceHandler.AddParameter<string>(DBConstants.CONST_SP_PARAM_SETTINGNAME, DbType.String, strSettingName.Trim()),
                        DataBaseServiceHandler.AddParameter<string>(DBConstants.CONST_SP_PARAM_SETTINGDEFAULT, DbType.String, string.Empty),
                        ParamValue);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            return ParamValue.Value.ToString();
        }



        public DataTable GetBatchNoForLiquidation()
        {
            DataSet dsBatchNos = new DataSet();
            try
            {

                SqlHelper.FillDataset(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, "rsp_GetBatchNoForLiquidation", dsBatchNos, new string[] {"BatchList"});
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
              
            }
            if (dsBatchNos.Tables.Count > 0)
                return dsBatchNos.Tables[0];
            else
                return null;
        }


        
    }
}
