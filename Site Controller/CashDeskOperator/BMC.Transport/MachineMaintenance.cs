using System;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace BMC.Transport.CashDeskOperatorEntity
{
    public class CMaintenanceSession
    {
        public int ID { get; set; }
        public int Installation_No { get; set; }
        public bool isAuthorized { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ClosedBy { get; set; }
        public DateTime ClosedOn { get; set; }
        public int CategoryID { get; set; }
        public int Reason { get; set; }
        public string Comment { get; set; }
        public bool IsSessionOpen { get; set; }
        public Collection<CMaintenanceHistory> oColMaintenanceHistory;
    }

    public class CMaintenanceHistory
    {
        public int ID { get; set; }
        public int SessionID { get; set; }
        public int EventID { get; set; }
        public DateTime TimeStamp { get; set; }
        public int CoinIn { get; set; }
        public int CoinOut { get; set; }
        public int Bill100 { get; set; }
        public int Bill50 { get; set; }
        public int Bill20 { get; set; }
        public int Bill10 { get; set; }
        public int Bill5 { get; set; }
        public int Bill1 { get; set; }
        public int TrueCoinIn { get; set; }
        public int TrueCoinOut { get; set; }
        public int Drop { get; set; }
        public int Jackpot { get; set; }
        public int CancelledCredits { get; set; }
        public int HandPaidCancelledCredits { get; set; }
        public int CashableTicketIn { get; set; }
        public int CashableTicketOut { get; set; }
        public int CashableTicketInQty { get; set; }
        public int CashableTicketOutQty { get; set; }
        public int ProgressiveHandPay { get; set; }
    }
}
