USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetExportCalendar]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetExportCalendar]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE rsp_GetExportCalendar(@Sub_Company_ID INT)
AS
BEGIN
	SELECT SITE.Site_ID AS Site_ID,
	       SITE.Site_Name AS Site_Name
	FROM   (
	           (
	               Calendar INNER JOIN Sub_Company_Calendar ON Calendar.Calendar_ID 
	               = Sub_Company_Calendar.Calendar_ID
	           ) INNER JOIN Sub_Company ON Sub_Company_Calendar.Sub_Company_ID =
	           Sub_Company.Sub_Company_ID
	       )
	       INNER JOIN SITE
	            ON  Sub_Company.Sub_Company_ID = SITE.Sub_Company_ID
	WHERE  Calendar.Calendar_ID IN (select Calendar_ID from Sub_Company_Calendar where Sub_Company_ID=@Sub_Company_ID)
	       AND Sub_Company.Sub_Company_ID = @Sub_Company_ID
END


GO

