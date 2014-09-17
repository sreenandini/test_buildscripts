USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetAssetNumberForTemplate]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetAssetNumberForTemplate]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[rsp_GetAssetNumberForTemplate]
	@TemplateName VARCHAR(50)
AS
BEGIN
	SELECT Machine_Stock_No
	FROM   AssetCreationTemplate
	WHERE  TemplateName = @TemplateName
END

GO

