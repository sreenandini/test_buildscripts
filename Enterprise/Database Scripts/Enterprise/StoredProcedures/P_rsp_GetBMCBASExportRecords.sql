USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetBMCBASExportRecords]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetBMCBASExportRecords]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =================================================================  
-- rsp_GetBMCBASExportRecords  
-- -----------------------------------------------------------------  
--  
-- Get the top 100 records to process.  
--   
-- -----------------------------------------------------------------      
-- Revision History         
--         
-- 18/11/09 Renjish Created        
-- =================================================================   
  
CREATE PROCEDURE dbo.rsp_GetBMCBASExportRecords  
AS  
  
SELECT TOP 100 * FROM BMC_BAS_Export_History WHERE ISNULL(BBEH_Status,0) <> 100 --AND ISNULL(BBEH_Status,0) <> 200 
ORDER BY 1 ASC  


GO

