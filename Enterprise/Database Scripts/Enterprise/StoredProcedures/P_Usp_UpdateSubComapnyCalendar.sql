USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Usp_UpdateSubComapnyCalendar]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Usp_UpdateSubComapnyCalendar]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].Usp_UpdateSubComapnyCalendar
(@Sub_Company_ID INT)
AS
BEGIN
	UPDATE Sub_Company_Calendar
	SET    Sub_Company_Calendar_Active = 0
	WHERE  Sub_Company_ID = @Sub_Company_ID
END


GO

