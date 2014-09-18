using System;
using System.Collections.Generic;
using System.Data;
using BMC.Business.CashDeskOperator;
using BMC.Common.ExceptionManagement;
using BMC.Common.LogManagement;
using BMC.Transport.CashDeskOperatorEntity;
using BMC.Transport;
using BMC.Security.Interfaces;

namespace BMC.CashDeskOperator.BusinessObjects
{

    public class oCommonUtilities
    {

        private static oCommonUtilities _oCommonUtilities;

        private oCommonUtilities() { }

        public static oCommonUtilities CreateInstance()
        {
            if (_oCommonUtilities == null)
                _oCommonUtilities = new oCommonUtilities();

            return _oCommonUtilities;
        }
        public DataTable GetUserRoles(string UserName, string Password)
        {
            return (new CommonUtilities()).GetUserRoles(UserName, Password);
        }

        public DataSet GetSiteDetails()
        {
            return CommonUtilities.SiteInformation;
        }

        
        
        //public bool SetRegistry(Dictionary<string, string> Registry, string path)
        //{
        //    return (new CommonUtilities()).SetRegistryEntries(Registry, path);
        //}
        public DataTable GetInstallationList()
        {
            return (new CommonUtilities()).GetInstallationList();
        }

        public void PrintCommonReceipt(bool isVoidedTransaction, string Type, string Treasury)
        {
            PrintCommonReceipt(isVoidedTransaction, Type, Treasury, Security.SecurityHelper.CurrentUser);
        }

        public void PrintCommonReceipt(bool isVoidedTransaction, string Type, string Treasury, IUser autherizedUser)
        {
            CommonUtilities CommonUtilitiesObject = new CommonUtilities();
            CommonUtilitiesObject.currentUser = autherizedUser;
            try
            { 
                switch (Type.ToUpper())
                {
                    case "PROG":
                        Type = "Progressive";
                        break;
                    case "REFILL":
                        Type = "Refills";
                        break;
                    case "REFUND":
                        Type = "Refunds";
                        break;
                    default:
                        break;
                }
                BMC.Common.LogManagement.LogManager.WriteLog("Called PrintCommonReceipt type:treasury" + Type.ToString() + " : " + Treasury.ToString()
                , BMC.Common.LogManagement.LogManager.enumLogLevel.Debug);
                CommonUtilitiesObject.GetCommonValues(isVoidedTransaction, Type, Treasury);

            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }

        }

        public void PrintCommonReceipt(Voucher voucher)
        {
            CommonUtilities CommonUtilitiesObject = new CommonUtilities();

            try
            {
                CommonUtilitiesObject.GetCommonValues(voucher);
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
        }

        public void PrintCommonReceipt(OfflineTicket OfflineTicket, int treasuryNo)
        {
            CommonUtilities CommonUtilitiesObject = new CommonUtilities();

            try
            {
                CommonUtilitiesObject.GetCommonValues(OfflineTicket, treasuryNo);
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
        }

        public DataSet GetInitialSettings()
        {
            return CommonUtilities.SettingInformation;
        }

        public DataSet GetAppSettings()
        {
            return CommonUtilities.AppSettingInformation;
        }
        public void UpdateAppSettingSortOrder(string AppSettingKey,string AppSettingValue)
        {
             CommonUtilities.UpdateAppSettingsSortOrder(AppSettingKey,AppSettingValue);
        }
        public void UpdateSettings(string SettingName, string SettingValue)
        {
            CommonUtilities.UpdateSettings(SettingName, SettingValue);
        }

        public void UpdateTicketExpire(int value)
        {
            CommonUtilities.UpdateTicketExpire(value);
        }
        public void UpdateGMUSiteCodeStatus( int Installation_No,int Status)
        {
            CommonUtilities.UpdateGMUSiteCodeStatus(Installation_No, Status);
        }

        public List<Employeecarddata> GetEmployeeCardPollingData()
        {
           return  CommonUtilities.GetEmployeeCardPollingData();
        }

        public void UpdateEmployeecardPolling(string Employeecardno, int InstallationNo)
        {
            CommonUtilities.UpdateEmployeeCardPolling(Employeecardno, InstallationNo);
        }

        public bool SiteSummary()
        {
            throw new NotImplementedException();
        }

        public bool RedeemOfflineTicket()
        {
            throw new NotImplementedException();
        }

        public string GetCurrency(double UnitCashValue)
        {
            return CommonUtilities.GetCurrency(UnitCashValue);
        }

        public string GetConnectionString()
        {
            return CommonUtilities.ExchangeConnectionString;
        }
        public string GetTicketConnectionString()
        {
            return CommonUtilities.TicketConnectionString;
        }
        public string GetCMPConnectionString()
        {
            return CommonUtilities.CMPConnectionString;
        }
        public string GetTicketLocationCode()
        {
            return CommonUtilities.TicketLocationCode;
        }
        public bool SQLConnectionExists()
        {
            return CommonUtilities.SQLConnectionExists;
        }

        public bool UserBasedPositionExists()
        {
            return CommonUtilities.UserBasedPositionExists;
        }
    }
}



internal interface ICashDeskOperator
{
    DataTable GetUserRoles(string strUserName, string strPassword);

