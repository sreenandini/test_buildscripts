using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Data;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Linq.Expressions;
using System.ComponentModel;
using System;
using BMC.DataAccess;
using System.Data.SqlClient;
using BMC.Common.ExceptionManagement;

namespace BMC.DBInterface.CashDeskOperator
{
    public class PromotionalDataAccess
    {
        public PromotionalDataAccess() { }
        CommonDataAccess commonDataAccess = new CommonDataAccess();

        #region "Private Variables"
        #endregion

        #region "Public Properties"
        #endregion

        #region "Private Function"
        #endregion

        //Insert Promotional
        public int InsertPromotionalTicket(int promTicketType, string promName, int promTotalTickets, double promAmount, DateTime promExpDate)
        {
            try
            {

                return DataBaseServiceHandler.ExecuteNonQuery(CommonDataAccess.TicketingConnectionString, CommandType.StoredProcedure, "usp_InsertPromotionalTicket",
                     DataBaseServiceHandler.AddParameter<int>("@PromotionalTicketType", DbType.Int32, promTicketType),
                     DataBaseServiceHandler.AddParameter<string>("@PromotionName", DbType.String, promName),
                     DataBaseServiceHandler.AddParameter<int>("@TotalTickets", DbType.Int32, promTotalTickets),
                     DataBaseServiceHandler.AddParameter<double>("@TicketAmount", DbType.Decimal, promAmount),
                     DataBaseServiceHandler.AddParameter<DateTime>("@ExpiryDate", DbType.DateTime, promExpDate));
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return -1;
            }
        }

