USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_Get_LiquidationSummary_ForRead]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_Get_LiquidationSummary_ForRead]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- rsp_Get_LiquidationSummary_ForRead
-- -----------------------------------------------------------------
-- 
-- To calculate liquidation summary values w.r.t Read data
-- 
-- -----------------------------------------------------------------
-- Revision History
-- 
-- 10/10/2012 Dinesh Rathinavel Created
-- 
-- =================================================================


CREATE PROCEDURE [dbo].[rsp_Get_LiquidationSummary_ForRead]
	@Site_Id INT,
	@StartDate DATETIME,
	@EndDate DATETIME
AS
BEGIN
	DECLARE @Min_Read_No			INT
	DECLARE @Max_Read_No			INT
	DECLARE @RetailNegativeNet		AS DECIMAL(18, 2)
	DECLARE @SETTINGVALUE			AS FLOAT
	DECLARE @CarriedFwdExpense		AS DECIMAL(18, 2)
	DECLARE @Handpay				AS DECIMAL(18, 2)
	DECLARE @JACKPOT				AS DECIMAL(18, 2)
	DECLARE @Advance_To_Retailer	AS DECIMAL(18, 2)
	DECLARE @Read_Time            AS VARCHAR(5)
	DECLARE @Read_Hour            AS INT
	DECLARE @Read_Minutes         AS INT
	
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
	               WHERE LD.SiteId = @Site_Id
	               ORDER BY
	                      LD.[ReadId] DESC
	           ),
	           0
	       )
	       AND BP.[Site_ID] = @Site_Id
	       AND DATEADD(
	               DD,
	               0,
	               DATEDIFF(DD, 0, CONVERT(DATETIME, R.[Read_Date], 105))
	           ) >= DATEADD(DD, 0, DATEDIFF(DD, 0, @StartDate))
	       AND DATEADD(
	               DD,
	               0,
	               DATEDIFF(DD, 0, CONVERT(DATETIME, R.[Read_Date], 105))
	           ) <= DATEADD(DD, 0, DATEDIFF(DD, 0, @EndDate))
	ORDER BY
	       R.[Read_ID] ASC
	
	SELECT TOP 1 @Max_Read_No = ISNULL(MAX(Read_ID), 0)
	FROM   [Read] R WITH(NOLOCK)
	       INNER JOIN [Installation] I WITH (NOLOCK)
	            ON  I.[Installation_ID] = R.[Installation_ID]
	       INNER JOIN [Bar_Position] BP WITH (NOLOCK)
	            ON  BP.[Bar_Position_ID] = I.[Bar_Position_ID]
	WHERE  BP.[Site_ID] = @Site_Id
	       AND DATEADD(
	               DD,
	               0,
	               DATEDIFF(DD, 0, CONVERT(DATETIME, R.[Read_Date], 105))
	           ) >= DATEADD(DD, 0, DATEDIFF(DD, 0, @StartDate))
	       AND DATEADD(
	               DD,
	               0,
	               DATEDIFF(DD, 0, CONVERT(DATETIME, R.[Read_Date], 105))
	           ) <= DATEADD(DD, 0, DATEDIFF(DD, 0, @EndDate))
	
	
	SELECT TOP 1 @RetailNegativeNet = CASE 
	                                       WHEN ISNULL([Negative_Net], 0) < 0 THEN 
	                                            [Negative_Net]
	                                       ELSE 0
	                                  END,
	       @CarriedFwdExpense = CASE 
	                                 WHEN ((ISNULL(CarriedForwardExpense, 0) <= 0) OR (ISNULL(RetailerShareBeforeFixedExpense, 0) >= 0)) THEN 
	                                      0
	                                 ELSE CarriedForwardExpense
	                            END
	FROM   [LiquidationDetails] WITH(NOLOCK)
	WHERE  [SiteID] = @Site_Id
	       AND ReadId IS NOT NULL
	ORDER BY
	       LiquidationId DESC
	
	SET @SETTINGVALUE = 0.00
	
	EXEC [dbo].[rsp_GetSiteSetting] @Site_ID,
	     'DailyAutoReadTime',
	     @Read_Time OUTPUT
	
	SELECT DATA INTO #Read_Data
	FROM   dbo.fnsplit(@Read_Time, ':')
	
	SELECT TOP 1 @Read_Hour = CAST(DATA AS INT)
	FROM   #Read_Data
	
	DELETE 
	FROM   #Read_Data
	WHERE  DATA = (
	           SELECT TOP 1 DATA
	           FROM   #Read_Data
	       )
	
	SELECT @Read_Minutes = CAST(DATA AS INT)
	FROM   #Read_Data
	
	SELECT @Handpay = SUM(Treasury_Amount)  
      FROM Treasury_Entry T  
     WHERE T.TREASURY_TYPE IN ('Handpay Credit', 'Attendantpay Credit') -- included the type Attendantpay Credit to calculate Advance to Retailer value
       AND (
	               T.[Treasury_Actual_Date] >= DATEADD(
	                   MINUTE,
	                   @Read_Minutes,
	                   DATEADD(
	                       HOUR,
	                       @Read_Hour, DATEADD(DD, -2, DATEDIFF(DD, -1, CONVERT(DATETIME, @StartDate, 105)))
	                   )
	               )
	               
	               AND T.[Treasury_Actual_Date] <= DATEADD(
	                   MINUTE,
	                   @Read_Minutes,
	                   DATEADD(HOUR, @Read_Hour, DATEADD(DD, 0, DATEDIFF(DD, 0, CONVERT(DATETIME, @StartDate, 105))))
	               )  
	           )
       AND Treasury_Amount >= 5000
  
    SELECT @JACKPOT = SUM(Treasury_Amount)  
      FROM Treasury_Entry T  
     WHERE T.TREASURY_TYPE IN ('Handpay Jackpot', 'Attendantpay Jackpot', 'Mystery Jackpot', 'PROGRESSIVE', 'PROG') -- -- included the type Attendantpay Jackpot to calculate Advance to Retailer value
       AND (
	               T.[Treasury_Actual_Date] >= DATEADD(
	                   MINUTE,
	                   @Read_Minutes,
	                   DATEADD(
	                       HOUR,
	                       @Read_Hour, DATEADD(DD, -2, DATEDIFF(DD, -1, CONVERT(DATETIME, @StartDate, 105)))
	                   )
	               )
	               AND T.[Treasury_Actual_Date] <= DATEADD(
	                   MINUTE,
	                   @Read_Minutes,
	                   DATEADD(
	                       HOUR,
	                       @Read_Hour,
	                       DATEADD(DD, 0, DATEDIFF(DD, 0, CONVERT(DATETIME, @StartDate, 105)))
	                   )
	               )  
	           )
       AND Treasury_Amount >= 5000  
  
    SET @Advance_To_Retailer = COALESCE(@Handpay,0) + COALESCE(@JACKPOT,0)
	
	;WITH Liq_Summary_ForRead_CTE AS
	(
	    SELECT MAX(R.[Read_ID]) AS Read_Id,
			   SUM(CAST(ISNULL(CASH_IN_1P, 0) AS FLOAT) / 100) AS CASH_IN_1P,
	           SUM(CAST(ISNULL(CASH_IN_2P, 0) AS FLOAT) / 50) AS CASH_IN_2P,
	           SUM(CAST(ISNULL(CASH_IN_5P, 0) AS FLOAT) / 20) AS CASH_IN_5P,
	           SUM(CAST(ISNULL(CASH_IN_10P, 0) AS FLOAT) / 10) AS CASH_IN_10P,
	           SUM(CAST(ISNULL(CASH_IN_20P, 0) AS FLOAT) / 5) AS CASH_IN_20P,
	           SUM(CAST(ISNULL(CASH_IN_50P, 0) AS FLOAT) / 2) AS CASH_IN_50P,
	           SUM(CAST(ISNULL(CASH_IN_100P, 0) AS FLOAT)) AS CASH_IN_100P,
	           SUM(CAST(ISNULL(CASH_IN_200P, 0) AS FLOAT) * 2) AS CASH_IN_200P,
	           SUM(CAST(ISNULL(CASH_IN_1P, 0) AS FLOAT) * 0.01) AS 
	           Cash_Collected_1p,
	           SUM(CAST(ISNULL(CASH_IN_2P, 0) AS FLOAT) * 0.02) AS 
	           Cash_Collected_2p,
	           SUM(CAST(ISNULL(CASH_IN_5P, 0) AS FLOAT) * 0.05) AS 
	           Cash_Collected_5p,
	           SUM(CAST(ISNULL(CASH_IN_10P, 0) AS FLOAT) * 0.10) AS 
	           Cash_Collected_10p,
	           SUM(CAST(ISNULL(CASH_IN_20P, 0) AS FLOAT) * 0.20) AS 
	           Cash_Collected_20p,
	           SUM(CAST(ISNULL(CASH_IN_50P, 0) AS FLOAT) * 0.50) AS 
	           Cash_Collected_50p,
	           SUM(CAST(ISNULL(CASH_IN_100P, 0) AS FLOAT)) AS 
	           Cash_Collected_100p,
	           SUM(CAST(ISNULL(CASH_IN_200P, 0) AS FLOAT) * 2) AS 
	           Cash_Collected_200p,
	           SUM(CAST(ISNULL(CASH_IN_500P, 0) AS FLOAT) * 5) AS 
	           Cash_Collected_500p,
	           SUM(CAST(ISNULL(CASH_IN_1000P, 0) AS FLOAT) * 10) AS 
	           Cash_Collected_1000p,
	           SUM(CAST(ISNULL(CASH_IN_2000P, 0) AS FLOAT) * 20) AS 
	           Cash_Collected_2000p,
	           SUM(CAST(ISNULL(CASH_IN_5000P, 0) AS FLOAT) * 50) AS 
	           Cash_Collected_5000p,
	           SUM(CAST(ISNULL(CASH_IN_10000P, 0) AS FLOAT) * 100) AS 
	           Cash_Collected_10000p,
	           SUM(CAST(ISNULL(CASH_IN_20000P, 0) AS FLOAT) * 200) AS 
	           Cash_Collected_20000p,
	           SUM(CAST(ISNULL(CASH_IN_50000P, 0) AS FLOAT) * 500) AS 
	           Cash_Collected_50000p,
	           SUM(CAST(ISNULL(CASH_IN_100000P, 0) AS FLOAT) * 1000) AS 
	           Cash_Collected_100000p,
	           SUM(
	               R.[READ_COIN_DROP] * ISNULL(I.Installation_Price_Per_Play, 0)
	           ) / 100.0 AS CashIn,
	           SUM(
	               (
	                   CAST(ISNULL(READ_HANDPAY, 0) AS FLOAT) * ISNULL(I.Installation_Price_Per_Play, 0)
	               ) / 100.0
	           ) AS READ_HANDPAY,
	           SUM(
	               (
	                   CAST(ISNULL(READ_RDC_JACKPOT, 0) AS FLOAT) * ISNULL(I.Installation_Price_Per_Play, 0)
	               ) / 100.0
	           ) AS READ_JACKPOT,
	           SUM(
	               (
	                   CAST(ISNULL(Progressive_Win_Value, 0) AS FLOAT) * ISNULL(I.Installation_Price_Per_Play, 0)
	               ) / 100.0
	           ) AS Progressive_Win_Value,
	           SUM(
	               (
	                   CAST(ISNULL(progressive_win_Handpay_value, 0) AS FLOAT) *
	                   ISNULL(I.Installation_Price_Per_Play, 0)
	               ) / 100.0
	           ) AS progressive_win_Handpay_value,
	           SUM(CAST(ISNULL(Mystery_Machine_Paid, 0) AS FLOAT)) AS 
	           Mystery_Machine_Paid,
	           SUM(CAST(ISNULL(Mystery_Attendant_Paid, 0) AS FLOAT)) AS 
	           Mystery_Attendant_Paid,
	           SUM(
	               CAST(ISNULL(TICKETS_INSERTED_NONCASHABLE_VALUE, 0) AS FLOAT) 
	               / 100.0
	           ) AS RDC_TICKETS_INSERTED_NONCASHABLE_VALUE,
	           SUM(
	               CAST(ISNULL(TICKETS_PRINTED_NONCASHABLE_VALUE, 0) AS FLOAT) /
	               100.0
	           ) AS RDC_TICKETS_PRINTED_NONCASHABLE_VALUE,
	           SUM(CAST(ISNULL(Promo_Cashable_EFT_IN, 0) AS FLOAT) / 100.0) AS 
	           Promo_Cashable_EFT_IN,
	           SUM(CAST(ISNULL(Promo_Cashable_EFT_OUT, 0) AS FLOAT) / 100.0) AS 
	           Promo_Cashable_EFT_OUT,
	           SUM(CAST(ISNULL(NonCashable_EFT_IN, 0) AS FLOAT) / 100.0) AS 
	           NonCashable_EFT_IN,
	           SUM(CAST(ISNULL(NonCashable_EFT_OUT, 0) AS FLOAT) / 100.0) AS 
	           NonCashable_EFT_OUT,
	           SUM(CAST(ISNULL(Cashable_EFT_IN, 0) AS FLOAT) / 100.0) AS 
	           Cashable_EFT_IN,
	           SUM(CAST(ISNULL(Cashable_EFT_OUT, 0) AS FLOAT) / 100.0) AS 
	           Cashable_EFT_OUT,
	           SUM(
	               (CAST(COALESCE(Read_Ticket_In_Suspense, 0) AS FLOAT)) / 100.0
	           ) AS TicketInValue,
	           SUM(CAST(COALESCE(READ_TICKET, 0) AS FLOAT)) / 100.0 AS 
	           CashableTicketPrintedValue,
	           SUM(
	               (
	                   CAST(
	                       (
	                           ISNULL(READ_HANDPAY, 0)
	                       ) AS FLOAT
	                   ) * ISNULL(I.Installation_Price_Per_Play, 0)
	               ) / 100.0
	           ) AS Handpay,
	           SUM(
	               (
	                   CAST(ISNULL(Progressive_Win_Handpay_Value, 0) AS FLOAT) *
	                   ISNULL(I.Installation_Price_Per_Play, 0)
	               ) / 100.0
	           ) AS Progressive_Value_Declared,
	           SUM(
	               (
	                   CAST(ISNULL(R.READ_RDC_TRUE_COIN_OUT, 0) AS FLOAT) * 
	                   ISNULL(I.Installation_Token_Value, 0)
	               ) / 100.0
	           ) AS READ_RDC_TRUE_COIN_OUT
	    FROM   [Read] R WITH (NOLOCK)
	           INNER JOIN [Installation] I WITH (NOLOCK)
	                ON  I.[Installation_ID] = R.[Installation_ID]
	           INNER JOIN [Machine] M WITH (NOLOCK)
	                ON  M.Machine_ID = I.Machine_ID
	           INNER JOIN [Bar_Position] BP WITH (NOLOCK)
	                ON  BP.[Bar_Position_ID] = I.[Bar_Position_ID]
	    WHERE  R.[Read_ID] BETWEEN @Min_Read_No AND @Max_Read_No
	           AND BP.[Site_ID] = @Site_Id
	           AND DATEADD(
	                   DD,
	                   0,
	                   DATEDIFF(DD, 0, CONVERT(DATETIME, R.[Read_Date], 105))
	               ) >= DATEADD(DD, 0, DATEDIFF(DD, 0, @StartDate))
	           AND DATEADD(
	                   DD,
	                   0,
	                   DATEDIFF(DD, 0, CONVERT(DATETIME, R.[Read_Date], 105))
	               ) <= DATEADD(DD, 0, DATEDIFF(DD, 0, @EndDate))
	)    
	
	SELECT S.[Site_Name],
	       CAST(ISNULL(L.CashIn, 0) AS DECIMAL(18, 2)) AS CashIn,
	       CAST(
	           ISNULL(L.CashableTicketPrintedValue, 0) + ISNULL(L.Handpay, 0) +
	           ISNULL(L.RDC_TICKETS_PRINTED_NONCASHABLE_VALUE, 0) + ISNULL(L.Promo_Cashable_EFT_OUT, 0) 
	           +
	           ISNULL(L.NonCashable_EFT_OUT, 0) + ISNULL(L.Cashable_EFT_OUT, 0) 
	           + ISNULL(L.READ_RDC_TRUE_COIN_OUT, 0) AS DECIMAL(18, 2)
	       ) AS CashOut,
	       --Read Handpay already sum up with Jackpot, so subtracted the jackpot value
			CAST(ISNULL(@Advance_To_Retailer, 0) AS DECIMAL(18,2)) AS Advance_To_Retailer
	       
	       INTO #Temp_LiqDetails
	FROM   Liq_Summary_ForRead_CTE L
	       INNER JOIN [Read] R WITH (NOLOCK)
	            ON  R.[Read_ID] = L.[Read_ID]
	       INNER JOIN [Installation] I WITH (NOLOCK)
	            ON  I.[Installation_ID] = R.[Installation_ID]
	       INNER JOIN [Machine] M WITH (NOLOCK)
	            ON  M.[Machine_ID] = I.[Machine_ID]
	       INNER JOIN [Bar_Position] BP WITH (NOLOCK)
	            ON  BP.[Bar_Position_ID] = I.[Bar_Position_ID]
	       INNER JOIN [Site] S WITH (NOLOCK)
	            ON  S.[Site_Id] = BP.[Site_Id]
	WHERE  S.[Site_ID] = @Site_Id
	
	SELECT NULL AS Collection_No,
	       @Max_Read_No AS Read_No,
	       [Site_Name] AS Retailer_Name,
	       CONVERT(DATETIME, GETDATE(), 105) AS Liquidation_Date,
	       CAST(SUM([CashIn]) AS DECIMAL(18, 2)) AS Gross,	--1 Gross (Meters In)
	       CAST(SUM([CashOut]) AS DECIMAL(18, 2)) AS Tickets_Expected,	--2 Tickets Expected (Meters Out)
	       CAST((SUM([CashIN]) - SUM(CashOut)) AS DECIMAL(18, 2)) AS Net,	--3 Net
	       CAST(
	           ((SUM([CashIn]) - SUM(CashOut)) * @SETTINGVALUE) AS 
	           DECIMAL(18, 2)
	       ) AS Net_Percentage,	--4 Net x Percentage    Retailer  share before adjustment
	       CAST(@SETTINGVALUE AS DECIMAL(18, 2)) AS Percentage_Setting,
	       CAST(@RetailNegativeNet AS DECIMAL(18, 2)) AS Retailer_Negative_Net,	--5 Retailers Negative Net
	       CAST(
	           (
	               ((SUM([CashIn]) - SUM(CashOut)) * @SETTINGVALUE) +
	               @RetailNegativeNet
	           ) AS DECIMAL(18, 2)
	       ) AS Retailer_Share,	--6 Retailers Share(4+5)    Retailer  share after adjustment
	       CAST(SUM([CashOut]) AS DECIMAL(18, 2)) AS Tickets_Paid,	--7 Tickets Paid(2)
	       CAST(Advance_To_Retailer AS DECIMAL(18, 2)) AS Advance_To_Retailer,	--8 Advance to Retailer
	       CASE 
	            WHEN (
	                     ((SUM([CashIn]) - SUM(CashOut)) * @SETTINGVALUE)
	                     + @RetailNegativeNet
	                 ) > 0 THEN CAST(
	                     (
	                         ((SUM([CashIn]) - SUM(CashOut)) * @SETTINGVALUE) 
	                         + @RetailNegativeNet
	                     ) + (SUM([CashOut]) -[Advance_To_Retailer]) AS DECIMAL(18, 2)
	                 )
	            ELSE CAST((SUM([CashOut]) - [Advance_To_Retailer]) AS DECIMAL(18, 2))
	       END AS Retailer,	--9 --if Retailer's Share>0 then Retailer=(6+7-8) else <0 then Retailer=(7-8)
	       CASE 
	            WHEN (
	                     ((SUM([CashIn]) - SUM(CashOut)) * @SETTINGVALUE)
	                     + @RetailNegativeNet
	                 ) > 0 THEN --if Retailer's Share>0 =(3-4)+(8-5) else <0 =(3+8)
	                 CAST(
	                     (
	                         (
	                             (SUM([CashIn]) - SUM(CashOut)) -((SUM([CashIn]) - SUM(CashOut)) * @SETTINGVALUE)
	                         ) + ([Advance_To_Retailer] - @RetailNegativeNet)
	                     ) AS DECIMAL(18, 2)
	                 )
	            ELSE CAST(
	                     (
	                         (SUM([CashIn]) - SUM(CashOut)) + COALESCE(Advance_To_Retailer, 0)
	                     ) AS DECIMAL(18, 2)
	                 )
	       END AS Balance_Due,	--10 if Retailer's Share>0 then Balance_Due=(6+7-8) else <0 then Balance_Due=(7-8)
	       CASE 
	            WHEN (
	                     ((SUM([CashIN]) - SUM(CashOut)) * @SETTINGVALUE)
	                     + @RetailNegativeNet
	                 ) > 0 THEN CAST(
	                     (
	                         ((SUM([CashIN]) - SUM(CashOut)) * @SETTINGVALUE)
	                         + @RetailNegativeNet
	                     ) - CAST(
	                         (
	                             (
	                                 (SUM([CashIn]) - SUM(CashOut)) -((SUM([CashIN]) - SUM(CashOut)) * @SETTINGVALUE)
	                             ) + ([Advance_To_Retailer] - @RetailNegativeNet)
	                         ) AS DECIMAL(18, 2)
	                     ) AS DECIMAL(18, 2)
	                 )
	            ELSE CAST(
	                     0 - CAST(
	                         (
	                             (SUM([CashIn]) - SUM(CashOut)) + COALESCE(Advance_To_Retailer, 0)
	                         ) AS DECIMAL(18, 2)
	                     ) AS DECIMAL(18, 2)
	                 )
	       END AS RetailerShareBeforeFixedExpense,
	       CAST(0 AS INT) AS ProfitShareGroupId,
	       CAST(0 AS INT) AS ExpenseShareGroupId,
	       CAST(0 AS DECIMAL(18, 2)) AS ExpenseSharePercentage,
	       CAST(0 AS DECIMAL(18, 2)) AS ExpenseShareAmount,
	       CAST(0 AS DECIMAL(18, 2)) AS WriteOffAmount,
	       CAST(0 AS INT) AS PayPeriodId,
	       CAST(0 AS DECIMAL(18, 2)) AS FixedExpenseAmount,
	       CAST(@CarriedFwdExpense AS DECIMAL(18, 2)) AS 
	       PrevCarriedForwardExpense,
	       CAST(0 AS DECIMAL(18, 2)) AS Negative_Net
	FROM   #Temp_LiqDetails
	GROUP BY
	       [Site_Name],
	       [Advance_To_Retailer]
END
GO

