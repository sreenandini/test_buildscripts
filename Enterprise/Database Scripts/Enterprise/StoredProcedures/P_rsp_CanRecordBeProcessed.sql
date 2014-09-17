USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_CanRecordBeProcessed]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_CanRecordBeProcessed]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--------------------------------------------------------------------------                     
--                    
-- Description: Determine if the Current EH_ID can be processed or not        
--              Rule/Condition: First check if this is the First Record, If Yes Process        
--              Else, Check if the previous Record has been Processed, If Yes Process Else Reject        
--                    
-- Inputs:      EH_ID        
--                    
-- Outputs:     NONE        
--                    
-- RETURN:      RETURNS TRUE IF RECORD CAN BE PROCESSED ELSE FALSE              
--                    
-- =======================================================================                    
--                     
-- Revision History                    
--                     
-- NaveenChander     27/05/2008     Created        
-- NaveenChander     30/08/2008     Changed Logic for Checking Previously Processed Record
---------------------------------------------------------------------------              
CREATE PROCEDURE rsp_CanRecordBeProcessed(@EH_ID INT, @IH_ID INT)        
AS        
BEGIN        
    DECLARE @RETURNSTATUS As Varchar(10)        
    DECLARE @SITE_CODE VARCHAR(50)        
    DECLARE @PreviousStatus INT        
    SELECT @SITE_CODE = IH_Site_Code FROM IMPORT_HISTORY WHERE IH_ID = @IH_ID
        
        
    -- CHECK IF IT IS NEW RECORD        
    IF EXISTS (SELECT 1 FROM IMPORT_HISTORY WHERE IH_EH_ID < @EH_ID AND IH_Site_Code = @SITE_CODE)        
    BEGIN
        SELECT @PreviousStatus = IH_Status FROM IMPORT_HISTORY WHERE IH_EH_ID = @EH_ID - 1  AND IH_Site_Code = @SITE_CODE
        SELECT @PreviousStatus = IH_Status FROM IMPORT_HISTORY WHERE IH_Site_Code = @SITE_CODE 
            AND IH_ID = (SELECT MAX(IH_ID) FROM IMPORT_HISTORY WHERE IH_Site_Code = @SITE_CODE  AND IH_ID < @IH_ID)
        -- CHECK IF Previous Record Has Been Successfully processed (Success = 100)        
        IF @PreviousStatus = 100         
        BEGIN         
            SELECT '0' AS RESULT        
            RETURN        
        END        
        ELSE        
        BEGIN    
                 SELECT '-1' AS RESULT        
            RETURN        
        END        
    END        
    ELSE        
    BEGIN        
        SELECT  '0' AS RESULT        
        RETURN        
    END        
        
            
END


GO

