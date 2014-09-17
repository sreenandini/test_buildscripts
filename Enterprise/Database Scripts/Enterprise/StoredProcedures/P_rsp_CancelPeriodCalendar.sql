USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_CancelPeriodCalendar]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_CancelPeriodCalendar]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].rsp_CancelPeriodCalendar
(@CalendarID INT, @CalendarPeriodNumber INT)
AS
BEGIN
	SELECT *
	FROM   Calendar_Period
	WHERE  Calendar_ID                    = @CalendarID
	       AND Calendar_Period_Number     = @CalendarPeriodNumber
END


GO

