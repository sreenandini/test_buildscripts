USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetLstCalendar]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetLstCalendar]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[rsp_GetLstCalendar]
AS
BEGIN
	SELECT *
	FROM   Calendar
	ORDER BY Calendar_Description
	
END

--EXEC rsp_GetLstCalendar
--INSERT INTO calendar
--  (
--    Calendar_Description,
--    Calendar_Year_Start_Date,
--    Calendar_Year_End_Date
--  )
--VALUES
--  (
--    'BMC',
--    '20 Jan 2013',
--    '20 Jan 2014'
--  )


GO

