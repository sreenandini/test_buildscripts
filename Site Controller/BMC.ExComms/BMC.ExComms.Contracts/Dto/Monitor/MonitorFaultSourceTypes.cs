using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Monitor
{
    #region Fault Source
    /// <summary>
    /// Fault Source Enumeration
    /// </summary>
    public enum FaultSource
    {
        ///// <summary>
        ///// Unknown
        ///// </summary>
        //Unknown = 0,
        /// <summary>
        /// NullEvent (1)
        /// </summary>
        NullEvent = 1,
        /// <summary>
        /// EPI (2)
        /// </summary>
        EPI = 2,
        /// <summary>
        /// ClearLogEvent (19)
        /// </summary>
        ClearLogEvent = 19,
        /// <summary>
        /// DoorEvent (20)
        /// </summary>
        DoorEvent = 20,
        /// <summary>
        /// NonPriorityEvent (21)
        /// </summary>
        NonPriorityEvent = 21,
        /// <summary>
        /// PriorityEvent (22)
        /// </summary>
        PriorityEvent = 22,
        /// <summary>
        /// ECash (23)
        /// </summary>
        ECash = 23,
        /// <summary>
        /// TiltEvent (24)
        /// </summary>
        TiltEvent = 24,
        /// <summary>
        /// ErrorEvent (25)
        /// </summary>
        ErrorEvent = 25,
        /// <summary>
        /// DeviceFaultEvent (26)
        /// </summary>
        DeviceFaultEvent = 26,
        /// <summary>
        /// GeneralEvent (27)
        /// </summary>
        GeneralEvent = 27,
        /// <summary>
        /// GameGmuRequest (28)
        /// </summary>
        GameGmuRequest = 28,
        /// <summary>
        /// PrinterEvent (29)
        /// </summary>
        PrinterEvent = 29,
        /// <summary>
        /// HopperEvent (30)
        /// </summary>
        HopperEvent = 30,
        /// <summary>
        /// GeneralEvent2 (31)
        /// </summary>
        GeneralEvent2 = 31,

        Game_Capping = 32,

        /// <summary>
        /// Precommitment (33)
        /// </summary>
        Precommitment = 33,

        /// <summary>
        /// GIM_Event (100)
        /// </summary>
        GIM_Event = 100,

        /// <summary>
        /// TicketEvent (101)
        /// </summary>
        TicketEvent = 101,

        /// <summary>
        /// GMUVariableAction
        /// </summary>
        GMUVarAction = 102,

        /// <summary>
        /// LongPoll (103)
        /// </summary>
        LongPoll = 103,

        /// <summary>
        ///  ExtendedGameInfo (253)
        /// </summary>
        ExtendedGameInfo = 253,

        /// <summary>
        /// CommsEvent (254)
        /// </summary>
        CommsEvent = 254,
        /// <summary>
        /// Client requests
        /// </summary>
        Client = 255,
    }
    #endregion

    #region Fault Types
    #region FaultType_NullEvent (1)
    /// <summary>
    /// FaultType_NullEvent (1)
    /// </summary>
    public enum FaultType_NullEvent
    {
        // Unknown = -1,
        NullXC = 1
    }
    #endregion

    #region FaultType_ClearLogEvent (19)
    /// <summary>
    /// FaultType_ClearLogEvent (19)
    /// </summary>
    public enum FaultType_ClearLogEvent
    {
        // Unknown = -1,
        HourlyLogFull = 10,
        EventLogFull = 12
    }
    #endregion

    #region FaultType_DoorEvent (20)
    /// <summary>
    /// FaultType_DoorEvent (20)
    /// </summary>
    public enum FaultType_DoorEvent
    {
        // Unknown = -1,
        BillCassetteDoorOpened = 1,
        BillCassetteDoorClosed = 2,
        CashDoorOpened = 3,
        CashDoorClosed = 4,
        SlotDoorOpened = 10,
        SlotDoorClosed = 11,
        StackerDoorOpened = 12,
        StackerDoorClosed = 13,
        DropDoorOpened = 14,
        DropDoorClosed = 15,
        CardCageOpened = 16,
        CardCageClosed = 17,
        BellyDoorOpened = 18,
        BellyDoorClosed = 19,
        BillCassetteRemoved = 20,
        BillCassetteReInserted = 21,
        GMUCompartmentOpened = 22,
        GMUCompartmentClosed = 23,
        GameMPURemoved = 32,
        GameMPUReinstalled = 33,
        AuxFillDoorOpened = 35,
        AuxFillDoorClosed = 36,
        AcceptorRemoved = 66,
        AcceptorDoorOpened = 75,
        AcceptorDoorClosed = 76,
        MPUCompartmentOpened = 167,
        MPUCompartmentClosed = 168,
        PowerOffCardCageAccess = 200,
        PowerOffSlotDoorAccess = 201,
        PowerOffCashBoxDoorAccess = 202,
        PowerOffDropDoorAccess = 203,
    }
    #endregion

    #region FaultType_NonPriorityEvent (21)
    /// <summary>
    /// FaultType_NonPriorityEvent (21)
    /// </summary>
    public enum FaultType_NonPriorityEvent
    {
        // Unknown = -1,
        GamePowerUp = 6,
        GamePowerDown = 7,
        PowerReset = 8,
        CommsStopped = 10,
        CommsResumed = 11,
        NOVOTicketOut = 14,
        EventLogFull = 15,
        Periodic = 16,
        ZeroCredit = 17,
        HandPaidJackpot = 18,
        JackpotReset = 19,
        RAMReset = 20,
        ExtraSAS = 21,
        GameChanged = 22,
        GameMPUReset = 40,
        ForcedPeriodic = 60,
        InstantPeriodic = 80,
        CompListChangedXC = 100,
        NewGameSelected = 174,
        StartCardlessPlay = 178,
        EndCardlessPlay = 179,
        MachinePaidExternalBonusWin = 204,
        AttendantPaidExternalBonusWin = 206,
        GMUHeartBeat = 211,
        ClosingSessions = 213,
        NonZeroCredit = 213,
        PolledHandpayEvent = 243,
        CurrentCreditMeter = 249,
    }
    #endregion

    #region FaultType_PriorityEvent (22)
    /// <summary>
    /// FaultType_PriorityEvent (22)
    /// </summary>
    public enum FaultType_PriorityEvent
    {
        // Unknown = -1,
        TicketPrintStart = 1,
        XCTicketRedeemStart = 2,
        TicketRedeem = 3,
        PlayerCardIn = 5,
        PlayercardOut = 6,
        TicketPrinted = 9,
        TicketRedeemCRCFailure = 10,
        AbandonedCard = 12,
        EmployeeCardIn = 37,
        EmployeecardOut = 38,
        ServiceRequest = 99,
        ZeroCredit = 205,
        NonZeroCredit = 210,
    }
    #endregion

    #region FaultType_ECash (23)
    /// <summary>
    /// FaultType_ECash (23)
    /// </summary>
    public enum FaultType_ECash
    {
        // Unknown = -1,
        AFTAccountList = 1,
        WithdrawalRequest = 2,
        DepositRequest = 3,
        Deposit = 4,
        WithdrawalComplete = 5,
        CashlessBalance = 21,
        CashlessWithdrawals = 22,
        CashlessCollect = 23,
        WithdrawalAuthorization = 31,
        EnableEFT = 91,
        DisableEFT = 92,
    }
    #endregion

    #region FaultType_TiltEvent (24)
    /// <summary>
    /// FaultType_TiltEvent (24)
    /// </summary>
    public enum FaultType_TiltEvent
    {
        // Unknown = -1,
        Reel1Tilt41 = 41,
        Reel2Tilt42 = 42,
        Reel3Tilt43 = 43,
        Reel4Tilt44 = 44,
        Reel5Tilt45 = 45,
        Tilt47 = 47,
        Tilt48 = 48,
        SlotMachineTilt64 = 64,
        ReelSpinAfterIndexTilt81 = 81,
        ReelSpinAfterIndexTilt82 = 82,
        ReelSpinAfterIndexTilt83 = 83,
        ReelSpinAfterIndexTilt84 = 84,
        ReelSpinAfterIndexTilt85 = 85,

    }
    #endregion

    #region FaultType_ErrorEvent (25)
    /// <summary>
    /// FaultType_ErrorEvent (25)
    /// </summary>
    public enum FaultType_ErrorEvent
    {
        // Unknown = -1,
        TooManyBadPINs = 2,
        DivMalfunXC = 9,
        IllegalCardremoval = 13,
        BadMagCardReader = 14,
        AcceptorLargeBuyIn = 15,
        AcceptorBadPay = 18,
        BonusPointRollover = 20,
        BadMachinePayAmt = 31,
        ResetDuringPayout = 47,
        ExtraCoinsPaidOut = 48,
        NoDataOnMagCard = 50,
        GMUMalfunction = 55,
        WinWithNoHandlePull = 57,
        WinWithNoCoinIn = 58,
        HopperCantPay = 59,
        TooManyBillsRejected = 86,
        AcceptorMalfunction = 87,
        CantReadMagCard = 88,
        GameMemoryMalfunction = 95,
        GMUMetersReset = 98,
    }
    #endregion

    #region FaultType_DeviceFaultEvent (26)
    /// <summary>
    /// FaultType_DeviceFaultEvent (26)
    /// </summary>
    public enum FaultType_DeviceFaultEvent
    {
        // Unknown = -1,
        AcceptorHopperJam = 3,
        AcceptorCantvend = 16,
        AcceptorRunawayHopper = 19,
        RunawayHopper = 49,
        CoinOutJam = 54,
        BillCassetteIsFull = 67,
        BillCassetteIsJammed = 68,
        AcceptorNotResponding = 69,
        AcceptorFunctioningAgain = 70,
        CoinInJam = 90,
        CoinDropSwitchStuck = 91,
        AcceptorJammed = 92,
        TooManyCoinsIn = 93,
        DisplayFault = 163,
        TouchScreenError = 164,
        LowBatteryCondition = 165,
        GameEPROMSignatureFault = 166,
        SlotPrinterFault = 176,
    }
    #endregion

    #region FaultType_GeneralEvent (27)
    /// <summary>
    /// FaultType_GeneralEvent (27)
    /// </summary>
    public enum FaultType_GeneralEvent
    {
        // Unknown = -1,
        ServiceRequest = 4,
        SpintekInfoMessage = 5,
        JCMStatusXC = 6,
        DMKPreemptiveFill = 7,
        HotPlayerXC = 8,
        PayoutPercentageChanged = 10,
        LinkProgressiveReport = 11,
        ReverseBillDetected = 14,
        GameReserved = 23,
        CouponRedeemed = 26,
        TransferFromGame = 27,
        CouponRequest = 28,
        DMKFillRequest = 29,
        JackpotToCreditMeter = 30,
        BackInPlay = 46,
        XCGMUWatchdogXC = 51,
        Blackout = 62,
        MachinePaidJackpot = 63,
        GameActivityReport = 65,
        BillVendToCreditMeter = 89,
        EmpServiceEntry = 99,
        PatronRequestForInfo = 160,
        UnknownTableIndex = 161,
        EmployeeKeySequence = 162,
        GMUIntrepidized = 182,
        FreeformResponse = 183,
        FreeformTransportNAK = 184,
        FreeformXC = 185,
        AcceptorSWChanged = 186,
        AcceptorSWChangeAcknowledged = 187,
        GMUInitiatedFreeformMessage = 188,
    }
    #endregion

    #region FaultType_GameGmuRequest (28)
    /// <summary>
    /// FaultType_GameGmuRequest (28)
    /// </summary>
    public enum FaultType_GameGmuRequest
    {
        // Unknown = -1,
        GMUUpdateRequest = 17,
        ChangeRequest = 21,
        BeverageRequest = 22,
        Emergency911 = 24,
        RequestToChangeGMU = 25,
        CashoutRequest = 177,
        ClearPlayerRequest = 180,
        QualifyingPlayAchieved = 181,
    }
    #endregion

    #region FaultType_PrinterEvent (29)
    /// <summary>
    /// FaultType_PrinterEvent (29)
    /// </summary>
    public enum FaultType_PrinterEvent
    {
        // Unknown = -1,
        PrinterCommunicationError = 26,
        PrinterPaperEmpty = 27,
        PrinterPaperLow = 28,
        PrinterPowerOff = 29,
        PrinterPowerOn = 30,
        ReplacePrinterRibbon = 31,
        PrinterCarriageJammed = 32,
    }
    #endregion

    #region FaultType_HopperEvent (30)
    /// <summary>
    /// FaultType_HopperEvent (30)
    /// </summary>
    public enum FaultType_HopperEvent
    {
        // Unknown = -1,
        HopperFull = 11,
        HopperLevelLow = 12,
    }
    #endregion

    #region FaultType_GeneralEvent2 (31)
    /// <summary>
    /// FaultType_GeneralEvent2 (31)
    /// </summary>
    public enum FaultType_GeneralEvent2
    {
        // Unknown = -1,
        ACPowerAppliedXC = 1,
        ACPowerLostXC = 2,
        GenTiltXC = 3,
        CounterfeitBillXC = 4,
        ReverseCoinInXC = 5,
        CashboxNearFullXC = 6,
        MemoryErrResetXC = 7,
        HandPayValidatedXC = 8,
        ValidationIDNotCfgXC = 9,
        NoProgFor5SecXC = 10,
        ProgLevelHitXC = 11,
        XCBuffOverflowXC = 12,
        BillValidatorRstXC = 13,
        MultipleJackpotXC = 14,
        AttMenuEnterXC = 15,
        AttMenuExitXC = 16,
        SelfTestEnterXC = 17,
        SelfTestExitXC = 18,
        OutOfServiceXC = 19,
    }
    #endregion

    #region FaultType_CommsEvent (254)
    /// <summary>
    /// FaultType_CommsEvent (254)
    /// </summary>
    public enum FaultType_CommsEvent
    {
        // Unknown = -1,
        CommsLost = 2,
        CommsResumed = 3,
        GameCommLost = 4,
        GameCommRestored = 5,
        GameDenomOrPayoutChange = 250,
        GameCountChange = 251,
        GameInformation = 252
    }
    #endregion

    #region GIM Event
    public enum FaultType_GIM
    {
        Game_Id_Info_G2H = 1,
        Aux_Network_Enable_Disable_G2H = 2,
        Game_Id_Request_G2H = 3,
        Game_Id_Info_H2G = 11,
        Aux_Network_Enable_Disable_H2G = 12,
        Game_Id_Request_H2G = 13,
    }
    #endregion

    #region FaultType_TicketEvent

    public enum FaultType_TicketEvent
    {
        TicketPrinted = 1,
        TicketPrintedResponse = 7,
        TicketVoid = 2,
        TicketRedemptionRequest = 3,
        TicketRedemptionResponse = 8,
        TicketRedemptionComplete = 4,
        TicketRedemptionCompleteResponse = 9,
        TicketPrintComplete = 5,
        TicketEnablementRequest = 6,
        TicketEnablementResponse = 10,
        TicketPrintStatus = 11,
        GameTicketSequenceNumberUpdate = 12
    }

    #endregion //FaultType_TicketEvent

    #region FaultType_Precommitment

    public enum FaultType_Precommitment
    {
        StatusRequest = 1,
        StatusResponse = 2,
        StatusResponsePlayerCardIn = 3,
        ApproachingLimit = 4,
        LimiReached = 5,
        RelaxedLimit = 6,
        NotificationResponse = 7,
        EnrollmentParamRequest = 8,
        EnrollmentParamResponse = 9,
        PlayerEnrollmentRequest = 10,
        PlayerEnrollmentResponse = 11,
        IntervalRating = 12
    }

    #endregion //FaultType_Precommitment

    #region Game_Capping
    public enum FaultType_Game_Capping
    {
        Lock = 1,
        Release = 2,
        TimerExpiry = 3,
    }
    #endregion Game_Capping

    #region GMU Variable Action

    /// <summary>
    /// FaultType_GMUVarAction
    /// </summary>
    public enum FaultType_GMUVarAction
    {
        CardlessPlayTimeOutRequest = 101,
        CardlessPlayTimeOutResponse = 102,
        CardlessPlayTimeOutStatus = 103,
        IntervalRatingRequest = 104,
        IntervalRatingResponse = 105,
        IntervalRatingStatus = 106,
        BonusPointsSubtractionRequest = 107,
        BonusPointsSubtractionResponse = 108,
        BonusPointsSubtractionStatus = 109,
        TicketNumberRequest = 110,
        TicketNumberResponse = 111,
        TicketNumberStatus = 112,
        TicketSystemSlotIDRequest = 113,
        TicketSystemSlotIDResponse = 114,
        TicketSystemSlotIDStatus = 115,
        TicketPrintDateRequest = 116,
        TicketPrintDateResponse = 117,
        TicketPrintDateStatus = 118,
        TicketExpirationDateRequest = 119,
        TicketExpirationDateResponse = 120,
        TicketExpirationDateStatus = 121,
        TicketKeyRequest = 122,
        TicketKeyResponse = 123,
        TicketKeyStatus = 124,
        LiabilityLimit = 125,
        EFTCharacteristicsRequest = 126,
        EFTCharacteristics = 127,
        EFTCharacteristicsStatus = 128,
        EFTTransactionTimeoutRequest = 129,
        EFTTransactionTimeoutResponse = 130,
        EFTTransactionTimeoutStatus = 131,
        EFTWithdrawalAmountsRequest = 132,
        EFTWithdrawalAmountsResponse = 133,
        EFTWithdrawalAmountsStatus = 134,
        TimeOfDayRequest = 135,
        TimeOfDayResponse = 136,
        TimeOfDayResponseNACK = 137,
        TimeOfDayStatus = 138,
        RestrictedTicketExpirationDaysRequest = 139,
        RestrictedTicketExpirationDaysResponse = 140,
        RestrictedTicketExpirationDaysStatus = 141,
        EnablePrintingOfRestrictedTicketsRequest = 142,
        EnablePrintingOfRestrictedTicketsResponse = 143,
        EnablePrintingOfRestrictedTicketsStatus = 144,
        MaximumOfflineTicketsAllowedRequest = 145,
        MaximumOfflineTicketsAllowedResponse = 146,
        OfflineTicketTextLine1Request = 147,
        OfflineTicketTextLine1Response = 148,
        OfflineTicketTextLine2Request = 149,
        OfflineTicketTextLine2Response = 150,
        MaxWithdrawParameterRequest = 151,
        MaxWithdrawParameterResponse = 152,
        MaxDepositParameterRequest = 153,
        MaxDepositParameterResponse = 154,
        SDSVersionRequest = 155,
        SDSVersionResponse = 156,
        EnhanceVdalidationVoucherProfileRequest = 157,
        EnhanceVdalidationVoucherProfileResponse = 158,
        EnhanceVdalidationVoucherDataRequest = 160,
        EnhanceVdalidationVoucherDataResponse = 161,
    }

    #endregion //GMU Variable Action

    #region FaultType_ExtendedGameInfo (253)
    public enum FaultType_ExtendedGameInfoEvent
    {
        UpdateExtendedGameInfo = 1
    }
    #endregion

    #region LongPollCode

    public enum FaultType_LongPollCode
    {
        LPC_Custom = -1,
        LPC_Send_1F = 0,
        LPC_Get_SAS_Ver = 1,
        LPC_Send_51 = 2,
        LPC_Receive_51 = 111,
        LPC_Send_53 = 3,
        LPC_Send_B5 = 4,
        LPC_Send_1A = 5,
        LPC_Send_1B = 6,
        LPC_Send_ForcePeriodic = 7,
        LPC_Send_InstantPeriodic = 8,
        LPC_Process_Ack = 9,
        LPC_Process_Data = 10,
        LPC_EnableMachine = 11,
        LPC_DisableMachine = 12,
    }

    #endregion //LongPollCode

    #region Client Types
    public enum FaultType_Client
    {
        AckNack = -99,
        AddUDPToList = 0,
        AddUDPsToList = 1,
        RemoveUDPFromList = 2,
        EnableDisableMachine = 3,
    }
    #endregion

    #region FaultType EPI

    public enum FaultType_EPI
    {
        EPI_Info = 1
    }

    #endregion //FaultType EPI

    #endregion Fault Types
}
