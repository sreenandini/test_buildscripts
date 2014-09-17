USE [Enterprise]
GO

IF NOT EXISTS(SELECT 1 FROM [Setting]WHERE Setting_Name = 'BGSAdminWSUserID')
    INSERT [Setting] ( Setting_Name, Setting_Value )
    SELECT 'BGSAdminWSUserID', 'BallyOneUK'
ELSE
    UPDATE [Setting]
    SET    Setting_Value = 'BallyOneUK'
    WHERE  Setting_Name = 'BGSAdminWSUserID'

IF NOT EXISTS(SELECT 1 FROM [Setting]WHERE Setting_Name = 'BGSAdminWSPwd')
    INSERT [Setting] ( Setting_Name, Setting_Value )
    SELECT 'BGSAdminWSPwd', 'BallyOneUK'
ELSE
    UPDATE [Setting]
    SET    Setting_Value = 'BallyOneUK'
    WHERE  Setting_Name = 'BGSAdminWSPwd'

IF NOT EXISTS(SELECT 1 FROM [Setting]WHERE Setting_Name = 'SGVI_Enabled')
    INSERT [Setting] ( Setting_Name, Setting_Value )
    SELECT 'SGVI_Enabled', 'False'
ELSE
    UPDATE [Setting]
    SET    Setting_Value = 'False'
    WHERE  Setting_Name = 'SGVI_Enabled'

IF NOT EXISTS(SELECT 1 FROM [Setting]WHERE Setting_Name = 'SGVI_Payment_Days')
    INSERT [Setting] ( Setting_Name, Setting_Value )
    SELECT 'SGVI_Payment_Days', '10'
ELSE
    UPDATE [Setting]
    SET    Setting_Value = '10'
    WHERE  Setting_Name = 'SGVI_Payment_Days'

IF NOT EXISTS(SELECT 1 FROM [Setting]WHERE Setting_Name = 'SGVI_Statement_Number')
    INSERT [Setting] ( Setting_Name, Setting_Value )
    SELECT 'SGVI_Statement_Number', '2'
ELSE
    UPDATE [Setting]
    SET    Setting_Value = '2'
    WHERE  Setting_Name = 'SGVI_Statement_Number'

IF NOT EXISTS(SELECT 1 FROM [Setting]WHERE Setting_Name = 'SGVI_Batch_Net_Value')
    INSERT [Setting] ( Setting_Name, Setting_Value )
    SELECT 'SGVI_Batch_Net_Value', '0.22'
ELSE
    UPDATE [Setting]
    SET    Setting_Value = '0.22'
    WHERE  Setting_Name = 'SGVI_Batch_Net_Value'

IF NOT EXISTS(SELECT 1 FROM [Setting]WHERE Setting_Name = 'ReportServerURL')
    INSERT [Setting] ( Setting_Name, Setting_Value )
    SELECT 'ReportServerURL', 'localhost'
ELSE
    UPDATE [Setting]
    SET    Setting_Value = 'localhost'
    WHERE  Setting_Name = 'ReportServerURL'

IF NOT EXISTS(SELECT 1 FROM [Setting]WHERE Setting_Name = 'ReportFolder')
    INSERT [Setting] ( Setting_Name, Setting_Value )
    SELECT 'ReportFolder', 'BMCReports'
ELSE
    UPDATE [Setting]
    SET    Setting_Value = 'BMCReports'
    WHERE  Setting_Name = 'ReportFolder'

IF NOT EXISTS(SELECT 1 FROM [Setting]WHERE Setting_Name = 'EmptyReportMessage')
    INSERT [Setting] ( Setting_Name, Setting_Value )
    SELECT 'EmptyReportMessage', 'No data available for the selected period'
ELSE
    UPDATE [Setting]
    SET    Setting_Value = 'No data available for the selected period'
    WHERE  Setting_Name = 'EmptyReportMessage'

IF NOT EXISTS(SELECT 1 FROM [Setting]WHERE Setting_Name = 'IsRegulatoryEnabled')
    INSERT [Setting] ( Setting_Name, Setting_Value )
    SELECT 'IsRegulatoryEnabled', 'False'
ELSE
    UPDATE [Setting]
    SET    Setting_Value = 'False'
    WHERE  Setting_Name = 'IsRegulatoryEnabled'

IF NOT EXISTS(SELECT 1 FROM [Setting]WHERE Setting_Name = 'RegulatoryType')
    INSERT [Setting] ( Setting_Name, Setting_Value )
    SELECT 'RegulatoryType', ''
ELSE
    UPDATE [Setting]
    SET    Setting_Value = ''
    WHERE  Setting_Name = 'RegulatoryType'

IF NOT EXISTS(SELECT 1 FROM [Setting]WHERE Setting_Name = 'AUTHORIZATION_KEY_EXPIRY_HOURS')
    INSERT [Setting] ( Setting_Name, Setting_Value )
    SELECT 'AUTHORIZATION_KEY_EXPIRY_HOURS', '1'
ELSE
    UPDATE [Setting]
    SET    Setting_Value = '1'
    WHERE  Setting_Name = 'AUTHORIZATION_KEY_EXPIRY_HOURS'

IF NOT EXISTS(SELECT 1 FROM [Setting]WHERE Setting_Name = 'IsAAMSApprovalForSiteRequired')
    INSERT [Setting] ( Setting_Name, Setting_Value )
    SELECT 'IsAAMSApprovalForSiteRequired', 'False'
ELSE
    UPDATE [Setting]
    SET    Setting_Value = 'False'
    WHERE  Setting_Name = 'IsAAMSApprovalForSiteRequired'

IF NOT EXISTS(SELECT 1 FROM [Setting]WHERE Setting_Name = 'LGEConnectionDetails')
    INSERT [Setting] ( Setting_Name, Setting_Value )
    SELECT 'LGEConnectionDetails', 'server=;username=;password=;database=;'
