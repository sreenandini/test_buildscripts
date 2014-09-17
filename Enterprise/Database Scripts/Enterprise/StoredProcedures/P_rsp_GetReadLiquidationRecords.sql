USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_GetReadLiquidationRecords]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_GetReadLiquidationRecords]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- rsp_GetReadLiquidationRecords  
-- -----------------------------------------------------------------  
--   
-- To fetch the read records for read based liquidation  
--   
-- -----------------------------------------------------------------  
-- Revision History  
--   
-- 18/10/2012 Dinesh Rathinavel Created  
--   
-- =================================================================  
  
CREATE PROCEDURE [dbo].[rsp_GetReadLiquidationRecords]
	@Site_Id INT
AS
BEGIN
	DECLARE @Prev_Read_No  INT  
	DECLARE @Min_Read_No   INT  
	DECLARE @Max_Read_No   INT  
	
	
	
	SELECT TOP 1 @Min_Read_No = ISNULL(R.[Read_ID], 0)
	FROM   [Read] R WITH(NOLOCK)
	       INNER JOIN [Installation] I WITH (NOLOCK)
	            ON  I.[Installation_ID] = R.[Installation_ID]
	       INNER JOIN [Bar_Position] BP WITH (NOLOCK)
	            ON  BP.[Bar_Position_ID] = I.[Bar_Position_ID]
	WHERE  R.Read_ID > ISNULL(
	           (
	               SELECT TOP 1 ISNULL(LD.[ReadId], 0)
	               FROM   [LiquidationDetails] LD WITH(NOLOCK)
	               WHERE  LD.[SiteId] = @Site_Id
	               ORDER BY
	                      LD.[ReadId] DESC
	           ),
	           0
	       )
	       AND BP.[Site_ID] = @Site_Id
	ORDER BY
	       R.[Read_ID] ASC
	
	SELECT TOP 1 @Max_Read_No = ISNULL(MAX(R.[Read_ID]), 0)
	FROM   [Read] R WITH(NOLOCK)
	       INNER JOIN [Installation] I WITH (NOLOCK)
	            ON  I.[Installation_ID] = R.[Installation_ID]
	       INNER JOIN [Bar_Position] BP WITH (NOLOCK)
	            ON  BP.[Bar_Position_ID] = I.[Bar_Position_ID]
	WHERE  BP.[Site_ID] = @Site_Id
	--ORDER BY R.[Read_ID] ASC
	
	;WITH Liq_Summary_ForRead_CTE AS
	(
	    SELECT MAX(R.[Read_ID]) AS [Read_ID],
	           DATEADD(DD, 0, DATEDIFF(DD, 0, R.[ReadDate])) AS [Read_Date],
	           SUM(R.[READ_COIN_DROP]) / 100.0 AS CashIn,
	           SUM(R.[READ_RDC_CANCELLED_CREDITS]) / 100.0 AS 
	           READ_RDC_CANCELLED_CREDITS,
	           SUM(
	               R.READ_RDC_TRUE_COIN_OUT * ISNULL(I.Installation_Token_Value, 0)
	           ) / 100.0 AS READ_RDC_TRUE_COIN_OUT,
	           SUM(R.READ_RDC_JACKPOT) / 100.0 AS READ_RDC_JACKPOT,
	           SUM(
	               READ_RDC_TRUE_COIN_IN * ISNULL(I.Installation_Price_Per_Play, 0)
	           ) / 100.0 AS Total_Coins_In,
	           SUM(
	               (
	                   CAST(
	                       (
	                           ISNULL(READ_HANDPAY, 0)
	                       ) AS FLOAT
	                   ) * ISNULL(I.Installation_Price_Per_Play, 0)
	               ) / 100.0
	           ) AS Handpay,
	           (
	               SUM([Read_Ticket_In_Suspense]) + SUM(TICKETS_INSERTED_NONCASHABLE_VALUE)
	           ) / 100.0 AS Tickets_In,
	           (SUM(READ_TICKET) + SUM(TICKETS_PRINTED_NONCASHABLE_VALUE)) / 100.0 AS 
	           Tickets_Out,
	           SUM(CAST(ISNULL(Promo_Cashable_EFT_OUT, 0) AS FLOAT) / 100.0) AS 
	           Promo_Cashable_EFT_OUT,
	           SUM(CAST(ISNULL(NonCashable_EFT_OUT, 0) AS FLOAT) / 100.0) AS 
	           NonCashable_EFT_OUT,
	           SUM(CAST(ISNULL(Cashable_EFT_OUT, 0) AS FLOAT) / 100.0) AS 
	           Cashable_EFT_OUT
	    FROM   [Read] R WITH (NOLOCK)
	           INNER JOIN [Installation] I WITH (NOLOCK)
	                ON  I.[Installation_ID] = R.[Installation_ID]
	           INNER JOIN [Machine] M WITH (NOLOCK)
	                ON  M.[Machine_ID] = I.[Machine_ID]
	           INNER JOIN [Bar_Position] BP WITH (NOLOCK)
	                ON  BP.[Bar_Position_ID] = I.[Bar_Position_ID]
	           INNER JOIN [Site] S WITH (NOLOCK)
	                ON  S.[Site_Id] = BP.[Site_Id]
	    WHERE  R.[Read_Id] BETWEEN @Min_Read_No AND @Max_Read_No
	           AND S.[Site_ID] = @Site_Id
	    GROUP BY
	           DATEADD(DD, 0, DATEDIFF(DD, 0, R.[ReadDate]))
	)
	
	--select * from Liq_Summary_ForRead_CTE
	
	SELECT L.[Read_ID] AS [Read_No],
	       L.Read_Date AS [Read_Date],
	       CAST(ISNULL(L.CashIn, 0) AS DECIMAL(18, 2)) AS CashIn,
	       CAST(
	           (
	               (
	                   ISNULL(Tickets_Out, 0) + ISNULL(L.Promo_Cashable_EFT_OUT, 0) 
	                   + ISNULL(L.NonCashable_EFT_OUT, 0)
	                   + ISNULL(L.Cashable_EFT_OUT, 0)
	               ) + (ISNULL(L.READ_RDC_TRUE_COIN_OUT, 0)) + (ISNULL(L.Handpay, 0))
	           ) AS DECIMAL(18, 2)
	       ) AS CashOut,
	       CAST(
	           (
	               (ISNULL(L.CashIn, 0)) -(
	                   (
	                       ISNULL(Tickets_Out, 0) + ISNULL(L.Promo_Cashable_EFT_OUT, 0) 
	                       + ISNULL(L.NonCashable_EFT_OUT, 0)
	                       + ISNULL(L.Cashable_EFT_OUT, 0)
	                   ) + (ISNULL(L.READ_RDC_TRUE_COIN_OUT, 0)) + (ISNULL(L.Handpay, 0))
	               )
	           ) AS DECIMAL(18, 2)
	       ) AS CashTake,
	       CAST(ISNULL(L.Total_Coins_In, 0) AS DECIMAL(18, 2)) AS Total_Coins_In,
	       CAST(ISNULL(L.Handpay, 0) AS DECIMAL(18, 2)) AS Handpay,
	       CAST(ISNULL(L.Tickets_In, 0) AS DECIMAL(18, 2)) AS Tickets_In,
	       CAST(ISNULL(L.Tickets_Out, 0) AS DECIMAL(18, 2)) AS Tickets_Out
	FROM   Liq_Summary_ForRead_CTE L
	ORDER BY
	       [Read_Date]
END
GO

