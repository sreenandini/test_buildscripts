USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetBarPosition]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetBarPosition]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE rsp_GetBarPosition
@SiteId INT,
@BarPositionName VARCHAR(50),
@CheckCount INT OUTPUT,
@BarPositionID INT = 0
AS
BEGIN
	DECLARE @BarPosition INT
	set @BarPosition = CAST(LTRIM(RTRIM(@BarPositionName)) AS INT)
	SET @CheckCount=0
	SELECT @CheckCount = COUNT(*) FROM Bar_Position 
	WHERE Bar_Position_Name = @BarPosition
	AND 
	Site_ID = @SiteId
	AND
	(@BarPositionID = 0 OR Bar_Position_ID != @BarPositionID) 
	
END


GO