ELSE
    UPDATE [Setting]
    SET    Setting_Value = 'server=;username=;password=;database=;'
    WHERE  Setting_Name = 'LGEConnectionDetails'

IF NOT EXISTS(SELECT 1 FROM [Setting]WHERE Setting_Name = 'BASWebService')
    INSERT [Setting] ( Setting_Name, Setting_Value )
    SELECT 'BASWebService', 'http://servername/'
ELSE
    UPDATE [Setting]
    SET    Setting_Value = 'http://servername/'
    WHERE  Setting_Name = 'BASWebService'

IF NOT EXISTS(SELECT 1 FROM [Setting]WHERE Setting_Name = 'LGEServer')
    INSERT [Setting] ( Setting_Name, Setting_Value )
    SELECT 'LGEServer', ''
ELSE
    UPDATE [Setting]
    SET    Setting_Value = ''
    WHERE  Setting_Name = 'LGEServer'

IF NOT EXISTS(SELECT 1 FROM [Setting]WHERE Setting_Name = 'LGEDatabase')
    INSERT [Setting] ( Setting_Name, Setting_Value )
    SELECT 'LGEDatabase', ''
ELSE
    UPDATE [Setting]
    SET    Setting_Value = ''
    WHERE  Setting_Name = 'LGEDatabase'

IF NOT EXISTS(SELECT 1 FROM [Setting]WHERE Setting_Name = 'LGEEnabled')
    INSERT [Setting] ( Setting_Name, Setting_Value )
    SELECT 'LGEEnabled', 'False'
ELSE
    UPDATE [Setting]
    SET    Setting_Value = 'False'
    WHERE  Setting_Name = 'LGEEnabled'

IF NOT EXISTS(SELECT 1 FROM [Setting]WHERE Setting_Name = 'SenderCode')
    INSERT [Setting] ( Setting_Name, Setting_Value )
    SELECT 'SenderCode', 'BMC'
ELSE
    UPDATE [Setting]
    SET    Setting_Value = 'BMC'
    WHERE  Setting_Name = 'SenderCode'

IF NOT EXISTS(SELECT 1 FROM [Setting]WHERE Setting_Name = 'IsLGERequired')
    INSERT [Setting] ( Setting_Name, Setting_Value )
    SELECT 'IsLGERequired', 'False'
ELSE
    UPDATE [Setting]
    SET    Setting_Value = 'False'
    WHERE  Setting_Name = 'IsLGERequired'

IF NOT EXISTS(SELECT 1 FROM [Setting]WHERE Setting_Name = 'IsAuditingEnabled')
    INSERT [Setting] ( Setting_Name, Setting_Value )
    SELECT 'IsAuditingEnabled', 'True'
ELSE
    UPDATE [Setting]
    SET    Setting_Value = 'True'
    WHERE  Setting_Name = 'IsAuditingEnabled'

IF NOT EXISTS(SELECT 1 FROM [Setting]WHERE Setting_Name = 'Client')
    INSERT [Setting] ( Setting_Name, Setting_Value )
    SELECT 'Client', 'Winchells'
ELSE
    UPDATE [Setting]
    SET    Setting_Value = 'Winchells'
    WHERE  Setting_Name = 'Client'

IF NOT EXISTS(SELECT 1 FROM [Setting]WHERE Setting_Name = 'BMC_Reports_Header')
    INSERT [Setting] ( Setting_Name, Setting_Value )
    SELECT 'BMC_Reports_Header', 'BMC Main Header'
ELSE
    UPDATE [Setting]
    SET    Setting_Value = 'BMC Main Header'
    WHERE  Setting_Name = 'BMC_Reports_Header'

IF NOT EXISTS(SELECT 1 FROM [Setting]WHERE Setting_Name = 'BMC_Reports_Language')
    INSERT [Setting] ( Setting_Name, Setting_Value )
    SELECT 'BMC_Reports_Language', 'en-GB'
ELSE
    UPDATE [Setting]
    SET    Setting_Value = 'en-GB'
    WHERE  Setting_Name = 'BMC_Reports_Language'

IF NOT EXISTS(SELECT 1 FROM [Setting]WHERE Setting_Name = 'MaxHandPayAuthRequired')
    INSERT [Setting] ( Setting_Name, Setting_Value )
    SELECT 'MaxHandPayAuthRequired', 'True'
ELSE
    UPDATE [Setting]
    SET    Setting_Value = 'True'
    WHERE  Setting_Name = 'MaxHandPayAuthRequired'

IF NOT EXISTS(SELECT 1 FROM [Setting]WHERE Setting_Name = 'ManualEntryTicketValidation')
    INSERT [Setting] ( Setting_Name, Setting_Value )
    SELECT 'ManualEntryTicketValidation', 'True'
ELSE
    UPDATE [Setting]
    SET    Setting_Value = 'True'
    WHERE  Setting_Name = 'ManualEntryTicketValidation'

IF NOT EXISTS(SELECT 1 FROM [Setting]WHERE Setting_Name = 'SlotLifeToDate')
    INSERT [Setting] ( Setting_Name, Setting_Value )
    SELECT 'SlotLifeToDate', 'False'
ELSE
    UPDATE [Setting]
    SET    Setting_Value = 'False'
    WHERE  Setting_Name = 'SlotLifeToDate'

IF NOT EXISTS(SELECT 1 FROM [Setting]WHERE Setting_Name = 'AllowPartNumberEdit')
    INSERT [Setting] ( Setting_Name, Setting_Value )
    SELECT 'AllowPartNumberEdit', 'False'
ELSE
    UPDATE [Setting]
    SET    Setting_Value = 'False'
    WHERE  Setting_Name = 'AllowPartNumberEdit'

IF NOT EXISTS(SELECT 1 FROM [Setting]WHERE Setting_Name = 'RedeemTicketCustomer_Min')
    INSERT [Setting] ( Setting_Name, Setting_Value )
    SELECT 'RedeemTicketCustomer_Min', '1000'
