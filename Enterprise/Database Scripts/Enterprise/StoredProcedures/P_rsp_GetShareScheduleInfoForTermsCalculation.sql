USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_GetShareScheduleInfoForTermsCalculation]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_GetShareScheduleInfoForTermsCalculation]
GO

CREATE PROCEDURE [dbo].[rsp_GetShareScheduleInfoForTermsCalculation] 
(@Share_Schedule_ID INT, @Machine_Class_ID INT)
AS
BEGIN
	SELECT Machine_Class_Share_Band.Machine_Class_Share_Band,
	       Machine_Class_Share_Band.Machine_Class_ID,
	       Machine_Class_Share_Band.Share_Band_ID,
	       Machine_Class_Share_Band.Share_Band_ID_Future,
	       Machine_Class_Share_Band.Machine_Class_Share_Future_Date,
	       Machine_Class_Share_Band.Share_Band_ID_Past,
	       Machine_Class_Share_Band.Machine_Class_Share_Past_Date,
	       Share_Schedule.Share_Schedule_Name
	FROM   Machine_Class_Share_Band
	       INNER JOIN Share_Band
	            ON  Machine_Class_Share_Band.Share_Band_ID = Share_Band.Share_Band_ID
	       INNER JOIN Share_Schedule
	            ON  Share_Band.Share_Schedule_ID = Share_Schedule.Share_Schedule_ID
	WHERE  Share_Band.Share_Schedule_ID = @Share_Schedule_ID
	       AND (
	               Machine_Class_ID = @Machine_Class_ID
	               OR Machine_Class_ID = 0
	           )
	ORDER BY
	       Machine_Class_ID DESC
END
GO
