using System;
using System.Linq;
using System.Collections.Generic;
using BMC.Common.ExceptionManagement;
using BMC.Common.LogManagement;
using BMC.DBInterface.CashDeskOperator;
using BMC.Transport.CashDeskOperatorEntity;
using System.Data;
using System.Reflection;
using BMC.Business.CashDeskOperator.WebServices;



namespace BMC.Business.CashDeskOperator
{
    public class Kiosk
    {
        #region Private Variables
            private KioskService kioskService;
            private PlayerInfo playerInfo;
            private ServiceResult serviceResult;
            private LoginInfo loginInfo;
            private PlayerInformationDataAccess playerInformationDataAccess = new PlayerInformationDataAccess();
        #endregion

        #region "Public Functions"

        /// <summary>
        /// Get the player Information
        /// </summary>
        /// <param name="strConnect"></param>
        /// <returns>dictionary</returns>
        public Dictionary<string, string> GetPlayerInfo(string AccountNumber)
        {
            Dictionary<string, string> dicPlayerInfo = new Dictionary<string, string>();

            try
            {
                PlayerInfoDTO playerInfo= GetPlayerInfoDTO(AccountNumber);
                if (AccountNumber != null)
                {
                    dicPlayerInfo.Add("AccountNumber", AccountNumber);
                    dicPlayerInfo.Add("ClubState", playerInfo.ClubState);
                    dicPlayerInfo.Add("ClubStatus", playerInfo.ClubStatus);
                    dicPlayerInfo.Add("DisplayName", playerInfo.DisplayName);
                    dicPlayerInfo.Add("FirstName", playerInfo.FirstName);
                    dicPlayerInfo.Add("LastName", playerInfo.LastName);
                    dicPlayerInfo.Add("PlayerId", playerInfo.PlayerID.ToString());
                    dicPlayerInfo.Add("PointsBalance", playerInfo.PointsBalance.ToString());
                }

                return dicPlayerInfo;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return null;
            }
        }

         /// <summary>
        /// Get the player Information
        /// </summary>
        /// <param name="strConnect"></param>
        /// <returns>dictionary</returns>
        public PlayerInfoDTO GetPlayerInfoDTO(string AccountNumber)
        {
            Dictionary<string, string> CMPDetails = playerInformationDataAccess.GetCMPCredentials(CommonDataAccess.ExchangeConnectionString);


            if (CMPDetails.Count > 0)
            {
                kioskService = new KioskService(CMPDetails["CMPURL"].ToString());
            }

            playerInfo = kioskService.RetrievePlayerInfo(AccountNumber);

            PlayerInfoDTO PlayerInfo = new PlayerInfoDTO();
            PlayerInfo.AccountNumber = playerInfo.AccountNumber;
            PlayerInfo.ClubState = playerInfo.ClubState;
            PlayerInfo.ClubStatus = playerInfo.ClubStatus;
            PlayerInfo.DisplayName = playerInfo.DisplayName;
            PlayerInfo.FirstName = playerInfo.FirstName;
            PlayerInfo.LastName = playerInfo.LastName;
            PlayerInfo.PlayerID = playerInfo.PlayerId;
            PlayerInfo.PointsBalance = playerInfo.PointsBalance;

            return PlayerInfo;
        }

