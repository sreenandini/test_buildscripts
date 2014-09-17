USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_UpdateDepreciationPolicy]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_UpdateDepreciationPolicy]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE usp_UpdateDepreciationPolicy(
    @Depreciation_Policy_ID AS INT,
    @Depreciation_Policy_Description AS VARCHAR(50),
    @Depreciation_Policy_Residual_Value AS REAL
)
AS
BEGIN
	IF EXISTS(
	       SELECT 1
	       FROM   Depreciation_Policy WITH(NOLOCK)
	       WHERE  Depreciation_Policy_ID = @Depreciation_Policy_ID
	   )
	BEGIN
	    UPDATE Depreciation_Policy
	    SET    Depreciation_Policy_Description = @Depreciation_Policy_Description,
	           Depreciation_Policy_Residual_Value = @Depreciation_Policy_Residual_Value
	    WHERE  Depreciation_Policy_ID = @Depreciation_Policy_ID
	END
END

GO

