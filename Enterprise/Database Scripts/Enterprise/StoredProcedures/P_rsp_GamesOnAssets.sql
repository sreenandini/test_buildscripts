USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GamesOnAssets]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GamesOnAssets]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*        
 *Purpose: this stored procedure is to fetch games on assets       
 * Change History: exec rsp_GamesOnAssets 2,0   
 * Vineetha  16-02-2009  created         
*/         
      
CREATE PROCEDURE [dbo].rsp_GamesOnAssets
(   
 @MachineID AS INT,  
 @Include AS INT  
)      
AS           
    
SET DATEFORMAT dmy  
  
IF (@Include=0)
	BEGIN  
		SELECT 
			G.MG_Game_ID AS MG_Game_ID  
			,G.MG_Game_Name AS MG_Game_Name  
		--FROM GameMaster G  
		 FROM Game_Library G
			LEFT JOIN dbo.AssetGameMap AGM ON G.MG_Game_ID = AGM.Master_Game_ID   
			AND AGM.MachineID = @MachineID  
		WHERE AGM.Master_Game_ID IS NULL --AND G.Master_Game_EndDate IS NULL      
	END  
ELSE IF (@Include=1) 
	BEGIN   
	SELECT 
		G.MG_Game_ID AS MG_Game_ID  
		,G.MG_Game_Name AS MG_Game_Name  
	FROM AssetGameMap AGM  
		--INNER JOIN dbo.GameMaster G ON AGM.Master_Game_ID = G.Master_Game_ID AND AGM.MachineID = @MachineID  
		INNER JOIN dbo.Game_Library G ON AGM.Master_Game_ID = G.MG_Game_ID AND AGM.MachineID = @MachineID  
	--WHERE  G.Master_Game_EndDate IS NULL      
	END  


GO

