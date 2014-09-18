using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Data;
using System.Data.Linq;
using System.Data.SqlClient;
using BMC.Business.CashDeskOperator;
using BMC.Common.Utilities;
using BMC.Common.ExceptionManagement;
using BMC.Transport;
using PrintUtility = BMC.Common.Utilities.PrintUtility;
using System.Collections.ObjectModel;
using System.Globalization;
using Microsoft.Win32;
using BMC.CashDeskOperator;
using BMC.Common.LogManagement;
using BMC.CoreLib;
using System.IO;

namespace BMC.CashDeskOperator
{
    public class CollectionHelper
    {
        #region private properties
        private readonly CollectionDataContext _collectionDataContext;
        private readonly String exchangeConnectionString;
        #endregion

        #region Ctor
        public CollectionHelper()
        {
            _collectionDataContext = new CollectionDataContext(new SqlConnection(CommonUtilities.ExchangeConnectionString));
            exchangeConnectionString = CommonUtilities.ExchangeConnectionString;

        }
        //
        public CollectionHelper(string sExchangeConnectionString)
        {
            _collectionDataContext = new CollectionDataContext(new SqlConnection(CommonUtilities.SiteConnectionString(sExchangeConnectionString)));
            exchangeConnectionString = sExchangeConnectionString;
        }
        #endregion

        #region "Private Methods"

        public static bool IsServerConnected( string connectionString) 
        {     
            if (string.IsNullOrEmpty(connectionString))
            {
                return false;
            }
            using (SqlConnection connection = new SqlConnection(CommonUtilities.SiteConnectionString(connectionString)))     
            {         
                try         
                {             
                    connection.Open();             
                    return true;         
                }         
                catch (SqlException)         
                {             
                    return false;         
                }         
                finally         
                {
                    connection.Close();         
                }     
            } 
        }

        //public ISingleResult<SiteConfig> GetSiteConfig()
        //{
        //    return _collectionDataContext.GetSiteConfig();
        //}
        //public static void SetCurrentExchangeConnectionString()
        //{
        //    CommonUtilities.SetCurrentExchangeConnectionString();
        //}

        //public static void SetCurrentTicketConnectionString()
        //{
        //    CommonUtilities.SetCurrentTicketConnectionString();
        //}


        public static void SetExchangeConnectionString(string ConnectionString)
        {
            CommonUtilities.SiteConnectionString(ConnectionString);
        }

        public static void SetTicketConnectionString(string ConnectionString)
        {
            CommonUtilities.TicketingConnectionString(ConnectionString);
        }
        //
        private List<UndeclaredCollectionRecord> GetUndeclaredFullCollection(int batchNo,
            DeclarationFilterBy filterBy, string filterValue)
        {
            var undeclaredCollectionRecords = new List<UndeclaredCollectionRecord>();
            var regionSettingValue = string.Empty;
            var isAutodeclaration = "true";

            decimal
                    handpay,
                    jackpot,
                    ticketsIn,
                    ticketsOut,
                    shortpay,
                    refund,
                    refill,
                    EftIn,
                    EFTOut,
                    winloss,
                    CoinOut,
                    CoinIn,
                    AttendantPay;

            decimal sumOfhandpay = 0,
                    sumOfjackpot = 0,
                    sumOfticketsIn = 0,
                    sumOfticketsOut = 0,
                    sumOfshortpay = 0,
                    sumOfrefund = 0,
                    sumOfrefill = 0,
                    sumOfWinloss = 0,
                    sumOfEftin = 0,
                    sumOfEftout = 0,
                    sumOfCoinOut = 0,
                    sumOfCoinIn = 0,
                    sumOfAttendantPay = 0;

            string Coll_Batch_Name = string.Empty;

            var i = 0;
            try
            {
                regionSettingValue = Common.Utilities.ExtensionMethods.CurrentSiteCulture;
                _collectionDataContext.GetSetting(0, "Auto_Declare_Monies", "true", ref isAutodeclaration);

                foreach (var collection in _collectionDataContext.GetFilteredUndeclaredCollectionByBatchNo(batchNo, (int)filterBy, filterValue))
                {
                    i++;

                    ticketsIn = Convert.ToDecimal((float)collection.TicketsIn);

                    //Include shortpay to voucher out if AddShortpayInVoucherOut is set
                    if (Settings.AddShortpayInVoucherOut)
                    {
                        ticketsOut = (Settings.Client == "SGVI" & Settings.TicketDeclaration == "AUTO") ?
                                          Convert.ToDecimal((float)collection.TicketsOut)
                                          : Convert.ToDecimal((float)collection.TicketsOut) + Convert.ToDecimal(collection.ShortPay);
                    }
                    else
                    {
                        ticketsOut = Convert.ToDecimal((float)collection.TicketsOut);
                    }

                    Coll_Batch_Name = collection.Collection_Batch_Name;

                    handpay = Convert.ToDecimal((float)collection.Handpay);
                    CoinOut = Convert.ToDecimal((float)collection.CoinOut);
                    CoinIn = Convert.ToDecimal((float)collection.CoinIn);
                    EftIn = (decimal)(_collectionDataContext.GetEftIn(collection.Collection_No));
                    EFTOut = (decimal)(_collectionDataContext.GetEftOut(collection.Collection_No));
                    AttendantPay = Convert.ToDecimal(collection.AttendantPay);
                    sumOfticketsIn += ticketsIn;
                    sumOfticketsOut += ticketsOut;
                    sumOfEftin += (EftIn / 100);
                    sumOfEftout += (EFTOut / 100);
                    sumOfCoinOut += CoinOut;
                    sumOfCoinIn += CoinIn;
                   

                    var undeclaredCollectionRecord = new UndeclaredCollectionRecord
                    {
                        TicketsInValue = ticketsIn,
                        TicketsOutValue = ticketsOut,
                        CoinOutValue = CoinOut,
                        CoinInValue = CoinIn,
                        AssetNo = collection.AssetNo,
                        ReferenceID = i,
                        Position = collection.Bar_Pos_Name,
                        GameTitle = collection.Name,
                        CollectionDate = (DateTime)collection.Date,
                        CollectionTime = (DateTime)collection.Date,
                        CollectionNo = collection.Collection_No,
                        InstallationNo = collection.Installation_No,
                        CollectionBatchNo = (int)collection.Collection_Batch_No,
                        P100000 = (int)(collection.Cash_Collected_100000P),
                        P50000 = (int)(collection.Cash_Collected_50000P),
                        P20000 = (int)(collection.Cash_Collected_20000P),
                        P10000 = (int)(collection.Cash_Collected_10000P),
                        P5000 = (int)(collection.Cash_Collected_5000P),
                        P2000 = (int)(collection.Cash_Collected_2000p),
                        P1000 = (int)(collection.Cash_Collected_1000P),
                        P500 = (int)(collection.Cash_Collected_500P),
                        P200 = (int)(collection.Cash_Collected_200P),
                        P100 = (int)(collection.Cash_Collected_100P),
                        P50 = (decimal)(collection.Cash_Collected_50p),
                        P20 = (decimal)(collection.Cash_Collected_20p),
                        P10 = (decimal)(collection.Cash_Collected_10p),
                        P5 = (decimal)(collection.Cash_Collected_5p),
                        P2 = (decimal)(collection.Cash_Collected_2p),
                        P1 = (decimal)(collection.Cash_Collected_1p),
                        EFTInValue = EftIn / 100,
                        EFTOutValue = EFTOut / 100,
                        Installation_Token_Value = (int)collection.Installation_Token_Value,
                        Collection_Batch_Name=Coll_Batch_Name
                    };


                    if (collection.Zone_No == null || collection.Zone_No == 0)
                        undeclaredCollectionRecord.Zone = "N/A";
                    else
                        undeclaredCollectionRecord.Zone = collection.Zone_Name;

                    if (collection.Collection_Defloat_Collection == null ||
                        collection.Collection_Defloat_Collection == false)
                    {
                        undeclaredCollectionRecord.BackColor = "Red";
                        undeclaredCollectionRecord.Type = "Full";
                    }
                    else
                    {
                        undeclaredCollectionRecord.BackColor = "Blue";
                        undeclaredCollectionRecord.Type = "Defloat";
                    }

                    //if (regionSettingValue.ToUpper().Contains("US"))
                    undeclaredCollectionRecord.Region = Settings.Region;//"US";

                    if (isAutodeclaration.ToUpper() == "TRUE")
                    {
                        //Added by Anil to fix CR 88038
                        shortpay = Convert.ToDecimal(collection.ShortPay);
                        handpay = Convert.ToDecimal(collection.Handpay);
                        refund = Convert.ToDecimal(collection.Refunds);
                        refill = Convert.ToDecimal(collection.Refills);
                        jackpot = Convert.ToDecimal(collection.HandpayJackpot);
                        AttendantPay = Convert.ToDecimal(collection.AttendantPay);
                        //shortpay = 0;
                        //handpay = Convert.ToDecimal(collection.DeclaredHandpay);                    
                        //refund = 0;
                        //refill = 0;
                        //jackpot = 0;


                        sumOfshortpay += shortpay;
                        sumOfhandpay += handpay;
                        sumOfrefill += refill;
                        sumOfrefund += refund;
                        sumOfjackpot += jackpot;
                        sumOfAttendantPay += AttendantPay;

                        undeclaredCollectionRecord.ShortPayValue = shortpay;
                        undeclaredCollectionRecord.HandpayValue = handpay;
                        undeclaredCollectionRecord.RefundValue = refund;
                        undeclaredCollectionRecord.RefillsValue = refill;
                        undeclaredCollectionRecord.JackpotValue = jackpot;
                        undeclaredCollectionRecord.AttendantPayValue = AttendantPay ;

                    }
                    else
                    {
                        shortpay = Convert.ToDecimal(collection.ShortPay);
                        handpay = Convert.ToDecimal(collection.Handpay);
                        refund = Convert.ToDecimal(collection.Refunds);
                        refill = Convert.ToDecimal(collection.Refills);
                        jackpot = Convert.ToDecimal(collection.HandpayJackpot);
                        AttendantPay = Convert.ToDecimal(collection.AttendantPay);
                        //durga
                       // AttendantPay = Convert.ToDecimal(collection.AttendantPay) + Convert.ToDecimal(collection.HandpayJackpot);

                        sumOfshortpay += shortpay;
                        sumOfhandpay += handpay;
                        sumOfrefill += refill;
                        sumOfrefund += refund;
                        sumOfjackpot += jackpot;
                        sumOfAttendantPay += AttendantPay;

                        undeclaredCollectionRecord.ShortPayValue = shortpay;
                        undeclaredCollectionRecord.HandpayValue = handpay;
                        undeclaredCollectionRecord.RefundValue = refund;
                        undeclaredCollectionRecord.RefillsValue = refill;
                        undeclaredCollectionRecord.JackpotValue = jackpot;
                        undeclaredCollectionRecord.AttendantPayValue = AttendantPay;
                    }

                    //Reset shortpay only if addshortpayvoucher is set
                    shortpay = Settings.AddShortpayInVoucherOut ? 0 : shortpay;

                    //Deducted Coin out from total in for WinLoss Calculation
                    winloss = (undeclaredCollectionRecord.TotalCoinsValue
                        + undeclaredCollectionRecord.TotalBillValue
                        + ticketsIn
                        - CoinOut
                        - ticketsOut                        
                        - handpay
                        - shortpay
                        - refill
                        - refund
                        - jackpot);

                    sumOfWinloss += winloss;
                    undeclaredCollectionRecord.WinLossValue = winloss;

                    undeclaredCollectionRecords.Add(undeclaredCollectionRecord);
                }

                if (undeclaredCollectionRecords.Count == 0) return null;

                var uc = (from collectionRecord in undeclaredCollectionRecords
                          group collectionRecord by collectionRecord.Region
                              into collectiongroup
                              select new UndeclaredCollectionRecord
                              {
                                  // Added bool to handle sort in datagrid
                                  IsTotalRow=true,
                                  Zone = "Total",
                                  BackColor = "Blue",
                                  AssetNo = "",
                                  CollectionDate = DateTime.MinValue,
                                  CollectionTime = DateTime.MinValue,
                                  GameTitle = "",
                                  HandpayValue = sumOfhandpay,
                                  P1 = collectiongroup.Sum(c => c.P1),
                                  P2 = collectiongroup.Sum(c => c.P2),
                                  P5 = collectiongroup.Sum(c => c.P5),
                                  P10 = collectiongroup.Sum(c => c.P10),
                                  P20 = collectiongroup.Sum(c => c.P20),
                                  P50 = collectiongroup.Sum(c => c.P50),
                                  P100 = collectiongroup.Sum(c => c.P100),
                                  P200 = collectiongroup.Sum(c => c.P200),
                                  P500 = collectiongroup.Sum(c => c.P500),
                                  P1000 = collectiongroup.Sum(c => c.P1000),
                                  P2000 = collectiongroup.Sum(c => c.P2000),
                                  P5000 = collectiongroup.Sum(c => c.P5000),
                                  P10000 = collectiongroup.Sum(c => c.P10000),
                                  P20000 = collectiongroup.Sum(c => c.P20000),
                                  P50000 = collectiongroup.Sum(c => c.P50000),
                                  P100000 = collectiongroup.Sum(c => c.P100000),
                                  JackpotValue = sumOfjackpot,
                                  Meters = "",
                                  Position = "",
                                  RefillsValue = sumOfrefill,
                                  RefundValue = sumOfrefund,
                                  ShortPayValue = sumOfshortpay,
                                  TicketsInValue = sumOfticketsIn,
                                  TicketsOutValue = sumOfticketsOut,
                                  EFTInValue = sumOfEftin,
                                  EFTOutValue = sumOfEftout,
                                  CoinOutValue = sumOfCoinOut,
                                  CoinInValue = sumOfCoinIn,
                                  AttendantPayValue = sumOfAttendantPay,
                                  Type = "Total",
                                  WinLossValue = sumOfWinloss,
                                  Region = regionSettingValue.ToUpper().Contains("US") ? "US" : "IT",
                                  Collection_Batch_Name = Coll_Batch_Name
                              }).FirstOrDefault();

                var returnCollection = new List<UndeclaredCollectionRecord> { uc };
                returnCollection.AddRange(undeclaredCollectionRecords);
                return returnCollection;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return null;
            }
        }

