using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.EnterpriseReportsDataAccess;
using BMC.EnterpriseReportsTransport;
using BMC.Security.Interfaces;
using System.Data.Linq;
using System.Data;
using System.Reflection;
using BMC.Reports;

namespace BMC.EnterpriseReportsBusiness
{
    public class ReportsBusiness
    {
        ReportDataContext oReportDataContext = new ReportDataContext();
        ReportServerSetting oReportServerSetting = new ReportServerSetting();
        Parameterlist oParamList = new Parameterlist();
        public ReportsBusiness()
        {

        }
        public List<GetAllReportsToRolesAccess> GetAllReportsToRolesAccess(int RoleID)
        {
            return oReportDataContext.GetAllReportsToRolesAccess(RoleID).ToList();
        }
        public List<IRole> GetRoleByName(string RoleName)
        {
            return oReportDataContext.GetRoleByName(RoleName).ToList();

        }
        public List<Company> CompanyDetails(int iSecurityUserID)
        {
            return oReportDataContext.GetCompanyDetails(iSecurityUserID).ToList();

        }
        public List<SubCompany> GetSubCompany(int CompanyID,int SecurityUserID)
        {
            return oReportDataContext.GetSubCompanyDetails(CompanyID,SecurityUserID).ToList();
        }
        public List<SubCompanyRegion> GetRegion(int subcmp,int comcmp)
        {
            return oReportDataContext.GetRegionDetails(subcmp, comcmp).ToList();
        }
        public List<SubCompanyArea> GetArea(int regid, int comcmp, int subcmp)
        {
            return oReportDataContext.GetAreaDetails(regid, comcmp, subcmp).ToList();
        }
        public List<SubCompanyDistrict> GetDistrict(int region, int area, int subcompany_id, int companyID)
        {
            return oReportDataContext.GetDistrictDetails(region, area, subcompany_id, companyID).ToList();
        }
        public List<Machine_Type> GetCategory()
        {
            return oReportDataContext.GetCategoryDetails().ToList();
        }
        public List<Depot> GetDepot(int SiteId)
        {
            return oReportDataContext.GetDepoDetails(SiteId).ToList();
        }
        public List<Site> GetSites(int cmp, int subcmp, int region, int area, int UserID)
        {
            return oReportDataContext.GetSites(cmp, subcmp, region, area, UserID).ToList();
        }

        public List<Site> GetSiteDetailsForDistict(int cmp, int subcmp, int region, int area,int district, int UserID)
        {
            return oReportDataContext.GetSiteDetailsForDistict(cmp, subcmp, region, area, district, UserID).ToList();
        }

        public List<BatchDetails> GetBatchDetails(string SiteCode)
        {
            return oReportDataContext.GetBatchDetails(SiteCode).ToList();
        }
        public List<Operator> GetSuppliers()
        {
            return oReportDataContext.GetSuppliers().ToList();
        }

        public List<PeriodEnd> GetWeeklyLiquidationBatch(int CompanyID, int SubCompany)
        {
            return oReportDataContext.GetWeeklyLiquidationBatch(CompanyID, SubCompany).ToList();
        }

        public List<EmployeeID> GetEmployeeID(int Site)
        {
            return oReportDataContext.GetEmployeeID(Site).ToList();
        }

        public List<EmployeeCardID> GetEmployeeCardID(int Site)
        {
            return GetEmployeeCardID(Site, 0);
        }
        public List<EmployeeCardID> GetEmployeeCardID(int Site, int EmpID)
        {
            return oReportDataContext.GetEmployeeCardID(Site, EmpID).ToList();
        }
        public List<CardNumberList> GetCardNumberList()
        {
            return oReportDataContext.GetEmployeeCardNumber().ToList();
        }
        public List<EmployeeNameList> GetEmployeeName(int EmpCardID)
        {
            return oReportDataContext.GetEmployeeName(EmpCardID).ToList();
        }
        public List<CardTypes> GetCardTypes()
        {
            return oReportDataContext.GetEmployeeCardTypes().ToList();
        }

        public List<SiteLicensing> GetRuleName()
        {
            return oReportDataContext.GetRuleName().ToList();
        }
        public List<UserRoles> GetRoleToUser(int SecurityUserID)
        {
            return oReportDataContext.GetRoleToUser(SecurityUserID).ToList();
        }

