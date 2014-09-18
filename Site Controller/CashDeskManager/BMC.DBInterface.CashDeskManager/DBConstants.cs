using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.DBInterface.CashDeskManager
{
    public class DBConstants
    {
        public const string CONST_PARAM_PERIOD_FROM = "@PeriodFrom";
        public const string CONST_PARAM_PERIOD_TO = "@PeriodTo";


        public const string CONST_PARAM_STARTDATE = "@StartDate";
        public const string CONST_PARAM_ENDDATE = "@EndDate";
        public const string CONST_PARAM_TYPE = "@Type";
        public const string CONST_PARAM_LIABILITY = "@IsLiability";
        public const string CONST_PARAM_BARCODE = "@Barcode";

        public const string CONST_SP_PARAM_SETTINGID = "@Setting_ID";
        public const string CONST_SP_PARAM_SETTINGNAME = "@Setting_Name";
        public const string CONST_SP_PARAM_SETTINGDEFAULT = "@Setting_Default";
        public const string CONST_SP_PARAM_SETTINGVALUE = "@Setting_Value";
        public const string CONST_SP_PARAM_RETURNVALUE = "@RETURN_VALUE";



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

        public const string CONST_HANDPAYCREDIT = "Handpay Credit";
        public const string CONST_PROG = "Prog";
        public const string CONST_JACKPOT = "Handpay Jackpot";
        public const string CONST_REFUND = "Refund";
        public const string CONST_REFILL = "Refill";
        public const string CONST_SHORTPAY = "Shortpay";
        public const string CONST_FLOAT = "Cash Desk Float";
    }
}
