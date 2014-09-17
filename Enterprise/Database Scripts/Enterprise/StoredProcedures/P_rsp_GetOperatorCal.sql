USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetOperatorCal]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetOperatorCal]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE rsp_GetOperatorCal
	@Operator_ID INT
	
AS
BEGIN
SELECT * FROM Operator WHERE Operator_ID = @Operator_ID 
END


GO

