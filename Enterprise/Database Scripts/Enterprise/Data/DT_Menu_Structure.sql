/************************************************************
 * Created by Bally Technologies © 2011
 * Time: 05/04/13 11:29:33 PM
 ************************************************************/
USE [Enterprise]
GO

IF NOT EXISTS(SELECT 1 FROM [Menu_Structure]WHERE MS_Caption = 'Management')
    INSERT [Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status )
    SELECT 1, 'M', 2, NULL, NULL, 'Management', NULL, NULL, 'Enterprise Reports', 'Active'
ELSE
    UPDATE [Menu_Structure]
    SET    MS_Order = 1, MS_Type = 'M', MS_Menu_ID = 2, MS_Application = NULL, MS_Additional = NULL, MS_AppID = NULL, MS_IconName = NULL, MS_Heading = 'Enterprise Reports', MS_Status = 'Active'
    WHERE  MS_Caption = 'Management'

IF NOT EXISTS(SELECT 1 FROM [Menu_Structure]WHERE MS_Caption = 'Cube')
    INSERT [Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status )
    SELECT 1, 'M', 3, NULL, NULL, 'Cube', NULL, NULL, 'Enterprise Reports', 'Active'
ELSE
    UPDATE [Menu_Structure]
    SET    MS_Order = 1, MS_Type = 'M', MS_Menu_ID = 3, MS_Application = NULL, MS_Additional = NULL, MS_AppID = NULL, MS_IconName = NULL, MS_Heading = 'Enterprise Reports', MS_Status = 'Active'
    WHERE  MS_Caption = 'Cube'

IF NOT EXISTS(SELECT 1 FROM [Menu_Structure]WHERE MS_Caption = 'Audit / Security')
    INSERT [Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status )
    SELECT 1, 'M', 4, NULL, NULL, 'Audit / Security', NULL, NULL, 'Enterprise Reports', 'Active'
ELSE
    UPDATE [Menu_Structure]
    SET    MS_Order = 1, MS_Type = 'M', MS_Menu_ID = 4, MS_Application = NULL, MS_Additional = NULL, MS_AppID = NULL, MS_IconName = NULL, MS_Heading = 'Enterprise Reports', MS_Status = 'Active'
    WHERE  MS_Caption = 'Audit / Security'

IF NOT EXISTS(SELECT 1 FROM [Menu_Structure]WHERE MS_Caption = 'Maintenance')
    INSERT [Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status )
    SELECT 1, 'M', 5, NULL, NULL, 'Maintenance', NULL, NULL, 'Enterprise Reports', 'Active'
ELSE
    UPDATE [Menu_Structure]
    SET    MS_Order = 1, MS_Type = 'M', MS_Menu_ID = 5, MS_Application = NULL, MS_Additional = NULL, MS_AppID = NULL, MS_IconName = NULL, MS_Heading = 'Enterprise Reports', MS_Status = 'Active'
    WHERE  MS_Caption = 'Maintenance'

IF NOT EXISTS(SELECT 1 FROM [Menu_Structure]WHERE MS_Caption = 'Service')
    INSERT [Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status )
    SELECT 1, 'M', 6, NULL, NULL, 'Service', NULL, NULL, 'Enterprise Reports', 'Active'
ELSE
    UPDATE [Menu_Structure]
    SET    MS_Order = 1, MS_Type = 'M', MS_Menu_ID = 6, MS_Application = NULL, MS_Additional = NULL, MS_AppID = NULL, MS_IconName = NULL, MS_Heading = 'Enterprise Reports', MS_Status = 'Active'
    WHERE  MS_Caption = 'Service'

IF NOT EXISTS(SELECT 1 FROM [Menu_Structure]WHERE MS_Caption = 'financials')
    INSERT [Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status )
    SELECT 1, 'M', 7, NULL, NULL, 'financials', NULL, NULL, 'Enterprise Reports', 'Active'
ELSE
    UPDATE [Menu_Structure]
    SET    MS_Order = 1, MS_Type = 'M', MS_Menu_ID = 7, MS_Application = NULL, MS_Additional = NULL, MS_AppID = NULL, MS_IconName = NULL, MS_Heading = 'Enterprise Reports', MS_Status = 'Active'
    WHERE  MS_Caption = 'financials'

IF NOT EXISTS(SELECT 1 FROM [Menu_Structure]WHERE MS_Caption = 'Machine Meter Analysis')
    INSERT [Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status )
    SELECT 2, 'A', 0, 'BGSEnterpriseSiteMachineAssetReport.EXE', NULL, 'Machine Meter Analysis', 'R01', '', 'Management Reports', 'Active'
ELSE
    UPDATE [Menu_Structure]
    SET    MS_Order = 2, MS_Type = 'A', MS_Menu_ID = 0, MS_Application = 'BGSEnterpriseSiteMachineAssetReport.EXE', MS_Additional = NULL, MS_AppID = 'R01', MS_IconName = '', MS_Heading = 'Management Reports', MS_Status = 'Active'
    WHERE  MS_Caption = 'Machine Meter Analysis'

IF NOT EXISTS(SELECT 1 FROM [Menu_Structure]WHERE MS_Caption = 'Meter Summary Report')
    INSERT [Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status )
    SELECT 2, 'A', 0, 'HQSummaryFlashReport.EXE', NULL, 'Meter Summary Report', 'R02', '', 'Management Reports', 'Active'
ELSE
    UPDATE [Menu_Structure]
    SET    MS_Order = 2, MS_Type = 'A', MS_Menu_ID = 0, MS_Application = 'HQSummaryFlashReport.EXE', MS_Additional = NULL, MS_AppID = 'R02', MS_IconName = '', MS_Heading = 'Management Reports', MS_Status = 'Active'
    WHERE  MS_Caption = 'Meter Summary Report'

IF NOT EXISTS(SELECT 1 FROM [Menu_Structure]WHERE MS_Caption = 'Meter Detailed Report')
    INSERT [Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status )
    SELECT 2, 'A', 0, 'HQDailyReadReport.EXE', NULL, 'Meter Detailed Report', 'R03', '', 'Management Reports', 'Active'
ELSE
    UPDATE [Menu_Structure]
    SET    MS_Order = 2, MS_Type = 'A', MS_Menu_ID = 0, MS_Application = 'HQDailyReadReport.EXE', MS_Additional = NULL, MS_AppID = 'R03', MS_IconName = '', MS_Heading = 'Management Reports', MS_Status = 'Active'
    WHERE  MS_Caption = 'Meter Detailed Report'

