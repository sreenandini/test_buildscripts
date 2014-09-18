using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;

namespace BMC.Transport
{
    public class CashDeskManagerEntity
    {

    }

    public class TicketsClaimed
    {

        public DateTime TicketsClaimedFrom
        { get; set; }
        public DateTime TicketsClaimedTo
        { get; set; }
        public int UserNo
        { get; set; }
    }

    public class Tickets
    {

        public DateTime StartDate
        { get; set; }
        public DateTime EndDate
        { get; set; }

        public string Type
        { get; set; }

        public bool IsLiability
        { get; set; }

        public bool IsClaimedInCashDesk
        { get; set; }

        public string BarCode
        { get; set; }
        public bool IsClaimedInMachine
        { get; set; }

        public bool IsPrintedInMachine
        { get; set; }
        public bool IsPrintedInCashDesk
        { get; set; }

        public int UserNo
        { get; set; }


    }

    public partial class CashierTransactions
    {

        private System.Nullable<decimal> _CDPaidAmount;

        private System.Nullable<decimal> _CDPaidCount;

        private System.Nullable<decimal> _CDPrintedAmount;

        private System.Nullable<decimal> _CDPrintedCount;

        private System.Nullable<decimal> _HandPayAmount;

        private System.Nullable<decimal> _HandPayCount;

        private System.Nullable<decimal> _ShortPayAmount;

        private System.Nullable<decimal> _ShortPayCount;

        private System.Nullable<decimal> _JackpotAmount;

        private System.Nullable<decimal> _JackpotCount;

        private System.Nullable<decimal> _ProgAmount;

        private System.Nullable<decimal> _ProgCount;

        private System.Nullable<decimal> _VoidAmount;

        private System.Nullable<decimal> _VoidCount;

        private System.Nullable<decimal> _MCPaidAmount;

        private System.Nullable<decimal> _MCPaidCount;

        private System.Nullable<decimal> _MCPrintAmount;

        private System.Nullable<decimal> _MCPrintCount;

        private System.Nullable<decimal> _ActiveCashableVoucherAmount;

        private System.Nullable<decimal> _ActiveCashableVoucherCount;

        private System.Nullable<decimal> _VoidTicketsAmount;

        private System.Nullable<decimal> _VoidTicketsCount;

        private System.Nullable<decimal> _VoidVoucherAmount;

        private System.Nullable<decimal> _VoidVoucherCount;

        private System.Nullable<decimal> _CancelledAmount;

        private System.Nullable<decimal> _CancelledCount;

        private System.Nullable<decimal> _ExpiredAmount;

        private System.Nullable<decimal> _ExpiredCount;

        private System.Nullable<decimal> _TicketInExceptionAmount;

        private System.Nullable<decimal> _TicketInExceptionCount;

        private System.Nullable<decimal> _TicketOutExceptionAmount;

        private System.Nullable<decimal> _TicketOutExceptionCount;

        private System.Nullable<decimal> _CashableVoucherLiabilityAmount;

        private System.Nullable<decimal> _CashableVoucherLiabilityCount;

        private System.Nullable<decimal> _PromoCashableAmount;

        private System.Nullable<decimal> _PromoCashableCount;

        private System.Nullable<decimal> _NonCashableINAmount;

        private System.Nullable<decimal> _NonCashableINCount;

        private System.Nullable<decimal> _NonCashableOutAmount;

        private System.Nullable<decimal> _NonCashableOutCount;

        private System.Nullable<decimal> _OfflineVoucherAmount;

        private System.Nullable<decimal> _OfflineVoucherCount;

        private System.Nullable<decimal> _OutstandingHandpaysAmount;

        private System.Nullable<decimal> _OutstandingHandpaysCount;

        public CashierTransactions()
        {
        }

