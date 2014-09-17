USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetDepreciationPolicy]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetDepreciationPolicy]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE rsp_GetDepreciationPolicy
@Depreciation_Policy_ID INT=NULL
AS
BEGIN
	SELECT *
	FROM   Depreciation_Policy dp WITH (NOLOCK)
	WHERE Depreciation_Policy_ID=COALESCE(@Depreciation_Policy_ID,Depreciation_Policy_ID)
	ORDER BY dp.Depreciation_Policy_Description
END

GO

