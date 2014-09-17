USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_GetSiteNameForSubCompanyID]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_GetSiteNameForSubCompanyID]
GO

CREATE PROCEDURE [dbo].[rsp_GetSiteNameForSubCompanyID] (@SubCompanyID INT = 0)
AS
BEGIN
	SELECT Site_ID,
	       Site_Name
	FROM   [Site]
	WHERE  (@SubCompanyID = 0 OR Sub_Company_ID = @SubCompanyID)
	ORDER BY
	       Site_Name ASC
END
GO