ELSE
    UPDATE [Setting]
    SET    Setting_Value = '1000'
    WHERE  Setting_Name = 'RedeemTicketCustomer_Min'

IF NOT EXISTS(SELECT 1 FROM [Setting]WHERE Setting_Name = 'RedeemTicketCustomer_Max')
    INSERT [Setting] ( Setting_Name, Setting_Value )
    SELECT 'RedeemTicketCustomer_Max', '4999'
ELSE
    UPDATE [Setting]
    SET    Setting_Value = '4999'
    WHERE  Setting_Name = 'RedeemTicketCustomer_Max'

IF NOT EXISTS(SELECT 1 FROM [Setting]WHERE Setting_Name = 'RedeemTicketCustomer_BankAcctNo')
    INSERT [Setting] ( Setting_Name, Setting_Value )
    SELECT 'RedeemTicketCustomer_BankAcctNo', '5000'
ELSE
    UPDATE [Setting]
    SET    Setting_Value = '5000'
    WHERE  Setting_Name = 'RedeemTicketCustomer_BankAcctNo'

IF NOT EXISTS(SELECT 1 FROM [Setting]WHERE Setting_Name = 'WindowsServices')
    INSERT [Setting] ( Setting_Name, Setting_Value )
    SELECT 'WindowsServices', 'BMCEnterpriseWindowsServiceInstaller,BMC.ExportDataToSiteService,BMC.MonitoringService,BMC.Utilities'
ELSE
    UPDATE [Setting]
    SET    Setting_Value = 'BMCEnterpriseWindowsServiceInstaller,BMC.ExportDataToSiteService,BMC.MonitoringService,BMC.Utilities'
    WHERE  Setting_Name = 'WindowsServices'

IF NOT EXISTS(SELECT 1 FROM [Setting]WHERE Setting_Name = 'IsAFTEnabledForSite')
    INSERT [Setting] ( Setting_Name, Setting_Value )
    SELECT 'IsAFTEnabledForSite', 'TRUE'
ELSE
    UPDATE [Setting]
    SET    Setting_Value = 'TRUE'
    WHERE  Setting_Name = 'IsAFTEnabledForSite'

IF NOT EXISTS(SELECT 1 FROM [Setting]WHERE Setting_Name = 'IsPowerPromoReportsRequired')
    INSERT [Setting] ( Setting_Name, Setting_Value )
    SELECT 'IsPowerPromoReportsRequired', 'TRUE'
ELSE
    UPDATE [Setting]
    SET    Setting_Value = 'TRUE'
    WHERE  Setting_Name = 'IsPowerPromoReportsRequired'

IF NOT EXISTS(SELECT 1 FROM [Setting]WHERE Setting_Name = 'MachineMaintenance')
    INSERT [Setting] ( Setting_Name, Setting_Value )
    SELECT 'MachineMaintenance', 'False'
ELSE
    UPDATE [Setting]
    SET    Setting_Value = 'False'
    WHERE  Setting_Name = 'MachineMaintenance'

IF NOT EXISTS(SELECT 1 FROM [Setting]WHERE Setting_Name = 'CertificateIssuer')
    INSERT [Setting] ( Setting_Name, Setting_Value )
    SELECT 'CertificateIssuer', 'VeriSign'
ELSE
    UPDATE [Setting]
    SET    Setting_Value = 'VeriSign'
    WHERE  Setting_Name = 'CertificateIssuer'

IF NOT EXISTS(SELECT 1 FROM [Setting]WHERE Setting_Name = 'IsCertificateRequired')
    INSERT [Setting] ( Setting_Name, Setting_Value )
    SELECT 'IsCertificateRequired', 'False'
ELSE
    UPDATE [Setting]
    SET    Setting_Value = 'False'
    WHERE  Setting_Name = 'IsCertificateRequired'

IF NOT EXISTS(SELECT 1 FROM [Setting]WHERE Setting_Name = 'ComponentVerification')
    INSERT [Setting] ( Setting_Name, Setting_Value )
    SELECT 'ComponentVerification', 'False'
ELSE
    UPDATE [Setting]
    SET    Setting_Value = 'False'
    WHERE  Setting_Name = 'ComponentVerification'

IF NOT EXISTS(SELECT 1 FROM [Setting]WHERE Setting_Name = 'GuardianServerIPAddress')
    INSERT [Setting] ( Setting_Name, Setting_Value )
    SELECT 'GuardianServerIPAddress', ''
ELSE
    UPDATE [Setting]
    SET    Setting_Value = ''
    WHERE  Setting_Name = 'GuardianServerIPAddress'

IF NOT EXISTS(SELECT 1 FROM [Setting]WHERE Setting_Name = 'IsMeterAdjustmentToolRequired')
    INSERT [Setting] ( Setting_Name, Setting_Value )
    SELECT 'IsMeterAdjustmentToolRequired', 'False'
ELSE
    UPDATE [Setting]
    SET    Setting_Value = 'False'
    WHERE  Setting_Name = 'IsMeterAdjustmentToolRequired'

IF NOT EXISTS(SELECT 1 FROM [Setting]WHERE Setting_Name = 'LiveMeter')
    INSERT [Setting] ( Setting_Name, Setting_Value )
    SELECT 'LiveMeter', 'False'
ELSE
    UPDATE [Setting]
    SET    Setting_Value = 'False'
    WHERE  Setting_Name = 'LiveMeter'

IF NOT EXISTS(SELECT 1 FROM [Setting]WHERE Setting_Name = 'ClearEventsOnFinalDrop')
    INSERT [Setting] ( Setting_Name, Setting_Value )
    SELECT 'ClearEventsOnFinalDrop', 'MANUAL'
ELSE
    UPDATE [Setting]
    SET    Setting_Value = 'MANUAL'
    WHERE  Setting_Name = 'ClearEventsOnFinalDrop'

IF NOT EXISTS(SELECT 1 FROM [Setting]WHERE Setting_Name = 'Auto_Declare_Monies')
    INSERT [Setting] ( Setting_Name, Setting_Value )
    SELECT 'Auto_Declare_Monies', 'TRUE'
