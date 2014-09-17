USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_CheckShareholder_ExistsIn_PSG_Or_ESG]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_CheckShareholder_ExistsIn_PSG_Or_ESG]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- exec dbo.rsp_GetCommonProfitShares 0, 1
CREATE PROCEDURE dbo.rsp_CheckShareholder_ExistsIn_PSG_Or_ESG
(
	@ShareholderId INT, 
	@IsExists BIT OUTPUT)
AS
BEGIN
	
	SET NOCOUNT ON
	
	DECLARE @Count INT
	SET @IsExists = 0
	
	SELECT 
		@Count = COUNT(PS.ProfitShareId)
	FROM  [dbo].[ProfitShare] PS WITH(NOLOCK)
       INNER JOIN [dbo].[ShareHolders] SH WITH(NOLOCK)
            ON  PS.ShareHolderId = SH.ShareHolderId
	WHERE SH.ShareHolderId = @ShareholderId
		AND PS.SysDelete = 0
	
	IF (@Count > 0)
	BEGIN
		SET @IsExists = 1
		RETURN
	END
	
	SELECT 
		@Count = COUNT(ES.ExpenseShareId)
	FROM   [dbo].[ExpenseShare] ES WITH(NOLOCK)
       INNER JOIN [dbo].[ShareHolders] SH WITH(NOLOCK)
            ON  ES.ShareHolderId = SH.ShareHolderId
	WHERE SH.ShareHolderId = @ShareholderId
		AND ES.SysDelete = 0
	
	IF (@Count > 0)
	BEGIN
		SET @IsExists = 1
	END
END
GO