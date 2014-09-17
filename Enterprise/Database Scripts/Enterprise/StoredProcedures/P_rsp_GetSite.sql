USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetSite]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetSite]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE rsp_GetSite
@SiteID INT
AS
BEGIN
	SELECT * FROM Site WHERE Site_ID = @SiteID
END


GO

