USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetGameNameForAsset]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetGameNameForAsset]
GO

USE [Enterprise]
GO

CREATE PROCEDURE rsp_GetGameNameForAsset
AS
BEGIN
	SELECT ISNULL(Game_Title_ID, 0) AS MG_Game_ID,
	       Game_Title AS MG_Game_Name
	FROM   Game_Title
	ORDER BY
	       Game_Title
END
GO