USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_InsertCRC]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_InsertCRC]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =================================================================  --  
-- Insert a new record into CRC table.    
-- -----------------------------------------------------------------      
-- Revision History   --exec usp_InsertCRC 1,'0010'      
--         
-- 19/11/09 Vineetha Mathew Created        
-- =================================================================   
  
CREATE PROCEDURE dbo.usp_InsertCRC  
	@GameID INT,
	@CRC VARCHAR(20),
	@Return INT OUTPUT
AS  

BEGIN 
	IF NOT EXISTS (SELECT CRC FROM Game_CRCDetails WHERE CRC = @CRC)
		BEGIN
			INSERT INTO Game_CRCDetails(Game_ID,CRC,Seed)
			VALUES(@GameID, @CRC, '01')   
		SET @Return=0
		END
	ELSE
		SET @Return=1
RETURN @Return
END


GO

