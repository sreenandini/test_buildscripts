USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[usp_InsertNewCalendar]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[usp_InsertNewCalendar]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

       
CREATE PROCEDURE [dbo].usp_InsertNewCalendar 
(
    @CalendarDescription    VARCHAR(50),
    @CalendarYearStartDate  VARCHAR(30),
    @CalendarYearEndDate    VARCHAR(30)
)
AS
BEGIN
	DECLARE @NewCalendarID INT = 0
	INSERT INTO Calendar
	  (
	    -- Calendar_ID -- this column value is auto-generated        
	    Calendar_Description,
	    Calendar_Year_Start_Date,
	    Calendar_Year_End_Date
	  )
	VALUES
	  (
	    @CalendarDescription,
	    @CalendarYearStartDate,
	    @CalendarYearEndDate
	  )
	
	SET @NewCalendarID = SCOPE_IDENTITY()
	
	INSERT INTO MeterAnalysis.dbo.Calendar
	  (
	    -- Calendar_ID -- this column value is auto-generated        
	    Calendar_Description,
	    Calendar_Year_Start_Date,
	    Calendar_Year_End_Date
	  )
	VALUES
	  (
	    @CalendarDescription,
	    @CalendarYearStartDate,
	    @CalendarYearEndDate
	  )
	
	IF (ISNULL(@NewCalendarID, 0) = 0)
	BEGIN
	    SET @NewCalendarID = 0
	END
	
	RETURN @NewCalendarID
END