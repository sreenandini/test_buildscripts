using BMC.PlayerGateway.Gateway;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.ExComms.Contracts.DTO.Monitor;
using BMC.CoreLib;
using BMC.Common.Utilities;
using BMC.ExComms.DataLayer.MSSQL;
using BMC.ExComms.Contracts.Configuration;
using System.Data;
using BMC.ExComms.Contracts.Interfaces;
using BMC.PlayerGateway.SDT.Messages;
using BMC.CoreLib.Comparers;
using BMC.PlayerGateway.SDT;
using BMC.Common.ExceptionManagement;
using BMC.ExMonitor.Server.Handlers.Precommitment;

namespace BMC.ExMonitor.Server.Handlers.EPI
{
    public class SDTMessages
        : DisposableObject
    {
        private delegate void ResponseType_MapDelegate(AFTInformation aftInfo, object requestId, bool returnStauts);
        private IDictionary<string, ResponseType_MapDelegate> _responseTypeMap = null;
        private IDictionary<Type, ResponseType_MapDelegate> _responseMapByType = null;
        private static SDTMessages _instance;
        private readonly IExMonitorServerConfigStore _configStore = ExMonitorServerConfigStoreFactory.Store;
        private int m_lNextPTGatewayRequest;

        private const string SESRATING_PROCESS_CARD_IN = "CI";
        private const string SESRATING_PROCESS_CARD_OUT = "CO";
        private const string SESRATING_PROCESS_INTERVAL_RATING = "IR";
        private const string SESRATING_PROCESS_FIRST_INTERVAL_RATING = "FIR";

        private const string SESRATING_TYPE_P = "P";
        private const string SESRATING_TYPE_C = "C";

        public SDTMessages()
        {
            FillResponseTypeMapandImpl();
        }

        public static SDTMessages Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SDTMessages();
                }
                return _instance;
            }
        }

        protected EPIManager CurrentEPIManager
        {
            get { return EPIManager.Current; }
        }

        private EPIMsgProcessor CurrentEPIMsgProcessor
        {
            get { return EPIMsgProcessor.Current; }
        }

        protected ExCommsDataContext CurrentDataContext
        {
            get { return ExCommsDataContext.Current; }
        }

        private void FillResponseTypeMapandImpl()
        {
            _responseTypeMap = new Dictionary<string, ResponseType_MapDelegate>() {
                { MonitorConstants.CARDIN,new  ResponseType_MapDelegate(this.ProcessPlayerCardInResponse)},
                { MonitorConstants.BALANCERESPONSE, new ResponseType_MapDelegate(this.ProcessBalanceResponse) },
                { MonitorConstants.WITHDRAWRESPONSE, new ResponseType_MapDelegate(this.Process_WithdrawResponse) },
                { MonitorConstants.WITHDRAWCOMPLETERESPONSE, new ResponseType_MapDelegate(this.Process_WithdrawCompleteResponse) },
                { MonitorConstants.DEPOSITRESPONSE, new ResponseType_MapDelegate(this.Process_DepositResponse) },
                { MonitorConstants.DEPOSITCOMPLETERESPONSE, new ResponseType_MapDelegate(this.Process_DepositCompleteResponse) },
                { MonitorConstants.GAMECAPPING_AUTHENTICATION, new ResponseType_MapDelegate(this.Process_GameCapAuthenticationResponse) },
                { MonitorConstants.GAMEUNCAPPING_AUTHENTICATION, new ResponseType_MapDelegate(this.Process_GameUnCapAuthenticationResponse) },
            };
            _responseMapByType = new SortedDictionary<Type, ResponseType_MapDelegate>(new TypeComparer())
            {
                { typeof(PlayerCardInResponse), this.ProcessPlayerCardInResponse },
                { typeof(PlayerCardOutResponse), null },
                { typeof(IntervalRatingResponse), null },
                { typeof(BalanceResponse), this.ProcessBalanceResponse },
                { typeof(WithdrawalResponse), this.Process_WithdrawResponse },
                { typeof(WithdrawalCompleteResponse), this.Process_WithdrawCompleteResponse },
            };
        }

        public void ProcessAFTInformation(AFTInformation aftInfo, string responseType, object requestId, bool returnStauts)
        {
            using (ILogMethod method = Log.LogMethod("MonitorHandler_GIM", "ProcessAFTInformation"))
            {
                try
                {
                    if (_responseTypeMap.ContainsKey(responseType))
                        _responseTypeMap[responseType](aftInfo, requestId, returnStauts);
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }
            }
        }

        public void ProcessAFTInformation<T>(T response)
            where T : GatewayResponseWrapper<AFTInformation>
        {
            using (ILogMethod method = Log.LogMethod("MonitorHandler_GIM", "ProcessAFTInformation"))
            {
                try
                {
                    Type typeOfT = typeof(T);
                    if (_responseMapByType.ContainsKey(typeOfT))
                    {
                        var callback = _responseMapByType[typeOfT];
                        if (callback != null)
                            callback(response.ReturnValue, response.RequestID, (response.ResultStatus == ResponseStatus.Success));
                    }
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }
            }
        }

        private void ProcessException(Exception ex, EPICardDetail cardDetail, MonMsg_G2H message, InstallationDetailsForMSMQ dbData)
        {
            Log.Exception(ex);
            //EPIMsgProcessor.Current.SendEPIMessage(message.InstallationNo, dbData.Bar_Pos_Name, "Exception", DateTime.Now, 30, MessagePriority.NORMAL, 18);
            EPIMsgProcessor.Current.SendCommand(message.InstallationNo, MonitorConstants.SECTOR_CMD_FLASHING, "\0\x0018");
            if (dbData != null)
                CurrentEPIMsgProcessor.DisplayBallyWelcomeMsg(message.InstallationNo, dbData.Bar_Pos_Name, DateTime.Now);
            if (cardDetail != null)
                CurrentEPIManager.ClearCardDetails(cardDetail);
        }

        private string GetAssetNo(InstallationDetailsForMSMQ dbData)
        {
            string assetNo = dbData.Stock_No;
            if (_configStore.IsStockPrefixRequired &&
                HandlerHelper.Current.IsAlphaNumeric(assetNo))
            {
                assetNo = assetNo.Substring(0, _configStore.Stock_Code_Prefix.Length);
            }
            return assetNo;
        }

        private string GetStringValueFromBool(bool value)
        {
            try
            {
                return value ? "1" : "0";
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
                return "0";
            }
        }

        private string GetPlayerFlagMsgOnSuccess(AFTInformation aftInfo, bool specialPlayer, bool offer = false)
        {
            return "00000000" + "0000001"
                                + GetStringValueFromBool(aftInfo.CanWithdrawCash)
                                + GetStringValueFromBool(offer ? aftInfo.RedeemPoints : aftInfo.CanWithdrawPoints)
                                + GetStringValueFromBool(offer ? aftInfo.WithdrawOffers : aftInfo.CanWithdrawPromo)
                                + GetStringValueFromBool(aftInfo.CanDepositNonCashable)
                                + GetStringValueFromBool(specialPlayer ? aftInfo.SpecialPlayer : aftInfo.VIPFlag)
                                + GetStringValueFromBool(aftInfo.CanDepositCashable)
                                + GetStringValueFromBool(aftInfo.PinRequired)
                                + GetStringValueFromBool(aftInfo.EcashPlayer);
        }

        private GatewayMessageStructures.AFTAuditHistory GetAuditHistoryDetails(AFTInformation aftInfo, bool transactionStatus, bool ecashEnabled, bool isWithdrawComplete, string transType)
        {
            GatewayMessageStructures.AFTAuditHistory auditHistory = new GatewayMessageStructures.AFTAuditHistory();
            ExComms.Contracts.DTO.Freeform.FF_AppId_EFT_AccountTypes accountType = HandlerHelper.Current.GetSDTAccountType(aftInfo.CardNumber);

            int errorCode = 0;
            if (isWithdrawComplete)
                errorCode = HandlerHelper.Current.GetWithDrawalErrorCode(aftInfo.CardNumber);

            auditHistory.ErrorMessage = aftInfo.DisplayMessage;
            auditHistory.ErrorCode = isWithdrawComplete ? (errorCode == 0 ? aftInfo.ErrorCode : errorCode) : aftInfo.ErrorCode;
            auditHistory.IsEcashEnabled = ecashEnabled ? aftInfo.EcashPlayer : false;
            auditHistory.InstallationNo = HandlerHelper.Current.GetSDTRequest(aftInfo.CardNumber);
            auditHistory.CollectionNo = 0;
            auditHistory.AccountNumber = aftInfo.CardNumber.Remove(0, 1);
            if (!transactionStatus)
                auditHistory.Amount = aftInfo.NonCashableFunds;

            if (accountType == ExComms.Contracts.DTO.Freeform.FF_AppId_EFT_AccountTypes.PointsRedemption)
            {
                auditHistory.CashableAmt = aftInfo.CashableFunds;
                auditHistory.WATAmt = 0.0D;
            }
            else if (accountType == ExComms.Contracts.DTO.Freeform.FF_AppId_EFT_AccountTypes.PlayerCash)
            {
                auditHistory.CashableAmt = 0.0D;
                auditHistory.WATAmt = aftInfo.CashableFunds;
            }

            auditHistory.NoncashableAmt = aftInfo.NonCashableFunds;
            auditHistory.TransactionStatus = true;

            if (MonitorConstants.AccountType.ContainsKey(accountType))
                auditHistory.AccountType = MonitorConstants.AccountType[accountType];

            auditHistory.TransactionType = transType;
            auditHistory.TransferID = Convert.ToInt32(aftInfo.TransactionID);

            return auditHistory;
        }

        private void InsertAFTTransactions(GatewayMessageStructures.AFTAuditHistory auditHistory)
        {
            bool depositComplete = auditHistory.TransactionType.Equals(MonitorConstants.DEPOSITCOMPLETE_TRANSACTIONTYPE);

            ExCommsDataContext.Current.InsertAFTTransactions(
                                                                auditHistory.InstallationNo,
                                                                Convert.ToInt32(auditHistory.AccountNumber),
                                                                0,
                                                                (depositComplete ? Convert.ToDouble(auditHistory.CashableDepAmt) : auditHistory.CashableAmt),
                                                                (depositComplete ? Convert.ToDouble(auditHistory.NoncashabledepAmt) : auditHistory.NoncashableAmt),
                                                                (depositComplete ? Convert.ToDouble(auditHistory.WATDepAmt) : auditHistory.WATAmt),
                                                                DateTime.Now,
                                                                auditHistory.TransactionType,
                                                                auditHistory.TransferID,
                                                                auditHistory.AccountType,
                                                                auditHistory.TransactionStatus
                                                            );
        }

        private void AuditAFTTransactions(GatewayMessageStructures.AFTAuditHistory auditHistory)
        {
            bool depositComplete = auditHistory.TransactionType.Equals(MonitorConstants.DEPOSITCOMPLETE_TRANSACTIONTYPE);

            ExCommsDataContext.Current.AuditAFTTransactions(
                                                                auditHistory.InstallationNo,
                                                                DateTime.Now,
                                                                auditHistory.TransactionType,
                                                                (depositComplete ? Convert.ToDouble(auditHistory.CashableDepAmt) : auditHistory.CashableAmt),
                                                                (depositComplete ? Convert.ToDouble(auditHistory.NoncashabledepAmt) : auditHistory.NoncashableAmt),
                                                                (depositComplete ? Convert.ToDouble(auditHistory.WATDepAmt) : auditHistory.WATAmt),
                                                                Convert.ToInt32(auditHistory.AccountNumber),
                                                                auditHistory.IsEcashEnabled,
                                                                auditHistory.ErrorCode,
                                                                auditHistory.ErrorMessage,
                                                                auditHistory.TransferID
                                                            );
        }

        public int NextPTRequestID()
        {
            //  create a request id..
            m_lNextPTGatewayRequest = (m_lNextPTGatewayRequest + 1);
            if (m_lNextPTGatewayRequest > 100000)
            {
                m_lNextPTGatewayRequest = 1;
            }
            return m_lNextPTGatewayRequest;
        }

        public int GetSDTRequest(string CardNo, bool RemoveFromList = false)
        {
            //+k
            return 0;
        }

        private void DoDBTransaction(AFTInformation aftInfo, GatewayMessageStructures.AFTAuditHistory auditHistory, string type)
        {
            Log.Info(type + " - insert into aft transactions");
            this.InsertAFTTransactions(auditHistory);

            Log.Info(type + " - insert audit details");
            this.AuditAFTTransactions(auditHistory);

            Log.Info(type + " - Update the display message in playergatewaymessages table");
            ExCommsDataContext.Current.UpdateDisplayMessageForEFT(HandlerHelper.Current.GetSDTRequest(aftInfo.CardNumber), aftInfo.CardNumber, new Guid(aftInfo.RequestID), aftInfo.DisplayMessage);
        }

        #region EmployeeCardIn

        public bool ProcessEmployeeCardIn(MonMsg_G2H message)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "ProcessEmployeeCardIn"))
            {
                try
                {
                    InstallationDetailsForMSMQ dbData = message.Extra as InstallationDetailsForMSMQ;
                    int RequestID = NextPTRequestID(); // Store the card information                

                    method.Info("Employee No: " + RequestID);
                    string Asset = message.Asset;
                    if (HandlerHelper.Current.IsStockPrefixRequired() && HandlerHelper.Current.IsAlphaNumeric(message.Asset))
                    {
                        Asset = message.Asset.Substring(HandlerHelper.Current.GetStockPrefix().Length);
                    }
                    method.Info("Inside Employee Card In event");
                    PlayerCardInRequest employeemsg = new PlayerCardInRequest
                    {
                        CardNo = message.CardNumber,
                        BarPosition = message.BarPositionNo,
                        ManufacturerID = dbData.Machine_Manufacturers_Serial_No,
                        HoldPercentage = Convert.ToInt16(dbData.Anticipated_Percentage_Payout * 100),
                        Zone = dbData.Zone_Name,
                        Denomination = Convert.ToInt16(dbData.Installation_Price_Of_Play),
                        Asset = message.Asset,
                        GameType = dbData.GameType.ToString(),
                        SiteCode = dbData.SiteCode,
                        EmployeeNo = RequestID.ToString(),
                        StartDate = DateTime.Now.Date,
                        StartTime = DateTime.Now.TimeOfDay
                    };
                    method.Info("Message sent successfully to CMP");
                    RequestAckMessage ackResponse = new RequestAckMessage();
                    ResponseCallback<EmployeeCardInResponse> sCallback = new ResponseCallback<EmployeeCardInResponse>(Process_EmployeeCardInResponse);
                    ackResponse = HandlerHelper.PlayerGatewayInstance.EmployeeCardIn(employeemsg, sCallback);
                    return true;
                }
                catch (Exception ex)
                {
                    method.Info("Error while processing");
                    method.Exception(ex);
                    return false;
                }
            }
        }

        private void Process_EmployeeCardInResponse(EmployeeCardInResponse response)
        {

        }

        #endregion

        #region EmployeeCardOut

        public bool ProcessEmployeeCardOut(MonMsg_G2H message)
        {

            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "ProcessEmployeeCardOut"))
            {
                try
                {
                    InstallationDetailsForMSMQ dbData = message.Extra as InstallationDetailsForMSMQ;
                    int RequestID = NextPTRequestID(); // Store the card information                

                    method.Info("Employee No: " + RequestID);
                    string Asset = message.Asset;
                    if (HandlerHelper.Current.IsStockPrefixRequired() && HandlerHelper.Current.IsAlphaNumeric(message.Asset))
                    {
                        Asset = message.Asset.Substring(HandlerHelper.Current.GetStockPrefix().Length);
                    }
                    method.Info("Inside Employee Card Out event");
                    PlayerCardInRequest employeemsg = new PlayerCardInRequest
                    {
                        CardNo = message.CardNumber,
                        BarPosition = message.BarPositionNo,
                        ManufacturerID = dbData.Machine_Manufacturers_Serial_No,
                        HoldPercentage = Convert.ToInt16(dbData.Anticipated_Percentage_Payout * 100),
                        Zone = dbData.Zone_Name,
                        Denomination = Convert.ToInt16(dbData.Installation_Price_Of_Play),
                        Asset = message.Asset,
                        GameType = dbData.GameType.ToString(),
                        SiteCode = dbData.SiteCode,
                        EmployeeNo = RequestID.ToString(),
                        StartDate = DateTime.Now.Date,
                        StartTime = DateTime.Now.TimeOfDay
                    };
                    method.Info("Message sent successfully to CMP");
                    RequestAckMessage ackResponse = new RequestAckMessage();
                    ResponseCallback<EmployeeCardOutResponse> sCallback = new ResponseCallback<EmployeeCardOutResponse>(Process_EmployeeCardOutResponse);
                    ackResponse = HandlerHelper.PlayerGatewayInstance.EmployeeCardOut(employeemsg, sCallback);
                    return true;
                }
                catch (Exception ex)
                {
                    method.Info("Error while processing");
                    method.Exception(ex);
                    return false;
                }
            }
        }

        private void Process_EmployeeCardOutResponse(EmployeeCardOutResponse response)
        {

        }

        #endregion

        #region Player Card In
        internal bool ProcessPlayerCardIn(MonMsg_G2H message, MonTgt_G2H_Status_PlayerCardIn monitorTarget)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "ProcessPlayerCardIn"))
            {
                bool result = default(bool);
                InstallationDetailsForMSMQ dbData = null;
                EPICardDetail cardDetail = null;

                try
                {
                    // variables
                    dbData = message.Extra as InstallationDetailsForMSMQ;
                    int cardNoInt = message.CardNumber.ConvertToInt32();
                    EPIMeterValueTypes meterValueType = EPIMeterValueTypes.Start;

                    // create the process
                    EPIManager.Current.CreateProcess(message.InstallationNo);

                    // get the floor financial
                    var dbFloorFinancial = ExCommsDataContext.Current.GetFloorFinancials(message.InstallationNo).FirstOrDefault();
                    if (dbFloorFinancial == null) return false;

                    // add the card detail
                    cardDetail = EPIManager.Current.AddOrUpdateCardInDetail(message.InstallationNo, message.CardNumber);
                    cardDetail.IsFirstCardIn = true;
                    cardDetail.EPISessionStart = DateTime.Now;
                    cardDetail.EPICMPGameCode = dbData.CMPCode;

                    // update the starting meters
                    cardDetail.CopyMeterValuesFromDB(dbFloorFinancial, meterValueType);

                    // insert the session rating
                    method.Info("Insert record in player session rating");
                    ExCommsDataContext.Current.InsertPlayerSessionRating(message.InstallationNo, cardNoInt, SESRATING_PROCESS_CARD_IN, SESRATING_TYPE_P);
                    ExCommsDataContext.Current.InsertPCSessionRating(message.InstallationNo, cardNoInt, SESRATING_PROCESS_CARD_IN, SESRATING_TYPE_P);
                    method.Info("Record inserted in Player session rating");

                    // log the meter values
                    method.Info("Player Card Session Ratings (Start) : " + Environment.NewLine + cardDetail.ToString());

                    // save the installation no against card no
                    HandlerHelper.Current.SaveSDTRequest(cardDetail.CardNo, message.InstallationNo);
                    HandlerHelper.Current.SavePTInstallation(cardDetail.CardNo, message.InstallationNo);

                    // asset no
                    string assetNo = this.GetAssetNo(dbData);

                    // invoke the player card in on player gateway
                    PlayerCardInRequest sdtRequest = new PlayerCardInRequest();
                    sdtRequest.Asset = assetNo;
                    sdtRequest.BarPosition = dbData.Bar_Pos_Name;
                    sdtRequest.CardNo = cardDetail.CardNo;
                    sdtRequest.CoinsIn = cardDetail[meterValueType, EPIMeterTypes.CoinsIn];
                    sdtRequest.CoinsOut = cardDetail[meterValueType, EPIMeterTypes.CoinsOut];
                    sdtRequest.GamesLost = cardDetail[meterValueType, EPIMeterTypes.GamesLost];
                    sdtRequest.GamesPlayed = cardDetail[meterValueType, EPIMeterTypes.GamesPlayed];
                    sdtRequest.GamesWon = cardDetail[meterValueType, EPIMeterTypes.GamesWon];
                    sdtRequest.Denomination = checked((short)Math.Round((double)dbData.Installation_Price_Of_Play));
                    sdtRequest.HoldPercentage = checked((short)Math.Round((double)unchecked(dbData.Anticipated_Percentage_Payout * 100.0)));
                    sdtRequest.InstallationNo = message.InstallationNo;
                    sdtRequest.ManufacturerID = dbData.CMPCode;
                    sdtRequest.SiteCode = dbData.SiteCode;
                    sdtRequest.Zone = dbData.Zone_Name;
                    sdtRequest.GameType = dbData.GameType.ToString();

                    // send the request
                    RequestAckMessage requestAckMessage = HandlerHelper.PlayerGatewayInstance.PlayerCardIn(sdtRequest,
                        new ResponseCallback<PlayerCardInResponse>(this.ProcessAFTInformation<PlayerCardInResponse>));
                    method.Info("PLAYERCARDIN : Time taken to receive ack from Gateway : " + DateTime.Now.TimeOfDay.ToString());
                    if (requestAckMessage != null)
                    {
                        CurrentEPIManager.CreateInactivityTimeout(message.InstallationNo);
                        CurrentEPIManager.CreateIntervalRatingTimer(message.InstallationNo);
                    }
                }
                catch (Exception ex)
                {
                    this.ProcessException(ex, cardDetail, message, dbData);
                }

                return result;
            }
        }

        private void ProcessPlayerCardInResponse(AFTInformation aftInfo, object requestId, bool returnStauts)
        {
            using (ILogMethod method = Log.LogMethod("SDTMessages", "Process_BalanceResponse"))
            {
                try
                {
                    string sMsg = string.Empty;
                    if ((aftInfo.ResponseReceivedType != GatewayResponseReceivedType.CacheResponse)
                        || (aftInfo.ResponseReceivedType == GatewayResponseReceivedType.CacheResponse)
                        && string.IsNullOrEmpty(aftInfo.EPIMessage))
                    {
                        int installationNo = GetSDTRequest(aftInfo.CardNumber, true);
                        method.Info("Installation Number 1" + installationNo);
                        string strCMPResponse = "";
                        if (returnStauts)
                        {
                            method.Info("Time taken to receive message from Gateway " + DateTime.Now.TimeOfDay.ToString());
                            strCMPResponse = (aftInfo.MessagetoDisplay + aftInfo.MessagetoDisplay2.TrimEnd());
                            method.Info("Card in Response " + strCMPResponse);
                            method.Info("Is Ecash Player" + aftInfo.EcashPlayer);
                            sMsg = "00000000" + "0000001"
                                    + GetStringValueFromBool(aftInfo.CanWithdrawCash)
                                    + GetStringValueFromBool(aftInfo.CanWithdrawPoints)
                                    + GetStringValueFromBool(aftInfo.CanWithdrawPromo)
                                    + GetStringValueFromBool(aftInfo.CanDepositNonCashable)
                                    + GetStringValueFromBool(aftInfo.VIPFlag)
                                    + GetStringValueFromBool(aftInfo.CanDepositCashable)
                                    + GetStringValueFromBool(aftInfo.PinRequired)
                                    + GetStringValueFromBool(aftInfo.EcashPlayer);

                            ExCommsDataContext.Current.InsertPlayerInformation(aftInfo.CardNumber.ConvertToInt32(), aftInfo.FirstName,
                                aftInfo.LastName, aftInfo.PointsBalance, aftInfo.VIPFlag, aftInfo.GameCapLevel);
                            method.Info("AFT flag entire string " + sMsg);

                            sMsg = (!String.IsNullOrEmpty(strCMPResponse)) ? (sMsg + strCMPResponse) : sMsg;
                            method.Info("Time taken to send msg to iView" + DateTime.Now.TimeOfDay.ToString());
                        }
                        else
                        {
                            sMsg = "00000000" + "0000000" + "00000000";
                            sMsg += "Welcome";
                        }
                        method.Info("Installation Number 2" + installationNo);
                        //++k EPIMsgProcessor.SendCommand
                        method.Info("Player Card in - Update the display message in playergatewaymessages table");
                        ExCommsDataContext.Current.UpdateDisplayMessageForEFT(installationNo, aftInfo.CardNumber, (Guid)requestId, sMsg);
                        ExCommsDataContext.Current.UpdateDisplayMessageForPDResponse(BmcConsants.PD.TYPE, BmcConsants.PD.CODE_ADD_UPDATE_EX, aftInfo.CardNumber, strCMPResponse);

                    }
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }
            }
        }

        #endregion

        #region Player Card Out
        internal bool ProcessPlayerCardOut(MonMsg_G2H message, MonTgt_G2H_Status_PlayerCardOut monitorTarget)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "ProcessPlayerCardOut"))
            {
                bool result = default(bool);

                try
                {
                    // variables
                    InstallationDetailsForMSMQ dbData = message.Extra as InstallationDetailsForMSMQ;
                    int cardNoInt = message.CardNumber.ConvertToInt32();
                    EPIMeterValueTypes meterValueType = EPIMeterValueTypes.Diff;

                    // get the card in detail
                    EPICardDetail cardDetail = CurrentEPIManager.AddOrUpdateCardInDetail(message.InstallationNo, message.CardNumber);

                    // When a player card has been removed from the card-reader, update the floor_status table, setting the EPIDetails field to NULL. 
                    ExCommsDataContext.Current.UpdateFloorStatus(message.InstallationNo, DateTime.Now);

                    // Send the precommitment status
                    if (_configStore.PreCommitMentEnabled)
                    {
                        this.SendPreCommitmentStats(SESRATING_PROCESS_CARD_OUT, message);
                    }

                    // player card out
                    if (this.ProcessPlayerCardSessionRatings(dbData, SESRATING_PROCESS_CARD_OUT, message.InstallationNo))
                    {
                        cardDetail.Clear();
                        CurrentEPIManager.RemoveTimeoutsIfExists(message.InstallationNo);
                    }
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }

                return result;
            }
        }
        #endregion

        #region Player Session Ratings
        internal bool ProcessPlayerCardSessionRatings(int installationNo)
        {
            return this.ProcessPlayerCardSessionRatings(ModuleHelper.Current.GetInstallationFromCache(string.Empty, installationNo),
                SESRATING_PROCESS_INTERVAL_RATING, installationNo);
        }

        private bool ProcessPlayerCardSessionRatings(InstallationDetailsForMSMQ dbData, string cardOutType, int installationNo)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "ProcessPlayerCardSessionRatings"))
            {
                bool result = default(bool);

                try
                {
                    // get thec card detail
                    EPICardDetail cardDetail = EPIManager.Current.GetCardInDetail(installationNo);
                    bool isIR = (cardOutType == SESRATING_PROCESS_INTERVAL_RATING);
                    DateTime sessionStartDate = DateTime.Now;
                    EPIMeterValueTypes meterValueType = EPIMeterValueTypes.Diff;

                    // no card in or invalid card in
                    if (cardDetail == null || cardDetail.CardNo.IsEmpty())
                    {
                        method.Info("Invalid card in session found.");
                        return false;
                    }

                    int cardNoInt = cardDetail.CardNo.ConvertToInt32();
                    method.InfoV("### Player Card Session ({0}) Started for {1}", cardOutType, cardDetail.CardNo);

                    // get the floor financial latest meters
                    if (!cardDetail.GetLatestMeters(installationNo, EPIMeterValueTypes.Current)) return false;
                    method.Info("Player Card Session Ratings (Current) : " + Environment.NewLine + cardDetail.ToString());

                    // get the session delta values
                    DataRow drSessionDelta = ExCommsDataContext.Current.GetMeterDeltaForPlayerSession(installationNo, cardDetail.CardNo,
                            (isIR ?
                            (cardDetail.IsFirstCardIn ? SESRATING_PROCESS_FIRST_INTERVAL_RATING : SESRATING_PROCESS_INTERVAL_RATING) :
                            cardOutType));
                    if (drSessionDelta != null)
                    {
                        if (drSessionDelta["SessionStartDate"] != DBNull.Value) sessionStartDate = drSessionDelta.Field<DateTime>("SessionStartDate");
                        cardDetail.CopyMeterValuesFromDB(drSessionDelta, EPIMeterValueTypes.Diff);
                        method.Info(string.Format("Player Card Session Ratings (Difference / {0}) : ", sessionStartDate) + Environment.NewLine + cardDetail.ToString());
                    }

                    if (cardOutType == SESRATING_PROCESS_CARD_OUT)
                    {
                        // remove the player session rating
                        CurrentDataContext.RemovePlayerSessionRating(installationNo, cardNoInt);
                        cardDetail.EPISessionStart = DateTime.Now;
                        method.Info("Record removed in Player session rating");
                    }

                    if (cardOutType == SESRATING_PROCESS_INTERVAL_RATING)
                    {
                        // start the interval rating
                        CurrentDataContext.InsertPlayerSessionRating(installationNo, cardNoInt, SESRATING_PROCESS_INTERVAL_RATING, SESRATING_TYPE_P);
                        method.Info("Record inserted in Player session rating");
                    }

                    // log the meter values
                    method.Info("Player Card Session Ratings (Current) : " + Environment.NewLine + cardDetail.ToString());

                    // save the installation no against card no
                    HandlerHelper.Current.SaveSDTRequest(cardDetail.CardNo, installationNo);

                    // asset no
                    string assetNo = dbData.Stock_No;
                    if (_configStore.IsStockPrefixRequired &&
                        HandlerHelper.Current.IsAlphaNumeric(assetNo))
                    {
                        assetNo = assetNo.Substring(0, _configStore.Stock_Code_Prefix.Length);
                    }

                    // invoke the player card out on player gateway

                    PlayerCardOutRequest sdtRequest = isIR ? new IntervalRatingRequest() : new PlayerCardOutRequest();
                    DateTime now = DateTime.Now;
                    sdtRequest.Asset = assetNo;
                    sdtRequest.BarPosition = dbData.Bar_Pos_Name;
                    sdtRequest.CardNo = cardDetail.CardNo;
                    sdtRequest.StartDate = sessionStartDate.Date;
                    sdtRequest.StartTime = sessionStartDate.TimeOfDay;
                    sdtRequest.EndDate = now.Date;
                    sdtRequest.EndTime = now.TimeOfDay;
                    sdtRequest.CoinsIn = cardDetail[meterValueType, EPIMeterTypes.CoinsIn];
                    sdtRequest.CoinsOut = cardDetail[meterValueType, EPIMeterTypes.CoinsOut];
                    sdtRequest.GamesLost = cardDetail[meterValueType, EPIMeterTypes.GamesLost];
                    sdtRequest.GamesPlayed = cardDetail[meterValueType, EPIMeterTypes.GamesPlayed];
                    sdtRequest.GamesWon = cardDetail[meterValueType, EPIMeterTypes.GamesWon];
                    sdtRequest.HPJackpot = cardDetail[meterValueType, EPIMeterTypes.Jackpot];
                    sdtRequest.BillsIn = cardDetail[meterValueType, EPIMeterTypes.BillsIn];
                    sdtRequest.VouchersIn = cardDetail[meterValueType, EPIMeterTypes.VouchersIn];
                    sdtRequest.cashableDeposits = cardDetail[meterValueType, EPIMeterTypes.CashableEFTOut];
                    sdtRequest.cashableFunds = cardDetail[meterValueType, EPIMeterTypes.CashableEFTIn];
                    sdtRequest.NoncashableDeposits = cardDetail[meterValueType, EPIMeterTypes.NonCashableEFTOut];
                    sdtRequest.NoncashableFunds = cardDetail[meterValueType, EPIMeterTypes.NonCashableEFTIn];
                    sdtRequest.Denomination = checked((short)Math.Round((double)dbData.Installation_Price_Of_Play));
                    sdtRequest.HoldPercentage = checked((short)Math.Round((double)unchecked(dbData.Anticipated_Percentage_Payout * 100.0)));
                    sdtRequest.InstallationNo = installationNo;
                    sdtRequest.ManufacturerID = string.Empty;
                    sdtRequest.SiteCode = dbData.SiteCode;
                    sdtRequest.Zone = dbData.Zone_Name;
                    sdtRequest.GameType = dbData.GameType.ToString();
                    // should get from db
                    sdtRequest.PayTableID = "0";
                    sdtRequest.CurrentDenomination = "1";
                    sdtRequest.GameName = dbData.Name;

                    // send the request
                    RequestAckMessage requestAckMessage = null;
                    if (isIR)
                        requestAckMessage = HandlerHelper.PlayerGatewayInstance.IntervalRating(sdtRequest as IntervalRatingRequest,
                            new ResponseCallback<IntervalRatingResponse>(this.ProcessAFTInformation<IntervalRatingResponse>));
                    else
                        requestAckMessage = HandlerHelper.PlayerGatewayInstance.PlayerCardOut(sdtRequest,
                            new ResponseCallback<PlayerCardOutResponse>(this.ProcessAFTInformation<PlayerCardOutResponse>));
                    method.Info("Time taken to send message to Gateway for Card Out : " + DateTime.Now.TimeOfDay.ToString());
                    result = true;
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }

                return result;
            }
        }
        #endregion

        #region Balance request

        internal bool ProcessBalanceRequest(MonMsg_G2H message, MonTgt_G2H_EFT_BalanceRequest monitorTarget)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "ProcessBalanceRequest"))
            {
                bool result = default(bool);
                InstallationDetailsForMSMQ dbData = null;
                EPICardDetail cardDetail = null;

                try
                {
                    // variables
                    dbData = message.Extra as InstallationDetailsForMSMQ;
                    int cardNoInt = message.CardNumber.ConvertToInt32();

                    // create the process
                    EPIManager.Current.CreateProcess(message.InstallationNo);

                    // get the card detail
                    cardDetail = EPIManager.Current.AddOrUpdateCardInDetail(message.InstallationNo, message.CardNumber);

                    // invoke the player card in on player gateway
                    DateTime transDate = DateTime.Now;
                    BalanceRequest sdtRequest = new BalanceRequest
                    {
                        BarPosition = dbData.Bar_Pos_Name,
                        CardNo = message.CardNumber,
                        EncryptedPin = CryptoHelper.EncryptAsciiString(monitorTarget.Pin, string.Empty),
                        InstallationNo = message.InstallationNo,
                        SlotIndex = dbData.Bar_Pos_Name,
                        SlotNumber = dbData.Bar_Pos_Name,
                        Stand = this.GetAssetNo(dbData),
                        TransactionDate = transDate.ToString("yyyyMMdd").PadLeft(8, '0'),
                        TransactionTime = transDate.ToString("HHmmss").PadLeft(6, '0'),
                        TransactionID = HandlerHelper.Current.NextPTRequestID().ToString(),
                        SiteCode = message.SiteCode
                    };

                    // send the request
                    RequestAckMessage requestAckMessage = HandlerHelper.PlayerGatewayInstance.BalanceRequest(sdtRequest,
                        new ResponseCallback<BalanceResponse>(this.ProcessAFTInformation<BalanceResponse>));
                    result = true;
                    method.Info("BALREQ : Time taken to receive ack from Gateway : " + DateTime.Now.TimeOfDay.ToString());
                }
                catch (Exception ex)
                {
                    this.ProcessException(ex, cardDetail, message, dbData);
                }

                return result;
            }
        }

        #endregion //Balance request

        #region Balance Response

        private void ProcessBalanceResponse(AFTInformation aftInfo, object requestId, bool returnStauts)
        {
            using (ILogMethod method = Log.LogMethod("SDTMessages", "Process_BalanceResponse"))
            {
                try
                {
                    // get the card detail
                    var cardDetail = EPIManager.Current.GetCardInDetailByCardNo(aftInfo.CardNumber);
                    if (cardDetail == null)
                    {
                        Log.Info("BALREQ : No card in .. Exiting");
                        return;
                    }

                    string _msg = string.Empty;
                    MonTgt_H2G_EFT_BalanceResponse monitorTarget = CreateBalanceResponse(aftInfo);
                    monitorTarget.PlayerFlags.Clear();

                    if (returnStauts)
                    {
                        ModuleHelper.Current.CopyTo(aftInfo, monitorTarget.PlayerFlags);
                        string flags = monitorTarget.PlayerFlags.ToStringSafe();
                        method.Info("Process_BalanceResponse AFT flag string " + flags);

                        _msg += flags + aftInfo.CardNumber + aftInfo.DisplayMessage;
                        method.Info("Message to be displayed in iView " + _msg);
                    }
                    else
                    {
                        _msg += monitorTarget.PlayerFlags.ToStringSafe() + aftInfo.CardNumber + "Communication Error";
                    }
                    
                    EPIMsgProcessor.Current.SendCommand(cardDetail.InstallationNo, monitorTarget);
                    ExCommsDataContext.Current.UpdateDisplayMessageForEFT(cardDetail.InstallationNo, aftInfo.CardNumber, Guid.Empty, aftInfo.DisplayMessage);
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }
            }
        }

        private MonTgt_H2G_EFT_BalanceResponse CreateBalanceResponse(AFTInformation aftInfo)
        {
            using (ILogMethod method = Log.LogMethod("SDTMessages", "GetMonitorBalanceRequestEntity"))
            {
                MonTgt_H2G_EFT_BalanceResponse monitorTarget = null;
                try
                {
                    monitorTarget = new MonTgt_H2G_EFT_BalanceResponse
                    {
                        PlayerCardNumber = aftInfo.CardNumber,
                        DisplayMessageLength = Convert.ToByte(aftInfo.DisplayMessage.Length),
                        DisplayMessage = aftInfo.DisplayMessage
                    };
                    monitorTarget.PlayerFlags.Clear();
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }
                return monitorTarget;
            }
        }

        #endregion //Balance Response

        #region Withdraw Request

        internal bool ProcessWithdrawRequest(MonMsg_G2H message, MonTgt_G2H_EFT_WithdrawalRequest monWithdrawalRequest)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "ProcessWithdrawRequest"))
            {
                bool result = default(bool);
                InstallationDetailsForMSMQ dbData = null;
                EPICardDetail cardDetail = null;

                try
                {
                    // variables
                    dbData = message.Extra as InstallationDetailsForMSMQ;
                    int cardNoInt = message.CardNumber.ConvertToInt32();

                    // create the process
                    EPIManager.Current.CreateProcess(message.InstallationNo);

                    // get the card detail
                    cardDetail = EPIManager.Current.AddOrUpdateCardInDetail(message.InstallationNo, message.CardNumber);
                    // asset no
                    string assetNo = this.GetAssetNo(dbData);

                    // invoke the player card in on player gateway
                    DateTime transDate = DateTime.Now;

                    WithdrawalRequest withdrawalRequest = new WithdrawalRequest
                    {
                        AccountType = Convert.ToInt32(monWithdrawalRequest.AccountType),
                        AmountRequested = Convert.ToInt32((monWithdrawalRequest.AmountRequested * 100)),
                        //Authentication = withdrawalRequest.Authentication,
                        BarPosition = dbData.Bar_Pos_Name,
                        CardNo = monWithdrawalRequest.CardNumber,
                        EncryptedPin = CryptoHelper.EncryptAsciiString(monWithdrawalRequest.Pin, string.Empty),
                        InstallationNo = message.InstallationNo,
                        SlotNumber = assetNo,
                        Stand = dbData.Bar_Pos_Name,
                        SlotIndex = dbData.Bar_Pos_Name,
                        TransactionDate = transDate.ToString("yyyyMMdd").PadLeft(8, '0'),
                        TransactionID = dbData.TransactionID.ToString(),
                        TransactionTime = transDate.ToString("HHmmss").PadLeft(6, '0'),
                        SiteCode = message.SiteCode
                    };

                    // send the request
                    RequestAckMessage requestAckMessage = HandlerHelper.PlayerGatewayInstance.WithDrawalRequest(withdrawalRequest,
                        new ResponseCallback<WithdrawalResponse>(this.ProcessAFTInformation<WithdrawalResponse>));
                    result = true;
                    method.Info("WITHDrawREQ : Time taken to receive ack from Gateway : " + DateTime.Now.TimeOfDay.ToString());
                }
                catch (Exception ex)
                {
                    this.ProcessException(ex, cardDetail, message, dbData);
                }

                return result;
            }
        }

        #endregion //Withdraw Request

        #region Withdraw Response

        private void Process_WithdrawResponse(AFTInformation aftInfo, object requestId, bool returnStauts)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "ProcessWithdrawRequest"))
            {
                //TO DO
                //Get Installation
                //Account Type
                try
                {
                    if (aftInfo == null)
                        throw new NullReferenceException("Withdraw Response is NULL");

                    string msg = string.Empty;
                    int installationNo = GetSDTRequest(aftInfo.CardNumber);
                    GatewayMessageStructures.AFTAuditHistory auditHistory = new GatewayMessageStructures.AFTAuditHistory();
                    MonTgt_H2G_EFT_WithdrawalAuthorization monitorTarget = GetMonitorWithdrawalAuthorizationEntity(aftInfo);

                    if (aftInfo.ErrorCode == 0 && returnStauts)
                    {
                        ModuleHelper.Current.CopyTo(aftInfo, monitorTarget.PlayerFlags, true);
                        string flags = monitorTarget.PlayerFlags.ToStringSafe();
                        method.Info("Process_BalanceResponse AFT flag string " + flags);

                        msg += flags + aftInfo.CardNumber + aftInfo.DisplayMessage;
                        method.Info("Message to be displayed in iView " + msg);

                        Log.Info("Time taken to receive message from Gateway" + DateTime.Now.TimeOfDay.ToString());

                        auditHistory = GetAuditHistoryDetails(aftInfo, true, true, false, MonitorConstants.WITHDRAWALREQUEST_TRANSACTIONTYPE);
                        Log.Info("Non cashable amount " + aftInfo.NonCashableFunds.ToString());
                        Log.Info("Cashable amount " + aftInfo.CashableFunds.ToString());
                    }
                    else
                    {
                        msg += monitorTarget.PlayerFlags.ToStringSafe() + aftInfo.CardNumber + "Communication Error";
                        auditHistory = GetAuditHistoryDetails(aftInfo, false, false, false, MonitorConstants.WITHDRAWALREQUEST_TRANSACTIONTYPE);
                    }

                    DoDBTransaction(aftInfo, auditHistory, MonitorConstants.WITHDRAWALRESPONSE_TRANSACTIONTYPE);

                    Log.Info("Message to Be displayed in iView " + monitorTarget);
                    EPIMsgProcessor.Current.SendEPIMessage(installationNo, MessagePriority.NORMAL, monitorTarget);
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }
            }
        }

        private MonTgt_H2G_EFT_WithdrawalAuthorization GetMonitorWithdrawalAuthorizationEntity(AFTInformation aftInfo)
        {
            MonTgt_H2G_EFT_WithdrawalAuthorization monWithdrawalResponse = new MonTgt_H2G_EFT_WithdrawalAuthorization
            {
                NonCashableAmount = aftInfo.NonCashableFunds,
                CashableAmount = aftInfo.CashableFunds,
                ErrorCode = Convert.ToByte(aftInfo.ErrorCode),
                PlayerCardNumber = aftInfo.CardNumber,
                //TO DO
                //PlayerFlags = 
                DisplayMessageLength = Convert.ToByte(aftInfo.DisplayMessage.Length),
                DisplayMessage = aftInfo.DisplayMessage
            };
            return monWithdrawalResponse;
        }

        #endregion //Withdraw Response

        #region Withdraw Complete

        internal bool ProcessWithdrawComplete(MonMsg_G2H message, MonTgt_G2H_EFT_WithdrawalComplete monwithdrawalComplete)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "ProcessWithdrawRequest"))
            {
                bool result = default(bool);
                InstallationDetailsForMSMQ dbData = null;
                EPICardDetail cardDetail = null;

                try
                {
                    // variables
                    dbData = message.Extra as InstallationDetailsForMSMQ;
                    int cardNoInt = message.CardNumber.ConvertToInt32();
                    
                    // create the process
                    EPIManager.Current.CreateProcess(message.InstallationNo);

                    // get the card detail
                    cardDetail = EPIManager.Current.AddOrUpdateCardInDetail(message.InstallationNo, message.CardNumber);
                    // asset no
                    string assetNo = this.GetAssetNo(dbData);

                    // invoke the player card in on player gateway
                    DateTime transDate = DateTime.Now;

                    WithdrawalRequest WithdrawResp = new WithdrawalRequest
                    {
                        AccountType = Convert.ToInt32(HandlerHelper.Current.GetSDTAccountType(monwithdrawalComplete.CardNumber)),
                        CashableFunds = monwithdrawalComplete.CashableAmount,
                        NonCashableFunds = monwithdrawalComplete.NonCashableAmount,
                        //Authentication = ,
                        BarPosition = dbData.Bar_Pos_Name,
                        CardNo = monwithdrawalComplete.CardNumber,
                        SlotIndex = dbData.Bar_Pos_Name,
                        ErrorCode = monwithdrawalComplete.CMPErrorCode,
                        Stand = dbData.Bar_Pos_Name,
                        SlotNumber = assetNo,
                        TransactionDate = transDate.ToString("yyyyMMdd").PadLeft(8, '0'),
                        TransactionTime = transDate.ToString("HHmmss").PadLeft(6, '0'),
                        TransactionID = dbData.TransactionID.ToString(),
                        SiteCode = message.SiteCode
                    };

                    // send the request
                    RequestAckMessage requestAckMessage = HandlerHelper.PlayerGatewayInstance.WithDrawalComplete(WithdrawResp,
                        new ResponseCallback<WithdrawalCompleteResponse>(this.ProcessAFTInformation<WithdrawalCompleteResponse>));
                    result = true;
                    method.Info("WITHDrawCOMPLETE : Time taken to receive ack from Gateway : " + DateTime.Now.TimeOfDay.ToString());
                }
                catch (Exception ex)
                {
                    this.ProcessException(ex, cardDetail, message, dbData);
                }

                return result;
            }
        }

        #endregion Withdraw Complete

        #region Withdraw Complete Response

        private void Process_WithdrawCompleteResponse(AFTInformation aftInfo, object requestId, bool returnStauts)
        {
            Log.Info("Received Withdrawal Complete Response");
            if (aftInfo == null)
                throw new NullReferenceException("Withdrawal Response is NULL");

            string playerFlags = string.Empty;
            int installationNo = GetSDTRequest(aftInfo.CardNumber);
            GatewayMessageStructures.AFTAuditHistory auditHistory = new GatewayMessageStructures.AFTAuditHistory();
            MonTgt_H2G_EFT_WithdrawalAcknowledgement monWithdrawalAck = GetMonitorWithdrawalAcknowledgementEntity(aftInfo);

            if (returnStauts)
            {
                Log.Info("Cash In Commit Message Received From Gateway " + aftInfo.DisplayMessage);
                Log.Info("Time taken to receive message from Gateway " + DateTime.Now.TimeOfDay.ToString());

                ModuleHelper.Current.CopyTo(aftInfo, monWithdrawalAck.PlayerFlags, true);
                playerFlags = monWithdrawalAck.PlayerFlags.ToStringSafe();
                auditHistory = GetAuditHistoryDetails(aftInfo, true, true, true, MonitorConstants.WITHDRAWALCOMPLETE_TRANSACTIONTYPE);

                Log.Info("Non cashable amount " + aftInfo.NonCashableFunds.ToString());
                Log.Info("Cashable amount " + aftInfo.CashableFunds.ToString());

                Log.Info("Transfer ID equal " + aftInfo.ErrorCode + aftInfo.DisplayMessage);
                string commitMsg = aftInfo.CardNumber + playerFlags + aftInfo.DisplayMessage;
                if (aftInfo.ErrorCode == 0)
                {
                    Log.Info("No error " + aftInfo.DisplayMessage);
                    Log.Info("Message to Be displayed in iView " + commitMsg);
                }
                else if (aftInfo.ErrorCode > 0)
                {
                    Log.Info("Commit message " + commitMsg);
                }
            }
            else
            {
                Log.Info("Time out");
                string msg = monWithdrawalAck.PlayerFlags.ToStringSafe() + aftInfo.CardNumber + "Communication Error";
                string dispMessage = string.IsNullOrEmpty(aftInfo.DisplayMessage) ? "Could not complete your transaction, Please refere Club Booth..." : aftInfo.DisplayMessage;
                string commitMsg = aftInfo.CardNumber + playerFlags + aftInfo.DisplayMessage;

                Log.Info("Inserting AFT Transactions ...");
                auditHistory = GetAuditHistoryDetails(aftInfo, false, false, true, MonitorConstants.WITHDRAWALCOMPLETE_TRANSACTIONTYPE);
                Log.Info("Message to Be displayed in iView " + commitMsg);
            }

            DoDBTransaction(aftInfo, auditHistory, MonitorConstants.WITHDRAWALCOMPLETE_TRANSACTIONTYPE);
            EPIMsgProcessor.Current.SendEPIMessage(installationNo, MessagePriority.NORMAL, monWithdrawalAck);
            HandlerHelper.Current.GetWithDrawalErrorCode(aftInfo.CardNumber, true);
        }

        private MonTgt_H2G_EFT_WithdrawalAcknowledgement GetMonitorWithdrawalAcknowledgementEntity(AFTInformation aftInfo)
        {
            MonTgt_H2G_EFT_WithdrawalAcknowledgement monWithdrawalAck = new MonTgt_H2G_EFT_WithdrawalAcknowledgement
            {
                PlayerCardNumber = aftInfo.CardNumber,
                //TO DO
                //PlayerFlags = 
                DisplayMessageLength = Convert.ToByte(aftInfo.DisplayMessage.Length),
                DisplayMessage = aftInfo.DisplayMessage
            };
            return monWithdrawalAck;
        }

        #endregion //Withdraw Complete Response

        #region Deposit Response

        private void Process_DepositResponse(AFTInformation aftInfo, object requestId, bool returnStauts)
        {
            Log.Info("Inside Deposit response");

            if (aftInfo == null)
                throw new NullReferenceException("Deposit Response is NULL");

            string playerFlags = string.Empty;
            string depositRequestMsg = string.Empty;
            int installationNo = GetSDTRequest(aftInfo.CardNumber);
            GatewayMessageStructures.AFTAuditHistory auditHistory = new GatewayMessageStructures.AFTAuditHistory();

            if (returnStauts)
            {
                playerFlags = GetPlayerFlagMsgOnSuccess(aftInfo, false, true);
                depositRequestMsg = aftInfo.ErrorCode.ToString().PadLeft(3, '0') + aftInfo.CardNumber + playerFlags + aftInfo.DisplayMessage;

                auditHistory = GetAuditHistoryDetails(aftInfo, true, true, false, MonitorConstants.DEPOSITREQUEST_TRANSACTIONTYPE);
                // Display Message and Cashable Amount clarification
                Log.Info("AFT Details - Noncashable = " + auditHistory.NoncashabledepAmt.ToString() + " Cashable =" + auditHistory.WATAmt.ToString());
            }
            else
            {
                playerFlags = "00000000" + "0000001" + "111111111";
                string displayMessage = string.IsNullOrEmpty(aftInfo.DisplayMessage) ? "Could not complete your transaction, Please refere Club Booth..." : aftInfo.DisplayMessage;
                depositRequestMsg = aftInfo.ErrorCode.ToString().PadLeft(3, '0') + aftInfo.CardNumber + playerFlags + aftInfo.DisplayMessage;

                Log.Info("Inserting AFT Transactions ...");
                auditHistory = GetAuditHistoryDetails(aftInfo, false, false, false, MonitorConstants.DEPOSITREQUEST_TRANSACTIONTYPE);
            }

            DoDBTransaction(aftInfo, auditHistory, MonitorConstants.DEPOSITRESPONSE_TRANSACTIONTYPE);
            MonTgt_H2G_EFT_DepositAuthorization monDepositAuth = GetMonitorDepositAuthorizationEntity(aftInfo);
            EPIMsgProcessor.Current.SendEPIMessage(installationNo, MessagePriority.NORMAL, monDepositAuth);
        }

        private MonTgt_H2G_EFT_DepositAuthorization GetMonitorDepositAuthorizationEntity(AFTInformation aftInfo)
        {
            MonTgt_H2G_EFT_DepositAuthorization monDepositAuth = new MonTgt_H2G_EFT_DepositAuthorization
            {
                ErrorCode = Convert.ToByte(aftInfo.ErrorCode),
                PlayerCardNumber = aftInfo.CardNumber,
                // TO DO
                //PlayerFlags = ,
                DisplayMessageLength = Convert.ToByte(aftInfo.DisplayMessage.Length),
                DisplayMessage = aftInfo.DisplayMessage
            };
            return monDepositAuth;
        }

        #endregion //Deposit Response

        #region Deposit Complete

        private void Process_DepositCompleteResponse(AFTInformation aftInfo, object requestId, bool returnStauts)
        {
            if (aftInfo == null)
                throw new NullReferenceException("Deposit Complete is NULL");

            Log.Info("Deposit Message Received From Gateway" + aftInfo.DisplayMessage);

            string playerFlags = string.Empty;
            string depositCompleteMsg = string.Empty;
            int installationNo = GetSDTRequest(aftInfo.CardNumber);
            GatewayMessageStructures.AFTAuditHistory auditHistory = new GatewayMessageStructures.AFTAuditHistory();
            if (returnStauts)
            {
                playerFlags = GetPlayerFlagMsgOnSuccess(aftInfo, false, true);
                depositCompleteMsg = aftInfo.CardNumber + playerFlags + aftInfo.DisplayMessage;
                auditHistory = GetAuditHistoryDetails(aftInfo, true, true, false, MonitorConstants.DEPOSITCOMPLETE_TRANSACTIONTYPE);
                // Display Message and Cashable Amount clarification


                Log.Info("Message to be displayed in iView" + aftInfo.DisplayMessage);
                Log.Info("Account type" + auditHistory.AccountType + " Cashable " + auditHistory.WATAmt);
                Log.Info("Message to be displayed in iView" + depositCompleteMsg);
            }
            else
            {
                Log.Info("Timeout");
                string displayMessage = string.IsNullOrEmpty(aftInfo.DisplayMessage) ? "Could not complete your transaction, Please refere Club Booth..." : aftInfo.DisplayMessage;
                playerFlags = string.Concat("00000000", "0000001", "00000000");
                depositCompleteMsg = string.Concat(aftInfo.CardNumber, playerFlags, displayMessage);

                auditHistory = GetAuditHistoryDetails(aftInfo, false, false, false, MonitorConstants.DEPOSITCOMPLETE_TRANSACTIONTYPE);
                Log.Info("Else part Cashable " + auditHistory.WATAmt.ToString() + " NonCashable " + auditHistory.NoncashableAmt.ToString());
            }

            DoDBTransaction(aftInfo, auditHistory, MonitorConstants.DEPOSITCOMPLETERESPONSE);
            MonTgt_H2G_EFT_DepositAcknowledgement monDepositAck = GetMonitorDepositAcknowledgementEntity(aftInfo);
            EPIMsgProcessor.Current.SendEPIMessage(installationNo, MessagePriority.NORMAL, monDepositAck);

            HandlerHelper.Current.GetSDTAccountType(aftInfo.CardNumber, true);
        }

        private MonTgt_H2G_EFT_DepositAcknowledgement GetMonitorDepositAcknowledgementEntity(AFTInformation aftInfo)
        {
            MonTgt_H2G_EFT_DepositAcknowledgement monDepositAuth = new MonTgt_H2G_EFT_DepositAcknowledgement
            {
                PlayerCardNumber = aftInfo.CardNumber,
                //PlayerFlags = ,
                DisplayMessageLength = Convert.ToByte(aftInfo.DisplayMessage.Length),
                DisplayMessage = aftInfo.DisplayMessage
            };
            return monDepositAuth;
        }

        #endregion //Deposit Complete

        #region Precommitment
        private bool SendPreCommitmentStats(string cardOutType, MonMsg_G2H message)
        {
            return false;
        }
        #endregion

        #region Game Capping

        public void Process_GameCapAuthenticationResponse(AFTInformation aftInfo, object requestId, bool returnStatus)
        {
            using (ILogMethod method = Log.LogMethod("SDTMessages", "GetMonitorBalanceRequestEntity"))
            {
                try
                {
                    ProcessGameCappingAuthenticationResp(false, aftInfo, returnStatus);
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }
            }
        }

        public void Process_GameUnCapAuthenticationResponse(AFTInformation aftInfo, object requestId, bool returnStatus)
        {
            using (ILogMethod method = Log.LogMethod("SDTMessages", "GetMonitorBalanceRequestEntity"))
            {
                try
                {
                    ProcessGameCappingAuthenticationResp(true, aftInfo, returnStatus);
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }
            }
        }

        private void ProcessGameCappingAuthenticationResp(bool isGameUnCapping, AFTInformation aftInfo, bool returnStatus)
        {
            try
            {
                IGameCapping gameCapping = GameCapping.GetInstance();
                GameCapInformation gameCapInformation = null;
                MonTgt_H2G_GameCapping monTgt_H2G_GameCapping = null;
                string message = string.Empty;

                //Get Installation
                int installationNo = HandlerHelper.Current.GetSDTRequest(aftInfo.CardNumber, false);
                bool validPin = false;

                //Need to set global vairable

                if (returnStatus && installationNo > 0 && aftInfo.ErrorCode == 0)
                {
                    gameCapInformation = gameCapping.GetGameCappingInformation(installationNo, aftInfo.CardNumber, isGameUnCapping);
                    validPin = true;
                }

                if (validPin)
                    monTgt_H2G_GameCapping = gameCapping.SetGameCappingMessage(gameCapInformation, validPin);
                else
                {
                    message = aftInfo.ErrorCode > 0 ? aftInfo.DisplayMessage.Replace("~", string.Empty) : (isGameUnCapping ? "Un-Capping" : "Capping") + " is not allowed";

                    monTgt_H2G_GameCapping = gameCapping.SetGameCappingMessage(new GameCapInformation()
                    {
                        Message = message
                    }, validPin);
                }

                if (gameCapping.SendCapInformationToGMU(installationNo, monTgt_H2G_GameCapping) && validPin)
                {
                    gameCapping.StartEndGameCappingSession(isGameUnCapping, installationNo, gameCapInformation.Asset, gameCapInformation.Position, aftInfo.CardNumber, aftInfo.CardNumber);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion Game Capping

        #region Jackpot Event

        internal bool ProcessJackpotEvent(MonMsg_G2H message, MonTgt_G2H_HPJP monTgt_G2H_HPJP)
        {
            InstallationDetailsForMSMQ dbData = null;
            try
            {
                dbData = message.Extra as InstallationDetailsForMSMQ;
                string assetNo = this.GetAssetNo(dbData);

                PlayerCardOutRequest entity = GetJackpotEntity(message, dbData, monTgt_G2H_HPJP, assetNo);
                RequestAckMessage requestAckMessage = HandlerHelper.PlayerGatewayInstance.Jackpot(entity,
                        new ResponseCallback<PlayerCardOutResponse>(this.ProcessAFTInformation<PlayerCardOutResponse>));

                if (HandlerHelper.Current.IsPreCommitmentEnabled)
                {
                    PCHandPaidJackpot pcJackpotEntity = GetPCJackpotEntity(message, dbData, monTgt_G2H_HPJP, assetNo);
                    HandlerHelper.PlayerGatewayInstance.PCHandPaidJackpotDM(pcJackpotEntity);
                }

                return true;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return false;
            }
        }

        private PlayerCardOutRequest GetJackpotEntity(MonMsg_G2H message, InstallationDetailsForMSMQ dbData, MonTgt_G2H_HPJP monTgt_G2H_HPJP, string assetNo)
        {
            PlayerCardOutRequest cardout = new PlayerCardOutRequest()
            {
                CardNo = message.CardNumber,
                BarPosition = dbData.Bar_Pos_Name,
                ManufacturerID = dbData.Bar_Pos_Name,
                HoldPercentage = Convert.ToInt16(dbData.Anticipated_Percentage_Payout * 100),
                Zone = dbData.Zone_Name,
                Denomination = Convert.ToInt16(dbData.Installation_Price_Of_Play),
                CoinsIn = Convert.ToInt64(monTgt_G2H_HPJP.CoinIn),
                CoinsOut = Convert.ToInt64(monTgt_G2H_HPJP.CoinOut),
                GamesPlayed = monTgt_G2H_HPJP.GamesPlayed,
                GamesWon = monTgt_G2H_HPJP.GamesWon,
                HPJackpot = Convert.ToInt64(monTgt_G2H_HPJP.Amount),
                //GameName = ,
                Asset = assetNo,
                GameType = dbData.GameType.ToString(),
                SiteCode = dbData.SiteCode,
                InstallationNo = dbData.Installation_No
                //PayTableID = 
                //CurrentDenomination = 
            };
            return cardout;
        }

        private PCHandPaidJackpot GetPCJackpotEntity(MonMsg_G2H message, InstallationDetailsForMSMQ dbData, MonTgt_G2H_HPJP monTgt_G2H_HPJP, string assetNo)
        {
            DateTime transDate = DateTime.Now;
            PCHandPaidJackpot pcJackpotEntity= new PCHandPaidJackpot()
            {
                CardNo = message.CardNumber,
                CardLength = message.CardNumber.Length,
                SlotNo = assetNo,
                Stand = dbData.Bar_Pos_Name,
                EventDate = transDate.Date.ToString("yyyyMMdd"),
                EventTime = transDate.ToString("HHmmss"),
                HPJackpots = Convert.ToInt64(monTgt_G2H_HPJP.Amount),
                SiteCode = dbData.SiteCode,
                BarPosition = dbData.Bar_Pos_Name,
                Asset = assetNo
            };
            return pcJackpotEntity;
        }

        #endregion //Jackpot Event
    }
}
