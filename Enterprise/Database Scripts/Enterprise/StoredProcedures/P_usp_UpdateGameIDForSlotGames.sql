USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_UpdateGameIDForSlotGames]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_UpdateGameIDForSlotGames]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--------------------------------------------------------------------------------------------------------------   
--  
-- Description: Checks to see if the incoming event is worthy of a service call entry  
--              if so auto generates the call, applying group etc.  
--      
-- Inputs:      See inputs   
-- Outputs:     NONE  
--  
-- Revision History
--
--	Sudarsan S		12/11/2009		Created
-- =============================================================================================================  
CREATE PROCEDURE dbo.usp_UpdateGameIDForSlotGames    
 @GroupId  VARCHAR(10),        
 @PaytableIDs VARCHAR(MAX)  
AS        
BEGIN    
    
 DECLARE @Query NVARCHAR(2000)    
 DECLARE @Delimeter CHAR(1)    
 DECLARE @GameCategoryID VARCHAR(10)    
 DECLARE @ManufactureID VARCHAR(10)    
 DECLARE @GameID VARCHAR(10)        
 DECLARE @StartPos INT, @Length INT    
     
    
 IF (@GroupId = 0)    
  BEGIN      
   SET @Query = 'UPDATE Game_Library SET MG_Group_ID = 1, MG_Game_CategoryID = 1 WHERE MG_Game_ID IN (' + @PaytableIDs + ')'   
   SET @Query += 'UPDATE MeterAnalysis.dbo.Game_Library SET MG_Group_ID = 1, MG_Game_CategoryID = 1 WHERE MG_Game_ID IN (' + @PaytableIDs + ')'    
  END      
  ELSE    
  BEGIN    
   SELECT @GameCategoryID = Game_Category_ID FROM Game_Title WHERE Game_Title_ID = @GroupId    
   SET @Query = 'UPDATE Game_Library SET MG_Group_ID = ' + @GroupId + ', MG_Game_CategoryID = ' + @GameCategoryID + ' WHERE MG_Game_ID IN (' + @PaytableIDs + ')'
   SET @Query += 'UPDATE MeterAnalysis.dbo.Game_Library SET MG_Group_ID = ' + @GroupId + ', MG_Game_CategoryID = ' + @GameCategoryID + ' WHERE MG_Game_ID IN (' + @PaytableIDs + ')'    
  END    
  EXEC SP_EXECUTESQL @Query    

  DECLARE @Count INT  
  DECLARE @Current INT 
  DECLARE @PaytableID INT

  SELECT @Count = COUNT(Data) FROM dbo.fnSplit(@PaytableIDs,',')
  SET @Current = 2    
  WHILE (@Current <= @Count)  
	BEGIN  
		SELECT @PaytableID = CAST(Data AS INT) FROM dbo.fnSplit(@PaytableIDs,',') WHERE id = @Current  
		EXEC usp_InsertExportHistory @PaytableID,'GAMELIBRARY_MAPPING','ALL'
		SET @Current = @Current + 1  
	END
  

END    







GO

