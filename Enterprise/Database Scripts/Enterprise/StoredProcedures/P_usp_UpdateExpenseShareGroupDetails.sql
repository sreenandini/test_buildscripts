USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_UpdateExpenseShareGroupDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_UpdateExpenseShareGroupDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- Author:		Aishwarrya v s
-- Create date: 8th October 2012
-- Description:	Update ExpenseShareGroup Details
-- =============================================
CREATE PROCEDURE usp_UpdateExpenseShareGroupDetails(
    @ExpenseShareGroupId           INT,
    @ExpenseShareGroupName         VARCHAR(50),
    @ExpenseSharePercentage        FLOAT,
    @ExpenseShareGroupDescription  VARCHAR(255),
    @ExpenseShareGroupIdOut        INT = 0 OUTPUT
)
AS
BEGIN
	IF NOT EXISTS(
	       SELECT 1
	       FROM   [dbo].[ExpenseShareGroup] WITH (NOLOCK)
	       WHERE  ExpenseShareGroupId = @ExpenseShareGroupId
	   )
	BEGIN
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
	    
	    SET @ExpenseShareGroupId = SCOPE_IDENTITY()
	    SET @ExpenseShareGroupIdOut = @ExpenseShareGroupId
	END
	ELSE
	BEGIN
	    UPDATE ExpenseShareGroup
	    SET    ExpenseShareGroupName = @ExpenseShareGroupName,
	           ExpenseSharePercentage = @ExpenseSharePercentage,
	           ExpenseShareGroupDescription = @ExpenseShareGroupDescription,
	           DateModified = GETDATE()
	    WHERE  ExpenseShareGroupId = @ExpenseShareGroupID
	    SET @ExpenseShareGroupIdOut = @ExpenseShareGroupId
	END
	
	IF @@ROWCOUNT > 0
	BEGIN
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
END

GO

