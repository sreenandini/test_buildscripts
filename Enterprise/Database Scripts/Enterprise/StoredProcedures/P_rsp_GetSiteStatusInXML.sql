USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetSiteStatusInXML]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetSiteStatusInXML]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =================================================================  
-- rsp_GetSiteStatusInXML  
-- -----------------------------------------------------------------  
--  
-- Get Site Status In XML
--   
-- -----------------------------------------------------------------      
-- Revision History         
--         
-- Yoganandh.P			09/09/2010			Created
-- =================================================================   
CREATE PROCEDURE rsp_GetSiteStatusInXML
(
	@Site_ID INT
)
AS
BEGIN

	DECLARE @XMLData XML  

	SET @XMLData =       
	(      
		SELECT Site_Enabled FROM Site WHERE Site_ID = @Site_ID
		FOR XML PATH('SiteStatus'), root('Site')      
	)
		
	SELECT @XMLData

END


GO

