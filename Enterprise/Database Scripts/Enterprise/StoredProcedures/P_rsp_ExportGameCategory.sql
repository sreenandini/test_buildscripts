USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_ExportGameCategory]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_ExportGameCategory]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].rsp_ExportGameCategory
@GameCategoryID INT 
AS                    
BEGIN    
 SELECT * FROM Game_Category WHERE Game_Category_ID =@GameCategoryID
 FOR XML AUTO, ELEMENTS, ROOT('Game_Categories')    
END
GO

