using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    public enum FF_AppId_Messages
    {
        Message1 = 0,
        Message2 = 1,
        Message3 = 2
    }

    public enum FF_AppId_Encryption_Types
    {
        None = 0,
        Standard = 1,
        AuthByteClearData = 2,
        AuthByteEncryptedData = 3
    }

    // Gmu 2 Net Message Types
    public enum FF_AppId_G2H_MessageTypes
    {
        None = 0,
        FullExtensions = 100,
        FreeForm = 101,
        UnifiedGMU = 102,
        ECash = 103,
        MeterConverted = 104,
        MeterWithJackpot = 105,
        MeterWithPlayer = 106,
        MeterFullParkPlace = 107,
        Coupons = 108,
        MeterFull = 109,
        MeterFullWithValidator = 110,
    };

    // Gmu 2 Net Commands
    public enum FF_AppId_G2H_Commands
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

    public enum FF_AppId_H2G_ResponseCodes
    {
        None = 0,
        ACK = 1,
        NACK = 2,
    }

    // Poll Codes
    public enum FF_AppId_H2G_PollCodes
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

    // Session Ids
    public enum FF_AppId_SessionIds
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
        //Dialog = 100,
        //Security = 101,
        //ECash = 102,
        //A1 = 103,
        //Jackpot = 104,
        //GIM = 105,
        //PlayerCapping = 106,
        //RouletteBallDrop = 107,
        //PWRCard = 108,
        //Game = 109,
        //JackpotKeypadProcessing = 110,
        //Precommitment = 116,
        //Ticket = 111,
        //EncryptedTicket = 112,
        //EncryptedECash = 113,
        //GameMessage = 114,
        //DirectMessageMin = 115,
        //DirectMessageMax = 116,
        ExCommsServer = 513,
    };

    public enum FF_AppId_TargetIds
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

    public enum FF_AppId_Acknowledgements
    {
        General = 100,
        Freeform = 101
    }

    #region GIM
    public enum FF_AppId_GIM_SubTargets
    {
        None = 0,
        GameIDInfo = 1,
        AuxNetworkEnableDisable = 2,
        GameIDRequest = 3,
    }

    public enum FF_AppId_GIM_GameIDInfoTags
    {
        None = 0,
        GMUGameNumber = 101,
        GameGameNumber = 102,
        ManufacturerID = 103,
        SerialNumber = 104,
        MACAddress = 105,
        SASVersion = 106,
        GMUVersion = 107
    }

    public enum FF_AppId_GIM_GameIDInfo_H2G
    {
        None = 0,
        DisplayMessage = 100,
        IPAddress = 101,
        AssetNumber = 102,
        PokerGamePrefix = 103,
    }

    #endregion

    #region GMU Event
    public enum FF_AppId_GMUEvent_DataSetIds
    {
        Standard = 101,
        Ticket = 102,
        EFT = 103,
        Coupon = 104,
        Printer = 105,
        NoteAcceptor = 106,
        TaggedStatus = 107
    }

    public enum FF_AppId_GMUEvent_XCodes
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

    public enum FF_AppId_GMUEvent_PrinterStatusCodes
    {
        PrinterFault = 100,
        PaperOut = 101,
        PaperJam = 102,
        PaperLow = 103,
        PrinterOnlineReady = 104,
        PrinterBusy = 105,
        PrinterInChute = 106,
        PrinterNotAtTopOfForm = 107,
    }

    public enum FF_AppId_GMUEvent_JackpotIDs
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
        MysteryWintoCard = 0xF9,
        SystemGameJackpot = 0xFA,
        MysteryWintoGame = 0xFB,
        Handpay = 0xFC,
        CelebrationLockup = 0xFD,
        CancelledCredit = 0xFE,
    }

    public enum FF_AppId_GMUEvent_TaggedStatusIds
    {
        None = 0,
        Date = 28,
        Time = 29,
    }

    #endregion

    #region Security

    /// <summary>
    /// Security Encryption Types
    /// </summary>
    public enum FF_AppId_Security_Encryption_Types
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

    public enum FF_AppId_KeyExchange
    {
        Ticket = 0,
        ECash = 1,
        FrontEnd = 2,
        Dialog = 3
    }

    #endregion //FF_Security_Encryption_Types

    #region EFT Transactions

    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Freeform",
         Name = "FF_AppId_EFT_ResponseTypes")]
    public enum FF_AppId_EFT_ResponseTypes
    {
        [EnumMember]
        Nack = 0,
        [EnumMember]
        Ack = 1,
    }

    /// <summary>
    /// EFT Transactions Types
    /// </summary>
    public enum FF_AppId_EFT_TransactionTypes
    {
        WithdrawalRequest = 101,
        WithdrawalAuthorization = 102,
        WithdrawalComplete = 103,
        WithdrawalAcknowledgement = 104,
        DepositRequest = 105,
        DepositAuthorization = 106,
        DepositComplete = 107,
        DepositAcknowledgement = 108,
        BalanceRequest = 109,
        BalanceResponse = 110,
        SystemEnableEFT = 111,
        SystemDisableEFT = 112,
        PlayerEnableEFT = 113,
        OfferListRequest = 114,
        OfferListReply = 115,
        PINCheckRequest = 116,
        PINCheckReply = 117,
        OfferRequest = 118,
        WithdrawalAuthorization2 = 119,
        Phishing = 120,
        CashlessAccountLookup = 121,
        CashlessAccountVerify = 122,
        CashlessAccountAccountNumber = 123,
        iButtonAccountNumber = 124,
        SmartcardVerifyBalance = 125,
        SmartcardTransactionUpdate = 126,
        SmartcardSerialNumber = 127,
        PINChangeRequest = 128,
        PINChangeResponse = 129,
        AutoDownload_TopUpStatusRequest = 130,
        AutoDownload_TopUpStatusResponse = 131,
        AutoDownloadSet_EnableAmountRqst = 132,
        AutoDownloadSet_EnableAmountResp = 133,
        AutoTopUpSet_EnableAmountRqst = 134,
        AutoTopUpSet_EnableAmountResp = 135,
        iButtonAccountNumberResponse = 136
    }

    /// <summary>
    /// Account Types Withdrawal Request
    /// </summary>
    public enum FF_AppId_EFT_AccountTypes
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
    public enum FF_AppId_EFT_AutoDownload_TopUp_StatusFlags
    {
        AutoDownloadEnabled = 0x01,
        PINRequiredOnAutoDownload = 0x02,
        AutoTopUpEnabled = 0x10,
        PINrequiredOnTopUp = 0x20
    }

    /// <summary>
    /// Auto Download/TopUp Account Type
    /// </summary>
    public enum FF_AppId_EFT_AutoDownload_TopUp_AccountTypes
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
    public enum FF_AppId_EFT_BalanceVerify_Status
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
    public enum FF_AppId_EFT_SC_Tranaction_Update_Status
    {
        NotProcessed = 0,
        Deposit = 1,
        Withdrawal = 2,
        Processed = 3
    }

    /// <summary>
    /// Smart-Card Transaction Update Account Type
    /// </summary>
    public enum FF_AppId_EFT_SC_Tranaction_Update_AccTypes
    {
        RestrictedPromotional = 1,
        Non_Restrictedpromotional = 2,
        PlayerCash = 3,
        WAT = 4
    }

    /// <summary>
    /// Auto-Download Enable/Set/Change Amount Request Status
    /// </summary>
    public enum FF_AppId_EFT_AutoDownload_Status
    {
        AutoDownloadEnabled = 0x01,
        RequirePINOnAutoDownload = 0x02
    }

    /// <summary>
    /// Auto-TopUp Enable/Set/Change Amount Request Status
    /// </summary>
    public enum FF_AppId_EFT_AutoTopUp_Status
    {
        AutoDownloadEnabled = 0x10,
        RequirePINOnAutoDownload = 0x20
    }

    /// <summary>
    /// Pin Check
    /// </summary>
    public enum FF_AppId_EFT_PINCheck
    {
        PINNotRequired = 0x00,
        PINRequired = 0x01
    }

    #endregion EFT Transactions

    /// <summary>
    /// Response Type
    /// </summary>
    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Freeform",
         Name = "FF_AppId_ResponseStatus_Types")]
    public enum FF_AppId_ResponseStatus_Types
    {
        [EnumMember]
        Fail = 0,
        [EnumMember]
        Success = 1
    }

    #region Application Qualifier

    public enum FF_AppId_ApplicationQualifier
    {
        PlayerCardId = 101,
        PlayerCardPresent = 102,
        Unknown = 100,
    }

    #endregion

    public enum FF_AppId_AppResponseConfig
    {
        None = 0,
        PlayerData = 100,
    }

    #region GMU Authentication
    public enum FF_AppId_GMUAuthentication
    {
        None = 0,
        Initiate = 101,
        Results = 102,
        Query = 103,
        Status = 104,
        SHA1Request = 105,
        SHA1Response = 106,
    }

    public enum FF_AppId_GMUAuthentication_AuthStatusTypes
    {
        Done = 100,
        StillProcessing = 101,
        NotStarted = 102,
        BootCRCFailed = 103
    }
    #endregion

    #region AccountMeters
    /// <summary>
    /// Describes Event Meteres Source 1.GMU 2.GAME
    /// </summary>
    public enum FF_AppId_AccountingMetersSourceIds
    {
        Gmu = 1,
        Game = 2
    }

    public enum FF_AppId_AccountingMeterIds
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

    #region Printer

    public enum FF_AppId_SystemPrinter
    {
        PrintString = 101,
        PrintStringEnd = 102,
        SetPrintCompValue = 103,
        ComVoucherRequest = 104,
        PrintJobCancel = 105,
        PrinterStatus = 106
    }

    public enum FF_AppId_SystemPrinter_PrinterResponse
    {
        Printjobsuccessful = 101,
        Paperout = 102,
        Undefined = 103,
        Paperlow = 104,
        Printerpaperjam = 105

    }

    public enum FF_AppId_SystemPrinter_PrinterStatus
    {
        DefaultGenericprinterfault = 100,
        PaperOut = 101,
        PaperJam = 102,
        PaperLow = 103,
        PrinterOnlineReady = 104,
        PrinterBusy = 105,
        PaperinChute = 106,
        PaperNotatTopofForm = 107
    }

    #endregion

    #region EPI MESSAGE

    public enum FF_AppId_SystemToEPI_MessageTypes
    {
        None = 0,
        TicketPrint = 101,
        TicketRedeem = 102,
        TicketError = 103,
        Promo = 104,
        Sports = 105,
        F5 = 106,
    }

    public enum FF_AppId_SystemToEPI_MessageAction1
    {
        Solid = 0x00,
        Blink = 0x01,
        Scroll = 0x02
    }

    public enum FF_AppId_SystemToEPI_MessageAction2
    {
        ReplaceCode = 0x01
    }

    #endregion

    #region Employee CardIn
    public enum FF_AppId_EmployeeCard_Types
    {
        EmployeeCardIn = 100,
    }

    public enum FF_AppId_EmployeeCard_G2H_Actions
    {
        EmployeeCardIn = 1,
    }

    public enum FF_AppId_EmployeeCard_H2G_Actions
    {
        EmployeeInformation = 100,
        EmployeeMainMenusToDisable = 101,
        Employee1stLevelSubMenustoDisable = 102
    }

    public enum FF_AppId_EmployeeCard_ResponseStatus
    {
        ValidEmployeeCard = 100,
        InvalidEmployeeCard = 101,
    }
    #endregion

    #region PowerCard
    public enum FF_AppId_PowerCardTypes
    {
        BalanceInquiry = 101,
        BalanceInquiryResponse = 102,
        RedeemStoredValue = 103,
        RedeemResponse = 104,
        RedeemConfirmed = 105,
    }

    public enum FF_AppId_PowerCard_RedeemErrorCode
    {
        Success = 100,
        Error = 101
    }
    #endregion

    #region Ticket Processing

    /// <summary>
    /// Ticket Message Type Enum
    /// </summary>
    public enum FF_AppId_TicketMessageTypes
    {
        TicketPrinted = 101,
        TicketVoid = 102,
        TicketRedemption = 103,
        TicketRedemptionComplete = 104,
        EnablementRequest = 105,
        EnablementResponse = 106,
        TicketPrintStatusResult = 107,
        OfflineTicketInfo = 108,
        TicketSequenceNumberUpdate = 109
    }

    /// <summary>
    /// Ticket Type
    /// </summary>
    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Freeform",
         Name = "FF_AppId_TicketTypes")]
    [ExCommsMessageKnownType]
    public enum FF_AppId_TicketTypes
    {
        [EnumMember]
        Cashable = 0,
        [EnumMember]
        NonCashable = 1,
        [EnumMember]
        CashablePromo = 2
    }

    /// <summary>
    /// Ticket Void Type
    /// </summary>
    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Freeform",
         Name = "FF_AppId_TicketPrintStatus")]
    [ExCommsMessageKnownType]
    public enum FF_AppId_TicketPrintStatus
    {
        [EnumMember]
        Unknown = 100,
        [EnumMember]
        PaperOut = 101,
        [EnumMember]
        PaperJam = 102,
        [EnumMember]
        PaperLow = 103,
        [EnumMember]
        PrinterCommunicationsFailure = 104
    }

    /// <summary>
    /// Ticket Redemption CLose Status
    /// </summary>
    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Freeform",
         Name = "FF_AppId_TicketRedemption_Close_Status")]
    [ExCommsMessageKnownType]
    public enum FF_AppId_TicketRedemption_Close_Status
    {
        [EnumMember]
        Success = 100,
        [EnumMember]
        CouponRejectedbySystem = 101,
        [EnumMember]
        SystemCommunicationsTimeout = 102,
        [EnumMember]
        TiltDuringTransaction = 103,
        [EnumMember]
        BlackoutDuringTransaction = 104,
        [EnumMember]
        GameCommunicationsTimeout = 105,
        [EnumMember]
        ValueLookupTableError = 106
    }

    /// <summary>
    /// Ticket Enablement Request Command
    /// </summary>
    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Freeform",
         Name = "FF_AppId_TicketEnablement_Request_Command")]
    [ExCommsMessageKnownType]
    public enum FF_AppId_TicketEnablement_Request_Command
    {
        [EnumMember]
        DisableTicketing = 100,
        [EnumMember]
        EnableTicketing = 101,
        [EnumMember]
        QueryCurrentTicketingEnablement = 102
    }

    /// <summary>
    /// Ticket Enablement Response Status
    /// </summary>
    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Freeform",
         Name = "FF_AppId_TicketEnablement_Response_Status")]
    [ExCommsMessageKnownType]
    public enum FF_AppId_TicketEnablement_Response_Status
    {
        [EnumMember]
        TicketingDisabled = 100,
        [EnumMember]
        TicketingEnabled = 101,
        [EnumMember]
        NotATicketCapableGame = 102
    }

    /// <summary>
    /// Ticket Offline Status
    /// </summary>
    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Freeform",
         Name = "FF_AppId_TicketOfflineStatus")]
    [ExCommsMessageKnownType]
    public enum FF_AppId_TicketOfflineStatus
    {
        [EnumMember]
        WasOnline = 100,
        [EnumMember]
        WasOffline = 101
    }

    /// <summary>
    /// Ticket Sequence Number Update Error
    /// </summary>
    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Freeform",
         Name = "FF_AppId_TicketSeqNoUpdate_Error")]
    [ExCommsMessageKnownType]
    public enum FF_AppId_TicketSeqNoUpdate_Error
    {
        [EnumMember]
        Unknown = 100,
        [EnumMember]
        PaperOut = 101,
        [EnumMember]
        PaperJam = 102,
        [EnumMember]
        PaperLow = 103,
        [EnumMember]
        PrinterCommunicationsFailure = 104,
        [EnumMember]
        DelayedResponseFromGame = 105
    }

    #endregion //Freeform Ticket Processing

    #region Code Download

    public enum FF_AppId_CodeDownloadOptions
    {
        ChangeBaudRate = 101,
        BaudRateTest = 102,
        BaudRateTestResponse = 103,
        GMUDataRequest = 104,
        GMUDataResponse = 105,
        ChangeVersion = 106,
        FileDownload = 107,
        CRCResults = 108
    }

    public enum FF_AppId_FileDownloadFileType
    {
        ImageFile = 101,
        AuthenticationFile = 102,
        Optionsfile = 103
    }
    #endregion

    #region Credit Key Off
    public enum FF_AppId_CreditKeyOff_G2H_Types
    {
        AuthorizationRequest = 101,
    }

    public enum FF_AppId_CreditKeyOff_H2G_Types
    {
        NotAuthorized = 100,
        AuthorizationApproved = 101,
    }
    #endregion

    #region Default IO
    public enum FF_AppId_DefaultIO_Types
    {
        DisplayText = 101,
        PlayerQuery = 102,
        SetLockout = 103,
        InitiateQuestion = 104,
        QuestionRequest = 105,
        QuestionOver = 106
    }

    public enum FF_AppId_DefaultIO_DisplayText_ResponseStatus
    {
        Failure = 100,
        Success = 101
    }

    public enum FF_AppId_DefaultIO_SetLockoutTypes
    {
        LockoutOff = 100,
        LockoutOn = 101
    }

    public enum FF_AppId_DefaultIO_QuestionRequest_ResponseStatus
    {
        FullResponse = 100,
        LessThanFullResponse = 101
    }

    public enum FF_AppId_DefaultIO_QuestionRequest_EncryptionStatus
    {
        NoEncryption = 100,
        EncryptionUsed = 101
    }
    #endregion

    #region PreCommitment

    /// <summary>
    /// PreCommitment Action
    /// </summary>
    public enum FF_AppId_PreCommitment_Action
    {
        Status = 101,
        CardIn = 102,
        ApproachingLimit = 103,
        LimitReached = 104,
        RelaxedLimit = 105,
        NotificationResponseAck = 106,
        EnrollmentParam = 107,
        EnrollmentResponse = 108
    }

    public enum FF_AppId_PreCommitment_LockType
    {
        NoLock = 100,
        TemporaryLock = 101,
        HardLock = 102
    }

    public enum FF_AppId_PreCommitmentAcknowledgementTypes
    {
        InvalidNotification = 101,
        ApproachingNotification = 102,
        LimitsReachedNotification = 103,
        BreakPeriodAccepted = 104,
        BreakPeriodRejected = 105,
        ChangeLimitsAccepted = 106,
        ChangeLimitsRejected = 107
    }

    #endregion //PreCommitment

    #region Remote Shutdown

    public enum FF_AppId_RemoteShutdown_RequestTypes
    {
        SendStatus = 100,
        StartGame = 101,
        ShutdownGameWithAutoDeposit = 102,
        ShutdownGameWithNoAutoDeposit = 103,
        SiteEnableDisable = 104,
    }

    public enum FF_AppId_RemoteShutdown_RequestGameStatus
    {
        GameDisabled = 100,
        GameEnabled = 101,
    }

    public enum FF_AppId_RemoteShutdown_ResponseTypes
    {
        GameStatusReturn = 100,
        AckingStartup = 101,
        AckingShutdown = 102,
        SiteEnableDisableScheduleAck = 103,
        SiteEnableDisableScheduleRequest = 104,
    }

    public enum FF_AppId_RemoteShutdown_ResponseStatus
    {
        NotEngaged = 100,
        WaitingForCreditRemoval = 101,
        WaitingForAutoDeposit = 102,
        GameShutdown = 103,
        WaitingForGameToRespondStartupCommand = 104,
        WaitingForGameToRespondShutdownCommand = 105,
        OverridingNotAllowed = 106,
        GameOffline = 107,
        SiteEnableDisableScheduleRequest = 108,
        GameStarted = 109,
    }

    #endregion

    #region Features Support

    public enum FF_AppId_FeatureSupports_Types
    {
        BMCFeaturesSupport = 1,
        GMUFeaturesSupport = 2,
    }

    #endregion

    #region Game Message

    public enum FF_AppId_GameMessage_ResponseTypes
    {
        Nack = 0,
        Ack = 1,
    }

    public enum FF_AppId_GameMessage_ProtocolTypes
    {
        SAS = 1,
        SimpleSerial = 2,
        BOB = 3,
    }

    public enum FF_AppId_GameMessage_GameResponses
    {
        Ignore = 0,
        Return = 1,
    }

    #endregion

    #region Game Capping
    public enum FF_AppId_GameCapping_G2H_RequestTypes
    {
        StartEnd = 0,
        TimerExpire = 1,
    }
    #endregion Game Capping

    #region GMU Variable Action

    public enum FF_AppId_GMUVarAction
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

    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Freeform",
         Name = "FF_AppId_PrintRestrictedTicket")]
    public enum FF_AppId_PrintRestrictedTicket
    {
        [EnumMember]
        Disable = 0,
        [EnumMember]
        Enable = 1
    }

    #endregion //GMU Variable Action

    #region Long PollCode

    public enum FF_AppId_LongPollCodes
    {
        LPC_MachineDisable = 0x01,
        LPC_MachineEnable = 0x02,
        LPC_LongPoll_51 = 0x51,
        LPC_GameDenom = 0x53,
        LPC_GetCommsStatus = 0x54,
        LPC_HandpayClear = 0x94,
        LPC_CurrentCredits = 0x1A,
        LPC_LongPoll_1B = 0x1B,
        LPC_NetPoll = 0xF0,
        LPC_NetForcedPeriodic = 0xF1,
        LPC_NetAck = 0xA0,
        LPC_NetNak = 0xB0,
        LPC_ExtendedGameInfo = 0xB5,
        LPC_NetHandpay = 0xE0,
        LPC_NetPromo = 0xF2,
        LPC_NetSports = 0xF3,
        LPC_NetCard = 0xF4,
        LPC_FF2Poll = 0xCD
    }

    #endregion //Long PollCode

    public enum FF_AppId_PIDTypes
    {
        None = 0,
        GetParameterRequest = 1,
        GetParameterResponse = 2,
        SetParameterRequest = 3,
        SetParameterResponse = 4,
        RSA_QuerySystem = 0xF,
        RSA_QueryGMU = 0x10,
    }

    public enum FF_AppId_PID_ParameterIds
    {
        None = 0,
        RSA_QuerySystem = 0xF,
        RSA_QueryGMU = 0x10,
    }
}