        private List<UndeclaredCollectionRecord> GetUndeclaredPartCollectionByBatchNo(int batchNo,
            DeclarationFilterBy filterBy, string filterValue)
        {
            var undeclaredCollectionRecords = new List<UndeclaredCollectionRecord>();
            var regionSettingValue = string.Empty;
            var isAutodeclaration = "true";
            var i = 0;

            decimal handpay,
                    jackpot = 0,
                    ticketsIn,
                    ticketsOut,
                    shortpay = 0,
                    refund = 0,
                    refill = 0,
                    winloss;

            decimal sumOfhandpay = 0,
                    sumOfjackpot = 0,
                    sumOfticketsIn = 0,
                    sumOfticketsOut = 0,
                    sumOfshortpay = 0,
                    sumOfrefund = 0,
                    sumOfrefill = 0,
                    sumOfWinloss = 0;

            _collectionDataContext.GetSetting(0, "REGION", "US", ref regionSettingValue);
            _collectionDataContext.GetSetting(0, "Auto_Declare_Monies", "true", ref isAutodeclaration);

            foreach (var collection in _collectionDataContext.GetUndeclaredPartCollectionByMachine(Dns.GetHostName(), batchNo,
             (int)filterBy, filterValue))
            {
                i++;
                ticketsIn = Convert.ToDecimal((float)collection.TicketsIn);
                ticketsOut = Convert.ToDecimal((float)collection.TicketsOut);
                var undeclaredCollectionRecord = new UndeclaredCollectionRecord
                {
                    ReferenceID = i,
                    Position = collection.Bar_Pos_Name,
                    AssetNo = collection.AssetNo,
                    GameTitle = collection.Name,
                    Installation_Token_Value = (int)collection.Installation_Token_Value,
                    CollectionNo = (int)collection.Collection_No,
                    CollectionBatchNo = collection.Collection_Batch_No,
                    CollectionDate = (DateTime)collection.Date,
                    CollectionTime = (DateTime)collection.Date,
                    InstallationNo = collection.Installation_No,
                    P100000 = (int)(collection.Cash_Collected_100000P),
                    P50000 = (int)(collection.Cash_Collected_50000P),
                    P20000 = (int)(collection.Cash_Collected_20000P),
                    P10000 = (int)(collection.Cash_Collected_10000P),
                    P5000 = (int)(collection.Cash_Collected_5000P),
                    P2000 = (int)(collection.Cash_Collected_2000p),
                    P1000 = (int)(collection.Cash_Collected_1000P),
                    P500 = (int)(collection.Cash_Collected_500P),
                    P200 = (int)(collection.Cash_Collected_200P),
                    P100 = (int)(collection.Cash_Collected_100P),
                    P50 = (decimal)(collection.Cash_Collected_50p),
                    P20 = (decimal)(collection.Cash_Collected_20p),
                    P10 = (decimal)(collection.Cash_Collected_10p),
                    P5 = (decimal)(collection.Cash_Collected_5p),
                    P2 = (decimal)(collection.Cash_Collected_2p),
                    P1 = (decimal)(collection.Cash_Collected_1p),
                    BackColor = "Black",
                    Type = "Part",
                    TicketsInValue = ticketsIn,
                    TicketsOutValue = ticketsOut
                };


                if (collection.Zone_No == null || collection.Zone_No == 0)
                    undeclaredCollectionRecord.Zone = "N/A";
                else
                    undeclaredCollectionRecord.Zone = collection.Zone_Name;


                if (regionSettingValue == "US")
                    undeclaredCollectionRecord.Region = "US";


                if (isAutodeclaration.ToLower() == "true")
                {
                    //Added by Anil to fix CR 88038
                    shortpay = Convert.ToDecimal(collection.ShortPay);
                    handpay = Convert.ToDecimal(collection.Handpay);
                    refund = Convert.ToDecimal(collection.Refunds);
                    refill = Convert.ToDecimal(collection.Refills);
                    jackpot = Convert.ToDecimal(collection.HandpayJackpot);

                    //handpay = Convert.ToDecimal(collection.DeclaredHandpay);

                    undeclaredCollectionRecord.ShortPayValue = shortpay;
                    undeclaredCollectionRecord.HandpayValue = handpay;
                    undeclaredCollectionRecord.RefundValue = refund;
                    undeclaredCollectionRecord.RefillsValue = refill;
                    undeclaredCollectionRecord.JackpotValue = jackpot;
                }
                else
                {
                    shortpay = Convert.ToDecimal(collection.ShortPay);
                    handpay = Convert.ToDecimal(collection.Handpay);
                    refund = Convert.ToDecimal(collection.Refunds);
                    refill = Convert.ToDecimal(collection.Refills);
                    jackpot = Convert.ToDecimal(collection.HandpayJackpot);

                    undeclaredCollectionRecord.ShortPayValue = shortpay;
                    undeclaredCollectionRecord.HandpayValue = handpay;
                    undeclaredCollectionRecord.RefundValue = refund;
                    undeclaredCollectionRecord.RefillsValue = refill;
                    undeclaredCollectionRecord.JackpotValue = jackpot;
                }

                winloss = (undeclaredCollectionRecord.TotalCoinsValue + undeclaredCollectionRecord.TotalBillValue + ticketsIn
                           - ticketsOut - handpay - shortpay - refill - refund - jackpot);

                sumOfticketsIn += ticketsIn;
                sumOfticketsOut += ticketsOut;
                sumOfshortpay += shortpay;
                sumOfhandpay += handpay;
                sumOfrefund += refund;
                sumOfrefill += refill;
                sumOfjackpot += jackpot;
                sumOfWinloss += winloss;

                undeclaredCollectionRecord.WinLossValue = winloss;

                undeclaredCollectionRecords.Add(undeclaredCollectionRecord);
            }

            if (undeclaredCollectionRecords.Count == 0) return null;

            var uc = (from collectionRecord in undeclaredCollectionRecords
                      group collectionRecord by collectionRecord.Region
                          into collectiongroup
                          select new UndeclaredCollectionRecord
                          {
                              // Added bool to handle sort in datagrid
                              IsTotalRow = true,
                              Zone = "Total",
                              BackColor = "Blue",
                              CollectionDate = DateTime.MinValue,
                              CollectionTime = DateTime.MinValue,
                              GameTitle = "",
                              HandpayValue = sumOfhandpay,
                              P1 = collectiongroup.Sum(c => c.P1),
                              P2 = collectiongroup.Sum(c => c.P2),
                              P5 = collectiongroup.Sum(c => c.P5),
                              P10 = collectiongroup.Sum(c => c.P10),
                              P20 = collectiongroup.Sum(c => c.P20),
                              P50 = collectiongroup.Sum(c => c.P50),
                              P100 = collectiongroup.Sum(c => c.P100),
                              P200 = collectiongroup.Sum(c => c.P200),
                              P500 = collectiongroup.Sum(c => c.P500),
                              P1000 = collectiongroup.Sum(c => c.P1000),
                              P2000 = collectiongroup.Sum(c => c.P2000),
                              P5000 = collectiongroup.Sum(c => c.P5000),
                              P10000 = collectiongroup.Sum(c => c.P10000),
                              P20000 = collectiongroup.Sum(c => c.P20000),
                              P50000 = collectiongroup.Sum(c => c.P50000),
                              P100000 = collectiongroup.Sum(c => c.P100000),
                              JackpotValue = sumOfjackpot,
                              Meters = "",
                              Position = "",
                              AssetNo = "",
                              RefillsValue = sumOfrefill,
                              RefundValue = sumOfrefund,
                              ShortPayValue = sumOfshortpay,
                              TicketsInValue = sumOfticketsIn,
                              TicketsOutValue = sumOfticketsOut,
                              Type = "Total",
                              WinLossValue = sumOfWinloss,
                              Collection_Batch_Name = ""
                          }).FirstOrDefault();

            var returnCollection = new List<UndeclaredCollectionRecord> { uc };
            returnCollection.AddRange(undeclaredCollectionRecords);
            return returnCollection;
        }

