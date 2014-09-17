using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ReportViewer
{
    public static class Constants
    {
        public const string MainHeader = "ENTERPRISE REPORTS";
        public const string MessageBoxHeader = "Bally MultiConnect - Reports";
        public const string ButtonOpenText = "OPEN REPORT";
        public const string ButtonOpenName = "btnOpen";
        public const string ButtonExportText = "EXPORT EXCEL";
        public const string ButtonExportName = "btnExportExcel";
        public const string Depreciation = "DEPRECIATIONREPORT";
        public const string ExpiredCalendars = "EXPIREDCALENDARS";
        public const string MajorPrizes = "MAJORPRIZESREPORT";

        public const string Liquidation = "LIQUIDATION";
        public const string SiteAddress = "SITEADDRESSREPORT";
        //public const string ValueNone = "--None--";
        //public const string ValueAny = "--All--";
        public const string HideZeroVarianceCollectionslbl = "HIDE ZERO VARIANCE COLLECTIONS";

        //(EP4 Changes)
        public const string VaultStatus = "VAULTSTATUS";
        public const string IncludeZero = "INCLUDEZERO";
        public const string UserID = "USERID";
        public const string SiteIDList = "SITEIDLIST";
        public const string TransactionType = "TRANSACTIONTYPE";
        public const string InventoryLevel = "INVENTORYLEVEL";
        public const string InventoryType = "INVENTORYTYPE";
        //(EP4 Changes Ends)

        public const string EmpID = "USERID"; //SecurityUserID
        public const string EmpCardID = "EMPCARDID"; //EmpCardID from tblEmployeeCardSessions
        public const string CardNumber = "CARDNUMBER"; //EmployeeCardNumber in tblEmployeeCardDetails
        public const string EmployeeName = "EMPLOYEENAME"; //Staff_First_Name and Staff_Last_Name from Staff for whoem cards are assigned.
        public const string CardStatus = "CARDSTATUS"; //'Active' or 'Inactive'
        public const string CardType = "CARDTYPE"; //EmpCardType from tblEmployeeCardType      
        public const string RuleName = "RULENAME";
        public const string StockStatus = "STOCKSTATUS";
        
  
        public const string StackerLevel = "STACKERLEVEL";
        public const string Company = "COMPANY";
        public const string Subcompany = "SUBCOMPANY";
        public const string Region = "REGION";
        public const string Area = "AREA";
        public const string District = "DISTRICT";
        public const string Site = "SITE";
        public const string SiteID = "SITEID";
        public const string Category = "CATEGORY";
        public const string Supplier = "SUPPLIER";
        public const string Depot = "DEPOT";
        public const string OrderBy = "ORDERBY";
        public const string StartDate = "STARTDATE";
        public const string GamingDate = "GAMINGDATE";
        public const string EndDate = "ENDDATE";
        public const string TO = "TO";
        public const string HideZeroVarianceCollections = "HIDEZEROVARIANCECOLLECTIONS";
        public const string Zone = "ZONE";
        public const string Period = "PERIOD";
        public const string StatementNo = "STATEMENTNO";
        public const string PeriodEndID = "PERIOD_END_ID";
        public const string PeriodEndDetails = "Period_End_Description";
        public const string WeeklyLiquid = "ISTATEMENT_NO";
        public const string Liquid = "BATCH_NO";
        public const string BatchID = "BATCH_ID";
        public const string BatchRef = "BATCH_REF";

        public const string cmbCompText = "Company_Name";
        public const string cmbCompValue = "Company_ID";
        public const string cmbSubCmpValue = "Sub_Company_ID";
        public const string cmbSubCmpText = "Sub_Company_Name";
        public const string cmbRegionText = "Sub_Company_Region_Name";
        public const string cmbRegionValue = "Sub_Company_Region_ID";
        public const string cmbAreaText = "Sub_Company_Area_Name";
        public const string cmbAreaValue = "Sub_Company_Area_ID";
        public const string cmbDistrictText = "Sub_Company_District_Name";
        public const string cmbDistrictValue = "Sub_Company_District_ID";
        public const string cmbDepotText = "Depot_Name";
        public const string cmbDepotValue = "Depot_ID";
        public const string cmbCategoryText = "Machine_type_code";
        public const string cmbCategoryValue = "Machine_type_id";
        public const string cmbSupplierText = "Operator_Name";
        public const string cmbSupplierValue = "Operator_ID";
        public const string cmbOrderByText = "OrderBy_Name";
        public const string cmbOrderByValue = "OrderBy_ID";
        public const string cmbSiteValue = "Site_ID";
        public const string cmbSite1Value = "Site_Code";
        public const string cmbSiteText = "Site_Name";
        public const string cmbZoneText = "Zone_Name";
        public const string cmbZoneValue = "Zone_ID";

        public const string cmbEmpIdText = "USERNAME";
        public const string cmbEmpIdValue = "SECURITYUSERID";   
        public const string cmbEmpCardIdText = "EmpCardID";
        public const string cmbEmpCardIdValue = "EmpCardID";

        public const string cmbCardNumberText = "EmployeeCardNumber";
        public const string cmbCardNumberValue = "EmployeeCardNumber";
        public const string cmbEmployeeNameText = "EmployeeName";
        public const string cmbEmployeeNameValue = "EmployeeName";    
        public const string cmbCardTypeText = "EmpCardType";
        public const string cmbCardTypeValue = "EmpCardType";
        public const string cmbRuleNameText = "RuleName";
        public const string cmbRuleNameValue = "RuleName";
        

        public const string SP_READEXCEPTION = "rsp_CheckMissingReadData";
        public const string ExceptionReportName = "EXCEPTIONDETAILS";

        //Reports
        public const string DeviceType = "DEVICETYPE";
        public const string IssueDate = "ISSUEDATE";
        public const string VoucherStatus = "VOUCHERSTATUS";
        public const string Slot = "SLOT";
        public const string ReportDate = "REPORTDATE";
        public const string ReportPeriod = "REPORTPERIOD";
        public const string GroupBy = "GROUPBY";

        //Crystal Reports
        public const string AuditTrail = "AUDITTRAIL";
        public const string Role = "ROLE";
        public const string Reportname = "REPORTNAME";
        public const string Fromdate = "FROMDATE";
        public const string Todate = "TODATE";
        public const string Moduleid = "MODULEID";
        public const string Modulename = "MODULENAME";
        public const string Local = "LOCAL";
        public const string Rows = "ROWS";
        public const string AuditTrailSPName = "rsp_GetAuditDetails";
        public const string IncludeNonCashable = "INCLUDENONCASHABLE";
        public const string UsePhysicalWin = "USEPHYSICALWIN";
        public const string UseHandpay = "SHOWHANDPAY";
        public const string UseJackpot= "SHOWJACKPOT";
        public const string UseGroupByZone = "GROUPBYZONE";
        public const string IsGamingDayBasedReport = "ISGAMINGDAYBASEDREPORT";

        public const string BMC = "BMC";
        public const string Promotional = "PROMOTIONAL";
        public const string Both = "BOTH";

    }
}
