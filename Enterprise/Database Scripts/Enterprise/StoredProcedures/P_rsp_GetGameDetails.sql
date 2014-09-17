USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetGameDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetGameDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- rsp_GetGameDetails
-- -----------------------------------------------------------------
-- 
-- To get game details.
-- 
-- -----------------------------------------------------------------
-- Revision History
-- 
-- 17/08/2012 Dinesh Rathinavel Created
-- 
-- =================================================================

CREATE PROCEDURE [dbo].[rsp_GetGameDetails]
(@ManufacturerID INT, @GameCategoryID INT)
AS        
BEGIN  
  
SELECT   
 GT.Game_Title_ID AS GameTitleId,  
 GT.Game_Title AS GameTitle,  
 GC.Game_Category_ID AS CategoryID,  
 GC.Game_Category_Name AS CategoryName,  
 M.Manufacturer_Name AS ManufacturerName   
FROM Game_Title  GT   
 INNER JOIN Game_Category GC ON GT.Game_Category_ID = GC.Game_Category_ID   
 LEFT JOIN Manufacturer M ON M.Manufacturer_ID = GT.Manufacturer_ID   
WHERE GT.Game_Title_ID <> 1 AND ( @ManufacturerID = 0
                                    OR M.Manufacturer_ID = @ManufacturerID )
                            AND ( @GameCategoryID = 0
                                    OR GC.Game_Category_ID = @GameCategoryID ) ORDER BY GT.Game_Title ASC
END
GO

