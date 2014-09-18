using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using BMC.DBInterface.CashDeskOperator;
using BMC.Common.Utilities;
using BMC.Transport;
using System.Data.SqlClient;
using BMC.Common.ExceptionManagement;


namespace BMC.Business.CashDeskOperator
{    
    public class BatchhistoryBreakdown
    {
        BatchDetails _details = new BatchDetails();
        BatchDataAccess _batch;        
        List<CollectionView> _collectionView;
        int _collectionID = 0;
        int _batchID = 0;
        int _weekID = 0;
        bool _isAFTIncludedInCalculation = false;
        string _ExchangeConst = "";
        int iCount = 0;
        //
        public BatchhistoryBreakdown(int collectionID, int InstallationNo, int BatchID, string ExchangeConst, int WeekId)
        {
            try
            {
                _ExchangeConst = String.IsNullOrEmpty(ExchangeConst) ? "" : CommonUtilities.SiteConnectionString(ExchangeConst);
                _batch = _ExchangeConst != string.Empty ? new BatchDataAccess(new SqlConnection(_ExchangeConst)) : new BatchDataAccess(_ExchangeConst);
                _isAFTIncludedInCalculation = (Settings.IsAFTIncludedInCalculation);
                _collectionID = collectionID;
                _batchID = BatchID;
                _weekID = WeekId;
            }
            catch (Exception ex) { LogError(ex); }
        }
        //
        public List<TreasuryUser> GetTreasuryTable(CollectionView _collectionRecords)
        {
            List<TreasuryUser> TreasuryUsers = null;
            try
            {
                TreasuryUsers = _batch.GetTreasuryDetails(_collectionRecords.Collection_No).ToList();
                if (Convert.ToDecimal(_collectionRecords.DeclaredTicketPrintedValue) > 0)
                {
                    TreasuryUser user = new TreasuryUser();
                    user.Treasury_Type = "Vouchers Out";
                    user.Treasury_Amount = Convert.ToDouble(_collectionRecords.DeclaredTicketPrintedValue);
                    user.Treasury_Date = _collectionRecords.Collection_Date_Performed;
                    user.User_ID = "n/a";
                    user.User_Name = "n/a";
                    TreasuryUsers.Add(user);
                }

            }
            catch (Exception ex) { LogError(ex); }
            return TreasuryUsers;
        }
        //
        public void GetBatchBreakdownhistory()
        {
            try
            {
                _collectionView = _batch.GetBatchBreakdownhistory(_batchID, _weekID).ToList();
            }
            catch (Exception ex) { LogError(ex); }
        }
        //
        public List<BatchHistoryListView> GetBatchDetails(out BatchDetails Details)
        {
            List<BatchHistoryListView> BatchHistoryList = new List<BatchHistoryListView>();
            try
            {
                foreach (var item in _collectionView.AsEnumerable())
                {
                    iCount = _collectionView.Count;
                    CalculateTotalSum(item);
                    BatchHistoryList.Add(GetIndividualCollectionDetails(item));
                    _details.WeekEndDate = item.WeekEndDate;
                }
                if (_details.VTPSum != 0)
                    _details.dPayoutSum = (((Convert.ToDecimal(_details.VTPSum) - _details.WinLossSum)) /Convert.ToDecimal(_details.VTPSum)) * 100;
                else
                    _details.dPayoutSum = 0;
                //_details.dPayoutSum = _details.PayoutSum / iCount;
                _details.dHoldSum = _details.dPayoutSum / iCount;
                if (_details.VTPSum == 0)
                {
                    _details.PayoutSum = 0;
                    _details.HoldSum = 100;
                }
            }
            catch (Exception ex) { LogError(ex); }
            Details = _details;
            return BatchHistoryList.ToList();
        }
        //
        private BatchHistoryListView GetIndividualCollectionDetails(CollectionView item)
        {
            BatchHistoryListView view = new BatchHistoryListView();
            try
            {
                double? casinoWin = 0;
                view.CollectionKey = item.Collection_No + "#" + item.Installation_No;
                view.Zone = item.ZoneName;

                view.Pos = item.PosName;
                view.Asset = item.Stock_No;
                view.Game = item.MachineName;
                if ((bool)item.Declaration == false)
                {
                    view.Zone = "UNDECLARED";
                }
                else
                {
                    view.WinLoss = Convert.ToDouble(item.Declared_Total_Notes) + Convert.ToDouble(item.DecTicketBalance) - Convert.ToDouble(item.DecHandpay) + Convert.ToDouble(item.Net_Coin)
                        + (_isAFTIncludedInCalculation ? (Convert.ToDouble(item.EftIn) / 100 //((Convert.ToDouble(item.Promo_Cashable_EFT_IN.Value) + Convert.ToDouble(item.NonCashable_EFT_IN.Value) + Convert.ToDouble(item.Cashable_EFT_IN.Value))
                                    - Convert.ToDouble(item.EftOut) / 100) : 0) - (Settings.AddShortpayInVoucherOut? 0 : Convert.ToDouble(item.Shortpay.Value));

                    view.WinLossMeter = Convert.ToDouble(item.RDC_Notes) + Convert.ToDouble((item.DecTicketBalance - item.TicketVar)) - Convert.ToDouble(item.RDCHandpay) + Convert.ToDouble(item.RDC_Coin)
                        + (_isAFTIncludedInCalculation ? ((((Convert.ToDouble(item.Promo_Cashable_EFT_IN) + Convert.ToDouble(item.NonCashable_EFT_IN) + Convert.ToDouble(item.Cashable_EFT_IN))))
                        - (Convert.ToDouble(item.Promo_Cashable_EFT_OUT.Value) + Convert.ToDouble(item.NonCashable_EFT_OUT.Value) + Convert.ToDouble(item.Cashable_EFT_OUT.Value))) : 0);
                    view.WinLossVar = view.WinLoss - view.WinLossMeter;
                    view.GrossCoin = item.Declared_Total_Coinage.Value;
                    view.FloatRec = item.Defloat.Value;
                    view.Refills = item.Refills.Value;
                    view.Refunds = item.Refunds.Value;
                    view.CoinNet = item.Net_Coin.Value;
                    view.CoinMeter = item.RDC_Coin.Value;
                    view.CoinVar = item.Coin_Var.Value;
                    view.Bills = item.Declared_Total_Notes.Value;
                    view.BillsMeter = item.RDC_Notes.Value;
                    view.BillsVar = item.Note_Var.Value;
                    view.Tickets = item.DecTicketBalance.Value;
                    view.Shortpay = item.Shortpay.Value;
                    view.VoidTicket = item.Void.Value;
                    view.TicketsMeter = (item.DecTicketBalance - item.TicketVar).Value;
                    view.TicketsVar = item.TicketVar.Value;
                    view.Handpay = item.DecHandpay;
                    view.HandpayNonTruncated = item.DecHandpay.Value.ToString("r");
                    view.HandpayMeter = item.RDCHandpay.Value;
                    view.HandpayVar = item.HandpayVar.Value;
                    view.Progressive = item.Progressive_Value_Declared.Value;
                    view.ProgressiveMeter = item.Progressive_Value_Meter.Value;
                    view.ProgressiveVar = item.Progressive_Value_Variance.Value;
                    view.Handle = (item.VTP / 10).Value;
                    casinoWin = Convert.ToDouble(item.RDC_Notes) + Convert.ToDouble((item.DecTicketBalance - item.TicketVar)) - Convert.ToDouble(item.RDCHandpay) + Convert.ToDouble(item.RDC_Coin)
                        + (_isAFTIncludedInCalculation ? ((((Convert.ToDouble(item.Promo_Cashable_EFT_IN) + Convert.ToDouble(item.NonCashable_EFT_IN) + Convert.ToDouble(item.Cashable_EFT_IN))))
                        - (Convert.ToDouble(item.Promo_Cashable_EFT_OUT.Value) + Convert.ToDouble(item.NonCashable_EFT_OUT.Value) + Convert.ToDouble(item.Cashable_EFT_OUT.Value))) : 0);

                    if (item.VTP != 0)
                    {
                        view.PercentPayout = ((view.Handle -view.WinLossMeter) / view.Handle) * 100.00;


                     // 
                        view.PercentHold = (100 - ((((Convert.ToDouble(item.VTP) / 10) - casinoWin) / (Convert.ToDouble(item.VTP) / 10)) * 100)).Value;
                    }
                    else
                    {
                        view.PercentPayout = 0;
                        view.PercentHold = 100;
                    }
                    view.Faults = item.CollectionNoFaultEvents.Value;
                    view.Door = item.CollectionNoDoorEvents.Value;
                    view.Power = item.CollectionNoPowerEvents.Value;
                }
            }
            catch (Exception ex) { LogError(ex); }
            return view;
        }
        //
        private void CalculateTotalSum(CollectionView item)
        {
            try
            {
                double? casinoWin = 0;
                double? WinLossMeter = 0;
                _details.BatchNo = _weekID > 0 ? item.WeekNo.ToString() : item.Collection_Batch_No.ToString();
                _details.BatchName = item.Collection_Batch_Name;
                _details.BatchDate = _weekID > 0 ? (DateTime)item.WeekStartDate : (DateTime)item.BatchDate;
                _details.CollectionDate = _weekID > 0 ? (DateTime)item.WeekEndDate : (DateTime)item.Collection_Performed_Date;
                _details.UserName = item.UserName;
                _details.GrossCoin += Convert.ToDecimal(item.Declared_Total_Coinage);
                _details.FloatRec += Convert.ToDecimal(item.Defloat);
                _details.RefillsSum += Convert.ToDecimal(item.Refills);
                _details.RefundsSum += Convert.ToDecimal(item.Refunds);
                _details.ProgressiveSum += Convert.ToDecimal(item.Progressive_Value_Declared);
                _details.Shortpay += Convert.ToDecimal(item.Shortpay);
                _details.NetCoinSum += Convert.ToDecimal(item.Net_Coin);
                _details.NotesSum += Convert.ToDecimal(item.Declared_Total_Notes);
                _details.HandpaySum += Convert.ToDecimal(item.DecHandpay.Value.ToString("r"));
                _details.TicketBalanceSum += Convert.ToDecimal(item.DecTicketBalance);
                _details.CashTakeSum += Convert.ToDecimal(item.Declared_Total_Notes) + Convert.ToDecimal(item.DecTicketBalance) - Convert.ToDecimal(item.DecHandpay.Value.ToString("r")) + Convert.ToDecimal(item.Net_Coin)
                        + (_isAFTIncludedInCalculation ? (Convert.ToDecimal(item.EftIn) / 100
                        - Convert.ToDecimal(item.EftOut) / 100) : 0) - (Settings.AddShortpayInVoucherOut? 0 : Convert.ToDecimal(item.Shortpay));
                _details.CoinVarSum += Convert.ToDecimal(item.Coin_Var);
                _details.ProgressiveVarSum += Convert.ToDecimal(item.Progressive_Value_Variance);
                _details.NotesVarSum += Convert.ToDecimal(item.Note_Var);
                _details.TicketsVarSum += Convert.ToDecimal(item.TicketVar);
                _details.HandpayVarSum += Convert.ToDecimal(item.HandpayVar);

                casinoWin += Convert.ToDouble(Convert.ToDecimal(item.RDC_Notes) + Convert.ToDecimal((item.DecTicketBalance - item.TicketVar)) - Convert.ToDecimal(item.RDCHandpay) + Convert.ToDecimal(item.RDC_Coin)
                        + (_isAFTIncludedInCalculation ? ((((Convert.ToDecimal(item.Promo_Cashable_EFT_IN) + Convert.ToDecimal(item.NonCashable_EFT_IN) + Convert.ToDecimal(item.Cashable_EFT_IN))))
                        - (Convert.ToDecimal(item.Promo_Cashable_EFT_OUT.Value) + Convert.ToDecimal(item.NonCashable_EFT_OUT.Value) + Convert.ToDecimal(item.Cashable_EFT_OUT.Value))) : 0));



                WinLossMeter = Convert.ToDouble(item.RDC_Notes) + Convert.ToDouble((item.DecTicketBalance - item.TicketVar)) - Convert.ToDouble(item.RDCHandpay) + Convert.ToDouble(item.RDC_Coin)
                       + (_isAFTIncludedInCalculation ? ((((Convert.ToDouble(item.Promo_Cashable_EFT_IN) + Convert.ToDouble(item.NonCashable_EFT_IN) + Convert.ToDouble(item.Cashable_EFT_IN))))
                       - (Convert.ToDouble(item.Promo_Cashable_EFT_OUT.Value) + Convert.ToDouble(item.NonCashable_EFT_OUT.Value) + Convert.ToDouble(item.Cashable_EFT_OUT.Value))) : 0);

               
                if (item.VTP != null)
                {
                    _details.VTPSum += Convert.ToDecimal((Convert.ToDecimal(item.VTP) / 10));
                    _details.WinLossSum += Convert.ToDecimal(WinLossMeter);
                }
                 if (item.VTP != 0)
                {
                    _details.PayoutSum += ((_details.VTPSum - Convert.ToDecimal(WinLossMeter)) / _details.VTPSum) * 100;
                   //_details.PayoutSum += ((((Convert.ToDecimal(item.VTP) / 10) - Convert.ToDecimal(casinoWin)) / (Convert.ToDecimal(item.VTP) / 10)) * 100);
                    _details.HoldSum += (100 - ((((Convert.ToDecimal(item.VTP) / 10) - Convert.ToDecimal(casinoWin)) / (Convert.ToDecimal(item.VTP) / 10)) * 100));
                }
                _details.NoofCollections += 1;
            }
            catch (Exception ex) { LogError(ex); }
        }
        //
        public CollectionView GetCollectionData(int CollectionNo)
        {
            return _collectionView.Where(item => item.Collection_No == CollectionNo).Single();
        }
        //
        public List<AllEvents> GetAllEvents(int CollectionNo, int InstallatioNo, int Top)
        {
            List<AllEvents> LstAllEvents = new List<AllEvents>();
            try
            {
                BatchDataAccess Batch = _ExchangeConst != string.Empty ? new BatchDataAccess(new SqlConnection(_ExchangeConst)) : new BatchDataAccess(_ExchangeConst);
                List<DoorEventRecord> DoorEvents;
                IEnumerable<FaultEventRecord> FaultEvents;
                IEnumerable<PowerEventRecord> PowerEvents;
                DoorEvents = Batch.GetDoorEventData(CollectionNo, InstallatioNo, Top).ToList();
                FaultEvents = Batch.GetFaultEventData(CollectionNo, InstallatioNo, Top).ToList();
                PowerEvents = Batch.GetPowerEventData(CollectionNo, InstallatioNo, Top).ToList();
                AllEvents Events;
                foreach (var item in DoorEvents)
                {
                    Events = new AllEvents();
                    Events.Type = "Door";
                    Events.Date = item.Date.GetUniversalDateFormat();
                    Events.Time = item.Date.GetUniversalTimeFormat();
                    Events.Duration = HelperFunctions.GetDuration(item.Duration);
                    Events.Description = item.Error_Code_Description;//Door_Event_Type.Trim();// == "3" ? "Cash Door open by " : "Slot Door open by " ;
                    LstAllEvents.Add(Events);
                }
                foreach (var item in FaultEvents)
                {
                    Events = new AllEvents();
                    Events.Type = "Fault";
                    Events.Date = item.Date.GetUniversalDateFormat();
                    Events.Time = item.Date.GetUniversalTimeFormat();
                    Events.Duration = "n/a";
                    Events.Description = item.Fault_Description;
                    LstAllEvents.Add(Events);
                }
                foreach (var item in PowerEvents)
                {
                    Events = new AllEvents();
                    Events.Type = "Power On";
                    Events.Date = item.Date.GetUniversalDateFormat();
                    Events.Time = item.Date.GetUniversalTimeFormat();
                    Events.Duration = HelperFunctions.GetDuration(item.Duration);
                    Events.Description = "";
                    LstAllEvents.Add(Events);
                }
            }
            catch (Exception ex) { LogError(ex); }
            return LstAllEvents;
        }
        //
        private void LogError(Exception ex)
        {
            BMC.Common.LogManagement.LogManager.WriteLog(ex.Message, BMC.Common.LogManagement.LogManager.enumLogLevel.Error);
        }
        //
        public List<CollectionListView> GetCollectionDetailsforListView(CollectionView _collectionRecords)
        {
            var collectionDetails = new List<CollectionListView>();
            var collGrossView = new CollectionListView();
            var collNetView = new CollectionListView();
            var colShortpayview = new CollectionListView();
            decimal treasurySum = Convert.ToDecimal(_collectionRecords.DecHandpay);
            decimal shortPayTreasurySum = Convert.ToDecimal(_collectionRecords.Shortpay);
            decimal OfflineshortPayTreasurySum = Convert.ToDecimal(_collectionRecords.OffLineShortpay);
            decimal voidTreasurySum = Convert.ToDecimal(_collectionRecords.Void);
            decimal CoinsOut = Convert.ToDecimal(_collectionRecords.RDC_Total_Coinage_Out);
            decimal totalCollection = Convert.ToDecimal(_collectionRecords.CashIn);
            int POP = Convert.ToInt32(_collectionRecords.Installation_Price_of_Play);

            collGrossView.Name = "Gross";
            collNetView.Name = "Net";
            collNetView.Total =  collGrossView.Total = Convert.ToDecimal(_collectionRecords.CashCollected) + (_isAFTIncludedInCalculation ? (Convert.ToDecimal(_collectionRecords.EftIn) / 100) : 0);
            collNetView.V1000 = collGrossView.V1000 = Convert.ToDecimal(_collectionRecords.Cash_Collected_100000p);
            collNetView.V500 = collGrossView.V500 = Convert.ToDecimal(_collectionRecords.Cash_Collected_50000p);
            collNetView.V200 = collGrossView.V200 = Convert.ToDecimal(_collectionRecords.Cash_Collected_20000p);
            collNetView.V100 = collGrossView.V100 = Convert.ToDecimal(_collectionRecords.Cash_Collected_10000p);
            collNetView.V50 = collGrossView.V50 = Convert.ToDecimal(_collectionRecords.Cash_Collected_5000p);
            collNetView.V20 = collGrossView.V20 = Convert.ToDecimal(_collectionRecords.Cash_Collected_2000p);
            collNetView.V10 = collGrossView.V10 = Convert.ToDecimal(_collectionRecords.Cash_Collected_1000p);
            collNetView.V5 = collGrossView.V5 = Convert.ToDecimal(_collectionRecords.Cash_Collected_500p);
            collNetView.V2 = collGrossView.V2 = Convert.ToDecimal(_collectionRecords.Cash_Collected_200p);
            collNetView.V1 = collGrossView.V1 = Convert.ToDecimal(_collectionRecords.Cash_Collected_100p);
            collNetView.V50p = collGrossView.V50p = Convert.ToDecimal(_collectionRecords.Cash_Collected_50p);
            collNetView.V20p = collGrossView.V20p = Convert.ToDecimal(_collectionRecords.Cash_Collected_20p);
            collNetView.V10P = collGrossView.V10P = Convert.ToDecimal(_collectionRecords.Cash_Collected_10p);
            decimal grossTotalCoins = Convert.ToDecimal(_collectionRecords.Declared_Total_TrueCoin_In);
            collNetView.TotalCoins = collGrossView.TotalCoins = Convert.ToDecimal(grossTotalCoins) - CoinsOut;
            collNetView.CoinsIn = collGrossView.CoinsIn = Convert.ToDecimal(grossTotalCoins);
            collNetView.CoinsOut = collGrossView.CoinsOut = CoinsOut;
            collNetView.TicketsIn = collGrossView.TicketsIn = Convert.ToDecimal(_collectionRecords.DeclaredTicketValue); // Non cashable has been removed as it is added in the declared value (Convert.ToDecimal(_collectionRecords.RDC_TICKETS_INSERTED_NONCASHABLE_VALUE)/100);
            if (Settings.Client == "SGVI" && Settings.TicketDeclaration.ToUpper() == "AUTO")
                collNetView.TicketsOut = collGrossView.TicketsOut = Convert.ToDecimal(_collectionRecords.DeclaredTicketPrintedValue);
            else
            {
                if (Settings.AddShortpayInVoucherOut)

                    collNetView.TicketsOut = collGrossView.TicketsOut = Convert.ToDecimal(_collectionRecords.DeclaredTicketPrintedValue) +
                                          (Convert.ToDecimal(shortPayTreasurySum) + Convert.ToDecimal(OfflineshortPayTreasurySum))
                                           - Convert.ToDecimal(voidTreasurySum);
                else
                {
                    collNetView.TicketsOut = collGrossView.TicketsOut = Convert.ToDecimal(_collectionRecords.DeclaredTicketPrintedValue) +
                                         +Convert.ToDecimal(OfflineshortPayTreasurySum)
                                          - Convert.ToDecimal(voidTreasurySum);
                }
            }

            collNetView.PromoCashableValue = collGrossView.PromoCashableValue = Convert.ToDecimal(_collectionRecords.PromoCashableValue);
            collNetView.PromoNonCashableValue = collGrossView.PromoNonCashableValue = Convert.ToDecimal(_collectionRecords.PromoNonCashableValue);

            // Non cashable voucher out is removed as it is added in the declared ticket printed value(Convert.ToDecimal(_collectionRecords.RDC_TICKETS_PRINTED_NONCASHABLE_VALUE)/100);
            collNetView.EFTIn = collGrossView.EFTIn = (Convert.ToDecimal(_collectionRecords.EftIn) / 100); // (((Convert.ToDecimal(_collectionRecords.Promo_Cashable_EFT_IN) + Convert.ToDecimal(_collectionRecords.NonCashable_EFT_IN) + Convert.ToDecimal(_collectionRecords.Cashable_EFT_IN))) / 100);
            collNetView.EFTOut = collGrossView.EFTOut = (Convert.ToDecimal(_collectionRecords.EftOut) / 100);
            collNetView.EFT = collGrossView.EFT = (Convert.ToDecimal(_collectionRecords.EftIn) / 100) - (Convert.ToDecimal(_collectionRecords.EftOut) / 100);
            collNetView.Tickets = collGrossView.Tickets = Convert.ToDecimal(collGrossView.TicketsIn) - Convert.ToDecimal(collGrossView.TicketsOut);
            collNetView.Handpay = collGrossView.Handpay = Convert.ToDecimal(_collectionRecords.DecHandpay.Value.ToString("r"));// Value Calculated in View/Function
            collNetView.Progressive = collGrossView.Progressive = Convert.ToDecimal(_collectionRecords.Progressive_Value_Declared);
            collNetView.IsMatch = collGrossView.IsMatch = totalCollection == Convert.ToDecimal(_collectionRecords.CashCollected) ? true : false;
            collectionDetails.Add(collGrossView);
            //ShortPay
            colShortpayview.Name = "Shortpay";

            colShortpayview.TicketsOut = Convert.ToDecimal(_collectionRecords.Shortpay);
            colShortpayview.Tickets = ((_collectionRecords.Shortpay > 0) ? Convert.ToDecimal((_collectionRecords.Shortpay * -1)) : Convert.ToDecimal(_collectionRecords.Shortpay));
            collectionDetails.Add(colShortpayview);
            //copy the gross object to net
            collectionDetails.Add(collNetView);

            
            CollectionListView collMeterView = new CollectionListView();
            collMeterView.Name = "Meters";

            var collVarView = new CollectionListView { Name = "Variance" };
            decimal totaCashDiff = CalculateCashDiff(_collectionRecords);
            decimal totalCoins;
            collVarView.Total = (Convert.ToDecimal(_collectionRecords.CashCollected) + (_isAFTIncludedInCalculation ? (Convert.ToDecimal(_collectionRecords.EftIn) / 100) : 0))
                                - (CalculateCashDiff(_collectionRecords) + Convert.ToDecimal(_collectionRecords.RDC_Total_Coinage_In));
            if (_collectionRecords.Collection_EDC_Status == 0)
            {
                collMeterView.Total = CalculateCashDiff(_collectionRecords) + Convert.ToDecimal(_collectionRecords.RDC_Total_Coinage_In);
                collMeterView.V1000 = (Convert.ToDecimal(_collectionRecords.CASH_IN_100000P) - Convert.ToDecimal(_collectionRecords.CASH_OUT_100000P)) * 1000;
                collMeterView.V500 = (Convert.ToDecimal(_collectionRecords.CASH_IN_50000P) - Convert.ToDecimal(_collectionRecords.CASH_OUT_50000P)) * 500;
                collMeterView.V200 = (Convert.ToDecimal(_collectionRecords.CASH_IN_20000P) - Convert.ToDecimal(_collectionRecords.CASH_OUT_20000P)) * 200;
                collMeterView.V100 = (Convert.ToDecimal(_collectionRecords.CASH_IN_10000P) - Convert.ToDecimal(_collectionRecords.CASH_OUT_10000P)) * 100;
                collMeterView.V50 = (Convert.ToDecimal(_collectionRecords.CASH_IN_5000P) - Convert.ToDecimal(_collectionRecords.CASH_OUT_5000P)) * 50;
                collMeterView.V20 = (Convert.ToDecimal(_collectionRecords.CASH_IN_2000P) - Convert.ToDecimal(_collectionRecords.CASH_OUT_2000P)) * 20;
                collMeterView.V10 = (Convert.ToDecimal(_collectionRecords.CASH_IN_1000P) - Convert.ToDecimal(_collectionRecords.CASH_OUT_1000P)) * 10;
                collMeterView.V5 = (Convert.ToDecimal(_collectionRecords.CASH_IN_500P) - Convert.ToDecimal(_collectionRecords.CASH_OUT_500P)) * 5;
                collMeterView.V2 = (Convert.ToDecimal(_collectionRecords.CASH_IN_200P) - Convert.ToDecimal(_collectionRecords.CASH_OUT_200P)) * 2;
                collMeterView.V1 = Convert.ToDecimal(_collectionRecords.CASH_IN_100P) - Convert.ToDecimal(_collectionRecords.CASH_OUT_100P);
                collMeterView.V50p = (Convert.ToDecimal(_collectionRecords.CASH_IN_50P) - Convert.ToDecimal(_collectionRecords.CASH_OUT_50P)) / 2;
                collMeterView.V20p = (Convert.ToDecimal(_collectionRecords.CASH_IN_20P) - Convert.ToDecimal(_collectionRecords.CASH_OUT_20P)) / 5;
                collMeterView.V10P = (Convert.ToDecimal(_collectionRecords.CASH_IN_10P) - Convert.ToDecimal(_collectionRecords.CASH_OUT_10P)) / 10;
                totalCoins = (Convert.ToDecimal(_collectionRecords.RDC_Total_Coinage_In) - Convert.ToDecimal(_collectionRecords.RDC_Total_Coinage_Out));
                collMeterView.TotalCoins = Convert.ToDecimal(totalCoins);
                collMeterView.CoinsIn = Convert.ToDecimal(totalCoins) + CoinsOut;
                collMeterView.CoinsOut = CoinsOut;
                collMeterView.TotalCoins = Convert.ToDecimal(totalCoins);
                collMeterView.TicketsIn = (Convert.ToDecimal(_collectionRecords.COLLECTION_RDC_TICKETS_INSERTED_VALUE) / 100) + (Convert.ToDecimal(_collectionRecords.RDC_TICKETS_INSERTED_NONCASHABLE_VALUE));
                collMeterView.TicketsOut = (Convert.ToDecimal(_collectionRecords.COLLECTION_RDC_TICKETS_PRINTED_VALUE) / 100) + (Convert.ToDecimal(_collectionRecords.RDC_TICKETS_PRINTED_NONCASHABLE_VALUE));
                collMeterView.Tickets = collMeterView.TicketsIn - collMeterView.TicketsOut;
                collMeterView.EFTIn = (Convert.ToDecimal(_collectionRecords.Promo_Cashable_EFT_IN) + Convert.ToDecimal(_collectionRecords.NonCashable_EFT_IN) + Convert.ToDecimal(_collectionRecords.Cashable_EFT_IN));
                collMeterView.EFTOut = (Convert.ToDecimal(_collectionRecords.Promo_Cashable_EFT_OUT) + Convert.ToDecimal(_collectionRecords.NonCashable_EFT_OUT) + Convert.ToDecimal(_collectionRecords.Cashable_EFT_OUT));
                collMeterView.EFT = collMeterView.EFTIn - collMeterView.EFTOut;
                collMeterView.Handpay = ((Convert.ToDecimal(_collectionRecords.COLLECTION_RDC_HANDPAY) * POP) / 100) + (Convert.ToDecimal(_collectionRecords.COLLECTION_RDC_JACKPOT));
                collMeterView.Progressive = ((Convert.ToDecimal(_collectionRecords.progressive_win_Handpay_value)) * POP / 100);
                collectionDetails.Add(collMeterView);

                //Difference between net and meter
                collVarView.V1000 = collNetView.V1000 - collMeterView.V1000;
                collVarView.V500 = collNetView.V500 - collMeterView.V500;
                collVarView.V200 = collNetView.V200 - collMeterView.V200;
                collVarView.V100 = collNetView.V100 - collMeterView.V100;
                collVarView.V50 = collNetView.V50 - collMeterView.V50;
                collVarView.V20 = collNetView.V20 - collMeterView.V20;
                collVarView.V10 = collNetView.V10 - collMeterView.V10;
                collVarView.V5 = collNetView.V5 - collMeterView.V5;
                collVarView.V2 = collNetView.V2 - collMeterView.V2;
                collVarView.V1 = collNetView.V1 - collMeterView.V1;
                collVarView.V50p = collNetView.V50p - collMeterView.V50p;
                collVarView.V20p = collNetView.V20p - collMeterView.V20p;
                collVarView.V10P = collNetView.V10P - collMeterView.V10P;
                collVarView.TotalCoins = collNetView.TotalCoins - collMeterView.TotalCoins;
                collVarView.CoinsIn = collNetView.CoinsIn - collMeterView.CoinsIn;
                collVarView.CoinsOut = 0;
                collVarView.TicketsIn = collNetView.TicketsIn - collMeterView.TicketsIn;
                collVarView.TicketsOut = collNetView.TicketsOut - collMeterView.TicketsOut;
                collVarView.Tickets = collVarView.TicketsIn - collVarView.TicketsOut;
                collVarView.EFTIn = collNetView.EFTIn - collMeterView.EFTIn;
                collVarView.EFTOut = collNetView.EFTOut - collMeterView.EFTOut;
                collVarView.EFT = collVarView.EFTIn - collVarView.EFTOut;
                collVarView.Handpay = collNetView.Handpay - collMeterView.Handpay;
                collVarView.Progressive = collNetView.Progressive - collMeterView.Progressive;
                collVarView.PromoCashableValue = collNetView.PromoCashableValue - collMeterView.PromoCashableValue;
                collVarView.PromoNonCashableValue = collNetView.PromoNonCashableValue - collMeterView.PromoNonCashableValue;
                collectionDetails.Add(collVarView);
            }
            else
            {
                collMeterView.Total = 0;
                collMeterView.V1000 = 0;
                collMeterView.V500 = 0;
                collMeterView.V200 = 0;
                collMeterView.V100 = 0;
                collMeterView.V50 = 0;
                collMeterView.V20 = 0;
                collMeterView.V10 = 0;
                collMeterView.V5 = 0;
                collMeterView.V2 = 0;
                collMeterView.V1 = 0;
                collMeterView.V50p = 0;
                collMeterView.V20p = 0;
                collMeterView.V10P = 0;
                collMeterView.TotalCoins = 0;
                collMeterView.CoinsIn = 0;
                collMeterView.CoinsOut = 0;
                collMeterView.TicketsIn = 0;
                collMeterView.TicketsOut = 0;
                collMeterView.EFTIn = 0;
                collMeterView.EFTOut = 0;
                collMeterView.EFT = 0;
                collMeterView.Tickets = 0;
                collMeterView.Handpay = 0;
                collMeterView.Progressive = 0;
                collectionDetails.Add(collMeterView);
                //Reusing Gross View as Variance since Meters values are zero
                collGrossView.Name = "Variance";
                collectionDetails.Add(collGrossView);

            }
         
            return collectionDetails;

        }
        //
        private decimal CalculateCashDiff(CollectionView cRecord)
        {
            decimal cashDiff =
            Settings.Region != "US" ? 0 : (Convert.ToDecimal(cRecord.CASH_IN_100P) - Convert.ToDecimal(cRecord.CASH_OUT_100P));
            cashDiff += (Settings.Region == "UK" ? 0 :  (Convert.ToDecimal(cRecord.CASH_IN_200P) - Convert.ToDecimal(cRecord.CASH_OUT_200P)) * 2);
            cashDiff += (Convert.ToDecimal(cRecord.CASH_IN_500P) - Convert.ToDecimal(cRecord.CASH_OUT_500P)) * 5;
            cashDiff += (Convert.ToDecimal(cRecord.CASH_IN_1000P) - Convert.ToDecimal(cRecord.CASH_OUT_1000P)) * 10;
            cashDiff += (Convert.ToDecimal(cRecord.CASH_IN_2000P) - Convert.ToDecimal(cRecord.CASH_OUT_2000P)) * 20;
            cashDiff += (Convert.ToDecimal(cRecord.CASH_IN_5000P) - Convert.ToDecimal(cRecord.CASH_OUT_5000P)) * 50;
            cashDiff += (Convert.ToDecimal(cRecord.CASH_IN_10000P) - Convert.ToDecimal(cRecord.CASH_OUT_10000P)) * 100;
            cashDiff += (Convert.ToDecimal(cRecord.CASH_IN_20000P) - Convert.ToDecimal(cRecord.CASH_OUT_20000P)) * 200;
            cashDiff += (Convert.ToDecimal(cRecord.CASH_IN_50000P) - Convert.ToDecimal(cRecord.CASH_OUT_50000P)) * 500;
            cashDiff += (Convert.ToDecimal(cRecord.CASH_IN_100000P) - Convert.ToDecimal(cRecord.CASH_OUT_100000P)) * 1000;
            cashDiff += (Convert.ToDecimal(cRecord.COLLECTION_RDC_TICKETS_INSERTED_VALUE) / 100) + (Convert.ToDecimal(cRecord.RDC_TICKETS_INSERTED_NONCASHABLE_VALUE));
            cashDiff += (_isAFTIncludedInCalculation ? ((((Convert.ToDecimal(cRecord.Promo_Cashable_EFT_IN) + Convert.ToDecimal(cRecord.NonCashable_EFT_IN) + Convert.ToDecimal(cRecord.Cashable_EFT_IN))))) : 0);
            return cashDiff;
        }
        //
        public List<rsp_AssetVarianceHistoryResult> GetAssetVarianceHistory(int InstallationNo, int RecordCount)
        {
            var batchDataAccess = _ExchangeConst != string.Empty ? new BatchDataAccess(new SqlConnection(_ExchangeConst)) : new BatchDataAccess(_ExchangeConst);
            List<rsp_AssetVarianceHistoryResult> Total=new List<rsp_AssetVarianceHistoryResult>();
            Total = batchDataAccess.AssetVarianceHistory(InstallationNo, RecordCount).ToList(
                
             
                //float? = Total.Sum( e => e.coin_Var );
                //float? fSumNote_Var = Total.Sum(e => e.note_var);
                //double? iSumTicket_In_Var = Total .Sum(e => e.ticket_in_var);
                //double? iSumTicket_Out_Var = Total.Sum(e => e.ticket_out_var);
                //double? iSumEftIn_Var = Total.Sum(e => e.EftIn_var);
                //double? iSumEftOut_Var = Total.Sum(e => e.EftOut_var);
                //double? iSumHandpay_Var = Total.Sum(e => e.HandPay_Var);
                //double? iSumProgressive_Var = Total.Sum(e => e.Prog_Var);
                );
            rsp_AssetVarianceHistoryResult a=new rsp_AssetVarianceHistoryResult();
           // a = default(rsp_AssetVarianceHistoryResult);
            
            a.ticket_out_var = 0;
            a.HandPay_Var = 0;
            a.Prog_Var = 0;
            a.EftIn_var = 0;
            a.EftOut_var = 0;
            a.ticket_in_var = 0;
            a.ticket_out_var = 0;
            a.note_var = 0;
            a.coin_Var = 0;
            foreach (var rspAssetVarianceHistoryResult in Total)
            {
                a.EftIn_var = a.EftIn_var + rspAssetVarianceHistoryResult.EftIn_var;
                a.EftOut_var = a.EftOut_var + rspAssetVarianceHistoryResult.EftOut_var;
                a.ticket_in_var = a.ticket_in_var + rspAssetVarianceHistoryResult.ticket_in_var;    
                a.ticket_out_var = a.ticket_out_var + rspAssetVarianceHistoryResult.ticket_out_var;
                a.HandPay_Var = a.HandPay_Var + rspAssetVarianceHistoryResult.HandPay_Var;
                a.Prog_Var= a.Prog_Var + rspAssetVarianceHistoryResult.Prog_Var;
                a.note_var = a.note_var + rspAssetVarianceHistoryResult.note_var;
                a.coin_Var = a.coin_Var + rspAssetVarianceHistoryResult.coin_Var;
            }
            a.gaming_day = "Total";
            //List<rsp_AssetVarianceHistoryResult> calculateList = (from Res in Total
            //    select new rsp_AssetVarianceHistoryResult {coin_Var = Total.Sum(e => e.coin_Var)}).ToList<rsp_AssetVarianceHistoryResult>();
            Total.Insert(0,a);
            return Total;
            //batchDataAccess.AssetVarianceHistory(InstallationNo, RecordCount).ToList();
        }
        //
        public List<PartCollectionUser> GetCollectionUser(CollectionView _collectionView)
        {
            List<PartCollectionUser> CollectionList = new List<PartCollectionUser>();
            PartCollectionUser Collection = new PartCollectionUser();
            Collection = new PartCollectionUser
            {
                Part_Collection_Date_Performed = _collectionView.Collection_Performed_Date,
                Part_Collection_DateOnly =
                    _collectionView.Collection_Date.GetUniversalDateFormat(),
                Part_Collection_Time =
                    _collectionView.Collection_Date.GetUniversalTimeFormat(),
                User_Name = _collectionView.UserName,
                Part_Collection_CashCollected = (float)(_collectionView.CashCollected)
            };
            Collection.Description = _collectionView.DropType;
            CollectionList.Add(Collection);
            return CollectionList;
        }
    }
    
    public class HelperFunctions
    {
        public static string GetDuration(object Duration)
        {
            try
            {
                return TimeSpan.FromSeconds(double.Parse(Duration.ToString())).ToString();
            }
            catch(Exception Ex) 
            {
                ExceptionManager.Publish(Ex);
                return "00:00:00";
            }
        }
    }
}