        public List<System.Data.SqlClient.SqlParameter> GetParameters(string SPName)
        {
            return oParamList.GetParameters(SPName);
        }


        public int GetALLSiteCount()
        {
            return oReportDataContext.GetAllSiteCount();
        }

        public string GetSetting(string Settingname)
        {
            return GetSetting(Settingname, "");
        }

        public string GetSetting(string Settingname, string SettingValue)
        {
            string settingvalue = string.Empty;
            oReportDataContext.GetSetting(Convert.ToInt32("0"), Settingname, SettingValue, ref settingvalue);
            return settingvalue;
        }

        public List<OrderBy> GetOrderBy()
        {
            List<OrderBy> oOrderBy = new List<OrderBy>();
            OrderBy o = new OrderBy();
            o.OrderBy_ID = 0;
            o.OrderBy_Name = "Site Name";
            oOrderBy.Add(o);
            o = new OrderBy();
            o.OrderBy_ID = 1;
            o.OrderBy_Name = "Site Code";
            oOrderBy.Add(o);
            o = new OrderBy();
            o.OrderBy_ID = 2;
            o.OrderBy_Name = "Postcode";
            oOrderBy.Add(o);

            return oOrderBy;
        }
        public List<Period> GetPeriod()
        {
            List<Period> oPeriod = new List<Period>();
            Period o = new Period();
            o.Period_Id = 0;
            o.DisplayPeriodName = "Month To Date";
            o.PeriodName = "MTD";
            oPeriod.Add(o);
            o = new Period();
            o.Period_Id = 1;
            o.DisplayPeriodName = "Life To Date";
            o.PeriodName = "LTD";
            oPeriod.Add(o);
            o = new Period();
            o.Period_Id = 2;
            o.DisplayPeriodName = "Period To Date";
            o.PeriodName = "PTD";
            oPeriod.Add(o);
            o = new Period();
            o.Period_Id = 3;
            o.DisplayPeriodName = "Year To Date";
            o.PeriodName = "YTD";
            oPeriod.Add(o);
            o = new Period();
            o.Period_Id = 4;
            o.DisplayPeriodName = "Gaming Date";
            o.PeriodName = "DAY";
            oPeriod.Add(o);
            o = new Period();
            o.Period_Id = 5;
            o.DisplayPeriodName = "Week To Date";
            o.PeriodName = "WTD";
            oPeriod.Add(o);
            o = new Period();
            o.Period_Id = 6;
            o.DisplayPeriodName = "Quarter To Date";
            o.PeriodName = "QTD";
            oPeriod.Add(o);
            return oPeriod;
        }
        public List<Period> GetPeriod(bool bPeriod)
        {
            List<Period> oPeriod = new List<Period>();
            Period o = new Period();
            o.Period_Id = 0;
            o.DisplayPeriodName = "Month To Date";
            o.PeriodName = "MTD";
            oPeriod.Add(o);

            o = new Period();
            o.Period_Id = 1;
            o.DisplayPeriodName = "Year To Date";
            o.PeriodName = "YTD";
            oPeriod.Add(o);
            o = new Period();
            o.Period_Id = 2;
            o.DisplayPeriodName = "Gaming Date";
            o.PeriodName = "DAY";
            oPeriod.Add(o);

            o = new Period();
            o.Period_Id = 3;
            o.DisplayPeriodName = "Quarter To Date";
            o.PeriodName = "QTD";
            oPeriod.Add(o);
            return oPeriod;
        }
        public List<Period> GetPeriod(int iPeriod)
        {
            List<Period> oPeriod = new List<Period>();
            Period o = new Period();

            o.Period_Id = 0;
            o.DisplayPeriodName = "Life To Date";
            o.PeriodName = "LTD";
            oPeriod.Add(o);
            o = new Period();
            o.Period_Id = 1;
            o.DisplayPeriodName = "Month To Date";
            o.PeriodName = "MTD";
            oPeriod.Add(o);
            o = new Period();
            o.Period_Id = 2;
            o.DisplayPeriodName = "Quarter To Date";
            o.PeriodName = "QTD";
            oPeriod.Add(o);
            o = new Period();
            o.Period_Id = 3;
            o.DisplayPeriodName = "Year To Date";
            o.PeriodName = "QTD";
            oPeriod.Add(o);
            return oPeriod;
        }

