using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.Reports
{
    public class clsSPParams
    {
        public DateTime To { get; set; }
        public int Company { get; set; }
        public string CompanyName { get; set; }
        public int SubCompany { get; set; }
        public string SubCompanyName { get; set; }
        public int Region { get; set; }
        public string RegionName { get; set; }
        public int Area { get; set; }
        public string AreaName { get; set; }
        public int District { get; set; }
        public string DistrictName { get; set; }
        public int Site { get; set; }
        public string SiteName { get; set; }
        public int Category { get; set; }
        public string CategoryName { get; set; }
        public int SiteID { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string StartDateString { get; set; }
        public string EndDateString { get; set; }
        public string CurrentDate { get; set; }
        public int Supplier { get; set; }
        public string SupplierName { get; set; }
        public int Depot { get; set; }
        public string DepotName { get; set; }
        public int iStatement_No { get; set; }
        public bool HideZeroVarianceCollections { get; set; }
        public string DateFormat { get; set; }
        public string Batch_No { get; set; }
        public string RepresentativeName { get; set; }
        public int Representative { get; set; }
        public string ProductName { get; set; }
        public int Product { get; set; }
        public string OrderByName { get; set; }
        public int OrderBy { get; set; }
        public string MachineName { get; set; }
        public int Machine { get; set; }
        public string chkAuditMoments { get; set; }
        public string PositionOrder { get; set; }
        public string IncludeCharts { get; set; }
        public string ShowAccessories { get; set; }
        public string IncludeSiteSummary { get; set; }
        public string IncludeRepSummary { get; set; }
        public string TotalOnly { get; set; }
        public string MTypeID { get; set; }
        public string Status { get; set; }
        public string EndDateLong { get; set; }
        public string EndDateShort { get; set; }
        public string CurrentTime { get; set; }
        public int Zone { get; set; }
        public string ZoneName { get; set; }
        public string GamingDate { get; set; }
        public string ReportHeader { get; set; }
        public string ProductVersion { get; set; }
        public string Criteria { get; set; }
        public string Language { get; set; }
        public int runtype { get; set; }
        public float Denom { get; set; }
        public string Period { get; set; }
        public bool ExcludeZero { get; set; }
        public bool OnlyeFundEnabled { get; set; }
        public string DeviceType { get; set; }
        public string DeviceTypeName { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime dtStartDate { get; set; }
        public DateTime dtEndDate { get; set; }
        public string VoucherStatus { get; set; }
        public string VoucherStatusName { get; set; }
        public string Slot { get; set; }
        public string SlotName { get; set; }
        public string ReportPeriod { get; set; }
        public string ReportPeriodName { get; set; }
        public DateTime ReportDate { get; set; }
        public string GroupBY { get; set; }
        public string ModuleID { get; set; }
        public int Rows { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string ModuleName { get; set; }
        public string ReportName { get; set; }
        public bool Local { get; set; }
        public bool UsePhysicalWin { get; set; }
        public bool IncludeNonCashable { get; set; }
        public bool ShowHandpay { get; set; }
        public bool GroupByZone { get; set; }
        public bool ShowJackpot { get; set; }
        public int StackerLevel { get; set; }
        public string EmpID { get; set; }
        public string EmpCardID { get; set; }
        public string CardNumber { get; set; }
        public string EmployeeName { get; set; }
        public string CardStatus { get; set; }
        public string CardType { get; set; }
        public string RuleName { get; set; }
        public string CurrencySymbol { get; set; }
        public string CurrencyCulture { get; set; }
        public int StockStatus { get; set; }
        public string StockStatusName { get; set; }
        public bool BMC { get; set; }
        public bool Promotional { get; set; }
        public bool Both { get; set; }
        public string VaultStatus { get; set; }
        public string TransactionType { get; set; }
        public int InventoryLevel { get; set; }
        public string InventoryType { get; set; }
        public bool IncludeZero { get; set; }
        public int UserId { get; set; }
        public string SiteIDList { get; set; }
        public bool IsGamingDayBasedReport { get; set; }
        public string Sites { get; set; } 
        //public string ReportType { get; set; }
        public string ReportFolder { get; set; }
        public string ReportURL { get; set; }
        public string ProcedureName { get; set; }
        public int? BatchId { get; set; }
        public int? ReadId { get; set; }
        public int WeekId { get; set; }
        public bool IsWeek { get; set;}
        public int BarPositionId { get; set;}
        public bool IsDetailed { get; set; }
        public string Bar_Position_ID { get; set; }
        public string BarPositionName { get; set; }
        public int VaultId { get; set;}
        public int Variance { get; set;}
        public int RouteNo { get; set; }
        public int FilterBy { get; set; }
        public string FilterValue { get; set; }
        public int BatchNo { get; set; }
        public string UserName { get; set; }
        public int MachineCount { get; set; }
        public string DropType { get; set; }
        public int UserNo { get; set; }
        public int ProfitShareGroupId { get; set; }
        public int ExpenseShareGroupID { get; set; }
        public decimal ExpenseShareAmount { get; set; }
        public decimal WriteOffAmount { get; set; }
        public int PayPeriodId { get; set; }
        public string SiteCode { get; set; }
        public int Vault_Id { get; set; }
        public int Site_ID { get; set; }
        public int VarianceType { get; set; }
        public string VarianceName { get; set; } 
		public int BarPosition_Id { get; set; }
        public int Bar_Position_Id { get; set; }
        public string BatchUser { get; set; }
        public string BatchCollectionDate { get; set; }
        public string BatchGamingDay { get; set; }
        public string startdate { get; set; }
        public string enddate { get; set; }
        public int SITE { get; set; }
		public int User_No { get; set; }

        public string WeekNo { get; set; }
        public string Week { get; set; }

        public string RouteName { get; set; }
        public DateTime AuditStartDate { get; set; }
        public DateTime AuditEndDate { get; set; }
        public string ListStatus { get; set; }

        public string ReportFilterDateFormat { get; set; }
        public string ReportPrintDateTimeFormat { get; set; }
        public string ReportDataDateAloneFormat { get; set; }
        public string ReportDataDateNTimeFormat { get; set; }
    }
}
