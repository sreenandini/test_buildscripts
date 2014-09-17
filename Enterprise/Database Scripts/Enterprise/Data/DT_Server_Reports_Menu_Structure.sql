USE [Enterprise]
GO

IF NOT EXISTS(SELECT 1 FROM [Server_Reports_Menu_Structure]WHERE MS_Caption = 'Management' AND MS_Heading = '         Enterprise Reports                                                                         ')
    INSERT [Server_Reports_Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status, MS_Profile_ID, MS_Report_Type, MS_ProcedureUsed )
    SELECT 1, 'M', 2, NULL, NULL, 'Management', NULL, NULL, '         Enterprise Reports                                                                         ', '      Active                                                                                        ', 1, 'A', NULL
ELSE
    UPDATE [Server_Reports_Menu_Structure]
    SET    MS_Order = 1, MS_Type = 'M', MS_Menu_ID = 2, MS_Application = NULL, MS_Additional = NULL, MS_AppID = NULL, MS_IconName = NULL, MS_Status = '      Active                                                                                        ', MS_Profile_ID = 1, MS_Report_Type = 'A', MS_ProcedureUsed = NULL
    WHERE  MS_Caption = 'Management' AND MS_Heading = '         Enterprise Reports                                                                         '

IF NOT EXISTS(SELECT 1 FROM [Server_Reports_Menu_Structure]WHERE MS_Caption = 'Cube' AND MS_Heading = '         Enterprise Reports                                                                         ')
    INSERT [Server_Reports_Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status, MS_Profile_ID, MS_Report_Type, MS_ProcedureUsed )
    SELECT 1, 'M', 3, NULL, NULL, 'Cube', NULL, NULL, '         Enterprise Reports                                                                         ', '      DeActive                                                                                      ', 1, 'A', NULL
ELSE
    UPDATE [Server_Reports_Menu_Structure]
    SET    MS_Order = 1, MS_Type = 'M', MS_Menu_ID = 3, MS_Application = NULL, MS_Additional = NULL, MS_AppID = NULL, MS_IconName = NULL, MS_Status = '      DeActive                                                                                      ', MS_Profile_ID = 1, MS_Report_Type = 'A', MS_ProcedureUsed = NULL
    WHERE  MS_Caption = 'Cube' AND MS_Heading = '         Enterprise Reports                                                                         '

IF NOT EXISTS(SELECT 1 FROM [Server_Reports_Menu_Structure]WHERE MS_Caption = 'Audit / Security' AND MS_Heading = '         Enterprise Reports                                                                         ')
    INSERT [Server_Reports_Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status, MS_Profile_ID, MS_Report_Type, MS_ProcedureUsed )
    SELECT 1, 'M', 4, NULL, NULL, 'Audit / Security', NULL, NULL, '         Enterprise Reports                                                                         ', '      Active                                                                                        ', 1, 'A', NULL
ELSE
    UPDATE [Server_Reports_Menu_Structure]
    SET    MS_Order = 1, MS_Type = 'M', MS_Menu_ID = 4, MS_Application = NULL, MS_Additional = NULL, MS_AppID = NULL, MS_IconName = NULL, MS_Status = '      Active                                                                                        ', MS_Profile_ID = 1, MS_Report_Type = 'A', MS_ProcedureUsed = NULL
    WHERE  MS_Caption = 'Audit / Security' AND MS_Heading = '         Enterprise Reports                                                                         '

IF NOT EXISTS(SELECT 1 FROM [Server_Reports_Menu_Structure]WHERE MS_Caption = 'Maintenance' AND MS_Heading = '         Enterprise Reports                                                                         ')
    INSERT [Server_Reports_Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status, MS_Profile_ID, MS_Report_Type, MS_ProcedureUsed )
    SELECT 1, 'M', 5, NULL, NULL, 'Maintenance', NULL, NULL, '         Enterprise Reports                                                                         ', '      Active                                                                                        ', 1, 'A', NULL
ELSE
    UPDATE [Server_Reports_Menu_Structure]
    SET    MS_Order = 1, MS_Type = 'M', MS_Menu_ID = 5, MS_Application = NULL, MS_Additional = NULL, MS_AppID = NULL, MS_IconName = NULL, MS_Status = '      Active                                                                                        ', MS_Profile_ID = 1, MS_Report_Type = 'A', MS_ProcedureUsed = NULL
    WHERE  MS_Caption = 'Maintenance' AND MS_Heading = '         Enterprise Reports                                                                         '

IF NOT EXISTS(SELECT 1 FROM [Server_Reports_Menu_Structure]WHERE MS_Caption = 'Service' AND MS_Heading = '         Enterprise Reports                                                                         ')
    INSERT [Server_Reports_Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status, MS_Profile_ID, MS_Report_Type, MS_ProcedureUsed )
    SELECT 1, 'M', 6, NULL, NULL, 'Service', NULL, NULL, '         Enterprise Reports                                                                         ', '      Active                                                                                        ', 1, 'A', NULL
ELSE
    UPDATE [Server_Reports_Menu_Structure]
    SET    MS_Order = 1, MS_Type = 'M', MS_Menu_ID = 6, MS_Application = NULL, MS_Additional = NULL, MS_AppID = NULL, MS_IconName = NULL, MS_Status = '      Active                                                                                        ', MS_Profile_ID = 1, MS_Report_Type = 'A', MS_ProcedureUsed = NULL
    WHERE  MS_Caption = 'Service' AND MS_Heading = '         Enterprise Reports                                                                         '

IF NOT EXISTS(SELECT 1 FROM [Server_Reports_Menu_Structure]WHERE MS_Caption = 'financials' AND MS_Heading = '         Enterprise Reports                                                                         ')
    INSERT [Server_Reports_Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status, MS_Profile_ID, MS_Report_Type, MS_ProcedureUsed )
    SELECT 1, 'M', 7, NULL, NULL, 'financials', NULL, NULL, '         Enterprise Reports                                                                         ', '      DeActive                                                                                      ', 1, 'A', NULL
ELSE
    UPDATE [Server_Reports_Menu_Structure]
    SET    MS_Order = 1, MS_Type = 'M', MS_Menu_ID = 7, MS_Application = NULL, MS_Additional = NULL, MS_AppID = NULL, MS_IconName = NULL, MS_Status = '      DeActive                                                                                      ', MS_Profile_ID = 1, MS_Report_Type = 'A', MS_ProcedureUsed = NULL
    WHERE  MS_Caption = 'financials' AND MS_Heading = '         Enterprise Reports                                                                         '

IF NOT EXISTS(SELECT 1 FROM [Server_Reports_Menu_Structure]WHERE MS_Caption = 'Machine Meter Analysis' AND MS_Heading = '         Management Reports                                                                         ')
    INSERT [Server_Reports_Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status, MS_Profile_ID, MS_Report_Type, MS_ProcedureUsed )
    SELECT 2, 'A', 0, 'BGSEnterpriseSiteMachineAssetReport.EXE', NULL, 'Machine Meter Analysis', 'R01', '', '         Management Reports                                                                         ', '      DeActive                                                                                      ', 1, 'A', NULL
ELSE
    UPDATE [Server_Reports_Menu_Structure]
    SET    MS_Order = 2, MS_Type = 'A', MS_Menu_ID = 0, MS_Application = 'BGSEnterpriseSiteMachineAssetReport.EXE', MS_Additional = NULL, MS_AppID = 'R01', MS_IconName = '', MS_Status = '      DeActive                                                                                      ', MS_Profile_ID = 1, MS_Report_Type = 'A', MS_ProcedureUsed = NULL
    WHERE  MS_Caption = 'Machine Meter Analysis' AND MS_Heading = '         Management Reports                                                                         '

IF NOT EXISTS(SELECT 1 FROM [Server_Reports_Menu_Structure]WHERE MS_Caption = 'Meter Summary Report' AND MS_Heading = '         Management Reports                                                                         ')
    INSERT [Server_Reports_Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status, MS_Profile_ID, MS_Report_Type, MS_ProcedureUsed )
    SELECT 2, 'A', 0, 'ReportCaller.EXE', 'METERSUMMARY', 'Meter Summary Report', 'R02', '', '         Management Reports                                                                         ', '      Active                                                                                        ', 1, 'R', 'rsp_REPORT_MeterSummaryDetail'
ELSE
    UPDATE [Server_Reports_Menu_Structure]
    SET    MS_Order = 2, MS_Type = 'A', MS_Menu_ID = 0, MS_Application = 'ReportCaller.EXE', MS_Additional = 'METERSUMMARY', MS_AppID = 'R02', MS_IconName = '', MS_Status = '      Active                                                                                        ', MS_Profile_ID = 1, MS_Report_Type = 'R', MS_ProcedureUsed = 'rsp_REPORT_MeterSummaryDetail'
    WHERE  MS_Caption = 'Meter Summary Report' AND MS_Heading = '         Management Reports                                                                         '

IF NOT EXISTS(SELECT 1 FROM [Server_Reports_Menu_Structure]WHERE MS_Caption = 'Meter Detailed Report' AND MS_Heading = '         Management Reports                                                                         ')
    INSERT [Server_Reports_Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status, MS_Profile_ID, MS_Report_Type, MS_ProcedureUsed )
    SELECT 2, 'A', 0, 'ReportCaller.EXE', 'METERDETAIL', 'Meter Detailed Report', 'R03', '', '         Management Reports                                                                         ', '      Active                                                                                        ', 1, 'R', 'rsp_REPORT_MeterDetail'
ELSE
    UPDATE [Server_Reports_Menu_Structure]
    SET    MS_Order = 2, MS_Type = 'A', MS_Menu_ID = 0, MS_Application = 'ReportCaller.EXE', MS_Additional = 'METERDETAIL', MS_AppID = 'R03', MS_IconName = '', MS_Status = '      Active                                                                                        ', MS_Profile_ID = 1, MS_Report_Type = 'R', MS_ProcedureUsed = 'rsp_REPORT_MeterDetail'
    WHERE  MS_Caption = 'Meter Detailed Report' AND MS_Heading = '         Management Reports                                                                         '

IF NOT EXISTS(SELECT 1 FROM [Server_Reports_Menu_Structure]WHERE MS_Caption = 'A/C Win/Loss Report' AND MS_Heading = '         Management Reports                                                                         ')
    INSERT [Server_Reports_Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status, MS_Profile_ID, MS_Report_Type, MS_ProcedureUsed )
    SELECT 2, 'A', 0, 'ReportCaller.EXE', 'ACWINLOSS', 'A/C Win/Loss Report', 'R04', '', '         Management Reports                                                                         ', '      Active                                                                                        ', 1, 'A', 'rsp_REPORT_BankingReport'
ELSE
    UPDATE [Server_Reports_Menu_Structure]
    SET    MS_Order = 2, MS_Type = 'A', MS_Menu_ID = 0, MS_Application = 'ReportCaller.EXE', MS_Additional = 'ACWINLOSS', MS_AppID = 'R04', MS_IconName = '', MS_Status = '      Active                                                                                        ', MS_Profile_ID = 1, MS_Report_Type = 'A', MS_ProcedureUsed = 'rsp_REPORT_BankingReport'
    WHERE  MS_Caption = 'A/C Win/Loss Report' AND MS_Heading = '         Management Reports                                                                         '

IF NOT EXISTS(SELECT 1 FROM [Server_Reports_Menu_Structure]WHERE MS_Caption = 'A/C Summary Report' AND MS_Heading = '         Management Reports                                                                         ')
    INSERT [Server_Reports_Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status, MS_Profile_ID, MS_Report_Type, MS_ProcedureUsed )
    SELECT 2, 'A', 0, 'ReportCaller.EXE', 'ACSUMMARY', 'A/C Summary Report', 'R05', '', '         Management Reports                                                                         ', '      Active                                                                                        ', 1, 'A', 'rsp_REPORT_AccountAvgDailyWin'
