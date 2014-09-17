using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.EnterpriseBusiness.Entities
{
    public static class SettingsEntity
    {
        private static string _BGSAdminWSUserID;

        private static string _BGSAdminWSPwd;

        private static bool _SGVI_Enabled;

        private static int _SGVI_Payment_Days;

        private static int _SGVI_Statement_Number;

        private static string _ReportServerURL;

        private static string _ReportFolder;

        private static string _EmptyReportMessage;

        private static int _AUTHORIZATION_KEY_EXPIRY_HOURS;

        private static string _SenderCode;

        private static bool _IsAuditingEnabled;

        private static string _Client;

        private static string _BMC_Reports_Header;

        private static string _BMC_Reports_Language;

        private static bool _MaxHandPayAuthRequired;

        private static bool _ManualEntryTicketValidation;

        private static bool _SlotLifeToDate;

        private static bool _AllowPartNumberEdit;

        private static int _RedeemTicketCustomer_Min;

        private static int _RedeemTicketCustomer_Max;

        private static string _RedeemTicketCustomer_BankAcctNo;

        private static string _WindowsServices;

        private static bool _IsAFTEnabledForSite;

        private static bool _IsPowerPromoReportsRequired;

        private static bool _MachineMaintenance;

        private static string _CertificateIssuer;

        private static bool _IsCertificateRequired;

        private static bool _ComponentVerification;

        private static string _GuardianServerIPAddress;

        private static bool _IsMeterAdjustmentToolRequired;

        private static bool _LiveMeter;

        private static string _ClearEventsOnFinalDrop;

        private static bool _Auto_Declare_Monies;

        private static bool _IsAFTIncludedInCalculation;

        private static int _TreasuryLimitForMajorPrizes;

        private static bool _SHOWHANDPAYCODE;

        private static bool _CheckForGamePartNumber;

        private static bool _CentralizedDeclaration;

        private static bool _IsTransmitterEnabled;

        private static string _STMServerIP;

        private static bool _StackerLevelAlert;

        private static bool _DropScheduleAlert;

        private static bool _AllowOfflineDeclaration;

        private static bool _DeclarationAlert;

        private static int _MinuteThreadCheckinHoursforAutoDrop;

        private static int _RetryMinutesForCheckDB;

        private static bool _StackerFeature;

        private static bool _IsEmployeeCardTrackingEnabled;

        private static bool _AddShortpayInVoucherOut;

        private static bool _IsSiteLicensingEnabled;

        private static bool _LiquidationProfitShare;

        private static bool _UseAssetTemplate;

        private static bool _CentralizedReadLiquidation;

        private static bool _AGSSerialNumberAlphaNumeric;

        private static bool _AllowDeMerge;

        private static bool _IsEnrolmentFlag;

        private static int _Login_Expiry_No_of_Days;

        private static int _Login_Max_No_Of_Attempts;

        private static string _PRODUCTVERSION;

        private static int _STMServerPort;

        private static bool _IsEnrolmentComplete;

        private static int _AGSValue;

        private static bool _IsSingleCardEmployee;

        private static int _MaxNoOfCardsForEmployee;

        private static int _MaxNoOfVaultCassettes;

        private static int _MaxNoOfVaultHoppers;

        private static bool _IsVaultEnabled;

        private static bool _IsBillCounterAmountEditable;

        private static bool _Vault_AutoPopulateDropValues;

        private static bool _Vault_EndDeviceOnTerminate;

        private static bool _IsCrossTicketingEnabled;

        private static bool _AllowEnableDisableBarPosition;

        private static string _CustomerName;

        private static bool _IsAlertEnabled;

        private static bool _IsEmailAlertEnabled;

        private static bool _IsAutoCalendarEnabled;

        private static string _ReportDataDateAloneFormat;

        private static string _ReportDataDateNTimeFormat;

        private static string _ReportPrintDateTimeFormat;

        private static string _ReportDateFormat;

        private static string _ReportDateTimeFormat;

        private static bool _IsServiceCallFeatureFull;

        private static bool _ShowCollectionReport;

        public static string BGSAdminWSUserID
        {
            get
            {
                return _BGSAdminWSUserID;
            }
            set
            {
                if ((_BGSAdminWSUserID != value))
                {
                    _BGSAdminWSUserID = value;
                }
            }
        }

        public static string BGSAdminWSPwd
        {
            get
            {
                return _BGSAdminWSPwd;
            }
            set
            {
                if ((_BGSAdminWSPwd != value))
                {
                    _BGSAdminWSPwd = value;
                }
            }
        }

        public static bool SGVI_Enabled
        {
            get
            {
                return _SGVI_Enabled;
            }
            set
            {
                if ((_SGVI_Enabled != value))
                {
                    _SGVI_Enabled = value;
                }
            }
        }

        public static int SGVI_Payment_Days
        {
            get
            {
                return _SGVI_Payment_Days;
            }
            set
            {
                if ((_SGVI_Payment_Days != value))
                {
                    _SGVI_Payment_Days = value;
                }
            }
        }

        public static int SGVI_Statement_Number
        {
            get
            {
                return _SGVI_Statement_Number;
            }
            set
            {
                if ((_SGVI_Statement_Number != value))
                {
                    _SGVI_Statement_Number = value;
                }
            }
        }

        public static string ReportServerURL
        {
            get
            {
                return _ReportServerURL;
            }
            set
            {
                if ((_ReportServerURL != value))
                {
                    _ReportServerURL = value;
                }
            }
        }

        public static string ReportFolder
        {
            get
            {
                return _ReportFolder;
            }
            set
            {
                if ((_ReportFolder != value))
                {
                    _ReportFolder = value;
                }
            }
        }

        public static string EmptyReportMessage
        {
            get
            {
                return _EmptyReportMessage;
            }
            set
            {
                if ((_EmptyReportMessage != value))
                {
                    _EmptyReportMessage = value;
                }
            }
        }

        public static int AUTHORIZATION_KEY_EXPIRY_HOURS
        {
            get
            {
                return _AUTHORIZATION_KEY_EXPIRY_HOURS;
            }
            set
            {
                if ((_AUTHORIZATION_KEY_EXPIRY_HOURS != value))
                {
                    _AUTHORIZATION_KEY_EXPIRY_HOURS = value;
                }
            }
        }

        public static string SenderCode
        {
            get
            {
                return _SenderCode;
            }
            set
            {
                if ((_SenderCode != value))
                {
                    _SenderCode = value;
                }
            }
        }

        public static bool IsAuditingEnabled
        {
            get
            {
                return _IsAuditingEnabled;
            }
            set
            {
                if ((_IsAuditingEnabled != value))
                {
                    _IsAuditingEnabled = value;
                }
            }
        }

        public static string Client
        {
            get
            {
                return _Client;
            }
            set
            {
                if ((_Client != value))
                {
                    _Client = value;
                }
            }
        }

        public static string BMC_Reports_Header
        {
            get
            {
                return _BMC_Reports_Header;
            }
            set
            {
                if ((_BMC_Reports_Header != value))
                {
                    _BMC_Reports_Header = value;
                }
            }
        }

        public static string BMC_Reports_Language
        {
            get
            {
                return _BMC_Reports_Language;
            }
            set
            {
                if ((_BMC_Reports_Language != value))
                {
                    _BMC_Reports_Language = value;
                }
            }
        }

        public static bool MaxHandPayAuthRequired
        {
            get
            {
                return _MaxHandPayAuthRequired;
            }
            set
            {
                if ((_MaxHandPayAuthRequired != value))
                {
                    _MaxHandPayAuthRequired = value;
                }
            }
        }

        public static bool ManualEntryTicketValidation
        {
            get
            {
                return _ManualEntryTicketValidation;
            }
            set
            {
                if ((_ManualEntryTicketValidation != value))
                {
                    _ManualEntryTicketValidation = value;
                }
            }
        }

        public static bool SlotLifeToDate
        {
            get
            {
                return _SlotLifeToDate;
            }
            set
            {
                if ((_SlotLifeToDate != value))
                {
                    _SlotLifeToDate = value;
                }
            }
        }

        public static bool AllowPartNumberEdit
        {
            get
            {
                return _AllowPartNumberEdit;
            }
            set
            {
                if ((_AllowPartNumberEdit != value))
                {
                    _AllowPartNumberEdit = value;
                }
            }
        }

        public static int RedeemTicketCustomer_Min
        {
            get
            {
                return _RedeemTicketCustomer_Min;
            }
            set
            {
                if ((_RedeemTicketCustomer_Min != value))
                {
                    _RedeemTicketCustomer_Min = value;
                }
            }
        }

        public static int RedeemTicketCustomer_Max
        {
            get
            {
                return _RedeemTicketCustomer_Max;
            }
            set
            {
                if ((_RedeemTicketCustomer_Max != value))
                {
                    _RedeemTicketCustomer_Max = value;
                }
            }
        }

        public static string RedeemTicketCustomer_BankAcctNo
        {
            get
            {
                return _RedeemTicketCustomer_BankAcctNo;
            }
            set
            {
                if ((_RedeemTicketCustomer_BankAcctNo != value))
                {
                    _RedeemTicketCustomer_BankAcctNo = value;
                }
            }
        }

        public static string WindowsServices
        {
            get
            {
                return _WindowsServices;
            }
            set
            {
                if ((_WindowsServices != value))
                {
                    _WindowsServices = value;
                }
            }
        }

        public static bool IsAFTEnabledForSite
        {
            get
            {
                return _IsAFTEnabledForSite;
            }
            set
            {
                if ((_IsAFTEnabledForSite != value))
                {
                    _IsAFTEnabledForSite = value;
                }
            }
        }

        public static bool IsPowerPromoReportsRequired
        {
            get
            {
                return _IsPowerPromoReportsRequired;
            }
            set
            {
                if ((_IsPowerPromoReportsRequired != value))
                {
                    _IsPowerPromoReportsRequired = value;
                }
            }
        }

        public static bool MachineMaintenance
        {
            get
            {
                return _MachineMaintenance;
            }
            set
            {
                if ((_MachineMaintenance != value))
                {
                    _MachineMaintenance = value;
                }
            }
        }

        public static string CertificateIssuer
        {
            get
            {
                return _CertificateIssuer;
            }
            set
            {
                if ((_CertificateIssuer != value))
                {
                    _CertificateIssuer = value;
                }
            }
        }

        public static bool IsCertificateRequired
        {
            get
            {
                return _IsCertificateRequired;
            }
            set
            {
                if ((_IsCertificateRequired != value))
                {
                    _IsCertificateRequired = value;
                }
            }
        }

        public static bool ComponentVerification
        {
            get
            {
                return _ComponentVerification;
            }
            set
            {
                if ((_ComponentVerification != value))
                {
                    _ComponentVerification = value;
                }
            }
        }

        public static string GuardianServerIPAddress
        {
            get
            {
                return _GuardianServerIPAddress;
            }
            set
            {
                if ((_GuardianServerIPAddress != value))
                {
                    _GuardianServerIPAddress = value;
                }
            }
        }

        public static bool IsMeterAdjustmentToolRequired
        {
            get
            {
                return _IsMeterAdjustmentToolRequired;
            }
            set
            {
                if ((_IsMeterAdjustmentToolRequired != value))
                {
                    _IsMeterAdjustmentToolRequired = value;
                }
            }
        }

        public static bool LiveMeter
        {
            get
            {
                return _LiveMeter;
            }
            set
            {
                if ((_LiveMeter != value))
                {
                    _LiveMeter = value;
                }
            }
        }

        public static string ClearEventsOnFinalDrop
        {
            get
            {
                return _ClearEventsOnFinalDrop;
            }
            set
            {
                if ((_ClearEventsOnFinalDrop != value))
                {
                    _ClearEventsOnFinalDrop = value;
                }
            }
        }

        public static bool Auto_Declare_Monies
        {
            get
            {
                return _Auto_Declare_Monies;
            }
            set
            {
                if ((_Auto_Declare_Monies != value))
                {
                    _Auto_Declare_Monies = value;
                }
            }
        }

        public static bool IsAFTIncludedInCalculation
        {
            get
            {
                return _IsAFTIncludedInCalculation;
            }
            set
            {
                if ((_IsAFTIncludedInCalculation != value))
                {
                    _IsAFTIncludedInCalculation = value;
                }
            }
        }

        public static int TreasuryLimitForMajorPrizes
        {
            get
            {
                return _TreasuryLimitForMajorPrizes;
            }
            set
            {
                if ((_TreasuryLimitForMajorPrizes != value))
                {
                    _TreasuryLimitForMajorPrizes = value;
                }
            }
        }

        public static bool SHOWHANDPAYCODE
        {
            get
            {
                return _SHOWHANDPAYCODE;
            }
            set
            {
                if ((_SHOWHANDPAYCODE != value))
                {
                    _SHOWHANDPAYCODE = value;
                }
            }
        }

        public static bool CheckForGamePartNumber
        {
            get
            {
                return _CheckForGamePartNumber;
            }
            set
            {
                if ((_CheckForGamePartNumber != value))
                {
                    _CheckForGamePartNumber = value;
                }
            }
        }

        public static bool CentralizedDeclaration
        {
            get
            {
                return _CentralizedDeclaration;
            }
            set
            {
                if ((_CentralizedDeclaration != value))
                {
                    _CentralizedDeclaration = value;
                }
            }
        }

        public static bool IsTransmitterEnabled
        {
            get
            {
                return _IsTransmitterEnabled;
            }
            set
            {
                if ((_IsTransmitterEnabled != value))
                {
                    _IsTransmitterEnabled = value;
                }
            }
        }

        public static string STMServerIP
        {
            get
            {
                return _STMServerIP;
            }
            set
            {
                if ((_STMServerIP != value))
                {
                    _STMServerIP = value;
                }
            }
        }

        public static bool StackerLevelAlert
        {
            get
            {
                return _StackerLevelAlert;
            }
            set
            {
                if ((_StackerLevelAlert != value))
                {
                    _StackerLevelAlert = value;
                }
            }
        }

        public static bool DropScheduleAlert
        {
            get
            {
                return _DropScheduleAlert;
            }
            set
            {
                if ((_DropScheduleAlert != value))
                {
                    _DropScheduleAlert = value;
                }
            }
        }

        public static bool AllowOfflineDeclaration
        {
            get
            {
                return _AllowOfflineDeclaration;
            }
            set
            {
                if ((_AllowOfflineDeclaration != value))
                {
                    _AllowOfflineDeclaration = value;
                }
            }
        }

        public static bool DeclarationAlert
        {
            get
            {
                return _DeclarationAlert;
            }
            set
            {
                if ((_DeclarationAlert != value))
                {
                    _DeclarationAlert = value;
                }
            }
        }

        public static int MinuteThreadCheckinHoursforAutoDrop
        {
            get
            {
                return _MinuteThreadCheckinHoursforAutoDrop;
            }
            set
            {
                if ((_MinuteThreadCheckinHoursforAutoDrop != value))
                {
                    _MinuteThreadCheckinHoursforAutoDrop = value;
                }
            }
        }

        public static int RetryMinutesForCheckDB
        {
            get
            {
                return _RetryMinutesForCheckDB;
            }
            set
            {
                if ((_RetryMinutesForCheckDB != value))
                {
                    _RetryMinutesForCheckDB = value;
                }
            }
        }

        public static bool StackerFeature
        {
            get
            {
                return _StackerFeature;
            }
            set
            {
                if ((_StackerFeature != value))
                {
                    _StackerFeature = value;
                }
            }
        }

        public static bool IsEmployeeCardTrackingEnabled
        {
            get
            {
                return _IsEmployeeCardTrackingEnabled;
            }
            set
            {
                if ((_IsEmployeeCardTrackingEnabled != value))
                {
                    _IsEmployeeCardTrackingEnabled = value;
                }
            }
        }

        public static bool AddShortpayInVoucherOut
        {
            get
            {
                return _AddShortpayInVoucherOut;
            }
            set
            {
                if ((_AddShortpayInVoucherOut != value))
                {
                    _AddShortpayInVoucherOut = value;
                }
            }
        }

        public static bool IsSiteLicensingEnabled
        {
            get
            {
                return _IsSiteLicensingEnabled;
            }
            set
            {
                if ((_IsSiteLicensingEnabled != value))
                {
                    _IsSiteLicensingEnabled = value;
                }
            }
        }

        public static bool LiquidationProfitShare
        {
            get
            {
                return _LiquidationProfitShare;
            }
            set
            {
                if ((_LiquidationProfitShare != value))
                {
                    _LiquidationProfitShare = value;
                }
            }
        }

        public static bool UseAssetTemplate
        {
            get
            {
                return _UseAssetTemplate;
            }
            set
            {
                if ((_UseAssetTemplate != value))
                {
                    _UseAssetTemplate = value;
                }
            }
        }

        public static bool CentralizedReadLiquidation
        {
            get
            {
                return _CentralizedReadLiquidation;
            }
            set
            {
                if ((_CentralizedReadLiquidation != value))
                {
                    _CentralizedReadLiquidation = value;
                }
            }
        }

        public static bool AGSSerialNumberAlphaNumeric
        {
            get
            {
                return _AGSSerialNumberAlphaNumeric;
            }
            set
            {
                if ((_AGSSerialNumberAlphaNumeric != value))
                {
                    _AGSSerialNumberAlphaNumeric = value;
                }
            }
        }

        public static bool AllowDeMerge
        {
            get
            {
                return _AllowDeMerge;
            }
            set
            {
                if ((_AllowDeMerge != value))
                {
                    _AllowDeMerge = value;
                }
            }
        }

        public static bool IsEnrolmentFlag
        {
            get
            {
                return _IsEnrolmentFlag;
            }
            set
            {
                if ((_IsEnrolmentFlag != value))
                {
                    _IsEnrolmentFlag = value;
                }
            }
        }

        public static int Login_Expiry_No_of_Days
        {
            get
            {
                return _Login_Expiry_No_of_Days;
            }
            set
            {
                if ((_Login_Expiry_No_of_Days != value))
                {
                    _Login_Expiry_No_of_Days = value;
                }
            }
        }

        public static int Login_Max_No_Of_Attempts
        {
            get
            {
                return _Login_Max_No_Of_Attempts;
            }
            set
            {
                if ((_Login_Max_No_Of_Attempts != value))
                {
                    _Login_Max_No_Of_Attempts = value;
                }
            }
        }

        public static string PRODUCTVERSION
        {
            get
            {
                return _PRODUCTVERSION;
            }
            set
            {
                if ((_PRODUCTVERSION != value))
                {
                    _PRODUCTVERSION = value;
                }
            }
        }

        public static int STMServerPort
        {
            get
            {
                return _STMServerPort;
            }
            set
            {
                if ((_STMServerPort != value))
                {
                    _STMServerPort = value;
                }
            }
        }

        public static bool IsEnrolmentComplete
        {
            get
            {
                return _IsEnrolmentComplete;
            }
            set
            {
                if ((_IsEnrolmentComplete != value))
                {
                    _IsEnrolmentComplete = value;
                }
            }
        }

        public static int AGSValue
        {
            get
            {
                return _AGSValue;
            }
            set
            {
                if ((_AGSValue != value))
                {
                    _AGSValue = value;
                }
            }
        }

        public static bool IsSingleCardEmployee
        {
            get
            {
                return _IsSingleCardEmployee;
            }
            set
            {
                if ((_IsSingleCardEmployee != value))
                {
                    _IsSingleCardEmployee = value;
                }
            }
        }

        public static int MaxNoOfCardsForEmployee
        {
            get
            {
                return _MaxNoOfCardsForEmployee;
            }
            set
            {
                if ((_MaxNoOfCardsForEmployee != value))
                {
                    _MaxNoOfCardsForEmployee = value;
                }
            }
        }


        public static int MaxNoOfVaultCassettes
        {
            get
            {
                return _MaxNoOfVaultCassettes;
            }
            set
            {
                if ((_MaxNoOfVaultCassettes != value))
                {
                    _MaxNoOfVaultCassettes = value;
                }
            }
        }


        public static int MaxNoOfVaultHoppers
        {
            get
            {
                return _MaxNoOfVaultHoppers;
            }
            set
            {
                if ((_MaxNoOfVaultHoppers != value))
                {
                    _MaxNoOfVaultHoppers = value;
                }
            }
        }

        public static bool IsVaultEnabled
        {
            get
            {
                return _IsVaultEnabled;
            }
            set
            {
                if ((_IsVaultEnabled != value))
                {
                    _IsVaultEnabled = value;
                }
            }
        }

        public static bool IsBillCounterAmountEditable
        {
            get
            {
                return _IsBillCounterAmountEditable;
            }
            set
            {
                if ((_IsBillCounterAmountEditable != value))
                {
                    _IsBillCounterAmountEditable = value;
                }
            }
        }

        public static bool Vault_AutoPopulateDropValues
        {
            get
            {
                return _Vault_AutoPopulateDropValues;
            }
            set
            {
                if ((_Vault_AutoPopulateDropValues != value))
                {
                    _Vault_AutoPopulateDropValues = value;
                }
            }
        }

        public static bool Vault_EndDeviceOnTerminate

        {
            get
            {
                return _Vault_EndDeviceOnTerminate;
            }
            set
            {
                if ((_Vault_EndDeviceOnTerminate != value))
                {
                    _Vault_EndDeviceOnTerminate = value;
                }
            }
        }

        public static bool IsCrossTicketingEnabled
        {
            get
            {
                return _IsCrossTicketingEnabled;
            }
            set
            {
                if ((_IsCrossTicketingEnabled != value))
                {
                    _IsCrossTicketingEnabled = value;
                }
            }
        }

        public static bool AllowEnableDisableBarPosition
        {
            get
            {
                return _AllowEnableDisableBarPosition;
            }
            set
            {
                if ((_AllowEnableDisableBarPosition != value))
                {
                    _AllowEnableDisableBarPosition = value;
                }
            }
        }

        public static string CustomerName
        {
            get
            {
                return _CustomerName;
            }
            set
            {
                if ((_CustomerName != value))
                {
                    _CustomerName = value;
                }
            }
        }

        public static bool IsAlertEnabled
        {
            get
            {
                return _IsAlertEnabled;
            }
            set
            {
                if ((_IsAlertEnabled != value))
                {
                    _IsAlertEnabled = value;
                }
            }
        }

        public static bool IsEmailAlertEnabled
        {
            get
            {
                return _IsEmailAlertEnabled;
            }
            set
            {
                if ((_IsEmailAlertEnabled != value))
                {
                    _IsEmailAlertEnabled = value;
                }
            }
        }

        public static bool IsAutoCalendarEnabled
        {
            get
            {
                return _IsAutoCalendarEnabled;
            }
            set
            {
                if ((_IsAutoCalendarEnabled != value))
                {
                    _IsAutoCalendarEnabled = value;
                }
            }
        }

        public static string ReportDataDateAloneFormat
        {
            get
            {
                return _ReportDataDateAloneFormat;
            }
            set
            {
                if ((_ReportDataDateAloneFormat != value))
                {
                    _ReportDataDateAloneFormat = value;
                }
            }
        }

        public static string ReportDataDateNTimeFormat
        {
            get
            {
                return _ReportDataDateNTimeFormat;
            }
            set
            {
                if ((_ReportDataDateNTimeFormat != value))
                {
                    _ReportDataDateNTimeFormat = value;
                }
            }
        }

        public static string ReportPrintDateTimeFormat
        {
            get
            {
                return _ReportPrintDateTimeFormat;
            }
            set
            {
                if ((_ReportPrintDateTimeFormat != value))
                {
                    _ReportPrintDateTimeFormat = value;
                }
            }
        }

        public static string ReportDateFormat
        {
            get
            {
                return _ReportDateFormat;
            }
            set
            {
                if ((_ReportDateFormat != value))
                {
                    _ReportDateFormat = value;
                }
            }
        }

        public static string ReportDateTimeFormat
        {
            get
            {
                return _ReportDateTimeFormat;
            }
            set
            {
                if ((_ReportDateTimeFormat != value))
                {
                    _ReportDateTimeFormat = value;
                }
            }
        }

        public static bool IsServiceCallFeatureFull
        {
            get
            {
                return _IsServiceCallFeatureFull;
            }
            set
            {
                if ((_IsServiceCallFeatureFull != value))
                {
                    _IsServiceCallFeatureFull = value;
                }
            }
        }

        public static bool ShowCollectionReport
        {
            get
            {
                return _ShowCollectionReport;
            }
            set
            {
                if ((_ShowCollectionReport != value))
                {
                    _ShowCollectionReport = value;
                }
            }
        }
        
    }
}
