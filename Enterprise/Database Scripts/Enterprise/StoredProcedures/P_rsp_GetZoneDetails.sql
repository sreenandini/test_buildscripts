USE [Enterprise]
GO

IF  EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetZoneDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetZoneDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE rsp_GetZoneDetails
@ZoneID INT
AS
BEGIN
	SELECT * FROM [Zone] WHERE Zone_ID = @ZoneID
END


GO

