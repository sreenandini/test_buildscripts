USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[USP_UpdateImportHistoryProcessDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[USP_UpdateImportHistoryProcessDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--------------------------------------------------------------------------               
--              
-- Description: Update process details for processed records      
--              
-- Inputs:      @Result: Success/Failure to be updated to IH_ExportResult      
--              @ID:     ID Value against which records are updated      
--              @Status: indicated if Record is processed or not      
--      
-- Outputs:     NONE      
--              
-- RETURN:      NONE        
--              
-- =======================================================================              
--               
-- Revision History              
--               
-- NaveenChander     27/05/2008     Created 
-- Siva				 18/09/2008     Added transaction to avoid deadlock				              
---------------------------------------------------------------------------        
CREATE PROCEDURE [dbo].[USP_UpdateImportHistoryProcessDetails] (@ID INT, @Result VARCHAR(8000), @Status INT)    
AS
BEGIN      
   BEGIN TRAN 
    UPDATE     
        IMPORT_HISTORY    
    SET    
         IH_Status = @Status,    
         IH_ExportResult = @Result,    
         IH_Processed_Date = Getdate()    
    WHERE    
         IH_ID = @ID 

	IF @@ERROR <> 0
		ROLLBACK TRAN
	ELSE
		COMMIT TRAN   
      
END

GO

