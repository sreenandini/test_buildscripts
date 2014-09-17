USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_InsertExpenseShareGroupDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_InsertExpenseShareGroupDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- Author:		Aishwarrya v s
-- Create date: 8th October 2012
-- Description:	Insert ExpenseShareGroup Details
-- =============================================
CREATE PROCEDURE usp_InsertExpenseShareGroupDetails
	@ExpenseShareGroupName VARCHAR(50),
	@ExpenseSharePercentage FLOAT,
	@ExpenseShareGroupDescription VARCHAR(255)
AS
BEGIN
	DECLARE @ExpenseShareGroupId INT
	
	
	INSERT INTO ExpenseShareGroup
	  (
	    ExpenseShareGroupName,
	    ExpenseSharePercentage,
	    ExpenseShareGroupDescription
	  )
	VALUES
	  (
	    @ExpenseShareGroupName,
	    @ExpenseSharePercentage,
	    @ExpenseShareGroupDescription
	  )
	
	SET @ExpenseShareGroupId = CAST(SCOPE_IDENTITY() AS INT)
	INSERT INTO dbo.Export_History
	  (
	    EH_Date,
	    EH_Reference1,
	    EH_Type,
	    EH_Site_Code
	  )
	SELECT GETDATE(),
	       @ExpenseShareGroupId,
	       'EXPENSESHAREGROUP',
	       Site_Code
	FROM   SITE WITH(NOLOCK)
	WHERE  Site_Enabled = 1
	       AND sitestatus = 'FULLYCONFIGURED'
END

GO