ELSE
    UPDATE [Setting]
    SET    Setting_Value = 'TRUE'
    WHERE  Setting_Name = 'Auto_Declare_Monies'

IF NOT EXISTS(SELECT 1 FROM [Setting]WHERE Setting_Name = 'IsAFTIncludedInCalculation')
    INSERT [Setting] ( Setting_Name, Setting_Value )
    SELECT 'IsAFTIncludedInCalculation', 'True'
ELSE
    UPDATE [Setting]
    SET    Setting_Value = 'True'
    WHERE  Setting_Name = 'IsAFTIncludedInCalculation'

IF NOT EXISTS(SELECT 1 FROM [Setting]WHERE Setting_Name = 'TreasuryLimitForMajorPrizes')
    INSERT [Setting] ( Setting_Name, Setting_Value )
    SELECT 'TreasuryLimitForMajorPrizes', '1200'
ELSE
    UPDATE [Setting]
    SET    Setting_Value = '1200'
    WHERE  Setting_Name = 'TreasuryLimitForMajorPrizes'

IF NOT EXISTS(SELECT 1 FROM [Setting]WHERE Setting_Name = 'SHOWHANDPAYCODE')
    INSERT [Setting] ( Setting_Name, Setting_Value )
    SELECT 'SHOWHANDPAYCODE', 'TRUE'
ELSE
    UPDATE [Setting]
    SET    Setting_Value = 'TRUE'
    WHERE  Setting_Name = 'SHOWHANDPAYCODE'

IF NOT EXISTS(SELECT 1 FROM [Setting]WHERE Setting_Name = 'CheckForGamePartNumber')
    INSERT [Setting] ( Setting_Name, Setting_Value )
    SELECT 'CheckForGamePartNumber', 'False'
ELSE
    UPDATE [Setting]
    SET    Setting_Value = 'False'
    WHERE  Setting_Name = 'CheckForGamePartNumber'

IF NOT EXISTS(SELECT 1 FROM [Setting]WHERE Setting_Name = 'CentralizedDeclaration')
    INSERT [Setting] ( Setting_Name, Setting_Value )
    SELECT 'CentralizedDeclaration', 'True'
ELSE
    UPDATE [Setting]
    SET    Setting_Value = 'True'
    WHERE  Setting_Name = 'CentralizedDeclaration'

IF NOT EXISTS(SELECT 1 FROM [Setting]WHERE Setting_Name = 'IsTransmitterEnabled')
    INSERT [Setting] ( Setting_Name, Setting_Value )
    SELECT 'IsTransmitterEnabled', '0'
ELSE
    UPDATE [Setting]
    SET    Setting_Value = '0'
    WHERE  Setting_Name = 'IsTransmitterEnabled'

IF NOT EXISTS(SELECT 1 FROM [Setting]WHERE Setting_Name = 'STMServerIP')
    INSERT [Setting] ( Setting_Name, Setting_Value )
    SELECT 'STMServerIP', ''
ELSE
    UPDATE [Setting]
    SET    Setting_Value = ''
    WHERE  Setting_Name = 'STMServerIP'

IF NOT EXISTS(SELECT 1 FROM [Setting]WHERE Setting_Name = 'StackerLevelAlert')
    INSERT [Setting] ( Setting_Name, Setting_Value )
    SELECT 'StackerLevelAlert', 'True'
ELSE
    UPDATE [Setting]
    SET    Setting_Value = 'True'
    WHERE  Setting_Name = 'StackerLevelAlert'

IF NOT EXISTS(SELECT 1 FROM [Setting]WHERE Setting_Name = 'DropScheduleAlert')
    INSERT [Setting] ( Setting_Name, Setting_Value )
    SELECT 'DropScheduleAlert', 'True'
ELSE
    UPDATE [Setting]
    SET    Setting_Value = 'True'
    WHERE  Setting_Name = 'DropScheduleAlert'

IF NOT EXISTS(SELECT 1 FROM [Setting]WHERE Setting_Name = 'AllowOfflineDeclaration')
    INSERT [Setting] ( Setting_Name, Setting_Value )
    SELECT 'AllowOfflineDeclaration', 'True'
ELSE
    UPDATE [Setting]
    SET    Setting_Value = 'True'
    WHERE  Setting_Name = 'AllowOfflineDeclaration'

IF NOT EXISTS(SELECT 1 FROM [Setting]WHERE Setting_Name = 'DeclarationAlert')
    INSERT [Setting] ( Setting_Name, Setting_Value )
    SELECT 'DeclarationAlert', 'True'
ELSE
    UPDATE [Setting]
    SET    Setting_Value = 'True'
    WHERE  Setting_Name = 'DeclarationAlert'

IF NOT EXISTS(SELECT 1 FROM [Setting]WHERE Setting_Name = 'MinuteThreadCheckinHoursforAutoDrop')
    INSERT [Setting] ( Setting_Name, Setting_Value )
    SELECT 'MinuteThreadCheckinHoursforAutoDrop', '1'
ELSE
    UPDATE [Setting]
    SET    Setting_Value = '1'
    WHERE  Setting_Name = 'MinuteThreadCheckinHoursforAutoDrop'

IF NOT EXISTS(SELECT 1 FROM [Setting]WHERE Setting_Name = 'RetryMinutesForCheckDB')
    INSERT [Setting] ( Setting_Name, Setting_Value )
    SELECT 'RetryMinutesForCheckDB', '5'
ELSE
    UPDATE [Setting]
    SET    Setting_Value = '5'
    WHERE  Setting_Name = 'RetryMinutesForCheckDB'

IF NOT EXISTS(SELECT 1 FROM [Setting]WHERE Setting_Name = 'StackerFeature')
    INSERT [Setting] ( Setting_Name, Setting_Value )
    SELECT 'StackerFeature', 'False'
