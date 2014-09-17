USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_ExportShareHolderInfoDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_ExportShareHolderInfoDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[rsp_ExportShareHolderInfoDetails]
	@ShareHolderId INT
AS
BEGIN
	SELECT ShareHolderId,
	       ShareHolderName,
	       ShareHolderDescription,
	       DateCreated,
	       DateModified,
	       SysDelete,
	       SHKey
	FROM   ShareHolders WITH(NOLOCK)
	WHERE  ShareHolderId = @ShareHolderId
	       FOR XML AUTO, ELEMENTS, ROOT('ShareHolder')
END

GO

