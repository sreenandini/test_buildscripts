USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_CashTake]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_CashTake]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE rsp_CashTake(@CollectionId INT)
AS
BEGIN
	DECLARE @Void            FLOAT
	DECLARE @TreasuryAmount  FLOAT
	SELECT @Void = SUM(Treasury_Amount)
	FROM   Treasury_Entry(NOLOCK)
	WHERE  Collection_ID = @CollectionId
	       AND Treasury_Type = 'VOID'
	
	SELECT @TreasuryAmount = SUM(Treasury_Amount)
	FROM   Treasury_Entry(NOLOCK)
	WHERE  Collection_ID = @CollectionId
	       AND Treasury_Type IN ('Shortpay', 'Offline Voucher-Shortpay', 'Refund')
	
	SELECT (
	           COALESCE(Collection_Cash_Take, 0)
	           +
	           COALESCE(Collection_Refills, 0)
	           +
	           COALESCE(@Void, 0)
	           -
	           COALESCE(@TreasuryAmount, 0)
	       ) AS Cash_Take
	FROM   Collection_Calcs WITH(NOLOCK)
	WHERE  COllection_ID = @CollectionId
END

GO

