USE [Enterprise]
GO

/****** Object:  StoredProcedure [dbo].[usp_InsertOrUpdateCallSource]    Script Date: 07/25/2014 17:46:27 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_InsertOrUpdateCallSource]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_InsertOrUpdateCallSource]
GO

USE [Enterprise]
GO

/****** Object:  StoredProcedure [dbo].[usp_InsertOrUpdateCallSource]    Script Date: 07/25/2014 17:46:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


Create PROCEDURE [dbo].[usp_InsertOrUpdateCallSource]
(
	@SourceId INT = NULL,
	@Description   VARCHAR(30),
    @Reference     VARCHAR(10),
    @SourceIdOut  INT OUTPUT
)
AS
BEGIN	
	IF NOT EXISTS(Select 1 from Call_Source WHERE Call_Source_ID = @SourceId)
		BEGIN
			INSERT INTO dbo.Call_Source ([Call_Source_Description],[Call_Source_Reference]) 
				VALUES (@Description, @Reference)
			SET @SourceIdOut =  SCOPE_IDENTITY()
		END
	ELSE	
		BEGIN
			UPDATE dbo.Call_Source
			SET
				[Call_Source_Description] = @Description,
				[Call_Source_Reference] = @Reference
			WHERE Call_Source_ID = @SourceId
			SET @SourceIdOut = @SourceId
		END
END

GO


