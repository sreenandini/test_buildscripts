USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetOperatorByActive]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetOperatorByActive]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].rsp_GetOperatorByActive
(@Operator_Id INT)
AS
BEGIN
SELECT * FROM Operator_Calendar WHERE Operator_ID = @Operator_Id AND Operator_Calendar_Active = 1
End


GO