        private List<UndeclaredCollectionRecord> GetUndeclaredPartCollectionByCollectionNo(int collectionNo,
          DeclarationFilterBy filterBy, string filterValue)
        {
            var undeclaredCollectionRecords = new List<UndeclaredCollectionRecord>();
            var regionSettingValue = string.Empty;
            var isAutodeclaration = "true";
            var i = 0;

            decimal handpay,
                    jackpot = 0,
                    ticketsIn,
                    ticketsOut,
                    shortpay = 0,
                    refund = 0,
                    refill = 0,
                    winloss;

            decimal sumOfhandpay = 0,
                    sumOfjackpot = 0,
                    sumOfticketsIn = 0,
                    sumOfticketsOut = 0,
                    sumOfshortpay = 0,
                    sumOfrefund = 0,
                    sumOfrefill = 0,
                    sumOfWinloss = 0;

            _collectionDataContext.GetSetting(0, "REGION", "US", ref regionSettingValue);
            _collectionDataContext.GetSetting(0, "Auto_Declare_Monies", "true", ref isAutodeclaration);

            foreach (var collection in _collectionDataContext.GetUndeclaredPartCollectionByCollectionNo(collectionNo,
             (int)filterBy, filterValue))
            {
                i++;
                ticketsIn = Convert.ToDecimal((float)collection.TicketsIn);
                ticketsOut = Convert.ToDecimal((float)collection.TicketsOut);
                var undeclaredCollectionRecord = new UndeclaredCollectionRecord
                {
                    ReferenceID = i,
                    Position = collection.Bar_Pos_Name,
                    AssetNo = collection.AssetNo,
                    GameTitle = collection.Name,
                    Installation_Token_Value = (int)collection.Installation_Token_Value,
                    CollectionNo = (int)collection.Collection_No,
                    CollectionBatchNo = collection.Collection_Batch_No,
                    CollectionDate = (DateTime)collection.Date,
                    CollectionTime = (DateTime)collection.Date,
                    InstallationNo = collection.Installation_No,
                    P100000 = (int)(collection.Cash_Collected_100000P),
                    P50000 = (int)(collection.Cash_Collected_50000P),
                    P20000 = (int)(collection.Cash_Collected_20000P),
                    P10000 = (int)(collection.Cash_Collected_10000P),
                    P5000 = (int)(collection.Cash_Collected_5000P),
                    P2000 = (int)(collection.Cash_Collected_2000p),
                    P1000 = (int)(collection.Cash_Collected_1000P),
                    P500 = (int)(collection.Cash_Collected_500P),
                    P200 = (int)(collection.Cash_Collected_200P),
                    P100 = (int)(collection.Cash_Collected_100P),
                    P50 = (decimal)(collection.Cash_Collected_50p),
                    P20 = (decimal)(collection.Cash_Collected_20p),
                    P10 = (decimal)(collection.Cash_Collected_10p),
                    P5 = (decimal)(collection.Cash_Collected_5p),
                    P2 = (decimal)(collection.Cash_Collected_2p),
                    P1 = (decimal)(collection.Cash_Collected_1p),
                    BackColor = "Black",
                    Type = "Part",
                    TicketsInValue = ticketsIn,
                    TicketsOutValue = ticketsOut
                };


                if (collection.Zone_No == null || collection.Zone_No == 0)
                    undeclaredCollectionRecord.Zone = "N/A";
                else
                    undeclaredCollectionRecord.Zone = collection.Zone_Name;


                if (regionSettingValue == "US")
                    undeclaredCollectionRecord.Region = "US";


                if (isAutodeclaration.ToLower() == "true")
                {
                    //Added by Anil to fix CR 88038
                    shortpay = Convert.ToDecimal(collection.ShortPay);
                    handpay = Convert.ToDecimal(collection.Handpay);
                    refund = Convert.ToDecimal(collection.Refunds);
                    refill = Convert.ToDecimal(collection.Refills);
                    jackpot = Convert.ToDecimal(collection.HandpayJackpot);

                    //handpay = Convert.ToDecimal(collection.DeclaredHandpay);

                    undeclaredCollectionRecord.ShortPayValue = shortpay;
                    undeclaredCollectionRecord.HandpayValue = handpay;
                    undeclaredCollectionRecord.RefundValue = refund;
                    undeclaredCollectionRecord.RefillsValue = refill;
                    undeclaredCollectionRecord.JackpotValue = jackpot;
                }
                else
                {
                    shortpay = Convert.ToDecimal(collection.ShortPay);
                    handpay = Convert.ToDecimal(collection.Handpay);
                    refund = Convert.ToDecimal(collection.Refunds);
                    refill = Convert.ToDecimal(collection.Refills);
                    jackpot = Convert.ToDecimal(collection.HandpayJackpot);

                    undeclaredCollectionRecord.ShortPayValue = shortpay;
                    undeclaredCollectionRecord.HandpayValue = handpay;
                    undeclaredCollectionRecord.RefundValue = refund;
                    undeclaredCollectionRecord.RefillsValue = refill;
                    undeclaredCollectionRecord.JackpotValue = jackpot;
                }

                winloss = (undeclaredCollectionRecord.TotalCoinsValue + undeclaredCollectionRecord.TotalBillValue + ticketsIn
                           - ticketsOut - handpay - shortpay - refill - refund - jackpot);

                sumOfticketsIn += ticketsIn;
                sumOfticketsOut += ticketsOut;
                sumOfshortpay += shortpay;
                sumOfhandpay += handpay;
                sumOfrefund += refund;
                sumOfrefill += refill;
                sumOfjackpot += jackpot;
                sumOfWinloss += winloss;

                undeclaredCollectionRecord.WinLossValue = winloss;

                undeclaredCollectionRecords.Add(undeclaredCollectionRecord);
            }

            if (undeclaredCollectionRecords.Count == 0) return null;

            var uc = (from collectionRecord in undeclaredCollectionRecords
                      group collectionRecord by collectionRecord.Region
                          into collectiongroup
                          select new UndeclaredCollectionRecord
                          {
                              // Added bool to handle sort in datagrid
                              IsTotalRow = true,
                              Zone = "Total",
                              BackColor = "Blue",
                              CollectionDate = DateTime.MinValue,
                              CollectionTime = DateTime.MinValue,
                              GameTitle = "",
                              HandpayValue = sumOfhandpay,
                              P1 = collectiongroup.Sum(c => c.P1),
                              P2 = collectiongroup.Sum(c => c.P2),
                              P5 = collectiongroup.Sum(c => c.P5),
                              P10 = collectiongroup.Sum(c => c.P10),
                              P20 = collectiongroup.Sum(c => c.P20),
                              P50 = collectiongroup.Sum(c => c.P50),
                              P100 = collectiongroup.Sum(c => c.P100),
                              P200 = collectiongroup.Sum(c => c.P200),
                              P500 = collectiongroup.Sum(c => c.P500),
                              P1000 = collectiongroup.Sum(c => c.P1000),
                              P2000 = collectiongroup.Sum(c => c.P2000),
                              P5000 = collectiongroup.Sum(c => c.P5000),
                              P10000 = collectiongroup.Sum(c => c.P10000),
                              P20000 = collectiongroup.Sum(c => c.P20000),
                              P50000 = collectiongroup.Sum(c => c.P50000),
                              P100000 = collectiongroup.Sum(c => c.P100000),
                              JackpotValue = sumOfjackpot,
                              Meters = "",
                              Position = "",
                              AssetNo = "",
                              RefillsValue = sumOfrefill,
                              RefundValue = sumOfrefund,
                              ShortPayValue = sumOfshortpay,
                              TicketsInValue = sumOfticketsIn,
                              TicketsOutValue = sumOfticketsOut,
                              Type = "Total",
                              WinLossValue = sumOfWinloss,
                              Collection_Batch_Name = ""
                          }).FirstOrDefault();

            var returnCollection = new List<UndeclaredCollectionRecord> { uc };
            returnCollection.AddRange(undeclaredCollectionRecords);
            return returnCollection;
        }

