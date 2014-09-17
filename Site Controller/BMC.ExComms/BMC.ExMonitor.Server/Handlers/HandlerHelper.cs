using BMC.CoreLib;
using BMC.ExComms.Contracts.Configuration;
using BMC.ExComms.DataLayer.MSSQL;
using BMC.PlayerGateway.SDT;
using BMC.PlayerGateway.Gateway;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using BMC.ExComms.Contracts.DTO.Freeform;
using BMC.CoreLib.Collections;
using BMC.PlayerGateway;
using BMC.Common.Utilities;

namespace BMC.ExMonitor.Server.Handlers
{
    public class HandlerHelper
        : DisposableObject
    {

        #region Private Membser

        private static readonly SingletonHelperBase<HandlerHelper> _singletonHelper = new SingletonHelper<HandlerHelper>(
                new Lazy<HandlerHelper>(() => new HandlerHelper()));

        private static readonly SingletonHelperBase<IPlayerGateway> _playerGatewayGlobal = new SingletonHelper<IPlayerGateway>(
                new Lazy<IPlayerGateway>(() => { return InitPlayerGateway(); }));
        private static readonly SingletonHelperBase<IPlayerGateway> _playerGatewayThread = new SingletonThreadHelper<IPlayerGateway>(
                new Lazy<IPlayerGateway>(() => { return InitPlayerGateway(); }));

        private static IPlayerGateway _playerGatewayInstance = null;
        private static IExMonitorServerConfigStore _configStore = ExMonitorServerConfigStoreFactory.Store;
        private string _cmpEncodingType = string.Empty;
        private string _encryptionKey = string.Empty;
        private string _localSiteCode = string.Empty;
        public int _nextPTGatewayrequest = 0;
        private bool? _isPreCommitmentEnabled = null;
        private bool? _isDMEnabled = null;
        private readonly Regex _alphaNumericCheck = new Regex("[^a-zA-Z0-9]");
        private static bool? _isStockPrefixRequired = null;
        private static string _stockPrefix = null;
        private static Encoding _encode = null;
        private static List<CMPErrorCodes> _errorCodes = null;
        private Dictionary<string, FF_AppId_EFT_AccountTypes> _sdtAccountType = null;
        private Dictionary<string, int> _sdtGatewayReturns = null;
        private Dictionary<string, int> _withdrawalErrorcodes = null;
        private Dictionary<int, int> _gmuAndCMPErrCodeMap = null;
        private readonly IDictionary<string, int> _ptInstallations = null;
        private bool _IsEmployeeCardTrackingEnabled;
        private bool _IsStockPrefixRequired;
        private bool? _isExtendedPlayer = null;
        private int? _breakPeriodInterval = null;
        private int? _instantPeriodicInterval = null;
        private bool? _isGamePlayInfoRequiredForSession = null;

        #endregion //Private Membser

        #region Constructor

        private HandlerHelper()
        {
            _sdtGatewayReturns = new Dictionary<string, int>();
            _sdtAccountType = new Dictionary<string, FF_AppId_EFT_AccountTypes>();
            _withdrawalErrorcodes = new Dictionary<string, int>();
            _ptInstallations = new StringConcurrentDictionary<int>();            
            this.InitializeGatewaySettings();
        }

        #endregion //Constructor

        #region Initialize

        public void InitializeGatewaySettings()
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "InitializeGatewaySettings"))
            {
                try
                {
                    GatewaySettings.ConnectionString = _configStore.ConnectionString;
                    GatewaySettings.ExtConnectionString = _configStore.ExtSysMsgConnectionString;

                    GatewaySettings.IsWSCallMessage = _configStore.IsWSCallMessage;
                    GatewaySettings.IsPlayerCacheResponseRequired = _configStore.IsPlayerCacheResponseRequired;

                    GatewaySettings.AddGetResponseFromCache("PT77", _configStore.IsGetResponseFromCache);

                    GatewaySettings.AddAlwaysSendRequestsToSocket("PT77", _configStore.IsAlwaysSendRequestsViaSocket);
                    GatewaySettings.AddAlwaysSendRequestsToSocket("PT65", _configStore.IsAlwaysSendRequestsViaSocket);
                    GatewaySettings.AddAlwaysSendRequestsToSocket("PT66", _configStore.IsAlwaysSendRequestsViaSocket);
                    GatewaySettings.AddAlwaysSendRequestsToSocket("PT10", _configStore.IsAlwaysSendRequestsViaSocket);
                    GatewaySettings.AddAlwaysSendRequestsToSocket("PT81", _configStore.IsAlwaysSendRequestsViaSocket);
                    GatewaySettings.AddAlwaysSendRequestsToSocket("PT37", _configStore.IsAlwaysSendRequestsViaSocket);
                    GatewaySettings.AddAlwaysSendRequestsToSocket("PT38", _configStore.IsAlwaysSendRequestsViaSocket);

                    GatewaySettings.AddWebServiceRequestNeeded("PT77", _configStore.IsPDWSCallRequired);
                    GatewaySettings.AddWebServiceRequestNeeded("PT65", _configStore.IsPDWSCallRequired);
                    GatewaySettings.AddWebServiceRequestNeeded("PT66", _configStore.IsPDWSCallRequired);
                    GatewaySettings.AddWebServiceRequestNeeded("PT10", _configStore.IsPDWSCallRequired);
                    GatewaySettings.AddWebServiceRequestNeeded("PT37", _configStore.IsPDWSCallRequired);
                    GatewaySettings.AddWebServiceRequestNeeded("PT38", _configStore.IsPDWSCallRequired);
                    GatewaySettings.AddWebServiceRequestNeeded("CA12", _configStore.IsCACallRequiredForWithdrawal);
                    GatewaySettings.AddWebServiceRequestNeeded("CA21", _configStore.IsCACallRequiredForWithdrawal);
                    GatewaySettings.AddWebServiceRequestNeeded("CA41", _configStore.IsCACallRequiredForDeposit);
                    GatewaySettings.AddWebServiceRequestNeeded("CA43", _configStore.IsCACallRequiredForDeposit);
                    GatewaySettings.AddWebServiceRequestNeeded("CA52", _configStore.IsCACallRequiredForBalance);
                    GatewaySettings.AddWebServiceRequestNeeded("PT81", _configStore.IsPT81WSCallRequired);

                    GatewaySettings.SDTMultipleClientSockets = _configStore.IsMultipleSocketClientCallsRequired;
                    GatewaySettings.SDTWSURL = _configStore.CMPWebServiceURL;
                    GatewaySettings.PCGatewayURL = ConfigurationManager.AppSettings["PCGatewayURL"];
                    GatewaySettings.UseCMP123 = ConfigurationManager.AppSettings["UseCMP123"];

                    GatewaySettings.SocketSendWithoutReconnect = _configStore.SocketSendWithoutReconnect;
                    GatewaySettings.SocketSendRetryCount = _configStore.SocketSendRetryCount;
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }
            }
        }

        private static IPlayerGateway InitPlayerGateway()
        {
            using (ILogMethod method = Log.LogMethod("HandlerHelper", "InitPlayerGateway"))
            {
                IPlayerGateway gateway = default(IPlayerGateway);

                try
                {
                    gateway = GatewayFactory.GetGateway(GatewayType.SDT);
                    gateway.Initialize();

                    gateway.SocketSenderPT2.ReceiveTimeout = GatewaySettings.PTAsyncReceiveTimeout;
                    gateway.SocketSenderPT.ReceiveTimeout = GatewaySettings.PTAsyncReceiveTimeout;
                    gateway.SocketSenderCA2.ReceiveTimeout = GatewaySettings.CAAsyncReceiveTimeout;
                    gateway.SocketSenderCA.ReceiveTimeout = GatewaySettings.CAAsyncReceiveTimeout;

                    gateway.SocketSenderPT.Initialize(_configStore.PT_GATEWAY_IP, _configStore.SDT_SendPTPortNo);
                    gateway.SocketSenderPT2.Initialize(_configStore.PT_GATEWAY_IP, _configStore.SDT_ReceivePTPortNo);
                    gateway.SocketSenderCA.Initialize(_configStore.PT_GATEWAY_IP, _configStore.SDT_SendCAPortNo);
                    gateway.SocketSenderCA2.Initialize(_configStore.PT_GATEWAY_IP, _configStore.SDT_ReceiveCAPortNo);

                    if (GatewaySettings.SocketSendWithoutReconnect)
                    {
                        gateway.SocketSenderPT.SendTimeout = GatewaySettings.PTAsyncSendTimeout;
                        gateway.SocketSenderCA.SendTimeout = GatewaySettings.CAAsyncSendTimeout;
                    }
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }

                return gateway;
            }
        }

        #endregion //Initialize

        #region Properties

        public static IPlayerGateway PlayerGatewayInstance
        {
            get
            {
                return GatewaySettings.SDTMultipleClientSockets ?
                        _playerGatewayThread.Current :
                        _playerGatewayThread.Current;
            }
        }

        public static HandlerHelper Current
        {
            get { return _singletonHelper.Current; }
        }

        public string CMPEncodingType
        {
            get { return _configStore.CMPEncodingType; }
        }

        public string EncryptionKey
        {
            get { return _configStore.EncryptionKey; }
        }

        public string AllowPT10ForNonCardedPlay
        {
            get { return _configStore.AllowPT10ForNonCardedPlay; }
        }

        public char PCRatingBasis
        {
            get { return _configStore.PCRatingBasis; }
        }

        public short MaxRows
        {
            get { return _configStore.MaxRows; }
        }

        public int GMUTimeOut
        {
            get { return _configStore.GMU_TimeOut; }
        }

        public int TotInstGrp
        {
            get { return _configStore.TotInstGrp; }
        }

        public int InsPerDelay
        {
            get { return _configStore.InsPerDelay; }
        }

        public bool RamResetRequired
        {
            get { return _configStore.RamResetRequired; }
        }

        public bool CheckDenomChangeForRamReset
        {
            get { return _configStore.CheckDenomChangeForRamReset; }
        }

        public int MachineDisableRetryCount
        {
            get { return _configStore.MachineDisableRetryCount; }
        }

        public int DenomChangeRetryCount
        {
            get { return _configStore.DenomChangeRetryCount; }
        }

        public Encoding Encode
        {
            get
            {
                if (_encode == null)
                    _encode = HandlerHelper.Current.IsASCIIEncoding() ? Encoding.ASCII : ASCIIEncoding.Default;

                return _encode;
            }
        }

        public List<CMPErrorCodes> CMPErrorCodesList
        {
            get
            {
                if (_errorCodes == null)
                    _errorCodes = ExCommsDataContext.Current.GetCMPErrorCodes();

                return _errorCodes;
            }
        }

        public Dictionary<int, int> GMUAndCMPErrCodeMap
        {
            get
            {
                if (_gmuAndCMPErrCodeMap == null)
                {
                    this.CMPErrorCodesList.ForEach(item =>
                    {
                        if (!_gmuAndCMPErrCodeMap.ContainsKey(item.GmuErrorCode.GetValueOrDefault()))
                        {
                            _gmuAndCMPErrCodeMap.Add(item.GmuErrorCode.GetValueOrDefault(), item.CMPErrorCode.GetValueOrDefault());
                        }
                    });
                }
                return _gmuAndCMPErrCodeMap;
            }
        }

        public bool IsPreCommitmentEnabled
        {
            get
            {
                return _configStore.PreCommitMentEnabled;
            }
        }

        public bool IsDMEnabled
        {
            get
            {
                if (_isDMEnabled == null)
                    _isDMEnabled = ExCommsDataContext.Current.GetSettingValueBool("USE_DIRECTED_MESSAGING", false);

                return _isDMEnabled.GetValueOrDefault();
            }
        }

        public bool IsEmployeeCardTrackingEnabled
        {
            get
            {
                return _configStore.IsEmployeeCardTrackingEnabled;
            }
        }


        public string LocalSiteCode
        {
            get
            {
                if (string.IsNullOrEmpty(_localSiteCode))
                    _localSiteCode = ExCommsDataContext.Current.GetLocalSiteCode().Trim();

                return _localSiteCode;
            }
        }

        public bool GameCappingReleaseOnPlayerCardIn
        {
            get;
            set;
        }

        public bool IsExtendedPlayer
        {
            get
            {
                if (_isExtendedPlayer == null)
                {
                    _isExtendedPlayer = ExCommsDataContext.Current.GetSettingValueBool("IsExtendedPlayer", false);
                }

                return _isExtendedPlayer.GetValueOrDefault();
            }
        }

        public int BreakPeriodInterval
        {
            get
            {
                try
                {
                    if (_breakPeriodInterval == null)
                    {
                        _breakPeriodInterval = Convert.ToInt32(ExCommsDataContext.Current.GetSettingValue("BreakPeriodInterval", "900"));
                    }

                }
                catch { _breakPeriodInterval = 900; }
                return _breakPeriodInterval.GetValueOrDefault();
            }
        }

        public int InstantPeriodicInterval
        {
            get
            {
                try
                {
                    if (_instantPeriodicInterval == null)
                    {
                        _instantPeriodicInterval = Convert.ToInt32(ExCommsDataContext.Current.GetSettingValue("Instant_Periodic_Interval", "20"));
                    }

                }
                catch { _instantPeriodicInterval = 20; }
                return _instantPeriodicInterval.GetValueOrDefault();
            }
        }

        public bool IsGamePlayInfoRequiredForSession
        {
            get
            {
                if (_isGamePlayInfoRequiredForSession == null)
                {
                    _isGamePlayInfoRequiredForSession = Convert.ToBoolean(ExCommsDataContext.Current.GetSettingValue("GamePlayInfoRequiredForSession", "False"));
                }
                return _isGamePlayInfoRequiredForSession.GetValueOrDefault();
            }
        }

        #endregion //Properties

        #region Public Methods

        /// <summary>
        /// To check whether encoding is ASCII or not.
        /// </summary>
        /// <returns></returns>
        public bool IsASCIIEncoding()
        {
            try
            {
                return CMPEncodingType.Equals("ASCII", StringComparison.OrdinalIgnoreCase) ? true : false;
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
                return false;
            }
        }

        /// <summary>
        /// To chech the parameter string contain only alphanumerc characters
        /// </summary>
        /// <param name="strToCheck"></param>
        /// <returns></returns>
        public bool IsAlphaNumeric(string strToCheck)
        {
            try
            {
                return !_alphaNumericCheck.IsMatch(strToCheck);
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
                return false;
            }
        }

        /// <summary>
        /// TO get CheckForStockPrefix setting value
        /// </summary>
        /// <returns></returns>
        public bool IsStockPrefixRequired()
        {
            try
            {
                return _configStore.IsStockPrefixRequired;
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
                return false;
            }
        }

        /// <summary>
        /// To get next Request Id
        /// </summary>
        /// <returns></returns>
        public int NextPTRequestID()
        {
            try
            {
                _nextPTGatewayrequest += 1;
                if (_nextPTGatewayrequest > 100000)
                    _nextPTGatewayrequest = 1;

                return _nextPTGatewayrequest;
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
                return 1;
            }
        }

        /// <summary>
        /// To get the System_Parameter_Stock_Code_Prefix setting value from the DB
        /// </summary>
        /// <returns></returns>
        public string GetStockPrefix()
        {
            try
            {
                if (string.IsNullOrEmpty(_stockPrefix))
                    _stockPrefix = ExCommsDataContext.Current.GetSettingValue("System_Parameter_Stock_Code_Prefix", "");

                return _stockPrefix;
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
                return string.Empty;
            }
        }

        /// <summary>
        /// Get the PIN value and returns Encrypted pin value
        /// </summary>
        /// <param name="pin"></param>
        /// <returns></returns>
        public string GetEncryptedPIN(string pin)
        {
            try
            {
                return Crypto.Crypto.EncryptHexString(HandlerHelper.Current.EncryptionKey, pin, this.Encode);
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
                return string.Empty;
            }
        }

        /// <summary>
        /// Get the asset and return the same based on the Stock Prefix settings
        /// </summary>
        /// <param name="asset"></param>
        /// <returns></returns>
        public string GetAssetByStockPrefix(string asset)
        {
            try
            {
                return (IsStockPrefixRequired() && IsAlphaNumeric(asset)) ? asset.Substring(GetStockPrefix().Length) : asset;
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
                return string.Empty;
            }
        }

        public void SaveSDTRequest(string cardNumber, int installationNo)
        {
            lock (_sdtGatewayReturns)
            {
                try
                {
                    RemoveSDTRequest(cardNumber);
                    _sdtGatewayReturns.Add(cardNumber, installationNo);
                }
                catch (Exception ex)
                {
                    Log.Exception(ex);
                }
            }
        }

        public void SavePTInstallation(string cardNumber, int installationNo)
        {
            _ptInstallations.AddOrUpdate2(cardNumber, installationNo);
        }

        /// <summary>
        /// To get installtion number from SDT Gateway Dictionary
        /// </summary>
        /// <param name="cardNumber"></param>
        /// <returns></returns>
        public int GetSDTRequest(string cardNumber)
        {
            return this.GetSDTRequest(cardNumber, false);
        }

        /// <summary>
        /// To get/and remove installtion number from SDT Gateway Dictionary
        /// </summary>
        /// <param name="cardNumber"></param>
        /// <param name="removeFromColl"></param>
        /// <returns></returns>
        public int GetSDTRequest(string cardNumber, bool removeFromColl)
        {
            int installationNo = 0;
            lock (_sdtGatewayReturns)
            {
                try
                {
                    if (_sdtGatewayReturns.ContainsKey(cardNumber))
                        installationNo = _sdtGatewayReturns[cardNumber];
                }
                catch (Exception ex)
                {
                    Log.Exception(ex);
                }
                finally
                {
                    if (removeFromColl)
                    {
                        RemoveSDTRequest(cardNumber);
                    }
                }
                return installationNo;
            }
        }

        /// <summary>
        /// To add an item in the Account type dictionary
        /// </summary>
        /// <param name="cardNumber"></param>
        /// <param name="accountType"></param>
        public void SaveSDTAccountType(string cardNumber, FF_AppId_EFT_AccountTypes accountType)
        {
            lock (_sdtAccountType)
            {
                try
                {
                    RemoveSDTAccountType(cardNumber);
                    _sdtAccountType.Add(cardNumber, accountType);
                }
                catch (Exception ex)
                {
                    Log.Exception(ex);
                }
            }
        }

        /// <summary>
        /// To get account type from Account Type Dictionary
        /// </summary>
        /// <param name="cardNumber"></param>
        /// <returns></returns>
        public FF_AppId_EFT_AccountTypes GetSDTAccountType(string cardNumber)
        {
            return this.GetSDTAccountType(cardNumber, false);
        }

        /// <summary>
        /// To get/and remove account type from Account Type Dictionary
        /// </summary>
        /// <param name="cardNumber"></param>
        /// <param name="removeFromColl"></param>
        /// <returns></returns>
        public FF_AppId_EFT_AccountTypes GetSDTAccountType(string cardNumber, bool removeFromColl)
        {
            lock (_sdtAccountType)
            {
                try
                {
                    if (_sdtAccountType.ContainsKey(cardNumber))
                        return _sdtAccountType[cardNumber];
                }
                catch (Exception ex)
                {
                    Log.Exception(ex);
                }
                finally
                {
                    if (removeFromColl)
                    {
                        RemoveSDTAccountType(cardNumber);
                    }
                }
                return new FF_AppId_EFT_AccountTypes();
            }
        }

        /// <summary>
        /// To add an Errorcode to Withdrawal Errorcode Dictionary
        /// </summary>
        /// <param name="cardNumber"></param>
        /// <param name="errorCode"></param>
        public void SaveWithdrawalErrorCode(string cardNumber, int errorCode)
        {
            lock (_withdrawalErrorcodes)
            {
                try
                {
                    RemoveWithdrawalErrorCode(cardNumber);
                    _withdrawalErrorcodes.Add(cardNumber, errorCode);
                }
                catch (Exception ex)
                {
                    Log.Exception(ex);
                }
            }
        }

        /// <summary>
        /// To get Errorcode from withdrawal errorcode Dictionary
        /// </summary>
        /// <param name="cardNumber"></param>
        /// <returns></returns>
        public int GetWithDrawalErrorCode(string cardNumber)
        {
            return this.GetWithDrawalErrorCode(cardNumber, false);
        }

        /// <summary>
        /// To get/and remove Errorcode from withdrawal errorcode Dictionary
        /// </summary>
        /// <param name="cardNumber"></param>
        /// <param name="removeFromColl"></param>
        /// <returns></returns>
        public int GetWithDrawalErrorCode(string cardNumber, bool removeFromColl)
        {
            int errorCode = 0;
            lock (_withdrawalErrorcodes)
            {
                try
                {
                    if (_withdrawalErrorcodes.ContainsKey(cardNumber))
                        errorCode = _withdrawalErrorcodes[cardNumber];
                }
                catch (Exception ex)
                {
                    Log.Exception(ex);
                }
                finally
                {
                    if (removeFromColl)
                    {
                        RemoveWithdrawalErrorCode(cardNumber);
                    }
                }
                return errorCode;
            }
        }

        #endregion //Public Methods

        #region Private Methods

        /// <summary>
        /// To remove installation number from SDT Gateway Dictionary
        /// </summary>
        /// <param name="cardNumber"></param>
        private void RemoveSDTRequest(string cardNumber)
        {
            try
            {
                if (_sdtGatewayReturns.ContainsKey(cardNumber))
                    _sdtGatewayReturns.Remove(cardNumber);
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
            }
        }

        /// <summary>
        /// To Remove an Account Type from the Dictionary
        /// </summary>
        /// <param name="cardNumber"></param>
        private void RemoveSDTAccountType(string cardNumber)
        {
            try
            {
                if (_sdtAccountType.ContainsKey(cardNumber))
                    _sdtAccountType.Remove(cardNumber);
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
            }
        }

        /// <summary>
        /// To Remove an Errorcode from the Dictionary specific to CardNumber
        /// </summary>
        /// <param name="cardNumber"></param>
        private void RemoveWithdrawalErrorCode(string cardNumber)
        {
            try
            {
                if (_withdrawalErrorcodes.ContainsKey(cardNumber))
                    _withdrawalErrorcodes.Remove(cardNumber);
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
            }
        }

        #endregion //Private Methods
    }
}