ELSE
    UPDATE [Server_Reports_Menu_Structure]
    SET    MS_Order = 2, MS_Type = 'A', MS_Menu_ID = 0, MS_Application = 'ReportCaller.EXE', MS_Additional = 'ACSUMMARY', MS_AppID = 'R05', MS_IconName = '', MS_Status = '      Active                                                                                        ', MS_Profile_ID = 1, MS_Report_Type = 'A', MS_ProcedureUsed = 'rsp_REPORT_AccountAvgDailyWin'
    WHERE  MS_Caption = 'A/C Summary Report' AND MS_Heading = '         Management Reports                                                                         '

IF NOT EXISTS(SELECT 1 FROM [Server_Reports_Menu_Structure]WHERE MS_Caption = 'A/C Detailed Report' AND MS_Heading = '         Management Reports                                                                         ')
    INSERT [Server_Reports_Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status, MS_Profile_ID, MS_Report_Type, MS_ProcedureUsed )
    SELECT 2, 'A', 0, 'ReportCaller.EXE', 'ACDETAIL', 'A/C Detailed Report', 'R06', '', '         Management Reports                                                                         ', '      Active                                                                                        ', 1, 'A', 'rsp_REPORT_AccountAvgDailyWin'
ELSE
    UPDATE [Server_Reports_Menu_Structure]
    SET    MS_Order = 2, MS_Type = 'A', MS_Menu_ID = 0, MS_Application = 'ReportCaller.EXE', MS_Additional = 'ACDETAIL', MS_AppID = 'R06', MS_IconName = '', MS_Status = '      Active                                                                                        ', MS_Profile_ID = 1, MS_Report_Type = 'A', MS_ProcedureUsed = 'rsp_REPORT_AccountAvgDailyWin'
    WHERE  MS_Caption = 'A/C Detailed Report' AND MS_Heading = '         Management Reports                                                                         '

IF NOT EXISTS(SELECT 1 FROM [Server_Reports_Menu_Structure]WHERE MS_Caption = 'Depreciation Report' AND MS_Heading = '         Management Reports                                                                         ')
    INSERT [Server_Reports_Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status, MS_Profile_ID, MS_Report_Type, MS_ProcedureUsed )
    SELECT 2, 'A', 0, 'ReportCaller.EXE', 'DEPRECIATIONDETAIL', 'Depreciation Report', 'R07', '', '         Management Reports                                                                         ', '      Active                                                                                        ', 1, 'A', 'GetDepreciationDet'
ELSE
    UPDATE [Server_Reports_Menu_Structure]
    SET    MS_Order = 2, MS_Type = 'A', MS_Menu_ID = 0, MS_Application = 'ReportCaller.EXE', MS_Additional = 'DEPRECIATIONDETAIL', MS_AppID = 'R07', MS_IconName = '', MS_Status = '      Active                                                                                        ', MS_Profile_ID = 1, MS_Report_Type = 'A', MS_ProcedureUsed = 'GetDepreciationDet'
    WHERE  MS_Caption = 'Depreciation Report' AND MS_Heading = '         Management Reports                                                                         '

IF NOT EXISTS(SELECT 1 FROM [Server_Reports_Menu_Structure]WHERE MS_Caption = 'Progressive Win Summary Report' AND MS_Heading = '         Management Reports                                                                         ')
    INSERT [Server_Reports_Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status, MS_Profile_ID, MS_Report_Type, MS_ProcedureUsed )
    SELECT 2, 'A', 0, 'ReportCaller.EXE', 'PROGSUMMARY', 'Progressive Win Summary Report', 'R60', '', '         Management Reports                                                                         ', '      Active                                                                                        ', 1, 'R', 'rsp_REPORT_ProgressiveReport'
ELSE
    UPDATE [Server_Reports_Menu_Structure]
    SET    MS_Order = 2, MS_Type = 'A', MS_Menu_ID = 0, MS_Application = 'ReportCaller.EXE', MS_Additional = 'PROGSUMMARY', MS_AppID = 'R60', MS_IconName = '', MS_Status = '      Active                                                                                        ', MS_Profile_ID = 1, MS_Report_Type = 'R', MS_ProcedureUsed = 'rsp_REPORT_ProgressiveReport'
    WHERE  MS_Caption = 'Progressive Win Summary Report' AND MS_Heading = '         Management Reports                                                                         '

IF NOT EXISTS(SELECT 1 FROM [Server_Reports_Menu_Structure]WHERE MS_Caption = 'Progressive Win Detailed Report' AND MS_Heading = '         Management Reports                                                                         ')
    INSERT [Server_Reports_Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status, MS_Profile_ID, MS_Report_Type, MS_ProcedureUsed )
    SELECT 2, 'A', 0, 'ReportCaller.EXE', 'PROGDETAIL', 'Progressive Win Detailed Report', 'R61', '', '         Management Reports                                                                         ', '      Active                                                                                        ', 1, 'R', 'rsp_REPORT_ProgressiveReport'
ELSE
    UPDATE [Server_Reports_Menu_Structure]
    SET    MS_Order = 2, MS_Type = 'A', MS_Menu_ID = 0, MS_Application = 'ReportCaller.EXE', MS_Additional = 'PROGDETAIL', MS_AppID = 'R61', MS_IconName = '', MS_Status = '      Active                                                                                        ', MS_Profile_ID = 1, MS_Report_Type = 'R', MS_ProcedureUsed = 'rsp_REPORT_ProgressiveReport'
    WHERE  MS_Caption = 'Progressive Win Detailed Report' AND MS_Heading = '         Management Reports                                                                         '

IF NOT EXISTS(SELECT 1 FROM [Server_Reports_Menu_Structure]WHERE MS_Caption = 'Accounting Win/Loss' AND MS_Heading = '         Cube Reports                                                                               ')
    INSERT [Server_Reports_Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status, MS_Profile_ID, MS_Report_Type, MS_ProcedureUsed )
    SELECT 3, 'A', 0, 'cubeCollectionData.Max', NULL, 'Accounting Win/Loss', 'R08', '', '         Cube Reports                                                                               ', '      DeActive                                                                                      ', 1, 'A', NULL
ELSE
    UPDATE [Server_Reports_Menu_Structure]
    SET    MS_Order = 3, MS_Type = 'A', MS_Menu_ID = 0, MS_Application = 'cubeCollectionData.Max', MS_Additional = NULL, MS_AppID = 'R08', MS_IconName = '', MS_Status = '      DeActive                                                                                      ', MS_Profile_ID = 1, MS_Report_Type = 'A', MS_ProcedureUsed = NULL
    WHERE  MS_Caption = 'Accounting Win/Loss' AND MS_Heading = '         Cube Reports                                                                               '

IF NOT EXISTS(SELECT 1 FROM [Server_Reports_Menu_Structure]WHERE MS_Caption = 'Daily Read Data' AND MS_Heading = '         Cube Reports                                                                               ')
    INSERT [Server_Reports_Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status, MS_Profile_ID, MS_Report_Type, MS_ProcedureUsed )
    SELECT 3, 'A', 0, 'cubeDailyReadData.Max', NULL, 'Daily Read Data', 'R09', '', '         Cube Reports                                                                               ', '      DeActive                                                                                      ', 1, 'A', NULL
ELSE
    UPDATE [Server_Reports_Menu_Structure]
    SET    MS_Order = 3, MS_Type = 'A', MS_Menu_ID = 0, MS_Application = 'cubeDailyReadData.Max', MS_Additional = NULL, MS_AppID = 'R09', MS_IconName = '', MS_Status = '      DeActive                                                                                      ', MS_Profile_ID = 1, MS_Report_Type = 'A', MS_ProcedureUsed = NULL
    WHERE  MS_Caption = 'Daily Read Data' AND MS_Heading = '         Cube Reports                                                                               '

IF NOT EXISTS(SELECT 1 FROM [Server_Reports_Menu_Structure]WHERE MS_Caption = 'Hourly Data' AND MS_Heading = '         Cube Reports                                                                               ')
    INSERT [Server_Reports_Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status, MS_Profile_ID, MS_Report_Type, MS_ProcedureUsed )
    SELECT 3, 'A', 0, 'cubeHourlyData.Max', NULL, 'Hourly Data', 'R10', '', '         Cube Reports                                                                               ', '      DeActive                                                                                      ', 1, 'A', NULL
ELSE
    UPDATE [Server_Reports_Menu_Structure]
    SET    MS_Order = 3, MS_Type = 'A', MS_Menu_ID = 0, MS_Application = 'cubeHourlyData.Max', MS_Additional = NULL, MS_AppID = 'R10', MS_IconName = '', MS_Status = '      DeActive                                                                                      ', MS_Profile_ID = 1, MS_Report_Type = 'A', MS_ProcedureUsed = NULL
    WHERE  MS_Caption = 'Hourly Data' AND MS_Heading = '         Cube Reports                                                                               '

IF NOT EXISTS(SELECT 1 FROM [Server_Reports_Menu_Structure]WHERE MS_Caption = 'Consolidated M/C Collection Variance' AND MS_Heading = '         Audit / Security Reports                                                                   ')
    INSERT [Server_Reports_Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status, MS_Profile_ID, MS_Report_Type, MS_ProcedureUsed )
    SELECT 4, 'A', 0, 'ReportCaller.EXE', 'CONSOLIDATEDVARIANCE', 'Consolidated M/C Collection Variance', 'R11', '', '         Audit / Security Reports                                                                   ', '      Active                                                                                        ', 1, 'A', 'rsp_consolidated_variance'
ELSE
    UPDATE [Server_Reports_Menu_Structure]
    SET    MS_Order = 4, MS_Type = 'A', MS_Menu_ID = 0, MS_Application = 'ReportCaller.EXE', MS_Additional = 'CONSOLIDATEDVARIANCE', MS_AppID = 'R11', MS_IconName = '', MS_Status = '      Active                                                                                        ', MS_Profile_ID = 1, MS_Report_Type = 'A', MS_ProcedureUsed = 'rsp_consolidated_variance'
    WHERE  MS_Caption = 'Consolidated M/C Collection Variance' AND MS_Heading = '         Audit / Security Reports                                                                   '

IF NOT EXISTS(SELECT 1 FROM [Server_Reports_Menu_Structure]WHERE MS_Caption = 'Remedy Codes' AND MS_Heading = '         Service Reports                                                                            ')
    INSERT [Server_Reports_Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status, MS_Profile_ID, MS_Report_Type, MS_ProcedureUsed )
    SELECT 6, 'A', 0, 'ReportCaller.EXE', 'SERVICEREMEDYCODES', 'Remedy Codes', 'R12', '', '         Service Reports                                                                            ', '      Active                                                                                        ', 1, 'A', 'GetServiceRemedyCodes'
ELSE
    UPDATE [Server_Reports_Menu_Structure]
    SET    MS_Order = 6, MS_Type = 'A', MS_Menu_ID = 0, MS_Application = 'ReportCaller.EXE', MS_Additional = 'SERVICEREMEDYCODES', MS_AppID = 'R12', MS_IconName = '', MS_Status = '      Active                                                                                        ', MS_Profile_ID = 1, MS_Report_Type = 'A', MS_ProcedureUsed = 'GetServiceRemedyCodes'
    WHERE  MS_Caption = 'Remedy Codes' AND MS_Heading = '         Service Reports                                                                            '