        private bool PerformFullCollection(int InstallationNo, int batchNo, CollectionType collectionType)
        {
            var errorMsg = string.Empty;
            _collectionDataContext.PerformCollection
                            (batchNo, InstallationNo, Dns.GetHostName(), (collectionType == CollectionType.DefloatCollection) , ref errorMsg,BMC.Security.SecurityHelper.CurrentUser.SecurityUserID);
            if (errorMsg == "SUCCESS")
                return true;
            else
                return false;
        }


        private bool performPartCollection(int batchID ,int InstallationNo, int? userNo)
        {
            int? partCollectionID = 0;
            //var partcollectionDataContext = new CollectionDataContext(new SqlConnection(CommonUtilities.ExchangeConnectionString));
            //_collectionDataContext
            _collectionDataContext.CreatePartCollection(batchID,userNo, InstallationNo,
                DateTime.Now, DateTime.Now,
                Dns.GetHostName(),
                ref partCollectionID);

            if (partCollectionID == null || partCollectionID == 0) 
                return false;
            else
                return true;


			 //var partCollections =
            //    _collectionDataContext.PartCollections.Where(p => p.Part_Collection_No == partCollectionID).Select(
            //        p => p).FirstOrDefault();


            //var prevCollection = (from collection in _collectionDataContext.Collections
            //                      join eventTable in _collectionDataContext.Events on collection.Collection_No equals
            //                          eventTable.Event_Detail
            //                      where ((eventTable.Event_Type == 2000) && (collection.Installation_No == InstallationNo))
            //                      orderby eventTable.Event_No descending
            //                      select new { collection, eventTable }
            //                    ).FirstOrDefault();

            //var prevPartCollection = (from partCollection in _collectionDataContext.PartCollections
            //                          where
            //                              (partCollection.Installation_No == InstallationNo &&
            //                               partCollection.Part_Collection_No != partCollectionID)
            //                          orderby partCollection.Part_Collection_No descending
            //                          select partCollection).FirstOrDefault();


            //var installation = (from installationrecord in _collectionDataContext.Installations
            //                    where installationrecord.Installation_No == InstallationNo
            //                    select installationrecord).FirstOrDefault();


            ////Reference 
            ////No Part, No Prev        fill from installation
            ////No Part, Prev           use prev
            ////Part, No Prev           use part
            ////Part, Prev              Use latest


            //if (prevCollection == null && prevPartCollection == null)
            //{
            //    partCollections.Part_Collection_PreviousCounterCashIn = installation.Coins_In_Counter;
            //    partCollections.Part_Collection_PreviousCounterCashOut = installation.Coins_Out_Counter;
            //    partCollections.Part_Collection_PreviousCounterTokensIn = installation.Tokens_In_Counter;
            //    partCollections.Part_Collection_PreviousCounterTokensOut = installation.Tokens_Out_Counter;
            //    partCollections.Part_Collection_PreviousCollectionDate = installation.Start_Date;
            //}

            //if (prevCollection != null && prevPartCollection == null)
            //{
            //    partCollections.Part_Collection_PreviousCounterCashIn = prevCollection.collection.CounterCashIn;
            //    partCollections.Part_Collection_PreviousCounterCashOut = prevCollection.collection.CounterCashOut;
            //    partCollections.Part_Collection_PreviousCounterTokensIn = prevCollection.collection.CounterTokensIn;
            //    partCollections.Part_Collection_PreviousCounterTokensOut = prevCollection.collection.CounterTokensOut;
            //    partCollections.Part_Collection_PreviousCollectionDate = prevCollection.collection.Collection_Date;
            //}

            //if (prevCollection == null && prevPartCollection != null)
            //{
            //    partCollections.Part_Collection_PreviousCounterCashIn = prevPartCollection.Part_Collection_CounterCashIn;
            //    partCollections.Part_Collection_PreviousCounterCashOut = prevPartCollection.Part_Collection_CounterCashOut;
            //    partCollections.Part_Collection_PreviousCounterTokensIn = prevPartCollection.Part_Collection_CounterTokensIn;
            //    partCollections.Part_Collection_PreviousCounterTokensOut = prevPartCollection.Part_Collection_CounterTokensOut;
            //    partCollections.Part_Collection_PreviousCollectionDate = prevPartCollection.Part_Collection_Date;
            //}

            //if (prevCollection != null && prevPartCollection != null)
            //{
            //    if (prevPartCollection.Part_Collection_Date > prevCollection.eventTable.Date)
            //    {
            //        partCollections.Part_Collection_PreviousCounterCashIn = prevPartCollection.Part_Collection_CounterCashIn;
            //        partCollections.Part_Collection_PreviousCounterCashOut = prevPartCollection.Part_Collection_CounterCashOut;
            //        partCollections.Part_Collection_PreviousCounterTokensIn = prevPartCollection.Part_Collection_CounterTokensIn;
            //        partCollections.Part_Collection_PreviousCounterTokensOut = prevPartCollection.Part_Collection_CounterTokensOut;
            //        partCollections.Part_Collection_PreviousCollectionDate = prevPartCollection.Part_Collection_Date;
            //    }
            //    else
            //    {
            //        partCollections.Part_Collection_PreviousCounterCashIn = prevCollection.collection.CounterCashIn;
            //        partCollections.Part_Collection_PreviousCounterCashOut = prevCollection.collection.CounterCashOut;
            //        partCollections.Part_Collection_PreviousCounterTokensIn = prevCollection.collection.CounterTokensIn;
            //        partCollections.Part_Collection_PreviousCounterTokensOut = prevCollection.collection.CounterTokensOut;
            //        partCollections.Part_Collection_PreviousCollectionDate = prevCollection.collection.Collection_Date;
            //    }
            //}
            //try
            //{
            //    _collectionDataContext.SubmitChanges();
            //    return true;
            //}
            //catch (Exception exception) { ExceptionManager.Publish(exception); }

            //return false;
        }

        #endregion

        #region "Public Methods"

        public int InsertIntoExportHistory(int batchNo)
        {
            return _collectionDataContext.InsertIntoExportHistory(batchNo.ToString(),null, "BATCH", null);
        }


        public ISingleResult<CollectionMachine> GetInstalledMachineForCollection()
        {
            return _collectionDataContext.GetInstalledMachineForCollection();
        }

        public ISingleResult<RouteCollection> GetRouteCollection()
        {
            return _collectionDataContext.GetRouteCollection();
        }

        public ISingleResult<CollectionBatchByMachine> GetCollectionBatchByMachine(string machineName)
        {
            return _collectionDataContext.GetCollectionBatchByMachine(machineName);
        }

        public ISingleResult<BarPositionRouteNo> GetBarPositionByRouteNo(string routeName)
        {
            return _collectionDataContext.GetBarPositionByRouteNo(routeName);
        }

        public int CreateCollectionBatch(string collectionBatchName, int? userNo, ref int? exisitngBatchNo)
        {
            return _collectionDataContext.CreateCollectionBatch
                (collectionBatchName,
                Dns.GetHostName(),
                userNo,
                ref exisitngBatchNo);
        }

        public bool PerformCollection(int InstallationNo, int batchID, int? userNo, CollectionType collectionType)
        {
            return collectionType == CollectionType.PartCollection ? performPartCollection(batchID,InstallationNo, userNo) : PerformFullCollection(InstallationNo, batchID, collectionType);
        }

        public int UpdateEventDetails(int userid , int installationNo)
        {
            return _collectionDataContext.UpdateEventDetails(userid, installationNo);
        }

