USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_DeleteExpenseShare]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_DeleteExpenseShare]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE usp_DeleteExpenseShare(
    @ExpenseShareGroupId  INT,
    @ExpenseShareId       INT,
    @Status               INT OUTPUT
)
AS
BEGIN
	UPDATE ExpenseShare
	SET    SysDelete = 1
	WHERE  ExpenseShareId = @ExpenseShareId
	
	SET @Status = 0
	IF @@ROWCOUNT > 0
	    SET @Status = 1
	
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
	
	UPDATE dbo.ExpenseSharegroup
	SET    ExpenseSharePercentage = COALESCE((
	           SELECT SUM(PS.ExpenseSharePercentage)
	           FROM   dbo.ExpenseSharegroup PSG WITH(NOLOCK)
	                  INNER JOIN dbo.ExpenseShare PS WITH(NOLOCK)
	                       ON  PSG.ExpenseShareGroupId = PS.ExpenseShareGroupId
	           WHERE  PSG.ExpenseShareGroupId = @ExpenseShareGroupId
	                  AND PS.SysDelete = 0
	       ), 0)
	WHERE  ExpenseShareGroupId = @ExpenseShareGroupId
END

GO

