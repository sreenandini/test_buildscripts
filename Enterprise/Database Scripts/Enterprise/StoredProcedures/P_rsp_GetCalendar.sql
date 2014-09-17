USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_GetCalendar]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_GetCalendar]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].rsp_GetCalendar
(@CalendarID INT)
AS
BEGIN
	SELECT C.Calendar_ID,
	       C.Calendar_Description,
	       C.Calendar_Year_Start_Date,
	       C.Calendar_Year_End_Date,
	       CASE 
	            WHEN ISNULL(ACD.ACD_New_Calendar_ID, 0) = 0 THEN 0
	            WHEN ACD.ACD_New_Calendar_ID > 0 THEN 1
	       END AS IsCalendarCreatedUsingAutoCalendar,
	       CASE 
	            WHEN ISNULL(ACD.ACD_Day, 0) = 0 THEN 'Date'
	            WHEN ACD.ACD_Day = 0 THEN 'Monday'
	            WHEN ACD.ACD_Day = 1 THEN 'Tuesday'
	            WHEN ACD.ACD_Day = 2 THEN 'Wednesday'
	            WHEN ACD.ACD_Day = 3 THEN 'Thursday'
	            WHEN ACD.ACD_Day = 4 THEN 'Friday'
	            WHEN ACD.ACD_Day = 5 THEN 'Saturday'
	            WHEN ACD.ACD_Day = 6 THEN 'Sunday'
	       END AS CalendarBasedOn
	FROM   Calendar C WITH(NOLOCK)
	       LEFT OUTER JOIN AutoCalendarDetails ACD WITH(NOLOCK)
	            ON  ACD.ACD_New_Calendar_ID = C.Calendar_ID
	            AND ACD.ACD_CalendarName = C.Calendar_Description
	WHERE  C.Calendar_ID = @CalendarID
END
GO