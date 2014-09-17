USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetGameDetailsFromGameLibrary]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetGameDetailsFromGameLibrary]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- rsp_GetGameDetailsFromGameLibrary
-- -----------------------------------------------------------------
-- 
-- To get game details from game library.
-- 
-- -----------------------------------------------------------------
-- Revision History
-- 
-- 17/08/2012 Dinesh Rathinavel Created
-- 
-- =================================================================

CREATE PROCEDURE [dbo].[rsp_GetGameDetailsFromGameLibrary]
AS
BEGIN

	SELECT 
		G.MG_Game_ID,
		G.MG_Game_Name
	FROM dbo.Game_Library G
		WHERE G.MG_Group_ID = 1
	ORDER BY G.MG_Game_Name

END

GO

