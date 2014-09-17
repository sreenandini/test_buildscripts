USE [Enterprise]
GO

/****** Object:  StoredProcedure [dbo].[usp_InsertOrUpdateCallRemedy]    Script Date: 07/25/2014 17:50:37 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_InsertOrUpdateCallRemedy]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_InsertOrUpdateCallRemedy]
GO

USE [Enterprise]
GO

/****** Object:  StoredProcedure [dbo].[usp_InsertOrUpdateCallRemedy]    Script Date: 07/25/2014 17:50:37 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE PROCEDURE [dbo].[usp_InsertOrUpdateCallRemedy]
(
	@RemedyId INT = NULL,
	@Description   VARCHAR(50),
    @Reference     INT = 0,
    @IsDowntime bit = 0,
    @EndDate DateTime = NULL,
    @RemedyIdOut  INT OUTPUT
)
AS
BEGIN	
	IF NOT EXISTS(Select 1 from dbo.Call_Remedy WHERE Call_Remedy_ID = @RemedyId)
		BEGIN
			INSERT INTO dbo.Call_Remedy ([Call_Remedy_Description],[Call_Remedy_Reference],[Call_Remedy_Attract_Downtime]) 
				VALUES (@Description,@Reference,@IsDowntime)
			SET @RemedyIdOut =  SCOPE_IDENTITY()
		END
	ELSE	
		BEGIN
			UPDATE dbo.Call_Remedy 
			SET
				[Call_Remedy_Description] = @Description,
				[Call_Remedy_Reference] = @Reference,
				[Call_Remedy_Attract_Downtime] = @IsDowntime,
				[Call_Remedy_End_Date] = @EndDate
			WHERE Call_Remedy_ID = @RemedyId
			SET @RemedyIdOut = @RemedyId
		END
END
GO


