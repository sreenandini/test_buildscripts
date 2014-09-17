USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_InsertAddEditExpenseShare]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_InsertAddEditExpenseShare]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[usp_InsertAddEditExpenseShare]
	@ShareHolderId INT,
	@ExpenseSharePercentage FLOAT,
	@ExpenseShareDescription VARCHAR(255)
AS
BEGIN
	INSERT INTO ExpenseShare
	  (
	    ShareHolderId,
	    ExpenseSharePercentage,
	    ExpenseShareDescription
	  )
	VALUES
	  (
	    @ShareHolderId,
	    @ExpenseSharePercentage,
	    @ExpenseShareDescription
	  )
END

GO

