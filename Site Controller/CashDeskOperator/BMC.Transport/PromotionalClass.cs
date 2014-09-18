using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;

namespace BMC.Transport.CashDeskOperatorEntity
{
    public partial class PromotionalClass
    {
        private Int64 _SrNo;
        private Int64 _SrNo1;
        private int _PromotionalID;
        private string _PromotionalName;
        private string _PromotionalTicketType;      
        private int _TotalTickets;
        private decimal _PromotionalTicketAmount;
        private decimal _TotalTicketAmount;
        private DateTime _dtPromoCreation;
        private string _PromoStatus;
        private DateTime _dtExpire;
        private int _NoOfRedeemed;
        private decimal _RedeemedAmount;
        private int _NoOfTicketExpired;
        private decimal _ExpiredAmount;
        private int _NoOfTicketsVoid;
        private decimal _VoidAmount;
        public PromotionalClass()
        {
        }
        [Column(Storage = "_SrNo", DbType = "BigInt")]
      
        public Int64 SrNo
        {
            get
            {
                return this._SrNo;
            }
            set
            {
                if ((this._SrNo != value))
                {
                    this._SrNo = value;
                }
            }
        }

        [Column(Storage = "_SrNo1", DbType = "BigInt")]

        public Int64 SrNo1
        {
            get
            {
                return this._SrNo1;
            }
            set
            {
                if ((this._SrNo1 != value))
                {
                    this._SrNo1 = value;
                }
            }
        }

        [Column(Storage = "_PromotionalID", DbType = "Int NOT NULL", IsPrimaryKey = true)]
        public int PromotionalID
        {
            get
            {
                return this._PromotionalID;
            }
            set
            {
                if ((this._PromotionalID != value))
                {
                    this._PromotionalID = value;
                }
            }
        }

        [Column(Storage = "_PromotionalName", DbType = "Varchar(255)")]
        public string PromotionalName
        {
            get
            {
                return this._PromotionalName;
            }
            set
            {
                if ((this._PromotionalName != value))
                {
                    this._PromotionalName = value;
                }
            }
        }

        [Column(Storage = "_PromotionalTicketType", DbType = "Varchar(15)")]
        public string PromotionalTicketType
        {
            get
            {
                return this._PromotionalTicketType;
            }
            set
            {
                if ((this._PromotionalTicketType != value))
                {
                    this._PromotionalTicketType = value;
                }
            }
        }

        [Column(Storage = "_TotalTickets", DbType = "int")]
        public int TotalTickets
        {
            get
            {
                return this._TotalTickets;
            }
            set
            {
                if ((this._TotalTickets != value))
                {
                    this._TotalTickets = value;
                }
            }
        }

        [Column(Storage = "_PromotionalTicketAmount", DbType = "decimal(18,2)")]
        public decimal PromotionalTicketAmount
        {
            get
            {
                return this._PromotionalTicketAmount;
            }
            set
            {
                if ((this._PromotionalTicketAmount != value))
                {
                    this._PromotionalTicketAmount = value;
                }
            }
        }

        [Column(Storage = "_TotalTicketAmount", DbType = "decimal(18,2)")]
        public decimal TotalTicketAmount
        {
            get
            {
                return this._TotalTicketAmount;
            }
            set
            {
                if ((this._TotalTicketAmount != value))
                {
                    this._TotalTicketAmount = value;
                }
            }
        }
        [Column(Storage = "_PromoStatus", DbType = "Varchar(15)")]
        public string PromoStatus
        {
            get
            {
                return this._PromoStatus;
            }
            set
            {
                if ((this._PromoStatus != value))
                {
                    this._PromoStatus = value;
                }
            }
        }

        [Column(Storage = "_dtPromoCreation", DbType = "DateTime")]
        public DateTime dtPromoCreation
        {
            get
            {
                return this._dtPromoCreation;
            }
            set
            {
                if ((this._dtPromoCreation != value))
                {
                    this._dtPromoCreation = value;
                }
            }
        }