        [Column(Storage = "_CDPaidAmount", DbType = "Decimal(0,0)")]
        public System.Nullable<decimal> CDPaidAmount
        {
            get
            {
                return this._CDPaidAmount;
            }
            set
            {
                if ((this._CDPaidAmount != value))
                {
                    this._CDPaidAmount = value;
                }
            }
        }

        [Column(Storage = "_CDPaidCount", DbType = "Decimal(0,0)")]
        public System.Nullable<decimal> CDPaidCount
        {
            get
            {
                return this._CDPaidCount;
            }
            set
            {
                if ((this._CDPaidCount != value))
                {
                    this._CDPaidCount = value;
                }
            }
        }

        [Column(Storage = "_CDPrintedAmount", DbType = "Decimal(0,0)")]
        public System.Nullable<decimal> CDPrintedAmount
        {
            get
            {
                return this._CDPrintedAmount;
            }
            set
            {
                if ((this._CDPrintedAmount != value))
                {
                    this._CDPrintedAmount = value;
                }
            }
        }

        [Column(Storage = "_CDPrintedCount", DbType = "Decimal(0,0)")]
        public System.Nullable<decimal> CDPrintedCount
        {
            get
            {
                return this._CDPrintedCount;
            }
            set
            {
                if ((this._CDPrintedCount != value))
                {
                    this._CDPrintedCount = value;
                }
            }
        }

        [Column(Storage = "_HandPayAmount", DbType = "Decimal(0,0)")]
        public System.Nullable<decimal> HandPayAmount
        {
            get
            {
                return this._HandPayAmount;
            }
            set
            {
                if ((this._HandPayAmount != value))
                {
                    this._HandPayAmount = value;
                }
            }
        }

        [Column(Storage = "_HandPayCount", DbType = "Decimal(0,0)")]
        public System.Nullable<decimal> HandPayCount
        {
            get
            {
                return this._HandPayCount;
            }
            set
            {
                if ((this._HandPayCount != value))
                {
                    this._HandPayCount = value;
                }
            }
        }

        [Column(Storage = "_ShortPayAmount", DbType = "Decimal(0,0)")]
        public System.Nullable<decimal> ShortPayAmount
        {
            get
            {
                return this._ShortPayAmount;
            }
            set
            {
                if ((this._ShortPayAmount != value))
                {
                    this._ShortPayAmount = value;
                }
            }
        }

        [Column(Storage = "_ShortPayCount", DbType = "Decimal(0,0)")]
        public System.Nullable<decimal> ShortPayCount
        {
            get
            {
                return this._ShortPayCount;
            }
            set
            {
                if ((this._ShortPayCount != value))
                {
                    this._ShortPayCount = value;
                }
            }
        }

        [Column(Storage = "_JackpotAmount", DbType = "Decimal(0,0)")]
        public System.Nullable<decimal> JackpotAmount
        {
            get
            {
                return this._JackpotAmount;
            }
            set
            {
                if ((this._JackpotAmount != value))
                {
                    this._JackpotAmount = value;
                }
            }
        }

        [Column(Storage = "_JackpotCount", DbType = "Decimal(0,0)")]
        public System.Nullable<decimal> JackpotCount
        {
            get
            {
                return this._JackpotCount;
            }
            set
            {
                if ((this._JackpotCount != value))
                {
                    this._JackpotCount = value;
                }
            }
        }

        [Column(Storage = "_ProgAmount", DbType = "Decimal(0,0)")]
        public System.Nullable<decimal> ProgAmount
        {
            get
            {
                return this._ProgAmount;
            }
            set
            {
                if ((this._ProgAmount != value))
                {
                    this._ProgAmount = value;
                }
            }
        }

        [Column(Storage = "_ProgCount", DbType = "Decimal(0,0)")]
        public System.Nullable<decimal> ProgCount
        {
            get
            {
                return this._ProgCount;
            }
            set
            {
                if ((this._ProgCount != value))
                {
                    this._ProgCount = value;
                }
            }
        }

