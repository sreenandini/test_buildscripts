using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.EnterpriseDataAccess;
using BMC.EnterpriseBusiness.Entities;
using BMC.Common.ExceptionManagement;
using BMC.CoreLib;
namespace BMC.EnterpriseBusiness.Business
{
    public class ViewSites_DropBatchBreakDown 
    {
        public List<CollectionEvents> GetCollectionEvents(int collection_ID)
        {
            List<CollectionEvents> listResult = null;
            List<rsp_ecGetCollectionEventsResult> lstEvents;
            using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
            {
                lstEvents = DataContext.GetCollectionEvents(collection_ID).ToList();
            }

            listResult = (from evnt in lstEvents
                          select new CollectionEvents
                          {
                              Event_ID = evnt.Event_ID,
                              Description = evnt.Description,
                              Duration = evnt.Duration.Value,
                              EVENT_Time = evnt.EVENT_Time,
                              Event_Date = evnt.Event_Date,
                              Type = evnt.Type
                          }).ToList();

            return listResult;
        }

        public List<DropBatchBreakDown> GetDropBatchBreakDown(int site_ID, int batch_ID)
        {
            
          
            List<DropBatchBreakDown> listResult = null;
            List<rsp_ecGetDropBatchBreakDownResult> lstEvents;
            using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
            {
                lstEvents = DataContext.GetDropBatchBreakDown(site_ID, batch_ID).ToList();
            }

            listResult = (from Coll in lstEvents
                         select new DropBatchBreakDown
                         {

                             IsSAS= Coll.IsSAS.Value,

                             Collection_ID= Coll.Collection_ID,

                             Installation_ID= Coll.Installation_ID,

                             Zone_Name= Coll.Zone_Name,

                             PosName= Coll.PosName,

                             MachineName= Coll.MachineName,

                             StockNo= Coll.StockNo,

                             Collection_Days= Coll.Collection_Days,
                           
                             DecWinOrLoss= Coll.DecWinOrLoss.Value,

                             MeterWinOrLoss= Coll.MeterWinOrLoss.Value,

                             TakeVariance= Coll.TakeVariance.Value,

                             Handle= Coll.Handle.Value,

                             nCasino= Coll.nCasino.Value,

                             nHold= Coll.nHold.Value,

                             Declared_Coins= Coll.Declared_Coins,

                             DeFloat= Coll.DeFloat,

                             Refills= Coll.Refills,

                             Refunds= Coll.Refunds.Value,

                             Net_Coin= Coll.Net_Coin.Value,

                             Datapak_ID= Coll.Datapak_ID.Value,

                             RDC_Coins= Coll.RDC_Coins,

                             RDC_Notes = Coll.RDC_Notes,

                             VTP= Coll.VTP,

                             Collection_EDC_Status = Coll.Collection_EDC_Status,

                             Coin_Var = Coll.Coin_Var.Value,

                             Declared_Notes = Coll.Declared_Notes,

                             

                             Note_Var = Coll.Note_Var,

                             DecTicketBalance= Coll.DecTicketBalance.Value,

                             Shortpay= Coll.Shortpay.Value,

                             Void= Coll.Void.Value,

                             RDCTickets= Coll.RDCTickets.Value,

                             Ticket_Var = Coll.Ticket_Var.Value,

                             DecHandpay= Coll.DecHandpay.Value,

                             RDCHandpay= Coll.RDCHandpay.Value,

                             Handpay_Var = Coll.Handpay_Var.Value,

                             Progressive_Value_Declared = Coll.Progressive_Value_Declared.Value,

                             Progressive_Value_Meter = Coll.Progressive_Value_Meter.Value,

                             Progressive_Value_Variance = Coll.Progressive_Value_Variance.Value,

                             Collection_Total_Power_Duration = Coll.Collection_Total_Power_Duration,

                             Total_Fault_Events = Coll.Total_Fault_Events.Value,

                             Total_Door_Events = Coll.Total_Door_Events.Value,

                             Total_Power_Events = Coll.Total_Power_Events.Value,

                             PromoCashableIn=Coll.PromoCashableIn,

                             PromoNonCashableIn=Coll.PromoNonCashableIn
                         }).ToList();


            return listResult;
        }
        public FillBatchSummary FillBatchSummary(int batch_ID)
        {
            List<FillBatchSummary> listResult = null;
            List<rsp_FillBatchSummaryResult> lstBatchSummary;
             using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
            {
                lstBatchSummary = DataContext.FillBatchSummary(batch_ID).ToList();
            }

             listResult = (from item in lstBatchSummary
                          select new FillBatchSummary
                          {
                              Batch_ID = item.Batch_ID,

                              BatchRef = item.BatchRef,

                              BatchDate = item.BatchDate,

                              Batch_Date_Performed = item.Batch_Date_Performed,

                              Batch_Time = item.Batch_Time,

                              Batch_User_Name = item.Batch_User_Name,

                              Declared_Coins = item.Declared_Coins.Value,

                              Defloat = item.Defloat.Value,

                              Refills = item.Refills.Value,

                              RDC_Notes = item.RDC_Notes.Value,

                              Refunds = item.Refunds.Value,

                              Progressive_Value_Declared = item.Progressive_Value_Declared.Value,

                              Shortpay = item.Shortpay.Value,

                              Declared_Notes = item.Declared_Notes.Value,

                              DecTicketBalance = item.DecTicketBalance.Value,

                              DecHandpay = item.DecHandpay.Value,

                              Net_Coin = item.Net_Coin.Value,

                              Ticket_Balance = item.Ticket_Balance.Value,

                              Ticket_Var = item.Ticket_Var.Value,

                              RDCHandpay = item.RDCHandpay.Value,

                              RDC_Coins = item.RDC_Coins.Value,

                              RDC_Coins_Out = item.RDC_Coins_Out.Value,

                              Handpay_Var = item.Handpay_Var.Value,

                              Coin_Var = item.Coin_Var.Value,

                              Note_Var = item.Note_Var.Value,

                              Progressive_Value_Variance = item.Progressive_Value_Variance.Value,

                              PercentageIn = item.PercentageIn.Value,

                              PercentageOut = item.PercentageOut.Value,

                              Handle = item.Handle.Value,

                              BatchCount = item.BatchCount.Value,

                              EftIn = item.EftIn.Value,

                              DecEftIn = item.DecEftIn.Value,

                              EftOut = item.EftOut.Value,

                              DecEftOut = item.DecEftOut.Value,

                             IsAFTIncluded = item.IsAFTIncluded,


                              nCasino = item.nCasino.SafeValue(),

                              nHold = 100 - item.nCasino.SafeValue(),

                              DecWinOrLoss = item.DecWinOrLoss.SafeValue(),

                              MeterWinOrLoss = item.MeterWinOrLoss.SafeValue(),
                              RouteName =item.RouteName


                          }).ToList();

                
                    return listResult[0];

        }

