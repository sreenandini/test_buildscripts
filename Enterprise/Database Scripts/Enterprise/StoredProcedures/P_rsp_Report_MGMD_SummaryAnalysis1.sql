USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_Report_MGMD_SummaryAnalysis1]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_Report_MGMD_SummaryAnalysis1]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--                      
-- Outputs:                               
-- rsp_Report_MGMD_SummaryAnalysis1 0,0,0,0, '2011-11-16 00:00:00.000'  ,  'MTD,DAY,YTD',1
--rsp_Report_MGMD_SummaryAnalysis1 2, 0, 2, 0, '06/15/12 6:31:04 AM','DAY'                         
-- =======================================================================                      
--                       
-- Revision History                      
--                      
-- Anuradha             09 Jan 2012  Created           
--
------------------------------------------------------------------------------------------------------                      
CREATE PROCEDURE rsp_Report_MGMD_SummaryAnalysis1
	@Company INT = 0,
	@SubCompany INT = 0,
	@Region INT = 0,
	@Area INT = 0,
	@District INT = 0,
	@Site INT = 0,
	@Zone INT = 0,
	@GamingDate DATETIME,
	@Period NVARCHAR(50) = 'DAY,LTD,MTD',
	@GroupByZone BIT,
	@SiteIDList VARCHAR(MAX)
