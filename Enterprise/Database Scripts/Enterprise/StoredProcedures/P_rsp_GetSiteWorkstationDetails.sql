USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetSiteWorkstationDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetSiteWorkstationDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =================================================================  
-- rsp_GetSiteWorkstationDetails  
-- -----------------------------------------------------------------  
--  
-- returns all workstation details.  rsp_GetSiteWorkstationDetails 1012 
--   
-- -----------------------------------------------------------------      
-- Revision History         
--         
-- 29/04/2010 Vineetha M  Created        
-- =================================================================    
CREATE PROCEDURE dbo.rsp_GetSiteWorkstationDetails  
@iSiteID INT  
  
AS  

SELECT Site_Workstation as TIW_Name FROM siteworkstations SW WHERE SW.site_id =(SELECT site_id FROM SITE WHERE site_code=@iSiteID)
  
GO

