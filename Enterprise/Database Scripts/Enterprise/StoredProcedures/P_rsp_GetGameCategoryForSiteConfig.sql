USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetGameCategoryForSiteConfig]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetGameCategoryForSiteConfig]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =================================================================  
-- rsp_GetGameCategoryForSiteConfig  
-- -----------------------------------------------------------------  
--  
-- Get the Game Category Details to export to Exchange.    
--   
-- -----------------------------------------------------------------      
-- Revision History         
--         
-- 06/09/10 Renjish Created      
-- 16/12/10 GBabu	Used XML 
-- 07/01/11 A.Vinod Kumar	Site based category
-- =================================================================  

CREATE PROCEDURE dbo.rsp_GetGameCategoryForSiteConfig
(	
	@Site_Id INT
)
AS
DECLARE @XMLData XML
	SET @XMLData = (
	 SELECT [ROWID_Game_Category_ID] AS Game_Category_ID
      ,[Game_Category_Name] FROM [dbo].[udf_GetGameCategory_BySite] (@Site_Id) 
FOR XML PATH ('Game_Category') ,ELEMENTS XSINIL,ROOT('Game'))
SELECT CONVERT(VARCHAR(MAX),@XMLData)  




GO

