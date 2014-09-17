USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FnGetGameNameforAsset]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[FnGetGameNameforAsset]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION dbo.[FnGetGameNameforAsset]      
 (@Asset varchar(50))        
  RETURNS VARCHAR(50)      
AS        
BEGIN        
 DECLARE @Installation_Id AS INT    
 DECLARE @GameCount AS INT      
 DECLARE @Return AS VARCHAR(50)      
       
SELECT     
 @Installation_Id= Installation_Id
FROM     
 Installation     
WHERE      
 Machine_Id in (SELECT Machine_Id FROM machine WHERE Machine_Stock_No=@Asset)    
 AND  Installation.Installation_End_Date  IS NULL      
      
IF @Installation_Id>0    
BEGIN    
  SELECT       
   @GameCount = COUNT(*)       
  FROM       
   Installation I      
   INNER JOIN Installation_Game_Info IGI       
  ON       
   I.Installation_Id = IGI.Installation_No      
  WHERE       
   I.Installation_Id = @Installation_Id
   AND I.Installation_End_Date  IS NULL
       
  IF @GameCount > 1      
   BEGIN      
    SET @Return = 'Multi Game'      
   END      
  ELSE      
   BEGIN      
    SELECT       
  @Return = Game_Title      
    FROM       
  Installation I      
  INNER JOIN Installation_Game_Info IGI ON I.Installation_Id = IGI.Installation_No
  INNER JOIN Game_Library GL ON IGI.Game_Name = GL.MG_Game_Name AND IGI.Game_Part_Number = GL.Game_Part_Number      
  INNER JOIN Game_Title GT ON GL.MG_Group_ID = GT.Game_Title_ID      
    WHERE       
  I.Installation_Id = @Installation_Id
  AND I.Installation_End_Date  IS NULL      
   END      
END    
ELSE    
 SET @Return='NA'    
        
 RETURN @Return      
END

GO

