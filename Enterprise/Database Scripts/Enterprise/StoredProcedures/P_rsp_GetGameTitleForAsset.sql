USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetGameTitleForAsset]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetGameTitleForAsset]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[rsp_GetGameTitleForAsset]  
AS  
BEGIN  
	DECLARE @MultiGame AS VARCHAR(50)
	SET @MultiGame='MULTI GAME'
	
	SELECT gt.Game_Title  FROM Game_Title gt WITH (NOLOCK) 
	UNION SELECT @MultiGame	
	ORDER BY gt.Game_Title
	
		
END



