USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_ExportProfitShareInfoDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_ExportProfitShareInfoDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE rsp_ExportProfitShareInfoDetails(@ProfitShareId INT)
AS
BEGIN
	SELECT ProfitShareId,
	       ShareHolderId,
	       ProfitShareGroupId,
	       ProfitSharePercentage,
	       ProfitShareDescription,
	       DateCreated,
	       DateModified,
	       SysDelete
	FROM   ProfitShare WITH(NOLOCK)
	WHERE  ProfitShareId = @ProfitShareId 
	       FOR XML AUTO, ELEMENTS, ROOT('ProfitShares')
END

GO