IF NOT EXISTS(SELECT 1 FROM [Server_Reports_Menu_Structure]WHERE MS_Caption = 'Service History' AND MS_Heading = '         Service Reports                                                                            ')
    INSERT [Server_Reports_Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status, MS_Profile_ID, MS_Report_Type, MS_ProcedureUsed )
    SELECT 6, 'A', 0, 'ServiceCallList.EXE', NULL, 'Service History', 'R13', '', '         Service Reports                                                                            ', '      DeActive                                                                                      ', 1, 'A', NULL
ELSE
    UPDATE [Server_Reports_Menu_Structure]
    SET    MS_Order = 6, MS_Type = 'A', MS_Menu_ID = 0, MS_Application = 'ServiceCallList.EXE', MS_Additional = NULL, MS_AppID = 'R13', MS_IconName = '', MS_Status = '      DeActive                                                                                      ', MS_Profile_ID = 1, MS_Report_Type = 'A', MS_ProcedureUsed = NULL
    WHERE  MS_Caption = 'Service History' AND MS_Heading = '         Service Reports                                                                            '

IF NOT EXISTS(SELECT 1 FROM [Server_Reports_Menu_Structure]WHERE MS_Caption = 'Fault Groups' AND MS_Heading = '         Service Reports                                                                            ')
    INSERT [Server_Reports_Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status, MS_Profile_ID, MS_Report_Type, MS_ProcedureUsed )
    SELECT 6, 'A', 0, 'ReportCaller.EXE', 'FAULTGROUP', 'Fault Groups', 'R14', '', '         Service Reports                                                                            ', '      Active                                                                                        ', 1, 'A', 'GetServiceFaultGroup'
ELSE
    UPDATE [Server_Reports_Menu_Structure]
    SET    MS_Order = 6, MS_Type = 'A', MS_Menu_ID = 0, MS_Application = 'ReportCaller.EXE', MS_Additional = 'FAULTGROUP', MS_AppID = 'R14', MS_IconName = '', MS_Status = '      Active                                                                                        ', MS_Profile_ID = 1, MS_Report_Type = 'A', MS_ProcedureUsed = 'GetServiceFaultGroup'
    WHERE  MS_Caption = 'Fault Groups' AND MS_Heading = '         Service Reports                                                                            '

IF NOT EXISTS(SELECT 1 FROM [Server_Reports_Menu_Structure]WHERE MS_Caption = 'SLA Terms' AND MS_Heading = '         Service Reports                                                                            ')
    INSERT [Server_Reports_Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status, MS_Profile_ID, MS_Report_Type, MS_ProcedureUsed )
    SELECT 6, 'A', 0, 'ReportCaller.EXE', 'SLATERMS', 'SLA Terms', 'R15', '', '         Service Reports                                                                            ', '      Active                                                                                        ', 1, 'A', 'GetServiceSLATerms'
ELSE
    UPDATE [Server_Reports_Menu_Structure]
    SET    MS_Order = 6, MS_Type = 'A', MS_Menu_ID = 0, MS_Application = 'ReportCaller.EXE', MS_Additional = 'SLATERMS', MS_AppID = 'R15', MS_IconName = '', MS_Status = '      Active                                                                                        ', MS_Profile_ID = 1, MS_Report_Type = 'A', MS_ProcedureUsed = 'GetServiceSLATerms'
    WHERE  MS_Caption = 'SLA Terms' AND MS_Heading = '         Service Reports                                                                            '

IF NOT EXISTS(SELECT 1 FROM [Server_Reports_Menu_Structure]WHERE MS_Caption = 'Site Analysis' AND MS_Heading = '         Service Reports                                                                            ')
    INSERT [Server_Reports_Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status, MS_Profile_ID, MS_Report_Type, MS_ProcedureUsed )
    SELECT 6, 'A', 0, 'ServiceSiteAnalysis.EXE', NULL, 'Site Analysis', 'R16', '', '         Service Reports                                                                            ', '      DeActive                                                                                      ', 1, 'A', NULL
ELSE
    UPDATE [Server_Reports_Menu_Structure]
    SET    MS_Order = 6, MS_Type = 'A', MS_Menu_ID = 0, MS_Application = 'ServiceSiteAnalysis.EXE', MS_Additional = NULL, MS_AppID = 'R16', MS_IconName = '', MS_Status = '      DeActive                                                                                      ', MS_Profile_ID = 1, MS_Report_Type = 'A', MS_ProcedureUsed = NULL
    WHERE  MS_Caption = 'Site Analysis' AND MS_Heading = '         Service Reports                                                                            '

IF NOT EXISTS(SELECT 1 FROM [Server_Reports_Menu_Structure]WHERE MS_Caption = 'Machine Analysis' AND MS_Heading = '         Service Reports                                                                            ')
    INSERT [Server_Reports_Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status, MS_Profile_ID, MS_Report_Type, MS_ProcedureUsed )
    SELECT 6, 'A', 0, 'ServiceMachineAnalysis.EXE', NULL, 'Machine Analysis', 'R17', '', '         Service Reports                                                                            ', '      DeActive                                                                                      ', 1, 'A', NULL
ELSE
    UPDATE [Server_Reports_Menu_Structure]
    SET    MS_Order = 6, MS_Type = 'A', MS_Menu_ID = 0, MS_Application = 'ServiceMachineAnalysis.EXE', MS_Additional = NULL, MS_AppID = 'R17', MS_IconName = '', MS_Status = '      DeActive                                                                                      ', MS_Profile_ID = 1, MS_Report_Type = 'A', MS_ProcedureUsed = NULL
    WHERE  MS_Caption = 'Machine Analysis' AND MS_Heading = '         Service Reports                                                                            '

IF NOT EXISTS(SELECT 1 FROM [Server_Reports_Menu_Structure]WHERE MS_Caption = 'Remedy Analysis' AND MS_Heading = '         Service Reports                                                                            ')
    INSERT [Server_Reports_Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status, MS_Profile_ID, MS_Report_Type, MS_ProcedureUsed )
    SELECT 6, 'A', 0, 'RemedyAnalysis.EXE', NULL, 'Remedy Analysis', 'R18', '', '         Service Reports                                                                            ', '      DeActive                                                                                      ', 1, 'A', NULL
ELSE
    UPDATE [Server_Reports_Menu_Structure]
    SET    MS_Order = 6, MS_Type = 'A', MS_Menu_ID = 0, MS_Application = 'RemedyAnalysis.EXE', MS_Additional = NULL, MS_AppID = 'R18', MS_IconName = '', MS_Status = '      DeActive                                                                                      ', MS_Profile_ID = 1, MS_Report_Type = 'A', MS_ProcedureUsed = NULL
    WHERE  MS_Caption = 'Remedy Analysis' AND MS_Heading = '         Service Reports                                                                            '

IF NOT EXISTS(SELECT 1 FROM [Server_Reports_Menu_Structure]WHERE MS_Caption = 'Statistical Analysis' AND MS_Heading = '         Service Reports                                                                            ')
    INSERT [Server_Reports_Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status, MS_Profile_ID, MS_Report_Type, MS_ProcedureUsed )
    SELECT 6, 'A', 0, 'ServiceStatistics.EXE', NULL, 'Statistical Analysis', 'R19', '', '         Service Reports                                                                            ', '      DeActive                                                                                      ', 1, 'A', NULL
ELSE
    UPDATE [Server_Reports_Menu_Structure]
    SET    MS_Order = 6, MS_Type = 'A', MS_Menu_ID = 0, MS_Application = 'ServiceStatistics.EXE', MS_Additional = NULL, MS_AppID = 'R19', MS_IconName = '', MS_Status = '      DeActive                                                                                      ', MS_Profile_ID = 1, MS_Report_Type = 'A', MS_ProcedureUsed = NULL
    WHERE  MS_Caption = 'Statistical Analysis' AND MS_Heading = '         Service Reports                                                                            '

IF NOT EXISTS(SELECT 1 FROM [Server_Reports_Menu_Structure]WHERE MS_Caption = 'Live Call List' AND MS_Heading = '         Service Reports                                                                            ')
    INSERT [Server_Reports_Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status, MS_Profile_ID, MS_Report_Type, MS_ProcedureUsed )
    SELECT 6, 'A', 0, 'LiveServiceCallList.EXE', NULL, 'Live Call List', 'R20', '', '         Service Reports                                                                            ', '      DeActive                                                                                      ', 1, 'A', NULL
ELSE
    UPDATE [Server_Reports_Menu_Structure]
    SET    MS_Order = 6, MS_Type = 'A', MS_Menu_ID = 0, MS_Application = 'LiveServiceCallList.EXE', MS_Additional = NULL, MS_AppID = 'R20', MS_IconName = '', MS_Status = '      DeActive                                                                                      ', MS_Profile_ID = 1, MS_Report_Type = 'A', MS_ProcedureUsed = NULL
    WHERE  MS_Caption = 'Live Call List' AND MS_Heading = '         Service Reports                                                                            '

IF NOT EXISTS(SELECT 1 FROM [Server_Reports_Menu_Structure]WHERE MS_Caption = 'Test Period End - by Product' AND MS_Heading = '         Financial Reports                                                                          ')
    INSERT [Server_Reports_Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status, MS_Profile_ID, MS_Report_Type, MS_ProcedureUsed )
    SELECT 7, 'A', 0, 'SupplierSummarybyProductTest.exe', NULL, 'Test Period End - by Product', 'R21', '', '         Financial Reports                                                                          ', '      DeActive                                                                                      ', 1, 'A', NULL
ELSE
    UPDATE [Server_Reports_Menu_Structure]
    SET    MS_Order = 7, MS_Type = 'A', MS_Menu_ID = 0, MS_Application = 'SupplierSummarybyProductTest.exe', MS_Additional = NULL, MS_AppID = 'R21', MS_IconName = '', MS_Status = '      DeActive                                                                                      ', MS_Profile_ID = 1, MS_Report_Type = 'A', MS_ProcedureUsed = NULL
    WHERE  MS_Caption = 'Test Period End - by Product' AND MS_Heading = '         Financial Reports                                                                          '

IF NOT EXISTS(SELECT 1 FROM [Server_Reports_Menu_Structure]WHERE MS_Caption = 'Secondary Period End - Consolidated' AND MS_Heading = '         Financial Reports                                                                          ')
    INSERT [Server_Reports_Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status, MS_Profile_ID, MS_Report_Type, MS_ProcedureUsed )
    SELECT 7, 'A', 0, 'SecondaryEndPeriodConsolidated.EXE', NULL, 'Secondary Period End - Consolidated', 'R22', '', '         Financial Reports                                                                          ', '      DeActive                                                                                      ', 1, 'A', NULL
ELSE
    UPDATE [Server_Reports_Menu_Structure]
    SET    MS_Order = 7, MS_Type = 'A', MS_Menu_ID = 0, MS_Application = 'SecondaryEndPeriodConsolidated.EXE', MS_Additional = NULL, MS_AppID = 'R22', MS_IconName = '', MS_Status = '      DeActive                                                                                      ', MS_Profile_ID = 1, MS_Report_Type = 'A', MS_ProcedureUsed = NULL
    WHERE  MS_Caption = 'Secondary Period End - Consolidated' AND MS_Heading = '         Financial Reports                                                                          '

IF NOT EXISTS(SELECT 1 FROM [Server_Reports_Menu_Structure]WHERE MS_Caption = 'Secondary Party Period End' AND MS_Heading = '         Financial Reports                                                                          ')
    INSERT [Server_Reports_Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status, MS_Profile_ID, MS_Report_Type, MS_ProcedureUsed )
    SELECT 7, 'A', 0, 'SecondaryEndPeriodReport.exe', NULL, 'Secondary Party Period End', 'R23', '', '         Financial Reports                                                                          ', '      DeActive                                                                                      ', 1, 'A', NULL
