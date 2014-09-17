USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_DeleteDropSchedule]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_DeleteDropSchedule]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[usp_DeleteDropSchedule] 
(
	@ScheduleID INT =0
)
AS
BEGIN

IF EXISTS (SELECT 1 FROM DROPSCHEDULE)
BEGIN
		Delete from DROPSCHEDULE where ScheduleID = @ScheduleID
END

END

GO

