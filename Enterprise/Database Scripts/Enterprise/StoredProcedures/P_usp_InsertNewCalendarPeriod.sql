USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_InsertNewCalendarPeriod]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_InsertNewCalendarPeriod]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].usp_InsertNewCalendarPeriod
(
    @Calendar_Period_Number         INT,
    @Calendar_Period_Start_Date     VARCHAR(30),
    @Calendar_Period_End_Date       VARCHAR(30),
    @CalendarID                     INT
)
AS
BEGIN
	INSERT INTO Calendar_Period
	  (
	    Calendar_ID,
	    Calendar_Period_Number,
	    Calendar_Period_Start_Date,
	    Calendar_Period_End_Date
	  )
	VALUES
	  (
	    @CalendarID,
	    @Calendar_Period_Number,
	    @Calendar_Period_Start_Date,
	    @Calendar_Period_End_Date
	  )
	  
	  INSERT INTO MeterAnalysis.dbo.Calendar_Period
	  (
	    Calendar_ID,
	    Calendar_Period_Number,
	    Calendar_Period_Start_Date,
	    Calendar_Period_End_Date
	  )
	VALUES
	  (
	    @CalendarID,
	    @Calendar_Period_Number,
	    @Calendar_Period_Start_Date,
	    @Calendar_Period_End_Date
	  )
END


GO

