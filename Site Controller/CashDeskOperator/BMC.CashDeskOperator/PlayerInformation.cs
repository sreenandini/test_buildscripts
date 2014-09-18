using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BMC.Transport.CashDeskOperatorEntity;
using BMC.Business.CashDeskOperator;

namespace BMC.CashDeskOperator.BusinessObjects
{
    public class PlayerInformationBusinessObject:IPlayerInformation
    {
        #region Private Variables
        #endregion

        #region Constructor
        private PlayerInformationBusinessObject() { }
        #endregion

        Kiosk kiosk = new Kiosk();

        #region Pubic Function

        /// <summary>
        /// Retrieve Player Information.
        /// </summary>
        ///  <param name="AccountNumber"></param>
        /// <returns >Dictionary<string,string></returns>       
        public Dictionary<string, string> GetPlayerInfo(string AccountNumber)
        {
            return kiosk.GetPlayerInfo(AccountNumber);
        }

        /// <summary>
        /// Retrieve Prize Details.
        /// </summary>
        /// <param name="AccountNumber"></param>
        /// <returns >List<PrizeInfo></returns>        
        public List<PrizeInfoDTO> RetreivePrizeInfo(string AccountNumber)
        {
            return kiosk.GetPrizeInfo(AccountNumber);
        }

        /// <summary>
        /// Update the current balance with points redeemed.
        /// </summary>
        /// <param name="PrizeID"></param>
        /// <param name="PrizeQty"></param>
        /// <param name="strAcctNumber"></param>
        /// <returns >success or failure</returns>       
        public bool UpdateRedeempoints(string AcctNumber, string PrizeID, int PrizeQty, int RedeemPoints,LoginInfoDTO loginInfo,PlayerInfoDTO playerInfo)
        {
            return kiosk.UpdateRedeempoints(AcctNumber, PrizeID, PrizeQty, RedeemPoints,loginInfo,playerInfo);
        }

        public bool CheckAccountNumber(string AccountNumber)
        {
            return kiosk.CheckAccountNumber(AccountNumber);

        }

        public LoginInfoDTO GetLoginInformation()
        {
            return kiosk.GetLoginInformation();

        }

        public PlayerInfoDTO GetPlayerInformation(string strAccountNumber)
        {
            return kiosk.GetPlayerInfoDTO(strAccountNumber);
        }

        /// <summary>
        /// Update the points which can be redeeemed and the cash value.
        /// </summary>
        /// <param name=""></param>
        /// <returns >success or failure</returns>       
        public bool UpdateUnitCashPoints(string strPrizeName, int RedeemPoints, float CashValue)
        {
            return kiosk.UpdateUnitCashPoints(strPrizeName, RedeemPoints, CashValue);
        }

        public bool CheckForPrefixSuffixSetting()
        {
            return kiosk.CheckForPrefixSuffixSetting();
        }

        /// <summary>
        /// To check whether Enable Redeem Receipt printer is enabled.
        /// </summary>
        /// <param name=""></param>
        /// <returns >true or false</returns>        
        public bool CheckEnableRedeemPrintCDO()
        {
            return kiosk.CheckEnableRedeemPrintCDO();
        }

        public string[] GetCardInformation(string cardNumber)
        {
            return kiosk.GetCardInformation(cardNumber);
        }

        #endregion

        #region Public Static Function
        public static IPlayerInformation CreateInstance()
        {
            return new PlayerInformationBusinessObject();
        }
        #endregion

        #region IPlayerInformation Members


        #endregion
    }
}
