USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_CheckBMCBASExportRecords]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_CheckBMCBASExportRecords]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =================================================================  
-- rsp_CheckBMCBASExportRecords  
-- -----------------------------------------------------------------  
--  
-- Returns the count of pending records in BMC BAS Export Table.  
--   
-- -----------------------------------------------------------------      
-- Revision History         
--         
-- 18/11/09 Renjish Created        
-- =================================================================    
  
CREATE PROCEDURE dbo.rsp_CheckBMCBASExportRecords  
AS  
  
SELECT COUNT(BBEH_ID) FROM BMC_BAS_Export_History WITH(NOLOCK) WHERE ISNULL(BBEH_Status,0) <> 100  


GO

