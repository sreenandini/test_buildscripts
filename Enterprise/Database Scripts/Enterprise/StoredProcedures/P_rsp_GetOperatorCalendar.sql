USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetOperatorCalendar]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetOperatorCalendar]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE rsp_GetOperatorCalendar
	@Operator_ID INT,
	@Calendar_ID INT
AS
BEGIN
SELECT * FROM Operator_Calendar WHERE Operator_ID = @Operator_ID AND Calendar_ID = @Calendar_ID
END


GO

