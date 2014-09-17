USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_UpdateBMCBASExportStatus]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_UpdateBMCBASExportStatus]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =================================================================  
-- usp_UpdateBMCBASExportStatus  
-- -----------------------------------------------------------------  
--  
-- Updates the status of the records in the BMC_BAS_Export_History.    
--   
-- -----------------------------------------------------------------      
-- Revision History         
--         
-- 19/11/09 Renjish Created        
-- =================================================================   
  
CREATE PROCEDURE dbo.usp_UpdateBMCBASExportStatus  
@EH_ID INT,  
@Status INT,
@Comments varchar(200),
@BAS_Message_ID varchar(20)
AS  

DECLARE @REQSTATUS VARCHAR(50)
SET @REQSTATUS = 'Failed'
IF @Status = 100
BEGIN
SET @REQSTATUS = 'Processed'
END

UPDATE BMC_BAS_Export_History   
SET   
BBEH_Status = @Status,  
BBEH_Exported_Date = GetDate(),
BBEH_Comments =   SUBSTRING(@Comments,1,98),
BBEH_Session_Status = @REQSTATUS
WHERE BBEH_ID = @EH_ID  

IF LEN(isnull(@BAS_Message_ID,'')) > 0
BEGIN
	UPDATE BMC_BAS_Export_History   
	SET BBEH_BAS_Message_ID = @BAS_Message_ID
	WHERE BBEH_ID = @EH_ID 
END


GO

