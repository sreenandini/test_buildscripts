USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetGameCategory]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetGameCategory]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- rsp_GetGameCategory
-- -----------------------------------------------------------------
-- 
-- To get all the game category.
-- 
-- -----------------------------------------------------------------
-- Revision History
-- 
-- 10/08/2012 Dinesh Rathinavel Created
-- [rsp_GetGameCategory] 'All'
-- =================================================================

CREATE PROCEDURE [dbo].[rsp_GetGameCategory]
	@Game_Category_Name VARCHAR(50) = NULL
AS      
BEGIN   
  
	SET NOCOUNT ON
	SELECT * FROM Game_Category 
	WHERE @Game_Category_Name IS NULL OR @Game_Category_Name = 'All' OR UPPER(LTRIM(RTRIM(Game_Category_Name))) = UPPER(LTRIM(RTRIM(@Game_Category_Name)))
	ORDER BY Game_Category_Name

END

GO

