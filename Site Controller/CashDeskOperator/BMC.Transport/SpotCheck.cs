using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;

namespace BMC.Transport.CashDeskOperatorEntity
{
    public class SpotCheck
    {

        public int InstallationNo { get; set; }
        public decimal CashIn { get; set; }
        public decimal CashOut { get; set; }
        public decimal TokenIn { get; set; }
        public decimal TokenOut { get; set; }
        public decimal TokenRefill { get; set; }
        public decimal CashRefill { get; set; }
        public decimal CoinsIn { get; set; }
        public decimal CoinsOut { get; set; }
        public double CoinsDrop { get; set; }
        public decimal Payout { get; set; }
        public decimal CancelledCredits { get; set; }
        public decimal VTP { get; set; }
        public DateTime DateTimeStamp { get; set; }
        public decimal Jackpot { get; set; }

        public decimal HandPay { get; set; }
        public decimal Bill1 { get; set; }
        public decimal Bill2 { get; set; }
        public decimal Bill5 { get; set; }
        public decimal Bill10 { get; set; }
        public decimal Bill20 { get; set; }
        public decimal Bill50 { get; set; }
        public decimal Bill100 { get; set; }
        public decimal Bill250 { get; set; }
        public decimal Bill200 { get; set; }
        public decimal Bill500 { get; set; }
        public decimal Bill10000 { get; set; }
        public decimal Bill20000 { get; set; }
        public decimal Bill50000 { get; set; }
        public decimal Bill100000 { get; set; }
        public decimal TicketsInserted { get; set; }
        public decimal TrueCoinIn { get; set; }
        public decimal TrueCoinOut { get; set; }
        public decimal CashIn1p { get; set; }
        public decimal CashIn2p { get; set; }
        public decimal CashIn100p { get; set; }
        public decimal CashIn200p { get; set; }
        public decimal CashIn500p { get; set; }
        public decimal CashIn1000p { get; set; }
        public decimal CashIn2000p { get; set; }
        public decimal TicketsPrinted { get; set; }
        public int StartOfDay { get; set; }
        public int SelectDay { get; set; }
        public DateTime Date { get; set; }
        public int NumberOfDays { get; set; }
        public int Type { get; set; }
        public decimal ProgHandpay { get; set; }
        public int GamesBet { get; set; }

        public decimal CashableEFTIn { get; set; }
        public decimal CashableEFTOut { get; set; }
        public decimal NonCashableEFTIn { get; set; }
        public decimal NonCashableEFTOut { get; set; }
        public decimal WATIn { get; set; }
        public decimal WATOut { get; set; }

        public decimal NonCashableTicketsInserted { get; set; }
        public decimal NonCashableTicketsPrinted { get; set; }        
    }

    public partial class rsp_GetInstallationDetailsForSpotCheckResult
    {

        private int _Installation_No;

        private string _Bar_Position_Name;

        private string _GameTitle;

        private System.Nullable<int> _POP;

        private System.Nullable<decimal> _PercentagePayout;

        private string _Zone_Name;

        private string _DisplayName;

        private System.Nullable<System.DateTime> _Installation_StartDate;

        public rsp_GetInstallationDetailsForSpotCheckResult()
        {
        }

        [Column(Storage = "_Installation_No", DbType = "Int NOT NULL")]
        public int Installation_No
        {
            get
            {
                return this._Installation_No;
            }
            set
            {
                if ((this._Installation_No != value))
                {
                    this._Installation_No = value;
                }
            }
        }

        [Column(Storage = "_Bar_Position_Name", DbType = "VarChar(50)")]
        public string Bar_Position_Name
        {
            get
            {
                return this._Bar_Position_Name;
            }
            set
            {
                if ((this._Bar_Position_Name != value))
                {
                    this._Bar_Position_Name = value;
                }
            }
        }

        [Column(Storage = "_GameTitle", DbType = "VarChar(50)")]
        public string GameTitle
        {
            get
            {
                return this._GameTitle;
            }
            set
            {
                if ((this._GameTitle != value))
                {
                    this._GameTitle = value;
                }
            }
        }

