USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_GetSubCompanyNames]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_GetSubCompanyNames]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE rsp_GetSubCompanyNames
AS
BEGIN
	SELECT sc.Sub_Company_ID,
	       sc.Sub_Company_Name
	FROM   Sub_Company sc
	ORDER BY
	       sc.Sub_Company_Name
END
GO