ELSE
    UPDATE [Setting]
    SET    Setting_Value = 'False'
    WHERE  Setting_Name = 'StackerFeature'

IF NOT EXISTS(SELECT 1 FROM [Setting]WHERE Setting_Name = 'IsEmployeeCardTrackingEnabled')
    INSERT [Setting] ( Setting_Name, Setting_Value )
    SELECT 'IsEmployeeCardTrackingEnabled', 'False'
ELSE
    UPDATE [Setting]
    SET    Setting_Value = 'False'
    WHERE  Setting_Name = 'IsEmployeeCardTrackingEnabled'

IF NOT EXISTS(SELECT 1 FROM [Setting]WHERE Setting_Name = 'AddShortpayInVoucherOut')
    INSERT [Setting] ( Setting_Name, Setting_Value )
    SELECT 'AddShortpayInVoucherOut', 'False'
ELSE
    UPDATE [Setting]
    SET    Setting_Value = 'False'
    WHERE  Setting_Name = 'AddShortpayInVoucherOut'

IF NOT EXISTS(SELECT 1 FROM [Setting]WHERE Setting_Name = 'IsSiteLicensingEnabled')
    INSERT [Setting] ( Setting_Name, Setting_Value )
    SELECT 'IsSiteLicensingEnabled', 'True'
ELSE
    UPDATE [Setting]
    SET    Setting_Value = 'True'
    WHERE  Setting_Name = 'IsSiteLicensingEnabled'

IF NOT EXISTS(SELECT 1 FROM [Setting]WHERE Setting_Name = 'LiquidationProfitShare')
    INSERT [Setting] ( Setting_Name, Setting_Value )
    SELECT 'LiquidationProfitShare', 'True'
ELSE
    UPDATE [Setting]
    SET    Setting_Value = 'True'
    WHERE  Setting_Name = 'LiquidationProfitShare'

IF NOT EXISTS(SELECT 1 FROM [Setting]WHERE Setting_Name = 'UseAssetTemplate')
    INSERT [Setting] ( Setting_Name, Setting_Value )
    SELECT 'UseAssetTemplate', 'True'
ELSE
    UPDATE [Setting]
    SET    Setting_Value = 'True'
    WHERE  Setting_Name = 'UseAssetTemplate'

IF NOT EXISTS(SELECT 1 FROM [Setting]WHERE Setting_Name = 'CentralizedReadLiquidation')
    INSERT [Setting] ( Setting_Name, Setting_Value )
    SELECT 'CentralizedReadLiquidation', 'True'
ELSE
    UPDATE [Setting]
    SET    Setting_Value = 'True'
    WHERE  Setting_Name = 'CentralizedReadLiquidation'
   
IF NOT EXISTS(SELECT 1 FROM [Setting]WHERE Setting_Name = 'AGSSerialNumberAlphaNumeric')
    INSERT [Setting] ( Setting_Name, Setting_Value )
    SELECT 'AGSSerialNumberAlphaNumeric', 'False'
ELSE
    UPDATE [Setting]
    SET    Setting_Value = 'False'
    WHERE  Setting_Name = 'AGSSerialNumberAlphaNumeric' 
    
IF NOT EXISTS(SELECT 1 FROM [Setting]WHERE Setting_Name = 'AllowDeMerge')
    INSERT [Setting] ( Setting_Name, Setting_Value )
    SELECT 'AllowDeMerge', 'False'
ELSE
    UPDATE [Setting]
    SET    Setting_Value = 'False'
    WHERE  Setting_Name = 'AllowDeMerge'    
    
    
IF NOT EXISTS(SELECT 1 FROM [Setting]WHERE Setting_Name = 'IsEnrolmentFlag')
    INSERT [Setting] ( Setting_Name, Setting_Value )
    SELECT 'IsEnrolmentFlag', 'True'
ELSE
    UPDATE [Setting]
    SET    Setting_Value = 'True'
    WHERE  Setting_Name = 'IsEnrolmentFlag'
    
IF NOT EXISTS(SELECT 1 FROM [Setting]WHERE Setting_Name = 'Login_Expiry_No_of_Days')
    INSERT [Setting] ( Setting_Name, Setting_Value )
    SELECT 'Login_Expiry_No_of_Days', 60
ELSE
    UPDATE [Setting]
    SET    Setting_Value = 60
    WHERE  Setting_Name = 'Login_Expiry_No_of_Days'
    
IF NOT EXISTS(SELECT 1 FROM [Setting]WHERE Setting_Name = 'Login_Max_No_Of_Attempts')
    INSERT [Setting] ( Setting_Name, Setting_Value )
    SELECT 'Login_Max_No_Of_Attempts', 3
ELSE
    UPDATE [Setting]
    SET    Setting_Value = 3
    WHERE  Setting_Name = 'Login_Max_No_Of_Attempts' 
    
IF NOT EXISTS(SELECT 1 FROM [Setting]WHERE Setting_Name = 'PRODUCTVERSION')
    INSERT [Setting] ( Setting_Name, Setting_Value )
    SELECT 'PRODUCTVERSION', '12.5'
ELSE
    UPDATE [Setting]
    SET    Setting_Value = '12.5'
    WHERE  Setting_Name = 'PRODUCTVERSION' 
    
IF NOT EXISTS(SELECT 1 FROM [Setting]WHERE Setting_Name = 'STMServerPort')
    INSERT [Setting] ( Setting_Name, Setting_Value )
    SELECT 'STMServerPort', ''
ELSE
    UPDATE [Setting]
    SET    Setting_Value = ''
    WHERE  Setting_Name = 'STMServerPort'    	
	

--Enrolment Flag-AGS Combination
IF NOT EXISTS(SELECT Setting_Name FROM Setting WHERE Setting_Name = 'IsEnrolmentComplete')
  INSERT INTO Setting(Setting_Name,Setting_Value) VALUES('IsEnrolmentComplete','False')
ELSE
  UPDATE Setting SET Setting_Value = 'False' WHERE Setting_Name = 'IsEnrolmentComplete'
