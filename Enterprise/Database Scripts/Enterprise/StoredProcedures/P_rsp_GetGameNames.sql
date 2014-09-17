USE Enterprise
GO


IF EXISTS (SELECT 1 FROM sys.objects o WHERE NAME='rsp_GetGameNames' AND o.[type]='P')
BEGIN
	DROP PROCEDURE rsp_GetGameNames
END

GO 
CREATE  PROCEDURE rsp_GetGameNames    
    
AS    
BEGIN        
SELECT   
MG_Game_ID,  
MG_Game_Name+', '+M.Manufacturer_Name AS MG_Game_Name from   
Game_Library GL
inner join 
Manufacturer M
ON M.Manufacturer_ID = GL.MG_Game_Manufacturer_ID
ORDER BY MG_Game_Name       
END
GO