
USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_Report_MGMD_GameReport]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_Report_MGMD_GameReport]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- Description: fetch the denom data           
-- Inputs:      See inputs             
--            
-- Outputs:                     
-- rsp_Report_MGMD_GameReport 2,0,0,  0,  '2011-12-19 12:44:02.340', '2011-12-30 13:15:30.810'                         
-- =======================================================================            
--             
-- Revision History            
--            
-- Anuradha             16/07/2010  Created  
-- Jisha Lenu George    08/10/2010  Modified the calculation for WinLoss 
-- Anil					08/10/2010  Modified the calculation for Denom 
-- Jisha Lenu George	07/01/2011	Added MGMD_COINS_IN & MGMD_COINS_OUT in NetWin,AverageWin
-- Yoganandh			19/01/2011	Used Left Join for Zone to display Total Bet correctly
-- Yoganandh P			22/01/2011	Updated Net Win, Average Win Formula
-- Anil					23/02/2011	Changed Net WIN = (Bet - Win - Jackpot) 
-- Anil					01/03/2011	Changed Average Win   =  (Net Win / Paytable Count), Average Net % =  (Net Win / Total Bet) * 100 
-- Anil					11/03/2011	Changed Net WIN = (Bet - Win - Jackpot)
------------------------------------------------------------------------------------------------------            
      
 -- Description: fetch the denom data
 -- Inputs:      See inputs
 --
 -- Outputs:
 -- rsp_Report_MGMD_GameReport 0,0,4,    '2010-07-13 12:44:02.340', '2012-07-21 13:15:30.810'
 -- rsp_Report_MGMD_GameReport1 0,0,4,'ZONE 2 ',    '2010-07-13 12:44:02.340', '2012-07-21 13:15:30.810'
 -- =======================================================================
 --
 -- Revision History
 --
 -- Anuradha             16/07/2010  Created
 -- Jisha Lenu George    08/10/2010  Modified the calculation for WinLoss
 -- Anil     08/10/2010  Modified the calculation for Denom
 -- Jisha Lenu George 07/01/2011 Added MGMD_COINS_IN & MGMD_COINS_OUT in NetWin,AverageWin
 -- Yoganandh   19/01/2011 Used Left Join for Zone to display Total Bet correctly
 -- Yoganandh P   22/01/2011 Updated Net Win, Average Win Formula
 -- Anil     23/02/2011 Changed Net WIN = (Bet - Win - Jackpot)
 -- Anil     01/03/2011 Changed Average Win   =  (Net Win / Paytable Count), Average Net % =  (Net Win / Total Bet) * 100
 -- Anil     11/03/2011 Changed Net WIN = (Bet - Win - Jackpot)
 ------------------------------------------------------------------------------------------------------                    
CREATE PROCEDURE rsp_Report_MGMD_GameReport
	@Company INT =0,
	@SubCompany INT=0,
	@Region INT=0,
	@Area INT=0,
	@District INT=0,
	@Site INT=0,
	@Zone INT=0,
	@StartDate DATETIME,
	@EndDate DATETIME,
	@SiteIDList VARCHAR(MAX)
