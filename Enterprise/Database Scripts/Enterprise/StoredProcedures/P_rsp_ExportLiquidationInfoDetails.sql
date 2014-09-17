USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_ExportLiquidationInfoDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_ExportLiquidationInfoDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[rsp_ExportLiquidationInfoDetails]
(@LiquidationId INT)
AS
BEGIN
	
	DECLARE @Site_Batch_ID INT
	DECLARE @HQ_installation_ID INT
	DECLARE @Read_Date VARCHAR(30)
	
	SELECT @Site_Batch_ID = RIGHT(B.Batch_Ref, LEN(B.Batch_Ref) - LEN(LEFT(B.Batch_Ref, 5))) 
	FROM Batch B
		INNER JOIN LiquidationDetails LD ON LD.CollectionBatchId = B.Batch_ID
	WHERE  LiquidationId = @LiquidationId
	
	SELECT 
		@HQ_installation_ID = R.Installation_ID, 
		@Read_Date = R.Read_Date 
	FROM [READ] R
		INNER JOIN LiquidationDetails LD ON LD.ReadId = R.Read_ID
	WHERE LiquidationId = @LiquidationId
	
	SELECT LiquidationId,
	       @Site_Batch_ID AS CollectionBatchId,
	       ReadId,
	       @HQ_installation_ID AS HQ_installation_ID,
	       @Read_Date AS Read_Date,
	       SiteName,
	       LiquidationPerformedDate,
	       CollectionPerformedDate,
	       ProfitShareGroupId,
	       ExpenseShareGroupId,
	       ExpenseShareAmount,
	       WriteOffAmount,
	       PayPeriodId,
	       MeterIn,
	       MeterOut,
	       BalanceDue,
	       RetailerShareBeforeAdjustment,
	       RetailerNegativeNet,
	       RetailerSharePercentage,
	       TicketPaid,
	       AdvanceToRetailer,
	       Retailer,
	       FixedExpenseAmount,
	       RetailerShareBeforeFixedExpense,
	       CarriedForwardExpense,
	       Negative_Net,
	       RetailerExpenseShareAmount,
	       PrevCarriedForwardExpense
	FROM   LiquidationDetails
	WHERE  LiquidationId = @LiquidationId 
	       
	       FOR XML AUTO, ELEMENTS, ROOT('LiquidationDetails')
END 

GO

