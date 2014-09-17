USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetReasonForSession]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetReasonForSession]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE rsp_GetReasonForSession (@SessionID INT, @SiteID INT)
AS 
BEGIN

SELECT 
	Category = tLC.DisplayText,
	Reason = tLR.DisplayText,
	Comment = tMR.Comments
FROM
MaintenanceReasonCategory tMR
INNER JOIN LookupMaster tLC ON tMR.CategoryID = tLC.ID
INNER JOIN LookupMaster tLR ON tMR.CategoryID = tLR.ID
WHERE SessionID = @SessionID AND tMR.Site_ID = @SiteID
END

GO

