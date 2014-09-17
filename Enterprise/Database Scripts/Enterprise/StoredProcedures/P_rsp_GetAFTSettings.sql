USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetAFTSettings]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetAFTSettings]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE rsp_GetAFTSettings (@SiteID INT)
AS
BEGIN
	SELECT * FROM AFTSetting WHERE SiteCode = @SiteId
END 

GO

