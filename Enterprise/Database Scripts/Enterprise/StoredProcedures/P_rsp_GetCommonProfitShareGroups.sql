USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetCommonProfitShareGroups]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetCommonProfitShareGroups]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- exec dbo.rsp_GetCommonProfitShareGroups 1
CREATE PROCEDURE dbo.rsp_GetCommonProfitShareGroups
(@CommonProfitShareType SMALLINT)
AS
BEGIN
	SET NOCOUNT ON
	-- BEGIN
	
	IF (@CommonProfitShareType = 1)
	BEGIN
	    SELECT [ExpenseShareGroupId] AS [Id],
	           [ExpenseShareGroupName] AS [Name],
	           [ExpenseSharePercentage] AS [Percentage],
	           [ExpenseShareGroupDescription] AS [Description]
	    FROM   [dbo].[ExpenseShareGroup] WITH(NOLOCK)
	    WHERE  SysDelete = 0
	    ORDER BY
	           2 ASC
	END
	ELSE
	BEGIN
	    SELECT [ProfitShareGroupId] AS [Id],
	           [ProfitShareGroupName] AS [Name],
	           [ProfitSharePercentage] AS [Percentage],
	           [ProfitShareGroupDescription] AS [Description]
	    FROM   [dbo].[ProfitShareGroup] WITH(NOLOCK)
	    WHERE  SysDelete = 0
	    ORDER BY
	           2 ASC
	END
	
	-- END
	SET NOCOUNT OFF
END

GO

