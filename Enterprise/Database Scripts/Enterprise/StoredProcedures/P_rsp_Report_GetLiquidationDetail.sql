USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_Report_GetLiquidationDetail]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_Report_GetLiquidationDetail]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =================================================================
-- rsp_Report_GetLiquidationDetail 1,1
-- -----------------------------------------------------------------
-- 
-- To liquidation details for the liquidation report
-- 
-- -----------------------------------------------------------------
-- Revision History
-- 
-- 05/12/2012 Dinesh Rathinavel Created
-- 
-- =================================================================

CREATE PROCEDURE [dbo].[rsp_Report_GetLiquidationDetail]
	@BatchId INT = NULL,
	@ReadId INT = NULL
AS
BEGIN
	DECLARE @Site_Id           INT
	DECLARE @Site_Name         VARCHAR(50)
	DECLARE @Read_Date         DATETIME
	DECLARE @CarriedFwdAmount  DECIMAL(18, 2)
	DECLARE @Site_Batch_ID     INT
	IF (@BatchId = 0)
	BEGIN
	    SET @BatchId = NULL
	END
	
	IF (@ReadId = 0)
	BEGIN
	    SET @ReadId = NULL
	END
	
	IF (@ReadId IS NOT NULL AND @BatchId IS NULL)
	BEGIN
	    SELECT @Site_Id = S.[Site_ID],
	           @Site_Name = S.[Site_Name],
	           @Read_Date = R.ReadDate
	    FROM   [Read] R
	           INNER JOIN [Installation] I WITH (NOLOCK)
	                ON  I.[Installation_ID] = R.[Installation_ID]
	           INNER JOIN [Bar_Position] BP WITH (NOLOCK)
	                ON  BP.[Bar_Position_ID] = I.[Bar_Position_ID]
	           INNER JOIN [Site] S
	                ON  S.[Site_ID] = BP.[Site_ID]
	    WHERE  R.[Read_ID] = @ReadId
	END
	
	IF (@ReadId IS NULL AND @BatchId IS NOT NULL)
	BEGIN
	    SELECT @Site_Id = S.[Site_ID],
	           @Site_Name = S.[Site_Name],
	           @Site_Batch_ID = RIGHT(B.Batch_Ref, LEN(B.Batch_Ref) - LEN(LEFT(B.Batch_Ref, 5)))
	    FROM   [Batch] B
	           INNER JOIN [Site] S
	                ON  S.[Site_Code] = SUBSTRING(B.[Batch_Ref], 1, CHARINDEX(',', B.[Batch_Ref], 1) -1)
	    WHERE  B.[Batch_ID] = @BatchId
	END
	
	SELECT TOP 1 @CarriedFwdAmount = CAST(
	           CASE 
	                WHEN (
	                         (ISNULL(LD.CarriedForwardExpense, 0) <= 0)
	                         OR (ISNULL(RetailerShareBeforeFixedExpense, 0) > = 0)
	                     ) THEN 0
	                ELSE LD.CarriedForwardExpense
	           END AS DECIMAL(18, 2)
	       )
	FROM   [dbo].LiquidationDetails LD WITH(NOLOCK)
	WHERE  (
	           (LD.CollectionBatchId <> @BatchId AND @ReadId IS NULL)
	           OR (LD.ReadId <> @ReadId AND @BatchId IS NULL)
	       )
	       AND LD.SiteId = @Site_Id
	ORDER BY
	       LD.LiquidationId DESC
	
	SELECT TOP 1
	       LD.LiquidationId,
	       @Site_Name AS SiteName,
	       @Site_Batch_ID AS Site_Batch_ID,
	       CONVERT(NVARCHAR(24), CP.Calendar_Period_End_Date, 121) AS Period_End,
	       CAST(ISNULL(LD.WriteOffAmount, 0) AS DECIMAL(18, 2)) AS 
	       WriteOffAmount,
	       CAST(ISNULL(LD.MeterIn, 0) AS DECIMAL(18, 2)) AS MeterIn,
	       CAST(ISNULL(LD.MeterOut, 0) AS DECIMAL(18, 2)) AS MeterOut,
	       CAST(ISNULL(LD.NetAmount, 0) AS DECIMAL(18, 2)) AS NetAmount,
	       CAST(
	           ISNULL(LD.RetailerShareBeforeAdjustment, 0) AS DECIMAL(18, 2)
	       ) AS RetailerShareBeforeAdjustment,
	       CAST(ISNULL(LD.RetailerNegativeNet, 0) AS DECIMAL(18, 2)) AS 
	       RetailerNegativeNet,
	       CAST(
	           ISNULL(LD.RetailerShareAfterAdjustment, 0) AS DECIMAL(18, 2)
	       ) AS RetailerShareAfterAdjustment,
	       CAST(ISNULL(LD.TicketPaid, 0) AS DECIMAL(18, 2)) AS TicketPaid,
	       CAST(ISNULL(LD.AdvanceToRetailer, 0) AS DECIMAL(18, 2)) AS 
	       AdvanceToRetailer,
	       CAST(ISNULL(LD.BalanceDue, 0) AS DECIMAL(18, 2)) AS BalanceDue,
	       CAST(
	           ISNULL(LD.RetailerShareBeforeFixedExpense, 0) AS DECIMAL(18, 2)
	       ) AS RetailerShareBeforeFixedExpense,
	       CAST(ISNULL(LD.FixedExpenseAmount, 0) AS DECIMAL(18, 2)) AS 
	       FixedExpenseAmount,
	       CAST(ISNULL(@CarriedFwdAmount, 0) AS DECIMAL(18, 2)) AS 
	       CarriedForwardExpense,
	       --CAST(ISNULL(LD.CarriedForwardExpense, 0) AS DECIMAL(18, 2)) AS CarriedForwardExpense,
	       CAST(ISNULL(LD.RetailerNetRevenue, 0) AS DECIMAL(18, 2)) AS 
	       RetailerNetRevenue,
	       @Read_Date AS Read_Date
	FROM   [dbo].LiquidationDetails LD WITH(NOLOCK)
	       LEFT JOIN [dbo].[Calendar_Period] CP WITH(NOLOCK)
	            ON  CP.Calendar_Period_ID = LD.PayPeriodId
	WHERE  (LD.CollectionBatchId = @BatchId AND @ReadId IS NULL)
	       OR  (LD.ReadId = @ReadId AND @BatchId IS NULL)
	ORDER BY
	       LD.LiquidationId DESC
END
GO

