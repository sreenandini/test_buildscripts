USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Usp_UpdateOperator]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Usp_UpdateOperator]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].Usp_UpdateOperator
(@Operator_Id INT)
AS
BEGIN
	UPDATE operator_calendar
	SET    Operator_Calendar_Active = 0
	WHERE  Operator_ID = @Operator_Id
END


GO

