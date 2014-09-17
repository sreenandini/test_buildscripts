USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_UpdateExpenseShareDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_UpdateExpenseShareDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- Author:		Aishwarrya v s
-- Create date: 8th October 2012
-- Description:	Update ExpenseShare Details
-- =============================================
CREATE PROCEDURE usp_UpdateExpenseShareDetails
	@ShareHolderId INT,
	@ExpenseShareGroupId INT,
	@ExpenseShareId INT,
	@ExpenseSharePercentage FLOAT,
	@ExpenseShareDescription VARCHAR(255)
AS
BEGIN
	IF NOT EXISTS(
	       SELECT 1
	       FROM   [dbo].[ExpenseShare] WITH (NOLOCK)
	       WHERE  ExpenseShareId = @ExpenseShareId
	   )
	BEGIN
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
	    SET @ExpenseShareId = SCOPE_IDENTITY()
	END
	ELSE
	BEGIN
	    UPDATE ExpenseShare
	    SET    ShareHolderId = @ShareHolderId,
	           ExpenseShareGroupId = @ExpenseShareGroupId,
	           ExpenseSharePercentage = @ExpenseSharePercentage,
	           ExpenseShareDescription = @ExpenseShareDescription,
	           DateModified = GETDATE()
	    WHERE  ExpenseShareId = @ExpenseShareId
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
	           @ExpenseShareId,
	           'EXPENSESHARE',
	           Site_Code
	    FROM   SITE WITH(NOLOCK)
	    WHERE  Site_Enabled = 1
	           AND sitestatus = 'FULLYCONFIGURED'
	END
	
	UPDATE dbo.ExpenseSharegroup
	SET    ExpenseSharePercentage = (
	           SELECT SUM(PS.ExpenseSharePercentage)
	           FROM   dbo.ExpenseSharegroup PSG WITH(NOLOCK)
	                  INNER JOIN dbo.ExpenseShare PS WITH(NOLOCK)
	                       ON  PSG.ExpenseShareGroupId = PS.ExpenseShareGroupId
	           WHERE  PSG.ExpenseShareGroupId = @ExpenseShareGroupId
	                  AND PS.SysDelete = 0
	       )
	WHERE  ExpenseShareGroupId = @ExpenseShareGroupId
END

GO

