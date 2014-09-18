using System;
using System.Data;
using System.Data.SqlClient;
using BMC.Common.Utilities;
using BMC.DataAccess;
using BMC.Common.ExceptionManagement;
using BMC.Transport;

namespace BMC.DBInterface.CashDeskOperator
{
    public class AnalysisDataAccess
    {
        #region "Private Variables"
        #endregion

        #region "Public Properties"
        #endregion

        #region "Private Function"
        #endregion

        #region "Public Function"

        public DataTable GetMonthOrWeekStartDate()
        {
            DataTable dtWeekStartDate = new DataTable();
            try
            {
                dtWeekStartDate = SqlHelper.ExecuteDataset(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, "rsp_GetWTDMTD").Tables[0];
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return dtWeekStartDate;
        }

        public void GetAnalysisDetails(ref BMC.Transport.CashDeskOperatorEntity.SpotCheck objSpotCheck)
        {
            SqlParameter[] objParams = null;

            try
            {
                if (System.Globalization.CultureInfo.CurrentCulture.Name == "it-IT" ||
                    System.Globalization.CultureInfo.CurrentUICulture.Name == "it-IT" ||
                    (Settings.RegulatoryEnabled && Settings.RegulatoryType == "AAMS"))
                {

                    if (Settings.IsAFTEnabledForSite)
                    {
                        objParams = new SqlParameter[54];
                    }
                    else
                    {
                        objParams = new SqlParameter[48];
                    }

                    objParams[0] = new SqlParameter("@Installation_No", objSpotCheck.InstallationNo);
                    objParams[1] = CommonDataAccess.AddOutputparameter("@Cash_In ", objSpotCheck.CashIn);
                    objParams[2] = CommonDataAccess.AddOutputparameter("@Cash_Out", objSpotCheck.CashOut);
                    objParams[3] = CommonDataAccess.AddOutputparameter("@Token_In", objSpotCheck.TokenIn);
                    objParams[4] = CommonDataAccess.AddOutputparameter("@Token_Out", objSpotCheck.TokenOut);
                    objParams[5] = CommonDataAccess.AddOutputparameter("@Token_Refill", objSpotCheck.TokenRefill);
                    objParams[6] = CommonDataAccess.AddOutputparameter("@Cash_Refill", objSpotCheck.CashRefill);
                    objParams[7] = CommonDataAccess.AddOutputparameter("@COINS_IN", objSpotCheck.CoinsIn);
                    objParams[8] = CommonDataAccess.AddOutputparameter("@COINS_OUT", objSpotCheck.CoinsOut);
                    objParams[9] = CommonDataAccess.AddOutputparameter("@COINS_DROP", objSpotCheck.CoinsDrop);
                    objParams[10] = CommonDataAccess.AddOutputparameter("@CANCELLED_CREDITS", objSpotCheck.CancelledCredits);
                    objParams[11] = CommonDataAccess.AddOutputparameter("@VTP", objSpotCheck.VTP);
                    objParams[12] = CommonDataAccess.AddOutputparameter("@Datetime", objSpotCheck.DateTimeStamp);
                    objParams[13] = CommonDataAccess.AddOutputparameter("@Jackpot", objSpotCheck.Jackpot);
                    objParams[14] = CommonDataAccess.AddOutputparameter("@Handpay", objSpotCheck.HandPay);
                    objParams[15] = CommonDataAccess.AddOutputparameter("@BILL_1", objSpotCheck.Bill1);
                    objParams[16] = CommonDataAccess.AddOutputparameter("@BILL_2", objSpotCheck.Bill2);
                    objParams[17] = CommonDataAccess.AddOutputparameter("@BILL_5", objSpotCheck.Bill5);
                    objParams[18] = CommonDataAccess.AddOutputparameter("@BILL_10", objSpotCheck.Bill10);
                    objParams[19] = CommonDataAccess.AddOutputparameter("@BILL_20", objSpotCheck.Bill20);
                    objParams[20] = CommonDataAccess.AddOutputparameter("@BILL_50", objSpotCheck.Bill50);
                    objParams[21] = CommonDataAccess.AddOutputparameter("@BILL_100", objSpotCheck.Bill100);
                    objParams[22] = CommonDataAccess.AddOutputparameter("@BILL_250", objSpotCheck.Bill250);
                    objParams[23] = CommonDataAccess.AddOutputparameter("@BILL_10000", objSpotCheck.Bill10000);
                    objParams[24] = CommonDataAccess.AddOutputparameter("@BILL_20000", objSpotCheck.Bill20000);
                    objParams[25] = CommonDataAccess.AddOutputparameter("@BILL_50000", objSpotCheck.Bill50000);
                    objParams[26] = CommonDataAccess.AddOutputparameter("@BILL_100000", objSpotCheck.Bill100000);
                    objParams[27] = CommonDataAccess.AddOutputparameter("@Ticktes_Inserted", objSpotCheck.TicketsInserted);
                    objParams[28] = CommonDataAccess.AddOutputparameter("@TRUE_COIN_IN", objSpotCheck.TrueCoinIn);
                    objParams[29] = CommonDataAccess.AddOutputparameter("@TRUE_COIN_OUT", objSpotCheck.TrueCoinOut);
                    objParams[30] = CommonDataAccess.AddOutputparameter("@CASH_IN_1P", objSpotCheck.CashIn1p);
                    objParams[31] = CommonDataAccess.AddOutputparameter("@CASH_IN_2P", objSpotCheck.CashIn2p);
                    objParams[32] = CommonDataAccess.AddOutputparameter("@CASH_IN_100P", objSpotCheck.CashIn100p);
                    objParams[33] = CommonDataAccess.AddOutputparameter("@CASH_IN_200P", objSpotCheck.CashIn200p);
                    objParams[34] = CommonDataAccess.AddOutputparameter("@CASH_IN_500P", objSpotCheck.CashIn500p);
                    objParams[35] = CommonDataAccess.AddOutputparameter("@CASH_IN_1000P", objSpotCheck.CashIn1000p);
                    objParams[36] = CommonDataAccess.AddOutputparameter("@CASH_IN_2000P", objSpotCheck.CashIn2000p);
                    objParams[37] = CommonDataAccess.AddOutputparameter("@Tickets_Printed", objSpotCheck.TicketsPrinted);
                    objParams[38] = new SqlParameter("@bStartOfDay", objSpotCheck.StartOfDay);
                    objParams[39] = new SqlParameter("@bSelectDay", objSpotCheck.SelectDay);
                    objParams[40] = new SqlParameter("@Date", objSpotCheck.Date);
                    objParams[41] = CommonDataAccess.AddOutputparameter("@NoOfDays", 0);
                    objParams[42] = CommonDataAccess.AddOutputparameter("@ProgHandpay", objSpotCheck.ProgHandpay);
                    objParams[43] = CommonDataAccess.AddOutputparameter("@Games_Bet", 0);

                    if (Settings.IsAFTEnabledForSite)
                    {
                        objParams[44] = CommonDataAccess.AddOutputparameter("@CashableAFTIn", objSpotCheck.CashableEFTIn);
                        objParams[45] = CommonDataAccess.AddOutputparameter("@CashableAFTOut", objSpotCheck.CashableEFTOut);
                        objParams[46] = CommonDataAccess.AddOutputparameter("@NonCashableAFTIn", objSpotCheck.NonCashableEFTIn);
                        objParams[47] = CommonDataAccess.AddOutputparameter("@NonCashableAFTOut", objSpotCheck.NonCashableEFTOut);
                        objParams[48] = CommonDataAccess.AddOutputparameter("@WATIn", objSpotCheck.WATIn);
                        objParams[49] = CommonDataAccess.AddOutputparameter("@WATOut", objSpotCheck.WATOut);
                        objParams[50] = CommonDataAccess.AddOutputparameter("@BILL_200", objSpotCheck.Bill200);
                        objParams[51] = CommonDataAccess.AddOutputparameter("@BILL_500", objSpotCheck.Bill500);
                        objParams[52] = CommonDataAccess.AddOutputparameter("@NonCashable_Tickets_Inserted", objSpotCheck.NonCashableTicketsInserted);
                        objParams[53] = CommonDataAccess.AddOutputparameter("@NonCashable_Tickets_Printed", objSpotCheck.NonCashableTicketsPrinted);
                    }
                    else
                    {
                        objParams[44] = CommonDataAccess.AddOutputparameter("@BILL_200", objSpotCheck.Bill200);
                        objParams[45] = CommonDataAccess.AddOutputparameter("@BILL_500", objSpotCheck.Bill500);
                        objParams[46] = CommonDataAccess.AddOutputparameter("@NonCashable_Tickets_Inserted", objSpotCheck.NonCashableTicketsInserted);
                        objParams[47] = CommonDataAccess.AddOutputparameter("@NonCashable_Tickets_Printed", objSpotCheck.NonCashableTicketsPrinted);
                    }

                    SqlConnection objcon = new SqlConnection(CommonDataAccess.ExchangeConnectionString);
                    objcon.Open();
                    SqlCommand objcomd = new SqlCommand("usp_GetSpotCheckDataSAS", objcon);
                    objcomd.CommandType = CommandType.StoredProcedure;
                    objcomd.CommandTimeout = SqlHelper.LoadCommandTimeout();
                    objcomd.Parameters.AddRange(objParams);
                    objcomd.ExecuteReader();
                    objcon.Close();

                    objSpotCheck.CashIn = (objParams[1].Value).ToString().GetDecimal();
                    objSpotCheck.CashOut = (objParams[2].Value).ToString().GetDecimal();
                    objSpotCheck.TokenIn = (objParams[3].Value).ToString().GetDecimal();
                    objSpotCheck.TokenOut = (objParams[4].Value).ToString().GetDecimal();
                    objSpotCheck.TokenRefill = (objParams[5].Value).ToString().GetDecimal();
                    objSpotCheck.CashRefill = (objParams[6].Value).ToString().GetDecimal();
                    objSpotCheck.CoinsIn = (objParams[7].Value).ToString().GetDecimal();
                    objSpotCheck.CoinsOut = (objParams[8].Value).ToString().GetDecimal();
                    objSpotCheck.CoinsDrop = (double)(objParams[9].Value).ToString().GetDecimal();
                    objSpotCheck.CancelledCredits = (objParams[10].Value).ToString().GetDecimal();
                    objSpotCheck.VTP = (objParams[11].Value).ToString().GetDecimal();
                    objSpotCheck.DateTimeStamp = objParams[12].Value.ToString().ReadDate();
                    objSpotCheck.Jackpot = (objParams[13].Value).ToString().GetDecimal();
                    objSpotCheck.HandPay = (objParams[14].Value).ToString().GetDecimal();
                    objSpotCheck.TicketsInserted = (objParams[27].Value).ToString().GetDecimal();
                    objSpotCheck.TrueCoinIn = (objParams[28].Value).ToString().GetDecimal();
                    objSpotCheck.TrueCoinOut = (objParams[29].Value).ToString().GetDecimal();
                    objSpotCheck.TicketsPrinted = (objParams[36].Value).ToString().GetDecimal();

                    objSpotCheck.Bill1 = (objParams[15].Value).ToString().GetDecimal();
                    objSpotCheck.Bill2 = (objParams[16].Value).ToString().GetDecimal();
                    objSpotCheck.Bill5 = (objParams[17].Value).ToString().GetDecimal();
                    objSpotCheck.Bill10 = (objParams[18].Value).ToString().GetDecimal();

                    objSpotCheck.Bill20 = (objParams[19].Value).ToString().GetDecimal();
                    objSpotCheck.Bill50 = (objParams[20].Value).ToString().GetDecimal();
                    objSpotCheck.Bill100 = (objParams[21].Value).ToString().GetDecimal();
                    objSpotCheck.Bill250 = (objParams[22].Value).ToString().GetDecimal();
                    objSpotCheck.Bill10000 = (objParams[23].Value).ToString().GetDecimal();
                    objSpotCheck.Bill20000 = (objParams[24].Value).ToString().GetDecimal();
                    objSpotCheck.Bill50000 = (objParams[25].Value).ToString().GetDecimal();
                    objSpotCheck.Bill100000 = (objParams[26].Value).ToString().GetDecimal();

                    objSpotCheck.CashIn2p = (objParams[30].Value).ToString().GetDecimal();
                    objSpotCheck.CashIn100p = (objParams[31].Value).ToString().GetDecimal();
                    objSpotCheck.CashIn200p = (objParams[32].Value).ToString().GetDecimal();
                    objSpotCheck.CashIn500p = (objParams[33].Value).ToString().GetDecimal();
                    objSpotCheck.CashIn1000p = (objParams[34].Value).ToString().GetDecimal();
                    objSpotCheck.CashIn2000p = (objParams[35].Value).ToString().GetDecimal();
                    objSpotCheck.NumberOfDays = (objParams[40].Value == DBNull.Value) ? 0 : Convert.ToInt32(objParams[40].Value);
                    objSpotCheck.ProgHandpay = (objParams[41].Value).ToString().GetDecimal();
                    objSpotCheck.GamesBet = Convert.ToInt32(objParams[42].Value);

                    if (Settings.IsAFTEnabledForSite)
                    {
                        objSpotCheck.CashableEFTIn = (objParams[43].Value).ToString().GetDecimal();
                        objSpotCheck.CashableEFTOut = (objParams[44].Value).ToString().GetDecimal();
                        objSpotCheck.NonCashableEFTIn = (objParams[45].Value).ToString().GetDecimal();
                        objSpotCheck.NonCashableEFTOut = (objParams[46].Value).ToString().GetDecimal();
                        objSpotCheck.WATIn = (objParams[47].Value).ToString().GetDecimal();
                        objSpotCheck.WATOut = (objParams[48].Value).ToString().GetDecimal();
                        objSpotCheck.Bill200 = (objParams[49].Value).ToString().GetDecimal();
                        objSpotCheck.Bill500 = (objParams[50].Value).ToString().GetDecimal();
                        objSpotCheck.NonCashableTicketsInserted = (objParams[51].Value).ToString().GetDecimal();
                        objSpotCheck.NonCashableTicketsPrinted = (objParams[52].Value).ToString().GetDecimal();
                    }
                    else
                    {
                        objSpotCheck.Bill200 = (objParams[43].Value).ToString().GetDecimal();
                        objSpotCheck.Bill500 = (objParams[44].Value).ToString().GetDecimal();
                        objSpotCheck.NonCashableTicketsInserted = (objParams[45].Value).ToString().GetDecimal();
                        objSpotCheck.NonCashableTicketsPrinted = (objParams[46].Value).ToString().GetDecimal();
                    }
                }
                else
                {
                    if (Settings.IsAFTEnabledForSite)

                    {
                        objParams = new SqlParameter[54];
                    }
                    else
                    {
                        objParams = new SqlParameter[48];
                    }

                    objParams[0] = new SqlParameter("@Installation_No", objSpotCheck.InstallationNo);
                    objParams[1] = CommonDataAccess.AddOutputparameter("@Cash_In ", objSpotCheck.CashIn);
                    objParams[2] = CommonDataAccess.AddOutputparameter("@Cash_Out", objSpotCheck.CashOut);
                    objParams[3] = CommonDataAccess.AddOutputparameter("@Token_In", objSpotCheck.TokenIn);
                    objParams[4] = CommonDataAccess.AddOutputparameter("@Token_Out", objSpotCheck.TokenOut);
                    objParams[5] = CommonDataAccess.AddOutputparameter("@Token_Refill", objSpotCheck.TokenRefill);
                    objParams[6] = CommonDataAccess.AddOutputparameter("@Cash_Refill", objSpotCheck.CashRefill);
                    objParams[7] = CommonDataAccess.AddOutputparameter("@COINS_IN", objSpotCheck.CoinsIn);
                    objParams[8] = CommonDataAccess.AddOutputparameter("@COINS_OUT", objSpotCheck.CoinsOut);
                    objParams[9] = CommonDataAccess.AddOutputparameter("@COINS_DROP", objSpotCheck.CoinsDrop);
                    objParams[10] = CommonDataAccess.AddOutputparameter("@CANCELLED_CREDITS", objSpotCheck.CancelledCredits);
                    objParams[11] = CommonDataAccess.AddOutputparameter("@VTP", objSpotCheck.VTP);
                    objParams[12] = CommonDataAccess.AddOutputparameter("@Datetime", objSpotCheck.DateTimeStamp);
                    objParams[13] = CommonDataAccess.AddOutputparameter("@Jackpot", objSpotCheck.Jackpot);
                    objParams[14] = CommonDataAccess.AddOutputparameter("@Handpay", objSpotCheck.HandPay);
                    objParams[15] = CommonDataAccess.AddOutputparameter("@BILL_1", objSpotCheck.Bill1);
                    objParams[16] = CommonDataAccess.AddOutputparameter("@BILL_2", objSpotCheck.Bill2);
                    objParams[17] = CommonDataAccess.AddOutputparameter("@BILL_5", objSpotCheck.Bill5);
                    objParams[18] = CommonDataAccess.AddOutputparameter("@BILL_10", objSpotCheck.Bill10);
                    objParams[19] = CommonDataAccess.AddOutputparameter("@BILL_20", objSpotCheck.Bill20);
                    objParams[20] = CommonDataAccess.AddOutputparameter("@BILL_50", objSpotCheck.Bill50);
                    objParams[21] = CommonDataAccess.AddOutputparameter("@BILL_100", objSpotCheck.Bill100);
                    objParams[22] = CommonDataAccess.AddOutputparameter("@BILL_250", objSpotCheck.Bill250);
                    objParams[23] = CommonDataAccess.AddOutputparameter("@BILL_10000", objSpotCheck.Bill10000);
                    objParams[24] = CommonDataAccess.AddOutputparameter("@BILL_20000", objSpotCheck.Bill20000);
                    objParams[25] = CommonDataAccess.AddOutputparameter("@BILL_50000", objSpotCheck.Bill50000);
                    objParams[26] = CommonDataAccess.AddOutputparameter("@BILL_100000", objSpotCheck.Bill100000);
                    objParams[27] = CommonDataAccess.AddOutputparameter("@Ticktes_Inserted", objSpotCheck.TicketsInserted);
                    objParams[28] = CommonDataAccess.AddOutputparameter("@TRUE_COIN_IN", objSpotCheck.TrueCoinIn);
                    objParams[29] = CommonDataAccess.AddOutputparameter("@TRUE_COIN_OUT", objSpotCheck.TrueCoinOut);
                    objParams[30] = CommonDataAccess.AddOutputparameter("@CASH_IN_1P", objSpotCheck.CashIn1p);
                    objParams[31] = CommonDataAccess.AddOutputparameter("@CASH_IN_2P", objSpotCheck.CashIn2p);
                    objParams[32] = CommonDataAccess.AddOutputparameter("@CASH_IN_100P", objSpotCheck.CashIn100p);
                    objParams[33] = CommonDataAccess.AddOutputparameter("@CASH_IN_200P", objSpotCheck.CashIn200p);
                    objParams[34] = CommonDataAccess.AddOutputparameter("@CASH_IN_500P", objSpotCheck.CashIn500p);
                    objParams[35] = CommonDataAccess.AddOutputparameter("@CASH_IN_1000P", objSpotCheck.CashIn1000p);
                    objParams[36] = CommonDataAccess.AddOutputparameter("@CASH_IN_2000P", objSpotCheck.CashIn2000p);
                    objParams[37] = CommonDataAccess.AddOutputparameter("@Tickets_Printed", objSpotCheck.TicketsPrinted);
                    objParams[38] = new SqlParameter("@bStartOfDay", objSpotCheck.StartOfDay);
                    objParams[39] = new SqlParameter("@bSelectDay", objSpotCheck.SelectDay);
                    objParams[40] = new SqlParameter("@Date", objSpotCheck.Date);
                    objParams[41] = CommonDataAccess.AddOutputparameter("@NoOfDays", 0);
                    objParams[42] = CommonDataAccess.AddOutputparameter("@ProgHandpay", 0);
                    objParams[43] = CommonDataAccess.AddOutputparameter("@Games_Bet", 0);

                    if (Settings.IsAFTEnabledForSite)
                    {
                        objParams[44] = CommonDataAccess.AddOutputparameter("@CashableAFTIn", objSpotCheck.CashableEFTIn);
                        objParams[45] = CommonDataAccess.AddOutputparameter("@CashableAFTOut", objSpotCheck.CashableEFTOut);
                        objParams[46] = CommonDataAccess.AddOutputparameter("@NonCashableAFTIn", objSpotCheck.NonCashableEFTIn);
                        objParams[47] = CommonDataAccess.AddOutputparameter("@NonCashableAFTOut", objSpotCheck.NonCashableEFTOut);
                        objParams[48] = CommonDataAccess.AddOutputparameter("@WATIn", objSpotCheck.WATIn);
                        objParams[49] = CommonDataAccess.AddOutputparameter("@WATOut", objSpotCheck.WATOut);
                        objParams[50] = CommonDataAccess.AddOutputparameter("@BILL_200", objSpotCheck.Bill200);
                        objParams[51] = CommonDataAccess.AddOutputparameter("@BILL_500", objSpotCheck.Bill500);
                        objParams[52] = CommonDataAccess.AddOutputparameter("@NonCashable_Tickets_Inserted", objSpotCheck.NonCashableTicketsInserted);
                        objParams[53] = CommonDataAccess.AddOutputparameter("@NonCashable_Tickets_Printed", objSpotCheck.NonCashableTicketsPrinted);
                    }
                    else
                    {
                        objParams[44] = CommonDataAccess.AddOutputparameter("@BILL_200", objSpotCheck.Bill200);
                        objParams[45] = CommonDataAccess.AddOutputparameter("@BILL_500", objSpotCheck.Bill500);
                        objParams[46] = CommonDataAccess.AddOutputparameter("@NonCashable_Tickets_Inserted", objSpotCheck.NonCashableTicketsInserted);
                        objParams[47] = CommonDataAccess.AddOutputparameter("@NonCashable_Tickets_Printed", objSpotCheck.NonCashableTicketsPrinted);
                    }

                    SqlConnection objcon = new SqlConnection(CommonDataAccess.ExchangeConnectionString);
                    objcon.Open();
                    SqlCommand objcomd = new SqlCommand("usp_GetSpotCheckDataSAS", objcon);
                    objcomd.CommandType = CommandType.StoredProcedure;
                    objcomd.CommandTimeout = SqlHelper.LoadCommandTimeout();
                    objcomd.Parameters.AddRange(objParams);
                    objcomd.ExecuteReader();
                    objcon.Close();

                    objSpotCheck.CashIn = (objParams[1].Value).ToString().GetDecimal();
                    objSpotCheck.CashOut = (objParams[2].Value).ToString().GetDecimal();
                    objSpotCheck.TokenIn = (objParams[3].Value).ToString().GetDecimal();
                    objSpotCheck.TokenOut = (objParams[4].Value).ToString().GetDecimal();
                    objSpotCheck.TokenRefill = (objParams[5].Value).ToString().GetDecimal();
                    objSpotCheck.CashRefill = (objParams[6].Value).ToString().GetDecimal();
                    objSpotCheck.CoinsIn = (objParams[7].Value).ToString().GetDecimal();
                    objSpotCheck.CoinsOut = (objParams[8].Value).ToString().GetDecimal();
                    objSpotCheck.CoinsDrop = (double)(objParams[9].Value).ToString().GetDecimal();
                    objSpotCheck.CancelledCredits = (objParams[10].Value).ToString().GetDecimal();
                    objSpotCheck.VTP = (objParams[11].Value).ToString().GetDecimal();
                    objSpotCheck.DateTimeStamp = objParams[12].Value.ToString().ReadDate();
                    objSpotCheck.Jackpot = (objParams[13].Value).ToString().GetDecimal();
                    objSpotCheck.HandPay = (objParams[14].Value).ToString().GetDecimal();
                    objSpotCheck.TicketsInserted = (objParams[27].Value).ToString().GetDecimal();
                    objSpotCheck.TrueCoinIn = (objParams[28].Value).ToString().GetDecimal();
                    objSpotCheck.TrueCoinOut = (objParams[29].Value).ToString().GetDecimal();
                    objSpotCheck.TicketsPrinted = (objParams[36].Value).ToString().GetDecimal();

                    objSpotCheck.Bill1 = (objParams[15].Value).ToString().GetDecimal();
                    objSpotCheck.Bill2 = (objParams[16].Value).ToString().GetDecimal();
                    objSpotCheck.Bill5 = (objParams[17].Value).ToString().GetDecimal();
                    objSpotCheck.Bill10 = (objParams[18].Value).ToString().GetDecimal();

                    objSpotCheck.Bill20 = (objParams[19].Value).ToString().GetDecimal();
                    objSpotCheck.Bill50 = (objParams[20].Value).ToString().GetDecimal();
                    objSpotCheck.Bill100 = (objParams[21].Value).ToString().GetDecimal();
                    objSpotCheck.Bill250 = (objParams[22].Value).ToString().GetDecimal();
                    objSpotCheck.Bill10000 = (objParams[23].Value).ToString().GetDecimal();
                    objSpotCheck.Bill20000 = (objParams[24].Value).ToString().GetDecimal();
                    objSpotCheck.Bill50000 = (objParams[25].Value).ToString().GetDecimal();
                    objSpotCheck.Bill100000 = (objParams[26].Value).ToString().GetDecimal();

                    objSpotCheck.CashIn2p = (objParams[30].Value).ToString().GetDecimal();
                    objSpotCheck.CashIn100p = (objParams[31].Value).ToString().GetDecimal();
                    objSpotCheck.CashIn200p = (objParams[32].Value).ToString().GetDecimal();
                    objSpotCheck.CashIn500p = (objParams[33].Value).ToString().GetDecimal();
                    objSpotCheck.CashIn1000p = (objParams[34].Value).ToString().GetDecimal();
                    objSpotCheck.CashIn2000p = (objParams[35].Value).ToString().GetDecimal();
                    objSpotCheck.NumberOfDays = (objParams[40].Value == DBNull.Value) ? 0 : Convert.ToInt32(objParams[40].Value);
                    objSpotCheck.ProgHandpay = (objParams[41].Value).ToString().GetDecimal();
                    objSpotCheck.GamesBet = Convert.ToInt32(objParams[42].Value);

                    if (Settings.IsAFTEnabledForSite)
                    {
                        objSpotCheck.CashableEFTIn = (objParams[43].Value).ToString().GetDecimal();
                        objSpotCheck.CashableEFTOut = (objParams[44].Value).ToString().GetDecimal();
                        objSpotCheck.NonCashableEFTIn = (objParams[45].Value).ToString().GetDecimal();
                        objSpotCheck.NonCashableEFTOut = (objParams[46].Value).ToString().GetDecimal();
                        objSpotCheck.WATIn = (objParams[47].Value).ToString().GetDecimal();
                        objSpotCheck.WATOut = (objParams[48].Value).ToString().GetDecimal();
                        objSpotCheck.Bill200 = (objParams[49].Value).ToString().GetDecimal();
                        objSpotCheck.Bill500 = (objParams[50].Value).ToString().GetDecimal();
                        objSpotCheck.NonCashableTicketsInserted = (objParams[51].Value).ToString().GetDecimal();
                        objSpotCheck.NonCashableTicketsPrinted = (objParams[52].Value).ToString().GetDecimal();
                    }
                    else
                    {
                        objSpotCheck.Bill200 = (objParams[43].Value).ToString().GetDecimal();
                        objSpotCheck.Bill500 = (objParams[44].Value).ToString().GetDecimal();
                        objSpotCheck.NonCashableTicketsInserted = (objParams[45].Value).ToString().GetDecimal();
                        objSpotCheck.NonCashableTicketsPrinted = (objParams[46].Value).ToString().GetDecimal();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        public DataTable GetAnalysisDetails(int InstallationNo, int StartOfDay, int SelectDay, DateTime Date)
        {
            SqlParameter[] objParams = null;
            DataTable dtAnalysisDetail = new DataTable();
            try
            {
                objParams = new SqlParameter[4];
                objParams[0] = new SqlParameter("@Installation_No", InstallationNo);
                objParams[1] = new SqlParameter("@bStartOfDay", StartOfDay);
                objParams[2] = new SqlParameter("@bSelectDay", SelectDay);
                objParams[3] = new SqlParameter("@Date", Date);

                dtAnalysisDetail = DataBaseServiceHandler.Fill(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, "rsp_GetAnalysisDetails", dtAnalysisDetail, objParams);

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return dtAnalysisDetail;
        }

        public DataTable GetWTDMTD()
        {
            DataTable dtWTDMTD = new DataTable();

            try
            {
                dtWTDMTD = DataBaseServiceHandler.Fill(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, "rsp_GetWTDMTD", dtWTDMTD);
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }

            return dtWTDMTD;
        }

        public DataTable GetBarPositionDetails(string SortBy, int InstallNo)
        {
            DataTable BarPositionDetail = new DataTable();

            try
            {
                //DataBaseServiceHandler.ConnectionString = CommonDataAccess.ExchangeConnectionString;
                BarPositionDetail = DataBaseServiceHandler.Fill(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, "rsp_GetBarPositionDetailsForCashDeskoperator", BarPositionDetail,
                    DataBaseServiceHandler.AddParameter<string>("SortBy", DbType.String, SortBy),
                    DataBaseServiceHandler.AddParameter<int>("InstallNo", DbType.Int32, InstallNo));
                //BarPositionDetail = SqlHelper.ExecuteDataset(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, "rsp_GetBarPositionDetailsForCashDeskoperator").Tables[0];
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }

            return BarPositionDetail;
        }

        public void SaveFloorPosition(int slotID, int securityUserID, int topPosition, int leftPosition)
        {
            try
            {
                //DataBaseServiceHandler.ConnectionString = CommonDataAccess.ExchangeConnectionString;
                DataBaseServiceHandler.ExecuteNonQuery(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, "usp_SetBarPositionDetailsForCashDeskoperator",
                    DataBaseServiceHandler.AddParameter<int>("@Bar_Pos_No", DbType.Int32, slotID),
                    DataBaseServiceHandler.AddParameter<int>("@SecurityUserID", DbType.Int32, securityUserID),
                    DataBaseServiceHandler.AddParameter<int>("@Top", DbType.Int32, topPosition),
                    DataBaseServiceHandler.AddParameter<int>("@Left", DbType.Int32, leftPosition));
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
        }

        public DataTable GetSpotCheckDataSAS(int InstallationNo, int SelectDay, DateTime date)
        {
            DataTable dtSpotCheckDataSAS = new DataTable();
            SqlParameter[] objParams = null;
            SqlParameter[] objOutputParams = null;
            try
            {

                objParams = SqlHelperParameterCache.GetSpParameterSet(CommonDataAccess.ExchangeConnectionString, "usp_GetSpotCheckDataSAS", true);
                objOutputParams = (objParams.Length > 0) ? new SqlParameter[objParams.Length] : null;
                if (objParams.Length > 0)
                {
                    for (int i = 0; i <= objParams.Length - 1; i++)
                    {
                        if (objParams[i].Direction == ParameterDirection.InputOutput || objParams[i].Direction == ParameterDirection.Output ||
                            objParams[i].Direction == ParameterDirection.ReturnValue)
                        {
                            objOutputParams[i] = objParams[i];
                            dtSpotCheckDataSAS.Columns.Add(objOutputParams[i].ParameterName.Replace('@', ' ').Trim());
                            if (objParams[i].ParameterName == "@Datetime")
                            {
                                objParams[i].Value = DateTime.Now;
                            }
                            else if (objParams[i].ParameterName == "@COINS_DROP")
                            {
                                objParams[i].Value = 0.0;
                            }
                            else { objParams[i].Value = 0; }
                        }
                        else
                        {
                            if (objParams[i].ParameterName == "@bSelectDay")
                            {
                                objParams[i].Value = 0;
                            }
                            else if (objParams[i].ParameterName == "@bStartOfDay")
                            {
                                objParams[i].Value = SelectDay; ;
                            }
                            else if (objParams[i].ParameterName == "@Date")
                            {
                                objParams[i].Value = date;
                            }
                            else if (objParams[i].ParameterName == "@Installation_No")
                            {
                                objParams[i].Value = InstallationNo;
                            }
                        }
                    }
                }

                SqlHelper.ExecuteNonQuery(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, "usp_GetSpotCheckDataSAS", objParams);

                var dr = dtSpotCheckDataSAS.NewRow();

                for (int i = 0; i <= objOutputParams.Length - 1; i++)
                {
                    if (objParams[i].ParameterName == ((objOutputParams[i] != null) ? objOutputParams[i].ParameterName : ""))
                    {
                        dr[objOutputParams[i].ParameterName.Replace('@', ' ').Trim().ToString()] = objOutputParams[i].Value;
                    }
                }
                dtSpotCheckDataSAS.Rows.Add(dr);

            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }

            return dtSpotCheckDataSAS;
        }

        public DataTable GetTicketsClaimedByCDForPeriod(int SelectDay, DateTime IsDate, int MachineNo)
        {
            DataTable dt = new DataTable();

            try
            {
                //DataBaseServiceHandler.ConnectionString = CommonDataAccess.ExchangeConnectionString;
                dt = DataBaseServiceHandler.Fill(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, "rsp_GetTicketsClaimedByCDForPeriod", dt,
                DataBaseServiceHandler.AddParameter<int>("@MachineNo", DbType.Int32, MachineNo),
                DataBaseServiceHandler.AddParameter<int>("@bStartOfDay", DbType.Int32, SelectDay),
                DataBaseServiceHandler.AddParameter<DateTime>("@Date", DbType.DateTime, IsDate));
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }

            return dt;
        }

        public DataTable GetEPIDetails(int InstallationNo)
        {
            string sqlQuery;
            DataTable EPIDetail = new DataTable();
            //sqlQuery = "SELECT EPIDetails = ISNULL(LTRIM(RTRIM(EPIDetails)), 0), CardinTime ,IsEPIAvailable = CASE WHEN ISNULL(LTRIM(RTRIM(EPIDetails)), 0)  IN ('0', '')  THEN '0' ELSE '1' END FROM Floor_status WHERE INSTALLATION_NO  = " + InstallationNo.ToString();

            try
            {
                //DataBaseServiceHandler.ConnectionString = CommonDataAccess.ExchangeConnectionString;
                //EPIDetail = DataBaseServiceHandler.Fill(CommonDataAccess.ExchangeConnectionString, CommandType.Text, sqlQuery, EPIDetail);
                EPIDetail = DataBaseServiceHandler.Fill(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, "rsp_PlayerClub_GetEPIDetails", EPIDetail,
                DataBaseServiceHandler.AddParameter<int>("@Installation_ID", DbType.Int32, InstallationNo));
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }

            return EPIDetail;
        }

        public DataTable GetPlayerDetails(Int32 InstallationNo)
        {
            DataTable PlayerDetail = new DataTable();

            try
            {
                PlayerDetail = DataBaseServiceHandler.Fill(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, "rsp_PlayerClub_GetPlayerDetails", PlayerDetail,
                DataBaseServiceHandler.AddParameter<Int32>("@Installation_No", DbType.Int32, InstallationNo));
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }

            return PlayerDetail;
        }

        public DataTable GetTicketException(int InstallationNo)
        {
            DataTable TicketException = new DataTable();
            string sqlQuery;
            sqlQuery = " SELECT TE_ID FROM Installation tI INNER JOIN Ticket_Exception tTE ON tTE.TE_Installation_No = tI.Installation_No ";
            sqlQuery = sqlQuery + "WHERE te_status_create_actual='HANDPAY' AND coalesce(te_status_final_actual,'') <> 'HANDPAY' AND tI.Installation_No = " + InstallationNo.ToString();

            try
            {
                //DataBaseServiceHandler.ConnectionString = CommonDataAccess.ExchangeConnectionString;
                TicketException = DataBaseServiceHandler.Fill(CommonDataAccess.ExchangeConnectionString, CommandType.Text, sqlQuery, TicketException);
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }

            return TicketException;
        }

        public DataTable GetHandpaystoClear(int InstallationNo)
        {
            DataTable Handpay = new DataTable();

            try
            {
                //DataBaseServiceHandler.ConnectionString = CommonDataAccess.ExchangeConnectionString;
                Handpay = DataBaseServiceHandler.Fill(CommonDataAccess.ExchangeConnectionString, CommandType.Text, "Select IsNull(Datapak_No, 0) As Datapak_No From installation Where Installation_no = " + InstallationNo.ToString(), Handpay);
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }

            return Handpay;
        }

        #endregion

        #region "Public Static Function"
        #endregion

    }
}
