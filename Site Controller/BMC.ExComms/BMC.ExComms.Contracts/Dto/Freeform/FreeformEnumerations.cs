using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    /// <summary>
    /// Freeform message flow direction
    /// </summary>
    public enum FF_FlowDirection
    {
        G2H = 0,    // GMU to Host
        H2G = 1     // Host to GMU
    }

    /// <summary>
    /// Freeform message flow Initiation
    /// </summary>
    public enum FF_FlowInitiation
    {
        Any = 0,
        Gmu = 1,    // GMU to Host
        Host = 2     // Host to GMU
    }

    // GMU Device Type
    public enum FF_GmuId_DeviceTypes
    {
        Unknown = 0,
        Ethernet = 5
    }

    // Session Ids
    [FFGmuIdAppIdMapping(typeof(FF_AppId_SessionIds))]
    public enum FF_GmuId_SessionIds
    {
        Override = -99,
        Internal = -1,
        None = 0,
        Intrepid = 1,
        GMU_GameSettings = 2,
        RequestQuery = 3,
        FrontEndEncryption = 4,
        CashCage = 9,
        Tickets = 10,
        Security = 11,
        ECash = 14,
        A1 = 15,
        GMUEvent = 16,
        CompPrinting = 17,
        GMUAuthentication = 18,
        RemoteShutdown = 19,
        SmartCard = 20,
        CodeDownloader = 21,
        GameMessage = 22,
        GIM = 23,
        GameCapping = 24,
        RouletteBallDrop = 25,
        PowerCard = 26,
        iButton_EmployeeECash = 27,
        PID = 28,
        PlayerInformationDisplay = 29,
        CashlessRechargeECash = 30,
        Multi_CurrencyECash = 31,
        SystemGame = 32,
        JackpotSlipProcessing = 33,
        ProgressiveMonitoringUnit = 38,
        PlayerIn_sessionPoints = 39,
        Pre_commitmentFacility = 40,
        DirectedDMKFills = 41,
        SystemCharacteristics = 42,
        Messages = 43,
        GMUDebug = 126,
        //None = 0x0,
        //Dialog = 0x03,
        //Security = 0x0B,
        //ECash = 0x0E,
        //A1 = 0x0F,
        //Jackpot = 0x16,
        //GIM = 0x17,
        //PlayerCapping = 0x18,
        //RouletteBallDrop = 0x19,
        //PWRCard = 0x1A,
        //Game = 0x20,
        //JackpotKeypadProcessing = 0x21,
        //Precommitment = 0x28,
        //Ticket = 0x8A,
        //EncryptedTicket = 0x8B,
        //EncryptedECash = 0x8E,
        //GameMessage = 0xA0,
        //DirectMessageMin = 0xB0,
        //DirectMessageMax = 0xDF,
        ExCommsServer = 513,
    };

    // Poll Codes
    [FFGmuIdAppIdMapping(typeof(FF_AppId_H2G_PollCodes))]
    public enum FF_GmuId_H2G_PollCodes
    {
        Override = -99,
        Internal = -1,
        None = 0,
        TransportACK = 0xA0,
        TransportACKDelayedOrPlayerInfo = 0xA1,
        TransportNACK = 0xB0,
        Freeform2Poll = 0xCD,
        JackpotResponse = 0xE0,
        Freeform2 = 0xEB,
        Freeform2Resend = 0xEC,
        Poll = 0xF0,
        ForcedPeriodic = 0xF1,
        Promo = 0xF2,
        PollNews = 0xF3,
        ConditionalDisplay = 0xF5,
        ConditionalForcedExceptionCode = 0xF6,
        _RESERVED = 0xF7,
        BonusPointMultiplier = 0xF8,
        ECash = 0xF9,
        ECashEnable = 0xFA,
        FreeformResponse = 0xFB,
        FreeformNoResponse = 0xFC,
        Freeform3Response = 0xFD,			// GMU initiating message, application response required from host + 16 bit length, no segment data
        Freeform3NoResponse = 0xFE,		    // GMU initiating message, no application response required from host + 16 bit length, no segment data        
    };

    // Gmu 2 Net Message Types
    [FFGmuIdAppIdMapping(typeof(FF_AppId_G2H_MessageTypes))]
    public enum FF_GmuId_G2H_MessageTypes
    {
        None = 0,
        FullExtensions = 0xA1,
        FreeForm = 0xA2,
        UnifiedGMU = 0xA4,
        ECash = 0xC0,
        MeterConverted = 0xF0,
        MeterWithJackpot = 0xF2,
        MeterWithPlayer = 0xF3,
        MeterFullParkPlace = 0xF5,
        Coupons = 0xF6,
        MeterFull = 0xF7,
        MeterFullWithValidator = 0xF8,
    };

    // Gmu 2 Net Commands
    [FFGmuIdAppIdMapping(typeof(FF_AppId_G2H_Commands))]
    public enum FF_GmuId_G2H_Commands
    {
        None = 0,
        ACK = 0xB7,							// Acknowledgement required from host
        NACK = 0xB8,						// No acknowledgement required from host
        GMUInitA0 = 0xB9,			        // GMU initiating message, no application response required from host (Response A0 or B0)
        ResponseRequest = 0xBC,			    // GMU initiating message, application response required from host
        Freeform3NoResponse = 0xBD,		    // GMU initiating message, no application response required from host + 16 bit length, no segment data
        Freeform3Response = 0xBE,			// GMU initiating message, application response required from host + 16 bit length, no segment data
        Freeform2 = 0xC6					// Freeform 2 Message
    };

    // Net 2 Gmu Response Codes
    [FFGmuIdAppIdMapping(typeof(FF_AppId_H2G_ResponseCodes))]
    public enum FF_GmuId_H2G_ResponseCodes
    {
        None = 0,
        ACK = 1,
        NACK = 2,
    }

    // Target Ids
    [FFGmuIdAppIdMapping(typeof(FF_AppId_TargetIds))]
    public enum FF_GmuId_TargetIds
    {
        None = 0,
        Internal = -2,
        Overall = -1,
        Interpid = 1,
        GMUVariableAction = 2,
        EPI = 3,
        Reserved = 4,
        ApplicationQualifier = 5,
        ApplicationResponseConfiguration = 6,
        ApplicationResponseEcho = 7,
        DefaultIO = 8,
        Cage = 9,
        Tickets = 10,                                       // 0x0A
        Security = 11,                                      // 0x0B
        TestBox = 12,                                       // 0x0C
        Unused = 13,                                        // 0x0D
        ECash = 14,                                         // 0x0E
        MeterBlock = 15,                                    // 0x0F
        StatusBlock = 16,                                   // 0x10
        Printer = 17,
        GMUAuthentication = 18,
        SystemToEPIDisplayMessage = 19,
        GameInfo = 20,
        CodeDownload = 21,
        GameMessage = 22,
        GIM = 23,
        GameCapping = 24,
        Roulette = 25,
        PowerCard = 26,
        CreditKeyOff = 27,
        PID = 28,
        JackpotSlopProcessing = 33,
        SystemLog = 34,
        ProgressiveReport = 35,
        FeatureSupport = 36,
        RemoteShutdown = 37,
        Precommitment = 41,
        EmployeeCard = 43,
        DebugFunctions = 126,
    }

    [FFGmuIdAppIdMapping(typeof(FF_AppId_Acknowledgements))]
    public enum FF_GmuId_Acknowledgements
    {
        General = 0,
        Freeform = 1
    }

    /// <summary>
    /// Response Type
    /// </summary>
    [FFGmuIdAppIdMapping(typeof(FF_AppId_ResponseStatus_Types))]
    public enum FF_GmuId_ResponseStatus_Types
    {
        Fail = 0,
        Success = 1
    }

    #region Ticket Processing

    /// <summary>
    /// Ticket Message Type Enum
    /// </summary>
    [FFGmuIdAppIdMapping(typeof(FF_AppId_TicketMessageTypes))]
    public enum FF_GmuId_TicketMessageTypes
    {
        TicketPrinted = 1,
        TicketVoid = 2,
        TicketRedemption = 3,
        TicketRedemptionComplete = 4,
        EnablementRequest = 5,
        EnablementResponse = 6,
        TicketPrintStatusResult = 7,
        OfflineTicketInfo = 8,
        TicketSequenceNumberUpdate = 9
    }

    /// <summary>
    /// Ticket Type
    /// </summary>
    [FFGmuIdAppIdMapping(typeof(FF_AppId_TicketTypes))]
    public enum FF_GmuId_TicketTypes
    {
        Cashable = 0,
        NonCashable = 1,
        CashablePromo = 2
    }

    /// <summary>
    /// Ticket Void Type
    /// </summary>
    [FFGmuIdAppIdMapping(typeof(FF_AppId_TicketPrintStatus))]
    public enum FF_GmuId_TicketPrintStatus
    {
        Unknown = 0,
        PaperOut = 1,
        PaperJam = 2,
        PaperLow = 3,
        PrinterCommunicationsFailure = 4
    }

    /// <summary>
    /// Ticket Redemption CLose Status
    /// </summary>
    [FFGmuIdAppIdMapping(typeof(FF_AppId_TicketRedemption_Close_Status))]
    public enum FF_GmuId_TicketRedemption_Close_Status
    {
        Success = 0,
        CouponRejectedbySystem = 1,
        SystemCommunicationsTimeout = 2,
        TiltDuringTransaction = 3,
        BlackoutDuringTransaction = 4,
        GameCommunicationsTimeout = 5,
        ValueLookupTableError = 6
    }

    /// <summary>
    /// Ticket Enablement Request Command
    /// </summary>
    [FFGmuIdAppIdMapping(typeof(FF_AppId_TicketEnablement_Request_Command))]
    public enum FF_GmuId_TicketEnablement_Request_Command
    {
        DisableTicketing = 0,
        EnableTicketing = 1,
        QueryCurrentTicketingEnablement = 2
    }

    /// <summary>
    /// Ticket Enablement Response Status
    /// </summary>
    [FFGmuIdAppIdMapping(typeof(FF_AppId_TicketEnablement_Response_Status))]
    public enum FF_GmuId_TicketEnablement_Response_Status
    {
        TicketingDisabled = 0,
        TicketingEnabled = 1,
        NotATicketCapableGame = 2
    }

    /// <summary>
    /// Ticket Offline Status
    /// </summary>
    [FFGmuIdAppIdMapping(typeof(FF_AppId_TicketOfflineStatus))]
    public enum FF_GmuId_TicketOfflineStatus
    {
        WasOnline = 0,
        WasOffline = 1
    }

    /// <summary>
    /// Ticket Sequence Number Update Error
    /// </summary>
    [FFGmuIdAppIdMapping(typeof(FF_AppId_TicketSeqNoUpdate_Error))]
    public enum FF_GmuId_TicketSeqNoUpdate_Error
    {
        Unknown = 0,
        PaperOut = 1,
        PaperJam = 2,
        PaperLow = 3,
        PrinterCommunicationsFailure = 4,
        DelayedResponseFromGame = 5
    }

    #endregion //Freeform Ticket Processing

    #region Security

    /// <summary>
    /// Security Encryption Types
    /// </summary>
    [FFGmuIdAppIdMapping(typeof(FF_AppId_Security_Encryption_Types))]
    public enum FF_GmuId_Security_Encryption_Types
    {
        Test = 1,
        SDSEncryption = 2,
        SDSKeyExchange = 3,
        SDSAuthentication = 4,
        SDSEncryptionAuthentication = 5,
        AFTKeyExchangeStart = 6,
        AFTKeyExchangeEnd = 7,
        IndexedEncryption = 8,
        IndexedKeyExchangeStart = 9,
        IndexedKeyExchangeEnd = 10
    }

    [FFGmuIdAppIdMapping(typeof(FF_AppId_KeyExchange))]
    public enum FF_GmuId_KeyExchange
    {
        Ticket = 0,
        ECash = 1,
        FrontEnd = 2,
        Dialog = 3
    }

    #endregion //FF_Security_Encryption_Types

    #region GMU Variable Action

    [FFGmuIdAppIdMapping(typeof(FF_AppId_GMUVarAction))]
    public enum FF_GmuId_GMUVarAction
    {
        TicketNumber = 4,
        TicketSystemSlotID = 5,
        TicketPrintDate = 6,
        TicketExpirationDate = 7,
        TicketKey = 8,
        TimeOfDay = 0xD,
        RestrictedTicketExpirationDays = 0xE,
        EnablePrintingOfRestrictedTickets = 0xF,
        MaximumOfflineTicketsAllowed = 0x10,
        OfflineTicketTextLine1 = 0x11,
        OfflineTicketTextLine2 = 0x12,
    }

    /// <summary>
    /// Enable Printing of Restricted Tickets
    /// </summary>
    public enum FF_GmuId_GmuVarAction_EnablePrint_RT
    {
        Dont_Allow_Printing_RT = 0,
        Allow_Printing_RT = 1,
    }

    #endregion //GMU Variable Action

    #region AccountMeters
    /// <summary>
    /// Describes Event Meteres Source 1.GMU 2.GAME
    /// </summary>
    [FFGmuIdAppIdMapping(typeof(FF_AppId_AccountingMetersSourceIds))]
    public enum FF_GmuId_AccountingMetersSourceIds
    {
        Gmu = 1,
        Game = 2
    }

    [FFGmuIdAppIdMapping(typeof(FF_AppId_AccountingMeterIds))]
    public enum FF_GmuId_AccountingMeterIds
    {
        None = 0,

        // Bet Meters
        GeneralMeters_Plays = 1,
        GeneralMeters_Bets = 2,
        GeneralMeters_Wins = 3,
        GeneralMeters_CoinDrop = 4,

        // Coin Meters

        // Bill Meters
        AcceptorMeters_Bill_1 = 7,
        AcceptorMeters_Bill_2 = 53,
        AcceptorMeters_Bill_5 = 8,
        AcceptorMeters_Bill_10 = 9,
        AcceptorMeters_Bill_20 = 10,
        AcceptorMeters_Bill_25 = 54,
        AcceptorMeters_Bill_50 = 11,
        AcceptorMeters_Bill_100 = 12,
        AcceptorMeters_Bill_200 = 55,
        AcceptorMeters_Bill_250 = 56,
        AcceptorMeters_Bill_500 = 57,
        AcceptorMeters_Bill_1000 = 58,
        AcceptorMeters_Bill_2000 = 59,
        AcceptorMeters_Bill_2500 = 60,
        AcceptorMeters_Bill_5000 = 61,
        AcceptorMeters_Bill_10000 = 62,
        AcceptorMeters_Bill_20000 = 63,
        AcceptorMeters_Bill_25000 = 64,
        AcceptorMeters_Bill_50000 = 65,
        AcceptorMeters_Bill_100000 = 66,
        AcceptorMeters_Bill_200000 = 67,
        AcceptorMeters_Bill_250000 = 68,
        AcceptorMeters_Bill_500000 = 69,
        AcceptorMeters_Bill_1000000 = 70,

        AcceptorMeters_CoinsPurchased = 5,
        AcceptorMeters_CoinsCollected = 6,
        AcceptorMeters_CouponsCount = 13,
        AcceptorMeters_BillCoinOut = 14,

        // Ticket Meters
        TicketIn_Cashable = 19,
        TicketOut_Cashable = 20,
        TicketIn_NonCashable = 21,
        TicketOut_NonCashable = 22,
        TicketIn_Qty_Cashable = 23,
        TicketOut_Qty_Cashable = 24,
        TicketIn_Qty_NonCashable = 25,
        TicketOut_Qty_NonCashable = 26,

        EFTIn_Promo_Cashable = 15,
        EFTOut_Promo_Cashable = 16,
        EFTIn_NonCashable = 17,
        EFTOut_NonCashable = 18,

        SASJPMeters_TotJPHandpayCredits = 27,
        SASJPMeters_TotCancelledCredits = 28,
        SASJPMeters_TotPRHandpayCredits = 29,
        SASJPMeters_TotPRMachpayCredits = 30,
        EFTIn_Cashable = 31,
        EFTOut_Cashable = 32,
        SysGame_Cashable_EFTIn = 33,
        SysGame_NonCashable_EFTIn = 34,
        SysGame_Hand_pay_Jackpot = 35,
        TicketIn_Promo_Cashable = 36,
        TicketIn_Qty_Promo_Cashable = 37,
        Cashless_Used = 38,
        Cashless_State = 39,
        Cashless_Transactions = 40,
        Cashless_Configuration = 41,
        Unrestricted_Win = 42,
        BonusMeters_MachinePaid = 43,
        BonusMeters_AttendantPaid = 44,
        SASJPMeters_CumulativeProgWin = 45,
        AcceptorMeters_Bill_200_1 = 46,
        AcceptorMeters_Bill_500_1 = 47,
        AcceptorMeters_CoinAcceptorCredits = 48,
        AcceptorMeters_HopperPaidDebits = 49,
        AcceptorMeters_Bill_2_1 = 50,
        EFT_Cashable_to_Ticket = 51,
        EFT_Restricted_Promo_to_Ticket = 52,

        Restricted_Bet_Amount = 71,
        Unrestricted_Bet_Amount = 72,
    }
    #endregion

    #region GMU Events

    [FFGmuIdAppIdMapping(typeof(FF_AppId_GMUEvent_DataSetIds))]
    public enum FF_GmuId_GMUEvent_DataSetIds
    {
        None = 0,
        Standard = 1,
        Ticket = 2,
        EFT = 3,
        Coupon = 4,
        Printer = 5,
        NoteAcceptor = 6,
        TaggedStatus = 7
    }

    [FFGmuIdAppIdMapping(typeof(FF_AppId_GMUEvent_XCodes))]
    public enum FF_GmuId_GMUEvent_XCodes
    {
        None = 0,
        NullXC = 0x01,
        TooManyBadPINs = 0x02,
        AcceptorHopperJamXC = 0x03,
        ServiceRequestXC = 0x04,
        ServiceRequestOffXC = 0x05,
        JCMStatusXC = 0x06,
        ProactiveFillXC = 0x07,
        HotPlayerXC = 0x08,
        DivMalfunXC = 0x09,
        GameOptionsChangedXC = 0x0A, // Added for BMC payout percentage change notification requirement
        HopperFullXC = 0x0B, // Added for BMC handle hopper events requirement
        HopperLevelLowXC = 0x0C, // Added for BMC handle hopper events requirement        
        JackpotXC = 0x10,
        PHYDownXC = 0x11, // Added for BMC Machine Disable based on PHY-Link Status
        AbandonedCardXC = 0x12,
        IllegalCardremoval = 0x13,
        BadMagCardReader = 0x14,
        LargeBuyIn = 0x15,
        AcceptorCantvendXC = 0x16,
        ReconfigurationRequestXC = 0x17,
        BillVendXC = 0x18,
        BallDropXC = 0x19, // For the Roulette Ball Drop feature BZ 11240 - Akhil
        PrinterComErrorXC = 0x1A, // Added for BMC handle printer events requirement
        PrinterPaperOutXC = 0x1B, // Added for BMC handle printer events requirement
        PrinterPaperLowXC = 0x1C, // Added for BMC handle printer events requirement
        PrinterPowerOffXC = 0x1D, // Added for BMC handle printer events requirement
        PrinterPowerOnXC = 0x1E, // Added for BMC handle printer events requirement
        PrinterRibbonXC = 0x1F, // Added for BMC handle printer events requirement
        WETOverflowXC = 0x20,
        ChangeRequestXC = 0x21,
        CocktailRequestXC = 0x22,
        ReserveXC = 0x23,
        Call911XC = 0x24,
        PersonalityUpdateRequestXC = 0x25,
        OBSOLETE_XC1 = 0x26, // OBSOLETE was  CashlessConfirmXC
        PrinterJamXC = 0x27,	// Added for BMC handle printer events requirement
        OBSOLETE_XC2 = 0x28, // OBSOLETE was  CashlessRedeemXC  
        HopperFillXC = 0x29,
        ACPowerAppliedXC = 0x2A,	// Added for BMC handle all SAS events requirement
        ACPowerLostXC = 0x2B,	// Added for BMC handle all SAS events requirement
        GenTiltXC = 0x2C,	// Added for BMC handle all SAS events requirement
        CounterfeitBillXC = 0x2D,	// Added for BMC handle all SAS events requirement
        ReverseCoinInXC = 0x2E,	// Added for BMC handle all SAS events requirement
        CashboxNearFullXC = 0x2F,	// Added for BMC handle all SAS events requirement
        CancelledJackpotXC = 0x30,
        IncorrectPayoutXC = 0x31,
        GameMPUOutXC = 0x32,
        GameMPUInXC = 0x33,
        FillDoorOpenedXC = 0x35,
        FillDoorClosedXC = 0x36,
        EmpCardInXC = 0x37,
        EmpCardOutXC = 0x38,
        PlayerCardInInfoXC = 0x39,
        MemoryErrResetXC = 0x3A,	// Added for BMC handle all SAS events requirement
        HandPayValidatedXC = 0x3B,	// Added for BMC handle all SAS events requirement
        ValidationIDNotCfgXC = 0x3C,	// Added for BMC handle all SAS events requirement
        NoProgFor5SecXC = 0x3D,	// Added for BMC handle all SAS events requirement
        ProgLevelHitXC = 0x3E,	// Added for BMC handle all SAS events requirement
        XCBuffOverflowXC = 0x3F,	// Added for BMC handle all SAS events requirement
        GameMPUResetXC = 0x40,
        Tilt41XC = 0x41,
        Tilt42XC = 0x42,
        Tilt43XC = 0x43,
        Tilt44XC = 0x44,
        Tilt45XC = 0x45,
        BackInPlayXC = 0x46,
        Tilt47XC = 0x47,
        Tilt48XC = 0x48,
        BillValidatorRstXC = 0x49,	// Added for BMC handle all SAS events requirement
        MultipleJackpotXC = 0x4A,	// Added for BMC handle all SAS events requirement
        AttMenuEnterXC = 0x4B,	// Added for BMC handle all SAS events requirement
        AttMenuExitXC = 0x4C,	// Added for BMC handle all SAS events requirement
        SelfTestEnterXC = 0x4D,	// Added for BMC handle all SAS events requirement
        SelfTestExitXC = 0x4E,	// Added for BMC handle all SAS events requirement
        OutOfServiceXC = 0x4F,	// Added for BMC handle all SAS events requirement
        NoCardDataXC = 0x50,
        GMUWatchdogXC = 0x51,
        JackpotResetXC = 0x52,
        Tilt54XC = 0x54,
        GMUMalfunctionXC = 0x55,
        GMUPowerResetXC = 0x56,
        WinWithNoHandlePull = 0x57,
        WinWithNoCoinIn = 0x58,
        Tilt59XC = 0x59,
        ForcedPeriodicXC = 0x60,
        PeriodicXC = 0x61,
        BlackOutXC = 0x62,
        MachinePayJPXC = 0x63,
        Tilt64XC = 0x64,
        EPIServiceRequestXC = 0x65,
        AcceptorRemovedXC = 0x66,
        StackerFullXC = 0x67,
        StackerJammedXC = 0x68,
        AcceptorNotRespondingXC = 0x69,
        VendMalFixedXC = 0x70,
        SlotDoorOpenedXC = 0x71,
        SlotDoorClosedXC = 0x72,
        DropDoorOpenedXC = 0x73,
        DropDoorClosedXC = 0x74,
        VendDoorOpenedXC = 0x75,
        VendDoorClosedXC = 0x76,
        PlayerCardInXC = 0x77,
        PlayerCardOutXC = 0x78,
        StackerRemovedXC = 0x79,
        StackerReInsertedXC = 0x80,
        Tilt81XC = 0x81,
        Tilt82XC = 0x82,
        Tilt83XC = 0x83,
        Tilt84XC = 0x84,
        Tilt85XC = 0x85,
        TooManyBillsRejected = 0x86,
        VendRamMalXC = 0x87,
        BadCardReadsXC = 0x88,
        CreditVendXC = 0x89,
        Tilt90XC = 0x90,
        Tilt91XC = 0x91,
        BillJammedXC = 0x92,
        Tilt93XC = 0x93,
        GameMetersClearedXC = 0x94,
        Tilt95XC = 0x95,
        CashDoorOpenedXC = 0x96,
        CashDoorClosedXC = 0x97,
        GMUMetersResetXC = 0x98,
        EmpServiceEntryXC = 0x99,
        InfoRequestXC = 0xA0,
        InvalidCoinInXC = 0xA1,
        EmployeeKeySequence = 0xA2,
        GameDispComErrXC = 0xA3,
        TouchScreenFaultXC = 0xA4,
        LowRamBatteryXC = 0xA5,
        EPROMSigFailXC = 0xA6,
        MPUDoorOpenXC = 0xA7,
        MPUDoorClosedXC = 0xA8,
        GMUDoorOpenXC = 0xA9,
        GMUDoorClosedXC = 0xAA,
        SlotResetXC = 0xAB,
        GameComLostXC = 0xAC,
        GameComRestoredXC = 0xAD,
        NewGameSelectXC = 0xAE,
        SlotPrinterFaultXC = 0xB0,
        CancelledCreditXC = 0xB1,
        StartCardlessPlayXC = 0xB2,
        EndCardlessPlayXC = 0xB3,
        ServiceClearedXC = 0xB4,
        IntervalRatingXC = 0xB5,
        GMUIntrepidized = 0xB6,
        FreeformResponse = 0xB7,
        FreeformTransportNAK = 0xB8,
        FreeformXC = 0xB9,
        AcceptorSWChangeXC = 0xBA,
        AcceptorSWChangeVerifyXC = 0xBB,
        GMUInitiatedFreeformMessage = 0xBC,
        TicketPrintedXC = 0xBD,
        TicketRedeemXC = 0xBE,
        TicketRedeemCRCFailureXC = 0xBF,
        SCWithdrawalReqXC = 0xC0,
        SCWithdrawalPostedXC = 0xC1,
        SCScrewupXC = 0xC2,
        SCDepositReqXC = 0xC3,
        SCBalanceReqXC = 0xC4,
        PwrCardRdmRqstXC = 0xC5,
        PwrCardRdmCnfrmXC = 0xC6,
        FF2ReTxMessageXC = 0xC7,
        POffCardCageAccessXC = 0xC8, // Added for BMC VLT Battery Backup Requirement
        POffSlotDoorAccessXC = 0xC9, // Added for BMC VLT Battery Backup Requirement
        POffCashBoxAccessXC = 0xCA, // Added for BMC VLT Battery Backup Requirement
        POffDropDoorAccessXC = 0xCB, // Added for BMC VLT Battery Backup Requirement
        MPBonusWinXC = 0xCC, // Added for BMC Bonus Win Meter Requirement
        ZeroCurCreditsXC = 0xCD, // Added for BMC Zero Current Credits Requirement
        APBonusWinXC = 0xCE, // Added for BMC Bonus Win Meter Requirement
        InstantPeriodicXC = 0xCF, // Added for BMC Instant-Periodic Requirement
        CompListChangedXC = 0xD0, // Added for Component Authentication Requirement
        OffLineTktPrntXC = 0xD1, // Added for BMC Requirement - FF XC For Offline Ticket Print
        ZeroToCreditsXC = 0xD2, // Added for BMC Requirement - Zero To Credit
        HeartBeatXC = 0xD3, // Added for BMC Requirement - Heart Beat
        PCIntervalRatingXC = 0xD4, // Added for BMC Requirement - Pre-Commitment
        NewGameChangeXC = 0xD5, // Added for BMC Requirement - Game Session Management

        // Extra 
        EXTRA_ZeroCurCreditsXC = 0xE0,
    }

    [FFGmuIdAppIdMapping(typeof(FF_AppId_GMUEvent_PrinterStatusCodes))]
    public enum FF_GmuId_GMUEvent_PrinterStatusCodes
    {
        PrinterFault = 0,
        PaperOut = 1,
        PaperJam = 2,
        PaperLow = 3,
        PrinterOnlineReady = 4,
        PrinterBusy = 5,
        PrinterInChute = 6,
        PrinterNotAtTopOfForm = 7,
    }

    [FFGmuIdAppIdMapping(typeof(FF_AppId_GMUEvent_JackpotIDs))]
    public enum FF_GmuId_GMUEvent_JackpotIDs
    {
        NoJackpot = 0x0,
        UnknownHandpay = 0x1,
        UnknownProgressive = 0x96,
        ProgressiveJackpot1 = 0x91,
        ProgressiveJackpot2 = 0x92,
        ProgressiveJackpot3 = 0x93,
        ProgressiveJackpot4 = 0x94,
        ProgressiveJackpot5 = 0x95,
        ProgressiveJackpot6 = 0x97,
        ProgressiveJackpot7 = 0x98,
        ProgressiveJackpot8 = 0x99,
        LargeWin = 0xF8,
        MysteryWinToCard = 0xF9,
        SystemGameJackpot = 0xFA,
        MysteryWinToGame = 0xFB,
        Handpay = 0xFC,
        CelebrationLockup = 0xFD,
        CancelledCredit = 0xFE,
    }

    [FFGmuIdAppIdMapping(typeof(FF_AppId_GMUEvent_TaggedStatusIds))]
    public enum FF_GmuId_GMUEvent_TaggedStatusIds
    {
        None = 0,
        Date = 28,
        Time = 29,
    }

    #endregion

    #region EFT Transactions

    [FFGmuIdAppIdMapping(typeof(FF_AppId_EFT_ResponseTypes))]
    public enum FF_GmuId_EFT_ResponseTypes
    {
        Nack = 0,
        Ack = 1,
    }

    /// <summary>
    /// EFT Transactions Types
    /// </summary>
    [FFGmuIdAppIdMapping(typeof(FF_AppId_EFT_TransactionTypes))]
    public enum FF_GmuId_EFT_TransactionTypes
    {
        WithdrawalRequest = 1,
        WithdrawalAuthorization = 2,
        WithdrawalComplete = 3,
        WithdrawalAcknowledgement = 4,
        DepositRequest = 5,
        DepositAuthorization = 6,
        DepositComplete = 7,
        DepositAcknowledgement = 8,
        BalanceRequest = 9,
        BalanceResponse = 10,
        SystemEnableEFT = 11,
        SystemDisableEFT = 12,
        PlayerEnableEFT = 13,
        OfferListRequest = 14,
        OfferListReply = 15,
        PINCheckRequest = 16,
        PINCheckReply = 17,
        OfferRequest = 18,
        WithdrawalAuthorization2 = 19,
        Phishing = 20,
        CashlessAccountLookup = 21,
        CashlessAccountVerify = 22,
        CashlessAccountAccountNumber = 23,
        iButtonAccountNumber = 24,
        SmartcardVerifyBalance = 25,
        SmartcardTransactionUpdate = 26,
        SmartcardSerialNumber = 27,
        PINChangeRequest = 28,
        PINChangeResponse = 29,
        AutoDownload_TopUpStatusRequest = 30,
        AutoDownload_TopUpStatusResponse = 31,
        AutoDownloadSet_EnableAmountRqst = 32,
        AutoDownloadSet_EnableAmountResp = 33,
        AutoTopUpSet_EnableAmountRqst = 34,
        AutoTopUpSet_EnableAmountResp = 35,
        iButtonAccountNumberResponse = 36
    }

    /// <summary>
    /// Account Types Withdrawal Request
    /// </summary>
    [FFGmuIdAppIdMapping(typeof(FF_AppId_EFT_AccountTypes))]
    public enum FF_GmuId_EFT_AccountTypes
    {
        PromotionalOffer = 1,
        PointsRedemption = 2,
        PlayerCash = 3,
        Offers = 4,
        All = 5
    }

    /// <summary>
    /// Auto Download TopUp Status Flag
    /// </summary>
    [FFGmuIdAppIdMapping(typeof(FF_AppId_EFT_AutoDownload_TopUp_StatusFlags))]
    public enum FF_GmuId_EFT_AutoDownload_TopUp_StatusFlags
    {
        AutoDownloadEnabled = 0x01,
        PINRequiredOnAutoDownload = 0x02,
        AutoTopUpEnabled = 0x10,
        PINrequiredOnTopUp = 0x20
    }

    /// <summary>
    /// Auto Download/TopUp Account Type
    /// </summary>
    [FFGmuIdAppIdMapping(typeof(FF_AppId_EFT_AutoDownload_TopUp_AccountTypes))]
    public enum FF_GmuId_EFT_AutoDownload_TopUp_AccountTypes
    {
        PromotionalOffer = 1,
        PointsRedemption = 2,
        PlayerCash = 3,
        Offers = 4,
        All = 5
    }

    /// <summary>
    /// Smartcard Verify Balance to System Status
    /// </summary>
    [FFGmuIdAppIdMapping(typeof(FF_AppId_EFT_BalanceVerify_Status))]
    public enum FF_GmuId_EFT_BalanceVerify_Status
    {
        Balance1Verified = 0x01,
        Balance2Verified = 0x02,
        Balance3Verified = 0x04,
        Balance4Verified = 0x08,
        Balance1NotVerified = 0x10,
        Balance2NotVerified = 0x20,
        Balance3NotVerified = 0x40,
        Balance4NotVerified = 0x80,
        Balance1NotUsed = 0x11,
        Balance2NotUsed = 0x22,
        Balance3NotUsed = 0x44,
        Balance4NotUsed = 0x88
    }

    /// <summary>
    /// Smart-Card Transaction Update
    /// </summary>
    [FFGmuIdAppIdMapping(typeof(FF_AppId_EFT_SC_Tranaction_Update_Status))]
    public enum FF_GmuId_EFT_SC_Tranaction_Update_Status
    {
        NotProcessed = 0,
        Deposit = 1,
        Withdrawal = 2,
        Processed = 3
    }

    /// <summary>
    /// Smart-Card Transaction Update Account Type
    /// </summary>
    [FFGmuIdAppIdMapping(typeof(FF_AppId_EFT_SC_Tranaction_Update_AccTypes))]
    public enum FF_GmuId_EFT_SC_Tranaction_Update_AccTypes
    {
        RestrictedPromotional = 1,
        Non_Restrictedpromotional = 2,
        PlayerCash = 3,
        WAT = 4
    }

    /// <summary>
    /// Auto-Download Enable/Set/Change Amount Request Status
    /// </summary>
    [FFGmuIdAppIdMapping(typeof(FF_AppId_EFT_AutoDownload_Status))]
    public enum FF_GmuId_EFT_AutoDownload_Status
    {
        AutoDownloadEnabled = 0x01,
        RequirePINOnAutoDownload = 0x02
    }

    /// <summary>
    /// Auto-TopUp Enable/Set/Change Amount Request Status
    /// </summary>
    [FFGmuIdAppIdMapping(typeof(FF_AppId_EFT_AutoTopUp_Status))]
    public enum FF_GmuId_EFT_AutoTopUp_Status
    {
        AutoDownloadEnabled = 0x10,
        RequirePINOnAutoDownload = 0x20
    }

    /// <summary>
    /// Pin Check
    /// </summary>
    [FFGmuIdAppIdMapping(typeof(FF_AppId_EFT_PINCheck))]
    public enum FF_GmuId_EFT_PINCheck
    {
        PINNotRequired = 0x00,
        PINRequired = 0x01
    }

    #endregion EFT Transactions

    #region GIM
    [FFGmuIdAppIdMapping(typeof(FF_AppId_GIM_SubTargets))]
    public enum FF_GmuId_GIM_SubTargets
    {
        None = 0,
        GameIDInfo = 1,
        AuxNetworkEnableDisable = 2,
        GameIDRequest = 3,
    }

    [FFGmuIdAppIdMapping(typeof(FF_AppId_GIM_GameIDInfoTags))]
    public enum FF_GmuId_GIM_GameIDInfoTags
    {
        None = 0,
        GMUGameNumber = 1,
        GameGameNumber = 2,
        ManufacturerID = 3,
        SerialNumber = 4,
        MACAddress = 5,
        SASVersion = 6,
        GMUVersion = 7
    }

    [FFGmuIdAppIdMapping(typeof(FF_AppId_GIM_GameIDInfo_H2G))]
    public enum FF_GmuId_GIM_GameIDInfo_H2G
    {
        None = 0,
        DisplayMessage = 2,
        IPAddress = 3,
        AssetNumber = 4,
        PokerGamePrefix = 5,
    }

    #endregion

    #region Code Download

    [FFGmuIdAppIdMapping(typeof(FF_AppId_CodeDownloadOptions))]
    public enum FF_GmuId_CodeDownloadOptions
    {
        ChangeBaudRate = 1,
        BaudRateTest = 2,
        BaudRateTestResponse = 3,
        GMUDataRequest = 4,
        GMUDataResponse = 5,
        ChangeVersion = 6,
        FileDownload = 7,
        CRCResults = 8
    }

    [FFGmuIdAppIdMapping(typeof(FF_AppId_FileDownloadFileType))]
    public enum FF_GmuId_FileDownloadFileType
    {
        ImageFile = 1,
        AuthenticationFile = 2,
        Optionsfile = 3
    }
    #endregion

    #region Authentication

    [FFGmuIdAppIdMapping(typeof(FF_AppId_GMUAuthentication))]
    public enum FF_GmuId_GMUAuthentication
    {
        None = 0,
        Initiate = 1,
        Results = 2,
        Query = 3,
        Status = 4,
        SHA1Request = 5,
        SHA1Response = 6,
    }

    [FFGmuIdAppIdMapping(typeof(FF_AppId_GMUAuthentication_AuthStatusTypes))]
    public enum FF_GmuId_GMUAuthentication_AuthStatusTypes
    {
        Done = 0,
        StillProcessing = 1,
        NotStarted = 2,
        BootCRCFailed = 3
    }

    #endregion

    #region EPI MESSAGE

    [FFGmuIdAppIdMapping(typeof(FF_AppId_SystemToEPI_MessageTypes))]
    public enum FF_GmuId_SystemToEPI_MessageTypes
    {
        None = 0,
        TicketPrint = 0x01,
        TicketRedeem = 0x02,
        TicketError = 0x03,
        Promo = 0xF2,
        Sports = 0xF3,
        F5 = 0xF5,
    }

    [FFGmuIdAppIdMapping(typeof(FF_AppId_SystemToEPI_MessageAction1))]
    public enum FF_GmuId_SystemToEPI_MessageAction1
    {
        Solid = 0x00,
        Blink = 0x01,
        Scroll = 0x02
    }

    [FFGmuIdAppIdMapping(typeof(FF_AppId_SystemToEPI_MessageAction2))]
    public enum FF_GmuId_SystemToEPI_MessageAction2
    {
        ReplaceCode = 0x01
    }

    #endregion

    #region Printer

    [FFGmuIdAppIdMapping(typeof(FF_AppId_SystemPrinter))]
    public enum FF_GmuId_SystemPrinter
    {
        PrintString = 1,
        PrintStringEnd = 2,
        SetPrintCompValue = 3,
        ComVoucherRequest = 4,
        PrintJobCancel = 5,
        PrinterStatus = 6
    }

    [FFGmuIdAppIdMapping(typeof(FF_AppId_SystemPrinter_PrinterResponse))]
    public enum FF_GmuId_SystemPrinter_PrinterResponse
    {
        Printjobsuccessful = 0x11,
        Paperout = 0x12,
        Undefined = 0x13,
        Paperlow = 0x14,
        Printerpaperjam = 0x15

    }

    [FFGmuIdAppIdMapping(typeof(FF_AppId_SystemPrinter_PrinterStatus))]
    public enum FF_GmuId_SystemPrinter_PrinterStatus
    {
        DefaultGenericprinterfault = 0,
        PaperOut = 1,
        PaperJam = 2,
        PaperLow = 3,
        PrinterOnlineReady = 4,
        PrinterBusy = 5,
        PaperinChute = 6,
        PaperNotatTopofForm = 7
    }

    #endregion

    #region Application Qualifier

    [FFGmuIdAppIdMapping(typeof(FF_AppId_ApplicationQualifier))]
    public enum FF_GmuId_ApplicationQualifier
    {
        PlayerCardId = 0x01,
        PlayerCardPresent = 0x02,
        Unknown = 0,
    }

    #endregion

    #region Employee CardIn
    [FFGmuIdAppIdMapping(typeof(FF_AppId_EmployeeCard_Types))]
    public enum FF_GmuId_EmployeeCard_Types
    {
        EmployeeCardIn = 0,
    }

    [FFGmuIdAppIdMapping(typeof(FF_AppId_EmployeeCard_G2H_Actions))]
    public enum FF_GmuId_EmployeeCard_G2H_Actions
    {
        EmployeeCardIn = 1,
    }

    [FFGmuIdAppIdMapping(typeof(FF_AppId_EmployeeCard_H2G_Actions))]
    public enum FF_GmuId_EmployeeCard_H2G_Actions
    {
        EmployeeInformation = 1,
        EmployeeMainMenusToDisable = 2,
        Employee1stLevelSubMenustoDisable = 3
    }

    [FFGmuIdAppIdMapping(typeof(FF_AppId_EmployeeCard_ResponseStatus))]
    public enum FF_GmuId_EmployeeCard_ResponseStatus
    {
        ValidEmployeeCard = 0xA0,
        InvalidEmployeeCard = 0xB0,
    }
    #endregion

    #region PowerCard
    [FFGmuIdAppIdMapping(typeof(FF_AppId_PowerCardTypes))]
    public enum FF_GmuId_PowerCardTypes
    {
        BalanceInquiry = 1,
        BalanceInquiryResponse = 2,
        RedeemStoredValue = 3,
        RedeemResponse = 4,
        RedeemConfirmed = 5,
    }

    [FFGmuIdAppIdMapping(typeof(FF_AppId_PowerCard_RedeemErrorCode))]
    public enum FF_GmuId_PowerCard_RedeemErrorCode
    {
        Success = 0,
        Error = 1
    }
    #endregion

    #region Credit Key Off
    [FFGmuIdAppIdMapping(typeof(FF_AppId_CreditKeyOff_G2H_Types))]
    public enum FF_GmuId_CreditKeyOff_G2H_Types
    {
        AuthorizationRequest = 01,
    }

    [FFGmuIdAppIdMapping(typeof(FF_AppId_CreditKeyOff_H2G_Types))]
    public enum FF_GmuId_CreditKeyOff_H2G_Types
    {
        NotAuthorized = 0,
        AuthorizationApproved = 01,
    }
    #endregion

    #region Default IO
    [FFGmuIdAppIdMapping(typeof(FF_AppId_DefaultIO_Types))]
    public enum FF_GmuId_DefaultIO_Types
    {
        DisplayText = 01,
        PlayerQuery = 02,
        SetLockout = 03,
        InitiateQuestion = 04,
        QuestionRequest = 05,
        QuestionOver = 06
    }

    [FFGmuIdAppIdMapping(typeof(FF_AppId_DefaultIO_DisplayText_ResponseStatus))]
    public enum FF_GmuId_DefaultIO_DisplayText_ResponseStatus
    {
        Failure = 0,
        Success = 1
    }

    [FFGmuIdAppIdMapping(typeof(FF_AppId_DefaultIO_SetLockoutTypes))]
    public enum FF_GmuId_DefaultIO_SetLockoutTypes
    {
        LockoutOff = 0,
        LockoutOn = 1
    }

    [FFGmuIdAppIdMapping(typeof(FF_AppId_DefaultIO_QuestionRequest_ResponseStatus))]
    public enum FF_GmuId_DefaultIO_QuestionRequest_ResponseStatus
    {
        FullResponse = 0,
        LessThanFullResponse = 1
    }

    [FFGmuIdAppIdMapping(typeof(FF_AppId_DefaultIO_QuestionRequest_EncryptionStatus))]
    public enum FF_GmuId_DefaultIO_QuestionRequest_EncryptionStatus
    {
        NoEncryption = 0,
        EncryptionUsed = 1
    }
    #endregion

    #region PreCommitment

    /// <summary>
    /// PreCommitment Action
    /// </summary>
    [FFGmuIdAppIdMapping(typeof(FF_AppId_PreCommitment_Action))]
    public enum FF_GmuId_PreCommitment_Action
    {
        Status = 1,
        CardIn = 2,
        ApproachingLimit = 3,
        LimitReached = 4,
        RelaxedLimit = 5,
        NotificationResponseAck = 6,
        EnrollmentParam = 7,
        EnrollmentResponse = 8
    }

    [FFGmuIdAppIdMapping(typeof(FF_AppId_PreCommitment_LockType))]
    public enum FF_GmuId_PreCommitment_LockType
    {
        NoLock = 0,
        TemporaryLock = 1,
        HardLock = 2
    }

    [FFGmuIdAppIdMapping(typeof(FF_AppId_PreCommitmentAcknowledgementTypes))]
    public enum FF_GmuId_PreCommitmentAcknowledgementTypes
    {
        InvalidNotification = 1,
        ApproachingNotification = 2,
        LimitsReachedNotification = 3,
        BreakPeriodAccepted = 4,
        BreakPeriodRejected = 5,
        ChangeLimitsAccepted = 6,
        ChangeLimitsRejected = 7
    }

    #endregion //PreCommitment

    //#region GMU Variable Action

    //[FFGmuIdAppIdMapping(typeof(FF_GmuId_GmuVarAction_Types))]
    //public enum FF_GmuId_GmuVarAction_Types
    //{
    //    CardlessPlayTimeout = 1,
    //}

    ///// <summary>
    ///// Enable Printing of Restricted Tickets
    ///// </summary>
    //public enum FF_GmuId_GmuVarAction_EnablePrint_RT
    //{
    //    Dont_Allow_Printing_RT = 0,
    //    Allow_Printing_RT = 1,
    //}

    //#endregion //GMU Variable Action    

    //#region Player Data

    ///// <summary>
    ///// Cap Enabled
    ///// </summary>
    //public enum FF_Cap_Status
    //{
    //    Disabled = 0,
    //    Enabled = 1
    //}

    ///// <summary>
    ///// Max Cap Not Exceeded
    ///// </summary>
    //public enum FF_Max_Cap_Exceeded
    //{
    //    NotExceeded = 0,
    //    Exceeded = 1
    //}

    ///// <summary>
    ///// Self Cap
    ///// </summary>
    //public enum FF_Self_Cap
    //{
    //    NoselfCap = 0,
    //    SelfCap = 1
    //}

    ///// <summary>
    ///// Cap Allowed or not
    ///// </summary>
    //public enum FF_Cap_Allowed
    //{
    //    NotAllowed = 0,
    //    Allowed = 1
    //}

    ///// <summary>
    ///// Auto Release
    ///// </summary>
    //public enum FF_Auto_Release
    //{
    //    NoAutoRelease = 0,
    //    AutoRelease = 1
    //}

    ///// <summary>
    ///// Valid Pin
    ///// </summary>
    //public enum FF_Valid_Pin
    //{
    //    ValidPinNotRequired = 0,
    //    ValidPinRequired = 1
    //}

    //#endregion //Player Data

    #region Jackpot Slip Processing

    /// <summary>
    /// Prompt for Jackpot amount
    /// </summary>
    public enum FF_GmuId_JPS_Prompt
    {
        DontPromptForJPAmt = 0,
        PromptForJPAmt = 1
    }

    /// <summary>
    /// Pay line
    /// </summary>
    public enum FF_GmuId_JPS_PayLine
    {
        DontPromptForPayLine = 0,
        PromptForPayLine = 1
    }

    /// <summary>
    /// Winning Combination
    /// </summary>
    public enum FF_GmuId_JPS_WinningCombination
    {
        DontPromptForWinningCombination = 0,
        PromptForWinningCombination = 1
    }

    /// <summary>
    /// Coins played
    /// </summary>
    public enum FF_GmuId_JPS_CoinsPlay
    {
        DontPromptForCoinsPlayed = 0,
        PromptForCoinsPlayed = 1
    }

    /// <summary>
    /// Shift
    /// </summary>
    public enum FF_GmuId_JPS_Shift
    {
        DontPromptForShift = 0,
        PromptForShift = 1
    }

    /// <summary>
    /// Pouch Pay
    /// </summary>
    public enum FF_GmuId_JPS_PouchPay
    {
        DontPromptForPouchPay = 0,
        PromptForPouchPay = 1
    }

    /// <summary>
    /// Employee Authorization
    /// </summary>
    public enum FF_GmuId_JPS_Emp_Auth
    {
        DontPromptForEmployeeAuthorization = 0,
        PromptForEmployeeAuthorization = 1
    }

    /// <summary>
    /// Shift Data
    /// </summary>
    public enum FF_GmuId_JPS_Shift_Data
    {
        Day = 1,
        Swing = 2,
        Graveyard = 3
    }

    /// <summary>
    /// Clear Jackpot Flag
    /// </summary>
    public enum FF_GmuId_ClearJP
    {
        DontClearJP = 0,
        ClearJP = 1
    }

    #endregion Jackpot Slip Processing

    #region Remote Shutdown

    [FFGmuIdAppIdMapping(typeof(FF_AppId_RemoteShutdown_RequestTypes))]
    public enum FF_GmuId_RemoteShutdown_RequestTypes
    {
        SendStatus = 0,
        StartGame = 1,
        ShutdownGameWithAutoDeposit = 2,
        ShutdownGameWithNoAutoDeposit = 3,
        SiteEnableDisable = 4,
    }

    [FFGmuIdAppIdMapping(typeof(FF_AppId_RemoteShutdown_RequestGameStatus))]
    public enum FF_GmuId_RemoteShutdown_RequestGameStatus
    {
        GameDisabled = 0,
        GameEnabled = 1,
    }

    [FFGmuIdAppIdMapping(typeof(FF_AppId_RemoteShutdown_ResponseTypes))]
    public enum FF_GmuId_RemoteShutdown_ResponseTypes
    {
        GameStatusReturn = 0,
        AckingStartup = 1,
        AckingShutdown = 2,
        SiteEnableDisableScheduleAck = 3,
        SiteEnableDisableScheduleRequest = 4,
    }

    [FFGmuIdAppIdMapping(typeof(FF_AppId_RemoteShutdown_ResponseStatus))]
    public enum FF_GmuId_RemoteShutdown_ResponseStatus
    {
        NotEngaged = 0,
        WaitingForCreditRemoval = 1,
        WaitingForAutoDeposit = 2,
        GameShutdown = 3,
        WaitingForGameToRespondStartupCommand = 4,
        WaitingForGameToRespondShutdownCommand = 5,
        OverridingNotAllowed = 6,
        GameOffline = 7,
        SiteEnableDisableScheduleRequest = 8,
        GameStarted = 9,
    }

    #endregion

    #region Features Support

    [FFGmuIdAppIdMapping(typeof(FF_AppId_FeatureSupports_Types))]
    public enum FF_GmuId_FeatureSupports_Types
    {
        BMCFeaturesSupport = 1,
        GMUFeaturesSupport = 2,
    }

    #endregion

    #region Game Message

    [FFGmuIdAppIdMapping(typeof(FF_AppId_GameMessage_ResponseTypes))]
    public enum FF_GmuId_GameMessage_ResponseTypes
    {
        Nack = 0,
        Ack = 1,
    }

    [FFGmuIdAppIdMapping(typeof(FF_AppId_GameMessage_ProtocolTypes))]
    public enum FF_GmuId_GameMessage_ProtocolTypes
    {
        SAS = 1,
        SimpleSerial = 2,
        BOB = 3,
    }

    [FFGmuIdAppIdMapping(typeof(FF_AppId_GameMessage_GameResponses))]
    public enum FF_GmuId_GameMessage_GameResponses
    {
        Ignore = 0,
        Return = 1,
    }

    #endregion

    [FFGmuIdAppIdMapping(typeof(FF_AppId_PrintRestrictedTicket))]
    public enum FF_GmuId_PrintRestrictedTicket
    {
        Disable = 0,
        Enable = 1
    }

    [FFGmuIdAppIdMapping(typeof(FF_AppId_PIDTypes))]
    public enum FF_GmuId_PIDTypes
    {
        None = 0,
        GetParameterRequest = 1,
        GetParameterResponse = 2,
        SetParameterRequest = 3,
        SetParameterResponse = 4,
        RSA_QuerySystem = 0xF,
        RSA_QueryGMU = 0x10,
    }

    [FFGmuIdAppIdMapping(typeof(FF_AppId_PID_ParameterIds))]
    public enum FF_GmuId_PID_ParameterIds
    {
        None = 0,
        RSA_QuerySystem = 0xF,
        RSA_QueryGMU = 0x10,
    }
}
