USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_InsertUpdateDropScheduleHistory]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_InsertUpdateDropScheduleHistory]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[usp_InsertUpdateDropScheduleHistory]   
(  
 @DropScheduleHistoryID INT,
 @ScheduleID INT,  
 @Name VARCHAR(50),   
 @ScheduleTime DATETIME,  
 @StackerLevel TINYINT,  
 @ScheduleType TINYINT,  
 @StartDate DATETIME,  
 @EndDate DATETIME,  
 @OccurrenceType TINYINT,  
 @TotalOccurrence INT,  
 @WeekDays TINYINT,  
 @MonthDuration INT,  
 @DateofMonth TINYINT,   
 @NextOcc DATETIME,  
 @IsActive BIT,
 @ExecutedTime DATETIME,
 @Status INT
 
)  
AS  
  
BEGIN  
  
IF NOT EXISTS(Select 1 from DropScheduleHistory Where DropScheduleHistoryID = @DropScheduleHistoryID)  
BEGIN  
  INSERT INTO [dbo].[DropScheduleHistory]
           ([ScheduleID]
           ,[ScheduleName]
           ,[ScheduleTime]
           ,[StackerLevel]
           ,[ScheduleType]
           ,[StartDate]
           ,[EndDate]
           ,[OccurrenceType]
           ,[TotalOccurrence]
           ,[WeekDays]
           ,[MonthDuration]
           ,[DateofMonth]
           ,[NextOcc]
           ,[IsActive]
           ,[ExecutedTime]
           ,[Status])
     VALUES
           ( 
			@ScheduleID, 
			@Name,   
			@ScheduleTime,  
			@StackerLevel,  
			@ScheduleType,  
			@StartDate,  
			@EndDate,  
			@OccurrenceType,  
			@TotalOccurrence,  
			@WeekDays,  
			@MonthDuration,  
			@DateofMonth,   
			@NextOcc,  
			@IsActive,
			@ExecutedTime,
			@Status
)  
 
END  
  
ELSE  
  
BEGIN  
  
UPDATE [dbo].[DropScheduleHistory]   
SET
 ScheduleID =@ScheduleID,
 ScheduleName = @Name,   
 ScheduleTime = @ScheduleTime,  
 StackerLevel = @StackerLevel,  
 ScheduleType = @ScheduleType,  
 StartDate = @StartDate,  
 EndDate = @EndDate,  
 OccurrenceType = @OccurrenceType,  
 TotalOccurrence = @TotalOccurrence,  
 WeekDays = @WeekDays,  
 MonthDuration = @MonthDuration,  
 DateofMonth = @DateofMonth,   
 NextOcc = @NextOcc,  
 IsActive = @IsActive,
 ExecutedTime = @ExecutedTime,
 [Status]  = @Status
 
 WHERE DropScheduleHistoryID = @DropScheduleHistoryID
END  
  
END  

GO

