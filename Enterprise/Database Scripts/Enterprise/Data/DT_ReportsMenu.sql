/************************************************************
 * Created by Bally Technologies © 2011
 * Time: 26/12/12 5:43:30 PM
 ************************************************************/
USE [Enterprise]
GO

DECLARE @Client AS VARCHAR(100)
SET @Client=''
	EXEC rsp_GetSetting NULL, 'CLIENT', '', @Client OUTPUT
SET @Client = UPPER(LTRIM(RTRIM(ISNULL(@Client,''))))

IF NOT EXISTS(SELECT 1 FROM [ReportsMenu] WHERE ReportName = 'Management Reports')
    INSERT [ReportsMenu] ( ReportID, ReportMenuID, LEVEL, ReportName, ReportDescription, ApplicationServer, ReportArgName, ReportStatus, ShowException, ShowPowerPromoReports, SecurityRoleID, MS_ProcedureUsed, Client )
    SELECT 1, NULL, 1, 'Management Reports', 'Main Menu', 'Enterprise', NULL, 1, NULL, NULL, 0, NULL, NULL
UPDATE [ReportsMenu] 
SET    
	ReportName = 'Management Reports',
	ReportArgName = NULL,
	MS_ProcedureUsed = NULL,
	IsTimeRequired = 0
WHERE  ReportName = 'Management Reports'



IF NOT EXISTS(SELECT 1 FROM [ReportsMenu] WHERE ReportName = 'Audit / Security Reports')
    INSERT [ReportsMenu] ( ReportID, ReportMenuID, LEVEL, ReportName, ReportDescription, ApplicationServer, ReportArgName, ReportStatus, ShowException, ShowPowerPromoReports, SecurityRoleID, MS_ProcedureUsed, Client )
    SELECT 2, NULL, 1, 'Audit / Security Reports', 'Main Menu', 'Enterprise', NULL, 1, NULL, NULL, 0, NULL, NULL
UPDATE [ReportsMenu] 
SET    
	ReportName = 'Audit / Security Reports',
	ReportArgName = NULL,
	MS_ProcedureUsed = NULL,
	IsTimeRequired = 0
WHERE  ReportName = 'Audit / Security Reports'



IF NOT EXISTS(SELECT 1 FROM [ReportsMenu] WHERE ReportName = 'Service Reports')
    INSERT [ReportsMenu] ( ReportID, ReportMenuID, LEVEL, ReportName, ReportDescription, ApplicationServer, ReportArgName, ReportStatus, ShowException, ShowPowerPromoReports, SecurityRoleID, MS_ProcedureUsed, Client )
    SELECT 3, NULL, 1, 'Service Reports', 'Main Menu', 'Enterprise', NULL, 1, NULL, NULL, 0, NULL, NULL
UPDATE [ReportsMenu] 
SET    
	ReportName = 'Service Reports',
	ReportArgName = NULL,
	MS_ProcedureUsed = NULL,
	IsTimeRequired = 0
WHERE  ReportName = 'Service Reports'


IF NOT EXISTS(SELECT 1 FROM [ReportsMenu] WHERE ReportName = 'Maintenance Reports')
    INSERT [ReportsMenu] ( ReportID, ReportMenuID, LEVEL, ReportName, ReportDescription, ApplicationServer, ReportArgName, ReportStatus, ShowException, ShowPowerPromoReports, SecurityRoleID, MS_ProcedureUsed, Client )
    SELECT 4, NULL, 1, 'Maintenance Reports', 'Main Menu', 'Enterprise', NULL, 1, NULL, NULL, 0, NULL, NULL
UPDATE [ReportsMenu] 
SET    
	ReportName = 'Maintenance Reports',
	ReportArgName = NULL,
	MS_ProcedureUsed = NULL,
	IsTimeRequired = 0
WHERE  ReportName = 'Maintenance Reports'


IF NOT EXISTS(SELECT 1 FROM [ReportsMenu] WHERE LTRIM(RTRIM(ReportName)) = 'Site Reports' AND ReportDescription = 'Main Menu')
    INSERT [ReportsMenu] ( ReportID, ReportMenuID, LEVEL, ReportName, ReportDescription, ApplicationServer, ReportArgName, ReportStatus, ShowException, ShowPowerPromoReports, SecurityRoleID, MS_ProcedureUsed, Client )
    SELECT 5, NULL, 1, 'Site Reports', 'Main Menu', 'Enterprise', NULL, 1, NULL, NULL, 0, NULL, NULL
UPDATE [ReportsMenu] 
SET    
	ReportName = 'Site Reports',
	ReportArgName = NULL,
	MS_ProcedureUsed = NULL,
	IsTimeRequired = 0
WHERE  LTRIM(RTRIM(ReportName)) = 'Site Reports' AND ReportDescription = 'Main Menu' --Trimmed to check for migrated ReportName consisting blank space.


IF NOT EXISTS(SELECT 1 FROM [ReportsMenu] WHERE ReportName = 'SGVI Lottery Reports')
    INSERT [ReportsMenu] ( ReportID, ReportMenuID, LEVEL, ReportName, ReportDescription, ApplicationServer, ReportArgName, ReportStatus, ShowException, ShowPowerPromoReports, SecurityRoleID, MS_ProcedureUsed, Client )
    SELECT 6, NULL, 1, 'SGVI Lottery Reports', 'Main Menu', 'Enterprise', NULL, 0, NULL, NULL, 0, NULL, 'SGVI'
UPDATE [ReportsMenu] 
SET    
	ReportName = 'SGVI Lottery Reports',
	ReportArgName = NULL,
	MS_ProcedureUsed = NULL,
	IsTimeRequired = 0
WHERE  ReportName = 'SGVI Lottery Reports'

IF NOT EXISTS(SELECT 1 FROM [ReportsMenu] WHERE ReportName = 'Admin Reports' AND ReportDescription='Main Menu')
    INSERT [ReportsMenu] ( ReportID, ReportMenuID, LEVEL, ReportName, ReportDescription, ApplicationServer, ReportArgName, ReportStatus, ShowException, ShowPowerPromoReports, SecurityRoleID, MS_ProcedureUsed, Client )
    SELECT 7, NULL, 1, 'Admin Reports', 'Main Menu', 'Enterprise', NULL, 1, NULL, NULL, 0, NULL, NULL
UPDATE [ReportsMenu] 
SET    
	ReportName = 'Admin Reports',
	ReportArgName = NULL,
	MS_ProcedureUsed = NULL,
	IsTimeRequired = 0
WHERE ReportName = 'Admin Reports' AND ReportDescription='Main Menu'

IF NOT EXISTS(SELECT 1 FROM [ReportsMenu] WHERE ReportName = 'Meter Summary Flash Report')
    INSERT [ReportsMenu] ( ReportID, ReportMenuID, LEVEL, ReportName, ReportDescription, ApplicationServer, ReportArgName, ReportStatus, ShowException, ShowPowerPromoReports, SecurityRoleID, MS_ProcedureUsed, Client )
    SELECT 8, 1, 2, 'Meter Summary Flash Report', 'Management Reports', 'Enterprise', 'METERSUMMARY', 1, 1, 'FALSE', 0, 'rsp_REPORT_MeterSummaryDetail', NULL
UPDATE [ReportsMenu] 
SET    
	ReportName = 'Meter Summary Flash Report',
	ReportArgName = 'METERSUMMARY',
	MS_ProcedureUsed = 'rsp_REPORT_MeterSummaryDetail',
	IsTimeRequired = 0
WHERE ReportArgName = 'METERSUMMARY'

IF NOT EXISTS(SELECT 1 FROM [ReportsMenu] WHERE ReportName = 'Meter Detailed Flash Report')
    INSERT [ReportsMenu] ( ReportID, ReportMenuID, LEVEL, ReportName, ReportDescription, ApplicationServer, ReportArgName, ReportStatus, ShowException, ShowPowerPromoReports, SecurityRoleID, MS_ProcedureUsed, Client )
    SELECT 9, 1, 2, 'Meter Detailed Flash Report', 'Management Reports', 'Enterprise', 'METERDETAIL', 1, 1, 'FALSE', 0, 'rsp_REPORT_MeterDetail', NULL
UPDATE [ReportsMenu] 
SET    
	ReportName = 'Meter Detailed Flash Report',
	ReportArgName = 'METERDETAIL',
	MS_ProcedureUsed = 'rsp_REPORT_MeterDetail',
	IsTimeRequired = 0
WHERE  ReportArgName = 'METERDETAIL'


IF NOT EXISTS(SELECT 1 FROM [ReportsMenu] WHERE ReportName = 'Accounting Summary Report')
    INSERT [ReportsMenu] ( ReportID, ReportMenuID, LEVEL, ReportName, ReportDescription, ApplicationServer, ReportArgName, ReportStatus, ShowException, ShowPowerPromoReports, SecurityRoleID, MS_ProcedureUsed, Client )
    SELECT 11, 1, 2, 'Accounting Summary Report', 'Management Reports', 'Enterprise', 'ACCOUNTINGSUMMARY', 1, 0, 'FALSE', 0, 'rsp_REPORT_AccountAvgDailyWin', NULL
UPDATE [ReportsMenu] 
SET    
	ReportName =  'Accounting Summary Report',
	ReportArgName = 'ACCOUNTINGSUMMARY',
	MS_ProcedureUsed = 'rsp_REPORT_AccountAvgDailyWin',
	IsTimeRequired = 0
WHERE ReportArgName = 'ACCOUNTINGSUMMARY'


IF NOT EXISTS(SELECT 1 FROM [ReportsMenu] WHERE ReportName = 'Accounting Detailed Report')
    INSERT [ReportsMenu] ( ReportID, ReportMenuID, LEVEL, ReportName, ReportDescription, ApplicationServer, ReportArgName, ReportStatus, ShowException, ShowPowerPromoReports, SecurityRoleID, MS_ProcedureUsed, Client )
    SELECT 12, 1, 2, 'Accounting Detailed Report', 'Management Reports', 'Enterprise', 'ACCOUNTINGDETAIL', 1, 0, 'FALSE', 0, 'rsp_REPORT_AccountAvgDailyWin', NULL
UPDATE [ReportsMenu] 
SET    
	ReportName =  'Accounting Detailed Report',
	ReportArgName = 'ACCOUNTINGDETAIL',
	MS_ProcedureUsed = 'rsp_REPORT_AccountAvgDailyWin',
	IsTimeRequired = 0
WHERE  ReportArgName = 'ACCOUNTINGDETAIL'


IF NOT EXISTS(SELECT 1 FROM [ReportsMenu] WHERE ReportName = 'Depreciation Report')
    INSERT [ReportsMenu] ( ReportID, ReportMenuID, LEVEL, ReportName, ReportDescription, ApplicationServer, ReportArgName, ReportStatus, ShowException, ShowPowerPromoReports, SecurityRoleID, MS_ProcedureUsed, Client )
    SELECT 13, 1, 2, 'Depreciation Report', 'Management Reports', 'Enterprise', 'DEPRECIATIONREPORT', 1, 0, 'FALSE', 0, 'GetDepreciationDet', NULL
UPDATE [ReportsMenu] 
SET    
	ReportName = 'Depreciation Report',
	ReportArgName = 'DEPRECIATIONREPORT',
	MS_ProcedureUsed = 'GetDepreciationDet',
	IsTimeRequired = 0
WHERE  ReportArgName = 'DEPRECIATIONREPORT'


IF NOT EXISTS(SELECT 1 FROM [ReportsMenu] WHERE ReportName = 'Progressive Win Summary Report')
    INSERT [ReportsMenu] ( ReportID, ReportMenuID, LEVEL, ReportName, ReportDescription, ApplicationServer, ReportArgName, ReportStatus, ShowException, ShowPowerPromoReports, SecurityRoleID, MS_ProcedureUsed, Client )
    SELECT 14, 1, 2, 'Progressive Win Summary Report', 'Management Reports', 'Enterprise', 'PROGRESSIVEWINSUMMARYREPORT', 1, 1, 'FALSE', 0, 'rsp_REPORT_ProgressiveReport', NULL
