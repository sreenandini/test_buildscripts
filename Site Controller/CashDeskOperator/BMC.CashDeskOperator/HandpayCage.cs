using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BMC.Transport.CashDeskOperatorEntity;
using BMC.Business.CashDeskOperator;
using BMC.Common.LogManagement;


namespace BMC.CashDeskOperator.BusinessObjects
{
    public partial class HandpayBusinessObject : IHandpay
    {

        public BMC.Transport.jackpotProcessInfoDTO getJackpotStatusAmount(string JackpotSlipNumber, int Site)
        {
            return handPay.getJackpotStatusAmount(JackpotSlipNumber, Site);
        }

        public BMC.Transport.jackpotProcessInfoDTO payJackpot(string SequenceNumber, int SiteId, string userId, string firstName, string lastName, string cashDeskLocation)
        {

                return handPay.payJackpot(SequenceNumber, SiteId, userId, firstName, lastName, cashDeskLocation);
        }

        public DataSet createTickeException_HandpayCAGE(int Installation_No, Double TicketValue, int isHandpayResponse, string HP_Type)
        {

            return handPay.CreateTickeException_HandpayCage(Installation_No, Math.Round( TicketValue * 100), isHandpayResponse, HP_Type);

        }

        public void PrintSlip(BMC.Transport.jackpotProcessInfoDTO jpinfo)
        {
            PrintCageSlip oSlip = new PrintCageSlip();
            oSlip.PrintSlip(jpinfo);
        }



        #region IHandpay Members


        public List<BarPositions> GetBarPositions()
        {
            return handPay.GetBarPositions();
        }

        #endregion

        #region IHandpay Members

        #endregion


        #region IHandpay Members


        public List<AssetNumberResult> GetAssetNumber(int installation_No)
        {
            return handPay.GetAssetNumber(installation_No);
        }

        #endregion

        #region IHandpay Members

        public bool CheckIfHandpayProcessed(FillTreasuryList fillTreasuryList)
        {
            return handPay.CheckIfHandpayProcessed(fillTreasuryList);
            
        }

        #endregion
    }
}