ELSE
    UPDATE [Server_Reports_Menu_Structure]
    SET    MS_Order = 7, MS_Type = 'A', MS_Menu_ID = 0, MS_Application = 'SecondaryEndPeriodReport.exe', MS_Additional = NULL, MS_AppID = 'R23', MS_IconName = '', MS_Status = '      DeActive                                                                                      ', MS_Profile_ID = 1, MS_Report_Type = 'A', MS_ProcedureUsed = NULL
    WHERE  MS_Caption = 'Secondary Party Period End' AND MS_Heading = '         Financial Reports                                                                          '

IF NOT EXISTS(SELECT 1 FROM [Server_Reports_Menu_Structure]WHERE MS_Caption = 'Site Owner Performance' AND MS_Heading = '         Financial Reports                                                                          ')
    INSERT [Server_Reports_Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status, MS_Profile_ID, MS_Report_Type, MS_ProcedureUsed )
    SELECT 7, 'A', 0, 'SiteOwnerPerformance.exe', NULL, 'Site Owner Performance', 'R24', '', '         Financial Reports                                                                          ', '      DeActive                                                                                      ', 1, 'A', NULL
ELSE
    UPDATE [Server_Reports_Menu_Structure]
    SET    MS_Order = 7, MS_Type = 'A', MS_Menu_ID = 0, MS_Application = 'SiteOwnerPerformance.exe', MS_Additional = NULL, MS_AppID = 'R24', MS_IconName = '', MS_Status = '      DeActive                                                                                      ', MS_Profile_ID = 1, MS_Report_Type = 'A', MS_ProcedureUsed = NULL
    WHERE  MS_Caption = 'Site Owner Performance' AND MS_Heading = '         Financial Reports                                                                          '

IF NOT EXISTS(SELECT 1 FROM [Server_Reports_Menu_Structure]WHERE MS_Caption = 'Period Input Report' AND MS_Heading = '         Financial Reports                                                                          ')
    INSERT [Server_Reports_Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status, MS_Profile_ID, MS_Report_Type, MS_ProcedureUsed )
    SELECT 7, 'A', 0, 'EndPeriodInputReport.EXE', NULL, 'Period Input Report', 'R25', '', '         Financial Reports                                                                          ', '      DeActive                                                                                      ', 1, 'A', NULL
ELSE
    UPDATE [Server_Reports_Menu_Structure]
    SET    MS_Order = 7, MS_Type = 'A', MS_Menu_ID = 0, MS_Application = 'EndPeriodInputReport.EXE', MS_Additional = NULL, MS_AppID = 'R25', MS_IconName = '', MS_Status = '      DeActive                                                                                      ', MS_Profile_ID = 1, MS_Report_Type = 'A', MS_ProcedureUsed = NULL
    WHERE  MS_Caption = 'Period Input Report' AND MS_Heading = '         Financial Reports                                                                          '

IF NOT EXISTS(SELECT 1 FROM [Server_Reports_Menu_Structure]WHERE MS_Caption = 'Period End Supplier Summary by Product' AND MS_Heading = '         Financial Reports                                                                          ')
    INSERT [Server_Reports_Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status, MS_Profile_ID, MS_Report_Type, MS_ProcedureUsed )
    SELECT 7, 'A', 0, 'SupplierSummarybyProduct.exe', NULL, 'Period End Supplier Summary by Product', 'R26', '', '         Financial Reports                                                                          ', '      DeActive                                                                                      ', 1, 'A', NULL
ELSE
    UPDATE [Server_Reports_Menu_Structure]
    SET    MS_Order = 7, MS_Type = 'A', MS_Menu_ID = 0, MS_Application = 'SupplierSummarybyProduct.exe', MS_Additional = NULL, MS_AppID = 'R26', MS_IconName = '', MS_Status = '      DeActive                                                                                      ', MS_Profile_ID = 1, MS_Report_Type = 'A', MS_ProcedureUsed = NULL
    WHERE  MS_Caption = 'Period End Supplier Summary by Product' AND MS_Heading = '         Financial Reports                                                                          '

IF NOT EXISTS(SELECT 1 FROM [Server_Reports_Menu_Structure]WHERE MS_Caption = 'Period End Supplier Summary Consolidated' AND MS_Heading = '         Financial Reports                                                                          ')
    INSERT [Server_Reports_Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status, MS_Profile_ID, MS_Report_Type, MS_ProcedureUsed )
    SELECT 7, 'A', 0, 'SupplierSummaryReportConsolidated.exe', NULL, 'Period End Supplier Summary Consolidated', 'R27', '', '         Financial Reports                                                                          ', '      DeActive                                                                                      ', 1, 'A', NULL
ELSE
    UPDATE [Server_Reports_Menu_Structure]
    SET    MS_Order = 7, MS_Type = 'A', MS_Menu_ID = 0, MS_Application = 'SupplierSummaryReportConsolidated.exe', MS_Additional = NULL, MS_AppID = 'R27', MS_IconName = '', MS_Status = '      DeActive                                                                                      ', MS_Profile_ID = 1, MS_Report_Type = 'A', MS_ProcedureUsed = NULL
    WHERE  MS_Caption = 'Period End Supplier Summary Consolidated' AND MS_Heading = '         Financial Reports                                                                          '

IF NOT EXISTS(SELECT 1 FROM [Server_Reports_Menu_Structure]WHERE MS_Caption = 'Regional Summary' AND MS_Heading = '         Financial Reports                                                                          ')
    INSERT [Server_Reports_Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status, MS_Profile_ID, MS_Report_Type, MS_ProcedureUsed )
    SELECT 7, 'A', 0, 'SiteOwnerRegionalSummary.exe', NULL, 'Regional Summary', 'R28', '', '         Financial Reports                                                                          ', '      DeActive                                                                                      ', 1, 'A', NULL
ELSE
    UPDATE [Server_Reports_Menu_Structure]
    SET    MS_Order = 7, MS_Type = 'A', MS_Menu_ID = 0, MS_Application = 'SiteOwnerRegionalSummary.exe', MS_Additional = NULL, MS_AppID = 'R28', MS_IconName = '', MS_Status = '      DeActive                                                                                      ', MS_Profile_ID = 1, MS_Report_Type = 'A', MS_ProcedureUsed = NULL
    WHERE  MS_Caption = 'Regional Summary' AND MS_Heading = '         Financial Reports                                                                          '

IF NOT EXISTS(SELECT 1 FROM [Server_Reports_Menu_Structure]WHERE MS_Caption = 'Regional Performance' AND MS_Heading = '         Financial Reports                                                                          ')
    INSERT [Server_Reports_Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status, MS_Profile_ID, MS_Report_Type, MS_ProcedureUsed )
    SELECT 7, 'A', 0, 'SiteOwnerRegionalPerformance.exe', NULL, 'Regional Performance', 'R29', '', '         Financial Reports                                                                          ', '      DeActive                                                                                      ', 1, 'A', NULL
ELSE
    UPDATE [Server_Reports_Menu_Structure]
    SET    MS_Order = 7, MS_Type = 'A', MS_Menu_ID = 0, MS_Application = 'SiteOwnerRegionalPerformance.exe', MS_Additional = NULL, MS_AppID = 'R29', MS_IconName = '', MS_Status = '      DeActive                                                                                      ', MS_Profile_ID = 1, MS_Report_Type = 'A', MS_ProcedureUsed = NULL
    WHERE  MS_Caption = 'Regional Performance' AND MS_Heading = '         Financial Reports                                                                          '

IF NOT EXISTS(SELECT 1 FROM [Server_Reports_Menu_Structure]WHERE MS_Caption = 'End Week Summary' AND MS_Heading = '         Financial Reports                                                                          ')
    INSERT [Server_Reports_Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status, MS_Profile_ID, MS_Report_Type, MS_ProcedureUsed )
    SELECT 7, 'A', 0, 'SiteOwnerEndWeekSummary.exe', NULL, 'End Week Summary', 'R30', '', '         Financial Reports                                                                          ', '      DeActive                                                                                      ', 1, 'A', NULL
ELSE
    UPDATE [Server_Reports_Menu_Structure]
    SET    MS_Order = 7, MS_Type = 'A', MS_Menu_ID = 0, MS_Application = 'SiteOwnerEndWeekSummary.exe', MS_Additional = NULL, MS_AppID = 'R30', MS_IconName = '', MS_Status = '      DeActive                                                                                      ', MS_Profile_ID = 1, MS_Report_Type = 'A', MS_ProcedureUsed = NULL
    WHERE  MS_Caption = 'End Week Summary' AND MS_Heading = '         Financial Reports                                                                          '

IF NOT EXISTS(SELECT 1 FROM [Server_Reports_Menu_Structure]WHERE MS_Caption = 'Period End Supplier Summary by Product by RAD' AND MS_Heading = '         Financial Reports                                                                          ')
    INSERT [Server_Reports_Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status, MS_Profile_ID, MS_Report_Type, MS_ProcedureUsed )
    SELECT 7, 'A', 0, 'SupplierSummarybyProductbyRAD.exe', NULL, 'Period End Supplier Summary by Product by RAD', 'R31', '', '         Financial Reports                                                                          ', '      DeActive                                                                                      ', 1, 'A', NULL
ELSE
    UPDATE [Server_Reports_Menu_Structure]
    SET    MS_Order = 7, MS_Type = 'A', MS_Menu_ID = 0, MS_Application = 'SupplierSummarybyProductbyRAD.exe', MS_Additional = NULL, MS_AppID = 'R31', MS_IconName = '', MS_Status = '      DeActive                                                                                      ', MS_Profile_ID = 1, MS_Report_Type = 'A', MS_ProcedureUsed = NULL
    WHERE  MS_Caption = 'Period End Supplier Summary by Product by RAD' AND MS_Heading = '         Financial Reports                                                                          '

IF NOT EXISTS(SELECT 1 FROM [Server_Reports_Menu_Structure]WHERE MS_Caption = 'Period End Supplier Summary Consolidated by RAD' AND MS_Heading = '         Financial Reports                                                                          ')
    INSERT [Server_Reports_Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status, MS_Profile_ID, MS_Report_Type, MS_ProcedureUsed )
    SELECT 7, 'A', 0, 'SupplierSummarybyRADConsolidated.exe', NULL, 'Period End Supplier Summary Consolidated by RAD', 'R32', '', '         Financial Reports                                                                          ', '      DeActive                                                                                      ', 1, 'A', NULL
ELSE
    UPDATE [Server_Reports_Menu_Structure]
    SET    MS_Order = 7, MS_Type = 'A', MS_Menu_ID = 0, MS_Application = 'SupplierSummarybyRADConsolidated.exe', MS_Additional = NULL, MS_AppID = 'R32', MS_IconName = '', MS_Status = '      DeActive                                                                                      ', MS_Profile_ID = 1, MS_Report_Type = 'A', MS_ProcedureUsed = NULL
    WHERE  MS_Caption = 'Period End Supplier Summary Consolidated by RAD' AND MS_Heading = '         Financial Reports                                                                          '

IF NOT EXISTS(SELECT 1 FROM [Server_Reports_Menu_Structure]WHERE MS_Caption = 'Area Summary' AND MS_Heading = '         Financial Reports                                                                          ')
    INSERT [Server_Reports_Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status, MS_Profile_ID, MS_Report_Type, MS_ProcedureUsed )
    SELECT 7, 'A', 0, 'SiteOwnerAreaSummary.exe', NULL, 'Area Summary', 'R33', '', '         Financial Reports                                                                          ', '      DeActive                                                                                      ', 1, 'A', NULL
