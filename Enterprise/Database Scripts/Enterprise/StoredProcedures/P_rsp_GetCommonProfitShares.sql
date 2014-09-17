USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetCommonProfitShares]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetCommonProfitShares]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- exec dbo.rsp_GetCommonProfitShares 0, 1
CREATE PROCEDURE dbo.rsp_GetCommonProfitShares
(@CommonProfitShareType SMALLINT, @ParentGroupId INT = 0)
AS
BEGIN
	SET NOCOUNT ON
	-- BEGIN
	
	IF (@CommonProfitShareType = 1)
	BEGIN
	    SELECT [ExpenseShareId] AS [Id],
	           [ExpenseSharePercentage] AS [Percentage],
	           [ExpenseShareDescription] AS [Description],
	           SH.[ShareHolderId] AS ShareHolderId,
	           SH.[ShareHolderName] AS ShareHolderName
	    FROM   [dbo].[ExpenseShare] ES WITH(NOLOCK)
	           INNER JOIN [dbo].[ShareHolders] SH WITH(NOLOCK)
	                ON  ES.ShareHolderId = SH.ShareHolderId
	    WHERE  ES.ExpenseShareGroupId = @ParentGroupId
	           AND ES.SysDelete = 0
	           AND SH.SysDelete = 0
	    ORDER BY
	           1 ASC
	END
	ELSE
	BEGIN
	    SELECT [ProfitShareId] AS [Id],
	           [ProfitSharePercentage] AS [Percentage],
	           [ProfitShareDescription] AS [Description],
	           SH.[ShareHolderId] AS ShareHolderId,
	           SH.[ShareHolderName] AS ShareHolderName
	    FROM   [dbo].[ProfitShare] ES WITH(NOLOCK)
	           INNER JOIN [dbo].[ShareHolders] SH WITH(NOLOCK)
	                ON  ES.ShareHolderId = SH.ShareHolderId
	    WHERE  ES.ProfitShareGroupId = @ParentGroupId
	           AND ES.SysDelete = 0
	           AND SH.SysDelete = 0
	    ORDER BY
	           1 ASC
	END
	
	-- END
	SET NOCOUNT OFF
END

GO

