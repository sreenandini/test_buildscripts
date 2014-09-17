USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[usp_UpdateCalendarPeriod]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[usp_UpdateCalendarPeriod]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].usp_UpdateCalendarPeriod 
(
    @CalendarPeriodNumber     INT,
    @CalendarPeriodStartDate  VARCHAR(30),
    @CalendarPeriodEndDate    VARCHAR(30),
    @CalendarId               INT
)
AS
BEGIN
	UPDATE Calendar_Period
	SET    Calendar_Period_Start_Date = @CalendarPeriodStartdate,
	       Calendar_Period_End_Date = @CalendarPeriodEndDate
	WHERE  Calendar_Period_Number = @CalendarPeriodNumber
	       AND Calendar_ID = @CalendarId
	       
	UPDATE MeterAnalysis.dbo.Calendar_Period
	SET    Calendar_Period_Start_Date = @CalendarPeriodStartdate,
	       Calendar_Period_End_Date = @CalendarPeriodEndDate
	WHERE  Calendar_Period_Number = @CalendarPeriodNumber
	       AND Calendar_ID = @CalendarId
END
GO