AS
	DECLARE @iDaysOnline INT   
	
	SELECT @iDaysOnline = DATEDIFF(DAY, @StartDate, @EndDate) + 1  
	           
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
	
	
	DECLARE @isAFTCalculationEnabled BIT        
	SELECT @isAFTCalculationEnabled = Setting_Value
	FROM   Setting
	WHERE  Setting_Name = 'IsAFTIncludedInCalculation'               
	
	DECLARE @GamePerf  TABLE 
	        (
	            GameName VARCHAR(100),
	            Denom FLOAT,
	            Zone VARCHAR(50),
	            Manufacturer VARCHAR(100),
	            Slot VARCHAR(10),
	            Paytable_Description VARCHAR(500),
	            PaytableCount INT,
	            AverageBet FLOAT,
	            TotalBet FLOAT,
	            NetWin FLOAT,
	            AverageWin FLOAT
	        )             
	
	DECLARE @PayTable  TABLE 
	        (
	            GameName VARCHAR(100),
	            Paytable_Description VARCHAR(500),
	            PaytableId INT,
	            PaytableCount INT,
	            Denom FLOAT,
	            Zone VARCHAR(50),
	            Manufacturer VARCHAR(100),
	            Slot VARCHAR(10)
	        ); 
	
	WITH PaytableCTE(
	    GameName,
	    Paytable_Description,
	    PaytableId,
	    Denom,
	    ZONE,
	    Manufacturer,
	    Slot
	) 
	AS 
	(
	    SELECT Game_Title,
	           P.PT_Description,
	           Paytable_ID,
	           --MS.MGMD_GAMES_BET,              
	           CAST(MI.MGMD_Denom_Value AS FLOAT) / 100,
	           Z.Zone_Name,
	           Manufacturer_Name,
	           BP.Bar_Position_Name
	    FROM   MGMD_Installation MI
	           INNER JOIN Paytable P
	                ON  MI.MGMD_Paytable_ID = P.Paytable_ID
	           INNER JOIN Game_Library GL
	                ON  MI.MGMD_Game_ID = GL.MG_Game_ID
	           INNER JOIN Game_Title GT
	                ON  GL.MG_Group_ID = GT.Game_Title_ID
	           INNER JOIN MGMD_SessionDelta MS
	                ON  MI.MGMD_ID = MS.MGMD_Combination_ID
	           INNER JOIN Installation I
	                ON  MI.MGMD_Installation_ID = I.Installation_ID
	           INNER JOIN BAR_POSITION BP
	                ON  BP.Bar_Position_ID = I.Bar_Position_ID
	           LEFT JOIN Zone Z
	                ON  BP.Zone_ID = Z.Zone_ID
	           INNER JOIN MACHINE M
	                ON  M.Machine_ID = I.Machine_ID
	           INNER JOIN Machine_Class MC
	                ON  M.Machine_Class_ID = MC.Machine_Class_ID
	           INNER JOIN manufacturer
	                ON  manufacturer.Manufacturer_ID = MC.Manufacturer_ID
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
	                   AND (@Company IS NULL OR (ISNULL(@Company,0) <> 0 AND C.Company_ID = @Company))
	                   AND (@SubCompany IS NULL OR (ISNULL(@SubCompany,0) <> 0 AND SC.Sub_Company_ID = @SubCompany))
	                   AND (@Region IS NULL OR (ISNULL(@Region,0) <> 0 AND S.Sub_Company_Region_ID = @Region))
	                   AND (@Area IS NULL OR (ISNULL(@Area,0) <> 0 AND S.Sub_Company_Area_ID = @Area))
	                   AND (@District IS NULL OR (ISNULL(@District,0) <> 0 AND S.Sub_Company_District_ID = @District))
	                   AND (@Zone IS NULL OR (ISNULL(@Zone,0) <> 0 AND Z.Zone_ID = @Zone))
	               )
	    GROUP BY
	           Game_Title,
	           P.PT_Description,
	           Paytable_ID,
	           --MS.MGMD_GAMES_BET,              
	           MI.MGMD_Denom_Value,
	           Z.Zone_Name,
	           Manufacturer_Name,
	           BP.Bar_Position_Name,
	           CONVERT(VARCHAR(20), MGMD_Start_DateTime, 103)
	)        
	
	INSERT INTO @PayTable
	SELECT GameName,
	       Paytable_Description,
	       PaytableId,
	       0 PaytableCount,
	       Denom,
	       ZONE,
	       Manufacturer,
	       Slot
	FROM   PaytableCTE
	GROUP BY
	       GameName,
	       Paytable_Description,
	       PaytableId,
	       Denom,
	       Zone,
	       Manufacturer,
	       Slot 
	
	UPDATE PT
	SET    PaytableCount = PtCount
	FROM   @PayTable PT
	       INNER JOIN (
	                SELECT GameName,
	                       COUNT('Paytable_ID') PtCount
	                FROM   (
	                           SELECT DISTINCT GameName,
	                                  PaytableID
	                           FROM   @PayTable
	                       ) X
	                GROUP BY
	                       GameName,
	                       PaytableID
	            ) Y
	            ON  PT.GameName = Y.GameName       
	
	INSERT INTO @GamePerf
	SELECT Game_Title,
	       CONVERT(
	           DECIMAL(10, 2),
	           CAST(MI.MGMD_Denom_Value AS FLOAT) / 100.00
	       ) AS Denom,
	       Z.Zone_Name,
	       Manufacturer_Name,
	       BP.Bar_Position_Name,
	       P.PT_Description,
	       P.Paytable_ID AS PaytableCount,
	       CASE 
	            WHEN SUM(MS.MGMD_GAMES_BET) > 0 THEN (
	                     SUM(
	                         CAST(MS.MGMD_COINS_IN * I.Installation_Price_Per_Play AS FLOAT)
	                     ) / 100
	                 )
	            ELSE 0
	       END AS AverageBet,	--Total Bet/ No of Games Played        
	       
	       SUM(
	           CAST(MS.MGMD_COINS_IN * I.Installation_Price_Per_Play AS FLOAT)
	       ) / 100 AS TotalBet,
	       (
	           (
	               SUM(
	                   CAST(MS.MGMD_COINS_IN * I.Installation_Price_Per_Play AS FLOAT)
	               ) / 100
	           ) -(
	               SUM(
	                   CAST(MS.MGMD_COINS_OUT * I.Installation_Price_Per_Play AS FLOAT)
	               ) / 100
	           ) -(
	               SUM(
	                   CAST(MS.MGMD_JACKPOT * I.Installation_Price_Per_Play AS FLOAT)
	               ) / 100
	           )
	       ) AS NetWin,	-- Net Win ($) (BillsIn+CoinsIn+TicketsIn+ECashIn)-(TicketOut+Hp+Jp+EcashOut+CoinsOut)          
	       
	       
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
	                     ) -(
	                         SUM(
	                             CAST(MS.MGMD_JACKPOT * I.Installation_Price_Per_Play AS FLOAT)
	                         ) / 100
	                     )
	                 ) / @iDaysOnline
	            ELSE 0
	       END AS AverageWin -- Net Win/ No of Day Online (Not in a use now)
	FROM   MGMD_Installation MI
	       INNER JOIN Paytable P
	            ON  MI.MGMD_Paytable_ID = P.Paytable_ID
	       INNER JOIN Game_Library GL
	            ON  MI.MGMD_Game_ID = GL.MG_Game_ID
	       INNER JOIN Game_Title GT
	            ON  GL.MG_Group_ID = GT.Game_Title_ID
	       INNER JOIN MGMD_SessionDelta MS
	            ON  MI.MGMD_ID = MS.MGMD_Combination_ID
	       INNER JOIN Installation I
	            ON  MI.MGMD_Installation_ID = I.Installation_ID
	       INNER JOIN BAR_POSITION BP
	            ON  BP.Bar_Position_ID = I.Bar_Position_ID
	       LEFT JOIN Zone Z
	            ON  BP.Zone_ID = Z.Zone_ID
	       INNER JOIN MACHINE M
	            ON  M.Machine_ID = I.Machine_ID
	       INNER JOIN Machine_Class MC
	            ON  M.Machine_Class_ID = MC.Machine_Class_ID
	       INNER JOIN manufacturer
	            ON  manufacturer.Manufacturer_ID = MC.Manufacturer_ID
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
	               AND (@Company IS NULL OR (ISNULL(@Company,0) <> 0 AND C.Company_ID = @Company))
	                   AND (@SubCompany IS NULL OR (ISNULL(@SubCompany,0) <> 0 AND SC.Sub_Company_ID = @SubCompany))
	                   AND (@Region IS NULL OR (ISNULL(@Region,0) <> 0 AND S.Sub_Company_Region_ID = @Region))
	                   AND (@Area IS NULL OR (ISNULL(@Area,0) <> 0 AND S.Sub_Company_Area_ID = @Area))
	                   AND (@District IS NULL OR (ISNULL(@District,0) <> 0 AND S.Sub_Company_District_ID = @District))
	                   AND (@Zone IS NULL OR (ISNULL(@Zone,0) <> 0 AND Z.Zone_ID = @Zone))
	           )
	GROUP BY
	       Game_Title,
	       P.PT_Description,
	       Paytable_ID,
	       --MS.MGMD_GAMES_BET,              
	       MI.MGMD_Denom_Value,
	       Z.Zone_Name,
	       Manufacturer_Name,
	       BP.Bar_Position_Name               
	
	SELECT G.GameName,
	       G.Denom AS Denom,
	       G.Zone,
	       G.Manufacturer,
	       G.Slot,
	       G.Paytable_Description,
	       P.PaytableCount AS paytablecount,
	       ROUND(AverageBet / COALESCE(SUM(P.PaytableCount), 1), 2) AS 
	       AverageBet,
	       G.TotalBet,
	       G.NetWin,
	       ROUND(G.NetWin / SUM(P.PaytableCount), 2) AS AverageWin,
	       CASE 
	            WHEN ROUND(AverageBet / COALESCE(SUM(P.PaytableCount), 1), 2) > 
	                 0 THEN (
	                     ROUND(G.NetWin / SUM(P.PaytableCount), 2) / ROUND(AverageBet / COALESCE(SUM(P.PaytableCount), 1), 2)
	                 ) * 100 --(Net Win/Total Bet) * 100
	            ELSE 0
	       END AS AverageNetPerc
	FROM   @GamePerf G
	       INNER JOIN @PayTable P
	            ON  G.GameName = P.GameName
	            AND G.Denom = P.Denom
	            AND G.Paytable_Description = P.Paytable_Description
	            AND ISNULL(G.Zone, '') = ISNULL(P.ZONE, '')
	            AND G.Manufacturer = P.Manufacturer
	            AND G.Slot = P.Slot
	WHERE  G.TotalBet > 0
	GROUP BY
	       G.GameName,
	       G.Denom,
	       G.Zone,
	       G.Manufacturer,
	       G.Slot,
	       G.Paytable_Description,
	       P.PaytableCount,
	       G.NetWin,
	       G.TotalBet,
	       G.AverageWin,
	       G.AverageBet
GO

