USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_GetReadLiquidationDetails]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_GetReadLiquidationDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- rsp_GetReadLiquidationDetails  
-- -----------------------------------------------------------------  
--   
-- To fetch the read records for read based liquidation  
--   
-- -----------------------------------------------------------------  
-- Revision History  
--   
-- 07/12/2012 Dinesh Rathinavel Created  
--   
-- =================================================================  
  
CREATE PROCEDURE [dbo].[rsp_GetReadLiquidationDetails]
	@Site_Id INT,
	@StartDate DATETIME,
	@EndDate DATETIME
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
	WHERE  BP.[Site_ID] = @Site_Id
	       AND DATEADD(DD, 0, DATEDIFF(DD, 0, R.[ReadDate])) >= DATEADD(DD, 0, DATEDIFF(DD, 0, @StartDate))
	       AND DATEADD(DD, 0, DATEDIFF(DD, 0, R.[ReadDate])) <= DATEADD(DD, 0, DATEDIFF(DD, 0, @EndDate))
	       AND R.Read_ID > ISNULL(
	               (
	                   SELECT TOP 1 ISNULL(LD.[ReadId], 0)
	                   FROM   [LiquidationDetails] LD WITH(NOLOCK)
	                   WHERE  LD.[SiteId] = @Site_Id
	                   ORDER BY
	                          LD.[ReadId] DESC
	               ),
	               0
	           )
	ORDER BY
	       R.[Read_ID] ASC
	
	SELECT TOP 1 @Max_Read_No = ISNULL(MAX(R.Read_ID), 0)
	FROM   [Read] R WITH(NOLOCK)
	       INNER JOIN [Installation] I WITH (NOLOCK)
	            ON  I.[Installation_ID] = R.[Installation_ID]
	       INNER JOIN [Bar_Position] BP WITH (NOLOCK)
	            ON  BP.[Bar_Position_ID] = I.[Bar_Position_ID]
	WHERE  BP.[Site_ID] = @Site_Id
	       AND DATEADD(DD, 0, DATEDIFF(DD, 0, R.[ReadDate])) >= DATEADD(DD, 0, DATEDIFF(DD, 0, @StartDate))
	       AND DATEADD(DD, 0, DATEDIFF(DD, 0, R.[ReadDate])) <= DATEADD(DD, 0, DATEDIFF(DD, 0, @EndDate))
	
	;WITH Liq_Summary_ForRead_CTE AS
	(
	    SELECT R.[Read_ID],
	           S.Region,
	           BP.[Bar_Position_Name],
	           R.[ReadDate] AS [Read_Date],
	           (
	               R.[READ_COIN_DROP] * ISNULL(I.Installation_Price_Per_Play, 0)
	           ) AS CashIn,
	           (
	               (
	                   R.[READ_RDC_CANCELLED_CREDITS] * ISNULL(I.Installation_Price_Per_Play, 0)
	               ) 
	               + (
	                   R.READ_RDC_TRUE_COIN_OUT * ISNULL(I.Installation_Token_Value, 0)
	               ) 
	               + (
	                   R.READ_RDC_JACKPOT * ISNULL(I.Installation_Price_Per_Play, 0)
	               )
	           ) AS CashOut,
	           (
	               READ_RDC_TRUE_COIN_IN * ISNULL(I.Installation_Token_Value, 0)
	           ) AS Total_Coins_In,
	           (READ_HANDPAY * ISNULL(I.Installation_Price_Per_Play, 0)) AS 
	           Handpay,
	           (
	               [Read_Ticket_In_Suspense] + 
	               TICKETS_INSERTED_NONCASHABLE_VALUE
	           ) AS Tickets_In,
	           (READ_TICKET + TICKETS_PRINTED_NONCASHABLE_VALUE) AS Tickets_Out
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
	           AND DATEADD(DD, 0, DATEDIFF(DD, 0, R.[ReadDate])) >= DATEADD(DD, 0, DATEDIFF(DD, 0, @StartDate))
	           AND DATEADD(DD, 0, DATEDIFF(DD, 0, R.[ReadDate])) <= DATEADD(DD, 0, DATEDIFF(DD, 0, @EndDate))
	           AND S.Site_ID = @Site_Id
	)
	
	--select * from Liq_Summary_ForRead_CTE
	
	SELECT --S.[Name],
	       [Read_ID] AS [Read_No],
	       [Bar_Position_Name] AS [Bar_Pos_Name],
	       [Read_Date] AS [Read_Date],
	       CAST(
	           CASE 
	                WHEN ISNULL(CashIn, 0) = 0 THEN 0
	                ELSE CashIn / 100.0
	           END AS DECIMAL(18, 2)
	       ) AS CashIn,
	       CAST(
	           CASE 
	                WHEN ISNULL(CashOut, 0) = 0 THEN 0
	                ELSE CashOut / 100.0
	           END AS DECIMAL(18, 2)
	       ) AS CashOut,
	       CAST((CashIn - CashOut) / 100.0 AS DECIMAL(18, 2)) AS CashTake,
	       CAST(
	           CASE 
	                WHEN ISNULL(Total_Coins_In, 0) = 0 THEN 0
	                ELSE Total_Coins_In / 100.0
	           END AS DECIMAL(18, 2)
	       ) AS Total_Coins_In,
	       CAST(
	           CASE 
	                WHEN ISNULL(Handpay, 0) = 0 THEN 0
	                ELSE Handpay / 100.0
	           END AS DECIMAL(18, 2)
	       ) AS Handpay,
	       CAST(
	           CASE 
	                WHEN ISNULL(Tickets_In, 0) = 0 THEN 0
	                ELSE Tickets_In / 100.0
	           END AS DECIMAL(18, 2)
	       ) AS Tickets_In,
	       CAST(
	           CASE 
	                WHEN ISNULL(Tickets_Out, 0) = 0 THEN 0
	                ELSE Tickets_Out / 100.0
	           END AS DECIMAL(18, 2)
	       ) AS Tickets_Out
	FROM   Liq_Summary_ForRead_CTE L
	ORDER BY
	       L.[Read_ID]
END
GO
