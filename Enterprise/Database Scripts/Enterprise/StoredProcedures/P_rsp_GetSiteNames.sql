USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_GetSiteNames]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_GetSiteNames]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE rsp_GetSiteNames
(
	@Site_ID INT=0
	)
AS
BEGIN
	SELECT s.Site_ID,
	       s.Site_Name
	FROM   [Site] s
	WHERE (@Site_ID=0 OR (@Site_ID<>0 AND s.Site_ID=@Site_ID))
	ORDER BY
	       s.Site_Name
END
GO
