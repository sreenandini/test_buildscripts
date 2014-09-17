USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_ExportExpenseShareInfoDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_ExportExpenseShareInfoDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[rsp_ExportExpenseShareInfoDetails]
(@ExpenseShareId INT)
AS
BEGIN
	SELECT ExpenseShareId,
	       ShareHolderId,
	       ExpenseShareGroupId,
	       ExpenseSharePercentage,
	       ExpenseShareDescription,
	       DateCreated,
	       DateModified,
	       SysDelete
	FROM   ExpenseShare WITH(NOLOCK)
	WHERE  ExpenseShareId = @ExpenseShareId 
	       FOR XML AUTO, ELEMENTS, ROOT('ExpenseShares')
END

GO

