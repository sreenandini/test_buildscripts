USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[udf_getGames]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[udf_getGames]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION [dbo].[udf_getGames]
(
	@GameTitleID     INT = 0,
	@GameCatID       INT = 0,
	@ManufacturerID  INT = 0,
	@GameCategoryID  INT = 0
)
RETURNS @Games TABLE (
            MG_Game_ID INT,
            GameName VARCHAR(100),
            SerialNo INT,
            Manufacturer VARCHAR(MAX),
            Alias_Machine_Name VARCHAR(MAX),
            CategoryID INT,
            Category VARCHAR(50),
            GroupID INT,
            Installation_ID INT
        )
AS
BEGIN
	DECLARE @GameId            INT
	DECLARE @GameName          VARCHAR(100)
	DECLARE @Version           VARCHAR(30)
	DECLARE @SerialNo          INT
	DECLARE @InstallationId    INT
	DECLARE @ManufacturerName  VARCHAR(MAX)
	DECLARE @StockNo           VARCHAR(MAX)
	DECLARE @CategoryID        INT
	DECLARE @Category          VARCHAR(50)
	DECLARE @GroupID           INT
	
	DECLARE @GamesTable        TABLE
	        (
	            MG_Game_ID INT,
	            GameName VARCHAR(100),
	            SerialNo INT,
	            Manufacturer VARCHAR(MAX),
	            Alias_Machine_Name VARCHAR(MAX),
	            CategoryID INT,
	            Category VARCHAR  (50),
	            GroupID           INT,
	            InstallationId    INT
	        )
	
	DECLARE Games                 CURSOR LOCAL 
	FOR
	    SELECT DISTINCT 
	           GL.MG_Game_ID AS MG_Game_ID,
	           GL .MG_Game_Name AS GameName,
	           CASE 
	                WHEN ISNULL(GL.MG_Game_SerialNo, '') = '' THEN 0
	                ELSE GL.MG_Game_SerialNo
	           END AS SerialNo,
	           ma.Manufacturer_Name AS Manufacturer,
	           M.Machine_Stock_No AS Alias_Machine_Name,
	           GC.Game_Category_ID AS CategoryID,
	           GC.Game_Category_Name AS Category,
	           GL.MG_Group_ID AS GroupID,
	           Installation_ID
	    FROM   Installation_game_info IG WITH(NOLOCK)
	           LEFT OUTER JOIN Game_Library GL WITH(NOLOCK)
	                ON  LTRIM(RTRIM(IG.Game_Name)) = LTRIM(RTRIM(GL.MG_Game_Name))	                
	           LEFT OUTER JOIN Game_Title GT WITH(NOLOCK)
	                ON  GT.Game_Title_ID = GL.MG_Group_ID
	           LEFT OUTER JOIN Installation I WITH(NOLOCK)
	                ON  IG.Installation_No = I.Installation_ID
	           LEFT OUTER JOIN MACHINE M WITH(NOLOCK)
	                ON  M.Machine_ID = I.Machine_ID
	           LEFT OUTER JOIN Machine_Class Mc WITH(NOLOCK)
	                ON  M.Machine_Class_ID = Mc.Machine_Class_ID
	           LEFT OUTER JOIN Manufacturer ma WITH(NOLOCK)
	                ON  ma.Manufacturer_ID = mc.Manufacturer_ID
	           LEFT OUTER JOIN Game_Category GC WITH(NOLOCK)
	                ON  GC.Game_Category_ID = GL.MG_Game_CategoryID
	    WHERE  (
	               ma.Manufacturer_ID = @ManufacturerID
	               OR @ManufacturerID = 0
	           )
	           AND (
	                   GT.Game_Category_ID = @GameCategoryID
	                   OR @GameCategoryID = 0
	               )
	           AND (
	                   (@GameTitleID IS NULL)
	                   OR (@GameTitleID IS NOT NULL AND GL.MG_Group_ID = @GameTitleID)
	               )
	           AND (
	                   (@GameCatID IS NULL)
	                   OR (
	                          @GameCatID IS NOT NULL
	                          AND GT.Game_Category_ID = @GameCatID
	                      )
	               )
	           OR  (@GameCatID = -1 AND GL.MG_Game_CategoryID IS NULL)
	
	OPEN Games
	
	FETCH NEXT FROM Games 
	INTO @GameId, @GameName , @SerialNo, @ManufacturerName, @StockNo, @CategoryID, 
	@Category, @GroupID, @InstallationId
	
	WHILE @@FETCH_STATUS = 0
	BEGIN
	    IF EXISTS(
	           SELECT 1
	           FROM   @GamesTable
	           WHERE  MG_Game_ID = @GameId
	       )
	    BEGIN
	        UPDATE @GamesTable
	        SET    Manufacturer = CASE 
	                                   WHEN Manufacturer LIKE @ManufacturerName         
										OR Manufacturer LIKE '%,' + @ManufacturerName + '%'
									    OR Manufacturer LIKE '%' + @ManufacturerName + ',%'                                      
                                       THEN Manufacturer        
                                       ELSE
                                        Manufacturer + ',' + @ManufacturerName   
	                              END,
	               Alias_Machine_Name = CASE 
	                                         WHEN Alias_Machine_Name LIKE @StockNo 
	                                              OR Alias_Machine_Name LIKE 
	                                              '%,' +
	                                              @StockNo 
	                                              OR Alias_Machine_Name LIKE @StockNo 
	                                              + ',%' THEN Alias_Machine_Name
	                                         ELSE Alias_Machine_Name + ',' + @StockNo
	                                    END
	        WHERE  MG_Game_ID = @GameId
	    END
	    ELSE
	    BEGIN
	        INSERT INTO @GamesTable
	        SELECT @GameId,
	               @GameName,
	               @SerialNo,
	               @ManufacturerName,
	               @StockNo,
	               @CategoryID,
	               @Category,
	               @GroupID,
	               @InstallationId
	    END
	    
	    FETCH NEXT FROM Games 
	    INTO @GameId, @GameName , @SerialNo, @ManufacturerName, @StockNo, @CategoryID, 
	    @Category, @GroupID, @InstallationId
	END 
	CLOSE Games
	DEALLOCATE Games
	
	INSERT @Games
	SELECT MG_Game_ID,
	       GameName,
	       SerialNo,
	       Manufacturer,
	       Alias_Machine_Name,
	       CategoryID,
	       Category,
	       GroupID,
	       InstallationId
	FROM   @GamesTable
	ORDER BY
	       GameName
	
	RETURN
END

GO