AS
	SET NOCOUNT ON                
	
	SET DATEFORMAT dmy              
	
	DECLARE @isAFTCalculationEnabled BIT                  
	SELECT @isAFTCalculationEnabled = Setting_Value
	FROM   Setting
	WHERE  Setting_Name = 'IsAFTIncludedInCalculation'                      
	
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
	
	IF @Zone = 0
	    SET @Zone = NULL
	
	
	
	-- create quarter jan-mar, apr-jun, jul-sep,oct-dec               
	DECLARE @qstartmonth  INT,
	        @qtdstart     DATETIME,
	        @mtdstart     DATETIME,
	        @ytdstart     DATETIME,
	        @wtdstart     DATETIME                
	
	SET @qstartmonth = CASE 
	                        WHEN MONTH(@gamingdate) BETWEEN 1 AND 3 THEN 1
	                        WHEN MONTH(@gamingdate) BETWEEN 4 AND 6 THEN 4
	                        WHEN MONTH(@gamingdate) BETWEEN 7 AND 9 THEN 7
	                        ELSE 10
	                   END                
	
	SET @qtdstart = '01/' + CAST(@qstartmonth AS VARCHAR(4)) + '/' + CAST(YEAR(@gamingdate) AS VARCHAR(4)) 
	-- create YTD                
	SET @ytdstart = '01 jan ' + CAST(YEAR(@gamingdate) AS VARCHAR(4)) 
	-- create mtd                
	SET @mtdstart = CAST(
	        '01/' + CAST(DATEPART(MONTH, @gamingdate) AS VARCHAR(3)) + '/' +
	        CAST(YEAR(@gamingdate) AS VARCHAR(4)) AS DATETIME
	    ) 
	-- create ptd ??                
	
	-- create wtd,                  
	SET DATEFIRST 1 -- use monday as week start                
	SET @wtdstart = DATEADD(
	        DD,
	        1 - DATEPART(DW, CONVERT(VARCHAR(10), @gamingdate, 103)),
	        @gamingdate
	    )                
	
	
	CREATE TABLE #Periods
	(
		[name]      VARCHAR(3),
		[ordering]  INT,
		[start]     DATETIME,
		[end]       DATETIME
	)                
	
	
	INSERT INTO #Periods
	  (
	    [name]
	  )
	SELECT DATA
	FROM   dbo.fnSplit(@Period, ',') 
	
	-- create our list of periods, complete with start and end dates                
	UPDATE #Periods
	SET    ordering = 1,
	       [start] = CONVERT(VARCHAR(15), @gamingdate, 103),
	       [end] = CONVERT(VARCHAR(15), @gamingdate, 103)
	WHERE  [name] = 'DAY' 
	
	UPDATE #Periods
	SET    ordering = 2,
	       [start] = @mtdstart,
	       [end] = @gamingdate
	WHERE  [name] = 'MTD'
	
	UPDATE #Periods
	SET    ordering = 3,
	       [start] = @qtdstart,
	       [end] = @gamingdate
	WHERE  [name] = 'QTD'                
	
	
	UPDATE #Periods
	SET    ordering = 4,
	       [start] = @ytdstart,
	       [end] = @gamingdate
	WHERE  [name] = 'YTD' 
	
	
	/*  
	***********************************************************  
	*/  
	
	SELECT [Order] = Pd.ordering,
	       [Period] = Pd.[Name],
	       Company_ID = SC.Company_ID,
	       Site_Name = S.Site_Name,
	       Zonename = z.zone_name,
	       GameCategory = gc.Game_Category_Name,
	       GameName = CASE 
	                       WHEN ISNULL(tGT.Game_Title, '') <> '' THEN tGT.Game_Title
	                       ELSE '[N/a]'
	                  END,
	       m.Machine_Stock_No,
	       [TotalBet] = SUM(
	           CAST(
	               tMS.MGMD_COINS_IN * tI.Installation_Price_Per_Play AS FLOAT
	           )
	       ) / 100,
	       [TotalWins] = (
	           SUM(
	               CAST(
	                   tMS.MGMD_COINS_OUT * tI.Installation_Price_Per_Play AS 
	                   FLOAT
	               )
	           ) / 100
	       ),
	       AverageBet = CASE 
	                         WHEN SUM(tMS.MGMD_GAMES_BET) > 0 THEN (
	                                  SUM(
	                                      CAST(
	                                          tMS.MGMD_COINS_IN * tI.Installation_Price_Per_Play 
	                                          AS FLOAT
	                                      )
	                                  ) / 100
	                              )
	                         ELSE 0
	                    END,
	       [NetWin] = (
	           (
	               SUM(
	                   CAST(
	                       tMS.MGMD_COINS_IN * tI.Installation_Price_Per_Play AS 
	                       FLOAT
	                   )
	               ) / 100
	           ) -(
	               SUM(
	                   CAST(
	                       tMS.MGMD_COINS_OUT * tI.Installation_Price_Per_Play 
	                       AS 
	                       FLOAT
	                   )
	               ) / 100
	           ) -(
	               SUM(
	                   CAST(tMS.MGMD_JACKPOT * tI.Installation_Price_Per_Play AS FLOAT)
	               ) / 100
	           )
	       ),
	       [TheoWin] = AVG(
	           CAST(
	               (100 - tI.Installation_Percentage_Payout) AS DECIMAL(10, 2)
	           )
	       ),
	       [Jackpot] = SUM(
	           CAST(
	               (
	                   (tMS.MGMD_JACKPOT * tI.Installation_Price_Per_Play) / 100.0
	               ) AS FLOAT
	           )
	       ),
	       [AverageWin] = (
	           (
	               SUM(
	                   CAST(
	                       tMS.MGMD_COINS_IN * tI.Installation_Price_Per_Play AS 
	                       FLOAT
	                   )
	               ) / 100
	           ) -(
	               SUM(
	                   CAST(
	                       tMS.MGMD_COINS_OUT * tI.Installation_Price_Per_Play 
	                       AS 
	                       FLOAT
	                   )
	               ) / 100
	           ) -(
	               SUM(
	                   CAST(tMS.MGMD_JACKPOT * tI.Installation_Price_Per_Play AS FLOAT)
	               ) / 100
	           )
	       ),
	       [Variance] = (
	           (
	               SUM(
	                   CAST(
	                       tMS.MGMD_COINS_IN * tI.Installation_Price_Per_Play AS 
	                       FLOAT
	                   )
	               ) / 100
	           ) -(
	               SUM(
	                   CAST(
	                       tMS.MGMD_COINS_OUT * tI.Installation_Price_Per_Play 
	                       AS 
	                       FLOAT
	                   )
	               ) / 100
	           ) -(
	               SUM(
	                   CAST(tMS.MGMD_JACKPOT * tI.Installation_Price_Per_Play AS FLOAT)
	               ) / 100
	           )
	       )
	       INTO #PreGrouping
	FROM   #Periods Pd,
	       MGMD_SessionDelta tMS
	       JOIN MGMD_Installation mi
	            ON  mi.MGMD_ID = tMS.MGMD_Combination_ID
	       JOIN Installation tI
	            ON  tI.Installation_ID = mi.MGMD_Installation_ID
	       JOIN Bar_Position bp
	            ON  bp.Bar_Position_ID = tI.Bar_Position_ID
	       JOIN [Machine] m
	            ON  m.Machine_ID = tI.Machine_ID
	       JOIN Game_Library gl
	            ON  gl.MG_Game_ID = mi.MGMD_Game_ID
	       JOIN Game_Title tGT
	            ON  tGT.Game_Title_ID = gl.MG_Group_ID
	       JOIN Game_Category gc
	            ON  gc.Game_Category_ID = tGT.Game_Category_ID
	       JOIN [Site] S
	            ON  S.Site_ID = bp.Site_ID
	       JOIN Sub_Company SC
	            ON  SC.Sub_Company_ID = S.Sub_Company_ID
	       INNER JOIN Company C
	            ON  C.Company_ID = SC.Company_ID
	       LEFT JOIN Zone Z
	            ON  Z.Zone_ID = bp.Zone_ID
	WHERE  CONVERT(VARCHAR(15), tMS.MGMD_Start_DateTime, 103) >= Pd.[start]
	       AND CONVERT(VARCHAR(15), tMS.MGMD_End_DateTime, 103) <= Pd.[End]
	       AND ISNULL(@Site, S.Site_id) = S.Site_ID
	       AND ISNULL(@Company, C.Company_Id) = C.Company_Id
	       AND ISNULL(@SubCompany, S.Sub_Company_Id) = S.Sub_Company_Id
	       AND ISNULL(@Region, S.Sub_Company_Region_ID) = S.Sub_Company_Region_ID
	       AND ISNULL(@Area, S.Sub_Company_Area_ID) = S.Sub_Company_Area_ID
	       AND ISNULL(@District, S.Sub_Company_District_ID) = S.Sub_Company_District_ID
	       AND (
	               @SiteIDList IS NOT NULL
	               AND S.Site_ID IN (SELECT DATA
	                                 FROM   fnSplit(@SiteIDList, ','))
	           )
	       AND ISNULL(@Zone, Z.Zone_id) = Z.Zone_ID
	GROUP BY
	       SC.Company_ID,
	       SC.Sub_Company_ID,
	       S.Site_Name,
	       z.zone_name,
	       tGT.Game_Title,
	       gc.Game_Category_Name,
	       m.Machine_Stock_No,
	       Pd.[name],
	       Pd.ordering,
	       Pd.[start],
	       Pd.[end]
	
	SELECT G.[Order],
	       G.[Period],
	       G.Company_ID,
	       G.Site_Name,
	       G.Zonename,
	       G.GameName,
	       G.GameCategory,
	       G.Machine_Stock_No,
	       G.TotalBet AS TotalBet,
	       G.TotalWins AS TotalWins,
	       ROUND(G.AverageBet / COALESCE(COUNT(G.Machine_Stock_No), 1), 2) AS 
	       AverageBet,
	       G.NetWin AS Netwin,
	       G.TheoWin AS TheoWin,
	       ROUND(G.NetWin / COUNT(G.Machine_Stock_No), 2) AS AverageWin,
	       CASE --(Net Win/Total Bet) * 100
	            WHEN ROUND(G.TotalBet, 2) > 0 THEN (ROUND(G.NetWin, 2) / ROUND(G.TotalBet, 2)) 
	                 * 100
	            ELSE 0
	       END AS ActualHoldPerc,
	       CASE --(Bets * hold% * 100)/(#Slot)
	            WHEN ROUND(G.TotalBet, 2) > 0 THEN (
	                     (ROUND(G.TotalBet, 2) * G.TheoWin) / 100 / COUNT(G.Machine_Stock_No)
	                 )
	            ELSE 0
	       END AS TheoHoldPerc,
	       (
	           G.variance -(
	               CASE 
	                    WHEN ROUND(G.TotalBet, 2) > 0 THEN (
	                             (ROUND(G.TotalBet, 2) * G.TheoWin) / 100 /
	                             COUNT(G.Machine_Stock_No)
	                         )
	                    ELSE 0
	               END
	           )
	       ) AS Variance,
	       g.Jackpot AS Jackpot
	       INTO #calculatedResult
	FROM   #preGrouping G
	WHERE  G.TotalBet > 0
	GROUP BY
	       G.[Order],
	       G.[Period],
	       G.Company_ID,
	       G.Site_Name,
	       G.zonename,
	       G.GameName,
	       G.GameCategory,
	       G.Machine_Stock_No,
	       G.NetWin,
	       G.TotalWins,
	       G.TheoWin,
	       G.variance,
	       G.TotalBet,
	       G.AverageBet,
	       G.Jackpot
	
	
	SELECT 1 AS SortOrder,
	       G.[order],
	       G.Period,
	       G.Site_Name,
	       '' AS Asset,
	       0 AS Denom,
	       ISNULL(G.zonename, 'NOT SET') AS Zonename,
	       '' AS GameType,
	       G.GameName,
	       '' AS GameGroup,
	       G.GameCategory,
	       Machine_Stock_No,
	       COUNT(Machine_Stock_No) SlotCount,
	       '' AS ManufacturerName,
	       SUM(G.TotalBet) AS TotalBet,
	       SUM(G.TotalWins) AS TotalWins,
	       AVG(G.AverageBet) AS AverageBet,
	       SUM(G.Netwin) AS NetWin,
	       SUM(G.TheoWin) AS TheoWin,
	       AVG(G.AverageWin) AS AverageWin,
	       SUM(ActualHoldPerc) AS ActualHoldPerc,
	       SUM(TheoHoldPerc) TheoHoldPerc,
	       SUM(Variance) AS Variance,
	       SUM(Jackpot) AS Jackpot
	       
	       INTO #ResultSet
	FROM   #calculatedResult G
	GROUP BY
	       G.[Order],
	       G.[Period],
	       G.Company_ID,
	       G.Site_Name,
	       G.Zonename,
	       Machine_Stock_No,
	       G.GameCategory,
	       G.GameName 
	
	
	--Group by GameName(2)  
	INSERT INTO #ResultSet
	  (
	    SortOrder,
	    [Order],
	    [Period],
	    Site_Name,
	    Asset,
	    Denom,
	    ZoneName,
	    GameType,
	    GameName,
	    GameGroup,
	    GameCategory,
	    SlotCount,
	    ManufacturerName,
	    TotalBet,
	    TotalWins,
	    AverageBet,
	    NetWin,
	    TheoWin,
	    AverageWin,
	    ActualHoldPerc,
	    TheoHoldPerc,
	    variance,
	    Jackpot
	  )
	SELECT 2 AS SortOrder,
	       G.[order],
	       G.Period,
	       G.Site_Name,
	       '',
	       0,
	       ISNULL(G.zonename, 'NOT SET') AS Zonename,
	       '',
	       G.GameName,
	       '',
	       G.GameCategory,
	       COUNT(DISTINCT Machine_Stock_No) AS SlotCount,
	       '',
	       SUM(G.TotalBet) AS TotalBet,
	       SUM(G.TotalWins) AS TotalWins,
	       AVG(G.AverageBet) AS AverageBet,
	       SUM(G.Netwin) AS NetWin,
	       SUM(G.TheoWin) AS TheoWin,
	       AVG(G.AverageWin) AS AverageWin,
	       SUM(ActualHoldPerc) AS ActualHoldPerc,
	       SUM(TheoHoldPerc) TheoHoldPerc,
	       SUM(Variance) AS Variance,
	       SUM(Jackpot) AS Jackpot
	FROM   #calculatedResult G
	GROUP BY
	       G.[Order],
	       G.[Period],
	       G.Company_ID,
	       G.Site_Name,
	       G.Zonename,
	       G.GameCategory,
	       G.GameName
	
	
	--Group by GameCategory(3)                
	INSERT INTO #ResultSet
	  (
	    SortOrder,
	    [Order],
	    [Period],
	    Site_Name,
	    Asset,
	    Denom,
	    ZoneName,
	    GameType,
	    GameName,
	    GameGroup,
	    GameCategory,
	    SlotCount,
	    ManufacturerName,
	    TotalBet,
	    TotalWins,
	    AverageBet,
	    NetWin,
	    TheoWin,
	    AverageWin,
	    ActualHoldPerc,
	    TheoHoldPerc,
	    variance,
	    Jackpot
	  )
	SELECT 3 AS SortOrder,
	       G.[order],
	       G.Period,
	       G.Site_Name,
	       '',
	       0,
	       ISNULL(G.zonename, 'NOT SET') AS Zonename,
	       '',
	       '' AS GameName,
	       '',
	       G.GameCategory,
	       COUNT(DISTINCT Machine_Stock_No) SlotCount,
	       '',
	       SUM(G.TotalBet) AS TotalBet,
	       SUM(G.TotalWins) AS TotalWins,
	       AVG(G.AverageBet) AS AverageBet,
	       SUM(G.Netwin) AS NetWin,
	       SUM(G.TheoWin) AS TheoWin,
	       AVG(G.AverageWin) AS AverageWin,
	       SUM(ActualHoldPerc) AS ActualHoldPerc,
	       SUM(TheoHoldPerc) TheoHoldPerc,
	       SUM(Variance) AS Variance,
	       SUM(Jackpot) AS Jackpot
	FROM   #calculatedResult G
	GROUP BY
	       G.[Order],
	       G.[Period],
	       G.Company_ID,
	       G.Site_Name,
	       G.Zonename,
	       G.GameCategory 
	
	
	--Group by ZoneName(4)    
	IF(@GroupByZone=1)
	BEGIN           
	INSERT INTO #ResultSet
	  (
	    SortOrder,
	    [Order],
	    [Period],
	    Site_Name,
	    Asset,
	    Denom,
	    ZoneName,
	    GameType,
	    GameName,
	    GameGroup,
	    GameCategory,
	    SlotCount,
	    ManufacturerName,
	    TotalBet,
	    TotalWins,
	    AverageBet,
	    NetWin,
	    TheoWin,
	    AverageWin,
	    ActualHoldPerc,
	    TheoHoldPerc,
	    variance,
	    Jackpot
	  )
	SELECT 4 AS SortOrder,
	       G.[order],
	       G.Period,
	       G.Site_Name,
	       '',
	       0,
	       ISNULL(G.zonename, 'NOT SET') AS Zonename,
	       '',
	       '' AS GameName,
	       '',
	       '' AS GameCategory,
	       COUNT(DISTINCT Machine_Stock_No) SlotCount,
	       '',
	       SUM(G.TotalBet) AS TotalBet,
	       SUM(G.TotalWins) AS TotalWins,
	       AVG(G.AverageBet) AS AverageBet,
	       SUM(G.Netwin) AS NetWin,
	       SUM(G.TheoWin) AS TheoWin,
	       AVG(G.AverageWin) AS AverageWin,
	       SUM(ActualHoldPerc) AS ActualHoldPerc,
	       SUM(TheoHoldPerc) TheoHoldPerc,
	       SUM(Variance) AS Variance,
	       SUM(Jackpot) AS Jackpot
	FROM   #calculatedResult G
	GROUP BY
	       G.[Order],
	       G.[Period],
	       G.Company_ID,
	       G.Site_Name,
	       G.Zonename 
	END
	
	--Group by SiteName(5)               
	INSERT INTO #ResultSet
	  (
	    SortOrder,
	    [Order],
	    [Period],
	    Site_Name,
	    Asset,
	    Denom,
	    ZoneName,
	    GameType,
	    GameName,
	    GameGroup,
	    GameCategory,
	    SlotCount,
	    ManufacturerName,
	    TotalBet,
	    TotalWins,
	    AverageBet,
	    NetWin,
	    TheoWin,
	    AverageWin,
	    ActualHoldPerc,
	    TheoHoldPerc,
	    variance,
	    Jackpot
	  )
	SELECT 5 AS SortOrder,
	       G.[order],
	       G.Period,
	       G.Site_Name,
	       '',
	       0,
	       '' AS Zonename,
	       '',
	       '' AS GameName,
	       '',
	       '' AS GameCategory,
	       COUNT(DISTINCT Machine_Stock_No) SlotCount,
	       '',
	       SUM(G.TotalBet) AS TotalBet,
	       SUM(G.TotalWins) AS TotalWins,
	       AVG(G.AverageBet) AS AverageBet,
	       SUM(G.Netwin) AS NetWin,
	       SUM(G.TheoWin) AS TheoWin,
	       AVG(G.AverageWin) AS AverageWin,
	       SUM(ActualHoldPerc) AS ActualHoldPerc,
	       SUM(TheoHoldPerc) TheoHoldPerc,
	       SUM(Variance) AS Variance,
	       SUM(Jackpot) AS Jackpot
	FROM   #calculatedResult G
	GROUP BY
	       G.[Order],
	       G.[Period],
	       G.Company_ID,
	       G.Site_Name
	
	
	-- GrandTotal(6)                
	INSERT INTO #ResultSet
	  (
	    SortOrder,
	    [Order],
	    [Period],
	    Site_Name,
	    Asset,
	    Denom,
	    ZoneName,
	    GameType,
	    GameName,
	    GameGroup,
	    GameCategory,
	    SlotCount,
	    ManufacturerName,
	    TotalBet,
	    TotalWins,
	    AverageBet,
	    NetWin,
	    TheoWin,
	    AverageWin,
	    ActualHoldPerc,
	    TheoHoldPerc,
	    variance,
	    Jackpot
	  )
	SELECT 6 AS SortOrder,
	       G.[order],
	       G.Period,
	       '' AS Site_Name,
	       '',
	       0,
	       '' AS Zonename,
	       '',
	       '' AS GameName,
	       '',
	       '' AS GameCategory,
	       COUNT(DISTINCT Machine_Stock_No) SlotCount,
	       '',
	       SUM(G.TotalBet) AS TotalBet,
	       SUM(G.TotalWins) AS TotalWins,
	       AVG(G.AverageBet) AS AverageBet,
	       SUM(G.Netwin) AS NetWin,
	       SUM(G.TheoWin) AS TheoWin,
	       AVG(G.AverageWin) AS AverageWin,
	       SUM(ActualHoldPerc) AS ActualHoldPerc,
	       SUM(TheoHoldPerc) TheoHoldPerc,
	       SUM(Variance) AS Variance,
	       SUM(Jackpot) AS Jackpot
	FROM   #calculatedResult G
	GROUP BY
	       G.[Order],
	       G.[Period],
	       G.Company_ID              
	
	
	SELECT SortOrder,
	       Site_Name AS SiteName,
	       Asset,
	       Denom,
	       Zonename,
	       GameType,
	       GameName,
	       GameGroup,
	       GameCategory,
	       CAST(SlotCount AS VARCHAR(50)) AS SlotCount,
	       ManufacturerName,
	       TotalBet,
	       TotalWins,
	       AverageBet,
	       NetWin,
	       TheoWin,
	       AverageWin,
	       ActualHoldPerc,
	       TheoHoldPerc,
	       Variance,
	       Jackpot,
	       [order],
	       Period
	FROM   #resultset
	WHERE  SortOrder <> 1
	ORDER BY
	       sortorder 
	
	DROP TABLE #Periods
GO

