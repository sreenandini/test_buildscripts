USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[USP_ResetImportHistoryRecordsForSites]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[USP_ResetImportHistoryRecordsForSites]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[USP_ResetImportHistoryRecordsForSites](@SiteCode VARCHAR(8000) = 'ALL')
AS
BEGIN
	IF @SiteCode = 'ALL'
	BEGIN
	    UPDATE IMPORT_HISTORY
	    SET    IH_Status = 0
	    WHERE  IH_Status = 1
	END
	ELSE
	BEGIN
	    UPDATE IH
	    SET    IH.IH_Status = 0
	    FROM   IMPORT_HISTORY IH
	           INNER JOIN UDF_GetStringTable(@SiteCode, ',') FIH
	                ON  IH.IH_Site_Code = FIH.value
	    WHERE  IH.IH_Status = 1
	END
END

GO

