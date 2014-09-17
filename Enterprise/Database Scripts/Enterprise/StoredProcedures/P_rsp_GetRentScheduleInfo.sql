USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_GetRentScheduleInfo]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_GetRentScheduleInfo]
GO

CREATE PROCEDURE [dbo].[rsp_GetRentScheduleInfo]
AS
BEGIN
	SELECT Rent_Schedule_ID,
	       Rent_Schedule_Name
	FROM   Rent_Schedule
	ORDER BY
	       Rent_Schedule_Name
END
GO
