USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_MapGames]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_MapGames]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE dbo.usp_MapGames
	@GroupId VARCHAR(10),
	@Game_Ids VARCHAR(MAX)
AS
	/*****************************************************************************************************
DESCRIPTION : PROC Description  
CREATED DATE: 30 - 11 - 2012
MODULE      : PROC used in Modules      
CHANGE HISTORY :
EXAMPLE		: 

------------------------------------------------------------------------------------------------------
AUTHOR                              DESCRIPTON                                                        MODIFIED DATE
------------------------------------------------------------------------------------------------------

*****************************************************************************************************/
BEGIN
	DECLARE @GameCategoryID VARCHAR(10)	      
	
	IF (@GroupId = 0)
	BEGIN
	    UPDATE Game_Library
	    SET    MG_Group_ID = 1,
	           MG_Game_CategoryID = 1
	    WHERE  MG_Game_ID IN (SELECT STR FROM dbo.iter_charlist_to_tbl(@Game_Ids,','))
	    
	     UPDATE MeterAnalysis.dbo.Game_Library
	    SET    MG_Group_ID = 1,
	           MG_Game_CategoryID = 1
	    WHERE  MG_Game_ID IN (SELECT STR FROM dbo.iter_charlist_to_tbl(@Game_Ids,','))
	END
	ELSE
	BEGIN
	    SELECT @GameCategoryID = Game_Category_ID
	    FROM   Game_Title WITH(NOLOCK)
	    WHERE  Game_Title_ID = @GroupId
	    
	    UPDATE Game_Library
	    SET    MG_Group_ID = @GroupId,
	           MG_Game_CategoryID = @GameCategoryID
	    WHERE  MG_Game_ID IN (SELECT STR FROM dbo.iter_charlist_to_tbl(@Game_Ids,','))
	    
	    UPDATE MeterAnalysis.dbo.Game_Library
	    SET    MG_Group_ID = @GroupId,
	           MG_Game_CategoryID = @GameCategoryID
	    WHERE  MG_Game_ID IN (SELECT STR FROM dbo.iter_charlist_to_tbl(@Game_Ids,','))
	END

	INSERT INTO Export_History  
       	(  
         	EH_Date,  
         	EH_Reference1,  
         	EH_Type,  
         	EH_Site_Code  
       	)  
       
     	SELECT GETDATE(),  
            	STR,  
            	'GAMELIBRARY_MAPPING',  
            	Site_Code  
     	FROM   dbo.iter_charlist_to_tbl(@Game_Ids,',')
            	CROSS APPLY SITE   
     	WHERE STR > 0
END

GO

