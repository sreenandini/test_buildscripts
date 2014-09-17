USE [Enterprise]
GO

IF EXISTS (
       SELECT TOP 1 1
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_CheckIncompleteCalendar]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_CheckIncompleteCalendar]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE rsp_CheckIncompleteCalendar(@Calendar_ID INT, @RetVal BIT OUTPUT)
AS
BEGIN
	SET NOCOUNT ON
	SET @Retval = 0
	
	DECLARE @Calendar_Period_Start  VARCHAR(30)
	DECLARE @Calendar_Period_End    VARCHAR(30)
	DECLARE @Calendar_Week_Start    VARCHAR(30)
	DECLARE @Calendar_Week_End      VARCHAR(30)
	DECLARE @Calendar_Year_Start    VARCHAR(30)
	DECLARE @Calendar_Year_End      VARCHAR(30)
	
	SELECT @Calendar_Year_Start = Calendar_Year_Start_Date,
	       @Calendar_Year_End = Calendar_Year_End_Date
	FROM   CALENDAR
	WHERE  CALENDAR_ID = @Calendar_ID
	
	SELECT TOP 1 @Calendar_Period_Start = Calendar_Period_Start_date
	FROM   Calendar_Period cp
	WHERE  CP.Calendar_ID = @Calendar_ID
	
	SELECT TOP 1 @Calendar_Period_End = cp.Calendar_Period_End_Date
	FROM   Calendar_Period cp
	WHERE  CP.Calendar_ID = @Calendar_ID
	ORDER BY
	       Calendar_Period_Id DESC
	
	SELECT TOP 1 @Calendar_Week_Start = Calendar_Week_Start_date
	FROM   Calendar_Week CW
	WHERE  CW.Calendar_ID = @Calendar_ID
	
	SELECT TOP 1 @Calendar_Week_End = CW.Calendar_Week_End_Date
	FROM   Calendar_Week CW
	WHERE  CW.Calendar_ID = @Calendar_ID
	ORDER BY
	       Calendar_week_Id DESC
	
	IF (
	       (@Calendar_Year_Start = @Calendar_Period_Start)
	       AND (@Calendar_Year_Start = @Calendar_Week_Start)
	       AND (@Calendar_Year_End = @Calendar_Period_End)
	       AND (@Calendar_Year_End = @Calendar_Week_End)
	   )
	    SET @Retval = 1
	ELSE
	    SET @Retval = 0
END











