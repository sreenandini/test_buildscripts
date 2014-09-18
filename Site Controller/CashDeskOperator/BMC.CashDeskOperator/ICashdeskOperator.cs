using System;
using System.Collections.Generic;
using System.Data;
using BMC.Business.CashDeskOperator;
using BMC.Common.ExceptionManagement;
using BMC.Common.LogManagement;
using BMC.Transport.CashDeskOperatorEntity;
using BMC.Transport;
using Microsoft.Win32;
using System.Windows.Controls;
using BMC.CommonLiquidation.Utilities;

namespace BMC.CashDeskOperator.BusinessObjects
{
    public interface IAnalysis
    {
        DataTable GetAnalysisDetails(int Type, DateTime StartDate, DateTime EndDate);
        DataTable GetAnalysisDetails(int Type, DateTime StartDate, DateTime EndDate, AnalysisView viewType, int zoneId);
        DataTable GetWeeklyCollectionSummary();
        DataTable GetWeeklyCollectionDetails(int WeekID);
        DataTable GetWTDMTD();
        DataTable GetBarPositionDetails(string SortBy, int InstallNo);
        void SaveFloorPosition(int slotID, int securityUserID, int topPosition, int leftPosition);
        DataTable GetSpotCheckDataSAS(int InstallationNo, int SelectDay, DateTime date);
        DataTable GetTicketsClaimedByCDForPeriod(int SelectDay, DateTime IsDate, int MachineNo);
        DataTable GetInstallations();
        DataTable GetEPIDetails(int InstallationNo);
        DataTable GetTicketException(int InstallationNo);
        DataTable GetHandpaystoClear(int InstallationNo);
        void PrintFunction(System.Windows.Controls.ListView lvView, string ReportName, DateTime ReportStartdate);
        DataTable GetInstallationsForReports(string reportType);
    }
    
    public interface IEnrollment 
    {
        DataTable GetAssetDetails(string AssetNo, string TransitSiteCode);
        PositionDetails GetPositionDetails(string Position);
        EnrollmentErrorCodes RemoveMachine(int InstallationNo, int MachineStatusFlag, string SiteCode,int nDisMachine);
        EnrollmentErrorCodes InstallMachine(PositionDetails PosDetails,int userid,out int installationNo);
        bool SetMachineMaintenanceState(int installationNo);
        bool SetMachinePreviousState(int installationNo);
        IEnumerable<PositionCurrentStatusResult> GetPositionCurrentStatus(bool allPosition, bool vLTAAMS, bool vLTVerification, bool gameAAMS, bool gameVerification, bool gameEnableAAMS, bool BADAAMSEnableDisable, bool BMCEnterpriseStatus);
        int GetInstallationNo(string strSerialNo);
        int ExecuteHourlyVTP(int InstallationNumber, DateTime dtTheDate, int iTheHour, bool isRead);
        bool UpdateHourlyStatsGamingday(string installationNo);
        DataTable GetActiveSiteDetails();
        DataTable GetInTransitAsset();
        void InsertIntoExportHistory(int InstallationNumber);
    }
    
    public interface IFieldService
    {        
        string GetCurrentSiteCode();
        string GetCurrentBarPositionNames();
        string LogSiteEvent(int InstallationNumber, int FaultType);

        DataTable GetCurrentServiceCalls(string SiteCode, string StartingBarPosition, string LastBarPosition);
        DataTable GetPositionList();
        DataTable GetCashdeskServiceFaults();
        DataTable GetOpenServiceCalls(string site_Code, string bar_Pos);
        DataTable GetServiceNotes(string JobID);
        DataTable GetRemedies();
        int EscalateServiceCall(string JobID, int UserID);
        int InsertServiceNotes(string JobID, string Notes, string User);
        int CloseServiceCall(int ServiceID, string JobID, int Remedy, int User, string Notes);
    }
    
    public interface IHandpay
    {
        int ProcessHandPay(Treasury Treasury);
        int ProcessHandPay(Treasury Treasury, int TE_ID);
        int VoidTransaction(VoidTranCreate objVoid);
        int ProcessHandPayOnDenomChange(string sBarpos, int userID);
        bool SaveVoidExpiredTreasury(VoidOrExpiredTreasury ExpiredTreasury);
        bool UpdateTicketException(string strTicketNumber);        
     
