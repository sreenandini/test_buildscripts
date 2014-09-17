

USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_Report_MGMD_DenomPerformance]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_Report_MGMD_DenomPerformance]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

------------------------------------------------------------------------------------------------------
--------------------------------------------------------------------------   
--  
-- Description: fetch the denom data 
-- Inputs:      See inputs   
--  
-- Outputs:           
-- --rsp_Report_MGMD_DenomPerformance '','',0,'2009-07-13 12:44:02.340', '2011-07-13 13:15:30.810'                   
-- =======================================================================  
--   
-- Revision History  
--  
-- Renjish		        16/07/2010  Created  
-- Yoganandh P	        25/08/2010	Modified - Net Win Formula
-- Jisha Lenu George    08/10/2010  Modified the Winloss calculation
-- Jisha Lenu George	07/01/2011	Added MGMD_COINS_IN & MGMD_COINS_OUT in NetWin,AverageWin
-- Yoganandh P			22/01/2011	Updated Net Win, Average Win Formula
-- Anil					23/02/2011	Changed Net WIN = (Bet - Win - Jackpot)  
-- Anil					01/03/2011	Changed Average Win   =  (Net Win / Paytable Count), Average Net % =  (Net Win / Total Bet) * 100
-- Anil					11/03/2011	Changed Net WIN = (Bet - Win - Jackpot)
------------------------------------------------------------------------------------------------------  
CREATE PROCEDURE rsp_Report_MGMD_DenomPerformance
	@Company INT = 0,
	@SubCompany INT = 0,
	@Region INT = 0,
	@Area INT = 0,
	@District INT = 0,
	@Site INT = 0,
	@StartDate DATETIME,
	@EndDate DATETIME,
	@SiteIDList VARCHAR(MAX)
