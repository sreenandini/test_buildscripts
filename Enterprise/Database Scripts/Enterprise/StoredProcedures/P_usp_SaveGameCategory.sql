USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_SaveGameCategory]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_SaveGameCategory]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-------------------------------------------------------------------------- 
--
-- Description: Save the Category details
-- Inputs:      See inputs 
--
-- Outputs:         
--                  
-- =======================================================================
-- 
-- Revision History
--
-- Sudarsan S		03/12/2009		Created
-- Saravanakumar P	19/Sep/2010		Issues Fixed
------------------------------------------------------------------------------------------------------
CREATE PROCEDURE dbo.usp_SaveGameCategory  
 @Category_ID INT = 0,  
 @Category_Name VARCHAR(50)  
AS  
  
BEGIN
   
   DECLARE @IDENTITY INT
   
   IF (@Category_ID > 0) 
		BEGIN
			IF NOT EXISTS(SELECT 1 FROM Game_Category WHERE Game_Category_Name = @Category_Name AND Game_Category_ID <> @Category_ID)
			BEGIN
				
				SET @IDENTITY =   @Category_ID
				
				UPDATE dbo.Game_Category 
				SET Game_Category_Name = @Category_Name 
				WHERE Game_Category_ID = @Category_ID
				
				UPDATE MeterAnalysis.dbo.Game_Category 
				SET Game_Category_Name = @Category_Name 
				WHERE Game_Category_ID = @Category_ID
			END
			ELSE
				BEGIN
					RETURN -1
				END
		END
   ELSE 
		BEGIN
			IF NOT EXISTS(SELECT 1 FROM Game_Category WHERE Game_Category_Name = @Category_Name)
				BEGIN    
					INSERT INTO dbo.Game_Category (Game_Category_Name) VALUES(@Category_Name)
					INSERT INTO MeterAnalysis.dbo.Game_Category (Game_Category_Name) VALUES(@Category_Name) 					   
					SET @IDENTITY = SCOPE_IDENTITY()					    
				END    			
			ELSE    
			BEGIN    
				RETURN -1
			END    
	END
	
	EXEC usp_InsertExportHistory @IDENTITY,'GAMECATEGORY','ALL'
END  

GO

