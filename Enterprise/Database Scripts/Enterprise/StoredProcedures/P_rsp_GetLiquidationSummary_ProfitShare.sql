USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_GetLiquidationSummary_ProfitShare]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_GetLiquidationSummary_ProfitShare]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[rsp_GetLiquidationSummary_ProfitShare] 
	@Batch_No INT = 0,
	@Site_Id INT
AS
	SET DATEFORMAT dmy               
	SET NOCOUNT ON              
	
	DECLARE @RetailNegativeNet       AS DECIMAL(18, 2),
	        @SETTINGVALUE            AS DECIMAL(18, 2),
	        @CarriedFwdExpense       AS DECIMAL(18, 2)
	
	DECLARE @CollectionBatchAdvance  DECIMAL(18, 2)
	DECLARE @Handpay                 DECIMAL(18, 2)              
	DECLARE @JACKPOT                 DECIMAL(18, 2) 
	
	IF @Batch_No = 0
	    RETURN (0) -- shouldn't happen              
	
	SET @SETTINGVALUE = 0.00    
	
	SET @RetailNegativeNet = 0    
	
	SELECT TOP 1 @RetailNegativeNet = CASE WHEN ISNULL(Negative_Net, 0) < 0 THEN Negative_Net ELSE 0 END,
	       @CarriedFwdExpense = CASE 
	                                 WHEN ((ISNULL(CarriedForwardExpense, 0) <= 0) OR (ISNULL(RetailerShareBeforeFixedExpense, 0) >= 0)) THEN 
	                                      0
	                                 ELSE CarriedForwardExpense
	                            END
	FROM   LiquidationDetails
	WHERE  CollectionBatchId IS NOT NULL
		AND SiteId = @Site_Id
	ORDER BY
	       LiquidationId DESC
	
	SELECT @Handpay = SUM(Treasury_Amount)
	FROM   Treasury_Entry T
	       JOIN VW_Collectiondata VWCD
	            ON  VWCD.Collection_Id = T.Collection_Id
	WHERE  T.TREASURY_TYPE IN ('Handpay Credit', 'Attendantpay Credit') -- included the type Attendantpay Credit to calculate Advance to Retailer value
	       AND VWCD.Batch_Id = @Batch_No
	       AND ISNULL(Treasury_Reason_Code, 0) = 0
	       AND Treasury_Amount > 5000 
	
	--    -- get value of jackpot      
	SELECT @JACKPOT = SUM(Treasury_Amount)
	FROM   Treasury_Entry T
	       JOIN VW_Collectiondata VWCD
	            ON  VWCD.Collection_Id = T.Collection_Id
	WHERE  T.TREASURY_TYPE IN ('Handpay Jackpot', 'Attendantpay Jackpot', 
	                          'Mystery Jackpot', 'PROGRESSIVE', 'PROG') -- -- included the type Attendantpay Jackpot to calculate Advance to Retailer value
	       AND VWCD.Batch_Id = @Batch_No
	       AND ISNULL(Treasury_Reason_Code, 0) = 0
	       AND Treasury_Amount > 5000 
	
	SET @CollectionBatchAdvance = ISNULL(@Handpay, 0) + ISNULL(@JACKPOT, 0)
	
	
	;WITH Liquidation_CTE AS(
	SELECT VWCD.Batch_Id AS BatchNo,
	       CAST(
	           (
	               MAX(VWCD.[Collection_Date_Of_Collection] + ' ' + C.Collection_Time)
	           ) AS DATETIME
	       ) AS Liquidation_Date,	--Batch Date  
	       
	       S.[Site_Name] AS Retailer_Name,	--Retailer Name              
	       CAST(
	           (SUM(VWCD.[Declared_Tickets]) + SUM(VWCD.[Declared_Notes]) + SUM(VWCD.[Declared_Coins_In])) AS DECIMAL(18, 2)
	       ) AS Gross,	--1 Gross (Meters In)              
	       CAST(SUM(VWCD.[Cash_Out]) + SUM(Coins_Out) AS DECIMAL(18, 2)) AS Tickets_Expected,	--2 Tickets Expected (Meters Out)              
	       CAST(
	           (SUM(VWCD.[Declared_Tickets]) + SUM(VWCD.[Declared_Notes]) + SUM(VWCD.[Declared_Coins_In])) -(SUM(VWCD.[Cash_Out]) + SUM(Coins_Out)) 
	           AS DECIMAL(18, 2)
	       ) AS Net,	--3 Net       
	       
	       CAST(
	           (CAST(
					(SUM(VWCD.[Declared_Tickets]) + SUM(VWCD.[Declared_Notes]) + SUM(VWCD.[Declared_Coins_In])) -(SUM(VWCD.[Cash_Out]) + SUM(Coins_Out)) 
					AS DECIMAL(18, 2))
	        * @SETTINGVALUE) AS DECIMAL(18, 2)
	       ) AS Net_Percentage	--4 Net x Percentage              
	FROM   VW_Collectiondata VWCD
	       JOIN COLLECTION C
	            ON  C.Collection_ID = VWCD.Collection_ID
	       JOIN Batch CB
	            ON  CB.Batch_Id = VWCD.Batch_Id
	       JOIN [Site] s
	            ON  VWCD.Site_ID = S.Site_ID
	WHERE  CB.Batch_Id = @Batch_No
	GROUP BY
	       VWCD.[batch_Name],
	       VWCD.Batch_Id,
	       s.Site_ID,
	       s.Site_Name
	)
	
	SELECT 
		BatchNo,
		Liquidation_Date,
		Retailer_Name,
		Gross,
		Tickets_Expected,
		Net,
		Net_Percentage,
		CAST(@SETTINGVALUE AS DECIMAL(18, 2)) AS Percentage_Setting,
		CAST(@RetailNegativeNet AS DECIMAL(18, 2)) AS Retailer_Negative_Net,	--5 Retailers Negative Net               
	       CAST(
	           (
	               Net_Percentage + @RetailNegativeNet
	           ) AS DECIMAL(18, 2)
	       ) AS Retailer_Share,	--6 Retailers Share(4+5)              
	       CAST(Tickets_Expected AS DECIMAL(18, 2)) AS Tickets_Paid,	--7 Tickets Paid(2)              
	       CAST(COALESCE(@CollectionBatchAdvance, 0) AS DECIMAL(18, 2)) AS Advance_To_Retailer,	--8 Advance to Retailer               
	       CAST(
	           CASE 
	                WHEN (
	                         (Net_Percentage) 
	                         + @RetailNegativeNet
	                     ) > 0 THEN (
	                         (Net_Percentage) 
	                         + @RetailNegativeNet
	                     ) + (Tickets_Expected -COALESCE(@CollectionBatchAdvance, 0))
	                ELSE (Tickets_Expected -COALESCE(@CollectionBatchAdvance, 0))
	           END AS DECIMAL(18, 2)
	       ) AS Retailer,	--9 --if Retailer's Share>0 then Retailer=(6+7-8) else <0 then Retailer=(7-8)              
	       CAST(
	           CASE 
	                WHEN (
	                         Net_Percentage
	                         + @RetailNegativeNet
	                     ) > 0 THEN --if Retailer's Share>0 =(3-4)+(8-5)-Exp Share Amt of Operator else <0 =(3+8)              
	                     (
	                         Net - Net_Percentage
	                     ) + (COALESCE(@CollectionBatchAdvance, 0) -@RetailNegativeNet)
	                ELSE (
	                         Net
	                         +
	                         COALESCE(@CollectionBatchAdvance, 0)
	                     )
	           END AS DECIMAL(18, 2)
	       ) AS Balance_Due,
	       CAST(
	           CASE 
	                WHEN (
	                         Net_Percentage + @RetailNegativeNet
	                     ) > 0 THEN (
	                         Net_Percentage + @RetailNegativeNet
	                     ) -(
	                         Net - Net_Percentage
	                     ) + (COALESCE(@CollectionBatchAdvance, 0) - @RetailNegativeNet)
	                ELSE 0 -(
	                         Net 
	                         +
	                         COALESCE(@CollectionBatchAdvance, 0)
	                     )
	           END AS DECIMAL(18, 2)) RetailerShareBeforeFixedExpense,
	       CAST(0 AS INT) AS ProfitShareGroupId,
	       CAST(0 AS INT) AS ExpenseShareGroupId,
	       CAST(0 AS DECIMAL(18, 2)) AS ExpenseSharePercentage,
	       CAST(0 AS DECIMAL(18, 2)) AS ExpenseShareAmount,
	       CAST(0 AS DECIMAL(18, 2)) AS WriteOffAmount,
	       CAST(0 AS INT) AS PayPeriodId,
	       CAST(0 AS DECIMAL(18, 2)) AS FixedExpenseAmount,
	       CAST(0 AS DECIMAL(18, 2)) AS CarriesForwardExpense,
	       CAST(0 AS DECIMAL(18, 2)) AS Negative_Net,
	       CAST(0 AS DECIMAL(18, 2)) AS RetailerExpenseShareAmount,
	       CAST(0 AS DECIMAL(18, 2)) AS RetailerNetRevenue,
	       CAST(0 AS DECIMAL(18, 2)) AS ExpenseShareAmountOfOperator,
	       CAST(@CarriedFwdExpense AS DECIMAL(18, 2)) AS 
	       PrevCarriedForwardExpense
	FROM Liquidation_CTE
	
GO