IF NOT EXISTS(SELECT 1 FROM [Menu_Structure]WHERE MS_Caption = 'A/C Win/Loss Report')
    INSERT [Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status )
    SELECT 2, 'A', 0, 'HQBankingReport.EXE', NULL, 'A/C Win/Loss Report', 'R04', '', 'Management Reports', 'Active'
ELSE
    UPDATE [Menu_Structure]
    SET    MS_Order = 2, MS_Type = 'A', MS_Menu_ID = 0, MS_Application = 'HQBankingReport.EXE', MS_Additional = NULL, MS_AppID = 'R04', MS_IconName = '', MS_Heading = 'Management Reports', MS_Status = 'Active'
    WHERE  MS_Caption = 'A/C Win/Loss Report'

IF NOT EXISTS(SELECT 1 FROM [Menu_Structure]WHERE MS_Caption = 'A/C Summary Report')
    INSERT [Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status )
    SELECT 2, 'A', 0, 'HQACSummaryReport.EXE', 'SUMMARY', 'A/C Summary Report', 'R05', '', 'Management Reports', 'Active'
ELSE
    UPDATE [Menu_Structure]
    SET    MS_Order = 2, MS_Type = 'A', MS_Menu_ID = 0, MS_Application = 'HQACSummaryReport.EXE', MS_Additional = 'SUMMARY', MS_AppID = 'R05', MS_IconName = '', MS_Heading = 'Management Reports', MS_Status = 'Active'
    WHERE  MS_Caption = 'A/C Summary Report'

IF NOT EXISTS(SELECT 1 FROM [Menu_Structure]WHERE MS_Caption = 'A/C Detailed Report')
    INSERT [Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status )
    SELECT 2, 'A', 0, 'HQACSummaryReport.EXE', NULL, 'A/C Detailed Report', 'R06', '', 'Management Reports', 'Active'
ELSE
    UPDATE [Menu_Structure]
    SET    MS_Order = 2, MS_Type = 'A', MS_Menu_ID = 0, MS_Application = 'HQACSummaryReport.EXE', MS_Additional = NULL, MS_AppID = 'R06', MS_IconName = '', MS_Heading = 'Management Reports', MS_Status = 'Active'
    WHERE  MS_Caption = 'A/C Detailed Report'

IF NOT EXISTS(SELECT 1 FROM [Menu_Structure]WHERE MS_Caption = 'Depreciation Report')
    INSERT [Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status )
    SELECT 2, 'A', 0, 'DepreciationDetailReport.EXE', NULL, 'Depreciation Report', 'R07', '', 'Management Reports', 'Active'
ELSE
    UPDATE [Menu_Structure]
    SET    MS_Order = 2, MS_Type = 'A', MS_Menu_ID = 0, MS_Application = 'DepreciationDetailReport.EXE', MS_Additional = NULL, MS_AppID = 'R07', MS_IconName = '', MS_Heading = 'Management Reports', MS_Status = 'Active'
    WHERE  MS_Caption = 'Depreciation Report'

IF NOT EXISTS(SELECT 1 FROM [Menu_Structure]WHERE MS_Caption = 'Accounting Win/Loss')
    INSERT [Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status )
    SELECT 3, 'A', 0, 'cubeCollectionData.Max', NULL, 'Accounting Win/Loss', 'R08', '', 'Cube Reports', 'Active'
ELSE
    UPDATE [Menu_Structure]
    SET    MS_Order = 3, MS_Type = 'A', MS_Menu_ID = 0, MS_Application = 'cubeCollectionData.Max', MS_Additional = NULL, MS_AppID = 'R08', MS_IconName = '', MS_Heading = 'Cube Reports', MS_Status = 'Active'
    WHERE  MS_Caption = 'Accounting Win/Loss'

IF NOT EXISTS(SELECT 1 FROM [Menu_Structure]WHERE MS_Caption = 'Daily Read Data')
    INSERT [Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status )
    SELECT 3, 'A', 0, 'cubeDailyReadData.Max', NULL, 'Daily Read Data', 'R09', '', 'Cube Reports', 'Active'
ELSE
    UPDATE [Menu_Structure]
    SET    MS_Order = 3, MS_Type = 'A', MS_Menu_ID = 0, MS_Application = 'cubeDailyReadData.Max', MS_Additional = NULL, MS_AppID = 'R09', MS_IconName = '', MS_Heading = 'Cube Reports', MS_Status = 'Active'
    WHERE  MS_Caption = 'Daily Read Data'

IF NOT EXISTS(SELECT 1 FROM [Menu_Structure]WHERE MS_Caption = 'Hourly Data')
    INSERT [Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status )
    SELECT 3, 'A', 0, 'cubeHourlyData.Max', NULL, 'Hourly Data', 'R10', '', 'Cube Reports', 'Active'
ELSE
    UPDATE [Menu_Structure]
    SET    MS_Order = 3, MS_Type = 'A', MS_Menu_ID = 0, MS_Application = 'cubeHourlyData.Max', MS_Additional = NULL, MS_AppID = 'R10', MS_IconName = '', MS_Heading = 'Cube Reports', MS_Status = 'Active'
    WHERE  MS_Caption = 'Hourly Data'

IF NOT EXISTS(SELECT 1 FROM [Menu_Structure]WHERE MS_Caption = 'Consolidated M/C Collection Variance')
    INSERT [Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status )
    SELECT 4, 'A', 0, 'HQConsolidatedVariance.EXE', NULL, 'Consolidated M/C Collection Variance', 'R11', '', 'Audit / Security Reports', 'Active'
ELSE
    UPDATE [Menu_Structure]
    SET    MS_Order = 4, MS_Type = 'A', MS_Menu_ID = 0, MS_Application = 'HQConsolidatedVariance.EXE', MS_Additional = NULL, MS_AppID = 'R11', MS_IconName = '', MS_Heading = 'Audit / Security Reports', MS_Status = 'Active'
    WHERE  MS_Caption = 'Consolidated M/C Collection Variance'

IF NOT EXISTS(SELECT 1 FROM [Menu_Structure]WHERE MS_Caption = 'Remedy Codes')
    INSERT [Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status )
    SELECT 6, 'A', 0, 'ServiceRemedyCodes.EXE', NULL, 'Remedy Codes', 'R12', '', 'Service Reports', 'Active'