        //DataTable FillMachines();
        DataTable GetUnprocessedHandpays();
        DataTable GetUnprocessedHandpays(int InstallationNo);
        DataTable GetTicketingExceptionTable(string strTicketNumber);
        IEnumerable<FillTreasuryList> GetHandpays(string BarPos);
        List<AssetNumberResult> GetAssetNumber(int installation_No);
        string GetEPIDetails(int installation_No);
        jackpotProcessInfoDTO getJackpotStatusAmount(string JackpotSlipNumber, int Site);
        jackpotProcessInfoDTO payJackpot(string SequenceNumber, int SiteId, string userId, string firstName, string lastName, string cashDeskLocation);
        void PrintSlip(jackpotProcessInfoDTO jpinfo);
        DataSet createTickeException_HandpayCAGE(int Installation_No, double TicketValue, int isHandpayResponse, string HP_Type);
        int Clearhandpay(int InstallationNo);
        List<BarPositions> GetBarPositions();
        
        DateTime? GetTreasuryDateTime(int Treasury_ID);
        bool CheckIfHandpayProcessed(FillTreasuryList fillTreasuryList);
       
        bool RollbackHandPay(int Ticket_ExceptionID, int Treasury_No);
        bool ExportHandPay(int Treasury_No);
        List<DenomValueResult> GetDenomValue(string Stock_No);
        int GetUserID(int? SecurityUserID);
    }
    
    public interface IIssueTicket
    {
        PrintTicketErrorCodes IssueTicket(IssueTicketEntity IssueTicketEntity);
        string GetPrinterInformation();
        voucherDTO IssueTicketToCage(IssueTicketEntity IssueTicketEntity, voucherDTO ovoucherDTO);
        voucherDTO[] searchVouchers(String partialBarcode, int siteId, long amount, int maxCount);
        voucherDTO redeemRequestVoucher(voucherDTO request);
        voucherDTO[] SearchTicketForCage(String partialBarcode);
        voucherDTO[] SearchTicketForCage(String partialBarcode, string strClientSiteCode);
        string CancelTicketCage(string strBarcode);
        bool ValidateUserCage(string UserName, string Password);
    }
    
    public interface IPlayerInformation
    {
        bool UpdateRedeempoints(string AccountNumber, string PrizeID, int PrizeQty, int RedeemPoints, LoginInfoDTO loginInfo, PlayerInfoDTO playerInfo);
        bool CheckAccountNumber(string AccountNumber);
        bool UpdateUnitCashPoints(string PrizeName, int RedeemPoints, float CashValue);
        bool CheckForPrefixSuffixSetting();
        bool CheckEnableRedeemPrintCDO();

        Dictionary<string, string> GetPlayerInfo(string AccountNumber);
        List<PrizeInfoDTO> RetreivePrizeInfo(string AccountNumber);
        string[] GetCardInformation(string cardNumber);
        LoginInfoDTO GetLoginInformation();
        PlayerInfoDTO GetPlayerInformation(string strAccountNumber);
    }
    
    public interface IRedeemOnlineTicket
    {
        RTOnlineTicketDetail RedeemOnlineTicket(RTOnlineTicketDetail TicketDetail);
        bool RedeemOnlineTicketCage(RTOnlineTicketDetail TicketDetail);
        bool CheckLaunderingEnabled();
        int CheckSDGTicket(string TicketString);
        int CheckSDGTicketCage(string TicketString);
        RTOnlineTicketDetail GetRedeemTicketAmount(RTOnlineTicketDetail TicketDetail);
        RTOnlineTicketDetail GetMultiRedeemTicketAmount(RTOnlineTicketDetail TicketDetail);
        RTOnlineTicketDetail GetRedeemTicketAmountCage(RTOnlineTicketDetail TicketDetail);
        int CheckSDGOfflineTicket(string TicketString);
        string GetTicketPrintDevice(string strBarcode, out DateTime PrintDate);
        
