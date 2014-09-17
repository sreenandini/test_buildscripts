USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetGameDetailsByGameGroup]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetGameDetailsByGameGroup]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- rsp_GetGameDetailsByGameGroup
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

CREATE PROCEDURE [dbo].[rsp_GetGameDetailsByGameGroup]
	@GroupId INT
AS
BEGIN

	SELECT 
		DISTINCT G.MG_Game_ID,
		G.MG_Game_Name,
		G.MG_Group_ID 
	FROM dbo.Game_Library G 
	WHERE G.MG_Group_ID = @GroupId

END

GO

