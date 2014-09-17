USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetAllSiteDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetAllSiteDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =================================================================  
-- rsp_GetAllSiteDetails  
-- -----------------------------------------------------------------  
--  
-- Get Site Details
--   
-- -----------------------------------------------------------------      
-- Revision History         
--         
-- Yoganandh.P		29/06/2010		Created
-- =================================================================   
CREATE PROCEDURE rsp_GetAllSiteDetails
AS
BEGIN
	SELECT 
		Site_ID, Site_Code, Site_Name, WebURL
	FROM 
		[Site]	
END

GO