        bool ValidateClientSiteCode(string sClientSiteCode);
        string GetVoucherDetailsToExport(int iVoucherID);
        string GetVoucherDetailsForCrossTicketing(string Barcode);
        void CreatePayDeviceID(string stockNo);
        void UpdateLiabilityStatus(string Barcode, string SiteCode, string Status);
        bool ImportVoucherDetails(RTOnlineTicketDetail TicketDetail);
        bool RollbackRedeemTicket(string TicketString);
        ReedeemTicketDetailsComms RedeemTicketStartComms(ReedeemTicketDetailsComms TicketDetailComms);
        ReedeemTicketDetailsComms RedeemTicketCompleteComms(ReedeemTicketDetailsComms TicketDetailComms);
        ReedeemTicketDetailsComms RedeemTicketCancelComms(ReedeemTicketDetailsComms TicketDetailComms);
        RTOnlineTicketDetail GetVoucherAmountAndStatusForMultipleTicket(RTOnlineTicketDetail TicketDetailEntity);

    }
    
    public interface IRedeemOfflineTicket
    {
        bool IsTicketValid(int InstallationNo, string TicketNumber, int Amount);
        bool SaveOfflineTicketDetails(OfflineTicket OfflineTicketEntity, out int treasuryNo);
    }
    
    public interface IShortPay
    {
        int SaveShortpayDetails(Treasury TreasuryEntity);
        int InsertException(TicketException objException);
        int UpdateVoidorExpiredTreasury(VoidOrExpiredTreasury VoidExpiredTreasuryEntity);
        int UpdateTicketException(int ID, string TicketNumber, string Value);
        int GetMaxTreasuryID();
        int SaveReasonDetails(ReasonCode objReasonCode);
        int DeleteReasonDetails(ReasonCode objReasonCode);
        bool CreateShortPayForApproval(Treasury TreasuryEntity, ref int iShortPayID);
        void CancelShortPayForApproval(int iShortPayID);

        DataTable GetFailureReasons();

        void ApproveShortPay(string strIDs, int UserID, int TreasuryID);
    }
    
    public interface IVoidTransaction
    {
        int VoidTransaction(VoidTranCreate objVoid);

        DataSet FillVoidList();
    }

    public interface IMeterLife
    {
        DataSet GetMeterLife(int InstallationNumber);
        void GetCurrentMeters(int InstallationNo);
        void GetCurrentMeters(IList<int> InstallationNo, Action<int, int, int> Act);
        void PrintCurrentMeters(ListView lvView,int InstallationNum);
        void GetAssetDetails(int iInstallationNo, ref string SiteCode, ref string AssetNumber, ref string PosNumber);
        DataSet GetCurrentDayMeters(int InstallationNumber);
    }

    public interface IHourly
    {
        DataTable GetInstallationDetails();
        DataTable GetMachineTypeDetails();
        DataTable GetSiteName();
        DataTable GetZones();
        DataTable GetPositions();
        DataTable GetHSTypes();
        HourlyDetailsEntity GetHourlyStatistics(int startHour, int rows, string dataType, int? category,
             int? zone, int? position, DateTime? date,  bool isCalenderDay);
    }

    public interface IDrop
    {
        DataTable GetCurrentMachines();
        DataTable GetMeterList(string display, int record_No, int hour_No);
        bool GetSGVISetting();
    }

    public interface IEventDetails
    {
        DataTable GetEventDetails(DateTime startDate, DateTime endDatetime, string strBarPos, int showCleared, string strEventType, int iPageSize, int LegalEvent);
        string FillEventType();
        bool UpdateEventDetails(string clearType, string eventType, int eventNo, int installationNo);
        bool CheckForUnclearedEvents();
    }

    public interface IFactoryReset
    {
        int CheckAuthorizationCode(string iAuthoCode);
        Dictionary<string, string> GetKeys(string sectionname);
         //Dictionary<string, string> GetRegistryEntries(string Registrypath);         
        // bool SetRegistryEntries(Dictionary<string, string> dictSetregistry, string strPath);
         Dictionary<string, string> RetrieveServerDetails(string ConnectionString);
         string MakeConnectionString(Dictionary<string, string> Credentials);
         bool GetServerDetails(string ConnectionString);
         bool TestConnectionDB(Dictionary<string, string> Credentials);
         bool TestConnectionDB(string Connectionstring);
         string GetServiceStatus(string strserviceName);
         bool EndService(string strserviceName);         
         int CreateSqlDatabaseBackUp(FactoryReset objFactoryReset);
         int CreateDBZip(FactoryReset objFactoryReset);
         string GetSettingValue(string settingname);
    }

