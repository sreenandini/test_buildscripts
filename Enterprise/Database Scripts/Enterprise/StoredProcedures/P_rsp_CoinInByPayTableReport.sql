USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_CoinInByPayTableReport]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_CoinInByPayTableReport]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
--sp_helptext 'rsp_CoinInByPayTableReport'
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--exec "rsp_CoinInByPayTableReport" 1,0,0,0,0,0,{ts '2014-04-18 00:00:01'},{ts '2014-05-19 16:23:09'},'Asset','1,4'
    
--------------------------------------------------------------------------     
--    
-- Description: Fetches data for Coin In by Paytable Report
-- Inputs:      See inputs     
--    
-- Outputs:             
--                      
-- =======================================================================    
--     
-- Revision History    
--    
-- Yoganandh P		06/07/2010		Created   
-- Yoganandh P		14/07/2010		Modified Paytable Join Condition	 
-- A.Vinod Kumar	27/12/2010		Price per play multiplied with Bet, Wins and jackpots

------------------------------------------------------------------------------------------------------    
CREATE PROCEDURE rsp_CoinInByPayTableReport
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
	       CAST(tMI.MGMD_Denom_Value / 100.0 AS FLOAT) AS Denom,
	       tGT.Game_Title_ID AS GameId,
	       CASE 
	            WHEN ISNULL(tGT.Game_Title, '') <> '' THEN tGT.Game_Title
	            ELSE '[N/a]'
	       END AS GameName,
	       tPT.PT_Description AS PayoutDescription,
	       tPT.Payout AS Payout,
	       SUM(
	           CAST(
	               (tMS.MGMD_Coins_IN * tI.Installation_Price_Per_Play) / 100.0 
	               AS FLOAT
	           )
	       ) AS Bet,
	       (
	           (100 - tPT.Payout) * SUM(
	               CAST(
	                   (tMS.MGMD_Coins_IN * tI.Installation_Price_Per_Play) /
	                   100.0 AS FLOAT
	               )
	           )
	       ) / 100 AS TheoreticalWin,
	       (100 - tPT.Payout) AS TheoreticalHoldPercentage
	FROM   Installation tI
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
	       INNER JOIN Bar_Position Bp
	            ON  Bp.Bar_Position_ID = tI.Bar_Position_ID
	       INNER JOIN SITE S
	            ON  Bp.Site_ID = S.Site_ID	     
	       INNER JOIN Sub_Company SC WITH(NOLOCK)
	            ON  SC.sub_company_id = S.sub_company_id
	       INNER JOIN Company C WITH(NOLOCK)
	            ON  SC.company_id = C.company_id
	WHERE  (
	           (
	               tMS.MGMD_Start_DateTime >= @StartDate
	               AND tMS.MGMD_End_DateTime <= @EndDate
	           )	          
	           AND ISNULL(@Site ,S.Site_ID) = S.Site_ID  
	           AND ISNULL(@Company, C.Company_ID) = C.Company_ID
	           AND ISNULL(@SubCompany, S.Sub_Company_ID) = S.Sub_Company_ID
	           AND ISNULL(@Region, S.Sub_Company_Region_ID) = S.Sub_Company_Region_ID
	           AND ISNULL(@Area, S.Sub_Company_Area_ID) = S.Sub_Company_Area_ID
	           AND ISNULL(@District, S.Sub_Company_District_ID) = S.Sub_Company_District_ID
	           AND (
	               @SiteIDList IS NOT NULL
	               AND S.Site_ID IN (SELECT DATA
	                                 FROM   fnSplit(@SiteIDList, ','))
	               )
	       ) 
	    
	GROUP BY
	       tM.Machine_Stock_No,
	       tM.Machine_Manufacturers_Serial_No,
	       tMI.MGMD_Denom_Value,
	       tGT.Game_Title_ID,
	       tGT.Game_Title,
	       tPT.PT_Description,
	       tPT.Payout
	ORDER BY
	       tM.Machine_Stock_No ASC
END
GO

