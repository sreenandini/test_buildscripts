USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_GetCalendarList]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_GetCalendarList]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
  
CREATE PROCEDURE [dbo].[rsp_GetCalendarList]
AS
BEGIN
	SELECT DISTINCT c.Calendar_ID,
	       Calendar_Description,
	       Calendar_Year_Start_Date,
	       Calendar_Year_End_Date,
	       CASE 
	            WHEN ISNULL(cp.Calendar_ID, 0) = 0 THEN 0
	            WHEN ISNULL(cw.Calendar_ID, 0) = 0 THEN 0
	            ELSE 1
	       END AS IsCompleteCalendar
	FROM   Calendar c
	       LEFT JOIN Calendar_Period cp
	            ON  c.Calendar_ID = cp.Calendar_ID
	       LEFT JOIN Calendar_Week cw
	            ON  c.Calendar_ID = cw.Calendar_ID
	ORDER BY
	       c.Calendar_Description
END
GO