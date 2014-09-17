USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetExpenseShareGroupList]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetExpenseShareGroupList]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [rsp_GetExpenseShareGroupList]
AS
BEGIN
	
	SELECT 
		ESG.ExpenseShareGroupId,
		ESG.ExpenseShareGroupName,
		CAST(ISNULL((SELECT B.ExpenseSharePercentage
			FROM  (SELECT ExpenseSharePercentage, ShareHolderId FROM ExpenseShare ES WITH(NOLOCK)      
					WHERE  ES.ExpenseShareGroupId = ESG.ExpenseShareGroupId AND ES.SysDelete = 0)B  
			WHERE B.ShareHolderId = 3), 0) AS FLOAT) AS ExpenseSharePercentage 
	FROM ExpenseShareGroup ESG WITH(NOLOCK)
	WHERE ESG.SysDelete = 0
	ORDER BY ExpenseShareGroupName
	
END

GO

