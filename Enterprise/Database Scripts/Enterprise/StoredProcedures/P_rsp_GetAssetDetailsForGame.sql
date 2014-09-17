USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetAssetDetailsForGame]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetAssetDetailsForGame]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--------------------------------------------------------------------------------------------------------------   
--  
-- Description: Asset Details For Game Info
--      
-- Inputs:      See inputs   
-- Outputs:     NONE  
--  
-- Revision History
--
--	Senthil M	14/07/2010		Created
--  Senthil M   26/11/10        Added Game payout % and active
-- =============================================================================================================  
CREATE PROCEDURE dbo.rsp_GetAssetDetailsForGame
	@GameId INT
AS
BEGIN
	SELECT IG.IGI_ID,
	       S.Site_Name,
	       BP.Bar_Position_Name,
	       M.Machine_Stock_No,
	       M.Machine_Manufacturers_Serial_No,
	       GL.Game_Part_Number,
	       CASE 
	            WHEN I.Installation_End_date IS NULL THEN 1
	            ELSE 0
	       END AS IsGameActive
	FROM   Installation_game_info IG WITH(NOLOCK)
	       LEFT OUTER JOIN Game_Library GL WITH(NOLOCK)
	            ON  IG.IGI_Game_ID = GL.MG_Game_ID
	       LEFT OUTER JOIN Installation I WITH(NOLOCK)
	            ON  IG.Installation_No = I.Installation_ID
	       LEFT OUTER JOIN MACHINE M WITH(NOLOCK)
	            ON  M.Machine_ID = I.Machine_ID
	       INNER JOIN BAR_POSITION BP WITH(NOLOCK)
	            ON  BP.Bar_Position_ID = I.Bar_Position_ID
	       INNER JOIN SITE S WITH(NOLOCK)
	            ON  S.Site_ID = BP.Site_ID
	WHERE  GL.MG_Game_ID = @GameId
END

GO

