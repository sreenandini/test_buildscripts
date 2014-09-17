USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_UpdateCancelWeekCalendar]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_UpdateCancelWeekCalendar]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].usp_UpdateCancelWeekCalendar
(
    @Calendar_ID                 INT,
    @CalendarWeekNumber        INT,
    @CalendarWeekStartdate     VARCHAR(30),	
    @CalendarWeekEndDate       VARCHAR(30),
    @CalendarPeriodID            INT
)
AS
BEGIN
	UPDATE Calendar_Period
	SET    Calendar_ID                    = @Calendar_ID,
	       Calendar_Period_Number         = @CalendarWeekNumber,
	       Calendar_Period_Start_Date     = @CalendarWeekStartdate,
	       Calendar_Period_End_Date       = @CalendarWeekEndDate
	WHERE  Calendar_Period_ID             = @CalendarPeriodID
END


GO