    public interface ISiteRecovery
    {       
        bool DatabaseIsEmpty();
        int EnterpriseUrlIsExists(string sServername,string sitecode);
        int ImportSiteDetails(int iSiteCode);
        int ImportInstallations(int iSiteCode);
        bool ImportMachines(int iSiteCode);
        int ImportBarPositions(int iSiteCode);
        int ImportZones(int iSiteCode);
        int ImportLatestMeterHistory(int iSiteCode);        
        bool ReseedCollectionBatch(int iBatchId);
        bool UpdateCheckPoints(int iSiteCode, int Value, string sTableName);
        int UpdateSiteStatus(int iSiteCode, string sUpdate);
        DataTable GetTableDetails();
        int CheckAuthorizationCode(string iAuthoCode, int iSiteCode,string TransactionType);
        Dictionary<int, string> GetCheckPointsStatus(byte iStatus);
        int ImportCollections(int iSiteCode);
        bool ImportTickets(int iSiteCode);
        int ImportSystemSettings(int iSiteCode);
        int ImportDailyRead(int iSiteCode);
        int ImportHourly(int iSiteCode);
        int ImportAllEvents(int iSiteCode);
        int ImportCashDeskTransactions(int iSiteCode);
        int ImportUserDetails(int iSiteCode);
        int ImportUserRoles(int iSiteCode);
        bool UpdateAllCheckPoints(int Value);
        int ImportCalendars(int iSiteCode);
        int ImportAAMSDetails(int iSiteCode);
        int ImportInstallationGameInfo(int iSiteCode);
        string GetSettingValue(string settingname);
        string GetServiceStatus(string strserviceName);
        bool EndService(string strserviceName);
        bool StartService(string strserviceName);
        int ImportComponentDetails(int iSiteCode);
        int ImportOtherGameDetails(int iSiteCode);
        bool IsValidSiteCode(int iSiteCode);
    }

    public interface IPlayerClub
    {
        Dictionary<string, string> RetrievePlayerInfoFromEPI(int InstallationNo);
    }

    public interface ICashDeskManager
    {
        List<string> FillListOfFilteredPositions(string RouteNumber);
        List<TicketExceptions> TITOTicketInExceptions(Tickets oTickets, List<string> lstPositions);
        Dictionary<string, string> FillRouteFilter();
        List<TicketExceptions> TitoTicketsAll(Tickets oTickets, List<string> lstPositions);
        List<TicketExceptions> TitoTicketOutExceptions(Tickets oTickets, List<string> lstPositionstoDisplay);
        List<TicketExceptions> TitoTicketsClaimed(Tickets oTickets, List<string> lstPositionstoDisplay);
        List<TicketExceptions> TitoTicketsClaimedLiability(Tickets oTickets, List<string> lstPositionstoDisplay);
        List<TicketExceptions> TicketsClaimed(TicketsClaimed oTickets, List<string> lstPositionstoDisplay);
        List<TicketExceptions> TicketsPrinted(TicketsClaimed oTickets, List<string> lstPositionstoDisplay);
        List<TicketExceptions> TitoTicketsPrinted(Tickets oTickets, List<string> lstPositionstoDisplay);
        List<TicketExceptions> TitoTicketsPrintedLiability(Tickets oTickets, List<string> lstPositionstoDisplay);
        List<TicketExceptions> GetTicket_VoidnExpired(Tickets oTickets, List<string> lstPositions);
        List<TicketExceptions> TitoTicketsUnclaimed(Tickets oTickets, List<string> lstPositions);
        List<TicketExceptions> TicketsUnclaimed(TicketsClaimed oTickets, List<string> lstPositions);
        List<TicketExceptions> GetPromoCashableTickets(TicketsClaimed oTickets, List<string> lstPositions);
        List<TicketExceptions> GetTicketAnomalies(TicketsClaimed oTickets, List<string> lstPositions);
        List<TicketExceptions> GetTreasuryItems(Tickets oTickets, List<string> lstPositions);
        string GetRegionFromSite();
        bool isValidDateRange(DateTime dStartdate, DateTime dEndDate);
        bool ExportToExcel(ListView lvView, string path);
       bool HourlyExportToExcel(ListView lvView, string path, bool HourlyExportToExcel);
       bool HourlyExportToExcel(Microsoft.Windows.Controls.DataGrid lvView, string path, bool HourlyExportToExcel);
        void CloseExcel();
        void PrintFunction(ListView lvView, DateTime StartDate, DateTime EndDate);
        void PrintFunction(ListView lvView, DateTime StartDate, DateTime EndDate, bool isPrintDate, bool isTransactionType, bool isZone,
            bool  isPos,bool  isMachine,bool  isAsset,bool  isAmount,bool  isTicketPrintedDate,bool  isDetails,string screenName);
        bool ShowHopper();
        bool IsRegulatoryEnabled();
        bool ClearTicketStatus(string Ticket, string DeviceID);
        Dictionary<string, bool> ActivateSDGTicket(string Ticket, string DeviceID, bool iStatus);
        List<User> GetListOfUsers(int UserNo);
        List<User> GetListOfUsersRoles(int UserNo);
        List<RouteCollection> GetRouteCollection(); 




       
    }