        [Column(Storage = "_VoidAmount", DbType = "Decimal(0,0)")]
        public System.Nullable<decimal> VoidAmount
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

        [Column(Storage = "_VoidCount", DbType = "Decimal(0,0)")]
        public System.Nullable<decimal> VoidCount
        {
            get
            {
                return this._VoidCount;
            }
            set
            {
                if ((this._VoidCount != value))
                {
                    this._VoidCount = value;
                }
            }
        }

        [Column(Storage = "_MCPaidAmount", DbType = "Decimal(0,0)")]
        public System.Nullable<decimal> MCPaidAmount
        {
            get
            {
                return this._MCPaidAmount;
            }
            set
            {
                if ((this._MCPaidAmount != value))
                {
                    this._MCPaidAmount = value;
                }
            }
        }

        [Column(Storage = "_MCPaidCount", DbType = "Decimal(0,0)")]
        public System.Nullable<decimal> MCPaidCount
        {
            get
            {
                return this._MCPaidCount;
            }
            set
            {
                if ((this._MCPaidCount != value))
                {
                    this._MCPaidCount = value;
                }
            }
        }

        [Column(Storage = "_MCPrintAmount", DbType = "Decimal(0,0)")]
        public System.Nullable<decimal> MCPrintAmount
        {
            get
            {
                return this._MCPrintAmount;
            }
            set
            {
                if ((this._MCPrintAmount != value))
                {
                    this._MCPrintAmount = value;
                }
            }
        }

        [Column(Storage = "_MCPrintCount", DbType = "Decimal(0,0)")]
        public System.Nullable<decimal> MCPrintCount
        {
            get
            {
                return this._MCPrintCount;
            }
            set
            {
                if ((this._MCPrintCount != value))
                {
                    this._MCPrintCount = value;
                }
            }
        }

        [Column(Storage = "_ActiveCashableVoucherAmount", DbType = "Decimal(0,0)")]
        public System.Nullable<decimal> ActiveCashableVoucherAmount
        {
            get
            {
                return this._ActiveCashableVoucherAmount;
            }
            set
            {
                if ((this._ActiveCashableVoucherAmount != value))
                {
                    this._ActiveCashableVoucherAmount = value;
                }
            }
        }

        [Column(Storage = "_ActiveCashableVoucherCount", DbType = "Decimal(0,0)")]
        public System.Nullable<decimal> ActiveCashableVoucherCount
        {
            get
            {
                return this._ActiveCashableVoucherCount;
            }
            set
            {
                if ((this._ActiveCashableVoucherCount != value))
                {
                    this._ActiveCashableVoucherCount = value;
                }
            }
        }

        [Column(Storage = "_VoidTicketsAmount", DbType = "Decimal(0,0)")]
        public System.Nullable<decimal> VoidTicketsAmount
        {
            get
            {
                return this._VoidTicketsAmount;
            }
            set
            {
                if ((this._VoidTicketsAmount != value))
                {
                    this._VoidTicketsAmount = value;
                }
            }
        }

        [Column(Storage = "_VoidTicketsCount", DbType = "Decimal(0,0)")]
        public System.Nullable<decimal> VoidTicketsCount
        {
            get
            {
                return this._VoidTicketsCount;
            }
            set
            {
                if ((this._VoidTicketsCount != value))
                {
                    this._VoidTicketsCount = value;
                }
            }
        }

        [Column(Storage = "_VoidVoucherAmount", DbType = "Decimal(0,0)")]
        public System.Nullable<decimal> VoidVoucherAmount
        {
            get
            {
                return this._VoidVoucherAmount;
            }
            set
            {
                if ((this._VoidVoucherAmount != value))
                {
                    this._VoidVoucherAmount = value;
                }
            }
        }

        [Column(Storage = "_VoidVoucherCount", DbType = "Decimal(0,0)")]
        public System.Nullable<decimal> VoidVoucherCount
        {
            get
            {
                return this._VoidVoucherCount;
            }
            set
            {
                if ((this._VoidVoucherCount != value))
                {
                    this._VoidVoucherCount = value;
                }
            }
        }