        [Column(Storage = "_dtExpire", DbType = "DateTime")]
        public DateTime dtExpire
        {
            get
            {
                return this._dtExpire;
            }
            set
            {
                if ((this._dtExpire != value))
                {
                    this._dtExpire = value;
                }
            }
        }
        [Column(Storage = "_NoOfRedeemed", DbType = "int")]
        public int NoOfRedeemed
        {
            get
            {
                return this._NoOfRedeemed;
            }
            set
            {
                if ((this._NoOfRedeemed != value))
                {
                    this._NoOfRedeemed = value;
                }
            }
        }
        [Column(Storage = "_RedeemedAmount", DbType = "decimal(18,2)")]
        public decimal RedeemedAmount
        {
            get
            {
                return this._RedeemedAmount;
            }
            set
            {
                if ((this._RedeemedAmount != value))
                {
                    this._RedeemedAmount = value;
                }
            }
        }
        [Column(Storage = "_NoOfTicketExpired", DbType = "int")]
        public int NoOfTicketExpired
        {
            get
            {
                return this._NoOfTicketExpired;
            }
            set
            {
                if ((this._NoOfTicketExpired != value))
                {
                    this._NoOfTicketExpired = value;
                }
            }
        }

        [Column(Storage = "_ExpiredAmount", DbType = "decimal(18,2)")]
        public decimal ExpiredAmount
        {
            get
            {
                return this._ExpiredAmount;
            }
            set
            {
                if ((this._ExpiredAmount != value))
                {
                    this._ExpiredAmount = value;
                }
            }
        }
        [Column(Storage = "_NoOfTicketsVoid", DbType = "int")]
        public int NoOfTicketsVoid
        {
            get
            {
                return this._NoOfTicketsVoid;
            }
            set
            {
                if ((this._NoOfTicketsVoid != value))
                {
                    this._NoOfTicketsVoid = value;
                }
            }
        }

        [Column(Storage = "_VoidAmount", DbType = "decimal(18,2)")]
        public decimal VoidAmount
        {
            get
            {
                return this._VoidAmount;
            }
            set
            {
                if ((this._VoidAmount != value))
                {
                    this._VoidAmount = value;
                }
            }
        }
    }

    //public partial class PromotionalClassHistoryDetails
    //{
    //    private string _strBarCode;
    //    private string _TicketType;
    //    private int _iAmount;
    //    private DateTime _dtPrinted;
    //    private DateTime _dtExpire;
    //    private string _VoucherStatus;
    //    public PromotionalClassHistoryDetails()
    //    {
    //    }
    //    [Column(Storage = "_strBarCode", DbType = "CHAR(32)")]
    //    public string strBarCode
    //    {
    //        get
    //        {
    //            return this._strBarCode;
    //        }
    //        set
    //        {
    //            if ((this._strBarCode != value))
    //            {
    //                this._strBarCode = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_TicketType", DbType = "varchar(15)")]
    //    public string TicketType
    //    {
    //        get
    //        {
    //            return this._TicketType;
    //        }
    //        set
    //        {
    //            if ((this._TicketType != value))
    //            {
    //                this._TicketType = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_iAmount", DbType = "int")]
    //    public int iAmount
    //    {
    //        get
    //        {
    //            return this._iAmount;
    //        }
    //        set
    //        {
    //            if ((this._iAmount != value))
    //            {
    //                this._iAmount = value;
    //            }
    //        }
    //    }
    //    [Column(Storage = "_dtPrinted", DbType = "DateTime")]
    //    public DateTime dtPrinted
    //    {
    //        get
    //        {
    //            return this._dtPrinted;
    //        }
    //        set
    //        {
    //            if ((this._dtPrinted != value))
    //            {
    //                this._dtPrinted = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_dtExpire", DbType = "DateTime")]
    //    public DateTime dtExpire
    //    {
    //        get
    //        {
    //            return this._dtExpire;
    //        }
    //        set
    //        {
    //            if ((this._dtExpire != value))
    //            {
    //                this._dtExpire = value;
    //            }
    //        }
    //    }
    //    [Column(Storage = "_VoucherStatus", DbType = "varchar(15)")]
    //    public string VoucherStatus
    //    {
    //        get
    //        {
    //            return this._VoucherStatus;
    //        }
    //        set
    //        {
    //            if ((this._VoucherStatus != value))
    //            {
    //                this._VoucherStatus = value;
    //            }
    //        }
    //    }

