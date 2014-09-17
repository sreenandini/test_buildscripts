USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[usp_UpdateWeekCalendar]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[usp_UpdateWeekCalendar]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].usp_UpdateWeekCalendar
(
    @CalendarWeekNumber     INT,
    @CalendarWeekStartDate  VARCHAR(30),
    @CalendarWeekEndDate    VARCHAR(30),
    @CalendarId             INT
)
AS
BEGIN
	UPDATE Calendar_Week
	SET    Calendar_Week_Start_Date = @CalendarWeekStartDate,
	       Calendar_Week_End_Date = @CalendarWeekEndDate
	WHERE  Calendar_Week_Number = @CalendarWeekNumber
	       AND Calendar_ID = @CalendarId
END
GO