        [Column(Storage = "_CancelledAmount", DbType = "Decimal(0,0)")]
        public System.Nullable<decimal> CancelledAmount
        {
            get
            {
                return this._CancelledAmount;
            }
            set
            {
                if ((this._CancelledAmount != value))
                {
                    this._CancelledAmount = value;
                }
            }
        }

        [Column(Storage = "_CancelledCount", DbType = "Decimal(0,0)")]
        public System.Nullable<decimal> CancelledCount
        {
            get
            {
                return this._CancelledCount;
            }
            set
            {
                if ((this._CancelledCount != value))
                {
                    this._CancelledCount = value;
                }
            }
        }

        [Column(Storage = "_ExpiredAmount", DbType = "Decimal(0,0)")]
        public System.Nullable<decimal> ExpiredAmount
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

        [Column(Storage = "_ExpiredCount", DbType = "Decimal(0,0)")]
        public System.Nullable<decimal> ExpiredCount
        {
            get
            {
                return this._ExpiredCount;
            }
            set
            {
                if ((this._ExpiredCount != value))
                {
                    this._ExpiredCount = value;
                }
            }
        }

        [Column(Storage = "_TicketInExceptionAmount", DbType = "Decimal(0,0)")]
        public System.Nullable<decimal> TicketInExceptionAmount
        {
            get
            {
                return this._TicketInExceptionAmount;
            }
            set
            {
                if ((this._TicketInExceptionAmount != value))
                {
                    this._TicketInExceptionAmount = value;
                }
            }
        }

        [Column(Storage = "_TicketInExceptionCount", DbType = "Decimal(0,0)")]
        public System.Nullable<decimal> TicketInExceptionCount
        {
            get
            {
                return this._TicketInExceptionCount;
            }
            set
            {
                if ((this._TicketInExceptionCount != value))
                {
                    this._TicketInExceptionCount = value;
                }
            }
        }

        [Column(Storage = "_TicketOutExceptionAmount", DbType = "Decimal(0,0)")]
        public System.Nullable<decimal> TicketOutExceptionAmount
        {
            get
            {
                return this._TicketOutExceptionAmount;
            }
            set
            {
                if ((this._TicketOutExceptionAmount != value))
                {
                    this._TicketOutExceptionAmount = value;
                }
            }
        }

        [Column(Storage = "_TicketOutExceptionCount", DbType = "Decimal(0,0)")]
        public System.Nullable<decimal> TicketOutExceptionCount
        {
            get
            {
                return this._TicketOutExceptionCount;
            }
            set
            {
                if ((this._TicketOutExceptionCount != value))
                {
                    this._TicketOutExceptionCount = value;
                }
            }
        }

        [Column(Storage = "_CashableVoucherLiabilityAmount", DbType = "Decimal(0,0)")]
        public System.Nullable<decimal> CashableVoucherLiabilityAmount
        {
            get
            {
                return this._CashableVoucherLiabilityAmount;
            }
            set
            {
                if ((this._CashableVoucherLiabilityAmount != value))
                {
                    this._CashableVoucherLiabilityAmount = value;
                }
            }
        }

        [Column(Storage = "_CashableVoucherLiabilityCount", DbType = "Decimal(0,0)")]
        public System.Nullable<decimal> CashableVoucherLiabilityCount
        {
            get
            {
                return this._CashableVoucherLiabilityCount;
            }
            set
            {
                if ((this._CashableVoucherLiabilityCount != value))
                {
                    this._CashableVoucherLiabilityCount = value;
                }
            }
        }

        [Column(Storage = "_PromoCashableAmount", DbType = "Decimal(0,0)")]
        public System.Nullable<decimal> PromoCashableAmount
        {
            get
            {
                return this._PromoCashableAmount;
            }
            set
            {
                if ((this._PromoCashableAmount != value))
                {
                    this._PromoCashableAmount = value;
                }
            }
        }

