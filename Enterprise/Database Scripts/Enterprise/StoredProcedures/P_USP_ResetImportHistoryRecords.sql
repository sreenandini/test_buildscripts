USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[USP_ResetImportHistoryRecords]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[USP_ResetImportHistoryRecords]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-----------------------------------------------------------------------------------------------------------------------------------------------------                                 
-- Description: Reset Unprocessed Records if there is a Break in FIFO Logic    
--                                
-- Inputs:      @SiteCode:     
--              ALL (OPTIONAL PARAMETER) = Reset to New Records for All Site    
--              SiteCoder =                 Reset to New Record for Specific Site    
--                                
-- Outputs:     NONE    
--                                
-- RETURN:      NONE                          
--                                
-- ==================================================================================================================================================                                
--                                 
-- Revision History                                
--                                 
-- NaveenChander     06/08/2008     Created        
-----------------------------------------------------------------------------------------------------------------------------------------------------                        
CREATE Procedure USP_ResetImportHistoryRecords(@SiteCode Varchar(50) = 'ALL')    
As    
BEGIN    
If @SiteCode = 'ALL'    
BEGIN    
    UPDATE IMPORT_HISTORY SET IH_Status = 0 WHERE IH_Status = 1    
END    
ELSE    
BEGIN    
    UPDATE IMPORT_HISTORY SET IH_Status = 0 WHERE IH_Status = 1 AND IH_Site_Code = @SiteCode    
END    
END 

GO

