USE [Enterprise]
GO

IF EXISTS (
		SELECT *
		FROM sys.objects
		WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetTermsGroup]')
			AND type IN (
				N'P'
				,N'PC'
				)
		)
	DROP PROCEDURE [dbo].[rsp_GetTermsGroup]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE rsp_GetTermsGroup
AS
BEGIN
	SELECT Terms_Group_ID
		,Terms_Group_Name
	FROM Terms_Group
	ORDER BY Terms_Group_Name
END
GO


