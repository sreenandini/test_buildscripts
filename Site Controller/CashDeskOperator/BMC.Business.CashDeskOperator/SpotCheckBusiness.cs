using System;
using System.Collections.Generic;
using System.Linq;
using BMC.Common.ExceptionManagement;
using BMC.DBInterface.CashDeskOperator;
using BMC.Transport.CashDeskOperatorEntity;
using BMC.Security;

namespace BMC.Business.CashDeskOperator
{
    public class SpotCheckBusiness
    {

        #region Constructor

        public SpotCheckBusiness()
        {
        }

        #endregion //Constructor

        #region Methods

        public List<Installations> GetInstallationDetails()
        {
            List<Installations> lstInstallationDetails = null;
            try
            {
                List<rsp_GetInstallationDetailsForSpotCheckResult> lstInstallationDetailsForSpotCheckResult = SpotCheckDataAccess.GetInstallationDetails().ToList();
                lstInstallationDetails = (from obj in lstInstallationDetailsForSpotCheckResult
                                          select new Installations
                          {
                              Installation_No = obj.Installation_No,
                              Bar_Position_Name = obj.Bar_Position_Name,
                              GameTitle = obj.GameTitle,
                              POP = obj.POP,
                              PercentagePayout = obj.PercentagePayout,
                              Zone_Name = obj.Zone_Name,
                              DisplayName = obj.DisplayName,
                              Installation_StartDate = obj.Installation_StartDate
                              
                          }).ToList<Installations>();

                if (lstInstallationDetails.Count > 0)
                    lstInstallationDetails[0].IsSelected = true;
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return lstInstallationDetails;
        }

        public List<SpotCheck> GetSpotCheckSummaryDetails(int iInstallation_No, int? iPop)
        {
            List<SpotCheck> lstSpotCheckSummaryDetails = null;
            try
            {
                List<usp_GetSpotCheckDataByDropResult> lstSpotCheckDataByDropResult = SpotCheckDataAccess.GetSpotCheckSummaryDetails(iInstallation_No, Convert.ToInt32(iPop)).ToList();
                lstSpotCheckSummaryDetails = (from obj in lstSpotCheckDataByDropResult
                                              select new SpotCheck
                                          {
                                              InstallationNo = Convert.ToInt32(obj.Installation_No),
                                              CashIn = Convert.ToDecimal(obj.CASH_IN),
                                              CashOut = Convert.ToDecimal(obj.CASH_OUT),
                                              TokenIn = Convert.ToDecimal(obj.TOKEN_IN),
                                              TokenOut = Convert.ToDecimal(obj.TOKEN_OUT),
                                              TokenRefill = Convert.ToDecimal(obj.TOKEN_REFILL),
                                              CashRefill = Convert.ToDecimal(obj.CASH_REFILL),
                                              CoinsIn = Convert.ToDecimal(obj.COINS_IN),
                                              CoinsOut = Convert.ToDecimal(obj.COINS_OUT),
                                              CoinsDrop = Convert.ToDouble(obj.COINS_DROP),
                                              CancelledCredits = Convert.ToDecimal(obj.CANCELLED_CREDITS),
                                              VTP = Convert.ToDecimal(obj.VTP),
                                              DateTimeStamp = Convert.ToDateTime(obj.DateTimeStamp),
                                              Date = Convert.ToDateTime(obj.Date),
                                              Jackpot = Convert.ToDecimal(obj.JACKPOT),
                                              HandPay = Convert.ToDecimal(obj.Handpay),
                                              Bill1 = Convert.ToDecimal(obj.BILL_1),
                                              Bill2 = Convert.ToDecimal(obj.BILL_2),
                                              Bill5 = Convert.ToDecimal(obj.BILL_5),
                                              Bill10 = Convert.ToDecimal(obj.BILL_10),
                                              Bill20 = Convert.ToDecimal(obj.BILL_20),
                                              Bill50 = Convert.ToDecimal(obj.BILL_50),
                                              Bill100 = Convert.ToDecimal(obj.BILL_100),
                                              Bill250 = Convert.ToDecimal(obj.BILL_250),
                                              Bill200 = Convert.ToDecimal(obj.BILL_200),
                                              Bill500 = Convert.ToDecimal(obj.BILL_500),
                                              Bill10000 = Convert.ToDecimal(obj.BILL_10000),
                                              Bill20000 = Convert.ToDecimal(obj.BILL_20000),
                                              Bill50000 = Convert.ToDecimal(obj.BILL_50000),
                                              Bill100000 = Convert.ToDecimal(obj.BILL_100000),
                                              TicketsInserted = Convert.ToDecimal(obj.Ticktes_Inserted),
                                              TrueCoinIn = Convert.ToDecimal(obj.TRUE_COIN_IN),
                                              TrueCoinOut = Convert.ToDecimal(obj.TRUE_COIN_OUT),
                                              CashIn1p = Convert.ToDecimal(obj.CASH_IN_1P),
                                              CashIn2p = Convert.ToDecimal(obj.CASH_IN_2P),
                                              CashIn100p = Convert.ToDecimal(obj.CASH_IN_100P),
                                              CashIn200p = Convert.ToDecimal(obj.CASH_IN_200P),
                                              CashIn500p = Convert.ToDecimal(obj.CASH_IN_500P),
                                              CashIn1000p = Convert.ToDecimal(obj.CASH_IN_1000P),
                                              CashIn2000p = Convert.ToDecimal(obj.CASH_IN_2000P),
                                              TicketsPrinted = Convert.ToDecimal(obj.Tickets_Printed),
                                              ProgHandpay = Convert.ToDecimal(obj.ProgHandpay),
                                              GamesBet = Convert.ToInt32(obj.Games_Bet),
                                              CashableEFTIn = Convert.ToDecimal(obj.CashableAFTIn),
                                              CashableEFTOut = Convert.ToDecimal(obj.CashableAFTOut),
                                              NonCashableEFTIn = Convert.ToDecimal(obj.NonCashableAFTIn),
                                              NonCashableEFTOut = Convert.ToDecimal(obj.NonCashableAFTOut),
                                              WATIn = Convert.ToDecimal(obj.WATIn),
                                              WATOut = Convert.ToDecimal(obj.WATOut),
                                              NonCashableTicketsInserted = Convert.ToDecimal(obj.NonCashable_Tickets_Inserted),
                                              NonCashableTicketsPrinted = Convert.ToDecimal(obj.NonCashable_Tickets_Printed),
                                              Payout=Convert.ToDecimal(obj.PercentagePayout)

                                          }).ToList<SpotCheck>();
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return lstSpotCheckSummaryDetails;
        }

        #endregion //Methods
    }
}
