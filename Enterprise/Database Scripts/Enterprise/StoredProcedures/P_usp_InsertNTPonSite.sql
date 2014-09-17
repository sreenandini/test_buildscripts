USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_InsertNTPonSite]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_InsertNTPonSite]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE  PROCEDURE [dbo].[usp_InsertNTPonSite] 
(
	@SiteID int,
	@NTPFrom varchar(30),
	@NTPTo	varchar(30)
)
AS

BEGIN
	UPDATE Site
	SET
	Site_Non_Trading_Period_From = @NTPFrom,
	Site_Non_Trading_Period_To = @NTPTo
	WHERE
	Site_ID = @SiteID
END

GO

