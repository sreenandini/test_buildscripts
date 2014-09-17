USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_InsertNewCalendarWeek]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_InsertNewCalendarWeek]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].usp_InsertNewCalendarWeek
(
    @Calendar_Week_Number         INT,
    @Calendar_Week_Start_Date     VARCHAR(30),
    @Calendar_Week_End_Date       VARCHAR(30),
    @CalendarID                     INT
)
AS
BEGIN
	INSERT INTO Calendar_Week
	  (
	    Calendar_ID,
	    Calendar_Week_Number,
	    Calendar_Week_Start_Date,
	    Calendar_Week_End_Date
	  )
	VALUES
	  (
	    @CalendarID,
	    @Calendar_Week_Number,
	    @Calendar_Week_Start_Date,
	    @Calendar_Week_End_Date
	  )
	  
	  INSERT INTO MeterAnalysis.dbo.Calendar_Week
	  (
	    Calendar_ID,
	    Calendar_Week_Number,
	    Calendar_Week_Start_Date,
	    Calendar_Week_End_Date
	  )
	VALUES
	  (
	    @CalendarID,
	    @Calendar_Week_Number,
	    @Calendar_Week_Start_Date,
	    @Calendar_Week_End_Date
	  )
END


GO