    public interface ISettingsDetails
    {
        DataSet GetSettingDetails();
        string FillSettingsToBeSkipped();
    }

    public interface IAFTSettingsDetails
    {
        List<BMC.Transport.AFTSetting> GetAFTSettings(int iDenom);
        bool SaveAFTSettings(List<Transport.AFTSetting> lstSettings);
        DataTable GetDenoms();
    }


    public interface IBatch
    { 
          List<CollectionListView> GetCollectionDetailsforListView(CollectionView collection);
          List<TreasuryUser> GetTreasuryTable(CollectionView collection);
          List<rsp_AssetVarianceHistoryResult> GetAssetVarianceHistory(int InstallationNo, int RecordCount);
    }
    public interface ICustomerDetails
    {
        int InsertCustomer(Customer oCustomerDetailsEntity);
        List<SearchCustomerDetailsResult> SearchCustomer(Customer oCustomerDetailsEntity);
        long RecentCustomerID();
    }
    public interface IAuditDetails
    {
        IEnumerable<FillModules> GetModulesList();
        List<GetAuditDetailsResult> GetAuditDetails(DateTime fromDate, DateTime ToDate, string ModuleID,int Rows);
    }
	
	public interface IReports
    {   
        DataSet GetJackpotSlipSummaryDetails(DateTime reportStartDateTime, DateTime reportEndDateTime,
            bool? ShowHandpay,bool? ShowJackpot);
        DataSet GetVoucherCouponLiabilityReport(DateTime issueDate,string sDeviceType, string sVoucherStatus);        
        DataSet GetRedeemedTicketByDevice(DateTime fromDate, DateTime toDate, string DeviceType);
        DataSet GetExpiredVoucherCouponReport(DateTime startDate, DateTime endDate, string sDeviceType);
        DataSet GetExpenseDetails(DateTime reportDate, string reportPeriod,bool IsGamingDayBasedReport);
        DataSet GetVoucherListingReport(DateTime startDate, DateTime endDate, string Status, string Slot);
        DataSet GetAuditTrailReport(DateTime fromDate, DateTime toDate, string sModuleName);
        DataSet GetMeterDetails(int installationNo);
        DataTable GetAssetNumberforActiveInstallations();
        DataTable GetBatchNumber(DateTime StartDate, DateTime EndDate, bool isdeclared);
        DataSet GetCashDeskReconcilationDetails(DateTime StartDate, DateTime EndDate, int UserNo, int iRoute_No);
        //GetCashierTransactions
        DataSet GetCashierTransactions(DateTime StartDate, DateTime EndDate, int UserNo, int iRoute_No);
        DataSet GetCashDeskMovementDetails(DateTime StartDate, DateTime EndDate, int UserNo, int iRoute_No);
        DataSet GetSystemBalancingDetails(DateTime StartDate, DateTime EndDate, int UserNo, int iRoute_No);
        DataSet GetLiquidationDetails(int BatchNo);
        int CheckLiquidationPerformed(int BatchID, ref int? LiquidationPerformed);
		DataSet GetExceptionSummary(int BatchNo);
        DataSet GetBatchWinLoss(int BatchNo,int WeekNo);
        DataSet GetTicketAnomalies(DateTime StartDate, DateTime EndDate);
        DataSet GetMachineDrop(int BatchNo, int WeekNo);
        void GetVersion_SiteName(out string sVersion, out string sSiteName);
        void GetVersion_SiteName(out string sVersion, out string sSiteName,out string SiteCode);
        void GetSlots(System.Windows.Controls.ComboBox combo);
        DataSet GetAFTAuditTrailReport(DateTime dtFromDate, DateTime dtToDate);
        List<ServerDetails> GetDataBaseConnectionString();
        List<ServerDetails> GetDataBaseConnectionString(string ExchangeConnectionString);
        DataSet GetLiquidationSummaryDetails(int _BatchID);
        DataSet GetExceptionVoucherDetails(DateTime reportStartDateTime, DateTime reportEndDateTime, bool? IsDrop, Int32 BatchNumber);
        DataSet GetCrossPropertyTicketAnalysisReport(DateTime StartDate, DateTime EndDate);
        DataSet GetAccountingWinLossReport(int ZoneNo, int MachineCategoryNo, DateTime StartDate, DateTime EndDate, bool IncludeNonCashable);
        DataSet GetCrossPropertyLiabilityTransferDetailsReport(DateTime StartDate, DateTime EndDate);