ELSE
    UPDATE [Menu_Structure]
    SET    MS_Order = 6, MS_Type = 'A', MS_Menu_ID = 0, MS_Application = 'ServiceRemedyCodes.EXE', MS_Additional = NULL, MS_AppID = 'R12', MS_IconName = '', MS_Heading = 'Service Reports', MS_Status = 'Active'
    WHERE  MS_Caption = 'Remedy Codes'

IF NOT EXISTS(SELECT 1 FROM [Menu_Structure]WHERE MS_Caption = 'Service History')
    INSERT [Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status )
    SELECT 6, 'A', 0, 'ServiceCallList.EXE', NULL, 'Service History', 'R13', '', 'Service Reports', 'Active'
ELSE
    UPDATE [Menu_Structure]
    SET    MS_Order = 6, MS_Type = 'A', MS_Menu_ID = 0, MS_Application = 'ServiceCallList.EXE', MS_Additional = NULL, MS_AppID = 'R13', MS_IconName = '', MS_Heading = 'Service Reports', MS_Status = 'Active'
    WHERE  MS_Caption = 'Service History'

IF NOT EXISTS(SELECT 1 FROM [Menu_Structure]WHERE MS_Caption = 'Fault Groups')
    INSERT [Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status )
    SELECT 6, 'A', 0, 'ServiceFaultGroups.EXE', NULL, 'Fault Groups', 'R14', '', 'Service Reports', 'Active'
ELSE
    UPDATE [Menu_Structure]
    SET    MS_Order = 6, MS_Type = 'A', MS_Menu_ID = 0, MS_Application = 'ServiceFaultGroups.EXE', MS_Additional = NULL, MS_AppID = 'R14', MS_IconName = '', MS_Heading = 'Service Reports', MS_Status = 'Active'
    WHERE  MS_Caption = 'Fault Groups'

IF NOT EXISTS(SELECT 1 FROM [Menu_Structure]WHERE MS_Caption = 'SLA Terms')
    INSERT [Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status )
    SELECT 6, 'A', 0, 'ServiceSLATerms.EXE', NULL, 'SLA Terms', 'R15', '', 'Service Reports', 'Active'
ELSE
    UPDATE [Menu_Structure]
    SET    MS_Order = 6, MS_Type = 'A', MS_Menu_ID = 0, MS_Application = 'ServiceSLATerms.EXE', MS_Additional = NULL, MS_AppID = 'R15', MS_IconName = '', MS_Heading = 'Service Reports', MS_Status = 'Active'
    WHERE  MS_Caption = 'SLA Terms'

IF NOT EXISTS(SELECT 1 FROM [Menu_Structure]WHERE MS_Caption = 'Site Analysis')
    INSERT [Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status )
    SELECT 6, 'A', 0, 'ServiceSiteAnalysis.EXE', NULL, 'Site Analysis', 'R16', '', 'Service Reports', 'Active'
ELSE
    UPDATE [Menu_Structure]
    SET    MS_Order = 6, MS_Type = 'A', MS_Menu_ID = 0, MS_Application = 'ServiceSiteAnalysis.EXE', MS_Additional = NULL, MS_AppID = 'R16', MS_IconName = '', MS_Heading = 'Service Reports', MS_Status = 'Active'
    WHERE  MS_Caption = 'Site Analysis'

IF NOT EXISTS(SELECT 1 FROM [Menu_Structure]WHERE MS_Caption = 'Machine Analysis')
    INSERT [Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status )
    SELECT 6, 'A', 0, 'ServiceMachineAnalysis.EXE', NULL, 'Machine Analysis', 'R17', '', 'Service Reports', 'Active'
ELSE
    UPDATE [Menu_Structure]
    SET    MS_Order = 6, MS_Type = 'A', MS_Menu_ID = 0, MS_Application = 'ServiceMachineAnalysis.EXE', MS_Additional = NULL, MS_AppID = 'R17', MS_IconName = '', MS_Heading = 'Service Reports', MS_Status = 'Active'
    WHERE  MS_Caption = 'Machine Analysis'

IF NOT EXISTS(SELECT 1 FROM [Menu_Structure]WHERE MS_Caption = 'Remedy Analysis')
    INSERT [Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status )
    SELECT 6, 'A', 0, 'RemedyAnalysis.EXE', NULL, 'Remedy Analysis', 'R18', '', 'Service Reports', 'Active'
ELSE
    UPDATE [Menu_Structure]
    SET    MS_Order = 6, MS_Type = 'A', MS_Menu_ID = 0, MS_Application = 'RemedyAnalysis.EXE', MS_Additional = NULL, MS_AppID = 'R18', MS_IconName = '', MS_Heading = 'Service Reports', MS_Status = 'Active'
    WHERE  MS_Caption = 'Remedy Analysis'

IF NOT EXISTS(SELECT 1 FROM [Menu_Structure]WHERE MS_Caption = 'Statistical Analysis')
    INSERT [Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status )
    SELECT 6, 'A', 0, 'ServiceStatistics.EXE', NULL, 'Statistical Analysis', 'R19', '', 'Service Reports', 'Active'
ELSE
    UPDATE [Menu_Structure]
    SET    MS_Order = 6, MS_Type = 'A', MS_Menu_ID = 0, MS_Application = 'ServiceStatistics.EXE', MS_Additional = NULL, MS_AppID = 'R19', MS_IconName = '', MS_Heading = 'Service Reports', MS_Status = 'Active'
    WHERE  MS_Caption = 'Statistical Analysis'

IF NOT EXISTS(SELECT 1 FROM [Menu_Structure]WHERE MS_Caption = 'Live Call List')
    INSERT [Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status )
    SELECT 6, 'A', 0, 'LiveServiceCallList.EXE', NULL, 'Live Call List', 'R20', '', 'Service Reports', 'Active'
ELSE
    UPDATE [Menu_Structure]
    SET    MS_Order = 6, MS_Type = 'A', MS_Menu_ID = 0, MS_Application = 'LiveServiceCallList.EXE', MS_Additional = NULL, MS_AppID = 'R20', MS_IconName = '', MS_Heading = 'Service Reports', MS_Status = 'Active'
    WHERE  MS_Caption = 'Live Call List'

