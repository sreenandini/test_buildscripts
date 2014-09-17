USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_GetGameCategoryGL]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_GetGameCategoryGL]
GO

  
CREATE PROCEDURE [dbo].[rsp_GetGameCategoryGL]    
 @Game_Category_Name VARCHAR(50) = NULL  ,  
 @GameName VARCHAR(50) = NULL  
AS          
BEGIN       
      
 SET NOCOUNT ON    
 SELECT * FROM Game_Category     
 WHERE @Game_Category_Name IS NULL OR @Game_Category_Name = 'All'   
 OR UPPER(LTRIM(RTRIM(Game_Category_Name))) = UPPER(LTRIM(RTRIM(@Game_Category_Name)))    
 ORDER BY Game_Category_Name    
    
END 