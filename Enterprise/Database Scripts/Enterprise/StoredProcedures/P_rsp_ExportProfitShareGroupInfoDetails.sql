USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_ExportProfitShareGroupInfoDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_ExportProfitShareGroupInfoDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[rsp_ExportProfitShareGroupInfoDetails]
(@ProfitShareGroupId INT)
AS
BEGIN
	SELECT ProfitShareGroupId,
	       ProfitShareGroupName,
	       ProfitSharePercentage,
	       ProfitShareGroupDescription,
	       DateCreated,
	       DateModified,
	       SysDelete
	FROM   ProfitShareGroup WITH(NOLOCK)
	WHERE  ProfitShareGroupId = @ProfitShareGroupId 
	       FOR XML AUTO, ELEMENTS, ROOT('ProfitShareGroups')
END

GO