        DataSet GetCrossPropertyLiabilityTransferSummaryReport(DateTime StartDate, DateTime EndDate);
        DataSet GetStackerDetails(int StackerLevel);
        DataSet GetPartCollectionDetails(int NoofRecords);

        List<ReadLiquidationReportRecords> GetReadLiquidationReportRecords(bool bOnlyLast20Records);
        List<LiquidationDetailForReport> GetLiquidationDetailForReport(int? iBatchId, int? iReadId);
        DataSet GetPromotionalTicketHistory();
        DataSet GetTISPromotionalDetails(DateTime StartDate, DateTime EndDate, int NoOfRecordsInPage);
 bool IsResetOccuredAndCompleted();
    }

    public interface ILiquidationDetails
    {
         List<LiquidationSummary> GetLiquidationSummaryDetails(int BatchNo);
         void UpdateBatchAdvance(int BatchNo, decimal AdvanceRetailer);
         void CalculateRetailerNegative(int BatchNo);
         string GetSetting(string SettingName);
    }
	
	public interface ISiteLicensing
    {
        int CheckSiteLicenseKey(string sLicenseKey, string sSiteCode, string sUserName);
        void UpdateLicenseStaus(string sLicenseKey, int iLicenseKeyStatus, int iUserID);
        void GetSiteLicenseList(); 
        SiteLicenseDetailsEntity ActiveLicense { get; }
        SiteLicenseDetailsEntity ExpiredLicense { get; }
    }

    public interface IReadBasedLiquidation
    {
        List<ReadLiquidationEntity> GetReadLiquidationRecords();
        List<CommonLiquidationEntity> GetReadLiquidation(DateTime minDate, DateTime maxDate);
    }

    public interface IReadLiquidationDetails
    {
        List<ReadLiquidationDetails> GetReadLiquidationDetails(DateTime minDate, DateTime maxDate);
    }

    public interface IReadLiquidation
    {
        decimal CalculateRetailerNegativeNet (decimal _profitSharePercentage);
        int SaveLiquidation(CommonLiquidationEntity objCommonLiquidationEntity);
    }

    public interface IProfitShare
    {
        List<ProfitShareGroup> GetProfitShareGroupList();
        List<ExpenseShareGroup> GetExpenseShareGroupList();
        List<PayPeriods> GetPayPeriods();
    }

    public interface ISpotCheck
    {
        List<Installations> GetInstallationDetails();
        List<SpotCheck> GetSpotCheckSummaryDetails(int iInstallation_No, int? iPop);
    }

    public interface ISessionGamePlayDetails
    {
        IEnumerable<SessionGamePlayDetails> GetSessionGamePlayDetails();
        IEnumerable<ActiveSessionInstallations> GetInstallationForActiveSession();
        void GetCurrentMeters(int InstallationNo);
    }
}
