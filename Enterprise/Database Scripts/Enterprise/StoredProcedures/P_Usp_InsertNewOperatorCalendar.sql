USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Usp_InsertNewOperatorCalendar]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Usp_InsertNewOperatorCalendar]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].Usp_InsertNewOperatorCalendar
(
    @Operator_Id               INT,
    @Calendar_Id               INT,
    @Operator_Calendar_Active  BIT
)
AS
BEGIN
	INSERT INTO operator_Calendar
	  (
	    Operator_Id,
	    Calendar_Id,
	    Operator_Calendar_Active
	  )
	VALUES
	  (
	    @Operator_Id,
	    @Calendar_Id,
	    @Operator_Calendar_Active
	  )
END

--select * from operator


GO

