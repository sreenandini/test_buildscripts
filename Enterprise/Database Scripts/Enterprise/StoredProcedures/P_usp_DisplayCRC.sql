USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_DisplayCRC]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_DisplayCRC]
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
-- Revision History   --exec usp_DisplayCRC 1,'0010'      
--         
-- 19/11/09 Vineetha Mathew Created        
-- =================================================================   
  
CREATE PROCEDURE dbo.usp_DisplayCRC  
	@GameID INT=0,
	@CRC varchar(20)=''
AS  
 
 IF @GameID = 0	SET @GameID = NULL 

SELECT 
	Game_ID as MG_Game_ID,
	CRC as MG_CRC,
	Seed as MG_Seed
FROM dbo.Game_CRCDetails 
WHERE 	
	( ( @GameID IS NULL )OR ( @GameID IS NOT NULL AND Game_ID = @GameID ))  	  


GO

