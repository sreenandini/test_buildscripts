
USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_MultiGameSlotDetailReport]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_MultiGameSlotDetailReport]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
  
    
--------------------------------------------------------------------------     
--    
-- Description: Fetches data for Multi Game Slot Detail Report
-- Inputs:      See inputs     
--    
-- Outputs:             
--                      
-- =======================================================================    
--     
-- Revision History    
--    
-- Yoganandh P		14/07/2010		Created   
-- A.Vinod Kumar	27/12/2010		Price per play multiplied with Bet, Wins and jackpots
-- Anil				23/02/2011		Changed Net WIN = (Bet - Win)
-- Anil				11/03/2011		Changed Net WIN = (Bet - Win - Jackpot)
------------------------------------------------------------------------------------------------------    
CREATE PROCEDURE rsp_MultiGameSlotDetailReport
	@Company INT = 0,
	@SubCompany INT = 0,
	@Region INT = 0,
	@Area INT = 0,
	@District INT = 0,
	@Site INT = 0,
	@StartDate DATETIME,
	@EndDate DATETIME,
	@GroupBy VARCHAR(25),
	@SiteIDList VARCHAR(MAX)
AS
BEGIN
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
	
	
	SELECT tM.Machine_Stock_No AS AssetNo,
	       tM.Machine_Manufacturers_Serial_No AS SlotSerialNo,
	       CASE 
	            WHEN ISNULL(tGT.Game_Title, '') <> '' THEN tGT.Game_Title
	            ELSE '[N/a]'
	       END AS GameName,
	       tPT.PT_Description AS PayoutDescription,
	       (CAST(tMI.MGMD_Denom_Value AS FLOAT) / 100) Denom,
	       SUM(tMS.MGMD_GAMES_BET) AS Plays,
	       SUM(
	           CAST(
	               (tMS.MGMD_COINS_IN * tI.Installation_Price_Per_Play) / 100.0 
	               AS FLOAT
	           )
	       ) AS Bet,
	       SUM(
	           CAST(
	               (tMS.MGMD_COINS_OUT * tI.Installation_Price_Per_Play) / 100.0 
	               AS FLOAT
	           )
	       ) AS Win,
	       SUM(
	           CAST(
	               (tMS.MGMD_JACKPOT * tI.Installation_Price_Per_Play) / 100.0 
	               AS FLOAT
	           )
	       ) AS Jackpot,
	       (
	           SUM(
	               CAST(
	                   (tMS.MGMD_COINS_IN * tI.Installation_Price_Per_Play) / 
	                   100.0 AS FLOAT
	               )
	           ) --Bet
	           - SUM(
	               CAST(
	                   (tMS.MGMD_COINS_OUT * tI.Installation_Price_Per_Play) / 
	                   100.0 AS FLOAT
	               )
	           )--Win
	           - SUM(
	               CAST(
	                   (tMS.MGMD_JACKPOT * tI.Installation_Price_Per_Play) / 100.0 
	                   AS FLOAT
	               )
	           ) --Jackpot
	       ) AS NetWin --NetWin = Bet-Win-Jackpot
	FROM   Installation tI
	       INNER JOIN Bar_Position bP
	            ON  Bp.Bar_Position_ID = tI.Bar_Position_ID
	       INNER JOIN SITE S
	            ON  S.Site_ID = bp.Site_ID
	       INNER JOIN MGMD_Installation tMI
	            ON  tI.Installation_ID = tMI.MGMD_Installation_ID
	       INNER JOIN MACHINE tM
	            ON  tI.Machine_ID = tM.Machine_ID
	       INNER JOIN Game_Library tGL
	            ON  tMI.MGMD_Game_ID = tGL.MG_Game_ID
	       INNER JOIN PayTable tPT
	            ON  tMI.MGMD_Paytable_ID = tPT.Paytable_ID
	       INNER JOIN Game_Title tGT
	            ON  tGL.MG_Group_ID = tGT.Game_Title_ID
	       INNER JOIN MGMD_SessionDelta tMS
	            ON  tMI.MGMD_ID = tMS.MGMD_Combination_ID
	       INNER JOIN Sub_Company SC
	            ON  S.Sub_Company_ID = SC.Sub_Company_ID
	       INNER JOIN Company C
	            ON  SC.Company_ID = C.Company_ID
	WHERE  (
	           tMS.MGMD_Start_DateTime >= @StartDate
	           AND tMS.MGMD_End_DateTime <= @EndDate
	       )
	       
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
	GROUP BY
	       tM.Machine_Stock_No,
	       tM.Machine_Manufacturers_Serial_No,
	       tMI.MGMD_Denom_Value,
	       tGT.Game_Title,
	       tPT.PT_Description
	HAVING SUM(
	           CAST(
	               (tMS.MGMD_COINS_IN * tI.Installation_Price_Per_Play) / 100.0 
	               AS FLOAT
	           )
	       ) > 0
	ORDER BY
	       tM.Machine_Stock_No ASC
END
GO

