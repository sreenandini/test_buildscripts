USE [Enterprise]
GO

/****** Object:  StoredProcedure [dbo].[usp_InsertOrUpdateCallGroup]    Script Date: 07/25/2014 17:44:11 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_InsertOrUpdateCallGroup]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_InsertOrUpdateCallGroup]
GO

USE [Enterprise]
GO

/****** Object:  StoredProcedure [dbo].[usp_InsertOrUpdateCallGroup]    Script Date: 07/25/2014 17:44:11 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[usp_InsertOrUpdateCallGroup]
(
	@GroupId INT = NULL,
	@Description   VARCHAR(50),
    @Reference     VARCHAR(50),
    @IsDowntime bit = 0,
    @IsLogEngineerChange bit = 0,
    @EndDate DateTime = NULL,
    @GroupIDOut  INT OUTPUT
)
AS
BEGIN	
	IF NOT EXISTS(Select 1 from dbo.Call_Group WHERE Call_Group_ID = @GroupId)
		BEGIN
			INSERT INTO dbo.Call_Group ([Call_Group_Description],[Call_Group_Reference],[Call_Group_Downtime],[Call_Group_Log_Engineer_Change]) 
				VALUES (@Description,@Reference,@IsDowntime,@IsLogEngineerChange)
			SET @GroupIDOut =  SCOPE_IDENTITY()
		END
	ELSE	
		BEGIN
			UPDATE dbo.Call_Group 
			SET
				[Call_Group_Description] = @Description,
				[Call_Group_Reference] = @Reference,
				[Call_Group_Downtime] = @IsDowntime,			
				[Call_Group_Log_Engineer_Change] = @IsLogEngineerChange,
				[Call_Group_End_Date] = @EndDate
			WHERE Call_Group_ID = @GroupId
			SET @GroupIDOut = @GroupId
		END
END


GO


