/****** Object:  StoredProcedure [dbo].[rsp_GetInitialSettings]    Script Date: 11/10/2008 16:48:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetInitialSettings]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetInitialSettings]
GO
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
------------------------------------------------------------------------------------------------------------------------    
---    
--- Description: Fetches the details from Setting table
---        
--- Inputs:         
--- Outputs:     
--- ======================================================================================================================    
---     
--- Revision History    
---     
--- Sudarsan S     10/11/08     Created     
--- Vineetha Mathew 08/Oct/09     Modified	Added 3 more fields to get hourly & daily read settings     
------------------------------------------------------------------------------------------------------------------------    
CREATE  PROCEDURE [dbo].[rsp_GetInitialSettings]
AS
BEGIN

SELECT 
		CAST(TicketValidate_EnableVoucher AS BIT) AS TicketValidate_EnableVoucher,
		TICKETVALIDATE_REQUIRES_HEADCASHIER_SIG,
		TICKETVALIDATE_REQUIRES_MANAGER_SIG,
		CAST(TicketValidate_EnableHandpayReceipt AS BIT) AS TicketValidate_EnableHandpayReceipt,
		CAST(TicketValidate_EnableIssueReceipt AS BIT) AS TicketValidate_EnableIssueReceipt,
		TicketDeclarationMethod,
		CAST(TicketValidate_EnableShortpayReceipt AS BIT) AS TicketValidate_EnableShortpayReceipt,
		CAST(TicketValidate_EnableRefillReceipt AS BIT) AS TicketValidate_EnableRefillReceipt,
		CAST(TicketValidate_EnableRefundReceipt AS BIT) AS TicketValidate_EnableRefundReceipt,
		CAST(TicketValidate_EnableProgressivePayoutReceipt AS BIT) AS TicketValidate_EnableProgressivePayoutReceipt,
		CAST(CD_NOT_ISSUE_TICKET AS BIT) AS CD_NOT_ISSUE_TICKET,
		CAST(CD_TITO_NOT_IN_USE AS BIT) AS CD_TITO_NOT_IN_USE,
		CAST(USE_ON_SCREEN_KEYBOARD AS BIT) AS USE_ON_SCREEN_KEYBOARD,
		CAST(TicketValidation_FillScreen AS BIT) AS TicketValidation_FillScreen,
		CAST(VoidTransactions AS BIT) AS VoidTransactions,
		CAST(HandpayManualEntry AS BIT) AS HandpayManualEntry,
		CAST(EnableLaundering AS BIT) AS EnableLaundering,
		Voucher_Site_Name,
		REGION,
		CAST(CD_NOT_USE_HOPPERS AS BIT) AS CD_NOT_USE_HOPPERS,
		[Ticketing.Connection] AS Connection,
		CAST(Allow_Offline_Redeem AS BIT) AS Allow_Offline_Redeem, 
		Ticket_Location_Code,
		CAST(ReceiptPrinter AS BIT) AS ReceiptPrinter,
		CAST(SGVI_ENABLED AS BIT) AS SGVI_ENABLED,
		CAST(ISNULL(GAMING_DAY_START_HOUR, '1') AS INT) AS GAMING_DAY_START_HOUR,
		CAST(HourlyTry AS INT) AS HourlyTry,
		CAST(DailyTry AS INT) AS DailyTry,
		DailyAutoReadTime,
		Interval_HourlyDaily_Service as HourlyDailyServiceInterval
FROM
	(
	SELECT Setting_Name, Setting_Value
	FROM Setting) AS Source
	PIVOT
		(
		MAX(Setting_Value)
			FOR Setting_Name IN (TicketValidate_EnableVoucher, TICKETVALIDATE_REQUIRES_HEADCASHIER_SIG, 
					TICKETVALIDATE_REQUIRES_MANAGER_SIG, TicketValidate_EnableHandpayReceipt, 
					TicketValidate_EnableIssueReceipt, TicketDeclarationMethod, TicketValidate_EnableShortpayReceipt, 
					TicketValidate_EnableRefillReceipt, TicketValidate_EnableRefundReceipt, 
					TicketValidate_EnableProgressivePayoutReceipt, CD_NOT_ISSUE_TICKET, CD_TITO_NOT_IN_USE, 
					USE_ON_SCREEN_KEYBOARD, TicketValidation_FillScreen, VoidTransactions, HandpayManualEntry, 
					EnableLaundering,Voucher_Site_Name,REGION,CD_NOT_USE_HOPPERS, [Ticketing.Connection], 
					Allow_Offline_Redeem, Ticket_Location_Code, ReceiptPrinter, SGVI_ENABLED, GAMING_DAY_START_HOUR,HourlyTry,DailyTry,DailyAutoReadTime,
					Interval_HourlyDaily_Service)
		) AS Pvt


END

