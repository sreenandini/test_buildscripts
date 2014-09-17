USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetCommonProfitSharePercentage]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetCommonProfitSharePercentage]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- exec dbo.rsp_GetCommonProfitSharePercentage 0, 1, 1
CREATE PROCEDURE dbo.rsp_GetCommonProfitSharePercentage
(
    @CommonProfitShareType  SMALLINT,
    @ParentGroupId          INT = 0,
    @ShareId                INT = 0
)
AS
BEGIN
	SET NOCOUNT ON
	-- BEGIN
	
	IF (@CommonProfitShareType = 1)
	BEGIN
	    SELECT SUM(ES.[ExpenseSharePercentage]) AS TotalPercentage
	    FROM   [dbo].[ExpenseShare] ES WITH(NOLOCK)
	           INNER JOIN [dbo].[ShareHolders] SH WITH(NOLOCK)
	                ON  ES.ShareHolderId = SH.ShareHolderId
	           INNER JOIN [dbo].[ExpenseShareGroup] PSG WITH(NOLOCK)
	                ON  PSG.ExpenseShareGroupId = ES.ExpenseShareGroupId
	    WHERE  PSG.ExpenseShareGroupId = @ParentGroupId
	           AND ES.ExpenseShareId <> @ShareId
	           AND ES.SysDelete = 0
	END
	ELSE
	BEGIN
	    SELECT SUM(ES.[ProfitSharePercentage]) AS TotalPercentage
	    FROM   [dbo].[ProfitShare] ES WITH(NOLOCK)
	           INNER JOIN [dbo].[ShareHolders] SH WITH(NOLOCK)
	                ON  ES.ShareHolderId = SH.ShareHolderId
	           INNER JOIN [dbo].[ProfitShareGroup] PSG WITH(NOLOCK)
	                ON  PSG.ProfitShareGroupId = ES.ProfitShareGroupId
	    WHERE  PSG.ProfitShareGroupId = @ParentGroupId
	           AND ES.ProfitShareId <> @ShareId
	           AND ES.SysDelete = 0
	END
	
	-- END
	SET NOCOUNT OFF
END

GO

