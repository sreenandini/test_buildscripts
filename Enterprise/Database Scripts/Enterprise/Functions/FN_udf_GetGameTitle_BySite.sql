USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[udf_GetGameTitle_BySite]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[udf_GetGameTitle_BySite]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--------------------------------------------------------------------------                           
--                          
-- Description: Gets the Game title based upon the site id
--                          
-- Inputs:      NONE                    
--                          
-- Outputs:     Filtered Game title data
--                          

-- =======================================================================                          
--                           
-- Revision History                          
--                           
-- A.Vinod Kumar		07/01/2011		Created		
---------------------------------------------------------------------------   
CREATE FUNCTION udf_GetGameTitle_BySite 
(	
	@Site_Id INT
)
RETURNS TABLE 
AS
RETURN 
(
	SELECT ROW_NUMBER() OVER(ORDER BY GT.[Game_Title_ID]) AS [ROWID_Game_Title_ID]
	  ,GT.[Game_Title_ID]
      ,GC.[ROWID_Game_Category_ID]
	  ,GC.[Game_Category_ID]
      ,GT.[Game_Title]
      ,GT.[Manufacturer_ID]
	  ,GC.[Game_Category_Name]
	FROM Game_Title GT 
		INNER JOIN dbo.udf_GetGameLibrary_BySite(@Site_Id) GM ON GM.MG_Group_ID = GT.Game_Title_ID
		INNER JOIN dbo.udf_GetGameCategory_BySite(@Site_Id) GC ON GC.[Game_Category_ID] = GT.[Game_Category_ID]
	GROUP BY GT.[Game_Title_ID]
      ,GC.[ROWID_Game_Category_ID]
	  ,GC.[Game_Category_ID]
      ,GT.[Game_Title]
      ,GT.[Manufacturer_ID]
	  ,GC.[Game_Category_Name]
)

GO

