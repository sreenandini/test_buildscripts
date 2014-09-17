USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fn_IsDepreciationPolicyExists]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[fn_IsDepreciationPolicyExists]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION fn_IsDepreciationPolicyExists(@Depreciation_Policy_Description VARCHAR(2000)) RETURNS BIT
AS
BEGIN
	IF EXISTS(
	       SELECT 1
	       FROM   Depreciation_Policy WITH(NOLOCK)
	       WHERE  RTRIM(LTRIM(Depreciation_Policy_Description)) = RTRIM(LTRIM(@Depreciation_Policy_Description))) 
	BEGIN
	       RETURN 1
	END
	RETURN 0
	
END

GO