        public List<DropBatchBreakDown> GetDropWeekBreakdown(int site_ID, int Week_ID)
        {
            List<DropBatchBreakDown> listResult = null;
            List<rsp_ecGetDropBatchBreakDownResult> lstEvents;
            using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
            {
                lstEvents = DataContext.GetDropWeekBreakdown(site_ID, Week_ID).ToList();
            }

            listResult = (from Coll in lstEvents
                          select new DropBatchBreakDown
                          {

                              IsSAS = Coll.IsSAS.Value,

                              Collection_ID = Coll.Collection_ID,

                              Installation_ID = Coll.Installation_ID,

                              Zone_Name = Coll.Zone_Name,

                              PosName = Coll.PosName,

                              MachineName = Coll.MachineName,

                              StockNo = Coll.StockNo,

                              Collection_Days = Coll.Collection_Days,

                              DecWinOrLoss = Coll.DecWinOrLoss.Value,

                              MeterWinOrLoss = Coll.MeterWinOrLoss.Value,

                              TakeVariance = Coll.TakeVariance.Value,

                              Handle = Coll.Handle.Value,

                              nCasino = Coll.nCasino.Value,

                              nHold = Coll.nHold.Value,

                              Declared_Coins = Coll.Declared_Coins,

                              DeFloat = Coll.DeFloat,

                              Refills = Coll.Refills,

                              Refunds = Coll.Refunds.Value,

                              Net_Coin = Coll.Net_Coin.Value,

                              Datapak_ID = Coll.Datapak_ID.Value,

                              RDC_Coins = Coll.RDC_Coins,

                              RDC_Notes = Coll.RDC_Notes,

                              VTP = Coll.VTP,

                              Collection_EDC_Status = Coll.Collection_EDC_Status,

                              Coin_Var = Coll.Coin_Var.Value,

                              Declared_Notes = Coll.Declared_Notes,



                              Note_Var = Coll.Note_Var,

                              DecTicketBalance = Coll.DecTicketBalance.Value,

                              Shortpay = Coll.Shortpay.Value,

                              Void = Coll.Void.Value,

                              RDCTickets = Coll.RDCTickets.Value,

                              Ticket_Var = Coll.Ticket_Var.Value,

                              DecHandpay = Coll.DecHandpay.Value,

                              RDCHandpay = Coll.RDCHandpay.Value,

                              Handpay_Var = Coll.Handpay_Var.Value,

                              Progressive_Value_Declared = Coll.Progressive_Value_Declared.Value,

                              Progressive_Value_Meter = Coll.Progressive_Value_Meter.Value,

                              Progressive_Value_Variance = Coll.Progressive_Value_Variance.Value,

                              Collection_Total_Power_Duration = Coll.Collection_Total_Power_Duration,

                              Total_Fault_Events = Coll.Total_Fault_Events.Value,

                              Total_Door_Events = Coll.Total_Door_Events.Value,

                              Total_Power_Events = Coll.Total_Power_Events.Value,

                              PromoCashableIn=Coll.PromoCashableIn,

                              PromoNonCashableIn=Coll.PromoNonCashableIn
                          }).ToList();


            return listResult;
        }
        public CollsWeekDetailSummary FillWeekSummary( int Site_ID,int Week_Id)
        {
            List<CollsWeekDetailSummary> listResult = null;
            List<rsp_GetSiteViewerCollsWeekDetailResult> lstBatchSummary;
            using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
            {
                try
                {
                    lstBatchSummary = DataContext.GetSiteViewerCollsWeekDetail(Week_Id, Site_ID).ToList();
                }
                catch (Exception Ex)
                {
                    
                    throw Ex;
                }
            }

            listResult = (from item in lstBatchSummary
                          select new CollsWeekDetailSummary
                          {
                              Week_ID= item.Week_ID.Value,
                              WeekCount=item.WeekCount.Value,
                              WeekNumber=item.WeekNumber.Value,
                              Batch_ID = item.Batch_ID.Value,
                              StartDate= item.StartDate,
                              EndDate= item.EndDate,
                              BatchRef = item.BatchRef,

                              BatchDate = item.BatchDate,

                              //Batch_Date_Performed = item.Batch_Date_Performed,

                              Batch_Time = item.Batch_Time,

                              Batch_User_Name = item.Batch_User_Name,

                              Declared_Coins = item.Declared_Coins.Value,

                              Defloat = item.Defloat.Value,

                              Refills = item.Refills.Value,

                              RDC_Notes = item.RDC_Notes.Value,

                              Refunds = item.Refunds.Value,

                              Progressive_Value_Declared = item.Progressive_Value_Declared.Value,

                              Shortpay = item.Shortpay.Value,

                              Declared_Notes = item.Declared_Notes.Value,

                              DecTicketBalance = item.DecTicketBalance.Value,

                              DecHandpay = item.DecHandpay.Value,

                              Net_Coin = item.Net_Coin.Value,

                              Ticket_Balance = item.Ticket_Balance.Value,

                              Ticket_Var = item.Ticket_Var.Value,

                              RDCHandpay = item.RDCHandpay.Value,

                              RDC_Coins = item.RDC_Coins.Value,

                              RDC_Coins_Out = item.RDC_Coins_Out.Value,

                              Handpay_Var = item.Handpay_Var.Value,

                              Coin_Var = item.Coin_Var.Value,

                              Note_Var = item.Note_Var.Value,

                              Progressive_Value_Variance = item.Progressive_Value_Variance.Value,

                              PercentageIn = item.PercentageIn.Value,

                              PercentageOut = item.PercentageOut.Value,

                              Handle = item.Handle.Value,

                              BatchCount = item.BatchCount.Value,

                              EftIn = item.EftIn.Value,

                              DecEftIn = item.DecEftIn.Value,

                              EftOut = item.EftOut.Value,

                              DecEftOut = item.DecEftOut.Value,

                              IsAFTIncluded = item.IsAFTIncluded.Value,
                              
                              nCasino   =   item.nCasino.SafeValue(),
                              
                              nHold = 100 - item.nCasino.SafeValue(), 

                              DecWinOrLoss=item.DecWinOrLoss.SafeValue(),

                              MeterWinOrLoss = item.MeterWinOrLoss.SafeValue()

                          }).ToList();

                      return listResult[0];
        }
    }
}