        public string GetReportPath()
        {
            return oReportServerSetting.ReportPathURL();
        }
        public string GetReportFolder()
        {
            return oReportServerSetting.ReportFolder();
        }
        public string ReportServerURL(string strReport)
        {
            return oReportServerSetting.GenerateURL(strReport);
        }
        public string GetRegionalDate(DateTime dtCurrentDate)
        {
            string strCurrentDate = string.Empty;
            // set currency format

            string curCulture = System.Threading.Thread.CurrentThread.CurrentCulture.ToString();

            System.Globalization.DateTimeFormatInfo DateFormat = new System.Globalization.CultureInfo(curCulture).DateTimeFormat;

            strCurrentDate = dtCurrentDate.ToString("d", DateFormat);

            return strCurrentDate;
        }
        public string GetRegionalTime(DateTime dtCurrentTime)
        {
            string strCurrentTime = string.Empty;
            // set currency format

            string curCulture = System.Threading.Thread.CurrentThread.CurrentCulture.ToString();

            System.Globalization.DateTimeFormatInfo TimeFormat = new System.Globalization.CultureInfo(curCulture).DateTimeFormat;

            strCurrentTime = dtCurrentTime.ToString(TimeFormat.LongTimePattern);

            return strCurrentTime;
        }

        //public bool ExecuteQueryForRowCount(string ProcedureName, ListReportparameters lstParams, bool SDSReports)
        //{
        //    return oParamList.ExecuteQueryForRowCount(ProcedureName, lstParams, SDSReports);
        //}

        public bool ExecuteQueryForRowCount(string ProcedureName, clsSPParams spParams, bool SDSReports)
        {
            return oParamList.ExecuteQueryForRowCount(ProcedureName, spParams, SDSReports);
        }

        public DataSet ExecuteDataSet(string ProcedureName, clsSPParams lstParams)
        {
            return oParamList.ExecuteDataSet(ProcedureName, lstParams);
        }


        public string GetReportMessageException()
        {
            return oReportServerSetting.GetReportMessageException();
        }

        public int GetReadException(string ProcedureName, clsSPParams lstParams)
        {
            return oParamList.GetReadException(ProcedureName, lstParams);
        }
        public List<Zone> GetZones(int siteid)
        {
            return oReportDataContext.GetZones(siteid).ToList();
        }

        public List<Zone> GetZones(int siteid, int SubCompany, int CompanyID)
        {
            return oReportDataContext.GetZones(siteid, SubCompany,CompanyID).ToList();
        }

        public string GetBMCVersion()
        {
            foreach (var result in oReportDataContext.GetBMCVersion())
                return result.Result;

            return "12.1";
        }

        public SiteCultureInfo GetSiteCulture(int userID)
        {
            foreach (var result in oReportDataContext.GetSiteCulture(userID))
                return result;

            return null;

            //return new SiteCultureInfo(string.Empty, "en-US", "en-US", "en-US");
        }

        public List<Slot> GetSlots(int SiteID,int CompanyID,int SubCompanyId)
        {
            return oReportDataContext.GetSlots(SiteID, CompanyID, SubCompanyId).ToList();
        }


        public DataSet GetRedeemedTicketByDevice(int Company, int SubCompany, int Site, int Zone, DateTime fromDate, DateTime toDate, string DeviceType,string sSiteIDList)
        {
            return oReportDataContext.GetRedeemedTicketByDevice(Company, SubCompany, Site, Zone, fromDate, toDate, DeviceType, sSiteIDList);
        }

        public DataSet GetExpiredVoucherCouponReport(int Company, int SubCompany, int Site, DateTime startDate, DateTime endDate, string sDeviceType, string sSiteIDList)
        {
            return oReportDataContext.GetExpiredVoucherCouponReport(Company, SubCompany, Site, startDate, endDate, sDeviceType, sSiteIDList);
        }

        public DataSet GetLiabilityTransferSummaryReport(int Company, int SubCompany, int Zone, int Site, DateTime startDate, DateTime endDate, string sSiteIDList)
        {
            return oReportDataContext.GetLiabilityTransferSummaryReport(Company, SubCompany, Zone, Site, startDate, endDate,sSiteIDList);
        }