        public ISingleResult<UndeclaredCollection> GetUndeclaredCollection(bool forPerformCollection, bool IsPartCollectionDeclaration)
        {
            return
                _collectionDataContext.
                    GetUndeclaredCollection(Dns.GetHostName(), forPerformCollection,IsPartCollectionDeclaration);
        }

        public List<UndeclaredCollection> GetUndeclaredCollectionList(bool forPerformCollection, bool IsPartCollectionDeclaration)
        {
            return
                _collectionDataContext.
                    GetUndeclaredCollection(Dns.GetHostName(), forPerformCollection, IsPartCollectionDeclaration).ToList();
        }

        public ISingleResult<UndeclaredCollection> GetUndeclaredDropBatch()
        {
            return _collectionDataContext.GetUndeclaredDropBatch();
        } 

        public List<UndeclaredCollectionRecord> GetUndeclaredCollectionByBatchNo(int batchNo)
        {
            var undeclaredCollectionRecords = new List<UndeclaredCollectionRecord>();
            if (batchNo == -1) return undeclaredCollectionRecords;
            return batchNo == 0 ? GetUndeclaredPartCollectionByBatchNo(0,DeclarationFilterBy.None,"") : GetUndeclaredFullCollection(batchNo, DeclarationFilterBy.None, string.Empty);
        }

        public List<UndeclaredCollectionRecord> GetUndeclaredCollectionByBatchNo(int batchNo,
            DeclarationFilterBy filterBy, string filterValue)
        {
            var undeclaredCollectionRecords = new List<UndeclaredCollectionRecord>();
            if (batchNo == -1) return undeclaredCollectionRecords;
            return batchNo == 0 ? GetUndeclaredPartCollectionByBatchNo(batchNo, filterBy, filterValue) : GetUndeclaredFullCollection(batchNo, filterBy, filterValue);
        }

        public List<UndeclaredCollectionRecord> GetUndeclaredCollectionByCollectionNo(int collectionNo,
           DeclarationFilterBy filterBy, string filterValue)
        {
            var undeclaredCollectionRecords = new List<UndeclaredCollectionRecord>();
            if (collectionNo == -1) return undeclaredCollectionRecords;
            return GetUndeclaredPartCollectionByCollectionNo(collectionNo, filterBy, filterValue);
        }

        public void PrintCollectionBatch(IList<UndeclaredCollectionRecord> collectionRecords, string userName)
        {
            var lineHeader = new DataTable();
            var lineItem = new DataTable();
            var isFirstRecord = true;

            lineHeader.Columns.Add("Date");
            lineHeader.Columns.Add("DropNo");
            lineHeader.Columns.Add("NoOfMachine");
            lineHeader.Columns.Add("user");

            lineHeader.Columns.Add("Bills");
            lineHeader.Columns.Add("CoinsIn");
            lineHeader.Columns.Add("TicketsIn");
            lineHeader.Columns.Add("EFTIn");
            lineHeader.Columns.Add("TotalCashIn");

            lineHeader.Columns.Add("TicketsOut");
            lineHeader.Columns.Add("EFTOut");
            lineHeader.Columns.Add("CancelledCredits");
            lineHeader.Columns.Add("Jackpots");
            lineHeader.Columns.Add("CoinsOut");
            lineHeader.Columns.Add("ShortPay");
            lineHeader.Columns.Add("TotalCashOut");            

            lineHeader.Columns.Add("NetWin");

            lineItem.Columns.Add("Asset");
            lineItem.Columns.Add("Pos");

            lineItem.Columns.Add("Bills");
            lineItem.Columns.Add("CoinsIn");
            lineItem.Columns.Add("TicketsIn");
            lineItem.Columns.Add("EFTIn");
            lineItem.Columns.Add("TotalCashIn");

            lineItem.Columns.Add("TicketsOut");
            lineItem.Columns.Add("EFTOut");
            lineItem.Columns.Add("CancelledCredits");
            lineItem.Columns.Add("Jackpots");
            lineItem.Columns.Add("CoinsOut");
            lineItem.Columns.Add("ShortPay");
            lineItem.Columns.Add("TotalCashOut");

            lineItem.Columns.Add("NetWin");

            foreach (var collectionRecord in collectionRecords)
            {
                var dr = isFirstRecord ? lineHeader.NewRow() : lineItem.NewRow();
                dr["Bills"] = collectionRecord.TotalBills;
                dr["CoinsIn"] = collectionRecord.TotalCoins;
                dr["TicketsIn"] = collectionRecord.TicketsIn;
                dr["EFTIn"] = collectionRecord.EFTIn ;
                dr["TotalCashIn"] = (collectionRecord.TotalBillValue + collectionRecord.TotalCoinsValue + collectionRecord.TicketsInValue + collectionRecord.EFTInValue).GetUniversalCurrencyFormatWithSymbol();
                
                dr["TicketsOut"] = (Settings.Client == "SGVI" & Settings.TicketDeclaration == "AUTO") ?
                                    (collectionRecord.TicketsOutValue).GetUniversalCurrencyFormatWithSymbol()
                                    : (collectionRecord.TicketsOutValue).GetUniversalCurrencyFormatWithSymbol();

                dr["EFTOut"] = collectionRecord.EFTOut;
                dr["CancelledCredits"] = collectionRecord.Handpay;
                dr["Jackpots"] = collectionRecord.Jackpot;
                dr["CoinsOut"] = ((decimal)collectionRecord.CoinOutValue).GetUniversalCurrencyFormatWithSymbol();
                dr["ShortPay"] = collectionRecord.ShortPay;

                // ShortPayValue should not be added in TotalCashOut as its been included already in VouchersOut, if AddShortpayInVoucherOut is set to true
                if (!Settings.AddShortpayInVoucherOut)
                {
                    dr["TotalCashOut"] = (Settings.Client == "SGVI" & Settings.TicketDeclaration == "AUTO") ?
                                        (collectionRecord.TicketsOutValue + collectionRecord.HandpayValue
                                        + collectionRecord.JackpotValue + collectionRecord.CoinOutValue
                                        + collectionRecord.EFTOutValue).GetUniversalCurrencyFormatWithSymbol()
                                        : (collectionRecord.TicketsOutValue + collectionRecord.ShortPayValue + collectionRecord.HandpayValue
                                        + collectionRecord.JackpotValue + collectionRecord.CoinOutValue
                                        + collectionRecord.EFTOutValue).GetUniversalCurrencyFormatWithSymbol();
                }
                else
                {
                    dr["TotalCashOut"] = (Settings.Client == "SGVI" & Settings.TicketDeclaration == "AUTO") ?
                                        (collectionRecord.TicketsOutValue + collectionRecord.HandpayValue
                                        + collectionRecord.JackpotValue + collectionRecord.CoinOutValue
                                        + collectionRecord.EFTOutValue).GetUniversalCurrencyFormatWithSymbol()
                                        : (collectionRecord.TicketsOutValue  + collectionRecord.HandpayValue
                                        + collectionRecord.JackpotValue + collectionRecord.CoinOutValue
                                        + collectionRecord.EFTOutValue).GetUniversalCurrencyFormatWithSymbol();
                }

                // ShortPayValue should not be added in TotalCashOut as its been included already in VouchersOut, if AddShortpayInVoucherOut is set to true
                if (!Settings.AddShortpayInVoucherOut)
                {
                dr["NetWin"] = (Settings.Client=="SGVI" & Settings.TicketDeclaration=="AUTO")?
                    ((collectionRecord.TotalBillValue + collectionRecord.TotalCoinsValue + collectionRecord.TicketsInValue + collectionRecord.EFTInValue)
                    - (collectionRecord.TicketsOutValue + collectionRecord.HandpayValue + collectionRecord.JackpotValue  + collectionRecord.CoinOutValue + collectionRecord.EFTOutValue)).GetUniversalCurrencyFormatWithSymbol()
                    :((collectionRecord.TotalBillValue + collectionRecord.TotalCoinsValue + collectionRecord.TicketsInValue + collectionRecord.EFTInValue)
                    - (collectionRecord.TicketsOutValue + collectionRecord.HandpayValue + collectionRecord.JackpotValue + collectionRecord.ShortPayValue + collectionRecord.CoinOutValue + collectionRecord.EFTOutValue)).GetUniversalCurrencyFormatWithSymbol();
                }
                else
                {
                    dr["NetWin"] = (Settings.Client == "SGVI" & Settings.TicketDeclaration == "AUTO") ?
                        ((collectionRecord.TotalBillValue + collectionRecord.TotalCoinsValue + collectionRecord.TicketsInValue + collectionRecord.EFTInValue)
                        - (collectionRecord.TicketsOutValue + collectionRecord.HandpayValue + collectionRecord.JackpotValue + collectionRecord.CoinOutValue + collectionRecord.EFTOutValue)).GetUniversalCurrencyFormatWithSymbol()
                        : ((collectionRecord.TotalBillValue + collectionRecord.TotalCoinsValue + collectionRecord.TicketsInValue + collectionRecord.EFTInValue)
                        - (collectionRecord.TicketsOutValue + collectionRecord.HandpayValue + collectionRecord.JackpotValue + collectionRecord.CoinOutValue + collectionRecord.EFTOutValue)).GetUniversalCurrencyFormatWithSymbol();
                }

                if (isFirstRecord)
                {
                    dr["Date"] = DateTime.Now.GetUniversalDateFormat();

                    dr["DropNo"] = collectionRecords[1].CollectionBatchNo == 0
                                       ? (object)"Interim Collection"
                                       : collectionRecords[1].CollectionBatchNo;

                    dr["NoOfMachine"] = collectionRecords.Count - 1;
                    dr["user"] = userName;
                    lineHeader.Rows.Add(dr);
                }
                else
                {
                    dr["Asset"] = collectionRecord.AssetNo;
                    dr["Pos"] = collectionRecord.Position;
                    lineItem.Rows.Add(dr);
                }
                isFirstRecord = false;
            }

            var printUtility = new PrintUtility();
            //printUtility.Print(lineHeader, lineItem, BMCRegistryHelper.GetRegKeyValue(string.Empty, "InstallationPath").ToString().Trim() + @"Print/ManualReceipt.html");
            printUtility.Print(lineHeader, lineItem, Path.Combine(Extensions.GetStartupDirectory(), @"Print\ManualReceipt.html"));
        }