    //}
    public partial class PromotionalClassHistoryDetails
    {
        private string _strBarCode;
        private string _TicketType;
        private decimal _iAmount;
       // private DateTime _dtPrinted;
        private DateTime _dtExpire;
        private string _VoucherStatus;
        public PromotionalClassHistoryDetails()
        {
        }
        [Column(Storage = "_strBarCode", DbType = "CHAR(32)")]
        public string strBarCode
        {
            get
            {
                return this._strBarCode;
            }
            set
            {
                if ((this._strBarCode != value))
                {
                    this._strBarCode = value;
                }
            }
        }

        [Column(Storage = "_TicketType", DbType = "varchar(15)")]
        public string TicketType
        {
            get
            {
                return this._TicketType;
            }
            set
            {
                if ((this._TicketType != value))
                {
                    this._TicketType = value;
                }
            }
        }

        [Column(Storage = "_iAmount", DbType = "decimal(18,2)")]
        public decimal iAmount
        {
            get
            {

                return this._iAmount;
            }
            set
            {
                if ((this._iAmount != value))
                {
                    this._iAmount = value;
                }
            }
        }
        //[Column(Storage = "_dtPrinted", DbType = "DateTime")]
        //public DateTime dtPrinted
        //{
        //    get
        //    {
        //        return this._dtPrinted;
        //    }
        //    set
        //    {
        //        if ((this._dtPrinted != value))
        //        {
        //            this._dtPrinted = value;
        //        }
        //    }
        //}

        [Column(Storage = "_dtExpire", DbType = "DateTime")]
        public DateTime dtExpire
        {
            get
            {
                return _dtExpire;
            }
            set
            {
                if ((this._dtExpire != value))
                {
                    this._dtExpire = value;
                }
            }
        }
        [Column(Storage = "_VoucherStatus", DbType = "varchar(15)")]
        public string VoucherStatus
        {
            get
            {
                return this._VoucherStatus;
            }
            set
            {
                if ((this._VoucherStatus != value))
                {
                    this._VoucherStatus = value;
                }
            }
        }

    }
    public partial class PromotionalClassVoidDetails
    {
        private Int64 _SrNo;
        private int _PromotionalID;
        private string _PromotionalName;
        private string _PromotionalTicketType;
        private int _TotalTickets;
        private decimal _PromotionalTicketAmount;
        private decimal _TotalTicketAmount;
        private DateTime _dtPromoCreation;
        private DateTime _dtExpire;

        public PromotionalClassVoidDetails()
        {
        }
        [Column(Storage = "_SrNo", DbType = "BigInt")]
        public Int64 SrNo
        {
            get
            {
                return this._SrNo;
            }
            set
            {
                if ((this._SrNo != value))
                {
                    this._SrNo = value;
                }
            }
        }

        [Column(Storage = "_PromotionalID", DbType = "Int NOT NULL", IsPrimaryKey = true)]
        public int PromotionalID
        {
            get
            {
                return this._PromotionalID;
            }
            set
            {
                if ((this._PromotionalID != value))
                {
                    this._PromotionalID = value;
                }
            }
        }

        [Column(Storage = "_PromotionalName", DbType = "Varchar(50)")]
        public string PromotionalName
        {
            get
            {
                return this._PromotionalName;
            }
            set
            {
                if ((this._PromotionalName != value))
                {
                    this._PromotionalName = value;
                }
            }
        }

        [Column(Storage = "_PromotionalTicketType", DbType = "varchar(15)")]
        public string PromotionalTicketType
        {
            get
            {
                return this._PromotionalTicketType;
            }
            set
            {
                if ((this._PromotionalTicketType != value))
                {
                    this._PromotionalTicketType = value;
                }
            }
        }