        //Name Exists
        public int PromotionNameExist(String pname)
        {
            int result;
            int countVal = 0;
            try
            {
                SqlParameter[] parames = new SqlParameter[2];
                parames[0] = DataBaseServiceHandler.AddParameter<String>("@PromotionName", DbType.String, pname, ParameterDirection.Input);
                parames[1] = DataBaseServiceHandler.AddParameter<Int32>("@CheckPromotionName", DbType.Int32, countVal, ParameterDirection.Output);
                result = DataBaseServiceHandler.ExecuteNonQuery(CommonDataAccess.TicketingConnectionString, CommandType.StoredProcedure, "rsp_PromotionNameExists", parames);
                countVal = Int32.Parse(parames[1].Value.ToString());
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return countVal;
        }

        //Void Status Update

        public int UpdateVoidStatus(int TicketID,string WSName,int VoidUserID)
        {
            int result = 0;
            try
            {
                SqlParameter[] parames = new SqlParameter[3];
                parames[0] = DataBaseServiceHandler.AddParameter<int>("@PromotionID", DbType.Int32, TicketID, ParameterDirection.Input);
                parames[1] = DataBaseServiceHandler.AddParameter<String>("@WorkStation", DbType.String, WSName, ParameterDirection.Input);
                parames[2] = DataBaseServiceHandler.AddParameter<int>("@iVoucherVoidUser", DbType.Int32, VoidUserID, ParameterDirection.Input);
                result = DataBaseServiceHandler.ExecuteNonQuery(CommonDataAccess.TicketingConnectionString, CommandType.StoredProcedure, "usp_PromotionVoidUpdate", parames);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return result;
        }

        public int DBPromoTicketCount(int PromotionTicketID)
        {

            int TicketCount = 0;
            try
            {
                SqlParameter[] parames = new SqlParameter[2];
                parames[0] = DataBaseServiceHandler.AddParameter<int>("@PromotionID", DbType.Int32, PromotionTicketID, ParameterDirection.Input);

                parames[1] = DataBaseServiceHandler.AddParameter<Int32>("@PromotionTicketCount", DbType.Int32, TicketCount, ParameterDirection.Output);
                int result = DataBaseServiceHandler.ExecuteNonQuery(CommonDataAccess.TicketingConnectionString, CommandType.StoredProcedure, "rsp_GetPromotionalTicketCount", parames);
                TicketCount = Int32.Parse(parames[1].Value.ToString());
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return TicketCount;
        }
        public int DBGetPromotionalID(String PromotionalName)
        {
            int reult = 0;
            int PromotionalId = 0;
            try
            {
                SqlParameter[] parames = new SqlParameter[2];
                parames[0] = DataBaseServiceHandler.AddParameter<String>("@PromotionName", DbType.String, PromotionalName, ParameterDirection.Input);

                parames[1] = DataBaseServiceHandler.AddParameter<Int32>("@PromotionTicketID", DbType.Int32, PromotionalId, ParameterDirection.Output);
                reult = DataBaseServiceHandler.ExecuteNonQuery(CommonDataAccess.TicketingConnectionString, CommandType.StoredProcedure, "rsp_GetPromotionalID", parames);
                PromotionalId = Int32.Parse(parames[1].Value.ToString());
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return PromotionalId;
        }

         public int DBUpdatePrintStatus(int PromoTicketID,int type)
        {
            int UpdatePrintStatus = 0;
            
            try
            {
                SqlParameter[] parames = new SqlParameter[2];
                parames[0] = DataBaseServiceHandler.AddParameter<int>("@PromotionID", DbType.Int32, PromoTicketID, ParameterDirection.Input);
                parames[1] = DataBaseServiceHandler.AddParameter<int>("@type", DbType.Int32, type, ParameterDirection.Input);
                UpdatePrintStatus = DataBaseServiceHandler.ExecuteNonQuery(CommonDataAccess.TicketingConnectionString, CommandType.StoredProcedure, "usp_UpdatePromotionPrintStatus", parames); 
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return UpdatePrintStatus;
        }
        public int DBUpdateVoucherPromotion(int PromoTicketID,String Barcode)
        {
            int UpdateVoucherPromotion = 0;
            
            try
            {
                SqlParameter[] parames = new SqlParameter[2];
                parames[0] = DataBaseServiceHandler.AddParameter<int>("@PromotionID", DbType.Int32, PromoTicketID, ParameterDirection.Input);
                parames[1] = DataBaseServiceHandler.AddParameter<string>("@Barcode", DbType.String, Barcode, ParameterDirection.Input);
                UpdateVoucherPromotion = DataBaseServiceHandler.ExecuteNonQuery(CommonDataAccess.TicketingConnectionString, CommandType.StoredProcedure, "usp_UpdateVoucherPromotion", parames); 
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return UpdateVoucherPromotion;
        }
        //Cancel Print Status Update
        public int DBCancelPrintStatusUpdate(int PromoTicketID)
        {
            int RescancelPrintStatusUpdate = 0;
            
            try
            {
                SqlParameter[] parames = new SqlParameter[1];
                parames[0] = DataBaseServiceHandler.AddParameter<int>("@PromotionID", DbType.Int32, PromoTicketID, ParameterDirection.Input);

                RescancelPrintStatusUpdate = DataBaseServiceHandler.ExecuteNonQuery(CommonDataAccess.TicketingConnectionString, CommandType.StoredProcedure, "usp_CancelPrintUpdateStatus", parames); 
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return RescancelPrintStatusUpdate;
        }

        public int MarkPromotionalTicketsAsValid(int PromoTicketID, int type)
        {
            int status = 0;

            try
            {
                SqlParameter[] parames = new SqlParameter[2];
                parames[0] = DataBaseServiceHandler.AddParameter<int>("@PromotionTicketID", DbType.Int32, PromoTicketID, ParameterDirection.Input);
                parames[1] = DataBaseServiceHandler.AddParameter<int>("@Type", DbType.Int32, type, ParameterDirection.Input);

                status = DataBaseServiceHandler.ExecuteNonQuery(CommonDataAccess.TicketingConnectionString, CommandType.StoredProcedure, "usp_MarkPromotionalTicketsAsValid", parames);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return status;
        }

        public int MarkPromotionalTicketsAsInValid(int PromoTicketID, string barcode)//
        {
            int status = 0;

            try
            {
                SqlParameter[] parames = new SqlParameter[2];
                parames[0] = DataBaseServiceHandler.AddParameter<int>("@PromotionTicketID", DbType.Int32, PromoTicketID, ParameterDirection.Input);
               parames[1] = DataBaseServiceHandler.AddParameter<string>("@strBarCode", DbType.String, barcode, ParameterDirection.Input);

                status = DataBaseServiceHandler.ExecuteNonQuery(CommonDataAccess.TicketingConnectionString, CommandType.StoredProcedure, "usp_MarkPromotionalTicketsAsInvalid", parames);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return status;
        }

        public DataTable GetTISPromoDetailsDT(DateTime StartDate, DateTime EndDate,int NoOfRecords)
        {
            DataSet dsTISDetails = new DataSet();
            try
            {
                SqlParameter[] objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@StartDate", StartDate);
                objParams[1] = new SqlParameter("@EndDate", EndDate);
                objParams[2] = new SqlParameter("@NoOfRecords", NoOfRecords);

                SqlHelper.FillDataset(CommonDataAccess.TicketingConnectionString, "rsp_SelectTISPromotionDetails", dsTISDetails, new string[] { "TISDetails" }, objParams);

                if (dsTISDetails.Tables.Count > 0)
                {
                    return dsTISDetails.Tables[0];
                }
                else
                {
                    return new DataTable();
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return new DataTable();
            }
        }

        public DataTable GetPromoHistoryDetailsDT(int type, int NoOfRecords)
        {
            DataSet dsPromoHistory = new DataSet();
            try
            {
                SqlParameter[] objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter("@type", type);
                objParams[1] = new SqlParameter("@NoOfRecords", NoOfRecords);

                SqlHelper.FillDataset(CommonDataAccess.TicketingConnectionString, "rsp_SelectPromotionHistory", dsPromoHistory, new string[] { "PromoHistoryDetails" }, objParams);

                if (dsPromoHistory.Tables.Count > 0)
                {
                    return dsPromoHistory.Tables[0];
                }
                else
                {
                    return new DataTable();
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return new DataTable();
            }
        }
        
        
    }
}