        public void AddCollectionToPartCollection(string c500, string c200, string c100, string c50, string c20, string c10, string c5,
            string c2, string c1, string coins, string ticketsIn, int tokenValue, int collectionNo)
        {
           // var partcollectionDataContext = new CollectionDataContext(new SqlConnection(ExchangeConnection));
            var partcollection = (from newcollection in _collectionDataContext.PartCollections
                                  where newcollection.Collection_No == collectionNo &&
                                  (bool)newcollection.Part_Collection_Declared == false &&
                                  newcollection.Part_Collection_Machine == Dns.GetHostName()
                                  select newcollection).FirstOrDefault();

            partcollection.Part_Collection_Cash_Collected_50000p = float.Parse(c500);
            partcollection.Part_Collection_Cash_Collected_20000p = float.Parse(c200);
            partcollection.Part_Collection_Cash_Collected_10000p = float.Parse(c100);
            partcollection.Part_Collection_Cash_Collected_5000p = float.Parse(c50);
            partcollection.Part_Collection_Cash_Collected_2000p = float.Parse(c20);
            partcollection.Part_Collection_Cash_Collected_1000p = float.Parse(c10);
            partcollection.Part_Collection_Cash_Collected_500p = float.Parse(c5);
            partcollection.Part_Collection_ValueTickets = ticketsIn.GetFloatFromString();
            partcollection.Part_Collection_Cash_Collected_200p = float.Parse(c2);
            partcollection.Part_Collection_Cash_Collected_100p = float.Parse(c1);
         //   partcollection.COLLECTION_RDC_HANDPAY = Convert.ToInt32(Attendantpay);



            if (tokenValue == 200)
                partcollection.Part_Collection_Cash_Collected_200p = coins.GetFloatFromString();
            if (tokenValue == 100)
                partcollection.Part_Collection_Cash_Collected_100p = coins.GetFloatFromString();
            if (tokenValue == 50)
                partcollection.Part_Collection_Cash_Collected_50p = coins.GetFloatFromString();
            if (tokenValue == 25)
                partcollection.Part_Collection_Cash_Collected_5p = coins.GetFloatFromString();
            if (tokenValue == 20)
                partcollection.Part_Collection_Cash_Collected_20p = coins.GetFloatFromString();
            if (tokenValue == 10)
                partcollection.Part_Collection_Cash_Collected_10p = coins.GetFloatFromString();
            if (tokenValue == 5)
                partcollection.Part_Collection_Cash_Collected_5p = coins.GetFloatFromString();
            if (tokenValue == 2)
                partcollection.Part_Collection_Cash_Collected_2p = coins.GetFloatFromString();
            if (tokenValue == 1)
                partcollection.Part_Collection_Cash_Collected_1p = coins.GetFloatFromString();

            partcollection.Part_Collection_CashCollected = partcollection.Part_Collection_Cash_Collected_50000p +
                                                           partcollection.Part_Collection_Cash_Collected_20000p +
                                                           partcollection.Part_Collection_Cash_Collected_10000p +
                                                           partcollection.Part_Collection_Cash_Collected_5000p +
                                                           partcollection.Part_Collection_Cash_Collected_2000p +
                                                           partcollection.Part_Collection_Cash_Collected_1000p +
                                                           partcollection.Part_Collection_Cash_Collected_500p +
                                                           partcollection.Part_Collection_Cash_Collected_200p +
                                                           partcollection.Part_Collection_Cash_Collected_100p +
                                                           partcollection.Part_Collection_Cash_Collected_50p +
                                                           partcollection.Part_Collection_Cash_Collected_20p +
                                                           partcollection.Part_Collection_Cash_Collected_10p +
                                                           partcollection.Part_Collection_Cash_Collected_5p +
                                                           partcollection.Part_Collection_Cash_Collected_2p +
                                                           partcollection.Part_Collection_Cash_Collected_1p +
                                                           partcollection.Part_Collection_ValueTickets;
            _collectionDataContext.SubmitChanges();
        }

        public void AddCollectionToFullCollection(string c500, string c200, string c100, string c50, string c20, string c10,
            string c5, string c2, string c1, string coins, string ticketsIn,  int tokenValue, int collectionNo)
        {
            //var collectionDataContext = new CollectionDataContext(new SqlConnection(ExchangeConnection));
            var collection = (from newcollection in _collectionDataContext.Collections
                              where newcollection.Collection_No == collectionNo
                              select newcollection).FirstOrDefault();

            collection.Cash_Collected_50000p = float.Parse(c500);
            collection.Cash_Collected_20000p = float.Parse(c200);
            collection.Cash_Collected_10000p = float.Parse(c100);
            collection.Cash_Collected_5000p = float.Parse(c50);
            collection.Cash_Collected_2000p = float.Parse(c20);
            collection.Cash_Collected_1000p = float.Parse(c10);
            collection.Cash_Collected_500p = float.Parse(c5);
            collection.Cash_Collected_200p = float.Parse(c2);
            collection.Cash_Collected_100p = float.Parse(c1);

            collection.DeclaredTicketValue = ticketsIn.GetFloatFromString();
            //collection.COLLECTION_RDC_HANDPAY = Attendantpay.GetSingleFromString();

            if (!Common.Utilities.ExtensionMethods.CurrentSiteCulture.ToUpper().Contains("US"))
            {
                if (tokenValue == 100)
                    collection.Cash_Collected_100p = coins.GetFloatFromString();
            }
            if (tokenValue == 50)
                collection.Cash_Collected_50p = coins.GetFloatFromString();
            if (tokenValue == 20)
                collection.Cash_Collected_20p = coins.GetFloatFromString();
            if (tokenValue == 25)
                collection.Cash_Collected_5p = coins.GetFloatFromString();
            if (tokenValue == 10)
                collection.Cash_Collected_10p = coins.GetFloatFromString();
            if (tokenValue == 5)
                collection.Cash_Collected_5p = coins.GetFloatFromString();
            if (tokenValue == 2)
                collection.Cash_Collected_2p = coins.GetFloatFromString();
            if (tokenValue == 1)
                collection.Cash_Collected_1p = coins.GetFloatFromString();

            _collectionDataContext.SubmitChanges();
        }

        private int InsertCollectionDeclaration(
            string collectionType, int? installationNo, int? collectionNo, int? userID, bool? manual, bool? forceCash,
            bool? forceMeters, double? vAtRate, bool? handHeldsActive, decimal? declarationCoinsIn, decimal? declarationCoinsOut,
            decimal? declarationCoinDrop, decimal? declarationHandPay, decimal? declarationExternalCredit, int? declarationGamesBet,
            int? declarationGamesWon, decimal? declarationNotes, decimal? declarationCashCashOut, decimal? declarationCashTokensOut,
            decimal? declarationCashTokenRefills, decimal? declarationCoinBreakDown1P, decimal? declarationCoinBreakDown2P, decimal? declarationCoinBreakDown5P,
            decimal? declarationCoinBreakDown10P, decimal? declarationCoinBreakDown20P, decimal? declarationCoinBreakDown50P,
            decimal? declarationCoinBreakDown100P, decimal? declarationCoinBreakDown200P, decimal? declarationCoinBreakDown500P,
            decimal? declarationCoinBreakDown1000P, decimal? declarationCoinBreakDown2000P, decimal? declarationCoinBreakDown5000P,
            decimal? declarationCoinBreakDown10000P, decimal? declarationCoinBreakDown20000P, decimal? declarationCoinBreakDown50000P,
            decimal? declarationCoinBreakDown100000P, decimal? declarationTicketValue, int? declarationTicketQty, decimal? declarationMetersCashIn,
            decimal? declarationMetersCashOut, decimal? declarationMetersTokensIn, decimal? declarationMetersTokensOut,
            decimal? declarationMetersPrize, decimal? declarationMetersJukebox, decimal? declarationMetersTournament, decimal? declarationMetersRefills)
        {
            return _collectionDataContext.InsertCollectionDeclaration(collectionType, installationNo, collectionNo, userID,
                                                                      manual, forceCash, forceMeters, vAtRate, handHeldsActive, declarationCoinsIn,
                                                                      declarationCoinsOut, declarationCoinDrop, declarationHandPay, declarationExternalCredit,
                                                                      declarationGamesBet, declarationGamesWon, declarationNotes,
                                                                      declarationCashCashOut, declarationCashTokensOut, declarationCashTokenRefills,
                                                                      declarationCoinBreakDown1P, declarationCoinBreakDown2P, declarationCoinBreakDown5P, declarationCoinBreakDown10P,
                                                                      declarationCoinBreakDown20P, declarationCoinBreakDown50P, declarationCoinBreakDown100P,
                                                                      declarationCoinBreakDown200P, declarationCoinBreakDown500P, declarationCoinBreakDown1000P,
                                                                      declarationCoinBreakDown2000P, declarationCoinBreakDown5000P, declarationCoinBreakDown10000P,
                                                                      declarationCoinBreakDown20000P, declarationCoinBreakDown50000P, declarationCoinBreakDown100000P,
                                                                      declarationTicketValue, declarationTicketQty, declarationMetersCashIn, declarationMetersCashOut,
                                                                      declarationMetersTokensIn, declarationMetersTokensOut, declarationMetersPrize, declarationMetersJukebox,
                                                                      declarationMetersTournament, declarationMetersRefills);
        }

