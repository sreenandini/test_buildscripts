USE [Enterprise]
GO

/****** Object:  StoredProcedure [dbo].[rsp_GetSiteSettingByCode]    Script Date: 02/06/2013 16:41:56 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetSiteSettingByCode]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetSiteSettingByCode]
GO

USE [Enterprise]
GO

/****** Object:  StoredProcedure [dbo].[rsp_GetSiteSettingByCode]    Script Date: 02/06/2013 16:41:56 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/************************************************************
 * Code formatted by SoftTree SQL Assistant © v4.8.29
 * Time: 06/02/2013 4:37:40 PM
 ************************************************************/

CREATE PROCEDURE [dbo].[rsp_GetSiteSettingByCode]
	@Site_Code VARCHAR(20),
	@SettingMaster_Name VARCHAR(100),
	@Setting_Value VARCHAR(500) OUTPUT
AS
BEGIN
	DECLARE @Site_Id INT
	SELECT @Site_Id = Site_ID
	FROM   SITE s
	WHERE  site_code = @Site_Code
	
	EXEC [rsp_GetSiteSetting] @Site_ID,
	     'DailyAutoReadTime',
	     @Setting_Value OUTPUT
	     
     
END

GO


