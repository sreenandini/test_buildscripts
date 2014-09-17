USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetSiteOpeningHours]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetSiteOpeningHours]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[rsp_GetSiteOpeningHours] (@SiteID int)
AS

BEGIN

SELECT  Site_Name,
		Site_Open_Monday,
		Site_Open_Tuesday,
		Site_Open_Wednesday,
		Site_Open_Thursday,
		Site_Open_Friday,
		Site_Open_Saturday,
		Site_Open_Sunday
FROM Site
WHERE Site_ID = @SiteID

END

GO