GO

IF NOT EXISTS(SELECT Setting_Name FROM Setting WHERE Setting_Name = 'AGSValue')
  INSERT INTO Setting(Setting_Name,Setting_Value) VALUES('AGSValue','16')
ELSE
  UPDATE Setting SET Setting_Value = '16' WHERE Setting_Name = 'AGSValue'

Go
  
IF NOT EXISTS(SELECT Setting_Name FROM Setting WHERE Setting_Name = 'IsSingleCardEmployee')
  INSERT INTO Setting(Setting_Name,Setting_Value) VALUES('IsSingleCardEmployee','False')
ELSE
  UPDATE Setting SET Setting_Value = 'False' WHERE Setting_Name = 'IsSingleCardEmployee'

GO

IF NOT EXISTS(SELECT Setting_Name FROM Setting WHERE Setting_Name = 'MaxNoOfCardsForEmployee')
  INSERT INTO Setting(Setting_Name,Setting_Value) VALUES('MaxNoOfCardsForEmployee','5')
ELSE
  UPDATE Setting SET Setting_Value = '5' WHERE Setting_Name = 'MaxNoOfCardsForEmployee'

GO

--Import/Export Asset File
 
IF NOT EXISTS(SELECT Setting_Name FROM Setting WHERE Setting_Name = 'ImportExport_AssetFile')
  INSERT INTO Setting(Setting_Name,Setting_Value) VALUES('ImportExport_AssetFile','False')
ELSE
  UPDATE Setting SET 
  Setting_Value = 'False'   WHERE
  Setting_Name = 'ImportExport_AssetFile'
  
GO

IF NOT EXISTS(SELECT 1 FROM [Setting]WHERE Setting_Name = 'VIEWPARTIALLYCONFIGUREDSITES')
    INSERT [Setting] ( Setting_Name, Setting_Value )
    SELECT 'VIEWPARTIALLYCONFIGUREDSITES', 'TRUE'
ELSE
    UPDATE [Setting]
    SET    Setting_Value = 'TRUE'
    WHERE  Setting_Name = 'VIEWPARTIALLYCONFIGUREDSITES'

GO

IF NOT EXISTS(SELECT Setting_Name FROM Setting WHERE Setting_Name = 'ValidateAGSForGMU')
  INSERT INTO Setting(Setting_Name,Setting_Value) VALUES('ValidateAGSForGMU','True')
ELSE
  UPDATE Setting SET 
  Setting_Value = 'True'   WHERE
  Setting_Name = 'ValidateAGSForGMU'

GO

IF NOT EXISTS(SELECT Setting_Name FROM Setting WHERE Setting_Name = 'Add_PromoCashable_In_WinLoss')
	INSERT INTO Setting(Setting_Name,Setting_Value) VALUES('Add_PromoCashable_In_WinLoss', 'True')
ELSE
	UPDATE Setting SET Setting_Value = 'True' WHERE Setting_Name = 'Add_PromoCashable_In_WinLoss'
GO

IF NOT EXISTS(SELECT Setting_Name FROM Setting WHERE Setting_Name = 'IncludeRareBills')
	INSERT INTO Setting(Setting_Name,Setting_Value) VALUES('IncludeRareBills', 'False')
ELSE
	UPDATE Setting SET Setting_Value = 'False' WHERE Setting_Name = 'IncludeRareBills'
GO

IF NOT EXISTS(SELECT Setting_Name FROM Setting WHERE Setting_Name = 'IsGameCappingEnabled')
	INSERT INTO Setting(Setting_Name,Setting_Value) VALUES('IsGameCappingEnabled', 'TRUE')
ELSE
	UPDATE Setting SET Setting_Value = 'TRUE' WHERE Setting_Name = 'IsGameCappingEnabled'
GO

IF NOT EXISTS(SELECT Setting_Name FROM Setting WHERE Setting_Name = 'IsVaultEnabled')
	INSERT INTO Setting(Setting_Name,Setting_Value) VALUES('IsVaultEnabled', 'True')
ELSE
	UPDATE Setting SET Setting_Value = 'True' WHERE Setting_Name = 'IsVaultEnabled'
GO

IF NOT EXISTS(SELECT Setting_Name FROM Setting WHERE Setting_Name = 'MaxNoOfVaultCassettes')
	INSERT INTO Setting(Setting_Name,Setting_Value) VALUES('MaxNoOfVaultCassettes', '6')
ELSE
	UPDATE Setting SET Setting_Value = '6' WHERE Setting_Name = 'MaxNoOfVaultCassettes'
GO

IF NOT EXISTS(SELECT Setting_Name FROM Setting WHERE Setting_Name = 'MaxNoOfVaultHoppers')
	INSERT INTO Setting(Setting_Name,Setting_Value) VALUES('MaxNoOfVaultHoppers', '3')
ELSE
	UPDATE Setting SET Setting_Value = '3' WHERE Setting_Name = 'MaxNoOfVaultHoppers'
GO

IF NOT EXISTS(SELECT Setting_Name FROM Setting WHERE Setting_Name = 'IsBillCounterAmountEditable')
	INSERT INTO Setting(Setting_Name,Setting_Value) VALUES('IsBillCounterAmountEditable', 'False')
ELSE
	UPDATE Setting SET Setting_Value = 'False' WHERE Setting_Name = 'IsBillCounterAmountEditable'
GO

IF NOT EXISTS(SELECT Setting_Name FROM Setting WHERE Setting_Name = 'HrchyFilterInReports')
	INSERT INTO Setting(Setting_Name,Setting_Value) VALUES('HrchyFilterInReports', 'True')
ELSE
	UPDATE Setting SET Setting_Value = 'True' WHERE Setting_Name = 'HrchyFilterInReports'
GO

IF NOT EXISTS(SELECT Setting_Name FROM Setting WHERE Setting_Name = 'Vault_AutoPopulateDropValues')
	INSERT INTO Setting(Setting_Name,Setting_Value) VALUES('Vault_AutoPopulateDropValues', 'True')
