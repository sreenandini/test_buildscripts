USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fn_IsOperatorExists]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[fn_IsOperatorExists]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION fn_IsOperatorExists(@OperatorName VARCHAR(2000) ,
      @OperatorID INT) RETURNS BIT
AS
BEGIN
	IF EXISTS(
	       SELECT 1
	       FROM   Operator WITH(NOLOCK)
	       WHERE  Operator_ID <> @OperatorID
		   and RTRIM(LTRIM(Operator_Name)) = RTRIM(LTRIM(@OperatorName))) 
	BEGIN
	       RETURN 1
	END
	RETURN 0
	
END

GO