USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_MultiGameLibraryThemes]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_MultiGameLibraryThemes]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
  
    
/*    
 * this stored procedure is to fetch the multi game details.    
 *    
 * Change History:    
 *     
 * Vineetha  16-02-2009  created    
 * Renjish   11-11-2009  Modified to get the Manufactorer name from Manufactorer table.    
 * Vineetha  13-11-2009  Modified to link between GameMaster & [Game_Library]  
 * Vineetha  24-11-2009  Modified to check  GM.Master_Game_EndDate IS NULL
 * Vineetha  05-12-2009  Modified to remove game_master  
 * Vineetha  14-12-2009  Modified to include left join for manufacture instead of inner join  
*/    
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
  
CREATE PROCEDURE [dbo].[rsp_MultiGameLibraryThemes]            
(  
@GameID INT = 0,
@GameCatID INT = 0  
)  
AS       

SET DATEFORMAT dmy

  BEGIN  	   
	   IF @GameID = 0	 SET @GameID = NULL   
	   IF @GameCatID = 0 SET @GameCatID = NULL

		 SELECT   
		  DISTINCT
		  --GM.MG_Game_ID AS MG_Game_ID  
		  --,CASE WHEN ISNULL(GM.MG_Game_Name,'')='' THEN 'N/a' ELSE GM.MG_Game_Name END AS MG_Game_Name 
		  GT.Game_Title AS AliasGameName 
		  ,GM.MG_Game_StartDate AS ReleaseDate    
		  ,GM.MG_Game_Version AS Version  
		  --,GM.MG_Game_SerialNo AS SerialNo  
		  ,PT.[PT_Description] AS Paytable  
		  ,M.Manufacturer_Name AS Manufacturer 
		  ,PT.Paytable_ID AS PaytableID
		  ,PT.[Payout] AS Payout 
		  ,GC.Game_Category_ID AS CategoryID   
		  ,GC.Game_Category_Name AS Category 
		  ,GM.MG_Group_ID AS GroupID 
		FROM Game_Library GM   						 
			LEFT JOIN Game_Title GT ON GT.Game_Title_ID = GM.MG_Group_ID
			LEFT JOIN Manufacturer M ON M.Manufacturer_ID = GT.Manufacturer_ID 
			LEFT JOIN [PayTable] PT ON PT.Game_ID = GM.MG_Game_ID 
			LEFT JOIN Game_Category GC on GC.Game_Category_ID = GM.MG_Game_CategoryID     
		WHERE 	
			( ( @GameID IS NULL )OR ( @GameID IS NOT NULL AND GM.MG_Game_ID = @GameID ))  
			AND 
			( ( @GameCatID IS NULL )OR ( @GameCatID IS NOT NULL AND GM.MG_Game_CategoryID = @GameCatID )) 
			OR
			(@GameCatID=-1 and GM.MG_Game_CategoryID is null) 	  
		ORDER BY AliasGameName
END 


GO