        [Column(Storage = "_PromoCashableCount", DbType = "Decimal(0,0)")]
        public System.Nullable<decimal> PromoCashableCount
        {
            get
            {
                return this._PromoCashableCount;
            }
            set
            {
                if ((this._PromoCashableCount != value))
                {
                    this._PromoCashableCount = value;
                }
            }
        }

        [Column(Storage = "_NonCashableINAmount", DbType = "Decimal(0,0)")]
        public System.Nullable<decimal> NonCashableINAmount
        {
            get
            {
                return this._NonCashableINAmount;
            }
            set
            {
                if ((this._NonCashableINAmount != value))
                {
                    this._NonCashableINAmount = value;
                }
            }
        }

        [Column(Storage = "_NonCashableINCount", DbType = "Decimal(0,0)")]
        public System.Nullable<decimal> NonCashableINCount
        {
            get
            {
                return this._NonCashableINCount;
            }
            set
            {
                if ((this._NonCashableINCount != value))
                {
                    this._NonCashableINCount = value;
                }
            }
        }

        [Column(Storage = "_NonCashableOutAmount", DbType = "Decimal(0,0)")]
        public System.Nullable<decimal> NonCashableOutAmount
        {
            get
            {
                return this._NonCashableOutAmount;
            }
            set
            {
                if ((this._NonCashableOutAmount != value))
                {
                    this._NonCashableOutAmount = value;
                }
            }
        }

        [Column(Storage = "_NonCashableOutCount", DbType = "Decimal(0,0)")]
        public System.Nullable<decimal> NonCashableOutCount
        {
            get
            {
                return this._NonCashableOutCount;
            }
            set
            {
                if ((this._NonCashableOutCount != value))
                {
                    this._NonCashableOutCount = value;
                }
            }
        }


        [Column(Storage = "_OfflineVoucherAmount", DbType = "Decimal(0,0)")]
        public System.Nullable<decimal> OfflineVoucherAmount
        {
            get
            {
                return this._OfflineVoucherAmount;
            }
            set
            {
                if ((this._OfflineVoucherAmount != value))
                {
                    this._OfflineVoucherAmount = value;
                }
            }
        }

        [Column(Storage = "_OfflineVoucherCount", DbType = "Decimal(0,0)")]
        public System.Nullable<decimal> OfflineVoucherCount
        {
            get
            {
                return this._OfflineVoucherCount;
            }
            set
            {
                if ((this._OfflineVoucherCount != value))
                {
                    this._OfflineVoucherCount = value;
                }
            }
        }

        [Column(Storage = "_OutstandingHandpaysAmount", DbType = "Decimal(0,0)")]
        public System.Nullable<decimal> OutstandingHandpaysAmount
        {
            get
            {
                return this._OutstandingHandpaysAmount;
            }
            set
            {
                if ((this._OutstandingHandpaysAmount != value))
                {
                    this._OutstandingHandpaysAmount = value;
                }
            }
        }
        [Column(Storage = "_OutstandingHandpaysCount", DbType = "Decimal(0,0)")]
        public System.Nullable<decimal> OutstandingHandpaysCount
        {
            get
            {
                return this._OutstandingHandpaysCount;
            }
            set
            {
                if ((this._OutstandingHandpaysCount != value))
                {
                    this._OutstandingHandpaysCount = value;
                }
            }
        }


    }

    public partial class CashierTransactionsDetails
    {

        private string _Trans_Type;

        private string _PrintAsset;

        private int _PrintSiteCode;

        private string _PrintPosition;

        private System.Nullable<System.DateTime> _PrintedDate;

        private string _PaidAsset;

        private string _PaidPosition;

        private System.Nullable<System.DateTime> _PaidDate;

        private System.Nullable<double> _Amount;

        private string _Ticket;