    bool IssueTicket(IssueTicketEntity IssueTicketEntity, Voucher voucherData);
    bool RedeemTicket(RTOnlineTicketDetail objTicketDetail);
    bool RedeemOfflineTicket();
    int ProcessHandPay(Treasury Treasury);

    int VoidTransaction(VoidTranCreate objVoid);
    DataSet FillVoidList();
    bool SiteSummary();
    DataTable FillMachines();
    bool SaveVoidExpiredTreasury(VoidOrExpiredTreasury ExpiredTreasury);
    DataTable GetTicketingExceptionTable(string strTicketNumber);
    bool UpdateTicketException(string strTicketNumber);
    //shortpay related
    Int32 SaveShortpayDetails(BMC.Transport.CashDeskOperatorEntity.Treasury objShortpay);
    int InsertException(TicketException objException);

    int UpdateVoidorExpiredTreasury(VoidOrExpiredTreasury objExpiredTreasury);
    int UpdateTicketException(Int32 iID, string strTicketNo, string strValue);
    DataTable GetFailureReasons();
    int GetMaxTreasuryID();
    DataTable GetInstallationList();
    DataSet GetSiteDetails();
    void PrintCommonReceipt(string strType);

    DataSet GetInitialSettings();
    bool IsTicketValid(Int32 iInstallationNo, string strTicketNo);
    bool SaveOfflineTicketDetails(OfflineTicket objOfflineTicketDetails);
    DataTable GetUnprocessedHandpays();