ELSE
    UPDATE [Server_Reports_Menu_Structure]
    SET    MS_Order = 7, MS_Type = 'A', MS_Menu_ID = 0, MS_Application = 'SiteOwnerAreaSummary.exe', MS_Additional = NULL, MS_AppID = 'R33', MS_IconName = '', MS_Status = '      DeActive                                                                                      ', MS_Profile_ID = 1, MS_Report_Type = 'A', MS_ProcedureUsed = NULL
    WHERE  MS_Caption = 'Area Summary' AND MS_Heading = '         Financial Reports                                                                          '

IF NOT EXISTS(SELECT 1 FROM [Server_Reports_Menu_Structure]WHERE MS_Caption = 'Area Performance' AND MS_Heading = '         Financial Reports                                                                          ')
    INSERT [Server_Reports_Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status, MS_Profile_ID, MS_Report_Type, MS_ProcedureUsed )
    SELECT 7, 'A', 0, 'SiteOwnerAreaPerformance.EXE', NULL, 'Area Performance', 'R34', '', '         Financial Reports                                                                          ', '      DeActive                                                                                      ', 1, 'A', NULL
ELSE
    UPDATE [Server_Reports_Menu_Structure]
    SET    MS_Order = 7, MS_Type = 'A', MS_Menu_ID = 0, MS_Application = 'SiteOwnerAreaPerformance.EXE', MS_Additional = NULL, MS_AppID = 'R34', MS_IconName = '', MS_Status = '      DeActive                                                                                      ', MS_Profile_ID = 1, MS_Report_Type = 'A', MS_ProcedureUsed = NULL
    WHERE  MS_Caption = 'Area Performance' AND MS_Heading = '         Financial Reports                                                                          '

IF NOT EXISTS(SELECT 1 FROM [Server_Reports_Menu_Structure]WHERE MS_Caption = 'End Week Report' AND MS_Heading = '         Financial Reports                                                                          ')
    INSERT [Server_Reports_Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status, MS_Profile_ID, MS_Report_Type, MS_ProcedureUsed )
    SELECT 7, 'A', 0, 'SiteOwnerEndWeekReport.exe', NULL, 'End Week Report', 'R35', '', '         Financial Reports                                                                          ', '      DeActive                                                                                      ', 1, 'A', NULL
ELSE
    UPDATE [Server_Reports_Menu_Structure]
    SET    MS_Order = 7, MS_Type = 'A', MS_Menu_ID = 0, MS_Application = 'SiteOwnerEndWeekReport.exe', MS_Additional = NULL, MS_AppID = 'R35', MS_IconName = '', MS_Status = '      DeActive                                                                                      ', MS_Profile_ID = 1, MS_Report_Type = 'A', MS_ProcedureUsed = NULL
    WHERE  MS_Caption = 'End Week Report' AND MS_Heading = '         Financial Reports                                                                          '

IF NOT EXISTS(SELECT 1 FROM [Server_Reports_Menu_Structure]WHERE MS_Caption = 'Period End Supplier Summary by Product by RAD-Royalties' AND MS_Heading = '         Financial Reports                                                                          ')
    INSERT [Server_Reports_Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status, MS_Profile_ID, MS_Report_Type, MS_ProcedureUsed )
    SELECT 7, 'A', 0, 'SupplierSummarybyProductRADRoyalty.exe', NULL, 'Period End Supplier Summary by Product by RAD-Royalties', 'R36', '', '         Financial Reports                                                                          ', '      DeActive                                                                                      ', 1, 'A', NULL
ELSE
    UPDATE [Server_Reports_Menu_Structure]
    SET    MS_Order = 7, MS_Type = 'A', MS_Menu_ID = 0, MS_Application = 'SupplierSummarybyProductRADRoyalty.exe', MS_Additional = NULL, MS_AppID = 'R36', MS_IconName = '', MS_Status = '      DeActive                                                                                      ', MS_Profile_ID = 1, MS_Report_Type = 'A', MS_ProcedureUsed = NULL
    WHERE  MS_Caption = 'Period End Supplier Summary by Product by RAD-Royalties' AND MS_Heading = '         Financial Reports                                                                          '

IF NOT EXISTS(SELECT 1 FROM [Server_Reports_Menu_Structure]WHERE MS_Caption = 'Period Summary by BDM' AND MS_Heading = '         Financial Reports                                                                          ')
    INSERT [Server_Reports_Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status, MS_Profile_ID, MS_Report_Type, MS_ProcedureUsed )
    SELECT 7, 'A', 0, 'PeriodSummarybyBDM.exe', NULL, 'Period Summary by BDM', 'R37', '', '         Financial Reports                                                                          ', '      DeActive                                                                                      ', 1, 'A', NULL
ELSE
    UPDATE [Server_Reports_Menu_Structure]
    SET    MS_Order = 7, MS_Type = 'A', MS_Menu_ID = 0, MS_Application = 'PeriodSummarybyBDM.exe', MS_Additional = NULL, MS_AppID = 'R37', MS_IconName = '', MS_Status = '      DeActive                                                                                      ', MS_Profile_ID = 1, MS_Report_Type = 'A', MS_ProcedureUsed = NULL
    WHERE  MS_Caption = 'Period Summary by BDM' AND MS_Heading = '         Financial Reports                                                                          '

IF NOT EXISTS(SELECT 1 FROM [Server_Reports_Menu_Structure]WHERE MS_Caption = 'District Summary' AND MS_Heading = '         Financial Reports                                                                          ')
    INSERT [Server_Reports_Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status, MS_Profile_ID, MS_Report_Type, MS_ProcedureUsed )
    SELECT 7, 'A', 0, 'SiteOwnerDistrictSummary.exe', NULL, 'District Summary', 'R38', '', '         Financial Reports                                                                          ', '      DeActive                                                                                      ', 1, 'A', NULL
ELSE
    UPDATE [Server_Reports_Menu_Structure]
    SET    MS_Order = 7, MS_Type = 'A', MS_Menu_ID = 0, MS_Application = 'SiteOwnerDistrictSummary.exe', MS_Additional = NULL, MS_AppID = 'R38', MS_IconName = '', MS_Status = '      DeActive                                                                                      ', MS_Profile_ID = 1, MS_Report_Type = 'A', MS_ProcedureUsed = NULL
    WHERE  MS_Caption = 'District Summary' AND MS_Heading = '         Financial Reports                                                                          '

IF NOT EXISTS(SELECT 1 FROM [Server_Reports_Menu_Structure]WHERE MS_Caption = 'District Performance' AND MS_Heading = '         Financial Reports                                                                          ')
    INSERT [Server_Reports_Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status, MS_Profile_ID, MS_Report_Type, MS_ProcedureUsed )
    SELECT 7, 'A', 0, 'SiteOwnerDistrictPerformance.exe', NULL, 'District Performance', 'R39', '', '         Financial Reports                                                                          ', '      DeActive                                                                                      ', 1, 'A', NULL
ELSE
    UPDATE [Server_Reports_Menu_Structure]
    SET    MS_Order = 7, MS_Type = 'A', MS_Menu_ID = 0, MS_Application = 'SiteOwnerDistrictPerformance.exe', MS_Additional = NULL, MS_AppID = 'R39', MS_IconName = '', MS_Status = '      DeActive                                                                                      ', MS_Profile_ID = 1, MS_Report_Type = 'A', MS_ProcedureUsed = NULL
    WHERE  MS_Caption = 'District Performance' AND MS_Heading = '         Financial Reports                                                                          '

IF NOT EXISTS(SELECT 1 FROM [Server_Reports_Menu_Structure]WHERE MS_Caption = 'Site Weekly Summary' AND MS_Heading = '         Financial Reports                                                                          ')
    INSERT [Server_Reports_Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status, MS_Profile_ID, MS_Report_Type, MS_ProcedureUsed )
    SELECT 7, 'A', 0, 'SiteOwnerSiteWeeklySummary.exe', NULL, 'Site Weekly Summary', 'R40', '', '         Financial Reports                                                                          ', '      DeActive                                                                                      ', 1, 'A', NULL
ELSE
    UPDATE [Server_Reports_Menu_Structure]
    SET    MS_Order = 7, MS_Type = 'A', MS_Menu_ID = 0, MS_Application = 'SiteOwnerSiteWeeklySummary.exe', MS_Additional = NULL, MS_AppID = 'R40', MS_IconName = '', MS_Status = '      DeActive                                                                                      ', MS_Profile_ID = 1, MS_Report_Type = 'A', MS_ProcedureUsed = NULL
    WHERE  MS_Caption = 'Site Weekly Summary' AND MS_Heading = '         Financial Reports                                                                          '

IF NOT EXISTS(SELECT 1 FROM [Server_Reports_Menu_Structure]WHERE MS_Caption = 'Site Budget' AND MS_Heading = '         Financial Reports                                                                          ')
    INSERT [Server_Reports_Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status, MS_Profile_ID, MS_Report_Type, MS_ProcedureUsed )
    SELECT 7, 'A', 0, 'SiteOwnerSiteBudget.exe', NULL, 'Site Budget', 'R41', '', '         Financial Reports                                                                          ', '      DeActive                                                                                      ', 1, 'A', NULL
ELSE
    UPDATE [Server_Reports_Menu_Structure]
    SET    MS_Order = 7, MS_Type = 'A', MS_Menu_ID = 0, MS_Application = 'SiteOwnerSiteBudget.exe', MS_Additional = NULL, MS_AppID = 'R41', MS_IconName = '', MS_Status = '      DeActive                                                                                      ', MS_Profile_ID = 1, MS_Report_Type = 'A', MS_ProcedureUsed = NULL
    WHERE  MS_Caption = 'Site Budget' AND MS_Heading = '         Financial Reports                                                                          '

IF NOT EXISTS(SELECT 1 FROM [Server_Reports_Menu_Structure]WHERE MS_Caption = 'Site Owner Period Audit Reports' AND MS_Heading = '         Financial Reports                                                                          ')
    INSERT [Server_Reports_Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status, MS_Profile_ID, MS_Report_Type, MS_ProcedureUsed )
    SELECT 7, 'A', 0, 'SiteOwnerDistrictBudget.exe', NULL, 'Site Owner Period Audit Reports', 'R42', '', '         Financial Reports                                                                          ', '      DeActive                                                                                      ', 1, 'A', NULL
ELSE
    UPDATE [Server_Reports_Menu_Structure]
    SET    MS_Order = 7, MS_Type = 'A', MS_Menu_ID = 0, MS_Application = 'SiteOwnerDistrictBudget.exe', MS_Additional = NULL, MS_AppID = 'R42', MS_IconName = '', MS_Status = '      DeActive                                                                                      ', MS_Profile_ID = 1, MS_Report_Type = 'A', MS_ProcedureUsed = NULL
    WHERE  MS_Caption = 'Site Owner Period Audit Reports' AND MS_Heading = '         Financial Reports                                                                          '

IF NOT EXISTS(SELECT 1 FROM [Server_Reports_Menu_Structure]WHERE MS_Caption = 'Site Owner Performance by Product' AND MS_Heading = '         Financial Reports                                                                          ')
    INSERT [Server_Reports_Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status, MS_Profile_ID, MS_Report_Type, MS_ProcedureUsed )
    SELECT 7, 'A', 0, 'SiteOwnerPerformancebyProduct.exe', NULL, 'Site Owner Performance by Product', 'R43', '', '         Financial Reports                                                                          ', '      DeActive                                                                                      ', 1, 'A', NULL
ELSE
    UPDATE [Server_Reports_Menu_Structure]
    SET    MS_Order = 7, MS_Type = 'A', MS_Menu_ID = 0, MS_Application = 'SiteOwnerPerformancebyProduct.exe', MS_Additional = NULL, MS_AppID = 'R43', MS_IconName = '', MS_Status = '      DeActive                                                                                      ', MS_Profile_ID = 1, MS_Report_Type = 'A', MS_ProcedureUsed = NULL
    WHERE  MS_Caption = 'Site Owner Performance by Product' AND MS_Heading = '         Financial Reports                                                                          '

