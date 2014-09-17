USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_UpdateGameTitle]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_UpdateGameTitle]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 
CREATE PROCEDURE dbo.usp_UpdateGameTitle
	@Game_Category_ID INT,
	@Manufacturer_ID INT,
	@Old_Game_Title VARCHAR(100),
	@New_Game_Title VARCHAR(100),	
	@IsMultiGame BIT
AS
/*****************************************************************************************************
DESCRIPTION : Update the Game Title in Game_Title & its corresponding Machine_Class table  
CREATED DATE: 
MODULE      : Game Liberary
CHANGE HISTORY :
----------------------------------------------------------------------------------------------------
AUTHOR						MODIFIED DATE		DESCRIPTON
----------------------------------------------------------------------------------------------------

*****************************************************************************************************/
BEGIN
	DECLARE @ID AS INT
	
	SET NOCOUNT ON
	
	SET @Old_Game_Title = LTRIM(RTRIM(@Old_Game_Title))
	SET @New_Game_Title = LTRIM(RTRIM(@New_Game_Title))
	
	UPDATE Game_Title
	SET    Game_Category_ID = @Game_Category_ID,
	       Game_Title = @New_Game_Title,
	       @ID = Game_Title_ID,
	       Manufacturer_ID = @Manufacturer_ID,
	       IsMultiGame = @IsMultiGame	 
	WHERE  Game_Title = @Old_Game_Title
	
	UPDATE MeterAnalysis.dbo.Game_Title
	SET    Game_Category_ID = @Game_Category_ID,
	       Game_Title = @New_Game_Title,
	       @ID = Game_Title_ID,
	       Manufacturer_ID = @Manufacturer_ID,
	       IsMultiGame = @IsMultiGame	 	       	       
	WHERE  Game_Title = @Old_Game_Title
	
	EXEC usp_InsertExportHistory @ID,
	     'GAMETITLE',
	     'ALL'
	
	EXEC usp_InsertExport_Data @ID,  
	'GAMETITLE',  
	@New_Game_Title,  
	@Old_Game_Title               
	
	UPDATE Machine_Class
	SET    Machine_Class_Model_Code = @New_Game_Title,
	       Machine_Name = @New_Game_Title
	WHERE  Machine_Class_Model_Code = @Old_Game_Title
	
	UPDATE MeterAnalysis.dbo.Machine_Class
	SET    Machine_Class_Model_Code = @New_Game_Title,
	       Machine_Name = @New_Game_Title
	WHERE  Machine_Class_Model_Code = @Old_Game_Title
		
END

GO