        /// <summary>
        /// Get the Prize Information for the player
        /// </summary>
        /// <param name="AccountNumber"></param>
        /// <returns>List of prizes available</returns>
        public List<PrizeInfoDTO> GetPrizeInfo(string AccountNumber)
        {
            List<PrizeInfoDTO> listPrizeInfo = new List<PrizeInfoDTO>();

            try
            {

                LoginInfoDTO loginInfoDTO = GetLoginInformation();
                serviceResult = kioskService.EmployeeLogin(loginInfo);

                if (MethodResult.Success == serviceResult.Result)
                {
                    loginInfo.LocationCode = serviceResult.Data[0].ToString();
                    loginInfoDTO.LocationCode = serviceResult.Data[0].ToString();
                    DateTime gamingdate = DateTime.Today;
                    if (true == DateTime.TryParse(serviceResult.Data[1].ToString(), out gamingdate))
                    {
                        loginInfo.GamingDate = gamingdate;
                        loginInfoDTO.GamingDate = gamingdate;
                    }
                    else
                    {
                        loginInfo.GamingDate = DateTime.Today;
                        loginInfoDTO.GamingDate = DateTime.Today;
                    }

                    loginInfo.Shift = int.Parse(serviceResult.Data[2].ToString());
                    loginInfoDTO.Shift = int.Parse(serviceResult.Data[2].ToString());
                }
                PrizeInfo[] PrizeInfoTbl = kioskService.RetrievePriceList(AccountNumber, loginInfo);
                List<PrizeInfoDTO> prizeInfoArray = new List<PrizeInfoDTO>();

                foreach (PrizeInfo item in PrizeInfoTbl)
                {
                    PrizeInfoDTO PrizeInfoObject = new PrizeInfoDTO();
                    PrizeInfoObject.AuthAward = item.AuthAward.ToString();
                    PrizeInfoObject.AwardUsed = item.AwardUsed.ToString();
                    PrizeInfoObject.BasePoints = item.BasePoints.ToString();
                    PrizeInfoObject.BonusPoints = item.BonusPoints.ToString();
                    PrizeInfoObject.PrizeId = item.PrizeId;
                    PrizeInfoObject.PrizeName = item.PrizeName;
                    PrizeInfoObject.RedeemPoints = item.RedeemPoints.ToString();
                    prizeInfoArray.Add(PrizeInfoObject);
                }

                foreach (PrizeInfoDTO objPrizeInfo in prizeInfoArray)
                {
                    listPrizeInfo.Add(objPrizeInfo);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return null;
            }
            return listPrizeInfo;
        }

        /// <summary>
        /// Get Login Information for the player
        /// </summary>
        /// 
        /// <returns>Login information for the player</returns>
        /// 
        public LoginInfoDTO GetLoginInformation()
        {
            Dictionary<string, string> CMPDetails = playerInformationDataAccess.GetCMPCredentials(CommonDataAccess.ExchangeConnectionString);

            loginInfo = new LoginInfo();
            loginInfo.ComputerName = Environment.MachineName;
            
            loginInfo.GamingDate = new DateTime(2001,01,01,0,0,0);
            
            loginInfo.LocationCode = null;
            loginInfo.Password = CMPDetails["CMPPWD"];
            loginInfo.Shift = 1;
            loginInfo.UserName = CMPDetails["CMPUSER"];

            if (CMPDetails.Count > 0)
            {
                kioskService = new KioskService(CMPDetails["CMPURL"].ToString());
            }

            serviceResult = kioskService.EmployeeLogin(loginInfo);

            if (MethodResult.Success == serviceResult.Result)
            {
                loginInfo.LocationCode = serviceResult.Data[0].ToString();
                DateTime gamingdate = DateTime.Today;
                if (true == DateTime.TryParse(serviceResult.Data[1].ToString(), out gamingdate))
                    loginInfo.GamingDate = gamingdate;
                else
                    loginInfo.GamingDate = DateTime.Today;

                loginInfo.Shift = int.Parse(serviceResult.Data[2].ToString());
            }
     
            List<LoginInfoDTO> LoginInfoList = new List<LoginInfoDTO>();

           
                LoginInfoDTO loginInfoDTO = new LoginInfoDTO();
                loginInfoDTO.ComputerName = loginInfo.ComputerName;
                loginInfoDTO.GamingDate = loginInfo.GamingDate;
                loginInfoDTO.LocationCode = loginInfo.LocationCode;
                loginInfoDTO.Password = loginInfo.Password;
                loginInfoDTO.Shift = loginInfo.Shift;
                loginInfoDTO.UserName = loginInfo.UserName;
            return loginInfoDTO;
        }

        /// <summary>
        /// Redeem Points for Player.
        /// </summary>
        /// <param name="AccountNumber"></param>
        /// 
        /// <returns>List of prizes available</returns>
        public bool UpdateRedeempoints(string AcctNumber, string PrizeID, int PrizeQty, int redeempoints,LoginInfoDTO loginInfo,PlayerInfoDTO playerInfo)
        {
            bool IsReedemed = false;
            try
            {
                Dictionary<string, string> CMPDetails = playerInformationDataAccess.GetCMPCredentials(CommonDataAccess.ExchangeConnectionString);

                if (CMPDetails.Count > 0)
                {
                    kioskService = new KioskService(CMPDetails["CMPURL"].ToString());
                }

                RedeemPrizeInfo redeemPrizeInfo = new RedeemPrizeInfo();
                redeemPrizeInfo.AccountNumber = AcctNumber;
                redeemPrizeInfo.ComputerName = Environment.MachineName;
                redeemPrizeInfo.GamingDate = new DateTime(2001,1,1);
                redeemPrizeInfo.LocationCode = loginInfo.LocationCode;
                redeemPrizeInfo.PrintedRemarks = null;
                redeemPrizeInfo.PrivateRemarks = null;
                redeemPrizeInfo.PlayerId = playerInfo.PlayerID;
                redeemPrizeInfo.Password = CMPDetails["CMPPWD"];
                redeemPrizeInfo.Shift = 1;

                redeemPrizeInfo.UserName = CMPDetails["CMPUSER"];

                if (!String.IsNullOrEmpty(PrizeID))
                {
                    redeemPrizeInfo.PrizeId = PrizeID;
                }
                redeemPrizeInfo.PrizeQty = PrizeQty;
                redeemPrizeInfo.RedeemPoints = redeempoints * redeemPrizeInfo.PrizeQty;

                ServiceResult objServiceResult = (ServiceResult)kioskService.RedeemPoints(redeemPrizeInfo);

                if (MethodResult.Success == objServiceResult.Result)
                    IsReedemed = true;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                IsReedemed = false;
            }
            return IsReedemed;
        }

        /// <summary>
        /// Check Account Number
        /// </summary>
        /// <param name="AccountNumber"></param>
        /// 
        /// <returns>success or failure</returns>
        public bool CheckAccountNumber(string AccountNumber)
        {
            try
            {
                return (new PlayerInformationDataAccess()).CheckAccountNumber(AccountNumber);
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                return false;
            }
        }

        /// <summary>
        /// Check Account Number
        /// </summary>
        /// <param name="AccountNumber"></param>
        /// 
        /// <returns>success or failure</returns>
        public bool UpdateUnitCashPoints(string PrizeName, int RedeemPoints, float CashValue)
        {
            try
            {
                return (new PlayerInformationDataAccess()).UpdateUnitCashPointValue(PrizeName, RedeemPoints, CashValue);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return false;
            }
        }

        public bool CheckForPrefixSuffixSetting()
        {
            try
            {
                return playerInformationDataAccess.CheckForPrefixSuffixSetting();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return false;
            }

        }

        /// <summary>
        /// To check whether Enable Redeem Receipt printer is enabled.
        /// </summary>
        /// <param name=""></param>
        /// <returns >true or false</returns>
        public bool CheckEnableRedeemPrintCDO()
        {
            try
            {
                return playerInformationDataAccess.CheckEnableRedeemPrintCDODB();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return false;
            }
        }

        public string[] GetCardInformation(string cardNumber)
        {
            return playerInformationDataAccess.GetCardInformation(cardNumber);
        }

        public Dictionary<string, string> RetrievePlayerDetailsFromEPI(int InstallationNo)
        {

            Dictionary<string, string> _PlayerInfo = null;
            AnalysisDataAccess analysisBusinessObject = new AnalysisDataAccess();
            DataTable dt = analysisBusinessObject.GetEPIDetails(InstallationNo);

            _PlayerInfo = new Dictionary<string, string>();

            PlayerInfo _POSDetail = null;
            foreach (DataRow dr in dt.Rows)
            {
                _POSDetail = new PlayerInfo();
                if (dr["IsEPIAvailable"].ToString() == "1")
                {
                  
                    _POSDetail.IsEPIAvailable = true;
                    _POSDetail.AccountNumber = dr["EPIDetails"].ToString();
                    Dictionary<string, string> PlayerInfo = GetPlayerInfo(_POSDetail.AccountNumber);
                    if (PlayerInfo != null)
                    {
                        //string PlayerName;
                        string FirstName, LastName = string.Empty;
                        string PlayerStatus;
                        TimeSpan CardTimeIn = new TimeSpan(0, 0, 0);
                        if (PlayerInfo.TryGetValue("FirstName", out FirstName))
                            _POSDetail.DisplayName = FirstName;

                        if (PlayerInfo.TryGetValue("LastName", out LastName))
                            _POSDetail.DisplayName = FirstName + " " + LastName;
                           
                        if (PlayerInfo.TryGetValue("ClubState", out PlayerStatus))
                            _POSDetail.ClubState = PlayerStatus;

                        if (PlayerInfo.TryGetValue("ClubStatus", out PlayerStatus))
                            _POSDetail.ClubStatus = PlayerStatus;

                        DateTime CardedTime;
                        if (DateTime.TryParse(dr["CardinTime"].ToString(), out CardedTime))
                            CardTimeIn = DateTime.Now.Subtract(CardedTime);

                        _POSDetail.PlayerTimeOfPlay += (CardTimeIn.Hours / 60).ToString() + " : " + CardTimeIn.Minutes.ToString() + " : " + CardTimeIn.Seconds.ToString();

                    }
                }
                else
                    _POSDetail.IsEPIAvailable = false;

                foreach (PropertyInfo info in _POSDetail.GetType().GetProperties())
                {
                    _PlayerInfo.Add(info.Name, info.GetValue(_POSDetail, null) == null ? "" : info.GetValue(_POSDetail, null).ToString());
                }
            }
            return _PlayerInfo;
        }
        #endregion
    }
}
