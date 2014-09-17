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
    public class ViewSites_DropBreakdown
    {
        public ViewSites_DropBreakdown()
        {

        }

        public CollectionBreakDown CollectionBreakDown(int collection_ID, int site_id)
        {
            CollectionBreakDown objCollectionBreakDown = null;
            List<rsp_ecCollectionBreakDownResult> Lst_CollectionDetails;
            using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
            {
                Lst_CollectionDetails = DataContext.rsp_ecCollectionBreakDown(collection_ID, site_id).ToList();
            }
            objCollectionBreakDown = (from obj in Lst_CollectionDetails
                                      select new CollectionBreakDown()
                                      {

                                          Batch_User_Name = obj.Batch_User_Name,
                                          Batch_Date = obj.Batch_Date,
                                          Batch_Time = string.Format("{0:00}:{1:00}", TimeSpan.Parse(obj.Batch_Time).Hours, TimeSpan.Parse(obj.Batch_Time).Minutes),
                                          Batch_Date_Performed = obj.Batch_Date_Performed,
                                          Batch_Ref = obj.Batch_Ref,
                                          Collection_Date_Of_Collection = obj.Collection_Date_Of_Collection,
                                          Collection_Defloat_Collection = obj.Collection_Defloat_Collection,
                                          Bar_Position_Name = obj.Bar_Position_Name,
                                          Installation_Id=obj.Installation_Id,
                                          Installation_Price_Per_Play = obj.Installation_Price_Per_Play.SafeValue(),
                                          Installation_Counter_Cash_In_Units = obj.Installation_Counter_Cash_In_Units,
                                          Installation_Counter_Cash_Out_Units = obj.Installation_Counter_Cash_Out_Units,
                                          Installation_Counter_Refill_Units = obj.Installation_Counter_Refill_Units,
                                          Machine_Class_SP_Features = obj.Machine_Class_SP_Features,
                                          Machine_Name = obj.Machine_Name,
                                          GameName = obj.GameName,
                                          Zone_Name = obj.Zone_Name,
                                          Collection_Treasury_Handpay_float = obj.Collection_Treasury_Handpay_float.SafeValue(),
                                          nCoinsOut = obj.nCoinsOut.SafeValue(),
                                          RDCCash = obj.RDCCash.SafeValue(),
                                          RDCCashIn = obj.RDCCashIn.SafeValue(),
                                          RDCCashOut = obj.RDCCashOut.SafeValue(),
                                          Collections = obj.Collections.SafeValue(),
                                          CashCollected = obj.CashCollected,
                                          Cash_Collected_100000p = obj.Cash_Collected_100000p.SafeValue(),
                                          Cash_Collected_50000p = obj.Cash_Collected_50000p.SafeValue(),
                                          Cash_Collected_20000p = obj.Cash_Collected_20000p.SafeValue(),
                                          Cash_Collected_10000p = obj.Cash_Collected_10000p.SafeValue(),
                                          Cash_Collected_5000p = obj.Cash_Collected_5000p.SafeValue(),
                                          Cash_Collected_2000p = obj.Cash_Collected_2000p.SafeValue(),
                                          Cash_Collected_1000p = obj.Cash_Collected_1000p.SafeValue(),
                                          Cash_Collected_500p = obj.Cash_Collected_500p.SafeValue(),
                                          Cash_Collected_200p = obj.Cash_Collected_200p.SafeValue(),
                                          Cash_Collected_100p = obj.Cash_Collected_100p.SafeValue(),
                                          COL_COINSIN = obj.COL_COINSIN.SafeValue(),
                                          DeclaredTicketValue = obj.DeclaredTicketValue,
                                          COL_TICKETSOUT = obj.COL_TICKETSOUT.SafeValue(),
                                          COL_TICKETS = obj.COL_TICKETS.SafeValue(),
                                          COL_PROG = obj.COL_PROG,
                                          COL_EFTOUT = obj.COL_EFTOUT.SafeValue(),
                                          COL_EFT = obj.COL_EFT.SafeValue(),
                                          FR_COL_TOTAL = obj.FR_COL_TOTAL,
                                          FR_COL_20 = obj.FR_COL_20,
                                          FR_COL_10 = obj.FR_COL_10,
                                          FR_COL_5 = obj.FR_COL_5,
                                          FR_COL_2 = obj.FR_COL_2,
                                          FR_COL_1 = obj.FR_COL_1,
                                          FR_COL_TOTALCOINS = obj.FR_COL_TOTALCOINS,
                                          Refills = obj.Refills.SafeValue(),
                                          RF_COL_1000 = obj.RF_COL_1000.SafeValue(),
                                          RF_COL_500 = obj.RF_COL_500.SafeValue(),
                                          RF_COL_200 = obj.RF_COL_200.SafeValue(),
                                          RF_COL_100 = obj.RF_COL_100.SafeValue(),
                                          RF_COL_50 = obj.RF_COL_50.SafeValue(),
                                          RF_COL_20 = obj.RF_COL_20.SafeValue(),
                                          RF_COL_10 = obj.RF_COL_10.SafeValue(),
                                          RF_COL_5 = obj.RF_COL_5.SafeValue(),
                                          Short_Pay = obj.Short_Pay.SafeValue(),
                                          TicketVoid = obj.TicketVoid.SafeValue(),
                                          RDC_COL_1000 = obj.RDC_COL_1000.SafeValue(),
                                          COL_500 = obj.COL_500.SafeValue(),
                                          COL_200 = obj.COL_200.SafeValue(),
                                          COL_100 = obj.COL_100.SafeValue(),
                                          COL_50 = obj.COL_50.SafeValue(),
                                          COL_20 = obj.COL_20.SafeValue(),
                                          COL_10 = obj.COL_10.SafeValue(),
                                          COL_5 = obj.COL_5.SafeValue(),
                                          RDC__COL_2 = obj.RDC__COL_2.SafeValue(),
                                          RDC_COL_1 = obj.RDC_COL_1.SafeValue(),
                                          RDC_COL_TOTALCOINS = obj.RDC_COL_TOTALCOINS.SafeValue(),
                                          RDC_COL_COINSIN = obj.RDC_COL_COINSIN.SafeValue(),
                                          RDC_COL_TICKETSIN = obj.RDC_COL_TICKETSIN.SafeValue(),
                                          RDC_COL_TICKETSOUT = obj.RDC_COL_TICKETSOUT.SafeValue(),
                                          RDC_COL_TICKETS = obj.RDC_COL_TICKETS.SafeValue(),
                                          RDC_COL_HANDPAY = obj.RDC_COL_HANDPAY.SafeValue(),
                                          RDC_COL_PROG = obj.RDC_COL_PROG.SafeValue(),
                                          RDC_COL_EFTIN = obj.RDC_COL_EFTIN.SafeValue(),
                                          RDC_COL_EFTOUT = obj.RDC_COL_EFTOUT.SafeValue(),
                                          RDC_COL_EFT = obj.RDC_COL_EFT.SafeValue(),
                                          CASH_IN_1P = obj.CASH_IN_1P.SafeValue(),
                                          CASH_OUT_1P = obj.CASH_OUT_1P.SafeValue(),
                                          Setting_SVGIEnabled = obj.Setting_SVGIEnabled,
                                          Setting_Region = obj.Setting_Region,
                                          Setting_AddShortpay = obj.Setting_AddShortpay,
                                          Setting_Auto_Declare_Monies = obj.Setting_Auto_Declare_Monies,
                                          Setting_IsAFTIncludedInCalculation = obj.Setting_IsAFTIncludedInCalculation,
                                          COL_PromoCashableIn = obj.COL_PromoCashableIn.SafeValue(),
                                          COL_PromoNonCashableIn = obj.COL_PromoNonCashableIn.SafeValue(),
                                          RDC_COL_PromoCashableIn = obj.RDC_COL_PromoCashableIn.SafeValue(),
                                          RDC_COL_PromoNonCashableIn = obj.RDC_COL_PromoNonCashableIn.SafeValue(),
                                          Asset = obj.Asset,
                                          Declaredby = obj.DeclaredBy
                                         

                                      }).Single();

            return objCollectionBreakDown;
        }

        public List<AssetVarianceHistory> GetAssetVarianceHistory(int installation_id, int rows)
        {
            List<AssetVarianceHistory> lstRetAssetVarianceHistory = null;
            try
            {
                List<rsp_AssetVarianceHistoryResult> lstAssetVarianceHistory;
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    lstAssetVarianceHistory = DataContext.GetAssetVarianceHistory(installation_id, rows).ToList();
                }

                lstRetAssetVarianceHistory = (from Records in lstAssetVarianceHistory
                                              select new AssetVarianceHistory
                                              {
                                                  Collection_Day = Records.collection_day,
                                                  Gaming_Day = Records.gaming_day,
                                                  Coin_Var = Records.coin_Var,
                                                  EftIn_Var = Records.EftIn_var,
                                                  EftOut_Var = Records.EftOut_var,
                                                  Handpay_Var = Records.handpay_var,
                                                  Note_Var = Records.note_var,
                                                  Progressive_Var = Records.Progressive_Var,
                                                  Ticket_In_Var = Records.ticket_in_var,
                                                  Ticket_Out_Var = Records.ticket_out_var,
                                                  Total_Var = Records.Total_Var,

                                              }).ToList<AssetVarianceHistory>();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstRetAssetVarianceHistory;
        }

        public List<TreasuryDetails> GetTreasuryDetails(int Collection_ID)
        {
            List<TreasuryDetails> lstRetTreasuryDetails = null;
            try
            {
                List<rsp_GetTreasuryDetailsResult> lstTreasuryDetails;
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    lstTreasuryDetails = DataContext.GetTreasuryDetails(Collection_ID).ToList();
                }
                
                lstRetTreasuryDetails = (from Records in lstTreasuryDetails
                                         select new TreasuryDetails
                                         {
                                             Treasury_Date = Records.Treasury_Date,
                                             Treasury_Time = string.Format("{0:00}:{1:00}", TimeSpan.Parse(Records.Treasury_Time).Hours, TimeSpan.Parse(Records.Treasury_Time).Minutes),
                                             Treasury_Type = Records.Treasury_Type,
                                             Treasury_User = Records.Treasury_User,
                                             Treasury_Issued_User = Records.Treasury_Issued_User,
                                             Treasury_Amount = Records.Treasury_Amount,
                                             Treasury_Reason = Records.Treasury_Reason
                                         }).ToList<TreasuryDetails>();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstRetTreasuryDetails;
        }

        public List<DeclaredCollection> GetCollectionToEdit(int Collection_ID,int Site_ID)
        {
            List<DeclaredCollection> lstCollection = null;
            try
            {
                List<rsp_ecGetDeclaredCollectionResult> lstCollectionsrc;
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    lstCollectionsrc = DataContext.GetDeclaredCollection(Collection_ID, Site_ID).ToList();
                }

                lstCollection = (from Records in lstCollectionsrc
                                         select new DeclaredCollection
                                         {
                                             Batch_ID= Records.Batch_ID,
                                             Cash_Collected_10000p=Records.Cash_Collected_10000p.SafeValue(),
                                             Cash_Collected_1000p = Records.Cash_Collected_1000p.SafeValue(),
                                             Cash_Collected_100p = Records.Cash_Collected_100p.SafeValue(),
                                             Cash_Collected_20000p = Records.Cash_Collected_20000p.SafeValue(),
                                             Cash_Collected_2000p = Records.Cash_Collected_2000p.SafeValue(),
                                             Cash_Collected_50000p = Records.Cash_Collected_50000p.SafeValue(),
                                             Cash_Collected_5000p = Records.Cash_Collected_5000p.SafeValue(),
                                             Cash_Collected_500p = Records.Cash_Collected_500p.SafeValue(),
                                             Cash_Collected_200p = Records.Cash_Collected_200p.SafeValue(),
                                             DecHandpay = Records.DecHandpay.SafeValue(),
                                             Declared_Tickets = Records.Declared_Tickets,
                                             Hand_Pay = Records.Hand_Pay.SafeValue(),
                                             Progressive_Value_Declared = Records.Progressive_Value_Declared.SafeValue(),
                                             Site_ID = Records.Site_ID,
                                             Tickets_Printed = Records.Tickets_Printed,
                                             Region=Records.Region
                                         }).ToList<DeclaredCollection>();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstCollection;
        }

        public void EditCollection(int collection_ID, decimal bill1, decimal bill2, decimal bill5, decimal bill10, decimal bill20, decimal bill50, decimal bill100, decimal ticket_In, decimal ticket_Out, decimal handpay, decimal progressive)
        {
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    DataContext.EditCollection(collection_ID, bill1,bill2, bill5, bill10, bill20, bill50, bill100, ticket_In, ticket_Out, handpay, progressive);
                }

        }

        public void Auditusers(string audittime, string sitecode, int collectionBatch, string username, string logmessage)
        {
            using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
            {
                DataContext.Auditusers(audittime, sitecode, collectionBatch, username, logmessage);
            }

        }



        #region Not used
        //public List<CollectionBreakDowndetailsResult> CollectionBreakDowndetails(int collection_id)
        //{
        //    CollectionBreakDowndetailsResult Lst_CollectionDetailsResponse = null;
        //    List<rsp_ecCollectionBreakDowndetailsResult> Lst_CollectionDetails;
        //    using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
        //    {
        //        Lst_CollectionDetails = DataContext.rsp_ecCollectionBreakDowndetails(collection_id).ToList();
        //    }

        //    Lst_CollectionDetailsResponse = (from obj in Lst_CollectionDetails
        //                             select new CollectionBreakDowndetailsResult()
        //                             {
        //                                 PrivUserName = obj.Batch_User_Name,
        //                                 Batch_Date = obj.Batch_Date,
        //                                 Batch_Time = obj.Batch_Time,
        //                                 Batch_Date_Performed = obj.Batch_Date_Performed,
        //                                 Batch_Ref = obj.Batch_Ref,
        //                                 PrivPositionName = obj.Bar_Position_Name,
        //                                 Installation_Price_Per_Play = obj.Installation_Price_Per_Play.SafeValue(),
        //                                 Installation_Counter_Cash_In_Units = obj.Installation_Counter_Cash_In_Units,
        //                                 Installation_Counter_Cash_Out_Units = obj.Installation_Counter_Cash_Out_Units,
        //                                 Installation_Counter_Refill_Units = obj.Installation_Counter_Refill_Units,
        //                                 PrivZoneName = obj.Zone_Name,
        //                                 Machine_Class_SP_Features = obj.Machine_Class_SP_Features.SafeValue(),
        //                                 Machine_Name = obj.Machine_Name,
        //                                 PrivMachineName = obj.GameName,
        //                                 CashCollected = obj.CashCollected,
        //                                 Collection_Treasury_Defloat = obj.Collection_Treasury_Defloat,
        //                                 CashRefills = obj.CashRefills,
        //                                 Collection_Sundries_Unsupported = obj.Collection_Sundries_Unsupported,
        //                                 Collection_Sundries = obj.Collection_Sundries,
        //                                 CASH_IN_2P = obj.CASH_IN_2P,
        //                                 CASH_IN_5P = obj.CASH_IN_5P,
        //                                 CASH_IN_10P = obj.CASH_IN_10P,
        //                                 CASH_IN_20P = obj.CASH_IN_20P,
        //                                 CASH_IN_50P = obj.CASH_IN_50P,
        //                                 CASH_IN_100P = obj.CASH_IN_100P,
        //                                 CASH_IN_200P = obj.CASH_IN_200P,
        //                                 CASH_IN_500P = obj.CASH_IN_500P,
        //                                 CASH_IN_1000P = obj.CASH_IN_1000P,
        //                                 CASH_IN_2000P = obj.CASH_IN_2000P,
        //                                 CASH_IN_5000P = obj.CASH_IN_5000P,
        //                                 CASH_IN_10000P = obj.CASH_IN_10000P,
        //                                 CASH_IN_20000P = obj.CASH_IN_20000P,
        //                                 CASH_IN_50000P = obj.CASH_IN_50000P,
        //                                 CASH_IN_100000P = obj.CASH_IN_100000P,
        //                                 CASH_OUT_2P = obj.CASH_OUT_2P,
        //                                 CASH_OUT_5P = obj.CASH_OUT_5P,
        //                                 CASH_OUT_10P = obj.CASH_OUT_10P,
        //                                 CASH_OUT_20P = obj.CASH_OUT_20P,
        //                                 CASH_OUT_50P = obj.CASH_OUT_50P,
        //                                 CASH_OUT_100P = obj.CASH_OUT_100P,
        //                                 CASH_OUT_200P = obj.CASH_OUT_200P,
        //                                 CASH_OUT_500P = obj.CASH_OUT_500P,
        //                                 CASH_OUT_1000P = obj.CASH_OUT_1000P,
        //                                 CASH_OUT_2000P = obj.CASH_OUT_2000P,
        //                                 CASH_OUT_5000P = obj.CASH_OUT_5000P,
        //                                 CASH_OUT_10000P = obj.CASH_OUT_10000P,
        //                                 CASH_OUT_20000P = obj.CASH_OUT_20000P,
        //                                 CASH_OUT_50000P = obj.CASH_OUT_50000P,
        //                                 CASH_OUT_100000P = obj.CASH_OUT_100000P,
        //                                 CASH_REFILL_5P = obj.CASH_REFILL_5P,
        //                                 CASH_REFILL_10P = obj.CASH_REFILL_10P,
        //                                 CASH_REFILL_20P = obj.CASH_REFILL_20P,
        //                                 CASH_REFILL_50P = obj.CASH_REFILL_50P,
        //                                 CASH_REFILL_100P = obj.CASH_REFILL_100P,
        //                                 CASH_REFILL_200P = obj.CASH_REFILL_200P,
        //                                 CASH_REFILL_500P = obj.CASH_REFILL_500P,
        //                                 CASH_REFILL_1000P = obj.CASH_REFILL_1000P,
        //                                 CASH_REFILL_2000P = obj.CASH_REFILL_2000P,
        //                                 CASH_REFILL_5000P = obj.CASH_REFILL_5000P,
        //                                 CASH_REFILL_10000P = obj.CASH_REFILL_10000P,
        //                                 CASH_REFILL_20000P = obj.CASH_REFILL_20000P,
        //                                 CASH_REFILL_50000P = obj.CASH_REFILL_50000P,
        //                                 CASH_REFILL_100000P = obj.CASH_REFILL_100000P,
        //                                 CounterCashIn = obj.CounterCashIn.Value,
        //                                 PreviousCounterCashIn = obj.PreviousCounterCashIn.SafeValue(),
        //                                 CounterCashOut = obj.CounterCashOut.Value,
        //                                 PreviousCounterCashOut = obj.PreviousCounterCashOut.SafeValue(),
        //                                 CounterRefill = obj.CounterRefill.Value,
        //                                 PreviousCounterRefills = obj.PreviousCounterRefills.SafeValue(),
        //                                 Collection_Meters_CoinsIn = obj.Collection_Meters_CoinsIn.SafeValue(),
        //                                 Previous_Meters_Coins_In = obj.Previous_Meters_Coins_In.SafeValue(),
        //                                 Collection_Meters_CoinsOut = obj.Collection_Meters_CoinsOut.SafeValue(),
        //                                 Previous_Meters_Coins_Out = obj.Previous_Meters_Coins_Out.SafeValue(),
        //                                 Collection_Treasury_Handpay = obj.Collection_Treasury_Handpay.SafeValue(),
        //                                 COLLECTION_RDC_COINSIN = obj.COLLECTION_RDC_COINSIN.SafeValue(),
        //                                 COLLECTION_RDC_COINSOUT = obj.COLLECTION_RDC_COINSOUT.SafeValue(),
        //                                 COLLECTION_RDC_VTP = obj.COLLECTION_RDC_VTP.SafeValue(),
        //                                 COLLECTION_RDC_HANDPAY = obj.COLLECTION_RDC_HANDPAY.SafeValue(),
        //                                 Collection_Meters_Handpay = obj.Collection_Meters_Handpay.SafeValue(),
        //                                 Previous_Meters_Handpay = obj.Previous_Meters_Handpay.SafeValue(),
        //                                 Collection_Date_Of_Collection = obj.Collection_Date_Of_Collection,

        //                                 //-----------


        //                             }).Single();


        //    //Cash Collected
        //    Lst_CollectionDetailsResponse.PrivCashCollected = Lst_CollectionDetailsResponse.CashCollected;
        //    //Float Recovered
        //    Lst_CollectionDetailsResponse.PrivFloatRec = Lst_CollectionDetailsResponse.Collection_Treasury_Defloat;
        //    //Gross Cash
        //    Lst_CollectionDetailsResponse.PrivGrossCash = Lst_CollectionDetailsResponse.PrivCashCollected - Lst_CollectionDetailsResponse.PrivFloatRec;
        //    //Refills
        //    Lst_CollectionDetailsResponse.PrivRefills = Lst_CollectionDetailsResponse.CashRefills;
        //    //Refunds
        //    Lst_CollectionDetailsResponse.PrivRefunds = Lst_CollectionDetailsResponse.Collection_Sundries_Unsupported + Lst_CollectionDetailsResponse.Collection_Sundries;
        //    ////Net cash
        //    Lst_CollectionDetailsResponse.PrivNetCash = Lst_CollectionDetailsResponse.PrivCashCollected - (Lst_CollectionDetailsResponse.PrivFloatRec + Lst_CollectionDetailsResponse.PrivRefills + Lst_CollectionDetailsResponse.PrivRefunds);

        //     //RDC Cash in
        //    Lst_CollectionDetailsResponse.PrivRDCCashIn =
        //                    (Lst_CollectionDetailsResponse.CASH_IN_2P / 50)
        //                   + (Lst_CollectionDetailsResponse.CASH_IN_5P.SafeValue() / 20)
        //                   + (Lst_CollectionDetailsResponse.CASH_IN_10P / 10)
        //                   + (Lst_CollectionDetailsResponse.CASH_IN_20P / 5)
        //                   + (Lst_CollectionDetailsResponse.CASH_IN_50P / 2)
        //                   + (Lst_CollectionDetailsResponse.CASH_IN_100P * 1)
        //                   + (Lst_CollectionDetailsResponse.CASH_IN_200P * 2)
        //                   + (Lst_CollectionDetailsResponse.CASH_IN_500P * 5)
        //                   + (Lst_CollectionDetailsResponse.CASH_IN_1000P * 10)
        //                   + (Lst_CollectionDetailsResponse.CASH_IN_2000P * 20)
        //                   + (Lst_CollectionDetailsResponse.CASH_IN_5000P * 50)
        //                   + (Lst_CollectionDetailsResponse.CASH_IN_10000P * 100)
        //                   + (Lst_CollectionDetailsResponse.CASH_IN_20000P * 200)
        //                   + (Lst_CollectionDetailsResponse.CASH_IN_50000P * 500)
        //                 + (Lst_CollectionDetailsResponse.CASH_IN_100000P * 1000);
        //       //RDC Cash Out
        //       Lst_CollectionDetailsResponse.PrivRDCCashOut = ((Lst_CollectionDetailsResponse.CASH_OUT_2P / 50)
        //                   + (Lst_CollectionDetailsResponse.CASH_OUT_5P.SafeValue() / 20)
        //                   + (Lst_CollectionDetailsResponse.CASH_OUT_10P / 10)
        //                   + (Lst_CollectionDetailsResponse.CASH_OUT_20P / 5)
        //                   + (Lst_CollectionDetailsResponse.CASH_OUT_50P / 2)
        //                   + (Lst_CollectionDetailsResponse.CASH_OUT_100P * 1)
        //                   + (Lst_CollectionDetailsResponse.CASH_OUT_200P * 2)
        //                   + (Lst_CollectionDetailsResponse.CASH_OUT_500P * 5)
        //                   + (Lst_CollectionDetailsResponse.CASH_OUT_1000P * 10)
        //                   + (Lst_CollectionDetailsResponse.CASH_OUT_2000P * 20)
        //                   + (Lst_CollectionDetailsResponse.CASH_OUT_5000P * 50)
        //                   + (Lst_CollectionDetailsResponse.CASH_OUT_10000P * 100)
        //                   + (Lst_CollectionDetailsResponse.CASH_OUT_20000P * 200)
        //                   + (Lst_CollectionDetailsResponse.CASH_OUT_50000P * 500)
        //                   + (Lst_CollectionDetailsResponse.CASH_OUT_100000P * 1000));

        //    //RDC Refills
        //    Lst_CollectionDetailsResponse.PrivRDCRefills = ((Lst_CollectionDetailsResponse.CASH_REFILL_5P / 20)
        //                   + (Lst_CollectionDetailsResponse.CASH_REFILL_10P / 10)
        //                   + (Lst_CollectionDetailsResponse.CASH_REFILL_20P / 5)
        //                   + (Lst_CollectionDetailsResponse.CASH_REFILL_50P / 2)
        //                   + (Lst_CollectionDetailsResponse.CASH_REFILL_100P * 1)
        //                   + (Lst_CollectionDetailsResponse.CASH_REFILL_200P * 2)
        //                   + (Lst_CollectionDetailsResponse.CASH_REFILL_500P * 5)
        //                   + (Lst_CollectionDetailsResponse.CASH_REFILL_1000P * 10)
        //                   + (Lst_CollectionDetailsResponse.CASH_REFILL_2000P * 20)
        //                   + (Lst_CollectionDetailsResponse.CASH_REFILL_5000P * 50)
        //                   + (Lst_CollectionDetailsResponse.CASH_REFILL_10000P * 100)
        //                   + (Lst_CollectionDetailsResponse.CASH_REFILL_20000P * 200)
        //                   + (Lst_CollectionDetailsResponse.CASH_REFILL_50000P * 500)
        //                   + (Lst_CollectionDetailsResponse.CASH_REFILL_100000P * 1000)).ToString();

        //    if (!Lst_CollectionDetailsResponse.CASH_IN_5P.IsValid())
        //    {
        //        Lst_CollectionDetailsResponse.PrivRDCCash = "Forced";
        //        Lst_CollectionDetailsResponse.PrivRDCRefills = "-";
        //    }
        //    else
        //    {
        //          Lst_CollectionDetailsResponse.PrivRDCCash = ((Lst_CollectionDetailsResponse.PrivRDCCashIn - Lst_CollectionDetailsResponse.PrivRDCCashOut) + Lst_CollectionDetailsResponse.PrivRDCRefills).ToString();

        //    }

        //    //RDC Cash Var
        //    Lst_CollectionDetailsResponse.PrivRDCVar = Lst_CollectionDetailsResponse.PrivNetCash -float.Parse(Lst_CollectionDetailsResponse.PrivRDCCash);

        //    if(Lst_CollectionDetailsResponse.Machine_Class_SP_Features !=SF_VALUE_SINGLE_VIDEO)
        //    {
        //         //Meter Cash in
        //        Lst_CollectionDetailsResponse.PrivMeterCashIn = (Lst_CollectionDetailsResponse.CounterCashIn -Lst_CollectionDetailsResponse.PreviousCounterCashIn) * (Lst_CollectionDetailsResponse.Installation_Counter_Cash_In_Units / 100);
        //        //Meter Cash out
        //        Lst_CollectionDetailsResponse.PrivMeterCashOut = (Lst_CollectionDetailsResponse.CounterCashOut - Lst_CollectionDetailsResponse.PreviousCounterCashOut) * (Lst_CollectionDetailsResponse.Installation_Counter_Cash_Out_Units / 100);
        //        //Meter Refills
        //        Lst_CollectionDetailsResponse.PrivMeterRefills = (Lst_CollectionDetailsResponse.CounterRefill - Lst_CollectionDetailsResponse.PreviousCounterRefills) * (Lst_CollectionDetailsResponse.Installation_Counter_Refill_Units/ 100);
        //        Lst_CollectionDetailsResponse.PrivMeterCash = ( Lst_CollectionDetailsResponse.PrivMeterCashIn -  Lst_CollectionDetailsResponse.PrivMeterCashOut) +  Lst_CollectionDetailsResponse.PrivMeterRefills;
        //    }
        //    else
        //    {
        //        Lst_CollectionDetailsResponse.PrivMeterCoinIn = ( Lst_CollectionDetailsResponse.Collection_Meters_CoinsIn-  Lst_CollectionDetailsResponse.Previous_Meters_Coins_In) * ( Lst_CollectionDetailsResponse.Installation_Price_Per_Play / 100);
        //        Lst_CollectionDetailsResponse.PrivMeterCoinOut = ( Lst_CollectionDetailsResponse.Collection_Meters_CoinsOut -  Lst_CollectionDetailsResponse.Previous_Meters_Coins_Out) * ( Lst_CollectionDetailsResponse.Installation_Price_Per_Play / 100);
        //        Lst_CollectionDetailsResponse.PrivMeterCash = ( Lst_CollectionDetailsResponse.PrivMeterCoinIn -  Lst_CollectionDetailsResponse.PrivMeterCoinOut) +  Lst_CollectionDetailsResponse.PrivMeterRefills;
        //    }

        //       //Meter Cash Var

        //      Lst_CollectionDetailsResponse.PrivMeterVar =   Lst_CollectionDetailsResponse.PrivNetCash -   Lst_CollectionDetailsResponse.PrivMeterCash;

        //    if(SiteDatapackNotUsed== true)
        //    {

        //        if(PrivRDCCash==0)
        //        {
        //              if( Lst_CollectionDetailsResponse.Machine_Class_SP_Features== SF_VALUE_SINGLE_VIDEO )
        //              {
        //                //Single Coin Machine
        //                Lst_CollectionDetailsResponse.PrivPayout = ((Lst_CollectionDetailsResponse.PrivMeterCoinOut +Lst_CollectionDetailsResponse.Collection_Treasury_Handpay) / Math.Max(Lst_CollectionDetailsResponse.PrivMeterCoinIn, 1)) * 100;
        //                Lst_CollectionDetailsResponse.PrivVTP = Lst_CollectionDetailsResponse.PrivMeterCoinIn * 10;
        //              }
        //              else
        //              {
        //                //Normal Machine
        //                Lst_CollectionDetailsResponse.PrivPayout = ((Lst_CollectionDetailsResponse.PrivMeterCashOut + Lst_CollectionDetailsResponse.Collection_Treasury_Handpay) / Math.Max(Lst_CollectionDetailsResponse.PrivMeterCashIn, 1)) * 100;
        //                Lst_CollectionDetailsResponse.PrivVTP = Lst_CollectionDetailsResponse.PrivMeterCashIn * 10;

        //              }

        //        }
        //        else
        //        {
        //                 //RDC Read

        //            if(  Lst_CollectionDetailsResponse.Machine_Class_SP_Features == SF_VALUE_SINGLE_VIDEO )
        //            {
        //                //Single coin machine

        //                 Lst_CollectionDetailsResponse.PrivRDCCoinsIn =  Lst_CollectionDetailsResponse.COLLECTION_RDC_COINSIN * ( Lst_CollectionDetailsResponse.Installation_Price_Per_Play / 100);
        //                 Lst_CollectionDetailsResponse.PrivRDCCoinsOut =  Lst_CollectionDetailsResponse.COLLECTION_RDC_COINSOUT * ( Lst_CollectionDetailsResponse.Installation_Price_Per_Play / 100);

        //                 Lst_CollectionDetailsResponse.PrivVTP = ( Lst_CollectionDetailsResponse.PrivRDCCoinsIn * 10);
        //                 Lst_CollectionDetailsResponse.PrivPayout = ( Lst_CollectionDetailsResponse.PrivRDCCoinsOut / Math.Max( Lst_CollectionDetailsResponse.PrivRDCCoinsIn, 1)) * 100;
        //            } 
        //            else
        //            {

        //                //Normal Machine
        //                Lst_CollectionDetailsResponse.PrivVTP =  Lst_CollectionDetailsResponse.COLLECTION_RDC_VTP;
        //                if(  Lst_CollectionDetailsResponse.PrivVTP > 0)
        //                {
        //                    Lst_CollectionDetailsResponse.PrivPayout = Lst_CollectionDetailsResponse.PrivRDCCashIn - (Lst_CollectionDetailsResponse.PrivVTP / 10);
        //                    Lst_CollectionDetailsResponse.PrivPayout = Lst_CollectionDetailsResponse.PrivRDCCashOut - Lst_CollectionDetailsResponse.PrivPayout;
        //                    Lst_CollectionDetailsResponse.PrivPayout = (Lst_CollectionDetailsResponse.PrivPayout / (Lst_CollectionDetailsResponse.PrivVTP / 10)) * 100;
        //                }
        //                else
        //                {
        //                    Lst_CollectionDetailsResponse.PrivPayout = 0;

        //                }

        //            }

        //        }

        //    }

        //     //Dec handpay
        //     Lst_CollectionDetailsResponse.PrivDecHandpay =  Lst_CollectionDetailsResponse.Collection_Treasury_Handpay;
        //     //RDC handpay

        //    if(!Lst_CollectionDetailsResponse.CASH_IN_5P.IsValid())
        //    {
        //        Lst_CollectionDetailsResponse.PrivRDCHandpay = "Forced";
        //    }   
        //    else
        //    {
        //        Lst_CollectionDetailsResponse.PrivRDCHandpay = ((Lst_CollectionDetailsResponse.COLLECTION_RDC_HANDPAY) * (Lst_CollectionDetailsResponse.Installation_Price_Per_Play / 100)).ToString();
        //    }


        //    //RDC Handpay Var
        //    if( !Lst_CollectionDetailsResponse.CASH_IN_5P.IsValid() )
        //    {
        //        PrivRDCHandpayVar = "-";
        //    }else
        //    {


        //        Lst_CollectionDetailsResponse.PrivRDCHandpayVar = Lst_CollectionDetailsResponse.PrivDecHandpay - Lst_CollectionDetailsResponse.PrivRDCHandpay;
        //    }   
        //    //Meter Handpay
        //    Lst_CollectionDetailsResponse.PrivMeterHandpay =(Lst_CollectionDetailsResponse.Collection_Meters_Handpay - Lst_CollectionDetailsResponse.Previous_Meters_Handpay) * (Lst_CollectionDetailsResponse.Installation_Price_Per_Play / 100);
        //    //Meter Handpay var
        //    Lst_CollectionDetailsResponse.PrivMeterHandpayVar =  Lst_CollectionDetailsResponse.PrivDecHandpay -  Lst_CollectionDetailsResponse.PrivMeterHandpay;            

        //    //Now put this info in the correct order into the listview

        //    if(Lst_CollectionDetailsResponseMachine_Class_SP_Features!=SF_VALUE_SINGLE_VIDEO )
        //    {

        //        Lst_CollectionDetailsResponse.PrivSingleCoin = false;
        //    }           
        //    else
        //    {

        //       Lst_CollectionDetailsResponse.PrivSingleCoin = true;
        //    }


        //}
        #endregion
    }
}
