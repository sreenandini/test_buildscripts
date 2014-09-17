USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetSiteAAMSDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetSiteAAMSDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =================================================================  
-- rsp_GetSiteDetailsForAAMSExport  
-- -----------------------------------------------------------------  
--  
-- Get the site details to export for AAMS.  
--   
-- -----------------------------------------------------------------      
-- Revision History         
--         
-- 19/11/09 Renjish Created        
-- ================================================================= 

CREATE PROCEDURE dbo.rsp_GetSiteAAMSDetails
@Site_Name VARCHAR(50)
AS

SELECT S.Site_ID, BAD.BAD_AAMS_Code, BAD.BAD_AAMS_Status    
 FROM dbo.[Site] S    
INNER JOIN dbo.BMC_AAMS_Details BAD ON S.Site_ID = BAD.BAD_Reference_ID    
WHERE S.Site_Name = @Site_Name AND ISNULL(BAD.BAD_Is_Warehouse,0) <> 1 
AND BAD.BAD_AAMS_Entity_Type = 2


GO

