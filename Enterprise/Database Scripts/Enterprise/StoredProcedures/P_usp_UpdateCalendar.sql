USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[usp_UpdateCalendar]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[usp_UpdateCalendar]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].usp_UpdateCalendar
(
    @Calendar_ID               INT,
    @CalendarDescription       VARCHAR(50),
    @Calendar_Year_Start_Date  VARCHAR(30),
    @Calendar_Year_End_Date    VARCHAR(30)
)
AS
BEGIN
	DECLARE @Cal_Description VARCHAR(50)
	
	SELECT @Cal_Description = Calendar_Description
	FROM   Calendar
	WHERE  Calendar_ID = @Calendar_ID
	
	UPDATE calendar
	SET    Calendar_Description = @CalendarDescription,
	       Calendar_Year_Start_Date = @Calendar_Year_Start_Date,
	       Calendar_Year_End_Date = @Calendar_Year_End_Date
	WHERE  Calendar_ID = @Calendar_ID
	
	UPDATE AutoCalendarDetails
	SET    ACD_CalendarName = @CalendarDescription
	WHERE  ACD_New_Calendar_ID = @Calendar_ID
	       AND ACD_CalendarName = @Cal_Description 
	
	UPDATE MeterAnalysis.dbo.calendar
	SET    Calendar_Year_Start_Date = @Calendar_Year_Start_Date,
	       Calendar_Year_End_Date = @Calendar_Year_End_Date
	WHERE  Calendar_ID = @Calendar_ID
END
GO

