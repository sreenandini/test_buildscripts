using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Linq.Mapping;

namespace Audit.Transport
{
    [Table(Name = "dbo.Audit_Modules")]
    public class AuditModules
    {

        private int _Audit_Module_ID;

        private string _Audit_Module_Name;

        public AuditModules()
        {
        }

        [Column(Storage = "_Audit_Module_ID", AutoSync = AutoSync.Always, DbType = "Int NOT NULL IDENTITY", IsDbGenerated = true)]
        public int Audit_Module_ID
        {
            get
            {
                return this._Audit_Module_ID;
            }
            set
            {
                if ((this._Audit_Module_ID != value))
                {
                    this._Audit_Module_ID = value;
                }
            }
        }

        [Column(Storage = "_Audit_Module_Name", DbType = "VarChar(50)")]
        public string Audit_Module_Name
        {
            get
            {
                return this._Audit_Module_Name;
            }
            set
            {
                if ((this._Audit_Module_Name != value))
                {
                    this._Audit_Module_Name = value;
                }
            }
        }
    }


    [Table(Name = "dbo.Audit_History")]
    public partial class Audit_History
    {

        private long _Audit_ID;

        private System.Nullable<System.DateTime> _Audit_Date;

        private System.Nullable<int> _Audit_User_ID;

        private string _Audit_User_Name;

        private System.Nullable<int> _Audit_Module_ID;

        private string _Audit_Module_Name;

        private string _Audit_Screen_Name;

        private string _Audit_Desc;

        private string _Audit_Slot;

        private string _Audit_Field;

        private string _Audit_Old_Vl;

        private string _Audit_New_Vl;

        private string _Audit_Operation_Type;

        private ModuleName _ModuleName;
        private ModuleNameEnterprise _ModuleNameEnterprise;
        private OperationType _OperationType;



        public Audit_History()
        {
        }

        public Audit_History Clone()
        {
            return (Audit_History)this.MemberwiseClone();
        }

        [Column(Storage = "_Audit_ID", AutoSync = AutoSync.Always, DbType = "BigInt NOT NULL IDENTITY", IsDbGenerated = true)]
        public long Audit_ID
        {
            get
            {
                return this._Audit_ID;
            }
            set
            {
                if ((this._Audit_ID != value))
                {
                    this._Audit_ID = value;
                }
            }
        }

        [Column(Storage = "_Audit_Date", DbType = "DateTime")]
        public System.Nullable<System.DateTime> Audit_Date
        {
            get
            {
                return this._Audit_Date;
            }
            set
            {
                if ((this._Audit_Date != value))
                {
                    this._Audit_Date = value;
                }
            }
        }

        [Column(Storage = "_Audit_User_ID", DbType = "Int")]
        public System.Nullable<int> Audit_User_ID
        {
            get
            {
                return this._Audit_User_ID;
            }
            set
            {
                if ((this._Audit_User_ID != value))
                {
                    this._Audit_User_ID = value;
                }
            }
        }

        [Column(Storage = "_Audit_User_Name", DbType = "VarChar(50)")]
        public string Audit_User_Name
        {
            get
            {
                return this._Audit_User_Name;
            }
            set
            {
                if ((this._Audit_User_Name != value))
                {
                    this._Audit_User_Name = value;
                }
            }
        }

        [Column(Storage = "_Audit_Module_ID", DbType = "Int")]
        public System.Nullable<int> Audit_Module_ID
        {
            get;
            private set;
         
        }


        [Column(Storage = "_Audit_Module_Name", DbType = "VarChar(50)")]
        public string Audit_Module_Name
        {
            get;
            private set;
       
        }

        public ModuleName AuditModuleName
        {
            get
            {
                return this._ModuleName;
            }
            set
            {
                this._ModuleName = value;
                this.Audit_Module_Name = value.ToString();
                this.Audit_Module_ID = (int)value;

            }
        }



        public ModuleNameEnterprise EnterpriseModuleName
        {
            get
            {
                return this._ModuleNameEnterprise;
            }
            set
            {
                this._ModuleNameEnterprise = value;
                this.Audit_Module_Name = value.ToString();
                this.Audit_Module_ID = (int)value;

            }

        }



        [Column(Storage = "_Audit_Screen_Name", DbType = "VarChar(50)")]
        public string Audit_Screen_Name
        {
            get
            {
                return this._Audit_Screen_Name;
            }
            set
            {
                if ((this._Audit_Screen_Name != value))
                {
                    this._Audit_Screen_Name = value;
                }
            }
        }

        [Column(Storage = "_Audit_Desc", DbType = "VarChar(500)")]
        public string Audit_Desc
        {
            get
            {
                return this._Audit_Desc;
            }
            set
            {
                if ((this._Audit_Desc != value))
                {
                    this._Audit_Desc = value;
                }
            }
        }

