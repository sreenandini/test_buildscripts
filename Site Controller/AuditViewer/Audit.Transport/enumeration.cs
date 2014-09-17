using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Audit.Transport
{
    // split enums from standard lisr, other list used to populate combo's etc
    public enum ModuleNameEnterprise
    {
        UserGroupAdmin = 500,
        UserSiteAdmin = 501,
        CentralisedSettings = 502,
        Settings = 515,
        AUDIT_GENERAL = 503,
        AUDIT_MODELTYPE = 504,
        // 502 --> 528 used by enterprise client
        Audit_Cascade = 507,
        AUDIT_SUBCOMPANY = 508,
        Audit_Company = 511,
        AUDIT_USERS = 516,
        Calendar = 512,
        AUDIT_GAMELIBRARY = 519,
        AUDIT_MACHINEMODEL = 522,
        AUDIT_MACHINETYPE = 523,
        AUDIT_MANUFACTURER = 524,
        PURCHASEMACHINE = 521,
        ReportRoleAdmin = 525,
        ComponentVerification = 530,
        SiteLicensing = 536,
        CentralDeclaration = 533,
        DropAlert = 534,
        AUDIT_USERGROUP = 518,
        EmployeeCard = 557,
        Stacker = 535,
        ProfitShare = 540,
        ExpenseShare = 541,
        ShareHolder = 539,
        AssetTemplate = 543,
        CollectionBasedLiquidation = 544,
        ReadBasedLiquidation = 545,
        ExportDetail = 546,
        Unlock = 547,
        RouteManager = 549,
        AUDIT_CUSTOMERACCESS = 517,
        ImportExportAssetFile = 552,
        VaultManager = 551,
        Operators = 513,
        Depot = 514,
        VoidVoucher = 554,
        EnableDisableMachine = 558,
        AUDIT_POSITION = 559,
        AUDIT_SITE = 560,
        AUDIT_GAMECAPPING = 561,
        AUDIT_TERMINATEMACHINE = 562,
        Login = 5,
        Logout = 6,
        ChangePassword = 7,
        Maintanance = 563,
        SiteDropSequencing = 564,
        GmuFaultEvents=505,
        MeterAdjustement = 564,
        MultipleVoucherRedeem = 714,        
        EmployeeGMUModes=715,
        EmployeeGMUEvents = 716,
        AutoCalendar = 717,
        SGVIFinancial = 718, // For terms module,
	MailServerInfo = 35,
	MailSubscribers = 35,
    ServiceCalls = 506  // Service calls


    }

    public enum ModuleIDEnterprise : int
    {
        UserGroupAdmin = 500,
        UserSiteAdmin = 501,
        CentralisedSettings = 502,
        Settings = 515,
        // 502 --> 528 used by enterprise client
        AUDIT_USERS = 516,
        AUDIT_GENERAL = 503,
        AUDIT_MODELTYPE = 504,
        AUDIT_MACHINEMODEL = 522,
        AUDIT_MACHINETYPE = 523,
        AUDIT_MANUFACTURER = 524,
        PURCHASEMACHINE = 521,
        ReportRoleAdmin = 525,
        ComponentVerification = 530,
        CentralDeclaration = 533,
        DropAlert = 534,
        SiteLicensing = 536,
        EmployeeCard = 537,
        AssetTemplate = 543,
        VaultManager = 551,
        ImportExportAssetFile = 552,
        VoidVoucher = 554,
        EnableDisableMachine = 558,
        MultipleVoucherRedeem = 714,
	MailServerInfo = 35,
	MailSubscribers = 35,
    ServiceCalls = 506  // Service calls
    }

    public enum ModuleName
    {
        Enrollment = 1,
        Events,
        //FactoryReset,
        FieldServices,
        AttendantPay,
        Login,
        Logout,
        MachineDrop,
        MachineMaintenance,
        ManualAttendantPay,
        Password,
        PlayerClub,
        PrintReports,
        ReinstateMachine,
        RemoveMachine,
        Exception_Voucher,
        Shutdown,
        //SiteSetup,
        // Treasury,
        Voucher,
        Promotion,
        Void,
        MSMQ,
        TransitMachine,
        PortBlocking,
        Shortpay,
        Cash_Dispenser = 701,
        SiteLicensing = 536,
        OfflineVoucher_Shortpay,
        LocalDeclaration,
        EmployeeCardsession,
        AnalysisDetails,
        CollectionBasedLiquidation = 544,
        ReadBasedLiquidation = 545,
        ExportDetail = 546,
        Unlock = 547,
        RouteManager = 549,
        SpotCheck = 550,
        Vault = 710,
        VoidVoucher = 554,
        UpdateGmuNo = 555,
        RebootGMU = 556,
        EnableDisableMachine = 558,
        EnrollmentNGA,
        VaultCassettes = 712,
        Factory_Reset=713,
        MultipleVoucherRedeem= 714,
	MailServerInfo = 35,
	MailSubscribers = 35,
    ServiceCalls = 506  // Service calls
    }

    public enum ModuleID : int
    {
        Enrollment = 1,
        Events,
        //FactoryReset,
        FieldServices,
        AttendantPay,
        Login,
        Logout,
        MachineDrop,
        MachineMaintenance,
        ManualAttendantPay,
        Password,
        PlayerClub,
        PrintReports,
        ReinstateMachine,
        RemoveMachine,
        Exception_Voucher,                          //Newly Added
        Shutdown,
        //SiteSetup,
        //Treasury,
        Voucher,
        Promotion,
        Void,
        MSMQ,
        TransitMachine,                             //Newly Added
        PortBlocking,                               //Newly Added 
        Shortpay,
        Cascade = 507,
        Cash_Dispenser = 701,
        SiteLicensing = 536,
        OfflineVoucher_Shortpay,
        LocalDeclaration,
        EmployeeCardSession,
        CollectionBasedLiquidation = 544,
        ReadBasedLiquidation = 545,
        ExportDetail = 546,
        Unlock = 547,
        RouteManager = 549,
        Vault = 710,
        VoidVoucher = 554,
        UpdateGmuNo = 555,
        EnableDisableMachine = 558,
        EnrollmentNGA,
        VaultCassettes = 712,
         Factory_Reset=713,
         MultipleVoucherRedeem = 714,
	MailServerInfo = 35,
	MailSubscribers = 35,
        ServiceCalls = 506  // Service calls
    }

    public enum OperationType
    {
        ADD,
        MODIFY,
        DELETE
    }
    public enum SiteMachineControlType
    {
        ENABLED,
        DISABLED,
        PENDING,
        ENABLE,
        DISABLE,
        MACHINEENABLE,
        MACHINEDISABLE
    }

}