ELSE
	UPDATE Setting SET Setting_Value = 'False' WHERE Setting_Name = 'Vault_AutoPopulateDropValues'
GO

IF NOT EXISTS(SELECT Setting_Name FROM Setting WHERE Setting_Name = 'Vault_EndDeviceOnTerminate')
	INSERT INTO Setting(Setting_Name,Setting_Value) VALUES('Vault_EndDeviceOnTerminate', 'True')
ELSE
	UPDATE Setting SET Setting_Value = 'True' WHERE Setting_Name = 'Vault_EndDeviceOnTerminate'
GO

IF NOT EXISTS(SELECT Setting_Name FROM Setting WHERE Setting_Name = 'DropSheduleAlert')
	INSERT INTO Setting(Setting_Name,Setting_Value) VALUES('DropSheduleAlert', 'True')
ELSE
	UPDATE Setting SET Setting_Value = 'True' WHERE Setting_Name = 'DropSheduleAlert'
GO

IF NOT EXISTS(SELECT Setting_Name FROM Setting WHERE Setting_Name = 'IsCrossTicketingEnabled')
	INSERT INTO Setting(Setting_Name,Setting_Value) VALUES('IsCrossTicketingEnabled', 'False')
ELSE
	UPDATE Setting SET Setting_Value = 'False' WHERE Setting_Name = 'IsCrossTicketingEnabled'
GO

IF NOT EXISTS(SELECT Setting_Name FROM Setting WHERE Setting_Name = 'STMAlertForSiteLicensing')
	INSERT INTO Setting(Setting_Name,Setting_Value) VALUES('STMAlertForSiteLicensing', 'True')
ELSE
	UPDATE Setting SET Setting_Value = 'True' WHERE Setting_Name = 'STMAlertForSiteLicensing'
GO

IF NOT EXISTS(SELECT Setting_Name FROM Setting WHERE Setting_Name = 'SiteLicensingSTMDurationInHours')
	INSERT INTO Setting(Setting_Name,Setting_Value) VALUES('SiteLicensingSTMDurationInHours', '1')
ELSE
	UPDATE Setting SET Setting_Value = '1' WHERE Setting_Name = 'SiteLicensingSTMDurationInHours'
GO

IF NOT EXISTS(SELECT Setting_Name FROM Setting WHERE Setting_Name = 'EnableCashdeskReconciliation')
	INSERT INTO Setting(Setting_Name,Setting_Value) VALUES('EnableCashdeskReconciliation', 'False')
ELSE
	UPDATE Setting SET Setting_Value = 'False' WHERE Setting_Name = 'EnableCashdeskReconciliation'
GO

IF NOT EXISTS(SELECT Setting_Name FROM Setting WHERE Setting_Name = 'EnableCashdeskMovement')
	INSERT INTO Setting(Setting_Name,Setting_Value) VALUES('EnableCashdeskMovement', 'False')
ELSE
	UPDATE Setting SET Setting_Value = 'False' WHERE Setting_Name = 'EnableCashdeskMovement'
GO

IF NOT EXISTS(SELECT Setting_Name FROM Setting WHERE Setting_Name = 'EnableSystemBalancing')
	INSERT INTO Setting(Setting_Name,Setting_Value) VALUES('EnableSystemBalancing', 'False')
ELSE
	UPDATE Setting SET Setting_Value = 'False' WHERE Setting_Name = 'EnableSystemBalancing'
GO
IF NOT EXISTS(SELECT Setting_Name FROM Setting WHERE Setting_Name = 'STMAlertLastUpdatedTime')
	INSERT INTO Setting(Setting_Name,Setting_Value) VALUES('STMAlertLastUpdatedTime', '0')
ELSE
	UPDATE Setting SET Setting_Value = '0' WHERE Setting_Name = 'STMAlertLastUpdatedTime'
GO
IF NOT EXISTS(SELECT Setting_Name FROM Setting WHERE Setting_Name = 'SiteLicensingSTMDurationInDay')
	INSERT INTO Setting(Setting_Name,Setting_Value) VALUES('SiteLicensingSTMDurationInDay', '1')
ELSE
	UPDATE Setting SET Setting_Value = '1' WHERE Setting_Name = 'SiteLicensingSTMDurationInDay'
GO

GO

IF NOT EXISTS(SELECT Setting_Name FROM Setting WHERE Setting_Name = 'SendDataToEBS')
	INSERT INTO Setting(Setting_Name,Setting_Value) VALUES('SendDataToEBS', 'False')
ELSE
	UPDATE Setting SET Setting_Value = 'False' WHERE Setting_Name = 'SendDataToEBS'
GO


GO

IF NOT EXISTS(SELECT Setting_Name FROM Setting WHERE Setting_Name = 'IsEBSEnabled')
	INSERT INTO Setting(Setting_Name,Setting_Value) VALUES('IsEBSEnabled', 'False')
ELSE
	UPDATE Setting SET Setting_Value = 'False' WHERE Setting_Name = 'IsEBSEnabled'
GO

GO

IF NOT EXISTS(SELECT Setting_Name FROM Setting WHERE Setting_Name = 'EBSEndPointURL')
	INSERT INTO Setting(Setting_Name,Setting_Value) VALUES('EBSEndPointURL', 'http://127.0.0.1:5018/S2SServer')
ELSE
	UPDATE Setting SET Setting_Value = 'http://127.0.0.1:5018/S2SServer' WHERE Setting_Name = 'EBSEndPointURL'
GO


IF NOT EXISTS(SELECT Setting_Name FROM Setting WHERE Setting_Name = 'EBSLastMessageId_Recv')
	INSERT INTO Setting(Setting_Name,Setting_Value) VALUES('EBSLastMessageId_Recv', '0')
ELSE
	UPDATE Setting SET Setting_Value = '0' WHERE Setting_Name = 'EBSLastMessageId_Recv'
