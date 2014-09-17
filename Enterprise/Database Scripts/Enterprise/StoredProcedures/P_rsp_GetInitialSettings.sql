USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_GetInitialSettings]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_GetInitialSettings]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-------------------------------------------------------------------------------------------------------------------------
---    
--- Description: Fetches the Settings From Setting table
---        
--- Inputs:         
--- Outputs:     
--- ======================================================================================================================    
---     
--- Revision History    
---     
--- Dinesh R		26/04/2013		Created     
--------------------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].[rsp_GetInitialSettings]
AS
BEGIN
	SELECT ISNULL(BGSAdminWSUserID, '') AS BGSAdminWSUserID,
	       ISNULL(BGSAdminWSPwd, '') AS BGSAdminWSPwd,
	       CAST(ISNULL(SGVI_Enabled, 0) AS BIT) AS SGVI_Enabled,
	       CAST(ISNULL(SGVI_Payment_Days, 0) AS INT) AS SGVI_Payment_Days,
	       CAST(ISNULL(SGVI_Statement_Number, 0) AS INT) AS 
	       SGVI_Statement_Number,
	       ISNULL(ReportServerURL, '') AS ReportServerURL,
	       ISNULL(ReportFolder, '') AS ReportFolder,
	       ISNULL(EmptyReportMessage, '') AS EmptyReportMessage,
	       CAST(ISNULL(AUTHORIZATION_KEY_EXPIRY_HOURS, 0) AS INT) AS 
	       AUTHORIZATION_KEY_EXPIRY_HOURS,
	       ISNULL(SenderCode, '') AS SenderCode,
	       CAST(ISNULL(IsAuditingEnabled, 0) AS BIT) AS IsAuditingEnabled,
	       ISNULL(Client, '') AS Client,
	       ISNULL(BMC_Reports_Header, '') AS BMC_Reports_Header,
	       ISNULL(BMC_Reports_Language, '') AS BMC_Reports_Language,
	       CAST(ISNULL(MaxHandPayAuthRequired, 0) AS BIT) AS 
	       MaxHandPayAuthRequired,
	       CAST(ISNULL(ManualEntryTicketValidation, 0) AS BIT) AS 
	       ManualEntryTicketValidation,
	       CAST(ISNULL(SlotLifeToDate, 0) AS BIT) AS SlotLifeToDate,
	       CAST(ISNULL(AllowPartNumberEdit, 0) AS BIT) AS AllowPartNumberEdit,
	       CAST(ISNULL(RedeemTicketCustomer_Min, 0) AS INT) AS 
	       RedeemTicketCustomer_Min,
	       CAST(ISNULL(RedeemTicketCustomer_Max, 0) AS INT) AS 
	       RedeemTicketCustomer_Max,
	       ISNULL(RedeemTicketCustomer_BankAcctNo, '') AS 
	       RedeemTicketCustomer_BankAcctNo,
	       ISNULL(WindowsServices, '') AS WindowsServices,
	       CAST(ISNULL(IsAFTEnabledForSite, 0) AS BIT) AS IsAFTEnabledForSite,
	       CAST(ISNULL(IsPowerPromoReportsRequired, 0) AS BIT) AS 
	       IsPowerPromoReportsRequired,
	       CAST(ISNULL(MachineMaintenance, 0) AS BIT) AS MachineMaintenance,
	       ISNULL(CertificateIssuer, '') AS CertificateIssuer,
	       CAST(ISNULL(IsCertificateRequired, 0) AS BIT) AS 
	       IsCertificateRequired,
	       CAST(ISNULL(ComponentVerification, 0) AS BIT) AS 
	       ComponentVerification,
	       ISNULL(GuardianServerIPAddress, '') AS GuardianServerIPAddress,
	       CAST(ISNULL(IsMeterAdjustmentToolRequired, 0) AS BIT) AS 
	       IsMeterAdjustmentToolRequired,
	       CAST(ISNULL(LiveMeter, 0) AS BIT) AS LiveMeter,
	       ISNULL(ClearEventsOnFinalDrop, '') AS ClearEventsOnFinalDrop,
	       CAST(ISNULL(Auto_Declare_Monies, 0) AS BIT) AS Auto_Declare_Monies,
	       CAST(ISNULL(IsAFTIncludedInCalculation, 0) AS BIT) AS 
	       IsAFTIncludedInCalculation,
	       CAST(ISNULL(TreasuryLimitForMajorPrizes, 0) AS INT) AS 
	       TreasuryLimitForMajorPrizes,
	       CAST(ISNULL(SHOWHANDPAYCODE, 0) AS BIT) AS SHOWHANDPAYCODE,
	       CAST(ISNULL(CheckForGamePartNumber, 0) AS BIT) AS 
	       CheckForGamePartNumber,
	       CAST(ISNULL(CentralizedDeclaration, 0) AS BIT) AS 
	       CentralizedDeclaration,
	       CAST(ISNULL(IsTransmitterEnabled, 0) AS BIT) AS IsTransmitterEnabled,
	       ISNULL(STMServerIP, '') AS STMServerIP,
	       CAST(ISNULL(StackerLevelAlert, 0) AS BIT) AS StackerLevelAlert,
	       CAST(ISNULL(DropScheduleAlert, 0) AS BIT) AS DropScheduleAlert,
	       CAST(ISNULL(AllowOfflineDeclaration, 0) AS BIT) AS 
	       AllowOfflineDeclaration,
	       CAST(ISNULL(DeclarationAlert, 0) AS BIT) AS DeclarationAlert,
	       CAST(ISNULL(MinuteThreadCheckinHoursforAutoDrop, 0) AS INT) AS 
	       MinuteThreadCheckinHoursforAutoDrop,
	       CAST(ISNULL(RetryMinutesForCheckDB, 0) AS INT) AS 
	       RetryMinutesForCheckDB,
	       CAST(ISNULL(StackerFeature, 0) AS BIT) AS StackerFeature,
	       CAST(ISNULL(IsEmployeeCardTrackingEnabled, 0) AS BIT) AS 
	       IsEmployeeCardTrackingEnabled,
	       CAST(ISNULL(AddShortpayInVoucherOut, 0) AS BIT) AS 
	       AddShortpayInVoucherOut,
	       CAST(ISNULL(IsSiteLicensingEnabled, 0) AS BIT) AS 
	       IsSiteLicensingEnabled,
	       CAST(ISNULL(LiquidationProfitShare, 0) AS BIT) AS 
	       LiquidationProfitShare,
	       CAST(ISNULL(UseAssetTemplate, 0) AS BIT) AS UseAssetTemplate,
	       CAST(ISNULL(CentralizedReadLiquidation, 0) AS BIT) AS 
	       CentralizedReadLiquidation,
	       CAST(ISNULL(AGSSerialNumberAlphaNumeric, 0) AS BIT) AS 
	       AGSSerialNumberAlphaNumeric,
	       CAST(ISNULL(AllowDeMerge, 0) AS BIT) AS AllowDeMerge,
	       CAST(ISNULL(IsEnrolmentFlag, 0) AS BIT) AS IsEnrolmentFlag,
	       CAST(ISNULL(Login_Expiry_No_of_Days, 0) AS INT) AS 
	       Login_Expiry_No_of_Days,
	       CAST(ISNULL(Login_Max_No_Of_Attempts, 0) AS INT) AS 
	       Login_Max_No_Of_Attempts,
	       ISNULL(PRODUCTVERSION, '') AS PRODUCTVERSION,
	       CAST(ISNULL(STMServerPort, 0) AS INT) AS STMServerPort,
	       CAST(ISNULL(IsEnrolmentComplete, 0) AS BIT) AS IsEnrolmentComplete,
	       CAST(ISNULL(AGSValue, 0) AS INT) AS AGSValue,
	       CAST(ISNULL(IsSingleCardEmployee, 0) AS BIT) AS IsSingleCardEmployee,
	       CAST(ISNULL(MaxNoOfCardsForEmployee, 0) AS INT) AS 
	       MaxNoOfCardsForEmployee,
	       CAST(ISNULL(MaxNoOfVaultCassettes, 0) AS INT) AS 
	       MaxNoOfVaultCassettes,
	       CAST(ISNULL(MaxNoOfVaultHoppers, 0) AS INT) AS MaxNoOfVaultHoppers,
	       CAST(ISNULL(IsVaultEnabled, 0) AS BIT) AS IsVaultEnabled,
	       CAST(ISNULL(IsBillCounterAmountEditable, 0) AS BIT) AS 
	       IsBillCounterAmountEditable,
	       CAST(ISNULL(Vault_AutoPopulateDropValues, 0) AS BIT) AS 
	       Vault_AutoPopulateDropValues,
	       CAST(ISNULL(Vault_EndDeviceOnTerminate, 0) AS BIT) AS 
	       Vault_EndDeviceOnTerminate,
	       CAST(ISNULL(IsCrossTicketingEnabled, 0) AS BIT) AS 
	       IsCrossTicketingEnabled,
	       CAST(ISNULL(AllowEnableDisableBarPosition, 0) AS BIT) AS 
	       AllowEnableDisableBarPosition,
	       CAST(ISNULL(IsAlertEnabled, 0) AS BIT) AS IsAlertEnabled,
	       CAST(ISNULL(IsEmailAlertEnabled, 0) AS BIT) AS IsEmailAlertEnabled,
	       CAST(ISNULL(ReportDateTimeFormat, 0) AS VARCHAR(50)) AS 
	       ReportDateTimeFormat,
	       CAST(ISNULL(ReportDateFormat, 0) AS VARCHAR(50)) AS ReportDateFormat,
	       CAST(ISNULL(ReportPrintDateTimeFormat, 0) AS VARCHAR(50)) 
	       ReportPrintDateTimeFormat,
	       CAST(ISNULL(ReportDataDateAloneFormat, 0) AS VARCHAR(50)) AS 
	       ReportDataDateAloneFormat,
	       CAST(ISNULL(ReportDataDateNTimeFormat, 0) AS VARCHAR(50)) AS 
	       ReportDataDateNTimeFormat,
	       CAST(ISNULL(IsAutoCalendarEnabled, 0) AS BIT) AS 
	       IsAutoCalendarEnabled,
	       CAST(ISNULL(IsServiceCallFeatureFull, 0) AS BIT) AS 
	       IsServiceCallFeatureFull,
	       CAST(ISNULL(ShowCollectionReport, 0) AS BIT) AS ShowCollectionReport
	FROM   (
	           SELECT Setting_Name,
	                  Setting_Value
	           FROM   Setting
	       ) AS Source 
	       PIVOT(
	           MAX(Setting_Value)
	           FOR Setting_Name IN (BGSAdminWSUserID, BGSAdminWSPwd, 
	                               SGVI_Enabled, SGVI_Payment_Days, 
	                               SGVI_Statement_Number, ReportServerURL, 
	                               ReportFolder, EmptyReportMessage, 
	                               AUTHORIZATION_KEY_EXPIRY_HOURS, SenderCode, 
	                               IsAuditingEnabled, Client, BMC_Reports_Header, 
	                               BMC_Reports_Language, MaxHandPayAuthRequired, 
	                               ManualEntryTicketValidation, SlotLifeToDate, 
	                               AllowPartNumberEdit, RedeemTicketCustomer_Min, 
	                               RedeemTicketCustomer_Max, 
	                               RedeemTicketCustomer_BankAcctNo, 
	                               WindowsServices, IsAFTEnabledForSite, 
	                               IsPowerPromoReportsRequired, 
	                               MachineMaintenance, CertificateIssuer, 
	                               IsCertificateRequired, ComponentVerification, 
	                               GuardianServerIPAddress, 
	                               IsMeterAdjustmentToolRequired, LiveMeter, 
	                               ClearEventsOnFinalDrop, Auto_Declare_Monies, 
	                               IsAFTIncludedInCalculation, 
	                               TreasuryLimitForMajorPrizes, SHOWHANDPAYCODE, 
	                               CheckForGamePartNumber, 
	                               CentralizedDeclaration, IsTransmitterEnabled, 
	                               STMServerIP, StackerLevelAlert, 
	                               DropScheduleAlert, AllowOfflineDeclaration, 
	                               DeclarationAlert, 
	                               MinuteThreadCheckinHoursforAutoDrop, 
	                               RetryMinutesForCheckDB, StackerFeature, 
	                               IsEmployeeCardTrackingEnabled, 
	                               AddShortpayInVoucherOut, 
	                               IsSiteLicensingEnabled, 
	                               LiquidationProfitShare, UseAssetTemplate, 
	                               CentralizedReadLiquidation, 
	                               AGSSerialNumberAlphaNumeric, AllowDeMerge, 
	                               IsEnrolmentFlag, Login_Expiry_No_of_Days, 
	                               Login_Max_No_Of_Attempts, PRODUCTVERSION, 
	                               STMServerPort, IsEnrolmentComplete, AGSValue, 
	                               IsSingleCardEmployee, MaxNoOfCardsForEmployee, 
	                               MaxNoOfVaultCassettes, MaxNoOfVaultHoppers, 
	                               IsVaultEnabled, IsBillCounterAmountEditable, 
	                               Vault_AutoPopulateDropValues, 
	                               Vault_EndDeviceOnTerminate, 
	                               IsCrossTicketingEnabled, 
	                               AllowEnableDisableBarPosition, IsAlertEnabled, 
	                               IsEmailAlertEnabled, ReportDateTimeFormat, 
	                               ReportDateFormat, ReportPrintDateTimeFormat, 
	                               ReportDataDateAloneFormat, 
	                               ReportDataDateNTimeFormat, 
	                               IsAutoCalendarEnabled, 
	                               IsServiceCallFeatureFull, 
	                               ShowCollectionReport)
	       ) AS Pvt
END
GO