IF NOT EXISTS(SELECT 1 FROM [Menu_Structure]WHERE MS_Caption = 'Test Period End - by Product')
    INSERT [Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status )
    SELECT 7, 'A', 0, 'SupplierSummarybyProductTest.exe', NULL, 'Test Period End - by Product', 'R21', '', 'Financial Reports', 'Active'
ELSE
    UPDATE [Menu_Structure]
    SET    MS_Order = 7, MS_Type = 'A', MS_Menu_ID = 0, MS_Application = 'SupplierSummarybyProductTest.exe', MS_Additional = NULL, MS_AppID = 'R21', MS_IconName = '', MS_Heading = 'Financial Reports', MS_Status = 'Active'
    WHERE  MS_Caption = 'Test Period End - by Product'

IF NOT EXISTS(SELECT 1 FROM [Menu_Structure]WHERE MS_Caption = 'Secondary Period End - Consolidated')
    INSERT [Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status )
    SELECT 7, 'A', 0, 'SecondaryEndPeriodConsolidated.EXE', NULL, 'Secondary Period End - Consolidated', 'R22', '', 'Financial Reports', 'Active'
ELSE
    UPDATE [Menu_Structure]
    SET    MS_Order = 7, MS_Type = 'A', MS_Menu_ID = 0, MS_Application = 'SecondaryEndPeriodConsolidated.EXE', MS_Additional = NULL, MS_AppID = 'R22', MS_IconName = '', MS_Heading = 'Financial Reports', MS_Status = 'Active'
    WHERE  MS_Caption = 'Secondary Period End - Consolidated'

IF NOT EXISTS(SELECT 1 FROM [Menu_Structure]WHERE MS_Caption = 'Secondary Party Period End')
    INSERT [Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status )
    SELECT 7, 'A', 0, 'SecondaryEndPeriodReport.exe', NULL, 'Secondary Party Period End', 'R23', '', 'Financial Reports', 'Active'
ELSE
    UPDATE [Menu_Structure]
    SET    MS_Order = 7, MS_Type = 'A', MS_Menu_ID = 0, MS_Application = 'SecondaryEndPeriodReport.exe', MS_Additional = NULL, MS_AppID = 'R23', MS_IconName = '', MS_Heading = 'Financial Reports', MS_Status = 'Active'
    WHERE  MS_Caption = 'Secondary Party Period End'

IF NOT EXISTS(SELECT 1 FROM [Menu_Structure]WHERE MS_Caption = 'Site Owner Performance')
    INSERT [Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status )
    SELECT 7, 'A', 0, 'SiteOwnerPerformance.exe', NULL, 'Site Owner Performance', 'R24', '', 'Financial Reports', 'Active'
ELSE
    UPDATE [Menu_Structure]
    SET    MS_Order = 7, MS_Type = 'A', MS_Menu_ID = 0, MS_Application = 'SiteOwnerPerformance.exe', MS_Additional = NULL, MS_AppID = 'R24', MS_IconName = '', MS_Heading = 'Financial Reports', MS_Status = 'Active'
    WHERE  MS_Caption = 'Site Owner Performance'

IF NOT EXISTS(SELECT 1 FROM [Menu_Structure]WHERE MS_Caption = 'Period Input Report')
    INSERT [Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status )
    SELECT 7, 'A', 0, 'EndPeriodInputReport.EXE', NULL, 'Period Input Report', 'R25', '', 'Financial Reports', 'Active'
ELSE
    UPDATE [Menu_Structure]
    SET    MS_Order = 7, MS_Type = 'A', MS_Menu_ID = 0, MS_Application = 'EndPeriodInputReport.EXE', MS_Additional = NULL, MS_AppID = 'R25', MS_IconName = '', MS_Heading = 'Financial Reports', MS_Status = 'Active'
    WHERE  MS_Caption = 'Period Input Report'

IF NOT EXISTS(SELECT 1 FROM [Menu_Structure]WHERE MS_Caption = 'Period End Supplier Summary by Product')
    INSERT [Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status )
    SELECT 7, 'A', 0, 'SupplierSummarybyProduct.exe', NULL, 'Period End Supplier Summary by Product', 'R26', '', 'Financial Reports', 'Active'
ELSE
    UPDATE [Menu_Structure]
    SET    MS_Order = 7, MS_Type = 'A', MS_Menu_ID = 0, MS_Application = 'SupplierSummarybyProduct.exe', MS_Additional = NULL, MS_AppID = 'R26', MS_IconName = '', MS_Heading = 'Financial Reports', MS_Status = 'Active'
    WHERE  MS_Caption = 'Period End Supplier Summary by Product'

IF NOT EXISTS(SELECT 1 FROM [Menu_Structure]WHERE MS_Caption = 'Period End Supplier Summary Consolidated')
    INSERT [Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status )
    SELECT 7, 'A', 0, 'SupplierSummaryReportConsolidated.exe', NULL, 'Period End Supplier Summary Consolidated', 'R27', '', 'Financial Reports', 'Active'
ELSE
    UPDATE [Menu_Structure]
    SET    MS_Order = 7, MS_Type = 'A', MS_Menu_ID = 0, MS_Application = 'SupplierSummaryReportConsolidated.exe', MS_Additional = NULL, MS_AppID = 'R27', MS_IconName = '', MS_Heading = 'Financial Reports', MS_Status = 'Active'
    WHERE  MS_Caption = 'Period End Supplier Summary Consolidated'

IF NOT EXISTS(SELECT 1 FROM [Menu_Structure]WHERE MS_Caption = 'Regional Summary')
    INSERT [Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status )
    SELECT 7, 'A', 0, 'SiteOwnerRegionalSummary.exe', NULL, 'Regional Summary', 'R28', '', 'Financial Reports', 'Active'
ELSE
    UPDATE [Menu_Structure]
    SET    MS_Order = 7, MS_Type = 'A', MS_Menu_ID = 0, MS_Application = 'SiteOwnerRegionalSummary.exe', MS_Additional = NULL, MS_AppID = 'R28', MS_IconName = '', MS_Heading = 'Financial Reports', MS_Status = 'Active'
    WHERE  MS_Caption = 'Regional Summary'

IF NOT EXISTS(SELECT 1 FROM [Menu_Structure]WHERE MS_Caption = 'Regional Performance')
    INSERT [Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status )
    SELECT 7, 'A', 0, 'SiteOwnerRegionalPerformance.exe', NULL, 'Regional Performance', 'R29', '', 'Financial Reports', 'Active'
