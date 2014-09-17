USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetCalendarWeek]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetCalendarWeek]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].rsp_GetCalendarWeek
(@CalendarID INT)
AS
BEGIN
	SELECT *
	FROM   Calendar_Week
	WHERE  Calendar_ID = @CalendarID
END


GO

