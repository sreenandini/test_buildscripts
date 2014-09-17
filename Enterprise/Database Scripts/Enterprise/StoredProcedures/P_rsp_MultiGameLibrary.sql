USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_MultiGameLibrary]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_MultiGameLibrary]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[rsp_MultiGameLibrary]                
(    
@Installation_id INT =0  
)    
AS         
  
SET DATEFORMAT dmy  
  
BEGIN    

   IF @Installation_id = 0       
		SET @Installation_id = NULL            

   SELECT  
	MG_Group_ID, 
     GT.Game_Title AS MG_Alias_Game_Name   
	,GC.Game_Category_Name AS Game_Category_Name        
    ,CASE WHEN M.Manufacturer_Name IS NULL THEN 'Not Available' WHEN M.Manufacturer_Name = '' THEN 'Not Available' ELSE M.Manufacturer_Name END AS Manufacturer    
   FROM Installation I 
	INNER JOIN installation_game_info IGI 
		ON IGI.Installation_No = I.Installation_ID 
	INNER JOIN Game_Library GL 
		ON  GL.MG_Game_ID = IGI.IGI_Game_ID
	INNER JOIN Game_Title GT
		ON GT.Game_Title_ID = GL.MG_Group_ID
    LEFT JOIN Manufacturer M 
		ON M.Manufacturer_ID = GT.Manufacturer_ID   
    LEFT JOIN Game_Category GC ON GC.Game_Category_ID = GL.MG_Game_CategoryID       

   WHERE (( @Installation_id IS NULL )OR ( @Installation_id IS NOT NULL AND i.Installation_id = @Installation_id ))        
	
   GROUP BY MG_Group_ID,GT.Game_Title,M.Manufacturer_Name,GC.Game_Category_Name

END   
  



GO