USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_UpdateSiteAAMSDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_UpdateSiteAAMSDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =================================================================  
-- usp_UpdateSiteAAMSDetails  
-- -----------------------------------------------------------------  
--  
-- Get the game details for the VLT.    
--   
-- -----------------------------------------------------------------      
-- Revision History         
--         
-- 19/11/09 Renjish Created        
-- =================================================================  

CREATE PROCEDURE dbo.usp_UpdateSiteAAMSDetails
@MessageID VARCHAR(20),
@LocationID VARCHAR(12)
AS

UPDATE BMC_AAMS_Details
SET BAD_AAMS_Code = @LocationID
WHERE BAD_Reference_ID = (SELECT BBEH_Reference FROM BMC_BAS_Export_History 
WHERE BBEH_BAS_Message_ID = @MessageID)
AND BAD_Is_Warehouse = (SELECT ISNULL(BBEH_Process_Type,0) FROM BMC_BAS_Export_History 
WHERE BBEH_BAS_Message_ID = @MessageID)
AND BAD_AAMS_Entity_Type = 2


GO

