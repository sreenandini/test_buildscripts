using System;
using System.Data;
using BMC.Business.CashDeskOperator.WebServices;
using BMC.Common.ConfigurationManagement;
using BMC.Common.ExceptionManagement;
using BMC.Common.LogManagement;
using BMC.Common.Utilities;
using BMC.DBInterface.CashDeskOperator;
using BMC.Transport.CashDeskOperatorEntity;
using Microsoft.Win32;
using System.Drawing;
using BMC.Transport;
using System.Linq;
using System.Collections.Generic;

namespace BMC.Business.CashDeskOperator
{
    public enum AnalysisView
    {
        Position = 0,
        Zone = 1,
    }

    public class Analysis
    {
        private AnalysisDataAccess analysisDataAccessHandler;
        PrintUtility objPrint = new PrintUtility();

        public Analysis()
        {
            analysisDataAccessHandler = new AnalysisDataAccess();
        }

        public DataTable GetAnalysisDetails(int SpotCheckDataType, DateTime StartDate, DateTime EndDate)
        {
            DataTable dtAnalysis = new DataTable();

            decimal CashIn = 0;
            decimal CashOut = 0;
            decimal TotalCashIn = 0;
            decimal TotalCashOut = 0;
            decimal Totalhandle = 0;
            decimal TotalNetWin = 0;
            int InstallationNo = 0;
            int Numberofdays = 0;
            int TotalGamesBet = 0;
            //int TotalBills = 0;
            //int TotalTicketsIn = 0;
            //int TotalCoinsIn = 0;
            //int TotalCoinsOut = 0;
            //int TicketsOut = 0;
            //int TotalHandpay = 0;
            //int TotalJackpot = 0;
            //int ProgressiveHandpay = 0;

            decimal TotalBills = 0;
            decimal TotalTicketsIn = 0;
            decimal TotalCoinsIn = 0;
            decimal TotalCoinsOut = 0;
            decimal TicketsOut = 0;
            decimal TotalHandpay = 0;
            decimal TotalJackpot = 0;
            decimal ProgressiveHandpay = 0;

            decimal PriceOfPlay = 1;
            decimal InstallationTokenValue = 0;
            decimal MDA = 0;

            decimal CashableEFTIn = 0;
            decimal CashableEFTOut = 0;
            decimal NonCashableEFTIn = 0;
            decimal NonCashableEFTOut = 0;
            decimal WATIn = 0;
            decimal WATOut = 0;

            decimal TotalNonCashableTicketsIn = 0;
            decimal TotalNonCashableTicketsOut = 0;
            decimal TotalMDA = 0;

            DateTime InstallationEndDate;
            SpotCheck spotCheckData;

            string reportType = string.Empty;

            dtAnalysis.Columns.Add("Installation_No");
            dtAnalysis.Columns.Add("Bar_Pos_Name");
            dtAnalysis.Columns.Add("Stock_No");
            dtAnalysis.Columns.Add("Name");
            dtAnalysis.Columns.Add("LastMeterUpdate");
            dtAnalysis.Columns.Add("GMUtoMachine");
            dtAnalysis.Columns.Add("GMUtoServer");
            dtAnalysis.Columns.Add("Bills_In", typeof(decimal));
            dtAnalysis.Columns.Add("Coin_In", typeof(decimal));
            dtAnalysis.Columns.Add("Coin_Out", typeof(decimal));
            dtAnalysis.Columns.Add("Tickets_In", typeof(decimal));
            dtAnalysis.Columns.Add("Tickets_Out", typeof(decimal));
            dtAnalysis.Columns.Add("Non_Cashable_Tickets_In", typeof(decimal));
            dtAnalysis.Columns.Add("Non_Cashable_Tickets_Out", typeof(decimal));
            dtAnalysis.Columns.Add("Handpay", typeof(decimal));
            dtAnalysis.Columns.Add("Jackpot", typeof(decimal));
            dtAnalysis.Columns.Add("Prog_Handpay", typeof(decimal));
            dtAnalysis.Columns.Add("Cash_In", typeof(decimal));
            dtAnalysis.Columns.Add("Cash_Out", typeof(decimal));
            dtAnalysis.Columns.Add("Handle", typeof(decimal));
            dtAnalysis.Columns.Add("AvgBet", typeof(decimal));
            dtAnalysis.Columns.Add("NetWin", typeof(decimal));
            dtAnalysis.Columns.Add("MDA", typeof(decimal));





            if (Settings.IsAFTEnabledForSite)
            {
                dtAnalysis.Columns.Add("CashableEFTIn", typeof(decimal));
                dtAnalysis.Columns.Add("CashableEFTOut", typeof(decimal));
                dtAnalysis.Columns.Add("NonCashableEFTIn", typeof(decimal));
                dtAnalysis.Columns.Add("NonCashableEFTOut", typeof(decimal));
                dtAnalysis.Columns.Add("WATIn", typeof(decimal));
                dtAnalysis.Columns.Add("WATOut", typeof(decimal));
            }

            try
            {
                switch (SpotCheckDataType.ToString())
                {
                    case "1":
                        reportType = "DAY";
                        break;
                    case "2":
                        reportType = "DROP";
                        break;
                    case "3":
                        reportType = "WEEK";
                        break;
                    case "4":
                        reportType = "MONTH";
                        break;
                }

                foreach (DataRow InstallationDetail in (new CommonDataAccess()).GetInstallationDetailsForReports(reportType).Rows)
                {
                    if (InstallationDetail["Installation_No"] != DBNull.Value)
                    {
                        PriceOfPlay = 1;
                        InstallationNo = Convert.ToInt32(InstallationDetail["Installation_No"]);
                        spotCheckData = new BMC.Transport.CashDeskOperatorEntity.SpotCheck();
                        spotCheckData.InstallationNo = InstallationNo;
                        spotCheckData.DateTimeStamp = DateTime.Now;
                        spotCheckData.Date = DateTime.Now;
                        InstallationEndDate = ((InstallationDetail["End_Date"]) != DBNull.Value) ? Convert.ToDateTime(InstallationDetail["End_Date"]) : DateTime.Now.AddDays(1);

                        switch (SpotCheckDataType)
                        {
                            case 1:
                                spotCheckData.StartOfDay = 1;
                                //if (InstallationEndDate.Date < System.DateTime.Now.Date) continue;
                                break;
                            case 2:
                                spotCheckData.Type = 2;
                                spotCheckData.StartOfDay = 2;
                                //if (InstallationEndDate.Date <= System.DateTime.Now.Date) continue;
                                break;
                            case 3:
                                StartDate = analysisDataAccessHandler.GetMonthOrWeekStartDate().Rows[0]["WeekStart"].ToString().ReadDate();
                                spotCheckData.Date = StartDate;
                                //if (InstallationEndDate.Date <= StartDate.Date) continue;
                                break;
                            case 4:
                                StartDate = analysisDataAccessHandler.GetMonthOrWeekStartDate().Rows[0]["MonthStart"].ToString().ReadDate();
                                spotCheckData.Date = StartDate;
                                //if (InstallationEndDate.Date <= StartDate.Date) continue;
                                break;
                        }

                        analysisDataAccessHandler.GetAnalysisDetails(ref spotCheckData);

                        InstallationTokenValue = (InstallationDetail["installation_token_value"]).ToString().GetDecimal();
                        PriceOfPlay = (InstallationDetail["installation_Price_Of_Play"]).ToString().GetDecimal();
                        PriceOfPlay = PriceOfPlay == 0 ? 1 : PriceOfPlay;
                        Totalhandle += (spotCheckData.CoinsIn * (PriceOfPlay / 100));

                        DataRow dr = dtAnalysis.NewRow();

                        dr["Installation_No"] = InstallationDetail["Installation_no"].ToString();
                        dr["Bar_Pos_Name"] = InstallationDetail["Bar_Pos_Name"].ToString();
                        dr["Stock_No"] = InstallationDetail["Stock_No"].ToString();
                        dr["Name"] = InstallationDetail["Name"].ToString();
                        dr["LastMeterUpdate"] = InstallationDetail["LastMeterUpdate"].ToString();
                        if (Convert.ToInt16(InstallationDetail["Datapak_PollingStatus"]) == 0)
                            dr["GMUtoServer"] = "ERROR";
                        else
                            dr["GMUtoServer"] = "OK";
                        if (Convert.ToInt16(InstallationDetail["GMU_Machine_Status"]) == 0)
                            dr["GMUtoMachine"] = "ERROR";
                        else
                        {
                            if (Convert.ToInt16(InstallationDetail["Datapak_PollingStatus"]) == 0)
                                dr["GMUtoMachine"] = "UNKNOWN";
                            else
                                dr["GMUtoMachine"] = "OK";
                        }
                        decimal Bills = 0;
                        if (System.Globalization.CultureInfo.CurrentCulture.Name == "it-IT" || System.Globalization.CultureInfo.CurrentUICulture.Name == "it-IT" ||
                            (Settings.RegulatoryEnabled && Settings.RegulatoryType == "AAMS"))
                            Bills = spotCheckData.Bill1 + (spotCheckData.Bill2 * 2) +
                                        (spotCheckData.Bill5 * 5) + (spotCheckData.Bill10 * 10) +
                                        (spotCheckData.Bill20 * 20) + (spotCheckData.Bill50 * 50) +
                                        (spotCheckData.Bill100 * 100) + (spotCheckData.Bill200 * 200) +
                                         (spotCheckData.Bill500 * 500);
                        else if (Settings.Region == "AR")
                            Bills = (spotCheckData.Bill2 * 2) +
                                     (spotCheckData.Bill5 * 5) + (spotCheckData.Bill10 * 10) +
                                     (spotCheckData.Bill20 * 20) + (spotCheckData.Bill50 * 50) +
                                     (spotCheckData.Bill100 * 100);
                        else
                            Bills = spotCheckData.Bill1 + (spotCheckData.Bill2 * 2) +
                                      (spotCheckData.Bill5 * 5) + (spotCheckData.Bill10 * 10) +
                                      (spotCheckData.Bill20 * 20) + (spotCheckData.Bill50 * 50) +
                                      (spotCheckData.Bill100 * 100);

                        dr["Bills_In"] = Bills;
                        dr["Tickets_In"] = spotCheckData.TicketsInserted / 100;
                        dr["Coin_In"] = spotCheckData.TrueCoinIn * (InstallationTokenValue / 100);
                        dr["Non_Cashable_Tickets_In"] = spotCheckData.NonCashableTicketsInserted / 100;
                        dr["Non_Cashable_Tickets_Out"] = spotCheckData.NonCashableTicketsPrinted / 100;

                        CashIn = (Bills + (spotCheckData.TicketsInserted / 100) + (spotCheckData.NonCashableTicketsInserted / 100) + (spotCheckData.TrueCoinIn * (InstallationTokenValue / 100)));
                        CashOut = spotCheckData.TrueCoinOut * (PriceOfPlay / 100) + spotCheckData.TicketsPrinted / 100 + spotCheckData.HandPay * (PriceOfPlay / 100) + spotCheckData.Jackpot * (PriceOfPlay / 100)
                            + spotCheckData.NonCashableTicketsPrinted / 100;

                        if (Settings.IsAFTIncludedInCalculation)
                        {
                            CashIn += spotCheckData.CashableEFTIn / 100 + spotCheckData.NonCashableEFTIn / 100 + spotCheckData.WATIn / 100;
                            CashOut += spotCheckData.CashableEFTOut / 100 + spotCheckData.NonCashableEFTOut / 100 + spotCheckData.WATOut / 100;
                        }

                        dr["Cash_In"] = CashIn;
                        dr["Cash_Out"] = CashOut;

                        TotalCashIn += CashIn;
                        TotalCashOut += CashOut;

                        dr["NetWin"] = CashIn - CashOut;
                        dr["Handle"] = (spotCheckData.CoinsIn * (PriceOfPlay / 100));
                        Numberofdays += spotCheckData.NumberOfDays;
                        if (spotCheckData.NumberOfDays > 0)
                            MDA = (decimal)(CashIn - CashOut) / spotCheckData.NumberOfDays;
                        else
                            MDA = (decimal)(CashIn - CashOut);

                        dr["MDA"] = MDA == 0 ? 0 : MDA;
                        dr["Coin_Out"] = spotCheckData.TrueCoinOut * (InstallationTokenValue / 100);
                        dr["Tickets_Out"] = spotCheckData.TicketsPrinted / 100;
                        dr["Handpay"] = spotCheckData.HandPay * (PriceOfPlay / 100);
                        dr["Jackpot"] = spotCheckData.Jackpot * (PriceOfPlay / 100);
                        dr["Prog_Handpay"] = spotCheckData.ProgHandpay * (PriceOfPlay / 100);
                        dr["AvgBet"] = spotCheckData.GamesBet == 0 ? (spotCheckData.CoinsIn * (PriceOfPlay / 100)) : (spotCheckData.CoinsIn * (PriceOfPlay / 100)) / spotCheckData.GamesBet;
                        if (Settings.IsAFTEnabledForSite)
                        {
                            dr["CashableEFTIn"] = spotCheckData.CashableEFTIn / 100;
                            dr["CashableEFTOut"] = spotCheckData.CashableEFTOut / 100;
                            dr["NonCashableEFTIn"] = spotCheckData.NonCashableEFTIn / 100;
                            dr["NonCashableEFTOut"] = spotCheckData.NonCashableEFTOut / 100;
                            dr["WATIn"] = spotCheckData.WATIn / 100;
                            dr["WATOut"] = spotCheckData.WATOut / 100;
                        }

                        if (System.Globalization.CultureInfo.CurrentCulture.Name == "it-IT" || System.Globalization.CultureInfo.CurrentUICulture.Name == "it-IT" ||
                           (Settings.RegulatoryEnabled && Settings.RegulatoryType == "AAMS"))
                            TotalBills += spotCheckData.Bill1 + (spotCheckData.Bill2 * 2) + (spotCheckData.Bill5 * 5) +
                                        (spotCheckData.Bill10 * 10) + (spotCheckData.Bill20 * 20) + (spotCheckData.Bill50 * 50) +
                                        (spotCheckData.Bill100 * 100) + (spotCheckData.Bill200 * 200) +
                                        (spotCheckData.Bill500 * 500);
                        else if (Settings.Region == "AR")
                            TotalBills += (spotCheckData.Bill2 * 2) + (spotCheckData.Bill5 * 5) +
                                       (spotCheckData.Bill10 * 10) + (spotCheckData.Bill20 * 20) + (spotCheckData.Bill50 * 50) +
                                       (spotCheckData.Bill100 * 100);
                        else
                            TotalBills += spotCheckData.Bill1 + (spotCheckData.Bill2 * 2) + (spotCheckData.Bill5 * 5) +
                                        (spotCheckData.Bill10 * 10) + (spotCheckData.Bill20 * 20) + (spotCheckData.Bill50 * 50) +
                                        (spotCheckData.Bill100 * 100);
                        TotalTicketsIn += spotCheckData.TicketsInserted / 100;
                        TotalGamesBet += spotCheckData.GamesBet;
                        TotalCoinsIn += Convert.ToDecimal(spotCheckData.TrueCoinIn * (InstallationTokenValue / 100));
                        TotalCoinsOut += Convert.ToDecimal(spotCheckData.TrueCoinOut * (InstallationTokenValue / 100));
                        TicketsOut += (decimal)(spotCheckData.TicketsPrinted / 100);
                        TotalHandpay += Convert.ToDecimal(spotCheckData.HandPay * (PriceOfPlay / 100));
                        TotalJackpot += Convert.ToDecimal(spotCheckData.Jackpot * (PriceOfPlay / 100));
                        ProgressiveHandpay += Convert.ToDecimal(spotCheckData.ProgHandpay * (PriceOfPlay / 100));
                        TotalNonCashableTicketsIn += spotCheckData.NonCashableTicketsInserted / 100;
                        TotalNonCashableTicketsOut += spotCheckData.NonCashableTicketsPrinted / 100;
                        TotalMDA += MDA;
                        if (Settings.IsAFTEnabledForSite)
                        {
                            CashableEFTIn += Convert.ToDecimal(spotCheckData.CashableEFTIn / 100);
                            CashableEFTOut += Convert.ToDecimal(spotCheckData.CashableEFTOut / 100);
                            NonCashableEFTIn += Convert.ToDecimal(spotCheckData.NonCashableEFTIn / 100);
                            NonCashableEFTOut += Convert.ToDecimal(spotCheckData.NonCashableEFTOut / 100);
                            WATIn += Convert.ToDecimal(spotCheckData.WATIn / 100);
                            WATOut += Convert.ToDecimal(spotCheckData.WATOut / 100);
                        }
                        dtAnalysis.Rows.Add(dr);
                    }
                }
                if (dtAnalysis.Rows.Count > 0)
                {
                    DataRow SumOfRows = dtAnalysis.NewRow();
                    SumOfRows["Installation_No"] = string.Empty;
                    SumOfRows["Bar_Pos_Name"] = "Total";
                    SumOfRows["Stock_No"] = string.Empty;
                    SumOfRows["Name"] = string.Empty;
                    SumOfRows["LastMeterUpdate"] = string.Empty;
                    SumOfRows["Cash_In"] = TotalCashIn;
                    SumOfRows["Cash_Out"] = TotalCashOut;
                    SumOfRows["NetWin"] = (TotalCashIn - TotalCashOut);
                    SumOfRows["Handle"] = Totalhandle;

                    decimal TotalAvgBet = 0;
                    if (TotalGamesBet == 0)
                        TotalAvgBet = Totalhandle;
                    else
                        TotalAvgBet = Totalhandle / TotalGamesBet;

                    //if (Numberofdays > 0)
                    //    MDA = (decimal)(TotalCashIn - TotalCashOut) /( dtAnalysis.Rows.Count * Numberofdays)  ;
                    //else
                    //    MDA = (decimal)(TotalCashIn - TotalCashOut) / dtAnalysis.Rows.Count;

                    SumOfRows["AvgBet"] = Math.Round(TotalAvgBet, 2);
                    SumOfRows["MDA"] = Math.Round(TotalMDA / (dtAnalysis.Rows.Count), 2);//Modified Total AvgDailyWin calculation CR#92149 Fix
                    SumOfRows["Bills_In"] = TotalBills;
                    SumOfRows["Tickets_In"] = TotalTicketsIn;
                    SumOfRows["Tickets_Out"] = TicketsOut;
                    SumOfRows["Coin_In"] = TotalCoinsIn;
                    SumOfRows["Coin_Out"] = TotalCoinsOut;
                    SumOfRows["Non_Cashable_Tickets_In"] = TotalNonCashableTicketsIn;
                    SumOfRows["Non_Cashable_Tickets_out"] = TotalNonCashableTicketsOut;
                    SumOfRows["Handpay"] = TotalHandpay;
                    SumOfRows["Jackpot"] = TotalJackpot;
                    SumOfRows["Prog_Handpay"] = ProgressiveHandpay;
                    if (Settings.IsAFTEnabledForSite)
                    {
                        SumOfRows["CashableEFTIn"] = CashableEFTIn;
                        SumOfRows["CashableEFTOut"] = CashableEFTOut;
                        SumOfRows["NonCashableEFTIn"] = NonCashableEFTIn;
                        SumOfRows["NonCashableEFTOut"] = NonCashableEFTOut;
                        SumOfRows["WATIn"] = WATIn;
                        SumOfRows["WATOut"] = WATOut;
                    }

                    dtAnalysis.Rows.InsertAt(SumOfRows, 0);
                }
                return dtAnalysis;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return new DataTable();
        }

        public DataTable GetAnalysisDetails(int SpotCheckDataType, DateTime StartDate, DateTime EndDate, AnalysisView viewType, int zoneId)
        {
            DataTable dtAnalysis = new DataTable();



            double TotalCashIn = 0;
            double TotalCashOut = 0;
            double Totalhandle = 0;
            int InstallationNo = 0;
            double TotalDrop = 0;
            double TotalBills = 0;
            double TotalTicketsIn = 0;
            double TotalCoinsIn = 0;
            double TotalCoinsOut = 0;
            double TicketsOut = 0;
            double TotalHandpay = 0;
            double TotalJackpot = 0;
            double TotalPayout = 0;
            double ProgressiveHandpay = 0;

            double CashableEFTIn = 0;
            double CashableEFTOut = 0;
            double NonCashableEFTIn = 0;
            double NonCashableEFTOut = 0;
            double WATIn = 0;
            double WATOut = 0;

            double TotalNonCashableTicketsIn = 0;
            double TotalNonCashableTicketsOut = 0;
            double TotalMDA = 0;
            double TotalAvgBet = 0;
            double TotalWon = 0;
            double TotalCashRefills = 0;
            DateTime InstallationEndDate;
            double TotalNetWin = 0;
            double NumberofDays = 0;
            double GAMES_BET = 0;


            string reportType = string.Empty;

            IDictionary<int, GetGroupByZone> zoneRows = new SortedDictionary<int, GetGroupByZone>();

            dtAnalysis.Columns.Add("Installation_No");
            dtAnalysis.Columns.Add("Bar_Pos_Name");
            dtAnalysis.Columns.Add("Zone_No");
            dtAnalysis.Columns.Add("Zone_Name");
            dtAnalysis.Columns.Add("Stock_No");
            dtAnalysis.Columns.Add("Name");
            dtAnalysis.Columns.Add("LastMeterDate", typeof(DateTime));
            dtAnalysis.Columns.Add("LastMeterUpdate");
            dtAnalysis.Columns.Add("NoofMachine");
            dtAnalysis.Columns.Add("GMUtoMachine");
            dtAnalysis.Columns.Add("GMUtoServer");
            dtAnalysis.Columns.Add("Bills_In", typeof(decimal));
            dtAnalysis.Columns.Add("Coin_In", typeof(decimal));
            dtAnalysis.Columns.Add("Coin_Out", typeof(decimal));
            dtAnalysis.Columns.Add("Drop", typeof(decimal));
            dtAnalysis.Columns.Add("Tickets_In", typeof(decimal));
            dtAnalysis.Columns.Add("Tickets_Out", typeof(decimal));
            dtAnalysis.Columns.Add("Non_Cashable_Tickets_In", typeof(decimal));
            dtAnalysis.Columns.Add("Non_Cashable_Tickets_Out", typeof(decimal));
            dtAnalysis.Columns.Add("Handpay", typeof(decimal));
            dtAnalysis.Columns.Add("Jackpot", typeof(decimal));
            dtAnalysis.Columns.Add("Prog_Handpay", typeof(decimal));

            if (Settings.IsAFTEnabledForSite)
            {
                dtAnalysis.Columns.Add("CashableEFTIn", typeof(decimal));
                dtAnalysis.Columns.Add("CashableEFTOut", typeof(decimal));
                dtAnalysis.Columns.Add("NonCashableEFTIn", typeof(decimal));
                dtAnalysis.Columns.Add("NonCashableEFTOut", typeof(decimal));
                dtAnalysis.Columns.Add("WATIn", typeof(decimal));
                dtAnalysis.Columns.Add("WATOut", typeof(decimal));
            }
            dtAnalysis.Columns.Add("Cash_In", typeof(decimal));
            dtAnalysis.Columns.Add("Cash_Out", typeof(decimal));
            dtAnalysis.Columns.Add("Handle", typeof(decimal));
            dtAnalysis.Columns.Add("Payout", typeof(decimal));
            dtAnalysis.Columns.Add("MDA", typeof(decimal));
            dtAnalysis.Columns.Add("AvgBet", typeof(decimal));
            dtAnalysis.Columns.Add("NetWin", typeof(decimal));

            dtAnalysis.Columns.Add("Won", typeof(decimal));
            dtAnalysis.Columns.Add("Cash_Refills", typeof(decimal));
            dtAnalysis.Columns.Add("SortColumn", typeof(int));//used to maintain Row "Total" in top
            dtAnalysis.Columns.Add("NumberofDays", typeof(decimal));
            dtAnalysis.Columns.Add("GAMES_BET", typeof(decimal));
            dtAnalysis.Columns.Add("IsTotalRow", typeof(bool)).DefaultValue = false;

            try
            {
                switch (SpotCheckDataType.ToString())
                {
                    case "1":
                        reportType = "DAY";
                        break;
                    case "2":
                        reportType = "DROP";
                        break;
                    case "3":
                        reportType = "WEEK";
                        break;
                    case "4":
                        reportType = "MONTH";
                        break;
                }

                DataTable dtDetails = new CommonDataAccess().GetInstallationDetailsForReports(reportType);
                bool isGrouping = false;
                if (zoneId != -1)
                {
                    dtDetails = (from i in dtDetails.Rows.OfType<DataRow>()
                                 where i["Zone_No"].ToString() == zoneId.ToString()
                                 select i).CopyToDataTable();
                    isGrouping = (viewType == AnalysisView.Zone);
                }
                if (zoneId == -1 && viewType == AnalysisView.Zone)
                {
                    isGrouping = true;
                }
                int ZoneCount = 0;
                foreach (DataRow InstallationDetail in dtDetails.Rows)
                {
                    if (InstallationDetail["Installation_No"] != DBNull.Value)
                    {
                        InstallationNo = Convert.ToInt32(InstallationDetail["Installation_No"]);
                        DateTime dt_date = DateTime.Now;
                        InstallationEndDate = ((InstallationDetail["End_Date"]) != DBNull.Value) ? Convert.ToDateTime(InstallationDetail["End_Date"]) : DateTime.Now.AddDays(1);
                        int StartOfDay = 0;

                        switch (SpotCheckDataType)
                        {
                            case 1:
                                StartOfDay = 1;
                                break;
                            case 2:
                                //spotCheckData.Type = 2;
                                StartOfDay = 2;
                                break;
                            case 3:
                                StartDate = analysisDataAccessHandler.GetMonthOrWeekStartDate().Rows[0]["WeekStart"].ToString().ReadDate();
                                dt_date = StartDate;
                                //if (InstallationEndDate.Date <= StartDate.Date) continue;
                                break;
                            case 4:
                                StartDate = analysisDataAccessHandler.GetMonthOrWeekStartDate().Rows[0]["MonthStart"].ToString().ReadDate();
                                dt_date = StartDate;
                                //if (InstallationEndDate.Date <= StartDate.Date) continue;
                                break;
                        }
                        LogManager.WriteLog("GetAnalysisDetails:Getting Meters for DB for Installation:" + InstallationNo, LogManager.enumLogLevel.Info);
                        DataTable dt_CurrMeters = analysisDataAccessHandler.GetAnalysisDetails(InstallationNo, StartOfDay, 0, dt_date);
                        DataRow dr_CurrM = null;
                        if (dt_CurrMeters.Rows.Count > 0)
                        {
                            dr_CurrM = dt_CurrMeters.Rows[0];
                        }
                        else
                        {

                            LogManager.WriteLog("GetAnalysisDetails:InstallationNo Not Found", LogManager.enumLogLevel.Error);

                        }

                        DataRow dr = null;
                        int zoneIdActual = 0;
                        Int32.TryParse(InstallationDetail["Zone_No"].ToString(), out zoneIdActual);
                        bool addToCollection = false;

                        switch (viewType)
                        {
                            case AnalysisView.Zone:
                                if (zoneRows.ContainsKey(zoneIdActual))
                                {
                                    GetGroupByZone gp_zone = zoneRows[zoneIdActual];
                                    dr = gp_zone.dr_row;
                                    gp_zone.count += 1;
                                   
                                }
                                else
                                {                                   
                                    addToCollection = true;
                                }
                                break;

                            default:
                                break;
                        }

                        if (dr == null)
                        {
                            dr = dtAnalysis.NewRow();
                            dtAnalysis.Rows.Add(dr);
                            if (addToCollection)
                            {
                                zoneRows.Add(zoneIdActual, new GetGroupByZone { dr_row = dr, count = 1 });

                            }
                        }

                        if (!isGrouping)
                        {
                            dr["Installation_No"] = InstallationDetail["Installation_no"].ToString();
                            dr["Bar_Pos_Name"] = InstallationDetail["Bar_Pos_Name"].ToString();
                            dr["Stock_No"] = InstallationDetail["Stock_No"].ToString();
                            dr["Name"] = InstallationDetail["Name"].ToString();
                            dr["LastMeterUpdate"] = InstallationDetail["LastMeterUpdate"].Equals(DBNull.Value) ? "" : Convert.ToDateTime(InstallationDetail["LastMeterUpdate"]).ToString("dd-MMM-yyyy HH:mm:ss");
                            dr["LastMeterDate"] = InstallationDetail["LastMeterUpdate"].Equals(DBNull.Value) ? DateTime.MinValue : InstallationDetail["LastMeterUpdate"];
                            //dr["Payout"] = Convert.ToDouble(dr_CurrM["Payout"]).ToString("F");

                            if (Convert.ToInt16(InstallationDetail["Datapak_PollingStatus"]) == 0)
                                dr["GMUtoServer"] = "ERROR";
                            else
                                dr["GMUtoServer"] = "OK";
                            if (Convert.ToInt16(InstallationDetail["GMU_Machine_Status"]) == 0)
                                dr["GMUtoMachine"] = "ERROR";
                            else
                            {
                                if (Convert.ToInt16(InstallationDetail["Datapak_PollingStatus"]) == 0)
                                    dr["GMUtoMachine"] = "UNKNOWN";
                                else
                                    dr["GMUtoMachine"] = "OK";
                            }
                        }
                        else
                        {
                            dr["Installation_No"] = string.Empty;
                            dr["Bar_Pos_Name"] = string.Empty;
                            dr["Stock_No"] = string.Empty;
                            dr["Name"] = string.Empty;
                            dr["LastMeterUpdate"] = string.Empty;
                            //dr["Payout"] = string.Empty;
                        }


                        dr["SortColumn"] = 0;

                        if (viewType == AnalysisView.Zone)
                        {
                            dr["NoofMachine"] = zoneRows[zoneIdActual].count;
                        }
                        else
                        {
                            dr["NoofMachine"] = Convert.ToInt32(InstallationDetail["NoofMachine"] != DBNull.Value ? InstallationDetail["NoofMachine"] : 0);
                        }

                        dr["Zone_No"] = InstallationDetail["Zone_No"].ToString();
                        dr["Zone_Name"] = InstallationDetail["Zone_Name"].ToString();
                        dr_CurrM["NumberofDays"] = Convert.ToDouble(dr_CurrM["NumberofDays"]).ToString();
                        dr_CurrM["GAMES_BET"] = Convert.ToDouble(dr_CurrM["GAMES_BET"]).ToString();
                        dr_CurrM["Payout"] = Convert.ToDouble(dr_CurrM["Payout"]).ToString("F");
                        TotalBills += this.AddIntValue(dr, dr_CurrM, "Bills_In");
                        TotalTicketsIn += this.AddDoubleValue(dr, dr_CurrM, "Tickets_In");

                        TotalCoinsIn += this.AddDoubleValue(dr, dr_CurrM, "Coin_In");
                        TotalNonCashableTicketsIn += this.AddDoubleValue(dr, dr_CurrM, "Non_Cashable_Tickets_In");
                        TotalNonCashableTicketsOut += this.AddDoubleValue(dr, dr_CurrM, "Non_Cashable_Tickets_Out");
                        TotalPayout += this.AddDoubleValue(dr, dr_CurrM, "Payout");
                        TotalDrop += this.AddDoubleValue(dr, dr_CurrM, "Drop");
                        TotalCashIn += this.AddDoubleValue(dr, dr_CurrM, "Cash_In");
                        TotalCashOut += this.AddDoubleValue(dr, dr_CurrM, "Cash_Out");

                        TotalNetWin += this.AddDoubleValue(dr, dr_CurrM, "NetWin");
                        NumberofDays += this.AddDoubleValue(dr, dr_CurrM, "NumberofDays");
                        GAMES_BET += this.AddDoubleValue(dr, dr_CurrM, "GAMES_BET");
                        Totalhandle += this.AddDoubleValue(dr, dr_CurrM, "Handle");
                        TotalMDA += this.AddDoubleValue(dr, dr_CurrM, "MDA");
                        TotalCoinsOut += this.AddDoubleValue(dr, dr_CurrM, "Coin_Out");
                        TicketsOut += this.AddDoubleValue(dr, dr_CurrM, "Tickets_Out");
                        TotalHandpay += this.AddDoubleValue(dr, dr_CurrM, "Handpay");
                        TotalJackpot += this.AddDoubleValue(dr, dr_CurrM, "Jackpot");
                        ProgressiveHandpay += this.AddDoubleValue(dr, dr_CurrM, "Prog_Handpay");
                        TotalAvgBet += this.AddDoubleValue(dr, dr_CurrM, "AvgBet");

                        TotalWon += this.AddDoubleValue(dr, dr_CurrM, "WON"); ;
                        TotalCashRefills += this.AddDoubleValue(dr, dr_CurrM, "Cash_Refills");
                        if (Settings.IsAFTEnabledForSite)
                        {
                            CashableEFTIn += this.AddDoubleValue(dr, dr_CurrM, "CashableEFTIn");
                            CashableEFTOut += this.AddDoubleValue(dr, dr_CurrM, "CashableEFTOut");
                            NonCashableEFTIn += this.AddDoubleValue(dr, dr_CurrM, "NonCashableEFTIn");
                            NonCashableEFTOut += this.AddDoubleValue(dr, dr_CurrM, "NonCashableEFTOut");
                            WATIn += this.AddDoubleValue(dr, dr_CurrM, "WATIn");
                            WATOut += this.AddDoubleValue(dr, dr_CurrM, "WATOut");
                        }

                    }
                }

                if (dtAnalysis.Rows.Count > 0)
                {
                    DataRow SumOfRows = dtAnalysis.NewRow();
                    SumOfRows["Installation_No"] = string.Empty;
                    SumOfRows["Bar_Pos_Name"] = "Total";
                    SumOfRows["Stock_No"] = string.Empty;
                    SumOfRows["Name"] = string.Empty;
                    SumOfRows["LastMeterUpdate"] = string.Empty;
                    SumOfRows["Cash_In"] = TotalCashIn;
                    SumOfRows["Cash_Out"] = TotalCashOut;
                    SumOfRows["NetWin"] = (TotalCashIn - TotalCashOut);
                    SumOfRows["Handle"] = Totalhandle;
                    SumOfRows["Drop"] = TotalDrop;
                    SumOfRows["NoofMachine"] = dtDetails.Rows.Count;
                    SumOfRows["WON"] = TotalWon;
                    SumOfRows["Cash_Refills"] = TotalCashRefills;
                    SumOfRows["IsTotalRow"] = true;
                    if (Totalhandle != 0)
                        SumOfRows["Payout"] = Math.Round(((Totalhandle - TotalNetWin) / Totalhandle) * 100, 2);
                    else
                        SumOfRows["Payout"] = 0;
                    /// dtDetails.Rows.Count,2,MidpointRounding.AwayFromZero);
                    //if (TotalGamesBet == 0)
                    //    TotalAvgBet = Totalhandle;
                    //else
                    //    TotalAvgBet = Totalhandle / TotalGamesBet;

                    //if (Numberofdays > 0)
                    //    MDA = (decimal)(TotalCashIn - TotalCashOut) /( dtAnalysis.Rows.Count * Numberofdays)  ;
                    //else
                    //    MDA = (decimal)(TotalCashIn - TotalCashOut) / dtAnalysis.Rows.Count;
                    if (GAMES_BET == 0)
                        SumOfRows["AvgBet"] = Math.Round(Totalhandle, 2);
                    else
                        SumOfRows["AvgBet"] = Math.Round(Convert.ToDecimal(Totalhandle / GAMES_BET), 2);// TotalAvgBet;
                    if (NumberofDays == 0)
                        SumOfRows["MDA"] = Math.Round(TotalNetWin, 2);
                    else
                        SumOfRows["MDA"] = Math.Round(Convert.ToDecimal(TotalNetWin / NumberofDays), 2);// TotalMDA;//Modified Total AvgDailyWin calculation CR#92149 Fix
                    SumOfRows["Bills_In"] = TotalBills;
                    SumOfRows["Tickets_In"] = TotalTicketsIn;
                    SumOfRows["Tickets_Out"] = TicketsOut;
                    SumOfRows["Coin_In"] = TotalCoinsIn;
                    SumOfRows["Coin_Out"] = TotalCoinsOut;
                    SumOfRows["Non_Cashable_Tickets_In"] = TotalNonCashableTicketsIn;
                    SumOfRows["Non_Cashable_Tickets_out"] = TotalNonCashableTicketsOut;
                    SumOfRows["Handpay"] = TotalHandpay;
                    SumOfRows["Jackpot"] = TotalJackpot;
                    SumOfRows["Prog_Handpay"] = ProgressiveHandpay;
                    if (Settings.IsAFTEnabledForSite)
                    {
                        SumOfRows["CashableEFTIn"] = CashableEFTIn;
                        SumOfRows["CashableEFTOut"] = CashableEFTOut;
                        SumOfRows["NonCashableEFTIn"] = NonCashableEFTIn;
                        SumOfRows["NonCashableEFTOut"] = NonCashableEFTOut;
                        SumOfRows["WATIn"] = WATIn;
                        SumOfRows["WATOut"] = WATOut;
                    }
                    SumOfRows["SortColumn"] = 100;//used to avoid Column "Total" Sorting
                    dtAnalysis.Rows.InsertAt(SumOfRows, 0);
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return dtAnalysis;
        }
        private double AddDoubleValue(DataRow dr, DataRow dr2, string columnName)
        {
            try
            {
                object value = dr[columnName];
                if (value == DBNull.Value) value = 0;

                object value2 = dr2[columnName];
                if (value2 == DBNull.Value) value2 = 0;

                double dblValue = 0;
                double dblValue2 = 0;
                Double.TryParse(value.ToString(), out dblValue);
                Double.TryParse(value2.ToString(), out dblValue2);

                double resultValue = (dblValue + dblValue2);
                dr[columnName] = resultValue;
                return dblValue2;
            }
            catch
            {
                dr[columnName] = 0;
                return 0;
            }

        }

        private int AddIntValue(DataRow dr, DataRow dr2, string columnName)
        {
            try
            {
                object value = dr[columnName];
                if (value == DBNull.Value) value = 0;

                object value2 = dr2[columnName];
                if (value2 == DBNull.Value) value2 = 0;

                int dblValue = 0;
                int dblValue2 = 0;
                int.TryParse(value.ToString(), out dblValue);
                int.TryParse(value2.ToString(), out dblValue2);

                int resultValue = (dblValue + dblValue2);
                dr[columnName] = resultValue;
                return dblValue2;
            }
            catch
            {
                dr[columnName] = 0;
                return 0;
            }
        }

        private decimal ToDecimal(object value)
        {
            try
            {
                if (value == null)
                {
                    return 0;
                }
                return Convert.ToDecimal(value);
            }
            catch
            {
                return 0;
            }
        }

        public DataTable GetWeekCollectionSummary()
        {
            DataTable dtWeekSummary = new DataTable();
            string SiteCode = CommonDataAccess.GetSettingValue("TICKET_LOCATION_CODE");
            string EnterpriseURL = GetEnterpriseURL();
            int NoOfRecords = int.Parse(ConfigManager.Read("NoOfRecords"));
            Proxy WebProxy = new Proxy(SiteCode);

            try
            {
                dtWeekSummary = WebProxy.GetWeeklyCollectionDetails(SiteCode, 0, NoOfRecords);

                foreach (DataRow dr in dtWeekSummary.Rows)
                    dr["StartDate"] += " - " + dr["EndDate"].ToString();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                dtWeekSummary = new DataTable();
            }
            return dtWeekSummary;
        }

        public DataTable GetWeekCollectionDetails(int WeekID)
        {
            DataTable dtWeeklyInvoice = new DataTable();
            string SiteCode = CommonDataAccess.GetSettingValue("TICKET_LOCATION_CODE");
            string EnterpriseURL = GetEnterpriseURL();
            string TotalPowerCollectionDuration;
            try
            {
                Proxy WebProxy = new Proxy(SiteCode);
                dtWeeklyInvoice = WebProxy.GetWeeklyCollectionDetails(SiteCode, WeekID, 0);

                dtWeeklyInvoice.Columns.Add("Payout", System.Type.GetType("System.Decimal"));
                dtWeeklyInvoice.Columns.Add("Hold", System.Type.GetType("System.Decimal"));
                dtWeeklyInvoice.Columns.Add("Duration");

                foreach (DataRow dr in dtWeeklyInvoice.Rows)
                {
                    double DecWinOrLoss;
                    double MeterWinOrLoss;
                    TotalPowerCollectionDuration = "0";
                    DecWinOrLoss = Convert.ToDouble(dr["Declared_Notes"]) + Convert.ToDouble(dr["DecTicketBalance"]) -
                                   Convert.ToDouble(dr["DecHandpay"]) + Convert.ToDouble(dr["Net_Coin"]);
                    MeterWinOrLoss = Convert.ToDouble(dr["RDC_Notes"]) + (Convert.ToDouble(dr["DecTicketBalance"]) - Convert.ToDouble(dr["Ticket_Var"])) -
                                            Convert.ToDouble(dr["RDCHandpay"]) + Convert.ToDouble(dr["RDC_Coins"]);
                    dr["Cash_Take"] = DecWinOrLoss;
                    dr["RDC_Take"] = MeterWinOrLoss;
                    dr["Take_Var"] = DecWinOrLoss - MeterWinOrLoss;
                    dr["Ticket_Balance"] = Convert.ToDouble(dr["DecTicketBalance"]);
                    dr["RDC_Ticket_Balance"] = Convert.ToDouble(dr["DecTicketBalance"]) - Convert.ToDouble(dr["Ticket_Var"]); ;
                    if (WeekID != 0)
                    {
                        if (Convert.ToDouble(dr["Handle"]) > 0)
                        {
                            dr["Payout"] = ((Convert.ToDouble(dr["Handle"]) - MeterWinOrLoss) / Convert.ToDouble(dr["Handle"])) * 100;
                            dr["Hold"] = 100 - (((Convert.ToDouble(dr["Handle"]) - MeterWinOrLoss) / Convert.ToDouble(dr["Handle"])) * 100);
                        }
                        else
                        {
                            dr["Payout"] = "0";
                            dr["Hold"] = "100";
                        }
                    }
                    else
                    {
                        dr["Payout"] = "0";
                        dr["Hold"] = "100";
                    }

                    if (Convert.ToInt64(dr["Collection_Total_Power_Duration"]) / 3600 > 0)
                        TotalPowerCollectionDuration = (Convert.ToInt64(dr["Collection_Total_Power_Duration"]) / 3600).ToString();

                    if ((Convert.ToInt64(dr["Collection_Total_Power_Duration"]) % 3600) / 60 > 0)
                        TotalPowerCollectionDuration = TotalPowerCollectionDuration + "." + ((Convert.ToInt64(dr["Collection_Total_Power_Duration"]) % 3600) / 60).ToString();

                    dr["Duration"] = TotalPowerCollectionDuration;

                }

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return dtWeeklyInvoice;
        }

        private string GetEnterpriseURL()
        {
            //string RegistryPath = ConfigManager.Read("RegistryPath");            
            string URL = string.Empty;
            //RegistryKey regKeyConnectionString;
            try
            {
                URL = BMCRegistryHelper.GetRegKeyValue(ConfigManager.Read("RegistryPath"), "BGSWebService");


                if (string.IsNullOrEmpty(URL))
                    LogManager.WriteLog("Enterprise URL not Found.", BMC.Common.LogManagement.LogManager.enumLogLevel.Error);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return URL;
        }

        public DataTable GetWTDMTD()
        {
            return analysisDataAccessHandler.GetWTDMTD();
        }

        public DataTable GetBarPositionDetails(string SortBy, int InstallNo)
        {
            return analysisDataAccessHandler.GetBarPositionDetails(SortBy, InstallNo);
        }

        public void SaveFloorPosition(int slotID, int securityUserID, int topPosition, int leftPosition)
        {
            analysisDataAccessHandler.SaveFloorPosition(slotID, securityUserID, topPosition, leftPosition);
        }

        public DataTable GetSpotCheckDataSAS(int InstallationNo, int SelectDay, DateTime date)
        {
            return analysisDataAccessHandler.GetSpotCheckDataSAS(InstallationNo, SelectDay, date);
        }

        public DataTable GetTicketsClaimedByCDForPeriod(int SelectDay, DateTime IsDate, int MachineNo)
        {
            return analysisDataAccessHandler.GetTicketsClaimedByCDForPeriod(SelectDay, IsDate, MachineNo);
        }

        public DataTable GetInstallations()
        {
            return CommonDataAccess.GetInstallationDetails();
        }

        public DataTable GetInstallationsForReports(string reportType)
        {
            return new CommonDataAccess().GetInstallationDetailsForReports(reportType);
        }

        public DataTable GetEPIDetails(int InstallationNo)
        {
            return analysisDataAccessHandler.GetEPIDetails(InstallationNo);
        }

        public DataTable GetTicketException(int InstallationNo)
        {
            return analysisDataAccessHandler.GetTicketException(InstallationNo);
        }

        public DataTable GetHandpaystoClear(int InstallationNo)
        {
            return analysisDataAccessHandler.GetHandpaystoClear(InstallationNo);
        }
        public void PrintFunction(System.Windows.Controls.ListView lvView, string ReportName, DateTime ReportStartdate)
        {
            objPrint.PrintFunction(lvView, ReportName, ReportStartdate);
        }
    }

    public class GetGroupByZone
    {
        public DataRow dr_row
        { get; set; }

        public int count
        { get; set; }
    }
}