AS
	IF @Company = 0
	    SET @Company = NULL
	
	IF @SubCompany = 0
	    SET @SubCompany = NULL
	
	IF @Region = 0
	    SET @Region = NULL
	
	IF @Area = 0
	    SET @Area = NULL
	
	IF @District = 0
	    SET @District = NULL
	
	IF @Site = 0
	    SET @Site = NULL 
	
	
	DECLARE @iDaysOnline INT  
	SELECT @iDaysOnline = DATEDIFF(DAY, @StartDate, @EndDate) + 1  
	
	DECLARE @isAFTCalculationEnabled BIT
	SELECT @isAFTCalculationEnabled = Setting_Value
	FROM   Setting
	WHERE  Setting_Name = 'IsAFTIncludedInCalculation'
	
	DECLARE @DenomPerf  TABLE
	        (
	            Denom FLOAT,
	            TotalBet FLOAT,
	            AverageBet FLOAT,
	            NetWin FLOAT,
	            AverageWin FLOAT
	        )
	
	
	DECLARE @PayTable   TABLE 
	        (Denom FLOAT, PaytableID INT)  
	
	DECLARE @Denom      TABLE 
	        (Denom FLOAT, PaytableCount INT);
	
	WITH PaytableCTE(Denom, PaytableId) 
	AS 
	(
	    SELECT CAST(MI.MGMD_Denom_Value AS FLOAT) / 100,
	           P.Paytable_ID
	    FROM   MGMD_Installation MI WITH(NOLOCK)
	           INNER JOIN Paytable P WITH(NOLOCK)
	                ON  MI.MGMD_Paytable_ID = P.Paytable_ID
	           INNER JOIN MGMD_SessionDelta MS WITH(NOLOCK)
	                ON  MI.MGMD_ID = MS.MGMD_Combination_ID
	           INNER JOIN Installation I WITH(NOLOCK)
	                ON  MI.MGMD_Installation_ID = I.Installation_ID
	           INNER JOIN Bar_Position WITH (NOLOCK)
	                ON  Bar_Position.Bar_Position_ID = I.Bar_Position_ID
	           INNER JOIN [Site] S
	                ON  Bar_Position.Site_ID = S.Site_ID
	           INNER JOIN Sub_Company SC
	                ON  S.Sub_Company_ID = SC.Sub_Company_ID
	           INNER JOIN Company C
	                ON  SC.Company_ID = C.Company_ID
	    WHERE  (
	               MS.MGMD_Start_DateTime >= @StartDate
	               AND MS.MGMD_End_DateTime <= @EndDate
	           )
	           AND (
	                   ISNULL(@Site, S.Site_id) = S.Site_ID
	                   AND (
	                           @SiteIDList IS NOT NULL
	                           AND S.Site_ID IN (SELECT DATA
	                                             FROM   fnSplit(@SiteIDList, ','))
	                       )
	                   AND ISNULL(@Company, C.Company_Id) = C.Company_Id
	                   AND ISNULL(@SubCompany, S.Sub_Company_Id) = S.Sub_Company_Id
	                   AND ISNULL(@Region, S.Sub_Company_Region_ID) = S.Sub_Company_Region_ID
	                   AND ISNULL(@Area, S.Sub_Company_Area_ID) = S.Sub_Company_Area_ID
	                   AND ISNULL(@District, S.Sub_Company_District_ID) = S.Sub_Company_District_ID
	               )
	    GROUP BY
	           Paytable_ID,
	           MI.MGMD_Denom_Value,
	           CONVERT(VARCHAR(20), MGMD_Start_DateTime, 103)
	)
	INSERT INTO @PayTable
	SELECT Denom,
	       PaytableID
	FROM   PaytableCTE
	GROUP BY
	       Denom,
	       PaytableID
	
	INSERT INTO @Denom
	SELECT Denom,
	       COUNT('')
	FROM   @PayTable
	GROUP BY
	       Denom
	
	INSERT INTO @DenomPerf
	SELECT CAST(MI.MGMD_Denom_Value AS FLOAT) / 100 AS Denom,
	       SUM(
	           CAST(MS.MGMD_COINS_IN * I.Installation_Price_Per_Play AS FLOAT)
	       ) / 100 AS TotalBet,
	       CASE 
	            WHEN SUM(MS.MGMD_GAMES_BET) > 0 THEN (
	                     SUM(
	                         CAST(MS.MGMD_COINS_IN * I.Installation_Price_Per_Play AS FLOAT)
	                     ) / 100
	                 )
	            ELSE 0
	       END AS AverageBet,
	       (
	           (
	               SUM(
	                   CAST(MS.MGMD_COINS_IN * I.Installation_Price_Per_Play AS FLOAT)
	               ) / 100
	           ) -(
	               SUM(
	                   CAST(MS.MGMD_COINS_OUT * I.Installation_Price_Per_Play AS FLOAT)
	               ) / 100
	           ) -
	           SUM(
	               CAST(
	                   (MS.MGMD_JACKPOT * I.Installation_Price_Per_Play) / 100.0 
	                   AS FLOAT
	               )
	           )
	       ) AS NetWin,
	       CASE 
	            WHEN ISNULL(@iDaysOnline, 0) > 0 THEN (
	                     (
	                         SUM(
	                             CAST(MS.MGMD_COINS_IN * I.Installation_Price_Per_Play AS FLOAT)
	                         ) / 100
	                     ) -(
	                         SUM(
	                             CAST(MS.MGMD_COINS_OUT * I.Installation_Price_Per_Play AS FLOAT)
	                         ) / 100
	                     ) -
	                     SUM(
	                         CAST(
	                             (MS.MGMD_JACKPOT * I.Installation_Price_Per_Play)
	                             / 100.0 AS FLOAT
	                         )
	                     )
	                 ) / @iDaysOnline
	            ELSE 0
	       END AS AverageWin -- Net Win/ No of Day Online (Not in a use now)
	FROM   MGMD_Installation MI
	       INNER JOIN Paytable P
	            ON  MI.MGMD_Paytable_ID = P.Paytable_ID
	       INNER JOIN MGMD_SessionDelta MS
	            ON  MI.MGMD_ID = MS.MGMD_Combination_ID
	       INNER JOIN Installation I
	            ON  MI.MGMD_Installation_ID = I.Installation_ID
	       INNER JOIN Bar_Position BP
	            ON  BP.Bar_Position_ID = I.Bar_Position_ID
	       INNER JOIN [Site] S
	            ON  BP.Site_ID = S.Site_ID
	       INNER JOIN Sub_Company SC
	            ON  S.Sub_Company_ID = SC.Sub_Company_ID
	       INNER JOIN Company C
	            ON  SC.Company_ID = C.Company_ID
	WHERE  (
	           MS.MGMD_Start_DateTime >= @StartDate
	           AND MS.MGMD_End_DateTime <= @EndDate
	       )
	       AND (
	               ISNULL(@Site, S.Site_id) = S.Site_ID
	               AND (
	                       @SiteIDList IS NOT NULL
	                       AND S.Site_ID IN (SELECT DATA
	                                         FROM   fnSplit(@SiteIDList, ','))
	                   )
	               AND ISNULL(@Company, C.Company_Id) = C.Company_Id
	               AND ISNULL(@SubCompany, S.Sub_Company_Id) = S.Sub_Company_Id
	               AND ISNULL(@Region, S.Sub_Company_Region_ID) = S.Sub_Company_Region_ID
	               AND ISNULL(@Area, S.Sub_Company_Area_ID) = S.Sub_Company_Area_ID
	               AND ISNULL(@District, S.Sub_Company_District_ID) = S.Sub_Company_District_ID
	           )
	GROUP BY
	       MI.MGMD_Denom_Value
	
	SELECT D.Denom,
	       CAST(PaytableCount AS VARCHAR(50)) AS PaytableCount,
	       TotalBet,
	       ROUND(AverageBet / PaytableCount, 2) AS AverageBet,
	       NetWin,
	       ROUND(NetWin / PaytableCount, 2) AS AverageWin,
	       CASE 
	            WHEN ROUND(TotalBet, 2) > 0 THEN (ROUND(NetWin, 2) / ROUND(TotalBet, 2)) 
	                 * 100
	            ELSE 0
	       END AS AverageNetPerc --(Net Win/Total Bet) * 100
	FROM   @DenomPerf D
	       INNER JOIN @Denom S
	            ON  S.Denom = D.Denom
	WHERE  D.TotalBet > 0
GO