        [Column(Storage = "_Audit_Slot", DbType = "VarChar(50)")]
        public string Audit_Slot
        {
            get
            {
                return this._Audit_Slot;
            }
            set
            {
                if ((this._Audit_Slot != value))
                {
                    this._Audit_Slot = value;
                }
            }
        }

        [Column(Storage = "_Audit_Field", DbType = "VarChar(500)")]
        public string Audit_Field
        {
            get
            {
                return this._Audit_Field;
            }
            set
            {
                if ((this._Audit_Field != value))
                {
                    this._Audit_Field = value;
                }
            }
        }

        [Column(Storage = "_Audit_Old_Vl", DbType = "VarChar(500)")]
        public string Audit_Old_Vl
        {
            get
            {
                return this._Audit_Old_Vl;
            }
            set
            {
                if ((this._Audit_Old_Vl != value))
                {
                    this._Audit_Old_Vl = value;
                }
            }
        }

        [Column(Storage = "_Audit_New_Vl", DbType = "VarChar(500)")]
        public string Audit_New_Vl
        {
            get
            {
                return this._Audit_New_Vl;
            }
            set
            {
                if ((this._Audit_New_Vl != value))
                {
                    this._Audit_New_Vl = value;
                }
            }
        }

        [Column(Storage = "_Audit_Operation_Type", DbType = "VarChar(25)")]
        public string Audit_Operation_Type
        {
            get;
            private set;


        }

        public OperationType AuditOperationType
        {
            get
            {
                return this._OperationType;
            }
            set
            {
                this._OperationType = value;
                this.Audit_Operation_Type = value.ToString();
            }
        }

    }

    public partial class rsp_GetInitialSettingsResult
    {

        private string _BGSAdminWSUserID;

        private string _BGSAdminWSPwd;

        private System.Nullable<bool> _SGVI_Enabled;

        private System.Nullable<int> _SGVI_Payment_Days;

        private System.Nullable<int> _SGVI_Statement_Number;

        private string _ReportServerURL;

        private string _ReportFolder;

        private string _EmptyReportMessage;

        private System.Nullable<int> _AUTHORIZATION_KEY_EXPIRY_HOURS;

        private string _SenderCode;

        private System.Nullable<bool> _IsAuditingEnabled;

        private string _Client;

        private string _BMC_Reports_Header;

        private string _BMC_Reports_Language;

        private System.Nullable<bool> _MaxHandPayAuthRequired;

        private System.Nullable<bool> _ManualEntryTicketValidation;

        private System.Nullable<bool> _SlotLifeToDate;

        private System.Nullable<bool> _AllowPartNumberEdit;

        private System.Nullable<int> _RedeemTicketCustomer_Min;

        private System.Nullable<int> _RedeemTicketCustomer_Max;

        private string _RedeemTicketCustomer_BankAcctNo;

        private string _WindowsServices;

        private System.Nullable<bool> _IsAFTEnabledForSite;

        private System.Nullable<bool> _IsPowerPromoReportsRequired;

        private System.Nullable<bool> _MachineMaintenance;

        private string _CertificateIssuer;

        private System.Nullable<bool> _IsCertificateRequired;

        private System.Nullable<bool> _ComponentVerification;

        private string _GuardianServerIPAddress;

        private System.Nullable<bool> _IsMeterAdjustmentToolRequired;

        private System.Nullable<bool> _LiveMeter;

        private string _ClearEventsOnFinalDrop;

        private System.Nullable<bool> _Auto_Declare_Monies;

        private System.Nullable<bool> _IsAFTIncludedInCalculation;

        private System.Nullable<int> _TreasuryLimitForMajorPrizes;

        private System.Nullable<bool> _SHOWHANDPAYCODE;

        private System.Nullable<bool> _CheckForGamePartNumber;

        private System.Nullable<bool> _CentralizedDeclaration;

        private System.Nullable<bool> _IsTransmitterEnabled;

        private string _STMServerIP;

        private System.Nullable<bool> _StackerLevelAlert;

        private System.Nullable<bool> _DropScheduleAlert;

        private System.Nullable<bool> _AllowOfflineDeclaration;

        private System.Nullable<bool> _DeclarationAlert;

        private System.Nullable<int> _MinuteThreadCheckinHoursforAutoDrop;

        private System.Nullable<int> _RetryMinutesForCheckDB;

        private System.Nullable<bool> _StackerFeature;

        private System.Nullable<bool> _IsEmployeeCardTrackingEnabled;

        private System.Nullable<bool> _AddShortpayInVoucherOut;

        private System.Nullable<bool> _IsSiteLicensingEnabled;

        private System.Nullable<bool> _LiquidationProfitShare;

        private System.Nullable<bool> _UseAssetTemplate;

