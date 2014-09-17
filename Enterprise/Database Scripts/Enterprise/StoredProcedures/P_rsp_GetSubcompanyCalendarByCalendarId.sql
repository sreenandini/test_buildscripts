USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetSubcompanyCalendarByCalendarId]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetSubcompanyCalendarByCalendarId]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].rsp_GetSubcompanyCalendarByCalendarId
(@Sub_Company_ID INT, @Calendar_ID INT)
AS
BEGIN
	SELECT *
	FROM   Sub_Company_Calendar
	WHERE  Sub_Company_ID = @Sub_Company_ID
	       AND Calendar_ID = @Calendar_ID
END


GO