UPDATE [ReportsMenu] 
SET    
	ReportName = 'Progressive Win Summary Report',
	ReportArgName = 'PROGRESSIVEWINSUMMARYREPORT',
	MS_ProcedureUsed = 'rsp_REPORT_ProgressiveReport',
	IsTimeRequired = 0
WHERE  ReportArgName = 'PROGRESSIVEWINSUMMARYREPORT'

IF NOT EXISTS(SELECT 1 FROM [ReportsMenu] WHERE ReportName = 'Progressive Win Detailed Report')
    INSERT [ReportsMenu] ( ReportID, ReportMenuID, LEVEL, ReportName, ReportDescription, ApplicationServer, ReportArgName, ReportStatus, ShowException, ShowPowerPromoReports, SecurityRoleID, MS_ProcedureUsed, Client )
    SELECT 15, 1, 2, 'Progressive Win Detailed Report', 'Management Reports', 'Enterprise', 'PROGRESSIVEWINDETAILEDREPORT', 1, 1, 'FALSE', 0, 'rsp_REPORT_ProgressiveReport', NULL
UPDATE [ReportsMenu] 
SET    
	ReportName = 'Progressive Win Detailed Report',
	ReportArgName = 'PROGRESSIVEWINDETAILEDREPORT',
	MS_ProcedureUsed = 'rsp_REPORT_ProgressiveReport',
	IsTimeRequired = 0
WHERE  ReportArgName = 'PROGRESSIVEWINDETAILEDREPORT'


IF NOT EXISTS(SELECT 1 FROM [ReportsMenu] WHERE ReportName = 'Consolidated M/C Collection Variance Report')
    INSERT [ReportsMenu] ( ReportID, ReportMenuID, LEVEL, ReportName, ReportDescription, ApplicationServer, ReportArgName, ReportStatus, ShowException, ShowPowerPromoReports, SecurityRoleID, MS_ProcedureUsed, Client )
    SELECT 16, 2, 2, 'Consolidated M/C Collection Variance Report', 'Audit / Security Reports', 'Enterprise', 'CONSOLIDATEDVARIANCE', 1, 0, 'FALSE', 0, 'rsp_consolidated_variance', NULL
UPDATE [ReportsMenu] 
SET    
	ReportName = 'Consolidated M/C Collection Variance Report',
	ReportArgName = 'CONSOLIDATEDVARIANCE',
	MS_ProcedureUsed = 'rsp_consolidated_variance',
	IsTimeRequired = 0
WHERE  ReportArgName = 'CONSOLIDATEDVARIANCE'


IF NOT EXISTS(SELECT 1 FROM [ReportsMenu] WHERE ReportName = 'Service Remedy Codes')
    INSERT [ReportsMenu] ( ReportID, ReportMenuID, LEVEL, ReportName, ReportDescription, ApplicationServer, ReportArgName, ReportStatus, ShowException, ShowPowerPromoReports, SecurityRoleID, MS_ProcedureUsed, Client )
    SELECT 17, 3, 2, 'Service Remedy Codes', 'Service Reports', 'Enterprise', 'SERVICEREMEDYCODES', 1, 0, 'FALSE', 0, 'GetServiceRemedyCodes', NULL

UPDATE [ReportsMenu] 
SET    
	ReportName = 'Service Remedy Codes',
	ReportArgName = 'SERVICEREMEDYCODES',
	MS_ProcedureUsed = 'GetServiceRemedyCodes',
	IsTimeRequired = 0
WHERE  ReportArgName = 'SERVICEREMEDYCODES'

IF NOT EXISTS(SELECT 1 FROM [ReportsMenu] WHERE ReportName = 'Service Fault Group')
    INSERT [ReportsMenu] ( ReportID, ReportMenuID, LEVEL, ReportName, ReportDescription, ApplicationServer, ReportArgName, ReportStatus, ShowException, ShowPowerPromoReports, SecurityRoleID, MS_ProcedureUsed, Client )
    SELECT 18, 3, 2, 'Service Fault Group', 'Service Reports', 'Enterprise', 'SERVICEFAULTGROUP', 1, 0, 'FALSE', 0, 'GetServiceFaultGroup', NULL
UPDATE [ReportsMenu] 
SET    
	ReportName = 'Service Fault Group',
	ReportArgName = 'SERVICEFAULTGROUP',
	MS_ProcedureUsed = 'GetServiceFaultGroup',
	IsTimeRequired = 0
WHERE  ReportArgName = 'SERVICEFAULTGROUP'


IF NOT EXISTS(SELECT 1 FROM [ReportsMenu] WHERE ReportName = 'Service SLA Terms')
    INSERT [ReportsMenu] ( ReportID, ReportMenuID, LEVEL, ReportName, ReportDescription, ApplicationServer, ReportArgName, ReportStatus, ShowException, ShowPowerPromoReports, SecurityRoleID, MS_ProcedureUsed, Client )
    SELECT 19, 3, 2, 'Service SLA Terms', 'Service Reports', 'Enterprise', 'SERVICESLATERMS', 1, 0, 'FALSE', 0, 'GetServiceSLATerms', NULL
UPDATE [ReportsMenu] 
SET    
	ReportName = 'Service SLA Terms',
	ReportArgName = 'SERVICESLATERMS',
	MS_ProcedureUsed = 'GetServiceSLATerms',
	IsTimeRequired = 0
WHERE ReportArgName = 'SERVICESLATERMS'


IF NOT EXISTS(SELECT 1 FROM [ReportsMenu] WHERE ReportName = 'Site Reports' AND ReportDescription='Maintenance Reports')
    INSERT [ReportsMenu] ( ReportID, ReportMenuID, LEVEL, ReportName, ReportDescription, ApplicationServer, ReportArgName, ReportStatus, ShowException, ShowPowerPromoReports, SecurityRoleID, MS_ProcedureUsed, Client )
    SELECT 20, 4, 1, 'Site Reports', 'Maintenance Reports', 'Enterprise', NULL, 0, 0, NULL, 0, NULL, NULL

UPDATE [ReportsMenu] 
SET    
	ReportName = 'Site Reports',
	ReportArgName = NULL,
	MS_ProcedureUsed = NULL,
	IsTimeRequired = 0
WHERE  ReportName = 'Site Reports' AND ReportDescription='Maintenance Reports'

IF NOT EXISTS(SELECT 1 FROM [ReportsMenu] WHERE ReportName = 'Admin Reports' AND ReportDescription='Maintenance Reports')
    INSERT [ReportsMenu] ( ReportID, ReportMenuID, LEVEL, ReportName, ReportDescription, ApplicationServer, ReportArgName, ReportStatus, ShowException, ShowPowerPromoReports, SecurityRoleID, MS_ProcedureUsed, Client )
    SELECT 21, 4, 1, 'Admin Reports', 'Maintenance Reports', 'Enterprise', NULL, 0, 0, NULL, 0, NULL, NULL
UPDATE [ReportsMenu] 
SET    
	ReportName = 'Admin Reports',
	ReportArgName = NULL,
	MS_ProcedureUsed = NULL,
	IsTimeRequired = 0
WHERE ReportName = 'Admin Reports' AND ReportDescription='Maintenance Reports'


IF NOT EXISTS(SELECT 1 FROM [ReportsMenu] WHERE ReportName = 'Site Address List Report')
    INSERT [ReportsMenu] ( ReportID, ReportMenuID, LEVEL, ReportName, ReportDescription, ApplicationServer, ReportArgName, ReportStatus, ShowException, ShowPowerPromoReports, SecurityRoleID, MS_ProcedureUsed, Client )
    SELECT 22, 20, 2, 'Site Address List Report', 'Site Reports', 'Enterprise', 'SITEADDRESSREPORT', 1, 0, 'FALSE', 0, 'rsp_Report_SiteAddressList', NULL
UPDATE [ReportsMenu] 
SET    
	ReportName = 'Site Address List Report',
	ReportArgName = 'SITEADDRESSREPORT',
	MS_ProcedureUsed = 'rsp_Report_SiteAddressList',
	IsTimeRequired = 0
WHERE ReportArgName = 'SITEADDRESSREPORT'

IF NOT EXISTS(SELECT 1 FROM [ReportsMenu] WHERE ReportName = 'Staff List Report')
    INSERT [ReportsMenu] ( ReportID, ReportMenuID, LEVEL, ReportName, ReportDescription, ApplicationServer, ReportArgName, ReportStatus, ShowException, ShowPowerPromoReports, SecurityRoleID, MS_ProcedureUsed, Client )
    SELECT 23, 21, 2, 'Staff List Report', 'Admin Reports', 'Enterprise', 'STAFFLIST', 1, 0, 'FALSE', 0, 'GetStaffList', NULL

UPDATE [ReportsMenu] 
SET    
	ReportName = 'Staff List Report',
	ReportArgName = 'STAFFLIST',
	MS_ProcedureUsed = 'GetStaffList',
	IsTimeRequired = 0
WHERE  ReportArgName = 'STAFFLIST'

IF NOT EXISTS(SELECT 1 FROM [ReportsMenu] WHERE ReportName = 'Staff List (Detailed) Report')
    INSERT [ReportsMenu] ( ReportID, ReportMenuID, LEVEL, ReportName, ReportDescription, ApplicationServer, ReportArgName, ReportStatus, ShowException, ShowPowerPromoReports, SecurityRoleID, MS_ProcedureUsed, Client )
    SELECT 24, 21, 2, 'Staff List (Detailed) Report', 'Admin Reports', 'Enterprise', 'STAFFLISTDETAILED', 1, 0, 'FALSE', 0, 'usp_GetDetailedStaffList', NULL

UPDATE [ReportsMenu] 
SET    
	ReportName = 'Staff List (Detailed) Report',
	ReportArgName = 'STAFFLISTDETAILED',
	MS_ProcedureUsed = 'usp_GetDetailedStaffList',
	IsTimeRequired = 0
WHERE  ReportArgName = 'STAFFLISTDETAILED'

IF NOT EXISTS(SELECT 1 FROM [ReportsMenu] WHERE ReportName = 'Expired Calendars Report')
    INSERT [ReportsMenu] ( ReportID, ReportMenuID, LEVEL, ReportName, ReportDescription, ApplicationServer, ReportArgName, ReportStatus, ShowException, ShowPowerPromoReports, SecurityRoleID, MS_ProcedureUsed, Client )
    SELECT 25, 21, 2, 'Expired Calendars Report', 'Admin Reports', 'Enterprise', 'EXPIREDCALENDARS', 1, 0, 'FALSE', 0, 'GetExpiredCalendarList', NULL
UPDATE [ReportsMenu] 
SET    
	ReportName = 'Expired Calendars Report',
	ReportArgName = 'EXPIREDCALENDARS',
	MS_ProcedureUsed = 'GetExpiredCalendarList',
	IsTimeRequired = 0
WHERE  ReportArgName = 'EXPIREDCALENDARS'


IF NOT EXISTS(SELECT 1 FROM [ReportsMenu] WHERE ReportName = 'Meter Period Comparison Report')
    INSERT [ReportsMenu] ( ReportID, ReportMenuID, LEVEL, ReportName, ReportDescription, ApplicationServer, ReportArgName, ReportStatus, ShowException, ShowPowerPromoReports, SecurityRoleID, MS_ProcedureUsed, Client )
    SELECT 26, 1, 2, 'Meter Period Comparison Report', 'Management Reports', 'Enterprise', 'METERPERIODCOMPARISON', 1, 1, 'FALSE', 0, 'rsp_REPORT_YearonYear', NULL
UPDATE [ReportsMenu] 
SET    
	ReportName = 'Meter Period Comparison Report',
	ReportArgName = 'METERPERIODCOMPARISON',
	MS_ProcedureUsed = 'rsp_REPORT_YearonYear',
	IsTimeRequired = 0
WHERE  ReportArgName = 'METERPERIODCOMPARISON'


