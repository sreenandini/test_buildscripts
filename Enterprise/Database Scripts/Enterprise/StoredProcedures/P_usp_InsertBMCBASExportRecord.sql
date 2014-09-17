USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_InsertBMCBASExportRecord]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_InsertBMCBASExportRecord]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =================================================================  
-- usp_InsertBMCBASExportRecord  
-- -----------------------------------------------------------------  
--  
-- Insert new record into BMC_BAS_Export_History.    
--   
-- -----------------------------------------------------------------      
-- Revision History         
--         
-- 19/11/09 Renjish Created        
-- =================================================================   
  
CREATE PROCEDURE dbo.usp_InsertBMCBASExportRecord  
@Reference varchar(100),  
@Entity_Type INT,
@Message_Type INT,
@Message_ID varchar(20),
@Type INT = 0,
@TypeComm VARCHAR(100) = NULL
AS  
  
INSERT INTO BMC_BAS_Export_History(BBEH_Reference, BBEH_AAMS_Entity_Type, BBEH_Message_Type, BBEH_Status, BBEH_Received_Date, 
BBEH_BAS_Message_ID, BBEH_AAMS_Approval, BBEH_Session_Status, BBEH_Process_Type, BBEH_Process_Type_Comments)
VALUES(@Reference, @Entity_Type, @Message_Type, 0, GETDATE(), @Message_ID, 0, 'Initiated', @Type, @TypeComm)   


GO

