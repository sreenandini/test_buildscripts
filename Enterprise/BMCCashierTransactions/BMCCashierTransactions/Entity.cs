using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMCCashierTransactions
{
    public class DBConstants
    {
        #region CashDeskManager
        public const string CONST_PARAM_PERIOD_FROM = "@PeriodFrom";
        public const string CONST_PARAM_PERIOD_TO = "@PeriodTo";
        public const string CONST_USER_ID = "@LoginUserId";


       
        public const string CONST_PARAM_LIABILITY = "@IsLiability";
       
        //Stored procedures
        public const string CONST_SP_GET_TicketExceptions = "rsp_GetTicketExceptions";
        public const string CONST_SP_GET_TICKETS_CLAIMED = "rsp_GetTicketsClaimed";
        public const string CONST_SP_RSP_GETTREASURYITEMS = "rsp_GetTreasuryItems";
        public const string CONST_SP_GET_TICKETS_UNCLAIMED = "rsp_GetTicketsUnclaimed";
        public const string CONST_SP_RSP_TICKET_ANOMALIES = "rsp_Ticket_Anomalies";
        public const string CONST_SP_GET_TICKETS_PRINTED = "rsp_GetTicketsPrinted";
        public const string CONST_SP_RSP_GETPROMOTICKETFORPERIODDETAILS = "rsp_GetPromoTicketForPeriodDetails";
        public const string CONST_SP_GETSETTING = "rsp_GetSetting";

        //Treasury Constants

        public const string CONST_HANDPAYCREDIT = "Attendantpay Credit";
        public const string CONST_PROG = "Prog";
        public const string CONST_JACKPOT = "Attendantpay Jackpot";
        public const string CONST_REFUND = "Refund";
        public const string CONST_REFILL = "Refill";
        public const string CONST_SHORTPAY = "Shortpay";
        public const string CONST_FLOAT = "Cash Desk Float";

        //Parameter for rsp_getSetting
        public const string CONST_SP_PARAM_SETTINGID = "@Setting_ID";
        public const string CONST_SP_PARAM_SETTINGNAME = "@Setting_Name";
        public const string CONST_SP_PARAM_SETTINGDEFAULT = "@Setting_Default";
        public const string CONST_SP_PARAM_SETTINGVALUE = "@Setting_Value";

        //Parameters for rsp_BGS_VoucherInformation
        public const string CONST_SP_PARAM_STARTDATE = "@StartDate";
        public const string CONST_SP_PARAM_ENDDATE = "@EndDate";
        public const string CONST_SP_PARAM_TYPE = "@Type";
        public const string CONST_PARAM_STARTDATE = "@StartDate";
        public const string CONST_PARAM_ENDDATE = "@EndDate";

        public const string CONST_PARAM_TYPE = "@Type";
        public const string CONST_PARAM_BARCODE = "@Barcode";
        public const string CONST_PARAM_SITE = "@SITE";

        public const string Company = "COMPANY";
        public const string Subcompany = "SUBCOMPANY";
        public const string Site = "SITE";
        public const string ValueAny = "--Any--"; 
        public const string ValueNone = "--None--";

        public const string cmbCompText = "Company_Name";
        public const string cmbCompValue = "Company_ID";

        public const string cmbSubCmpValue = "Sub_Company_ID";
        public const string cmbSubCmpText = "Sub_Company_Name";

        public const string cmbSiteValue = "Site_ID";        
        public const string cmbSiteText = "Site_Name";


        #endregion

    }
    
    
    public class TicketsClaimed
    {

        public DateTime TicketsClaimedFrom
        { get; set; }
        public DateTime TicketsClaimedTo
        { get; set; }
        public Int32 SITE
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

        public Int32 SITE
        { get; set; }


    }

    public class TicketExceptions
    {

        public string SEGM
        { get; set; }

        public string Machine
        { get; set; }

        public double CurrentCashDesk
        { get; set; }
        public int CashDeskClaimedQty
        { get; set; }
        public int CashDeskPrintedQty
        { get; set; }

        public string Reference
        { get; set; }

        public string Details
        { get; set; }
        public double currEGM
        { get; set; }

        public int MachineClaimedQty
        { get; set; }

        public int MachinePrintedQty
        { get; set; }

        public float currValue
        { get; set; }
        public string Type
        { get; set; }

        public string Position
        { get; set; }

        public string Reason
        { get; set; }

        public string ReasonCode
        { get; set; }
        public string TransactionType
        { get; set; }

        public string Zone
        { get; set; }
        public string PrintDate
        { get; set; }
        public string PayDate
        { get; set; }

        public string ClaimedTerminal
        { get; set; }

        public string Ticket
        { get; set; }

        public double Value
        { get; set; }

        public string Asset
        { get; set; }

        public string PayDevice
        { get; set; }

        public int COLINSTALLID
        { get; set; }

        public string CreateCompleted
        { get; set; }
        public string Status
        { get; set; }
        public float cTicketTotal
        { get; set; }
        public float cExceptionsTotal
        { get; set; }
        public bool bExceptionRecordFound
        { get; set; }

        public string VoucherStatus
        { get; set; }
        public bool TicketAddedtoList
        { get; set; }

        public string Amount
        { get; set; }

        public string TreasuryDate { get; set; }
        public string TreasuryDateTime { get; set; }
        public string TreasuryTime { get; set; }
        public string TreasuryType { get; set; }
        public double TreasuryAmount { get; set; }
        public string ZoneName { get; set; }
        public string MachineName { get; set; }
        public string PositionName { get; set; }
        public int HandpayQty { get; set; }
        public int JackPotQty { get; set; }
        public int ProgQty { get; set; }
        public int RefundQty { get; set; }
        public int RefillQty { get; set; }
        public int ShortQty { get; set; }
        public int FloatQty { get; set; }
        public bool TreasuryTemp { get; set; }
    }

    public enum ReportOptions
    {
        Export,
        Print
    }
}
