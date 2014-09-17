USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetAAMSFinancialData]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetAAMSFinancialData]
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
-- Returns the Fi.  
--   
-- -----------------------------------------------------------------      
-- Revision History         
--         
-- 18/11/09 Renjish Created        
-- =================================================================    
  
CREATE PROCEDURE dbo.rsp_GetAAMSFinancialData
@ID AS INT  
AS  
  
SELECT BBEH_Financial_Data FROM BMC_BAS_Export_History WITH(NOLOCK) WHERE BBEH_ID = @ID  


GO