    Dictionary<string, string> PlayerInfo(string AccountNumber);
    List<PrizeInfoDTO> RetreivePrizeInfo(string AccountNumber);
    bool UpdateRedeempoints(string strAcctNumber, string PrizeID, int PrizeQty, int RedeemPoints);
    bool UpdateUnitCashPoints(string strPrizeName, int RedeemPoints, float CashValue);
    string GetCurrency(double strText);
    DataTable GetAnalysisDetails(Int32 iType, DateTime dteStartDate, DateTime dteEndDate);
    DataTable GetPositionList();
    DataTable GetWeeklyCollectionSummary();
    DataTable GetWeeklyCollectionDetails(int iWeekID);
}

// class CashDeskOperator_old : ICashDeskOperator
//{
//    Voucher voucher = new Voucher();
//    Kiosk kiosk = new Kiosk();

//    public DataTable GetAnalysisDetails(int Type, DateTime StartDate, DateTime EndDate)
//    {
//        return (new BMC.Business.CashDeskOperator.Analysis()).GetAnalysisDetails(Type, StartDate, EndDate);
//    }

//    public bool IsTicketValid(int InstallationNo, string TicketNumber)
//    {
//        return (new BMC.Business.CashDeskOperator.RedeemOfflineTicket()).IsTicketValid(InstallationNo, TicketNumber);
//    }

//    public bool SaveOfflineTicketDetails(OfflineTicket OfflineTicketEntity)
//    {
//        return (new BMC.Business.CashDeskOperator.RedeemOfflineTicket()).SaveOfflineTicketDetails(OfflineTicketEntity);
//    }

//    public DataTable GetUserRoles(string UserName, string Password)
//    {
//        return (new BMC.Business.CashDeskOperator.CommonUtilities()).GetUserRoles(UserName, Password);
//    }

//    public int InsertException(TicketException TicketExceptionEntity)
//    {
//        return (new BMC.Business.CashDeskOperator.ShortPay()).InsertException(TicketExceptionEntity);
//    }

//    #region ICashDeskOperator Members

//    #region "Issue Ticket"
//    /// <summary>
//    /// Returns the bar code number for the ticket number entered.
//    /// </summary>
//    /// <param name="ObjCDOEntity"></param>
//    /// <returns></returns>        
//    public bool IssueTicket(IssueTicketEntity IssueTicketEntity, Voucher voucherData)
//    {
//        bool HasTicketCreated = false;
//        voucher = voucherData;

//        try
//        {
//            IssueTicket IssueTicketBusinessObject = new IssueTicket();

//            if (IssueTicketBusinessObject.CreateTicket(IssueTicketEntity, ref voucher) == true)
//            {
//                HasTicketCreated = true;
//                PrintCommonReceipt("Ticket Issue");
//            }
//            else
//            {
//                HasTicketCreated = false;
//            }
//        }
//        catch (Exception ex)
//        {
//            ExceptionManager.Publish(ex);
//        }
//        return HasTicketCreated;
//    }
//    #endregion

//    #region Redeem Ticket
//    /// <summary>
//    /// 
//    /// </summary>
//    /// <param name="objTicketDetail"></param>
//    /// <returns></returns>
//    public bool RedeemTicket(RTOnlineTicketDetail TicketDetailEntity)
//    {
//        (new BMC.Business.CashDeskOperator.RedeemTicket()).CheckTicket(TicketDetailEntity);
//        return true;
//    }
//    #endregion Redeem Ticket

//    #region Handpay
//    /// <summary>
//    /// Functions used to process handpay from events and manual process.
//    /// </summary>
//    /// <returns></returns>
//    public DataTable GetUnprocessedHandpays()
//    {
//        return (new BMC.Business.CashDeskOperator.HandPay()).GetUnprocessedHandPays();
//    }

//    public DataTable GetUnprocessedHandpays(int InstallationNo)
//    {
//        return (new BMC.Business.CashDeskOperator.HandPay()).GetUnprocessedHandPays(InstallationNo);
//    }

//    public int ProcessHandPay(Treasury Treasury)
//    {
//        return (new BMC.Business.CashDeskOperator.HandPay()).ProcessManualHandpay(Treasury);
//    }

//    public DataTable FillMachines()
//    {
//        return (new BMC.Business.CashDeskOperator.HandPay()).FillMachines();
//    }

//    public bool SaveVoidExpiredTreasury(VoidOrExpiredTreasury ExpiredTreasury)
//    {
//        return (new BMC.Business.CashDeskOperator.HandPay()).SaveVoidorExpiredTreasury(ExpiredTreasury);
//    }

//    public bool UpdateTicketException(string TicketNumber)
//    {
//        return (new BMC.Business.CashDeskOperator.HandPay()).UpdateFinalStatusTicketException(TicketNumber);
//    }

//    public bool ClearHandpayLock(int InstallationNumber)
//    {
//        return (new BMC.Business.CashDeskOperator.HandPay()).ClearHandpayLock(InstallationNumber);
//    }

//    public DataTable GetTicketingExceptionTable(string TicketNumber)
//    {
//        return (new BMC.Business.CashDeskOperator.HandPay()).GetTicketExceptions(TicketNumber);
//    }

//    #endregion

//    #region Shortpay

//    public int SaveShortpayDetails(BMC.Transport.CashDeskOperatorEntity.Treasury TreasuryEntity)
//    {
//        return (new BMC.Business.CashDeskOperator.ShortPay()).SaveShortpayDetails(TreasuryEntity);

//    }
//    public DataTable GetFailureReasons()
//    {
//        return (new BMC.Business.CashDeskOperator.ShortPay()).GetFailureReasons();
//    }

//    public int UpdateVoidorExpiredTreasury(VoidOrExpiredTreasury ExpiredTreasuryEntity)
//    {
//        return (new BMC.Business.CashDeskOperator.ShortPay()).UpdateVoidorExpiredTreasury(ExpiredTreasuryEntity);
//    }

//    public int UpdateTicketException(int ID, string TicketNumber, string Value)
//    {
//        return (new BMC.Business.CashDeskOperator.ShortPay()).UpdateTicketException(ID, TicketNumber, Value);
//    }

//    public int GetMaxTreasuryID()
//    {
//        return (new BMC.Business.CashDeskOperator.ShortPay()).GetMaxTreasuryID();
//    }

//    public DataTable GetInstallationList()
//    {
//        return (new BMC.Business.CashDeskOperator.CommonUtilities()).GetInstallationList();
//    }


//    #endregion

//    public int VoidTransaction(VoidTranCreate VoidTransactionEntity)
//    {
//        return (new BMC.Business.CashDeskOperator.VoidTransaction()).VoidCreate(VoidTransactionEntity);
//    }

//    public DataSet FillVoidList()
//    {
//        return (new BMC.Business.CashDeskOperator.VoidTransaction()).FillVoidList();
//    }

//    public DataSet GetSiteDetails()
//    {
//        return BMC.Business.CashDeskOperator.CommonUtilities.SiteInformation;
//    }

//    public void PrintCommonReceipt(string Type)
//    {
//        BMC.Business.CashDeskOperator.CommonUtilities CommonUtilitiesObject = new CommonUtilities();
//        Site SiteEntity = new Site();
//        DataSet dsSite;

//        if (Type.Contains("Prog"))
//        {
//            Type = "Progressive";
//        }
//        try
//        {
//            dsSite = GetSiteDetails();

//            if (dsSite != null)
//            {
//                SiteEntity.SiteCode = dsSite.Tables[0].Rows[0]["Code"].ToString();
//                SiteEntity.SiteName = dsSite.Tables[0].Rows[0]["Name"].ToString();
//            }

//            switch (Type)
//            {
//                case "Handpay Jackpot":
//                    Type = "Handpay Jackpot";
//                    break;
//                case "Refill":
//                    Type = "Refills";
//                    break;
//                case "Refund":
//                    Type = "Refunds";
//                    break;
//                default:
//                    break;
//            }

//            //CommonUtilitiesObject.GetCommonValues(SiteEntity, Type, voucher);
//            CommonUtilitiesObject.GetCommonValues(Type);

//        }
//        catch (Exception Ex)
//        {
//            ExceptionManager.Publish(Ex);
//        }

//    }

//    public DataSet GetInitialSettings()
//    {
//        return BMC.Business.CashDeskOperator.CommonUtilities.SettingInformation;
//    }

//    public bool SiteSummary()
//    {
//        throw new NotImplementedException();
//    }

//    public bool RedeemOfflineTicket()
//    {
//        throw new NotImplementedException();
//    }

//    #endregion


//    #region PlayerInfo
//    /// <summary>
//    /// Retrieve Player Information.
//    /// </summary>
//    ///  <param name="AccountNumber"></param>
//    /// <returns >Dictionary<string,string></returns>       
//    public Dictionary<string, string> PlayerInfo(string AccountNumber)
//    {
//        return kiosk.GetPlayerInfo(AccountNumber);
//    }

//    /// <summary>
//    /// Retrieve Prize Details.
//    /// </summary>
//    /// <param name="AccountNumber"></param>
//    /// <returns >List<PrizeInfo></returns>        
//    public List<PrizeInfoDTO> RetreivePrizeInfo(string AccountNumber)
//    {
//        return kiosk.GetPrizeInfo(AccountNumber);
//    }

//    /// <summary>
//    /// Update the current balance with points redeemed.
//    /// </summary>
//    /// <param name="PrizeID"></param>
//    /// <param name="PrizeQty"></param>
//    /// <param name="strAcctNumber"></param>
//    /// <returns >success or failure</returns>       
//    public bool UpdateRedeempoints(string strAcctNumber, string PrizeID, int PrizeQty, int RedeemPoints)
//    {
//        return kiosk.UpdateRedeempoints(strAcctNumber, PrizeID, PrizeQty, RedeemPoints);
//    }

//    public bool CheckAccountNumber(string strAccountNumber)
//    {
//        return kiosk.CheckAccountNumber(strAccountNumber);

//    }

//    /// <summary>
//    /// Update the points which can be redeeemed and the cash value.
//    /// </summary>
//    /// <param name=""></param>
//    /// <returns >success or failure</returns>       
//    public bool UpdateUnitCashPoints(string strPrizeName, int RedeemPoints, float CashValue)
//    {
//        return kiosk.UpdateUnitCashPoints(strPrizeName, RedeemPoints, CashValue);
//    }

//    public string GetCurrency(double strText)
//    {
//        return BMC.Business.CashDeskOperator.CommonUtilities.GetCurrency(strText);
//    }

//    public bool CheckForPrefixSuffixSetting()
//    {
//        return kiosk.CheckForPrefixSuffixSetting();
//    }

//    /// <summary>
//    /// To check whether Enable Redeem Receipt printer is enabled.
//    /// </summary>
//    /// <param name=""></param>
//    /// <returns >true or false</returns>        
//    public bool CheckEnableRedeemPrintCDO()
//    {
//        return kiosk.CheckEnableRedeemPrintCDO();
//    }

//    #endregion

//    #region Field Service
//    /// <summary>
//    /// Returns the datatable with the list of Current Calls.
//    /// </summary>
//    /// <returns>Datatable</returns>        
//    public DataTable GetCurrentServiceCalls(string SiteCode, string StartingBarPos, string LastBarPos)
//    {
//        return (new BMC.Business.CashDeskOperator.FieldService()).GetCurrentServiceCalls(SiteCode, StartingBarPos, LastBarPos);
//    }

//    /// <summary>
//    /// Returns the site code.
//    /// </summary>
//    /// <returns>String</returns>
//    [Obsolete]
//    public string GetCurrentSiteCode()
//    {
//        return (new BMC.Business.CashDeskOperator.FieldService()).GetCurrentSiteCode();
//    }

//    /// <summary>
//    /// Returns the list of Bar Position Names for the site.
//    /// </summary>
//    /// <returns>String</returns>
//    [Obsolete]
//    public string GetCurrentBarPositionNames()
//    {
//        return (new BMC.Business.CashDeskOperator.FieldService()).GetCurrentBarPositionNames();
//    }

//    public DataTable GetPositionList()
//    {
//        return (new BMC.Business.CashDeskOperator.FieldService()).GetPositionList();
//    }

//    #endregion

//    public DataTable GetWeeklyCollectionSummary()
//    {
//        return (new BMC.Business.CashDeskOperator.Analysis()).GetWeekCollectionSummary();
//    }
//    public DataTable GetWeeklyCollectionDetails(int iWeekID)
//    {
//        return (new BMC.Business.CashDeskOperator.Analysis()).GetWeekCollectionDetails(iWeekID);
//    }
//}

