
USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FnGetGameNameforAssetHistory]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[FnGetGameNameforAssetHistory]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION dbo.[FnGetGameNameforAssetHistory](
@Installation_Id AS INT  ,
@Bar_possition_Id AS INT,   
@Site_id    AS INT  

)      
      RETURNS VARCHAR(50)
AS          
BEGIN          
 
 DECLARE @Return AS VARCHAR(50)         
SELECT       
@Return=Game_Title
 from         
  Installation I   
  INNER JOIN Bar_Position BP ON I.Bar_Position_ID=BP.Bar_Position_ID
  INNER JOIN Site ST ON ST.Site_ID =BP.Site_ID
  INNER JOIN Installation_Game_Info IGI ON I.Installation_Id = IGI.Installation_No  
  INNER JOIN Game_Library GL ON IGI.Game_Name = GL.MG_Game_Name AND IGI.Game_Part_Number = GL.Game_Part_Number        
  INNER JOIN Game_Title GT ON GL.MG_Group_ID = GT.Game_Title_ID        
  WHERE         
  I.Installation_Id =@Installation_Id
  AND I.Installation_End_Date  IS NULL   
  AND BP.Bar_Position_ID=@Bar_possition_Id
  AND ST.Site_ID=@Site_id
     return @Return
 END  
 
 
 
 GO     
   
     