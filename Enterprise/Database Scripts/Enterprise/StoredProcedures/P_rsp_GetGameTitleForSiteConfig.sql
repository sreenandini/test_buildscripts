USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetGameTitleForSiteConfig]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetGameTitleForSiteConfig]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =================================================================  
-- rsp_GetGameTitleForSiteConfig  
-- -----------------------------------------------------------------  
--  
-- Get the Game Title Details to export to Exchange.    
--   
-- -----------------------------------------------------------------      
-- Revision History         
--         
-- 06/09/10 Renjish Created       
-- 16/12/10 GBabu  Used Xml variable
-- 07/01/11 A.Vinod Kumar	Site based title
-- =================================================================  

CREATE PROCEDURE dbo.rsp_GetGameTitleForSiteConfig
(	
	@Site_Id INT
)
AS   
DECLARE @XMLData XML
	SET @XMLData = (
		SELECT GT.[ROWID_Game_Title_ID] AS Game_Title_ID
			  ,GC.[ROWID_Game_Category_ID] AS Game_Category_ID
			  ,GC.Game_Category_Name
			  ,GT.[Game_Title]
			  ,GT.[Manufacturer_ID]
			  ,MA.[Manufacturer_Name]
			FROM dbo.udf_GetGameTitle_BySite(@Site_Id) GT 		
				INNER JOIN dbo.udf_GetGameCategory_BySite(@Site_Id) GC ON GC.[Game_Category_ID] = GT.[Game_Category_ID]
				LEFT JOIN Manufacturer MA ON MA.[Manufacturer_ID] = GT.[Manufacturer_ID]
			GROUP BY GT.[ROWID_Game_Title_ID]
			  ,GC.[ROWID_Game_Category_ID]
			  ,GC.Game_Category_Name
			  ,GT.[Game_Title]
			  ,GT.[Manufacturer_ID]
			  ,MA.[Manufacturer_Name]
			ORDER BY 1
		FOR XML PATH ('Game_Title') ,ELEMENTS XSINIL,ROOT('Game')  )
SELECT CONVERT(VARCHAR(MAX),@XMLData)    


GO

