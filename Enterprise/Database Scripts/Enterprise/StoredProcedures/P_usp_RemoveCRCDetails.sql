USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_RemoveCRCDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_RemoveCRCDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- Insert a new record into CRC table.      
-- -----------------------------------------------------------------        
-- Revision History   --exec usp_RemoveCRCDetails '0010'        
--           
-- 08/02/2010 Vineetha Mathew Created          
-- =================================================================     
    
CREATE PROCEDURE dbo.usp_RemoveCRCDetails      
 @CRC VARCHAR(20),  
 @Return INT OUTPUT  
AS    
  
BEGIN   
 IF  EXISTS (SELECT CRC FROM Game_CRCDetails WHERE CRC = @CRC)  
  BEGIN  
   DELETE FROM Game_CRCDetails WHERE CRC = @CRC
  SET @Return=0  
  END  
 ELSE  
  SET @Return=1  
RETURN @Return  
END  
  
GO

