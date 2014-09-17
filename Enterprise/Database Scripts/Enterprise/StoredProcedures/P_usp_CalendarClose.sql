USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_CalendarClose]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_CalendarClose]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].usp_CalendarClose
AS
BEGIN
	UPDATE Calendar_Week
	SET    Calendar_Period_ID = ISNULL(
	           (
	               SELECT TOP 1 CP.Calendar_Period_ID
	               FROM   Calendar_Period CP
	               WHERE  CP.Calendar_ID = Calendar_Week.Calendar_ID
	                      AND CONVERT(DATETIME, Calendar_Week.Calendar_Week_End_Date, 103)
	                          > CONVERT(DATETIME, CP.Calendar_Period_Start_Date, 103)
	                      AND CONVERT(DATETIME, Calendar_Week.Calendar_Week_End_Date, 103)
	                          <= CONVERT(DATETIME, CP.Calendar_Period_End_Date, 103)
	               ORDER BY
	                      CP.Calendar_ID ASC
	           ),
	           0
	       )
	WHERE  Calendar_Week.Calendar_Period_ID = 0
	       OR  Calendar_Week.Calendar_Period_ID = NULL
	       
	UPDATE MeterAnalysis.dbo.Calendar_Week
	SET    Calendar_Period_ID = ISNULL(
	           (
	               SELECT TOP 1 CP.Calendar_Period_ID
	               FROM   Calendar_Period CP
	               WHERE  CP.Calendar_ID = Calendar_Week.Calendar_ID
	                      AND CONVERT(DATETIME, Calendar_Week.Calendar_Week_End_Date, 103)
	                          > CONVERT(DATETIME, CP.Calendar_Period_Start_Date, 103)
	                      AND CONVERT(DATETIME, Calendar_Week.Calendar_Week_End_Date, 103)
	                          <= CONVERT(DATETIME, CP.Calendar_Period_End_Date, 103)
	               ORDER BY
	                      CP.Calendar_ID ASC
	           ),
	           0
	       )
	WHERE  Calendar_Week.Calendar_Period_ID = 0
	       OR  Calendar_Week.Calendar_Period_ID = NULL
END


GO