ELSE
    UPDATE [Menu_Structure]
    SET    MS_Order = 7, MS_Type = 'A', MS_Menu_ID = 0, MS_Application = 'SiteOwnerRegionalPerformance.exe', MS_Additional = NULL, MS_AppID = 'R29', MS_IconName = '', MS_Heading = 'Financial Reports', MS_Status = 'Active'
    WHERE  MS_Caption = 'Regional Performance'

IF NOT EXISTS(SELECT 1 FROM [Menu_Structure]WHERE MS_Caption = 'End Week Summary')
    INSERT [Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status )
    SELECT 7, 'A', 0, 'SiteOwnerEndWeekSummary.exe', NULL, 'End Week Summary', 'R30', '', 'Financial Reports', 'Active'
ELSE
    UPDATE [Menu_Structure]
    SET    MS_Order = 7, MS_Type = 'A', MS_Menu_ID = 0, MS_Application = 'SiteOwnerEndWeekSummary.exe', MS_Additional = NULL, MS_AppID = 'R30', MS_IconName = '', MS_Heading = 'Financial Reports', MS_Status = 'Active'
    WHERE  MS_Caption = 'End Week Summary'

IF NOT EXISTS(SELECT 1 FROM [Menu_Structure]WHERE MS_Caption = 'Period End Supplier Summary by Product by RAD')
    INSERT [Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status )
    SELECT 7, 'A', 0, 'SupplierSummarybyProductbyRAD.exe', NULL, 'Period End Supplier Summary by Product by RAD', 'R31', '', 'Financial Reports', 'Active'
ELSE
    UPDATE [Menu_Structure]
    SET    MS_Order = 7, MS_Type = 'A', MS_Menu_ID = 0, MS_Application = 'SupplierSummarybyProductbyRAD.exe', MS_Additional = NULL, MS_AppID = 'R31', MS_IconName = '', MS_Heading = 'Financial Reports', MS_Status = 'Active'
    WHERE  MS_Caption = 'Period End Supplier Summary by Product by RAD'

IF NOT EXISTS(SELECT 1 FROM [Menu_Structure]WHERE MS_Caption = 'Period End Supplier Summary Consolidated by RAD')
    INSERT [Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status )
    SELECT 7, 'A', 0, 'SupplierSummarybyRADConsolidated.exe', NULL, 'Period End Supplier Summary Consolidated by RAD', 'R32', '', 'Financial Reports', 'Active'
ELSE
    UPDATE [Menu_Structure]
    SET    MS_Order = 7, MS_Type = 'A', MS_Menu_ID = 0, MS_Application = 'SupplierSummarybyRADConsolidated.exe', MS_Additional = NULL, MS_AppID = 'R32', MS_IconName = '', MS_Heading = 'Financial Reports', MS_Status = 'Active'
    WHERE  MS_Caption = 'Period End Supplier Summary Consolidated by RAD'

IF NOT EXISTS(SELECT 1 FROM [Menu_Structure]WHERE MS_Caption = 'Area Summary')
    INSERT [Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status )
    SELECT 7, 'A', 0, 'SiteOwnerAreaSummary.exe', NULL, 'Area Summary', 'R33', '', 'Financial Reports', 'Active'
ELSE
    UPDATE [Menu_Structure]
    SET    MS_Order = 7, MS_Type = 'A', MS_Menu_ID = 0, MS_Application = 'SiteOwnerAreaSummary.exe', MS_Additional = NULL, MS_AppID = 'R33', MS_IconName = '', MS_Heading = 'Financial Reports', MS_Status = 'Active'
    WHERE  MS_Caption = 'Area Summary'

IF NOT EXISTS(SELECT 1 FROM [Menu_Structure]WHERE MS_Caption = 'Area Performance')
    INSERT [Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status )
    SELECT 7, 'A', 0, 'SiteOwnerAreaPerformance.EXE', NULL, 'Area Performance', 'R34', '', 'Financial Reports', 'Active'
ELSE
    UPDATE [Menu_Structure]
    SET    MS_Order = 7, MS_Type = 'A', MS_Menu_ID = 0, MS_Application = 'SiteOwnerAreaPerformance.EXE', MS_Additional = NULL, MS_AppID = 'R34', MS_IconName = '', MS_Heading = 'Financial Reports', MS_Status = 'Active'
    WHERE  MS_Caption = 'Area Performance'

IF NOT EXISTS(SELECT 1 FROM [Menu_Structure]WHERE MS_Caption = 'End Week Report')
    INSERT [Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status )
    SELECT 7, 'A', 0, 'SiteOwnerEndWeekReport.exe', NULL, 'End Week Report', 'R35', '', 'Financial Reports', 'Active'
ELSE
    UPDATE [Menu_Structure]
    SET    MS_Order = 7, MS_Type = 'A', MS_Menu_ID = 0, MS_Application = 'SiteOwnerEndWeekReport.exe', MS_Additional = NULL, MS_AppID = 'R35', MS_IconName = '', MS_Heading = 'Financial Reports', MS_Status = 'Active'
    WHERE  MS_Caption = 'End Week Report'

IF NOT EXISTS(SELECT 1 FROM [Menu_Structure]WHERE MS_Caption = 'Period End Supplier Summary by Product by RAD-Royalties')
    INSERT [Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status )
    SELECT 7, 'A', 0, 'SupplierSummarybyProductRADRoyalty.exe', NULL, 'Period End Supplier Summary by Product by RAD-Royalties', 'R36', '', 'Financial Reports', 'Active'
ELSE
    UPDATE [Menu_Structure]
    SET    MS_Order = 7, MS_Type = 'A', MS_Menu_ID = 0, MS_Application = 'SupplierSummarybyProductRADRoyalty.exe', MS_Additional = NULL, MS_AppID = 'R36', MS_IconName = '', MS_Heading = 'Financial Reports', MS_Status = 'Active'
    WHERE  MS_Caption = 'Period End Supplier Summary by Product by RAD-Royalties'

IF NOT EXISTS(SELECT 1 FROM [Menu_Structure]WHERE MS_Caption = 'Period Summary by BDM')
    INSERT [Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status )
    SELECT 7, 'A', 0, 'PeriodSummarybyBDM.exe', NULL, 'Period Summary by BDM', 'R37', '', 'Financial Reports', 'Active'
