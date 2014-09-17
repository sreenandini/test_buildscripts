USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetSiteList]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetSiteList]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--------------------------------------------------------------------------         
--        
-- Description: List All Existing Site  
--            
--        
-- Inputs:      NONE  
--        
-- Outputs:     Selects a result set listing all the site in enterprise        
--        
-- Return:          See Comments      
--        
-- =======================================================================        
--         
-- Revision History        
--         
-- NaveenChander     15/05/2008     Created        
---------------------------------------------------------------------------      
  
CREATE PROCEDURE rsp_GetSiteList         
As        
BEGIN        
    SELECT DISTINCT SITE_CODE FROM SITE WHERE ISNULL(LTRIM(RTRIM(SITE_CODE)),'') <> ''        
END 


GO