        public bool SavePartCollection(int partCollectioNo, int userID)
        {
           // var partcollectionDataContext = new CollectionDataContext(new SqlConnection(ExchangeConnection));
            var partCollections = from partCollection in _collectionDataContext.PartCollections
                                  where partCollection.Part_Collection_No == partCollectioNo &&
                                  (bool)partCollection.Part_Collection_Declared == false &&
                                  partCollection.Part_Collection_Machine == Dns.GetHostName()
                                  select partCollection;
            int installationNo = 0;
            foreach (var partCollection in partCollections)
            {
                partCollection.Part_Collection_Declared = true;
                partCollection.User_No = userID;
                installationNo = (int)partCollection.Installation_No;
            }
            _collectionDataContext.SubmitChanges();
            //Delete lock is commented for Multiple drop funtionality. Logic is added in Perform collection itself
            //var lockHelper = new LockHandler(exchangeConnectionString);
            //lockHelper.DeleteLockRecord(userID, Dns.GetHostName(), "COLLECT", "INST", installationNo.ToString());
            return true;
        }
        public bool DeclarePartCollection(int userID,int Part_Collection_No)
        {
            var lockHelper = new LockHandler(exchangeConnectionString);
            _collectionDataContext.SavePartDeclaration(Dns.GetHostName(), userID, Part_Collection_No);
          
            return true;

        }
        public bool SaveFullCollection(UndeclaredCollectionRecord undeclaredCollectionRecord, int userId)
        {
            var collectionType = undeclaredCollectionRecord.Type == "Defloat" ? "DC" : "FC";
            if (InsertCollectionDeclaration(collectionType, undeclaredCollectionRecord.InstallationNo,
                                               undeclaredCollectionRecord.CollectionNo, userId, undeclaredCollectionRecord.TotalAmountValue > 0 ? true : false, false, false, 0.175,
                                               false,
                                               undeclaredCollectionRecord.TotalCoinsValue, 0, 0, undeclaredCollectionRecord.HandpayValue, 0, 0, 0, 0, 0, 0, 0, undeclaredCollectionRecord.P1, undeclaredCollectionRecord.P2, undeclaredCollectionRecord.P5, undeclaredCollectionRecord.P10, undeclaredCollectionRecord.P20, undeclaredCollectionRecord.P50, undeclaredCollectionRecord.P100, undeclaredCollectionRecord.P200, undeclaredCollectionRecord.P500, undeclaredCollectionRecord.P1000,
                                               undeclaredCollectionRecord.P2000, undeclaredCollectionRecord.P5000, undeclaredCollectionRecord.P10000, undeclaredCollectionRecord.P20000, undeclaredCollectionRecord.P50000, undeclaredCollectionRecord.P100000, undeclaredCollectionRecord.TicketsInValue, 0, 0, 0, 0, 0, 0, 0, 0, 0) == 0)
            {
                LogManager.WriteLog("InsertCollectonDeclaration", LogManager.enumLogLevel.Info);
                var lockHelper = new LockHandler(exchangeConnectionString);
                lockHelper.DeleteLockRecord(userId, Dns.GetHostName(), "COLLECT", "INST", undeclaredCollectionRecord.InstallationNo.ToString());
                return true;
            }

            return false;


        }

        public List<FullWeekCollectionData> GetCollectionWeekData(int iWeekCount)
        {
            var fullWeekCollectionDatas = new List<FullWeekCollectionData>();
            var IsAFTIncludedInCalculation = "FALSE";
            _collectionDataContext.GetSetting(0, "IsAFTIncludedInCalculation", "FALSE", ref IsAFTIncludedInCalculation);
            foreach (var collectionWeekData in _collectionDataContext.GetCollectionWeekData(iWeekCount))
            {
                fullWeekCollectionDatas.Add(new FullWeekCollectionData
                {
                    WeekNumber = collectionWeekData.WeekNumber,
                    Dates = collectionWeekData.WeekStartDate.GetUniversalDateFormat() + " - " + collectionWeekData.WeekEndDate.GetUniversalDateFormat(),
                    NoOfMachineDrops = collectionWeekData.CollectionCount,
                    WinLoss = (Convert.ToDecimal(collectionWeekData.CashTake) + (((IsAFTIncludedInCalculation.ToUpper() == "TRUE")) ? (Convert.ToDecimal(collectionWeekData.Eft) / 100 ): 0 )).GetUniversalCurrencyFormatWithSymbol(),
                    WinLossVariance = Convert.ToDecimal(collectionWeekData.TakeVar).GetUniversalCurrencyFormatWithSymbol(),
                    WeekId = collectionWeekData.WeekNo,
                    BatchId = collectionWeekData.BatchNo
                });
            }
            return fullWeekCollectionDatas;
        }

        public List<FullBatchCollectionData> GetCollectionBatchData(bool isChecked)
        {
            var fullBatchCollectionDatas = new List<FullBatchCollectionData>();
            foreach (var collectionBatchData in _collectionDataContext.GetCollectionBatchData(isChecked ? 20 : 2000))
            {
                fullBatchCollectionDatas.Add(new FullBatchCollectionData
                {
                    Number = collectionBatchData.Collection_Batch_No,
                    Route = collectionBatchData.Collection_Batch_Name,
                    DateCollected = (DateTime)collectionBatchData.Collection_Batch_Date_Performed,
                    GamingDate = (DateTime)collectionBatchData.Collection_Batch_Date
                });
            }
            return fullBatchCollectionDatas;
        }

        public List<PartBatchCollectionData> GetPartCollectionBatchData(bool isChecked)
        {
            var partBatchCollectionDatas = new List<PartBatchCollectionData>();
            foreach (var collectionBatchData in _collectionDataContext.GetPartCollectionBatchData(isChecked ? 20 : 2000))
            {
                partBatchCollectionDatas.Add(new PartBatchCollectionData
                {
                    BarPosition = collectionBatchData.Bar_Pos_Name,
                    Cash = Convert.ToDecimal((float)collectionBatchData.Part_Collection_CashCollected).GetUniversalCurrencyFormatWithSymbol(),
                    dCash=Convert.ToDecimal((float)collectionBatchData.Part_Collection_CashCollected),
                    Date = collectionBatchData.Part_Collection_Date.GetUniversalDateFormat(),
                    DateTime= collectionBatchData.Part_Collection_Date.Value,
                    Machine = collectionBatchData.Name,
                    Time = collectionBatchData.Part_Collection_Date.GetUniversalTimeFormat()
                });                          
            }
            return partBatchCollectionDatas;
        }
              
        public ObservableCollection<rsp_GetDeclaredTicketResult> GetDeclaredTicket(int iCollectionID, int iInstallationID)
        {
            ObservableCollection<rsp_GetDeclaredTicketResult> objcollection = new ObservableCollection<rsp_GetDeclaredTicketResult>();
            foreach (var item in _collectionDataContext.rsp_GetDeclaredTicket(iCollectionID, iInstallationID).ToList<rsp_GetDeclaredTicketResult>())
            {
                objcollection.Add(new rsp_GetDeclaredTicketResult { BarCode = item.BarCode, Value = item.Value, ID = item.ID });
            }

            return objcollection;
        }



        public int InsertDeclaredTicket(string barcode, decimal value, int user, int printed_Installation_ID,
            int printed_Collection_ID, int inserted_Installation_ID, int inserted_Collection_ID, ref int? retDeclaredTicketID)
        {
            return _collectionDataContext.usp_InsertDeclaredTicket(barcode, value, user, printed_Installation_ID,
                    printed_Collection_ID, inserted_Installation_ID, inserted_Collection_ID, ref retDeclaredTicketID);

        }

        public int DeleteDeclaredTicket(int ticket_ID, int installation_ID, int collection_ID)
        {
            return _collectionDataContext.dsp_DeleteDeclaredTicket(ticket_ID, installation_ID, collection_ID);
        }

        public int GetValidationLength(int Installation_ID, ref int? ValidationLength)
        {
            return _collectionDataContext.rsp_GetValidationLength(Installation_ID, ref ValidationLength);
        }

        public int IsPaidTicket(string Barcode, int Installation_ID, ref int? Count, ref int? Amt)
        {
            return _collectionDataContext.rsp_IsPaidTicket(Barcode, Installation_ID, ref Count, ref Amt);
        }

        public int UpdateVoucherCollection(string Barcode, string Value)
        {
            return _collectionDataContext.usp_UpdateVoucherCollection(Barcode, Value);
        }

        public int GetHandPayPlayCreditStatus(CollectionMachine colMachine)
        {
            //var collectionDataContext = new CollectionDataContext(new SqlConnection(CommonUtilities.ExchangeConnectionString));
            int nReturn = 0;

            foreach (var collection in _collectionDataContext.GetHandPayPlayCreditStatus(colMachine.Installation_No))
            {
                if (collection.IsHandPayUnProcessed != 0)
                    nReturn = 1;
                else if (collection.isCardedPlay != 0)
                    nReturn = 3;
                else if (collection.inplay != 0)
                    nReturn = 2;

            }
            return nReturn;
        }

