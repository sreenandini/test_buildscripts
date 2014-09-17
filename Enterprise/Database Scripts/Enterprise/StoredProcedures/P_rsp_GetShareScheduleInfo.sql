USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_GetShareScheduleInfo]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_GetShareScheduleInfo]
GO

CREATE PROCEDURE [dbo].[rsp_GetShareScheduleInfo]
AS
BEGIN
	SELECT Share_Schedule_ID,
	       Share_Schedule_Name
	FROM   Share_Schedule
	ORDER BY
	       Share_Schedule_Name
END
GO
