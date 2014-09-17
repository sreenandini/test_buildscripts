USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_ISCalendarNameExists]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_ISCalendarNameExists]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[rsp_ISCalendarNameExists]
(
    @CalendarName  VARCHAR(50),
    @CalendarId    INT,
    @NameCount     INT OUT
)
AS
BEGIN
	SELECT @NameCount = COUNT(*)
	FROM   Calendar
	WHERE  Calendar_Description = @CalendarName
	       AND Calendar_ID <> @CalendarId
END
GO