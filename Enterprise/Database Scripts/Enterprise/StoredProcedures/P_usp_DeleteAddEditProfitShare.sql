USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_DeleteAddEditProfitShare]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_DeleteAddEditProfitShare]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[usp_DeleteAddEditProfitShare]
	@ProfitShareGroupName VARCHAR(50)
AS
BEGIN
	DECLARE @ProfitShareGroupId INT
	
	SELECT @ProfitShareGroupId = ProfitShareGroupId
	FROM   ProfitShareGroup WITH(NOLOCK)
	WHERE  ProfitShareGroupName = @ProfitShareGroupName
	
	DELETE 
	FROM   ProfitShare
	WHERE  ProfitShareGroupId = @ProfitShareGroupId
	
	DELETE 
	FROM   ProfitShareGroup
	WHERE  ProfitShareGroupId = @ProfitShareGroupId
	
	SET @ProfitShareGroupId = CAST(SCOPE_IDENTITY() AS INT)
	INSERT INTO dbo.Export_History
	  (
	    EH_Date,
	    EH_Reference1,
	    EH_Type,
	    EH_Site_Code
	  )
	SELECT GETDATE(),
	       @ProfitShareGroupId,
	       'PROFITSHAREGROUP',
	       Site_Code
	FROM   SITE WITH(NOLOCK)
	WHERE  Site_Enabled = 1
	       AND sitestatus = 'FULLYCONFIGURED'
END

GO

