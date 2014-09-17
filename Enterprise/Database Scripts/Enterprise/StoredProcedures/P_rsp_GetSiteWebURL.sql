USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetSiteWebURL]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetSiteWebURL]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =================================================================  
-- rsp_GetSiteWebURL  
-- -----------------------------------------------------------------  
--  
-- Get Site Web URL Change Notification Event for SQL Dependency
--   
-- -----------------------------------------------------------------      
-- Revision History         
--         
-- Yoganandh.P		26/07/2010		Created
-- =================================================================   
CREATE PROCEDURE rsp_GetSiteWebURL
AS
BEGIN
	SELECT WebURL FROM dbo.Site
END


GO

