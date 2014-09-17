USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetGameLibraryForSiteConfig]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetGameLibraryForSiteConfig]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--------------------------------------------------------------------------                           
--                          
-- Description: Get the data from Game Library table and convert that into XML
--                          
-- Inputs:      NONE                    
--                          
-- Outputs:     Game Library XML Data
--                          

-- =======================================================================                          
--                           
-- Revision History                          
--                           
-- GBabu     12/11/2010     Created    
-- GBabu     23/11/2010     Modified	Get the Xml properly
-- A.Vinod Kumar		06/01/2011		Modified		Site code included
---------------------------------------------------------------------------                    

CREATE PROCEDURE [dbo].[rsp_GetGameLibraryForSiteConfig]
(
	@Site_Id INT
)
AS
BEGIN
	DECLARE @XMLData XML
	SET @XMLData = (
	SELECT GM.MG_Game_ID AS Game_ID  
		,GM.MG_Game_Name AS Game_Name      
		,GM.MG_Game_StartDate AS Game_Start_Date        
		,GM.MG_Game_Version AS Version      
		,GM.MG_Game_SerialNo AS SerialNo          
		,M.Manufacturer_Name AS Manufacturer
		,GM.Game_Part_Number As GamePartNumber  
		,GT.ROWID_Game_Title_ID As GroupID		 
		,GM.[MG_HQ_Game_ID] AS HQ_Game_ID
		,GT.[Game_Title] AS Game_Title
		,GT.[Game_Category_Name] AS Game_Category
	FROM [dbo].[udf_GetGameLibrary_BySite](@Site_id) GM
		INNER JOIN [dbo].[udf_GetGameTitle_BySite](@Site_id) GT ON GT.Game_Title_ID = GM.MG_Group_ID  
		LEFT JOIN Manufacturer M ON M.Manufacturer_ID = GT.Manufacturer_ID	
	ORDER BY GM.[MG_HQ_GAME_ID]
	FOR XML PATH('GAME_LIBRARY'), ELEMENTS XSINIL, ROOT('GAME'))	
	SELECT CONVERT(VARCHAR(MAX),@XMLData)  
END


GO

