USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetBarPositionID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetBarPositionID]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE rsp_GetBarPositionID
@BarPositionName VARCHAR(50)
AS
BEGIN
	SELECT Bar_Position_ID FROM Bar_Position bp WHERE bp.Bar_Position_Name=@BarPositionName
END


GO