ELSE
    UPDATE [Menu_Structure]
    SET    MS_Order = 7, MS_Type = 'A', MS_Menu_ID = 0, MS_Application = 'PeriodSummarybyBDM.exe', MS_Additional = NULL, MS_AppID = 'R37', MS_IconName = '', MS_Heading = 'Financial Reports', MS_Status = 'Active'
    WHERE  MS_Caption = 'Period Summary by BDM'

IF NOT EXISTS(SELECT 1 FROM [Menu_Structure]WHERE MS_Caption = 'District Summary')
    INSERT [Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status )
    SELECT 7, 'A', 0, 'SiteOwnerDistrictSummary.exe', NULL, 'District Summary', 'R38', '', 'Financial Reports', 'Active'
ELSE
    UPDATE [Menu_Structure]
    SET    MS_Order = 7, MS_Type = 'A', MS_Menu_ID = 0, MS_Application = 'SiteOwnerDistrictSummary.exe', MS_Additional = NULL, MS_AppID = 'R38', MS_IconName = '', MS_Heading = 'Financial Reports', MS_Status = 'Active'
    WHERE  MS_Caption = 'District Summary'

IF NOT EXISTS(SELECT 1 FROM [Menu_Structure]WHERE MS_Caption = 'District Performance')
    INSERT [Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status )
    SELECT 7, 'A', 0, 'SiteOwnerDistrictPerformance.exe', NULL, 'District Performance', 'R39', '', 'Financial Reports', 'Active'
ELSE
    UPDATE [Menu_Structure]
    SET    MS_Order = 7, MS_Type = 'A', MS_Menu_ID = 0, MS_Application = 'SiteOwnerDistrictPerformance.exe', MS_Additional = NULL, MS_AppID = 'R39', MS_IconName = '', MS_Heading = 'Financial Reports', MS_Status = 'Active'
    WHERE  MS_Caption = 'District Performance'

IF NOT EXISTS(SELECT 1 FROM [Menu_Structure]WHERE MS_Caption = 'Site Weekly Summary')
    INSERT [Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status )
    SELECT 7, 'A', 0, 'SiteOwnerSiteWeeklySummary.exe', NULL, 'Site Weekly Summary', 'R40', '', 'Financial Reports', 'Active'
ELSE
    UPDATE [Menu_Structure]
    SET    MS_Order = 7, MS_Type = 'A', MS_Menu_ID = 0, MS_Application = 'SiteOwnerSiteWeeklySummary.exe', MS_Additional = NULL, MS_AppID = 'R40', MS_IconName = '', MS_Heading = 'Financial Reports', MS_Status = 'Active'
    WHERE  MS_Caption = 'Site Weekly Summary'

IF NOT EXISTS(SELECT 1 FROM [Menu_Structure]WHERE MS_Caption = 'Site Budget')
    INSERT [Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status )
    SELECT 7, 'A', 0, 'SiteOwnerSiteBudget.exe', NULL, 'Site Budget', 'R41', '', 'Financial Reports', 'Active'
ELSE
    UPDATE [Menu_Structure]
    SET    MS_Order = 7, MS_Type = 'A', MS_Menu_ID = 0, MS_Application = 'SiteOwnerSiteBudget.exe', MS_Additional = NULL, MS_AppID = 'R41', MS_IconName = '', MS_Heading = 'Financial Reports', MS_Status = 'Active'
    WHERE  MS_Caption = 'Site Budget'

IF NOT EXISTS(SELECT 1 FROM [Menu_Structure]WHERE MS_Caption = 'Site Owner Period Audit Reports')
    INSERT [Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status )
    SELECT 7, 'A', 0, 'SiteOwnerDistrictBudget.exe', NULL, 'Site Owner Period Audit Reports', 'R42', '', 'Financial Reports', 'Active'
ELSE
    UPDATE [Menu_Structure]
    SET    MS_Order = 7, MS_Type = 'A', MS_Menu_ID = 0, MS_Application = 'SiteOwnerDistrictBudget.exe', MS_Additional = NULL, MS_AppID = 'R42', MS_IconName = '', MS_Heading = 'Financial Reports', MS_Status = 'Active'
    WHERE  MS_Caption = 'Site Owner Period Audit Reports'

IF NOT EXISTS(SELECT 1 FROM [Menu_Structure]WHERE MS_Caption = 'Site Owner Performance by Product')
    INSERT [Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status )
    SELECT 7, 'A', 0, 'SiteOwnerPerformancebyProduct.exe', NULL, 'Site Owner Performance by Product', 'R43', '', 'Financial Reports', 'Active'
ELSE
    UPDATE [Menu_Structure]
    SET    MS_Order = 7, MS_Type = 'A', MS_Menu_ID = 0, MS_Application = 'SiteOwnerPerformancebyProduct.exe', MS_Additional = NULL, MS_AppID = 'R43', MS_IconName = '', MS_Heading = 'Financial Reports', MS_Status = 'Active'
    WHERE  MS_Caption = 'Site Owner Performance by Product'

IF NOT EXISTS(SELECT 1 FROM [Menu_Structure]WHERE MS_Caption = 'Site')
    INSERT [Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status )
    SELECT 5, 'M', 8, NULL, NULL, 'Site', NULL, NULL, 'Maintenance Reports', 'Active'
ELSE
    UPDATE [Menu_Structure]
    SET    MS_Order = 5, MS_Type = 'M', MS_Menu_ID = 8, MS_Application = NULL, MS_Additional = NULL, MS_AppID = NULL, MS_IconName = NULL, MS_Heading = 'Maintenance Reports', MS_Status = 'Active'
    WHERE  MS_Caption = 'Site'

IF NOT EXISTS(SELECT 1 FROM [Menu_Structure]WHERE MS_Caption = 'Stock')
    INSERT [Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status )
    SELECT 5, 'M', 9, NULL, NULL, 'Stock', NULL, NULL, 'Maintenance Reports', 'Active'
ELSE
    UPDATE [Menu_Structure]
    SET    MS_Order = 5, MS_Type = 'M', MS_Menu_ID = 9, MS_Application = NULL, MS_Additional = NULL, MS_AppID = NULL, MS_IconName = NULL, MS_Heading = 'Maintenance Reports', MS_Status = 'Active'
    WHERE  MS_Caption = 'Stock'

