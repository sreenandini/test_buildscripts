USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_UpdateBASBMCImportStatus]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_UpdateBASBMCImportStatus]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =================================================================  
-- usp_UpdateBASBMCImportStatus  
-- -----------------------------------------------------------------  
--  
-- Updates the status of the current record in the BMC_BAS_Import_History.    
--   
-- -----------------------------------------------------------------      
-- Revision History         
--         
-- 19/11/09 Renjish Created        
-- =================================================================   
  
CREATE PROCEDURE dbo.usp_UpdateBASBMCImportStatus  
@MessageID varchar(20), 
@Status INT,
@Comments varchar(200),
@MessageStatus varchar(50)
AS  
  
DECLARE @Session_Status VARCHAR(50)
SET @Session_Status = 'Failed'
IF @Status = 100
BEGIN
	SET @Session_Status = 'Processed'
END

UPDATE BMC_BAS_Import_History   
SET   
BBIH_Status = @Status,  
BBIH_Imported_Date = GetDate(),
BBIH_Comments =   SUBSTRING(@Comments,1,98),
BBIH_Message_Status = @MessageStatus,
BBIH_Session_Status = @Session_Status
WHERE BBIH_BAS_Message_ID = @MessageID  


GO