IF NOT EXISTS(SELECT 1 FROM [ReportsMenu] WHERE ReportName = 'Daily Accounting Detail Report')
    INSERT [ReportsMenu] ( ReportID, ReportMenuID, LEVEL, ReportName, ReportDescription, ApplicationServer, ReportArgName, ReportStatus, ShowException, ShowPowerPromoReports, SecurityRoleID, MS_ProcedureUsed, Client )
    SELECT 27, 1, 2, 'Daily Accounting Detail Report', 'Management Reports', 'Enterprise', 'DAILYACCOUNTINGDETAIL', 1, 1, 'FALSE', 0, 'rsp_REPORT_DailyAccounting', NULL
UPDATE [ReportsMenu] 
SET    
	ReportName = 'Daily Accounting Detail Report',
	ReportArgName = 'DAILYACCOUNTINGDETAIL',
	MS_ProcedureUsed = 'rsp_REPORT_DailyAccounting',
	IsTimeRequired = 0
WHERE ReportArgName = 'DAILYACCOUNTINGDETAIL'


IF NOT EXISTS(SELECT 1 FROM [ReportsMenu] WHERE ReportName = 'Daily Accounting Summary Report')
    INSERT [ReportsMenu] ( ReportID, ReportMenuID, LEVEL, ReportName, ReportDescription, ApplicationServer, ReportArgName, ReportStatus, ShowException, ShowPowerPromoReports, SecurityRoleID, MS_ProcedureUsed, Client )
    SELECT 28, 1, 2, 'Daily Accounting Summary Report', 'Management Reports', 'Enterprise', 'DAILYACCOUNTINGSUMMARY', 1, 1, 'FALSE', 0, 'rsp_REPORT_DailyAccounting', NULL
UPDATE [ReportsMenu] 
SET    
	ReportName = 'Daily Accounting Summary Report',
	ReportArgName = 'DAILYACCOUNTINGSUMMARY',
	MS_ProcedureUsed = 'rsp_REPORT_DailyAccounting',
	IsTimeRequired = 0
WHERE ReportArgName = 'DAILYACCOUNTINGSUMMARY'


IF NOT EXISTS(SELECT 1 FROM [ReportsMenu] WHERE ReportName = 'Exception Report')
    INSERT [ReportsMenu] ( ReportID, ReportMenuID, LEVEL, ReportName, ReportDescription, ApplicationServer, ReportArgName, ReportStatus, ShowException, ShowPowerPromoReports, SecurityRoleID, MS_ProcedureUsed, Client )
    SELECT 29, 1, 0, 'Exception Report', 'Management Reports', 'Enterprise', 'EXCEPTION', 1, 0, 'FALSE', 0, 'rsp_REPORT_GetMissingReadData', NULL
UPDATE [ReportsMenu] 
SET    
	ReportName = 'Exception Report',
	ReportArgName = 'EXCEPTION',
	MS_ProcedureUsed = 'rsp_REPORT_GetMissingReadData',
	IsTimeRequired = 0
WHERE ReportArgName = 'EXCEPTION'


IF NOT EXISTS(SELECT 1 FROM [ReportsMenu] WHERE ReportName = 'Major Prizes Report')
    INSERT [ReportsMenu] ( ReportID, ReportMenuID, LEVEL, ReportName, ReportDescription, ApplicationServer, ReportArgName, ReportStatus, ShowException, ShowPowerPromoReports, SecurityRoleID, MS_ProcedureUsed, Client )
    SELECT 30, 6, 2, 'Major Prizes Report', 'SGVI Lottery Reports', 'Enterprise', 'MAJORPRIZESREPORT', 0, 0, 'FALSE', 0, 'rsp_GetTreasuryEntries', 'SGVI'
UPDATE [ReportsMenu] 
SET    
	ReportName = 'Major Prizes Report',
	ReportArgName = 'MAJORPRIZESREPORT',
	MS_ProcedureUsed = 'rsp_GetTreasuryEntries',
	IsTimeRequired = 1
WHERE ReportArgName = 'MAJORPRIZESREPORT'


IF NOT EXISTS(SELECT 1 FROM [ReportsMenu] WHERE ReportName = 'Weekly Revenue Liquidation Report')
    INSERT [ReportsMenu] ( ReportID, ReportMenuID, LEVEL, ReportName, ReportDescription, ApplicationServer, ReportArgName, ReportStatus, ShowException, ShowPowerPromoReports, SecurityRoleID, MS_ProcedureUsed, Client )
    SELECT 31, 6, 2, 'Weekly Revenue Liquidation Report', 'SGVI Lottery Reports', 'Enterprise', 'WRL', 0, 0, 'FALSE', 0, 'rsp_SSRS_REPORT_SGVI_WRL', 'SGVI'
UPDATE [ReportsMenu] 
SET    
	ReportName = 'Weekly Revenue Liquidation Report',
	ReportArgName = 'WRL',
	MS_ProcedureUsed = 'rsp_SSRS_REPORT_SGVI_WRL',
	IsTimeRequired = 0
WHERE ReportArgName = 'WRL'


IF NOT EXISTS(SELECT 1 FROM [ReportsMenu] WHERE ReportName = 'Liquidations Report')
    INSERT [ReportsMenu] ( ReportID, ReportMenuID, LEVEL, ReportName, ReportDescription, ApplicationServer, ReportArgName, ReportStatus, ShowException, ShowPowerPromoReports, SecurityRoleID, MS_ProcedureUsed, Client )
    SELECT 32, 6, 2, 'Liquidations Report', 'SGVI Lottery Reports', 'Enterprise', 'LIQUIDATION', 0, 0, 'FALSE', 0, 'rsp_REPORT_SGVI_LiquidationDetail', 'SGVI'
UPDATE [ReportsMenu] 
SET    
	ReportName = 'Liquidations Report',
	ReportArgName = 'LIQUIDATION',
	MS_ProcedureUsed = 'rsp_REPORT_SGVI_LiquidationDetail',
	IsTimeRequired = 0
WHERE ReportArgName = 'LIQUIDATION'

IF @Client = 'SGVI'
BEGIN
IF NOT EXISTS(SELECT 1 FROM [ReportsMenu] WHERE ReportName = 'Accounting Machine Win/Loss Report')
		INSERT [ReportsMenu] ( ReportID, ReportMenuID, LEVEL, ReportName, ReportDescription, ApplicationServer, ReportArgName, ReportStatus, ShowException, ShowPowerPromoReports, SecurityRoleID, MS_ProcedureUsed, Client, ExportExcel, XMLName)
		SELECT 33, 1, 2, 'Accounting Machine Win/Loss Report', 'Management Reports', 'Enterprise', 'ACCOUNTINGWINLOSS', 0, 0, 'FALSE', 0, 'rsp_REPORT_BankingReport', 'SGVI',1,'AccountingMachineWinLoss.xml'
UPDATE [ReportsMenu] 
SET    
	ReportName = 'Accounting Machine Win/Loss Report',
	ReportArgName = 'ACCOUNTINGWINLOSS',
	MS_ProcedureUsed = 'rsp_REPORT_BankingReport',
	IsTimeRequired = 0
WHERE ReportArgName = 'ACCOUNTINGWINLOSS'
END

IF NOT EXISTS(SELECT 1 FROM [ReportsMenu] WHERE ReportName = 'Soft Count Comparison Report')
    INSERT [ReportsMenu] ( ReportID, ReportMenuID, LEVEL, ReportName, ReportDescription, ApplicationServer, ReportArgName, ReportStatus, ShowException, ShowPowerPromoReports, SecurityRoleID, MS_ProcedureUsed, Client )
    SELECT 34, 1, 2, 'Soft Count Comparison Report', 'Management Reports', 'Enterprise', 'SoftCountComparisonReport', 1, 0, 'FALSE', 0, 'rsp_Report_SoftCountComparison', NULL
UPDATE [ReportsMenu] 
SET    
	ReportName = 'Soft Count Comparison Report',
	ReportArgName = 'SoftCountComparisonReport',
	MS_ProcedureUsed = 'rsp_Report_SoftCountComparison',
	IsTimeRequired = 0
WHERE ReportArgName = 'SoftCountComparisonReport' OR ReportArgName = 'BMC_SOFTCOUNTCOMPARISON'


IF NOT EXISTS(SELECT 1 FROM [ReportsMenu] WHERE ReportName = 'Slot Machine Performance Report')
    INSERT [ReportsMenu] ( ReportID, ReportMenuID, LEVEL, ReportName, ReportDescription, ApplicationServer, ReportArgName, ReportStatus, ShowException, ShowPowerPromoReports, SecurityRoleID, MS_ProcedureUsed, Client )
    SELECT 35, 1, 2, 'Slot Machine Performance Report', 'Management Reports', 'Enterprise', 'SlotMachinePerformanceReport', 1, 0, 'FALSE', 0, 'rsp_Report_SlotMachinePerformance', NULL
UPDATE [ReportsMenu] 
SET    
	ReportName = 'Slot Machine Performance Report',
	ReportArgName = 'SlotMachinePerformanceReport',
	MS_ProcedureUsed = 'rsp_Report_SlotMachinePerformance',
	IsTimeRequired = 0
WHERE ReportArgName = 'SlotMachinePerformanceReport' OR ReportArgName = 'BMC_SLOTPERFORMANCE'


IF NOT EXISTS(SELECT 1 FROM [ReportsMenu] WHERE ReportName = 'Standard Meter Comparison Report')
    INSERT [ReportsMenu] ( ReportID, ReportMenuID, LEVEL, ReportName, ReportDescription, ApplicationServer, ReportArgName, ReportStatus, ShowException, ShowPowerPromoReports, SecurityRoleID, MS_ProcedureUsed, Client )
    SELECT 36, 1, 2, 'Standard Meter Comparison Report', 'Management Reports', 'Enterprise', 'MeterComparisonReport', 1, 0, 'FALSE', 0, 'rsp_report_metercomparison', NULL
UPDATE [ReportsMenu] 
SET    
	ReportName = 'Standard Meter Comparison Report',
	ReportArgName = 'MeterComparisonReport',
	MS_ProcedureUsed = 'rsp_report_metercomparison',
	IsTimeRequired = 0
WHERE ReportArgName = 'MeterComparisonReport' OR ReportArgName = 'BMC_METERCOMPARISON'

IF @Client ='WINCHELLS'
BEGIN
IF NOT EXISTS(SELECT 1 FROM [ReportsMenu] WHERE ReportName = 'Accounting Machine Win/Loss Report')
	INSERT [ReportsMenu] ( ReportID, ReportMenuID, LEVEL, ReportName, ReportDescription, ApplicationServer, ReportArgName, ReportStatus, ShowException, ShowPowerPromoReports, SecurityRoleID, MS_ProcedureUsed, Client,ExportExcel,XMLName )
	SELECT 37, 1, 2, 'Accounting Machine Win/Loss Report', 'Management Reports', 'Enterprise', 'ACCOUNTINGWINLOSS', 1, 0, 'FALSE', 0, 'rsp_REPORT_BankingReport', 'WINCHELLS',1,'AccountingMachineWinLoss.xml'
UPDATE [ReportsMenu] 
SET    
	ReportName = 'Accounting Machine Win/Loss Report',
	ReportArgName = 'ACCOUNTINGWINLOSS',
	MS_ProcedureUsed = 'rsp_REPORT_BankingReport',
	IsTimeRequired = 0
WHERE ReportArgName = 'ACCOUNTINGWINLOSS'
END

IF NOT EXISTS(SELECT 1 FROM [ReportsMenu] WHERE ReportName = 'Daily Electronic Fund Revenue Report')
    INSERT [ReportsMenu] ( ReportID, ReportMenuID, LEVEL, ReportName, ReportDescription, ApplicationServer, ReportArgName, ReportStatus, ShowException, ShowPowerPromoReports, SecurityRoleID, MS_ProcedureUsed, Client )
    SELECT 38, 1, 2, 'Daily Electronic Fund Revenue Report', 'Management Reports', 'Enterprise', 'DailyElectronicFundRevenue', 1, 0, 'TRUE', 0, 'rsp_Report_DailyElectronicCashRevenue_Crystal', 'WINCHELLS'