IF NOT EXISTS(SELECT 1 FROM [Server_Reports_Menu_Structure]WHERE MS_Caption = 'Site' AND MS_Heading = '         Maintenance Reports                                                                        ')
    INSERT [Server_Reports_Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status, MS_Profile_ID, MS_Report_Type, MS_ProcedureUsed )
    SELECT 5, 'M', 8, NULL, NULL, 'Site', NULL, NULL, '         Maintenance Reports                                                                        ', '      Active                                                                                        ', 1, 'A', NULL
ELSE
    UPDATE [Server_Reports_Menu_Structure]
    SET    MS_Order = 5, MS_Type = 'M', MS_Menu_ID = 8, MS_Application = NULL, MS_Additional = NULL, MS_AppID = NULL, MS_IconName = NULL, MS_Status = '      Active                                                                                        ', MS_Profile_ID = 1, MS_Report_Type = 'A', MS_ProcedureUsed = NULL
    WHERE  MS_Caption = 'Site' AND MS_Heading = '         Maintenance Reports                                                                        '

IF NOT EXISTS(SELECT 1 FROM [Server_Reports_Menu_Structure]WHERE MS_Caption = 'Stock' AND MS_Heading = '         Maintenance Reports                                                                        ')
    INSERT [Server_Reports_Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status, MS_Profile_ID, MS_Report_Type, MS_ProcedureUsed )
    SELECT 5, 'M', 9, NULL, NULL, 'Stock', NULL, NULL, '         Maintenance Reports                                                                        ', '      DeActive                                                                                      ', 1, 'A', NULL
ELSE
    UPDATE [Server_Reports_Menu_Structure]
    SET    MS_Order = 5, MS_Type = 'M', MS_Menu_ID = 9, MS_Application = NULL, MS_Additional = NULL, MS_AppID = NULL, MS_IconName = NULL, MS_Status = '      DeActive                                                                                      ', MS_Profile_ID = 1, MS_Report_Type = 'A', MS_ProcedureUsed = NULL
    WHERE  MS_Caption = 'Stock' AND MS_Heading = '         Maintenance Reports                                                                        '

IF NOT EXISTS(SELECT 1 FROM [Server_Reports_Menu_Structure]WHERE MS_Caption = 'Movement' AND MS_Heading = '         Maintenance Reports                                                                        ')
    INSERT [Server_Reports_Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status, MS_Profile_ID, MS_Report_Type, MS_ProcedureUsed )
    SELECT 5, 'M', 10, NULL, NULL, 'Movement', NULL, NULL, '         Maintenance Reports                                                                        ', '      DeActive                                                                                      ', 1, 'A', NULL
ELSE
    UPDATE [Server_Reports_Menu_Structure]
    SET    MS_Order = 5, MS_Type = 'M', MS_Menu_ID = 10, MS_Application = NULL, MS_Additional = NULL, MS_AppID = NULL, MS_IconName = NULL, MS_Status = '      DeActive                                                                                      ', MS_Profile_ID = 1, MS_Report_Type = 'A', MS_ProcedureUsed = NULL
    WHERE  MS_Caption = 'Movement' AND MS_Heading = '         Maintenance Reports                                                                        '

IF NOT EXISTS(SELECT 1 FROM [Server_Reports_Menu_Structure]WHERE MS_Caption = 'Admin' AND MS_Heading = '         Maintenance Reports                                                                        ')
    INSERT [Server_Reports_Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status, MS_Profile_ID, MS_Report_Type, MS_ProcedureUsed )
    SELECT 5, 'M', 11, NULL, NULL, 'Admin', NULL, NULL, '         Maintenance Reports                                                                        ', '      Active                                                                                        ', 1, 'A', NULL
ELSE
    UPDATE [Server_Reports_Menu_Structure]
    SET    MS_Order = 5, MS_Type = 'M', MS_Menu_ID = 11, MS_Application = NULL, MS_Additional = NULL, MS_AppID = NULL, MS_IconName = NULL, MS_Status = '      Active                                                                                        ', MS_Profile_ID = 1, MS_Report_Type = 'A', MS_ProcedureUsed = NULL
    WHERE  MS_Caption = 'Admin' AND MS_Heading = '         Maintenance Reports                                                                        '

IF NOT EXISTS(SELECT 1 FROM [Server_Reports_Menu_Structure]WHERE MS_Caption = 'Site Address List' AND MS_Heading = '         Site Reports                                                                               ')
    INSERT [Server_Reports_Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status, MS_Profile_ID, MS_Report_Type, MS_ProcedureUsed )
    SELECT 8, 'A', 0, 'ReportCaller.EXE', 'SITEADDRESS', 'Site Address List', 'R44', '', '         Site Reports                                                                               ', '      Active                                                                                        ', 1, 'A', 'rsp_Report_SiteAddressList'
ELSE
    UPDATE [Server_Reports_Menu_Structure]
    SET    MS_Order = 8, MS_Type = 'A', MS_Menu_ID = 0, MS_Application = 'ReportCaller.EXE', MS_Additional = 'SITEADDRESS', MS_AppID = 'R44', MS_IconName = '', MS_Status = '      Active                                                                                        ', MS_Profile_ID = 1, MS_Report_Type = 'A', MS_ProcedureUsed = 'rsp_Report_SiteAddressList'
    WHERE  MS_Caption = 'Site Address List' AND MS_Heading = '         Site Reports                                                                               '

IF NOT EXISTS(SELECT 1 FROM [Server_Reports_Menu_Structure]WHERE MS_Caption = 'Sites and Installed Machines' AND MS_Heading = '         Site Reports                                                                               ')
    INSERT [Server_Reports_Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status, MS_Profile_ID, MS_Report_Type, MS_ProcedureUsed )
    SELECT 8, 'A', 0, 'SitePosition.EXE', NULL, 'Sites and Installed Machines', 'R45', '', '         Site Reports                                                                               ', '      DeActive                                                                                      ', 1, 'A', NULL
ELSE
    UPDATE [Server_Reports_Menu_Structure]
    SET    MS_Order = 8, MS_Type = 'A', MS_Menu_ID = 0, MS_Application = 'SitePosition.EXE', MS_Additional = NULL, MS_AppID = 'R45', MS_IconName = '', MS_Status = '      DeActive                                                                                      ', MS_Profile_ID = 1, MS_Report_Type = 'A', MS_ProcedureUsed = NULL
    WHERE  MS_Caption = 'Sites and Installed Machines' AND MS_Heading = '         Site Reports                                                                               '

IF NOT EXISTS(SELECT 1 FROM [Server_Reports_Menu_Structure]WHERE MS_Caption = 'Stock Location' AND MS_Heading = '         Stock Reports                                                                              ')
    INSERT [Server_Reports_Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status, MS_Profile_ID, MS_Report_Type, MS_ProcedureUsed )
    SELECT 9, 'A', 0, 'MachineLocationList.EXE', NULL, 'Stock Location', 'R46', '', '         Stock Reports                                                                              ', '      DeActive                                                                                      ', 1, 'A', NULL
ELSE
    UPDATE [Server_Reports_Menu_Structure]
    SET    MS_Order = 9, MS_Type = 'A', MS_Menu_ID = 0, MS_Application = 'MachineLocationList.EXE', MS_Additional = NULL, MS_AppID = 'R46', MS_IconName = '', MS_Status = '      DeActive                                                                                      ', MS_Profile_ID = 1, MS_Report_Type = 'A', MS_ProcedureUsed = NULL
    WHERE  MS_Caption = 'Stock Location' AND MS_Heading = '         Stock Reports                                                                              '

IF NOT EXISTS(SELECT 1 FROM [Server_Reports_Menu_Structure]WHERE MS_Caption = 'In Stock' AND MS_Heading = '         Stock Reports                                                                              ')
    INSERT [Server_Reports_Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status, MS_Profile_ID, MS_Report_Type, MS_ProcedureUsed )
    SELECT 9, 'A', 0, 'MachineInStockList.EXE', NULL, 'In Stock', 'R47', '', '         Stock Reports                                                                              ', '      DeActive                                                                                      ', 1, 'A', NULL
ELSE
    UPDATE [Server_Reports_Menu_Structure]
    SET    MS_Order = 9, MS_Type = 'A', MS_Menu_ID = 0, MS_Application = 'MachineInStockList.EXE', MS_Additional = NULL, MS_AppID = 'R47', MS_IconName = '', MS_Status = '      DeActive                                                                                      ', MS_Profile_ID = 1, MS_Report_Type = 'A', MS_ProcedureUsed = NULL
    WHERE  MS_Caption = 'In Stock' AND MS_Heading = '         Stock Reports                                                                              '

IF NOT EXISTS(SELECT 1 FROM [Server_Reports_Menu_Structure]WHERE MS_Caption = 'Additions to Stock' AND MS_Heading = '         Stock Reports                                                                              ')
    INSERT [Server_Reports_Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status, MS_Profile_ID, MS_Report_Type, MS_ProcedureUsed )
    SELECT 9, 'A', 0, 'StockAdditions.EXE', NULL, 'Additions to Stock', 'R48', '', '         Stock Reports                                                                              ', '      DeActive                                                                                      ', 1, 'A', NULL
ELSE
    UPDATE [Server_Reports_Menu_Structure]
    SET    MS_Order = 9, MS_Type = 'A', MS_Menu_ID = 0, MS_Application = 'StockAdditions.EXE', MS_Additional = NULL, MS_AppID = 'R48', MS_IconName = '', MS_Status = '      DeActive                                                                                      ', MS_Profile_ID = 1, MS_Report_Type = 'A', MS_ProcedureUsed = NULL
    WHERE  MS_Caption = 'Additions to Stock' AND MS_Heading = '         Stock Reports                                                                              '

IF NOT EXISTS(SELECT 1 FROM [Server_Reports_Menu_Structure]WHERE MS_Caption = 'Disposals' AND MS_Heading = '         Stock Reports                                                                              ')
    INSERT [Server_Reports_Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status, MS_Profile_ID, MS_Report_Type, MS_ProcedureUsed )
    SELECT 9, 'A', 0, 'StockDisposals.EXE', NULL, 'Disposals', 'R49', '', '         Stock Reports                                                                              ', '      DeActive                                                                                      ', 1, 'A', NULL
ELSE
    UPDATE [Server_Reports_Menu_Structure]
    SET    MS_Order = 9, MS_Type = 'A', MS_Menu_ID = 0, MS_Application = 'StockDisposals.EXE', MS_Additional = NULL, MS_AppID = 'R49', MS_IconName = '', MS_Status = '      DeActive                                                                                      ', MS_Profile_ID = 1, MS_Report_Type = 'A', MS_ProcedureUsed = NULL
    WHERE  MS_Caption = 'Disposals' AND MS_Heading = '         Stock Reports                                                                              '

IF NOT EXISTS(SELECT 1 FROM [Server_Reports_Menu_Structure]WHERE MS_Caption = 'Conversions' AND MS_Heading = '         Stock Reports                                                                              ')
    INSERT [Server_Reports_Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status, MS_Profile_ID, MS_Report_Type, MS_ProcedureUsed )
    SELECT 9, 'A', 0, 'StockConversions.EXE', NULL, 'Conversions', 'R50', '', '         Stock Reports                                                                              ', '      DeActive                                                                                      ', 1, 'A', NULL
