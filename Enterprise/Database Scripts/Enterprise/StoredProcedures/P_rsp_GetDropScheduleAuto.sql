USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetDropScheduleAuto]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetDropScheduleAuto]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[rsp_GetDropScheduleAuto]
(
	@ScheduleID INT = 0,  
	@CurrentDate DATETIME = NULL  
)
AS
BEGIN
	IF @ScheduleID = 0 SET @ScheduleID=NULL
	
	IF EXISTS (SELECT 1 FROM DROPSCHEDULE (NOLOCK))  
	BEGIN  
		DECLARE @Setting_Value   varchar(100)
		EXEC rsp_getSetting 0,'RetryMinutesForCheckDB', '5', @Setting_Value Output
		
		SELECT 
			[ScheduleID], 
			[ScheduleName], 
			[ScheduleTime], 
			[StackerLevel], 
			[ScheduleType], 
			[StartDate], 
			DATEADD(dd, 0, DATEDIFF(dd, 0, EndDate)) +' '+ DATEADD(Day, -DATEDIFF(Day, 0, ScheduleTime), ScheduleTime) AS [EndDate], 
			[OccurrenceType],
			[TotalOccurrence], 
			[WeekDays], 
			[MonthDuration], 
			[DateofMonth], 
			[NextOcc], 
			[IsActive]
		FROM DROPSCHEDULE (NOLOCK)
		WHERE
			ScheduleID = COALESCE(@ScheduleID, ScheduleID) 
			AND DropAlertType=1
			AND (@CurrentDate IS NULL OR  @CurrentDate   
				BETWEEN DateAdd(minute, -(CAST(@Setting_Value AS INT)), StartDate) AND   
				DATEADD(dd, 0, DATEDIFF(dd, 0, EndDate)) +' '+ DATEADD(Day, -DATEDIFF(Day, 0, ScheduleTime), ScheduleTime)) 
	END  

END

GO