UPDATE [ReportsMenu] 
SET    
	ReportName = 'Daily Electronic Fund Revenue Report',
	ReportArgName = 'DailyElectronicFundRevenue',
	MS_ProcedureUsed = 'rsp_Report_DailyElectronicCashRevenue_Crystal',
	IsTimeRequired = 0
WHERE ReportArgName = 'DailyElectronicFundRevenue' OR ReportArgName = 'BMC_DAILYEFTFUNDREVENUE'


IF NOT EXISTS(SELECT 1 FROM [ReportsMenu] WHERE ReportName = 'EFT Questionable Transactions Report')
    INSERT [ReportsMenu] ( ReportID, ReportMenuID, LEVEL, ReportName, ReportDescription, ApplicationServer, ReportArgName, ReportStatus, ShowException, ShowPowerPromoReports, SecurityRoleID, MS_ProcedureUsed, Client )
    SELECT 39, 1, 2, 'EFT Questionable Transactions Report', 'Management Reports', 'Enterprise', 'EFTQuestionableTransactionsReport', 1, 0, 'TRUE', 0, 'rsp_Report_EFTQuestionableTransactions', 'WINCHELLS'
UPDATE [ReportsMenu] 
SET    
	ReportName = 'EFT Questionable Transactions Report',
	ReportArgName = 'EFTQuestionableTransactionsReport',
	MS_ProcedureUsed = 'rsp_Report_EFTQuestionableTransactions',
	IsTimeRequired = 1
WHERE ReportArgName = 'EFTQuestionableTransactionsReport' OR ReportArgName = 'BMC_EFTQUESTIONABLETRANSACTIONS'


IF NOT EXISTS(SELECT 1 FROM [ReportsMenu] WHERE ReportName = 'EFT Slot Activity Report')
    INSERT [ReportsMenu] ( ReportID, ReportMenuID, LEVEL, ReportName, ReportDescription, ApplicationServer, ReportArgName, ReportStatus, ShowException, ShowPowerPromoReports, SecurityRoleID, MS_ProcedureUsed, Client )
    SELECT 40, 1, 2, 'EFT Slot Activity Report', 'Management Reports', 'Enterprise', 'EFTSlotActivityReport', 1, 0, 'TRUE', 0, 'rsp_Report_EFTSlotActivity', 'WINCHELLS'
UPDATE [ReportsMenu] 
SET    
	ReportName = 'EFT Slot Activity Report',
	ReportArgName = 'EFTSlotActivityReport',
	MS_ProcedureUsed = 'rsp_Report_EFTSlotActivity',
	IsTimeRequired = 1
WHERE ReportArgName = 'EFTSlotActivityReport' OR ReportArgName = 'BMC_EFTSLOTACTIVITY'


IF NOT EXISTS(SELECT 1 FROM [ReportsMenu] WHERE ReportName = 'EFT Slot Activity Cumulative Report')
    INSERT [ReportsMenu] ( ReportID, ReportMenuID, LEVEL, ReportName, ReportDescription, ApplicationServer, ReportArgName, ReportStatus, ShowException, ShowPowerPromoReports, SecurityRoleID, MS_ProcedureUsed, Client )
    SELECT 41, 1, 2, 'EFT Slot Activity Cumulative Report', 'Management Reports', 'Enterprise', 'EFTSlotActivityCumulativeReport', 1, 0, 'TRUE', 0, 'rsp_Report_EFTSlotActivity', 'WINCHELLS'
UPDATE [ReportsMenu] 
SET    
	ReportName = 'EFT Slot Activity Cumulative Report',
	ReportArgName = 'EFTSlotActivityCumulativeReport',
	MS_ProcedureUsed = 'rsp_Report_EFTSlotActivity',
	IsTimeRequired = 1
WHERE ReportArgName = 'EFTSlotActivityCumulativeReport' OR ReportArgName = 'BMC_EFTSLOTACTIVITYCUMULATIVE'


IF NOT EXISTS(SELECT 1 FROM [ReportsMenu] WHERE ReportName = 'EFT Vs Revenue Comparison Report')
    INSERT [ReportsMenu] ( ReportID, ReportMenuID, LEVEL, ReportName, ReportDescription, ApplicationServer, ReportArgName, ReportStatus, ShowException, ShowPowerPromoReports, SecurityRoleID, MS_ProcedureUsed, Client )
    SELECT 42, 1, 2, 'EFT Vs Revenue Comparison Report', 'Management Reports', 'Enterprise', 'ElecTransferVsRevenueComparisonReport', 1, 0, 'TRUE', 0, 'rsp_Report_EFTVersusBMCRevenueComparison', 'WINCHELLS'
UPDATE [ReportsMenu] 
SET    
	ReportName = 'EFT Vs Revenue Comparison Report',
	ReportArgName = 'ElecTransferVsRevenueComparisonReport',
	MS_ProcedureUsed = 'rsp_Report_EFTVersusBMCRevenueComparison',
	IsTimeRequired = 0
WHERE ReportArgName = 'ElecTransferVsRevenueComparisonReport' OR ReportArgName = 'BMC_ELECTRANSFERVSREVENUECOMPARISON'


IF NOT EXISTS(SELECT 1 FROM [ReportsMenu] WHERE ReportName = 'Voucher Reports')
    INSERT [ReportsMenu] ( ReportID, ReportMenuID, LEVEL, ReportName, ReportDescription, ApplicationServer, ReportArgName, ReportStatus, ShowException, ShowPowerPromoReports, SecurityRoleID, MS_ProcedureUsed, Client )
    SELECT 43, NULL, 1, 'Voucher Reports', 'Main Menu', 'Enterprise', NULL, 1, NULL, NULL, 2, NULL, NULL
UPDATE [ReportsMenu] 
SET    
	ReportName = 'Voucher Reports',
	ReportArgName = NULL,
	MS_ProcedureUsed = NULL,
	IsTimeRequired = 0
WHERE ReportName = 'Voucher Reports'


IF NOT EXISTS(SELECT 1 FROM [ReportsMenu] WHERE ReportName = 'Voucher/Coupon Liability Report')
    INSERT [ReportsMenu] ( ReportID, ReportMenuID, LEVEL, ReportName, ReportDescription, ApplicationServer, ReportArgName, ReportStatus, ShowException, ShowPowerPromoReports, SecurityRoleID, MS_ProcedureUsed, Client )
    SELECT 44, 43, 2, 'Voucher/Coupon Liability Report', 'Voucher Reports', 'Enterprise', 'VoucherCouponLiabilityReport', 1, 0, 'FALSE', 2, 'rsp_GetTicketLiabilityReport', NULL
UPDATE [ReportsMenu] 
SET    
	ReportName = 'Voucher/Coupon Liability Report',
	ReportArgName = 'VoucherCouponLiabilityReport',
	MS_ProcedureUsed = 'rsp_GetTicketLiabilityReport',
	IsTimeRequired = 1
WHERE ReportArgName = 'VoucherCouponLiabilityReport' OR ReportArgName = 'VoucherCouponLiablityReport'


IF NOT EXISTS(SELECT 1 FROM [ReportsMenu] WHERE ReportName = 'Redeemed Voucher By Device Report')
    INSERT [ReportsMenu] ( ReportID, ReportMenuID, LEVEL, ReportName, ReportDescription, ApplicationServer, ReportArgName, ReportStatus, ShowException, ShowPowerPromoReports, SecurityRoleID, MS_ProcedureUsed, Client )
    SELECT 45, 43, 2, 'Redeemed Voucher By Device Report', 'Voucher Reports', 'Enterprise', 'RedeemedTicketByDeviceReport', 1, 0, 'FALSE', 2, 'rsp_getRedeemedTicketByDevice', NULL
UPDATE [ReportsMenu] 
SET    
	ReportName = 'Redeemed Voucher By Device Report',
	ReportArgName = 'RedeemedTicketByDeviceReport',
	MS_ProcedureUsed = 'rsp_getRedeemedTicketByDevice',
	IsTimeRequired = 1
WHERE ReportArgName = 'RedeemedTicketByDeviceReport' OR ReportArgName = 'RedeemedVoucherByDeviceReport'


IF NOT EXISTS(SELECT 1 FROM [ReportsMenu] WHERE ReportName = 'Expired Voucher/Coupon Report')
    INSERT [ReportsMenu] ( ReportID, ReportMenuID, LEVEL, ReportName, ReportDescription, ApplicationServer, ReportArgName, ReportStatus, ShowException, ShowPowerPromoReports, SecurityRoleID, MS_ProcedureUsed, Client )
    SELECT 46, 43, 2, 'Expired Voucher/Coupon Report', 'Voucher Reports', 'Enterprise', 'ExpiredVoucherCouponReport', 1, 0, 'FALSE', 2, 'rsp_GetExpiredVoucherCouponReport', NULL
UPDATE [ReportsMenu] 
SET    
	ReportName = 'Expired Voucher/Coupon Report',
	ReportArgName = 'ExpiredVoucherCouponReport',
	MS_ProcedureUsed = 'rsp_GetExpiredVoucherCouponReport',
	IsTimeRequired = 1
WHERE ReportArgName = 'ExpiredVoucherCouponReport'


IF NOT EXISTS(SELECT 1 FROM [ReportsMenu] WHERE ReportName = 'Voucher Listing Report')
    INSERT [ReportsMenu] ( ReportID, ReportMenuID, LEVEL, ReportName, ReportDescription, ApplicationServer, ReportArgName, ReportStatus, ShowException, ShowPowerPromoReports, SecurityRoleID, MS_ProcedureUsed, Client )
    SELECT 47, 43, 2, 'Voucher Listing Report', 'Voucher Reports', 'Enterprise', 'VoucherListingReport', 1, 0, 'FALSE', 2, 'rsp_GetVoucherListingReport', NULL
UPDATE [ReportsMenu] 
SET    
	ReportName = 'Voucher Listing Report',
	ReportArgName = 'VoucherListingReport',
	MS_ProcedureUsed = 'rsp_GetVoucherListingReport',
	IsTimeRequired = 1
WHERE ReportArgName = 'VoucherListingReport'


IF NOT EXISTS(SELECT 1 FROM [ReportsMenu] WHERE ReportName = 'Jackpot Slip Summary Report')
    INSERT [ReportsMenu] ( ReportID, ReportMenuID, LEVEL, ReportName, ReportDescription, ApplicationServer, ReportArgName, ReportStatus, ShowException, ShowPowerPromoReports, SecurityRoleID, MS_ProcedureUsed, Client )
    SELECT 48, 1, 2, 'Jackpot Slip Summary Report', 'Management Reports', 'Enterprise', 'JackpotSlipSummaryReport', 1, 0, 'FALSE', 2, 'rsp_GetJackpotSlipSummaryDetails', NULL
UPDATE [ReportsMenu] 
SET    
	ReportName = 'Jackpot Slip Summary Report',
	ReportArgName = 'JackpotSlipSummaryReport',
	MS_ProcedureUsed = 'rsp_GetJackpotSlipSummaryDetails',
	IsTimeRequired = 1
WHERE ReportArgName = 'JackpotSlipSummaryReport'


IF NOT EXISTS(SELECT 1 FROM [ReportsMenu] WHERE ReportName = 'Expense Details Report')
    INSERT [ReportsMenu] ( ReportID, ReportMenuID, LEVEL, ReportName, ReportDescription, ApplicationServer, ReportArgName, ReportStatus, ShowException, ShowPowerPromoReports, SecurityRoleID, MS_ProcedureUsed, Client )
    SELECT 49, 1, 2, 'Expense Details Report', 'Management Reports', 'Enterprise', 'ExpenseDetailsReport', 1, 0, 'FALSE', 2, 'rsp_GetExpenseDetails', NULL
UPDATE [ReportsMenu] 
SET    
	ReportName = 'Expense Details Report',
	ReportArgName = 'ExpenseDetailsReport',
	MS_ProcedureUsed = 'rsp_GetExpenseDetails',
	IsTimeRequired = 0
WHERE ReportArgName = 'ExpenseDetailsReport'


