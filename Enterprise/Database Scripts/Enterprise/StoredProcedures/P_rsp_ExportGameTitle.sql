USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_ExportGameTitle]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_ExportGameTitle]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].rsp_ExportGameTitle
@GameTitleID INT 
AS                    
BEGIN    
	SELECT GT.Game_Title_ID,
		(SELECT TOP 1 GC.Game_Category_Name FROM Game_Category GC WHERE GC.Game_Category_ID = GT.Game_Category_ID) AS Game_Category_Name,
		GT.Game_Title,
		(SELECT TOP 1 MN.Manufacturer_Name FROM Manufacturer MN WHERE MN.Manufacturer_ID = GT.Manufacturer_ID) AS  Manufacturer_Name,
		ISNULL((SELECT TOP 1 D.Prev_Value FROM Export_Data D  
			WHERE  D.Source_ID =@GameTitleID and EH_Type='GAMETITLE' ORDER BY D.[dtCreatedTime] DESC),'')  AS PrevGameName
	FROM Game_Title GT 
	WHERE GT.Game_Title_ID = @GameTitleID
	FOR XML AUTO, ELEMENTS, ROOT('Game_Titles')    
END


GO

