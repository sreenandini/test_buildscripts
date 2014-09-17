USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_MultiGameLibraryThemesDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_MultiGameLibraryThemesDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


  
    
/*    
 * this stored procedure is to fetch the multi game details.    
 *    
 * Change History:    
 *     
 * Vineetha  16-02-2009  created    rsp_MultiGameLibraryThemesDetails 0, 0
 * Renjish   11-11-2009  Modified to get the Manufactorer name from Manufactorer table.    
 * Vineetha  13-11-2009  Modified to link between GameMaster & [Game_Library]  
 * Vineetha  24-11-2009  Modified to check  GM.Master_Game_EndDate IS NULL
 * Vineetha  05-12-2009  Modified to remove game_master  
 * Vineetha  14-12-2009  Modified to include left join for manufacture instead of inner join  
 * SaravanaP 26-11-2010  Added Manufacture in Where condition
*/    
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
  
CREATE PROCEDURE [dbo].rsp_MultiGameLibraryThemesDetails
(
    @GameTitleID            INT = 0,
    @GameCatID              INT = 0,
    @ManufacturerID         INT = 0,
    @GameCategoryID         INT = 0,
    @MachineManufacturerID  INT = 0,
    @GameName               VARCHAR(250) = 'ALL'
)
AS
	SET DATEFORMAT dmy         
	BEGIN
		DECLARE @Game_Category_Name  VARCHAR(50)
		DECLARE @Manufacturer        VARCHAR(50)
		DECLARE @GameTitle           VARCHAR(250)
		
		CREATE TABLE #tmpGameTitle
		(
			Game_Title_ID       INT,
			Game_Title          VARCHAR(250),
			Game_Category_ID    INT,
			Game_Category_Name  VARCHAR(50),
			Manufacturer_Name   VARCHAR(50)
		)
		
		CREATE TABLE #tmpMachineGame
		(
			MG_Game_ID          INT,
			GameName            VARCHAR(250),
			Manufacturer        VARCHAR(MAX),
			Alias_machine_Name  VARCHAR(MAX),
			MG_Group_ID			INT
		)
		
		IF @GameTitleID = 0    
		SET @GameTitleID = NULL 
		
		
		
		IF @GameCatID = 0    
		BEGIN
			SET @GameCatID = NULL  
			SET @Game_Category_Name = 'ALL'
		END
		ELSE
		BEGIN
			SELECT @Game_Category_Name = Game_Category_Name
			FROM   Game_Category
			WHERE  Game_Category_ID = @GameCategoryID
		END
		
		IF @ManufacturerID = 0
		BEGIN
			SET @Manufacturer = 'ALL'
		END
		ELSE
		BEGIN
			SELECT @Manufacturer = Manufacturer_Name
			FROM   Manufacturer
			WHERE  Manufacturer_ID = @ManufacturerID
		END
		
		--Get all game title along with category
		INSERT INTO #tmpGameTitle
		SELECT tGT.Game_Title_ID,
		       tGT.Game_Title,
		       tGC.Game_Category_ID,
		       tGC.Game_Category_Name,
		       tM.Manufacturer_Name
		FROM   Game_Title tGT
		       LEFT JOIN game_Category tGC
		            ON  tGT.Game_Category_ID = tGC.Game_Category_ID
		       LEFT JOIN Manufacturer tM
		            ON  tM.Manufacturer_ID = tGT.Manufacturer_ID
		WHERE  (
		           @Game_Category_Name = 'All'
		           OR Game_Category_Name = @Game_Category_Name
		       )
		       AND (@Manufacturer = 'All' OR Manufacturer_Name = @Manufacturer)

		--Get all machine game name along with manufacture and machine details
		INSERT INTO #tmpMachineGame
		SELECT MG_Game_ID,
		       GameName,
		       Manufacturer,
		       [Alias_Machine_Name],
		       MG_Group_ID
		FROM   VW_GameThemeDetail
		WHERE  (
		           Manufacturer_ID = @MachineManufacturerID
		           OR @MachineManufacturerID = 0
		       )
		       AND (GTCategoryID = @GameCategoryID OR @GameCategoryID = 0)
		       AND (
		               (@GameTitleID IS NULL)
		               OR (
		                      @GameTitleID IS NOT NULL
		                      AND MG_Group_ID = @GameTitleID
		                  )
		           )
		       AND (
		               (@GameCatID IS NULL)
		               OR (@GameCatID IS NOT NULL AND GTCategoryID = @GameCatID)
		           )
		       OR  (@GameCatID = -1 AND CategoryID IS NULL)		

		--List the machine game	names based on game tile and its category
		SELECT #tmpMachineGame.* 
		FROM   #tmpGameTitle
		       INNER JOIN #tmpMachineGame
		            ON  #tmpGameTitle.Game_Title_ID = #tmpMachineGame.MG_Group_ID
		WHERE  #tmpMachineGame.GameName = @GameName
		       OR  @GameName = 'ALL'
	END
GO