IF NOT EXISTS(SELECT 1 FROM [ReportsMenu] WHERE ReportName = 'Audit Trail Report')
    INSERT [ReportsMenu] ( ReportID, ReportMenuID, LEVEL, ReportName, ReportDescription, ApplicationServer, ReportArgName, ReportStatus, ShowException, ShowPowerPromoReports, SecurityRoleID, MS_ProcedureUsed, Client )
    SELECT 50, 1, 2, 'Audit Trail Report', 'Management Reports', 'Enterprise', 'AUDITTRAIL', 1, 0, 'FALSE', 2, 'rsp_GetAuditDetails', NULL
UPDATE [ReportsMenu] 
SET    
	ReportName = 'Audit Trail Report',
	ReportArgName = 'AUDITTRAIL',
	MS_ProcedureUsed = 'rsp_GetAuditDetails',
	IsTimeRequired = 1
WHERE ReportArgName = 'AUDITTRAIL'

IF NOT EXISTS(SELECT 1 FROM [ReportsMenu] WHERE ReportName = 'Coin In by Paytable Report')
    INSERT [ReportsMenu] ( ReportID, ReportMenuID, LEVEL, ReportName, ReportDescription, ApplicationServer, ReportArgName, ReportStatus, ShowException, ShowPowerPromoReports, SecurityRoleID, MS_ProcedureUsed, Client )
    SELECT 52, 1, 2, 'Coin In by Paytable Report', 'Management Reports', 'Enterprise', 'CoinInbyPaytableReport', 1, 0, 'FALSE', 1, 'rsp_CoinInByPayTableReport', NULL
UPDATE [ReportsMenu] 
SET    
	ReportName = 'Coin In by Paytable Report',
	ReportArgName = 'CoinInbyPaytableReport',
	MS_ProcedureUsed = 'rsp_CoinInByPayTableReport',
	IsTimeRequired = 1
WHERE ReportArgName = 'CoinInbyPaytableReport'

IF NOT EXISTS(SELECT 1 FROM [ReportsMenu] WHERE ReportName = 'Multi-Denom Slot Detail Report')
    INSERT [ReportsMenu] ( ReportID, ReportMenuID, LEVEL, ReportName, ReportDescription, ApplicationServer, ReportArgName, ReportStatus, ShowException, ShowPowerPromoReports, SecurityRoleID, MS_ProcedureUsed, Client )
    SELECT 53, 1, 2, 'Multi-Denom Slot Detail Report', 'Management Reports', 'Enterprise', 'MultiDenomSlotDetailReport', 1, 0, 'FALSE', 1, 'rsp_MultiDenomSlotDetailReport', NULL
UPDATE [ReportsMenu] 
SET    
	ReportName = 'Multi-Denom Slot Detail Report',
	ReportArgName = 'MultiDenomSlotDetailReport',
	MS_ProcedureUsed = 'rsp_MultiDenomSlotDetailReport',
	IsTimeRequired = 1
WHERE ReportArgName = 'MultiDenomSlotDetailReport'

IF NOT EXISTS(SELECT 1 FROM [ReportsMenu] WHERE ReportName = 'Multi-Game Slot Detail Report')
    INSERT [ReportsMenu] ( ReportID, ReportMenuID, LEVEL, ReportName, ReportDescription, ApplicationServer, ReportArgName, ReportStatus, ShowException, ShowPowerPromoReports, SecurityRoleID, MS_ProcedureUsed, Client )
    SELECT 54, 1, 2, 'Multi-Game Slot Detail Report', 'Management Reports', 'Enterprise', 'MultiGameSlotDetailReport', 1, 0, 'FALSE', 1, 'rsp_MultiGameSlotDetailReport', NULL
UPDATE [ReportsMenu] 
SET    
	ReportName = 'Multi-Game Slot Detail Report',
	ReportArgName = 'MultiGameSlotDetailReport',
	MS_ProcedureUsed = 'rsp_MultiGameSlotDetailReport',
	IsTimeRequired = 1
WHERE ReportArgName = 'MultiGameSlotDetailReport'

IF NOT EXISTS(SELECT 1 FROM [ReportsMenu] WHERE ReportName = 'Multi-Game Denom Performance Report')
    INSERT [ReportsMenu] ( ReportID, ReportMenuID, LEVEL, ReportName, ReportDescription, ApplicationServer, ReportArgName, ReportStatus, ShowException, ShowPowerPromoReports, SecurityRoleID, MS_ProcedureUsed, Client )
    SELECT 55, 1, 2, 'Multi-Game Denom Performance Report', 'Management Reports', 'Enterprise', 'MGMDDenomPerformance', 1, 0, 'FALSE', 1, 'rsp_Report_MGMD_DenomPerformance', NULL
UPDATE [ReportsMenu] 
SET    
	ReportName = 'Multi-Game Denom Performance Report',
	ReportArgName = 'MGMDDenomPerformance',
	MS_ProcedureUsed = 'rsp_Report_MGMD_DenomPerformance',
	IsTimeRequired = 1
WHERE ReportArgName = 'MGMDDenomPerformance'

IF NOT EXISTS(SELECT 1 FROM [ReportsMenu] WHERE ReportName = 'Game Performance Report')
    INSERT [ReportsMenu] ( ReportID, ReportMenuID, LEVEL, ReportName, ReportDescription, ApplicationServer, ReportArgName, ReportStatus, ShowException, ShowPowerPromoReports, SecurityRoleID, MS_ProcedureUsed, Client )
    SELECT 56, 1, 2, 'Game Performance Report', 'Management Reports', 'Enterprise', 'GamePerformanceReport', 1, 0, 'FALSE', 1, 'rsp_Report_MGMD_GamePerformance', NULL
UPDATE [ReportsMenu] 
SET    
	ReportName = 'Game Performance Report',
	ReportArgName = 'GamePerformanceReport',
	MS_ProcedureUsed = 'rsp_Report_MGMD_GamePerformance',
	IsTimeRequired = 1
WHERE ReportArgName = 'GamePerformanceReport'

IF NOT EXISTS(SELECT 1 FROM [ReportsMenu] WHERE ReportName = 'Game Report')
    INSERT [ReportsMenu] ( ReportID, ReportMenuID, LEVEL, ReportName, ReportDescription, ApplicationServer, ReportArgName, ReportStatus, ShowException, ShowPowerPromoReports, SecurityRoleID, MS_ProcedureUsed, Client )
    SELECT 57, 1, 2, 'Game Report', 'Management Reports', 'Enterprise', 'GameReport', 1, 0, 'FALSE', 1, 'rsp_Report_MGMD_GameReport', NULL
UPDATE [ReportsMenu] 
SET    
	ReportName = 'Game Report',
	ReportArgName = 'GameReport',
	MS_ProcedureUsed = 'rsp_Report_MGMD_GameReport',
	IsTimeRequired = 1
WHERE ReportArgName = 'GameReport'

IF NOT EXISTS(SELECT 1 FROM [ReportsMenu] WHERE ReportName = 'Lottery Revenue Report')
    INSERT [ReportsMenu] ( ReportID, ReportMenuID, LEVEL, ReportName, ReportDescription, ApplicationServer, ReportArgName, ReportStatus, ShowException, ShowPowerPromoReports, SecurityRoleID, MS_ProcedureUsed, Client )
    SELECT 58, 6, 2, 'Lottery Revenue Report', 'SGVI Lottery Reports', 'Enterprise', 'LotteryRevenue_US', 0, 0, 'FALSE', 0, 'rsp_Report_LotteryRevenue', 'SGVI'
UPDATE [ReportsMenu] 
SET    
	ReportName = 'Lottery Revenue Report',
	ReportArgName = 'LotteryRevenue_US',
	MS_ProcedureUsed = 'rsp_Report_LotteryRevenue',
	IsTimeRequired = 1
WHERE ReportArgName = 'LotteryRevenue_US'

IF NOT EXISTS(SELECT 1 FROM [ReportsMenu] WHERE ReportName = 'Enrollment Win Loss Report')
    INSERT [ReportsMenu] ( ReportID, ReportMenuID, LEVEL, ReportName, ReportDescription, ApplicationServer, ReportArgName, ReportStatus, ShowException, ShowPowerPromoReports, SecurityRoleID, MS_ProcedureUsed, Client )
    SELECT 59, 1, 2, 'Enrollment Win Loss Report', 'Management Reports', 'Enterprise', 'InstallationWinLoss', 1, 0, 'FALSE', 1, 'rsp_Report_BankingReport_PerInstallation', NULL
UPDATE [ReportsMenu] 
SET    
	ReportName = 'Enrollment Win Loss Report',
	ReportArgName = 'InstallationWinLoss',
	MS_ProcedureUsed = 'rsp_Report_BankingReport_PerInstallation',
	IsTimeRequired = 0
WHERE ReportArgName = 'InstallationWinLoss'

IF NOT EXISTS(SELECT 1 FROM [ReportsMenu] WHERE ReportName = 'Daily Accounting Per Enrollment Report')
    INSERT [ReportsMenu] ( ReportID, ReportMenuID, LEVEL, ReportName, ReportDescription, ApplicationServer, ReportArgName, ReportStatus, ShowException, ShowPowerPromoReports, SecurityRoleID, MS_ProcedureUsed, Client )
    SELECT 60, 1, 2, 'Daily Accounting Per Enrollment Report', 'Management Reports', 'Enterprise', 'DailyAccountingPerInstallation', 1, 0, 'FALSE', 1, 'rsp_Report_DailyAccounting_PerInstallation', NULL
UPDATE [ReportsMenu] 
SET    
	ReportName = 'Daily Accounting Per Enrollment Report',
	ReportArgName = 'DailyAccountingPerInstallation',
	MS_ProcedureUsed = 'rsp_Report_DailyAccounting_PerInstallation',
	IsTimeRequired = 0
WHERE ReportArgName = 'DailyAccountingPerInstallation'

IF NOT EXISTS(SELECT 1 FROM [ReportsMenu] WHERE ReportName = 'Asset Details Report')
    INSERT [ReportsMenu] ( ReportID, ReportMenuID, LEVEL, ReportName, ReportDescription, ApplicationServer, ReportArgName, ReportStatus, ShowException, ShowPowerPromoReports, SecurityRoleID, MS_ProcedureUsed, Client )
    SELECT 61, 1, 2, 'Asset Details Report', 'Management Reports', 'Enterprise', 'AssetDetails', 1, 0, 'FALSE', 1, 'rsp_GetAssetDetailsReport', NULL
UPDATE [ReportsMenu] 
SET    
	ReportName = 'Asset Details Report',
	ReportArgName = 'AssetDetails',
	MS_ProcedureUsed = 'rsp_GetAssetDetailsReport',
	IsTimeRequired = 0
WHERE ReportArgName = 'AssetDetails'

IF NOT EXISTS(SELECT 1 FROM [ReportsMenu] WHERE ReportName = 'MGMD Summary Analysis')
    INSERT [ReportsMenu] ( ReportID, ReportMenuID, LEVEL, ReportName, ReportDescription, ApplicationServer, ReportArgName, ReportStatus, ShowException, ShowPowerPromoReports, SecurityRoleID, MS_ProcedureUsed, Client )
    SELECT 62, 1, 2, 'MGMD Summary Analysis', 'Management Reports', 'Enterprise', 'MGMDSummaryAnalysis', 1, 0, 'FALSE', 1, 'rsp_Report_MGMD_SummaryAnalysis1', NULL
UPDATE [ReportsMenu] 
SET    
	ReportName = 'MGMD Summary Analysis',
	ReportArgName = 'MGMDSummaryAnalysis',
	MS_ProcedureUsed = 'rsp_Report_MGMD_SummaryAnalysis1',
	IsTimeRequired = 0
WHERE ReportArgName = 'MGMDSummaryAnalysis'

IF NOT EXISTS(SELECT 1 FROM [ReportsMenu] WHERE ReportName = 'MGMD By Gaming Device Cabinet Report')
    INSERT [ReportsMenu] ( ReportID, ReportMenuID, LEVEL, ReportName, ReportDescription, ApplicationServer, ReportArgName, ReportStatus, ShowException, ShowPowerPromoReports, SecurityRoleID, MS_ProcedureUsed, Client )
    SELECT 63, 1, 2, 'MGMD By Gaming Device Cabinet Report', 'Management Reports', 'Enterprise', 'MGMDByGamingDeviceCabinetReport', 1, 0, 'FALSE', 1, 'rsp_Report_MGMDByGamingDeviceCabinetReport', NULL
