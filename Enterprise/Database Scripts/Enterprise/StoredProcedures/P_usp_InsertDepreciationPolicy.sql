USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_InsertDepreciationPolicy]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_InsertDepreciationPolicy]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE usp_InsertDepreciationPolicy(
    @Depreciation_Policy_Description     AS VARCHAR(50),
    @Depreciation_Policy_Residual_Value  AS INT,
    @Depreciation_Policy_ID AS INT OUTPUT
)
AS
BEGIN
	IF NOT EXISTS(
	       SELECT 1
	       FROM   Depreciation_Policy WITH (NOLOCK)
	       WHERE  Depreciation_Policy_Description = @Depreciation_Policy_Description
	   )
	BEGIN
	    INSERT INTO Depreciation_Policy
	      (
	        Depreciation_Policy_Description,
	        Depreciation_Policy_Residual_Value
	      )
	    VALUES
	      (
	        @Depreciation_Policy_Description,
	        @Depreciation_Policy_Residual_Value
	      )SELECT @Depreciation_Policy_ID=SCOPE_IDENTITY()
	END
END

GO