        public DataSet GetVoucherListingReport(int Company, int SubCompany, int Site, int ZoneID, DateTime startDate, DateTime endDate, string sStatus, string sSlot, string sSiteIDList)
        {
            return oReportDataContext.GetVoucherListingReport(Company, SubCompany, Site, ZoneID, startDate, endDate, sStatus, sSlot, sSiteIDList);
        }


        public DataSet GetPromotionalVoucherListingReport(int Company, int SubCompany, int Site,DateTime startDate, DateTime endDate, string sStatus,string sSiteIDList)
        {
            return oReportDataContext.GetPromotionalVoucherListingReport(Company, SubCompany, Site, startDate, endDate, sStatus, sSiteIDList);
        }

        

        public DataSet GetJackpotSlipSummaryDetails(int company, int subCompany, int site, DateTime fromDate,
            DateTime toDate, bool ShowHandpay, bool ShowJackpot, string SiteIDList)
        {
            return oReportDataContext.GetJackpotSlipSummaryDetails(company, subCompany, 
                site, fromDate, toDate, ShowHandpay,ShowJackpot,SiteIDList);
        }

        public DataSet GetCoinInByPaytableDetails(int company, int subCompany, int site, DateTime fromDate, DateTime toDate, string groupBy, string SiteIDList)
        {
            return oReportDataContext.GetCoinInByPaytableDetails(company, subCompany, site, fromDate, toDate, groupBy,SiteIDList);
        }

        public DataSet GetMultiDenomSlotDetails(int company, int subCompany, int site, DateTime fromDate, DateTime toDate, string groupBy, string SiteIDList)
        {
            return oReportDataContext.GetMultiDenomSlotDetails(company, subCompany, site, fromDate, toDate, groupBy,SiteIDList);
        }

        public DataSet GetMultiGameSlotDetails(int company, int subCompany, int site, DateTime fromDate, DateTime toDate, string groupBy, string SiteIDList)
        {
            return oReportDataContext.GetMultiGameSlotDetails(company, subCompany, site, fromDate, toDate, groupBy,SiteIDList);
        }

        public DataSet GetMultiDenomPerformanceDetails(int company, int subCompany, int site, DateTime fromDate, DateTime toDate, string SiteIDList)
        {
            return oReportDataContext.GetMultiDenomPerformanceDetails(company, subCompany, site, fromDate, toDate,SiteIDList);
        }

        public DataSet GetMultiGamePerformanceDetails(int company, int subCompany, int site, int zone, DateTime fromDate, DateTime toDate, string SiteIDList)
        {
            return oReportDataContext.GetMultiGamePerformanceDetails(company, subCompany, site, zone, fromDate, toDate,SiteIDList);
        }

        public DataSet GetMultiGameDetails(int company, int subCompany, int site, int zone, DateTime fromDate, DateTime toDate, string SiteIDList)
        {
            return oReportDataContext.GetMultiGameDetails(company, subCompany, site,zone, fromDate, toDate,SiteIDList);
        }

        public DataSet GetStandardMeterComparisonDetails(int company, int subCompany, int site, int zone, DateTime fromDate, DateTime toDate, float denom, int runtype, string SiteIDList)
        {
            return oReportDataContext.GetStandardMeterComparisonDetails(company, subCompany, site, zone, fromDate, toDate, denom, runtype, SiteIDList);
        }

        public DataSet GetSlotMachinePerformaceDetails(int company, int subCompany, int site, DateTime gamingDate, float denom, string machineTypeID, int runType, bool usePhysicalWin, bool includeNonCashable, string SiteIDList)
        {
            return oReportDataContext.GetSlotMachinePerformaceDetails(company, subCompany, site, gamingDate, denom, machineTypeID, runType, usePhysicalWin, includeNonCashable,SiteIDList);
        }

        public DataSet GetSlotCountComparisonDetails(int company, int subCompany, int site, int zone, DateTime fromDate, DateTime toDate, float denom, int runtype, bool GroupByZone, string SiteIDList)
        {
            return oReportDataContext.GetSlotCountComparisonDetails(company, subCompany, site, zone, fromDate, toDate, denom, runtype, GroupByZone, SiteIDList);
        }

