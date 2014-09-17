USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_CheckSite]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_CheckSite]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[rsp_CheckSite] (@SiteCode Varchar(50), @SiteID int)
AS

BEGIN

SELECT Site_ID FROM dbo.Site 
WHERE Site_Code = @SiteCode 
AND Site_ID <> @SiteID

END


GO

