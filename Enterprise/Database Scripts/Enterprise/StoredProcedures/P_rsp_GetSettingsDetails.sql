USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_GetSettingsDetails]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_GetSettingsDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE rsp_GetSettingsDetails
	@SettingsProfile_Description VARCHAR(200),
	@GetAllSettings BIT,
	@GETAFTSettings BIT = 0,
	@SiteID INT = 0
AS
	SET NOCOUNT ON
	DECLARE @AFTEnabled BIT        
	EXEC rsp_GetSetting NULL,
	     'IsAFTEnabledForSite',
	     'false',
	     @AFTEnabled OUTPUT           
	
	
	
	IF (@GETAFTSettings = 0)
	BEGIN
	    SELECT [ID] = SM.SettingsMaster_ID,
	           [Name] = CASE 
	                         WHEN (UPPER(SM.SettingsMaster_Name) = 'SGVI_ENABLED') THEN 
	                              'LIQUIDATION_ENABLED'
	                         WHEN (UPPER(SM.SettingsMaster_Name) = 'TICKET_CREATE_TIMEOUT') THEN 
	                              'VOUCHER_CREATE_TIMEOUT'
	                         WHEN (
	                                  UPPER(SM.SettingsMaster_Name) =
	                                  'TICKETVALIDATE_ENABLEVOUCHER'
	                              ) THEN 'VOUCHERVALIDATE_ENABLEVOUCHER'
	                         WHEN (
	                                  UPPER(SM.SettingsMaster_Name) =
	                                  'TICKETVALIDATE_ENABLEREFUNDRECEIPT'
	                              ) THEN 'VOUCHERVALIDATE_ENABLEREFUNDRECEIPT'
	                         WHEN (
	                                  UPPER(SM.SettingsMaster_Name) =
	                                  'TICKETVALIDATE_REQUIRES_HEADCASHIER_SIG'
	                              ) THEN 
	                              'VOUCHERVALIDATE_REQUIRES_HEADCASHIER_SIG'
	                         WHEN (
	                                  UPPER(SM.SettingsMaster_Name) =
	                                  'TICKETVALIDATE_REQUIRES_MANAGER_SIG'
	                              ) THEN 'VOUCHERVALIDATE_REQUIRES_MANAGER_SIG'
	                         WHEN (
	                                  UPPER(SM.SettingsMaster_Name) =
	                                  'TICKETVALIDATE_ENABLEHANDPAYRECEIPT'
	                              ) THEN 'VOUCHERVALIDATE_ENABLEHANDPAYRECEIPT'
	                         WHEN (
	                                  UPPER(SM.SettingsMaster_Name) =
	                                  'TICKETVALIDATE_ENABLEISSUERECEIPT'
	                              ) THEN 'VOUCHERVALIDATE_ENABLEISSUERECEIPT'
	                         WHEN (
	                                  UPPER(SM.SettingsMaster_Name) =
	                                  'TICKETVALIDATE_ENABLESHORTPAYRECEIPT'
	                              ) THEN 'VOUCHERVALIDATE_ENABLESHORTPAYRECEIPT'
	                         WHEN (UPPER(SM.SettingsMaster_Name) = 'TICKET_DB_NAME') THEN 
	                              'VOUCHER_DB_NAME'
	                         WHEN (UPPER(SM.SettingsMaster_Name) = 'SAS_UPDATETICKETDETAILS') THEN 
	                              'SAS_UPDATEVOUCHERDETAILS'
	                         WHEN (UPPER(SM.SettingsMaster_Name) = 'PROMO_TICKET_CODE') THEN 
	                              'PROMO_VOUCHER_CODE'
	                         WHEN (
	                                  UPPER(SM.SettingsMaster_Name) =
	                                  'TICKETVALIDATION_FILLSCREEN'
	                              ) THEN 'VOUCHERVALIDATION_FILLSCREEN'
	                         WHEN (UPPER(SM.SettingsMaster_Name) = 'REDEEMTICKETCUSTOMER_MIN') THEN 
	                              'REDEEMVOUCHERCUSTOMER_MIN'
	                         WHEN (UPPER(SM.SettingsMaster_Name) = 'REDEEMTICKETCUSTOMER_MAX') THEN 
	                              'REDEEMVOUCHERCUSTOMER_MAX'
	                         WHEN (
	                                  UPPER(SM.SettingsMaster_Name) =
	                                  'REDEEMTICKETCUSTOMER_BANKACCTNO'
	                              ) THEN 'REDEEMVOUCHERCUSTOMER_BANKACCTNO'
	                         WHEN (UPPER(SM.SettingsMaster_Name) = 'ISSUETICKETMAXVALUE') THEN 
	                              'ISSUEVOUCHERMAXVALUE'
	                         WHEN (
	                                  UPPER(SM.SettingsMaster_Name) =
	                                  'ISSUE_TICKET_ENCRYPT_BARCODE'
	                              ) THEN 'ISSUE_VOUCHER_ENCRYPT_BARCODE'
	                         WHEN (
	                                  UPPER(SM.SettingsMaster_Name) =
	                                  'MANUALENTRYTICKETVALIDATION'
	                              ) THEN 'MANUALENTRYVOUCHERVALIDATION'
	                         WHEN (UPPER(SM.SettingsMaster_Name) = 'REDEEMEXPIREDTICKET') THEN 
	                              'REDEEMEXPIREDVOUCHER'
	                         WHEN (UPPER(SM.SettingsMaster_Name) = 'TICKETDECLARATIONMETHOD') THEN 
	                              'VOUCHERDECLARATIONMETHOD'
	                         WHEN (
	                                  UPPER(SM.SettingsMaster_Name) =
	                                  'TICKETVALIDATE_ENABLEPROGRESSIVEPAYOUTRECEIPT'
	                              ) THEN 
	                              'VOUCHERVALIDATE_ENABLEPROGRESSIVEPAYOUTRECEIPT'
	                         WHEN (UPPER(SM.SettingsMaster_Name) = 'TICKET_EXPIRE') THEN 
	                              'VOUCHER_EXPIRE'
	                         WHEN (UPPER(SM.SettingsMaster_Name) = 'HANDLE_EXCEPTIONTICKETS') THEN 
	                              'HANDLE_EXCEPTIONVOUCHERS'
	                         WHEN (
	                                  UPPER(SM.SettingsMaster_Name) =
	                                  'HANDLE_EXCEPTIONTICKETS_COUNTER'
	                              ) THEN 'HANDLE_EXCEPTIONVOUCHERS_COUNTER'
	                         WHEN (
	                                  UPPER(SM.SettingsMaster_Name) =
	                                  'TICKETVALIDATE_ENABLECUSTOMERRECEIPT'
	                              ) THEN 'VOUCHERVALIDATE_ENABLECUSTOMERRECEIPT'
	                         WHEN (
	                                  UPPER(SM.SettingsMaster_Name) =
	                                  'DEC_AUTOMARKACTIVETICKETSASPD'
	                              ) THEN 'DEC_AUTOMARKACTIVEVOUCHERSASPD'
	                         WHEN (
	                                  UPPER(SM.SettingsMaster_Name) =
	                                  'DEC_AUTOMARKEXCEPTIONTICKETASPD'
	                              ) THEN 'DEC_AUTOMARKEXCEPTIONVOUCHERASPD'
	                         WHEN (UPPER(SM.SettingsMaster_Name) = 'VOIDEXPIREDTICKET') THEN 
	                              'VOIDEXPIREDVOUCHER'
	                         WHEN (
	                                  UPPER(SM.SettingsMaster_Name) =
	                                  'ISPROMOTIONALTICKETENABLED'
	                              ) THEN 'ISPROMOTIONALVOUCHERENABLED'
	                         WHEN (
	                                  UPPER(SM.SettingsMaster_Name) =
	                                  'MAXIMUMPROMOTIONALTICKETSCOUNT'
	                              ) THEN 'MAXIMUMPROMOTIONALVOUCHERSCOUNT'
	                         WHEN (
	                                  UPPER(SM.SettingsMaster_Name) =
	                                  'MAXIMUMPROMOTIONALTICKETAMOUNT'
	                              ) THEN 'MAXIMUMPROMOTIONALVOUCHERAMOUNT'
	                         WHEN (
	                                  UPPER(SM.SettingsMaster_Name) =
	                                  'DEFAULTPROMOTIONALTICKETEXPIREDAYS'
	                              ) THEN 'DEFAULTPROMOTIONALVOUCHEREXPIREDAYS'
	                         WHEN (
	                                  UPPER(SM.SettingsMaster_Name) =
	                                  'SAS_-- UPDATETICKETDETAILS'
	                              ) THEN 'SAS_-- UPDATEVOUCHERDETAILS'
	                         WHEN (
	                                  UPPER(SM.SettingsMaster_Name) =
	                                  'AUTOCALCDECTICKETSONEXPORT'
	                              ) THEN 'AUTOCALCDECVOUCHERSONEXPORT'
	                         WHEN (UPPER(SM.SettingsMaster_Name) = 'TICKETANOMALIESENABLED') THEN 
	                              'VOUCHERANOMALIESENABLED'
	                         ELSE UPPER(SM.SettingsMaster_Name)
	                    END,
	           [Type] = SM.SettingsMaster_Type,
	           [Value] = ISNULL(SettingsProfileItems_SettingsMaster_Values, ''),
	           [Description] = ISNULL(SM.SettingsMaster_Description, '')
	    FROM   SettingsMaster SM
	           INNER JOIN SettingsProfileItems SPI
	                ON  SM.SettingsMaster_ID = SPI.SettingsProfileItems_SettingsMaster_ID
	           INNER JOIN SettingsProfile SP
	                ON  SP.SettingsProfile_ID = SPI.SettingsProfileItems_SettingsProfile_ID
	    WHERE  SP.SettingsProfile_Description = @SettingsProfile_Description
	           AND (@GetAllSettings = 1 OR SM.SettingsMaster_IsEnabled = 'Y')
	END
	ELSE
	BEGIN
	    SELECT * INTO #temptable
	    FROM   aftsettings AFS
	           INNER JOIN SITE S
	                ON  AFS.SiteID = S.Site_ID
	                AND ISNULL(S.AFT_Settings_Enabled, 0) = 1
	    WHERE  SiteID = @SiteID   
	    
	    SELECT ROW_NUMBER() OVER(ORDER BY NAME) AS Setting_ID,
	           NAME,
	           VALUE
	    FROM   dbo.#temptable 
	           UNPIVOT(
	               VALUE FOR NAME IN ([AFTTransactionsAllowed], 
	                                 [AllowNonCashableDeposits], 
	                                 [AllowCashableDeposits], 
	                                 [AllowCashWithdrawal], [AllowOffers], 
	                                 [AllowPartialTransfers], 
	                                 [AllowPointsWithdrawal], 
	                                 [AllowRedeemOffers], 
	                                 [AutoDepositCashableCreditsonCardOut], 
	                                 [AutoDepositNonCashableCreditsonCardOut], 
	                                 [EFTTimeoutValue], [MaxDepositAmount], 
	                                 [MaxWithDrawAmount], 
	                                 [Option1WithdrawalAmount], 
	                                 [Option2WithdrawalAmount], 
	                                 [Option3WithdrawalAmount], 
	                                 [Option4WithdrawalAmount], 
	                                 [Option5WithdrawalAmount], [SiteID])
	           ) AS Un 
	    
	    DROP TABLE #temptable
	END
GO

