USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_InsertUpdateDropSchedule]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_InsertUpdateDropSchedule]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[usp_InsertUpdateDropSchedule] 
(
	@ScheduleID int,
	@Name varchar(50),	
	@ScheduleTime datetime,
	@StackerLevel tinyint,
	@ScheduleType tinyint,
	@StartDate datetime,
	@EndDate datetime,
	@OccurrenceType tinyint,
	@TotalOccurrence int,
	@WeekDays tinyint,
	@MonthDuration int,
	@DateofMonth tinyint,	
	@NextOcc datetime,
	@IsActive bit,
	@DropAlertType INT,
	@RegionId INT,
	@SiteId INT,
	@ScheduleIDOut int = NULL OUTPUT  
)
AS

BEGIN

	SET @ScheduleIDOut = @ScheduleID
	IF NOT EXISTS(Select 1 from DropSchedule WHERE ScheduleId=@ScheduleID)
	BEGIN
		INSERT INTO DropSchedule ([OccurrenceType]) VALUES (@OccurrenceType)
		SET @ScheduleIDOut =  SCOPE_IDENTITY()
	END

	UPDATE DropSchedule
	SET
		[ScheduleName] = @Name,
		[ScheduleTime] = @ScheduleTime,
		[StackerLevel] = @StackerLevel,
		[ScheduleType] = @ScheduleType,
		[StartDate] = @StartDate,
		[EndDate] = @EndDate,
		[OccurrenceType] = @OccurrenceType,
		[TotalOccurrence] = @TotalOccurrence,
		[WeekDays] = @WeekDays,
		[MonthDuration] = @MonthDuration,
		[DateofMonth] = @DateofMonth,
		[NextOcc] = @NextOcc,
		[IsActive] = @IsActive,
		[DropAlertType] = @DropAlertType,
		[RegionId] = @RegionId,
		[SiteId] = @SiteId
	WHERE ScheduleID = @ScheduleIDOut
END

GO

