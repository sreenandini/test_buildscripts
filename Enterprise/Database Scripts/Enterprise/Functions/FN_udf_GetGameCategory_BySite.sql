USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[udf_GetGameCategory_BySite]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[udf_GetGameCategory_BySite]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--------------------------------------------------------------------------                           
--                          
-- Description: Gets the Game category based upon the site id
--                          
-- Inputs:      NONE                    
--                          
-- Outputs:     Filtered Game category data
--                          

-- =======================================================================                          
--                           
-- Revision History                          
--                           
-- A.Vinod Kumar		07/01/2011		Created		
---------------------------------------------------------------------------   
CREATE FUNCTION udf_GetGameCategory_BySite 
(	
	@Site_Id INT
)
RETURNS TABLE 
AS
RETURN 
(
	SELECT ROW_NUMBER() OVER(ORDER BY GC.Game_Category_ID) AS ROWID_Game_Category_ID
	   ,GC.[Game_Category_ID]
	   ,GC.[Game_Category_Name]
	FROM dbo.udf_GetGameLibrary_BySite(@Site_Id) GM
	INNER JOIN Game_Title GT ON GT.Game_Title_ID = GM.MG_Group_ID  
	INNER JOIN Game_Category GC ON GC.[Game_Category_ID] = GT.[Game_Category_ID]
	GROUP BY GC.[Game_Category_ID]
		  ,GC.[Game_Category_Name]
)

GO

