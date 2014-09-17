USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_ExportExpenseShareGroupInfoDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_ExportExpenseShareGroupInfoDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[rsp_ExportExpenseShareGroupInfoDetails]
(@ExpenseShareGroupId INT)
AS
BEGIN
	SELECT ExpenseShareGroupId,
	       ExpenseShareGroupName,
	       ExpenseSharePercentage,
	       ExpenseShareGroupDescription,
	       DateCreated,
	       DateModified,
	       SysDelete
	FROM   ExpenseShareGroup WITH(NOLOCK)
	WHERE  ExpenseShareGroupId = @ExpenseShareGroupId 
	       FOR XML AUTO, ELEMENTS, ROOT('ExpenseShareGroups')
END 

GO