GO
IF NOT EXISTS(SELECT Setting_Name FROM Setting WHERE Setting_Name = 'EBSLastMessageId_Send')
	INSERT INTO Setting(Setting_Name,Setting_Value) VALUES('EBSLastMessageId_Send', '0')
ELSE
	UPDATE Setting SET Setting_Value = '0' WHERE Setting_Name = 'EBSLastMessageId_Send'
GO

IF NOT EXISTS(SELECT Setting_Name FROM Setting WHERE Setting_Name = 'EBSVersion')
	INSERT INTO Setting(Setting_Name,Setting_Value) VALUES('EBSVersion', '13.0.1')
ELSE
	UPDATE Setting SET Setting_Value = '13.0.1' WHERE Setting_Name = 'EBSVersion'
GO

IF NOT EXISTS(SELECT Setting_Name FROM SETTING WHERE Setting_Name = 'ActiveMachinesBasedDOF')

	INSERT INTO SETTING(SETTING_NAME, SETTING_VALUE) VALUES ('ActiveMachinesBasedDOF', 'TRUE')
ELSE
	UPDATE SETTING SET SETTING_VALUE = 'TRUE' WHERE SETTING_NAME = 'ActiveMachinesBasedDOF'
GO
IF NOT EXISTS(SELECT Setting_Name FROM Setting WHERE Setting_Name = 'CustomMultiGameName')
	INSERT INTO Setting(Setting_Name,Setting_Value) VALUES('CustomMultiGameName', 'False')
ELSE
	UPDATE Setting SET Setting_Value = 'False' WHERE Setting_Name = 'CustomMultiGameName'
GO

IF NOT EXISTS(SELECT Setting_Name FROM Setting WHERE Setting_Name = 'OddRowColor')
	INSERT INTO Setting(Setting_Name,Setting_Value) VALUES('OddRowColor', '#bbbbff')
ELSE
	UPDATE Setting SET Setting_Value = '#bbbbff' WHERE Setting_Name = 'OddRowColor'
GO

IF NOT EXISTS(SELECT Setting_Name FROM Setting WHERE Setting_Name = 'EvenRowColor')
	INSERT INTO Setting(Setting_Name,Setting_Value) VALUES('EvenRowColor', 'Transperant')
ELSE
	UPDATE Setting SET Setting_Value = 'Transperant' WHERE Setting_Name = 'EvenRowColor'
GO

IF NOT EXISTS(SELECT Setting_Name FROM Setting WHERE Setting_Name = 'Include_AllTreasuryItems_In_Handpay')
	INSERT INTO Setting(Setting_Name,Setting_Value) VALUES('Include_AllTreasuryItems_In_Handpay', 'True')
ELSE
	UPDATE Setting SET Setting_Value = 'True' WHERE Setting_Name = 'Include_AllTreasuryItems_In_Handpay'
GO

IF NOT EXISTS(SELECT Setting_Name FROM Setting WHERE Setting_Name = 'ReportDateTimeFormat')
	INSERT INTO Setting(Setting_Name,Setting_Value) VALUES('ReportDateTimeFormat', 'dd-MMM-yyyy HH:mm:ss')
ELSE
	UPDATE Setting SET Setting_Value = 'dd-MMM-yyyy HH:mm:ss' WHERE Setting_Name = 'ReportDateTimeFormat'
GO

IF NOT EXISTS(SELECT Setting_Name FROM Setting WHERE Setting_Name = 'ReportDateFormat')
	INSERT INTO Setting(Setting_Name,Setting_Value) VALUES('ReportDateFormat', 'dd-MMM-yyyy')
ELSE
	UPDATE Setting SET Setting_Value = 'dd-MMM-yyyy' WHERE Setting_Name = 'ReportDateFormat'
GO

IF NOT EXISTS(SELECT Setting_Name FROM Setting WHERE Setting_Name = 'ReportPrintDateTimeFormat')
	INSERT INTO Setting(Setting_Name,Setting_Value) VALUES('ReportPrintDateTimeFormat', 'dd-MMM-yyyy HH:mm:ss')
ELSE
	UPDATE Setting SET Setting_Value = 'dd-MMM-yyyy HH:mm:ss' WHERE Setting_Name = 'ReportPrintDateTimeFormat'
GO

IF NOT EXISTS(SELECT Setting_Name FROM Setting WHERE Setting_Name = 'ReportDataDateAloneFormat')
	INSERT INTO Setting(Setting_Name,Setting_Value) VALUES('ReportDataDateAloneFormat', 'dd-MMM-yyyy')
ELSE
	UPDATE Setting SET Setting_Value = 'dd-MMM-yyyy' WHERE Setting_Name = 'ReportDataDateAloneFormat'
GO

IF NOT EXISTS(SELECT Setting_Name FROM Setting WHERE Setting_Name = 'ReportDataDateNTimeFormat')
	INSERT INTO Setting(Setting_Name,Setting_Value) VALUES('ReportDataDateNTimeFormat', 'dd-MMM-yyyy HH:mm:ss')
ELSE
	UPDATE Setting SET Setting_Value = 'dd-MMM-yyyy HH:mm:ss' WHERE Setting_Name = 'ReportDataDateNTimeFormat'
GO

IF NOT EXISTS(SELECT Setting_Name FROM Setting WHERE Setting_Name = 'IsServiceCallFeatureFull')
	INSERT INTO Setting(Setting_Name,Setting_Value) VALUES('IsServiceCallFeatureFull', 'False')
ELSE
	UPDATE Setting SET Setting_Value = 'False' WHERE Setting_Name = 'IsServiceCallFeatureFull'
GO

IF NOT EXISTS(SELECT Setting_Name FROM Setting WHERE Setting_Name = 'ShowCollectionReport')
	INSERT INTO Setting(Setting_Name,Setting_Value) VALUES('ShowCollectionReport', 'True')
ELSE
	UPDATE Setting SET Setting_Value = 'True' WHERE Setting_Name = 'ShowCollectionReport'
GO
