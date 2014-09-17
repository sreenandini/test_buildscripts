using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace BMC.ExMonitor.Server.Handlers
{
    public class GatewayMessageStructures
    {
        [DebuggerNonUserCode]
        public GatewayMessageStructures()
        {
        }

        public class CMPPlayerInformation
        {
            private string _Firstname;
            private string _Lastname;
            private int _pointsBalance;
            private int _CompsBalance;
            private bool _IsVIP;
            private bool _EcashEnabled;
            private bool _IsPinRequired;
            private bool _CanQueryAmount;
            private bool _CanWithdrawPoints;
            private bool _CanWithdrawPromo;
            private bool _CanWithdrawCash;
            private bool _CanDepositPoints;
            private bool _CanDepositPromo;
            private bool _CanDepositCash;

            public string FirstName
            {
                get
                {
                    return this._Firstname;
                }
                set
                {
                    this._Firstname = value;
                }
            }

            public string LastName
            {
                get
                {
                    return this._Lastname;
                }
                set
                {
                    this._Lastname = value;
                }
            }

            public int PointsBalance
            {
                get
                {
                    return this._pointsBalance;
                }
                set
                {
                    this._pointsBalance = value;
                }
            }

            public int CompsBalance
            {
                get
                {
                    return this._CompsBalance;
                }
                set
                {
                    this._CompsBalance = value;
                }
            }

            public string DisplayName
            {
                get
                {
                    return this.FirstName;
                }
            }

            public bool IsVIP
            {
                get
                {
                    return this._IsVIP;
                }
                set
                {
                    this._IsVIP = value;
                }
            }

            public bool EcashEnabled
            {
                get
                {
                    return this._EcashEnabled;
                }
                set
                {
                    this._EcashEnabled = value;
                }
            }

            public bool IsPinRequired
            {
                get
                {
                    return this._IsPinRequired;
                }
                set
                {
                    this._IsPinRequired = value;
                }
            }

            public bool CanWithDrawPoints
            {
                get
                {
                    return this._CanWithdrawPoints;
                }
                set
                {
                    this._CanWithdrawPoints = value;
                }
            }

            public bool CanWithDrawPromo
            {
                get
                {
                    return this._CanWithdrawPromo;
                }
                set
                {
                    this._CanWithdrawPromo = value;
                }
            }

            public bool CanWithDrawCash
            {
                get
                {
                    return this._CanWithdrawCash;
                }
                set
                {
                    this._CanWithdrawCash = value;
                }
            }

            public bool CanDepositCash
            {
                get
                {
                    return this._CanDepositCash;
                }
                set
                {
                    this._CanDepositCash = value;
                }
            }

            public bool CanDepositPromo
            {
                get
                {
                    return this._CanDepositPromo;
                }
                set
                {
                    this._CanDepositPromo = value;
                }
            }

            public bool CanQueryAmount
            {
                get
                {
                    return true;
                }
            }

            public bool CanAllowOffers
            {
                get
                {
                    return true;
                }
            }

            public CMPPlayerInformation()
            {
                this._Firstname = "";
                this._Lastname = "";
            }
        }

        public class MessageStructureforAFT
        {
            private string _GatewayID;
            private string _LGC;
            private string _LGCPassBack;
            private string _SysPassBack;
            private string _Time;
            private string _MSTime;
            private string _AssetNumber;
            private string _PT;
            private string _SessionAccountNumber;
            private string _Command;
            private string _VLTID;
            private string _Denom;
            private string _ManufacturerID;
            private string _SerialNumber;
            private string _PlayerID;
            private string _PIN;
            private string _TransferID;
            private string _accountID;
            private string _accountID1;
            private string _accountID2;
            private string _accountID3;
            private string _cashable1;
            private string _cashable2;
            private string _cashable3;
            private string _noncashable1;
            private string _noncashable2;
            private string _noncashable3;
            private int _maxamount;
            private string _Status;
            private int _pointsBalance;
            private int _CompsBalance;

            public MessageStructureforAFT()
            {
                this._GatewayID = string.Empty;
                this._LGC = string.Empty;
                this._LGCPassBack = string.Empty;
                this._SysPassBack = string.Empty;
                this._Time = string.Empty;
                this._MSTime = string.Empty;
                this._AssetNumber = string.Empty;
                this._PT = string.Empty;
                this._SessionAccountNumber = string.Empty;
                this._Command = string.Empty;
                this._VLTID = string.Empty;
                this._Denom = string.Empty;
                this._ManufacturerID = string.Empty;
                this._SerialNumber = string.Empty;
                this._PlayerID = string.Empty;
                this._PIN = string.Empty;
                this._TransferID = string.Empty;
                this._accountID = string.Empty;
                this._accountID1 = string.Empty;
                this._accountID2 = string.Empty;
                this._accountID3 = string.Empty;
                this._cashable1 = (string)null;
                this._cashable2 = (string)null;
                this._cashable3 = (string)null;
                this._noncashable1 = (string)null;
                this._noncashable2 = (string)null;
                this._noncashable3 = (string)null;
                this._maxamount = 0;
                this._Status = string.Empty;
            }

            public string GatewayID
            {
                get
                {
                    return this._GatewayID;
                }
                set
                {
                    this._GatewayID = value;
                }
            }

            public string LGC
            {
                get
                {
                    return this._LGC;
                }
                set
                {
                    this._LGC = value;
                }
            }

            public string SYSPassBack
            {
                get
                {
                    return this._SysPassBack;
                }
                set
                {
                    this._SysPassBack = value;
                }
            }

            public string LGCPassBack
            {
                get
                {
                    return this._LGCPassBack;
                }
                set
                {
                    this._LGCPassBack = value;
                }
            }

            public string AssetNumber
            {
                get
                {
                    return this._AssetNumber;
                }
                set
                {
                    this._AssetNumber = value;
                }
            }

            public string SerialNumber
            {
                get
                {
                    return this._SerialNumber;
                }
                set
                {
                    this._SerialNumber = value;
                }
            }

            public string Time
            {
                get
                {
                    return this._Time;
                }
                set
                {
                    this._Time = value;
                }
            }

            public string Denomination
            {
                get
                {
                    return this._Denom;
                }
                set
                {
                    this._Denom = value;
                }
            }

            public string MSTime
            {
                get
                {
                    return this._MSTime;
                }
                set
                {
                    this._MSTime = value;
                }
            }

            public string PT
            {
                get
                {
                    return this._PT;
                }
                set
                {
                    this._PT = value;
                }
            }

            public string SessionAccountNumber
            {
                get
                {
                    return this._SessionAccountNumber;
                }
                set
                {
                    this._SessionAccountNumber = value;
                }
            }

            public string PlayerCardNumber
            {
                get
                {
                    return this._SessionAccountNumber;
                }
                set
                {
                    this._SessionAccountNumber = value;
                }
            }

            public string Command
            {
                get
                {
                    return this._Command;
                }
                set
                {
                    this._Command = value;
                }
            }

            public string VLTID
            {
                get
                {
                    return this._VLTID;
                }
                set
                {
                    this._VLTID = value;
                }
            }

            public string ManufactureID
            {
                get
                {
                    return this._ManufacturerID;
                }
                set
                {
                    this._ManufacturerID = value;
                }
            }

            public string PlayerID
            {
                get
                {
                    return this._PlayerID;
                }
                set
                {
                    this._PlayerID = value;
                }
            }

            public string PIN
            {
                get
                {
                    return this._PIN;
                }
                set
                {
                    this._PIN = value;
                }
            }

            public string TransferID
            {
                get
                {
                    return this._TransferID;
                }
                set
                {
                    this._TransferID = value;
                }
            }

            public string accountID
            {
                get
                {
                    return this._accountID;
                }
                set
                {
                    this._accountID = value;
                }
            }

            public string accountID1
            {
                get
                {
                    return this._accountID1;
                }
                set
                {
                    this._accountID1 = value;
                }
            }

            public string accountID2
            {
                get
                {
                    return this._accountID2;
                }
                set
                {
                    this._accountID2 = value;
                }
            }

            public string accountID3
            {
                get
                {
                    return this._accountID3;
                }
                set
                {
                    this._accountID3 = value;
                }
            }

            public string Cashable
            {
                get
                {
                    return this._cashable1;
                }
                set
                {
                    this._cashable1 = value;
                }
            }

            public string Cashable1
            {
                get
                {
                    return this._cashable1;
                }
                set
                {
                    this._cashable1 = value;
                }
            }

            public string Cashable2
            {
                get
                {
                    return this._cashable2;
                }
                set
                {
                    this._cashable2 = value;
                }
            }

            public string Cashable3
            {
                get
                {
                    return this._cashable3;
                }
                set
                {
                    this._cashable3 = value;
                }
            }

            public string NonCashable
            {
                get
                {
                    return this._noncashable1;
                }
                set
                {
                    this._noncashable1 = value;
                }
            }

            public string NonCashable1
            {
                get
                {
                    return this._noncashable1;
                }
                set
                {
                    this._noncashable1 = value;
                }
            }

            public string NonCashable2
            {
                get
                {
                    return this._cashable2;
                }
                set
                {
                    this._noncashable2 = value;
                }
            }

            public string NonCashable3
            {
                get
                {
                    return this._noncashable3;
                }
                set
                {
                    this._noncashable3 = value;
                }
            }

            public int MaxAmount
            {
                get
                {
                    return this._maxamount;
                }
                set
                {
                    this._maxamount = value;
                }
            }

            public string Status
            {
                get
                {
                    return this._Status;
                }
                set
                {
                    this._Status = value;
                }
            }
        }

        public class AFTAuditHistory
        {
            private string _Firstname;
            private string _Lastname;
            private double _Amount;
            private double _CashableAmt;
            private double _NonCashableAmt;
            private double _WATAmt;
            private int _InstallationNo;
            private int _CollectionNo;
            private bool _EcashEnabled;
            private bool _IsVIP;
            private string _AccountNumber;
            private string _TransactionType;
            private int _ErrorCode;
            private string _ErrorMessage;
            private int _TransferID;
            private string _AccountType;
            private bool _TransactionStatus;
            private string _CashableDepAmt;
            private string _NonCashableDepAmt;
            private string _WATDepAmt;

            public AFTAuditHistory()
            {
                this._Firstname = "";
                this._Lastname = "";
            }

            public string FirstName
            {
                get
                {
                    return this._Firstname;
                }
                set
                {
                    this._Firstname = value;
                }
            }

            public string LasttName
            {
                get
                {
                    return this._Lastname;
                }
                set
                {
                    this._Lastname = value;
                }
            }

            public double Amount
            {
                get
                {
                    return this._Amount;
                }
                set
                {
                    this._Amount = value;
                }
            }

            public double CashableAmt
            {
                get
                {
                    return this._CashableAmt;
                }
                set
                {
                    this._CashableAmt = value;
                }
            }

            public double NoncashableAmt
            {
                get
                {
                    return this._NonCashableAmt;
                }
                set
                {
                    this._NonCashableAmt = value;
                }
            }

            public double WATAmt
            {
                get
                {
                    return this._WATAmt;
                }
                set
                {
                    this._WATAmt = value;
                }
            }

            public string CashableDepAmt
            {
                get
                {
                    return this._CashableDepAmt;
                }
                set
                {
                    this._CashableDepAmt = value;
                }
            }

            public string NoncashabledepAmt
            {
                get
                {
                    return this._NonCashableDepAmt;
                }
                set
                {
                    this._NonCashableDepAmt = value;
                }
            }

            public string WATDepAmt
            {
                get
                {
                    return this._WATDepAmt;
                }
                set
                {
                    this._WATDepAmt = value;
                }
            }

            public int InstallationNo
            {
                get
                {
                    return this._InstallationNo;
                }
                set
                {
                    this._InstallationNo = value;
                }
            }

            public int CollectionNo
            {
                get
                {
                    return this._CollectionNo;
                }
                set
                {
                    this._CollectionNo = value;
                }
            }

            public bool IsEcashEnabled
            {
                get
                {
                    return this._EcashEnabled;
                }
                set
                {
                    this._EcashEnabled = value;
                }
            }

            public bool IsVIP
            {
                get
                {
                    return this._IsVIP;
                }
                set
                {
                    this._IsVIP = value;
                }
            }

            public string AccountNumber
            {
                get
                {
                    return this._AccountNumber;
                }
                set
                {
                    this._AccountNumber = value;
                }
            }

            public string TransactionType
            {
                get
                {
                    return this._TransactionType;
                }
                set
                {
                    this._TransactionType = value;
                }
            }

            public int ErrorCode
            {
                get
                {
                    return this._ErrorCode;
                }
                set
                {
                    this._ErrorCode = value;
                }
            }

            public string ErrorMessage
            {
                get
                {
                    return this._ErrorMessage;
                }
                set
                {
                    this._ErrorMessage = value;
                }
            }

            public int TransferID
            {
                get
                {
                    return this._TransferID;
                }
                set
                {
                    this._TransferID = value;
                }
            }

            public string AccountType
            {
                get
                {
                    return this._AccountType;
                }
                set
                {
                    this._AccountType = value;
                }
            }

            public bool TransactionStatus
            {
                get
                {
                    return this._TransactionStatus;
                }
                set
                {
                    this._TransactionStatus = value;
                }
            }
        }
    }

    public class MessageStructure
    {
        private string _GatewayID;
        private string _LGC;
        private string _LGCPassBack;
        private string _SysPassBack;
        private string _Time;
        private string _MSTime;
        private string _AssetNumber;
        private string _PT;
        private string _Command;
        private string _VLTID;
        private string _Denom;
        private string _ManufacturerID;
        private string _SerialNumber;
        private string _MeterBill001;
        private string _MeterBill002;
        private string _MeterBill005;
        private string _MeterBill010;
        private string _MeterBill020;
        private string _MeterBill050;
        private string _MeterBill100;
        private string _MeterBillOther;
        private string _MeterVoucherInAmount;
        private string _MeterVoucherInCount;
        private string _MeterVoucherOutAmount;
        private string _MeterVoucherOutCount;
        private string _MeterJackpotOutAmount;
        private string _MeterJackpotOutCount;
        private string _MeterGamesPlayed;
        private string _MeterMoneyWagered;
        private string _MeterMoneyWon;
        private string _MeterProgressiveContributions;
        private string _MeterProgressiveAwards;
        private string _MeterForm;
        private string _PercentHold;
        private string _SessionAccountNumber;
        private string _SessionPointBalance;
        private string _SessionTrack1;
        private string _SessionTrack2;
        private string _SessionTrack3;
        private string _SessionPoints;
        private string _SessionCountdown;
        private string _SessionGamesPlayed;
        private string _SessionMoneyWagered;
        private string _SessionMoneyWon;
        private string _BaseAwardPoints;
        private string _PlayerAwardPoints;
        private string _HostAwardPoints;
        private string _OverridePoints;
        private int _pointsBalance;
        private int _CompsBalance;
        private string _JackpotAmount;
        private string _JackpotWinDescription;

        public MessageStructure()
        {
            this._GatewayID = string.Empty;
            this._LGC = string.Empty;
            this._LGCPassBack = string.Empty;
            this._SysPassBack = string.Empty;
            this._Time = string.Empty;
            this._MSTime = string.Empty;
            this._AssetNumber = string.Empty;
            this._PT = string.Empty;
            this._Command = string.Empty;
            this._VLTID = string.Empty;
            this._Denom = string.Empty;
            this._ManufacturerID = string.Empty;
            this._SerialNumber = string.Empty;
            this._MeterBill001 = string.Empty;
            this._MeterBill002 = string.Empty;
            this._MeterBill005 = string.Empty;
            this._MeterBill010 = string.Empty;
            this._MeterBill020 = string.Empty;
            this._MeterBill050 = string.Empty;
            this._MeterBill100 = string.Empty;
            this._MeterBillOther = string.Empty;
            this._MeterVoucherInAmount = string.Empty;
            this._MeterVoucherInCount = string.Empty;
            this._MeterVoucherOutAmount = string.Empty;
            this._MeterVoucherOutCount = string.Empty;
            this._MeterJackpotOutAmount = string.Empty;
            this._MeterJackpotOutCount = string.Empty;
            this._MeterGamesPlayed = string.Empty;
            this._MeterMoneyWagered = string.Empty;
            this._MeterMoneyWon = string.Empty;
            this._MeterProgressiveContributions = string.Empty;
            this._MeterProgressiveAwards = string.Empty;
            this._MeterForm = string.Empty;
            this._PercentHold = string.Empty;
            this._SessionAccountNumber = string.Empty;
            this._SessionPointBalance = string.Empty;
            this._SessionTrack1 = string.Empty;
            this._SessionTrack2 = string.Empty;
            this._SessionTrack3 = string.Empty;
            this._SessionPoints = string.Empty;
            this._SessionCountdown = string.Empty;
            this._SessionGamesPlayed = string.Empty;
            this._SessionMoneyWagered = string.Empty;
            this._SessionMoneyWon = string.Empty;
            this._BaseAwardPoints = string.Empty;
            this._PlayerAwardPoints = string.Empty;
            this._HostAwardPoints = string.Empty;
            this._OverridePoints = string.Empty;
            this._JackpotAmount = string.Empty;
            this._JackpotWinDescription = string.Empty;
        }

        public string GatewayID
        {
            get
            {
                return this._GatewayID;
            }
            set
            {
                this._GatewayID = value;
            }
        }

        public string LGC
        {
            get
            {
                return this._LGC;
            }
            set
            {
                this._LGC = value;
            }
        }

        public string SYSPassBack
        {
            get
            {
                return this._SysPassBack;
            }
            set
            {
                this._SysPassBack = value;
            }
        }

        public string LGCPassBack
        {
            get
            {
                return this._LGCPassBack;
            }
            set
            {
                this._LGCPassBack = value;
            }
        }

        public string AssetNumber
        {
            get
            {
                return this._AssetNumber;
            }
            set
            {
                this._AssetNumber = value;
            }
        }

        public string SerialNumber
        {
            get
            {
                return this._SerialNumber;
            }
            set
            {
                this._SerialNumber = value;
            }
        }

        public string Time
        {
            get
            {
                return this._Time;
            }
            set
            {
                this._Time = value;
            }
        }

        public string Denomination
        {
            get
            {
                return this._Denom;
            }
            set
            {
                this._Denom = value;
            }
        }

        public string MSTime
        {
            get
            {
                return this._MSTime;
            }
            set
            {
                this._MSTime = value;
            }
        }

        public string PT
        {
            get
            {
                return this._PT;
            }
            set
            {
                this._PT = value;
            }
        }

        public string Command
        {
            get
            {
                return this._Command;
            }
            set
            {
                this._Command = value;
            }
        }

        public string VLTID
        {
            get
            {
                return this._VLTID;
            }
            set
            {
                this._VLTID = value;
            }
        }

        public string ManufactureID
        {
            get
            {
                return this._ManufacturerID;
            }
            set
            {
                this._ManufacturerID = value;
            }
        }

        public string MeterBill001
        {
            get
            {
                return this._MeterBill001;
            }
            set
            {
                this._MeterBill001 = value;
            }
        }

        public string MeterBill002
        {
            get
            {
                return this._MeterBill002;
            }
            set
            {
                this._MeterBill002 = value;
            }
        }

        public string MeterBill005
        {
            get
            {
                return this._MeterBill005;
            }
            set
            {
                this._MeterBill005 = value;
            }
        }

        public string MeterBill010
        {
            get
            {
                return this._MeterBill010;
            }
            set
            {
                this._MeterBill010 = value;
            }
        }

        public string MeterBill020
        {
            get
            {
                return this._MeterBill020;
            }
            set
            {
                this._MeterBill020 = value;
            }
        }

        public string MeterBill050
        {
            get
            {
                return this._MeterBill050;
            }
            set
            {
                this._MeterBill050 = value;
            }
        }

        public string MeterBill100
        {
            get
            {
                return this._MeterBill100;
            }
            set
            {
                this._MeterBill100 = value;
            }
        }

        public string MeterVoucherInAmount
        {
            get
            {
                return this._MeterVoucherInAmount;
            }
            set
            {
                this._MeterVoucherInAmount = value;
            }
        }

        public string MeterVoucherInCount
        {
            get
            {
                return this._MeterVoucherInCount;
            }
            set
            {
                this._MeterVoucherInCount = value;
            }
        }

        public string MeterVoucherOutAmount
        {
            get
            {
                return this._MeterVoucherOutAmount;
            }
            set
            {
                this._MeterVoucherOutAmount = value;
            }
        }

        public string MeterVoucherOutCount
        {
            get
            {
                return this._MeterVoucherOutCount;
            }
            set
            {
                this._MeterVoucherOutCount = value;
            }
        }

        public string MeterJackpotOutAmount
        {
            get
            {
                return this._MeterJackpotOutAmount;
            }
            set
            {
                this._MeterJackpotOutAmount = value;
            }
        }

        public string MeterJackpotOutCount
        {
            get
            {
                return this._MeterJackpotOutCount;
            }
            set
            {
                this._MeterJackpotOutCount = value;
            }
        }

        public string MeterGamesPlayed
        {
            get
            {
                return this._MeterGamesPlayed;
            }
            set
            {
                this._MeterGamesPlayed = value;
            }
        }

        public string MeterMoneyWagered
        {
            get
            {
                return this._MeterMoneyWagered;
            }
            set
            {
                this._MeterMoneyWagered = value;
            }
        }

        public string MeterMoneyWon
        {
            get
            {
                return this._MeterMoneyWon;
            }
            set
            {
                this._MeterMoneyWon = value;
            }
        }

        public string MeterProgressiveContributions
        {
            get
            {
                return this._MeterProgressiveContributions;
            }
            set
            {
                this._MeterProgressiveContributions = value;
            }
        }

        public string MeterProgressiveAwards
        {
            get
            {
                return this._MeterProgressiveAwards;
            }
            set
            {
                this._MeterProgressiveAwards = value;
            }
        }

        public string MeterForm
        {
            get
            {
                return this._MeterForm;
            }
            set
            {
                this._MeterForm = value;
            }
        }

        public string PercentHold
        {
            get
            {
                return this._PercentHold;
            }
            set
            {
                this._PercentHold = value;
            }
        }

        public string SessionAccountNumber
        {
            get
            {
                return this._SessionAccountNumber;
            }
            set
            {
                this._SessionAccountNumber = value;
            }
        }

        public string SessionPointBalance
        {
            get
            {
                return this._SessionPointBalance;
            }
            set
            {
                this._SessionPointBalance = value;
            }
        }

        public string SessionTrack1
        {
            get
            {
                return this._SessionTrack1;
            }
            set
            {
                this._SessionTrack1 = value;
            }
        }

        public string SessionTrack2
        {
            get
            {
                return this._SessionTrack2;
            }
            set
            {
                this._SessionTrack2 = value;
            }
        }

        public string SessionTrack3
        {
            get
            {
                return this._SessionTrack3;
            }
            set
            {
                this._SessionTrack3 = value;
            }
        }

        public string SessionPoints
        {
            get
            {
                return this._SessionPoints;
            }
            set
            {
                this._SessionPoints = value;
            }
        }

        public string SessionCountdown
        {
            get
            {
                return this._SessionCountdown;
            }
            set
            {
                this._SessionCountdown = value;
            }
        }

        public string SessionGamesPlayed
        {
            get
            {
                return this._SessionGamesPlayed;
            }
            set
            {
                this._SessionGamesPlayed = value;
            }
        }

        public string SessionMoneyWagered
        {
            get
            {
                return this._SessionMoneyWagered;
            }
            set
            {
                this._SessionMoneyWagered = value;
            }
        }

        public string SessionMoneyWon
        {
            get
            {
                return this._SessionMoneyWon;
            }
            set
            {
                this._SessionMoneyWon = value;
            }
        }

        public string BaseAwardPoints
        {
            get
            {
                return this._BaseAwardPoints;
            }
            set
            {
                this._BaseAwardPoints = value;
            }
        }

        public string PlayerAwardPoints
        {
            get
            {
                return this._PlayerAwardPoints;
            }
            set
            {
                this._PlayerAwardPoints = value;
            }
        }

        public string HostAwardPoints
        {
            get
            {
                return this._HostAwardPoints;
            }
            set
            {
                this._HostAwardPoints = value;
            }
        }

        public string OverridePoints
        {
            get
            {
                return this._OverridePoints;
            }
            set
            {
                this._OverridePoints = value;
            }
        }

        public string JackpotAmount
        {
            get
            {
                return this._JackpotAmount;
            }
            set
            {
                this._JackpotAmount = value;
            }
        }

        public string JackpotWinDescription
        {
            get
            {
                return this._JackpotWinDescription;
            }
            set
            {
                this._JackpotWinDescription = value;
            }
        }
    }
}