IF NOT EXISTS(SELECT 1 FROM [Menu_Structure]WHERE MS_Caption = 'Movement')
    INSERT [Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status )
    SELECT 5, 'M', 10, NULL, NULL, 'Movement', NULL, NULL, 'Maintenance Reports', 'Active'
ELSE
    UPDATE [Menu_Structure]
    SET    MS_Order = 5, MS_Type = 'M', MS_Menu_ID = 10, MS_Application = NULL, MS_Additional = NULL, MS_AppID = NULL, MS_IconName = NULL, MS_Heading = 'Maintenance Reports', MS_Status = 'Active'
    WHERE  MS_Caption = 'Movement'

IF NOT EXISTS(SELECT 1 FROM [Menu_Structure]WHERE MS_Caption = 'Admin')
    INSERT [Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status )
    SELECT 5, 'M', 11, NULL, NULL, 'Admin', NULL, NULL, 'Maintenance Reports', 'Active'
ELSE
    UPDATE [Menu_Structure]
    SET    MS_Order = 5, MS_Type = 'M', MS_Menu_ID = 11, MS_Application = NULL, MS_Additional = NULL, MS_AppID = NULL, MS_IconName = NULL, MS_Heading = 'Maintenance Reports', MS_Status = 'Active'
    WHERE  MS_Caption = 'Admin'

IF NOT EXISTS(SELECT 1 FROM [Menu_Structure]WHERE MS_Caption = 'Site Address List')
    INSERT [Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status )
    SELECT 8, 'A', 0, 'SiteAddressList.EXE', NULL, 'Site Address List', 'R44', '', 'Site Reports', 'Active'
ELSE
    UPDATE [Menu_Structure]
    SET    MS_Order = 8, MS_Type = 'A', MS_Menu_ID = 0, MS_Application = 'SiteAddressList.EXE', MS_Additional = NULL, MS_AppID = 'R44', MS_IconName = '', MS_Heading = 'Site Reports', MS_Status = 'Active'
    WHERE  MS_Caption = 'Site Address List'

IF NOT EXISTS(SELECT 1 FROM [Menu_Structure]WHERE MS_Caption = 'Sites and Installed Machines')
    INSERT [Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status )
    SELECT 8, 'A', 0, 'SitePosition.EXE', NULL, 'Sites and Installed Machines', 'R45', '', 'Site Reports', 'Active'
ELSE
    UPDATE [Menu_Structure]
    SET    MS_Order = 8, MS_Type = 'A', MS_Menu_ID = 0, MS_Application = 'SitePosition.EXE', MS_Additional = NULL, MS_AppID = 'R45', MS_IconName = '', MS_Heading = 'Site Reports', MS_Status = 'Active'
    WHERE  MS_Caption = 'Sites and Installed Machines'

IF NOT EXISTS(SELECT 1 FROM [Menu_Structure]WHERE MS_Caption = 'Stock Location')
    INSERT [Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status )
    SELECT 9, 'A', 0, 'MachineLocationList.EXE', NULL, 'Stock Location', 'R46', '', 'Stock Reports', 'Active'
ELSE
    UPDATE [Menu_Structure]
    SET    MS_Order = 9, MS_Type = 'A', MS_Menu_ID = 0, MS_Application = 'MachineLocationList.EXE', MS_Additional = NULL, MS_AppID = 'R46', MS_IconName = '', MS_Heading = 'Stock Reports', MS_Status = 'Active'
    WHERE  MS_Caption = 'Stock Location'

IF NOT EXISTS(SELECT 1 FROM [Menu_Structure]WHERE MS_Caption = 'In Stock')
    INSERT [Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status )
    SELECT 9, 'A', 0, 'MachineInStockList.EXE', NULL, 'In Stock', 'R47', '', 'Stock Reports', 'Active'
ELSE
    UPDATE [Menu_Structure]
    SET    MS_Order = 9, MS_Type = 'A', MS_Menu_ID = 0, MS_Application = 'MachineInStockList.EXE', MS_Additional = NULL, MS_AppID = 'R47', MS_IconName = '', MS_Heading = 'Stock Reports', MS_Status = 'Active'
    WHERE  MS_Caption = 'In Stock'

IF NOT EXISTS(SELECT 1 FROM [Menu_Structure]WHERE MS_Caption = 'Additions to Stock')
    INSERT [Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status )
    SELECT 9, 'A', 0, 'StockAdditions.EXE', NULL, 'Additions to Stock', 'R48', '', 'Stock Reports', 'Active'
ELSE
    UPDATE [Menu_Structure]
    SET    MS_Order = 9, MS_Type = 'A', MS_Menu_ID = 0, MS_Application = 'StockAdditions.EXE', MS_Additional = NULL, MS_AppID = 'R48', MS_IconName = '', MS_Heading = 'Stock Reports', MS_Status = 'Active'
    WHERE  MS_Caption = 'Additions to Stock'

IF NOT EXISTS(SELECT 1 FROM [Menu_Structure]WHERE MS_Caption = 'Disposals')
    INSERT [Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status )
    SELECT 9, 'A', 0, 'StockDisposals.EXE', NULL, 'Disposals', 'R49', '', 'Stock Reports', 'Active'
ELSE
    UPDATE [Menu_Structure]
    SET    MS_Order = 9, MS_Type = 'A', MS_Menu_ID = 0, MS_Application = 'StockDisposals.EXE', MS_Additional = NULL, MS_AppID = 'R49', MS_IconName = '', MS_Heading = 'Stock Reports', MS_Status = 'Active'
    WHERE  MS_Caption = 'Disposals'

IF NOT EXISTS(SELECT 1 FROM [Menu_Structure]WHERE MS_Caption = 'Conversions')
    INSERT [Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status )
    SELECT 9, 'A', 0, 'StockConversions.EXE', NULL, 'Conversions', 'R50', '', 'Stock Reports', 'Active'
ELSE
    UPDATE [Menu_Structure]
    SET    MS_Order = 9, MS_Type = 'A', MS_Menu_ID = 0, MS_Application = 'StockConversions.EXE', MS_Additional = NULL, MS_AppID = 'R50', MS_IconName = '', MS_Heading = 'Stock Reports', MS_Status = 'Active'
    WHERE  MS_Caption = 'Conversions'

IF NOT EXISTS(SELECT 1 FROM [Menu_Structure]WHERE MS_Caption = 'Machine Summary')
    INSERT [Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status )
    SELECT 9, 'A', 0, 'MachineSummary.EXE', NULL, 'Machine Summary', 'R51', '', 'Stock Reports', 'Active'