UPDATE [ReportsMenu] 
SET    
	ReportName = 'MGMD By Gaming Device Cabinet Report',
	ReportArgName = 'MGMDByGamingDeviceCabinetReport',
	MS_ProcedureUsed = 'rsp_Report_MGMDByGamingDeviceCabinetReport',
	IsTimeRequired = 0
WHERE ReportArgName = 'MGMDByGamingDeviceCabinetReport'

IF NOT EXISTS(SELECT 1 FROM [ReportsMenu] WHERE ReportName = 'Manufacturer Performance Report')
    INSERT [ReportsMenu] ( ReportID, ReportMenuID, LEVEL, ReportName, ReportDescription, ApplicationServer, ReportArgName, ReportStatus, ShowException, ShowPowerPromoReports, SecurityRoleID, MS_ProcedureUsed, Client )
    SELECT 64, 1, 2, 'Manufacturer Performance Report', 'Management Reports', 'Enterprise', 'ManufacturerPerformanceReport', 1, 0, 'FALSE', 1, '[rsp_Report_ManufacturerPerformanceReport]', NULL
UPDATE [ReportsMenu] 
SET    
	ReportName = 'Manufacturer Performance Report',
	ReportArgName = 'ManufacturerPerformanceReport',
	MS_ProcedureUsed = 'rsp_Report_ManufacturerPerformanceReport',
	IsTimeRequired = 0
WHERE ReportArgName = 'ManufacturerPerformanceReport'

IF NOT EXISTS(SELECT 1 FROM [ReportsMenu] WHERE ReportName = 'CrossPropertyLiabilityTransferSummaryReport' OR ReportName = 'Cross Property Liability Transfer Summary Report')
    INSERT [ReportsMenu] ( ReportID, ReportMenuID, LEVEL, ReportName, ReportDescription, ApplicationServer, ReportArgName, ReportStatus, ShowException, ShowPowerPromoReports, SecurityRoleID, MS_ProcedureUsed, Client )
    SELECT 65, 43, 2, 'Cross Property Liability Transfer Summary Report', 'Voucher Reports', 'Enterprise', 'CrossPropertyLiabilityTransferSummaryReport', 1, 0, 'FALSE', 2, 'rsp_GetLiabilityTransferSummaryReport', NULL
UPDATE [ReportsMenu] 
SET    
	ReportName = 'Cross Property Liability Transfer Summary Report',
	ReportArgName = 'CrossPropertyLiabilityTransferSummaryReport',
	MS_ProcedureUsed = 'rsp_GetLiabilityTransferSummaryReport',
	IsTimeRequired = 1
WHERE ReportArgName = 'CrossPropertyLiabilityTransferSummaryReport'

IF NOT EXISTS(SELECT 1 FROM [ReportsMenu] WHERE ReportName = 'CrossPropertyLiabilityTransferDetailsReport' OR ReportName = 'Cross Property Liability Transfer Details Report')
    INSERT [ReportsMenu] ( ReportID, ReportMenuID, LEVEL, ReportName, ReportDescription, ApplicationServer, ReportArgName, ReportStatus, ShowException, ShowPowerPromoReports, SecurityRoleID, MS_ProcedureUsed, Client )
    SELECT 66, 43, 2, 'Cross Property Liability Transfer Details Report', 'Voucher Reports', 'Enterprise', 'CrossPropertyLiabilityTransferDetailsReport', 1, 0, 'FALSE', 2, 'rsp_getreportliabilitytransfersdetails', NULL
UPDATE [ReportsMenu] 
SET    
	ReportName = 'Cross Property Liability Transfer Details Report',
	ReportArgName = 'CrossPropertyLiabilityTransferDetailsReport',
	MS_ProcedureUsed = 'rsp_getreportliabilitytransfersdetails',
	IsTimeRequired = 1
WHERE ReportArgName = 'CrossPropertyLiabilityTransferDetailsReport'

IF NOT EXISTS(SELECT 1 FROM [ReportsMenu] WHERE ReportName = 'CrossPropertyTicketAnalysisReport' OR ReportName = 'Cross Property Ticket Analysis Report')
    INSERT [ReportsMenu] ( ReportID, ReportMenuID, LEVEL, ReportName, ReportDescription, ApplicationServer, ReportArgName, ReportStatus, ShowException, ShowPowerPromoReports, SecurityRoleID, MS_ProcedureUsed, Client )
    SELECT 67, 43, 2, 'Cross Property Ticket Analysis Report', 'Voucher Reports', 'Enterprise', 'CrossPropertyTicketAnalysisReport', 1, 0, 'FALSE', 2, 'rsp_getCrossPropertyTicketAnalysis', NULL
UPDATE [ReportsMenu] 
SET    
	ReportName = 'Cross Property Voucher Analysis Report',
	ReportArgName = 'CrossPropertyTicketAnalysisReport',
	MS_ProcedureUsed = 'rsp_getCrossPropertyTicketAnalysis',
	IsTimeRequired = 1
WHERE ReportArgName = 'CrossPropertyTicketAnalysisReport'

IF NOT EXISTS(SELECT 1 FROM [ReportsMenu] WHERE ReportName = 'Win Comparison Report')
    INSERT [ReportsMenu] ( ReportID, ReportMenuID, LEVEL, ReportName, ReportDescription, ApplicationServer, ReportArgName, ReportStatus, ShowException, ShowPowerPromoReports, SecurityRoleID, MS_ProcedureUsed, Client,ExportExcel,XMLName )
    SELECT 68, 1, 2, 'Win Comparison Report', 'Management Reports', 'Enterprise', 'WinComparisonReport', 1, 0, 'FALSE', 1, 'rsp_Report_WinComparison', NULL,1,'WinComparisonReport.xml'
UPDATE [ReportsMenu] 
SET    
	ReportName = 'Win Comparison Report',
	ReportArgName = 'WinComparisonReport',
	MS_ProcedureUsed = 'rsp_Report_WinComparison',
	IsTimeRequired = 0
WHERE ReportArgName = 'WinComparisonReport'

IF NOT EXISTS(SELECT 1 FROM [ReportsMenu] WHERE ReportName = 'Stacker Level Details Report')
    INSERT [ReportsMenu] ( ReportID, ReportMenuID, LEVEL, ReportName, ReportDescription, ApplicationServer, ReportArgName, ReportStatus, ShowException, ShowPowerPromoReports, SecurityRoleID, MS_ProcedureUsed, Client )
    SELECT 69, 1, 2, 'Stacker Level Details Report', 'Management Reports', 'Enterprise', 'StackerLevelDetailsReport', 1, 1, 'FALSE', 1, 'rsp_StackerLevelDetails', NULL
UPDATE [ReportsMenu] 
SET    
	ReportName = 'Stacker Level Details Report',
	ReportArgName = 'StackerLevelDetailsReport',
	MS_ProcedureUsed = 'rsp_StackerLevelDetails',
	IsTimeRequired = 0
WHERE ReportArgName = 'StackerLevelDetailsReport'

IF NOT EXISTS(SELECT 1 FROM [ReportsMenu] WHERE ReportName = 'Drop Schedule Details Report')
    INSERT [ReportsMenu] ( ReportID, ReportMenuID, LEVEL, ReportName, ReportDescription, ApplicationServer, ReportArgName, ReportStatus, ShowException, ShowPowerPromoReports, SecurityRoleID, MS_ProcedureUsed, Client )
    SELECT 70, 1, 2, 'Drop Schedule Details Report', 'Management Reports', 'Enterprise', 'DropScheduleDetails', 1, 1, 'FALSE', 1, 'rsp_StackerLevelDetails', NULL
UPDATE [ReportsMenu] 
SET    
	ReportName = 'Drop Schedule Details Report',
	ReportArgName = 'DropScheduleDetails',
	MS_ProcedureUsed = 'rsp_StackerLevelDetails',
	IsTimeRequired = 0
WHERE ReportArgName = 'DropScheduleDetails'

IF NOT EXISTS(SELECT 1 FROM [ReportsMenu] WHERE ReportName = 'Employee Card Sessions Report')
    INSERT [ReportsMenu] ( ReportID, ReportMenuID, LEVEL, ReportName, ReportDescription, ApplicationServer, ReportArgName, ReportStatus, ShowException, ShowPowerPromoReports, SecurityRoleID, MS_ProcedureUsed, Client )
    SELECT 71, 1, 2, 'Employee Card Sessions Report', 'Management Reports', 'Enterprise', 'EmployeeCardSessionsReport', 1, 1, 'FALSE', 1, 'rsp_GetEmployeeCardSessions', NULL
UPDATE [ReportsMenu] 
SET    
	ReportName = 'Employee Card Sessions Report',
	ReportArgName = 'EmployeeCardSessionsReport',
	MS_ProcedureUsed = 'rsp_GetEmployeeCardSessions',
	IsTimeRequired = 1
WHERE ReportArgName = 'EmployeeCardSessionsReport'

IF NOT EXISTS(SELECT 1 FROM [ReportsMenu] WHERE ReportName = 'Employee Card List Report')
    INSERT [ReportsMenu] ( ReportID, ReportMenuID, LEVEL, ReportName, ReportDescription, ApplicationServer, ReportArgName, ReportStatus, ShowException, ShowPowerPromoReports, SecurityRoleID, MS_ProcedureUsed, Client )
    SELECT 72, 1, 2, 'Employee Card List Report', 'Management Reports', 'Enterprise', 'EmployeeCardListReport', 1, 1, 'FALSE', 1, 'rsp_GetEmployeeCardList', NULL
UPDATE [ReportsMenu] 
SET    
	ReportName = 'Employee Card List Report',
	ReportArgName = 'EmployeeCardListReport',
	MS_ProcedureUsed = 'rsp_GetEmployeeCardList',
	IsTimeRequired = 0
WHERE ReportArgName = 'EmployeeCardListReport'

IF NOT EXISTS(SELECT 1 FROM [ReportsMenu] WHERE ReportName = 'Site Licensing Reports')
    INSERT [ReportsMenu] ( ReportID, ReportMenuID, LEVEL, ReportName, ReportDescription, ApplicationServer, ReportArgName, ReportStatus, ShowException, ShowPowerPromoReports, SecurityRoleID, MS_ProcedureUsed, Client )
    SELECT 73, NULL, 1, 'Site Licensing Reports', 'Main Menu', 'Enterprise', NULL, 1, NULL, NULL, 1, NULL, NULL
UPDATE [ReportsMenu] 
SET    
	ReportName = 'Site Licensing Reports',
	ReportArgName = NULL,
	MS_ProcedureUsed = NULL,
	IsTimeRequired = 0
WHERE ReportName = 'Site Licensing Reports'

IF NOT EXISTS(SELECT 1 FROM [ReportsMenu] WHERE ReportName = 'License History Report')
    INSERT [ReportsMenu] ( ReportID, ReportMenuID, LEVEL, ReportName, ReportDescription, ApplicationServer, ReportArgName, ReportStatus, ShowException, ShowPowerPromoReports, SecurityRoleID, MS_ProcedureUsed, Client )
    SELECT 74, 73, 2, 'License History Report', 'Site License Reports', 'Enterprise', 'LicenseHistoryReport', 1, 0, 'FALSE', 1, 'rsp_REPORT_SiteLicenseHistory', NULL
UPDATE [ReportsMenu] 
SET    
	ReportName = 'License History Report',
	ReportArgName = 'LicenseHistoryReport',
	MS_ProcedureUsed = 'rsp_REPORT_SiteLicenseHistory',
	IsTimeRequired = 0
WHERE ReportArgName = 'LicenseHistoryReport'

