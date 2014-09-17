USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetLatestBarPositionID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetLatestBarPositionID]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER OFF
GO


CREATE PROCEDURE rsp_GetLatestBarPositionID
AS
BEGIN
	SELECT MAX(Bar_Position_ID) AS BarPositionID FROM Bar_Position bp
END


GO

