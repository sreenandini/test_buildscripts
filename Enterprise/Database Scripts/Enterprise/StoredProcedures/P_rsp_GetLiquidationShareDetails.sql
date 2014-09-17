USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetLiquidationShareDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetLiquidationShareDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [rsp_GetLiquidationShareDetails]
	@BatchId INT = NULL,
	@ReadId INT = NULL
AS
BEGIN
	IF(@ReadId=0)
	BEGIN 
		SET @ReadId=NULL
	END
	IF(@BatchId=0)
	BEGIN 
		SET @BatchId=NULL
	END
	
	
	DECLARE @TotalExpenseAmount AS DECIMAL(18, 2)
	DECLARE @RetailerNet AS DECIMAL(18, 2)
	
	SELECT
		ISNULL(LD.RetailerNetRevenue, 0) AS RetailerNet
	INTO #TempRetailerNet 
	FROM  LiquidationDetails LD WITH(NOLOCK)
	WHERE
		((LD.CollectionBatchId = @BatchId AND @ReadId IS NULL)
		OR (LD.ReadId = @ReadId AND @BatchId IS NULL))
	
	SELECT @TotalExpenseAmount = (SUM(CASE WHEN ISNULL(SH.ShareHolderId, 0) = 0 THEN 0 ELSE ISNULL(LSD.ExpenseShareAmount, 0) END))
	FROM  LiquidationDetails LD WITH(NOLOCK)
	      LEFT JOIN LiquidationShareDetails LSD WITH(NOLOCK) ON LSD.LiquidationId = LD.LiquidationId
	      LEFT JOIN ShareHolders SH WITH(NOLOCK) ON  SH.ShareHolderId = LSD.ShareHolderId 
	WHERE   
		(
			(SH.ShareHolderId IS NULL OR (SH.ShareHolderId <> 3 )) AND
			(LSD.ExpenseShareAmount IS NOT NULL) AND
			((LD.CollectionBatchId = @BatchId AND @ReadId IS NULL)
		OR (LD.ReadId = @ReadId AND @BatchId IS NULL)))
		
	;WITH SH_CTE AS (
	SELECT lsd.ShareHolderName,
	       CAST(ISNULL(lsd.ExpenseShareAmount, 0) AS DECIMAL(18,2)) AS ExpenseShareAmount
	FROM  LiquidationDetails LD  WITH(NOLOCK)
	       LEFT JOIN LiquidationShareDetails LSD WITH(NOLOCK) 
				ON LSD.LiquidationId = LD.LiquidationId
	       LEFT JOIN ShareHolders SH WITH(NOLOCK)
	            ON  SH.ShareHolderId = LSD.ShareHolderId
	WHERE  
	    (
			(SH.ShareHolderId IS NULL OR (SH.ShareHolderId <> 3 )) AND
			(LSD.ExpenseShareAmount IS NOT NULL) AND
			((LD.CollectionBatchId = @BatchId AND @ReadId IS NULL)
		OR (LD.ReadId = @ReadId AND @BatchId IS NULL)))
	)
	
	SELECT 
		ISNULL(S.ShareHolderName, '') AS ShareHolderName,
		CAST(ISNULL(ExpenseShareAmount, 0) AS DECIMAL(18, 2)) AS ExpenseShareAmount,
		CAST(ISNULL(@TotalExpenseAmount, 0) + ISNULL(RetailerNet, 0) AS DECIMAL(18, 2)) AS TotalNetRevenue
	FROM #TempRetailerNet T
		LEFT JOIN SH_CTE S ON 1 = 1
	
	DROP TABLE #TempRetailerNet
END 

GO


