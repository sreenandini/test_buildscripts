USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_MultiGameMultiDenomReport]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_MultiGameMultiDenomReport]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--------------------------------------------------------------------------   
--  
-- Description: Fetches the issued Voucher/ticket records
--  
-- Inputs: 	@SubCompany	,@Site,@StartDate,@EndDate,@GroupBy
-- Outputs:       
--  
-- =======================================================================  
--   
-- Revision History  

-- Kirubakar S 21/05/2010 Created
  
--------------------------------------------------------------------------- 

CREATE PROCEDURE dbo.rsp_MultiGameMultiDenomReport 
	--@BarPosition_ID	INT=0,
	@SubCompany		Varchar(25),
	@Site			int,
	@StartDate		DATETIME,
	@EndDate		DATETIME,
	@GroupBy		VARCHAR(25)
AS

BEGIN

	DECLARE @iDays	INT
	SELECT @iDays = DATEDIFF(DAY, @StartDate, @EndDate) + 1

	SET @EndDate = DATEADD(S, -1, DATEADD(DAY, 1, @EndDate))

	SELECT  --I.Bar_Position_ID,
			M.Machine_Stock_No,
			CASE WHEN ISNULL(MF.Manufacturer_Name ,'')<>'' THEN MF.Manufacturer_Name ELSE 'N/a' END AS MG_Game_Manufacturer_Name,
			CASE WHEN ISNULL(GL.MG_Game_Name, '') <> '' THEN GL.MG_Game_Name ELSE 'N/a' END AS Game_Name,
			GT.Game_Title AS AliasGameName,
			CAST(MGI.MGMD_Denom_Value/100.0 AS FLOAT) AS MGMD_Denom_Value,
			SUM(CAST(MG.MGMD_COINS_IN * I.Installation_Price_Per_Play AS FLOAT))/100 AS Handle,
			(SUM(CAST(MG.MGMD_COINS_IN * I.Installation_Price_Per_Play AS FLOAT)) - 
			SUM(CAST(MG.MGMD_COINS_OUT * I.Installation_Price_Per_Play AS FLOAT)) - 
			SUM(CAST(MG.MGMD_JACKPOT * I.Installation_Price_Per_Play AS FLOAT)) ) / 100 AS NetWin,
			((SUM(CAST(MG.MGMD_COINS_IN * I.Installation_Price_Per_Play AS FLOAT)) - 
			SUM(CAST(MG.MGMD_COINS_OUT * I.Installation_Price_Per_Play AS FLOAT)) -
			SUM(CAST(MG.MGMD_JACKPOT * I.Installation_Price_Per_Play AS FLOAT)) ) / 100) / @iDays AS DailyWin,
			CASE WHEN  SUM(MG.MGMD_GAMES_BET)>0 THEN
			(SUM(CAST(MG.MGMD_COINS_IN * I.Installation_Price_Per_Play AS FLOAT))/100) / SUM(MG.MGMD_GAMES_BET) 
			ELSE 0 END AS AvgBet,
			SUM(MG.MGMD_GAMES_BET) AS Played,
			CAST(SUM(MG.MGMD_COINS_IN) AS FLOAT)/100 AS TotalBet,
			(CAST(SUM(MG.MGMD_COINS_OUT) AS FLOAT)/100 +
			CAST(SUM(MG.MGMD_JACKPOT) AS FLOAT)/100) AS TotalWon,
			(100 - ISNULL(PT.Payout,0))AS Payout,--% Hold Theo = 100 - Theo Payout %  
		   PT.PT_Description AS PaytableDescription,      
		   CASE WHEN SUM(MG.MGMD_COINS_IN) > 0 THEN  
				 (100 - (100*((SUM(CAST(MG.MGMD_COINS_OUT * I.Installation_Price_Per_Play AS FLOAT)) 
				+ SUM(CAST(MG.MGMD_JACKPOT * I.Installation_Price_Per_Play AS FLOAT)))
				/ SUM(CAST(MG.MGMD_COINS_IN * I.Installation_Price_Per_Play AS FLOAT))) ) )
		   ELSE 0 END AS Actual, --% Hold Actual = 100 - (100 * (Credits Won/Credits Bet))  
		   CASE WHEN SUM(MG.MGMD_COINS_IN) > 0 THEN  
		   --100 - ((100 - ISNULL(PT.Payout,0)) + ISNULL((SUM(CAST(MG.MGMD_COINS_OUT * I.Installation_Price_Per_Play AS FLOAT)) 
			--/ SUM(CAST(MG.MGMD_COINS_IN * I.Installation_Price_Per_Play AS FLOAT))) * 100,0))  
		   ((100 -(100*(SUM(CAST(MG.MGMD_COINS_OUT * I.Installation_Price_Per_Play AS FLOAT)) 
				/ SUM(CAST(MG.MGMD_COINS_IN * I.Installation_Price_Per_Play AS FLOAT))))) 
				-(100 - ISNULL(PT.Payout,0)))  
		   ELSE 0 END AS Variance --% Hold Variance = % Hold Actual - % Hold Theo  

	  FROM dbo.Installation I
		--INNER JOIN dbo.Bar_Position B ON I.Bar_Position_ID = B.Bar_Position_ID
		INNER JOIN dbo.MGMD_Installation MGI ON I.Installation_ID = MGI.MGMD_Installation_ID
		LEFT JOIN dbo.MGMD_SessionDelta MG ON MG.MGMD_Combination_ID = MGI.MGMD_ID
		INNER JOIN dbo.Game_Library GL ON MGI.MGMD_Game_ID = GL.MG_Game_ID
		INNER JOIN dbo.Game_Title GT ON GT.Game_Title_ID = GL.MG_Group_ID
		INNER JOIN dbo.PayTable PT ON PT.Game_ID = GL.MG_Game_ID
		INNER JOIN dbo.Machine M ON M.Machine_ID = I.Machine_ID
		-- LEFT JOIN dbo.GameMaster GM ON GL.Master_Game_ID = GM.Master_Game_ID
		LEFT JOIN dbo.Manufacturer MF ON GT.Manufacturer_ID = MF.Manufacturer_ID
	WHERE 
		(@Site=0 OR I.Bar_Position_ID in(SELECT Bar_Position_ID FROM Bar_Position WHERE SITE_ID=@Site))
		AND MG.MGMD_Start_DateTime BETWEEN @StartDate AND @EndDate
		AND MG.MGMD_End_DateTime BETWEEN @StartDate AND @EndDate
	 -- AND (@BarPosition_ID=0 OR I.Bar_Position_ID = @BarPosition_ID) --AND M.IsMultiGame = 1
	GROUP BY 
--I.Bar_Position_ID,
 GL.MG_Game_Name, GT.Game_Title, M.Machine_Stock_No, MF.Manufacturer_Name, PT.Payout, PT.PT_Description, MGI.MGMD_Denom_Value			

END


GO

