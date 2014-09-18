using System;
using System.Xml;
using BMC.Common.ExceptionManagement;
using BMC.Common.LogManagement;
using BMC.Common.Utilities;
using BMC.DBInterface.CashDeskOperator;
using TCKPrint;
using BMC.Transport.CashDeskOperatorEntity;
using BMC.Transport;
using Gen2PrinterLib;
using System.Windows;
using System.Data;
using System.Collections.Generic;
using System.Data.Linq;
namespace BMC.Business.CashDeskOperator
{
    public  class Promotional
    {
        CommonDataAccess commonDataAccess = new CommonDataAccess();
        PromotionalDataAccess objDBPromotional = new PromotionalDataAccess();
        LinqDataAccessDataContext context = new LinqDataAccessDataContext(CommonUtilities.TicketConnectionString);

        //Insert Promotional

        public int BInsertPromotionalTicket(int promTicketType, string promName, int promTotalTickets, double promAmount, DateTime promExpDate)
        {
            int resultDBInsertPromotional = 0;
            try
            {
                double PromAmount = double.Parse((promAmount * 100).ToString());
                resultDBInsertPromotional = objDBPromotional.InsertPromotionalTicket(promTicketType, promName, promTotalTickets, PromAmount, promExpDate);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("BInsertPromotionalTicket : " + ex.Message, LogManager.enumLogLevel.Error);               
            }
            return resultDBInsertPromotional;
        }

        //NameExists

        public int BPromotionNameExist(String pname)
        {
            int count = 0;
            try
            {
                count = objDBPromotional.PromotionNameExist(pname);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("BPromotionNameExist : " + ex.Message, LogManager.enumLogLevel.Error);
            }
            return count;
        }

        //Promotional History

        public ISingleResult<PromotionalClass> BGetPromoHistory(int type,int NoofRecords)
        {
            try
            {
                return context.GetPromoHistory(type, NoofRecords);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("BGetPromoHistory : " + ex.Message, LogManager.enumLogLevel.Error);
                return null;
            }
        }

        //Promotional History Details

        public ISingleResult<PromotionalClassHistoryDetails> BGetPromoHistoryDetails(int RowSelectedTicketId)
        {
            try
            {
                return context.GetPromoHistoryDetails(RowSelectedTicketId);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("BGetPromoHistoryDetails : " + ex.Message, LogManager.enumLogLevel.Error);
                return null;
            }

        }

      //  Promotional Void Details
        public ISingleResult<PromotionalClassVoidDetails> BGetPromoVoidDetails(int voidtype,int NoofRecords)
        {
            try
            {
                return context.GetPromoVoidDetails(voidtype, NoofRecords);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("BGetPromoVoidDetails : " + ex.Message, LogManager.enumLogLevel.Error);
                return null;
            }

        }
        //Void Status Update
        public int BVoidStatusUpdate(int TicketID,string WSName,int VoidUserID)
        {
            int resultDBUpdateVoidStatus = 0;
            try
            {
                resultDBUpdateVoidStatus = objDBPromotional.UpdateVoidStatus(TicketID,WSName,VoidUserID);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("BVoidStatusUpdate : " + ex.Message, LogManager.enumLogLevel.Error);
            }
            return resultDBUpdateVoidStatus;
        }

        //Promotional ticket print
        public int BPromotionalTicketCount(int PromoTicketID)
        {
            int PromoTicketCount = 0;
            try
            {
                PromoTicketCount = objDBPromotional.DBPromoTicketCount(PromoTicketID);
            }
            catch(Exception ex)
            {
                LogManager.WriteLog("BPromotionTicketCount : " + ex.Message, LogManager.enumLogLevel.Error);
            }
            return PromoTicketCount;
        }

        public int BGetPromotionTicketID(String PromotionalName)
        {
            int PromoTicketID = 0;
            try 
            {
                PromoTicketID = objDBPromotional.DBGetPromotionalID(PromotionalName);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("BGetPromotionTicketID : " + ex.Message, LogManager.enumLogLevel.Error);
            }
            return PromoTicketID;
        }

        public int BUpdatePrintStatus(int PromotionTicketID,int type)
        {
            int UpdatePrintStatus = 0;
            try
            {
                UpdatePrintStatus = objDBPromotional.DBUpdatePrintStatus(PromotionTicketID,type);

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("BUpdatePrintStatus : " + ex.Message, LogManager.enumLogLevel.Error);
            }
            return UpdatePrintStatus;
        }


        public int BUpdateVoucherPromotion(int PromotionTicketID,String Barcode)
        {
            int UpdatePrintStatus = 0;
            try
            {
                UpdatePrintStatus = objDBPromotional.DBUpdateVoucherPromotion(PromotionTicketID, Barcode);

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("BUpdateVoucherPromotion : " + ex.Message, LogManager.enumLogLevel.Error);
            }
            return UpdatePrintStatus;
        }
        //Cancel Print Status Update
        public int BCancelPrintStatusUpdate(int PromotionTicketID)
        {
            int ResCancelPrintStatus = 0;
            try
            {
                ResCancelPrintStatus = objDBPromotional.DBCancelPrintStatusUpdate(PromotionTicketID);

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("BCancelPrintStatusUpdate : " + ex.Message, LogManager.enumLogLevel.Error);
            }
            return ResCancelPrintStatus;
        }
        
        public int MarkPromotionalTicketsAsValid(int PromotionTicketID, int type)
        {
            int status = 0;
            try
            {
                status = objDBPromotional.MarkPromotionalTicketsAsValid(PromotionTicketID, type);

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("MarkPromotionalTicketsAsValid : " + ex.Message, LogManager.enumLogLevel.Error);
            }
            return status;
        }

        public int MarkPromotionalTicketsAsInValid(int PromotionTicketID, string barcode)
        {
            int status = 0;
            try
            {
                status = objDBPromotional.MarkPromotionalTicketsAsInValid(PromotionTicketID, barcode);

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("MarkPromotionalTicketsAsInValid : " + ex.Message, LogManager.enumLogLevel.Error);
            }
            return status;
        }
      //TIS Promotional Ticket Details
        public ISingleResult<TISPromotionalClassDetails> BGetTISPromoDetails(DateTime StartDate,DateTime EndDate)
        {
            try
            {
                return context.GetTISPromoDetails(StartDate,EndDate);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("BGetTISPromoDetails : " + ex.Message, LogManager.enumLogLevel.Error);
                return null;
            }

        }

        public ISingleResult<VoucherInformation> GetVoucherInfo(string BarCode)
        {
            LinqDataAccessDataContext context = new LinqDataAccessDataContext(CommonDataAccess.TicketingConnectionString);
            try
            {
                return context.GetVoucherDetails(BarCode);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("GetVoucherDetails : " + ex.Message, LogManager.enumLogLevel.Error);
                return null;
            }

        }

        public DataTable BGetTISPromoDetailsDT(DateTime StartDate, DateTime EndDate,int NoOfRecords)
        {
            try
            {
                return objDBPromotional.GetTISPromoDetailsDT(StartDate, EndDate,NoOfRecords);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return null;
            }

           
            
        
        }

        public DataTable BGetPromoHistoryDetailsDT(int type, int NoOfRecords)
        {
            try
            {
                return objDBPromotional.GetPromoHistoryDetailsDT(type, NoOfRecords);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return null;
            }




        }
    }
}