        private System.Nullable<bool> _CentralizedReadLiquidation;

        private System.Nullable<bool> _AGSSerialNumberAlphaNumeric;

        private System.Nullable<bool> _AllowDeMerge;

        private System.Nullable<bool> _IsEnrolmentFlag;

        private System.Nullable<int> _Login_Expiry_No_of_Days;

        private System.Nullable<int> _Login_Max_No_Of_Attempts;

        private string _PRODUCTVERSION;

        private System.Nullable<int> _STMServerPort;

        private System.Nullable<bool> _IsEnrolmentComplete;

        private System.Nullable<int> _AGSValue;

        private System.Nullable<bool> _IsSingleCardEmployee;

        private System.Nullable<int> _MaxNoOfCardsForEmployee;

        private System.Nullable<int> _MaxNoOfVaultCassettes;

        private System.Nullable<int> _MaxNoOfVaultHoppers;

        private System.Nullable<bool> _IsVaultEnabled;

        private System.Nullable<bool> _IsBillCounterAmountEditable;

        private System.Nullable<bool> _Vault_AutoPopulateDropValues;

        private System.Nullable<bool> _Vault_EndDeviceOnTerminate;

        private System.Nullable<bool> _IsCrossTicketingEnabled;

        private System.Nullable<bool> _AllowEnableDisableBarPosition;

        private System.Nullable<bool> _IsAlertEnabled;

        private System.Nullable<bool> _IsEmailAlertEnabled;

        private string _CustomerName;

        private System.Nullable<bool> _IsAutoCalendarEnabled;

        private string _ReportDataDateAloneFormat;

        private string _ReportDataDateNTimeFormat;

        private string _ReportPrintDateTimeFormat;

        private string _ReportDateFormat;

        private string _ReportDateTimeFormat;

