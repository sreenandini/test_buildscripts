USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[usp_EditCollection]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[usp_EditCollection]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-------------------------------------------------------------------------- 
---
--- Description: To update the Collection with .
---
--- Inputs:      see inputs
---
--- Outputs:     (0)   - no error .. 
---              OTHER - SQL error 
--- 
--- =======================================================================
--- 
--- Revision History
--- 
--- Taylor C	16/10/08    Created
--- Sudarsan S  25/10/08	add/diff with the existing value
--- C.Taylor    12/12/08    Added call to "esp_Calculate_Batch_Negative_Net"
--- Vineetha M  12/06/09    Included Cash collected for A/C Winloss Report(Total Coins column)
--- Anuradha J  26/06/09	Included Declared Tickets Value for CashCollected. 
--- Anuradha J  29/06/09	Changed the CashCollected Calculation 
--------------------------------------------------------------------------- 

CREATE PROCEDURE [dbo].[usp_EditCollection]
	@Collection_ID INT,
	 
	 --  @Coins         money,
	@Bill1 MONEY,
	@Bill5 MONEY,
	@Bill2 MONEY,
	@Bill10 MONEY,
	@Bill20 MONEY,
	@Bill50 MONEY,
	@Bill100 MONEY,
	@Ticket_In MONEY,
	@Ticket_Out MONEY,
	@Handpay MONEY,
	@Progressive MONEY
AS
	UPDATE COLLECTION
	SET    cash_collected_100p = cash_collected_100p + (@Bill1 * 100),
			cash_collected_200p = cash_collected_200p + (@Bill2 * 100),
	       cash_collected_500p = cash_collected_500p + (@Bill5 * 100),
	       cash_collected_1000p = cash_collected_1000p + (@Bill10 * 100),
	       cash_collected_2000p = cash_collected_2000p + (@Bill20 * 100),
	       cash_collected_5000p = cash_collected_5000p + (@Bill50 * 100),
	       cash_collected_10000p = cash_collected_10000p + (@Bill100 * 100),
	       declaredticketvalue = declaredticketvalue + @Ticket_In,
	       declaredticketprintedvalue = declaredticketprintedvalue + @Ticket_Out,
	       collection_treasury_handpay = collection_treasury_handpay + @Handpay,
	       Progressive_Value_Declared = Progressive_Value_Declared + @Progressive,
	       CashCollected = cashcollected + (
	           @Bill1 + @Bill5 + @Bill10 + @Bill20 + @Bill50 + @Bill100 + @Ticket_In
	       )
	WHERE  Collection_ID = @Collection_ID
	
	
	-- get batch_no for collection, and re-run the negative net recalculation
	DECLARE @Batch_No INT
	
	SELECT @Batch_No = Batch_ID
	FROM   COLLECTION
	WHERE  Collection_ID = @Collection_ID
	
	EXEC esp_Calculate_Batch_Negative_Net @Batch_No
GO 