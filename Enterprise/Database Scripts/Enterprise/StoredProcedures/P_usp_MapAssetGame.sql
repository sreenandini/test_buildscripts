USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_MapAssetGame]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_MapAssetGame]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*      
 *Purpose: To map assets with games    
 * Change History: exec usp_MapAssetGame 2,2
 * Vineetha  16-02-2009  created       
*/
CREATE PROCEDURE dbo.usp_MapAssetGame
	@GameID		INT,
	@MachineID	INT
AS


	DECLARE @AssetGameMapID INT
	SELECT @AssetGameMapID=ISNULL(AssetGameMap_ID,0) FROM AssetGameMap WHERE  MachineID=@MachineID AND Master_Game_ID=@GameID

	IF (@AssetGameMapID<=0 OR @AssetGameMapID IS NULL)
		BEGIN
			INSERT INTO [AssetGameMap]
				   ([Master_Game_ID]
				   ,[MachineID]
				   ,[AssetGameMap_StartDate]
				   ,[AssetGameMap_EndDate])
			 VALUES
				   (@GameID
				   ,@MachineID
				   ,getdate()
				   ,getdate())
		END
	ELSE 
		BEGIN			
			DELETE FROM AssetGameMap WHERE AssetGameMap_ID=@AssetGameMapID
		END




GO

