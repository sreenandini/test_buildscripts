using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.Transport
{
    public static class Settings
    {
        static Settings()
        {
            TISTicketPrefixDigit = '7';
            TISTicketPrefix = '7';//added for Multiple ticket voucher TIS ticket handling
        }

        private static bool _EnableVoucher, _EnableHandpayReceipt, _EnableIssueReceipt,
            _EnableShortpayReceipt, _EnableRefillReceipt, _EnableRefundReceipt,
            _EnableProgReceipt, _Not_Issue_Ticket, _TITO_Not_In_Use, _OnScreenKeyboard, _TV_FillScreen,
            _VoidTransaction, _HandpayManual, _EnableLaundering, _CD_NOT_USE_HOPPERS,
            _Allow_Offline_Redeem, _ReceiptPrinter, _SGVIEnabled, _RedeemConfirm, _RegulatoryEnabled,
            _MaxHandPayAuthRequired, _ManualEntryTicketValidation, _RedeemExpiredTicket, _IsAFTEnabledForSite,
            _MachineMaintenance, _EnableCustomerReceipt, _CentralizedDeclaration, _DeclarationAlert, _DropAlert,
            _ExpenseShare, _WriteOffShare, _LiquidationProfitShare, _CentralizedReadLiquidation, _IsPartCollectionEnabled, _Allow_Machine_Removal_WithoutDeclaration, _DropSummaryReport, _TicketValidate_EnableIssueReceipt, _ProcessW2GAmount, _AddShortpayInVoucherOut,
            _IsPromotionalTicketEnabled, _IsTISEnabled, _AllowManualKeyboard;

        private static string _HeadCashierSig, _TicketDeclaration, _ManagerSig, _Connection, _Region, _SiteName, _SiteCode, _PrinterPort, _WebURL, _issueticketmaxvalue, _RegulatoryType, _Client, _SiteFloorViewState, _LiquidationType, _MaximumPromotionalTicketsCount
                                   , _MaximumPromotionalTicketAmount
                                   , _DefaultPromotionalTicketExpireDays;

        private static int _Gaming_Day_Start_Hour, _Ticket_Expire;

        public static bool EnableCustomerReceipt
        {
            get { return _EnableCustomerReceipt; }
            set { _EnableCustomerReceipt = value; }
        }
        public static bool AllowManualKeyboard
        {
            get { return _AllowManualKeyboard; }
            set { _AllowManualKeyboard = value; }
        }
        public static bool IsPromotionalTicketEnabled
        {
            get { return _IsPromotionalTicketEnabled; }
            set { _IsPromotionalTicketEnabled = value; }
        }
        public static bool IsTISEnabled
        {
            get { return _IsTISEnabled; }
            set { _IsTISEnabled = value; }
        }
        public static bool IsGloryCDEnabled
        {
            get;
            set;
        }
        public static bool IsMachineBasedAutoDrop
        {
            get;
            set;
        }
        public static string Site_Address_1
        {
            get;
            set;
        }
        public static string Site_Address_2
        {
            get;
            set;
        }

        public static string PrintHeaderFormat
        {
            get;
            set;
        }

        public static bool StackerFeature
        {
            get;
            set;
        }

        public static DateTime? dtCashierTransStartTime
        {
            get;
            set;
        }

        public static bool EnableMonthToDateTab
        {
            get;
            set;
        }

        public static bool EnableCashdeskReconciliation
        {
            get;
            set;
        }

        public static bool EnableCashdeskMovement
        {
            get;
            set;
        }

        public static bool EnableSystemBalancing
        {
            get;
            set;
        }
        public static decimal VaultStandardFillAmount
        {
            get;
            set;
        }
        public static bool CentralizedVaultDeclaration
        {
            get;
            set;
        }


        public static string AGSValue
        {
            get;
            set;
        }

        public static bool ValidateGMUInSite
        {
            get;
            set;
        }

        public static string DefaultGMUValue
        {
            get;
            set;
        }

        public static int ServiceNotRunningInterval
        {
            get;
            set;
        }

        public static bool ShowVaultPrintMessage
        {
            get;
            set;
        }

        public static bool ShowVaultConfirmMessage
        {
            get;
            set;
        }
        public static bool ShowVaultSuccessMessage
        {
            get;
            set;
        }

        public static bool ManualCashEntryEnableZero
        {
            get;
            set;
        }

        public static bool ShowSystemCalendar
        {
            get;
            set;
        }
        public static bool IsMultipleVoucherRedemptionEnabled
        {
            get;
            set;
        }

        public static bool AutoFillDeclaredAmount
        {
            get;
            set;
        }
        public static bool IsCommonCDODeclarationEnabled
        {
            get;
            set;
        }

        public static bool ProcessW2GAmount
        {

            get { return _ProcessW2GAmount; }
            set { _ProcessW2GAmount = value; }
        }
        public static bool AddShortpayInVoucherOut
        {
            get
            { return _AddShortpayInVoucherOut; }
            set { _AddShortpayInVoucherOut = value; }
        }
        public static bool EnableVoucher
        {
            get { return _EnableVoucher; }
            set { _EnableVoucher = value; }
        }

        public static string HeadCashierSig
        {
            get { return _HeadCashierSig; }
            set { _HeadCashierSig = value; }
        }

        public static string ManagerSig
        {
            get { return _ManagerSig; }
            set { _ManagerSig = value; }
        }

        public static bool EnableHandpayReceipt
        {
            get { return _EnableHandpayReceipt; }
            set { _EnableHandpayReceipt = value; }
        }

        public static bool EnableIssueReceipt
        {
            get { return _EnableIssueReceipt; }
            set { _EnableIssueReceipt = value; }
        }

        public static string TicketDeclaration
        {
            get { return _TicketDeclaration; }
            set { _TicketDeclaration = value; }
        }

        public static bool EnableShortPayReceipt
        {
            get { return _EnableShortpayReceipt; }
            set { _EnableShortpayReceipt = value; }
        }

        public static bool EnableRefillReceipt
        {
            get { return _EnableRefillReceipt; }
            set { _EnableRefillReceipt = value; }
        }

        public static bool EnableRefundReceipt
        {
            get { return _EnableRefundReceipt; }
            set { _EnableRefundReceipt = value; }
        }

        public static bool EnableProgReceipt
        {
            get { return _EnableProgReceipt; }
            set { _EnableProgReceipt = value; }
        }

        public static bool Not_Issue_Ticket
        {
            get { return _Not_Issue_Ticket; }
            set { _Not_Issue_Ticket = value; }
        }

        public static bool TITO_Not_In_Use
        {
            get { return _TITO_Not_In_Use; }
            set { _TITO_Not_In_Use = value; }
        }

        public static bool OnScreenKeyboard
        {
            get { return _OnScreenKeyboard; }
            set { _OnScreenKeyboard = value; }
        }

        public static bool TV_FillScreen
        {
            get { return _TV_FillScreen; }
            set { _TV_FillScreen = value; }
        }

        public static bool VoidTransaction
        {
            get { return _VoidTransaction; }
            set { _VoidTransaction = value; }
        }

        public static bool HandpayManual
        {
            get { return _HandpayManual; }
            set { _HandpayManual = value; }
        }

        public static bool EnableLaundering
        {
            get { return _EnableLaundering; }
            set { _EnableLaundering = value; }
        }

        public static string SiteName
        {
            get { return _SiteName; }
            set { _SiteName = value; }
        }

        public static string Region
        {
            get { return _Region; }
            set { _Region = value; }
        }

        public static bool CD_Not_Use_Hoppers
        {
            get { return _CD_NOT_USE_HOPPERS; }
            set { _CD_NOT_USE_HOPPERS = value; }
        }

        public static string Connection
        {
            get { return _Connection; }
            set { _Connection = value; }
        }

        public static bool AllowOffLineRedeem
        {
            get { return _Allow_Offline_Redeem; }
            set { _Allow_Offline_Redeem = value; }
        }

        public static bool ReceiptPrinter
        {
            get { return _ReceiptPrinter; }
            set { _ReceiptPrinter = value; }
        }

        public static string SiteCode
        {
            get { return _SiteCode; }
            set { _SiteCode = value; }
        }

        public static bool SGVI_Enabled
        {
            get { return _SGVIEnabled; }
            set { _SGVIEnabled = value; }
        }

        public static int Gaming_Day_Start_Hour
        {
            get { return _Gaming_Day_Start_Hour; }
            set { _Gaming_Day_Start_Hour = value; }
        }

        public static int Ticket_Expire
        {
            get { return _Ticket_Expire; }
            set { _Ticket_Expire = value; }
        }

        public static string WebURL
        {
            get { return _WebURL; }
            set { _WebURL = value; }
        }

        public static string PrinterPort
        {
            get { return _PrinterPort; }
            set { _PrinterPort = value; }
        }

        public static string IssueTicketMaxValue
        {
            get { return _issueticketmaxvalue; }
            set { _issueticketmaxvalue = value; }
        }

        public static string RegulatoryType
        {
            get { return _RegulatoryType; }
            set { _RegulatoryType = value; }
        }

        public static string Client
        {
            get { return _Client; }
            set { _Client = value; }
        }

        public static bool RegulatoryEnabled
        {
            get { return _RegulatoryEnabled; }
            set { _RegulatoryEnabled = value; }
        }

        public static bool MaxHandPayAuthRequired
        {
            get { return _MaxHandPayAuthRequired; }
            set { _MaxHandPayAuthRequired = value; }
        }

        public static bool ManualEntryTicketValidation
        {
            get { return _ManualEntryTicketValidation; }
            set { _ManualEntryTicketValidation = value; }
        }

        public static bool RedeemExpiredTicket
        {
            get { return _RedeemExpiredTicket; }
            set { _RedeemExpiredTicket = value; }
        }

        public static bool IsAFTEnabledForSite
        {
            get { return _IsAFTEnabledForSite; }
            set { _IsAFTEnabledForSite = value; }
        }

        public static bool MachineMaintenance
        {
            get { return _MachineMaintenance; }
            set { _MachineMaintenance = value; }
        }

        public static string SiteFloorViewState
        {
            get { return _SiteFloorViewState; }
            set { _SiteFloorViewState = value; }
        }

        public static bool CentralizedDeclaration
        {

            get { return _CentralizedDeclaration; }
            set { _CentralizedDeclaration = value; }
        }
        public static bool Allow_Machine_Removal
        {

            get { return _Allow_Machine_Removal_WithoutDeclaration; }
            set { _Allow_Machine_Removal_WithoutDeclaration = value; }
        }
        public static bool DropSummaryReport
        {

            get { return _DropSummaryReport; }
            set { _DropSummaryReport = value; }
        }

        public static bool DeclarationAlert
        {

            get { return _DeclarationAlert; }
            set { _DeclarationAlert = value; }
        }

        public static bool DropAlert
        {

            get { return _DropAlert; }
            set { _DropAlert = value; }
        }

        public static bool ExpenseShare
        {

            get { return _ExpenseShare; }
            set { _ExpenseShare = value; }
        }

        public static bool WriteOffShare
        {

            get { return _WriteOffShare; }
            set { _WriteOffShare = value; }
        }

        public static string LiquidationType
        {
            get { return _LiquidationType; }
            set { _LiquidationType = value; }
        }
        public static bool LiquidationProfitShare
        {

            get { return _LiquidationProfitShare; }
            set { _LiquidationProfitShare = value; }
        }
        public static bool CentralizedReadLiquidation
        {

            get { return _CentralizedReadLiquidation; }
            set { _CentralizedReadLiquidation = value; }
        }




        public static bool IsPartCollectionEnabled
        {

            get { return _IsPartCollectionEnabled; }
            set { _IsPartCollectionEnabled = value; }
        }
        public static bool TicketValidate_EnableIssueReceipt
        {
            get { return _TicketValidate_EnableIssueReceipt; }
            set { _TicketValidate_EnableIssueReceipt = value; }
        }
        public static bool WeeklyReport
        {
            get;
            set;
        }

        public static bool EnableTicketRedeemRecipt { get; set; }

        public static int HourlyScreenMaxRecords { get; set; }

        public static bool RedeemConfirm { get; set; }

        public static bool printTicket { get; set; }

        public static double HandpayPayoutCustomer_Min { get; set; }

        public static double HandpayPayoutCustomer_Max { get; set; }

        public static double HandpayPayoutCustomer_BankAccNo { get; set; }

        public static bool PrintTicket_EncryptDigits { get; set; }

        public static int RedeemTicketCustomer_Min { get; set; }

        public static int RedeemTicketCustomer_Max { get; set; }

        public static int RedeemTicketCustomer_BankAcctNo { get; set; }

        public static bool CheckExchangeServerConnectivity { get; set; }

        public static int ExchangeServerConnectivityCheckInterval { get; set; }

        public static string ClearEventsOnFinalDrop { get; set; }

        public static string VoucherPrinterName { get; set; }

        public static bool IsFinalDropRequiredForRemoval { get; set; }

        public static string TimeZoneID { get; set; }

        public static string BillVoucherCounterCOMPort { get; set; }

        public static bool IsAFTIncludedInCalculation { get; set; }

        public static bool W2GMessage { get; set; }

        public static double W2GWinAmount { get; set; }
        
        public static string  CopyRightInfo { get; set; }       

        public static string HandPayBeepEnabled { get; set; }

        public static bool IsKioskRequired { get; set; }


        public static string DailyAutoReadTime { get; set; }

        public static string Handpay_Wav_Path { get; set; }

        public static string SHOWHANDPAYCODE { get; set; }

        public static bool IsEmployeeCardTrackingEnabled { get; set; }

        //CAGE
        public static bool CAGE_ALLOWCASHIERLOCONTKTS { get; set; }

        public static bool CAGE_ALLOWPRINTTKTOVERRIDE { get; set; }

        public static bool CAGE_TKTPRINTERENABLED { get; set; }

        public static int CAGE_MINTKTPRINTAMTFOREMP { get; set; }

        public static int CAGE_MAXTKTREDEMPTIONAMTFOREMP { get; set; }

        public static int CAGE_MAXTKTREDEMPTIONAMT { get; set; }

        public static int CAGE_MAXNOOFTKTPRINTLIMIT { get; set; }

        public static int CAGE_MAXDAILYCASHIERGENTKTAMT { get; set; }

        public static int CAGE_MAXTKTPRINTAMTFOREMP { get; set; }

        public static bool CAGE_ENABLED { get; set; }
        public static bool VoidVouchers { get; set; }
        public static bool SHOW_NAME_IN_RECEPIT_SIGNATURE { get; set; }

        public static bool Auto_Declare_Monies { get; set; }

        public static bool Disable_Machine_On_Drop { get; set; }

        public static bool NoWaitForDisableMachine { get; set; }

        public static int AUTOLOGOFF_TIMEOUT { get; set; }

        public static bool HANDLE_EXCEPTIONTICKETS { get; set; }

        public static bool HANDLE_EXCEPTIONTICKETS_COUNTER { get; set; }
        public static string PT_GATEWAY_IP { get; set; }
        public static int SDT_SendPTPortNo { get; set; }
        public static int SDT_SendCAPortNo { get; set; }

        public static bool HANDLE_EXCEPTION_PP_TICKETS { get; set; }

        public static bool ShortPayEnabled { get; set; }

        public static bool AutoDropEnabled { get; set; }

        public static bool ForceManualDrop { get; set; }

        #region Code Added by A.Vinod Kumar
        /// <summary>
        /// Gets or sets a value indicating whether [cash dispenser enabled].
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [cash dispenser enabled]; otherwise, <c>false</c>.
        /// </value>
        public static bool CashDispenserEnabled { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [auto cash dispense required].
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [auto cash dispense required]; otherwise, <c>false</c>.
        /// </value>
        public static bool AutoCashDispenseRequired { get; set; }

        /// <summary>
        /// Gets the bool value.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="name">The name.</param>
        /// <returns>Converted boolean value.</returns>
        public static bool GetBoolValue(System.Data.DataRow row, string name)
        {
            object source = null;

            if (row != null &&
                row.Table.Columns.Contains(name))
            {
                source = row[name];
            }
            return GetBoolValue(source, name);
        }

        /// <summary>
        /// Gets the bool value.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="name">The name.</param>
        /// <returns>Converted boolean value.</returns>
        public static bool GetBoolValue(object source, string name)
        {
            if (source != null)
            {
                string source2 = source.ToString().Trim();
                if (string.IsNullOrEmpty(source2)) return false;

                if (string.Compare(source2, "true", true) == 0 ||
                    source2 == "1") return true;
            }
            return false;
        }

        /// <summary>
        /// Gets the string value.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="name">The name.</param>
        /// <returns>Converted boolean value.</returns>
        public static string GetStringValue(System.Data.DataRow row, string name)
        {
            object source = null;

            if (row != null &&
                row.Table.Columns.Contains(name))
            {
                source = row[name];
            }
            return GetStringValue(source, name);
        }

        /// <summary>
        /// Gets the string value.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="name">The name.</param>
        /// <returns>Converted boolean value.</returns>
        public static string GetStringValue(object source, string name)
        {
            if (source != null)
            {
                string source2 = source.ToString().Trim();
                if (string.IsNullOrEmpty(source2)) return string.Empty;
                return source2;
            }
            return string.Empty;
        }

        public static bool SendPT10FromClient { get; set; }

        //public static bool REDEEM_TICKET_POP_UP_ALERT_VISIBILITY
        //{
        //    get { return _REDEEM_TICKET_POP_UP_ALERT_VISIBILITY; }
        //    set { _REDEEM_TICKET_POP_UP_ALERT_VISIBILITY = value; }
        //}
        #endregion

        public static bool Declaration_ShowoutValues { get; set; }

        public static bool NotesCounter_AutoStart { get; set; }

        public static bool ShortPayAuthorizationRequired { get; set; }

        public static double ShortPayAuthorizationLimit { get; set; }

        public static bool EnableCounterInManualCashEntry { get; set; }

       
        public static char TISTicketPrefixDigit { get; set; }

        public static int TISTicketPrefix { get; set; }

        public static bool IsGameCappingEnabled { get; set; }


        public static string MaximumPromotionalTicketsCount
        {
            get { return _MaximumPromotionalTicketsCount; }
            set { _MaximumPromotionalTicketsCount = value; }
        }

        public static string MaximumPromotionalTicketAmount
        {
            get { return _MaximumPromotionalTicketAmount; }
            set { _MaximumPromotionalTicketAmount = value; }
        }

        public static string DefaultPromotionalTicketExpireDays
        {
            get { return _DefaultPromotionalTicketExpireDays; }
            set { _DefaultPromotionalTicketExpireDays = value; }
        }

        public static bool DisplayGameNameInFloorView
        { get; set; }

        public static bool IsBillCounterAmountEditable
        { get; set; }

        public static int GridViewForcePeriodicWaitInterval
        { get; set; }

        public static int GridViewForcePeriodicThreadWaitInterval
        { get; set; }

        public static bool IsTicketAnomaliesEnabled
        {
            get;
            set;
        }
        public static string Hourly_DefaultItem { get; set; }

        public static bool AllowMultipleDrops { get; set; }

        public static bool ClearHandpayTilt { get; set; }
        public static bool AddShortpayCommentstoDefault { get; set; }
        public static bool ShowBatchWinLossOnDeclaration { get; set; }
        public static bool ShowCollectionReport { get; set; }
        public static bool ShowVarianceReport { get; set; }
    }
}