ELSE
    UPDATE [Menu_Structure]
    SET    MS_Order = 9, MS_Type = 'A', MS_Menu_ID = 0, MS_Application = 'MachineSummary.EXE', MS_Additional = NULL, MS_AppID = 'R51', MS_IconName = '', MS_Heading = 'Stock Reports', MS_Status = 'Active'
    WHERE  MS_Caption = 'Machine Summary'

IF NOT EXISTS(SELECT 1 FROM [Menu_Structure]WHERE MS_Caption = 'Machine Mix')
    INSERT [Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status )
    SELECT 9, 'A', 0, 'MachineMix.EXE', NULL, 'Machine Mix', 'R52', '', 'Stock Reports', 'Active'
ELSE
    UPDATE [Menu_Structure]
    SET    MS_Order = 9, MS_Type = 'A', MS_Menu_ID = 0, MS_Application = 'MachineMix.EXE', MS_Additional = NULL, MS_AppID = 'R52', MS_IconName = '', MS_Heading = 'Stock Reports', MS_Status = 'Active'
    WHERE  MS_Caption = 'Machine Mix'

IF NOT EXISTS(SELECT 1 FROM [Menu_Structure]WHERE MS_Caption = 'Inventory Analysis by Age')
    INSERT [Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status )
    SELECT 9, 'A', 0, 'InventoryAnalysisbyAge.EXE', NULL, 'Inventory Analysis by Age', 'R53', '', 'Stock Reports', 'Active'
ELSE
    UPDATE [Menu_Structure]
    SET    MS_Order = 9, MS_Type = 'A', MS_Menu_ID = 0, MS_Application = 'InventoryAnalysisbyAge.EXE', MS_Additional = NULL, MS_AppID = 'R53', MS_IconName = '', MS_Heading = 'Stock Reports', MS_Status = 'Active'
    WHERE  MS_Caption = 'Inventory Analysis by Age'

IF NOT EXISTS(SELECT 1 FROM [Menu_Structure]WHERE MS_Caption = 'In Stock by Age')
    INSERT [Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status )
    SELECT 9, 'A', 0, 'InStockbyAge.EXE', NULL, 'In Stock by Age', 'R54', '', 'Stock Reports', 'Active'
ELSE
    UPDATE [Menu_Structure]
    SET    MS_Order = 9, MS_Type = 'A', MS_Menu_ID = 0, MS_Application = 'InStockbyAge.EXE', MS_Additional = NULL, MS_AppID = 'R54', MS_IconName = '', MS_Heading = 'Stock Reports', MS_Status = 'Active'
    WHERE  MS_Caption = 'In Stock by Age'

IF NOT EXISTS(SELECT 1 FROM [Menu_Structure]WHERE MS_Caption = 'Movement History')
    INSERT [Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status )
    SELECT 10, 'A', 0, 'MovementHistory.EXE', NULL, 'Movement History', 'R55', '', 'Movement Reports', 'Active'
ELSE
    UPDATE [Menu_Structure]
    SET    MS_Order = 10, MS_Type = 'A', MS_Menu_ID = 0, MS_Application = 'MovementHistory.EXE', MS_Additional = NULL, MS_AppID = 'R55', MS_IconName = '', MS_Heading = 'Movement Reports', MS_Status = 'Active'
    WHERE  MS_Caption = 'Movement History'

IF NOT EXISTS(SELECT 1 FROM [Menu_Structure]WHERE MS_Caption = 'Flagged for Change')
    INSERT [Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status )
    SELECT 10, 'A', 0, 'FlaggedForChange.EXE', NULL, 'Flagged for Change', 'R56', '', 'Movement Reports', 'Active'
ELSE
    UPDATE [Menu_Structure]
    SET    MS_Order = 10, MS_Type = 'A', MS_Menu_ID = 0, MS_Application = 'FlaggedForChange.EXE', MS_Additional = NULL, MS_AppID = 'R56', MS_IconName = '', MS_Heading = 'Movement Reports', MS_Status = 'Active'
    WHERE  MS_Caption = 'Flagged for Change'

IF NOT EXISTS(SELECT 1 FROM [Menu_Structure]WHERE MS_Caption = 'Staff List')
    INSERT [Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status )
    SELECT 11, 'A', 0, 'StaffList.EXE', NULL, 'Staff List', 'R57', '', 'Admin Reports', 'Active'
ELSE
    UPDATE [Menu_Structure]
    SET    MS_Order = 11, MS_Type = 'A', MS_Menu_ID = 0, MS_Application = 'StaffList.EXE', MS_Additional = NULL, MS_AppID = 'R57', MS_IconName = '', MS_Heading = 'Admin Reports', MS_Status = 'Active'
    WHERE  MS_Caption = 'Staff List'

IF NOT EXISTS(SELECT 1 FROM [Menu_Structure]WHERE MS_Caption = 'Staff List (Detailed)')
    INSERT [Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status )
    SELECT 11, 'A', 0, 'StaffListDetailed.EXE', NULL, 'Staff List (Detailed)', 'R58', '', 'Admin Reports', 'Active'
ELSE
    UPDATE [Menu_Structure]
    SET    MS_Order = 11, MS_Type = 'A', MS_Menu_ID = 0, MS_Application = 'StaffListDetailed.EXE', MS_Additional = NULL, MS_AppID = 'R58', MS_IconName = '', MS_Heading = 'Admin Reports', MS_Status = 'Active'
    WHERE  MS_Caption = 'Staff List (Detailed)'

IF NOT EXISTS(SELECT 1 FROM [Menu_Structure]WHERE MS_Caption = 'Expired Calendars')
    INSERT [Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status )
    SELECT 11, 'A', 0, 'ExpiredCalendars.EXE', NULL, 'Expired Calendars', 'R59', '', 'Admin Reports', 'Active'
ELSE
    UPDATE [Menu_Structure]
    SET    MS_Order = 11, MS_Type = 'A', MS_Menu_ID = 0, MS_Application = 'ExpiredCalendars.EXE', MS_Additional = NULL, MS_AppID = 'R59', MS_IconName = '', MS_Heading = 'Admin Reports', MS_Status = 'Active'
    WHERE  MS_Caption = 'Expired Calendars'
    
    GO