ELSE
    UPDATE [Server_Reports_Menu_Structure]
    SET    MS_Order = 9, MS_Type = 'A', MS_Menu_ID = 0, MS_Application = 'StockConversions.EXE', MS_Additional = NULL, MS_AppID = 'R50', MS_IconName = '', MS_Status = '      DeActive                                                                                      ', MS_Profile_ID = 1, MS_Report_Type = 'A', MS_ProcedureUsed = NULL
    WHERE  MS_Caption = 'Conversions' AND MS_Heading = '         Stock Reports                                                                              '

IF NOT EXISTS(SELECT 1 FROM [Server_Reports_Menu_Structure]WHERE MS_Caption = 'Machine Summary' AND MS_Heading = '         Stock Reports                                                                              ')
    INSERT [Server_Reports_Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status, MS_Profile_ID, MS_Report_Type, MS_ProcedureUsed )
    SELECT 9, 'A', 0, 'MachineSummary.EXE', NULL, 'Machine Summary', 'R51', '', '         Stock Reports                                                                              ', '      DeActive                                                                                      ', 1, 'A', NULL
ELSE
    UPDATE [Server_Reports_Menu_Structure]
    SET    MS_Order = 9, MS_Type = 'A', MS_Menu_ID = 0, MS_Application = 'MachineSummary.EXE', MS_Additional = NULL, MS_AppID = 'R51', MS_IconName = '', MS_Status = '      DeActive                                                                                      ', MS_Profile_ID = 1, MS_Report_Type = 'A', MS_ProcedureUsed = NULL
    WHERE  MS_Caption = 'Machine Summary' AND MS_Heading = '         Stock Reports                                                                              '

IF NOT EXISTS(SELECT 1 FROM [Server_Reports_Menu_Structure]WHERE MS_Caption = 'Machine Mix' AND MS_Heading = '         Stock Reports                                                                              ')
    INSERT [Server_Reports_Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status, MS_Profile_ID, MS_Report_Type, MS_ProcedureUsed )
    SELECT 9, 'A', 0, 'MachineMix.EXE', NULL, 'Machine Mix', 'R52', '', '         Stock Reports                                                                              ', '      DeActive                                                                                      ', 1, 'A', NULL
ELSE
    UPDATE [Server_Reports_Menu_Structure]
    SET    MS_Order = 9, MS_Type = 'A', MS_Menu_ID = 0, MS_Application = 'MachineMix.EXE', MS_Additional = NULL, MS_AppID = 'R52', MS_IconName = '', MS_Status = '      DeActive                                                                                      ', MS_Profile_ID = 1, MS_Report_Type = 'A', MS_ProcedureUsed = NULL
    WHERE  MS_Caption = 'Machine Mix' AND MS_Heading = '         Stock Reports                                                                              '

IF NOT EXISTS(SELECT 1 FROM [Server_Reports_Menu_Structure]WHERE MS_Caption = 'Inventory Analysis by Age' AND MS_Heading = '         Stock Reports                                                                              ')
    INSERT [Server_Reports_Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status, MS_Profile_ID, MS_Report_Type, MS_ProcedureUsed )
    SELECT 9, 'A', 0, 'InventoryAnalysisbyAge.EXE', NULL, 'Inventory Analysis by Age', 'R53', '', '         Stock Reports                                                                              ', '      DeActive                                                                                      ', 1, 'A', NULL
ELSE
    UPDATE [Server_Reports_Menu_Structure]
    SET    MS_Order = 9, MS_Type = 'A', MS_Menu_ID = 0, MS_Application = 'InventoryAnalysisbyAge.EXE', MS_Additional = NULL, MS_AppID = 'R53', MS_IconName = '', MS_Status = '      DeActive                                                                                      ', MS_Profile_ID = 1, MS_Report_Type = 'A', MS_ProcedureUsed = NULL
    WHERE  MS_Caption = 'Inventory Analysis by Age' AND MS_Heading = '         Stock Reports                                                                              '

IF NOT EXISTS(SELECT 1 FROM [Server_Reports_Menu_Structure]WHERE MS_Caption = 'In Stock by Age' AND MS_Heading = '         Stock Reports                                                                              ')
    INSERT [Server_Reports_Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status, MS_Profile_ID, MS_Report_Type, MS_ProcedureUsed )
    SELECT 9, 'A', 0, 'InStockbyAge.EXE', NULL, 'In Stock by Age', 'R54', '', '         Stock Reports                                                                              ', '      DeActive                                                                                      ', 1, 'A', NULL
ELSE
    UPDATE [Server_Reports_Menu_Structure]
    SET    MS_Order = 9, MS_Type = 'A', MS_Menu_ID = 0, MS_Application = 'InStockbyAge.EXE', MS_Additional = NULL, MS_AppID = 'R54', MS_IconName = '', MS_Status = '      DeActive                                                                                      ', MS_Profile_ID = 1, MS_Report_Type = 'A', MS_ProcedureUsed = NULL
    WHERE  MS_Caption = 'In Stock by Age' AND MS_Heading = '         Stock Reports                                                                              '

IF NOT EXISTS(SELECT 1 FROM [Server_Reports_Menu_Structure]WHERE MS_Caption = 'Movement History' AND MS_Heading = '         Movement Reports                                                                           ')
    INSERT [Server_Reports_Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status, MS_Profile_ID, MS_Report_Type, MS_ProcedureUsed )
    SELECT 10, 'A', 0, 'MovementHistory.EXE', NULL, 'Movement History', 'R55', '', '         Movement Reports                                                                           ', '      DeActive                                                                                      ', 1, 'A', NULL
ELSE
    UPDATE [Server_Reports_Menu_Structure]
    SET    MS_Order = 10, MS_Type = 'A', MS_Menu_ID = 0, MS_Application = 'MovementHistory.EXE', MS_Additional = NULL, MS_AppID = 'R55', MS_IconName = '', MS_Status = '      DeActive                                                                                      ', MS_Profile_ID = 1, MS_Report_Type = 'A', MS_ProcedureUsed = NULL
    WHERE  MS_Caption = 'Movement History' AND MS_Heading = '         Movement Reports                                                                           '

IF NOT EXISTS(SELECT 1 FROM [Server_Reports_Menu_Structure]WHERE MS_Caption = 'Flagged for Change' AND MS_Heading = '         Movement Reports                                                                           ')
    INSERT [Server_Reports_Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status, MS_Profile_ID, MS_Report_Type, MS_ProcedureUsed )
    SELECT 10, 'A', 0, 'FlaggedForChange.EXE', NULL, 'Flagged for Change', 'R56', '', '         Movement Reports                                                                           ', '      DeActive                                                                                      ', 1, 'A', NULL
ELSE
    UPDATE [Server_Reports_Menu_Structure]
    SET    MS_Order = 10, MS_Type = 'A', MS_Menu_ID = 0, MS_Application = 'FlaggedForChange.EXE', MS_Additional = NULL, MS_AppID = 'R56', MS_IconName = '', MS_Status = '      DeActive                                                                                      ', MS_Profile_ID = 1, MS_Report_Type = 'A', MS_ProcedureUsed = NULL
    WHERE  MS_Caption = 'Flagged for Change' AND MS_Heading = '         Movement Reports                                                                           '

IF NOT EXISTS(SELECT 1 FROM [Server_Reports_Menu_Structure]WHERE MS_Caption = 'Staff List' AND MS_Heading = '         Admin Reports                                                                              ')
    INSERT [Server_Reports_Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status, MS_Profile_ID, MS_Report_Type, MS_ProcedureUsed )
    SELECT 11, 'A', 0, 'ReportCaller.EXE', 'STAFFLIST', 'Staff List', 'R57', '', '         Admin Reports                                                                              ', '      Active                                                                                        ', 1, 'A', 'GetStaffList'
ELSE
    UPDATE [Server_Reports_Menu_Structure]
    SET    MS_Order = 11, MS_Type = 'A', MS_Menu_ID = 0, MS_Application = 'ReportCaller.EXE', MS_Additional = 'STAFFLIST', MS_AppID = 'R57', MS_IconName = '', MS_Status = '      Active                                                                                        ', MS_Profile_ID = 1, MS_Report_Type = 'A', MS_ProcedureUsed = 'GetStaffList'
    WHERE  MS_Caption = 'Staff List' AND MS_Heading = '         Admin Reports                                                                              '

IF NOT EXISTS(SELECT 1 FROM [Server_Reports_Menu_Structure]WHERE MS_Caption = 'Staff List (Detailed)' AND MS_Heading = '         Admin Reports                                                                              ')
    INSERT [Server_Reports_Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status, MS_Profile_ID, MS_Report_Type, MS_ProcedureUsed )
    SELECT 11, 'A', 0, 'ReportCaller.EXE', 'DETAILEDSTAFF', 'Staff List (Detailed)', 'R58', '', '         Admin Reports                                                                              ', '      Active                                                                                        ', 1, 'A', 'usp_GetDetailedStaffList'
ELSE
    UPDATE [Server_Reports_Menu_Structure]
    SET    MS_Order = 11, MS_Type = 'A', MS_Menu_ID = 0, MS_Application = 'ReportCaller.EXE', MS_Additional = 'DETAILEDSTAFF', MS_AppID = 'R58', MS_IconName = '', MS_Status = '      Active                                                                                        ', MS_Profile_ID = 1, MS_Report_Type = 'A', MS_ProcedureUsed = 'usp_GetDetailedStaffList'
    WHERE  MS_Caption = 'Staff List (Detailed)' AND MS_Heading = '         Admin Reports                                                                              '

IF NOT EXISTS(SELECT 1 FROM [Server_Reports_Menu_Structure]WHERE MS_Caption = 'Expired Calendars' AND MS_Heading = '         Admin Reports                                                                              ')
    INSERT [Server_Reports_Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status, MS_Profile_ID, MS_Report_Type, MS_ProcedureUsed )
    SELECT 11, 'A', 0, 'ReportCaller.EXE', 'EXPIREDCALENDARS', 'Expired Calendars', 'R59', '', '         Admin Reports                                                                              ', 'Active', 1, 'A', 'GetExpiredCalendarList'
ELSE
    UPDATE [Server_Reports_Menu_Structure]
    SET    MS_Order = 11, MS_Type = 'A', MS_Menu_ID = 0, MS_Application = 'ReportCaller.EXE', MS_Additional = 'EXPIREDCALENDARS', MS_AppID = 'R59', MS_IconName = '', MS_Status = 'Active', MS_Profile_ID = 1, MS_Report_Type = 'A', MS_ProcedureUsed = 'GetExpiredCalendarList'
    WHERE  MS_Caption = 'Expired Calendars' AND MS_Heading = '         Admin Reports                                                                              '

IF NOT EXISTS(SELECT 1 FROM [Server_Reports_Menu_Structure]WHERE MS_Caption = 'Meter Period Comparison Report' AND MS_Heading = '         Management Reports                                                                         ')
    INSERT [Server_Reports_Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status, MS_Profile_ID, MS_Report_Type, MS_ProcedureUsed )
    SELECT 2, 'A', 0, 'ReportCaller.EXE', 'METERYEAR', 'Meter Period Comparison Report', 'R62', NULL, '         Management Reports                                                                         ', 'Active', 1, 'R', 'rsp_REPORT_YearonYear'
ELSE
    UPDATE [Server_Reports_Menu_Structure]
    SET    MS_Order = 2, MS_Type = 'A', MS_Menu_ID = 0, MS_Application = 'ReportCaller.EXE', MS_Additional = 'METERYEAR', MS_AppID = 'R62', MS_IconName = NULL, MS_Status = 'Active', MS_Profile_ID = 1, MS_Report_Type = 'R', MS_ProcedureUsed = 'rsp_REPORT_YearonYear'
    WHERE  MS_Caption = 'Meter Period Comparison Report' AND MS_Heading = '         Management Reports                                                                         '

