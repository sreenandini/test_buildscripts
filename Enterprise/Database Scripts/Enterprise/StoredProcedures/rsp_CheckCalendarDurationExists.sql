USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_CheckCalendarDurationExists]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_CheckCalendarDurationExists]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE rsp_CheckCalendarDurationExists(@CType Varchar(30), @Start_Date     VARCHAR(30),    @End_Date  VARCHAR(30),@Retval BIT OUTPUT)
AS 
BEGIN

SET NOCOUNT ON
SET DATEFORMAT YMD
SET @Retval=0

	IF UPPER(@CType) ='CALENDAR'
	BEGIN
	 
		IF  EXISTS(SELECT TOP 1 1 FROM Calendar WITH(NOLOCK) WHERE
		(CONVERT(DATETIME, @Start_Date, 103) >=  CONVERT(DATETIME, Calendar_Year_Start_Date, 103)
		AND  CONVERT(DATETIME, @End_Date, 103) <=  CONVERT(DATETIME, Calendar_Year_End_Date, 103))
		OR (CONVERT(DATETIME, @Start_Date, 103)BETWEEN CONVERT(DATETIME, Calendar_Year_Start_Date, 103) AND CONVERT(DATETIME, Calendar_Year_End_Date, 103))
		OR  (CONVERT(DATETIME, @End_Date, 103) BETWEEN  CONVERT(DATETIME, Calendar_Year_Start_Date, 103) AND CONVERT(DATETIME, Calendar_Year_End_Date, 103)))
		BEGIN
		SET @Retval = CAST(1 as BIT)
		END
	END
	ELSE IF UPPER(@Ctype)='CALENDAR_PERIOD'
	BEGIN
	
		IF  EXISTS(SELECT TOP 1 1 FROM Calendar_Period WITH(NOLOCK) WHERE
		(CONVERT(DATETIME, @Start_Date, 103) >=  CONVERT(DATETIME, Calendar_Period_Start_Date, 103)
		AND  CONVERT(DATETIME, @End_Date, 103) <=  CONVERT(DATETIME, Calendar_Period_End_Date, 103))
		OR (CONVERT(DATETIME, @Start_Date, 103)BETWEEN CONVERT(DATETIME, Calendar_Period_Start_Date, 103) AND CONVERT(DATETIME, Calendar_Period_End_Date, 103))
		OR  (CONVERT(DATETIME, @End_Date, 103) BETWEEN  CONVERT(DATETIME, Calendar_Period_Start_Date, 103) AND CONVERT(DATETIME, Calendar_Period_End_Date, 103))
		)
		BEGIN
		SET @Retval =CAST(1 AS BIT)
		END
	END
	ELSE IF UPPER(@Ctype)='CALENDAR_WEEK'
	BEGIN
		
		IF  EXISTS(
			  SELECT TOP 1 1 FROM Calendar_Week WITH(NOLOCK) WHERE
		(CONVERT(DATETIME, @Start_Date, 103) >=  CONVERT(DATETIME, Calendar_Week_Start_Date, 103)
		AND  CONVERT(DATETIME, @End_Date, 103) <=  CONVERT(DATETIME, Calendar_Week_End_Date, 103))
		OR (CONVERT(DATETIME, @Start_Date, 103)BETWEEN CONVERT(DATETIME, Calendar_Week_Start_Date, 103) AND CONVERT(DATETIME, Calendar_Week_End_Date, 103))
		OR  (CONVERT(DATETIME, @End_Date, 103) BETWEEN  CONVERT(DATETIME, Calendar_Week_Start_Date, 103) AND CONVERT(DATETIME, Calendar_Week_End_Date, 103))
		)
		BEGIN
				SET @Retval =CAST(1 AS BIT)

		END
	END
END
		
 

----DECLAre @Retval BIT
----Exec rsp_CheckCalendarDurationExists 'Calendar_Week','2016-05-22','2016-05-28', @Retval OUTPUT
----SELECT @Retval


--SELECT * FROM Calendar_week

--select * from [USER] 
--update [USER] set isLocked=0