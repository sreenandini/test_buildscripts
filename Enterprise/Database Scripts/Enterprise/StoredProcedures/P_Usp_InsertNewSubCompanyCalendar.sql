USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Usp_InsertNewSubCompanyCalendar]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Usp_InsertNewSubCompanyCalendar]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].Usp_InsertNewSubCompanyCalendar
(
    @Sub_Company_ID               INT,
    @Calendar_ID                  INT,
    @Sub_Company_Calendar_Active  BIT
)
AS
BEGIN
	INSERT INTO Sub_Company_Calendar
	  (
	    Sub_Company_ID,
	    Calendar_ID,
	    Sub_Company_Calendar_Active
	  )
	VALUES
	  (
	    @Sub_Company_ID,
	    @Calendar_ID,
	    @Sub_Company_Calendar_Active
	  )
END


GO

