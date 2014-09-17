USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[usp_AC_UpdateAutoCalendarDetails]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[usp_AC_UpdateAutoCalendarDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].usp_AC_UpdateAutoCalendarDetails
(
	@PreviousCalendar_ID INT,
    @NewCalendar_ID   INT,
    @CalendarName  VARCHAR(50),
    @Day           INT
)
AS
BEGIN
	INSERT INTO AutoCalendarDetails
	  (
	  	ACD_Previous_Calendar_ID,
	    ACD_New_Calendar_ID,
	    ACD_CalendarName,
	    ACD_Day
	  )
	VALUES
	  (
	  	@PreviousCalendar_ID,
	    @NewCalendar_ID,
	    @CalendarName,
	    @Day
	  )
END
GO