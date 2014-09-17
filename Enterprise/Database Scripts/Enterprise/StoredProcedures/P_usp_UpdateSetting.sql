USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[usp_UpdateSetting]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[usp_UpdateSetting]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--
-- Description: Update setting table
--
-- Inputs:      See inputs
--
-- Outputs:     
--
-- =======================================================================
-- 
-- Revision History
-- 
-- Anil   16/12/10   Modified   Added update statment for setting table. 
-- 
--------------------------------------------------------------------------- 

CREATE PROCEDURE [dbo].[usp_UpdateSetting]
	@SettingsProfile_Description VARCHAR(200),
	@SettingName VARCHAR(200),
	@SettingValue VARCHAR(500)
AS
BEGIN

	UPDATE SettingsProfileItems
	SET    SettingsProfileItems_SettingsMaster_Values = @SettingValue
	WHERE  SettingsProfileItems_SettingsProfile_ID = (
	           SELECT SettingsProfile_ID
	           FROM   SettingsProfile
	           WHERE  SettingsProfile_Description = @SettingsProfile_Description
	       )
	       AND SettingsProfileItems_SettingsMaster_ID = (
	               SELECT SettingsMaster_ID
	               FROM   SettingsMaster
	               WHERE  SettingsMaster_Name = CASE 
	                                                 WHEN (UPPER(@SettingName) = 'LIQUIDATION_ENABLED') THEN 
	                                                      'SGVI_ENABLED'
	                                                 WHEN (UPPER(@SettingName) = 'VOUCHER_CREATE_TIMEOUT') THEN 
	                                                      'TICKET_CREATE_TIMEOUT'
	                                                 WHEN (UPPER(@SettingName) = 'VOUCHERVALIDATE_ENABLEVOUCHER') THEN 
	                                                      'TICKETVALIDATE_ENABLEVOUCHER'
	                                                 WHEN (
	                                                          UPPER(@SettingName) 
	                                                          = 
	                                                          'VOUCHERVALIDATE_ENABLEREFUNDRECEIPT'
	                                                      ) THEN 
	                                                      'TICKETVALIDATE_ENABLEREFUNDRECEIPT'
	                                                 WHEN (
	                                                          UPPER(@SettingName) 
	                                                          = 
	                                                          'VOUCHERVALIDATE_REQUIRES_HEADCASHIER_SIG'
	                                                      ) THEN 
	                                                      'TICKETVALIDATE_REQUIRES_HEADCASHIER_SIG'
	                                                 WHEN (
	                                                          UPPER(@SettingName) 
	                                                          = 
	                                                          'VOUCHERVALIDATE_REQUIRES_MANAGER_SIG'
	                                                      ) THEN 
	                                                      'TICKETVALIDATE_REQUIRES_MANAGER_SIG'
	                                                 WHEN (
	                                                          UPPER(@SettingName) 
	                                                          = 
	                                                          'VOUCHERVALIDATE_ENABLEHANDPAYRECEIPT'
	                                                      ) THEN 
	                                                      'TICKETVALIDATE_ENABLEHANDPAYRECEIPT'
	                                                 WHEN (UPPER(@SettingName) = 'VOUCHERVALIDATE_ENABLEISSUERECEIPT') THEN 
	                                                      'TICKETVALIDATE_ENABLEISSUERECEIPT'
	                                                 WHEN (
	                                                          UPPER(@SettingName) 
	                                                          = 
	                                                          'VOUCHERVALIDATE_ENABLESHORTPAYRECEIPT'
	                                                      ) THEN 
	                                                      'TICKETVALIDATE_ENABLESHORTPAYRECEIPT'
	                                                 WHEN (UPPER(@SettingName) = 'VOUCHER_DB_NAME') THEN 
	                                                      'TICKET_DB_NAME'
	                                                 WHEN (UPPER(@SettingName) = 'SAS_UPDATEVOUCHERDETAILS') THEN 
	                                                      'SAS_UPDATETICKETDETAILS'
	                                                 WHEN (UPPER(@SettingName) = 'PROMO_VOUCHER_CODE') THEN 
	                                                      'PROMO_TICKET_CODE'
	                                                 WHEN (UPPER(@SettingName) = 'VOUCHERVALIDATION_FILLSCREEN') THEN 
	                                                      'TICKETVALIDATION_FILLSCREEN'
	                                                 WHEN (UPPER(@SettingName) = 'REDEEMVOUCHERCUSTOMER_MIN') THEN 
	                                                      'REDEEMTICKETCUSTOMER_MIN'
	                                                 WHEN (UPPER(@SettingName) = 'REDEEMVOUCHERCUSTOMER_MAX') THEN 
	                                                      'REDEEMTICKETCUSTOMER_MAX'
	                                                 WHEN (UPPER(@SettingName) = 'REDEEMVOUCHERCUSTOMER_BANKACCTNO') THEN 
	                                                      'REDEEMTICKETCUSTOMER_BANKACCTNO'
	                                                 WHEN (UPPER(@SettingName) = 'ISSUEVOUCHERMAXVALUE') THEN 
	                                                      'ISSUETICKETMAXVALUE'
	                                                 WHEN (UPPER(@SettingName) = 'ISSUE_VOUCHER_ENCRYPT_BARCODE') THEN 
	                                                      'ISSUE_TICKET_ENCRYPT_BARCODE'
	                                                 WHEN (UPPER(@SettingName) = 'MANUALENTRYVOUCHERVALIDATION') THEN 
	                                                      'MANUALENTRYTICKETVALIDATION'
	                                                 WHEN (UPPER(@SettingName) = 'REDEEMEXPIREDVOUCHER') THEN 
	                                                      'REDEEMEXPIREDTICKET'
	                                                 WHEN (UPPER(@SettingName) = 'VOUCHERDECLARATIONMETHOD') THEN 
	                                                      'TICKETDECLARATIONMETHOD'
	                                                 WHEN (
	                                                          UPPER(@SettingName) 
	                                                          = 
	                                                          'VOUCHERVALIDATE_ENABLEPROGRESSIVEPAYOUTRECEIPT'
	                                                      ) THEN 
	                                                      'TICKETVALIDATE_ENABLEPROGRESSIVEPAYOUTRECEIPT'
	                                                 WHEN (UPPER(@SettingName) = 'VOUCHER_EXPIRE') THEN 
	                                                      'TICKET_EXPIRE'
	                                                 WHEN (UPPER(@SettingName) = 'HANDLE_EXCEPTIONVOUCHERS') THEN 
	                                                      'HANDLE_EXCEPTIONTICKETS'
	                                                 WHEN (UPPER(@SettingName) = 'HANDLE_EXCEPTIONVOUCHERS_COUNTER') THEN 
	                                                      'HANDLE_EXCEPTIONTICKETS_COUNTER'
	                                                 WHEN (
	                                                          UPPER(@SettingName) 
	                                                          = 
	                                                          'VOUCHERVALIDATE_ENABLECUSTOMERRECEIPT'
	                                                      ) THEN 
	                                                      'TICKETVALIDATE_ENABLECUSTOMERRECEIPT'
	                                                 WHEN (UPPER(@SettingName) = 'DEC_AUTOMARKACTIVEVOUCHERSASPD') THEN 
	                                                      'DEC_AUTOMARKACTIVETICKETSASPD'
	                                                 WHEN (UPPER(@SettingName) = 'DEC_AUTOMARKEXCEPTIONVOUCHERASPD') THEN 
	                                                      'DEC_AUTOMARKEXCEPTIONTICKETASPD'
	                                                 WHEN (UPPER(@SettingName) = 'VOIDEXPIREDVOUCHER') THEN 
	                                                      'VOIDEXPIREDTICKET'
	                                                 WHEN (UPPER(@SettingName) = 'ISPROMOTIONALVOUCHERENABLED') THEN 
	                                                      'ISPROMOTIONALTICKETENABLED'
	                                                 WHEN (UPPER(@SettingName) = 'MAXIMUMPROMOTIONALVOUCHERSCOUNT') THEN 
	                                                      'MAXIMUMPROMOTIONALTICKETSCOUNT'
	                                                 WHEN (UPPER(@SettingName) = 'MAXIMUMPROMOTIONALVOUCHERAMOUNT') THEN 
	                                                      'MAXIMUMPROMOTIONALTICKETAMOUNT'
	                                                 WHEN (
	                                                          UPPER(@SettingName) 
	                                                          = 
	                                                          'DEFAULTPROMOTIONALVOUCHEREXPIREDAYS'
	                                                      ) THEN 
	                                                      'DEFAULTPROMOTIONALTICKETEXPIREDAYS'
	                                                 WHEN (UPPER(@SettingName) = 'SAS_-- UPDATEVOUCHERDETAILS') THEN 
	                                                      'SAS_-- UPDATETICKETDETAILS'
	                                                 WHEN (UPPER(@SettingName) = 'AUTOCALCDECVOUCHERSONEXPORT') THEN 
	                                                      'AUTOCALCDECTICKETSONEXPORT'
	                                                 WHEN (UPPER(@SettingName) = 'VOUCHERANOMALIESENABLED') THEN 
	                                                      'TICKETANOMALIESENABLED'
	                                                 ELSE @SettingName
	                                            END
	           )
	
	UPDATE Setting
	SET    Setting_Value = @SettingValue
	WHERE  Setting_Name = CASE 
	                           WHEN (UPPER(@SettingName) = 'LIQUIDATION_ENABLED') THEN 
	                                'SGVI_ENABLED'
	                           WHEN (UPPER(@SettingName) = 'VOUCHER_CREATE_TIMEOUT') THEN 
	                                'TICKET_CREATE_TIMEOUT'
	                           WHEN (UPPER(@SettingName) = 'VOUCHERVALIDATE_ENABLEVOUCHER') THEN 
	                                'TICKETVALIDATE_ENABLEVOUCHER'
	                           WHEN (
	                                    UPPER(@SettingName) = 
	                                    'VOUCHERVALIDATE_ENABLEREFUNDRECEIPT'
	                                ) THEN 'TICKETVALIDATE_ENABLEREFUNDRECEIPT'
	                           WHEN (
	                                    UPPER(@SettingName) = 
	                                    'VOUCHERVALIDATE_REQUIRES_HEADCASHIER_SIG'
	                                ) THEN 
	                                'TICKETVALIDATE_REQUIRES_HEADCASHIER_SIG'
	                           WHEN (
	                                    UPPER(@SettingName) = 
	                                    'VOUCHERVALIDATE_REQUIRES_MANAGER_SIG'
	                                ) THEN 'TICKETVALIDATE_REQUIRES_MANAGER_SIG'
	                           WHEN (
	                                    UPPER(@SettingName) = 
	                                    'VOUCHERVALIDATE_ENABLEHANDPAYRECEIPT'
	                                ) THEN 'TICKETVALIDATE_ENABLEHANDPAYRECEIPT'
	                           WHEN (UPPER(@SettingName) = 'VOUCHERVALIDATE_ENABLEISSUERECEIPT') THEN 
	                                'TICKETVALIDATE_ENABLEISSUERECEIPT'
	                           WHEN (
	                                    UPPER(@SettingName) = 
	                                    'VOUCHERVALIDATE_ENABLESHORTPAYRECEIPT'
	                                ) THEN 
	                                'TICKETVALIDATE_ENABLESHORTPAYRECEIPT'
	                           WHEN (UPPER(@SettingName) = 'VOUCHER_DB_NAME') THEN 
	                                'TICKET_DB_NAME'
	                           WHEN (UPPER(@SettingName) = 'SAS_UPDATEVOUCHERDETAILS') THEN 
	                                'SAS_UPDATETICKETDETAILS'
	                           WHEN (UPPER(@SettingName) = 'PROMO_VOUCHER_CODE') THEN 
	                                'PROMO_TICKET_CODE'
	                           WHEN (UPPER(@SettingName) = 'VOUCHERVALIDATION_FILLSCREEN') THEN 
	                                'TICKETVALIDATION_FILLSCREEN'
	                           WHEN (UPPER(@SettingName) = 'REDEEMVOUCHERCUSTOMER_MIN') THEN 
	                                'REDEEMTICKETCUSTOMER_MIN'
	                           WHEN (UPPER(@SettingName) = 'REDEEMVOUCHERCUSTOMER_MAX') THEN 
	                                'REDEEMTICKETCUSTOMER_MAX'
	                           WHEN (UPPER(@SettingName) = 'REDEEMVOUCHERCUSTOMER_BANKACCTNO') THEN 
	                                'REDEEMTICKETCUSTOMER_BANKACCTNO'
	                           WHEN (UPPER(@SettingName) = 'ISSUEVOUCHERMAXVALUE') THEN 
	                                'ISSUETICKETMAXVALUE'
	                           WHEN (UPPER(@SettingName) = 'ISSUE_VOUCHER_ENCRYPT_BARCODE') THEN 
	                                'ISSUE_TICKET_ENCRYPT_BARCODE'
	                           WHEN (UPPER(@SettingName) = 'MANUALENTRYVOUCHERVALIDATION') THEN 
	                                'MANUALENTRYTICKETVALIDATION'
	                           WHEN (UPPER(@SettingName) = 'REDEEMEXPIREDVOUCHER') THEN 
	                                'REDEEMEXPIREDTICKET'
	                           WHEN (UPPER(@SettingName) = 'VOUCHERDECLARATIONMETHOD') THEN 
	                                'TICKETDECLARATIONMETHOD'
	                           WHEN (
	                                    UPPER(@SettingName) = 
	                                    'VOUCHERVALIDATE_ENABLEPROGRESSIVEPAYOUTRECEIPT'
	                                ) THEN 
	                                'TICKETVALIDATE_ENABLEPROGRESSIVEPAYOUTRECEIPT'
	                           WHEN (UPPER(@SettingName) = 'VOUCHER_EXPIRE') THEN 
	                                'TICKET_EXPIRE'
	                           WHEN (UPPER(@SettingName) = 'HANDLE_EXCEPTIONVOUCHERS') THEN 
	                                'HANDLE_EXCEPTIONTICKETS'
	                           WHEN (UPPER(@SettingName) = 'HANDLE_EXCEPTIONVOUCHERS_COUNTER') THEN 
	                                'HANDLE_EXCEPTIONTICKETS_COUNTER'
	                           WHEN (
	                                    UPPER(@SettingName) = 
	                                    'VOUCHERVALIDATE_ENABLECUSTOMERRECEIPT'
	                                ) THEN 
	                                'TICKETVALIDATE_ENABLECUSTOMERRECEIPT'
	                           WHEN (UPPER(@SettingName) = 'DEC_AUTOMARKACTIVEVOUCHERSASPD') THEN 
	                                'DEC_AUTOMARKACTIVETICKETSASPD'
	                           WHEN (UPPER(@SettingName) = 'DEC_AUTOMARKEXCEPTIONVOUCHERASPD') THEN 
	                                'DEC_AUTOMARKEXCEPTIONTICKETASPD'
	                           WHEN (UPPER(@SettingName) = 'VOIDEXPIREDVOUCHER') THEN 
	                                'VOIDEXPIREDTICKET'
	                           WHEN (UPPER(@SettingName) = 'ISPROMOTIONALVOUCHERENABLED') THEN 
	                                'ISPROMOTIONALTICKETENABLED'
	                           WHEN (UPPER(@SettingName) = 'MAXIMUMPROMOTIONALVOUCHERSCOUNT') THEN 
	                                'MAXIMUMPROMOTIONALTICKETSCOUNT'
	                           WHEN (UPPER(@SettingName) = 'MAXIMUMPROMOTIONALVOUCHERAMOUNT') THEN 
	                                'MAXIMUMPROMOTIONALTICKETAMOUNT'
	                           WHEN (
	                                    UPPER(@SettingName) = 
	                                    'DEFAULTPROMOTIONALVOUCHEREXPIREDAYS'
	                                ) THEN 'DEFAULTPROMOTIONALTICKETEXPIREDAYS'
	                           WHEN (UPPER(@SettingName) = 'SAS_-- UPDATEVOUCHERDETAILS') THEN 
	                                'SAS_-- UPDATETICKETDETAILS'
	                           WHEN (UPPER(@SettingName) = 'AUTOCALCDECVOUCHERSONEXPORT') THEN 
	                                'AUTOCALCDECTICKETSONEXPORT'
	                           WHEN (UPPER(@SettingName) = 'VOUCHERANOMALIESENABLED') THEN 
	                                'TICKETANOMALIESENABLED' 
	                                --ELSE  @SettingName
	                      END
END

GO

