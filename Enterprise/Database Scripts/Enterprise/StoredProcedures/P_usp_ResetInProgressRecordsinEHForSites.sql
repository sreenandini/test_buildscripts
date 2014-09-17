USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_ResetInProgressRecordsinEHForSites]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_ResetInProgressRecordsinEHForSites]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE dbo.usp_ResetInProgressRecordsinEHForSites
(@Site_List VARCHAR(8000))
AS
BEGIN
	UPDATE EH
	SET    EH.EH_Status = NULL
	FROM   dbo.Export_History EH
	       INNER JOIN dbo.UDF_GetStringTable(@Site_List, ',') TT
	            ON  TT.[value] = EH.EH_Site_Code
	WHERE  EH.EH_Status = 1
END

GO