        public DataSet GetStackerDetails(int Company, int SubCompany, int Area, int Region, int Site, int District, int StackerLevel, string SiteIDList)
        {
            return oReportDataContext.GetStackerDetails(Company, SubCompany, Area, Region, Site, District, StackerLevel, SiteIDList);
        }

        public DataSet GetDropScheduleDetails(int Company, int SubCompany, int Area, int Region, int Site, int District, int StackerLevel, string SiteIDList)
        {
            return oReportDataContext.GetDropScheduleDetails(Company, SubCompany, Area, Region, Site, District, StackerLevel,SiteIDList);
        }
        //public DataSet GetDropScheduleDetails()
        //{
        //    return oReportDataContext.GetDropScheduleDetails();
        //}

        public DataSet GetEmployeeCardSessions(int Company, int SubCompany, int Region, int Area, int District, int Site, int EmpID, string EmpCardID, DateTime StarDate, DateTime EndDate, string SiteIDList)
        {
            return oReportDataContext.GetEmployeeCardSessionsDetails(Company, SubCompany, Region, Area, District, Site, EmpID, EmpCardID, StarDate, EndDate,SiteIDList);
        }

        public DataSet GetEmployeeCardList(int CardNumber, string EmpName, string CardStatus, string CardType)
        {
            return oReportDataContext.GetEmployeeCardListDetails(CardNumber, EmpName, CardStatus, CardType);
        }
        
        // Added by A.Vinod Kumar on 16/12/2010
        public DataSet GetElecTransferVsRevenueComparisonDetails(int company, int subCompany, int site, string zone, DateTime gamingDate, int ZoneId, bool GroupByZone, string SiteIDList)
        {
            return oReportDataContext.GetElecTransferVsRevenueComparisonDetails(company, subCompany, site, zone, gamingDate, ZoneId,GroupByZone,SiteIDList);
        }

        public string GetRegulatoryType()
        {
            ReportServerSetting oSetting = new ReportServerSetting();
            return oSetting.GetRegulatory();
        }
        public string GetCurrentCurrenyCulture()
        {
            ReportServerSetting oSetting = new ReportServerSetting();
            string sCurrentCurrenyCulture = oSetting.GetCurrentCurrenyCulture();
            return sCurrentCurrenyCulture;                      
        }

        public DataSet getDailyEFTFundRevenueDetails(int ncompany, int nsubCompany, int nSite, int zone, string dtGamingDate, string sPeriod, string SiteIDList, ref bool isSuccess)
        {
            return oReportDataContext.getDailyEFTFundRevenueDetails(ncompany, nsubCompany, nSite, zone, dtGamingDate, sPeriod,SiteIDList,ref isSuccess);
        }

        public DataSet getEFTQuestionableTransactions(int nCompany, int nSubCompany, int nSite, string dtStartDate, string dtEndDate, string SiteIDList)
        {
            return oReportDataContext.getEFTQuestionableTransactions(nCompany, nSubCompany, nSite, dtStartDate, dtEndDate,SiteIDList);
        }

        public DataSet getEFTSlotActivity(int nCompany, int nSubCompany, int nSite, string dtStartDate, string dtEndDate, string SiteIDList)
        {
            return oReportDataContext.getEFTSlotActivity(nCompany, nSubCompany, nSite, dtStartDate, dtEndDate,SiteIDList);
        }

        public DataSet getInstallationWinLoss(int nCompany, int nSubCompany,
            int region, int area, int district, int nSite, int Category, string dtStartDate, string dtEndDate, string SiteIDList)
        {
            return oReportDataContext.getInstallationWinLoss(nCompany, nSubCompany, region, area, district, nSite, Category, dtStartDate, dtEndDate,SiteIDList);
        }

        public DataSet GetDailyAccoutingDetails(int nCompany, int nSubCompany,
           int region, int area, int district, int nSite, int Category, string dtStartDate, string dtEndDate, string SiteIDList)
        {
            return oReportDataContext.GetDailyAccoutingDetails(nCompany, nSubCompany, region, area, district, nSite, Category, dtStartDate, dtEndDate,SiteIDList);
        }

