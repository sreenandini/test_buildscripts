USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_DeleteAddEditExpenseShare]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_DeleteAddEditExpenseShare]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[usp_DeleteAddEditExpenseShare]
	@ExpenseShareGroupName VARCHAR(50)
AS
BEGIN
	DECLARE @ExpenseShareGroupId INT
	
	SELECT @ExpenseShareGroupId = ExpenseShareGroupId
	FROM   ExpenseShareGroup WITH(NOLOCK)
	WHERE  ExpenseShareGroupName = @ExpenseShareGroupName
	
	DELETE 
	FROM   ExpenseShare
	WHERE  ExpenseShareGroupId = @ExpenseShareGroupId
	
	DELETE 
	FROM   ExpenseShareGroup
	WHERE  ExpenseShareGroupId = @ExpenseShareGroupId
END

GO

