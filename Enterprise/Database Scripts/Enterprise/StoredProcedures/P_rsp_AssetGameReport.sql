USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_AssetGameReport]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_AssetGameReport]
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
-- Description: fetch the data for the games installed in a position
-- Inputs:      See inputs 
--
-- Outputs:         
--                  
-- =======================================================================
-- 
-- Revision History
--
-- Sudarsan S		26/10/2009		Created
------------------------------------------------------------------------------------------------------
CREATE PROCEDURE dbo.rsp_AssetGameReport
	@BarPosition_ID	INT,
	@Site_Id INT,
	@StartDate		DATETIME,
	@EndDate		DATETIME
AS

BEGIN
 DECLARE @iDays INT  
  DECLARE @AddShortpay VARCHAR(10)   
 SELECT @AddShortpay = setting_value  
 FROM   setting  
 WHERE  setting_name = 'AddShortpayInVoucherOut'  
 SELECT @iDays = DATEDIFF(DAY, @StartDate, @EndDate) + 1    
   
 SELECT I.Bar_Position_ID,  
        M.Machine_Stock_No,  
        MF.Manufacturer_Name AS MG_Game_Manufacturer_Name,  
        CASE   
             WHEN ISNULL(GL.MG_Game_Name, '') <> '' THEN GL.MG_Game_Name  
             ELSE '[N/a]'  
        END AS Game_Name,  
        GT.Game_Title AS MG_Alias_Game_Name,  
        SUM(  
            CAST(MG.MGMD_COINS_IN * I.Installation_Price_Per_Play AS FLOAT)  
        ) / 100 AS Handle,
        (               
		(SUM(CAST(MG.MGMD_COINS_IN * I.Installation_Price_Per_Play AS FLOAT))/100)-              
		(SUM(CAST(MG.MGMD_COINS_OUT * I.Installation_Price_Per_Play AS FLOAT))/100)-              
		(SUM(CAST(MG.MGMD_JACKPOT * I.Installation_Price_Per_Play AS FLOAT))/100)              
        ) AS NetWin,  
        (  
            (               
		(SUM(CAST(MG.MGMD_COINS_IN * I.Installation_Price_Per_Play AS FLOAT))/100)-              
		(SUM(CAST(MG.MGMD_COINS_OUT * I.Installation_Price_Per_Play AS FLOAT))/100)-              
		(SUM(CAST(MG.MGMD_JACKPOT * I.Installation_Price_Per_Play AS FLOAT))/100)              
        )  
        ) / @iDays AS DailyWin,
        CASE WHEN SUM(MG.MGMD_GAMES_BET) > 0 THEN                
		(SUM(CAST(MG.MGMD_COINS_IN * I.Installation_Price_Per_Play AS FLOAT))/100)/SUM(MG.MGMD_GAMES_BET)  ELSE 0 END AS AvgBet,  
        CASE   
             WHEN SUM(MG.MGMD_GAMES_BET) > 0 THEN SUM(MG.MGMD_GAMES_BET)  
             ELSE 0  
        END AS Played,  
        SUM(CAST(MG.MGMD_COINS_IN AS FLOAT)) / 100 AS TotalBet,  
        (SUM(CAST(MG.MGMD_COINS_OUT * I.Installation_Price_Per_Play AS FLOAT)) / 100)+
        (SUM(CAST(MG.MGMD_JACKPOT * I.Installation_Price_Per_Play AS FLOAT))/100)+
        (SUM(CAST(MG.MGMD_progressive_win_value * I.Installation_Price_Per_Play AS FLOAT))/100) AS TotalWon,  
        PT.Payout AS Theo,  
        --((Handle - NetWin)/Handle) * 100
        CASE
           WHEN SUM(MG.MGMD_COINS_IN) > 0 THEN 
           (
				(
					(
						(SUM(CAST(MG.MGMD_COINS_IN * I.Installation_Price_Per_Play AS FLOAT)) / 100) - 
						(
							(SUM(CAST(MG.MGMD_COINS_IN * I.Installation_Price_Per_Play AS FLOAT))/100)-
							(SUM(CAST(MG.MGMD_COINS_OUT * I.Installation_Price_Per_Play AS FLOAT))/100)-
							(SUM(CAST(MG.MGMD_JACKPOT * I.Installation_Price_Per_Play AS FLOAT))/100)
						 )
					 )
					/(SUM(CAST(MG.MGMD_COINS_IN * I.Installation_Price_Per_Play AS FLOAT)) / 100)
				  ) * 100
		    )
			ELSE 0
			END AS Actual,
        CASE   
             WHEN SUM(MG.MGMD_COINS_IN) > 0 THEN 
				PT.Payout - 
				(
					(
						(SUM(CAST(MG.MGMD_COINS_IN * I.Installation_Price_Per_Play AS FLOAT)) / 100) - 
						(
							(SUM(CAST(MG.MGMD_COINS_IN * I.Installation_Price_Per_Play AS FLOAT))/100)-
							(SUM(CAST(MG.MGMD_COINS_OUT * I.Installation_Price_Per_Play AS FLOAT))/100)-
							(SUM(CAST(MG.MGMD_JACKPOT * I.Installation_Price_Per_Play AS FLOAT))/100)
						 )
					 )
					/(SUM(CAST(MG.MGMD_COINS_IN * I.Installation_Price_Per_Play AS FLOAT)) / 100)
				 ) * 100
             ELSE 0  
        END AS Variance  
 FROM   dbo.Installation I WITH (NOLOCK)  
        INNER JOIN dbo.Bar_Position B WITH (NOLOCK)  
             ON  I.Bar_Position_ID = B.Bar_Position_ID  
        INNER JOIN dbo.MGMD_Installation MGI WITH (NOLOCK)  
             ON  I.Installation_ID = MGI.MGMD_Installation_ID  
        INNER JOIN dbo.MGMD_SessionDelta MG WITH (NOLOCK)  
             ON  MG.MGMD_Combination_ID = MGI.MGMD_ID  
        INNER JOIN dbo.Game_Library GL WITH (NOLOCK)  
             ON  MGI.MGMD_Game_ID = GL.MG_Game_ID  
        INNER JOIN dbo.Game_Title GT WITH (NOLOCK)  
             ON  GT.Game_Title_ID = GL.MG_Group_ID  
        INNER JOIN dbo.PayTable PT WITH (NOLOCK)  
             ON  PT.Paytable_ID = MGI.MGMD_Paytable_ID  
        INNER JOIN dbo.Machine M WITH (NOLOCK)  
             ON  M.Machine_ID = I.Machine_ID  
        LEFT JOIN dbo.Manufacturer MF WITH (NOLOCK)  
             ON  GT.Manufacturer_ID = MF.Manufacturer_ID  
        WHERE  MG.MGMD_Start_DateTime >= @StartDate
        AND  MG.MGMD_End_DateTime <= @EndDate
        AND B.Site_ID=@Site_Id 
 GROUP BY  
        I.Bar_Position_ID,  
        GL.MG_Game_Name,  
        M.Machine_Stock_No,  
        MF.Manufacturer_Name,  
        PT.Payout,  
        GT.Game_Title  
END
GO