        [Column(Storage = "_POP", DbType = "Int")]
        public System.Nullable<int> POP
        {
            get
            {
                return this._POP;
            }
            set
            {
                if ((this._POP != value))
                {
                    this._POP = value;
                }
            }
        }

        [Column(Storage = "_PercentagePayout", DbType = "Decimal(0,0)")]
        public System.Nullable<decimal> PercentagePayout
        {
            get
            {
                return this._PercentagePayout;
            }
            set
            {
                if ((this._PercentagePayout != value))
                {
                    this._PercentagePayout = value;
                }
            }
        }

        [Column(Storage = "_Zone_Name", DbType = "VarChar(50)")]
        public string Zone_Name
        {
            get
            {
                return this._Zone_Name;
            }
            set
            {
                if ((this._Zone_Name != value))
                {
                    this._Zone_Name = value;
                }
            }
        }

        [Column(Storage = "_DisplayName", DbType = "VarChar(120)")]
        public string DisplayName
        {
            get
            {
                return this._DisplayName;
            }
            set
            {
                if ((this._DisplayName != value))
                {
                    this._DisplayName = value;
                }
            }
        }

        [Column(Storage = "_Installation_StartDate", DbType = "DateTime")]
        public System.Nullable<System.DateTime> Installation_StartDate
        {
            get
            {
                return this._Installation_StartDate;
            }
            set
            {
                if ((this._Installation_StartDate != value))
                {
                    this._Installation_StartDate = value;
                }
            }
        }
    }

    public class Installations : INotifyPropertyChanged
    {
        private int _Installation_No;
        
        private string _Bar_Position_Name;

        private string _GameTitle;

        private string _Zone_Name;

        private System.Nullable<int> _POP;

        private System.Nullable<decimal> _PercentagePayout;

        private string _DisplayName;

        private bool _IsSelected;

        private System.Nullable<System.DateTime> _Installation_StartDate;

        public Installations()
        {
        }

        [Column(Storage = "_Installation_No", DbType = "Int NOT NULL")]
        public int Installation_No
        {
            get
            {
                return this._Installation_No;
            }
            set
            {
                if ((this._Installation_No != value))
                {
                    this._Installation_No = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Installation_No"));
                    }
                }
            }
        }

