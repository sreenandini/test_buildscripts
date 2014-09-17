USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetSubCompanyCalenderByActive]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetSubCompanyCalenderByActive]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE rsp_GetSubCompanyCalenderByActive
	@SubCompanyId INT
AS
BEGIN
	SELECT *
	FROM   Sub_Company_Calendar
	WHERE  Sub_Company_ID = @SubCompanyId
	       AND Sub_Company_Calendar_Active = 1
END


GO

