USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetAssetTemplateDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetAssetTemplateDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Create date: 05-04-2013
-- Description:	To get the Template Name
-- =============================================
CREATE PROCEDURE rsp_GetAssetTemplateDetails
AS
BEGIN
	Select AssetCrTempNumber,
	TemplateName FROM AssetCreationTemplate
	order by TemplateName
END

GO