        public string Bar_Position_Name
        {
            get
            {
                return this._Bar_Position_Name;
            }
            set
            {
                if ((this._Bar_Position_Name != value))
                {
                    this._Bar_Position_Name = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Bar_Position_Name"));
                    }
                }
            }
        }

        public string GameTitle
        {
            get
            {
                return this._GameTitle;
            }
            set
            {
                if ((this._GameTitle != value))
                {
                    this._GameTitle = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged.Invoke(this, new PropertyChangedEventArgs("GameTitle"));
                    }
                }
            }
        }

        public System.Nullable<int> POP
        {
            get
            {
                return this._POP;
            }
            set
            {
                if ((this._POP != value))
                {
                    this._POP = value;
                }
                if (PropertyChanged != null)
                {
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("POP"));
                }
            }
        }

        [Column(Storage = "_PercentagePayout", DbType = "Decimal(0,0)")]
        public System.Nullable<decimal> PercentagePayout
        {
            get
            {
                return this._PercentagePayout;
            }
            set
            {
                if ((this._PercentagePayout != value))
                {
                    this._PercentagePayout = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged.Invoke(this, new PropertyChangedEventArgs("PercentagePayout"));
                    }
                }
            }
        }

        public string Zone_Name
        {
            get
            {
                return _Zone_Name;
            }
            set
            {
                if ((this._Zone_Name != value))
                {
                    this._Zone_Name = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Zone_Name"));
                    }
                }
            }
        }

        public string DisplayName
        {
            get
            {
                return this._DisplayName;
            }
            set
            {
                if ((this._DisplayName != value))
                {
                    this._DisplayName = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged.Invoke(this, new PropertyChangedEventArgs("DisplayName"));
                    }
                }
            }
        }

        public System.Nullable<System.DateTime> Installation_StartDate
        {
            get
            {
                return this._Installation_StartDate;
            }
            set
            {
                if ((this._Installation_StartDate != value))
                {
                    this._Installation_StartDate = value;
                }
            }
        }

        public bool IsSelected
        {
            get
            {
                return this._IsSelected;
            }
            set
            {
                if ((this._IsSelected != value))
                {
                    this._IsSelected = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged.Invoke(this, new PropertyChangedEventArgs("IsSelected"));
                    }
                }
            }
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }

    public partial class usp_GetSpotCheckDataByDropResult
    {

        private System.Nullable<int> _Installation_No;

        private System.Nullable<decimal> _CASH_IN;

        private System.Nullable<decimal> _CASH_OUT;

        private System.Nullable<int> _TOKEN_IN;

        private System.Nullable<int> _TOKEN_OUT;

        private System.Nullable<int> _CASH_REFILL;

        private System.Nullable<int> _TOKEN_REFILL;

        private System.Nullable<decimal> _COINS_IN;

        private System.Nullable<decimal> _COINS_OUT;

        private System.Nullable<decimal> _COINS_DROP;

        private System.Nullable<int> _CANCELLED_CREDITS;

        private System.Nullable<int> _VTP;

        private System.Nullable<System.DateTime> _DateTimeStamp;

        private System.Nullable<System.DateTime> _Date;

        private System.Nullable<int> _JACKPOT;

        private System.Nullable<decimal> _Handpay;

        private System.Nullable<int> _BILL_1;

        private System.Nullable<int> _BILL_2;

        private System.Nullable<int> _BILL_5;

        private System.Nullable<int> _BILL_10;

        private System.Nullable<int> _BILL_20;

        private System.Nullable<int> _BILL_50;

        private System.Nullable<int> _BILL_100;

        private System.Nullable<int> _BILL_250;

        private System.Nullable<int> _BILL_10000;

        private System.Nullable<int> _BILL_20000;

        private System.Nullable<int> _BILL_50000;

        private System.Nullable<int> _BILL_100000;

        private System.Nullable<long> _Ticktes_Inserted;

        private System.Nullable<int> _TRUE_COIN_IN;

        private System.Nullable<int> _TRUE_COIN_OUT;

        private System.Nullable<int> _CASH_IN_1P;
        
        private System.Nullable<int> _CASH_IN_2P;

        private System.Nullable<int> _CASH_IN_100P;

        private System.Nullable<int> _CASH_IN_200P;

        private System.Nullable<int> _CASH_IN_500P;

        private System.Nullable<int> _CASH_IN_1000P;

        private System.Nullable<int> _CASH_IN_2000P;

        private System.Nullable<int> _Tickets_Printed;

        private System.Nullable<int> _Games_Bet;

        private System.Nullable<int> _ProgHandpay;

        private System.Nullable<int> _BILL_200;

        private System.Nullable<int> _BILL_500;

        private System.Nullable<int> _CashableAFTIn;

        private System.Nullable<int> _CashableAFTOut;

        private System.Nullable<int> _NonCashableAFTIn;

        private System.Nullable<int> _NonCashableAFTOut;

        private System.Nullable<int> _WATIn;

        private System.Nullable<int> _WATOut;

        private System.Nullable<int> _NonCashable_Tickets_Inserted;

        private System.Nullable<int> _NonCashable_Tickets_Printed;

        private System.Nullable<decimal> _PercentagePayout;

        public usp_GetSpotCheckDataByDropResult()
        {
        }

        [Column(Storage = "_Installation_No", DbType = "Int")]
        public System.Nullable<int> Installation_No
        {
            get
            {
                return this._Installation_No;
            }
            set
            {
                if ((this._Installation_No != value))
                {
                    this._Installation_No = value;
                }
            }
        }

        [Column(Storage = "_CASH_IN", DbType = "Decimal(0,0)")]
        public System.Nullable<decimal> CASH_IN
        {
            get
            {
                return this._CASH_IN;
            }
            set
            {
                if ((this._CASH_IN != value))
                {
                    this._CASH_IN = value;
                }
            }
        }

        [Column(Storage = "_CASH_OUT", DbType = "Decimal(0,0)")]
        public System.Nullable<decimal> CASH_OUT
        {
            get
            {
                return this._CASH_OUT;
            }
            set
            {
                if ((this._CASH_OUT != value))
                {
                    this._CASH_OUT = value;
                }
            }
        }

        [Column(Storage = "_TOKEN_IN", DbType = "Int")]
        public System.Nullable<int> TOKEN_IN
        {
            get
            {
                return this._TOKEN_IN;
            }
            set
            {
                if ((this._TOKEN_IN != value))
                {
                    this._TOKEN_IN = value;
                }
            }
        }

        [Column(Storage = "_PercentagePayout", DbType = "Decimal(0,0)")]
        public System.Nullable<decimal> PercentagePayout
        {
            get
            {
                return this._PercentagePayout;
            }
            set
            {
                if ((this._PercentagePayout != value))
                {
                    this._PercentagePayout = value;
                }
            }
        }

        [Column(Storage = "_TOKEN_OUT", DbType = "Int")]
        public System.Nullable<int> TOKEN_OUT
        {
            get
            {
                return this._TOKEN_OUT;
            }
            set
            {
                if ((this._TOKEN_OUT != value))
                {
                    this._TOKEN_OUT = value;
                }
            }
        }

        [Column(Storage = "_CASH_REFILL", DbType = "Int")]
        public System.Nullable<int> CASH_REFILL
        {
            get
            {
                return this._CASH_REFILL;
            }
            set
            {
                if ((this._CASH_REFILL != value))
                {
                    this._CASH_REFILL = value;
                }
            }
        }

        [Column(Storage = "_TOKEN_REFILL", DbType = "Int")]
        public System.Nullable<int> TOKEN_REFILL
        {
            get
            {
                return this._TOKEN_REFILL;
            }
            set
            {
                if ((this._TOKEN_REFILL != value))
                {
                    this._TOKEN_REFILL = value;
                }
            }
        }

        [Column(Storage = "_COINS_IN", DbType = "Decimal(0,0)")]
        public System.Nullable<decimal> COINS_IN
        {
            get
            {
                return this._COINS_IN;
            }
            set
            {
                if ((this._COINS_IN != value))
                {
                    this._COINS_IN = value;
                }
            }
        }

        [Column(Storage = "_COINS_OUT", DbType = "Decimal(0,0)")]
        public System.Nullable<decimal> COINS_OUT
        {
            get
            {
                return this._COINS_OUT;
            }
            set
            {
                if ((this._COINS_OUT != value))
                {
                    this._COINS_OUT = value;
                }
            }
        }

        [Column(Storage = "_COINS_DROP", DbType = "Decimal(0,0)")]
        public System.Nullable<decimal> COINS_DROP
        {
            get
            {
                return this._COINS_DROP;
            }
            set
            {
                if ((this._COINS_DROP != value))
                {
                    this._COINS_DROP = value;
                }
            }
        }

        [Column(Storage = "_CANCELLED_CREDITS", DbType = "Int")]
        public System.Nullable<int> CANCELLED_CREDITS
        {
            get
            {
                return this._CANCELLED_CREDITS;
            }
            set
            {
                if ((this._CANCELLED_CREDITS != value))
                {
                    this._CANCELLED_CREDITS = value;
                }
            }
        }

        [Column(Storage = "_VTP", DbType = "Int")]
        public System.Nullable<int> VTP
        {
            get
            {
                return this._VTP;
            }
            set
            {
                if ((this._VTP != value))
                {
                    this._VTP = value;
                }
            }
        }

        [Column(Storage = "_DateTimeStamp", DbType = "DateTime")]
        public System.Nullable<System.DateTime> DateTimeStamp
        {
            get
            {
                return this._DateTimeStamp;
            }
            set
            {
                if ((this._DateTimeStamp != value))
                {
                    this._DateTimeStamp = value;
                }
            }
        }

        [Column(Storage = "_Date", DbType = "DateTime")]
        public System.Nullable<System.DateTime> Date
        {
            get
            {
                return this._Date;
            }
            set
            {
                if ((this._Date != value))
                {
                    this._Date = value;
                }
            }
        }

        [Column(Storage = "_JACKPOT", DbType = "Int")]
        public System.Nullable<int> JACKPOT
        {
            get
            {
                return this._JACKPOT;
            }
            set
            {
                if ((this._JACKPOT != value))
                {
                    this._JACKPOT = value;
                }
            }
        }

        [Column(Storage = "_Handpay", DbType = "Decimal(0,0)")]
        public System.Nullable<decimal> Handpay
        {
            get
            {
                return this._Handpay;
            }
            set
            {
                if ((this._Handpay != value))
                {
                    this._Handpay = value;
                }
            }
        }

        [Column(Storage = "_BILL_1", DbType = "Int")]
        public System.Nullable<int> BILL_1
        {
            get
            {
                return this._BILL_1;
            }
            set
            {
                if ((this._BILL_1 != value))
                {
                    this._BILL_1 = value;
                }
            }
        }

        [Column(Storage = "_BILL_2", DbType = "Int")]
        public System.Nullable<int> BILL_2
        {
            get
            {
                return this._BILL_2;
            }
            set
            {
                if ((this._BILL_2 != value))
                {
                    this._BILL_2 = value;
                }
            }
        }

        [Column(Storage = "_BILL_5", DbType = "Int")]
        public System.Nullable<int> BILL_5
        {
            get
            {
                return this._BILL_5;
            }
            set
            {
                if ((this._BILL_5 != value))
                {
                    this._BILL_5 = value;
                }
            }
        }

        [Column(Storage = "_BILL_10", DbType = "Int")]
        public System.Nullable<int> BILL_10
        {
            get
            {
                return this._BILL_10;
            }
            set
            {
                if ((this._BILL_10 != value))
                {
                    this._BILL_10 = value;
                }
            }
        }

        [Column(Storage = "_BILL_20", DbType = "Int")]
        public System.Nullable<int> BILL_20
        {
            get
            {
                return this._BILL_20;
            }
            set
            {
                if ((this._BILL_20 != value))
                {
                    this._BILL_20 = value;
                }
            }
        }

        [Column(Storage = "_BILL_50", DbType = "Int")]
        public System.Nullable<int> BILL_50
        {
            get
            {
                return this._BILL_50;
            }
            set
            {
                if ((this._BILL_50 != value))
                {
                    this._BILL_50 = value;
                }
            }
        }

        [Column(Storage = "_BILL_100", DbType = "Int")]
        public System.Nullable<int> BILL_100
        {
            get
            {
                return this._BILL_100;
            }
            set
            {
                if ((this._BILL_100 != value))
                {
                    this._BILL_100 = value;
                }
            }
        }

        [Column(Storage = "_BILL_250", DbType = "Int")]
        public System.Nullable<int> BILL_250
        {
            get
            {
                return this._BILL_250;
            }
            set
            {
                if ((this._BILL_250 != value))
                {
                    this._BILL_250 = value;
                }
            }
        }

        [Column(Storage = "_BILL_10000", DbType = "Int")]
        public System.Nullable<int> BILL_10000
        {
            get
            {
                return this._BILL_10000;
            }
            set
            {
                if ((this._BILL_10000 != value))
                {
                    this._BILL_10000 = value;
                }
            }
        }

        [Column(Storage = "_BILL_20000", DbType = "Int")]
        public System.Nullable<int> BILL_20000
        {
            get
            {
                return this._BILL_20000;
            }
            set
            {
                if ((this._BILL_20000 != value))
                {
                    this._BILL_20000 = value;
                }
            }
        }

        [Column(Storage = "_BILL_50000", DbType = "Int")]
        public System.Nullable<int> BILL_50000
        {
            get
            {
                return this._BILL_50000;
            }
            set
            {
                if ((this._BILL_50000 != value))
                {
                    this._BILL_50000 = value;
                }
            }
        }

        [Column(Storage = "_BILL_100000", DbType = "Int")]
        public System.Nullable<int> BILL_100000
        {
            get
            {
                return this._BILL_100000;
            }
            set
            {
                if ((this._BILL_100000 != value))
                {
                    this._BILL_100000 = value;
                }
            }
        }

        [Column(Storage = "_Ticktes_Inserted", DbType = "BigInt")]
        public System.Nullable<long> Ticktes_Inserted
        {
            get
            {
                return this._Ticktes_Inserted;
            }
            set
            {
                if ((this._Ticktes_Inserted != value))
                {
                    this._Ticktes_Inserted = value;
                }
            }
        }

        [Column(Storage = "_TRUE_COIN_IN", DbType = "Int")]
        public System.Nullable<int> TRUE_COIN_IN
        {
            get
            {
                return this._TRUE_COIN_IN;
            }
            set
            {
                if ((this._TRUE_COIN_IN != value))
                {
                    this._TRUE_COIN_IN = value;
                }
            }
        }

        [Column(Storage = "_TRUE_COIN_OUT", DbType = "Int")]
        public System.Nullable<int> TRUE_COIN_OUT
        {
            get
            {
                return this._TRUE_COIN_OUT;
            }
            set
            {
                if ((this._TRUE_COIN_OUT != value))
                {
                    this._TRUE_COIN_OUT = value;
                }
            }
        }

        [Column(Storage = "_CASH_IN_1P", DbType = "Int")]
        public System.Nullable<int> CASH_IN_1P
        {
            get
            {
                return this._CASH_IN_1P;
            }
            set
            {
                if ((this._CASH_IN_1P != value))
                {
                    this._CASH_IN_1P = value;
                }
            }
        }

        [Column(Storage = "_CASH_IN_2P", DbType = "Int")]
        public System.Nullable<int> CASH_IN_2P
        {
            get
            {
                return this._CASH_IN_2P;
            }
            set
            {
                if ((this._CASH_IN_2P != value))
                {
                    this._CASH_IN_2P = value;
                }
            }
        }

        [Column(Storage = "_CASH_IN_100P", DbType = "Int")]
        public System.Nullable<int> CASH_IN_100P
        {
            get
            {
                return this._CASH_IN_100P;
            }
            set
            {
                if ((this._CASH_IN_100P != value))
                {
                    this._CASH_IN_100P = value;
                }
            }
        }

        [Column(Storage = "_CASH_IN_200P", DbType = "Int")]
        public System.Nullable<int> CASH_IN_200P
        {
            get
            {
                return this._CASH_IN_200P;
            }
            set
            {
                if ((this._CASH_IN_200P != value))
                {
                    this._CASH_IN_200P = value;
                }
            }
        }

        [Column(Storage = "_CASH_IN_500P", DbType = "Int")]
        public System.Nullable<int> CASH_IN_500P
        {
            get
            {
                return this._CASH_IN_500P;
            }
            set
            {
                if ((this._CASH_IN_500P != value))
                {
                    this._CASH_IN_500P = value;
                }
            }
        }

        [Column(Storage = "_CASH_IN_1000P", DbType = "Int")]
        public System.Nullable<int> CASH_IN_1000P
        {
            get
            {
                return this._CASH_IN_1000P;
            }
            set
            {
                if ((this._CASH_IN_1000P != value))
                {
                    this._CASH_IN_1000P = value;
                }
            }
        }

        [Column(Storage = "_CASH_IN_2000P", DbType = "Int")]
        public System.Nullable<int> CASH_IN_2000P
        {
            get
            {
                return this._CASH_IN_2000P;
            }
            set
            {
                if ((this._CASH_IN_2000P != value))
                {
                    this._CASH_IN_2000P = value;
                }
            }
        }

        [Column(Storage = "_Tickets_Printed", DbType = "Int")]
        public System.Nullable<int> Tickets_Printed
        {
            get
            {
                return this._Tickets_Printed;
            }
            set
            {
                if ((this._Tickets_Printed != value))
                {
                    this._Tickets_Printed = value;
                }
            }
        }

        [Column(Storage = "_Games_Bet", DbType = "Int")]
        public System.Nullable<int> Games_Bet
        {
            get
            {
                return this._Games_Bet;
            }
            set
            {
                if ((this._Games_Bet != value))
                {
                    this._Games_Bet = value;
                }
            }
        }

        [Column(Storage = "_ProgHandpay", DbType = "Int")]
        public System.Nullable<int> ProgHandpay
        {
            get
            {
                return this._ProgHandpay;
            }
            set
            {
                if ((this._ProgHandpay != value))
                {
                    this._ProgHandpay = value;
                }
            }
        }

        [Column(Storage = "_BILL_200", DbType = "Int")]
        public System.Nullable<int> BILL_200
        {
            get
            {
                return this._BILL_200;
            }
            set
            {
                if ((this._BILL_200 != value))
                {
                    this._BILL_200 = value;
                }
            }
        }

        [Column(Storage = "_BILL_500", DbType = "Int")]
        public System.Nullable<int> BILL_500
        {
            get
            {
                return this._BILL_500;
            }
            set
            {
                if ((this._BILL_500 != value))
                {
                    this._BILL_500 = value;
                }
            }
        }

        [Column(Storage = "_CashableAFTIn", DbType = "Int")]
        public System.Nullable<int> CashableAFTIn
        {
            get
            {
                return this._CashableAFTIn;
            }
            set
            {
                if ((this._CashableAFTIn != value))
                {
                    this._CashableAFTIn = value;
                }
            }
        }

        [Column(Storage = "_CashableAFTOut", DbType = "Int")]
        public System.Nullable<int> CashableAFTOut
        {
            get
            {
                return this._CashableAFTOut;
            }
            set
            {
                if ((this._CashableAFTOut != value))
                {
                    this._CashableAFTOut = value;
                }
            }
        }

        [Column(Storage = "_NonCashableAFTIn", DbType = "Int")]
        public System.Nullable<int> NonCashableAFTIn
        {
            get
            {
                return this._NonCashableAFTIn;
            }
            set
            {
                if ((this._NonCashableAFTIn != value))
                {
                    this._NonCashableAFTIn = value;
                }
            }
        }

        [Column(Storage = "_NonCashableAFTOut", DbType = "Int")]
        public System.Nullable<int> NonCashableAFTOut
        {
            get
            {
                return this._NonCashableAFTOut;
            }
            set
            {
                if ((this._NonCashableAFTOut != value))
                {
                    this._NonCashableAFTOut = value;
                }
            }
        }

        [Column(Storage = "_WATIn", DbType = "Int")]
        public System.Nullable<int> WATIn
        {
            get
            {
                return this._WATIn;
            }
            set
            {
                if ((this._WATIn != value))
                {
                    this._WATIn = value;
                }
            }
        }

        [Column(Storage = "_WATOut", DbType = "Int")]
        public System.Nullable<int> WATOut
        {
            get
            {
                return this._WATOut;
            }
            set
            {
                if ((this._WATOut != value))
                {
                    this._WATOut = value;
                }
            }
        }

        [Column(Storage = "_NonCashable_Tickets_Inserted", DbType = "Int")]
        public System.Nullable<int> NonCashable_Tickets_Inserted
        {
            get
            {
                return this._NonCashable_Tickets_Inserted;
            }
            set
            {
                if ((this._NonCashable_Tickets_Inserted != value))
                {
                    this._NonCashable_Tickets_Inserted = value;
                }
            }
        }

        [Column(Storage = "_NonCashable_Tickets_Printed", DbType = "Int")]
        public System.Nullable<int> NonCashable_Tickets_Printed
        {
            get
            {
                return this._NonCashable_Tickets_Printed;
            }
            set
            {
                if ((this._NonCashable_Tickets_Printed != value))
                {
                    this._NonCashable_Tickets_Printed = value;
                }
            }
        }
    }
}