        public List<CreditStatus> GetHandPayPlayCreditStatus(string installNos)
        {
            return  _collectionDataContext.GetHandPayPlayCreditStatus(installNos).ToList();
        }

        public int GetHandPayPlayCreditStatus(int Installation_No)
        {
            //var collectionDataContext = new CollectionDataContext(new SqlConnection(CommonUtilities.ExchangeConnectionString));
            int nReturn = 0;

            foreach (var collection in _collectionDataContext.GetHandPayPlayCreditStatus(Installation_No))
            {
                if (collection.IsHandPayUnProcessed != 0)
                    nReturn = 1;
                else if (collection.isCardedPlay != 0)
                    nReturn = 3;
                else if (collection.inplay != 0)
                    nReturn = 2;

            }
            return nReturn;

        }




        public List<CashCounterCollectionResult> GetCashCounterCollectionResult(int Batch_No)
        {
            List<CashCounterCollectionResult> CashCounterCollection = new List<CashCounterCollectionResult>();
            if (Batch_No == -1)
                return CashCounterCollection;

            return _collectionDataContext.GetCashCounterCollection(Batch_No).ToList();
        }

        public bool IsEventsUnCleared(string InstallationNos)
        {
            return _collectionDataContext.IsEventUnCleared(InstallationNos).Value;
        }

        public bool IsEventsUnCleared(int Installation_No)
        {
            bool bReturn = false;

            if (_collectionDataContext.fnIsEventUnCleared(Installation_No).Value)
            {
                bReturn = true;
            }

            return bReturn;
        }

        #endregion


        public bool HasUndeclaredCollecion(int Installation_No)
        {
            return _collectionDataContext.HasUndeclaredCollection(Installation_No).Value;
        }
        public int UpdateAutoDropSession()
        {
            return _collectionDataContext.UpdateAutoDropSession();
        }
        public bool IsAuthorized(int UserId, string ObjectName)
        {
            return _collectionDataContext.IsAuthorized(UserId, ObjectName).Value;
        }
        public int CreateDropSession(string installationNos, int finalDrop, int? userNo, int BatchNo, string BatchType)
        {
            return _collectionDataContext.CreateDropSession(installationNos, finalDrop, userNo, BatchNo, BatchType, Dns.GetHostName());
        }
        public int AcceptDeclarationforAutoCollection(int? BatchID, ref string Error)
        {
            return _collectionDataContext.AcceptDeclarationforAutoCollection(BatchID, ref Error);
        }
        public List<InstallationData> GetNotdisabledMachines(string installationNos)
        {
            return _collectionDataContext.GetNotdisabledMachines(installationNos).ToList();
        }
        public string IsMachinesNotDisabled(string installationNos)
        {
            return _collectionDataContext.IsMachinesNotDisabled(installationNos);
        }
        public string IsStackerEventNotReceived(string installationNos)
        {
            return _collectionDataContext.IsStackerEventNotReceived(installationNos);
        }

        public bool RevertFinalDropStatus(int installation_No)
        {
            try
            {
                _collectionDataContext.RevertFinalDropStatus(installation_No);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public List<StockNumber> GetInstStatusForAutoDrop(string InstallationNos)
        {
            return _collectionDataContext.GetInstStatusForAutoDrop(InstallationNos).ToList();
        }
        public decimal GetVoucherInValueByCollectionNo(int? Collection_no)
        {
            return (decimal)_collectionDataContext.GetVoucherInValueByCollectionNo(Collection_no);
        }
        public string AutoDropSession(int? Batch_ID)
        {
            return _collectionDataContext.AutoDropSession(Batch_ID);
        }
        public int IsDropSessionActive()
        {
            return _collectionDataContext.GetActiveDropSession();
        }
        public bool IsDropSessionCompleted(string AutoDropSessionNos)
        {
            return _collectionDataContext.IsDropSessionCompleted(AutoDropSessionNos);
        }
        public bool IsBatchProcessedByAnotherUser(int BatchID, int PartCollectionNo)
        {
            return _collectionDataContext.IsBatchProcessedByAnotherUser(BatchID, PartCollectionNo);
        }
        public List<InstallationData> GetDropActiveSessionData()
        {
            return _collectionDataContext.GetDropActiveSessionData().ToList();
        }

        public bool IsUndeclaredPartCollection(int installation_no)
        {
            bool retval = false;
            List<rsp_IsUndeclaredPartCollectionResult> lst_ispart = _collectionDataContext.IsUndeclaredPartCollection(installation_no).ToList();
            if (lst_ispart.Count > 0)
            {
                retval = lst_ispart[0].COLLECTED.Value;
            }
            return retval;
        }
        public ISingleResult<UndeclaredCollection> GetUndeclaredCollectionForAutoDrop(bool forPerformCollection)
        {
            return
                _collectionDataContext.
                    GetUndeclaredCollectionForAutoDrop(Dns.GetHostName(), forPerformCollection);
        }

        public bool IsNoteCounterVisible()
        {
            try
            {
                bool isVisible = true;
                _collectionDataContext.IsNoteCounterVisible(ref isVisible);
                return isVisible;
            }
            catch (Exception ex)
            {
                return true;
            }
        }

        public bool DiscardExceptionVoucherChanges(int collection_No, int installation_No, bool commit)
        {
            try
            {
                _collectionDataContext.DiscardExceptionVoucherChanges(collection_No, installation_No, commit);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<GetDropAlertDataResult> GetDropAlertData(int batchNo)
        {
            List<GetDropAlertDataResult> getDropAlertDataResult = new List<GetDropAlertDataResult>();
            foreach (var dropAlertData in _collectionDataContext.GetDropAlertData(batchNo))
            {
                getDropAlertDataResult.Add(new GetDropAlertDataResult
                {
                    Source = dropAlertData.Source.Trim(),
                    BMCVersion = dropAlertData.BMCVersion.Trim(),
                    ExceptionCode = dropAlertData.ExceptionCode, 
                    OperatorId = dropAlertData.OperatorId,
                    SubCode = dropAlertData.SubCode.Trim(),
                    Company = dropAlertData.Company.Trim(),
                    Region = dropAlertData.Region.Trim(),
                    Area = dropAlertData.Area.Trim(),
                    SiteId = dropAlertData.SiteId.Trim(),
                    SiteName = dropAlertData.SiteName.Trim(),
                    Asset = dropAlertData.Asset.Trim(),
                    Stand = dropAlertData.Stand.Trim(),
                    DropType = dropAlertData.DropType.Trim(),
                    DropDate = dropAlertData.DropDate, 
                    DropPositionsList = dropAlertData.DropPositionsList,
                    DropScheduleId = dropAlertData.DropScheduleId,
                    BatchNumber = dropAlertData.BatchNumber,
                    EmployeeCardNumber = dropAlertData.EmployeeCardNumber.Trim(),
                    EmployeeName = dropAlertData.EmployeeName.Trim(),
                    MessageDateTime = dropAlertData.MessageDateTime,
                   
                });
            }
            return getDropAlertDataResult;
        }

        public int Insert_STM_Export_History(String type, Int32 clientID, String site_Code, System.Xml.Linq.XElement xmlMessage)
        {
            return _collectionDataContext.Insert_STM_Export_History(type, clientID, site_Code, xmlMessage);
        }

        public int ResetStackerLevel(string installationNos)
        {
            return _collectionDataContext.ResetStackerLevel(installationNos);
        }

        public int CheckPreviousDeclarationStatus(int nPartCollectionId,int nCollectionId,int InstallationNo)
        {
            return _collectionDataContext.CheckPreviousDeclarationStatus(nPartCollectionId,nCollectionId, InstallationNo);
            
        }


        #region Declaration Filtering Columns
        private static bool _isInitialized = false;
        private static object _lockDecl = new object();
        private static List<DeclarationFilterColumn> _declFilterColumns = null;

        public List<DeclarationFilterColumn> GetDeclarationFilterColumns(System.Windows.FrameworkElement source)
        {
            if (!_isInitialized)
            {
                lock (_lockDecl)
                {
                    if (!_isInitialized)
                    {
                        _declFilterColumns = new List<DeclarationFilterColumn>()
                        {
                            new DeclarationFilterColumn { DisplayName = GetResourceValue(source, "CDeclaration_FilterBy_All", "All"), 
                                ValueName = DeclarationFilterBy.None},
                           new DeclarationFilterColumn { DisplayName = GetResourceValue(source, "CDeclaration_FilterBy_GameTitle", "GameTitle"), 
                                ValueName = DeclarationFilterBy.GameTitle},
                           new DeclarationFilterColumn { DisplayName = GetResourceValue(source, "CDeclaration_FilterBy_Position", "Position"), 
                                ValueName = DeclarationFilterBy.Position},
                            new DeclarationFilterColumn { DisplayName = GetResourceValue(source, "CDeclaration_FilterBy_Type", "Type"), 
                                ValueName = DeclarationFilterBy.Type}
                        };
                        _isInitialized = true;
                    }
                }
            }

            return _declFilterColumns;
        }

        private string GetResourceValue(System.Windows.FrameworkElement source, string key, string defaultValue)
        {
            object value = source.TryFindResource(key);
            if (value == null) value = defaultValue;
            return value.ToString();
        }

        #endregion
    }
}
