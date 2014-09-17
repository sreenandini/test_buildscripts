USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_FetchGameCategory]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_FetchGameCategory]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-------------------------------------------------------------------------- 
--
-- Description: Fetch the details
-- Inputs:      See inputs 
--
-- Outputs:         
--                  
-- =======================================================================
-- 
-- Revision History
--
-- Sudarsan S		03/12/2009		Created
------------------------------------------------------------------------------------------------------
CREATE PROCEDURE dbo.rsp_FetchGameCategory
	@Category_ID INT = 0
AS

BEGIN
		SELECT * FROM dbo.Game_Category WHERE (Game_Category_ID = @Category_ID OR @Category_ID = 0)
END

GO