        public CashierTransactionsDetails()
        {
        }

        [Column(Storage = "_Trans_Type", DbType = "VarChar(50)")]
        public string Trans_Type
        {
            get
            {
                return this._Trans_Type;
            }
            set
            {
                if ((this._Trans_Type != value))
                {
                    this._Trans_Type = value;
                }
            }
        }

        [Column(Storage = "_PrintAsset", DbType = "VarChar(50)")]
        public string PrintAsset
        {
            get
            {
                return this._PrintAsset;
            }
            set
            {
                if ((this._PrintAsset != value))
                {
                    this._PrintAsset = value;
                }
            }
        }


        [Column(Storage = "_PrintSiteCode", DbType = "Int")]
        public int PrintSiteCode
        {
            get
            {
                return this._PrintSiteCode;
            }
            set
            {
                if ((this._PrintSiteCode != value))
                {
                    this._PrintSiteCode = value;
                }
            }
        }
        [Column(Storage = "_PrintPosition", DbType = "VarChar(50)")]
        public string PrintPosition
        {
            get
            {
                return this._PrintPosition;
            }
            set
            {
                if ((this._PrintPosition != value))
                {
                    this._PrintPosition = value;
                }
            }
        }

        [Column(Storage = "_PrintedDate", DbType = "DateTime")]
        public System.Nullable<System.DateTime> PrintedDate
        {
            get
            {
                return this._PrintedDate;
            }
            set
            {
                if ((this._PrintedDate != value))
                {
                    this._PrintedDate = value;
                }
            }
        }

        [Column(Storage = "_PaidAsset", DbType = "VarChar(50)")]
        public string PaidAsset
        {
            get
            {
                return this._PaidAsset;
            }
            set
            {
                if ((this._PaidAsset != value))
                {
                    this._PaidAsset = value;
                }
            }
        }

        [Column(Storage = "_PaidPosition", DbType = "VarChar(50)")]
        public string PaidPosition
        {
            get
            {
                return this._PaidPosition;
            }
            set
            {
                if ((this._PaidPosition != value))
                {
                    this._PaidPosition = value;
                }
            }
        }

        [Column(Storage = "_PaidDate", DbType = "DateTime")]
        public System.Nullable<System.DateTime> PaidDate
        {
            get
            {
                return this._PaidDate;
            }
            set
            {
                if ((this._PaidDate != value))
                {
                    this._PaidDate = value;
                }
            }
        }

        [Column(Storage = "_Amount", DbType = "Float")]
        public System.Nullable<double> Amount
        {
            get
            {
                return this._Amount;
            }
            set
            {
                if ((this._Amount != value))
                {
                    this._Amount = value;
                }
            }
        }

        [Column(Storage = "_Ticket", DbType = "VarChar(32)")]
        public string Ticket
        {
            get
            {
                return this._Ticket;
            }
            set
            {
                if ((this._Ticket != value))
                {
                    this._Ticket = value;
                }
            }
        }
    }

    public partial class rsp_CDM_GetCashierTransactionsDetails_New
    {

        private string _Trans_Type;

        private string _PrintAsset;

        private string _PrintSiteCode;

        private string _PrintPosition;

        private System.Nullable<System.DateTime> _PrintedDate;

        private string _PaidAsset;

        private string _PaidPosition;

        private System.Nullable<System.DateTime> _PaidDate;

        private System.Nullable<decimal> _Amount;

        private string _Ticket;

        private System.Nullable<int> _Userid;

        private string _ROUTEMember;

        private string _Summary_Type;

