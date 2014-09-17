USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[usp_C_CreateCalendar]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[usp_C_CreateCalendar]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[usp_C_CreateCalendar]
	@CalendarID INT = 0,
	@IsDaysBased BIT,
	@CalendarStartDay INT,
	@ACP_ID INT = 0,
	@NewCalendarID INT OUTPUT,
	@NewCalendarName VARCHAR(50) OUTPUT
AS
	SET NOCOUNT ON
	BEGIN TRAN 
	BEGIN
		/*****************************************
		* Declared variables and purpose
		******************************************/
		DECLARE @NoOFDays         INT = 0
		DECLARE @PeriodCount      INT = 0 
		DECLARE @WeekCount        INT = 0 
		DECLARE @PeriodDiff       INT = 0
		DECLARE @WeekDiff         INT = 0
		DECLARE @iPeriodCount     INT = 1
		DECLARE @iWeekCount       INT = 1 
		DECLARE @CalendarEndDate  DATETIME
		DECLARE @Day              INT
		/*****************************************
		* Creat temp table for the new Calendar
		******************************************/
		CREATE TABLE #tempNewCalendar
		(
			new_cal_Startdate  DATETIME,
			new_cal_Enddate    DATETIME,
			new_DayDiff        INT
		)
		
		/*****************************************
		* Creat temp table for the new Calendar_Period
		******************************************/
		CREATE TABLE #tempNewCalendarPeriod
		(
			new_cal_Period_Startdate  DATETIME,
			new_cal_Period_Enddate    DATETIME,
			new_cal_Period_Number     INT
		)
		
		/*******************************************
		* Creat temp table for the new Calendar_Week
		********************************************/
		CREATE TABLE #tempNewCalendarWeek
		(
			new_Cal_Week_Startdate  DATETIME,
			new_Cal_Week_Enddate    DATETIME,
			new_Cal_Week_Number     INT
		)
		
		/********Calendar creation process Start********/
		
		/** Get the number of days of the pervious calendar to create the new calendar **/
		SELECT @NoOFDays = DATEDIFF(DAY,CONVERT(DATETIME, Calendar_Year_Start_Date, 103),CONVERT(DATETIME, Calendar_Year_End_Date, 103))
		FROM   Calendar
		WHERE  Calendar_ID = @CalendarID
		
		DECLARE @NewCalEndDate VARCHAR(30)
		IF (@isDaysBased = 1)
		BEGIN
		    SELECT @NewCalEndDate = DATEADD(ss,-1,DATEADD(DAY,(DATEDIFF(DAY,ISNULL(@CalendarStartDay,0),CONVERT(DATETIME, c.Calendar_Year_End_Date, 103))/7)*7 + 7 ,ISNULL(@CalendarStartDay,0)))
		    FROM   Calendar c
		    WHERE  c.Calendar_ID = @CalendarID
		END
		ELSE
		BEGIN
		    SELECT @NewCalEndDate = c.Calendar_Year_End_Date
		    FROM   Calendar c
		    WHERE  c.Calendar_ID = @CalendarID
		END		
		
		INSERT INTO #tempNewCalendar
		(
			new_cal_Startdate,  
			new_cal_Enddate, 
			new_DayDiff       
		)
		SELECT
		DATEADD(dd,1,DATEADD(dd,DATEDIFF(dd, 0, CONVERT(DATETIME, @NewCalEndDate, 103)),0)),
		DATEADD(dd,@NoOFDays + 1,CONVERT(DATETIME, @NewCalEndDate, 103)),
		DATEDIFF(DD,DATEADD(dd,1,DATEADD(dd,DATEDIFF(dd, 0, CONVERT(DATETIME, @NewCalEndDate, 103)),0)),
					DATEADD(dd,@NoOFDays + 1,CONVERT(DATETIME, @NewCalEndDate, 103)))
		FROM   Calendar (NOLOCK)
		WHERE  Calendar_ID = @CalendarID
		
		SELECT @CalendarEndDate = new_cal_Enddate FROM   #tempNewCalendar
		
		/********Calendar creation process End**********/
		
		/********Calendar Period creation process Start********/
		
		/** Get the number of days of the pervious calendar period to create the new calendar period **/
		SET @PeriodDiff = (
		        SELECT TOP 1 DATEDIFF(
								DAY,
								CONVERT(DATETIME, calendar_period_Start_Date, 103),
								CONVERT(DATETIME, calendar_period_End_Date, 103)
		               )
		        FROM   Calendar_Period(NOLOCK)
		        WHERE  Calendar_ID = @CalendarID
		    )
		INSERT INTO #tempNewCalendarPeriod
		  (
		    new_cal_Period_Startdate,
		    new_cal_Period_Enddate,
		    new_cal_Period_Number
		  )
		SELECT new_cal_Startdate,
		       DATEADD(SECOND,-1,DATEADD(DAY, @PeriodDiff + 1, new_cal_Startdate)),
		       @iPeriodCount
		FROM   #tempNewCalendar(NOLOCK)
		
		SET @iPeriodCount = @iPeriodCount + 1
		
		SELECT @PeriodCount = (SELECT COUNT(Calendar_Period_Number)
							   FROM   Calendar_Period
							   WHERE  Calendar_ID = @CalendarID)
		
		WHILE (@PeriodCount >= @iPeriodCount)
		BEGIN
		    SET @PeriodDiff = (SELECT TOP 1 DATEDIFF(
		                       DAY,
		                       CONVERT(DATETIME, calendar_period_Start_Date, 103),
		                       CONVERT(DATETIME, calendar_period_End_Date, 103))
		            FROM   Calendar_Period(NOLOCK)
		            WHERE  Calendar_ID = @CalendarID
		                   AND Calendar_Period_Number = @iPeriodCount
		        )
		    
		    INSERT INTO #tempNewCalendarPeriod
		      (
		        new_cal_Period_Startdate,
		        new_cal_Period_Enddate,
		        new_cal_Period_Number
		      )
		    SELECT DATEADD(second, 1, new_cal_Period_Enddate),
		           DATEADD(SECOND,-1,DATEADD(DAY,@PeriodDiff + 1,DATEADD(second, 1, new_cal_Period_Enddate))),
		           @iPeriodCount
		    FROM   #tempNewCalendarPeriod(NOLOCK)
		    WHERE  new_cal_Period_Number = @iPeriodCount -1
		           AND new_cal_Period_Enddate < @CalendarEndDate
		    
		    SET @iPeriodCount = @iPeriodCount + 1
		END
		
		/********Calendar Period creation process Start**********/
		/** Get the number of days of the pervious calendar week to create the new calendar week **/
		SET @WeekDiff = (
		        SELECT TOP 1 DATEDIFF(
		                   DAY,
		                   CONVERT(DATETIME, Calendar_Week_Start_Date, 103),
		                   CONVERT(DATETIME, Calendar_Week_End_Date, 103)
		               )
		        FROM   Calendar_Week(NOLOCK)
		        WHERE  Calendar_ID = @CalendarID
		    )
		
		INSERT INTO #tempNewCalendarWeek
		  (
		    new_Cal_Week_Startdate,
		    new_Cal_Week_Enddate,
		    new_Cal_Week_Number
		  )
		SELECT new_cal_Startdate,
		       DATEADD(SECOND, -1, DATEADD(DAY, @WeekDiff + 1, new_cal_Startdate)),
		       @iWeekCount
		FROM   #tempNewCalendar(NOLOCK)
		
		SET @iWeekCount = @iWeekCount + 1
		
		SET @WeekCount = (
		        SELECT COUNT(Calendar_Week_Number)
		        FROM   Calendar_Week
		        WHERE  Calendar_ID = @CalendarID
		    )
		
		WHILE (@WeekCount >= @iWeekCount)
		BEGIN
		    INSERT INTO #tempNewCalendarWeek
		      (
		        new_Cal_Week_Startdate,
		        new_Cal_Week_Enddate,
		        new_Cal_Week_Number
		      )
		    SELECT DATEADD(second, 1, new_Cal_Week_Enddate),
		           DATEADD(SECOND,-1,DATEADD(DAY,@WeekDiff + 1,DATEADD(second, 1, new_Cal_Week_Enddate))),
		           @iWeekCount
		    FROM   #tempNewCalendarWeek(NOLOCK)
		    WHERE  new_Cal_Week_Number = @iWeekCount -1
		           AND new_Cal_Week_Enddate < @CalendarEndDate
		    
		    SET @iWeekCount = @iWeekCount + 1
		END
		
		UPDATE #tempNewCalendarPeriod
		SET    new_cal_Period_Enddate = @CalendarEndDate
		WHERE  new_cal_Period_Number = (
		           SELECT MAX(new_cal_Period_Number)
		           FROM   #tempNewCalendarPeriod
		       )
		
		UPDATE #tempNewCalendarWeek
		SET    new_Cal_Week_Enddate = @CalendarEndDate
		WHERE  new_Cal_Week_Number = (
		           SELECT MAX(new_Cal_Week_Number)
		           FROM   #tempNewCalendarWeek
		       )
		       /********Calendar Week creation process End*********/
		 
		 SET @NewCalendarName = 'AC_' + REPLACE(REPLACE(REPLACE(REPLACE(CONVERT(VARCHAR(30), GETDATE(), 126), '-', ''),'T',''),':',''),'.','')
		 
		 INSERT INTO Calendar
		 (
		 	-- Calendar_ID -- this column value is auto-generated,
		 	Calendar_Description,
		 	Calendar_Year_Start_Date,
		 	Calendar_Year_End_Date
		 )
		 SELECT @NewCalendarName,
		        dbo.fnGetCustomDateFormat(new_cal_Startdate, 'DD-MM-YYYY HH:mm:ss'),
		        dbo.fnGetCustomDateFormat(new_cal_Enddate, 'DD-MM-YYYY HH:mm:ss')
		 FROM   #tempNewCalendar
		 
		 SELECT @NewCalendarID = SCOPE_IDENTITY()
		 
		 INSERT INTO Calendar_Period
		   (
		     Calendar_ID,
		     Calendar_Period_Number,
		     Calendar_Period_Start_Date,
		     Calendar_Period_End_Date
		   )
		 SELECT @NewCalendarID,
		        tncp.new_cal_Period_Number,
		        dbo.fnGetCustomDateFormat(tncp.new_cal_Period_Startdate, 'DD-MM-YYYY HH:mm:ss'),
		        dbo.fnGetCustomDateFormat(tncp.new_cal_Period_Enddate, 'DD-MM-YYYY HH:mm:ss')
		 FROM   #tempNewCalendarPeriod tncp
		 
		 INSERT INTO Calendar_Week
		   (
		     Calendar_ID,
		     Calendar_Week_Number,
		     Calendar_Week_Start_Date,
		     Calendar_Week_End_Date
		   )
		 SELECT @NewCalendarID,
		        new_Cal_Week_Number,
		        dbo.fnGetCustomDateFormat(new_Cal_Week_Startdate, 'DD-MM-YYYY HH:mm:ss'),
		        dbo.fnGetCustomDateFormat(new_Cal_Week_Enddate, 'DD-MM-YYYY HH:mm:ss')
		 FROM   #tempNewCalendarWeek
		 
		 /* 
		 * Updating the table to show the calendars created using AutoCalendar Functionality
		 * and its previous calendar details
		 */
		 EXEC usp_AC_UpdateAutoCalendarDetails	@CalendarID,
		      							 		@NewCalendarID,
		      							 		@NewCalendarName,
		      							 		@CalendarStartDay
		 
	END
	IF (@@ERROR <> 0)
	BEGIN
	    ROLLBACK TRAN
	END
	ELSE
	BEGIN
	    COMMIT TRAN
	END
GO