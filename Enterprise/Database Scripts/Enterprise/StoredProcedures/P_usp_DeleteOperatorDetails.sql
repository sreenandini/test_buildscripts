USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_DeleteOperatorDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_DeleteOperatorDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE usp_DeleteOperatorDetails(@OperatorID AS INT)
AS
BEGIN
	DELETE 
	FROM   Operator
	WHERE  Operator_ID = @OperatorID
END

GO

