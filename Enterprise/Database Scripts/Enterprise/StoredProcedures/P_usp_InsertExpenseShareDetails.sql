USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_InsertExpenseShareDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_InsertExpenseShareDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- Author:		Aishwarrya v s
-- Create date: 8th October 2012
-- Description:	Insert Expense Share Details
-- =============================================


CREATE PROCEDURE usp_InsertExpenseShareDetails
	@ExpenseSharePercentage FLOAT,
	@ExpenseShareDescription VARCHAR(255)
AS
BEGIN
	DECLARE @ShareHolderId               INT
	DECLARE @ExpenseShareGroupId               INT
	DECLARE @ExpenseShareId  INT
	
	
	SELECT @ShareHolderId = s.ShareHolderId,
	       @ExpenseShareGroupId = E.ExpenseShareGroupId
	FROM   ShareHolders S WITH(NOLOCK)
	       INNER JOIN ExpenseShareGroup E WITH(NOLOCK)
	            ON  s.ShareHolderId = E.ExpenseShareGroupId 
	
	INSERT INTO ExpenseShare
	  (
	    ShareHolderId,
	    ExpenseShareGroupId,
	    ExpenseSharePercentage,
	    ExpenseShareDescription
	  )
	VALUES
	  (
	    @ShareHolderId,
	    @ExpenseShareGroupId,
	    @ExpenseSharePercentage,
	    @ExpenseShareDescription
	  )
	
	SET @ExpenseShareId = CAST(SCOPE_IDENTITY() AS INT)
	INSERT INTO dbo.Export_History
	  (
	    EH_Date,
	    EH_Reference1,
	    EH_Type,
	    EH_Site_Code
	  )
	SELECT GETDATE(),
	       @ExpenseShareId,
	       'EXPENSESHARE',
	       Site_Code
	FROM   SITE WITH(NOLOCK)
	WHERE  Site_Enabled = 1
	       AND sitestatus = 'FULLYCONFIGURED'
END

GO

