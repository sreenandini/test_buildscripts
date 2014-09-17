USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetExpenseShare]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetExpenseShare]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE rsp_GetExpenseShare
AS
BEGIN
	SELECT *
	FROM   ExpenseShare WITH(NOLOCK)
END

GO

