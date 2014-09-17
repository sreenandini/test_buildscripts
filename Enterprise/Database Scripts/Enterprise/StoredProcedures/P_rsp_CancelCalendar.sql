USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_CancelCalendar]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_CancelCalendar]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].rsp_CancelCalendar
AS
BEGIN

declare @CalendarID INT
declare @CalendarPeriodNumber int
SELECT *
FROM   Calendar_Period
WHERE  Calendar_ID = @CalendarID
       AND Calendar_Period_Number = @CalendarPeriodNumber
END

GO

