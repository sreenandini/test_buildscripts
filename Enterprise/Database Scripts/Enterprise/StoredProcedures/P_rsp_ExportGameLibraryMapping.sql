USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_ExportGameLibraryMapping]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_ExportGameLibraryMapping]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[rsp_ExportGameLibraryMapping]  
@GameLibraryID INT   
AS                      
BEGIN
DECLARE @GameTitle VARCHAR(50)
SELECT @GameTitle = Game_Title
FROM Game_Library GL  INNER JOIN Game_Title GT ON GT.Game_Title_ID = GL.MG_Group_ID
WHERE GL.MG_Game_ID = @GameLibraryID 

--Create MachineDetails xML
 DECLARE @XML XML

 SELECT @XML = (
           SELECT COALESCE(CAST(Manufacturer_Name AS VARCHAR(50)), '') 
                  Manufacturer_Name,
                  COALESCE(CAST(Machine_Stock_No AS VARCHAR(50)), '') 
                  Machine_Stock_No,
                  COALESCE(CAST(Machine_Type_Code AS VARCHAR(50)), '') 
                  Machine_Type_Code
           FROM   Machine_Class MC
                  INNER JOIN Manufacturer MA
                       ON  MC.Manufacturer_ID = MA.Manufacturer_ID
                  INNER JOIN MACHINE M
                       ON  M.Machine_Class_ID = MC.Machine_Class_ID
                  INNER JOIN Machine_Type MT
                       ON  MT.Machine_Type_Id = MC.Machine_Type_ID
           WHERE  MC.Machine_Name = @GameTitle 
                  FOR XML AUTO ,ELEMENTS,ROOT('MachineDetails')
       )
 
 DECLARE @GameLibraryDetails VARCHAR(MAX)     
 
 SELECT @GameLibraryDetails = (SELECT (SELECT TOP 1 GC.Game_Category_Name FROM Game_Category GC WHERE GC.Game_Category_ID = GL.MG_Game_CategoryID) AS Game_Category_Name,  
   (SELECT TOP 1 GT.Game_Title FROM Game_Title GT WHERE GT.Game_Title_ID = GL.MG_Group_ID) AS Game_Title,  
   MG_Game_ID,
  @XML
 FROM Game_Library GL  
 WHERE GL.MG_Game_ID = @GameLibraryID  
 FOR XML AUTO, ELEMENTS, ROOT('GameLibraries')
 )
 
 SELECT @GameLibraryDetails
       
END

GO

