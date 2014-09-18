using System;

namespace BMC.Transport.CashDeskOperatorEntity
{
    public class Treasury
    {
        public int TreasuryIssuerUserNo { get; set; }
        public int TreasuryReasonCode { get; set; }
        public string TreasuryMembershipNo { get; set; }
        public int TreasuryBreakdown2p { get; set; }
        public int TreasuryBreakdown5p { get; set; }
        public int TreasuryBreakdown10p { get; set; }
        public int TreasuryBreakdown20p { get; set; }
        public int TreasuryBreakdown50p { get; set; }
        public int TreasuryBreakdown100p { get; set; }
        public int TreasuryBreakdown200p { get; set; }
        public int TreasuryAllocated { get; set; }
        public double TreasuryAmount { get; set; }
        public string TreasuryReason { get; set; }
        public string TreasuryType { get; set; }
        public int UserID { get; set; }
        public int InstallationNumber { get; set; }
        public int CollectionNumber { get; set; }
        public bool TreasuryTemp { get; set; }
        public int TreasuryFloatIssuedBy { get; set; }
        public DateTime ActualTreasuryDate { get; set; }
        public long CustomerID { get; set; }
        public long AuthorizedUser_No { get; set; }
        public DateTime Authorized_Date { get; set; }

        public string Asset { get; set; }
        public string BarPosName { get; set; }
    }

    public class ReasonCode
    {
        public int Reason_Code { get; set; }
        public string ReasonDescription { get; set; }

    }
}