IF NOT EXISTS(SELECT 1 FROM [ReportsMenu] WHERE ReportName = 'Slot Enrollment Report')
    INSERT [ReportsMenu] ( ReportID, ReportMenuID, LEVEL, ReportName, ReportDescription, ApplicationServer, ReportArgName, ReportStatus, ShowException, ShowPowerPromoReports, SecurityRoleID, MS_ProcedureUsed, Client )
    SELECT 75, 1, 2, 'Slot Enrollment Report', 'Management Reports', 'Enterprise', 'SlotEnrollmentReport', 1, 0, 'FALSE', 1, 'rsp_REPORT_SlotEnrollment', NULL
UPDATE [ReportsMenu] 
SET    
	ReportName = 'Slot Enrollment Report',
	ReportArgName = 'SlotEnrollmentReport',
	MS_ProcedureUsed = 'rsp_REPORT_SlotEnrollment',
	IsTimeRequired = 0
WHERE ReportArgName = 'SlotEnrollmentReport'

IF NOT EXISTS(SELECT 1 FROM [ReportsMenu] WHERE ReportName = 'License Expiry Report')
    INSERT [ReportsMenu] ( ReportID, ReportMenuID, LEVEL, ReportName, ReportDescription, ApplicationServer, ReportArgName, ReportStatus, ShowException, ShowPowerPromoReports, SecurityRoleID, MS_ProcedureUsed, Client )
    SELECT 76, 73, 2, 'License Expiry Report', 'Site License Reports', 'Enterprise', 'LicenseExpiryReport', 1, 0, 'FALSE', 1, 'rsp_REPORT_LicenseExpiry', NULL
UPDATE [ReportsMenu] 
SET    
	ReportName = 'License Expiry Report',
	ReportArgName = 'LicenseExpiryReport',
	MS_ProcedureUsed = 'rsp_REPORT_LicenseExpiry',
	IsTimeRequired = 0
WHERE ReportArgName = 'LicenseExpiryReport'


IF NOT EXISTS (SELECT 1 FROM [ReportsMenu] WHERE ReportName = 'Total Funds In Summary Report')
	INSERT INTO [Enterprise].[dbo].[ReportsMenu]([ReportID],[ReportMenuID],[Level],[ReportName],[ReportDescription],[ApplicationServer],[ReportArgName]
	,[ReportStatus],[ShowException],[ShowPowerPromoReports],[SecurityRoleID],[MS_ProcedureUsed],[Client])VALUES(79,87,2,'Total Funds In Summary Report','Vault Reports','Enterprise','TotalFundsIn',1,0,'FALSE',1,'rsp_Report_TotalFundsIn',NULL)
UPDATE [ReportsMenu] 
SET    
	ReportName = 'Total Funds In Summary Report',
	ReportArgName = 'TotalFundsIn',
	MS_ProcedureUsed = 'rsp_Report_TotalFundsIn',
	IsTimeRequired = 0
WHERE ReportArgName = 'TotalFundsIn'


IF NOT EXISTS (SELECT 1 FROM [ReportsMenu] WHERE ReportName = 'Total Funds In Detail Report')
	INSERT INTO [Enterprise].[dbo].[ReportsMenu]([ReportID],[ReportMenuID],[Level],[ReportName],[ReportDescription],[ApplicationServer],[ReportArgName]
	,[ReportStatus],[ShowException],[ShowPowerPromoReports],[SecurityRoleID],[MS_ProcedureUsed],[Client])VALUES(80,87,2,'Total Funds In Detail Report','Vault Reports','Enterprise','TotalFundsInDetails',1,0,'FALSE',1,'rsp_Report_TotalFundsInDetails',NULL)
UPDATE [ReportsMenu] 
SET    
	ReportName = 'Total Funds In Detail Report',
	ReportArgName = 'TotalFundsInDetails',
	MS_ProcedureUsed = 'rsp_Report_TotalFundsInDetails',
	IsTimeRequired = 0
WHERE ReportArgName = 'TotalFundsInDetails'


IF NOT EXISTS (SELECT 1 FROM [ReportsMenu] WHERE ReportName = 'Cash Dispenser Inventory Status Report')
	INSERT INTO [Enterprise].[dbo].[ReportsMenu]([ReportID],[ReportMenuID],[Level],[ReportName],[ReportDescription],[ApplicationServer],[ReportArgName]
	,[ReportStatus],[ShowException],[ShowPowerPromoReports],[SecurityRoleID],[MS_ProcedureUsed],[Client])VALUES(81,87,2,'Cash Dispenser Inventory Status Report','Vault Reports','Enterprise','CashDispenserInventoryStatusReport',1,0,'FALSE',1,'rsp_Report_VaultBalanceReport',NULL)
UPDATE [ReportsMenu]
SET    IsTimeRequired=0
WHERE  ReportID = 92
UPDATE [ReportsMenu] 
SET    
	ReportName = 'Cash Dispenser Inventory Status Report',
	ReportArgName = 'CashDispenserInventoryStatusReport',
	MS_ProcedureUsed = 'rsp_Report_VaultBalanceReport',
	IsTimeRequired = 0
WHERE ReportArgName = 'CashDispenserInventoryStatusReport'


IF NOT EXISTS(SELECT 1 FROM [ReportsMenu] WHERE ReportName = 'Game Capping Reports')
    INSERT [ReportsMenu] ( ReportID, ReportMenuID, LEVEL, ReportName, ReportDescription, ApplicationServer, ReportArgName, ReportStatus, ShowException, ShowPowerPromoReports, SecurityRoleID, MS_ProcedureUsed, Client )
    SELECT 82, NULL, 1, 'Game Capping Reports', 'Main Menu', 'Enterprise', NULL, 1, NULL, NULL, 0, NULL, NULL
UPDATE [ReportsMenu] 
SET    
	ReportName = 'Game Capping Reports',
	ReportArgName = NULL,
	MS_ProcedureUsed = NULL,
	IsTimeRequired = 0
WHERE ReportName = 'Game Capping Reports'
    
IF NOT EXISTS(SELECT 1 FROM [ReportsMenu] WHERE ReportName = 'Capped Game Summary Report')
	INSERT [ReportsMenu] ( ReportID, ReportMenuID, LEVEL, ReportName, ReportDescription, ApplicationServer, ReportArgName, ReportStatus, ShowException, ShowPowerPromoReports, SecurityRoleID, MS_ProcedureUsed, Client )
	SELECT 83, 82, 2, 'Capped Game Summary Report', 'Game Capping Reports', 'Enterprise', 'CappedGameSummaryReport', 1, 0, 'FALSE', 1, 'rsp_Report_CappedGameSummaryReport', NULL
UPDATE [ReportsMenu] 
SET    
	ReportName = 'Capped Game Summary Report',
	ReportArgName = 'CappedGameSummaryReport',
	MS_ProcedureUsed = 'rsp_Report_CappedGameSummaryReport',
	IsTimeRequired = 1
WHERE ReportArgName = 'CappedGameSummaryReport'
      
IF NOT EXISTS(SELECT 1 FROM [ReportsMenu] WHERE ReportName = 'Capped Game List Report')
	INSERT [ReportsMenu] ( ReportID, ReportMenuID, LEVEL, ReportName, ReportDescription, ApplicationServer, ReportArgName, ReportStatus, ShowException, ShowPowerPromoReports, SecurityRoleID, MS_ProcedureUsed, Client )
	SELECT 84, 82, 2, 'Capped Game List Report', 'Game Capping Reports', 'Enterprise', 'CappedGameListReport', 1, 0, 'FALSE', 1, 'rsp_Report_CappedGameListReport', NULL
UPDATE [ReportsMenu] 
SET    
	ReportName = 'Capped Game List Report',
	ReportArgName = 'CappedGameListReport',
	MS_ProcedureUsed = 'rsp_Report_CappedGameListReport',
	IsTimeRequired = 1
WHERE ReportArgName = 'CappedGameListReport'
      
GO	
IF NOT EXISTS(SELECT 1 FROM [ReportsMenu] WHERE ReportName = 'Promotional Summary Report')
    INSERT [ReportsMenu] ( ReportID, ReportMenuID, LEVEL, ReportName, ReportDescription, ApplicationServer, ReportArgName, ReportStatus, ShowException, ShowPowerPromoReports, SecurityRoleID, MS_ProcedureUsed, Client )
    SELECT 85, 43, 2, 'Promotional Summary Report', 'Voucher Reports', 'Enterprise', 'PromotionalSummaryReport', 1, 0, 'FALSE', 2, 'rsp_SelectPromotionHistoryReport', NULL
GO
UPDATE [ReportsMenu] 
SET    
	ReportName = 'Promotional Summary Report',
	ReportArgName = 'PromotionalSummaryReport',
	MS_ProcedureUsed = 'rsp_SelectPromotionHistoryReport',
	IsTimeRequired = 1
WHERE ReportArgName = 'PromotionalSummaryReport'


IF NOT EXISTS(SELECT 1 FROM [ReportsMenu] WHERE ReportName = 'Promotional Voucher Listing Report')
    INSERT [ReportsMenu] ( ReportID, ReportMenuID, LEVEL, ReportName, ReportDescription, ApplicationServer, ReportArgName, ReportStatus, ShowException, ShowPowerPromoReports, SecurityRoleID, MS_ProcedureUsed, Client )
    SELECT 86, 43, 2, 'Promotional Voucher Listing Report', 'Voucher Reports', 'Enterprise', 'PromotionalVoucherListingReport', 1, 0, 'FALSE', 2, 'rsp_PromotionalVoucherListingReport', NULL
GO
UPDATE [ReportsMenu] 
SET    
	ReportName = 'Promotional Voucher Listing Report',
	ReportArgName = 'PromotionalVoucherListingReport',
	MS_ProcedureUsed = 'rsp_PromotionalVoucherListingReport',
	IsTimeRequired = 1
WHERE ReportArgName = 'PromotionalVoucherListingReport'

IF NOT EXISTS(SELECT 1 FROM [ReportsMenu] WHERE ReportName = 'Vault Reports')
	INSERT INTO [Enterprise].[dbo].[ReportsMenu] ([ReportID],[ReportMenuID],[Level],[ReportName],[ReportDescription],[ApplicationServer],[ReportArgName]
	,[ReportStatus],[ShowException],[ShowPowerPromoReports],[MS_ProcedureUsed],[SecurityRoleID],Client)VALUES  (87,null,1,'Vault Reports','Main Menu','Enterprise',Null,1,null,null,null,0,null)
GO
UPDATE [ReportsMenu] 
SET    
	ReportName = 'Vault Reports',
	ReportArgName = NULL,
	MS_ProcedureUsed = NULL,
	IsTimeRequired = 0
WHERE ReportName = 'Vault Reports'

IF NOT EXISTS (SELECT 1 FROM [ReportsMenu] WHERE ReportName = 'Cash Dispenser Configuration Details')
	INSERT INTO [Enterprise].[dbo].[ReportsMenu]([ReportID],[ReportMenuID],[Level],[ReportName],[ReportDescription],[ApplicationServer],[ReportArgName]
	,[ReportStatus],[ShowException],[ShowPowerPromoReports],[SecurityRoleID],[MS_ProcedureUsed],[Client])VALUES(88,87,2,'Cash Dispenser Configuration Details','Vault Reports','Enterprise','CashDispenserConfigurationDetailsReport',1,0,'FALSE',1,'rsp_Report_VaultConfigDetails',NULL)	
GO
UPDATE [ReportsMenu] 
SET    
	ReportName = 'Cash Dispenser Configuration Details',
	ReportArgName = 'CashDispenserConfigurationDetailsReport',
	MS_ProcedureUsed = 'rsp_Report_VaultConfigDetails',
	IsTimeRequired = 1
WHERE ReportArgName = 'CashDispenserConfigurationDetailsReport'

IF NOT EXISTS (SELECT 1 FROM [ReportsMenu] WHERE ReportName = 'Cash Dispenser Cassettes Inventory Status' OR ReportName = 'CashDispenser Cassettes Inventory Status')
	INSERT INTO [Enterprise].[dbo].[ReportsMenu]([ReportID],[ReportMenuID],[Level],[ReportName],[ReportDescription],[ApplicationServer],[ReportArgName]
	,[ReportStatus],[ShowException],[ShowPowerPromoReports],[SecurityRoleID],[MS_ProcedureUsed],[Client])VALUES(89,87,2,'Cash Dispenser Cassettes Inventory Status','Vault Reports','Enterprise','CashDispenserCassettesInventoryStatus',1,0,'FALSE',1,'rsp_Report_VaultCassettesInventoryStatus',NULL)
