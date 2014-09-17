USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_GetSubCompanyNameForCompanyID]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_GetSubCompanyNameForCompanyID]
GO

CREATE PROCEDURE [dbo].[rsp_GetSubCompanyNameForCompanyID]
(@CompanyID INT = 0)
AS
BEGIN
	SELECT Sub_Company_ID,
	       Sub_Company_Name
	FROM   [dbo].Sub_Company
	WHERE  (@CompanyID = 0 OR Company_ID = @CompanyID)
	ORDER BY
	       Sub_Company_Name ASC
END
GO
