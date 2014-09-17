USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_GetCalendarPeriods]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_GetCalendarPeriods]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[rsp_GetCalendarPeriods]
	@Site_ID INT,
	@Date DATETIME,
	@Operator_No INT,
	@Site_Week_ID INT OUTPUT,
	@Site_Period_ID INT OUTPUT,
	@Operator_Week_ID INT OUTPUT,
	@Operator_Period_ID INT OUTPUT
AS
	SET DATEFORMAT DMY
	
	-- site week id ..
	--
	DECLARE @CalendarId INT
	
	--@CalendarId added by M.Durga
	
	SELECT @CalendarId = sc.Calendar_ID
	FROM   Sub_Company_Calendar
	       INNER JOIN Sub_Company sc
	            ON  sc.Sub_Company_ID = Sub_Company_Calendar.Sub_Company_ID
	       INNER JOIN [Site] s
	            ON  s.Sub_Company_ID = sc.Sub_Company_ID
	WHERE  s.Site_ID = @Site_ID
	
	SELECT @Site_Week_ID = Calendar_Week.Calendar_Week_ID
	FROM   SITE
	       JOIN Calendar
	            ON  Calendar.Calendar_ID = @CalendarId
	       JOIN Calendar_Week
	            ON  Calendar.Calendar_ID = Calendar_Week.Calendar_ID
	WHERE  @Date BETWEEN CONVERT(DATETIME, Calendar_Week_Start_Date, 103)
	       AND CONVERT(DATETIME, Calendar_Week_End_Date, 103)
	       AND SITE.Site_ID = @Site_Id
	
	-- site period id ..
	--
	SELECT @Site_Period_ID = Calendar_Period.Calendar_Period_ID
	FROM   SITE
	       JOIN Calendar
	            ON  Calendar.Calendar_ID = @CalendarId
	       JOIN Calendar_Period
	            ON  Calendar.Calendar_ID = Calendar_Period.Calendar_ID
	WHERE  @Date BETWEEN CONVERT(DATETIME, Calendar_Period_Start_Date, 103) AND 
	       CONVERT(DATETIME, Calendar_Period_End_Date, 103) 
	
	-- operator week id ..
	--
	SELECT @Operator_Week_ID = Calendar_Week.Calendar_Week_ID
	FROM   Operator_Calendar
	       JOIN Calendar
	            ON  Operator_Calendar.Calendar_ID = Calendar.Calendar_ID
	       JOIN Calendar_Week
	            ON  Calendar.Calendar_ID = Calendar_Week.Calendar_ID
	WHERE  Operator_Calendar.Operator_ID = @Operator_No
	       AND @Date BETWEEN CONVERT(DATETIME, Calendar_Week_Start_Date, 103) 
	           AND CONVERT(DATETIME, Calendar_Week_End_Date, 103) 
	
	
	-- operator period id ..
	--
	SELECT @Operator_Period_ID = Calendar_Period.Calendar_Period_ID
	FROM   Operator_Calendar
	       JOIN Calendar
	            ON  Operator_Calendar.Calendar_ID = Calendar.Calendar_ID
	       JOIN Calendar_Period
	            ON  Calendar.Calendar_ID = Calendar_Period.Calendar_ID
	WHERE  Operator_Calendar.Operator_ID = @Operator_No
	       AND @Date BETWEEN CONVERT(DATETIME, Calendar_Period_Start_Date, 103) 
	           AND CONVERT(DATETIME, Calendar_Period_End_Date, 103)
	
	-- return error if any
	--
	
	--	
	IF (ISNULL(@Site_Week_ID, 0) = 0)
	BEGIN
	    SELECT TOP 1 @Site_Week_ID = ISNULL(c.Week_ID, 0)
	    FROM   [Collection] c
	           INNER JOIN Installation i
	                ON  c.Installation_ID = i.Installation_ID
	           INNER JOIN Bar_Position bp
	                ON  bp.Bar_Position_ID = i.Bar_Position_ID
	           INNER JOIN [site] s
	                ON  s.Site_ID = bp.Site_ID
	                AND s.Site_ID = @Site_ID
	    ORDER BY
	           c.Collection_ID DESC
	END
	
	IF (ISNULL(@Site_Period_ID, 0) = 0)
	BEGIN
	    SELECT TOP 1 @Site_Period_ID = ISNULL(c.Period_ID, 0)
	    FROM   [Collection] c
	           INNER JOIN Installation i
	                ON  c.Installation_ID = i.Installation_ID
	           INNER JOIN Bar_Position bp
	                ON  bp.Bar_Position_ID = i.Bar_Position_ID
	           INNER JOIN [site] s
	                ON  s.Site_ID = bp.Site_ID
	                AND s.Site_ID = @Site_ID
	    ORDER BY
	           c.Collection_ID DESC
	END
	
	IF (ISNULL(@Operator_Week_ID, 0) = 0)
	BEGIN
	    SELECT TOP 1 @Operator_Week_ID = ISNULL(c.Operator_Week_ID, 0)
	    FROM   [Collection] c
	           INNER JOIN Installation i
	                ON  c.Installation_ID = i.Installation_ID
	           INNER JOIN Bar_Position bp
	                ON  bp.Bar_Position_ID = i.Bar_Position_ID
	           INNER JOIN [site] s
	                ON  s.Site_ID = bp.Site_ID
	                AND s.Site_ID = @Site_ID
	    ORDER BY
	           c.Collection_ID DESC
	END
	
	IF (ISNULL(@Operator_Period_ID, 0) = 0)
	BEGIN
	    SELECT TOP 1 @Operator_Period_ID = ISNULL(c.Operator_Week_ID, 0)
	    FROM   [Collection] c
	           INNER JOIN Installation i
	                ON  c.Installation_ID = i.Installation_ID
	           INNER JOIN Bar_Position bp
	                ON  bp.Bar_Position_ID = i.Bar_Position_ID
	           INNER JOIN [site] s
	                ON  s.Site_ID = bp.Site_ID
	                AND s.Site_ID = @Site_ID
	    ORDER BY
	           c.Collection_ID DESC
	END
	
	RETURN @@Error
GO