        public rsp_CDM_GetCashierTransactionsDetails_New()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Trans_Type", DbType = "VarChar(200)")]
        public string Trans_Type
        {
            get
            {
                return this._Trans_Type;
            }
            set
            {
                if ((this._Trans_Type != value))
                {
                    this._Trans_Type = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_PrintAsset", DbType = "VarChar(100)")]
        public string PrintAsset
        {
            get
            {
                return this._PrintAsset;
            }
            set
            {
                if ((this._PrintAsset != value))
                {
                    this._PrintAsset = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_PrintSiteCode", DbType = "VarChar(10)")]
        public string PrintSiteCode
        {
            get
            {
                return this._PrintSiteCode;
            }
            set
            {
                if ((this._PrintSiteCode != value))
                {
                    this._PrintSiteCode = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_PrintPosition", DbType = "VarChar(100)")]
        public string PrintPosition
        {
            get
            {
                return this._PrintPosition;
            }
            set
            {
                if ((this._PrintPosition != value))
                {
                    this._PrintPosition = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_PrintedDate", DbType = "DateTime")]
        public System.Nullable<System.DateTime> PrintedDate
        {
            get
            {
                return this._PrintedDate;
            }
            set
            {
                if ((this._PrintedDate != value))
                {
                    this._PrintedDate = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_PaidAsset", DbType = "VarChar(100)")]
        public string PaidAsset
        {
            get
            {
                return this._PaidAsset;
            }
            set
            {
                if ((this._PaidAsset != value))
                {
                    this._PaidAsset = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_PaidPosition", DbType = "VarChar(10)")]
        public string PaidPosition
        {
            get
            {
                return this._PaidPosition;
            }
            set
            {
                if ((this._PaidPosition != value))
                {
                    this._PaidPosition = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_PaidDate", DbType = "DateTime")]
        public System.Nullable<System.DateTime> PaidDate
        {
            get
            {
                return this._PaidDate;
            }
            set
            {
                if ((this._PaidDate != value))
                {
                    this._PaidDate = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Amount", DbType = "Decimal(18,2)")]
        public System.Nullable<decimal> Amount
        {
            get
            {
                return this._Amount;
            }
            set
            {
                if ((this._Amount != value))
                {
                    this._Amount = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Ticket", DbType = "VarChar(100)")]
        public string Ticket
        {
            get
            {
                return this._Ticket;
            }
            set
            {
                if ((this._Ticket != value))
                {
                    this._Ticket = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Userid", DbType = "Int")]
        public System.Nullable<int> Userid
        {
            get
            {
                return this._Userid;
            }
            set
            {
                if ((this._Userid != value))
                {
                    this._Userid = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ROUTEMember", DbType = "VarChar(100)")]
        public string ROUTEMember
        {
            get
            {
                return this._ROUTEMember;
            }
            set
            {
                if ((this._ROUTEMember != value))
                {
                    this._ROUTEMember = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Summary_Type", DbType = "VarChar(100)")]
        public string Summary_Type
        {
            get
            {
                return this._Summary_Type;
            }
            set
            {
                if ((this._Summary_Type != value))
                {
                    this._Summary_Type = value;
                }
            }
        }
    }

    public partial class rsp_CDM_GetCashierTransactionsSummary
    {

        private string _Summary_Type;

        private decimal _Amount;

        private int _Count_Summary;

        public rsp_CDM_GetCashierTransactionsSummary()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Summary_Type", DbType = "VarChar(100)")]
        public string Summary_Type
        {
            get
            {
                return this._Summary_Type;
            }
            set
            {
                if ((this._Summary_Type != value))
                {
                    this._Summary_Type = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Amount", DbType = "Decimal(38,2) NOT NULL")]
        public decimal Amount
        {
            get
            {
                return this._Amount;
            }
            set
            {
                if ((this._Amount != value))
                {
                    this._Amount = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Count_Summary", DbType = "Int NOT NULL")]
        public int Count_Summary
        {
            get
            {
                return this._Count_Summary;
            }
            set
            {
                if ((this._Count_Summary != value))
                {
                    this._Count_Summary = value;
                }
            }
        }
    }

    public class CashierHistory
    {
        public List<rsp_CDM_GetCashierTransactionsSummary> Summary;
    }
}
