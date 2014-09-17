USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetBMCBASExportTable]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetBMCBASExportTable]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =================================================================  
-- rsp_CheckBMCBASExportTable  
-- -----------------------------------------------------------------  
--  
-- Returns the data from BMC BAS Export Table.  
-- 
-- -----------------------------------------------------------------      
-- Revision History         
--         
-- 18/11/09 Renjish Created        
-- =================================================================    

CREATE PROCEDURE [dbo].rsp_GetBMCBASExportTable  
AS 
 
SELECT BBEH_ID, BBEH_Message_Type, BBEH_Status FROM dbo.BMC_BAS_Export_History 


GO