GO
UPDATE [ReportsMenu] 
SET    
	ReportName = 'Cash Dispenser Cassettes Inventory Status',
	ReportArgName = 'CashDispenserCassettesInventoryStatus',
	MS_ProcedureUsed = 'rsp_Report_VaultCassettesInventoryStatus',
	IsTimeRequired = 1
WHERE ReportArgName = 'CashDispenserCassettesInventoryStatus'

IF NOT EXISTS (SELECT 1 FROM [ReportsMenu] WHERE ReportName = 'Cash Dispenser Drop Report')
	INSERT INTO [Enterprise].[dbo].[ReportsMenu]([ReportID],[ReportMenuID],[Level],[ReportName],[ReportDescription],[ApplicationServer],[ReportArgName]
	,[ReportStatus],[ShowException],[ShowPowerPromoReports],[SecurityRoleID],[MS_ProcedureUsed],[Client])VALUES(90,87,2,'Cash Dispenser Drop Report','Vault Reports','Enterprise','CashDispenserDropReport',1,0,'FALSE',1,'rsp_Report_VaultDrop',NULL)	
GO
UPDATE [ReportsMenu] 
SET    
	ReportName = 'Cash Dispenser Drop Report',
	ReportArgName = 'CashDispenserDropReport',
	MS_ProcedureUsed = 'rsp_Report_VaultDrop',
	IsTimeRequired = 1
WHERE ReportArgName = 'CashDispenserDropReport'

IF NOT EXISTS (SELECT 1 FROM [ReportsMenu] WHERE ReportName = 'Cash Dispenser Level Details')
	INSERT INTO [Enterprise].[dbo].[ReportsMenu]([ReportID],[ReportMenuID],[Level],[ReportName],[ReportDescription],[ApplicationServer],[ReportArgName]
	,[ReportStatus],[ShowException],[ShowPowerPromoReports],[SecurityRoleID],[MS_ProcedureUsed],[Client])VALUES(91,87,2,'Cash Dispenser Level Details','Vault Reports','Enterprise','CashDispenserLevelDetails',1,0,'FALSE',1,'rsp_REPORT_VaultLevelDetails',NULL)

UPDATE [ReportsMenu] 
SET    
	ReportName = 'Cash Dispenser Level Details',
	ReportArgName = 'CashDispenserLevelDetails',
	MS_ProcedureUsed = 'rsp_REPORT_VaultLevelDetails',
	IsTimeRequired = 1
WHERE ReportArgName = 'CashDispenserLevelDetails'


IF NOT EXISTS (SELECT 1 FROM [ReportsMenu] WHERE ReportName = 'Cash Dispenser Transaction Details')
	INSERT INTO [Enterprise].[dbo].[ReportsMenu]([ReportID],[ReportMenuID],[Level],[ReportName],[ReportDescription],[ApplicationServer],[ReportArgName]
	,[ReportStatus],[ShowException],[ShowPowerPromoReports],[SecurityRoleID],[MS_ProcedureUsed],[Client])VALUES(92,87,2,'Cash Dispenser Transaction Details','Vault Reports','Enterprise','CashDispenserTransactionDetails',1,0,'FALSE',1,'rsp_Report_VaultTransactionsDetails',NULL)
UPDATE [ReportsMenu] 
SET    
	ReportName = 'Cash Dispenser Transaction Details',
	ReportArgName = 'CashDispenserTransactionDetails',
	MS_ProcedureUsed = 'rsp_Report_VaultTransactionsDetails',
	IsTimeRequired = 1
WHERE ReportArgName = 'CashDispenserTransactionDetails'

IF NOT EXISTS (SELECT 1 FROM [ReportsMenu] WHERE ReportName = 'Cash Dispenser Variance Report')
	INSERT INTO [Enterprise].[dbo].[ReportsMenu]([ReportID],[ReportMenuID],[Level],[ReportName],[ReportDescription],[ApplicationServer],[ReportArgName]
	,[ReportStatus],[ShowException],[ShowPowerPromoReports],[SecurityRoleID],[MS_ProcedureUsed],[Client])VALUES(93,87,2,'Cash Dispenser Variance Report','Vault Reports','Enterprise','CashDispenserVarianceReport',1,0,'FALSE',1,'rsp_Report_VaultVariance',NULL)
UPDATE [ReportsMenu] 
SET    
	ReportName = 'Cash Dispenser Variance Report',
	ReportArgName = 'CashDispenserVarianceReport',
	MS_ProcedureUsed = 'rsp_Report_VaultVariance',
	IsTimeRequired = 1
WHERE ReportArgName = 'CashDispenserVarianceReport'

IF NOT EXISTS (SELECT 1 FROM [ReportsMenu] WHERE ReportName = 'Cash Dispenser Cassette Accounting Detail')
	INSERT INTO [Enterprise].[dbo].[ReportsMenu]([ReportID],[ReportMenuID],[Level],[ReportName],[ReportDescription],[ApplicationServer],[ReportArgName]
	,[ReportStatus],[ShowException],[ShowPowerPromoReports],[SecurityRoleID],[MS_ProcedureUsed],[Client])VALUES(94,87,2,'Cash Dispenser Cassette Accounting Detail','Vault Reports','Enterprise','CashDispenserCassetteAccountingDetail',1,0,'FALSE',1,'rsp_Report_VaultCassetteTransactions',NULL)	
GO
UPDATE [ReportsMenu] 
SET    
	ReportName = 'Cash Dispenser Cassette Accounting Detail',
	ReportArgName = 'CashDispenserCassetteAccountingDetail',
	MS_ProcedureUsed = 'rsp_Report_VaultCassetteTransactions',
	IsTimeRequired = 1
WHERE ReportArgName = 'CashDispenserCassetteAccountingDetail'

IF NOT EXISTS(SELECT 1 FROM [ReportsMenu] WHERE ReportName = 'Cash Dispenser Accounting Report')
	INSERT INTO [Enterprise].[dbo].[ReportsMenu]([ReportID],[ReportMenuID],[Level],[ReportName],[ReportDescription],[ApplicationServer],[ReportArgName]
	,[ReportStatus],[ShowException],[ShowPowerPromoReports],[SecurityRoleID],[MS_ProcedureUsed],[Client])VALUES(95,87,2,'Cash Dispenser Accounting Report','Vault Reports','Enterprise','CashDispenserAccounting',1,0,'FALSE',1,'rsp_Report_VaultTransactions',NULL)	


UPDATE [ReportsMenu] 
SET    
	ReportName = 'Cash Dispenser Accounting Report',
	ReportArgName = 'CashDispenserAccounting',
	MS_ProcedureUsed = 'rsp_Report_VaultTransactions',
	IsTimeRequired = 1
WHERE ReportArgName = 'CashDispenserAccounting'


IF NOT EXISTS(SELECT 1 FROM [ReportsMenu] WHERE ReportName = 'Fixed Expense Details Report')
    INSERT [ReportsMenu] ( ReportID, ReportMenuID, LEVEL, ReportName, ReportDescription, ApplicationServer, ReportArgName, ReportStatus, ShowException, ShowPowerPromoReports, SecurityRoleID, MS_ProcedureUsed, Client )
    SELECT 96, 1, 2, 'Fixed Expense Details Report', 'Management Reports', 'Enterprise', 'LiquidationExpenseReport', 1, 1, 'FALSE', 1, 'rsp_LiquidationExpenseReport', NULL
UPDATE [ReportsMenu] 
SET    
	ReportName = 'Fixed Expense Details Report',
	ReportArgName = 'LiquidationExpenseReport',
	MS_ProcedureUsed = 'rsp_LiquidationExpenseReport',
	IsTimeRequired = 1
WHERE ReportArgName = 'LiquidationExpenseReport'

IF NOT EXISTS(SELECT 1 FROM [ReportsMenu] WHERE ReportName = 'Period-End Liquidation Revenue Report')
    INSERT [ReportsMenu] ( ReportID, ReportMenuID, LEVEL, ReportName, ReportDescription, ApplicationServer, ReportArgName, ReportStatus, ShowException, ShowPowerPromoReports, SecurityRoleID, MS_ProcedureUsed, Client )
    SELECT 97, 1, 2, 'Period-End Liquidation Revenue Report', 'Management Reports', 'Enterprise', 'PeriodEndLiquidationRevenueReport', 1, 1, 'FALSE', 1, 'rsp_PeriodEndLiquidationRevenueReport', NULL
UPDATE [ReportsMenu] 
SET    
	ReportName = 'Period-End Liquidation Revenue Report',
	ReportArgName = 'PeriodEndLiquidationRevenueReport',
	MS_ProcedureUsed = 'rsp_PeriodEndLiquidationRevenueReport',
	IsTimeRequired = 1
WHERE ReportArgName = 'PeriodEndLiquidationRevenueReport'

GO

IF NOT EXISTS(SELECT 1 FROM [ReportsMenu] WHERE [ReportArgName] = 'DropBasedResultsReport')
BEGIN
	 DECLARE @ReportID INT
	 DECLARE @ManagementRootID INT

	 SELECT @ReportID=MAX([ReportID])+1 FROM [dbo].[ReportsMenu]
	 SELECT @ManagementRootID = ReportID FROM [dbo].[ReportsMenu] WHERE REPORTNAME = 'Management Reports'
	 
    INSERT [ReportsMenu] ( ReportID, ReportMenuID, LEVEL, ReportName, ReportDescription, ApplicationServer, ReportArgName, ReportStatus, ShowException, ShowPowerPromoReports, SecurityRoleID, MS_ProcedureUsed, Client )
    SELECT @ReportID, @ManagementRootID, 2, 'Drop Based Results Report', 'Management Reports', 'Enterprise', 'DropBasedResultsReport', 1, 1, 'FALSE', 1, 'rsp_Report_DropBasedResults_ENT', NULL
END
ELSE
BEGIN
	UPDATE [ReportsMenu] 
SET    
	ReportName = 'Drop Based Results Report',
	ReportArgName = 'DropBasedResultsReport',
	MS_ProcedureUsed = 'rsp_Report_DropBasedResults_ENT',
	IsTimeRequired = 1
WHERE ReportArgName = 'DropBasedResultsReport'
END
GO
--(EP4 Changes Ends)

--IsTimeRequired = True for below reports
--'MAJORPRIZESREPORT',
--'EFTQuestionableTransactionsReport',
--'EFTSlotActivityReport',
--'EFTSlotActivityCumulativeReport',
--'VoucherCouponLiabilityReport',
--'RedeemedTicketByDeviceReport',
--'ExpiredVoucherCouponReport',
--'VoucherListingReport',
--'JackpotSlipSummaryReport',
--'AUDITTRAIL',
--'CoinInbyPaytableReport',
--'MultiDenomSlotDetailReport',
--'MultiGameSlotDetailReport',
--'MGMDDenomPerformance',
--'GamePerformanceReport',
--'GameReport',
--'LotteryRevenue_US',
--'CrossPropertyLiabilityTransferSummaryReport',
--'CrossPropertyLiabilityTransferDetailsReport',
--'CrossPropertyTicketAnalysisReport',
--'EmployeeCardSessionsReport',
--'CappedGameSummaryReport',
--'CappedGameListReport',
--'PromotionalSummaryReport',
--'PromotionalVoucherListingReport',
--'CashDispenserCassetteAccountingDetail',
--'CashDispenserConfigurationDetailsReport',
--'CashDispenserCassettesInventoryStatus',
--'CashDispenserDropReport',
--'CashDispenserLevelDetails',
--'CashDispenserTransactionDetails',
--'CashDispenserVarianceReport',
--'LiquidationExpenseReport',
--'PeriodEndLiquidationRevenueReport',
--'CashDispenserAccounting'