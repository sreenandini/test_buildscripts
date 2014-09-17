USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_InsertBASBMCImportRecord]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_InsertBASBMCImportRecord]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =================================================================  
-- usp_InsertBASBMCImportRecord  
-- -----------------------------------------------------------------  
--  
-- Insert a new record into BMC_BAS_Import_History.    
--   
-- -----------------------------------------------------------------      
-- Revision History         
--         
-- 19/11/09 Renjish Created        
-- =================================================================   
  
CREATE PROCEDURE dbo.usp_InsertBASBMCImportRecord  
@Reference varchar(50),  
@Entity_Type INT
AS  
  
IF NOT EXISTS (SELECT BBIH_Reference FROM BMC_BAS_Import_History WHERE BBIH_BAS_Message_ID = @Reference)
BEGIN
	INSERT INTO BMC_BAS_Import_History(BBIH_Reference, BBIH_AAMS_Entity_Type, BBIH_Status, BBIH_Received_Date, BBIH_Comments, BBIH_Session_Status, BBIH_BAS_Message_ID)
	VALUES(@Reference, @Entity_Type, 0, GETDATE(), 'Data Recieved from BAS', 'Initiated', @Reference)   
END
ELSE
BEGIN
	UPDATE BMC_BAS_Import_History
	SET BBIH_Reference = @Reference,
	BBIH_AAMS_Entity_Type = @Entity_Type,
	BBIH_Status = 0,	
	BBIH_Received_Date = GETDATE(),
	BBIH_Comments = 'Data Recieved from BAS',
	BBIH_Session_Status = 'Initiated'
	WHERE BBIH_BAS_Message_ID = @Reference
END


GO