        [Column(Storage = "_ReportDataDateAloneFormat", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string ReportDataDateAloneFormat
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

        [Column(Storage = "_ReportDataDateNTimeFormat", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string ReportDataDateNTimeFormat
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

        [Column(Storage = "_ReportPrintDateTimeFormat", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string ReportPrintDateTimeFormat
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

        [Column(Storage = "_ReportDateFormat", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string ReportDateFormat
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

        [Column(Storage = "_ReportDateTimeFormat", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string ReportDateTimeFormat
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


        [Column(Storage = "_BGSAdminWSUserID", DbType = "VarChar(8000) NOT NULL", CanBeNull = false)]
        public string BGSAdminWSUserID
        {
            get
            {
                return this._BGSAdminWSUserID;
            }
            set
            {
                if ((this._BGSAdminWSUserID != value))
                {
                    this._BGSAdminWSUserID = value;
                }
            }
        }

        [Column(Storage = "_BGSAdminWSPwd", DbType = "VarChar(8000) NOT NULL", CanBeNull = false)]
        public string BGSAdminWSPwd
        {
            get
            {
                return this._BGSAdminWSPwd;
            }
            set
            {
                if ((this._BGSAdminWSPwd != value))
                {
                    this._BGSAdminWSPwd = value;
                }
            }
        }

        [Column(Storage = "_SGVI_Enabled", DbType = "Bit")]
        public System.Nullable<bool> SGVI_Enabled
        {
            get
            {
                return this._SGVI_Enabled;
            }
            set
            {
                if ((this._SGVI_Enabled != value))
                {
                    this._SGVI_Enabled = value;
                }
            }
        }

        [Column(Storage = "_SGVI_Payment_Days", DbType = "Int")]
        public System.Nullable<int> SGVI_Payment_Days
        {
            get
            {
                return this._SGVI_Payment_Days;
            }
            set
            {
                if ((this._SGVI_Payment_Days != value))
                {
                    this._SGVI_Payment_Days = value;
                }
            }
        }

        [Column(Storage = "_SGVI_Statement_Number", DbType = "Int")]
        public System.Nullable<int> SGVI_Statement_Number
        {
            get
            {
                return this._SGVI_Statement_Number;
            }
            set
            {
                if ((this._SGVI_Statement_Number != value))
                {
                    this._SGVI_Statement_Number = value;
                }
            }
        }

        [Column(Storage = "_ReportServerURL", DbType = "VarChar(8000) NOT NULL", CanBeNull = false)]
        public string ReportServerURL
        {
            get
            {
                return this._ReportServerURL;
            }
            set
            {
                if ((this._ReportServerURL != value))
                {
                    this._ReportServerURL = value;
                }
            }
        }

        [Column(Storage = "_ReportFolder", DbType = "VarChar(8000) NOT NULL", CanBeNull = false)]
        public string ReportFolder
        {
            get
            {
                return this._ReportFolder;
            }
            set
            {
                if ((this._ReportFolder != value))
                {
                    this._ReportFolder = value;
                }
            }
        }

        [Column(Storage = "_EmptyReportMessage", DbType = "VarChar(8000) NOT NULL", CanBeNull = false)]
        public string EmptyReportMessage
        {
            get
            {
                return this._EmptyReportMessage;
            }
            set
            {
                if ((this._EmptyReportMessage != value))
                {
                    this._EmptyReportMessage = value;
                }
            }
        }

        [Column(Storage = "_AUTHORIZATION_KEY_EXPIRY_HOURS", DbType = "Int")]
        public System.Nullable<int> AUTHORIZATION_KEY_EXPIRY_HOURS
        {
            get
            {
                return this._AUTHORIZATION_KEY_EXPIRY_HOURS;
            }
            set
            {
                if ((this._AUTHORIZATION_KEY_EXPIRY_HOURS != value))
                {
                    this._AUTHORIZATION_KEY_EXPIRY_HOURS = value;
                }
            }
        }

        [Column(Storage = "_SenderCode", DbType = "VarChar(8000) NOT NULL", CanBeNull = false)]
        public string SenderCode
        {
            get
            {
                return this._SenderCode;
            }
            set
            {
                if ((this._SenderCode != value))
                {
                    this._SenderCode = value;
                }
            }
        }

        [Column(Storage = "_IsAuditingEnabled", DbType = "Bit")]
        public System.Nullable<bool> IsAuditingEnabled
        {
            get
            {
                return this._IsAuditingEnabled;
            }
            set
            {
                if ((this._IsAuditingEnabled != value))
                {
                    this._IsAuditingEnabled = value;
                }
            }
        }

        [Column(Storage = "_Client", DbType = "VarChar(8000) NOT NULL", CanBeNull = false)]
        public string Client
        {
            get
            {
                return this._Client;
            }
            set
            {
                if ((this._Client != value))
                {
                    this._Client = value;
                }
            }
        }

        [Column(Storage = "_BMC_Reports_Header", DbType = "VarChar(8000) NOT NULL", CanBeNull = false)]
        public string BMC_Reports_Header
        {
            get
            {
                return this._BMC_Reports_Header;
            }
            set
            {
                if ((this._BMC_Reports_Header != value))
                {
                    this._BMC_Reports_Header = value;
                }
            }
        }

        [Column(Storage = "_BMC_Reports_Language", DbType = "VarChar(8000) NOT NULL", CanBeNull = false)]
        public string BMC_Reports_Language
        {
            get
            {
                return this._BMC_Reports_Language;
            }
            set
            {
                if ((this._BMC_Reports_Language != value))
                {
                    this._BMC_Reports_Language = value;
                }
            }
        }

        [Column(Storage = "_MaxHandPayAuthRequired", DbType = "Bit")]
        public System.Nullable<bool> MaxHandPayAuthRequired
        {
            get
            {
                return this._MaxHandPayAuthRequired;
            }
            set
            {
                if ((this._MaxHandPayAuthRequired != value))
                {
                    this._MaxHandPayAuthRequired = value;
                }
            }
        }

        [Column(Storage = "_ManualEntryTicketValidation", DbType = "Bit")]
        public System.Nullable<bool> ManualEntryTicketValidation
        {
            get
            {
                return this._ManualEntryTicketValidation;
            }
            set
            {
                if ((this._ManualEntryTicketValidation != value))
                {
                    this._ManualEntryTicketValidation = value;
                }
            }
        }

        [Column(Storage = "_SlotLifeToDate", DbType = "Bit")]
        public System.Nullable<bool> SlotLifeToDate
        {
            get
            {
                return this._SlotLifeToDate;
            }
            set
            {
                if ((this._SlotLifeToDate != value))
                {
                    this._SlotLifeToDate = value;
                }
            }
        }

        [Column(Storage = "_AllowPartNumberEdit", DbType = "Bit")]
        public System.Nullable<bool> AllowPartNumberEdit
        {
            get
            {
                return this._AllowPartNumberEdit;
            }
            set
            {
                if ((this._AllowPartNumberEdit != value))
                {
                    this._AllowPartNumberEdit = value;
                }
            }
        }

        [Column(Storage = "_RedeemTicketCustomer_Min", DbType = "Int")]
        public System.Nullable<int> RedeemTicketCustomer_Min
        {
            get
            {
                return this._RedeemTicketCustomer_Min;
            }
            set
            {
                if ((this._RedeemTicketCustomer_Min != value))
                {
                    this._RedeemTicketCustomer_Min = value;
                }
            }
        }

        [Column(Storage = "_RedeemTicketCustomer_Max", DbType = "Int")]
        public System.Nullable<int> RedeemTicketCustomer_Max
        {
            get
            {
                return this._RedeemTicketCustomer_Max;
            }
            set
            {
                if ((this._RedeemTicketCustomer_Max != value))
                {
                    this._RedeemTicketCustomer_Max = value;
                }
            }
        }

        [Column(Storage = "_RedeemTicketCustomer_BankAcctNo", DbType = "VarChar(8000) NOT NULL", CanBeNull = false)]
        public string RedeemTicketCustomer_BankAcctNo
        {
            get
            {
                return this._RedeemTicketCustomer_BankAcctNo;
            }
            set
            {
                if ((this._RedeemTicketCustomer_BankAcctNo != value))
                {
                    this._RedeemTicketCustomer_BankAcctNo = value;
                }
            }
        }

        [Column(Storage = "_WindowsServices", DbType = "VarChar(8000) NOT NULL", CanBeNull = false)]
        public string WindowsServices
        {
            get
            {
                return this._WindowsServices;
            }
            set
            {
                if ((this._WindowsServices != value))
                {
                    this._WindowsServices = value;
                }
            }
        }

        [Column(Storage = "_IsAFTEnabledForSite", DbType = "Bit")]
        public System.Nullable<bool> IsAFTEnabledForSite
        {
            get
            {
                return this._IsAFTEnabledForSite;
            }
            set
            {
                if ((this._IsAFTEnabledForSite != value))
                {
                    this._IsAFTEnabledForSite = value;
                }
            }
        }

        [Column(Storage = "_IsPowerPromoReportsRequired", DbType = "Bit")]
        public System.Nullable<bool> IsPowerPromoReportsRequired
        {
            get
            {
                return this._IsPowerPromoReportsRequired;
            }
            set
            {
                if ((this._IsPowerPromoReportsRequired != value))
                {
                    this._IsPowerPromoReportsRequired = value;
                }
            }
        }

        [Column(Storage = "_MachineMaintenance", DbType = "Bit")]
        public System.Nullable<bool> MachineMaintenance
        {
            get
            {
                return this._MachineMaintenance;
            }
            set
            {
                if ((this._MachineMaintenance != value))
                {
                    this._MachineMaintenance = value;
                }
            }
        }

        [Column(Storage = "_CertificateIssuer", DbType = "VarChar(8000) NOT NULL", CanBeNull = false)]
        public string CertificateIssuer
        {
            get
            {
                return this._CertificateIssuer;
            }
            set
            {
                if ((this._CertificateIssuer != value))
                {
                    this._CertificateIssuer = value;
                }
            }
        }

        [Column(Storage = "_IsCertificateRequired", DbType = "Bit")]
        public System.Nullable<bool> IsCertificateRequired
        {
            get
            {
                return this._IsCertificateRequired;
            }
            set
            {
                if ((this._IsCertificateRequired != value))
                {
                    this._IsCertificateRequired = value;
                }
            }
        }

        [Column(Storage = "_ComponentVerification", DbType = "Bit")]
        public System.Nullable<bool> ComponentVerification
        {
            get
            {
                return this._ComponentVerification;
            }
            set
            {
                if ((this._ComponentVerification != value))
                {
                    this._ComponentVerification = value;
                }
            }
        }

        [Column(Storage = "_GuardianServerIPAddress", DbType = "VarChar(8000) NOT NULL", CanBeNull = false)]
        public string GuardianServerIPAddress
        {
            get
            {
                return this._GuardianServerIPAddress;
            }
            set
            {
                if ((this._GuardianServerIPAddress != value))
                {
                    this._GuardianServerIPAddress = value;
                }
            }
        }

        [Column(Storage = "_IsMeterAdjustmentToolRequired", DbType = "Bit")]
        public System.Nullable<bool> IsMeterAdjustmentToolRequired
        {
            get
            {
                return this._IsMeterAdjustmentToolRequired;
            }
            set
            {
                if ((this._IsMeterAdjustmentToolRequired != value))
                {
                    this._IsMeterAdjustmentToolRequired = value;
                }
            }
        }

        [Column(Storage = "_LiveMeter", DbType = "Bit")]
        public System.Nullable<bool> LiveMeter
        {
            get
            {
                return this._LiveMeter;
            }
            set
            {
                if ((this._LiveMeter != value))
                {
                    this._LiveMeter = value;
                }
            }
        }

        [Column(Storage = "_ClearEventsOnFinalDrop", DbType = "VarChar(8000) NOT NULL", CanBeNull = false)]
        public string ClearEventsOnFinalDrop
        {
            get
            {
                return this._ClearEventsOnFinalDrop;
            }
            set
            {
                if ((this._ClearEventsOnFinalDrop != value))
                {
                    this._ClearEventsOnFinalDrop = value;
                }
            }
        }

        [Column(Storage = "_Auto_Declare_Monies", DbType = "Bit")]
        public System.Nullable<bool> Auto_Declare_Monies
        {
            get
            {
                return this._Auto_Declare_Monies;
            }
            set
            {
                if ((this._Auto_Declare_Monies != value))
                {
                    this._Auto_Declare_Monies = value;
                }
            }
        }

        [Column(Storage = "_IsAFTIncludedInCalculation", DbType = "Bit")]
        public System.Nullable<bool> IsAFTIncludedInCalculation
        {
            get
            {
                return this._IsAFTIncludedInCalculation;
            }
            set
            {
                if ((this._IsAFTIncludedInCalculation != value))
                {
                    this._IsAFTIncludedInCalculation = value;
                }
            }
        }

        [Column(Storage = "_TreasuryLimitForMajorPrizes", DbType = "Int")]
        public System.Nullable<int> TreasuryLimitForMajorPrizes
        {
            get
            {
                return this._TreasuryLimitForMajorPrizes;
            }
            set
            {
                if ((this._TreasuryLimitForMajorPrizes != value))
                {
                    this._TreasuryLimitForMajorPrizes = value;
                }
            }
        }

        [Column(Storage = "_SHOWHANDPAYCODE", DbType = "Bit")]
        public System.Nullable<bool> SHOWHANDPAYCODE
        {
            get
            {
                return this._SHOWHANDPAYCODE;
            }
            set
            {
                if ((this._SHOWHANDPAYCODE != value))
                {
                    this._SHOWHANDPAYCODE = value;
                }
            }
        }

        [Column(Storage = "_CheckForGamePartNumber", DbType = "Bit")]
        public System.Nullable<bool> CheckForGamePartNumber
        {
            get
            {
                return this._CheckForGamePartNumber;
            }
            set
            {
                if ((this._CheckForGamePartNumber != value))
                {
                    this._CheckForGamePartNumber = value;
                }
            }
        }

        [Column(Storage = "_CentralizedDeclaration", DbType = "Bit")]
        public System.Nullable<bool> CentralizedDeclaration
        {
            get
            {
                return this._CentralizedDeclaration;
            }
            set
            {
                if ((this._CentralizedDeclaration != value))
                {
                    this._CentralizedDeclaration = value;
                }
            }
        }

        [Column(Storage = "_IsTransmitterEnabled", DbType = "Bit")]
        public System.Nullable<bool> IsTransmitterEnabled
        {
            get
            {
                return this._IsTransmitterEnabled;
            }
            set
            {
                if ((this._IsTransmitterEnabled != value))
                {
                    this._IsTransmitterEnabled = value;
                }
            }
        }

        [Column(Storage = "_STMServerIP", DbType = "VarChar(8000) NOT NULL", CanBeNull = false)]
        public string STMServerIP
        {
            get
            {
                return this._STMServerIP;
            }
            set
            {
                if ((this._STMServerIP != value))
                {
                    this._STMServerIP = value;
                }
            }
        }

        [Column(Storage = "_StackerLevelAlert", DbType = "Bit")]
        public System.Nullable<bool> StackerLevelAlert
        {
            get
            {
                return this._StackerLevelAlert;
            }
            set
            {
                if ((this._StackerLevelAlert != value))
                {
                    this._StackerLevelAlert = value;
                }
            }
        }

        [Column(Storage = "_DropScheduleAlert", DbType = "Bit")]
        public System.Nullable<bool> DropScheduleAlert
        {
            get
            {
                return this._DropScheduleAlert;
            }
            set
            {
                if ((this._DropScheduleAlert != value))
                {
                    this._DropScheduleAlert = value;
                }
            }
        }

        [Column(Storage = "_AllowOfflineDeclaration", DbType = "Bit")]
        public System.Nullable<bool> AllowOfflineDeclaration
        {
            get
            {
                return this._AllowOfflineDeclaration;
            }
            set
            {
                if ((this._AllowOfflineDeclaration != value))
                {
                    this._AllowOfflineDeclaration = value;
                }
            }
        }

        [Column(Storage = "_DeclarationAlert", DbType = "Bit")]
        public System.Nullable<bool> DeclarationAlert
        {
            get
            {
                return this._DeclarationAlert;
            }
            set
            {
                if ((this._DeclarationAlert != value))
                {
                    this._DeclarationAlert = value;
                }
            }
        }

        [Column(Storage = "_MinuteThreadCheckinHoursforAutoDrop", DbType = "Int")]
        public System.Nullable<int> MinuteThreadCheckinHoursforAutoDrop
        {
            get
            {
                return this._MinuteThreadCheckinHoursforAutoDrop;
            }
            set
            {
                if ((this._MinuteThreadCheckinHoursforAutoDrop != value))
                {
                    this._MinuteThreadCheckinHoursforAutoDrop = value;
                }
            }
        }

        [Column(Storage = "_RetryMinutesForCheckDB", DbType = "Int")]
        public System.Nullable<int> RetryMinutesForCheckDB
        {
            get
            {
                return this._RetryMinutesForCheckDB;
            }
            set
            {
                if ((this._RetryMinutesForCheckDB != value))
                {
                    this._RetryMinutesForCheckDB = value;
                }
            }
        }

        [Column(Storage = "_StackerFeature", DbType = "Bit")]
        public System.Nullable<bool> StackerFeature
        {
            get
            {
                return this._StackerFeature;
            }
            set
            {
                if ((this._StackerFeature != value))
                {
                    this._StackerFeature = value;
                }
            }
        }

        [Column(Storage = "_IsEmployeeCardTrackingEnabled", DbType = "Bit")]
        public System.Nullable<bool> IsEmployeeCardTrackingEnabled
        {
            get
            {
                return this._IsEmployeeCardTrackingEnabled;
            }
            set
            {
                if ((this._IsEmployeeCardTrackingEnabled != value))
                {
                    this._IsEmployeeCardTrackingEnabled = value;
                }
            }
        }

        [Column(Storage = "_AddShortpayInVoucherOut", DbType = "Bit")]
        public System.Nullable<bool> AddShortpayInVoucherOut
        {
            get
            {
                return this._AddShortpayInVoucherOut;
            }
            set
            {
                if ((this._AddShortpayInVoucherOut != value))
                {
                    this._AddShortpayInVoucherOut = value;
                }
            }
        }

        [Column(Storage = "_IsSiteLicensingEnabled", DbType = "Bit")]
        public System.Nullable<bool> IsSiteLicensingEnabled
        {
            get
            {
                return this._IsSiteLicensingEnabled;
            }
            set
            {
                if ((this._IsSiteLicensingEnabled != value))
                {
                    this._IsSiteLicensingEnabled = value;
                }
            }
        }

        [Column(Storage = "_LiquidationProfitShare", DbType = "Bit")]
        public System.Nullable<bool> LiquidationProfitShare
        {
            get
            {
                return this._LiquidationProfitShare;
            }
            set
            {
                if ((this._LiquidationProfitShare != value))
                {
                    this._LiquidationProfitShare = value;
                }
            }
        }

        [Column(Storage = "_UseAssetTemplate", DbType = "Bit")]
        public System.Nullable<bool> UseAssetTemplate
        {
            get
            {
                return this._UseAssetTemplate;
            }
            set
            {
                if ((this._UseAssetTemplate != value))
                {
                    this._UseAssetTemplate = value;
                }
            }
        }

        [Column(Storage = "_CentralizedReadLiquidation", DbType = "Bit")]
        public System.Nullable<bool> CentralizedReadLiquidation
        {
            get
            {
                return this._CentralizedReadLiquidation;
            }
            set
            {
                if ((this._CentralizedReadLiquidation != value))
                {
                    this._CentralizedReadLiquidation = value;
                }
            }
        }

        [Column(Storage = "_AGSSerialNumberAlphaNumeric", DbType = "Bit")]
        public System.Nullable<bool> AGSSerialNumberAlphaNumeric
        {
            get
            {
                return this._AGSSerialNumberAlphaNumeric;
            }
            set
            {
                if ((this._AGSSerialNumberAlphaNumeric != value))
                {
                    this._AGSSerialNumberAlphaNumeric = value;
                }
            }
        }

        [Column(Storage = "_AllowDeMerge", DbType = "Bit")]
        public System.Nullable<bool> AllowDeMerge
        {
            get
            {
                return this._AllowDeMerge;
            }
            set
            {
                if ((this._AllowDeMerge != value))
                {
                    this._AllowDeMerge = value;
                }
            }
        }

        [Column(Storage = "_IsEnrolmentFlag", DbType = "Bit")]
        public System.Nullable<bool> IsEnrolmentFlag
        {
            get
            {
                return this._IsEnrolmentFlag;
            }
            set
            {
                if ((this._IsEnrolmentFlag != value))
                {
                    this._IsEnrolmentFlag = value;
                }
            }
        }

        [Column(Storage = "_Login_Expiry_No_of_Days", DbType = "Int")]
        public System.Nullable<int> Login_Expiry_No_of_Days
        {
            get
            {
                return this._Login_Expiry_No_of_Days;
            }
            set
            {
                if ((this._Login_Expiry_No_of_Days != value))
                {
                    this._Login_Expiry_No_of_Days = value;
                }
            }
        }

        [Column(Storage = "_Login_Max_No_Of_Attempts", DbType = "Int")]
        public System.Nullable<int> Login_Max_No_Of_Attempts
        {
            get
            {
                return this._Login_Max_No_Of_Attempts;
            }
            set
            {
                if ((this._Login_Max_No_Of_Attempts != value))
                {
                    this._Login_Max_No_Of_Attempts = value;
                }
            }
        }

        [Column(Storage = "_PRODUCTVERSION", DbType = "VarChar(8000) NOT NULL", CanBeNull = false)]
        public string PRODUCTVERSION
        {
            get
            {
                return this._PRODUCTVERSION;
            }
            set
            {
                if ((this._PRODUCTVERSION != value))
                {
                    this._PRODUCTVERSION = value;
                }
            }
        }

        [Column(Storage = "_STMServerPort", DbType = "Int")]
        public System.Nullable<int> STMServerPort
        {
            get
            {
                return this._STMServerPort;
            }
            set
            {
                if ((this._STMServerPort != value))
                {
                    this._STMServerPort = value;
                }
            }
        }

        [Column(Storage = "_IsEnrolmentComplete", DbType = "Bit")]
        public System.Nullable<bool> IsEnrolmentComplete
        {
            get
            {
                return this._IsEnrolmentComplete;
            }
            set
            {
                if ((this._IsEnrolmentComplete != value))
                {
                    this._IsEnrolmentComplete = value;
                }
            }
        }

        [Column(Storage = "_AGSValue", DbType = "Int")]
        public System.Nullable<int> AGSValue
        {
            get
            {
                return this._AGSValue;
            }
            set
            {
                if ((this._AGSValue != value))
                {
                    this._AGSValue = value;
                }
            }
        }

        [Column(Storage = "_IsSingleCardEmployee", DbType = "Bit")]
        public System.Nullable<bool> IsSingleCardEmployee
        {
            get
            {
                return this._IsSingleCardEmployee;
            }
            set
            {
                if ((this._IsSingleCardEmployee != value))
                {
                    this._IsSingleCardEmployee = value;
                }
            }
        }

        [Column(Storage = "_MaxNoOfCardsForEmployee", DbType = "Int")]
        public System.Nullable<int> MaxNoOfCardsForEmployee
        {
            get
            {
                return this._MaxNoOfCardsForEmployee;
            }
            set
            {
                if ((this._MaxNoOfCardsForEmployee != value))
                {
                    this._MaxNoOfCardsForEmployee = value;
                }
            }
        }

        [Column(Storage = "_MaxNoOfVaultCassettes", DbType = "Int")]
        public System.Nullable<int> MaxNoOfVaultCassettes
        {
            get
            {
                return this._MaxNoOfVaultCassettes;
            }
            set
            {
                if ((this._MaxNoOfVaultCassettes != value))
                {
                    this._MaxNoOfVaultCassettes = value;
                }
            }
        }

        [Column(Storage = "_MaxNoOfVaultHoppers", DbType = "Int")]
        public System.Nullable<int> MaxNoOfVaultHoppers
        {
            get
            {
                return this._MaxNoOfVaultHoppers;
            }
            set
            {
                if ((this._MaxNoOfVaultHoppers != value))
                {
                    this._MaxNoOfVaultHoppers = value;
                }
            }
        }

        [Column(Storage = "_IsVaultEnabled", DbType = "Bit")]
        public System.Nullable<bool> IsVaultEnabled
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

        [Column(Storage = "_IsBillCounterAmountEditable", DbType = "Bit")]
        public System.Nullable<bool> IsBillCounterAmountEditable
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

        [Column(Storage = "_Vault_AutoPopulateDropValues", DbType = "Bit")]
        public System.Nullable<bool> Vault_AutoPopulateDropValues
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

        [Column(Storage = "_Vault_EndDeviceOnTerminate", DbType = "Bit")]
        public System.Nullable<bool> Vault_EndDeviceOnTerminate
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

        [Column(Storage = "_IsCrossTicketingEnabled", DbType = "Bit")]
        public System.Nullable<bool> IsCrossTicketingEnabled
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


        [Column(Storage = "_AllowEnableDisableBarPosition", DbType = "Bit")]
        public System.Nullable<bool> AllowEnableDisableBarPosition
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

        [Column(Storage = "_CustomerName", DbType = "VarChar(8000) NOT NULL", CanBeNull = false)]
        public string CustomerName
        {
            get
            {
                return this._CustomerName;
            }
            set
            {
                if ((this._CustomerName != value))
                {
                    this._CustomerName = value;
                }
            }
        }

        [Column(Storage = "_IsAlertEnabled", DbType = "BIT")]
        public System.Nullable<bool> IsAlertEnabled
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

        [Column(Storage = "_IsEmailAlertEnabled", DbType = "BIT")]
        public System.Nullable<bool> IsEmailAlertEnabled
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

        [Column(Storage = "_IsAutoCalendarEnabled", DbType = "BIT")]
        public System.Nullable<bool> IsAutoCalendarEnabled
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


    }
}