        [Column(Storage = "_TotalTickets", DbType = "int")]
        public int TotalTickets
        {
            get
            {
                return this._TotalTickets;
            }
            set
            {
                if ((this._TotalTickets != value))
                {
                    this._TotalTickets = value;
                }
            }
        }

        [Column(Storage = "_PromotionalTicketAmount", DbType = "DECIMAL(18,2)")]
        public decimal PromotionalTicketAmount
        {
            get
            {
                return this._PromotionalTicketAmount;
            }
            set
            {
                if ((this._PromotionalTicketAmount != value))
                {
                    this._PromotionalTicketAmount = value;
                }
            }
        }

        [Column(Storage = "_TotalTicketAmount", DbType = "DECIMAL(18,2)")]
        public decimal TotalTicketAmount
        {
            get
            {
                return this._TotalTicketAmount;
            }
            set
            {
                if ((this._TotalTicketAmount != value))
                {
                    this._TotalTicketAmount = value;
                }
            }
        }

         [Column(Storage = "_dtExpire", DbType = "DateTime")]
        public DateTime dtExpire
        {
            get
            {
                return this._dtExpire;
            }
            set
            {
                if ((this._dtExpire != value))
                {
                    this._dtExpire = value;
                }
            }
        }

        [Column(Storage = "_dtPromoCreation", DbType = "DateTime")]
         public DateTime dtPromoCreation
        {
            get
            {
                return this._dtPromoCreation;
            }
            set
            {
                if ((this._dtPromoCreation != value))
                {
                    this._dtPromoCreation = value;
                }
            }
        }


    }

    public partial class TISPromotionalClassDetails
    {
        private Int64 _SrNo;
        private string _BarCode;
        private string _PromotionalTicketType;
        private decimal _PromotionalTicketAmount;
         private DateTime _dtPrinted;
        private DateTime _dtExpire;
        private string _VoucherStatus;
        public TISPromotionalClassDetails()
        {
        }
        [Column(Storage = "_SrNo", DbType = "BigInt")]

        public Int64 SrNo
        {
            get
            {
                return this._SrNo;
            }
            set
            {
                if ((this._SrNo != value))
                {
                    this._SrNo = value;
                }
            }
        }
        [Column(Storage = "_BarCode", DbType = "CHAR(32)")]
        public string BarCode
        {
            get
            {
                return this._BarCode;
            }
            set
            {
                if ((this._BarCode != value))
                {
                    this._BarCode = value;
                }
            }
        }

        [Column(Storage = "_PromotionalTicketType", DbType = "varchar(15)")]
        public string PromotionalTicketType
        {
            get
            {
                return this._PromotionalTicketType;
            }
            set
            {
                if ((this._PromotionalTicketType != value))
                {
                    this._PromotionalTicketType = value;
                }
            }
        }

        [Column(Storage = "_PromotionalTicketAmount", DbType = "decimal(18,2)")]
        public decimal PromotionalTicketAmount
        {
            get
            {

                return this._PromotionalTicketAmount;
            }
            set
            {
                if ((this._PromotionalTicketAmount != value))
                {
                    this._PromotionalTicketAmount = value;
                }
            }
        }
        [Column(Storage = "_dtPrinted", DbType = "DateTime")]
        public DateTime dtPrinted
        {
            get
            {
                return this._dtPrinted;
            }
            set
            {
                if ((this._dtPrinted != value))
                {
                    this._dtPrinted = value;
                }
            }
        }

        [Column(Storage = "_dtExpire", DbType = "DateTime")]
        public DateTime dtExpire
        {
            get
            {
                return this._dtExpire;
            }
            set
            {
                if ((this._dtExpire != value))
                {
                    this._dtExpire = value;
                }
            }
        }
        [Column(Storage = "_VoucherStatus", DbType = "varchar(15)")]
        public string VoucherStatus
        {
            get
            {
                return this._VoucherStatus;
            }
            set
            {
                if ((this._VoucherStatus != value))
                {
                    this._VoucherStatus = value;
                }
            }
        }

    }
}