        public DataSet getEFTSlotActivityCumulative(int nCompany, int nSubCompany, int nSite, string dtStartDate, string dtEndDate, string SiteIDList)
        {
            return oReportDataContext.getEFTSlotActivityCumulative(nCompany, nSubCompany, nSite, dtStartDate, dtEndDate,SiteIDList);
        }
        public DataSet GetAssetDetails(int CompanyID, int MachineStatusFlag )
        {
            return oReportDataContext.GetAssetDetails(CompanyID, MachineStatusFlag);
        }

        public DataSet getMGMDByGamingDeviceCabinetReport(int ncompany, int nsubCompany, int nSite, int zone, string dtGamingDate, string sPeriod)
        {

            return oReportDataContext.getMGMDByGamingDeviceCabinetReport(ncompany, nsubCompany, nSite, zone, dtGamingDate, sPeriod);
        }



        public DataSet getMGMDSummaryDetails(int nCompany, int nSubCompany, int nSite, int nZone, string dtGamingDate, string Period, string SiteIDList)
        {
            return oReportDataContext.getMGMDSummaryDetails(nCompany, nSubCompany, nSite, nZone, dtGamingDate, Period,SiteIDList);
        }

        public DataSet getManufacturerPerformanceReport(int ncompany, int nsubCompany, int nSite, string dtGamingDate, string sPeriod, string SiteIDList)
        {
            return oReportDataContext.getManufacturerPerformanceReport(ncompany, nsubCompany, nSite, dtGamingDate, sPeriod,SiteIDList);
        }
        public DataSet GetLiabilityTransferDetailsReport(int Company, int SubCompany, int Zone, int Site, DateTime startDate, DateTime endDate,string sSiteIDList)
        {
            return oReportDataContext.GetLiabilityTransferDetailsReport(Company, SubCompany, Zone, Site, startDate, endDate, sSiteIDList);
        }

        public DataSet GetCrossPropertyTicketAnalysisReport(int Company, int SubCompany, int Zone, int Site, DateTime startDate, DateTime endDate, string sSiteIDList)
        {
            return oReportDataContext.GetCrossPropertyTicketAnalysisReport(Company, SubCompany, Zone, Site, startDate, endDate, sSiteIDList);

        }
        public DataSet GetWinComparisonReport(int Company, int SubCompany, int Zone, int Site, DateTime GamingDate, bool IncludeNonCashable, bool UsePhysicalWin, string Slot, string Period, string SiteIDList, ref bool isSuccess)
        {
            return oReportDataContext.GetWinComparisonReport(Company, SubCompany, Zone, Site, GamingDate, IncludeNonCashable, UsePhysicalWin, Slot, Period,SiteIDList, ref isSuccess);
        }

        public DataSet GetDeclarationVouchersDetails(int Company, int SubCompany, int Site, string Slot, DateTime startDate, DateTime endDate)
        {
            return oReportDataContext.GetDeclarationVouchersDetails(Company, SubCompany, Site, Slot, startDate, endDate);
        }
		       
        public DataSet GetVoucherCouponLiabilityReport(int nCompany, int nSubCompany, int nSite, int nZone, DateTime dtIssueDate, string strVoucherStatus, string sDeviceType,string sSiteIDList)
        {
            return oReportDataContext.GetVoucherCouponLiabilityReport(nCompany, nSubCompany, nSite, nZone, dtIssueDate, strVoucherStatus, sDeviceType, sSiteIDList);
        }
		
		public int GetAccountingWinLossDetails(int nCompany, int nSubCompany,
           int region, int area, int district, int nSite,int zone, int Category, string dtStartDate, string dtEndDate, string SiteIDList)
        {
            return oReportDataContext.GetAccountingWinLossDetails(nCompany, nSubCompany, region, area, district, nSite, zone, Category, dtStartDate, dtEndDate, SiteIDList);
        }

