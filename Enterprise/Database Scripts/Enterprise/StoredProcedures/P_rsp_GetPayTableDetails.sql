USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetPayTableDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetPayTableDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- rsp_GetGamesByCategory
-- -----------------------------------------------------------------
-- 
-- To get pay table details w.r.t the paytable id.
-- 
-- -----------------------------------------------------------------
-- Revision History
-- 
-- 11/08/2012 Dinesh Rathinavel Created
-- 
-- =================================================================

CREATE PROCEDURE [dbo].[rsp_GetPayTableDetails]
	@Paytable_ID INT
AS      
BEGIN   
  
	SET NOCOUNT ON
	
	SELECT
		tGL.MG_Game_Name As GAMENAME,
		tP.* 
	FROM PayTable tP
		INNER JOIN GAME_LIBRARY tGL ON tGL.MG_Game_ID = tP.Game_ID 
	WHERE tP.Paytable_ID = @Paytable_ID
	
END

GO

