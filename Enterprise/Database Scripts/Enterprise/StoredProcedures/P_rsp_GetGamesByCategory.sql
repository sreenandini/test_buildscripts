USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetGamesByCategory]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetGamesByCategory]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- rsp_GetGamesByCategory
-- -----------------------------------------------------------------
-- 
-- To get all the game category by category name.
-- 
-- -----------------------------------------------------------------
-- Revision History
-- 
-- 10/08/2012 Dinesh Rathinavel Created
-- 
-- =================================================================

CREATE PROCEDURE [dbo].[rsp_GetGamesByCategory] 
	@Game_Category_Name VARCHAR(50),
	@Manufacturer VARCHAR(50),
	 @GameID INT
AS        
BEGIN     
    
 SET NOCOUNT ON  
   
 SELECT DISTINCT  
  tGT.Game_Title_ID,  
  tGT.Game_Title,  
  tGC.Game_Category_ID,  
  tGC.Game_Category_Name,  
  tM.Manufacturer_Name  
 FROM Game_Title tGT 
  LEFT JOIN Game_Library GL ON GL.MG_Group_ID = tGT.Game_Title_ID
  LEFT JOIN game_Category tGC ON tGT.Game_Category_ID = tGC.Game_Category_ID  
  LEFT JOIN Manufacturer tM ON tM.Manufacturer_ID = tGT.Manufacturer_ID  
 WHERE  
  (@Game_Category_Name = 'All' OR Game_Category_Name = @Game_Category_Name)
  AND (@Manufacturer = 'All' OR Manufacturer_Name = @Manufacturer)
  AND (@GameID = 0 OR GL.MG_Game_ID = @GameID)
END

GO