        public DataSet GetLiquidationExpenseDetails(int Company, int SubCompany, int Region, int Area, int District, int Site, string dtStartDate, string dtEndDate, string SiteIDList)
        {
            return oReportDataContext.GetLiquidationExpenseDetails(Company, SubCompany, Region,Area, District, Site, dtStartDate, dtEndDate,SiteIDList);
        }
        public DataSet GetPeriodEndLiquidationRevenueDetails(int Company, int SubCompany, int Region, int Area, int District, int Site, string dtStartDate, string dtEndDate, string SiteIDList)
        {
            return oReportDataContext.GetPeriodEndLiquidationRevenueDetails(Company, SubCompany, Region, Area, District, Site, dtStartDate, dtEndDate,SiteIDList);
        }
        

        //SP8 Base Starts
        public DataSet getTotalFundsInSummary(int Company, int SubCompany, int Site, string SiteIDList)
        {
            return oReportDataContext.GetTotalFundsInSummary(Company, SubCompany, Site, SiteIDList);
        }

        public DataSet getTotalFundsInDetails(int Company, int SubCompany, int Site, int Zone, string SiteIDList)
        {
            return oReportDataContext.GetTotalFundsInDetails(Company, SubCompany, Site, Zone, SiteIDList);
        }
        public DataSet GetVaultBalanceDetails(int company, int subCompany, int site, string SiteIDList)
        {
            return oReportDataContext.GetVaultBalanceDetails(company, subCompany, site,SiteIDList);
        }
        public DataSet GetPromoSummaryDetails(int Company, int SubCompany, int Region, int Area, int District, int Site, string StartDate, string EndDate,string sSiteIDList)
        {
            return oReportDataContext.GetPromoSummaryDetails(Company, SubCompany, Region, Area, District, Site, StartDate, EndDate,sSiteIDList);
        }

        //SP8 Base Ends
        //(Ep4 Changes)
        public DataSet GetVaultConfigurationDetails(int company, int subCompany, int site, string VaultStatus, int UserID)
        {
            return oReportDataContext.GetVaultConfigurationDetails(company, subCompany, site, VaultStatus, UserID);
        }
        public DataSet GetCashDispenserAccounting(int company, int subcompany, int site, DateTime  startDate, DateTime Enddate, int userID, string SiteIDList)
        {
            return oReportDataContext.GetCashDispenserAccountingReport(company, subcompany, site, startDate, Enddate, userID, SiteIDList);
        }
        public DataSet GetCashDispenserDropDetails(int Company, int SubCompany, int Site, string Status, bool IncludeZero, int UserID, string StartDate, string EndDate, string SiteIDList)
        {
            return oReportDataContext.GetCashDispDrop(Company, SubCompany, Site, Status, IncludeZero, UserID, StartDate, EndDate, SiteIDList);
        }

        public DataSet GetCashDispVarianceDetails(int Company, int SubCompany, int Site, string Status, bool IncludeZero, string StartDate, string EndDate, int UserID, string SiteIDList)
        {
            return oReportDataContext.GetCashDispVariance(Company, SubCompany, Site, Status, IncludeZero, StartDate, EndDate, UserID, SiteIDList);
        }

        public DataSet GetCashDispTransactionDetails(int Company, int SubCompany, int Site, string TransType, string StartDate, string EndDate, int UserID, string SiteIDList)
        {
            return oReportDataContext.GetCashDispTransaction(Company, SubCompany, Site, TransType, StartDate, EndDate, UserID, SiteIDList);
        }

        public DataSet GetCashDispenserLevelDetails(int Company, int SubCompany, int Region, int Area, int District, int Site, int InventoryLevel, string InventoryType, int UserID, string SiteIDList)
        {
            return oReportDataContext.GetCashDispLevelDetails(Company, SubCompany, Region, Area, District, Site, InventoryLevel, InventoryType, UserID, SiteIDList);
        }

        //(Ep4 Changes Ends)                

        //12.4.2
        public DataSet GetCashDispCassetteInventoryStatus(int Company, int Subcompany, int Site, int Userid, string SiteIDList)
        {
            return oReportDataContext.GetCashDispCassettesInventoryStatus(Company, Subcompany, Site, Userid, SiteIDList);
        }

        public DataSet GetCashDispCassetteAccountingDetail(int Company, int Subcompany, int Site, string StartDate, string EndDate, int Userid, string SiteIDList)
        {
            return oReportDataContext.GetCashDispCassettesAccountingDetail(Company, Subcompany, Site, StartDate, EndDate, Userid, SiteIDList);
        }
    }
   
}
