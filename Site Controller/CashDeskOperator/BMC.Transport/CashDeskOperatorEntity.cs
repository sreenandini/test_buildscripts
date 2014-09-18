using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace BMC.Transport.CashDeskOperatorEntity
{
    

    


    


   

    public class TicketException
    {
        public TicketException()
        { }
        private Int32 _iInstallationNo = 0;
        private string _strException = string.Empty;
        private Int32 _iExceptionType = 0;
        private string _strReference = string.Empty;
        private Int32 _iUser = 0;

        public Int32 InstallationNumber
        {
            get
            {
                return _iInstallationNo;
            }
            set
            {
                _iInstallationNo = value;
            }
        }
        public string ExceptionDetails
        {
            get
            {
                return _strException;
            }
            set
            {
                _strException = value;
            }
        }
        public Int32 ExceptionType
        {
            get
            {
                return _iExceptionType;
            }
            set
            {
                _iExceptionType = value;
            }
        }
        public string Reference
        {
            get
            {
                return _strReference;
            }
            set
            {
                _strReference = value;
            }
        }
        public Int32 User
        {
            get
            {
                return _iUser;
            }
            set
            {
                _iUser = value;
            }
        }

    }

    public class Treasuries
    {
        public Treasuries()
        {
        }

        private Int32 _iCollectionNo = 0;
        private Int32 _iuserID = 0;
        private string _strTreasuryType = "";
        private string _strTreasuryReason = "";
        private Int32 _iTreasuryBreakdown2P = 0;
        private Int32 _iTreasuryBreakdown5P = 0;
        private Int32 _iTreasuryBreakdown10P = 0;
        private Int32 _iTreasuryBreakdown20P = 0;
        private Int32 _iTreasuryBreakdown50P = 0;
        private Int32 _iTreasuryBreakdown100P = 0;
        private Int32 _iTreasuryBreakdown200P = 0;
        private float _iTreasuryAmount = 0;
        private Int32 _iTreasuryAllocated = 0;
        private string _strTreausryMembershipNo = "";
        private Int32 _iTreasuryReasonCode = 0;
        private Int32 _iTreasuryIssuerUserNo = 0;
        private Int32 _iInstallationNumber = 0;
        private Int32 _iValue = 0;
        private bool b_TreasuryTemp = false;
        private int _iTreasuryFloatIssuedBy = 0;

        public Int32 TreasuryIssuerUserNo
        {
            get
            {
                return _iTreasuryIssuerUserNo;
            }
            set
            {
                _iTreasuryIssuerUserNo = value;
            }
        }

        public Int32 TreasuryReasonCode
        {
            get
            {
                return _iTreasuryReasonCode;
            }
            set
            {
                _iTreasuryReasonCode = value;
            }
        }

        public string TreasuryMembershipNo
        {
            get
            {
                return _strTreausryMembershipNo;
            }
            set
            {
                _strTreausryMembershipNo = value;
            }
        }

        public Int32 TreasuryBreakdown2P
        {
            get
            {
                return _iTreasuryBreakdown2P;
            }
            set
            {
                _iTreasuryBreakdown2P = value;
            }
        }

        public Int32 TreasuryBreakdown5P
        {
            get
            {
                return _iTreasuryBreakdown5P;
            }
            set
            {
                _iTreasuryBreakdown5P = value;
            }
        }


        public Int32 TreasuryBreakdown10P
        {
            get
            {
                return _iTreasuryBreakdown10P;
            }
            set
            {
                _iTreasuryBreakdown10P = value;
            }
        }

        public Int32 TreasuryBreakdown20P
        {
            get
            {
                return _iTreasuryBreakdown20P;
            }
            set
            {
                _iTreasuryBreakdown20P = value;
            }
        }


        public Int32 TreasuryBreakdown50P
        {
            get
            {
                return _iTreasuryBreakdown50P;
            }
            set
            {
                _iTreasuryBreakdown50P = value;
            }
        }


        public Int32 TreasuryBreakdown100P
        {
            get
            {
                return _iTreasuryBreakdown100P;
            }
            set
            {
                _iTreasuryBreakdown100P = value;
            }
        }


        public Int32 TreasuryBreakdown200P
        {
            get
            {
                return _iTreasuryBreakdown200P;
            }
            set
            {
                _iTreasuryBreakdown200P = value;
            }
        }


        public Int32 TreasuryAllocated
        {
            get
            {
                return _iTreasuryAllocated;
            }
            set
            {
                _iTreasuryAllocated = value;
            }
        }


        public float TreasuryAmount
        {
            get
            {
                return _iTreasuryAmount;
            }
            set
            {
                _iTreasuryAmount = value;
            }
        }

        public string TreasuryReason
        {
            get
            {
                return _strTreasuryReason;
            }
            set
            {
                _strTreasuryReason = value;
            }
        }

        public string TreasuryType
        {
            get
            {
                return _strTreasuryType;
            }
            set
            {
                _strTreasuryType = value;
            }
        }

        public Int32 UserID
        {
            get
            {
                return _iuserID;
            }
            set
            {
                _iuserID = value;
            }
        }

        public Int32 InstallationNumber
        {
            get
            {
                return _iInstallationNumber;
            }
            set
            {
                _iInstallationNumber = value;
            }
        }

        public Int32 CollectionNumber
        {
            get
            {
                return _iCollectionNo;
            }
            set
            {
                _iCollectionNo = value;
            }
        }


        public bool TreasuryTemp
        {
            get
            {
                return b_TreasuryTemp;
            }
            set
            {
                b_TreasuryTemp = value;
            }
        }


        public Int32 TreasuryFloatIssuedBy
        {
            get
            {
                return _iTreasuryFloatIssuedBy;
            }
            set
            {
                _iTreasuryFloatIssuedBy = value;
            }
        }
    }

    public class PrintReceipt
    {
        private int _iInstallationID = 0;
        private string _strTreasuryType = string.Empty;
        private float _fTreasuryAmount = 0.00F;
        private int _iTreasuryID = 0;
        private string _strTreasuryDate = string.Empty;


        public int InstallationNumber
        {
            get
            {
                return _iInstallationID;
            }
            set
            {
                _iInstallationID = value;
            }
        }

        public string TreasuryType
        {
            get
            {
                return _strTreasuryType;
            }
            set
            {
                _strTreasuryType = value;
            }
        }

        public float TreasuryAmount
        {
            get
            {
                return _fTreasuryAmount;
            }
            set
            {
                _fTreasuryAmount = value;
            }
        }

        public int TreasuryID
        {
            get
            {
                return _iTreasuryID;
            }
            set
            {
                _iTreasuryID = value;
            }
        }
        public string TreasuryDate
        {
            get
            {
                return _strTreasuryDate;
            }
            set
            {
                _strTreasuryDate = value;
            }
        }

    }

    public class SpotCheckData
    {
        private Int32 _InstallationNo = 0;

        public Int32 InstallationNo
        {
            get { return _InstallationNo; }
            set { _InstallationNo = value; }
        }
        private Int32 _CashIn = 0;

        public Int32 CashIn
        {
            get { return _CashIn; }
            set { _CashIn = value; }
        }
        private int _CashOut = 0;

        public int CashOut
        {
            get { return _CashOut; }
            set { _CashOut = value; }
        }
        private int _TokenIn = 0;

        public int TokenIn
        {
            get { return _TokenIn; }
            set { _TokenIn = value; }
        }
        private int _TokenOut = 0;

        public int TokenOut
        {
            get { return _TokenOut; }
            set { _TokenOut = value; }
        }
        private int _TokenRefill = 0;

        public int TokenRefill
        {
            get { return _TokenRefill; }
            set { _TokenRefill = value; }
        }
        private int _CashRefill = 0;

        public int CashRefill
        {
            get { return _CashRefill; }
            set { _CashRefill = value; }
        }
        private int _CoinsIn = 0;

        public int CoinsIn
        {
            get { return _CoinsIn; }
            set { _CoinsIn = value; }
        }
        private int _CoinsOut = 0;

        public int CoinsOut
        {
            get { return _CoinsOut; }
            set { _CoinsOut = value; }
        }
        private double _CoinsDrop = 0;

        public double CoinsDrop
        {
            get { return _CoinsDrop; }
            set { _CoinsDrop = value; }
        }
        private int _CancelledCredits = 0;

        public int CancelledCredits
        {
            get { return _CancelledCredits; }
            set { _CancelledCredits = value; }
        }
        private int _VTP = 0;

        public int VTP
        {
            get { return _VTP; }
            set { _VTP = value; }
        }
        private DateTime _DateTimeStamp;

        public DateTime DateTimeStamp
        {
            get { return _DateTimeStamp; }
            set { _DateTimeStamp = value; }
        }

        private DateTime _Date;

        public DateTime Date
        {
            get { return _Date; }
            set { _Date = value; }
        }

        private int _Jackpot = 0;

        public int Jackpot
        {
            get { return _Jackpot; }
            set { _Jackpot = value; }
        }
        private int _Handpay = 0;

        public int Handpay
        {
            get { return _Handpay; }
            set { _Handpay = value; }
        }
        private int _Bill1 = 0;

        public int Bill1
        {
            get { return _Bill1; }
            set { _Bill1 = value; }
        }
        private int _Bill2 = 0;

        public int Bill2
        {
            get { return _Bill2; }
            set { _Bill2 = value; }
        }
        private int _Bill5 = 0;

        public int Bill5
        {
            get { return _Bill5; }
            set { _Bill5 = value; }
        }
        private int _Bill10 = 0;

        public int Bill10
        {
            get { return _Bill10; }
            set { _Bill10 = value; }
        }
        private int _Bill20 = 0;

        public int Bill20
        {
            get { return _Bill20; }
            set { _Bill20 = value; }
        }
        private int _Bill50 = 0;

        public int Bill50
        {
            get { return _Bill50; }
            set { _Bill50 = value; }
        }
        private int _Bill100 = 0;

        public int Bill100
        {
            get { return _Bill100; }
            set { _Bill100 = value; }
        }
        private int _Bill250 = 0;

        public int Bill250
        {
            get { return _Bill250; }
            set { _Bill250 = value; }
        }
        private int _Bill10000 = 0;

        public int Bill10000
        {
            get { return _Bill10000; }
            set { _Bill10000 = value; }
        }
        private int _Bill20000 = 0;

        public int Bill20000
        {
            get { return _Bill20000; }
            set { _Bill20000 = value; }
        }
        private int _Bill50000 = 0;

        public int Bill50000
        {
            get { return _Bill50000; }
            set { _Bill50000 = value; }
        }
        private int _Bill100000 = 0;

        public int Bill100000
        {
            get { return _Bill100000; }
            set { _Bill100000 = value; }
        }
        private int _TicketInserted = 0;

        public int TicketsInserted
        {
            get { return _TicketInserted; }
            set { _TicketInserted = value; }
        }
        private int _TrueCoinIn = 0;

        public int TrueCoinIn
        {
            get { return _TrueCoinIn; }
            set { _TrueCoinIn = value; }
        }
        private int _TrueCoinOut = 0;

        public int TrueCoinOut
        {
            get { return _TrueCoinOut; }
            set { _TrueCoinOut = value; }
        }
        private int _CashIn2P = 0;

        public int CashIn2P
        {
            get { return _CashIn2P; }
            set { _CashIn2P = value; }
        }
        private int _CashIn100P = 0;

        public int CashIn100P
        {
            get { return _CashIn100P; }
            set { _CashIn100P = value; }
        }
        private int _CashIn200P = 0;

        public int CashIn200P
        {
            get { return _CashIn200P; }
            set { _CashIn200P = value; }
        }
        private int _CashIn500P = 0;

        public int CashIn500P
        {
            get { return _CashIn500P; }
            set { _CashIn500P = value; }
        }
        private int _CashIn1000P = 0;

        public int CashIn1000P
        {
            get { return _CashIn1000P; }
            set { _CashIn1000P = value; }
        }
        private int _CashIn2000P = 0;

        public int CashIn2000P
        {
            get { return _CashIn2000P; }
            set { _CashIn2000P = value; }
        }
        private int _TicketsPrinted = 0;

        public int TicketsPrinted
        {
            get { return _TicketsPrinted; }
            set { _TicketsPrinted = value; }
        }
        private int _StartOfDay = 0;

        public int StartOfDay
        {
            get { return _StartOfDay; }
            set { _StartOfDay = value; }
        }
        private int _SelectDay = 0;

        public int SelectDay
        {
            get { return _SelectDay; }
            set { _SelectDay = value; }
        }
        private DateTime _Date;

        public DateTime Date
        {
            get { return _Date; }
            set { _Date = value; }
        }

        private int _Handle = 0;

        public int NumberOfDays
        {
            get { return _Handle; }
            set { _Handle = value; }
        }
        private int _type = 0;


        public int Type
        {
            get { return _type; }
            set { _type = value; }
        }

        private int _progHandpay = 0;

        public int ProgHandpay
        {
            get { return _progHandpay; }
            set { _progHandpay = value; }
        }

    }

    public class VoidTreasuryNegativeTransaction
    {
        private int _iTreasuryNumber = 0;
        private string _strTreasuryDate = string.Empty;
        private string _strTreasuryTime = string.Empty;
        private int _iUserID = 0;

        public int TreasuryNumber
        {
            get
            {
                return _iTreasuryNumber;
            }
            set
            {
                _iTreasuryNumber = value;
            }
        }
        public string TreasuryDate
        {
            get
            {
                return _strTreasuryDate;
            }
            set
            {
                _strTreasuryDate = value;
            }
        }
        public string TreasuryTime
        {
            get
            {
                return _strTreasuryTime;
            }
            set
            {
                _strTreasuryTime = value;
            }
        }
        public int UserID
        {
            get
            {
                return _iUserID;
            }
            set
            {
                _iUserID = value;
            }
        }
    }

    public class VoidTranCreate
    {
        private string _TreasuryID;
        private string _Date;
        private string _Time;
        private string _UserNo;

        public string TreasuryID
        {
            get { return _TreasuryID; }
            set { _TreasuryID = value; }
        }

        public string Date
        {
            get { return _Date; }
            set { _Date = value; }
        }

        public string Time
        {
            get { return _Time; }
            set { _Time = value; }
        }

        public string UserNo
        {
            get { return _UserNo; }
            set { _UserNo = value; }
        }
    }

    public class Site
    {
        private string _SiteCode, _SiteName;

        public string SiteCode
        {
            get { return _SiteCode; }
            set { _SiteCode = value; }
        }

        public string SiteName
        {
            get { return _SiteName; }
            set { _SiteName = value; }
        }
    }

    public class MachineDetails
    {
        private static string _TreasuryNo, _BarPosName, _StockNo, _MacName, _Value;

        public string BarPosName
        {
            get { return _BarPosName; }
            set { _BarPosName = value; }
        }

        public string StockNo
        {
            get { return _StockNo; }
            set { _StockNo = value; }
        }

        public string MacName
        {
            get { return _MacName; }
            set { _MacName = value; }
        }

        public string TreasuryNo
        {
            get { return _TreasuryNo; }
            set { _TreasuryNo = value; }
        }

        public string Value
        {
            get { return _Value; }
            set { _Value = value; }
        }

    }

    public class PlayerInfo
    {
        private string _accountNumber = string.Empty;
        private string _firstName = string.Empty;
        private string _lastName = string.Empty;
        private string _displayName = string.Empty;
        private int _playerId;
        private double _pointBal;
        private string _clubState = string.Empty;
        private string _clubStatus = string.Empty;


        public string AccountNumber
        {
            get { return this._accountNumber; }
            set { this._accountNumber = value; }
        }

        public string FirstName
        {
            get { return this._firstName; }
            set { this._firstName = value; }
        }

        public string LastName
        {
            get { return this._lastName; }
            set { this._lastName = value; }
        }

        public string DisplayName
        {
            get { return this._displayName; }
            set { this._displayName = value; }
        }

        public int PlayerId
        {
            get { return this._playerId; }
            set { this._playerId = value; }
        }

        public double PointsBalance
        {
            get { return this._pointBal; }
            set { this._pointBal = value; }
        }
        public string ClubState
        {
            get { return this._clubState; }
            set { this._clubState = value; }
        }
        public string ClubStatus
        {
            get { return this._clubStatus; }
            set { this._clubStatus = value; }
        }
    }


}
