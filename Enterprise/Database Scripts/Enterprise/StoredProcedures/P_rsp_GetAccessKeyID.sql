USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetAccessKeyID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetAccessKeyID]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE rsp_GetAccessKeyID
@SiteID INT
AS
BEGIN
SELECT Sub_Company.Access_Key_ID 
FROM Sub_Company 
INNER JOIN Site 
ON Sub_Company.Sub_Company_ID = Site.Sub_Company_ID 
WHERE Site.Site_ID = @SiteID
END


GO

