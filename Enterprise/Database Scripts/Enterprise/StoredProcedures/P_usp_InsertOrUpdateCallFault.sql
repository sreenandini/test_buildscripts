USE [Enterprise]
GO

/****** Object:  StoredProcedure [dbo].[usp_InsertOrUpdateCallFault]    Script Date: 07/25/2014 17:45:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_InsertOrUpdateCallFault]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_InsertOrUpdateCallFault]
GO

USE [Enterprise]
GO

/****** Object:  StoredProcedure [dbo].[usp_InsertOrUpdateCallFault]    Script Date: 07/25/2014 17:45:49 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[usp_InsertOrUpdateCallFault]
(
	@FaultId INT = NULL,
	@GroupId INT,
	@Description   VARCHAR(50),
    @Reference     VARCHAR(20),
    @EndDate DateTime = NULL,
    @FaultIdOut  INT OUTPUT
)
AS
BEGIN	
	IF NOT EXISTS(Select 1 from dbo.Call_Fault WHERE Call_Fault_ID = @FaultId)
		BEGIN
			INSERT INTO dbo.Call_Fault ([Call_Group_ID], [Call_Fault_Description],[Call_Fault_Reference]) 
				VALUES (@GroupId, @Description, @Reference)
			SET @FaultIdOut =  SCOPE_IDENTITY()
		END
	ELSE	
		BEGIN
			UPDATE dbo.Call_Fault
			SET
				[Call_Group_ID] = @GroupId,
				[Call_Fault_Description] = @Description,
				[Call_Fault_Reference] = @Reference,
				[Call_Fault_End_Date] = @EndDate
			WHERE Call_Fault_ID = @FaultId
			SET @FaultIdOut = @FaultId
		END
END

GO