IF NOT EXISTS(SELECT 1 FROM [Server_Reports_Menu_Structure]WHERE MS_Caption = 'Major Prizes Report' AND MS_Heading = 'Management Reports')
    INSERT [Server_Reports_Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status, MS_Profile_ID, MS_Report_Type, MS_ProcedureUsed )
    SELECT 2, 'A', 0, 'SGVIReports.exe', 'PRIZES', 'Major Prizes Report', 'R62', '', 'Management Reports', 'Active', 1, 'A', 'rsp_GetTreasuryEntries'
ELSE
    UPDATE [Server_Reports_Menu_Structure]
    SET    MS_Order = 2, MS_Type = 'A', MS_Menu_ID = 0, MS_Application = 'SGVIReports.exe', MS_Additional = 'PRIZES', MS_AppID = 'R62', MS_IconName = '', MS_Status = 'Active', MS_Profile_ID = 1, MS_Report_Type = 'A', MS_ProcedureUsed = 'rsp_GetTreasuryEntries'
    WHERE  MS_Caption = 'Major Prizes Report' AND MS_Heading = 'Management Reports'

IF NOT EXISTS(SELECT 1 FROM [Server_Reports_Menu_Structure]WHERE MS_Caption = 'Weekly Revenue Liquidation' AND MS_Heading = 'Management Reports')
    INSERT [Server_Reports_Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status, MS_Profile_ID, MS_Report_Type, MS_ProcedureUsed )
    SELECT 2, 'A', 0, 'SGVIReports.exe', 'WEEKLYLIQUID', 'Weekly Revenue Liquidation', 'R64', '', 'Management Reports', 'Active', 1, 'A', 'rsp_SSRS_REPORT_SGVI_WRL'
ELSE
    UPDATE [Server_Reports_Menu_Structure]
    SET    MS_Order = 2, MS_Type = 'A', MS_Menu_ID = 0, MS_Application = 'SGVIReports.exe', MS_Additional = 'WEEKLYLIQUID', MS_AppID = 'R64', MS_IconName = '', MS_Status = 'Active', MS_Profile_ID = 1, MS_Report_Type = 'A', MS_ProcedureUsed = 'rsp_SSRS_REPORT_SGVI_WRL'
    WHERE  MS_Caption = 'Weekly Revenue Liquidation' AND MS_Heading = 'Management Reports'

IF NOT EXISTS(SELECT 1 FROM [Server_Reports_Menu_Structure]WHERE MS_Caption = 'Major Prizes Report' AND MS_Heading = 'SGVI Lottery')
    INSERT [Server_Reports_Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status, MS_Profile_ID, MS_Report_Type, MS_ProcedureUsed )
    SELECT 1, 'A', NULL, 'ReportCaller.EXE', 'PRIZES', 'Major Prizes Report', NULL, NULL, 'SGVI Lottery', 'Active', 2, 'A', 'rsp_GetTreasuryEntries'
ELSE
    UPDATE [Server_Reports_Menu_Structure]
    SET    MS_Order = 1, MS_Type = 'A', MS_Menu_ID = NULL, MS_Application = 'ReportCaller.EXE', MS_Additional = 'PRIZES', MS_AppID = NULL, MS_IconName = NULL, MS_Status = 'Active', MS_Profile_ID = 2, MS_Report_Type = 'A', MS_ProcedureUsed = 'rsp_GetTreasuryEntries'
    WHERE  MS_Caption = 'Major Prizes Report' AND MS_Heading = 'SGVI Lottery'

IF NOT EXISTS(SELECT 1 FROM [Server_Reports_Menu_Structure]WHERE MS_Caption = 'Weekly Revenue Liquidation' AND MS_Heading = 'SGVI Lottery')
    INSERT [Server_Reports_Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status, MS_Profile_ID, MS_Report_Type, MS_ProcedureUsed )
    SELECT 1, 'A', NULL, 'ReportCaller.EXE', 'WEEKLYLIQUID', 'Weekly Revenue Liquidation', NULL, NULL, 'SGVI Lottery', 'Active', 2, 'A', 'rsp_SSRS_REPORT_SGVI_WRL'
ELSE
    UPDATE [Server_Reports_Menu_Structure]
    SET    MS_Order = 1, MS_Type = 'A', MS_Menu_ID = NULL, MS_Application = 'ReportCaller.EXE', MS_Additional = 'WEEKLYLIQUID', MS_AppID = NULL, MS_IconName = NULL, MS_Status = 'Active', MS_Profile_ID = 2, MS_Report_Type = 'A', MS_ProcedureUsed = 'rsp_SSRS_REPORT_SGVI_WRL'
    WHERE  MS_Caption = 'Weekly Revenue Liquidation' AND MS_Heading = 'SGVI Lottery'

IF NOT EXISTS(SELECT 1 FROM [Server_Reports_Menu_Structure]WHERE MS_Caption = 'Liquidations Report' AND MS_Heading = 'Management Reports')
    INSERT [Server_Reports_Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status, MS_Profile_ID, MS_Report_Type, MS_ProcedureUsed )
    SELECT 2, 'A', NULL, 'ReportCaller.EXE', 'LIQUID', 'Liquidations Report', NULL, NULL, 'Management Reports', 'Active', 1, 'A', 'rsp_REPORT_SGVI_LiquidationDetail'
ELSE
    UPDATE [Server_Reports_Menu_Structure]
    SET    MS_Order = 2, MS_Type = 'A', MS_Menu_ID = NULL, MS_Application = 'ReportCaller.EXE', MS_Additional = 'LIQUID', MS_AppID = NULL, MS_IconName = NULL, MS_Status = 'Active', MS_Profile_ID = 1, MS_Report_Type = 'A', MS_ProcedureUsed = 'rsp_REPORT_SGVI_LiquidationDetail'
    WHERE  MS_Caption = 'Liquidations Report' AND MS_Heading = 'Management Reports'

IF NOT EXISTS(SELECT 1 FROM [Server_Reports_Menu_Structure]WHERE MS_Caption = 'Liquidations Report' AND MS_Heading = 'Management Reports')
    INSERT [Server_Reports_Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status, MS_Profile_ID, MS_Report_Type, MS_ProcedureUsed )
    SELECT 2, 'A', NULL, 'ReportCaller.EXE', 'LIQUID', 'Liquidations Report', NULL, NULL, 'Management Reports', 'Active', 1, 'A', 'rsp_REPORT_SGVI_LiquidationDetail'
ELSE
    UPDATE [Server_Reports_Menu_Structure]
    SET    MS_Order = 2, MS_Type = 'A', MS_Menu_ID = NULL, MS_Application = 'ReportCaller.EXE', MS_Additional = 'LIQUID', MS_AppID = NULL, MS_IconName = NULL, MS_Status = 'Active', MS_Profile_ID = 1, MS_Report_Type = 'A', MS_ProcedureUsed = 'rsp_REPORT_SGVI_LiquidationDetail'
    WHERE  MS_Caption = 'Liquidations Report' AND MS_Heading = 'Management Reports'

IF NOT EXISTS(SELECT 1 FROM [Server_Reports_Menu_Structure]WHERE MS_Caption = 'Liquidations Report' AND MS_Heading = 'Management Reports')
    INSERT [Server_Reports_Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status, MS_Profile_ID, MS_Report_Type, MS_ProcedureUsed )
    SELECT 2, 'A', NULL, 'ReportCaller.EXE', 'LIQUID', 'Liquidations Report', NULL, NULL, 'Management Reports', 'Active', 2, 'A', 'rsp_REPORT_SGVI_LiquidationDetail'
ELSE
    UPDATE [Server_Reports_Menu_Structure]
    SET    MS_Order = 2, MS_Type = 'A', MS_Menu_ID = NULL, MS_Application = 'ReportCaller.EXE', MS_Additional = 'LIQUID', MS_AppID = NULL, MS_IconName = NULL, MS_Status = 'Active', MS_Profile_ID = 2, MS_Report_Type = 'A', MS_ProcedureUsed = 'rsp_REPORT_SGVI_LiquidationDetail'
    WHERE  MS_Caption = 'Liquidations Report' AND MS_Heading = 'Management Reports'

IF NOT EXISTS(SELECT 1 FROM [Server_Reports_Menu_Structure]WHERE MS_Caption = 'Daily Accounting Detail Report' AND MS_Heading = 'Management Reports')
    INSERT [Server_Reports_Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status, MS_Profile_ID, MS_Report_Type, MS_ProcedureUsed )
    SELECT 2, 'A', 0, 'ReportCaller.EXE', 'DAILYACCOUNTINGDETAIL', 'Daily Accounting Detail Report', 'R65', '', 'Management Reports', 'Active', 1, 'R', 'rsp_REPORT_DailyAccounting'
ELSE
    UPDATE [Server_Reports_Menu_Structure]
    SET    MS_Order = 2, MS_Type = 'A', MS_Menu_ID = 0, MS_Application = 'ReportCaller.EXE', MS_Additional = 'DAILYACCOUNTINGDETAIL', MS_AppID = 'R65', MS_IconName = '', MS_Status = 'Active', MS_Profile_ID = 1, MS_Report_Type = 'R', MS_ProcedureUsed = 'rsp_REPORT_DailyAccounting'
    WHERE  MS_Caption = 'Daily Accounting Detail Report' AND MS_Heading = 'Management Reports'

IF NOT EXISTS(SELECT 1 FROM [Server_Reports_Menu_Structure]WHERE MS_Caption = 'Daily Accounting Summary Report' AND MS_Heading = 'Management Reports')
    INSERT [Server_Reports_Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status, MS_Profile_ID, MS_Report_Type, MS_ProcedureUsed )
    SELECT 2, 'A', 0, 'ReportCaller.EXE', 'DAILYACCOUNTINGSUMMARY', 'Daily Accounting Summary Report', 'R66', '', 'Management Reports', 'Active', 1, 'R', 'rsp_REPORT_DailyAccounting'
ELSE
    UPDATE [Server_Reports_Menu_Structure]
    SET    MS_Order = 2, MS_Type = 'A', MS_Menu_ID = 0, MS_Application = 'ReportCaller.EXE', MS_Additional = 'DAILYACCOUNTINGSUMMARY', MS_AppID = 'R66', MS_IconName = '', MS_Status = 'Active', MS_Profile_ID = 1, MS_Report_Type = 'R', MS_ProcedureUsed = 'rsp_REPORT_DailyAccounting'
    WHERE  MS_Caption = 'Daily Accounting Summary Report' AND MS_Heading = 'Management Reports'

IF NOT EXISTS(SELECT 1 FROM [Server_Reports_Menu_Structure]WHERE MS_Caption = 'Exception Report' AND MS_Heading = 'Management Reports')
    INSERT [Server_Reports_Menu_Structure] ( MS_Order, MS_Type, MS_Menu_ID, MS_Application, MS_Additional, MS_Caption, MS_AppID, MS_IconName, MS_Heading, MS_Status, MS_Profile_ID, MS_Report_Type, MS_ProcedureUsed )
    SELECT 3, 'E', 0, NULL, 'EXCEPTION', 'Exception Report', 'R65', '', 'Management Reports', 'Active', 1, 'A', NULL
ELSE
    UPDATE [Server_Reports_Menu_Structure]
    SET    MS_Order = 3, MS_Type = 'E', MS_Menu_ID = 0, MS_Application = NULL, MS_Additional = 'EXCEPTION', MS_AppID = 'R65', MS_IconName = '', MS_Status = 'Active', MS_Profile_ID = 1, MS_Report_Type = 'A', MS_ProcedureUsed = NULL
    WHERE  MS_Caption = 'Exception Report' AND MS_Heading = 'Management Reports'
    
GO